<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Info
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
        Me.cmdClose = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.chkGermanText = New System.Windows.Forms.CheckBox
        Me.lblDisclaimer = New System.Windows.Forms.Label
        Me.lblCompany = New System.Windows.Forms.Label
        Me.lblProductName = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.Label2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Location = New System.Drawing.Point(249, 198)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 20
        Me.cmdClose.Text = "Schließen"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(165, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(105, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "©opyleft 1995 - 2007"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'chkGermanText
        '
        Me.chkGermanText.Location = New System.Drawing.Point(4, 198)
        Me.chkGermanText.Name = "chkGermanText"
        Me.chkGermanText.Size = New System.Drawing.Size(104, 24)
        Me.chkGermanText.TabIndex = 18
        Me.chkGermanText.Text = "Deutsch"
        '
        'lblDisclaimer
        '
        Me.lblDisclaimer.Location = New System.Drawing.Point(4, 102)
        Me.lblDisclaimer.Name = "lblDisclaimer"
        Me.lblDisclaimer.Size = New System.Drawing.Size(320, 96)
        Me.lblDisclaimer.TabIndex = 17
        Me.lblDisclaimer.Text = "#"
        '
        'lblCompany
        '
        Me.lblCompany.AutoSize = True
        Me.lblCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany.Location = New System.Drawing.Point(4, 6)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(26, 29)
        Me.lblCompany.TabIndex = 15
        Me.lblCompany.Text = "#"
        '
        'lblProductName
        '
        Me.lblProductName.AutoSize = True
        Me.lblProductName.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductName.Location = New System.Drawing.Point(4, 38)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(26, 29)
        Me.lblProductName.TabIndex = 14
        Me.lblProductName.Text = "#"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label1.Location = New System.Drawing.Point(127, 225)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(195, 12)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "basiert auf Vokabeltrainer, ©opyleft 1995-2007"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.LinkLabel2.Location = New System.Drawing.Point(165, 38)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(129, 13)
        Me.LinkLabel2.TabIndex = 13
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "http://www.kappmeier.de"
        Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(165, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Jan-Philipp Kappmeier"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Info
        '
        Me.AcceptButton = Me.cmdClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(328, 240)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.chkGermanText)
        Me.Controls.Add(Me.lblDisclaimer)
        Me.Controls.Add(Me.lblCompany)
        Me.Controls.Add(Me.lblProductName)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Info"
        Me.Text = "#"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents chkGermanText As System.Windows.Forms.CheckBox
  Friend WithEvents lblDisclaimer As System.Windows.Forms.Label
  Friend WithEvents lblCompany As System.Windows.Forms.Label
  Friend WithEvents lblProductName As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
  Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
