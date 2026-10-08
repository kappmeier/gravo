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
    public void Explorer_ShowsWordEditorForWordNodes()
    {
        using var voc = new TempVocabulary();
        voc.SeedStandard();
        var vm = new ExplorerViewModel(voc.Vocabulary, Fakes.Properties().Object, Fakes.Texts(),
            Fakes.Dialogs().Object, TempVocabulary.MainLanguage);

        var fixture = new ViewLocator().Build(vm).Should().BeOfType<ExplorerView>().Which;
        var window = new Window { Content = fixture };
        window.Show();
        Dispatcher.UIThread.RunJobs();
        var editor = fixture.FindControl<WordEditorView>("WordEditor")!;
        editor.IsVisible.Should().BeFalse();
        fixture.FindControl<MenuItem>("PanelsMenu")!.Header.Should().Be("T" + localization.EXPLORER_MENU_PANELS);

        var english = vm.Roots[0].Children[0].Children[0];
        english.IsExpanded = true;
        vm.SelectedNode = english.Children.Single(n => n.Title == "H");
        Dispatcher.UIThread.RunJobs();
        editor.IsVisible.Should().BeTrue();
        editor.FindControl<TextBox>("WordBox")!.Text.Should().Be("house");
        editor.FindControl<TextBox>("MainEntryBox")!.Text.Should().Be("house");
        editor.FindControl<Button>("AddButton")!.Content.Should().Be("T" + localization.BUTTON_ADD);
        editor.FindControl<Button>("ChangeButton")!.Content.Should().Be("T" + localization.BUTTON_CHANGE);
        editor.FindControl<CheckBox>("AddToGroupCheck")!.IsVisible.Should().BeFalse();
        window.Close();
    }

    [AvaloniaTest]
    public void Explorer_ShowsMultiEditorForSeveralRowsAndKeepsThemSelected()
    {
        using var voc = new TempVocabulary();
        voc.SeedStandard();
        var vm = new ExplorerViewModel(voc.Vocabulary, Fakes.Properties().Object, Fakes.Texts(),
            Fakes.Dialogs().Object, TempVocabulary.MainLanguage);

        var fixture = new ViewLocator().Build(vm).Should().BeOfType<ExplorerView>().Which;
        var window = new Window { Content = fixture };
        window.Show();
        var english = vm.Roots[0].Children[0].Children[0];
        english.IsExpanded = true;
        vm.SelectedNode = english.Children.Single(n => n.Title == "H");
        Dispatcher.UIThread.RunJobs();
        var grid = fixture.FindControl<DataGrid>("RowsGrid")!;
        grid.SelectedItems.Add(vm.Rows[1]);
        Dispatcher.UIThread.RunJobs();

        var multi = fixture.FindControl<MultiEditView>("MultiEditor")!;
        multi.IsVisible.Should().BeTrue();
        fixture.FindControl<WordEditorView>("WordEditor")!.IsVisible.Should().BeFalse();
        var post = multi.FindControl<TextBox>("MultiPostBox")!;
        post.IsEnabled.Should().BeFalse();
        multi.FindControl<CheckBox>("EnablePostCheck")!.IsChecked = true;
        Dispatcher.UIThread.RunJobs();
        post.IsEnabled.Should().BeTrue();
        vm.Multi.EnablePost.Should().BeTrue();
        multi.FindControl<Button>("MultiChangeButton")!.Content.Should().Be("T" + localization.BUTTON_CHANGE);

        vm.Multi.Post = "(n)";
        vm.ChangeSelectedCommand.Execute(null);
        Dispatcher.UIThread.RunJobs();

        grid.SelectedItems.Cast<ExplorerRow>().Select(r => r.Post).Should().Equal("(n)", "(n)");
        vm.SelectedRows.Should().HaveCount(2);
        multi.IsVisible.Should().BeTrue();
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
