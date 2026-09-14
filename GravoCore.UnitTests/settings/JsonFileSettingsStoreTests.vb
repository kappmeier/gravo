Imports System.IO
Imports Gravo
Imports NUnit.Framework
Imports FluentAssertions

''' <summary>
''' Tests the <see cref="JsonFileSettingsStore"/> with a temporary file a. The default location
''' <see cref="JsonFileSettingsStore.DefaultPath"/> is asserted, but not written to.
''' </summary>
<TestFixture>
Public Class JsonFileSettingsStoreTests
    Private tempDir As String
    Private filePath As String

    <SetUp>
    Public Sub Setup()
        tempDir = Path.Combine(Path.GetTempPath(), "gravo-settings-" & Guid.NewGuid().ToString("N"))
        filePath = Path.Combine(tempDir, "settings.json")
    End Sub

    <TearDown>
    Public Sub CleanUp()
        If Directory.Exists(tempDir) Then Directory.Delete(tempDir, True)
    End Sub

    <Test>
    Public Sub Load_MissingFile_ReturnsEmptyDictionary()
        Dim store As New JsonFileSettingsStore(filePath)

        Dim result As IDictionary(Of String, String) = store.Load()

        result.Should().NotBeNull()
        result.Should().BeEmpty()
    End Sub

    <Test>
    Public Sub Save_DirectoryDoesNotExistYet_CreatesDirectoryAndFile()
        Directory.Exists(tempDir).Should().BeFalse()
        Dim store As New JsonFileSettingsStore(filePath)

        store.Save(New Dictionary(Of String, String) From {{"a", "b"}})

        Directory.Exists(tempDir).Should().BeTrue()
        File.Exists(filePath).Should().BeTrue()
    End Sub

    ''' <summary>
    ''' Tests that values containing special characters and the empty string remain unchanged after JSON encoding and
    ''' reading again.
    ''' </summary>
    <Test>
    Public Sub SaveThenLoad_SpecialCharacterValues_RoundTrip()
        Dim store As New JsonFileSettingsStore(filePath)
        Dim quoted As String = "she said " & Chr(34) & "hi" & Chr(34)
        Dim values As New Dictionary(Of String, String) From {
            {"quote", quoted},
            {"umlaut", "Übung äöü ß"},
            {"equals", "a=b"},
            {"slash", "a/b"},
            {"empty", ""}
        }

        store.Save(values)
        Dim loaded As IDictionary(Of String, String) = store.Load()

        loaded.Should().Equal(values)
    End Sub

    <Test>
    Public Sub Load_CorruptFile_ReturnsEmptyDictionary()
        Directory.CreateDirectory(tempDir)
        File.WriteAllText(filePath, "this is { not json")
        Dim store As New JsonFileSettingsStore(filePath)

        Dim result As IDictionary(Of String, String) = store.Load()

        result.Should().NotBeNull()
        result.Should().BeEmpty()
    End Sub

    <Test>
    Public Sub Load_EmptyFile_ReturnsEmptyDictionary()
        Directory.CreateDirectory(tempDir)
        File.WriteAllText(filePath, "")
        Dim store As New JsonFileSettingsStore(filePath)

        Dim result As IDictionary(Of String, String) = store.Load()

        result.Should().NotBeNull()
        result.Should().BeEmpty()
    End Sub

    <Test>
    Public Sub DefaultPath_UnderApplicationDataGravoSettingsJson()
        Dim defaultPath As String = JsonFileSettingsStore.DefaultPath()

        defaultPath.Should().Be(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Gravo", "settings.json"))
    End Sub

    ''' <summary>
    ''' The end-to-end test for saving and loading settings <see cref="Settings"/> through the real JSON file store.
    ''' </summary>
    <Test>
    Public Sub Settings_SaveThenLoadOverRealStore_RoundTrips()
        Dim store As New JsonFileSettingsStore(filePath)
        Dim settings As New Settings(store)
        settings.LoadSettings()
        settings.TestSetPhrases = True
        settings.QueryLanguage = QueryLanguage.OriginalLanguage
        settings.MainWindowState = WindowStateSetting.Maximized
        settings.LastGroup = "Verbs"
        settings.MainWindowSettings = New WindowSettings With {.name = "WindowSettingsMain",
                .height = 111, .width = 222, .posX = 333, .posY = 444}
        settings.SaveSettings()

        Dim reloadedStore As New JsonFileSettingsStore(filePath)
        Dim reloaded As New Settings(reloadedStore)
        reloaded.LoadSettings()

        reloaded.TestSetPhrases.Should().BeTrue()
        reloaded.QueryLanguage.Should().Be(QueryLanguage.OriginalLanguage)
        reloaded.MainWindowState.Should().Be(WindowStateSetting.Maximized)
        reloaded.LastGroup.Should().Be("Verbs")
        reloaded.MainWindowSettings.Should().Be(New WindowSettings With {.name = "WindowSettingsMain",
                .height = 111, .width = 222, .posX = 333, .posY = 444})
    End Sub
End Class
