Imports System.Collections.ObjectModel
Imports Gravo2k8.AccessDatabaseOperation

Public Class xlsGroups
  Inherits xlsBase

  ' Standardkonstruktor
  Sub New()
    MyBase.New()
  End Sub

  Sub New(ByVal db As AccessDatabaseOperation)    ' Keinen Speziellen Table auswählen
    MyBase.New(db)
  End Sub

  Public Function GetAllGroups() As Collection(Of xlsGroupEntry)
    ' Alle Gruppen ausgeben
    Dim cGroups As New Collection(Of xlsGroupEntry)
    Dim indices As New Collection(Of Integer)
    Dim command As String = "SELECT Index FROM Groups;"
    DBConnection.ExecuteReader(command)
    Do While DBConnection.DBCursor.Read
      indices.Add(DBConnection.SecureGetInt32(0))
    Loop
    DBConnection.DBCursor.Close()
    For Each Index As Integer In indices
      Dim entry As New xlsGroupEntry(DBConnection)
      entry.LoadGroup(Index)
      cGroups.Add(entry)
    Next
    Return cGroups
  End Function

  Public Function GetGroups() As Collection(Of String)
    ' Alle Ober-Gruppen ausgeben
    Dim groupNames As New Collection(Of String)
    Dim command As String = "SELECT DISTINCT GroupName FROM Groups;"
    DBConnection.ExecuteReader(command)
    Do While DBConnection.DBCursor.Read
      groupNames.Add(DBConnection.SecureGetString(0))
    Loop
    DBConnection.DBCursor.Close()
    Return groupNames
  End Function

  Public Function GetGroup(ByVal group As String, ByVal subGroup As String) As xlsGroup
    Dim command As String = "SELECT GroupTable FROM Groups WHERE GroupName=" & GetDBEntry(group) & " AND GroupSubName=" & GetDBEntry(subGroup) & ";"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Return Nothing
    DBConnection.DBCursor.Read()
    Dim retGroup As New xlsGroup(DBConnection.SecureGetString(0))
    retGroup.DBConnection = DBConnection
    DBConnection.DBCursor.Close()
    Return retGroup
  End Function

  Public Function GetSubGroups(ByVal groupName As String) As Collection(Of xlsGroupEntry)
    ' Alle Gruppen ausgeben
    Dim subGroups As New Collection(Of xlsGroupEntry)
    Dim indices As New Collection(Of Integer)
    Dim command As String = "SELECT Index FROM Groups WHERE GroupName='" & groupName & "';"
    DBConnection.ExecuteReader(command)
    Do While DBConnection.DBCursor.Read
      indices.Add(DBConnection.SecureGetInt32(0))
    Loop
    DBConnection.DBCursor.Close()
    For Each index As Integer In indices
      Dim entry As New xlsGroupEntry(DBConnection)
      entry.LoadGroup(index)
      subGroups.Add(entry)
    Next
    Return subGroups
  End Function

  Public Sub AddGroup(ByVal groupName As String, ByVal subGroupName As String)
    ' Testen, ob bereits ein Wort unter dem Eintrag existiert
    Dim command As String = "SELECT [Index] FROM Groups WHERE Groups.GroupName=" & GetDBEntry(groupName) & " AND Groups.GroupSubName=" & GetDBEntry(subGroupName) & ";"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows Then
      DBConnection.DBCursor.Close()
      Throw New xlsExceptionEntryExists("Eine Untergruppe mit diesem Namen existiert schon.")
    End If

    ' Herausfinden, wieviele Untergruppen zu der angegebenen Gruppe vorhanden sind
    command = "SELECT COUNT(Index) FROM Groups WHERE Groups.GroupName=" & GetDBEntry(groupName) & ";"

    DBConnection.ExecuteReader(command)
    DBConnection.DBCursor.Read()
    Dim groupCount As Integer = DBConnection.SecureGetInt32(0) + 1
    DBConnection.DBCursor.Close()

    ' bestimme Tabellenname
    Dim tableName As String
    If groupCount < 10 Then
      tableName = "Group" & StripSpecialCharacters(groupName) & "0" & groupCount
    Else
      tableName = "Group" & StripSpecialCharacters(groupName) & groupCount
    End If

        command = "CREATE TABLE [" & tableName & "] ([Index] AUTOINCREMENT, [WordIndex] LONG NOT NULL, [Marked] BIT, [Example] TEXT(64), [TestInterval] INT NOT NULL, [Counter] INT NOT NULL, [LastDate] DATETIME NOT NULL, [TestIntervalMain] INT NOT NULL, [CounterMain] INT NOT NULL, CONSTRAINT prkey PRIMARY KEY ([Index]) );"
    DBConnection.ExecuteNonQuery(command)

		command = "INSERT INTO Groups (GroupName, GroupSubName, GroupTable) VALUES (" & GetDBEntry(groupName) & ", " & GetDBEntry(subGroupName) & ", " & GetDBEntry(tableName) & ");"
    DBConnection.ExecuteNonQuery(command)
  End Sub

  Public Sub EditGroup(ByVal groupName As String, ByVal newName As String)
    ' Teste zuerst, ob die neue Gruppe schon existiert
    If IsGroupExisting(newName) Then Throw New xlsExceptionEntryExists("Gruppe " & newName & " existiert bereits.")

    ' Kopiere _alle_ Einträge in neu erstellte Tabellen. Scheinbar gehts nicht anders...
    For Each subGroup As xlsGroupEntry In Me.GetSubGroups(groupName)
      AddGroup(newName, subGroup.SubGroup)
      ' kopieren der einträge
      Dim grpOld As xlsGroup = Me.GetGroup(groupName, subGroup.SubGroup)
      Dim grpNew As xlsGroup = Me.GetGroup(newName, subGroup.SubGroup)
      For Each index As Integer In grpOld.GetIndices()
        Dim marked As Boolean = grpOld.GetMarked(index)
        ' TODO example
        grpNew.Add(index, marked, "")
      Next index
    Next subGroup

    ' Lösche die Gruppe
    DeleteGroup(groupName)
  End Sub

  Public Sub EditSubGroup(ByVal groupName As String, ByVal subGroupName As String, ByVal newSubGroupName As String)
    Dim command As String = "UPDATE Groups SET GroupSubName=" & GetDBEntry(newSubGroupName) & " WHERE GroupName=" & GetDBEntry(groupName) & " AND GroupSubName=" & GetDBEntry(subGroupName) & ";"
    DBConnection.ExecuteNonQuery(command)
  End Sub

  Public Sub DeleteGroup(ByVal groupName As String)
    ' Löschen der Tabellen
    Dim command As String = "SELECT GroupTable FROM Groups WHERE GroupName=" & GetDBEntry(groupName) & ";"
    Dim tables As New Collection(Of String)
    DBConnection.ExecuteReader(command)
    While DBConnection.DBCursor.Read()
      tables.Add(DBConnection.SecureGetString(0))
    End While
    DBConnection.DBCursor.Close()

    For Each table As String In tables
      command = "DROP TABLE " & table
      DBConnection.ExecuteNonQuery(command)
    Next

    ' Löschen der Einträge aus der Group-Table
    command = "DELETE FROM Groups WHERE GroupName=" & GetDBEntry(groupName) & ";"
    DBConnection.ExecuteNonQuery(command)
  End Sub

  Public Function WordCount(ByVal groupName As String) As Integer
    Dim command As String = "SELECT GroupTable FROM Groups WHERE GroupName=" & GetDBEntry(groupName) & ";"
    Dim tables As New Collection(Of String)
    DBConnection.ExecuteReader(command)
    While DBConnection.DBCursor.Read()
      tables.Add(DBConnection.SecureGetString(0))
    End While
    DBConnection.DBCursor.Close()

    Dim counter As Integer = 0
    For Each table As String In tables
      Dim group As New xlsGroup(table)
      group.DBConnection = DBConnection
      counter += group.WordCount
    Next
    Return counter
  End Function

  Public Function WordCount(ByVal groupName As String, ByVal subGroupName As String) As Integer
    Dim command As String = "SELECT GroupTable FROM Groups WHERE GroupName=" & GetDBEntry(groupName) & " AND GroupSubName=" & GetDBEntry(subGroupName) & ";"
    DBConnection.ExecuteReader(command)
    DBConnection.DBCursor.Read()
    Dim table As String = DBConnection.SecureGetString(0)
    DBConnection.DBCursor.Close()

    Dim group As New xlsGroup(table)
    group.DBConnection = DBConnection

    Return group.WordCount
  End Function

  Public Function IsGroupExisting(ByVal groupName As String) As Boolean
    Dim command As String = "SELECT DISTINCT GroupTable FROM Groups WHERE GroupName=" & GetDBEntry(groupName) & ";"
    DBConnection.ExecuteReader(command)
    Dim ret As Boolean
    If DBConnection.DBCursor.HasRows Then
      ret = True
    Else
      ret = False
    End If
    DBConnection.CloseReader()
    Return ret
  End Function

  Public Function SubGroupCount(ByVal groupName As String) As Integer
    Dim command As String = "SELECT COUNT([Index]) FROM Groups WHERE GroupName=" & GetDBEntry(groupName) & ";"
    DBConnection.ExecuteReader(command)
    DBConnection.DBCursor.Read()
    Dim count As Integer = DBConnection.SecureGetInt32(0)
    DBConnection.DBCursor.Close()
    Return count
	End Function

	Public Function UsedLanguagesCount(ByVal groupName As String) As Integer
		Return GetUsedLanguages(groupName).Count
	End Function

	Public Function GetUsedLanguages(ByVal groupname As String) As SortedList(Of String, String)
		Dim command As String = "SELECT GroupTable FROM Groups WHERE GroupName=" & GetDBEntry(groupname) & ";"
		Dim tables As New Collection(Of String)
		DBConnection.ExecuteReader(command)
		While DBConnection.DBCursor.Read()
			tables.Add(DBConnection.SecureGetString(0))
		End While
		DBConnection.DBCursor.Close()

		Dim usedLanguages As SortedList(Of String, String) = New SortedList(Of String, String)
		For Each table As String In tables
			Dim group As New xlsGroup(table)
			group.DBConnection = DBConnection
			Dim groupLanguages As Collection(Of String) = group.GetLanguages
			For Each language As String In groupLanguages
				If Not usedLanguages.ContainsKey(language) Then usedLanguages.Add(language, language)
			Next language
		Next table
		Return usedLanguages
	End Function
End Class