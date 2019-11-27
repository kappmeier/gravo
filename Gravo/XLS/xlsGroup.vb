Imports System.Collections.ObjectModel
Imports Gravo.AccessDatabaseOperation

Public Class xlsGroup
    Inherits xlsBase

    Dim groupTableName As String

    Sub New(ByVal GroupTable As String)
        MyBase.new()
        groupTableName = GroupTable
    End Sub

    ' To DictionaryWords DAO
    Public Function GetWords() As Collection(Of String)
        Dim words As New Collection(Of String)
        Dim command As String = "SELECT D.Word, G.[Index] FROM DictionaryWords AS D, [" & EscapeSingleQuotes(groupTableName) & "] AS G WHERE D.[Index]=G.[WordIndex] ORDER BY G.[Index];"
        Try
            DBConnection.ExecuteReader(command)
        Catch
            Dim e As EntryNotFoundException = New Exception("Es gibt keine Tabelle """ & groupTableName & """")
            Throw e
        End Try
        Do While DBConnection.DBCursor.Read()
            Dim add As String = DBConnection.SecureGetString(0)
            If words.Contains(add) Then
            Else
                words.Add(add)
            End If
        Loop
        DBConnection.DBCursor.Close()
        Return words
    End Function

    ' Use ismarked from groupdto
    Public Function GetMarked(ByVal WordIndex As Integer) As Boolean
        Dim command As String = "SELECT [Marked] FROM [" & groupTableName & "] WHERE [WordIndex]=" & WordIndex & ";"
        DBConnection.ExecuteReader(command)
        If Not DBConnection.DBCursor.HasRows Then Throw New EntryNotFoundException("No Entry with this Index in the Group.")
        ' vorhanden, also auslesen
        DBConnection.DBCursor.Read()
        Dim ret As Boolean = DBConnection.SecureGetBool(0)
        DBConnection.DBCursor.Close()
        Return ret
    End Function

    ' as UpdateMarked in groupdao
    Public Sub SetMarked(ByVal WordIndex As Integer, ByVal Value As Boolean)
        Dim command As String = "SELECT [Marked] FROM [" & groupTableName & "] WHERE [WordIndex]=" & WordIndex & ";"
        DBConnection.ExecuteReader(command)
        If Not DBConnection.DBCursor.HasRows Then Throw New EntryNotFoundException("No Entry with this Index in the Group.")
        ' vorhanden, also auslesen
        DBConnection.DBCursor.Close()
        command = "UPDATE [" & groupTableName & "] SET [Marked]=" & GetDBEntry(Value) & "WHERE [WordIndex]=" & WordIndex & ";"
        DBConnection.ExecuteNonQuery(command)
    End Sub

    ' Hohlt alle wörter, bei denen word = word gilt, die auch in der gruppe sind, als komplette dictionaryentrys
    ' not required any more, implemented as filter on group
    'Public Function GetWords(ByVal word As String) As Collection(Of xlsDictionaryEntry)
    '    Dim dictionaryEntrys As New Collection(Of xlsDictionaryEntry)

    '    Dim command As String = "Select D.[Index] FROM DictionaryWords AS D, [" & EscapeSingleQuotes(groupTableName) & "] AS G WHERE (((D.[Index])=G.[WordIndex]) AND ((D.Word)='" & EscapeSingleQuotes(word) & "'));"
    '    DBConnection.ExecuteReader(command)
    '    If DBConnection.DBCursor.HasRows = False Then Return dictionaryEntrys ' kein wort entspricht den geforderten angaben
    '    Dim indices As New Collection(Of Integer)
    '    Do While DBConnection.DBCursor.Read
    '        indices.Add(DBConnection.SecureGetInt32(0))
    '    Loop
    '    DBConnection.DBCursor.Close()
    '    Dim wCurrent As xlsDictionaryEntry
    '    For Each index As Integer In indices
    '        wCurrent = New xlsDictionaryEntry(DBConnection, index)
    '        dictionaryEntrys.Add(wCurrent)
    '    Next
    '    Return dictionaryEntrys
    'End Function

    Public ReadOnly Property WordCount() As Integer
        Get
            Dim command As String = "SELECT COUNT([Index]) FROM [" & EscapeSingleQuotes(groupTableName) & "];"
            DBConnection.ExecuteReader(command)
            DBConnection.DBCursor.Read()
            Dim ret As Integer = DBConnection.SecureGetInt32(0)
            DBConnection.DBCursor.Close()
            Return ret
        End Get
    End Property

    Public ReadOnly Property LanguageCount() As Integer
        Get
            Dim command As String = "SELECT DISTINCT M.LanguageName FROM DictionaryMain AS M, DictionaryWords AS W, [" & EscapeSingleQuotes(groupTableName) & "] AS G WHERE G.WordIndex = W.[Index] AND W.MainIndex= M.[Index];"
            DBConnection.ExecuteReader(command)
            Dim count As Integer = 0
            Do While DBConnection.DBCursor.Read
                count += 1
            Loop
            DBConnection.DBCursor.Close()
            Return count
        End Get
    End Property

    Public ReadOnly Property MainLanguageCount() As Integer
        Get
            Dim command As String = "SELECT DISTINCT M.MainLanguage FROM DictionaryMain AS M, DictionaryWords AS W, [" & EscapeSingleQuotes(groupTableName) & "] AS G WHERE G.WordIndex = W.[Index] AND W.MainIndex = M.[Index]"
            DBConnection.ExecuteReader(command)
            Dim count As Integer = 0
            Do While DBConnection.DBCursor.Read
                count += 1
            Loop
            DBConnection.DBCursor.Close()
            Return count
        End Get
    End Property

    ' Moved to GroupDao
    Public Function GetLanguages() As Collection(Of String)
        Dim languages As Collection(Of String) = New Collection(Of String)
        Dim command As String = "SELECT DISTINCT LanguageName FROM DictionaryMain AS M, DictionaryWords AS W, [" & EscapeSingleQuotes(groupTableName) & "] AS G WHERE W.MainIndex = M.[Index] AND W.[Index] = G.WordIndex"
        DBConnection.ExecuteReader(command)
        Do While DBConnection.DBCursor.Read
            languages.Add(DBConnection.SecureGetString(0))
        Loop
        DBConnection.DBCursor.Close()
        Return languages
    End Function

    ' Moved to groupdao
    Public Function GetUniqueMainLanguage() As String
        Dim ret As String = ""
        Dim once As Boolean = True
        Dim command As String = "SELECT DISTINCT M.MainLanguage FROM DictionaryMain AS M, DictionaryWords AS W, [" & EscapeSingleQuotes(groupTableName) & "] AS G WHERE G.WordIndex = W.[Index] AND W.MainIndex = M.[Index]"
        DBConnection.ExecuteReader(command)
        Do While DBConnection.DBCursor.Read
            If ret <> "" Then once = False : Exit Do
            ret = DBConnection.SecureGetString(0)
            If ret = "" Then Throw New xlsException("Illegal language found.")
        Loop
        DBConnection.DBCursor.Close()
        If Not once Then Throw New xlsException("More than one language.")
        Return ret
    End Function

    Public Function GetIndices() As Collection(Of Integer)
        If DBConnection Is Nothing Then Throw New xlsException("Datenbank ist nicht verbunden")
        Dim indices As New Collection(Of Integer)
        Dim command As String = "SELECT WordIndex FROM [" & EscapeSingleQuotes(groupTableName) & "];"
        DBConnection.ExecuteReader(command)
        While DBConnection.DBCursor.Read()
            indices.Add(DBConnection.SecureGetInt32(0))
        End While
        DBConnection.CloseReader()
        Return indices
    End Function
End Class
