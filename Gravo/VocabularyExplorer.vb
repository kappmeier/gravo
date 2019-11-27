Imports Gravo.localization
Imports System.Collections.ObjectModel  ' Für Collection(Of T)
Imports System.Linq

Public Class VocabularyExplorer
    Enum ListViewStyleEnum
        WordSubEntry
        WordEntry
        WordEntryGroup
        WordEntrySubGroup
        MainLanguage
        Language
        Dictionary
        Groups
    End Enum

    Dim m_listviewstyle As ListViewStyleEnum
    Dim ListView1Initialized As Boolean = False

    Enum ColumnName
        GroupsName
        GroupsSubGroup
        GroupsCountEntry
        EntryPre
        EntryWord
        EntryPost
        EntryMeaning
        EntryType
        EntryExtendedInfo
        EntryIrregular
        GroupEntryMarked
        GroupEntrySubgroup
        DictMainLanguage
        DictLanguage
        DictCountMainEntry
        DictCountEntrys
        GroupCountLanguages
    End Enum

    Dim range() As String
    Dim rangeStart As String

    Private Const NODE_LEVEL_BASE As Integer = 0

    Private Const NODE_LEVEL_DICTIONARY As Integer = NODE_LEVEL_BASE
    Private Const NODE_LEVEL_MAIN_LANGUAGE As Integer = NODE_LEVEL_DICTIONARY + 1
    Private Const NODE_LEVEL_LANGUAGE As Integer = NODE_LEVEL_MAIN_LANGUAGE + 1
    ''' <summary>
    ''' Nodes containing main entries.
    ''' </summary>
    Private Const NODE_LEVEL_ENTRY As Integer = NODE_LEVEL_LANGUAGE + 2

    Private Const NODE_LEVEL_GROUPS As Integer = NODE_LEVEL_BASE
    Private Const NODE_LEVEL_GROUP As Integer = NODE_LEVEL_GROUPS + 1
    Private Const NODE_LEVEL_SUBGROUP As Integer = NODE_LEVEL_GROUP + 1
    Private Const NODE_LEVEL_GROUP_ENTRY As Integer = NODE_LEVEL_SUBGROUP + 1

    Dim db As New SQLiteDataBaseOperation()
    ''' <summary>
    ''' Data access for groups.
    ''' </summary>
    Dim GroupsDao As IGroupsDao
    Dim GroupDao As IGroupDao
    Dim DictionaryDao As IDictionaryDao
    Dim properties As Properties
    Dim wordTypes As WordTypes

    Dim listUpdate As Boolean = True
    Dim listSort As System.Windows.Forms.SortOrder = SortOrder.Ascending
    Dim PanelViewItems As New Collection(Of System.Windows.Forms.ToolStripMenuItem)

    Enum PanelViews
        DefaultView
        Search
        Input
        Multi
    End Enum

    Dim currentPanel As PanelViews = PanelViews.DefaultView

    Public Sub New()
        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        db.Open(DBPath)     ' Datenbank öffnen
        GroupsDao = New GroupsDao(db)
        GroupDao = New GroupDao(db)
        DictionaryDao = New DictionaryDao(db)
        Dim propertiesDao As IPropertiesDao = New PropertiesDao(db)
        properties = propertiesDao.LoadProperties()
        WordTypes = propertiesDao.LoadWordTypes()

        ' Lade die Collections
        PanelViewItems.Add(PanelViewDefaultMenuItem)
        PanelViewItems.Add(PanelViewInputMenuItem)
        PanelViewItems.Add(PanelViewSearchMenuItem)
        PanelViewItems.Add(PanelViewMultiMenuItem)

        PanelWordInfo.Dock = DockStyle.Fill
        PanelMultiEdit.Dock = DockStyle.Fill

        ' Set number of chars per field with respect to the database properties
        txtPre.MaxLength = properties.DictionaryWordsMaxLengthPre
        txtPost.MaxLength = properties.DictionaryWordsMaxLengthPost
        txtWord.MaxLength = properties.DictionaryWordsMaxLengthWord
        txtMeaning.MaxLength = properties.DictionaryWordsMaxLengthMeaning
        txtAdditionalTargetLangInfo.MaxLength = properties.DictionaryWordsMaxLengthAdditionalTargetLangInfo
        txtMainEntry.MaxLength = properties.DictionaryMainMaxLengthWordEntry
    End Sub

    Private Sub VocabularyExplorer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Benutzeroberfläche einrichten
        LoadTree()

        txtMainEntry.BackColor = cmdChangeWord.BackColor

        LocalizationChanged()
    End Sub

    ' Lokalisierung
    Public Overrides Sub LocalizationChanged()
        Me.Text = GetLoc.GetText(EXPLORER_TITLE)

        TreeView.Nodes(0).Text = GetLoc.GetText(TREE_DICTIONARY)
        TreeView.Nodes(1).Text = GetLoc.GetText(TREE_GROUPS)

        Dim i As Integer
        For i = 0 To GetColumnCount(ListViewStyle) - 1
            ListView.Columns.Item(i).Text = GetColumnText(GetColumn(i, ListViewStyle))
        Next i

        lblPre.Text = GetLoc.GetText(WORDS_PRE) & ":"
        lblPost.Text = GetLoc.GetText(WORDS_POST) & ":"
        lblWord.Text = GetLoc.GetText(WORDS_WORD) & ":"
        lblMeaning.Text = GetLoc.GetText(WORDS_MEANING) & ":"
        lblAdditionalTargetLangInfo.Text = GetLoc.GetText(WORDS_ADDITIONAL_INFO) & ":"
        lblMainEntry.Text = GetLoc.GetText(WORDS_MAIN_ENTRY) & ":"
        lblWordType.Text = GetLoc.GetText(WORDS_WORD_TYPE) & ":"
        lblLanguage.Text = GetLoc.GetText(WORDS_LANGUAGE) & ":"
        lblMainLanguage.Text = GetLoc.GetText(WORDS_MAIN_LANGUAGE) & ":"

        chkMarked.Text = GetLoc.GetText(WORDS_MARKED)
        chkIrregular.Text = GetLoc.GetText(WORDS_IRREGULAR)
        chkAddToGroup.Text = GetLoc.GetText(WORDS_DIRECT_ADD)

        chkEnableMultiPre.Text = GetLoc.GetText(WORDS_PRE) & ":"
        chkEnableMultiPost.Text = GetLoc.GetText(WORDS_POST) & ":"
        chkEnableMultiWord.Text = GetLoc.GetText(WORDS_WORD) & ":"
        chkEnableMultiMeaning.Text = GetLoc.GetText(WORDS_MEANING) & ":"
        chkEnableMultiAdditionalTargetLangInfo.Text = GetLoc.GetText(WORDS_ADDITIONAL_INFO) & ":"
        chkEnableMultiWordType.Text = GetLoc.GetText(WORDS_WORD_TYPE) & ":"
        chkEnableMultiMarked.Text = GetLoc.GetText(WORDS_MARKED) & ":"
        chkEnableMultiMainEntry.Text = GetLoc.GetText(WORDS_MAIN_ENTRY) & ":"
        chkEnableMultiIrregular.Text = GetLoc.GetText(WORDS_IRREGULAR) & ":"

        chkMultiMarked.Text = GetLoc.GetText(WORDS_MARKED)
        chkMultiIrregular.Text = GetLoc.GetText(WORDS_IRREGULAR)

        cmdAdd.Text = GetLoc.GetText(BUTTON_ADD)
        cmdChangeWord.Text = GetLoc.GetText(BUTTON_CHANGE)
        cmdMultiChange.Text = GetLoc.GetText(BUTTON_CHANGE)

        PanelsMenu.Text = GetLoc.GetText(EXPLORER_MENU_PANELS)
        PanelViewDefaultMenuItem.Text = GetLoc.GetText(EXPLORER_MENU_PANELS_DEFAULT)
        PanelViewInputMenuItem.Text = GetLoc.GetText(EXPLORER_MENU_PANELS_WORD_INPUT)
        PanelViewSearchMenuItem.Text = GetLoc.GetText(EXPLORER_MENU_PANELS_SEARCH)
        PanelViewMultiMenuItem.Text = GetLoc.GetText(EXPLORER_MENU_PANELS_MULTI_EDIT)

        Dim typeSelected As Integer = lstWordType.SelectedIndex
        Dim typeMultiSelected As Integer = lstWordType.SelectedIndex
        lstWordType.Items.Clear()
        lstMultiWordType.Items.Clear()
        For Each type As String In wordTypes.GetSupportedWordTypes()
            Dim dictionaryCode = "WORD_TYPE_" & type.ToUpper
            lstWordType.Items.Add(GetLoc.GetText(dictionaryCode))
            lstMultiWordType.Items.Add(GetLoc.GetText(dictionaryCode))
        Next type
        If lstWordType.Items.Count > 0 Then
            lstWordType.SelectedIndex = 0
            lstMultiWordType.SelectedIndex = 0
        End If
        If typeSelected <> -1 Then lstWordType.SelectedIndex = typeSelected
        If typeMultiSelected <> -1 Then lstMultiWordType.SelectedIndex = typeMultiSelected
    End Sub

    ' Tree-Funktionen
    Private Sub LoadTree()
        Dim tvFirst As TreeNode
        Dim tvRoot As TreeNode
        Dim tvNode As TreeNode

        TreeView.BeginUpdate()
        tvRoot = TreeView.Nodes.Add(GetLoc.GetText(TREE_DICTIONARY))
        tvFirst = tvRoot

        ' add dictionary entries
        For Each mainLanguage As String In DictionaryDao.DictionaryMainLanguages()
            tvRoot = tvRoot.Nodes.Add(mainLanguage)
            Dim languages As Collection(Of String) = DictionaryDao.DictionaryLanguages(mainLanguage)
            Dim i As Integer
            Dim tvLang As TreeNode
            For i = 0 To languages.Count - 1
                tvLang = tvRoot.Nodes.Add(languages.Item(i)) 'root for each language
                ' create temporary child node
                tvNode = tvLang.Nodes.Add("temp")
            Next i
        Next mainLanguage
        tvRoot = TreeView.Nodes.Add(GetLoc.GetText(TREE_GROUPS))

        ' add group entries
        For Each group As String In GroupsDao.GetGroups()
            tvNode = tvRoot.Nodes.Add(group)
            For Each subGroup As GroupEntry In GroupsDao.GetSubGroups(group)
                Dim tv As TreeNode = tvNode.Nodes.Add(subGroup.SubGroup)
                If GroupDao.Load(subGroup).WordCount > 0 Then
                    tv.Nodes.Add("temp")
                End If
            Next subGroup
        Next group
        TreeView.EndUpdate()
        TreeView.SelectedNode = tvFirst
    End Sub

    Private Sub TreeView_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles TreeView.AfterLabelEdit
        Dim tvSelectedNode As TreeNode = TreeView.SelectedNode
        Dim oldName As String = tvSelectedNode.Text
        Dim newName As String = Trim(e.Label)
        If newName = "" Or (newName = oldName) Then
            e.CancelEdit = True
            Exit Sub
        End If

        If GetBaseFromNode(tvSelectedNode) = GetLoc.GetText(TREE_DICTIONARY) Then
            Select Case tvSelectedNode.Level
                Case NODE_LEVEL_ENTRY
                    Dim oldEntry As MainEntry = DictionaryDao.GetMainEntry(oldName, GetLanguageFromNode(), GetMainLanguageFromNode())
                    Try
                        ' Den Eintrag im Hauptverzeichnis ändern
                        Dim updatedMainEntry = DictionaryDao.ChangeMainEntry(oldEntry, newName)
                        ' Alle Einträge für das Wort im Unterverzeichnis ändern, falls es welche gibt
                        DictionaryDao.AdaptSubEntries(updatedMainEntry, oldName)
                        ' TODO: update the words also in the list
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Exclamation, """" & oldName & """ konnte nicht umbenannt werden.")
                        e.CancelEdit = True
                    End Try
                Case NODE_LEVEL_LANGUAGE + 1
                    e.CancelEdit = True
                Case Else
                    MsgBox("Änderungen werden nicht übernommen", MsgBoxStyle.Exclamation, "Warunung")
                    e.CancelEdit = True
            End Select
        ElseIf IsGroupNode() Then
            Select Case tvSelectedNode.Level
                Case NODE_LEVEL_GROUP
                    GroupsDao.EditGroup(oldName, newName)
                Case NODE_LEVEL_SUBGROUP
                    GroupsDao.EditSubGroup(GetGroupFromNode(), oldName, newName)
                Case NODE_LEVEL_GROUP_ENTRY
                    Dim item As ListViewItem = ListView.Items.Item(0)
                    Dim testWord As TestWord = item.Tag
                    Dim word As WordEntry = testWord.WordEntry
                    Dim nameUpdate As New IDictionaryDao.UpdateData With {
                        .Word = newName
                    }
                    Try
                        Dim updatedWord = DictionaryDao.ChangeEntry(word, nameUpdate)
                        item.SubItems(GetColumnIndex(ColumnName.EntryWord)).Text = updatedWord.word
                        ' TODO: update test word in item.tag after update and also the texts in the list
                        Throw New Exception()
                    Catch ex As EntryExistsException
                        MsgBox("Eintrag existiert bereits.")
                        e.CancelEdit = True
                    End Try
            End Select
        Else
            MsgBox("Änderungen werden nicht übernommen", MsgBoxStyle.Exclamation, "Warnung")
            e.CancelEdit = True
        End If
    End Sub

    Private Sub TreeView_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView.AfterSelect
        'Listview aktualisieren
        'LoadListView()
        listUpdate = True
        SetView()
    End Sub

    Private Sub TreeView_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeView.BeforeExpand
        ' Einträge nachladen
        Dim tvNode As TreeNode = e.Node
        Dim tvSub As TreeNode
        Dim i As Integer

        ' Keine Zahlen! "0..9"
        Dim a() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

        If GetBaseFromNode(tvNode) = GetLoc.GetText(TREE_DICTIONARY) Then
            Select Case tvNode.Level
                Case NODE_LEVEL_LANGUAGE
                    ' Nachladen der Vokabeln für diese Sprache und "temp" löschen
                    ' Erzeuge Unter-Knoten von A - Z und 0..9
                    tvNode.Nodes.Clear()
                    For Each character As String In a
                        tvSub = tvNode.Nodes.Add(character)
                        If DictionaryDao.WordCount(tvNode.Text, GetMainLanguageFromNode(tvNode), character) > 0 Then
                            tvSub.Nodes.Add("temp")
                        End If
                    Next
                Case NODE_LEVEL_LANGUAGE + 1
                    ' Get all main word entries starting with this letter
                    Dim words As ICollection(Of MainEntry) = DictionaryDao.GetMainEntries(GetLanguageFromNode(tvNode), GetMainLanguageFromNode(tvNode), tvNode.Text)
                    tvNode.Nodes.Clear()

                    Dim distinctWords As IEnumerable(Of String) = words.Select(Of String)(Function(t) t.Word).Distinct()

                    For Each word As String In distinctWords
                        tvSub = tvNode.Nodes.Add(word)
                    Next word
            End Select
        Else
            ' Gruppen wurden ausgewählt
            If tvNode.Level = NODE_LEVEL_SUBGROUP Then
                tvNode.Nodes.Clear()
                ' Laden der Gruppeneinträge
                Dim groupEntry As GroupEntry = GroupsDao.GetGroup(GetGroupFromNode(tvNode), GetSubGroupFromNode(tvNode))
                Dim groupData As GroupDto = GroupDao.Load(groupEntry)
                For Each entry As TestWord In groupData.Entries
                    tvSub = tvNode.Nodes.Add(entry.Word)
                    tvSub.Tag = entry
                Next entry
            End If
        End If
    End Sub

    Private Sub ListView_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles ListView.ColumnClick
        Static lastSortedColumn As Integer = -1

        Dim list As New List(Of ListViewItem)
        For Each item As ListViewItem In ListView.Items
            list.Add(item)
        Next item

        If lastSortedColumn <> e.Column Then
            ' auf jeden fall sortieren, aufsteigend
            listSort = SortOrder.Ascending
            Dim comp As New ListElementComparer(Of ListViewItem)(e.Column)
            comp.Sorting = listSort
            list.Sort(comp)
            lastSortedColumn = e.Column
        Else
            ' die gleiche spalte nochmal
            If listUpdate = False Then
                list.Reverse()
            Else
                Dim comp As New ListElementComparer(Of ListViewItem)(e.Column)
                comp.Sorting = listSort
                list.Sort(comp)
                listUpdate = False
            End If
        End If

        ' update der sortierrichtung
        If listSort = SortOrder.Ascending Then
            listSort = SortOrder.Descending
        ElseIf listSort = SortOrder.Descending Then
            listSort = SortOrder.Ascending
        End If

        ListView.Items.Clear()
        Dim i As Integer
        For i = 0 To list.Count - 1
            AddRange(list.Item(i))
        Next i
    End Sub

    ' List-Funktionen
    Private Sub MultiSelection()
        ' Falls Standard-Ansicht aktiviert ist, neu zeichnen lassen da evtl. gewechselt werden muß
        If currentPanel = PanelViews.DefaultView Then LoadPanel()
    End Sub

    Private Sub ListView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView.SelectedIndexChanged
        MultiSelection()
        If ListView.SelectedIndices.Count = 0 Then Exit Sub
        ' Lade die Daten in die Textfelder
        Dim selectedNode As TreeNode = TreeView.SelectedNode
        Dim item As ListViewItem = ListView.Items.Item(ListView.SelectedIndices.Item(0))
        If IsWordEntryNode() Then
            If ListView1.Visible Then
                Me.ListView1.Visible = False
                PanelWordInfoInner.Top = 0
            End If
            ' Es handelt sich um einen Knoten, zu dem Vokabel-Informationen angezeigt werden sollen.
            ' Der zugehörige Index des Haupteintrages kann aus der item.Tag Eigenschaft geholt werden
            If IsGroupNode() Then
                Dim wordEntry As TestWord = item.Tag
                ShowWordInfo(wordEntry)

            Else
                Dim wordEntry As WordEntry = item.Tag
                ShowWordInfo(wordEntry)

            End If
        ElseIf IsLanguageNode() Then
            If ListView1.Visible = False Then
                Me.ListView1.Visible = True
                PanelWordInfoInner.Top = ListView1.Height
            End If
            Dim words As ICollection(Of WordEntry) = DictionaryDao.GetWordsAndSubWords(item.SubItems(0).Text, Me.GetLanguageFromNode(), Me.GetMainLanguageFromNode())
            ListView1.Items.Clear()
            Me.SetUpListViewColumns2(ListViewStyleEnum.WordEntry)
            For Each word As WordEntry In words
                AddDictionaryEntryToList2(word)
            Next word
            If ListView1.Items.Count > 0 Then
                ListView1.SelectedIndices.Add(0)
            End If
        Else
            If ListView1.Visible Then
                Me.ListView1.Visible = False
                PanelWordInfoInner.Top = 0
            End If
            Exit Sub
        End If
    End Sub

    ' Ansicht anpassen
    Private Sub SetView()
        LoadPanel()
        LoadListView()
    End Sub

    ' Liste anpassen
    Private Sub LoadListView()
        ListView.Items.Clear()

        If GetBaseFromNode() = GetLoc.GetText(TREE_DICTIONARY) Then
            LoadListViewDict()
        ElseIf GetBaseFromNode() = GetLoc.GetText(TREE_GROUPS) Then
            LoadListViewGroup()
        End If

        ' Markierung setzen
        If ListView.Items.Count > 0 Then ListView.SelectedIndices.Add(0)
    End Sub

    Private Sub LoadListViewDict()
        ' Abhängig vom aktuell ausgewählten Wort die Bedeutungen anzeigen
        Dim tvSelectedNode As TreeNode = TreeView.SelectedNode

        Dim words As ICollection(Of WordEntry) ' Finde die Ebene Raus
        Select Case tvSelectedNode.Level
            Case NODE_LEVEL_DICTIONARY
                ' Zeige für alle Sprachen/Hauptsprachen die Anzahl der Einträge an
                SetUpListViewColumns(ListViewStyleEnum.Dictionary)
                ListView.BeginUpdate()
                For Each mainLanguage As String In DictionaryDao.DictionaryMainLanguages()
                    For Each language As String In DictionaryDao.DictionaryLanguages(mainLanguage)
                        SetRangeEntry(GetColumnIndex(ColumnName.DictMainLanguage), mainLanguage)
                        SetRangeEntry(GetColumnIndex(ColumnName.DictLanguage), language)
                        SetRangeEntry(GetColumnIndex(ColumnName.DictCountMainEntry), DictionaryDao.WordCount(language, mainLanguage))
                        SetRangeEntry(GetColumnIndex(ColumnName.DictCountEntrys), DictionaryDao.WordCountTotal(language, mainLanguage))
                        AddRange()
                    Next language
                Next mainLanguage
                ListView.EndUpdate()
            Case NODE_LEVEL_MAIN_LANGUAGE
                ' Zeige für alle Sprachen die Anzahl der Einträge an
                SetUpListViewColumns(ListViewStyleEnum.MainLanguage)
                Dim mainLanguage As String = GetMainLanguageFromNode()
                ListView.BeginUpdate()
                For Each tvNode As TreeNode In tvSelectedNode.Nodes
                    Dim currentLanguage As String = GetLanguageFromNode(tvNode)
                    Dim item As ListViewItem = ListView.Items.Add(currentLanguage)
                    item.SubItems.AddRange(New String() {DictionaryDao.WordCount(currentLanguage, mainLanguage), DictionaryDao.WordCountTotal(currentLanguage, mainLanguage)})
                Next tvNode
                ListView.EndUpdate()
            Case NODE_LEVEL_LANGUAGE
                SetUpListViewColumns(ListViewStyleEnum.Language)
                ListView.BeginUpdate()
                Dim wordsa As ICollection(Of MainEntry) = DictionaryDao.GetMainEntries(GetLanguageFromNode(), GetMainLanguageFromNode())
                For Each entry As String In wordsa.Select(Of String)(Function(t) t.Word)
                    AddMainEntryToList(entry)
                Next entry
                ListView.EndUpdate()
            Case NODE_LEVEL_LANGUAGE + 1
                ' Add all main entries/words starting with this letter
                SetUpListViewColumns(ListViewStyleEnum.WordEntry)
                ListView.BeginUpdate()
                For Each entry As WordEntry In DictionaryDao.GetWords(GetLanguageFromNode(), GetMainLanguageFromNode(), GetInitialFromNode())
                    AddDictionaryEntryToList(entry)
                Next entry
                ListView.EndUpdate()
            Case NODE_LEVEL_ENTRY
                ' Sprache herausfinden
                Dim language As String = GetLanguageFromNode(tvSelectedNode)
                Dim mainEntry As String = GetEntryFromNode(tvSelectedNode)
                ' Anzeigen der Bedeutungen für dieses Wort 
                SetUpListViewColumns(ListViewStyleEnum.WordEntry)
                ' Main-Entry berechnen
                words = DictionaryDao.GetWords(mainEntry, mainEntry, language, GetMainLanguageFromNode())
                ' Anzeigen aller Einträge aus der Collection
                For Each entry As WordEntry In words
                    AddDictionaryEntryToList(entry)
                Next entry
                ' Anzeigen der SubEntrys zum gewählten Eintrag
                words = DictionaryDao.GetSubWords(mainEntry, language, GetMainLanguageFromNode())
                ' Anzeigen aller Einträge aus der Collection
                For Each entry As WordEntry In words
                    Me.AddDictionaryEntryToList(entry)
                Next entry
        End Select
    End Sub

    Private Sub LoadListViewGroup()
        Dim tvSelectedNode As TreeNode = TreeView.SelectedNode
        ' Gruppen
        Select Case tvSelectedNode.Level
            Case NODE_LEVEL_GROUPS
                SetUpListViewColumns(ListViewStyleEnum.Groups)
                ListView.BeginUpdate()
                For Each groupName As String In GroupsDao.GetGroups
                    SetRangeEntry(GetColumnIndex(ColumnName.GroupsName), groupName)
                    SetRangeEntry(GetColumnIndex(ColumnName.GroupsSubGroup), GroupsDao.SubGroupCount(groupName))
                    SetRangeEntry(GetColumnIndex(ColumnName.GroupsCountEntry), DataTools.WordCount(GroupsDao, GroupDao, groupName))
                    SetRangeEntry(GetColumnIndex(ColumnName.GroupCountLanguages), DataTools.UsedLanguagesCount(GroupsDao, GroupDao, groupName))
                    AddRange()
                Next groupName
                ListView.EndUpdate()
            Case NODE_LEVEL_GROUP_ENTRY
                SetUpListViewColumns(ListViewStyleEnum.WordEntrySubGroup)
                Dim theWord As TestWord = tvSelectedNode.Tag
                AddDictionaryGroupEntryToList(theWord)
            Case NODE_LEVEL_SUBGROUP
                SetUpListViewColumns(ListViewStyleEnum.WordEntrySubGroup)
                Dim groupEntry As GroupEntry = GroupsDao.GetGroup(GetGroupFromNode(tvSelectedNode), GetSubGroupFromNode(tvSelectedNode))
                Dim group As GroupDto = GroupDao.Load(groupEntry)
                ListView.BeginUpdate()
                For Each testWord As TestWord In group.Entries
                    AddDictionaryGroupEntryToList(testWord, group)
                Next testWord
                ListView.EndUpdate()
            Case NODE_LEVEL_GROUP
                SetUpListViewColumns(ListViewStyleEnum.WordEntryGroup)
                For Each subGroup As GroupEntry In GroupsDao.GetSubGroups(GetGroupFromNode())
                    Dim groupEntry As GroupEntry = GroupsDao.GetGroup(subGroup.Name, subGroup.SubGroup)
                    Dim group As GroupDto = GroupDao.Load(groupEntry)
                    ListView.BeginUpdate()
                    For Each testWord As TestWord In group.Entries
                        AddDictionaryGroupEntryToList(testWord, group)
                    Next testWord
                    ListView.EndUpdate()
                Next subGroup
        End Select
    End Sub

    Private Sub SetUpListViewColumns2(ByVal Type As ListViewStyleEnum)
        If Not ListView1Initialized Then
            ListView1.Columns.Clear()
            For i As Integer = 0 To GetColumnCount(Type) - 1
                Dim newColumn As ColumnName = GetColumn(i, Type)
                ListView1.Columns.Add(GetColumnText(newColumn))
                ListView1.Columns.Item(i).Width = GetColumnSize(newColumn)
            Next i
            ListView1Initialized = True
        End If
        System.Array.Resize(range, GetColumnCount(Type) - 1)
        For i As Integer = 0 To range.Length - 1
            range(i) = ""
        Next
    End Sub

    Private Sub SetUpListViewColumns(ByVal Type As ListViewStyleEnum)
        If ListViewStyle = Type Then Exit Sub Else ListViewStyle = Type
        ListView.Columns.Clear()
        Dim i As Integer
        For i = 0 To GetColumnCount(Type) - 1
            Dim newColumn As ColumnName = GetColumn(i, Type)
            ListView.Columns.Add(GetColumnText(newColumn))
            ListView.Columns.Item(i).Width = GetColumnSize(newColumn)
        Next i
        SetRange()
    End Sub

    Private Sub AddEntryToList(ByVal word As TestWord)
        ' fügt einen eintrag in die liste hinzu, abhängig davon, ob er gerade angezeigt werden soll
        ' In die Liste hinzufügen
        ' abhängig machen, vom knotentyp
        If IsStrictlySubGroupNode() Then
            AddDictionaryGroupEntryToList(word)
        ElseIf IsDictionaryNode() Then
            Dim node As TreeNode = TreeView.SelectedNode
            If node.Level = NODE_LEVEL_LANGUAGE Then
                AddDictionaryEntryToList(word)
            ElseIf node.Level = NODE_LEVEL_LANGUAGE + 1 Then
                ' nur einfügen, wenn Anfangsbuchstabe übereinstimmt
                If GetLanguageLetterFromNode().ToUpper = word.Word.Chars(0).ToString.ToUpper Then
                    AddDictionaryEntryToList(word)
                Else
                    Exit Sub
                End If
            ElseIf node.Level = NODE_LEVEL_ENTRY Then
                ' einfügen, wenn main-index gleich ist
                Dim mainEntry As MainEntry = DictionaryDao.GetMainEntry(word.WordEntry)
                If mainEntry.Word.ToUpper = GetEntryFromNode().ToUpper Then
                    AddDictionaryEntryToList(word)
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        Else
            ' wird nicht eingefügt
            Exit Sub
        End If

        ' wurde eingefüt, also markieren
        ListView.SelectedItems.Clear()
        ListView.SelectedIndices.Add(ListView.Items.Count - 1)
        ListView.FocusedItem = ListView.Items.Item(ListView.Items.Count - 1)
        ListView.TopItem = ListView.Items.Item(ListView.Items.Count - 1)
    End Sub

    ' Panels anpassen
    Private Sub PanelViewDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PanelViewDefaultMenuItem.Click
        currentPanel = PanelViews.DefaultView
        SetPanelViewItemsCheck(PanelViewDefaultMenuItem)
    End Sub

    Private Sub PanelViewInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PanelViewInputMenuItem.Click
        currentPanel = PanelViews.Input
        SetPanelViewItemsCheck(PanelViewInputMenuItem)
    End Sub

    Private Sub PanelViewSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PanelViewSearchMenuItem.Click
        currentPanel = PanelViews.Search
        SetPanelViewItemsCheck(PanelViewSearchMenuItem)
    End Sub

    Private Sub PanelViewMulti_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PanelViewMultiMenuItem.Click
        currentPanel = PanelViews.Multi
        SetPanelViewItemsCheck(PanelViewMultiMenuItem)
    End Sub

    Private Sub LoadPanel()
        Select Case Me.currentPanel
            Case PanelViews.DefaultView
                LoadPanelDefault()
            Case PanelViews.Input
                LoadPanelInput()
            Case PanelViews.Search
                LoadPanelSearch()
            Case PanelViews.Multi
                LoadPanelMulti()
            Case Else
                Exit Sub
        End Select
    End Sub

    Private Sub LoadPanelDefault()
        If ListView.SelectedIndices.Count > 1 Then LoadPanelMulti() Else LoadPanelInput()
    End Sub

    Private Sub LoadPanelInput()
        PanelMultiEdit.Hide()
        If GetBaseFromNode() = GetLoc.GetText(TREE_DICTIONARY) Then
            chkAddToGroup.Hide()
            chkMarked.Hide()
            Select Case TreeView.SelectedNode.Level
                Case NODE_LEVEL_LANGUAGE
                    PanelWordInfo.Show()
                Case NODE_LEVEL_LANGUAGE + 1
                    PanelWordInfo.Show()
                Case NODE_LEVEL_ENTRY
                    PanelWordInfo.Show()
                Case Else
                    PanelWordInfo.Hide()
            End Select
        ElseIf GetBaseFromNode() = GetLoc.GetText(TREE_GROUPS) Then
            chkAddToGroup.Hide()
            chkMarked.Hide()
            Select Case TreeView.SelectedNode.Level
                Case NODE_LEVEL_GROUP
                    PanelWordInfo.Show()
                Case NODE_LEVEL_SUBGROUP
                    PanelWordInfo.Show()
                    chkAddToGroup.Show()
                    chkMarked.Show()
                Case NODE_LEVEL_GROUP_ENTRY
                    PanelWordInfo.Show()
                    chkAddToGroup.Show()
                    chkMarked.Show()
                Case Else
                    PanelWordInfo.Hide()
            End Select
        Else
            PanelWordInfo.Hide()
        End If
    End Sub

    Private Sub LoadPanelSearch()
        PanelMultiEdit.Hide()
        PanelWordInfo.Hide()
    End Sub

    Private Sub LoadPanelMulti()
        Me.PanelMultiEdit.Show()
        PanelWordInfo.Hide()
    End Sub

    ' Sonstige Funktionen
    Private Sub cmdChangeWord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub ChangeWord(ByVal selectedIndex As Integer)
        Dim wordEntry As WordEntry = GetChangeWord(selectedIndex)

        Try
            Dim newEntry As WordEntry = ChangeWord(wordEntry)
            If IsSubGroupNode() Then
                ChangeGroup(newEntry, chkMarked.Checked)
            End If

            Dim mainEntry As MainEntry = DictionaryDao.GetMainEntry(wordEntry)
            If (mainEntry.Word <> txtMainEntry.Text) Then
                ChangeMainEntry(newEntry)
            End If

            UpdateListViewAfterWordEdit(selectedIndex, newEntry)
        Catch ex As EntryExistsException
            MsgBox("Eintrag existiert bereits: " & txtWord.Text)
        End Try
    End Sub

    Private Function GetChangeWord(selectedIndex As Integer) As WordEntry
        Dim item As ListViewItem = GetSelectedItem(selectedIndex)
        Return item.Tag
    End Function

    Private Function ChangeWord(wordEntry As WordEntry) As WordEntry
        Dim allUpdate As New IDictionaryDao.UpdateData With {
            .Word = txtWord.Text,
            .Pre = txtPre.Text,
            .Post = txtPost.Text,
            .WordType = wordTypes.GetWordType(wordTypes.GetWordType(lstWordType.SelectedIndex)),
            .Meaning = txtMeaning.Text,
            .AdditionalTargetLangInfo = txtAdditionalTargetLangInfo.Text,
            .Irregular = chkIrregular.Checked
        }
        Return DictionaryDao.ChangeEntry(wordEntry, allUpdate)
    End Function

    Private Sub ChangeGroup(newEntry As WordEntry, marked As Boolean)
        Dim groupEntry As GroupEntry = GroupsDao.GetGroup(GetGroupFromNode(), GetSubGroupFromNode())
        ' TODO: add loadsingleentry combining the next two options
        Dim groupData As GroupDto = GroupDao.Load(groupEntry)
        Dim testWord As TestWord = groupData.GetWord(newEntry.WordIndex)
        GroupDao.UpdateMarked(groupEntry, testWord, marked)
    End Sub

    Private Sub ChangeMainEntry(wordEntry As WordEntry)
        Dim mainEntry As MainEntry = DataTools.GetOrCreateMainEntry(DictionaryDao, txtMainEntry.Text, GetLanguageFromNode, "german")
        DictionaryDao.ChangeEntry(wordEntry, mainEntry)
    End Sub

    Private Sub UpdateListViewAfterWordEdit(selectedIndex As Integer, newEntry As WordEntry)
        Dim item As ListViewItem = GetSelectedItem(selectedIndex)
        ' Laden in die Auswahlliste, falls der MainIndex geändert wurde, aktualisieren
        Dim lvs As ListViewStyleEnum
        If ListView1.Visible Then lvs = ListViewStyleEnum.WordEntry Else lvs = ListViewStyle
        item.SubItems(GetColumnIndex(ColumnName.EntryPre, lvs)).Text = newEntry.Pre
        item.SubItems(GetColumnIndex(ColumnName.EntryWord, lvs)).Text = newEntry.Word
        item.SubItems(GetColumnIndex(ColumnName.EntryPost, lvs)).Text = newEntry.Post
        item.SubItems(GetColumnIndex(ColumnName.EntryMeaning, lvs)).Text = newEntry.Meaning
        item.SubItems(GetColumnIndex(ColumnName.EntryType, lvs)).Text = TextTypeName(newEntry.WordType)
        item.SubItems(GetColumnIndex(ColumnName.EntryExtendedInfo, lvs)).Text = newEntry.AdditionalTargetLangInfo
        item.SubItems(GetColumnIndex(ColumnName.EntryIrregular, lvs)).Text = TextYesNo(newEntry.Irregular)
        If IsGroupNode() Then item.SubItems(GetColumnIndex(ColumnName.GroupEntryMarked, lvs)).Text = TextYesNo(chkMarked.Checked)
    End Sub

    Private Function GetSelectedItem(selectedIndex As Integer) As ListViewItem
        If ListView1.Visible Then
            GetSelectedItem = ListView1.Items.Item(selectedIndex)
        Else
            GetSelectedItem = ListView.Items.Item(selectedIndex)
        End If
    End Function

    Private Sub cmdMultiChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMultiChange.Click
        Dim newMainEntry As MainEntry = Nothing
        If chkEnableMultiMainEntry.Checked Then
            newMainEntry = DataTools.GetOrCreateMainEntry(DictionaryDao, txtMultiMainEntry.Text, GetLanguageFromNode, "german")
        End If

        For Each index As Integer In ListView.SelectedIndices
            MultiChangeWord(index, newMainEntry)
        Next
    End Sub

    Private Sub MultiChangeWord(selectedIndex As Integer, newMainEntry As MainEntry)
        ' TODO: create main entry before this is called, if multi main entry is set
        Dim wordEntry As WordEntry = GetChangeWord(selectedIndex)
        Try
            Dim newEntry As WordEntry = ChangeWordMulti(wordEntry)

            If (IsSubGroupNode()) And chkEnableMultiMarked.Checked Then
                ' Markiert dann auch updaten
                ChangeGroup(newEntry, chkMultiMarked.Checked)
            End If

            If chkEnableMultiMainEntry.Checked Then
                DictionaryDao.ChangeEntry(wordEntry, newMainEntry)
            End If

            UpdateListViewAfterWordEdit(selectedIndex, newEntry)
        Catch ex As EntryExistsException
            MsgBox("Eintrag existiert bereits: " & txtMultiWord.Text)
        End Try
    End Sub

    Private Function ChangeWordMulti(wordEntry As WordEntry) As WordEntry
        Dim allUpdate As New IDictionaryDao.UpdateData
        If chkEnableMultiWord.Checked Then allUpdate.Word = txtMultiWord.Text
        If chkEnableMultiPre.Checked Then allUpdate.Pre = txtMultiPre.Text
        If chkEnableMultiPost.Checked Then allUpdate.Post = txtMultiPost.Text
        If chkEnableMultiWordType.Checked Then allUpdate.WordType = wordTypes.GetWordType(wordTypes.GetWordType(lstWordType.SelectedIndex))
        If chkEnableMultiMeaning.Checked Then allUpdate.Meaning = txtMultiMeaning.Text
        If chkEnableMultiAdditionalTargetLangInfo.Checked Then allUpdate.AdditionalTargetLangInfo = txtMultiAdditionaltargetLangInfo.Text
        If chkEnableMultiIrregular.Checked Then allUpdate.Irregular = chkMultiIrregular.Checked
        Return DictionaryDao.ChangeEntry(wordEntry, allUpdate)
    End Function

    ' Hilfsfunktionen für die Knotenbehandlung
    Private Function GetBaseFromNode() As String
        Return GetNodePathPart(TreeView.SelectedNode, NODE_LEVEL_BASE)
    End Function

    Private Function GetBaseFromNode(ByRef tvNode As TreeNode) As String
        Return GetNodePathPart(tvNode, NODE_LEVEL_BASE)
    End Function

    Private Function GetInitialFromNode() As String
        Return GetNodePathPart(TreeView.SelectedNode, NODE_LEVEL_LANGUAGE + 1)
    End Function

    Private Function GetEntryFromNode() As String
        Return GetNodePathPart(TreeView.SelectedNode, NODE_LEVEL_ENTRY)
    End Function

    Private Function GetEntryFromNode(ByRef tvNode As TreeNode) As String
        Return GetNodePathPart(tvNode, NODE_LEVEL_ENTRY)
    End Function

    Private Function GetGroupFromNode(ByRef tvNode As TreeNode) As String
        Return GetNodePathPart(tvNode, NODE_LEVEL_GROUP)
    End Function

    Private Function GetGroupFromNode() As String
        Return GetNodePathPart(TreeView.SelectedNode, NODE_LEVEL_GROUP)
    End Function

    Private Function GetLanguageFromNode(ByRef tvNode As TreeNode) As String
        ' Sprache herausfinden
        Return GetNodePathPart(tvNode, NODE_LEVEL_LANGUAGE)
    End Function

    Private Function GetLanguageFromNode() As String
        ' Sprache herausfinden
        Return GetNodePathPart(TreeView.SelectedNode, NODE_LEVEL_LANGUAGE)
    End Function

    Private Function GetLanguageLetterFromNode(ByRef tvNode As TreeNode) As String
        ' Buchstabe herausfinden
        Return GetNodePathPart(tvNode, NODE_LEVEL_LANGUAGE + 1)
    End Function

    Private Function GetLanguageLetterFromNode() As String
        ' Buchstabe herausfinden
        Return GetNodePathPart(TreeView.SelectedNode, NODE_LEVEL_LANGUAGE + 1)
    End Function

    Private Function GetMainLanguageFromNode() As String
        ' Sprache herausfinden
        Return GetNodePathPart(TreeView.SelectedNode, NODE_LEVEL_MAIN_LANGUAGE)
    End Function

    Private Function GetMainLanguageFromNode(ByRef tvNode As TreeNode) As String
        ' Sprache herausfinden
        Return GetNodePathPart(tvNode, NODE_LEVEL_MAIN_LANGUAGE)
    End Function

    Private Function GetSubGroupFromNode(ByRef tvNode As TreeNode) As String
        Return GetNodePathPart(tvNode, NODE_LEVEL_SUBGROUP)
    End Function

    Private Function GetSubGroupFromNode() As String
        Return GetNodePathPart(TreeView.SelectedNode, NODE_LEVEL_SUBGROUP)
    End Function

    Private Function GetNodePathPart(ByRef tvnode As TreeNode, ByVal level As Integer) As String
        Dim text As String = tvnode.FullPath
        Dim textArray() As String = text.Split("\")
        If level >= textArray.Length Then Return ""
        Return textArray(level)
    End Function

    Private Function IsDictionaryNode() As Boolean
        Dim tvNode As TreeNode = TreeView.SelectedNode
        If GetBaseFromNode(tvNode) = GetLoc.GetText(TREE_DICTIONARY) And tvNode.Level > NODE_LEVEL_LANGUAGE Then
            Return True
        End If
        Return False
    End Function

    Private Function IsGroupNode() As Boolean
        Dim Node As TreeNode = TreeView.SelectedNode
        If GetBaseFromNode(Node) = GetLoc.GetText(TREE_GROUPS) And Node.Level >= NODE_LEVEL_GROUP Then
            Return True
        End If
        Return False
    End Function

    Private Function IsSubGroupNode() As Boolean
        Dim Node As TreeNode = TreeView.SelectedNode
        If GetBaseFromNode(Node) = GetLoc.GetText(TREE_GROUPS) And Node.Level >= NODE_LEVEL_SUBGROUP Then
            Return True
        End If
        Return False
    End Function

    Private Function IsStrictlySubGroupNode() As Boolean
        Dim Node As TreeNode = TreeView.SelectedNode
        If GetBaseFromNode(Node) = GetLoc.GetText(TREE_GROUPS) And Node.Level = NODE_LEVEL_SUBGROUP Then
            Return True
        End If
        Return False
    End Function

    Private Function IsLanguageNode() As Boolean
        Dim Node As TreeNode = TreeView.SelectedNode
        If GetBaseFromNode(Node) = GetLoc.GetText(TREE_DICTIONARY) And Node.Level = NODE_LEVEL_LANGUAGE Then
            Return True
        End If
        Return False
    End Function

    Private Function IsWordEntryNode() As Boolean
        If IsDictionaryNode() Then Return True
        If IsGroupNode() Then Return True
        Return False
    End Function

    ' Andere Hilfsfunktionen
    Private Sub SetPanelViewItemsCheck(ByRef currentPanel As ToolStripMenuItem)
        For Each entry As ToolStripMenuItem In Me.PanelViewItems
            entry.Checked = False
        Next entry
        currentPanel.Checked = True
        LoadPanel()
    End Sub

    Public Sub AddMainEntryToList(ByVal Word As String)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryWord), Word)
        AddRange()
    End Sub

    Public Sub AddDictionaryEntryToList(ByVal Word As WordEntry)
        AddDictionaryEntryToListData(Word)
        AddRange().Tag = Word
    End Sub

    Public Sub AddDictionaryEntryToList(ByVal Word As TestWord)
        AddDictionaryEntryToListData(Word.WordEntry)
        AddRange().Tag = Word
    End Sub

    Private Sub AddDictionaryEntryToListData(ByRef word As WordEntry)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPre), word.Pre)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryWord), word.Word)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPost), word.Post)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryMeaning), word.Meaning)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryType), TextTypeName(word.WordType))
        SetRangeEntry(GetColumnIndex(ColumnName.EntryExtendedInfo), word.AdditionalTargetLangInfo)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryIrregular), TextYesNo(word.Irregular))
    End Sub

    Public Sub AddDictionaryEntryToList2(ByVal Word As WordEntry)
        Dim listViewStyle As ListViewStyleEnum = ListViewStyleEnum.WordEntry
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPre, listViewStyle), Word.Pre)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryWord, listViewStyle), Word.Word)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPost, listViewStyle), Word.Post)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryMeaning, listViewStyle), Word.Meaning)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryType, listViewStyle), TextTypeName(Word.WordType))
        SetRangeEntry(GetColumnIndex(ColumnName.EntryExtendedInfo, listViewStyle), Word.AdditionalTargetLangInfo)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryIrregular, listViewStyle), TextYesNo(Word.Irregular))
        AddRange2().Tag = Word
    End Sub

    Public Sub AddDictionaryGroupEntryToList(ByVal Word As TestWord, ByRef group As GroupDto)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPre), Word.Pre)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryWord), Word.Word)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPost), Word.Post)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryMeaning), Word.Meaning)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryType), TextTypeName(Word.WordEntry.WordType))
        SetRangeEntry(GetColumnIndex(ColumnName.EntryExtendedInfo), Word.AdditionalTargetLangInfo)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryIrregular), TextYesNo(Word.Irregular))
        SetRangeEntry(GetColumnIndex(ColumnName.GroupEntryMarked), TextYesNo(GroupDto.IsMarked(group, Word.WordIndex)))
        SetRangeEntry(GetColumnIndex(ColumnName.GroupEntrySubgroup), group.GroupSubName)
        AddRange().Tag = Word
    End Sub

    Public Sub AddDictionaryGroupEntryToList(ByVal Word As TestWord)
        Dim groupEntry As GroupEntry = GroupsDao.GetGroup(GetGroupFromNode(), GetSubGroupFromNode())
        Dim group As GroupDto = GroupDao.Load(groupEntry)

        SetRangeEntry(GetColumnIndex(ColumnName.EntryPre), Word.Pre)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryWord), Word.Word)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPost), Word.Post)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryMeaning), Word.Meaning)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryType), TextTypeName(Word.WordEntry.WordType))
        SetRangeEntry(GetColumnIndex(ColumnName.EntryExtendedInfo), Word.AdditionalTargetLangInfo)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryIrregular), TextYesNo(Word.Irregular))
        SetRangeEntry(GetColumnIndex(ColumnName.GroupEntryMarked), TextYesNo(GroupDto.IsMarked(group, Word.WordIndex)))
        SetRangeEntry(GetColumnIndex(ColumnName.GroupEntrySubgroup), groupEntry.SubGroup)
        AddRange().Tag = Word
    End Sub

    ' Hilfsfunktion für die Spalten
    Private Function GetColumnIndex(ByVal column As ColumnName) As Integer
        Return GetColumnIndex(column, ListViewStyle)
    End Function

    Private Function GetColumnIndex(ByVal column As ColumnName, ByVal currentStyle As ListViewStyleEnum) As Integer
        Select Case currentStyle
            Case ListViewStyleEnum.Groups
                Select Case column
                    Case ColumnName.GroupsName
                        Return 0
                    Case ColumnName.GroupsSubGroup
                        Return 1
                    Case ColumnName.GroupsCountEntry
                        Return 2
                    Case ColumnName.GroupCountLanguages
                        Return 3
                    Case Else
                        Return -1
                End Select
            Case ListViewStyleEnum.Language
                Select Case column
                    Case ColumnName.EntryWord
                        Return 0
                        'Case ColumnName.EntryPre
                        '  Return 0
                        'Case ColumnName.EntryWord
                        '  Return 1
                        'Case ColumnName.EntryPost
                        '  Return 2
                        'Case ColumnName.EntryMeaning
                        '  Return 3
                        'Case ColumnName.EntryType
                        '  Return 4
                        'Case ColumnName.EntryExtendedInfo
                        '  Return 6
                        'Case ColumnName.EntryIrregular
                        '  Return 5
                    Case Else
                        Return -1
                End Select
            Case ListViewStyleEnum.WordEntry
                Select Case column
                    Case ColumnName.EntryPre
                        Return 0
                    Case ColumnName.EntryWord
                        Return 1
                    Case ColumnName.EntryPost
                        Return 2
                    Case ColumnName.EntryMeaning
                        Return 3
                    Case ColumnName.EntryType
                        Return 4
                    Case ColumnName.EntryExtendedInfo
                        Return 6
                    Case ColumnName.EntryIrregular
                        Return 5
                    Case Else
                        Return -1
                End Select
            Case ListViewStyleEnum.WordEntryGroup
                Select Case column
                    Case ColumnName.EntryPre
                        Return 0
                    Case ColumnName.EntryWord
                        Return 1
                    Case ColumnName.EntryPost
                        Return 2
                    Case ColumnName.EntryMeaning
                        Return 3
                    Case ColumnName.EntryType
                        Return 4
                    Case ColumnName.EntryExtendedInfo
                        Return 6
                    Case ColumnName.EntryIrregular
                        Return 5
                    Case ColumnName.GroupEntryMarked
                        Return 7
                    Case ColumnName.GroupEntrySubgroup
                        Return 8
                    Case Else
                        Return -1
                End Select
            Case ListViewStyleEnum.WordEntrySubGroup
                Select Case column
                    Case ColumnName.EntryPre
                        Return 0
                    Case ColumnName.EntryWord
                        Return 1
                    Case ColumnName.EntryPost
                        Return 2
                    Case ColumnName.EntryMeaning
                        Return 3
                    Case ColumnName.EntryType
                        Return 4
                    Case ColumnName.EntryExtendedInfo
                        Return 6
                    Case ColumnName.EntryIrregular
                        Return 5
                    Case ColumnName.GroupEntryMarked
                        Return 7
                    Case Else
                        Return -1
                End Select
            Case ListViewStyleEnum.Dictionary
                Select Case column
                    Case ColumnName.DictMainLanguage
                        Return 0
                    Case ColumnName.DictLanguage
                        Return 1
                    Case ColumnName.DictCountMainEntry
                        Return 2
                    Case ColumnName.DictCountEntrys
                        Return 3
                    Case Else
                        Return -1
                End Select
            Case ListViewStyleEnum.MainLanguage
                Select Case column
                    Case ColumnName.DictLanguage
                        Return 0
                    Case ColumnName.DictCountMainEntry
                        Return 1
                    Case ColumnName.DictCountEntrys
                        Return 2
                    Case Else
                        Return -1
                End Select
        End Select
    End Function

    Private Function GetColumnText(ByVal column As ColumnName) As String
        Select Case column
            Case ColumnName.DictCountEntrys
                Return GetLoc.GetText(EXPLORER_HEADLINE_TOTAL_ENTRYS)
            Case ColumnName.DictCountMainEntry
                Return GetLoc.GetText(EXPLORER_HEADLINE_MAIN_ENTRYS)
            Case ColumnName.DictLanguage
                Return GetLoc.GetText(EXPLORER_HEADLINE_LANGUAGE)
            Case ColumnName.DictMainLanguage
                Return GetLoc.GetText(EXPLORER_HEADLINE_MAIN_LANGUAGE)
            Case ColumnName.EntryExtendedInfo
                Return GetLoc.GetText(EXPLORER_HEADLINE_ADDITIONAL_INFO)
            Case ColumnName.EntryIrregular
                Return GetLoc.GetText(EXPLORER_HEADLINE_IRREGULAR)
            Case ColumnName.EntryMeaning
                Return GetLoc.GetText(EXPLORER_HEADLINE_MEANING)
            Case ColumnName.EntryPost
                Return GetLoc.GetText(EXPLORER_HEADLINE_POST)
            Case ColumnName.EntryPre
                Return GetLoc.GetText(EXPLORER_HEADLINE_PRE)
            Case ColumnName.EntryType
                Return GetLoc.GetText(EXPLORER_HEADLINE_WORD_TYPE)
            Case ColumnName.EntryWord
                Return GetLoc.GetText(EXPLORER_HEADLINE_WORD)
            Case ColumnName.GroupEntryMarked
                Return GetLoc.GetText(EXPLORER_HEADLINE_MARKED)
            Case ColumnName.GroupEntrySubgroup
                Return GetLoc.GetText(EXPLORER_HEADLINE_SUBGROUP)
            Case ColumnName.GroupsCountEntry
                Return GetLoc.GetText(EXPLORER_HEADLINE_ENTRYS)
            Case ColumnName.GroupsName
                Return GetLoc.GetText(EXPLORER_HEADLINE_GROUPS)
            Case ColumnName.GroupsSubGroup
                Return GetLoc.GetText(EXPLORER_HEADLINE_SUBGROUPS)
            Case ColumnName.GroupCountLanguages
                Return GetLoc.GetText(EXPLORER_HEADLINE_GROUP_LANGUAGE_COUNT)
            Case Else
                Throw New Exception(GetLoc.GetText(EXCEPTION_UNKNOWN_HEADLINE))
        End Select
    End Function

    Private Function GetColumnSize(ByVal column As ColumnName) As Integer
        Select Case column
            Case ColumnName.DictCountEntrys
                Return 110 '"Einträge gesamt"
            Case ColumnName.DictCountMainEntry
                Return 110 '"Haupteinträge"
            Case ColumnName.DictLanguage
                Return 110 '"Sprache"
            Case ColumnName.DictMainLanguage
                Return 110 '"Hauptsprache"
            Case ColumnName.EntryExtendedInfo
                Return 110 '"Erweiterte Info"
            Case ColumnName.EntryIrregular
                Return 45   '0.5
            Case ColumnName.EntryMeaning
                Return 110 '"Bedeutung"
            Case ColumnName.EntryPost
                Return 45   '0.75
            Case ColumnName.EntryPre
                Return 45   '0.75
            Case ColumnName.EntryType
                Return 65   '0.75
            Case ColumnName.EntryWord
                Return 110 '"Wort"
            Case ColumnName.GroupEntryMarked
                Return 45   '0.5
            Case ColumnName.GroupEntrySubgroup
                Return 110
            Case ColumnName.GroupsCountEntry
                Return 110 '2.0
            Case ColumnName.GroupsName
                Return 110 '2.0
            Case ColumnName.GroupsSubGroup
                Return 110 '2.0
            Case ColumnName.GroupCountLanguages
                Return 110 ' 1.0
            Case Else
                Throw New Exception("Unbekannte Überschrift in GetColumnText gefunden.")
        End Select
    End Function

    Private Function GetColumn(ByVal ColumnIndex As Integer, ByVal currentStyle As ListViewStyleEnum) As ColumnName
        If GetColumnIndex(ColumnName.GroupsName, currentStyle) = ColumnIndex Then Return ColumnName.GroupsName
        If GetColumnIndex(ColumnName.GroupsSubGroup, currentStyle) = ColumnIndex Then Return ColumnName.GroupsSubGroup
        If GetColumnIndex(ColumnName.GroupsCountEntry, currentStyle) = ColumnIndex Then Return ColumnName.GroupsCountEntry
        If GetColumnIndex(ColumnName.EntryPre, currentStyle) = ColumnIndex Then Return ColumnName.EntryPre
        If GetColumnIndex(ColumnName.EntryWord, currentStyle) = ColumnIndex Then Return ColumnName.EntryWord
        If GetColumnIndex(ColumnName.EntryPost, currentStyle) = ColumnIndex Then Return ColumnName.EntryPost
        If GetColumnIndex(ColumnName.EntryMeaning, currentStyle) = ColumnIndex Then Return ColumnName.EntryMeaning
        If GetColumnIndex(ColumnName.EntryType, currentStyle) = ColumnIndex Then Return ColumnName.EntryType
        If GetColumnIndex(ColumnName.EntryExtendedInfo, currentStyle) = ColumnIndex Then Return ColumnName.EntryExtendedInfo
        If GetColumnIndex(ColumnName.EntryIrregular, currentStyle) = ColumnIndex Then Return ColumnName.EntryIrregular
        If GetColumnIndex(ColumnName.GroupEntryMarked, currentStyle) = ColumnIndex Then Return ColumnName.GroupEntryMarked
        If GetColumnIndex(ColumnName.GroupEntrySubgroup, currentStyle) = ColumnIndex Then Return ColumnName.GroupEntrySubgroup
        If GetColumnIndex(ColumnName.DictMainLanguage, currentStyle) = ColumnIndex Then Return ColumnName.DictMainLanguage
        If GetColumnIndex(ColumnName.DictLanguage, currentStyle) = ColumnIndex Then Return ColumnName.DictLanguage
        If GetColumnIndex(ColumnName.DictCountMainEntry, currentStyle) = ColumnIndex Then Return ColumnName.DictCountMainEntry
        If GetColumnIndex(ColumnName.DictCountEntrys, currentStyle) = ColumnIndex Then Return ColumnName.DictCountEntrys
        If GetColumnIndex(ColumnName.GroupCountLanguages, currentStyle) = ColumnIndex Then Return ColumnName.GroupCountLanguages
        Throw New Exception("No Column found")
    End Function

    Private Function GetColumnCount(ByVal currentStyle As ListViewStyleEnum)
        ' Gibt die Anzahl der Spalten für einen bestimmten List-View-Style
        Dim count As Integer = 0
        While True
            Dim newColumn As ColumnName
            Try
                newColumn = GetColumn(count, currentStyle)
            Catch e As Exception
                Exit While
            End Try
            count += 1
        End While
        Return count
    End Function

    Public Property ListViewStyle() As ListViewStyleEnum
        Get
            Return m_listviewstyle
        End Get
        Set(ByVal value As ListViewStyleEnum)
            m_listviewstyle = value
        End Set
    End Property

    Private Function AddRange() As ListViewItem
        Dim item As ListViewItem = ListView.Items.Add(rangeStart)
        item.SubItems.AddRange(range)

        Dim i As Integer
        For i = 0 To range.Length - 1
            range(i) = ""
        Next i
        rangeStart = ""
        Return item
    End Function

    Private Function AddRange2() As ListViewItem
        Dim item As ListViewItem = ListView1.Items.Add(rangeStart)
        item.SubItems.AddRange(range)

        Dim i As Integer
        For i = 0 To range.Length - 1
            range(i) = ""
        Next i
        rangeStart = ""
        Return item
    End Function

    Private Function AddRange(ByVal item As ListViewItem) As ListViewItem
        Dim i As Integer
        For i = 0 To range.Length
            SetRangeEntry(i, item.SubItems(i).Text)
        Next i
        Dim ret As ListViewItem = AddRange()
        ret.Tag = item.Tag
        Return ret
    End Function

    Private Sub SetRange()
        System.Array.Resize(range, GetColumnCount(ListViewStyle) - 1)
        Dim i As Integer
        For i = 0 To range.Length - 1
            range(i) = ""
        Next
    End Sub

    Private Sub SetRangeEntry(ByVal index As Integer, ByVal value As String)
        If index < 0 Then Exit Sub ' negative einfach ignorieren. ermöglicht einfaches Hinzufügen von nicht vorhandenen Feldern über GetColumnIndex
        If index = 0 Then
            rangeStart = value
        Else
            If index > range.Length Then Throw New IndexOutOfRangeException("Nicht so viele Columns")
            range(index - 1) = value
        End If
    End Sub

    ' Sprachspezifisches
    Private Function TextTypeName(ByVal value As Integer)
        Return GetLoc.GetText(wordTypes.GetWordType(value))
    End Function

    Private Function TextYesNo(ByVal value As Boolean) As String
        Return IIf(value, GetLoc.GetText(YES), GetLoc.GetText(NO))
    End Function

    ' Multi-Editing, aktivieren und deaktivieren der Steuerelemente
    Private Sub chkEnableMultiPre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableMultiPre.CheckedChanged
        txtMultiPre.Enabled = chkEnableMultiPre.Checked
    End Sub

    Private Sub chkEnableMultiWord_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableMultiWord.CheckedChanged
        txtMultiWord.Enabled = chkEnableMultiWord.Checked
    End Sub

    Private Sub chkEnableMultiPost_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableMultiPost.CheckedChanged
        txtMultiPost.Enabled = chkEnableMultiPost.Checked
    End Sub

    Private Sub chkEnableMultiAdditionalTargetLangInfo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableMultiAdditionalTargetLangInfo.CheckedChanged
        txtMultiAdditionaltargetLangInfo.Enabled = chkEnableMultiAdditionalTargetLangInfo.Checked
    End Sub

    Private Sub chkEnableMultiMeaning_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableMultiMeaning.CheckedChanged
        txtMultiMeaning.Enabled = chkEnableMultiMeaning.Checked
    End Sub

    Private Sub chkEnableMultiIrregular_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableMultiIrregular.CheckedChanged
        chkMultiIrregular.Enabled = chkEnableMultiIrregular.Checked
    End Sub

    Private Sub chkEnableMultiWordType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableMultiWordType.CheckedChanged
        lstMultiWordType.Enabled = chkEnableMultiWordType.Checked
    End Sub

    Private Sub chkEnableMultiMainEntry_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableMultiMainEntry.CheckedChanged
        txtMultiMainEntry.Enabled = chkEnableMultiMainEntry.Checked
    End Sub

    Private Sub chkEnableMultiMarked_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableMultiMarked.CheckedChanged
        chkMultiMarked.Enabled = chkEnableMultiMarked.Checked
    End Sub

    Private Sub txtWord_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWord.TextChanged
        'txtMainEntry.Text = txtWord.Text
    End Sub

    Private Sub ListView_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView.SizeChanged
        ListView1.Width = ListView.Width
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        ' Zeige ausgewählten Eintrag an
        '(genauso wie in der anderen Liste)
        If ListView1.SelectedIndices.Count = 0 Then Exit Sub
        Dim selectedNode As TreeNode = TreeView.SelectedNode
        Dim item As ListViewItem = ListView1.Items.Item(ListView1.SelectedIndices.Item(0))
        Dim wordEntry As WordEntry = item.Tag
        ShowWordInfo(wordEntry)
    End Sub

    Private Sub ShowWordInfo(ByRef dicEntry As WordEntry)
        Dim mainEntry As MainEntry = DictionaryDao.GetMainEntry(dicEntry)
        txtMainEntry.Text = mainEntry.Word
        txtPre.Text = dicEntry.Pre
        txtWord.Text = dicEntry.Word
        txtPost.Text = dicEntry.Post
        txtAdditionalTargetLangInfo.Text = dicEntry.AdditionalTargetLangInfo
        txtMeaning.Text = dicEntry.Meaning
        lstWordType.SelectedIndex = dicEntry.WordType

        txtLanguage.Text = mainEntry.Language
        txtMainLanguage.Text = mainEntry.MainLanguage
    End Sub

    Private Sub ShowWordInfo(ByRef dicEntry As TestWord)
        ShowWordInfo(dicEntry.WordEntry)

        chkIrregular.Checked = dicEntry.Irregular
        If IsSubGroupNode() Then
            chkMarked.Checked = dicEntry.Marked
        End If
    End Sub

    Private Sub cmdChangeWord_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChangeWord.Click
        If ListView.SelectedIndices.Count > 1 Then MsgBox("Bitte nur einen Eintrag markieren!") : Exit Sub
        If ListView.SelectedIndices.Count = 0 Then MsgBox("Sie müssen einen Eintrag markieren") : Exit Sub
        If ListView1.SelectedIndices.Count > 1 And ListView1.Visible Then MsgBox("Bitte nur einen Eintrag markieren!") : Exit Sub
        If ListView1.SelectedIndices.Count = 0 And ListView1.Visible Then MsgBox("Sie müssen einen Eintrag markieren") : Exit Sub
        listUpdate = True
        If ListView1.Visible Then
            ChangeWord(ListView1.SelectedIndices.Item(0))
        Else
            ChangeWord(ListView.SelectedIndices.Item(0))
        End If
    End Sub

    Private Sub cmdAdd_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        listUpdate = True
        Dim deWord As New WordEntry(txtWord.Text, txtPre.Text, txtPost.Text, lstWordType.SelectedIndex, txtMeaning.Text, txtAdditionalTargetLangInfo.Text, chkIrregular.Checked)

        ' Versuch, ins Wörterbuch einzufügen
        Dim language As String
        Dim mainLanguage As String

        If IsDictionaryNode() Then
            language = GetLanguageFromNode()
            mainLanguage = GetMainLanguageFromNode()
        ElseIf IsGroupNode() Then
            Dim groupEntry As GroupEntry = GroupsDao.GetGroup(GetGroupFromNode(), GetSubGroupFromNode())
            If GroupDao.GetLanguages(groupEntry).Count > 1 Then
                MsgBox("Zu viele Sprachen in der Gruppe. Die Sprache kann nicht automatisch festgelegt werden! Eintrag wird nicht hinzugefügt.", MsgBoxStyle.Information, "Warning")
                Exit Sub
            End If
            If GroupDao.GetMainLanguages(groupEntry).Count > 1 Then
                MsgBox("Zu viele Zielsprachen in der Gruppe. Die Sprache kann nicht automatisch festgelegt werden! Eintrag wird nicht hinzugefügt.", MsgBoxStyle.Information, "Warning")
                Exit Sub
            End If
            language = GroupDao.GetUniqueLanguage(GroupEntry)
            mainLanguage = GroupDao.GetUniqueMainLanguage(GroupEntry)
            If (language = "") Then language = txtLanguage.Text
            If (mainLanguage = "") Then mainLanguage = txtMainLanguage.Text
        Else
            MsgBox("Eintrag kann nicht hinzugefügt werden.", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        End If

        If (txtMainEntry.Text = "") Then txtMainEntry.Text = txtWord.Text
        If language = "" Or mainLanguage = "" Then
            MsgBox("Bitte geben sie eine Sprache und eine Hauptsprache an.", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        End If
        Try
            DictionaryDao.AddSubEntry(deWord, txtMainEntry.Text, language, mainLanguage)
        Catch ex As EntryExistsException
            ' Existiert schon, nix zu tun, index feststellen
        Catch ex As EntryNotFoundException
            ' Haupteintrag nicht vorhanden
            Dim res As MsgBoxResult = MsgBox("Der Haupteintrag '" & txtMainEntry.Text & "' ist für die gewählten Sprachen '" & mainLanguage & "' und '" & language & "' nicht vorhanden. Soll er erstellt werden?", MsgBoxStyle.YesNo, "Haupteintrag nicht vorhanden")
            If res = MsgBoxResult.Yes Then
                Try
                    DictionaryDao.AddEntry(Trim(txtMainEntry.Text), language, mainLanguage)
                Catch ex2 As xlsExceptionInput
                    MsgBox(ex2.Message, MsgBoxStyle.Information, "Unkorrekte Eingabe")
                End Try
                ' Untereintrag hinzufügen
                Try
                    DictionaryDao.AddSubEntry(deWord, txtMainEntry.Text, language, mainLanguage)
                Catch ex2 As Exception
                    MsgBox("Eintrag nicht möglich, konflikt mit Index wahrscheinlich. Überprüfen Sie Ihre Datenbankversion." & vbCrLf & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
                End Try
            Else
                Exit Sub
            End If
        Catch ex As Exception   'System.Data.OleDb.OleDbException
            'ErrorCode = -2147467259
            MsgBox("Eintrag nicht möglich, konflikt mit Index wahrscheinlich. Überprüfen Sie Ihre Datenbankversion." & vbCrLf & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

        ' Müsste im Wörterbuch sein, füge nun in die Gruppe ein
        If IsSubGroupNode() And Me.chkAddToGroup.Checked Then   ' hinzufügen für subgroup und tiefer
            Dim groupEntry As GroupEntry = GroupsDao.GetGroup(GetGroupFromNode(), GetSubGroupFromNode())
            ' TODO example
            GroupDao.Add(groupEntry, deWord, chkMarked.Checked, "")
        End If

        ' This will fail for IsDictionaryNode() == True, and not a group node.
        If (IsGroupNode() And chkMarked.Checked) Or IsDictionaryNode() Then AddEntryToList(GroupDao.Load(GroupsDao.GetGroup(GetGroupFromNode(), GetSubGroupFromNode()), deWord))

        ' Anzeige aktualisieren
        txtMainEntry.SelectAll()
        txtPre.Focus()
        txtAdditionalTargetLangInfo.SelectAll()
        txtMeaning.SelectAll()
        txtPost.SelectAll()
        txtPre.SelectAll()
        txtWord.SelectAll()
    End Sub
End Class

