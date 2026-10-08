using Gravo;
using GravoApp.Localization;
using GravoApp.Services;

namespace GravoApp;

public readonly record struct WindowGeometry(int X, int Y, int Width, int Height, WindowStateSetting State);

/// <summary>
/// The services every view model receives, built once by the composition root in <see cref="App"/>.
/// </summary>
public sealed record AppServices(
    IDataBaseOperation Db, string DbPath, VocabularyDatabase Vocabulary, ICardsDao Cards, IManagementDao Management,
    IPropertiesDao Properties, Settings Settings, ILocalization Localization, UiTexts Texts, IDialogService Dialogs)
{
    public const string MainLanguage = "german";

    /// <summary>
    /// Initializes the application services: Opens the databases for vocabulary and languages and loads the settings.
    /// </summary>
    /// <remarks>
    /// <paramref name="dialogs"/> receives the texts because the message boxes read their button labels from them.
    /// </remarks>
    public static AppServices Create(string dbPath, string languagesPath, Func<UiTexts, IDialogService> dialogs)
    {
        IDataBaseOperation db = new SQLiteDataBaseOperation();
        db.Open(dbPath);
        var loc = OpenLocalization(languagesPath);
        var texts = new UiTexts(loc);
        var settings = new Settings(new JsonFileSettingsStore(JsonFileSettingsStore.DefaultPath()));
        settings.LoadSettings();
        return new AppServices(db, dbPath, new VocabularyDatabase(db), CoreFactory.Cards(db),
            CoreFactory.Management(db), CoreFactory.Properties(db), settings, loc, texts, dialogs(texts));
    }

    /// <summary>
    /// Creates the localization over the languages database at <paramref name="languagesPath"/>.
    /// </summary>
    public static ILocalization OpenLocalization(string languagesPath)
    {
        IDataBaseOperation db = new SQLiteDataBaseOperation();
        db.Open(languagesPath);
        return new localization(db);
    }
}
