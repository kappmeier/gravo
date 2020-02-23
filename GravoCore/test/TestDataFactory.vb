''' <summary>
''' Methods to create an initial set of test data.
''' </summary>
Public Class TestDataFactory
    ''' <summary>
    ''' Creates a test data set containing _all_ entries.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function Create(dictionary As IDictionaryDao, cards As ICardsDao, testPhrases As Boolean, queryLanguage As QueryLanguage) As TestData
        Throw New NotImplementedException
    End Function

    ''' <summary>
    ''' Creates a test data set containing all entries for a given language.
    ''' </summary>
    ''' <param name="language"></param>
    ''' <returns></returns>
    Public Shared Function Create(dictionary As IDictionaryDao, cards As ICardsDao, language As String, testPhrases As Boolean, queryLanguage As QueryLanguage) As TestData
        Dim words As ICollection(Of WordEntry) = dictionary.GetWords(language, "german")
        If Not testPhrases Then
            words = words.Where(Function(t) t.WordType <> "Phrase")
        End If
        Return New TestData(cards, words, queryLanguage)
    End Function

    ''' <summary>
    ''' Creates a test data set containing all entries for a given group
    ''' </summary>
    ''' <param name="group"></param>
    ''' <returns></returns>
    Public Shared Function Create(groupDao As IGroupDao, cards As ICardsDao, group As GroupEntry, testPhrases As Boolean, testMarked As Boolean, queryLanguage As QueryLanguage) As TestData
        Dim groupDto = groupDao.Load(group)
        ' TODO: filter
        Dim words As ICollection(Of WordEntry) = New List(Of WordEntry)
        For Each word As TestWord In groupDto.Entries
            words.Add(word.WordEntry)
        Next
        Return New TestData(cards, words, queryLanguage)
    End Function
End Class
