<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GroupInput
    Inherits Gravo2k8.MDIChild

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
    Me.cmbSelectGroup = New System.Windows.Forms.ComboBox
    Me.lstWordsInGroup = New System.Windows.Forms.ListBox
    Me.cmdSelect = New System.Windows.Forms.Button
    Me.cmdDeselect = New System.Windows.Forms.Button
    Me.cmdExit = New System.Windows.Forms.Button
    Me.lstWords = New System.Windows.Forms.ListView
    Me.lstMeanings = New System.Windows.Forms.ListView
    Me.txtSearchText = New System.Windows.Forms.TextBox
    Me.cmbSelectLanguage = New System.Windows.Forms.ComboBox
    Me.cmbSelectSubGroup = New System.Windows.Forms.ComboBox
    Me.lblWordsInGroup = New System.Windows.Forms.Label
    Me.lblWordsInLanguage = New System.Windows.Forms.Label
    Me.lblSimilarWord = New System.Windows.Forms.Label
    Me.lblWordsInSubGroup = New System.Windows.Forms.Label
    Me.lblMeaningsDescription = New System.Windows.Forms.Label
    Me.lblWordsDescription = New System.Windows.Forms.Label
    Me.lblSearchDescription = New System.Windows.Forms.Label
    Me.lblCurrentWordIndex = New System.Windows.Forms.Label
    Me.chkMarked = New System.Windows.Forms.CheckBox
    Me.SuspendLayout()
    '
    'cmbSelectGroup
    '
    Me.cmbSelectGroup.FormattingEnabled = True
    Me.cmbSelectGroup.Location = New System.Drawing.Point(12, 12)
    Me.cmbSelectGroup.Name = "cmbSelectGroup"
    Me.cmbSelectGroup.Size = New System.Drawing.Size(213, 21)
    Me.cmbSelectGroup.TabIndex = 0
    '
    'lstWordsInGroup
    '
    Me.lstWordsInGroup.FormattingEnabled = True
    Me.lstWordsInGroup.Location = New System.Drawing.Point(15, 63)
    Me.lstWordsInGroup.Name = "lstWordsInGroup"
    Me.lstWordsInGroup.Size = New System.Drawing.Size(135, 303)
    Me.lstWordsInGroup.TabIndex = 4
    '
    'cmdSelect
    '
    Me.cmdSelect.Location = New System.Drawing.Point(153, 253)
    Me.cmdSelect.Name = "cmdSelect"
    Me.cmdSelect.Size = New System.Drawing.Size(75, 23)
    Me.cmdSelect.TabIndex = 5
    Me.cmdSelect.Text = "<<"
    Me.cmdSelect.UseVisualStyleBackColor = True
    '
    'cmdDeselect
    '
    Me.cmdDeselect.Location = New System.Drawing.Point(153, 121)
    Me.cmdDeselect.Name = "cmdDeselect"
    Me.cmdDeselect.Size = New System.Drawing.Size(75, 23)
    Me.cmdDeselect.TabIndex = 5
    Me.cmdDeselect.Text = ">>"
    Me.cmdDeselect.UseVisualStyleBackColor = True
    '
    'cmdExit
    '
    Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdExit.Location = New System.Drawing.Point(591, 369)
    Me.cmdExit.Name = "cmdExit"
    Me.cmdExit.Size = New System.Drawing.Size(75, 23)
    Me.cmdExit.TabIndex = 11
    Me.cmdExit.Text = "Schließen"
    Me.cmdExit.UseVisualStyleBackColor = True
    '
    'lstWords
    '
    Me.lstWords.FullRowSelect = True
    Me.lstWords.Location = New System.Drawing.Point(234, 211)
    Me.lstWords.MultiSelect = False
    Me.lstWords.Name = "lstWords"
    Me.lstWords.Size = New System.Drawing.Size(432, 104)
    Me.lstWords.TabIndex = 9
    Me.lstWords.UseCompatibleStateImageBehavior = False
    Me.lstWords.View = System.Windows.Forms.View.Details
    '
    'lstMeanings
    '
    Me.lstMeanings.FullRowSelect = True
    Me.lstMeanings.Location = New System.Drawing.Point(234, 79)
    Me.lstMeanings.MultiSelect = False
    Me.lstMeanings.Name = "lstMeanings"
    Me.lstMeanings.Size = New System.Drawing.Size(432, 104)
    Me.lstMeanings.TabIndex = 8
    Me.lstMeanings.UseCompatibleStateImageBehavior = False
    Me.lstMeanings.View = System.Windows.Forms.View.Details
    '
    'txtSearchText
    '
    Me.txtSearchText.Location = New System.Drawing.Point(234, 343)
    Me.txtSearchText.Name = "txtSearchText"
    Me.txtSearchText.Size = New System.Drawing.Size(432, 20)
    Me.txtSearchText.TabIndex = 10
    '
    'cmbSelectLanguage
    '
    Me.cmbSelectLanguage.FormattingEnabled = True
    Me.cmbSelectLanguage.Location = New System.Drawing.Point(453, 12)
    Me.cmbSelectLanguage.Name = "cmbSelectLanguage"
    Me.cmbSelectLanguage.Size = New System.Drawing.Size(213, 21)
    Me.cmbSelectLanguage.TabIndex = 3
    '
    'cmbSelectSubGroup
    '
    Me.cmbSelectSubGroup.FormattingEnabled = True
    Me.cmbSelectSubGroup.Location = New System.Drawing.Point(234, 12)
    Me.cmbSelectSubGroup.Name = "cmbSelectSubGroup"
    Me.cmbSelectSubGroup.Size = New System.Drawing.Size(213, 21)
    Me.cmbSelectSubGroup.TabIndex = 1
    '
    'lblWordsInGroup
    '
    Me.lblWordsInGroup.AutoSize = True
    Me.lblWordsInGroup.Location = New System.Drawing.Point(12, 36)
    Me.lblWordsInGroup.Name = "lblWordsInGroup"
    Me.lblWordsInGroup.Size = New System.Drawing.Size(14, 13)
    Me.lblWordsInGroup.TabIndex = 13
    Me.lblWordsInGroup.Text = "#"
    '
    'lblWordsInLanguage
    '
    Me.lblWordsInLanguage.AutoSize = True
    Me.lblWordsInLanguage.Location = New System.Drawing.Point(450, 36)
    Me.lblWordsInLanguage.Name = "lblWordsInLanguage"
    Me.lblWordsInLanguage.Size = New System.Drawing.Size(14, 13)
    Me.lblWordsInLanguage.TabIndex = 14
    Me.lblWordsInLanguage.Text = "#"
    '
    'lblSimilarWord
    '
    Me.lblSimilarWord.AutoSize = True
    Me.lblSimilarWord.Location = New System.Drawing.Point(231, 369)
    Me.lblSimilarWord.Name = "lblSimilarWord"
    Me.lblSimilarWord.Size = New System.Drawing.Size(14, 13)
    Me.lblSimilarWord.TabIndex = 15
    Me.lblSimilarWord.Text = "#"
    '
    'lblWordsInSubGroup
    '
    Me.lblWordsInSubGroup.AutoSize = True
    Me.lblWordsInSubGroup.Location = New System.Drawing.Point(231, 36)
    Me.lblWordsInSubGroup.Name = "lblWordsInSubGroup"
    Me.lblWordsInSubGroup.Size = New System.Drawing.Size(14, 26)
    Me.lblWordsInSubGroup.TabIndex = 16
    Me.lblWordsInSubGroup.Text = "#" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "#"
    '
    'lblMeaningsDescription
    '
    Me.lblMeaningsDescription.AutoSize = True
    Me.lblMeaningsDescription.Location = New System.Drawing.Point(233, 63)
    Me.lblMeaningsDescription.Name = "lblMeaningsDescription"
    Me.lblMeaningsDescription.Size = New System.Drawing.Size(216, 13)
    Me.lblMeaningsDescription.TabIndex = 17
    Me.lblMeaningsDescription.Text = "Einträge in der Gruppe zum gewählten Wort:"
    '
    'lblWordsDescription
    '
    Me.lblWordsDescription.AutoSize = True
    Me.lblWordsDescription.Location = New System.Drawing.Point(233, 195)
    Me.lblWordsDescription.Name = "lblWordsDescription"
    Me.lblWordsDescription.Size = New System.Drawing.Size(234, 13)
    Me.lblWordsDescription.TabIndex = 18
    Me.lblWordsDescription.Text = "Einträge in der Datenbank zum gewählten Wort:"
    '
    'lblSearchDescription
    '
    Me.lblSearchDescription.AutoSize = True
    Me.lblSearchDescription.Location = New System.Drawing.Point(231, 327)
    Me.lblSearchDescription.Name = "lblSearchDescription"
    Me.lblSearchDescription.Size = New System.Drawing.Size(166, 13)
    Me.lblSearchDescription.TabIndex = 19
    Me.lblSearchDescription.Text = "Eintrag in der Datenbank suchen:"
    '
    'lblCurrentWordIndex
    '
    Me.lblCurrentWordIndex.AutoSize = True
    Me.lblCurrentWordIndex.Location = New System.Drawing.Point(12, 369)
    Me.lblCurrentWordIndex.Name = "lblCurrentWordIndex"
    Me.lblCurrentWordIndex.Size = New System.Drawing.Size(14, 13)
    Me.lblCurrentWordIndex.TabIndex = 20
    Me.lblCurrentWordIndex.Text = "#"
    '
    'chkMarked
    '
    Me.chkMarked.AutoSize = True
    Me.chkMarked.Checked = True
    Me.chkMarked.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkMarked.Location = New System.Drawing.Point(156, 282)
    Me.chkMarked.Name = "chkMarked"
    Me.chkMarked.Size = New System.Drawing.Size(75, 17)
    Me.chkMarked.TabIndex = 6
    Me.chkMarked.Text = "Markieren"
    Me.chkMarked.UseVisualStyleBackColor = True
    '
    'GroupInput
    '
    Me.AcceptButton = Me.cmdSelect
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdExit
    Me.ClientSize = New System.Drawing.Size(672, 399)
    Me.Controls.Add(Me.chkMarked)
    Me.Controls.Add(Me.lblCurrentWordIndex)
    Me.Controls.Add(Me.lblSearchDescription)
    Me.Controls.Add(Me.lblWordsDescription)
    Me.Controls.Add(Me.lblMeaningsDescription)
    Me.Controls.Add(Me.lblWordsInSubGroup)
    Me.Controls.Add(Me.lblSimilarWord)
    Me.Controls.Add(Me.lblWordsInLanguage)
    Me.Controls.Add(Me.lblWordsInGroup)
    Me.Controls.Add(Me.cmbSelectSubGroup)
    Me.Controls.Add(Me.cmbSelectLanguage)
    Me.Controls.Add(Me.txtSearchText)
    Me.Controls.Add(Me.lstMeanings)
    Me.Controls.Add(Me.lstWords)
    Me.Controls.Add(Me.cmdExit)
    Me.Controls.Add(Me.cmdDeselect)
    Me.Controls.Add(Me.cmdSelect)
    Me.Controls.Add(Me.lstWordsInGroup)
    Me.Controls.Add(Me.cmbSelectGroup)
    Me.Name = "GroupInput"
    Me.Text = "Gruppen erweitern"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cmbSelectGroup As System.Windows.Forms.ComboBox
  Friend WithEvents lstWordsInGroup As System.Windows.Forms.ListBox
  Friend WithEvents cmdSelect As System.Windows.Forms.Button
  Friend WithEvents cmdDeselect As System.Windows.Forms.Button
  Friend WithEvents cmdExit As System.Windows.Forms.Button
  Friend WithEvents lstWords As System.Windows.Forms.ListView
  Friend WithEvents lstMeanings As System.Windows.Forms.ListView
  Friend WithEvents txtSearchText As System.Windows.Forms.TextBox
  Friend WithEvents cmbSelectLanguage As System.Windows.Forms.ComboBox
  Friend WithEvents cmbSelectSubGroup As System.Windows.Forms.ComboBox
  Friend WithEvents lblWordsInGroup As System.Windows.Forms.Label
  Friend WithEvents lblWordsInLanguage As System.Windows.Forms.Label
  Friend WithEvents lblSimilarWord As System.Windows.Forms.Label
  Friend WithEvents lblWordsInSubGroup As System.Windows.Forms.Label
  Friend WithEvents lblMeaningsDescription As System.Windows.Forms.Label
  Friend WithEvents lblWordsDescription As System.Windows.Forms.Label
  Friend WithEvents lblSearchDescription As System.Windows.Forms.Label
  Friend WithEvents lblCurrentWordIndex As System.Windows.Forms.Label
  Friend WithEvents chkMarked As System.Windows.Forms.CheckBox
End Class
