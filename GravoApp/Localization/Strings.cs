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

    // TestSelect
    public static string WordsToTest(int count) =>
        count == 1 ? count + " Vokabel abzufragen." : count + " Vokabeln abzufragen.";

    // WordInput
    public const string SelectExistingGroup =
        "Bitte wählen sie eine existierende Gruppe aus. Eintrag wird nicht erstellt!";git

    public const string NewLanguageTitle = "Neue Sprache";

    public static string NoEntryInGroupYet(string language, string mainLanguage) =>
        "Es ist bisher noch kein Eintrag in der gewählten Gruppe vorhanden. Soll ein neuer Eintrag mit den Sprachen '"
        + language + "' und '" + mainLanguage + "' erstellt werden?";

    public static string SecondLanguageInGroup(string language, string mainLanguage) =>
        "Sie beabsichtigen einen eintrag mit den zweiten Sprachen '" + language + "' und '" + mainLanguage
        + "' zu erstellen. Soll damit fortgefahren werden?";

    public const string MainEntryMissingTitle = "Haupteintrag nicht vorhanden";

    public static string MainEntryMissing(string mainEntry) =>
        "Der Haupteintrag " + mainEntry + " ist für die gewählten Sprachen nicht vorhanden. Soll er erstellt werden?";

    public static string EntryConflict(string message) =>
        "Eintrag nicht möglich, konflikt mit Index wahrscheinlich. Überprüfen Sie Ihre Datenbankversion."
        + Environment.NewLine + "Fehler: " + message;

    public const string LanguageNotAutoSelected =
        "Sprache konnte nicht automatisch festgelegt werden. Bitte setzen sie manuell.";

    public const string MainLanguageNotAutoSelected =
        "Hauptsprache konnte nicht automatisch festgelegt werden. Bitte setzen sie manuell.";

    public const string AddNotPossibleTitle = "Hinzufügen nicht möglich";
    public const string WordAlreadyInGroup = "Wort bereits in der Gruppe enthalten";
    public const string WordInputSection = "Eintrag hinzufügen:";
    public const string LabelMainEntry = "Haupteintrag:";
    public const string LabelSubEntry = "Untereintrag:";
    public const string LabelPre = "Pre:";
    public const string LabelPost = "Post:";
    public const string LabelMeaning = "Bedeutung:";
    public const string LabelWordInfo = "Vokabelinfo:";
    public const string LabelWordType = "Worttyp:";
    public const string Irregular = "Unregelmäßig";
    public const string ImportantWord = "Wichtiges Wort";
    public const string DirectAdd = "Vokabeln sofort einer Gruppe hinzufügen";
    public const string LabelLanguage = "Sprache:";
    public const string LabelMainLanguage = "Hauptsprache:";
    public const string NewLanguages = "Neue Sprachen anlegen";

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

    // Info
    public const string InfoTitle = "Gravo 7 Sprachtrainer info";
    public const string InfoGermanCheckbox = "Deutsch";
    public const string InfoProductName = "Gravo";
    public const string InfoCopyrightName = "Jan-Philipp Kappmeier";
    public const string InfoCopyright = "© 1995-2026";
    public const string InfoLink = "http://www.kappmeier.de";
    public static string InfoVersion(string version) => "Version: " + version;
    public static string InfoDbVersion(string version) => "DB-Version: " + version;
    public const string InfoCopyrightOldEnglish = "based on Vokabeltrainer, © 1995-2007";
    public const string InfoCopyrightOldGerman = "basiert auf Vokabeltrainer, © 1995-2007";
    public const string InfoDisclaimerEnglish =
        "Working with this version of Gravo should be possible without problems, however, some smaller errors "
        + "could occur. We recommend to save your database whenever you've added some vocabulary and to not "
        + "change the database manually. This software is distributed \"as is\", we are neither responisble for "
        + "anything that happens using this software nor responsible for the correct function of this piece of "
        + "software.";

    public const string InfoDisclaimerGerman =
        "Das Arbeiten mit dieser Version von Gravo sollte problemlos möglich sein, dennoch können noch kleinere "
        + "Fehler auftreten. Wir emfehlen nach jeder Vokabeleingabe die Datenbank zu sichern und keine Änderungen "
        + "an der Datenbank manuell durchzufüren. Diese Software wird vertrieben \"wie sie ist\", wir sind nicht "
        + "verantwortlich für das, was durch Benutzung dieser Software geschieht, noch kann die "
        + "Funktionsfähigkeit dieser Software garantiert werden.";
}
