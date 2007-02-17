<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GroupInput
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
    Me.cmbSelectGroup = New System.Windows.Forms.ComboBox
    Me.lstWordsInGroup = New System.Windows.Forms.ListBox
    Me.lstAllWords = New System.Windows.Forms.ListBox
    Me.cmdSelect = New System.Windows.Forms.Button
    Me.cmdDeselect = New System.Windows.Forms.Button
    Me.cmdExit = New System.Windows.Forms.Button
    Me.cmdNewGroup = New System.Windows.Forms.Button
    Me.lstWords = New System.Windows.Forms.ListView
    Me.lstMeanings = New System.Windows.Forms.ListView
    Me.cmdSearch = New System.Windows.Forms.Button
    Me.txtSearchText = New System.Windows.Forms.TextBox
    Me.SuspendLayout()
    '
    'cmbSelectGroup
    '
    Me.cmbSelectGroup.FormattingEnabled = True
    Me.cmbSelectGroup.Location = New System.Drawing.Point(12, 12)
    Me.cmbSelectGroup.Name = "cmbSelectGroup"
    Me.cmbSelectGroup.Size = New System.Drawing.Size(120, 21)
    Me.cmbSelectGroup.TabIndex = 0
    '
    'lstWordsInGroup
    '
    Me.lstWordsInGroup.FormattingEnabled = True
    Me.lstWordsInGroup.Location = New System.Drawing.Point(12, 39)
    Me.lstWordsInGroup.Name = "lstWordsInGroup"
    Me.lstWordsInGroup.Size = New System.Drawing.Size(120, 238)
    Me.lstWordsInGroup.TabIndex = 2
    '
    'lstAllWords
    '
    Me.lstAllWords.FormattingEnabled = True
    Me.lstAllWords.Location = New System.Drawing.Point(713, 39)
    Me.lstAllWords.Name = "lstAllWords"
    Me.lstAllWords.Size = New System.Drawing.Size(120, 238)
    Me.lstAllWords.TabIndex = 9
    '
    'cmdSelect
    '
    Me.cmdSelect.Location = New System.Drawing.Point(385, 62)
    Me.cmdSelect.Name = "cmdSelect"
    Me.cmdSelect.Size = New System.Drawing.Size(75, 23)
    Me.cmdSelect.TabIndex = 4
    Me.cmdSelect.Text = "<<"
    Me.cmdSelect.UseVisualStyleBackColor = True
    '
    'cmdDeselect
    '
    Me.cmdDeselect.Location = New System.Drawing.Point(385, 91)
    Me.cmdDeselect.Name = "cmdDeselect"
    Me.cmdDeselect.Size = New System.Drawing.Size(75, 23)
    Me.cmdDeselect.TabIndex = 5
    Me.cmdDeselect.Text = ">>"
    Me.cmdDeselect.UseVisualStyleBackColor = True
    '
    'cmdExit
    '
    Me.cmdExit.Location = New System.Drawing.Point(466, 254)
    Me.cmdExit.Name = "cmdExit"
    Me.cmdExit.Size = New System.Drawing.Size(75, 23)
    Me.cmdExit.TabIndex = 10
    Me.cmdExit.Text = "Schließen"
    Me.cmdExit.UseVisualStyleBackColor = True
    '
    'cmdNewGroup
    '
    Me.cmdNewGroup.Location = New System.Drawing.Point(138, 12)
    Me.cmdNewGroup.Name = "cmdNewGroup"
    Me.cmdNewGroup.Size = New System.Drawing.Size(138, 23)
    Me.cmdNewGroup.TabIndex = 1
    Me.cmdNewGroup.Text = "Neue Gruppe"
    Me.cmdNewGroup.UseVisualStyleBackColor = True
    '
    'lstWords
    '
    Me.lstWords.FullRowSelect = True
    Me.lstWords.Location = New System.Drawing.Point(466, 39)
    Me.lstWords.MultiSelect = False
    Me.lstWords.Name = "lstWords"
    Me.lstWords.Size = New System.Drawing.Size(241, 104)
    Me.lstWords.TabIndex = 6
    Me.lstWords.UseCompatibleStateImageBehavior = False
    Me.lstWords.View = System.Windows.Forms.View.Details
    '
    'lstMeanings
    '
    Me.lstMeanings.FullRowSelect = True
    Me.lstMeanings.Location = New System.Drawing.Point(138, 39)
    Me.lstMeanings.MultiSelect = False
    Me.lstMeanings.Name = "lstMeanings"
    Me.lstMeanings.Size = New System.Drawing.Size(241, 104)
    Me.lstMeanings.TabIndex = 3
    Me.lstMeanings.UseCompatibleStateImageBehavior = False
    Me.lstMeanings.View = System.Windows.Forms.View.Details
    '
    'cmdSearch
    '
    Me.cmdSearch.Location = New System.Drawing.Point(632, 149)
    Me.cmdSearch.Name = "cmdSearch"
    Me.cmdSearch.Size = New System.Drawing.Size(75, 23)
    Me.cmdSearch.TabIndex = 8
    Me.cmdSearch.Text = "Suchen"
    Me.cmdSearch.UseVisualStyleBackColor = True
    '
    'txtSearchText
    '
    Me.txtSearchText.Location = New System.Drawing.Point(466, 149)
    Me.txtSearchText.Name = "txtSearchText"
    Me.txtSearchText.Size = New System.Drawing.Size(160, 20)
    Me.txtSearchText.TabIndex = 7
    '
    'GroupInput
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(841, 281)
    Me.Controls.Add(Me.txtSearchText)
    Me.Controls.Add(Me.cmdSearch)
    Me.Controls.Add(Me.lstMeanings)
    Me.Controls.Add(Me.lstWords)
    Me.Controls.Add(Me.cmdNewGroup)
    Me.Controls.Add(Me.cmdExit)
    Me.Controls.Add(Me.cmdDeselect)
    Me.Controls.Add(Me.cmdSelect)
    Me.Controls.Add(Me.lstAllWords)
    Me.Controls.Add(Me.lstWordsInGroup)
    Me.Controls.Add(Me.cmbSelectGroup)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.Name = "GroupInput"
    Me.ShowInTaskbar = False
    Me.Text = "Gruppen hinzufügen"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cmbSelectGroup As System.Windows.Forms.ComboBox
  Friend WithEvents lstWordsInGroup As System.Windows.Forms.ListBox
  Friend WithEvents lstAllWords As System.Windows.Forms.ListBox
  Friend WithEvents cmdSelect As System.Windows.Forms.Button
  Friend WithEvents cmdDeselect As System.Windows.Forms.Button
  Friend WithEvents cmdExit As System.Windows.Forms.Button
  Friend WithEvents cmdNewGroup As System.Windows.Forms.Button
  Friend WithEvents lstWords As System.Windows.Forms.ListView
  Friend WithEvents lstMeanings As System.Windows.Forms.ListView
  Friend WithEvents cmdSearch As System.Windows.Forms.Button
  Friend WithEvents txtSearchText As System.Windows.Forms.TextBox
End Class
