Imports Gravo
Imports NUnit.Framework
Imports FluentAssertions

''' <summary>
''' Tests the GravoCore/xls/GroupEntry class. Equality and hashing are defined by the
''' group and sub group names only; Index and Table are ignored by both.
''' </summary>
<TestFixture>
Public Class GroupEntryTests

    <Test>
    Public Sub Equals_NothingVersusEmptyStringFields_ReturnsTrue()
        ' VB `=` on strings treats Nothing and "" as equal.
        Dim entryA As New GroupEntry(1, Nothing, Nothing, "table")
        Dim entryB As New GroupEntry(1, "", "", "table")

        entryA.Equals(entryB).Should.Be(True)
    End Sub

    <Test>
    Public Sub GetHashCode_SameConstructorArgs_ReturnsEqualHashCodes()
        Dim entryA As New GroupEntry(1, "group", "sub", "table")
        Dim entryB As New GroupEntry(1, "group", "sub", "table")

        entryA.GetHashCode().Should.Be(entryB.GetHashCode())
    End Sub

    <Test>
    Public Sub GetHashCode_NothingVersusEmptyStringFields_ReturnsEqualHashCodes()
        ' Equals treats Nothing and "" as equal (VB `=`), so equal entries must hash equally.
        ' Both names vary so both normalizations are covered.
        Dim entryA As New GroupEntry(1, Nothing, Nothing, "table")
        Dim entryB As New GroupEntry(1, "", "", "table")

        entryA.GetHashCode().Should.Be(entryB.GetHashCode())
    End Sub

    <Test>
    Public Sub EqualsAndGetHashCode_DifferingIndexAndTableOnly_TreatedAsEqual()
        Dim entryA As New GroupEntry(1, "group", "sub", "table1")
        Dim entryB As New GroupEntry(2, "group", "sub", "table2")

        entryA.Equals(entryB).Should.Be(True)
        entryA.GetHashCode().Should.Be(entryB.GetHashCode())
    End Sub
End Class
