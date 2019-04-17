﻿Imports Gravo
Imports NUnit.Framework

Public Class DaoToolsTests
    <Test>
    Public Sub AddHighColons_WithoutSingleQuote_RemainsUnchanged()
        Dim result As String = DaoTools.EscapeSingleQuotes("test")
        Assert.AreEqual("test", result)
    End Sub

    <Test>
    Public Sub AddHighColons_WithSingleQuote_IsEscaped()
        Dim result As String = DaoTools.EscapeSingleQuotes("can't")
        Assert.AreEqual("can''t", result)
    End Sub

    <Test>
    Public Sub AddHighColons_DoubledQuotes_AreEscaped()
        Dim result As String = DaoTools.EscapeSingleQuotes("What''?")
        Assert.AreEqual("What''''?", result)
    End Sub

    <Test>
    Public Sub AddHighColons_QuotesAtEnd_AreEscaped()
        Dim result As String = DaoTools.EscapeSingleQuotes("Alex'")
        Assert.AreEqual("Alex''", result)
    End Sub

    <Test>
    Public Sub AddHighColons_OnlyQuotes_AreEscaped()
        Dim result As String = DaoTools.EscapeSingleQuotes("'")
        Assert.AreEqual("''", result)
    End Sub

    <Test>
    Public Sub AddHighColons_EmptyInput_RemainsUnchanged()
        Dim result As String = DaoTools.EscapeSingleQuotes("")
        Assert.AreEqual("", result)
    End Sub

    <Test>
    Public Sub BooleanToString_AllInputs_AreConverted()
        Assert.AreEqual("0", DaoTools.BooleanToString(False))
        Assert.AreEqual("-1", DaoTools.BooleanToString(True))
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
