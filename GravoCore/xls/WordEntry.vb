Imports Gravo

Public Class WordEntry
    Implements IWordReference

    Private _index As Integer = Nothing
    Private ReadOnly _word As String
    Private ReadOnly _pre As String
    Private ReadOnly _post As String
    Private ReadOnly _wordType As WordType
    Private ReadOnly _meaning As String
    Private ReadOnly _additionalTargetLangInfo As String
    Private ReadOnly _irregular As Boolean

    Protected Friend Sub New(index As Integer, word As String, pre As String, post As String, wordType As WordType, meaning As String, additionalTargetLangInfo As String, irregular As Boolean)
        _index = index
        _word = word
        _pre = pre
        _post = post
        _wordType = wordType
        _meaning = meaning
        _additionalTargetLangInfo = additionalTargetLangInfo
        _irregular = irregular
    End Sub

    Public Sub New(word As String, pre As String, post As String, wordType As WordType, meaning As String, additionalTargetLangInfo As String, irregular As Boolean)
        _index = Index
        _word = word
        _pre = pre
        _post = post
        _wordType = wordType
        _meaning = meaning
        _additionalTargetLangInfo = additionalTargetLangInfo
        _irregular = irregular
    End Sub

    Public Property Index As Integer
        Get
            Return _index
        End Get
        Friend Set(ByVal index As Integer)
            ' check for index=nothing?
            _index = index
        End Set
    End Property

    Public ReadOnly Property Word As String
        Get
            Return _word
        End Get
    End Property

    Public ReadOnly Property Pre As String
        Get
            Return _pre
        End Get
    End Property

    Public ReadOnly Property Post As String
        Get
            Return _post
        End Get
    End Property

    Public ReadOnly Property Meaning As String
        Get
            Return _meaning
        End Get
    End Property

    Public ReadOnly Property AdditionalTargetLangInfo As String
        Get
            Return _additionalTargetLangInfo
        End Get
    End Property

    Public ReadOnly Property Irregular As Boolean
        Get
            Return _irregular
        End Get
    End Property

    Public ReadOnly Property WordType As WordType
        Get
            Return _wordType
        End Get
    End Property

    Public ReadOnly Property WordIndex As Integer Implements IWordReference.WordIndex
        Get
            Return _index
        End Get
    End Property

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim entry = TryCast(obj, WordEntry)
        '_index = entry._index AndAlso
        Return entry IsNot Nothing AndAlso
               _word = entry._word AndAlso
               _pre = entry._pre AndAlso
               _post = entry._post AndAlso
               _wordType = entry._wordType AndAlso
               _meaning = entry._meaning AndAlso
               _additionalTargetLangInfo = entry._additionalTargetLangInfo AndAlso
               _irregular = entry._irregular
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return (_index, _word, _pre, _post, _wordType, _meaning, _additionalTargetLangInfo, _irregular).GetHashCode()
    End Function
End Class
