Imports Microsoft.Data.Sqlite
Imports System.IO
Imports Gravo
Imports NUnit.Framework
Imports FluentAssertions

<TestFixture>
Public Class LocalizationTests

    Private ReadOnly ResourceFile As String = "languages.s3db"
    Private _loc As localization
    Private _tempDb As String
    Private _db As IDataBaseOperation

    <SetUp>
    Public Sub Setup()
        _tempDb = Path.GetTempFileName
        File.Copy(DaoUtils.GetSqliteResource(ResourceFile), _tempDb, True)

        _db = New SQLiteDataBaseOperation()
        _db.Open(_tempDb)

        _loc = New localization(_db)
    End Sub

    <TearDown>
    Public Sub CleanUp()
        _db.Close()
        SqliteConnection.ClearAllPools()

        File.Delete(_tempDb)
    End Sub

    <Test>
    Public Sub GetLanguageNames_ReturnsSortedNames()
        _loc.GetLanguageNames.Should.Equal("Deutsch", "English")
    End Sub

    <Test>
    Public Sub GetText_ByCode_UsesGermanTableByDefault()
        _loc.Language.Should.Be("german")
        _loc.GetText(localization.DISCLAIMER_1).Should.Be("Das Arbeiten mit dieser Version von")
    End Sub

    <Test>
    Public Sub SwitchToLanguage_English_SwitchesTableAndText()
        _loc.SwitchToLanguage("English")

        _loc.Language.Should.Be("english")
        _loc.GetText(localization.DISCLAIMER_1).Should.Be("Working with this version of")
    End Sub

    <Test>
    Public Sub GetTableFor_KnownLanguage_ReturnsTable()
        _loc.GetTableFor("Deutsch").Should.Be("german")
    End Sub

    <Test>
    Public Sub GetVersionFor_KnownLanguage_ReturnsVersion()
        _loc.GetVersionFor("Deutsch").Should.Be("1.00")
    End Sub

    <Test>
    Public Sub GetAuthorFor_KnownLanguage_ReturnsAuthor()
        _loc.GetAuthorFor("Deutsch").Should.Be("Kap")
    End Sub

    <Test>
    Public Sub GetText_ByName_MatchesCode()
        _loc.GetText("WORD_TYPE_VERB").Should.Be(_loc.GetText(localization.WORD_TYPE_VERB))
        _loc.GetText("WORD_TYPE_VERB").Should.Be("Verb")
    End Sub

    ''' <summary>
    ''' Tests that requesting text for an unknown name falls back to the default with index 0.
    ''' </summary>
    <Test>
    Public Sub GetText_UnknownName_RoutesToFieldZero()
        _loc.GetText("NOT_A_FIELD_NAME").Should.Be(_loc.GetText(0))
        _loc.GetText("NOT_A_FIELD_NAME").Should.Be("#")
    End Sub

    <Test>
    Public Sub GetTableFor_UnknownLanguage_Throws()
        Assert.Throws(Of Exception)(Sub() _loc.GetTableFor("Klingon")).Message.Should.Be("Wrong language")
    End Sub

    <Test>
    Public Sub SwitchToLanguage_UnknownLanguage_Throws()
        Assert.Throws(Of Exception)(Sub() _loc.SwitchToLanguage("Klingon")).Message.Should.Be("Wrong language")

        _loc.Language.Should.Be("german")
    End Sub
End Class
