using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gravo;
using GravoApp.Localization;
using GravoApp.Services;

namespace GravoApp.ViewModels.Explorer;

/// <summary>Panels in the edit-side of the vocabulary explorer. Panels are shown below the lists of words.</summary>
/// <remarks>
/// <see cref="Default"/> shows the word panel for one selected row and the multi edit panel for several.
/// <see cref="Search"/> shows no panel.
/// </remarks>
public enum PanelMode
{
    Default,
    Input,
    Search,
    Multi,
}

/// <summary>
/// Behavior and model of the vocabulary explorer, which allows browsing the dictionary and the groups in a tree and a
/// list. The tree is organized by latin dictionary letters, main entries, and group words.
/// </summary>
/// <remarks>
/// The tree loads the letters, main entries and group words of a node on its first expand. The leaf nodes show their
/// content as a list in one of the <see cref="ListStyle"/> layouts. On a language node the selected main entry shows
/// its words in a second list. The word panel below the lists shows the selected word and adds and changes words.
/// </remarks>
public sealed partial class ExplorerViewModel : ViewModelBase
{
    private const string IndexerName = "Item[]";

    private static readonly string[] Letters =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(c => new string(c, 1)).ToArray();

    private readonly IDictionaryDao _dictionary;
    private readonly IGroupsDao _groups;
    private readonly IGroupDao _group;
    private readonly IDialogService _dialogs;
    private readonly string _mainLanguage;
    private readonly WordTypes _wordTypes;
    private bool _showSubRows;
    private bool _keepEditor;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowEditor), nameof(ShowGroupFields))]
    private ExplorerNode? _selectedNode;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowEditor), nameof(ShowMultiEditor))]
    [NotifyPropertyChangedFor(nameof(IsDefaultPanel), nameof(IsInputPanel))]
    [NotifyPropertyChangedFor(nameof(IsSearchPanel), nameof(IsMultiPanel))]
    private PanelMode _panel;

    [ObservableProperty] private ExplorerRow? _selectedRow;
    [ObservableProperty] private ExplorerRow? _selectedSubRow;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowWordColumns), nameof(ShowMarkedColumn), nameof(ShowSubGroupColumn))]
    [NotifyPropertyChangedFor(nameof(ShowDictionaryColumns), nameof(ShowMainLanguageColumn))]
    [NotifyPropertyChangedFor(nameof(ShowGroupsColumns), nameof(ShowWordOnly))]
    private ListStyle _style;

    /// <summary>
    /// Initializes the view model. Loads the word types, sets up the tree and selects the dictionary root node.
    /// </summary>
    /// <remarks>
    /// <paramref name="mainLanguage"/> is the target language of new main entries when a word moves to another main
    /// entry.
    /// </remarks>
    /// <exception cref="DataInvalidException">The database holds invalid word types.</exception>
    public ExplorerViewModel(VocabularyDatabase vocabulary, IPropertiesDao properties, UiTexts texts,
        IDialogService dialogs, string mainLanguage)
    {
        _dictionary = vocabulary.Dictionary;
        _groups = vocabulary.Groups;
        _group = vocabulary.Group;
        _dialogs = dialogs;
        _mainLanguage = mainLanguage;
        _wordTypes = properties.LoadWordTypes();
        Texts = texts;
        Editor = new WordEditorViewModel(_wordTypes.GetSupportedWordTypes(), texts, properties.LoadProperties());
        Title = texts.Get(localization.EXPLORER_TITLE);
        SelectedRows.CollectionChanged += (_, _) => NotifyPanels();
        LoadTree();
        texts.PropertyChanged += OnTextsChanged;
    }

    public UiTexts Texts { get; }

    /// <summary>The fields of the word panel.</summary>
    public WordEditorViewModel Editor { get; }

    /// <summary>Occurs when a word was added. Enables putting the focus back into the word panel.</summary>
    public event Action? WordAdded;

    /// <summary>The two tree roots, the dictionary and the groups.</summary>
    public ObservableCollection<ExplorerNode> Roots { get; } = new();

    /// <summary>The rows of the list for the selected node.</summary>
    public ObservableCollection<ExplorerRow> Rows { get; } = new();

    /// <summary>All selected rows of the list, which the view keeps in sync.</summary>
    public ObservableCollection<ExplorerRow> SelectedRows { get; } = new();

    /// <summary>The words of the main entry that is selected on a language node.</summary>
    public ObservableCollection<ExplorerRow> SubRows { get; } = new();

    /// <summary>Decides whether the second list with the words of the selected main entry is visible.</summary>
    public bool ShowSubRows
    {
        get => _showSubRows;
        private set => SetProperty(ref _showSubRows, value);
    }

    public bool ShowWordColumns =>
        Style is ListStyle.WordEntry or ListStyle.WordEntryGroup or ListStyle.WordEntrySubGroup;

    public bool ShowMarkedColumn => Style is ListStyle.WordEntryGroup or ListStyle.WordEntrySubGroup;

    public bool ShowSubGroupColumn => Style == ListStyle.WordEntryGroup;

    public bool ShowDictionaryColumns => Style is ListStyle.Dictionary or ListStyle.MainLanguage;

    public bool ShowMainLanguageColumn => Style == ListStyle.Dictionary;

    public bool ShowGroupsColumns => Style == ListStyle.Groups;

    public bool ShowWordOnly => Style == ListStyle.Language;

    /// <summary>Decides whether the word panel is visible.</summary>
    /// <remarks>
    /// The panel needs a node below a language or a group to be selected. In the default mode it is replaced by the
    /// multi edit panel while several rows are selected.
    /// </remarks>
    public bool ShowEditor => Panel switch
    {
        PanelMode.Input => EditorAllowed,
        PanelMode.Default => SelectedRows.Count <= 1 && EditorAllowed,
        _ => false,
    };

    /// <summary>Decides whether the multi edit panel is visible.</summary>
    /// <remarks>
    /// The multi edit panel is visible when the mode is explicitly enabled, or multiple rows are selected.
    /// </remarks>
    public bool ShowMultiEditor => Panel == PanelMode.Multi || (Panel == PanelMode.Default && SelectedRows.Count > 1);

    /// <summary>Decides whether the group check boxes of the word panel are visible.</summary>
    /// <remarks>They are visible on a sub group and its words only.</remarks>
    public bool ShowGroupFields => IsSubGroupNode(SelectedNode);

    /// <summary>Decides whether the default panel is visible.</summary>
    public bool IsDefaultPanel
    {
        get => Panel == PanelMode.Default;
        set => SelectPanel(PanelMode.Default, value);
    }

    /// <summary>Decides whether the input panel is visible.</summary>
    public bool IsInputPanel
    {
        get => Panel == PanelMode.Input;
        set => SelectPanel(PanelMode.Input, value);
    }

    /// <summary>Decides whether the search panel is visible.</summary>
    public bool IsSearchPanel
    {
        get => Panel == PanelMode.Search;
        set => SelectPanel(PanelMode.Search, value);
    }

    /// <summary>Decides whether the multi edit panel is visible.</summary>
    /// <remarks>
    /// Explicitly sets the multi edit panel mode to active. Showing the multi edit-panel while multiple rows are
    /// selected is independent from this property.
    /// </remarks>
    public bool IsMultiPanel
    {
        get => Panel == PanelMode.Multi;
        set => SelectPanel(PanelMode.Multi, value);
    }

    private bool EditorAllowed => SelectedNode?.Kind is NodeKind.Language or NodeKind.Letter or NodeKind.MainEntry
        or NodeKind.Group or NodeKind.SubGroup or NodeKind.GroupWord;

    /// <summary>Rebuilds the tree from the database and selects the dictionary root.</summary>
    /// <remarks>The previous selection and the expanded nodes are not restored.</remarks>
    public void Reload() => LoadTree();

    partial void OnSelectedNodeChanged(ExplorerNode? value) => LoadList();

    partial void OnSelectedRowChanged(ExplorerRow? value)
    {
        SelectedSubRow = null;
        SubRows.Clear();
        var node = SelectedNode;
        ShowSubRows = value is not null && node?.Kind == NodeKind.Language;
        if (!ShowSubRows)
        {
            ShowInEditor(value);
            return;
        }
        foreach (var entry in _dictionary.GetWordsAndSubWords(value!.Word, node!.Title, node.MainLanguage!))
        {
            SubRows.Add(EntryRow(entry, entry));
        }
        SelectedSubRow = SubRows.FirstOrDefault();
    }

    partial void OnSelectedSubRowChanged(ExplorerRow? value) => ShowInEditor(value);

    /// <summary>
    /// Adds the word of the word panel to the dictionary and, if requested, to the selected sub group.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A letter or main entry node gives the languages. On a sub group or one of its words the languages come from
    /// the words in the group, or from the panel when the group is empty. A missing main entry is created after
    /// confirmation. A word that is already in the dictionary is not added twice, but it can still go into the group.
    /// </para>
    /// <para>
    /// After the dictionary or group is updated, the node is loaded again, so a new main entry or group word shows up
    /// in the tree, and the new word is selected in the list.
    /// </para>
    /// </remarks>
    [RelayCommand]
    private async Task AddWordAsync()
    {
        var node = SelectedNode;
        if (node is null)
        {
            return;
        }
        string language;
        string mainLanguage;
        GroupEntry? groupEntry = null;
        if (node.Kind is NodeKind.Letter or NodeKind.MainEntry)
        {
            language = node.Language!;
            mainLanguage = node.MainLanguage!;
        }
        else if (IsSubGroupNode(node))
        {
            var group = _groups.GetGroup(node.Group!, node.SubGroup!);
            groupEntry = group;
            if (_group.GetLanguages(ref group).Count > 1)
            {
                await _dialogs.ShowMessageAsync(Strings.EnglishWarningTitle, Strings.TooManyLanguagesInGroup);
                return;
            }
            if (_group.GetMainLanguages(ref group).Count > 1)
            {
                await _dialogs.ShowMessageAsync(Strings.EnglishWarningTitle, Strings.TooManyMainLanguagesInGroup);
                return;
            }
            language = _group.GetUniqueLanguage(ref group);
            mainLanguage = _group.GetUniqueMainLanguage(ref group);
            if (language.Length == 0)
            {
                language = Editor.Language;
            }
            if (mainLanguage.Length == 0)
            {
                mainLanguage = Editor.MainLanguage;
            }
        }
        else
        {
            await _dialogs.ShowMessageAsync(Strings.EnglishErrorTitle, Strings.CannotAddHere);
            return;
        }

        if (Editor.MainEntry.Length == 0)
        {
            Editor.MainEntry = Editor.Word;
        }
        if (language.Length == 0 || mainLanguage.Length == 0)
        {
            await _dialogs.ShowMessageAsync(Strings.EnglishErrorTitle, Strings.LanguageRequired);
            return;
        }

        var word = new WordEntry(Editor.Word, Editor.Pre, Editor.Post, Editor.SelectedWordType(_wordTypes),
            Editor.Meaning, Editor.AdditionalInfo, Editor.Irregular);
        try
        {
            _dictionary.AddSubEntry(ref word, Editor.MainEntry, language, mainLanguage);
        }
        catch (InputException ex)
        {
            await _dialogs.ShowMessageAsync(Strings.InvalidInputTitle, ex.Message);
            return;
        }
        catch (EntryExistsException)
        {
            word = StoredWord(word, language, mainLanguage);
        }
        catch (EntryNotFoundException notFound)
        {
            if (!await _dialogs.ConfirmAsync(Strings.MainEntryMissingTitle,
                    Strings.MainEntryMissingForLanguages(Editor.MainEntry, mainLanguage, language)))
            {
                return;
            }
            try
            {
                _dictionary.AddEntry(Editor.MainEntry.Trim(), language, mainLanguage);
            }
            catch (Exception ex) when (ex is LanguageNotFoundException or EntryNotFoundException or InputException)
            {
                await _dialogs.ShowMessageAsync(Strings.InvalidInputTitle, ex.Message);
            }
            try
            {
                _dictionary.AddSubEntry(ref word, Editor.MainEntry, language, mainLanguage);
            }
            catch (Exception)
            {
                await _dialogs.ShowMessageAsync(Strings.ErrorTitle, Strings.EntryConflict(notFound.Message));
                return;
            }
        }
        catch (Exception ex)
        {
            await _dialogs.ShowMessageAsync(Strings.ErrorTitle, Strings.EntryConflict(ex.Message));
            return;
        }

        if (groupEntry is not null && Editor.AddToGroup)
        {
            var group = groupEntry;
            var marked = Editor.Marked;
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
        ReloadAfterAdd(node, word);
        WordAdded?.Invoke();
    }

    /// <summary>Writes back changes made in the word panel to the selected word and updates its row.</summary>
    /// <remarks>
    /// On a sub group the marked flag of the group word is written as well. A changed main entry moves the word to
    /// that main entry, which is created when it is missing.
    /// </remarks>
    [RelayCommand]
    private async Task ChangeWordAsync()
    {
        if (SelectedRows.Count > 1)
        {
            await _dialogs.ShowMessageAsync(Strings.ProductName, Strings.SelectOnlyOne);
            return;
        }
        var row = ShowSubRows ? SelectedSubRow : SelectedRow;
        if (SelectedRow is null || row?.Entry is not { } entry)
        {
            await _dialogs.ShowMessageAsync(Strings.ProductName, Strings.SelectOne);
            return;
        }
        var node = SelectedNode!;
        try
        {
            var updated = _dictionary.ChangeEntry(ref entry, Editor.ToUpdateData(_wordTypes));
            var groupChanged = IsSubGroupNode(node);
            if (groupChanged)
            {
                ChangeMarked(node, updated, Editor.Marked);
            }
            var main = _dictionary.GetMainEntry(ref entry);
            if (main.Word != Editor.MainEntry)
            {
                _dictionary.ChangeEntry(updated, DataTools.GetOrCreateMainEntry(_dictionary, Editor.MainEntry,
                    node.Language ?? main.Language, _mainLanguage));
            }
            ReplaceRow(row, updated, groupChanged ? Editor.Marked : null);
        }
        catch (InputException ex)
        {
            await _dialogs.ShowMessageAsync(Strings.InvalidInputTitle, ex.Message);
        }
        catch (EntryExistsException)
        {
            await _dialogs.ShowMessageAsync(Strings.ProductName, Strings.EntryExists(Editor.Word));
        }
    }

    /// <summary>Builds the dictionary root with its main languages and languages and the groups root.</summary>
    /// <remarks>
    /// Every further main language is nested as the last child of the previous one.
    /// Languages always get a placeholder. A sub group gets a placeholder only if it contains words.
    /// </remarks>
    private void LoadTree()
    {
        var dictionary = CreateNode(NodeKind.DictionaryRoot, Texts.Get(localization.TREE_DICTIONARY), null);
        var parent = dictionary;
        var children = new List<ExplorerNode>();
        foreach (var mainLanguage in _dictionary.DictionaryMainLanguages())
        {
            var node = CreateNode(NodeKind.MainLanguage, mainLanguage, parent);
            children.Add(node);
            parent.SetChildren(children);
            parent = node;
            children = _dictionary.DictionaryLanguages(mainLanguage)
                .Select(language => CreateNode(NodeKind.Language, language, node, hasChildren: true)).ToList();
        }
        parent.SetChildren(children);

        var groups = CreateNode(NodeKind.GroupsRoot, Texts.Get(localization.TREE_GROUPS), null);
        var groupNodes = new List<ExplorerNode>();
        foreach (var name in _groups.GetGroups())
        {
            var node = CreateNode(NodeKind.Group, name, groups);
            node.SetChildren(_groups.GetSubGroups(name)
                .Select(sub => CreateNode(NodeKind.SubGroup, sub.SubGroup, node, Load(sub).WordCount > 0)).ToList());
            groupNodes.Add(node);
        }
        groups.SetChildren(groupNodes);

        Roots.Clear();
        Roots.Add(dictionary);
        Roots.Add(groups);
        SelectedNode = dictionary;
    }

    /// <summary>Loads the children of an expanded node.</summary>
    /// <remarks>
    /// A language gets the letters A to Z, and a letter with main entries gets a placeholder. A letter gets its
    /// distinct main entries and a sub group gets its words.
    /// </remarks>
    private void LoadChildren(ExplorerNode node)
    {
        switch (node.Kind)
        {
            case NodeKind.Language:
                node.SetChildren(Letters.Select(letter => CreateNode(NodeKind.Letter, letter, node,
                    _dictionary.WordCount(node.Title, node.MainLanguage!, letter) > 0)).ToList());
                break;
            case NodeKind.Letter:
                node.SetChildren(_dictionary.GetMainEntries(node.Language!, node.MainLanguage!, node.Title)
                    .Select(entry => entry.Word).Distinct()
                    .Select(word => CreateNode(NodeKind.MainEntry, word, node)).ToList());
                break;
            case NodeKind.SubGroup:
                node.SetChildren(LoadSubGroup(node).Entries
                    .Select(word => CreateNode(NodeKind.GroupWord, word.Word, node, payload: word)).ToList());
                break;
        }
    }

    /// <summary>Fills the list for the selected node and selects the first row.</summary>
    private void LoadList()
    {
        SelectedRow = null;
        Rows.Clear();
        if (SelectedNode is { } node)
        {
            AddRows(node);
        }
        SelectedRow = Rows.FirstOrDefault();
    }

    private void AddRows(ExplorerNode node)
    {
        switch (node.Kind)
        {
            case NodeKind.DictionaryRoot:
                Style = ListStyle.Dictionary;
                foreach (var mainLanguage in _dictionary.DictionaryMainLanguages())
                {
                    foreach (var language in _dictionary.DictionaryLanguages(mainLanguage))
                    {
                        Rows.Add(CountRow(mainLanguage, language));
                    }
                }
                break;
            case NodeKind.MainLanguage:
                Style = ListStyle.MainLanguage;
                foreach (var language in node.Children.Where(child => child.Kind == NodeKind.Language))
                {
                    Rows.Add(CountRow(node.Title, language.Title));
                }
                break;
            case NodeKind.Language:
                Style = ListStyle.Language;
                foreach (var entry in _dictionary.GetMainEntries(node.Title, node.MainLanguage!))
                {
                    Rows.Add(new ExplorerRow(null) { Word = entry.Word });
                }
                break;
            case NodeKind.Letter:
                Style = ListStyle.WordEntry;
                AddWordRows(_dictionary.GetWords(node.Language!, node.MainLanguage!, node.Title));
                break;
            case NodeKind.MainEntry:
                Style = ListStyle.WordEntry;
                AddWordRows(_dictionary.GetWords(node.Title, node.Title, node.Language!, node.MainLanguage!));
                AddWordRows(_dictionary.GetSubWords(node.Title, node.Language!, node.MainLanguage!));
                break;
            case NodeKind.GroupsRoot:
                Style = ListStyle.Groups;
                foreach (var name in _groups.GetGroups())
                {
                    Rows.Add(new ExplorerRow(null)
                    {
                        GroupName = name,
                        SubGroupCount = _groups.SubGroupCount(name),
                        Count1 = DataTools.WordCount(_groups, _group, name),
                        LanguageCount = DataTools.UsedLanguagesCount(_groups, _group, name),
                    });
                }
                break;
            case NodeKind.Group:
                Style = ListStyle.WordEntryGroup;
                foreach (var sub in _groups.GetSubGroups(node.Title))
                {
                    AddGroupRows(Load(sub).Entries, sub.SubGroup);
                }
                break;
            case NodeKind.SubGroup:
                Style = ListStyle.WordEntrySubGroup;
                AddGroupRows(LoadSubGroup(node).Entries, node.Title);
                break;
            case NodeKind.GroupWord:
                Style = ListStyle.WordEntrySubGroup;
                AddGroupRows([(TestWord)node.Payload!], node.SubGroup!);
                break;
        }
    }

    private void AddWordRows(IEnumerable<WordEntry> entries)
    {
        foreach (var entry in entries)
        {
            Rows.Add(EntryRow(entry, entry));
        }
    }

    private void AddGroupRows(IEnumerable<TestWord> words, string subGroup)
    {
        foreach (var word in words)
        {
            Rows.Add(EntryRow(word.WordEntry, word, YesNo(word.Marked), subGroup));
        }
    }

    private ExplorerRow CountRow(string mainLanguage, string language) => new(null)
    {
        MainLanguage = mainLanguage,
        Language = language,
        Count1 = _dictionary.WordCount(language, mainLanguage),
        Count2 = _dictionary.WordCountTotal(language, mainLanguage),
    };

    private ExplorerRow EntryRow(WordEntry entry, object payload, string marked = "", string subGroup = "") =>
        new(payload)
        {
            Pre = entry.Pre,
            Word = entry.Word,
            Post = entry.Post,
            Meaning = entry.Meaning,
            WordType = WordTypeName(entry.WordType),
            AdditionalInfo = entry.AdditionalTargetLangInfo,
            Irregular = YesNo(entry.Irregular),
            Marked = marked,
            SubGroup = subGroup,
        };

    /// <summary>Returns the localized name of a word type, or an empty text for an unknown type.</summary>
    private string WordTypeName(WordType type) =>
        _wordTypes.GetWordType((int)type) is { } name ? WordTypeTexts.Display(Texts, name) : "";

    private string YesNo(bool value) => Texts.Get(value ? localization.YES : localization.NO);

    private static bool IsSubGroupNode(ExplorerNode? node) =>
        node?.Kind is NodeKind.SubGroup or NodeKind.GroupWord;

    private void SelectPanel(PanelMode mode, bool selected)
    {
        if (selected)
        {
            Panel = mode;
        }
    }

    private void NotifyPanels()
    {
        OnPropertyChanged(nameof(ShowEditor));
        OnPropertyChanged(nameof(ShowMultiEditor));
    }

    /// <summary>Fills the word panel with the content from the selected <paramref name="row"/>.</summary>
    /// <remarks>
    /// Rows without word valid data (overview rows, rows during a word addition) leave the panel unchanged.
    /// </remarks>
    private void ShowInEditor(ExplorerRow? row)
    {
        if (_keepEditor || row?.Entry is not { } entry)
        {
            return;
        }
        var main = _dictionary.GetMainEntry(ref entry);
        if (row.Payload is TestWord word)
        {
            Editor.Show(word, main, _wordTypes, IsSubGroupNode(SelectedNode));
        }
        else
        {
            Editor.Show(entry, main, _wordTypes);
        }
    }

    /// <summary>Returns the dictionary entry that has the word and meaning of <paramref name="word"/>.</summary>
    private WordEntry StoredWord(WordEntry word, string language, string mainLanguage)
    {
        var mainEntry = Editor.MainEntry;
        var main = _dictionary.GetMainEntry(ref mainEntry, language, mainLanguage);
        return _dictionary.GetEntry(main, word.Word, word.Meaning);
    }

    /// <summary>
    /// When a new word is added, reloads the tree branch and the list of <paramref name="node"/> and selects the new
    /// word.
    /// </summary>
    /// <remarks>
    /// A selected main entry or group word is replaced by its reloaded counterpart, or by its parent when it is gone.
    /// When the word is not part of the list, the first row is selected and the word panel keeps the typed word.
    /// </remarks>
    private void ReloadAfterAdd(ExplorerNode node, WordEntry word)
    {
        var branch = node.Kind is NodeKind.MainEntry or NodeKind.GroupWord ? node.Parent! : node;
        _keepEditor = true;
        try
        {
            branch.Invalidate();
            if (branch == node)
            {
                LoadList();
            }
            else
            {
                SelectedNode = branch.Children.FirstOrDefault(child => child.Title == node.Title) ?? branch;
            }
        }
        finally
        {
            _keepEditor = false;
        }
        if (Rows.FirstOrDefault(row => row.Entry?.WordIndex == word.WordIndex) is { } added)
        {
            SelectedRow = added;
        }
    }

    private void ChangeMarked(ExplorerNode node, WordEntry word, bool marked)
    {
        var group = _groups.GetGroup(node.Group!, node.SubGroup!);
        var testWord = _group.Load(ref group).GetWord(word.WordIndex);
        _group.UpdateMarked(ref group, ref testWord, marked);
    }

    /// <summary>Replaces <paramref name="row"/> by a row containing a changed word and selects it.</summary>
    /// <remarks>A group row keeps its marked flag unless <paramref name="marked"/> is <c>true</c>.</remarks>
    private void ReplaceRow(ExplorerRow row, WordEntry updated, bool? marked)
    {
        ExplorerRow changed;
        if (row.Payload is TestWord word)
        {
            var testWord = new TestWord(updated, marked ?? word.Marked, word.Example);
            changed = EntryRow(updated, testWord, YesNo(testWord.Marked), row.SubGroup);
        }
        else
        {
            changed = EntryRow(updated, updated);
        }
        if (ShowSubRows)
        {
            SubRows[SubRows.IndexOf(row)] = changed;
            SelectedSubRow = changed;
        }
        else
        {
            Rows[Rows.IndexOf(row)] = changed;
            SelectedRow = changed;
        }
    }

    private ExplorerNode CreateNode(NodeKind kind, string title, ExplorerNode? parent, bool hasChildren = false,
        object? payload = null)
    {
        var node = new ExplorerNode(kind, title, parent, hasChildren, payload);
        node.ExpandRequested += LoadChildren;
        return node;
    }

    private GroupDto Load(GroupEntry entry) => _group.Load(ref entry);

    private GroupDto LoadSubGroup(ExplorerNode node) => Load(_groups.GetGroup(node.Group!, node.SubGroup!));

    /// <summary>Handles a language switch. Renames the roots and refills the list.</summary>
    /// <remarks>The selected row keeps its position.</remarks>
    private void OnTextsChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != IndexerName)
        {
            return;
        }
        Title = Texts.Get(localization.EXPLORER_TITLE);
        Editor.RefreshTexts();
        Roots[0].Title = Texts.Get(localization.TREE_DICTIONARY);
        Roots[1].Title = Texts.Get(localization.TREE_GROUPS);
        var selected = SelectedRow is null ? -1 : Rows.IndexOf(SelectedRow);
        LoadList();
        if (selected >= 0 && selected < Rows.Count)
        {
            SelectedRow = Rows[selected];
        }
    }
}
