<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Management
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
    Me.cmbUnitSelectGroup = New System.Windows.Forms.ComboBox
    Me.lblUnitInfo = New System.Windows.Forms.Label
    Me.cmdUnitDelete = New System.Windows.Forms.Button
    Me.cmdUnitEdit = New System.Windows.Forms.Button
    Me.cmdGroupDelete = New System.Windows.Forms.Button
    Me.cmdGroupEdit = New System.Windows.Forms.Button
    Me.txtGroupName = New System.Windows.Forms.TextBox
    Me.txtUnitName = New System.Windows.Forms.TextBox
    Me.lstUnitList = New System.Windows.Forms.ListBox
    Me.lblGroupInfo = New System.Windows.Forms.Label
    Me.tabUnit = New System.Windows.Forms.TabPage
    Me.cmdUnitAdd = New System.Windows.Forms.Button
    Me.lstGroupList = New System.Windows.Forms.ListBox
    Me.cmdGroupAdd = New System.Windows.Forms.Button
    Me.tabGroup = New System.Windows.Forms.TabPage
    Me.cmdSchließen = New System.Windows.Forms.Button
    Me.tab = New System.Windows.Forms.TabControl
    Me.tabImport = New System.Windows.Forms.TabPage
    Me.lblImportGroupCount = New System.Windows.Forms.Label
    Me.optGroupAppend = New System.Windows.Forms.RadioButton
    Me.optGroupOverwrite = New System.Windows.Forms.RadioButton
    Me.lblImportDictCount = New System.Windows.Forms.Label
    Me.chkImportStatistic = New System.Windows.Forms.CheckBox
    Me.cmdImportGroup = New System.Windows.Forms.Button
    Me.cmdImportDictionary = New System.Windows.Forms.Button
    Me.lblImportDB = New System.Windows.Forms.Label
    Me.cmdImortSelectDB = New System.Windows.Forms.Button
    Me.tabExport = New System.Windows.Forms.TabPage
    Me.cmdExportUserData = New System.Windows.Forms.Button
    Me.chkExportStats = New System.Windows.Forms.CheckBox
    Me.chkExportEmptyEntrys = New System.Windows.Forms.CheckBox
    Me.lstExportLanguages = New System.Windows.Forms.CheckedListBox
    Me.lstExportGroups = New System.Windows.Forms.CheckedListBox
    Me.cmdExport = New System.Windows.Forms.Button
    Me.tapDatabase = New System.Windows.Forms.TabPage
    Me.lblErrorCount = New System.Windows.Forms.Label
    Me.cmdSaveDB = New System.Windows.Forms.Button
    Me.cmdReorganizeDB = New System.Windows.Forms.Button
    Me.cmdDBVersion = New System.Windows.Forms.Button
    Me.lblDBVersion = New System.Windows.Forms.Label
    Me.dlgExport = New System.Windows.Forms.OpenFileDialog
    Me.dlgSaveDb = New System.Windows.Forms.SaveFileDialog
    Me.dlgImport = New System.Windows.Forms.OpenFileDialog
    Me.Button1 = New System.Windows.Forms.Button
    Me.tabUnit.SuspendLayout()
    Me.tabGroup.SuspendLayout()
    Me.tab.SuspendLayout()
    Me.tabImport.SuspendLayout()
    Me.tabExport.SuspendLayout()
    Me.tapDatabase.SuspendLayout()
    Me.SuspendLayout()
    '
    'cmbUnitSelectGroup
    '
    Me.cmbUnitSelectGroup.Location = New System.Drawing.Point(3, 3)
    Me.cmbUnitSelectGroup.Name = "cmbUnitSelectGroup"
    Me.cmbUnitSelectGroup.Size = New System.Drawing.Size(354, 21)
    Me.cmbUnitSelectGroup.TabIndex = 16
    Me.cmbUnitSelectGroup.Text = "#"
    '
    'lblUnitInfo
    '
    Me.lblUnitInfo.Location = New System.Drawing.Point(153, 56)
    Me.lblUnitInfo.Name = "lblUnitInfo"
    Me.lblUnitInfo.Size = New System.Drawing.Size(118, 88)
    Me.lblUnitInfo.TabIndex = 15
    Me.lblUnitInfo.Text = "#"
    '
    'cmdUnitDelete
    '
    Me.cmdUnitDelete.Enabled = False
    Me.cmdUnitDelete.Location = New System.Drawing.Point(277, 116)
    Me.cmdUnitDelete.Name = "cmdUnitDelete"
    Me.cmdUnitDelete.Size = New System.Drawing.Size(80, 24)
    Me.cmdUnitDelete.TabIndex = 14
    Me.cmdUnitDelete.Text = "Löschen"
    '
    'cmdUnitEdit
    '
    Me.cmdUnitEdit.Location = New System.Drawing.Point(277, 86)
    Me.cmdUnitEdit.Name = "cmdUnitEdit"
    Me.cmdUnitEdit.Size = New System.Drawing.Size(80, 24)
    Me.cmdUnitEdit.TabIndex = 11
    Me.cmdUnitEdit.Text = "Ändern"
    '
    'cmdGroupDelete
    '
    Me.cmdGroupDelete.Location = New System.Drawing.Point(277, 92)
    Me.cmdGroupDelete.Name = "cmdGroupDelete"
    Me.cmdGroupDelete.Size = New System.Drawing.Size(80, 24)
    Me.cmdGroupDelete.TabIndex = 6
    Me.cmdGroupDelete.Text = "Löschen"
    '
    'cmdGroupEdit
    '
    Me.cmdGroupEdit.Location = New System.Drawing.Point(277, 62)
    Me.cmdGroupEdit.Name = "cmdGroupEdit"
    Me.cmdGroupEdit.Size = New System.Drawing.Size(80, 24)
    Me.cmdGroupEdit.TabIndex = 3
    Me.cmdGroupEdit.Text = "Ändern"
    '
    'txtGroupName
    '
    Me.txtGroupName.Location = New System.Drawing.Point(153, 3)
    Me.txtGroupName.Name = "txtGroupName"
    Me.txtGroupName.Size = New System.Drawing.Size(204, 20)
    Me.txtGroupName.TabIndex = 2
    Me.txtGroupName.Text = "#"
    '
    'txtUnitName
    '
    Me.txtUnitName.Location = New System.Drawing.Point(153, 30)
    Me.txtUnitName.Name = "txtUnitName"
    Me.txtUnitName.Size = New System.Drawing.Size(204, 20)
    Me.txtUnitName.TabIndex = 10
    Me.txtUnitName.Text = "#"
    '
    'lstUnitList
    '
    Me.lstUnitList.Location = New System.Drawing.Point(3, 30)
    Me.lstUnitList.Name = "lstUnitList"
    Me.lstUnitList.Size = New System.Drawing.Size(144, 199)
    Me.lstUnitList.TabIndex = 9
    '
    'lblGroupInfo
    '
    Me.lblGroupInfo.Location = New System.Drawing.Point(153, 26)
    Me.lblGroupInfo.Name = "lblGroupInfo"
    Me.lblGroupInfo.Size = New System.Drawing.Size(118, 88)
    Me.lblGroupInfo.TabIndex = 16
    Me.lblGroupInfo.Text = "#"
    '
    'tabUnit
    '
    Me.tabUnit.Controls.Add(Me.cmbUnitSelectGroup)
    Me.tabUnit.Controls.Add(Me.lblUnitInfo)
    Me.tabUnit.Controls.Add(Me.cmdUnitDelete)
    Me.tabUnit.Controls.Add(Me.cmdUnitEdit)
    Me.tabUnit.Controls.Add(Me.txtUnitName)
    Me.tabUnit.Controls.Add(Me.lstUnitList)
    Me.tabUnit.Controls.Add(Me.cmdUnitAdd)
    Me.tabUnit.Location = New System.Drawing.Point(4, 22)
    Me.tabUnit.Name = "tabUnit"
    Me.tabUnit.Size = New System.Drawing.Size(360, 238)
    Me.tabUnit.TabIndex = 2
    Me.tabUnit.Text = "Lektionen"
    Me.tabUnit.UseVisualStyleBackColor = True
    '
    'cmdUnitAdd
    '
    Me.cmdUnitAdd.Location = New System.Drawing.Point(277, 56)
    Me.cmdUnitAdd.Name = "cmdUnitAdd"
    Me.cmdUnitAdd.Size = New System.Drawing.Size(80, 24)
    Me.cmdUnitAdd.TabIndex = 8
    Me.cmdUnitAdd.Text = "Hinzufügen"
    '
    'lstGroupList
    '
    Me.lstGroupList.Location = New System.Drawing.Point(3, 3)
    Me.lstGroupList.Name = "lstGroupList"
    Me.lstGroupList.Size = New System.Drawing.Size(144, 225)
    Me.lstGroupList.TabIndex = 1
    '
    'cmdGroupAdd
    '
    Me.cmdGroupAdd.Location = New System.Drawing.Point(277, 32)
    Me.cmdGroupAdd.Name = "cmdGroupAdd"
    Me.cmdGroupAdd.Size = New System.Drawing.Size(80, 24)
    Me.cmdGroupAdd.TabIndex = 0
    Me.cmdGroupAdd.Text = "Hinzufügen"
    '
    'tabGroup
    '
    Me.tabGroup.Controls.Add(Me.lblGroupInfo)
    Me.tabGroup.Controls.Add(Me.cmdGroupDelete)
    Me.tabGroup.Controls.Add(Me.cmdGroupEdit)
    Me.tabGroup.Controls.Add(Me.txtGroupName)
    Me.tabGroup.Controls.Add(Me.lstGroupList)
    Me.tabGroup.Controls.Add(Me.cmdGroupAdd)
    Me.tabGroup.Location = New System.Drawing.Point(4, 22)
    Me.tabGroup.Name = "tabGroup"
    Me.tabGroup.Size = New System.Drawing.Size(360, 238)
    Me.tabGroup.TabIndex = 1
    Me.tabGroup.Text = "Gruppen"
    Me.tabGroup.UseVisualStyleBackColor = True
    '
    'cmdSchließen
    '
    Me.cmdSchließen.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdSchließen.Location = New System.Drawing.Point(297, 274)
    Me.cmdSchließen.Name = "cmdSchließen"
    Me.cmdSchließen.Size = New System.Drawing.Size(75, 23)
    Me.cmdSchließen.TabIndex = 6
    Me.cmdSchließen.Text = "Schließen"
    '
    'tab
    '
    Me.tab.Controls.Add(Me.tabGroup)
    Me.tab.Controls.Add(Me.tabUnit)
    Me.tab.Controls.Add(Me.tabImport)
    Me.tab.Controls.Add(Me.tabExport)
    Me.tab.Controls.Add(Me.tapDatabase)
    Me.tab.Location = New System.Drawing.Point(8, 8)
    Me.tab.Name = "tab"
    Me.tab.SelectedIndex = 0
    Me.tab.Size = New System.Drawing.Size(368, 264)
    Me.tab.TabIndex = 5
    '
    'tabImport
    '
    Me.tabImport.Controls.Add(Me.lblImportGroupCount)
    Me.tabImport.Controls.Add(Me.optGroupAppend)
    Me.tabImport.Controls.Add(Me.optGroupOverwrite)
    Me.tabImport.Controls.Add(Me.lblImportDictCount)
    Me.tabImport.Controls.Add(Me.chkImportStatistic)
    Me.tabImport.Controls.Add(Me.cmdImportGroup)
    Me.tabImport.Controls.Add(Me.cmdImportDictionary)
    Me.tabImport.Controls.Add(Me.lblImportDB)
    Me.tabImport.Controls.Add(Me.cmdImortSelectDB)
    Me.tabImport.Location = New System.Drawing.Point(4, 22)
    Me.tabImport.Name = "tabImport"
    Me.tabImport.Size = New System.Drawing.Size(360, 238)
    Me.tabImport.TabIndex = 0
    Me.tabImport.Text = "Importieren"
    Me.tabImport.UseVisualStyleBackColor = True
    '
    'lblImportGroupCount
    '
    Me.lblImportGroupCount.AutoSize = True
    Me.lblImportGroupCount.Location = New System.Drawing.Point(3, 138)
    Me.lblImportGroupCount.Name = "lblImportGroupCount"
    Me.lblImportGroupCount.Size = New System.Drawing.Size(14, 13)
    Me.lblImportGroupCount.TabIndex = 29
    Me.lblImportGroupCount.Text = "#"
    '
    'optGroupAppend
    '
    Me.optGroupAppend.AutoSize = True
    Me.optGroupAppend.Location = New System.Drawing.Point(3, 78)
    Me.optGroupAppend.Name = "optGroupAppend"
    Me.optGroupAppend.Size = New System.Drawing.Size(121, 17)
    Me.optGroupAppend.TabIndex = 28
    Me.optGroupAppend.Text = "Gruppen hinzufügen"
    Me.optGroupAppend.UseVisualStyleBackColor = True
    '
    'optGroupOverwrite
    '
    Me.optGroupOverwrite.AutoSize = True
    Me.optGroupOverwrite.Checked = True
    Me.optGroupOverwrite.Location = New System.Drawing.Point(3, 55)
    Me.optGroupOverwrite.Name = "optGroupOverwrite"
    Me.optGroupOverwrite.Size = New System.Drawing.Size(132, 17)
    Me.optGroupOverwrite.TabIndex = 27
    Me.optGroupOverwrite.TabStop = True
    Me.optGroupOverwrite.Text = "Überschreibe Gruppen"
    Me.optGroupOverwrite.UseVisualStyleBackColor = True
    '
    'lblImportDictCount
    '
    Me.lblImportDictCount.AutoSize = True
    Me.lblImportDictCount.Location = New System.Drawing.Point(177, 138)
    Me.lblImportDictCount.Name = "lblImportDictCount"
    Me.lblImportDictCount.Size = New System.Drawing.Size(14, 13)
    Me.lblImportDictCount.TabIndex = 26
    Me.lblImportDictCount.Text = "#"
    '
    'chkImportStatistic
    '
    Me.chkImportStatistic.AutoSize = True
    Me.chkImportStatistic.Location = New System.Drawing.Point(3, 32)
    Me.chkImportStatistic.Name = "chkImportStatistic"
    Me.chkImportStatistic.Size = New System.Drawing.Size(117, 17)
    Me.chkImportStatistic.TabIndex = 25
    Me.chkImportStatistic.Text = "Statistik importieren"
    Me.chkImportStatistic.UseVisualStyleBackColor = True
    '
    'cmdImportGroup
    '
    Me.cmdImportGroup.Location = New System.Drawing.Point(3, 101)
    Me.cmdImportGroup.Name = "cmdImportGroup"
    Me.cmdImportGroup.Size = New System.Drawing.Size(168, 23)
    Me.cmdImportGroup.TabIndex = 24
    Me.cmdImportGroup.Text = "Importiere Gruppe"
    Me.cmdImportGroup.UseVisualStyleBackColor = True
    '
    'cmdImportDictionary
    '
    Me.cmdImportDictionary.Location = New System.Drawing.Point(177, 101)
    Me.cmdImportDictionary.Name = "cmdImportDictionary"
    Me.cmdImportDictionary.Size = New System.Drawing.Size(168, 23)
    Me.cmdImportDictionary.TabIndex = 22
    Me.cmdImportDictionary.Text = "Imporiere Wörterbuch"
    Me.cmdImportDictionary.UseVisualStyleBackColor = True
    '
    'lblImportDB
    '
    Me.lblImportDB.Location = New System.Drawing.Point(177, 3)
    Me.lblImportDB.Name = "lblImportDB"
    Me.lblImportDB.Size = New System.Drawing.Size(180, 67)
    Me.lblImportDB.TabIndex = 1
    Me.lblImportDB.Text = "#"
    '
    'cmdImortSelectDB
    '
    Me.cmdImortSelectDB.Location = New System.Drawing.Point(3, 3)
    Me.cmdImortSelectDB.Name = "cmdImortSelectDB"
    Me.cmdImortSelectDB.Size = New System.Drawing.Size(168, 23)
    Me.cmdImortSelectDB.TabIndex = 0
    Me.cmdImortSelectDB.Text = "Datei auswählen"
    '
    'tabExport
    '
    Me.tabExport.Controls.Add(Me.cmdExportUserData)
    Me.tabExport.Controls.Add(Me.chkExportStats)
    Me.tabExport.Controls.Add(Me.chkExportEmptyEntrys)
    Me.tabExport.Controls.Add(Me.lstExportLanguages)
    Me.tabExport.Controls.Add(Me.lstExportGroups)
    Me.tabExport.Controls.Add(Me.cmdExport)
    Me.tabExport.Location = New System.Drawing.Point(4, 22)
    Me.tabExport.Name = "tabExport"
    Me.tabExport.Size = New System.Drawing.Size(360, 238)
    Me.tabExport.TabIndex = 4
    Me.tabExport.Text = "Exportieren"
    Me.tabExport.UseVisualStyleBackColor = True
    '
    'cmdExportUserData
    '
    Me.cmdExportUserData.Location = New System.Drawing.Point(189, 181)
    Me.cmdExportUserData.Name = "cmdExportUserData"
    Me.cmdExportUserData.Size = New System.Drawing.Size(168, 23)
    Me.cmdExportUserData.TabIndex = 25
    Me.cmdExportUserData.Text = "Benutzerdaten exportieren"
    Me.cmdExportUserData.UseVisualStyleBackColor = True
    '
    'chkExportStats
    '
    Me.chkExportStats.AutoSize = True
    Me.chkExportStats.Location = New System.Drawing.Point(3, 103)
    Me.chkExportStats.Name = "chkExportStats"
    Me.chkExportStats.Size = New System.Drawing.Size(116, 17)
    Me.chkExportStats.TabIndex = 24
    Me.chkExportStats.Text = "Statistik mit sichern"
    Me.chkExportStats.UseVisualStyleBackColor = True
    '
    'chkExportEmptyEntrys
    '
    Me.chkExportEmptyEntrys.AutoSize = True
    Me.chkExportEmptyEntrys.Location = New System.Drawing.Point(183, 103)
    Me.chkExportEmptyEntrys.Name = "chkExportEmptyEntrys"
    Me.chkExportEmptyEntrys.Size = New System.Drawing.Size(169, 17)
    Me.chkExportEmptyEntrys.TabIndex = 23
    Me.chkExportEmptyEntrys.Text = "leere Haupteinträge auslassen"
    Me.chkExportEmptyEntrys.UseVisualStyleBackColor = True
    '
    'lstExportLanguages
    '
    Me.lstExportLanguages.FormattingEnabled = True
    Me.lstExportLanguages.Location = New System.Drawing.Point(3, 3)
    Me.lstExportLanguages.Name = "lstExportLanguages"
    Me.lstExportLanguages.Size = New System.Drawing.Size(174, 94)
    Me.lstExportLanguages.TabIndex = 22
    '
    'lstExportGroups
    '
    Me.lstExportGroups.FormattingEnabled = True
    Me.lstExportGroups.Location = New System.Drawing.Point(183, 3)
    Me.lstExportGroups.Name = "lstExportGroups"
    Me.lstExportGroups.Size = New System.Drawing.Size(174, 94)
    Me.lstExportGroups.TabIndex = 21
    '
    'cmdExport
    '
    Me.cmdExport.Location = New System.Drawing.Point(189, 212)
    Me.cmdExport.Name = "cmdExport"
    Me.cmdExport.Size = New System.Drawing.Size(168, 23)
    Me.cmdExport.TabIndex = 20
    Me.cmdExport.Text = "Exportieren"
    '
    'tapDatabase
    '
    Me.tapDatabase.Controls.Add(Me.Button1)
    Me.tapDatabase.Controls.Add(Me.lblErrorCount)
    Me.tapDatabase.Controls.Add(Me.cmdSaveDB)
    Me.tapDatabase.Controls.Add(Me.cmdReorganizeDB)
    Me.tapDatabase.Controls.Add(Me.cmdDBVersion)
    Me.tapDatabase.Controls.Add(Me.lblDBVersion)
    Me.tapDatabase.Location = New System.Drawing.Point(4, 22)
    Me.tapDatabase.Name = "tapDatabase"
    Me.tapDatabase.Size = New System.Drawing.Size(360, 238)
    Me.tapDatabase.TabIndex = 3
    Me.tapDatabase.Text = "Datenbank"
    Me.tapDatabase.UseVisualStyleBackColor = True
    '
    'lblErrorCount
    '
    Me.lblErrorCount.AutoSize = True
    Me.lblErrorCount.Location = New System.Drawing.Point(3, 82)
    Me.lblErrorCount.Name = "lblErrorCount"
    Me.lblErrorCount.Size = New System.Drawing.Size(14, 13)
    Me.lblErrorCount.TabIndex = 31
    Me.lblErrorCount.Text = "#"
    '
    'cmdSaveDB
    '
    Me.cmdSaveDB.Location = New System.Drawing.Point(3, 212)
    Me.cmdSaveDB.Name = "cmdSaveDB"
    Me.cmdSaveDB.Size = New System.Drawing.Size(168, 23)
    Me.cmdSaveDB.TabIndex = 30
    Me.cmdSaveDB.Text = "Datenbank sichern"
    Me.cmdSaveDB.UseVisualStyleBackColor = True
    '
    'cmdReorganizeDB
    '
    Me.cmdReorganizeDB.Location = New System.Drawing.Point(6, 56)
    Me.cmdReorganizeDB.Name = "cmdReorganizeDB"
    Me.cmdReorganizeDB.Size = New System.Drawing.Size(168, 23)
    Me.cmdReorganizeDB.TabIndex = 29
    Me.cmdReorganizeDB.Text = "Konsistenz prüfen"
    Me.cmdReorganizeDB.UseVisualStyleBackColor = True
    '
    'cmdDBVersion
    '
    Me.cmdDBVersion.Enabled = False
    Me.cmdDBVersion.Location = New System.Drawing.Point(6, 27)
    Me.cmdDBVersion.Name = "cmdDBVersion"
    Me.cmdDBVersion.Size = New System.Drawing.Size(168, 23)
    Me.cmdDBVersion.TabIndex = 27
    Me.cmdDBVersion.Text = "#"
    '
    'lblDBVersion
    '
    Me.lblDBVersion.Location = New System.Drawing.Point(3, 0)
    Me.lblDBVersion.Name = "lblDBVersion"
    Me.lblDBVersion.Size = New System.Drawing.Size(190, 24)
    Me.lblDBVersion.TabIndex = 28
    Me.lblDBVersion.Text = "#"
    Me.lblDBVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'dlgExport
    '
    Me.dlgExport.CheckFileExists = False
    Me.dlgExport.DefaultExt = "mdb"
    Me.dlgExport.FileName = "ExportDB.mdb"
    Me.dlgExport.Filter = "Datenbanken|*.mdb"
    Me.dlgExport.ShowHelp = True
    '
    'dlgSaveDb
    '
    Me.dlgSaveDb.FileName = "SaveDB.mdb"
    '
    'dlgImport
    '
    Me.dlgImport.CheckFileExists = False
    Me.dlgImport.DefaultExt = "mdb"
    Me.dlgImport.FileName = "ImportDB.mdb"
    Me.dlgImport.Filter = "Datenbanken|*.mdb"
    Me.dlgImport.ShowHelp = True
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(134, 119)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(75, 23)
    Me.Button1.TabIndex = 32
    Me.Button1.Text = "Button1"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'Management
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdSchließen
    Me.ClientSize = New System.Drawing.Size(382, 304)
    Me.Controls.Add(Me.cmdSchließen)
    Me.Controls.Add(Me.tab)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "Management"
    Me.Text = "Daten-Management"
    Me.tabUnit.ResumeLayout(False)
    Me.tabUnit.PerformLayout()
    Me.tabGroup.ResumeLayout(False)
    Me.tabGroup.PerformLayout()
    Me.tab.ResumeLayout(False)
    Me.tabImport.ResumeLayout(False)
    Me.tabImport.PerformLayout()
    Me.tabExport.ResumeLayout(False)
    Me.tabExport.PerformLayout()
    Me.tapDatabase.ResumeLayout(False)
    Me.tapDatabase.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents cmbUnitSelectGroup As System.Windows.Forms.ComboBox
  Friend WithEvents lblUnitInfo As System.Windows.Forms.Label
  Friend WithEvents cmdUnitDelete As System.Windows.Forms.Button
  Friend WithEvents cmdUnitEdit As System.Windows.Forms.Button
  Friend WithEvents cmdGroupDelete As System.Windows.Forms.Button
  Friend WithEvents cmdGroupEdit As System.Windows.Forms.Button
  Friend WithEvents txtGroupName As System.Windows.Forms.TextBox
  Friend WithEvents txtUnitName As System.Windows.Forms.TextBox
  Friend WithEvents lstUnitList As System.Windows.Forms.ListBox
  Friend WithEvents lblGroupInfo As System.Windows.Forms.Label
  Friend WithEvents tabUnit As System.Windows.Forms.TabPage
  Friend WithEvents cmdUnitAdd As System.Windows.Forms.Button
  Friend WithEvents lstGroupList As System.Windows.Forms.ListBox
  Friend WithEvents cmdGroupAdd As System.Windows.Forms.Button
  Friend WithEvents tabGroup As System.Windows.Forms.TabPage
  Friend WithEvents cmdSchließen As System.Windows.Forms.Button
  Friend WithEvents tab As System.Windows.Forms.TabControl
  Friend WithEvents tabImport As System.Windows.Forms.TabPage
  Friend WithEvents lblImportDB As System.Windows.Forms.Label
  Friend WithEvents cmdImortSelectDB As System.Windows.Forms.Button
  Friend WithEvents tapDatabase As System.Windows.Forms.TabPage
  Friend WithEvents cmdReorganizeDB As System.Windows.Forms.Button
  Friend WithEvents cmdDBVersion As System.Windows.Forms.Button
  Friend WithEvents lblDBVersion As System.Windows.Forms.Label
  Friend WithEvents cmdSaveDB As System.Windows.Forms.Button
  Friend WithEvents tabExport As System.Windows.Forms.TabPage
  Friend WithEvents cmdExport As System.Windows.Forms.Button
  Friend WithEvents chkImportStatistic As System.Windows.Forms.CheckBox
  Friend WithEvents cmdImportGroup As System.Windows.Forms.Button
  Friend WithEvents cmdImportDictionary As System.Windows.Forms.Button
  Friend WithEvents cmdExportUserData As System.Windows.Forms.Button
  Friend WithEvents chkExportStats As System.Windows.Forms.CheckBox
  Friend WithEvents chkExportEmptyEntrys As System.Windows.Forms.CheckBox
  Friend WithEvents lstExportLanguages As System.Windows.Forms.CheckedListBox
  Friend WithEvents lstExportGroups As System.Windows.Forms.CheckedListBox
  Friend WithEvents dlgExport As System.Windows.Forms.OpenFileDialog
  Friend WithEvents dlgSaveDb As System.Windows.Forms.SaveFileDialog
  Friend WithEvents lblErrorCount As System.Windows.Forms.Label
  Friend WithEvents lblImportDictCount As System.Windows.Forms.Label
  Friend WithEvents dlgImport As System.Windows.Forms.OpenFileDialog
  Friend WithEvents optGroupAppend As System.Windows.Forms.RadioButton
  Friend WithEvents optGroupOverwrite As System.Windows.Forms.RadioButton
  Friend WithEvents lblImportGroupCount As System.Windows.Forms.Label
  Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
