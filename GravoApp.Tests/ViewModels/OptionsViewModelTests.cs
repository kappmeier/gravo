using FluentAssertions;
using Gravo;
using GravoApp.Localization;
using GravoApp.Services;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using Moq;
using NUnit.Framework;

namespace GravoApp.Tests.ViewModels;

public class OptionsViewModelTests
{
    private static readonly Dictionary<string, string> Stored = new()
    {
        ["TestTargetLanguage"] = "0",
        ["TestSetPhrases"] = "1",
        ["SaveWindowPosition"] = "1",
        ["UseCards"] = "0",
        ["CardsInitialInterval"] = "8",
    };

    private static OptionsViewModel Create(
        Settings settings, Mock<IManagementDao>? management = null, Mock<IDialogService>? dialogs = null) =>
        new(settings, (management ?? new Mock<IManagementDao>()).Object, (dialogs ?? Fakes.Dialogs()).Object,
            Fakes.Texts());

    [Test]
    public void Constructor_ReflectsSettings()
    {
        var fixture = Create(Fakes.DefaultSettings(out _, Stored));
        fixture.TestTargetLanguage.Should().BeFalse();
        fixture.TestSetPhrases.Should().BeTrue();
        fixture.SaveWindowPosition.Should().BeTrue();
        fixture.UseCards.Should().BeFalse();
        fixture.CardsInitialInterval.Should().Be(8);
        fixture.Title.Should().Be("Optionen");
    }

    [Test]
    public void Ok_WritesBackSavesAndCloses()
    {
        var settings = Fakes.DefaultSettings(out var store);
        var fixture = Create(settings);
        bool? closed = null;
        fixture.CloseRequested += ok => closed = ok;
        fixture.TestTargetLanguage = false;
        fixture.TestSetPhrases = true;
        fixture.SaveWindowPosition = true;
        fixture.UseCards = false;
        fixture.CardsInitialInterval = 4;
        fixture.OkCommand.Execute(null);
        settings.QueryLanguage.Should().Be(QueryLanguage.OriginalLanguage);
        settings.TestSetPhrases.Should().BeTrue();
        settings.SaveWindowPosition.Should().BeTrue();
        settings.UseCards.Should().BeFalse();
        settings.CardsInitialInterval.Should().Be(4);
        store.Verify(s => s.Save(It.IsAny<IDictionary<string, string>>()), Times.Once);
        closed.Should().BeTrue();
    }

    [Test]
    public void Cancel_LeavesSettingsAndDoesNotSave()
    {
        var settings = Fakes.DefaultSettings(out var store);
        var fixture = Create(settings);
        bool? closed = null;
        fixture.CloseRequested += ok => closed = ok;
        fixture.TestSetPhrases = true;
        fixture.CancelCommand.Execute(null);
        settings.TestSetPhrases.Should().BeFalse();
        store.Verify(s => s.Save(It.IsAny<IDictionary<string, string>>()), Times.Never);
        closed.Should().BeFalse();
    }

    [TestCase(1, 2)]
    [TestCase(8, 16)]
    [TestCase(1024, 1024)]
    public void IncreaseInterval_DoublesUpTo1024(int start, int expected)
    {
        var fixture = Create(Fakes.DefaultSettings(out _));
        fixture.CardsInitialInterval = start;
        fixture.IncreaseIntervalCommand.Execute(null);
        fixture.CardsInitialInterval.Should().Be(expected);
    }

    [TestCase(8, 4)]
    [TestCase(1, 1)]
    public void DecreaseInterval_HalvesDownTo1(int start, int expected)
    {
        var fixture = Create(Fakes.DefaultSettings(out _));
        fixture.CardsInitialInterval = start;
        fixture.DecreaseIntervalCommand.Execute(null);
        fixture.CardsInitialInterval.Should().Be(expected);
    }

    [Test]
    public async Task CopyCards_CallsManagement()
    {
        var management = new Mock<IManagementDao>(MockBehavior.Strict);
        management.Setup(m => m.CopyGlobalCardsToGroups());
        await Create(Fakes.DefaultSettings(out _), management).CopyCardsCommand.ExecuteAsync(null);
        management.Verify(m => m.CopyGlobalCardsToGroups(), Times.Once);
    }

    [Test]
    public async Task CopyCards_Failure_ShowsMessage()
    {
        var management = new Mock<IManagementDao>();
        management.Setup(m => m.CopyGlobalCardsToGroups()).Throws(new InvalidOperationException("boom"));
        var dialogs = Fakes.Dialogs();
        await Create(Fakes.DefaultSettings(out _), management, dialogs).CopyCardsCommand.ExecuteAsync(null);
        dialogs.Verify(d => d.ShowMessageAsync(Strings.ErrorTitle, "boom"), Times.Once);
    }
}
