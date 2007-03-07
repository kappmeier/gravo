Imports System.Diagnostics
Imports System.Windows.Forms
Imports Gravo2k7.AccessDatabaseOperation
Imports System.Collections.ObjectModel

'Temporär:
Imports System.Data.OleDb

Public Class VocabularyExplorer
  Enum ListViewStyle
    WordSubEntry
    WordEntry
  End Enum

  Private Const NODE_LEVEL_LANGUAGE As Integer = 0
  Private Const NODE_LEVEL_ENTRY As Integer = NODE_LEVEL_LANGUAGE + 1
  Private Const NODE_LEVEL_SUBENTRY As Integer = NODE_LEVEL_ENTRY + 1

  'Gibt an, ob der ausgewählte Knoten der Strukturansicht programmgesteuert geändert wird
  Private ChangingSelectedNode As Boolean

  Dim db As New AccessDatabaseOperation                 ' Datenbankoperationen für Microsoft Access Datenbanken
  Dim voc As New xlsDictionary                          ' Zugriff auf die Wort-Datenbank allgemein

  Public Sub New()
    ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
    InitializeComponent()

    ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
    db.Open(Application.StartupPath() & "\voc.mdb")     ' Datenbank öffnen
    voc.DBConnection = db
  End Sub

  Private Sub VocabularyExplorer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    'Benutzeroberfläche einrichten
    SetUpListViewColumns(ListViewStyle.WordEntry)
    LoadTree()
    Exit Sub
  End Sub

  Private Sub LoadTree()
    ' TODO: nur temp-platzhalter einsetzen und beim expandieren füllen. --> schnelleres laden!
    Dim tvRoot As TreeNode
    Dim tvNode As TreeNode
    Dim tvSubNode As TreeNode

    TreeView.BeginUpdate()
    ' für jede vorhandene Sprache einen Wurzelknoten hinzufügen
    Dim cLanguages As Collection(Of String) = voc.DictionaryLanguages()
    Dim i, j, k As Integer
    For i = 0 To cLanguages.Count - 1
      tvRoot = Me.TreeView.Nodes.Add(cLanguages.Item(i))   ' Wurzel für jede Sprache erzeugen
      ' Für diese Sprache sämtliche vorhandenen vokabeln einfügen
      Dim words As Collection(Of String) = voc.DictionaryEntrys(cLanguages.Item(i), "german")
      For j = 0 To words.Count - 1
        tvNode = tvRoot.Nodes.Add(words.Item(j))
        ' Für dieses Wort sämtliche Untereinträge eintragen
        Dim cSubWords As Collection(Of String) = voc.DictionarySubEntrys(words.Item(j), cLanguages.Item(i), "german")
        If cSubWords.Count = 0 Then Continue For
        For k = 0 To cSubWords.Count - 1
          tvSubNode = tvNode.Nodes.Add(cSubWords.Item(k))
        Next k
      Next j
    Next i
    TreeView.EndUpdate()
  End Sub

  Private Sub TreeView_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles TreeView.AfterLabelEdit
    Dim tvSelectedNode As TreeNode = TreeView.SelectedNode
    Dim sOldName As String = tvSelectedNode.Text
    Dim sNewName As String = e.Label

    Select Case tvSelectedNode.Level
      Case NODE_LEVEL_ENTRY
        Dim index As Integer = voc.GetEntryIndex(sOldName, GetLanguageFromNode(tvSelectedNode), "german")
        Try
          ' Den Eintrag im Hauptverzeichnis ändern
          voc.ChangeEntry(index, sNewName)
          ' Alle Einträge für das Wort im Unterverzeichnis ändern, falls es welche gibt
          Dim indices As Collection(Of Integer) = voc.GetSubEntryIndices(index, sOldName)
          voc.ChangeSubEntries(indices, sNewName)
        Catch ex As Exception
          MsgBox(ex.Message, MsgBoxStyle.Exclamation, """" & sOldName & """ konnte nicht umbenannt werden.")
          e.CancelEdit = True
        End Try
      Case NODE_LEVEL_SUBENTRY
        Dim index As Integer = voc.GetEntryIndex(GetEntryFromNode(tvSelectedNode), GetLanguageFromNode(tvSelectedNode), "german")
        Try
          Dim indices As Collection(Of Integer) = voc.GetSubEntryIndices(voc.GetEntryIndex(GetEntryFromNode(tvSelectedNode), GetLanguageFromNode(tvSelectedNode), "german"), sOldName)
          voc.ChangeSubEntries(indices, sNewName)
        Catch ex As Exception
          MsgBox(ex.Message, MsgBoxStyle.Exclamation, """" & sOldName & """ konnte nicht umbenannt werden.")
          e.CancelEdit = True
        End Try
    End Select
  End Sub

  Private Sub TreeView_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView.AfterSelect
    LoadListView()
  End Sub

  Private Sub SetUpListViewColumns(ByVal Type As ListViewStyle)
    ListView.Columns.Clear()
    Select Case Type
      Case ListViewStyle.WordEntry
        ListView.Columns.Add("Pre")
        ListView.Columns.Add("Wort")
        ListView.Columns.Add("Post")
        ListView.Columns.Add("Bedeutung")
        ListView.Columns.Add("Typ")
        ListView.Columns.Add("Erweiterte Info")
      Case ListViewStyle.WordSubEntry
        ListView.Columns.Add("Bedeutung")
        ListView.Columns.Add("Pre")
        ListView.Columns.Add("Post")
        ListView.Columns.Add("Typ")
        ListView.Columns.Add("Erweiterte Info")
      Case Else
    End Select
    SetView(View.Details)
  End Sub

  Private Sub LoadListView()
    ' Abhängig vom aktuell ausgewählten Wort die Bedeutungen anzeigen

    Dim tvSelectedNode As TreeNode = TreeView.SelectedNode
    Dim lvItem As ListViewItem
    ListView.Items.Clear()

    ' Sprache herausfinden
    Dim sLanguage As String = GetLanguageFromNode(tvSelectedNode)
    Dim sMainEntry As String = GetEntryFromNode(tvSelectedNode)
    Dim sSubEntry As String = GetSubEntryFromNode(tvSelectedNode)

    Dim words As Collection(Of xlsDictionaryEntry) ' Finde die Ebene Raus
    Select Case tvSelectedNode.Level
      Case NODE_LEVEL_LANGUAGE
        ' Nichts zu tun, für die gesamte Sprache wird zur Zeit nichts angezeigt
        Exit Sub
      Case NODE_LEVEL_ENTRY
        ' Anzeigen der Bedeutungen für dieses Wort 
        Me.SetUpListViewColumns(ListViewStyle.WordEntry)
        ' Main-Entry berechnen
        words = voc.GetWords(sMainEntry, sMainEntry, sLanguage, "german")
        ' Anzeigen aller Einträge aus der Collection
        For Each wCurrent As xlsDictionaryEntry In words
          lvItem = ListView.Items.Add(wCurrent.Pre)
          lvItem.SubItems.AddRange(New String() {wCurrent.Word, wCurrent.Post, wCurrent.Meaning, wCurrent.WordType, wCurrent.AdditionalTargetLangInfo})
        Next
        ' Anzeigen der SubEntrys zum gewählten Eintrag
        words = voc.GetSubWords(sMainEntry, sLanguage, "german")
        ' Anzeigen aller Einträge aus der Collection
        For Each wCurrent As xlsDictionaryEntry In words
          lvItem = ListView.Items.Add(wCurrent.Pre)
          lvItem.SubItems.AddRange(New String() {wCurrent.Word, wCurrent.Post, wCurrent.Meaning, wCurrent.WordType, wCurrent.AdditionalTargetLangInfo})
        Next
      Case NODE_LEVEL_SUBENTRY
        ' Anzeigen aller Einträge die angegebenen Wort aus der Sprache entsprechen
        'Main-Entry berechnen
        SetUpListViewColumns(ListViewStyle.WordSubEntry)
        words = voc.GetWords(sMainEntry, sSubEntry, sLanguage, "german")
        ' Anzeigen aller Einträge aus der Collection
        For Each wCurrent As xlsDictionaryEntry In words
          lvItem = ListView.Items.Add(wCurrent.Meaning)
          lvItem.SubItems.AddRange(New String() {wCurrent.Pre, wCurrent.Post, wCurrent.WordType, wCurrent.AdditionalTargetLangInfo})
        Next
    End Select
  End Sub

  Private Function GetLanguageFromNode(ByRef tvNode As TreeNode) As String
    ' Sprache herausfinden
    Return Mid(tvNode.FullPath, 1, InStr(tvNode.FullPath & "\", "\") - 1)
  End Function

  Private Function GetEntryFromNode(ByRef tvNode As TreeNode) As String
    Select Case tvNode.Level
      Case NODE_LEVEL_ENTRY
        Return Mid(tvNode.FullPath, InStrRev(tvNode.FullPath, "\") + 1)
      Case NODE_LEVEL_SUBENTRY
        Return Mid(tvNode.FullPath, InStr(tvNode.FullPath, "\") + 1, InStrRev(tvNode.FullPath, "\") - InStr(tvNode.FullPath, "\") - 1)
      Case Else
        Return ""
    End Select
  End Function

  Private Function GetSubEntryFromNode(ByRef tvNode As TreeNode) As String
    Return Mid(tvNode.FullPath, InStrRev(tvNode.FullPath, "\") + 1)
  End Function

  ' Stuff für die unsichtaben Sachen, evtl. später mal verwenden TODO
  Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
    'Anwendung beenden
    Global.System.Windows.Forms.Application.Exit()
  End Sub

  Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolBarToolStripMenuItem.Click
    'Sichtbarkeit des Toolstrips und aktivierten Zustand des zugehörigen Menüelements umschalten
    ToolBarToolStripMenuItem.Checked = Not ToolBarToolStripMenuItem.Checked
    ToolStrip.Visible = ToolBarToolStripMenuItem.Checked
  End Sub

  Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusBarToolStripMenuItem.Click
    'Sichtbarkeit des Statusstrips und aktivierten Zustand des zugehörigen Menüelements umschalten
    StatusBarToolStripMenuItem.Checked = Not StatusBarToolStripMenuItem.Checked
    StatusStrip.Visible = StatusBarToolStripMenuItem.Checked
  End Sub

  'Sichtbarkeit des Ordnerbereichs ändern
  Private Sub ToggleFoldersVisible()
    'Zuerst den aktivierten Zustand des zugehörigen Menüelements umschalten
    FoldersToolStripMenuItem.Checked = Not FoldersToolStripMenuItem.Checked

    'Symbolleistenschaltfläche "Ordner" für die Synchronisierung ändern
    FoldersToolStripButton.Checked = FoldersToolStripMenuItem.Checked

    ' Bereich mit TreeView reduzieren.
    Me.SplitContainer.Panel1Collapsed = Not FoldersToolStripMenuItem.Checked
  End Sub

  Private Sub FoldersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FoldersToolStripMenuItem.Click
    ToggleFoldersVisible()
  End Sub

  Private Sub FoldersToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FoldersToolStripButton.Click
    ToggleFoldersVisible()
  End Sub

  Private Sub SetView(ByVal View As System.Windows.Forms.View)
    'Bestimmen, welche Menüelemente aktiviert werden sollen
    Dim MenuItemToCheck As ToolStripMenuItem = Nothing
    Select Case View
      Case View.Details
        MenuItemToCheck = DetailsToolStripMenuItem
      Case View.LargeIcon
        MenuItemToCheck = LargeIconsToolStripMenuItem
      Case View.List
        MenuItemToCheck = ListToolStripMenuItem
      Case View.SmallIcon
        MenuItemToCheck = SmallIconsToolStripMenuItem
      Case View.Tile
        MenuItemToCheck = TileToolStripMenuItem
      Case Else
        Debug.Fail("Unerwartete Ansicht")
        View = View.Details
        MenuItemToCheck = DetailsToolStripMenuItem
    End Select

    'Entsprechendes Menüelement aktivieren und Auswahl aller anderen Elemente im Menü "Ansichten" aufheben
    For Each MenuItem As ToolStripMenuItem In ListViewToolStripButton.DropDownItems
      If MenuItem Is MenuItemToCheck Then
        MenuItem.Checked = True
      Else
        MenuItem.Checked = False
      End If
    Next

    'Abschließend die angeforderte Ansicht festlegen
    ListView.View = View
  End Sub

  Private Sub ListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListToolStripMenuItem.Click
    SetView(View.List)
  End Sub

  Private Sub DetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DetailsToolStripMenuItem.Click
    SetView(View.Details)
  End Sub

  Private Sub LargeIconsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LargeIconsToolStripMenuItem.Click
    SetView(View.LargeIcon)
  End Sub

  Private Sub SmallIconsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SmallIconsToolStripMenuItem.Click
    SetView(View.SmallIcon)
  End Sub

  Private Sub TileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TileToolStripMenuItem.Click
    SetView(View.Tile)
  End Sub

  Private Sub OpenToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
    Dim OpenFileDialog As New OpenFileDialog
    OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    OpenFileDialog.Filter = "Textdateien (*.txt)|*.txt"
    OpenFileDialog.ShowDialog(Me)

    Dim FileName As String = OpenFileDialog.FileName
    ' TODO: Code zum Öffnen der Datei hinzufügen
  End Sub

  Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click
    Dim SaveFileDialog As New SaveFileDialog
    SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    SaveFileDialog.Filter = "Textdateien (*.txt)|*.txt"
    SaveFileDialog.ShowDialog(Me)

    Dim FileName As String = SaveFileDialog.FileName
    ' TODO: Hier Code einfügen, um den aktuellen Inhalt des Formulars in einer Datei zu speichern.
  End Sub

  Private Sub TreeView_DrawNode(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawTreeNodeEventArgs) Handles TreeView.DrawNode
  End Sub

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    TreeView.Sort()
  End Sub
End Class
