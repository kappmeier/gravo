Imports System.Data.OleDb

Public Structure xlsWordAndMainIndex
  Dim Word As String
  Dim MainIndex As Integer
End Structure

Public Class xlsDictionary
  Inherits xlsBase
  ' Grundlegende Informationen über die Datenbank und die vorhandenen Vokabeln:
  ' Version
  ' Änderungen
  ' Vokabelanzahl
  ' Vokabelinfo
  ' Benutzer
  ' Gruppen

  ' Alle Sprachen die es im Dictionary gibt, anzeigen
  Public Function DictionaryEntrys(ByVal Language As String) As Collection
    Dim cWords As New Collection
    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT WordEntry FROM DictionaryMain WHERE LanguageName='" & AddHighColons(Language) & "' ORDER BY WordEntry;"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    Do While DBCursor.Read
      If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then cWords.Add(DBCursor.GetString(0))
    Loop
    DBCursor.Close()
    Return cWords
  End Function

  Public Function DictionaryLanguages() As Collection
    Dim cLanguages As New Collection
    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT DISTINCT LanguageName FROM DictionaryMain ORDER BY LanguageName;"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    Do While DBCursor.Read
      If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then cLanguages.Add(DBCursor.GetString(0))
    Loop
    DBCursor.Close()
    Return cLanguages
  End Function

  Public Function DictionarySubEntrys(ByVal Word As String) As Collection
    Dim cSubWords As New Collection
    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT Index FROM DictionaryMain WHERE WordEntry='" & AddHighColons(Word) & "';"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    Dim e As New Exception("Eintrag " & Word & " nicht in DictionaryMain.")
    If DBCursor.HasRows = False Then Throw e
    DBCursor.Read()
    Dim i As Integer = DBCursor.GetInt32(0)
    DBCursor.Close()
    sCommand = "SELECT DISTINCT Word FROM DictionaryWords WHERE MainIndex = " & i & " AND NOT Word='" & AddHighColons(Word) & "';"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    If DBCursor.HasRows = False Then Return cSubWords ' Keine Einträge unter diesem Namen!
    Do While DBCursor.Read
      cSubWords.Add(DBCursor.GetString(0))
    Loop
    DBCursor.Close()
    Return cSubWords
  End Function

  Function GetWords(ByVal Language As String, ByVal MainEntry As String, ByVal SubEntry As String) As Collection
    Dim cWords As New Collection
    Dim cIndices As New Collection

    Dim iMainIndex As Int32 = GetEntryIndex(Language, MainEntry)
    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT Index FROM DictionaryWords WHERE Word='" & AddHighColons(SubEntry) & "' AND MainIndex=" & iMainIndex & ";"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    If DBCursor.HasRows = False Then Return cWords ' Nichts zurückgeben, wenn kein Wort mit der angegebenen Beschreibung existiert
    Dim wCurrent As xlsDictionaryEntry
    Do While DBCursor.Read
      cIndices.Add(Me.SecureGetInt32(DBCursor, 0))
    Loop
    For Each iIndex As Integer In cIndices
      wCurrent = New xlsDictionaryEntry(DBConnection, iIndex)
      cWords.Add(wCurrent)
    Next
    Return cWords
  End Function

  Function GetWordsAndSubWords(ByVal Language As String, ByVal MainEntry As String) As Collection
    Dim cWords As Collection
    cWords = GetWords(Language, MainEntry, MainEntry)
    Me.GetSubWords(Language, MainEntry, cWords)
    Return cWords
  End Function

  Private Sub GetSubWords(ByVal Language As String, ByVal MainEntry As String, ByRef gCol As Collection)
    Dim cIndices As New Collection

    Dim iMainIndex As Int32 = GetEntryIndex(Language, MainEntry)
    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT Index FROM DictionaryWords WHERE (NOT Word='" & AddHighColons(MainEntry) & "') AND MainIndex=" & iMainIndex & ";"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    If DBCursor.HasRows = False Then Exit Sub ' alte Collection zurückgeben, da kein entsprechendes Wort gefunden wurde
    Dim wCurrent As xlsDictionaryEntry
    Do While DBCursor.Read
      cIndices.Add(DBCursor.GetInt32(0))
    Loop
    For Each iIndex As Integer In cIndices
      wCurrent = New xlsDictionaryEntry(DBConnection, iIndex)
      gCol.Add(wCurrent)
    Next
    'Return gCol
  End Sub

  Function GetSubWords(ByVal Language As String, ByVal MainEntry As String) As Collection
    Dim cWords As New Collection
    Me.GetSubWords(Language, MainEntry, cWords) ' hier wird cwords aktualisiert
    Return cWords
  End Function

  Function GetEntryIndex(ByVal Language As String, ByVal MainEntry As String) As Integer
    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT DISTINCT LanguageName FROM DictionaryMain WHERE LanguageName='" & AddHighColons(Language) & "';"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    If DBCursor.HasRows = False Then Throw New xlsExceptionLanguageNotFound("Sprache " & Language & " nicht vorhanden.")
    sCommand = "SELECT Index FROM DictionaryMain WHERE WordEntry='" & AddHighColons(MainEntry) & "' AND LanguageName = '" & AddHighColons(Language) & "';"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    If DBCursor.HasRows = False Then Throw New xlsExceptionEntryNotFound("Kein Haupteintrag " & MainEntry & " zur gewählten Sprache vorhanden.")
    DBCursor.Read()
    Return DBCursor.GetInt32(0)
  End Function

  Function GetSubEntryIndices(ByVal MainIndex As Integer, ByVal Name As String) As Collection
    Dim cIndices As Collection = New Collection
    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT Index FROM DictionaryWords WHERE Word='" & Name & "' AND MainIndex=" & MainIndex & ";"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    If DBCursor.HasRows = False Then Return cIndices
    Do While DBCursor.Read
      cIndices.Add(SecureGetInt32(DBCursor, 0))
    Loop
    Return cIndices
  End Function

  Sub ChangeEntry(ByVal Index As Integer, ByVal NewEntry As String)
    ' Die Sprache und das xlseintrag soll gleich bleiben
    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT LanguageName, XLSType FROM DictionaryMain WHERE Index=" & Index & ";" ' 
    DBCursor = DBConnection.ExecuteReader(sCommand)
    If DBCursor.HasRows = False Then Throw New Exception("Kein Eintrag unter Index " & Index & "vorhanden.")
    DBCursor.Read()
    Dim sLanguage As String = Me.SecureGetString(DBCursor, 0)
    Dim sXLSType As String = Me.SecureGetString(DBCursor, 1)
    ' Zunächst testen, ob der Eintrag gegen die Vorschriften verstößt
    sCommand = "SELECT Index FROM DictionaryMain WHERE WordEntry='" & AddHighColons(NewEntry) & "' AND LanguageName='" & AddHighColons(sLanguage) & "' AND XLSType='" & AddHighColons(sXLSType) & "'"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    If DBCursor.HasRows Then
      DBCursor.Read()
      Dim e As Exception = New Exception("Es gibt bereits einen Eintrag """ & NewEntry & """ mit Index " & SecureGetInt32(DBCursor, 0) & ".")
      DBCursor.Close()
      Throw e
    End If
    ' Alles OK, umbenennen möglich
    sCommand = "UPDATE DictionaryMain SET WordEntry='" & AddHighColons(NewEntry) & "' WHERE Index=" & Index & ";"
    DBConnection.ExecuteNonQuery(sCommand)
  End Sub

  Public Sub ChangeSubEntries(ByVal Index As Collection, ByVal NewSubEntry As String)
    Dim sCommand As String
    For Each iIndex As Integer In Index
      sCommand = "UPDATE DictionaryWords SET Word='" & AddHighColons(NewSubEntry) & "' WHERE Index=" & iIndex & ";"
      DBConnection.ExecuteNonQuery(sCommand)
    Next
  End Sub

  Public Sub AddEntry(ByVal Word As String, ByVal Language As String, ByVal XLSType As String)
    If Word = "" Then Throw New xlsExceptionInput(1)
    If Language = "" Then Throw New xlsExceptionInput(3)
    If XLSType = "" Then Throw New xlsExceptionInput(4)
    Dim iIndex As String
    Try
      iIndex = Me.GetEntryIndex(Language, Word)
    Catch ex As xlsExceptionEntryNotFound
      ' Eintrag nicht gefunden, kann also hinzugefügt werden
      Dim iNewIndex As Integer = GetMaxEntryIndex() + 1
      Dim sCommand As String = "INSERT INTO DictionaryMain VALUES(" & iNewIndex & ", '" & AddHighColons(Word) & "', '" & AddHighColons(Language) & "', '" & AddHighColons(XLSType) & "');"
      DBConnection.ExecuteNonQuery(sCommand)
      Exit Sub
    Catch ex As xlsExceptionLanguageNotFound
      ' Sprache nicht vorhanden! Exception weiterleiten
      Throw ex
    Catch ex As Exception
      ' Etwas anderes ist schiefgegangen. Weiterleiten
      Throw ex
    End Try
    ' Nichts schiefgegangen, das heißt es gibt ein Wort mit diesem Index
    Throw New xlsExceptionEntryExists("Es existiert bereits ein Wort unter diesem Eintrag.")
  End Sub

  Public Sub AddSubEntry(ByRef Word As xlsDictionaryEntry, ByVal MainEntry As String, ByVal Language As String, ByVal XLSType As String)
    ' Testen, ob schon ein Wort unter diesem Index vorhanden ist
    Dim sCommand As String = "SELECT Index FROM DictionaryWords WHERE Index=" & Word.WordIndex & ";"
    Dim DBCursor As OleDbDataReader
    DBCursor = DBConnection.ExecuteReader(sCommand)
    If DBCursor.HasRows = True Then
      'Fehler
    Else
      ' Keine Einträge, also gibt es unter dem Index nichts
      ' Zunächst testen, ob es den Hauptindex überhaupt gibt
      Dim iindex As Integer
      Try
        iindex = Me.GetEntryIndex(Language, MainEntry)
      Catch ex As xlsExceptionEntryNotFound
        'ex wurde nichts gefunden, weiterleiten und vom aufrufer behandeln lassen
        Throw ex
      End Try
      ' Kein Fehler aufgetreten, also existiert unter Index iIndex das Wort zum Haupteintrag
      ' Automatisch index als autowert belegen lassen
      sCommand = "INSERT INTO DictionaryWords (MainIndex, Word, Pre, Post, WordType, Meaning, TargetLanguageInfo, Deleted) VALUES(" & iindex & ", '" & AddHighColons(Word.Word) & "', '" & AddHighColons(Word.Pre) & "', '" & AddHighColons(Word.Post) & "', " & Word.WordType & ", '" & AddHighColons(Word.Meaning) & "', '" & AddHighColons(Word.AdditionalTargetLangInfo) & "', " & False & ");"
      'sCommand = "INSERT INTO DictionaryWords (MainIndex, Word, Pre, Post, WordType, Meaning, TargetLanguageInfo, Deleted) VALUES(" & Word.WordIndex & ", " & iindex & ", '" & AddHighColons(Word.Word) & "', '" & AddHighColons(Word.Pre) & "', '" & AddHighColons(Word.Post) & "', " & Word.WordType & ", '" & AddHighColons(Word.Meaning) & "', '" & AddHighColons(Word.AdditionalTargetLangInfo) & "', " & False & ");"
      DBConnection.ExecuteNonQuery(sCommand)
      ' Card-Status hinzufügen
      Dim card As New xlsCards
      card.DBConnection = Me.DBConnection
      card.AddNewEntry()
    End If
  End Sub

  Public Function GetMaxEntryIndex() As Integer
    Return GetMaxIndex("DictionaryMain")
    'Dim DBCursor As OleDbDataReader
    'Dim sCommand As String = "SELECT MAX(Index) FROM DictionaryMain;"
    'DBCursor = DBConnection.ExecuteReader(sCommand)
    'DBCursor.Read()
    'Return Me.SecureGetInt32(DBCursor, 0)
  End Function

  Public Function GetMaxSubEntryIndex() As Integer
    Return GetMaxIndex("DictionaryWords")
    'Dim sCommand As String = "SELECT MAX(Index) FROM DictionaryWords;"
    'Dim DBCursor As OleDbDataReader = DBConnection.ExecuteReader(sCommand)
    'DBCursor.Read()
    'Return Me.SecureGetInt32(DBCursor, 0)
  End Function

  Public Function GetWords(ByVal Language As String, ByVal XLSType As String) As Collection
    ' Gibt alle Wörter aus DictionaryWords zurück, welche die angegebene Sprache und XLS erfüllen
    ' Die collection ist eine Sammlung von Strings mit zugehörigen MainIndizes vom Typ xlsWordAndMainIndex
    Dim cWords As New Collection
    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT DISTINCT W.Word, W.MainIndex FROM DictionaryWords AS W, DictionaryMain AS M WHERE (W.MainIndex = M.Index) AND (M.LanguageName='" & AddHighColons(Language) & "') AND (M.XLSType='" & AddHighColons(XLSType) & "') ORDER BY W.Word;"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    Do While DBCursor.Read
      Dim word As xlsWordAndMainIndex
      word.Word = SecureGetString(DBCursor, 0)
      word.MainIndex = SecureGetInt32(DBCursor, 1)
      cWords.Add(word)
    Loop
    Return cWords
  End Function

  Public Function GetEntry(ByVal Index As Integer) As String
    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT WordEntry FROM DictionaryMain WHERE Index=" & Index & ";"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    If DBCursor.HasRows = False Then Throw New xlsExceptionEntryNotFound("Der Eintrag existiert nicht.")
    DBCursor.Read()
    Dim sOutput As String = SecureGetString(DBCursor, 0)
    DBCursor.Close()
    Return sOutput
  End Function
End Class
