Imports System.Data.OleDb

Public Class xlsTestBase
  Inherits xlsBase
  ' Abfragen von Vokabeln
  ' unterstützt keine Gruppen

  Private ldfManagement As New xlsLDFManagement
  Private cTestWords As Collection

  Private m_bTestMode As Boolean = False
  Private m_iTestWordCountDone As Integer = 0
  Private m_iTestWordCountDoneRight As Integer = 0
  Private m_iTestWordCountDoneFalse As Integer = 0
  Private m_iTestWordCountDoneFalseAllTrys As Integer = 0

  Private m_bWordToMeaning As Boolean

  Private iTestIndex As Integer
  Private TestDictionaryEntry As xlsDictionaryEntry

  Private iTestCurrentWord As Integer = -1

  Private bFirstTest As Boolean = True        ' gibt an, ob _zwischen zwei NextWord()_ aufrufen das wort zum ersten mal geprüft wird,
  ' Falls ein wort in einem Durchlauf öfter abgefragt wird und gleichzeitig das cards-system benutzt wird,
  ' können sich effekte potenzieren oder aufheben!!!
  ' deshalb werden wörter gelöscht, sobald sie richtig eingegeben werden sind. es kann allerdings natürlich von
  ' außen über einen NextWord() aufruf dieser mechanismus außer kraft gesetzt werden.
  Private bUseCards As Boolean = True         ' soll das Karteikarten-System benutzt werden?



  Overridable Sub Start(ByRef TestWords As Collection)  ' Wörter sollen übergeben werden Collection von indizes aus DictionaryWords
    ldfManagement.LDFPath = Application.StartupPath() ' TODO in den konstruktor packen
    'Randomize(Now.ToOADate) ' zufallszahlengenerator initialisieren
    Randomize()

    If IsConnected() = False Then Return
    If m_bTestMode Then StopTest()

    Reset()
    m_bTestMode = True
    cTestWords = TestWords

    ' Standard-Abfragerichtung aus der Datei laden
    If ldfManagement.LanguageInfo.TestDirection = xlsLanguageTestDirection.TestWord Then
      Me.m_bWordToMeaning = True
    Else
      Me.m_bWordToMeaning = False
    End If
  End Sub

  Overridable Sub Start(ByVal Language As String)
    ' Finde alle Wörter, die zu dieser Sprache passen heraus
    Dim cWords As Collection = New Collection
    Dim sCommand As String = "SELECT W.Index FROM DictionaryWords AS W, DictionaryMain AS M WHERE W.MainIndex = M.Index AND M.LanguageName='" & AddHighColons(Language) & "'"
    Dim DBCursor As OleDbDataReader = DBConnection.ExecuteReader(sCommand)
    Do While DBCursor.Read
      cWords.Add(SecureGetInt32(DBCursor, 0))
    Loop
    DBCursor.Close()
    Start(cWords)
  End Sub

  Private Sub Reset()
    cTestWords = Nothing
    m_iTestWordCountDone = 0
    m_iTestWordCountDoneRight = 0
    m_iTestWordCountDoneFalse = 0
    m_iTestWordCountDoneFalseAllTrys = 0
    m_bTestMode = False
  End Sub

  Overridable Sub StopTest()
    m_bTestMode = False
    Reset()
  End Sub

  Overridable Sub NextWord()

    Me.bFirstTest = True
    ' übernehmen, falls cards aus sind, ansonsten testen, ob es überhaupt abgefragt werden soll
    If Me.bUseCards = False Then
      ' Ein Wort aus der liste zufällig aussuchen und auf jeden fall übernehmen
      iTestCurrentWord = CInt(Int((cTestWords.Count * Rnd()) + 1)) ' zufälliges wort bestimmen
      iTestIndex = cTestWords.Item(iTestCurrentWord)
      TestDictionaryEntry = New xlsDictionaryEntry(DBConnection, iTestIndex)
    Else
      ' das kartensystem wird genutzt
      Dim cards As New xlsCards
      cards.DBConnection = DBConnection

      Do ' solange suchen, bis ein wort gefunden worden ist, das genommen werden kann
        ' index berechnen
        If cTestWords.Count = 0 Then Exit Do ' kein wort mehr da
        iTestCurrentWord = CInt(Int((cTestWords.Count * Rnd()) + 1)) ' zufälliges wort bestimmen
        iTestIndex = cTestWords.Item(iTestCurrentWord)
        ' counter verringern, wenn 1 wird exception ausgelöst
        Try
          cards.Update(iTestIndex)
          ' verringern hat geklappt, es muß also ein neues wort gesucht werden
          Me.DeleteWord() ' löschen, benutzt wird iTestCurrentWord
        Catch ex As xlsExceptionCards
          If ex.ErrorCode = 1 Then
            ' Schon 1, also wort übernehmen
            TestDictionaryEntry = New xlsDictionaryEntry(DBConnection, iTestIndex)
            Exit Do ' schleife verlassen
          Else
            ' anderer fehler
            Throw ex
          End If
        Catch ex As Exception
          ' anderer fehler
          Throw ex
        End Try
      Loop
    End If
  End Sub

  Overridable Function TestControl(ByVal input As String) As Integer
    ' Im einen Fall müssen pre, word und post eingegeben werden.
    ' im anderen fall wird geprüft, ob die bedeutung die richtige ist. wenn nicht, wird getestet, ob es
    ' diese Bedeutung auch gibt.

    ' 0 = kein fehler
    ' 1 = andere Bedeutung
    ' 2 = falsch

    If Not IsTesting() Then Return -1

    Dim iRight As Integer
    iRight = 0 ' kein fehler

    If m_bWordToMeaning Then
      ' testen, ob die bedeutung übereinstimmt
      If TestDictionaryEntry.Meaning <> input Then  ' wenn gleich ist, ist nichts zu tun
        ' prüfen, ob es die eingegebene bedeutung auch gibt
        ' zunächst die sprache herausfinden
        Dim sCommand As String = "SELECT LanguageName FROM DictionaryMain WHERE Index=" & TestDictionaryEntry.MainIndex & ";"
        Dim DBCursor As OleDbDataReader = DBConnection.ExecuteReader(sCommand)
        DBCursor.Read()
        Dim sLanguage As String = SecureGetString(DBCursor, 0)
        sCommand = "SELECT W.Index FROM DictionaryWords AS W, DictionaryMain AS M WHERE W.Word='" & AddHighColons(TestDictionaryEntry.Word) & "' AND W.Meaning='" & AddHighColons(input) & "' AND M.LanguageName='" & AddHighColons(sLanguage) & "' AND W.MainIndex=M.Index"
        DBCursor = DBConnection.ExecuteReader(sCommand)
        If DBCursor.HasRows = False Then iRight = 2 Else iRight = 1
        DBCursor.Close()
      End If
    Else
      ' das wort muß erkannt werden.
      If input <> TestDictionaryEntry.Word Then iRight = 2
    End If

    ' update des cards-systems, falls nötig
    If Me.bUseCards And bFirstTest Then
      Dim cards As New xlsCards
      cards.DBConnection = DBConnection
      If iRight = 0 Then
        cards.Update(TestDictionaryEntry.WordIndex, True)
      ElseIf iRight = 2 Then
        cards.Update(TestDictionaryEntry.WordIndex, False)
      End If
    End If
    If iRight = 0 Then ' falls richtig, löschen
      DeleteWord()
    End If

    If iRight <> 1 Then Me.bFirstTest = False ' damit keine zwei updates nacheinander gemacht werden können

    Return iRight
  End Function

  Sub DeleteWord()
    cTestWords.Remove(iTestCurrentWord)
  End Sub

  ReadOnly Property TestWord() As String
    Get
      If m_bWordToMeaning Then
        Return TestDictionaryEntry.Pre & " " & TestDictionaryEntry.Word & " " & TestDictionaryEntry.Post
      Else
        ' Ausgabe ist eine Bedeutung, es wird das dazu passende Wort gesucht
        Return TestDictionaryEntry.Meaning  ' Nur eine Bedeutung für das Wort
      End If
    End Get
  End Property

  ReadOnly Property Answer() As String
    Get
      If m_bWordToMeaning Then
        Return TestDictionaryEntry.Meaning
      Else
        Return TestDictionaryEntry.Word
      End If
    End Get
  End Property

  ReadOnly Property WordCountAllTests() As Integer
    Get
      Return WordCountDone + WordCountDoneFalseAllTrys
    End Get
  End Property

  ReadOnly Property WordCountDone() As Integer
    Get
      Return m_iTestWordCountDone
    End Get
  End Property

  ReadOnly Property WordCountDoneRight() As Integer
    Get
      Return m_iTestWordCountDoneRight
    End Get
  End Property

  ReadOnly Property WordCountDoneFalse() As Integer
    Get
      Return m_iTestWordCountDoneFalse
    End Get
  End Property

  ReadOnly Property WordCountDoneFalseAllTrys() As Integer
    Get
      Return m_iTestWordCountDoneFalseAllTrys
    End Get
  End Property

  Function IsTesting() As Boolean
    Return Me.m_bTestMode
  End Function
End Class