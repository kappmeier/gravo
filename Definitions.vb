Module _Definitions
  Public AppVersionLong As String = "2k7-Edition"
  Public AppVersionShort As String = "2k7"
    Public AppTitleLong As String = Application.ProductName & " - " & " Sprachtrainer"
  Public AppTitleShort As String = Application.ProductName

  Function NowDB() As String
        Return Format(Now, "dd.MM.yyyy")
  End Function
End Module
