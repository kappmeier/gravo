Imports System.Collections.ObjectModel
Imports System.Data.Common
Imports Gravo

Public Class GroupDao
    Implements IGroupDao

    Private ReadOnly DBConnection As IDataBaseOperation

    Sub New(ByRef db As IDataBaseOperation)
        DBConnection = db
    End Sub

    ''' <summary>
    ''' Throws exeption if a word with the given index is already contained in the group.
    ''' </summary>
    ''' <param name="group"></param>
    ''' <param name="data"></param>
    Sub Add(ByRef group As GroupEntry, ByRef word As WordEntry, ByRef marked As Boolean, ByRef example As String) Implements IGroupDao.Add
        ' TODO Exception, falls GroupTable nicht existiert, evtl. update von marked falls schon vorhanden...?

        If Exists(group, word) Then
            Throw New EntryExistsException("Word index " & word.Index & " already exists in " & group.SubGroup)
        End If

        '' Lade alten Wert für Cards aus globaler Karten-Tabelle
        'Dim card As xlsCard = New xlsCard(DBConnection, wordIndex)

        '' einfügen
        'Dim month As String
        'If card.LastDate.Month < 10 Then
        '    month = "0" & card.LastDate.Month
        'Else
        '    month = card.LastDate.Month
        'End If
        'Dim day As String
        'If card.LastDate.Day < 10 Then
        '    day = "0" & card.LastDate.Day
        'Else
        '    day = card.LastDate.Day
        'End If

        Dim dateString As String = "1900-01-01"
        Dim command As String = "INSERT INTO [" & StripSpecialCharacters(group.Table) & "] ([WordIndex], [Marked], [Example], [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain]) VALUES(?, ?, ?, 1, 1, " & GetDBEntry(dateString) & ", 1, 1)"
        Dim parameters = EscapeSingleQuotes(New List(Of Object) From {word.Index, marked, example})
        DBConnection.ExecuteNonQuery(command, parameters)
    End Sub

    Function Load(ByRef group As GroupEntry) As GroupDto Implements IGroupDao.Load
        Dim words As New Collection(Of TestWord)
        Dim command As String = "SELECT D.[Index], D.Word, D.Pre, D.Post, D.WordType, D.Meaning, D.TargetLanguageInfo, D.Irregular, G.[Index], G.Marked, G.Example FROM DictionaryWords AS D, [" & StripSpecialCharacters(group.Table) & "] AS G WHERE D.[Index]=G.[WordIndex] ORDER BY G.[Index]"
        DBConnection.ExecuteReader(command)
        Do While DBConnection.DBCursor.Read()
            Dim wordEntry = Extract(DBConnection)

            Dim marked As String = DBConnection.SecureGetBool(9)
            Dim example As String = DBConnection.SecureGetString(10)

            Dim testword As TestWord = New TestWord(wordEntry, marked, example)
            words.Add(testword)
        Loop
        DBConnection.DBCursor.Close()
        Load = New GroupDto(group, words)
    End Function

    Function Load(ByRef group As GroupEntry, ByRef word As WordEntry) As TestWord Implements IGroupDao.Load
        Dim command As String = "SELECT D.[Index], D.Word, D.Pre, D.Post, D.WordType, D.Meaning, D.TargetLanguageInfo, D.Irregular, G.[Index], G.Marked, G.Example FROM DictionaryWords AS D, [" & StripSpecialCharacters(group.Table) & "] AS G WHERE D.[Index]=G.[WordIndex] AND D.[Index] = ? AND G.[WordIndex] = ?"
        Dim parameters = EscapeSingleQuotes(New List(Of Object) From {word.Index, word.Index})
        DBConnection.ExecuteReader(command, parameters)
        FailIfEmpty(DBConnection, Function() As Exception
                                      Return New EntryNotFoundException("No Entry with this index in the Group.")
                                  End Function)
        DBConnection.DBCursor.Read()
        Dim wordEntry = Extract(DBConnection)

        Dim marked As String = DBConnection.SecureGetBool(9)
        Dim example As String = DBConnection.SecureGetString(10)

        Load = New TestWord(wordEntry, marked, example)
        DBConnection.DBCursor.Close()
    End Function

    ''' <summary>
    ''' Extracts the data entries in the following list. The data is assumed to start at index 0.
    ''' <list type="type">
    '''   <item><description>index</description></item>
    '''   <item><description>word</description></item>
    '''   <item><description>pre</description></item>
    '''   <item><description>post</description></item>
    '''   <item><description>wordType</description></item>
    '''   <item><description>meaning</description></item>
    '''   <item><description>additionalTargetLangInfo</description></item>
    '''   <item><description>irregular</description></item>
    ''' </list>
    ''' </summary>
    ''' <param name="dbConnection"></param>
    ''' <returns></returns>
    Shared Function Extract(dbConnection As IDataBaseOperation) As WordEntry
        Dim index = dbConnection.SecureGetInt32(0)
        Dim word = dbConnection.SecureGetString(1)
        Dim pre = dbConnection.SecureGetString(2)
        Dim post = dbConnection.SecureGetString(3)
        Dim wordType = dbConnection.SecureGetInt32(4)
        Dim meaning = dbConnection.SecureGetString(5)
        Dim additionalTargetLangInfo = dbConnection.SecureGetString(6)
        Dim irregular = dbConnection.SecureGetBool(7)
        Extract = New WordEntry(index, word, pre, post, wordType, meaning, additionalTargetLangInfo, irregular)
    End Function

    Sub UpdateMarked(ByRef group As GroupEntry, ByRef word As TestWord, ByVal marked As Boolean) Implements IGroupDao.UpdateMarked
        Dim command As String = "SELECT [Marked] FROM [" & StripSpecialCharacters(group.Table) & "] WHERE [WordIndex] = ?"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {word.WordIndex}))
        FailIfEmpty(DBConnection, Function() As Exception
                                      Return New EntryNotFoundException("No Entry with this index in the Group.")
                                  End Function)
        DBConnection.DBCursor.Close()
        command = "UPDATE [" & StripSpecialCharacters(group.Table) & "] SET [Marked] = ? WHERE [WordIndex] = ?"
        DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(New List(Of Object) From {marked, word.WordIndex}))
        DBConnection.DBCursor.Close()
    End Sub

    ''' <summary>
    ''' Deletes an entry from a group.
    ''' </summary>
    ''' <param name="group">thre group</param>
    ''' <param name="entry">the entry that is to be deleted</param>
    Public Sub Delete(ByRef group As GroupEntry, ByRef entry As TestWord) Implements IGroupDao.Delete
        If Not Exists(group, entry) Then
            Throw New EntryNotFoundException
        End If

        ' TODO: check consistency regarding marked and comment?
        ' Should also be possible to delete data that is not there actually to re-establish consitency
        Dim command As String = "DELETE FROM [" & StripSpecialCharacters(group.Table) & "] WHERE [WordIndex] = ?"
        Dim parameters = New List(Of String) From {entry.WordIndex}
        DBConnection.ExecuteNonQuery(command, parameters)
    End Sub

    Private Function Exists(ByRef group As GroupEntry, ByRef data As IWordReference) As Boolean
        Dim command As String = "SELECT [WordIndex] FROM [" & StripSpecialCharacters(group.Table) & "] WHERE [WordIndex] = ?"
        DBConnection.ExecuteReader(command, Enumerable.Repeat(CStr(data.WordIndex), 1))
        Exists = DBConnection.DBCursor.HasRows
        DBConnection.DBCursor.Close()
    End Function

    ''' <summary>
    ''' Retrieves the index of a word within a group by its word and meaning
    ''' </summary>
    ''' <param name="group"></param>
    ''' <param name="word"></param>
    ''' <param name="meaning"></param>
    ''' <returns>The index of a word in the group table</returns>
    <Obsolete("This method is deprecated, work on data objects and update.")>
    Public Function GetIndex(ByRef group As GroupEntry, ByVal word As String, ByVal meaning As String) As Integer
        Dim command As String = "SELECT G.WordIndex FROM DictionaryWords AS W, [" & StripSpecialCharacters(group.Table) & "] AS G WHERE G.WordIndex = W.[Index] AND W.Word= ? AND W.Meaning = ?"
        DBConnection.ExecuteReader(command, New List(Of String) From {EscapeSingleQuotes(word), EscapeSingleQuotes(meaning)})
        If Not DBConnection.DBCursor.HasRows Then
            DBConnection.DBCursor.Close()
            Throw New EntryNotFoundException("No Entry for the given word and meaning in the current group.")
        End If
        DBConnection.DBCursor.Read()
        Dim index As Integer = DBConnection.SecureGetInt32(0)
        DBConnection.DBCursor.Close()
        Return index
    End Function

    ''' <summary>
    ''' Retrieves the test word belonging to a group by its word and meaning
    ''' </summary>
    ''' <param name="group"></param>
    ''' <param name="word"></param>
    ''' <param name="meaning"></param>
    ''' <returns>The test word datastructure of a word in the group table</returns>
    <Obsolete("This method is deprecated, work on data objects and update.")>
    Public Function GetTestWord(ByRef group As GroupEntry, ByVal word As String, ByVal meaning As String) As TestWord Implements IGroupDao.GetTestWord

        Dim command As String = "SELECT D.[Index], D.Word, D.Pre, D.Post, D.WordType, D.Meaning, D.TargetLanguageInfo, D.Irregular, G.[Index], G.Marked, G.Example FROM DictionaryWords AS D, [" & StripSpecialCharacters(group.Table) & "] AS G WHERE G.WordIndex = D.[Index] AND D.Word= ? AND D.Meaning = ?"
        DBConnection.ExecuteReader(command, New List(Of String) From {EscapeSingleQuotes(word), EscapeSingleQuotes(meaning)})
        If Not DBConnection.DBCursor.HasRows Then
            DBConnection.DBCursor.Close()
            Throw New EntryNotFoundException("No Entry for the given word and meaning in the current group.")
        End If

        DBConnection.DBCursor.Read()
        Dim wordEntry = Extract(DBConnection)

        Dim index As String = DBConnection.SecureGetInt32(0)
        Dim marked As String = DBConnection.SecureGetBool(9)
        Dim example As String = DBConnection.SecureGetString(10)

        GetTestWord = New TestWord(wordEntry, marked, example)
        DBConnection.DBCursor.Close()
    End Function

    ''' <summary>
    ''' Retrieves the unique language of a group. Throws an exception, if the group's language is not unique.
    ''' </summary>
    ''' <param name="group"></param>
    ''' <returns></returns>
    Public Function GetUniqueLanguage(ByRef group As GroupEntry) As String Implements IGroupDao.GetUniqueLanguage
        Dim ret As String = ""
        Dim once As Boolean = True
        Dim command As String = "SELECT DISTINCT M.LanguageName FROM DictionaryMain AS M, DictionaryWords AS W, [" & StripSpecialCharacters(group.Table) & "] AS G WHERE G.WordIndex = W.[Index] AND W.MainIndex = M.[Index]"
        DBConnection.ExecuteReader(command)
        Do While DBConnection.DBCursor.Read
            If ret <> "" Then once = False : Exit Do
            ret = DBConnection.SecureGetString(0)
        Loop
        DBConnection.DBCursor.Close()
        If Not once Then Throw New LanguageException("More than one language.")
        Return ret
    End Function

    Public Function GetLanguages(ByRef group As GroupEntry) As ICollection(Of String) Implements IGroupDao.GetLanguages
        Dim languages As Collection(Of String) = New Collection(Of String)
        Dim command As String = "SELECT DISTINCT LanguageName FROM DictionaryMain AS M, DictionaryWords AS W, [" & StripSpecialCharacters(group.Table) & "] AS G WHERE W.MainIndex = M.[Index] AND W.[Index] = G.WordIndex"
        DBConnection.ExecuteReader(command)
        Do While DBConnection.DBCursor.Read
            languages.Add(DBConnection.SecureGetString(0))
        Loop
        DBConnection.DBCursor.Close()
        Return languages
    End Function

    Public Function GetUniqueMainLanguage(ByRef group As GroupEntry) As String Implements IGroupDao.GetUniqueMainLanguage
        Dim ret As String = ""
        Dim once As Boolean = True
        Dim command As String = "SELECT DISTINCT M.MainLanguage FROM DictionaryMain AS M, DictionaryWords AS W, [" & StripSpecialCharacters(group.Table) & "] AS G WHERE G.WordIndex = W.[Index] AND W.MainIndex = M.[Index]"
        DBConnection.ExecuteReader(command)
        Do While DBConnection.DBCursor.Read
            If ret <> "" Then once = False : Exit Do
            ret = DBConnection.SecureGetString(0)
        Loop
        DBConnection.DBCursor.Close()
        If Not once Then Throw New LanguageException("More than one language.")
        Return ret
    End Function

    Public Function GetMainLanguages(ByRef group As GroupEntry) As ICollection(Of String) Implements IGroupDao.GetMainLanguages
        Dim languages As Collection(Of String) = New Collection(Of String)
        Dim command As String = "SELECT DISTINCT M.MainLanguage FROM DictionaryMain AS M, DictionaryWords AS W, [" & StripSpecialCharacters(group.Table) & "] AS G WHERE G.WordIndex = W.[Index] AND W.MainIndex = M.[Index]"
        DBConnection.ExecuteReader(command)
        Do While DBConnection.DBCursor.Read
            languages.Add(DBConnection.SecureGetString(0))
        Loop
        DBConnection.DBCursor.Close()
        Return languages
    End Function
End Class
