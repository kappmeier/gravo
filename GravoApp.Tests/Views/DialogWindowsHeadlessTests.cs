using Avalonia.Headless.NUnit;
using System.Collections.ObjectModel;
using Avalonia.Threading;
using FluentAssertions;
using Gravo;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using GravoApp.Views;
using Moq;
using NUnit.Framework;

namespace GravoApp.Tests.Views;

[TestFixture]
public class DialogWindowsHeadlessTests
{
    [AvaloniaTest]
    public void WordInput_CreatesWordInputWindowWithTitleAndButtonCaptions()
    {
        var dictionary = new Mock<IDictionaryDao>();
        dictionary.Setup(d => d.DictionaryLanguages("german")).Returns(new List<string> { "english" });
        dictionary.Setup(d => d.DictionaryMainLanguages()).Returns(new List<string> { "german" });
        var groups = new Mock<IGroupsDao>();
        groups.Setup(g => g.GetGroups()).Returns(new Collection<string>());
        var vm = new WordInputViewModel(dictionary.Object, groups.Object, new Mock<IGroupDao>().Object,
            Fakes.Properties().Object, Fakes.Texts(), Fakes.Dialogs().Object, AppServices.MainLanguage);
        var fixture = ViewLocator.CreateWindow(vm).Should().BeOfType<WordInputWindow>().Which;
        fixture.Show();
        fixture.Title.Should().Be("T59");
        fixture.AddButton.Content.Should().Be("T82");
        fixture.CloseButton.Content.Should().Be("T84");
        fixture.LanguageBox.SelectedItem.Should().Be("english");
        fixture.WordTypeList.SelectedIndex.Should().Be(0);
        fixture.DirectAddCheck.IsEnabled.Should().BeFalse();
        vm.MainEntry = "Haus";
        Dispatcher.UIThread.RunJobs();
        fixture.WordBox.Text.Should().Be("Haus");
    }

    [AvaloniaTest]
    public void TestSelect_CreatesTestSelectWindowWithLocalizedTexts()
    {
        var groups = new Mock<IGroupsDao>();
        groups.Setup(g => g.GetGroups()).Returns(new Collection<string>());
        var vm = new TestSelectViewModel(
            groups.Object, new Mock<IGroupDao>().Object, Fakes.DefaultSettings(out _), Fakes.Texts());
        var fixture = ViewLocator.CreateWindow(vm).Should().BeOfType<TestSelectWindow>().Which;
        fixture.Show();
        fixture.Title.Should().Be("T108");
        fixture.GroupLabel.Content.Should().Be("T106:");
        fixture.SubGroupLabel.Content.Should().Be("T107:");
        fixture.OkButton.Content.Should().Be("T85");
        fixture.CancelButton.Content.Should().Be("T94");
    }

    [AvaloniaTest]
    public void Quiz_CreatesQuizWindowWithLocalizedTextsAndTheQuestion()
    {
        var cards = new Mock<ICardsDao>();
        var words = new List<WordEntry> { new("house", "", "", WordType.Substantive, "Haus", "n.", false) };
        var data = new TestData(cards.Object, words, QueryLanguage.OriginalLanguage);
        var controller = new TestController(
            data, QueryLanguage.OriginalLanguage, new Mock<IDataBaseOperation>().Object);
        var vm = new QuizViewModel(controller, Fakes.Texts(), Fakes.Dialogs().Object);
        var fixture = ViewLocator.CreateWindow(vm).Should().BeOfType<QuizWindow>().Which;
        fixture.Show();
        vm.StartAsync().Wait();
        Dispatcher.UIThread.RunJobs();
        fixture.Title.Should().Be("T105");
        fixture.InfoLabel.Content.Should().Be("T95");
        fixture.FeedbackLabel.Content.Should().Be("T96");
        fixture.InputLabel.Content.Should().Be("T23");
        fixture.OkButton.Content.Should().Be("T85");
        fixture.CloseButton.Content.Should().Be("T84");
        fixture.QuestionText.Text.Should().Be("Haus");
        fixture.InfoText.Text.Should().Be("n.");
        fixture.CountText.Text.Should().Be("1");
    }

    [AvaloniaTest]
    public void LanguageSelect_CreatesLanguageSelectWindowWithLocalizedTexts()
    {
        var dictionary = new Mock<IDictionaryDao>();
        dictionary.Setup(d => d.DictionaryLanguages("german")).Returns(new List<string> { "english" });
        var vm = new LanguageSelectViewModel(
            dictionary.Object, Fakes.DefaultSettings(out _), Fakes.Texts(), AppServices.MainLanguage);
        var fixture = ViewLocator.CreateWindow(vm).Should().BeOfType<LanguageSelectWindow>().Which;
        fixture.Show();
        fixture.Title.Should().Be("T64");
        fixture.LanguageLabel.Content.Should().Be("T19:");
        fixture.LanguageBox.SelectedItem.Should().Be("english");
        fixture.OkButton.Content.Should().Be("T85");
        fixture.CancelButton.Content.Should().Be("T94");
    }

    [AvaloniaTest]
    public void Options_CreatesOptionsWindowWithTitleAndButtonCaptions()
    {
        var vm = new OptionsViewModel(
            Fakes.DefaultSettings(out _), new Mock<IManagementDao>().Object, Fakes.Dialogs().Object,
            Fakes.Texts());
        var fixture = ViewLocator.CreateWindow(vm).Should().BeOfType<OptionsWindow>().Which;
        fixture.Show();
        fixture.Title.Should().Be("Optionen");
        fixture.OkButton.Content.Should().Be("T85");
        fixture.CancelButton.Content.Should().Be("T94");
    }

    [AvaloniaTest]
    public void Info_CreatesInfoWindowWithTitleAndGermanToggle()
    {
        var management = new Mock<IManagementDao>();
        management.Setup(m => m.LatestVersion)
            .Returns(new Properties.DBVersion(1, 7, new DateTime(2020, 1, 1), ""));
        var fixture = new InfoViewModel(management.Object, Fakes.Texts());
        var window = ViewLocator.CreateWindow(fixture).Should().BeOfType<InfoWindow>().Which;
        window.Show();
        window.Title.Should().Be("Gravo 7 Sprachtrainer info");
        window.CloseButton.Content.Should().Be("T84");
        fixture.German = true;
        Dispatcher.UIThread.RunJobs();
        window.DisclaimerText.Text.Should().StartWith("Das Arbeiten mit dieser Version von Gravo");
    }
}
