Imports System.Data.OleDb

Public Class xlsOldVoc
	Inherits xlsDBBase

#Region " Variablen "
	' Klassenzustände
	Protected m_bTestMode As Boolean = False

	' Vokabeln
	Public Word As xlsWord
	Protected wtWordsInUnit As Collection
	Protected wtUnitsInGroup As Collection
	Protected wtWord As xlsWord

	Protected m_iUnit As Integer
	Protected m_iWordNumber As Integer




	Protected m_iTestType As Integer	 'Abfrageart (Random ...)
	Protected bNew = True	 ' Neuer Datensatz
	Protected cTestUnits As Collection
	Protected m_sTestWord As String
	Protected m_iTestNextMode As Integer = 3
	Protected m_iTestNextModeWrong As Integer = 4
	Protected iTestCurrentWord As Integer = 0
	Protected bErneut = False
	Protected m_iIrregular As xlsVocTestExtended
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
	Sub New(ByVal db As CDBOperation, ByVal Table As String)	' Bestimmte Tabelle zum Zugriff öffnen
		MyBase.new(db, Table)
		m_bTestMode = False
		'Groups = New xlsVocInputGroupCollection(db)
		'wtWordsInUnit = New xlsCollection(db)
		wtUnitsInGroup = New Collection
		wtWord = New xlsWord(db)
	End Sub

	Sub New(ByVal db As CDBOperation)	   ' Keinen Speziellen Table auswählen
		MyBase.New(db)
		m_bTestMode = False
		'Groups = New xlsVocInputGroupCollection(db)
		'wtWordsInUnit = New xlsWordCollection(db)
		wtUnitsInGroup = New Collection
		wtWord = New xlsWord(db)
	End Sub

	Sub SelectTable(ByVal Table As String)
		'Me.Table = Table
		m_bTestMode = False
	End Sub

	Overridable Function GetWord(ByVal WordNumber As Int32) As xlsWord
		If IsConnected() = False Or IsGroupSelected() = False Then Return Nothing

		'wtWord.LoadWord(WordNumber, Table)
		m_iUnit = wtWord.UnitNumber
		m_iWordNumber = WordNumber
		Return wtWord
	End Function

	Overridable ReadOnly Property WordsInUnit(ByVal UnitNumber As Int32) As Collection
		Get
			If IsConnected() = False Then Exit Property
			If (m_bTestMode = True) Or (IsGroupSelected() = False) Then Exit Property

			'wtWordsInUnit.LoadUnit(UnitNumber, CurrentGroupName)
			Return wtWordsInUnit
		End Get
	End Property

	Overridable ReadOnly Property UnitsInGroup() As Collection
		Get
			If Me.IsConnected = False Then Exit Property
			If (m_bTestMode = True) Or (IsGroupSelected() = False) Then Exit Property

			'wtUnitsInGroup.LoadGroup(CurrentGroupName)
			Return wtUnitsInGroup
		End Get
	End Property


	Protected Function ExistDeleted() As Boolean
		Dim sCommand = "SELECT COUNT(Deleted) FROM " & CurrentGroupName & " WHERE Deleted=" & True & ";"
		ExecuteReader(sCommand)
		DBCursor.Read()
		Dim iCount As Integer
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iCount = 0 Else iCount = DBCursor.GetValue(0)
		If iCount > 0 Then Return True Else Return False
	End Function

	Protected Function GetDeleted() As Integer
		If Not ExistDeleted() Then
			Return 0
		Else
			Dim sCommand = "SELECT WordNumber FROM " & CurrentGroupName & " WHERE Deleted=" & True & ";"
			ExecuteReader(sCommand)
			DBCursor.Read()
			If TypeOf (DBCursor.GetValue(0)) Is DBNull Then Return 0 Else Return DBCursor.GetValue(0)
		End If
	End Function




#Region " Datenbank-Funktionen "
	Function SaveTable(ByVal Path As String, Optional ByVal SaveOnlyNewFiles As Boolean = False, Optional ByVal Overwrite As Boolean = False, Optional ByRef ProgressBar As ProgressBar = Nothing, Optional ByRef InfoLabel As Label = Nothing, Optional ByVal Action As String = "gesichert") As xlsSaveErrors
		'If m_bConnected = False Then Return xlsSaveErrors.NotConnected
		'Dim bProgress As Boolean = Not (ProgressBar Is Nothing)
		'Dim bLabel As Boolean = Not (InfoLabel Is Nothing)
		'If bLabel Then
		'	InfoLabel.Text = "Sichern wird vorbereitet."
		'	Application.DoEvents()
		'End If
		'Dim DBSaveConnection As New CDBOperation
		'Dim iWords As Integer
		'Dim sGroupName As String
		'Dim sNewTableName As String
		'Dim sLanguage As String
		'Dim iTablesInLanguage As Integer

		'DBSaveConnection.Open(Path)
		'' Unit und Table Informationen eruieren
		'DBCursor = DBConnection.ExecuteReader("SELECT Lehrbuch FROM Tables WHERE Tabelle='" & m_sTable & "';")
		'DBCursor.Read()
		'If TypeOf (DBCursor.GetValue(0)) Is DBNull Then Return xlsSaveErrors.UnknownError Else sGroupName = DBCursor.GetValue(0)

		'DBCursor = DBSaveConnection.ExecuteReader("SELECT Tabelle FROM Tables WHERE Lehrbuch='" & sGroupName & "';")
		'DBCursor.Read()
		'Try
		'	If DBCursor.HasRows = True Then
		'		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then sNewTableName = "" Else sNewTableName = DBCursor.GetValue(0)
		'	Else
		'		sNewTableName = ""
		'	End If
		'Catch e As InvalidOperationException		  ' Keine Daten vorhanden	TODO		???? weiterhin sinnvoll ????
		'	sNewTableName = ""
		'End Try

		'' Aktuellen Table hinzufügen, falls schon vorhanden, löschen und neuanlegen
		'Dim iStart As Integer = 1
		'If sNewTableName <> "" Then		  ' Schon vorhanden, löschen und anschließend Neuanlegen
		'	If SaveOnlyNewFiles Then
		'		DBCommand = "SELECT COUNT(WordNumber) FROM " & sNewTableName & ";"
		'		DBCursor = DBSaveConnection.ExecuteReader(DBCommand)
		'		DBCursor.Read()
		'		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iStart = 1 Else iStart = DBCursor.GetValue(0) + 1
		'	Else
		'		If (Overwrite) Then
		'			DBSaveConnection.ExecuteNonQuery("DROP TABLE " & sNewTableName & ";")
		'			DBSaveConnection.ExecuteNonQuery("DROP TABLE " & sNewTableName & "Stats;")
		'			DBSaveConnection.ExecuteNonQuery("DROP TABLE " & sNewTableName & "Units;")
		'		Else
		'			Return xlsSaveErrors.TableExists
		'		End If
		'	End If
		'Else		  ' Feststellen der richtigen Nummer und Sprache, nicht vorhanden und anschließend Neuanlegen
		'	DBCursor = DBSaveConnection.ExecuteReader("SELECT COUNT(Tabelle) FROM Tables WHERE Art='" & Me.Language & "';")
		'	DBCursor.Read()
		'	If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iTablesInLanguage = 0 Else iTablesInLanguage = DBCursor.GetValue(0)
		'	' Table in Table-Liste eintragen
		'	If iTablesInLanguage < 8 Then sNewTableName = Me.Language & "0" & (iTablesInLanguage + 1) Else sNewTableName = Me.Language & (iTablesInLanguage + 1)
		'	DBCommand = "INSERT INTO Tables VALUES('" & AddHighColons(sGroupName) & "',"
		'	DBCommand += "'" & AddHighColons(sNewTableName) & "',"
		'	DBCommand += "'" & AddHighColons(Language) & "');"
		'	DBSaveConnection.ExecuteNonQuery(DBCommand)
		'End If

		'' Tabellen anlegen
		'Dim SaveGroups As New xlsVocInputGroupCollection(Path)
		'SaveGroups.AddExisting(sNewTableName)

		'' Anzahl der Datensätze feststellen
		'DBCommand = "SELECT COUNT(WordNumber) FROM " & m_sTable & ";"
		'DBCursor = DBConnection.ExecuteReader(DBCommand)
		'DBCursor.Read()
		'If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iWords = 0 Else iWords = DBCursor.GetValue(0)

		'' Daten kopieren
		'Dim i As Integer
		'DBSaveConnection.CloseReader()
		'If bProgress Then
		'	ProgressBar.Maximum = iWords
		'	ProgressBar.Minimum = 0
		'	ProgressBar.Step = 1
		'	'ProgressBar.PerformStep()
		'End If
		'If bLabel Then
		'	InfoLabel.Text = "Datensätze werden " & Action & "."
		'	Application.DoEvents()
		'End If
		'For i = iStart To iWords
		'	If bLabel Then
		'		InfoLabel.Text = "Schreibe " & i & " von " & iWords & "..."
		'	End If
		'	Application.DoEvents()
		'	Me.GoToWord(i)			 ' Daten lesen
		'	InsertWord(DBSaveConnection, sNewTableName)			   ' Daten schreiben
		'	' Statistik schreiben		leer anlegen, da Benutzer noch nicht eingeführt wurden
		'	Dim j As Integer
		'	j = m_iWordNumber
		'	CreateNewStat(DBSaveConnection, sNewTableName)
		'	If bProgress Then ProgressBar.PerformStep()
		'	Application.DoEvents()
		'Next i
		'If bLabel Then
		'	InfoLabel.Text = iWords & " Datensätze erfolgreich " & Action & "."
		'	Application.DoEvents()
		'	InfoLabel.Text = "Die Unit-Informationen werden " & Action & "..."
		'	Application.DoEvents()
		'End If
		'' Sichern der Unit-Namen
		'Dim iUnitCount As Integer
		'DBCommand = "SELECT COUNT(*) FROM " & m_sTable & "Units;"
		'DBCursor = DBConnection.ExecuteReader(DBCommand)
		'DBCursor.Read()
		'If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iUnitCount = 1 Else iUnitCount = DBCursor.GetValue(0)
		'DBCommand = "SELECT * FROM " & m_sTable & "Units;"
		'DBCursor = DBConnection.ExecuteReader(DBCommand)
		'Dim iUnitNumber As Integer
		'Dim sUnitText As String
		'For i = 1 To iUnitCount
		'	DBCursor.Read()
		'	If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iUnitNumber = i Else iUnitNumber = DBCursor.GetValue(0)
		'	If TypeOf (DBCursor.GetValue(0)) Is DBNull Then sUnitText = "" Else sUnitText = DBCursor.GetValue(1)
		'	DBCommand = "INSERT INTO " & sNewTableName & "Units VALUES(" & iUnitNumber & ", '" & AddHighColons(sUnitText) & "');"
		'	DBSaveConnection.ExecuteNonQuery(DBCommand)
		'Next i


		'DBSaveConnection.Close()		 ' Verbindung schließen
		'If bLabel Then
		'	InfoLabel.Text = "Fertig."
		'	Application.DoEvents()
		'End If
		'Return xlsSaveErrors.NoError		 ' Beenden. OK
	End Function
#End Region

#Region " Test-Funktionen "
	Overridable Sub TestInitialize(ByRef TestUnits As Collection, Optional ByVal WordToMeaning As Boolean = False)
		'If m_bConnected = False Then Exit Sub

		'm_bTestMode = True
		''b_TableSel = False
		'm_bWordToMeaning = WordToMeaning

		'Dim i As Integer
		'Dim structWord As TestWord

		'cTestUnits = TestUnits
		'cWords = New Collection
		'For i = 1 To TestUnits.Count
		'	DBCommand = "SELECT WordNumber FROM " & TestUnits(i).table & " WHERE UnitNumber=" & TestUnits(i).unit & " AND Deleted=" & False & " ORDER BY WordNumber;"
		'	DBCursor = DBConnection.ExecuteReader(DBCommand)
		'	structWord.Table = TestUnits(i).table
		'	Do While DBCursor.Read
		'		If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then structWord.WordNumber = DBCursor.GetValue(0) Else structWord.WordNumber = 0
		'		cWords.Add(structWord)
		'	Loop
		'Next i

		'bErneut = False
		'm_iTestWordCountAll = cWords.Count
		'm_iTestWordCountToDo = cWords.Count
		'm_iTestWordCountDone = 0
		'm_iTestWordCountDoneRight = 0
		'm_iTestWordCountDoneFalse = 0
		'm_iTestWordCountDoneFalseAllTrys = 0
		'iTestWordCountDoneCorrection = 0
	End Sub

	Overridable Sub TestClose()
		'Dim DBCommand = "UPDATE " & m_sTable & "Stats SET AbfrageGestartet=" & False & " WHERE AbfrageGestartet=" & True & ";"
		'DBConnection.ExecuteReader(DBCommand)
		'm_bTestMode = False
		'm_bTableSelected = True
		'cTestUnits = Nothing
		'cWords = Nothing
		'm_iTestWordCountAll = 0
		'm_iTestWordCountToDo = 0
		'm_iTestWordCountDone = 0
		'm_iTestWordCountDoneRight = 0
		'm_iTestWordCountDoneFalse = 0
		'm_iTestWordCountDoneFalseAllTrys = 0
		'iTestWordCountDoneCorrection = 0
	End Sub

	Overridable Sub TestGetNext()
		'If m_bConnected = False Then Exit Sub
		'If m_bTestMode = False Then Exit Sub

		'If cWords.Count = 0 Then Exit Sub
		'Select Case m_iTestNextMode
		'	Case 0			  ' Der Reihe nach
		'		m_sTable = cWords(1).table
		'		GoToWord(cWords(1).wordnumber)
		'		iTestCurrentWord = 1
		'	Case 1			  ' Zufällig alle gewählten
		'		If bErneut = False Then
		'			Dim iNext As Integer
		'			Randomize()
		'			iNext = CInt(Int((cWords.Count * Rnd()) + 1))
		'			iTestCurrentWord = iNext
		'			m_sTable = cWords(1).table
		'			GoToWord(cWords(iNext).wordnumber)
		'		End If
		'	Case Else
		'		MsgBox("Dieser Abfrage-Modus wird zur zeit nicht unterstützt!")
		'		m_iTestNextMode = 0
		'End Select

		'm_bWordToMeaning = TestWordToMeaning()
		'If m_bWordToMeaning Then
		'	m_sTestWord = m_sPre & m_sWord & m_sPost
		'Else
		'	m_sTestWord = m_sMeaning1
		'	If m_sMeaning2 <> "" Then m_sTestWord += ", " & m_sMeaning2
		'	If m_sMeaning3 <> "" Then m_sTestWord += ", " & m_sMeaning3
		'End If
		'Me.CreateTypeForms()
	End Sub

	Overridable Function TestControl(Optional ByVal Word As String = "", Optional ByVal Meaning1 As String = "", Optional ByVal Meaning2 As String = "", Optional ByVal Meaning3 As String = "", Optional ByVal Irregular1 As String = "", Optional ByVal Irregular2 As String = "", Optional ByVal Irregular3 As String = "") As Boolean
		'If m_bConnected = False Then Exit Function
		'If (Not m_bTestMode) Or (Not m_bTableSelected) Then Exit Function

		'Dim bRight As Boolean
		'bRight = False
		'bRight = CheckWord(Meaning1, Meaning2, Meaning3)
		'If ((m_iIrregular = xlsVocTestExtended.Always) Or (m_iIrregular = xlsVocTestExtended.IrregularOnly And m_bExtendedIsValid)) Then
		'	If Irregular1 <> m_sExtended1 Then bRight = False
		'	If Irregular2 <> m_sExtended2 Then bRight = False
		'	If Irregular3 <> m_sExtended3 Then bRight = False
		'End If
		'UpdateStats(bRight)
		'Return bRight
	End Function

	Protected Sub UpdateStats(ByVal Right As Boolean)
		''**********************************
		''* Aktualisierung der Statistiken *
		''**********************************
		'Dim iTests, iTestsAll, iRight, iWrong, iWrongAll As Integer
		'Dim iHelp1, iHelp2, iHelp3 As Integer
		'Dim sFirst As String
		'Dim bTestStart As Boolean
		'Dim bFirstTry As Boolean

		'DBConnection.Open(dbPath)
		'DBCommand = "SELECT Abfragen, AbfragenGesamt, Richtig, Falsch, FalschGesamt, AbfrageGestartet, ErsteAbfrage, LetzteAbfrage FROM " & m_sTable & "Stats WHERE WordNumber=" & m_iWordNumber & ";"
		'DBCursor = DBConnection.ExecuteReader(DBCommand)
		'DBCursor.Read()
		'If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iTests = 0 Else iTests = DBCursor.GetValue(0)
		'If TypeOf (DBCursor.GetValue(1)) Is DBNull Then iTestsAll = 0 Else iTestsAll = DBCursor.GetValue(1)
		'If TypeOf (DBCursor.GetValue(2)) Is DBNull Then iRight = 0 Else iRight = DBCursor.GetValue(2)
		'If TypeOf (DBCursor.GetValue(3)) Is DBNull Then iWrong = 0 Else iWrong = DBCursor.GetValue(3)
		'If TypeOf (DBCursor.GetValue(4)) Is DBNull Then iWrongAll = 0 Else iWrongAll = DBCursor.GetValue(4)
		'If TypeOf (DBCursor.GetValue(5)) Is DBNull Then bTestStart = False Else bTestStart = DBCursor.GetBoolean(5)
		'If TypeOf (DBCursor.GetValue(6)) Is DBNull Then sFirst = "01.01.1900" Else sFirst = DBCursor.GetDateTime(6)
		'DBCommand = "SELECT Hilfe1Richtig, Hilfe2Richtig, Hilfe3Richtig FROM " & m_sTable & "Stats WHERE WordNumber=" & m_iWordNumber & ";"
		'DBCursor = DBConnection.ExecuteReader(DBCommand)
		'DBCursor.Read()
		'If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iHelp1 = 0 Else iHelp1 = DBCursor.GetValue(0)
		'If TypeOf (DBCursor.GetValue(1)) Is DBNull Then iHelp2 = 0 Else iHelp2 = DBCursor.GetValue(1)
		'If TypeOf (DBCursor.GetValue(2)) Is DBNull Then iHelp3 = 0 Else iHelp3 = DBCursor.GetValue(2)
		'DBCursor.Close()

		'm_sLastTested = Format(Now, "dd.MM.yyyy")
		'If sFirst = "01.01.1900" Then
		'	sFirst = m_sLastTested
		'	If m_bFirstTry = True Then
		'		bFirstTry = True
		'		DBCommand = "UPDATE " & m_sTable & "Stats SET ErsteAbfrage='" & sFirst & "', LetzteAbfrage='" & m_sLastTested & "' WHERE WordNumber=" & m_iWordNumber & ";"
		'		DBConnection.ExecuteNonQuery(DBCommand)
		'	Else
		'		bFirstTry = False
		'	End If
		'Else
		'	bFirstTry = False
		'End If
		'If Right = True Then		  ' richtige Antwort
		'	If bTestStart = False Then
		'		Select Case m_iHelpMode				' Test ob Hilfe benutzt wurde
		'			Case xlsVocTestHelpModes.NoHelp
		'				m_iTestWordCountDoneRight += 1
		'				iRight += 1
		'			Case xlsVocTestHelpModes.LightHelp
		'				m_iTestWordCountHelp1 += 1
		'				iHelp1 += 1
		'			Case xlsVocTestHelpModes.MiddleHelp
		'				m_iTestWordCountHelp2 += 1
		'				iHelp2 += 1
		'			Case xlsVocTestHelpModes.HeavyHelp
		'				m_iTestWordCountHelp3 += 1
		'				iHelp3 += 1
		'		End Select
		'		iTests += 1
		'		iTestsAll += 1
		'	Else
		'		iTestsAll += 1
		'	End If
		'	bTestStart = False
		'	cWords.Remove(iTestCurrentWord)
		'	bErneut = False
		'Else		  ' falsche antwort
		'	m_iTestWordCountDoneFalseAllTrys += 1
		'	If bTestStart = False Then
		'		m_iTestWordCountDoneFalse += 1
		'		iTests += 1
		'		iTestsAll += 1
		'		iWrong += 1
		'		iWrongAll += 1
		'	Else
		'		iTestsAll += 1
		'		iWrongAll += 1
		'	End If
		'	Select Case m_iTestNextModeWrong			 ' Eventuelle Wort-Neu-Abfrage Testen:
		'		Case 0				' Fehlerhafte sofort abfragen bis Korrekt
		'			bTestStart = True
		'			bErneut = True
		'		Case 1				'Fehlerhafte sofort erneut abfragen
		'			If bErneut = False Then
		'				bErneut = True
		'				bTestStart = True
		'			Else
		'				cWords.Remove(iTestCurrentWord)
		'				bErneut = False
		'				bTestStart = False
		'			End If
		'		Case 2				'Fehlerhafte abfragen bis Korrekt, in Liste einfügen
		'			bTestStart = True
		'			Dim structWord As TestWord
		'			structWord.Table = m_sTable
		'			structWord.WordNumber = m_iWordNumber
		'			cWords.Remove(iTestCurrentWord)
		'			cWords.Add(structWord)
		'		Case 3				' Fehlerhafte erneut abfragen, in Liste einfügen
		'			If bTestStart = True Then
		'				bTestStart = False
		'				cWords.Remove(iTestCurrentWord)
		'			Else
		'				bTestStart = True
		'				Dim structWord As TestWord
		'				structWord.Table = m_sTable
		'				structWord.WordNumber = m_iWordNumber
		'				cWords.Remove(iTestCurrentWord)
		'				cWords.Add(structWord)
		'			End If
		'		Case 4				 ' Fehlerhafte abfragen bis Korrekt, neue Liste am Ende
		'			iTestWordCountDoneCorrection += 1
		'			bTestStart = True
		'			cWords.Remove(iTestCurrentWord)
		'		Case 5				 ' Fehlerhafte erneut abfragen, neue Liste am Ende
		'			If bTestStart = True Then
		'				bTestStart = False
		'			Else
		'				iTestWordCountDoneCorrection += 1
		'				bTestStart = True
		'			End If
		'			cWords.Remove(iTestCurrentWord)
		'		Case 6
		'			bTestStart = False
		'			cWords.Remove(iTestCurrentWord)
		'		Case Else
		'			MsgBox("Dieser Falsche-Vokabel-Modus wird leider nicht unterstützt!")
		'	End Select
		'End If
		'If Not bFirstTry Then
		'	DBCommand = "UPDATE " & m_sTable & "Stats SET Abfragen=" & iTests & ", AbfragenGesamt=" & iTestsAll & ", Richtig=" & iRight & ", Falsch=" & iWrong & ", FalschGesamt=" & iWrongAll & ", AbfrageGestartet=" & bTestStart & ", ErsteAbfrage='" & sFirst & "', LetzteAbfrage='" & m_sLastTested & "' WHERE WordNumber=" & m_iWordNumber & ";"
		'	DBConnection.ExecuteNonQuery(DBCommand)
		'	DBCommand = "UPDATE " & m_sTable & "Stats SET Hilfe1Richtig=" & iHelp1 & ", Hilfe2Richtig=" & iHelp2 & ", Hilfe3Richtig=" & iHelp3 & " WHERE WordNumber=" & m_iWordNumber & ";"
		'	DBConnection.ExecuteNonQuery(DBCommand)
		'Else
		'	DBCommand = "UPDATE " & m_sTable & "Stats SET AbfrageGestartet=" & bTestStart & ", ErsteAbfrage='" & sFirst & "', LetzteAbfrage='" & m_sLastTested & "' WHERE WordNumber=" & m_iWordNumber & ";"
		'	DBConnection.ExecuteNonQuery(DBCommand)
		'End If

		'If cWords.Count = 0 Then		  ' Liste Leer, füllen mit den noch nicht beendeten
		'	Dim i As Integer
		'	Dim structWord As TestWord

		'	For i = 1 To cTestUnits.Count
		'		DBCommand = "SELECT WordNumber FROM " & cTestUnits(i).table & "Stats WHERE AbfrageGestartet=" & True & " ORDER BY WordNumber;"
		'		DBCursor = DBConnection.ExecuteReader(DBCommand)
		'		structWord.Table = cTestUnits(i).table
		'		Do While DBCursor.Read
		'			If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then structWord.WordNumber = DBCursor.GetValue(0) Else structWord.WordNumber = 0
		'			cWords.Add(structWord)
		'		Loop
		'		DBCursor.Close()
		'	Next
		'	iTestWordCountDoneCorrection = 0
		'	bErneut = False
		'End If

		'm_iTestWordCountToDo = cWords.Count + iTestWordCountDoneCorrection
		'm_iTestWordCountDone = m_iTestWordCountAll - m_iTestWordCountToDo
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

	Shared ReadOnly Property xlsVocTestExtendedModes() As ArrayList
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

	Property xlsVocTestExtendedMode() As xlsVocTestExtended
		Get
			Return m_iIrregular
		End Get
		Set(ByVal Value As xlsVocTestExtended)
			m_iIrregular = Value
		End Set
	End Property

	ReadOnly Property TypeText(ByVal TypeNumber) As String
		Get
			If IsConnected() = False Then Exit Property
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
		If IsConnected() = False Then Exit Sub
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

	Property TestWordMode() As xlsVocTestDirection
		Get
			Return m_iTestWordMode
		End Get
		Set(ByVal Value As xlsVocTestDirection)
			m_iTestWordMode = Value
		End Set
	End Property

	Protected Function TestWordToMeaning() As Boolean
		Select Case m_iTestWordMode
			Case xlsVocTestDirection.LanguageDefault
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
			Case xlsVocTestDirection.TestMeaning
				Return True
			Case xlsVocTestDirection.TestWord
				Return False
		End Select
	End Function

	ReadOnly Property TestAnswer1() As String
		Get
			'If m_bWordToMeaning Then
			'	Return m_sMeaning1
			'Else
			'	Return m_sPre & " " & m_sWord & " " & m_sPost
			'End If
		End Get
	End Property

	ReadOnly Property TestAnswer2() As String
		Get
			'If m_bWordToMeaning Then
			'	Return m_sMeaning2
			'Else
			'	Return ""
			'End If
		End Get
	End Property

	ReadOnly Property TestAnswer3() As String
		Get
			'If m_bWordToMeaning Then
			'	Return m_sMeaning3
			'Else
			'	Return ""
			'End If
		End Get
	End Property

	ReadOnly Property TestGrammar1() As String
		Get
			'If m_iIrregular = xlsVocTestExtended.Never Then Return ""
			'If (m_iIrregular = xlsVocTestExtended.IrregularOnly) And (m_bExtendedIsValid) Then Return Irregular1
			'If m_iIrregular = xlsVocTestExtended.Always Then Return Irregular1
			'Return ""
		End Get
	End Property

	ReadOnly Property TestGrammar2() As String
		Get
			'If m_iIrregular = xlsVocTestExtended.Never Then Return ""
			'If (m_iIrregular = xlsVocTestExtended.IrregularOnly) And (m_bExtendedIsValid) Then Return Irregular2
			'If m_iIrregular = xlsVocTestExtended.Always Then Return Irregular2
			'Return ""
		End Get
	End Property

	ReadOnly Property TestGrammar3() As String
		Get
			'If m_iIrregular = xlsVocTestExtended.Never Then Return ""
			'If (m_iIrregular = xlsVocTestExtended.IrregularOnly) And (m_bExtendedIsValid) Then Return Irregular3
			'If m_iIrregular = xlsVocTestExtended.Always Then Return Irregular3
			'Return ""
		End Get
	End Property

	ReadOnly Property TestWordCount() As Integer
		Get
			'Return cWords.Count
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

	Property HelpMode() As xlsVocTestHelpModes
		Get
			Return m_iHelpMode
		End Get
		Set(ByVal Value As xlsVocTestHelpModes)
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
		'If m_bConnected = False Then Exit Function
		'If m_bTestMode = False Then Exit Function
		'Select Case Language()
		'	Case "French"
		'		Return CheckFrenchWord(Meaning1, Meaning2, Meaning3)
		'	Case "English"
		'		Return CheckEnglishWord(Meaning1, Meaning2, Meaning3)
		'	Case "Latin"
		'		Return CheckLatinWord(Meaning1, Meaning2, Meaning3)
		'	Case Else
		'		If m_bWordToMeaning Then
		'			If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning3) Then Return True
		'			If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning2) Then Return True
		'			If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning3) Then Return True
		'			If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning1) Then Return True
		'			If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning1) Then Return True
		'			If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning2) Then Return True
		'		Else
		'			If Meaning1 = m_sWord Then Return True
		'			Return False
		'		End If
		'End Select
	End Function
#End Region

#Region " Unit-Funktionen "

	Overridable Function GetUnit(ByVal Number As Integer) As String
		If IsConnected() = False Then Exit Function
		'If m_bTestMode Or m_bNoSpecialMode Then Exit Function

		Dim sTemp As String

		'DBCommand = "SELECT Name FROM " & CurrentGroupName & "Units WHERE Nummer=" & Number & ";"
		'DBCursor = DBConnection.ExecuteReader(DBCommand)
		If DBCursor.Read Then sTemp = DBCursor.GetString(0) Else sTemp = ""

		Return sTemp
	End Function

	Overridable Function GetUnitNumber(ByVal Name As String) As Integer
		If IsConnected() = False Then Exit Function
		'If m_bTestMode Or m_bNoSpecialMode Then Exit Function
		Dim iTemp As Integer

		'DBCommand = "SELECT Nummer FROM " & CurrentGroupName & "Units WHERE Name='" & AddHighColons(Name) & "';"
		'DBCursor = DBConnection.ExecuteReader(DBCommand)
		If DBCursor.Read Then iTemp = DBCursor.GetInt32(0) Else iTemp = 0

		Return iTemp
	End Function

	Overridable Function UnitAdd(ByVal UnitName As String)
		If IsConnected() = False Then Exit Function
		If (m_bTestMode = True) Or (IsGroupSelected() = False) Then Exit Function

		Dim iCount As Integer
		'DBCommand = "SELECT COUNT(Nummer) FROM " & CurrentGroupName & "Units"
		'DBCursor = DBConnection.ExecuteReader(DBCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iCount = 0 Else iCount = DBCursor.GetValue(0)

		'DBCommand = "INSERT INTO " & CurrentGroupName & "Units VALUES (" & iCount + 1 & ", '" & AddHighColons(UnitName) & "')"
		'DBConnection.ExecuteReader(DBCommand)
	End Function

	Overridable Function UnitEdit(ByVal Name As String, ByVal Unit As Integer)
		If IsConnected() = False Then Exit Function
		If (m_bTestMode = True) Or (IsGroupSelected() = False) Then Exit Function

		'DBCommand = "UPDATE " & CurrentGroupName & "Units SET Name='" & Name & "' WHERE Nummer=" & Unit & ";"
		'DBConnection.ExecuteNonQuery(DBCommand)
	End Function

	Overridable Function UnitEdit(ByVal Name As String, ByVal Unit As String)
		If IsConnected() = False Then Exit Function
		If (m_bTestMode = True) Or (IsGroupSelected() = False) Then Exit Function

		'DBCommand = "UPDATE " & CurrentGroupName & "Units SET Name='" & Name & "' WHERE Nummer=" & Unit & ";"
		'DBConnection.ExecuteNonQuery(DBCommand)
	End Function
#End Region

#Region " Zusätzliche Wort-Informationen "

	Property TestType() As Integer
		Get
			Return m_iTestType
		End Get
		Set(ByVal Value As Integer)
			If (m_bTestMode = False) Or (IsGroupSelected() = False) Then Exit Property
			m_iTestType = Value
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
		'If m_bExtendedIsValid Then Exit Sub
		'If m_bTestMode = False Then Exit Sub
		'Select Case m_iWordType
		'	Case 0			  ' Substantiv
		'		If (m_sPre = "la") Or (m_sPre = "une") Then
		'			m_sExtended1 = "f"
		'		ElseIf (m_sPre = "le/la") Or (m_sPre = "un/une") Then
		'			m_sExtended1 = "m/f"
		'		Else
		'			m_sExtended1 = "m"
		'		End If
		'	Case 1			  ' Verb

		'	Case 2			  ' Adjektiv
		'		m_sExtended1 = m_sWord & "e"
		'	Case 3			  ' Else ;P
		'		m_sExtended1 = ""
		'End Select
		'm_sExtended2 = ""
		'm_sExtended3 = ""
	End Sub

	Protected Function CheckFrenchWord(ByVal Meaning1 As String, ByVal Meaning2 As String, ByVal Meaning3 As String)
		'If m_bWordToMeaning Then
		'	If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning3) Then Return True
		'	If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning2) Then Return True
		'	If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning3) Then Return True
		'	If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning1) Then Return True
		'	If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning1) Then Return True
		'	If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning2) Then Return True
		'Else
		'	Select Case m_sPre
		'		Case "la"
		'			If Meaning1 = m_sPre & " " & m_sWord Then Return True
		'			If Meaning1 = "une " & m_sWord Then Return True
		'		Case "le"
		'			If Meaning1 = m_sPre & " " & m_sWord Then Return True
		'			If Meaning1 = "un " & m_sWord Then Return True
		'		Case "le/la"
		'			If Meaning1 = m_sPre & " " & m_sWord Then Return True
		'			If Meaning1 = "un/une " & m_sWord Then Return True
		'		Case "un/une", "une", "un"
		'			If Meaning1 = m_sPre & " " & m_sWord Then Return True
		'		Case "l'"
		'			If Meaning1 = m_sPre & m_sWord Then Return True
		'		Case Else
		'			If Meaning1 = m_sWord Then Return True
		'	End Select
		'End If
		'Return False
	End Function

	Protected Function IrregularDescriptionFrench() As Collection
		'Dim cList As New Collection
		'Dim sString As String
		'Select Case m_iWordType
		'	Case 0
		'		cList.Add("Genus")
		'		cList.Add("")
		'		cList.Add("")
		'	Case 1
		'		cList.Add("")
		'		cList.Add("")
		'		cList.Add("")
		'	Case 2
		'		cList.Add("Feminin")
		'		cList.Add("")
		'		cList.Add("")
		'	Case 3
		'		cList.Add("")
		'		cList.Add("")
		'		cList.Add("")
		'End Select
		'Return cList
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
		'If m_bWordToMeaning Then
		'	If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning3) Then Return True
		'	If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning2) Then Return True
		'	If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning3) Then Return True
		'	If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning1) Then Return True
		'	If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning1) Then Return True
		'	If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning2) Then Return True
		'Else
		'	If Meaning1 = m_sWord Then Return True
		'End If
		'Return False
	End Function

	Protected Function IrregularDescriptionEnglish() As Collection
		'Dim cList As New Collection
		'Dim sString As String
		'Select Case m_iWordType
		'	Case 0
		'		cList.Add("")
		'		cList.Add("")
		'		cList.Add("")
		'	Case 1
		'		cList.Add("")
		'		cList.Add("")
		'		cList.Add("")
		'	Case 2
		'		cList.Add("")
		'		cList.Add("")
		'		cList.Add("")
		'	Case 3
		'		cList.Add("")
		'		cList.Add("")
		'		cList.Add("")
		'End Select
		'Return cList
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
		'If m_bExtendedIsValid Then Exit Sub
		'If m_bTestMode = False Then Exit Sub
		'Select Case m_iWordType
		'	Case 0			  ' Substantiv

		'	Case 1			  ' Verb

		'	Case 2			  ' Adjektiv

		'	Case 3			  ' Andere

		'End Select
	End Sub

	Protected Function CheckLatinWord(ByVal Meaning1 As String, ByVal Meaning2 As String, ByVal Meaning3 As String)
		'If m_bWordToMeaning Then
		'	If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning3) Then Return True
		'	If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning2) Then Return True
		'	If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning3) Then Return True
		'	If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning1) Then Return True
		'	If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning1) Then Return True
		'	If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning2) Then Return True
		'Else
		'	If Meaning1 = m_sWord Then Return True
		'End If
		'Return False
	End Function

	Protected Function IrregularDescriptionLatin() As Collection
		'Dim cList As New Collection
		'Dim sString As String
		'Select Case m_iWordType
		'	Case 0
		'		cList.Add("Genitiv")
		'		cList.Add("Genus")
		'		cList.Add("")
		'	Case 1
		'		cList.Add("1. Person Präsens")
		'		cList.Add("Partizip")
		'		cList.Add("Partizip")
		'	Case 2
		'		cList.Add("Feminin")
		'		cList.Add("Neutrum")
		'		cList.Add("")
		'	Case 3
		'		cList.Add("")
		'		cList.Add("")
		'		cList.Add("")
		'End Select
		'Return cList
	End Function
#End Region
End Class
