namespace GravoApp.ViewModels;

/// <summary>A row of a word table that shows the pre text, word, post text and meaning of a word.</summary>
/// <remarks>
/// <c>Payload</c> is the Core object the row was made from, a <c>WordEntry</c> or a <c>TestWord</c>.
/// </remarks>
public sealed record WordRow(string Pre, string Word, string Post, string Meaning, object Payload);
