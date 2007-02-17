Imports System.Data.OleDb

Public Class xlsCards
  Inherits xlsBase

  Sub AddNewEntry()
    ' TODO irgendwie vernünftiger regeln ;)
    Dim sCommand As String = "INSERT INTO Cards ( [Counter] ) SELECT 1 AS A;"
    DBConnection.ExecuteNonQuery(sCommand)
  End Sub

  Sub Update(ByVal WordNumber As Integer, ByVal TestResult As Boolean)
    ' alte daten holen
    Dim sCommand As String = "SELECT TestInterval FROM Cards WHERE Index=" & WordNumber & ";"
    'sCommand = "SELECT Cards.TestInterval FROM(Cards) WHERE (((Cards.Index)=662));"

    Dim DBCursor As OleDbDataReader = DBConnection.ExecuteReader(sCommand)
    DBCursor.Read()
    Dim interval As Integer = SecureGetInt32(DBCursor, 0)
    If TestResult Then
      ' richtig abgefragt, intervall erhöhen, counter neu setzen
      interval *= 2
      'sCommand = "UPDATE Cards SET TestInterval=" & iInterval & ", Counter=" & iInterval & ", LastDate='" & NowDB() & "' WHERE Index=" & WordNumber & ";"
      sCommand = "UPDATE Cards SET Cards.TestInterval = " & interval & ", Cards.[Counter] = " & interval & ", Cards.LastDate = '" & NowDB() & "' WHERE (((Cards.Index)=" & WordNumber & "));"
      DBConnection.ExecuteNonQuery(sCommand)
    Else
      ' falsch abgefragt, intervall verringern, falls möglich und counter neu setzen
      interval = Math.Max((interval / 2), 1)
      sCommand = "UPDATE Cards SET Cards.TestInterval = " & interval & ", Cards.[Counter] = " & interval & ", Cards.LastDate = '" & NowDB() & "' WHERE (((Cards.Index)=" & WordNumber & "));"
      'sCommand = "UPDATE Cards SET TestInterval=" & iInterval & ", Counter=" & iInterval & ", LastDate='" & NowDB() & "' WHERE Index=" & WordNumber & ";"
      DBConnection.ExecuteNonQuery(sCommand)
    End If
  End Sub

  Sub Update(ByVal WordNumber As Integer)
    ' Update ohne Ergebnis, d.h. es wurde zur Abfrage ausgewählt, die Abfrage wird aber übersprungen
    Dim sCommand As String = "SELECT Counter, TestInterval FROM Cards WHERE Index=" & WordNumber & ";"
    Dim DBCursor As OleDbDataReader = DBConnection.ExecuteReader(sCommand)
    DBCursor.Read()
    Dim counter As Integer = SecureGetInt32(DBCursor, 0)

    If counter = 1 Then Throw New xlsExceptionCards(1) ' Counter kann nicht verringert werden
    If counter <= 0 Then Throw New xlsExceptionCards(0) ' Darf nicht vorkommen, evtl. Datenbank inkonsistent!

    ' Counter war größer als 1. Um 1 verringert speichern.
    sCommand = "UPDATE Cards SET Cards.Counter=" & counter - 1 & " WHERE Cards.Index=" & WordNumber & ";"
    DBConnection.ExecuteNonQuery(sCommand)
  End Sub
End Class