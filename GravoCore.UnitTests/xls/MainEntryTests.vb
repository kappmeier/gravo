Imports Gravo
Imports NUnit.Framework
Imports FluentAssertions

''' <summary>
''' Tests the GravoCore/xls/MainEntry.vb class. Tests simple data holder class,
''' including construction, properties, typed-Equals, and GetHashCode.
''' </summary>
<TestFixture>
Public Class MainEntryTests

    <Test>
    Public Sub Constructor_SetsWordLanguageAndMainLanguage()
        Dim entry As New MainEntry("word", "lang", "mainLang")

        entry.Word.Should.Be("word")
        entry.Language.Should.Be("lang")
        entry.MainLanguage.Should.Be("mainLang")
    End Sub

    <Test>
    Public Sub Equals_SameValues_ReturnsTrue()
        Dim entryA As New MainEntry("word", "lang", "mainLang")
        Dim entryB As New MainEntry("word", "lang", "mainLang")

        entryA.Equals(entryB).Should.Be(True)
    End Sub

    <Test>
    Public Sub Equals_Nothing_ReturnsFalse()
        Dim entry As New MainEntry("word", "lang", "mainLang")

        entry.Equals(Nothing).Should.Be(False)
    End Sub

    <Test>
    Public Sub Equals_OtherType_ReturnsFalse()
        Dim entry As New MainEntry("word", "lang", "mainLang")

        entry.Equals("word").Should.Be(False)
    End Sub

    <TestCase("otherWord", "lang", "mainLang")>
    <TestCase("word", "otherLang", "mainLang")>
    <TestCase("word", "lang", "otherMainLang")>
    Public Sub Equals_DifferingField_ReturnsFalse(word As String, language As String, mainLanguage As String)
        Dim entryA As New MainEntry("word", "lang", "mainLang")
        Dim entryB As New MainEntry(word, language, mainLanguage)

        entryA.Equals(entryB).Should.Be(False)
    End Sub

    <Test>
    Public Sub Equals_NothingVersusEmptyStringField_ReturnsTrue()
        ' VB `=` on strings treats Nothing and "" as equal, so entries differing only
        ' by Nothing vs "" in a string field are considered equal.
        Dim entryA As New MainEntry("word", Nothing, "mainLang")
        Dim entryB As New MainEntry("word", "", "mainLang")

        entryA.Equals(entryB).Should.Be(True)
    End Sub

    <Test>
    Public Sub GetHashCode_SameConstructorArgs_ReturnsEqualHashCodes()
        Dim entryA As New MainEntry("word", "lang", "mainLang")
        Dim entryB As New MainEntry("word", "lang", "mainLang")

        entryA.GetHashCode().Should.Be(entryB.GetHashCode())
    End Sub

    <Test>
    Public Sub GetHashCode_NothingVersusEmptyStringField_ReturnsEqualHashCodes()
        Dim entryA As New MainEntry("word", Nothing, "mainLang")
        Dim entryB As New MainEntry("word", "", "mainLang")

        entryA.GetHashCode().Should.Be(entryB.GetHashCode())
    End Sub
End Class
