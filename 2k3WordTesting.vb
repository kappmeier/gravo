Public Class WordTesting
    Inherits System.Windows.Forms.Form
	Private voc As CWordTest
	Friend WithEvents lblWrong As System.Windows.Forms.Label
	Const LENGTH_WORD = 50
	Private m_cTestUnits As Collection
	Friend WithEvents lblWordCount As System.Windows.Forms.Label
	Private m_sType As String
	Dim m_iNextWordMode As Integer
	Dim m_iNextWordModeWrong As Integer
	Dim m_iIrregularTestMode As IrregularTest
	Dim m_iTestMode As TestWordModes
	Dim m_bFirstTry As Boolean
	Dim m_bOnlyUsed As Boolean
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
	Friend WithEvents chkInfoModeEx As System.Windows.Forms.CheckBox


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
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
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
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.chkWaitAfterOK = New System.Windows.Forms.CheckBox
		Me.lblWord = New System.Windows.Forms.Label
		Me.lblWrong = New System.Windows.Forms.Label
		Me.GroupBox1 = New System.Windows.Forms.GroupBox
		Me.txtMeaning2 = New System.Windows.Forms.TextBox
		Me.txtMeaning3 = New System.Windows.Forms.TextBox
		Me.txtMeaning1 = New System.Windows.Forms.TextBox
		Me.GroupBox2 = New System.Windows.Forms.GroupBox
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
		Me.chkInfoModeEx = New System.Windows.Forms.CheckBox
		Me.lblDescription = New System.Windows.Forms.Label
		Me.GroupBox1.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.SuspendLayout()
		'
		'chkWaitAfterOK
		'
		Me.chkWaitAfterOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.chkWaitAfterOK.Location = New System.Drawing.Point(296, 304)
		Me.chkWaitAfterOK.Name = "chkWaitAfterOK"
		Me.chkWaitAfterOK.Size = New System.Drawing.Size(152, 16)
		Me.chkWaitAfterOK.TabIndex = 16
		Me.chkWaitAfterOK.Text = "Nach Bestätigung warten"
		'
		'lblWord
		'
		Me.lblWord.BackColor = System.Drawing.SystemColors.Control
		Me.lblWord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblWord.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.lblWord.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold)
		Me.lblWord.Location = New System.Drawing.Point(16, 24)
		Me.lblWord.Name = "lblWord"
		Me.lblWord.Size = New System.Drawing.Size(424, 40)
		Me.lblWord.TabIndex = 0
		Me.lblWord.Text = "#"
		Me.lblWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lblWrong
		'
		Me.lblWrong.Location = New System.Drawing.Point(16, 8)
		Me.lblWrong.Name = "lblWrong"
		Me.lblWrong.Size = New System.Drawing.Size(424, 16)
		Me.lblWrong.TabIndex = 11
		Me.lblWrong.Text = "richtig"
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.txtMeaning2)
		Me.GroupBox1.Controls.Add(Me.txtMeaning3)
		Me.GroupBox1.Controls.Add(Me.txtMeaning1)
		Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.GroupBox1.Location = New System.Drawing.Point(16, 96)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(208, 144)
		Me.GroupBox1.TabIndex = 9
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Bedeutung"
		'
		'txtMeaning2
		'
		Me.txtMeaning2.Location = New System.Drawing.Point(8, 72)
		Me.txtMeaning2.Name = "txtMeaning2"
		Me.txtMeaning2.Size = New System.Drawing.Size(192, 20)
		Me.txtMeaning2.TabIndex = 2
		Me.txtMeaning2.Text = "#"
		'
		'txtMeaning3
		'
		Me.txtMeaning3.Location = New System.Drawing.Point(8, 112)
		Me.txtMeaning3.Name = "txtMeaning3"
		Me.txtMeaning3.Size = New System.Drawing.Size(192, 20)
		Me.txtMeaning3.TabIndex = 3
		Me.txtMeaning3.Text = "#"
		'
		'txtMeaning1
		'
		Me.txtMeaning1.Location = New System.Drawing.Point(8, 32)
		Me.txtMeaning1.Name = "txtMeaning1"
		Me.txtMeaning1.Size = New System.Drawing.Size(192, 20)
		Me.txtMeaning1.TabIndex = 1
		Me.txtMeaning1.Text = "#"
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.lblIrregularDescription3)
		Me.GroupBox2.Controls.Add(Me.lblIrregularDescription2)
		Me.GroupBox2.Controls.Add(Me.lblIrregularDescription1)
		Me.GroupBox2.Controls.Add(Me.txtIrregular3)
		Me.GroupBox2.Controls.Add(Me.txtIrregular2)
		Me.GroupBox2.Controls.Add(Me.txtIrregular1)
		Me.GroupBox2.Location = New System.Drawing.Point(232, 96)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(208, 144)
		Me.GroupBox2.TabIndex = 10
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Zusatz"
		'
		'lblIrregularDescription3
		'
		Me.lblIrregularDescription3.AutoSize = True
		Me.lblIrregularDescription3.Location = New System.Drawing.Point(8, 96)
		Me.lblIrregularDescription3.Name = "lblIrregularDescription3"
		Me.lblIrregularDescription3.Size = New System.Drawing.Size(10, 16)
		Me.lblIrregularDescription3.TabIndex = 21
		Me.lblIrregularDescription3.Text = "#"
		'
		'lblIrregularDescription2
		'
		Me.lblIrregularDescription2.AutoSize = True
		Me.lblIrregularDescription2.Location = New System.Drawing.Point(8, 56)
		Me.lblIrregularDescription2.Name = "lblIrregularDescription2"
		Me.lblIrregularDescription2.Size = New System.Drawing.Size(10, 16)
		Me.lblIrregularDescription2.TabIndex = 20
		Me.lblIrregularDescription2.Text = "#"
		'
		'lblIrregularDescription1
		'
		Me.lblIrregularDescription1.AutoSize = True
		Me.lblIrregularDescription1.Location = New System.Drawing.Point(8, 16)
		Me.lblIrregularDescription1.Name = "lblIrregularDescription1"
		Me.lblIrregularDescription1.Size = New System.Drawing.Size(10, 16)
		Me.lblIrregularDescription1.TabIndex = 19
		Me.lblIrregularDescription1.Text = "#"
		'
		'txtIrregular3
		'
		Me.txtIrregular3.Location = New System.Drawing.Point(8, 112)
		Me.txtIrregular3.Name = "txtIrregular3"
		Me.txtIrregular3.Size = New System.Drawing.Size(192, 20)
		Me.txtIrregular3.TabIndex = 6
		Me.txtIrregular3.Text = "#"
		'
		'txtIrregular2
		'
		Me.txtIrregular2.Location = New System.Drawing.Point(8, 72)
		Me.txtIrregular2.Name = "txtIrregular2"
		Me.txtIrregular2.Size = New System.Drawing.Size(192, 20)
		Me.txtIrregular2.TabIndex = 5
		Me.txtIrregular2.Text = "#"
		'
		'txtIrregular1
		'
		Me.txtIrregular1.Location = New System.Drawing.Point(8, 32)
		Me.txtIrregular1.Name = "txtIrregular1"
		Me.txtIrregular1.Size = New System.Drawing.Size(192, 20)
		Me.txtIrregular1.TabIndex = 4
		Me.txtIrregular1.Text = "#"
		'
		'cmdHelp
		'
		Me.cmdHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdHelp.Location = New System.Drawing.Point(296, 248)
		Me.cmdHelp.Name = "cmdHelp"
		Me.cmdHelp.Size = New System.Drawing.Size(144, 23)
		Me.cmdHelp.TabIndex = 7
		Me.cmdHelp.Text = "Beschreibung anzeigen"
		'
		'cmdCcedilleMajor
		'
		Me.cmdCcedilleMajor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdCcedilleMajor.Location = New System.Drawing.Point(200, 336)
		Me.cmdCcedilleMajor.Name = "cmdCcedilleMajor"
		Me.cmdCcedilleMajor.Size = New System.Drawing.Size(24, 23)
		Me.cmdCcedilleMajor.TabIndex = 14
		Me.cmdCcedilleMajor.Text = "Ç"
		'
		'cmdCcedilleMinor
		'
		Me.cmdCcedilleMinor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdCcedilleMinor.Location = New System.Drawing.Point(240, 336)
		Me.cmdCcedilleMinor.Name = "cmdCcedilleMinor"
		Me.cmdCcedilleMinor.Size = New System.Drawing.Size(24, 23)
		Me.cmdCcedilleMinor.TabIndex = 15
		Me.cmdCcedilleMinor.Text = "ç"
		'
		'cmdCancel
		'
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdCancel.Location = New System.Drawing.Point(280, 336)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.TabIndex = 13
		Me.cmdCancel.Text = "Abbrechen"
		'
		'lblWordCount
		'
		Me.lblWordCount.Location = New System.Drawing.Point(16, 248)
		Me.lblWordCount.Name = "lblWordCount"
		Me.lblWordCount.Size = New System.Drawing.Size(168, 112)
		Me.lblWordCount.TabIndex = 12
		Me.lblWordCount.Text = "Übungsinfo"
		'
		'lblWordInfo
		'
		Me.lblWordInfo.Location = New System.Drawing.Point(184, 248)
		Me.lblWordInfo.Name = "lblWordInfo"
		Me.lblWordInfo.Size = New System.Drawing.Size(104, 80)
		Me.lblWordInfo.TabIndex = 12
		Me.lblWordInfo.Text = "Wortinfo"
		'
		'cmdOK
		'
		Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdOK.Location = New System.Drawing.Point(368, 336)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.TabIndex = 8
		Me.cmdOK.Text = "OK"
		'
		'chkInfoModeEx
		'
		Me.chkInfoModeEx.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.chkInfoModeEx.Location = New System.Drawing.Point(296, 280)
		Me.chkInfoModeEx.Name = "chkInfoModeEx"
		Me.chkInfoModeEx.Size = New System.Drawing.Size(128, 16)
		Me.chkInfoModeEx.TabIndex = 17
		Me.chkInfoModeEx.Text = "Erweitertes Wortinfo"
		'
		'lblDescription
		'
		Me.lblDescription.Location = New System.Drawing.Point(16, 64)
		Me.lblDescription.Name = "lblDescription"
		Me.lblDescription.Size = New System.Drawing.Size(424, 32)
		Me.lblDescription.TabIndex = 18
		Me.lblDescription.Text = "#"
		'
		'WordTesting
		'
		Me.AcceptButton = Me.cmdOK
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(450, 367)
		Me.Controls.Add(Me.lblDescription)
		Me.Controls.Add(Me.chkInfoModeEx)
		Me.Controls.Add(Me.chkWaitAfterOK)
		Me.Controls.Add(Me.lblWordInfo)
		Me.Controls.Add(Me.cmdCcedilleMinor)
		Me.Controls.Add(Me.cmdCcedilleMajor)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.lblWordCount)
		Me.Controls.Add(Me.lblWrong)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.lblWord)
		Me.Controls.Add(Me.cmdHelp)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.Name = "WordTesting"
		Me.Text = "Abfrage - Vokabeltrainer 2k3-Edition"
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox2.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private Sub WordTestIng_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
		Try
			voc.TestClose()
			voc.Close()
		Catch ex As Exception
			' nichts zu tun, da voc nicht geladen ist
		End Try
	End Sub

	Private Sub WordTesting_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
		If bFirstPaint Then bFirstPaint = False : Initialize()
	End Sub

	Private Sub Initialize()
		Dim frmSelect As New UnitSelect
		Application.DoEvents()
		frmSelect.SetParent = Me
		frmSelect.ShowDialog(Me)
		If (m_cTestUnits.Count = Nothing) Or (m_cTestUnits.Count) = 0 Then Me.Hide() : Exit Sub
		Dim frmModes As New TestModeSelect
		frmModes.SetParent = Me
		frmModes.ShowDialog(Me)

		voc = New CWordTest(Application.StartupPath() & "\voc.mdb")
		voc.NextWordMode = Me.m_iNextWordMode
		voc.NextWordModeWrong = Me.m_iNextWordModeWrong
		voc.IrregularTestMode = Me.m_iIrregularTestMode
		voc.FirstTry = m_bFirstTry
		voc.TestInitialize(m_cTestUnits)
		If m_bDescription Then Me.cmdHelp.Enabled = False

		ShowNewWord()
		ShowStatistic()
	End Sub

	Private Sub ShowNewWord()
		If voc.TestWordCount = 0 Then
			ShowStatistic()
			voc.TestClose()
			MsgBox("Zuende gelernt ;D")
			Me.Close()
			Exit Sub
		End If

		bUsedHelp1 = False
		bUsedHelp2 = False
		bUsedHelp3 = False

		voc.TestGetNext()
		' Fenster vorbereiten
		Me.lblWord.Text = voc.TestWord
		If Me.m_bDescription Then Me.lblDescription.Text = voc.Description Else Me.lblDescription.Text = ""
		If voc.Description = "" Then Me.cmdHelp.Enabled = False Else Me.cmdHelp.Enabled = True
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
		sTestInfo = voc.TestWordCountAll & " zu testen. Davon: " & vbCrLf
		sTestInfo = sTestInfo & voc.TestWordCountToDo & " noch ausstehend." & vbCrLf
		sTestInfo = sTestInfo & voc.TestWordCountDone & " beantwortet." & vbCrLf
		sTestInfo = sTestInfo & voc.TestWordCountDoneRight & " sofort richtig." & vbCrLf
		sTestInfo &= voc.TestWordCountDoneWithHelpAll & " mit Hilfe gelöst." & vbCrLf
		sTestInfo &= voc.TestWordCountDoneWithHelp1 & " mit leichter, " & voc.TestWordCountDoneWithHelp2 & " mit mittlerer und" & vbCrLf
		sTestInfo &= voc.TestWordCountDoneWithHelp3 & " mit starker Hilfe." & vbCrLf
		sTestInfo = sTestInfo & voc.TestWordCountDoneFalse & " falsch beantwortet." & vbCrLf
		sTestInfo = sTestInfo & voc.TestWordCountDoneFAlseAllTrys & " falsche Versuche insgesamt."
		Me.lblWordCount.Text = sTestInfo
	End Sub

	Private Sub ShowWordInfo()
		Dim sWordInfo As String
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
		If Me.chkInfoModeEx.Checked Then
			sWordInfo += "Wortart: " & voc.TypeText(voc.WordType)
			Dim sList As Collection
			sList = voc.IrregularDescription
			Me.lblIrregularDescription1.Text = sList(1)
			Me.lblIrregularDescription2.Text = sList(2)
			Me.lblIrregularDescription3.Text = sList(3)
		Else
			sWordInfo += "Wortart: ?"
			Me.lblIrregularDescription1.Text = ""
			Me.lblIrregularDescription2.Text = ""
			Me.lblIrregularDescription3.Text = ""
		End If
		sWordInfo &= vbCrLf & "Zuletzt abgefragt: "
		If voc.LastTested = "01.01.1900" Then sWordInfo &= "nie" Else sWordInfo &= voc.LastTested

		Me.lblWordInfo.Text = sWordInfo
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
		If voc.TestAnswer1 = "" Then iDisableCount += 1
		If voc.TestAnswer2 = "" Then iDisableCount += 1
		If voc.TestAnswer3 = "" Then iDisableCount += 1
		If iDisableCount >= 1 Then Me.txtMeaning3.Enabled = False
		If iDisableCount >= 2 Then Me.txtMeaning2.Enabled = False
		If iDisableCount >= 3 Then Me.txtMeaning1.Enabled = False
		iDisableCount = 0
		If voc.Irregular1 = "" Then iDisableCount += 1
		If voc.Irregular2 = "" Then iDisableCount += 1
		If voc.Irregular3 = "" Then iDisableCount += 1
		If iDisableCount >= 1 Then Me.txtIrregular3.Enabled = False
		If iDisableCount >= 2 Then Me.txtIrregular2.Enabled = False
		If iDisableCount >= 3 Then Me.txtIrregular1.Enabled = False
	End Sub

	Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
		Static bWeiter As Boolean = False

		' Help-Mode setzen
		If (chkInfoModeEx.Checked) And (Me.m_bOnlyUsed) Then Me.bUsedHelp1 = True
		voc.HelpMode = HelpModes.NoHelp
		If bUsedHelp1 Then voc.HelpMode = HelpModes.LightHelp
		If bUsedHelp2 Then voc.HelpMode = HelpModes.MiddleHelp
		If bUsedHelp3 Then voc.HelpMode = HelpModes.HeavyHelp
		If Not bWeiter Then
			' Kontrolle, ob richtig
			If voc.TestControl("", Me.txtMeaning1.Text, Me.txtMeaning2.Text, Me.txtMeaning3.Text, Me.txtIrregular1.Text, Me.txtIrregular2.Text, Me.txtIrregular3.Text) = False Then
				Me.cmdOK.Text = "Weiter..."
				Me.lblWrong.Text = "Leider Falsch! Hier die richtigen Antworten:"
				Me.txtMeaning1.Text = Trim(voc.TestAnswer1)
				Me.txtMeaning2.Text = Trim(voc.TestAnswer2)
				Me.txtMeaning3.Text = Trim(voc.TestAnswer3)
				Me.txtIrregular1.Text = voc.Irregular1
				Me.txtIrregular2.Text = voc.Irregular2
				Me.txtIrregular3.Text = voc.Irregular3
				bWeiter = True
			Else
				If Me.chkWaitAfterOK.Checked = True Then
					Me.cmdOK.Text = "Weiter..."
					Me.txtMeaning1.Text = Trim(voc.Pre & " " & voc.Word & " " & voc.Post)
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

	Private Sub cmdCcedilleMinor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCcedilleMinor.Click
		Me.txtMeaning1.Text += "ç"
	End Sub

	Private Sub chkInfoModeEx_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInfoModeEx.CheckedChanged
		If Me.m_bOnlyUsed Then Me.bUsedHelp1 = True ' Wenn markiert war, jetzt nicht mehr, sonst jetzt
		ShowWordInfo()
	End Sub

	Private Sub cmdCcedilleMajor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCcedilleMajor.Click
		Me.txtMeaning1.Text += "Ç"
	End Sub

	Private Sub cmdHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHelp.Click
		Me.lblDescription.Text = voc.Description
		If InStr(LCase(voc.Description), LCase(voc.Word)) <> 0 Then bUsedHelp3 = True Else bUsedHelp2 = True
		If InStr(LCase(voc.Description), LCase(voc.Meaning1)) <> 0 Then bUsedHelp3 = True Else bUsedHelp2 = True
		If InStr(LCase(voc.Description), LCase(voc.Meaning2)) <> 0 Then bUsedHelp3 = True Else bUsedHelp2 = True
		If InStr(LCase(voc.Description), LCase(voc.Meaning3)) <> 0 Then bUsedHelp3 = True Else bUsedHelp2 = True
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

	WriteOnly Property IrregularTestMode() As IrregularTest
		Set(ByVal Value As IrregularTest)
			m_iIrregularTestMode = Value
		End Set
	End Property

	WriteOnly Property TestMode() As TestWordModes
		Set(ByVal Value As TestWordModes)
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
End Class
