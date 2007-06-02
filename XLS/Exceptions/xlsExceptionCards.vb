Public Class xlsExceptionCards
  Inherits Exception

  ' Wird geworfen, wenn bei einer Operation ein Parameter den falschen Wert hat.

  ' TODO Error-Code in Enumeration umwandeln
  ' Code 1 steht für Counter kann nicht verringert werden, da schon 1
  Dim m_iErrorCode As Integer = 0   ' Error-Code

  Public Sub New()
    MyBase.New()
  End Sub

  Public Sub New(ByVal errorcode As Integer)
    MyBase.New(GetMessageFromCode(errorcode))
    m_iErrorCode = errorcode
  End Sub

  Private Shared Function GetMessageFromCode(ByVal code As Integer) As String
    Select Case code
      Case 1
        Return "Counter ist 1."
      Case 2
        Return "Not ready. Load first."
      Case Else
        Return "Unbekannter Fehler"
    End Select
  End Function

  Public Sub New(ByVal message As String, ByVal errorcode As Integer)
    MyBase.New(message)
    m_iErrorCode = errorcode
  End Sub

  Public ReadOnly Property ErrorCode() As Integer
    Get
      Return m_iErrorCode
    End Get
  End Property

  'Public Sub New(ByVal message As String, ByVal innerException As System.Exception)
  '  MyBase.New(Message, InnerException)
  'End Sub

  'Public Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.serilization.serializationinfo, ByVal context As System.Runtime.Serialization.StreamingContext)
  '  MyBase.New(info, context)
  'End Sub
End Class
