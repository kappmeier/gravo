Public Class TestModeSelect
    Inherits System.Windows.Forms.Form
	Dim m_Parent As VocTest
    'Dim voc As New xlsVocInput(Application.StartupPath() & "\voc.mdb")

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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optMeaningToWord As System.Windows.Forms.RadioButton
    Friend WithEvents optWordToMeaning As System.Windows.Forms.RadioButton
    Friend WithEvents cmbNextWordModes As System.Windows.Forms.ComboBox
    Friend WithEvents cmbNextWordModesWrong As System.Windows.Forms.ComboBox
    Friend WithEvents cmbxlsVocTestExtendedModes As System.Windows.Forms.ComboBox
    Friend WithEvents optDefault As System.Windows.Forms.RadioButton
    Friend WithEvents chkFirstTry As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents chkOnlyUsed As System.Windows.Forms.CheckBox
	Friend WithEvents chkDescription As System.Windows.Forms.CheckBox
	Friend WithEvents chkRequestedOnly As System.Windows.Forms.CheckBox
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.cmdOK = New System.Windows.Forms.Button
		Me.cmbNextWordModes = New System.Windows.Forms.ComboBox
		Me.cmbNextWordModesWrong = New System.Windows.Forms.ComboBox
		Me.GroupBox1 = New System.Windows.Forms.GroupBox
		Me.optDefault = New System.Windows.Forms.RadioButton
		Me.optMeaningToWord = New System.Windows.Forms.RadioButton
		Me.optWordToMeaning = New System.Windows.Forms.RadioButton
		Me.chkFirstTry = New System.Windows.Forms.CheckBox
		Me.cmbxlsVocTestExtendedModes = New System.Windows.Forms.ComboBox
		Me.Label1 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.chkOnlyUsed = New System.Windows.Forms.CheckBox
		Me.chkDescription = New System.Windows.Forms.CheckBox
		Me.chkRequestedOnly = New System.Windows.Forms.CheckBox
		Me.GroupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'cmdOK
		'
		Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdOK.Location = New System.Drawing.Point(272, 200)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.TabIndex = 0
		Me.cmdOK.Text = "OK"
		'
		'cmbNextWordModes
		'
		Me.cmbNextWordModes.Location = New System.Drawing.Point(8, 24)
		Me.cmbNextWordModes.Name = "cmbNextWordModes"
		Me.cmbNextWordModes.Size = New System.Drawing.Size(168, 21)
		Me.cmbNextWordModes.TabIndex = 1
		Me.cmbNextWordModes.Text = "cmbNextWordModes"
		'
		'cmbNextWordModesWrong
		'
		Me.cmbNextWordModesWrong.Location = New System.Drawing.Point(8, 72)
		Me.cmbNextWordModesWrong.Name = "cmbNextWordModesWrong"
		Me.cmbNextWordModesWrong.Size = New System.Drawing.Size(344, 21)
		Me.cmbNextWordModesWrong.TabIndex = 2
		Me.cmbNextWordModesWrong.Text = "cmbNextWordModesWrong"
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.optDefault)
		Me.GroupBox1.Controls.Add(Me.optMeaningToWord)
		Me.GroupBox1.Controls.Add(Me.optWordToMeaning)
		Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.GroupBox1.Location = New System.Drawing.Point(8, 104)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(152, 88)
		Me.GroupBox1.TabIndex = 6
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Abfragerichtung"
		'
		'optDefault
		'
		Me.optDefault.Checked = True
		Me.optDefault.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.optDefault.Location = New System.Drawing.Point(8, 16)
		Me.optDefault.Name = "optDefault"
		Me.optDefault.Size = New System.Drawing.Size(120, 16)
		Me.optDefault.TabIndex = 9
		Me.optDefault.TabStop = True
		Me.optDefault.Text = "Sprachstandard"
		'
		'optMeaningToWord
		'
		Me.optMeaningToWord.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.optMeaningToWord.Location = New System.Drawing.Point(8, 40)
		Me.optMeaningToWord.Name = "optMeaningToWord"
		Me.optMeaningToWord.Size = New System.Drawing.Size(120, 16)
		Me.optMeaningToWord.TabIndex = 8
		Me.optMeaningToWord.Text = "Bedeutung zu Wort"
		'
		'optWordToMeaning
		'
		Me.optWordToMeaning.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.optWordToMeaning.Location = New System.Drawing.Point(8, 64)
		Me.optWordToMeaning.Name = "optWordToMeaning"
		Me.optWordToMeaning.Size = New System.Drawing.Size(120, 16)
		Me.optWordToMeaning.TabIndex = 7
		Me.optWordToMeaning.Text = "Wort zu Bedeutung"
		'
		'chkFirstTry
		'
		Me.chkFirstTry.Checked = True
		Me.chkFirstTry.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkFirstTry.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.chkFirstTry.Location = New System.Drawing.Point(168, 104)
		Me.chkFirstTry.Name = "chkFirstTry"
		Me.chkFirstTry.Size = New System.Drawing.Size(184, 16)
		Me.chkFirstTry.TabIndex = 7
		Me.chkFirstTry.Text = "Erste-Abfrage-Modus aktivieren"
		'
		'cmbxlsVocTestExtendedModes
		'
		Me.cmbxlsVocTestExtendedModes.Location = New System.Drawing.Point(184, 24)
		Me.cmbxlsVocTestExtendedModes.Name = "cmbxlsVocTestExtendedModes"
		Me.cmbxlsVocTestExtendedModes.Size = New System.Drawing.Size(168, 21)
		Me.cmbxlsVocTestExtendedModes.TabIndex = 9
		Me.cmbxlsVocTestExtendedModes.Text = "cmbxlsVocTestExtendedModes"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(8, 8)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(139, 16)
		Me.Label1.TabIndex = 10
		Me.Label1.Text = "Nächstes Wort auswählen:"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(184, 8)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(98, 16)
		Me.Label2.TabIndex = 11
		Me.Label2.Text = "Irreguläre Formen:"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(8, 56)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(178, 16)
		Me.Label3.TabIndex = 12
		Me.Label3.Text = "Behandlung von falschen Wörtern:"
		'
		'chkOnlyUsed
		'
		Me.chkOnlyUsed.Checked = True
		Me.chkOnlyUsed.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkOnlyUsed.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.chkOnlyUsed.Location = New System.Drawing.Point(168, 128)
		Me.chkOnlyUsed.Name = "chkOnlyUsed"
		Me.chkOnlyUsed.Size = New System.Drawing.Size(184, 16)
		Me.chkOnlyUsed.TabIndex = 13
		Me.chkOnlyUsed.Text = "Nur benötigte Felder aktivieren"
		'
		'chkDescription
		'
		Me.chkDescription.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.chkDescription.Location = New System.Drawing.Point(168, 152)
		Me.chkDescription.Name = "chkDescription"
		Me.chkDescription.Size = New System.Drawing.Size(184, 16)
		Me.chkDescription.TabIndex = 14
		Me.chkDescription.Text = "Beschreibung immer anzeigen"
		'
		'chkRequestedOnly
		'
		Me.chkRequestedOnly.Checked = True
		Me.chkRequestedOnly.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkRequestedOnly.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.chkRequestedOnly.Location = New System.Drawing.Point(168, 176)
		Me.chkRequestedOnly.Name = "chkRequestedOnly"
		Me.chkRequestedOnly.Size = New System.Drawing.Size(184, 16)
		Me.chkRequestedOnly.TabIndex = 15
		Me.chkRequestedOnly.Text = "Nur Pflicht-Vokabeln"
		'
		'TestModeSelect
		'
		Me.AcceptButton = Me.cmdOK
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
		Me.ClientSize = New System.Drawing.Size(354, 231)
		Me.Controls.Add(Me.chkRequestedOnly)
		Me.Controls.Add(Me.chkDescription)
		Me.Controls.Add(Me.chkOnlyUsed)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.cmbxlsVocTestExtendedModes)
		Me.Controls.Add(Me.chkFirstTry)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.cmbNextWordModesWrong)
		Me.Controls.Add(Me.cmbNextWordModes)
		Me.Controls.Add(Me.cmdOK)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.Name = "TestModeSelect"
		Me.ShowInTaskbar = False
		Me.Text = "Test-Modi auswählen"
		Me.GroupBox1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

#End Region

    Private Sub TestModeSelect_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim asList As ArrayList
		asList = xlsVocTest.NextWordModes


        For i = 0 To asList.Count - 1
            Me.cmbNextWordModes.Items.Add(asList(i))
        Next i
		asList = xlsVocTest.NextWordModesWrong
        For i = 0 To asList.Count - 1
            Me.cmbNextWordModesWrong.Items.Add(asList(i))
        Next i
		asList = xlsVocTest.ExtendedModes
        For i = 0 To asList.Count - 1
            Me.cmbxlsVocTestExtendedModes.Items.Add(asList(i))
        Next i
        Me.cmbNextWordModes.SelectedIndex = 1
        Me.cmbNextWordModesWrong.SelectedIndex = 4
		Me.cmbxlsVocTestExtendedModes.SelectedIndex = 0

		' opt-Wert in Abfrage übertragen, falls nicht geändert wird
		m_Parent.TestMode = xlsVocTestDirection.LanguageDefault
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
		m_Parent.FirstTry = Me.chkFirstTry.Checked
		m_Parent.ShowOnlyUsed = Me.chkOnlyUsed.Checked
		m_Parent.Description = Me.chkDescription.Checked
		m_Parent.RequestedOnly = Me.chkRequestedOnly.Checked
        Me.Close()
    End Sub

	Property SetParent() As VocTest
		Get
			Return m_Parent
		End Get
		Set(ByVal Form As VocTest)
			m_Parent = Form
		End Set
	End Property

	Private Sub cmbNextWordModes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNextWordModes.SelectedIndexChanged
		m_Parent.NextWordMode = cmbNextWordModes.SelectedIndex
	End Sub

	Private Sub cmbNextWordModesWrong_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNextWordModesWrong.SelectedIndexChanged
		m_Parent.NextWordModeWrong = Me.cmbNextWordModesWrong.SelectedIndex
	End Sub

	Private Sub cmbExtendedModes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbxlsVocTestExtendedModes.SelectedIndexChanged
		m_Parent.ExtendedMode = Me.cmbxlsVocTestExtendedModes.SelectedIndex
	End Sub

	Private Sub optMeaningToWord_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optMeaningToWord.CheckedChanged
		If optMeaningToWord.Checked = True Then m_Parent.TestMode = xlsVocTestDirection.TestWord
	End Sub

	Private Sub optWordToMeaning_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optWordToMeaning.CheckedChanged
		If optMeaningToWord.Checked = True Then m_Parent.TestMode = xlsVocTestDirection.TestMeaning
	End Sub

	Private Sub optDefault_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDefault.CheckedChanged
		If optMeaningToWord.Checked = True Then m_Parent.TestMode = xlsVocTestDirection.LanguageDefault
	End Sub

	Private Sub chkDescription_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDescription.CheckedChanged
		If chkDescription.Checked Then MsgBox("Mit angezeigter Beschreibung wird jede Vokabel als mit Hilfe gelöst gewertet!")
	End Sub

	Private Sub chkRequestedOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRequestedOnly.CheckedChanged

	End Sub
End Class
