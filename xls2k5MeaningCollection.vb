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
		If m_stable = "" Then Exit Sub
		If m_bconnected = False Then Exit Sub
		DBCommand = "UPDATE " & m_sTable & " SET Meaning" & Count + 1 & "='" & AddHighColons(Meaning) & "' WHERE WordNumber=" & m_iWordNumber & ";"
		DBConnection.ExecuteReader(DBCommand)
		m_clist.Add(Meaning)
	End Sub

	Public Sub SetWord(ByVal Table As String, ByVal Number As Integer)
		Dim i As Integer
		Dim sTemp As String
		m_stable = Table
		m_iWordNumber = Number
		DBCommand = "SELECT Meaning1, Meaning2, Meaning3 FROM " & m_sTable & " WHERE WordNumber=" & m_iWordNumber & ";"
		DBCursor = DBConnection.ExecuteReader(DBCommand)
		DBCursor.Read()
		For i = 0 To 2
			If Not TypeOf (DBcursor.GetValue(i)) Is DBNull Then sTemp = dbcursor.GetValue(i)
			If Not Trim(sTemp) = "" Then m_clist.Add(dbcursor.GetString(i))
		Next i
	End Sub

	Public Sub SetWord(ByVal Number As Integer)
		If m_stable = "" Then Return
		SetWord(m_stable, Number)
	End Sub

	Public Sub Update(ByVal OldMeaning As String, ByVal NewMeaning As String)

	End Sub

	Default Public Shadows ReadOnly Property Item(ByVal Index As Integer) As String
		Get
			Return MyBase.Item(Index)
		End Get
	End Property

End Class
