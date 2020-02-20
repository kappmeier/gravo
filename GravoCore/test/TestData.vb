Imports Gravo

Public Enum TestResult
    NoError
    OtherMeaning
    Wrong
    Misspelled
End Enum

Class TestEntry
    Friend ReadOnly word As WordEntry
    Friend ReadOnly firstTest As Boolean

    Public Sub New(word As WordEntry, firstTest As Boolean)
        If word Is Nothing Then
            Throw New ArgumentNullException(NameOf(word))
        End If
        Me.word = word
        Me.firstTest = firstTest
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim entry = TryCast(obj, TestEntry)
        Return entry IsNot Nothing AndAlso
            entry.word.Equals(word) AndAlso
            firstTest = entry.firstTest
    End Function
End Class

''' <summary>
''' Container for test data.
''' </summary>
Public Class TestData
    Private ReadOnly cards As ICardsDao
    Private ReadOnly words As ICollection(Of WordEntry)
    Private queryLanguage As QueryLanguage

    Public Sub New(cards As ICardsDao, words As ICollection(Of WordEntry), queryLanguage As QueryLanguage)
        Me.cards = cards
        Me.words = words
        Me.queryLanguage = queryLanguage
    End Sub

    Public Function IsEmpty() As Boolean
        Return Words.Count = 0
    End Function

    Friend Function Current() As TestEntry
        Advance()
        If IsEmpty() Then
            Current = Nothing
        Else
            Current = New TestEntry(words.FirstOrDefault, True)
        End If
    End Function

    ''' <summary>
    ''' Advances pointer to next word. It may happen that after a call no word is left.
    ''' </summary>
    Private Sub Advance()
        Dim found As Boolean = False
        While Not IsEmpty() And Not found
            Dim candidate As WordEntry = words.First
            'cards.Skip(testWordEntries.Item(iTestCurrentWord).WordEntry, QueryLanguage)
            '' verringern hat geklappt, es muß also ein neues Wort gesucht werden
            'DeleteWord() ' und das alte kann gelöscht werden, es wird ja nicht abgefragt
            If cards.Skip(candidate, queryLanguage) Then
                words.Remove(candidate)
            Else
                found = True
            End If
        End While
    End Sub

    Public Function Count() As Integer
        Return Words.Count
    End Function

    Public Sub Update(result As TestResult)
        If result = TestResult.NoError Then
            words.Remove(Current.word)
        End If
    End Sub

End Class
