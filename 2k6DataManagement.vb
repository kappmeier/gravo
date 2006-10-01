Public Class Management
	Inherits System.Windows.Forms.Form

	' Datenbank-Zugriff
	Dim voc As xlsDBManagement	' Zugriff auf Vokabel-Datenbank
  Dim ldf As xlsLDFManagement  ' Zugriff auf Languages die im LDF-System vorhanden sind
  Dim hashGroups As Hashtable ' GroupDescription zu GroupTable zuordnen

#Region " Windows Form Designer generated code "

  Public Sub New()
    MyBase.New()

    'This call is required by the Windows Form Designer.
    InitializeComponent()

    'Add any initialization after the InitializeComponent() call

  End Sub

  'Form overrides dispose to clean up the component list.
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing Then
      If Not (components Is Nothing) Then
        components.Dispose()
      End If
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  Friend WithEvents tab As System.Windows.Forms.TabControl
  Friend WithEvents tabData As System.Windows.Forms.TabPage
  Friend WithEvents SaveFile As System.Windows.Forms.SaveFileDialog
  Friend WithEvents OpenFile As System.Windows.Forms.OpenFileDialog
  Friend WithEvents lblSelectedDB As System.Windows.Forms.Label
  Friend WithEvents cmdDataDBVersion As System.Windows.Forms.Button
  Friend WithEvents cmdDataSaveUnit As System.Windows.Forms.Button
  Friend WithEvents cmdDataSelectSaveDB As System.Windows.Forms.Button
  Friend WithEvents cmbDataDBSelection As System.Windows.Forms.ComboBox
  Friend WithEvents chkDataOverwrite As System.Windows.Forms.CheckBox
  Friend WithEvents chkDataAddOnly As System.Windows.Forms.CheckBox
  Friend WithEvents lblDataDBVersion As System.Windows.Forms.Label
  Friend WithEvents cmdSchließen As System.Windows.Forms.Button
  Friend WithEvents lstGroupList As System.Windows.Forms.ListBox
  Friend WithEvents cmdGroupDelete As System.Windows.Forms.Button
  Friend WithEvents cmdGroupEdit As System.Windows.Forms.Button
  Friend WithEvents cmdGroupAdd As System.Windows.Forms.Button
  Friend WithEvents txtGroupName As System.Windows.Forms.TextBox
  Friend WithEvents cmbGroupLanguage As System.Windows.Forms.ComboBox
  Friend WithEvents lstUnitList As System.Windows.Forms.ListBox
  Friend WithEvents cmdUnitDelete As System.Windows.Forms.Button
  Friend WithEvents cmdUnitEdit As System.Windows.Forms.Button
  Friend WithEvents cmdUnitAdd As System.Windows.Forms.Button
  Friend WithEvents cmbUnitSelectGroup As System.Windows.Forms.ComboBox
  Friend WithEvents txtUnitName As System.Windows.Forms.TextBox
  Friend WithEvents lblUnitInfo As System.Windows.Forms.Label
  Friend WithEvents tabGroup As System.Windows.Forms.TabPage
  Friend WithEvents tabUnit As System.Windows.Forms.TabPage
  Friend WithEvents optImport As System.Windows.Forms.RadioButton
  Friend WithEvents optExport As System.Windows.Forms.RadioButton
  Friend WithEvents cmbLDFType As System.Windows.Forms.ComboBox
  Friend WithEvents cmdReorganizeDB As System.Windows.Forms.Button
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.tab = New System.Windows.Forms.TabControl
    Me.tabData = New System.Windows.Forms.TabPage
    Me.cmdReorganizeDB = New System.Windows.Forms.Button
    Me.optExport = New System.Windows.Forms.RadioButton
    Me.optImport = New System.Windows.Forms.RadioButton
    Me.lblDataDBVersion = New System.Windows.Forms.Label
    Me.cmdDataDBVersion = New System.Windows.Forms.Button
    Me.chkDataOverwrite = New System.Windows.Forms.CheckBox
    Me.chkDataAddOnly = New System.Windows.Forms.CheckBox
    Me.cmdDataSaveUnit = New System.Windows.Forms.Button
    Me.cmbDataDBSelection = New System.Windows.Forms.ComboBox
    Me.lblSelectedDB = New System.Windows.Forms.Label
    Me.cmdDataSelectSaveDB = New System.Windows.Forms.Button
    Me.tabGroup = New System.Windows.Forms.TabPage
    Me.cmbLDFType = New System.Windows.Forms.ComboBox
    Me.cmdGroupDelete = New System.Windows.Forms.Button
    Me.cmbGroupLanguage = New System.Windows.Forms.ComboBox
    Me.cmdGroupEdit = New System.Windows.Forms.Button
    Me.txtGroupName = New System.Windows.Forms.TextBox
    Me.lstGroupList = New System.Windows.Forms.ListBox
    Me.cmdGroupAdd = New System.Windows.Forms.Button
    Me.tabUnit = New System.Windows.Forms.TabPage
    Me.cmbUnitSelectGroup = New System.Windows.Forms.ComboBox
    Me.lblUnitInfo = New System.Windows.Forms.Label
    Me.cmdUnitDelete = New System.Windows.Forms.Button
    Me.cmdUnitEdit = New System.Windows.Forms.Button
    Me.txtUnitName = New System.Windows.Forms.TextBox
    Me.lstUnitList = New System.Windows.Forms.ListBox
    Me.cmdUnitAdd = New System.Windows.Forms.Button
    Me.cmdSchließen = New System.Windows.Forms.Button
    Me.SaveFile = New System.Windows.Forms.SaveFileDialog
    Me.OpenFile = New System.Windows.Forms.OpenFileDialog
    Me.tab.SuspendLayout()
    Me.tabData.SuspendLayout()
    Me.tabGroup.SuspendLayout()
    Me.tabUnit.SuspendLayout()
    Me.SuspendLayout()
    '
    'tab
    '
    Me.tab.Controls.Add(Me.tabData)
    Me.tab.Controls.Add(Me.tabGroup)
    Me.tab.Controls.Add(Me.tabUnit)
    Me.tab.Location = New System.Drawing.Point(8, 8)
    Me.tab.Name = "tab"
    Me.tab.SelectedIndex = 0
    Me.tab.Size = New System.Drawing.Size(368, 264)
    Me.tab.TabIndex = 1
    '
    'tabData
    '
    Me.tabData.Controls.Add(Me.cmdReorganizeDB)
    Me.tabData.Controls.Add(Me.optExport)
    Me.tabData.Controls.Add(Me.optImport)
    Me.tabData.Controls.Add(Me.lblDataDBVersion)
    Me.tabData.Controls.Add(Me.cmdDataDBVersion)
    Me.tabData.Controls.Add(Me.chkDataOverwrite)
    Me.tabData.Controls.Add(Me.chkDataAddOnly)
    Me.tabData.Controls.Add(Me.cmdDataSaveUnit)
    Me.tabData.Controls.Add(Me.cmbDataDBSelection)
    Me.tabData.Controls.Add(Me.lblSelectedDB)
    Me.tabData.Controls.Add(Me.cmdDataSelectSaveDB)
    Me.tabData.Location = New System.Drawing.Point(4, 22)
    Me.tabData.Name = "tabData"
    Me.tabData.Size = New System.Drawing.Size(360, 238)
    Me.tabData.TabIndex = 0
    Me.tabData.Text = "Daten"
    '
    'cmdReorganizeDB
    '
    Me.cmdReorganizeDB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.cmdReorganizeDB.Location = New System.Drawing.Point(176, 172)
    Me.cmdReorganizeDB.Name = "cmdReorganizeDB"
    Me.cmdReorganizeDB.Size = New System.Drawing.Size(168, 23)
    Me.cmdReorganizeDB.TabIndex = 26
    Me.cmdReorganizeDB.Text = "Datenbank reorganisieren"
    Me.cmdReorganizeDB.UseVisualStyleBackColor = True
    '
    'optExport
    '
    Me.optExport.Checked = True
    Me.optExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.optExport.Location = New System.Drawing.Point(16, 56)
    Me.optExport.Name = "optExport"
    Me.optExport.Size = New System.Drawing.Size(104, 16)
    Me.optExport.TabIndex = 25
    Me.optExport.TabStop = True
    Me.optExport.Text = "Exportieren"
    '
    'optImport
    '
    Me.optImport.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.optImport.Location = New System.Drawing.Point(16, 80)
    Me.optImport.Name = "optImport"
    Me.optImport.Size = New System.Drawing.Size(104, 16)
    Me.optImport.TabIndex = 24
    Me.optImport.Text = "Importieren"
    '
    'lblDataDBVersion
    '
    Me.lblDataDBVersion.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.lblDataDBVersion.Location = New System.Drawing.Point(8, 208)
    Me.lblDataDBVersion.Name = "lblDataDBVersion"
    Me.lblDataDBVersion.Size = New System.Drawing.Size(152, 24)
    Me.lblDataDBVersion.TabIndex = 23
    Me.lblDataDBVersion.Text = "Aktuelle Datenbank-Version:"
    Me.lblDataDBVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'cmdDataDBVersion
    '
    Me.cmdDataDBVersion.Enabled = False
    Me.cmdDataDBVersion.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.cmdDataDBVersion.Location = New System.Drawing.Point(176, 208)
    Me.cmdDataDBVersion.Name = "cmdDataDBVersion"
    Me.cmdDataDBVersion.Size = New System.Drawing.Size(168, 23)
    Me.cmdDataDBVersion.TabIndex = 22
    Me.cmdDataDBVersion.Text = "Datenbank-Version angleichen"
    '
    'chkDataOverwrite
    '
    Me.chkDataOverwrite.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.chkDataOverwrite.Location = New System.Drawing.Point(8, 160)
    Me.chkDataOverwrite.Name = "chkDataOverwrite"
    Me.chkDataOverwrite.Size = New System.Drawing.Size(160, 16)
    Me.chkDataOverwrite.TabIndex = 21
    Me.chkDataOverwrite.Text = "Vorhandene überschreiben"
    '
    'chkDataAddOnly
    '
    Me.chkDataAddOnly.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.chkDataAddOnly.Location = New System.Drawing.Point(8, 136)
    Me.chkDataAddOnly.Name = "chkDataAddOnly"
    Me.chkDataAddOnly.Size = New System.Drawing.Size(128, 16)
    Me.chkDataAddOnly.TabIndex = 20
    Me.chkDataAddOnly.Text = "Nur neue anhängen"
    '
    'cmdDataSaveUnit
    '
    Me.cmdDataSaveUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.cmdDataSaveUnit.Location = New System.Drawing.Point(176, 136)
    Me.cmdDataSaveUnit.Name = "cmdDataSaveUnit"
    Me.cmdDataSaveUnit.Size = New System.Drawing.Size(168, 23)
    Me.cmdDataSaveUnit.TabIndex = 19
    Me.cmdDataSaveUnit.Text = "Gruppe sichern"
    '
    'cmbDataDBSelection
    '
    Me.cmbDataDBSelection.Location = New System.Drawing.Point(8, 104)
    Me.cmbDataDBSelection.Name = "cmbDataDBSelection"
    Me.cmbDataDBSelection.Size = New System.Drawing.Size(336, 21)
    Me.cmbDataDBSelection.TabIndex = 18
    '
    'lblSelectedDB
    '
    Me.lblSelectedDB.Location = New System.Drawing.Point(152, 16)
    Me.lblSelectedDB.Name = "lblSelectedDB"
    Me.lblSelectedDB.Size = New System.Drawing.Size(184, 40)
    Me.lblSelectedDB.TabIndex = 1
    Me.lblSelectedDB.Text = "Datenbank: noch keine gewählt"
    '
    'cmdDataSelectSaveDB
    '
    Me.cmdDataSelectSaveDB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.cmdDataSelectSaveDB.Location = New System.Drawing.Point(16, 16)
    Me.cmdDataSelectSaveDB.Name = "cmdDataSelectSaveDB"
    Me.cmdDataSelectSaveDB.Size = New System.Drawing.Size(128, 32)
    Me.cmdDataSelectSaveDB.TabIndex = 0
    Me.cmdDataSelectSaveDB.Text = "Zieldatei auswählen"
    '
    'tabGroup
    '
    Me.tabGroup.Controls.Add(Me.cmbLDFType)
    Me.tabGroup.Controls.Add(Me.cmdGroupDelete)
    Me.tabGroup.Controls.Add(Me.cmbGroupLanguage)
    Me.tabGroup.Controls.Add(Me.cmdGroupEdit)
    Me.tabGroup.Controls.Add(Me.txtGroupName)
    Me.tabGroup.Controls.Add(Me.lstGroupList)
    Me.tabGroup.Controls.Add(Me.cmdGroupAdd)
    Me.tabGroup.Location = New System.Drawing.Point(4, 22)
    Me.tabGroup.Name = "tabGroup"
    Me.tabGroup.Size = New System.Drawing.Size(360, 238)
    Me.tabGroup.TabIndex = 1
    Me.tabGroup.Text = "Gruppen"
    '
    'cmbLDFType
    '
    Me.cmbLDFType.Location = New System.Drawing.Point(168, 48)
    Me.cmbLDFType.Name = "cmbLDFType"
    Me.cmbLDFType.Size = New System.Drawing.Size(176, 21)
    Me.cmbLDFType.TabIndex = 8
        Me.cmbLDFType.Text = "std"
        '
        'cmdGroupDelete
        '
        Me.cmdGroupDelete.Enabled = False
        Me.cmdGroupDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdGroupDelete.Location = New System.Drawing.Point(264, 200)
        Me.cmdGroupDelete.Name = "cmdGroupDelete"
        Me.cmdGroupDelete.Size = New System.Drawing.Size(80, 24)
        Me.cmdGroupDelete.TabIndex = 6
        Me.cmdGroupDelete.Text = "Löschen"
        '
        'cmbGroupLanguage
        '
        Me.cmbGroupLanguage.Location = New System.Drawing.Point(168, 16)
        Me.cmbGroupLanguage.Name = "cmbGroupLanguage"
        Me.cmbGroupLanguage.Size = New System.Drawing.Size(176, 21)
        Me.cmbGroupLanguage.TabIndex = 4
        Me.cmbGroupLanguage.Text = "ComboBox1"
        '
        'cmdGroupEdit
        '
        Me.cmdGroupEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdGroupEdit.Location = New System.Drawing.Point(264, 144)
        Me.cmdGroupEdit.Name = "cmdGroupEdit"
        Me.cmdGroupEdit.Size = New System.Drawing.Size(80, 24)
        Me.cmdGroupEdit.TabIndex = 3
        Me.cmdGroupEdit.Text = "Ändern"
        '
        'txtGroupName
        '
        Me.txtGroupName.Location = New System.Drawing.Point(168, 80)
        Me.txtGroupName.Name = "txtGroupName"
        Me.txtGroupName.Size = New System.Drawing.Size(176, 20)
        Me.txtGroupName.TabIndex = 2
        Me.txtGroupName.Text = "#"
        '
        'lstGroupList
        '
        Me.lstGroupList.Location = New System.Drawing.Point(8, 16)
        Me.lstGroupList.Name = "lstGroupList"
        Me.lstGroupList.Size = New System.Drawing.Size(144, 212)
        Me.lstGroupList.TabIndex = 1
        '
        'cmdGroupAdd
        '
        Me.cmdGroupAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdGroupAdd.Location = New System.Drawing.Point(264, 112)
        Me.cmdGroupAdd.Name = "cmdGroupAdd"
        Me.cmdGroupAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdGroupAdd.TabIndex = 0
        Me.cmdGroupAdd.Text = "Hinzufügen"
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
        '
        'cmbUnitSelectGroup
        '
        Me.cmbUnitSelectGroup.Location = New System.Drawing.Point(8, 16)
        Me.cmbUnitSelectGroup.Name = "cmbUnitSelectGroup"
        Me.cmbUnitSelectGroup.Size = New System.Drawing.Size(336, 21)
        Me.cmbUnitSelectGroup.TabIndex = 16
        Me.cmbUnitSelectGroup.Text = "ComboBox1"
        '
        'lblUnitInfo
        '
        Me.lblUnitInfo.Location = New System.Drawing.Point(168, 80)
        Me.lblUnitInfo.Name = "lblUnitInfo"
        Me.lblUnitInfo.Size = New System.Drawing.Size(88, 88)
        Me.lblUnitInfo.TabIndex = 15
        Me.lblUnitInfo.Text = "#"
        '
        'cmdUnitDelete
        '
        Me.cmdUnitDelete.Enabled = False
        Me.cmdUnitDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdUnitDelete.Location = New System.Drawing.Point(264, 144)
        Me.cmdUnitDelete.Name = "cmdUnitDelete"
        Me.cmdUnitDelete.Size = New System.Drawing.Size(80, 24)
        Me.cmdUnitDelete.TabIndex = 14
        Me.cmdUnitDelete.Text = "Löschen"
        '
        'cmdUnitEdit
        '
        Me.cmdUnitEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdUnitEdit.Location = New System.Drawing.Point(264, 112)
        Me.cmdUnitEdit.Name = "cmdUnitEdit"
        Me.cmdUnitEdit.Size = New System.Drawing.Size(80, 24)
        Me.cmdUnitEdit.TabIndex = 11
        Me.cmdUnitEdit.Text = "Ändern"
        '
        'txtUnitName
        '
        Me.txtUnitName.Location = New System.Drawing.Point(168, 48)
        Me.txtUnitName.Name = "txtUnitName"
        Me.txtUnitName.Size = New System.Drawing.Size(176, 20)
        Me.txtUnitName.TabIndex = 10
        Me.txtUnitName.Text = "txtNameUnit"
        '
        'lstUnitList
        '
        Me.lstUnitList.Location = New System.Drawing.Point(8, 48)
        Me.lstUnitList.Name = "lstUnitList"
        Me.lstUnitList.Size = New System.Drawing.Size(144, 160)
        Me.lstUnitList.TabIndex = 9
        '
        'cmdUnitAdd
        '
        Me.cmdUnitAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdUnitAdd.Location = New System.Drawing.Point(264, 80)
        Me.cmdUnitAdd.Name = "cmdUnitAdd"
        Me.cmdUnitAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdUnitAdd.TabIndex = 8
        Me.cmdUnitAdd.Text = "Hinzufügen"
        '
        'cmdSchließen
        '
        Me.cmdSchließen.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdSchließen.Location = New System.Drawing.Point(296, 280)
        Me.cmdSchließen.Name = "cmdSchließen"
        Me.cmdSchließen.Size = New System.Drawing.Size(75, 23)
        Me.cmdSchließen.TabIndex = 2
        Me.cmdSchließen.Text = "Schließen"
        '
        'SaveFile
        '
        Me.SaveFile.CreatePrompt = True
        Me.SaveFile.FileName = "new.mdb"
        Me.SaveFile.Filter = "Database|*.mdb"
        Me.SaveFile.InitialDirectory = "C:\"
        Me.SaveFile.OverwritePrompt = False
        Me.SaveFile.Title = "Datei zum Speichern auswählen"
        '
        'Management
        '
        Me.AcceptButton = Me.cmdSchließen
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(384, 310)
        Me.Controls.Add(Me.cmdSchließen)
        Me.Controls.Add(Me.tab)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "Management"
        Me.ShowInTaskbar = False
        Me.Text = "Daten-Management"
        Me.tab.ResumeLayout(False)
        Me.tabData.ResumeLayout(False)
        Me.tabGroup.ResumeLayout(False)
        Me.tabGroup.PerformLayout()
        Me.tabUnit.ResumeLayout(False)
        Me.tabUnit.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub LoadForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer     ' Index
        Dim db As New AccessDatabaseOperation       ' Datenbankoperationen
        db.Open(Application.StartupPath() & "\voc.mdb")    ' Datenbank öffnen
        voc = New xlsDBManagement(db)     ' Datenbank zur Verfügung stellen
        hashGroups = New Hashtable

        ' Anzeigen der verfügbaren Sprachen
        ldf = New xlsLDFManagement
        For i = 1 To ldf.Languages.Count
            cmbGroupLanguage.Items.Add(ldf.Languages.Item(i).Name)      '.Description)
        Next i

        ' Anzeigen der verschiedenen LDF's. (geht ja nur eine ;) )
        cmbLDFType.Items.Add("std")
        cmbLDFType.SelectedIndex = 0
        UpdateForm()
    End Sub

  Private Sub UpdateForm()
    Dim i As Integer      ' Index
    ' Datenbank-Versionsinfo
    Me.lblDataDBVersion.Text = "Aktuelle Datenbank-Version:" & vbCrLf & voc.DatabaseVersion
    Me.cmdDataDBVersion.Text = "Auf Version " & voc.DatabaseVersion(voc.NextVersionIndex) & " updaten."
    If voc.DatabaseVersionIndex = 0 Then cmdDataDBVersion.Enabled = False Else cmdDataDBVersion.Enabled = True

    ' Gruppen in die Listen einfügen
    lstGroupList.Items.Clear()
    cmbUnitSelectGroup.Items.Clear()
    hashGroups.Clear()
    For i = 0 To voc.CountGroups - 1
      hashGroups.Add(voc.Groups.Item(i + 1).Description, voc.Groups.Item(i + 1).Table)
      lstGroupList.Items.Add(voc.Groups.Item(i + 1).Description)
      cmbUnitSelectGroup.Items.Add(voc.Groups.Item(i + 1).description)
    Next i
    If voc.CountGroups > 0 Then     ' Erstes Listelement auswählen
      lstGroupList.SelectedIndex = 0
      cmbUnitSelectGroup.SelectedIndex = 0
    End If
  End Sub

  Private Sub CloseForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSchließen.Click
    Me.Close()
  End Sub

  Private Sub ClosingForm(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    voc.Close()
  End Sub

  Private Sub DataSaveUnit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDataSaveUnit.Click
    'Dim progress As New SaveProgress
    'If bExport Then
    '	voc.SelectTable(voc.Groups(cmbDataDBSelection.SelectedIndex).Table)
    '	progress.Overwrite = Me.chkDataOverwrite.Checked
    '	progress.AddOnly = Me.chkDataAddOnly.Checked
    '	progress.DBPath = sDBPath
    '	progress.Show()
    '	Application.DoEvents()
    '	Do While progress.IsShown = False
    '	Loop
    '	progress.SetVoc(voc)
    '	progress.Save()
    '	progress.Close()
    'Else
    '	vocSave.SelectTable(vocSave.Groups(cmbDataDBSelection.SelectedIndex).Table)
    '	progress.Overwrite = True
    '	progress.AddOnly = False
    '	progress.DBPath = Application.StartupPath() & "\voc.mdb"
    '	progress.Show()
    '	Application.DoEvents()
    '	Do While progress.IsShown = False
    '	Loop
    '	progress.SetVoc(vocSave)
    '	progress.Save()
    '	progress.Close()
    'End If

    'Exit Sub
  End Sub

  Private Sub DataUpdateDBVersion(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDataDBVersion.Click
    voc.UpdateDatabaseVersion()
    UpdateForm()
  End Sub

  Private Sub DataSelectSaveDB(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDataSelectSaveDB.Click
    'Me.SaveFile.ShowDialog()
    'bDBChosen = True
    'Me.lblSelectedDB.Text = "Datenbank:" & vbCrLf & SaveFile.FileName
    'sDBPath = SaveFile.FileName
    'Try
    '	FileOpen(1, sDBPath, OpenMode.Input)
    '	FileClose(1)
    'Catch ex As IO.IOException When Err.Number = 53		 ' Datei existiert nicht, kopieren
    '	' Datei auf neueste version überprüfen
    '	vocSave = New xlsOldVoc(db)
    '	Do While vocSave.DatabaseVersionIndex <> 0
    '		vocSave.UpdateDatabaseVersion()
    '	Loop
    '	vocSave = Nothing
    '	FileCopy(Application.StartupPath() & "\new.mdb", SaveFile.FileName)
    'Catch es As IO.IOException When Err.Number = 75
    '	' nichts
    'Catch ex As Exception
    '	MsgBox(ex.Message & vbCrLf & Err.Number)
    'End Try
    '' Datenbank laden, Version aktualisieren
    'vocSave = New xlsOldVoc(db)
    'Do While vocSave.DatabaseVersionIndex <> 0
    '	vocSave.UpdateDatabaseVersion()
    'Loop
    'UpdateForm()
  End Sub

  Private Sub DataExportMode(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optExport.CheckedChanged
    'bExport = True
    'Me.cmdDataSelectSaveDB.Text = "Zieldatei wählen"
    'Me.cmdDataSaveUnit.Text = "Gruppe sichern"
    'UpdateForm()
  End Sub

  Private Sub DataImportMode(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optImport.CheckedChanged
    'bExport = False
    'Me.cmdDataSelectSaveDB.Text = "Quelldatei wählen"
    'Me.cmdDataSaveUnit.Text = "Gruppe importieren"
    'UpdateForm()
  End Sub

  Private Sub GroupAdd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGroupAdd.Click
    If Trim(Me.txtGroupName.Text) = "" Then Exit Sub
    voc.AddGroup(Me.txtGroupName.Text, Me.cmbGroupLanguage.SelectedItem, Me.cmbLDFType.SelectedItem)
    UpdateForm()
  End Sub

  Private Sub GroupEdit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGroupEdit.Click
    If Trim(Me.txtGroupName.Text) = "" Then Exit Sub
    MsgBox("Die Sprache kann nicht geändert Werden!")
    MsgBox("Es ist zur Zeit nicht möglich, den LDF-Typ zu ändern! Warten sie auf ein Update.")

    ' Ändern der Gruppen-Informationen in der Datenbank
    Dim sOldText As String = txtGroupName.Text
    voc.UpdateGroup(lstGroupList.SelectedItem, sOldText)

    UpdateForm()      ' Anzeige Aktualisieren
    Dim iIndex As Integer = lstGroupList.FindStringExact(sOldText)
    lstGroupList.SelectedIndex = iIndex
  End Sub

  Private Sub GroupDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGroupDelete.Click
    MsgBox("Leider nicht möglich!", vbInformation)
  End Sub

  Private Sub GroupSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstGroupList.SelectedIndexChanged
    If lstGroupList.SelectedIndex = -1 Then Exit Sub ' Irreguläre Werte abfangen
    voc.SelectGroup(hashGroups.Item(lstGroupList.SelectedItem))
    txtGroupName.Text = lstGroupList.SelectedItem     ' Text aktualisieren
    Me.cmbGroupLanguage.SelectedIndex = cmbGroupLanguage.FindStringExact(voc.CurrentGroup.Language)
  End Sub

  Private Sub ReorganizeDB(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReorganizeDB.Click
    voc.reorganize()
  End Sub

  Private Sub UnitAdd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnitAdd.Click
    If Trim(Me.txtUnitName.Text = "") Then Exit Sub
    voc.AddUnit(txtUnitName.Text)
    Me.lstUnitList.Items.Add(txtUnitName.Text)
    Me.lstUnitList.SelectedItem = txtUnitName.Text
  End Sub

  Private Sub UnitEdit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnitEdit.Click
    Dim iCurrentSelection As Integer = lstUnitList.SelectedIndex
    voc.UpdateUnit(lstUnitList.SelectedIndex + 1, txtUnitName.Text)
    Me.UnitSelectGroup(sender, e)
    lstUnitList.SelectedIndex = iCurrentSelection
  End Sub

  Private Sub UnitDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnitDelete.Click
    MsgBox("Leider ist es zur Zeit nicht möglich Units zu Löschen oder zu Verschieben.", vbInformation)
  End Sub

  Private Sub UnitSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstUnitList.SelectedIndexChanged
    Me.txtUnitName.Text = Me.lstUnitList.SelectedItem
  End Sub

  Private Sub UnitSelectGroup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnitSelectGroup.SelectedIndexChanged
    ' Lektionen anzeigen
    Dim i As Integer      ' Index
    lstUnitList.Items.Clear()     ' Liste leeren
    voc.SelectGroup(voc.Groups.Item(cmbUnitSelectGroup.SelectedIndex + 1).table)
    cmdUnitEdit.Enabled = voc.CountUnits <> 0
    If voc.CountUnits = 0 Then txtUnitName.Text = "" : Exit Sub
    voc.SelectUnit(1)
    For i = 0 To voc.CountUnits - 1
      lstUnitList.Items.Add(voc.GetUnitName(i + 1))
    Next i
    lstUnitList.SelectedIndex = 0
  End Sub
End Class
