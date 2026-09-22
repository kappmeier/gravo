using Avalonia.Headless.NUnit;
using FluentAssertions;
using GravoApp.Views;
using NUnit.Framework;

namespace GravoApp.Tests.Views;

[TestFixture]
public class MainWindowHeadlessTests
{
    [AvaloniaTest]
    public void Show_HasApplicationTitle()
    {
        var window = new MainWindow();
        window.Show();
        window.Title.Should().Be("Gravo");
    }
}
