using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Gravo;
using GravoApp.Localization;
using GravoApp.ViewModels;
using GravoApp.Views;

namespace GravoApp.Services;

public sealed class DialogService : IDialogService
{
    private readonly Func<Window> _owner;
    private readonly UiTexts _texts;

    public DialogService(Func<Window> owner, UiTexts texts)
    {
        _owner = owner;
        _texts = texts;
    }

    public Task ShowMessageAsync(string title, string message) =>
        new MessageWindow(title, message, confirm: false, _texts).ShowDialog<bool?>(_owner());

    public async Task<bool> ConfirmAsync(string title, string message) =>
        await new MessageWindow(title, message, confirm: true, _texts).ShowDialog<bool?>(_owner()) == true;

    public Task<string?> PromptAsync(string title, string label, string initial) =>
        new PromptWindow(title, label, initial, _texts).ShowDialog<string?>(_owner());

    public async Task<bool> ShowDialogAsync(ViewModelBase viewModel)
    {
        var window = ViewLocator.CreateWindow(viewModel);
        Attach(window, viewModel);
        viewModel.CloseRequested += ok => window.Close(ok);
        return await window.ShowDialog<bool?>(_owner()) == true;
    }

    public void ShowWindow(ViewModelBase viewModel)
    {
        var window = ViewLocator.CreateWindow(viewModel);
        Attach(window, viewModel);
        viewModel.CloseRequested += _ => window.Close();
        window.Show(_owner());
    }

    /// <summary>Forwards the window's opened and closed events to the view model.</summary>
    private static void Attach(Window window, ViewModelBase viewModel)
    {
        window.Opened += async (_, _) => await viewModel.OnOpenedAsync();
        window.Closed += (_, _) => viewModel.OnClosed();
    }

    public async Task<string?> PickOpenFileAsync(string title, string extension)
    {
        var files = await _owner().StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = title,
            AllowMultiple = false,
            FileTypeFilter = new[] { Filter(extension), FilePickerFileTypes.All },
        });
        return files.Count == 0 ? null : files[0].TryGetLocalPath();
    }

    public async Task<string?> PickSaveFileAsync(string title, string extension, string? suggestedName)
    {
        var file = await _owner().StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = title,
            DefaultExtension = extension,
            SuggestedFileName = suggestedName,
            FileTypeChoices = new[] { Filter(extension) },
        });
        return file?.TryGetLocalPath();
    }

    private FilePickerFileType Filter(string extension) =>
        new(_texts.Get(localization.FILE_FILTER_DATABASES) + " (*." + extension + ")")
        {
            Patterns = new[] { "*." + extension },
        };
}
