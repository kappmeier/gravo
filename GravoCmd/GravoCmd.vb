Module GravoCmd
    Private DBPath As String = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()(0)) & "\voc.s3db"

    Sub Main()
        Console.WriteLine("Gravo Command Line Edition (pre-release)")

        Console.WriteLine("Loading database from" & DBPath)

        Dim DB As IDataBaseOperation = New SQLiteDataBaseOperation()
        DB.Open(DBPath)

        Dim GroupsDao As IGroupsDao = New GroupsDao(DB)

        Dim GroupNames = GroupsDao.GetGroups()

        Console.WriteLine("Available Groups:")
        Dim counter = 1
        For Each Group As String In GroupNames
            Console.WriteLine(counter & ") " & Group)
            counter += 1
        Next Group

        Console.Write("Please select: ")
        Dim input As String = Console.ReadLine()

        Dim result As Integer
        Integer.TryParse(input, result)

        counter = 1
        Dim TargetGroup As String = Nothing
        For Each Group As String In GroupNames
            If counter = result Then
                TargetGroup = Group
            End If
            counter += 1
        Next Group

        If (TargetGroup Is Nothing) Then
            Console.WriteLine("Invalid entry")
        Else

        End If
        Dim Groups = GroupsDao.GetSubGroups(TargetGroup)
        Console.WriteLine(TargetGroup & ":")
        For Each Group As GroupEntry In Groups
            Console.WriteLine(" - " & Group.SubGroup)
        Next Group

        Console.ReadLine()

    End Sub

End Module
