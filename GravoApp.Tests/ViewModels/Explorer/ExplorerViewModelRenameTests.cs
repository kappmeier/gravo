using System.Collections.ObjectModel;
using FluentAssertions;
using Gravo;
using GravoApp.Localization;
using GravoApp.Services;
using GravoApp.Tests.Support;
using GravoApp.ViewModels.Explorer;
using Moq;
using NUnit.Framework;

namespace GravoApp.Tests.ViewModels.Explorer;

public class ExplorerViewModelRenameTests
{
    private static readonly string Dictionary = "T" + localization.TREE_DICTIONARY;
    private static readonly string Groups = "T" + localization.TREE_GROUPS;

    private static readonly string[] House = [Dictionary, "german", "english", "H", "house"];
    private static readonly string[] Book = [Groups, "Book"];
    private static readonly string[] Unit1 = [Groups, "Book", "Unit 1"];
    private static readonly string[] Go = [Groups, "Book", "Unit 1", "go"];

    private TempVocabulary _voc = null!;
    private Mock<IDialogService> _dialogs = null!;

    [SetUp]
    public void SetUp()
    {
        _voc = new TempVocabulary();
        _voc.SeedStandard();
        _dialogs = Fakes.Dialogs();
    }

    [TearDown]
    public void TearDown() => _voc.Dispose();

    private ExplorerViewModel Create() => new(_voc.Vocabulary, Fakes.Properties().Object, Fakes.Texts(),
        _dialogs.Object, TempVocabulary.MainLanguage);

    private void Answer(string? newName) =>
        _dialogs.Setup(d => d.PromptAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(newName);

    /// <summary>Walks the tree along the titles in the <paramref name="path"/> and expands every node on it.</summary>
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

    private void VerifyNoMessage() =>
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

    private IEnumerable<string> SubGroupNames(string group) =>
        _voc.Groups.GetSubGroups(group).Select(g => g.SubGroup);

    [Test]
    public async Task RenameMainEntry_ChangesMainAndAdaptsSubEntries()
    {
        var fixture = Create();
        var node = Node(fixture, House);
        fixture.SelectedNode = node;
        Answer("home");

        await fixture.RenameNodeCommand.ExecuteAsync(node);

        _dialogs.Verify(d => d.PromptAsync(Strings.RenameTitle, Strings.NewName, "house"), Times.Once);
        var home = "home";
        _voc.Dictionary.GetMainEntry(ref home, "english", "german").Word.Should().Be("home");
        _voc.Dictionary.GetWordsAndSubWords("home", "english", "german").Select(w => w.Word).Should()
            .BeEquivalentTo("home", "houses");
        node.Title.Should().Be("home");
        fixture.Rows.Select(r => r.Word).Should().BeEquivalentTo("home", "houses");
        VerifyNoMessage();
    }

    [Test]
    public async Task RenameMainEntry_Existing_ShowsCouldNotRename()
    {
        var fixture = Create();
        var node = Node(fixture, House);
        Answer("go");

        await fixture.RenameNodeCommand.ExecuteAsync(node);

        _dialogs.Verify(d => d.ShowMessageAsync("\"house\" konnte nicht umbenannt werden.", It.IsAny<string>()),
            Times.Once);
        node.Title.Should().Be("house");
        _voc.Dictionary.GetWordsAndSubWords("house", "english", "german").Select(w => w.Word).Should()
            .BeEquivalentTo("house", "houses");
    }

    [Test]
    public async Task RenameGroup_EditsGroup()
    {
        var fixture = Create();
        var node = Node(fixture, Book);
        Answer("Buch");

        await fixture.RenameNodeCommand.ExecuteAsync(node);

        _voc.Groups.GetGroups().Should().BeEquivalentTo("Buch", "Other");
        SubGroupNames("Buch").Should().BeEquivalentTo("Unit 1", "Unit 2");
        var unit1 = _voc.Groups.GetGroup("Buch", "Unit 1");
        _voc.Group.Load(ref unit1).WordCount.Should().Be(2);
        node.Title.Should().Be("Buch");
        node.Children.Select(c => c.Group).Should().Equal("Buch", "Buch");
        VerifyNoMessage();
    }

    [Test]
    public async Task RenameGroup_Existing_ShowsError()
    {
        var fixture = Create();
        var node = Node(fixture, Book);
        Answer("Other");

        await fixture.RenameNodeCommand.ExecuteAsync(node);

        _dialogs.Verify(d => d.ShowMessageAsync(Strings.ErrorTitle, It.IsAny<string>()), Times.Once);
        _voc.Groups.GetGroups().Should().BeEquivalentTo("Book", "Other");
        node.Title.Should().Be("Book");
    }

    [Test]
    public async Task RenameSubGroup_EditsSubGroup()
    {
        var fixture = Create();
        var node = Node(fixture, Unit1);
        fixture.SelectedNode = node;
        Answer("Lektion 1");

        await fixture.RenameNodeCommand.ExecuteAsync(node);

        SubGroupNames("Book").Should().BeEquivalentTo("Lektion 1", "Unit 2");
        node.Title.Should().Be("Lektion 1");
        fixture.Rows.Select(r => r.SubGroup).Should().Equal("Lektion 1", "Lektion 1");
        VerifyNoMessage();
    }

    [Test]
    public async Task RenameSubGroup_Apostrophe_RoundTrips()
    {
        var fixture = Create();
        var node = Node(fixture, Unit1);
        Answer("Peter's Unit");

        await fixture.RenameNodeCommand.ExecuteAsync(node);

        SubGroupNames("Book").Should().BeEquivalentTo("Peter's Unit", "Unit 2");
        node.Title.Should().Be("Peter's Unit");
        VerifyNoMessage();
    }

    [Test]
    public async Task RenameGroupWord_ChangesWord()
    {
        var fixture = Create();
        var node = Node(fixture, Go);
        fixture.SelectedNode = node;
        Answer("went");

        await fixture.RenameNodeCommand.ExecuteAsync(node);

        _voc.Dictionary.GetWordsAndSubWords("go", "english", "german").Single().Word.Should().Be("went");
        var unit = Node(fixture, Unit1);
        unit.Children.Select(c => c.Title).Should().BeEquivalentTo("house", "went");
        fixture.SelectedNode.Should().NotBeSameAs(node);
        fixture.SelectedNode!.Title.Should().Be("went");
        ((TestWord)fixture.SelectedNode.Payload!).Word.Should().Be("went");
        fixture.Rows.Single().Word.Should().Be("went");
        VerifyNoMessage();
    }

    [Test]
    public async Task RenameGroupWord_Duplicate_ShowsEntryExists()
    {
        _voc.AddWord("go", "english", "goes", "gehen");
        var fixture = Create();
        var node = Node(fixture, Go);
        Answer("goes");

        await fixture.RenameNodeCommand.ExecuteAsync(node);

        _dialogs.Verify(d => d.ShowMessageAsync(Strings.ProductName, "Eintrag existiert bereits."), Times.Once);
        _voc.Dictionary.GetWordsAndSubWords("go", "english", "german").Select(w => w.Word).Should()
            .BeEquivalentTo("go", "goes");
        Node(fixture, Unit1).Children.Should().Contain(node);
        node.Title.Should().Be("go");
    }

    [Test]
    public async Task RenameGroup_DoubleQuote_ShowsInvalidInput()
    {
        var fixture = Create();
        var node = Node(fixture, Book);
        Answer("Bo\"ok");

        await fixture.RenameNodeCommand.ExecuteAsync(node);

        _dialogs.Verify(d => d.ShowMessageAsync(Strings.InvalidInputTitle, It.IsAny<string>()), Times.Once);
        _voc.Groups.GetGroups().Should().BeEquivalentTo("Book", "Other");
        node.Title.Should().Be("Book");
    }

    [TestCase("   ")]
    [TestCase(" Book ")]
    public async Task Rename_EmptyOrUnchanged_DoesNothing(string answer)
    {
        var fixture = Create();
        var node = Node(fixture, Book);
        Answer(answer);

        await fixture.RenameNodeCommand.ExecuteAsync(node);

        _voc.Groups.GetGroups().Should().BeEquivalentTo("Book", "Other");
        node.Title.Should().Be("Book");
        VerifyNoMessage();
    }

    [Test]
    public async Task Rename_Cancelled_DoesNothing()
    {
        var fixture = Create();
        var node = Node(fixture, Unit1);
        Answer(null);

        await fixture.RenameNodeCommand.ExecuteAsync(node);

        SubGroupNames("Book").Should().BeEquivalentTo("Unit 1", "Unit 2");
        node.Title.Should().Be("Unit 1");
        VerifyNoMessage();
    }

    [Test]
    public async Task RenameLetter_DoesNothing()
    {
        var fixture = Create();
        var node = Node(fixture, Dictionary, "german", "english", "H");
        Answer("X");

        await fixture.RenameNodeCommand.ExecuteAsync(node);

        node.Title.Should().Be("H");
        VerifyNoMessage();
    }

    [Test]
    public async Task RenameRootOrLanguage_ShowsNotSupported()
    {
        var fixture = Create();
        var nodes = new[]
        {
            Node(fixture, Dictionary), Node(fixture, Dictionary, "german"),
            Node(fixture, Dictionary, "german", "english"), Node(fixture, Groups),
        };
        var titles = nodes.Select(n => n.Title).ToList();
        Answer("X");

        foreach (var node in nodes)
        {
            await fixture.RenameNodeCommand.ExecuteAsync(node);
        }

        _dialogs.Verify(d => d.ShowMessageAsync("Warnung", "Änderungen werden nicht übernommen"), Times.Exactly(4));
        nodes.Select(n => n.Title).Should().Equal(titles);
        _voc.Dictionary.DictionaryLanguages("german").Should().BeEquivalentTo("english", "french");
    }
}
