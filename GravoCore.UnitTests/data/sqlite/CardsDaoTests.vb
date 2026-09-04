Imports Microsoft.Data.Sqlite
Imports System.IO
Imports Gravo
Imports NUnit.Framework
Imports FluentAssertions
Imports GravoCore.UnitTests.GroupDaoTests

<TestFixture>
Public Class CardsDaoTests

    Private ReadOnly ResourceFile As String = "test-data-cards.s3db"
    Private _cardsDao As CardsDao
    Private _tempDb As String
    Private _db As IDataBaseOperation

    Private Shared ReadOnly Success As Func(Of Integer, Integer) = Function(x As Integer) x * 2
    Private Shared ReadOnly Failure As Func(Of Integer, Integer) = Function(x As Integer) CInt(x / 2)
    Private Shared ReadOnly FailureKeep As Func(Of Integer, Integer) = Function(x As Integer) x

    ReadOnly ExampleGroup As GroupEntry = New GroupEntry(123, "", "", "GroupTest-Example01")
    ReadOnly EmptyGroup As GroupEntry = New GroupEntry(123, "", "", "GroupTest-Example02")
    Private Shared ReadOnly testWord4NotExisting As MockTestWord = New MockTestWord(GroupDaoTests.word4, False, "")

    <SetUp>
    Public Sub Setup()
        _tempDb = Path.GetTempFileName
        File.Copy(DaoUtils.GetSqliteResource(ResourceFile), _tempDb, True)

        _db = New SQLiteDataBaseOperation()
        _db.Open(_tempDb)

        _cardsDao = New CardsDao(_db)
    End Sub

    <TearDown>
    Public Sub CleanUp()
        _db.Close()
        SqliteConnection.ClearAllPools()

        File.Delete(_tempDb)
    End Sub

    <Test>
    Public Sub UpdateSuccessTargetLanguage_GroupEntry_ResetsValues()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateSuccess(ExampleGroup, GroupDaoTests.testWord1, QueryLanguage.TargetLanguage))
        Test(updateAction, "GroupTest-Example01", "", 4, Success)
    End Sub

    <Test>
    Public Sub UpdateSuccessSourceLanguage_GroupEntry_ResetsValues()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateSuccess(ExampleGroup, GroupDaoTests.testWord1, QueryLanguage.OriginalLanguage))
        Test(updateAction, "GroupTest-Example01", "Main", 1, Success)
    End Sub

    <Test>
    Public Sub UpdateSuccessBoth_GroupEntry_ResetsValuesTargetLanguage()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateSuccess(ExampleGroup, GroupDaoTests.testWord1, QueryLanguage.Both))
        Test(updateAction, "GroupTest-Example01", "", 4, Success)
    End Sub

    <Test>
    Public Sub UpdateSuccessBoth_GroupEntry_ResetsValuesOriginalLanguage()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateSuccess(ExampleGroup, GroupDaoTests.testWord1, QueryLanguage.Both))
        Test(updateAction, "GroupTest-Example01", "Main", 1, Success)
    End Sub

    <Test>
    Public Sub UpdateFailureTargetLanguage_GroupEntry_ResetsValues()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateFailure(ExampleGroup, GroupDaoTests.testWord1, QueryLanguage.TargetLanguage))
        Test(updateAction, "GroupTest-Example01", "", 4, Failure)
    End Sub

    <Test>
    Public Sub UpdateFailureSourceLanguage_GroupEntry_DoesNotResetValues()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateFailure(ExampleGroup, GroupDaoTests.testWord1, QueryLanguage.OriginalLanguage))
        Test(updateAction, "GroupTest-Example01", "Main", 1, FailureKeep)
    End Sub

    <Test>
    Public Sub UpdateFailureBoth_GroupEntry_DoesNotResetValues()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateFailure(ExampleGroup, GroupDaoTests.testWord1, QueryLanguage.Both))
        Test(updateAction, "GroupTest-Example01", "", 4, Failure)
        Test(updateAction, "GroupTest-Example01", "Main", 1, FailureKeep)
    End Sub

    <Test>
    Public Sub SkipTargetLanguage1_GroupEntry_ReducesCounter()
        Dim skipPossible As Boolean = _cardsDao.Skip(ExampleGroup, GroupDaoTests.testWord1, QueryLanguage.TargetLanguage)
        Assert.IsTrue(skipPossible)
        AssertValue("GroupTest-Example01", "Counter", 1, 3)
        AssertValue("GroupTest-Example01", "TestInterval", 1, 4)
        AssertValue("GroupTest-Example01", "LastDate", 1, New Date(2019, 4, 19))
    End Sub

    <Test>
    Public Sub SkipOriginalLanguage_GroupEntry_DoesNotReduceBelow1()
        Dim skipPossible As Boolean = _cardsDao.Skip(ExampleGroup, GroupDaoTests.testWord1, QueryLanguage.OriginalLanguage)
        Assert.IsFalse(skipPossible)
        AssertValue("GroupTest-Example01", "CounterMain", 3, 1)
        AssertValue("GroupTest-Example01", "TestIntervalMain", 3, 1)
    End Sub

    <Test>
    Public Sub SkipBoth_GroupEntry_DoesNotReduceBelow1()
        Dim skipPossible As Boolean = _cardsDao.Skip(ExampleGroup, GroupDaoTests.testWord1, QueryLanguage.Both)
        Assert.IsFalse(skipPossible)
        AssertValue("GroupTest-Example01", "Counter", 3, 1)
        AssertValue("GroupTest-Example01", "TestInterval", 3, 1)
        AssertValue("GroupTest-Example01", "CounterMain", 3, 1)
        AssertValue("GroupTest-Example01", "TestIntervalMain", 3, 1)
    End Sub

    <Test>
    Public Sub Skip_GroupEntry_Throws()
        Assert.Throws(Of EntryNotFoundException)(Sub() _cardsDao.Skip(ExampleGroup, testWord4NotExisting, QueryLanguage.TargetLanguage))
    End Sub

    <Test>
    Public Sub UpdateSuccessTargetLanguage_WordEntry_ResetsValues()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateSuccess(GroupDaoTests.word1, QueryLanguage.TargetLanguage))
        Test(updateAction, "Cards", "", 4, Success)
    End Sub

    <Test>
    Public Sub UpdateSuccessSourceLanguage_WordEntry_ResetsValues()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateSuccess(GroupDaoTests.word1, QueryLanguage.OriginalLanguage))
        Test(updateAction, "Cards", "Main", 4, Success)
    End Sub

    <Test>
    Public Sub UpdateSuccessBoth_WordEntry_ResetsValuesTargetLanguage()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateSuccess(GroupDaoTests.word1, QueryLanguage.Both))
        Test(updateAction, "Cards", "", 4, Success)
    End Sub

    <Test>
    Public Sub UpdateSuccessBoth_WordEntry_ResetsValuesOriginalLanguage()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateSuccess(GroupDaoTests.word1, QueryLanguage.Both))
        Test(updateAction, "Cards", "Main", 4, Success)
    End Sub

    <Test>
    Public Sub UpdateFailureTargetLanguage_WordEntry_ResetsValues()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateFailure(GroupDaoTests.word1, QueryLanguage.TargetLanguage))
        Test(updateAction, "Cards", "", 4, Failure)
    End Sub

    <Test>
    Public Sub UpdateFailureSourceLanguage_WordEntry_ResetsValues()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateFailure(GroupDaoTests.word1, QueryLanguage.OriginalLanguage))
        Test(updateAction, "Cards", "Main", 4, Failure)
    End Sub

    <Test>
    Public Sub UpdateFailureBoth_WordEntry_ResetsValuesTargetLanguage()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateFailure(GroupDaoTests.word1, QueryLanguage.Both))
        Test(updateAction, "Cards", "", 4, Failure)
    End Sub

    <Test>
    Public Sub UpdateFailureBoth_WordEntry_ResetsValuesOriginalLanguage()
        Dim updateAction As Action = (Sub() _cardsDao.UpdateFailure(GroupDaoTests.word1, QueryLanguage.Both))
        Test(updateAction, "Cards", "Main", 4, Failure)
    End Sub

    <Test>
    Public Sub SkipTargetLanguage_WordEntry_ReducesCounter()
        Dim skipPossible As Boolean = _cardsDao.Skip(GroupDaoTests.word1, QueryLanguage.TargetLanguage)
        Assert.IsTrue(skipPossible)
        AssertValue("Cards", "Counter", 1, 3)
        AssertValue("GroupTest-Example01", "TestInterval", 1, 4)
    End Sub

    <Test>
    Public Sub SkipOriginalLanguage_WordEntry_ReducesCounter()
        Dim skipPossible As Boolean = _cardsDao.Skip(GroupDaoTests.word1, QueryLanguage.OriginalLanguage)
        Assert.IsTrue(skipPossible)
        AssertValue("Cards", "CounterMain", 1, 3)
        AssertValue("GroupTest-Example01", "TestIntervalMain", 1, 1)
    End Sub

    <Test>
    Public Sub SkipBoth_WordEntry_ReducesCounter()
        Dim skipPossible As Boolean = _cardsDao.Skip(GroupDaoTests.word1, QueryLanguage.Both)
        Assert.IsTrue(skipPossible)
        AssertValue("Cards", "Counter", 1, 3)
        AssertValue("GroupTest-Example01", "TestInterval", 1, 4)
        AssertValue("Cards", "CounterMain", 1, 3)
        AssertValue("GroupTest-Example01", "TestIntervalMain", 1, 1)
    End Sub

    <Test>
    Public Sub SkipTargetLanguage_WordEntry_DoesNotReduceBelow1()
        Dim skipPossible As Boolean = _cardsDao.Skip(GroupDaoTests.word3, QueryLanguage.TargetLanguage)
        Assert.IsFalse(skipPossible)
        AssertValue("Cards", "Counter", 3, 1)
        AssertValue("GroupTest-Example01", "TestInterval", 1, 4)
    End Sub

    <Test>
    Public Sub SkipOriginalLanguage_WordEntry_DoesNotReduceBelow1()
        Dim skipPossible As Boolean = _cardsDao.Skip(GroupDaoTests.word3, QueryLanguage.OriginalLanguage)
        Assert.IsFalse(skipPossible)
        AssertValue("Cards", "CounterMain", 3, 1)
        AssertValue("GroupTest-Example01", "TestIntervalMain", 1, 1)
    End Sub

    <Test>
    Public Sub SkipBoth_WordEntry_DoesNotReduceBelow1()
        Dim skipPossible As Boolean = _cardsDao.Skip(GroupDaoTests.word3, QueryLanguage.Both)
        Assert.IsFalse(skipPossible)
        AssertValue("Cards", "Counter", 3, 1)
        AssertValue("GroupTest-Example01", "TestInterval", 1, 4)
        AssertValue("Cards", "CounterMain", 3, 1)
        AssertValue("GroupTest-Example01", "TestIntervalMain", 1, 1)
    End Sub

    <Test>
    Public Sub Skip_WordEntry_Throws()
        Assert.Throws(Of EntryNotFoundException)(Sub() _cardsDao.Skip(GroupDaoTests.word4, QueryLanguage.TargetLanguage))
    End Sub

    <Test>
    Public Sub Load_ExistingIndex_ReturnsCard()
        Dim card As Card = _cardsDao.Load(2)

        card.TestInterval.Should.Be(4)
        card.Counter.Should.Be(2)
        card.LastDate.Should.Be(New Date(2019, 9, 7))
        card.TestIntervalMain.Should.Be(4)
        card.CounterMain.Should.Be(2)
    End Sub

    <Test>
    Public Sub Load_MissingIndex_Throws()
        Assert.Throws(Of EntryNotFoundException)(Sub() _cardsDao.Load(999))
    End Sub

    ''' <summary>
    ''' Asserts working round-trip of writing dates through the wrapper (e.g. '12/24/2021), and reading
    ''' back with culture invariant reader.
    ''' </summary>
    <Test>
    Public Sub Save_ThenLoad_RoundTripsCard()
        Dim saved As New Card(8, 3, New Date(2021, 12, 24), 5, 2)

        _cardsDao.Save(saved, 3)
        Dim loaded As Card = _cardsDao.Load(3)

        loaded.TestInterval.Should.Be(8)
        loaded.Counter.Should.Be(3)
        loaded.LastDate.Should.Be(New Date(2021, 12, 24))
        loaded.TestIntervalMain.Should.Be(5)
        loaded.CounterMain.Should.Be(2)
    End Sub

    Private Sub Test(updateAction As Action, table As String, columnSuffix As String, baseInterval As Integer, updateInterval As Func(Of Integer, Integer))
        Dim executionData = ExecuteOnSameDay(updateAction)
        Dim expectedInterval As Integer = If(executionData.Item2, updateInterval(baseInterval), updateInterval(updateInterval(baseInterval)))
        AssertValue(table, "Counter" & columnSuffix, 1, expectedInterval)
        AssertValue(table, "TestInterval" & columnSuffix, 1, expectedInterval)
        AssertValue(table, "LastDate", 1, executionData.Item1)
    End Sub

    ''' <summary>
    ''' Ensures that action is performed on a certain day. If it could not be ensured on which day the action
    ''' was performed (because before executing it was another day than after the execution), it is executed
    ''' again.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <returns>Tuple of day of execution and weather first execution was on same day.</returns>
    Private Shared Function ExecuteOnSameDay(action As Action) As Tuple(Of Date, Boolean)
        Dim startDate = Date.Now.Date
        action()
        Dim endDate As Date = Date.Now.Date
        Console.WriteLine("startDate = " & startDate & " - endDate = " & endDate)
        If startDate.Equals(endDate) Then
            Console.WriteLine("Returning")
            Return Tuple.Create(endDate, True)
        End If
        action()
        Return Tuple.Create(endDate, False)
    End Function

    Private Sub AssertValue(table As String, column As String, row As Integer, expected As Integer)
        Dim command = "SELECT " & column & " FROM [" & table & "] WHERE [Index] = ?"
        _db.ExecuteReader(command, CStr(row))
        _db.DBCursor.Read()
        Dim value As Integer = _db.SecureGetInt32(0)
        _db.DBCursor.Close()
        value.Should.Be(expected)
    End Sub

    Private Sub AssertValue(table As String, column As String, row As Integer, expected As Date)
        Dim command = "SELECT " & column & " FROM [" & table & "] WHERE [Index] = ?"
        _db.ExecuteReader(command, CStr(row))
        _db.DBCursor.Read()
        Dim value As DateTime = _db.SecureGetDateTime(0)
        _db.DBCursor.Close()
        value.Should.Be(expected)
    End Sub
End Class
