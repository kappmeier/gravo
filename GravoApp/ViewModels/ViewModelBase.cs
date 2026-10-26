using CommunityToolkit.Mvvm.ComponentModel;

namespace GravoApp.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    [ObservableProperty] private string _title = "";

    /// <summary>
    /// An event raised when the hosting view should close. The flag is the dialog result (true = OK).
    /// </summary>
    public event Action<bool>? CloseRequested;

    protected void RequestClose(bool ok) => CloseRequested?.Invoke(ok);

    /// <summary>Called once the window has opened.</summary>
    /// <remarks>The default does nothing. Called by <c>DialogService</c> for every opened window.</remarks>
    public virtual Task OnOpenedAsync() => Task.CompletedTask;

    /// <summary>Called once the window or tab has closed. Independent of how it was closed</summary>
    /// <remarks>
    /// The default does nothing. Called by <c>DialogService</c> for every opened window and by <c>MainViewModel</c>
    /// for every removed tab.
    /// </remarks>
    public virtual void OnClosed()
    {
    }
}
