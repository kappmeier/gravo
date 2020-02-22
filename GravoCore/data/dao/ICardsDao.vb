Public Interface ICardsDao

    Function Load(ByVal wordNumber As Integer) As Card

    Sub Save(ByVal card As Card, ByVal wordNumber As Integer)

    Sub UpdateSuccess(group As GroupEntry, testWord As TestWord, queryDirection As QueryLanguage)

    Sub UpdateFailure(group As GroupEntry, testWord As TestWord, queryDirection As QueryLanguage)

    Function Skip(Group As GroupEntry, testWord As TestWord, queryDirection As QueryLanguage) As Boolean

    Sub UpdateSuccess(entry As WordEntry, queryDirection As QueryLanguage)

    Sub UpdateFailure(entry As WordEntry, queryDirection As QueryLanguage)

    ''' <summary>
    ''' Tries to skip a word entry during test. If the count is larger than 1, it is reduced by 1. <c>false</c>
    ''' is returned if and only if the count was already at 1.
    ''' </summary>
    ''' <remarks>When the <paramref name="queryDirection"> is set to <c cref="QueryLanguage.Both">Both</c>,
    ''' true is only returned if the word entry could be skipped for both directions.</remarks>
    ''' <param name="entry"></param>
    ''' <param name="queryDirection"></param>
    ''' <returns><c>true</c> if the <paramref name="entry"/> could be skipped</returns>
    Function Skip(entry As WordEntry, queryDirection As QueryLanguage) As Boolean

End Interface


