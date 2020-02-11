Imports System.Collections.ObjectModel
Imports Gravo
Imports Gravo.AccessDatabaseOperation

Public Enum TestResult
    NoError
    OtherMeaning
    Wrong
    Misspelled
End Enum

Public Enum xlsTestStyle
    TestOnce
    TestAgain
    RandomTestOnce
    RandomTestAgain
End Enum

' Abfragen von Vokabeln
Public Class xlsTestBase
    Inherits xlsBase

    ' Wörter, die abgefragt werden sollen
    Private testWords As Collection(Of Integer) = New Collection(Of Integer)
    Private testWordEntries As Collection(Of TestWord)
    Private nextWords As Collection(Of Integer) = New Collection(Of Integer)

    ' Abfragedetails, wie oft, welches Wort usw.
    Protected firstTest As Boolean = True        ' gibt an, ob _zwischen zwei NextWord()_ aufrufen das wort zum ersten mal geprüft wird,
    Protected firstRun As Boolean = True
    Private deleted As Boolean
    Private iTestIndex As Integer
    Protected TestDictionaryEntry As WordEntry
    Protected CurrentTestWord As TestWord
    Private iTestCurrentWord As Integer = -1

    ' Zähler
    Private m_iTestWordCountDone As Integer = 0
    Private m_iTestWordCountDoneRight As Integer = 0
    Private m_iTestWordCountDoneFalse As Integer = 0
    Private m_iTestWordCountDoneFalseAllTrys As Integer = 0

    ' Abfrageeinstellungen
    Private m_testStyle As xlsTestStyle = xlsTestStyle.RandomTestAgain
    Private m_useCards As Boolean = True          ' soll das Karteikarten-System benutzt werden?
    Private m_testSetPhrases As Boolean = True
    Private m_testFormerLanguage As Boolean = True ' Remove and use m_queryLanguage instead
    Private m_queryLanguage As QueryLanguage = QueryLanguage.TargetLanguage

    Public Sub New()
        MyBase.New()
    End Sub

    ' Suche _alle_ Wörter
    Overridable Sub Start()
        If IsConnected() = False Then Throw New Exception("Database not connected.")
        Dim words As Collection(Of Integer) = New Collection(Of Integer)
        Dim command As String
        If TestSetPhrases Then
            command = "SELECT W.[Index] FROM DictionaryWords AS W, DictionaryMain AS M WHERE W.MainIndex = M.[Index];"
        Else
            command = "SELECT W.[Index] FROM DictionaryWords AS W, DictionaryMain AS M WHERE W.MainIndex = M.[Index] AND (NOT W.WordType=5);"
        End If
        DBConnection.ExecuteReader(command)
        Do While DBConnection.DBCursor.Read
            words.Add(DBConnection.SecureGetInt32(0))
        Loop
        DBConnection.DBCursor.Close()
        Start(words)
    End Sub

    ' Finde alle Wörter, die zu dieser Sprache passen heraus
    Overridable Sub Start(ByVal Language As String)
        If IsConnected() = False Then Throw New Exception("Database not connected.")
        Dim words As Collection(Of Integer) = New Collection(Of Integer)
        Dim command As String
        If TestSetPhrases Then
            command = "SELECT W.[Index] FROM DictionaryWords AS W, DictionaryMain AS M WHERE W.MainIndex = M.[Index] AND M.LanguageName=" & GetDBEntry(Language) & ";"
        Else
            command = "SELECT W.[Index] FROM DictionaryWords AS W, DictionaryMain AS M WHERE W.MainIndex = M.[Index] AND M.LanguageName=" & GetDBEntry(Language) & " AND (NOT W.WordType=5);"
        End If
        DBConnection.ExecuteReader(command)
        Do While DBConnection.DBCursor.Read()
            words.Add(DBConnection.SecureGetInt32(0))
        Loop
        DBConnection.DBCursor.Close()
        Start(words)
    End Sub

    Overridable Sub Start(ByRef TestWords As Collection(Of Integer))  ' Wörter sollen übergeben werden Collection von indizes aus DictionaryWords
        Randomize()
        Reset()
        Me.testWords = TestWords
        nextWords = New Collection(Of Integer)
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

        ' übernehmen, falls cards aus sind, ansonsten testen, ob es überhaupt abgefragt werden soll
        If UseCards = False Then
            ' Ein Wort aus der liste zufällig aussuchen und auf jeden fall übernehmen
            iTestCurrentWord = CInt(Int((testWords.Count * Rnd()))) ' zufälliges wort bestimmen
            TestDictionaryEntry = testWordEntries.Item(iTestCurrentWord).WordEntry
        Else
            ' das Kartensystem wird genutzt
            Dim cards As New CardsDao(DBConnection)

            Do ' solange suchen, bis ein Wort gefunden worden ist, das genommen werden kann
                ' Index berechnen und beenden falls keine Wörter mehr da sind
                If testWords.Count = 0 Then
                    If TestStyle = xlsTestStyle.RandomTestAgain Or TestStyle = xlsTestStyle.TestAgain Then
                        testWords = nextWords
                        nextWords = New Collection(Of Integer)
                        firstRun = False
                        If testWords.Count = 0 Then Exit Do ' Auch in der anderen Liste kein Wort mehr da
                    Else
                        Exit Do
                    End If
                End If

                ' Wort rausfinden
                If TestStyle = xlsTestStyle.RandomTestAgain Or TestStyle = xlsTestStyle.RandomTestOnce Then
                    iTestCurrentWord = CInt(Int((testWords.Count * Rnd()))) ' zufälliges Wort bestimmen, von 0 bis testWords.Count - 1
                Else
                    iTestCurrentWord = 0
                End If
                iTestIndex = testWords.Item(iTestCurrentWord)

                ' Wenn firstRun nicht true ist, das Wort direkt übernehmen, Cards ist hier an
                If Not firstRun Then
                    TestDictionaryEntry = testWordEntries.Item(iTestCurrentWord).WordEntry
                    Exit Do
                End If

                ' Counter für Cards verringern, wenn 1 wird exception ausgelöst
                Try
                    cards.Skip(testWordEntries.Item(iTestCurrentWord).WordEntry, QueryLanguage)
                    ' verringern hat geklappt, es muß also ein neues Wort gesucht werden
                    DeleteWord() ' und das alte kann gelöscht werden, es wird ja nicht abgefragt
                Catch ex As Exception
                    ' anderer fehler
                    MsgBox("Unknon Error! Maybe an error in the Cards-Table?" & vbCrLf & "Error-Message: " & ex.Message, MsgBoxStyle.Critical, "Error")
                    Throw ex
                End Try
            Loop
        End If
    End Sub

    Overridable Function TestControl(ByVal input As String) As TestResult
        ' Im einen Fall müssen pre, word und post eingegeben werden.
        ' eigentlich. Noch nicht implementiert...
        ' im anderen Fall wird geprüft, ob die Bedeutung die richtige ist. wenn nicht, wird getestet, ob es
        ' diese Bedeutung auch gibt.

        Dim right As TestResult = TestResult.NoError
        If TestFormerLanguage Then
            ' testen, ob die bedeutung übereinstimmt
            If TestDictionaryEntry.Meaning <> input Then  ' Eine Ungleichheit wurde erkannt. Spezifizieren, welche.
                ' prüfen, ob es die eingegebene bedeutung auch gibt
                ' zunächst die Sprache herausfinden
                ' TODO: use dao objects to get information
                Dim command As String = "SELECT LanguageName, MainLanguage FROM DictionaryMain WHERE [Index] =" & TestDictionaryEntry.WordIndex & ";"
                DBConnection.ExecuteReader(command)
                DBConnection.DBCursor.Read()
                Dim language As String = DBConnection.SecureGetString(0)
                Dim mainLanguage As String = DBConnection.SecureGetString(1)
                DBConnection.DBCursor.Close()
                command = "SELECT W.[Index] FROM DictionaryWords AS W, DictionaryMain AS M WHERE W.Word=" & GetDBEntry(TestDictionaryEntry.Word) & " AND W.Meaning=" & GetDBEntry(input) & " AND M.LanguageName=" & GetDBEntry(language) & " AND M.MainLanguage=" & GetDBEntry(mainLanguage) & " AND W.MainIndex=M.[Index]"
                DBConnection.ExecuteReader(command)
                If DBConnection.DBCursor.HasRows = False Then
                    right = TestResult.Wrong
                Else
                    If TestDictionaryEntry.Meaning.ToUpper = input.ToUpper Then right = TestResult.Misspelled Else right = TestResult.OtherMeaning
                End If
                DBConnection.DBCursor.Close()
            End If
        Else
            ' Testen ob das italienische Wort korrekt eingegeben worden ist.
            If input <> TestDictionaryEntry.Word Then ' Eine Ungleichheit wurde erkannt. Spezifizieren, welche.
                ' prüfen, ob es das eingegebene Wort auch gibt
                ' zunächst die Sprache herausfinden
                ' TODO: use dao objects to get information
                Dim command As String = "SELECT LanguageName, MainLanguage FROM DictionaryMain WHERE Index=" & TestDictionaryEntry.WordIndex & ";"
                DBConnection.ExecuteReader(command)
                DBConnection.DBCursor.Read()
                Dim language As String = DBConnection.SecureGetString(0)
                Dim mainLanguage As String = DBConnection.SecureGetString(1)
                DBConnection.DBCursor.Close()
                command = "SELECT W.[Index] FROM DictionaryWords AS W, DictionaryMain AS M WHERE W.Word=" & GetDBEntry(input) & " AND W.Meaning=" & GetDBEntry(TestDictionaryEntry.Meaning) & " AND M.LanguageName=" & GetDBEntry(language) & " AND M.MainLanguage=" & GetDBEntry(mainLanguage) & " AND W.MainIndex=M.[Index]"
                DBConnection.ExecuteReader(command)
                If DBConnection.DBCursor.HasRows = False Then
                    right = TestResult.Wrong
                Else
                    If TestDictionaryEntry.Word.ToUpper = input.ToUpper Then right = TestResult.Misspelled Else right = TestResult.OtherMeaning
                End If
                'right = TestResult.Wrong
            End If
        End If

        ' Update des cards-systems, falls nötig
        If UseCards And firstTest Then
            Dim cards As New CardsDao(DBConnection)
            If right = TestResult.NoError Then
                cards.UpdateSuccess(TestDictionaryEntry, QueryLanguage)
                firstTest = False
            ElseIf right = TestResult.Wrong Then
                cards.UpdateFailure(TestDictionaryEntry, QueryLanguage)
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
            ' hier passiert nix, es muß nochmal abgefragt werden. auf jeden fall war ja nichts falsch...
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

    ' Einstellungen
    Public Property TestSetPhrases() As Boolean
        Get
            Return m_testSetPhrases
        End Get
        Set(ByVal value As Boolean)
            m_testSetPhrases = value
        End Set
    End Property

    Public Property TestFormerLanguage() As Boolean
        Get
            Return m_testFormerLanguage
        End Get
        Set(ByVal value As Boolean)
            m_testFormerLanguage = value
        End Set
    End Property

    Public Property TestStyle() As xlsTestStyle
        Get
            Return m_testStyle
        End Get
        Set(ByVal testStyle As xlsTestStyle)
            m_testStyle = testStyle
        End Set
    End Property

    ' Ausgaben für das Wort
    ReadOnly Property AdditionalInfo() As String
        Get
            If TestDictionaryEntry Is Nothing Then Return ""
            Return TestDictionaryEntry.AdditionalTargetLangInfo
        End Get
    End Property

    ReadOnly Property TestWord() As String
        Get
            If TestDictionaryEntry Is Nothing Then Return ""
            If TestFormerLanguage Then
                Return TestDictionaryEntry.Pre & " " & TestDictionaryEntry.Word & " " & TestDictionaryEntry.Post
            Else
                ' Ausgabe ist eine Bedeutung, es wird das dazu passende Wort gesucht
                Return TestDictionaryEntry.Meaning  ' Nur eine Bedeutung für das Wort
            End If
        End Get
    End Property

    ReadOnly Property Answer() As String
        Get
            If TestDictionaryEntry Is Nothing Then Return ""
            If TestFormerLanguage Then
                Return TestDictionaryEntry.Meaning
            Else
                Return TestDictionaryEntry.Word
            End If
        End Get
    End Property

    ' Ausgaben für die Zähler
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

    ' Gibt die Anzahl der noch zu prüfenden Vokabeln an, _nicht_ die tatsächlich geprüft werden.
    ' Verschiebungen durch das Cards-System sind möglich.
    ReadOnly Property WordCount() As Integer
        Get
            If testWords.Count <> 0 Then Return testWords.Count Else Return nextWords.Count
        End Get
    End Property

    Public Property UseCards() As Boolean
        Get
            Return m_useCards
        End Get
        Set(ByVal value As Boolean)
            m_useCards = value
        End Set
    End Property

    Public ReadOnly Property QueryLanguage As QueryLanguage
        Get
            Return m_queryLanguage
        End Get
    End Property
End Class
