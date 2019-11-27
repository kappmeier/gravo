Imports System.Collections.ObjectModel
Imports Gravo

Public Class GroupDto
    Private ReadOnly group As GroupEntry
    Private ReadOnly _entries As ICollection(Of TestWord)

    Public Sub New(group As GroupEntry, entries As ICollection(Of TestWord))
        Me.group = group
        Me._entries = entries
    End Sub

    Public ReadOnly Property GroupTable() As String
        Get
            Return group.Table
        End Get
    End Property

    Public ReadOnly Property GroupSubName() As String
        Get
            Return group.SubGroup
        End Get
    End Property

    Public ReadOnly Property Entries As ReadOnlyCollection(Of TestWord)
        Get
            Return New ReadOnlyCollection(Of TestWord)(_entries)
        End Get
    End Property

    Public ReadOnly Property Indices As ReadOnlyCollection(Of Integer)
        Get
            Dim result As IEnumerable(Of Integer) = Entries.Select(Of Integer)(Function(t) t.WordIndex)
            Return result.ToList.AsReadOnly
        End Get
    End Property

    Public ReadOnly Property WordCount() As Integer
        Get
            Return _entries.Count
        End Get
    End Property

    Public Function GetWords() As IEnumerable(Of String)
        Return New AnonymousEnumerable(_entries)
    End Function

    Public Function GetWord(ByVal wordIndex As Integer) As TestWord
        GetWord = Entries.Where(Function(entry) entry.WordIndex = wordIndex).SingleOrDefault()
        If GetWord Is Nothing Then
            Throw New EntryNotFoundException()
        End If
    End Function

    ''' <summary>
    ''' Returns only the entries from the group whose word literally equals a given test.
    ''' </summary>
    ''' <param name="word">the words that should be returned</param>
    ''' <returns>an enumeration of the matching entries in the group</returns>
    Public Function FilterWords(word As String) As IEnumerable(Of TestWord)
        Return _entries.Where(Function(entry) entry.Word = word)
    End Function

    Shared Function IsMarked(ByRef group As GroupDto, wordIndex As Integer)
        Dim testWord As TestWord = group.GetWord(wordIndex)
        IsMarked = testWord.Marked
    End Function

    Private Class AnonymousEnumerable
        Implements IEnumerable(Of String)

        Private ReadOnly innerEnumerable As IEnumerable(Of TestWord)

        Public Sub New(innerEnumerable As IEnumerable(Of TestWord))
            Me.innerEnumerable = innerEnumerable
        End Sub

        Public Function GetEnumerator() As IEnumerator(Of String) Implements IEnumerable(Of String).GetEnumerator
            Return New AnonymousEnumerator(innerEnumerable.GetEnumerator)
        End Function

        Private Function GetObjectEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return Me.GetEnumerator
        End Function
    End Class

    Private Class AnonymousEnumerator
        Implements IEnumerator(Of String)

        Private innerEnumerator As IEnumerator(Of TestWord)

        Public Sub New(innerEnumerator As IEnumerator(Of TestWord))
            Me.innerEnumerator = innerEnumerator
        End Sub

        Public ReadOnly Property Current As String Implements IEnumerator(Of String).Current
            Get
                Return innerEnumerator.Current.Word
            End Get
        End Property

        Private ReadOnly Property CurrentObject As Object Implements IEnumerator.Current
            Get
                Return Current
            End Get
        End Property

        Public Sub Reset() Implements IEnumerator.Reset
            innerEnumerator.Reset()
        End Sub

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            Return innerEnumerator.MoveNext()
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            innerEnumerator.Dispose()
        End Sub
    End Class

End Class
