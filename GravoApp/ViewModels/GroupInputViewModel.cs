using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gravo;
using GravoApp.Localization;
using GravoApp.Services;

namespace GravoApp.ViewModels;

/// <summary>
/// Behavior and model for the group input tab. It provides functionality to add words from the dictionary to a sub
/// group.
/// </summary>
/// <remarks>
/// Group input provides the following functionality:
/// - Full text search for a main entry in the dictionary
/// - Adding a selected word from a main entry to a sub group
/// - Removing a word from the sub group
/// </remarks>
public sealed partial class GroupInputViewModel : ViewModelBase
{
    private readonly IGroupsDao _groups;
    private readonly IDictionaryDao _dictionary;
    private readonly IGroupDao _group;
    private readonly IDialogService _dialogs;
    private readonly string _mainLanguage;
    private GroupEntry? _groupEntry;
    private GroupDto? _groupData;

    [ObservableProperty] private string? _selectedGroup;
    [ObservableProperty] private string? _selectedSubGroup;
    [ObservableProperty] private string? _selectedLanguage;
    [ObservableProperty] private string? _selectedWordInGroup;
    [ObservableProperty] private WordRow? _selectedMeaning;
    [ObservableProperty] private string _searchText = "";
    [ObservableProperty] private string _similarWord = "";
    [ObservableProperty] private WordRow? _selectedWord;
    [ObservableProperty] private bool _marked;
    [ObservableProperty] private string _wordsInGroupText = "";
    [ObservableProperty] private string _wordsInSubGroupText = "";
    [ObservableProperty] private string _wordsInLanguageText = "";

    /// <summary>
    /// Initializes the tab with the groups and languages and selects the first group and its first sub group.
    /// </summary>
    /// <remarks>
    /// The language is initialized as the unique language of that sub group if it has one, otherwise the first.
    /// </remarks>
    public GroupInputViewModel(IGroupsDao groups, IDictionaryDao dictionary, IGroupDao group, IDialogService dialogs,
        UiTexts texts, string mainLanguage)
    {
        _groups = groups;
        _dictionary = dictionary;
        _group = group;
        _dialogs = dialogs;
        _mainLanguage = mainLanguage;
        Texts = texts;
        Title = texts.Get(localization.MAIN_MENU_VOCABULARY_INSERT_GROUPS);
        Groups = new ObservableCollection<string>(groups.GetGroups());
        Languages = new ObservableCollection<string>(dictionary.DictionaryLanguages(mainLanguage));
        SelectedLanguage = Languages.FirstOrDefault();
        SelectedGroup = Groups.FirstOrDefault();
        texts.PropertyChanged += OnTextsChanged;
    }

    public UiTexts Texts { get; }

    /// <summary>An event raised after <see cref="SelectCommand"/> added a word to the sub group.</summary>
    public event Action? WordAdded;

    public ObservableCollection<string> Groups { get; }

    public ObservableCollection<string> SubGroups { get; } = new();

    public ObservableCollection<string> Languages { get; }

    /// <summary>The words of the selected sub group with one item per group row.</summary>
    /// <remarks>Words with two or more meanings in the sub group is listed respectively.</remarks>
    public ObservableCollection<string> WordsInGroup { get; } = new();

    /// <summary>The rows of the selected sub group that belong to <see cref="SelectedWordInGroup"/>.</summary>
    public ObservableCollection<WordRow> Meanings { get; } = new();

    /// <summary>The words of the main entry found by the search.</summary>
    public ObservableCollection<WordRow> Words { get; } = new();

    partial void OnSelectedGroupChanged(string? value)
    {
        SelectedSubGroup = null;
        SubGroups.Clear();
        if (value is not null)
        {
            foreach (var entry in _groups.GetSubGroups(value))
            {
                SubGroups.Add(entry.SubGroup);
            }
            SelectedSubGroup = SubGroups.FirstOrDefault();
        }
        UpdateDisplayedInfo();
    }

    partial void OnSelectedSubGroupChanged(string? value)
    {
        UpdateWordsInGroup();
        SelectUniqueLanguage();
    }

    partial void OnSelectedLanguageChanged(string? value)
    {
        Search(SearchText);
        UpdateDisplayedInfo();
    }

    partial void OnSelectedWordInGroupChanged(string? value)
    {
        SelectedMeaning = null;
        Meanings.Clear();
        if (value is null || _groupData is null)
        {
            return;
        }
        foreach (var entry in _groupData.FilterWords(value))
        {
            Meanings.Add(new WordRow(entry.Pre, entry.Word, entry.Post, entry.Meaning, entry));
        }
        SelectedMeaning = Meanings.FirstOrDefault();
    }

    partial void OnSearchTextChanged(string value) => Search(value);

    /// <summary>Searches the text and, if nothing is found, ever shorter beginnings of it.</summary>
    /// <remarks>
    /// The empty text is never searched because it would match every main entry. <see cref="SimilarWord"/> is
    /// cleared when nothing matches and the listed words remain.
    /// </remarks>
    private void Search(string text)
    {
        for (var length = text.Length; length > 0; length--)
        {
            if (SearchWord(text[..length]))
            {
                return;
            }
        }
        SimilarWord = "";
    }

    /// <summary>Lists the words of the first main entry that starts with <paramref name="word"/>.</summary>
    /// <returns><c>true</c> if a main entry was found.</returns>
    private bool SearchWord(string word)
    {
        if (SelectedLanguage is null)
        {
            return false;
        }
        var similar = _dictionary.FindSimilar(word, SelectedLanguage, _mainLanguage);
        if (similar.Length == 0)
        {
            return false;
        }
        SimilarWord = similar;
        SelectedWord = null;
        Words.Clear();
        foreach (var entry in _dictionary.GetWordsAndSubWords(similar, SelectedLanguage, _mainLanguage))
        {
            Words.Add(new WordRow(entry.Pre, entry.Word, entry.Post, entry.Meaning, entry));
        }
        SelectedWord = Words.FirstOrDefault();
        return true;
    }

    /// <summary>Selects the language of the sub group if all of its words have the same language.</summary>
    /// <remarks>The selection stays unchanged if the sub group is empty or mixes languages.</remarks>
    private void SelectUniqueLanguage()
    {
        if (_groupEntry is null)
        {
            return;
        }
        var entry = _groupEntry;
        string language;
        try
        {
            language = _group.GetUniqueLanguage(entry);
        }
        catch (Exception ex) when (ex is LanguageException or LanguageNotFoundException)
        {
            return;
        }
        if (Languages.Contains(language))
        {
            SelectedLanguage = language;
        }
    }

    /// <summary>Reloads the selected sub group. If possible keeps the selected position in the word list.</summary>
    private void UpdateWordsInGroup()
    {
        var selected = SelectedWordInGroup is null ? -1 : WordsInGroup.IndexOf(SelectedWordInGroup);
        SelectedWordInGroup = null;
        WordsInGroup.Clear();
        _groupEntry = null;
        _groupData = null;
        if (SelectedGroup is not null && SelectedSubGroup is not null)
        {
            var entry = _groups.GetGroup(SelectedGroup, SelectedSubGroup);
            _groupEntry = entry;
            _groupData = _group.Load(entry);
            foreach (var word in _groupData.GetWords())
            {
                WordsInGroup.Add(word);
            }
        }
        if (selected >= 0 && selected < WordsInGroup.Count)
        {
            SelectedWordInGroup = WordsInGroup[selected];
        }
        UpdateDisplayedInfo();
    }

    private void UpdateDisplayedInfo()
    {
        if (SelectedLanguage is null)
        {
            return;
        }
        WordsInLanguageText = Texts.Plural(_dictionary.WordCount(SelectedLanguage, _mainLanguage),
            localization.GROUP_INPUT_ENTRIES_IN_LANGUAGE_ONE,
            localization.GROUP_INPUT_ENTRIES_IN_LANGUAGE_MANY);
        WordsInSubGroupText = Texts.Plural(WordsInGroup.Count,
                localization.GROUP_INPUT_DISTINCT_ENTRIES_ONE,
                localization.GROUP_INPUT_DISTINCT_ENTRIES_MANY)
            + Texts.Get(localization.GROUP_INPUT_IN_THE_GROUP)
            + Environment.NewLine
            + Texts.Plural(_groupData?.WordCount ?? 0,
                localization.GROUP_INPUT_ENTRIES_TOTAL_ONE,
                localization.GROUP_INPUT_ENTRIES_TOTAL_MANY);
        WordsInGroupText = SelectedGroup is null
            ? ""
            : Texts.Plural(DataTools.WordCount(_groups, _group, SelectedGroup),
                localization.GROUP_INPUT_ENTRIES_IN_WHOLE_GROUP_ONE,
                localization.GROUP_INPUT_ENTRIES_IN_WHOLE_GROUP_MANY);
    }

    /// <summary>Handles a language switch. Resets the title and the count texts.</summary>
    private void OnTextsChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == UiTexts.IndexerName)
        {
            Title = Texts.Get(localization.MAIN_MENU_VOCABULARY_INSERT_GROUPS);
            UpdateDisplayedInfo();
        }
    }

    public override void OnClosed() => Texts.PropertyChanged -= OnTextsChanged;

    /// <summary>Adds the selected dictionary word to the sub group with the marked flag and an empty example.</summary>
    /// <remarks>A word that is in the sub group already is not added again. Instead, a message is shown.</remarks>
    [RelayCommand]
    private async Task SelectAsync()
    {
        if (Words.Count == 0 || _groupEntry is null)
        {
            return;
        }
        if (SelectedWord?.Payload is not WordEntry word)
        {
            await _dialogs.ShowMessageAsync(Texts.Get(localization.ERROR_OCCURRED_TITLE),
                Texts.Get(localization.GROUP_INPUT_RESELECT_WORD));
            return;
        }
        var group = _groupEntry;
        try
        {
            _group.Add(group, word, Marked, "");
        }
        catch (EntryExistsException)
        {
            await _dialogs.ShowMessageAsync(Texts.Get(localization.ADD_NOT_POSSIBLE_TITLE),
                Texts.Get(localization.WORD_ALREADY_IN_GROUP));
            return;
        }
        UpdateWordsInGroup();
        SelectedWordInGroup = WordsInGroup.LastOrDefault();
        WordAdded?.Invoke();
    }

    /// <summary>Removes the selected row from the sub group.</summary>
    /// <remarks>
    /// The same position in the word list stays selected, or the previous one when the last word was removed.
    /// </remarks>
    [RelayCommand]
    private void Deselect()
    {
        if (SelectedMeaning?.Payload is not TestWord testWord || _groupEntry is null)
        {
            return;
        }
        var selected = SelectedWordInGroup is null ? -1 : WordsInGroup.IndexOf(SelectedWordInGroup);
        var group = _groupEntry;
        _group.Delete(group, testWord);
        UpdateWordsInGroup();
        if (selected > WordsInGroup.Count - 1)
        {
            if (selected > 0)
            {
                SelectedWordInGroup = WordsInGroup[selected - 1];
            }
            else
            {
                Meanings.Clear();
            }
        }
        else if (selected >= 0)
        {
            SelectedWordInGroup = WordsInGroup[selected];
        }
    }

    [RelayCommand]
    private void Close() => RequestClose(false);
}
