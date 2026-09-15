Imports Gravo
Imports Moq
Imports NUnit.Framework
Imports FluentAssertions

''' <summary>
''' Tests the <see cref="Settings"/> behavior against mocked data.
''' For real data I/O see <see cref="JsonFileSettingsStoreTests"/>.
''' </summary>
<TestFixture>
Public Class SettingsTests
    Private storeMock As Mock(Of ISettingsStore)
    Private settings As Settings

    <SetUp>
    Public Sub Setup()
        storeMock = New Mock(Of ISettingsStore)(MockBehavior.Strict)
        settings = New Settings(storeMock.Object)
    End Sub

    Private Sub SetUpLoad(values As IDictionary(Of String, String))
        storeMock.Setup(Function(s As ISettingsStore) s.Load()).Returns(values)
    End Sub

    <Test>
    Public Sub LoadSettings_EmptyStore_AppliesAllDefaults()
        SetUpLoad(New Dictionary(Of String, String)())

        settings.LoadSettings()

        settings.QueryLanguage.Should().Be(QueryLanguage.TargetLanguage)
        settings.TestSetPhrases.Should().BeFalse()
        settings.SaveWindowPosition.Should().BeFalse()
        settings.UseCards.Should().BeTrue()
        settings.CardsInitialInterval.Should().Be(1)
        settings.LastGroup.Should().Be("")
        settings.LastSubGroup.Should().Be("")
        settings.MainWindowState.Should().Be(WindowStateSetting.Normal)
        settings.ChildWindowState.Should().Be(WindowStateSetting.Normal)
        settings.MainWindowSettings.Should().Be(New WindowSettings With {.name = "WindowSettingsMain",
                .height = 600, .width = 800, .posX = 0, .posY = 0})
        settings.ExplorerWindowSettings.Should().Be(New WindowSettings With {.name = "WindowSettingsExplorer",
                .height = 300, .width = 400, .posX = 0, .posY = 0})
        settings.GroupWindowSettings.Should().Be(New WindowSettings With {.name = "WindowSettingsGroups",
                .height = 300, .width = 400, .posX = 22, .posY = 29})
        settings.StatisticWindowSettings.Should().Be(New WindowSettings With {.name = "WindowSettingsStatistic",
                .height = 300, .width = 400, .posX = 44, .posY = 58})
    End Sub

    ''' <summary>
    ''' All keys in the store can be restored. Test with non-default values.
    ''' </summary>
    ''' <remarks>
    ''' The test should cover all program settings. It is likely that it drifts over time. If applicable, additional
    ''' settings should be added. (See <see cref="SaveSettings_AfterLoadEmptyStore_WritesAllDefaultsWithExactKeys"/>).
    ''' </remarks>
    <Test>
    Public Sub LoadSettings_FullStore_AppliesAllStoredValues()
        Dim values As New Dictionary(Of String, String) From {
            {"TestSetPhrases", "1"},
            {"TestTargetLanguage", "0"},
            {"SaveWindowPosition", "1"},
            {"LastGroup", "Verbs"},
            {"LastSubGroup", "Nouns"},
            {"WindowSettingsExplorerHeight", "111"},
            {"WindowSettingsExplorerWidth", "222"},
            {"WindowSettingsExplorerPosX", "333"},
            {"WindowSettingsExplorerPosY", "444"},
            {"WindowSettingsGroupsHeight", "555"},
            {"WindowSettingsGroupsWidth", "666"},
            {"WindowSettingsGroupsPosX", "777"},
            {"WindowSettingsGroupsPosY", "888"},
            {"WindowSettingsMainHeight", "999"},
            {"WindowSettingsMainWidth", "1010"},
            {"WindowSettingsMainPosX", "1111"},
            {"WindowSettingsMainPosY", "1212"},
            {"WindowSettingsStatisticHeight", "1313"},
            {"WindowSettingsStatisticWidth", "1414"},
            {"WindowSettingsStatisticPosX", "1515"},
            {"WindowSettingsStatisticPosY", "1616"},
            {"MainWindowState", "2"},
            {"ChildWindowState", "1"},
            {"UseCards", "0"},
            {"CardsInitialInterval", "5"}
        }
        SetUpLoad(values)

        settings.LoadSettings()

        settings.QueryLanguage.Should().Be(QueryLanguage.OriginalLanguage)
        settings.TestSetPhrases.Should().BeTrue()
        settings.SaveWindowPosition.Should().BeTrue()
        settings.LastGroup.Should().Be("Verbs")
        settings.LastSubGroup.Should().Be("Nouns")
        settings.ExplorerWindowSettings.Should().Be(New WindowSettings With {.name = "WindowSettingsExplorer",
                .height = 111, .width = 222, .posX = 333, .posY = 444})
        settings.GroupWindowSettings.Should().Be(New WindowSettings With {.name = "WindowSettingsGroups",
                .height = 555, .width = 666, .posX = 777, .posY = 888})
        settings.MainWindowSettings.Should().Be(New WindowSettings With {.name = "WindowSettingsMain",
                .height = 999, .width = 1010, .posX = 1111, .posY = 1212})
        settings.StatisticWindowSettings.Should().Be(New WindowSettings With {.name = "WindowSettingsStatistic",
                .height = 1313, .width = 1414, .posX = 1515, .posY = 1616})
        settings.MainWindowState.Should().Be(WindowStateSetting.Maximized)
        settings.ChildWindowState.Should().Be(WindowStateSetting.Minimized)
        settings.UseCards.Should().BeFalse()
        settings.CardsInitialInterval.Should().Be(5)
    End Sub

    <Test>
    Public Sub LoadSettings_PartialStore_AppliesGivenKeysDefaultsRest()
        Dim values As New Dictionary(Of String, String) From {
            {"TestSetPhrases", "1"},
            {"LastGroup", "Verbs"},
            {"WindowSettingsMainHeight", "900"}
        }
        SetUpLoad(values)

        settings.LoadSettings()

        settings.TestSetPhrases.Should().BeTrue()
        settings.LastGroup.Should().Be("Verbs")
        settings.MainWindowSettings.Should().Be(New WindowSettings With {.name = "WindowSettingsMain",
                .height = 900, .width = 800, .posX = 0, .posY = 0})

        settings.QueryLanguage.Should().Be(QueryLanguage.TargetLanguage)
        settings.SaveWindowPosition.Should().BeFalse()
        settings.LastSubGroup.Should().Be("")
        settings.UseCards.Should().BeTrue()
        settings.CardsInitialInterval.Should().Be(1)
        settings.MainWindowState.Should().Be(WindowStateSetting.Normal)
        settings.ChildWindowState.Should().Be(WindowStateSetting.Normal)
        settings.ExplorerWindowSettings.Should().Be(New WindowSettings With {.name = "WindowSettingsExplorer",
                .height = 300, .width = 400, .posX = 0, .posY = 0})
        settings.GroupWindowSettings.Should().Be(New WindowSettings With {.name = "WindowSettingsGroups",
                .height = 300, .width = 400, .posX = 22, .posY = 29})
        settings.StatisticWindowSettings.Should().Be(New WindowSettings With {.name = "WindowSettingsStatistic",
                .height = 300, .width = 400, .posX = 44, .posY = 58})
    End Sub

    ''' <summary>
    ''' Loading settings should call <c>store.Load()</c> only once.
    ''' </summary>
    <Test>
    Public Sub LoadSettings_CallsStoreLoadExactlyOnce()
        SetUpLoad(New Dictionary(Of String, String)())

        settings.LoadSettings()

        storeMock.Verify(Function(s As ISettingsStore) s.Load(), Times.Exactly(1))
    End Sub

    ''' <summary>
    ''' All default keys are written when <c>SaveSettings</c> is called.
    ''' </summary>
    ''' <remarks>
    ''' The test should cover all program settings. It is likely that it drifts over time. If applicable, additional
    ''' settings should be added. (See <see cref="LoadSettings_FullStore_AppliesAllStoredValues"/>).
    ''' </remarks>
    <Test>
    Public Sub SaveSettings_AfterLoadEmptyStore_WritesAllDefaultsWithExactKeys()
        SetUpLoad(New Dictionary(Of String, String)())
        Dim saved As IDictionary(Of String, String) = Nothing
        storeMock.Setup(Sub(s As ISettingsStore) s.Save(It.IsAny(Of IDictionary(Of String, String))())).
            Callback(Of IDictionary(Of String, String))(Sub(d As IDictionary(Of String, String)) saved = d)

        settings.LoadSettings()
        settings.SaveSettings()

        storeMock.Verify(Sub(s As ISettingsStore) s.Save(It.IsAny(Of IDictionary(Of String, String))()), Times.Exactly(1))
        Dim expected As New Dictionary(Of String, String) From {
            {"TestSetPhrases", "0"},
            {"TestTargetLanguage", "1"},
            {"SaveWindowPosition", "0"},
            {"LastGroup", ""},
            {"LastSubGroup", ""},
            {"WindowSettingsExplorerHeight", "300"},
            {"WindowSettingsExplorerWidth", "400"},
            {"WindowSettingsExplorerPosX", "0"},
            {"WindowSettingsExplorerPosY", "0"},
            {"WindowSettingsGroupsHeight", "300"},
            {"WindowSettingsGroupsWidth", "400"},
            {"WindowSettingsGroupsPosX", "22"},
            {"WindowSettingsGroupsPosY", "29"},
            {"WindowSettingsMainHeight", "600"},
            {"WindowSettingsMainWidth", "800"},
            {"WindowSettingsMainPosX", "0"},
            {"WindowSettingsMainPosY", "0"},
            {"WindowSettingsStatisticHeight", "300"},
            {"WindowSettingsStatisticWidth", "400"},
            {"WindowSettingsStatisticPosX", "44"},
            {"WindowSettingsStatisticPosY", "58"},
            {"MainWindowState", "0"},
            {"ChildWindowState", "0"},
            {"UseCards", "1"},
            {"CardsInitialInterval", "1"}
        }
        saved.Should().NotBeNull()
        saved.Should().Equal(expected)
    End Sub

    <Test>
    Public Sub SaveSettings_WithModifiedProperties_WritesEncodedValues()
        SetUpLoad(New Dictionary(Of String, String)())
        Dim saved As IDictionary(Of String, String) = Nothing
        storeMock.Setup(Sub(s As ISettingsStore) s.Save(It.IsAny(Of IDictionary(Of String, String))())).
            Callback(Of IDictionary(Of String, String))(Sub(d As IDictionary(Of String, String)) saved = d)

        settings.LoadSettings()
        settings.QueryLanguage = QueryLanguage.OriginalLanguage
        settings.TestSetPhrases = True
        settings.SaveWindowPosition = True
        settings.UseCards = False
        settings.CardsInitialInterval = 3
        settings.LastGroup = "x"
        settings.LastSubGroup = "y"
        settings.MainWindowState = WindowStateSetting.Maximized
        settings.ChildWindowState = WindowStateSetting.Minimized
        settings.MainWindowSettings = New WindowSettings With {.name = "WindowSettingsMain",
                .height = 111, .width = 222, .posX = 333, .posY = 444}

        settings.SaveSettings()

        saved.Should().NotBeNull()
        saved.Should().Contain("TestTargetLanguage", "0")
        saved.Should().Contain("TestSetPhrases", "1")
        saved.Should().Contain("SaveWindowPosition", "1")
        saved.Should().Contain("UseCards", "0")
        saved.Should().Contain("CardsInitialInterval", "3")
        saved.Should().Contain("LastGroup", "x")
        saved.Should().Contain("LastSubGroup", "y")
        saved.Should().Contain("MainWindowState", "2")
        saved.Should().Contain("ChildWindowState", "1")
        saved.Should().Contain("WindowSettingsMainHeight", "111")
        saved.Should().Contain("WindowSettingsMainWidth", "222")
        saved.Should().Contain("WindowSettingsMainPosX", "333")
        saved.Should().Contain("WindowSettingsMainPosY", "444")
    End Sub

    ''' <summary>
    ''' Currently <see cref="QueryLanguage.Both"/> is not supported and cannot be encoded as <c>TestTargetLanguage</c>.
    ''' </summary>
    ''' <remarks>
    ''' Test until implementation done; this behavior may change in the future.
    ''' </remarks>
    <Test>
    Public Sub SaveSettings_QueryLanguageBoth_ThrowsArgumentExceptionAndSavesNothing()
        SetUpLoad(New Dictionary(Of String, String)())
        settings.LoadSettings()
        settings.QueryLanguage = QueryLanguage.Both

        Assert.Throws(Of ArgumentException)(Sub() settings.SaveSettings())
    End Sub

    <Test>
    Public Sub LoadSettings_UnknownWindowStateValue_DefaultsToNormal()
        Dim values As New Dictionary(Of String, String) From {
            {"MainWindowState", "7"},
            {"ChildWindowState", "7"}
        }
        SetUpLoad(values)

        settings.LoadSettings()

        settings.MainWindowState.Should().Be(WindowStateSetting.Normal)
        settings.ChildWindowState.Should().Be(WindowStateSetting.Normal)
    End Sub

    <Test>
    Public Sub LoadSettings_TestTargetLanguageZero_ReturnsOriginalLanguage()
        Dim values As New Dictionary(Of String, String) From {{"TestTargetLanguage", "0"}}
        SetUpLoad(values)

        settings.LoadSettings()

        settings.QueryLanguage.Should().Be(QueryLanguage.OriginalLanguage)
    End Sub

    ''' <summary>
    ''' An unparsable integer text should fall back to the default, same as a missing key.
    ''' </summary>
    <Test>
    Public Sub LoadSettings_UnparsableCardsInitialInterval_DefaultsToOne()
        Dim values As New Dictionary(Of String, String) From {{"CardsInitialInterval", "abc"}}
        SetUpLoad(values)

        settings.LoadSettings()

        settings.CardsInitialInterval.Should().Be(1)
    End Sub

    ''' <summary>
    ''' Any nonzero integer text, not just <c>"1"</c>, should be interpreted as True.
    ''' </summary>
    <Test>
    Public Sub LoadSettings_NonZeroIntegerBooleanText_ReadsAsTrue()
        Dim values As New Dictionary(Of String, String) From {
            {"TestSetPhrases", "5"},
            {"UseCards", "-1"}
        }
        SetUpLoad(values)

        settings.LoadSettings()

        settings.TestSetPhrases.Should().BeTrue()
        settings.UseCards.Should().BeTrue()
    End Sub
End Class
