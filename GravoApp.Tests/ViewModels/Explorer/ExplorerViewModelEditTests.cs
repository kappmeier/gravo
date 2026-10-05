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

public class ExplorerViewModelEditTests
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

    /// <summary>Selects the node at <paramref name="path"/> and the row of <paramref name="word"/>.</summary>
    private static ExplorerRow SelectRow(ExplorerViewModel vm, string word, params string[] path)
    {
        vm.SelectedNode = Node(vm, path);
        var row = vm.Rows.Single(r => r.Word == word);
        vm.SelectedRow = row;
        return row;
    }

    private void VerifyMessage(string title, string message) =>
        _dialogs.Verify(d => d.ShowMessageAsync(title, message), Times.Once);

    private GroupDto Load(GroupEntry group) => _voc.Group.Load(ref group);

    [Test]
    public void SelectWordRow_FillsEditor()
    {
        var fixture = Create();

        SelectRow(fixture, "house", Groups, "Book", "Unit 1");
        fixture.Editor.MainEntry.Should().Be("house");
        fixture.Editor.Word.Should().Be("house");
        fixture.Editor.Meaning.Should().Be("Haus");
        fixture.Editor.Language.Should().Be("english");
        fixture.Editor.MainLanguage.Should().Be("german");
        fixture.Editor.Marked.Should().BeTrue();
        fixture.Editor.Irregular.Should().BeFalse();
        fixture.Editor.WordTypeIndex.Should().Be(fixture.Editor.WordTypeNames.ToList().IndexOf("Substantive"));
        fixture.Editor.WordTypeDisplays[fixture.Editor.WordTypeIndex].Should()
            .Be("T" + localization.WORD_TYPE_SUBSTANTIVE);
        fixture.ShowGroupFields.Should().BeTrue();
        fixture.ShowEditor.Should().BeTrue();
    }

    [Test]
    public void SelectDictionaryRow_SetsIrregular()
    {
        var fixture = Create();

        SelectRow(fixture, "go", Groups, "Book", "Unit 1");
        SelectRow(fixture, "go", Dictionary, "german", "english", "G");
        fixture.Editor.Irregular.Should().BeTrue();
        SelectRow(fixture, "house", Dictionary, "german", "english", "H");
        fixture.Editor.Irregular.Should().BeFalse();
        fixture.Editor.WordTypeIndex.Should().Be(fixture.Editor.WordTypeNames.ToList().IndexOf("Substantive"));
    }

    [Test]
    public void SelectLetterRow_HidesGroupFields()
    {
        var fixture = Create();

        SelectRow(fixture, "houses", Dictionary, "german", "english", "H");
        fixture.ShowGroupFields.Should().BeFalse();
        fixture.ShowEditor.Should().BeTrue();
        fixture.Editor.Word.Should().Be("houses");
        fixture.Editor.MainEntry.Should().Be("house");
    }

    [Test]
    public void SelectGroupRow_KeepsMarkedOutsideSubGroups()
    {
        var fixture = Create();

        SelectRow(fixture, "house", Groups, "Book", "Unit 1");
        SelectRow(fixture, "go", Groups, "Book");
        fixture.Editor.Word.Should().Be("go");
        fixture.Editor.Marked.Should().BeTrue();
        fixture.ShowGroupFields.Should().BeFalse();
    }

    [Test]
    public void SelectSubRow_OnLanguageNode_FillsEditor()
    {
        var fixture = Create();

        SelectRow(fixture, "house", Dictionary, "german", "english");
        fixture.SelectedSubRow = fixture.SubRows.Single(r => r.Word == "houses");
        fixture.Editor.Word.Should().Be("houses");
        fixture.Editor.Meaning.Should().Be("Häuser");
        fixture.ShowEditor.Should().BeTrue();
    }

    [Test]
    public void SelectOverviewNode_HidesEditor()
    {
        var fixture = Create();

        fixture.SelectedNode = fixture.Roots[0];
        fixture.ShowEditor.Should().BeFalse();
        fixture.SelectedNode = Node(fixture, Dictionary, "german");
        fixture.ShowEditor.Should().BeFalse();
        fixture.SelectedNode = fixture.Roots[1];
        fixture.ShowEditor.Should().BeFalse();
    }

    [Test]
    public void PanelMode_Multi_ShowsMultiEditor()
    {
        var fixture = Create();
        fixture.SelectedNode = Node(fixture, Dictionary, "german", "english", "H");

        fixture.Panel = PanelMode.Multi;
        fixture.ShowMultiEditor.Should().BeTrue();
        fixture.ShowEditor.Should().BeFalse();
        fixture.IsMultiPanel.Should().BeTrue();
        fixture.IsDefaultPanel.Should().BeFalse();

        fixture.IsSearchPanel = true;
        fixture.Panel.Should().Be(PanelMode.Search);
        fixture.ShowMultiEditor.Should().BeFalse();
        fixture.ShowEditor.Should().BeFalse();
    }

    [Test]
    public void PanelMode_Default_SwitchesWithTheSelectionCount()
    {
        var fixture = Create();
        fixture.SelectedNode = Node(fixture, Dictionary, "german", "english", "H");
        var changed = new List<string?>();
        fixture.PropertyChanged += (_, e) => changed.Add(e.PropertyName);

        fixture.SelectedRows.Add(fixture.Rows[0]);
        fixture.SelectedRows.Add(fixture.Rows[1]);
        fixture.ShowMultiEditor.Should().BeTrue();
        fixture.ShowEditor.Should().BeFalse();
        changed.Should().Contain(new[] { nameof(fixture.ShowEditor), nameof(fixture.ShowMultiEditor) });

        fixture.Panel = PanelMode.Input;
        fixture.ShowMultiEditor.Should().BeFalse();
        fixture.ShowEditor.Should().BeTrue();
    }

    [Test]
    public async Task AddWord_OnLetterNode_AddsToDictionaryAndList()
    {
        var fixture = Create();
        var letter = Node(fixture, Dictionary, "german", "english", "H");
        fixture.SelectedNode = letter;
        var added = 0;
        fixture.WordAdded += () => added++;

        fixture.Editor.MainEntry = "hat";
        fixture.Editor.Word = "hat";
        fixture.Editor.Meaning = "Hut";
        await fixture.AddWordCommand.ExecuteAsync(null);

        _dialogs.Verify(d => d.ConfirmAsync(Strings.MainEntryMissingTitle,
            Strings.MainEntryMissingForLanguages("hat", "german", "english")), Times.Once);
        _voc.Dictionary.GetWords("english", "german").Select(w => w.Word).Should().Contain("hat");
        var row = fixture.Rows.Should().ContainSingle(r => r.Word == "hat").Which;
        fixture.SelectedRow.Should().BeSameAs(row);
        fixture.SelectedNode.Should().BeSameAs(letter);
        letter.Children.Select(n => n.Title).Should().BeEquivalentTo("hat", "house");
        added.Should().Be(1);
    }

    [Test]
    public async Task AddWord_MainEntryDeclined_AddsNothing()
    {
        _dialogs = Fakes.Dialogs(confirm: false);
        var fixture = Create();
        fixture.SelectedNode = Node(fixture, Dictionary, "german", "english", "H");

        fixture.Editor.MainEntry = "hat";
        fixture.Editor.Word = "hat";
        fixture.Editor.Meaning = "Hut";
        await fixture.AddWordCommand.ExecuteAsync(null);

        _voc.Dictionary.GetWords("english", "german").Select(w => w.Word).Should().NotContain("hat");
        fixture.Rows.Select(r => r.Word).Should().Equal("house", "houses");
    }

    [Test]
    public async Task AddWord_EmptyMainEntry_UsesTheWord()
    {
        var fixture = Create();
        fixture.SelectedNode = Node(fixture, Dictionary, "german", "english", "H", "house");

        fixture.Editor.MainEntry = "";
        fixture.Editor.Word = "house";
        fixture.Editor.Meaning = "Gebäude";
        await fixture.AddWordCommand.ExecuteAsync(null);

        fixture.Editor.MainEntry.Should().Be("house");
        _dialogs.Verify(d => d.ConfirmAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        // Words equal to the main entry come in meaning order, which is the order of the unique word index.
        fixture.Rows.Select(r => r.Meaning).Should().Equal("Gebäude", "Haus", "Häuser");
        fixture.SelectedRow!.Meaning.Should().Be("Gebäude");
    }

    [Test]
    public async Task AddWord_OnSubGroupNode_AddsToGroupWhenChecked()
    {
        var fixture = Create();
        SelectRow(fixture, "house", Groups, "Book", "Unit 1");
        var unit2 = Node(fixture, Groups, "Book", "Unit 2");
        fixture.SelectedNode = unit2;

        fixture.Editor.AddToGroup = true;
        fixture.Editor.Marked = true;
        fixture.Editor.MainEntry = "cat";
        fixture.Editor.Word = "cat";
        fixture.Editor.Meaning = "Katze";
        await fixture.AddWordCommand.ExecuteAsync(null);

        var entry = Load(_seed.Unit2).Entries.Should().ContainSingle().Which;
        entry.Word.Should().Be("cat");
        entry.Marked.Should().BeTrue();
        var row = fixture.Rows.Should().ContainSingle().Which;
        row.Word.Should().Be("cat");
        row.Marked.Should().Be(Yes);
        fixture.SelectedRow.Should().BeSameAs(row);
        unit2.Children.Select(n => n.Title).Should().Equal("cat");
    }

    [Test]
    public async Task AddWord_OnSubGroupNodeUnchecked_AddsToDictionaryOnly()
    {
        var fixture = Create();
        fixture.SelectedNode = Node(fixture, Groups, "Book", "Unit 1");

        fixture.Editor.AddToGroup = false;
        fixture.Editor.MainEntry = "go";
        fixture.Editor.Word = "goes";
        fixture.Editor.Meaning = "geht";
        await fixture.AddWordCommand.ExecuteAsync(null);

        _voc.Dictionary.GetSubWords("go", "english", "german").Select(w => w.Word).Should().Contain("goes");
        Load(_seed.Unit1).Entries.Select(e => e.Word).Should().BeEquivalentTo("house", "go");
        fixture.Rows.Select(r => r.Word).Should().Equal("house", "go");
        fixture.Editor.Word.Should().Be("goes");
        fixture.Editor.Meaning.Should().Be("geht");
    }

    [Test]
    public async Task AddWord_ExistingWordOnSubGroup_AddsTheStoredWordToGroup()
    {
        var fixture = Create();
        fixture.SelectedNode = Node(fixture, Groups, "Book", "Unit 2");

        fixture.Editor.AddToGroup = true;
        fixture.Editor.Language = "english";
        fixture.Editor.MainLanguage = "german";
        fixture.Editor.MainEntry = "house";
        fixture.Editor.Word = "houses";
        fixture.Editor.Meaning = "Häuser";
        await fixture.AddWordCommand.ExecuteAsync(null);

        Load(_seed.Unit2).Entries.Should().ContainSingle().Which.WordIndex.Should().Be(_seed.Houses.Index);
        _voc.Dictionary.GetWordsAndSubWords("house", "english", "german").Should().HaveCount(2);
        fixture.SelectedRow!.Word.Should().Be("houses");
    }

    [Test]
    public async Task AddWord_WordAlreadyInGroup_ShowsMessage()
    {
        var fixture = Create();
        SelectRow(fixture, "go", Groups, "Book", "Unit 1");

        fixture.Editor.AddToGroup = true;
        await fixture.AddWordCommand.ExecuteAsync(null);

        VerifyMessage(Strings.AddNotPossibleTitle, Strings.WordAlreadyInGroup);
        Load(_seed.Unit1).Entries.Should().HaveCount(2);
    }

    [Test]
    public async Task AddWord_OnGroupNodeWithTwoLanguages_ShowsMessage()
    {
        _voc.AddToGroup(_seed.Words, _seed.House);
        var fixture = Create();
        fixture.SelectedNode = Node(fixture, Groups, "Other", "Words");

        fixture.Editor.AddToGroup = true;
        fixture.Editor.Word = "cat";
        fixture.Editor.Meaning = "Katze";
        await fixture.AddWordCommand.ExecuteAsync(null);

        VerifyMessage(Strings.EnglishWarningTitle, Strings.TooManyLanguagesInGroup);
        Load(_seed.Words).Entries.Should().HaveCount(2);
        _voc.Dictionary.GetWords("english", "german").Select(w => w.Word).Should().NotContain("cat");
    }

    [Test]
    public async Task AddWord_OnGroupWithTwoMainLanguages_ShowsMessage()
    {
        _voc.Dictionary.AddEntry("house", "english", "spanish");
        var casa = new WordEntry("house", "", "", WordType.Substantive, "casa", "", false);
        _voc.Dictionary.AddSubEntry(ref casa, "house", "english", "spanish");
        _voc.AddToGroup(_seed.Unit1, casa);
        var fixture = Create();
        fixture.SelectedNode = Node(fixture, Groups, "Book", "Unit 1");

        fixture.Editor.Word = "cat";
        fixture.Editor.Meaning = "Katze";
        await fixture.AddWordCommand.ExecuteAsync(null);

        VerifyMessage(Strings.EnglishWarningTitle, Strings.TooManyMainLanguagesInGroup);
        _voc.Dictionary.GetWords("english", "german").Select(w => w.Word).Should().NotContain("cat");
    }

    [Test]
    public async Task AddWord_MissingLanguage_ShowsMessage()
    {
        var fixture = Create();
        fixture.SelectedNode = Node(fixture, Groups, "Book", "Unit 2");

        fixture.Editor.Word = "cat";
        fixture.Editor.Meaning = "Katze";
        await fixture.AddWordCommand.ExecuteAsync(null);

        fixture.Editor.MainEntry.Should().Be("cat");
        VerifyMessage(Strings.EnglishErrorTitle, Strings.LanguageRequired);
    }

    [Test]
    public async Task AddWord_OnNodeWithoutLanguage_ShowsCannotAddHere()
    {
        var fixture = Create();

        var language = Node(fixture, Dictionary, "german", "english");
        var group = Node(fixture, Groups, "Book");
        var nodes = new[] { fixture.Roots[0], language, group };
        foreach (var node in nodes)
        {
            fixture.SelectedNode = node;
            await fixture.AddWordCommand.ExecuteAsync(null);
        }

        _dialogs.Verify(d => d.ShowMessageAsync(Strings.EnglishErrorTitle, Strings.CannotAddHere), Times.Exactly(3));
        _dialogs.Verify(d => d.ConfirmAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task AddWord_InputException_ShowsInvalidInput()
    {
        var fixture = Create();
        fixture.SelectedNode = Node(fixture, Dictionary, "german", "english", "H");

        fixture.Editor.MainEntry = "house";
        fixture.Editor.Word = "x\"y";
        fixture.Editor.Meaning = "Hut";
        await fixture.AddWordCommand.ExecuteAsync(null);

        _dialogs.Verify(d => d.ShowMessageAsync(Strings.InvalidInputTitle, It.IsAny<string>()), Times.Once);
        _voc.Dictionary.GetWordsAndSubWords("house", "english", "german").Should().HaveCount(2);
    }

    [Test]
    public async Task ChangeWord_UpdatesDictionaryAndRow()
    {
        var fixture = Create();
        var row = SelectRow(fixture, "houses", Dictionary, "german", "english", "H");
        var index = fixture.Rows.IndexOf(row);

        fixture.Editor.Meaning = "Häuser (pl.)";
        fixture.Editor.Post = "(pl)";
        await fixture.ChangeWordCommand.ExecuteAsync(null);

        var houses = _seed.Houses;
        var main = _voc.Dictionary.GetMainEntry(ref houses);
        var stored = _voc.Dictionary.GetEntry(main, "houses", "Häuser (pl.)");
        stored.Post.Should().Be("(pl)");
        stored.Index.Should().Be(_seed.Houses.Index);
        fixture.Rows[index].Meaning.Should().Be("Häuser (pl.)");
        fixture.Rows[index].Post.Should().Be("(pl)");
        fixture.SelectedRow.Should().BeSameAs(fixture.Rows[index]);
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task ChangeWord_OnLanguageNode_ChangesTheSubRow()
    {
        var fixture = Create();
        SelectRow(fixture, "house", Dictionary, "german", "english");
        fixture.SelectedSubRow = fixture.SubRows.Single(r => r.Word == "houses");

        fixture.Editor.Irregular = true;
        await fixture.ChangeWordCommand.ExecuteAsync(null);

        _voc.Dictionary.GetSubWords("house", "english", "german").Single(w => w.Word == "houses").Irregular
            .Should().BeTrue();
        fixture.SelectedSubRow!.Word.Should().Be("houses");
        fixture.SelectedSubRow.Irregular.Should().Be(Yes);
        fixture.SubRows.Should().HaveCount(2);
        fixture.SelectedRow!.Word.Should().Be("house");
    }

    [Test]
    public async Task ChangeWord_OnSubGroupRow_UpdatesMarked()
    {
        var fixture = Create();
        var row = SelectRow(fixture, "go", Groups, "Book", "Unit 1");
        var index = fixture.Rows.IndexOf(row);

        fixture.Editor.Marked = true;
        await fixture.ChangeWordCommand.ExecuteAsync(null);

        Load(_seed.Unit1).GetWord(_seed.Go.Index).Marked.Should().BeTrue();
        fixture.Rows[index].Marked.Should().Be(Yes);
        fixture.Rows[index].Payload.Should().BeOfType<TestWord>().Which.Marked.Should().BeTrue();
    }

    [Test]
    public async Task ChangeWord_NewMainEntry_MovesWord()
    {
        var fixture = Create();
        SelectRow(fixture, "houses", Dictionary, "german", "english", "H");

        fixture.Editor.MainEntry = "houses";
        await fixture.ChangeWordCommand.ExecuteAsync(null);

        var houses = _seed.Houses;
        _voc.Dictionary.GetMainEntry(ref houses).Word.Should().Be("houses");
    }

    [Test]
    public async Task ChangeWord_NewMainEntryOnGroupRow_UsesTheWordsLanguage()
    {
        var fixture = Create();
        SelectRow(fixture, "go", Groups, "Book", "Unit 1");

        fixture.Editor.MainEntry = "went";
        await fixture.ChangeWordCommand.ExecuteAsync(null);

        var go = _seed.Go;
        var main = _voc.Dictionary.GetMainEntry(ref go);
        main.Word.Should().Be("went");
        main.Language.Should().Be("english");
        main.MainLanguage.Should().Be("german");
    }

    [Test]
    public async Task ChangeWord_NoSelection_ShowsMessage()
    {
        var fixture = Create();
        fixture.SelectedNode = Node(fixture, Dictionary, "german", "english", "H");

        fixture.SelectedRow = null;
        await fixture.ChangeWordCommand.ExecuteAsync(null);

        VerifyMessage(Strings.ProductName, Strings.SelectOne);
    }

    [Test]
    public async Task ChangeWord_MultipleSelection_ShowsMessage()
    {
        var fixture = Create();
        fixture.SelectedNode = Node(fixture, Dictionary, "german", "english", "H");

        fixture.SelectedRows.Add(fixture.Rows[0]);
        fixture.SelectedRows.Add(fixture.Rows[1]);
        fixture.Editor.Meaning = "anders";
        await fixture.ChangeWordCommand.ExecuteAsync(null);

        VerifyMessage(Strings.ProductName, Strings.SelectOnlyOne);
        _voc.Dictionary.GetWordsAndSubWords("house", "english", "german").Select(w => w.Meaning).Should()
            .Equal("Haus", "Häuser");
    }

    [Test]
    public async Task ChangeWord_DuplicateWord_ShowsEntryExists()
    {
        var fixture = Create();
        SelectRow(fixture, "houses", Dictionary, "german", "english", "H");

        fixture.Editor.Word = "house";
        fixture.Editor.Meaning = "Haus";
        await fixture.ChangeWordCommand.ExecuteAsync(null);

        VerifyMessage(Strings.ProductName, Strings.EntryExists("house"));
        fixture.Rows.Select(r => r.Word).Should().Equal("house", "houses");
    }

    [Test]
    public async Task ChangeWord_InputException_ShowsInvalidInput()
    {
        var fixture = Create();
        SelectRow(fixture, "houses", Dictionary, "german", "english", "H");

        fixture.Editor.Meaning = "a\"b";
        await fixture.ChangeWordCommand.ExecuteAsync(null);

        _dialogs.Verify(d => d.ShowMessageAsync(Strings.InvalidInputTitle, It.IsAny<string>()), Times.Once);
    }
}
