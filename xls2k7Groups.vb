Imports System.Data.OleDb

Public Class xlsGroups
  Inherits xlsBase

  Public Function GetGroups() As Collection
    ' Alle Gruppen ausgeben
    Dim cGroups As New Collection
    Dim cIndices As New Collection
    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT Index FROM Groups;"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    Do While DBCursor.Read
      cIndices.Add(SecureGetInt32(DBCursor, 0))
    Loop
    DBCursor.Close()
    For Each Index As Integer In cIndices
      Dim entry As New xlsGroupEntry(DBConnection)
      entry.LoadGroup(Index)
      cGroups.Add(entry)
    Next
    Return cGroups
  End Function
End Class