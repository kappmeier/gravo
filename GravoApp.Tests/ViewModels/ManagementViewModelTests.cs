using System.IO;
using FluentAssertions;
using Gravo;
using GravoApp.Localization;
using GravoApp.Services;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using Microsoft.Data.Sqlite;
using Moq;
using NUnit.Framework;

namespace GravoApp.Tests.ViewModels;

public class ManagementViewModelTests
{
    private static readonly string NL = Environment.NewLine;

    private TempVocabulary _active = null!;
    private Mock<IDialogService> _dialogs = null!;
    private readonly List<string> _files = new();

    [SetUp]
    public void SetUp()
    {
        _active = new TempVocabulary();
        _active.SeedStandard();
        _dialogs = Fakes.Dialogs();
    }

    [TearDown]
    public void TearDown()
    {
        _active.Dispose();
        SqliteConnection.ClearAllPools();
        foreach (var file in _files)
        {
            File.Delete(file);
        }
        _files.Clear();
    }

    private ManagementViewModel Create(
        IManagementDao? management = null, Func<IDataBaseOperation, IManagementDao>? managementFor = null) => new(
        _active.Vocabulary, management ?? _active.Management, _active.Properties, _dialogs.Object, Fakes.Texts(),
        _active.FilePath, TempVocabulary.MainLanguage, OpenDatabase, managementFor ?? CoreFactory.Management);

    private static IDataBaseOperation OpenDatabase(string path)
    {
        IDataBaseOperation db = new SQLiteDataBaseOperation();
        db.Open(path);
        return db;
    }

    /// <summary>Returns a path in the temp directory that does not exist yet and is deleted after the test.</summary>
    private string TempPath()
    {
        var path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".s3db");
        _files.Add(path);
        return path;
    }

    private void PickForImport(string path) =>
        _dialogs.Setup(d => d.PickOpenFileAsync(Strings.SelectFile, "s3db")).ReturnsAsync(path);

    private static Mock<IManagementDao> Outdated()
    {
        var management = new Mock<IManagementDao>();
        management.Setup(m => m.IsVersionUpToDate()).Returns(false);
        management.Setup(m => m.GetCurrentVersion())
            .Returns(new Properties.DBVersion(1, 6, new DateTime(2019, 1, 1), ""));
        management.Setup(m => m.GetNextVersion())
            .Returns(new Properties.DBVersion(1, 7, new DateTime(2020, 1, 1), ""));
        return management;
    }

    [Test]
    public void Constructor_ListsGroupsUnitsLanguagesAndVersion()
    {
        var fixture = Create();
        fixture.Title.Should().Be("Daten-Management");
        fixture.GroupNames.Should().Equal("Book", "Other");
        fixture.HasGroups.Should().BeTrue();
        fixture.SelectedGroup.Should().Be("Book");
        fixture.GroupName.Should().Be("Book");
        fixture.GroupInfo.Should().Be("2 Einträge");
        fixture.SelectedUnitGroup.Should().Be("Book");
        fixture.Units.Should().Equal("Unit 1", "Unit 2");
        fixture.SelectedUnit.Should().Be("Unit 1");
        fixture.UnitName.Should().Be("Unit 1");
        fixture.UnitInfo.Should().Be("2 Einträge" + NL + "1 benutzte Sprache");
        fixture.ExportLanguages.Select(l => l.Name).Should().Equal("english", "french");
        fixture.ExportGroups.Select(g => g.Name).Should().Equal("Book", "Other");
        fixture.ExportLanguages.Concat(fixture.ExportGroups).Should().OnlyContain(i => !i.IsChecked);
        fixture.ExportSkipEmptyMains.Should().BeFalse();
        fixture.VersionButtonText.Should().StartWith("Auf aktueller Version 1.");
        fixture.CanUpdateVersion.Should().BeFalse();
        fixture.ErrorCountText.Should().Be("Gefundene und behobene Fehler: keine Überprüfung durchgeführt");
        fixture.ImportDatabaseText.Should().Be("Datenbank: noch keine gewählt");
        fixture.CanImport.Should().BeFalse();
        fixture.ImportDictCountText.Should().Be("Importierte Haupteinträge: " + NL + "Importierte Untereinträge: ");
        fixture.ImportGroupCountText.Should().Be(
            "Importierte Gruppen: " + NL + "Importierte Untergruppen: " + NL + "Importierte Gruppeneinträge: ");
    }

    [Test]
    public void SelectGroup_WithOneEntry_UsesSingular()
    {
        var fixture = Create();
        fixture.SelectedGroup = "Other";
        fixture.GroupName.Should().Be("Other");
        fixture.GroupInfo.Should().Be("1 Eintrag");
    }

    [Test]
    public void SelectUnitGroup_ListsItsUnits()
    {
        var fixture = Create();
        fixture.SelectedUnitGroup = "Other";
        fixture.Units.Should().Equal("Words");
        fixture.SelectedUnit.Should().Be("Words");
        fixture.UnitInfo.Should().Be("1 Eintrag" + NL + "1 benutzte Sprache");
    }

    [Test]
    public async Task AddGroup_CreatesGroupWithDefaultUnit()
    {
        var fixture = Create();
        fixture.GroupName = "New";
        await fixture.AddGroupCommand.ExecuteAsync(null);
        _active.Groups.GetSubGroups("New").Single().SubGroup.Should().Be("Untereintrag 1");
        fixture.GroupNames.Should().Contain("New");
        fixture.ExportGroups.Select(g => g.Name).Should().Contain("New");
    }

    [Test]
    public async Task AddGroup_ExistingGroup_AddsDefaultUnitToIt()
    {
        var fixture = Create();
        fixture.GroupName = "Book";
        await fixture.AddGroupCommand.ExecuteAsync(null);
        _active.Groups.GetSubGroups("Book").Select(g => g.SubGroup)
            .Should().Equal("Unit 1", "Unit 2", "Untereintrag 1");
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task AddGroup_DefaultUnitExists_ShowsWarning()
    {
        var fixture = Create();
        fixture.GroupName = "New";
        await fixture.AddGroupCommand.ExecuteAsync(null);
        fixture.GroupName = "New";
        await fixture.AddGroupCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync("Warning", Strings.GroupNameTaken), Times.Once);
        _active.Groups.GetSubGroups("New").Should().ContainSingle();
    }

    [Test]
    public async Task AddGroup_EmptyName_DoesNothing()
    {
        var fixture = Create();
        fixture.GroupName = "  ";
        await fixture.AddGroupCommand.ExecuteAsync(null);
        _active.Groups.GetGroups().Should().Equal("Book", "Other");
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task EditGroup_RenamesAndReselects()
    {
        var fixture = Create();
        fixture.SelectedGroup = "Book";
        fixture.GroupName = "Buch";
        await fixture.EditGroupCommand.ExecuteAsync(null);
        fixture.GroupNames.Should().Equal("Buch", "Other");
        fixture.SelectedGroup.Should().Be("Buch");
        _active.Groups.GetSubGroups("Buch").Should().HaveCount(2);
    }

    [Test]
    public async Task EditGroup_ExistingName_ShowsErrorAndKeepsList()
    {
        var fixture = Create();
        fixture.SelectedGroup = "Book";
        fixture.GroupName = "Other";
        await fixture.EditGroupCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync(Strings.ErrorTitle, "A group with name Other already exists."),
            Times.Once);
        fixture.GroupNames.Should().Equal("Book", "Other");
        _active.Groups.GetSubGroups("Book").Should().HaveCount(2);
    }

    [Test]
    public async Task DeleteGroup_Confirmed_Removes()
    {
        var fixture = Create();
        fixture.SelectedGroup = "Other";
        await fixture.DeleteGroupCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ConfirmAsync("Warning", Strings.ConfirmDeleteGroup), Times.Once);
        _active.Groups.GetGroups().Should().Equal("Book");
        fixture.GroupNames.Should().Equal("Book");
        fixture.SelectedGroup.Should().Be("Book");
    }

    [Test]
    public async Task DeleteGroup_Declined_Keeps()
    {
        _dialogs = Fakes.Dialogs(confirm: false);
        var fixture = Create();
        fixture.SelectedGroup = "Other";
        await fixture.DeleteGroupCommand.ExecuteAsync(null);
        _active.Groups.GetGroups().Should().Equal("Book", "Other");
        fixture.SelectedGroup.Should().Be("Other");
    }

    [Test]
    public async Task DeleteGroup_LastGroup_ShowsNoGroup()
    {
        var fixture = Create();
        fixture.SelectedGroup = "Book";
        await fixture.DeleteGroupCommand.ExecuteAsync(null);
        fixture.SelectedGroup = "Other";
        await fixture.DeleteGroupCommand.ExecuteAsync(null);
        fixture.GroupNames.Should().BeEmpty();
        fixture.HasGroups.Should().BeFalse();
        fixture.SelectedGroup.Should().BeNull();
        fixture.GroupName.Should().BeEmpty();
        fixture.GroupInfo.Should().Be("Keine Gruppe vorhanden");
        fixture.SelectedUnitGroup.Should().BeNull();
        fixture.Units.Should().BeEmpty();
        fixture.UnitName.Should().BeEmpty();
        fixture.UnitInfo.Should().Be("Keine Gruppe vorhanden");
    }

    [Test]
    public async Task AddUnit_AddsAndSelects()
    {
        var fixture = Create();
        fixture.UnitName = "Unit 3";
        await fixture.AddUnitCommand.ExecuteAsync(null);
        fixture.Units.Should().Equal("Unit 1", "Unit 2", "Unit 3");
        fixture.SelectedUnit.Should().Be("Unit 3");
        _active.Groups.GetSubGroups("Book").Select(g => g.SubGroup).Should().Equal("Unit 1", "Unit 2", "Unit 3");
    }

    [Test]
    public async Task EditUnit_Renames()
    {
        var fixture = Create();
        fixture.SelectedUnit = "Unit 2";
        fixture.UnitName = "Lektion 2";
        await fixture.EditUnitCommand.ExecuteAsync(null);
        // The sub groups of a group come back sorted by name.
        fixture.Units.Should().Equal("Lektion 2", "Unit 1");
        fixture.SelectedUnit.Should().Be("Lektion 2");
        _active.Groups.GetSubGroups("Book").Select(g => g.SubGroup).Should().Equal("Lektion 2", "Unit 1");
    }

    [Test]
    public async Task EditUnit_ExistingName_ShowsErrorAndKeepsList()
    {
        var fixture = Create();
        fixture.SelectedUnit = "Unit 2";
        fixture.UnitName = "Unit 1";
        await fixture.EditUnitCommand.ExecuteAsync(null);
        _dialogs.Verify(
            d => d.ShowMessageAsync(Strings.ErrorTitle, "Sub group Unit 1 for group Book exists."), Times.Once);
        fixture.Units.Should().Equal("Unit 1", "Unit 2");
        fixture.SelectedUnit.Should().Be("Unit 2");
        _active.Groups.GetSubGroups("Book").Select(g => g.SubGroup).Should().Equal("Unit 1", "Unit 2");
    }

    [Test]
    public void MoveUnitDown_SwapsIndexListStaysSortedByName()
    {
        var fixture = Create();
        fixture.SelectedUnit = "Unit 1";
        fixture.MoveUnitDownCommand.Execute(null);
        fixture.Units.Should().Equal("Unit 1", "Unit 2");
        fixture.SelectedUnit.Should().Be("Unit 1");
        fixture.UnitInfo.Should().Be("2 Einträge" + NL + "1 benutzte Sprache");
        BookUnitsByIndex().Should().Equal("Unit 2", "Unit 1");
    }

    [Test]
    public void MoveUnitUp_FirstUnit_DoesNothing()
    {
        var fixture = Create();
        fixture.SelectedUnit = "Unit 1";
        fixture.MoveUnitUpCommand.Execute(null);
        fixture.Units.Should().Equal("Unit 1", "Unit 2");
        fixture.SelectedUnit.Should().Be("Unit 1");
        BookUnitsByIndex().Should().Equal("Unit 1", "Unit 2");
    }

    [Test]
    public void MoveUnitUp_SecondUnit_SwapsIndexListStaysSortedByName()
    {
        var fixture = Create();
        fixture.SelectedUnit = "Unit 2";
        fixture.MoveUnitUpCommand.Execute(null);
        fixture.Units.Should().Equal("Unit 1", "Unit 2");
        fixture.SelectedUnit.Should().Be("Unit 2");
        BookUnitsByIndex().Should().Equal("Unit 2", "Unit 1");
    }

    /// <summary>Returns the units of the group Book in the order of their database index.</summary>
    private IEnumerable<string> BookUnitsByIndex() =>
        _active.Groups.GetAllGroups().Where(g => g.Name == "Book").Select(g => g.SubGroup);

    [Test]
    public async Task Reorganize_ReportsCountAndMessage()
    {
        var fixture = Create();
        await fixture.ReorganizeCommand.ExecuteAsync(null);
        fixture.ErrorCountText.Should().Be("Gefundene und behobene Fehler: 0");
        _dialogs.Verify(d => d.ShowMessageAsync(Strings.HintTitle, Strings.ConsistencyCheckDone), Times.Once);
    }

    [Test]
    public void Constructor_OutdatedDatabase_OffersUpdateToNextVersion()
    {
        var fixture = Create(management: Outdated().Object);
        fixture.VersionButtonText.Should().Be("Update auf Version 1.7");
        fixture.CanUpdateVersion.Should().BeTrue();
    }

    [Test]
    public async Task UpdateVersion_Complex_ShowsHintAndUpdates()
    {
        var management = Outdated();
        management.Setup(m => m.IsUpdateComplex(It.IsAny<Properties.DBVersion>())).Returns(true);
        var fixture = Create(management: management.Object);
        await fixture.UpdateVersionCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync(Strings.HintTitle, Strings.UpdateMayTakeTime), Times.Once);
        management.Verify(m => m.UpdateDatabaseVersion(), Times.Once);
    }

    [Test]
    public async Task SaveDatabaseCopy_CopiesActiveFile()
    {
        var target = TempPath();
        _dialogs.Setup(d => d.PickSaveFileAsync(Strings.SaveDatabase, "s3db", Path.GetFileName(_active.FilePath)))
            .ReturnsAsync(target);
        await Create().SaveDatabaseCopyCommand.ExecuteAsync(null);
        File.Exists(target).Should().BeTrue();
        new FileInfo(target).Length.Should().Be(new FileInfo(_active.FilePath).Length);
    }

    [Test]
    public async Task SelectImportDatabase_ExistingDatabase_EnablesImport()
    {
        using var source = new TempVocabulary();
        PickForImport(source.FilePath);
        var fixture = Create();
        await fixture.SelectImportDatabaseCommand.ExecuteAsync(null);
        fixture.CanImport.Should().BeTrue();
        fixture.ImportDatabaseText.Should().Be("Datenbank: " + source.FilePath);
    }

    [Test]
    public async Task SelectImportDatabase_UnopenableFile_ShowsMessage()
    {
        PickForImport(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName(), "missing", "x.s3db"));
        var fixture = Create();
        await fixture.SelectImportDatabaseCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync(Strings.ErrorTitle, Strings.PickExistingFile), Times.Once);
        fixture.CanImport.Should().BeFalse();
        fixture.ImportDatabaseText.Should().Be("Datenbank: noch keine gewählt");
    }

    [Test]
    public async Task ImportGroups_CopiesGroupsAndShowsCounters()
    {
        using var source = new TempVocabulary();
        var casa = source.AddWord("casa", "latin", "casa", "Haus");
        source.AddToGroup(source.AddGroup("Lat", "L1"), casa);
        PickForImport(source.FilePath);
        var fixture = Create();
        await fixture.SelectImportDatabaseCommand.ExecuteAsync(null);
        await fixture.ImportGroupsCommand.ExecuteAsync(null);
        _active.Groups.GetGroups().Should().Contain("Lat");
        fixture.GroupNames.Should().Contain("Lat");
        fixture.ImportGroupCountText.Should().Be(
            "Importierte Gruppen: 1" + NL + "Importierte Untergruppen: 1" + NL + "Importierte Gruppeneinträge: 1");
        fixture.ImportDictCountText.Should().Be("Importierte Haupteinträge: 1" + NL + "Importierte Untereinträge: 1");
        _dialogs.Verify(d => d.ShowMessageAsync("Gravo", Strings.ImportDone), Times.Once);
    }

    [Test]
    public async Task ImportGroups_OutdatedActiveDatabase_ShowsMessageAndStops()
    {
        using var source = new TempVocabulary();
        source.AddGroup("Lat", "L1");
        PickForImport(source.FilePath);
        var fixture = Create(management: Outdated().Object);
        await fixture.SelectImportDatabaseCommand.ExecuteAsync(null);
        await fixture.ImportGroupsCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync(Strings.ErrorTitle, Strings.DatabaseOutdated), Times.Once);
        _active.Groups.GetGroups().Should().Equal("Book", "Other");
    }

    [Test]
    public async Task ImportGroups_OutdatedSource_Declined_Stops()
    {
        using var source = new TempVocabulary();
        source.AddGroup("Lat", "L1");
        _dialogs = Fakes.Dialogs(confirm: false);
        PickForImport(source.FilePath);
        var outdated = Outdated();
        var fixture = Create(managementFor: _ => outdated.Object);
        await fixture.SelectImportDatabaseCommand.ExecuteAsync(null);
        await fixture.ImportGroupsCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ConfirmAsync(Strings.WarningTitle, Strings.ImportSourceOutdated), Times.Once);
        outdated.Verify(m => m.UpdateDatabaseVersion(), Times.Never);
        _active.Groups.GetGroups().Should().Equal("Book", "Other");
    }

    [Test]
    public async Task ImportDictionary_CopiesWords()
    {
        using var source = new TempVocabulary();
        source.AddWord("casa", "latin", "casa", "Haus");
        source.AddWord("aqua", "latin", "aqua", "Wasser");
        PickForImport(source.FilePath);
        var fixture = Create();
        await fixture.SelectImportDatabaseCommand.ExecuteAsync(null);
        await fixture.ImportDictionaryCommand.ExecuteAsync(null);
        _active.Dictionary.GetWords("latin", "german").Should().HaveCount(2);
        fixture.ExportLanguages.Select(l => l.Name).Should().Equal("english", "french", "latin");
        fixture.ImportDictCountText.Should().Be("Importierte Haupteinträge: 2" + NL + "Importierte Untereinträge: 2");
        _dialogs.Verify(d => d.ShowMessageAsync("Gravo", Strings.ImportDone), Times.Once);
    }

    [Test]
    public async Task Export_WritesCheckedLanguageAndGroup()
    {
        var target = TempPath();
        File.WriteAllText(target, "an old file in the way");
        _dialogs.Setup(d => d.PickSaveFileAsync(Strings.Export, "s3db", null)).ReturnsAsync(target);
        var fixture = Create();
        fixture.ExportLanguages.Single(l => l.Name == "english").IsChecked = true;
        fixture.ExportGroups.Single(g => g.Name == "Book").IsChecked = true;
        await fixture.ExportCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync("Gravo", Strings.ExportDone), Times.Once);
        var db = OpenDatabase(target);
        try
        {
            CoreFactory.Dictionary(db).DictionaryLanguages("german").Should().Equal("english");
            CoreFactory.Groups(db).GetGroups().Should().Equal("Book");
        }
        finally
        {
            db.Close();
        }
    }

    [Test]
    public async Task Export_OutdatedActiveDatabase_ShowsMessageAndStops()
    {
        var fixture = Create(management: Outdated().Object);
        await fixture.ExportCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync(Strings.ErrorTitle, Strings.DatabaseOutdated), Times.Once);
        _dialogs.Verify(
            d => d.PickSaveFileAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string?>()), Times.Never);
    }

    [Test]
    public void Close_RequestsCloseWithoutResult()
    {
        var fixture = Create();
        bool? result = null;
        fixture.CloseRequested += ok => result = ok;
        fixture.CloseCommand.Execute(null);
        result.Should().BeFalse();
    }
}
