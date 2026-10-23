using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using Gravo;

namespace GravoApp.Localization;

/// <summary>
/// Localized texts for XAML bindings, e.g. <c>{Binding Texts[MAIN_MENU_FILE]}</c>. The indexer takes the name of a
/// Public Const field of <see cref="localization"/>.
/// </summary>
public sealed class UiTexts : ObservableObject
{
    private static readonly Dictionary<string, int> Codes =
        typeof(localization).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.IsLiteral && f.FieldType == typeof(int))
            .ToDictionary(f => f.Name, f => (int)f.GetRawConstantValue()!);

    private readonly ILocalization _loc;

    public UiTexts(ILocalization loc) => _loc = loc;

    /// <summary>
    /// Returns the localized text for the constant <paramref name="constantName"/> with access keys.
    /// (See <see cref="localization"/>.)
    /// </summary>
    /// <remarks>
    /// XAML binds this indexer to menus and buttons. WinForms mnemonics in the stored entries become Avalonia access
    /// keys, so "&amp;x" becomes "_x", "&amp;&amp;" becomes "&amp;" and a literal "_" is escaped as "__". Unknown
    /// names return the name itself.
    /// </remarks>
    public string this[string constantName] =>
        Codes.TryGetValue(constantName, out var code) ? ToAccessKeys(_loc.GetText(code)) : constantName;

    /// <summary>
    /// Returns the text for <paramref name="code"/> as plain text for window titles, pickers and messages.
    /// </summary>
    /// <remarks>
    /// WinForms mnemonics are removed, so "&amp;x" becomes "x" and "&amp;&amp;" becomes "&amp;". A "_" stays as it is.
    /// </remarks>
    public string Get(int code) => Regex.Replace(_loc.GetText(code), "&(&?)", "$1");

    /// <summary>
    /// Formats the localized text for <paramref name="code"/> with the given values filled in placeholders.
    /// </summary>
    /// <remarks>
    /// Takes the plain text for <paramref name="code"/> and fills the <c>{0}</c>, <c>{1}</c> placeholders with the
    /// values from <paramref name="args"/>.
    /// </remarks>
    /// <param name="code">The <c>localization</c> constant of the text to format.</param>
    /// <param name="args">The values for the placeholders of the text.</param>
    public string Format(int code, params object?[] args) =>
        string.Format(CultureInfo.CurrentCulture, Get(code), args);

    /// <summary>
    /// Formats localized texts depending on the <paramref name="count"/> into the singular or the plural text.
    /// </summary>
    /// <param name="count">The count to determine singular or plural form.</param>
    /// <param name="oneCode">The localization code for the singular form.</param>
    /// <param name="manyCode">The localization code for the plural form, or zero.</param>
    public string Plural(int count, int oneCode, int manyCode) => Format(count == 1 ? oneCode : manyCode, count);

    private static string ToAccessKeys(string text) =>
        Regex.Replace(text, "&&|&|_", m => m.Value switch { "&&" => "&", "&" => "_", _ => "__" });

    /// <summary>Re-reads every bound text. Necessary after <see cref="ILocalization.SwitchToLanguage"/>.</summary>
    public void Refresh()
    {
        OnPropertyChanged("Item[]");
        OnPropertyChanged(string.Empty);
    }

    /// <summary>Returns the localized name of <paramref name="type"/>, or an empty text for an unknown type.</summary>
    public string DisplayWordType(WordType type) =>
        localization.WordTypeCodes.TryGetValue(type, out var code) ? Get(code) : "";
}
