'Public MustInherit Class MDIChild
Public Class MDIChild
  Dim frmParent As Main

  Private Sub MDIChild_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    frmParent = Me.MdiParent
  End Sub

  Private Sub MDIChild_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
    If frmParent IsNot Nothing Then frmParent.UpdateWindowSettings(Me.WindowState)
  End Sub

  Private Sub MDIChild_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    If frmParent IsNot Nothing Then frmParent.UpdateWindowSettings(Me.WindowState)
  End Sub
End Class