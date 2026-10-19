using Avalonia.Controls;
using Avalonia.Headless.NUnit;
using Avalonia.Threading;
using Avalonia.VisualTree;
using AwesomeAssertions;
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
        var (viewModel, window, fixture) = CreateWordEditorTestInstance(voc);

        fixture.FindControl<TreeView>("Tree")!.ItemCount.Should().Be(2);

        var grid = fixture.FindControl<DataGrid>("RowsGrid")!;
        grid.ItemsSource.Should().BeSameAs(viewModel.Rows);
        viewModel.SelectedRows.Should().Equal(viewModel.Rows[0]);
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
        var (viewModel, window, fixture) = CreateWordEditorTestInstance(voc);

        var editor = fixture.FindControl<WordEditorView>("WordEditor")!;
        editor.IsVisible.Should().BeFalse();
        fixture.FindControl<MenuItem>("PanelsMenu")!.Header.Should().Be("T" + localization.EXPLORER_MENU_PANELS);

        var english = viewModel.Roots[0].Children[0].Children[0];
        english.IsExpanded = true;
        viewModel.SelectedNode = english.Children.Single(n => n.Title == "H");
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
    public void WordEditor_LabelsGetLocalized()
    {
        using var voc = new TempVocabulary();
        voc.SeedStandard();
        var (viewModel, window, explorerView) = CreateWordEditorTestInstance(voc);

        var editor = explorerView.FindControl<WordEditorView>("WordEditor")!;
        var english = viewModel.Roots[0].Children[0].Children[0];
        english.IsExpanded = true;
        viewModel.SelectedNode = english.Children.Single(n => n.Title == "H");
        Dispatcher.UIThread.RunJobs();

        var labels = editor.GetVisualDescendants().OfType<Label>().Select(l => l.Content?.ToString()).ToList();
        Console.WriteLine($"Labels: {string.Join(", ", labels)}");
        labels.Should().Contain("T" + localization.WORDS_MAIN_ENTRY + ":")
            .And.Contain("T" + localization.WORDS_WORD_TYPE + ":")
            .And.Contain("T" + localization.WORDS_MAIN_LANGUAGE + ":");

        window.Close();
    }

    private static (ExplorerViewModel viewModel, Window window, Control explorerView)
    CreateWordEditorTestInstance(TempVocabulary voc)
    {
        var viewModel = new ExplorerViewModel(voc.Vocabulary, Fakes.Properties().Object, Fakes.Texts(),
            Fakes.Dialogs().Object, TempVocabulary.MainLanguage);

        var built = new ViewLocator().Build(viewModel);
        var explorerView = built.Should().BeOfType<ExplorerView>().Which;

        var window = new Window { Content = explorerView };
        window.Show();
        Dispatcher.UIThread.RunJobs();


        return (viewModel, window, explorerView);
    }

    [AvaloniaTest]
    public void Explorer_ShowsMultiEditorForSeveralRowsAndKeepsThemSelected()
    {
        using var voc = new TempVocabulary();
        voc.SeedStandard();
        var (viewModel, window, fixture) = CreateWordEditorTestInstance(voc);

        var english = viewModel.Roots[0].Children[0].Children[0];
        english.IsExpanded = true;
        viewModel.SelectedNode = english.Children.Single(n => n.Title == "H");
        Dispatcher.UIThread.RunJobs();
        var grid = fixture.FindControl<DataGrid>("RowsGrid")!;
        grid.SelectedItems.Add(viewModel.Rows[1]);
        Dispatcher.UIThread.RunJobs();

        var multi = fixture.FindControl<MultiEditView>("MultiEditor")!;
        multi.IsVisible.Should().BeTrue();
        fixture.FindControl<WordEditorView>("WordEditor")!.IsVisible.Should().BeFalse();
        var post = multi.FindControl<TextBox>("MultiPostBox")!;
        post.IsEnabled.Should().BeFalse();
        multi.FindControl<CheckBox>("EnablePostCheck")!.IsChecked = true;
        Dispatcher.UIThread.RunJobs();
        post.IsEnabled.Should().BeTrue();
        viewModel.Multi.EnablePost.Should().BeTrue();
        multi.FindControl<Button>("MultiChangeButton")!.Content.Should().Be("T" + localization.BUTTON_CHANGE);

        viewModel.Multi.Post = "(n)";
        viewModel.ChangeSelectedCommand.Execute(null);
        Dispatcher.UIThread.RunJobs();

        grid.SelectedItems.Cast<ExplorerRow>().Select(r => r.Post).Should().Equal("(n)", "(n)");
        viewModel.SelectedRows.Should().HaveCount(2);
        multi.IsVisible.Should().BeTrue();
        window.Close();
    }

    [AvaloniaTest]
    public void GroupInput_BuildsGroupInputViewWithBothGrids()
    {
        using var voc = new TempVocabulary();
        voc.SeedStandard();
        var viewModel = new GroupInputViewModel(voc.Groups, voc.Dictionary, voc.Group, Fakes.Dialogs().Object,
            Fakes.Texts(), TempVocabulary.MainLanguage);

        var fixture = new ViewLocator().Build(viewModel).Should().BeOfType<GroupInputView>().Which;
        var window = new Window { Content = fixture };
        window.Show();

        fixture.FindControl<DataGrid>("WordsGrid").Should().NotBeNull();
        fixture.FindControl<DataGrid>("MeaningsGrid").Should().NotBeNull();
        fixture.CloseButton.Content.Should().Be("T84");
        window.Close();
    }
}
