using CommunityToolkit.Mvvm.Input;
using Gravo;
using GravoApp.Localization;

namespace GravoApp.ViewModels;

/// <summary>
/// Behavior and model for the info dialog showing the product name, version and copyright. The disclaimer and the old
/// copyright line are <see cref="UiTexts">localized</see>.
/// </summary>
public sealed partial class InfoViewModel : ViewModelBase
{
    public InfoViewModel(IManagementDao management, UiTexts texts)
    {
        Texts = texts;
        Title = texts.Get(localization.INFO_TITLE);
        VersionText = texts.Format(localization.INFO_VERSION, FormatVersion(typeof(App).Assembly.GetName().Version));
        var latest = management.LatestVersion;
        DbVersionText = texts.Format(localization.INFO_DB_VERSION, latest.Major + "." + latest.Minor);
    }

    public UiTexts Texts { get; }

    public string VersionText { get; }

    public string DbVersionText { get; }

    private static string FormatVersion(Version? version)
    {
        var v = version ?? new Version(0, 0, 0);
        return v.Major + "." + v.Minor + "." + v.Build;
    }

    [RelayCommand]
    private void Close() => RequestClose(false);
}
