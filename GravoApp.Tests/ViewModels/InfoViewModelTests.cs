using FluentAssertions;
using Gravo;
using GravoApp.Localization;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using Moq;
using NUnit.Framework;

namespace GravoApp.Tests.ViewModels;

public class InfoViewModelTests
{
    private static readonly Properties.DBVersion DefaultVersion =
        new(1, 7, new DateTime(2020, 1, 1), "");

    private static InfoViewModel Create()
    {
        var management = new Mock<IManagementDao>();
        management.Setup(m => m.LatestVersion).Returns(DefaultVersion);
        return new InfoViewModel(management.Object, Fakes.Texts());
    }

    [Test]
    public void Constructor_FormatsDbVersionFromLatestVersion()
    {
        var management = new Mock<IManagementDao>();
        management.Setup(m => m.LatestVersion)
            .Returns(new Properties.DBVersion(1, 7, new DateTime(2020, 1, 1), ""));
        var fixture = new InfoViewModel(management.Object, Fakes.Texts());
        fixture.DbVersionText.Should().Be("DB-Version: 1.7");
    }

    [Test]
    public void German_Toggle_SwapsDisclaimerAndCopyrightOld()
    {
        var fixture = Create();
        fixture.Disclaimer.Should().Be(Strings.InfoDisclaimerEnglish);
        fixture.CopyrightOld.Should().Be(Strings.InfoCopyrightOldEnglish);

        fixture.German = true;

        fixture.Disclaimer.Should().Be(Strings.InfoDisclaimerGerman);
        fixture.CopyrightOld.Should().Be(Strings.InfoCopyrightOldGerman);

        fixture.German = false;

        fixture.Disclaimer.Should().Be(Strings.InfoDisclaimerEnglish);
        fixture.CopyrightOld.Should().Be(Strings.InfoCopyrightOldEnglish);
    }

    [Test]
    public void Title_IsProductTitle()
    {
        var fixture = Create();
        fixture.Title.Should().Be("Gravo 7 Sprachtrainer info");
    }

    [Test]
    public void Close_RequestsClose()
    {
        var fixture = Create();
        bool? closed = null;
        fixture.CloseRequested += ok => closed = ok;
        fixture.CloseCommand.Execute(null);
        closed.Should().NotBeNull();
    }
}
