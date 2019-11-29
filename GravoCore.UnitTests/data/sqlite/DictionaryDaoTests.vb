Imports System.Collections.ObjectModel
Imports System.Data.SQLite
Imports System.IO
Imports Gravo
Imports NUnit.Framework
Imports FluentAssertions

<TestFixture>
Public Class DictionaryDaoTests
    Private ReadOnly ResourceFile = "test-data-dictionary.s3db"
    Private _dictionaryDao As DictionaryDao
    Private _tempDb As String
    Private _db As IDataBaseOperation

    Private ReadOnly language = "lang"
    Private ReadOnly targetLanguage = "targetLang"
    Private ReadOnly word1MainEntry = New MockMainEntry(1, "word1", language, targetLanguage)
    Private ReadOnly word1entry1 = New GroupDaoTests.MockWordEntry(1, "word1", "pre", "post", WordType.Verb, "m", "l", False)
    Private ReadOnly word1entry1a = New GroupDaoTests.MockWordEntry(33, "word1a", "pre", "post", WordType.Verb, "m", "l", False)
    Private ReadOnly word1entry2 = New WordEntry("word1", "", "", WordType.Verb, "with a meaning", "", True)
    ''' <summary>
    ''' A word for the same main entry (word1), but with a sub entry that is not equal.
    ''' </summary>
    Private ReadOnly word1Variance = New WordEntry("word1 variance", "", "", WordType.Verb, "", "", False)
    ''' <summary>
    ''' A word for which only the main entry exists in the database.
    ''' </summary>
    Private ReadOnly wordWithoutSubwords = New WordEntry("wordx", "", "", WordType.Adjective, "", "", True)

    Private ReadOnly newWordIndex = 34
    ''' <summary>
    ''' A new word that can be added to an existing main index without violating the rules
    ''' </summary>
    Private ReadOnly newWord = New WordEntry("wordx", "", "", WordType.Adjective, "new meaning", "", True)
    Private ReadOnly nonExistingMainEntry = New MockMainEntry(9, "", "", "")
    Private ReadOnly lang2MainEntry = New MockMainEntry(2, "some word", "lang2", "targetLang")
    Private ReadOnly lang2AnotherMainEntry = New MockMainEntry(4, "another", "lang2", "targetLang")

    Public Class MockMainEntry
        Inherits MainEntry

        Protected Friend Sub New(index As Integer, word As String, language As String, mainLanguage As String)
            MyBase.New(index, word, language, mainLanguage)
        End Sub
    End Class

    <SetUp>
    Public Sub Setup()
        _tempDb = Path.GetTempFileName
        File.Copy(DaoUtils.GetSqliteResource(ResourceFile), _tempDb, True)

        _db = New SQLiteDataBaseOperation()
        _db.Open(_tempDb)

        _dictionaryDao = New DictionaryDao(_db)
    End Sub

    <TearDown>
    Public Sub CleanUp()
        _db.Close()
        SQLiteConnection.ClearAllPools()

        File.Delete(_tempDb)
    End Sub

    <Test>
    Public Sub GetWord_Existing_Returns()
        Dim wordEntry As WordEntry = _dictionaryDao.GetEntry(word1MainEntry, "word1", "m")
        wordEntry.Should.BeEquivalentTo(word1entry1)
    End Sub

    <Test>
    Public Sub GetWord_NonExisting_Fails()
        Assert.Throws(Of EntryNotFoundException)(Sub() _dictionaryDao.GetEntry(word1MainEntry, "non existing", "x"))
    End Sub

    <Test>
    Public Sub WordsLoaded_Existing_ListReturned()
        Dim words As ICollection(Of WordEntry) = _dictionaryDao.GetWords("word1", "word1", language, targetLanguage)

        Dim expectedResult = New List(Of WordEntry) From {word1entry1, word1entry2}

        words.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub GetWords_WordWithoutEntry_NothingReturned()
        Dim words As ICollection(Of WordEntry) = _dictionaryDao.GetWords("word1", "noWord", language, targetLanguage)

        Dim expectedResult = New List(Of WordEntry)

        words.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub GetWords_StartsWith_ReturnsWords()
        Dim words As ICollection(Of WordEntry) = _dictionaryDao.GetWords(language, targetLanguage, "wo")

        Dim expectedResult = New List(Of WordEntry) From {word1entry1, word1entry2, word1Variance, wordWithoutSubwords, word1entry1a}

        words.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub GetWords_StartsWith_ReturnsEmpty()
        Dim words As ICollection(Of WordEntry) = _dictionaryDao.GetWords(language, targetLanguage, "dr")

        Dim expectedResult = New List(Of WordEntry)

        words.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub WordsLoaded_Language_Fails()
        Assert.Throws(Of LanguageNotFoundException)(Sub() _dictionaryDao.GetWords("word1", "word1", "nonExistingLanguage", targetLanguage))
    End Sub

    <Test>
    Public Sub WordsLoaded_MainLanguage_Fails()
        Assert.Throws(Of LanguageNotFoundException)(Sub() _dictionaryDao.GetWords("word1", "word1", language, "nonExistingTargetLanguage"))
    End Sub

    <Test>
    Public Sub WordsLoaded_NoMainEntry_Fails()
        Assert.Throws(Of EntryNotFoundException)(Sub() _dictionaryDao.GetWords("noMainEntry", "word1", language, targetLanguage))
    End Sub

    <Test>
    Public Sub WordsAndSubwords_AdditionalSubwords_ListReturned()
        Dim words As Collection(Of WordEntry) = _dictionaryDao.GetWordsAndSubWords("word1", language, targetLanguage)

        Dim expectedResult = New List(Of WordEntry) From {word1entry1, word1entry2, word1Variance, word1entry1a}

        words.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub WordsAndSubwords_NoAdditionalSubwords_ListReturned()
        Dim words As Collection(Of WordEntry) = _dictionaryDao.GetWordsAndSubWords("wordx", language, targetLanguage)

        Dim expectedResult = New List(Of WordEntry) From {wordWithoutSubwords}

        words.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub AddEntry_ForNewWord_IsAdded()
        Dim newMainEntry = _dictionaryDao.AddEntry("new main entry", language, targetLanguage)

        newMainEntry.Should.BeEquivalentTo(New MockMainEntry(5, "new main entry", language, targetLanguage))
    End Sub

    <Test>
    Public Sub AddEntry_ForNewLanguage_IsAdded()
        Dim newMainEntry As MainEntry = _dictionaryDao.AddEntry("new main entry", "new language", targetLanguage)

        newMainEntry.Should.BeEquivalentTo(New MockMainEntry(5, "new main entry", "new language", targetLanguage))
    End Sub

    <Test>
    Public Sub AddEntry_WithSameWord_Fails()
        Assert.Throws(Of EntryExistsException)(Sub() _dictionaryDao.AddEntry("word1", language, targetLanguage))
    End Sub

    <Test>
    Public Sub AddSubEntry_WithNewValues_AreAdded()
        _dictionaryDao.AddSubEntry(newWord, "wordx", language, targetLanguage)

        Assert.AreEqual(newWordIndex, newWord.index)

        Dim updatedWords As Collection(Of WordEntry) = _dictionaryDao.GetWordsAndSubWords("wordx", language, targetLanguage)
        Dim expectedResult = New List(Of WordEntry) From {wordWithoutSubwords, newWord}

        updatedWords.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub AddSubEntry_WithSameWord_Fails()
        Assert.Throws(Of EntryExistsException)(Sub() _dictionaryDao.AddSubEntry(word1entry1, "word1", language, targetLanguage))
    End Sub

    <Test>
    Public Sub ChangeEntry_NewWordExists_Fails()
        Dim nameUpdate As New IDictionaryDao.UpdateData With {
            .Word = "word1"
        }
        Assert.Throws(Of EntryExistsException)(Sub() _dictionaryDao.ChangeEntry(word1entry1a, nameUpdate))
    End Sub

    <Test>
    Public Sub ChangeEntry_NewMeaningExists_Fails()
        Dim meaningUpdate As New IDictionaryDao.UpdateData With {
            .Meaning = "with a meaning"
        }
        Assert.Throws(Of EntryExistsException)(Sub() _dictionaryDao.ChangeEntry(word1entry1, meaningUpdate))
    End Sub

    <Test>
    Public Sub ChangeEntry_SameNewWordAndMeaning_ChangesNothing()
        Dim nameUpdate As New IDictionaryDao.UpdateData With {
            .Word = "word1"
        }
        Dim newEntryWord As WordEntry = _dictionaryDao.ChangeEntry(word1entry1, nameUpdate)
        newEntryWord.Should.BeEquivalentTo(word1entry1)

        Dim meaningUpdate As New IDictionaryDao.UpdateData With {
            .Meaning = "m"
        }
        Dim newEntryMeaning As WordEntry = _dictionaryDao.ChangeEntry(word1entry1, meaningUpdate)
        newEntryWord.Should.BeEquivalentTo(word1entry1)
    End Sub

    <Test>
    Public Sub ChangeEntry_ChangeWord_Works()
        Dim nameUpdate As New IDictionaryDao.UpdateData With {
            .Word = "word1 update"
        }
        Dim newEntryWord As WordEntry = _dictionaryDao.ChangeEntry(word1entry1, nameUpdate)
        Dim expectedResult As WordEntry = New GroupDaoTests.MockWordEntry(1, "word1 update", "pre", "post", WordType.Verb, "m", "l", False)
        newEntryWord.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub ChangeEntry_ChangeMeaning_Works()
        Dim meaningUpdate As New IDictionaryDao.UpdateData With {
            .Meaning = "new meaning"
        }
        Dim newEntryWord As WordEntry = _dictionaryDao.ChangeEntry(word1entry1, meaningUpdate)
        Dim expectedResult As WordEntry = New GroupDaoTests.MockWordEntry(1, "word1", "pre", "post", WordType.Verb, "new meaning", "l", False)
        newEntryWord.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub ChangeEntry_ChangeAll_Works()
        Dim nameUpdate As New IDictionaryDao.UpdateData With {
            .Word = "new word",
            .Pre = "new pre",
            .Post = "new post",
            .WordType = WordType.Adverb,
            .Meaning = "new meaning",
            .AdditionalTargetLangInfo = "new info",
            .Irregular = True
        }
        Dim newEntryWord As WordEntry = _dictionaryDao.ChangeEntry(word1entry1, nameUpdate)
        Dim expectedResult As WordEntry = New GroupDaoTests.MockWordEntry(1, "new word", "new pre", "new post", WordType.Adverb, "new meaning", "new info", True)
        newEntryWord.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub ChangeEntry_NewMainEntry_IsUpdated()
        Dim oldMainEntry = _dictionaryDao.GetMainEntry(word1entry1)
        oldMainEntry.Should.BeEquivalentTo(word1MainEntry)

        _dictionaryDao.ChangeEntry(word1entry1, lang2MainEntry)

        Dim newMainEntry = _dictionaryDao.GetMainEntry(word1entry1)
        newMainEntry.Should.BeEquivalentTo(lang2MainEntry)
    End Sub

    <Test>
    Public Sub ChangeEntry_NonExistingMainEntry_Fails()
        Assert.Throws(Of EntryNotFoundException)(Sub() _dictionaryDao.ChangeEntry(word1entry1, nonExistingMainEntry))
    End Sub

    <Test>
    Public Sub GetMainEntry_ExistingWordEntryReturns_MainEntry()
        Dim mainEntry = _dictionaryDao.GetMainEntry(word1entry1)

        Dim expectedResult = New MockMainEntry(1, "word1", language, targetLanguage)

        Assert.AreEqual(mainEntry, expectedResult)
    End Sub

    <Test>
    Public Sub GetMainEntry_NoMainEntryForWord_Fails()
        Dim nonExisting = New GroupDaoTests.MockWordEntry(5, "", "", "", WordType.Verb, "", "", False)
        Assert.Throws(Of EntryNotFoundException)(Sub() _dictionaryDao.GetMainEntry(nonExisting))
    End Sub

    <Test>
    Public Sub GetMainEntry_WithExistingWord_Returns()
        Dim mainEntry = _dictionaryDao.GetMainEntry("word1", language, targetLanguage)

        Dim expectedResult = New MockMainEntry(1, "word1", language, targetLanguage)

        mainEntry.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub GetMainEntry_WordNotExists_Fails()
        Assert.Throws(Of EntryNotFoundException)(Sub() _dictionaryDao.GetMainEntry("non existing", language, targetLanguage))
    End Sub

    <Test>
    Public Sub GetMainEntries_ForLanguage_ReturnsAllWords()
        Dim words As ICollection(Of MainEntry) = _dictionaryDao.GetMainEntries(language, targetLanguage)

        Dim expectedResult = New List(Of MainEntry) From {New MockMainEntry(1, "word1", "lang", "targetLang"), New MockMainEntry(3, "wordx", "lang", "targetLang")}

        words.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub GetMainEntries_ForLanguageWithStart_ReturnsCorrectWords()
        Dim words As ICollection(Of MainEntry) = _dictionaryDao.GetMainEntries("lang2", targetLanguage, "s")

        Dim expectedResult = New List(Of MainEntry) From {lang2MainEntry}

        words.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub GetMainEntries_ForLanguageWithStartNotFound_ReturnsEmpty()
        Dim words As ICollection(Of MainEntry) = _dictionaryDao.GetMainEntries("lang2", targetLanguage, "t")

        Dim expectedResult = New List(Of MainEntry)

        words.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub ChangeEntry_NonConflictingWord_Successful()
        Dim oldMainEntry = New MockMainEntry(1, "word1", "lang", "targetLang")
        Dim modifiedEntry = _dictionaryDao.ChangeMainEntry(oldMainEntry, "word new")
        Dim expectedEntry = New MockMainEntry(1, "word new", "lang", "targetLang")

        modifiedEntry.Should.BeEquivalentTo(expectedEntry)
    End Sub

    <Test>
    Public Sub ChangeEntry_NonConflictingWord_ChangesDatabase()
        Dim updatedLang2Entry = _dictionaryDao.ChangeMainEntry(lang2MainEntry, "word new")

        Dim mainEntriesAfterUpdate = _dictionaryDao.GetMainEntries("lang2", "targetLang")

        Dim expectedUpdatedLang2Entry = New MockMainEntry(2, "word new", "lang2", "targetLang")

        updatedLang2Entry.Should.BeEquivalentTo(updatedLang2Entry)
        mainEntriesAfterUpdate.Should.BeEquivalentTo(New List(Of MainEntry) From {expectedUpdatedLang2Entry, lang2AnotherMainEntry})
    End Sub

    <Test>
    Public Sub ChangeEntry_NotExisting_Fails()
        Assert.Throws(Of EntryNotFoundException)(Sub() _dictionaryDao.ChangeMainEntry(nonExistingMainEntry, ""))
    End Sub

    <Test>
    Public Sub ChangeEntry_NewWordExisting_Fails()
        Dim oldMainEntry = New MockMainEntry(1, "word1", "lang", "targetLang")
        Assert.Throws(Of EntryExistsException)(Sub() _dictionaryDao.ChangeMainEntry(oldMainEntry, "wordx"))
    End Sub

    <Test>
    Public Sub AdaptSubEntries_ForMainEntry_ModifiesSubEntries()
        Dim newMainEntry = New MockMainEntry(1, "word1 update", "lang", "targetLang")
        _dictionaryDao.AdaptSubEntries(newMainEntry, "word1")

        Dim updatedWords = _dictionaryDao.GetWords(language, targetLanguage, "w")

        Dim word1entry1Updated = New GroupDaoTests.MockWordEntry(1, "word1 update", "pre", "post", WordType.Verb, "m", "l", False)
        Dim word1entry2Updated = New GroupDaoTests.MockWordEntry(3, "word1 update", "", "", WordType.Verb, "with a meaning", "", True)

        Dim expectedResults As New List(Of WordEntry) From {word1entry1Updated, word1Variance, word1entry2Updated, wordWithoutSubwords, word1entry1a}

        updatedWords.Should.BeEquivalentTo(expectedResults)
    End Sub

    <Test>
    Public Sub WordCount_ReturnsCount_ForLanguagePair()
        Dim count = _dictionaryDao.WordCount("lang", "targetLang")
        count.Should.Be(2)
    End Sub

    <Test>
    Public Sub WordCount_ReturnsZero_ForNonExistingLanguages()
        Dim count = _dictionaryDao.WordCount("some", "thing")
        count.Should.Be(0)
    End Sub

    <Test>
    Public Sub WordCount_ReturnsCount_WithFirstCharacter()
        Dim count = _dictionaryDao.WordCount("lang", "targetLang", "w")
        count.Should.Be(2)
    End Sub

    <Test>
    Public Sub WordCountTotal_ReturnsTotalCount_ForLanguagePair()
        Dim count = _dictionaryDao.WordCountTotal("lang", "targetLang")
        count.Should.Be(5)
    End Sub

    <Test>
    Public Sub FindSimilar_Finds_Similar()
        Dim similarWord = _dictionaryDao.FindSimilar("wo", "lang", "targetLang")

        similarWord.Should.Be("word1")
    End Sub

    <Test>
    Public Sub FindSimilar_Finds_NonSimiloar()
        Dim similarWord = _dictionaryDao.FindSimilar("qr", "lang", "targetLang")

        similarWord.Should.Be("")
    End Sub

    <Test>
    Public Sub MainLanguages_Returns_MainLanguages()
        Dim mainLanguages = _dictionaryDao.DictionaryMainLanguages()

        Dim expectedLanguages = New List(Of String) From {"targetLang"}

        mainLanguages.Should.BeEquivalentTo(expectedLanguages)
    End Sub

    <Test>
    Public Sub DictionaryLanguages_Returns_AllLanguages()
        Dim languages = _dictionaryDao.DictionaryLanguages("targetLang")

        Dim expectedLanguages = New List(Of String) From {"lang", "lang2"}

        languages.Should.BeEquivalentTo(expectedLanguages)
    End Sub
End Class
