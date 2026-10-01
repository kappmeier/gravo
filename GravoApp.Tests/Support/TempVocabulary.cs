using System.IO;
using Gravo;
using Microsoft.Data.Sqlite;

namespace GravoApp.Tests.Support;

/// <summary>A fresh vocabulary database in a temp file with the real DAOs and seed helpers.</summary>
/// <remarks>
/// The database is created by <c>ManagementDao.CreateNewVocabularyDatabase</c>, the main language is
/// <see cref="MainLanguage"/>. <see cref="Dispose"/> closes the connection, clears the SQLite pools and deletes the
/// file. <c>IPropertiesDao.LoadWordTypes</c> throws on such a database, so tests mock it.
/// </remarks>
public sealed class TempVocabulary : IDisposable
{
    public const string MainLanguage = "german";

    public TempVocabulary()
    {
        FilePath = Path.GetTempFileName();
        ManagementDao.CreateNewVocabularyDatabase(FilePath);
        IDataBaseOperation db = new SQLiteDataBaseOperation();
        db.Open(FilePath);
        Db = db;
        Dictionary = CoreFactory.Dictionary(db);
        Groups = CoreFactory.Groups(db);
        Group = CoreFactory.Group(db);
        Cards = CoreFactory.Cards(db);
        Management = CoreFactory.Management(db);
        Properties = CoreFactory.Properties(db);
        Vocabulary = new VocabularyDatabase(Dictionary, Groups, Group);
    }

    public string FilePath { get; }
    public IDataBaseOperation Db { get; }
    public IDictionaryDao Dictionary { get; }
    public IGroupsDao Groups { get; }
    public IGroupDao Group { get; }
    public ICardsDao Cards { get; }
    public IManagementDao Management { get; }
    public IPropertiesDao Properties { get; }
    public VocabularyDatabase Vocabulary { get; }

    /// <summary>
    /// Adds a word below the main entry <paramref name="mainEntry"/> of <paramref name="language"/>.
    /// </summary>
    /// <remarks>
    /// The main entry is created automatically when it is missing. The DAO also creates the card of the word.
    /// </remarks>
    /// <returns>The added word with its database index.</returns>
    public WordEntry AddWord(string mainEntry, string language, string word, string meaning,
        WordType type = WordType.Substantive, string pre = "", string post = "", string info = "",
        bool irregular = false)
    {
        try
        {
            Dictionary.AddEntry(mainEntry, language, MainLanguage);
        }
        catch (EntryExistsException)
        {
            // The main entry is shared by several words.
        }
        var entry = new WordEntry(word, pre, post, type, meaning, info, irregular);
        Dictionary.AddSubEntry(ref entry, mainEntry, language, MainLanguage);
        return entry;
    }

    /// <summary>Adds the sub group <paramref name="unit"/> to the group <paramref name="name"/>.</summary>
    /// <returns>The stored group entry.</returns>
    public GroupEntry AddGroup(string name, string unit)
    {
        Groups.AddGroup(name, unit);
        return Groups.GetGroup(name, unit);
    }

    /// <summary>Adds a <paramref name="word"/> to a <paramref name="group"/>.</summary>
    public void AddToGroup(GroupEntry group, WordEntry word, bool marked = false, string example = "") =>
        Group.Add(ref group, ref word, ref marked, ref example);

    /// <summary>Fills the database with a small standard data set.</summary>
    /// <remarks>
    /// Words in english:
    /// - <c>house</c> with the words house/Haus and houses/Häuser,
    /// - <c>go</c> with go/gehen (verb, irregular).
    /// Groups: Book/Unit 1 holds house (marked) and go, Book/Unit 2 is empty
    /// Words in french:
    /// - <c>maison</c> with la maison/Haus.
    /// Groups: Other/Words holds maison.
    /// </remarks>
    /// <returns>The added words and groups.</returns>
    public Seed SeedStandard()
    {
        var house = AddWord("house", "english", "house", "Haus");
        var houses = AddWord("house", "english", "houses", "Häuser");
        var go = AddWord("go", "english", "go", "gehen", WordType.Verb, irregular: true);
        var maison = AddWord("maison", "french", "maison", "Haus", pre: "la");
        var unit1 = AddGroup("Book", "Unit 1");
        var unit2 = AddGroup("Book", "Unit 2");
        var words = AddGroup("Other", "Words");
        AddToGroup(unit1, house, marked: true);
        AddToGroup(unit1, go);
        AddToGroup(words, maison);
        return new Seed(house, houses, go, maison, unit1, unit2, words);
    }

    /// <summary>The words and groups added by <see cref="SeedStandard"/>.</summary>
    public sealed record Seed(
        WordEntry House, WordEntry Houses, WordEntry Go, WordEntry Maison,
        GroupEntry Unit1, GroupEntry Unit2, GroupEntry Words);

    public void Dispose()
    {
        Db.Close();
        SqliteConnection.ClearAllPools();
        File.Delete(FilePath);
    }
}
