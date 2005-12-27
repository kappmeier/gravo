Imports System.Data.OleDb

Public Class xlsVocInput
	Inherits xlsVocBase

	Public Word As xlsWord

	Sub New(ByVal db As CDBOperation, ByVal Table As String)	' Bestimmte Tabelle zum Zugriff öffnen
		MyBase.new(db, Table)
	End Sub

	Sub New(ByVal db As CDBOperation)	   ' Keinen Speziellen Table auswählen
		MyBase.New(db)
	End Sub

	Overridable Function NewWord() As Integer
		If IsConnected() = False Then Exit Function
		If (IsGroupSelected() = False) Then Exit Function

		' Vokabelnummer feststellen
		Dim sCommand As String
		Dim iCountWords As Integer = Me.WordNumbers.Count
		Dim iCountAll As Integer
		sCommand = "SELECT COUNT(*) FROM " & CurrentGroupName & ";"
		ExecuteReader(sCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iCountAll = 0 Else iCountAll = DBCursor.GetValue(0)

		' Zuerst schauen, ob gelöschte Vokabeln vorhanden sind.
		Dim iNewWordNumber As Integer
		iNewWordNumber = Me.GetDeleted
		If iNewWordNumber <> 0 Then
			sCommand = "UPDATE " & CurrentGroupName & " SET Deleted=" & False & " WHERE WordNumber=" & iNewWordNumber & ";"
			ExecuteNonQuery(sCommand)
			sCommand = "UPDATE " & CurrentGroupName & " SET UnitNumber=" & CurrentUnitNumber & ", WordInUnit=" & iCountWords + 1 & " WHERE WordNumber=" & iNewWordNumber & ";"
			ExecuteNonQuery(sCommand)
			GetWord(iNewWordNumber)
			CurrentWord.Word = ""
			CurrentWord.DeleteAllMeanings()
			CurrentWord.Extended1 = ""
			CurrentWord.Extended2 = ""
			CurrentWord.Extended3 = ""
			CurrentWord.ExtendedIsValid = False
			CurrentWord.Description = ""
			CurrentWord.MustKnow = True
			CurrentWord.WordType = 1
			CurrentWord.AdditionalTargetLangInfo = ""
			' TODO stat zurücksetzen im fall von deleted hinzugefügt
		Else		  ' Datensatz einfügen
			iNewWordNumber = iCountAll + 1
			CreateNewStat(iNewWordNumber)
			sCommand = "INSERT INTO " & CurrentGroupName & " VALUES ("
			sCommand += AddHighColons(CurrentUnitNumber) & ","
			sCommand += AddHighColons(0) & ","
			sCommand += "'" & AddHighColons("") & "',"
			sCommand += AddHighColons(iNewWordNumber) & ","
			sCommand += AddHighColons(iCountWords + 1) & ","
			sCommand += AddHighColons(0) & ","
			sCommand += True & ","			 ' MustKnow
			sCommand += "'" & AddHighColons("") & "',"			 ' Pre
			sCommand += "'" & AddHighColons("") & "',"			 ' Post
			sCommand += "'" & AddHighColons("") & "',"			   ' Meaning
			sCommand += False & ","			 ' Irregular Form
			sCommand += "'" & AddHighColons("") & "',"			 ' Irregular 1
			sCommand += "'" & AddHighColons("") & "',"			 ' Irregular 2
			sCommand += "'" & AddHighColons("") & "',"			 ' Irregular 3
			sCommand += "'" & AddHighColons("") & "',"			 ' Description
			sCommand += False & ","			 ' Deleted
			sCommand += "'" & AddHighColons("") & "'" & ");"			 ' Additional Target
			ExecuteReader(sCommand)
			DBCursor.Close()
		End If
		' hinzufügen eines neuen wortes
		' vtl. unit neu laden?
		Me.SelectUnit(Me.CurrentUnitNumber)
		Me.CurrentWordNumber = iNewWordNumber
		Return iNewWordNumber
	End Function

	Overridable Function NewWord(ByVal iUnit As Integer) As Integer
		SelectUnit(iUnit)
		Return NewWord()
	End Function

	Protected Function CreateNewStat(ByVal WordNumber As Integer)
		Dim sCommand As String
		sCommand = "INSERT INTO " & CurrentGroupName & "Stats VALUES ("
		sCommand += AddHighColons(WordNumber) & ","
		sCommand += AddHighColons(0) & ","
		sCommand += AddHighColons(0) & ","
		sCommand += AddHighColons(0) & ","
		sCommand += AddHighColons(0) & ","
		sCommand += AddHighColons(0) & ","
		sCommand += "'" & AddHighColons("01.01.1900") & "',"
		sCommand += "'" & AddHighColons("01.01.1900") & "',"
		sCommand += AddHighColons(False) & ","
		sCommand &= AddHighColons(0) & ","
		sCommand &= AddHighColons(0) & ","
		sCommand &= AddHighColons(0) & ");"
		ExecuteReader(sCommand)
		DBCursor.Close()
	End Function

	Protected Function ExistDeleted() As Boolean
		Dim sCommand As String
		sCommand = "SELECT COUNT(Deleted) FROM " & CurrentGroupName & " WHERE Deleted=" & True & ";"
		ExecuteReader(sCommand)
		DBCursor.Read()
		Dim iCount As Integer
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iCount = 0 Else iCount = DBCursor.GetValue(0)
		If iCount > 0 Then Return True Else Return False
	End Function

	Protected Function GetDeleted() As Integer
		Dim sCommand As String
		If Not ExistDeleted() Then
			Return 0
		Else
			sCommand = "SELECT WordNumber FROM " & CurrentGroupName & " WHERE Deleted=" & True & ";"
			ExecuteReader(sCommand)
			DBCursor.Read()
			If TypeOf (DBCursor.GetValue(0)) Is DBNull Then Return 0 Else Return DBCursor.GetValue(0)
		End If
	End Function






	' ********************************
	' * Muß noch überarbeitet werden *
	' ********************************
	Sub Delete()
		If IsConnected() = False Then Exit Sub
		If (IsGroupSelected() = False) Then Exit Sub
		' TODO beim abfragen muß getestet werden, ob die gerade betrachtete Vokabel gelöscht werden kann
		' Aktuelle Vokabel auf "Deleted" setzen
		Dim sCommand As String
		sCommand = "UPDATE " & CurrentGroupName & " SET Deleted=" & True & " WHERE WordNumber=" & CurrentWordNumber & ";"
		ExecuteNonQuery(sCommand)

		' Nachfolgende Vokabeln in derselben Lektion eine Nummer heraufsetzen
		Dim i As Integer
		For i = CurrentWord.WordInUnit + 1 To Me.WordNumbers.Count + 1		  ' Da vorher schon einer auf Deleted gesetzt wurde, um eins erhöhen
			sCommand = "UPDATE " & CurrentGroupName & " SET WordInUnit=" & i - 1 & " WHERE WordInUnit=" & i & ";"
			ExecuteNonQuery(sCommand)
		Next i
	End Sub
End Class
