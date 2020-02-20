Imports System.Collections.ObjectModel
Imports Gravo

Public Interface IDictionaryDao

    Class UpdateData
        Private _word As String = Nothing
        Private _pre = Nothing
        Private _post = Nothing
        Private _wordType As Nullable(Of WordType) = Nothing
        Private _meaning As String = Nothing
        Private _additionalTargetLangInfo As String = Nothing
        Private _irregular As Boolean? = Nothing

        Public Property Word As String
            Get
                Return _word
            End Get
            Set(value As String)
                _word = value
            End Set
        End Property

        Function WordDefault(entry As WordEntry) As String
            Return If(_word, entry.Word)
        End Function

        Public Property Pre As Object
            Get
                Return _pre
            End Get
            Set(value As Object)
                _pre = value
            End Set
        End Property

        Function PreDefault(entry As WordEntry) As String
            Return If(_pre, entry.Pre)
        End Function

        Public Property Post As Object
            Get
                Return _post
            End Get
            Set(value As Object)
                _post = value
            End Set
        End Property

        Function PostDefault(entry As WordEntry) As String
            Return If(_post, entry.Post)
        End Function

        Public Property WordType As WordType
            Get
                Return _wordType.Value
            End Get
            Set(value As WordType)
                _wordType = value
            End Set
        End Property

        Function WordTypeDefault(entry As WordEntry) As WordType
            Return If(_wordType, entry.WordType)
        End Function

        Public Property Meaning As String
            Get
                Return _meaning
            End Get
            Set(value As String)
                _meaning = value
            End Set
        End Property

        Function MeaningDefault(entry As WordEntry) As String
            Return If(_meaning, entry.Meaning)
        End Function

        Public Property AdditionalTargetLangInfo As String
            Get
                Return _additionalTargetLangInfo
            End Get
            Set(value As String)
                _additionalTargetLangInfo = value
            End Set
        End Property

        Function AdditionalTargetLangInfoDefault(entry As WordEntry) As String
            Return If(_additionalTargetLangInfo, entry.AdditionalTargetLangInfo)
        End Function

        Public Property Irregular As Boolean
            Get
                Return _irregular
            End Get
            Set(value As Boolean)
                _irregular = value
            End Set
        End Property

        Function IrregularDefault(entry As WordEntry) As Boolean
            Return If(_irregular, entry.Irregular)
        End Function
    End Class

    Function GetEntry(mainEntry As MainEntry, word As String, meaning As String) As WordEntry

    Function GetWords(ByVal mainLanguage As String) As ICollection(Of WordEntry)

    Function GetWords(ByVal mainEntry As String, ByVal subEntry As String, ByVal language As String, ByVal mainLanguage As String) As ICollection(Of WordEntry)

    Function GetWords(ByVal language As String, ByVal mainLanguage As String) As ICollection(Of WordEntry)

    Function GetWords(ByVal language As String, ByVal mainLanguage As String, ByVal startsWith As String) As ICollection(Of WordEntry)

    Function GetSubWords(ByVal mainEntry As String, ByVal language As String, ByVal mainLanguage As String) As ICollection(Of WordEntry)

    Function GetWordsAndSubWords(ByVal mainEntry As MainEntry) As ICollection(Of WordEntry)

    Function GetWordsAndSubWords(ByVal mainEntry As String, ByVal language As String, ByVal mainLanguage As String) As ICollection(Of WordEntry)

    Function GetWordsWithMeaning(ByVal meaning As String, ByVal language As String, ByVal mainLanguage As String) As ICollection(Of WordEntry)

    Function AddEntry(ByVal word As String, ByVal language As String, ByVal mainLanguage As String) As MainEntry

    Sub AddSubEntry(ByRef entry As WordEntry, ByVal mainEntry As String, ByVal language As String, ByVal mainLanguage As String)

    ''' <summary>
    ''' Sets data entries of a word entry to new values.
    ''' </summary>
    ''' <param name="entry"></param>
    ''' <param name="updateData"></param>
    ''' <returns></returns>
    Function ChangeEntry(ByRef entry As WordEntry, ByVal updateData As UpdateData) As WordEntry

    ''' <summary>
    ''' Sets a new main entry for a word
    ''' </summary>
    ''' <param name="entry"></param>
    ''' <param name="mainEntry"></param>
    ''' <returns></returns>
    Sub ChangeEntry(entry As WordEntry, mainEntry As MainEntry)

    Function GetMainEntry(ByRef word As WordEntry) As MainEntry

    Function GetMainEntry(ByRef mainEntry As String, ByVal language As String, ByVal mainLanguage As String) As MainEntry

    Function GetMainEntries(ByVal language As String, ByVal mainLanguage As String) As ICollection(Of MainEntry)

    Function GetMainEntries(ByVal language As String, ByVal mainLanguage As String, ByVal startsWith As String) As ICollection(Of MainEntry)

    Function ChangeMainEntry(ByRef mainEntry As MainEntry, ByVal newWord As String) As MainEntry

    ''' <summary>
    ''' Changes all sub entries of a main entry with a given word to the main entrie's word entry.
    ''' </summary>
    ''' <param name="mainEntry">The main entry whose sub entries are adapted.</param>
    ''' <param name="word">The word that the sub entries must have.</param>
    Sub AdaptSubEntries(ByRef mainEntry As MainEntry, ByVal word As String)

    Function WordCount(ByVal language As String, ByVal mainLanguage As String) As Integer

    Function WordCount(ByVal language As String, ByVal mainLanguage As String, ByVal startsWith As String) As Integer

    Function WordCountTotal(ByVal language As String, ByVal mainLanguage As String) As Integer

    Function FindSimilar(ByVal beginning As String, ByVal language As String, ByVal mainLanguage As String) As String

    Function DictionaryMainLanguages() As ICollection(Of String)

    Function DictionaryLanguages(ByVal mainLanguage As String) As ICollection(Of String)
End Interface
