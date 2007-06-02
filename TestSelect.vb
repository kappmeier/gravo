Imports System.Collections.ObjectModel
Imports Gravo2k7.localization

Public Class TestSelect
  Dim db As New AccessDatabaseOperation                 ' Datenbankoperationen für Microsoft Access Datenbanken
  Dim groups As New xlsGroups
  Dim group As xlsGroup = Nothing

  Public Sub New()
    ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
    InitializeComponent()

    ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
    db.Open(Application.StartupPath() & "\voc.mdb")     ' Datenbank öffnen
    groups.DBConnection = db

    ' Gruppen in die Liste einfügen
    cmbGroup.Items.Clear()
    Dim groupNames As Collection(Of String) = groups.GetGroups()
    For Each groupName As String In groupNames
      cmbGroup.Items.Add(groupName)
    Next
    If groupNames.Count > 0 Then cmbGroup.SelectedIndex = 0
  End Sub

  Private Sub TestSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ' Position
    Me.Left = Me.Owner.Left + Me.Owner.Width / 2 - Me.Width / 2
    Me.Top = Me.Owner.Top + Me.Owner.Height / 2 - Me.Height / 2
    If Me.Top < 0 Then Me.Top = 0
    If Me.Left < 0 Then Me.Left = 0
    LocalizationChanged()
  End Sub

  ' Lokalisierung
  Public Overrides Sub LocalizationChanged()
    Me.Text = GetLoc.GetText(TEST_SELECT_TITLE)
    lblGroup.Text = GetLoc.GetText(TEST_SELECT_GROUP) & ":"
    lblSubGroup.Text = GetLoc.GetText(TEST_SELECT_SUBGROUP) & ":"
    chkRandomOrder.Text = GetLoc.GetText(TEST_SELECT_RANDOM_ORDER)
    chkTestDirection.Text = GetLoc.GetText(TEST_SELECT_TEST_DIRECTION)
    chkTestMarked.Text = GetLoc.GetText(TEST_SELECT_ONLY_MARKED)
    chkTestSetPhrases.Text = GetLoc.GetText(TEST_SELECT_PHRASES)
    cmdOK.Text = GetLoc.GetText(BUTTON_OK)
    cmdCancel.Text = GetLoc.GetText(BUTTON_CANCEL)
  End Sub

  Private Sub cmbGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroup.SelectedIndexChanged
    ' Untergruppen in die andere Liste eintragen
    cmbSubGroup.Items.Clear()     ' Liste leeren
    Dim subGroups As Collection(Of xlsGroupEntry) = groups.GetSubGroups(cmbGroup.SelectedItem)
    For Each entry As xlsGroupEntry In subGroups
      cmbSubGroup.Items.Add(entry.SubGroup)
    Next
    If cmbSubGroup.Items.Count > 0 Then cmbSubGroup.SelectedIndex = 0
  End Sub

  Public ReadOnly Property SelectedGroup() As xlsGroup
    Get
      Return group
    End Get
  End Property

  Private Sub cmbSubGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSubGroup.SelectedIndexChanged
    group = groups.GetGroup(cmbGroup.SelectedItem, cmbSubGroup.SelectedItem)
    Dim t As String = group.WordCount & IIf(group.WordCount = 1, " Vokabel abzufragen.", " Vokabeln abzufragen.")
    lblWordCount.Text = t
  End Sub

  Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    DialogResult = System.Windows.Forms.DialogResult.OK
    Hide()
  End Sub

  Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    DialogResult = System.Windows.Forms.DialogResult.Cancel
    group = Nothing
  End Sub

  Public Property LastGroup() As String
    Get
      Return cmbGroup.SelectedItem
    End Get
    Set(ByVal value As String)
      ' Versuche, die Gruppe auszuwählen
      If cmbGroup.Items.IndexOf(value) >= 0 Then
        cmbGroup.SelectedIndex = cmbGroup.Items.IndexOf(value)
      Else
        If cmbGroup.Items.Count > 0 Then cmbGroup.SelectedIndex = 0
      End If
    End Set
  End Property

  Public Property LastSubGroup() As String
    Get
      Return cmbSubGroup.SelectedItem
    End Get
    Set(ByVal value As String)
      If cmbSubGroup.Items.IndexOf(value) >= 0 Then
        cmbSubGroup.SelectedIndex = cmbSubGroup.Items.IndexOf(value)
      End If
    End Set
  End Property

  Public Property TestFormerLanguage() As Boolean
    Get
      Return chkTestDirection.Checked
    End Get
    Set(ByVal value As Boolean)
      chkTestDirection.Checked = value
    End Set
  End Property

  Public Property TestSetPhrases() As Boolean
    Get
      Return chkTestSetPhrases.Checked
    End Get
    Set(ByVal value As Boolean)
      chkTestSetPhrases.Checked = value
    End Set
  End Property

  Public Property TestMarked() As Boolean
    Get
      Return chkTestMarked.Checked
    End Get
    Set(ByVal value As Boolean)
      chkTestMarked.Checked = value
    End Set
  End Property

  Public Property RandomOrder() As Boolean
    Get
      Return chkRandomOrder.Checked
    End Get
    Set(ByVal value As Boolean)
      chkRandomOrder.Checked = value
    End Set
  End Property
End Class