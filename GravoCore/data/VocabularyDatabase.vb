Imports System.Runtime.CompilerServices

<Assembly: InternalsVisibleTo("GravoApp.Tests")>

''' <summary>
''' The DAOs of one vocabulary database together with the connection they work on.
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
    ''' Initializes a new instance from a database connection and the DAOs that work on it. The constructor is
    ''' private so that every instance comes from <see cref="Create"/> or <see cref="CreateForTest"/>.
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
    ''' Creates an instance for tests from the given DAOs. It does not check that they use <paramref name="db"/>.
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
