using Avalonia.Controls;
using Avalonia.Headless.NUnit;
using FluentAssertions;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using GravoApp.Views;
using NUnit.Framework;

namespace GravoApp.Tests.Views;

[TestFixture]
public class TabViewsHeadlessTests
{
    [AvaloniaTest]
    public void GroupInput_BuildsGroupInputViewWithBothGrids()
    {
        using var voc = new TempVocabulary();
        voc.SeedStandard();
        var vm = new GroupInputViewModel(voc.Groups, voc.Dictionary, voc.Group, Fakes.Dialogs().Object,
            Fakes.Texts(), TempVocabulary.MainLanguage);
        var fixture = new ViewLocator().Build(vm).Should().BeOfType<GroupInputView>().Which;
        var window = new Window { Content = fixture };
        window.Show();
        fixture.FindControl<DataGrid>("WordsGrid").Should().NotBeNull();
        fixture.FindControl<DataGrid>("MeaningsGrid").Should().NotBeNull();
        fixture.CloseButton.Content.Should().Be("T84");
        window.Close();
    }
}
