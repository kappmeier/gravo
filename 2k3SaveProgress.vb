Public Class SaveProgress
	Inherits System.Windows.Forms.Form

	Private m_bOverwrite As Boolean = True
	Private m_bAddOnly As Boolean = True
	Private m_sDBPath As String = ""
	Private m_bIsShown As Boolean = False
	Private m_voc As CWordTest

	'Dim voc As New CWordTest(Application.StartupPath() & "\voc.mdb")

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
	Friend WithEvents Progress As System.Windows.Forms.ProgressBar
	Friend WithEvents lblCurrentElement As System.Windows.Forms.Label
	Friend WithEvents Label1 As System.Windows.Forms.Label
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.Progress = New System.Windows.Forms.ProgressBar
		Me.lblCurrentElement = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.SuspendLayout()
		'
		'Progress
		'
		Me.Progress.Location = New System.Drawing.Point(8, 24)
		Me.Progress.Maximum = 1580
		Me.Progress.Name = "Progress"
		Me.Progress.Size = New System.Drawing.Size(232, 23)
		Me.Progress.TabIndex = 0
		'
		'lblCurrentElement
		'
		Me.lblCurrentElement.Location = New System.Drawing.Point(8, 56)
		Me.lblCurrentElement.Name = "lblCurrentElement"
		Me.lblCurrentElement.Size = New System.Drawing.Size(232, 16)
		Me.lblCurrentElement.TabIndex = 1
		Me.lblCurrentElement.Text = "#"
		'
		'Label1
		'
		Me.Label1.Location = New System.Drawing.Point(8, 8)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(224, 16)
		Me.Label1.TabIndex = 2
		Me.Label1.Text = "#"
		'
		'SaveProgress
		'
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
		Me.ClientSize = New System.Drawing.Size(248, 82)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.lblCurrentElement)
		Me.Controls.Add(Me.Progress)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "SaveProgress"
		Me.Text = "Sichern..."
		Me.TopMost = True
		Me.ResumeLayout(False)

	End Sub

#End Region

	WriteOnly Property Overwrite() As Boolean
		Set(ByVal Value As Boolean)
			m_bOverwrite = Value
		End Set
	End Property

	WriteOnly Property AddOnly() As Boolean
		Set(ByVal Value As Boolean)
			m_bAddOnly = Value
		End Set
	End Property

	WriteOnly Property DBPath() As String
		Set(ByVal Value As String)
			m_sDBPath = Value
		End Set
	End Property

	ReadOnly Property IsShown() As Boolean
		Get
			Return m_bIsShown
		End Get
	End Property

	Public Sub SetVoc(ByRef voc As CWordTest)
		m_voc = voc
	End Sub

	Public Function Save()
		Select Case m_voc.SaveTable(m_sDBPath, m_bAddOnly, m_bOverwrite, Me.Progress, Me.lblCurrentElement)
			Case SaveErrors.NoError
				Return True
			Case SaveErrors.NotConnected
				MsgBox("Sie müssen sich mit einer Datenbank verbinden," & vbCrLf & "bevor Sie Daten sichern können!", vbCritical)
				Return False
			Case SaveErrors.TableExists
				Dim iYesNo As MsgBoxResult = MsgBox("Soll die Tabelle überschrieben werden?", MsgBoxStyle.YesNo)
				If iYesNo = MsgBoxResult.Yes Then
					If m_voc.SaveTable(m_sDBPath, m_bAddOnly, True, Me.Progress, Me.lblCurrentElement) Then MsgBox("Sichern fehlgeschlagen!", MsgBoxStyle.Critical) : Return False Else Return True
				Else
					Return False
				End If
			Case SaveErrors.UnknownError
				MsgBox("Sichern fehlgeschlagen!", MsgBoxStyle.Critical)
				Return False
		End Select
	End Function

	Private Sub SaveProgress_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
		m_bIsShown = True
	End Sub
End Class
