Imports System.Data.OleDb

Public Class xlsVocInput
	Inherits xlsVocBase

	Protected wtWordsInUnit As xlsWordCollection
	Public Word As xlsWordInformationEx

	Sub New(ByVal db As CDBOperation, ByVal Table As String)	' Bestimmte Tabelle zum Zugriff öffnen
		MyBase.new(db, Table)
		wtWordsInUnit = New xlsWordCollection(db)
	End Sub

	Sub New(ByVal db As CDBOperation)	   ' Keinen Speziellen Table auswählen
		MyBase.New(db)
		wtWordsInUnit = New xlsWordCollection(db)
	End Sub

	Overloads Overrides ReadOnly Property WordsInUnit(ByVal UnitNumber As Int32) As xlsWordCollection
		Get
			wtWordsInUnit = MyBase.WordsInUnit(UnitNumber)
			Return wtWordsInUnit
		End Get
	End Property

	Overridable Function NewWord() As Integer
		If m_bConnected = False Then Exit Function
		If (m_bTestMode = True) Or (m_bTableSelected = False) Then Exit Function

		' Vokabelnummer feststellen
		Dim iCountWords As Integer = WordsInUnit(m_iUnit).Count
		Dim iCountAll As Integer
		DBCommand = "SELECT COUNT(*) FROM " & m_sTable & ";"
		DBCursor = DBConnection.ExecuteReader(DBCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iCountAll = 0 Else iCountAll = DBCursor.GetValue(0)

		' Zuerst schauen, ob gelöschte Vokabeln vorhanden sind.
		Dim iNewWordNumber As Integer
		iNewWordNumber = Me.GetDeleted
		If iNewWordNumber <> 0 Then
			DBCommand = "UPDATE " & m_sTable & " SET Deleted=" & False & " WHERE WordNumber=" & iNewWordNumber & ";"
			DBConnection.ExecuteNonQuery(DBCommand)
			DBCommand = "UPDATE " & m_sTable & " SET UnitNumber=" & m_iUnit & ", WordInUnit=" & iCountWords + 1 & " WHERE WordNumber=" & iNewWordNumber & ";"
			DBConnection.ExecuteNonQuery(DBCommand)
			GetWord(iNewWordNumber)
			wtWord.Word = ""
			wtWord.Meaning1 = ""
			wtWord.Meaning2 = ""
			wtWord.Meaning3 = ""
			wtWord.Extended1 = ""
			wtWord.Extended2 = ""
			wtWord.Extended3 = ""
			wtWord.ExtendedIsValid = False
			wtWord.Description = ""
			wtWord.MustKnow = True
			wtWord.WordType = 1
			wtword.AdditionalTargetLangInfo = ""
		Else		  ' Datensatz einfügen
			iNewWordNumber = iCountAll + 1
			CreateNewStat(iNewWordNumber)
			DBCommand = "INSERT INTO " & m_sTable & " VALUES ("
			DBCommand += AddHighColons(m_iUnit) & ","
			DBCommand += AddHighColons(0) & ","
			DBCommand += "'" & AddHighColons("") & "',"
			DBCommand += AddHighColons(iNewWordNumber) & ","
			DBCommand += AddHighColons(iCountWords + 1) & ","
			DBCommand += AddHighColons(0) & ","
			DBCommand += True & ","
			DBCommand += "'" & AddHighColons("") & "',"
			DBCommand += "'" & AddHighColons("") & "',"
			DBCommand += "'" & AddHighColons("") & "',"
			DBCommand += "'" & AddHighColons("") & "',"
			DBCommand += "'" & AddHighColons("") & "',"
			DBCommand += False & ","
			DBCommand += "'" & AddHighColons("") & "',"
			DBCommand += "'" & AddHighColons("") & "',"
			DBCommand += "'" & AddHighColons("") & "',"
			DBCommand += "'" & AddHighColons("") & "',"
			DBCommand += False & ","			 ' Description + Deleted
			DBCommand += "'" & AddHighColons("") & "'" & ");"
			dbcursor = dbConnection.ExecuteReader(DBCommand)
			dbcursor.Close()
		End If
		Return iNewWordNumber
	End Function

	Overridable Function NewWord(ByVal Unit As Integer) As Integer
		m_iUnit = Unit
		Return NewWord()
	End Function

	Protected Function CreateNewStat(ByVal WordNumber As Integer)
		DBCommand = "INSERT INTO " & m_stable & "Stats VALUES ("
		DBCommand += AddHighColons(WordNumber) & ","
		DBCommand += AddHighColons(0) & ","
		DBCommand += AddHighColons(0) & ","
		DBCommand += AddHighColons(0) & ","
		DBCommand += AddHighColons(0) & ","
		DBCommand += AddHighColons(0) & ","
		DBCommand += "'" & AddHighColons("01.01.1900") & "',"
		DBCommand += "'" & AddHighColons("01.01.1900") & "',"
		DBCommand += AddHighColons(False) & ","
		DBCommand &= AddHighColons(0) & ","
		DBCommand &= AddHighColons(0) & ","
		DBCommand &= AddHighColons(0) & ");"
		dbcursor = DBConnection.ExecuteReader(dbcommand)
		dbcursor.Close()
	End Function

	Sub Delete()
		If m_bConnected = False Then Exit Sub
		If (m_bTestMode = True) Or (m_bTableSelected = False) Then Exit Sub

		' Aktuelle Vokabel auf "Deleted" setzen
		DBCommand = "UPDATE " & m_sTable & " SET Deleted=" & True & " WHERE WordNumber=" & m_iWordNumber & ";"
		DBConnection.ExecuteNonQuery(DBCommand)

		' Nachfolgende Vokabeln in derselben Lektion eine Nummer heraufsetzen
		Dim i As Integer
		For i = wtWord.WordInUnit + 1 To WordsInUnit(m_iUnit).Count + 1		 ' Da vorher schon einer auf Deleted gesetzt wurde, um eins erhöhen
			DBCommand = "UPDATE " & m_sTable & " SET WordInUnit=" & i - 1 & " WHERE WordInUnit=" & i & ";"
			DBConnection.ExecuteNonQuery(DBCommand)
		Next i
	End Sub

	Protected Function ExistDeleted() As Boolean
		DBCommand = "SELECT COUNT(Deleted) FROM " & m_sTable & " WHERE Deleted=" & True & ";"
		DBCursor = DBConnection.ExecuteReader(DBCommand)
		DBCursor.Read()
		Dim iCount As Integer
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iCount = 0 Else iCount = DBCursor.GetValue(0)
		If iCount > 0 Then Return True Else Return False
	End Function

	Protected Function GetDeleted() As Integer
		If Not ExistDeleted() Then
			Return 0
		Else
			DBCommand = "SELECT WordNumber FROM " & m_sTable & " WHERE Deleted=" & True & ";"
			DBCursor = DBConnection.ExecuteReader(DBCommand)
			DBCursor.Read()
			If TypeOf (DBCursor.GetValue(0)) Is DBNull Then Return 0 Else Return DBCursor.GetValue(0)
		End If
	End Function

	ReadOnly Property Language() As String
		Get
			If m_bConnected = False Then Exit Property
			If Trim(m_sTable) = "" Then Exit Property

			Dim sLanguage As String

			DBCommand = "SELECT Art FROM Tables WHERE Tabelle='" & m_sTable & "';"
			DBCursor = DBConnection.ExecuteReader(DBCommand)
			DBCursor.Read()
			If TypeOf (DBCursor.GetValue(0)) Is DBNull Then sLanguage = "" Else sLanguage = DBCursor.GetValue(0)

			Return sLanguage
		End Get
	End Property

	ReadOnly Property Types() As ArrayList
		Get
			Select Case Language()
				Case "French"
					Return FrenchTypes()
				Case "English"
					Return EnglishTypes()
				Case "Latin"
					Return LatinTypes()
				Case "Italian"
					Return ItalianTypes()
				Case Else
					Dim sList As New ArrayList
					sList.Add("Einfach")					  '0
					Return sList
			End Select
		End Get
	End Property

	Protected Function ItalianTypes() As ArrayList
		Dim sList As New ArrayList
		sList.Add("Substantiv")		   '0
		sList.Add("Verb")		   '1
		sList.Add("Adjektiv")		  '2
		sList.Add("Einfache")		  '3
		Return sList
	End Function

	Protected Function FrenchTypes() As ArrayList
		Dim sList As New ArrayList
		sList.Add("Substantiv")		   '0
		sList.Add("Verb")		   '1
		sList.Add("Adjektiv")		  '2
		sList.Add("Einfache")		  '3
		Return sList
	End Function

	Protected Function EnglishTypes() As ArrayList
		Dim sList As New ArrayList
		sList.Add("Substantiv")		   '0
		sList.Add("Verb")		   '1
		sList.Add("Adjektiv")		  '2
		sList.Add("Einfache")		  '3
		Return sList
	End Function

	Protected Function LatinTypes() As ArrayList
		Dim sList As New ArrayList
		sList.Add("Substantiv")		   '0
		sList.Add("Verb")		   '1
		sList.Add("Adjektiv")		  '2
		sList.Add("Einfach")		   '3
		Return sList
	End Function
End Class
