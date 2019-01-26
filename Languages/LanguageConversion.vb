Imports System.Collections.ObjectModel
''' <summary>
''' Provides helper methods to copy language databases.
''' </summary>
Module LanguageConversion
    Sub ConvertDatabase(OldDB As DataBaseOperation, NewDB As DataBaseOperation)
        Dim Loc As localization = New localization(OldDB)

        CreateDictionaryTable(Loc, NewDB)

        CopyLanguages(Loc, NewDB)

        Dim loc2 As localization = New localization(NewDB)
        Dim languages As ObjectModel.Collection(Of String) = Loc.GetLanguageNames()

        NewDB.Close()
    End Sub

    Private Sub CreateDictionaryTable(OldLoc As localization, NewDB As DataBaseOperation)
        Dim Command = "CREATE TABLE [languages] ([Language] Text(16) NOT NULL, " &
            "[Name] Text(32) NOT NULL, [Author] Text(64) NOT NULL, [Table] Text(32) NOT NULL, " &
            "[Date] DateTime NOT NULL, [Version] Text(5));"
        NewDB.ExecuteNonQuery(Command)

        For Each language As String In OldLoc.GetLanguageNames
            Command = "INSERT INTO languages ([Language], [Name], [Author], [Table], [Date], " &
                "[Version]) VALUES ('" & OldLoc.GetLanguageFor(language) & "', '" & language &
                "', '" & OldLoc.GetAuthorFor(language) & "', '" & OldLoc.GetTableFor(language) &
                "', '" & OldLoc.GetDateFor(language) & "', '" & OldLoc.GetVersionFor(language) & "')"
            NewDB.ExecuteNonQuery(Command)
        Next
    End Sub

    Private Sub CopyLanguages(Loc As localization, NewDB As DataBaseOperation)
        Dim max As Int16 = 0
        For Each language As String In Loc.GetLanguageNames
            max = Math.Max(max, CopyLanguage(language, Loc, NewDB))
        Next

        Dim createTableCommand = "CREATE TABLE [general] ([Field] LONG NOT NULL, [Text] MEMO)"
        NewDB.ExecuteNonQuery(createTableCommand)
        For i As Integer = 0 To max
            Dim Command = "INSERT INTO [general] ([Field], [Text]) VALUES (" & i & " , '#')"
            NewDB.ExecuteNonQuery(Command)
        Next i
    End Sub

    Private Function CopyLanguage(language As String, Loc As localization,
                                  NewDB As DataBaseOperation) As SByte
        Loc.SwitchToLanguage(language)

        Dim languageTable = Loc.GetTableFor(language)
        Dim createTableCommand = "CREATE TABLE [" & languageTable & "] " &
            "([Field] LONG NOT NULL, [Text] MEMO)"
        NewDB.ExecuteNonQuery(createTableCommand)

        Dim i As Int16 = -1
        Dim text As String
        Do
            i = i + 1
            Try
                text = Loc.GetText(i)
            Catch ex As Exception
                text = Nothing
            End Try
            If Not text Is Nothing Then
                Dim Command = "INSERT INTO [" & languageTable & "] ([Field], [Text]) " &
                    "VALUES (" & i & ", '" & AccessDatabaseOperation.AddHighColons(text) & "')"
                NewDB.ExecuteNonQuery(Command)
            End If
        Loop While Not text Is Nothing
        Return i
    End Function
End Module
