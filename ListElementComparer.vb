Imports System.Collections.ObjectModel  ' Für Collection(Of T)

Public Class ListElementComparer(Of T)
  Inherits Comparer(Of T)

  Dim m_sortColumn As Integer = 0
  Dim m_sorting As System.Windows.Forms.SortOrder = SortOrder.Ascending

  Public Sub New(ByVal sortColumn As Integer)
    Me.SortColumn = sortColumn
  End Sub

  Overrides Function Compare(ByVal x As T, ByVal y As T) As Integer
    If Sorting = SortOrder.None Then Return 0
    If TypeOf x Is ListViewItem Then
      'TODO find better construct for this ;)
      ' sort numbers accurately
      Dim a As Object = x
      Dim b As Object = y
      Dim tmp As ListViewItem = a
      Dim tmp2 As ListViewItem = b
      If SortColumn >= tmp.SubItems.Count Then Throw New IndexOutOfRangeException("No column " & SortColumn & " in List")
      If tmp.SubItems(SortColumn).Text.ToUpper = tmp2.SubItems(SortColumn).Text.ToUpper Then Return 0
      If tmp.SubItems(SortColumn).Text.ToUpper < tmp2.SubItems(SortColumn).Text.ToUpper Then Return IIf(Sorting = SortOrder.Ascending, -1, 1)
      If tmp.SubItems(SortColumn).Text.ToUpper > tmp2.SubItems(SortColumn).Text.ToUpper Then Return IIf(Sorting = SortOrder.Ascending, 1, -1)
    End If
    Throw New Exception("Only ListViewItem objects can be sortetd.")
  End Function

  Public Property SortColumn() As Integer
    Get
      Return m_sortColumn
    End Get
    Set(ByVal value As Integer)
      m_sortColumn = value
    End Set
  End Property

  Public Property Sorting() As System.Windows.Forms.SortOrder
    Get
      Return m_sorting
    End Get
    Set(ByVal value As System.Windows.Forms.SortOrder)
      m_sorting = value
    End Set
  End Property
End Class
