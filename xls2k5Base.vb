Imports System.Data.OleDb

Public Class xlsBase
	' Datenbank-Anbindung
	Protected DBConnection As CDBOperation
	Protected DBCommand As String
	Protected DBCursor As OleDbDataReader

	' Klassenzustände
	Protected m_bConnected As Boolean = False		' mit der Datenbank verbunden
	Protected m_bTableSelected As Boolean = False	' ob ein Vokabelset gewählt wurde
	Protected m_sTable As String = ""		' aktuelles Vokabelset

	Sub New(ByVal db As CDBOperation, ByVal Table As String)	' Bestimmte Tabelle zum Zugriff öffnen
		m_bConnected = True
		DBConnection = db
		m_sTable = Table
		m_bTableSelected = True
	End Sub

	Sub New(ByVal db As CDBOperation)	   ' Keinen Speziellen Table auswählen
		m_bConnected = True
		DBConnection = db
		m_bTableSelected = False
	End Sub

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
