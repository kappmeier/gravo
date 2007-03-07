<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestSelect
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
    Me.cmdOK = New System.Windows.Forms.Button
    Me.cmdCancel = New System.Windows.Forms.Button
    Me.cmbGroup = New System.Windows.Forms.ComboBox
    Me.cmbSubGroup = New System.Windows.Forms.ComboBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.lblWordCount = New System.Windows.Forms.Label
    Me.Label3 = New System.Windows.Forms.Label
    Me.SuspendLayout()
    '
    'cmdOK
    '
    Me.cmdOK.Location = New System.Drawing.Point(86, 105)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(75, 23)
    Me.cmdOK.TabIndex = 0
    Me.cmdOK.Text = "OK"
    Me.cmdOK.UseVisualStyleBackColor = True
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Location = New System.Drawing.Point(167, 105)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
    Me.cmdCancel.TabIndex = 1
    Me.cmdCancel.Text = "Abbrechen"
    Me.cmdCancel.UseVisualStyleBackColor = True
    '
    'cmbGroup
    '
    Me.cmbGroup.FormattingEnabled = True
    Me.cmbGroup.Location = New System.Drawing.Point(12, 25)
    Me.cmbGroup.Name = "cmbGroup"
    Me.cmbGroup.Size = New System.Drawing.Size(232, 21)
    Me.cmbGroup.TabIndex = 2
    '
    'cmbSubGroup
    '
    Me.cmbSubGroup.FormattingEnabled = True
    Me.cmbSubGroup.Location = New System.Drawing.Point(12, 65)
    Me.cmbSubGroup.Name = "cmbSubGroup"
    Me.cmbSubGroup.Size = New System.Drawing.Size(232, 21)
    Me.cmbSubGroup.TabIndex = 3
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 9)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(45, 13)
    Me.Label1.TabIndex = 4
    Me.Label1.Text = "Gruppe:"
    '
    'lblWordCount
    '
    Me.lblWordCount.AutoSize = True
    Me.lblWordCount.Location = New System.Drawing.Point(12, 89)
    Me.lblWordCount.Name = "lblWordCount"
    Me.lblWordCount.Size = New System.Drawing.Size(14, 13)
    Me.lblWordCount.TabIndex = 5
    Me.lblWordCount.Text = "#"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(12, 49)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(69, 13)
    Me.Label3.TabIndex = 6
    Me.Label3.Text = "Untergruppe:"
    '
    'TestSelect
    '
    Me.AcceptButton = Me.cmdOK
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(254, 137)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.lblWordCount)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.cmbSubGroup)
    Me.Controls.Add(Me.cmbGroup)
    Me.Controls.Add(Me.cmdCancel)
    Me.Controls.Add(Me.cmdOK)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "TestSelect"
    Me.Text = "Testgruppe auswählen..."
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cmdOK As System.Windows.Forms.Button
  Friend WithEvents cmdCancel As System.Windows.Forms.Button
  Friend WithEvents cmbGroup As System.Windows.Forms.ComboBox
  Friend WithEvents cmbSubGroup As System.Windows.Forms.ComboBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents lblWordCount As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
