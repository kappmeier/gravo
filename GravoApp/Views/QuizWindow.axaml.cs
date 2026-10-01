using System.ComponentModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using GravoApp.ViewModels;

namespace GravoApp.Views;

/// <summary>The quiz window view. All behavior lives in <c>QuizViewModel</c>.</summary>
/// <remarks>
/// The input box has the focus on opening and after each answer, once the answer command and its messages are
/// done. It selects its text when the view model asks for it.
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
            _viewModel.AnswerCommand.PropertyChanged -= OnAnswerCommandChanged;
        }
        _viewModel = DataContext as QuizViewModel;
        if (_viewModel is not null)
        {
            _viewModel.SelectInputRequested += SelectInput;
            _viewModel.AnswerCommand.PropertyChanged += OnAnswerCommandChanged;
        }
    }

    private void OnAnswerCommandChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IAsyncRelayCommand.IsRunning) && sender is IAsyncRelayCommand { IsRunning: false })
        {
            InputBox.Focus();
        }
    }

    private void SelectInput()
    {
        InputBox.Focus();
        InputBox.SelectAll();
    }
}
