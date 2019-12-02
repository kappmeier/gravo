Imports System.Collections.ObjectModel
Imports Gravo.AccessDatabaseOperation

Public Class xlsGroup
    Inherits xlsBase

    Dim groupTableName As String

    Sub New(ByVal GroupTable As String)
        MyBase.new()
        groupTableName = GroupTable
    End Sub

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
