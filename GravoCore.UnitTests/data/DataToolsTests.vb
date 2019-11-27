Imports NUnit.Framework
Imports Moq
Imports Gravo
Imports System.Collections.ObjectModel
Imports FluentAssertions
Imports GravoCore.UnitTests.DictionaryDaoTests

Public Class DataToolsTests
    ReadOnly groupName = "groupName"
    ReadOnly group1 = New GroupEntry(0, groupName, "sub1", "sub1table")
    ReadOnly group2 = New GroupEntry(0, groupName, "sub2", "sub2table")
    ReadOnly group3 = New GroupEntry(0, groupName, "sub3", "sub3table")
    ReadOnly subGroups As ICollection(Of GroupEntry) = New List(Of GroupEntry) From {group1, group2, group3}

    <Test>
    Public Sub WordCounts_Counts_SubGroups()
        Dim groupsDaoMock As New Mock(Of IGroupsDao)(MockBehavior.Strict)
        Dim groupDaoMock As New Mock(Of IGroupDao)(MockBehavior.Strict)

        groupsDaoMock.Setup(Function(x) x.GetSubGroups(groupName)).Returns(subGroups)

        Dim list1 As ICollection(Of TestWord) = Enumerable.Repeat(CreateFakeTestWord, 5).ToList
        Dim group1Dto As New Mock(Of GroupDto)(MockBehavior.Strict, group1, list1)
        Dim emptyList As ICollection(Of TestWord) = New List(Of TestWord) From {}
        Dim group2Dto As New Mock(Of GroupDto)(MockBehavior.Strict, group1, emptyList)
        Dim list3 As ICollection(Of TestWord) = Enumerable.Repeat(CreateFakeTestWord, 10).ToList
        Dim group3Dto As New Mock(Of GroupDto)(MockBehavior.Strict, group1, list3)

        groupDaoMock.Setup(Function(x) x.Load(group1)).Returns(group1Dto.Object)
        groupDaoMock.Setup(Function(x) x.Load(group2)).Returns(group2Dto.Object)
        groupDaoMock.Setup(Function(x) x.Load(group3)).Returns(group3Dto.Object)

        Dim result As Integer = DataTools.WordCount(groupsDaoMock.Object, groupDaoMock.Object, groupName)

        Assert.AreEqual(15, result)
    End Sub

    Private Function CreateFakeTestWord() As TestWord
        Return New TestWord(New WordEntry("", "", "", WordType.Verb, "", "", False), True, "")
    End Function

    <Test>
    Public Sub UsedLanguage_Counts_AllUsedLanguages()
        Dim groupsDaoMock As New Mock(Of IGroupsDao)(MockBehavior.Strict)
        Dim groupDaoMock As New Mock(Of IGroupDao)(MockBehavior.Strict)

        groupsDaoMock.Setup(Function(x) x.GetSubGroups(groupName)).Returns(subGroups)

        groupDaoMock.Setup(Function(x) x.GetLanguages(group1)).Returns(New List(Of String) From {"lang1", "lang2"})
        groupDaoMock.Setup(Function(x) x.GetLanguages(group2)).Returns(New List(Of String) From {})
        groupDaoMock.Setup(Function(x) x.GetLanguages(group3)).Returns(New List(Of String) From {"lang2", "lang3"})

        Dim result As Integer = DataTools.UsedLanguagesCount(groupsDaoMock.Object, groupDaoMock.Object, groupName)

        result.Should.Be(3)
    End Sub

    <Test>
    Public Sub GetOrCreate_Existing_Returns()
        Dim dictionaryDaoMock As New Mock(Of IDictionaryDao)(MockBehavior.Strict)

        Dim existingEntry As MainEntry = New MockMainEntry(1, "word", "language", "target")

        dictionaryDaoMock.Setup(Function(x) x.GetMainEntry("word", "language", "target")).Returns(existingEntry)

        Dim result As MainEntry = DataTools.GetOrCreateMainEntry(dictionaryDaoMock.Object, "word", "language", "target")

        result.Should.BeSameAs(existingEntry)
    End Sub

    <Test>
    Public Sub GetOrCreate_NonExisting_CreatesAndReturns()
        Dim dictionaryDaoMock As New Mock(Of IDictionaryDao)(MockBehavior.Strict)

        Dim newEntry As MainEntry = New MockMainEntry(1, "word", "language", "target")

        dictionaryDaoMock.Setup(Function(x) x.GetMainEntry("word", "language", "target")).Throws(New EntryNotFoundException())
        dictionaryDaoMock.Setup(Function(x) x.AddEntry("word", "language", "target")).Returns(newEntry)

        Dim result As MainEntry = DataTools.GetOrCreateMainEntry(dictionaryDaoMock.Object, "word", "language", "target")

        result.Should.BeSameAs(newEntry)

    End Sub
End Class
