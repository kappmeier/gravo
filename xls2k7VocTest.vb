Imports System.Data.OleDb

Public Class xlsVocTest
  Inherits xlsVocBase
  ' Interne Variablen
  Private cTestWords As Collection
  Private cTestWordsNext As Collection
  Private iTestCurrentWord As Integer = 0             ' Position des zu testenden wortes in der Liste
  Private ldfManagement As xlsLDFManagement

  ' Membervariablen
  Private m_bTestMode As Boolean = False

  ' Statistiken
  Private m_iTestWordCountAll As Integer
  Private m_iTestWordCountToDo As Integer
  Private m_iTestWordCountDone As Integer
  Private m_iTestWordCountDoneCorrection As Integer ' Noch zu Korrigierende Vokabeln, die momentan aus der Liste entfernt sind
  Private m_iTestWordCountDoneRight As Integer
  Private m_iTestWordCountDoneFalse As Integer
  Private m_iTestWordCountDoneFalseAllTrys As Integer

  ' Modes für die Abfrage
  Private m_iTestNextMode As Integer = 0                    ' Bestimmen des nächsten abzufragenden wortes, 0 = der Reihe nach
  Private m_bAllMeanings As Boolean = True                  ' Bestimmt, ob alle Bedeutungen gewuß werden müssen
  Private m_bWordToMeaning As Boolean                       ' Legt fest, ob zu einem Wort die Bedeutungen abgefragt werden
  Private m_bFirstTryMode As Boolean                        ' Im FirstTry-Mode wird das Ergebnis des ersten Abfragens nicht gespeichert
  Private m_iTestNextModeWrong As Integer = 4               ' Legt fest, wie das neue Wort gefunden wird, wenn es falsch war.
  Private bFirstRunOver As Boolean = False                  ' Speichert, ob der erste Durchgang abgeschlossen wurde
  Private m_TestSystem As xlsTestSystem = xlsTestSystem.All ' Speichert das TestSystem


  ' Noch nicht geupdated

  ' Allgemein
  Protected bErneut As Boolean = False

  'Informationen über die gewählte Sprache
  Protected ldfCurrentLanguage As New xlsLanguageDefinitionOld

  ' Modes für die Abfrage
  Protected m_iTestWordMode As Integer
  Protected m_iExtendedTestMode As xlsVocTestExtended
  Protected m_iHelpMode As Integer
  Protected m_bRequiredOnly As Boolean

  ' Abfrage-Zähler
  Protected m_iTestWordCountHelp1 As Integer
  Protected m_iTestWordCountHelp2 As Integer
  Protected m_iTestWordCountHelp3 As Integer

  Sub New(ByVal db As AccessDatabaseOperation, ByVal Table As String)  ' Bestimmte Tabelle zum Zugriff öffnen
    MyBase.new(db, Table)
    ldfManagement = New xlsLDFManagement
    ldfManagement.LDFPath = Application.StartupPath()
  End Sub

  Sub New(ByVal db As AccessDatabaseOperation)   ' Keinen Speziellen Table auswählen
    MyBase.New(db)
    ldfManagement = New xlsLDFManagement
    ldfManagement.LDFPath = Application.StartupPath()
  End Sub

  Overridable Sub Start(ByRef TestWords As Collection)  ' Wörter sollen übergeben werden als WortInformation Collection
    Randomize(Now.ToOADate) ' zufallszahlengenerator initialisieren

    If IsConnected() = False Then Return
    If m_bTestMode Then StopTest()

    m_bTestMode = True
    Reset()
    cTestWords = TestWords
    bErneut = False    '?
    m_iTestWordCountToDo = cTestWords.Count
    m_iTestWordCountAll = m_iTestWordCountToDo

    ' Standard-Abfragerichtung aus der Datei laden
    If ldfManagement.LanguageInfo.TestDirection = xlsLanguageTestDirection.TestWord Then
      Me.m_bWordToMeaning = True
    Else
      Me.m_bWordToMeaning = False
    End If
  End Sub

  Overridable Sub Start()
    ' Starten mit der aktuell ausgewählten Unit als Abfrage-Objekt
    If IsUnitSelected() = False Then Return

    ' Wörter aus der aktuell gewählten Unit zusammensuchen gemäß den Vorgaben
    Select Case m_TestSystem
      Case xlsTestSystem.All
        Start(Words) ' Wörter in dieser Unit übergeben
      Case xlsTestSystem.Cards
        ' Es dürfen nur Wörter übergeben werden, die Abfrage-Counter + 1 haben.
        ' alle anderen müssen im Counter um 1 verringert werden
        ' Dies muß geschehen, wenn ordnungsgemäß gestoppt wird
        ' Falls vorher abgebrochen wird, werden nur die getesteten aktualisiert
        Dim sCommand As String
        Dim cWords As Collection = New Collection

        cWords = New Collection
        sCommand = "SELECT A.WordNumber, A.Word FROM " & CurrentGroupName & " A, " & CurrentGroupName & "Stats B WHERE A.UnitNumber=" & CurrentUnitNumber & " AND A.Deleted=" & False & " AND B.NextTest=1 ORDER BY A.WordInUnit"
        ExecuteReader(sCommand)
        Dim wiNew As xlsWordInformation
        Do While DBCursor.Read
          wiNew = New xlsWordInformation
          wiNew.Group = CurrentGroupName
          wiNew.Number = DBCursor.GetInt32(0)
          cWords.Add(wiNew)
        Loop
        DBCursor.Close()
        Start(cWords)
    End Select
  End Sub

  Private Sub Reset()
    cTestWords = Nothing
    cTestWordsNext = New Collection
    m_iTestWordCountAll = 0
    m_iTestWordCountToDo = 0
    m_iTestWordCountDone = 0
    m_iTestWordCountDoneRight = 0
    m_iTestWordCountDoneFalse = 0
    m_iTestWordCountDoneFalseAllTrys = 0
    m_iTestWordCountDoneCorrection = 0
    bFirstRunOver = False
  End Sub

  Overridable Sub StopTest()
    Dim sCommand As String = "UPDATE " & CurrentGroupName & "Stats SET AbfrageGestartet=" & False & " WHERE AbfrageGestartet=" & True & ";"
    ExecuteReader(sCommand)
    m_bTestMode = False
    Reset()
  End Sub

  Public Overrides Property CurrentWordNumber() As Integer
    Get
      Return MyBase.CurrentWordNumber
    End Get
    Set(ByVal Value As Integer)
      MyBase.CurrentWordNumber = Value
      SelectUnit(CurrentWord.UnitNumber)
    End Set
  End Property

  Overridable Sub NextWord()
    If IsConnected() = False Or m_bTestMode = False Then Exit Sub

    If cTestWords.Count = 0 Then ' alle wörter in der liste bearbeitet. suchen von markierten
      Select Case m_iTestNextModeWrong
        Case 4 ' Fehlerhafte abfragen bis Korrekt, neue Liste am Ende
          ' hinzufügen von allen wörtern die markiert sind.
          ' vorhanden in cTestWordsNext
          cTestWords = cTestWordsNext
          cTestWordsNext = New Collection
          m_iTestWordCountDoneCorrection = 0
          Me.bFirstRunOver = True
      End Select
      If cTestWords.Count = 0 Then Exit Sub ' tatsächlich ende
    End If

    Select Case m_iTestNextMode
      Case 0    ' Der Reihe nach
        SelectGroup(cTestWords(1).Group)
        CurrentWordNumber = cTestWords(1).number
        iTestCurrentWord = 1
      Case 1    ' Zufällig alle gewählten
        'If bErneut = False Then ' welchen sinn hat das hier ?
        iTestCurrentWord = CInt(Int((cTestWords.Count * Rnd()) + 1)) ' zufälliges wort bestimmen
        SelectGroup(cTestWords(iTestCurrentWord).Group)
        'GetWord(cTestWords(iNext).WordNumber)
        CurrentWordNumber = cTestWords(iTestCurrentWord).number
        'End If
      Case Else
        MsgBox("Dieser Abfrage-Modus wird zur zeit nicht unterstützt! Zurücksetzen auf standard!")
        m_iTestNextMode = 0
        NextWord()
    End Select

    'ldfCurrentLanguage.LoadLDF(Me.Language)     'Die LDF-Datei zu der ausgewählten Sprache laden
    'CurrentWord.Extended1 = ldfCurrentLanguage.CreateExtended1(CurrentWord)
    'CurrentWord.Extended2 = ldfCurrentLanguage.CreateExtended2(CurrentWord)
    'CurrentWord.Extended3 = ldfCurrentLanguage.CreateExtended3(CurrentWord)

    'If ldfCurrentLanguage.TestDirection = xlsLanguageTestDirection.TestMeaning Then
    '  m_bWordToMeaning = True
    'Else
    '  m_bWordToMeaning = False
    'End If
  End Sub

  Public Overrides Sub SelectGroup(ByVal sGroupname As String)
    If ldfManagement Is Nothing Then Exit Sub
    Dim oldLanguage As String = Me.Language
    Dim oldLDFType As String = Me.LDFType
    MyBase.SelectGroup(sGroupname)
    If (oldLanguage <> Language) Or (oldLDFType <> LDFType) Then
      Me.ldfManagement.SelectLD(Language, LDFType)
    End If

  End Sub

  Overridable Function TestControl(ByVal Word As String, ByRef Meanings As ArrayList, Optional ByVal Irregular1 As String = "", Optional ByVal Irregular2 As String = "", Optional ByVal Irregular3 As String = "") As Boolean
    If IsConnected() = False Then Exit Function
    If (Not m_bTestMode) Or (Not IsGroupSelected()) Then Exit Function

    If Meanings.Count = 0 Then Return False

    Dim bRight As Boolean
    bRight = True

    Dim words As ArrayList = New ArrayList(CurrentWord.Meaning)

    If m_bWordToMeaning Then
      ' alle bedeutungen abfragen (aus meanings) oder nicht, falls m_ballmeanings = false
      If Me.m_bAllMeanings Then
        ' Für jedes Wort überprüfen, ob es korrekt ist
        Dim i As Integer
        If Meanings.Count <> words.Count Then Return False
        For i = 1 To words.Count
          If Not words.Contains(Meanings(i - 1)) Then bRight = False : Exit For
        Next
      Else
        ' nur eine Bedeutung erkannt reicht aus
      End If
    Else
      ' das wort muß erkannt werden.
      If Meanings.Count <> 1 Then Return False
      If Meanings(0) <> CurrentWord.Word Then bRight = False
    End If

    Return bRight
    'If ((m_iIrregular = xlsVocTestExtended.Always) Or (m_iIrregular = xlsVocTestExtended.IrregularOnly And wtWord.ExtendedIsValid)) Then
    ' TODO irreguläre werden immer abgefragt
    If Irregular1 <> ExtendedAnswer1 Then bRight = False
    If Irregular2 <> ExtendedAnswer2 Then bRight = False
    If Irregular3 <> ExtendedAnswer3 Then bRight = False
    'End If
  End Function

  ReadOnly Property TestWord() As String
    Get
      If m_bWordToMeaning Then
        Return CurrentWord.Pre & " " & CurrentWord.Word & " " & CurrentWord.Post
      Else
        Dim sTemp As String
        Dim sWord As String = ""
        For Each sTemp In CurrentWord.Meaning
          sWord += Trim(sTemp) & ", "
        Next
        ' TODO falls CurrentWord.Meaning leer ist, tritt exception auf
        If Len(sWord) <> 0 Then sWord = Left(sWord, Len(sWord) - 2)
        'sWord += " " & CurrentWord.AdditionalTargetLangInfo
        Return sWord
      End If
    End Get
  End Property

  ReadOnly Property AnswerCount() As Integer
    Get
      If m_bWordToMeaning Then
        Return CurrentWord.Meaning.Length
      Else
        Return 1
      End If
    End Get
  End Property

  ReadOnly Property Answer(ByVal index As Integer) As String
    Get
      If m_bWordToMeaning Then
        Return CurrentWord.Meaning(index - 1)
      Else
        If index = 1 Then

        Return CurrentWord.Word
          ' TODO index out of range exception
        Else
          Throw New Exception
        End If
      End If
    End Get
  End Property

  ReadOnly Property ExtendedAnswer1() As String
    Get
      If m_iExtendedTestMode = xlsVocTestExtended.Never Then Return ""
      If (m_iExtendedTestMode = xlsVocTestExtended.IrregularOnly) And (CurrentWord.ExtendedIsValid) Then Return CurrentWord.Extended1
      If m_iExtendedTestMode = xlsVocTestExtended.Always Then Return CurrentWord.Extended1
      Return ""
    End Get
  End Property

  ReadOnly Property ExtendedAnswer2() As String
    Get
      If m_iExtendedTestMode = xlsVocTestExtended.Never Then Return ""
      If (m_iExtendedTestMode = xlsVocTestExtended.IrregularOnly) And (CurrentWord.ExtendedIsValid) Then Return CurrentWord.Extended2
      If m_iExtendedTestMode = xlsVocTestExtended.Always Then Return CurrentWord.Extended2
      Return ""
    End Get
  End Property

  ReadOnly Property ExtendedAnswer3() As String
    Get
      If m_iExtendedTestMode = xlsVocTestExtended.Never Then Return ""
      If (m_iExtendedTestMode = xlsVocTestExtended.IrregularOnly) And (CurrentWord.ExtendedIsValid) Then Return CurrentWord.Extended3
      If m_iExtendedTestMode = xlsVocTestExtended.Always Then Return CurrentWord.Extended3
      Return ""
    End Get
  End Property

  Shared ReadOnly Property NextWordModes() As ArrayList
    Get
      Dim asList As New ArrayList
      asList.Add("Nacheinander")    ' 0
      asList.Add("Zufällig")     ' 1
      ' sind diese sinvoll? geht zur zeit eh nicht, da nicht festgestellt werden kann aus welcher lektion
      ' oder sprache ein gegebenes wort kommt. die unit darf sich nicht ändern!
      'asList.Add("Nacheinander, zufällige Lektionen")   ' 2
      'asList.Add("Zufällig in Lektionen")     ' 3
      'asList.Add("Zufällig in Sprachen")      ' 4
      'asList.Add("Zufällig in Sprachen und Lektionen")  ' 5
      Return asList
    End Get
  End Property

  Property NextWordMode() As Integer
    Get
      Return m_iTestNextMode
    End Get
    Set(ByVal Value As Integer)
      m_iTestNextMode = Value
    End Set
  End Property

  Property TestSystem() As xlsTestSystem
    Get
      Return m_TestSystem
    End Get
    Set(ByVal system As xlsTestSystem)
      m_TestSystem = system
    End Set
  End Property

  ReadOnly Property WordCountAll() As Integer
    Get
      Return m_iTestWordCountAll
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

  ReadOnly Property WordCountDoneWithHelpAll() As Integer
    Get
      Return m_iTestWordCountHelp1 + m_iTestWordCountHelp2 + m_iTestWordCountHelp3
    End Get
  End Property

  ReadOnly Property WordCountDoneWithHelp1() As Integer
    Get
      Return m_iTestWordCountHelp1
    End Get
  End Property

  ReadOnly Property WordCountDoneWithHelp2() As Integer
    Get
      Return m_iTestWordCountHelp2
    End Get
  End Property

  ReadOnly Property WordCountDoneWithHelp3() As Integer
    Get
      Return m_iTestWordCountHelp3
    End Get
  End Property

  ReadOnly Property WordCountToDo() As Integer
    Get
      Return m_iTestWordCountToDo
    End Get
  End Property











  Public Sub UpdateStats(ByVal Right As Boolean)

    ' TODO teilweise aktualisiert. nicht alle abfragemodi sind aktualisiert

    '**********************************
    '* Aktualisierung der Statistiken *
    '**********************************
    Dim iTests, iTestsAll, iRight, iWrong, iWrongAll As Integer
    Dim iHelp1, iHelp2, iHelp3 As Integer
    Dim sFirst As String
    Dim sLast As String
    Dim bTestStart As Boolean
    Dim bFirstTry As Boolean
    Dim sCommand As String

    'BConnection.Open(dbconnection)
    sCommand = "SELECT Abfragen, AbfragenGesamt, Richtig, Falsch, FalschGesamt, AbfrageGestartet, ErsteAbfrage, LetzteAbfrage FROM " & CurrentGroupName & "Stats WHERE WordNumber=" & CurrentWordNumber & ";"
    ExecuteReader(sCommand)
    DBCursor.Read()
    If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iTests = 0 Else iTests = DBCursor.GetValue(0)
    If TypeOf (DBCursor.GetValue(1)) Is DBNull Then iTestsAll = 0 Else iTestsAll = DBCursor.GetValue(1)
    If TypeOf (DBCursor.GetValue(2)) Is DBNull Then iRight = 0 Else iRight = DBCursor.GetValue(2)
    If TypeOf (DBCursor.GetValue(3)) Is DBNull Then iWrong = 0 Else iWrong = DBCursor.GetValue(3)
    If TypeOf (DBCursor.GetValue(4)) Is DBNull Then iWrongAll = 0 Else iWrongAll = DBCursor.GetValue(4)
    If TypeOf (DBCursor.GetValue(5)) Is DBNull Then bTestStart = False Else bTestStart = DBCursor.GetBoolean(5)
    If TypeOf (DBCursor.GetValue(6)) Is DBNull Then sFirst = "01.01.1900" Else sFirst = DBCursor.GetDateTime(6)
    If TypeOf (DBCursor.GetValue(7)) Is DBNull Then sLast = "01.01.1900" Else sLast = DBCursor.GetDateTime(7)
    DBCursor.Close()
    sCommand = "SELECT Hilfe1Richtig, Hilfe2Richtig, Hilfe3Richtig FROM " & CurrentGroupName & "Stats WHERE WordNumber=" & CurrentWordNumber & ";"
    ExecuteReader(sCommand)
    DBCursor.Read()
    If TypeOf (DBCursor.GetValue(0)) Is DBNull Then iHelp1 = 0 Else iHelp1 = DBCursor.GetValue(0)
    If TypeOf (DBCursor.GetValue(1)) Is DBNull Then iHelp2 = 0 Else iHelp2 = DBCursor.GetValue(1)
    If TypeOf (DBCursor.GetValue(2)) Is DBNull Then iHelp3 = 0 Else iHelp3 = DBCursor.GetValue(2)
    DBCursor.Close()

    'CurrentWord.LastTested = Format(Now, "dd.MM.yyyy")
    Dim sNow As String = NowDB()
    If sFirst = "01.01.1900" And sLast = "01.01.1900" Then ' Erste Abfrage tatsächlich
      If m_bFirstTryMode = True Then ' Erste Abfrage-Modus. Erkennung: ErsteAbfrage bleibt uninitialisiert
        bFirstTry = True
      Else ' Setze ErsteAbfrage da der erste Abfrage-Modus aus ist
        bFirstTry = False
        sCommand = "UPDATE " & CurrentGroupName & "Stats SET ErsteAbfrage='" & sNow & "' WHERE WordNumber=" & CurrentWordNumber & ";"
      End If
    Else
      If sFirst = "01.01.1900" Then ' Falls letztes mal erste Abfrage war, setze ErsteAbfrage jetzt.
        bFirstTry = False
        sCommand = "UPDATE " & CurrentGroupName & "Stats SET ErsteAbfrage='" & sNow & "' WHERE WordNumber=" & CurrentWordNumber & ";"
        ExecuteNonQuery(sCommand)
      End If
      bFirstTry = False
    End If
    If Right = True Then    ' richtige Antwort
      If bFirstRunOver = False Then
        Select Case m_iHelpMode    ' Test ob Hilfe benutzt wurde
          Case xlsVocTestHelpModes.NoHelp
            m_iTestWordCountDoneRight += 1
            iRight += 1
          Case xlsVocTestHelpModes.LightHelp
            m_iTestWordCountHelp1 += 1
            iHelp1 += 1
          Case xlsVocTestHelpModes.MiddleHelp
            m_iTestWordCountHelp2 += 1
            iHelp2 += 1
          Case xlsVocTestHelpModes.HeavyHelp
            m_iTestWordCountHelp3 += 1
            iHelp3 += 1
        End Select
        iTests += 1
        iTestsAll += 1
      Else
        If iTests = 0 Then iTests = 1 ' Falls vorher FirstTry war, ist iTest s = 0, muß dann jetzt hier auf 1 gesetzt werden.
        iTestsAll += 1
      End If
      bTestStart = False
      cTestWords.Remove(iTestCurrentWord)
      bErneut = False
    Else    ' falsche antwort
      m_iTestWordCountDoneFalseAllTrys += 1
      If Me.bFirstRunOver = False Then
        m_iTestWordCountDoneFalse += 1
        iTests += 1
        iTestsAll += 1
        iWrong += 1
        iWrongAll += 1
      Else
        iTestsAll += 1
        iWrongAll += 1
      End If

      Select Case m_iTestNextModeWrong     ' Eventuelle Wort-Neu-Abfrage Testen:
        Case 0    ' Fehlerhafte sofort abfragen bis Korrekt
          'bTestStart = True
          bErneut = True
        Case 1    'Fehlerhafte sofort erneut abfragen
          If bErneut = False Then
            bErneut = True
            'bTestStart = True
          Else
            cTestWords.Remove(iTestCurrentWord)
            bErneut = False
            'bTestStart = False
          End If
        Case 2    'Fehlerhafte abfragen bis Korrekt, in Liste einfügen
          'bTestStart = True
          'Dim structWord As xlsWordListInfo
          'structWord.group = CurrentGroupName
          'structWord.WordNumber = CurrentWordNumber
          cTestWords.Remove(iTestCurrentWord)
          'wtTestWords.Add(structword)
        Case 3    ' Fehlerhafte erneut abfragen, in Liste einfügen
          If bTestStart = True Then
            bTestStart = False
            cTestWords.Remove(iTestCurrentWord)
          Else
            bTestStart = True
            'Dim structWord As xlsWordListInfo
            'structWord.Group = CurrentGroupName
            'structWord.WordNumber = CurrentWordNumber
            cTestWords.Remove(iTestCurrentWord)
            'wtTestWords.Add(structWord)
          End If
        Case 4     ' Fehlerhafte abfragen bis Korrekt, neue Liste am Ende
          m_iTestWordCountDoneCorrection += 1
          'bTestStart = True ' völlig egal für typ 4
          ' hinzufügen in die next-collection
          cTestWordsNext.Add(cTestWords(iTestCurrentWord))
          cTestWords.Remove(iTestCurrentWord)
        Case 5     ' Fehlerhafte erneut abfragen, neue Liste am Ende
          If bTestStart = True Then
            bTestStart = False
          Else
            m_iTestWordCountDoneCorrection += 1
            bTestStart = True
          End If
          cTestWords.Remove(iTestCurrentWord)
        Case 6
          bTestStart = False
          cTestWords.Remove(iTestCurrentWord)
        Case Else
          MsgBox("Dieser Falsche-Vokabel-Modus wird leider nicht unterstützt!")
      End Select
    End If

    sCommand = "UPDATE " & CurrentGroupName & "Stats SET LetzteAbfrage='" & sNow & "' WHERE WordNumber=" & CurrentWordNumber & ";"
    ExecuteNonQuery(sCommand)

    If Not bFirstTry Then
      sCommand = "UPDATE " & CurrentGroupName & "Stats SET Abfragen=" & iTests & ", AbfragenGesamt=" & iTestsAll & ", Richtig=" & iRight & ", Falsch=" & iWrong & ", FalschGesamt=" & iWrongAll & ", AbfrageGestartet=" & bTestStart & " WHERE WordNumber=" & CurrentWordNumber & ";"
      ExecuteNonQuery(sCommand)
      sCommand = "UPDATE " & CurrentGroupName & "Stats SET Hilfe1Richtig=" & iHelp1 & ", Hilfe2Richtig=" & iHelp2 & ", Hilfe3Richtig=" & iHelp3 & " WHERE WordNumber=" & CurrentWordNumber & ";"
      ExecuteNonQuery(sCommand)
    Else
      ' TODO AbfrageGestartet ist überflüssig
      sCommand = "UPDATE " & CurrentGroupName & "Stats SET AbfrageGestartet=" & False & ", ErsteAbfrage='" & sFirst & "' WHERE WordNumber=" & CurrentWordNumber & ";"
      ExecuteNonQuery(sCommand)
    End If

    ' Wenn das TestSystem Cards ist, müssen die Einstellungen aktualisiert werden
    If m_TestSystem = xlsTestSystem.Cards Then
      sCommand = "SELECT NextTest, NextInterval FROM " & CurrentGroupName & "Stats WHERE WordNumber=" & CurrentWordNumber & ";"
      ExecuteReader(sCommand)
      DBCursor.Read()
      Dim iNext As Integer = DBCursor.GetInt32(0)
      Dim iInterval As Integer = DBCursor.GetInt32(1)
      If Right Then
        iInterval *= 2
        iNext = iInterval
      Else
        If iInterval <> 1 Then
          iInterval /= 2
          iNext = iInterval
        End If
      End If

    End If

    m_iTestWordCountToDo = cTestWords.Count + m_iTestWordCountDoneCorrection
    m_iTestWordCountDone = m_iTestWordCountAll - m_iTestWordCountToDo
  End Sub















  Protected Function CheckWord(ByVal Meaning1 As String, ByVal Meaning2 As String, ByVal Meaning3 As String) As Boolean
    If IsConnected() = False Then Exit Function
    If m_bTestMode = False Then Exit Function
    ' Auf Test von pre/post verzichten! Dazu Grammatik-Test benutzen,
    ' evtl. nach dem Test eines Wortes die korrekte Form anzeigen
    If m_bWordToMeaning Then
      ' TODO kontrolle mit neuem meaning implementieren
      'If (Meaning1 = CurrentWord.Meaning1) And (Meaning2 = CurrentWord.Meaning2) And (Meaning3 = CurrentWord.Meaning3) Then Return True
      'If (Meaning1 = wtWord.Meaning1) And (Meaning2 = CurrentWord.Meaning3) And (Meaning3 = CurrentWord.Meaning2) Then Return True
      'If (Meaning1 = wtWord.Meaning2) And (Meaning2 = CurrentWord.Meaning1) And (Meaning3 = wtWord.Meaning3) Then Return True
      'If (Meaning1 = wtWord.Meaning2) And (Meaning2 = CurrentWord.Meaning3) And (Meaning3 = wtWord.Meaning1) Then Return True
      'If (Meaning1 = wtWord.Meaning3) And (Meaning2 = CurrentWord.Meaning2) And (Meaning3 = wtWord.Meaning1) Then Return True
      'If (Meaning1 = wtWord.Meaning3) And (Meaning2 = CurrentWord.Meaning1) And (Meaning3 = wtWord.Meaning2) Then Return True
    Else
      'If Meaning1 = wtWord.Word Then Return True
      Return False
    End If
  End Function

  Shared ReadOnly Property NextWordModesWrong() As ArrayList
    Get
      Dim aslist As New ArrayList
      aslist.Add("Fehlerhafte sofort abfragen bis korrekt")     ' 0   d
      aslist.Add("Fehlerhafte sofort erneut abfragen")    ' 1   d
      aslist.Add("Fehlerhafte abfragen bis korrekt, in Liste einfügen")    ' 2   d
      aslist.Add("Fehlerhafte erneut abfragen, in Liste einfügen")    ' 3   d
      aslist.Add("Fehlerhafte abfragen bis korrekt, neue Liste am Ende")    ' 4   ia
      aslist.Add("Fehlerhafte erneut abfragen, neue Liste am Ende")     ' 5   ia
      aslist.Add("Fehlerhafte nicht nochmal abfragen")    ' 6   d
      Return aslist
    End Get
  End Property

  Shared ReadOnly Property ExtendedModes() As ArrayList
    Get
      Dim asList As New ArrayList
      asList.Add("immer abfragen")
      asList.Add("nie abfragen")
      asList.Add("bei irregulären abfragen")
      Return asList
    End Get
  End Property

  Property NextWordModeWrong() As Integer
    Get
      Return m_iTestNextModeWrong
    End Get
    Set(ByVal Value As Integer)
      m_iTestNextModeWrong = Value
    End Set
  End Property

  Property ExtendedMode() As xlsVocTestExtended
    Get
      Return m_iExtendedTestMode
    End Get
    Set(ByVal Value As xlsVocTestExtended)
      m_iExtendedTestMode = Value
    End Set
  End Property

  Property FirstTryMode() As Boolean
    Get
      Return m_bFirstTryMode
    End Get
    Set(ByVal Value As Boolean)
      m_bFirstTryMode = Value
    End Set
  End Property

  Property RequiredOnly() As Boolean
    Get
      Return m_bRequiredOnly
    End Get
    Set(ByVal Value As Boolean)
      m_bRequiredOnly = Value
    End Set
  End Property

  Property HelpMode() As xlsVocTestHelpModes
    Get
      Return m_iHelpMode
    End Get
    Set(ByVal Value As xlsVocTestHelpModes)
      m_iHelpMode = Value
    End Set
  End Property

  Function IrregularDescription() As Collection
    Dim cList As Collection
    cList = ldfCurrentLanguage.FormNames(CurrentWord.WordType)
    Return cList
  End Function

  ReadOnly Property Types() As Collection
    Get
      Dim sList As Collection = ldfCurrentLanguage.FormList()
      Return sList
    End Get
  End Property

  ReadOnly Property TypeText(ByVal TypeNumber As Integer) As String
    Get
      If IsConnected() = False Then Return ""
      Dim sList As New Collection
      sList = Types()
      Return sList(TypeNumber + 1)
    End Get
  End Property
End Class
