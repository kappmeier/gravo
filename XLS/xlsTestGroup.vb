Imports System.Collections.ObjectModel
Imports Gravo2k7.AccessDatabaseOperation

Public Class xlsTestGroup
  Inherits xlsTestBase
  ' Abfragen von Vokabeln
  ' unterstützt (nur) Gruppen

  ' Finde alle Wörter, die zu dieser Sprache passen heraus
  Overrides Sub Start(ByVal Group As String)
    If IsConnected() = False Then Throw New Exception("Database not connected.")
    Dim words As Collection(Of Integer) = New Collection(Of Integer)
    Dim command As String = "SELECT W.Index FROM DictionaryWords AS W, " & AddHighColons(Group) & " AS G WHERE W.Index = G.WordIndex;"
    DBConnection.ExecuteReader(command)
    Do While DBConnection.DBCursor.Read
      words.Add(DBConnection.SecureGetInt32(0))
    Loop
    DBConnection.DBCursor.Close()
    Start(words)
  End Sub
End Class
