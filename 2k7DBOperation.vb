Imports System.Data.OleDb

Public Class CDBOperation
  Protected oledbCmd As OleDbCommand = New OleDbCommand
  Protected oledbConnect As OleDbConnection = New OleDbConnection
  Protected oleCursor As OleDbDataReader
  Protected bInit As Boolean
  Private m_Path As String

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

  Public Function ExecuteReader(ByVal DBPath As String, ByVal CommandText As String) As OleDbDataReader
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

    If oledbConnect.State <> ConnectionState.Open Then MsgBox("Not open")

    If Not bInit Then Return Nothing
    ReOpen()
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
End Class