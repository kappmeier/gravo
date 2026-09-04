Imports System.Data.Common
Imports System.Data.SQLite
Imports System.Globalization

Public Class SQLiteDataBaseOperation
    Implements IDataBaseOperation

    Dim connection As New SQLiteConnection()
    Dim connected As Boolean
    Dim SQLreader As SQLiteDataReader
    ' The currently active (most recent) SQL command, kept as state to allow reading without disposal.
    Dim currentCommand As SQLiteCommand

    Public Function Open(DBPath As String) As Boolean Implements IDataBaseOperation.Open
        If connected Then Close()
        connection.ConnectionString = "Data Source=" & DBPath & ";"
        connection.Open()
        connected = True
        Return True
    End Function

    Public Function Close() As Boolean Implements IDataBaseOperation.Close
        connection.Close()
        connection.Dispose()
        connected = False
        Return True
    End Function

    Public Function ExecuteNonQuery(CommandText As String) As Boolean Implements IDataBaseOperation.ExecuteNonQuery
        If Not connected Then Return False
        If Not SQLreader Is Nothing Then SQLreader.Close()
        DisposePreviousCommand()
        Dim command As SQLiteCommand
        command = connection.CreateCommand
        command.CommandText = CommandText
        command.ExecuteNonQuery()
        command.Dispose()
        Return True
    End Function

    ''' <summary>
    ''' Observe that due to the limitations of SQLite it is not possible to use values for parameters in table names for ALTER TABLE
    ''' commands.
    ''' </summary>
    ''' <param name="CommandText">the command text includding placeholders (?) for paramters</param>
    ''' <param name="values">the parameter values</param>
    ''' <returns></returns>
    Function ExecuteNonQuery(ByVal CommandText As String, ByRef values As IEnumerable(Of Object)) As Boolean Implements IDataBaseOperation.ExecuteNonQuery
        If Not connected Then Return False
        If Not SQLreader Is Nothing Then SQLreader.Close()
        DisposePreviousCommand()
        Dim command As SQLiteCommand
        command = connection.CreateCommand
        command.CommandText = CommandText
        Dim count As Integer = 0
        For Each parameter As String In values
            command.Parameters.AddWithValue("param" & count, parameter)
            count += 1
        Next parameter
        command.ExecuteNonQuery()
        command.Dispose()
    End Function

    Public Function ExecuteReader(CommandText As String) As DbDataReader Implements IDataBaseOperation.ExecuteReader
        Dim SQLcommand As SQLiteCommand
        If Not SQLreader Is Nothing Then SQLreader.Close()
        DisposePreviousCommand()
        SQLcommand = connection.CreateCommand
        SQLcommand.CommandText = CommandText
        SQLreader = SQLcommand.ExecuteReader()
        currentCommand = SQLcommand
        Return SQLreader
    End Function

    Function ExecuteReader(ByVal commandText As String, ByRef values As IEnumerable(Of Object)) As DbDataReader Implements IDataBaseOperation.ExecuteReader
        Dim sqlCommand As SQLiteCommand
        If Not SQLreader Is Nothing Then SQLreader.Close()
        DisposePreviousCommand()
        sqlCommand = connection.CreateCommand
        sqlCommand.CommandText = commandText
        Dim count As Integer = 0
        For Each value As String In values
            sqlCommand.Parameters.AddWithValue("param" & count, value)
            count += 1
        Next value
        SQLreader = sqlCommand.ExecuteReader()
        currentCommand = sqlCommand
        Return SQLreader
    End Function

    Public Function DBCursor() As DbDataReader Implements IDataBaseOperation.DBCursor
        Return SQLreader
    End Function

    Public Sub CloseReader() Implements IDataBaseOperation.CloseReader
        If Not SQLreader Is Nothing Then SQLreader.Close()
        DisposePreviousCommand()
    End Sub

    ''' <summary>
    ''' Disposes the previously executed SQL command, if any. Must be called before a new command is created. In each
    ''' procedure and function.
    ''' </summary>
    ''' <remarks>
    ''' This is necessary to prevent the disposal of the command from invalidating the active reader.
    ''' </remarks>
    Private Sub DisposePreviousCommand()
        If currentCommand IsNot Nothing Then
            currentCommand.Dispose()
            currentCommand = Nothing
        End If
    End Sub

    Public Function SecureGetBool(Index As Integer) As Boolean Implements IDataBaseOperation.SecureGetBool
        If TypeOf (SQLreader.GetValue(Index)) Is DBNull Then Return False Else Return SQLreader.GetBoolean(Index)
    End Function

    Public Function SecureGetInt32(Index As Integer) As Integer Implements IDataBaseOperation.SecureGetInt32
        If TypeOf (SQLreader.GetValue(Index)) Is DBNull Then Return 0 Else Return SQLreader.GetInt32(Index)
    End Function

    Public Function SecureGetString(Index As Integer) As String Implements IDataBaseOperation.SecureGetString
        If TypeOf (SQLreader.GetValue(Index)) Is DBNull Then Return "" Else Return SQLreader.GetString(Index)
    End Function

    Public Function SecureGetDateTime(Index As Integer) As Date Implements IDataBaseOperation.SecureGetDateTime
        '        Try
        If TypeOf (SQLreader.GetValue(Index)) Is DBNull Then
            Return Nothing
        Else
            ' For some reason, we need to extract it first...
            Dim d As Date = SQLreader.GetDateTime(Index)
            Return d
        End If
        'Catch ex As FormatException
        '    Dim d As String = SQLreader.GetString(Index)
        '    Return Date.Parse(d)
        'End Try
    End Function

    Shared Function NowDB() As String
        Return DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
    End Function

End Class
