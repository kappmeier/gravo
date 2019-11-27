Imports Gravo
''' <summary>
''' Holds all information about available word types.
''' 
''' Holds word types that are available by the language system as WordType as
''' well, as word types that are only defined within the database.
''' </summary>
Public Class WordTypes
    Private ReadOnly wordTypes As IDictionary(Of String, Integer)
    Private ReadOnly foundWordTypes As IDictionary(Of String, WordType)

    ''' <summary>
    ''' The ownership of the dictionaries is transfered to this instance.
    ''' </summary>
    ''' <param name="wordTypes"></param>
    ''' <param name="foundWordTypes"></param>
    Public Sub New(ByRef wordTypes As IDictionary(Of String, Integer), ByRef foundWordTypes As IDictionary(Of String, WordType))
        Me.wordTypes = wordTypes
        Me.foundWordTypes = foundWordTypes
    End Sub

    Public Function GetSupportedWordTypes() As ICollection(Of String)
        Return wordTypes.Keys
    End Function

    Public Function GetWordType(ByVal number As Integer) As String
        Return wordTypes.FirstOrDefault(Function(x) x.Value = number).Key
    End Function

    Public Function GetWordType(name As String) As WordType
        Return wordTypes(name)
    End Function
End Class
