Imports Gravo.AccessDatabaseOperation

' Diese Klasse enthält alle Worteigenschaften, die für das Anzeigen des Wortverzeichnisses benötigt werden.
' Informationen über irreguläre Formen oder Gruppen/Unit/Abfrage sind nicht enthalten.
Public Class xlsDictionaryEntry
  Inherits xlsBase

  Private m_index As Integer = -1                 ' Index des Wortes in DictionaryWords
  Private m_iMainIndex As Integer                 ' Index in DictionaryMain

  ' Vokabelinformationen
  Private m_word As String                        ' Vokabel
  Private m_pre As String                         ' Pre-Vokabel    (to, le, ...)
  Private m_post As String                        ' Post-Vokabel
  Private m_meaning As String                     ' Bedeutung als String
  Private m_wordType As Integer                   ' Vokabelart (Nomen, Verb ...) codiert als Nummer
  Private m_additionalTargetLangInfo As String    ' Beschreibung der gesuchten Vokabel (Plural, slang, ...)
  Private m_irregular As Boolean
  'Private m_marked As Boolean                     ' Wort markiert? z.b. rezeptiver Wortschatz etc.

  Sub New(ByVal db as DatabaseOperation, ByVal Index As Integer)
    MyBase.new(db)
    LoadWord(Index)
  End Sub

  Sub New(ByVal db as DatabaseOperation)
    MyBase.New(db)
  End Sub

  Public Sub LoadWord(ByVal Index As Integer)
    m_index = Index
    LoadWord(False)
  End Sub

  Public Sub LoadNewWord(ByVal index As Integer)
    m_index = index
    LoadWord(True)
  End Sub

  Private Sub LoadWord(ByVal newWord As Boolean)
    If IsConnected() = False Then Throw New Exception("Datenbank nicht verbunden.")
        Dim command As String = "SELECT MainIndex, Word, Pre, Post, WordType, Meaning, TargetLanguageInfo, Irregular FROM DictionaryWords WHERE [Index] = " & WordIndex & ";"
        DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then
      If newWord Then
        ' neues wort, eintrag kann also nicht existieren
        m_iMainIndex = 0                                              ' Index des Haupteintrags
        Word = ""                                                     ' Vokabel
        Pre = ""                                                      ' Pre
        Post = ""                                                     ' Post
        Meaning = ""                                                  ' Bedeutung
        WordType = -1                                                 ' Vokabelart
        AdditionalTargetLangInfo = ""                                 ' erweiterte Beschreibung in Zielsprache
        Irregular = False
        '        Marked = False                                                ' markiert?
        Exit Sub
      Else
        ' Kein neues Wort, eintrag muß also eigentlich existieren
        Throw New Exception("Eintrag mit Index " & WordIndex & " nicht vorhanden.")
      End If
    End If
    DBConnection.DBCursor.Read()                                      ' Laden des Eintrages
    m_iMainIndex = DBConnection.SecureGetInt32(0)                     ' Index des Haupteintrags
    Word = DBConnection.SecureGetString(1)                            ' Vokabel
    Pre = DBConnection.SecureGetString(2)                             ' Pre
    Post = DBConnection.SecureGetString(3)                            ' Post
    WordType = DBConnection.SecureGetInt32(4)                         ' Vokabelart
    Meaning = DBConnection.SecureGetString(5)                         ' Bedeutung
    AdditionalTargetLangInfo = DBConnection.SecureGetString(6)        ' erweiterte Beschreibung in Zielsprache
    Irregular = DBConnection.SecureGetBool(7)
    DBConnection.DBCursor.Close()
  End Sub

  Public Sub SaveWord()
    If IsConnected() = False Then Throw New Exception("Datenbank nicht verbunden.")
    ' Prüfen, ob die Änderung überhaupt durchgeführt werden darf

    Dim command As String = "SELECT [Index] FROM [DictionaryWords] WHERE [MainIndex]=" & MainIndex & " AND [Word]=" & GetDBEntry(Word) & " AND [Meaning]=" & GetDBEntry(Meaning) & ";"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows Then
      DBConnection.DBCursor.Read()
      If DBConnection.SecureGetInt32(0) <> WordIndex Then Throw New xlsExceptionEntryExists("Entry '" & Word & "' exists with index " & DBConnection.SecureGetInt32(0) & ".")
    End If
        command = "UPDATE DictionaryWords SET MainIndex=" & MainIndex & ", Word=" & GetDBEntry(Word) & ", Pre=" & GetDBEntry(Pre) & ", Post=" & GetDBEntry(Post) & ", WordType=" & WordType & ", Meaning=" & GetDBEntry(Meaning) & ", TargetLanguageInfo=" & GetDBEntry(AdditionalTargetLangInfo) & ", Irregular=" & GetDBEntry(Irregular) & " WHERE [Index] = " & WordIndex & ";"
        DBConnection.ExecuteNonQuery(command)
  End Sub

  Public Sub FindCorrectWordIndex()
    ' ändert direkt den WordIndex. sorgt dafür, daß er korrigiert wird...
    ' TODO wenn es keinen wordindex gibt, setze auf einen neuen. also wie LoadNewWord
    Dim dic As New xlsDictionary(DBConnection)
    m_index = dic.GetSubEntryIndex(MainIndex, Word, Meaning)
  End Sub

  Public ReadOnly Property WordIndex() As Integer
    ' kann geändert werden
    'Set(ByVal NewIndex As Integer)
    '  Me.LoadWord(NewIndex)
    'End Set
    Get
      Return m_index
    End Get
  End Property

  Property Word() As String
    Get
      Return m_word
    End Get
    Set(ByVal Word As String)
      m_word = Word
    End Set
  End Property

  Property Pre() As String
    Get
      Return m_pre
    End Get
    Set(ByVal Pre As String)
      m_pre = Pre
    End Set
  End Property

  Property Post() As String
    Get
      Return m_post
    End Get
    Set(ByVal Post As String)
      m_post = Post
    End Set
  End Property

  Property Meaning() As String
    Get
      Return m_meaning
    End Get
    Set(ByVal Meaning As String)
      m_meaning = Meaning
    End Set
  End Property

  Property WordType() As Integer
    Get
      Return m_wordType
    End Get
    Set(ByVal Value As Integer)
      m_wordType = Value
    End Set
  End Property

  Property AdditionalTargetLangInfo() As String
    Get
      Return m_additionalTargetLangInfo
    End Get
    Set(ByVal Infotext As String)
      m_additionalTargetLangInfo = Infotext
    End Set
  End Property

  Property Irregular() As Boolean
    Get
      Return m_irregular
    End Get
    Set(ByVal value As Boolean)
      m_irregular = value
    End Set
  End Property

  Property MainIndex() As Integer
    Get
      Return m_iMainIndex
    End Get
    Set(ByVal Value As Integer)
      m_iMainIndex = Value
    End Set
  End Property
End Class