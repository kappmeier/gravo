Public Class TestSimple

  Dim voc As xlsTestBase
  Dim db As New AccessDatabaseOperation

  Private Sub TestSimple_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    lblWord.Text = voc.TestWord
    lblAdditionalInfo.Text = ""
    lblTestInformation.Text = ""
    txtInput.Text = ""
    txtInput.Focus()
  End Sub

  Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    Dim iResult As Integer = voc.TestControl(txtInput.Text)
    Select Case iResult
      Case 0
        ' Richtig
        ' entfernen
        'voc.DeleteWord() wird schon in der funktion durchgeführt!
        voc.NextWord()
        lblWord.Text = voc.TestWord
        lblAdditionalInfo.Text = ""
        lblTestInformation.Text = "Richtig!"
        txtInput.Text = ""
        txtInput.Focus()
      Case 1
        ' Richtig, aber nicht die gewünschte Bedeutung
        lblTestInformation.Text = "Bitte geben sie eine weitere Bedeutung ein."
        txtInput.SelectAll()
        txtInput.Focus()
      Case 2
        ' Falsch
        MsgBox("Leider falsch", MsgBoxStyle.Information, "Fehler")
        lblTestInformation.Text = "Richtig wäre gewesen:" & vbCrLf & voc.TestWord & " = " & voc.Answer
        txtInput.Text = ""
        txtInput.Focus()
    End Select
  End Sub

  Public Sub New()
    ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
    InitializeComponent()

    ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
    ' laden einer Sprache, zur zeit nur italienisch
    voc = New xlsTestBase
    db.Open(Application.StartupPath() & "\voc.mdb")     ' Datenbank öffnen
    voc.DBConnection = db
    voc.Start("italian")
    voc.NextWord()
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
    'SetGroup(GroupName)
  End Sub
End Class