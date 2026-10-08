using Avalonia.Controls;
using GravoApp.ViewModels;

namespace GravoApp.Views;

/// <summary>The quiz window view. All behavior lives in <c>QuizViewModel</c>.</summary>
/// <remarks>
/// The input box has the focus on opening and after each answer once its messages are closed. It selects its text
/// when the view model requests it.
/// </remarks>
public partial class QuizWindow : Window
{
    private QuizViewModel? _viewModel;

    public QuizWindow()
    {
        InitializeComponent();
        Opened += (_, _) => InputBox.Focus();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        if (_viewModel is not null)
        {
            _viewModel.SelectInputRequested -= SelectInput;
            _viewModel.FocusInputRequested -= FocusInput;
        }
        _viewModel = DataContext as QuizViewModel;
        if (_viewModel is not null)
        {
            _viewModel.SelectInputRequested += SelectInput;
            _viewModel.FocusInputRequested += FocusInput;
        }
    }

    private void FocusInput() => InputBox.Focus();

    private void SelectInput()
    {
        InputBox.Focus();
        InputBox.SelectAll();
    }
}
