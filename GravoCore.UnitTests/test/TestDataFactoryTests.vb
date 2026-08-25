Imports Gravo
Imports Moq
Imports NUnit.Framework
Imports FluentAssertions

<TestFixture>
Public Class TestDataFactoryTests
    <Test>
    Public Sub Create_LanguageOverloadWithPhrases_DefaultsMainLanguageToGerman()
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

    <Test>
    Public Sub Create_LanguageOverloadWithExplicitMainLanguage_QueriesThatMainLanguage()
        Dim dictionaryMock As New Mock(Of IDictionaryDao)(MockBehavior.Strict)
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        Dim words As ICollection(Of WordEntry) = New List(Of WordEntry) From {
            New WordEntry("word1", "", "", WordType.Verb, "m1", "", False)
        }
        dictionaryMock.Setup(Function(x) x.GetWords("english", "french")).Returns(words)

        Dim data As TestData = TestDataFactory.Create(dictionaryMock.Object, cardsMock.Object, "english", True,
                                                      QueryLanguage.OriginalLanguage, "french")

        data.Count().Should.Be(1)
    End Sub

    ''' <summary>
    ''' When testPhrases is set to False, set phrases only are excluded. Example entries stay in.
    ''' So the excluded member is identified exactly.
    ''' </summary>
    <TestCase(WordType.SetPhrase, 0)>
    <TestCase(WordType.Example, 1)>
    <TestCase(WordType.Verb, 1)>
    Public Sub Create_LanguageOverloadWithoutPhrases_FiltersSetPhrasesOnly(wordType As WordType, expectedCount As Integer)
        Dim dictionaryMock As New Mock(Of IDictionaryDao)(MockBehavior.Strict)
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        Dim words As ICollection(Of WordEntry) = New List(Of WordEntry) From {
            New WordEntry("word1", "", "", wordType, "m1", "", False)
        }
        dictionaryMock.Setup(Function(x) x.GetWords("english", "german")).Returns(words)

        Dim data As TestData = TestDataFactory.Create(dictionaryMock.Object, cardsMock.Object, "english", False, QueryLanguage.OriginalLanguage)

        data.Count().Should.Be(expectedCount)
    End Sub

    Private Shared Function CreateTestWord(wordType As WordType, marked As Boolean) As TestWord
        Return New TestWord(New WordEntry("word" & wordType.ToString(), "", "", wordType, "m" & wordType.ToString(), "", False), marked, "")
    End Function

    Private Shared Function CreateGroupDto(group As GroupEntry, ParamArray entries As TestWord()) As GroupDto
        Return New GroupDto(group, New List(Of TestWord)(entries))
    End Function

    ''' <summary>
    ''' Tests filtering of marked words. Two cases are supported:
    ''' testMarked:=True restricts the test to marked words
    ''' testMarked:=False takes all words.
    ''' The test uses single-word cases to evaluate the filter exactly.
    ''' </summary>
    <TestCase(True, True, 1)>
    <TestCase(False, True, 0)>
    <TestCase(True, False, 1)>
    <TestCase(False, False, 1)>
    Public Sub Create_GroupOverloadWithMarkedFlag_TestsOnlyMarkedWords(marked As Boolean, testMarked As Boolean, expectedCount As Integer)
        Dim groupDaoMock As New Mock(Of IGroupDao)(MockBehavior.Strict)
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        Dim group As GroupEntry = New GroupEntry(1, "group", "sub", "table")
        groupDaoMock.Setup(Function(x) x.Load(group)).Returns(CreateGroupDto(group, CreateTestWord(WordType.Verb, marked)))

        Dim data As TestData = TestDataFactory.Create(groupDaoMock.Object, cardsMock.Object, group, True, testMarked, QueryLanguage.OriginalLanguage)

        data.Count().Should.Be(expectedCount)
    End Sub

    ''' <summary>
    ''' testPhrases:=False excludes set phrases only.
    ''' </summary>
    <TestCase(WordType.SetPhrase, False, 0)>
    <TestCase(WordType.Example, False, 1)>
    <TestCase(WordType.Verb, False, 1)>
    <TestCase(WordType.SetPhrase, True, 1)>
    Public Sub Create_GroupOverloadWithPhrasesFlag_FiltersSetPhrasesOnly(wordType As WordType, testPhrases As Boolean, expectedCount As Integer)
        Dim groupDaoMock As New Mock(Of IGroupDao)(MockBehavior.Strict)
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        Dim group As GroupEntry = New GroupEntry(1, "group", "sub", "table")
        groupDaoMock.Setup(Function(x) x.Load(group)).Returns(CreateGroupDto(group, CreateTestWord(wordType, False)))

        Dim data As TestData = TestDataFactory.Create(groupDaoMock.Object, cardsMock.Object, group, testPhrases, False, QueryLanguage.OriginalLanguage)

        data.Count().Should.Be(expectedCount)
    End Sub

    <Test>
    Public Sub Create_GroupOverloadWithBothFilters_AppliesBoth()
        Dim groupDaoMock As New Mock(Of IGroupDao)(MockBehavior.Strict)
        Dim cardsMock As New Mock(Of ICardsDao)(MockBehavior.Strict)
        Dim group As GroupEntry = New GroupEntry(1, "group", "sub", "table")
        groupDaoMock.Setup(Function(x) x.Load(group)).Returns(CreateGroupDto(group,
            CreateTestWord(WordType.Verb, True),
            CreateTestWord(WordType.Verb, False),
            CreateTestWord(WordType.SetPhrase, True),
            CreateTestWord(WordType.SetPhrase, False)))

        Dim data As TestData = TestDataFactory.Create(groupDaoMock.Object, cardsMock.Object, group, False, True, QueryLanguage.OriginalLanguage)

        data.Count().Should.Be(1)
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
