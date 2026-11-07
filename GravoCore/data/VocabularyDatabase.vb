Imports System.Runtime.CompilerServices

<Assembly: InternalsVisibleTo("GravoApp.Tests")>

''' <summary>
''' The DAOs of one vocabulary database together with the connection they work on. This class provides method to update
''' the vocabulary in the dictionary and groups in a consistent way.
''' </summary>
''' <remarks>
''' Current implementation supports SQLite vocabulary databases (<c>*.s3db</c>).
''' All implementations use transactions to not leave a brokoen state.
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

    ''' <summary>
    ''' Changes the fields of a word, its marked flag in a group and its main entry in one transaction. Nothing is
    ''' changed when one of the steps fails.
    ''' </summary>
    ''' <param name="entry">The word to change.</param>
    ''' <param name="data">The new field values.</param>
    ''' <param name="mainEntry">
    ''' The new main entry, created when it does not exist. <c>Nothing</c> or the current main entry keeps it.
    ''' </param>
    ''' <param name="language">The language of the new main entry. Only read when the main entry changes.</param>
    ''' <param name="mainLanguage">
    ''' The main language of the new main entry. Only read when the main entry changes.
    ''' </param>
    ''' <param name="group">The group whose marked flag is set. <c>Nothing</c> keeps every flag.</param>
    ''' <param name="marked">The new marked flag in <paramref name="group"/>.</param>
    ''' <returns>The changed word.</returns>
    ''' <exception cref="InputException">
    ''' When the word is empty after trimming or a text field holds a forbidden character.
    ''' </exception>
    ''' <exception cref="EntryExistsException">
    ''' When the current or the new main entry has a word with the new word and meaning already.
    ''' </exception>
    Public Function ChangeWord(entry As WordEntry, data As IDictionaryDao.UpdateData, mainEntry As String,
        language As String, mainLanguage As String, group As GroupEntry, marked As Boolean) As WordEntry
        Dim result As WordEntry = Nothing
        Connection.InTransaction(
            Sub() result = ChangeWordSteps(entry, data, mainEntry, language, mainLanguage, group, marked))
        Return result
    End Function

    Private Function ChangeWordSteps(entry As WordEntry, data As IDictionaryDao.UpdateData, mainEntry As String,
        language As String, mainLanguage As String, group As GroupEntry, marked As Boolean) As WordEntry
        Dim updatedWord As WordEntry = Dictionary.ChangeEntry(entry, data)
        If group IsNot Nothing Then
            Me.Group.UpdateMarked(group, Me.Group.Load(group).GetWord(updatedWord.WordIndex), marked)
        End If
        If mainEntry IsNot Nothing AndAlso mainEntry <> Dictionary.GetMainEntry(entry).Word Then
            Dictionary.ChangeEntry(updatedWord,
                DataTools.GetOrCreateMainEntry(Dictionary, mainEntry, language, mainLanguage))
        End If
        Return updatedWord
    End Function
End Class
