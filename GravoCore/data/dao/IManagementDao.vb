Imports Gravo.Properties

Public Interface IManagementDao
    ''' <summary>
    ''' Initializes a database to be a Gravo database.
    ''' </summary>
    Sub Initialize()

    Function IsVersionUpToDate() As Boolean

    Function IsUpdateComplex(version As DBVersion) As Boolean

    ReadOnly Property LatestVersion() As DBVersion

    Function GetCurrentVersion() As DBVersion

    Function GetNextVersion() As DBVersion

    ''' <summary>
    ''' Updates the database from one version to the next.
    ''' </summary>
    ''' <exception cref="Gravo.IllegalVersionException">Thrown if the current version is not known
    ''' or database is up to date.</exception>
    Sub UpdateDatabaseVersion()

    ''' <summary>
    ''' Check the data base for errors that can be fixed automatically. Do performance improvements, if possible.
    ''' </summary>
    Function Reorganize() As Integer

End Interface
