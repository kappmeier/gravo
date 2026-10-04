using Avalonia.Controls;
using Avalonia.Headless.NUnit;
using Avalonia.Threading;
using FluentAssertions;
using Gravo;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using GravoApp.ViewModels.Explorer;
using GravoApp.Views;
using NUnit.Framework;

namespace GravoApp.Tests.Views;

[TestFixture]
public class TabViewsHeadlessTests
{
    [AvaloniaTest]
    public void Explorer_BuildsExplorerViewWithTreeAndLocalizedColumns()
    {
        using var voc = new TempVocabulary();
        voc.SeedStandard();
        var vm = new ExplorerViewModel(voc.Vocabulary, Fakes.Properties().Object, Fakes.Texts(),
            Fakes.Dialogs().Object, TempVocabulary.MainLanguage);

        var fixture = new ViewLocator().Build(vm).Should().BeOfType<ExplorerView>().Which;
        var window = new Window { Content = fixture };
        window.Show();
        Dispatcher.UIThread.RunJobs();
        fixture.FindControl<TreeView>("Tree")!.ItemCount.Should().Be(2);

        var grid = fixture.FindControl<DataGrid>("RowsGrid")!;
        grid.ItemsSource.Should().BeSameAs(vm.Rows);
        vm.SelectedRows.Should().Equal(vm.Rows[0]);
        grid.Columns.Where(c => c.IsVisible).Select(c => c.Header as string).Should().Equal(
            "T" + localization.EXPLORER_HEADLINE_MAIN_LANGUAGE, "T" + localization.EXPLORER_HEADLINE_LANGUAGE,
            "T" + localization.EXPLORER_HEADLINE_MAIN_ENTRIES, "T" + localization.EXPLORER_HEADLINE_TOTAL_ENTRIES);
        fixture.FindControl<DataGrid>("SubRowsGrid")!.IsVisible.Should().BeFalse();
        window.Close();
    }

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
