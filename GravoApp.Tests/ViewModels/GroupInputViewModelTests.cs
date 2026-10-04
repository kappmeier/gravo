using FluentAssertions;
using GravoApp.Localization;
using GravoApp.Services;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using Moq;
using NUnit.Framework;

namespace GravoApp.Tests.ViewModels;

public class GroupInputViewModelTests
{
    private static readonly string NL = Environment.NewLine;

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

    private GroupInputViewModel Create() => new(
        _voc.Groups, _voc.Dictionary, _voc.Group, _dialogs.Object, Fakes.Texts(), TempVocabulary.MainLanguage);

    [Test]
    public void Constructor_LoadsGroupsUnitsLanguagesAndCounts()
    {
        var fixture = Create();
        fixture.Title.Should().Be("T60");
        fixture.Groups.Should().Equal("Book", "Other");
        fixture.SelectedGroup.Should().Be("Book");
        fixture.SubGroups.Should().Equal("Unit 1", "Unit 2");
        fixture.SelectedSubGroup.Should().Be("Unit 1");
        fixture.WordsInGroup.Should().Equal("house", "go");
        fixture.SelectedWordInGroup.Should().BeNull();
        fixture.Languages.Should().Equal("english", "french");
        fixture.SelectedLanguage.Should().Be("english");
        fixture.WordsInGroupText.Should().Be("2 Einträge in der Gruppe insgesamt.");
        fixture.WordsInSubGroupText.Should()
            .Be("2 verschiedene Einträge in der Gruppe," + NL + "2 Einträge insgesamt.");
        fixture.WordsInLanguageText.Should().Be("2 Einträge in der Sprache.");
    }

    [Test]
    public void Search_FindsMainEntryAndListsItsWords()
    {
        var fixture = Create();
        fixture.SearchText = "hou";
        fixture.SimilarWord.Should().Be("house");
        fixture.Words.Select(r => r.Word).Should().Equal("house", "houses");
        fixture.Words[0].Meaning.Should().Be("Haus");
        fixture.SelectedWord.Should().BeSameAs(fixture.Words[0]);
    }

    [Test]
    public void Search_ShortensUntilAMatch()
    {
        var fixture = Create();
        fixture.SearchText = "housez";
        fixture.SimilarWord.Should().Be("house");
        fixture.Words.Select(r => r.Word).Should().Equal("house", "houses");
    }

    [Test]
    public void Search_NoMatch_ClearsSimilarWord()
    {
        var fixture = Create();
        fixture.SearchText = "hou";
        fixture.SearchText = "zzz";
        fixture.SimilarWord.Should().BeEmpty();
    }

    [Test]
    public void SelectWordInGroup_ListsItsGroupRows()
    {
        var fixture = Create();
        fixture.SelectedWordInGroup = "house";
        var row = fixture.Meanings.Should().ContainSingle().Which;
        row.Word.Should().Be("house");
        row.Meaning.Should().Be("Haus");
        fixture.SelectedMeaning.Should().BeSameAs(row);
    }

    [Test]
    public async Task Select_AddsWordToSubGroupWithMarkedFlag()
    {
        var fixture = Create();
        fixture.SelectedSubGroup = "Unit 2";
        fixture.SearchText = "go";
        fixture.Marked = true;
        await fixture.SelectCommand.ExecuteAsync(null);
        fixture.WordsInGroup.Should().Equal("go");
        fixture.SelectedWordInGroup.Should().Be("go");
        var unit2 = _seed.Unit2;
        var entry = _voc.Group.Load(ref unit2).Entries.Should().ContainSingle().Which;
        entry.Word.Should().Be("go");
        entry.Marked.Should().BeTrue();
    }

    [Test]
    public async Task Select_DuplicateWord_ShowsMessage()
    {
        var fixture = Create();
        fixture.SearchText = "go";
        await fixture.SelectCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync(Strings.AddNotPossibleTitle, Strings.WordAlreadyInGroup), Times.Once);
        var unit1 = _seed.Unit1;
        _voc.Group.Load(ref unit1).Entries.Select(e => e.Word).Should().Equal("house", "go");
    }

    [Test]
    public async Task Select_RaisesWordAddedAfterEachAdd()
    {
        var fixture = Create();
        var raised = 0;
        fixture.WordAdded += () => raised++;
        fixture.SelectedSubGroup = "Unit 2";
        fixture.SearchText = "go";
        await fixture.SelectCommand.ExecuteAsync(null);
        fixture.SearchText = "hou";
        await fixture.SelectCommand.ExecuteAsync(null);
        raised.Should().Be(2);
    }

    [Test]
    public async Task Select_DuplicateWord_DoesNotRaiseWordAdded()
    {
        var fixture = Create();
        var raised = 0;
        fixture.WordAdded += () => raised++;
        fixture.SearchText = "go";
        await fixture.SelectCommand.ExecuteAsync(null);
        raised.Should().Be(0);
    }

    [Test]
    public void Deselect_RemovesSelectedRow()
    {
        var fixture = Create();
        fixture.SelectedWordInGroup = "house";
        fixture.DeselectCommand.Execute(null);
        fixture.WordsInGroup.Should().Equal("go");
        fixture.SelectedWordInGroup.Should().Be("go");
        fixture.Meanings.Should().ContainSingle().Which.Meaning.Should().Be("gehen");
        var unit1 = _seed.Unit1;
        _voc.Group.Load(ref unit1).Entries.Should().ContainSingle().Which.Word.Should().Be("go");
    }

    [Test]
    public void SelectSubGroup_SwitchesToItsUniqueLanguage()
    {
        var fixture = Create();
        fixture.SelectedGroup = "Other";
        fixture.SubGroups.Should().Equal("Words");
        fixture.SelectedSubGroup.Should().Be("Words");
        fixture.WordsInGroup.Should().Equal("maison");
        fixture.SelectedLanguage.Should().Be("french");
        fixture.WordsInLanguageText.Should().Be("1 Eintrag in der Sprache.");
    }

    [Test]
    public void Close_RequestsClose()
    {
        var fixture = Create();
        bool? result = null;
        fixture.CloseRequested += ok => result = ok;
        fixture.CloseCommand.Execute(null);
        result.Should().BeFalse();
    }
}
