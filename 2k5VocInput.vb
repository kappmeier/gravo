Public Class VocInput
	Inherits System.Windows.Forms.Form
	Private voc As xlsVocInput
	Private ldf As xlsLanguageDefinition = New xlsLanguageDefinition

	Private db As New CDBOperation

	Private m_structGroupInfo As xlsVocInputGroupListInfo
	Private ctlFocus As TextBox
	Dim wtWord As xlsWord
	Dim wtWordList As xlsWordCollection

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
	Friend WithEvents lstUnits As System.Windows.Forms.ListBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents lstWords As System.Windows.Forms.ListBox
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents txtIrregular2 As System.Windows.Forms.TextBox
	Friend WithEvents txtIrregular1 As System.Windows.Forms.TextBox
	Friend WithEvents txtWord As System.Windows.Forms.TextBox
	Friend WithEvents txtPre As System.Windows.Forms.TextBox
	Friend WithEvents txtPost As System.Windows.Forms.TextBox
	Friend WithEvents txtIrregular3 As System.Windows.Forms.TextBox
	Friend WithEvents cmdSave As System.Windows.Forms.Button
	Friend WithEvents cmdNew As System.Windows.Forms.Button
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents Label7 As System.Windows.Forms.Label
	Friend WithEvents Label11 As System.Windows.Forms.Label
	Friend WithEvents Label12 As System.Windows.Forms.Label
	Friend WithEvents Label13 As System.Windows.Forms.Label
	Friend WithEvents chkIrregular As System.Windows.Forms.CheckBox
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents lstTypes As System.Windows.Forms.ListBox
	Friend WithEvents Label2 As System.Windows.Forms.Label

	' Für Windows-Formular-Designer erforderlich
	Private components As System.ComponentModel.Container

	'HINWEIS: Die folgende Prozedur ist für den Windows-Formular-Designer erforderlich
	'Sie kann mit dem Windows-Formular-Designer modifiziert werden.
	'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
	Friend WithEvents chkMustKnow As System.Windows.Forms.CheckBox
	Friend WithEvents Label10 As System.Windows.Forms.Label
	Friend WithEvents Label8 As System.Windows.Forms.Label
	Friend WithEvents lblUnit As System.Windows.Forms.Label
	Friend WithEvents nudChapter As System.Windows.Forms.NumericUpDown
	Friend WithEvents cmbUnits As System.Windows.Forms.ComboBox
	Friend WithEvents lblWordInUnit As System.Windows.Forms.Label
	Friend WithEvents lblCountVocabulary As System.Windows.Forms.Label
	Friend WithEvents lblCountUnits As System.Windows.Forms.Label
	Friend WithEvents cmdSearch As System.Windows.Forms.Button
	Friend WithEvents cmdDelete As System.Windows.Forms.Button
	Friend WithEvents Label14 As System.Windows.Forms.Label
	Friend WithEvents txtDescription As System.Windows.Forms.TextBox
	Friend WithEvents cmdNext As System.Windows.Forms.Button
	Friend WithEvents cmdLast As System.Windows.Forms.Button
	Friend WithEvents cmbGroup As System.Windows.Forms.ComboBox
	Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
	Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
	Friend WithEvents mnuInputNext As System.Windows.Forms.MenuItem
	Friend WithEvents mnuInputLast As System.Windows.Forms.MenuItem
	Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
	Friend WithEvents mnuInputNew As System.Windows.Forms.MenuItem
	Friend WithEvents txtAdditionalInfo As System.Windows.Forms.TextBox
	Friend WithEvents Label15 As System.Windows.Forms.Label
	Friend WithEvents lstWordMeanings As System.Windows.Forms.ListBox
	Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
	Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
	Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
	Friend WithEvents Label9 As System.Windows.Forms.Label
	Friend WithEvents txtMeaning As System.Windows.Forms.TextBox
	Friend WithEvents cmdNewMeaning As System.Windows.Forms.Button
	Friend WithEvents cmdDeleteMeaning As System.Windows.Forms.Button
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.txtPre = New System.Windows.Forms.TextBox
		Me.txtPost = New System.Windows.Forms.TextBox
		Me.Label13 = New System.Windows.Forms.Label
		Me.Label12 = New System.Windows.Forms.Label
		Me.txtIrregular3 = New System.Windows.Forms.TextBox
		Me.txtIrregular2 = New System.Windows.Forms.TextBox
		Me.txtIrregular1 = New System.Windows.Forms.TextBox
		Me.lstUnits = New System.Windows.Forms.ListBox
		Me.Label4 = New System.Windows.Forms.Label
		Me.Label5 = New System.Windows.Forms.Label
		Me.Label6 = New System.Windows.Forms.Label
		Me.Label7 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.lstWords = New System.Windows.Forms.ListBox
		Me.cmdCancel = New System.Windows.Forms.Button
		Me.cmdNew = New System.Windows.Forms.Button
		Me.chkIrregular = New System.Windows.Forms.CheckBox
		Me.txtMeaning = New System.Windows.Forms.TextBox
		Me.lstTypes = New System.Windows.Forms.ListBox
		Me.GroupBox1 = New System.Windows.Forms.GroupBox
		Me.Label9 = New System.Windows.Forms.Label
		Me.Label15 = New System.Windows.Forms.Label
		Me.txtAdditionalInfo = New System.Windows.Forms.TextBox
		Me.Label14 = New System.Windows.Forms.Label
		Me.txtDescription = New System.Windows.Forms.TextBox
		Me.chkMustKnow = New System.Windows.Forms.CheckBox
		Me.Label10 = New System.Windows.Forms.Label
		Me.Label8 = New System.Windows.Forms.Label
		Me.lblUnit = New System.Windows.Forms.Label
		Me.nudChapter = New System.Windows.Forms.NumericUpDown
		Me.cmbUnits = New System.Windows.Forms.ComboBox
		Me.lblWordInUnit = New System.Windows.Forms.Label
		Me.Label11 = New System.Windows.Forms.Label
		Me.txtWord = New System.Windows.Forms.TextBox
		Me.cmdSave = New System.Windows.Forms.Button
		Me.lblCountVocabulary = New System.Windows.Forms.Label
		Me.lblCountUnits = New System.Windows.Forms.Label
		Me.cmdSearch = New System.Windows.Forms.Button
		Me.cmdDelete = New System.Windows.Forms.Button
		Me.cmdNext = New System.Windows.Forms.Button
		Me.cmdLast = New System.Windows.Forms.Button
		Me.cmbGroup = New System.Windows.Forms.ComboBox
		Me.MainMenu1 = New System.Windows.Forms.MainMenu
		Me.MenuItem1 = New System.Windows.Forms.MenuItem
		Me.mnuInputNew = New System.Windows.Forms.MenuItem
		Me.MenuItem3 = New System.Windows.Forms.MenuItem
		Me.mnuInputLast = New System.Windows.Forms.MenuItem
		Me.mnuInputNext = New System.Windows.Forms.MenuItem
		Me.lstWordMeanings = New System.Windows.Forms.ListBox
		Me.GroupBox2 = New System.Windows.Forms.GroupBox
		Me.cmdNewMeaning = New System.Windows.Forms.Button
		Me.cmdDeleteMeaning = New System.Windows.Forms.Button
		Me.GroupBox3 = New System.Windows.Forms.GroupBox
		Me.GroupBox4 = New System.Windows.Forms.GroupBox
		Me.GroupBox1.SuspendLayout()
		CType(Me.nudChapter, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.GroupBox2.SuspendLayout()
		Me.GroupBox3.SuspendLayout()
		Me.GroupBox4.SuspendLayout()
		Me.SuspendLayout()
		'
		'txtPre
		'
		Me.txtPre.Location = New System.Drawing.Point(8, 32)
		Me.txtPre.MaxLength = 50
		Me.txtPre.Name = "txtPre"
		Me.txtPre.Size = New System.Drawing.Size(56, 20)
		Me.txtPre.TabIndex = 3
		Me.txtPre.Text = ""
		'
		'txtPost
		'
		Me.txtPost.Location = New System.Drawing.Point(256, 32)
		Me.txtPost.MaxLength = 50
		Me.txtPost.Name = "txtPost"
		Me.txtPost.Size = New System.Drawing.Size(56, 20)
		Me.txtPost.TabIndex = 5
		Me.txtPost.Text = ""
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
		Me.Label12.TabIndex = 16
		Me.Label12.Text = "Vor:"
		Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'txtIrregular3
		'
		Me.txtIrregular3.Enabled = False
		Me.txtIrregular3.Location = New System.Drawing.Point(136, 128)
		Me.txtIrregular3.MaxLength = 50
		Me.txtIrregular3.Name = "txtIrregular3"
		Me.txtIrregular3.Size = New System.Drawing.Size(176, 20)
		Me.txtIrregular3.TabIndex = 14
		Me.txtIrregular3.Text = ""
		'
		'txtIrregular2
		'
		Me.txtIrregular2.Enabled = False
		Me.txtIrregular2.Location = New System.Drawing.Point(136, 80)
		Me.txtIrregular2.MaxLength = 50
		Me.txtIrregular2.Name = "txtIrregular2"
		Me.txtIrregular2.Size = New System.Drawing.Size(176, 20)
		Me.txtIrregular2.TabIndex = 13
		Me.txtIrregular2.Text = ""
		'
		'txtIrregular1
		'
		Me.txtIrregular1.Enabled = False
		Me.txtIrregular1.Location = New System.Drawing.Point(136, 32)
		Me.txtIrregular1.MaxLength = 50
		Me.txtIrregular1.Name = "txtIrregular1"
		Me.txtIrregular1.Size = New System.Drawing.Size(176, 20)
		Me.txtIrregular1.TabIndex = 12
		Me.txtIrregular1.Text = ""
		'
		'lstUnits
		'
		Me.lstUnits.Location = New System.Drawing.Point(8, 64)
		Me.lstUnits.Name = "lstUnits"
		Me.lstUnits.Size = New System.Drawing.Size(176, 17)
		Me.lstUnits.TabIndex = 1
		'
		'Label4
		'
		Me.Label4.Location = New System.Drawing.Point(136, 64)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(88, 16)
		Me.Label4.TabIndex = 15
		Me.Label4.Text = "Unregelmäßig 2:"
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'Label5
		'
		Me.Label5.Location = New System.Drawing.Point(136, 24)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(176, 16)
		Me.Label5.TabIndex = 16
		Me.Label5.Text = "neue Bedeutung:"
		Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'Label6
		'
		Me.Label6.Location = New System.Drawing.Point(136, 112)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(88, 16)
		Me.Label6.TabIndex = 17
		Me.Label6.Text = "Unregelmäßig 3:"
		Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'Label7
		'
		Me.Label7.Location = New System.Drawing.Point(136, 16)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(88, 16)
		Me.Label7.TabIndex = 18
		Me.Label7.Text = "Unregelmäßig 1:"
		Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
		Me.Label2.TabIndex = 2
		Me.Label2.Text = "Vokabeln:"
		'
		'lstWords
		'
		Me.lstWords.Location = New System.Drawing.Point(8, 104)
		Me.lstWords.Name = "lstWords"
		Me.lstWords.Size = New System.Drawing.Size(176, 238)
		Me.lstWords.TabIndex = 2
		'
		'cmdCancel
		'
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdCancel.Location = New System.Drawing.Point(536, 392)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(72, 23)
		Me.cmdCancel.TabIndex = 22
		Me.cmdCancel.Text = "Schließen"
		'
		'cmdNew
		'
		Me.cmdNew.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.cmdNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdNew.Location = New System.Drawing.Point(536, 296)
		Me.cmdNew.Name = "cmdNew"
		Me.cmdNew.Size = New System.Drawing.Size(72, 23)
		Me.cmdNew.TabIndex = 19
		Me.cmdNew.Text = "&Neu"
		'
		'chkIrregular
		'
		Me.chkIrregular.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.chkIrregular.Location = New System.Drawing.Point(8, 136)
		Me.chkIrregular.Name = "chkIrregular"
		Me.chkIrregular.Size = New System.Drawing.Size(104, 16)
		Me.chkIrregular.TabIndex = 11
		Me.chkIrregular.Text = "Unregelmäßig"
		'
		'txtMeaning
		'
		Me.txtMeaning.Location = New System.Drawing.Point(136, 40)
		Me.txtMeaning.MaxLength = 50
		Me.txtMeaning.Name = "txtMeaning"
		Me.txtMeaning.Size = New System.Drawing.Size(176, 20)
		Me.txtMeaning.TabIndex = 9
		Me.txtMeaning.Text = ""
		'
		'lstTypes
		'
		Me.lstTypes.Location = New System.Drawing.Point(8, 32)
		Me.lstTypes.Name = "lstTypes"
		Me.lstTypes.Size = New System.Drawing.Size(120, 95)
		Me.lstTypes.TabIndex = 10
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Label9)
		Me.GroupBox1.Controls.Add(Me.lstTypes)
		Me.GroupBox1.Controls.Add(Me.Label7)
		Me.GroupBox1.Controls.Add(Me.Label6)
		Me.GroupBox1.Controls.Add(Me.Label4)
		Me.GroupBox1.Controls.Add(Me.txtIrregular2)
		Me.GroupBox1.Controls.Add(Me.txtIrregular1)
		Me.GroupBox1.Controls.Add(Me.txtIrregular3)
		Me.GroupBox1.Controls.Add(Me.chkIrregular)
		Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.GroupBox1.Location = New System.Drawing.Point(208, 256)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(320, 160)
		Me.GroupBox1.TabIndex = 12
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
		'Label15
		'
		Me.Label15.Location = New System.Drawing.Point(8, 64)
		Me.Label15.Name = "Label15"
		Me.Label15.Size = New System.Drawing.Size(64, 24)
		Me.Label15.TabIndex = 32
		Me.Label15.Text = "Zusatzinfo:"
		Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'txtAdditionalInfo
		'
		Me.txtAdditionalInfo.Location = New System.Drawing.Point(72, 64)
		Me.txtAdditionalInfo.MaxLength = 50
		Me.txtAdditionalInfo.Name = "txtAdditionalInfo"
		Me.txtAdditionalInfo.Size = New System.Drawing.Size(176, 20)
		Me.txtAdditionalInfo.TabIndex = 6
		Me.txtAdditionalInfo.Text = ""
		'
		'Label14
		'
		Me.Label14.Location = New System.Drawing.Point(8, 96)
		Me.Label14.Name = "Label14"
		Me.Label14.Size = New System.Drawing.Size(80, 16)
		Me.Label14.TabIndex = 30
		Me.Label14.Text = "Hilfe:"
		Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'txtDescription
		'
		Me.txtDescription.Location = New System.Drawing.Point(72, 96)
		Me.txtDescription.MaxLength = 50
		Me.txtDescription.Name = "txtDescription"
		Me.txtDescription.Size = New System.Drawing.Size(176, 20)
		Me.txtDescription.TabIndex = 7
		Me.txtDescription.Text = ""
		'
		'chkMustKnow
		'
		Me.chkMustKnow.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.chkMustKnow.Location = New System.Drawing.Point(536, 152)
		Me.chkMustKnow.Name = "chkMustKnow"
		Me.chkMustKnow.Size = New System.Drawing.Size(56, 16)
		Me.chkMustKnow.TabIndex = 17
		Me.chkMustKnow.Text = "Pflicht"
		'
		'Label10
		'
		Me.Label10.Location = New System.Drawing.Point(536, 16)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(64, 16)
		Me.Label10.TabIndex = 28
		Me.Label10.Text = "Unit:"
		'
		'Label8
		'
		Me.Label8.Location = New System.Drawing.Point(536, 88)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(56, 16)
		Me.Label8.TabIndex = 27
		Me.Label8.Text = "Chapter:"
		'
		'lblUnit
		'
		Me.lblUnit.Location = New System.Drawing.Point(536, 56)
		Me.lblUnit.Name = "lblUnit"
		Me.lblUnit.Size = New System.Drawing.Size(72, 16)
		Me.lblUnit.TabIndex = 26
		Me.lblUnit.Text = "#Unit"
		Me.lblUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'nudChapter
		'
		Me.nudChapter.Location = New System.Drawing.Point(536, 104)
		Me.nudChapter.Name = "nudChapter"
		Me.nudChapter.Size = New System.Drawing.Size(72, 20)
		Me.nudChapter.TabIndex = 16
		'
		'cmbUnits
		'
		Me.cmbUnits.DropDownWidth = 121
		Me.cmbUnits.Location = New System.Drawing.Point(536, 32)
		Me.cmbUnits.Name = "cmbUnits"
		Me.cmbUnits.Size = New System.Drawing.Size(72, 21)
		Me.cmbUnits.TabIndex = 15
		'
		'lblWordInUnit
		'
		Me.lblWordInUnit.Location = New System.Drawing.Point(8, 344)
		Me.lblWordInUnit.Name = "lblWordInUnit"
		Me.lblWordInUnit.Size = New System.Drawing.Size(88, 16)
		Me.lblWordInUnit.TabIndex = 24
		Me.lblWordInUnit.Text = "# Word in Unit"
		'
		'Label11
		'
		Me.Label11.Location = New System.Drawing.Point(72, 16)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(176, 16)
		Me.Label11.TabIndex = 16
		Me.Label11.Text = "Vokabel:"
		Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'txtWord
		'
		Me.txtWord.Location = New System.Drawing.Point(72, 32)
		Me.txtWord.MaxLength = 50
		Me.txtWord.Name = "txtWord"
		Me.txtWord.Size = New System.Drawing.Size(176, 20)
		Me.txtWord.TabIndex = 4
		Me.txtWord.Text = ""
		'
		'cmdSave
		'
		Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdSave.Location = New System.Drawing.Point(536, 264)
		Me.cmdSave.Name = "cmdSave"
		Me.cmdSave.Size = New System.Drawing.Size(72, 23)
		Me.cmdSave.TabIndex = 18
		Me.cmdSave.Text = "Speichern"
		'
		'lblCountVocabulary
		'
		Me.lblCountVocabulary.AutoSize = True
		Me.lblCountVocabulary.Location = New System.Drawing.Point(80, 88)
		Me.lblCountVocabulary.Name = "lblCountVocabulary"
		Me.lblCountVocabulary.Size = New System.Drawing.Size(34, 16)
		Me.lblCountVocabulary.TabIndex = 19
		Me.lblCountVocabulary.Text = "Count"
		Me.lblCountVocabulary.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblCountUnits
		'
		Me.lblCountUnits.AutoSize = True
		Me.lblCountUnits.Location = New System.Drawing.Point(80, 48)
		Me.lblCountUnits.Name = "lblCountUnits"
		Me.lblCountUnits.Size = New System.Drawing.Size(34, 16)
		Me.lblCountUnits.TabIndex = 20
		Me.lblCountUnits.Text = "Count"
		Me.lblCountUnits.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'cmdSearch
		'
		Me.cmdSearch.Enabled = False
		Me.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdSearch.Location = New System.Drawing.Point(536, 360)
		Me.cmdSearch.Name = "cmdSearch"
		Me.cmdSearch.Size = New System.Drawing.Size(72, 23)
		Me.cmdSearch.TabIndex = 21
		Me.cmdSearch.Text = "Suchen"
		'
		'cmdDelete
		'
		Me.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdDelete.Location = New System.Drawing.Point(536, 328)
		Me.cmdDelete.Name = "cmdDelete"
		Me.cmdDelete.Size = New System.Drawing.Size(72, 23)
		Me.cmdDelete.TabIndex = 20
		Me.cmdDelete.Text = "&Löschen"
		'
		'cmdNext
		'
		Me.cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdNext.Location = New System.Drawing.Point(152, 376)
		Me.cmdNext.Name = "cmdNext"
		Me.cmdNext.Size = New System.Drawing.Size(24, 23)
		Me.cmdNext.TabIndex = 24
		Me.cmdNext.Text = ">"
		'
		'cmdLast
		'
		Me.cmdLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdLast.Location = New System.Drawing.Point(120, 376)
		Me.cmdLast.Name = "cmdLast"
		Me.cmdLast.Size = New System.Drawing.Size(24, 23)
		Me.cmdLast.TabIndex = 23
		Me.cmdLast.Text = "<"
		'
		'cmbGroup
		'
		Me.cmbGroup.Location = New System.Drawing.Point(8, 16)
		Me.cmbGroup.Name = "cmbGroup"
		Me.cmbGroup.Size = New System.Drawing.Size(176, 21)
		Me.cmbGroup.TabIndex = 0
		Me.cmbGroup.Text = "groups"
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
		'lstWordMeanings
		'
		Me.lstWordMeanings.Location = New System.Drawing.Point(8, 24)
		Me.lstWordMeanings.Name = "lstWordMeanings"
		Me.lstWordMeanings.Size = New System.Drawing.Size(120, 69)
		Me.lstWordMeanings.TabIndex = 8
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.cmbGroup)
		Me.GroupBox2.Controls.Add(Me.lstWords)
		Me.GroupBox2.Controls.Add(Me.lstUnits)
		Me.GroupBox2.Controls.Add(Me.Label1)
		Me.GroupBox2.Controls.Add(Me.Label2)
		Me.GroupBox2.Controls.Add(Me.lblCountVocabulary)
		Me.GroupBox2.Controls.Add(Me.lblCountUnits)
		Me.GroupBox2.Controls.Add(Me.lblWordInUnit)
		Me.GroupBox2.Controls.Add(Me.cmdNext)
		Me.GroupBox2.Controls.Add(Me.cmdLast)
		Me.GroupBox2.Location = New System.Drawing.Point(8, 8)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(192, 408)
		Me.GroupBox2.TabIndex = 28
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Allgemein"
		'
		'cmdNewMeaning
		'
		Me.cmdNewMeaning.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdNewMeaning.Location = New System.Drawing.Point(136, 72)
		Me.cmdNewMeaning.Name = "cmdNewMeaning"
		Me.cmdNewMeaning.Size = New System.Drawing.Size(80, 24)
		Me.cmdNewMeaning.TabIndex = 33
		Me.cmdNewMeaning.Text = "Übernehmen"
		'
		'cmdDeleteMeaning
		'
		Me.cmdDeleteMeaning.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdDeleteMeaning.Location = New System.Drawing.Point(232, 72)
		Me.cmdDeleteMeaning.Name = "cmdDeleteMeaning"
		Me.cmdDeleteMeaning.Size = New System.Drawing.Size(80, 24)
		Me.cmdDeleteMeaning.TabIndex = 34
		Me.cmdDeleteMeaning.Text = "löschen"
		'
		'GroupBox3
		'
		Me.GroupBox3.Controls.Add(Me.Label5)
		Me.GroupBox3.Controls.Add(Me.cmdDeleteMeaning)
		Me.GroupBox3.Controls.Add(Me.lstWordMeanings)
		Me.GroupBox3.Controls.Add(Me.cmdNewMeaning)
		Me.GroupBox3.Controls.Add(Me.txtMeaning)
		Me.GroupBox3.Location = New System.Drawing.Point(208, 144)
		Me.GroupBox3.Name = "GroupBox3"
		Me.GroupBox3.Size = New System.Drawing.Size(320, 104)
		Me.GroupBox3.TabIndex = 35
		Me.GroupBox3.TabStop = False
		Me.GroupBox3.Text = "Bedeutung:"
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
		Me.GroupBox4.Location = New System.Drawing.Point(208, 8)
		Me.GroupBox4.Name = "GroupBox4"
		Me.GroupBox4.Size = New System.Drawing.Size(320, 128)
		Me.GroupBox4.TabIndex = 36
		Me.GroupBox4.TabStop = False
		Me.GroupBox4.Text = "Vokabelinfo:"
		'
		'VocInput
		'
		Me.AcceptButton = Me.cmdNew
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(618, 423)
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
		Me.Controls.Add(Me.cmbUnits)
		Me.Controls.Add(Me.chkMustKnow)
		Me.Controls.Add(Me.cmdSearch)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.Menu = Me.MainMenu1
		Me.Name = "VocInput"
		Me.Text = "Eingabe"
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		Me.GroupBox1.ResumeLayout(False)
		CType(Me.nudChapter, System.ComponentModel.ISupportInitialize).EndInit()
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox3.ResumeLayout(False)
		Me.GroupBox4.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private Sub VocInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim i As Integer
		db.Open(Application.StartupPath() & "\voc.mdb")
		voc = New xlsVocInput(db)

		For i = 0 To voc.Groups.Count - 1
			Me.cmbGroup.Items.Add(voc.Groups(i).Description)
		Next i
	End Sub

	Private Sub lstUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstUnits.SelectedIndexChanged
		' Die Vokabelliste Aktualisieren
		UpdateWordList()
	End Sub

	Private Sub UpdateWordList()
		Dim i As Int32
		Dim iUnit As Integer = voc.UnitsInGroup(lstUnits.SelectedIndex + 1).Number
		lstWords.Items.Clear()
		wtWordList = Nothing
		wtWordList = voc.WordsInUnit(iUnit)
		For i = 1 To wtWordList.Count
			lstWords.Items.Add(wtWordList(i).Word)
		Next i
		If (lstWords.Items.Count) > 0 Then
			lstWords.SelectedIndex = 0
		End If
		Me.lblCountVocabulary.Text = Me.lstWords.Items.Count
	End Sub

	Private Sub ChangeWord(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstWords.SelectedIndexChanged
		' Ändert das momentan angezeigte Wort
		Dim iWordNumber As Int32
		If lstWords.SelectedIndex = -1 Then Exit Sub
		iWordNumber = wtWordList(lstWords.SelectedIndex + 1).WordNumber

		wtWord = voc.GetWord(iWordNumber)
		lblWordInUnit.Text = "Vokabel " & wtWord.WordInUnit
		cmbUnits.SelectedItem = wtWord.UnitName
		lblUnit.Text = "Lektion " & wtWord.UnitNumber
		nudChapter.Value = wtWord.Chapter

		txtWord.Text = wtWord.Word			   'voc.Word
		txtPre.Text = wtWord.Pre						'voc.Pre
		txtPost.Text = wtWord.Post		   ' voc.Post
		Dim i As Integer
		Dim sAddstring As String
		lstWordMeanings.Items.Clear()
		For i = 1 To 3
			sAddstring = wtWord.Meaning.Item(i)
			If sAddstring <> "" Then Me.lstWordMeanings.Items.Add(sAddstring)
		Next i
		'txtMeaning1.Text = wtWord.Meaning1		 'voc.Meaning1
		'txtMeaning2.Text = wtWord.Meaning2		  'voc.Meaning2
		'txtMeaning3.Text = wtWord.Meaning3		  'voc.Meaning3
		txtIrregular1.Text = wtWord.Extended1		  'voc.Irregular1
		txtIrregular2.Text = wtWord.Extended2		  ' voc.Irregular2
		txtIrregular3.Text = wtWord.Extended3		  'voc.Irregular3
		txtDescription.Text = wtWord.Description		  'voc.Description
		chkIrregular.Checked = wtWord.ExtendedIsValid		  ' voc.IrregularForm
		chkMustKnow.Checked = wtWord.MustKnow
		nudChapter.Value = wtWord.Chapter
		lstTypes.SelectedIndex = wtWord.WordType
		txtAdditionalInfo.Text = wtWord.AdditionalTargetLangInfo

		' Zeige die Irregulär-Informationen, gemäß aktueller LDF an:
		If Not wtWord.ExtendedIsValid Then
			txtIrregular1.Text = ldf.CreateExtended1(wtWord)
			txtIrregular2.Text = ldf.CreateExtended2(wtWord)
			txtIrregular3.Text = ldf.CreateExtended3(wtWord)
		End If

	End Sub

	Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
		Dim iIndex = lstWords.SelectedIndex
		SaveData()
		lstWords.SelectedIndex = iIndex
		txtWord.Focus()
	End Sub

	Private Sub SaveData()
		If Me.lstUnits.Items.Count = -1 Then Exit Sub ' Keine Units definiert
		Dim iIndex = lstWords.SelectedIndex
		If wtWord.Word <> Trim(txtWord.Text) Then
			lstWords.Items.RemoveAt(iIndex)
			lstWords.Items.Insert(iIndex, Trim(txtWord.Text))
		End If
		wtWord.Word = txtWord.Text
		wtWord.Pre = txtPre.Text
		wtWord.Post = txtPost.Text
		'		'		'		'wtWord.Meaning1 = txtMeaning1.Text
		'		'		'		'wtWord.Meaning2 = txtMeaning2.Text
		'		'		'		'wtWord.Meaning3 = txtMeaning3.Text
		If chkIrregular.Checked = True Then
			wtWord.ExtendedIsValid = True
			wtWord.Extended1 = txtIrregular1.Text
			wtWord.Extended2 = txtIrregular2.Text
			wtWord.Extended3 = txtIrregular3.Text
		Else
			wtWord.ExtendedIsValid = False
		End If
		wtWord.Description() = txtDescription.Text
		wtWord.Chapter = nudChapter.Value()
		wtWord.MustKnow = chkMustKnow.Checked
		wtWord.WordType = lstTypes.SelectedIndex
		wtWord.AdditionalTargetLangInfo = txtAdditionalInfo.Text
	End Sub

	Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
		Dim iChapterNumber = nudChapter.Text
		If Me.lstUnits.Items.Count = -1 Then Exit Sub ' Keine Units definiert
		Dim iWordNumber As Integer
		If Me.lstWords.Items.Count <= 0 Then
			iWordNumber = voc.NewWord(Me.lstUnits.SelectedIndex + 1)
		Else
			SaveData()
			Application.DoEvents()
			iWordNumber = voc.NewWord()
		End If

		Dim wtNew As xlsWordInformation
		Dim sGroup As String = voc.Groups(cmbGroup.SelectedIndex).Table
		wtNew = New xlsWordInformation(db, iWordNumber, sGroup)

		wtWordList.Add(iWordNumber, wtNew.Word, sGroup)
		lstWords.Items.Add(wtNew.Word)

		Me.lblCountVocabulary.Text = Me.lstWords.Items.Count

		lstWords.SelectedIndex = lstWords.Items.Count - 1
		Me.lblCountVocabulary.Text = lstWords.Items.Count
		nudChapter.Text = iChapterNumber
		txtWord.Focus()
	End Sub

	Private Sub chkIrregular_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIrregular.CheckedChanged
		If chkIrregular.Checked Then
			txtIrregular1.Enabled = True
			txtIrregular2.Enabled = True
			txtIrregular3.Enabled = True
		Else
			txtIrregular1.Enabled = False
			txtIrregular2.Enabled = False
			txtIrregular3.Enabled = False
		End If
	End Sub

	WriteOnly Property Group() As xlsVocInputGroupListInfo
		Set(ByVal Selected As xlsVocInputGroupListInfo)
			m_structGroupInfo = Selected
		End Set
	End Property

	Private Sub WordInput_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
		If Not (voc Is Nothing) Then voc.Close()
	End Sub

	Private Sub cmbUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnits.SelectedIndexChanged
		If cmbUnits.SelectedItem <> wtWord.UnitName Then
			Dim sOldWord = voc.Word
			wtWord.UnitName = cmbUnits.SelectedItem
			lstUnits.SelectedItem = cmbUnits.SelectedItem
			lstWords.SelectedItem = sOldWord
		End If
	End Sub

	Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
		Dim iOldSelected = Me.lstWords.SelectedIndex
		If Me.lstWords.Items.Count >= 1 Then voc.Delete()
		Me.lstUnits_SelectedIndexChanged(sender, e)		' GEANDERT von ME zu SENDER
		If iOldSelected <= Me.lstWords.Items.Count - 1 Then
			Me.lstWords.SelectedIndex = iOldSelected
		Else
			If iOldSelected >= 1 Then Me.lstWords.SelectedIndex = iOldSelected - 1
		End If
	End Sub

	Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
		Me.Close()
	End Sub

	Private Sub NextVoc(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext.Click
		SaveData()
		If Me.lstWords.SelectedIndex = Me.lstWords.Items.Count - 1 Then Exit Sub
		If Me.lstWords.Items.Count = 0 Then Exit Sub
		Me.lstWords.SelectedIndex += 1
		If Not (ctlFocus Is Nothing) Then ctlFocus.Focus()
	End Sub

	Private Sub LastVoc(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLast.Click
		SaveData()
		If Me.lstWords.SelectedIndex = 0 Then Exit Sub
		If Me.lstWords.Items.Count = 0 Then Exit Sub
		Me.lstWords.SelectedIndex -= 1
	End Sub

	Private Sub cmbGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroup.SelectedIndexChanged
		' Neue Sprache auswählen
		Dim i As Short
		voc.SelectTable(voc.Groups(cmbGroup.SelectedIndex).Table)

		' Anhand der Sprache, die richtige LDF-Datei auswählen:
		ldf.LoadLDF(voc.Language)

		lstTypes.Items.Clear()
		Dim aTypes As ArrayList
		aTypes = voc.Types
		For i = 0 To aTypes.Count - 1
			lstTypes.Items.Add(aTypes(i))
		Next i
		lstUnits.Items.Clear()
		For i = 1 To voc.UnitsInGroup.Count
			lstUnits.Items.Add(voc.UnitsInGroup(i).Name)
			cmbUnits.Items.Add(voc.UnitsInGroup(i).Name)
		Next i
		If lstUnits.Items.Count >= 1 Then lstUnits.SelectedIndex = 0
		Me.lblCountUnits.Text = lstUnits.Items.Count
	End Sub

	Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Dim structTest As TestUnits
		Dim cTestUnits As New Collection

		structTest.Unit = lstUnits.SelectedIndex + 1		  '                  lstUnits.SelectedItem
		structTest.Table = voc.Groups(cmbGroup.SelectedIndex).Table
		cTestUnits.Add(structTest)
	End Sub

	Private Sub mnuInputNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInputNext.Click
		NextVoc(sender, e)
	End Sub

	Private Sub mnuInputLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInputLast.Click
		LastVoc(sender, e)
	End Sub

	Private Sub mnuInputNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInputNew.Click
		cmdNew_Click(sender, e)
	End Sub

	Private Sub txtDescription_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescription.GotFocus
		ctlFocus = Me.txtDescription
	End Sub

	Private Sub cmdNewMeaning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewMeaning.Click
		wtWord.Meaning.Add(Me.txtMeaning.Text)
		Me.lstWordMeanings.Items.Add(Me.txtMeaning.Text)
	End Sub
End Class
