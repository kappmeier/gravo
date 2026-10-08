using Avalonia.Controls;

namespace GravoApp.Views;

/// <summary>The "Info" dialog view. All behavior lives in <c>InfoViewModel</c>.</summary>
/// <remarks>The close button has the focus on opening.</remarks>
public partial class InfoWindow : Window
{
    public InfoWindow()
    {
        InitializeComponent();
        Opened += (_, _) => CloseButton.Focus();
    }
}
