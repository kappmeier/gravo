Imports System.Data.OleDb

Public Structure xlsUnitListInfo
	Public Number As Integer
	Public Name As String
End Structure

Public Class xlsUnitCollection
	Inherits xlsCollection

	Public Sub New(ByVal db As CDBOperation)
		MyBase.New(db)
		m_cList = New Collection
	End Sub

	Public Shadows Sub Add(ByVal Name As String)
		Dim wtUnit As New xlsUnitListInfo

		Dim iCount As Integer
		Dim oleCursor As OleDbDataReader
		Dim sCommandText As String
		sCommandText = "SELECT COUNT(Nummer) FROM " & m_stable & "Units;"
		oleCursor = DBConnection.ExecuteReader(sCommandText)

		oleCursor.Read()
		If TypeOf (oleCursor.GetValue(0)) Is DBNull Then iCount = 0 Else iCount = oleCursor.GetValue(0)
		iCount += 1

		sCommandText = "INSERT INTO " & m_stable & "Units VALUES ("
		sCommandText &= iCount & ","
		sCommandText += "'" & Name & "'"
		sCommandText += ");"
		DBConnection.ExecuteNonQuery(sCommandText)

		wtUnit.Name = Name
		wtUnit.Number = iCount
		m_cList.Add(wtUnit)
	End Sub

	Public Sub LoadGroup(ByVal Group As String)
		Dim iUnitNumber As Integer
		Dim sUnitName As String

		Clear()
		m_stable = Group
		DBCommand = "SELECT DISTINCT Nummer, Name FROM " & m_sTable & "Units ORDER BY Nummer;"
		DBCursor = DBConnection.ExecuteReader(DBCommand)
		Do While DBCursor.Read
			Dim wtUnit As New xlsUnitListInfo
			wtUnit.Number = DBCursor.GetInt32(0)
			wtUnit.Name = DBCursor.GetString(1)
			m_cList.Add(wtUnit)
		Loop
	End Sub

	Overridable Function Rename(ByVal UnitNumber As Integer, ByVal NewName As String)
		dbcommand = "UPDATE " & m_stable & "Units SET Name='" & xlsVocInput.AddHighColons(NewName) & "' WHERE Nummer=" & UnitNumber
		DBConnection.ExecuteNonQuery(DBCommand)

		LoadGroup(m_stable)
	End Function
End Class
