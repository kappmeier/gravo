Imports System.Data.OleDb

Public Class AccessDatabaseOperation
  Protected oledbCmd As OleDbCommand = New OleDbCommand
  Protected oledbConnect As OleDbConnection = New OleDbConnection
  Protected oleCursor As OleDbDataReader
  Protected bInit As Boolean
  Protected m_Path As String
  Protected m_sCommand As String = ""

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

  Public Function Open(ByVal DBPath As String) As Boolean
    If bInit Then Close()
    oledbConnect.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=" & DBPath
    oledbConnect.Open()
    oledbCmd.Connection = oledbConnect
    bInit = True
    m_Path = DBPath
    Return True
  End Function

  Public Function ExecuteNonQuery(ByVal DBPath As String, ByVal CommandText As String) As Boolean
    m_sCommand = CommandText
    If bInit Then Close()
    Open(DBPath)
    oledbCmd.CommandText = CommandText
    Try
      oledbCmd.ExecuteNonQuery()
    Catch e As Exception
      MsgBox("Exception")
      ReOpen()
      oledbCmd.ExecuteNonQuery()
    End Try
    Return True
  End Function

  Public Function ExecuteNonQuery(ByVal CommandText As String) As Boolean
    m_sCommand = CommandText
    If Not bInit Then Return False
    If Not oleCursor Is Nothing Then oleCursor.Close()
    oledbCmd.CommandText = CommandText
    Try
      oledbCmd.ExecuteNonQuery()
    Catch e As Exception When Err.Number = 5
      ReOpen()
      oledbCmd.ExecuteNonQuery()
    Catch e As Exception
      MsgBox(e.Message)
    End Try
    Return True
  End Function

  Public Function ExecuteNonQuery() As Boolean
    Return ExecuteNonQuery(m_sCommand)
  End Function

  Public Function ExecuteReader(ByVal DBPath As String, ByVal CommandText As String) As OleDbDataReader
    m_sCommand = CommandText
    If bInit Then Close()
    Open(DBPath)
    oledbCmd.CommandText = CommandText
    Try
      oleCursor = oledbCmd.ExecuteReader()
    Catch e As Exception
      ReOpen()
      oleCursor = oledbCmd.ExecuteReader()
    End Try
    Return oleCursor
  End Function

  Public Function ExecuteReader(ByVal CommandText As String) As OleDbDataReader
    m_sCommand = CommandText

    If oledbConnect.State <> ConnectionState.Open Then MsgBox("Database is not open")

    If Not bInit Then Return Nothing
    'ReOpen()
    If Not oleCursor Is Nothing Then oleCursor.Close()
    oledbCmd.CommandText = CommandText
    Try
      oleCursor = oledbCmd.ExecuteReader()
    Catch e As Exception
      ReOpen()
      MsgBox("Exception")
      oleCursor = oledbCmd.ExecuteReader()
    End Try
    Return oleCursor
  End Function

  Public Function ExecuteReader() As OleDbDataReader
    Return ExecuteReader(m_sCommand)
  End Function

  Public Function Close() As Boolean
    If Not bInit Then Return True
    oledbConnect.Close()
    If Not oleCursor Is Nothing Then oleCursor.Close()
    bInit = False
    Return True
  End Function

  Public Sub CloseReader()
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
End Class