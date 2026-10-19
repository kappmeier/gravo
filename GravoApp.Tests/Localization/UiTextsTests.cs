using System.IO;
using AwesomeAssertions;
using Gravo;
using GravoApp.Localization;
using GravoApp.Tests.Support;
using Microsoft.Data.Sqlite;
using NUnit.Framework;

namespace GravoApp.Tests.Localization;

public class UiTextsTests
{
    [Test]
    public void Indexer_ByConstantName_ReadsTheConstantsCode()
    {
        var texts = Fakes.Texts();
        texts["MAIN_MENU_FILE"].Should().Be("T" + localization.MAIN_MENU_FILE);
        texts["EXPLORER_HEADLINE_WORD_TYPE"].Should().Be("T26");
    }

    [Test]
    public void Indexer_UnknownName_ReturnsTheName()
    {
        Fakes.Texts()["NO_SUCH_CONSTANT"].Should().Be("NO_SUCH_CONSTANT");
    }

    [Test]
    public void Get_ByCode_ReadsLocalization()
    {
        Fakes.Texts().Get(localization.HINT).Should().Be("T11");
    }

    [Test]
    public void Indexer_MapsWinFormsMnemonicsToAccessKeys()
    {
        var loc = Fakes.Localization();
        loc.Setup(l => l.GetText(localization.MAIN_MENU_FILE)).Returns("&Datei");
        loc.Setup(l => l.GetText(localization.HINT)).Returns("A && B");
        loc.Setup(l => l.GetText(localization.DISCLAIMER_1)).Returns("x_y");
        var texts = Fakes.Texts(loc);
        texts["MAIN_MENU_FILE"].Should().Be("_Datei");
        texts["HINT"].Should().Be("A & B");
        texts["DISCLAIMER_1"].Should().Be("x__y");
    }

    [Test]
    public void Get_RemovesWinFormsMnemonics()
    {
        var loc = Fakes.Localization();
        loc.Setup(l => l.GetText(localization.MAIN_MENU_FILE_SAVE_AS)).Returns("Speichern &unter ...");
        loc.Setup(l => l.GetText(localization.HINT)).Returns("A && B");
        loc.Setup(l => l.GetText(localization.DISCLAIMER_1)).Returns("x_y");
        var texts = Fakes.Texts(loc);
        texts.Get(localization.MAIN_MENU_FILE_SAVE_AS).Should().Be("Speichern unter ...");
        texts.Get(localization.HINT).Should().Be("A & B");
        texts.Get(localization.DISCLAIMER_1).Should().Be("x_y");
    }

    [Test]
    public void Refresh_RaisesIndexerChanged()
    {
        var texts = Fakes.Texts();
        var names = new List<string?>();
        texts.PropertyChanged += (_, e) => names.Add(e.PropertyName);
        texts.Refresh();
        names.Should().Contain("Item[]").And.Contain("");
    }

    [Test]
    public void Indexer_OverRealLanguagesDatabase_ReturnsGermanText()
    {
        var temp = Path.GetTempFileName();
        File.Copy(Path.Combine(AppContext.BaseDirectory, "languages.s3db"), temp, true);
        var db = SQLiteDataBaseOperation.OpenDatabase(temp);
        try
        {
            var texts = new UiTexts(new localization(db));
            texts["MAIN_MENU_FILE"].Should().Be("_Datei");
        }
        finally
        {
            db.Close();
            SqliteConnection.ClearAllPools();
            File.Delete(temp);
        }
    }

    [TestCase(WordType.Substantive, "T3")]
    [TestCase(WordType.Verb, "T4")]
    [TestCase(WordType.Adjective, "T5")]
    [TestCase(WordType.Simple, "T6")]
    [TestCase(WordType.Adverb, "T7")]
    [TestCase(WordType.SetPhrase, "T8")]
    [TestCase(WordType.Example, "T9")]
    public void DisplayWordType_KnownType_UsesWordTypeCode(WordType type, string expected) =>
        Fakes.Texts().DisplayWordType(type).Should().Be(expected);

    [Test]
    public void DisplayWordType_UnknownType_ReturnsEmptyText() =>
        Fakes.Texts().DisplayWordType((WordType)42).Should().BeEmpty();
}
