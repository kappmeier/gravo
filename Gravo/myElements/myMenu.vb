Public Class myMenu
  Inherits ToolStripMenuItem

  Dim m_mainForm As Main

  Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
    MyBase.OnClick(e)
    m_mainForm.LocalizationChangeLanguage(Me.Tag)
  End Sub

  Public Property MainForm() As Main
    Get
      Return m_mainForm
    End Get
    Set(ByVal mainForm As Main)
      m_mainForm = mainForm
    End Set
  End Property
End Class
