using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gravo;
using GravoApp.Localization;
using GravoApp.Services;

namespace GravoApp.ViewModels;

/// <summary>
/// Behavior and model for the "Word input" dialog to enlarge the dictionary. One sub entry after the other is added
/// to the dictionary.
/// </summary>
/// <remarks>
/// Missing main entries are created after confirmation. If direct add is set to on new words are also added to the
/// selected sub group automatically. The main entry text is copied into the word box until the word is edited.
/// </remarks>
public sealed partial class WordInputViewModel : ViewModelBase
{
    private readonly IDictionaryDao _dictionary;
    private readonly IGroupsDao _groups;
    private readonly IGroupDao _group;
    private readonly IDialogService _dialogs;
    private readonly WordTypes _wordTypes;
    private bool _wordEdited;
    private bool _syncing;

    [ObservableProperty] private string? _selectedLanguage;
    [ObservableProperty] private string? _selectedMainLanguage;
    [ObservableProperty] private bool _newLanguages;
    [ObservableProperty] private string _languageText = "";
    [ObservableProperty] private string _mainLanguageText = "";
    [ObservableProperty] private int _selectedWordTypeIndex;
    [ObservableProperty] private string _mainEntry = "";
    [ObservableProperty] private string _word = "";
    [ObservableProperty] private string _pre = "";
    [ObservableProperty] private string _post = "";
    [ObservableProperty] private string _meaning = "";
    [ObservableProperty] private string _additionalInfo = "";
    [ObservableProperty] private bool _irregular;
    [ObservableProperty] private bool _marked;
    [ObservableProperty] private bool _directAdd;
    [ObservableProperty] private string? _selectedGroup;
    [ObservableProperty] private string? _selectedSubGroup;

    /// <summary>
    /// Initializes the data model to add words. Loads the languages, word types and groups and preselects the first
    /// of each.
    /// </summary>
    /// <exception cref="DataInvalidException">If the word types stored in the database are invalid.</exception>
    public WordInputViewModel(IDictionaryDao dictionary, IGroupsDao groups, IGroupDao group,
        IPropertiesDao properties, UiTexts texts, IDialogService dialogs, string mainLanguage)
    {
        _dictionary = dictionary;
        _groups = groups;
        _group = group;
        _dialogs = dialogs;
        Texts = texts;
        Title = texts.Get(localization.MAIN_MENU_VOCABULARY_ENLARGE_DICTIONARY);
        Limits = properties.LoadProperties();
        _wordTypes = properties.LoadWordTypes();
        WordTypeNames = _wordTypes.GetSupportedWordTypes().ToList();
        WordTypeDisplays = WordTypeNames.Select(name => WordTypeTexts.Display(texts, name)).ToList();

        Languages = new ObservableCollection<string>(dictionary.DictionaryLanguages(mainLanguage));
        SelectedLanguage = Languages.FirstOrDefault();
        MainLanguages = new ObservableCollection<string>(dictionary.DictionaryMainLanguages());
        SelectedMainLanguage = MainLanguages.FirstOrDefault();
        Groups = new ObservableCollection<string>(groups.GetGroups());
        SelectedGroup = Groups.FirstOrDefault();
    }

    public UiTexts Texts { get; }

    /// <summary>The maximum lengths of the input boxes.</summary>
    public Properties Limits { get; }

    public ObservableCollection<string> Languages { get; }

    public ObservableCollection<string> MainLanguages { get; }

    /// <summary>The language of the new entry, filled automatically when <see cref="NewLanguages"/> is on.</summary>
    public string Language => NewLanguages ? LanguageText : SelectedLanguage ?? "";

    /// <summary>
    /// The main language of the new entry, filled automatically when <see cref="NewLanguages"/> is on.
    /// </summary>
    public string MainLanguage => NewLanguages ? MainLanguageText : SelectedMainLanguage ?? "";

    /// <summary>The word type names as stored in the database.</summary>
    public IReadOnlyList<string> WordTypeNames { get; }

    /// <summary>The localized word type names in the order of <see cref="WordTypeNames"/>.</summary>
    public IReadOnlyList<string> WordTypeDisplays { get; }

    public WordType SelectedWordType => _wordTypes.GetWordType(WordTypeNames[SelectedWordTypeIndex]);

    public ObservableCollection<string> Groups { get; }

    /// <summary>Direct add is only possible when there is at least one group.</summary>
    public bool CanDirectAdd => Groups.Count > 0;

    public ObservableCollection<string> SubGroups { get; } = new();

    /// <summary>The group entry of the selected sub group, or <c>null</c> if no sub group is selected.</summary>
    public GroupEntry? SelectedGroupEntry { get; private set; }

    partial void OnMainEntryChanged(string value)
    {
        if (_wordEdited)
        {
            return;
        }
        _syncing = true;
        Word = value;
        _syncing = false;
    }

    partial void OnWordChanged(string value)
    {
        if (!_syncing)
        {
            _wordEdited = true;
        }
    }

    /// <summary>Prefills the language boxes with the selected languages when new languages are switched on.</summary>
    partial void OnNewLanguagesChanged(bool value)
    {
        if (value)
        {
            LanguageText = SelectedLanguage ?? "";
            MainLanguageText = SelectedMainLanguage ?? "";
        }
    }

    partial void OnSelectedGroupChanged(string? value)
    {
        SelectedSubGroup = null;
        SubGroups.Clear();
        if (value is null)
        {
            return;
        }
        foreach (var entry in _groups.GetSubGroups(value))
        {
            SubGroups.Add(entry.SubGroup);
        }
        SelectedSubGroup = SubGroups.FirstOrDefault();
    }

    /// <summary>
    /// Selects the languages of the chosen sub group if the group uses only one of each, target and test langauge.
    /// </summary>
    /// <remarks>
    /// The hints are not awaited because a property hook cannot wait for the message window.
    /// </remarks>
    partial void OnSelectedSubGroupChanged(string? value)
    {
        if (value is null || SelectedGroup is null)
        {
            SelectedGroupEntry = null;
            return;
        }
        var entry = _groups.GetGroup(SelectedGroup, value);
        SelectedGroupEntry = entry;
        try
        {
            var language = _group.GetUniqueLanguage(ref entry);
            if (Languages.Contains(language))
            {
                SelectedLanguage = language;
            }
        }
        catch (LanguageException)
        {
            _ = _dialogs.ShowMessageAsync(Strings.HintTitle, Strings.LanguageNotAutoSelected);
        }
        try
        {
            var mainLanguage = _group.GetUniqueMainLanguage(ref entry);
            if (MainLanguages.Contains(mainLanguage))
            {
                SelectedMainLanguage = mainLanguage;
            }
        }
        catch (LanguageException)
        {
            _ = _dialogs.ShowMessageAsync(Strings.HintTitle, Strings.MainLanguageNotAutoSelected);
        }
    }

    /// <summary>Adds the word as a sub entry and, if direct add is enabled, also to the selected group.</summary>
    /// <remarks>
    /// With direct add enabled, confirmation is needed when the group has no words yet or uses another language.
    /// </remarks>
    [RelayCommand]
    private async Task AddSubEntryAsync()
    {
        if (DirectAdd && SelectedGroupEntry is null)
        {
            await _dialogs.ShowMessageAsync(Strings.WarningTitle, Strings.SelectExistingGroup);
            return;
        }
        if (DirectAdd && !await ConfirmGroupLanguagesAsync(SelectedGroupEntry!))
        {
            return;
        }

        var entry = new WordEntry(Word, Pre, Post, SelectedWordType, Meaning, AdditionalInfo, Irregular);
        try
        {
            _dictionary.AddSubEntry(ref entry, MainEntry, Language, MainLanguage);
            await AddToGroupIfDirectAsync();
        }
        catch (InputException ex)
        {
            await _dialogs.ShowMessageAsync(Strings.InvalidInputTitle, ex.Message);
        }
        catch (EntryExistsException)
        {
            await AddToGroupIfDirectAsync();
        }
        catch (EntryNotFoundException notFound)
        {
            if (await _dialogs.ConfirmAsync(Strings.MainEntryMissingTitle, Strings.MainEntryMissing(MainEntry)))
            {
                await AddMainEntryAndRetryAsync(entry, notFound);
            }
        }
        _wordEdited = false;
    }

    private async Task<bool> ConfirmGroupLanguagesAsync(GroupEntry selected)
    {
        var group = selected;
        var languageCount = _group.GetLanguages(ref group).Count;
        var mainLanguageCount = _group.GetMainLanguages(ref group).Count;
        if (languageCount == 0 || mainLanguageCount == 0)
        {
            return await _dialogs.ConfirmAsync(
                Strings.NewLanguageTitle, Strings.NoEntryInGroupYet(Language, MainLanguage));
        }
        if (languageCount == 1 && mainLanguageCount == 1
            && (_group.GetUniqueLanguage(ref group) != Language
                || _group.GetUniqueMainLanguage(ref group) != MainLanguage))
        {
            return await _dialogs.ConfirmAsync(
                Strings.NewLanguageTitle, Strings.SecondLanguageInGroup(Language, MainLanguage));
        }
        return true;
    }

    /// <summary>Creates a missing main entry and adds the sub entry again.</summary>
    /// <remarks>The conflict message names the original <paramref name="notFound"/> error.</remarks>
    private async Task AddMainEntryAndRetryAsync(WordEntry entry, EntryNotFoundException notFound)
    {
        try
        {
            _dictionary.AddEntry(MainEntry.Trim(), Language, MainLanguage);
        }
        catch (Exception ex) when (ex is LanguageNotFoundException or EntryNotFoundException or InputException)
        {
            await _dialogs.ShowMessageAsync(Strings.InvalidInputTitle, ex.Message);
        }
        try
        {
            _dictionary.AddSubEntry(ref entry, MainEntry, Language, MainLanguage);
            await AddToGroupIfDirectAsync();
        }
        catch (EntryExistsException)
        {
            await AddToGroupIfDirectAsync();
        }
        catch (Exception)
        {
            await _dialogs.ShowMessageAsync(Strings.ErrorTitle, Strings.EntryConflict(notFound.Message));
        }
    }

    private async Task AddToGroupIfDirectAsync()
    {
        if (!DirectAdd)
        {
            return;
        }
        var main = MainEntry;
        var mainEntry = _dictionary.GetMainEntry(ref main, Language, MainLanguage);
        var word = _dictionary.GetEntry(mainEntry, Word, Meaning);
        var group = SelectedGroupEntry!;
        var marked = Marked;
        var example = "";
        try
        {
            _group.Add(ref group, ref word, ref marked, ref example);
        }
        catch (EntryExistsException)
        {
            await _dialogs.ShowMessageAsync(Strings.AddNotPossibleTitle, Strings.WordAlreadyInGroup);
        }
    }

    [RelayCommand]
    private void Close() => RequestClose(false);
}
