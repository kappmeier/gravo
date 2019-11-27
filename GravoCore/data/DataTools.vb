Imports System.Collections.ObjectModel
Imports Gravo
''' <summary>
''' Collection of shared methods that require DAO access, but on a higher abstraction level.
''' </summary>
Public Class DataTools

    Public Shared Function WordCount(groupsDao As IGroupsDao, groupDao As IGroupDao, ByVal groupName As String) As Integer
        Dim subGroups = groupsDao.GetSubGroups(groupName)
        Dim counter As Integer = 0
        For Each group As GroupEntry In subGroups
            Dim groupData = groupDao.Load(group)
            counter += groupData.WordCount
        Next group
        Return counter
    End Function

    Public Shared Function UsedLanguagesCount(groupsDao As IGroupsDao, groupDao As IGroupDao, ByVal groupName As String) As Integer
        Dim subGroups = groupsDao.GetSubGroups(groupName)
        'Dim command As String = "SELECT GroupTable FROM Groups WHERE GroupName=" & GetDBEntry(groupName) & ";"
        'Dim tables As New Collection(Of String)
        'DBConnection.ExecuteReader(command)
        'While DBConnection.DBCursor.Read()
        '    tables.Add(DBConnection.SecureGetString(0))
        'End While
        'DBConnection.DBCursor.Close()

        Dim usedLanguages As SortedList(Of String, String) = New SortedList(Of String, String)
        For Each groupEntry As GroupEntry In subGroups
            '            Dim group As New xlsGroup(table)
            '            group.DBConnection = DBConnection
            Dim groupLanguages As ICollection(Of String) = groupDao.GetLanguages(groupEntry)
            'Dim groupLanguages As Collection(Of String) = group.GetLanguages
            For Each language As String In groupLanguages
                If Not usedLanguages.ContainsKey(language) Then usedLanguages.Add(language, language)
            Next language
        Next groupEntry
        Return usedLanguages.Count
    End Function

    Public Shared Function GetOrCreateMainEntry(dictionaryDao As IDictionaryDao, mainEntry As String, language As String, mainLanguage As String) As MainEntry
        Try
            GetOrCreateMainEntry = dictionaryDao.GetMainEntry(mainEntry, language, mainLanguage)
        Catch ex As EntryNotFoundException
            GetOrCreateMainEntry = dictionaryDao.AddEntry(mainEntry, language, mainLanguage)
        End Try
    End Function
End Class
