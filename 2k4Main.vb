Public Class Main
	Inherits System.Windows.Forms.Form

#Region " Vom Windows Form Designer generierter Code "

	Public Sub New()
		MyBase.New()

		' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
		InitializeComponent()

		' Initialisierungen nach dem Aufruf InitializeComponent() hinzufügen

	End Sub

	' Die Form überschreibt den Löschvorgang der Basisklasse, um Komponenten zu bereinigen.
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	' Für Windows Form-Designer erforderlich
	Private components As System.ComponentModel.IContainer

	'HINWEIS: Die folgende Prozedur ist für den Windows Form-Designer erforderlich
	'Sie kann mit dem Windows Form-Designer modifiziert werden.
	'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
	Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
	Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
	Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
	Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
	Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
	Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
	Friend WithEvents mnuFile As System.Windows.Forms.MenuItem
	Friend WithEvents mnuFileChangeUser As System.Windows.Forms.MenuItem
	Friend WithEvents mnuFileExit As System.Windows.Forms.MenuItem
	Friend WithEvents mnuHelpInfo As System.Windows.Forms.MenuItem
	Friend WithEvents mnuVocTest As System.Windows.Forms.MenuItem
	Friend WithEvents mnuVocInput As System.Windows.Forms.MenuItem
	Friend WithEvents mnuVocStatistic As System.Windows.Forms.MenuItem
	Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
	Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
	Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
	Friend WithEvents mnuExtraManagement As System.Windows.Forms.MenuItem
	Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
		Me.MainMenu1 = New System.Windows.Forms.MainMenu
		Me.mnuFile = New System.Windows.Forms.MenuItem
		Me.mnuFileChangeUser = New System.Windows.Forms.MenuItem
		Me.MenuItem5 = New System.Windows.Forms.MenuItem
		Me.mnuFileExit = New System.Windows.Forms.MenuItem
		Me.MenuItem6 = New System.Windows.Forms.MenuItem
		Me.mnuVocTest = New System.Windows.Forms.MenuItem
		Me.mnuVocInput = New System.Windows.Forms.MenuItem
		Me.mnuVocStatistic = New System.Windows.Forms.MenuItem
		Me.MenuItem3 = New System.Windows.Forms.MenuItem
		Me.mnuExtraManagement = New System.Windows.Forms.MenuItem
		Me.MenuItem7 = New System.Windows.Forms.MenuItem
		Me.MenuItem9 = New System.Windows.Forms.MenuItem
		Me.MenuItem4 = New System.Windows.Forms.MenuItem
		Me.MenuItem1 = New System.Windows.Forms.MenuItem
		Me.MenuItem2 = New System.Windows.Forms.MenuItem
		Me.mnuHelpInfo = New System.Windows.Forms.MenuItem
		'
		'MainMenu1
		'
		Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFile, Me.MenuItem6, Me.MenuItem3, Me.MenuItem1, Me.MenuItem2})
		'
		'mnuFile
		'
		Me.mnuFile.Index = 0
		Me.mnuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFileChangeUser, Me.MenuItem5, Me.mnuFileExit})
		Me.mnuFile.Text = "&Datei"
		'
		'mnuFileChangeUser
		'
		Me.mnuFileChangeUser.Enabled = False
		Me.mnuFileChangeUser.Index = 0
		Me.mnuFileChangeUser.Text = "Benutzer &wechseln"
		'
		'MenuItem5
		'
		Me.MenuItem5.Index = 1
		Me.MenuItem5.Text = "-"
		'
		'mnuFileExit
		'
		Me.mnuFileExit.Index = 2
		Me.mnuFileExit.Text = "&Beenden"
		'
		'MenuItem6
		'
		Me.MenuItem6.Index = 1
		Me.MenuItem6.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuVocTest, Me.mnuVocInput, Me.mnuVocStatistic})
		Me.MenuItem6.Text = "&Vokabeln"
		'
		'mnuVocTest
		'
		Me.mnuVocTest.Index = 0
		Me.mnuVocTest.Text = "&Abfragen"
		'
		'mnuVocInput
		'
		Me.mnuVocInput.Index = 1
		Me.mnuVocInput.Text = "&Eingeben"
		'
		'mnuVocStatistic
		'
		Me.mnuVocStatistic.Index = 2
		Me.mnuVocStatistic.Text = "&Statistik"
		'
		'MenuItem3
		'
		Me.MenuItem3.Index = 2
		Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuExtraManagement, Me.MenuItem7, Me.MenuItem9, Me.MenuItem4})
		Me.MenuItem3.Text = "E&xtras"
		'
		'mnuExtraManagement
		'
		Me.mnuExtraManagement.Index = 0
		Me.mnuExtraManagement.Text = "Daten-Management ..."
		'
		'MenuItem7
		'
		Me.MenuItem7.Enabled = False
		Me.MenuItem7.Index = 1
		Me.MenuItem7.Text = "Datenbank überprüfen"
		'
		'MenuItem9
		'
		Me.MenuItem9.Index = 2
		Me.MenuItem9.Text = "-"
		'
		'MenuItem4
		'
		Me.MenuItem4.Index = 3
		Me.MenuItem4.Text = "&Optionen ..."
		'
		'MenuItem1
		'
		Me.MenuItem1.Index = 3
		Me.MenuItem1.MdiList = True
		Me.MenuItem1.Text = "&Fenster"
		'
		'MenuItem2
		'
		Me.MenuItem2.Index = 4
		Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuHelpInfo})
		Me.MenuItem2.Text = "&Hilfe"
		'
		'mnuHelpInfo
		'
		Me.mnuHelpInfo.Index = 0
		Me.mnuHelpInfo.Text = "Inf&o ..."
		'
		'Main
		'
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
		Me.ClientSize = New System.Drawing.Size(600, 373)
		Me.IsMdiContainer = True
		Me.Menu = Me.MainMenu1
		Me.Name = "Main"
		Me.Text = "2k4Main"

	End Sub

#End Region

	Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		VocTestShow(sender, e)
		'VocInputShow(sender, e)
		'VocStatisticShow(sender, e)
	End Sub

	Private Sub VocTestShow(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVocTest.Click
		Dim frmTest As New VocTest
		frmTest.MdiParent = Me
		frmTest.Show()
	End Sub

	Private Sub VocInputShow(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVocInput.Click
		Dim frmInput As New VocInput
		frmInput.MdiParent = Me
		frmInput.Show()
	End Sub

	Private Sub VocStatisticShow(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVocStatistic.Click
		Dim frmStat As New VocStatistic
		frmStat.MdiParent = Me
		frmStat.Show()
	End Sub

	Private Sub mnuHelpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHelpInfo.Click
		Dim frmInfo As New Info
		frmInfo.ShowDialog(Me)
	End Sub

	Private Sub mnuFileExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileExit.Click
		MsgBox(Me.Width)
	End Sub

	Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
	End Sub

	Private Sub mnuExtraManagement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExtraManagement.Click
		Dim frmManagement As New Management
		frmManagement.ShowDialog(Me)
	End Sub

	Private Sub mnuInputLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

	End Sub
End Class
