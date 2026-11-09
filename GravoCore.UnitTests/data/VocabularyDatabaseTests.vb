Imports System.IO
Imports AwesomeAssertions
Imports Gravo
Imports Microsoft.Data.Sqlite
Imports NUnit.Framework

''' <summary>
''' Tests for <see cref="VocabularyDatabase"/>. Tests run on a copy of <c>test-data.s3db</c>.
''' </summary>
''' <remarks>
''' The database contains the following group:
''' - Test/Example = GroupTest-Example01 (words 1 marked, 2 marked, 29)
'''
''' Dictionary data:
''' - main entry word1/lang (words 1 = word1/m, 2, 3)
''' - wordx/lang (word 29)
''' </remarks>
<TestFixture>
Public Class VocabularyDatabaseTests
    Private ReadOnly ResourceFile As String = "test-data.s3db"
    Private ReadOnly language As String = "lang"
    Private ReadOnly mainLanguage As String = "targetLang"

    Private _tempDb As String
    Private _db As IDataBaseOperation
    Private fixture As VocabularyDatabase
    Private _group As GroupEntry

    <SetUp>
    Public Sub Setup()
        _tempDb = Path.GetTempFileName()
        File.Copy(DaoUtils.GetSqliteResource(ResourceFile), _tempDb, True)
        _db = New SQLiteDataBaseOperation()
        _db.Open(_tempDb)
        _db.ExecuteNonQuery("
            INSERT INTO Groups (GroupName, GroupSubName, GroupTable)
            VALUES ('Test', 'Example', 'GroupTest-Example01')
        ", Array.Empty(Of Object))

        fixture = VocabularyDatabase.Create(_db)
        _group = fixture.Groups.GetGroup("Test", "Example")
    End Sub

    <TearDown>
    Public Sub CleanUp()
        _db.Close()

        File.Delete(_tempDb)
    End Sub

    <Test>
    Public Sub ChangeWord_EditMarkedAndMove_ChangesAll()
        Dim word1 As TestWord = fixture.Group.Load(_group).GetWord(1)
        Dim update As New IDictionaryDao.UpdateData With {.Word = "word1 changed"}

        Dim changed As WordEntry = fixture.ChangeWord(word1.WordEntry, update, "wordx", language, mainLanguage, _group,
            Not word1.Marked)

        changed.Word.Should.Be("word1 changed")
        fixture.Dictionary.GetMainEntry(changed).Word.Should.Be("wordx")
        fixture.Group.Load(_group).GetWord(1).Marked.Should.Be(Not word1.Marked)
    End Sub

    <Test>
    Public Sub ChangeWord_MoveConflicts_ThrowsAndChangesNothing()
        Dim word1 As TestWord = fixture.Group.Load(_group).GetWord(1)
        Dim conflicting As New WordEntry("word1 changed", "", "", WordType.Verb, "m", "", False)
        fixture.Dictionary.AddSubEntry(conflicting, "wordx", language, mainLanguage)
        Dim update As New IDictionaryDao.UpdateData With {.Word = "word1 changed"}

        Assert.Throws(Of EntryExistsException)(
            Sub() fixture.ChangeWord(word1.WordEntry, update, "wordx", language, mainLanguage, _group,
                Not word1.Marked))

        Dim stored As TestWord = fixture.Group.Load(_group).GetWord(1)
        stored.Word.Should.Be("word1")
        stored.Marked.Should.Be(word1.Marked)
        fixture.Dictionary.GetMainEntry(stored.WordEntry).Word.Should.Be("word1")
    End Sub
End Class
