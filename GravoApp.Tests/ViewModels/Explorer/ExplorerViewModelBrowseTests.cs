using System.Collections.ObjectModel;
using FluentAssertions;
using Gravo;
using GravoApp.Tests.Support;
using GravoApp.ViewModels.Explorer;
using Moq;
using NUnit.Framework;

namespace GravoApp.Tests.ViewModels.Explorer;

public class ExplorerViewModelBrowseTests
{
    private static readonly string Dictionary = "T" + localization.TREE_DICTIONARY;
    private static readonly string Groups = "T" + localization.TREE_GROUPS;
    private static readonly string Yes = "T" + localization.YES;
    private static readonly string No = "T" + localization.NO;

    private TempVocabulary _voc = null!;
    private Mock<ILocalization> _loc = null!;

    [SetUp]
    public void SetUp()
    {
        _voc = new TempVocabulary();
        _voc.SeedStandard();
        _loc = Fakes.Localization();
    }

    [TearDown]
    public void TearDown() => _voc.Dispose();

    private ExplorerViewModel Create() => new(_voc.Vocabulary, Fakes.Properties().Object, Fakes.Texts(_loc),
        Fakes.Dialogs().Object, TempVocabulary.MainLanguage);

    /// <summary>Walks the tree along the titles in <paramref name="path"/> and expands every node on it.</summary>
    /// <returns>The node at the end of the path.</returns>
    private static ExplorerNode Node(ExplorerViewModel vm, params string[] path)
    {
        ObservableCollection<ExplorerNode> nodes = vm.Roots;
        ExplorerNode? node = null;
        foreach (var title in path)
        {
            node = nodes.Single(n => n.Title == title);
            node.IsExpanded = true;
            nodes = node.Children;
        }
        return node!;
    }

    [Test]
    public void Roots_ReflectDictionaryAndGroups()
    {
        var fixture = Create();

        fixture.Roots.Select(n => n.Title).Should().Equal(Dictionary, Groups);

        var german = fixture.Roots[0].Children.Should().ContainSingle().Which;
        german.Title.Should().Be("german");
        german.Kind.Should().Be(NodeKind.MainLanguage);
        german.Children.Select(n => n.Title).Should().Equal("english", "french");
        german.Children.Should().OnlyContain(n => n.Children.Count == 1 && n.Children[0].Kind == NodeKind.Placeholder);
        fixture.Roots[1].Children.Select(n => n.Title).Should().Equal("Book", "Other");

        var book = fixture.Roots[1].Children[0];
        book.Children.Select(n => n.Title).Should().Equal("Unit 1", "Unit 2");
        book.Children[0].Children.Should().ContainSingle().Which.Kind.Should().Be(NodeKind.Placeholder);
        book.Children[1].Children.Should().BeEmpty();

        var words = fixture.Roots[1].Children[1].Children.Should().ContainSingle().Which;
        words.Title.Should().Be("Words");
        words.Children.Should().ContainSingle().Which.Kind.Should().Be(NodeKind.Placeholder);
    }

    [Test]
    public void Roots_FurtherMainLanguage_NestsUnderThePreviousOne()
    {
        _voc.Dictionary.AddEntry("Haus", "german", "spanish");

        var fixture = Create();

        var german = fixture.Roots[0].Children.Should().ContainSingle().Which;
        german.Children.Select(n => n.Title).Should().Equal("english", "french", "spanish");
        var spanish = german.Children[2];
        spanish.Kind.Should().Be(NodeKind.MainLanguage);

        var language = spanish.Children.Should().ContainSingle().Which;
        language.Title.Should().Be("german");
        language.Kind.Should().Be(NodeKind.Language);
        language.MainLanguage.Should().Be("spanish");
        fixture.SelectedNode = german;
        fixture.Rows.Select(r => r.Language).Should().Equal("english", "french");
    }

    [Test]
    public void Rows_WithTheSameCells_StayDistinct()
    {
        var fixture = Create();

        fixture.SelectedNode = Node(fixture, Dictionary, "german", "english");
        var first = new ExplorerRow(null) { Word = "go" };
        first.Should().NotBe(new ExplorerRow(null) { Word = "go" });
        fixture.Rows.IndexOf(first).Should().Be(-1);
    }

    [Test]
    public void ExpandLanguage_ListsLettersWithWordsOnly()
    {
        var fixture = Node(Create(), Dictionary, "german", "english");

        fixture.Children.Select(n => n.Title).Should()
            .Equal("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(c => new string(c, 1)));
        fixture.Children.Where(n => n.Children.Count > 0).Select(n => n.Title).Should().Equal("G", "H");
        fixture.Children.Single(n => n.Title == "H").Children.Should().ContainSingle()
            .Which.Kind.Should().Be(NodeKind.Placeholder);
    }

    [Test]
    public void ExpandLetter_ListsDistinctMainEntries()
    {
        var fixture = Node(Create(), Dictionary, "german", "english", "H");

        fixture.Children.Select(n => n.Title).Should().Equal("house");
        fixture.Children[0].Kind.Should().Be(NodeKind.MainEntry);
        fixture.Children[0].Children.Should().BeEmpty();
    }

    [Test]
    public void ExpandSubGroup_ListsGroupWords()
    {
        var fixture = Node(Create(), Groups, "Book", "Unit 1");

        fixture.Children.Select(n => n.Title).Should().BeEquivalentTo("house", "go");
        fixture.Children.Should().OnlyContain(n => n.Kind == NodeKind.GroupWord && n.Payload is TestWord);
    }

    [Test]
    public void SelectDictionaryRoot_ShowsLanguageOverview()
    {
        var fixture = Create();

        fixture.SelectedNode = fixture.Roots[1];
        fixture.SelectedNode = fixture.Roots[0];
        fixture.Style.Should().Be(ListStyle.Dictionary);
        fixture.ShowDictionaryColumns.Should().BeTrue();
        fixture.ShowMainLanguageColumn.Should().BeTrue();
        fixture.ShowWordColumns.Should().BeFalse();
        fixture.Rows.Select(r => (r.MainLanguage, r.Language, r.Count1, r.Count2)).Should()
            .Equal(("german", "english", 2, 3), ("german", "french", 1, 1));
    }

    [Test]
    public void Constructor_SelectsDictionaryRoot()
    {
        var fixture = Create();

        fixture.SelectedNode.Should().BeSameAs(fixture.Roots[0]);
        fixture.Rows.Should().HaveCount(2);
        fixture.SelectedRow.Should().BeSameAs(fixture.Rows[0]);
    }

    [Test]
    public void SelectMainLanguage_ShowsPerLanguageCounts()
    {
        var fixture = Create();

        fixture.SelectedNode = Node(fixture, Dictionary, "german");
        fixture.Style.Should().Be(ListStyle.MainLanguage);
        fixture.ShowDictionaryColumns.Should().BeTrue();
        fixture.ShowMainLanguageColumn.Should().BeFalse();
        fixture.Rows.Select(r => (r.Language, r.Count1, r.Count2)).Should()
            .Equal(("english", 2, 3), ("french", 1, 1));
    }

    [Test]
    public void SelectLanguage_ShowsMainEntriesAndSubRowsForSelection()
    {
        var fixture = Create();

        fixture.SelectedNode = Node(fixture, Dictionary, "german", "english");
        fixture.Style.Should().Be(ListStyle.Language);
        fixture.ShowWordOnly.Should().BeTrue();
        fixture.ShowWordColumns.Should().BeFalse();
        fixture.Rows.Select(r => r.Word).Should().Equal("go", "house");
        fixture.SelectedRow.Should().BeSameAs(fixture.Rows[0]);
        fixture.SubRows.Select(r => r.Word).Should().Equal("go");

        fixture.SelectedRow = fixture.Rows[1];
        fixture.ShowSubRows.Should().BeTrue();
        fixture.SubRows.Select(r => r.Word).Should().Equal("house", "houses");
        fixture.SubRows[0].WordType.Should().Be("T" + localization.WORD_TYPE_SUBSTANTIVE);
        fixture.SubRows[1].Meaning.Should().Be("Häuser");
        fixture.SelectedSubRow.Should().BeSameAs(fixture.SubRows[0]);

        fixture.SelectedNode = fixture.Roots[1];
        fixture.ShowSubRows.Should().BeFalse();
        fixture.SubRows.Should().BeEmpty();
    }

    [Test]
    public void SelectLetter_ShowsWordsOfThatLetter()
    {
        var fixture = Create();

        fixture.SelectedNode = Node(fixture, Dictionary, "german", "english", "H");
        fixture.Style.Should().Be(ListStyle.WordEntry);
        fixture.ShowWordColumns.Should().BeTrue();
        fixture.ShowMarkedColumn.Should().BeFalse();
        fixture.Rows.Select(r => r.Word).Should().Equal("house", "houses");
        fixture.Rows[0].Irregular.Should().Be(No);
        fixture.Rows[0].Entry.Should().BeSameAs(fixture.Rows[0].Payload);
        fixture.ShowSubRows.Should().BeFalse();
    }

    [Test]
    public void SelectMainEntry_ShowsItsWordsAndSubWords()
    {
        var fixture = Create();

        fixture.SelectedNode = Node(fixture, Dictionary, "german", "english", "G", "go");
        fixture.Style.Should().Be(ListStyle.WordEntry);

        var row = fixture.Rows.Should().ContainSingle().Which;
        row.Word.Should().Be("go");
        row.Meaning.Should().Be("gehen");
        row.Irregular.Should().Be(Yes);
        row.WordType.Should().Be("T" + localization.WORD_TYPE_VERB);
    }

    [Test]
    public void SelectMainEntry_WithSubWords_ShowsAllWordsOfTheEntry()
    {
        var fixture = Create();

        fixture.SelectedNode = Node(fixture, Dictionary, "german", "english", "H", "house");
        fixture.Rows.Select(r => r.Word).Should().Equal("house", "houses");
    }

    [Test]
    public void SelectGroupsRoot_ShowsGroupOverview()
    {
        var fixture = Create();

        fixture.SelectedNode = fixture.Roots[1];
        fixture.Style.Should().Be(ListStyle.Groups);
        fixture.ShowGroupsColumns.Should().BeTrue();
        fixture.ShowDictionaryColumns.Should().BeFalse();
        fixture.Rows.Select(r => (r.GroupName, r.SubGroupCount, r.Count1, r.LanguageCount)).Should()
            .Equal(("Book", 2, 2, 1), ("Other", 1, 1, 1));
    }

    [Test]
    public void SelectGroup_ShowsAllSubGroupRowsWithSubGroupColumn()
    {
        var fixture = Create();

        fixture.SelectedNode = Node(fixture, Groups, "Book");
        fixture.Style.Should().Be(ListStyle.WordEntryGroup);
        fixture.ShowWordColumns.Should().BeTrue();
        fixture.ShowMarkedColumn.Should().BeTrue();
        fixture.ShowSubGroupColumn.Should().BeTrue();
        fixture.Rows.Select(r => (r.Word, r.Marked, r.SubGroup)).Should()
            .Equal(("house", Yes, "Unit 1"), ("go", No, "Unit 1"));
    }

    [Test]
    public void SelectSubGroup_ShowsRowsWithMarkedColumn()
    {
        var fixture = Create();

        fixture.SelectedNode = Node(fixture, Groups, "Book", "Unit 1");
        fixture.Style.Should().Be(ListStyle.WordEntrySubGroup);
        fixture.ShowMarkedColumn.Should().BeTrue();
        fixture.ShowSubGroupColumn.Should().BeFalse();
        fixture.Rows.Select(r => (r.Word, r.Marked)).Should().Equal(("house", Yes), ("go", No));
        fixture.Rows.Should().OnlyContain(r => r.Payload is TestWord);
    }

    [Test]
    public void SelectGroupWord_ShowsTheOneRow()
    {
        var fixture = Create();

        var house = Node(fixture, Groups, "Book", "Unit 1", "house");
        fixture.SelectedNode = house;
        fixture.Style.Should().Be(ListStyle.WordEntrySubGroup);

        var row = fixture.Rows.Should().ContainSingle().Which;
        row.Payload.Should().BeSameAs(house.Payload);
        row.Entry!.Word.Should().Be("house");
        row.Marked.Should().Be(Yes);
    }

    [Test]
    public void TextsRefresh_RenamesRootsAndRowTexts()
    {
        var fixture = Create();

        fixture.SelectedNode = Node(fixture, Dictionary, "german", "english", "H");
        fixture.SelectedRow = fixture.Rows[1];
        _loc.Setup(l => l.GetText(It.IsAny<int>())).Returns((int code) => "X" + code);
        fixture.Texts.Refresh();
        fixture.Roots.Select(n => n.Title).Should()
            .Equal("X" + localization.TREE_DICTIONARY, "X" + localization.TREE_GROUPS);
        fixture.Title.Should().Be("X" + localization.EXPLORER_TITLE);
        fixture.Rows.Select(r => r.Irregular).Should().Equal("X" + localization.NO, "X" + localization.NO);
        fixture.SelectedRow.Should().BeSameAs(fixture.Rows[1]);
    }

    [Test]
    public void Reload_RebuildsTree()
    {
        var fixture = Create();

        fixture.SelectedNode = fixture.Roots[1];
        _voc.AddGroup("Zeta", "1");
        fixture.Reload();
        fixture.Roots[1].Children.Select(n => n.Title).Should().Equal("Book", "Other", "Zeta");
        fixture.SelectedNode.Should().BeSameAs(fixture.Roots[0]);
        fixture.Style.Should().Be(ListStyle.Dictionary);
    }
}
