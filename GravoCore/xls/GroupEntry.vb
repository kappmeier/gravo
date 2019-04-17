Imports Gravo

Public Class GroupEntry
    Private ReadOnly _Index As Integer
    Private ReadOnly _GroupName As String
    Private ReadOnly _SubName As String
    Private ReadOnly _Table As String

    Public Sub New(index As Integer, name As String, subName As String, table As String)
        Me._Index = index
        Me._GroupName = name
        Me._SubName = subName
        Me._Table = table
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim entry = TryCast(obj, GroupEntry)
        Return entry IsNot Nothing AndAlso
               _GroupName = entry._GroupName AndAlso
               _SubName = entry._SubName
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return (_GroupName, _SubName).GetHashCode()
    End Function

    Public ReadOnly Property Index() As Integer
        Get
            Return _Index
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return _GroupName
        End Get
    End Property

    Public ReadOnly Property SubGroup() As String
        Get
            Return _SubName
        End Get
    End Property

    ''' <summary>
    ''' TODO: should be removed or at least protected?
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Table() As String
        Get
            Return _Table
        End Get
    End Property

    Public Overrides Function ToString() As String
        Dim format = "GroupEntry: {{{0}, {1}, {2}}}"
        Return String.Format(format, _GroupName, _SubName, _Table)
    End Function
End Class
