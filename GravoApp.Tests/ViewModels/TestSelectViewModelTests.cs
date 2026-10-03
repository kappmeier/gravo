using System.Collections.ObjectModel;
using FluentAssertions;
using Gravo;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using Moq;
using NUnit.Framework;

namespace GravoApp.Tests.ViewModels;

public class TestSelectViewModelTests
{
    private readonly GroupEntry _unit1 = new(1, "Language Book", "Unit 1", "GroupLanguageBook01");
    private readonly GroupEntry _unit2 = new(2, "Language Book", "Unit 2", "GroupLanguageBook02");
    private readonly GroupEntry _words = new(3, "Other Group", "Some Words", "GroupOtherGroup01");

    private Mock<IGroupsDao> _groups = null!;
    private Mock<IGroupDao> _group = null!;

    [SetUp]
    public void SetUp()
    {
        _groups = new Mock<IGroupsDao>(MockBehavior.Strict);
        _groups.Setup(g => g.GetGroups()).Returns(new Collection<string> { "Language Book", "Other Group" });
        _groups.Setup(g => g.GetSubGroups("Language Book")).Returns(new List<GroupEntry> { _unit1, _unit2 });
        _groups.Setup(g => g.GetSubGroups("Other Group")).Returns(new List<GroupEntry> { _words });
        _group = new Mock<IGroupDao>(MockBehavior.Strict);
        SetupGroup(_unit1, 2);
        SetupGroup(_unit2, 1);
        SetupGroup(_words, 0);
    }

    private void SetupGroup(GroupEntry entry, int wordCount)
    {
        _groups.Setup(g => g.GetGroup(entry.Name, entry.SubGroup)).Returns(entry);
        var words = Enumerable.Range(0, wordCount)
            .Select(i => new WordEntry("word" + i, "", "", WordType.Substantive, "", "", false))
            .Select(word => new TestWord(word, false, ""))
            .ToList();
        var match = entry;
        _group.Setup(g => g.Load(ref match)).Returns(new GroupDto(entry, words));
    }

    private TestSelectViewModel Create(Settings settings) =>
        new(_groups.Object, _group.Object, settings, Fakes.Texts());

    [Test]
    public void InitializedWith_FirstGroupSelectedAndSubGroupCounts()
    {
        var fixture = Create(Fakes.DefaultSettings(out _));
        fixture.Groups.Should().Equal("Language Book", "Other Group");
        fixture.SubGroups.Should().Equal("Unit 1", "Unit 2");
        fixture.SelectedGroup.Should().Be("Language Book");
        fixture.SelectedSubGroup.Should().Be("Unit 1");
        fixture.SelectedGroupEntry.Should().BeSameAs(_unit1);
        fixture.WordCountText.Should().Be("2 Vokabeln abzufragen.");
    }

    [Test]
    public void InitializedWith_LastGroupAndSubGroupFromSettingsPreselected()
    {
        var settings = Fakes.DefaultSettings(out _, new Dictionary<string, string>
        {
            ["LastGroup"] = "Other Group",
            ["LastSubGroup"] = "Some Words",
        });
        var fixture = Create(settings);
        fixture.SelectedGroup.Should().Be("Other Group");
        fixture.SelectedSubGroup.Should().Be("Some Words");
        fixture.SelectedGroupEntry.Should().BeSameAs(_words);
        fixture.WordCountText.Should().Be("0 Vokabeln abzufragen.");
    }

    [Test]
    public void InitializedWith_UnknownLastGroup_FallsBackToFirst()
    {
        var settings = Fakes.DefaultSettings(out _, new Dictionary<string, string>
        {
            ["LastGroup"] = "Missing Group",
            ["LastSubGroup"] = "Unit 2",
        });
        var fixture = Create(settings);
        fixture.SelectedGroup.Should().Be("Language Book");
        fixture.SelectedSubGroup.Should().Be("Unit 2");
        fixture.WordCountText.Should().Be("1 Vokabel abzufragen.");
    }

    [Test]
    public void InitializedWith_DirectionAndPhrasesFromSettings()
    {
        var fixture = Create(Fakes.DefaultSettings(out _));
        fixture.TestTargetLanguage.Should().BeTrue();
        fixture.TestPhrases.Should().BeFalse();
        fixture.TestMarked.Should().BeFalse();
    }

    [Test]
    public void SelectGroup_ReloadsSubGroupsAndSelectsFirst()
    {
        var fixture = Create(Fakes.DefaultSettings(out _));
        fixture.SelectedGroup = "Other Group";
        fixture.SubGroups.Should().Equal("Some Words");
        fixture.SelectedSubGroup.Should().Be("Some Words");
        fixture.WordCountText.Should().Be("0 Vokabeln abzufragen.");
    }

    [Test]
    public void SelectSubGroup_WithOneWord_UsesSingular()
    {
        var fixture = Create(Fakes.DefaultSettings(out _));
        fixture.SelectedSubGroup = "Unit 2";
        fixture.SelectedGroupEntry.Should().BeSameAs(_unit2);
        fixture.WordCountText.Should().Be("1 Vokabel abzufragen.");
    }


    [Test]
    public void QueryLanguage_FollowsCheckbox()
    {
        var fixture = Create(Fakes.DefaultSettings(out _));
        fixture.QueryLanguage.Should().Be(QueryLanguage.TargetLanguage);
        fixture.TestTargetLanguage = false;
        fixture.QueryLanguage.Should().Be(QueryLanguage.OriginalLanguage);
    }

    [Test]
    public void Ok_SavesLastGroupAndCloses()
    {
        var settings = Fakes.DefaultSettings(out var store);
        var fixture = Create(settings);
        bool? closed = null;
        fixture.CloseRequested += ok => closed = ok;
        fixture.OkCommand.Execute(null);
        settings.LastGroup.Should().Be("Language Book");
        settings.LastSubGroup.Should().Be("Unit 1");
        store.Verify(s => s.Save(It.IsAny<IDictionary<string, string>>()), Times.Once);
        closed.Should().BeTrue();
    }

    [Test]
    public void Cancel_DoesNotSave()
    {
        var settings = Fakes.DefaultSettings(out var store);
        var fixture = Create(settings);
        bool? closed = null;
        fixture.CloseRequested += ok => closed = ok;
        fixture.CancelCommand.Execute(null);
        settings.LastGroup.Should().Be("");
        store.Verify(s => s.Save(It.IsAny<IDictionary<string, string>>()), Times.Never);
        closed.Should().BeFalse();
    }

    [Test]
    public void Title_IsLocalized()
    {
        Create(Fakes.DefaultSettings(out _)).Title.Should().Be("T" + localization.TEST_SELECT_TITLE);
    }
}
