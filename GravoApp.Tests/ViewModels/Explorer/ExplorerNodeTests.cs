using FluentAssertions;
using GravoApp.ViewModels.Explorer;
using NUnit.Framework;

namespace GravoApp.Tests.ViewModels.Explorer;

public class ExplorerNodeTests
{
    [Test]
    public void Constructor_WithChildren_HasOnePlaceholder()
    {
        var fixture = new ExplorerNode(NodeKind.Language, "english", null, hasChildren: true);
        fixture.Children.Should().ContainSingle().Which.Kind.Should().Be(NodeKind.Placeholder);
        fixture.Children[0].Parent.Should().BeSameAs(fixture);
        fixture.IsLoaded.Should().BeFalse();
    }

    [Test]
    public void IsExpanded_FirstTime_RaisesExpandRequestedOnce()
    {
        var fixture = new ExplorerNode(NodeKind.Language, "english", null, hasChildren: true);

        var requests = new List<ExplorerNode>();
        fixture.ExpandRequested += node =>
        {
            requests.Add(node);
            node.SetChildren([new ExplorerNode(NodeKind.Letter, "A", node, hasChildren: false)]);
        };
        fixture.IsExpanded = true;
        fixture.IsExpanded = false;
        fixture.IsExpanded = true;
        requests.Should().Equal(fixture);
    }

    [Test]
    public void SetChildren_ReplacesPlaceholderAndMarksLoaded()
    {
        var fixture = new ExplorerNode(NodeKind.Letter, "H", null, hasChildren: true);

        var house = new ExplorerNode(NodeKind.MainEntry, "house", fixture, hasChildren: false);
        fixture.SetChildren([house]);
        fixture.Children.Should().Equal(house);
        fixture.IsLoaded.Should().BeTrue();
    }

    [Test]
    public void Invalidate_Expanded_RestoresPlaceholderAndRequestsReload()
    {
        var fixture = new ExplorerNode(NodeKind.SubGroup, "Unit 1", null, hasChildren: true);

        var requests = 0;
        fixture.ExpandRequested += node =>
        {
            requests++;
            node.SetChildren([new ExplorerNode(NodeKind.GroupWord, "house", node, hasChildren: false)]);
        };
        fixture.IsExpanded = true;
        fixture.Invalidate();
        requests.Should().Be(2);
        fixture.Children.Should().ContainSingle().Which.Title.Should().Be("house");
        fixture.IsLoaded.Should().BeTrue();
    }

    [Test]
    public void PathProperties_WalkAncestors()
    {
        var root = new ExplorerNode(NodeKind.DictionaryRoot, "Dictionary", null, hasChildren: false);
        var german = new ExplorerNode(NodeKind.MainLanguage, "german", root, hasChildren: false);
        var english = new ExplorerNode(NodeKind.Language, "english", german, hasChildren: false);
        var letter = new ExplorerNode(NodeKind.Letter, "H", english, hasChildren: false);

        var fixture = new ExplorerNode(NodeKind.MainEntry, "house", letter, hasChildren: false);

        fixture.MainLanguage.Should().Be("german");
        fixture.Language.Should().Be("english");
        fixture.Letter.Should().Be("H");
        fixture.MainEntry.Should().Be("house");
        fixture.Group.Should().BeNull();
        fixture.SubGroup.Should().BeNull();
        fixture.IsDictionaryBranch.Should().BeTrue();
        fixture.IsGroupsBranch.Should().BeFalse();
    }
}
