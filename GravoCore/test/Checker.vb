Imports Gravo

''' <summary>
''' Used to evaluate a word. Can be called multiple times to evaluate, and stores the status.
''' </summary>
Public Class Checker
    Private ReadOnly dictionaryDao As IDictionaryDao
    Private ReadOnly queryLanguage As QueryLanguage
    Private ReadOnly current As WordEntry
    Private m_retest As Boolean
    Private m_language As String = Nothing
    Private m_mainEntry As MainEntry = Nothing

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
            Throw New ArgumentException("Query language must be OriginalLanguage or TargetLanguage, but was " & queryLanguage.ToString() & ".")
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
        ElseIf LanguageRequiresReading() Then
            Return EvaluateActiveWithReading(input)
        Else
            ' The input was not literally correct and reading is not allowed. Investigate further.
            If String.Equals(input, Current.Word, StringComparison.OrdinalIgnoreCase) Then
                Return TestResult.Misspelled
            End If
            Dim mainEntry As MainEntry = DictionaryDao.GetMainEntry(Current)
            Dim words As ICollection(Of WordEntry) = DictionaryDao.GetWordsWithMeaning(Current.Meaning, mainEntry.Language, mainEntry.MainLanguage)
            For Each word In words
                If String.Equals(input, word.Word, StringComparison.OrdinalIgnoreCase) Then
                    Return TestResult.OtherMeaning
                End If
            Next word
            Return TestResult.Wrong
        End If
    End Function

    ''' <summary>
    ''' Evaluate whether the language also accepts Reading or match via canonical form (e.g. japanese)
    ''' </summary>
    ''' <returns>True if the language requires reading, otherwise false.</returns>
    Private Function LanguageRequiresReading() As Boolean
        Return Language = Languages.Japanese
    End Function

    ''' <summary>
    ''' Evaluate active vocabulary test with reading consideration for Japanese.
    ''' <para>Reading is checked for any kana in the canonical form.</para>
    ''' </summary>
    ''' <param name="input">the user input</param>
    ''' <remarks>Currently only Japanese is supported for reading.</remarks>
    ''' <returns>The result of the evaluation, indicating correctness or type of error.</returns>
    Private Function EvaluateActiveWithReading(ByVal input As String) As TestResult
        If Not String.IsNullOrEmpty(current.Reading) AndAlso input = current.Reading Then
            Return TestResult.NoError
        End If
        Dim target = If(String.IsNullOrEmpty(current.Reading), current.Word, current.Reading)
        Dim targetForm = KanaRomaji.CanonicalForm(target)
        Dim inputForm = KanaRomaji.CanonicalForm(input)
        If inputForm = targetForm Then
            Return TestResult.NoError
        End If
        If String.Equals(input, Current.Word, StringComparison.OrdinalIgnoreCase) Then
            Return TestResult.Misspelled
        End If
        Return TestResult.Wrong
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
            If String.Equals(input, Current.Meaning, StringComparison.OrdinalIgnoreCase) Then
                Return TestResult.Misspelled
            End If
            Dim mainEntry As MainEntry = DictionaryDao.GetMainEntry(Current)
            Dim words As ICollection(Of WordEntry) = DictionaryDao.GetWordsAndSubWords(mainEntry)
            For Each word In words
                If String.Equals(input, word.Meaning, StringComparison.OrdinalIgnoreCase) Then
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
                Throw New ArgumentException("Query language must be OriginalLanguage or TargetLanguage, but was " &
                    queryLanguage.ToString() & ".")
            End If
        End Get
    End Property

    ReadOnly Property Answer() As String
        Get
            If queryLanguage = QueryLanguage.OriginalLanguage Then
                If Not String.IsNullOrEmpty(current.Reading) Then
                    Return current.Word & "（" & current.Reading & "）"
                End If
                Return current.Word
            ElseIf queryLanguage = QueryLanguage.TargetLanguage Then
                Return current.Meaning
            Else
                Throw New ArgumentException("Query language must be OriginalLanguage or TargetLanguage, but was " &
                    queryLanguage.ToString() & ".")
            End If
        End Get
    End Property

    Public ReadOnly Property Info() As String
        Get
            If LanguageRequiresReading() AndAlso queryLanguage = QueryLanguage.TargetLanguage Then
                Return current.Reading
            End If
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

    Public ReadOnly Property Language As String
        Get
            If m_language Is Nothing Then
                Try
                    m_mainEntry = DictionaryDao.GetMainEntry(Current)
                    m_language = m_mainEntry.Language
                Catch ex As Exception
                    Return ""
                End Try
            End If
            Return m_language
        End Get
    End Property
End Class
