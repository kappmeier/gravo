Public Class TestSimple

  Dim voc As New xlsTestBase
  Dim db As New AccessDatabaseOperation

  Private Sub TestSimple_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ' laden einer Sprache, zur zeit nur italienisch
    db.Open(Application.StartupPath() & "\voc.mdb")     ' Datenbank öffnen
    voc.DBConnection = db
    voc.Start("italian")
    voc.NextWord()
    Me.lblWord.Text = voc.TestWord
    Me.lblAdditionalInfo.Text = ""
    Me.lblTestInformation.Text = ""
    Me.txtInput.Text = ""
    Me.txtInput.Focus()
  End Sub

  Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    Dim iResult As Integer = voc.TestControl(Me.txtInput.Text)
    Select Case iResult
      Case 0
        ' Richtig
        ' entfernen
        'voc.DeleteWord() wird schon in der funktion durchgeführt!
        voc.NextWord()
        Me.lblWord.Text = voc.TestWord
        Me.lblAdditionalInfo.Text = ""
        Me.lblTestInformation.Text = "Richtig!"
        Me.txtInput.Text = ""
        Me.txtInput.Focus()
      Case 1
        ' Richtig, aber nicht die gewünschte Bedeutung
        Me.lblTestInformation.Text = "Bitte geben sie eine weitere Bedeutung ein."
        Me.txtInput.SelectAll()
        Me.txtInput.Focus()
      Case 2
        ' Falsch
        MsgBox("Leider falsch", MsgBoxStyle.Information, "Fehler")
        Me.lblTestInformation.Text = "Richtig wäre gewesen:" & vbCrLf & voc.TestWord & " = " & voc.Answer
        Me.txtInput.Text = ""
        Me.txtInput.Focus()
    End Select
  End Sub
End Class