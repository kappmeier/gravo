Imports Microsoft.Data.Sqlite
Imports System.IO
Imports Gravo
Imports NUnit.Framework
Imports FluentAssertions

''' <summary>
''' Tests for the group part of <see cref="DatabaseTransfer"/>. Tests run on a copy of
''' <c>test-data-dictionary.s3db</c>. Target is a freshly created vocabulary database.
''' </summary>
''' <remarks>
''' The database contains the following groups:
''' - main entries Test/Example = GroupTest-Example01 (words 1, 2, 29)
''' - Test/Multi = GroupTest-MultipleLanguages01 (words 1, 4 - two languages)
''' - Other/Some = GroupTest-Example02 (word 3, seeded as well)
'''
''' Dictionary data :
''' - main entries word1/lang (words 1, 2, 3)
''' - some word/lang2 (word 4)
''' - wordx/lang (word 29)
''' - another/lang2 (no word)
''' </remarks>
<TestFixture>
Public Class DatabaseTransferGroupTests
    Private ReadOnly ResourceFile As String = "test-data.s3db"
    Private ReadOnly mainLanguage As String = "targetLang"

    Private _sourcePath As String
    Private _targetPath As String
    Private _sourceDb As IDataBaseOperation
    Private _targetDb As IDataBaseOperation
    Private _source As VocabularyDatabase
    Private _target As VocabularyDatabase
    Private _transfer As DatabaseTransfer

    ''' <summary>
    ''' Sets up the source and target databases and inserts some groups into the source database.
    ''' </summary>
    <SetUp>
    Public Sub Setup()
        _sourcePath = Path.GetTempFileName()
        File.Copy(DaoUtils.GetSqliteResource(ResourceFile), _sourcePath, True)
        _sourceDb = New SQLiteDataBaseOperation()
        _sourceDb.Open(_sourcePath)
        _sourceDb.ExecuteNonQuery("
            INSERT INTO Groups (GroupName, GroupSubName, GroupTable)
            VALUES
                ('Test', 'Example', 'GroupTest-Example01'),
                ('Test', 'Multi', 'GroupTest-MultipleLanguages01'),
                ('Other', 'Some', 'GroupTest-Example02')
        ", Array.Empty(Of Object))
        _sourceDb.ExecuteNonQuery("
            INSERT INTO [GroupTest-Example02]
                (WordIndex, Marked, Example, TestInterval, Counter, LastDate, TestIntervalMain, CounterMain)
            VALUES (3, 0, '', 1, 1, '1900-01-01', 1, 1)
        ", Array.Empty(Of Object))

        _targetPath = Path.GetTempFileName()
        ManagementDao.CreateNewVocabularyDatabase(_targetPath)
        _targetDb = New SQLiteDataBaseOperation()
        _targetDb.Open(_targetPath)

        _source = New VocabularyDatabase(_sourceDb)
        _target = New VocabularyDatabase(_targetDb)
        _transfer = New DatabaseTransfer(_source, _target)
    End Sub

    <TearDown>
    Public Sub CleanUp()
        _sourceDb.Close()
        _targetDb.Close()
        SqliteConnection.ClearAllPools()

        File.Delete(_sourcePath)
        File.Delete(_targetPath)
    End Sub

    <Test>
    Public Sub CopyGroup_NewGroup_CreatesSubGroupsWordsAndMains()
        Dim result As TransferResult = _transfer.CopyGroup("Test")

        result.Groups.Should().Be(1)
        result.SubGroups.Should().Be(2)
        result.GroupEntries.Should().Be(5)
        result.MainEntries.Should().Be(3)
        result.SubEntries.Should().Be(4)
        _target.Groups.GetSubGroups("Test").Select(Function(g) g.SubGroup).Should().Equal("Example", "Multi")
        _target.Group.Load(TargetGroup("Test", "Example")).WordCount.Should().Be(3)
        _target.Group.Load(TargetGroup("Test", "Multi")).WordCount.Should().Be(2)
        _target.Dictionary.DictionaryLanguages(mainLanguage).Should().Equal("lang", "lang2")
    End Sub

    <Test>
    Public Sub CopyGroup_TargetIndexesDiffer_RemapsWordIndexes()
        _target.Dictionary.AddEntry("filler", "lang", mainLanguage)
        Dim filler As New WordEntry("filler", "", "", WordType.Verb, "f", "", False)
        _target.Dictionary.AddSubEntry(filler, "filler", "lang", mainLanguage)

        _transfer.CopyGroup("Test")

        Dim example As GroupDto = _target.Group.Load(TargetGroup("Test", "Example"))
        example.Indices.Should().Equal(2, 3, 4)
        _target.Group.Load(TargetGroup("Test", "Multi")).Indices.Should().Equal(2, 5)
        ' WordEntry.Equals ignores the index.
        example.Entries.Select(Function(t) t.WordEntry).Should().Equal(
            _source.Group.Load(SourceGroup("Test", "Example")).Entries.Select(Function(t) t.WordEntry))
        Dim wordxMain As MainEntry = _target.Dictionary.GetMainEntry("wordx", "lang", mainLanguage)
        _target.Dictionary.GetEntry(wordxMain, "wordx", "").Index.Should().Be(
            example.Entries.Single(Function(t) t.Word = "wordx").WordIndex)
    End Sub

    <Test>
    Public Sub CopyGroup_PreservesMarkedAndExample()
        _transfer.CopyGroup("Test")

        Dim example As GroupDto = _target.Group.Load(TargetGroup("Test", "Example"))
        Dim word2 As TestWord = example.Entries.Single(Function(t) t.Word = "word2")
        word2.Marked.Should().BeTrue()
        word2.Example.Should().Be("An example.")
        Dim wordx As TestWord = example.Entries.Single(Function(t) t.Word = "wordx")
        wordx.Marked.Should().BeFalse()
        wordx.Example.Should().Be("")
        ' TestWord.Equals compares the attributes, Marked and Example, not the index.
        _target.Group.Load(TargetGroup("Test", "Multi")).Entries.Should().Equal(
            _source.Group.Load(SourceGroup("Test", "Multi")).Entries)
    End Sub

    <Test>
    Public Sub CopyGroup_SecondCall_AddsNothing()
        _transfer.CopyGroup("Test")

        Dim result As TransferResult = _transfer.CopyGroup("Test")

        result.Groups.Should().Be(1)
        result.SubGroups.Should().Be(0)
        result.GroupEntries.Should().Be(0)
        result.MainEntries.Should().Be(0)
        result.SubEntries.Should().Be(0)
        _target.Groups.GetSubGroups("Test").Should().HaveCount(2)
        _target.Group.Load(TargetGroup("Test", "Example")).WordCount.Should().Be(3)
        _target.Dictionary.WordCountTotal("lang", mainLanguage).Should().Be(3)
    End Sub

    <Test>
    Public Sub CopyGroup_IntoExistingSubGroup_MergesAndKeepsExistingRow()
        _target.Dictionary.AddEntry("word1", "lang", mainLanguage)
        Dim word1 As New WordEntry("word1", "pre", "post", WordType.Verb, "m", "l", False)
        _target.Dictionary.AddSubEntry(word1, "word1", "lang", mainLanguage)
        _target.Groups.AddGroup("Test", "Example")
        Dim existingGroup As GroupEntry = TargetGroup("Test", "Example")
        Dim marked As Boolean = False
        Dim example As String = "kept"
        _target.Group.Add(existingGroup, word1, marked, example)
        _targetDb.ExecuteNonQuery("
            UPDATE [" & existingGroup.Table & "]
            SET TestInterval = 7
            WHERE WordIndex = ?
        ", Enumerable.Repeat(CObj(word1.Index), 1))

        Dim result As TransferResult = _transfer.CopyGroup("Test")

        result.SubGroups.Should().Be(1)
        result.GroupEntries.Should().Be(4)
        result.MainEntries.Should().Be(2)
        result.SubEntries.Should().Be(3)
        Dim loaded As GroupDto = _target.Group.Load(existingGroup)
        loaded.WordCount.Should().Be(3)
        ' The source row is marked with an empty example; the target row is kept as it was.
        Dim kept As TestWord = loaded.GetWord(word1.Index)
        kept.Marked.Should().BeFalse()
        kept.Example.Should().Be("kept")
        ReadInt(_targetDb, "
            SELECT TestInterval
            FROM [" & existingGroup.Table & "]
            WHERE WordIndex = ?
        ", word1.Index).Should().Be(7)
    End Sub

    <Test>
    Public Sub CopyGroup_CardStatistics_AreNotCopied()
        _transfer.CopyGroup("Test")

        Dim example As GroupEntry = TargetGroup("Test", "Example")
        Dim word1Index As Integer = _target.Group.Load(example).Entries.Single(Function(t) t.Word = "word1").WordIndex
        ReadInt(_targetDb, "
            SELECT TestInterval
            FROM [" & example.Table & "]
            WHERE WordIndex = ?
        ", word1Index).Should().Be(1)
        ReadInt(_targetDb, "
            SELECT Counter
            FROM [" & example.Table & "]
            WHERE WordIndex = ?
        ", word1Index).Should().Be(1)
        Dim card As Card = New CardsDao(_targetDb).Load(word1Index)
        card.TestInterval.Should().Be(1)
        card.Counter.Should().Be(1)
    End Sub

    <Test>
    Public Sub CopyGroup_UnknownGroup_AddsNothing()
        Dim result As TransferResult = _transfer.CopyGroup("Nope")

        result.Groups.Should().Be(0)
        result.SubGroups.Should().Be(0)
        result.GroupEntries.Should().Be(0)
        result.MainEntries.Should().Be(0)
        result.SubEntries.Should().Be(0)
        _target.Groups.GetGroups().Should().BeEmpty()
    End Sub

    <Test>
    Public Sub CopyAllGroups_CopiesEveryMainGroup()
        Dim result As TransferResult = _transfer.CopyAllGroups()

        result.Groups.Should().Be(2)
        result.SubGroups.Should().Be(3)
        result.GroupEntries.Should().Be(6)
        result.MainEntries.Should().Be(3)
        result.SubEntries.Should().Be(5)
        _target.Groups.GetGroups().OrderBy(Function(g) g).Should().Equal("Other", "Test")
        _target.Group.Load(TargetGroup("Other", "Some")).Entries.Select(Function(t) t.Word).Should().Equal("some word")
    End Sub

    <Test>
    Public Sub CopyGroup_EmptySourceSubGroup_CreatesEmptyTargetSubGroup()
        _source.Groups.AddGroup("Test", "Empty")

        Dim result As TransferResult = _transfer.CopyGroup("Test")

        result.SubGroups.Should().Be(3)
        result.GroupEntries.Should().Be(5)
        _target.Group.Load(TargetGroup("Test", "Empty")).WordCount.Should().Be(0)
    End Sub

    <Test>
    Public Sub CopyGroup_ApostropheSubGroupAndExample_ArriveUnchanged()
        _sourceDb.ExecuteNonQuery("UPDATE Groups SET GroupSubName = 'Lezione 8 - Sapori d''Italia' WHERE GroupSubName = 'Example'", Array.Empty(Of Object))
        _sourceDb.ExecuteNonQuery("UPDATE [GroupTest-Example01] SET Example = 'Auf geht''s!' WHERE WordIndex = 2", Array.Empty(Of Object))

        _transfer.CopyGroup("Test")

        Dim copied As GroupDto = _target.Group.Load(TargetGroup("Test", "Lezione 8 - Sapori d'Italia"))
        copied.Entries.Single(Function(t) t.Word = "word2").Example.Should().Be("Auf geht's!")
    End Sub

    Private Function TargetGroup(groupName As String, subGroupName As String) As GroupEntry
        Return _target.Groups.GetGroup(groupName, subGroupName)
    End Function

    Private Function SourceGroup(groupName As String, subGroupName As String) As GroupEntry
        Return _source.Groups.GetGroup(groupName, subGroupName)
    End Function

    Private Shared Function ReadInt(db As IDataBaseOperation, sql As String, parameter As Integer) As Integer
        db.ExecuteReader(sql, Enumerable.Repeat(CObj(parameter), 1))
        db.DBCursor.Read()
        ReadInt = db.SecureGetInt32(0)
        db.DBCursor.Close()
    End Function
End Class
