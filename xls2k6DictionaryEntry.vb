Imports System.Data.OleDb

Public Class xlsDictionaryEntry
  Inherits xlsBase

  ' Diese Klasse enthält alle Worteigenschaften, die für das Anzeigen des Wortverzeichnisses benötigt werden.
  ' Informationen über irreguläre Formen oder Gruppen/Unit/Abfrage sind nicht enthalten.

  Private m_iIndex As Integer = -1                ' Index des Wortes in DictionaryWords
  Private m_iMainIndex As Integer                 ' Index in DictionaryMain

  ' Vokabelinformationen
  Private m_sWord As String                       ' Vokabel
  Private m_sPre As String                        ' Pre-Vokabel    (to, le, ...)
  Private m_sPost As String                       ' Post-Vokabel
  Private m_sMeaning As String                    ' Bedeutung als String
  Private m_iWordType As Integer                  ' Vokabelart (Nomen, Verb ...) codiert als Nummer
  Private m_sAdditionalTargetLangInfo As String   ' Beschreibung der gesuchten Vokabel (Plural, slang, ...)
  Private m_bDeleted As Boolean                   ' Gibt an, ob das Wort gelöscht worden ist

  Sub New(ByVal db As AccessDatabaseOperation, ByVal Index As Integer)
    MyBase.new(db)
    LoadWord(Index)
  End Sub

  Sub New(ByVal db As AccessDatabaseOperation)
    MyBase.New(db)
  End Sub

  Public Sub LoadWord(ByVal Index As Integer)
    m_iIndex = Index
    LoadWord(False)
  End Sub

  Public Sub LoadNewWord(ByVal index As Integer)
    m_iIndex = index
    LoadWord(True)
  End Sub

  Private Sub LoadWord(ByVal newWord As Boolean)
    If IsConnected() = False Then Throw New Exception("Datenbank nicht verbunden.")

    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT MainIndex, Word, Pre, Post, WordType, Meaning, TargetLanguageInfo, Deleted FROM DictionaryWords WHERE Index = " & WordIndex & ";"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    If DBCursor.HasRows = False Then
      If newWord = True Then
        ' neues wort, eintrag kann also nicht existieren
        m_iMainIndex = 0                                        ' Index des Haupteintrags
        m_sWord = ""                                            ' Vokabel
        m_sPre = ""                                             ' Pre
        m_sPost = ""                                            ' Post
        m_sMeaning = ""                                         ' Bedeutung
        m_iWordType = -1                                        ' Vokabelart
        m_sAdditionalTargetLangInfo = ""                        ' erweiterte Beschreibung in Zielsprache
        m_bDeleted = True                                       ' es wurde gelöscht
        Exit Sub        ' Trotzdem darf nicht gelesen werden, da nichts da ist!
      Else
        ' Kein neues wort, eintrag muß also existieren
        Throw New Exception("Eintrag mit Index " & WordIndex & " nicht vorhanden.")
      End If
    End If
    DBCursor.Read()
    If DBCursor.GetBoolean(7) = True Then                     ' leer initialisieren
      m_iMainIndex = 0                                        ' Index des Haupteintrags
      m_sWord = ""                                            ' Vokabel
      m_sPre = ""                                             ' Pre
      m_sPost = ""                                            ' Post
      m_sMeaning = ""                                         ' Bedeutung
      m_iWordType = -1                                        ' Vokabelart
      m_sAdditionalTargetLangInfo = ""                        ' erweiterte Beschreibung in Zielsprache
      m_bDeleted = True                                       ' es wurde gelöscht
    Else
      m_iMainIndex = SecureGetInt32(DBCursor, 0)                      ' Index des Haupteintrags
      m_sWord = SecureGetString(DBCursor, 1)                          ' Vokabel
      m_sPre = SecureGetString(DBCursor, 2)                           ' Pre
      m_sPost = SecureGetString(DBCursor, 3)                          ' Post
      m_iWordType = SecureGetInt32(DBCursor, 4)                       ' Vokabelart
      m_sMeaning = SecureGetString(DBCursor, 5)                       ' Bedeutung
      m_sAdditionalTargetLangInfo = SecureGetString(DBCursor, 6)      ' erweiterte Beschreibung in Zielsprache
      m_bDeleted = False                                              ' es wurde gelöscht
    End If
  End Sub

  Public Property WordIndex() As Integer
    Set(ByVal NewIndex As Integer)
      Me.LoadWord(NewIndex)
    End Set
    Get
      Return m_iIndex
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