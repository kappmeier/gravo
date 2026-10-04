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
        Title = texts.Get(localization.TEST_TITLE);
        _checker = controller.GetTestChecker();
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
                    Feedback = Texts.Get(localization.TEST_ANOTHER_MEANING);
                    SelectInputRequested?.Invoke();
                    break;
                case TestResult.Wrong:
                    Feedback = WrongHint(_checker);
                    await ShowWrongMessageAsync();
                    Input = "";
                    break;
                case TestResult.Misspelled:
                    Feedback = Texts.Get(localization.TEST_TYPE_ERROR);
                    break;
            }
            FocusInputRequested?.Invoke();
            return;
        }
        if (result == TestResult.Wrong)
        {
            Feedback = WrongHint(oldChecker);
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
        Feedback = " ";
        Info = "";
        await _dialogs.ShowMessageAsync(Texts.Get(localization.TEST_WELL_DONE), Texts.Get(localization.TEST_FINISHED));
        RequestClose(true);
        return true;
    }

    private void DisplayCurrentWord()
    {
        Question = _checker!.Question;
        Info = _checker.Info;
        Feedback = "";
        Input = "";
        CountText = _controller.Count().ToString(CultureInfo.InvariantCulture);
    }

    private Task ShowWrongMessageAsync() =>
        _dialogs.ShowMessageAsync(Texts.Get(localization.TEST_ERROR), Texts.Get(localization.TEST_WRONG));

    private string WrongHint(Checker checker) =>
        Texts.Get(localization.TEST_WRONG_HINT) + Environment.NewLine + checker.Question + " = " + checker.Answer;
}
