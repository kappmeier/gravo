using Gravo;
using GravoApp.Localization;
using GravoApp.Services;

namespace GravoApp.ViewModels;

/// <summary>
/// Adds a word to the dictionary and to a group. Central place, called by the word input and the explorer.
/// </summary>
/// <remarks>
/// A missing main entry is created only when confirmed. The word, the optional new main entry, and the group row are
/// written in one transaction. Errors are shown as messages, one per failed add.
/// </remarks>
public sealed class SubEntryAdder
{
    private readonly VocabularyDatabase _vocabulary;
    private readonly IDialogService _dialogs;
    private readonly UiTexts _texts;

    public SubEntryAdder(VocabularyDatabase vocabulary, IDialogService dialogs, UiTexts texts)
    {
        _vocabulary = vocabulary;
        _dialogs = dialogs;
        _texts = texts;
    }

    /// <summary>
    /// Adds <paramref name="entry"/> to <paramref name="mainEntry"/> and to <paramref name="group"/>. The dictionary
    /// stores its text fields trimmed.
    /// </summary>
    /// <param name="entry">The word to add.</param>
    /// <param name="mainEntry">The trimmed main entry.</param>
    /// <param name="language">The language of the word.</param>
    /// <param name="mainLanguage">The main language of the word.</param>
    /// <param name="missingMessage">The question shown when the main entry does not exist.</param>
    /// <param name="group">The sub group to add the word to, or <c>null</c> to add it to no group.</param>
    /// <param name="marked">Whether the word is marked in the group.</param>
    /// <returns>
    /// The added entry with its index, or the dictionary's word when it was stored already. It is <c>null</c> after
    /// an error or a declined question.
    /// </returns>
    public async Task<WordEntry?> AddAsync(WordEntry entry, string mainEntry, string language, string mainLanguage,
        string missingMessage, GroupEntry? group, bool marked)
    {
        try
        {
            return _vocabulary.AddWord(entry, mainEntry, language, mainLanguage, false, group, marked);
        }
        catch (InputException ex)
        {
            await _dialogs.ShowMessageAsync(_texts.Get(localization.INVALID_INPUT_TITLE), ex.Message);
            return null;
        }
        catch (EntryExistsException)
        {
            return await AlreadyInGroupAsync(entry, mainEntry, language, mainLanguage);
        }
        catch (EntryNotFoundException notFound)
        {
            if (!await _dialogs.ConfirmAsync(_texts.Get(localization.MAIN_ENTRY_MISSING_TITLE), missingMessage))
            {
                return null;
            }
            try
            {
                return _vocabulary.AddWord(entry, mainEntry, language, mainLanguage, true, group, marked);
            }
            catch (EntryExistsException)
            {
                return await AlreadyInGroupAsync(entry, mainEntry, language, mainLanguage);
            }
            catch (Exception ex) when (ex is LanguageNotFoundException or EntryNotFoundException or InputException)
            {
                await _dialogs.ShowMessageAsync(_texts.Get(localization.INVALID_INPUT_TITLE), ex.Message);
                return null;
            }
            catch (Exception)
            {
                await _dialogs.ShowMessageAsync(_texts.Get(localization.ERROR_TITLE),
                    _texts.Format(localization.ENTRY_CONFLICT, notFound.Message));
                return null;
            }
        }
        catch (Exception ex)
        {
            await _dialogs.ShowMessageAsync(_texts.Get(localization.ERROR_TITLE),
                _texts.Format(localization.ENTRY_CONFLICT, ex.Message));
            return null;
        }
    }

    /// <summary>Shows the "word already in group" message and returns the stored word.</summary>
    private async Task<WordEntry> AlreadyInGroupAsync(WordEntry entry, string mainEntry, string language,
        string mainLanguage)
    {
        await _dialogs.ShowMessageAsync(_texts.Get(localization.ADD_NOT_POSSIBLE_TITLE),
            _texts.Get(localization.WORD_ALREADY_IN_GROUP));
        var main = _vocabulary.Dictionary.GetMainEntry(mainEntry, language, mainLanguage);
        return _vocabulary.Dictionary.GetEntry(main, entry.Word, entry.Meaning);
    }
}
