''' <summary>
''' Copies dictionary entries from a vocabulary database (including groups and group entries, see
''' <see cref="VocabularyDatabase"/>). Transfers databases are selected by the constructor.
''' </summary>
''' <remarks>
''' Transfer follows the DAO uniqueness rules: a main entry is matched by (word, language, main language), a word by
''' (main entry, word, meaning). Existing target rows are never modified. New words get assigned a default <c>Cards</c>
''' Card statistics (user data) are not transferred.
''' A forbidden character (<see cref="DaoTools.ForbiddenCharacters"/>) in the source aborts the transfer with an
''' <see cref="InputException"/>. The merge is idempotent, transfer can be retried after fixing the source.
'''
''' The DAOs read NULL text columns as "" (via <c>SecureGetString</c>), so a transfer writes these values as "".
''' </remarks>
Public Class DatabaseTransfer
    Private ReadOnly source As VocabularyDatabase
    Private ReadOnly target As VocabularyDatabase

    Public Sub New(source As VocabularyDatabase, target As VocabularyDatabase)
        Me.source = source
        Me.target = target
    End Sub

    ''' <summary>
    ''' Copies all main entries of one language and their words.
    ''' </summary>
    ''' <param name="includeEmptyMains">Whether main entries without any word are copied too.</param>
    Public Function CopyLanguage(language As String, mainLanguage As String, includeEmptyMains As Boolean) As TransferResult
        Dim result As New TransferResult()
        For Each main As MainEntry In source.Dictionary.GetMainEntries(language, mainLanguage)
            Dim words As ICollection(Of WordEntry) = source.Dictionary.GetWordsAndSubWords(main)
            If words.Count = 0 AndAlso Not includeEmptyMains Then Continue For

            Dim targetMain As MainEntry = GetOrAddMain(main.Word, main.Language, main.MainLanguage, result)
            For Each word As WordEntry In words
                GetOrAddWord(targetMain, word, result)
            Next
        Next
        Return result
    End Function

    ''' <summary>
    ''' Copies every language of a main language including empty main entries.
    ''' </summary>
    Public Function CopyDictionary(mainLanguage As String) As TransferResult
        Dim result As New TransferResult()
        For Each language As String In source.Dictionary.DictionaryLanguages(mainLanguage)
            result.Add(CopyLanguage(language, mainLanguage, True))
        Next
        Return result
    End Function

    ''' <summary>
    ''' Copies a main group including its subgroups, their words and the words' dictionary entries. Subgroups are
    ''' merged with existing ones without altering existing data: Group rows, dictionary rows and card statistics
    ''' already in the target database remain as before. The result counts the rows actually added, except
    ''' <c>Groups</c> counts the main group, whether added or not.
    ''' </summary>
    Public Function CopyGroup(groupName As String) As TransferResult
        Dim result As New TransferResult()
        Dim subGroups As ICollection(Of GroupEntry) = source.Groups.GetSubGroups(groupName)
        If subGroups.Count = 0 Then Return result
        result.Groups = 1

        For Each sourceGroup As GroupEntry In subGroups
            If Not target.Groups.GroupExists(groupName, sourceGroup.SubGroup) Then
                target.Groups.AddGroup(groupName, sourceGroup.SubGroup)
                result.SubGroups += 1
            End If
            Dim targetGroup As GroupEntry = target.Groups.GetGroup(groupName, sourceGroup.SubGroup)

            For Each testWord As TestWord In source.Group.Load(sourceGroup).Entries
                Dim sourceWord As WordEntry = testWord.WordEntry
                Dim sourceMain As MainEntry = source.Dictionary.GetMainEntry(sourceWord)
                Dim targetMain As MainEntry = GetOrAddMain(sourceMain.Word, sourceMain.Language,
                        sourceMain.MainLanguage, result)
                Dim targetWord As WordEntry = GetOrAddWord(targetMain, sourceWord, result)
                Dim marked As Boolean = testWord.Marked
                Dim example As String = testWord.Example
                Try
                    target.Group.Add(targetGroup, targetWord, marked, example)
                    result.GroupEntries += 1
                Catch ex As EntryExistsException
                    ' Already in the target subgroup. Keep existing as merge policy.
                End Try
            Next
        Next
        Return result
    End Function

    ''' <summary>
    ''' Copies every main group of the source to the target db.
    ''' </summary>
    Public Function CopyAllGroups() As TransferResult
        Dim result As New TransferResult()
        For Each groupName As String In source.Groups.GetGroups()
            result.Add(CopyGroup(groupName))
        Next
        Return result
    End Function

    Private Function GetOrAddMain(word As String, language As String, mainLanguage As String, result As TransferResult
            ) As MainEntry
        Try
            Return target.Dictionary.GetMainEntry(word, language, mainLanguage)
        Catch ex As EntryNotFoundException
            result.MainEntries += 1
            Return target.Dictionary.AddEntry(word, language, mainLanguage)
        End Try
    End Function

    ''' <summary>
    ''' Returns the target's word for a source word. If it does not exist, it is added. The result contains the
    ''' index in the target db.
    ''' </summary>
    Private Function GetOrAddWord(targetMain As MainEntry, word As WordEntry, result As TransferResult) As WordEntry
        Try
            Return target.Dictionary.GetEntry(targetMain, word.Word, word.Meaning)
        Catch ex As EntryNotFoundException
            Dim copy As New WordEntry(word.Word, word.Pre, word.Post, word.WordType, word.Meaning,
                    word.AdditionalTargetLangInfo, word.Irregular)
            target.Dictionary.AddSubEntry(copy, targetMain.Word, targetMain.Language, targetMain.MainLanguage)
            result.SubEntries += 1
            Return copy
        End Try
    End Function
End Class
