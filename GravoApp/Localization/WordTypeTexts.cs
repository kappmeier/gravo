using Gravo;

namespace GravoApp.Localization;

public static class WordTypeTexts
{
    private static readonly Dictionary<string, string> Codes = new()
    {
        [nameof(WordType.Substantive)] = "WORD_TYPE_SUBSTANTIVE",
        [nameof(WordType.Verb)] = "WORD_TYPE_VERB",
        [nameof(WordType.Adjective)] = "WORD_TYPE_ADJECTIVE",
        [nameof(WordType.Simple)] = "WORD_TYPE_SIMPLE",
        [nameof(WordType.Adverb)] = "WORD_TYPE_ADVERB",
        [nameof(WordType.SetPhrase)] = "WORD_TYPE_SET_PHRASE",
        [nameof(WordType.Example)] = "WORD_TYPE_EXAMPLE",
    };

    /// <summary>
    /// Display name of a word type as stored in <c>SupportedWordTypes</c>. The name is either a database key
    /// (<c>WORD_TYPE_VERB</c>) or an enum name (<c>Verb</c>). Other names are displayed unchanged.
    /// </summary>
    public static string Display(UiTexts texts, string wordTypeName) =>
        Codes.ContainsValue(wordTypeName) ? texts[wordTypeName]
        : Codes.TryGetValue(wordTypeName, out var code) ? texts[code]
        : wordTypeName;
}
