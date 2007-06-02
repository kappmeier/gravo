<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
  Inherits MyForm

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub


    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
    Me.MenuStrip = New System.Windows.Forms.MenuStrip
    Me.FileMenu = New System.Windows.Forms.ToolStripMenuItem
    Me.ChangeUserMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator
    Me.NewMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.OpenMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
    Me.SaveMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.SaveAsMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
    Me.PrintMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.PrintPreviewMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.PrintSetupMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
    Me.ExitMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.EditMenu = New System.Windows.Forms.ToolStripMenuItem
    Me.UndoMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.RedoMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
    Me.CutMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.CopyMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.PasteMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
    Me.SelectAllMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ViewMenu = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolBarMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.StatusBarMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.VocabularyMenu = New System.Windows.Forms.ToolStripMenuItem
    Me.ExplorerMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.EnlargeDictionaryMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.InsertGroupsMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
    Me.TestMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.TestGeneralMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.TestGroupsMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.TestLanguageMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.StatisticMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ExtrasMenu = New System.Windows.Forms.ToolStripMenuItem
    Me.DataManagementMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.CheckDatabaseMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.LDFEditorMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
    Me.OptionsMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.LanguageMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.WindowsMenu = New System.Windows.Forms.ToolStripMenuItem
    Me.NewWindowMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.CascadeMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.TileVerticalMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.TileHorizontalMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.CloseAllMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ArrangeIconsMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.HelpMenu = New System.Windows.Forms.ToolStripMenuItem
    Me.ContentsMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.IndexMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.SearchMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
    Me.AboutMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStrip = New System.Windows.Forms.ToolStrip
    Me.NewToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
    Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.PrintPreviewToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
    Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.StatusStrip = New System.Windows.Forms.StatusStrip
    Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel
    Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
    Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
    Me.MenuStrip.SuspendLayout()
    Me.ToolStrip.SuspendLayout()
    Me.StatusStrip.SuspendLayout()
    Me.SuspendLayout()
    '
    'MenuStrip
    '
    Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMenu, Me.EditMenu, Me.ViewMenu, Me.VocabularyMenu, Me.ExtrasMenu, Me.WindowsMenu, Me.HelpMenu})
    Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
    Me.MenuStrip.MdiWindowListItem = Me.WindowsMenu
    Me.MenuStrip.Name = "MenuStrip"
    Me.MenuStrip.Size = New System.Drawing.Size(632, 24)
    Me.MenuStrip.TabIndex = 5
    Me.MenuStrip.Text = "MenuStrip"
    '
    'FileMenu
    '
    Me.FileMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChangeUserMenuItem, Me.ToolStripMenuItem3, Me.NewMenuItem, Me.OpenMenuItem, Me.ToolStripSeparator3, Me.SaveMenuItem, Me.SaveAsMenuItem, Me.ToolStripSeparator4, Me.PrintMenuItem, Me.PrintPreviewMenuItem, Me.PrintSetupMenuItem, Me.ToolStripSeparator5, Me.ExitMenuItem})
    Me.FileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder
    Me.FileMenu.Name = "FileMenu"
    Me.FileMenu.Size = New System.Drawing.Size(44, 20)
    Me.FileMenu.Text = "&Datei"
    '
    'ChangeUserMenuItem
    '
    Me.ChangeUserMenuItem.Name = "ChangeUserMenuItem"
    Me.ChangeUserMenuItem.Size = New System.Drawing.Size(175, 22)
    Me.ChangeUserMenuItem.Text = "Benutzer wechseln"
    '
    'ToolStripMenuItem3
    '
    Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
    Me.ToolStripMenuItem3.Size = New System.Drawing.Size(172, 6)
    '
    'NewMenuItem
    '
    Me.NewMenuItem.Image = CType(resources.GetObject("NewMenuItem.Image"), System.Drawing.Image)
    Me.NewMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.NewMenuItem.Name = "NewMenuItem"
    Me.NewMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
    Me.NewMenuItem.Size = New System.Drawing.Size(175, 22)
    Me.NewMenuItem.Text = "&Neu"
    Me.NewMenuItem.Visible = False
    '
    'OpenMenuItem
    '
    Me.OpenMenuItem.Image = CType(resources.GetObject("OpenMenuItem.Image"), System.Drawing.Image)
    Me.OpenMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.OpenMenuItem.Name = "OpenMenuItem"
    Me.OpenMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
    Me.OpenMenuItem.Size = New System.Drawing.Size(175, 22)
    Me.OpenMenuItem.Text = "&Öffnen"
    Me.OpenMenuItem.Visible = False
    '
    'ToolStripSeparator3
    '
    Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
    Me.ToolStripSeparator3.Size = New System.Drawing.Size(172, 6)
    Me.ToolStripSeparator3.Visible = False
    '
    'SaveMenuItem
    '
    Me.SaveMenuItem.Image = CType(resources.GetObject("SaveMenuItem.Image"), System.Drawing.Image)
    Me.SaveMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.SaveMenuItem.Name = "SaveMenuItem"
    Me.SaveMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
    Me.SaveMenuItem.Size = New System.Drawing.Size(175, 22)
    Me.SaveMenuItem.Text = "&Speichern"
    Me.SaveMenuItem.Visible = False
    '
    'SaveAsMenuItem
    '
    Me.SaveAsMenuItem.Name = "SaveAsMenuItem"
    Me.SaveAsMenuItem.Size = New System.Drawing.Size(175, 22)
    Me.SaveAsMenuItem.Text = "Speichern &unter"
    '
    'ToolStripSeparator4
    '
    Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
    Me.ToolStripSeparator4.Size = New System.Drawing.Size(172, 6)
    '
    'PrintMenuItem
    '
    Me.PrintMenuItem.Image = CType(resources.GetObject("PrintMenuItem.Image"), System.Drawing.Image)
    Me.PrintMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.PrintMenuItem.Name = "PrintMenuItem"
    Me.PrintMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
    Me.PrintMenuItem.Size = New System.Drawing.Size(175, 22)
    Me.PrintMenuItem.Text = "&Drucken"
    '
    'PrintPreviewMenuItem
    '
    Me.PrintPreviewMenuItem.Image = CType(resources.GetObject("PrintPreviewMenuItem.Image"), System.Drawing.Image)
    Me.PrintPreviewMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.PrintPreviewMenuItem.Name = "PrintPreviewMenuItem"
    Me.PrintPreviewMenuItem.Size = New System.Drawing.Size(175, 22)
    Me.PrintPreviewMenuItem.Text = "&Seitenansicht"
    Me.PrintPreviewMenuItem.Visible = False
    '
    'PrintSetupMenuItem
    '
    Me.PrintSetupMenuItem.Name = "PrintSetupMenuItem"
    Me.PrintSetupMenuItem.Size = New System.Drawing.Size(175, 22)
    Me.PrintSetupMenuItem.Text = "Druckeinrichtung"
    '
    'ToolStripSeparator5
    '
    Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
    Me.ToolStripSeparator5.Size = New System.Drawing.Size(172, 6)
    '
    'ExitMenuItem
    '
    Me.ExitMenuItem.Name = "ExitMenuItem"
    Me.ExitMenuItem.Size = New System.Drawing.Size(175, 22)
    Me.ExitMenuItem.Text = "&Beenden"
    '
    'EditMenu
    '
    Me.EditMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UndoMenuItem, Me.RedoMenuItem, Me.ToolStripSeparator6, Me.CutMenuItem, Me.CopyMenuItem, Me.PasteMenuItem, Me.ToolStripSeparator7, Me.SelectAllMenuItem})
    Me.EditMenu.Name = "EditMenu"
    Me.EditMenu.Size = New System.Drawing.Size(71, 20)
    Me.EditMenu.Text = "&Bearbeiten"
    Me.EditMenu.Visible = False
    '
    'UndoMenuItem
    '
    Me.UndoMenuItem.Image = CType(resources.GetObject("UndoMenuItem.Image"), System.Drawing.Image)
    Me.UndoMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.UndoMenuItem.Name = "UndoMenuItem"
    Me.UndoMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
    Me.UndoMenuItem.Size = New System.Drawing.Size(198, 22)
    Me.UndoMenuItem.Text = "&Rückgängig"
    '
    'RedoMenuItem
    '
    Me.RedoMenuItem.Image = CType(resources.GetObject("RedoMenuItem.Image"), System.Drawing.Image)
    Me.RedoMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.RedoMenuItem.Name = "RedoMenuItem"
    Me.RedoMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
    Me.RedoMenuItem.Size = New System.Drawing.Size(198, 22)
    Me.RedoMenuItem.Text = "&Wiederholen"
    '
    'ToolStripSeparator6
    '
    Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
    Me.ToolStripSeparator6.Size = New System.Drawing.Size(195, 6)
    '
    'CutMenuItem
    '
    Me.CutMenuItem.Image = CType(resources.GetObject("CutMenuItem.Image"), System.Drawing.Image)
    Me.CutMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.CutMenuItem.Name = "CutMenuItem"
    Me.CutMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
    Me.CutMenuItem.Size = New System.Drawing.Size(198, 22)
    Me.CutMenuItem.Text = "&Ausschneiden"
    '
    'CopyMenuItem
    '
    Me.CopyMenuItem.Image = CType(resources.GetObject("CopyMenuItem.Image"), System.Drawing.Image)
    Me.CopyMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.CopyMenuItem.Name = "CopyMenuItem"
    Me.CopyMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
    Me.CopyMenuItem.Size = New System.Drawing.Size(198, 22)
    Me.CopyMenuItem.Text = "&Kopieren"
    '
    'PasteMenuItem
    '
    Me.PasteMenuItem.Image = CType(resources.GetObject("PasteMenuItem.Image"), System.Drawing.Image)
    Me.PasteMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.PasteMenuItem.Name = "PasteMenuItem"
    Me.PasteMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
    Me.PasteMenuItem.Size = New System.Drawing.Size(198, 22)
    Me.PasteMenuItem.Text = "&Einfügen"
    '
    'ToolStripSeparator7
    '
    Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
    Me.ToolStripSeparator7.Size = New System.Drawing.Size(195, 6)
    '
    'SelectAllMenuItem
    '
    Me.SelectAllMenuItem.Name = "SelectAllMenuItem"
    Me.SelectAllMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
    Me.SelectAllMenuItem.Size = New System.Drawing.Size(198, 22)
    Me.SelectAllMenuItem.Text = "&Alle auswählen"
    '
    'ViewMenu
    '
    Me.ViewMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolBarMenuItem, Me.StatusBarMenuItem})
    Me.ViewMenu.Name = "ViewMenu"
    Me.ViewMenu.Size = New System.Drawing.Size(54, 20)
    Me.ViewMenu.Text = "&Ansicht"
    '
    'ToolBarMenuItem
    '
    Me.ToolBarMenuItem.Checked = True
    Me.ToolBarMenuItem.CheckOnClick = True
    Me.ToolBarMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
    Me.ToolBarMenuItem.Name = "ToolBarMenuItem"
    Me.ToolBarMenuItem.Size = New System.Drawing.Size(144, 22)
    Me.ToolBarMenuItem.Text = "&Symbolleiste"
    '
    'StatusBarMenuItem
    '
    Me.StatusBarMenuItem.Checked = True
    Me.StatusBarMenuItem.CheckOnClick = True
    Me.StatusBarMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
    Me.StatusBarMenuItem.Name = "StatusBarMenuItem"
    Me.StatusBarMenuItem.Size = New System.Drawing.Size(144, 22)
    Me.StatusBarMenuItem.Text = "Status&leiste"
    '
    'VocabularyMenu
    '
    Me.VocabularyMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExplorerMenuItem, Me.EnlargeDictionaryMenuItem, Me.InsertGroupsMenuItem, Me.ToolStripMenuItem2, Me.TestMenuItem, Me.StatisticMenuItem})
    Me.VocabularyMenu.Name = "VocabularyMenu"
    Me.VocabularyMenu.Size = New System.Drawing.Size(62, 20)
    Me.VocabularyMenu.Text = "&Vokabeln"
    '
    'ExplorerMenuItem
    '
    Me.ExplorerMenuItem.Name = "ExplorerMenuItem"
    Me.ExplorerMenuItem.Size = New System.Drawing.Size(191, 22)
    Me.ExplorerMenuItem.Text = "&Explorer"
    '
    'EnlargeDictionaryMenuItem
    '
    Me.EnlargeDictionaryMenuItem.Name = "EnlargeDictionaryMenuItem"
    Me.EnlargeDictionaryMenuItem.Size = New System.Drawing.Size(191, 22)
    Me.EnlargeDictionaryMenuItem.Text = "&Wörterbuch erweitern"
    '
    'InsertGroupsMenuItem
    '
    Me.InsertGroupsMenuItem.Name = "InsertGroupsMenuItem"
    Me.InsertGroupsMenuItem.Size = New System.Drawing.Size(191, 22)
    Me.InsertGroupsMenuItem.Text = "&Gruppen eingeben"
    '
    'ToolStripMenuItem2
    '
    Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
    Me.ToolStripMenuItem2.Size = New System.Drawing.Size(188, 6)
    '
    'TestMenuItem
    '
    Me.TestMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TestGeneralMenuItem, Me.TestGroupsMenuItem, Me.TestLanguageMenuItem})
    Me.TestMenuItem.Name = "TestMenuItem"
    Me.TestMenuItem.Size = New System.Drawing.Size(191, 22)
    Me.TestMenuItem.Text = "Abfragen"
    '
    'TestGeneralMenuItem
    '
    Me.TestGeneralMenuItem.Name = "TestGeneralMenuItem"
    Me.TestGeneralMenuItem.Size = New System.Drawing.Size(173, 22)
    Me.TestGeneralMenuItem.Text = "&Allgemein"
    '
    'TestGroupsMenuItem
    '
    Me.TestGroupsMenuItem.Name = "TestGroupsMenuItem"
    Me.TestGroupsMenuItem.Size = New System.Drawing.Size(173, 22)
    Me.TestGroupsMenuItem.Text = "&Gruppen abfragen"
    '
    'TestLanguageMenuItem
    '
    Me.TestLanguageMenuItem.Name = "TestLanguageMenuItem"
    Me.TestLanguageMenuItem.Size = New System.Drawing.Size(173, 22)
    Me.TestLanguageMenuItem.Text = "&Sprache abfragen"
    '
    'StatisticMenuItem
    '
    Me.StatisticMenuItem.Name = "StatisticMenuItem"
    Me.StatisticMenuItem.Size = New System.Drawing.Size(191, 22)
    Me.StatisticMenuItem.Text = "Abfragestatistik"
    '
    'ExtrasMenu
    '
    Me.ExtrasMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DataManagementMenuItem, Me.CheckDatabaseMenuItem, Me.LDFEditorMenuItem, Me.ToolStripMenuItem1, Me.OptionsMenuItem, Me.LanguageMenuItem})
    Me.ExtrasMenu.Name = "ExtrasMenu"
    Me.ExtrasMenu.Size = New System.Drawing.Size(50, 20)
    Me.ExtrasMenu.Text = "&Extras"
    '
    'DataManagementMenuItem
    '
    Me.DataManagementMenuItem.Name = "DataManagementMenuItem"
    Me.DataManagementMenuItem.Size = New System.Drawing.Size(195, 22)
    Me.DataManagementMenuItem.Text = "Daten-Management ..."
    '
    'CheckDatabaseMenuItem
    '
    Me.CheckDatabaseMenuItem.Name = "CheckDatabaseMenuItem"
    Me.CheckDatabaseMenuItem.Size = New System.Drawing.Size(195, 22)
    Me.CheckDatabaseMenuItem.Text = "Datenbank überprüfen"
    '
    'LDFEditorMenuItem
    '
    Me.LDFEditorMenuItem.Name = "LDFEditorMenuItem"
    Me.LDFEditorMenuItem.Size = New System.Drawing.Size(195, 22)
    Me.LDFEditorMenuItem.Text = "LDF-Editor ..."
    '
    'ToolStripMenuItem1
    '
    Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    Me.ToolStripMenuItem1.Size = New System.Drawing.Size(192, 6)
    '
    'OptionsMenuItem
    '
    Me.OptionsMenuItem.Name = "OptionsMenuItem"
    Me.OptionsMenuItem.Size = New System.Drawing.Size(195, 22)
    Me.OptionsMenuItem.Text = "&Optionen ..."
    '
    'LanguageMenuItem
    '
    Me.LanguageMenuItem.Name = "LanguageMenuItem"
    Me.LanguageMenuItem.Size = New System.Drawing.Size(195, 22)
    Me.LanguageMenuItem.Text = "Sprache"
    '
    'WindowsMenu
    '
    Me.WindowsMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewWindowMenuItem, Me.CascadeMenuItem, Me.TileVerticalMenuItem, Me.TileHorizontalMenuItem, Me.CloseAllMenuItem, Me.ArrangeIconsMenuItem})
    Me.WindowsMenu.Name = "WindowsMenu"
    Me.WindowsMenu.Size = New System.Drawing.Size(56, 20)
    Me.WindowsMenu.Text = "&Fenster"
    '
    'NewWindowMenuItem
    '
    Me.NewWindowMenuItem.Name = "NewWindowMenuItem"
    Me.NewWindowMenuItem.Size = New System.Drawing.Size(174, 22)
    Me.NewWindowMenuItem.Text = "&Neues Fenster"
    Me.NewWindowMenuItem.Visible = False
    '
    'CascadeMenuItem
    '
    Me.CascadeMenuItem.Name = "CascadeMenuItem"
    Me.CascadeMenuItem.Size = New System.Drawing.Size(174, 22)
    Me.CascadeMenuItem.Text = "Ü&berlappend"
    '
    'TileVerticalMenuItem
    '
    Me.TileVerticalMenuItem.Name = "TileVerticalMenuItem"
    Me.TileVerticalMenuItem.Size = New System.Drawing.Size(174, 22)
    Me.TileVerticalMenuItem.Text = "&Nebeneinander"
    '
    'TileHorizontalMenuItem
    '
    Me.TileHorizontalMenuItem.Name = "TileHorizontalMenuItem"
    Me.TileHorizontalMenuItem.Size = New System.Drawing.Size(174, 22)
    Me.TileHorizontalMenuItem.Text = "&Untereinander"
    '
    'CloseAllMenuItem
    '
    Me.CloseAllMenuItem.Name = "CloseAllMenuItem"
    Me.CloseAllMenuItem.Size = New System.Drawing.Size(174, 22)
    Me.CloseAllMenuItem.Text = "&Alle schließen"
    '
    'ArrangeIconsMenuItem
    '
    Me.ArrangeIconsMenuItem.Name = "ArrangeIconsMenuItem"
    Me.ArrangeIconsMenuItem.Size = New System.Drawing.Size(174, 22)
    Me.ArrangeIconsMenuItem.Text = "Symbole &anordnen"
    '
    'HelpMenu
    '
    Me.HelpMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContentsMenuItem, Me.IndexMenuItem, Me.SearchMenuItem, Me.ToolStripSeparator8, Me.AboutMenuItem})
    Me.HelpMenu.Name = "HelpMenu"
    Me.HelpMenu.Size = New System.Drawing.Size(40, 20)
    Me.HelpMenu.Text = "&Hilfe"
    '
    'ContentsMenuItem
    '
    Me.ContentsMenuItem.Name = "ContentsMenuItem"
    Me.ContentsMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F1), System.Windows.Forms.Keys)
    Me.ContentsMenuItem.Size = New System.Drawing.Size(160, 22)
    Me.ContentsMenuItem.Text = "&Inhalt"
    Me.ContentsMenuItem.Visible = False
    '
    'IndexMenuItem
    '
    Me.IndexMenuItem.Image = CType(resources.GetObject("IndexMenuItem.Image"), System.Drawing.Image)
    Me.IndexMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.IndexMenuItem.Name = "IndexMenuItem"
    Me.IndexMenuItem.Size = New System.Drawing.Size(160, 22)
    Me.IndexMenuItem.Text = "&Index"
    Me.IndexMenuItem.Visible = False
    '
    'SearchMenuItem
    '
    Me.SearchMenuItem.Image = CType(resources.GetObject("SearchMenuItem.Image"), System.Drawing.Image)
    Me.SearchMenuItem.ImageTransparentColor = System.Drawing.Color.Black
    Me.SearchMenuItem.Name = "SearchMenuItem"
    Me.SearchMenuItem.Size = New System.Drawing.Size(160, 22)
    Me.SearchMenuItem.Text = "&Suchen"
    Me.SearchMenuItem.Visible = False
    '
    'ToolStripSeparator8
    '
    Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
    Me.ToolStripSeparator8.Size = New System.Drawing.Size(157, 6)
    Me.ToolStripSeparator8.Visible = False
    '
    'AboutMenuItem
    '
    Me.AboutMenuItem.Name = "AboutMenuItem"
    Me.AboutMenuItem.Size = New System.Drawing.Size(160, 22)
    Me.AboutMenuItem.Text = "&Info ..."
    '
    'ToolStrip
    '
    Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripButton, Me.OpenToolStripButton, Me.SaveToolStripButton, Me.ToolStripSeparator1, Me.PrintToolStripButton, Me.PrintPreviewToolStripButton, Me.ToolStripSeparator2, Me.HelpToolStripButton})
    Me.ToolStrip.Location = New System.Drawing.Point(0, 24)
    Me.ToolStrip.Name = "ToolStrip"
    Me.ToolStrip.Size = New System.Drawing.Size(632, 25)
    Me.ToolStrip.TabIndex = 6
    Me.ToolStrip.Text = "ToolStrip"
    '
    'NewToolStripButton
    '
    Me.NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.NewToolStripButton.Image = CType(resources.GetObject("NewToolStripButton.Image"), System.Drawing.Image)
    Me.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.NewToolStripButton.Name = "NewToolStripButton"
    Me.NewToolStripButton.Size = New System.Drawing.Size(23, 22)
    Me.NewToolStripButton.Text = "Neu"
    Me.NewToolStripButton.Visible = False
    '
    'OpenToolStripButton
    '
    Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
    Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.OpenToolStripButton.Name = "OpenToolStripButton"
    Me.OpenToolStripButton.Size = New System.Drawing.Size(23, 22)
    Me.OpenToolStripButton.Text = "Öffnen"
    '
    'SaveToolStripButton
    '
    Me.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.SaveToolStripButton.Image = CType(resources.GetObject("SaveToolStripButton.Image"), System.Drawing.Image)
    Me.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.SaveToolStripButton.Name = "SaveToolStripButton"
    Me.SaveToolStripButton.Size = New System.Drawing.Size(23, 22)
    Me.SaveToolStripButton.Text = "Speichern"
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
    '
    'PrintToolStripButton
    '
    Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
    Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.PrintToolStripButton.Name = "PrintToolStripButton"
    Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
    Me.PrintToolStripButton.Text = "Drucken"
    '
    'PrintPreviewToolStripButton
    '
    Me.PrintPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.PrintPreviewToolStripButton.Image = CType(resources.GetObject("PrintPreviewToolStripButton.Image"), System.Drawing.Image)
    Me.PrintPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.PrintPreviewToolStripButton.Name = "PrintPreviewToolStripButton"
    Me.PrintPreviewToolStripButton.Size = New System.Drawing.Size(23, 22)
    Me.PrintPreviewToolStripButton.Text = "Seitenansicht"
    Me.PrintPreviewToolStripButton.Visible = False
    '
    'ToolStripSeparator2
    '
    Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
    Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
    '
    'HelpToolStripButton
    '
    Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
    Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.HelpToolStripButton.Name = "HelpToolStripButton"
    Me.HelpToolStripButton.Size = New System.Drawing.Size(23, 22)
    Me.HelpToolStripButton.Text = "Hilfe"
    '
    'StatusStrip
    '
    Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel})
    Me.StatusStrip.Location = New System.Drawing.Point(0, 239)
    Me.StatusStrip.Name = "StatusStrip"
    Me.StatusStrip.Size = New System.Drawing.Size(632, 22)
    Me.StatusStrip.TabIndex = 7
    Me.StatusStrip.Text = "StatusStrip"
    '
    'ToolStripStatusLabel
    '
    Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
    Me.ToolStripStatusLabel.Size = New System.Drawing.Size(38, 17)
    Me.ToolStripStatusLabel.Text = "Status"
    '
    'Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(632, 261)
    Me.Controls.Add(Me.ToolStrip)
    Me.Controls.Add(Me.MenuStrip)
    Me.Controls.Add(Me.StatusStrip)
    Me.IsMdiContainer = True
    Me.MainMenuStrip = Me.MenuStrip
    Me.Name = "Main"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "#"
    Me.MenuStrip.ResumeLayout(False)
    Me.MenuStrip.PerformLayout()
    Me.ToolStrip.ResumeLayout(False)
    Me.ToolStrip.PerformLayout()
    Me.StatusStrip.ResumeLayout(False)
    Me.StatusStrip.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ContentsMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents HelpMenu As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents IndexMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents SearchMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents AboutMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ArrangeIconsMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents CloseAllMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents NewWindowMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents WindowsMenu As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents CascadeMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents TileVerticalMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents TileHorizontalMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents OptionsMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents PrintPreviewToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
  Friend WithEvents ToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
  Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents NewToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
  Friend WithEvents OpenToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents PrintPreviewMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents PrintMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ExitMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents PrintSetupMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents SaveAsMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents NewMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents FileMenu As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents OpenMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents SaveMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
  Friend WithEvents EditMenu As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents UndoMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents RedoMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents CutMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents CopyMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents PasteMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents SelectAllMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ViewMenu As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolBarMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents StatusBarMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ExtrasMenu As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
  Friend WithEvents DataManagementMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ChangeUserMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents VocabularyMenu As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ExplorerMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents EnlargeDictionaryMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents InsertGroupsMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents TestMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents TestGeneralMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents TestGroupsMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents TestLanguageMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents StatisticMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents CheckDatabaseMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents LDFEditorMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents LanguageMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
