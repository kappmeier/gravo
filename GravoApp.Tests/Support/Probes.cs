using Avalonia.Controls;

/// <summary>Test doubles for the <see cref="ViewLocator"/>.</summary>
/// <remarks>
/// The test doubles live in the test assembly under the app's namespaces, so the locator must look types up in the
/// view model's own assembly.
/// </remarks>
namespace GravoApp.ViewModels
{
    public sealed class ProbeViewModel : ViewModelBase
    {
        public int OpenedCount { get; private set; }

        public int ClosedCount { get; private set; }

        public override Task OnOpenedAsync()
        {
            OpenedCount++;
            return Task.CompletedTask;
        }

        public override void OnClosed() => ClosedCount++;

        public void Close(bool ok) => RequestClose(ok);
    }
}

namespace GravoApp.Views
{
    public sealed class ProbeView : UserControl { }

    public sealed class ProbeWindow : Window { }
}
