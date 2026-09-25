using Avalonia.Controls;
using Avalonia.Interactivity;
using GravoApp.Localization;

namespace GravoApp.Views;

/// <summary>
/// A dialog window showing OK, or yes/no when <c>confirm</c> is set.
/// </summary>
/// <remarks>Replacement for <c>MsgBox</c> in Windows applications.</remarks>
/// <returns><c>true</c> when the user accepts (OK or Yes), <c>false</c> when the user rejects (No).</returns>
public partial class MessageWindow : Window
{
    public MessageWindow() => InitializeComponent();

    public MessageWindow(string title, string message, bool confirm, UiTexts texts) : this()
    {
        Title = title;
        MessageText.Text = message;
        OkButton.Content = texts["BUTTON_OK"];
        YesButton.Content = texts["YES"];
        NoButton.Content = texts["NO"];
        OkButton.IsVisible = !confirm;
        OkButton.IsDefault = !confirm;
        YesButton.IsVisible = confirm;
        YesButton.IsDefault = confirm;
        NoButton.IsVisible = confirm;
    }

    private void OnAccept(object? sender, RoutedEventArgs e) => Close(true);

    private void OnReject(object? sender, RoutedEventArgs e) => Close(false);
}
