using System.Collections.ObjectModel;
using Gravo;
using GravoApp.Localization;
using GravoApp.Services;
using GravoApp.ViewModels;
using Moq;

namespace GravoApp.Tests.Support;

public static class Fakes
{
    /// <summary>
    /// An ILocalization mock. GetText(n) returns "T{n}" and the languages are Deutsch and English.
    /// </summary>
    public static Mock<ILocalization> Localization()
    {
        var localizationMock = new Mock<ILocalization>();
        localizationMock.SetupProperty(l => l.Language, "german");
        localizationMock.Setup(l => l.GetText(It.IsAny<int>())).Returns((int code) => "T" + code);
        localizationMock.Setup(l => l.GetLanguageNames()).Returns(new Collection<string> { "Deutsch", "English" });
        return localizationMock;
    }

    public static UiTexts Texts(Mock<ILocalization>? loc = null) => new((loc ?? Localization()).Object);

    /// <summary>
    /// Mocked settings store. Load returns the given values (or the defaults when <c>null</c>) and Save calls are
    /// recorded on the given <paramref name="store"/>.
    /// </summary>
    public static Settings DefaultSettings(out Mock<ISettingsStore> store, IDictionary<string, string>? values = null)
    {
        store = new Mock<ISettingsStore>();
        store.Setup(s => s.Load()).Returns(values ?? new Dictionary<string, string>());
        var settings = new Settings(store.Object);
        settings.LoadSettings();
        return settings;
    }

    /// <summary>
    /// A simple dialog service mock. Messages succeed, Confirm answers <paramref name="confirm"/>, and the pickers
    /// return <c>null</c>.
    /// </summary>
    public static Mock<IDialogService> Dialogs(bool confirm = true)
    {
        var dialogMock = new Mock<IDialogService>();
        dialogMock.Setup(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
        dialogMock.Setup(d => d.ConfirmAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(confirm);
        dialogMock.Setup(d => d.ShowDialogAsync(It.IsAny<ViewModelBase>())).ReturnsAsync(true);
        dialogMock.Setup(d => d.PickOpenFileAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((string?)null);
        dialogMock.Setup(d => d.PickSaveFileAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string?>()))
            .ReturnsAsync((string?)null);
        return dialogMock;
    }

    /// <summary>
    /// An <c>IPropertiesDao</c> mock whose <c>LoadWordTypes</c> method returns the database keys
    /// (<c>WORD_TYPE_*</c>) as <c>PropertiesDao</c> does.
    /// </summary>
    public static Mock<IPropertiesDao> Properties(Properties? limits = null)
    {
        var keys = new Dictionary<WordType, string>
        {
            [WordType.Substantive] = "WORD_TYPE_SUBSTANTIVE",
            [WordType.Verb] = "WORD_TYPE_VERB",
            [WordType.Adjective] = "WORD_TYPE_ADJECTIVE",
            [WordType.Simple] = "WORD_TYPE_SIMPLE",
            [WordType.Adverb] = "WORD_TYPE_ADVERB",
            [WordType.SetPhrase] = "WORD_TYPE_SET_PHRASE",
            [WordType.Example] = "WORD_TYPE_EXAMPLE",
        };
        IDictionary<string, int> codes = keys.ToDictionary(k => k.Value, k => (int)k.Key);
        IDictionary<string, WordType> found = keys.ToDictionary(k => k.Value, k => k.Key);
        var propertiesMock = new Mock<IPropertiesDao>();
        propertiesMock.Setup(p => p.LoadWordTypes()).Returns(new WordTypes(codes, found));
        propertiesMock.Setup(p => p.LoadProperties())
            .Returns(limits ?? new Properties(new Properties.PropertiesBuilder()));
        return propertiesMock;
    }
}
