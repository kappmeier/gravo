using Avalonia.Controls;

namespace GravoApp.Views;

/// <summary>The data management dialog view. All behavior is implemented in <c>ManagementViewModel</c>.</summary>
/// <remarks>The selected group has the focus on opening.</remarks>
public partial class ManagementWindow : Window
{
    public ManagementWindow()
    {
        InitializeComponent();
        // A ListBox is not focusable itself; its items are.
        Opened += (_, _) => GroupList.ContainerFromIndex(Math.Max(GroupList.SelectedIndex, 0))?.Focus();
    }
}
