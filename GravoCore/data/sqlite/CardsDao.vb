Imports Gravo

Public Class CardsDao
    Implements ICardsDao

    ' TODO: Enum für MainLanguage test vs. ForeignLanguage test

    Private ReadOnly DBConnection As IDataBaseOperation
    Private ReadOnly TargetLanguageIntervalName As String = "[TestInterval]"
    Private ReadOnly TargetLanguageCounterName As String = "[Counter]"
    Private ReadOnly OriginalLanguageIntervalName As String = "[TestIntervalMain]"
    Private ReadOnly OriginalLanguageCounterName As String = "[CounterMain]"
    Shared ReadOnly Success As Func(Of Integer, Integer) = Function(interval) interval * 2
    Shared ReadOnly Failure As Func(Of Integer, Integer) = Function(interval) Math.Max((interval / 2), 1)

    Sub New(ByRef db As IDataBaseOperation)
        DBConnection = db
    End Sub

    Public Function Load(wordNumber As Integer) As Card Implements ICardsDao.Load
        Dim command As String = "SELECT [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain] FROM [Cards] WHERE [Index] = ?" & wordNumber
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {wordNumber}))
        If Not DBConnection.DBCursor.HasRows Then Throw New EntryNotFoundException("Entry " & wordNumber & " not found in global cards-system. If you Expect to have it, try to reorganize the database.")

        DBConnection.DBCursor.Read()
        Dim testInterval As Integer = DBConnection.SecureGetInt32(0)
        Dim counter As Integer = DBConnection.SecureGetInt32(1)
        Dim lastDate As DateTime = DBConnection.SecureGetDateTime(2)
        Dim testIntervalMain As Integer = DBConnection.SecureGetInt32(3)
        Dim counterMain As Integer = DBConnection.SecureGetInt32(4)
        DBConnection.DBCursor.Close()

        Return New Card(testInterval, counter, lastDate, testIntervalMain, counterMain)
    End Function

    ''' <summary>
    ''' Creates a new card entry. To be used when a new word is added to the dictionary.
    ''' </summary>
    ''' <param name="index">The word index.</param>
    Friend Sub AddNewEntry(ByVal index As Integer)
        Dim command As String = "INSERT INTO Cards ([Index], [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain]) VALUES (?, ?, ?, ?, ?, ?)"
        DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(New List(Of Object) From {index, 1, 1, SQLiteDataBaseOperation.NowDB(), 1, 1}))
    End Sub

    Public Sub Save(card As Card, wordNumber As Integer) Implements ICardsDao.Save
        'If m_wordNumber = -1 Then Throw New xlsExceptionCards(2)
        Dim command As String = "UPDATE [Cards] SET [TestInterval] = ?, [Counter] = ?, [LastDate] = ?, [TestIntervalMain] = ?, [CounterMain] = ? WHERE [Index] = ?"
        DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(New List(Of Object) From {card.TestInterval, card.Counter, card.LastDate, card.TestIntervalMain, card.CounterMain, wordNumber}))
    End Sub

    Public Sub UpdateSuccess(group As GroupEntry, testWord As TestWord, queryDirection As QueryLanguage) Implements ICardsDao.UpdateSuccess
        Dim tableNameSafe As String = StripSpecialCharacters(group.Table)
        If queryDirection = QueryLanguage.TargetLanguage Or queryDirection = QueryLanguage.Both Then
            Dim interval = GetValues(tableNameSafe, testWord.WordIndex, "WordIndex", "word entry", TargetLanguageIntervalName, TargetLanguageCounterName).Item2
            Update(tableNameSafe, testWord.WordIndex, "WordIndex", Success(interval), TargetLanguageIntervalName, TargetLanguageCounterName)
        End If
        If queryDirection = QueryLanguage.OriginalLanguage Or queryDirection = QueryLanguage.Both Then
            Dim interval = GetValues(tableNameSafe, testWord.WordIndex, "WordIndex", "word entry", OriginalLanguageIntervalName, OriginalLanguageCounterName).Item2
            Update(tableNameSafe, testWord.WordIndex, "WordIndex", Success(interval), OriginalLanguageIntervalName, OriginalLanguageCounterName)
        End If
    End Sub

    Public Sub UpdateFailure(Group As GroupEntry, testWord As TestWord, queryDirection As QueryLanguage) Implements ICardsDao.UpdateFailure
        Dim tableNameSafe As String = StripSpecialCharacters(Group.Table)
        If queryDirection = QueryLanguage.TargetLanguage Or queryDirection = QueryLanguage.Both Then
            Dim interval = GetValues(tableNameSafe, testWord.WordIndex, "WordIndex", "word entry", TargetLanguageIntervalName, TargetLanguageCounterName).Item2
            Update(tableNameSafe, testWord.WordIndex, "WordIndex", Failure(interval), TargetLanguageIntervalName, TargetLanguageCounterName)
        End If
        If queryDirection = QueryLanguage.OriginalLanguage Or queryDirection = QueryLanguage.Both Then
            Dim interval = GetValues(tableNameSafe, testWord.WordIndex, "WordIndex", "word entry", OriginalLanguageIntervalName, OriginalLanguageCounterName).Item2
            Update(tableNameSafe, testWord.WordIndex, "WordIndex", Failure(interval), OriginalLanguageIntervalName, OriginalLanguageCounterName)
        End If
    End Sub

    ''' <summary>
    ''' Store a test entry as skipped because it's not ready for next card based check. When the counter until next check is
    ''' already 1, it is not furhter decreased. By definiton of the card system, it is an error to skip such word entries,
    ''' but no exception is thrown.
    ''' </summary>
    ''' <param name="GroupTable"></param>
    ''' <param name="WordNumber"></param>
    Public Function Skip(group As GroupEntry, testWord As TestWord, queryDirection As QueryLanguage) As Boolean Implements ICardsDao.Skip
        Dim tableNameSafe As String = StripSpecialCharacters(group.Table)
        If queryDirection = QueryLanguage.TargetLanguage Or queryDirection = QueryLanguage.Both Then
            Return Skip(tableNameSafe, testWord.WordIndex, "WordIndex", "test word", TargetLanguageIntervalName, TargetLanguageCounterName)
        End If
        If queryDirection = QueryLanguage.OriginalLanguage Or queryDirection = QueryLanguage.Both Then
            Return Skip(tableNameSafe, testWord.WordIndex, "WordIndex", "test word", OriginalLanguageIntervalName, OriginalLanguageCounterName)
        End If
    End Function

    Public Sub UpdateSuccess(entry As WordEntry, queryDirection As QueryLanguage) Implements ICardsDao.UpdateSuccess
        If queryDirection = QueryLanguage.TargetLanguage Or queryDirection = QueryLanguage.Both Then
            Dim interval = GetValues("Cards", entry.Index, "Index", "word entry", TargetLanguageIntervalName, TargetLanguageCounterName).Item2
            Update("Cards", entry.Index, "Index", Success(interval), TargetLanguageIntervalName, TargetLanguageCounterName)
        End If
        If queryDirection = QueryLanguage.OriginalLanguage Or queryDirection = QueryLanguage.Both Then
            Dim interval = GetValues("Cards", entry.Index, "Index", "word entry", OriginalLanguageIntervalName, OriginalLanguageCounterName).Item2
            Update("Cards", entry.Index, "Index", Success(interval), OriginalLanguageIntervalName, OriginalLanguageCounterName)
        End If
    End Sub

    Public Sub UpdateFailure(entry As WordEntry, queryDirection As QueryLanguage) Implements ICardsDao.UpdateFailure
        If queryDirection = QueryLanguage.TargetLanguage Or queryDirection = QueryLanguage.Both Then
            Dim interval = GetValues("Cards", entry.Index, "Index", "word entry", TargetLanguageIntervalName, TargetLanguageCounterName).Item2
            Update("Cards", entry.Index, "Index", Failure(interval), TargetLanguageIntervalName, TargetLanguageCounterName)
        End If
        If queryDirection = QueryLanguage.OriginalLanguage Or queryDirection = QueryLanguage.Both Then
            Dim interval = GetValues("Cards", entry.Index, "Index", "word entry", OriginalLanguageIntervalName, OriginalLanguageCounterName).Item2
            Update("Cards", entry.Index, "Index", Failure(interval), OriginalLanguageIntervalName, OriginalLanguageCounterName)
        End If
    End Sub

    Private Sub Update(tableNameSafe As String, wordIndex As Integer, indexColumn As String, interval As Integer, intervalColumn As String, counterColumn As String)
        Dim command = "UPDATE [" & tableNameSafe & "] SET " & intervalColumn & " = ?, " & counterColumn & " = ?, [LastDate] = ? WHERE [" & indexColumn & "] = ?"
        DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(New List(Of Object) From {interval, interval, SQLiteDataBaseOperation.NowDB(), wordIndex}))
    End Sub

    Public Function Skip(entry As WordEntry, queryDirection As QueryLanguage) As Boolean Implements ICardsDao.Skip
        If queryDirection = QueryLanguage.TargetLanguage Or queryDirection = QueryLanguage.Both Then
            Return Skip("Cards", entry.Index, "Index", "word entry", TargetLanguageIntervalName, TargetLanguageCounterName)
        End If
        If queryDirection = QueryLanguage.OriginalLanguage Or queryDirection = QueryLanguage.Both Then
            Return Skip("Cards", entry.Index, "Index", "word entry", OriginalLanguageIntervalName, OriginalLanguageCounterName)
        End If
    End Function

    Private Function Skip(tableNameSafe As String, wordIndex As Integer, indexColumn As String, what As String, intervalColumn As String, counterColumn As String) As Boolean
        Dim counter As Integer = GetValues(tableNameSafe, wordIndex, indexColumn, what, intervalColumn, counterColumn).Item1

        If counter <= 1 Then
            Return False
        End If

        Dim command = "UPDATE [" & tableNameSafe & "] SET " & counterColumn & " = ? WHERE [" & indexColumn & "] = ?"
        DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(New List(Of Object) From {counter - 1, wordIndex}))
        Return True
    End Function

    ''' <summary>
    ''' tableNameSafe must be called with safe table name that is stripped from special characters.
    ''' </summary>
    ''' <returns>Tuple containing counter and interval.</returns>
    Private Function GetValues(tableNameSafe As String, wordIndex As Integer, indexColumn As String, what As String, intervalColumn As String, counterColumn As String) As Tuple(Of Integer, Integer)
        Dim command As String = "SELECT " & counterColumn & ", " & intervalColumn & " FROM [" & tableNameSafe & "] WHERE [" & indexColumn & "] = ?"
        DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {wordIndex}))
        FailIfEmpty(DBConnection, Function() As Exception
                                      Return New EntryNotFoundException("Cards for " & what & " not found.")
                                  End Function)
        DBConnection.DBCursor.Read()
        Dim counter As Integer = DBConnection.SecureGetInt32(0)
        Dim interval As Integer = DBConnection.SecureGetInt32(1)
        DBConnection.DBCursor.Close()
        GetValues = Tuple.Create(counter, interval)
    End Function

End Class
