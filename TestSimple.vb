Imports Gravo.localization

Public Class TestSimple
  Dim voc As xlsTestBase
  Dim db As New AccessDatabaseOperation

  Dim startVal As String

  Public Sub New(ByVal OneLanguage As Boolean, ByVal Language As String, ByRef Owner As Main)
    ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
    InitializeComponent()

    ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
    voc = New xlsTestBase
    db.Open(Application.StartupPath() & "\voc.mdb")     ' Datenbank öffnen
    voc.DBConnection = db
    If OneLanguage Then
      startVal = Language
    Else
      startVal = ""
    End If
  End Sub

  Public Sub New(ByVal GroupName As String, ByRef Owner As Main)
    ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
    InitializeComponent()

    ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
    voc = New xlsTestGroup
    db.Open(Application.StartupPath() & "\voc.mdb")     ' Datenbank öffnen
    voc.DBConnection = db
    startVal = GroupName
  End Sub

  Private Sub TestSimple_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    lblWord.Text = voc.TestWord
    lblAdditionalInfo.Text = voc.AdditionalInfo
    lblTestInformation.Text = ""
    txtInput.Text = ""
    lblCount.Text = voc.WordCount
    txtInput.Focus()
    LocalizationChanged()
  End Sub

  Public Overrides Sub LocalizationChanged()
    lblAdditionalInfoDescription.Text = GetLoc.GetText(TEST_INFO)
    lblMeaningDescription.Text = GetLoc.GetText(TEST_MEANING)
    lblTestInformationDescription.Text = GetLoc.GetText(TEST_TEST)
    cmdOK.Text = GetLoc.GetText(BUTTON_OK)
    cmdExit.Text = GetLoc.GetText(BUTTON_CLOSE)
    Me.Text = GetLoc.GetText(TEST_TITLE)
  End Sub

  Public Sub Start()
    If startVal = "" Then
      voc.Start()
    Else
      voc.Start(startVal)
    End If
    voc.NextWord()
  End Sub

  Public ReadOnly Property RestCount()
    Get
      Return voc.WordCount
    End Get
  End Property

  Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    Dim result As TestResult = voc.TestControl(txtInput.Text)
    Select Case result
      Case TestResult.NoError
        ' Richtig
        ' entfernen
        voc.NextWord()
        lblWord.Text = voc.TestWord
        lblAdditionalInfo.Text = voc.AdditionalInfo
        lblTestInformation.Text = GetLoc.GetText(TEST_CORRECT)
        txtInput.Text = ""
        txtInput.Focus()
        lblCount.Text = voc.WordCount
      Case TestResult.OtherMeaning
        ' Richtig, aber nicht die gewünschte Bedeutung
        lblTestInformation.Text = GetLoc.GetText(TEST_ANOTHER_MEANING)
        txtInput.SelectAll()
        txtInput.Focus()
      Case TestResult.Wrong
        ' Falsch
        lblTestInformation.Text = GetLoc.GetText(TEST_WRONG_HINT) & vbCrLf & voc.TestWord & " = " & voc.Answer
        MsgBox(GetLoc.GetText(TEST_WRONG), MsgBoxStyle.Information, GetLoc.GetText(TEST_ERROR))
        txtInput.Text = ""
        txtInput.Focus()
      Case TestResult.Misspelled
        lblTestInformation.Text = GetLoc.GetText(TEST_TYPE_ERROR)
    End Select
    CheckForQuit()
  End Sub

  Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
    DialogResult = Windows.Forms.DialogResult.Cancel
    Close()
  End Sub

  Private Sub CheckForQuit()
    If voc.WordCount = 0 Then
      lblWord.Text = ""
      lblTestInformation.Text = " "
      lblAdditionalInfo.Text = ""
      MsgBox(GetLoc.GetText(TEST_FINISHED), MsgBoxStyle.Information, TEST_WELL_DONE)
      voc.StopTest()
      Dim frmMain As Main = Me.Owner
      Close()
      frmMain.TestFinished()
    End If
  End Sub

  Public WriteOnly Property TestFormerLanguage() As Boolean
    Set(ByVal value As Boolean)
      voc.TestFormerLanguage = value
    End Set
  End Property

  Public WriteOnly Property UseCards() As Boolean
    Set(ByVal value As Boolean)
      voc.UseCards = value
    End Set
  End Property

  Public WriteOnly Property TestSetPhrases() As Boolean
    Set(ByVal value As Boolean)
      voc.TestSetPhrases = value
    End Set
  End Property

  Public WriteOnly Property TestMarked() As Boolean
    Set(ByVal value As Boolean)
      If TypeOf voc Is xlsTestGroup Then
        Dim voc2 As xlsTestGroup = voc
        voc2.TestMarked = value
      End If
    End Set
  End Property

  Public WriteOnly Property RandomOrder() As Boolean
    Set(ByVal value As Boolean)
      If value Then
        voc.TestStyle = xlsTestStyle.RandomTestAgain
      Else
        voc.TestStyle = xlsTestStyle.TestAgain
      End If
    End Set
  End Property
End Class