Imports System.Data.OleDb

Public Class xlsBase
	' Datenbank-Anbindung
  Private m_DBConnection As AccessDatabaseOperation
	'Private m_DBCommand As String
	Private m_DBCursor As OleDbDataReader

	' Klassenzustände
	Private m_bConnected As Boolean = False	 ' mit der Datenbank verbunden

	Sub New()
		m_bConnected = False
	End Sub

  Sub New(ByVal db As AccessDatabaseOperation)    ' Keinen Speziellen Table auswählen
    m_bConnected = True
    m_DBConnection = db
  End Sub

  Public Property DBConnection() As AccessDatabaseOperation
    Get
      Return m_DBConnection
    End Get
    Set(ByVal DB As  AccessDatabaseOperation)
      If m_bConnected Then m_DBConnection.Close()
      m_DBConnection = DB
      m_bConnected = True
    End Set
  End Property

  Sub Close()
    If IsConnected() = False Then Exit Sub
    m_DBConnection.Close()
    m_bConnected = False
  End Sub

  Public Function IsConnected() As Boolean
    Return m_bConnected
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

  Public Function SecureGetInt32(ByRef dbc As OleDbDataReader, ByVal Index As Integer) As Integer
    If TypeOf (dbc.GetValue(Index)) Is DBNull Then Return 0 Else Return dbc.GetInt32(Index)
  End Function

  Public Function SecureGetString(ByRef dbc As OleDbDataReader, ByVal Index As Integer) As String
    If TypeOf (dbc.GetValue(Index)) Is DBNull Then Return "" Else Return dbc.GetString(Index)
  End Function

  Protected Function GetMaxIndex(ByVal Table As String) As Integer
    'TODO exception
    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT MAX(Index) FROM " & AddHighColons(Table) & ";"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    DBCursor.Read()
    Return Me.SecureGetInt32(DBCursor, 0)
  End Function

  '**********************************************************************
  '**********************************************************************
  '***                                                                ***
  '***                            Entfernen                           ***
  '***                                                                ***
  '**********************************************************************
  '**********************************************************************

  Public Sub ExecuteReader(ByVal sCommand As String)
    m_DBCursor = m_DBConnection.ExecuteReader(sCommand)
  End Sub

  Public Sub ExecuteNonQuery(ByVal sCommand As String)
    m_DBConnection.ExecuteNonQuery(sCommand)
  End Sub

  Public ReadOnly Property DBCursor() As OleDbDataReader
    Get
      Return m_DBCursor
    End Get
  End Property
End Class
