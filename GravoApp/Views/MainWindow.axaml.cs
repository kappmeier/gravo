using Avalonia;
using Avalonia.Controls;
using Gravo;
using GravoApp.ViewModels;

namespace GravoApp.Views;

/// <summary>
/// The main window, which restores and stores its geometry through the <see cref="MainViewModel"/>.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Opened += OnOpened;
        Closing += OnClosing;
    }

    private MainViewModel? Vm => DataContext as MainViewModel;

    private async void OnOpened(object? sender, EventArgs e)
    {
        if (Vm is not { } vm)
        {
            return;
        }
        vm.CloseRequested += _ => Close();
        if (vm.RestoreGeometry() is { } g)
        {
            Position = new PixelPoint(g.X, g.Y);
            Width = g.Width;
            Height = g.Height;
            WindowState = (WindowState)g.State;
        }
        await vm.CheckDatabaseVersionAsync();
    }

    private void OnClosing(object? sender, WindowClosingEventArgs e)
    {
        var state = WindowState == WindowState.FullScreen
            ? WindowStateSetting.Normal
            : (WindowStateSetting)WindowState;
        Vm?.SaveGeometry(Position.X, Position.Y, (int)ClientSize.Width, (int)ClientSize.Height, state);
    }
}
