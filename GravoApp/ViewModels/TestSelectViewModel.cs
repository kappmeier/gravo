using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gravo;
using GravoApp.Localization;

namespace GravoApp.ViewModels;

/// <summary>Behavior and model for the quiz group and sub group selection dialog.</summary>
/// <remarks>
/// The dialog preselects the group and sub group of the last quiz. If the remembered group is missing, the first
/// group is selected and the remembered sub group is looked up there. OK stores the selection in the settings.
/// TODO: storing selection in the data instead settings, as they are user specific.
/// </remarks>
public sealed partial class TestSelectViewModel : ViewModelBase
{
    private readonly IGroupsDao _groups;
    private readonly IGroupDao _group;
    private readonly Settings _settings;

    [ObservableProperty] private string? _selectedGroup;
    [ObservableProperty] private string? _selectedSubGroup;
    [ObservableProperty] private string _wordCountText = "";
    [ObservableProperty] private bool _testTargetLanguage;
    [ObservableProperty] private bool _testMarked;
    [ObservableProperty] private bool _testPhrases;

    public TestSelectViewModel(IGroupsDao groups, IGroupDao group, Settings settings, UiTexts texts)
    {
        _groups = groups;
        _group = group;
        _settings = settings;
        Texts = texts;
        Title = texts.Get(localization.TEST_SELECT_TITLE);
        TestTargetLanguage = settings.QueryLanguage == QueryLanguage.TargetLanguage;
        TestPhrases = settings.TestSetPhrases;
        foreach (var name in groups.GetGroups())
        {
            Groups.Add(name);
        }

        SelectedGroup = Groups.Contains(settings.LastGroup) ? settings.LastGroup : Groups.FirstOrDefault();
        if (SubGroups.Contains(settings.LastSubGroup))
        {
            SelectedSubGroup = settings.LastSubGroup;
        }
    }

    public UiTexts Texts { get; }

    public ObservableCollection<string> Groups { get; } = new();

    public ObservableCollection<string> SubGroups { get; } = new();

    /// <summary>The group entry of the selected sub group, or <c>null</c> if no sub group is selected.</summary>
    public GroupEntry? SelectedGroupEntry { get; private set; }

    /// <summary>The direction of the quiz as chosen by the options checkbox.</summary>
    public QueryLanguage QueryLanguage =>
        TestTargetLanguage ? QueryLanguage.TargetLanguage : QueryLanguage.OriginalLanguage;

    /// <summary>Fills the available sub groups when a new group is selected.</summary>
    partial void OnSelectedGroupChanged(string? value)
    {
        SelectedSubGroup = null;
        SubGroups.Clear();
        if (value is null)
        {
            return;
        }

        foreach (var entry in _groups.GetSubGroups(value))
        {
            SubGroups.Add(entry.SubGroup);
        }

        SelectedSubGroup = SubGroups.FirstOrDefault();
    }

    /// <summary>
    /// Selects a group entry when a new sub group is chosen (<see cref="SelectedGroupEntry"/>) and updates the word
    /// count text accordingly.
    /// </summary>
    /// <param name="value">The newly selected sub group.</param>
    /// <remarks>Loads the full group to determine the word count.</remarks>
    partial void OnSelectedSubGroupChanged(string? value)
    {
        if (value is null || SelectedGroup is null)
        {
            SelectedGroupEntry = null;
            WordCountText = "";
            return;
        }

        var entry = _groups.GetGroup(SelectedGroup, value);
        SelectedGroupEntry = entry;
        WordCountText = Strings.WordsToTest(_group.Load(ref entry).WordCount);
    }

    [RelayCommand]
    private void Ok()
    {
        _settings.LastGroup = SelectedGroup;
        _settings.LastSubGroup = SelectedSubGroup;
        _settings.SaveSettings();
        RequestClose(true);
    }

    [RelayCommand]
    private void Cancel() => RequestClose(false);
}
