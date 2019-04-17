Imports System.Collections.ObjectModel
Imports Gravo.AccessDatabaseOperation

Public Class xlsGroups
    Inherits xlsBase

    ' Standardkonstruktor
    Sub New()
        MyBase.New()
    End Sub

    Sub New(ByVal db As DataBaseOperation)    ' Keinen Speziellen Table auswählen
        MyBase.New(db)
    End Sub

    ' To data access layer
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

    ' To data access layer
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

    ' To data access layer
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

    ' To data access layer
    Public Function UsedLanguagesCount(ByVal groupName As String) As Integer
        Return GetUsedLanguages(groupName).Count
    End Function

    ' To data access layer
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