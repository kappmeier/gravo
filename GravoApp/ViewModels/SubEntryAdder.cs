using Gravo;
using GravoApp.Localization;
using GravoApp.Services;

namespace GravoApp.ViewModels;

/// <summary>
/// Adds a word to the dictionary and to a group. Central place, called by the word input and the explorer.
/// </summary>
/// <remarks>
/// A missing main entry is created after the user confirms, then the word is added. Errors are shown as messages.
/// </remarks>
public sealed class SubEntryAdder
{
    private readonly IDictionaryDao _dictionary;
    private readonly IGroupDao _group;
    private readonly IDialogService _dialogs;
    private readonly UiTexts _texts;

    public SubEntryAdder(IDictionaryDao dictionary, IGroupDao group, IDialogService dialogs, UiTexts texts)
    {
        _dictionary = dictionary;
        _group = group;
        _dialogs = dialogs;
        _texts = texts;
    }

    /// <summary>Adds <paramref name="entry"/> to <paramref name="mainEntry"/>.</summary>
    /// <param name="entry">The word to add.</param>
    /// <param name="mainEntry">The trimmed main entry.</param>
    /// <param name="language">The language of the word.</param>
    /// <param name="mainLanguage">The main language of the word.</param>
    /// <param name="missingMessage">The question shown when the main entry does not exist.</param>
    /// <returns>
    /// The stored word. It is the passed <paramref name="entry"/> when it was added, or the dictionary's word when it
    /// was stored already, and <c>null</c> after an error or a declined question.
    /// </returns>
    public async Task<WordEntry?> AddAsync(WordEntry entry, string mainEntry, string language, string mainLanguage,
        string missingMessage)
    {
        try
        {
            _dictionary.AddSubEntry(entry, mainEntry, language, mainLanguage);
            return entry;
        }
        catch (InputException ex)
        {
            await _dialogs.ShowMessageAsync(_texts.Get(localization.INVALID_INPUT_TITLE), ex.Message);
            return null;
        }
        catch (EntryExistsException)
        {
            var main = _dictionary.GetMainEntry(mainEntry, language, mainLanguage);
            return _dictionary.GetEntry(main, entry.Word, entry.Meaning);
        }
        catch (EntryNotFoundException notFound)
        {
            if (!await _dialogs.ConfirmAsync(_texts.Get(localization.MAIN_ENTRY_MISSING_TITLE), missingMessage))
            {
                return null;
            }
            return await AddMainEntryAndRetryAsync(entry, mainEntry, language, mainLanguage, notFound);
        }
        catch (Exception ex)
        {
            await _dialogs.ShowMessageAsync(_texts.Get(localization.ERROR_TITLE),
                _texts.Format(localization.ENTRY_CONFLICT, ex.Message));
            return null;
        }
    }

    /// <summary>Adds <paramref name="word"/> to <paramref name="group"/>.</summary>
    /// <remarks>A word that is in the group already is reported with a message.</remarks>
    /// <param name="group">The sub group to add the word to.</param>
    /// <param name="word">The stored word.</param>
    /// <param name="marked">Whether the word is marked in the group.</param>
    public async Task AddToGroupAsync(GroupEntry group, WordEntry word, bool marked)
    {
        try
        {
            _group.Add(group, word, marked, "");
        }
        catch (EntryExistsException)
        {
            await _dialogs.ShowMessageAsync(_texts.Get(localization.ADD_NOT_POSSIBLE_TITLE),
                _texts.Get(localization.WORD_ALREADY_IN_GROUP));
        }
    }

    /// <summary>Creates a missing main entry and adds the sub entry again.</summary>
    /// <remarks>
    /// A main entry that exists by now is used as it is. The conflict message names the original
    /// <paramref name="notFound"/> error.
    /// </remarks>
    /// <param name="entry">The sub entry to add.</param>
    /// <param name="mainEntry">The trimmed main entry.</param>
    /// <param name="language">The language of the word.</param>
    /// <param name="mainLanguage">The main language of the word.</param>
    /// <param name="notFound">The error of the first attempt.</param>
    private async Task<WordEntry?> AddMainEntryAndRetryAsync(WordEntry entry, string mainEntry, string language,
        string mainLanguage, EntryNotFoundException notFound)
    {
        try
        {
            _dictionary.AddEntry(mainEntry, language, mainLanguage);
        }
        catch (EntryExistsException)
        {
            // Created in the meantime, the retry below uses it.
        }
        catch (Exception ex) when (ex is LanguageNotFoundException or EntryNotFoundException or InputException)
        {
            await _dialogs.ShowMessageAsync(_texts.Get(localization.INVALID_INPUT_TITLE), ex.Message);
        }
        try
        {
            _dictionary.AddSubEntry(entry, mainEntry, language, mainLanguage);
            return entry;
        }
        catch (Exception)
        {
            await _dialogs.ShowMessageAsync(_texts.Get(localization.ERROR_TITLE),
                _texts.Format(localization.ENTRY_CONFLICT, notFound.Message));
            return null;
        }
    }
}
