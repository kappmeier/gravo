Public Class GroupSelect
	Inherits System.Windows.Forms.Form
	Private m_Parent As WordInput
	Friend WithEvents cmbGroup As System.Windows.Forms.ComboBox
	Private voc As CWordTest
	Private cLections As Collection
	Private cTestUnits As New Collection

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
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents cmdCancel As System.Windows.Forms.Button

	' Für Windows-Formular-Designer erforderlich
	Private components As System.ComponentModel.Container

	'HINWEIS: Die folgende Prozedur ist für den Windows-Formular-Designer erforderlich
	'Sie kann mit dem Windows-Formular-Designer modifiziert werden.
	'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.cmbGroup = New System.Windows.Forms.ComboBox
		Me.cmdCancel = New System.Windows.Forms.Button
		Me.cmdOK = New System.Windows.Forms.Button
		Me.SuspendLayout()
		'
		'cmbGroup
		'
		Me.cmbGroup.DropDownWidth = 121
		Me.cmbGroup.Location = New System.Drawing.Point(8, 8)
		Me.cmbGroup.Name = "cmbGroup"
		Me.cmbGroup.Size = New System.Drawing.Size(232, 21)
		Me.cmbGroup.TabIndex = 0
		Me.cmbGroup.Text = "#"
		'
		'cmdCancel
		'
		Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdCancel.Location = New System.Drawing.Point(72, 40)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.TabIndex = 9
		Me.cmdCancel.Text = "Abbrechen"
		'
		'cmdOK
		'
		Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.cmdOK.Location = New System.Drawing.Point(160, 40)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.TabIndex = 8
		Me.cmdOK.Text = "OK"
		'
		'GroupSelect
		'
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
		Me.ClientSize = New System.Drawing.Size(248, 70)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.cmbGroup)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.Name = "GroupSelect"
		Me.ShowInTaskbar = False
		Me.Text = "Gruppe auswählen"
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private Sub UnitSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		' Laden der Daten
		Dim i As Integer

		voc = New CWordTest(Application.StartupPath() & "\voc.mdb")
		For i = 0 To voc.Groups.Count - 1
			Me.cmbGroup.Items.Add(voc.Groups(i).Description)
		Next
		Me.cmbGroup.SelectedIndex = 0
		voc.Close()
	End Sub

	Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
		Me.Close()
	End Sub

	Property SetParent() As WordInput
		Get
			Return m_Parent
		End Get
		Set(ByVal Form As WordInput)
			m_Parent = Form
		End Set
	End Property

	Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
		m_Parent.Group = voc.Groups(Me.cmbGroup.SelectedIndex)
		Me.Close()
	End Sub
End Class
