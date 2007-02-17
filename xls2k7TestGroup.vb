Imports System.Data.OleDb

Public Class xlsTestGroup
  Inherits xlsTestBase
  ' Abfragen von Vokabeln
  ' unterstützt (nur) Gruppen

  Overrides Sub Start(ByVal Group As String)
    ' Finde alle Wörter, die zu dieser Sprache passen heraus
    Dim cWords As Collection = New Collection
    Dim sCommand As String = "SELECT W.Index FROM DictionaryWords AS W, " & AddHighColons(Group) & " AS G WHERE W.Index = G.WordIndex;"
    Dim DBCursor As OleDbDataReader = DBConnection.ExecuteReader(sCommand)
    Do While DBCursor.Read
      cWords.Add(SecureGetInt32(DBCursor, 0))
    Loop
    DBCursor.Close()
    Start(cWords)
  End Sub
End Class
