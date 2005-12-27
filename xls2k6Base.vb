Imports System.Data.OleDb

Public Class xlsBase
	' Datenbank-Anbindung
	Private m_DBConnection As CDBOperation
	'Private m_DBCommand As String
	Private m_DBCursor As OleDbDataReader

	' Klassenzustände
	Private m_bConnected As Boolean = False	 ' mit der Datenbank verbunden

	Sub New()
		m_bConnected = False
	End Sub

	Sub New(ByVal db As CDBOperation)	   ' Keinen Speziellen Table auswählen
		m_bConnected = True
		m_DBConnection = db
	End Sub

	Sub Close()
		If IsConnected() = False Then Exit Sub
		m_DBConnection.Close()
		m_bConnected = False
	End Sub

	Public Function ExecuteReader(ByVal sCommand)
		m_DBCursor = m_DBConnection.ExecuteReader(sCommand)
	End Function

	Public Function ExecuteNonQuery(ByVal sCommand)
		m_DBConnection.ExecuteNonQuery(sCommand)
	End Function

	Public ReadOnly Property DBConnection() As CDBOperation
		Get
			Return m_DBConnection
		End Get
	End Property

	Public ReadOnly Property DBCursor() As OleDbDataReader
		Get
			Return m_DBCursor
		End Get
	End Property

	Public Function IsConnected() As Boolean
		Return m_bConnected
	End Function

	Public Shared Function AddHighColons(ByVal Text As String) As String
		Dim sTemp, sTemp2 As String
		Dim i As Integer = 0
		sTemp2 = Text
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
