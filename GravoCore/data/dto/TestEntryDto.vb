'Imports Gravo
''' <summary>
''' Stores raw data for one test entry. A test entry is defined by the index of its word, the marked property
''' indicating if it is important and an optional example.
''' </summary>
'Public Class TestEntryDto
'    Implements IWordReference
'    Private ReadOnly _wordIndex As Integer
'    Private ReadOnly _marked As Boolean
'    Private ReadOnly _example As String

'    Public Sub New(wordIndex As Integer, marked As Boolean, example As String)
'        _wordIndex = wordIndex
'        _marked = marked
'        _example = example
'    End Sub

'    Public ReadOnly Property WordIndex As Integer Implements IWordReference.WordIndex
'        Get
'            Return _wordIndex
'        End Get
'    End Property

'    Public ReadOnly Property Marked As Boolean
'        Get
'            Return _marked
'        End Get
'    End Property

'    Public ReadOnly Property Example As String
'        Get
'            Return _example
'        End Get
'    End Property

'    Public Overrides Function Equals(obj As Object) As Boolean
'        Dim dto = TryCast(obj, TestEntryDto)
'        Return dto IsNot Nothing AndAlso
'               _wordIndex = dto._wordIndex AndAlso
'               _marked = dto._marked AndAlso
'               _example = dto._example
'    End Function

'    Public Overrides Function GetHashCode() As Integer
'        Return (_wordIndex, _marked, _example).GetHashCode()
'    End Function
'End Class
