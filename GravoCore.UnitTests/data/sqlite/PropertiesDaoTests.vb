Imports FluentAssertions
Imports Gravo
Imports NUnit.Framework
Imports Microsoft.Data.Sqlite
Imports System.IO

Public Class PropertiesDaoTests
    Private ReadOnly ResourceFile As String = "test-data-meta.s3db"
    Private _propertiesDao As PropertiesDao
    Private _tempDb As String
    Private _db As IDataBaseOperation

    <SetUp>
    Public Sub Setup()
        _tempDb = Path.GetTempFileName
        File.Copy(DaoUtils.GetSqliteResource(ResourceFile), _tempDb, True)

        _db = New SQLiteDataBaseOperation()
        _db.Open(_tempDb)

        _propertiesDao = New PropertiesDao(_db)
    End Sub

    <TearDown>
    Public Sub CleanUp()
        _db.Close()
        SqliteConnection.ClearAllPools()

        File.Delete(_tempDb)
    End Sub

    <Test>
    Public Sub Load_LoadsDefault()
        Dim p As Properties = _propertiesDao.LoadProperties()

        p.DBVersionMaxLengthDescription.Should.Be(80)
        p.DictionaryMainMaxLengthLanguage.Should.Be(16)
        p.DictionaryMainMaxLengthMainLanguage.Should.Be(16)
        p.DictionaryMainMaxLengthWordEntry.Should.Be(50)
        p.DictionaryWordsMaxLengthAdditionalTargetLangInfo.Should.Be(50)
        p.DictionaryWordsMaxLengthMeaning.Should.Be(80)
        p.DictionaryWordsMaxLengthPost.Should.Be(16)
        p.DictionaryWordsMaxLengthPre.Should.Be(16)
        p.DictionaryWordsMaxLengthWord.Should.Be(80)
        p.GroupMaxLengthExample.Should.Be(64)
        p.GroupsMaxLengthName.Should.Be(50)
        p.GroupsMaxLengthSubName.Should.Be(50)
        p.GroupsMaxLengthTable.Should.Be(50)
    End Sub

    <Test>
    Public Sub Load_Loads_Version()
        Dim p As Properties = _propertiesDao.LoadProperties()

        Dim expectedVersion As New Properties.DBVersion(1, 23, New Date(2017, 4, 5), "DB-Version Test")

        p.Verion.Should.BeEquivalentTo(expectedVersion)
    End Sub

    <Test>
    Public Sub Load_OnEmpty_Returns()
        Dim emptyDb As IDataBaseOperation = CreateEmptyTestDb()

        Dim fixture As New PropertiesDao(emptyDb)

        fixture.LoadVersions().Should.BeEmpty()

        emptyDb.Close()
    End Sub

    <Test>
    Public Sub Load_EmptyVersionTable_Throws()
        Dim emptyDb As IDataBaseOperation = CreateEmptyTestDb()

        Dim createValidTAbleCommand = "CREATE TABLE [DBVersion] ([Version] TEXT(5) NOT NULL, [Date] DATETIME NOT NULL, [Description] TEXT(80) NOT NULL);"
        emptyDb.ExecuteNonQuery(createValidTAbleCommand, Array.Empty(Of Object))

        Dim fixture As New PropertiesDao(emptyDb)

        Assert.Throws(Of IllegalVersionException)(Sub() fixture.LoadVersions())

        emptyDb.Close()
    End Sub

    Private Shared ReadOnly DbNames As String() = {"WORD_TYPE_SUBSTANTIVE", "WORD_TYPE_VERB", "WORD_TYPE_ADJECTIVE",
        "WORD_TYPE_SIMPLE", "WORD_TYPE_ADVERB", "WORD_TYPE_SET_PHRASE", "WORD_TYPE_EXAMPLE"}

    ''' <summary>
    ''' Adds <c>SupportedWordTypes</c> table and rows to the meta fixture.
    ''' </summary>
    Private Sub InsertWordTypes(names As IEnumerable(Of String))
        _db.ExecuteNonQuery("CREATE TABLE [SupportedWordTypes] ([Type] TEXT(32) NOT NULL, [Index] INT PRIMARY KEY)", Array.Empty(Of Object))
        Dim index As Integer = 10
        For Each name In names
            _db.ExecuteNonQuery("INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES ('" & name & "', " & index & ")", Array.Empty(Of Object))
            index += 1
        Next
    End Sub

    Private Shared Function FoundMap(types As WordTypes) As IDictionary(Of String, WordType)
        Dim field = GetType(WordTypes).GetField("foundWordTypes", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
        Return DirectCast(field.GetValue(types), IDictionary(Of String, WordType))
    End Function

    <Test>
    Public Sub LoadWordTypes_MapsDatabaseNamesToEnum()
        InsertWordTypes(DbNames)

        Dim found = FoundMap(_propertiesDao.LoadWordTypes())

        found.Should.HaveCount(7)
        found("WORD_TYPE_SUBSTANTIVE").Should.Be(WordType.Substantive)
        found("WORD_TYPE_VERB").Should.Be(WordType.Verb)
        found("WORD_TYPE_ADJECTIVE").Should.Be(WordType.Adjective)
        found("WORD_TYPE_SIMPLE").Should.Be(WordType.Simple)
        found("WORD_TYPE_ADVERB").Should.Be(WordType.Adverb)
        found("WORD_TYPE_SET_PHRASE").Should.Be(WordType.SetPhrase)
        found("WORD_TYPE_EXAMPLE").Should.Be(WordType.Example)
    End Sub

    <Test>
    Public Sub LoadWordTypes_IsCaseInsensitive()
        InsertWordTypes(DbNames.Select(Function(n) n.ToLowerInvariant()))

        Dim found = FoundMap(_propertiesDao.LoadWordTypes())

        found("word_type_set_phrase").Should.Be(WordType.SetPhrase)
        found.Should.HaveCount(7)
    End Sub

    <Test>
    Public Sub LoadWordTypes_MissingType_Throws()
        InsertWordTypes(DbNames.Take(6))

        Dim ex = Assert.Throws(Of DataInvalidException)(Sub() _propertiesDao.LoadWordTypes())

        ex.Message.Should.Contain("Should be 7, but only found 6.")
    End Sub

    Public Shared Function CreateEmptyTestDb() As IDataBaseOperation
        Dim emptyDbPath = Path.GetTempFileName
        CreateEmptyTestDb = New SQLiteDataBaseOperation()
        CreateEmptyTestDb.Open(emptyDbPath)
    End Function
End Class
