Public Class Management
    Inherits System.Windows.Forms.Form

    Dim voc As New CWordTest(Application.StartupPath() & "\voc.mdb")
    Dim bDBChosen As Boolean = False
    Dim sDBPath As String = ""

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
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents tab As System.Windows.Forms.TabControl
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents lstGroups As System.Windows.Forms.ListBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents cmbLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents tabData As System.Windows.Forms.TabPage
    Friend WithEvents tabGroups As System.Windows.Forms.TabPage
    Friend WithEvents tabUnits As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lstUnits As System.Windows.Forms.ListBox
    Friend WithEvents cmdDeleteUnit As System.Windows.Forms.Button
    Friend WithEvents cmdEditUnit As System.Windows.Forms.Button
    Friend WithEvents cmdAddUnit As System.Windows.Forms.Button
    Friend WithEvents cmbGroupSelect As System.Windows.Forms.ComboBox
    Friend WithEvents txtNameUnit As System.Windows.Forms.TextBox
    Friend WithEvents SaveFile As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmdSelectSaveDB As System.Windows.Forms.Button
    Friend WithEvents lblSelectedDB As System.Windows.Forms.Label
    Friend WithEvents cmdSaveUnit As System.Windows.Forms.Button
    Friend WithEvents cmbGroupSelectSaveDB As System.Windows.Forms.ComboBox
    Friend WithEvents chkAddOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents lblDBVersion As System.Windows.Forms.Label
    Friend WithEvents cmdDBVersion As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.tab = New System.Windows.Forms.TabControl
        Me.tabData = New System.Windows.Forms.TabPage
        Me.lblDBVersion = New System.Windows.Forms.Label
        Me.cmdDBVersion = New System.Windows.Forms.Button
        Me.chkOverwrite = New System.Windows.Forms.CheckBox
        Me.chkAddOnly = New System.Windows.Forms.CheckBox
        Me.cmdSaveUnit = New System.Windows.Forms.Button
        Me.cmbGroupSelectSaveDB = New System.Windows.Forms.ComboBox
        Me.lblSelectedDB = New System.Windows.Forms.Label
        Me.cmdSelectSaveDB = New System.Windows.Forms.Button
        Me.tabGroups = New System.Windows.Forms.TabPage
        Me.lblInfo = New System.Windows.Forms.Label
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbLanguage = New System.Windows.Forms.ComboBox
        Me.cmdEdit = New System.Windows.Forms.Button
        Me.txtName = New System.Windows.Forms.TextBox
        Me.lstGroups = New System.Windows.Forms.ListBox
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.tabUnits = New System.Windows.Forms.TabPage
        Me.cmbGroupSelect = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdDeleteUnit = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmdEditUnit = New System.Windows.Forms.Button
        Me.txtNameUnit = New System.Windows.Forms.TextBox
        Me.lstUnits = New System.Windows.Forms.ListBox
        Me.cmdAddUnit = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.SaveFile = New System.Windows.Forms.SaveFileDialog
        Me.OpenFile = New System.Windows.Forms.OpenFileDialog
        Me.tab.SuspendLayout()
        Me.tabData.SuspendLayout()
        Me.tabGroups.SuspendLayout()
        Me.tabUnits.SuspendLayout()
        Me.SuspendLayout()
        '
        'tab
        '
        Me.tab.Controls.Add(Me.tabData)
        Me.tab.Controls.Add(Me.tabGroups)
        Me.tab.Controls.Add(Me.tabUnits)
        Me.tab.Location = New System.Drawing.Point(8, 8)
        Me.tab.Name = "tab"
        Me.tab.SelectedIndex = 0
        Me.tab.Size = New System.Drawing.Size(368, 264)
        Me.tab.TabIndex = 1
        '
        'tabData
        '
        Me.tabData.Controls.Add(Me.lblDBVersion)
        Me.tabData.Controls.Add(Me.cmdDBVersion)
        Me.tabData.Controls.Add(Me.chkOverwrite)
        Me.tabData.Controls.Add(Me.chkAddOnly)
        Me.tabData.Controls.Add(Me.cmdSaveUnit)
        Me.tabData.Controls.Add(Me.cmbGroupSelectSaveDB)
        Me.tabData.Controls.Add(Me.lblSelectedDB)
        Me.tabData.Controls.Add(Me.cmdSelectSaveDB)
        Me.tabData.Location = New System.Drawing.Point(4, 22)
        Me.tabData.Name = "tabData"
        Me.tabData.Size = New System.Drawing.Size(360, 238)
        Me.tabData.TabIndex = 0
        Me.tabData.Text = "Daten"
        '
        'lblDBVersion
        '
        Me.lblDBVersion.Location = New System.Drawing.Point(8, 144)
        Me.lblDBVersion.Name = "lblDBVersion"
        Me.lblDBVersion.Size = New System.Drawing.Size(152, 24)
        Me.lblDBVersion.TabIndex = 23
        Me.lblDBVersion.Text = "Aktuelle Datenbank-Version:"
        '
        'cmdDBVersion
        '
        Me.cmdDBVersion.Enabled = False
        Me.cmdDBVersion.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdDBVersion.Location = New System.Drawing.Point(176, 144)
        Me.cmdDBVersion.Name = "cmdDBVersion"
        Me.cmdDBVersion.Size = New System.Drawing.Size(168, 23)
        Me.cmdDBVersion.TabIndex = 22
        Me.cmdDBVersion.Text = "Datenbank-Version angleichen"
        '
        'chkOverwrite
        '
        Me.chkOverwrite.Location = New System.Drawing.Point(8, 120)
        Me.chkOverwrite.Name = "chkOverwrite"
        Me.chkOverwrite.Size = New System.Drawing.Size(168, 16)
        Me.chkOverwrite.TabIndex = 21
        Me.chkOverwrite.Text = "Ohne Abfrage überschreiben"
        '
        'chkAddOnly
        '
        Me.chkAddOnly.Location = New System.Drawing.Point(8, 96)
        Me.chkAddOnly.Name = "chkAddOnly"
        Me.chkAddOnly.Size = New System.Drawing.Size(128, 16)
        Me.chkAddOnly.TabIndex = 20
        Me.chkAddOnly.Text = "Nur neue anhängen"
        '
        'cmdSaveUnit
        '
        Me.cmdSaveUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdSaveUnit.Location = New System.Drawing.Point(176, 96)
        Me.cmdSaveUnit.Name = "cmdSaveUnit"
        Me.cmdSaveUnit.Size = New System.Drawing.Size(168, 23)
        Me.cmdSaveUnit.TabIndex = 19
        Me.cmdSaveUnit.Text = "Gruppe sichern"
        '
        'cmbGroupSelectSaveDB
        '
        Me.cmbGroupSelectSaveDB.Location = New System.Drawing.Point(8, 56)
        Me.cmbGroupSelectSaveDB.Name = "cmbGroupSelectSaveDB"
        Me.cmbGroupSelectSaveDB.Size = New System.Drawing.Size(336, 21)
        Me.cmbGroupSelectSaveDB.TabIndex = 18
        '
        'lblSelectedDB
        '
        Me.lblSelectedDB.Location = New System.Drawing.Point(152, 16)
        Me.lblSelectedDB.Name = "lblSelectedDB"
        Me.lblSelectedDB.Size = New System.Drawing.Size(184, 32)
        Me.lblSelectedDB.TabIndex = 1
        Me.lblSelectedDB.Text = "Datenbank: noch keine gewählt"
        '
        'cmdSelectSaveDB
        '
        Me.cmdSelectSaveDB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdSelectSaveDB.Location = New System.Drawing.Point(16, 16)
        Me.cmdSelectSaveDB.Name = "cmdSelectSaveDB"
        Me.cmdSelectSaveDB.Size = New System.Drawing.Size(128, 32)
        Me.cmdSelectSaveDB.TabIndex = 0
        Me.cmdSelectSaveDB.Text = "Sicherungsdatei auswählen"
        '
        'tabGroups
        '
        Me.tabGroups.Controls.Add(Me.lblInfo)
        Me.tabGroups.Controls.Add(Me.cmdDelete)
        Me.tabGroups.Controls.Add(Me.Label1)
        Me.tabGroups.Controls.Add(Me.cmbLanguage)
        Me.tabGroups.Controls.Add(Me.cmdEdit)
        Me.tabGroups.Controls.Add(Me.txtName)
        Me.tabGroups.Controls.Add(Me.lstGroups)
        Me.tabGroups.Controls.Add(Me.cmdAdd)
        Me.tabGroups.Location = New System.Drawing.Point(4, 22)
        Me.tabGroups.Name = "tabGroups"
        Me.tabGroups.Size = New System.Drawing.Size(360, 238)
        Me.tabGroups.TabIndex = 1
        Me.tabGroups.Text = "Gruppen"
        '
        'lblInfo
        '
        Me.lblInfo.Location = New System.Drawing.Point(168, 80)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(88, 88)
        Me.lblInfo.TabIndex = 7
        Me.lblInfo.Text = "#"
        '
        'cmdDelete
        '
        Me.cmdDelete.Enabled = False
        Me.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdDelete.Location = New System.Drawing.Point(264, 144)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(80, 24)
        Me.cmdDelete.TabIndex = 6
        Me.cmdDelete.Text = "Löschen"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(160, 176)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(184, 56)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Zur zeit ist es leider nicht möglich, die einmal eingestellte Sprache für eine Gr" & _
        "uppe zu Ändern, oder Gruppen zu löschen."
        '
        'cmbLanguage
        '
        Me.cmbLanguage.Location = New System.Drawing.Point(168, 16)
        Me.cmbLanguage.Name = "cmbLanguage"
        Me.cmbLanguage.Size = New System.Drawing.Size(176, 21)
        Me.cmbLanguage.TabIndex = 4
        Me.cmbLanguage.Text = "ComboBox1"
        '
        'cmdEdit
        '
        Me.cmdEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdEdit.Location = New System.Drawing.Point(264, 112)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(80, 24)
        Me.cmdEdit.TabIndex = 3
        Me.cmdEdit.Text = "Ändern"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(168, 48)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(176, 20)
        Me.txtName.TabIndex = 2
        Me.txtName.Text = "TextBox1"
        '
        'lstGroups
        '
        Me.lstGroups.Location = New System.Drawing.Point(8, 16)
        Me.lstGroups.Name = "lstGroups"
        Me.lstGroups.Size = New System.Drawing.Size(144, 212)
        Me.lstGroups.TabIndex = 1
        '
        'cmdAdd
        '
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdAdd.Location = New System.Drawing.Point(264, 80)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "Hinzufügen"
        '
        'tabUnits
        '
        Me.tabUnits.Controls.Add(Me.cmbGroupSelect)
        Me.tabUnits.Controls.Add(Me.Label2)
        Me.tabUnits.Controls.Add(Me.cmdDeleteUnit)
        Me.tabUnits.Controls.Add(Me.Label3)
        Me.tabUnits.Controls.Add(Me.cmdEditUnit)
        Me.tabUnits.Controls.Add(Me.txtNameUnit)
        Me.tabUnits.Controls.Add(Me.lstUnits)
        Me.tabUnits.Controls.Add(Me.cmdAddUnit)
        Me.tabUnits.Location = New System.Drawing.Point(4, 22)
        Me.tabUnits.Name = "tabUnits"
        Me.tabUnits.Size = New System.Drawing.Size(360, 238)
        Me.tabUnits.TabIndex = 2
        Me.tabUnits.Text = "Lektionen"
        '
        'cmbGroupSelect
        '
        Me.cmbGroupSelect.Location = New System.Drawing.Point(8, 16)
        Me.cmbGroupSelect.Name = "cmbGroupSelect"
        Me.cmbGroupSelect.Size = New System.Drawing.Size(336, 21)
        Me.cmbGroupSelect.TabIndex = 16
        Me.cmbGroupSelect.Text = "ComboBox1"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(168, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 88)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "#"
        '
        'cmdDeleteUnit
        '
        Me.cmdDeleteUnit.Enabled = False
        Me.cmdDeleteUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdDeleteUnit.Location = New System.Drawing.Point(264, 144)
        Me.cmdDeleteUnit.Name = "cmdDeleteUnit"
        Me.cmdDeleteUnit.Size = New System.Drawing.Size(80, 24)
        Me.cmdDeleteUnit.TabIndex = 14
        Me.cmdDeleteUnit.Text = "Löschen"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(164, 184)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(184, 40)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Zur zeit ist es leider nicht möglich, Units zu Verschieben oder zu Löschen."
        '
        'cmdEditUnit
        '
        Me.cmdEditUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdEditUnit.Location = New System.Drawing.Point(264, 112)
        Me.cmdEditUnit.Name = "cmdEditUnit"
        Me.cmdEditUnit.Size = New System.Drawing.Size(80, 24)
        Me.cmdEditUnit.TabIndex = 11
        Me.cmdEditUnit.Text = "Ändern"
        '
        'txtNameUnit
        '
        Me.txtNameUnit.Location = New System.Drawing.Point(168, 48)
        Me.txtNameUnit.Name = "txtNameUnit"
        Me.txtNameUnit.Size = New System.Drawing.Size(176, 20)
        Me.txtNameUnit.TabIndex = 10
        Me.txtNameUnit.Text = "txtNameUnit"
        '
        'lstUnits
        '
        Me.lstUnits.Location = New System.Drawing.Point(8, 48)
        Me.lstUnits.Name = "lstUnits"
        Me.lstUnits.Size = New System.Drawing.Size(144, 160)
        Me.lstUnits.TabIndex = 9
        '
        'cmdAddUnit
        '
        Me.cmdAddUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdAddUnit.Location = New System.Drawing.Point(264, 80)
        Me.cmdAddUnit.Name = "cmdAddUnit"
        Me.cmdAddUnit.Size = New System.Drawing.Size(80, 24)
        Me.cmdAddUnit.TabIndex = 8
        Me.cmdAddUnit.Text = "Hinzufügen"
        '
        'cmdOK
        '
        Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdOK.Location = New System.Drawing.Point(296, 280)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.TabIndex = 2
        Me.cmdOK.Text = "Schließen"
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
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(384, 310)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.tab)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "Management"
        Me.Text = "Management"
        Me.tab.ResumeLayout(False)
        Me.tabData.ResumeLayout(False)
        Me.tabGroups.ResumeLayout(False)
        Me.tabUnits.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Management_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cTemp As Collection
        Dim i As Integer        ' Indexzähler

        ' Anzeigen der verfügbaren Sprachen
        cTemp = voc.GetLanguages()
        For i = 1 To cTemp.Count
            Me.cmbLanguage.Items.Add(cTemp.Item(i))
        Next i
        Initialize()

        Me.lstGroups.SelectedIndex = 0
        Me.cmbGroupSelect.SelectedIndex = 0
        Me.cmbGroupSelectSaveDB.SelectedIndex = 0
    End Sub

    Private Sub Management_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        voc.Close()
    End Sub

    Protected Sub Initialize()
        Dim iSelectedGroup As Integer = lstGroups.SelectedIndex
        Dim iSelectedGroupUnits As Integer = cmbGroupSelect.SelectedIndex
        Dim iSelectedUnit = Me.lstUnits.SelectedIndex
        Dim i As Integer
        lstGroups.Items.Clear()
        cmbGroupSelect.Items.Clear()
        ' Füllen der Listen mit allen verfügbaren Gruppen
        For i = 0 To voc.Groups.Count - 1
            Me.lstGroups.Items.Add(voc.Groups(i).Description)
            Me.cmbGroupSelect.Items.Add(voc.Groups(i).Description)
            Me.cmbGroupSelectSaveDB.Items.Add(voc.Groups(i).Description)
        Next i
        lstGroups.SelectedIndex = iSelectedGroup
        cmbGroupSelect.SelectedIndex = iSelectedGroupUnits
        Me.lstUnits.SelectedIndex = iSelectedUnit
        Me.lblDBVersion.Text = "Aktuelle Datenbank-Version:" & vbCrLf & voc.DatabaseVersion
        If voc.DatabaseVersionIndex <> 0 Then
            Me.cmdDBVersion.Text = "Auf Version " & voc.DatabaseVersion(voc.DatabaseVersionIndex - 1) & " updaten."
            Me.cmdDBVersion.Enabled = True
        Else
            Me.cmdDBVersion.Text = "Auf Version " & voc.DatabaseVersion(0) & " updaten."
        End If

    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        MsgBox("Leider nicht möglich!", vbInformation)
    End Sub

    Private Sub lstGroups_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstGroups.SelectedIndexChanged
        If lstGroups.SelectedIndex = -1 Then Exit Sub ' Irreguläre Werte abfangen
        Me.txtName.Text = Me.lstGroups.SelectedItem                                     ' Text aktualisieren
        Me.cmbLanguage.SelectedItem = voc.Groups(lstGroups.SelectedIndex).Type          ' Kombobox aktualisieren
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If Trim(Me.txtName.Text) = "" Then Exit Sub
        ' Ändern der Gruppen-Informationen in der Datenbank
        voc.CloseTable()
        voc.Groups.Language(Me.lstGroups.SelectedItem, Me.cmbLanguage.SelectedIndex + 1)
        voc.Groups.Rename(Me.lstGroups.SelectedItem, Me.txtName.Text)
        Initialize()    ' Anzeige Aktualisieren
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If Trim(Me.txtName.Text) = "" Then Exit Sub
        voc.CloseTable()
        voc.Groups.Add(Me.txtName.Text, Me.cmbLanguage.SelectedIndex + 1)
        Initialize()
    End Sub

    Private Sub cmdDeleteUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteUnit.Click
        MsgBox("Leider ist es zur Zeit nicht möglich Units zu Löschen oder zu Verschieben.", vbInformation)
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub

    Private Sub cmdAddUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddUnit.Click
        If Trim(Me.txtNameUnit.Text = "") Then Exit Sub
        voc.SelectTable(voc.Groups(cmbGroupSelect.SelectedIndex).Table)
        voc.UnitAdd(Me.txtNameUnit.Text)
        Initialize()    'Anzeige aktualisieren
    End Sub

    Private Sub cmbGroupSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroupSelect.SelectedIndexChanged
        ' Lektionen anzeigen
        Dim cUnits As New Collection()
        Dim i As Integer
        voc.SelectTable(voc.Groups(cmbGroupSelect.SelectedIndex).Table)
        cUnits = voc.GetUnits
        Me.lstUnits.Items.Clear()
        For i = 1 To cUnits.Count
            lstUnits.Items.Add(cUnits.Item(i).item(2))
        Next i
        If cUnits.Count > 0 Then lstUnits.SelectedIndex = 0 Else Me.txtNameUnit.Text = ""
    End Sub

    Private Sub lstUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstUnits.SelectedIndexChanged
        Me.txtNameUnit.Text = Me.lstUnits.SelectedItem
    End Sub

    Private Sub cmdEditUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditUnit.Click
        voc.UnitEdit(Me.txtNameUnit.Text, Me.lstUnits.SelectedIndex + 1)
        Initialize()
    End Sub

    Private Sub cmdSelectSaveDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelectSaveDB.Click
        Dim vocSave As CWordTest
        Me.SaveFile.ShowDialog()
        bDBChosen = True
        Me.lblSelectedDB.Text = "Datenbank:" & vbCrLf & SaveFile.FileName
        sDBPath = SaveFile.FileName
        Try
            FileOpen(1, sDBPath, OpenMode.Input)
            FileClose(1)
        Catch ex As IO.IOException When Err.Number = 53      ' Datei existiert nicht, kopieren
            ' Datei auf neueste version überprüfen
            vocSave = New CWordTest(Application.StartupPath() & "\new.mdb")
            Do While vocSave.DatabaseVersionIndex <> 0
                vocSave.UpdateDatabaseVersion()
            Loop
            vocSave = Nothing
            FileCopy(Application.StartupPath() & "\new.mdb", SaveFile.FileName)
        Catch es As IO.IOException When Err.Number = 75
            ' nichts
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & Err.Number)
        End Try
        ' Version aktualisieren
        vocSave = New CWordTest(sDBPath)
        Do While vocSave.DatabaseVersionIndex <> 0
            vocSave.UpdateDatabaseVersion()
        Loop
    End Sub

    Private Sub cmbGroupSelectSaveDB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroupSelectSaveDB.SelectedIndexChanged
        voc.SelectTable(voc.Groups(cmbGroupSelectSaveDB.SelectedIndex).Table)
    End Sub

    Private Sub cmdSaveUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveUnit.Click
        Select Case voc.SaveTable(sDBPath, Me.chkAddOnly.Checked, Not Me.chkOverwrite.Checked)
            Case SaveErrors.NoError
                MsgBox("Sichern erfolgreich!", vbInformation)
            Case SaveErrors.NotConnected
                MsgBox("Sie müssen sich mit einer Datenbank verbinden," & vbCrLf & "bevor Sie Daten sichern können!", vbCritical)
            Case SaveErrors.TableExists
                Dim iYesNo As MsgBoxResult = MsgBox("Soll die Tabelle überschrieben werden?", MsgBoxStyle.YesNo)
                If iYesNo = MsgBoxResult.Yes Then
                    If voc.SaveTable(sDBPath, Me.chkAddOnly.Checked, True) Then MsgBox("Sichern fehlgeschlagen!", MsgBoxStyle.Critical)
                End If
            Case SaveErrors.UnknownError
                MsgBox("Sichern fehlgeschlagen!", MsgBoxStyle.Critical)
        End Select
    End Sub

    Private Sub txtDBVersion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDBVersion.Click
        voc.UpdateDatabaseVersion()
        Initialize()
    End Sub

    Private Sub tabData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabData.Click

    End Sub
End Class
