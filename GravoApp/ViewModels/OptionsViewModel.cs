using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gravo;
using GravoApp.Localization;
using GravoApp.Services;

namespace GravoApp.ViewModels;

/// <summary>The "Options" dialog for the test direction, display, and card-strategy settings.</summary>
public sealed partial class OptionsViewModel : ViewModelBase
{
    private readonly Settings _settings;
    private readonly IManagementDao _management;
    private readonly IDialogService _dialogs;

    [ObservableProperty] private bool _testTargetLanguage;
    [ObservableProperty] private bool _testSetPhrases;
    [ObservableProperty] private bool _saveWindowPosition;
    [ObservableProperty] private bool _useCards;
    [ObservableProperty] private int _cardsInitialInterval;

    public OptionsViewModel(Settings settings, IManagementDao management, IDialogService dialogs, UiTexts texts)
    {
        _settings = settings;
        _management = management;
        _dialogs = dialogs;
        Texts = texts;
        Title = Strings.OptionsTitle;
        TestTargetLanguage = settings.QueryLanguage == QueryLanguage.TargetLanguage;
        TestSetPhrases = settings.TestSetPhrases;
        SaveWindowPosition = settings.SaveWindowPosition;
        UseCards = settings.UseCards;
        CardsInitialInterval = settings.CardsInitialInterval;
    }

    public UiTexts Texts { get; }

    /// <summary>Doubles the card interval, clamped to the maximum of <c>1024</c> equalling 10.</summary>
    [RelayCommand]
    private void IncreaseInterval() => CardsInitialInterval = Math.Min(1024, CardsInitialInterval * 2);

    /// <summary>Halves the card interval, clamped to the minimum of <c>1</c>.</summary>
    [RelayCommand]
    private void DecreaseInterval() => CardsInitialInterval = Math.Max(1, CardsInitialInterval / 2);

    [RelayCommand]
    private void Ok()
    {
        _settings.QueryLanguage = TestTargetLanguage ? QueryLanguage.TargetLanguage : QueryLanguage.OriginalLanguage;
        _settings.TestSetPhrases = TestSetPhrases;
        _settings.SaveWindowPosition = SaveWindowPosition;
        _settings.UseCards = UseCards;
        _settings.CardsInitialInterval = CardsInitialInterval;
        _settings.SaveSettings();
        RequestClose(true);
    }

    [RelayCommand]
    private void Cancel() => RequestClose(false);

    /// <summary>Copies the global cards of every word into its group's card system.</summary>
    [RelayCommand]
    private async Task CopyCardsAsync()
    {
        try
        {
            _management.CopyGlobalCardsToGroups();
        }
        catch (Exception ex)
        {
            await _dialogs.ShowMessageAsync(Strings.ErrorTitle, ex.Message);
        }
    }
}
