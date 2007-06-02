<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestSimple
  Inherits MyForm

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
    Me.lblMeaningDescription = New System.Windows.Forms.Label
    Me.lblWord = New System.Windows.Forms.Label
    Me.lblTestInformation = New System.Windows.Forms.Label
    Me.lblTestInformationDescription = New System.Windows.Forms.Label
    Me.lblAdditionalInfo = New System.Windows.Forms.Label
    Me.lblAdditionalInfoDescription = New System.Windows.Forms.Label
    Me.lblCount = New System.Windows.Forms.Label
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
    Me.cmdOK = New System.Windows.Forms.Button
    Me.cmdExit = New System.Windows.Forms.Button
    Me.TableLayoutPanel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'txtInput
    '
    Me.txtInput.Location = New System.Drawing.Point(92, 123)
    Me.txtInput.Name = "txtInput"
    Me.txtInput.Size = New System.Drawing.Size(320, 20)
    Me.txtInput.TabIndex = 0
    '
    'lblMeaningDescription
    '
    Me.lblMeaningDescription.AutoSize = True
    Me.lblMeaningDescription.Location = New System.Drawing.Point(12, 126)
    Me.lblMeaningDescription.Name = "lblMeaningDescription"
    Me.lblMeaningDescription.Size = New System.Drawing.Size(62, 13)
    Me.lblMeaningDescription.TabIndex = 2
    Me.lblMeaningDescription.Text = "Bedeutung:"
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
    Me.lblTestInformation.Size = New System.Drawing.Size(320, 48)
    Me.lblTestInformation.TabIndex = 34
    Me.lblTestInformation.Text = "lblTestInformation"
    '
    'lblTestInformationDescription
    '
    Me.lblTestInformationDescription.Location = New System.Drawing.Point(12, 73)
    Me.lblTestInformationDescription.Name = "lblTestInformationDescription"
    Me.lblTestInformationDescription.Size = New System.Drawing.Size(80, 16)
    Me.lblTestInformationDescription.TabIndex = 33
    Me.lblTestInformationDescription.Text = "Abfrage:"
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
    'lblAdditionalInfoDescription
    '
    Me.lblAdditionalInfoDescription.Location = New System.Drawing.Point(12, 56)
    Me.lblAdditionalInfoDescription.Name = "lblAdditionalInfoDescription"
    Me.lblAdditionalInfoDescription.Size = New System.Drawing.Size(80, 16)
    Me.lblAdditionalInfoDescription.TabIndex = 32
    Me.lblAdditionalInfoDescription.Text = "Zusatz:"
    '
    'lblCount
    '
    Me.lblCount.AutoSize = True
    Me.lblCount.Location = New System.Drawing.Point(12, 156)
    Me.lblCount.Name = "lblCount"
    Me.lblCount.Size = New System.Drawing.Size(14, 13)
    Me.lblCount.TabIndex = 35
    Me.lblCount.Text = "#"
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.cmdOK, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.cmdExit, 1, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(269, 148)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 36
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.cmdOK.Location = New System.Drawing.Point(3, 3)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(67, 23)
    Me.cmdOK.TabIndex = 5
    Me.cmdOK.Text = "OK"
    '
    'cmdExit
    '
    Me.cmdExit.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdExit.Location = New System.Drawing.Point(76, 3)
    Me.cmdExit.Name = "cmdExit"
    Me.cmdExit.Size = New System.Drawing.Size(67, 23)
    Me.cmdExit.TabIndex = 6
    Me.cmdExit.Text = "Schließen"
    '
    'TestSimple
    '
    Me.AcceptButton = Me.cmdOK
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdExit
    Me.ClientSize = New System.Drawing.Size(427, 189)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.Controls.Add(Me.lblCount)
    Me.Controls.Add(Me.lblTestInformation)
    Me.Controls.Add(Me.lblTestInformationDescription)
    Me.Controls.Add(Me.lblAdditionalInfo)
    Me.Controls.Add(Me.lblAdditionalInfoDescription)
    Me.Controls.Add(Me.lblWord)
    Me.Controls.Add(Me.lblMeaningDescription)
    Me.Controls.Add(Me.txtInput)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "TestSimple"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Test"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtInput As System.Windows.Forms.TextBox
  Friend WithEvents lblMeaningDescription As System.Windows.Forms.Label
  Friend WithEvents lblWord As System.Windows.Forms.Label
  Friend WithEvents lblTestInformation As System.Windows.Forms.Label
  Friend WithEvents lblTestInformationDescription As System.Windows.Forms.Label
  Friend WithEvents lblAdditionalInfo As System.Windows.Forms.Label
  Friend WithEvents lblAdditionalInfoDescription As System.Windows.Forms.Label
  Friend WithEvents lblCount As System.Windows.Forms.Label
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents cmdOK As System.Windows.Forms.Button
  Friend WithEvents cmdExit As System.Windows.Forms.Button
End Class
