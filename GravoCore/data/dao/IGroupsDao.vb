Imports System.Collections.ObjectModel

Public Interface IGroupsDao
    ''' <summary>
    ''' Returns the names of all major groups.
    ''' 
    ''' TODO: Define the order the strings have to be.
    ''' </summary>
    ''' <returns></returns>
    Function GetGroups() As Collection(Of String)

    ''' <summary>
    ''' Returns all groups as full entities.
    ''' 
    ''' TODO: Define the order the strings have to be.
    ''' </summary>
    ''' <returns></returns>
    Function GetAllGroups() As Collection(Of GroupEntry)

    ''' <summary>
    ''' Returns all groups belonging to a major group entry as full entities.
    ''' 
    ''' TODO: Define the order the strings have to be.
    ''' </summary>
    ''' <returns></returns>
    Function GetSubGroups(ByVal groupName As String) As ICollection(Of GroupEntry)

    Function GetGroup(ByVal groupName As String, ByVal subGroupName As String) As GroupEntry

    Function SubGroupCount(ByVal groupName As String) As Integer

    Sub AddGroup(ByVal groupName As String, ByVal subGroupName As String)

    Sub EditGroup(ByVal groupName As String, ByVal newName As String)

    Sub EditSubGroup(ByVal groupName As String, ByVal subGroupName As String, ByVal newSubGroupName As String)

    Sub SwapGroups(ByVal groupName As String, ByVal groupSubName1 As String, ByVal groupSubName2 As String)

    Sub DeleteGroup(ByVal groupName As String)

    Sub DeleteSubGroup(ByVal groupName As String, ByVal subGroupName As String)

    ''' <summary>
    ''' Checks whether a main group with the given name exists.
    ''' </summary>
    ''' <param name="groupName"></param>
    ''' <returns></returns>
    Function GroupExists(ByVal groupName As String) As Boolean

    Function GroupExists(groupName As String, subGroupName As String) As Boolean
End Interface
