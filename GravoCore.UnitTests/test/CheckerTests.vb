Imports AwesomeAssertions
Imports Gravo
Imports Moq
Imports NUnit.Framework

<TestFixture>
Public Class CheckerTests
    Private ReadOnly current As WordEntry = New WordEntry("word1", "pre", "post", WordType.Verb, "meaning1", "info1", False)
    Private ReadOnly mainEntry As MainEntry = New MainEntry("word1", "lang", "mainLang")
    Private dictionaryDaoMock As Mock(Of IDictionaryDao)

    Private ReadOnly japaneseCurrent As WordEntry = New WordEntry("食べる", "", "", WordType.Verb, "essen", "", False, "たべる")
    Private ReadOnly japaneseMainEntry As MainEntry = New MainEntry("食べる", "japanese", "german")
    Private ReadOnly japaneseKatakanaCurrent As WordEntry = New WordEntry("ケーキ", "", "", WordType.Substantive, "Kuchen", "", False, "")
    Private ReadOnly nonJapaneseWithReading As WordEntry = New WordEntry("word", "", "", WordType.Verb, "meaning", "", False, "reading")
    Private ReadOnly nonJapaneseMainEntry As MainEntry = New MainEntry("word", "lang", "mainLang")

    <SetUp>
    Public Sub Setup()
        dictionaryDaoMock = New Mock(Of IDictionaryDao)(MockBehavior.Strict)
    End Sub

    Private Function CreateFixture(queryLanguage As QueryLanguage) As Checker
        dictionaryDaoMock.Setup(Function(x) x.GetMainEntry(current)).Returns(mainEntry)
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
    Public Sub Evaluate_ActiveCaseDifferingSynonym_ReturnsOtherMeaning()
        SetUpSynonymWord("SYNONYM")
        Dim checker As Checker = CreateFixture(QueryLanguage.OriginalLanguage)

        Dim result As TestResult = checker.Evaluate("synonym")

        result.Should.Be(TestResult.OtherMeaning)
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
    Public Sub Evaluate_PassiveCaseDifferingSynonym_ReturnsOtherMeaning()
        SetUpSynonymMeaning("SYNONYMMEANING")
        Dim checker As Checker = CreateFixture(QueryLanguage.TargetLanguage)

        Dim result As TestResult = checker.Evaluate("synonymmeaning")

        result.Should.Be(TestResult.OtherMeaning)
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

    Private Function CreateJapaneseFixture(queryLanguage As QueryLanguage) As Checker
        dictionaryDaoMock.Setup(Function(x) x.GetMainEntry(japaneseCurrent)).Returns(japaneseMainEntry)
        dictionaryDaoMock.Setup(Function(x) x.GetWordsWithMeaning(japaneseCurrent.Meaning, japaneseMainEntry.Language, japaneseMainEntry.MainLanguage)).
            Returns(New List(Of WordEntry) From {japaneseCurrent})
        dictionaryDaoMock.Setup(Function(x) x.GetWordsAndSubWords(japaneseMainEntry)).
            Returns(New List(Of WordEntry) From {japaneseCurrent})
        Return New Checker(dictionaryDaoMock.Object, queryLanguage, japaneseCurrent)
    End Function

    Private Function CreateKatakanaFixture(queryLanguage As QueryLanguage) As Checker
        dictionaryDaoMock.Setup(Function(x) x.GetMainEntry(japaneseKatakanaCurrent)).Returns(japaneseMainEntry)
        dictionaryDaoMock.Setup(Function(x) x.GetWordsWithMeaning(japaneseKatakanaCurrent.Meaning, japaneseMainEntry.Language, japaneseMainEntry.MainLanguage)).
            Returns(New List(Of WordEntry) From {japaneseKatakanaCurrent})
        dictionaryDaoMock.Setup(Function(x) x.GetWordsAndSubWords(japaneseMainEntry)).
            Returns(New List(Of WordEntry) From {japaneseKatakanaCurrent})
        Return New Checker(dictionaryDaoMock.Object, queryLanguage, japaneseKatakanaCurrent)
    End Function

    Private Function CreateNonJapaneseWithReadingFixture(queryLanguage As QueryLanguage) As Checker
        dictionaryDaoMock.Setup(Function(x) x.GetMainEntry(nonJapaneseWithReading)).Returns(nonJapaneseMainEntry)
        dictionaryDaoMock.Setup(Function(x) x.GetWordsWithMeaning(nonJapaneseWithReading.Meaning, nonJapaneseMainEntry.Language, nonJapaneseMainEntry.MainLanguage)).
            Returns(New List(Of WordEntry) From {nonJapaneseWithReading})
        dictionaryDaoMock.Setup(Function(x) x.GetWordsAndSubWords(nonJapaneseMainEntry)).
            Returns(New List(Of WordEntry) From {nonJapaneseWithReading})
        Return New Checker(dictionaryDaoMock.Object, queryLanguage, nonJapaneseWithReading)
    End Function

    <Test>
    Public Sub Evaluate_JapaneseActive_AnyInput_ReturnsNoError()
        Dim checker As Checker = CreateJapaneseFixture(QueryLanguage.OriginalLanguage)

        ' Romaji, any case
        checker.Evaluate("taberu").Should().Be(TestResult.NoError)
        checker.Evaluate("TABERU").Should().Be(TestResult.NoError)
        ' The original Japanese word in kanji
        checker.Evaluate("食べる").Should().Be(TestResult.NoError)
        ' Hiragana
        checker.Evaluate("たべる").Should().Be(TestResult.NoError)
        ' Katakana
        checker.Evaluate("タベル").Should().Be(TestResult.NoError)
    End Sub

    <Test>
    Public Sub Evaluate_JapaneseKatakanaActive_AnyInput_ReturnsNoError()
        Dim checker As Checker = CreateKatakanaFixture(QueryLanguage.OriginalLanguage)

        ' Romaji, any case
        checker.Evaluate("keeki").Should().Be(TestResult.NoError)
        checker.Evaluate("KeEki").Should().Be(TestResult.NoError)
        ' Romaji with macron
        checker.Evaluate("kēki").Should().Be(TestResult.NoError)
        ' Hiragana
        checker.Evaluate("けえき").Should().Be(TestResult.NoError)
        ' Katakana
        checker.Evaluate("ケーキ").Should().Be(TestResult.NoError)
    End Sub

    <Test>
    Public Sub Evaluate_JapaneseActive_WrongInput_ReturnsWrong()
        Dim checker As Checker = CreateJapaneseFixture(QueryLanguage.OriginalLanguage)

        checker.Evaluate("nomatch").Should().Be(TestResult.Wrong)
    End Sub

    <Test>
    Public Sub Evaluate_JapanesePassive_QuestionAndAnswer()
        Dim checker As Checker = CreateJapaneseFixture(QueryLanguage.TargetLanguage)

        checker.Question.Should().Be("食べる")
        checker.Answer.Should().Be("essen")
        checker.Info.Should().Be("たべる")
    End Sub

    <Test>
    Public Sub Evaluate_JapaneseActive_QuestionAndAnswer()
        Dim checker As Checker = CreateJapaneseFixture(QueryLanguage.OriginalLanguage)

        checker.Question.Should().Be("essen")
        checker.Answer.Should().Be("食べる（たべる）")
        checker.Info.Should().Be("")
    End Sub

    <Test>
    Public Sub Evaluate_NonJapaneseWithReading_Active_StillChecksWordOnly()
        Dim checker As Checker = CreateNonJapaneseWithReadingFixture(QueryLanguage.OriginalLanguage)

        checker.Evaluate("word").Should().Be(TestResult.NoError)
        checker.Evaluate("reading").Should().Be(TestResult.Wrong)
    End Sub
End Class
