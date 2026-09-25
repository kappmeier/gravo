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
}
