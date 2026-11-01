using System.IO;
using AwesomeAssertions;
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
        IManagementDao? management = null, Func<IDataBaseOperation, IManagementDao>? managementFor = null,
        UiTexts? texts = null) => new(
        _active.Vocabulary, management ?? _active.Management, _active.Properties, _dialogs.Object,
        texts ?? Fakes.Texts(), _active.FilePath, TempVocabulary.MainLanguage, SQLiteDataBaseOperation.OpenDatabase,
        managementFor ?? DaoFactory.Management);

    /// <summary>Returns a path in the temp directory that does not exist yet and is deleted after the test.</summary>
    private string TempPath()
    {
        var path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".s3db");
        _files.Add(path);
        return path;
    }

    private void PickForImport(string path) =>
        _dialogs.Setup(d => d.PickOpenFileAsync("T180", "s3db")).ReturnsAsync(path);

    private static Mock<IManagementDao> Outdated()
    {
        var management = new Mock<IManagementDao>();
        management.Setup(m => m.IsSchemaVersionUpToDate()).Returns(false);
        management.Setup(m => m.GetCurrentSchemaVersion())
            .Returns(new Properties.DbSchemaVersion(1, 6, new DateTime(2019, 1, 1), ""));
        management.Setup(m => m.GetNextSchemaVersion())
            .Returns(new Properties.DbSchemaVersion(1, 7, new DateTime(2020, 1, 1), ""));
        return management;
    }

    [Test]
    public void Constructor_ListsGroupsUnitsLanguagesAndVersion()
    {
        var fixture = Create();
        fixture.Title.Should().Be("T172");
        fixture.GroupNames.Should().Equal("Book", "Other");
        fixture.HasGroups.Should().BeTrue();
        fixture.SelectedGroup.Should().Be("Book");
        fixture.GroupName.Should().Be("Book");
        fixture.GroupInfo.Should().Be("2 T109");
        fixture.SelectedUnitGroup.Should().Be("Book");
        fixture.Units.Should().Equal("Unit 1", "Unit 2");
        fixture.SelectedUnit.Should().Be("Unit 1");
        fixture.UnitName.Should().Be("Unit 1");
        fixture.UnitInfo.Should().Be("2 T109" + NL + "T186");
        fixture.ExportLanguages.Select(l => l.Name).Should().Equal("english", "french");
        fixture.ExportGroups.Select(g => g.Name).Should().Equal("Book", "Other");
        fixture.ExportLanguages.Concat(fixture.ExportGroups).Should().OnlyContain(i => !i.IsChecked);
        fixture.ExportSkipEmptyMains.Should().BeFalse();
        fixture.VersionButtonText.Should().Be("T206");
        fixture.CanUpdateVersion.Should().BeFalse();
        fixture.ErrorCountText.Should().Be("T192");
        fixture.ImportDatabaseText.Should().Be("T195");
        fixture.CanImport.Should().BeFalse();
        fixture.ImportDictCountText.Should().Be("T201");
        fixture.ImportGroupCountText.Should().Be("T202");
    }

    [Test]
    public void SelectGroup_WithOneEntry_UsesSingular()
    {
        var fixture = Create();
        fixture.SelectedGroup = "Other";
        fixture.GroupName.Should().Be("Other");
        fixture.GroupInfo.Should().Be("1 T110");
    }

    [Test]
    public void SelectUnitGroup_ListsItsUnits()
    {
        var fixture = Create();
        fixture.SelectedUnitGroup = "Other";
        fixture.Units.Should().Equal("Words");
        fixture.SelectedUnit.Should().Be("Words");
        fixture.UnitInfo.Should().Be("1 T110" + NL + "T186");
    }

    [Test]
    public async Task AddGroup_CreatesGroupWithDefaultUnit()
    {
        var fixture = Create();
        fixture.GroupName = "New";
        await fixture.AddGroupCommand.ExecuteAsync(null);
        _active.Groups.GetSubGroups("New").Single().SubGroup.Should().Be("T188");
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
            .Should().Equal("Unit 1", "Unit 2", "T188");
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
        _dialogs.Verify(d => d.ShowMessageAsync("T118", "T189"), Times.Once);
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
        _dialogs.Verify(d => d.ShowMessageAsync("T104", "A group with name Other already exists."),
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
        _dialogs.Verify(d => d.ConfirmAsync("T118", "T191"), Times.Once);
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
        fixture.GroupInfo.Should().Be("T185");
        fixture.SelectedUnitGroup.Should().BeNull();
        fixture.Units.Should().BeEmpty();
        fixture.UnitName.Should().BeEmpty();
        fixture.UnitInfo.Should().Be("T185");
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

        // The sub groups keep their index order.
        fixture.Units.Should().Equal("Unit 1", "Lektion 2");
        fixture.SelectedUnit.Should().Be("Lektion 2");
        _active.Groups.GetSubGroups("Book").Select(g => g.SubGroup).Should().Equal("Unit 1", "Lektion 2");
    }

    [Test]
    public async Task EditUnit_ExistingName_ShowsErrorAndKeepsList()
    {
        var fixture = Create();
        fixture.SelectedUnit = "Unit 2";
        fixture.UnitName = "Unit 1";
        await fixture.EditUnitCommand.ExecuteAsync(null);
        _dialogs.Verify(
            d => d.ShowMessageAsync("T104", "Sub group Unit 1 for group Book exists."), Times.Once);
        fixture.Units.Should().Equal("Unit 1", "Unit 2");
        fixture.SelectedUnit.Should().Be("Unit 2");
        _active.Groups.GetSubGroups("Book").Select(g => g.SubGroup).Should().Equal("Unit 1", "Unit 2");
    }

    [Test]
    public void MoveUnitDown_SwapsIndexAndListOrder()
    {
        var fixture = Create();
        fixture.SelectedUnit = "Unit 1";
        fixture.MoveUnitDownCommand.Execute(null);

        fixture.Units.Should().Equal("Unit 2", "Unit 1");
        fixture.SelectedUnit.Should().Be("Unit 1");
        fixture.UnitInfo.Should().Be("2 T109" + NL + "T186");
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
    public void MoveUnitUp_SecondUnit_SwapsIndexAndListOrder()
    {
        var fixture = Create();
        fixture.SelectedUnit = "Unit 2";
        fixture.MoveUnitUpCommand.Execute(null);

        fixture.Units.Should().Equal("Unit 2", "Unit 1");
        fixture.SelectedUnit.Should().Be("Unit 2");
        BookUnitsByIndex().Should().Equal("Unit 2", "Unit 1");
    }

    /// <summary>Returns the units of the group Book in the order of their database index.</summary>
    private IEnumerable<string> BookUnitsByIndex() =>
        _active.Groups.
            GetAllGroups().
            Where(g => g.Name == "Book").
            OrderBy(g => g.Index).
            Select(g => g.SubGroup);

    [Test]
    public async Task Reorganize_ReportsCountAndMessage()
    {
        var fixture = Create();
        await fixture.ReorganizeCommand.ExecuteAsync(null);
        fixture.ErrorCountText.Should().Be("T193");
        _dialogs.Verify(d => d.ShowMessageAsync("T11", "T194"), Times.Once);
    }

    [Test]
    public void Constructor_OutdatedDatabase_OffersUpdateToNextVersion()
    {
        var fixture = Create(management: Outdated().Object);
        fixture.VersionButtonText.Should().Be("T207");
        fixture.CanUpdateVersion.Should().BeTrue();
    }

    [Test]
    public async Task UpdateVersion_Complex_ShowsHintAndUpdates()
    {
        var management = Outdated();
        management.Setup(m => m.IsSchemaUpdateComplex(It.IsAny<Properties.DbSchemaVersion>())).Returns(true);
        var fixture = Create(management: management.Object);
        await fixture.UpdateVersionCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync("T11", "T205"), Times.Once);
        management.Verify(m => m.UpdateDatabaseSchemaVersion(), Times.Once);
    }

    [Test]
    public async Task SaveDatabaseCopy_CopiesActiveFile()
    {
        var target = TempPath();
        _dialogs.Setup(d => d.PickSaveFileAsync("T178", "s3db", Path.GetFileName(_active.FilePath)))
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
        fixture.ImportDatabaseText.Should().Be("T196");
    }

    [Test]
    public async Task SelectImportDatabase_UnopenableFile_ShowsMessage()
    {
        PickForImport(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName(), "missing", "x.s3db"));
        var fixture = Create();
        await fixture.SelectImportDatabaseCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync("T104", "T197"), Times.Once);
        fixture.CanImport.Should().BeFalse();
        fixture.ImportDatabaseText.Should().Be("T195");
    }

    [Test]
    public async Task ImportGroups_CopiesGroupsAndShowsDone()
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
        _dialogs.Verify(d => d.ShowMessageAsync("Gravo", "T203"), Times.Once);
    }

    [Test]
    public async Task ImportGroups_RealTexts_ShowsTheCounts()
    {
        using var source = new TempVocabulary();
        var casa = source.AddWord("casa", "latin", "casa", "Haus");
        var casae = source.AddWord("casa", "latin", "casae", "Häuser");
        var l1 = source.AddGroup("Lat", "L1");
        source.AddToGroup(l1, casa);
        source.AddToGroup(l1, casae);
        source.AddToGroup(source.AddGroup("Lat", "L2"), casa);

        _dialogs.Setup(d => d.PickOpenFileAsync(It.IsAny<string>(), "s3db")).ReturnsAsync(source.FilePath);

        using var real = new RealLocalization();
        var fixture = Create(texts: real.Texts);

        await fixture.SelectImportDatabaseCommand.ExecuteAsync(null);
        await fixture.ImportGroupsCommand.ExecuteAsync(null);

        fixture.ImportDictCountText.Should().Be("Importierte Haupteinträge: 1\nImportierte Untereinträge: 2");
        fixture.ImportGroupCountText.Should()
            .Be("Importierte Gruppen: 1\nImportierte Untergruppen: 2\nImportierte Gruppeneinträge: 3");
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
        _dialogs.Verify(d => d.ShowMessageAsync("T104", "T198"), Times.Once);
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
        _dialogs.Verify(d => d.ConfirmAsync("T118", "T200"), Times.Once);
        outdated.Verify(m => m.UpdateDatabaseSchemaVersion(), Times.Never);
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
        fixture.ImportDictCountText.Should().Be("T201");
        _dialogs.Verify(d => d.ShowMessageAsync("Gravo", "T203"), Times.Once);
    }

    [Test]
    public async Task Export_WritesCheckedLanguageAndGroup()
    {
        var target = TempPath();
        File.WriteAllText(target, "an old file in the way");
        _dialogs.Setup(d => d.PickSaveFileAsync("T176", "s3db", null)).ReturnsAsync(target);
        var fixture = Create();
        fixture.ExportLanguages.Single(l => l.Name == "english").IsChecked = true;
        fixture.ExportGroups.Single(g => g.Name == "Book").IsChecked = true;
        await fixture.ExportCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync("Gravo", "T204"), Times.Once);
        using var db = SQLiteDataBaseOperation.OpenDatabase(target);
        DaoFactory.Dictionary(db).DictionaryLanguages("german").Should().Equal("english");
        DaoFactory.Groups(db).GetGroups().Should().Equal("Book");
    }

    [Test]
    public async Task Export_OutdatedActiveDatabase_ShowsMessageAndStops()
    {
        var fixture = Create(management: Outdated().Object);
        await fixture.ExportCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync("T104", "T198"), Times.Once);
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
