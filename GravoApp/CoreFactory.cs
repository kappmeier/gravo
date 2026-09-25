using Gravo;

namespace GravoApp;

/// <summary>
/// Single factory to generate the <c>ref</c> passed to VB DAO constructors ByRef connection parameters.
/// </summary>
public static class CoreFactory
{
    public static IDictionaryDao Dictionary(IDataBaseOperation db) => new DictionaryDao(ref db);
    public static IGroupsDao Groups(IDataBaseOperation db) => new GroupsDao(ref db);
    public static IGroupDao Group(IDataBaseOperation db) => new GroupDao(ref db);
    public static ICardsDao Cards(IDataBaseOperation db) => new CardsDao(ref db);
    public static IManagementDao Management(IDataBaseOperation db) => new ManagementDao(ref db);
    public static IPropertiesDao Properties(IDataBaseOperation db) => new PropertiesDao(ref db);
}
