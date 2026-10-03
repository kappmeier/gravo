using CommunityToolkit.Mvvm.ComponentModel;

namespace GravoApp.ViewModels;

/// <summary>A named entry of a list with check boxes.</summary>
public sealed partial class CheckableItem : ObservableObject
{
    [ObservableProperty] private bool _isChecked;

    public CheckableItem(string name) => Name = name;

    public string Name { get; }
}
