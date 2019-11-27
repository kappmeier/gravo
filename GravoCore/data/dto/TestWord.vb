Imports Gravo

Public Class TestWord
    Implements IWordReference

    Private ReadOnly _wordEntry As WordEntry
    Private ReadOnly _marked As Boolean
    Private ReadOnly _example As String

    Sub New(wordEntry As WordEntry, marked As Boolean, example As String)
        Me._wordEntry = wordEntry
        Me._marked = marked
        Me._example = example
    End Sub

    Public ReadOnly Property Marked As Boolean
        Get
            Return _marked
        End Get
    End Property

    Public ReadOnly Property Example As String
        Get
            Return _example
        End Get
    End Property

    Public ReadOnly Property Index As Integer
        Get
            Return _wordEntry.Index
        End Get
    End Property

    Public ReadOnly Property WordIndex As Integer Implements IWordReference.WordIndex
        Get
            Return _wordEntry.Index
        End Get
    End Property

    Public ReadOnly Property Word As String
        Get
            Return _wordEntry.Word
        End Get
    End Property

    Public ReadOnly Property Pre As String
        Get
            Return _wordEntry.Pre
        End Get
    End Property

    Public ReadOnly Property Post As String
        Get
            Return _wordEntry.Post
        End Get
    End Property

    Public ReadOnly Property Meaning As String
        Get
            Return _wordEntry.Meaning
        End Get
    End Property

    Public ReadOnly Property AdditionalTargetLangInfo As String
        Get
            Return _wordEntry.AdditionalTargetLangInfo
        End Get
    End Property

    Public ReadOnly Property Irregular As Boolean
        Get
            Return _wordEntry.Irregular
        End Get
    End Property

    Public ReadOnly Property WordEntry As WordEntry
        Get
            Return _wordEntry
        End Get
    End Property

    Friend ReadOnly Property WordType As WordType
        Get
            Return _wordEntry.WordType
        End Get
    End Property

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim word = TryCast(obj, TestWord)
        Return word IsNot Nothing AndAlso
               EqualityComparer(Of WordEntry).Default.Equals(_wordEntry, word._wordEntry) AndAlso
               _marked = word._marked AndAlso
               _example = word._example
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return (_wordEntry, _marked, _example).GetHashCode()
    End Function
End Class
