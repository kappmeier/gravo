Imports Gravo2k7.AccessDatabaseOperation

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

  Protected Function GetMaxIndex(ByVal Table As String) As Integer
    'TODO exception
    Dim command As String = "SELECT MAX(Index) FROM " & AddHighColons(Table) & ";"
    DBConnection.ExecuteReader(command)
    DBConnection.DBCursor.Read()
    Return DBConnection.SecureGetInt32(0)
  End Function
End Class
