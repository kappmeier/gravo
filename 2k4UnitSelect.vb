Public Class UnitSelect
    Inherits System.Windows.Forms.Form
	Private m_Parent As VocTest
    Friend WithEvents cmbGroup As System.Windows.Forms.ComboBox
	Private voc As xlsVocInput
	Private db As New CDBOperation
    Private cLections As Collection
    Private cTestUnits As New Collection()

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
    Friend WithEvents lstToTest As System.Windows.Forms.ListBox
    Friend WithEvents cmdTake As System.Windows.Forms.Button
    Friend WithEvents cmdTakeAll As System.Windows.Forms.Button
    Friend WithEvents cmbChapter As System.Windows.Forms.ComboBox
    Friend WithEvents cmdDeleteAll As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button

    ' Für Windows-Formular-Designer erforderlich
    Private components As System.ComponentModel.Container

    'HINWEIS: Die folgende Prozedur ist für den Windows-Formular-Designer erforderlich
    'Sie kann mit dem Windows-Formular-Designer modifiziert werden.
    'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.cmdOK = New System.Windows.Forms.Button
		Me.cmbChapter = New System.Windows.Forms.ComboBox
		Me.lstUnits = New System.Windows.Forms.ListBox
		Me.lstToTest = New System.Windows.Forms.ListBox
		Me.cmbGroup = New System.Windows.Forms.ComboBox
		Me.cmdCancel = New System.Windows.Forms.Button
		Me.cmdTakeAll = New System.Windows.Forms.Button
		Me.cmdDelete = New System.Windows.Forms.Button
		Me.cmdDeleteAll = New System.Windows.Forms.Button
		Me.cmdTake = New System.Windows.Forms.Button
		Me.SuspendLayout()
		'
		'cmdOK
		'
		Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdOK.Location = New System.Drawing.Point(440, 176)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(72, 23)
		Me.cmdOK.TabIndex = 8
		Me.cmdOK.Text = "OK"
		'
		'cmbChapter
		'
		Me.cmbChapter.DropDownWidth = 121
		Me.cmbChapter.Enabled = False
		Me.cmbChapter.Location = New System.Drawing.Point(8, 176)
		Me.cmbChapter.Name = "cmbChapter"
		Me.cmbChapter.Size = New System.Drawing.Size(208, 21)
		Me.cmbChapter.TabIndex = 5
		Me.cmbChapter.Text = "Alle"
		'
		'lstUnits
		'
		Me.lstUnits.Location = New System.Drawing.Point(8, 40)
		Me.lstUnits.Name = "lstUnits"
		Me.lstUnits.Size = New System.Drawing.Size(208, 121)
		Me.lstUnits.TabIndex = 1
		'
		'lstToTest
		'
		Me.lstToTest.Location = New System.Drawing.Point(304, 40)
		Me.lstToTest.Name = "lstToTest"
		Me.lstToTest.Size = New System.Drawing.Size(208, 121)
		Me.lstToTest.TabIndex = 2
		'
		'cmbGroup
		'
		Me.cmbGroup.DropDownWidth = 121
		Me.cmbGroup.Location = New System.Drawing.Point(8, 8)
		Me.cmbGroup.Name = "cmbGroup"
		Me.cmbGroup.Size = New System.Drawing.Size(208, 21)
		Me.cmbGroup.TabIndex = 0
		Me.cmbGroup.Text = "#"
		'
		'cmdCancel
		'
		Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdCancel.Location = New System.Drawing.Point(352, 176)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(72, 23)
		Me.cmdCancel.TabIndex = 9
		Me.cmdCancel.Text = "Abbrechen"
		'
		'cmdTakeAll
		'
		Me.cmdTakeAll.Enabled = False
		Me.cmdTakeAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdTakeAll.Location = New System.Drawing.Point(224, 72)
		Me.cmdTakeAll.Name = "cmdTakeAll"
		Me.cmdTakeAll.Size = New System.Drawing.Size(72, 23)
		Me.cmdTakeAll.TabIndex = 4
		Me.cmdTakeAll.Text = ">>"
		'
		'cmdDelete
		'
		Me.cmdDelete.Enabled = False
		Me.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdDelete.Location = New System.Drawing.Point(224, 136)
		Me.cmdDelete.Name = "cmdDelete"
		Me.cmdDelete.Size = New System.Drawing.Size(72, 23)
		Me.cmdDelete.TabIndex = 7
		Me.cmdDelete.Text = "<"
		'
		'cmdDeleteAll
		'
		Me.cmdDeleteAll.Enabled = False
		Me.cmdDeleteAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdDeleteAll.Location = New System.Drawing.Point(224, 104)
		Me.cmdDeleteAll.Name = "cmdDeleteAll"
		Me.cmdDeleteAll.Size = New System.Drawing.Size(72, 23)
		Me.cmdDeleteAll.TabIndex = 6
		Me.cmdDeleteAll.Text = "<<"
		'
		'cmdTake
		'
		Me.cmdTake.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdTake.Location = New System.Drawing.Point(224, 40)
		Me.cmdTake.Name = "cmdTake"
		Me.cmdTake.Size = New System.Drawing.Size(72, 23)
		Me.cmdTake.TabIndex = 3
		Me.cmdTake.Text = ">"
		'
		'UnitSelect
		'
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
		Me.ClientSize = New System.Drawing.Size(522, 207)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.cmdDelete)
		Me.Controls.Add(Me.cmdDeleteAll)
		Me.Controls.Add(Me.cmbChapter)
		Me.Controls.Add(Me.cmdTakeAll)
		Me.Controls.Add(Me.cmdTake)
		Me.Controls.Add(Me.lstToTest)
		Me.Controls.Add(Me.lstUnits)
		Me.Controls.Add(Me.cmbGroup)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.Name = "UnitSelect"
		Me.ShowInTaskbar = False
		Me.Text = "Lektionen zum Abfragen auswählen"
		Me.ResumeLayout(False)

	End Sub

#End Region

    Private Sub UnitSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Laden der Daten
        Dim i As Integer

		db.Open(Application.StartupPath() & "\voc.mdb")
		voc = New xlsVocInput(db)
        For i = 0 To voc.Groups.Count - 1
            Me.cmbGroup.Items.Add(voc.Groups(i).Description)
        Next
        Me.cmbGroup.SelectedIndex = 0
        voc.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Dim i As Byte
        For i = 1 To cTestUnits.Count
            cTestUnits.Remove(1)
        Next
        m_Parent.TestUnits = cTestUnits
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

	Private Sub cmbGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroup.SelectedIndexChanged
		voc = New xlsVocInput(db, voc.Groups(cmbGroup.SelectedIndex).Table)

		Dim i As Short, cTemp As Collection

		Me.lstUnits.Items.Clear()
		For i = 1 To cLections.Count
			cTemp = cLections.Item(i)
			lstUnits.Items.Add(cTemp.Item(2))
		Next i
		If lstUnits.Items.Count > 0 Then lstUnits.SelectedIndex = 0
		voc.Close()
	End Sub

	Private Sub cmdTake_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTake.Click
		Dim structTest As TestUnits

		Me.lstToTest.Items.Add(Me.cmbGroup.SelectedItem & " - " & Me.lstUnits.SelectedItem)
		structTest.Unit = lstUnits.SelectedIndex + 1	   '                  lstUnits.SelectedItem
		structTest.Table = voc.Groups(cmbGroup.SelectedIndex).Table
		cTestUnits.Add(structTest)
		m_Parent.TestType = voc.Groups(cmbGroup.SelectedIndex).Type
	End Sub

	Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
		m_Parent.TestUnits = cTestUnits
		Me.Close()
	End Sub
End Class
