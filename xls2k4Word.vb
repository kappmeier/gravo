Public Class xlsWord
	Inherits xlsWordInformationEx

	' Vokabel-Daten
	'Protected m_iWordNumber As Integer
	Protected m_iUnit As Integer
	Protected m_sUnit As String
	Protected m_iChapter As Integer
	Protected m_iWordInUnit As Integer



	Protected m_sLastTested As String


	Sub New(ByVal db As CDBOperation)
		MyBase.New(db)
	End Sub

	Sub New(ByVal db As CDBOperation, ByVal WordNumber As Integer, ByVal Table As String)
		MyBase.New(db, WordNumber, Table)
		LoadWord()
	End Sub

	Protected Overloads Sub LoadWord()
		MyBase.LoadWord()
		If m_bvalid = False Then Exit Sub

		DBCommand = "SELECT UnitNumber, ChapterNumber, WordInUnit FROM " & m_sTable & " WHERE WordNumber=" & m_iWordNumber & ";"
		DBCursor = DBConnection.ExecuteReader(DBCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then m_iUnit = -1 Else m_iUnit = DBCursor.GetValue(0)
		If TypeOf (DBCursor.GetValue(1)) Is DBNull Then m_iChapter = -1 Else m_iChapter = DBCursor.GetValue(1)
		If TypeOf (DBCursor.GetValue(2)) Is DBNull Then m_iWordInUnit = -1 Else m_iWordInUnit = DBCursor.GetValue(2)

		If m_iwordnumber > 1 Then DBCommand = "SELECT LetzteAbfrage FROM " & m_sTable & "Stats WHERE WordNumber=" & m_iWordNumber - 1 & ";"
		DBCursor = DBConnection.ExecuteReader(dbCommand)
		DBCursor.Read()
		'If TypeOf (dbCursor.GetValue(0)) Is DBNull Thenm_sLastTested = "01.01.1900" Else 
		m_sLastTested = dbCursor.GetValue(0)
		dbcursor.Close()



		DBCommand = "SELECT UnitNumber, ChapterNumber, WordInUnit FROM " & m_sTable & " WHERE WordNumber=" & m_iWordNumber & ";"
		DBCursor = DBConnection.ExecuteReader(DBCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then m_iUnit = -1 Else m_iUnit = DBCursor.GetValue(0)
		If TypeOf (DBCursor.GetValue(1)) Is DBNull Then m_iChapter = -1 Else m_iChapter = DBCursor.GetValue(1)
		If TypeOf (DBCursor.GetValue(2)) Is DBNull Then m_iWordInUnit = -1 Else m_iWordInUnit = DBCursor.GetValue(2)
		'm_sUnit = GetUnit(m_iUnit)
		dbcursor.Close()

		DBCommand = "SELECT LetzteAbfrage FROM " & m_sTable & "Stats WHERE WordNumber=" & m_iWordNumber & ";"
		'Application.DoEvents()
		DBCursor = DBConnection.ExecuteReader(dbCommand)
		DBCursor.Read()
		'If TypeOf (dbCursor.GetValue(0)) Is DBNull Thenm_sLastTested = "01.01.1900" Else 
		m_sLastTested = dbCursor.GetValue(0)
		dbcursor.Close()


	End Sub

	Public Overloads Sub LoadWord(ByVal WordNumber As Integer)
		m_iWordNumber = WordNumber
		LoadWord()
	End Sub

	Public Overloads Sub LoadWord(ByVal WordNumber As Integer, ByVal Table As String)
		m_stable = Table
		m_btableselected = True
		LoadWord(WordNumber)
	End Sub

	Property UnitName() As String
		Get
			Return m_sUnit
		End Get
		Set(ByVal Unit As String)
			'If (m_bTestMode = True) Or (m_bTableSelected = False) Then Exit Property

			'' Zur neuen Unit die Number feststellen
			'Dim iNumber As Integer

			'iNumber = GetUnitNumber(Unit)
			'If iNumber <= 0 Then MsgBox("Fehler! UnitNumber zur neuen Unit ist falsch!!!")

			'' Aus alter Unit die NumberInUnit-Werte der anderen Vokabeln ändern
			'' Daten hohlen
			'Dim aTemp As New ArrayList, iWordInUnit As Integer
			'DBCommand = "SELECT WordInUnit, WordNumber FROM " & m_sTable & " WHERE UnitNumber=" & m_iUnit & ";"
			'DBCursor = DBConnection.ExecuteReader(DBCommand)
			'Do While DBCursor.Read
			'	iWordInUnit = DBCursor.GetValue(0)
			'	If iWordInUnit > m_iWordInUnit Then
			'		aTemp.Add(DBCursor.GetValue(1))					   ' Add WordNumber to Arraylist
			'		aTemp.Add(iWordInUnit)					   ' Add WordInUnit to Arraylist
			'	End If
			'Loop

			'' Daten ändern
			'Dim i As Integer
			'For i = 0 To aTemp.Count - 1 Step 2
			'	DBCommand = "UPDATE " & m_sTable & " SET WordInUnit=" & aTemp(i + 1) - 1 & " WHERE WordNumber=" & aTemp(i) & ";"
			'	DBConnection.ExecuteNonQuery(DBCommand)
			'Next i

			'' Höchste UnitInNumber feststellen
			'Dim iHighestWordInUnit As Integer = 0
			'DBCommand = "SELECT WordInUnit FROM " & m_sTable & " WHERE UnitNumber=" & iNumber & ";"
			'DBCursor = DBConnection.ExecuteReader(DBCommand)
			'Do While DBCursor.Read
			'	If DBCursor.GetValue(0) > iHighestWordInUnit Then iHighestWordInUnit = DBCursor.GetValue(0)
			'Loop

			'' Daten der alten Vokabel ändern
			'DBCommand = "UPDATE " & m_sTable & " SET UnitNumber=" & iNumber & ", WordInUnit=" & iHighestWordInUnit + 1 & " WHERE WordNumber=" & m_iWordNumber & ";"
			'DBConnection.ExecuteNonQuery(DBCommand)

			'' Membervariable ändern
			'm_sUnit = Unit
			'm_iUnit = iNumber
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
			If m_bValid = False Then Exit Property
			m_iChapter = Chapter
			DBCommand = "UPDATE " & m_sTable & " SET ChapterNumber=" & m_iChapter & " WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property

	ReadOnly Property WordInUnit() As Integer
		Get
			Return m_iWordInUnit
		End Get
	End Property
End Class