using AwesomeAssertions;
using Gravo;
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
    public void Constructor_ReadsLocalizedTexts()
    {
        var fixture = Create();

        fixture.Title.Should().Be("T219");
        fixture.VersionText.Should().Be("T220");
        fixture.DbVersionText.Should().Be("T221");
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
