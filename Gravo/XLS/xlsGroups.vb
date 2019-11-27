Imports System.Collections.ObjectModel
Imports Gravo.AccessDatabaseOperation

Public Class xlsGroups
    Inherits xlsBase

    ' Standardkonstruktor
    Sub New()
        MyBase.New()
    End Sub

    Sub New(ByVal db As IDataBaseOperation)    ' Keinen Speziellen Table auswählen
        MyBase.New(db)
    End Sub

    ' Delete. Replaced by Load in GroupDao
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

End Class