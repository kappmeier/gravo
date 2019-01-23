Imports System.Data.OleDb
Imports Gravo

Public Class AccessDatabaseOperation
    Implements DataBaseOperation
    Protected oledbCmd As OleDbCommand = New OleDbCommand
    Protected oledbConnect As OleDbConnection = New OleDbConnection
    Protected oleCursor As OleDbDataReader
    Protected bInit As Boolean
    Protected m_Path As String
    Protected m_command As String = ""

    Public Sub New()

    End Sub

    Public Sub New(ByVal DBPath As String)
        Open(DBPath)
    End Sub

    Public ReadOnly Property Path() As String
        Get
            Return m_Path
        End Get
    End Property

    Public Function Open(ByVal DBPath As String) As Boolean Implements DataBaseOperation.Open
        If bInit Then Close()
        oledbConnect.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=" & DBPath
        oledbConnect.Open()
        oledbCmd.Connection = oledbConnect
        bInit = True
        m_Path = DBPath
        Return True
    End Function

    Public Function ExecuteNonQuery(ByVal DBPath As String, ByVal CommandText As String) As Boolean
        m_command = CommandText
        If bInit Then Close()
        Open(DBPath)
        oledbCmd.CommandText = CommandText
        Try
            oledbCmd.ExecuteNonQuery()
        Catch e As Exception
            Throw e
        End Try
        Return True
    End Function

    Public Function ExecuteNonQuery(ByVal CommandText As String) As Boolean Implements DataBaseOperation.ExecuteNonQuery
        m_command = CommandText
        If Not bInit Then Return False
        If Not oleCursor Is Nothing Then oleCursor.Close()
        oledbCmd.CommandText = CommandText
        Try
            oledbCmd.ExecuteNonQuery()
        Catch e As Exception When Err.Number = 5
            ReOpen()
            oledbCmd.ExecuteNonQuery()
        Catch e As Exception
            Throw e
        End Try
        Return True
    End Function

    Public Function ExecuteNonQuery() As Boolean
        Return ExecuteNonQuery(m_command)
    End Function

    Public Function ExecuteReader(ByVal DBPath As String, ByVal CommandText As String) As OleDbDataReader Implements DataBaseOperation.ExecuteReader
        m_command = CommandText
        If bInit Then Close()
        Open(DBPath)
        oledbCmd.CommandText = CommandText
        Try
            oleCursor = oledbCmd.ExecuteReader()
        Catch e As Exception
            Throw e
        End Try
        Return oleCursor
    End Function

    Public Function ExecuteReader(ByVal CommandText As String) As OleDbDataReader Implements DataBaseOperation.ExecuteReader
        m_command = CommandText

        If oledbConnect.State <> ConnectionState.Open Then MsgBox("Database is not open")

        If Not bInit Then Return Nothing
        'ReOpen()
        If Not oleCursor Is Nothing Then oleCursor.Close()
        oledbCmd.CommandText = CommandText

        Try
            oleCursor = oledbCmd.ExecuteReader()
        Catch e As Exception
            Throw e
        End Try
        Return oleCursor
    End Function

    Public Function ExecuteReader() As OleDbDataReader
        Return ExecuteReader(m_command)
    End Function

    Public Function Close() As Boolean Implements DataBaseOperation.Close
        If Not bInit Then Return True
        oledbConnect.Close()
        If Not oleCursor Is Nothing Then oleCursor.Close()
        bInit = False
        Return True
    End Function

    Public Sub CloseReader() Implements DataBaseOperation.CloseReader
        If Not oleCursor Is Nothing Then oleCursor.Close()
    End Sub

    Public Function ReOpen() As Boolean
        If Not bInit Then Return False
        Dim sPath As String = oledbConnect.ConnectionString
        Close()
        oledbConnect.ConnectionString = sPath
        oledbConnect.Open()
        oledbCmd.Connection = oledbConnect
        bInit = True
        Return True
    End Function

    Public Shared Function GetDBEntry(ByVal text As String) As String
        Return "'" & AddHighColons(text) & "'"
    End Function

    Public Shared Function GetDBEntry(ByVal text As Integer) As String
        Return "'" & text.ToString & "'"
    End Function

    Public Shared Function GetDBEntry(ByVal value As Boolean) As String
        Return "'" & AddHighColons(value) & "'"
    End Function

    Public Shared Function AddHighColons(ByVal Text As String) As String
        Dim sTemp, sTemp2 As String
        Dim i As Integer = 0
        sTemp2 = Text
        sTemp = ""
        Do
            i = InStr(1, sTemp2, "'")
            If i > 0 Then
                sTemp = sTemp & Mid(sTemp2, 1, i) & "'"
                sTemp2 = Right(sTemp2, Len(sTemp2) - i)
            Else
                sTemp = sTemp & sTemp2
                sTemp2 = ""
            End If
        Loop Until sTemp2 = ""
        Return sTemp
    End Function

    Public Shared Function AddHighColons(ByVal Value As Boolean) As String
        Return (IIf(Value, "-1", "0"))
    End Function

    Public Shared Function SecureGetBool(ByRef dbc As OleDbDataReader, ByVal Index As Integer) As Boolean
        If TypeOf (dbc.GetValue(Index)) Is DBNull Then Return False Else Return dbc.GetBoolean(Index)
    End Function

    Public Shared Function SecureGetInt32(ByRef dbc As OleDbDataReader, ByVal Index As Integer) As Integer
        If TypeOf (dbc.GetValue(Index)) Is DBNull Then Return 0 Else Return dbc.GetInt32(Index)
    End Function

    Public Shared Function SecureGetString(ByRef dbc As OleDbDataReader, ByVal Index As Integer) As String
        If TypeOf (dbc.GetValue(Index)) Is DBNull Then Return "" Else Return dbc.GetString(Index)
    End Function

    Public Function SecureGetDateTime(ByRef dbc As OleDbDataReader, ByVal Index As Integer) As DateTime
        If TypeOf (dbc.GetValue(Index)) Is DBNull Then Return "" Else Return dbc.GetDateTime(Index)
    End Function

    Public Function SecureGetBool(ByVal Index As Integer) As Boolean Implements DataBaseOperation.SecureGetBool
        If TypeOf (oleCursor.GetValue(Index)) Is DBNull Then Return False Else Return oleCursor.GetBoolean(Index)
    End Function

    Public Function SecureGetInt32(ByVal Index As Integer) As Integer Implements DataBaseOperation.SecureGetInt32
        If TypeOf (oleCursor.GetValue(Index)) Is DBNull Then Return 0 Else Return oleCursor.GetInt32(Index)
    End Function

    Public Function SecureGetString(ByVal Index As Integer) As String Implements DataBaseOperation.SecureGetString
        If TypeOf (oleCursor.GetValue(Index)) Is DBNull Then Return "" Else Return oleCursor.GetString(Index)
    End Function

    Public Function SecureGetDateTime(ByVal Index As Integer) As DateTime Implements DataBaseOperation.SecureGetDateTime
        If TypeOf (oleCursor.GetValue(Index)) Is DBNull Then Return "" Else Return oleCursor.GetDateTime(Index)
    End Function

    Public Function DBCursor() As OleDbDataReader Implements DataBaseOperation.DBCursor

        Return oleCursor
    End Function

    Public Shared Function StripSpecialCharacters(ByVal input As String) As String
        Dim withoutSpecialCharacters As String = input
        withoutSpecialCharacters = withoutSpecialCharacters.Replace(" ", "")
        withoutSpecialCharacters = withoutSpecialCharacters.Replace("!", "")
        Return withoutSpecialCharacters
    End Function

End Class