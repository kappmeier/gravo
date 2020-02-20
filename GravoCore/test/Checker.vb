Imports Gravo

''' <summary>
''' Used to evaluate a word. Can be called multiple times to evaluate, and stores the status.
''' </summary>
Public Class Checker
    Private ReadOnly dictionaryDao As IDictionaryDao
    Private ReadOnly queryLanguage As QueryLanguage
    Private ReadOnly current As WordEntry
    Private m_retest As Boolean

    Public Sub New(dictionaryDao As IDictionaryDao, queryLanguage As QueryLanguage, current As WordEntry)
        Me.dictionaryDao = dictionaryDao
        Me.queryLanguage = queryLanguage
        Me.current = current
    End Sub

    ''' <summary>
    ''' Evaluates a given input and classifies it into a result category.
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    Function Evaluate(ByVal input As String) As TestResult
        If queryLanguage = QueryLanguage.OriginalLanguage Then
            Return EvaluateActive(input)
        ElseIf queryLanguage = QueryLanguage.TargetLanguage Then
            Return EvaluatePassive(input)
        Else
            Throw New ArgumentException()
        End If
    End Function

    ''' <summary>
    ''' Evaluate active vocabulary test. Requires input in target language.
    ''' </summary>
    ''' <param name="input">the user input</param>
    ''' <returns></returns>
    Private Function EvaluateActive(ByVal input As String) As TestResult
        If input = current.Word Then
            Return TestResult.NoError
        Else
            ' The input was not literally correct. Investigate further.
            If input.ToUpper = Current.Word.ToUpper Then
                Return TestResult.Misspelled
            End If
            Dim mainEntry As MainEntry = DictionaryDao.GetMainEntry(Current)
            Dim words As ICollection(Of WordEntry) = DictionaryDao.GetWordsWithMeaning(Current.Meaning, mainEntry.Language, mainEntry.MainLanguage)
            For Each word In words
                If input = word.Word Then
                    Return TestResult.OtherMeaning
                End If
            Next word
            Return TestResult.Wrong
        End If
    End Function

    ''' <summary>
    ''' Evaluate passive vocabulary test. Requires input in original language.
    ''' </summary>
    ''' <param name="input">the user input</param>
    ''' <returns></returns>
    Private Function EvaluatePassive(ByVal input As String) As TestResult
        If input = current.Meaning Then
            Return TestResult.NoError
        Else
            ' The input was not literally correct. Investigate further.
            If input.ToUpper = Current.Meaning.ToUpper Then
                Return TestResult.Misspelled
            End If
            Dim mainEntry As MainEntry = DictionaryDao.GetMainEntry(Current)
            Dim words As ICollection(Of WordEntry) = DictionaryDao.GetWordsAndSubWords(mainEntry)
            For Each word In words
                If input = word.Meaning Then
                    Return TestResult.OtherMeaning
                End If
            Next word
            Return TestResult.Wrong
        End If

    End Function

    ReadOnly Property Question() As String
        Get
            If queryLanguage = QueryLanguage.OriginalLanguage Then
                Return current.Meaning
            ElseIf queryLanguage = QueryLanguage.TargetLanguage Then
                Return current.Word
            Else
                Throw New ArgumentException()
            End If
        End Get
    End Property

    ReadOnly Property Answer() As String
        Get
            If queryLanguage = QueryLanguage.OriginalLanguage Then
                Return current.Word
            ElseIf queryLanguage = QueryLanguage.TargetLanguage Then
                Return current.Meaning
            Else
                Throw New ArgumentException()
            End If
        End Get
    End Property

    Public ReadOnly Property Info() As String
        Get
            Return current.AdditionalTargetLangInfo
        End Get
    End Property

    Public Property Retest As Boolean
        Get
            Return m_retest
        End Get
        Friend Set(value As Boolean)
            m_retest = value
        End Set
    End Property
End Class
