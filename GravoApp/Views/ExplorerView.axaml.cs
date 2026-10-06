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
    private ExplorerViewModel? _viewModel;

    public ExplorerView()
    {
        InitializeComponent();
        RowsGrid.SelectionChanged += OnRowsSelectionChanged;
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        if (_viewModel is not null)
        {
            _viewModel.RowsReplaced -= SelectRows;
        }
        _viewModel = DataContext as ExplorerViewModel;
        if (_viewModel is not null)
        {
            _viewModel.RowsReplaced += SelectRows;
        }
    }

    private void SelectRows(IReadOnlyList<ExplorerRow> rows)
    {
        RowsGrid.SelectedItems.Clear();
        foreach (var row in rows)
        {
            RowsGrid.SelectedItems.Add(row);
        }
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
