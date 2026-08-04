Imports Gravo
Imports NUnit.Framework
Imports FluentAssertions

''' <summary>
''' Tests the GravoCore/data/dto/TestWord.vb class. Simple delegated wrapper of WordEntry.
''' </summary>
<TestFixture>
Public Class TestWordTests
    Private ReadOnly wordEntry As WordEntry = New WordEntry("word", "pre", "post", WordType.Verb, "meaning", "info", True)

    <Test>
    Public Sub Constructor_DelegatesFieldsToWrappedWordEntry()
        Dim testWord As New TestWord(wordEntry, True, "example")

        testWord.Word.Should.Be("word")
        testWord.Pre.Should.Be("pre")
        testWord.Post.Should.Be("post")
        testWord.Meaning.Should.Be("meaning")
        testWord.AdditionalTargetLangInfo.Should.Be("info")
        testWord.Irregular.Should.Be(True)
        testWord.Index.Should.Be(wordEntry.Index)
        testWord.WordIndex.Should.Be(wordEntry.WordIndex)
        testWord.WordEntry.Should.Be(wordEntry)
    End Sub

    <Test>
    Public Sub Constructor_SetsMarkedAndExampleAsOwnFields()
        Dim testWord As New TestWord(wordEntry, True, "example")

        testWord.Marked.Should.Be(True)
        testWord.Example.Should.Be("example")
    End Sub

    <Test>
    Public Sub Constructor_WithNothingWordEntry_SucceedsButDelegatingGetterThrows()
        Dim testWord As New TestWord(Nothing, True, "example")

        Assert.Throws(Of NullReferenceException)(Sub()
                                                       Dim word As String = testWord.Word
                                                   End Sub)
    End Sub

    <Test>
    Public Sub Equals_SameValues_ReturnsTrue()
        Dim testWordA As New TestWord(wordEntry, True, "example")
        Dim testWordB As New TestWord(wordEntry, True, "example")

        testWordA.Equals(testWordB).Should.Be(True)
    End Sub

    <Test>
    Public Sub Equals_Nothing_ReturnsFalse()
        Dim testWord As New TestWord(wordEntry, True, "example")

        testWord.Equals(Nothing).Should.Be(False)
    End Sub

    <Test>
    Public Sub Equals_OtherType_ReturnsFalse()
        Dim testWord As New TestWord(wordEntry, True, "example")

        testWord.Equals("word").Should.Be(False)
    End Sub

    <Test>
    Public Sub Equals_DifferingWordEntry_ReturnsFalse()
        Dim otherEntry As New WordEntry("otherWord", "pre", "post", WordType.Verb, "meaning", "info", True)
        Dim testWordA As New TestWord(wordEntry, True, "example")
        Dim testWordB As New TestWord(otherEntry, True, "example")

        testWordA.Equals(testWordB).Should.Be(False)
    End Sub

    <Test>
    Public Sub Equals_DifferingMarked_ReturnsFalse()
        Dim testWordA As New TestWord(wordEntry, True, "example")
        Dim testWordB As New TestWord(wordEntry, False, "example")

        testWordA.Equals(testWordB).Should.Be(False)
    End Sub

    <Test>
    Public Sub Equals_DifferingExample_ReturnsFalse()
        Dim testWordA As New TestWord(wordEntry, True, "example")
        Dim testWordB As New TestWord(wordEntry, True, "otherExample")

        testWordA.Equals(testWordB).Should.Be(False)
    End Sub

    <Test>
    Public Sub Equals_NothingVersusEmptyStringExample_ReturnsTrue()
        ' VB `=` on strings treats Nothing and "" as equal.
        Dim testWordA As New TestWord(wordEntry, True, Nothing)
        Dim testWordB As New TestWord(wordEntry, True, "")

        testWordA.Equals(testWordB).Should.Be(True)
    End Sub

    <Test>
    Public Sub GetHashCode_SameCtorArgs_ReturnsEqualHashCodes()
        Dim entryA As New WordEntry("word", "pre", "post", WordType.Verb, "meaning", "info", True)
        Dim entryB As New WordEntry("word", "pre", "post", WordType.Verb, "meaning", "info", True)
        Dim testWordA As New TestWord(entryA, True, "example")
        Dim testWordB As New TestWord(entryB, True, "example")

        testWordA.GetHashCode().Should.Be(testWordB.GetHashCode())
    End Sub
End Class
