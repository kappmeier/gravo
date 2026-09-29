using Avalonia.Headless.NUnit;
using Avalonia.Threading;
using FluentAssertions;
using Gravo;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using GravoApp.Views;
using Moq;
using NUnit.Framework;

namespace GravoApp.Tests.Views;

[TestFixture]
public class DialogWindowsHeadlessTests
{
    [AvaloniaTest]
    public void Options_CreatesOptionsWindowWithTitleAndButtonCaptions()
    {
        var vm = new OptionsViewModel(
            Fakes.DefaultSettings(out _), new Mock<IManagementDao>().Object, Fakes.Dialogs().Object,
            Fakes.Texts());
        var fixture = ViewLocator.CreateWindow(vm).Should().BeOfType<OptionsWindow>().Which;
        fixture.Show();
        fixture.Title.Should().Be("Optionen");
        fixture.OkButton.Content.Should().Be("T85");
        fixture.CancelButton.Content.Should().Be("T94");
    }

    [AvaloniaTest]
    public void Info_CreatesInfoWindowWithTitleAndGermanToggle()
    {
        var management = new Mock<IManagementDao>();
        management.Setup(m => m.LatestVersion)
            .Returns(new Properties.DBVersion(1, 7, new DateTime(2020, 1, 1), ""));
        var fixture = new InfoViewModel(management.Object, Fakes.Texts());
        var window = ViewLocator.CreateWindow(fixture).Should().BeOfType<InfoWindow>().Which;
        window.Show();
        window.Title.Should().Be("Gravo 7 Sprachtrainer info");
        window.CloseButton.Content.Should().Be("T84");
        fixture.German = true;
        Dispatcher.UIThread.RunJobs();
        window.DisclaimerText.Text.Should().StartWith("Das Arbeiten mit dieser Version von Gravo");
    }
}
