Imports Gravo
Imports NUnit.Framework
Imports FluentAssertions
Imports GravoCore.UnitTests.GroupDaoTests

<TestFixture>
Public Class GroupDtoTests
    Private ReadOnly ResourceFile = "test-data-groups.s3db"
    Private _groupDao As GroupDao
    Private _tempDb As String
    Private _db As IDataBaseOperation

    Private ReadOnly testWord1 = New TestWord(New MockWordEntry(1, "word1", "pre", "post", WordType.Verb, "m", "l", False), True, "")
    Private ReadOnly testWord2 = New TestWord(New MockWordEntry(2, "word2", "", "", WordType.Verb, "", "", False), True, "An example.")
    Private ReadOnly testWord29 = New TestWord(New MockWordEntry(29, "wordx", "", "", WordType.Adjective, "", "", True), False, "")

    Private ReadOnly word3 = New WordEntry("some word", "", "", WordType.Verb, "with a meaning", "", True)
    Private ReadOnly word4 = New WordEntry("word4", "", "", WordType.Adjective, "test", "info", False)

    ReadOnly exampleGroup = New GroupEntry(123, "Test-Example", "Subgroup1", "GroupTest-Example01")
    ReadOnly emptyGroup = New GroupEntry(123, "Test-Example", "Empty subgroup", "GroupTest-Example02")

    Private ReadOnly defaultWords = New List(Of TestWord) From {testWord1, testWord2, testWord29}

    <Test>
    Public Sub GetMarked_Existing_ReturnsCorrectValue()
        Dim group As GroupDto = New GroupDto(exampleGroup, defaultWords)

        Assert.IsTrue(GroupDto.IsMarked(group, 1))
        Assert.IsTrue(GroupDto.IsMarked(group, 2))
        Assert.False(GroupDto.IsMarked(group, 29))
    End Sub

    <Test>
    Public Sub GetMarked_NonExisting_ThrowsException()
        Dim group As GroupDto = New GroupDto(exampleGroup, New List(Of TestWord)())
        Assert.Throws(Of EntryNotFoundException)(Function() GroupDto.IsMarked(group, 3))
    End Sub

    <Test>
    Public Sub Filter_Words_Filters()
        Dim group As GroupDto = New GroupDto(exampleGroup, defaultWords)
        Dim filtered = group.FilterWords("word2")

        Dim expectedResult = New List(Of TestWord) From {testWord2}

        filtered.Should.BeEquivalentTo(expectedResult)
    End Sub

    <Test>
    Public Sub Filter_NonExistingWord_ReturnsEmpty()
        Dim group As GroupDto = New GroupDto(exampleGroup, defaultWords)

        Dim filtered = group.FilterWords("non-existing")

        filtered.Should.BeEmpty()
    End Sub
End Class
