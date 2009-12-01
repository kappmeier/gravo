Imports Gravo2k9.AccessDatabaseOperation

Public Class xlsBase
	' Datenbank-Anbindung
  Private m_DBConnection As AccessDatabaseOperation

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
    Set(ByVal DB As AccessDatabaseOperation)
      If m_bConnected Then m_DBConnection.Close()
      m_DBConnection = DB
      ' testen, ob die datenbank das richtige format hat
      Dim command As String = "SELECT [Date] FROM DBVersion WHERE Version='1.00';"
      DBConnection.ExecuteReader(command)
      m_bConnected = False
      If DBConnection.DBCursor.HasRows = False Then
        Throw New Exception("Database not valid!")
      Else
        DBConnection.DBCursor.Read()
        Dim DBdate As DateTime = DBConnection.SecureGetDateTime(0)
        If DBdate.Year = 2007 And DBdate.Month = 2 And DBdate.Day = 27 Then
          m_bConnected = True
        Else
          Throw New Exception("Database not valid!")
        End If
      End If
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

  Protected Function GetMaxIndex(ByVal Table As String) As Integer
    'TODO exception
    Dim command As String = "SELECT MAX(Index) FROM " & AddHighColons(Table) & ";"
    DBConnection.ExecuteReader(command)
    DBConnection.DBCursor.Read()
    Return DBConnection.SecureGetInt32(0)
  End Function
End Class
