Imports Gravo2k7.AccessDatabaseOperation

' Diese Klasse enthält alle Worteigenschaften, die für das Anzeigen des Wortverzeichnisses benötigt werden.
' Informationen über irreguläre Formen oder Gruppen/Unit/Abfrage sind nicht enthalten.
Public Class xlsDictionaryEntry
  Inherits xlsBase

  Private m_index As Integer = -1                ' Index des Wortes in DictionaryWords
  Private m_iMainIndex As Integer                 ' Index in DictionaryMain

  ' Vokabelinformationen
  Private m_sWord As String                       ' Vokabel
  Private m_sPre As String                        ' Pre-Vokabel    (to, le, ...)
  Private m_sPost As String                       ' Post-Vokabel
  Private m_sMeaning As String                    ' Bedeutung als String
  Private m_iWordType As Integer                  ' Vokabelart (Nomen, Verb ...) codiert als Nummer
  Private m_sAdditionalTargetLangInfo As String   ' Beschreibung der gesuchten Vokabel (Plural, slang, ...)

  Sub New(ByVal db As AccessDatabaseOperation, ByVal Index As Integer)
    MyBase.new(db)
    LoadWord(Index)
  End Sub

  Sub New(ByVal db As AccessDatabaseOperation)
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
    Dim command As String = "SELECT MainIndex, Word, Pre, Post, WordType, Meaning, TargetLanguageInfo FROM DictionaryWords WHERE Index = " & WordIndex & ";"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then
      If newWord Then
        ' neues wort, eintrag kann also nicht existieren
        m_iMainIndex = 0                                                ' Index des Haupteintrags
        m_sWord = ""                                                    ' Vokabel
        m_sPre = ""                                                     ' Pre
        m_sPost = ""                                                    ' Post
        m_sMeaning = ""                                                 ' Bedeutung
        m_iWordType = -1                                                ' Vokabelart
        m_sAdditionalTargetLangInfo = ""                                ' erweiterte Beschreibung in Zielsprache
        Exit Sub
      Else
        ' Kein neues Wort, eintrag muß also eigentlich existieren
        Throw New Exception("Eintrag mit Index " & WordIndex & " nicht vorhanden.")
      End If
    End If
    DBConnection.DBCursor.Read()                                        ' Laden des Eintrages
    m_iMainIndex = DBConnection.SecureGetInt32(0)                       ' Index des Haupteintrags
    m_sWord = DBConnection.SecureGetString(1)                           ' Vokabel
    m_sPre = DBConnection.SecureGetString(2)                            ' Pre
    m_sPost = DBConnection.SecureGetString(3)                           ' Post
    m_iWordType = DBConnection.SecureGetInt32(4)                        ' Vokabelart
    m_sMeaning = DBConnection.SecureGetString(5)                        ' Bedeutung
    m_sAdditionalTargetLangInfo = DBConnection.SecureGetString(6)       ' erweiterte Beschreibung in Zielsprache
    DBConnection.DBCursor.Close()
  End Sub

  Public Property WordIndex() As Integer
    Set(ByVal NewIndex As Integer)
      Me.LoadWord(NewIndex)
    End Set
    Get
      Return m_index
    End Get
  End Property

  Property Word() As String
    Get
      Return m_sWord
    End Get
    Set(ByVal Word As String)
      m_sWord = Word
    End Set
  End Property

  Property Pre() As String
    Get
      Return m_sPre
    End Get
    Set(ByVal Pre As String)
      m_sPre = Pre
    End Set
  End Property

  Property Post() As String
    Get
      Return m_sPost
    End Get
    Set(ByVal Post As String)
      m_sPost = Post
    End Set
  End Property

  Property Meaning() As String
    Get
      Return m_sMeaning
    End Get
    Set(ByVal Meaning As String)
      m_sMeaning = Meaning
    End Set
  End Property

  Property WordType() As Integer
    Get
      Return m_iWordType
    End Get
    Set(ByVal Value As Integer)
      m_iWordType = Value
    End Set
  End Property

  Property AdditionalTargetLangInfo() As String
    Get
      Return m_sAdditionalTargetLangInfo
    End Get
    Set(ByVal Infotext As String)
      m_sAdditionalTargetLangInfo = Infotext
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