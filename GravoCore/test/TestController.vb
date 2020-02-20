Imports Gravo

Public Class TestController
    Private ReadOnly dbConnection As IDataBaseOperation
    Private ReadOnly dictionaryDao As DictionaryDao
    Private ReadOnly testData As TestData
    Private ReadOnly useCards As Boolean = True
    Private ReadOnly queryLanguage As QueryLanguage

    Private currentChecker As Checker
    Private currentTestEntry As TestEntry

    Public Sub New(testData As TestData, queryLanguage As QueryLanguage, dbConnection As IDataBaseOperation)
        Me.testData = testData
        Me.queryLanguage = queryLanguage
        Me.dbConnection = dbConnection
        Me.dictionaryDao = New DictionaryDao(dbConnection)

        NextWord()
    End Sub

    ''' <summary>
    ''' Checks wheather more words are to be tested until the test is over.
    ''' </summary>
    ''' <returns>true if more words are to test</returns>
    Public Function HasWords() As Boolean
        HasWords = Not testData.IsEmpty
    End Function

    Public Function GetTestChecker() As Checker
        GetTestChecker = currentChecker
    End Function

    ''' <summary>
    ''' Offer a test result status.
    ''' </summary>
    Public Sub Update(result As TestResult)
        testData.Update(result)
        If useCards AndAlso currentTestEntry.firstTest Then
            UpdateCards(result)
        End If
        NextWord()
    End Sub

    Private Sub NextWord()
        If testData.IsEmpty() Then
            currentTestEntry = Nothing
            currentChecker = Nothing
        Else
            If testData.Current.Equals(currentTestEntry) Then
                currentChecker.Retest = True
            Else
                currentTestEntry = testData.Current()
                currentChecker = New Checker(dictionaryDao, queryLanguage, currentTestEntry.word)
            End If
        End If
    End Sub

    Public Function Count() As String
        Count = testData.Count
    End Function

    ''' <summary>
    ''' Updates the card system with respect to a test result.
    ''' </summary>
    ''' <param name="result">the test result</param>
    Private Sub UpdateCards(result As TestResult)
        Dim cards As New CardsDao(dbConnection)
        If result = TestResult.NoError Then
            cards.UpdateSuccess(currentTestEntry.word, queryLanguage)
        ElseIf result = TestResult.Wrong Then
            cards.UpdateFailure(currentTestEntry.word, queryLanguage)
        End If
    End Sub
End Class
