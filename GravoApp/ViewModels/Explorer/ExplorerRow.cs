using Gravo;

namespace GravoApp.ViewModels.Explorer;

/// <summary>
/// Holds one row of the selected list in the vocabulary explorer. Each row includes the cells of every column layout.
/// </summary>
/// <remarks>
/// A row fills only the cells of its <see cref="ListStyle"/>. The <see cref="Payload"/> is the <c>WordEntry</c> of a
/// dictionary row, the <c>TestWord</c> of a group row, and <c>null</c> for the overview rows. Rows compare by
/// reference, so two rows with the same cells stay distinct for the selection.
/// </remarks>
public sealed class ExplorerRow
{
    public ExplorerRow(object? payload) => Payload = payload;

    public object? Payload { get; }
    public string Pre { get; init; } = "";
    public string Word { get; init; } = "";
    public string Post { get; init; } = "";
    public string Meaning { get; init; } = "";
    public string WordType { get; init; } = "";
    public string AdditionalInfo { get; init; } = "";
    public string Irregular { get; init; } = "";
    public string Marked { get; init; } = "";
    public string SubGroup { get; init; } = "";
    public string MainLanguage { get; init; } = "";
    public string Language { get; init; } = "";
    public int Count1 { get; init; }
    public int Count2 { get; init; }
    public string GroupName { get; init; } = "";
    public int SubGroupCount { get; init; }
    public int LanguageCount { get; init; }

    /// <summary>Returns the dictionary word behind the row, or <c>null</c> for an overview row.</summary>
    public WordEntry? Entry => Payload as WordEntry ?? (Payload as TestWord)?.WordEntry;
}
