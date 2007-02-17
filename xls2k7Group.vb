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
    ' dieser befehl holt alle wörter aus DictionaryMain die in einer gruppe benutzt werden
    'Dim sCommand As String = "SELECT DISTINCT M.WordEntry, D.Word, G.Index FROM DictionaryMain AS M, DictionaryWords AS D, " & AddHighColons(m_sGroupTable) & " AS G WHERE D.Index=G.WordIndex AND M.Index=D.MainIndex ORDER BY G.Index;"
    ' dieser befehl holt alle wörter aus DictionaryWords die in einer gruppe benutzt werden
    ' das G.Index ist nötig damit nach g.Index sortiert werden kann
    Dim sCommand As String = "SELECT D.Word, G.Index FROM DictionaryWords AS D, " & AddHighColons(m_sGroupTable) & " AS G WHERE D.Index=G.WordIndex ORDER BY G.Index;"
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

    ' nur hinzufügen, wenn noch nicht vorhanden
    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT WordIndex FROM " & AddHighColons(m_sGroupTable) & " WHERE WordIndex=" & WordIndex & ";"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    If DBCursor.HasRows Then DBCursor.Close() : Exit Sub Else DBCursor.Close() ' schon ein eintrag vorhanden!

    ' einfügen
    sCommand = "INSERT INTO " & m_sGroupTable & " VALUES(" & GetMaxIndex(m_sGroupTable) + 1 & ", " & WordIndex & ");"
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

  ' Hohlt alle wörter, bei denen word = word gilt, die auch in der gruppe sind, als komplette dictionaryentrys
  Public Function GetWords(ByVal word As String) As Collection
    Dim cDictionaryEntrys As New Collection
    Dim DBCursor As OleDbDataReader

    Dim sCommand As String = "Select D.Index FROM DictionaryWords AS D, " & AddHighColons(m_sGroupTable) & " AS G WHERE (((D.Index)=G.WordIndex) AND ((D.Word)='" & AddHighColons(word) & "'));"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    If DBCursor.HasRows = False Then Return cDictionaryEntrys ' kein wort entspricht den geforderten angaben
    Dim cIndices As New Collection
    Do While DBCursor.Read
      cIndices.add(Me.SecureGetInt32(DBCursor, 0))
    Loop
    DBCursor.Close()
    Dim wCurrent As xlsDictionaryEntry
    For Each iIndex As Integer In cIndices
      wCurrent = New xlsDictionaryEntry(DBConnection, iIndex)
      cDictionaryEntrys.Add(wCurrent)
    Next
    Return cDictionaryEntrys


    'Dim iMainIndex As Int32 = GetEntryIndex(Language, MainEntry)
    'Dim DBCursor As OleDbDataReader
    'Dim sCommand As String = "SELECT Index FROM DictionaryWords WHERE Word='" & AddHighColons(SubEntry) & "' AND MainIndex=" & iMainIndex & ";"
    'DBCursor = DBConnection.ExecuteReader(sCommand)
    'If DBCursor.HasRows = False Then Return cWords ' Nichts zurückgeben, wenn kein Wort mit der angegebenen Beschreibung existiert
    'Dim wCurrent As xlsDictionaryEntry
    'Do While DBCursor.Read
    '  cIndices.Add(Me.SecureGetInt32(DBCursor, 0))
    'Loop
    'For Each iIndex As Integer In cIndices
    '  wCurrent = New xlsDictionaryEntry(DBConnection, iIndex)
    '  cWords.Add(wCurrent)
    'Next
    'Return cWords




  End Function

End Class
