using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gravo;
using GravoApp.Localization;
using GravoApp.Services;

namespace GravoApp.ViewModels;

/// <summary>
/// Behavior and model of the data management dialog.
/// </summary>
/// <remarks>
/// The dialog edits groups and units, updates and checks the database, and imports and exports data. Imports copy
/// from a chosen database file into the active database, exports copy from the active database into a new file. The
/// second database is opened on its own connection by a function the caller provides.
/// </remarks>
public sealed partial class ManagementViewModel : ViewModelBase
{
    private const string DatabaseExtension = "s3db";

    private readonly VocabularyDatabase _active;
    private readonly IManagementDao _management;
    private readonly IDialogService _dialogs;
    private readonly string _dbPath;
    private readonly string _mainLanguage;
    private readonly Func<string, IDataBaseOperation> _openDatabase;
    private readonly Func<IDataBaseOperation, IManagementDao> _managementFor;
    private string _importFile = "";

    [ObservableProperty] private string? _selectedGroup;
    [ObservableProperty] private string _groupName = "";
    [ObservableProperty] private string _groupInfo = "";
    [ObservableProperty] private string? _selectedUnitGroup;
    [ObservableProperty] private string? _selectedUnit;
    [ObservableProperty] private string _unitName = "";
    [ObservableProperty] private string _unitInfo = "";
    [ObservableProperty] private string _versionButtonText = "";
    [ObservableProperty] private bool _canUpdateVersion;
    [ObservableProperty] private string _errorCountText = Strings.NoCheckYet;
    [ObservableProperty] private string _importDatabaseText = Strings.NoImportDatabase;
    [ObservableProperty] private bool _canImport;
    [ObservableProperty] private string _importDictCountText = Strings.ImportedDictionary(null, null);
    [ObservableProperty] private string _importGroupCountText = Strings.ImportedGroups(null, null, null);
    [ObservableProperty] private bool _exportSkipEmptyMains;

    /// <summary>Initializes the dialog with the <paramref name="active"/> database and loads its groups.</summary>
    /// <remarks>
    /// <paramref name="openDatabase"/> opens a database file on a new connection, <paramref name="managementFor"/>
    /// creates the management DAO for such a connection. Both are used for the second database of a transfer.
    /// </remarks>
    public ManagementViewModel(VocabularyDatabase active, IManagementDao management, IPropertiesDao properties,
        IDialogService dialogs, UiTexts texts, string dbPath, string mainLanguage,
        Func<string, IDataBaseOperation> openDatabase, Func<IDataBaseOperation, IManagementDao> managementFor)
    {
        _active = active;
        _management = management;
        _dialogs = dialogs;
        _dbPath = dbPath;
        _mainLanguage = mainLanguage;
        _openDatabase = openDatabase;
        _managementFor = managementFor;
        Texts = texts;
        Title = Strings.ManagementTitle;
        Limits = properties.LoadProperties();
        UpdateForm();
        RefreshVersionText();
    }

    public UiTexts Texts { get; }

    /// <summary>The maximum lengths of the group and unit name boxes.</summary>
    public Properties Limits { get; }

    public ObservableCollection<string> GroupNames { get; } = new();

    /// <summary>Editing and deleting is only possible when there is at least one group.</summary>
    public bool HasGroups => GroupNames.Count > 0;

    public ObservableCollection<string> Units { get; } = new();

    public ObservableCollection<CheckableItem> ExportLanguages { get; } = new();

    public ObservableCollection<CheckableItem> ExportGroups { get; } = new();

    /// <summary>Reloads all group lists and the export languages and selects the first group.</summary>
    private void UpdateForm()
    {
        SelectedGroup = null;
        SelectedUnitGroup = null;
        GroupNames.Clear();
        ExportGroups.Clear();
        foreach (var name in _active.Groups.GetGroups())
        {
            GroupNames.Add(name);
            ExportGroups.Add(new CheckableItem(name));
        }
        OnPropertyChanged(nameof(HasGroups));
        if (HasGroups)
        {
            SelectedGroup = GroupNames[0];
            SelectedUnitGroup = GroupNames[0];
        }
        else
        {
            GroupName = "";
            GroupInfo = Strings.NoGroup;
            UnitName = "";
            UnitInfo = Strings.NoGroup;
        }

        ExportLanguages.Clear();
        foreach (var language in _active.Dictionary.DictionaryLanguages(_mainLanguage))
        {
            ExportLanguages.Add(new CheckableItem(language));
        }
    }

    partial void OnSelectedGroupChanged(string? value)
    {
        if (value is null)
        {
            return;
        }
        GroupName = value;
        GroupInfo = Strings.Entries(DataTools.WordCount(_active.Groups, _active.Group, value));
    }

    partial void OnSelectedUnitGroupChanged(string? value) => LoadUnits();

    /// <summary>Lists the units of the selected group and selects the first one.</summary>
    private void LoadUnits()
    {
        SelectedUnit = null;
        Units.Clear();
        if (SelectedUnitGroup is null)
        {
            return;
        }
        foreach (var entry in _active.Groups.GetSubGroups(SelectedUnitGroup))
        {
            Units.Add(entry.SubGroup);
        }
        SelectedUnit = Units.FirstOrDefault();
    }

    partial void OnSelectedUnitChanged(string? value)
    {
        if (value is null || SelectedUnitGroup is null)
        {
            return;
        }
        UnitName = value;
        var entry = _active.Groups.GetGroup(SelectedUnitGroup, value);
        var words = _active.Group.Load(ref entry).WordCount;
        var languages = _active.Group.GetLanguages(ref entry).Count;
        UnitInfo = Strings.Entries(words) + Environment.NewLine + Strings.UsedLanguages(languages);
    }

    /// <summary>Creates a group named <see cref="GroupName"/> with a default unit.</summary>
    /// <remarks>A group cannot exist without a unit, so the first unit has to be created along with it.</remarks>
    [RelayCommand]
    private async Task AddGroupAsync()
    {
        if (GroupName.Trim().Length == 0)
        {
            return;
        }
        if (await TryAddGroupAsync(GroupName, Strings.DefaultUnit))
        {
            UpdateForm();
        }
    }

    /// <summary>Adds the unit <paramref name="unit"/> to the group <paramref name="group"/>.</summary>
    /// <remarks>In case of an unexpected error a message is shown and the form is reloaded.</remarks>
    /// <returns><c>true</c> if the unit was added.</returns>
    private async Task<bool> TryAddGroupAsync(string group, string unit)
    {
        try
        {
            _active.Groups.AddGroup(group, unit);
            return true;
        }
        catch (EntryExistsException)
        {
            await _dialogs.ShowMessageAsync(Strings.EnglishWarningTitle, Strings.GroupNameTaken);
        }
        catch (Exception ex)
        {
            await _dialogs.ShowMessageAsync(Strings.EnglishErrorTitle, Strings.ErrorOccurred(ex.Message));
            UpdateForm();
        }
        return false;
    }

    /// <summary>Renames the selected group to <see cref="GroupName"/> and selects it again.</summary>
    /// <remarks>If a group with the new name exists, the error is shown and nothing changes.</remarks>
    [RelayCommand]
    private async Task EditGroupAsync()
    {
        var group = SelectedGroup;
        var newName = GroupName;
        if (newName.Trim().Length == 0 || group is null)
        {
            return;
        }
        try
        {
            _active.Groups.EditGroup(group, newName);
        }
        catch (InputException ex)
        {
            await _dialogs.ShowMessageAsync(Strings.InvalidInputTitle, ex.Message);
            return;
        }
        catch (EntryExistsException ex)
        {
            await _dialogs.ShowMessageAsync(Strings.ErrorTitle, ex.Message);
            return;
        }
        UpdateForm();
        SelectedGroup = GroupNames.Contains(newName) ? newName : null;
    }

    /// <summary>Deletes the selected group with all its units. Deletion only starts after confirmation.</summary>
    [RelayCommand]
    private async Task DeleteGroupAsync()
    {
        var group = SelectedGroup;
        if (group is null
            || !await _dialogs.ConfirmAsync(Strings.EnglishWarningTitle, Strings.ConfirmDeleteGroup))
        {
            return;
        }
        _active.Groups.DeleteGroup(group);
        UpdateForm();
    }

    /// <summary>Adds the unit <see cref="UnitName"/> to the selected group and selects it.</summary>
    [RelayCommand]
    private async Task AddUnitAsync()
    {
        var group = SelectedUnitGroup;
        var unit = UnitName;
        if (unit.Trim().Length == 0 || group is null)
        {
            return;
        }
        if (await TryAddGroupAsync(group, unit))
        {
            Units.Add(unit);
            SelectedUnit = unit;
        }
    }

    /// <summary>Renames the selected unit to <see cref="UnitName"/> and selects it again.</summary>
    /// <remarks>If the group has a unit with the new name, an error message is shown and nothing changes.</remarks>
    [RelayCommand]
    private async Task EditUnitAsync()
    {
        var group = SelectedUnitGroup;
        var unit = SelectedUnit;
        var newName = UnitName;
        if (newName.Trim().Length == 0 || group is null || unit is null)
        {
            return;
        }
        try
        {
            _active.Groups.EditSubGroup(group, unit, newName);
        }
        catch (InputException ex)
        {
            await _dialogs.ShowMessageAsync(Strings.InvalidInputTitle, ex.Message);
            return;
        }
        catch (EntryExistsException ex)
        {
            await _dialogs.ShowMessageAsync(Strings.ErrorTitle, ex.Message);
            return;
        }
        LoadUnits();
        SelectedUnit = Units.Contains(newName) ? newName : null;
    }

    /// <summary>Moves the selected unit one position up.</summary>
    /// <remarks>Nothing happens for the first unit.</remarks>
    [RelayCommand]
    private void MoveUnitUp() => MoveUnit(-1);

    /// <summary>Moves the selected unit one position down.</summary>
    /// <remarks>Nothing happens for the last unit.</remarks>
    [RelayCommand]
    private void MoveUnitDown() => MoveUnit(1);

    private void MoveUnit(int offset)
    {
        var group = SelectedUnitGroup;
        var unit = SelectedUnit;
        if (group is null || unit is null)
        {
            return;
        }
        var other = Units.IndexOf(unit) + offset;
        if (other < 0 || other >= Units.Count)
        {
            return;
        }
        _active.Groups.SwapGroups(group, unit, Units[other]);
        LoadUnits();
        SelectedUnit = unit;
    }

    private void RefreshVersionText()
    {
        var next = _management.GetNextVersion();
        CanUpdateVersion = next is not null;
        VersionButtonText = next is null
            ? Strings.OnCurrentVersion(Format(_management.GetCurrentVersion()))
            : Strings.UpdateToVersion(Format(next));
    }

    private static string Format(Properties.DBVersion version) => version.Major + "." + version.Minor;

    /// <summary>Updates the database to the next version.</summary>
    /// <remarks>
    /// A hint is shown first when the update takes long. The app may become unresponsive during the update.
    /// </remarks>
    [RelayCommand]
    private async Task UpdateVersionAsync()
    {
        if (_management.IsUpdateComplex(_management.GetCurrentVersion()))
        {
            await _dialogs.ShowMessageAsync(Strings.HintTitle, Strings.UpdateMayTakeTime);
        }
        _management.UpdateDatabaseVersion();
        RefreshVersionText();
    }

    /// <summary>Checks the database for consistency and shows the number of fixed errors.</summary>
    [RelayCommand]
    private async Task ReorganizeAsync()
    {
        ErrorCountText = Strings.ErrorsFixed(_management.Reorganize());
        await _dialogs.ShowMessageAsync(Strings.HintTitle, Strings.ConsistencyCheckDone);
    }

    /// <summary>Copies the active database file to a chosen path.</summary>
    [RelayCommand]
    private async Task SaveDatabaseCopyAsync()
    {
        var target = await _dialogs.PickSaveFileAsync(
            Strings.SaveDatabase, DatabaseExtension, Path.GetFileName(_dbPath));
        if (target is null)
        {
            return;
        }
        try
        {
            File.Copy(_dbPath, target, overwrite: true);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            await _dialogs.ShowMessageAsync(Strings.ErrorTitle, Strings.CopyFailed(ex.Message));
        }
    }

    /// <summary>Lets the user choose the database to import from.</summary>
    /// <remarks>The import buttons are enabled only if the file can be opened.</remarks>
    [RelayCommand]
    private async Task SelectImportDatabaseAsync()
    {
        var path = await _dialogs.PickOpenFileAsync(Strings.SelectFile, DatabaseExtension);
        if (path is null)
        {
            return;
        }
        try
        {
            _openDatabase(path).Close();
        }
        catch (Exception)
        {
            ImportDatabaseText = Strings.NoImportDatabase;
            CanImport = false;
            await _dialogs.ShowMessageAsync(Strings.ErrorTitle, Strings.PickExistingFile);
            return;
        }
        ImportDatabaseText = Strings.ImportDatabase(path);
        CanImport = true;
        _importFile = path;
    }

    /// <summary>Copies all groups with their words from the chosen database into the active database.</summary>
    /// <remarks>
    /// Both databases must be up to date. An outdated source is updated after confirmation.
    /// </remarks>
    [RelayCommand]
    private async Task ImportGroupsAsync()
    {
        if (!_management.IsVersionUpToDate())
        {
            await _dialogs.ShowMessageAsync(Strings.ErrorTitle, Strings.DatabaseOutdated);
            return;
        }
        await ImportAsync(async db =>
        {
            var sourceManagement = _managementFor(db);
            if (!sourceManagement.IsVersionUpToDate())
            {
                if (!await _dialogs.ConfirmAsync(Strings.WarningTitle, Strings.ImportSourceOutdated))
                {
                    return null;
                }
                sourceManagement.UpdateDatabaseVersion();
            }
            return new DatabaseTransfer(new VocabularyDatabase(db), _active).CopyAllGroups();
        });
    }

    /// <summary>Copies all languages of the main language from the chosen database into the active database.</summary>
    [RelayCommand]
    private Task ImportDictionaryAsync() => ImportAsync(db => Task.FromResult<TransferResult?>(
        new DatabaseTransfer(new VocabularyDatabase(db), _active).CopyDictionary(_mainLanguage)));

    /// <summary>Opens the chosen database, runs <paramref name="transfer"/> on it and shows counters.</summary>
    /// <remarks>
    /// Any error while opening or copying is shown as a message. The connection is always closed. A <c>null</c> result
    /// means that the transfer was cancelled.
    /// </remarks>
    private async Task ImportAsync(Func<IDataBaseOperation, Task<TransferResult?>> transfer)
    {
        IDataBaseOperation db;
        try
        {
            db = _openDatabase(_importFile);
        }
        catch (Exception ex)
        {
            await _dialogs.ShowMessageAsync(Strings.ErrorTitle, Strings.DatabaseAccessFailed(ex.Message));
            return;
        }
        TransferResult? result;
        try
        {
            result = await transfer(db);
        }
        catch (Exception ex)
        {
            await _dialogs.ShowMessageAsync(Strings.ErrorTitle, ex.Message);
            return;
        }
        finally
        {
            db.Close();
        }
        if (result is null)
        {
            return;
        }
        ImportDictCountText = Strings.ImportedDictionary(result.MainEntries, result.SubEntries);
        ImportGroupCountText = Strings.ImportedGroups(result.Groups, result.SubGroups, result.GroupEntries);
        UpdateForm();
        await _dialogs.ShowMessageAsync(Strings.ProductName, Strings.ImportDone);
    }

    /// <summary>Writes the checked languages and groups of the active database into a new database file.</summary>
    /// <remarks>An existing file at the chosen path is replaced. The active database must be up to date.</remarks>
    [RelayCommand]
    private async Task ExportAsync()
    {
        if (!_management.IsVersionUpToDate())
        {
            await _dialogs.ShowMessageAsync(Strings.ErrorTitle, Strings.DatabaseOutdated);
            return;
        }
        var target = await _dialogs.PickSaveFileAsync(Strings.Export, DatabaseExtension, null);
        if (target is null)
        {
            return;
        }
        if (File.Exists(target))
        {
            try
            {
                File.Delete(target);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                await _dialogs.ShowMessageAsync(Strings.ErrorTitle, ex.Message);
                return;
            }
        }
        ManagementDao.CreateNewVocabularyDatabase(target);
        var db = _openDatabase(target);
        try
        {
            var export = new DatabaseTransfer(_active, new VocabularyDatabase(db));
            foreach (var language in ExportLanguages.Where(l => l.IsChecked))
            {
                export.CopyLanguage(language.Name, _mainLanguage, !ExportSkipEmptyMains);
            }
            foreach (var group in ExportGroups.Where(g => g.IsChecked))
            {
                export.CopyGroup(group.Name);
            }
        }
        catch (Exception ex)
        {
            await _dialogs.ShowMessageAsync(Strings.ErrorTitle, ex.Message);
            return;
        }
        finally
        {
            db.Close();
        }
        await _dialogs.ShowMessageAsync(Strings.ProductName, Strings.ExportDone);
    }

    [RelayCommand]
    private void Close() => RequestClose(false);
}
