Imports System.Data.OleDb

Public Class GroupInfo
    Public Table As String
    Public Description As String
    Public Type As String
End Class

Public Structure TestUnits
    Public Unit As Integer
    Public Table As String
End Structure

Public Structure TestWord
    Public WordNumber As Integer
    Public Table As String
End Structure

Public Enum TestWordModes
    LanguageDefault
    TestWord
    TestMeaning
End Enum

Public Enum SaveErrors
    NoError
    TableExists
    UnknownError
    NotConnected
End Enum

Public Enum IrregularTest
	Always
	Never
	IrregularOnly
End Enum

Public Enum HelpModes
	NoHelp
	LightHelp
	MiddleHelp
	HeavyHelp
End Enum

Public Class CWordTest

#Region " Variablen "
	Public Groups As CWordTestGroupCollection
	Protected DBConnection As New CDBOperation
	Protected sCommandText As String
	Protected oleCursor As OleDbDataReader

	Protected bConnected As Boolean = False

	Protected sVersionHistory() As String = {"1.21", "1.20", "1.11", "1.10", "1.00"}	' Neuste vorne

	Protected bNew = True	  ' Neuer Datensatz
	Protected iCurrent As Integer = 0	  ' Aktueller Datensatz
	Protected cWords As Collection
	Protected cTestUnits As Collection

	' Membervariablen
	Protected m_sTable As String = ""
	Protected m_sWord As String = ""	   'Vokabel
	Protected m_sPre As String = ""	 'Pre-Vokabel    (to, le, ...)
	Protected m_sPost As String = ""	   'Post-Vokabel   (Plural, slang, ...)
	Protected m_sMeaning1 As String
	Protected m_sMeaning2 As String
	Protected m_sMeaning3 As String	'Bedeutung
	Protected m_sIrregular1, m_sIrregular2, m_sIrregular3 As String	   'Irregular

	Protected m_sDescription As String
	Protected m_bDeleted As Boolean

	Protected m_iWordType As Integer = 1	  'Vokabelart (Nomen, Verb ...)
	Protected m_iTestType As Integer = 1	  'Abfrageart (Random ...)
	Protected m_bMustKnow As Boolean = True	  'Vokabel muß nicht gewußt werden
	Protected m_bIrregularForm As Boolean = False	  'Vokabel hat irreguläre Formen, abhängig von Vokabelart
	Protected m_sUnit As String = ""
	Protected m_iUnit As Integer = -1
	Protected m_iChapter As Integer = 1
	Protected m_iWordInUnit As Integer = -1
	Protected m_bTestMode As Boolean = False
	Protected m_sTestWord As String
	Protected m_bNoSpecialMode As Boolean = False
	Protected m_sPath As String	   'Pfad zur Datenbank
	Protected m_iTestNextMode As Integer = 3
	Protected m_iTestNextModeWrong As Integer = 4
	Protected iTestCurrentWord As Integer = 0
	Protected bErneut = False
	Protected m_iIrregular As IrregularTest

	Protected m_iTestWordCountAll As Integer
	Protected m_iTestWordCountToDo As Integer
	Protected m_iTestWordCountDone As Integer
	Protected m_iTestWordCountDoneRight As Integer
	Protected m_iTestWordCountDoneFalse As Integer
	Protected m_iTestWordCountDoneFalseAllTrys As Integer
	Protected m_bWordToMeaning As Boolean
	Protected m_bFirstTry As Boolean
	Protected iTestWordCountDoneCorrection As Integer
	Protected m_iHelpMode As Integer
	Protected m_iTestWordCountHelp1 As Integer
	Protected m_iTestWordCountHelp2 As Integer
	Protected m_iTestWordCountHelp3 As Integer

	Protected m_iTestWordMode As Integer

	Protected m_sLastTested As String
#End Region

#Region " Datenbank-Funktionen "
	Sub New(ByVal Path As String, ByVal Table As String)	' Bestimmte Tabelle zum Zugriff öffnen
		bConnected = True
		DBConnection.Open(Path)
		m_sTable = Table
		m_bNoSpecialMode = False
		m_bTestMode = False
		m_sPath = Path
		Groups = New CWordTestGroupCollection(Path)
	End Sub

	Sub New(ByVal Path As String)	   ' Keinen Speziellen Table auswählen
		bConnected = True
		DBConnection.Open(Path)
		m_bNoSpecialMode = True
		m_bTestMode = False
		m_sPath = Path
		Groups = New CWordTestGroupCollection(Path)
	End Sub

	Sub SelectTable(ByVal table As String)
		m_sTable = table
		m_bNoSpecialMode = False
		m_bTestMode = False
	End Sub

	Sub CloseTable()
		m_sTable = ""
		m_bNoSpecialMode = True
		m_bTestMode = False
	End Sub

	Sub Close()
		If bConnected = False Then Exit Sub
		DBConnection.Close()
		bConnected = False
	End Sub

	Function SaveTable(ByVal Path As String, Optional ByVal SaveOnlyNewFiles As Boolean = False, Optional ByVal Overwrite As Boolean = False, Optional ByRef ProgressBar As ProgressBar = Nothing, Optional ByRef InfoLabel As Label = Nothing, Optional ByVal Action As String = "gesichert") As SaveErrors
		If bConnected = False Then Return SaveErrors.NotConnected
		Dim bProgress As Boolean = Not (ProgressBar Is Nothing)
		Dim bLabel As Boolean = Not (InfoLabel Is Nothing)
		If bLabel Then
			InfoLabel.Text = "Sichern wird vorbereitet."
			Application.DoEvents()
		End If
		Dim DBSaveConnection As New CDBOperation
		Dim iWords As Integer
		Dim sGroupName As String
		Dim sNewTableName As String
		Dim sLanguage As String
		Dim iTablesInLanguage As Integer

		DBSaveConnection.Open(Path)
		' Unit und Table Informationen eruieren
		oleCursor = DBConnection.ExecuteReader("SELECT Lehrbuch FROM Tables WHERE Tabelle='" & m_sTable & "';")
		oleCursor.Read()
		If TypeOf (oleCursor.GetValue(0)) Is DBNull Then Return SaveErrors.UnknownError Else sGroupName = oleCursor.GetValue(0)

		oleCursor = DBSaveConnection.ExecuteReader("SELECT Tabelle FROM Tables WHERE Lehrbuch='" & sGroupName & "';")
		oleCursor.Read()
		Try
			If oleCursor.HasRows = True Then
				If TypeOf (oleCursor.GetValue(0)) Is DBNull Then sNewTableName = "" Else sNewTableName = oleCursor.GetValue(0)
			Else
				sNewTableName = ""
			End If
		Catch e As InvalidOperationException		  ' Keine Daten vorhanden	TODO		???? weiterhin sinnvoll ????
			sNewTableName = ""
		End Try

		' Aktuellen Table hinzufügen, falls schon vorhanden, löschen und neuanlegen
		Dim iStart As Integer = 1
		If sNewTableName <> "" Then		  ' Schon vorhanden, löschen und anschließend Neuanlegen
			If SaveOnlyNewFiles Then
				sCommandText = "SELECT COUNT(WordNumber) FROM " & sNewTableName & ";"
				oleCursor = DBSaveConnection.ExecuteReader(sCommandText)
				oleCursor.Read()
				If TypeOf (oleCursor.GetValue(0)) Is DBNull Then iStart = 1 Else iStart = oleCursor.GetValue(0) + 1
			Else
				If (Overwrite) Then
					DBSaveConnection.ExecuteNonQuery("DROP TABLE " & sNewTableName & ";")
					DBSaveConnection.ExecuteNonQuery("DROP TABLE " & sNewTableName & "Stats;")
					DBSaveConnection.ExecuteNonQuery("DROP TABLE " & sNewTableName & "Units;")
				Else
					Return SaveErrors.TableExists
				End If
			End If
		Else		  ' Feststellen der richtigen Nummer und Sprache, nicht vorhanden und anschließend Neuanlegen
			oleCursor = DBSaveConnection.ExecuteReader("SELECT COUNT(Tabelle) FROM Tables WHERE Art='" & Me.Language & "';")
			oleCursor.Read()
			If TypeOf (oleCursor.GetValue(0)) Is DBNull Then iTablesInLanguage = 0 Else iTablesInLanguage = oleCursor.GetValue(0)
			' Table in Table-Liste eintragen
			If iTablesInLanguage < 8 Then sNewTableName = Me.Language & "0" & (iTablesInLanguage + 1) Else sNewTableName = Me.Language & (iTablesInLanguage + 1)
			sCommandText = "INSERT INTO Tables VALUES('" & AddHighColons(sGroupName) & "',"
			sCommandText += "'" & AddHighColons(sNewTableName) & "',"
			sCommandText += "'" & AddHighColons(Language) & "');"
			DBSaveConnection.ExecuteNonQuery(sCommandText)
		End If

		' Tabellen anlegen
		Dim SaveGroups As New CWordTestGroupCollection(Path)
		SaveGroups.AddExisting(sNewTableName)

		' Anzahl der Datensätze feststellen
		sCommandText = "SELECT COUNT(WordNumber) FROM " & m_sTable & ";"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		oleCursor.Read()
		If TypeOf (oleCursor.GetValue(0)) Is DBNull Then iWords = 0 Else iWords = oleCursor.GetValue(0)

		' Daten kopieren
		Dim i As Integer
		DBSaveConnection.CloseReader()
		If bProgress Then
			ProgressBar.Maximum = iWords
			ProgressBar.Minimum = 0
			ProgressBar.Step = 1
			'ProgressBar.PerformStep()
		End If
		If bLabel Then
			InfoLabel.Text = "Datensätze werden " & Action & "."
			Application.DoEvents()
		End If
		For i = iStart To iWords
			If bLabel Then
				InfoLabel.Text = "Schreibe " & i & " von " & iWords & "..."
			End If
			Application.DoEvents()
			Me.GoToWord(i)			 ' Daten lesen
			InsertWord(DBSaveConnection, sNewTableName)			   ' Daten schreiben
			' Statistik schreiben		leer anlegen, da Benutzer noch nicht eingeführt wurden
			Dim j As Integer
			j = iCurrent
			CreateNewStat(DBSaveConnection, sNewTableName)
			If bProgress Then ProgressBar.PerformStep()
			Application.DoEvents()
		Next i
		If bLabel Then
			InfoLabel.Text = iWords & " Datensätze erfolgreich " & Action & "."
			Application.DoEvents()
			InfoLabel.Text = "Die Unit-Informationen werden " & Action & "..."
			Application.DoEvents()
		End If
		' Sichern der Unit-Namen
		Dim iUnitCount As Integer
		sCommandText = "SELECT COUNT(*) FROM " & m_sTable & "Units;"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		oleCursor.Read()
		If TypeOf (oleCursor.GetValue(0)) Is DBNull Then iUnitCount = 1 Else iUnitCount = oleCursor.GetValue(0)
		sCommandText = "SELECT * FROM " & m_sTable & "Units;"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		Dim iUnitNumber As Integer
		Dim sUnitText As String
		For i = 1 To iUnitCount
			oleCursor.Read()
			If TypeOf (oleCursor.GetValue(0)) Is DBNull Then iUnitNumber = i Else iUnitNumber = oleCursor.GetValue(0)
			If TypeOf (oleCursor.GetValue(0)) Is DBNull Then sUnitText = "" Else sUnitText = oleCursor.GetValue(1)
			sCommandText = "INSERT INTO " & sNewTableName & "Units VALUES(" & iUnitNumber & ", '" & AddHighColons(sUnitText) & "');"
			DBSaveConnection.ExecuteNonQuery(sCommandText)
		Next i


		DBSaveConnection.Close()		 ' Verbindung schließen
		If bLabel Then
			InfoLabel.Text = "Fertig."
			Application.DoEvents()
		End If
		Return SaveErrors.NoError		 ' Beenden. OK
	End Function

	Function DatabaseVersion() As String
		Return sVersionHistory(DatabaseVersionIndex)
	End Function

	Function DatabaseVersion(ByVal Index As Integer) As String
		Return sVersionHistory(Index)
	End Function

	Function DatabaseVersionIndex() As Integer
		' Version Prüfen
		Dim bFound As Boolean = False
		Dim i As Integer = 0
		Do Until bFound = True		  'Or i = sVersionHistory.Length spätestens bei 1.00 ist schluß
			sCommandText = "SELECT Version FROM Version WHERE Version='" & sVersionHistory(i) & "'"
			oleCursor = DBConnection.ExecuteReader(sCommandText)
			oleCursor.Read()
			Try
				If oleCursor.GetValue(0) Then bFound = True Else i += 1
			Catch e As Exception
				i += 1
			End Try
		Loop
		Return i
	End Function

	Function UpdateDatabaseVersion()
		' Die Datenbank auf die neueste Version bringen.
		Dim i As Integer
		Dim iVersion = DatabaseVersionIndex()
		If iVersion = 0 Then Exit Function
		Select Case sVersionHistory(iVersion)
			Case "1.00"			 ' Startversion
				' Einfügen der Versions-Zählung
				sCommandText = "INSERT INTO Version VALUES('1.10', '24.10.2003', 'Versionsinfo')"
			Case "1.10"			 ' Versionsinfo
				' Hinzufügen von Beschreibungen zu den Wörtern
				For i = 0 To Groups.Count - 1
					sCommandText = "ALTER TABLE " & Groups(i).Table & " ADD COLUMN Description TEXT(80);"
					DBConnection.ExecuteNonQuery(sCommandText)
				Next i
				sCommandText = "INSERT INTO Version VALUES('1.11', '25.10.2003', 'Beschreibung')"
			Case "1.11"			 ' Beschreibung
				' Hinzufügen von Lösch-Feldern
				For i = 0 To Groups.Count - 1
					sCommandText = "ALTER TABLE " & Groups(i).Table & " ADD COLUMN Deleted BIT;"
					DBConnection.ExecuteNonQuery(sCommandText)
				Next i
				sCommandText = "INSERT INTO Version VALUES('1.20', '26.10.2003', 'Löschen')"
			Case "1.20"			 ' Löschen
				' Hinzufügen von Stat-Informationen für Hilfe
				For i = 0 To Groups.Count - 1
					sCommandText = "ALTER TABLE " & Groups(i).Table & "Stats ADD COLUMN Hilfe1Richtig INT;"
					DBConnection.ExecuteNonQuery(sCommandText)
					sCommandText = "ALTER TABLE " & Groups(i).Table & "Stats ADD COLUMN Hilfe2Richtig INT;"
					DBConnection.ExecuteNonQuery(sCommandText)
					sCommandText = "ALTER TABLE " & Groups(i).Table & "Stats ADD COLUMN Hilfe3Richtig INT;"
					DBConnection.ExecuteNonQuery(sCommandText)
				Next i
				sCommandText = "INSERT INTO Version VALUES('1.21', '29.02.2004', 'Hilfe')"
			Case "1.21"
				' Hinzufügen von Benutzern
				'sCommandText = "INSERT INTO Version VALUES('2.00', '01.10.2003', 'Benutzer')"
			Case "2.00"			 'Benutzer
				' Aktuelle Version
				'sCommandText = ""
		End Select
		DBConnection.ExecuteNonQuery(sCommandText)
	End Function
#End Region

#Region " Word-Operation Funktionen "
	Overridable Sub NewWord()
		If bConnected = False Then Exit Sub
		If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Sub
		' Eine neue Vokabel anlegen
		' Neue Vokabeln werden automatisch in die letzte gewählte Lektion eingefügt und Kapitel

		' Vokabelnummer feststellen
		Dim iCountWords As Integer = WordsInUnit
		Dim iCountAll As Integer
		sCommandText = "SELECT COUNT(*) FROM " & m_sTable & ";"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		oleCursor.Read()
		If TypeOf (oleCursor.GetValue(0)) Is DBNull Then m_sWord = "" Else iCountAll = oleCursor.GetValue(0)

		' Alle Variablen auf leer setzten
		m_sWord = "NewWord"		  'Vokabel
		m_sPre = ""		 'Pre-Vokabel    (to, le, ...)
		m_sPost = ""		   'Post-Vokabel   (Plural, slang, ...)
		m_sMeaning1 = ""		  'Bedeutung
		m_sMeaning2 = ""		  'Bedeutung
		m_sMeaning3 = ""		  'Bedeutung
		m_sIrregular1 = ""		   'Irregular
		m_sIrregular2 = ""		   'Irregular
		m_sIrregular3 = ""		  'Irregular
		m_sDescription = ""
		m_bDeleted = False
		m_iWordType = 1		   'Vokabelart (Nomen, Verb ...)
		m_iTestType = 1		   'Abfrageart (Random ...)
		m_bMustKnow = True		   'Vokabel muß nicht gewußt werden
		m_bIrregularForm = False		   'Vokabel hat irreguläre Formen, abhängig von Vokabelart
		m_iWordInUnit = iCountWords + 1		  'Neue Nummer erzeugen

		' Zuerst schauen, ob gelöschte Vokabeln vorhanden sind.
		iCurrent = Me.GetDeleted
		If iCurrent <> 0 Then
			sCommandText = "UPDATE " & m_sTable & " SET Deleted=" & False & " WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteNonQuery(sCommandText)
			sCommandText = "UPDATE " & m_sTable & " SET UnitNumber=" & m_iUnit & ", WordInUnit=" & m_iWordInUnit & " WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteNonQuery(sCommandText)
			GoToWord(iCurrent)
			Word = "NewWord"
			Meaning1 = ""
			Meaning2 = ""
			Meaning3 = ""
			Irregular1 = ""
			Irregular2 = ""
			Irregular3 = ""
			Description = ""
			MustKnow = True
			WordType = 1
			IrregularForm = False
		Else
			' Datensatz einfügen
			iCurrent = iCountAll + 1
			InsertWord(DBConnection, m_sTable)
			CreateNewStat(DBConnection, m_sTable)
		End If
	End Sub

	Overridable Sub NewWord(ByVal Unit As Integer)
		m_iUnit = Unit
		NewWord()
	End Sub

	Sub Delete()
		If bConnected = False Then Exit Sub
		If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Sub

		' Aktuelle Vokabel auf "Deletet" setzen
		sCommandText = "UPDATE " & m_sTable & " SET Deleted=" & True & " WHERE WordNumber=" & iCurrent & ";"
		DBConnection.ExecuteNonQuery(sCommandText)

		' Nachfolgende Vokabeln in derselben Lektion eine Nummer heraufsetzen
		'GoToWord(iCurrent)	' Überflüssig
		Dim i As Integer
		' TO DO !!!
		For i = m_iWordInUnit + 1 To WordsInUnit + 1		' Da vorher schon einer auf Deleted gesetzt wurde, um eins erhöhen
			sCommandText = "UPDATE " & m_sTable & " SET WordInUnit=" & i - 1 & " WHERE WordInUnit=" & i & ";"
			DBConnection.ExecuteNonQuery(sCommandText)
		Next i
	End Sub

	Overridable Sub GoToWord(ByVal WordNumber As Int32)
		If bConnected = False Then Exit Sub
		If m_bNoSpecialMode = True Then Exit Sub
		iCurrent = WordNumber
		bNew = False

		sCommandText = "SELECT Word, Meaning1, Meaning2, Meaning3, Pre, Post, Description FROM " & m_sTable & " WHERE WordNumber=" & iCurrent & ";"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		oleCursor.Read()
		If TypeOf (oleCursor.GetValue(0)) Is DBNull Then m_sWord = "" Else m_sWord = oleCursor.GetValue(0)
		If TypeOf (oleCursor.GetValue(1)) Is DBNull Then m_sMeaning1 = "" Else m_sMeaning1 = oleCursor.GetValue(1)
		If TypeOf (oleCursor.GetValue(2)) Is DBNull Then m_sMeaning2 = "" Else m_sMeaning2 = oleCursor.GetValue(2)
		If TypeOf (oleCursor.GetValue(3)) Is DBNull Then m_sMeaning3 = "" Else m_sMeaning3 = oleCursor.GetValue(3)
		If TypeOf (oleCursor.GetValue(4)) Is DBNull Then m_sPre = "" Else m_sPre = oleCursor.GetValue(4)
		If TypeOf (oleCursor.GetValue(5)) Is DBNull Then m_sPost = "" Else m_sPost = oleCursor.GetValue(5)
		If TypeOf (oleCursor.GetValue(6)) Is DBNull Then m_sDescription = "" Else m_sDescription = oleCursor.GetValue(6)

		sCommandText = "SELECT WordType, MustKnow, IrregularForm FROM " & m_sTable & " WHERE WordNumber=" & iCurrent & ";"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		oleCursor.Read()
		If TypeOf (oleCursor.GetValue(0)) Is DBNull Then m_iWordType = 0 Else m_iWordType = oleCursor.GetValue(0)
		If TypeOf (oleCursor.GetValue(1)) Is DBNull Then m_bMustKnow = False Else m_bMustKnow = oleCursor.GetBoolean(1)
		If TypeOf (oleCursor.GetValue(2)) Is DBNull Then m_bIrregularForm = False Else m_bIrregularForm = oleCursor.GetBoolean(2)

		If m_bIrregularForm Then
			sCommandText = "SELECT Irregular1, Irregular2, Irregular3 FROM " & m_sTable & " WHERE WordNumber=" & iCurrent & ";"
			oleCursor = DBConnection.ExecuteReader(sCommandText)
			oleCursor.Read()
			If TypeOf (oleCursor.GetValue(0)) Is DBNull Then m_sIrregular1 = "" Else m_sIrregular1 = oleCursor.GetValue(0)
			If TypeOf (oleCursor.GetValue(1)) Is DBNull Then m_sIrregular2 = "" Else m_sIrregular2 = oleCursor.GetValue(1)
			If TypeOf (oleCursor.GetValue(2)) Is DBNull Then m_sIrregular3 = "" Else m_sIrregular3 = oleCursor.GetValue(2)
		Else
			m_sIrregular1 = ""
			m_sIrregular2 = ""
			m_sIrregular3 = ""
		End If

		sCommandText = "SELECT UnitNumber, ChapterNumber, WordInUnit FROM " & m_sTable & " WHERE WordNumber=" & iCurrent & ";"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		oleCursor.Read()
		If TypeOf (oleCursor.GetValue(0)) Is DBNull Then m_iUnit = -1 Else m_iUnit = oleCursor.GetValue(0)
		If TypeOf (oleCursor.GetValue(1)) Is DBNull Then m_iChapter = -1 Else m_iChapter = oleCursor.GetValue(1)
		If TypeOf (oleCursor.GetValue(2)) Is DBNull Then m_iWordInUnit = -1 Else m_iWordInUnit = oleCursor.GetValue(2)
		m_sUnit = GetUnit(m_iUnit)

		sCommandText = "SELECT Abfragen, AbfragenGesamt, Richtig, Falsch, FalschGesamt, AbfrageGestartet, ErsteAbfrage, LetzteAbfrage FROM " & m_sTable & "Stats WHERE WordNumber=" & iCurrent & ";"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		oleCursor.Read()
		If TypeOf (oleCursor.GetValue(7)) Is DBNull Then m_sLastTested = "01.01.1900" Else m_sLastTested = oleCursor.GetValue(7)
		m_sUnit = GetUnit(m_iUnit)

	End Sub

	Protected Function InsertWord(ByRef Connection As CDBOperation, ByVal Tablename As String)
		sCommandText = "INSERT INTO " & Tablename & " VALUES ("
		sCommandText += AddHighColons(m_iUnit) & ","
		sCommandText += AddHighColons(m_iChapter) & ","
		sCommandText += "'" & AddHighColons(m_sWord) & "',"
		sCommandText += AddHighColons(iCurrent) & ","
		sCommandText += AddHighColons(m_iWordInUnit) & ","
		sCommandText += AddHighColons(m_iWordType) & ","
		sCommandText += AddHighColons(m_bMustKnow) & ","
		sCommandText += "'" & AddHighColons(m_sPre) & "',"
		sCommandText += "'" & AddHighColons(m_sPost) & "',"
		sCommandText += "'" & AddHighColons(m_sMeaning1) & "',"
		sCommandText += "'" & AddHighColons(m_sMeaning2) & "',"
		sCommandText += "'" & AddHighColons(m_sMeaning3) & "',"
		sCommandText += AddHighColons(m_bIrregularForm) & ","
		sCommandText += "'" & AddHighColons(m_sIrregular1) & "',"
		sCommandText += "'" & AddHighColons(m_sIrregular2) & "',"
		sCommandText += "'" & AddHighColons(m_sIrregular3) & "',"
		sCommandText += "'" & AddHighColons(m_sDescription) & "',"
		sCommandText += AddHighColons(m_bDeleted) & ");"		  ' Description + Deleted
		Connection.ExecuteNonQuery(sCommandText)
	End Function

	Protected Function CreateNewStat(ByRef Connection As CDBOperation, ByVal Tablename As String)
		Dim sInput As String
		sInput = "INSERT INTO " & Tablename & "Stats VALUES ("
		sInput += AddHighColons(iCurrent) & ","
		sInput += AddHighColons(0) & ","
		sInput += AddHighColons(0) & ","
		sInput += AddHighColons(0) & ","
		sInput += AddHighColons(0) & ","
		sInput += AddHighColons(0) & ","
		sInput += "'" & AddHighColons("01.01.1900") & "',"
		sInput += "'" & AddHighColons("01.01.1900") & "',"
		sInput += AddHighColons(False) & ","
		sInput &= AddHighColons(0) & ","
		sInput &= AddHighColons(0) & ","
		sInput &= AddHighColons(0) & ");"
		Connection.ExecuteReader(sInput)
	End Function

	Protected Function SaveStats()

	End Function

	Protected Function ExistDeleted() As Boolean
		sCommandText = "SELECT COUNT(Deleted) FROM " & m_sTable & " WHERE Deleted=" & True & ";"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		oleCursor.Read()
		Dim iCount As Integer
		If TypeOf (oleCursor.GetValue(0)) Is DBNull Then iCount = 0 Else iCount = oleCursor.GetValue(0)
		If iCount > 0 Then Return True Else Return False
	End Function

	Protected Function GetDeleted() As Integer
		If Not ExistDeleted() Then
			Return 0
		Else
			sCommandText = "SELECT WordNumber FROM " & m_sTable & " WHERE Deleted=" & True & ";"
			oleCursor = DBConnection.ExecuteReader(sCommandText)
			oleCursor.Read()
			If TypeOf (oleCursor.GetValue(0)) Is DBNull Then Return 0 Else Return oleCursor.GetValue(0)
		End If
	End Function
#End Region

#Region " Test-Funktionen "
	Overridable Sub TestInitialize(ByRef TestUnits As Collection, Optional ByVal WordToMeaning As Boolean = False)
		If bConnected = False Then Exit Sub

		m_bTestMode = True
		m_bNoSpecialMode = False
		m_bWordToMeaning = WordToMeaning

		Dim i As Integer
		Dim structWord As TestWord

		cTestUnits = TestUnits
		cWords = New Collection
		For i = 1 To TestUnits.Count
			sCommandText = "SELECT WordNumber FROM " & TestUnits(i).table & " WHERE UnitNumber=" & TestUnits(i).unit & " AND Deleted=" & False & " ORDER BY WordNumber;"
			oleCursor = DBConnection.ExecuteReader(sCommandText)
			structWord.Table = TestUnits(i).table
			Do While oleCursor.Read
				If Not TypeOf (oleCursor.GetValue(0)) Is DBNull Then structWord.WordNumber = oleCursor.GetValue(0) Else structWord.WordNumber = 0
				cWords.Add(structWord)
			Loop
		Next i

		bErneut = False
		m_iTestWordCountAll = cWords.Count
		m_iTestWordCountToDo = cWords.Count
		m_iTestWordCountDone = 0
		m_iTestWordCountDoneRight = 0
		m_iTestWordCountDoneFalse = 0
		m_iTestWordCountDoneFalseAllTrys = 0
		iTestWordCountDoneCorrection = 0
	End Sub

	Overridable Sub TestClose()
		Dim sCommandText = "UPDATE " & m_sTable & "Stats SET AbfrageGestartet=" & False & " WHERE AbfrageGestartet=" & True & ";"
		DBConnection.ExecuteReader(sCommandText)
		m_bTestMode = False
		m_bNoSpecialMode = True
		cTestUnits = Nothing
		cWords = Nothing
		m_iTestWordCountAll = 0
		m_iTestWordCountToDo = 0
		m_iTestWordCountDone = 0
		m_iTestWordCountDoneRight = 0
		m_iTestWordCountDoneFalse = 0
		m_iTestWordCountDoneFalseAllTrys = 0
		iTestWordCountDoneCorrection = 0
	End Sub

	Overridable Sub TestGetNext()
		If bConnected = False Then Exit Sub
		If m_bTestMode = False Then Exit Sub

		If cWords.Count = 0 Then Exit Sub
		Select Case m_iTestNextMode
			Case 0			  ' Der Reihe nach
				m_sTable = cWords(1).table
				GoToWord(cWords(1).wordnumber)
				iTestCurrentWord = 1
			Case 1			  ' Zufällig alle gewählten
				If bErneut = False Then
					Dim iNext As Integer
					Randomize()
					iNext = CInt(Int((cWords.Count * Rnd()) + 1))
					iTestCurrentWord = iNext
					m_sTable = cWords(1).table
					GoToWord(cWords(iNext).wordnumber)
				End If
			Case Else
				MsgBox("Dieser Abfrage-Modus wird zur zeit nicht unterstützt!")
				m_iTestNextMode = 0
		End Select

		m_bWordToMeaning = TestWordToMeaning()
		If m_bWordToMeaning Then
			m_sTestWord = m_sPre & m_sWord & m_sPost
		Else
			m_sTestWord = m_sMeaning1
			If m_sMeaning2 <> "" Then m_sTestWord += ", " & m_sMeaning2
			If m_sMeaning3 <> "" Then m_sTestWord += ", " & m_sMeaning3
		End If
		Me.CreateTypeForms()
	End Sub

	Overridable Function TestControl(Optional ByVal Word As String = "", Optional ByVal Meaning1 As String = "", Optional ByVal Meaning2 As String = "", Optional ByVal Meaning3 As String = "", Optional ByVal Irregular1 As String = "", Optional ByVal Irregular2 As String = "", Optional ByVal Irregular3 As String = "") As Boolean
		If bConnected = False Then Exit Function
		If (Not m_bTestMode) Or m_bNoSpecialMode Then Exit Function

		Dim bRight As Boolean
		bRight = False
		bRight = CheckWord(Meaning1, Meaning2, Meaning3)
		If ((m_iIrregular = IrregularTest.Always) Or (m_iIrregular = IrregularTest.IrregularOnly And m_bIrregularForm)) Then
			If Irregular1 <> m_sIrregular1 Then bRight = False
			If Irregular2 <> m_sIrregular2 Then bRight = False
			If Irregular3 <> m_sIrregular3 Then bRight = False
		End If
		UpdateStats(bRight)
		Return bRight
	End Function

	Protected Sub UpdateStats(ByVal Right As Boolean)
		'**********************************
		'* Aktualisierung der Statistiken *
		'**********************************
		Dim iTests, iTestsAll, iRight, iWrong, iWrongAll As Integer
		Dim iHelp1, iHelp2, iHelp3 As Integer
		Dim sFirst As String
		Dim bTestStart As Boolean
		Dim bFirstTry As Boolean

		DBConnection.Open(m_sPath)
		sCommandText = "SELECT Abfragen, AbfragenGesamt, Richtig, Falsch, FalschGesamt, AbfrageGestartet, ErsteAbfrage, LetzteAbfrage FROM " & m_sTable & "Stats WHERE WordNumber=" & iCurrent & ";"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		oleCursor.Read()
		If TypeOf (oleCursor.GetValue(0)) Is DBNull Then iTests = 0 Else iTests = oleCursor.GetValue(0)
		If TypeOf (oleCursor.GetValue(1)) Is DBNull Then iTestsAll = 0 Else iTestsAll = oleCursor.GetValue(1)
		If TypeOf (oleCursor.GetValue(2)) Is DBNull Then iRight = 0 Else iRight = oleCursor.GetValue(2)
		If TypeOf (oleCursor.GetValue(3)) Is DBNull Then iWrong = 0 Else iWrong = oleCursor.GetValue(3)
		If TypeOf (oleCursor.GetValue(4)) Is DBNull Then iWrongAll = 0 Else iWrongAll = oleCursor.GetValue(4)
		If TypeOf (oleCursor.GetValue(5)) Is DBNull Then bTestStart = False Else bTestStart = oleCursor.GetBoolean(5)
		If TypeOf (oleCursor.GetValue(6)) Is DBNull Then sFirst = "01.01.1900" Else sFirst = oleCursor.GetDateTime(6)
		sCommandText = "SELECT Hilfe1Richtig, Hilfe2Richtig, Hilfe3Richtig FROM " & m_sTable & "Stats WHERE WordNumber=" & iCurrent & ";"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		oleCursor.Read()
		If TypeOf (oleCursor.GetValue(0)) Is DBNull Then iHelp1 = 0 Else iHelp1 = oleCursor.GetValue(0)
		If TypeOf (oleCursor.GetValue(1)) Is DBNull Then iHelp2 = 0 Else iHelp2 = oleCursor.GetValue(1)
		If TypeOf (oleCursor.GetValue(2)) Is DBNull Then iHelp3 = 0 Else iHelp3 = oleCursor.GetValue(2)
		oleCursor.Close()

		m_sLastTested = Format(Now, "dd.MM.yyyy")
		If sFirst = "01.01.1900" Then
			sFirst = m_sLastTested
			If m_bFirstTry = True Then
				bFirstTry = True
				sCommandText = "UPDATE " & m_sTable & "Stats SET ErsteAbfrage='" & sFirst & "', LetzteAbfrage='" & m_sLastTested & "' WHERE WordNumber=" & iCurrent & ";"
				DBConnection.ExecuteNonQuery(sCommandText)
			Else
				bFirstTry = False
			End If
		Else
			bFirstTry = False
		End If
		If Right = True Then		  ' richtige Antwort
			If bTestStart = False Then
				Select Case m_iHelpMode				' Test ob Hilfe benutzt wurde
					Case HelpModes.NoHelp
						m_iTestWordCountDoneRight += 1
						iRight += 1
					Case HelpModes.LightHelp
						m_iTestWordCountHelp1 += 1
						iHelp1 += 1
					Case HelpModes.MiddleHelp
						m_iTestWordCountHelp2 += 1
						iHelp2 += 1
					Case HelpModes.HeavyHelp
						m_iTestWordCountHelp3 += 1
						iHelp3 += 1
				End Select
				iTests += 1
				iTestsAll += 1
			Else
				iTestsAll += 1
			End If
			bTestStart = False
			cWords.Remove(iTestCurrentWord)
			bErneut = False
		Else		  ' falsche antwort
			m_iTestWordCountDoneFalseAllTrys += 1
			If bTestStart = False Then
				m_iTestWordCountDoneFalse += 1
				iTests += 1
				iTestsAll += 1
				iWrong += 1
				iWrongAll += 1
			Else
				iTestsAll += 1
				iWrongAll += 1
			End If
			Select Case m_iTestNextModeWrong			 ' Eventuelle Wort-Neu-Abfrage Testen:
				Case 0				' Fehlerhafte sofort abfragen bis Korrekt
					bTestStart = True
					bErneut = True
				Case 1				'Fehlerhafte sofort erneut abfragen
					If bErneut = False Then
						bErneut = True
						bTestStart = True
					Else
						cWords.Remove(iTestCurrentWord)
						bErneut = False
						bTestStart = False
					End If
				Case 2				'Fehlerhafte abfragen bis Korrekt, in Liste einfügen
					bTestStart = True
					Dim structWord As TestWord
					structWord.Table = m_sTable
					structWord.WordNumber = iCurrent
					cWords.Remove(iTestCurrentWord)
					cWords.Add(structWord)
				Case 3				' Fehlerhafte erneut abfragen, in Liste einfügen
					If bTestStart = True Then
						bTestStart = False
						cWords.Remove(iTestCurrentWord)
					Else
						bTestStart = True
						Dim structWord As TestWord
						structWord.Table = m_sTable
						structWord.WordNumber = iCurrent
						cWords.Remove(iTestCurrentWord)
						cWords.Add(structWord)
					End If
				Case 4				 ' Fehlerhafte abfragen bis Korrekt, neue Liste am Ende
					iTestWordCountDoneCorrection += 1
					bTestStart = True
					cWords.Remove(iTestCurrentWord)
				Case 5				 ' Fehlerhafte erneut abfragen, neue Liste am Ende
					If bTestStart = True Then
						bTestStart = False
					Else
						iTestWordCountDoneCorrection += 1
						bTestStart = True
					End If
					cWords.Remove(iTestCurrentWord)
				Case 6
					bTestStart = False
					cWords.Remove(iTestCurrentWord)
				Case Else
					MsgBox("Dieser Falsche-Vokabel-Modus wird leider nicht unterstützt!")
			End Select
		End If
		If Not bFirstTry Then
			sCommandText = "UPDATE " & m_sTable & "Stats SET Abfragen=" & iTests & ", AbfragenGesamt=" & iTestsAll & ", Richtig=" & iRight & ", Falsch=" & iWrong & ", FalschGesamt=" & iWrongAll & ", AbfrageGestartet=" & bTestStart & ", ErsteAbfrage='" & sFirst & "', LetzteAbfrage='" & m_sLastTested & "' WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteNonQuery(sCommandText)
			sCommandText = "UPDATE " & m_sTable & "Stats SET Hilfe1Richtig=" & iHelp1 & ", Hilfe2Richtig=" & iHelp2 & ", Hilfe3Richtig=" & iHelp3 & " WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteNonQuery(sCommandText)
		Else
			sCommandText = "UPDATE " & m_sTable & "Stats SET AbfrageGestartet=" & bTestStart & ", ErsteAbfrage='" & sFirst & "', LetzteAbfrage='" & m_sLastTested & "' WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteNonQuery(sCommandText)
		End If

		If cWords.Count = 0 Then		  ' Liste Leer, füllen mit den noch nicht beendeten
			Dim i As Integer
			Dim structWord As TestWord

			For i = 1 To cTestUnits.Count
				sCommandText = "SELECT WordNumber FROM " & cTestUnits(i).table & "Stats WHERE AbfrageGestartet=" & True & " ORDER BY WordNumber;"
				oleCursor = DBConnection.ExecuteReader(sCommandText)
				structWord.Table = cTestUnits(i).table
				Do While oleCursor.Read
					If Not TypeOf (oleCursor.GetValue(0)) Is DBNull Then structWord.WordNumber = oleCursor.GetValue(0) Else structWord.WordNumber = 0
					cWords.Add(structWord)
				Loop
				oleCursor.Close()
			Next
			iTestWordCountDoneCorrection = 0
			bErneut = False
		End If

		m_iTestWordCountToDo = cWords.Count + iTestWordCountDoneCorrection
		m_iTestWordCountDone = m_iTestWordCountAll - m_iTestWordCountToDo
	End Sub

	Shared ReadOnly Property NextWordModes() As ArrayList
		Get
			Dim asList As New ArrayList
			asList.Add("Nacheinander")			  ' 0
			asList.Add("Zufällig")			   ' 1
			'asList.Add("Nacheinander, zufällige Lektionen")     ' 2
			'asList.Add("Zufällig in Lektionen")                 ' 3
			'asList.Add("Zufällig in Sprachen")                  ' 4
			'asList.Add("Zufällig in Sprachen und Lektionen")    ' 5
			Return asList
		End Get
	End Property

	Shared ReadOnly Property NextWordModesWrong() As ArrayList
		Get
			Dim aslist As New ArrayList
			aslist.Add("Fehlerhafte sofort abfragen bis korrekt")			   ' 0     d
			aslist.Add("Fehlerhafte sofort erneut abfragen")			  ' 1     d
			aslist.Add("Fehlerhafte abfragen bis korrekt, in Liste einfügen")			' 2     d
			aslist.Add("Fehlerhafte erneut abfragen, in Liste einfügen")			  ' 3     d
			aslist.Add("Fehlerhafte abfragen bis korrekt, neue Liste am Ende")			  ' 4     ia
			aslist.Add("Fehlerhafte erneut abfragen, neue Liste am Ende")			 ' 5     ia
			aslist.Add("Fehlerhafte nicht nochmal abfragen")			  ' 6     d
			Return aslist
		End Get
	End Property

	Shared ReadOnly Property IrregularTestModes() As ArrayList
		Get
			Dim asList As New ArrayList
			asList.Add("immer abfragen")
			asList.Add("nie abfragen")
			asList.Add("bei irregulären abfragen")
			Return asList
		End Get
	End Property

	Property NextWordMode() As Integer
		Get
			Return m_iTestNextMode
		End Get
		Set(ByVal Value As Integer)
			m_iTestNextMode = Value
		End Set
	End Property

	Property NextWordModeWrong() As Integer
		Get
			Return m_iTestNextModeWrong
		End Get
		Set(ByVal Value As Integer)
			m_iTestNextModeWrong = Value
		End Set
	End Property

	Property IrregularTestMode() As IrregularTest
		Get
			Return m_iIrregular
		End Get
		Set(ByVal Value As IrregularTest)
			m_iIrregular = Value
		End Set
	End Property

	ReadOnly Property TypeText(ByVal TypeNumber) As String
		Get
			If bConnected = False Then Exit Property
			Dim sList As New ArrayList
			sList = Types()
			Return sList(TypeNumber)
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
				Case Else
					Dim sList As New ArrayList
					sList.Add("Einfach")					  '0
					Return sList
			End Select
		End Get
	End Property

	Protected Sub CreateTypeForms()
		If bConnected = False Then Exit Sub
		If m_bTestMode = False Then Exit Sub

		Select Case Language()
			Case "French"
				CreateFrenchTypeForms()
			Case "English"
				CreateEnglishTypeForms()
			Case "Latin"
				CreateLatinTypeForms()
			Case Else
				' Nichts
		End Select
	End Sub

	ReadOnly Property TestWord() As String
		Get
			Return m_sTestWord
		End Get
	End Property

	Property TestWordMode() As TestWordModes
		Get
			Return m_iTestWordMode
		End Get
		Set(ByVal Value As TestWordModes)
			m_iTestWordMode = Value
		End Set
	End Property

	Protected Function TestWordToMeaning() As Boolean
		Select Case m_iTestWordMode
			Case TestWordModes.LanguageDefault
				Select Case Language()
					Case "Latin"
						Return True
					Case "English"
						Return False
					Case "French"
						Return False
					Case Else
						Return False
				End Select
			Case TestWordModes.TestMeaning
				Return True
			Case TestWordModes.TestWord
				Return False
		End Select
	End Function

	ReadOnly Property TestAnswer1() As String
		Get
			If m_bWordToMeaning Then
				Return m_sMeaning1
			Else
				Return m_sPre & " " & m_sWord & " " & m_sPost
			End If
		End Get
	End Property

	ReadOnly Property TestAnswer2() As String
		Get
			If m_bWordToMeaning Then
				Return m_sMeaning2
			Else
				Return ""
			End If
		End Get
	End Property

	ReadOnly Property TestAnswer3() As String
		Get
			If m_bWordToMeaning Then
				Return m_sMeaning3
			Else
				Return ""
			End If
		End Get
	End Property

	ReadOnly Property TestGrammar1() As String
		Get
			If m_iIrregular = IrregularTest.Never Then Return ""
			If (m_iIrregular = IrregularTest.IrregularOnly) And (m_bIrregularForm) Then Return Irregular1
			If m_iIrregular = IrregularTest.Always Then Return Irregular1
			Return ""
		End Get
	End Property

	ReadOnly Property TestGrammar2() As String
		Get
			If m_iIrregular = IrregularTest.Never Then Return ""
			If (m_iIrregular = IrregularTest.IrregularOnly) And (m_bIrregularForm) Then Return Irregular2
			If m_iIrregular = IrregularTest.Always Then Return Irregular2
			Return ""
		End Get
	End Property

	ReadOnly Property TestGrammar3() As String
		Get
			If m_iIrregular = IrregularTest.Never Then Return ""
			If (m_iIrregular = IrregularTest.IrregularOnly) And (m_bIrregularForm) Then Return Irregular3
			If m_iIrregular = IrregularTest.Always Then Return Irregular3
			Return ""
		End Get
	End Property

	ReadOnly Property TestWordCount() As Integer
		Get
			Return cWords.Count
		End Get
	End Property

	ReadOnly Property TestWordCountAll() As Integer
		Get
			Return m_iTestWordCountAll
		End Get
	End Property

	ReadOnly Property TestWordCountToDo() As Integer
		Get
			Return m_iTestWordCountToDo
		End Get
	End Property

	ReadOnly Property TestWordCountDone() As Integer
		Get
			Return m_iTestWordCountDone
		End Get
	End Property

	ReadOnly Property TestWordCountDoneRight() As Integer
		Get
			Return m_iTestWordCountDoneRight
		End Get
	End Property

	ReadOnly Property TestWordCountDoneFalse() As Integer
		Get
			Return m_iTestWordCountDoneFalse
		End Get
	End Property

	ReadOnly Property TestWordCountDoneFAlseAllTrys() As Integer
		Get
			Return m_iTestWordCountDoneFalseAllTrys
		End Get
	End Property

	ReadOnly Property LastTested() As String
		Get
			Return m_sLastTested
		End Get
	End Property

	Property FirstTry() As Boolean
		Get
			Return m_bFirstTry
		End Get
		Set(ByVal Value As Boolean)
			m_bFirstTry = Value
		End Set
	End Property

	Function IrregularDescription() As Collection
		Dim cList As Collection
		Select Case Language()
			Case "French"
				Return IrregularDescriptionFrench()
			Case "English"
				Return IrregularDescriptionEnglish()
			Case "Latin"
				Return IrregularDescriptionLatin()
			Case Else
				cList.Add("")
				cList.Add("")
				cList.Add("")
		End Select
	End Function

	Property HelpMode() As HelpModes
		Get
			Return m_iHelpMode
		End Get
		Set(ByVal Value As HelpModes)
			m_iHelpMode = Value
		End Set
	End Property

	ReadOnly Property TestWordCountDoneWithHelpAll()
		Get
			Return m_iTestWordCountHelp1 + m_iTestWordCountHelp2 + m_iTestWordCountHelp3
		End Get
	End Property

	ReadOnly Property TestWordCountDoneWithHelp1()
		Get
			Return m_iTestWordCountHelp1
		End Get
	End Property

	ReadOnly Property TestWordCountDoneWithHelp2()
		Get
			Return m_iTestWordCountHelp2
		End Get
	End Property

	ReadOnly Property TestWordCountDoneWithHelp3()
		Get
			Return m_iTestWordCountHelp3
		End Get
	End Property

	Protected Function CheckWord(ByVal Meaning1 As String, ByVal Meaning2 As String, ByVal Meaning3 As String)
		If bConnected = False Then Exit Function
		If m_bTestMode = False Then Exit Function
		Select Case Language()
			Case "French"
				Return CheckFrenchWord(Meaning1, meaning2, Meaning3)
			Case "English"
				Return CheckEnglishWord(Meaning1, meaning2, Meaning3)
			Case "Latin"
				Return CheckLatinWord(Meaning1, meaning2, Meaning3)
			Case Else
				If m_bWordToMeaning Then
					If (Meaning1 = m_sMeaning1) And (meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning3) Then Return True
					If (Meaning1 = m_sMeaning1) And (meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning2) Then Return True
					If (Meaning1 = m_sMeaning2) And (meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning3) Then Return True
					If (Meaning1 = m_sMeaning2) And (meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning1) Then Return True
					If (Meaning1 = m_sMeaning3) And (meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning1) Then Return True
					If (Meaning1 = m_sMeaning3) And (meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning2) Then Return True
				Else
					If Meaning1 = m_sWord Then Return True
					Return False
				End If
		End Select
	End Function
#End Region

#Region " Unit-Funktionen "
	Overridable Function GetUnits() As Collection
		If bConnected = False Then Exit Function
		'If m_bTestMode Or m_bNoSpecialMode Then Exit Function


		Dim cList As New Collection
		Dim cTemp As Collection

		sCommandText = "SELECT DISTINCT Nummer, Name FROM " & m_sTable & "Units ORDER BY Nummer;"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		Do While oleCursor.Read
			cTemp = New Collection			   ' Create new Collection-in-Collection
			cTemp.Add(oleCursor.GetInt32(0))			  ' Add number of unit to Collection-in-Collection
			cTemp.Add(oleCursor.GetString(1))			 ' Add name of unit to Collection-in-Collection
			cList.Add(cTemp)			   ' Add Collection to Collection
		Loop


		Return cList
	End Function

	Overridable Function GetUnit(ByVal Number As Integer) As String
		If bConnected = False Then Exit Function
		'If m_bTestMode Or m_bNoSpecialMode Then Exit Function

		Dim sTemp As String

		sCommandText = "SELECT Name FROM " & m_sTable & "Units WHERE Nummer=" & Number & ";"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		If oleCursor.Read Then sTemp = oleCursor.GetString(0) Else sTemp = ""

		Return sTemp
	End Function

	Overridable Function GetUnitNumber(ByVal Name As String) As Integer
		If bConnected = False Then Exit Function
		'If m_bTestMode Or m_bNoSpecialMode Then Exit Function
		Dim iTemp As Integer

		sCommandText = "SELECT Nummer FROM " & m_sTable & "Units WHERE Name='" & AddHighColons(Name) & "';"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		If oleCursor.Read Then iTemp = oleCursor.GetInt32(0) Else iTemp = 0

		Return iTemp
	End Function

	Overridable Function GetWordsInUnit(ByVal UnitNumber As Int32) As Collection
		If bConnected = False Then Exit Function
		If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Function

		Dim cList As New Collection
		Dim cTemp As Collection

		sCommandText = "SELECT Word, WordNumber, WordInUnit FROM " & m_sTable & " WHERE UnitNumber=" & UnitNumber & " AND Deleted=" & False & " ORDER BY WordInUnit ;"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		Do While oleCursor.Read
			cTemp = New Collection			   ' Create new Collection-in-Collection
			cTemp.Add(oleCursor.GetInt32(2))			  ' Add number of word in this unit to Collection-in-Collection
			cTemp.Add(oleCursor.GetString(0))			 ' Add word to Collection-in-Collection
			cTemp.Add(oleCursor.GetInt32(1))			  ' Add number of word to Collection-in-Collection
			cList.Add(cTemp)			   ' Add Collection to Collection
		Loop

		Return cList
	End Function

	Overridable Function UnitAdd(ByVal UnitName As String)
		If bConnected = False Then Exit Function
		If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Function

		Dim iCount As Integer
		sCommandText = "SELECT COUNT(Nummer) FROM " & m_sTable & "Units"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		oleCursor.Read()
		If TypeOf (oleCursor.GetValue(0)) Is DBNull Then iCount = 0 Else iCount = oleCursor.GetValue(0)

		sCommandText = "INSERT INTO " & m_sTable & "Units VALUES (" & iCount + 1 & ", '" & AddHighColons(UnitName) & "')"
		DBConnection.ExecuteReader(sCommandText)
	End Function

	Overridable Function UnitEdit(ByVal Name As String, ByVal Unit As Integer)
		If bConnected = False Then Exit Function
		If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Function

		sCommandText = "UPDATE " & m_sTable & "Units SET Name='" & Name & "' WHERE Nummer=" & Unit & ";"
		DBConnection.ExecuteNonQuery(sCommandText)
	End Function

	Overridable Function UnitEdit(ByVal Name As String, ByVal Unit As String)
		If bConnected = False Then Exit Function
		If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Function

		sCommandText = "UPDATE " & m_sTable & "Units SET Name='" & Name & "' WHERE Nummer=" & Unit & ";"
		DBConnection.ExecuteNonQuery(sCommandText)
	End Function

	ReadOnly Property WordsInUnit()
		Get
			Dim iCountwords As Integer
			sCommandText = "SELECT COUNT(*) FROM " & m_sTable & " WHERE UnitNumber=" & m_iUnit & " AND Deleted=" & False & ";"
			oleCursor = DBConnection.ExecuteReader(sCommandText)
			oleCursor.Read()
			If TypeOf (oleCursor.GetValue(0)) Is DBNull Then m_sWord = "" Else iCountwords = oleCursor.GetValue(0)
			oleCursor.Close()
			Return iCountwords
		End Get
	End Property
#End Region

#Region " Wort-Informationen "
	Property Word() As String
		Get
			Return m_sWord
		End Get
		Set(ByVal Word As String)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property
			m_sWord = Word
			sCommandText = "UPDATE " & m_sTable & " SET Word='" & AddHighColons(m_sWord) & "' WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteReader(sCommandText)
		End Set
	End Property

	Property Pre() As String
		Get
			Return m_sPre
		End Get
		Set(ByVal Pre As String)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property
			m_sPre = Pre
			sCommandText = "UPDATE " & m_sTable & " SET Pre='" & AddHighColons(m_sPre) & "' WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteReader(sCommandText)
		End Set
	End Property

	Property Post() As String
		Get
			Return m_sPost
		End Get
		Set(ByVal Post As String)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property
			m_sPost = Post
			sCommandText = "UPDATE " & m_sTable & " SET Post='" & AddHighColons(m_sPost) & "' WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteReader(sCommandText)
		End Set
	End Property

	Property Meaning1() As String
		Get
			Return m_sMeaning1
		End Get
		Set(ByVal Meaning As String)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property
			m_sMeaning1 = Meaning

			sCommandText = "UPDATE " & m_sTable & " SET Meaning1='" & AddHighColons(m_sMeaning1) & "' WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteReader(sCommandText)
		End Set
	End Property

	Property Meaning2() As String
		Get
			Return m_sMeaning2
		End Get
		Set(ByVal Meaning As String)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property
			m_sMeaning2 = Meaning
			sCommandText = "UPDATE " & m_sTable & " SET Meaning2='" & AddHighColons(m_sMeaning2) & "' WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteReader(sCommandText)
		End Set
	End Property

	Property Meaning3() As String
		Get
			Return m_sMeaning3
		End Get
		Set(ByVal Meaning As String)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property
			m_sMeaning3 = Meaning
			sCommandText = "UPDATE " & m_sTable & " SET Meaning3='" & AddHighColons(m_sMeaning3) & "' WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteReader(sCommandText)
		End Set
	End Property

	Property IrregularForm() As Boolean
		Get
			Return m_bIrregularForm
		End Get
		Set(ByVal Irregular As Boolean)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property
			m_bIrregularForm = Irregular
			If Irregular = False Then
				m_sIrregular1 = ""
				sCommandText = "UPDATE " & m_sTable & " SET Irregular1='" & AddHighColons(m_sIrregular1) & "' WHERE WordNumber=" & iCurrent & ";"
				DBConnection.ExecuteReader(sCommandText)
				m_sIrregular2 = ""
				sCommandText = "UPDATE " & m_sTable & " SET Irregular2='" & AddHighColons(m_sIrregular1) & "' WHERE WordNumber=" & iCurrent & ";"
				DBConnection.ExecuteReader(sCommandText)
				m_sIrregular3 = ""
				sCommandText = "UPDATE " & m_sTable & " SET Irregular2='" & AddHighColons(m_sIrregular1) & "' WHERE WordNumber=" & iCurrent & ";"
				DBConnection.ExecuteReader(sCommandText)
			End If
			sCommandText = "UPDATE " & m_sTable & " SET IrregularForm=" & m_bIrregularForm & " WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteReader(sCommandText)
		End Set
	End Property

	Property Irregular1() As String
		Get
			Return m_sIrregular1
		End Get
		Set(ByVal Irregular As String)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property
			m_sIrregular1 = Irregular
			sCommandText = "UPDATE " & m_sTable & " SET Irregular1='" & AddHighColons(m_sIrregular1) & "' WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteReader(sCommandText)
		End Set
	End Property

	Property Irregular2() As String
		Get
			Return m_sIrregular2
		End Get
		Set(ByVal Irregular As String)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property
			m_sIrregular2 = Irregular
			sCommandText = "UPDATE " & m_sTable & " SET Irregular2='" & AddHighColons(m_sIrregular2) & "' WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteReader(sCommandText)

		End Set
	End Property

	Property Irregular3() As String
		Get
			Return m_sIrregular3
		End Get
		Set(ByVal Irregular As String)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property
			m_sIrregular3 = Irregular
			sCommandText = "UPDATE " & m_sTable & " SET Irregular3='" & AddHighColons(m_sIrregular3) & "' WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteReader(sCommandText)

		End Set
	End Property

	Property Description() As String
		Get
			Return m_sDescription
		End Get
		Set(ByVal description As String)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property
			m_sDescription = description
			sCommandText = "UPDATE " & m_sTable & " SET Description='" & AddHighColons(m_sDescription) & "' WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteReader(sCommandText)

		End Set
	End Property
#End Region

#Region " Zusätzliche Wort-Informationen "
	ReadOnly Property Language() As String
		Get
			If bConnected = False Then Exit Property
			If Trim(m_sTable) = "" Then Exit Property

			Dim sLanguage As String

			sCommandText = "SELECT Art FROM Tables WHERE Tabelle='" & m_sTable & "';"
			oleCursor = DBConnection.ExecuteReader(sCommandText)
			oleCursor.Read()
			If TypeOf (oleCursor.GetValue(0)) Is DBNull Then sLanguage = "" Else sLanguage = oleCursor.GetValue(0)


			Return sLanguage
		End Get
	End Property

	Property WordType() As Integer
		Get
			Return m_iWordType
		End Get
		Set(ByVal Value As Integer)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property
			m_iWordType = Value
			sCommandText = "UPDATE " & m_sTable & " SET WordType=" & m_iWordType & " WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteReader(sCommandText)
		End Set
	End Property

	Property TestType() As Integer
		Get
			Return m_iTestType
		End Get
		Set(ByVal Value As Integer)
			If (m_bTestMode = False) Or (m_bNoSpecialMode = True) Then Exit Property
			m_iTestType = Value
		End Set
	End Property

	Property UnitName() As String
		Get
			Return m_sUnit
		End Get
		Set(ByVal Unit As String)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property

			' Zur neuen Unit die Number feststellen
			Dim iNumber As Integer

			iNumber = GetUnitNumber(Unit)
			If iNumber <= 0 Then MsgBox("Fehler! UnitNumber zur neuen Unit ist falsch!!!")

			' Aus alter Unit die NumberInUnit-Werte der anderen Vokabeln ändern
			' Daten hohlen
			Dim aTemp As New ArrayList, iWordInUnit As Integer
			sCommandText = "SELECT WordInUnit, WordNumber FROM " & m_sTable & " WHERE UnitNumber=" & m_iUnit & ";"
			oleCursor = DBConnection.ExecuteReader(sCommandText)
			Do While oleCursor.Read
				iWordInUnit = oleCursor.GetValue(0)
				If iWordInUnit > m_iWordInUnit Then
					aTemp.Add(oleCursor.GetValue(1))					   ' Add WordNumber to Arraylist
					aTemp.Add(iWordInUnit)					   ' Add WordInUnit to Arraylist
				End If
			Loop

			' Daten ändern
			Dim i As Integer
			For i = 0 To aTemp.Count - 1 Step 2
				sCommandText = "UPDATE " & m_sTable & " SET WordInUnit=" & aTemp(i + 1) - 1 & " WHERE WordNumber=" & aTemp(i) & ";"
				DBConnection.ExecuteNonQuery(sCommandText)
			Next i

			' Höchste UnitInNumber feststellen
			Dim iHighestWordInUnit As Integer = 0
			sCommandText = "SELECT WordInUnit FROM " & m_sTable & " WHERE UnitNumber=" & iNumber & ";"
			oleCursor = DBConnection.ExecuteReader(sCommandText)
			Do While oleCursor.Read
				If oleCursor.GetValue(0) > iHighestWordInUnit Then iHighestWordInUnit = oleCursor.GetValue(0)
			Loop

			' Daten der alten Vokabel ändern
			sCommandText = "UPDATE " & m_sTable & " SET UnitNumber=" & iNumber & ", WordInUnit=" & iHighestWordInUnit + 1 & " WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteNonQuery(sCommandText)

			' Membervariable ändern
			m_sUnit = Unit
			m_iUnit = iNumber
		End Set
	End Property

	Property UnitNumber() As Integer
		Get
			Return m_iUnit
		End Get
		Set(ByVal Unit As Integer)

		End Set
	End Property

	Property Chapter() As Integer
		Get
			Return m_iChapter
		End Get
		Set(ByVal Chapter As Integer)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property
			m_iChapter = Chapter
			sCommandText = "UPDATE " & m_sTable & " SET ChapterNumber=" & m_iChapter & " WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteReader(sCommandText)
		End Set
	End Property

	ReadOnly Property WordInUnit() As Integer
		Get
			Return m_iWordInUnit
		End Get
	End Property

	Property MustKnow() As Boolean
		Get
			Return m_bMustKnow
		End Get
		Set(ByVal KnowType As Boolean)
			If (m_bTestMode = True) Or (m_bNoSpecialMode = True) Then Exit Property
			m_bMustKnow = KnowType
			sCommandText = "UPDATE " & m_sTable & " SET MustKnow=" & m_bMustKnow & " WHERE WordNumber=" & iCurrent & ";"
			DBConnection.ExecuteReader(sCommandText)
		End Set
	End Property
#End Region

#Region " Sprachen-Funktionen "
	Protected Function FrenchTypes() As ArrayList
		Dim sList As New ArrayList
		sList.Add("Substantiv")		   '0
		sList.Add("Verb")		   '1
		sList.Add("Adjektiv")		  '2
		sList.Add("Einfache")		  '3
		Return sList
	End Function

	Protected Sub CreateFrenchTypeForms()
		If m_bIrregularForm Then Exit Sub
		If m_bTestMode = False Then Exit Sub
		Select Case m_iWordType
			Case 0			  ' Substantiv
				If (m_sPre = "la") Or (m_sPre = "une") Then
					m_sIrregular1 = "f"
				ElseIf (m_sPre = "le/la") Or (m_sPre = "un/une") Then
					m_sIrregular1 = "m/f"
				Else
					m_sIrregular1 = "m"
				End If
			Case 1			  ' Verb

			Case 2			  ' Adjektiv
				m_sIrregular1 = m_sWord & "e"
			Case 3			  ' Else ;P
				m_sIrregular1 = ""
		End Select
		m_sIrregular2 = ""
		m_sIrregular3 = ""
	End Sub

	Protected Function CheckFrenchWord(ByVal Meaning1 As String, ByVal Meaning2 As String, ByVal Meaning3 As String)
		If m_bWordToMeaning Then
			If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning3) Then Return True
			If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning2) Then Return True
			If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning3) Then Return True
			If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning1) Then Return True
			If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning1) Then Return True
			If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning2) Then Return True
		Else
			Select Case m_sPre
				Case "la"
					If Meaning1 = m_sPre & " " & m_sWord Then Return True
					If Meaning1 = "une " & m_sWord Then Return True
				Case "le"
					If Meaning1 = m_sPre & " " & m_sWord Then Return True
					If Meaning1 = "un " & m_sWord Then Return True
				Case "le/la"
					If Meaning1 = m_sPre & " " & m_sWord Then Return True
					If Meaning1 = "un/une " & m_sWord Then Return True
				Case "un/une", "une", "un"
					If Meaning1 = m_sPre & " " & m_sWord Then Return True
				Case "l'"
					If Meaning1 = m_sPre & m_sWord Then Return True
				Case Else
					If Meaning1 = m_sWord Then Return True
			End Select
		End If
		Return False
	End Function

	Protected Function IrregularDescriptionFrench() As Collection
		Dim cList As New Collection
		Dim sString As String
		Select Case m_iWordType
			Case 0
				cList.Add("Genus")
				cList.Add("")
				cList.Add("")
			Case 1
				cList.Add("")
				cList.Add("")
				cList.Add("")
			Case 2
				cList.Add("Feminin")
				cList.Add("")
				cList.Add("")
			Case 3
				cList.Add("")
				cList.Add("")
				cList.Add("")
		End Select
		Return cList
	End Function

	Protected Function EnglishTypes() As ArrayList
		Dim sList As New ArrayList
		sList.Add("Substantiv")		   '0
		sList.Add("Verb")		   '1
		sList.Add("Adjektiv")		  '2
		sList.Add("Einfache")		  '3
		Return sList
	End Function

	Protected Sub CreateEnglishTypeForms()
		'nichts
	End Sub

	Protected Function CheckEnglishWord(ByVal Meaning1 As String, ByVal Meaning2 As String, ByVal Meaning3 As String)
		If m_bWordToMeaning Then
			If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning3) Then Return True
			If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning2) Then Return True
			If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning3) Then Return True
			If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning1) Then Return True
			If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning1) Then Return True
			If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning2) Then Return True
		Else
			If Meaning1 = m_sWord Then Return True
		End If
		Return False
	End Function

	Protected Function IrregularDescriptionEnglish() As Collection
		Dim cList As New Collection
		Dim sString As String
		Select Case m_iWordType
			Case 0
				cList.Add("")
				cList.Add("")
				cList.Add("")
			Case 1
				cList.Add("")
				cList.Add("")
				cList.Add("")
			Case 2
				cList.Add("")
				cList.Add("")
				cList.Add("")
			Case 3
				cList.Add("")
				cList.Add("")
				cList.Add("")
		End Select
		Return cList
	End Function

	Protected Function LatinTypes() As ArrayList
		Dim sList As New ArrayList
		sList.Add("Substantiv")		   '0
		sList.Add("Verb")		   '1
		sList.Add("Adjektiv")		  '2
		sList.Add("Einfach")		   '3
		Return sList
	End Function

	Protected Sub CreateLatinTypeForms()
		If m_bIrregularForm Then Exit Sub
		If m_bTestMode = False Then Exit Sub
		Select Case m_iWordType
			Case 0			  ' Substantiv

			Case 1			  ' Verb

			Case 2			  ' Adjektiv

			Case 3			  ' Andere

		End Select
	End Sub

	Protected Function CheckLatinWord(ByVal Meaning1 As String, ByVal Meaning2 As String, ByVal Meaning3 As String)
		If m_bWordToMeaning Then
			If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning3) Then Return True
			If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning2) Then Return True
			If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning3) Then Return True
			If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning1) Then Return True
			If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning1) Then Return True
			If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning2) Then Return True
		Else
			If Meaning1 = m_sWord Then Return True
		End If
		Return False
	End Function

	Protected Function IrregularDescriptionLatin() As Collection
		Dim cList As New Collection
		Dim sString As String
		Select Case m_iWordType
			Case 0
				cList.Add("Genitiv")
				cList.Add("Genus")
				cList.Add("")
			Case 1
				cList.Add("1. Person Präsens")
				cList.Add("Partizip")
				cList.Add("Partizip")
			Case 2
				cList.Add("Feminin")
				cList.Add("Neutrum")
				cList.Add("")
			Case 3
				cList.Add("")
				cList.Add("")
				cList.Add("")
		End Select
		Return cList
	End Function
#End Region

	Public Shared Function GetLanguages() As Collection
		Dim cLanguages As New Collection
		cLanguages.Add("General")		 ' 1
		cLanguages.Add("English")		 ' 2
		cLanguages.Add("French")		  ' 3
		cLanguages.Add("Latin")		   ' 4
		Return cLanguages
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
