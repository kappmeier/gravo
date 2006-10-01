<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WordInput
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
    Me.txtWord = New System.Windows.Forms.TextBox
    Me.txtMainEntry = New System.Windows.Forms.TextBox
    Me.cmdAddSubEntry = New System.Windows.Forms.Button
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.Label9 = New System.Windows.Forms.Label
    Me.Label8 = New System.Windows.Forms.Label
    Me.Label6 = New System.Windows.Forms.Label
    Me.Label5 = New System.Windows.Forms.Label
    Me.Label4 = New System.Windows.Forms.Label
    Me.txtAdditionalTargetlanguageInfo = New System.Windows.Forms.TextBox
    Me.lstWordTypes = New System.Windows.Forms.ListBox
    Me.txtPost = New System.Windows.Forms.TextBox
    Me.txtPre = New System.Windows.Forms.TextBox
    Me.txtMeaning = New System.Windows.Forms.TextBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.Label1 = New System.Windows.Forms.Label
    Me.GroupBox2 = New System.Windows.Forms.GroupBox
    Me.Label3 = New System.Windows.Forms.Label
    Me.cmdAddEntry = New System.Windows.Forms.Button
    Me.txtAddEntry = New System.Windows.Forms.TextBox
    Me.Button3 = New System.Windows.Forms.Button
    Me.cmbLanguages = New System.Windows.Forms.ComboBox
    Me.cmbXLSTypes = New System.Windows.Forms.ComboBox
    Me.GroupBox1.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    Me.SuspendLayout()
    '
    'txtWord
    '
    Me.txtWord.Location = New System.Drawing.Point(87, 68)
    Me.txtWord.Name = "txtWord"
    Me.txtWord.Size = New System.Drawing.Size(120, 20)
    Me.txtWord.TabIndex = 7
    '
    'txtMainEntry
    '
    Me.txtMainEntry.Location = New System.Drawing.Point(87, 16)
    Me.txtMainEntry.Name = "txtMainEntry"
    Me.txtMainEntry.Size = New System.Drawing.Size(120, 20)
    Me.txtMainEntry.TabIndex = 5
    '
    'cmdAddSubEntry
    '
    Me.cmdAddSubEntry.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.cmdAddSubEntry.Location = New System.Drawing.Point(6, 244)
    Me.cmdAddSubEntry.Name = "cmdAddSubEntry"
    Me.cmdAddSubEntry.Size = New System.Drawing.Size(75, 23)
    Me.cmdAddSubEntry.TabIndex = 13
    Me.cmdAddSubEntry.Text = "Hinzufügen"
    Me.cmdAddSubEntry.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.Label9)
    Me.GroupBox1.Controls.Add(Me.Label8)
    Me.GroupBox1.Controls.Add(Me.Label6)
    Me.GroupBox1.Controls.Add(Me.Label5)
    Me.GroupBox1.Controls.Add(Me.Label4)
    Me.GroupBox1.Controls.Add(Me.txtAdditionalTargetlanguageInfo)
    Me.GroupBox1.Controls.Add(Me.lstWordTypes)
    Me.GroupBox1.Controls.Add(Me.txtPost)
    Me.GroupBox1.Controls.Add(Me.txtPre)
    Me.GroupBox1.Controls.Add(Me.txtMeaning)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.cmdAddSubEntry)
    Me.GroupBox1.Controls.Add(Me.txtWord)
    Me.GroupBox1.Controls.Add(Me.txtMainEntry)
    Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.GroupBox1.Location = New System.Drawing.Point(266, 12)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(246, 280)
    Me.GroupBox1.TabIndex = 3
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Untereintrag hinzufügen:"
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Location = New System.Drawing.Point(9, 172)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(47, 13)
    Me.Label9.TabIndex = 16
    Me.Label9.Text = "Worttyp:"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(9, 146)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(66, 13)
    Me.Label8.TabIndex = 15
    Me.Label8.Text = "Vokabelinfo:"
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(9, 120)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(62, 13)
    Me.Label6.TabIndex = 13
    Me.Label6.Text = "Bedeutung:"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(9, 94)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(31, 13)
    Me.Label5.TabIndex = 12
    Me.Label5.Text = "Post:"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(9, 42)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(26, 13)
    Me.Label4.TabIndex = 11
    Me.Label4.Text = "Pre:"
    '
    'txtAdditionalTargetlanguageInfo
    '
    Me.txtAdditionalTargetlanguageInfo.Location = New System.Drawing.Point(87, 146)
    Me.txtAdditionalTargetlanguageInfo.Name = "txtAdditionalTargetlanguageInfo"
    Me.txtAdditionalTargetlanguageInfo.Size = New System.Drawing.Size(120, 20)
    Me.txtAdditionalTargetlanguageInfo.TabIndex = 11
    '
    'lstWordTypes
    '
    Me.lstWordTypes.FormattingEnabled = True
    Me.lstWordTypes.Location = New System.Drawing.Point(87, 172)
    Me.lstWordTypes.Name = "lstWordTypes"
    Me.lstWordTypes.Size = New System.Drawing.Size(120, 95)
    Me.lstWordTypes.TabIndex = 12
    '
    'txtPost
    '
    Me.txtPost.Location = New System.Drawing.Point(87, 94)
    Me.txtPost.Name = "txtPost"
    Me.txtPost.Size = New System.Drawing.Size(120, 20)
    Me.txtPost.TabIndex = 8
    '
    'txtPre
    '
    Me.txtPre.Location = New System.Drawing.Point(87, 42)
    Me.txtPre.Name = "txtPre"
    Me.txtPre.Size = New System.Drawing.Size(120, 20)
    Me.txtPre.TabIndex = 6
    '
    'txtMeaning
    '
    Me.txtMeaning.Location = New System.Drawing.Point(87, 120)
    Me.txtMeaning.Name = "txtMeaning"
    Me.txtMeaning.Size = New System.Drawing.Size(120, 20)
    Me.txtMeaning.TabIndex = 9
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(6, 16)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(71, 13)
    Me.Label2.TabIndex = 4
    Me.Label2.Text = "Haupteintrag:"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(9, 68)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(68, 13)
    Me.Label1.TabIndex = 3
    Me.Label1.Text = "Untereintrag:"
    '
    'GroupBox2
    '
    Me.GroupBox2.Controls.Add(Me.Label3)
    Me.GroupBox2.Controls.Add(Me.cmdAddEntry)
    Me.GroupBox2.Controls.Add(Me.txtAddEntry)
    Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.GroupBox2.Location = New System.Drawing.Point(12, 39)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(246, 79)
    Me.GroupBox2.TabIndex = 0
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Haupteintrag hinzufügen:"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(6, 19)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(71, 13)
    Me.Label3.TabIndex = 2
    Me.Label3.Text = "Haupteintrag:"
    '
    'cmdAddEntry
    '
    Me.cmdAddEntry.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.cmdAddEntry.Location = New System.Drawing.Point(87, 45)
    Me.cmdAddEntry.Name = "cmdAddEntry"
    Me.cmdAddEntry.Size = New System.Drawing.Size(75, 23)
    Me.cmdAddEntry.TabIndex = 4
    Me.cmdAddEntry.Text = "Hinzufügen"
    Me.cmdAddEntry.UseVisualStyleBackColor = True
    '
    'txtAddEntry
    '
    Me.txtAddEntry.Location = New System.Drawing.Point(87, 19)
    Me.txtAddEntry.Name = "txtAddEntry"
    Me.txtAddEntry.Size = New System.Drawing.Size(120, 20)
    Me.txtAddEntry.TabIndex = 3
    '
    'Button3
    '
    Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.Button3.Location = New System.Drawing.Point(12, 269)
    Me.Button3.Name = "Button3"
    Me.Button3.Size = New System.Drawing.Size(75, 23)
    Me.Button3.TabIndex = 14
    Me.Button3.Text = "Schließen"
    Me.Button3.UseVisualStyleBackColor = True
    '
    'cmbLanguages
    '
    Me.cmbLanguages.FormattingEnabled = True
    Me.cmbLanguages.Items.AddRange(New Object() {"italian", "english", "latin", "french"})
    Me.cmbLanguages.Location = New System.Drawing.Point(12, 12)
    Me.cmbLanguages.Name = "cmbLanguages"
    Me.cmbLanguages.Size = New System.Drawing.Size(121, 21)
    Me.cmbLanguages.TabIndex = 1
    '
    'cmbXLSTypes
    '
    Me.cmbXLSTypes.FormattingEnabled = True
        Me.cmbXLSTypes.Items.AddRange(New Object() {"std"})
    Me.cmbXLSTypes.Location = New System.Drawing.Point(139, 12)
    Me.cmbXLSTypes.Name = "cmbXLSTypes"
    Me.cmbXLSTypes.Size = New System.Drawing.Size(121, 21)
    Me.cmbXLSTypes.Sorted = True
    Me.cmbXLSTypes.TabIndex = 2
    '
    'WordInput
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(523, 304)
    Me.Controls.Add(Me.cmbXLSTypes)
    Me.Controls.Add(Me.cmbLanguages)
    Me.Controls.Add(Me.Button3)
    Me.Controls.Add(Me.GroupBox2)
    Me.Controls.Add(Me.GroupBox1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.Name = "WordInput"
    Me.ShowInTaskbar = False
    Me.Text = "Wörter hinzufügen"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.GroupBox2.ResumeLayout(False)
    Me.GroupBox2.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents txtWord As System.Windows.Forms.TextBox
  Friend WithEvents txtMainEntry As System.Windows.Forms.TextBox
  Friend WithEvents cmdAddSubEntry As System.Windows.Forms.Button
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents cmdAddEntry As System.Windows.Forms.Button
  Friend WithEvents txtAddEntry As System.Windows.Forms.TextBox
  Friend WithEvents Button3 As System.Windows.Forms.Button
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents cmbLanguages As System.Windows.Forms.ComboBox
  Friend WithEvents cmbXLSTypes As System.Windows.Forms.ComboBox
  Friend WithEvents txtAdditionalTargetlanguageInfo As System.Windows.Forms.TextBox
  Friend WithEvents lstWordTypes As System.Windows.Forms.ListBox
  Friend WithEvents txtPost As System.Windows.Forms.TextBox
  Friend WithEvents txtPre As System.Windows.Forms.TextBox
  Friend WithEvents txtMeaning As System.Windows.Forms.TextBox
  Friend WithEvents Label9 As System.Windows.Forms.Label
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
