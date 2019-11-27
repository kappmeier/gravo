Module _Definitions
    Public AppVersionLong As String = "6.0 α"
    Public AppVersionShort As String = "6"
    Public AppTitleLong As String = Application.ProductName & " " & AppVersionShort & " Sprachtrainer"
    Public AppTitleShort As String = Application.ProductName

    Public DBPath As String = Application.StartupPath() & "\voc.s3db"
    Public DBPathLoc As String = Application.StartupPath() & "\languages.s3db"

    Function NowDB() As String
        Return Format(Now, "yyyy-MM-dd")
    End Function

    Function GetLoc() As localization
        Static loc As localization
        If loc Is Nothing Then
            Dim db As IDataBaseOperation = New SQLiteDataBaseOperation()
            db.Open(DBPathLoc)
            loc = New localization(db)
        End If

        Return loc
    End Function
End Module
