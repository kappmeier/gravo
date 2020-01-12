Imports System.Collections.ObjectModel
Imports Gravo.AccessDatabaseOperation

Public Class xlsManagement
    Inherits xlsBase

    Public Sub New(ByRef db As IDataBaseOperation)
        MyBase.New(db)
    End Sub

    Public Sub CopyGobalCardsToGroups()
        Dim command As String
        Dim xlsGrp As New xlsGroups(Me.DBConnection)
        Dim GroupsDao As IGroupsDao = New GroupsDao(Me.DBConnection)
        Dim Groups As Collection(Of GroupEntry) = GroupsDao.GetAllGroups()
        For Each Group As GroupEntry In Groups
            command = "SELECT [WordIndex] FROM [" & Group.Table & "];"
            DBConnection.ExecuteReader(command)
            Dim indices As New Collection(Of Integer)
            While DBConnection.DBCursor.Read()
                Dim index = DBConnection.SecureGetInt32(0)
                ' speichere den Index in ein Array
                indices.Add(index)
            End While

            Dim dict As New xlsDictionary(Me.DBConnection)
            For Each index As Integer In indices
                command = "SELECT [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain] FROM [Cards] WHERE [Index]=" & index & ";"
                DBConnection.ExecuteReader(command)
                DBConnection.DBCursor.Read()
                Dim testInterval As Integer = DBConnection.SecureGetInt32(0)
                Dim counter As Integer = DBConnection.SecureGetInt32(1)
                Dim lastDateTemp As System.DateTime = DBConnection.SecureGetDateTime(2)
                Dim lastDate As String = lastDateTemp.Day & "." & lastDateTemp.Month & "." & lastDateTemp.Year
                Dim testIntervalMain As Integer = DBConnection.SecureGetInt32(3)
                Dim counterMain As Integer = DBConnection.SecureGetInt32(4)
                DBConnection.DBCursor.Close()

                'speichern
                command = "UPDATE [" & Group.Table & "] SET [TestInterval] = " & GetDBEntry(testInterval) & ", [Counter] = " & GetDBEntry(counter) & ",[LastDate] = " & GetDBEntry(lastDate) & ",[TestIntervalMain] = " & GetDBEntry(testIntervalMain) & ",[CounterMain] = " & GetDBEntry(counterMain) & " WHERE [WordIndex]=" & index & ";"
                DBConnection.ExecuteNonQuery(command)
            Next index
        Next Group
    End Sub
End Class
