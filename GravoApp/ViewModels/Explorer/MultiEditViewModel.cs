using CommunityToolkit.Mvvm.ComponentModel;
using Gravo;
using GravoApp.Localization;

namespace GravoApp.ViewModels.Explorer;

/// <summary>Behavior and model of the multi edit panel in the vocabulary explorer.</summary>
/// <remarks>
/// Every field has an <c>Enable</c> flag. Selected words are only updated for the enabled fields. The command is
/// implemented in the <c>ExplorerViewModel</c>. The word types are listed in the order of <see cref="WordTypeNames"/>,
/// and the first one is selected at the start.
/// </remarks>
public sealed partial class MultiEditViewModel : ObservableObject
{
    private readonly UiTexts _texts;

    [ObservableProperty] private bool _enablePre;
    [ObservableProperty] private string _pre = "";
    [ObservableProperty] private bool _enableWord;
    [ObservableProperty] private string _word = "";
    [ObservableProperty] private bool _enablePost;
    [ObservableProperty] private string _post = "";
    [ObservableProperty] private bool _enableAdditionalInfo;
    [ObservableProperty] private string _additionalInfo = "";
    [ObservableProperty] private bool _enableMeaning;
    [ObservableProperty] private string _meaning = "";
    [ObservableProperty] private bool _enableIrregular;
    [ObservableProperty] private bool _irregular;
    [ObservableProperty] private bool _enableWordType;
    [ObservableProperty] private int _wordTypeIndex;
    [ObservableProperty] private bool _enableMainEntry;
    [ObservableProperty] private string _mainEntry = "";
    [ObservableProperty] private bool _enableMarked;
    [ObservableProperty] private bool _marked;
    [ObservableProperty] private IReadOnlyList<string> _wordTypeDisplays = [];

    public MultiEditViewModel(IEnumerable<string> wordTypeNames, UiTexts texts)
    {
        _texts = texts;
        WordTypeNames = wordTypeNames.ToList();
        RefreshTexts();
    }

    /// <summary>The word type names as stored in the database.</summary>
    public IReadOnlyList<string> WordTypeNames { get; }

    /// <summary>Returns the changes of the enabled fields of the dictionary word.</summary>
    /// <remarks>
    /// The main entry and the marked flag are not part of the dictionary word. An enabled word type without a
    /// selection is left unchanged.
    /// </remarks>
    public IDictionaryDao.UpdateData ToUpdateData(WordTypes types)
    {
        var data = new IDictionaryDao.UpdateData();
        if (EnableWord)
        {
            data.Word = Word;
        }
        if (EnablePre)
        {
            data.Pre = Pre;
        }
        if (EnablePost)
        {
            data.Post = Post;
        }
        if (EnableWordType && WordTypeIndex >= 0 && WordTypeIndex < WordTypeNames.Count)
        {
            data.WordType = types.GetWordType(WordTypeNames[WordTypeIndex]);
        }
        if (EnableMeaning)
        {
            data.Meaning = Meaning;
        }
        if (EnableAdditionalInfo)
        {
            data.AdditionalTargetLangInfo = AdditionalInfo;
        }
        if (EnableIrregular)
        {
            data.Irregular = Irregular;
        }
        return data;
    }

    /// <summary>Reloads the localized word type names and keeps the selected type.</summary>
    public void RefreshTexts()
    {
        var index = WordTypeIndex;
        WordTypeDisplays = WordTypeNames.Select(name => WordTypeTexts.Display(_texts, name)).ToList();
        WordTypeIndex = WordTypeNames.Count == 0 ? -1 : index;
    }
}
