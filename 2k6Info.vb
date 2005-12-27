Public Class Info
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel

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
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Info))
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblDisclaimer = New System.Windows.Forms.Label
        Me.chkGermanText = New System.Windows.Forms.CheckBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmdClose = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'LinkLabel2
        '
        Me.LinkLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.LinkLabel2.Location = New System.Drawing.Point(184, 40)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(137, 19)
        Me.LinkLabel2.TabIndex = 3
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "http://www.kappmeier.de"
        Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(192, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "© 1995 - 2005"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(169, 29)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Vokabeltrainer"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(94, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 22)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "2k6-Edition"
        '
        'lblDisclaimer
        '
        Me.lblDisclaimer.Location = New System.Drawing.Point(8, 104)
        Me.lblDisclaimer.Name = "lblDisclaimer"
        Me.lblDisclaimer.Size = New System.Drawing.Size(320, 96)
        Me.lblDisclaimer.TabIndex = 7
        Me.lblDisclaimer.Text = resources.GetString("lblDisclaimer.Text")
        '
        'chkGermanText
        '
        Me.chkGermanText.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkGermanText.Location = New System.Drawing.Point(8, 200)
        Me.chkGermanText.Name = "chkGermanText"
        Me.chkGermanText.Size = New System.Drawing.Size(104, 24)
        Me.chkGermanText.TabIndex = 8
        Me.chkGermanText.Text = "Deutsch"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(192, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(111, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Jan-Philipp Kappmeier"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cmdClose
        '
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdClose.Location = New System.Drawing.Point(248, 200)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 11
        Me.cmdClose.Text = "Schließen"
        '
        'Info
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(330, 231)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.chkGermanText)
        Me.Controls.Add(Me.lblDisclaimer)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LinkLabel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Info"
        Me.ShowInTaskbar = False
        Me.Text = "Vokabeltrainer 2k6-Edition"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub Start_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = AppTitleLong
        Me.lblDisclaimer.Text = "! ! ! WARNING ! ! ! " & vbCrLf & "This version of Vokabeltrainer is still beta. We recommend to save your database whenever you've added some vocabulary. This software is distributed ""as is"", we are neither responisble for anything that happens using this software nor responsible for the correct functioning of this piece of software."
    End Sub

    Private Sub chkGermanText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGermanText.CheckedChanged
        If chkGermanText.Checked = True Then
            Me.lblDisclaimer.Text = "! ! ! WARNUNG ! ! ! " & vbCrLf & "Diese Version von Vokabeltrainer ist noch im beta-Status. Wir emfehlen nach jeder Vokabeleingabe die Datenbank zu sichern. Diese Software wird vertrieben ""wie sie ist"", wir sind nicht verantwortlich für das, was durch Benutzung dieser Software geschieht, noch wird die Funktionsfähigkeit dieser Software garantiert."
        Else
            Me.lblDisclaimer.Text = "! ! ! WARNING ! ! ! " & vbCrLf & "This version of Vokabeltrainer is still beta. We recommend to save your database whenever you've added some vocabulary. This software is distributed ""as is"", we are neither responisble for anything that happens using this software nor responsible for the correct functioning of this piece of software."
        End If
    End Sub
End Class