using System.Collections.ObjectModel;
using System.IO;
using FluentAssertions;
using Gravo;
using GravoApp.Localization;
using GravoApp.Services;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using Moq;
using NUnit.Framework;

namespace GravoApp.Tests.ViewModels;

public class MainViewModelTests
{
    private Mock<IDataBaseOperation> _db = null!;
    private Mock<IDictionaryDao> _dictionary = null!;
    private Mock<IGroupsDao> _groups = null!;
    private Mock<IGroupDao> _group = null!;
    private Mock<ICardsDao> _cards = null!;
    private Mock<IManagementDao> _management = null!;
    private Mock<IPropertiesDao> _properties = null!;
    private Mock<ISettingsStore> _store = null!;
    private Mock<IDialogService> _dialogs = null!;
    private Mock<ILocalization> _loc = null!;
    private Settings _settings = null!;
    private string _dbPath = null!;

    [SetUp]
    public void SetUp()
    {
        _db = new Mock<IDataBaseOperation>();
        _dictionary = new Mock<IDictionaryDao>();
        _groups = new Mock<IGroupsDao>();
        _group = new Mock<IGroupDao>();
        _cards = new Mock<ICardsDao>();
        _management = new Mock<IManagementDao>(MockBehavior.Strict);
        _properties = Fakes.Properties();
        _settings = Fakes.DefaultSettings(out _store);
        _dialogs = Fakes.Dialogs();
        _loc = Fakes.Localization();
        _dbPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".s3db");
    }

    private MainViewModel Create() => new(new AppServices(
        _db.Object, _dbPath, new VocabularyDatabase(_dictionary.Object, _groups.Object, _group.Object),
        _cards.Object, _management.Object, _properties.Object, _settings, _loc.Object,
        new UiTexts(_loc.Object), _dialogs.Object));

    /// <summary>
    /// Creates a <see cref="MainViewModel"/> with mocks for the headless window tests.
    /// </summary>
    /// <remarks>
    /// The database version is up to date, so opening the window shows no dialog, and the list queries of the
    /// dictionary and group DAOs return empty collections.
    /// </remarks>
    public static MainViewModel CreateFor(Mock<ILocalization> loc)
    {
        var tests = new MainViewModelTests();
        tests.SetUp();
        tests._loc = loc;
        tests._management.Setup(m => m.IsVersionUpToDate()).Returns(true);
        tests._groups.Setup(g => g.GetGroups()).Returns(new Collection<string>());
        tests._groups.Setup(g => g.GetAllGroups()).Returns(new Collection<GroupEntry>());
        tests._groups.Setup(g => g.GetSubGroups(It.IsAny<string>())).Returns(new List<GroupEntry>());
        tests._dictionary.Setup(d => d.DictionaryMainLanguages()).Returns(new List<string>());
        tests._dictionary.Setup(d => d.DictionaryLanguages(It.IsAny<string>())).Returns(new List<string>());
        tests._dictionary.Setup(d => d.GetMainEntries(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(new List<MainEntry>());
        tests._dictionary.Setup(d => d.GetWords(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(new List<WordEntry>());
        return tests.Create();
    }

    [Test]
    public void Constructor_BuildsLanguageMenuAndSelectsDeutsch()
    {
        var vm = Create();
        vm.Languages.Select(l => l.Name).Should().Equal("Deutsch", "English");
        vm.Languages.Single(l => l.IsChecked).Name.Should().Be("Deutsch");
        _loc.Verify(l => l.SwitchToLanguage("Deutsch"), Times.Once);
        vm.Title.Should().Be("Gravo");
    }

    private void SetUpGroup(params TestWord[] words)
    {
        var entry = new GroupEntry(1, "G", "U", "Group1");
        _groups.Setup(g => g.GetGroups()).Returns(new Collection<string> { "G" });
        _groups.Setup(g => g.GetSubGroups("G")).Returns(new List<GroupEntry> { entry });
        _groups.Setup(g => g.GetGroup("G", "U")).Returns(entry);
        _group.Setup(g => g.Load(ref It.Ref<GroupEntry>.IsAny)).Returns(new GroupDto(entry, words.ToList()));
    }

    [Test]
    public async Task AddWords_OpensWordInputDialog()
    {
        _dictionary.Setup(d => d.DictionaryLanguages("german")).Returns(new List<string> { "english" });
        _dictionary.Setup(d => d.DictionaryMainLanguages()).Returns(new List<string> { "german" });
        _groups.Setup(g => g.GetGroups()).Returns(new Collection<string>());
        await Create().AddWordsCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowDialogAsync(It.IsAny<WordInputViewModel>()), Times.Once);
    }

    [Test]
    public async Task AddWords_InvalidWordTypes_ShowsErrorWithoutDialog()
    {
        _dictionary.Setup(d => d.DictionaryLanguages("german")).Returns(new List<string> { "english" });
        _dictionary.Setup(d => d.DictionaryMainLanguages()).Returns(new List<string> { "german" });
        _groups.Setup(g => g.GetGroups()).Returns(new Collection<string>());
        _properties.Setup(p => p.LoadWordTypes()).Throws(new DataInvalidException("Word type invalid."));
        await Create().AddWordsCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync("Fehler", "Word type invalid."), Times.Once);
        _dialogs.Verify(d => d.ShowDialogAsync(It.IsAny<ViewModelBase>()), Times.Never);
    }

    [Test]
    public void ShowGroupInput_AddsTabOnceAndSelectsIt()
    {
        _groups.Setup(g => g.GetGroups()).Returns(new Collection<string>());
        _dictionary.Setup(d => d.DictionaryLanguages("german")).Returns(new List<string>());
        var fixture = Create();
        fixture.ShowGroupInputCommand.Execute(null);
        fixture.ShowGroupInputCommand.Execute(null);
        var tab = fixture.Tabs.Should().ContainSingle().Which.Should().BeOfType<GroupInputViewModel>().Which;
        fixture.SelectedTab.Should().BeSameAs(tab);
    }

    [Test]
    public async Task TestGroups_Cancelled_OpensNoQuiz()
    {
        SetUpGroup(new TestWord(new WordEntry("house", "", "", WordType.Substantive, "Haus", "", false), false, ""));
        _dialogs.Setup(d => d.ShowDialogAsync(It.IsAny<TestSelectViewModel>())).ReturnsAsync(false);
        await Create().TestGroupsCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowWindow(It.IsAny<ViewModelBase>()), Times.Never);
    }

    [Test]
    public async Task TestGroups_AcceptedWithoutSubGroup_OpensNoQuiz()
    {
        _groups.Setup(g => g.GetGroups()).Returns(new Collection<string> { "G" });
        _groups.Setup(g => g.GetSubGroups("G")).Returns(new List<GroupEntry>());
        await Create().TestGroupsCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowDialogAsync(It.IsAny<TestSelectViewModel>()), Times.Once);
        _dialogs.Verify(d => d.ShowWindow(It.IsAny<ViewModelBase>()), Times.Never);
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task TestGroups_Accepted_OpensQuizInOriginalLanguage()
    {
        _settings = Fakes.DefaultSettings(out _store, new Dictionary<string, string> { ["TestTargetLanguage"] = "0" });
        SetUpGroup(new TestWord(new WordEntry("house", "", "", WordType.Substantive, "Haus", "", false), false, ""));
        QuizViewModel? quiz = null;
        _dialogs.Setup(d => d.ShowWindow(It.IsAny<QuizViewModel>()))
            .Callback<ViewModelBase>(v => quiz = (QuizViewModel)v);
        await Create().TestGroupsCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowWindow(It.IsAny<QuizViewModel>()), Times.Once);
        quiz!.Question.Should().Be("Haus");
        quiz.CountText.Should().Be("1");
    }

    [Test]
    public async Task TestLanguage_Accepted_OpensQuizInTargetLanguage()
    {
        _dictionary.Setup(d => d.DictionaryLanguages("german")).Returns(new List<string> { "english" });
        _dictionary.Setup(d => d.GetWords("english", "german")).Returns(new List<WordEntry>
        {
            new("house", "", "", WordType.Substantive, "Haus", "", false),
        });
        QuizViewModel? quiz = null;
        _dialogs.Setup(d => d.ShowWindow(It.IsAny<QuizViewModel>()))
            .Callback<ViewModelBase>(v => quiz = (QuizViewModel)v);
        await Create().TestLanguageCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowDialogAsync(It.IsAny<LanguageSelectViewModel>()), Times.Once);
        quiz!.Question.Should().Be("house");
    }

    [Test]
    public async Task TestLanguage_Cancelled_OpensNoQuiz()
    {
        _dictionary.Setup(d => d.DictionaryLanguages("german")).Returns(new List<string> { "english" });
        _dialogs.Setup(d => d.ShowDialogAsync(It.IsAny<LanguageSelectViewModel>())).ReturnsAsync(false);
        await Create().TestLanguageCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowWindow(It.IsAny<ViewModelBase>()), Times.Never);
    }

    [Test]
    public void SwitchLanguage_SwitchesChecksAndRefreshesTexts()
    {
        var vm = Create();
        var refreshed = false;
        vm.Texts.PropertyChanged += (_, e) => refreshed |= e.PropertyName == "Item[]";
        vm.Languages.Single(l => l.Name == "English").SelectCommand.Execute(null);
        _loc.Verify(l => l.SwitchToLanguage("English"), Times.Once);
        vm.Languages.Single(l => l.IsChecked).Name.Should().Be("English");
        refreshed.Should().BeTrue();
    }

    [Test]
    public async Task CheckDatabaseVersion_Outdated_ShowsHint()
    {
        _management.Setup(m => m.IsVersionUpToDate()).Returns(false);
        await Create().CheckDatabaseVersionAsync();
        _dialogs.Verify(
            d => d.ShowMessageAsync("T" + localization.HINT, "T" + localization.DB_VERSION_OUTDATED), Times.Once);
    }

    [Test]
    public async Task CheckDatabaseVersion_UpToDate_ShowsNothing()
    {
        _management.Setup(m => m.IsVersionUpToDate()).Returns(true);
        await Create().CheckDatabaseVersionAsync();
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void RestoreGeometry_WithoutSaveWindowPosition_IsNull() => Create().RestoreGeometry().Should().BeNull();

    [Test]
    public void RestoreGeometry_WithSaveWindowPosition_ReturnsStoredValues()
    {
        _settings = Fakes.DefaultSettings(out _store, new Dictionary<string, string>
        {
            ["SaveWindowPosition"] = "1",
            ["WindowSettingsMainPosX"] = "10",
            ["WindowSettingsMainPosY"] = "20",
            ["WindowSettingsMainWidth"] = "900",
            ["WindowSettingsMainHeight"] = "700",
            ["MainWindowState"] = "2",
        });
        Create().RestoreGeometry().Should().Be(new WindowGeometry(10, 20, 900, 700, WindowStateSetting.Maximized));
    }

    [Test]
    public void SaveGeometry_Normal_StoresPositionSizeAndState()
    {
        IDictionary<string, string>? saved = null;
        _store.Setup(s => s.Save(It.IsAny<IDictionary<string, string>>()))
            .Callback<IDictionary<string, string>>(v => saved = v);
        Create().SaveGeometry(5, 6, 810, 610, WindowStateSetting.Normal);
        saved.Should().NotBeNull();
        saved!["WindowSettingsMainPosX"].Should().Be("5");
        saved["WindowSettingsMainPosY"].Should().Be("6");
        saved["WindowSettingsMainWidth"].Should().Be("810");
        saved["WindowSettingsMainHeight"].Should().Be("610");
        saved["MainWindowState"].Should().Be("0");
    }

    [Test]
    public void SaveGeometry_Maximized_KeepsPreviousSizeStoresState()
    {
        IDictionary<string, string>? saved = null;
        _store.Setup(s => s.Save(It.IsAny<IDictionary<string, string>>()))
            .Callback<IDictionary<string, string>>(v => saved = v);
        Create().SaveGeometry(5, 6, 1920, 1080, WindowStateSetting.Maximized);
        saved!["WindowSettingsMainWidth"].Should().Be("800");
        saved["MainWindowState"].Should().Be("2");
    }

    [Test]
    public async Task CheckDatabase_Confirmed_ReorganizesAndReportsCount()
    {
        _management.Setup(m => m.Reorganize()).Returns(3);
        await Create().CheckDatabaseCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync(Strings.HintTitle, Strings.CheckDatabaseFixed(3)), Times.Once);
    }

    [Test]
    public async Task CheckDatabase_Declined_DoesNothing()
    {
        _dialogs = Fakes.Dialogs(confirm: false);
        await Create().CheckDatabaseCommand.ExecuteAsync(null);
        _management.Verify(m => m.Reorganize(), Times.Never);
    }

    [Test]
    public async Task SaveDatabaseAs_CopiesTheDatabaseFile()
    {
        File.WriteAllText(_dbPath, "not really a database");
        var target = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".s3db");
        _dialogs.Setup(d => d.PickSaveFileAsync(It.IsAny<string>(), "s3db", Path.GetFileName(_dbPath)))
            .ReturnsAsync(target);
        try
        {
            await Create().SaveDatabaseAsCommand.ExecuteAsync(null);
            File.ReadAllText(target).Should().Be("not really a database");
        }
        finally
        {
            File.Delete(target);
            File.Delete(_dbPath);
        }
    }

    [Test]
    public void Exit_RequestsClose()
    {
        var vm = Create();
        bool? result = null;
        vm.CloseRequested += ok => result = ok;
        vm.ExitCommand.Execute(null);
        result.Should().BeTrue();
    }

    [Test]
    public async Task ShowOptions_OpensOptionsDialog()
    {
        await Create().ShowOptionsCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowDialogAsync(It.IsAny<OptionsViewModel>()), Times.Once);
    }

    [Test]
    public async Task ShowInfo_OpensInfoDialog()
    {
        _management.Setup(m => m.LatestVersion)
            .Returns(new Properties.DBVersion(1, 7, new DateTime(2020, 1, 1), ""));
        await Create().ShowInfoCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowDialogAsync(It.IsAny<InfoViewModel>()), Times.Once);
    }

    [Test]
    public async Task ShowManagement_OpensManagementDialog()
    {
        _management.Setup(m => m.GetNextVersion()).Returns((Properties.DBVersion)null!);
        _management.Setup(m => m.GetCurrentVersion())
            .Returns(new Properties.DBVersion(1, 7, new DateTime(2020, 1, 1), ""));
        _groups.Setup(g => g.GetGroups()).Returns(new Collection<string>());
        _dictionary.Setup(d => d.DictionaryLanguages("german")).Returns(new List<string>());
        ManagementViewModel? management = null;
        _dialogs.Setup(d => d.ShowDialogAsync(It.IsAny<ManagementViewModel>()))
            .Callback<ViewModelBase>(v => management = (ManagementViewModel)v)
            .ReturnsAsync(false);
        await Create().ShowManagementCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowDialogAsync(It.IsAny<ManagementViewModel>()), Times.Once);
        management!.VersionButtonText.Should().Be("Auf aktueller Version 1.7");
    }

    [Test]
    public void ShowTab_AddsOnceSelectsAndRemovesOnClose()
    {
        var vm = Create();
        var tab = new ProbeViewModel();
        vm.ShowTab(tab);
        vm.ShowTab(tab);
        vm.Tabs.Should().ContainSingle().Which.Should().BeSameAs(tab);
        vm.SelectedTab.Should().BeSameAs(tab);
        tab.Close(false);
        vm.Tabs.Should().BeEmpty();
    }
}
