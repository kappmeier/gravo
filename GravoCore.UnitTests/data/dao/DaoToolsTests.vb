Imports Gravo
Imports NUnit.Framework
Imports NUnit.Framework.Legacy

Public Class DaoToolsTests
    <Test>
    Public Sub EscapeSingleQuotes_WithoutSingleQuote_RemainsUnchanged()
        Dim result As String = DaoTools.EscapeSingleQuotes("test")
        ClassicAssert.AreEqual("test", result)
    End Sub

    <Test>
    Public Sub EscapeSingleQuotes_WithSingleQuote_IsEscaped()
        Dim result As String = DaoTools.EscapeSingleQuotes("can't")
        ClassicAssert.AreEqual("can''t", result)
    End Sub

    <Test>
    Public Sub EscapeSingleQuotes_DoubledQuotes_AreEscaped()
        Dim result As String = DaoTools.EscapeSingleQuotes("What''?")
        ClassicAssert.AreEqual("What''''?", result)
    End Sub

    <Test>
    Public Sub EscapeSingleQuotes_QuotesAtEnd_AreEscaped()
        Dim result As String = DaoTools.EscapeSingleQuotes("Alex'")
        ClassicAssert.AreEqual("Alex''", result)
    End Sub

    <Test>
    Public Sub EscapeSingleQuotes_OnlyQuotes_AreEscaped()
        Dim result As String = DaoTools.EscapeSingleQuotes("'")
        ClassicAssert.AreEqual("''", result)
    End Sub

    <Test>
    Public Sub EscapeSingleQuotes_EmptyInput_RemainsUnchanged()
        Dim result As String = DaoTools.EscapeSingleQuotes("")
        ClassicAssert.AreEqual("", result)
    End Sub

    <Test>
    Public Sub ToDbParameters_EmptyList_RemainsEmpty()
        Dim result As IEnumerable(Of Object) = DaoTools.ToDbParameters(New List(Of Object))
        CollectionAssert.AreEqual(New List(Of String), result)
    End Sub

    <Test>
    Public Sub ToDbParameters_Strings_ArePassedThroughUnescaped()
        Dim result As IEnumerable(Of Object) = DaoTools.ToDbParameters(New List(Of Object) From {"2", "'", "can't"})
        CollectionAssert.AreEqual(New List(Of String) From {"2", "'", "can't"}, result)
    End Sub

    <Test>
    Public Sub ToDbParameters_Booleans_BecomeOneAndZero()
        Dim result As IEnumerable(Of Object) = DaoTools.ToDbParameters(New List(Of Object) From {True, False, 7})
        CollectionAssert.AreEqual(New List(Of Object) From {"1", "0", 7}, result)
    End Sub

    <Test>
    Public Sub BooleanToDBFormat_AllInputs_AreConverted()
        ClassicAssert.AreEqual(0, DaoTools.GetDBEntry(False))
        ClassicAssert.AreEqual(1, DaoTools.GetDBEntry(True))
    End Sub

    <Test>
    Public Sub StripSpecialCharacters_WithoutSpecialCharacters_RemainsUnchanged()
        ClassicAssert.AreEqual("Hello", DaoTools.StripSpecialCharacters("Hello"))
        ClassicAssert.AreEqual("x", DaoTools.StripSpecialCharacters("x"))
        ClassicAssert.AreEqual("", DaoTools.StripSpecialCharacters(""))
    End Sub

    <Test>
    Public Sub StripSpecialCharacters_Space_IsRemoved()
        ClassicAssert.AreEqual("Hello", DaoTools.StripSpecialCharacters("Hello!"))
        ClassicAssert.AreEqual("", DaoTools.StripSpecialCharacters("!"))
    End Sub

    <Test>
    Public Sub StripSpecialCharacters_ExclamationMark_IsRemoved()
        ClassicAssert.AreEqual("HelloWorld", DaoTools.StripSpecialCharacters("Hello World"))
        ClassicAssert.AreEqual("", DaoTools.StripSpecialCharacters(" "))
    End Sub

    <Test>
    Public Sub StripSpecialCharacters_MultipleCharacters_AreRemoved()
        ClassicAssert.AreEqual("Thisiscrazy1", DaoTools.StripSpecialCharacters("This is crazy!!!1!"))
    End Sub

    <Test>
    Public Sub CheckAllowedText_DoubleQuote_Throws()
        Assert.Throws(Of InputException)(Sub() DaoTools.CheckAllowedText("ok", "x""y"))
    End Sub

    <Test>
    Public Sub CheckAllowedText_ApostropheAndNothing_Pass()
        Assert.DoesNotThrow(Sub() DaoTools.CheckAllowedText("un po'", "l'", Nothing, ""))
        Assert.DoesNotThrow(Sub() DaoTools.CheckAllowedText())
        Assert.DoesNotThrow(Sub() DaoTools.CheckAllowedText(CType(Nothing, String())))
    End Sub

End Class
