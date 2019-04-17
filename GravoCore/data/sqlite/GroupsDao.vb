Imports System.Collections.ObjectModel
Imports Gravo

Public Class GroupsDao
    Implements IGroupsDao

    Private ReadOnly DBConnection As DataBaseOperation

    Sub New(ByRef db As DataBaseOperation)
        DBConnection = db
    End Sub

    ''' <summary>
    ''' Load all names of groups. Each group name will only be included once.
    ''' </summary>
    ''' <returns>List of all group names.</returns>
    Public Function GetGroups() As Collection(Of String) Implements IGroupsDao.GetGroups
        Dim GroupNames As New Collection(Of String)
        Dim Command As String = "SELECT DISTINCT [GroupName] FROM [Groups];"
        DBConnection.ExecuteReader(Command)
        Do While DBConnection.DBCursor.Read
            GroupNames.Add(DBConnection.SecureGetString(0))
        Loop
        DBConnection.DBCursor.Close()
        Return GroupNames
    End Function

    Public Function GetAllGroups() As Collection(Of GroupEntry) Implements IGroupsDao.GetAllGroups
        Dim Groups As New Collection(Of GroupEntry)
        Dim Command As String = "SELECT [Index], [GroupName], [GroupSubName], [GroupTable] FROM [Groups]"
        DBConnection.ExecuteReader(Command)
        Do While DBConnection.DBCursor.Read()
            Dim Index = DBConnection.SecureGetInt32(0)
            Dim Name = DBConnection.SecureGetString(1)
            Dim SubName = DBConnection.SecureGetString(2)
            Dim Table = DBConnection.SecureGetString(3)
            Dim entry As New GroupEntry(Index, Name, SubName, Table)
            Groups.Add(entry)
        Loop
        DBConnection.DBCursor.Close()
        Return Groups
    End Function

    Public Function GetSubGroups(groupName As String) As Collection(Of GroupEntry) Implements IGroupsDao.GetSubGroups
        Dim SubGroups As New Collection(Of GroupEntry)
        Dim Command As String = "SELECT [Index], [GroupName], [GroupSubName], [GroupTable] FROM Groups WHERE GroupName=" & GetDBEntry(groupName) & ";"
        DBConnection.ExecuteReader(Command)
        Do While DBConnection.DBCursor.Read
            SubGroups.Add(ExtractGroupEntry())
        Loop
        DBConnection.DBCursor.Close()
        Return SubGroups
    End Function

    Private Function GetGroup(groupName As String, subGroupName As String) As GroupEntry
        Dim Command As String = "SELECT [Index], [GroupName], [GroupSubName], [GroupTable] FROM Groups WHERE GroupName=" & GetDBEntry(groupName) & " AND Groups.GroupSubName=" & GetDBEntry(subGroupName)
        DBConnection.ExecuteReader(Command)
        If Not DBConnection.DBCursor.HasRows Then
            DBConnection.DBCursor.Close()
            Throw New EntryNotFoundException("Group " & groupName & " and " & subGroupName & " does not exist")
        End If
        DBConnection.DBCursor.Read()
        GetGroup = ExtractGroupEntry()

        If DBConnection.DBCursor.Read Then
            DBConnection.DBCursor.Close()
            Throw New Exception("Database inconsistent")
        End If
        DBConnection.DBCursor.Close()
    End Function

    Private Function ExtractGroupEntry() As GroupEntry
        Dim Index = DBConnection.SecureGetInt32(0)
        Dim Name = DBConnection.SecureGetString(1)
        Dim SubName = DBConnection.SecureGetString(2)
        Dim Table = DBConnection.SecureGetString(3)
        ExtractGroupEntry = New GroupEntry(Index, Name, SubName, Table)
    End Function

    Public Function SubGroupCount(ByVal groupName As String) As Integer Implements IGroupsDao.SubGroupCount
        Dim command As String = "SELECT COUNT([Index]) FROM Groups WHERE GroupName = ?"
        DBConnection.ExecuteReader(command, Enumerable.Repeat(groupName, 1))
        DBConnection.DBCursor.Read()
        Dim count As Integer = DBConnection.SecureGetInt32(0)
        DBConnection.DBCursor.Close()
        If count = 0 Then
            Throw New EntryNotFoundException("Group " & groupName & " does not exist")
        End If
        Return count
    End Function

    ''' <summary>
    ''' Adds a new group consisting of a group and a sub group. Creates the table for the new group and
    ''' also inserts it into the groups table.
    ''' 
    ''' Throws an EntryExistsException if the group already exists.
    ''' </summary>
    ''' <param name="groupName"></param>
    ''' <param name="subGroupName"></param>
    Public Sub AddGroup(groupName As String, subGroupName As String) Implements IGroupsDao.AddGroup
        If GroupExists(groupName, subGroupName) Then
            Throw New EntryExistsException("Group with the given name already exists.")
        End If

        Dim nextTableNumber = CountSubGroups(groupName) + 1

        Dim tableName As String
        If nextTableNumber < 10 Then
            tableName = CreateGroupBaseTableName(groupName) & "0" & nextTableNumber
        Else
            tableName = CreateGroupBaseTableName(groupName) & nextTableNumber
        End If

        CreateNewGroupTable(tableName)

        Dim command = "INSERT INTO Groups (GroupName, GroupSubName, GroupTable) VALUES (" & GetDBEntry(groupName) & ", " & GetDBEntry(subGroupName) & ", " & GetDBEntry(tableName) & ")"
        DBConnection.ExecuteNonQuery(command)
    End Sub

    Private Function CountSubGroups(groupName As String) As Integer
        Dim command = "SELECT COUNT([Index]) FROM Groups WHERE Groups.GroupName=" & GetDBEntry(groupName)

        DBConnection.ExecuteReader(command)
        DBConnection.DBCursor.Read()
        CountSubGroups = DBConnection.SecureGetInt32(0)
        DBConnection.DBCursor.Close()
    End Function

    ''' <summary>
    ''' Creates a new group table with the given name. The method does not perform any checks on the table name.
    ''' </summary>
    ''' <param name="tableName">The name of the table, not checked for illegal characters</param>
    Private Sub CreateNewGroupTable(tableName As String)
        Dim command = "CREATE TABLE [" & tableName & "] ([Index] INTEGER PRIMARY KEY AUTOINCREMENT, [WordIndex] LONG NOT NULL, [Marked] BIT, [Example] TEXT(64), [TestInterval] INT NOT NULL, [Counter] INT NOT NULL, [LastDate] DATETIME NOT NULL, [TestIntervalMain] INT NOT NULL, [CounterMain] INT NOT NULL)"
        DBConnection.ExecuteNonQuery(command)
    End Sub

    ''' <summary>
    ''' Renames a group's name.
    ''' </summary>
    ''' <param name="groupName"></param>
    ''' <param name="newName"></param>
    Public Sub EditGroup(ByVal groupName As String, ByVal newName As String) Implements IGroupsDao.EditGroup
        If GroupExists(newName) Then Throw New EntryExistsException("A group with name " & newName & " already exists.")

        Dim subGroups As Collection(Of GroupEntry) = GetSubGroups(groupName)
        Dim baseTableName = CreateGroupBaseTableName(groupName)
        Dim newBaseTableName = CreateGroupBaseTableName(newName)
        For Each group As GroupEntry In subGroups
            ' Alter the table name
            Dim number = group.Table.Substring(baseTableName.Length)
            Dim newTableName = newBaseTableNAme & number

            Dim renameCommand = CreateRenameTableCommand(group.Table, newTableName)
            DBConnection.ExecuteNonQuery(renameCommand)

            ' Update the entry in the table
            UpdateGroup(group, newName, group.SubGroup, newTableName)
        Next group
    End Sub

    ''' <summary>
    ''' Rename a sub group. Only the entry in the overview is modified, the tables remain untouched.
    ''' </summary>
    ''' <param name="groupName">The group name</param>
    ''' <param name="subGroupName">The sub group name</param>
    ''' <param name="newSubGroupName">The new sub group name</param>
    Public Sub EditSubGroup(ByVal groupName As String, ByVal subGroupName As String, ByVal newSubGroupName As String) Implements IGroupsDao.EditSubGroup
        Dim oldEntry As GroupEntry = GetGroup(groupName, subGroupName)
        If (GroupExists(groupName, newSubGroupName)) Then
            Throw New EntryExistsException("Sub group " & newSubGroupName & " for group " & groupName & " exists.")
        End If
        UpdateGroup(oldEntry, groupName, newSubGroupName, oldEntry.Table)
    End Sub

    ''' <summary>
    ''' Updates the data for an existing group in the group table. Method for internal use only, when called it must be
    ''' clear that the group table exists.
    ''' </summary>
    ''' <param name="group">Entry of an existing group</param>
    ''' <param name="newGroupName">The updated group name, can be the same</param>
    ''' <param name="newSubGroup">The updated sub group name, can be the same</param>
    ''' <param name="newTableName">The updated table name, can be the same</param>
    Private Sub UpdateGroup(group As GroupEntry, newGroupName As String, newSubGroup As String, newTableName As String)
        Dim command As String = "UPDATE Groups SET [GroupName] = ?, [GroupSubName] = ?, [GroupTable] = ? WHERE [Index]= ?"
        Dim parameters = New List(Of String) From {newGroupName, newSubGroup, newTableName, group.Index}
        DBConnection.ExecuteNonQuery(command, parameters)
    End Sub

    Public Function GroupExists(ByVal groupName As String) As Boolean Implements IGroupsDao.GroupExists
        Dim command As String = "SELECT DISTINCT GroupTable FROM Groups WHERE GroupName = ?"
        DBConnection.ExecuteReader(command, Enumerable.Repeat(groupName, 1))
        Dim ret As Boolean
        If DBConnection.DBCursor.HasRows Then
            ret = True
        Else
            ret = False
        End If
        DBConnection.CloseReader()
        Return ret
    End Function

    ''' <summary>
    ''' Checks whether a group with certain names exist. Groups are defined to be unique by their two names.
    ''' </summary>
    ''' <param name="groupName">Group name</param>
    ''' <param name="subGroupName">Sub group name</param>
    ''' <returns>Whether a group exists</returns>
    Private Function GroupExists(groupName As String, subGroupName As String) As Boolean Implements IGroupsDao.GroupExists
        Dim command As String = "SELECT [Index] FROM Groups WHERE Groups.GroupName = ? AND Groups.GroupSubName = ?"
        DBConnection.ExecuteReader(command, New List(Of String) From {groupName, subGroupName})
        GroupExists = DBConnection.DBCursor.HasRows
        DBConnection.DBCursor.Close()
    End Function

    Public Sub DeleteGroup(ByVal groupName As String) Implements IGroupsDao.DeleteGroup
        Dim toDelete As Collection(Of GroupEntry) = GetSubGroups(groupName)
        If toDelete.Count = 0 Then
            Throw New EntryNotFoundException("Group " & groupName & " does not exist")
        End If

        DropGroupTables(toDelete.Select(Function(entry As GroupEntry) entry.Table))
        DeleteGroupEntries(groupName)
    End Sub

    Private Sub DeleteGroupEntries(ByVal groupName As String)
        Dim command = "DELETE FROM Groups WHERE GroupName = ?"
        DBConnection.ExecuteNonQuery(command, Enumerable.Repeat(groupName, 1))
    End Sub

    Public Sub DeleteGroup(ByVal groupName As String, ByVal subGroupName As String) Implements IGroupsDao.DeleteSubGroup
        ' Retrieve original data
        Dim group = GetGroup(groupName, subGroupName)
        Dim subGroups = GetSubGroups(groupName)

        DropGroupTables(Enumerable.Repeat(group.Table, 1))
        DeleteGroupEntries(groupName, subGroupName)

        Dim lastTable As String = Nothing
        Dim started As Boolean = False
        For Each entry As GroupEntry In subGroups
            If Not lastTable Is Nothing Then
                started = True
            End If
            If started Then
                Dim command = CreateRenameTableCommand(entry.Table, lastTable)
                DBConnection.ExecuteNonQuery(command)
            End If
            If entry.Equals(group) Then
                lastTable = entry.Table
            End If
        Next

    End Sub

    Private Sub DropGroupTables(ByRef toDelete As IEnumerable(Of String))
        For Each table As String In toDelete
            Dim command = "DROP TABLE " & table
            DBConnection.ExecuteNonQuery(command)
        Next
    End Sub

    Private Sub DeleteGroupEntries(ByVal groupName As String, ByVal subGroupName As String)
        Dim command = "DELETE FROM Groups WHERE GroupName = ? AND GroupSubName = ?"
        DBConnection.ExecuteNonQuery(command, New List(Of String) From {groupName, subGroupName})
    End Sub

    ''' <summary>
    ''' Swaps two subgroup of a group. The groups change their position in the ordering of subgroups for the given group.
    ''' </summary>
    ''' <param name="groupName"></param>
    ''' <param name="groupSubName1"></param>
    ''' <param name="groupSubName2"></param>
    Public Sub SwapGroups(ByVal groupName As String, ByVal groupSubName1 As String, ByVal groupSubName2 As String) Implements IGroupsDao.SwapGroups
        Dim command As String = ""
        Dim group As GroupEntry

        ' Holen der Daten
        group = GetGroup(groupName, groupSubName1)
        Dim index1 As Integer = group.Index
        Dim table1 As String = group.Table

        group = GetGroup(groupName, groupSubName2)
        Dim index2 As Integer = group.Index
        Dim table2 As String = group.Table

        ' Swap the tables
        CreateRenameTableCommand(table1, table1 & "_")
        CreateRenameTableCommand(table2, table1)
        CreateRenameTableCommand(table1 & "_", table2)

        ' Swap the names
        command = "UPDATE Groups SET [GroupSubName] = ? WHERE [Index] = ?"
        DBConnection.ExecuteNonQuery(command, New List(Of String) From {groupSubName2 & "_", index1})
        DBConnection.ExecuteNonQuery(command, New List(Of String) From {groupSubName1, index2})
        DBConnection.ExecuteNonQuery(command, New List(Of String) From {groupSubName2, index1})
    End Sub

    Private Function CreateRenameTableCommand(existingTable As String, newTableName As String) As String
        If StripSpecialCharacters(newTableName) <> newTableName Then
            Throw New ArgumentException("Table name not stripped")
        End If

        Const renamePattern = "ALTER TABLE [{0}] RENAME TO [{1}]"
        CreateRenameTableCommand = String.Format(renamePattern, existingTable, newTableName)
    End Function

    Private Function CreateGroupBaseTableName(groupName As String) As String
        Return "Group" & StripSpecialCharacters(groupName)
    End Function
End Class
