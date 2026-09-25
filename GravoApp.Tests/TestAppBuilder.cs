using Avalonia;
using Avalonia.Headless;
using GravoApp;
using GravoApp.Tests;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

namespace GravoApp.Tests;

public static class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>().UseHeadless(new AvaloniaHeadlessPlatformOptions());
}
