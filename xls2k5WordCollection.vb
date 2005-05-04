Imports System.Data.OleDb

Public Structure xlsWordListInfo
	Public WordNumber As Integer
	Public Word As String
	Public Group As String
End Structure

Public Class xlsWordCollection
	Inherits xlsBase

	Protected m_cList As Collection
	Protected m_iCount As Integer

	Public Sub New(ByVal db As CDBOperation)
		MyBase.New(db)
		m_cList = New Collection
	End Sub

	Public Sub Add(ByVal WordNumber As Integer, ByVal Word As String, ByVal Group As String)
		Dim wi As New xlsWordListInfo
		m_stable = Group
		wi.Group = Group
		wi.Word = Word
		wi.WordNumber = WordNumber
		m_cList.Add(wi)
	End Sub

	Public Sub Add(ByVal WordNumber As Integer, ByVal Group As String)
		Dim wi As New xlsWordListInfo
		Dim wtWordInfo As xlsWordInformation

		m_stable = Group
		wtWordInfo = New xlsWordInformation(dbconnection, WordNumber, Group)
		wi.Group = Group
		wi.Word = wtWordInfo.Word
		wi.WordNumber = WordNumber
		m_cList.Add(wi)
	End Sub

	Public Sub Add(ByRef WordInfo As xlsWordListInfo)
		m_cList.Add(WordInfo)
	End Sub

	Public Sub Clear()
		m_cList = Nothing
		m_cList = New Collection
	End Sub

	Public ReadOnly Property Count() As Integer
		Get
			Return m_cList.Count
		End Get
	End Property

	Default Public ReadOnly Property Item(ByVal Index As Integer) As xlsWordListInfo
		Get
			Return m_cList(Index)
		End Get
	End Property

	Public Sub LoadUnit(ByVal Unit As Integer, ByVal Group As String)
		Dim wtWordListInfo As xlsWordListInfo

		Clear()
		m_stable = Group
		DBCommand = "SELECT WordNumber, Word FROM " & m_sTable & " WHERE UnitNumber=" & Unit & " AND Deleted=" & False & " ORDER BY WordInUnit ;"
		DBCursor = DBConnection.ExecuteReader(DBCommand)
		Do While DBCursor.Read
			wtWordListInfo.WordNumber = DBCursor.GetInt32(0)
			wtWordListInfo.Word = dbcursor.GetString(1)
			wtWordListInfo.Group = Group
			Add(wtWordListInfo)
		Loop
	End Sub

	Public Sub LoadTestUnit(ByVal Unit As Integer, ByVal group As String, ByVal RequestedOnly As Boolean)
		Dim wtWordListInfo As xlsWordListInfo

		Clear()
		m_sTable = group
		If RequestedOnly = True Then
			DBCommand = "SELECT WordNumber, Word FROM " & m_sTable & " WHERE MustKnow=" & RequestedOnly & " AND UnitNumber=" & Unit & " AND Deleted=" & False & " ORDER BY WordInUnit ;"
		Else
			DBCommand = "SELECT WordNumber, Word FROM " & m_sTable & " WHERE UnitNumber=" & Unit & " AND Deleted=" & False & " ORDER BY WordInUnit ;"
		End If
		DBCursor = DBConnection.ExecuteReader(DBCommand)
		Do While DBCursor.Read
			wtWordListInfo.WordNumber = DBCursor.GetInt32(0)
			wtWordListInfo.Word = dbcursor.GetString(1)
			wtWordListInfo.Group = group
			Add(wtWordListInfo)
		Loop
	End Sub

	Public Sub Remove(ByVal i As Integer)
		m_cList.Remove(i)
		m_iCount -= 1
	End Sub
End Class

