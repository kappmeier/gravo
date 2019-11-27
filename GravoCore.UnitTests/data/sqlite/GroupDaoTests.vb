Imports System.Data.SQLite
Imports System.IO
Imports Gravo
Imports NUnit.Framework
Imports FluentAssertions

<TestFixture>
Public Class GroupDaoTests
    Private ReadOnly ResourceFile = "test-data-groups.s3db"
    Private _groupDao As GroupDao
    Private _tempDb As String
    Private _db As IDataBaseOperation

    Class MockTestWord
        Inherits TestWord

        Protected Friend Sub New(wordEntry As WordEntry, marked As Boolean, example As String)
            MyBase.New(wordEntry, marked, example)
        End Sub
    End Class

    Class MockWordEntry
        Inherits WordEntry

        Protected Friend Sub New(index As Integer, word As String, pre As String, post As String, wordType As WordType, meaning As String, additionalTargetLangInfo As String, irregular As Boolean)
            MyBase.New(index, word, pre, post, wordType, meaning, additionalTargetLangInfo, irregular)
        End Sub
    End Class
    Private ReadOnly word1 = New MockWordEntry(1, "word1", "pre", "post", WordType.Verb, "m", "l", False)
    Private ReadOnly testWord1 As TestWord = New MockTestWord(word1, True, "")
    Private ReadOnly testWord2 = New MockTestWord(New MockWordEntry(2, "word2", "", "", WordType.Verb, "", "", False), True, "An example.")
    Private ReadOnly testWord29 = New MockTestWord(New MockWordEntry(29, "wordx", "", "", WordType.Adjective, "", "", True), False, "")

    Private ReadOnly word3 = New MockWordEntry(3, "some word", "", "", WordType.Verb, "with a meaning", "", True)
    Private ReadOnly word4 = New MockWordEntry(4, "word4", "", "", WordType.Adjective, "test", "info", False)

    Dim exampleGroup = New GroupEntry(123, "Test-Example", "Subgroup1", "GroupTest-Example01")
    Dim emptyGroup = New GroupEntry(123, "Test-Example", "Empty subgroup", "GroupTest-Example02")
    Dim multiLanguageGroup = New GroupEntry(123, "Test-Example", "Multiple languages", "GroupTest-MultipleLanguages01")

    <SetUp>
    Public Sub Setup()
        _tempDb = Path.GetTempFileName
        File.Copy(DaoUtils.GetSqliteResource(ResourceFile), _tempDb, True)

        _db = New SQLiteDataBaseOperation()
        _db.Open(_tempDb)

        _groupDao = New GroupDao(_db)
    End Sub

    <TearDown>
        Public Sub CleanUp()
            _db.Close()
            SQLiteConnection.ClearAllPools()

            File.Delete(_tempDb)
        End Sub

    <Test>
    Public Sub LoadGroup_Existing_CreatesDTO()
        Dim group As GroupDto = _groupDao.Load(exampleGroup)

        Dim expectedResult = New List(Of TestWord) From {testWord1, testWord2, testWord29}

        group.Entries.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub LoadGroup_WhenEmpty_CreatesEmptyDTO()
        Dim group As GroupDto = _groupDao.Load(emptyGroup)

        group.Entries.Should.BeEmpty()
    End Sub

    <Test>
    Public Sub LoadSingle_Existing_Loads()
        Dim testEntry As TestWord = _groupDao.Load(exampleGroup, word1)

        testEntry.Should.Be(testWord1)
    End Sub

    <Test>
    Public Sub LoadSingle_WhenNotExisting_Fails()
        Assert.Throws(Of EntryNotFoundException)(Sub() _groupDao.Load(exampleGroup, word3))
    End Sub

    <Test>
    Public Sub AddTestEntry_NewWord_Added()
        _groupDao.Add(exampleGroup, word3, False, "some text")
        _groupDao.Add(exampleGroup, word4, True, "")

        Dim expectedResult = New List(Of TestWord) From {testWord1, testWord2, testWord29,
                New MockTestWord(word3, False, "some text"),
                New MockTestWord(word4, True, "")
            }

        _groupDao.Load(exampleGroup).Entries.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
        Public Sub AddTestEntry_Existing_ThrowsException()
        Assert.Throws(Of EntryExistsException)(Sub() _groupDao.Add(exampleGroup, word1, False, ""))
    End Sub

    <Test>
    Public Sub GetWords_Returns_Words()
        Dim group As GroupDto = _groupDao.Load(exampleGroup)

        Dim expected = New List(Of String) From {"word1", "word2", "wordx"}

        group.GetWords.Should.BeEquivalentTo(expected)
    End Sub

    <Test>
    Public Sub UdpateMarked_Updates_MarkedStatus()
        Dim originalGroup As GroupDto = _groupDao.Load(exampleGroup)
        Dim testWordMarked = originalGroup.GetWord(1)
        Dim testWordUnmarked = originalGroup.GetWord(29)

        testWordMarked.Marked.Should.Be(True)
        testWordUnmarked.Marked.Should.Be(False)

        _groupDao.UpdateMarked(exampleGroup, testWordMarked, False)
        _groupDao.UpdateMarked(exampleGroup, testWordUnmarked, True)

        Dim updatedGroup As GroupDto = _groupDao.Load(exampleGroup)

        updatedGroup.GetWord(1).Marked.Should.Be(False)
        updatedGroup.GetWord(29).Marked.Should.Be(True)
    End Sub

    <Test>
    Public Sub UpdateMarked_Throws_NonExistingWord()
        Dim nonExisting = New MockTestWord(word3, False, "some text")
        Assert.Throws(Of EntryNotFoundException)(Sub() _groupDao.UpdateMarked(exampleGroup, nonExisting, True))
    End Sub

    <Test>
    Public Sub Delete_Existing_Deletes()
        _groupDao.Delete(exampleGroup, testWord1)

        Dim expected = New List(Of TestWord) From {testWord2, testWord29}

        _groupDao.Load(exampleGroup).Entries.Should.BeEquivalentTo(expected)
    End Sub

    <Test>
    Public Sub Delete_NonExisting_Throws()
        Dim nonExisting = New MockTestWord(word3, False, "some text")
        Assert.Throws(Of EntryNotFoundException)(Sub() _groupDao.Delete(exampleGroup, nonExisting))
    End Sub

    <Test>
        Public Sub GetTestWord_Returns_CorrectIndex()
            Dim result = _groupDao.GetTestWord(exampleGroup, "word1", "m")
            Assert.AreEqual(testWord1, result)
        End Sub

    <Test>
    Public Sub GetTestWord_NonExisting_Throws()
        Assert.Throws(Of EntryNotFoundException)(Sub() _groupDao.GetTestWord(exampleGroup, "word", "meaning"))
    End Sub

    <Test>
    Public Sub UniqueLanguage_Returned_WhenExists()
        Dim language = _groupDao.GetUniqueLanguage(exampleGroup)
        language.Should.Be("lang")
    End Sub

    <Test>
    Public Sub UniqueLanguage_Fails_WhenNotUnique()
        Assert.Throws(Of LanguageException)(Sub() _groupDao.GetUniqueLanguage(multiLanguageGroup))
    End Sub

    <Test>
    Public Sub GetLanguages_Returns_Languages()
        Dim languages = _groupDao.GetLanguages(exampleGroup)
        Dim expectedLanguages = New List(Of String) From {"lang"}
        languages.Should.BeEquivalentTo(expectedLanguages)
    End Sub

    <Test>
    Public Sub GetLanguages_Returns_MultipleLanguages()
        Dim languages = _groupDao.GetLanguages(multiLanguageGroup)
        Dim expectedLanguages = New List(Of String) From {"lang", "lang2"}
        languages.Should.BeEquivalentTo(expectedLanguages)
    End Sub

    <Test>
    Public Sub GetUniqueMainLanguages_Returns_WhenExists()
        _groupDao.GetUniqueMainLanguage(exampleGroup).Should.Be("targetLang")
        _groupDao.GetUniqueMainLanguage(multiLanguageGroup).Should.Be("targetLang")
    End Sub

    <Test>
    Public Sub GetMainLanguages_Returns_MainLanguages()
        _groupDao.GetMainLanguages(exampleGroup).Should.BeEquivalentTo(New List(Of String) From {"targetLang"})
    End Sub

End Class
