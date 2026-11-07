Imports System.Collections.ObjectModel
Imports Gravo

Public Interface IDictionaryDao

    Class UpdateData
        Private _word As String
        Private _pre As String
        Private _post As String
        Private _wordType As Nullable(Of WordType)
        Private _meaning As String
        Private _additionalTargetLangInfo As String
        Private _irregular As Boolean?

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

        Public Property Pre As String
            Get
                Return _pre
            End Get
            Set(value As String)
                _pre = value
            End Set
        End Property

        Function PreDefault(entry As WordEntry) As String
            Return If(_pre, entry.Pre)
        End Function

        Public Property Post As String
            Get
                Return _post
            End Get
            Set(value As String)
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
                Return CBool(_irregular)
            End Get
            Set(value As Boolean)
                _irregular = value
            End Set
        End Property

        Function IrregularDefault(entry As WordEntry) As Boolean
            Return If(_irregular, entry.Irregular)
        End Function
    End Class

    ''' <summary>
    ''' Returns the word of a main entry by its word and meaning.
    ''' </summary>
    ''' <remarks>Both keys are matched trimmed, as the dictionary stores them.</remarks>
    ''' <param name="mainEntry">The main entry the word belongs to.</param>
    ''' <param name="word">The word.</param>
    ''' <param name="meaning">The meaning of the word.</param>
    ''' <returns>The stored word with its index.</returns>
    ''' <exception cref="EntryNotFoundException">When the main entry has no such word.</exception>
    ''' <exception cref="ArgumentNullException">
    ''' When <paramref name="word"/> or <paramref name="meaning"/> is <c>Nothing</c>.
    ''' </exception>
    Function GetEntry(mainEntry As MainEntry, word As String, meaning As String) As WordEntry

    Function GetWords(ByVal mainEntry As String, ByVal subEntry As String, ByVal language As String, ByVal mainLanguage As String) As ICollection(Of WordEntry)

    Function GetWords(ByVal language As String, ByVal mainLanguage As String) As ICollection(Of WordEntry)

    Function GetWords(ByVal language As String, ByVal mainLanguage As String, ByVal startsWith As String) As ICollection(Of WordEntry)

    Function GetSubWords(ByVal mainEntry As String, ByVal language As String, ByVal mainLanguage As String) As ICollection(Of WordEntry)

    Function GetWordsAndSubWords(ByVal mainEntry As MainEntry) As ICollection(Of WordEntry)

    Function GetWordsAndSubWords(ByVal mainEntry As String, ByVal language As String, ByVal mainLanguage As String) As ICollection(Of WordEntry)

    Function GetWordsWithMeaning(ByVal meaning As String, ByVal language As String, ByVal mainLanguage As String) As ICollection(Of WordEntry)

    ''' <summary>
    ''' Adds a main entry.
    ''' </summary>
    ''' <param name="word">The word of the main entry.</param>
    ''' <param name="language">The language of the word.</param>
    ''' <param name="mainLanguage">The language of the main entry.</param>
    ''' <returns>The new main entry.</returns>
    ''' <exception cref="InputException">
    ''' When the word is empty after trimming, the language is empty or a text holds a forbidden character.
    ''' </exception>
    ''' <exception cref="EntryExistsException">When the language has the main entry already.</exception>
    Function AddEntry(ByVal word As String, ByVal language As String, ByVal mainLanguage As String) As MainEntry

    ''' <summary>
    ''' Adds a word to a main entry.
    ''' </summary>
    ''' <remarks>
    ''' The text fields are stored trimmed. A word that is empty after trimming is rejected.
    ''' </remarks>
    ''' <param name="entry">The word to add.</param>
    ''' <param name="mainEntry">The main entry the word belongs to.</param>
    ''' <param name="language">The language of the word.</param>
    ''' <param name="mainLanguage">The language of the main entry.</param>
    ''' <exception cref="InputException">
    ''' When the word is empty or a text field holds a forbidden character.
    ''' </exception>
    ''' <exception cref="EntryExistsException">When the main entry has the word already.</exception>
    ''' <exception cref="EntryNotFoundException">When the main entry does not exist.</exception>
    Sub AddSubEntry(ByVal entry As WordEntry, ByVal mainEntry As String, ByVal language As String,
        ByVal mainLanguage As String)

    ''' <summary>
    ''' Sets data entries of a word entry to new values.
    ''' </summary>
    ''' <remarks>
    ''' Changed text fields are stored trimmed. A word that is empty after trimming is rejected.
    ''' </remarks>
    ''' <param name="entry">The word to change.</param>
    ''' <param name="updateData">The new values. A field that is <c>Nothing</c> stays unchanged.</param>
    ''' <returns>The changed word.</returns>
    ''' <exception cref="InputException">
    ''' When the word is empty after trimming or a text field holds a forbidden character.
    ''' </exception>
    ''' <exception cref="EntryExistsException">
    ''' When the main entry has a word with the new word and meaning already.
    ''' </exception>
    Function ChangeEntry(ByVal entry As WordEntry, ByVal updateData As UpdateData) As WordEntry

    ''' <summary>
    ''' Sets a new main entry for a word.
    ''' </summary>
    ''' <param name="entry">The word to move.</param>
    ''' <param name="mainEntry">The new main entry of the word.</param>
    ''' <exception cref="EntryNotFoundException">When the main entry does not exist.</exception>
    Sub ChangeEntry(entry As WordEntry, mainEntry As MainEntry)

    Function GetMainEntry(ByVal word As WordEntry) As MainEntry

    Function GetMainEntry(ByVal mainEntry As String, ByVal language As String, ByVal mainLanguage As String
        ) As MainEntry

    Function GetMainEntries(ByVal language As String, ByVal mainLanguage As String) As ICollection(Of MainEntry)

    Function GetMainEntries(ByVal language As String, ByVal mainLanguage As String, ByVal startsWith As String
        ) As ICollection(Of MainEntry)

    ''' <summary>
    ''' Renames a main entry and the sub entries whose word equals the old word of the main entry. The renaming is
    ''' securely performed in one transaction.
    ''' </summary>
    ''' <param name="mainEntry">The main entry to rename.</param>
    ''' <param name="newWord">The new word of the main entry. Will be trimmed.</param>
    ''' <returns>The renamed main entry.</returns>
    ''' <exception cref="InputException">
    ''' When the word is empty after trimming or holds a forbidden character.
    ''' </exception>
    ''' <exception cref="EntryNotFoundException">When the main entry does not exist.</exception>
    ''' <exception cref="EntryExistsException">
    ''' When another main entry has the new word already, or when a renamed sub entry would equal another word of the
    ''' main entry in word and meaning. Nothing is changed then.
    ''' </exception>
    Function ChangeMainEntry(ByVal mainEntry As MainEntry, ByVal newWord As String) As MainEntry

    Function WordCount(ByVal language As String, ByVal mainLanguage As String) As Integer

    Function WordCount(ByVal language As String, ByVal mainLanguage As String, ByVal startsWith As String) As Integer

    Function WordCountTotal(ByVal language As String, ByVal mainLanguage As String) As Integer

    Function FindSimilar(ByVal beginning As String, ByVal language As String, ByVal mainLanguage As String) As String

    Function DictionaryMainLanguages() As ICollection(Of String)

    Function DictionaryLanguages(ByVal mainLanguage As String) As ICollection(Of String)
End Interface
