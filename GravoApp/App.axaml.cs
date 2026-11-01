using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Gravo;
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
                    texts.Get(localization.ERROR_TITLE),
                    texts.Format(localization.STARTUP_DATABASE_NOT_FOUND, dbPath),
                    confirm: false, texts);
            }
            else
            {
                MainWindow? window = null;
                // Messages belong to the active window, so closing one returns the focus there.
                var services = AppServices.Create(dbPath, languagesPath, texts => new DialogService(
                    () => desktop.Windows.FirstOrDefault(w => w.IsActive) ?? window!, texts));
                window = new MainWindow { DataContext = new MainViewModel(services) };
                desktop.Exit += (_, _) => services.Db.Dispose();
                desktop.MainWindow = window;
            }
        }
        base.OnFrameworkInitializationCompleted();
    }
}
