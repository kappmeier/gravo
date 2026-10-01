using System.Collections.ObjectModel;
using FluentAssertions;
using Gravo;
using GravoApp.Services;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using Moq;
using NUnit.Framework;

namespace GravoApp.Tests.ViewModels;

public class WordInputViewModelTests
{
    private delegate void AddSubEntryCallback(
        ref WordEntry entry, string mainEntry, string language, string mainLanguage);

    private delegate void GroupAddCallback(
        ref GroupEntry group, ref WordEntry word, ref bool marked, ref string example);

    private readonly GroupEntry _unit1 = new(1, "Book", "Unit 1", "GroupBook01");

    private Mock<IDictionaryDao> _dictionary = null!;
    private Mock<IGroupsDao> _groups = null!;
    private Mock<IGroupDao> _group = null!;
    private Mock<IDialogService> _dialogs = null!;

    [SetUp]
    public void SetUp()
    {
        _dictionary = new Mock<IDictionaryDao>(MockBehavior.Strict);
        _dictionary.Setup(d => d.DictionaryLanguages("german")).Returns(new List<string> { "english", "italian" });
        _dictionary.Setup(d => d.DictionaryMainLanguages()).Returns(new List<string> { "german" });
        _groups = new Mock<IGroupsDao>(MockBehavior.Strict);
        _groups.Setup(g => g.GetGroups()).Returns(new Collection<string> { "Book" });
        _groups.Setup(g => g.GetSubGroups("Book")).Returns(new List<GroupEntry> { _unit1 });
        _groups.Setup(g => g.GetGroup("Book", "Unit 1")).Returns(_unit1);
        _group = new Mock<IGroupDao>(MockBehavior.Strict);
        _group.Setup(g => g.GetUniqueLanguage(ref It.Ref<GroupEntry>.IsAny)).Returns("english");
        _group.Setup(g => g.GetUniqueMainLanguage(ref It.Ref<GroupEntry>.IsAny)).Returns("german");
        _group.Setup(g => g.GetLanguages(ref It.Ref<GroupEntry>.IsAny)).Returns(new List<string> { "english" });
        _group.Setup(g => g.GetMainLanguages(ref It.Ref<GroupEntry>.IsAny)).Returns(new List<string> { "german" });
        _dialogs = Fakes.Dialogs();
    }

    private WordInputViewModel Create() => new(_dictionary.Object, _groups.Object, _group.Object,
        Fakes.Properties().Object, Fakes.Texts(), _dialogs.Object, AppServices.MainLanguage);

    private void SetUpAddSubEntry() =>
        _dictionary.Setup(d => d.AddSubEntry(ref It.Ref<WordEntry>.IsAny, "Haus", "english", "german"));

    private void SetUpAddToGroup(out MainEntry main, out WordEntry word)
    {
        var m = new MainEntry("Haus", "english", "german");
        var w = new WordEntry("house", "", "", WordType.Substantive, "Haus", "", false);
        _dictionary.Setup(d => d.GetMainEntry(ref It.Ref<string>.IsAny, "english", "german")).Returns(m);
        _dictionary.Setup(d => d.GetEntry(m, "house", "Haus")).Returns(w);
        main = m;
        word = w;
    }

    private static void FillHouse(WordInputViewModel fixture)
    {
        fixture.MainEntry = "Haus";
        fixture.Word = "house";
        fixture.Meaning = "Haus";
    }

    [Test]
    public void Constructor_LoadsLists()
    {
        var fixture = Create();
        fixture.Languages.Should().Equal("english", "italian");
        fixture.SelectedLanguage.Should().Be("english");
        fixture.MainLanguages.Should().Equal("german");
        fixture.SelectedMainLanguage.Should().Be("german");
        fixture.Groups.Should().Equal("Book");
        fixture.SelectedGroup.Should().Be("Book");
        fixture.SubGroups.Should().Equal("Unit 1");
        fixture.SelectedGroupEntry.Should().BeSameAs(_unit1);
        fixture.WordTypeNames[1].Should().Be("Verb");
        fixture.WordTypeDisplays[1].Should().Be("T4");
        fixture.CanDirectAdd.Should().BeTrue();
        fixture.DirectAdd.Should().BeFalse();
        fixture.Title.Should().Be("T" + localization.MAIN_MENU_VOCABULARY_ENLARGE_DICTIONARY);
    }

    [Test]
    public void MainEntry_MirrorsIntoWord_UntilWordIsEdited()
    {
        var fixture = Create();
        fixture.MainEntry = "Haus";
        fixture.Word.Should().Be("Haus");
        fixture.Word = "Häuser";
        fixture.MainEntry = "Hausx";
        fixture.Word.Should().Be("Häuser");
    }

    [Test]
    public async Task AddSubEntry_Done_MirrorsMainEntryAgain()
    {
        SetUpAddSubEntry();
        var fixture = Create();
        FillHouse(fixture);
        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        fixture.MainEntry = "Baum";
        fixture.Word.Should().Be("Baum");
    }

    [Test]
    public void NewLanguages_PrefillsAndUsesTypedTexts()
    {
        var fixture = Create();
        fixture.NewLanguages = true;
        fixture.LanguageText.Should().Be("english");
        fixture.MainLanguageText.Should().Be("german");
        fixture.LanguageText = "spanish";
        fixture.Language.Should().Be("spanish");
        fixture.MainLanguage.Should().Be("german");
        fixture.NewLanguages = false;
        fixture.Language.Should().Be("english");
    }

    [Test]
    public async Task AddSubEntry_NewWord_PassesAllFields()
    {
        WordEntry? captured = null;
        _dictionary.Setup(d => d.AddSubEntry(ref It.Ref<WordEntry>.IsAny, "Haus", "english", "german"))
            .Callback(new AddSubEntryCallback(
                (ref WordEntry entry, string _, string _, string _) => captured = entry));
        var fixture = Create();
        fixture.MainEntry = "Haus";
        fixture.Word = "house";
        fixture.Pre = "the";
        fixture.Post = "s";
        fixture.Meaning = "Haus";
        fixture.AdditionalInfo = "n.";
        fixture.SelectedWordTypeIndex = 0;
        fixture.Irregular = true;
        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        captured.Should().NotBeNull();
        captured!.Word.Should().Be("house");
        captured.Pre.Should().Be("the");
        captured.Post.Should().Be("s");
        captured.Meaning.Should().Be("Haus");
        captured.AdditionalTargetLangInfo.Should().Be("n.");
        captured.WordType.Should().Be(WordType.Substantive);
        captured.Irregular.Should().BeTrue();
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _dialogs.Verify(d => d.ConfirmAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task AddSubEntry_SelectedWordType_IsResolvedByName()
    {
        WordEntry? captured = null;
        _dictionary.Setup(d => d.AddSubEntry(ref It.Ref<WordEntry>.IsAny, "Haus", "english", "german"))
            .Callback(new AddSubEntryCallback(
                (ref WordEntry entry, string _, string _, string _) => captured = entry));
        var fixture = Create();
        FillHouse(fixture);
        fixture.SelectedWordTypeIndex = 5;
        fixture.SelectedWordType.Should().Be(WordType.SetPhrase);
        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        captured!.WordType.Should().Be(WordType.SetPhrase);
    }

    [Test]
    public async Task AddSubEntry_InputException_ShowsInvalidInput()
    {
        _dictionary.Setup(d => d.AddSubEntry(ref It.Ref<WordEntry>.IsAny, "Haus", "english", "german"))
            .Throws(new InputException(InputException.ErrorType.IllegalCharacter));
        var fixture = Create();
        FillHouse(fixture);
        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _dialogs.Verify(
            d => d.ShowMessageAsync("Fehlerhafte Eingabe", "Input must not contain a double quote (\")."), Times.Once);
    }

    [Test]
    public async Task AddSubEntry_MainEntryMissing_ConfirmYes_AddsMainAndRetries()
    {
        var mainAdded = false;
        _dictionary.Setup(d => d.AddSubEntry(ref It.Ref<WordEntry>.IsAny, "Haus", "english", "german"))
            .Callback(new AddSubEntryCallback((ref WordEntry entry, string _, string _, string _) =>
            {
                if (!mainAdded)
                {
                    throw new EntryNotFoundException("Main entry missing.");
                }
            }));
        _dictionary.Setup(d => d.AddEntry("Haus", "english", "german"))
            .Callback(() => mainAdded = true)
            .Returns(new MainEntry("Haus", "english", "german"));
        var fixture = Create();
        FillHouse(fixture);
        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ConfirmAsync("Haupteintrag nicht vorhanden",
            "Der Haupteintrag Haus ist für die gewählten Sprachen nicht vorhanden. Soll er erstellt werden?"),
            Times.Once);
        _dictionary.Verify(d => d.AddEntry("Haus", "english", "german"), Times.Once);
        _dictionary.Verify(
            d => d.AddSubEntry(ref It.Ref<WordEntry>.IsAny, "Haus", "english", "german"), Times.Exactly(2));
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task AddSubEntry_MainEntryMissing_ConfirmNo_Stops()
    {
        _dialogs = Fakes.Dialogs(confirm: false);
        _dictionary.Setup(d => d.AddSubEntry(ref It.Ref<WordEntry>.IsAny, "Haus", "english", "german"))
            .Throws(new EntryNotFoundException("Main entry missing."));
        var fixture = Create();
        FillHouse(fixture);
        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _dictionary.Verify(d => d.AddEntry(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _dictionary.Verify(
            d => d.AddSubEntry(ref It.Ref<WordEntry>.IsAny, "Haus", "english", "german"), Times.Once);
    }

    [Test]
    public async Task AddSubEntry_MainEntryCannotBeCreated_ShowsInvalidInputAndConflict()
    {
        var calls = 0;
        _dictionary.Setup(d => d.AddSubEntry(ref It.Ref<WordEntry>.IsAny, "Haus", "english", "german"))
            .Callback(new AddSubEntryCallback((ref WordEntry entry, string _, string _, string _) =>
            {
                calls++;
                throw calls == 1
                    ? (Exception)new EntryNotFoundException("Main entry missing.")
                    : new InvalidOperationException("Index conflict.");
            }));
        _dictionary.Setup(d => d.AddEntry("Haus", "english", "german"))
            .Throws(new LanguageNotFoundException("Language missing."));
        var fixture = Create();
        FillHouse(fixture);
        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync("Fehlerhafte Eingabe", "Language missing."), Times.Once);
        _dialogs.Verify(d => d.ShowMessageAsync("Fehler",
            "Eintrag nicht möglich, konflikt mit Index wahrscheinlich. Überprüfen Sie Ihre Datenbankversion."
            + Environment.NewLine + "Fehler: Main entry missing."), Times.Once);
        _dictionary.Verify(
            d => d.AddSubEntry(ref It.Ref<WordEntry>.IsAny, "Haus", "english", "german"), Times.Exactly(2));
    }

    [Test]
    public async Task AddSubEntry_DirectAddWithoutGroup_ShowsWarning()
    {
        _groups.Setup(g => g.GetGroups()).Returns(new Collection<string>());
        var fixture = Create();
        fixture.CanDirectAdd.Should().BeFalse();
        fixture.SelectedGroupEntry.Should().BeNull();
        FillHouse(fixture);
        fixture.DirectAdd = true;
        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync("Warnung",
            "Bitte wählen sie eine existierende Gruppe aus. Eintrag wird nicht erstellt!"), Times.Once);
        _dictionary.Verify(
            d => d.AddSubEntry(ref It.Ref<WordEntry>.IsAny, It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task AddSubEntry_DirectAdd_EmptyGroup_ConfirmNo_Aborts()
    {
        _dialogs = Fakes.Dialogs(confirm: false);
        _group.Setup(g => g.GetLanguages(ref It.Ref<GroupEntry>.IsAny)).Returns(new List<string>());
        var fixture = Create();
        FillHouse(fixture);
        fixture.DirectAdd = true;
        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ConfirmAsync("Neue Sprache",
            "Es ist bisher noch kein Eintrag in der gewählten Gruppe vorhanden. Soll ein neuer Eintrag mit den "
            + "Sprachen 'english' und 'german' erstellt werden?"), Times.Once);
        _dictionary.Verify(
            d => d.AddSubEntry(ref It.Ref<WordEntry>.IsAny, It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task AddSubEntry_DirectAdd_SecondLanguage_ConfirmNo_Aborts()
    {
        _dialogs = Fakes.Dialogs(confirm: false);
        var fixture = Create();
        FillHouse(fixture);
        fixture.SelectedLanguage = "italian";
        fixture.DirectAdd = true;
        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ConfirmAsync("Neue Sprache",
            "Sie beabsichtigen einen eintrag mit den zweiten Sprachen 'italian' und 'german' zu erstellen. Soll "
            + "damit fortgefahren werden?"), Times.Once);
        _dictionary.Verify(
            d => d.AddSubEntry(ref It.Ref<WordEntry>.IsAny, It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task AddSubEntry_DirectAdd_AddsToGroupWithMarked()
    {
        SetUpAddSubEntry();
        SetUpAddToGroup(out _, out var word);
        GroupEntry? capturedGroup = null;
        WordEntry? capturedWord = null;
        bool? capturedMarked = null;
        string? capturedExample = null;
        _group.Setup(g => g.Add(ref It.Ref<GroupEntry>.IsAny, ref It.Ref<WordEntry>.IsAny, ref It.Ref<bool>.IsAny,
                ref It.Ref<string>.IsAny))
            .Callback(new GroupAddCallback((ref GroupEntry group, ref WordEntry entry, ref bool marked,
                ref string example) =>
            {
                capturedGroup = group;
                capturedWord = entry;
                capturedMarked = marked;
                capturedExample = example;
            }));
        var fixture = Create();
        FillHouse(fixture);
        fixture.DirectAdd = true;
        fixture.Marked = true;
        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        capturedGroup.Should().BeSameAs(_unit1);
        capturedWord.Should().BeSameAs(word);
        capturedMarked.Should().BeTrue();
        capturedExample.Should().Be("");
        _dialogs.Verify(d => d.ConfirmAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task AddSubEntry_DirectAdd_WordExistsInDictionary_AddsToGroup()
    {
        _dictionary.Setup(d => d.AddSubEntry(ref It.Ref<WordEntry>.IsAny, "Haus", "english", "german"))
            .Throws(new EntryExistsException("Entry exists."));
        SetUpAddToGroup(out _, out _);
        _group.Setup(g => g.Add(ref It.Ref<GroupEntry>.IsAny, ref It.Ref<WordEntry>.IsAny, ref It.Ref<bool>.IsAny,
            ref It.Ref<string>.IsAny));
        var fixture = Create();
        FillHouse(fixture);
        fixture.DirectAdd = true;
        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _group.Verify(g => g.Add(ref It.Ref<GroupEntry>.IsAny, ref It.Ref<WordEntry>.IsAny, ref It.Ref<bool>.IsAny,
            ref It.Ref<string>.IsAny), Times.Once);
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task AddSubEntry_DirectAdd_WordAlreadyInGroup_ShowsMessage()
    {
        SetUpAddSubEntry();
        SetUpAddToGroup(out _, out _);
        _group.Setup(g => g.Add(ref It.Ref<GroupEntry>.IsAny, ref It.Ref<WordEntry>.IsAny, ref It.Ref<bool>.IsAny,
                ref It.Ref<string>.IsAny))
            .Throws(new EntryExistsException("Entry exists."));
        var fixture = Create();
        FillHouse(fixture);
        fixture.DirectAdd = true;
        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _dialogs.Verify(
            d => d.ShowMessageAsync("Hinzufügen nicht möglich", "Wort bereits in der Gruppe enthalten"), Times.Once);
    }

    [Test]
    public void SelectSubGroup_UniqueLanguages_SelectsThem()
    {
        _group.Setup(g => g.GetUniqueLanguage(ref It.Ref<GroupEntry>.IsAny)).Returns("italian");
        Create().SelectedLanguage.Should().Be("italian");
    }

    [Test]
    public void SelectSubGroup_MultipleLanguages_ShowsHint()
    {
        _group.Setup(g => g.GetUniqueLanguage(ref It.Ref<GroupEntry>.IsAny))
            .Throws(new LanguageException("More than one language."));
        var fixture = Create();
        _dialogs.Verify(d => d.ShowMessageAsync("Hinweis",
            "Sprache konnte nicht automatisch festgelegt werden. Bitte setzen sie manuell."), Times.Once);
        fixture.SelectedLanguage.Should().Be("english");
    }

    [Test]
    public void SelectSubGroup_MultipleMainLanguages_ShowsHint()
    {
        _group.Setup(g => g.GetUniqueMainLanguage(ref It.Ref<GroupEntry>.IsAny))
            .Throws(new LanguageException("More than one language."));
        Create();
        _dialogs.Verify(d => d.ShowMessageAsync("Hinweis",
            "Hauptsprache konnte nicht automatisch festgelegt werden. Bitte setzen sie manuell."), Times.Once);
    }

    [Test]
    public void Close_RequestsCloseWithoutResult()
    {
        var fixture = Create();
        bool? closed = null;
        fixture.CloseRequested += ok => closed = ok;
        fixture.CloseCommand.Execute(null);
        closed.Should().BeFalse();
    }
}
