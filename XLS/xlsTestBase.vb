Imports System.Collections.ObjectModel
Imports Gravo2k7.AccessDatabaseOperation

Public Enum TestResult
  NoError
  OtherMeaning
  Wrong
  Misspelled
End Enum

Enum xlsTestStyle
  TestOnce
  TestAgain
  RandomTestOnce
  RandomTestAgain
End Enum

' Abfragen von Vokabeln
Public Class xlsTestBase
  Inherits xlsBase

  Private testWords As Collection(Of Integer)
  Private nextWords As Collection(Of Integer)

  Private firstTest As Boolean = True        ' gibt an, ob _zwischen zwei NextWord()_ aufrufen das wort zum ersten mal gepr�ft wird,
  Private firstRun As Boolean = True
  Private testStyle As xlsTestStyle = xlsTestStyle.RandomTestAgain
  Private deleted As Boolean

  Private m_iTestWordCountDone As Integer = 0
  Private m_iTestWordCountDoneRight As Integer = 0
  Private m_iTestWordCountDoneFalse As Integer = 0
  Private m_iTestWordCountDoneFalseAllTrys As Integer = 0

  Private m_bWordToMeaning As Boolean

  Private iTestIndex As Integer
  Private TestDictionaryEntry As xlsDictionaryEntry

  Private iTestCurrentWord As Integer = -1

  Private bUseCards As Boolean = True         ' soll das Karteikarten-System benutzt werden?

  Public Sub New()

  End Sub

  ' Suche _alle_ W�rter
  Overridable Sub Start()
    If IsConnected() = False Then Throw New Exception("Database not connected.")
    Dim words As Collection(Of Integer) = New Collection(Of Integer)
    Dim command As String = "SELECT W.Index FROM DictionaryWords AS W, DictionaryMain AS M WHERE W.MainIndex = M.Index;"
    DBConnection.ExecuteReader(command)
    Do While DBConnection.DBCursor.Read
      words.Add(DBConnection.SecureGetInt32(0))
    Loop
    DBConnection.DBCursor.Close()
    Start(words)
  End Sub

  ' Finde alle W�rter, die zu dieser Sprache passen heraus
  Overridable Sub Start(ByVal Language As String)
    If IsConnected() = False Then Throw New Exception("Database not connected.")
    Dim words As Collection(Of Integer) = New Collection(Of Integer)
    Dim command As String = "SELECT W.Index FROM DictionaryWords AS W, DictionaryMain AS M WHERE W.MainIndex = M.Index AND M.LanguageName='" & AddHighColons(Language) & "';"
    DBConnection.ExecuteReader(command)
    Do While DBConnection.DBCursor.Read()
      words.Add(DBConnection.SecureGetInt32(0))
    Loop
    DBConnection.DBCursor.Close()
    Start(words)
  End Sub

  Overridable Sub Start(ByRef TestWords As Collection(Of Integer))  ' W�rter sollen �bergeben werden Collection von indizes aus DictionaryWords
    Randomize()
    Reset()
    Me.testWords = TestWords
    nextWords = New Collection(Of Integer)

    ' Standard-Abfragerichtung aus der Datei laden
    m_bWordToMeaning = True
  End Sub

  Private Sub Reset()
    If testWords IsNot Nothing Then testWords.Clear()
    testWords = Nothing
    If nextWords IsNot Nothing Then nextWords.Clear()
    nextWords = Nothing
    m_iTestWordCountDone = 0
    m_iTestWordCountDoneRight = 0
    m_iTestWordCountDoneFalse = 0
    m_iTestWordCountDoneFalseAllTrys = 0
    firstRun = True
  End Sub

  Overridable Sub StopTest()
    Reset()
  End Sub

  Overridable Sub NextWord()
    firstTest = IIf(firstRun, True, False)
    deleted = False

    ' �bernehmen, falls cards aus sind, ansonsten testen, ob es �berhaupt abgefragt werden soll
    If Me.bUseCards = False Then
      ' Ein Wort aus der liste zuf�llig aussuchen und auf jeden fall �bernehmen
      iTestCurrentWord = CInt(Int((testWords.Count * Rnd()))) ' zuf�lliges wort bestimmen
      iTestIndex = testWords.Item(iTestCurrentWord)
      TestDictionaryEntry = New xlsDictionaryEntry(DBConnection, iTestIndex)
    Else
      ' das Kartensystem wird genutzt
      Dim cards As New xlsCards
      cards.DBConnection = DBConnection

      Do ' solange suchen, bis ein Wort gefunden worden ist, das genommen werden kann
        ' Index berechnen und beenden falls keine W�rter mehr da sind
        If testWords.Count = 0 Then
          If testStyle = xlsTestStyle.RandomTestAgain Then
            testWords = nextWords
            nextWords = New Collection(Of Integer)
            firstRun = False
            If testWords.Count = 0 Then Exit Do ' Auch in der anderen Liste kein Wort mehr da
          Else
            Exit Do
          End If
        End If

        ' Wort rausfinden
        iTestCurrentWord = CInt(Int((testWords.Count * Rnd()))) ' zuf�lliges wort bestimmen, von 0 bis count-1
        iTestIndex = testWords.Item(iTestCurrentWord)

        ' Wenn firstRun nicht true ist, das Wort direkt �bernehmen, cards ist hier an
        If Not firstRun Then
          TestDictionaryEntry = New xlsDictionaryEntry(DBConnection, iTestIndex)
          Exit Do
        End If

        ' Counter f�r Cards verringern, wenn 1 wird exception ausgel�st
        Try
          cards.Update(iTestIndex)
          ' verringern hat geklappt, es mu� also ein neues Wort gesucht werden
          DeleteWord() ' und das alte kann gel�scht werden, es wird ja nicht abgefragt
        Catch ex As xlsExceptionCards
          If ex.ErrorCode = 1 Then
            ' Schon 1, also Wort �bernehmen
            TestDictionaryEntry = New xlsDictionaryEntry(DBConnection, iTestIndex)
            Exit Do
          Else
            ' anderer fehler
            Throw ex
          End If
        Catch ex As Exception
          ' anderer fehler
          MsgBox("D�rfte eigentlich nicht vorkommen! Evtl. ein Fehler mit der Count-Tabelle?" & vbCrLf & "Nachricht: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
          Throw ex
        End Try
      Loop
    End If
  End Sub

  Overridable Function TestControl(ByVal input As String) As TestResult
    ' Im einen Fall m�ssen pre, word und post eingegeben werden.
    ' im anderen fall wird gepr�ft, ob die bedeutung die richtige ist. wenn nicht, wird getestet, ob es
    ' diese Bedeutung auch gibt.

    Dim right As TestResult = TestResult.NoError

    ' Test, ob richtig oder falsch
    If m_bWordToMeaning Then
      ' testen, ob die bedeutung �bereinstimmt
      If TestDictionaryEntry.Meaning <> input Then  ' wenn gleich ist, ist nichts zu tun
        ' pr�fen, ob es die eingegebene bedeutung auch gibt
        ' zun�chst die sprache herausfinden
        Dim command As String = "SELECT LanguageName FROM DictionaryMain WHERE Index=" & TestDictionaryEntry.MainIndex & ";"
        DBConnection.ExecuteReader(command)
        DBConnection.DBCursor.Read()
        Dim language As String = DBConnection.SecureGetString(0)
        DBConnection.DBCursor.Close()
        command = "SELECT W.Index FROM DictionaryWords AS W, DictionaryMain AS M WHERE W.Word='" & AddHighColons(TestDictionaryEntry.Word) & "' AND W.Meaning='" & AddHighColons(input) & "' AND M.LanguageName='" & AddHighColons(language) & "' AND W.MainIndex=M.Index"
        DBConnection.ExecuteReader(command)
        If DBConnection.DBCursor.HasRows = False Then
          right = TestResult.Wrong
        Else
          If TestDictionaryEntry.Meaning.ToUpper = input.ToUpper Then right = TestResult.Misspelled Else right = TestResult.OtherMeaning
        End If
        DBConnection.DBCursor.Close()
      End If
    Else
      ' das Wort mu� erkannt werden.
      If input <> TestDictionaryEntry.Word Then right = TestResult.Wrong
    End If

    ' Update des cards-systems, falls n�tig
    If bUseCards And firstTest Then
      Dim cards As New xlsCards
      cards.DBConnection = DBConnection
      If right = TestResult.NoError Then
        cards.Update(TestDictionaryEntry.WordIndex, True)
        firstTest = False
      ElseIf right = TestResult.Wrong Then
        cards.Update(TestDictionaryEntry.WordIndex, False)
        firstTest = False
      End If
    End If

    ' Wortlisten aktualisieren
    If right = TestResult.NoError And Not deleted Then ' direkt richtig beantworted
      DeleteWord()
      deleted = True
    ElseIf right = TestResult.Wrong And Not deleted Then
      ' sichern, falls nochmal abgefragt werden soll!
      DeleteWord(True)
      deleted = True
    Else
      ' hier passiert nix, es mu� nochmal abgefragt werden. auf jeden fall war ja nichts falsch...
    End If

    Return right
  End Function

  Sub DeleteWord()
    testWords.Remove(iTestIndex)
  End Sub

  Sub DeleteWord(ByVal testAgain As Boolean)
    If testAgain Then
      nextWords.Add(iTestIndex)
      testWords.Remove(iTestIndex)
    Else
      DeleteWord()
    End If
  End Sub

  ReadOnly Property AdditionalInfo() As String
    Get
      Return TestDictionaryEntry.AdditionalTargetLangInfo
    End Get
  End Property

  ReadOnly Property TestWord() As String
    Get
      If TestDictionaryEntry Is Nothing Then Return ""
      If m_bWordToMeaning Then
        Return TestDictionaryEntry.Pre & " " & TestDictionaryEntry.Word & " " & TestDictionaryEntry.Post
      Else
        ' Ausgabe ist eine Bedeutung, es wird das dazu passende Wort gesucht
        Return TestDictionaryEntry.Meaning  ' Nur eine Bedeutung f�r das Wort
      End If
    End Get
  End Property

  ReadOnly Property Answer() As String
    Get
      If TestDictionaryEntry Is Nothing Then Return ""
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

  ' Gibt die Anzahl der noch zu pr�fenden Vokabeln an, _nicht_ die tats�chlich gepr�ft werden.
  ' Verschiebungen durch das Cards-System sind m�glich.
  ReadOnly Property WordCount() As Integer
    Get
      If testWords.Count <> 0 Then Return testWords.Count Else Return nextWords.Count
    End Get
  End Property
End Class