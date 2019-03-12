Public Class xlsExceptionLanguageNotFound
  Inherits Exception

  Public Sub New()
    MyBase.New()
  End Sub

  Public Sub New(ByVal message As String)
    MyBase.New(Message)
  End Sub
End Class