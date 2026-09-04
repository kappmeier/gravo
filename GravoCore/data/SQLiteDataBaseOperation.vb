Imports System.Data.Common
Imports Microsoft.Data.Sqlite
Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class SQLiteDataBaseOperation
    Implements IDataBaseOperation

    Private Const SingleQuotedLiteral As String = "'(?:[^']|'')*'"          ' '…' with '' escapes
    Private Const DoubleQuotedIdentifier As String = """(?:[^""]|"""")*"""  ' "…" with "" escapes
    Private Const BracketedIdentifier As String = "\[[^\]]*\]"              ' […]

    ' Regex to replace placeholders (?), built from three quoted patterns
    Private Shared ReadOnly QuotedRegionOrPlaceholder As New Regex(
        SingleQuotedLiteral & "|" & DoubleQuotedIdentifier & "|" & BracketedIdentifier & "|\?")

    Dim connection As New SqliteConnection()
    Dim connected As Boolean
    Dim SQLreader As SqliteDataReader
    ' The currently active (most recent) SQL command, kept as state to allow reading without disposal.
    Dim currentCommand As SqliteCommand

    Public Function Open(DBPath As String) As Boolean Implements IDataBaseOperation.Open
        If connected Then Close()
        connection.ConnectionString = "Data Source=" & DBPath & ";Foreign Keys=False;"
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
        Dim command As SqliteCommand
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
        Dim command As SqliteCommand = CreateParameterizedCommand(CommandText, values)
        command.ExecuteNonQuery()
        command.Dispose()
    End Function

    Public Function ExecuteReader(CommandText As String) As DbDataReader Implements IDataBaseOperation.ExecuteReader
        Dim SQLcommand As SqliteCommand
        If Not SQLreader Is Nothing Then SQLreader.Close()
        DisposePreviousCommand()
        SQLcommand = connection.CreateCommand
        SQLcommand.CommandText = CommandText
        SQLreader = SQLcommand.ExecuteReader()
        currentCommand = SQLcommand
        Return SQLreader
    End Function

    Function ExecuteReader(ByVal commandText As String, ByRef values As IEnumerable(Of Object)) As DbDataReader Implements IDataBaseOperation.ExecuteReader
        If Not SQLreader Is Nothing Then SQLreader.Close()
        DisposePreviousCommand()
        Dim sqlCommand As SqliteCommand = CreateParameterizedCommand(commandText, values)
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

    ''' <summary>
    ''' Creates a command from <paramref name="commandText"/> with potential ? placeholders rewritten to
    ''' named @paramN parameters. Each value of <paramref name="values"/> is bound to the corresponding named
    ''' parameter.
    ''' During replacement every value is bound through the VB Object-to-String conversion, as the DAOs rely on
    ''' (e.g. Date becomes a culture-general string).
    ''' </summary>
    Private Function CreateParameterizedCommand(commandText As String, values As IEnumerable(Of Object)) As SqliteCommand
        Dim command As SqliteCommand = connection.CreateCommand()
        Dim count As Integer = 0
        For Each value As String In values
            command.Parameters.AddWithValue("@param" & count, value)
            count += 1
        Next value
        command.CommandText = NameParameters(commandText, count)
        Return command
    End Function

    ''' <summary>
    ''' Replaces each positional ? placeholder to a named @paramN parameter. Quoted segments are matched
    ''' as units and kept verbatim, as user-derived group names and table names may contain '?'. Matched
    ''' groups include '…' string literals (honoring '' escapes), [.…] and "…" quoted identifiers.
    ''' Throws if the placeholder count does not match the supplied value count.
    ''' </summary>
    Private Shared Function NameParameters(commandText As String, expectedCount As Integer) As String
        Dim count As Integer = 0
        Dim result As String = QuotedRegionOrPlaceholder.Replace(
            commandText,
            Function(m)
                If m.Value <> "?" Then Return m.Value
                Dim name As String = "@param" & count
                count += 1
                Return name
            End Function)
        If count <> expectedCount Then
            Throw New ArgumentException("Found " & count & " parameter placeholders but got " &
                                        expectedCount & " values for command: " & commandText)
        End If
        Return result
    End Function

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
