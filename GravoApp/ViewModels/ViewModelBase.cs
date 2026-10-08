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
}
