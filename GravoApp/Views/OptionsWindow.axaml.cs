using Avalonia.Controls;

namespace GravoApp.Views;

/// <summary>The "Options" dialog view. All behavior lives in <c>OptionsViewModel</c>.</summary>
/// <remarks>The OK button has the focus on opening.</remarks>
public partial class OptionsWindow : Window
{
    public OptionsWindow()
    {
        InitializeComponent();
        Opened += (_, _) => OkButton.Focus();
    }
}
