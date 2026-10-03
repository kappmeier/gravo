using GravoApp.ViewModels;

namespace GravoApp.Services;

/// <summary>Windows, message boxes, and file pickers, etc. opened by view models.</summary>
/// <remarks>This is the central definition for all dialog interactions in the application.</remarks>
public interface IDialogService
{
    Task ShowMessageAsync(string title, string message);

    /// <summary>Dialog asking a yes/no question.</summary>
    /// <returns><c>true</c> if the user answered yes, <c>false</c> otherwise.</returns>
    Task<bool> ConfirmAsync(string title, string message);

    /// <summary>Dialog asking the user to enter text.</summary>
    /// <returns>The entered text, or <c>null</c> when the user cancels.</returns>
    Task<string?> PromptAsync(string title, string label, string initial);

    /// <summary>
    /// Shows the view model's window modally.
    /// </summary>
    /// <returns>
    /// <c>true</c> when the dialog closes itself with <c>RequestClose(true)</c>, <c>false</c> otherwise.
    /// </returns>
    Task<bool> ShowDialogAsync(ViewModelBase viewModel);

    /// <summary>Opens the view model's window non-modally.</summary>
    /// <remarks>A non-blocking window, owned by the main window.</remarks>
    void ShowWindow(ViewModelBase viewModel);

    /// <summary>
    /// Dialog asking for a path to open. <paramref name="extension"/> has no dot, for example "s3db".
    /// </summary>
    /// <returns>The chosen path, or <c>null</c> when the user cancels.</returns>
    Task<string?> PickOpenFileAsync(string title, string extension);

    /// <summary>
    /// Dialog asking for a path to save a file. <paramref name="extension"/> has no dot, for example "s3db".
    /// </summary>
    /// <returns>The chosen path, or <c>null</c> when the user cancels.</returns>
    Task<string?> PickSaveFileAsync(string title, string extension, string? suggestedName);
}
