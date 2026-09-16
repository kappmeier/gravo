''' <summary>
''' Counts the rows added to the target database during a <see cref="DatabaseTransfer"/>.
''' Skips already existing rows with the exception of <c>Groups</c>, which it counts wether added, or not.
''' </summary>
Public Class TransferResult
    Public Property MainEntries As Integer
    Public Property SubEntries As Integer
    Public Property Groups As Integer
    Public Property SubGroups As Integer
    Public Property GroupEntries As Integer

    Friend Sub Add(other As TransferResult)
        MainEntries += other.MainEntries
        SubEntries += other.SubEntries
        Groups += other.Groups
        SubGroups += other.SubGroups
        GroupEntries += other.GroupEntries
    End Sub
End Class
