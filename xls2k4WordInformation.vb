Public Class xlsWordInformation
	Inherits xlsBase

	' Vokabel-Speicher-Ort
	Protected m_bValid As Boolean = False
	Protected m_iWordNumber As Integer

	' Vokabelinformationen
	Protected m_sWord As String	  'Vokabel
	Protected m_sPre As String	'Pre-Vokabel    (to, le, ...)
	Protected m_sPost As String	  'Post-Vokabel   (Plural, slang, ...)
	Protected m_sMeaning1 As String
	Protected m_sMeaning2 As String
	Protected m_sMeaning3 As String	  'Bedeutung
	Protected m_sExtended1 As String	  'Irregular
	Protected m_sExtended2 As String	  'Irregular
	Protected m_sExtended3 As String	  'Irregular
	Protected m_sDescription As String
	Protected m_bExtendedIsValid As Boolean	 'Vokabel hat irreguläre Formen
	Protected m_iWordType As Integer	 'Vokabelart (Nomen, Verb ...)

	Sub New(ByVal db As CDBOperation)
		MyBase.new(db)
		m_btableselected = False
	End Sub

	Sub New(ByVal db As CDBOperation, ByVal WordNumber As Integer, ByVal Table As String)
		MyBase.New(db)
		m_stable = Table
		m_bTableSelected = True
		m_iWordNumber = WordNumber
		LoadWord()
	End Sub

	Protected Sub LoadWord()
		If m_bConnected = False Then Exit Sub
		If m_bTableSelected = False Then Exit Sub
		Dim bDeleted As Boolean

		dbcommand = "SELECT Deleted FROM " & m_stable & " WHERE WordNumber=" & m_iWordNumber & ";"
		dbcursor = dbconnection.ExecuteReader(dbcommand)
		dbcursor.Read()
		If TypeOf (dbcursor.GetValue(0)) Is DBNull Then bDeleted = False Else bDeleted = DBCursor.GetValue(0)
		dbcursor.Close()
		If bDeleted Then
			m_bValid = False
			Exit Sub
		End If

		DBCommand = "SELECT Word, Meaning1, Meaning2, Meaning3, Pre, Post, Description FROM " & m_sTable & " WHERE WordNumber=" & m_iWordNumber & ";"
		DBCursor = DBConnection.ExecuteReader(DBCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then m_sWord = "" Else m_sWord = DBCursor.GetValue(0)
		If TypeOf (DBCursor.GetValue(1)) Is DBNull Then m_sMeaning1 = "" Else m_sMeaning1 = DBCursor.GetValue(1)
		If TypeOf (DBCursor.GetValue(2)) Is DBNull Then m_sMeaning2 = "" Else m_sMeaning2 = DBCursor.GetValue(2)
		If TypeOf (DBCursor.GetValue(3)) Is DBNull Then m_sMeaning3 = "" Else m_sMeaning3 = DBCursor.GetValue(3)
		If TypeOf (DBCursor.GetValue(4)) Is DBNull Then m_sPre = "" Else m_sPre = DBCursor.GetValue(4)
		If TypeOf (DBCursor.GetValue(5)) Is DBNull Then m_sPost = "" Else m_sPost = DBCursor.GetValue(5)
		If TypeOf (DBCursor.GetValue(6)) Is DBNull Then m_sDescription = "" Else m_sDescription = DBCursor.GetValue(6)

		DBCommand = "SELECT WordType, IrregularForm FROM " & m_sTable & " WHERE WordNumber=" & m_iWordNumber & ";"
		DBCursor = DBConnection.ExecuteReader(DBCommand)
		DBCursor.Read()
		If TypeOf (DBCursor.GetValue(0)) Is DBNull Then m_iWordType = 0 Else m_iWordType = DBCursor.GetValue(0)
		If TypeOf (DBCursor.GetValue(1)) Is DBNull Then m_bExtendedIsValid = False Else m_bExtendedIsValid = DBCursor.GetBoolean(1)

		If m_bExtendedIsValid Then
			DBCommand = "SELECT Irregular1, Irregular2, Irregular3 FROM " & m_sTable & " WHERE WordNumber=" & m_iWordNumber & ";"
			DBCursor = DBConnection.ExecuteReader(DBCommand)
			DBCursor.Read()
			If TypeOf (DBCursor.GetValue(0)) Is DBNull Then m_sExtended1 = "" Else m_sExtended1 = DBCursor.GetValue(0)
			If TypeOf (DBCursor.GetValue(1)) Is DBNull Then m_sExtended2 = "" Else m_sExtended2 = DBCursor.GetValue(1)
			If TypeOf (DBCursor.GetValue(2)) Is DBNull Then m_sExtended3 = "" Else m_sExtended3 = DBCursor.GetValue(2)
		Else
			m_sExtended1 = ""
			m_sExtended2 = ""
			m_sExtended3 = ""
		End If
		m_bValid = True
	End Sub

	Public Sub LoadWord(ByVal WordNumber As Integer)
		m_iWordNumber = WordNumber
		LoadWord()
	End Sub

	Public Sub LoadWord(ByVal WordNumber As Integer, ByVal Table As String)
		m_stable = Table
		m_btableselected = True
		LoadWord(WordNumber)
	End Sub

	Property Word() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sWord
		End Get
		Set(ByVal Word As String)
			If m_bconnected = False Or m_bValid = False Then Exit Property
			m_sWord = Word
			DBCommand = "UPDATE " & m_sTable & " SET Word='" & AddHighColons(m_sWord) & "' WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property

	Property Pre() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sPre
		End Get
		Set(ByVal Pre As String)
			If m_bconnected = False Or m_bValid = False Then Exit Property
			m_sPre = Pre
			DBCommand = "UPDATE " & m_sTable & " SET Pre='" & AddHighColons(m_sPre) & "' WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property

	Property Post() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sPost
		End Get
		Set(ByVal Post As String)
			If m_bconnected = False Or m_bValid = False Then Exit Property
			m_sPost = Post
			DBCommand = "UPDATE " & m_sTable & " SET Post='" & AddHighColons(m_sPost) & "' WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property

	Property Meaning1() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sMeaning1
		End Get
		Set(ByVal Meaning As String)
			If m_bconnected = False Or m_bValid = False Then Exit Property
			m_sMeaning1 = Meaning
			DBCommand = "UPDATE " & m_sTable & " SET Meaning1='" & AddHighColons(m_sMeaning1) & "' WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property

	Property Meaning2() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sMeaning2
		End Get
		Set(ByVal Meaning As String)
			If m_bconnected = False Or m_bValid = False Then Exit Property
			m_sMeaning2 = Meaning
			DBCommand = "UPDATE " & m_sTable & " SET Meaning2='" & AddHighColons(m_sMeaning2) & "' WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property

	Property Meaning3() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sMeaning3
		End Get
		Set(ByVal Meaning As String)
			If m_bconnected = False Or m_bValid = False Then Exit Property
			m_sMeaning3 = Meaning
			DBCommand = "UPDATE " & m_sTable & " SET Meaning3='" & AddHighColons(m_sMeaning3) & "' WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property

	Property Extended1() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sExtended1
		End Get
		Set(ByVal Irregular As String)
			If m_bconnected = False Or m_bValid = False Then Exit Property
			m_sExtended1 = Irregular
			DBCommand = "UPDATE " & m_sTable & " SET Irregular1='" & AddHighColons(m_sExtended1) & "' WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property

	Property Extended2() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sExtended2
		End Get
		Set(ByVal Irregular As String)
			If m_bconnected = False Or m_bValid = False Then Exit Property
			m_sExtended2 = Irregular
			DBCommand = "UPDATE " & m_sTable & " SET Irregular2='" & AddHighColons(m_sExtended2) & "' WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property

	Property Extended3() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sExtended3
		End Get
		Set(ByVal Irregular As String)
			If m_bconnected = False Or m_bValid = False Then Exit Property
			m_sExtended3 = Irregular
			DBCommand = "UPDATE " & m_sTable & " SET Irregular3='" & AddHighColons(m_sExtended3) & "' WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property

	Property ExtendedIsValid() As Boolean
		Get
			If m_bValid = False Then Return Nothing
			Return m_bExtendedIsValid
		End Get
		Set(ByVal Extended As Boolean)
			If m_bconnected = False Or m_bValid = False Then Exit Property
			m_bExtendedIsValid = Extended
			If Extended = False Then
				m_sExtended1 = ""
				DBCommand = "UPDATE " & m_sTable & " SET Irregular1='" & AddHighColons(m_sExtended1) & "' WHERE WordNumber=" & m_iWordNumber & ";"
				DBConnection.ExecuteReader(DBCommand)
				m_sExtended2 = ""
				dBCommand = "UPDATE " & m_sTable & " SET Irregular2='" & AddHighColons(m_sExtended1) & "' WHERE WordNumber=" & m_iWordNumber & ";"
				DBConnection.ExecuteReader(DBCommand)
				m_sExtended3 = ""
				DBCommand = "UPDATE " & m_sTable & " SET Irregular2='" & AddHighColons(m_sExtended1) & "' WHERE WordNumber=" & m_iWordNumber & ";"
				DBConnection.ExecuteReader(DBCommand)
			End If
			DBCommand = "UPDATE " & m_sTable & " SET IrregularForm=" & m_bExtendedIsValid & " WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property

	Property Description() As String
		Get
			If m_bValid = False Then Return Nothing
			Return m_sDescription
		End Get
		Set(ByVal Description As String)
			If m_bconnected = False Or m_bValid = False Then Exit Property
			m_sDescription = Description
			DBCommand = "UPDATE " & m_sTable & " SET Description='" & AddHighColons(m_sDescription) & "' WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property

	Property WordType() As Integer
		Get
			If m_bValid = False Then Return Nothing
			Return m_iWordType
		End Get
		Set(ByVal Value As Integer)
			If m_bconnected = False Or m_bValid = False Then Exit Property
			m_iWordType = Value
			DBCommand = "UPDATE " & m_sTable & " SET WordType=" & m_iWordType & " WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteReader(DBCommand)
		End Set
	End Property
End Class