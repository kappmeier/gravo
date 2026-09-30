using Avalonia.Controls;

namespace GravoApp.Views;

/// <summary>The dialog that selects the language for a quiz.</summary>
/// <remarks>All behavior lives in <c>LanguageSelectViewModel</c>. The language box has the focus on opening.</remarks>
public partial class LanguageSelectWindow : Window
{
    public LanguageSelectWindow()
    {
        InitializeComponent();
        Opened += (_, _) => LanguageBox.Focus();
    }
}
