Public Class TestSimple
  Dim voc As xlsTestBase
  Dim db As New AccessDatabaseOperation

  Public Sub New(ByVal OneLanguage As Boolean, ByVal Language As String)
    ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
    InitializeComponent()

    ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
    ' laden einer Sprache, zur zeit nur italienisch
    voc = New xlsTestBase
    db.Open(Application.StartupPath() & "\voc.mdb")     ' Datenbank öffnen
    voc.DBConnection = db
    If OneLanguage Then
      voc.Start(Language)
    Else
      voc.Start() ' Language wird ignoriert
    End If
    voc.NextWord()
    If voc.WordCount = 0 Then voc.StopTest() : Close()
  End Sub

  Public Sub New(ByVal GroupName As String)
    ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
    InitializeComponent()

    ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
    voc = New xlsTestGroup
    db.Open(Application.StartupPath() & "\voc.mdb")     ' Datenbank öffnen
    voc.DBConnection = db
    voc.Start(GroupName)
    voc.NextWord()
    If voc.WordCount = 0 Then voc.StopTest() : Close()
  End Sub

  Private Sub TestSimple_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ' Position
    Me.Left = Me.Owner.Left + Me.Owner.Width / 2 - Me.Width / 2
    Me.Top = Me.Owner.Top + Me.Owner.Height / 2 - Me.Height / 2
    If Me.Top < 0 Then Me.Top = 0
    If Me.Left < 0 Then Me.Left = 0

    If voc.WordCount = 0 Then Me.Close() : Exit Sub
    lblWord.Text = voc.TestWord
    lblAdditionalInfo.Text = ""
    lblTestInformation.Text = ""
    txtInput.Text = ""
    lblCount.Text = voc.WordCount
    txtInput.Focus()
  End Sub

  Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    Dim result As TestResult = voc.TestControl(txtInput.Text)
    Select Case result
      Case TestResult.NoError
        ' Richtig
        ' entfernen
        voc.NextWord()
        lblWord.Text = voc.TestWord
        lblAdditionalInfo.Text = voc.AdditionalInfo
        lblTestInformation.Text = "Richtig!"
        txtInput.Text = ""
        txtInput.Focus()
        lblCount.Text = voc.WordCount
      Case TestResult.OtherMeaning
        ' Richtig, aber nicht die gewünschte Bedeutung
        lblTestInformation.Text = "Bitte geben sie eine weitere Bedeutung ein."
        txtInput.SelectAll()
        txtInput.Focus()
      Case TestResult.Wrong
        ' Falsch
        lblTestInformation.Text = "Richtig wäre gewesen:" & vbCrLf & voc.TestWord & " = " & voc.Answer
        MsgBox("Leider falsch", MsgBoxStyle.Information, "Fehler")
        txtInput.Text = ""
        txtInput.Focus()
      Case TestResult.Misspelled
        lblTestInformation.Text = "Es liegt ein Rechtschreibfehler vor."
    End Select
    CheckForQuit()
  End Sub

  Public Sub CheckForQuit()
    If voc.WordCount = 0 Then
      lblWord.Text = ""
      lblTestInformation.Text = " "
      lblAdditionalInfo.Text = ""
      MsgBox("Alle Vokabeln abgefragt.", MsgBoxStyle.Information, "Gut gemacht!")
      voc.StopTest()
      Close()
    End If
  End Sub
End Class