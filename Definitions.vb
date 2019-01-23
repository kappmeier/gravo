Module _Definitions
    Public AppVersionLong As String = "6.0 α"
    Public AppVersionShort As String = "6"
    Public AppTitleLong As String = Application.ProductName & " " & AppVersionShort & " Sprachtrainer"
    Public AppTitleShort As String = Application.ProductName

    Public DBPath As String = Application.StartupPath() & "\voc.mdb"
    Public DBPathLoc As String = Application.StartupPath() & "\languages.mdb"

    Function NowDB() As String
        ' System.Data.Common.
        Return Format(Now, "dd.MM.yyyy")
    End Function

    Function GetLoc() As localization
        Static loc As localization
        If loc Is Nothing Then
            Dim db As DataBaseOperation = New AccessDatabaseOperation(DBPathLoc)
            loc = New localization(db)
        End If
        Return loc
    End Function

End Module
