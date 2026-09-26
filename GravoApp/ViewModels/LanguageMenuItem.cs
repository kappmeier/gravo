using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GravoApp.ViewModels;

/// <summary>
/// One entry of the language submenu in the main menu.
/// </summary>
public sealed partial class LanguageMenuItem : ObservableObject
{
    [ObservableProperty] private bool _isChecked;

    public LanguageMenuItem(string name, Action<string> select)
    {
        Name = name;
        SelectCommand = new RelayCommand(() => select(name));
    }

    public string Name { get; }

    public IRelayCommand SelectCommand { get; }
}
