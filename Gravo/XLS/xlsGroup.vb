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
