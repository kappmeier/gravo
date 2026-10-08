using Avalonia.Controls;
using GravoApp.ViewModels;

namespace GravoApp.Views;

/// <summary>
/// The group input tab. The data behavior is in <c>GroupInputViewModel</c>, the view only restores the focus.
/// </summary>
/// <remarks>
/// After an add the focus goes back to where the user worked before: the search box gets the focus with its text
/// selected, or the word table keeps it and the search text is selected.
/// </remarks>
public partial class GroupInputView : UserControl
{
    private GroupInputViewModel? _viewModel;
    private Control? _lastFocused;

    public GroupInputView()
    {
        InitializeComponent();
        Control[] tracked = [GroupBox, SubGroupBox, LanguageBox, WordsInGroupList, MeaningsGrid, WordsGrid, SearchBox];
        foreach (var control in tracked)
        {
            control.GotFocus += (_, _) => _lastFocused = control;
        }
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        if (_viewModel is not null)
        {
            _viewModel.WordAdded -= OnWordAdded;
        }
        _viewModel = DataContext as GroupInputViewModel;
        if (_viewModel is not null)
        {
            _viewModel.WordAdded += OnWordAdded;
        }
    }

    private void OnWordAdded()
    {
        if (_lastFocused == SearchBox)
        {
            SearchBox.SelectAll();
            SearchBox.Focus();
        }
        else if (_lastFocused == WordsGrid)
        {
            SearchBox.SelectAll();
            WordsGrid.Focus();
        }
    }
}
