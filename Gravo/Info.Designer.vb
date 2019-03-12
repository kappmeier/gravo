<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Info
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
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.chkGermanText = New System.Windows.Forms.CheckBox()
        Me.lblDisclaimer = New System.Windows.Forms.Label()
        Me.lblProductName = New System.Windows.Forms.Label()
        Me.lblCopyrightOld = New System.Windows.Forms.Label()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.lblCopyrightName = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblDBVersion = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Location = New System.Drawing.Point(249, 198)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "Schließen"
        '
        'lblCopyright
        '
        Me.lblCopyright.AutoSize = True
        Me.lblCopyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyright.Location = New System.Drawing.Point(165, 6)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(13, 13)
        Me.lblCopyright.TabIndex = 19
        Me.lblCopyright.Text = "#"
        Me.lblCopyright.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'chkGermanText
        '
        Me.chkGermanText.Location = New System.Drawing.Point(4, 198)
        Me.chkGermanText.Name = "chkGermanText"
        Me.chkGermanText.Size = New System.Drawing.Size(104, 24)
        Me.chkGermanText.TabIndex = 1
        Me.chkGermanText.Text = "Deutsch"
        '
        'lblDisclaimer
        '
        Me.lblDisclaimer.Location = New System.Drawing.Point(4, 76)
        Me.lblDisclaimer.Name = "lblDisclaimer"
        Me.lblDisclaimer.Size = New System.Drawing.Size(320, 119)
        Me.lblDisclaimer.TabIndex = 17
        Me.lblDisclaimer.Text = "#"
        '
        'lblProductName
        '
        Me.lblProductName.AutoSize = True
        Me.lblProductName.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductName.Location = New System.Drawing.Point(4, 23)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(26, 29)
        Me.lblProductName.TabIndex = 14
        Me.lblProductName.Text = "#"
        '
        'lblCopyrightOld
        '
        Me.lblCopyrightOld.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.lblCopyrightOld.Location = New System.Drawing.Point(102, 236)
        Me.lblCopyrightOld.Name = "lblCopyrightOld"
        Me.lblCopyrightOld.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblCopyrightOld.Size = New System.Drawing.Size(224, 12)
        Me.lblCopyrightOld.TabIndex = 12
        Me.lblCopyrightOld.Text = "#"
        Me.lblCopyrightOld.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.LinkLabel2.Location = New System.Drawing.Point(165, 38)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(129, 13)
        Me.LinkLabel2.TabIndex = 0
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "http://www.kappmeier.de"
        Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCopyrightName
        '
        Me.lblCopyrightName.AutoSize = True
        Me.lblCopyrightName.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyrightName.Location = New System.Drawing.Point(165, 22)
        Me.lblCopyrightName.Name = "lblCopyrightName"
        Me.lblCopyrightName.Size = New System.Drawing.Size(13, 13)
        Me.lblCopyrightName.TabIndex = 21
        Me.lblCopyrightName.Text = "#"
        Me.lblCopyrightName.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.lblVersion.Location = New System.Drawing.Point(2, 224)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(10, 12)
        Me.lblVersion.TabIndex = 22
        Me.lblVersion.Text = "#"
        '
        'lblDBVersion
        '
        Me.lblDBVersion.AutoSize = True
        Me.lblDBVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.lblDBVersion.Location = New System.Drawing.Point(2, 236)
        Me.lblDBVersion.Name = "lblDBVersion"
        Me.lblDBVersion.Size = New System.Drawing.Size(10, 12)
        Me.lblDBVersion.TabIndex = 23
        Me.lblDBVersion.Text = "#"
        '
        'Info
        '
        Me.AcceptButton = Me.cmdClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(328, 250)
        Me.Controls.Add(Me.lblDBVersion)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblCopyrightName)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.lblCopyright)
        Me.Controls.Add(Me.chkGermanText)
        Me.Controls.Add(Me.lblDisclaimer)
        Me.Controls.Add(Me.lblProductName)
        Me.Controls.Add(Me.lblCopyrightOld)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Info"
        Me.Text = "#"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents chkGermanText As System.Windows.Forms.CheckBox
    Friend WithEvents lblDisclaimer As System.Windows.Forms.Label
    Friend WithEvents lblProductName As System.Windows.Forms.Label
    Friend WithEvents lblCopyrightOld As System.Windows.Forms.Label
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents lblCopyrightName As System.Windows.Forms.Label
  Friend WithEvents lblVersion As System.Windows.Forms.Label
  Friend WithEvents lblDBVersion As System.Windows.Forms.Label
End Class
