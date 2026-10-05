using Avalonia.Controls;
using GravoApp.ViewModels.Explorer;

namespace GravoApp.Views;

/// <summary>Shows the word panel of the vocabulary explorer as defined by the <c>WordEditorViewModel</c>.</summary>
/// <remarks>
/// The view shares the <c>ExplorerViewModel</c> of the explorer. After a word was added, the pre box gets the focus
/// with its text selected.
/// </remarks>
public partial class WordEditorView : UserControl
{
    private ExplorerViewModel? _viewModel;

    public WordEditorView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        if (_viewModel is not null)
        {
            _viewModel.WordAdded -= SelectPre;
        }
        _viewModel = DataContext as ExplorerViewModel;
        if (_viewModel is not null)
        {
            _viewModel.WordAdded += SelectPre;
        }
    }

    private void SelectPre()
    {
        PreBox.Focus();
        PreBox.SelectAll();
    }
}
