Imports System.Collections.ObjectModel
Imports System.Data.SQLite
Imports System.IO
Imports Gravo
Imports NUnit.Framework

''' <summary>
''' Performs tests on a very simple test database.
''' 
''' It contains the following groups initially fore ach test:
''' 
'''  - Language Book
'''    - Unit 1
'''    - Unit 2
'''  - Other Group
'''    - Some Words
'''    
''' </summary>
<TestFixture>
Public Class GroupsDaoTests
    Private ReadOnly ResourceFile = "test-data-simple.s3db"
    Private _groupsDao As GroupsDao
    Private _tempDb As String
    Private _db As IDataBaseOperation

    <SetUp>
    Public Sub Setup()
        _tempDb = Path.GetTempFileName
        File.Copy(DaoUtils.GetSqliteResource("test-data-simple.s3db"), _tempDb, True)

        _db = New SQLiteDataBaseOperation()
        _db.Open(_tempDb)

        _groupsDao = New GroupsDao(_db)
    End Sub

    <TearDown>
    Public Sub CleanUp()
        _db.Close()
        SQLiteConnection.ClearAllPools()

        File.Delete(_tempDb)
    End Sub

    <Test>
    Public Sub GetGroups_OnValidDatabase_ReturnsAllGroupNames()
        Dim result As Collection(Of String) = _groupsDao.GetGroups

        Dim expectedResult = New List(Of String) From {"Language Book", "Other Group"}

        CollectionAssert.AreEqual(expectedResult, result, "list of groups not equal")
    End Sub

    <Test>
    Public Sub GetAllGroups_OnValidDatabase_ReturnsAll()
        Dim result As Collection(Of GroupEntry) = _groupsDao.GetAllGroups

        Dim expectedResult = New List(Of GroupEntry) From {
            New GroupEntry(1, "Language Book", "Unit 1", "GroupLanguageBook01"),
            New GroupEntry(2, "Language Book", "Unit 2", "GroupLanguageBook02"),
            New GroupEntry(3, "Other Group", "Some Words", "GroupOtherGroup01")
        }

        CollectionAssert.AreEqual(expectedResult, result, "list of groups including sub groups not equal")
    End Sub

    <Test>
    Public Sub GetAllSubGroups_OnValidDatabase_ReturnsCorrectGroups()
        Dim result As Collection(Of GroupEntry) = _groupsDao.GetSubGroups("Other Group")

        Dim expectedResult = New List(Of GroupEntry) From {
            New GroupEntry(3, "Other Group", "Some Words", "GroupOtherGroup01")
        }

        CollectionAssert.AreEqual(expectedResult, result, "list of sub groups wrong")
    End Sub

    <Test>
    Public Sub GetAllSubGroups_WithNonExistingGroup_Empty()
        Dim result As Collection(Of GroupEntry) = _groupsDao.GetSubGroups("Non Existing Group")

        CollectionAssert.IsEmpty(result, "result list not empty")
    End Sub

    <Test>
    Public Sub SubGroupCount_OnExistingGroup_ReturnsCount()
        Assert.AreEqual(2, _groupsDao.SubGroupCount("Language Book"))
        Assert.AreEqual(1, _groupsDao.SubGroupCount("Other Group"))
    End Sub

    <Test>
    Public Sub SubGroupCount_OnNonExistingGroup_Throws()
        Assert.Throws(Of EntryNotFoundException)(Sub() _groupsDao.SubGroupCount("Whatever"))
    End Sub

    <Test>
    Public Sub AddGroup_ExistingGroup_Throws()
        Assert.Throws(Of EntryExistsException)(Sub() _groupsDao.AddGroup("Language Book", "Unit 1"))
    End Sub

    <Test>
    Public Sub AddGroup_ToExistingGroup_CreatesTable()
        _groupsDao.AddGroup("Language Book", "Unit 3")

        Dim verification As Collection(Of GroupEntry) = _groupsDao.GetSubGroups("Language Book")

        Dim expectedResult = New List(Of GroupEntry) From {
            New GroupEntry(1, "Language Book", "Unit 1", "GroupOtherGroup01"),
            New GroupEntry(2, "Language Book", "Unit 2", "GroupOtherGroup02"),
            New GroupEntry(4, "Language Book", "Unit 3", "GroupOtherGroup03")
        }

        CollectionAssert.AreEqual(expectedResult, verification, "list of groups does not contain new group")
    End Sub

    <Test>
    Public Sub AddGroup_CompletelyNew_CreatesTable()
        _groupsDao.AddGroup("Another Group", "First Sub")

        Dim verification As Collection(Of GroupEntry) = _groupsDao.GetSubGroups("Another Group")

        Dim expectedResult = New List(Of GroupEntry) From {
            New GroupEntry(5, "Another Group", "First Sub", "GroupAnotherGroup01")
        }

        CollectionAssert.AreEqual(expectedResult, verification, "new table not created")
    End Sub

    <Test>
    Public Sub EditGroup_AlreadyExists_ThrowsException()
        Assert.Throws(Of EntryExistsException)(Sub() _groupsDao.EditGroup("Language Book", "Other Group"))
    End Sub

    <Test>
    Public Sub EditGroup_WithFreeName_RenamesAllTables()
        _groupsDao.EditGroup("Language Book", "Crazy Lecture")

        Dim newGroups As Collection(Of String) = _groupsDao.GetGroups
        Dim expectedNewGroupNames = New List(Of String) From {"Crazy Lecture", "Other Group"}
        CollectionAssert.AreEqual(expectedNewGroupNames, newGroups, "new group not created")

        Dim allNewGroups As Collection(Of GroupEntry) = _groupsDao.GetAllGroups

        Dim newGroupName1 As String = "GroupCrazyLecture01"
        Dim newGroupName2 As String = "GroupCrazyLecture02"

        Dim expectedNewGroups = New List(Of GroupEntry) From {
            New GroupEntry(1, "Crazy Lecture", "Unit 1", newGroupName1),
            New GroupEntry(2, "Crazy Lecture", "Unit 2", newGroupName2),
            New GroupEntry(3, "Other Group", "Some Words", "GroupOtherGroup01")
        }

        Assert.IsTrue(TableExists(newGroupName1))
        Assert.IsTrue(TableExists(newGroupName2))
        Assert.IsFalse(TableExists("GroupLanguageBook01"))
        Assert.IsFalse(TableExists("GroupLanguageBook02"))

        CollectionAssert.AreEqual(expectedNewGroups, allNewGroups, "list of groups including sub groups not equal")

        Assert.IsTrue(_groupsDao.GroupExists("Crazy Lecture"))
        Assert.IsFalse(_groupsDao.GroupExists("Language Book"))
    End Sub

    <Test>
    Public Sub DeleteGroup_WithExistingGroup_DeletesAllData()
        _groupsDao.DeleteGroup("Language Book")

        Dim verification As Collection(Of GroupEntry) = _groupsDao.GetAllGroups()

        Dim expectedResult = New List(Of GroupEntry) From {
            New GroupEntry(1, "Other Group", "Some Words", "GroupOtherGroup01")
        }

        CollectionAssert.AreEqual(expectedResult, verification, "list of groups does contain deleted group")

        Assert.IsFalse(TableExists("GroupLanguageBook01"))
        Assert.IsFalse(TableExists("GroupLanguageBook02"))
        Assert.IsTrue(TableExists("GroupOtherGroup01"))
    End Sub

    <Test>
    Public Sub DeleteGroup_NonExisting_ThrowsException()
        Assert.Throws(Of EntryNotFoundException)(Sub() _groupsDao.DeleteGroup("Whatever"))
    End Sub

    <Test>
    Public Sub DeleteGroup_WithExistingSubGroup_DeletesAllData()
        _groupsDao.DeleteGroup("Language Book", "Unit 1")

        Dim verification As Collection(Of GroupEntry) = _groupsDao.GetAllGroups()

        Dim expectedResult = New List(Of GroupEntry) From {
            New GroupEntry(1, "Language Book", "Unit 2", "GroupOtherGroup01"),
            New GroupEntry(2, "Other Group", "Some Words", "GroupOtherGroup01")
        }

        CollectionAssert.AreEqual(expectedResult, verification, "list of groups does contain deleted group")

        Assert.IsTrue(TableExists("GroupLanguageBook01"))
        Assert.IsFalse(TableExists("GroupLanguageBook02"))
        Assert.IsTrue(TableExists("GroupOtherGroup01"))
    End Sub


    <Test>
    Public Sub DeleteGroup_LastSubGroup_DeletesAllData()
        _groupsDao.DeleteGroup("Language Book", "Unit 2")

        Dim verification As Collection(Of GroupEntry) = _groupsDao.GetAllGroups()

        Dim expectedResult = New List(Of GroupEntry) From {
            New GroupEntry(1, "Language Book", "Unit 1", "GroupOtherGroup01"),
            New GroupEntry(2, "Other Group", "Some Words", "GroupOtherGroup01")
        }

        CollectionAssert.AreEqual(expectedResult, verification, "list of groups does contain deleted group")

        Assert.IsTrue(TableExists("GroupLanguageBook01"))
        Assert.IsFalse(TableExists("GroupLanguageBook02"))
        Assert.IsTrue(TableExists("GroupOtherGroup01"))
    End Sub

    <Test>
    Public Sub DeleteGroup_SubGroupNonExisting_ThrowsException()
        Assert.Throws(Of EntryNotFoundException)(Sub() _groupsDao.DeleteGroup("Language Book", "Unit 3"))
    End Sub

    <Test>
    Public Sub EditSubGroup_NonExisting_Throws()
        Assert.Throws(Of EntryNotFoundException)(Sub() _groupsDao.EditSubGroup("Language Book", "Unit 3", "failure"))
    End Sub

    <Test>
    Public Sub EditSubGroup_AlreadyExisting_Throws()
        Assert.Throws(Of EntryExistsException)(Sub() _groupsDao.EditSubGroup("Language Book", "Unit 1", "Unit 2"))
    End Sub

    <Test>
    Public Sub EditSubGroup_NewName_IsRenamed()
        _groupsDao.EditSubGroup("Language Book", "Unit 2", "New Name")

        Dim result As Collection(Of GroupEntry) = _groupsDao.GetSubGroups("Language Book")

        Dim expectedResult = New List(Of GroupEntry) From {
            New GroupEntry(1, "Language Book", "Unit 1", "GroupOtherGroup01"),
            New GroupEntry(2, "Language Book", "New Name", "GroupOtherGroup02")
        }

        CollectionAssert.AreEquivalent(expectedResult, result, "new subgroups incorrect")
    End Sub

    <Test>
    Public Sub SwapGroups_Existing_AreSwapped()
        _groupsDao.SwapGroups("Language Book", "Unit 1", "Unit 2")

        Dim result As Collection(Of GroupEntry) = _groupsDao.GetAllGroups()

        Dim expectedResult = New List(Of GroupEntry) From {
            New GroupEntry(1, "Language Book", "Unit 2", "GroupLanguageBook01"),
            New GroupEntry(2, "Language Book", "Unit 1", "GroupLanguageBook02"),
            New GroupEntry(3, "Other Group", "Some Words", "GroupOtherGroup01")
        }

        CollectionAssert.AreEqual(expectedResult, result, "groups not swapped")

        Assert.True(TableExists("GroupLanguageBook01"))
        Assert.True(TableExists("GroupLanguageBook02"))
        Assert.True(TableExists("GroupOtherGroup01"))
    End Sub

    Private Function TableExists(tableName As String) As Boolean
        'Dim sinsertSQL = New SQLiteCommand("INSERT INTO Book (Id, Title, Language, PublicationDate, Publisher, Edition, OfficialUrl, Description, EBookFormat) VALUES (?,?,?,?,?,?,?,?)", _db)
        Dim command As String = "SELECT name FROM sqlite_master WHERE type='table' AND name=?"
        _db.ExecuteReader(command, Enumerable.Repeat(tableName, 1))

        If _db.DBCursor.HasRows Then
            TableExists = True
        Else
            TableExists = False
        End If
        _db.CloseReader()
    End Function
End Class