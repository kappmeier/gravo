using Avalonia.Controls;
using GravoApp.ViewModels;

namespace GravoApp.Views;

/// <summary>
/// The dialog that adds words to the dictionary. All behavior is implemented in <c>WordInputViewModel</c>.
/// </summary>
/// <remarks>
/// The main entry box has the focus on opening. After each add it gets the focus back with its text selected, so
/// the next word can be typed immediately.
/// </remarks>
public partial class WordInputWindow : Window
{
    private WordInputViewModel? _viewModel;

    public WordInputWindow()
    {
        InitializeComponent();
        Opened += (_, _) => MainEntryBox.Focus();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        if (_viewModel is not null)
        {
            _viewModel.SelectMainEntryRequested -= SelectMainEntry;
        }
        _viewModel = DataContext as WordInputViewModel;
        if (_viewModel is not null)
        {
            _viewModel.SelectMainEntryRequested += SelectMainEntry;
        }
    }

    private void SelectMainEntry()
    {
        MainEntryBox.Focus();
        MainEntryBox.SelectAll();
    }
}
