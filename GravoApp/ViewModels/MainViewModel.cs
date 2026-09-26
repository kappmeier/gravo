using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gravo;
using GravoApp.Localization;

namespace GravoApp.ViewModels;

/// <summary>
/// The main window with its menu commands and the open tabs. Supports reading and writing settings to restore the
/// state after a restart.
/// </summary>
public sealed partial class MainViewModel : ViewModelBase
{
    private const string DefaultLanguage = "Deutsch";
    private const string DatabaseExtension = "s3db";

    private readonly AppServices _s;

    [ObservableProperty] private ViewModelBase? _selectedTab;

    public MainViewModel(AppServices services)
    {
        _s = services;
        Title = "Gravo";
        Languages = new ObservableCollection<LanguageMenuItem>(
            _s.Localization.GetLanguageNames().Select(n => new LanguageMenuItem(n, SwitchLanguage)));
        if (Languages.Any(l => l.Name == DefaultLanguage))
        {
            SwitchLanguage(DefaultLanguage);
        }
    }

    public UiTexts Texts => _s.Texts;

    public ObservableCollection<ViewModelBase> Tabs { get; } = new();

    public ObservableCollection<LanguageMenuItem> Languages { get; }

    /// <summary>
    /// Shows a hint when the vocabulary database needs an update.
    /// </summary>
    public async Task CheckDatabaseVersionAsync()
    {
        if (!_s.Management.IsVersionUpToDate())
        {
            await _s.Dialogs.ShowMessageAsync(
                Texts.Get(localization.HINT), Texts.Get(localization.DB_VERSION_OUTDATED));
        }
    }

    /// <summary>
    /// Returns the stored main window geometry.
    /// </summary>
    /// <returns>The stored geometry, or <c>null</c> when <c>SaveWindowPosition</c> is off.</returns>
    public WindowGeometry? RestoreGeometry()
    {
        if (!_s.Settings.SaveWindowPosition)
        {
            return null;
        }
        var w = _s.Settings.MainWindowSettings;
        return new WindowGeometry(w.posX, w.posY, w.width, w.height, _s.Settings.MainWindowState);
    }

    /// <summary>
    /// Stores the main window state and saves the settings.
    /// </summary>
    /// <remarks>
    /// Position and size are only taken over in the normal state, so a maximized window keeps its previous size.
    /// </remarks>
    public void SaveGeometry(int x, int y, int width, int height, WindowStateSetting state)
    {
        var w = _s.Settings.MainWindowSettings;
        if (state == WindowStateSetting.Normal)
        {
            w.posX = x;
            w.posY = y;
            w.width = width;
            w.height = height;
        }
        _s.Settings.MainWindowSettings = w;
        _s.Settings.MainWindowState = state;
        _s.Settings.SaveSettings();
    }

    /// <summary>
    /// Adds a new <paramref name="tab"/> unless it is open already and selects it.
    /// </summary>
    /// <remarks>The tab is removed again when it requests to close.</remarks>
    internal void ShowTab(ViewModelBase tab)
    {
        if (!Tabs.Contains(tab))
        {
            tab.CloseRequested += _ => Tabs.Remove(tab);
            Tabs.Add(tab);
        }
        SelectedTab = tab;
    }

    [RelayCommand]
    private void SwitchLanguage(string name)
    {
        _s.Localization.SwitchToLanguage(name);
        foreach (var item in Languages)
        {
            item.IsChecked = item.Name == name;
        }
        Texts.Refresh();
    }

    [RelayCommand]
    private async Task SaveDatabaseAsAsync()
    {
        var target = await _s.Dialogs.PickSaveFileAsync(
            Texts.Get(localization.MAIN_MENU_FILE_SAVE_AS), DatabaseExtension, Path.GetFileName(_s.DbPath));
        if (target is null)
        {
            return;
        }
        try
        {
            File.Copy(_s.DbPath, target, overwrite: true);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            await _s.Dialogs.ShowMessageAsync(Strings.ErrorTitle, Strings.CopyFailed(ex.Message));
        }
    }

    [RelayCommand]
    private async Task CheckDatabaseAsync()
    {
        if (!await _s.Dialogs.ConfirmAsync(Strings.HintTitle, Strings.CheckDatabaseQuestion))
        {
            return;
        }
        var errors = _s.Management.Reorganize();
        await _s.Dialogs.ShowMessageAsync(
            Strings.HintTitle, errors > 0 ? Strings.CheckDatabaseFixed(errors) : Strings.CheckDatabaseClean);
    }

    [RelayCommand]
    private void Exit() => RequestClose(true);
}
