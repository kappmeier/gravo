Public Class xlsWord
	Inherits xlsBase

	Private m_sTable As String = ""

	' Vokabel-Speicher-Ort
	Protected m_bValid As Boolean = False
	Protected m_iWordNumber As Integer

	' Vokabelinformationen
	Protected m_sWord As String	  'Vokabel
	Protected m_sPre As String	'Pre-Vokabel    (to, le, ...)
	Protected m_sPost As String	  'Post-Vokabel   (Plural, slang, ...)
	Protected m_aMeaning As ArrayList	' Bedeutungsliste
	Protected m_sExtended1 As String	  'Irregular
	Protected m_sExtended2 As String	  'Irregular
	Protected m_sExtended3 As String	  'Irregular
	Protected m_sDescription As String
	Protected m_bExtendedIsValid As Boolean	 'Vokabel hat irreguläre Formen
	Protected m_iWordType As Integer	 'Vokabelart (Nomen, Verb ...)

	' Word informatonEX
	Protected m_bMustKnow As Boolean	 'Vokabel muß nicht gewußt werden
	Protected m_sAdditionalTargetLangInfo	' Beschreibung der gesuchten Vokabel

	' Word
	Protected m_iUnit As Integer
	Protected m_sUnit As String
	Protected m_iChapter As Integer
	Protected m_iWordInUnit As Integer

	Protected m_sLastTested As String

	Sub New(ByVal db As CDBOperation)
		MyBase.new(db)
	End Sub

	Sub New(ByVal db As CDBOperation, ByVal WordNumber As Integer, ByVal sTable As String)
		MyBase.New(db)
		Me.m_sTable = sTable
		'm_bTableSelected = True
		m_iWordNumber = WordNumber
		LoadWord()
	End Sub

	Public Sub AddMeaning(ByVal sWord As String)
		If m_aMeaning.Contains(Trim(sWord)) Then Exit Sub
		If InStr(sWord, ";") > 0 Then Exit Sub
		m_aMeaning.Add(Trim(sWord))
	End Sub

	Public Sub DeleteAllMeanings()
		m_aMeaning.Clear()
	End Sub

	Public Sub DeleteMeaning(ByVal sWord As String)
		If Not m_aMeaning.Contains(Trim(sWord)) Then Exit Sub
		m_aMeaning.Remove(Trim(sWord))
	End Sub

	Private Sub LoadWord()
		If IsConnected() = False Then Exit Sub
		If SelectedGroup() = "" Then Exit Sub
		Dim bDeleted As Boolean

		Dim sCommand = "SELECT Deleted FROM " & SelectedGroup() & " WHERE WordNumber=" & m_iWordNumber & ";"
		ExecuteReader(sCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then bDeleted = False Else bDeleted = DBCursor.GetValue(0)
		DBCursor.Close()
		If bDeleted Then
			m_bValid = False
			Exit Sub
		End If

		sCommand = "SELECT MEaning1 FROM " & SelectedGroup() & " WHERE WordNumber=" & m_iWordNumber & ";"
		ExecuteReader(sCommand)
		DBCursor.Read()
		sCommand = DBCursor.GetValue(0)
		m_aMeaning = New ArrayList
		If Trim(sCommand) <> "" Then
			Dim aTemp As Array = Split(DBCursor.GetValue(0), ";")
			m_aMeaning.AddRange(aTemp)
		End If

		sCommand = "SELECT Word, Pre, Post, Description FROM " & SelectedGroup() & " WHERE WordNumber=" & m_iWordNumber & ";"
		ExecuteReader(sCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then m_sWord = "" Else m_sWord = DBCursor.GetValue(0)
		If TypeOf (DBCursor.GetValue(1)) Is DBNull Then m_sPre = "" Else m_sPre = DBCursor.GetValue(1)
		If TypeOf (DBCursor.GetValue(2)) Is DBNull Then m_sPost = "" Else m_sPost = DBCursor.GetValue(2)
		If TypeOf (DBCursor.GetValue(3)) Is DBNull Then m_sDescription = "" Else m_sDescription = DBCursor.GetValue(3)

		sCommand = "SELECT WordType, IrregularForm FROM " & SelectedGroup() & " WHERE WordNumber=" & m_iWordNumber & ";"
		ExecuteReader(sCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then m_iWordType = 0 Else m_iWordType = DBCursor.GetValue(0)
		If TypeOf (DBCursor.GetValue(1)) Is DBNull Then m_bExtendedIsValid = False Else m_bExtendedIsValid = DBCursor.GetBoolean(1)

		sCommand = "SELECT Irregular1, Irregular2, Irregular3 FROM " & SelectedGroup() & " WHERE WordNumber=" & m_iWordNumber & ";"
		ExecuteReader(sCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then m_sExtended1 = "" Else m_sExtended1 = DBCursor.GetValue(0)
		If TypeOf (DBCursor.GetValue(1)) Is DBNull Then m_sExtended2 = "" Else m_sExtended2 = DBCursor.GetValue(1)
		If TypeOf (DBCursor.GetValue(2)) Is DBNull Then m_sExtended3 = "" Else m_sExtended3 = DBCursor.GetValue(2)

		' Word-Information-Ex
		sCommand = "SELECT MustKnow, AdditionalTargetLangInfo FROM " & SelectedGroup() & " WHERE WordNumber=" & m_iWordNumber & ";"
		ExecuteReader(sCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then m_bMustKnow = False Else m_bMustKnow = DBCursor.GetBoolean(0)
		If TypeOf (DBCursor.GetValue(1)) Is DBNull Then m_sAdditionalTargetLangInfo = "" Else m_sAdditionalTargetLangInfo = DBCursor.GetString(1)

		' Word
		sCommand = "SELECT UnitNumber, ChapterNumber, WordInUnit FROM " & SelectedGroup() & " WHERE WordNumber=" & m_iWordNumber & ";"
		ExecuteReader(sCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then m_iUnit = -1 Else m_iUnit = DBCursor.GetValue(0)
		If TypeOf (DBCursor.GetValue(1)) Is DBNull Then m_iChapter = -1 Else m_iChapter = DBCursor.GetValue(1)
		If TypeOf (DBCursor.GetValue(2)) Is DBNull Then m_iWordInUnit = -1 Else m_iWordInUnit = DBCursor.GetValue(2)

		If m_iWordNumber > 1 Then sCommand = "SELECT LetzteAbfrage FROM " & SelectedGroup() & "Stats WHERE WordNumber=" & m_iWordNumber - 1 & ";"
		ExecuteReader(sCommand)
		DBCursor.Read()
		'If TypeOf (dbCursor.GetValue(0)) Is DBNull Thenm_sLastTested = "01.01.1900" Else 
		m_sLastTested = DBCursor.GetValue(0)
		DBCursor.Close()

		sCommand = "SELECT UnitNumber, ChapterNumber, WordInUnit FROM " & SelectedGroup() & " WHERE WordNumber=" & m_iWordNumber & ";"
		ExecuteReader(sCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then m_iUnit = -1 Else m_iUnit = DBCursor.GetValue(0)
		If TypeOf (DBCursor.GetValue(1)) Is DBNull Then m_iChapter = -1 Else m_iChapter = DBCursor.GetValue(1)
		If TypeOf (DBCursor.GetValue(2)) Is DBNull Then m_iWordInUnit = -1 Else m_iWordInUnit = DBCursor.GetValue(2)
		'm_sUnit = GetUnit(m_iUnit)
		DBCursor.Close()

		sCommand = "SELECT LetzteAbfrage FROM " & SelectedGroup() & "Stats WHERE WordNumber=" & m_iWordNumber & ";"
		'Application.DoEvents()
		ExecuteReader(sCommand)
		DBCursor.Read()
		'If TypeOf (dbCursor.GetValue(0)) Is DBNull Thenm_sLastTested = "01.01.1900" Else 
		m_sLastTested = DBCursor.GetValue(0)
		DBCursor.Close()

		m_bValid = True
	End Sub

	Public Sub LoadWord(ByVal WordNumber As Integer, ByVal sTable As String)
		Me.m_sTable = sTable
		Me.m_iWordNumber = WordNumber
		LoadWord()
	End Sub

	Protected Function SelectedGroup() As String
		Return m_sTable
	End Function

	Public Sub Update()
		' Speichern der geänderten informationen
		If IsConnected() = False Or m_bValid = False Then Exit Sub

		Dim sCommand As String
		sCommand = "UPDATE " & SelectedGroup() & " SET Word='" & AddHighColons(m_sWord)
		sCommand &= "', Pre='" & AddHighColons(m_sPre)
		sCommand &= "', Post='" & AddHighColons(m_sPost)
		sCommand &= "', AdditionalTargetLangInfo='" & AddHighColons(m_sAdditionalTargetLangInfo)
		sCommand &= "', Description='" & AddHighColons(m_sDescription)
		sCommand &= "' WHERE WordNumber=" & m_iWordNumber
		ExecuteNonQuery(sCommand)

		'Meaning
		Dim sMeaning As String = ""
		Dim i As Integer		  ' Index
		For i = 0 To m_aMeaning.Count - 1
			sMeaning = sMeaning & m_aMeaning.Item(i) & ";"
		Next i
		If Right(sMeaning, 1) = ";" Then sMeaning = Left(sMeaning, Len(sMeaning) - 1)

		sCommand = "UPDATE " & SelectedGroup() & " SET Meaning1='" & AddHighColons(sMeaning) & "' WHERE WordNumber=" & m_iWordNumber
		ExecuteNonQuery(sCommand)

		sCommand = "UPDATE " & SelectedGroup() & " SET Irregular1='" & AddHighColons(m_sExtended1)
		sCommand &= "', Irregular2='" & AddHighColons(m_sExtended2)
		sCommand &= "', Irregular3='" & AddHighColons(m_sExtended3)
		sCommand &= "', IrregularForm=" & m_bExtendedIsValid
		sCommand &= ", WordType=" & m_iWordType
		sCommand &= " WHERE WordNumber=" & m_iWordNumber
		ExecuteNonQuery(sCommand)

		Exit Sub





		If m_bExtendedIsValid = False Then
			m_sExtended1 = ""
			sCommand = "UPDATE " & SelectedGroup() & " SET Irregular1='" & AddHighColons(m_sExtended1) & "' WHERE WordNumber=" & m_iWordNumber & ";"
			ExecuteReader(sCommand)
			m_sExtended2 = ""
			sCommand = "UPDATE " & SelectedGroup() & " SET Irregular2='" & AddHighColons(m_sExtended1) & "' WHERE WordNumber=" & m_iWordNumber & ";"
			ExecuteReader(sCommand)
			m_sExtended3 = ""
			sCommand = "UPDATE " & SelectedGroup() & " SET Irregular2='" & AddHighColons(m_sExtended1) & "' WHERE WordNumber=" & m_iWordNumber & ";"
			ExecuteReader(sCommand)
		End If

		sCommand = "UPDATE " & SelectedGroup() & " SET MustKnow=" & m_bMustKnow & " WHERE WordNumber=" & m_iWordNumber & ";"
		ExecuteReader(sCommand)
		sCommand = "UPDATE " & SelectedGroup() & " SET ChapterNumber=" & m_iChapter & " WHERE WordNumber=" & m_iWordNumber & ";"
		ExecuteReader(sCommand)

	End Sub

	Public Property WordNumber()
		Get
			Return Me.m_iWordNumber
		End Get
		Set(ByVal Value)

		End Set
	End Property

	' Wort Propertys

	Property Word() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sWord
		End Get
		Set(ByVal Word As String)
			m_sWord = Word
		End Set
	End Property

	Property Pre() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sPre
		End Get
		Set(ByVal Pre As String)
			m_sPre = Pre
		End Set
	End Property

	Property Post() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sPost
		End Get
		Set(ByVal Post As String)
			m_sPost = Post
		End Set
	End Property

	ReadOnly Property Meaning() As Array
		Get
			Return m_aMeaning.ToArray
		End Get
	End Property

	Property Extended1() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sExtended1
		End Get
		Set(ByVal Irregular As String)
			m_sExtended1 = Irregular
		End Set
	End Property

	Property Extended2() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sExtended2
		End Get
		Set(ByVal Irregular As String)
			m_sExtended2 = Irregular
		End Set
	End Property

	Property Extended3() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sExtended3
		End Get
		Set(ByVal Irregular As String)
			m_sExtended3 = Irregular
		End Set
	End Property

	Property ExtendedIsValid() As Boolean
		Get
			If m_bValid = False Then Return Nothing
			Return m_bExtendedIsValid
		End Get
		Set(ByVal Extended As Boolean)
			m_bExtendedIsValid = Extended
		End Set
	End Property

	Property Description() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sDescription
		End Get
		Set(ByVal Description As String)
			m_sDescription = Description
		End Set
	End Property

	Property WordType() As Integer
		Get
			If m_bValid = False Then Return Nothing
			Return m_iWordType
		End Get
		Set(ByVal Value As Integer)
			m_iWordType = Value
		End Set
	End Property

	' wordinformationex

	Property MustKnow() As Boolean
		Get
			Return m_bMustKnow
		End Get
		Set(ByVal KnowType As Boolean)
			m_bMustKnow = KnowType
		End Set
	End Property

	Property AdditionalTargetLangInfo() As String
		Get
			Return m_sAdditionalTargetLangInfo
		End Get
		Set(ByVal Infotext As String)
			m_sAdditionalTargetLangInfo = Infotext
		End Set
	End Property

	' Word
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
			m_iChapter = Chapter
		End Set
	End Property

	ReadOnly Property WordInUnit() As Integer
		Get
			Return m_iWordInUnit
		End Get
	End Property
End Class