Public Class Start
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents cmdInput As System.Windows.Forms.Button
    Friend WithEvents cmdTest As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents cmdStatistic As System.Windows.Forms.Button
    Friend WithEvents cmdManagement As System.Windows.Forms.Button

    ' Für Windows-Formular-Designer erforderlich
    Private components As System.ComponentModel.Container

    'HINWEIS: Die folgende Prozedur ist für den Windows-Formular-Designer erforderlich
    'Sie kann mit dem Windows-Formular-Designer modifiziert werden.
    'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdInput = New System.Windows.Forms.Button
        Me.cmdTest = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.cmdStatistic = New System.Windows.Forms.Button
        Me.cmdManagement = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cmdInput
        '
        Me.cmdInput.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdInput.Location = New System.Drawing.Point(8, 8)
        Me.cmdInput.Name = "cmdInput"
        Me.cmdInput.TabIndex = 0
        Me.cmdInput.Text = "Eingabe"
        '
        'cmdTest
        '
        Me.cmdTest.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdTest.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdTest.Location = New System.Drawing.Point(8, 40)
        Me.cmdTest.Name = "cmdTest"
        Me.cmdTest.TabIndex = 1
        Me.cmdTest.Text = "Abfrage"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(96, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(152, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "© by Jan-Philipp Kappmeier"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Location = New System.Drawing.Point(96, 32)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(104, 16)
        Me.LinkLabel1.TabIndex = 3
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "www.kappmeier.de"
        '
        'cmdStatistic
        '
        Me.cmdStatistic.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdStatistic.Location = New System.Drawing.Point(8, 72)
        Me.cmdStatistic.Name = "cmdStatistic"
        Me.cmdStatistic.TabIndex = 2
        Me.cmdStatistic.Text = "Auswertung"
        '
        'cmdManagement
        '
        Me.cmdManagement.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdManagement.Location = New System.Drawing.Point(8, 104)
        Me.cmdManagement.Name = "cmdManagement"
        Me.cmdManagement.TabIndex = 3
        Me.cmdManagement.Text = "Verwaltung"
        '
        'Start
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(300, 132)
        Me.Controls.Add(Me.cmdManagement)
        Me.Controls.Add(Me.cmdStatistic)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdTest)
        Me.Controls.Add(Me.cmdInput)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "Start"
        Me.Text = "Vokabeltrainer"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInput.Click
        Dim frmInput As New WordInput()
        frmInput.Show()
    End Sub

    Private Sub cmdTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTest.Click
        Dim frmTest As New WordTestIng()
        frmTest.Show()
    End Sub

    Private Sub cmdManagement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdManagement.Click
        Dim frmManagement As New Management()
        frmManagement.Show()
    End Sub

End Class
