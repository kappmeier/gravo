Public Interface ICardsDao

    Function Load(ByVal wordNumber As Integer) As Card

    Sub Save(ByVal card As Card, ByVal wordNumber As Integer)

    Sub UpdateSuccess(group As GroupEntry, testWord As TestWord, queryDirection As QueryLanguage)

    Sub UpdateFailure(group As GroupEntry, testWord As TestWord, queryDirection As QueryLanguage)

    Function Skip(Group As GroupEntry, testWord As TestWord, queryDirection As QueryLanguage) As Boolean

    Sub UpdateSuccess(entry As WordEntry, queryDirection As QueryLanguage)

    Sub UpdateFailure(entry As WordEntry, queryDirection As QueryLanguage)

    Function Skip(entry As WordEntry, queryDirection As QueryLanguage) As Boolean

End Interface


