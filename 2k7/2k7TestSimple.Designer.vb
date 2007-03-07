<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestSimple
  Inherits System.Windows.Forms.Form

  'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Wird vom Windows Form-Designer benötigt.
  Private components As System.ComponentModel.IContainer

  'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
  'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
  'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.txtInput = New System.Windows.Forms.TextBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.cmdOK = New System.Windows.Forms.Button
    Me.cmdExit = New System.Windows.Forms.Button
    Me.lblWord = New System.Windows.Forms.Label
    Me.lblTestInformation = New System.Windows.Forms.Label
    Me.Label4 = New System.Windows.Forms.Label
    Me.lblAdditionalInfo = New System.Windows.Forms.Label
    Me.Label1 = New System.Windows.Forms.Label
    Me.lblCount = New System.Windows.Forms.Label
    Me.SuspendLayout()
    '
    'txtInput
    '
    Me.txtInput.Location = New System.Drawing.Point(92, 107)
    Me.txtInput.Name = "txtInput"
    Me.txtInput.Size = New System.Drawing.Size(320, 20)
    Me.txtInput.TabIndex = 1
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 107)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(62, 13)
    Me.Label2.TabIndex = 2
    Me.Label2.Text = "Bedeutung:"
    '
    'cmdOK
    '
    Me.cmdOK.Location = New System.Drawing.Point(337, 133)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(75, 23)
    Me.cmdOK.TabIndex = 3
    Me.cmdOK.Text = "OK"
    Me.cmdOK.UseVisualStyleBackColor = True
    '
    'cmdExit
    '
    Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdExit.Location = New System.Drawing.Point(256, 133)
    Me.cmdExit.Name = "cmdExit"
    Me.cmdExit.Size = New System.Drawing.Size(75, 23)
    Me.cmdExit.TabIndex = 4
    Me.cmdExit.Text = "Schließen"
    Me.cmdExit.UseVisualStyleBackColor = True
    '
    'lblWord
    '
    Me.lblWord.BackColor = System.Drawing.SystemColors.Control
    Me.lblWord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblWord.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.lblWord.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold)
    Me.lblWord.Location = New System.Drawing.Point(12, 9)
    Me.lblWord.Name = "lblWord"
    Me.lblWord.Size = New System.Drawing.Size(400, 40)
    Me.lblWord.TabIndex = 5
    Me.lblWord.Text = "lblWord"
    Me.lblWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblTestInformation
    '
    Me.lblTestInformation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblTestInformation.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.lblTestInformation.Location = New System.Drawing.Point(92, 72)
    Me.lblTestInformation.Name = "lblTestInformation"
    Me.lblTestInformation.Size = New System.Drawing.Size(320, 32)
    Me.lblTestInformation.TabIndex = 34
    Me.lblTestInformation.Text = "lblTestInformation"
    '
    'Label4
    '
    Me.Label4.Location = New System.Drawing.Point(12, 73)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(80, 16)
    Me.Label4.TabIndex = 33
    Me.Label4.Text = "Abfrage:"
    '
    'lblAdditionalInfo
    '
    Me.lblAdditionalInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblAdditionalInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.lblAdditionalInfo.Location = New System.Drawing.Point(92, 56)
    Me.lblAdditionalInfo.Name = "lblAdditionalInfo"
    Me.lblAdditionalInfo.Size = New System.Drawing.Size(320, 16)
    Me.lblAdditionalInfo.TabIndex = 31
    Me.lblAdditionalInfo.Text = "lblAdditionalInfo"
    '
    'Label1
    '
    Me.Label1.Location = New System.Drawing.Point(12, 56)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(80, 16)
    Me.Label1.TabIndex = 32
    Me.Label1.Text = "Zusatz:"
    '
    'lblCount
    '
    Me.lblCount.AutoSize = True
    Me.lblCount.Location = New System.Drawing.Point(12, 138)
    Me.lblCount.Name = "lblCount"
    Me.lblCount.Size = New System.Drawing.Size(14, 13)
    Me.lblCount.TabIndex = 35
    Me.lblCount.Text = "#"
    '
    'TestSimple
    '
    Me.AcceptButton = Me.cmdOK
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdExit
    Me.ClientSize = New System.Drawing.Size(420, 164)
    Me.Controls.Add(Me.lblCount)
    Me.Controls.Add(Me.lblTestInformation)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.lblAdditionalInfo)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.lblWord)
    Me.Controls.Add(Me.cmdExit)
    Me.Controls.Add(Me.cmdOK)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.txtInput)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "TestSimple"
    Me.ShowInTaskbar = False
    Me.Text = "Test"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtInput As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents cmdOK As System.Windows.Forms.Button
  Friend WithEvents cmdExit As System.Windows.Forms.Button
  Friend WithEvents lblWord As System.Windows.Forms.Label
  Friend WithEvents lblTestInformation As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents lblAdditionalInfo As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents lblCount As System.Windows.Forms.Label
End Class
