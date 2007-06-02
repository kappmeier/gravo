Public Class RegistryNotReadyException
  Inherits Exception

  Public Sub New()
    MyBase.New()
  End Sub

  Public Sub New(ByVal Message As String)
    MyBase.New(message)
  End Sub
End Class
