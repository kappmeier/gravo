Public Class VocInput
	Inherits System.Windows.Forms.Form
	Dim voc As xlsVocInput	' Zugriff auf Vokabel-Datenbank für Vokabeleingabe
	'Dim ldfRule As xlsLDFRule	 ' Zugriff auf Languages die im LDF-System vorhanden sind
	Dim ldfManagement As xlsLDFManagement

#Region " Windows Form Designer generated code "

	Public Sub New()
		MyBase.New()

		' Dieser Aufruf ist für den Windows-Formular-Designer erforderlich.
		InitializeComponent()

		' Initialisierungen nach dem Aufruf InitializeComponent() hinzufügen

	End Sub

	' Form überschreibt den Löschvorgang zur Bereinigung der Komponentenliste.
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	' Für Windows-Formular-Designer erforderlich
	Private components As System.ComponentModel.Container

	'HINWEIS: Die folgende Prozedur ist für den Windows-Formular-Designer erforderlich
	'Sie kann mit dem Windows-Formular-Designer modifiziert werden.
	'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
	Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
	Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
	Friend WithEvents mnuInputNext As System.Windows.Forms.MenuItem
	Friend WithEvents mnuInputLast As System.Windows.Forms.MenuItem
	Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
	Friend WithEvents mnuInputNew As System.Windows.Forms.MenuItem
	Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
	Friend WithEvents txtDescription As System.Windows.Forms.TextBox
	Friend WithEvents Label15 As System.Windows.Forms.Label
	Friend WithEvents txtAdditionalInfo As System.Windows.Forms.TextBox
	Friend WithEvents Label14 As System.Windows.Forms.Label
	Friend WithEvents Label11 As System.Windows.Forms.Label
	Friend WithEvents txtPost As System.Windows.Forms.TextBox
	Friend WithEvents txtWord As System.Windows.Forms.TextBox
	Friend WithEvents Label13 As System.Windows.Forms.Label
	Friend WithEvents Label12 As System.Windows.Forms.Label
	Friend WithEvents txtPre As System.Windows.Forms.TextBox
	Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents cmdDeleteMeaning As System.Windows.Forms.Button
	Friend WithEvents lstWordMeanings As System.Windows.Forms.ListBox
	Friend WithEvents cmdNewMeaning As System.Windows.Forms.Button
	Friend WithEvents txtMeaning As System.Windows.Forms.TextBox
	Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
	Friend WithEvents cmbUnits As System.Windows.Forms.ComboBox
	Friend WithEvents cmbGroups As System.Windows.Forms.ComboBox
	Friend WithEvents lstWords As System.Windows.Forms.ListBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblWords As System.Windows.Forms.Label
	Friend WithEvents lblUnits As System.Windows.Forms.Label
	Friend WithEvents lblWordInUnit As System.Windows.Forms.Label
	Friend WithEvents cmdNext As System.Windows.Forms.Button
	Friend WithEvents cmdLast As System.Windows.Forms.Button
	Friend WithEvents cmdDelete As System.Windows.Forms.Button
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents Label9 As System.Windows.Forms.Label
	Friend WithEvents lstWordTypes As System.Windows.Forms.ListBox
	Friend WithEvents lblGrammar1 As System.Windows.Forms.Label
	Friend WithEvents lblGrammar3 As System.Windows.Forms.Label
	Friend WithEvents lblGrammar2 As System.Windows.Forms.Label
	Friend WithEvents txtGrammar2 As System.Windows.Forms.TextBox
	Friend WithEvents txtGrammar1 As System.Windows.Forms.TextBox
	Friend WithEvents txtGrammar3 As System.Windows.Forms.TextBox
	Friend WithEvents chkIrregular As System.Windows.Forms.CheckBox
	Friend WithEvents cmdSave As System.Windows.Forms.Button
	Friend WithEvents cmdNew As System.Windows.Forms.Button
	Friend WithEvents Label10 As System.Windows.Forms.Label
	Friend WithEvents Label8 As System.Windows.Forms.Label
	Friend WithEvents lblUnit As System.Windows.Forms.Label
	Friend WithEvents nudChapter As System.Windows.Forms.NumericUpDown
	Friend WithEvents cmbUnit As System.Windows.Forms.ComboBox
	Friend WithEvents chkMustKnow As System.Windows.Forms.CheckBox
	Friend WithEvents cmdSearch As System.Windows.Forms.Button
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.MainMenu1 = New System.Windows.Forms.MainMenu
		Me.MenuItem1 = New System.Windows.Forms.MenuItem
		Me.mnuInputNew = New System.Windows.Forms.MenuItem
		Me.MenuItem3 = New System.Windows.Forms.MenuItem
		Me.mnuInputLast = New System.Windows.Forms.MenuItem
		Me.mnuInputNext = New System.Windows.Forms.MenuItem
		Me.GroupBox4 = New System.Windows.Forms.GroupBox
		Me.txtDescription = New System.Windows.Forms.TextBox
		Me.Label15 = New System.Windows.Forms.Label
		Me.txtAdditionalInfo = New System.Windows.Forms.TextBox
		Me.Label14 = New System.Windows.Forms.Label
		Me.Label11 = New System.Windows.Forms.Label
		Me.txtPost = New System.Windows.Forms.TextBox
		Me.txtWord = New System.Windows.Forms.TextBox
		Me.Label13 = New System.Windows.Forms.Label
		Me.Label12 = New System.Windows.Forms.Label
		Me.txtPre = New System.Windows.Forms.TextBox
		Me.GroupBox3 = New System.Windows.Forms.GroupBox
		Me.Label5 = New System.Windows.Forms.Label
		Me.cmdDeleteMeaning = New System.Windows.Forms.Button
		Me.lstWordMeanings = New System.Windows.Forms.ListBox
		Me.cmdNewMeaning = New System.Windows.Forms.Button
		Me.txtMeaning = New System.Windows.Forms.TextBox
		Me.GroupBox2 = New System.Windows.Forms.GroupBox
		Me.cmbUnits = New System.Windows.Forms.ComboBox
		Me.cmbGroups = New System.Windows.Forms.ComboBox
		Me.lstWords = New System.Windows.Forms.ListBox
		Me.Label1 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.lblWords = New System.Windows.Forms.Label
		Me.lblUnits = New System.Windows.Forms.Label
		Me.lblWordInUnit = New System.Windows.Forms.Label
		Me.cmdNext = New System.Windows.Forms.Button
		Me.cmdLast = New System.Windows.Forms.Button
		Me.cmdDelete = New System.Windows.Forms.Button
		Me.cmdCancel = New System.Windows.Forms.Button
		Me.GroupBox1 = New System.Windows.Forms.GroupBox
		Me.Label9 = New System.Windows.Forms.Label
		Me.lstWordTypes = New System.Windows.Forms.ListBox
		Me.lblGrammar1 = New System.Windows.Forms.Label
		Me.lblGrammar3 = New System.Windows.Forms.Label
		Me.lblGrammar2 = New System.Windows.Forms.Label
		Me.txtGrammar2 = New System.Windows.Forms.TextBox
		Me.txtGrammar1 = New System.Windows.Forms.TextBox
		Me.txtGrammar3 = New System.Windows.Forms.TextBox
		Me.chkIrregular = New System.Windows.Forms.CheckBox
		Me.cmdSave = New System.Windows.Forms.Button
		Me.cmdNew = New System.Windows.Forms.Button
		Me.Label10 = New System.Windows.Forms.Label
		Me.Label8 = New System.Windows.Forms.Label
		Me.lblUnit = New System.Windows.Forms.Label
		Me.nudChapter = New System.Windows.Forms.NumericUpDown
		Me.cmbUnit = New System.Windows.Forms.ComboBox
		Me.chkMustKnow = New System.Windows.Forms.CheckBox
		Me.cmdSearch = New System.Windows.Forms.Button
		Me.GroupBox4.SuspendLayout()
		Me.GroupBox3.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		CType(Me.nudChapter, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'MainMenu1
		'
		Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
		'
		'MenuItem1
		'
		Me.MenuItem1.Index = 0
		Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuInputNew, Me.MenuItem3, Me.mnuInputLast, Me.mnuInputNext})
		Me.MenuItem1.Text = "&Eingabe"
		Me.MenuItem1.Visible = False
		'
		'mnuInputNew
		'
		Me.mnuInputNew.Index = 0
		Me.mnuInputNew.Text = "N&eue Vokabel"
		'
		'MenuItem3
		'
		Me.MenuItem3.Index = 1
		Me.MenuItem3.Text = "-"
		'
		'mnuInputLast
		'
		Me.mnuInputLast.Index = 2
		Me.mnuInputLast.Shortcut = System.Windows.Forms.Shortcut.CtrlV
		Me.mnuInputLast.Text = "&Vorherige Vokabel"
		'
		'mnuInputNext
		'
		Me.mnuInputNext.Index = 3
		Me.mnuInputNext.Shortcut = System.Windows.Forms.Shortcut.CtrlN
		Me.mnuInputNext.Text = "&Nächste Vokabel"
		'
		'GroupBox4
		'
		Me.GroupBox4.Controls.Add(Me.txtDescription)
		Me.GroupBox4.Controls.Add(Me.Label15)
		Me.GroupBox4.Controls.Add(Me.txtAdditionalInfo)
		Me.GroupBox4.Controls.Add(Me.Label14)
		Me.GroupBox4.Controls.Add(Me.Label11)
		Me.GroupBox4.Controls.Add(Me.txtPost)
		Me.GroupBox4.Controls.Add(Me.txtWord)
		Me.GroupBox4.Controls.Add(Me.Label13)
		Me.GroupBox4.Controls.Add(Me.Label12)
		Me.GroupBox4.Controls.Add(Me.txtPre)
		Me.GroupBox4.Location = New System.Drawing.Point(209, 7)
		Me.GroupBox4.Name = "GroupBox4"
		Me.GroupBox4.Size = New System.Drawing.Size(320, 128)
		Me.GroupBox4.TabIndex = 11
		Me.GroupBox4.TabStop = False
		Me.GroupBox4.Text = "Vokabelinfo:"
		'
		'txtDescription
		'
		Me.txtDescription.Location = New System.Drawing.Point(72, 96)
		Me.txtDescription.MaxLength = 50
		Me.txtDescription.Name = "txtDescription"
		Me.txtDescription.Size = New System.Drawing.Size(176, 20)
		Me.txtDescription.TabIndex = 21
		Me.txtDescription.Text = ""
		'
		'Label15
		'
		Me.Label15.Location = New System.Drawing.Point(8, 64)
		Me.Label15.Name = "Label15"
		Me.Label15.Size = New System.Drawing.Size(64, 24)
		Me.Label15.TabIndex = 18
		Me.Label15.Text = "Zusatzinfo:"
		Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'txtAdditionalInfo
		'
		Me.txtAdditionalInfo.Location = New System.Drawing.Point(72, 64)
		Me.txtAdditionalInfo.MaxLength = 50
		Me.txtAdditionalInfo.Name = "txtAdditionalInfo"
		Me.txtAdditionalInfo.Size = New System.Drawing.Size(176, 20)
		Me.txtAdditionalInfo.TabIndex = 19
		Me.txtAdditionalInfo.Text = ""
		'
		'Label14
		'
		Me.Label14.Location = New System.Drawing.Point(8, 96)
		Me.Label14.Name = "Label14"
		Me.Label14.Size = New System.Drawing.Size(80, 16)
		Me.Label14.TabIndex = 20
		Me.Label14.Text = "Hilfe:"
		Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'Label11
		'
		Me.Label11.Location = New System.Drawing.Point(72, 16)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(176, 16)
		Me.Label11.TabIndex = 14
		Me.Label11.Text = "Vokabel:"
		Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'txtPost
		'
		Me.txtPost.Location = New System.Drawing.Point(256, 32)
		Me.txtPost.MaxLength = 50
		Me.txtPost.Name = "txtPost"
		Me.txtPost.Size = New System.Drawing.Size(56, 20)
		Me.txtPost.TabIndex = 17
		Me.txtPost.Text = ""
		'
		'txtWord
		'
		Me.txtWord.Location = New System.Drawing.Point(72, 32)
		Me.txtWord.MaxLength = 50
		Me.txtWord.Name = "txtWord"
		Me.txtWord.Size = New System.Drawing.Size(176, 20)
		Me.txtWord.TabIndex = 15
		Me.txtWord.Text = ""
		'
		'Label13
		'
		Me.Label13.Location = New System.Drawing.Point(256, 16)
		Me.Label13.Name = "Label13"
		Me.Label13.Size = New System.Drawing.Size(56, 16)
		Me.Label13.TabIndex = 16
		Me.Label13.Text = "Nach:"
		Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'Label12
		'
		Me.Label12.Location = New System.Drawing.Point(8, 16)
		Me.Label12.Name = "Label12"
		Me.Label12.Size = New System.Drawing.Size(56, 16)
		Me.Label12.TabIndex = 12
		Me.Label12.Text = "Vor:"
		Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'txtPre
		'
		Me.txtPre.Location = New System.Drawing.Point(8, 32)
		Me.txtPre.MaxLength = 50
		Me.txtPre.Name = "txtPre"
		Me.txtPre.Size = New System.Drawing.Size(56, 20)
		Me.txtPre.TabIndex = 13
		Me.txtPre.Text = ""
		'
		'GroupBox3
		'
		Me.GroupBox3.Controls.Add(Me.Label5)
		Me.GroupBox3.Controls.Add(Me.cmdDeleteMeaning)
		Me.GroupBox3.Controls.Add(Me.lstWordMeanings)
		Me.GroupBox3.Controls.Add(Me.cmdNewMeaning)
		Me.GroupBox3.Controls.Add(Me.txtMeaning)
		Me.GroupBox3.Location = New System.Drawing.Point(209, 143)
		Me.GroupBox3.Name = "GroupBox3"
		Me.GroupBox3.Size = New System.Drawing.Size(320, 104)
		Me.GroupBox3.TabIndex = 22
		Me.GroupBox3.TabStop = False
		Me.GroupBox3.Text = "Bedeutung:"
		'
		'Label5
		'
		Me.Label5.Location = New System.Drawing.Point(136, 24)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(176, 16)
		Me.Label5.TabIndex = 24
		Me.Label5.Text = "neue Bedeutung:"
		Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'cmdDeleteMeaning
		'
		Me.cmdDeleteMeaning.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdDeleteMeaning.Location = New System.Drawing.Point(232, 72)
		Me.cmdDeleteMeaning.Name = "cmdDeleteMeaning"
		Me.cmdDeleteMeaning.Size = New System.Drawing.Size(80, 24)
		Me.cmdDeleteMeaning.TabIndex = 27
		Me.cmdDeleteMeaning.Text = "löschen"
		'
		'lstWordMeanings
		'
		Me.lstWordMeanings.Location = New System.Drawing.Point(8, 24)
		Me.lstWordMeanings.Name = "lstWordMeanings"
		Me.lstWordMeanings.Size = New System.Drawing.Size(120, 69)
		Me.lstWordMeanings.TabIndex = 23
		'
		'cmdNewMeaning
		'
		Me.cmdNewMeaning.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdNewMeaning.Location = New System.Drawing.Point(136, 72)
		Me.cmdNewMeaning.Name = "cmdNewMeaning"
		Me.cmdNewMeaning.Size = New System.Drawing.Size(80, 24)
		Me.cmdNewMeaning.TabIndex = 26
		Me.cmdNewMeaning.Text = "Übernehmen"
		'
		'txtMeaning
		'
		Me.txtMeaning.Location = New System.Drawing.Point(136, 40)
		Me.txtMeaning.MaxLength = 50
		Me.txtMeaning.Name = "txtMeaning"
		Me.txtMeaning.Size = New System.Drawing.Size(176, 20)
		Me.txtMeaning.TabIndex = 25
		Me.txtMeaning.Text = ""
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.cmbUnits)
		Me.GroupBox2.Controls.Add(Me.cmbGroups)
		Me.GroupBox2.Controls.Add(Me.lstWords)
		Me.GroupBox2.Controls.Add(Me.Label1)
		Me.GroupBox2.Controls.Add(Me.Label2)
		Me.GroupBox2.Controls.Add(Me.lblWords)
		Me.GroupBox2.Controls.Add(Me.lblUnits)
		Me.GroupBox2.Controls.Add(Me.lblWordInUnit)
		Me.GroupBox2.Controls.Add(Me.cmdNext)
		Me.GroupBox2.Controls.Add(Me.cmdLast)
		Me.GroupBox2.Location = New System.Drawing.Point(9, 7)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(192, 400)
		Me.GroupBox2.TabIndex = 0
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Allgemein"
		'
		'cmbUnits
		'
		Me.cmbUnits.Location = New System.Drawing.Point(8, 64)
		Me.cmbUnits.Name = "cmbUnits"
		Me.cmbUnits.Size = New System.Drawing.Size(176, 21)
		Me.cmbUnits.TabIndex = 4
		Me.cmbUnits.Text = "cmbUnits"
		'
		'cmbGroups
		'
		Me.cmbGroups.Location = New System.Drawing.Point(8, 16)
		Me.cmbGroups.Name = "cmbGroups"
		Me.cmbGroups.Size = New System.Drawing.Size(176, 21)
		Me.cmbGroups.TabIndex = 1
		Me.cmbGroups.Text = "cmbGroups"
		'
		'lstWords
		'
		Me.lstWords.Location = New System.Drawing.Point(8, 104)
		Me.lstWords.Name = "lstWords"
		Me.lstWords.Size = New System.Drawing.Size(176, 251)
		Me.lstWords.TabIndex = 7
		'
		'Label1
		'
		Me.Label1.Location = New System.Drawing.Point(8, 48)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(64, 16)
		Me.Label1.TabIndex = 2
		Me.Label1.Text = "Lektionen:"
		'
		'Label2
		'
		Me.Label2.Location = New System.Drawing.Point(8, 88)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(64, 16)
		Me.Label2.TabIndex = 5
		Me.Label2.Text = "Vokabeln:"
		'
		'lblWords
		'
		Me.lblWords.AutoSize = True
		Me.lblWords.Location = New System.Drawing.Point(80, 88)
		Me.lblWords.Name = "lblWords"
		Me.lblWords.Size = New System.Drawing.Size(34, 16)
		Me.lblWords.TabIndex = 6
		Me.lblWords.Text = "Count"
		Me.lblWords.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblUnits
		'
		Me.lblUnits.AutoSize = True
		Me.lblUnits.Location = New System.Drawing.Point(80, 48)
		Me.lblUnits.Name = "lblUnits"
		Me.lblUnits.Size = New System.Drawing.Size(34, 16)
		Me.lblUnits.TabIndex = 3
		Me.lblUnits.Text = "Count"
		Me.lblUnits.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblWordInUnit
		'
		Me.lblWordInUnit.Location = New System.Drawing.Point(8, 368)
		Me.lblWordInUnit.Name = "lblWordInUnit"
		Me.lblWordInUnit.Size = New System.Drawing.Size(88, 16)
		Me.lblWordInUnit.TabIndex = 8
		Me.lblWordInUnit.Text = "# Word in Unit"
		'
		'cmdNext
		'
		Me.cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdNext.Location = New System.Drawing.Point(152, 368)
		Me.cmdNext.Name = "cmdNext"
		Me.cmdNext.Size = New System.Drawing.Size(24, 23)
		Me.cmdNext.TabIndex = 10
		Me.cmdNext.Text = ">"
		'
		'cmdLast
		'
		Me.cmdLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdLast.Location = New System.Drawing.Point(120, 368)
		Me.cmdLast.Name = "cmdLast"
		Me.cmdLast.Size = New System.Drawing.Size(24, 23)
		Me.cmdLast.TabIndex = 23
		Me.cmdLast.Text = "<"
		'
		'cmdDelete
		'
		Me.cmdDelete.Enabled = False
		Me.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdDelete.Location = New System.Drawing.Point(537, 319)
		Me.cmdDelete.Name = "cmdDelete"
		Me.cmdDelete.Size = New System.Drawing.Size(72, 23)
		Me.cmdDelete.TabIndex = 46
		Me.cmdDelete.Text = "&Löschen"
		'
		'cmdCancel
		'
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdCancel.Location = New System.Drawing.Point(537, 383)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(72, 23)
		Me.cmdCancel.TabIndex = 48
		Me.cmdCancel.Text = "Schließen"
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Label9)
		Me.GroupBox1.Controls.Add(Me.lstWordTypes)
		Me.GroupBox1.Controls.Add(Me.lblGrammar1)
		Me.GroupBox1.Controls.Add(Me.lblGrammar3)
		Me.GroupBox1.Controls.Add(Me.lblGrammar2)
		Me.GroupBox1.Controls.Add(Me.txtGrammar2)
		Me.GroupBox1.Controls.Add(Me.txtGrammar1)
		Me.GroupBox1.Controls.Add(Me.txtGrammar3)
		Me.GroupBox1.Controls.Add(Me.chkIrregular)
		Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.GroupBox1.Location = New System.Drawing.Point(209, 255)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(320, 152)
		Me.GroupBox1.TabIndex = 28
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Grammatik:"
		'
		'Label9
		'
		Me.Label9.Location = New System.Drawing.Point(8, 16)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(100, 16)
		Me.Label9.TabIndex = 29
		Me.Label9.Text = "Wortart:"
		'
		'lstWordTypes
		'
		Me.lstWordTypes.Location = New System.Drawing.Point(8, 32)
		Me.lstWordTypes.Name = "lstWordTypes"
		Me.lstWordTypes.Size = New System.Drawing.Size(120, 95)
		Me.lstWordTypes.TabIndex = 30
		'
		'lblGrammar1
		'
		Me.lblGrammar1.Location = New System.Drawing.Point(136, 16)
		Me.lblGrammar1.Name = "lblGrammar1"
		Me.lblGrammar1.Size = New System.Drawing.Size(176, 16)
		Me.lblGrammar1.TabIndex = 32
		Me.lblGrammar1.Text = "Grammatik 1:"
		Me.lblGrammar1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'lblGrammar3
		'
		Me.lblGrammar3.Location = New System.Drawing.Point(136, 96)
		Me.lblGrammar3.Name = "lblGrammar3"
		Me.lblGrammar3.Size = New System.Drawing.Size(176, 16)
		Me.lblGrammar3.TabIndex = 36
		Me.lblGrammar3.Text = "Grammatik 3:"
		Me.lblGrammar3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'lblGrammar2
		'
		Me.lblGrammar2.Location = New System.Drawing.Point(136, 56)
		Me.lblGrammar2.Name = "lblGrammar2"
		Me.lblGrammar2.Size = New System.Drawing.Size(176, 16)
		Me.lblGrammar2.TabIndex = 34
		Me.lblGrammar2.Text = "Grammatik 2:"
		Me.lblGrammar2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'txtGrammar2
		'
		Me.txtGrammar2.Enabled = False
		Me.txtGrammar2.Location = New System.Drawing.Point(136, 72)
		Me.txtGrammar2.MaxLength = 50
		Me.txtGrammar2.Name = "txtGrammar2"
		Me.txtGrammar2.Size = New System.Drawing.Size(176, 20)
		Me.txtGrammar2.TabIndex = 35
		Me.txtGrammar2.Text = ""
		'
		'txtGrammar1
		'
		Me.txtGrammar1.Enabled = False
		Me.txtGrammar1.Location = New System.Drawing.Point(136, 32)
		Me.txtGrammar1.MaxLength = 50
		Me.txtGrammar1.Name = "txtGrammar1"
		Me.txtGrammar1.Size = New System.Drawing.Size(176, 20)
		Me.txtGrammar1.TabIndex = 33
		Me.txtGrammar1.Text = ""
		'
		'txtGrammar3
		'
		Me.txtGrammar3.Enabled = False
		Me.txtGrammar3.Location = New System.Drawing.Point(136, 112)
		Me.txtGrammar3.MaxLength = 50
		Me.txtGrammar3.Name = "txtGrammar3"
		Me.txtGrammar3.Size = New System.Drawing.Size(176, 20)
		Me.txtGrammar3.TabIndex = 37
		Me.txtGrammar3.Text = ""
		'
		'chkIrregular
		'
		Me.chkIrregular.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.chkIrregular.Location = New System.Drawing.Point(8, 128)
		Me.chkIrregular.Name = "chkIrregular"
		Me.chkIrregular.Size = New System.Drawing.Size(104, 16)
		Me.chkIrregular.TabIndex = 31
		Me.chkIrregular.Text = "Unregelmäßig"
		'
		'cmdSave
		'
		Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdSave.Location = New System.Drawing.Point(537, 255)
		Me.cmdSave.Name = "cmdSave"
		Me.cmdSave.Size = New System.Drawing.Size(72, 23)
		Me.cmdSave.TabIndex = 44
		Me.cmdSave.Text = "Speichern"
		'
		'cmdNew
		'
		Me.cmdNew.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.cmdNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdNew.Location = New System.Drawing.Point(537, 287)
		Me.cmdNew.Name = "cmdNew"
		Me.cmdNew.Size = New System.Drawing.Size(72, 23)
		Me.cmdNew.TabIndex = 45
		Me.cmdNew.Text = "&Neu"
		'
		'Label10
		'
		Me.Label10.Enabled = False
		Me.Label10.Location = New System.Drawing.Point(537, 15)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(64, 16)
		Me.Label10.TabIndex = 38
		Me.Label10.Text = "Unit:"
		'
		'Label8
		'
		Me.Label8.Enabled = False
		Me.Label8.Location = New System.Drawing.Point(537, 87)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(56, 16)
		Me.Label8.TabIndex = 41
		Me.Label8.Text = "Chapter:"
		'
		'lblUnit
		'
		Me.lblUnit.Enabled = False
		Me.lblUnit.Location = New System.Drawing.Point(537, 55)
		Me.lblUnit.Name = "lblUnit"
		Me.lblUnit.Size = New System.Drawing.Size(72, 16)
		Me.lblUnit.TabIndex = 40
		Me.lblUnit.Text = "#Unit"
		Me.lblUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'nudChapter
		'
		Me.nudChapter.Enabled = False
		Me.nudChapter.Location = New System.Drawing.Point(537, 103)
		Me.nudChapter.Name = "nudChapter"
		Me.nudChapter.Size = New System.Drawing.Size(72, 20)
		Me.nudChapter.TabIndex = 42
		'
		'cmbUnit
		'
		Me.cmbUnit.DropDownWidth = 121
		Me.cmbUnit.Enabled = False
		Me.cmbUnit.Location = New System.Drawing.Point(537, 31)
		Me.cmbUnit.Name = "cmbUnit"
		Me.cmbUnit.Size = New System.Drawing.Size(72, 21)
		Me.cmbUnit.TabIndex = 39
		'
		'chkMustKnow
		'
		Me.chkMustKnow.Enabled = False
		Me.chkMustKnow.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.chkMustKnow.Location = New System.Drawing.Point(537, 151)
		Me.chkMustKnow.Name = "chkMustKnow"
		Me.chkMustKnow.Size = New System.Drawing.Size(56, 16)
		Me.chkMustKnow.TabIndex = 43
		Me.chkMustKnow.Text = "Pflicht"
		'
		'cmdSearch
		'
		Me.cmdSearch.Enabled = False
		Me.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdSearch.Location = New System.Drawing.Point(537, 351)
		Me.cmdSearch.Name = "cmdSearch"
		Me.cmdSearch.Size = New System.Drawing.Size(72, 23)
		Me.cmdSearch.TabIndex = 47
		Me.cmdSearch.Text = "Suchen"
		'
		'VocInput
		'
		Me.AcceptButton = Me.cmdNew
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
		Me.ClientSize = New System.Drawing.Size(618, 415)
		Me.Controls.Add(Me.GroupBox4)
		Me.Controls.Add(Me.GroupBox3)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.cmdDelete)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.cmdSave)
		Me.Controls.Add(Me.cmdNew)
		Me.Controls.Add(Me.Label10)
		Me.Controls.Add(Me.Label8)
		Me.Controls.Add(Me.lblUnit)
		Me.Controls.Add(Me.nudChapter)
		Me.Controls.Add(Me.cmbUnit)
		Me.Controls.Add(Me.chkMustKnow)
		Me.Controls.Add(Me.cmdSearch)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.Menu = Me.MainMenu1
		Me.Name = "VocInput"
		Me.Text = "Eingabe"
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		Me.GroupBox4.ResumeLayout(False)
		Me.GroupBox3.ResumeLayout(False)
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox1.ResumeLayout(False)
		CType(Me.nudChapter, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private Sub VocInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

		Dim db As New CDBOperation		  ' Datenbankoperationen
		db.Open(Application.StartupPath() & "\voc.mdb")		  ' Datenbank öffnen
		voc = New xlsVocInput(db)		' Datenbank zur Verfügung stellen
		ldfManagement = New xlsLDFManagement
		ldfManagement.LDFPath = Application.StartupPath()

		'Füllen der Listen
		Dim i As Integer	   ' Index
		For i = 1 To voc.Groups.Count
			Me.cmbGroups.Items.Add(voc.Groups.Item(i).Description)
		Next i

		' Falls möglich, erste auswählen
		If cmbGroups.Items.Count > 0 Then cmbGroups.SelectedIndex = 0
	End Sub

	Private Sub cmbGroups_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroups.SelectedIndexChanged
		' Liste der Units füllen
		cmbUnits.Items.Clear()
		cmbUnits.Text = ""
		voc.SelectGroup(voc.GroupDescriptionToName(cmbGroups.SelectedItem))
		Me.cmbUnits.Items.AddRange(voc.UnitNames.ToArray)
		Me.lblUnits.Text = voc.Units.Count

		' LDF für diese Sprache wählen
		ldfManagement.SelectLD(voc.Language, voc.LDFType)

		' Liste der Wortarten füllen
		lstWordTypes.Items.Clear()
		Dim i As Integer		  ' Index
		For i = 1 To ldfManagement.FormList.Count
			Me.lstWordTypes.Items.Add(ldfManagement.FormList.Item(i).Right)
		Next i

		' Wort auswählen
		If cmbUnits.Items.Count > 0 Then cmbUnits.SelectedIndex = 0 Else lstWords.Items.Clear()
	End Sub

	Private Sub cmbUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnits.SelectedIndexChanged
		' Vokabeln anzeigen
		lstWords.Items.Clear()
		voc.SelectUnit(cmbUnits.SelectedIndex + 1)
		Dim i As Integer		  ' Index
		For i = 1 To voc.WordNumbers.Count
			Me.lstWords.Items.Add(voc.WordNames.Item(i))
		Next i
		Me.lblWords.Text = voc.WordNumbers.Count
		If lstWords.Items.Count > 0 Then lstWords.SelectedIndex = 0 Else  ' Clear alle felder
	End Sub

	Private Sub lstWords_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstWords.SelectedIndexChanged
		If lstWords.SelectedIndex = -1 Then Exit Sub
		' Wort laden
		voc.CurrentWordNumber = voc.WordNumbers.Item(lstWords.SelectedIndex + 1)
		Me.lblWordInUnit.Text = voc.CurrentWord.WordInUnit

		' Vokabelinfo
		Me.txtWord.Text = voc.CurrentWord.Word
		Me.txtPre.Text = voc.CurrentWord.Pre
		Me.txtPost.Text = voc.CurrentWord.Post
		Me.txtAdditionalInfo.Text = voc.CurrentWord.AdditionalTargetLangInfo
		Me.txtDescription.Text = voc.CurrentWord.Description

		' Bedeutung
		Me.lstWordMeanings.Items.Clear()
		Me.txtMeaning.Text = ""
		If voc.CurrentWord.Meaning.Length <> 0 Then
			Me.lstWordMeanings.Items.AddRange(voc.CurrentWord.Meaning)
		End If

		' Grammatik
		Me.txtGrammar1.Text = voc.CurrentWord.Extended1
		Me.txtGrammar2.Text = voc.CurrentWord.Extended2
		Me.txtGrammar3.Text = voc.CurrentWord.Extended3
		Me.lstWordTypes.SelectedIndex = voc.CurrentWord.WordType
		Me.chkIrregular.Checked = voc.CurrentWord.ExtendedIsValid

		' Sonstiges
		' Mustknow wird entfernt, ein neues feld mit ranking 1 - ... wird eingeführt
	End Sub

	Private Sub Clear()
		' Vokabelinfo löschen
		Me.txtWord.Text = ""
		Me.txtPre.Text = ""
		Me.txtPost.Text = ""
		Me.txtAdditionalInfo.Text = ""
		Me.txtDescription.Text = ""

		' Bedeutung
		Me.lstWordMeanings.Items.Clear()
		Me.txtMeaning.Text = ""

		' Grammatik
		Me.txtGrammar1.Text = ""
		Me.txtGrammar2.Text = ""
		Me.txtGrammar2.Text = ""
		' ändern der wortliste nicht nötig
		Me.chkIrregular.Checked = False
	End Sub

	Private Sub chkIrregular_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIrregular.CheckedChanged
		txtGrammar1.Enabled = chkIrregular.Checked And lblGrammar1.Text <> ""
		txtGrammar2.Enabled = chkIrregular.Checked And lblGrammar2.Text <> ""
		txtGrammar3.Enabled = chkIrregular.Checked And lblGrammar3.Text <> ""
	End Sub

	Private Sub lstWordTypes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstWordTypes.SelectedIndexChanged
		Me.lblGrammar1.Text = ldfManagement.FormDescEx(lstWordTypes.SelectedIndex + 1).item(0) & ":"
		Me.lblGrammar2.Text = ldfManagement.FormDescEx(lstWordTypes.SelectedIndex + 1).item(1) & ":"
		Me.lblGrammar3.Text = ldfManagement.FormDescEx(lstWordTypes.SelectedIndex + 1).item(2) & ":"
		If lblGrammar1.Text = ":" Then lblGrammar1.Text = ""
		If lblGrammar2.Text = ":" Then lblGrammar2.Text = ""
		If lblGrammar3.Text = ":" Then lblGrammar3.Text = ""
		txtGrammar1.Enabled = chkIrregular.Checked And lblGrammar1.Text <> ""
		txtGrammar2.Enabled = chkIrregular.Checked And lblGrammar2.Text <> ""
		txtGrammar3.Enabled = chkIrregular.Checked And lblGrammar3.Text <> ""
	End Sub

	Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
		Save()
	End Sub

	Private Sub Save()
		If voc.CurrentWord Is Nothing Then Exit Sub ' Tritt auf, wenn kein wort selektiert ist

		Dim bWordChanged As Boolean
		bWordChanged = (voc.CurrentWord.Word <> Me.txtWord.Text) And (Me.lstWords.Items.Count > 0)

		' Vokabelinfo
		voc.CurrentWord.Word = Me.txtWord.Text
		voc.CurrentWord.Pre = Me.txtPre.Text
		voc.CurrentWord.Post = Me.txtPost.Text
		voc.CurrentWord.AdditionalTargetLangInfo = Me.txtAdditionalInfo.Text
		voc.CurrentWord.Description = Me.txtDescription.Text

		' Bedeutung
		' automatisch durch die buttons

		' Grammatik
		voc.CurrentWord.Extended1 = Me.txtGrammar1.Text
		voc.CurrentWord.Extended2 = Me.txtGrammar2.Text
		voc.CurrentWord.Extended3 = Me.txtGrammar3.Text
		voc.CurrentWord.WordType = Me.lstWordTypes.SelectedIndex
		voc.CurrentWord.ExtendedIsValid = Me.chkIrregular.Checked

		voc.CurrentWord.Update()

		If bWordChanged Then
			Dim iLastIndex = lstWords.SelectedIndex
			Me.lstWords.Items.RemoveAt(iLastIndex)
			Me.lstWords.Items.Insert(iLastIndex, Me.txtWord.Text)
			Me.lstWords.SelectedIndex = iLastIndex
		End If

	End Sub

	Private Sub cmdNewMeaning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewMeaning.Click
		voc.CurrentWord.AddMeaning(Me.txtMeaning.Text)
		lstWordMeanings.Items.Add(Me.txtMeaning.Text)		  ' TODO error meldungen abfangen oder exception senden
	End Sub

	Private Sub cmdDeleteMeaning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteMeaning.Click
		If lstWordMeanings.SelectedIndex = -1 Then Exit Sub
		voc.CurrentWord.DeleteMeaning(Me.lstWordMeanings.SelectedItem)
		lstWordMeanings.Items.RemoveAt(lstWordMeanings.SelectedIndex)
	End Sub

	Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
		Save()
		voc.NewWord()
		Clear()
		Me.lstWordTypes.SelectedIndex = 0
		Me.lblWords.Text = voc.WordNumbers.Count
		Me.lstWords.Items.Add("neue Vokabel")
		Me.lstWords.SelectedIndex = lstWords.Items.Count - 1
		Me.txtWord.Text = "neue vokabel"
		Save()
		Me.txtWord.SelectAll()
		Me.txtWord.Focus()
	End Sub
End Class
