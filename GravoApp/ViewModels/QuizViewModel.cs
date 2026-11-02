using System.ComponentModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gravo;
using GravoApp.Localization;
using GravoApp.Services;

namespace GravoApp.ViewModels;

/// <summary>
/// The quiz window actually performing a vocabulary test. Asks all the words of a <c>TestController</c> one by one.
/// </summary>
/// <remarks>
/// A wrong, misspelled or other-meaning answer asks the same word again with a hint. The window closes after the
/// last word with a "well done" message.
/// </remarks>
public sealed partial class QuizViewModel : ViewModelBase
{
    private readonly TestController _controller;
    private readonly IDialogService _dialogs;
    private Checker? _checker;
    private Func<string> _feedbackText = () => "";

    [ObservableProperty] private string _question = "";
    [ObservableProperty] private string _info = "";
    [ObservableProperty] private string _feedback = "";
    [ObservableProperty] private string _input = "";
    [ObservableProperty] private string _countText = "";

    public QuizViewModel(TestController controller, UiTexts texts, IDialogService dialogs)
    {
        _controller = controller;
        _dialogs = dialogs;
        Texts = texts;
        _checker = controller.GetTestChecker();
        Title = texts.Get(localization.TEST_TITLE);
        texts.PropertyChanged += OnTextsChanged;
    }

    public UiTexts Texts { get; }

    /// <summary>An event raised when the view should select the whole input text.</summary>
    public event Action? SelectInputRequested;

    /// <summary>An event raised when the view should focus the input box after an answer.</summary>
    /// <remarks>It is raised when messages are closed, but not when the quiz is finished.</remarks>
    public event Action? FocusInputRequested;

    /// <summary>Shows the first word, or finishes the quiz at once when there is no word to ask.</summary>
    public async Task StartAsync()
    {
        if (await CheckForQuitAsync())
        {
            return;
        }
        DisplayCurrentWord();
    }

    [RelayCommand]
    private async Task AnswerAsync()
    {
        if (_checker is null)
        {
            return;
        }
        var result = _checker.Evaluate(Input.Trim());
        var oldChecker = _checker;
        _controller.Update(result);
        _checker = _controller.GetTestChecker();
        if (_checker is not null && _checker.Retest)
        {
            switch (result)
            {
                case TestResult.OtherMeaning:
                    ShowFeedback(() => Texts.Get(localization.TEST_ANOTHER_MEANING));
                    SelectInputRequested?.Invoke();
                    break;
                case TestResult.Wrong:
                    var checker = _checker;
                    ShowFeedback(() => WrongHint(checker));
                    await ShowWrongMessageAsync();
                    Input = "";
                    break;
                case TestResult.Misspelled:
                    ShowFeedback(() => Texts.Get(localization.TEST_TYPE_ERROR));
                    break;
            }
            FocusInputRequested?.Invoke();
            return;
        }
        if (result == TestResult.Wrong)
        {
            ShowFeedback(() => WrongHint(oldChecker));
            await ShowWrongMessageAsync();
        }
        if (await CheckForQuitAsync())
        {
            return;
        }
        DisplayCurrentWord();
        FocusInputRequested?.Invoke();
    }

    [RelayCommand]
    private void Close() => RequestClose(false);

    /// <summary>Finishes the quiz with the "well done" message when no word is left.</summary>
    /// <returns><c>true</c> when the quiz is finished and the window closes.</returns>
    private async Task<bool> CheckForQuitAsync()
    {
        if (_controller.HasWords())
        {
            return false;
        }
        Question = "";
        ShowFeedback(() => " ");
        Info = "";
        await _dialogs.ShowMessageAsync(Texts.Get(localization.TEST_WELL_DONE), Texts.Get(localization.TEST_FINISHED));
        RequestClose(true);
        return true;
    }

    private void DisplayCurrentWord()
    {
        Question = _checker!.Question;
        Info = _checker.Info;
        ShowFeedback(() => "");
        Input = "";
        CountText = _controller.Count().ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>Shows the feedback upon wrong answers and remembers how to build it.</summary>
    /// <param name="text">The function that builds the text.</param>
    private void ShowFeedback(Func<string> text)
    {
        _feedbackText = text;
        Feedback = text();
    }

    /// <summary>Handles UI langauge switch and builds the title and the feedback again.</summary>
    private void OnTextsChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != UiTexts.IndexerName)
        {
            return;
        }
        Title = Texts.Get(localization.TEST_TITLE);
        Feedback = _feedbackText();
    }

    /// <summary>Stops listening to the UI texts when the quiz window closes.</summary>
    public override void OnClosed() => Texts.PropertyChanged -= OnTextsChanged;

    private Task ShowWrongMessageAsync() =>
        _dialogs.ShowMessageAsync(Texts.Get(localization.TEST_ERROR), Texts.Get(localization.TEST_WRONG));

    private string WrongHint(Checker checker) =>
        Texts.Get(localization.TEST_WRONG_HINT) + Environment.NewLine + checker.Question + " = " + checker.Answer;
}
