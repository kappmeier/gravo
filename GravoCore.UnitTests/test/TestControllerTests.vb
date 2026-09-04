Imports Microsoft.Data.Sqlite
Imports System.IO
Imports Gravo
Imports Moq
Imports NUnit.Framework
Imports FluentAssertions
Imports GravoCore.UnitTests.GroupDaoTests

''' <summary>
''' Hybrid tests for GravoCore/test/TestController.vb sticking the current behavior. TestController uses
''' a concrete DictionaryDao/CardsDao implementation, so it is not cleanly mockable:
'''' a real SQLite fixture (a temp copy of test-data-cards.s3db) is used for the tests.
''' </summary>
<TestFixture>
Public Class TestControllerTests
    Private ReadOnly ResourceFile As String = "test-data-cards.s3db"
    Private _tempDb As String
    Private _db As IDataBaseOperation

    <SetUp>
    Public Sub Setup()
        _tempDb = Path.GetTempFileName
        File.Copy(DaoUtils.GetSqliteResource(ResourceFile), _tempDb, True)

        _db = New SQLiteDataBaseOperation()
        _db.Open(_tempDb)
    End Sub

    <TearDown>
    Public Sub CleanUp()
        _db.Close()
        SqliteConnection.ClearAllPools()

        File.Delete(_tempDb)
    End Sub

    Private Function CreateController(words As List(Of WordEntry), cardsMock As Mock(Of ICardsDao), queryLanguage As QueryLanguage) As TestController
        Dim testData As TestData = New TestData(cardsMock.Object, words, queryLanguage)
        Return New TestController(testData, queryLanguage, _db)
    End Function

    Private Function CreateWord1Controller(queryLanguage As QueryLanguage) As TestController
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        cardsMock.Setup(Function(x) x.Skip(GroupDaoTests.word1, queryLanguage)).Returns(False)
        Return CreateController(New List(Of WordEntry) From {GroupDaoTests.word1}, cardsMock, queryLanguage)
    End Function

    Private Function ReadCardColumn(column As String, index As Integer) As Integer
        Dim command = "SELECT [" & column & "] FROM [Cards] WHERE [Index] = ?"
        _db.ExecuteReader(command, Enumerable.Repeat(CObj(index), 1))
        _db.DBCursor.Read()
        Dim value As Integer = _db.SecureGetInt32(0)
        _db.DBCursor.Close()
        Return value
    End Function

    <Test>
    Public Sub Constructor_WithWords_PresentsHeadWordAndChecksSkipOnce()
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        cardsMock.Setup(Function(x) x.Skip(GroupDaoTests.word1, QueryLanguage.TargetLanguage)).Returns(False)
        Dim words As New List(Of WordEntry) From {GroupDaoTests.word1, GroupDaoTests.word3}

        Dim controller As TestController = CreateController(words, cardsMock, QueryLanguage.TargetLanguage)

        controller.HasWords().Should.Be(True)
        controller.Count().Should.Be(2)
        controller.GetTestChecker().Question.Should.Be(GroupDaoTests.word1.Word)
        controller.GetTestChecker().Retest.Should.Be(False)
        cardsMock.Verify(Function(x) x.Skip(GroupDaoTests.word1, QueryLanguage.TargetLanguage), Times.Exactly(1))
    End Sub

    <Test>
    Public Sub Constructor_WithEmptyTestData_HasNoCheckerAndCountIsZero()
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        Dim words As New List(Of WordEntry)

        Dim controller As TestController = CreateController(words, cardsMock, QueryLanguage.TargetLanguage)

        controller.HasWords().Should.Be(False)
        controller.GetTestChecker().Should.BeNull()
        controller.Count().Should.Be(0)
    End Sub

    <Test>
    Public Sub Constructor_WithAllSkippableWords_EndsTestImmediately()
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        cardsMock.Setup(Function(x) x.Skip(GroupDaoTests.word1, QueryLanguage.TargetLanguage)).Returns(True)
        Dim words As New List(Of WordEntry) From {GroupDaoTests.word1}
        Dim testData As New TestData(cardsMock.Object, words, QueryLanguage.TargetLanguage)

        Dim controller As TestController = New TestController(testData, QueryLanguage.TargetLanguage, _db)

        controller.HasWords().Should.Be(False)
        controller.GetTestChecker().Should.BeNull()
        controller.Count().Should.Be(0)
    End Sub

    <Test>
    Public Sub Update_NoError_AdvancesToNextWordAndDoublesCardInterval()
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        cardsMock.Setup(Function(x) x.Skip(GroupDaoTests.word1, QueryLanguage.TargetLanguage)).Returns(False)
        cardsMock.Setup(Function(x) x.Skip(GroupDaoTests.word3, QueryLanguage.TargetLanguage)).Returns(False)
        Dim words As New List(Of WordEntry) From {GroupDaoTests.word1, GroupDaoTests.word3}
        Dim controller As TestController = CreateController(words, cardsMock, QueryLanguage.TargetLanguage)

        controller.Update(TestResult.NoError)

        controller.HasWords().Should.Be(True)
        controller.GetTestChecker().Question.Should.Be(GroupDaoTests.word3.Word)
        ReadCardColumn("Counter", 1).Should.Be(8)
        ReadCardColumn("TestInterval", 1).Should.Be(8)
    End Sub

    <Test>
    Public Sub Update_NoErrorOnLastWord_EndsTestWithNoChecker()
        Dim controller As TestController = CreateWord1Controller(QueryLanguage.TargetLanguage)

        controller.Update(TestResult.NoError)

        controller.HasWords().Should.Be(False)
        controller.GetTestChecker().Should.BeNull()
        controller.Count().Should.Be(0)
        ReadCardColumn("Counter", 1).Should.Be(8)
    End Sub

    <Test>
    Public Sub Update_NoErrorWithOriginalLanguage_UpdatesMainCounterOnlyPinningDirectionRouting()
        Dim controller As TestController = CreateWord1Controller(QueryLanguage.OriginalLanguage)

        controller.Update(TestResult.NoError)

        ReadCardColumn("CounterMain", 1).Should.Be(8)
        ReadCardColumn("TestIntervalMain", 1).Should.Be(8)
        ReadCardColumn("Counter", 1).Should.Be(4)
        ReadCardColumn("TestInterval", 1).Should.Be(4)
    End Sub

    <Test>
    Public Sub Update_Wrong_KeepsSameCheckerSetsRetestAndHalvesInterval()
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        cardsMock.Setup(Function(x) x.Skip(GroupDaoTests.word1, QueryLanguage.TargetLanguage)).Returns(False)
        Dim words As New List(Of WordEntry) From {GroupDaoTests.word1, GroupDaoTests.word3}
        Dim controller As TestController = CreateController(words, cardsMock, QueryLanguage.TargetLanguage)
        Dim checkerBeforeUpdate As Checker = controller.GetTestChecker()

        controller.Update(TestResult.Wrong)

        controller.GetTestChecker().Should.BeSameAs(checkerBeforeUpdate)
        controller.GetTestChecker().Retest.Should.Be(True)
        ReadCardColumn("Counter", 1).Should.Be(2)
        ReadCardColumn("TestInterval", 1).Should.Be(2)
    End Sub

    <TestCase(TestResult.Misspelled)>
    <TestCase(TestResult.OtherMeaning)>
    Public Sub Update_MisspelledOrOtherMeaning_SetsRetestAndLeavesCardsUnchanged(result As TestResult)
        Dim controller As TestController = CreateWord1Controller(QueryLanguage.TargetLanguage)

        controller.Update(result)

        controller.GetTestChecker().Retest.Should.Be(True)
        ReadCardColumn("Counter", 1).Should.Be(4)
        ReadCardColumn("TestInterval", 1).Should.Be(4)
    End Sub

    <TestCase(TestResult.NoError)>
    <TestCase(TestResult.Wrong)>
    Public Sub Update_AfterTestEnded_ThrowsInvalidOperationException(result As TestResult)
        Dim controller As TestController = CreateWord1Controller(QueryLanguage.TargetLanguage)
        controller.Update(TestResult.NoError) ' drains the only word and ends the test

        Assert.Throws(Of InvalidOperationException)(Sub() controller.Update(result))
    End Sub

    ''' <summary>
    ''' A word answered correctly can leave only skippable words behind; the test then ends cleanly.
    ''' </summary>
    <Test>
    Public Sub Update_NoErrorWithRemainingWordsSkippable_EndsTest()
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        cardsMock.Setup(Function(x) x.Skip(GroupDaoTests.word1, QueryLanguage.TargetLanguage)).Returns(False)
        cardsMock.Setup(Function(x) x.Skip(GroupDaoTests.word3, QueryLanguage.TargetLanguage)).Returns(True)
        Dim words As New List(Of WordEntry) From {GroupDaoTests.word1, GroupDaoTests.word3}
        Dim controller As TestController = CreateController(words, cardsMock, QueryLanguage.TargetLanguage)

        controller.Update(TestResult.NoError)

        controller.HasWords().Should.Be(False)
        controller.GetTestChecker().Should.BeNull()
        controller.Count().Should.Be(0)
        ReadCardColumn("Counter", 1).Should.Be(8)
    End Sub

    ''' <summary>
    ''' The cards are updated at most once per word: the first Wrong consumes firstTest,
    ''' so repeated Wrong answers for the very same word halve the interval only once.
    ''' </summary>
    <Test>
    Public Sub Update_WrongTwiceOnSameWord_HalvesIntervalOnlyOnce()
        Dim controller As TestController = CreateWord1Controller(QueryLanguage.TargetLanguage)

        controller.Update(TestResult.Wrong)
        ReadCardColumn("Counter", 1).Should.Be(2)

        controller.Update(TestResult.Wrong)
        ReadCardColumn("Counter", 1).Should.Be(2)

        controller.GetTestChecker().Retest.Should.Be(True)
    End Sub

    ''' <summary>
    ''' A correct answer on a retest after Wrong does not move the card again.
    ''' </summary>
    <Test>
    Public Sub Update_NoErrorAfterWrong_DoesNotUpdateCardAgain()
        Dim controller As TestController = CreateWord1Controller(QueryLanguage.TargetLanguage)

        controller.Update(TestResult.Wrong)
        controller.Update(TestResult.NoError)

        ReadCardColumn("Counter", 1).Should.Be(2)
        ReadCardColumn("TestInterval", 1).Should.Be(2)
        controller.HasWords().Should.Be(False)
    End Sub

    ''' <summary>
    ''' Misspelled does not consume firstTest, so the following correct answer still updates the card.
    ''' </summary>
    <Test>
    Public Sub Update_NoErrorAfterMisspelled_StillUpdatesCard()
        Dim controller As TestController = CreateWord1Controller(QueryLanguage.TargetLanguage)

        controller.Update(TestResult.Misspelled)
        controller.Update(TestResult.NoError)

        ReadCardColumn("Counter", 1).Should.Be(8)
        ReadCardColumn("TestInterval", 1).Should.Be(8)
        controller.HasWords().Should.Be(False)
    End Sub
End Class
