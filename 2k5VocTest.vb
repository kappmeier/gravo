Public Class VocTest
	Inherits System.Windows.Forms.Form
	Private voc As xlsVocTest
	Private wtWord As xlsWordStats
	Private db As New CDBOperation

	Friend WithEvents lblWrong As System.Windows.Forms.Label
	Const LENGTH_WORD = 50
	Private m_cTestUnits As Collection
	Friend WithEvents lblWordCount As System.Windows.Forms.Label
	Private m_sType As String
	Dim m_iNextWordMode As Integer = 1
	Dim m_iNextWordModeWrong As Integer = 4
	Dim m_iTestExtendedMode As xlsVocTestExtended = xlsVocTestExtended.Always
	Dim m_iTestMode As xlsVocTestDirection = xlsVocTestDirection.LanguageDefault
	Dim m_bRequestedOnly As Boolean = True
	Dim m_bFirstTry As Boolean = True
	Dim m_bOnlyUsed As Boolean = True
	Dim m_bDescription As Boolean = False
	Dim bUsedHelp1 As Boolean = False
	Dim bUsedHelp2 As Boolean = False
	Dim bUsedHelp3 As Boolean = False
	Dim bFirstPaint As Boolean = True
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdCcedilleMajor As System.Windows.Forms.Button
	Friend WithEvents cmdCcedilleMinor As System.Windows.Forms.Button
	Friend WithEvents lblWordInfo As System.Windows.Forms.Label
	Friend WithEvents chkWaitAfterOK As System.Windows.Forms.CheckBox

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
	Friend WithEvents lblWord As System.Windows.Forms.Label
	Friend WithEvents txtMeaning2 As System.Windows.Forms.TextBox
	Friend WithEvents txtMeaning3 As System.Windows.Forms.TextBox
	Friend WithEvents txtMeaning1 As System.Windows.Forms.TextBox
	Friend WithEvents txtIrregular3 As System.Windows.Forms.TextBox
	Friend WithEvents txtIrregular2 As System.Windows.Forms.TextBox
	Friend WithEvents txtIrregular1 As System.Windows.Forms.TextBox
	Friend WithEvents cmdHelp As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button

	' Für Windows-Formular-Designer erforderlich
	Private components As System.ComponentModel.Container

	'HINWEIS: Die folgende Prozedur ist für den Windows-Formular-Designer erforderlich
	'Sie kann mit dem Windows-Formular-Designer modifiziert werden.
	'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
	Friend WithEvents lblIrregularDescription1 As System.Windows.Forms.Label
	Friend WithEvents lblIrregularDescription2 As System.Windows.Forms.Label
	Friend WithEvents lblIrregularDescription3 As System.Windows.Forms.Label
	Friend WithEvents lblDescription As System.Windows.Forms.Label
	Friend WithEvents lblTestInfo As System.Windows.Forms.Label
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents cmdChangeSettings As System.Windows.Forms.Button
	Friend WithEvents cmdStartUnit As System.Windows.Forms.Button
	Friend WithEvents cmbGroup As System.Windows.Forms.ComboBox
	Friend WithEvents cmbUnits As System.Windows.Forms.ComboBox
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
		Me.cmbGroup = New System.Windows.Forms.ComboBox
		Me.cmbUnits = New System.Windows.Forms.ComboBox
		Me.cmdStartUnit = New System.Windows.Forms.Button
		Me.SuspendLayout()
		'
		'chkWaitAfterOK
		'
		Me.chkWaitAfterOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.chkWaitAfterOK.Location = New System.Drawing.Point(216, 336)
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
		Me.lblWord.Location = New System.Drawing.Point(8, 24)
		Me.lblWord.Name = "lblWord"
		Me.lblWord.Size = New System.Drawing.Size(400, 40)
		Me.lblWord.TabIndex = 0
		Me.lblWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lblWrong
		'
		Me.lblWrong.Location = New System.Drawing.Point(8, 8)
		Me.lblWrong.Name = "lblWrong"
		Me.lblWrong.Size = New System.Drawing.Size(400, 16)
		Me.lblWrong.TabIndex = 11
		'
		'txtMeaning2
		'
		Me.txtMeaning2.Enabled = False
		Me.txtMeaning2.Location = New System.Drawing.Point(8, 168)
		Me.txtMeaning2.Name = "txtMeaning2"
		Me.txtMeaning2.Size = New System.Drawing.Size(192, 20)
		Me.txtMeaning2.TabIndex = 2
		Me.txtMeaning2.Text = ""
		'
		'txtMeaning3
		'
		Me.txtMeaning3.Enabled = False
		Me.txtMeaning3.Location = New System.Drawing.Point(8, 208)
		Me.txtMeaning3.Name = "txtMeaning3"
		Me.txtMeaning3.Size = New System.Drawing.Size(192, 20)
		Me.txtMeaning3.TabIndex = 3
		Me.txtMeaning3.Text = ""
		'
		'txtMeaning1
		'
		Me.txtMeaning1.Enabled = False
		Me.txtMeaning1.Location = New System.Drawing.Point(8, 128)
		Me.txtMeaning1.Name = "txtMeaning1"
		Me.txtMeaning1.Size = New System.Drawing.Size(192, 20)
		Me.txtMeaning1.TabIndex = 1
		Me.txtMeaning1.Text = ""
		'
		'lblIrregularDescription3
		'
		Me.lblIrregularDescription3.AutoSize = True
		Me.lblIrregularDescription3.Location = New System.Drawing.Point(216, 192)
		Me.lblIrregularDescription3.Name = "lblIrregularDescription3"
		Me.lblIrregularDescription3.Size = New System.Drawing.Size(0, 16)
		Me.lblIrregularDescription3.TabIndex = 21
		'
		'lblIrregularDescription2
		'
		Me.lblIrregularDescription2.AutoSize = True
		Me.lblIrregularDescription2.Location = New System.Drawing.Point(216, 152)
		Me.lblIrregularDescription2.Name = "lblIrregularDescription2"
		Me.lblIrregularDescription2.Size = New System.Drawing.Size(0, 16)
		Me.lblIrregularDescription2.TabIndex = 20
		'
		'lblIrregularDescription1
		'
		Me.lblIrregularDescription1.AutoSize = True
		Me.lblIrregularDescription1.Location = New System.Drawing.Point(216, 112)
		Me.lblIrregularDescription1.Name = "lblIrregularDescription1"
		Me.lblIrregularDescription1.Size = New System.Drawing.Size(0, 16)
		Me.lblIrregularDescription1.TabIndex = 19
		'
		'txtIrregular3
		'
		Me.txtIrregular3.Enabled = False
		Me.txtIrregular3.Location = New System.Drawing.Point(216, 208)
		Me.txtIrregular3.Name = "txtIrregular3"
		Me.txtIrregular3.Size = New System.Drawing.Size(192, 20)
		Me.txtIrregular3.TabIndex = 6
		Me.txtIrregular3.Text = ""
		'
		'txtIrregular2
		'
		Me.txtIrregular2.Enabled = False
		Me.txtIrregular2.Location = New System.Drawing.Point(216, 168)
		Me.txtIrregular2.Name = "txtIrregular2"
		Me.txtIrregular2.Size = New System.Drawing.Size(192, 20)
		Me.txtIrregular2.TabIndex = 5
		Me.txtIrregular2.Text = ""
		'
		'txtIrregular1
		'
		Me.txtIrregular1.Enabled = False
		Me.txtIrregular1.Location = New System.Drawing.Point(216, 128)
		Me.txtIrregular1.Name = "txtIrregular1"
		Me.txtIrregular1.Size = New System.Drawing.Size(192, 20)
		Me.txtIrregular1.TabIndex = 4
		Me.txtIrregular1.Text = ""
		'
		'cmdHelp
		'
		Me.cmdHelp.Enabled = False
		Me.cmdHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdHelp.Location = New System.Drawing.Point(216, 240)
		Me.cmdHelp.Name = "cmdHelp"
		Me.cmdHelp.Size = New System.Drawing.Size(192, 23)
		Me.cmdHelp.TabIndex = 7
		Me.cmdHelp.Text = "Beschreibung anzeigen"
		'
		'cmdCcedilleMajor
		'
		Me.cmdCcedilleMajor.Enabled = False
		Me.cmdCcedilleMajor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdCcedilleMajor.Location = New System.Drawing.Point(216, 272)
		Me.cmdCcedilleMajor.Name = "cmdCcedilleMajor"
		Me.cmdCcedilleMajor.Size = New System.Drawing.Size(24, 23)
		Me.cmdCcedilleMajor.TabIndex = 14
		Me.cmdCcedilleMajor.Text = "Ç"
		'
		'cmdCcedilleMinor
		'
		Me.cmdCcedilleMinor.Enabled = False
		Me.cmdCcedilleMinor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdCcedilleMinor.Location = New System.Drawing.Point(248, 272)
		Me.cmdCcedilleMinor.Name = "cmdCcedilleMinor"
		Me.cmdCcedilleMinor.Size = New System.Drawing.Size(24, 23)
		Me.cmdCcedilleMinor.TabIndex = 15
		Me.cmdCcedilleMinor.Text = "ç"
		'
		'cmdCancel
		'
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdCancel.Location = New System.Drawing.Point(424, 328)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(72, 23)
		Me.cmdCancel.TabIndex = 13
		Me.cmdCancel.Text = "Schließen"
		'
		'lblWordCount
		'
		Me.lblWordCount.Location = New System.Drawing.Point(8, 240)
		Me.lblWordCount.Name = "lblWordCount"
		Me.lblWordCount.Size = New System.Drawing.Size(192, 112)
		Me.lblWordCount.TabIndex = 12
		Me.lblWordCount.Text = "Übungsinfo"
		'
		'lblWordInfo
		'
		Me.lblWordInfo.Location = New System.Drawing.Point(424, 272)
		Me.lblWordInfo.Name = "lblWordInfo"
		Me.lblWordInfo.Size = New System.Drawing.Size(160, 48)
		Me.lblWordInfo.TabIndex = 12
		Me.lblWordInfo.Text = "Wortinfo"
		'
		'cmdOK
		'
		Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.cmdOK.Enabled = False
		Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdOK.Location = New System.Drawing.Point(512, 328)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(72, 23)
		Me.cmdOK.TabIndex = 8
		Me.cmdOK.Text = "OK"
		'
		'lblDescription
		'
		Me.lblDescription.Location = New System.Drawing.Point(8, 72)
		Me.lblDescription.Name = "lblDescription"
		Me.lblDescription.Size = New System.Drawing.Size(400, 32)
		Me.lblDescription.TabIndex = 18
		'
		'lblTestInfo
		'
		Me.lblTestInfo.Location = New System.Drawing.Point(424, 128)
		Me.lblTestInfo.Name = "lblTestInfo"
		Me.lblTestInfo.Size = New System.Drawing.Size(184, 112)
		Me.lblTestInfo.TabIndex = 19
		Me.lblTestInfo.Text = "###"
		'
		'cmdChangeSettings
		'
		Me.cmdChangeSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdChangeSettings.Location = New System.Drawing.Point(424, 240)
		Me.cmdChangeSettings.Name = "cmdChangeSettings"
		Me.cmdChangeSettings.Size = New System.Drawing.Size(72, 23)
		Me.cmdChangeSettings.TabIndex = 20
		Me.cmdChangeSettings.Text = "Ändern"
		'
		'Label1
		'
		Me.Label1.Location = New System.Drawing.Point(8, 112)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(100, 16)
		Me.Label1.TabIndex = 22
		Me.Label1.Text = "Beudeutungen:"
		'
		'cmbGroup
		'
		Me.cmbGroup.Location = New System.Drawing.Point(424, 24)
		Me.cmbGroup.Name = "cmbGroup"
		Me.cmbGroup.Size = New System.Drawing.Size(168, 21)
		Me.cmbGroup.TabIndex = 23
		Me.cmbGroup.Text = "#"
		'
		'cmbUnits
		'
		Me.cmbUnits.Location = New System.Drawing.Point(424, 56)
		Me.cmbUnits.Name = "cmbUnits"
		Me.cmbUnits.Size = New System.Drawing.Size(168, 21)
		Me.cmbUnits.TabIndex = 24
		Me.cmbUnits.Text = "#"
		'
		'cmdStartUnit
		'
		Me.cmdStartUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdStartUnit.Location = New System.Drawing.Point(424, 88)
		Me.cmdStartUnit.Name = "cmdStartUnit"
		Me.cmdStartUnit.Size = New System.Drawing.Size(72, 23)
		Me.cmdStartUnit.TabIndex = 25
		Me.cmdStartUnit.Text = "Beginnen"
		'
		'VocTest
		'
		Me.AcceptButton = Me.cmdOK
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(594, 359)
		Me.Controls.Add(Me.cmdStartUnit)
		Me.Controls.Add(Me.cmbUnits)
		Me.Controls.Add(Me.cmbGroup)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.cmdChangeSettings)
		Me.Controls.Add(Me.lblTestInfo)
		Me.Controls.Add(Me.lblDescription)
		Me.Controls.Add(Me.chkWaitAfterOK)
		Me.Controls.Add(Me.lblWordInfo)
		Me.Controls.Add(Me.cmdCcedilleMinor)
		Me.Controls.Add(Me.cmdCcedilleMajor)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.lblWordCount)
		Me.Controls.Add(Me.lblWrong)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.lblWord)
		Me.Controls.Add(Me.cmdHelp)
		Me.Controls.Add(Me.txtMeaning2)
		Me.Controls.Add(Me.txtMeaning1)
		Me.Controls.Add(Me.txtMeaning3)
		Me.Controls.Add(Me.txtIrregular3)
		Me.Controls.Add(Me.txtIrregular2)
		Me.Controls.Add(Me.txtIrregular1)
		Me.Controls.Add(Me.lblIrregularDescription2)
		Me.Controls.Add(Me.lblIrregularDescription1)
		Me.Controls.Add(Me.lblIrregularDescription3)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.Name = "VocTest"
		Me.Text = "Abfrage"
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private Sub VocTest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim i As Integer
		db.Open(Application.StartupPath() & "\voc.mdb")
		voc = New xlsVocTest(db)
		For i = 0 To voc.Groups.Count - 1
			Me.cmbGroup.Items.Add(voc.Groups(i).Description)
		Next i
		If cmbGroup.Items.Count >= 1 Then cmbGroup.SelectedIndex = 0

		ShowStatistic()
		ShowWordInfo()
		ShowTestInfo()
	End Sub

	Private Sub VocTest_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
		voc.StopTest()
	End Sub

	Private Sub ShowNewWord()
		If voc.WordCountToDo = 0 Then
			ShowStatistic()
			voc.StopTest()
			DisableBeforeEnd()
			MsgBox("Zuende gelernt ;D")
			cmbGroup_SelectedIndexChanged(Nothing, Nothing)
			Exit Sub
		End If

		'Me.chkInfoModeEx.Checked = False
		bUsedHelp1 = False
		bUsedHelp2 = False
		bUsedHelp3 = False

		voc.NextWord()
		' Fenster vorbereiten
		Me.lblWord.Text = voc.TestWord
		If Me.m_bDescription Then Me.lblDescription.Text = wtWord.Description Else Me.lblDescription.Text = ""
		If wtWord.Description = "" Then Me.cmdHelp.Enabled = False Else Me.cmdHelp.Enabled = True
		Me.txtMeaning1.Text = ""
		Me.txtMeaning2.Text = ""
		Me.txtMeaning3.Text = ""
		Me.txtIrregular1.Text = ""
		Me.txtIrregular2.Text = ""
		Me.txtIrregular3.Text = ""
		Me.lblWrong.Text = ""

		If Me.m_bOnlyUsed Then DisableElements()
		ShowWordInfo()
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
		wtWord = voc.GetWord()
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
			sWordInfo += "Wortart: " & voc.TypeText(wtWord.WordType)
		Catch
			sWordInfo += "Wortart: "
		End Try

		sWordInfo &= vbCrLf & "Zuletzt abgefragt: "

		If wtWord Is Nothing Then
			sWordInfo &= "nie"
		Else
			If wtWord.LastTested = "01.01.1900" Then sWordInfo &= "nie" Else sWordInfo &= wtWord.LastTested
		End If

		Me.lblWordInfo.Text = sWordInfo
	End Sub

	Private Sub ShowTestInfo()
		Me.lblTestInfo.Text = "Abfragereihenfolge " & xlsVocTest.NextWordModes(m_iNextWordMode) & vbCrLf
		Me.lblTestInfo.Text &= "Irreguläre " & xlsVocTest.ExtendedModes(m_iTestExtendedMode) & vbCrLf
		Me.lblTestInfo.Text &= xlsVocTest.NextWordModesWrong(m_iNextWordModeWrong) & vbCrLf
		Me.lblTestInfo.Text &= "Reihenfolge "
		Select Case m_iTestMode
			Case xlsVocTestDirection.LanguageDefault
				Me.lblTestInfo.Text &= "Sprachstandard"
			Case xlsVocTestDirection.TestMeaning
				Me.lblTestInfo.Text &= "Wort zu Bedeutung"
			Case xlsVocTestDirection.TestWord
				Me.lblTestInfo.Text &= "Bedeutung zu Wort"
		End Select
		Me.lblTestInfo.Text &= vbCrLf
		If m_bFirstTry Then Me.lblTestInfo.Text &= "Erster-Abfrage-Modus aktiviert" & vbCrLf Else Me.lblTestInfo.Text &= "Erster-Abfrage-Modus nicht aktiviert" & vbCrLf
		If m_bOnlyUsed Then Me.lblTestInfo.Text &= "Nur benötigte Felder aktiviert" & vbCrLf Else Me.lblTestInfo.Text &= "Alle Felder aktiviert" & vbCrLf
		If m_bDescription Then Me.lblTestInfo.Text &= "Beschreibung immer anzeigen" & vbCrLf Else Me.lblTestInfo.Text &= "Beschreibung manuell anzeigen" & vbCrLf
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
		If voc.Answer1 = "" Then iDisableCount += 1
		If voc.Answer2 = "" Then iDisableCount += 1
		If voc.Answer3 = "" Then iDisableCount += 1
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

	Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
		Static bWeiter As Boolean = False

		' Help-Mode setzen
		'If (chkInfoModeEx.Checked) And (Me.m_bOnlyUsed) Then Me.bUsedHelp1 = True
		voc.HelpMode = xlsVocTestHelpModes.NoHelp
		If bUsedHelp1 Then voc.HelpMode = xlsVocTestHelpModes.LightHelp
		If bUsedHelp2 Then voc.HelpMode = xlsVocTestHelpModes.MiddleHelp
		If bUsedHelp3 Then voc.HelpMode = xlsVocTestHelpModes.HeavyHelp
		If Not bWeiter Then
			' Kontrolle, ob richtig
			If voc.TestControl("", Me.txtMeaning1.Text, Me.txtMeaning2.Text, Me.txtMeaning3.Text, Me.txtIrregular1.Text, Me.txtIrregular2.Text, Me.txtIrregular3.Text) = False Then
				Me.cmdOK.Text = "Weiter..."
				Me.lblWrong.Text = "Leider Falsch! Hier die richtigen Antworten:"
				Me.txtMeaning1.Text = Trim(voc.Answer1)
				Me.txtMeaning2.Text = Trim(voc.Answer2)
				Me.txtMeaning3.Text = Trim(voc.Answer3)
				Me.txtIrregular1.Text = Trim(voc.ExtendedAnswer1)
				Me.txtIrregular2.Text = Trim(voc.ExtendedAnswer2)
				Me.txtIrregular3.Text = Trim(voc.ExtendedAnswer3)
				bWeiter = True
			Else
				If Me.chkWaitAfterOK.Checked = True Then
					Me.cmdOK.Text = "Weiter..."
					Me.txtMeaning1.Text = Trim(wtWord.Pre & " " & wtWord.Word & " " & wtWord.Post)
					Me.lblWrong.Text = "Richtig! Zum Fortfahren 'Weiter' drücken."
					bWeiter = True
				Else
					ShowNewWord()
				End If
			End If
		Else
			' Nächstes Wort anzeigen
			cmdOK.Text = "OK"
			ShowNewWord()
			bWeiter = False
		End If
		ShowStatistic()
	End Sub

	Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
		Me.Close()
	End Sub

	Private Sub cmdChangeSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChangeSettings.Click
		Dim frmModes As New TestModeSelect
		frmModes.SetParent = Me
		frmModes.ShowDialog(Me)
		ShowTestInfo()
		StartTest(sender, e)
	End Sub

	Private Sub StartTest(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartUnit.Click
		voc.NextWordMode = Me.m_iNextWordMode
		voc.NextWordModeWrong = Me.m_iNextWordModeWrong
		voc.ExtendedMode = Me.m_iTestExtendedMode

		voc.FirstTryMode = m_bFirstTry
		If m_bDescription Then Me.cmdHelp.Enabled = False

		EnableBeforeStart()
		voc.StopTest()
		voc.Start(voc.WordsInUnit(cmbUnits.SelectedIndex + 1, m_bRequestedOnly))
		ShowNewWord()
		ShowStatistic()
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
	End Sub

	Private Sub cmdCcedilleMajor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCcedilleMajor.Click
		Me.txtMeaning1.Text += "Ç"
	End Sub

	Private Sub cmdCcedilleMinor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCcedilleMinor.Click
		Me.txtMeaning1.Text += "ç"
	End Sub

	Private Sub cmdHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHelp.Click
		Me.lblDescription.Text = wtWord.Description
		If InStr(LCase(wtWord.Description), LCase(wtWord.Word)) <> 0 Then bUsedHelp3 = True Else bUsedHelp2 = True
		If InStr(LCase(wtWord.Description), LCase(wtWord.Meaning1)) <> 0 Then bUsedHelp3 = True Else bUsedHelp2 = True
		If InStr(LCase(wtWord.Description), LCase(wtWord.Meaning2)) <> 0 Then bUsedHelp3 = True Else bUsedHelp2 = True
		If InStr(LCase(wtWord.Description), LCase(wtWord.Meaning3)) <> 0 Then bUsedHelp3 = True Else bUsedHelp2 = True
	End Sub

	Private Sub cmbGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroup.SelectedIndexChanged
		Dim i As Short
		voc.SelectTable(voc.Groups(cmbGroup.SelectedIndex).Table)
		cmbUnits.Items.Clear()
		cmbUnits.Items.Clear()
		For i = 1 To voc.UnitsInGroup.Count
			cmbUnits.Items.Add(voc.UnitsInGroup(i).Name)
		Next i
		If cmbUnits.Items.Count >= 1 Then cmbUnits.SelectedIndex = 0

		ShowWordInfo()
	End Sub

	Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnits.SelectedIndexChanged
		Dim structTest As TestUnits
		Dim cTestUnits As New Collection

		voc.Start(voc.WordsInUnit(cmbUnits.SelectedIndex + 1, m_bRequestedOnly))

		ShowStatistic()
		cTestUnits = Nothing
	End Sub

	WriteOnly Property TestUnits() As Collection
		Set(ByVal Units As Collection)
			m_cTestUnits = Units
		End Set
	End Property

	WriteOnly Property TestType() As String
		Set(ByVal Type As String)
			m_sType = Type
		End Set
	End Property

	WriteOnly Property NextWordMode() As Integer
		Set(ByVal Value As Integer)
			m_iNextWordMode = Value
		End Set
	End Property

	WriteOnly Property NextWordModeWrong() As Integer
		Set(ByVal Value As Integer)
			m_iNextWordModeWrong = Value
		End Set
	End Property

	WriteOnly Property ExtendedMode() As xlsVocTestExtended
		Set(ByVal Value As xlsVocTestExtended)
			m_iTestExtendedMode = Value
		End Set
	End Property

	WriteOnly Property TestMode() As xlsVocTestDirection
		Set(ByVal Value As xlsVocTestDirection)
			m_iTestMode = Value
		End Set
	End Property

	WriteOnly Property FirstTry() As Boolean
		Set(ByVal Value As Boolean)
			m_bFirstTry = Value
		End Set
	End Property

	WriteOnly Property ShowOnlyUsed() As Boolean
		Set(ByVal Value As Boolean)
			m_bOnlyUsed = Value
		End Set
	End Property

	WriteOnly Property Description() As Boolean
		Set(ByVal Value As Boolean)
			m_bDescription = Value
		End Set
	End Property

	WriteOnly Property RequestedOnly() As Boolean
		Set(ByVal Value As Boolean)
			m_bRequestedOnly = Value
		End Set
	End Property
End Class
