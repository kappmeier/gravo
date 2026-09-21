Public Class InputException
    Inherits Exception

    Public Enum ErrorType
        NoWord
        NoMeaning
        NoLanguage
        IllegalWordType
        IllegalCharacter
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal errorType As ErrorType)
        MyBase.New(GetMessageFromCode(errorType))
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

    Private Shared Function GetMessageFromCode(ByVal code As ErrorType) As String
        Select Case code
            Case ErrorType.NoWord
                Return "No entry for the word."
            Case ErrorType.NoMeaning
                Return "No entry for the meaning."
            Case ErrorType.NoLanguage
                Return "No entry for the language."
            Case ErrorType.IllegalWordType
                Return "Illegal word type."
            Case ErrorType.IllegalCharacter
                Return "Input must not contain a double quote ("")."
        End Select
    End Function

End Class
