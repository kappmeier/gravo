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
    ''' <param name="mainLanguage">The target language the user is training.</param>
    ''' <returns></returns>
    Public Shared Function Create(dictionary As IDictionaryDao, cards As ICardsDao, language As String,
                                  testPhrases As Boolean, queryLanguage As QueryLanguage,
                                  Optional mainLanguage As String = "german") As TestData
        Dim words As ICollection(Of WordEntry) = dictionary.GetWords(language, mainLanguage)
        If Not testPhrases Then
            words = words.Where(Function(t) t.WordType <> WordType.SetPhrase).ToList()
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
        Dim entries As IEnumerable(Of TestWord) = groupDto.Entries
        If testMarked Then
            entries = entries.Where(Function(t) t.Marked)
        End If
        If Not testPhrases Then
            entries = entries.Where(Function(t) t.WordType <> WordType.SetPhrase)
        End If
        Dim words As ICollection(Of WordEntry) = entries.Select(Function(t) t.WordEntry).ToList()
        Return New TestData(cards, words, queryLanguage)
    End Function
End Class
