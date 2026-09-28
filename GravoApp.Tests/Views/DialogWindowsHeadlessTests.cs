using Avalonia.Headless.NUnit;
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
}
