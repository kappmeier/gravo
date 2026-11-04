Imports System.Collections.ObjectModel

Public Interface IGroupsDao
    ''' <summary>
    ''' Returns the names of all major groups.
    ''' 
    ''' Ordered alphabetically by the name of the groups, ignoring case. Sort order ignores special characters.
    ''' </summary>
    ''' <returns></returns>
    Function GetGroups() As Collection(Of String)

    ''' <summary>
    ''' Returns all groups as full entities.
    ''' 
    ''' Ordered by the sub group index, grouped per group.
    ''' </summary>
    ''' <returns></returns>
    Function GetAllGroups() As Collection(Of GroupEntry)

    ''' <summary>
    ''' Returns all groups belonging to a major group entry as full entities.
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' The subgroups are ordered by the group index.
    ''' </remarks>
    ''' <returns></returns>
    Function GetSubGroups(ByVal groupName As String) As ICollection(Of GroupEntry)

    Function GetGroup(ByVal groupName As String, ByVal subGroupName As String) As GroupEntry

    Function SubGroupCount(ByVal groupName As String) As Integer

    Sub AddGroup(ByVal groupName As String, ByVal subGroupName As String)

    ''' <summary>
    ''' Renames a group together with its sub groups.
    ''' </summary>
    ''' <param name="groupName">The group to rename.</param>
    ''' <param name="newName">The new name of the group.</param>
    ''' <exception cref="InputException">When the new name holds a forbidden character.</exception>
    ''' <exception cref="EntryNotFoundException">When the group does not exist.</exception>
    ''' <exception cref="EntryExistsException">When a group with the new name exists already.</exception>
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
