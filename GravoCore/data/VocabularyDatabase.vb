Imports System.Runtime.CompilerServices

<Assembly: InternalsVisibleTo("GravoApp.Tests")>

''' <summary>
''' Container for the three DAOs of one vocabulary database bundled together.
''' </summary>
''' <remarks>
''' Current implementation supports SQLite vocabulary databases (<c>*.s3db</c>).
''' </remarks>
Public Class VocabularyDatabase
    Private ReadOnly _dictionary As IDictionaryDao
    Private ReadOnly _groups As IGroupsDao
    Private ReadOnly _group As IGroupDao
    Private ReadOnly _connection As IDataBaseOperation

    ''' <summary>
    ''' Initializes a new instance of the vocabulary with a given database connection and DAOs. It is assumed that the
    ''' DAOs are using the given connection. To make sure only the <see cref="Create"/> factory method is used.
    ''' </summary>
    Private Sub New(db As IDataBaseOperation, dictionary As IDictionaryDao, groups As IGroupsDao, group As IGroupDao)
        _dictionary = dictionary
        _groups = groups
        _group = group
        _connection = db
    End Sub

    ''' <summary>
    ''' Creates a new instance of the vocabulary from a database connection and creates the concrete DAOs.
    ''' </summary>
    Public Shared Function Create(db As IDataBaseOperation) As VocabularyDatabase
        Return New VocabularyDatabase(db, New DictionaryDao(db), New GroupsDao(db), New GroupDao(db))
    End Function

    ''' <summary>
    ''' Creates a vocabulary database instance for testing with the provided DAOs. Skips the creation of concrete DAOs and
    ''' therefore not ensures all daos use the same database connection.
    ''' </summary>
    Friend Shared Function CreateForTest(db As IDataBaseOperation, dictionary As IDictionaryDao, groups As IGroupsDao,
        group As IGroupDao) As VocabularyDatabase
        Return New VocabularyDatabase(db, dictionary, groups, group)
    End Function

    ''' <summary>
    ''' The connection the DAOs work on.
    ''' </summary>
    Public ReadOnly Property Connection As IDataBaseOperation
        Get
            Return _connection
        End Get
    End Property

    Public ReadOnly Property Dictionary As IDictionaryDao
        Get
            Return _dictionary
        End Get
    End Property

    Public ReadOnly Property Groups As IGroupsDao
        Get
            Return _groups
        End Get
    End Property

    Public ReadOnly Property Group As IGroupDao
        Get
            Return _group
        End Get
    End Property
End Class
