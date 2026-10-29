using System.IO;
using Gravo;
using GravoApp.Localization;
using Microsoft.Data.Sqlite;

namespace GravoApp.Tests.Support;

/// <summary>
/// Allows using real German localized texts in tests. Uses a temporary copy of the real <c>languages.s3db</c>.
/// </summary>
public sealed class RealLocalization : IDisposable
{
    private readonly string _path;
    private readonly IDataBaseOperation _db;

    public RealLocalization()
    {
        _path = Path.GetTempFileName();
        File.Copy(Path.Combine(AppContext.BaseDirectory, "languages.s3db"), _path, true);
        _db = SQLiteDataBaseOperation.OpenDatabase(_path);
        Texts = new UiTexts(new localization(_db));
    }

    public UiTexts Texts { get; }

    public void Dispose()
    {
        _db.Dispose();
        SqliteConnection.ClearAllPools();
        File.Delete(_path);
    }
}
