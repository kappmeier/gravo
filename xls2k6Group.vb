Imports System.Data.OleDb

Public Class xlsGroup
  Inherits xlsBase

  Dim m_sGroupTable As String

  Sub New(ByVal GroupTable As String)
    MyBase.new()
    m_sGroupTable = GroupTable
  End Sub

  Public Function GetWords() As Collection
    ' TODO Exception, falls GroupTable nicht existiert
    Dim words As New Collection
    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT DISTINCT D.Word, G.Index FROM DictionaryWords AS D, " & AddHighColons(m_sGroupTable) & " AS G WHERE D.Index=G.WordIndex ORDER BY G.Index;"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    Do While DBCursor.Read
      Dim sAdd As String = SecureGetString(DBCursor, 0)
      If words.Contains(sAdd) Then
      Else
        words.Add(sAdd, sAdd)
      End If
    Loop
    DBCursor.Close()
    Return words
  End Function

  Sub Add(ByVal WordIndex As Integer)
    ' TODO Exception, falls GroupTable nicht existiert
    ' TODO, falls WordIndex nicht existiert... oder bei der abfrage mal sehen
    Dim sCommand As String = "INSERT INTO " & m_sGroupTable & " VALUES(" & GetMaxIndex(m_sGroupTable) + 1 & ", " & WordIndex & ");"
    DBConnection.ExecuteReader(sCommand)
  End Sub

  Public Property GroupTable() As String
    Get
      Return m_sGroupTable
    End Get
    Set(ByVal value As String)
      m_sGroupTable = value
    End Set
  End Property

  Public Function GetWords(ByVal word As String) As Collection
    Dim cDictionaryEntry As New Collection
    ' Hohlt alle wörter, bei denen word = word gilt, die auch in der gruppe sind, als komplette dictionaryentrys
    Dim sCommand As String = "Select Case D.Index FROM DictionaryWords AS D, GroupEspresso11 AS G WHERE (((D.Index)=G.WordIndex) AND ((D.Word)='" & AddHighColons(word) & "'));"
    Return cDictionaryEntry
  End Function

End Class
