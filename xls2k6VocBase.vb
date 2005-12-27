Imports System.Data.OleDb

Public Structure xlsUnitListInfo
	Public Number As Integer
	Public Name As String
End Structure

Public Structure xlsWordInformation
	Public Number As Integer
	Public Group As String
End Structure

Public Class xlsVocBase
	Inherits xlsDBBase

	' Variablen
	Private cWords As Collection	' Collection von xlsWordInformation
	Private cWordNames As Collection
	Private cWordNumbers As Collection
	Private m_xWord As xlsWord
	Private m_bWordSelected As Boolean = False
	' Klassenzustände

	Sub New(ByVal db As CDBOperation, ByVal Table As String)	' Bestimmte Tabelle zum Zugriff öffnen
		MyBase.new(db, Table)
	End Sub

	Sub New(ByVal db As CDBOperation)	   ' Keinen Speziellen Table auswählen
		MyBase.New(db)
	End Sub

	Public Function CountWords() As Integer
		Return cWordNumbers.Count
	End Function

	Public ReadOnly Property CurrentWord() As xlsWord
		Get
			Return m_xWord
		End Get
	End Property

	Public Overridable Property CurrentWordNumber() As Integer
		Get
			Return m_xWord.WordNumber
		End Get
		Set(ByVal iNumber As Integer)
			m_xWord = New xlsWord(DBConnection, iNumber, Me.CurrentGroupName)
		End Set
	End Property

	Public Function IsWordSelected() As Boolean
		Return m_bWordSelected
	End Function

	Private Sub LoadWordInfos()
		If Not Me.IsUnitSelected Then Exit Sub ' TODO exception

		Dim sCommand As String
		cWordNames = New Collection
		cWordNumbers = New Collection
		cWords = New Collection
		sCommand = "SELECT WordNumber, Word FROM " & CurrentGroupName & " WHERE UnitNumber=" & CurrentUnitNumber & " AND Deleted=" & False & " ORDER BY WordInUnit"
		ExecuteReader(sCommand)
		Dim wiNew As xlsWordInformation
		Do While DBCursor.Read
			cWordNumbers.Add(DBCursor.GetInt32(0))
			cWordNames.Add(DBCursor.GetString(1))
			wiNew = New xlsWordInformation
			wiNew.Group = CurrentGroupName
			wiNew.Number = DBCursor.GetInt32(0)
			cWords.Add(wiNew)
		Loop
		DBCursor.Close()
	End Sub

	Public Overloads Sub SelectUnit(ByVal iNumber As Int32)
		MyBase.SelectUnit(iNumber)
		LoadWordInfos()
	End Sub

	Public Overloads Sub SelectUnit(ByVal sName As String)
		MyBase.SelectUnit(sName)
		LoadWordInfos()
	End Sub

	Public ReadOnly Property WordNames() As Collection
		Get
			Return cWordNames
		End Get
	End Property

	Public ReadOnly Property WordNumbers() As Collection
		Get
			Return cWordNumbers
		End Get
	End Property

	Public ReadOnly Property Words() As Collection
		Get
			Return cWords
		End Get
	End Property





	' Löschen oder auch nicht ?`????? ß? ß??? ???? ??? ?? ?
	Overridable Function GetWord(ByVal WordNumber As Int32) As xlsWord
		If IsConnected() = False Or IsGroupSelected() = False Then Return Nothing

		CurrentWord.LoadWord(WordNumber, CurrentGroupName)
		SelectUnit(CurrentWord.UnitNumber)		 'm_iUnit = wtWord.UnitNumber
		'm_iWordNumber = WordNumber
		Return CurrentWord
	End Function
End Class
