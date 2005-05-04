Imports System.Data.OleDb

Public Structure xlsVocInputGroupListInfo
	Public Table As String
	Public Description As String
	Public Type As String
End Structure

Public Class xlsVocInputGroupCollection
	Inherits CollectionBase

	Protected DBConnection As New CDBOperation
	Protected iCurrentItem As Integer
	'Protected sPath As String
	Protected sCommandText As String

	Public Sub New(ByVal db As CDBOperation)
		MyBase.New()
		DBConnection = db
		LoadInfos()
	End Sub

	Protected Sub LoadInfos()
		MyBase.Clear()
		' Laden der Gruppen informationen

		Dim structGroup As xlsVocInputGroupListInfo
		Dim oleCursor As OleDbDataReader
		sCommandText = "SELECT * FROM Tables ORDER BY Lehrbuch;"
		oleCursor = DBConnection.ExecuteReader(sCommandText)
		Do While oleCursor.Read
			structGroup = New xlsVocInputGroupListInfo
			If Not TypeOf (oleCursor.GetValue(0)) Is DBNull Then structGroup.Description = oleCursor.GetValue(0) Else structGroup.Description = ""
			If Not TypeOf (oleCursor.GetValue(1)) Is DBNull Then structGroup.Table = oleCursor.GetValue(1) Else structGroup.Table = ""
			If Not TypeOf (oleCursor.GetValue(2)) Is DBNull Then structGroup.Type = oleCursor.GetValue(2) Else structGroup.Type = ""
			List.Add(structGroup)
		Loop

		iCurrentItem = 0
	End Sub

	Public Sub Add(ByVal GroupName As String, ByVal LanguageIndex As Integer)
		Dim sLanguage As String
		Select Case LanguageIndex
			Case 1			  ' General
				sLanguage = "General"
			Case 2			  ' English
				sLanguage = "English"
			Case 3			  ' French
				sLanguage = "French"
			Case 4			  ' Latin
				sLanguage = "Latin"
			Case 5
				sLanguage = "Italian"
		End Select

		Dim iCount As Integer
		Dim oleCursor As OleDbDataReader
		Dim sCommandText As String
		sCommandText = "SELECT COUNT(Art) FROM Tables WHERE Art='" & sLanguage & "';"
		oleCursor = DBConnection.ExecuteReader(sCommandText)

		oleCursor.Read()
		If TypeOf (oleCursor.GetValue(0)) Is DBNull Then iCount = 0 Else iCount = oleCursor.GetValue(0)

		Dim sNewTable As String
		If iCount + 1 < 10 Then
			sNewTable = sLanguage & "0" & Trim(Str(iCount + 1))
		Else
			sNewTable = sLanguage & Trim(Str(iCount + 1))
		End If

		sCommandText = "INSERT INTO Tables VALUES ("
		sCommandText += "'" & xlsVocInput.AddHighColons(GroupName) & "',"
		sCommandText += "'" & sNewTable & "',"
		sCommandText += "'" & sLanguage & "'"
		sCommandText += ");"
		DBConnection.ExecuteNonQuery(sCommandText)

		CreateTables(sNewTable)
		LoadInfos()
	End Sub

	Public Sub AddExisting(ByVal TableName As String)
		CreateTables(TableName)
	End Sub

	Overridable Function Language(ByVal GroupName As String, ByVal LanguageIndex As Integer)
		MsgBox("The language changes will not be saved!", vbInformation)
	End Function

	Overridable Function Rename(ByVal GroupName As String, ByVal NewName As String)
		sCommandText = "UPDATE Tables SET Lehrbuch='" & xlsVocInput.AddHighColons(NewName) & "' WHERE Lehrbuch='" & GroupName & "'"
		DBConnection.ExecuteNonQuery(sCommandText)

		LoadInfos()
	End Function

	Overloads Sub RemoveAt(ByVal Index As Integer)
		MsgBox("Removeing is not yet supported!")
	End Sub

	Overloads Sub Clear()
		MsgBox("Clearing is not yet supported!")
	End Sub

	Default Public ReadOnly Property Item(ByVal Index As Integer) As xlsVocInputGroupListInfo
		Get
			iCurrentItem = Index
			Return CType(List.Item(Index), xlsVocInputGroupListInfo)
		End Get
	End Property

	Protected Function CreateTables(ByVal TableName As String)
		CreateTableWords(TableName)
		CreateTableWordsStats(TableName)
		CreateTableWordsUnits(TableName)
	End Function

	Protected Friend Sub CreateTableWords(ByVal Name As String)
		' Erstellt Tabellen nach Version 1.20
		sCommandText = "CREATE TABLE " & Name & " (UnitNumber INTEGER, ChapterNumber INTEGER, Word TEXT(50), "
		sCommandText += "WordNumber INTEGER, WordInUnit INTEGER, WordType INTEGER, MustKnow BIT, Pre TEXT(50), "
		sCommandText += "Post TEXT(50), Meaning1 TEXT(50), Meaning2 TEXT(50), Meaning3 TEXT(50), IrregularForm BIT, "
		sCommandText += "Irregular1 TEXT(50), Irregular2 TEXT(50), Irregular3 TEXT(50), Description TEXT(80), Deleted BIT, "
		sCommandText += "AdditionalTargetLangInfo TEXT(80))"
		DBConnection.ExecuteNonQuery(sCommandText)
	End Sub

	Protected Friend Sub CreateTableWordsStats(ByVal Name As String)
		sCommandText = "CREATE TABLE " & Name & "Stats (WordNumber INTEGER, Abfragen INTEGER, AbfragenGesamt INTEGER, "
		sCommandText += "Richtig INTEGER, Falsch INTEGER, FalschGesamt INTEGER, ErsteAbfrage DATETIME, LetzteAbfrage DATETIME, "
		sCommandText += "AbfrageGestartet BIT, Hilfe1Richtig INT, Hilfe2Richtig INT, Hilfe3Richtig INT)"
		DBConnection.ExecuteNonQuery(sCommandText)
	End Sub

	Protected Friend Sub CreateTableWordsUnits(ByVal Name As String)
		sCommandText = "CREATE TABLE " & Name & "Units (Nummer INTEGER, Name TEXT(50))"
		DBConnection.ExecuteNonQuery(sCommandText)
		sCommandText = "CREATE UNIQUE INDEX IndexDoppelt ON " & Name & "Units (Nummer)"
		DBConnection.ExecuteNonQuery(sCommandText)
	End Sub
End Class
