using System.Collections.ObjectModel;
using AwesomeAssertions;
using Gravo;
using GravoApp.Services;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using Moq;
using NUnit.Framework;

namespace GravoApp.Tests.ViewModels;

public class WordInputViewModelTests
{
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
        _group.Setup(g => g.GetUniqueLanguage(It.IsAny<GroupEntry>())).Returns("english");
        _group.Setup(g => g.GetUniqueMainLanguage(It.IsAny<GroupEntry>())).Returns("german");
        _group.Setup(g => g.GetLanguages(It.IsAny<GroupEntry>())).Returns(new List<string> { "english" });
        _group.Setup(g => g.GetMainLanguages(It.IsAny<GroupEntry>())).Returns(new List<string> { "german" });
        _dialogs = Fakes.Dialogs();
    }

    private WordInputViewModel Create() => new(_dictionary.Object, _groups.Object, _group.Object,
        Fakes.Properties().Object, Fakes.Texts(), _dialogs.Object, AppServices.MainLanguage);

    private void SetUpAddSubEntry() =>
        _dictionary.Setup(d => d.AddSubEntry(It.IsAny<WordEntry>(), "Haus", "english", "german"));

    private void SetUpAddToGroup(out MainEntry main, out WordEntry word)
    {
        var m = new MainEntry("Haus", "english", "german");
        var w = new WordEntry("house", "", "", WordType.Substantive, "Haus", "", false);
        _dictionary.Setup(d => d.GetMainEntry(It.IsAny<string>(), "english", "german")).Returns(m);
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

    private void MakeGroupLanguagesAmbiguous()
    {
        _group.Setup(g => g.GetUniqueLanguage(It.IsAny<GroupEntry>()))
            .Throws(new LanguageException("two"));
        _group.Setup(g => g.GetUniqueMainLanguage(It.IsAny<GroupEntry>()))
            .Throws(new LanguageException("two"));
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
        fixture.WordTypeValues[1].Should().Be(WordType.Verb);
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
    public async Task AddSubEntry_TwoAdds_RequestMainEntrySelectionAfterEach()
    {
        SetUpAddSubEntry();

        var fixture = Create();
        var requested = 0;
        fixture.SelectMainEntryRequested += () => requested++;
        FillHouse(fixture);
        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        await fixture.AddSubEntryCommand.ExecuteAsync(null);

        requested.Should().Be(2);
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
        _dictionary.Setup(d => d.AddSubEntry(It.IsAny<WordEntry>(), "Haus", "english", "german"))
            .Callback<WordEntry, string, string, string>((entry, _, _, _) => captured = entry);

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
    public async Task AddSubEntry_SelectedWordType_IsStored()
    {
        WordEntry? captured = null;
        _dictionary.Setup(d => d.AddSubEntry(It.IsAny<WordEntry>(), "Haus", "english", "german"))
            .Callback<WordEntry, string, string, string>((entry, _, _, _) => captured = entry);

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
        _dictionary.Setup(d => d.AddSubEntry(It.IsAny<WordEntry>(), "Haus", "english", "german"))
            .Throws(new InputException(InputException.ErrorType.IllegalCharacter));

        var fixture = Create();
        FillHouse(fixture);

        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync("T119", "Input must not contain a double quote (\")."), Times.Once);
    }

    [Test]
    public async Task AddSubEntry_MainEntryMissing_ConfirmYes_AddsMainAndRetries()
    {
        var mainAdded = false;
        _dictionary.Setup(d => d.AddSubEntry(It.IsAny<WordEntry>(), "Haus", "english", "german"))
            .Callback<WordEntry, string, string, string>((entry, _, _, _) =>
            {
                if (!mainAdded)
                {
                    throw new EntryNotFoundException("Main entry missing.");
                }
            });
        _dictionary.Setup(d => d.AddEntry("Haus", "english", "german"))
            .Callback(() => mainAdded = true)
            .Returns(new MainEntry("Haus", "english", "german"));

        var fixture = Create();
        FillHouse(fixture);

        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ConfirmAsync("T143", "T144"), Times.Once);
        _dictionary.Verify(d => d.AddEntry("Haus", "english", "german"), Times.Once);
        _dictionary.Verify(
            d => d.AddSubEntry(It.IsAny<WordEntry>(), "Haus", "english", "german"), Times.Exactly(2));
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task AddSubEntry_MainEntryMissing_ConfirmNo_Stops()
    {
        _dialogs = Fakes.Dialogs(confirm: false);
        _dictionary.Setup(d => d.AddSubEntry(It.IsAny<WordEntry>(), "Haus", "english", "german"))
            .Throws(new EntryNotFoundException("Main entry missing."));

        var fixture = Create();
        FillHouse(fixture);

        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _dictionary.Verify(d => d.AddEntry(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _dictionary.Verify(
            d => d.AddSubEntry(It.IsAny<WordEntry>(), "Haus", "english", "german"), Times.Once);
    }

    [Test]
    public async Task AddSubEntry_MainEntryCannotBeCreated_ShowsInvalidInputAndConflict()
    {
        var calls = 0;
        _dictionary.Setup(d => d.AddSubEntry(It.IsAny<WordEntry>(), "Haus", "english", "german"))
            .Callback<WordEntry, string, string, string>((entry, _, _, _) =>
            {
                calls++;
                throw calls == 1
                    ? (Exception)new EntryNotFoundException("Main entry missing.")
                    : new InvalidOperationException("Index conflict.");
            });
        _dictionary.Setup(d => d.AddEntry("Haus", "english", "german"))
            .Throws(new LanguageNotFoundException("Language missing."));

        var fixture = Create();
        FillHouse(fixture);

        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync("T119", "Language missing."), Times.Once);
        _dialogs.Verify(d => d.ShowMessageAsync("T104", "T145"), Times.Once);
        _dictionary.Verify(
            d => d.AddSubEntry(It.IsAny<WordEntry>(), "Haus", "english", "german"), Times.Exactly(2));
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
        _dialogs.Verify(d => d.ShowMessageAsync("T118", "T139"), Times.Once);
        _dictionary.Verify(
            d => d.AddSubEntry(It.IsAny<WordEntry>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task AddSubEntry_DirectAddWithoutGroup_DoesNotRequestMainEntrySelection()
    {
        _groups.Setup(g => g.GetGroups()).Returns(new Collection<string>());

        var fixture = Create();
        var requested = 0;
        fixture.SelectMainEntryRequested += () => requested++;
        FillHouse(fixture);
        fixture.DirectAdd = true;

        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        requested.Should().Be(0);
    }

    [Test]
    public async Task AddSubEntry_DirectAdd_EmptyGroup_ConfirmNo_Aborts()
    {
        _dialogs = Fakes.Dialogs(confirm: false);
        _group.Setup(g => g.GetLanguages(It.IsAny<GroupEntry>())).Returns(new List<string>());

        var fixture = Create();
        FillHouse(fixture);
        fixture.DirectAdd = true;

        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ConfirmAsync("T140", "T141"), Times.Once);
        _dictionary.Verify(
            d => d.AddSubEntry(It.IsAny<WordEntry>(), It.IsAny<string>(), It.IsAny<string>(),
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
        _dialogs.Verify(d => d.ConfirmAsync("T140", "T142"), Times.Once);
        _dictionary.Verify(
            d => d.AddSubEntry(It.IsAny<WordEntry>(), It.IsAny<string>(), It.IsAny<string>(),
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
        _group.Setup(g => g.Add(It.IsAny<GroupEntry>(), It.IsAny<WordEntry>(), It.IsAny<bool>(),
                It.IsAny<string>()))
            .Callback<GroupEntry, WordEntry, bool, string>((group, entry, marked, example) =>
            {
                capturedGroup = group;
                capturedWord = entry;
                capturedMarked = marked;
                capturedExample = example;
            });

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
        _dictionary.Setup(d => d.AddSubEntry(It.IsAny<WordEntry>(), "Haus", "english", "german"))
            .Throws(new EntryExistsException("Entry exists."));
        SetUpAddToGroup(out _, out _);
        _group.Setup(g => g.Add(It.IsAny<GroupEntry>(), It.IsAny<WordEntry>(), It.IsAny<bool>(),
            It.IsAny<string>()));

        var fixture = Create();
        FillHouse(fixture);
        fixture.DirectAdd = true;

        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _group.Verify(g => g.Add(It.IsAny<GroupEntry>(), It.IsAny<WordEntry>(), It.IsAny<bool>(),
            It.IsAny<string>()), Times.Once);
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task AddSubEntry_DirectAdd_WordAlreadyInGroup_ShowsMessage()
    {
        SetUpAddSubEntry();
        SetUpAddToGroup(out _, out _);
        _group.Setup(g => g.Add(It.IsAny<GroupEntry>(), It.IsAny<WordEntry>(), It.IsAny<bool>(),
                It.IsAny<string>()))
            .Throws(new EntryExistsException("Entry exists."));

        var fixture = Create();
        FillHouse(fixture);
        fixture.DirectAdd = true;

        await fixture.AddSubEntryCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync("T148", "T149"), Times.Once);
    }

    [Test]
    public void SelectSubGroup_UniqueLanguages_SelectsThem()
    {
        _group.Setup(g => g.GetUniqueLanguage(It.IsAny<GroupEntry>())).Returns("italian");
        Create().SelectedLanguage.Should().Be("italian");
    }

    [Test]
    public async Task SelectSubGroup_MultipleLanguages_ShowsMessage()
    {
        _group.Setup(g => g.GetUniqueLanguage(It.IsAny<GroupEntry>()))
            .Throws(new LanguageException("More than one language."));

        var fixture = Create();
        await fixture.OnOpenedAsync();

        _dialogs.Verify(d => d.ShowMessageAsync("T11", "T146"), Times.Once);
        fixture.SelectedLanguage.Should().Be("english");
    }

    [Test]
    public async Task SelectSubGroup_MultipleMainLanguages_ShowsMessage()
    {
        _group.Setup(g => g.GetUniqueMainLanguage(It.IsAny<GroupEntry>()))
            .Throws(new LanguageException("More than one language."));

        var fixture = Create();
        await fixture.OnOpenedAsync();

        _dialogs.Verify(d => d.ShowMessageAsync("T11", "T147"), Times.Once);
    }

    [Test]
    public void SelectSubGroup_AmbiguousLanguages_QueuesMessageUntilOpened()
    {
        MakeGroupLanguagesAmbiguous();

        var fixture = Create();

        _dialogs.Verify(d => d.ShowMessageAsync("T11", It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task OnOpened_ShowsTheQueuedMessagesOneAfterAnother()
    {
        MakeGroupLanguagesAmbiguous();
        var first = new TaskCompletionSource();
        _dialogs.Setup(d => d.ShowMessageAsync("T11", "T146"))
            .Returns(first.Task);
        var fixture = Create();

        var opened = fixture.OnOpenedAsync();

        _dialogs.Verify(d => d.ShowMessageAsync("T11", "T146"), Times.Once);
        _dialogs.Verify(d => d.ShowMessageAsync("T11", "T147"), Times.Never);

        first.SetResult();
        await opened;

        _dialogs.Verify(d => d.ShowMessageAsync("T11", "T147"), Times.Once);
    }

    [Test]
    public async Task SelectSubGroup_AfterOpened_ShowsTheMessageAtOnce()
    {
        var fixture = Create();
        await fixture.OnOpenedAsync();
        MakeGroupLanguagesAmbiguous();

        fixture.SelectedSubGroup = null;
        fixture.SelectedSubGroup = "Unit 1";

        _dialogs.Verify(d => d.ShowMessageAsync("T11", "T146"), Times.Once);
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
