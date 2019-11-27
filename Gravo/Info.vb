Imports Microsoft.VisualBasic.ApplicationServices

Public Class Info
    Private Sub Start_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Left = Me.Owner.Left + Me.Owner.Width / 2 - Me.Width / 2
        Me.Top = Me.Owner.Top + Me.Owner.Height / 2 - Me.Height / 2
        If Me.Top < 0 Then Me.Top = 0
        If Me.Left < 0 Then Me.Left = 0
        Me.Text = AppTitleLong & " info"
        lblDisclaimer.Text = "Working with this version of " & Application.ProductName & " should be possible without problems, however, some smaller errors could occur. We recommend you to save your database whenever you've added some vocabulary and non change database manually. This software is distributed ""as is"", we are neither responisble for anything that happens using this software nor responsible for the correct function of this piece of software."
        lblCopyrightOld.Text = "based on Vokabeltrainer, © 1995-2007"
        lblCopyright.Text = "© 1995-2019"
        lblCopyrightName.Text = "Jan-Philipp Kappmeier"
        lblProductName.Text = Application.ProductName
        lblVersion.Text = "Version: " & Application.ProductVersion
        Dim db As IDataBaseOperation = New SQLiteDataBaseOperation()
        db.Open(DBPathLoc)
        Dim man As New xlsManagement(db)
        lblDBVersion.Text = "DB-Version: " & man.DatabaseVersionNeeded
    End Sub

    Public Overrides Sub LocalizationChanged()

    End Sub

    Private Sub chkGermanText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGermanText.CheckedChanged
        If chkGermanText.Checked = True Then
            lblDisclaimer.Text = "Das Arbeiten mit dieser Version von " & Application.ProductName & " sollte problemlos möglich sein, dennoch können noch kleinere Fehler auftreten. Wir emfehlen nach jeder Vokabeleingabe die Datenbank zu sichern und keine Änderungen an der Datenbank manuell durchzufüren. Diese Software wird vertrieben ""wie sie ist"", wir sind nicht verantwortlich für das, was durch Benutzung dieser Software geschieht, noch kann die Funktionsfähigkeit dieser Software garantiert werden."
            lblCopyrightOld.Text = "basiert auf Vokabeltrainer, © 1995-2007"
        Else
            lblDisclaimer.Text = "Working with this version of " & Application.ProductName & " should be possible without problems, however, some smaller errors could occur. We recommend to save your database whenever you've added some vocabulary and to not change the database manually. This software is distributed ""as is"", we are neither responisble for anything that happens using this software nor responsible for the correct function of this piece of software."
            lblCopyrightOld.Text = "based on Vokabeltrainer, © 1995-2007"
        End If
    End Sub
End Class