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

    // Main
    public static string CopyFailed(string message) => "Beim Kopieren ist ein Fehler aufgetreten: " + message;

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
        "Sprache konnte nicht automatisch festgelegt werden. Bitte setzen Sie manuell.";

    public const string MainLanguageNotAutoSelected =
        "Hauptsprache konnte nicht automatisch festgelegt werden. Bitte setzen Sie manuell.";

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
    public const string ConfirmDeleteGroup = "Wollen Sie wirklich die komplette Gruppe löschen?";
    public const string NoCheckYet = "Gefundene und behobene Fehler: keine Überprüfung durchgeführt";
    public static string ErrorsFixed(int count) => "Gefundene und behobene Fehler: " + count;
    public const string ConsistencyCheckDone = "Testen der Datenbank auf Konsistenz abgeschlossen.";
    public const string NoImportDatabase = "Datenbank: noch keine gewählt";
    public static string ImportDatabase(string path) => "Datenbank: " + path;
    public const string PickExistingFile = "Bitte geben Sie eine existierende Datei an";

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
}
