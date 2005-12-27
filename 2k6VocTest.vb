Public Class VocTest
	Inherits System.Windows.Forms.Form

	Dim voc As xlsVocTest
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
	Friend WithEvents lblWrong As System.Windows.Forms.Label
	Friend WithEvents lblWordCount As System.Windows.Forms.Label
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdCcedilleMajor As System.Windows.Forms.Button
	Friend WithEvents cmdCcedilleMinor As System.Windows.Forms.Button
	Friend WithEvents lblWordInfo As System.Windows.Forms.Label
	Friend WithEvents chkWaitAfterOK As System.Windows.Forms.CheckBox
	Friend WithEvents lblWord As System.Windows.Forms.Label
	Friend WithEvents txtMeaning2 As System.Windows.Forms.TextBox
	Friend WithEvents txtMeaning3 As System.Windows.Forms.TextBox
	Friend WithEvents txtMeaning1 As System.Windows.Forms.TextBox
	Friend WithEvents txtIrregular3 As System.Windows.Forms.TextBox
	Friend WithEvents txtIrregular2 As System.Windows.Forms.TextBox
	Friend WithEvents txtIrregular1 As System.Windows.Forms.TextBox
	Friend WithEvents cmdHelp As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents lblIrregularDescription1 As System.Windows.Forms.Label
	Friend WithEvents lblIrregularDescription2 As System.Windows.Forms.Label
	Friend WithEvents lblIrregularDescription3 As System.Windows.Forms.Label
	Friend WithEvents lblDescription As System.Windows.Forms.Label
	Friend WithEvents lblTestInfo As System.Windows.Forms.Label
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents cmdChangeSettings As System.Windows.Forms.Button
	Friend WithEvents cmdStartUnit As System.Windows.Forms.Button
	Friend WithEvents cmbUnits As System.Windows.Forms.ComboBox
	Friend WithEvents lblAdditionalInfo As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents lblTestInformation As System.Windows.Forms.Label
	Friend WithEvents cmbGroups As System.Windows.Forms.ComboBox
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.chkWaitAfterOK = New System.Windows.Forms.CheckBox
		Me.lblWord = New System.Windows.Forms.Label
		Me.lblWrong = New System.Windows.Forms.Label
		Me.txtMeaning2 = New System.Windows.Forms.TextBox
		Me.txtMeaning3 = New System.Windows.Forms.TextBox
		Me.txtMeaning1 = New System.Windows.Forms.TextBox
		Me.lblIrregularDescription3 = New System.Windows.Forms.Label
		Me.lblIrregularDescription2 = New System.Windows.Forms.Label
		Me.lblIrregularDescription1 = New System.Windows.Forms.Label
		Me.txtIrregular3 = New System.Windows.Forms.TextBox
		Me.txtIrregular2 = New System.Windows.Forms.TextBox
		Me.txtIrregular1 = New System.Windows.Forms.TextBox
		Me.cmdHelp = New System.Windows.Forms.Button
		Me.cmdCcedilleMajor = New System.Windows.Forms.Button
		Me.cmdCcedilleMinor = New System.Windows.Forms.Button
		Me.cmdCancel = New System.Windows.Forms.Button
		Me.lblWordCount = New System.Windows.Forms.Label
		Me.lblWordInfo = New System.Windows.Forms.Label
		Me.cmdOK = New System.Windows.Forms.Button
		Me.lblDescription = New System.Windows.Forms.Label
		Me.lblTestInfo = New System.Windows.Forms.Label
		Me.cmdChangeSettings = New System.Windows.Forms.Button
		Me.Label1 = New System.Windows.Forms.Label
		Me.cmbGroups = New System.Windows.Forms.ComboBox
		Me.cmbUnits = New System.Windows.Forms.ComboBox
		Me.cmdStartUnit = New System.Windows.Forms.Button
		Me.lblAdditionalInfo = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.GroupBox1 = New System.Windows.Forms.GroupBox
		Me.lblTestInformation = New System.Windows.Forms.Label
		Me.Label4 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.GroupBox2 = New System.Windows.Forms.GroupBox
		Me.GroupBox1.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.SuspendLayout()
		'
		'chkWaitAfterOK
		'
		Me.chkWaitAfterOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.chkWaitAfterOK.Location = New System.Drawing.Point(440, 264)
		Me.chkWaitAfterOK.Name = "chkWaitAfterOK"
		Me.chkWaitAfterOK.Size = New System.Drawing.Size(152, 16)
		Me.chkWaitAfterOK.TabIndex = 16
		Me.chkWaitAfterOK.Text = "Nach Bestätigung warten"
		'
		'lblWord
		'
		Me.lblWord.BackColor = System.Drawing.SystemColors.Control
		Me.lblWord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblWord.Enabled = False
		Me.lblWord.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.lblWord.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold)
		Me.lblWord.Location = New System.Drawing.Point(8, 16)
		Me.lblWord.Name = "lblWord"
		Me.lblWord.Size = New System.Drawing.Size(400, 40)
		Me.lblWord.TabIndex = 0
		Me.lblWord.Text = "word"
		Me.lblWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lblWrong
		'
		Me.lblWrong.Location = New System.Drawing.Point(440, 280)
		Me.lblWrong.Name = "lblWrong"
		Me.lblWrong.Size = New System.Drawing.Size(72, 24)
		Me.lblWrong.TabIndex = 11
		Me.lblWrong.Text = "wrong"
		'
		'txtMeaning2
		'
		Me.txtMeaning2.Enabled = False
		Me.txtMeaning2.Location = New System.Drawing.Point(8, 80)
		Me.txtMeaning2.Name = "txtMeaning2"
		Me.txtMeaning2.Size = New System.Drawing.Size(192, 20)
		Me.txtMeaning2.TabIndex = 2
		Me.txtMeaning2.Text = ""
		'
		'txtMeaning3
		'
		Me.txtMeaning3.Enabled = False
		Me.txtMeaning3.Location = New System.Drawing.Point(8, 120)
		Me.txtMeaning3.Name = "txtMeaning3"
		Me.txtMeaning3.Size = New System.Drawing.Size(192, 20)
		Me.txtMeaning3.TabIndex = 3
		Me.txtMeaning3.Text = ""
		'
		'txtMeaning1
		'
		Me.txtMeaning1.Enabled = False
		Me.txtMeaning1.Location = New System.Drawing.Point(8, 40)
		Me.txtMeaning1.Name = "txtMeaning1"
		Me.txtMeaning1.Size = New System.Drawing.Size(192, 20)
		Me.txtMeaning1.TabIndex = 1
		Me.txtMeaning1.Text = ""
		'
		'lblIrregularDescription3
		'
		Me.lblIrregularDescription3.Location = New System.Drawing.Point(208, 104)
		Me.lblIrregularDescription3.Name = "lblIrregularDescription3"
		Me.lblIrregularDescription3.Size = New System.Drawing.Size(192, 16)
		Me.lblIrregularDescription3.TabIndex = 21
		Me.lblIrregularDescription3.Text = "lblIrregularDescription3"
		'
		'lblIrregularDescription2
		'
		Me.lblIrregularDescription2.Location = New System.Drawing.Point(208, 64)
		Me.lblIrregularDescription2.Name = "lblIrregularDescription2"
		Me.lblIrregularDescription2.Size = New System.Drawing.Size(192, 16)
		Me.lblIrregularDescription2.TabIndex = 20
		Me.lblIrregularDescription2.Text = "lblIrregularDescription2"
		'
		'lblIrregularDescription1
		'
		Me.lblIrregularDescription1.Location = New System.Drawing.Point(208, 24)
		Me.lblIrregularDescription1.Name = "lblIrregularDescription1"
		Me.lblIrregularDescription1.Size = New System.Drawing.Size(192, 16)
		Me.lblIrregularDescription1.TabIndex = 19
		Me.lblIrregularDescription1.Text = "lblIrregularDescription1"
		'
		'txtIrregular3
		'
		Me.txtIrregular3.Enabled = False
		Me.txtIrregular3.Location = New System.Drawing.Point(208, 120)
		Me.txtIrregular3.Name = "txtIrregular3"
		Me.txtIrregular3.Size = New System.Drawing.Size(192, 20)
		Me.txtIrregular3.TabIndex = 6
		Me.txtIrregular3.Text = ""
		'
		'txtIrregular2
		'
		Me.txtIrregular2.Enabled = False
		Me.txtIrregular2.Location = New System.Drawing.Point(208, 80)
		Me.txtIrregular2.Name = "txtIrregular2"
		Me.txtIrregular2.Size = New System.Drawing.Size(192, 20)
		Me.txtIrregular2.TabIndex = 5
		Me.txtIrregular2.Text = ""
		'
		'txtIrregular1
		'
		Me.txtIrregular1.Enabled = False
		Me.txtIrregular1.Location = New System.Drawing.Point(208, 40)
		Me.txtIrregular1.Name = "txtIrregular1"
		Me.txtIrregular1.Size = New System.Drawing.Size(192, 20)
		Me.txtIrregular1.TabIndex = 4
		Me.txtIrregular1.Text = ""
		'
		'cmdHelp
		'
		Me.cmdHelp.Enabled = False
		Me.cmdHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdHelp.Location = New System.Drawing.Point(88, 352)
		Me.cmdHelp.Name = "cmdHelp"
		Me.cmdHelp.Size = New System.Drawing.Size(80, 23)
		Me.cmdHelp.TabIndex = 7
		Me.cmdHelp.Text = "Hilfe"
		'
		'cmdCcedilleMajor
		'
		Me.cmdCcedilleMajor.Enabled = False
		Me.cmdCcedilleMajor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdCcedilleMajor.Location = New System.Drawing.Point(8, 352)
		Me.cmdCcedilleMajor.Name = "cmdCcedilleMajor"
		Me.cmdCcedilleMajor.Size = New System.Drawing.Size(24, 23)
		Me.cmdCcedilleMajor.TabIndex = 14
		Me.cmdCcedilleMajor.Text = "Ç"
		'
		'cmdCcedilleMinor
		'
		Me.cmdCcedilleMinor.Enabled = False
		Me.cmdCcedilleMinor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdCcedilleMinor.Location = New System.Drawing.Point(48, 352)
		Me.cmdCcedilleMinor.Name = "cmdCcedilleMinor"
		Me.cmdCcedilleMinor.Size = New System.Drawing.Size(24, 23)
		Me.cmdCcedilleMinor.TabIndex = 15
		Me.cmdCcedilleMinor.Text = "ç"
		'
		'cmdCancel
		'
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdCancel.Location = New System.Drawing.Point(432, 376)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(72, 23)
		Me.cmdCancel.TabIndex = 13
		Me.cmdCancel.Text = "Schließen"
		'
		'lblWordCount
		'
		Me.lblWordCount.Location = New System.Drawing.Point(440, 240)
		Me.lblWordCount.Name = "lblWordCount"
		Me.lblWordCount.Size = New System.Drawing.Size(64, 24)
		Me.lblWordCount.TabIndex = 12
		Me.lblWordCount.Text = "Übungsinfo"
		'
		'lblWordInfo
		'
		Me.lblWordInfo.Location = New System.Drawing.Point(504, 240)
		Me.lblWordInfo.Name = "lblWordInfo"
		Me.lblWordInfo.Size = New System.Drawing.Size(56, 16)
		Me.lblWordInfo.TabIndex = 12
		Me.lblWordInfo.Text = "Wortinfo"
		'
		'cmdOK
		'
		Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.cmdOK.Enabled = False
		Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdOK.Location = New System.Drawing.Point(520, 376)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(72, 23)
		Me.cmdOK.TabIndex = 8
		Me.cmdOK.Text = "OK"
		'
		'lblDescription
		'
		Me.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.lblDescription.Location = New System.Drawing.Point(88, 120)
		Me.lblDescription.Name = "lblDescription"
		Me.lblDescription.Size = New System.Drawing.Size(320, 32)
		Me.lblDescription.TabIndex = 18
		Me.lblDescription.Text = "lblDescription"
		'
		'lblTestInfo
		'
		Me.lblTestInfo.Location = New System.Drawing.Point(560, 240)
		Me.lblTestInfo.Name = "lblTestInfo"
		Me.lblTestInfo.Size = New System.Drawing.Size(32, 16)
		Me.lblTestInfo.TabIndex = 19
		Me.lblTestInfo.Text = "###"
		'
		'cmdChangeSettings
		'
		Me.cmdChangeSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdChangeSettings.Location = New System.Drawing.Point(520, 112)
		Me.cmdChangeSettings.Name = "cmdChangeSettings"
		Me.cmdChangeSettings.Size = New System.Drawing.Size(72, 23)
		Me.cmdChangeSettings.TabIndex = 20
		Me.cmdChangeSettings.Text = "Ändern"
		'
		'Label1
		'
		Me.Label1.Location = New System.Drawing.Point(8, 24)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(100, 16)
		Me.Label1.TabIndex = 22
		Me.Label1.Text = "Beudeutungen:"
		'
		'cmbGroups
		'
		Me.cmbGroups.Location = New System.Drawing.Point(432, 16)
		Me.cmbGroups.Name = "cmbGroups"
		Me.cmbGroups.Size = New System.Drawing.Size(160, 21)
		Me.cmbGroups.TabIndex = 23
		Me.cmbGroups.Text = "cmbGroups"
		'
		'cmbUnits
		'
		Me.cmbUnits.Location = New System.Drawing.Point(432, 48)
		Me.cmbUnits.Name = "cmbUnits"
		Me.cmbUnits.Size = New System.Drawing.Size(160, 21)
		Me.cmbUnits.TabIndex = 24
		Me.cmbUnits.Text = "cmbUnits"
		'
		'cmdStartUnit
		'
		Me.cmdStartUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdStartUnit.Location = New System.Drawing.Point(520, 80)
		Me.cmdStartUnit.Name = "cmdStartUnit"
		Me.cmdStartUnit.Size = New System.Drawing.Size(72, 23)
		Me.cmdStartUnit.TabIndex = 25
		Me.cmdStartUnit.Text = "Beginnen"
		'
		'lblAdditionalInfo
		'
		Me.lblAdditionalInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblAdditionalInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.lblAdditionalInfo.Location = New System.Drawing.Point(88, 64)
		Me.lblAdditionalInfo.Name = "lblAdditionalInfo"
		Me.lblAdditionalInfo.Size = New System.Drawing.Size(320, 16)
		Me.lblAdditionalInfo.TabIndex = 26
		Me.lblAdditionalInfo.Text = "lblAdditionalInfo"
		'
		'Label2
		'
		Me.Label2.Location = New System.Drawing.Point(8, 64)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(80, 16)
		Me.Label2.TabIndex = 27
		Me.Label2.Text = "Zusatz:"
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.lblTestInformation)
		Me.GroupBox1.Controls.Add(Me.Label4)
		Me.GroupBox1.Controls.Add(Me.Label3)
		Me.GroupBox1.Controls.Add(Me.lblAdditionalInfo)
		Me.GroupBox1.Controls.Add(Me.Label2)
		Me.GroupBox1.Controls.Add(Me.lblWord)
		Me.GroupBox1.Controls.Add(Me.lblDescription)
		Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(416, 160)
		Me.GroupBox1.TabIndex = 28
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Abgefragtes Wort:"
		'
		'lblTestInformation
		'
		Me.lblTestInformation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblTestInformation.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.lblTestInformation.Location = New System.Drawing.Point(88, 88)
		Me.lblTestInformation.Name = "lblTestInformation"
		Me.lblTestInformation.Size = New System.Drawing.Size(320, 16)
		Me.lblTestInformation.TabIndex = 30
		Me.lblTestInformation.Text = "lblTestInformation"
		'
		'Label4
		'
		Me.Label4.Location = New System.Drawing.Point(8, 88)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(80, 16)
		Me.Label4.TabIndex = 29
		Me.Label4.Text = "Abfrage:"
		'
		'Label3
		'
		Me.Label3.Location = New System.Drawing.Point(8, 120)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(80, 16)
		Me.Label3.TabIndex = 28
		Me.Label3.Text = "Beschreibung:"
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.txtMeaning3)
		Me.GroupBox2.Controls.Add(Me.txtMeaning1)
		Me.GroupBox2.Controls.Add(Me.Label1)
		Me.GroupBox2.Controls.Add(Me.txtMeaning2)
		Me.GroupBox2.Controls.Add(Me.lblIrregularDescription3)
		Me.GroupBox2.Controls.Add(Me.lblIrregularDescription2)
		Me.GroupBox2.Controls.Add(Me.lblIrregularDescription1)
		Me.GroupBox2.Controls.Add(Me.txtIrregular3)
		Me.GroupBox2.Controls.Add(Me.txtIrregular2)
		Me.GroupBox2.Controls.Add(Me.txtIrregular1)
		Me.GroupBox2.Location = New System.Drawing.Point(8, 184)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(416, 152)
		Me.GroupBox2.TabIndex = 29
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Eingaben:"
		'
		'VocTest
		'
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
		Me.ClientSize = New System.Drawing.Size(602, 412)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.cmdStartUnit)
		Me.Controls.Add(Me.cmbUnits)
		Me.Controls.Add(Me.cmbGroups)
		Me.Controls.Add(Me.cmdChangeSettings)
		Me.Controls.Add(Me.lblTestInfo)
		Me.Controls.Add(Me.chkWaitAfterOK)
		Me.Controls.Add(Me.lblWordInfo)
		Me.Controls.Add(Me.cmdCcedilleMinor)
		Me.Controls.Add(Me.cmdCcedilleMajor)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.lblWordCount)
		Me.Controls.Add(Me.lblWrong)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.cmdHelp)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.Name = "VocTest"
		Me.Text = "Abfrage"
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox2.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private Sub VocTest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim db As New CDBOperation		  ' Datenbankoperationen
		db.Open(Application.StartupPath() & "\voc.mdb")		  ' Datenbank öffnen
		voc = New xlsVocTest(db)		' Datenbank zur Verfügung stellen
		ldfManagement = New xlsLDFManagement
		ldfManagement.LDFPath = Application.StartupPath()

		'Füllen der Listen
		Dim i As Integer		  ' Index
		For i = 1 To voc.Groups.Count
			Me.cmbGroups.Items.Add(voc.Groups.Item(i).Description)
		Next i

		' Falls möglich, erste auswählen
		If cmbGroups.Items.Count > 0 Then cmbGroups.SelectedIndex = 0

		Me.lblAdditionalInfo.Text = ""
		Me.lblDescription.Text = ""
		Me.lblIrregularDescription1.Text = ""
		Me.lblIrregularDescription2.Text = ""
		Me.lblIrregularDescription3.Text = ""
		Me.lblTestInformation.Text = ""
		Me.lblWord.Text = ""

		'ShowStatistic()
		'ShowWordInfo()
		'ShowTestInfo()
	End Sub

	Private Sub VocTest_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
		voc.StopTest()
	End Sub

	Private Sub cmbGroups_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroups.SelectedIndexChanged
		' Liste der Units füllen
		cmbUnits.Items.Clear()
		cmbUnits.Text = ""
		voc.SelectGroup(voc.GroupDescriptionToName(cmbGroups.SelectedItem))
		Me.cmbUnits.Items.AddRange(voc.UnitNames.ToArray)

		' LDF für diese Sprache wählen
		ldfManagement.SelectLD(voc.Language, voc.LDFType)

		' Wort auswählen
		If cmbUnits.Items.Count > 0 Then cmbUnits.SelectedIndex = 0
	End Sub

	Private Sub cmbUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnits.SelectedIndexChanged
		voc.SelectUnit(cmbUnits.SelectedIndex + 1)
	End Sub

	Private Sub EnableBeforeStart()
		Me.cmdOK.Enabled = True
		Me.txtIrregular1.Enabled = True
		Me.txtIrregular2.Enabled = True
		Me.txtIrregular3.Enabled = True
		Me.txtMeaning1.Enabled = True
		Me.txtMeaning2.Enabled = True
		Me.txtMeaning3.Enabled = True
		'Me.chkInfoModeEx.Enabled = True
		Me.lblWord.Enabled = True
		Me.cmdCcedilleMajor.Enabled = True
		Me.cmdCcedilleMinor.Enabled = True
		Me.cmdHelp.Enabled = True
	End Sub

	Private Sub DisableBeforeEnd()
		Me.cmdOK.Enabled = False
		Me.txtIrregular1.Enabled = False
		Me.txtIrregular2.Enabled = False
		Me.txtIrregular3.Enabled = False
		Me.txtMeaning1.Enabled = False
		Me.txtMeaning2.Enabled = False
		Me.txtMeaning3.Enabled = False
		'Me.chkInfoModeEx.Enabled = False
		Me.lblWord.Enabled = False
		Me.cmdCcedilleMajor.Enabled = False
		Me.cmdCcedilleMinor.Enabled = False
		Me.cmdHelp.Enabled = False
		Me.lblWord.Text = ""
	End Sub

	Private Sub StartTest(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartUnit.Click
		EnableBeforeStart()
		voc.StopTest()
		voc.Start()		  ' Wörter in dieser Unit abfragen
		ShowNewWord()

		Exit Sub
		'voc.NextWordMode = Me.m_iNextWordMode
		'voc.NextWordModeWrong = Me.m_iNextWordModeWrong
		'voc.ExtendedMode = Me.m_iTestExtendedMode

		'voc.FirstTryMode = m_bFirstTry
		'If m_bDescription Then Me.cmdHelp.Enabled = False

		EnableBeforeStart()
		voc.StopTest()
		'voc.Start(WordsInUnit(cmbUnits.SelectedIndex + 1, m_bRequestedOnly))
		'ShowNewWord()
		'ShowStatistic()
	End Sub

	Private Sub ShowNewWord()
		If voc.WordCountToDo = 0 Then
			'ShowStatistic()
			voc.StopTest()
			DisableBeforeEnd()
			MsgBox("Zuende gelernt ;D")
			'cmbGroup_SelectedIndexChanged(Nothing, Nothing)
			Exit Sub
		End If

		voc.NextWord()		  ' nächste Vokabel laden (oder erste)

		'If voc.CurrentWord.Pre <> "" Then lblWord.Text = voc.CurrentWord.Pre & " " Else lblWord.Text = ""
		Me.lblWord.Text = voc.CurrentWord.Word
		'If voc.CurrentWord.Pre <> "" Then lblWord.Text = voc.CurrentWord.Pre & " " Else lblWord.Text = ""
		Me.txtMeaning1.Text = ""
		Me.txtMeaning2.Text = ""
		Me.txtMeaning3.Text = ""
		Me.txtIrregular1.Text = ""
		Me.txtIrregular2.Text = ""
		Me.txtIrregular3.Text = ""
		Me.lblAdditionalInfo.Text = voc.CurrentWord.AdditionalTargetLangInfo
		Me.lblTestInformation.Text = ""
		Me.lblDescription.Text = ""

		' Grammatikfelder richtig anzeigen
		Me.lblIrregularDescription1.Text = ldfManagement.FormDescEx(voc.CurrentWord.WordType + 1).item(0) & ":"
		Me.lblIrregularDescription2.Text = ldfManagement.FormDescEx(voc.CurrentWord.WordType + 1).item(1) & ":"
		Me.lblIrregularDescription3.Text = ldfManagement.FormDescEx(voc.CurrentWord.WordType + 1).item(2) & ":"
		If lblIrregularDescription1.Text = ":" Then lblIrregularDescription1.Text = ""
		If lblIrregularDescription2.Text = ":" Then lblIrregularDescription2.Text = ""
		If lblIrregularDescription3.Text = ":" Then lblIrregularDescription3.Text = ""
		txtIrregular1.Enabled = lblIrregularDescription1.Text <> ""	   ' hier fehlt noch die auswahl ob nur bei unregelmäßigen angezeigt werden soll
		txtIrregular2.Enabled = lblIrregularDescription2.Text <> ""
		txtIrregular3.Enabled = lblIrregularDescription3.Text <> ""

		' Eingabefelder für Bedeutungen richtig anzeigen
		If voc.CurrentWord.Meaning.Length = 2 Or voc.CurrentWord.Meaning.Length = 1 Then
			Me.txtMeaning3.Enabled = False
		End If
		If voc.CurrentWord.Meaning.Length = 1 Then
			Me.txtMeaning2.Enabled = False
		End If

		' Erweiterte Wort-Info anzeigen
		Me.lblTestInformation.Text = "Alle Bedeutungen müssen angegeben werden!"
		Me.txtMeaning1.Focus()
	End Sub
















	Private Sub ShowStatistic()
		Dim sTestInfo As String
		sTestInfo = voc.WordCountAll & " zu testen. Davon: " & vbCrLf
		sTestInfo = sTestInfo & voc.WordCountToDo & " noch ausstehend." & vbCrLf
		sTestInfo = sTestInfo & voc.WordCountDone & " beantwortet." & vbCrLf
		sTestInfo = sTestInfo & voc.WordCountDoneRight & " sofort richtig." & vbCrLf
		sTestInfo &= voc.WordCountDoneWithHelpAll & " mit Hilfe gelöst." & vbCrLf
		sTestInfo &= voc.WordCountDoneWithHelp1 & " mit leichter, " & voc.WordCountDoneWithHelp2 & " mit mittlerer und" & vbCrLf
		sTestInfo &= voc.WordCountDoneWithHelp3 & " mit starker Hilfe." & vbCrLf
		sTestInfo = sTestInfo & voc.WordCountDoneFalse & " falsch beantwortet." & vbCrLf
		sTestInfo = sTestInfo & voc.WordCountDoneFAlseAllTrys & " falsche Versuche insgesamt."
		Me.lblWordCount.Text = sTestInfo
	End Sub

	Private Sub ShowWordInfo()
		Dim sWordInfo As String
		'wtWord = voc.GetWord(1)
		Select Case voc.Language
			Case "French"
				sWordInfo = "Sprache: Französisch"
			Case "English"
				sWordInfo = "Sprache: Englisch"
			Case "Latin"
				sWordInfo = "Sprache: Latein"
			Case Else
				sWordInfo = "Sprache: Unbekannt"
		End Select
		sWordInfo += vbCrLf

		Dim sList As Collection
		Try
			sList = voc.IrregularDescription
			If sList.Count > 0 Then Me.lblIrregularDescription1.Text = sList(1) & ":" Else Me.lblIrregularDescription1.Text = ""
			If sList.Count > 1 Then Me.lblIrregularDescription2.Text = sList(2) & ":" Else Me.lblIrregularDescription2.Text = ""
			If sList.Count > 2 Then Me.lblIrregularDescription3.Text = sList(3) & ":" Else Me.lblIrregularDescription3.Text = ""
			'	sWordInfo += "Wortart: " & voc.TypeText(wtWord.WordType)
		Catch
			sWordInfo += "Wortart: "
		End Try

		sWordInfo &= vbCrLf & "Zuletzt abgefragt: "

		'If wtWord Is Nothing Then
		sWordInfo &= "nie"
		'Else
		' TODO LastTested
		'If wtWord.LastTested = "01.01.1900" Then sWordInfo &= "nie" Else sWordInfo &= wtWord.LastTested
		'End If

		Me.lblWordInfo.Text = sWordInfo
	End Sub

	Private Sub ShowTestInfo()
		'		Me.lblTestInfo.Text = "Abfragereihenfolge " & xlsVocTest.NextWordModes(m_iNextWordMode) & vbCrLf
		'		Me.lblTestInfo.Text &= "Irreguläre " & xlsVocTest.ExtendedModes(m_iTestExtendedMode) & vbCrLf
		'		Me.lblTestInfo.Text &= xlsVocTest.NextWordModesWrong(m_iNextWordModeWrong) & vbCrLf
		Me.lblTestInfo.Text &= "Reihenfolge "
		Select Case 1		 'm_iTestMode
			Case xlsVocTestDirection.LanguageDefault
				Me.lblTestInfo.Text &= "Sprachstandard"
			Case xlsVocTestDirection.TestMeaning
				Me.lblTestInfo.Text &= "Wort zu Bedeutung"
			Case xlsVocTestDirection.TestWord
				Me.lblTestInfo.Text &= "Bedeutung zu Wort"
		End Select
		Me.lblTestInfo.Text &= vbCrLf
		'If m_bFirstTry Then Me.lblTestInfo.Text &= "Erster-Abfrage-Modus aktiviert" & vbCrLf Else Me.lblTestInfo.Text &= "Erster-Abfrage-Modus nicht aktiviert" & vbCrLf
		'If m_bOnlyUsed Then Me.lblTestInfo.Text &= "Nur benötigte Felder aktiviert" & vbCrLf Else Me.lblTestInfo.Text &= "Alle Felder aktiviert" & vbCrLf
		'If m_bDescription Then Me.lblTestInfo.Text &= "Beschreibung immer anzeigen" & vbCrLf Else Me.lblTestInfo.Text &= "Beschreibung manuell anzeigen" & vbCrLf
	End Sub

	Private Sub DisableElements()
		Dim iDisableCount As Integer = 0
		Me.txtMeaning1.Enabled = True
		Me.txtMeaning2.Enabled = True
		Me.txtMeaning3.Enabled = True
		Me.txtIrregular1.Enabled = True
		Me.txtIrregular2.Enabled = True
		Me.txtIrregular3.Enabled = True
		'If voc.Word Then
		'If voc.Answer1 = "" Then iDisableCount += 1
		'If voc.Answer2 = "" Then iDisableCount += 1
		'If voc.Answer3 = "" Then iDisableCount += 1
		If iDisableCount >= 1 Then Me.txtMeaning3.Enabled = False
		If iDisableCount >= 2 Then Me.txtMeaning2.Enabled = False
		If iDisableCount >= 3 Then Me.txtMeaning1.Enabled = False
		iDisableCount = 0
		If voc.ExtendedAnswer1 = "" Then iDisableCount += 1
		If voc.ExtendedAnswer2 = "" Then iDisableCount += 1
		If voc.ExtendedAnswer3 = "" Then iDisableCount += 1
		If iDisableCount >= 1 Then Me.txtIrregular3.Enabled = False
		If iDisableCount >= 2 Then Me.txtIrregular2.Enabled = False
		If iDisableCount >= 3 Then Me.txtIrregular1.Enabled = False
	End Sub


	Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
		Me.Close()
	End Sub


	Private Sub cmdCcedilleMajor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCcedilleMajor.Click
		Me.txtMeaning1.Text += "Ç"
	End Sub

	Private Sub cmdCcedilleMinor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCcedilleMinor.Click
		Me.txtMeaning1.Text += "ç"
	End Sub

	Private Sub txtMeaning1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMeaning1.TextChanged

	End Sub
End Class
