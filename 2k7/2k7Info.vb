Public Class Info
    Private Sub Start_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Left = Me.Owner.Left + Me.Owner.Width / 2 - Me.Width / 2
        Me.Top = Me.Owner.Top + Me.Owner.Height / 2 - Me.Height / 2
        If Me.Top < 0 Then Me.Top = 0
        If Me.Left < 0 Then Me.Left = 0
        Me.Text = AppTitleLong & " info"
        Me.lblDisclaimer.Text = "! ! ! WARNING ! ! ! " & vbCrLf & "This version of Gravo is still beta. We recommend to save your database whenever you've added some vocabulary. This software is distributed ""as is"", we are neither responisble for anything that happens using this software nor responsible for the correct functioning of this piece of software."
        Me.lblCompany.Text = Application.CompanyName
        Me.lblProductName.Text = Application.ProductName
    End Sub

    Private Sub chkGermanText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGermanText.CheckedChanged
        If chkGermanText.Checked = True Then
            Me.lblDisclaimer.Text = "! ! ! WARNUNG ! ! ! " & vbCrLf & "Diese Version von Gravo ist noch im beta-Status. Wir emfehlen nach jeder Vokabeleingabe die Datenbank zu sichern. Diese Software wird vertrieben ""wie sie ist"", wir sind nicht verantwortlich für das, was durch Benutzung dieser Software geschieht, noch wird die Funktionsfähigkeit dieser Software garantiert."
        Else
            Me.lblDisclaimer.Text = "! ! ! WARNING ! ! ! " & vbCrLf & "This version of Gravo is still beta. We recommend to save your database whenever you've added some vocabulary. This software is distributed ""as is"", we are neither responisble for anything that happens using this software nor responsible for the correct functioning of this piece of software."
        End If
    End Sub
End Class