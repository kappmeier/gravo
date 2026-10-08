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

public class ExplorerViewModelMultiEditTests
{
    private static readonly string Dictionary = "T" + localization.TREE_DICTIONARY;
    private static readonly string Groups = "T" + localization.TREE_GROUPS;
    private static readonly string Yes = "T" + localization.YES;

    private TempVocabulary _voc = null!;
    private TempVocabulary.Seed _seed = null!;
    private Mock<IDialogService> _dialogs = null!;

    [SetUp]
    public void SetUp()
    {
        _voc = new TempVocabulary();
        _seed = _voc.SeedStandard();
        _dialogs = Fakes.Dialogs();
    }

    [TearDown]
    public void TearDown() => _voc.Dispose();

    private ExplorerViewModel Create() => new(_voc.Vocabulary, Fakes.Properties().Object, Fakes.Texts(),
        _dialogs.Object, TempVocabulary.MainLanguage);

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

    /// <summary>Selects the node at the given <paramref name="path"/> and all of its rows.</summary>
    private static void SelectAll(ExplorerViewModel vm, params string[] path)
    {
        vm.SelectedNode = Node(vm, path);
        foreach (var row in vm.Rows)
        {
            vm.SelectedRows.Add(row);
        }
    }

    private static readonly string[] Letter = [Dictionary, "german", "english", "H"];

    private static readonly string[] Unit1 = [Groups, "Book", "Unit 1"];

    private List<WordEntry> HouseWords() =>
        _voc.Dictionary.GetWordsAndSubWords("house", "english", "german").ToList();

    private GroupDto Load(GroupEntry group) => _voc.Group.Load(ref group);

    [Test]
    public async Task ChangeSelected_OnlyEnabledFieldsChange()
    {
        var fixture = Create();
        SelectAll(fixture, Letter);

        fixture.Multi.EnablePost = true;
        fixture.Multi.Post = "(n)";
        fixture.Multi.Meaning = "ignored";
        fixture.Multi.Word = "ignored";
        await fixture.ChangeSelectedCommand.ExecuteAsync(null);

        var words = HouseWords();
        words.Select(w => w.Post).Should().Equal("(n)", "(n)");
        words.Select(w => w.Word).Should().BeEquivalentTo("house", "houses");
        words.Select(w => w.Meaning).Should().BeEquivalentTo("Haus", "Häuser");
        fixture.Rows.Select(r => r.Post).Should().Equal("(n)", "(n)");
        fixture.SelectedRows.Should().BeEquivalentTo(fixture.Rows);
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task ChangeSelected_WordType_UsesMultiList()
    {
        var fixture = Create();
        SelectAll(fixture, Letter);

        fixture.Editor.WordTypeIndex = fixture.Editor.WordTypeNames.ToList().IndexOf("WORD_TYPE_ADJECTIVE");
        fixture.Multi.EnableWordType = true;
        fixture.Multi.WordTypeIndex = fixture.Multi.WordTypeNames.ToList().IndexOf("WORD_TYPE_VERB");
        await fixture.ChangeSelectedCommand.ExecuteAsync(null);

        HouseWords().Select(w => w.WordType).Should().Equal(WordType.Verb, WordType.Verb);
        fixture.Rows.Select(r => r.WordType).Should()
            .Equal("T" + localization.WORD_TYPE_VERB, "T" + localization.WORD_TYPE_VERB);
    }

    [Test]
    public async Task ChangeSelected_MarkedOnSubGroup_UpdatesGroup()
    {
        var fixture = Create();
        SelectAll(fixture, Unit1);

        fixture.Multi.EnableMarked = true;
        fixture.Multi.Marked = true;
        await fixture.ChangeSelectedCommand.ExecuteAsync(null);

        Load(_seed.Unit1).Entries.Select(e => e.Marked).Should().Equal(true, true);
        fixture.Rows.Select(r => r.Marked).Should().Equal(Yes, Yes);
        fixture.Rows.Select(r => ((TestWord)r.Payload!).Marked).Should().Equal(true, true);
    }

    [Test]
    public async Task ChangeSelected_MarkedDisabled_KeepsMarksAndWritesIrregular()
    {
        var fixture = Create();
        SelectAll(fixture, Unit1);

        fixture.Multi.Marked = true;
        fixture.Multi.EnableIrregular = true;
        await fixture.ChangeSelectedCommand.ExecuteAsync(null);

        Load(_seed.Unit1).GetWord(_seed.Go.Index).Marked.Should().BeFalse();
        Load(_seed.Unit1).GetWord(_seed.House.Index).Marked.Should().BeTrue();
        _voc.Dictionary.GetWordsAndSubWords("go", "english", "german").Single().Irregular.Should().BeFalse();
    }

    [Test]
    public async Task ChangeSelected_MainEntry_MovesAll()
    {
        var fixture = Create();
        SelectAll(fixture, Letter);

        fixture.Multi.EnableMainEntry = true;
        fixture.Multi.MainEntry = "home";
        await fixture.ChangeSelectedCommand.ExecuteAsync(null);

        var house = _seed.House;
        var houses = _seed.Houses;
        _voc.Dictionary.GetMainEntry(ref house).Word.Should().Be("home");
        var main = _voc.Dictionary.GetMainEntry(ref houses);
        main.Word.Should().Be("home");
        main.Language.Should().Be("english");
        main.MainLanguage.Should().Be("german");
    }

    [Test]
    public async Task ChangeSelected_MainEntryOnSubGroup_UsesTheWordsLanguage()
    {
        var fixture = Create();
        SelectAll(fixture, Unit1);

        fixture.Multi.EnableMainEntry = true;
        fixture.Multi.MainEntry = "misc";
        await fixture.ChangeSelectedCommand.ExecuteAsync(null);

        var go = _seed.Go;
        var main = _voc.Dictionary.GetMainEntry(ref go);
        main.Word.Should().Be("misc");
        main.Language.Should().Be("english");
    }

    [Test]
    public async Task ChangeSelected_InputException_ShowsMessageAndStops()
    {
        var fixture = Create();
        SelectAll(fixture, Letter);

        fixture.Multi.EnablePost = true;
        fixture.Multi.Post = "(n)";
        fixture.Multi.EnableMainEntry = true;
        fixture.Multi.MainEntry = "a\"b";
        await fixture.ChangeSelectedCommand.ExecuteAsync(null);

        _dialogs.Verify(d => d.ShowMessageAsync(Strings.InvalidInputTitle, It.IsAny<string>()), Times.Once);
        var words = HouseWords();
        words.Should().HaveCount(2);
        words.Select(w => w.Post).Should().Equal("", "");
    }

    [Test]
    public async Task ChangeSelected_EntryExists_ShowsMessageAndContinues()
    {
        var fixture = Create();
        SelectAll(fixture, Letter);

        fixture.Multi.EnableWord = true;
        fixture.Multi.Word = "house";
        fixture.Multi.EnableMeaning = true;
        fixture.Multi.Meaning = "Haus";
        await fixture.ChangeSelectedCommand.ExecuteAsync(null);

        _dialogs.Verify(d => d.ShowMessageAsync(Strings.ProductName, Strings.EntryExists("house")), Times.Once);
        HouseWords().Select(w => w.Word).Should().BeEquivalentTo("house", "houses");
        fixture.SelectedRows.Should().HaveCount(2);
    }

    [Test]
    public async Task ChangeSelected_OverviewRows_ChangesNothing()
    {
        var fixture = Create();
        SelectAll(fixture, Dictionary, "german", "english");

        fixture.Multi.EnableMainEntry = true;
        fixture.Multi.MainEntry = "home";
        await fixture.ChangeSelectedCommand.ExecuteAsync(null);

        _voc.Dictionary.GetMainEntries("english", "german").Select(m => m.Word).Should().NotContain("home");
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void Multi_StartsWithEverythingDisabledAndTheFirstWordType()
    {
        var fixture = Create().Multi;

        new[]
        {
            fixture.EnablePre, fixture.EnableWord, fixture.EnablePost, fixture.EnableAdditionalInfo,
            fixture.EnableMeaning, fixture.EnableIrregular, fixture.EnableWordType, fixture.EnableMainEntry,
            fixture.EnableMarked,
        }.Should().OnlyContain(enabled => !enabled);
        fixture.WordTypeIndex.Should().Be(0);
        fixture.WordTypeDisplays.Should().HaveCount(fixture.WordTypeNames.Count);
    }
}
