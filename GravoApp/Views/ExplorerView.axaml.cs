using Avalonia.Controls;
using GravoApp.ViewModels.Explorer;

namespace GravoApp.Views;

/// <summary>
/// Shows the vocabulary explorer as a tab. Its data behavior is defined in <c>ExplorerViewModel</c>.
/// </summary>
/// <remarks>
/// The view copies the selected rows of the list into <c>SelectedRows</c> because a binding cannot do that.
/// </remarks>
public partial class ExplorerView : UserControl
{
    public ExplorerView()
    {
        InitializeComponent();
        RowsGrid.SelectionChanged += OnRowsSelectionChanged;
    }

    private void OnRowsSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is not ExplorerViewModel vm)
        {
            return;
        }
        vm.SelectedRows.Clear();
        foreach (var row in RowsGrid.SelectedItems.OfType<ExplorerRow>())
        {
            vm.SelectedRows.Add(row);
        }
    }
}
