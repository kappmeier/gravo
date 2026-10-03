using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gravo;
using GravoApp.Localization;

namespace GravoApp.ViewModels;

/// <summary>The dialog that selects the dictionary language for a quiz over a whole language.</summary>
/// <remarks>The dialog lists the languages of the target language.</remarks>
public sealed partial class LanguageSelectViewModel : ViewModelBase
{
    [ObservableProperty] private string? _selectedLanguage;
    [ObservableProperty] private bool _testTargetLanguage;
    [ObservableProperty] private bool _testPhrases;

    public LanguageSelectViewModel(IDictionaryDao dictionary, Settings settings, UiTexts texts, string targetLanguage)
    {
        Texts = texts;
        Title = texts.Get(localization.MAIN_MENU_VOCABULARY_TEST_LANGUAGE);
        Languages = new ObservableCollection<string>(dictionary.DictionaryLanguages(targetLanguage));
        SelectedLanguage = Languages.FirstOrDefault();
        TestTargetLanguage = settings.QueryLanguage == QueryLanguage.TargetLanguage;
        TestPhrases = settings.TestSetPhrases;
    }

    public UiTexts Texts { get; }

    public ObservableCollection<string> Languages { get; }

    /// <summary>The direction of the quiz as chosen by the options checkbox.</summary>
    public QueryLanguage QueryLanguage =>
        TestTargetLanguage ? QueryLanguage.TargetLanguage : QueryLanguage.OriginalLanguage;

    [RelayCommand]
    private void Ok() => RequestClose(true);

    [RelayCommand]
    private void Cancel() => RequestClose(false);
}
