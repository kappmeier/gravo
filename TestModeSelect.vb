Public Class TestModeSelect
    Inherits System.Windows.Forms.Form
    Dim m_Parent As WordTesting
    'Dim voc As New CWordTest(Application.StartupPath() & "\voc.mdb")

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
    Friend WithEvents cmbIrregularTestModes As System.Windows.Forms.ComboBox
    Friend WithEvents optDefault As System.Windows.Forms.RadioButton
    Friend WithEvents chkFirstTry As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmbNextWordModes = New System.Windows.Forms.ComboBox()
        Me.cmbNextWordModesWrong = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optDefault = New System.Windows.Forms.RadioButton()
        Me.optMeaningToWord = New System.Windows.Forms.RadioButton()
        Me.optWordToMeaning = New System.Windows.Forms.RadioButton()
        Me.chkFirstTry = New System.Windows.Forms.CheckBox()
        Me.cmbIrregularTestModes = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdOK.Location = New System.Drawing.Point(280, 168)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.TabIndex = 0
        Me.cmdOK.Text = "OK"
        '
        'cmbNextWordModes
        '
        Me.cmbNextWordModes.Location = New System.Drawing.Point(16, 24)
        Me.cmbNextWordModes.Name = "cmbNextWordModes"
        Me.cmbNextWordModes.Size = New System.Drawing.Size(168, 21)
        Me.cmbNextWordModes.TabIndex = 1
        Me.cmbNextWordModes.Text = "cmbNextWordModes"
        '
        'cmbNextWordModesWrong
        '
        Me.cmbNextWordModesWrong.Location = New System.Drawing.Point(16, 72)
        Me.cmbNextWordModesWrong.Name = "cmbNextWordModesWrong"
        Me.cmbNextWordModesWrong.Size = New System.Drawing.Size(344, 21)
        Me.cmbNextWordModesWrong.TabIndex = 2
        Me.cmbNextWordModesWrong.Text = "cmbNextWordModesWrong"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.AddRange(New System.Windows.Forms.Control() {Me.optDefault, Me.optMeaningToWord, Me.optWordToMeaning})
        Me.GroupBox1.Location = New System.Drawing.Point(16, 104)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 88)
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
        Me.chkFirstTry.Location = New System.Drawing.Point(224, 112)
        Me.chkFirstTry.Name = "chkFirstTry"
        Me.chkFirstTry.Size = New System.Drawing.Size(136, 16)
        Me.chkFirstTry.TabIndex = 7
        Me.chkFirstTry.Text = "Erste-Abfrage-Modus aktivieren"
        '
        'cmbIrregularTestModes
        '
        Me.cmbIrregularTestModes.Location = New System.Drawing.Point(192, 24)
        Me.cmbIrregularTestModes.Name = "cmbIrregularTestModes"
        Me.cmbIrregularTestModes.Size = New System.Drawing.Size(168, 21)
        Me.cmbIrregularTestModes.TabIndex = 9
        Me.cmbIrregularTestModes.Text = "cmbIrregularTestModes"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(139, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Nächstes Wort auswählen:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(192, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Irreguläre Formen:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(178, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Behandlung von falschen Wörtern:"
        '
        'TestModeSelect
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(370, 200)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label3, Me.Label2, Me.Label1, Me.cmbIrregularTestModes, Me.chkFirstTry, Me.GroupBox1, Me.cmbNextWordModesWrong, Me.cmbNextWordModes, Me.cmdOK})
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
        asList = CWordTest.NextWordModes
        For i = 0 To asList.Count - 1
            Me.cmbNextWordModes.Items.Add(asList(i))
        Next i
        asList = CWordTest.NextWordModesWrong
        For i = 0 To asList.Count - 1
            Me.cmbNextWordModesWrong.Items.Add(asList(i))
        Next i
        asList = CWordTest.IrregularTestModes
        For i = 0 To asList.Count - 1
            Me.cmbIrregularTestModes.Items.Add(asList(i))
        Next i
        Me.cmbNextWordModes.SelectedIndex = 1
        Me.cmbNextWordModesWrong.SelectedIndex = 4
        Me.cmbIrregularTestModes.SelectedIndex = 0
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        m_Parent.FirstTry = Me.chkFirstTry.Checked
        Me.Close()
    End Sub

    Property SetParent() As WordTesting
        Get
            Return m_Parent
        End Get
        Set(ByVal Form As WordTesting)
            m_Parent = Form
        End Set
    End Property

    Private Sub cmbNextWordModes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNextWordModes.SelectedIndexChanged
        m_Parent.NextWordMode = cmbNextWordModes.SelectedIndex
    End Sub

    Private Sub cmbNextWordModesWrong_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNextWordModesWrong.SelectedIndexChanged
        m_Parent.NextWordModeWrong = Me.cmbNextWordModesWrong.SelectedIndex
    End Sub

    Private Sub cmbIrregularTestModes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbIrregularTestModes.SelectedIndexChanged
        m_Parent.IrregularTestMode = Me.cmbIrregularTestModes.SelectedIndex
    End Sub

    Private Sub optMeaningToWord_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optMeaningToWord.CheckedChanged
        If optMeaningToWord.Checked = True Then m_Parent.TestMode = TestWordModes.TestWord
    End Sub

    Private Sub optWordToMeaning_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optWordToMeaning.CheckedChanged
        If optMeaningToWord.Checked = True Then m_Parent.TestMode = TestWordModes.TestMeaning
    End Sub

    Private Sub optDefault_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDefault.CheckedChanged
        If optMeaningToWord.Checked = True Then m_Parent.TestMode = TestWordModes.LanguageDefault
    End Sub
End Class
