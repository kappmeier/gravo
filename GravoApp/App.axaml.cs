using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GravoApp.Localization;
using GravoApp.Services;
using GravoApp.ViewModels;
using GravoApp.Views;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("GravoApp.Tests")]

namespace GravoApp;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var dbPath = desktop.Args is { Length: > 0 }
                ? desktop.Args[0]
                : Path.Combine(AppContext.BaseDirectory, "voc.s3db");
            var languagesPath = Path.Combine(AppContext.BaseDirectory, "languages.s3db");
            if (!File.Exists(dbPath))
            {
                var texts = new UiTexts(AppServices.OpenLocalization(languagesPath));
                desktop.MainWindow = new MessageWindow(
                    Strings.ErrorTitle, Strings.DatabaseNotFound(dbPath), confirm: false, texts);
            }
            else
            {
                MainWindow? window = null;
                var services = AppServices.Create(
                    dbPath, languagesPath, texts => new DialogService(() => window!, texts));
                window = new MainWindow { DataContext = new MainViewModel(services) };
                desktop.MainWindow = window;
            }
        }
        base.OnFrameworkInitializationCompleted();
    }
}
