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
    /// An ILocalization mock. GetText(n) returns "T{n}", GetText(name) returns "T:{name}", and the languages are
    /// Deutsch and English.
    /// </summary>
    public static Mock<ILocalization> Localization()
    {
        var m = new Mock<ILocalization>();
        m.SetupProperty(l => l.Language, "german");
        m.Setup(l => l.GetText(It.IsAny<int>())).Returns((int code) => "T" + code);
        m.Setup(l => l.GetText(It.IsAny<string>())).Returns((string name) => "T:" + name);
        m.Setup(l => l.GetLanguageNames()).Returns(new Collection<string> { "Deutsch", "English" });
        return m;
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
        var m = new Mock<IDialogService>();
        m.Setup(d => d.ShowMessageAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
        m.Setup(d => d.ConfirmAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(confirm);
        m.Setup(d => d.ShowDialogAsync(It.IsAny<ViewModelBase>())).ReturnsAsync(true);
        m.Setup(d => d.PickOpenFileAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((string?)null);
        m.Setup(d => d.PickSaveFileAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string?>()))
            .ReturnsAsync((string?)null);
        return m;
    }

    /// <summary>
    /// An <c>IPropertiesDao</c> mock whose <c>LoadWordTypes</c> method returns the enum names.
    /// </summary>
    public static Mock<IPropertiesDao> Properties(Properties? limits = null)
    {
        IDictionary<string, int> codes = Enum.GetValues<WordType>().ToDictionary(t => t.ToString(), t => (int)t);
        IDictionary<string, WordType> found = Enum.GetValues<WordType>().ToDictionary(t => t.ToString(), t => t);
        var m = new Mock<IPropertiesDao>();
        m.Setup(p => p.LoadWordTypes()).Returns(new WordTypes(ref codes, ref found));
        m.Setup(p => p.LoadProperties()).Returns(limits ?? new Properties(new Properties.PropertiesBuilder()));
        return m;
    }
}
