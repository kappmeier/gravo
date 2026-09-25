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

    public string this[string constantName] => Codes.TryGetValue(constantName, out var code) ? Get(code) : constantName;

    /// <summary>
    /// Returns the text for <paramref name="code"/> supporting menu access keys.
    /// </summary>
    /// <remarks>
    /// WinForms mnemonics in the stored localization entries are converted into Avalonia access keys:
    /// "&amp;x" becomes "_x", "&amp;&amp;" becomes "&amp;", and a literal "_" is escaped as "__".
    /// The "&amp;&amp;" is never read as a mnemonic.
    /// </remarks>
    public string Get(int code) => ToAccessKeys(_loc.GetText(code));

    private static string ToAccessKeys(string text) =>
        Regex.Replace(text, "&&|&|_", m => m.Value switch { "&&" => "&", "&" => "_", _ => "__" });

    /// <summary>Re-reads every bound text. Necessary after <see cref="ILocalization.SwitchToLanguage"/>.</summary>
    public void Refresh()
    {
        OnPropertyChanged("Item[]");
        OnPropertyChanged(string.Empty);
    }
}
