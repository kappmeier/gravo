Imports System.Data.Common
Imports System.Data.SQLite

Public Class SQLiteDataBaseOperation
    Implements DataBaseOperation

    Dim connection As New SQLite.SQLiteConnection()
    Dim connected As Boolean = False
    Dim SQLreader As SQLiteDataReader

    Public Function Open(DBPath As String) As Boolean Implements DataBaseOperation.Open
        If connected Then Close()
        connection.ConnectionString = "Data Source=" & DBPath & ";"
        connection.Open()
        connected = True
    End Function

    Public Function Close() As Boolean Implements DataBaseOperation.Close
        connection.Close()
        connected = False
    End Function

    Public Function ExecuteNonQuery(CommandText As String) As Boolean Implements DataBaseOperation.ExecuteNonQuery
        If Not connected Then Return False
        If Not SQLreader Is Nothing Then SQLreader.Close()
        Dim command As SQLiteCommand
        command = connection.CreateCommand
        command.CommandText = CommandText
        command.ExecuteNonQuery()
        command.Dispose()
        Return True
    End Function

    Public Function ExecuteReader(CommandText As String) As DbDataReader Implements DataBaseOperation.ExecuteReader
        Dim SQLcommand As SQLiteCommand
        If Not SQLreader Is Nothing Then SQLreader.Close()
        SQLcommand = connection.CreateCommand
        SQLcommand.CommandText = CommandText

        SQLreader = SQLcommand.ExecuteReader()
        Return SQLreader
    End Function

    Public Function DBCursor() As DbDataReader Implements DataBaseOperation.DBCursor
        Return SQLreader
    End Function

    Public Sub CloseReader() Implements DataBaseOperation.CloseReader
        If Not SQLreader Is Nothing Then SQLreader.Close()
    End Sub

    Public Function SecureGetBool(Index As Integer) As Boolean Implements DataBaseOperation.SecureGetBool
        Throw New NotImplementedException()
    End Function

    Public Function SecureGetInt32(Index As Integer) As Integer Implements DataBaseOperation.SecureGetInt32
        Throw New NotImplementedException()
    End Function

    Public Function SecureGetString(Index As Integer) As String Implements DataBaseOperation.SecureGetString
        If TypeOf (SQLreader.GetValue(Index)) Is DBNull Then Return "" Else Return SQLreader.GetString(Index)
    End Function

    Public Function SecureGetDateTime(Index As Integer) As Date Implements DataBaseOperation.SecureGetDateTime
        Throw New NotImplementedException()
    End Function
End Class
