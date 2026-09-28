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
    public const string DatabaseFilter = "Datenbanken";
    public static string DatabaseNotFound(string path) => "Die Datenbank wurde nicht gefunden: " + path;

    // Main
    public static string CopyFailed(string message) => "Beim Kopieren ist ein Fehler aufgetreten: " + message;

    public const string CheckDatabaseQuestion =
        "Es wird versucht, Fehler automatisch zu Beheben. Für weitere Möglichkeiten benutzen Sie bitte das "
        + "Datenbank-Management. Wollen Sie den Test jetzt durchführen? Der Vorgang kann einige Minuten dauern.";

    public static string CheckDatabaseFixed(int count) =>
        "Testen der Datenbank auf Konsistenz abgeschlossen. Es wurden " + count + " Fehler behoben.";

    public const string CheckDatabaseClean =
        "Testen der Datenbank auf Konsistenz abgeschlossen. Es wurden keine Fehler gefunden.";

    // Options
    public const string OptionsTitle = "Optionen";
    public const string OptionsTestSection = "Standard-Test-Einstellungen";
    public const string OptionsTestTargetLanguage = "Frage Wörter in fremder Sprache ab";
    public const string OptionsTestSetPhrases = "Frage Redewendungen ab";
    public const string OptionsDisplaySection = "Anzeige Einstellungen";
    public const string OptionsSaveWindowPosition = "Fensterposition speichern";
    public const string OptionsStrategies = "Lernstrategien:";
    public const string OptionsUseCards = "Karteikartensystem";
    public const string OptionsInitialValue = "Startwert:";

    public const string OptionsCardsHint =
        "Hinweis: Das Karteikartensystem ist immer aktiviert, der Startwert ist 1. Diese Option ist erst in "
        + "späteren Versionen aktiviert.";

    public const string OptionsCopyCards = "Globale Karteikarten in Gruppen kopieren";
}
