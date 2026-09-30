using Avalonia.Controls;

namespace GravoApp.Views;

/// <summary>The dialog that selects the group for a quiz.</summary>
/// <remarks>All behavior lives in <c>TestSelectViewModel</c>. The group box has the focus on opening.</remarks>
public partial class TestSelectWindow : Window
{
    public TestSelectWindow()
    {
        InitializeComponent();
        Opened += (_, _) => GroupBox.Focus();
    }
}
