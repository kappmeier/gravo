Public Class VocStatistic
    Inherits System.Windows.Forms.Form

#Region " Vom Windows Form Designer generierter Code "

	Public Sub New()
		MyBase.New()

		' Dieser Aufruf ist f�r den Windows Form-Designer erforderlich.
		InitializeComponent()

		' Initialisierungen nach dem Aufruf InitializeComponent() hinzuf�gen

	End Sub

	' Die Form �berschreibt den L�schvorgang der Basisklasse, um Komponenten zu bereinigen.
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	' F�r Windows Form-Designer erforderlich
	Private components As System.ComponentModel.IContainer

	'HINWEIS: Die folgende Prozedur ist f�r den Windows Form-Designer erforderlich
	'Sie kann mit dem Windows Form-Designer modifiziert werden.
	'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		'
		'VocStatistic
		'
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
		Me.ClientSize = New System.Drawing.Size(560, 373)
		Me.Name = "VocStatistic"
		Me.Text = "Statistik"

	End Sub

#End Region
End Class