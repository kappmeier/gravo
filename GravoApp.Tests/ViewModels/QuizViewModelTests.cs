using FluentAssertions;
using Gravo;
using GravoApp.Services;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using Moq;
using NUnit.Framework;

namespace GravoApp.Tests.ViewModels;

public class QuizViewModelTests
{
    private TempVocabulary _voc = null!;
    private Mock<IDialogService> _dialogs = null!;
    private WordEntry _house = null!;

    [SetUp]
    public void SetUp()
    {
        _voc = new TempVocabulary();
        _house = _voc.AddWord("house", "english", "house", "Haus", info: "n.");
        // A second word with the same meaning, so "home" is another meaning of "Haus".
        _voc.AddWord("house", "english", "home", "Haus");
        _dialogs = Fakes.Dialogs();
    }

    [TearDown]
    public void TearDown() => _voc.Dispose();

    private QuizViewModel Create(QueryLanguage direction, params WordEntry[] words)
    {
        var cards = new Mock<ICardsDao>();
        cards.Setup(c => c.Skip(It.IsAny<WordEntry>(), direction)).Returns(false);
        var data = new TestData(cards.Object, words.ToList(), direction);
        return new QuizViewModel(new TestController(data, direction, _voc.Db), Fakes.Texts(), _dialogs.Object);
    }

    [Test]
    public async Task Start_ShowsQuestionInfoAndCount()
    {
        var fixture = Create(QueryLanguage.OriginalLanguage, _house);
        await fixture.StartAsync();
        fixture.Question.Should().Be("Haus");
        fixture.Info.Should().Be("n.");
        fixture.Feedback.Should().BeEmpty();
        fixture.CountText.Should().Be("1");
        fixture.Title.Should().Be("T105");
    }

    [Test]
    public async Task Start_TargetLanguage_AsksTheForeignWord()
    {
        var fixture = Create(QueryLanguage.TargetLanguage, _house);
        await fixture.StartAsync();
        fixture.Question.Should().Be("house");
    }

    [Test]
    public async Task Start_WithoutWords_ShowsFinishedAndCloses()
    {
        var fixture = Create(QueryLanguage.OriginalLanguage);
        bool? closed = null;
        fixture.CloseRequested += ok => closed = ok;
        await fixture.StartAsync();
        _dialogs.Verify(d => d.ShowMessageAsync("T102", "T101"), Times.Once);
        closed.Should().BeTrue();
        fixture.Question.Should().BeEmpty();
    }

    [Test]
    public async Task Start_GroupOfSeed_OnlyMarked_AsksTheMarkedWord()
    {
        using var voc = new TempVocabulary();
        var seed = voc.SeedStandard();
        var data = TestDataFactory.Create(
            voc.Group, voc.Cards, seed.Unit1, false, true, QueryLanguage.OriginalLanguage);
        var fixture = new QuizViewModel(
            new TestController(data, QueryLanguage.OriginalLanguage, voc.Db), Fakes.Texts(), _dialogs.Object);
        await fixture.StartAsync();
        fixture.Question.Should().Be("Haus");
        fixture.CountText.Should().Be("1");
    }

    [Test]
    public async Task Answer_Correct_OnLastWord_ShowsFinishedAndCloses()
    {
        var fixture = Create(QueryLanguage.OriginalLanguage, _house);
        bool? closed = null;
        fixture.CloseRequested += ok => closed = ok;
        await fixture.StartAsync();
        fixture.Input = " house ";
        await fixture.AnswerCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync("T102", "T101"), Times.Once);
        closed.Should().BeTrue();
    }

    [Test]
    public async Task Answer_Correct_DoublesTheCardIntervalOfTheQueriedDirection()
    {
        var fixture = Create(QueryLanguage.TargetLanguage, _house);
        await fixture.StartAsync();
        fixture.Input = "Haus";
        await fixture.AnswerCommand.ExecuteAsync(null);
        // Interval 2 means the next quiz in this direction skips the word once.
        _voc.Cards.Skip(_house, QueryLanguage.TargetLanguage).Should().BeTrue();
        _voc.Cards.Skip(_house, QueryLanguage.OriginalLanguage).Should().BeFalse();
    }

    [Test]
    public async Task Answer_Correct_ShowsTheNextWord()
    {
        var go = _voc.AddWord("go", "english", "go", "gehen", WordType.Verb);
        var fixture = Create(QueryLanguage.OriginalLanguage, _house, go);
        await fixture.StartAsync();
        fixture.CountText.Should().Be("2");
        fixture.Input = "house";
        await fixture.AnswerCommand.ExecuteAsync(null);
        fixture.Question.Should().Be("gehen");
        fixture.Input.Should().BeEmpty();
        fixture.CountText.Should().Be("1");
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task Answer_Wrong_ShowsHintWithAnswerAndKeepsWord()
    {
        var fixture = Create(QueryLanguage.OriginalLanguage, _house);
        await fixture.StartAsync();
        fixture.Input = "tree";
        await fixture.AnswerCommand.ExecuteAsync(null);
        _dialogs.Verify(d => d.ShowMessageAsync("T104", "T103"), Times.Once);
        fixture.Feedback.Should().Be("T99" + Environment.NewLine + "Haus = house");
        fixture.Input.Should().BeEmpty();
        fixture.Question.Should().Be("Haus");
        fixture.CountText.Should().Be("1");
    }

    [Test]
    public async Task Answer_Misspelled_ShowsTypeErrorWithoutDialog()
    {
        var fixture = Create(QueryLanguage.OriginalLanguage, _house);
        await fixture.StartAsync();
        fixture.Input = "HOUSE";
        await fixture.AnswerCommand.ExecuteAsync(null);
        fixture.Feedback.Should().Be("T100");
        fixture.Input.Should().Be("HOUSE");
        _dialogs.Verify(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task Answer_OtherMeaning_ShowsHintKeepsInputAndSelectsIt()
    {
        var fixture = Create(QueryLanguage.OriginalLanguage, _house);
        var selected = false;
        fixture.SelectInputRequested += () => selected = true;
        await fixture.StartAsync();
        fixture.Input = "home";
        await fixture.AnswerCommand.ExecuteAsync(null);
        fixture.Feedback.Should().Be("T98");
        fixture.Input.Should().Be("home");
        selected.Should().BeTrue();
    }

    [Test]
    public void Close_RequestsCloseWithFalse()
    {
        var fixture = Create(QueryLanguage.OriginalLanguage, _house);
        bool? closed = null;
        fixture.CloseRequested += ok => closed = ok;
        fixture.CloseCommand.Execute(null);
        closed.Should().BeFalse();
    }
}
