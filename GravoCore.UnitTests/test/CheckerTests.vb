Imports Gravo
Imports Moq
Imports NUnit.Framework
Imports FluentAssertions

<TestFixture>
Public Class CheckerTests
    Private ReadOnly current As WordEntry = New WordEntry("word1", "pre", "post", WordType.Verb, "meaning1", "info1", False)
    Private ReadOnly mainEntry As MainEntry = New MainEntry("word1", "lang", "mainLang")
    Private dictionaryDaoMock As Mock(Of IDictionaryDao)

    <SetUp>
    Public Sub Setup()
        dictionaryDaoMock = New Mock(Of IDictionaryDao)(MockBehavior.Strict)
    End Sub

    Private Function CreateFixture(queryLanguage As QueryLanguage) As Checker
        Return New Checker(dictionaryDaoMock.Object, queryLanguage, current)
    End Function

    Private Sub SetUpSynonymWord(wordMeaning as String)
        dictionaryDaoMock.Setup(Function(x) x.GetMainEntry(current)).Returns(mainEntry)
        dictionaryDaoMock.Setup(Function(x) x.GetWordsWithMeaning(current.Meaning, mainEntry.Language, mainEntry.MainLanguage)).
            Returns(New List(Of WordEntry) From {New WordEntry(wordMeaning, "", "", WordType.Verb, "meaning1", "", False)})
    End Sub

    Private Sub SetUpSynonymMeaning(subWordMeaning as String)
        dictionaryDaoMock.Setup(Function(x) x.GetMainEntry(current)).Returns(mainEntry)
        dictionaryDaoMock.Setup(Function(x) x.GetWordsAndSubWords(mainEntry)).
            Returns(New List(Of WordEntry) From {New WordEntry("word2", "", "", WordType.Verb, subWordMeaning, "", False)})
    End Sub

    <Test>
    Public Sub Evaluate_ActiveExactMatch_ReturnsNoError()
        Dim checker As Checker = CreateFixture(QueryLanguage.OriginalLanguage)

        Dim result As TestResult = checker.Evaluate("word1")

        result.Should.Be(TestResult.NoError)
    End Sub

    <Test>
    Public Sub Evaluate_ActiveCaseOnlyMismatch_ReturnsMisspelled()
        Dim checker As Checker = CreateFixture(QueryLanguage.OriginalLanguage)

        Dim result As TestResult = checker.Evaluate("WORD1")

        result.Should.Be(TestResult.Misspelled)
    End Sub

    <Test>
    Public Sub Evaluate_ActiveSynonymMatch_ReturnsOtherMeaning()
        SetUpSynonymWord("synonym")
        Dim checker As Checker = CreateFixture(QueryLanguage.OriginalLanguage)

        Dim result As TestResult = checker.Evaluate("synonym")

        result.Should.Be(TestResult.OtherMeaning)
    End Sub

    <Test>
    Public Sub Evaluate_ActiveNoMatch_ReturnsWrong()
        SetUpSynonymWord("otherword")
        Dim checker As Checker = CreateFixture(QueryLanguage.OriginalLanguage)

        Dim result As TestResult = checker.Evaluate("nomatch")

        result.Should.Be(TestResult.Wrong)
    End Sub

    <Test>
    Public Sub Evaluate_ActiveCaseDifferingSynonym_ReturnsWrong()
        SetUpSynonymWord("SYNONYM")
        Dim checker As Checker = CreateFixture(QueryLanguage.OriginalLanguage)

        Dim result As TestResult = checker.Evaluate("synonym")

        result.Should.Be(TestResult.Wrong)
    End Sub

    <Test>
    Public Sub Evaluate_PassiveExactMatch_ReturnsNoError()
        Dim checker As Checker = CreateFixture(QueryLanguage.TargetLanguage)

        Dim result As TestResult = checker.Evaluate("meaning1")

        result.Should.Be(TestResult.NoError)
    End Sub

    <Test>
    Public Sub Evaluate_PassiveCaseOnlyMismatch_ReturnsMisspelled()
        Dim checker As Checker = CreateFixture(QueryLanguage.TargetLanguage)

        Dim result As TestResult = checker.Evaluate("MEANING1")

        result.Should.Be(TestResult.Misspelled)
    End Sub

    <Test>
    Public Sub Evaluate_PassiveSynonymMatch_ReturnsOtherMeaning()
        SetUpSynonymMeaning("synonymMeaning")
        Dim checker As Checker = CreateFixture(QueryLanguage.TargetLanguage)

        Dim result As TestResult = checker.Evaluate("synonymMeaning")

        result.Should.Be(TestResult.OtherMeaning)
    End Sub

    <Test>
    Public Sub Evaluate_PassiveNoMatch_ReturnsWrong()
        SetUpSynonymMeaning("otherMeaning")
        Dim checker As Checker = CreateFixture(QueryLanguage.TargetLanguage)

        Dim result As TestResult = checker.Evaluate("nomatch")

        result.Should.Be(TestResult.Wrong)
    End Sub

    <Test>
    Public Sub Evaluate_PassiveCaseDifferingSynonym_ReturnsWrong()
        SetUpSynonymMeaning("SYNONYMMEANING")
        Dim checker As Checker = CreateFixture(QueryLanguage.TargetLanguage)

        Dim result As TestResult = checker.Evaluate("synonymmeaning")

        result.Should.Be(TestResult.Wrong)
    End Sub

    <Test>
    Public Sub Evaluate_Both_ThrowsArgumentException()
        Dim checker As Checker = CreateFixture(QueryLanguage.Both)

        Assert.Throws(Of ArgumentException)(Sub() checker.Evaluate("anything"))
    End Sub

    <Test>
    Public Sub Question_Both_ThrowsArgumentException()
        Dim checker As Checker = CreateFixture(QueryLanguage.Both)

        Assert.Throws(Of ArgumentException)(Sub()
                                                 Dim question As String = checker.Question
                                             End Sub)
    End Sub

    <Test>
    Public Sub Answer_Both_ThrowsArgumentException()
        Dim checker As Checker = CreateFixture(QueryLanguage.Both)

        Assert.Throws(Of ArgumentException)(Sub()
                                                 Dim answer As String = checker.Answer
                                             End Sub)
    End Sub

    <TestCase(QueryLanguage.OriginalLanguage, "meaning1")>
    <TestCase(QueryLanguage.TargetLanguage, "word1")>
    Public Sub Question_ByQueryLanguage_ReturnsExpectedSide(queryLanguage As QueryLanguage, expected As String)
        Dim checker As Checker = CreateFixture(queryLanguage)

        checker.Question.Should.Be(expected)
    End Sub

    <TestCase(QueryLanguage.OriginalLanguage, "word1")>
    <TestCase(QueryLanguage.TargetLanguage, "meaning1")>
    Public Sub Answer_ByQueryLanguage_ReturnsExpectedSide(queryLanguage As QueryLanguage, expected As String)
        Dim checker As Checker = CreateFixture(queryLanguage)

        checker.Answer.Should.Be(expected)
    End Sub

    <Test>
    Public Sub Info_Returns_AdditionalTargetLangInfo()
        Dim checker As Checker = CreateFixture(QueryLanguage.OriginalLanguage)

        checker.Info.Should.Be("info1")
    End Sub

    <Test>
    Public Sub Retest_Initially_IsFalse()
        Dim checker As Checker = CreateFixture(QueryLanguage.OriginalLanguage)

        checker.Retest.Should.Be(False)
    End Sub
End Class
