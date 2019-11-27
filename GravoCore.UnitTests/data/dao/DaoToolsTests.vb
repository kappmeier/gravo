Imports Gravo
Imports NUnit.Framework

Public Class DaoToolsTests
    <Test>
    Public Sub EscapeSingleQuotes_WithoutSingleQuote_RemainsUnchanged()
        Dim result As String = DaoTools.EscapeSingleQuotes("test")
        Assert.AreEqual("test", result)
    End Sub

    <Test>
    Public Sub EscapeSingleQuotes_WithSingleQuote_IsEscaped()
        Dim result As String = DaoTools.EscapeSingleQuotes("can't")
        Assert.AreEqual("can''t", result)
    End Sub

    <Test>
    Public Sub EscapeSingleQuotes_DoubledQuotes_AreEscaped()
        Dim result As String = DaoTools.EscapeSingleQuotes("What''?")
        Assert.AreEqual("What''''?", result)
    End Sub

    <Test>
    Public Sub EscapeSingleQuotes_QuotesAtEnd_AreEscaped()
        Dim result As String = DaoTools.EscapeSingleQuotes("Alex'")
        Assert.AreEqual("Alex''", result)
    End Sub

    <Test>
    Public Sub EscapeSingleQuotes_OnlyQuotes_AreEscaped()
        Dim result As String = DaoTools.EscapeSingleQuotes("'")
        Assert.AreEqual("''", result)
    End Sub

    <Test>
    Public Sub EscapeSingleQuotes_EmptyInput_RemainsUnchanged()
        Dim result As String = DaoTools.EscapeSingleQuotes("")
        Assert.AreEqual("", result)
    End Sub

    <Test>
    Public Sub EscapeSingleQuotes_EmptyList_RemainsEmpty()
        Dim result As IEnumerable(Of Object) = DaoTools.EscapeSingleQuotes(New List(Of Object))
        CollectionAssert.AreEqual(New List(Of String), result)
    End Sub

    <Test>
    Public Sub EscapeSingleQuotes_EmptyList_EscapesAllEntries()
        Dim result As IEnumerable(Of Object) = DaoTools.EscapeSingleQuotes(New List(Of Object) From {"2", "'", "can't"})
        CollectionAssert.AreEqual(New List(Of String) From {"2", "''", "can''t"}, result)
    End Sub

    <Test>
    Public Sub BooleanToDBFormat_AllInputs_AreConverted()
        Assert.AreEqual(0, DaoTools.GetDBEntry(False))
        Assert.AreEqual(1, DaoTools.GetDBEntry(True))
    End Sub

    <Test>
    Public Sub StripSpecialCharacters_WithoutSpecialCharacters_RemainsUnchanged()
        Assert.AreEqual("Hello", DaoTools.StripSpecialCharacters("Hello"))
        Assert.AreEqual("x", DaoTools.StripSpecialCharacters("x"))
        Assert.AreEqual("", DaoTools.StripSpecialCharacters(""))
    End Sub

    <Test>
    Public Sub StripSpecialCharacters_Space_IsRemoved()
        Assert.AreEqual("Hello", DaoTools.StripSpecialCharacters("Hello!"))
        Assert.AreEqual("", DaoTools.StripSpecialCharacters("!"))
    End Sub

    <Test>
    Public Sub StripSpecialCharacters_ExclamationMark_IsRemoved()
        Assert.AreEqual("HelloWorld", DaoTools.StripSpecialCharacters("Hello World"))
        Assert.AreEqual("", DaoTools.StripSpecialCharacters(" "))
    End Sub

    <Test>
    Public Sub StripSpecialCharacters_MultipleCharacters_AreRemoved()
        Assert.AreEqual("Thisiscrazy1", DaoTools.StripSpecialCharacters("This is crazy!!!1!"))
    End Sub

End Class
