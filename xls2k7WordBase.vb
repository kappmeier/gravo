Public Class xlsWordBase
  Inherits xlsBase

  ' Diese Klasse enthält alle Word-Eigenschaften, unterstützt allerdings nicht das Speichern in
  ' die Datei aber das Laden.

  Private m_sTable As String = ""

  ' Vokabel-Speicher-Ort
  Private m_bValid As Boolean = False
  Private m_iWordNumber As Integer

  ' Vokabelinformationen
  Private m_sWord As String   'Vokabel
  Private m_sPre As String  'Pre-Vokabel    (to, le, ...)
  Private m_sPost As String   'Post-Vokabel   (Plural, slang, ...)
  Private m_sExtended1 As String    'Irregular
  Private m_sExtended2 As String    'Irregular
  Private m_sExtended3 As String    'Irregular
  Private m_aMeaning As ArrayList ' Bedeutungsliste
  Private m_sDescription As String
  Private m_bExtendedIsValid As Boolean  'Vokabel hat irreguläre Formen
  Private m_iWordType As Integer   'Vokabelart (Nomen, Verb ...)

  ' Word informatonEX
  Private m_bMustKnow As Boolean   'Vokabel muß nicht gewußt werden
  Private m_sAdditionalTargetLangInfo As String ' Beschreibung der gesuchten Vokabel

  ' Word
  Private m_iUnit As Integer
  Private m_sUnit As String
  Private m_iChapter As Integer
  Private m_iWordInUnit As Integer

  'Private m_sLastTested As String

  Sub New(ByVal db As AccessDatabaseOperation)
    MyBase.new(db)
  End Sub

  Protected Property Table() As String
    Get
      Return m_sTable
    End Get
    Set(ByVal value As String)
      m_sTable = value
    End Set
  End Property

  Public Sub AddMeaning(ByVal sWord As String)
    If MeaningList.Contains(Trim(sWord)) Then Exit Sub
    If InStr(sWord, ";") > 0 Then Exit Sub
    MeaningList.Add(Trim(sWord))
  End Sub

  Public Sub DeleteAllMeanings()
    MeaningList.Clear()
  End Sub

  Public Sub DeleteMeaning(ByVal sWord As String)
    If Not MeaningList.Contains(Trim(sWord)) Then Exit Sub
    MeaningList.Remove(Trim(sWord))
  End Sub

  Private Sub InitializeMeaning()
    m_ameaning = New ArrayList
  End Sub
  Public Sub LoadWord(ByVal WordNumber As Integer, ByVal sTable As String)
    Table = sTable
    Me.m_iWordNumber = WordNumber ' Hier wird die m_iWordNumber Variable gesetzt. Einziger Ort!
    LoadWord()
  End Sub

  Private Sub LoadWord()
    If IsConnected() = False Then Exit Sub
    If SelectedGroup() = "" Then Exit Sub
    Dim bDeleted As Boolean

    Dim sCommand As String = "SELECT Deleted FROM " & SelectedGroup() & " WHERE WordNumber=" & WordNumber & ";"
    ExecuteReader(sCommand)
    DBCursor.Read()
    If TypeOf (DBCursor.GetValue(0)) Is DBNull Then bDeleted = False Else bDeleted = DBCursor.GetValue(0)
    DBCursor.Close()
    If bDeleted Then
      Valid = False
      Exit Sub
    End If

    sCommand = "SELECT MEaning1 FROM " & SelectedGroup() & " WHERE WordNumber=" & WordNumber & ";"
    ExecuteReader(sCommand)
    DBCursor.Read()
    sCommand = DBCursor.GetValue(0)
    'm_aMeaning = New ArrayList
    InitializeMeaning()
    If Trim(sCommand) <> "" Then
      Dim aTemp As Array = Split(DBCursor.GetValue(0), ";")
      MeaningList.AddRange(aTemp)
    End If

    sCommand = "SELECT Word, Pre, Post, Description FROM " & SelectedGroup() & " WHERE WordNumber=" & WordNumber & ";"
    ExecuteReader(sCommand)
    DBCursor.Read()
    If TypeOf (DBCursor.GetValue(0)) Is DBNull Then Word = "" Else Word = DBCursor.GetValue(0)
    If TypeOf (DBCursor.GetValue(1)) Is DBNull Then Pre = "" Else Pre = DBCursor.GetValue(1)
    If TypeOf (DBCursor.GetValue(2)) Is DBNull Then Post = "" Else Post = DBCursor.GetValue(2)
    If TypeOf (DBCursor.GetValue(3)) Is DBNull Then Description = "" Else Description = DBCursor.GetValue(3)

    sCommand = "SELECT WordType, IrregularForm FROM " & SelectedGroup() & " WHERE WordNumber=" & WordNumber & ";"
    ExecuteReader(sCommand)
    DBCursor.Read()
    If TypeOf (DBCursor.GetValue(0)) Is DBNull Then WordType = 0 Else WordType = DBCursor.GetValue(0)
    If TypeOf (DBCursor.GetValue(1)) Is DBNull Then ExtendedIsValid = False Else ExtendedIsValid = DBCursor.GetBoolean(1)

    sCommand = "SELECT Irregular1, Irregular2, Irregular3 FROM " & SelectedGroup() & " WHERE WordNumber=" & WordNumber & ";"
    ExecuteReader(sCommand)
    DBCursor.Read()
    If TypeOf (DBCursor.GetValue(0)) Is DBNull Then Extended1 = "" Else Extended1 = DBCursor.GetValue(0)
    If TypeOf (DBCursor.GetValue(1)) Is DBNull Then Extended2 = "" Else Extended2 = DBCursor.GetValue(1)
    If TypeOf (DBCursor.GetValue(2)) Is DBNull Then Extended3 = "" Else Extended3 = DBCursor.GetValue(2)

    ' Word-Information-Ex
    sCommand = "SELECT MustKnow, AdditionalTargetLangInfo FROM " & SelectedGroup() & " WHERE WordNumber=" & WordNumber & ";"
    ExecuteReader(sCommand)
    DBCursor.Read()
    If TypeOf (DBCursor.GetValue(0)) Is DBNull Then MustKnow = False Else MustKnow = DBCursor.GetBoolean(0)
    If TypeOf (DBCursor.GetValue(1)) Is DBNull Then AdditionalTargetLangInfo = "" Else AdditionalTargetLangInfo = DBCursor.GetString(1)

    ' Word
    sCommand = "SELECT UnitNumber, ChapterNumber, WordInUnit FROM " & SelectedGroup() & " WHERE WordNumber=" & WordNumber & ";"
    ExecuteReader(sCommand)
    DBCursor.Read()
    If TypeOf (DBCursor.GetValue(0)) Is DBNull Then m_iUnit = -1 Else m_iUnit = DBCursor.GetValue(0)
    If TypeOf (DBCursor.GetValue(1)) Is DBNull Then m_iChapter = -1 Else m_iChapter = DBCursor.GetValue(1)
    If TypeOf (DBCursor.GetValue(2)) Is DBNull Then m_iWordInUnit = -1 Else m_iWordInUnit = DBCursor.GetValue(2)

    'If WordNumber > 1 Then sCommand = "SELECT LetzteAbfrage FROM " & SelectedGroup() & "Stats WHERE WordNumber=" & WordNumber - 1 & ";"
    'ExecuteReader(sCommand)
    'DBCursor.Read()
    'If TypeOf (dbCursor.GetValue(0)) Is DBNull Then m_sLastTested = "01.01.1900" Else 
    'LastTested = DBCursor.GetValue(0)
    'DBCursor.Close()

    sCommand = "SELECT UnitNumber, ChapterNumber, WordInUnit FROM " & SelectedGroup() & " WHERE WordNumber=" & WordNumber & ";"
    ExecuteReader(sCommand)
    DBCursor.Read()
    If TypeOf (DBCursor.GetValue(0)) Is DBNull Then m_iUnit = -1 Else m_iUnit = DBCursor.GetValue(0)
    If TypeOf (DBCursor.GetValue(1)) Is DBNull Then m_iChapter = -1 Else m_iChapter = DBCursor.GetValue(1)
    If TypeOf (DBCursor.GetValue(2)) Is DBNull Then m_iWordInUnit = -1 Else m_iWordInUnit = DBCursor.GetValue(2)
    'm_sUnit = GetUnit(m_iUnit)
    DBCursor.Close()

    'sCommand = "SELECT LetzteAbfrage FROM " & SelectedGroup() & "Stats WHERE WordNumber=" & WordNumber & ";"
    'Application.DoEvents()
    'ExecuteReader(sCommand)
    'DBCursor.Read()
    'If TypeOf (dbCursor.GetValue(0)) Is DBNull Then m_sLastTested = "01.01.1900" Else 
    'LastTested = DBCursor.GetValue(0)
    'DBCursor.Close()

    Valid = True
  End Sub

  Protected Function SelectedGroup() As String
    Return Table
  End Function

  Protected Property Valid() As Boolean
    Get
      Return m_bValid
    End Get
    Set(ByVal value As Boolean)
      m_bValid = value
    End Set
  End Property

  Public ReadOnly Property WordNumber() As Integer
    ' Die word-nummer wird nur beim laden geändert, dort wird direkt auf die m_iWordNumber Variable zugegriffen
    Get
      Return m_iWordNumber
    End Get
  End Property

  ' Wort Propertys

  Property Word() As String
    Get
      If Valid = False Then Return Nothing
      Return m_sWord
    End Get
    Set(ByVal Word As String)
      m_sWord = Word
    End Set
  End Property

  Property Pre() As String
    Get
      If Valid = False Then Return Nothing
      Return m_sPre
    End Get
    Set(ByVal Pre As String)
      m_sPre = Pre
    End Set
  End Property

  Property Post() As String
    Get
      If Valid = False Then Return Nothing
      Return m_sPost
    End Get
    Set(ByVal Post As String)
      m_sPost = Post
    End Set
  End Property

  ReadOnly Property Meaning() As Array
    Get
      Return m_aMeaning.ToArray
    End Get
  End Property

  ReadOnly Property MeaningList() As ArrayList
    Get
      Return m_aMeaning
    End Get
  End Property

  Property Extended1() As String
    Get
      If Valid = False Then Return Nothing
      Return m_sExtended1
    End Get
    Set(ByVal Irregular As String)
      m_sExtended1 = Irregular
    End Set
  End Property

  Property Extended2() As String
    Get
      If Valid = False Then Return Nothing
      Return m_sExtended2
    End Get
    Set(ByVal Irregular As String)
      m_sExtended2 = Irregular
    End Set
  End Property

  Property Extended3() As String
    Get
      If Valid = False Then Return Nothing
      Return m_sExtended3
    End Get
    Set(ByVal Irregular As String)
      m_sExtended3 = Irregular
    End Set
  End Property

  Property ExtendedIsValid() As Boolean
    Get
      If m_bValid = False Then Return Nothing
      Return m_bExtendedIsValid
    End Get
    Set(ByVal Extended As Boolean)
      m_bExtendedIsValid = Extended
    End Set
  End Property


  Property Description() As String
    Get
      If Valid = False Then Return Nothing
      Return m_sDescription
    End Get
    Set(ByVal Description As String)
      m_sDescription = Description
    End Set
  End Property

  Property WordType() As Integer
    Get
      If Valid = False Then Return Nothing
      Return m_iWordType
    End Get
    Set(ByVal Value As Integer)
      m_iWordType = Value
    End Set
  End Property

  ' wordinformationex

  Property MustKnow() As Boolean
    Get
      Return m_bMustKnow
    End Get
    Set(ByVal KnowType As Boolean)
      m_bMustKnow = KnowType
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
  ' Word
  Property UnitName() As String
    Get
      Return m_sUnit
    End Get
    Set(ByVal Unit As String)
      'If (m_bTestMode = True) Or (m_bTableSelected = False) Then Exit Property

      '' Zur neuen Unit die Number feststellen
      'Dim iNumber As Integer

      'iNumber = GetUnitNumber(Unit)
      'If iNumber <= 0 Then MsgBox("Fehler! UnitNumber zur neuen Unit ist falsch!!!")

      '' Aus alter Unit die NumberInUnit-Werte der anderen Vokabeln ändern
      '' Daten hohlen
      'Dim aTemp As New ArrayList, iWordInUnit As Integer
      'DBCommand = "SELECT WordInUnit, WordNumber FROM " & m_sTable & " WHERE UnitNumber=" & m_iUnit & ";"
      'DBCursor = DBConnection.ExecuteReader(DBCommand)
      'Do While DBCursor.Read
      '	iWordInUnit = DBCursor.GetValue(0)
      '	If iWordInUnit > m_iWordInUnit Then
      '		aTemp.Add(DBCursor.GetValue(1))					   ' Add WordNumber to Arraylist
      '		aTemp.Add(iWordInUnit)					   ' Add WordInUnit to Arraylist
      '	End If
      'Loop

      '' Daten ändern
      'Dim i As Integer
      'For i = 0 To aTemp.Count - 1 Step 2
      '	DBCommand = "UPDATE " & m_sTable & " SET WordInUnit=" & aTemp(i + 1) - 1 & " WHERE WordNumber=" & aTemp(i) & ";"
      '	DBConnection.ExecuteNonQuery(DBCommand)
      'Next i

      '' Höchste UnitInNumber feststellen
      'Dim iHighestWordInUnit As Integer = 0
      'DBCommand = "SELECT WordInUnit FROM " & m_sTable & " WHERE UnitNumber=" & iNumber & ";"
      'DBCursor = DBConnection.ExecuteReader(DBCommand)
      'Do While DBCursor.Read
      '	If DBCursor.GetValue(0) > iHighestWordInUnit Then iHighestWordInUnit = DBCursor.GetValue(0)
      'Loop

      '' Daten der alten Vokabel ändern
      'DBCommand = "UPDATE " & m_sTable & " SET UnitNumber=" & iNumber & ", WordInUnit=" & iHighestWordInUnit + 1 & " WHERE WordNumber=" & WordNumber & ";"
      'DBConnection.ExecuteNonQuery(DBCommand)

      '' Membervariable ändern
      'm_sUnit = Unit
      'm_iUnit = iNumber
    End Set
  End Property

  Property UnitNumber() As Integer
    Get
      Return m_iUnit
    End Get
    Set(ByVal Unit As Integer)

    End Set
  End Property

  Property Chapter() As Integer
    Get
      Return m_iChapter
    End Get
    Set(ByVal Chapter As Integer)
      m_iChapter = Chapter
    End Set
  End Property

  ReadOnly Property WordInUnit() As Integer
    Get
      Return m_iWordInUnit
    End Get
  End Property
End Class
