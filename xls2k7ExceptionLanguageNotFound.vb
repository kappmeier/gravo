Public Class xlsExceptionLanguageNotFound
  Inherits Exception

  Public Sub New()
    MyBase.New()
  End Sub

  Public Sub New(ByVal message As String)
    MyBase.New(Message)
  End Sub

  'Public Sub New(ByVal message As String, ByVal innerException As System.Exception)
  '  MyBase.New(Message, InnerException)
  'End Sub

  'Public Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.serilization.serializationinfo, ByVal context As System.Runtime.Serialization.StreamingContext)
  '  MyBase.New(info, context)
  'End Sub
End Class