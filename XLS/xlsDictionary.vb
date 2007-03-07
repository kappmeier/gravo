Imports System.Collections.ObjectModel
Imports Gravo2k7.AccessDatabaseOperation

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

  ' Standardkonstruktor
  Sub New()
    MyBase.New()
  End Sub

  Sub New(ByVal db As AccessDatabaseOperation)    ' Keinen Speziellen Table auswählen
    MyBase.New(db)
  End Sub

  ' Alle Sprachen die es im Dictionary gibt, anzeigen
  Public Function DictionaryEntrys(ByVal Language As String, ByVal MainLanguage As String) As Collection(Of String)
    Dim words As New Collection(Of String)
    Dim command As String = "SELECT WordEntry FROM DictionaryMain WHERE LanguageName='" & AddHighColons(Language) & "' AND MainLanguage='" & AddHighColons(MainLanguage) & "' ORDER BY WordEntry;"
    DBConnection.ExecuteReader(command)
    Do While DBConnection.DBCursor.Read()
      words.Add(DBConnection.SecureGetString(0))
    Loop
    DBConnection.DBCursor.Close()
    Return words
  End Function

  Public Function DictionaryLanguages() As Collection(Of String)
    Dim languages As New Collection(Of String)
    Dim command As String = "SELECT DISTINCT LanguageName FROM DictionaryMain ORDER BY LanguageName;"
    DBConnection.ExecuteReader(command)
    Do While DBConnection.DBCursor.Read()
      languages.Add(DBConnection.SecureGetString(0))
    Loop
    DBConnection.DBCursor.Close()
    Return languages
  End Function

  Public Function DictionaryMainLanguages() As Collection(Of String)
    Dim mainLanguages As New Collection(Of String)
    Dim command As String = "SELECT DISTINCT MainLanguage FROM DictionaryMain ORDER BY MainLanguage;"
    DBConnection.ExecuteReader(command)
    Do While DBConnection.DBCursor.Read()
      mainLanguages.Add(DBConnection.SecureGetString(0))
    Loop
    DBConnection.DBCursor.Close()
    Return mainLanguages
  End Function

  Public Function DictionarySubEntrys(ByVal Word As String, ByVal Language As String, ByVal MainLanguage As String) As Collection(Of String)
    Dim subWords As New Collection(Of String)
    Dim command As String = "SELECT Index FROM DictionaryMain WHERE WordEntry='" & AddHighColons(Word) & "' AND LanguageName='" & AddHighColons(Language) & "' AND MainLanguage='" & AddHighColons(MainLanguage) & "';"
    DBConnection.ExecuteReader(command)
    Dim e As New Exception("Eintrag " & Word & " nicht in DictionaryMain.")
    If DBConnection.DBCursor.HasRows = False Then Throw e
    DBConnection.DBCursor.Read()
    Dim i As Integer = DBConnection.SecureGetInt32(0)
    DBConnection.DBCursor.Close()
    command = "SELECT DISTINCT Word FROM DictionaryWords WHERE MainIndex = " & i & " AND NOT Word='" & AddHighColons(Word) & "';"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Return subWords ' Keine Einträge unter diesem Namen!
    Do While DBConnection.DBCursor.Read
      subWords.Add(DBConnection.SecureGetString(0))
    Loop
    DBConnection.DBCursor.Close()
    Return subWords
  End Function

  Function GetWords(ByVal MainEntry As String, ByVal SubEntry As String, ByVal Language As String, ByVal MainLanguage As String) As Collection(Of xlsDictionaryEntry)
    Dim words As New Collection(Of xlsDictionaryEntry)
    Dim indices As New Collection(Of Integer)

    Dim mainIndex As Int32 = GetEntryIndex(MainEntry, Language, MainLanguage)
    Dim command As String = "SELECT Index FROM DictionaryWords WHERE Word='" & AddHighColons(SubEntry) & "' AND MainIndex=" & mainIndex & ";"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Return words ' Nichts zurückgeben, wenn kein Wort mit der angegebenen Beschreibung existiert
    Dim currentEntry As xlsDictionaryEntry
    Do While DBConnection.DBCursor.Read()
      indices.Add(DBConnection.SecureGetInt32(0))
    Loop
    For Each index As Integer In indices
      currentEntry = New xlsDictionaryEntry(DBConnection, index)
      words.Add(currentEntry)
    Next
    Return words
  End Function

  Function GetWordsAndSubWords(ByVal MainEntry As String, ByVal Language As String, ByVal MainLanguage As String) As Collection(Of xlsDictionaryEntry)
    Dim words As Collection(Of xlsDictionaryEntry)
    words = GetWords(MainEntry, MainEntry, Language, MainLanguage)
    AddSubWordsToCollection(MainEntry, Language, MainLanguage, words)
    Return words
  End Function

  Private Sub AddSubWordsToCollection(ByVal MainEntry As String, ByVal Language As String, ByVal MainLanguage As String, ByRef gCol As Collection(Of xlsDictionaryEntry))
    Dim indices As New Collection(Of Integer)

    Dim mainIndex As Int32 = GetEntryIndex(MainEntry, Language, MainLanguage)
    Dim command As String = "SELECT Index FROM DictionaryWords WHERE (NOT Word='" & AddHighColons(MainEntry) & "') AND MainIndex=" & mainIndex & ";"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Exit Sub ' alte Collection zurückgeben, da kein entsprechendes Wort gefunden wurde
    Dim currentEntry As xlsDictionaryEntry
    Do While DBConnection.DBCursor.Read()
      indices.Add(DBConnection.SecureGetInt32(0))
    Loop
    For Each index As Integer In indices
      currentEntry = New xlsDictionaryEntry(DBConnection, index)
      gCol.Add(currentEntry)
    Next
  End Sub

  Function GetSubWords(ByVal MainEntry As String, ByVal Language As String, ByVal MainLanguage As String) As Collection(Of xlsDictionaryEntry)
    Dim words As New Collection(Of xlsDictionaryEntry)
    AddSubWordsToCollection(MainEntry, Language, MainLanguage, words) ' hier wird words aktualisiert
    Return words
  End Function

  Function GetEntryIndex(ByVal MainEntry As String, ByVal Language As String, ByVal MainLanguage As String) As Integer
    ' Check, ob die Sprache vorhanden ist
    Dim command As String = "SELECT DISTINCT LanguageName FROM DictionaryMain WHERE LanguageName='" & AddHighColons(Language) & "';"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Throw New xlsExceptionLanguageNotFound("Sprache " & Language & " nicht vorhanden.")

    ' Check, ob die Main-Sprache vorhanden ist
    command = "SELECT DISTINCT LanguageName FROM DictionaryMain WHERE MainLanguage='" & AddHighColons(MainLanguage) & "';"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Throw New xlsExceptionLanguageNotFound("Hauptsprache " & MainLanguage & " nicht vorhanden.")

    ' Index herausfinden
    command = "SELECT Index FROM DictionaryMain WHERE WordEntry='" & AddHighColons(MainEntry) & "' AND LanguageName = '" & AddHighColons(Language) & "' AND MainLanguage='" & AddHighColons(MainLanguage) & "';"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Throw New xlsExceptionEntryNotFound("Kein Haupteintrag " & MainEntry & " zur gewählten Sprache vorhanden.")
    DBConnection.DBCursor.Read()
    Dim ret As Int32 = DBConnection.SecureGetInt32(0)
    DBConnection.DBCursor.Close()
    Return ret
  End Function

  Function GetSubEntryIndices(ByVal MainIndex As Integer, ByVal Name As String) As Collection(Of Integer)
    Dim indices As Collection(Of Integer) = New Collection(Of Integer)
    Dim command As String = "SELECT Index FROM DictionaryWords WHERE Word='" & Name & "' AND MainIndex=" & MainIndex & ";"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Return indices
    Do While DBConnection.DBCursor.Read()
      indices.Add(DBConnection.SecureGetInt32(0))
    Loop
    Return indices
  End Function

  Sub ChangeEntry(ByVal Index As Integer, ByVal NewEntry As String)
    ' Die Sprache und das xlseintrag soll gleich bleiben
    Dim command As String = "SELECT LanguageName, MainLanguage FROM DictionaryMain WHERE Index=" & Index & ";" ' 
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Throw New Exception("Kein Eintrag unter Index " & Index & "vorhanden.")
    DBConnection.DBCursor.Read()
    Dim language As String = DBConnection.SecureGetString(0)
    Dim mainLanguage As String = DBConnection.SecureGetString(1)
    ' Zunächst testen, ob der Eintrag gegen die Vorschriften verstößt
    command = "SELECT Index FROM DictionaryMain WHERE WordEntry='" & AddHighColons(NewEntry) & "' AND LanguageName='" & AddHighColons(language) & "' AND MainLanguage='" & AddHighColons(mainLanguage) & "';"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows Then
      DBConnection.DBCursor.Read()
      Dim e As Exception = New Exception("Es gibt bereits einen Eintrag """ & NewEntry & """ mit Index " & DBConnection.SecureGetint32(0) & ".")
      DBConnection.DBCursor.Close()
      Throw e
    End If
    ' Alles OK, umbenennen möglich
    command = "UPDATE DictionaryMain SET WordEntry='" & AddHighColons(NewEntry) & "' WHERE Index=" & Index & ";"
    DBConnection.ExecuteNonQuery(command)
  End Sub

  Public Sub ChangeSubEntries(ByVal Indices As Collection(Of Integer), ByVal NewSubEntry As String)
    Dim command As String
    For Each index As Integer In Indices
      command = "UPDATE DictionaryWords SET Word='" & AddHighColons(NewSubEntry) & "' WHERE Index=" & index & ";"
      DBConnection.ExecuteNonQuery(command)
    Next
  End Sub

  Public Sub AddEntry(ByVal Word As String, ByVal Language As String, ByVal MainLanguage As String)
    If Word = "" Then Throw New xlsExceptionInput(1)
    If Language = "" Then Throw New xlsExceptionInput(3)
    Try
      GetEntryIndex(Word, Language, MainLanguage)
    Catch ex As xlsExceptionEntryNotFound
      ' Eintrag nicht gefunden, kann also hinzugefügt werden
      Dim command As String = "INSERT INTO DictionaryMain (WordEntry, LanguageName, MainLanguage) VALUES('" & AddHighColons(Word) & "', '" & AddHighColons(Language) & "', '" & AddHighColons(mainlanguage) & "');"
      DBConnection.ExecuteNonQuery(command)
      Exit Sub
    Catch ex As xlsExceptionLanguageNotFound
      ' Sprache nicht vorhanden! kann also auf jeden fall eingefügt werden
      Dim command As String = "INSERT INTO DictionaryMain (WordEntry, LanguageName, MainLanguage) VALUES('" & AddHighColons(Word) & "', '" & AddHighColons(Language) & "', '" & AddHighColons(MainLanguage) & "');"
      DBConnection.ExecuteNonQuery(command)
      Exit Sub
    Catch ex As Exception
      ' Etwas anderes ist schiefgegangen. Weiterleiten
      Throw ex
    End Try
    ' Nichts schiefgegangen, das heißt es gibt ein Wort mit diesem Index
    Throw New xlsExceptionEntryExists("Es existiert bereits ein Wort unter diesem Eintrag.")
  End Sub

  Public Sub AddSubEntry(ByRef Word As xlsDictionaryEntry, ByVal MainEntry As String, ByVal Language As String, ByVal MainLanguage As String)
    Dim mainIndex As Integer
    Try
      mainIndex = GetEntryIndex(MainEntry, Language, MainLanguage)
    Catch ex As xlsExceptionLanguageNotFound
      ' Unter der Sprache ist nix vorhanden, also kann es den eintrag auch nicht geben
      Throw New xlsExceptionEntryNotFound
    Catch ex As xlsExceptionEntryNotFound
      'ex wurde nichts gefunden, weiterleiten und vom aufrufer behandeln lassen
      Throw ex
    End Try

    ' Testen, ob es schon ein Wort gibt, das so ist
    Dim command As String = "SELECT Index FROM DictionaryWords WHERE MainIndex=" & mainIndex & " AND Word='" & AddHighColons(Word.Word) & "' AND Meaning='" & AddHighColons(Word.Meaning) & "';"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows Then Throw New xlsExceptionEntryExists("Der gewählte Subentry existiert schon unter dem MainIndex und mit der Bedeutung")


    ' Wort einfügen
    command = "INSERT INTO DictionaryWords (MainIndex, Word, Pre, Post, WordType, Meaning, TargetLanguageInfo) VALUES(" & mainIndex & ", '" & AddHighColons(Word.Word) & "', '" & AddHighColons(Word.Pre) & "', '" & AddHighColons(Word.Post) & "', " & Word.WordType & ", '" & AddHighColons(Word.Meaning) & "', '" & AddHighColons(Word.AdditionalTargetLangInfo) & "');"
    DBConnection.ExecuteNonQuery(command)

    ' Card-Status hinzufügen
    Dim card As New xlsCards
    card.DBConnection = DBConnection
    card.AddNewEntry(GetSubEntryIndex(mainIndex, Word.Word, Word.Meaning))
    'End If
  End Sub

  Public Function GetMaxEntryIndex() As Integer
    Return GetMaxIndex("DictionaryMain")
  End Function

  Public Function GetMaxSubEntryIndex() As Integer
    Return GetMaxIndex("DictionaryWords")
  End Function

  Public Function GetWords(ByVal Language As String, ByVal MainLanguage As String) As Collection(Of xlsWordAndMainIndex)
    ' Gibt alle Wörter aus DictionaryWords zurück, welche die angegebene Sprache und XLS erfüllen
    ' Die collection ist eine Sammlung von Strings mit zugehörigen MainIndizes vom Typ xlsWordAndMainIndex
    Dim words As New Collection(Of xlsWordAndMainIndex)
    Dim command As String = "SELECT DISTINCT W.Word, W.MainIndex FROM DictionaryWords AS W, DictionaryMain AS M WHERE (W.MainIndex = M.Index) AND (M.LanguageName='" & AddHighColons(Language) & "') AND (M.MainLanguage='" & AddHighColons(MainLanguage) & "')ORDER BY W.Word;"
    DBConnection.ExecuteReader(command)
    Do While DBConnection.DBCursor.Read
      Dim word As xlsWordAndMainIndex
      word.Word = DBConnection.SecureGetString(0)
      word.MainIndex = DBConnection.SecureGetInt32(1)
      words.Add(word)
    Loop
    Return words
  End Function

  Public Function GetEntry(ByVal Index As Integer) As String
    Dim command As String = "SELECT WordEntry FROM DictionaryMain WHERE Index=" & Index & ";"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Throw New xlsExceptionEntryNotFound("Der Eintrag existiert nicht.")
    DBConnection.DBCursor.Read()
    Dim ret As String = DBConnection.SecureGetString(0)
    DBConnection.DBCursor.Close()
    Return ret
  End Function

  Public Function GetEntryLanguage(ByVal Index As Integer) As String
    Dim command As String = "SELECT LanguageName FROM DictionaryMain WHERE Index=" & Index & ";"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Throw New xlsExceptionEntryNotFound("Der Eintrag existiert nicht.")
    DBConnection.DBCursor.Read()
    Dim ret As String = DBConnection.SecureGetString(0)
    DBConnection.DBCursor.Close()
    Return ret
  End Function

  Public Function GetEntryMainLanguage(ByVal Index As Integer) As String
    Dim command As String = "SELECT MainLanguage FROM DictionaryMain WHERE Index=" & Index & ";"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Throw New xlsExceptionEntryNotFound("Der Eintrag existiert nicht.")
    DBConnection.DBCursor.Read()
    Dim ret As String = DBConnection.SecureGetString(0)
    DBConnection.DBCursor.Close()
    Return ret
  End Function

  Public Function GetSubEntryIndex(ByVal MainIndex As Integer, ByVal Word As String, ByVal Meaning As String) As Integer
    Dim command As String = "SELECT Index FROM DictionaryWords WHERE Word='" & AddHighColons(Word) & "' AND Meaning='" & AddHighColons(Meaning) & "' AND MainIndex=" & MainIndex & ";"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Throw New xlsExceptionEntryNotFound("Der Eintrag existiert nicht.")
    DBConnection.DBCursor.Read()
    Dim ret As String = DBConnection.SecureGetInt32(0)
    DBConnection.DBCursor.Close()
    Return ret
  End Function

  Public Function FindSimilar(ByVal WordBeginning As String, ByVal Language As String, ByVal MainLanguage As String) As String
    Dim command As String = "SELECT M.[WordEntry] FROM DictionaryMain AS M WHERE M.[WordEntry] LIKE '" & AddHighColons(WordBeginning) & "%' AND M.[LanguageName]='" & AddHighColons(Language) & "' AND M.[MainLanguage]='" & AddHighColons(MainLanguage) & "' ORDER BY M.[WordEntry];"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Return ""
    DBConnection.DBCursor.Read()
    Dim word As String = DBConnection.SecureGetString(0)
    DBConnection.DBCursor.Close()
    Return word
  End Function

  Public Function WordCount(ByVal Language As String, ByVal MainLanguage As String) As String
    Dim command As String = "SELECT COUNT([Index]) FROM DictionaryMain WHERE [LanguageName]='" & AddHighColons(Language) & "' AND [MainLanguage]='" & AddHighColons(MainLanguage) & "';"
    DBConnection.ExecuteReader(command)
    DBConnection.DBCursor.Read()
    Dim count As Integer = DBConnection.SecureGetInt32(0)
    DBConnection.DBCursor.Close()
    Return count
  End Function
End Class
