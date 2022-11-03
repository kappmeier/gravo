Imports System.Collections.ObjectModel
Imports Gravo.localization

Public Class TestSelect
    ''' <summary>
    ''' Data access for groups.
    ''' </summary>
    Dim GroupsDao As IGroupsDao
    Dim groupEntry As GroupEntry
    ''Dim group As GroupDto = Nothing
    ''' <summary>
    ''' Loading group data.
    ''' </summary>
    Dim GroupDao As IGroupDao

    Public Sub New()
        InitializeComponent()

        Dim db As New SQLiteDataBaseOperation
        db.Open(DBPath)

        GroupsDao = New GroupsDao(db)
        GroupDao = New GroupDao(db)

        ' Gruppen in die Liste einfügen
        cmbGroup.Items.Clear()
        Dim groupNames As Collection(Of String) = GroupsDao.GetGroups()
        For Each groupName As String In groupNames
            cmbGroup.Items.Add(groupName)
        Next
        If groupNames.Count > 0 Then cmbGroup.SelectedIndex = 0
    End Sub

    Private Sub TestSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Center()
        LocalizationChanged()
    End Sub

    Private Sub Center()
        Me.Left = Me.Owner.Left + Me.Owner.Width / 2 - Me.Width / 2
        Me.Top = Me.Owner.Top + Me.Owner.Height / 2 - Me.Height / 2
        If Me.Top < 0 Then Me.Top = 0
        If Me.Left < 0 Then Me.Left = 0
    End Sub

    ' Lokalisierung
    Public Overrides Sub LocalizationChanged()
        Me.Text = GetLoc.GetText(TEST_SELECT_TITLE)
        lblGroup.Text = GetLoc.GetText(TEST_SELECT_GROUP) & ":"
        lblSubGroup.Text = GetLoc.GetText(TEST_SELECT_SUBGROUP) & ":"
        chkRandomOrder.Text = GetLoc.GetText(TEST_SELECT_RANDOM_ORDER)
        chkTestDirection.Text = GetLoc.GetText(TEST_SELECT_TEST_DIRECTION)
        chkTestMarked.Text = GetLoc.GetText(TEST_SELECT_ONLY_MARKED)
        chkTestPhrases.Text = GetLoc.GetText(TEST_SELECT_PHRASES)
        cmdOK.Text = GetLoc.GetText(BUTTON_OK)
        cmdCancel.Text = GetLoc.GetText(BUTTON_CANCEL)
    End Sub

    Private Sub cmbGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroup.SelectedIndexChanged
        ' Untergruppen in die andere Liste eintragen
        cmbSubGroup.Items.Clear()     ' Liste leeren
        Dim subGroups As Collection(Of GroupEntry) = GroupsDao.GetSubGroups(cmbGroup.SelectedItem)
        For Each entry As GroupEntry In subGroups
            cmbSubGroup.Items.Add(entry.SubGroup)
        Next
        If cmbSubGroup.Items.Count > 0 Then cmbSubGroup.SelectedIndex = 0
    End Sub

    ''' <summary>
    ''' Returns the currently selected group.
    ''' </summary>
    ''' <returns>The selected group</returns>
    Public ReadOnly Property SelectedGroup() As GroupEntry
        Get
            Return groupEntry
        End Get
    End Property

    Private Sub cmbSubGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSubGroup.SelectedIndexChanged
        groupEntry = GroupsDao.GetGroup(cmbGroup.SelectedItem, cmbSubGroup.SelectedItem)
        ' TODO: not required to load all data just to show the count
        Dim group = GroupDao.Load(groupEntry)
        Dim t As String = group.WordCount & IIf(group.WordCount = 1, " Vokabel abzufragen.", " Vokabeln abzufragen.")
        lblWordCount.Text = t
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        DialogResult = System.Windows.Forms.DialogResult.OK
        Hide()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        groupEntry = Nothing
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

    Public Property QueryLanguage() As QueryLanguage
        Get
            Return If(chkTestDirection.Checked, QueryLanguage.TargetLanguage, QueryLanguage.TargetLanguage)
        End Get
        Set(ByVal value As QueryLanguage)
            Select Case value
                Case QueryLanguage.TargetLanguage
                    chkTestDirection.Checked = True
                Case QueryLanguage.OriginalLanguage
                    chkTestDirection.Checked = False
                Case Else
                    Throw New ArgumentException("Direction " & value & " not supported")
            End Select
        End Set
    End Property

    Public Property TestPhrases() As Boolean
        Get
            Return chkTestPhrases.Checked
        End Get
        Set(ByVal value As Boolean)
            chkTestPhrases.Checked = value
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