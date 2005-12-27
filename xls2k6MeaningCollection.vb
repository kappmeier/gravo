Imports System.Data.OleDb

Public Class xlsMeaningCollection
	Inherits xlsCollection

	Dim m_iWordNumber As Integer = -1

	Public Sub New(ByVal db As CDBOperation)
		MyBase.New(db)
	End Sub

	Public Sub New(ByVal db As CDBOperation, ByVal Table As String, ByVal Number As Integer)
		MyBase.New(db)
		SetWord(Table, Number)
	End Sub

	Public Shadows Sub Add(ByVal Meaning As String)
		If Count >= 3 Then Exit Sub ' Hier gehört eine Exception rein, das klappt noch nicht!
		If CurrentGroupName = "" Then Exit Sub
		If IsConnected() = False Then Exit Sub
		Dim sCommand = "UPDATE " & CurrentGroupName & " SET Meaning" & Count + 1 & "='" & AddHighColons(Meaning) & "' WHERE WordNumber=" & m_iWordNumber & ";"
		ExecuteReader(sCommand)
		m_clist.Add(Meaning)
	End Sub

	Public Sub SetWord(ByVal Table As String, ByVal Number As Integer)
		Dim i As Integer
		Dim sTemp As String
		Table = Table
		m_iWordNumber = Number
		Dim sCommand = "SELECT Meaning1, Meaning2, Meaning3 FROM " & Table & " WHERE WordNumber=" & m_iWordNumber & ";"
		ExecuteReader(sCommand)
		DBCursor.Read()
		For i = 0 To 2
			If Not TypeOf (DBcursor.GetValue(i)) Is DBNull Then sTemp = dbcursor.GetValue(i)
			If Not Trim(sTemp) = "" Then m_clist.Add(dbcursor.GetString(i))
		Next i
	End Sub

	Public Sub SetWord(ByVal Number As Integer)
		If CurrentGroupName = "" Then Return
		SetWord(CurrentGroupName, Number)
	End Sub

	Public Sub Update(ByVal OldMeaning As String, ByVal NewMeaning As String)

	End Sub

	Default Public Shadows ReadOnly Property Item(ByVal Index As Integer) As String
		Get
			Return MyBase.Item(Index)
		End Get
	End Property

End Class
