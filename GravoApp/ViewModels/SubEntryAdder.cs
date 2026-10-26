using Gravo;
using GravoApp.Localization;
using GravoApp.Services;

namespace GravoApp.ViewModels;

/// <summary>Adds a word to the dictionary for the word input and the explorer.</summary>
/// <remarks>
/// A missing main entry is created after the user confirms, then the word is added. Errors are shown as messages.
/// </remarks>
public sealed class SubEntryAdder
{
    /// <summary>The outcome of an attempt to add a sub entry. See <see cref="AddAsync"/>.</summary>
    public enum Result
    {
        Added,
        AlreadyStored,
        NotAdded,
    }

    private readonly IDictionaryDao _dictionary;
    private readonly IDialogService _dialogs;
    private readonly UiTexts _texts;

    public SubEntryAdder(IDictionaryDao dictionary, IDialogService dialogs, UiTexts texts)
    {
        _dictionary = dictionary;
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
    /// <c>AlreadyStored</c> when the dictionary holds the word already, <c>NotAdded</c> after an error or a declined
    /// question.
    /// </returns>
    public async Task<Result> AddAsync(WordEntry entry, string mainEntry, string language, string mainLanguage,
        string missingMessage)
    {
        try
        {
            _dictionary.AddSubEntry(entry, mainEntry, language, mainLanguage);
            return Result.Added;
        }
        catch (InputException ex)
        {
            await _dialogs.ShowMessageAsync(_texts.Get(localization.INVALID_INPUT_TITLE), ex.Message);
            return Result.NotAdded;
        }
        catch (EntryExistsException)
        {
            return Result.AlreadyStored;
        }
        catch (EntryNotFoundException notFound)
        {
            if (!await _dialogs.ConfirmAsync(_texts.Get(localization.MAIN_ENTRY_MISSING_TITLE), missingMessage))
            {
                return Result.NotAdded;
            }
            return await AddMainEntryAndRetryAsync(entry, mainEntry, language, mainLanguage, notFound);
        }
        catch (Exception ex)
        {
            await _dialogs.ShowMessageAsync(_texts.Get(localization.ERROR_TITLE),
                _texts.Format(localization.ENTRY_CONFLICT, ex.Message));
            return Result.NotAdded;
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
    private async Task<Result> AddMainEntryAndRetryAsync(WordEntry entry, string mainEntry, string language,
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
            return Result.Added;
        }
        catch (Exception)
        {
            await _dialogs.ShowMessageAsync(_texts.Get(localization.ERROR_TITLE),
                _texts.Format(localization.ENTRY_CONFLICT, notFound.Message));
            return Result.NotAdded;
        }
    }
}
