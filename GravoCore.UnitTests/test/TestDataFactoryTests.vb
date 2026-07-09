Imports Gravo
Imports Moq
Imports NUnit.Framework
Imports FluentAssertions

<TestFixture>
Public Class TestDataFactoryTests
    <Test>
    Public Sub Create_AllEntriesOverload_ThrowsNotImplementedException()
        Dim dictionaryMock As New Mock(Of IDictionaryDao)(MockBehavior.Strict)
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)

        Assert.Throws(Of NotImplementedException)(Sub() TestDataFactory.Create(dictionaryMock.Object, cardsMock.Object, True, QueryLanguage.OriginalLanguage))
    End Sub

    <Test>
    Public Sub Create_LanguageOverloadWithPhrases_WrapsWordsAndPinsHardcodedGerman()
        Dim dictionaryMock As New Mock(Of IDictionaryDao)(MockBehavior.Strict)
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        Dim words As ICollection(Of WordEntry) = New List(Of WordEntry) From {
            New WordEntry("word1", "", "", WordType.Verb, "m1", "", False),
            New WordEntry("word2", "", "", WordType.SetPhrase, "m2", "", False)
        }
        dictionaryMock.Setup(Function(x) x.GetWords("english", "german")).Returns(words)

        Dim data As TestData = TestDataFactory.Create(dictionaryMock.Object, cardsMock.Object, "english", True, QueryLanguage.OriginalLanguage)

        data.Count().Should.Be(2)
    End Sub

    ''' <summary>
    ''' Testing a failure in the current implementation of TestDataFactory.vb
    ''' which assigns the IEnumerable(Of WordEntry) returned by words.Where(...) to a variable
    ''' declared As ICollection(Of WordEntry). Throwing an InvalidCastException.
    ''' </summary>
    <Test>
    Public Sub Create_LanguageOverloadWithoutPhrases_ThrowsInvalidCastException()
        Dim dictionaryMock As New Mock(Of IDictionaryDao)(MockBehavior.Strict)
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        Dim words As ICollection(Of WordEntry) = New List(Of WordEntry) From {
            New WordEntry("word1", "", "", WordType.Verb, "m1", "", False)
        }
        dictionaryMock.Setup(Function(x) x.GetWords("english", "german")).Returns(words)

        Assert.Throws(Of InvalidCastException)(Sub() TestDataFactory.Create(dictionaryMock.Object, cardsMock.Object, "english", False, QueryLanguage.OriginalLanguage))
    End Sub

    Private Function CreateGroupDto(group As GroupEntry) As GroupDto
        Dim phraseWord As TestWord = New TestWord(New WordEntry("phraseWord", "", "", WordType.SetPhrase, "meaningPhrase", "", False), False, "")
        Dim markedWord As TestWord = New TestWord(New WordEntry("markedWord", "", "", WordType.Verb, "meaningMarked", "", False), True, "")
        Return New GroupDto(group, New List(Of TestWord) From {phraseWord, markedWord})
    End Function

    ''' <summary>
    ''' Tests the not implemented flags in the overloaded TestDataFactory.Create function.
    ''' Any flag combination returns every entry from the group.
    ''' </summary>
    <TestCase(True, True)>
    <TestCase(True, False)>
    <TestCase(False, True)>
    <TestCase(False, False)>
    Public Sub Create_GroupOverloadWithAnyFlagCombination_ReturnsAllEntries(testPhrases As Boolean, testMarked As Boolean)
        Dim groupDaoMock As New Mock(Of IGroupDao)(MockBehavior.Strict)
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        Dim group As GroupEntry = New GroupEntry(1, "group", "sub", "table")
        groupDaoMock.Setup(Function(x) x.Load(group)).Returns(CreateGroupDto(group))

        Dim data As TestData = TestDataFactory.Create(groupDaoMock.Object, cardsMock.Object, group, testPhrases, testMarked, QueryLanguage.OriginalLanguage)

        data.Count().Should.Be(2)
    End Sub

    <Test>
    Public Sub Create_GroupOverloadWithEmptyGroup_ReturnsEmptyTestData()
        Dim groupDaoMock As New Mock(Of IGroupDao)(MockBehavior.Strict)
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        Dim group As GroupEntry = New GroupEntry(2, "group", "empty", "table2")
        Dim emptyGroupDto As GroupDto = New GroupDto(group, New List(Of TestWord))
        groupDaoMock.Setup(Function(x) x.Load(group)).Returns(emptyGroupDto)

        Dim data As TestData = TestDataFactory.Create(groupDaoMock.Object, cardsMock.Object, group, True, True, QueryLanguage.OriginalLanguage)

        data.IsEmpty().Should.Be(True)
    End Sub
End Class
