Imports System.Data.OleDb

Public Class xlsVocBase
	Inherits xlsBase

	' Klassenzustände
	Protected m_bTestMode As Boolean = False

	' Vokabeln
	Protected wtUnitsInGroup As xlsUnitCollection
	Protected wtWord As xlsWordStats

	Protected m_iUnit As Integer
	Protected m_iWordNumber As Integer

	' Unsortiert
	Public Groups As xlsVocInputGroupCollection

	Sub New(ByVal db As CDBOperation, ByVal Table As String)	' Bestimmte Tabelle zum Zugriff öffnen
		MyBase.new(db, Table)
		m_bTestMode = False
		Groups = New xlsVocInputGroupCollection(db)
		wtUnitsInGroup = New xlsUnitCollection(db)
		wtWord = New xlsWord(db)
	End Sub

	Sub New(ByVal db As CDBOperation)	   ' Keinen Speziellen Table auswählen
		MyBase.New(db)
		m_bTestMode = False
		Groups = New xlsVocInputGroupCollection(db)
		wtUnitsInGroup = New xlsUnitCollection(db)
		wtWord = New xlsWordStats(db)
	End Sub

	Sub SelectTable(ByVal Table As String)
		m_sTable = Table
		m_bTableSelected = True
		m_bTestMode = False
	End Sub

	Sub CloseTable()
		m_sTable = ""		  ' Zur Sicherheit, später überflüssig TODO
		m_bTableSelected = False
		m_bTestMode = False
	End Sub

	Sub Close()
		If m_bConnected = False Then Exit Sub
		DBConnection.Close()
		m_bConnected = False
	End Sub

	Overridable ReadOnly Property UnitsInGroup() As xlsUnitCollection
		Get
			If m_bConnected = False Then Exit Property
			If (m_bTestMode = True) Or (m_bTableSelected = False) Then Exit Property

			wtUnitsInGroup.LoadGroup(m_stable)
			Return wtUnitsInGroup
		End Get
	End Property

	Overridable ReadOnly Property WordsInUnit(ByVal UnitNumber As Int32) As xlsWordCollection
		Get
			Dim wtWordsInUnit As xlsWordCollection = New xlsWordCollection(dbconnection)
			If m_bConnected = False Then Exit Property
			If (m_bTableSelected = False) Then Exit Property

			wtWordsInUnit.LoadUnit(UnitNumber, m_stable)
			Return wtWordsInUnit
		End Get
	End Property

	Overridable ReadOnly Property WordsInUnit(ByVal UnitNumber As Int32, ByVal RequestedOnly As Boolean) As xlsWordCollection
		Get
			Dim wtWordsInUnit As xlsWordCollection = New xlsWordCollection(dbconnection)
			If m_bConnected = False Then Exit Property
			If (m_bTableSelected = False) Then Exit Property

			wtWordsInUnit.LoadTestUnit(UnitNumber, m_Stable, RequestedOnly)
			Return wtWordsInUnit
		End Get
	End Property

	Overridable Function GetWord() As xlsWordStats
		If m_bconnected = False Or m_btableselected = False Then Return Nothing

		Return wtWord
	End Function

	Overridable Function GetWord(ByVal WordNumber As Int32) As xlsWord
		If m_bconnected = False Or m_btableselected = False Then Return Nothing

		wtWord.LoadWord(WordNumber, m_stable)
		m_iUnit = wtWord.UnitNumber
		m_iWordNumber = WordNumber
		Return wtWord
	End Function
End Class
