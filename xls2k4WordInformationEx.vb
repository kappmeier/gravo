
Public Class xlsWordInformationEx
	Inherits xlsWordInformation

	' Vokabel erweitert
	Protected m_bMustKnow As Boolean	 'Vokabel muﬂ nicht gewuﬂt werden
	Protected m_sAdditionalTargetLangInfo	' Beschreibung der gesuchten Vokabel

	Sub New(ByVal db As CDBOperation)
		MyBase.New(db)
	End Sub

	Sub New(ByVal db As CDBOperation, ByVal WordNumber As Integer, ByVal Group As String)
		MyBase.New(db, WordNumber, Group)
		LoadWord()
	End Sub

	Protected Overloads Sub LoadWord()
		MyBase.LoadWord()
		If m_bvalid = False Then Exit Sub

		DBCommand = "SELECT MustKnow, AdditionalTargetLangInfo FROM " & m_sTable & " WHERE WordNumber=" & m_iWordNumber & ";"
		DBCursor = DBConnection.ExecuteReader(DBCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then m_bMustKnow = False Else m_bMustKnow = DBCursor.GetBoolean(0)
		If TypeOf (dbcursor.GetValue(1)) Is DBNull Then m_sAdditionalTargetLangInfo = "" Else m_sAdditionalTargetLangInfo = dbcursor.GetString(1)
	End Sub

	Public Overloads Sub LoadWord(ByVal WordNumber As Integer)
		m_iWordNumber = WordNumber
		LoadWord()
	End Sub

	Public Overloads Sub LoadWord(ByVal WordNumber As Integer, ByVal Group As String)
		m_stable = Group
		m_btableselected = True
		LoadWord(WordNumber)
	End Sub

	Property MustKnow() As Boolean
		Get
			Return m_bMustKnow
		End Get
		Set(ByVal KnowType As Boolean)
			If m_bValid = False Then Exit Property
			m_bMustKnow = KnowType
			DBCommand = "UPDATE " & m_sTable & " SET MustKnow=" & m_bMustKnow & " WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property

	Property AdditionalTargetLangInfo() As String
		Get
			Return m_sAdditionalTargetLangInfo
		End Get
		Set(ByVal Infotext As String)
			If m_bvalid = False Then Exit Property
			m_sAdditionalTargetLangInfo = Infotext
			DBCommand = "UPDATE " & m_sTable & " SET AdditionalTargetLangInfo='" & AddHighColons(Infotext) & "' WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property
End Class
