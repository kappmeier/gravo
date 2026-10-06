using CommunityToolkit.Mvvm.ComponentModel;
using Gravo;
using GravoApp.Localization;

namespace GravoApp.ViewModels.Explorer;

/// <summary>Behavior and model of the word panel in the vocabulary explorer.</summary>
/// <remarks>
/// The panel shows the selected word and is the input for adding and changing words. The commands live in
/// <c>ExplorerViewModel</c>. The word types are listed in the order of <see cref="WordTypeNames"/>.
/// </remarks>
public sealed partial class WordEditorViewModel : ObservableObject
{
    private readonly UiTexts _texts;

    [ObservableProperty] private string _mainEntry = "";
    [ObservableProperty] private string _word = "";
    [ObservableProperty] private string _pre = "";
    [ObservableProperty] private string _post = "";
    [ObservableProperty] private string _meaning = "";
    [ObservableProperty] private string _additionalInfo = "";
    [ObservableProperty] private string _language = "";
    [ObservableProperty] private string _mainLanguage = "";
    [ObservableProperty] private int _wordTypeIndex;
    [ObservableProperty] private bool _irregular;
    [ObservableProperty] private bool _marked;
    [ObservableProperty] private bool _addToGroup;
    [ObservableProperty] private IReadOnlyList<string> _wordTypeDisplays = [];

    public WordEditorViewModel(IEnumerable<string> wordTypeNames, UiTexts texts, Properties limits)
    {
        _texts = texts;
        WordTypeNames = wordTypeNames.ToList();
        Limits = limits;
        RefreshTexts();
    }

    /// <summary>The word type names as stored in the database.</summary>
    public IReadOnlyList<string> WordTypeNames { get; }

    /// <summary>The maximum lengths of the input boxes.</summary>
    public Properties Limits { get; }

    /// <summary>Fills the panel with a dictionary word and its main entry.</summary>
    /// <remarks>
    /// <see cref="Marked"/> and <see cref="AddToGroup"/> keep their values. A word type that is not in
    /// <see cref="WordTypeNames"/> leaves no selected type.
    /// </remarks>
    public void Show(WordEntry entry, MainEntry main, WordTypes types)
    {
        MainEntry = main.Word;
        Pre = entry.Pre;
        Word = entry.Word;
        Post = entry.Post;
        AdditionalInfo = entry.AdditionalTargetLangInfo;
        Meaning = entry.Meaning;
        WordTypeIndex = WordTypeNames.ToList().IndexOf(types.GetWordType((int)entry.WordType) ?? "");
        Irregular = entry.Irregular;
        Language = main.Language;
        MainLanguage = main.MainLanguage;
    }

    /// <summary>Fills the panel with a group word and its main entry.</summary>
    /// <remarks><see cref="Marked"/> is only set when <paramref name="showMarked"/> is <c>true</c>.</remarks>
    public void Show(TestWord word, MainEntry main, WordTypes types, bool showMarked)
    {
        Show(word.WordEntry, main, types);
        if (showMarked)
        {
            Marked = word.Marked;
        }
    }

    /// <summary>Returns the selected word type, or the first one when none is selected.</summary>
    public WordType SelectedWordType(WordTypes types) => types.GetWordType(WordTypeNames[Math.Max(WordTypeIndex, 0)]);

    /// <summary>Returns the changes for every field of the dictionary word.</summary>
    /// <remarks>Without a selected word type the stored type stays unchanged.</remarks>
    public IDictionaryDao.UpdateData ToUpdateData(WordTypes types)
    {
        var data = new IDictionaryDao.UpdateData
        {
            Word = Word,
            Pre = Pre,
            Post = Post,
            Meaning = Meaning,
            AdditionalTargetLangInfo = AdditionalInfo,
            Irregular = Irregular,
        };
        if (WordTypeIndex >= 0)
        {
            data.WordType = SelectedWordType(types);
        }
        return data;
    }

    /// <summary>Reloads the localized word type names and keeps the selected type.</summary>
    public void RefreshTexts()
    {
        var index = WordTypeIndex;
        WordTypeDisplays = WordTypeNames.Select(name => WordTypeTexts.Display(_texts, name)).ToList();
        WordTypeIndex = index;
    }
}
