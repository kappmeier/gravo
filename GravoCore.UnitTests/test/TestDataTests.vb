Imports Gravo
Imports Moq
Imports NUnit.Framework
Imports FluentAssertions

''' <summary>
''' Tests the GravoCore/test/TestData.vb class behavior. Parts of the interfaces is not finally
''' defined, so this tests ensure the current behavior. Needs to be adapted when the class is
''' finalized.
''' </summary>
<TestFixture>
Public Class TestDataTests
    Private ReadOnly wordA As WordEntry = New WordEntry("wordA", "", "", WordType.Verb, "meaningA", "", False)
    Private ReadOnly wordB As WordEntry = New WordEntry("wordB", "", "", WordType.Verb, "meaningB", "", False)
    Private cardsMock As Mock(Of ICardsDao)

    <SetUp>
    Public Sub Setup()
        cardsMock = New Mock(Of ICardsDao)(MockBehavior.Strict)
    End Sub

    <Test>
    Public Sub IsEmpty_WithNoWords_ReturnsTrue()
        Dim words As New List(Of WordEntry)
        Dim data As New TestData(cardsMock.Object, words, QueryLanguage.TargetLanguage)

        data.IsEmpty().Should.Be(True)
    End Sub

    <Test>
    Public Sub IsEmpty_WithWords_ReturnsFalse()
        Dim words As New List(Of WordEntry) From {wordA}
        Dim data As New TestData(cardsMock.Object, words, QueryLanguage.TargetLanguage)

        data.IsEmpty().Should.Be(False)
    End Sub

    <Test>
    Public Sub Count_ReturnsNumberOfWords()
        Dim words As New List(Of WordEntry) From {wordA, wordB}
        Dim data As New TestData(cardsMock.Object, words, QueryLanguage.TargetLanguage)

        data.Count().Should.Be(2)
    End Sub

    <Test>
    Public Sub Update_NoErrorWithNonSkippableHead_RemovesHeadAndCallsSkipOnce()
        Dim words As New List(Of WordEntry) From {wordA, wordB}
        cardsMock.Setup(Function(x) x.Skip(wordA, QueryLanguage.TargetLanguage)).Returns(False)
        Dim data As New TestData(cardsMock.Object, words, QueryLanguage.TargetLanguage)

        data.Update(TestResult.NoError)

        words.Should.BeEquivalentTo(New List(Of WordEntry) From {wordB})
        cardsMock.Verify(Function(x) x.Skip(wordA, QueryLanguage.TargetLanguage), Times.Exactly(1))
    End Sub

    <Test>
    Public Sub Update_NoErrorWithSkippableHead_RemovesSkippedAndAnsweredWords()
        Dim words As New List(Of WordEntry) From {wordA, wordB}
        cardsMock.Setup(Function(x) x.Skip(wordA, QueryLanguage.TargetLanguage)).Returns(True)
        cardsMock.Setup(Function(x) x.Skip(wordB, QueryLanguage.TargetLanguage)).Returns(False)
        Dim data As New TestData(cardsMock.Object, words, QueryLanguage.TargetLanguage)

        data.Update(TestResult.NoError)

        words.Should.BeEmpty()
        cardsMock.Verify(Function(x) x.Skip(wordA, QueryLanguage.TargetLanguage), Times.Exactly(1))
        cardsMock.Verify(Function(x) x.Skip(wordB, QueryLanguage.TargetLanguage), Times.Exactly(1))
    End Sub

    <TestCase(TestResult.Wrong)>
    <TestCase(TestResult.Misspelled)>
    <TestCase(TestResult.OtherMeaning)>
    Public Sub Update_NonNoErrorResult_KeepsWordsAndMakesNoCardCalls(result As TestResult)
        Dim words As New List(Of WordEntry) From {wordA, wordB}
        Dim data As New TestData(cardsMock.Object, words, QueryLanguage.TargetLanguage)

        data.Update(result)

        words.Should.BeEquivalentTo(New List(Of WordEntry) From {wordA, wordB})
        cardsMock.VerifyNoOtherCalls()
    End Sub

    <Test>
    Public Sub Update_NoErrorOnEmptyData_ThrowsNullReferenceExceptionAndStaysEmpty()
        Dim words As New List(Of WordEntry)
        Dim data As New TestData(cardsMock.Object, words, QueryLanguage.TargetLanguage)

        Assert.Throws(Of NullReferenceException)(Sub() data.Update(TestResult.NoError))

        words.Should.BeEmpty()
    End Sub

    <Test>
    Public Sub Update_NoErrorWithAllSkippableWords_ThrowsNullReferenceExceptionAndDrainsList()
        Dim words As New List(Of WordEntry) From {wordA, wordB}
        cardsMock.Setup(Function(x) x.Skip(wordA, QueryLanguage.TargetLanguage)).Returns(True)
        cardsMock.Setup(Function(x) x.Skip(wordB, QueryLanguage.TargetLanguage)).Returns(True)
        Dim data As New TestData(cardsMock.Object, words, QueryLanguage.TargetLanguage)

        Assert.Throws(Of NullReferenceException)(Sub() data.Update(TestResult.NoError))

        words.Should.BeEmpty()
    End Sub
End Class
