Imports Gravo.localization
Imports System.Collections.ObjectModel  ' Für Collection(Of T)

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
    Private Const NODE_LEVEL_ENTRY As Integer = NODE_LEVEL_LANGUAGE + 2

    Private Const NODE_LEVEL_GROUPS As Integer = NODE_LEVEL_BASE
    Private Const NODE_LEVEL_GROUP As Integer = NODE_LEVEL_GROUPS + 1
    Private Const NODE_LEVEL_SUBGROUP As Integer = NODE_LEVEL_GROUP + 1
    Private Const NODE_LEVEL_GROUP_ENTRY As Integer = NODE_LEVEL_SUBGROUP + 1

    Dim db As New SQLiteDataBaseOperation()
    Dim voc As New xlsDictionary                          ' Zugriff auf die Wort-Datenbank allgemein
    Dim groups As New xlsGroups()
    Dim prop As New xlsDBPropertys()

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
        voc.DBConnection = db
        groups.DBConnection = db
        prop.DBConnection = db

        ' Lade die Collections
        PanelViewItems.Add(PanelViewDefaultMenuItem)
        PanelViewItems.Add(PanelViewInputMenuItem)
        PanelViewItems.Add(PanelViewSearchMenuItem)
        PanelViewItems.Add(PanelViewMultiMenuItem)

        PanelWordInfo.Dock = DockStyle.Fill
        PanelMultiEdit.Dock = DockStyle.Fill

        ' Anzahl der Zeichen pro Textfeld
        txtPre.MaxLength = prop.DictionaryWordsMaxLengthPre
        txtPost.MaxLength = prop.DictionaryWordsMaxLengthPost
        txtWord.MaxLength = prop.DictionaryWordsMaxLengthWord
        txtMeaning.MaxLength = prop.DictionaryWordsMaxLengthMeaning
        txtAdditionalTargetLangInfo.MaxLength = prop.DictionaryWordsMaxLengthAdditionalTargetLangInfo
        txtMainEntry.MaxLength = prop.DictionaryMainMaxLengthWordEntry
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
        For Each type As String In prop.GetSupportedWordTypes()
            lstWordType.Items.Add(GetLoc.GetText(type))
            lstMultiWordType.Items.Add(GetLoc.GetText(type))
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

        ' Wörterbucheinträge Hinzufügen
        For Each mainLanguage As String In voc.DictionaryMainLanguages()
            tvRoot = tvRoot.Nodes.Add(mainLanguage) ' Bis jetzt nur deutsch ;)
            Dim languages As Collection(Of String) = voc.DictionaryLanguages(mainLanguage)
            Dim i As Integer
            Dim tvLang As TreeNode
            For i = 0 To languages.Count - 1
                tvLang = tvRoot.Nodes.Add(languages.Item(i)) 'Wurzel für jede Sprache
                ' Erstelle den Temp-Subeintrag
                tvNode = tvLang.Nodes.Add("temp")
            Next i
        Next mainLanguage
        tvRoot = TreeView.Nodes.Add(GetLoc.GetText(TREE_GROUPS))

        ' Gruppen hinzufügen
        For Each group As String In groups.GetGroups()
            tvNode = tvRoot.Nodes.Add(group)
            For Each subGroup As xlsGroupEntry In groups.GetSubGroups(group)
                Dim tv As TreeNode = tvNode.Nodes.Add(subGroup.SubGroup)
                If groups.WordCount(group, subGroup.SubGroup) > 0 Then
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
                    Dim index As Integer = voc.GetEntryIndex(oldName, GetLanguageFromNode(), GetMainLanguageFromNode())
                    Try
                        ' Den Eintrag im Hauptverzeichnis ändern
                        voc.ChangeEntry(index, newName)
                        ' Alle Einträge für das Wort im Unterverzeichnis ändern, falls es welche gibt
                        Dim indices As Collection(Of Integer) = voc.GetSubEntryIndices(index, oldName)
                        voc.ChangeSubEntries(indices, newName)
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
        ElseIf IsGroupNode() Then 'GetBaseFromNode(tvSelectedNode) = GetLoc.GetText(TREE_GROUPS) Then
            Select Case tvSelectedNode.Level
                Case NODE_LEVEL_GROUP
                    groups.EditGroup(oldName, newName)
                Case NODE_LEVEL_SUBGROUP
                    groups.EditSubGroup(GetGroupFromNode(), oldName, newName)
                Case NODE_LEVEL_GROUP_ENTRY
                    Dim item As ListViewItem = ListView.Items.Item(0)
                    Dim index As Integer = voc.GetSubEntryIndex(item.Tag, item.SubItems(1).Text, item.SubItems(3).Text)
                    Dim dicEntry As xlsDictionaryEntry = New xlsDictionaryEntry(voc.DBConnection, index)
                    Dim selectedWordIndex As Integer = dicEntry.WordIndex
                    Dim t As xlsDictionaryEntry = New xlsDictionaryEntry(voc.DBConnection, selectedWordIndex)
                    t.Word = newName
                    Try
                        t.SaveWord()
                        item.SubItems(GetColumnIndex(ColumnName.EntryWord)).Text = t.Word
                    Catch ex As xlsExceptionEntryExists
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
                        If voc.WordCount(tvNode.Text, GetMainLanguageFromNode(tvNode), character) > 0 Then
                            tvSub.Nodes.Add("temp")
                        End If
                    Next
                Case NODE_LEVEL_LANGUAGE + 1
                    ' Lade die Wörter die mit diesem Buchstaben beginnen
                    Dim words As Collection(Of String) = voc.DictionaryEntrys(GetLanguageFromNode(tvNode), GetMainLanguageFromNode(tvNode), tvNode.Text)
                    tvNode.Nodes.Clear()
                    For i = 0 To words.Count - 1
                        tvSub = tvNode.Nodes.Add(words.Item(i))
                    Next i
            End Select
        Else
            ' Gruppen wurden ausgewählt
            If tvNode.Level = NODE_LEVEL_SUBGROUP Then
                tvNode.Nodes.Clear()
                ' Laden der Gruppeneinträge
                Dim group As String = GetGroupFromNode(tvNode)
                Dim subGroup As String = GetSubGroupFromNode(tvNode)
                Dim grp As xlsGroup = groups.GetGroup(group, subGroup)
                For Each index As Integer In grp.GetIndices()
                    tvSub = tvNode.Nodes.Add(voc.GetSubEntryName(index))
                    tvSub.Tag = index
                Next index
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
            Dim index As Integer = voc.GetSubEntryIndex(item.Tag, item.SubItems(1).Text, item.SubItems(3).Text)
            ShowWordInfo(index)
        ElseIf IsLanguageNode() Then    ' LanguageNode ist kein WordEntry Node!
            If ListView1.Visible = False Then
                Me.ListView1.Visible = True
                PanelWordInfoInner.Top = ListView1.Height
            End If
            Dim words As Collection(Of xlsDictionaryEntry) = voc.DictionarySubEntrysExt(item.SubItems(0).Text, Me.GetLanguageFromNode(), Me.GetMainLanguageFromNode())
            ListView1.Items.Clear()
            Me.SetUpListViewColumns2(ListViewStyleEnum.WordEntry)
            For Each word As xlsDictionaryEntry In words
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
        If ListView.Items.Count > 0 Then ListView.SelectedIndices.Add(0) 'Else cmdChangeWord.Enabled = False
    End Sub

    Private Sub LoadListViewDict()
        ' Abhängig vom aktuell ausgewählten Wort die Bedeutungen anzeigen
        Dim tvSelectedNode As TreeNode = TreeView.SelectedNode

        Dim words As Collection(Of xlsDictionaryEntry) ' Finde die Ebene Raus
        Select Case tvSelectedNode.Level
            Case NODE_LEVEL_DICTIONARY
                ' Zeige für alle Sprachen/Hauptsprachen die Anzahl der Einträge an
                SetUpListViewColumns(ListViewStyleEnum.Dictionary)
                ListView.BeginUpdate()
                For Each mainLanguage As String In voc.DictionaryMainLanguages()
                    For Each language As String In voc.DictionaryLanguages(mainLanguage)
                        SetRangeEntry(GetColumnIndex(ColumnName.DictMainLanguage), mainLanguage)
                        SetRangeEntry(GetColumnIndex(ColumnName.DictLanguage), language)
                        SetRangeEntry(GetColumnIndex(ColumnName.DictCountMainEntry), voc.WordCount(language, mainLanguage))
                        SetRangeEntry(GetColumnIndex(ColumnName.DictCountEntrys), voc.WordCountTotal(language, mainLanguage))
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
                    item.SubItems.AddRange(New String() {voc.WordCount(currentLanguage, mainLanguage), voc.WordCountTotal(currentLanguage, mainLanguage)})
                Next tvNode
                ListView.EndUpdate()
            Case NODE_LEVEL_LANGUAGE
                ' 
                SetUpListViewColumns(ListViewStyleEnum.Language)
                ListView.BeginUpdate()
                Dim wordsa As Collection(Of String) = voc.DictionaryEntrys(GetLanguageFromNode(), GetMainLanguageFromNode()) 'voc.DictionaryEntrys(GetLanguageFromNode(tvNode), GetMainLanguageFromNode(tvNode), tvNode.Text)
                For Each entry As String In wordsa
                    AddMainEntryToList(entry)
                Next entry
                ListView.EndUpdate()
            Case NODE_LEVEL_LANGUAGE + 1
                ' Zeige alle Buchstaben mit dem anfangsbuchstaben an
                SetUpListViewColumns(ListViewStyleEnum.WordEntry)
                ListView.BeginUpdate()
                For Each entry As xlsDictionaryEntry In voc.GetWords(GetLanguageFromNode(), GetMainLanguageFromNode(), GetInitialFromNode())
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
                words = voc.GetWords(mainEntry, mainEntry, language, GetMainLanguageFromNode())
                ' Anzeigen aller Einträge aus der Collection
                For Each entry As xlsDictionaryEntry In words
                    AddDictionaryEntryToList(entry)
                Next entry
                ' Anzeigen der SubEntrys zum gewählten Eintrag
                words = voc.GetSubWords(mainEntry, language, GetMainLanguageFromNode())
                ' Anzeigen aller Einträge aus der Collection
                For Each entry As xlsDictionaryEntry In words
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
                For Each groupName As String In groups.GetGroups
                    SetRangeEntry(GetColumnIndex(ColumnName.GroupsName), groupName)
                    SetRangeEntry(GetColumnIndex(ColumnName.GroupsSubGroup), groups.SubGroupCount(groupName))
                    SetRangeEntry(GetColumnIndex(ColumnName.GroupsCountEntry), groups.WordCount(groupName))
                    SetRangeEntry(GetColumnIndex(ColumnName.GroupCountLanguages), groups.UsedLanguagesCount(groupName))
                    AddRange()
                Next groupName
                ListView.EndUpdate()
            Case NODE_LEVEL_GROUP_ENTRY
                SetUpListViewColumns(ListViewStyleEnum.WordEntrySubGroup)
                AddDictionaryGroupEntryToList(voc.GetSubEntry(tvSelectedNode.Tag))
            Case NODE_LEVEL_SUBGROUP
                SetUpListViewColumns(ListViewStyleEnum.WordEntrySubGroup)
                Dim group As xlsGroup = groups.GetGroup(GetGroupFromNode(tvSelectedNode), GetSubGroupFromNode(tvSelectedNode))
                ListView.BeginUpdate()
                For Each index As Integer In group.GetIndices
                    AddDictionaryGroupEntryToList(voc.GetSubEntry(index), group)
                Next index
                ListView.EndUpdate()
            Case NODE_LEVEL_GROUP
                SetUpListViewColumns(ListViewStyleEnum.WordEntryGroup)
                For Each subGroup As xlsGroupEntry In groups.GetSubGroups(GetGroupFromNode())
                    Dim group As xlsGroup = groups.GetGroup(subGroup.Group, subGroup.SubGroup)
                    ListView.BeginUpdate()
                    For Each index As Integer In group.GetIndices
                        AddDictionaryGroupEntryToList(voc.GetSubEntry(index), group)
                    Next index
                    ListView.EndUpdate()
                Next subGroup
        End Select
    End Sub

    Private Sub SetUpListViewColumns2(ByVal Type As ListViewStyleEnum)
        'If ListViewStyle = Type Then Exit Sub Else ListViewStyle = Type
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

    Private Sub AddEntryToList(ByVal word As xlsDictionaryEntry)
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
                Dim mainEntry As String = voc.GetEntryName(word.MainIndex)
                If mainEntry.ToUpper = GetEntryFromNode().ToUpper Then
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
        Dim item As ListViewItem
        If ListView1.Visible Then
            item = ListView1.Items.Item(selectedIndex)
        Else
            item = ListView.Items.Item(selectedIndex)
        End If


        Dim index As Integer = voc.GetSubEntryIndex(item.Tag, item.SubItems(1).Text, item.SubItems(3).Text)
        Dim dicEntry As xlsDictionaryEntry = New xlsDictionaryEntry(voc.DBConnection, index)
        Dim selectedWordIndex As Integer = dicEntry.WordIndex

        Dim t As xlsDictionaryEntry = New xlsDictionaryEntry(voc.DBConnection, selectedWordIndex)
        ' Main-Index kann nicht geändert werden
        ' finde den main-index heraus, das dazugehörige wort und teste, ob sie übereinstimmen
        Dim newIndex As Integer
        Dim mainIndexChanged As Boolean = False
        If Trim(voc.GetEntryName(t.MainIndex)) <> Trim(txtMainEntry.Text) Then
            mainIndexChanged = True
            ' Erzeuge neuen MainIndex oder hole den Index eines alten
            Try
                newIndex = voc.GetEntryIndex(txtMainEntry.Text, voc.GetEntryLanguage(t.MainIndex), voc.GetEntryMainLanguage(t.MainIndex))
            Catch ex As xlsExceptionEntryNotFound
                ' Haupteintrag erstellen und anschließend laden
                Try
                    MsgBox("Neuer Eintrag wird erstellt")
                    voc.AddEntry(Trim(txtMainEntry.Text), voc.GetEntryLanguage(t.MainIndex), voc.GetEntryMainLanguage(t.MainIndex))
                    newIndex = voc.GetEntryIndex(txtMainEntry.Text, voc.GetEntryLanguage(t.MainIndex), voc.GetEntryMainLanguage(t.MainIndex))
                Catch sex As xlsExceptionInput
                    MsgBox(sex.Message, MsgBoxStyle.Information, "Unerwarteter Fehler")
                    Exit Sub
                End Try
            End Try
            t.MainIndex = newIndex
        End If
        t.Pre = txtPre.Text
        t.Word = txtWord.Text
        t.Post = txtPost.Text
        t.AdditionalTargetLangInfo = txtAdditionalTargetLangInfo.Text
        t.Meaning = txtMeaning.Text
        t.WordType = lstWordType.SelectedIndex
        t.Irregular = chkIrregular.Checked
        Try
            t.SaveWord()

            ' Falls ein Gruppeneintrag ist, marked setzen
            If (IsSubGroupNode()) Then
                Dim groups As xlsGroups = New xlsGroups(voc.DBConnection)
                Dim group As xlsGroup = groups.GetGroup(GetGroupFromNode(), GetSubGroupFromNode())
                group.SetMarked(t.WordIndex, chkMarked.Checked)
                ' Markiert dann auch updaten
            End If
            ' Laden in die Auswahlliste, falls der MainIndex geändert wurde, aktualisieren
            If mainIndexChanged Then
                item.Tag = t.MainIndex
            End If
            Dim lvs As ListViewStyleEnum
            If ListView1.Visible Then lvs = ListViewStyleEnum.WordEntry Else lvs = ListViewStyle
            item.SubItems(GetColumnIndex(ColumnName.EntryPre, lvs)).Text = t.Pre
            item.SubItems(GetColumnIndex(ColumnName.EntryWord, lvs)).Text = t.Word
            item.SubItems(GetColumnIndex(ColumnName.EntryPost, lvs)).Text = t.Post
            item.SubItems(GetColumnIndex(ColumnName.EntryMeaning, lvs)).Text = t.Meaning
            item.SubItems(GetColumnIndex(ColumnName.EntryType, lvs)).Text = TextTypeName(t.WordType)
            item.SubItems(GetColumnIndex(ColumnName.EntryExtendedInfo, lvs)).Text = t.AdditionalTargetLangInfo
            item.SubItems(GetColumnIndex(ColumnName.EntryIrregular, lvs)).Text = TextYesNo(t.Irregular)
            If IsGroupNode() Then item.SubItems(GetColumnIndex(ColumnName.GroupEntryMarked, lvs)).Text = TextYesNo(chkMarked.Checked) 'IIf(chkMarked.Checked, "Ja", "Nein")
            'End If
        Catch ex As xlsExceptionEntryExists
            MsgBox("Eintrag existiert bereits.")
        End Try
    End Sub

    Private Sub cmdMultiChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMultiChange.Click
        For Each index As Integer In ListView.SelectedIndices
            MultiChangeWord(index)
        Next
    End Sub

    Private Sub MultiChangeWord(ByVal selectedIndex As Integer)
        Dim item As ListViewItem = ListView.Items.Item(selectedIndex)
        Dim index As Integer = voc.GetSubEntryIndex(item.Tag, item.SubItems(1).Text, item.SubItems(3).Text)
        Dim dicEntry As xlsDictionaryEntry = New xlsDictionaryEntry(voc.DBConnection, index)
        Dim selectedWordIndex As Integer = dicEntry.WordIndex

        Dim t As xlsDictionaryEntry = New xlsDictionaryEntry(voc.DBConnection, selectedWordIndex)
        ' finde den main-index heraus, das dazugehörige wort und teste, ob sie übereinstimmen
        'Dim newIndex As Integer
        'Dim mainIndexChanged As Boolean = False

        ' Neuen Main-Index nur suchen, wenn geändert werden soll.
        If chkEnableMultiMainEntry.Checked Then
            Dim newIndex As Integer
            Try
                newIndex = voc.GetEntryIndex(txtMultiMainEntry.Text, voc.GetEntryLanguage(t.MainIndex), voc.GetEntryMainLanguage(t.MainIndex))
            Catch ex As xlsExceptionEntryNotFound
                ' Haupteintrag erstellen und anschließend laden
                Try
                    MsgBox("Neuer Eintrag wird erstellt")
                    voc.AddEntry(Trim(txtMultiMainEntry.Text), voc.GetEntryLanguage(t.MainIndex), voc.GetEntryMainLanguage(t.MainIndex))
                    newIndex = voc.GetEntryIndex(txtMultiMainEntry.Text, voc.GetEntryLanguage(t.MainIndex), voc.GetEntryMainLanguage(t.MainIndex))
                Catch sex As xlsExceptionInput
                    MsgBox(sex.Message, MsgBoxStyle.Information, "Unerwarteter Fehler")
                    Exit Sub
                End Try
            End Try
            t.MainIndex = newIndex
        End If

        If chkEnableMultiPre.Checked Then t.Pre = txtMultiPre.Text
        If chkEnableMultiWord.Checked Then t.Word = txtMultiWord.Text
        If chkEnableMultiPost.Checked Then t.Post = txtMultiPost.Text
        If chkEnableMultiAdditionalTargetLangInfo.Checked Then t.AdditionalTargetLangInfo = txtMultiAdditionaltargetLangInfo.Text
        If chkEnableMultiMeaning.Checked Then t.Meaning = txtMultiMeaning.Text
        If chkEnableMultiWordType.Checked Then t.WordType = lstMultiWordType.SelectedIndex
        If chkEnableMultiIrregular.Checked Then t.Irregular = chkMultiIrregular.Checked
        t.SaveWord()

        ' Falls ein Gruppeneintrag ist, marked setzen
        Dim newMarkStatus As Boolean
        Dim groups As xlsGroups = New xlsGroups(voc.DBConnection)
        Dim group As xlsGroup = groups.GetGroup(GetGroupFromNode(), GetSubGroupFromNode())
        If (IsSubGroupNode()) And chkEnableMultiMarked.Checked Then
            ' Markiert dann auch updaten
            group.SetMarked(t.WordIndex, chkMultiMarked.Checked)
            newMarkStatus = chkMultiMarked.Checked
        ElseIf IsSubGroupNode() Then
            newMarkStatus = group.GetMarked(t.WordIndex)
        End If

        ' Laden in die Auswahlliste, falls der MainIndex geändert wurde, aktualisieren
        'Dim item As ListViewItem = ListView.Items.Item(ListView.SelectedIndices.Item(0))
        If chkEnableMultiMainEntry.Checked Then
            item.Tag = t.MainIndex
        End If
        item.SubItems(GetColumnIndex(ColumnName.EntryPre)).Text = t.Pre
        item.SubItems(GetColumnIndex(ColumnName.EntryWord)).Text = t.Word
        item.SubItems(GetColumnIndex(ColumnName.EntryPost)).Text = t.Post
        item.SubItems(GetColumnIndex(ColumnName.EntryMeaning)).Text = t.Meaning
        item.SubItems(GetColumnIndex(ColumnName.EntryType)).Text = TextTypeName(t.WordType)
        item.SubItems(GetColumnIndex(ColumnName.EntryExtendedInfo)).Text = t.AdditionalTargetLangInfo
        item.SubItems(GetColumnIndex(ColumnName.EntryIrregular)).Text = TextYesNo(t.Irregular)
        item.SubItems(GetColumnIndex(ColumnName.GroupEntryMarked)).Text = TextYesNo(newMarkStatus) 'IIf(chkMarked.Checked, "Ja", "Nein")
        'End If
    End Sub

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

    Public Sub AddDictionaryEntryToList(ByVal Word As xlsDictionaryEntry)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPre), Word.Pre)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryWord), Word.Word)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPost), Word.Post)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryMeaning), Word.Meaning)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryType), TextTypeName(Word.WordType))
        SetRangeEntry(GetColumnIndex(ColumnName.EntryExtendedInfo), Word.AdditionalTargetLangInfo)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryIrregular), TextYesNo(Word.Irregular))
        AddRange().Tag = Word.MainIndex
    End Sub

    Public Sub AddDictionaryEntryToList2(ByVal Word As xlsDictionaryEntry)
        Dim listViewStyle As ListViewStyleEnum = ListViewStyleEnum.WordEntry
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPre, listViewStyle), Word.Pre)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryWord, listViewStyle), Word.Word)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPost, listViewStyle), Word.Post)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryMeaning, listViewStyle), Word.Meaning)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryType, listViewStyle), TextTypeName(Word.WordType))
        SetRangeEntry(GetColumnIndex(ColumnName.EntryExtendedInfo, listViewStyle), Word.AdditionalTargetLangInfo)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryIrregular, listViewStyle), TextYesNo(Word.Irregular))
        AddRange2().Tag = Word.MainIndex
    End Sub

    Public Sub AddDictionaryGroupEntryToList(ByVal Word As xlsDictionaryEntry, ByRef group As xlsGroup)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPre), Word.Pre)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryWord), Word.Word)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPost), Word.Post)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryMeaning), Word.Meaning)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryType), TextTypeName(Word.WordType))
        SetRangeEntry(GetColumnIndex(ColumnName.EntryExtendedInfo), Word.AdditionalTargetLangInfo)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryIrregular), TextYesNo(Word.Irregular))
        SetRangeEntry(GetColumnIndex(ColumnName.GroupEntryMarked), TextYesNo(group.GetMarked(Word.WordIndex)))
        SetRangeEntry(GetColumnIndex(ColumnName.GroupEntrySubgroup), group.GroupSubName)
        AddRange().Tag = Word.MainIndex
    End Sub

    Public Sub AddDictionaryGroupEntryToList(ByVal Word As xlsDictionaryEntry)
        Dim groups As xlsGroups = New xlsGroups(voc.DBConnection)
        Dim group As xlsGroup = groups.GetGroup(GetGroupFromNode(), GetSubGroupFromNode())
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPre), Word.Pre)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryWord), Word.Word)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryPost), Word.Post)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryMeaning), Word.Meaning)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryType), TextTypeName(Word.WordType))
        SetRangeEntry(GetColumnIndex(ColumnName.EntryExtendedInfo), Word.AdditionalTargetLangInfo)
        SetRangeEntry(GetColumnIndex(ColumnName.EntryIrregular), TextYesNo(Word.Irregular))
        SetRangeEntry(GetColumnIndex(ColumnName.GroupEntryMarked), TextYesNo(group.GetMarked(Word.WordIndex)))
        SetRangeEntry(GetColumnIndex(ColumnName.GroupEntrySubgroup), group.GroupSubName)
        AddRange().Tag = Word.MainIndex
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
        item.SubItems.AddRange(range)   'New String() {language, voc.WordCount(language, mainLanguage), voc.WordCountTotal(language, mainLanguage)})

        Dim i As Integer
        For i = 0 To range.Length - 1
            range(i) = ""
        Next i
        rangeStart = ""
        Return item
    End Function

    Private Function AddRange2() As ListViewItem
        Dim item As ListViewItem = ListView1.Items.Add(rangeStart)
        item.SubItems.AddRange(range)   'New String() {language, voc.WordCount(language, mainLanguage), voc.WordCountTotal(language, mainLanguage)})

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
        Return GetLoc.GetText(prop.GetWordType(value))
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
        txtMainEntry.Text = txtWord.Text
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

        ' Der zugehörige Index des Haupteintrages kann aus der item.Tag Eigenschaft geholt werden
        Dim index As Integer = voc.GetSubEntryIndex(item.Tag, item.SubItems(1).Text, item.SubItems(3).Text)
        ShowWordInfo(index)

    End Sub

    Private Sub ShowWordInfo(ByVal Index As Integer)
        Dim dicEntry As xlsDictionaryEntry = New xlsDictionaryEntry(voc.DBConnection, Index)
        txtMainEntry.Text = voc.GetEntryName(dicEntry.MainIndex)
        txtPre.Text = dicEntry.Pre
        txtWord.Text = dicEntry.Word
        txtPost.Text = dicEntry.Post
        txtAdditionalTargetLangInfo.Text = dicEntry.AdditionalTargetLangInfo
        txtMeaning.Text = dicEntry.Meaning
        chkIrregular.Checked = dicEntry.Irregular
        lstWordType.SelectedIndex = dicEntry.WordType
        ' Falls Group-Entry ist, muß "markiert" gesetzt werden
        If IsSubGroupNode() Then
            Dim groups As New xlsGroups(voc.DBConnection)
            Dim group As xlsGroup = groups.GetGroup(GetGroupFromNode(), GetSubGroupFromNode())
            chkMarked.Checked = group.GetMarked(Index)
        End If
        txtLanguage.Text = voc.GetEntryLanguage(dicEntry.MainIndex)
        txtMainLanguage.Text = voc.GetEntryMainLanguage(dicEntry.MainIndex)
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
        Dim deWord As New xlsDictionaryEntry(voc.DBConnection)
        deWord.LoadNewWord(voc.GetMaxSubEntryIndex + 1)
        deWord.Pre = txtPre.Text
        deWord.Word = txtWord.Text
        deWord.Post = txtPost.Text
        deWord.Meaning = txtMeaning.Text
        deWord.AdditionalTargetLangInfo = txtAdditionalTargetLangInfo.Text
        deWord.WordType = lstWordType.SelectedIndex
        deWord.Irregular = chkIrregular.Checked

        ' Versuch, ins Wörterbuch einzufügen
        Dim language As String
        Dim mainLanguage As String

        Dim group As xlsGroup = Nothing
        If IsDictionaryNode() Then
            language = GetLanguageFromNode()
            mainLanguage = GetMainLanguageFromNode()
        ElseIf IsGroupNode() Then
            group = groups.GetGroup(GetGroupFromNode(), GetSubGroupFromNode())
            If group.LanguageCount > 1 Then
                MsgBox("Zu viele Sprachen in der Gruppe. Die Sprache kann nicht automatisch festgelegt werden! Eintrag wird nicht hinzugefügt.", MsgBoxStyle.Information, "Warning")
                Exit Sub
            End If
            If group.MainLanguageCount > 1 Then ' ob das jemals vorkommen kann?
                MsgBox("Zu viele Zielsprachen in der Gruppe. Die Sprache kann nicht automatisch festgelegt werden! Eintrag wird nicht hinzugefügt.", MsgBoxStyle.Information, "Warning")
                Exit Sub
            End If
            language = group.GetUniqueLanguage()
            mainLanguage = group.GetUniqueMainLanguage()
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
            voc.AddSubEntry(deWord, txtMainEntry.Text, language, mainLanguage)
        Catch ex As xlsExceptionEntryExists
            ' Existiert schon, nix zu tun, index feststellen
        Catch ex As xlsExceptionEntryNotFound
            ' Haupteintrag nicht vorhanden
            Dim res As MsgBoxResult = MsgBox("Der Haupteintrag '" & txtMainEntry.Text & "' ist für die gewählten Sprachen '" & mainLanguage & "' und '" & language & "' nicht vorhanden. Soll er erstellt werden?", MsgBoxStyle.YesNo, "Haupteintrag nicht vorhanden")
            If res = MsgBoxResult.Yes Then
                Try
                    voc.AddEntry(Trim(txtMainEntry.Text), language, mainLanguage)
                Catch ex2 As xlsExceptionInput
                    MsgBox(ex2.Message, MsgBoxStyle.Information, "Unkorrekte Eingabe")
                End Try
                ' Untereintrag hinzufügen
                Try
                    voc.AddSubEntry(deWord, txtMainEntry.Text, language, mainLanguage)
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
        deWord.MainIndex = voc.GetEntryIndex(txtMainEntry.Text, language, mainLanguage)
        deWord.FindCorrectWordIndex()   ' aktualisieren, falls schon vorhanden war!

        ' Müsste im Wörterbuch sein, füge nun in die Gruppe ein
        If IsSubGroupNode() And Me.chkAddToGroup.Checked Then   ' hinzufügen für subgroup und tiefer
            ' Davon ausgehen, daß das Einfügen in die Wortliste korrekt erfolgt ist
            Dim subIndex As Integer = voc.GetSubEntryIndex(deWord.MainIndex, deWord.Word, deWord.Meaning)
            ' TODO example
            group.Add(subIndex, chkMarked.Checked, "")
        End If

        If (IsGroupNode() And chkMarked.Checked) Or IsDictionaryNode() Then AddEntryToList(deWord)

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

