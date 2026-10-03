using Avalonia.Controls;
using Avalonia.Interactivity;
using GravoApp.Localization;

namespace GravoApp.Views;

/// <summary>Dialog for requesting a user input. Closes with the entered text on OK and with null on cancel.</summary>
/// <remarks>Replacement for <c>InputBox</c> in Windows applications.</remarks>
public partial class PromptWindow : Window
{
    public PromptWindow() => InitializeComponent();

    public PromptWindow(string title, string label, string initial, UiTexts texts) : this()
    {
        Title = title;
        LabelText.Text = label;
        InputBox.Text = initial;
        OkButton.Content = texts["BUTTON_OK"];
        CancelButton.Content = texts["BUTTON_CANCEL"];
        Opened += (_, _) =>
        {
            InputBox.Focus();
            InputBox.SelectAll();
        };
    }

    private void OnOk(object? sender, RoutedEventArgs e) => Close(InputBox.Text);

    private void OnCancel(object? sender, RoutedEventArgs e) => Close(null);
}
