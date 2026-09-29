using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gravo;
using GravoApp.Localization;

namespace GravoApp.ViewModels;

/// <summary>The "Info" dialog showing the product name, version, and copyright information.</summary>
public sealed partial class InfoViewModel : ViewModelBase
{
    [ObservableProperty] private bool _german;

    public InfoViewModel(IManagementDao management, UiTexts texts)
    {
        Texts = texts;
        Title = Strings.InfoTitle;
        VersionText = Strings.InfoVersion(FormatVersion(typeof(App).Assembly.GetName().Version));
        var latest = management.LatestVersion;
        DbVersionText = Strings.InfoDbVersion(latest.Major + "." + latest.Minor);
        Disclaimer = Strings.InfoDisclaimerEnglish;
        CopyrightOld = Strings.InfoCopyrightOldEnglish;
    }

    public UiTexts Texts { get; }

    public string VersionText { get; }

    public string DbVersionText { get; }

    /// <summary>
    /// The disclaimer text. Starts as the WinForms load-time English text; once <see cref="German"/> is set,
    /// in either direction, it switches to the German text or the toggled-back English text.
    /// </summary>
    public string Disclaimer { get; private set; }

    /// <summary>The old copyright line, switched together with <see cref="Disclaimer"/>.</summary>
    public string CopyrightOld { get; private set; }

    partial void OnGermanChanged(bool value)
    {
        Disclaimer = value ? Strings.InfoDisclaimerGerman : Strings.InfoDisclaimerEnglish;
        CopyrightOld = value ? Strings.InfoCopyrightOldGerman : Strings.InfoCopyrightOldEnglish;
        OnPropertyChanged(nameof(Disclaimer));
        OnPropertyChanged(nameof(CopyrightOld));
    }

    private static string FormatVersion(Version? version)
    {
        var v = version ?? new Version(0, 0, 0);
        return v.Major + "." + v.Minor + "." + v.Build;
    }

    [RelayCommand]
    private void Close() => RequestClose(false);
}
