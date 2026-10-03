using Avalonia.Controls;
using Avalonia.Headless.NUnit;
using Avalonia.Threading;
using FluentAssertions;
using Gravo;
using GravoApp.Tests.Support;
using GravoApp.Tests.ViewModels;
using GravoApp.Views;
using NUnit.Framework;

namespace GravoApp.Tests.Views;

[TestFixture]
public class MainWindowHeadlessTests
{
    [AvaloniaTest]
    public void Show_BindsLocalizedMenuHeaders()
    {
        var loc = Fakes.Localization();
        loc.Setup(l => l.GetText(localization.MAIN_MENU_FILE)).Returns("Datei");
        var window = new MainWindow { DataContext = MainViewModelTests.CreateFor(loc) };
        window.Show();
        Dispatcher.UIThread.RunJobs();
        var panel = window.Content.Should().BeOfType<DockPanel>().Which;
        var menu = panel.Children.OfType<Menu>().Should().ContainSingle().Which;
        menu.Items.OfType<MenuItem>().Should().NotBeEmpty();
        menu.Items.OfType<MenuItem>().First().Header.Should().Be("Datei");
        panel.Children.OfType<TabControl>().Should().ContainSingle();
        window.Title.Should().Be("Gravo");
    }
}
