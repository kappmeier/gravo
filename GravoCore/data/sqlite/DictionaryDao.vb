Imports System.Collections.ObjectModel
Imports Gravo
Imports System.Runtime.CompilerServices
Imports System.Diagnostics.Contracts

Friend Module DictionaryExtensions

    ' TODO: use System.Collections.Generic.GetValueOrDefault after updating to .NET Standard 2.1
    <Extension()>
    Public Function GetValueOrDefault(Of TKey, TValue)(dictionary As IReadOnlyDictionary(Of TKey, TValue), key As TKey, defaultValue As TValue) As TValue
        If Not dictionary.TryGetValue(key, GetValueOrDefault) Then GetValueOrDefault = defaultValue
    End Function
End Module

Public Class DictionaryDao
    Implements IDictionaryDao

    Private ReadOnly DBConnection As IDataBaseOperation
    Private ReadOnly SELECT_WORDENTRY As String = "SELECT [Index], Word, Pre, Post, WordType, Meaning, TargetLanguageInfo, Irregular"

    Sub New(ByRef db As IDataBaseOperation)
        DBConnection = db
    End Sub

    Function GetEntry(mainEntry As MainEntry, word As String, meaning As String) As WordEntry Implements IDictionaryDao.GetEntry
        Dim command As String = SELECT_WORDENTRY & " FROM DictionaryWords WHERE MainIndex = ? AND Word = ? AND Meaning = ?"

        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {mainEntry.Index, word, meaning}))
        FailIfEmpty(DBConnection, Function() As Exception
                                      Return New EntryNotFoundException("There is no entry for the given word/meaning.")
                                  End Function)

        DBConnection.DBCursor.Read()
        GetEntry = GroupDao.Extract(DBConnection)
        DBConnection.DBCursor.Close()
    End Function

    Function GetWords(ByVal mainLanguage As String) As ICollection(Of WordEntry) Implements IDictionaryDao.GetWords
        Dim command = "SELECT W.[Index] FROM DictionaryWords AS W, DictionaryMain AS M WHERE W.MainIndex = M.[Index]"

    End Function

    Private ReadOnly GetWordsSelect As String = "SELECT W.[Index], W.Word, W.Pre, W.Post, W.WordType, W.Meaning, W.TargetLanguageInfo, W.Irregular FROM DictionaryWords AS W"
    Private ReadOnly GetWordsJoinWithMain As String = ", DictionaryMain AS M WHERE (W.MainIndex = M.[Index])"

    Function GetWords(ByVal mainEntry As String, ByVal subEntry As String, ByVal language As String, ByVal mainLanguage As String) As ICollection(Of WordEntry) Implements IDictionaryDao.GetWords
        Dim mainIndex As Int32 = GetEntryIndex(mainEntry, language, mainLanguage)
        Dim command As String = GetWordsSelect & " WHERE W.Word = ? AND W.MainIndex = ?"
        DBConnection.ExecuteReader(command, New List(Of String) From {EscapeSingleQuotes(subEntry), mainIndex})
        Return ExtractWordsFromCursor()
    End Function

    Function GetWords(ByVal language As String, ByVal mainLanguage As String) As ICollection(Of WordEntry) Implements IDictionaryDao.GetWords
        Dim command = GetWordsSelect & GetWordsJoinWithMain & "  AND (M.LanguageName = ?) AND (M.MainLanguage = ?)"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {language, mainLanguage}))
        Return ExtractWordsFromCursor()
    End Function

    Public Function GetWords(ByVal language As String, ByVal mainLanguage As String, ByVal startsWith As String) As ICollection(Of WordEntry) Implements IDictionaryDao.GetWords
        Dim command As String = GetWordsSelect & GetWordsJoinWithMain & " AND (M.LanguageName = ?) AND (M.MainLanguage = ?) AND (W.Word LIKE ?) AND (M.WordEntry LIKE ?) ORDER BY W.[Index]"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {language, mainLanguage, startsWith + "%", startsWith + "%"}))
        Return ExtractWordsFromCursor()
    End Function

    Function GetSubWords(ByVal mainEntry As String, ByVal language As String, ByVal mainLanguage As String) As ICollection(Of WordEntry) Implements IDictionaryDao.GetSubWords
        Dim words As New Collection(Of WordEntry)
        AddSubWordsToCollection(mainEntry, language, mainLanguage, words)
        Return words
    End Function

    Function GetWordsAndSubWords(ByVal mainEntry As MainEntry) As ICollection(Of WordEntry) Implements IDictionaryDao.GetWordsAndSubWords
        Return GetWordsAndSubWords(mainEntry.Word, mainEntry.Language, mainEntry.MainLanguage)
    End Function

    Function GetWordsAndSubWords(ByVal MainEntry As String, ByVal Language As String, ByVal MainLanguage As String) As ICollection(Of WordEntry) Implements IDictionaryDao.GetWordsAndSubWords
        GetWordsAndSubWords = GetWords(MainEntry, MainEntry, Language, MainLanguage)
        AddSubWordsToCollection(MainEntry, Language, MainLanguage, GetWordsAndSubWords)
    End Function

    Function GetWordsWithMeaning(ByVal meaning As String, ByVal language As String, ByVal mainLanguage As String) As ICollection(Of WordEntry) Implements IDictionaryDao.GetWordsWithMeaning
        Dim command As String = GetWordsSelect & GetWordsJoinWithMain & " AND W.Meaning = ? AND M.LanguageName = ? AND M.MainLanguage = ?"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {meaning, language, mainLanguage}))
        Return ExtractWordsFromCursor()
    End Function

    Private Function ExtractWordsFromCursor() As ICollection(Of WordEntry)
        ExtractWordsFromCursor = New Collection(Of WordEntry)
        If Not DBConnection.DBCursor.HasRows Then
            DBConnection.DBCursor.Close()
        Else
            Do While DBConnection.DBCursor.Read()
                Dim wordEntry = GroupDao.Extract(DBConnection)
                ExtractWordsFromCursor.Add(wordEntry)
            Loop
            DBConnection.DBCursor.Close()
        End If
    End Function

    Public Function AddEntry(ByVal Word As String, ByVal Language As String, ByVal MainLanguage As String) As MainEntry Implements IDictionaryDao.AddEntry
        If Word = "" Then Throw New InputException(InputException.ErrorType.NoWord)
        If Language = "" Then Throw New InputException(InputException.ErrorType.NoLanguage)
        Try
            GetEntryIndex(Word, Language, MainLanguage)
        Catch ex As EntryNotFoundException
            ' Eintrag nicht gefunden, kann also hinzugefügt werden
            Dim command As String = "INSERT INTO DictionaryMain (WordEntry, LanguageName, MainLanguage) VALUES(?, ?, ?)"
            DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(New List(Of Object) From {Word, Language, MainLanguage}))
            Return GetMainEntry(Word, Language, MainLanguage)
        Catch ex As LanguageNotFoundException
            ' Sprache nicht vorhanden! kann also auf jeden fall eingefügt werden
            Dim command As String = "INSERT INTO DictionaryMain (WordEntry, LanguageName, MainLanguage) VALUES(?, ?, ?)"
            DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(New List(Of Object) From {Word, Language, MainLanguage}))
            Return GetMainEntry(Word, Language, MainLanguage)
        Catch ex As Exception
            ' Etwas anderes ist schiefgegangen. Weiterleiten
            Throw ex
        End Try
        ' Nichts schiefgegangen, das heißt es gibt ein Wort mit diesem Index
        Throw New EntryExistsException("Entry for " & Word & " already exists")
    End Function

    Public Sub AddSubEntry(ByRef Entry As WordEntry, ByVal MainEntry As String, ByVal Language As String, ByVal MainLanguage As String) Implements IDictionaryDao.AddSubEntry
        Dim mainIndex As Integer
        Try
            mainIndex = GetEntryIndex(MainEntry, Language, MainLanguage)
        Catch ex As LanguageNotFoundException
            Throw New EntryNotFoundException("Main entry not found for languages", ex)
        Catch ex As EntryNotFoundException
            Throw ex
        End Try

        ' Check, if there is already the exact same entry (i.e. word and meaning are the same)
        Dim command As String = "SELECT [Index] FROM DictionaryWords WHERE MainIndex = ? AND Word = ? AND Meaning = ?"
        DBConnection.ExecuteReader(command, New List(Of String) From {mainIndex, EscapeSingleQuotes(Entry.Word), EscapeSingleQuotes(Entry.Meaning)})
        FailIfExists(DBConnection, Function() As Exception
                                       Throw New EntryExistsException("The entry exists already with the same meaning for the main entry.")
                                   End Function)

        command = "INSERT INTO DictionaryWords (MainIndex, Word, Pre, Post, WordType, Meaning, TargetLanguageInfo, Irregular) VALUES(?, ?, ?, ?, ?, ?, ?, ?)"
        DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(New List(Of Object) From {mainIndex, Entry.Word, Entry.Pre, Entry.Post, Entry.WordType, Entry.Meaning, Entry.AdditionalTargetLangInfo, If(Entry.Irregular, 1, 0)}))

        ' Card-Status hinzufügen
        Dim card As New CardsDao(DBConnection)
        Dim subEntryIndex As Integer = GetSubEntryIndex(mainIndex, Entry.Word, Entry.Meaning)
        card.AddNewEntry(subEntryIndex)
        Entry.Index = subEntryIndex
    End Sub

    Function ChangeEntry(ByRef entry As WordEntry, ByVal updateData As IDictionaryDao.UpdateData) As WordEntry Implements IDictionaryDao.ChangeEntry
        If (updateData.Word IsNot Nothing AndAlso updateData.Word <> entry.Word) OrElse (updateData.Meaning IsNot Nothing AndAlso updateData.Meaning <> entry.Meaning) Then
            Dim newWord = updateData.WordDefault(entry)
            Dim newMeaning = updateData.MeaningDefault(entry)
            CheckViolation(newWord, newMeaning, GetMainEntry(entry).Index)
        End If
        Dim baseCommand = "UPDATE DictionaryWords SET "

        Dim updateCommand As String = ""
        Dim params As New List(Of Object)

        If updateData.WordDefault(entry) <> entry.Word Then
            updateCommand = updateCommand & ", Word = ?"
            params.Add(updateData.Word)
        End If
        If updateData.PreDefault(entry) <> entry.Pre Then
            updateCommand = updateCommand & ", Pre = ?"
            params.Add(updateData.Pre)
        End If
        If updateData.PostDefault(entry) <> entry.Post Then
            updateCommand = updateCommand & ", Post = ?"
            params.Add(updateData.Post)
        End If
        If updateData.WordTypeDefault(entry) <> entry.WordType Then
            updateCommand = updateCommand & ", WordType = ?"
            params.Add(updateData.WordType)
        End If
        If updateData.MeaningDefault(entry) <> entry.Meaning Then
            updateCommand = updateCommand & ", Meaning = ?"
            params.Add(updateData.Meaning)
        End If
        If updateData.AdditionalTargetLangInfoDefault(entry) <> entry.AdditionalTargetLangInfo Then
            updateCommand = updateCommand & ", TargetLanguageInfo = ?"
            params.Add(updateData.AdditionalTargetLangInfo)
        End If
        If updateData.IrregularDefault(entry) <> entry.Irregular Then
            updateCommand = updateCommand & ", Irregular = ?"
            params.Add(updateData.Irregular)
        End If
        ' If nothing is gonna be changed simply return the original entry
        If updateCommand.Length = 0 Then Return entry
        updateCommand = updateCommand.Substring(1)

        Dim postCommand = " WHERE [Index] = ?"
        params.Add(entry.WordIndex)
        Dim command = baseCommand & updateCommand & postCommand
        DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(params))

        ChangeEntry = New WordEntry(entry.WordIndex, updateData.WordDefault(entry), updateData.PreDefault(entry),
                                        updateData.PostDefault(entry), updateData.WordTypeDefault(entry),
                                        updateData.MeaningDefault(entry), updateData.AdditionalTargetLangInfoDefault(entry),
                                        updateData.IrregularDefault(entry))
    End Function

    Sub ChangeEntry(entry As WordEntry, mainEntry As MainEntry) Implements IDictionaryDao.ChangeEntry
        CheckMainEntry(mainEntry)

        Dim command = "UPDATE DictionaryWords SET MainIndex = ? WHERE [Index] = ?"
        DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(New List(Of Object) From {mainEntry.Index, entry.Index}))
    End Sub


    Friend Shared Function GetU(wordEntry As WordEntry, field As WordEntryFields)
        Select Case field
            Case WordEntryFields.Word
                GetU = wordEntry.Word
            Case WordEntryFields.Pre
                GetU = wordEntry.Pre
            Case WordEntryFields.Post
                GetU = wordEntry.Post
            Case WordEntryFields.WordType
                GetU = wordEntry.WordType
            Case WordEntryFields.Meaning
                GetU = wordEntry.Meaning
            Case WordEntryFields.TargetLanguageInfo
                GetU = wordEntry.AdditionalTargetLangInfo
            Case WordEntryFields.Irregular
                GetU = wordEntry.Irregular
            Case Else
                Contract.Assert(False)
        End Select
    End Function

    Private Sub CheckViolation(newWord As String, newMeaning As String, mainIndex As Int32)
        Dim command As String = "SELECT [Index] FROM [DictionaryWords] WHERE [MainIndex] = ? AND [Word] = ? AND [Meaning] = ?"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {mainIndex, newWord, newMeaning}))
        If DBConnection.DBCursor.HasRows Then
            DBConnection.DBCursor.Close()
            Throw New EntryExistsException("Entry for " & newWord & " and " & newMeaning & " exists.")
        End If
        DBConnection.DBCursor.Close()
    End Sub

    Private Sub AddSubWordsToCollection(ByVal MainEntry As String, ByVal Language As String, ByVal MainLanguage As String, ByRef gCol As ICollection(Of WordEntry))
        Dim mainIndex As Int32 = GetEntryIndex(MainEntry, Language, MainLanguage)
        Dim command As String
        command = "SELECT [Index], Word, Pre, Post, WordType, Meaning, TargetLanguageInfo, Irregular FROM DictionaryWords WHERE (NOT Word= ? ) AND MainIndex = ?"
        DBConnection.ExecuteReader(command, New List(Of String) From {EscapeSingleQuotes(MainEntry), mainIndex})
        If Not DBConnection.DBCursor.HasRows Then
            DBConnection.DBCursor.Close()
            Exit Sub
        End If
        Do While DBConnection.DBCursor.Read()
            Dim currentEntry = GroupDao.Extract(DBConnection)
            gCol.Add(currentEntry)
        Loop
        DBConnection.DBCursor.Close()
    End Sub

    Function GetMainEntry(ByRef word As WordEntry) As MainEntry Implements IDictionaryDao.GetMainEntry
        Dim mainIndex As Int32 = GetMainIndex(word)
        GetMainEntry = GetMainEntry(mainIndex)
    End Function

    Function GetMainEntry(ByRef mainEntry As String, ByVal language As String, ByVal mainLanguage As String) As MainEntry Implements IDictionaryDao.GetMainEntry
        Dim command As String = "SELECT [Index], WordEntry, LanguageName, MainLanguage FROM DictionaryMain WHERE WordEntry = ? AND LanguageName = ? AND MainLanguage = ?"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {mainEntry, language, mainLanguage}))
        FailIfEmpty(DBConnection, Function() As Exception
                                      Return New EntryNotFoundException("Main entry not found.")
                                  End Function)

        DBConnection.DBCursor.Read()
        GetMainEntry = ExtractMainEntry(DBConnection)
        DBConnection.DBCursor.Close()
    End Function

    Private Function GetMainEntry(ByVal mainIndex As Int32) As MainEntry
        Dim command As String = "SELECT [Index], WordEntry, LanguageName, MainLanguage FROM DictionaryMain WHERE [Index] = ?"
        DBConnection.ExecuteReader(command, Enumerable.Repeat(EscapeSingleQuotes(mainIndex), 1))
        FailIfEmpty(DBConnection, Function() As Exception
                                      Return New EntryNotFoundException("No main entry found for given entry.")
                                  End Function)

        DBConnection.DBCursor.Read()
        GetMainEntry = ExtractMainEntry(DBConnection)
        DBConnection.DBCursor.Close()
    End Function

    Private Function GetMainIndex(word As WordEntry) As Int32
        Dim command As String = "SELECT [MainIndex] FROM [DictionaryWords] WHERE [Index] = ?"
        DBConnection.ExecuteReader(command, Enumerable.Repeat(EscapeSingleQuotes(word.WordIndex), 1))
        FailIfEmpty(DBConnection, Function() As Exception
                                      Return New EntryNotFoundException("Data for word not in database.")
                                  End Function)

        DBConnection.DBCursor.Read()
        GetMainIndex = DBConnection.SecureGetInt32(0)
        DBConnection.DBCursor.Close()
    End Function

    Public Function GetMainEntries(ByVal language As String, ByVal mainLanguage As String) As ICollection(Of MainEntry) Implements IDictionaryDao.GetMainEntries
        Dim command As String = "SELECT [Index], WordEntry FROM DictionaryMain WHERE LanguageName = ? AND MainLanguage = ? ORDER BY WordEntry"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {language, mainLanguage}))
        GetMainEntries = ExtractMainEntries(DBConnection, language, mainLanguage)
        DBConnection.DBCursor.Close()
    End Function

    Public Function GetMainEntries(ByVal language As String, ByVal mainLanguage As String, ByVal startsWith As String) As ICollection(Of MainEntry) Implements IDictionaryDao.GetMainEntries
        Dim command As String = "SELECT [Index], WordEntry FROM DictionaryMain WHERE LanguageName = ? AND MainLanguage = ? AND WordEntry LIKE ?"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {language, mainLanguage, startsWith + "%"}))
        GetMainEntries = ExtractMainEntries(DBConnection, language, mainLanguage)
        DBConnection.DBCursor.Close()
    End Function

    Private Function ExtractMainEntries(DBConnection As IDataBaseOperation, language As String, mainLanguage As String) As ICollection(Of MainEntry)
        ExtractMainEntries = New Collection(Of MainEntry)
        Do While DBConnection.DBCursor.Read()
            Dim wordEntry = ExtractMainEntry(DBConnection, language, mainLanguage)
            ExtractMainEntries.Add(wordEntry)
        Loop
    End Function

    ''' <summary>
    ''' Extracts the data entries in the following list. The data is assumed to start at index 0.
    ''' <list type="type">
    '''   <item><description>index</description></item>
    '''   <item><description>word</description></item>
    '''   <item><description>language</description></item>
    '''   <item><description>main language</description></item>
    ''' </list>
    ''' </summary>
    ''' <param name="dbConnection"></param>
    ''' <returns></returns>
    Shared Function ExtractMainEntry(dbConnection As IDataBaseOperation) As MainEntry
        Dim language As String = dbConnection.SecureGetString(2)
        Dim mainLanguage As String = dbConnection.SecureGetString(3)
        Return ExtractMainEntry(dbConnection, language, mainLanguage)
    End Function

    Shared Function ExtractMainEntry(dbConnection As IDataBaseOperation, language As String, mainLanguage As String) As MainEntry
        Dim index As Integer = dbConnection.SecureGetInt32(0)
        Dim word As String = dbConnection.SecureGetString(1)
        ExtractMainEntry = New MainEntry(index, word, language, mainLanguage)
    End Function

    Function ChangeMainEntry(ByRef mainEntry As MainEntry, ByVal newWord As String) As MainEntry Implements IDictionaryDao.ChangeMainEntry
        ' Verify that the original entry exists
        CheckMainEntry(mainEntry)

        ' Verify that the entry with updated word would not conflict
        Dim command = "SELECT COUNT(*) FROM DictionaryMain WHERE WordEntry = ? AND LanguageName = ? AND MainLanguage = ?"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {newWord, mainEntry.Language, mainEntry.MainLanguage}))
        DBConnection.DBCursor.Read()
        Dim Count = DBConnection.SecureGetInt32(0)
        DBConnection.DBCursor.Close()
        If Count > 0 Then Throw New EntryExistsException("Entry " & newWord & " already exists.")

        command = "UPDATE DictionaryMain SET WordEntry = ? WHERE [Index] = ?"
        DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(New List(Of Object) From {newWord, mainEntry.Index}))
        Return New MainEntry(mainEntry.Index, newWord, mainEntry.Language, mainEntry.MainLanguage)
    End Function

    Private Sub CheckMainEntry(mainEntry As MainEntry)
        Dim dbEntry = GetMainEntry(mainEntry.Index)
        If Not dbEntry.Equals(mainEntry) Then Throw New EntryNotFoundException("Given entry not in database")
    End Sub

    Sub AdaptSubEntries(ByRef mainEntry As MainEntry, ByVal word As String) Implements IDictionaryDao.AdaptSubEntries
        Dim indices As ICollection(Of Integer) = GetSubEntryIndices(mainEntry.Index, word)
        Dim command As String
        For Each index As Integer In indices
            command = "UPDATE DictionaryWords SET Word = ? WHERE [Index] = ?"
            DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(New List(Of Object) From {mainEntry.Word, index}))
        Next
    End Sub

    Private Function GetSubEntryIndices(ByVal mainIndex As Integer, ByVal word As String) As ICollection(Of Integer)
        GetSubEntryIndices = New Collection(Of Integer)
        Dim command As String = "SELECT [Index] FROM DictionaryWords WHERE Word= ? AND MainIndex = ?"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {word, mainIndex}))

        If DBConnection.DBCursor.HasRows = True Then
            Do While DBConnection.DBCursor.Read()
                GetSubEntryIndices.Add(DBConnection.SecureGetInt32(0))
            Loop
        End If
        DBConnection.DBCursor.Close()
    End Function

    Function GetEntryIndex(ByVal MainEntry As String, ByVal Language As String, ByVal MainLanguage As String) As Integer
        ' Check language exists at all
        Dim command As String = "SELECT DISTINCT LanguageName FROM DictionaryMain WHERE LanguageName = ?"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(Language))
        FailIfEmpty(DBConnection, Function() As Exception
                                      Return New LanguageNotFoundException("Language " & Language & " does not exist.")
                                  End Function)

        ' Check main language exists at all
        command = "SELECT DISTINCT LanguageName FROM DictionaryMain WHERE MainLanguage = ?"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(MainLanguage))
        FailIfEmpty(DBConnection, Function() As Exception
                                      Return New LanguageNotFoundException("Main language " & MainLanguage & " does not exist.")
                                  End Function)

        ' Detect index for word
        command = "SELECT [Index] FROM DictionaryMain WHERE WordEntry = ? AND LanguageName = ? AND MainLanguage = ?"
        DBConnection.ExecuteReader(command, New List(Of String) From {EscapeSingleQuotes(MainEntry), EscapeSingleQuotes(Language), EscapeSingleQuotes(MainLanguage)})
        FailIfEmpty(DBConnection, Function() As Exception
                                      Return New EntryNotFoundException("Main entry " & MainEntry & " not found for language " & Language & ".")
                                  End Function)

        DBConnection.DBCursor.Read()
        GetEntryIndex = DBConnection.SecureGetInt32(0)
        DBConnection.DBCursor.Close()
    End Function

    Public Function GetSubEntryIndex(ByVal MainIndex As Integer, ByVal Word As String, ByVal Meaning As String) As Integer
        Dim command As String = "SELECT [Index] FROM DictionaryWords WHERE Word = ? AND Meaning = ? AND MainIndex= ?"
        DBConnection.ExecuteReader(command, New List(Of String) From {EscapeSingleQuotes(Word), EscapeSingleQuotes(Meaning), MainIndex})
        FailIfEmpty(DBConnection, Function() As Exception
                                      Return New EntryNotFoundException("There is no entry for the given word/meaning.")
                                  End Function)

        DBConnection.DBCursor.Read()
        GetSubEntryIndex = DBConnection.SecureGetInt32(0)
        DBConnection.DBCursor.Close()
    End Function

    Public Function WordCount(ByVal language As String, ByVal mainLanguage As String) As Integer Implements IDictionaryDao.WordCount
        Dim command As String = "SELECT COUNT([Index]) FROM DictionaryMain WHERE [LanguageName] = ? AND [MainLanguage] = ?"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {language, mainLanguage}))
        DBConnection.DBCursor.Read()
        WordCount = DBConnection.SecureGetInt32(0)
        DBConnection.DBCursor.Close()
    End Function

    Public Function WordCount(ByVal language As String, ByVal mainLanguage As String, ByVal startsWith As String) As Integer Implements IDictionaryDao.WordCount
        Dim command As String = "SELECT COUNT(M.[WordEntry]) FROM DictionaryMain AS M WHERE M.[WordEntry] LIKE ? AND M.[LanguageName] = ? AND M.[MainLanguage] = ?"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {startsWith & "%", language, mainLanguage}))
        DBConnection.DBCursor.Read()
        WordCount = DBConnection.SecureGetInt32(0)
        DBConnection.DBCursor.Close()
    End Function

    Public Function WordCountTotal(ByVal language As String, ByVal mainLanguage As String) As Integer Implements IDictionaryDao.WordCountTotal
        Dim command As String = "SELECT COUNT(W.[Index]) FROM DictionaryWords W, DictionaryMain M WHERE W.MainIndex = M.[Index] AND M.LanguageName = ? AND M.MainLanguage = ?"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {language, mainLanguage}))
        DBConnection.DBCursor.Read()
        WordCountTotal = DBConnection.SecureGetInt32(0)
        DBConnection.DBCursor.Close()
    End Function

    Public Function FindSimilar(ByVal wordBeginning As String, ByVal language As String, ByVal mainLanguage As String) As String Implements IDictionaryDao.FindSimilar
        Dim command As String = "SELECT M.[WordEntry] FROM DictionaryMain AS M WHERE M.[WordEntry] LIKE ? AND M.[LanguageName] = ? AND M.[MainLanguage] = ? ORDER BY M.[WordEntry]"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {wordBeginning & "%", language, mainLanguage}))
        If DBConnection.DBCursor.HasRows = False Then
            DBConnection.DBCursor.Close()
            Return ""
        End If
        DBConnection.DBCursor.Read()
        Dim word As String = DBConnection.SecureGetString(0)
        DBConnection.DBCursor.Close()
        Return word
    End Function

    Public Function DictionaryMainLanguages() As ICollection(Of String) Implements IDictionaryDao.DictionaryMainLanguages
        Dim mainLanguages As New Collection(Of String)
        Dim command As String = "SELECT DISTINCT MainLanguage FROM DictionaryMain ORDER BY MainLanguage;"
        DBConnection.ExecuteReader(command)
        Do While DBConnection.DBCursor.Read()
            mainLanguages.Add(DBConnection.SecureGetString(0))
        Loop
        DBConnection.DBCursor.Close()
        Return mainLanguages
    End Function

    Public Function DictionaryLanguages(ByVal mainLanguage As String) As ICollection(Of String) Implements IDictionaryDao.DictionaryLanguages
        Dim languages As New Collection(Of String)
        Dim command As String = "SELECT DISTINCT LanguageName FROM DictionaryMain WHERE MainLanguage = ? ORDER BY LanguageName;"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(mainLanguage))
        Do While DBConnection.DBCursor.Read()
            languages.Add(DBConnection.SecureGetString(0))
        Loop
        DBConnection.DBCursor.Close()
        Return languages
    End Function

End Class
