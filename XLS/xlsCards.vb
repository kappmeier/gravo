Imports Gravo2k7.AccessDatabaseOperation

Public Class xlsCards
  Inherits xlsBase

  Sub AddNewEntry(ByVal index As Integer)
    Dim command As String = "INSERT INTO Cards ([Index], [TestInterval], [Counter], [LastDate])VALUES (" & index & ", 1, 1, '01.01.1900');"
    DBConnection.ExecuteNonQuery(command)
  End Sub

  Sub Update(ByVal WordNumber As Integer, ByVal TestResult As Boolean)
    ' alte daten holen
    Dim command As String = "SELECT TestInterval FROM Cards WHERE Index=" & WordNumber & ";"

    DBConnection.ExecuteReader(command)
    DBConnection.DBCursor.Read()
    Dim interval As Integer = DBConnection.SecureGetInt32(0)
    DBConnection.DBCursor.Close()
    If TestResult Then
      ' richtig abgefragt, intervall erhöhen, counter neu setzen
      interval *= 2
      command = "UPDATE Cards SET Cards.TestInterval = " & interval & ", Cards.[Counter] = " & interval & ", Cards.LastDate = '" & NowDB() & "' WHERE (((Cards.Index)=" & WordNumber & "));"
      DBConnection.ExecuteNonQuery(command)
    Else
      ' falsch abgefragt, intervall verringern, falls möglich und counter neu setzen
      interval = Math.Max((interval / 2), 1)
      command = "UPDATE Cards SET Cards.TestInterval = " & interval & ", Cards.[Counter] = " & interval & ", Cards.LastDate = '" & NowDB() & "' WHERE (((Cards.Index)=" & WordNumber & "));"
      DBConnection.ExecuteNonQuery(command)
    End If
  End Sub

  Sub Update(ByVal WordNumber As Integer)
    ' Update ohne Ergebnis, d.h. es wurde zur Abfrage ausgewählt, die Abfrage wird aber übersprungen
    Dim command As String = "SELECT Counter, TestInterval FROM Cards WHERE Index=" & WordNumber & ";"
    DBConnection.ExecuteReader(command)
    DBConnection.DBCursor.Read()
    Dim counter As Integer = DBConnection.SecureGetInt32(0)
    DBConnection.DBCursor.Close()

    If counter = 1 Then Throw New xlsExceptionCards(1) ' Counter kann nicht verringert werden
    If counter <= 0 Then Throw New xlsExceptionCards(0) ' Darf nicht vorkommen, evtl. Datenbank inkonsistent!

    ' Counter war größer als 1. Um 1 verringert speichern.
    command = "UPDATE Cards SET Cards.Counter=" & counter - 1 & " WHERE Cards.Index=" & WordNumber & ";"
    DBConnection.ExecuteNonQuery(command)
  End Sub
End Class