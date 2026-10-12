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
        var viewModel = new ProbeViewModel();

        var locator = new ViewLocator();
        locator.Match(viewModel).Should().BeTrue();

        var view = locator.Build(viewModel);
        view.Should().BeOfType<ProbeView>();
        view.DataContext.Should().BeSameAs(viewModel);
    }

    [AvaloniaTest]
    public void CreateWindow_MapsViewModelToWindow()
    {
        var viewModel = new ProbeViewModel();
        ViewLocator.CreateWindow(viewModel).Should()
            .BeOfType<ProbeWindow>().Which.DataContext.Should().BeSameAs(viewModel);
    }

    [Test]
    public void Match_NonViewModel_IsFalse() => new ViewLocator().Match("text").Should().BeFalse();
}
