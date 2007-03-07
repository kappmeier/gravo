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
    Me.cmdClose = New System.Windows.Forms.Button
    Me.cmbLanguages = New System.Windows.Forms.ComboBox
    Me.chkDirectAdd = New System.Windows.Forms.CheckBox
    Me.cmbDirectAddGroup = New System.Windows.Forms.ComboBox
    Me.Label7 = New System.Windows.Forms.Label
    Me.cmbMainLanguages = New System.Windows.Forms.ComboBox
    Me.Label10 = New System.Windows.Forms.Label
    Me.txtMainLanguage = New System.Windows.Forms.TextBox
    Me.txtLanguage = New System.Windows.Forms.TextBox
    Me.chkNewLanguages = New System.Windows.Forms.CheckBox
    Me.cmbDirectAddSubGroup = New System.Windows.Forms.ComboBox
    Me.GroupBox1.SuspendLayout()
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
    Me.GroupBox1.Text = "Eintrag hinzufügen:"
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
    'cmdClose
    '
    Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdClose.Location = New System.Drawing.Point(436, 298)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(75, 23)
    Me.cmdClose.TabIndex = 14
    Me.cmdClose.Text = "Schließen"
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmbLanguages
    '
    Me.cmbLanguages.FormattingEnabled = True
    Me.cmbLanguages.Location = New System.Drawing.Point(99, 12)
    Me.cmbLanguages.Name = "cmbLanguages"
    Me.cmbLanguages.Size = New System.Drawing.Size(159, 21)
    Me.cmbLanguages.TabIndex = 1
    '
    'chkDirectAdd
    '
    Me.chkDirectAdd.AutoSize = True
    Me.chkDirectAdd.Location = New System.Drawing.Point(12, 180)
    Me.chkDirectAdd.Name = "chkDirectAdd"
    Me.chkDirectAdd.Size = New System.Drawing.Size(219, 17)
    Me.chkDirectAdd.TabIndex = 15
    Me.chkDirectAdd.Text = "Vokabeln sofort einer Gruppe hinzufügen"
    Me.chkDirectAdd.UseVisualStyleBackColor = True
    '
    'cmbDirectAddGroup
    '
    Me.cmbDirectAddGroup.FormattingEnabled = True
    Me.cmbDirectAddGroup.Location = New System.Drawing.Point(12, 203)
    Me.cmbDirectAddGroup.Name = "cmbDirectAddGroup"
    Me.cmbDirectAddGroup.Size = New System.Drawing.Size(246, 21)
    Me.cmbDirectAddGroup.TabIndex = 16
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(9, 15)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(50, 13)
    Me.Label7.TabIndex = 17
    Me.Label7.Text = "Sprache:"
    '
    'cmbMainLanguages
    '
    Me.cmbMainLanguages.FormattingEnabled = True
    Me.cmbMainLanguages.Items.AddRange(New Object() {"german"})
    Me.cmbMainLanguages.Location = New System.Drawing.Point(99, 65)
    Me.cmbMainLanguages.Name = "cmbMainLanguages"
    Me.cmbMainLanguages.Size = New System.Drawing.Size(159, 21)
    Me.cmbMainLanguages.TabIndex = 18
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Location = New System.Drawing.Point(9, 65)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(77, 13)
    Me.Label10.TabIndex = 19
    Me.Label10.Text = "Hauptsprache:"
    '
    'txtMainLanguage
    '
    Me.txtMainLanguage.Location = New System.Drawing.Point(99, 92)
    Me.txtMainLanguage.Name = "txtMainLanguage"
    Me.txtMainLanguage.Size = New System.Drawing.Size(159, 20)
    Me.txtMainLanguage.TabIndex = 20
    '
    'txtLanguage
    '
    Me.txtLanguage.Location = New System.Drawing.Point(99, 39)
    Me.txtLanguage.Name = "txtLanguage"
    Me.txtLanguage.Size = New System.Drawing.Size(159, 20)
    Me.txtLanguage.TabIndex = 21
    '
    'chkNewLanguages
    '
    Me.chkNewLanguages.AutoSize = True
    Me.chkNewLanguages.Location = New System.Drawing.Point(12, 118)
    Me.chkNewLanguages.Name = "chkNewLanguages"
    Me.chkNewLanguages.Size = New System.Drawing.Size(142, 17)
    Me.chkNewLanguages.TabIndex = 22
    Me.chkNewLanguages.Text = "Neue Sprachen anlegen"
    Me.chkNewLanguages.UseVisualStyleBackColor = True
    '
    'cmbDirectAddSubGroup
    '
    Me.cmbDirectAddSubGroup.FormattingEnabled = True
    Me.cmbDirectAddSubGroup.Location = New System.Drawing.Point(12, 230)
    Me.cmbDirectAddSubGroup.Name = "cmbDirectAddSubGroup"
    Me.cmbDirectAddSubGroup.Size = New System.Drawing.Size(246, 21)
    Me.cmbDirectAddSubGroup.TabIndex = 23
    '
    'WordInput
    '
    Me.AcceptButton = Me.cmdAddSubEntry
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdClose
    Me.ClientSize = New System.Drawing.Size(523, 329)
    Me.Controls.Add(Me.cmbDirectAddSubGroup)
    Me.Controls.Add(Me.chkNewLanguages)
    Me.Controls.Add(Me.txtLanguage)
    Me.Controls.Add(Me.txtMainLanguage)
    Me.Controls.Add(Me.Label10)
    Me.Controls.Add(Me.cmbMainLanguages)
    Me.Controls.Add(Me.Label7)
    Me.Controls.Add(Me.cmbDirectAddGroup)
    Me.Controls.Add(Me.chkDirectAdd)
    Me.Controls.Add(Me.cmbLanguages)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.GroupBox1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "WordInput"
    Me.ShowInTaskbar = False
    Me.Text = "Wörter hinzufügen"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtWord As System.Windows.Forms.TextBox
  Friend WithEvents txtMainEntry As System.Windows.Forms.TextBox
  Friend WithEvents cmdAddSubEntry As System.Windows.Forms.Button
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents cmbLanguages As System.Windows.Forms.ComboBox
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
  Friend WithEvents chkDirectAdd As System.Windows.Forms.CheckBox
  Friend WithEvents cmbDirectAddGroup As System.Windows.Forms.ComboBox
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents cmbMainLanguages As System.Windows.Forms.ComboBox
  Friend WithEvents Label10 As System.Windows.Forms.Label
  Friend WithEvents txtMainLanguage As System.Windows.Forms.TextBox
  Friend WithEvents txtLanguage As System.Windows.Forms.TextBox
  Friend WithEvents chkNewLanguages As System.Windows.Forms.CheckBox
  Friend WithEvents cmbDirectAddSubGroup As System.Windows.Forms.ComboBox
End Class
