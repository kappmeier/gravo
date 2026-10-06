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
    public const string SelectExistingGroup =
        "Bitte wählen sie eine existierende Gruppe aus. Eintrag wird nicht erstellt!";

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

    // GroupInput
    public static string EntriesInLanguage(int count) =>
        count + (count == 1 ? " Eintrag" : " Einträge") + " in der Sprache.";

    public static string DistinctEntriesInGroup(int count) =>
        count + (count == 1 ? " verschiedener Eintrag" : " verschiedene Einträge");

    public static readonly string InTheGroupSeparator = " in der Gruppe," + Environment.NewLine;

    public static string EntriesTotal(int count) =>
        count + (count == 1 ? " Eintrag insgesamt" : " Einträge insgesamt") + ".";

    public static string EntriesInWholeGroup(int count) =>
        count + (count == 1 ? " Eintrag" : " Einträge") + " in der Gruppe insgesamt.";

    public const string ReselectWord = "Bitte wählen sie das Wort erneut aus!";
    public const string ErrorOccurredTitle = "Fehler aufgetreten!";
    public const string GroupRowsForWord = "Einträge in der Gruppe zum gewählten Wort:";
    public const string DictionaryRowsForWord = "Einträge in der Datenbank zum gewählten Wort:";
    public const string SearchEntry = "Eintrag in der Datenbank suchen:";
    public const string Mark = "Markieren";
    public const string SelectWord = "<<";
    public const string DeselectWord = ">>";
    public const string EntryPre = "Pre";
    public const string EntryWord = "Word";
    public const string EntryPost = "Post";
    public const string EntryMeaning = "Bedeutung";

    // TestSelect
    public static string WordsToTest(int count) =>
        count == 1 ? count + " Vokabel abzufragen." : count + " Vokabeln abzufragen.";

    // Management
    public const string ManagementTitle = "Daten-Management";
    public const string ProductName = "Gravo";
    public const string EnglishWarningTitle = "Warning";
    public const string EnglishErrorTitle = "Error";
    public const string GroupsTab = "Gruppen";
    public const string UnitsTab = "Lektionen";
    public const string DatabaseTab = "Datenbank";
    public const string ImportTab = "Importieren";
    public const string Export = "Exportieren";
    public const string Delete = "Löschen";
    public const string MoveUp = "Nach oben";
    public const string MoveDown = "Nach unten";
    public const string SaveDatabase = "Datenbank sichern";
    public const string CheckConsistency = "Konsistenz prüfen";
    public const string SelectFile = "Datei auswählen";
    public const string ImportGroupsButton = "Importiere Gruppe";
    public const string ImportDictionaryButton = "Imporiere Wörterbuch";
    public const string SkipEmptyMains = "leere Haupteinträge auslassen";

    public const string ImportMayTakeTime =
        "Das Importieren kann einige Zeit dauern, da jeder Datensatz einzeln eingelesen und dabei auf Kohärenz "
        + "geprüft wird.";

    public const string NoGroup = "Keine Gruppe vorhanden";
    public static string Entries(int count) => count == 1 ? count + " Eintrag" : count + " Einträge";

    public static string UsedLanguages(int count) =>
        count == 1 ? count + " benutzte Sprache" : count + " benutzte Sprachen";

    public const string DefaultUnit = "Untereintrag 1";
    public const string GroupNameTaken = "Gruppen können nur einmal unter einem Namen existieren.";
    public static string ErrorOccurred(string message) => "Ein Fehler ist aufgetreten: " + message;
    public const string ConfirmDeleteGroup = "Wollen sie wirklich die komplette Gruppe löschen?";
    public const string NoCheckYet = "Gefundene und behobene Fehler: keine Überprüfung durchgeführt";
    public static string ErrorsFixed(int count) => "Gefundene und behobene Fehler: " + count;
    public const string ConsistencyCheckDone = "Testen der Datenbank auf Konsistenz abgeschlossen.";
    public const string NoImportDatabase = "Datenbank: noch keine gewählt";
    public static string ImportDatabase(string path) => "Datenbank: " + path;
    public const string PickExistingFile = "Bitte geben sie eine existierende Datei an";

    public const string DatabaseOutdated =
        "Ihre Datenbank ist nicht aktuell. Bitte aktualisieren Sie sie bevor Sie Daten exportieren.";

    public static string DatabaseAccessFailed(string message) => "Fehler beim Datenbankzugriff: " + message;

    public const string ImportSourceOutdated =
        "Die Version der zu importierenden Datenbank ist nicht aktuell. Soll sie aktualisiert werden?";

    /// <summary>Returns the dictionary counters of an import, empty counts before the first import.</summary>
    public static string ImportedDictionary(int? mainEntries, int? subEntries) =>
        "Importierte Haupteinträge: " + mainEntries + Environment.NewLine + "Importierte Untereinträge: " + subEntries;

    /// <summary>Returns the group counters of an import, empty counts before the first import.</summary>
    public static string ImportedGroups(int? groups, int? subGroups, int? groupEntries) =>
        "Importierte Gruppen: " + groups + Environment.NewLine + "Importierte Untergruppen: " + subGroups
        + Environment.NewLine + "Importierte Gruppeneinträge: " + groupEntries;

    public const string ImportDone = "Importieren erfolgreich!";
    public const string ExportDone = "Exportieren erfolgreich!";
    public const string UpdateMayTakeTime = "Der Updatevorgang kann einige Zeit dauern!";
    public static string OnCurrentVersion(string version) => "Auf aktueller Version " + version;
    public static string UpdateToVersion(string version) => "Update auf Version " + version;

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
