using Avalonia.Controls;
using Avalonia.Controls.Templates;
using GravoApp.ViewModels;

namespace GravoApp;

/// <summary>
/// Maps a <c>GravoApp.ViewModels</c> view model to its corresponding <c>GravoApp.Views</c> view. The naming scheme
/// follows <c>FooViewModel</c> -> <c>FooView</c> for tab content, and <c>FooWindow</c> for dialogs, respectively. The
/// view type is looked up in the view model's own assembly.
/// </summary>
public sealed class ViewLocator : IDataTemplate
{
    private const string ViewsNamespace = "GravoApp.Views.";
    private const string ViewModelSuffix = "ViewModel";

    public bool Match(object? data) => data is ViewModelBase;

    /// <summary>
    /// Builds a view for the given view model. Name is the namespace, basename, and the suffix "View".
    /// </summary>
    public Control Build(object? param)
    {
        var view = Create(param!, "View");
        view.DataContext = param;
        return view;
    }

    /// <summary>
    /// Builds a window for the given view model. Name is the namespace, basename, and the suffix "Window".
    /// </summary>
    public static Window CreateWindow(ViewModelBase viewModel)
    {
        var window = (Window)Create(viewModel, "Window");
        window.DataContext = viewModel;
        return window;
    }

    private static Control Create(object data, string suffix)
    {
        var type = data.GetType();
        var baseName = type.Name.EndsWith(ViewModelSuffix, StringComparison.Ordinal)
            ? type.Name[..^ViewModelSuffix.Length]
            : type.Name;
        var name = ViewsNamespace + baseName + suffix;
        var viewType = type.Assembly.GetType(name)
            ?? throw new InvalidOperationException($"No {suffix} found for {type.Name}: expected {name}");
        return (Control)Activator.CreateInstance(viewType)!;
    }
}
