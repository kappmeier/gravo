Module _2k6Def
    Public AppTitleLong As String = "VokTrain 2k7-Edition"
    Public AppTitleShort As String = "VokTrain 2k7"
    Public DBVersion As String = "1.22"

  Function NowDB() As String
    Return Format(Now, "dd.MM.yyyy")
  End Function
End Module
