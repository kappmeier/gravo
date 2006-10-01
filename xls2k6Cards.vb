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
    Dim sCommand As String = "SELECT Interval FROM Cards WHERE Index=" & WordNumber & ";"
    sCommand = "SELECT Cards.TestInterval FROM(Cards) WHERE (((Cards.Index)=662));"

    Dim DBCursor As OleDbDataReader = DBConnection.ExecuteReader(sCommand)
    DBCursor.Read()
    Dim iInterval As Integer = SecureGetInt32(DBCursor, 0)
    If TestResult Then
      ' richtig abgefragt, intervall erhöhen, counter neu setzen
      iInterval *= 2
      'sCommand = "UPDATE Cards SET TestInterval=" & iInterval & ", Counter=" & iInterval & ", LastDate='" & NowDB() & "' WHERE Index=" & WordNumber & ";"
      sCommand = "UPDATE Cards SET Cards.TestInterval = " & iInterval & ", Cards.[Counter] = " & 1 & ", Cards.LastDate = '" & NowDB() & "' WHERE (((Cards.Index)=" & WordNumber & "));"

      DBConnection.ExecuteNonQuery(sCommand)
    Else
      ' falsch abgefragt, intervall verringern, falls möglich und counter neu setzen
      iInterval = Math.Max((iInterval / 2), 1)
      sCommand = "UPDATE Cards SET Cards.TestInterval = " & iInterval & ", Cards.[Counter] = " & 1 & ", Cards.LastDate = '" & NowDB() & "' WHERE (((Cards.Index)=" & WordNumber & "));"
      'sCommand = "UPDATE Cards SET TestInterval=" & iInterval & ", Counter=" & iInterval & ", LastDate='" & NowDB() & "' WHERE Index=" & WordNumber & ";"
      DBConnection.ExecuteNonQuery(sCommand)
    End If
  End Sub

  Sub Update(ByVal WordNumber As Integer)
    ' Update ohne ergebnis, d.h. es wurde zur abfrage ausgewählt, die abfrage wird aber übersprungen
    ' alte daten holen
    Dim sCommand As String = "SELECT Counter FROM Cards WHERE Index=" & WordNumber & ";"
    Dim DBCursor As OleDbDataReader = DBConnection.ExecuteReader(sCommand)
    DBCursor.Read()
    Dim iCounter As Integer = SecureGetInt32(DBCursor, 0)
    If iCounter = 1 Then Throw New xlsExceptionCards(1) ' Counter kann nicht verringert werden
    If iCounter <= 0 Then Throw New xlsExceptionCards(0) ' darf nicht vorkommen!
    ' counter neu setzen
    sCommand = "UPDATE Cards SET Cards.Counter=" & iCounter - 1 & " WHERE Cards.Index=" & WordNumber & ";"
    DBConnection.ExecuteNonQuery(sCommand)
  End Sub
End Class