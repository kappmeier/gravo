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

    Public Sub New(dictionary As IDictionaryDao, groups As IGroupsDao, group As IGroupDao)
        _dictionary = dictionary
        _groups = groups
        _group = group
    End Sub

    ''' <summary>
    ''' Creates the concrete DAOs from an open connection.
    ''' </summary>
    Public Sub New(db As IDataBaseOperation)
        Me.New(New DictionaryDao(db), New GroupsDao(db), New GroupDao(db))
    End Sub

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
