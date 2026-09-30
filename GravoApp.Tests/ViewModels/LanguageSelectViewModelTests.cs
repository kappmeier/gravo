using FluentAssertions;
using Gravo;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using Moq;
using NUnit.Framework;

namespace GravoApp.Tests.ViewModels;

public class LanguageSelectViewModelTests
{
    private static LanguageSelectViewModel Create(Settings settings)
    {
        var dictionary = new Mock<IDictionaryDao>(MockBehavior.Strict);
        dictionary.Setup(d => d.DictionaryLanguages("german")).Returns(new List<string> { "english", "italian" });
        return new LanguageSelectViewModel(dictionary.Object, settings, Fakes.Texts(), AppServices.MainLanguage);
    }

    [Test]
    public void InitializedWith_LanguagesForMainLanguageAndSelectsFirst()
    {
        var fixture = Create(Fakes.DefaultSettings(out _));
        fixture.Languages.Should().Equal("english", "italian");
        fixture.SelectedLanguage.Should().Be("english");
        fixture.TestTargetLanguage.Should().BeTrue();
        fixture.TestPhrases.Should().BeFalse();
        fixture.Title.Should().Be("T" + localization.MAIN_MENU_VOCABULARY_TEST_LANGUAGE);
    }

    [Test]
    public void OkAndCancel_Closes()
    {
        var settings = Fakes.DefaultSettings(out var store);
        var fixture = Create(settings);
        var results = new List<bool>();
        fixture.CloseRequested += results.Add;
        fixture.OkCommand.Execute(null);
        fixture.CancelCommand.Execute(null);
        results.Should().Equal(true, false);
        store.Verify(s => s.Save(It.IsAny<IDictionary<string, string>>()), Times.Never);
    }

    [Test]
    public void QueryLanguage_FollowsCheckbox()
    {
        var fixture = Create(Fakes.DefaultSettings(out _));
        fixture.QueryLanguage.Should().Be(QueryLanguage.TargetLanguage);
        fixture.TestTargetLanguage = false;
        fixture.QueryLanguage.Should().Be(QueryLanguage.OriginalLanguage);
    }
}
