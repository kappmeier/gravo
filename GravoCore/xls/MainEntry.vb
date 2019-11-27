Imports Gravo

Public Class MainEntry

    Private _index As Integer = Nothing
    Private ReadOnly _word As String
    Private ReadOnly _language As String
    Private ReadOnly _mainLanguage As String

    Protected Friend Sub New(index As Integer, word As String, language As String, mainLanguage As String)
        _index = index
        _word = word
        _language = language
        _mainLanguage = mainLanguage
    End Sub

    Public Sub New(word As String, language As String, mainLanguage As String)
        _word = word
        _language = language
        _mainLanguage = mainLanguage
    End Sub

    Friend ReadOnly Property Index As String
        Get
            Return _index
        End Get
    End Property

    Public ReadOnly Property Word As String
        Get
            Return _word
        End Get
    End Property

    Public ReadOnly Property Language As String
        Get
            Return _language
        End Get
    End Property

    Public ReadOnly Property MainLanguage As String
        Get
            Return _mainLanguage
        End Get
    End Property

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim entry = TryCast(obj, MainEntry)
        Return entry IsNot Nothing AndAlso
               _index = entry._index AndAlso
               _word = entry._word AndAlso
               _language = entry._language AndAlso
               _mainLanguage = entry._mainLanguage
    End Function
End Class
