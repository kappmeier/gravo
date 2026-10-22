namespace GravoApp.Localization;

/// <summary>
/// Hard-coded German literals, kept here per until they move into languages.s3db.
/// Grouped by screen.
/// </summary>
/// <remarks>
/// TODO: Move these literals into languages.s3db.
/// </remarks>
public static class Strings
{
    public const string ErrorTitle = "Fehler";
    public const string HintTitle = "Hinweis";
    public const string WarningTitle = "Warnung";
    public const string InvalidInputTitle = "Fehlerhafte Eingabe";

    // Explorer
    public const string SelectOnlyOne = "Bitte nur einen Eintrag markieren!";
    public const string SelectOne = "Sie müssen einen Eintrag markieren";

    public const string TooManyLanguagesInGroup =
        "Zu viele Sprachen in der Gruppe. Die Sprache kann nicht automatisch festgelegt werden! Eintrag wird nicht "
        + "hinzugefügt.";

    public const string TooManyMainLanguagesInGroup =
        "Zu viele Zielsprachen in der Gruppe. Die Sprache kann nicht automatisch festgelegt werden! Eintrag wird "
        + "nicht hinzugefügt.";

    public const string CannotAddHere = "Eintrag kann nicht hinzugefügt werden.";
    public const string LanguageRequired = "Bitte geben sie eine Sprache und eine Hauptsprache an.";

    public static string MainEntryMissingForLanguages(string mainEntry, string mainLanguage, string language) =>
        "Der Haupteintrag '" + mainEntry + "' ist für die gewählten Sprachen '" + mainLanguage + "' und '" + language
        + "' nicht vorhanden. Soll er erstellt werden?";

    public static string EntryExists(string word) => "Eintrag existiert bereits: " + word;
    public const string RenameTitle = "Umbenennen";
    public const string NewName = "Neuer Name:";
    public static string CouldNotRename(string name) => "\"" + name + "\" konnte nicht umbenannt werden.";
    public const string ChangesNotApplied = "Änderungen werden nicht übernommen";
    public const string EntryAlreadyExists = "Eintrag existiert bereits.";

    // WordInput
    public const string MainEntryMissingTitle = "Haupteintrag nicht vorhanden";

    public static string EntryConflict(string message) =>
        "Eintrag nicht möglich, konflikt mit Index wahrscheinlich. Überprüfen Sie Ihre Datenbankversion."
        + Environment.NewLine + "Fehler: " + message;

    public const string AddNotPossibleTitle = "Hinzufügen nicht möglich";
    public const string WordAlreadyInGroup = "Wort bereits in der Gruppe enthalten";

    // Management
    public const string ProductName = "Gravo";
    public const string EnglishWarningTitle = "Warning";
    public const string EnglishErrorTitle = "Error";
}
