Imports Gravo
Imports NUnit.Framework
Imports FluentAssertions

''' <summary>
''' Tests the GravoCore/xls/WordEntry class. For the simple data holder the
''' construction, typed-Equals, and GetHashCode behavior is tested.
''' A publicly constructed WordEntry is not yet stored in the database and reports
''' Index = 0 and WordIndex = 0.
''' </summary>
<TestFixture>
Public Class WordEntryTests

    <Test>
    Public Sub Constructor_RoundTripsAllFields()
        Dim entry As New WordEntry("word", "pre", "post", WordType.Verb, "meaning", "info", True)

        entry.Word.Should.Be("word")
        entry.Pre.Should.Be("pre")
        entry.Post.Should.Be("post")
        entry.WordType.Should.Be(WordType.Verb)
        entry.Meaning.Should.Be("meaning")
        entry.AdditionalTargetLangInfo.Should.Be("info")
        entry.Irregular.Should.Be(True)
    End Sub

    <Test>
    Public Sub Constructor_PubliclyConstructed_IndexAndWordIndexAreAlwaysZero()
        Dim entry As New WordEntry("word", "pre", "post", WordType.Verb, "meaning", "info", True)

        entry.Index.Should.Be(0)
        entry.WordIndex.Should.Be(0)
    End Sub

    <Test>
    Public Sub Equals_SameValues_ReturnsTrue()
        Dim entryA As New WordEntry("word", "pre", "post", WordType.Verb, "meaning", "info", True)
        Dim entryB As New WordEntry("word", "pre", "post", WordType.Verb, "meaning", "info", True)

        entryA.Equals(entryB).Should.Be(True)
    End Sub

    <Test>
    Public Sub Equals_Nothing_ReturnsFalse()
        Dim entry As New WordEntry("word", "pre", "post", WordType.Verb, "meaning", "info", True)

        entry.Equals(Nothing).Should.Be(False)
    End Sub

    <Test>
    Public Sub Equals_OtherType_ReturnsFalse()
        Dim entry As New WordEntry("word", "pre", "post", WordType.Verb, "meaning", "info", True)

        entry.Equals("word").Should.Be(False)
    End Sub

    <TestCase("otherWord", "pre", "post", WordType.Verb, "meaning", "info", True)>
    <TestCase("word", "otherPre", "post", WordType.Verb, "meaning", "info", True)>
    <TestCase("word", "pre", "otherPost", WordType.Verb, "meaning", "info", True)>
    <TestCase("word", "pre", "post", WordType.Adjective, "meaning", "info", True)>
    <TestCase("word", "pre", "post", WordType.Verb, "otherMeaning", "info", True)>
    <TestCase("word", "pre", "post", WordType.Verb, "meaning", "otherInfo", True)>
    <TestCase("word", "pre", "post", WordType.Verb, "meaning", "info", False)>
    Public Sub Equals_DifferingField_ReturnsFalse(word As String, pre As String, post As String, wordType As WordType, meaning As String, info As String, irregular As Boolean)
        Dim entryA As New WordEntry("word", "pre", "post", WordType.Verb, "meaning", "info", True)
        Dim entryB As New WordEntry(word, pre, post, wordType, meaning, info, irregular)

        entryA.Equals(entryB).Should.Be(False)
    End Sub

    <Test>
    Public Sub Equals_NothingVersusEmptyStringField_ReturnsTrue()
        ' VB `=` on strings treats Nothing and "" as equal.
        Dim entryA As New WordEntry("word", Nothing, "post", WordType.Verb, "meaning", "info", True)
        Dim entryB As New WordEntry("word", "", "post", WordType.Verb, "meaning", "info", True)

        entryA.Equals(entryB).Should.Be(True)
    End Sub

    <Test>
    Public Sub GetHashCode_SameCtorArgs_ReturnsEqualHashCodes()
        Dim entryA As New WordEntry("word", "pre", "post", WordType.Verb, "meaning", "info", True)
        Dim entryB As New WordEntry("word", "pre", "post", WordType.Verb, "meaning", "info", True)

        entryA.GetHashCode().Should.Be(entryB.GetHashCode())
    End Sub

    <Test>
    Public Sub GetHashCode_NothingVersusEmptyStringField_ReturnsEqualHashCodes()
        ' Equals treats Nothing and "" as equal (VB standard = comparison)
        Dim entryA As New WordEntry("word", Nothing, "post", WordType.Verb, "meaning", "info", True)
        Dim entryB As New WordEntry("word", "", "post", WordType.Verb, "meaning", "info", True)

        entryA.GetHashCode().Should.Be(entryB.GetHashCode())
    End Sub

    <Test>
    Public Sub GetHashCode_DifferingIndexOnly_ReturnsEqualHashCodes()
        ' Equals ignores _index
        Dim entryA As New GroupDaoTests.MockWordEntry(1, "word", "pre", "post", WordType.Verb, "meaning", "info", True)
        Dim entryB As New GroupDaoTests.MockWordEntry(2, "word", "pre", "post", WordType.Verb, "meaning", "info", True)

        entryA.Equals(entryB).Should.Be(True)
        entryA.GetHashCode().Should.Be(entryB.GetHashCode())
    End Sub
End Class
