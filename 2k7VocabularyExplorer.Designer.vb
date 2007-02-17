<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VocabularyExplorer
  Inherits System.Windows.Forms.Form

  'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing Then
      If Not (components Is Nothing) Then
        components.Dispose()
      End If
    End If
    MyBase.Dispose(disposing)
  End Sub

  Friend WithEvents ToolStripContainer As System.Windows.Forms.ToolStripContainer
  Friend WithEvents TreeNodeImageList As System.Windows.Forms.ImageList
  Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
  Friend WithEvents BackToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents ForwardToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents FoldersToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents ListViewToolStripButton As System.Windows.Forms.ToolStripDropDownButton
  Friend WithEvents ListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents DetailsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents LargeIconsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents SmallIconsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents TileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
  Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents PrintPreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents UndoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents RedoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolBarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents StatusBarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents FoldersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
  Friend WithEvents TreeView As System.Windows.Forms.TreeView
  Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
  Friend WithEvents ToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents ToolTip As System.Windows.Forms.ToolTip

  'Wird vom Windows Form-Designer benötigt.
  Private components As System.ComponentModel.IContainer

  'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
  'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
  'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VocabularyExplorer))
    Me.StatusStrip = New System.Windows.Forms.StatusStrip
    Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel
    Me.TreeNodeImageList = New System.Windows.Forms.ImageList(Me.components)
    Me.ToolStrip = New System.Windows.Forms.ToolStrip
    Me.BackToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.ForwardToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
    Me.FoldersToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
    Me.ListViewToolStripButton = New System.Windows.Forms.ToolStripDropDownButton
    Me.ListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.DetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.LargeIconsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.SmallIconsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.TileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.MenuStrip = New System.Windows.Forms.MenuStrip
    Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
    Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
    Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.PrintPreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
    Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.UndoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.RedoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
    Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
    Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.StatusBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.FoldersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
    Me.ToolStripContainer = New System.Windows.Forms.ToolStripContainer
    Me.SplitContainer = New System.Windows.Forms.SplitContainer
    Me.TreeView = New System.Windows.Forms.TreeView
    Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
    Me.ListView = New System.Windows.Forms.ListView
    Me.Button1 = New System.Windows.Forms.Button
    Me.rtbDescription = New System.Windows.Forms.RichTextBox
    Me.StatusStrip.SuspendLayout()
    Me.ToolStrip.SuspendLayout()
    Me.MenuStrip.SuspendLayout()
    Me.ToolStripContainer.BottomToolStripPanel.SuspendLayout()
    Me.ToolStripContainer.ContentPanel.SuspendLayout()
    Me.ToolStripContainer.TopToolStripPanel.SuspendLayout()
    Me.ToolStripContainer.SuspendLayout()
    Me.SplitContainer.Panel1.SuspendLayout()
    Me.SplitContainer.Panel2.SuspendLayout()
    Me.SplitContainer.SuspendLayout()
    Me.SplitContainer1.Panel1.SuspendLayout()
    Me.SplitContainer1.Panel2.SuspendLayout()
    Me.SplitContainer1.SuspendLayout()
    Me.SuspendLayout()
    '
    'StatusStrip
    '
    Me.StatusStrip.Dock = System.Windows.Forms.DockStyle.None
    Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel})
    Me.StatusStrip.Location = New System.Drawing.Point(0, 0)
    Me.StatusStrip.Name = "StatusStrip"
    Me.StatusStrip.Size = New System.Drawing.Size(632, 22)
    Me.StatusStrip.TabIndex = 6
    Me.StatusStrip.Text = "StatusStrip"
    '
    'ToolStripStatusLabel
    '
    Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
    Me.ToolStripStatusLabel.Size = New System.Drawing.Size(38, 17)
    Me.ToolStripStatusLabel.Text = "Status"
    '
    'TreeNodeImageList
    '
    Me.TreeNodeImageList.ImageStream = CType(resources.GetObject("TreeNodeImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.TreeNodeImageList.TransparentColor = System.Drawing.Color.Transparent
    Me.TreeNodeImageList.Images.SetKeyName(0, "ClosedFolder")
    Me.TreeNodeImageList.Images.SetKeyName(1, "OpenFolder")
    '
    'ToolStrip
    '
    Me.ToolStrip.Anchor = System.Windows.Forms.AnchorStyles.Right
    Me.ToolStrip.Dock = System.Windows.Forms.DockStyle.None
    Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BackToolStripButton, Me.ForwardToolStripButton, Me.ToolStripSeparator7, Me.FoldersToolStripButton, Me.ToolStripSeparator8, Me.ListViewToolStripButton})
    Me.ToolStrip.Location = New System.Drawing.Point(3, 0)
    Me.ToolStrip.Name = "ToolStrip"
    Me.ToolStrip.Size = New System.Drawing.Size(210, 25)
    Me.ToolStrip.TabIndex = 0
    Me.ToolStrip.Text = "ToolStrip1"
    Me.ToolStrip.Visible = False
    '
    'BackToolStripButton
    '
    Me.BackToolStripButton.Enabled = False
    Me.BackToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.BackToolStripButton.Name = "BackToolStripButton"
    Me.BackToolStripButton.Size = New System.Drawing.Size(43, 22)
    Me.BackToolStripButton.Text = "Zurück"
    Me.BackToolStripButton.ToolTipText = "Zurück zum vorherigen Element"
    '
    'ForwardToolStripButton
    '
    Me.ForwardToolStripButton.Enabled = False
    Me.ForwardToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.ForwardToolStripButton.Name = "ForwardToolStripButton"
    Me.ForwardToolStripButton.Size = New System.Drawing.Size(54, 22)
    Me.ForwardToolStripButton.Text = "Vorwärts"
    '
    'ToolStripSeparator7
    '
    Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
    Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
    '
    'FoldersToolStripButton
    '
    Me.FoldersToolStripButton.Checked = True
    Me.FoldersToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked
    Me.FoldersToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.FoldersToolStripButton.Name = "FoldersToolStripButton"
    Me.FoldersToolStripButton.Size = New System.Drawing.Size(45, 22)
    Me.FoldersToolStripButton.Text = "Ordner"
    Me.FoldersToolStripButton.ToolTipText = "Ordneransicht umschalten"
    '
    'ToolStripSeparator8
    '
    Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
    Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
    '
    'ListViewToolStripButton
    '
    Me.ListViewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.ListViewToolStripButton.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ListToolStripMenuItem, Me.DetailsToolStripMenuItem, Me.LargeIconsToolStripMenuItem, Me.SmallIconsToolStripMenuItem, Me.TileToolStripMenuItem})
    Me.ListViewToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.ListViewToolStripButton.Name = "ListViewToolStripButton"
    Me.ListViewToolStripButton.Size = New System.Drawing.Size(13, 22)
    Me.ListViewToolStripButton.Text = "Ansichten"
    '
    'ListToolStripMenuItem
    '
    Me.ListToolStripMenuItem.Name = "ListToolStripMenuItem"
    Me.ListToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
    Me.ListToolStripMenuItem.Text = "Liste"
    '
    'DetailsToolStripMenuItem
    '
    Me.DetailsToolStripMenuItem.Checked = True
    Me.DetailsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
    Me.DetailsToolStripMenuItem.Name = "DetailsToolStripMenuItem"
    Me.DetailsToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
    Me.DetailsToolStripMenuItem.Text = "Details"
    '
    'LargeIconsToolStripMenuItem
    '
    Me.LargeIconsToolStripMenuItem.Name = "LargeIconsToolStripMenuItem"
    Me.LargeIconsToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
    Me.LargeIconsToolStripMenuItem.Text = "Große Symbole"
    '
    'SmallIconsToolStripMenuItem
    '
    Me.SmallIconsToolStripMenuItem.Name = "SmallIconsToolStripMenuItem"
    Me.SmallIconsToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
    Me.SmallIconsToolStripMenuItem.Text = "Kleine Symbole"
    '
    'TileToolStripMenuItem
    '
    Me.TileToolStripMenuItem.Name = "TileToolStripMenuItem"
    Me.TileToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
    Me.TileToolStripMenuItem.Text = "Nebeneinander"
    '
    'MenuStrip
    '
    Me.MenuStrip.Dock = System.Windows.Forms.DockStyle.None
    Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ViewToolStripMenuItem, Me.ToolsToolStripMenuItem})
    Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
    Me.MenuStrip.Name = "MenuStrip"
    Me.MenuStrip.Size = New System.Drawing.Size(632, 24)
    Me.MenuStrip.TabIndex = 0
    Me.MenuStrip.Text = "MenuStrip1"
    Me.MenuStrip.Visible = False
    '
    'FileToolStripMenuItem
    '
    Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.OpenToolStripMenuItem, Me.ToolStripSeparator1, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ToolStripSeparator2, Me.PrintToolStripMenuItem, Me.PrintPreviewToolStripMenuItem, Me.ToolStripSeparator3, Me.ExitToolStripMenuItem})
    Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
    Me.FileToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
    Me.FileToolStripMenuItem.Text = "&Datei"
    '
    'NewToolStripMenuItem
    '
    Me.NewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
    Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
    Me.NewToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
    Me.NewToolStripMenuItem.Text = "&Neu"
    '
    'OpenToolStripMenuItem
    '
    Me.OpenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
    Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
    Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
    Me.OpenToolStripMenuItem.Text = "&Öffnen"
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(170, 6)
    '
    'SaveToolStripMenuItem
    '
    Me.SaveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
    Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
    Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
    Me.SaveToolStripMenuItem.Text = "&Speichern"
    '
    'SaveAsToolStripMenuItem
    '
    Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
    Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
    Me.SaveAsToolStripMenuItem.Text = "Speichern &unter"
    '
    'ToolStripSeparator2
    '
    Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
    Me.ToolStripSeparator2.Size = New System.Drawing.Size(170, 6)
    '
    'PrintToolStripMenuItem
    '
    Me.PrintToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
    Me.PrintToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
    Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
    Me.PrintToolStripMenuItem.Text = "&Drucken"
    '
    'PrintPreviewToolStripMenuItem
    '
    Me.PrintPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.PrintPreviewToolStripMenuItem.Name = "PrintPreviewToolStripMenuItem"
    Me.PrintPreviewToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
    Me.PrintPreviewToolStripMenuItem.Text = "&Seitenansicht"
    '
    'ToolStripSeparator3
    '
    Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
    Me.ToolStripSeparator3.Size = New System.Drawing.Size(170, 6)
    '
    'ExitToolStripMenuItem
    '
    Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
    Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
    Me.ExitToolStripMenuItem.Text = "&Beenden"
    '
    'EditToolStripMenuItem
    '
    Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UndoToolStripMenuItem, Me.RedoToolStripMenuItem, Me.ToolStripSeparator4, Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.ToolStripSeparator5, Me.SelectAllToolStripMenuItem})
    Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
    Me.EditToolStripMenuItem.Size = New System.Drawing.Size(71, 20)
    Me.EditToolStripMenuItem.Text = "&Bearbeiten"
    '
    'UndoToolStripMenuItem
    '
    Me.UndoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem"
    Me.UndoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
    Me.UndoToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
    Me.UndoToolStripMenuItem.Text = "&Rückgängig"
    '
    'RedoToolStripMenuItem
    '
    Me.RedoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem"
    Me.RedoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
    Me.RedoToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
    Me.RedoToolStripMenuItem.Text = "&Wiederholen"
    '
    'ToolStripSeparator4
    '
    Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
    Me.ToolStripSeparator4.Size = New System.Drawing.Size(195, 6)
    '
    'CutToolStripMenuItem
    '
    Me.CutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
    Me.CutToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
    Me.CutToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
    Me.CutToolStripMenuItem.Text = "&Ausschneiden"
    '
    'CopyToolStripMenuItem
    '
    Me.CopyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
    Me.CopyToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
    Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
    Me.CopyToolStripMenuItem.Text = "&Kopieren"
    '
    'PasteToolStripMenuItem
    '
    Me.PasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
    Me.PasteToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
    Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
    Me.PasteToolStripMenuItem.Text = "&Einfügen"
    '
    'ToolStripSeparator5
    '
    Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
    Me.ToolStripSeparator5.Size = New System.Drawing.Size(195, 6)
    '
    'SelectAllToolStripMenuItem
    '
    Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
    Me.SelectAllToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
    Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
    Me.SelectAllToolStripMenuItem.Text = "&Alle auswählen"
    '
    'ViewToolStripMenuItem
    '
    Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolBarToolStripMenuItem, Me.StatusBarToolStripMenuItem, Me.FoldersToolStripMenuItem})
    Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
    Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
    Me.ViewToolStripMenuItem.Text = "&Ansicht"
    '
    'ToolBarToolStripMenuItem
    '
    Me.ToolBarToolStripMenuItem.Checked = True
    Me.ToolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
    Me.ToolBarToolStripMenuItem.Name = "ToolBarToolStripMenuItem"
    Me.ToolBarToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
    Me.ToolBarToolStripMenuItem.Text = "&Symbolleiste"
    '
    'StatusBarToolStripMenuItem
    '
    Me.StatusBarToolStripMenuItem.Checked = True
    Me.StatusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
    Me.StatusBarToolStripMenuItem.Name = "StatusBarToolStripMenuItem"
    Me.StatusBarToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
    Me.StatusBarToolStripMenuItem.Text = "Status&leiste"
    '
    'FoldersToolStripMenuItem
    '
    Me.FoldersToolStripMenuItem.Checked = True
    Me.FoldersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
    Me.FoldersToolStripMenuItem.Name = "FoldersToolStripMenuItem"
    Me.FoldersToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
    Me.FoldersToolStripMenuItem.Text = "&Ordner"
    '
    'ToolsToolStripMenuItem
    '
    Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OptionsToolStripMenuItem})
    Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
    Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
    Me.ToolsToolStripMenuItem.Text = "&Extras"
    '
    'OptionsToolStripMenuItem
    '
    Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
    Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
    Me.OptionsToolStripMenuItem.Text = "&Optionen"
    '
    'ToolStripContainer
    '
    '
    'ToolStripContainer.BottomToolStripPanel
    '
    Me.ToolStripContainer.BottomToolStripPanel.Controls.Add(Me.StatusStrip)
    '
    'ToolStripContainer.ContentPanel
    '
    Me.ToolStripContainer.ContentPanel.Controls.Add(Me.SplitContainer)
    Me.ToolStripContainer.ContentPanel.Size = New System.Drawing.Size(632, 193)
    Me.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ToolStripContainer.Location = New System.Drawing.Point(0, 0)
    Me.ToolStripContainer.Name = "ToolStripContainer"
    Me.ToolStripContainer.Size = New System.Drawing.Size(632, 240)
    Me.ToolStripContainer.TabIndex = 7
    Me.ToolStripContainer.Text = "ToolStripContainer1"
    '
    'ToolStripContainer.TopToolStripPanel
    '
    Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.MenuStrip)
    Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip)
    '
    'SplitContainer
    '
    Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
    Me.SplitContainer.Location = New System.Drawing.Point(0, 0)
    Me.SplitContainer.Name = "SplitContainer"
    '
    'SplitContainer.Panel1
    '
    Me.SplitContainer.Panel1.Controls.Add(Me.TreeView)
    '
    'SplitContainer.Panel2
    '
    Me.SplitContainer.Panel2.Controls.Add(Me.SplitContainer1)
    Me.SplitContainer.Size = New System.Drawing.Size(632, 193)
    Me.SplitContainer.SplitterDistance = 189
    Me.SplitContainer.TabIndex = 0
    Me.SplitContainer.Text = "SplitContainer1"
    '
    'TreeView
    '
    Me.TreeView.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TreeView.HotTracking = True
    Me.TreeView.ImageIndex = 0
    Me.TreeView.ImageList = Me.TreeNodeImageList
    Me.TreeView.LabelEdit = True
    Me.TreeView.Location = New System.Drawing.Point(0, 0)
    Me.TreeView.Name = "TreeView"
    Me.TreeView.SelectedImageIndex = 1
    Me.TreeView.ShowLines = False
    Me.TreeView.Size = New System.Drawing.Size(189, 193)
    Me.TreeView.TabIndex = 0
    '
    'SplitContainer1
    '
    Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
    Me.SplitContainer1.Name = "SplitContainer1"
    Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
    '
    'SplitContainer1.Panel1
    '
    Me.SplitContainer1.Panel1.Controls.Add(Me.ListView)
    '
    'SplitContainer1.Panel2
    '
    Me.SplitContainer1.Panel2.Controls.Add(Me.Button1)
    Me.SplitContainer1.Panel2.Controls.Add(Me.rtbDescription)
    Me.SplitContainer1.Size = New System.Drawing.Size(439, 193)
    Me.SplitContainer1.SplitterDistance = 92
    Me.SplitContainer1.TabIndex = 1
    '
    'ListView
    '
    Me.ListView.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ListView.Location = New System.Drawing.Point(0, 0)
    Me.ListView.Name = "ListView"
    Me.ListView.Size = New System.Drawing.Size(439, 92)
    Me.ListView.TabIndex = 1
    Me.ListView.UseCompatibleStateImageBehavior = False
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(109, 35)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(87, 33)
    Me.Button1.TabIndex = 1
    Me.Button1.Text = "Button1"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'rtbDescription
    '
    Me.rtbDescription.Dock = System.Windows.Forms.DockStyle.Fill
    Me.rtbDescription.EnableAutoDragDrop = True
    Me.rtbDescription.Location = New System.Drawing.Point(0, 0)
    Me.rtbDescription.Name = "rtbDescription"
    Me.rtbDescription.ReadOnly = True
    Me.rtbDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
    Me.rtbDescription.Size = New System.Drawing.Size(439, 97)
    Me.rtbDescription.TabIndex = 0
    Me.rtbDescription.Text = ""
    '
    'VocabularyExplorer
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(632, 240)
    Me.Controls.Add(Me.ToolStripContainer)
    Me.Name = "VocabularyExplorer"
    Me.Text = "Vokabel-Explorer"
    Me.StatusStrip.ResumeLayout(False)
    Me.StatusStrip.PerformLayout()
    Me.ToolStrip.ResumeLayout(False)
    Me.ToolStrip.PerformLayout()
    Me.MenuStrip.ResumeLayout(False)
    Me.MenuStrip.PerformLayout()
    Me.ToolStripContainer.BottomToolStripPanel.ResumeLayout(False)
    Me.ToolStripContainer.BottomToolStripPanel.PerformLayout()
    Me.ToolStripContainer.ContentPanel.ResumeLayout(False)
    Me.ToolStripContainer.TopToolStripPanel.ResumeLayout(False)
    Me.ToolStripContainer.TopToolStripPanel.PerformLayout()
    Me.ToolStripContainer.ResumeLayout(False)
    Me.ToolStripContainer.PerformLayout()
    Me.SplitContainer.Panel1.ResumeLayout(False)
    Me.SplitContainer.Panel2.ResumeLayout(False)
    Me.SplitContainer.ResumeLayout(False)
    Me.SplitContainer1.Panel1.ResumeLayout(False)
    Me.SplitContainer1.Panel2.ResumeLayout(False)
    Me.SplitContainer1.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
  Friend WithEvents ListView As System.Windows.Forms.ListView
  Friend WithEvents rtbDescription As System.Windows.Forms.RichTextBox
  Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
