using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GravoApp.ViewModels.Explorer;

/// <summary>The levels of the vocabulary explorer tree.</summary>
/// <remarks>
/// The dictionary branch consists of the main language, language, initial letter and the main entry. The groups branch
/// consists of group, sub group, and the words of the sub group. A <see cref="Placeholder"/> child marks a node whose
/// children are not yet loaded.
/// </remarks>
public enum NodeKind
{
    Placeholder,
    DictionaryRoot,
    MainLanguage,
    Language,
    Letter,
    MainEntry,
    GroupsRoot,
    Group,
    SubGroup,
    GroupWord,
}

/// <summary>
/// Represents a node of the vocabulary explorer tree whose children can be loaded on the first expand.
/// </summary>
/// <remarks>
/// A node with children is created with one placeholder child, so the tree shows an expander. The first expand raises
/// the <see cref="ExpandRequested"/> event and the handler replaces the placeholder through with actual data
/// (see <see cref="SetChildren"/>).
/// </remarks>
public sealed partial class ExplorerNode : ObservableObject
{
    [ObservableProperty] private string _title = "";
    [ObservableProperty] private bool _isExpanded;

    public ExplorerNode(NodeKind kind, string title, ExplorerNode? parent, bool hasChildren, object? payload = null)
    {
        Kind = kind;
        Title = title;
        Parent = parent;
        Payload = payload;
        if (hasChildren)
        {
            AddPlaceholder();
        }
    }

    public NodeKind Kind { get; }

    public ExplorerNode? Parent { get; }

    /// <summary>The data behind the node.</summary>
    /// <remarks>A group word holds its <c>TestWord</c>, every other node holds <c>null</c>.</remarks>
    public object? Payload { get; }

    public ObservableCollection<ExplorerNode> Children { get; } = new();

    /// <summary>Indicates whether the children are loaded.</summary>
    /// <remarks>The property is <c>false</c> while the placeholder is in place.</remarks>
    public bool IsLoaded { get; private set; }

    /// <summary>Occurs when a node with unloaded children is expanded.</summary>
    public event Action<ExplorerNode>? ExpandRequested;

    public string? MainLanguage => Find(NodeKind.MainLanguage)?.Title;

    public string? Language => Find(NodeKind.Language)?.Title;

    public string? Letter => Find(NodeKind.Letter)?.Title;

    public string? MainEntry => Find(NodeKind.MainEntry)?.Title;

    public string? Group => Find(NodeKind.Group)?.Title;

    public string? SubGroup => Find(NodeKind.SubGroup)?.Title;

    public bool IsDictionaryBranch => Find(NodeKind.DictionaryRoot) is not null;

    public bool IsGroupsBranch => Find(NodeKind.GroupsRoot) is not null;

    /// <summary>Replaces the children, including the placeholder, and marks the node as loaded.</summary>
    public void SetChildren(IEnumerable<ExplorerNode> children)
    {
        Children.Clear();
        foreach (var child in children)
        {
            Children.Add(child);
        }
        IsLoaded = true;
    }

    /// <summary>Drops the loaded children so that they are loaded again.</summary>
    /// <remarks>An expanded node requests the new children right away.</remarks>
    public void Invalidate()
    {
        Children.Clear();
        AddPlaceholder();
        IsLoaded = false;
        if (IsExpanded)
        {
            ExpandRequested?.Invoke(this);
        }
    }

    partial void OnIsExpandedChanged(bool value)
    {
        if (value && !IsLoaded && Children.Count > 0)
        {
            ExpandRequested?.Invoke(this);
        }
    }

    private void AddPlaceholder() => Children.Add(new ExplorerNode(NodeKind.Placeholder, "", this, false));

    /// <summary>Returns the nearest node of the given <paramref name="kind"/> on the path from this node up.</summary>
    private ExplorerNode? Find(NodeKind kind)
    {
        for (var node = this; node is not null; node = node.Parent)
        {
            if (node.Kind == kind)
            {
                return node;
            }
        }
        return null;
    }
}
