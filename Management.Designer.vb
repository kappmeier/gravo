<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Management
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
        Me.cmbUnitSelectGroup = New System.Windows.Forms.ComboBox()
        Me.lblUnitInfo = New System.Windows.Forms.Label()
        Me.cmdUnitDelete = New System.Windows.Forms.Button()
        Me.cmdUnitEdit = New System.Windows.Forms.Button()
        Me.cmdGroupDelete = New System.Windows.Forms.Button()
        Me.cmdGroupEdit = New System.Windows.Forms.Button()
        Me.txtGroupName = New System.Windows.Forms.TextBox()
        Me.txtUnitName = New System.Windows.Forms.TextBox()
        Me.lstUnitList = New System.Windows.Forms.ListBox()
        Me.lblGroupInfo = New System.Windows.Forms.Label()
        Me.tabUnit = New System.Windows.Forms.TabPage()
        Me.cmdUnitUp = New System.Windows.Forms.Button()
        Me.cmdUnitDown = New System.Windows.Forms.Button()
        Me.cmdUnitAdd = New System.Windows.Forms.Button()
        Me.lstGroupList = New System.Windows.Forms.ListBox()
        Me.cmdGroupAdd = New System.Windows.Forms.Button()
        Me.tabGroup = New System.Windows.Forms.TabPage()
        Me.cmdSchließen = New System.Windows.Forms.Button()
        Me.tab = New System.Windows.Forms.TabControl()
        Me.tabImport = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblImportGroupCount = New System.Windows.Forms.Label()
        Me.optGroupAppend = New System.Windows.Forms.RadioButton()
        Me.optGroupOverwrite = New System.Windows.Forms.RadioButton()
        Me.lblImportDictCount = New System.Windows.Forms.Label()
        Me.chkImportStatistic = New System.Windows.Forms.CheckBox()
        Me.cmdImportGroup = New System.Windows.Forms.Button()
        Me.cmdImportDictionary = New System.Windows.Forms.Button()
        Me.lblImportDB = New System.Windows.Forms.Label()
        Me.cmdImortSelectDB = New System.Windows.Forms.Button()
        Me.tabExport = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdExportUserData = New System.Windows.Forms.Button()
        Me.chkExportStats = New System.Windows.Forms.CheckBox()
        Me.chkExportEmptyEntrys = New System.Windows.Forms.CheckBox()
        Me.lstExportLanguages = New System.Windows.Forms.CheckedListBox()
        Me.lstExportGroups = New System.Windows.Forms.CheckedListBox()
        Me.cmdExport = New System.Windows.Forms.Button()
        Me.tapDatabase = New System.Windows.Forms.TabPage()
        Me.lblErrorCount = New System.Windows.Forms.Label()
        Me.cmdSaveDB = New System.Windows.Forms.Button()
        Me.cmdReorganizeDB = New System.Windows.Forms.Button()
        Me.cmdDBVersion = New System.Windows.Forms.Button()
        Me.lblDBVersion = New System.Windows.Forms.Label()
        Me.dlgExport = New System.Windows.Forms.OpenFileDialog()
        Me.dlgSaveDb = New System.Windows.Forms.SaveFileDialog()
        Me.dlgImport = New System.Windows.Forms.OpenFileDialog()
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
        Me.cmbUnitSelectGroup.Location = New System.Drawing.Point(6, 6)
        Me.cmbUnitSelectGroup.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmbUnitSelectGroup.Name = "cmbUnitSelectGroup"
        Me.cmbUnitSelectGroup.Size = New System.Drawing.Size(704, 33)
        Me.cmbUnitSelectGroup.TabIndex = 1
        Me.cmbUnitSelectGroup.Text = "#"
        '
        'lblUnitInfo
        '
        Me.lblUnitInfo.Location = New System.Drawing.Point(306, 108)
        Me.lblUnitInfo.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblUnitInfo.Name = "lblUnitInfo"
        Me.lblUnitInfo.Size = New System.Drawing.Size(236, 169)
        Me.lblUnitInfo.TabIndex = 15
        Me.lblUnitInfo.Text = "#"
        '
        'cmdUnitDelete
        '
        Me.cmdUnitDelete.Enabled = False
        Me.cmdUnitDelete.Location = New System.Drawing.Point(554, 223)
        Me.cmdUnitDelete.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdUnitDelete.Name = "cmdUnitDelete"
        Me.cmdUnitDelete.Size = New System.Drawing.Size(160, 46)
        Me.cmdUnitDelete.TabIndex = 6
        Me.cmdUnitDelete.Text = "Löschen"
        '
        'cmdUnitEdit
        '
        Me.cmdUnitEdit.Location = New System.Drawing.Point(554, 165)
        Me.cmdUnitEdit.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdUnitEdit.Name = "cmdUnitEdit"
        Me.cmdUnitEdit.Size = New System.Drawing.Size(160, 46)
        Me.cmdUnitEdit.TabIndex = 5
        Me.cmdUnitEdit.Text = "Ändern"
        '
        'cmdGroupDelete
        '
        Me.cmdGroupDelete.Location = New System.Drawing.Point(554, 177)
        Me.cmdGroupDelete.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdGroupDelete.Name = "cmdGroupDelete"
        Me.cmdGroupDelete.Size = New System.Drawing.Size(160, 46)
        Me.cmdGroupDelete.TabIndex = 5
        Me.cmdGroupDelete.Text = "Löschen"
        '
        'cmdGroupEdit
        '
        Me.cmdGroupEdit.Location = New System.Drawing.Point(554, 119)
        Me.cmdGroupEdit.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdGroupEdit.Name = "cmdGroupEdit"
        Me.cmdGroupEdit.Size = New System.Drawing.Size(160, 46)
        Me.cmdGroupEdit.TabIndex = 4
        Me.cmdGroupEdit.Text = "Ändern"
        '
        'txtGroupName
        '
        Me.txtGroupName.Location = New System.Drawing.Point(306, 6)
        Me.txtGroupName.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.txtGroupName.Name = "txtGroupName"
        Me.txtGroupName.Size = New System.Drawing.Size(404, 31)
        Me.txtGroupName.TabIndex = 2
        Me.txtGroupName.Text = "#"
        '
        'txtUnitName
        '
        Me.txtUnitName.Location = New System.Drawing.Point(306, 58)
        Me.txtUnitName.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.txtUnitName.Name = "txtUnitName"
        Me.txtUnitName.Size = New System.Drawing.Size(404, 31)
        Me.txtUnitName.TabIndex = 3
        Me.txtUnitName.Text = "#"
        '
        'lstUnitList
        '
        Me.lstUnitList.ItemHeight = 25
        Me.lstUnitList.Location = New System.Drawing.Point(6, 58)
        Me.lstUnitList.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.lstUnitList.Name = "lstUnitList"
        Me.lstUnitList.Size = New System.Drawing.Size(284, 379)
        Me.lstUnitList.TabIndex = 2
        '
        'lblGroupInfo
        '
        Me.lblGroupInfo.Location = New System.Drawing.Point(306, 50)
        Me.lblGroupInfo.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblGroupInfo.Name = "lblGroupInfo"
        Me.lblGroupInfo.Size = New System.Drawing.Size(236, 169)
        Me.lblGroupInfo.TabIndex = 16
        Me.lblGroupInfo.Text = "#"
        '
        'tabUnit
        '
        Me.tabUnit.Controls.Add(Me.cmdUnitUp)
        Me.tabUnit.Controls.Add(Me.cmdUnitDown)
        Me.tabUnit.Controls.Add(Me.cmbUnitSelectGroup)
        Me.tabUnit.Controls.Add(Me.lblUnitInfo)
        Me.tabUnit.Controls.Add(Me.cmdUnitDelete)
        Me.tabUnit.Controls.Add(Me.cmdUnitEdit)
        Me.tabUnit.Controls.Add(Me.txtUnitName)
        Me.tabUnit.Controls.Add(Me.lstUnitList)
        Me.tabUnit.Controls.Add(Me.cmdUnitAdd)
        Me.tabUnit.Location = New System.Drawing.Point(8, 39)
        Me.tabUnit.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.tabUnit.Name = "tabUnit"
        Me.tabUnit.Size = New System.Drawing.Size(720, 461)
        Me.tabUnit.TabIndex = 2
        Me.tabUnit.Text = "Lektionen"
        Me.tabUnit.UseVisualStyleBackColor = True
        '
        'cmdUnitUp
        '
        Me.cmdUnitUp.Location = New System.Drawing.Point(392, 283)
        Me.cmdUnitUp.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdUnitUp.Name = "cmdUnitUp"
        Me.cmdUnitUp.Size = New System.Drawing.Size(150, 44)
        Me.cmdUnitUp.TabIndex = 17
        Me.cmdUnitUp.Text = "#"
        Me.cmdUnitUp.UseVisualStyleBackColor = True
        '
        'cmdUnitDown
        '
        Me.cmdUnitDown.Location = New System.Drawing.Point(554, 281)
        Me.cmdUnitDown.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdUnitDown.Name = "cmdUnitDown"
        Me.cmdUnitDown.Size = New System.Drawing.Size(150, 44)
        Me.cmdUnitDown.TabIndex = 16
        Me.cmdUnitDown.Text = "#"
        Me.cmdUnitDown.UseVisualStyleBackColor = True
        '
        'cmdUnitAdd
        '
        Me.cmdUnitAdd.Location = New System.Drawing.Point(554, 108)
        Me.cmdUnitAdd.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdUnitAdd.Name = "cmdUnitAdd"
        Me.cmdUnitAdd.Size = New System.Drawing.Size(160, 46)
        Me.cmdUnitAdd.TabIndex = 4
        Me.cmdUnitAdd.Text = "Hinzufügen"
        '
        'lstGroupList
        '
        Me.lstGroupList.ItemHeight = 25
        Me.lstGroupList.Location = New System.Drawing.Point(6, 6)
        Me.lstGroupList.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.lstGroupList.Name = "lstGroupList"
        Me.lstGroupList.Size = New System.Drawing.Size(284, 429)
        Me.lstGroupList.TabIndex = 1
        '
        'cmdGroupAdd
        '
        Me.cmdGroupAdd.Location = New System.Drawing.Point(554, 62)
        Me.cmdGroupAdd.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdGroupAdd.Name = "cmdGroupAdd"
        Me.cmdGroupAdd.Size = New System.Drawing.Size(160, 46)
        Me.cmdGroupAdd.TabIndex = 3
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
        Me.tabGroup.Location = New System.Drawing.Point(8, 39)
        Me.tabGroup.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.tabGroup.Name = "tabGroup"
        Me.tabGroup.Size = New System.Drawing.Size(720, 461)
        Me.tabGroup.TabIndex = 1
        Me.tabGroup.Text = "Gruppen"
        Me.tabGroup.UseVisualStyleBackColor = True
        '
        'cmdSchließen
        '
        Me.cmdSchließen.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdSchließen.Location = New System.Drawing.Point(594, 527)
        Me.cmdSchließen.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdSchließen.Name = "cmdSchließen"
        Me.cmdSchließen.Size = New System.Drawing.Size(150, 44)
        Me.cmdSchließen.TabIndex = 20
        Me.cmdSchließen.Text = "Schließen"
        '
        'tab
        '
        Me.tab.Controls.Add(Me.tabGroup)
        Me.tab.Controls.Add(Me.tabUnit)
        Me.tab.Controls.Add(Me.tabImport)
        Me.tab.Controls.Add(Me.tabExport)
        Me.tab.Controls.Add(Me.tapDatabase)
        Me.tab.Location = New System.Drawing.Point(16, 15)
        Me.tab.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.tab.Name = "tab"
        Me.tab.SelectedIndex = 0
        Me.tab.Size = New System.Drawing.Size(736, 508)
        Me.tab.TabIndex = 5
        '
        'tabImport
        '
        Me.tabImport.Controls.Add(Me.Label1)
        Me.tabImport.Controls.Add(Me.lblImportGroupCount)
        Me.tabImport.Controls.Add(Me.optGroupAppend)
        Me.tabImport.Controls.Add(Me.optGroupOverwrite)
        Me.tabImport.Controls.Add(Me.lblImportDictCount)
        Me.tabImport.Controls.Add(Me.chkImportStatistic)
        Me.tabImport.Controls.Add(Me.cmdImportGroup)
        Me.tabImport.Controls.Add(Me.cmdImportDictionary)
        Me.tabImport.Controls.Add(Me.lblImportDB)
        Me.tabImport.Controls.Add(Me.cmdImortSelectDB)
        Me.tabImport.Location = New System.Drawing.Point(8, 39)
        Me.tabImport.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.tabImport.Name = "tabImport"
        Me.tabImport.Size = New System.Drawing.Size(720, 461)
        Me.tabImport.TabIndex = 0
        Me.tabImport.Text = "Importieren"
        Me.tabImport.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(6, 373)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(708, 85)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Das Importieren kann einige Zeit dauern, da jeder Datensatz einzeln eingelesen un" &
    "d dabei auf Kohärenz geprüft wird."
        '
        'lblImportGroupCount
        '
        Me.lblImportGroupCount.AutoSize = True
        Me.lblImportGroupCount.Location = New System.Drawing.Point(6, 265)
        Me.lblImportGroupCount.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblImportGroupCount.Name = "lblImportGroupCount"
        Me.lblImportGroupCount.Size = New System.Drawing.Size(24, 25)
        Me.lblImportGroupCount.TabIndex = 29
        Me.lblImportGroupCount.Text = "#"
        '
        'optGroupAppend
        '
        Me.optGroupAppend.AutoSize = True
        Me.optGroupAppend.Location = New System.Drawing.Point(6, 150)
        Me.optGroupAppend.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.optGroupAppend.Name = "optGroupAppend"
        Me.optGroupAppend.Size = New System.Drawing.Size(238, 29)
        Me.optGroupAppend.TabIndex = 3
        Me.optGroupAppend.Text = "Gruppen hinzufügen"
        Me.optGroupAppend.UseVisualStyleBackColor = True
        '
        'optGroupOverwrite
        '
        Me.optGroupOverwrite.AutoSize = True
        Me.optGroupOverwrite.Checked = True
        Me.optGroupOverwrite.Location = New System.Drawing.Point(6, 106)
        Me.optGroupOverwrite.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.optGroupOverwrite.Name = "optGroupOverwrite"
        Me.optGroupOverwrite.Size = New System.Drawing.Size(260, 29)
        Me.optGroupOverwrite.TabIndex = 2
        Me.optGroupOverwrite.TabStop = True
        Me.optGroupOverwrite.Text = "Überschreibe Gruppen"
        Me.optGroupOverwrite.UseVisualStyleBackColor = True
        '
        'lblImportDictCount
        '
        Me.lblImportDictCount.AutoSize = True
        Me.lblImportDictCount.Location = New System.Drawing.Point(354, 265)
        Me.lblImportDictCount.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblImportDictCount.Name = "lblImportDictCount"
        Me.lblImportDictCount.Size = New System.Drawing.Size(24, 25)
        Me.lblImportDictCount.TabIndex = 26
        Me.lblImportDictCount.Text = "#"
        '
        'chkImportStatistic
        '
        Me.chkImportStatistic.AutoSize = True
        Me.chkImportStatistic.Location = New System.Drawing.Point(6, 62)
        Me.chkImportStatistic.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.chkImportStatistic.Name = "chkImportStatistic"
        Me.chkImportStatistic.Size = New System.Drawing.Size(233, 29)
        Me.chkImportStatistic.TabIndex = 1
        Me.chkImportStatistic.Text = "Statistik importieren"
        Me.chkImportStatistic.UseVisualStyleBackColor = True
        '
        'cmdImportGroup
        '
        Me.cmdImportGroup.Location = New System.Drawing.Point(6, 194)
        Me.cmdImportGroup.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdImportGroup.Name = "cmdImportGroup"
        Me.cmdImportGroup.Size = New System.Drawing.Size(336, 44)
        Me.cmdImportGroup.TabIndex = 4
        Me.cmdImportGroup.Text = "Importiere Gruppe"
        Me.cmdImportGroup.UseVisualStyleBackColor = True
        '
        'cmdImportDictionary
        '
        Me.cmdImportDictionary.Location = New System.Drawing.Point(354, 194)
        Me.cmdImportDictionary.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdImportDictionary.Name = "cmdImportDictionary"
        Me.cmdImportDictionary.Size = New System.Drawing.Size(336, 44)
        Me.cmdImportDictionary.TabIndex = 5
        Me.cmdImportDictionary.Text = "Imporiere Wörterbuch"
        Me.cmdImportDictionary.UseVisualStyleBackColor = True
        '
        'lblImportDB
        '
        Me.lblImportDB.Location = New System.Drawing.Point(354, 6)
        Me.lblImportDB.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblImportDB.Name = "lblImportDB"
        Me.lblImportDB.Size = New System.Drawing.Size(360, 129)
        Me.lblImportDB.TabIndex = 1
        Me.lblImportDB.Text = "#"
        '
        'cmdImortSelectDB
        '
        Me.cmdImortSelectDB.Location = New System.Drawing.Point(6, 6)
        Me.cmdImortSelectDB.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdImortSelectDB.Name = "cmdImortSelectDB"
        Me.cmdImortSelectDB.Size = New System.Drawing.Size(336, 44)
        Me.cmdImortSelectDB.TabIndex = 0
        Me.cmdImortSelectDB.Text = "Datei auswählen"
        '
        'tabExport
        '
        Me.tabExport.Controls.Add(Me.Label2)
        Me.tabExport.Controls.Add(Me.cmdExportUserData)
        Me.tabExport.Controls.Add(Me.chkExportStats)
        Me.tabExport.Controls.Add(Me.chkExportEmptyEntrys)
        Me.tabExport.Controls.Add(Me.lstExportLanguages)
        Me.tabExport.Controls.Add(Me.lstExportGroups)
        Me.tabExport.Controls.Add(Me.cmdExport)
        Me.tabExport.Location = New System.Drawing.Point(8, 39)
        Me.tabExport.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.tabExport.Name = "tabExport"
        Me.tabExport.Size = New System.Drawing.Size(720, 461)
        Me.tabExport.TabIndex = 4
        Me.tabExport.Text = "Exportieren"
        Me.tabExport.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 256)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(708, 67)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Das Importieren kann einige Zeit dauern, da jeder Datensatz einzeln eingelesen un" &
    "d dabei auf Kohärenz geprüft wird."
        '
        'cmdExportUserData
        '
        Me.cmdExportUserData.Location = New System.Drawing.Point(378, 348)
        Me.cmdExportUserData.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdExportUserData.Name = "cmdExportUserData"
        Me.cmdExportUserData.Size = New System.Drawing.Size(336, 44)
        Me.cmdExportUserData.TabIndex = 4
        Me.cmdExportUserData.Text = "Benutzerdaten exportieren"
        Me.cmdExportUserData.UseVisualStyleBackColor = True
        '
        'chkExportStats
        '
        Me.chkExportStats.AutoSize = True
        Me.chkExportStats.Location = New System.Drawing.Point(6, 198)
        Me.chkExportStats.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.chkExportStats.Name = "chkExportStats"
        Me.chkExportStats.Size = New System.Drawing.Size(230, 29)
        Me.chkExportStats.TabIndex = 2
        Me.chkExportStats.Text = "Statistik mit sichern"
        Me.chkExportStats.UseVisualStyleBackColor = True
        '
        'chkExportEmptyEntrys
        '
        Me.chkExportEmptyEntrys.AutoSize = True
        Me.chkExportEmptyEntrys.Location = New System.Drawing.Point(366, 198)
        Me.chkExportEmptyEntrys.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.chkExportEmptyEntrys.Name = "chkExportEmptyEntrys"
        Me.chkExportEmptyEntrys.Size = New System.Drawing.Size(337, 29)
        Me.chkExportEmptyEntrys.TabIndex = 3
        Me.chkExportEmptyEntrys.Text = "leere Haupteinträge auslassen"
        Me.chkExportEmptyEntrys.UseVisualStyleBackColor = True
        '
        'lstExportLanguages
        '
        Me.lstExportLanguages.FormattingEnabled = True
        Me.lstExportLanguages.Location = New System.Drawing.Point(6, 6)
        Me.lstExportLanguages.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.lstExportLanguages.Name = "lstExportLanguages"
        Me.lstExportLanguages.Size = New System.Drawing.Size(344, 160)
        Me.lstExportLanguages.TabIndex = 0
        '
        'lstExportGroups
        '
        Me.lstExportGroups.FormattingEnabled = True
        Me.lstExportGroups.Location = New System.Drawing.Point(366, 6)
        Me.lstExportGroups.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.lstExportGroups.Name = "lstExportGroups"
        Me.lstExportGroups.Size = New System.Drawing.Size(344, 160)
        Me.lstExportGroups.TabIndex = 1
        '
        'cmdExport
        '
        Me.cmdExport.Location = New System.Drawing.Point(378, 408)
        Me.cmdExport.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.Size = New System.Drawing.Size(336, 44)
        Me.cmdExport.TabIndex = 5
        Me.cmdExport.Text = "Exportieren"
        '
        'tapDatabase
        '
        Me.tapDatabase.Controls.Add(Me.lblErrorCount)
        Me.tapDatabase.Controls.Add(Me.cmdSaveDB)
        Me.tapDatabase.Controls.Add(Me.cmdReorganizeDB)
        Me.tapDatabase.Controls.Add(Me.cmdDBVersion)
        Me.tapDatabase.Controls.Add(Me.lblDBVersion)
        Me.tapDatabase.Location = New System.Drawing.Point(8, 39)
        Me.tapDatabase.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.tapDatabase.Name = "tapDatabase"
        Me.tapDatabase.Size = New System.Drawing.Size(720, 461)
        Me.tapDatabase.TabIndex = 3
        Me.tapDatabase.Text = "Datenbank"
        Me.tapDatabase.UseVisualStyleBackColor = True
        '
        'lblErrorCount
        '
        Me.lblErrorCount.AutoSize = True
        Me.lblErrorCount.Location = New System.Drawing.Point(6, 158)
        Me.lblErrorCount.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblErrorCount.Name = "lblErrorCount"
        Me.lblErrorCount.Size = New System.Drawing.Size(24, 25)
        Me.lblErrorCount.TabIndex = 31
        Me.lblErrorCount.Text = "#"
        '
        'cmdSaveDB
        '
        Me.cmdSaveDB.Location = New System.Drawing.Point(6, 408)
        Me.cmdSaveDB.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdSaveDB.Name = "cmdSaveDB"
        Me.cmdSaveDB.Size = New System.Drawing.Size(336, 44)
        Me.cmdSaveDB.TabIndex = 2
        Me.cmdSaveDB.Text = "Datenbank sichern"
        Me.cmdSaveDB.UseVisualStyleBackColor = True
        '
        'cmdReorganizeDB
        '
        Me.cmdReorganizeDB.Location = New System.Drawing.Point(12, 108)
        Me.cmdReorganizeDB.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdReorganizeDB.Name = "cmdReorganizeDB"
        Me.cmdReorganizeDB.Size = New System.Drawing.Size(336, 44)
        Me.cmdReorganizeDB.TabIndex = 1
        Me.cmdReorganizeDB.Text = "Konsistenz prüfen"
        Me.cmdReorganizeDB.UseVisualStyleBackColor = True
        '
        'cmdDBVersion
        '
        Me.cmdDBVersion.Enabled = False
        Me.cmdDBVersion.Location = New System.Drawing.Point(12, 52)
        Me.cmdDBVersion.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmdDBVersion.Name = "cmdDBVersion"
        Me.cmdDBVersion.Size = New System.Drawing.Size(336, 44)
        Me.cmdDBVersion.TabIndex = 0
        Me.cmdDBVersion.Text = "#"
        '
        'lblDBVersion
        '
        Me.lblDBVersion.Location = New System.Drawing.Point(6, 0)
        Me.lblDBVersion.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblDBVersion.Name = "lblDBVersion"
        Me.lblDBVersion.Size = New System.Drawing.Size(380, 46)
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
        'Management
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdSchließen
        Me.ClientSize = New System.Drawing.Size(764, 585)
        Me.Controls.Add(Me.cmdSchließen)
        Me.Controls.Add(Me.tab)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
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
  Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents cmdUnitUp As System.Windows.Forms.Button
	Friend WithEvents cmdUnitDown As System.Windows.Forms.Button
End Class
