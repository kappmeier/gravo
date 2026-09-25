using Avalonia.Headless.NUnit;
using FluentAssertions;
using GravoApp.ViewModels;
using GravoApp.Views;
using NUnit.Framework;

namespace GravoApp.Tests;

public class ViewLocatorTests
{
    [AvaloniaTest]
    public void Build_MapsViewModelToViewAndSetsDataContext()
    {
        var vm = new ProbeViewModel();
        var locator = new ViewLocator();
        locator.Match(vm).Should().BeTrue();
        var view = locator.Build(vm);
        view.Should().BeOfType<ProbeView>();
        view.DataContext.Should().BeSameAs(vm);
    }

    [AvaloniaTest]
    public void CreateWindow_MapsViewModelToWindow()
    {
        var vm = new ProbeViewModel();
        ViewLocator.CreateWindow(vm).Should().BeOfType<ProbeWindow>().Which.DataContext.Should().BeSameAs(vm);
    }

    [Test]
    public void Match_NonViewModel_IsFalse() => new ViewLocator().Match("text").Should().BeFalse();
}
