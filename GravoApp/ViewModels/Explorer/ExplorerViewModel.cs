using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Gravo;
using GravoApp.Localization;
using GravoApp.Services;

namespace GravoApp.ViewModels.Explorer;

/// <summary>
/// Behavior and model of the vocabulary explorer, which allows browsing the dictionary and the groups in a tree and a
/// list. The tree is organized by latin dictionary letters, main entries, and group words.
/// </summary>
/// <remarks>
/// The tree loads the letters, main entries and group words of a node on its first expand. The leaf nodes show their
/// content as a list in one of the <see cref="ListStyle"/> layouts. On a language node the selected main entry shows
/// its words in a second list.
/// </remarks>
public sealed partial class ExplorerViewModel : ViewModelBase
{
    private const string IndexerName = "Item[]";

    private static readonly string[] Letters =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(c => new string(c, 1)).ToArray();

    private readonly IDictionaryDao _dictionary;
    private readonly IGroupsDao _groups;
    private readonly IGroupDao _group;
    private readonly WordTypes _wordTypes;
    private bool _showSubRows;

    [ObservableProperty] private ExplorerNode? _selectedNode;
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
    /// <paramref name="dialogs"/> and <paramref name="mainLanguage"/> are reserved for the word editor, which is to be
    /// added.
    /// </remarks>
    /// <exception cref="DataInvalidException">The database holds invalid word types.</exception>
    public ExplorerViewModel(VocabularyDatabase vocabulary, IPropertiesDao properties, UiTexts texts,
        IDialogService dialogs, string mainLanguage)
    {
        _dictionary = vocabulary.Dictionary;
        _groups = vocabulary.Groups;
        _group = vocabulary.Group;
        _wordTypes = properties.LoadWordTypes();
        Texts = texts;
        Title = texts.Get(localization.EXPLORER_TITLE);
        LoadTree();
        texts.PropertyChanged += OnTextsChanged;
    }

    public UiTexts Texts { get; }

    /// <summary>The two tree roots, the dictionary and the groups.</summary>
    public ObservableCollection<ExplorerNode> Roots { get; } = new();

    /// <summary>The rows of the list for the selected node.</summary>
    public ObservableCollection<ExplorerRow> Rows { get; } = new();

    /// <summary>All selected rows of the list, which the view keeps in sync.</summary>
    public ObservableCollection<ExplorerRow> SelectedRows { get; } = new();

    /// <summary>The words of the main entry that is selected on a language node.</summary>
    public ObservableCollection<ExplorerRow> SubRows { get; } = new();

    /// <summary>DDecides whether the second list with the words of the selected main entry is visible.</summary>
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
            return;
        }
        foreach (var entry in _dictionary.GetWordsAndSubWords(value!.Word, node!.Title, node.MainLanguage!))
        {
            SubRows.Add(EntryRow(entry, entry));
        }
        SelectedSubRow = SubRows.FirstOrDefault();
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
