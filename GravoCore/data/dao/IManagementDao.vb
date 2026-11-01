Imports Gravo.Properties

Public Interface IManagementDao
    ''' <summary>
    ''' Initializes a database to be a Gravo database.
    ''' </summary>
    Sub Initialize()

    Function IsSchemaVersionUpToDate() As Boolean

    Function IsSchemaUpdateComplex(version As DbSchemaVersion) As Boolean

    ReadOnly Property LatestSchemaVersion() As DbSchemaVersion

    Function GetCurrentSchemaVersion() As DbSchemaVersion

    Function GetNextSchemaVersion() As DbSchemaVersion

    ''' <summary>
    ''' Updates the database from one version to the next.
    ''' </summary>
    ''' <exception cref="Gravo.IllegalVersionException">Thrown if the current version is not known
    ''' or database is up to date.</exception>
    Sub UpdateDatabaseSchemaVersion()

    ''' <summary>
    ''' Check the data base for errors that can be fixed automatically. Do performance improvements, if possible.
    ''' </summary>
    Function Reorganize() As Integer

    ''' <summary>
    ''' Copies the global per-word card statistics from the Cards table into the card columns of every group table.
    ''' </summary>
    Sub CopyGlobalCardsToGroups()

End Interface
