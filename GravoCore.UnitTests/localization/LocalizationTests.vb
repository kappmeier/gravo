Imports System.IO
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports AwesomeAssertions
Imports Gravo
Imports Microsoft.Data.Sqlite
Imports NUnit.Framework

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
    Public Sub GetTableFor_UnknownLanguage_Throws()
        Assert.Throws(Of Exception)(Sub() _loc.GetTableFor("Klingon")).Message.Should.Be("Wrong language")
    End Sub

    <Test>
    Public Sub SwitchToLanguage_UnknownLanguage_Throws()
        Assert.Throws(Of Exception)(Sub() _loc.SwitchToLanguage("Klingon")).Message.Should.Be("Wrong language")

        _loc.Language.Should.Be("german")
    End Sub

    <Test>
    Public Sub Localization_AsILocalization_RoutesAllMembers()
        Dim loc As ILocalization = _loc
        loc.Language.Should.Be("german")
        loc.GetLanguageNames.Should.Equal("Deutsch", "English")
        loc.GetText(localization.WordTypeCodes(WordType.Verb)).Should.Be(
            _loc.GetText(localization.WordTypeCodes(WordType.Verb)))
        loc.SwitchToLanguage("English")
        loc.Language.Should.Be("english")
    End Sub

    <Test>
    Public Sub GetText_MissingField_ThrowsKeyNotFound()
        Assert.Throws(Of KeyNotFoundException)(Sub() _loc.GetText(9999)).
            Message.Should.Be("No text 9999 in the language table german.")
    End Sub

    Private Shared Function ConstantCodes() As IEnumerable(Of Integer)
        Return GetType(localization).GetFields(BindingFlags.Public Or BindingFlags.Static).
            Where(Function(f) f.IsLiteral AndAlso f.FieldType Is GetType(Integer)).
            Select(Function(f) CInt(f.GetRawConstantValue())).Distinct().ToList()
    End Function

    <Test>
    Public Sub EveryConstant_HasATextInEnglishAndGerman()
        For Each table In {"german", "english"}
            _loc.Language = table
            For Each code In ConstantCodes()
                _loc.GetText(code).
                    Should.NotBeNullOrEmpty("field {0} must have a row in {1}", code, table)
            Next
        Next
    End Sub

    <Test>
    Public Sub EveryConstant_HasTheSamePlaceholdersInEveryLanguage()
        Dim placeholder As New Regex("\{\d\}")
        For Each code In ConstantCodes()
            _loc.Language = "german"
            Dim german = placeholder.Matches(_loc.GetText(code)).
                Cast(Of Match).
                Select(Function(m) m.Value).
                OrderBy(Function(v) v).
                ToList()
            _loc.Language = "english"
            Dim english = placeholder.Matches(_loc.GetText(code)).
                Cast(Of Match).
                Select(Function(m) m.Value).
                OrderBy(Function(v) v).
                ToList()
            english.Should.Equal(
                german, "field {0} must take the same arguments in both languages", code)
        Next
    End Sub
End Class
