Public Class WordInput
    Inherits System.Windows.Forms.Form
    Private voc As CWordTest
    Private cLections As Collection
    Private cWords As Collection
    Private m_structGroupInfo As GroupInfo

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
    Friend WithEvents txtMeaning1 As System.Windows.Forms.TextBox
    Friend WithEvents txtMeaning3 As System.Windows.Forms.TextBox
    Friend WithEvents txtMeaning2 As System.Windows.Forms.TextBox
    Friend WithEvents txtIrregular3 As System.Windows.Forms.TextBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
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
    Friend WithEvents txtSearchText As System.Windows.Forms.TextBox
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtPre = New System.Windows.Forms.TextBox()
        Me.txtPost = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtIrregular3 = New System.Windows.Forms.TextBox()
        Me.txtIrregular2 = New System.Windows.Forms.TextBox()
        Me.txtIrregular1 = New System.Windows.Forms.TextBox()
        Me.lstUnits = New System.Windows.Forms.ListBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lstWords = New System.Windows.Forms.ListBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdNew = New System.Windows.Forms.Button()
        Me.chkIrregular = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMeaning1 = New System.Windows.Forms.TextBox()
        Me.txtMeaning3 = New System.Windows.Forms.TextBox()
        Me.lstTypes = New System.Windows.Forms.ListBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkMustKnow = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.nudChapter = New System.Windows.Forms.NumericUpDown()
        Me.cmbUnits = New System.Windows.Forms.ComboBox()
        Me.lblWordInUnit = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtWord = New System.Windows.Forms.TextBox()
        Me.txtMeaning2 = New System.Windows.Forms.TextBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.lblCountVocabulary = New System.Windows.Forms.Label()
        Me.lblCountUnits = New System.Windows.Forms.Label()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.txtSearchText = New System.Windows.Forms.TextBox()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nudChapter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtPre
        '
        Me.txtPre.Location = New System.Drawing.Point(112, 48)
        Me.txtPre.MaxLength = 50
        Me.txtPre.Name = "txtPre"
        Me.txtPre.Size = New System.Drawing.Size(56, 20)
        Me.txtPre.TabIndex = 3
        Me.txtPre.Text = ""
        '
        'txtPost
        '
        Me.txtPost.Location = New System.Drawing.Point(112, 72)
        Me.txtPost.MaxLength = 50
        Me.txtPost.Name = "txtPost"
        Me.txtPost.Size = New System.Drawing.Size(56, 20)
        Me.txtPost.TabIndex = 4
        Me.txtPost.Text = ""
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(16, 72)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(88, 24)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "Nach:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(16, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 24)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Vor:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIrregular3
        '
        Me.txtIrregular3.Enabled = False
        Me.txtIrregular3.Location = New System.Drawing.Point(112, 272)
        Me.txtIrregular3.MaxLength = 50
        Me.txtIrregular3.Name = "txtIrregular3"
        Me.txtIrregular3.Size = New System.Drawing.Size(144, 20)
        Me.txtIrregular3.TabIndex = 11
        Me.txtIrregular3.Text = ""
        '
        'txtIrregular2
        '
        Me.txtIrregular2.Enabled = False
        Me.txtIrregular2.Location = New System.Drawing.Point(112, 248)
        Me.txtIrregular2.MaxLength = 50
        Me.txtIrregular2.Name = "txtIrregular2"
        Me.txtIrregular2.Size = New System.Drawing.Size(144, 20)
        Me.txtIrregular2.TabIndex = 10
        Me.txtIrregular2.Text = ""
        '
        'txtIrregular1
        '
        Me.txtIrregular1.Enabled = False
        Me.txtIrregular1.Location = New System.Drawing.Point(112, 224)
        Me.txtIrregular1.MaxLength = 50
        Me.txtIrregular1.Name = "txtIrregular1"
        Me.txtIrregular1.Size = New System.Drawing.Size(144, 20)
        Me.txtIrregular1.TabIndex = 9
        Me.txtIrregular1.Text = ""
        '
        'lstUnits
        '
        Me.lstUnits.Location = New System.Drawing.Point(8, 24)
        Me.lstUnits.Name = "lstUnits"
        Me.lstUnits.Size = New System.Drawing.Size(112, 290)
        Me.lstUnits.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(16, 136)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 24)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Bedeutung 2:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(16, 248)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 24)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Unregelmäßig 2:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(16, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 24)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Bedeutung 1:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(16, 272)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 24)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Unregelmäßig 3:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(16, 224)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 24)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Unregelmäßig 1:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Lektionen:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(136, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Vokabeln:"
        '
        'lstWords
        '
        Me.lstWords.Location = New System.Drawing.Point(136, 24)
        Me.lstWords.Name = "lstWords"
        Me.lstWords.Size = New System.Drawing.Size(112, 290)
        Me.lstWords.TabIndex = 1
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdCancel.Location = New System.Drawing.Point(592, 328)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.TabIndex = 20
        Me.cmdCancel.Text = "Schließen"
        '
        'cmdNew
        '
        Me.cmdNew.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdNew.Location = New System.Drawing.Point(336, 328)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.TabIndex = 19
        Me.cmdNew.Text = "&Neu"
        '
        'chkIrregular
        '
        Me.chkIrregular.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkIrregular.Location = New System.Drawing.Point(16, 200)
        Me.chkIrregular.Name = "chkIrregular"
        Me.chkIrregular.Size = New System.Drawing.Size(104, 16)
        Me.chkIrregular.TabIndex = 8
        Me.chkIrregular.Text = "Unregelmäßig"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(16, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 24)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Bedeutung 3:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMeaning1
        '
        Me.txtMeaning1.Location = New System.Drawing.Point(112, 112)
        Me.txtMeaning1.MaxLength = 50
        Me.txtMeaning1.Name = "txtMeaning1"
        Me.txtMeaning1.Size = New System.Drawing.Size(144, 20)
        Me.txtMeaning1.TabIndex = 5
        Me.txtMeaning1.Text = ""
        '
        'txtMeaning3
        '
        Me.txtMeaning3.Location = New System.Drawing.Point(112, 160)
        Me.txtMeaning3.MaxLength = 50
        Me.txtMeaning3.Name = "txtMeaning3"
        Me.txtMeaning3.Size = New System.Drawing.Size(144, 20)
        Me.txtMeaning3.TabIndex = 7
        Me.txtMeaning3.Text = ""
        '
        'lstTypes
        '
        Me.lstTypes.Location = New System.Drawing.Point(272, 24)
        Me.lstTypes.Name = "lstTypes"
        Me.lstTypes.Size = New System.Drawing.Size(120, 82)
        Me.lstTypes.TabIndex = 12
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.AddRange(New System.Windows.Forms.Control() {Me.chkMustKnow, Me.Label10, Me.Label8, Me.lblUnit, Me.nudChapter, Me.cmbUnits, Me.lblWordInUnit, Me.lstTypes, Me.chkIrregular, Me.Label13, Me.Label12, Me.Label11, Me.Label9, Me.Label7, Me.Label6, Me.Label5, Me.Label4, Me.Label3, Me.txtIrregular2, Me.txtIrregular1, Me.txtWord, Me.txtPre, Me.txtPost, Me.txtMeaning1, Me.txtMeaning3, Me.txtMeaning2, Me.txtIrregular3})
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox1.Location = New System.Drawing.Point(256, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(408, 312)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Vokabelinfo"
        '
        'chkMustKnow
        '
        Me.chkMustKnow.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkMustKnow.Location = New System.Drawing.Point(272, 128)
        Me.chkMustKnow.Name = "chkMustKnow"
        Me.chkMustKnow.Size = New System.Drawing.Size(104, 16)
        Me.chkMustKnow.TabIndex = 13
        Me.chkMustKnow.Text = "Pflicht"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(272, 160)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(100, 16)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "Unit:"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(272, 224)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 16)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "Chapter:"
        '
        'lblUnit
        '
        Me.lblUnit.Location = New System.Drawing.Point(272, 200)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(112, 16)
        Me.lblUnit.TabIndex = 26
        Me.lblUnit.Text = "#Unit"
        Me.lblUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'nudChapter
        '
        Me.nudChapter.Location = New System.Drawing.Point(272, 240)
        Me.nudChapter.Name = "nudChapter"
        Me.nudChapter.Size = New System.Drawing.Size(56, 20)
        Me.nudChapter.TabIndex = 15
        '
        'cmbUnits
        '
        Me.cmbUnits.DropDownWidth = 121
        Me.cmbUnits.Location = New System.Drawing.Point(272, 176)
        Me.cmbUnits.Name = "cmbUnits"
        Me.cmbUnits.Size = New System.Drawing.Size(121, 21)
        Me.cmbUnits.TabIndex = 14
        '
        'lblWordInUnit
        '
        Me.lblWordInUnit.Location = New System.Drawing.Point(272, 272)
        Me.lblWordInUnit.Name = "lblWordInUnit"
        Me.lblWordInUnit.Size = New System.Drawing.Size(104, 16)
        Me.lblWordInUnit.TabIndex = 24
        Me.lblWordInUnit.Text = "# Word in Unit"
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(16, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 24)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "Vokabel:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtWord
        '
        Me.txtWord.Location = New System.Drawing.Point(112, 24)
        Me.txtWord.MaxLength = 50
        Me.txtWord.Name = "txtWord"
        Me.txtWord.Size = New System.Drawing.Size(144, 20)
        Me.txtWord.TabIndex = 2
        Me.txtWord.Text = ""
        '
        'txtMeaning2
        '
        Me.txtMeaning2.Location = New System.Drawing.Point(112, 136)
        Me.txtMeaning2.MaxLength = 50
        Me.txtMeaning2.Name = "txtMeaning2"
        Me.txtMeaning2.Size = New System.Drawing.Size(144, 20)
        Me.txtMeaning2.TabIndex = 6
        Me.txtMeaning2.Text = ""
        '
        'cmdSave
        '
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdSave.Location = New System.Drawing.Point(256, 328)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.TabIndex = 18
        Me.cmdSave.Text = "Speichern"
        '
        'lblCountVocabulary
        '
        Me.lblCountVocabulary.AutoSize = True
        Me.lblCountVocabulary.Location = New System.Drawing.Point(216, 8)
        Me.lblCountVocabulary.Name = "lblCountVocabulary"
        Me.lblCountVocabulary.Size = New System.Drawing.Size(34, 13)
        Me.lblCountVocabulary.TabIndex = 19
        Me.lblCountVocabulary.Text = "Count"
        Me.lblCountVocabulary.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCountUnits
        '
        Me.lblCountUnits.AutoSize = True
        Me.lblCountUnits.Location = New System.Drawing.Point(88, 8)
        Me.lblCountUnits.Name = "lblCountUnits"
        Me.lblCountUnits.Size = New System.Drawing.Size(34, 13)
        Me.lblCountUnits.TabIndex = 20
        Me.lblCountUnits.Text = "Count"
        Me.lblCountUnits.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdSearch
        '
        Me.cmdSearch.Enabled = False
        Me.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdSearch.Location = New System.Drawing.Point(8, 328)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.TabIndex = 16
        Me.cmdSearch.Text = "Suchen"
        '
        'txtSearchText
        '
        Me.txtSearchText.Enabled = False
        Me.txtSearchText.Location = New System.Drawing.Point(96, 328)
        Me.txtSearchText.Name = "txtSearchText"
        Me.txtSearchText.Size = New System.Drawing.Size(152, 20)
        Me.txtSearchText.TabIndex = 17
        Me.txtSearchText.Text = ""
        '
        'cmdDelete
        '
        Me.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdDelete.Location = New System.Drawing.Point(416, 328)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.TabIndex = 21
        Me.cmdDelete.Text = "&Löschen"
        '
        'WordInput
        '
        Me.AcceptButton = Me.cmdNew
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(668, 354)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.cmdDelete, Me.txtSearchText, Me.cmdSearch, Me.lblCountUnits, Me.lblCountVocabulary, Me.cmdCancel, Me.GroupBox1, Me.Label2, Me.lstWords, Me.Label1, Me.cmdSave, Me.lstUnits, Me.cmdNew})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "WordInput"
        Me.Text = "Vokabeleingabe"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.nudChapter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub WordInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim frmSelect As New GroupSelect()
        frmSelect.SetParent = Me
        frmSelect.ShowDialog(Me)

        If m_structGroupInfo Is Nothing Then Me.Close() : Exit Sub

        voc = New CWordTest(Application.StartupPath() & "\voc.mdb", m_structGroupInfo.Table)
        cLections = voc.GetUnits()

        Dim aTypes As ArrayList
        Dim i As Short, cTemp As Collection
        aTypes = voc.Types
        For i = 0 To aTypes.Count - 1
            lstTypes.Items.Add(aTypes(i))
        Next i
        For i = 1 To cLections.Count
            cTemp = cLections.Item(i)
            lstUnits.Items.Add(cTemp.Item(2))
            cmbUnits.Items.Add(cTemp.Item(2))
        Next i
        If lstUnits.Items.Count >= 1 Then lstUnits.SelectedIndex = 0
        Me.lblCountUnits.Text = lstUnits.Items.Count
    End Sub

    Private Sub lstUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstUnits.SelectedIndexChanged
        ' Die Vokabelliste Aktualisieren
        Dim i As Int32, cTemp As Collection
        cTemp = cLections(lstUnits.SelectedIndex + 1)
        i = cTemp(1)
        cWords = voc.GetWordsInUnit(i)
        lstWords.Items.Clear()
        For i = 1 To cWords.Count
            cTemp = cWords.Item(i)
            lstWords.Items.Add(cTemp.Item(2))
        Next (i)
        If (lstWords.Items.Count) > 0 Then
            lstWords.SelectedIndex = 0
        End If
        Me.lblCountVocabulary.Text = Me.lstWords.Items.Count
    End Sub

    Private Sub lstWords_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstWords.SelectedIndexChanged
        Dim i As Int32, cTemp As Collection
        If lstWords.SelectedIndex = -1 Then Exit Sub
        cTemp = cWords(lstWords.SelectedIndex + 1)
        i = cTemp(3)
        voc.GoToWord(i)
        lblWordInUnit.Text = "Vokabel " & voc.WordInUnit
        cmbUnits.SelectedItem = voc.UnitName
        lblUnit.Text = "Lektion " & voc.UnitNumber
        nudChapter.Value = voc.Chapter
        txtWord.Text = voc.Word
        txtPre.Text = voc.Pre
        txtPost.Text = voc.Post
        txtMeaning1.Text = voc.Meaning1
        txtMeaning2.Text = voc.Meaning2
        txtMeaning3.Text = voc.Meaning3
        txtIrregular1.Text = voc.Irregular1
        txtIrregular2.Text = voc.Irregular2
        txtIrregular3.Text = voc.Irregular3
        chkIrregular.Checked = voc.IrregularForm
        chkMustKnow.Checked = voc.MustKnow
        nudChapter.Value = voc.Chapter
        lstTypes.SelectedIndex = voc.WordType
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Me.lstUnits.Items.Count = -1 Then Exit Sub ' Keine Units definiert
        Dim iIndex = lstWords.SelectedIndex
        If voc.Word <> Trim(txtWord.Text) Then
            lstWords.Items.Remove(voc.Word)
            lstWords.Items.Insert(iIndex, Trim(txtWord.Text))
        End If
        lstWords.SelectedValue = Trim(txtWord.Text)
        voc.Word = txtWord.Text
        voc.Pre = txtPre.Text
        voc.Post = txtPost.Text
        voc.Meaning1 = txtMeaning1.Text
        voc.Meaning2 = txtMeaning2.Text
        voc.Meaning3 = txtMeaning3.Text
        If chkIrregular.Checked = True Then
            voc.IrregularForm = True
            voc.Irregular1 = txtIrregular1.Text
            voc.Irregular2 = txtIrregular2.Text
            voc.Irregular3 = txtIrregular3.Text
        Else
            voc.IrregularForm = False
        End If
        voc.Chapter = nudChapter.Value()
        voc.MustKnow = chkMustKnow.Checked
        voc.WordType = lstTypes.SelectedIndex
        lstWords.SelectedIndex = iIndex
        txtWord.Focus()
    End Sub

    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        If Me.lstUnits.Items.Count = -1 Then Exit Sub ' Keine Units definiert
        If Me.lstWords.Items.Count <= 0 Then
            voc.NewWord(Me.lstUnits.SelectedIndex + 1)
        Else
            cmdSave.PerformClick()
            voc.NewWord()
        End If
        lstUnits_SelectedIndexChanged(cmdNew, e)
        lstWords.SelectedIndex = lstWords.Items.Count - 1
        txtWord.Text = ""
        Me.lblCountVocabulary.Text = lstWords.Items.Count
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

    Property Group() As GroupInfo
        Get

        End Get
        Set(ByVal Selected As GroupInfo)
            m_structGroupInfo = Selected
        End Set
    End Property

    Private Sub WordInput_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Not (voc Is Nothing) Then voc.Close()
    End Sub

    Private Sub cmbUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnits.SelectedIndexChanged
        If cmbUnits.SelectedItem <> voc.UnitName Then
            Dim sOldWord = voc.Word
            voc.UnitName = cmbUnits.SelectedItem
            lstUnits.SelectedItem = cmbUnits.SelectedItem
            lstWords.SelectedItem = sOldWord
        End If
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        voc.Delete()
    End Sub
End Class
