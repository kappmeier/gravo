Imports Microsoft.Data.Sqlite
Imports System.IO
Imports Gravo
Imports Moq
Imports NUnit.Framework
Imports FluentAssertions

''' <summary>
''' Tests for the dictionary part of <see cref="DatabaseTransfer"/>. Tests run on a copy of
''' <c>test-data-dictionary.s3db</c>. Target is a freshly created vocabulary database.
''' </summary>
''' <remarks>
''' The database contains the following data:
'''' - main entries word1/lang (words 1, 2, 3, 33)
'''' - some word/lang2 (word 4)
'''' - wordx/lang (word 29)
'''' - another/lang2 (no word)
''' </remarks>
<TestFixture>
Public Class DatabaseTransferLanguageTests
    Private ReadOnly ResourceFile As String = "test-data-dictionary.s3db"
    Private ReadOnly mainLanguage As String = "targetLang"

    Private _sourcePath As String
    Private _targetPath As String
    Private _sourceDb As IDataBaseOperation
    Private _targetDb As IDataBaseOperation
    Private _source As VocabularyDatabase
    Private _target As VocabularyDatabase
    Private _transfer As DatabaseTransfer

    <SetUp>
    Public Sub Setup()
        _sourcePath = Path.GetTempFileName()
        File.Copy(DaoUtils.GetSqliteResource(ResourceFile), _sourcePath, True)
        _sourceDb = New SQLiteDataBaseOperation()
        _sourceDb.Open(_sourcePath)

        _targetPath = Path.GetTempFileName()
        ManagementDao.CreateNewVocabularyDatabase(_targetPath)
        _targetDb = New SQLiteDataBaseOperation()
        _targetDb.Open(_targetPath)

        _source = New VocabularyDatabase(_sourceDb)
        _target = New VocabularyDatabase(_targetDb)
        _transfer = New DatabaseTransfer(_source, _target)
    End Sub

    <TearDown>
    Public Sub CleanUp()
        _sourceDb.Close()
        _targetDb.Close()
        SqliteConnection.ClearAllPools()

        File.Delete(_sourcePath)
        File.Delete(_targetPath)
    End Sub

    <Test>
    Public Sub CopyLanguage_Lang_CopiesMainsWordsAndCards()
        Dim result As TransferResult = _transfer.CopyLanguage("lang", mainLanguage, False)

        result.MainEntries.Should().Be(2)
        result.SubEntries.Should().Be(5)
        result.Groups.Should().Be(0)
        result.SubGroups.Should().Be(0)
        result.GroupEntries.Should().Be(0)
        _target.Dictionary.WordCount("lang", mainLanguage).Should().Be(2)
        _target.Dictionary.WordCountTotal("lang", mainLanguage).Should().Be(5)
        CountRows(_targetDb, "Cards").Should().Be(5)
    End Sub

    <Test>
    Public Sub CopyLanguage_Lang2_SkipsMainWithoutWords()
        Dim result As TransferResult = _transfer.CopyLanguage("lang2", mainLanguage, False)

        result.MainEntries.Should().Be(1)
        result.SubEntries.Should().Be(1)
        MainWords("lang2").Should().Equal("some word")
    End Sub

    <Test>
    Public Sub CopyLanguage_Lang2IncludeEmptyMains_CopiesMainWithoutWords()
        Dim result As TransferResult = _transfer.CopyLanguage("lang2", mainLanguage, True)

        result.MainEntries.Should().Be(2)
        result.SubEntries.Should().Be(1)
        MainWords("lang2").Should().Equal("another", "some word")
        _target.Dictionary.WordCountTotal("lang2", mainLanguage).Should().Be(1)
    End Sub

    <Test>
    Public Sub CopyLanguage_PreservesWordAttributes()
        _transfer.CopyLanguage("lang", mainLanguage, False)
        _transfer.CopyLanguage("lang2", mainLanguage, False)

        ' WordEntry.Equals ignores the index, which differs between the databases.
        _target.Dictionary.GetWordsAndSubWords("word1", "lang", mainLanguage).Should().Equal(
            _source.Dictionary.GetWordsAndSubWords("word1", "lang", mainLanguage))
        Dim someWordMain As MainEntry = _target.Dictionary.GetMainEntry("some word", "lang2", mainLanguage)
        _target.Dictionary.GetEntry(someWordMain, "some word", "test").Should().Be(
            New WordEntry("some word", "", "", WordType.Adjective, "test", "info", False))
        Dim wordxMain As MainEntry = _target.Dictionary.GetMainEntry("wordx", "lang", mainLanguage)
        _target.Dictionary.GetEntry(wordxMain, "wordx", "").Should().Be(
            New WordEntry("wordx", "", "", WordType.Adjective, "", "", True))
    End Sub

    <Test>
    Public Sub CopyLanguage_SecondCall_AddsNothing()
        _transfer.CopyLanguage("lang", mainLanguage, False)

        Dim result As TransferResult = _transfer.CopyLanguage("lang", mainLanguage, False)

        result.MainEntries.Should().Be(0)
        result.SubEntries.Should().Be(0)
        _target.Dictionary.WordCount("lang", mainLanguage).Should().Be(2)
        _target.Dictionary.WordCountTotal("lang", mainLanguage).Should().Be(5)
        CountRows(_targetDb, "Cards").Should().Be(5)
    End Sub

    <Test>
    Public Sub CopyLanguage_ExistingTargetWord_KeepsTargetAttributes()
        Dim existing As New WordEntry("word1", "other pre", "other post", WordType.Substantive, "m", "other info", True)
        _target.Dictionary.AddEntry("word1", "lang", mainLanguage)
        _target.Dictionary.AddSubEntry(existing, "word1", "lang", mainLanguage)

        Dim result As TransferResult = _transfer.CopyLanguage("lang", mainLanguage, False)

        result.MainEntries.Should().Be(1)
        result.SubEntries.Should().Be(4)
        Dim word1Main As MainEntry = _target.Dictionary.GetMainEntry("word1", "lang", mainLanguage)
        _target.Dictionary.GetEntry(word1Main, "word1", "m").Should().Be(existing)
        _target.Dictionary.WordCountTotal("lang", mainLanguage).Should().Be(5)
    End Sub

    <Test>
    Public Sub CopyLanguage_UnknownLanguage_AddsNothing()
        Dim result As TransferResult = _transfer.CopyLanguage("klingon", mainLanguage, True)

        result.MainEntries.Should().Be(0)
        result.SubEntries.Should().Be(0)
        _target.Dictionary.DictionaryLanguages(mainLanguage).Should().BeEmpty()
    End Sub

    <Test>
    Public Sub CopyDictionary_CopiesAllLanguagesIncludingEmptyMains()
        Dim result As TransferResult = _transfer.CopyDictionary(mainLanguage)

        result.MainEntries.Should().Be(4)
        result.SubEntries.Should().Be(6)
        _target.Dictionary.DictionaryLanguages(mainLanguage).Should().Equal("lang", "lang2")
        MainWords("lang2").Should().Equal("another", "some word")
        CountRows(_targetDb, "Cards").Should().Be(6)
    End Sub

    <Test>
    Public Sub CopyLanguage_WordExistsInTarget_NeverAddsSubEntry()
        Dim dictionaryMock As New Mock(Of IDictionaryDao)(MockBehavior.Strict)
        Dim groupsMock As New Mock(Of IGroupsDao)(MockBehavior.Strict)
        Dim groupMock As New Mock(Of IGroupDao)(MockBehavior.Strict)
        Dim targetMain As New MainEntry("word1", "lang", mainLanguage)
        Dim targetWord As New WordEntry("word1", "", "", WordType.Verb, "m", "", False)
        dictionaryMock.Setup(Function(x) x.GetMainEntry(It.Ref(Of String).IsAny, "lang", mainLanguage)).Returns(targetMain)
        dictionaryMock.Setup(Function(x) x.GetEntry(targetMain, It.IsAny(Of String), It.IsAny(Of String))).Returns(targetWord)
        Dim transfer As New DatabaseTransfer(_source, New VocabularyDatabase(dictionaryMock.Object, groupsMock.Object, groupMock.Object))

        Dim result As TransferResult = transfer.CopyLanguage("lang", mainLanguage, False)

        result.MainEntries.Should().Be(0)
        result.SubEntries.Should().Be(0)
    End Sub

    Private Function MainWords(language As String) As IEnumerable(Of String)
        Return _target.Dictionary.GetMainEntries(language, mainLanguage).Select(Function(m) m.Word)
    End Function

    Private Shared Function CountRows(db As IDataBaseOperation, table As String) As Integer
        db.ExecuteReader("SELECT COUNT(*) FROM [" & table & "]", Array.Empty(Of Object))
        db.DBCursor.Read()
        CountRows = db.SecureGetInt32(0)
        db.DBCursor.Close()
    End Function
End Class
