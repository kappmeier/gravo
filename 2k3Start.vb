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
    Friend WithEvents cmdStatistic As System.Windows.Forms.Button
    Friend WithEvents cmdManagement As System.Windows.Forms.Button

    ' Für Windows-Formular-Designer erforderlich
    Private components As System.ComponentModel.Container

    'HINWEIS: Die folgende Prozedur ist für den Windows-Formular-Designer erforderlich
    'Sie kann mit dem Windows-Formular-Designer modifiziert werden.
    'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
	Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents chkGermanText As System.Windows.Forms.CheckBox
	Friend WithEvents lblDisclaimer As System.Windows.Forms.Label
	Friend WithEvents Label5 As System.Windows.Forms.Label
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdInput = New System.Windows.Forms.Button
        Me.cmdTest = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdStatistic = New System.Windows.Forms.Button
        Me.cmdManagement = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblDisclaimer = New System.Windows.Forms.Label
        Me.chkGermanText = New System.Windows.Forms.CheckBox
        Me.Label5 = New System.Windows.Forms.Label
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
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(144, 86)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "© 1995 - 2004"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cmdStatistic
        '
        Me.cmdStatistic.Enabled = False
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(96, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(159, 29)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Vokabeltrainer"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(192, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 23)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "2k3-Edition"
        '
        'lblDisclaimer
        '
        Me.lblDisclaimer.Location = New System.Drawing.Point(8, 144)
        Me.lblDisclaimer.Name = "lblDisclaimer"
        Me.lblDisclaimer.Size = New System.Drawing.Size(280, 112)
        Me.lblDisclaimer.TabIndex = 7
        Me.lblDisclaimer.Text = "! ! ! WARNING ! ! ! This version of Vokabeltrainer is still beta. We recommend to" & _
        " save your database whenever you've added some vocabulary. It is distributed ""as" & _
        " is"", we are not responisble for anything that happens using this software."
        '
        'chkGermanText
        '
        Me.chkGermanText.Location = New System.Drawing.Point(8, 256)
        Me.chkGermanText.Name = "chkGermanText"
        Me.chkGermanText.Size = New System.Drawing.Size(104, 16)
        Me.chkGermanText.TabIndex = 8
        Me.chkGermanText.Text = "Deutsch"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(144, 102)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(121, 15)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "by Jan-Philipp Kappmeier"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Start
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(278, 275)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.chkGermanText)
        Me.Controls.Add(Me.lblDisclaimer)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdManagement)
        Me.Controls.Add(Me.cmdStatistic)
        Me.Controls.Add(Me.cmdTest)
        Me.Controls.Add(Me.cmdInput)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "Start"
        Me.Text = "Vokabeltrainer 2k3 Edition"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInput.Click
        Dim frmInput As New WordInput()
        frmInput.Show()
    End Sub

	Private Sub cmdManagement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdManagement.Click
		Dim frmManagement As New Management
		frmManagement = New Management
		frmManagement.ShowDialog(Me)
	End Sub

	Private Sub cmdTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTest.Click
		Dim frmTest As New WordTesting
		frmTest.Show()
	End Sub

	Private Sub Start_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblDisclaimer.Text = "! ! ! WARNING ! ! ! " & vbCrLf & "This version of Vokabeltrainer is still beta. We recommend you to save your database whenever you've added some vocabulary. This software is distributed ""as is"", we are neither responisble for anything that happens using this software nor responsible for the correct function of this piece of software."
    End Sub

    Private Sub chkGermanText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGermanText.CheckedChanged
        If chkGermanText.Checked = True Then
            Me.lblDisclaimer.Text = "! ! ! WARNUNG ! ! ! " & vbCrLf & "Diese Version von Vokabeltrainer ist noch im beta-Status. Wir emfehlen nach jeder Vokabeleingabe die Datenbank zu sichern. Diese Software wird vertrieben ""wie sie ist"", wir sind nicht verantwortlich für das, was durch Benutzung dieser Software geschieht, noch kann die Funktionsfähigkeit dieser Software garantiert werden."
        Else
            Me.lblDisclaimer.Text = "! ! ! WARNING ! ! ! " & vbCrLf & "This version of Vokabeltrainer is still beta. We recommend you to save your database whenever you've added some vocabulary. This software is distributed ""as is"", we are neither responisble for anything that happens using this software nor responsible for the correct function of this piece of software."
        End If
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class
