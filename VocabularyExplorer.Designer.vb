<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class VocabularyExplorer
    Inherits MDIChild

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VocabularyExplorer))
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.BottomToolStripPanel = New System.Windows.Forms.ToolStripPanel
        Me.TopToolStripPanel = New System.Windows.Forms.ToolStripPanel
        Me.RightToolStripPanel = New System.Windows.Forms.ToolStripPanel
        Me.LeftToolStripPanel = New System.Windows.Forms.ToolStripPanel
        Me.ContentPanel = New System.Windows.Forms.ToolStripContentPanel
        Me.ToolStripContainer = New System.Windows.Forms.ToolStripContainer
        Me.SplitContainer = New System.Windows.Forms.SplitContainer
        Me.TreeView = New System.Windows.Forms.TreeView
        Me.TreeNodeImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.ListView = New System.Windows.Forms.ListView
        Me.PanelWordInfo = New System.Windows.Forms.Panel
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.PanelWordInfoInner = New System.Windows.Forms.Panel
        Me.txtMainLanguage = New System.Windows.Forms.TextBox
        Me.txtLanguage = New System.Windows.Forms.TextBox
        Me.lblLanguage = New System.Windows.Forms.Label
        Me.lblMainLanguage = New System.Windows.Forms.Label
        Me.chkMarked = New System.Windows.Forms.CheckBox
        Me.chkAddToGroup = New System.Windows.Forms.CheckBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.txtMainEntry = New System.Windows.Forms.TextBox
        Me.txtMeaning = New System.Windows.Forms.TextBox
        Me.txtAdditionalTargetLangInfo = New System.Windows.Forms.TextBox
        Me.txtPre = New System.Windows.Forms.TextBox
        Me.txtPost = New System.Windows.Forms.TextBox
        Me.txtWord = New System.Windows.Forms.TextBox
        Me.lstWordType = New System.Windows.Forms.ListBox
        Me.lblMainEntry = New System.Windows.Forms.Label
        Me.cmdChangeWord = New System.Windows.Forms.Button
        Me.lblWordType = New System.Windows.Forms.Label
        Me.lblMeaning = New System.Windows.Forms.Label
        Me.lblAdditionalTargetLangInfo = New System.Windows.Forms.Label
        Me.chkIrregular = New System.Windows.Forms.CheckBox
        Me.lblWord = New System.Windows.Forms.Label
        Me.lblPost = New System.Windows.Forms.Label
        Me.lblPre = New System.Windows.Forms.Label
        Me.PanelMultiEdit = New System.Windows.Forms.Panel
        Me.chkEnableMultiMeaning = New System.Windows.Forms.CheckBox
        Me.chkEnableMultiMarked = New System.Windows.Forms.CheckBox
        Me.chkEnableMultiPre = New System.Windows.Forms.CheckBox
        Me.chkEnableMultiPost = New System.Windows.Forms.CheckBox
        Me.chkEnableMultiAdditionalTargetLangInfo = New System.Windows.Forms.CheckBox
        Me.chkEnableMultiIrregular = New System.Windows.Forms.CheckBox
        Me.chkEnableMultiWordType = New System.Windows.Forms.CheckBox
        Me.chkEnableMultiWord = New System.Windows.Forms.CheckBox
        Me.chkEnableMultiMainEntry = New System.Windows.Forms.CheckBox
        Me.chkMultiMarked = New System.Windows.Forms.CheckBox
        Me.txtMultiMainEntry = New System.Windows.Forms.TextBox
        Me.txtMultiMeaning = New System.Windows.Forms.TextBox
        Me.txtMultiAdditionaltargetLangInfo = New System.Windows.Forms.TextBox
        Me.txtMultiPre = New System.Windows.Forms.TextBox
        Me.txtMultiPost = New System.Windows.Forms.TextBox
        Me.txtMultiWord = New System.Windows.Forms.TextBox
        Me.lstMultiWordType = New System.Windows.Forms.ListBox
        Me.cmdMultiChange = New System.Windows.Forms.Button
        Me.chkMultiIrregular = New System.Windows.Forms.CheckBox
        Me.MainMenuStrip = New System.Windows.Forms.MenuStrip
        Me.PanelsMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.PanelViewDefaultMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PanelViewInputMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PanelViewMultiMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PanelViewSearchMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripContainer.ContentPanel.SuspendLayout()
        Me.ToolStripContainer.SuspendLayout()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.PanelWordInfo.SuspendLayout()
        Me.PanelWordInfoInner.SuspendLayout()
        Me.PanelMultiEdit.SuspendLayout()
        Me.MainMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'BottomToolStripPanel
        '
        Me.BottomToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.BottomToolStripPanel.Name = "BottomToolStripPanel"
        Me.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.BottomToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.BottomToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'TopToolStripPanel
        '
        Me.TopToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopToolStripPanel.Name = "TopToolStripPanel"
        Me.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.TopToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.TopToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'RightToolStripPanel
        '
        Me.RightToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.RightToolStripPanel.Name = "RightToolStripPanel"
        Me.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.RightToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.RightToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'LeftToolStripPanel
        '
        Me.LeftToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.LeftToolStripPanel.Name = "LeftToolStripPanel"
        Me.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.LeftToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LeftToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'ContentPanel
        '
        Me.ContentPanel.Size = New System.Drawing.Size(547, 452)
        '
        'ToolStripContainer
        '
        '
        'ToolStripContainer.ContentPanel
        '
        Me.ToolStripContainer.ContentPanel.Controls.Add(Me.SplitContainer)
        Me.ToolStripContainer.ContentPanel.Size = New System.Drawing.Size(547, 541)
        Me.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer.Location = New System.Drawing.Point(0, 24)
        Me.ToolStripContainer.Name = "ToolStripContainer"
        Me.ToolStripContainer.Size = New System.Drawing.Size(547, 566)
        Me.ToolStripContainer.TabIndex = 7
        Me.ToolStripContainer.Text = "ToolStripContainer1"
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
        Me.SplitContainer.Size = New System.Drawing.Size(547, 541)
        Me.SplitContainer.SplitterDistance = 124
        Me.SplitContainer.TabIndex = 0
        Me.SplitContainer.Text = "SplitContainer1"
        '
        'TreeView
        '
        Me.TreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView.HotTracking = True
        Me.TreeView.ImageIndex = 1
        Me.TreeView.ImageList = Me.TreeNodeImageList
        Me.TreeView.LabelEdit = True
        Me.TreeView.Location = New System.Drawing.Point(0, 0)
        Me.TreeView.Name = "TreeView"
        Me.TreeView.SelectedImageIndex = 0
        Me.TreeView.ShowLines = False
        Me.TreeView.Size = New System.Drawing.Size(124, 541)
        Me.TreeView.TabIndex = 0
        '
        'TreeNodeImageList
        '
        Me.TreeNodeImageList.ImageStream = CType(resources.GetObject("TreeNodeImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TreeNodeImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.TreeNodeImageList.Images.SetKeyName(0, "OpenFolder")
        Me.TreeNodeImageList.Images.SetKeyName(1, "ClosedFolder")
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.PanelWordInfo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PanelMultiEdit)
        Me.SplitContainer1.Size = New System.Drawing.Size(419, 541)
        Me.SplitContainer1.SplitterDistance = 94
        Me.SplitContainer1.TabIndex = 1
        '
        'ListView
        '
        Me.ListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView.FullRowSelect = True
        Me.ListView.Location = New System.Drawing.Point(0, 0)
        Me.ListView.Name = "ListView"
        Me.ListView.Size = New System.Drawing.Size(419, 94)
        Me.ListView.TabIndex = 1
        Me.ListView.UseCompatibleStateImageBehavior = False
        Me.ListView.View = System.Windows.Forms.View.Details
        '
        'PanelWordInfo
        '
        Me.PanelWordInfo.Controls.Add(Me.ListView1)
        Me.PanelWordInfo.Controls.Add(Me.PanelWordInfoInner)
        Me.PanelWordInfo.Location = New System.Drawing.Point(0, 0)
        Me.PanelWordInfo.Name = "PanelWordInfo"
        Me.PanelWordInfo.Size = New System.Drawing.Size(417, 376)
        Me.PanelWordInfo.TabIndex = 41
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.Location = New System.Drawing.Point(0, 1)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(455, 156)
        Me.ListView1.TabIndex = 40
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        Me.ListView1.Visible = False
        '
        'PanelWordInfoInner
        '
        Me.PanelWordInfoInner.Controls.Add(Me.txtMainLanguage)
        Me.PanelWordInfoInner.Controls.Add(Me.txtLanguage)
        Me.PanelWordInfoInner.Controls.Add(Me.lblLanguage)
        Me.PanelWordInfoInner.Controls.Add(Me.lblMainLanguage)
        Me.PanelWordInfoInner.Controls.Add(Me.chkMarked)
        Me.PanelWordInfoInner.Controls.Add(Me.chkAddToGroup)
        Me.PanelWordInfoInner.Controls.Add(Me.Label9)
        Me.PanelWordInfoInner.Controls.Add(Me.cmdAdd)
        Me.PanelWordInfoInner.Controls.Add(Me.txtMainEntry)
        Me.PanelWordInfoInner.Controls.Add(Me.txtMeaning)
        Me.PanelWordInfoInner.Controls.Add(Me.txtAdditionalTargetLangInfo)
        Me.PanelWordInfoInner.Controls.Add(Me.txtPre)
        Me.PanelWordInfoInner.Controls.Add(Me.txtPost)
        Me.PanelWordInfoInner.Controls.Add(Me.txtWord)
        Me.PanelWordInfoInner.Controls.Add(Me.lstWordType)
        Me.PanelWordInfoInner.Controls.Add(Me.lblMainEntry)
        Me.PanelWordInfoInner.Controls.Add(Me.cmdChangeWord)
        Me.PanelWordInfoInner.Controls.Add(Me.lblWordType)
        Me.PanelWordInfoInner.Controls.Add(Me.lblMeaning)
        Me.PanelWordInfoInner.Controls.Add(Me.lblAdditionalTargetLangInfo)
        Me.PanelWordInfoInner.Controls.Add(Me.chkIrregular)
        Me.PanelWordInfoInner.Controls.Add(Me.lblWord)
        Me.PanelWordInfoInner.Controls.Add(Me.lblPost)
        Me.PanelWordInfoInner.Controls.Add(Me.lblPre)
        Me.PanelWordInfoInner.Location = New System.Drawing.Point(0, 0)
        Me.PanelWordInfoInner.Name = "PanelWordInfoInner"
        Me.PanelWordInfoInner.Size = New System.Drawing.Size(390, 373)
        Me.PanelWordInfoInner.TabIndex = 39
        '
        'txtMainLanguage
        '
        Me.txtMainLanguage.Location = New System.Drawing.Point(73, 321)
        Me.txtMainLanguage.Name = "txtMainLanguage"
        Me.txtMainLanguage.Size = New System.Drawing.Size(100, 20)
        Me.txtMainLanguage.TabIndex = 62
        '
        'txtLanguage
        '
        Me.txtLanguage.Location = New System.Drawing.Point(73, 295)
        Me.txtLanguage.Name = "txtLanguage"
        Me.txtLanguage.Size = New System.Drawing.Size(100, 20)
        Me.txtLanguage.TabIndex = 61
        '
        'lblLanguage
        '
        Me.lblLanguage.AutoSize = True
        Me.lblLanguage.Location = New System.Drawing.Point(5, 298)
        Me.lblLanguage.Name = "lblLanguage"
        Me.lblLanguage.Size = New System.Drawing.Size(39, 13)
        Me.lblLanguage.TabIndex = 60
        Me.lblLanguage.Text = "Label2"
        '
        'lblMainLanguage
        '
        Me.lblMainLanguage.AutoSize = True
        Me.lblMainLanguage.Location = New System.Drawing.Point(5, 324)
        Me.lblMainLanguage.Name = "lblMainLanguage"
        Me.lblMainLanguage.Size = New System.Drawing.Size(39, 13)
        Me.lblMainLanguage.TabIndex = 59
        Me.lblMainLanguage.Text = "Label1"
        '
        'chkMarked
        '
        Me.chkMarked.AutoSize = True
        Me.chkMarked.Location = New System.Drawing.Point(73, 249)
        Me.chkMarked.Name = "chkMarked"
        Me.chkMarked.Size = New System.Drawing.Size(73, 17)
        Me.chkMarked.TabIndex = 51
        Me.chkMarked.Text = "Markieren"
        Me.chkMarked.UseVisualStyleBackColor = True
        '
        'chkAddToGroup
        '
        Me.chkAddToGroup.AutoSize = True
        Me.chkAddToGroup.Location = New System.Drawing.Point(73, 272)
        Me.chkAddToGroup.Name = "chkAddToGroup"
        Me.chkAddToGroup.Size = New System.Drawing.Size(170, 17)
        Me.chkAddToGroup.TabIndex = 52
        Me.chkAddToGroup.Text = "Eintrag der Gruppe hinzufügen"
        Me.chkAddToGroup.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(199, 100)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(140, 88)
        Me.Label9.TabIndex = 58
        Me.Label9.Text = "Neue Haupteinträge werden nicht in die TreeView eingefügt! Dazu muß das Fenster e" &
                "inmal geschlossen und erneut geöffnet werden."
        '
        'cmdAdd
        '
        Me.cmdAdd.Location = New System.Drawing.Point(264, 191)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(75, 23)
        Me.cmdAdd.TabIndex = 54
        Me.cmdAdd.Text = "&Hinzufügen"
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'txtMainEntry
        '
        Me.txtMainEntry.Location = New System.Drawing.Point(73, 223)
        Me.txtMainEntry.Name = "txtMainEntry"
        Me.txtMainEntry.Size = New System.Drawing.Size(120, 20)
        Me.txtMainEntry.TabIndex = 50
        '
        'txtMeaning
        '
        Me.txtMeaning.Location = New System.Drawing.Point(73, 76)
        Me.txtMeaning.Name = "txtMeaning"
        Me.txtMeaning.Size = New System.Drawing.Size(198, 20)
        Me.txtMeaning.TabIndex = 45
        '
        'txtAdditionalTargetLangInfo
        '
        Me.txtAdditionalTargetLangInfo.Location = New System.Drawing.Point(73, 50)
        Me.txtAdditionalTargetLangInfo.Name = "txtAdditionalTargetLangInfo"
        Me.txtAdditionalTargetLangInfo.Size = New System.Drawing.Size(198, 20)
        Me.txtAdditionalTargetLangInfo.TabIndex = 43
        '
        'txtPre
        '
        Me.txtPre.Location = New System.Drawing.Point(4, 21)
        Me.txtPre.Name = "txtPre"
        Me.txtPre.Size = New System.Drawing.Size(62, 20)
        Me.txtPre.TabIndex = 40
        '
        'txtPost
        '
        Me.txtPost.Location = New System.Drawing.Point(277, 21)
        Me.txtPost.Name = "txtPost"
        Me.txtPost.Size = New System.Drawing.Size(62, 20)
        Me.txtPost.TabIndex = 42
        '
        'txtWord
        '
        Me.txtWord.Location = New System.Drawing.Point(73, 21)
        Me.txtWord.Name = "txtWord"
        Me.txtWord.Size = New System.Drawing.Size(198, 20)
        Me.txtWord.TabIndex = 41
        '
        'lstWordType
        '
        Me.lstWordType.FormattingEnabled = True
        Me.lstWordType.Location = New System.Drawing.Point(73, 122)
        Me.lstWordType.Name = "lstWordType"
        Me.lstWordType.Size = New System.Drawing.Size(120, 95)
        Me.lstWordType.TabIndex = 49
        '
        'lblMainEntry
        '
        Me.lblMainEntry.AutoSize = True
        Me.lblMainEntry.Location = New System.Drawing.Point(5, 226)
        Me.lblMainEntry.Name = "lblMainEntry"
        Me.lblMainEntry.Size = New System.Drawing.Size(71, 13)
        Me.lblMainEntry.TabIndex = 39
        Me.lblMainEntry.Text = "Haupteintrag:"
        '
        'cmdChangeWord
        '
        Me.cmdChangeWord.Location = New System.Drawing.Point(264, 220)
        Me.cmdChangeWord.Name = "cmdChangeWord"
        Me.cmdChangeWord.Size = New System.Drawing.Size(75, 23)
        Me.cmdChangeWord.TabIndex = 56
        Me.cmdChangeWord.Text = "Ä&ndern"
        Me.cmdChangeWord.UseVisualStyleBackColor = True
        '
        'lblWordType
        '
        Me.lblWordType.AutoSize = True
        Me.lblWordType.Location = New System.Drawing.Point(5, 122)
        Me.lblWordType.Name = "lblWordType"
        Me.lblWordType.Size = New System.Drawing.Size(45, 13)
        Me.lblWordType.TabIndex = 57
        Me.lblWordType.Text = "Wortart:"
        '
        'lblMeaning
        '
        Me.lblMeaning.AutoSize = True
        Me.lblMeaning.Location = New System.Drawing.Point(5, 76)
        Me.lblMeaning.Name = "lblMeaning"
        Me.lblMeaning.Size = New System.Drawing.Size(62, 13)
        Me.lblMeaning.TabIndex = 55
        Me.lblMeaning.Text = "Bedeutung:"
        '
        'lblAdditionalTargetLangInfo
        '
        Me.lblAdditionalTargetLangInfo.AutoSize = True
        Me.lblAdditionalTargetLangInfo.Location = New System.Drawing.Point(5, 50)
        Me.lblAdditionalTargetLangInfo.Name = "lblAdditionalTargetLangInfo"
        Me.lblAdditionalTargetLangInfo.Size = New System.Drawing.Size(59, 13)
        Me.lblAdditionalTargetLangInfo.TabIndex = 53
        Me.lblAdditionalTargetLangInfo.Text = "Zusatzinfo:"
        '
        'chkIrregular
        '
        Me.chkIrregular.AutoSize = True
        Me.chkIrregular.Location = New System.Drawing.Point(73, 99)
        Me.chkIrregular.Name = "chkIrregular"
        Me.chkIrregular.Size = New System.Drawing.Size(64, 17)
        Me.chkIrregular.TabIndex = 47
        Me.chkIrregular.Text = "Irregulär"
        Me.chkIrregular.UseVisualStyleBackColor = True
        '
        'lblWord
        '
        Me.lblWord.AutoSize = True
        Me.lblWord.Location = New System.Drawing.Point(70, 5)
        Me.lblWord.Name = "lblWord"
        Me.lblWord.Size = New System.Drawing.Size(49, 13)
        Me.lblWord.TabIndex = 48
        Me.lblWord.Text = "Vokabel:"
        '
        'lblPost
        '
        Me.lblPost.AutoSize = True
        Me.lblPost.Location = New System.Drawing.Point(277, 5)
        Me.lblPost.Name = "lblPost"
        Me.lblPost.Size = New System.Drawing.Size(36, 13)
        Me.lblPost.TabIndex = 46
        Me.lblPost.Text = "Nach:"
        '
        'lblPre
        '
        Me.lblPre.AutoSize = True
        Me.lblPre.Location = New System.Drawing.Point(5, 5)
        Me.lblPre.Name = "lblPre"
        Me.lblPre.Size = New System.Drawing.Size(26, 13)
        Me.lblPre.TabIndex = 44
        Me.lblPre.Text = "Vor:"
        '
        'PanelMultiEdit
        '
        Me.PanelMultiEdit.Controls.Add(Me.chkEnableMultiMeaning)
        Me.PanelMultiEdit.Controls.Add(Me.chkEnableMultiMarked)
        Me.PanelMultiEdit.Controls.Add(Me.chkEnableMultiPre)
        Me.PanelMultiEdit.Controls.Add(Me.chkEnableMultiPost)
        Me.PanelMultiEdit.Controls.Add(Me.chkEnableMultiAdditionalTargetLangInfo)
        Me.PanelMultiEdit.Controls.Add(Me.chkEnableMultiIrregular)
        Me.PanelMultiEdit.Controls.Add(Me.chkEnableMultiWordType)
        Me.PanelMultiEdit.Controls.Add(Me.chkEnableMultiWord)
        Me.PanelMultiEdit.Controls.Add(Me.chkEnableMultiMainEntry)
        Me.PanelMultiEdit.Controls.Add(Me.chkMultiMarked)
        Me.PanelMultiEdit.Controls.Add(Me.txtMultiMainEntry)
        Me.PanelMultiEdit.Controls.Add(Me.txtMultiMeaning)
        Me.PanelMultiEdit.Controls.Add(Me.txtMultiAdditionaltargetLangInfo)
        Me.PanelMultiEdit.Controls.Add(Me.txtMultiPre)
        Me.PanelMultiEdit.Controls.Add(Me.txtMultiPost)
        Me.PanelMultiEdit.Controls.Add(Me.txtMultiWord)
        Me.PanelMultiEdit.Controls.Add(Me.lstMultiWordType)
        Me.PanelMultiEdit.Controls.Add(Me.cmdMultiChange)
        Me.PanelMultiEdit.Controls.Add(Me.chkMultiIrregular)
        Me.PanelMultiEdit.Location = New System.Drawing.Point(20, 30)
        Me.PanelMultiEdit.Name = "PanelMultiEdit"
        Me.PanelMultiEdit.Size = New System.Drawing.Size(164, 147)
        Me.PanelMultiEdit.TabIndex = 42
        '
        'chkEnableMultiMeaning
        '
        Me.chkEnableMultiMeaning.AutoSize = True
        Me.chkEnableMultiMeaning.Location = New System.Drawing.Point(3, 109)
        Me.chkEnableMultiMeaning.Name = "chkEnableMultiMeaning"
        Me.chkEnableMultiMeaning.Size = New System.Drawing.Size(81, 17)
        Me.chkEnableMultiMeaning.TabIndex = 9
        Me.chkEnableMultiMeaning.Text = "Bedeutung:"
        Me.chkEnableMultiMeaning.UseVisualStyleBackColor = True
        '
        'chkEnableMultiMarked
        '
        Me.chkEnableMultiMarked.AutoSize = True
        Me.chkEnableMultiMarked.Location = New System.Drawing.Point(3, 283)
        Me.chkEnableMultiMarked.Name = "chkEnableMultiMarked"
        Me.chkEnableMultiMarked.Size = New System.Drawing.Size(76, 17)
        Me.chkEnableMultiMarked.TabIndex = 17
        Me.chkEnableMultiMarked.Text = "Markieren:"
        Me.chkEnableMultiMarked.UseVisualStyleBackColor = True
        '
        'chkEnableMultiPre
        '
        Me.chkEnableMultiPre.AutoSize = True
        Me.chkEnableMultiPre.Location = New System.Drawing.Point(3, 5)
        Me.chkEnableMultiPre.Name = "chkEnableMultiPre"
        Me.chkEnableMultiPre.Size = New System.Drawing.Size(45, 17)
        Me.chkEnableMultiPre.TabIndex = 1
        Me.chkEnableMultiPre.Text = "Vor:"
        Me.chkEnableMultiPre.UseVisualStyleBackColor = True
        '
        'chkEnableMultiPost
        '
        Me.chkEnableMultiPost.AutoSize = True
        Me.chkEnableMultiPost.Location = New System.Drawing.Point(3, 57)
        Me.chkEnableMultiPost.Name = "chkEnableMultiPost"
        Me.chkEnableMultiPost.Size = New System.Drawing.Size(55, 17)
        Me.chkEnableMultiPost.TabIndex = 5
        Me.chkEnableMultiPost.Text = "Nach:"
        Me.chkEnableMultiPost.UseVisualStyleBackColor = True
        '
        'chkEnableMultiAdditionalTargetLangInfo
        '
        Me.chkEnableMultiAdditionalTargetLangInfo.AutoSize = True
        Me.chkEnableMultiAdditionalTargetLangInfo.Location = New System.Drawing.Point(3, 86)
        Me.chkEnableMultiAdditionalTargetLangInfo.Name = "chkEnableMultiAdditionalTargetLangInfo"
        Me.chkEnableMultiAdditionalTargetLangInfo.Size = New System.Drawing.Size(97, 17)
        Me.chkEnableMultiAdditionalTargetLangInfo.TabIndex = 7
        Me.chkEnableMultiAdditionalTargetLangInfo.Text = "Erweiterte Info:"
        Me.chkEnableMultiAdditionalTargetLangInfo.UseVisualStyleBackColor = True
        '
        'chkEnableMultiIrregular
        '
        Me.chkEnableMultiIrregular.AutoSize = True
        Me.chkEnableMultiIrregular.Location = New System.Drawing.Point(3, 133)
        Me.chkEnableMultiIrregular.Name = "chkEnableMultiIrregular"
        Me.chkEnableMultiIrregular.Size = New System.Drawing.Size(67, 17)
        Me.chkEnableMultiIrregular.TabIndex = 11
        Me.chkEnableMultiIrregular.Text = "Irregulär:"
        Me.chkEnableMultiIrregular.UseVisualStyleBackColor = True
        '
        'chkEnableMultiWordType
        '
        Me.chkEnableMultiWordType.AutoSize = True
        Me.chkEnableMultiWordType.Location = New System.Drawing.Point(3, 156)
        Me.chkEnableMultiWordType.Name = "chkEnableMultiWordType"
        Me.chkEnableMultiWordType.Size = New System.Drawing.Size(64, 17)
        Me.chkEnableMultiWordType.TabIndex = 13
        Me.chkEnableMultiWordType.Text = "Wortart:"
        Me.chkEnableMultiWordType.UseVisualStyleBackColor = True
        '
        'chkEnableMultiWord
        '
        Me.chkEnableMultiWord.AutoSize = True
        Me.chkEnableMultiWord.Location = New System.Drawing.Point(3, 31)
        Me.chkEnableMultiWord.Name = "chkEnableMultiWord"
        Me.chkEnableMultiWord.Size = New System.Drawing.Size(52, 17)
        Me.chkEnableMultiWord.TabIndex = 3
        Me.chkEnableMultiWord.Text = "Wort:"
        Me.chkEnableMultiWord.UseVisualStyleBackColor = True
        '
        'chkEnableMultiMainEntry
        '
        Me.chkEnableMultiMainEntry.AutoSize = True
        Me.chkEnableMultiMainEntry.Location = New System.Drawing.Point(3, 258)
        Me.chkEnableMultiMainEntry.Name = "chkEnableMultiMainEntry"
        Me.chkEnableMultiMainEntry.Size = New System.Drawing.Size(90, 17)
        Me.chkEnableMultiMainEntry.TabIndex = 15
        Me.chkEnableMultiMainEntry.Text = "Haupteintrag:"
        Me.chkEnableMultiMainEntry.UseVisualStyleBackColor = True
        '
        'chkMultiMarked
        '
        Me.chkMultiMarked.AutoSize = True
        Me.chkMultiMarked.Enabled = False
        Me.chkMultiMarked.Location = New System.Drawing.Point(109, 283)
        Me.chkMultiMarked.Name = "chkMultiMarked"
        Me.chkMultiMarked.Size = New System.Drawing.Size(73, 17)
        Me.chkMultiMarked.TabIndex = 18
        Me.chkMultiMarked.Text = "Markieren"
        Me.chkMultiMarked.UseVisualStyleBackColor = True
        '
        'txtMultiMainEntry
        '
        Me.txtMultiMainEntry.Enabled = False
        Me.txtMultiMainEntry.Location = New System.Drawing.Point(109, 257)
        Me.txtMultiMainEntry.Name = "txtMultiMainEntry"
        Me.txtMultiMainEntry.Size = New System.Drawing.Size(120, 20)
        Me.txtMultiMainEntry.TabIndex = 16
        '
        'txtMultiMeaning
        '
        Me.txtMultiMeaning.Enabled = False
        Me.txtMultiMeaning.Location = New System.Drawing.Point(109, 107)
        Me.txtMultiMeaning.Name = "txtMultiMeaning"
        Me.txtMultiMeaning.Size = New System.Drawing.Size(198, 20)
        Me.txtMultiMeaning.TabIndex = 10
        '
        'txtMultiAdditionaltargetLangInfo
        '
        Me.txtMultiAdditionaltargetLangInfo.Enabled = False
        Me.txtMultiAdditionaltargetLangInfo.Location = New System.Drawing.Point(109, 81)
        Me.txtMultiAdditionaltargetLangInfo.Name = "txtMultiAdditionaltargetLangInfo"
        Me.txtMultiAdditionaltargetLangInfo.Size = New System.Drawing.Size(198, 20)
        Me.txtMultiAdditionaltargetLangInfo.TabIndex = 8
        '
        'txtMultiPre
        '
        Me.txtMultiPre.Enabled = False
        Me.txtMultiPre.Location = New System.Drawing.Point(109, 3)
        Me.txtMultiPre.Name = "txtMultiPre"
        Me.txtMultiPre.Size = New System.Drawing.Size(62, 20)
        Me.txtMultiPre.TabIndex = 2
        '
        'txtMultiPost
        '
        Me.txtMultiPost.Enabled = False
        Me.txtMultiPost.Location = New System.Drawing.Point(109, 55)
        Me.txtMultiPost.Name = "txtMultiPost"
        Me.txtMultiPost.Size = New System.Drawing.Size(62, 20)
        Me.txtMultiPost.TabIndex = 6
        '
        'txtMultiWord
        '
        Me.txtMultiWord.Enabled = False
        Me.txtMultiWord.Location = New System.Drawing.Point(109, 29)
        Me.txtMultiWord.Name = "txtMultiWord"
        Me.txtMultiWord.Size = New System.Drawing.Size(198, 20)
        Me.txtMultiWord.TabIndex = 4
        '
        'lstMultiWordType
        '
        Me.lstMultiWordType.Enabled = False
        Me.lstMultiWordType.FormattingEnabled = True
        Me.lstMultiWordType.Location = New System.Drawing.Point(109, 156)
        Me.lstMultiWordType.Name = "lstMultiWordType"
        Me.lstMultiWordType.Size = New System.Drawing.Size(120, 95)
        Me.lstMultiWordType.TabIndex = 14
        '
        'cmdMultiChange
        '
        Me.cmdMultiChange.Location = New System.Drawing.Point(262, 224)
        Me.cmdMultiChange.Name = "cmdMultiChange"
        Me.cmdMultiChange.Size = New System.Drawing.Size(75, 23)
        Me.cmdMultiChange.TabIndex = 19
        Me.cmdMultiChange.Text = "Ä&ndern"
        Me.cmdMultiChange.UseVisualStyleBackColor = True
        '
        'chkMultiIrregular
        '
        Me.chkMultiIrregular.AutoSize = True
        Me.chkMultiIrregular.Enabled = False
        Me.chkMultiIrregular.Location = New System.Drawing.Point(109, 133)
        Me.chkMultiIrregular.Name = "chkMultiIrregular"
        Me.chkMultiIrregular.Size = New System.Drawing.Size(64, 17)
        Me.chkMultiIrregular.TabIndex = 12
        Me.chkMultiIrregular.Text = "Irregulär"
        Me.chkMultiIrregular.UseVisualStyleBackColor = True
        '
        'MainMenuStrip
        '
        Me.MainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PanelsMenu})
        Me.MainMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MainMenuStrip.Name = "MainMenuStrip"
        Me.MainMenuStrip.Size = New System.Drawing.Size(547, 24)
        Me.MainMenuStrip.TabIndex = 9
        Me.MainMenuStrip.Text = "MainMenuStrip"
        '
        'PanelsMenu
        '
        Me.PanelsMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PanelViewDefaultMenuItem, Me.PanelViewInputMenuItem, Me.PanelViewMultiMenuItem, Me.PanelViewSearchMenuItem})
        Me.PanelsMenu.Name = "PanelsMenu"
        Me.PanelsMenu.Size = New System.Drawing.Size(50, 20)
        Me.PanelsMenu.Text = "Panels"
        '
        'PanelViewDefaultMenuItem
        '
        Me.PanelViewDefaultMenuItem.Checked = True
        Me.PanelViewDefaultMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.PanelViewDefaultMenuItem.Name = "PanelViewDefaultMenuItem"
        Me.PanelViewDefaultMenuItem.ShortcutKeyDisplayString = ""
        Me.PanelViewDefaultMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.PanelViewDefaultMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.PanelViewDefaultMenuItem.Text = "Default"
        '
        'PanelViewInputMenuItem
        '
        Me.PanelViewInputMenuItem.Name = "PanelViewInputMenuItem"
        Me.PanelViewInputMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.PanelViewInputMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.PanelViewInputMenuItem.Text = "Wort-Eingabe"
        '
        'PanelViewMultiMenuItem
        '
        Me.PanelViewMultiMenuItem.Name = "PanelViewMultiMenuItem"
        Me.PanelViewMultiMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.PanelViewMultiMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.PanelViewMultiMenuItem.Text = "Multi-Eingabe"
        '
        'PanelViewSearchMenuItem
        '
        Me.PanelViewSearchMenuItem.Name = "PanelViewSearchMenuItem"
        Me.PanelViewSearchMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.PanelViewSearchMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.PanelViewSearchMenuItem.Text = "Suchen"
        '
        'VocabularyExplorer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(547, 590)
        Me.Controls.Add(Me.ToolStripContainer)
        Me.Controls.Add(Me.MainMenuStrip)
        Me.Name = "VocabularyExplorer"
        Me.Text = "Vokabel-Explorer"
        Me.ToolStripContainer.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer.ResumeLayout(False)
        Me.ToolStripContainer.PerformLayout()
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        Me.SplitContainer.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.PanelWordInfo.ResumeLayout(False)
        Me.PanelWordInfoInner.ResumeLayout(False)
        Me.PanelWordInfoInner.PerformLayout()
        Me.PanelMultiEdit.ResumeLayout(False)
        Me.PanelMultiEdit.PerformLayout()
        Me.MainMenuStrip.ResumeLayout(False)
        Me.MainMenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents BottomToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents TopToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents RightToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents LeftToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents ContentPanel As System.Windows.Forms.ToolStripContentPanel
    Friend WithEvents ToolStripContainer As System.Windows.Forms.ToolStripContainer
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents TreeView As System.Windows.Forms.TreeView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ListView As System.Windows.Forms.ListView
    Friend WithEvents PanelWordInfo As System.Windows.Forms.Panel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents PanelMultiEdit As System.Windows.Forms.Panel
    Friend WithEvents chkMultiMarked As System.Windows.Forms.CheckBox
    Friend WithEvents txtMultiMainEntry As System.Windows.Forms.TextBox
    Friend WithEvents txtMultiMeaning As System.Windows.Forms.TextBox
    Friend WithEvents txtMultiAdditionaltargetLangInfo As System.Windows.Forms.TextBox
    Friend WithEvents txtMultiPre As System.Windows.Forms.TextBox
    Friend WithEvents txtMultiPost As System.Windows.Forms.TextBox
    Friend WithEvents txtMultiWord As System.Windows.Forms.TextBox
    Friend WithEvents lstMultiWordType As System.Windows.Forms.ListBox
    Friend WithEvents cmdMultiChange As System.Windows.Forms.Button
    Friend WithEvents chkMultiIrregular As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableMultiPre As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableMultiPost As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableMultiAdditionalTargetLangInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableMultiIrregular As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableMultiWordType As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableMultiWord As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableMultiMainEntry As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableMultiMeaning As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableMultiMarked As System.Windows.Forms.CheckBox
    Friend WithEvents TreeNodeImageList As System.Windows.Forms.ImageList
    Friend Shadows WithEvents MainMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents PanelsMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PanelViewDefaultMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PanelViewInputMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PanelViewMultiMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PanelViewSearchMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PanelWordInfoInner As System.Windows.Forms.Panel
    Friend WithEvents txtMainLanguage As System.Windows.Forms.TextBox
    Friend WithEvents txtLanguage As System.Windows.Forms.TextBox
    Friend WithEvents lblLanguage As System.Windows.Forms.Label
    Friend WithEvents lblMainLanguage As System.Windows.Forms.Label
    Friend WithEvents chkMarked As System.Windows.Forms.CheckBox
    Friend WithEvents chkAddToGroup As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents txtMainEntry As System.Windows.Forms.TextBox
    Friend WithEvents txtMeaning As System.Windows.Forms.TextBox
    Friend WithEvents txtAdditionalTargetLangInfo As System.Windows.Forms.TextBox
    Friend WithEvents txtPre As System.Windows.Forms.TextBox
    Friend WithEvents txtPost As System.Windows.Forms.TextBox
    Friend WithEvents txtWord As System.Windows.Forms.TextBox
    Friend WithEvents lstWordType As System.Windows.Forms.ListBox
    Friend WithEvents lblMainEntry As System.Windows.Forms.Label
    Friend WithEvents cmdChangeWord As System.Windows.Forms.Button
    Friend WithEvents lblWordType As System.Windows.Forms.Label
    Friend WithEvents lblMeaning As System.Windows.Forms.Label
    Friend WithEvents lblAdditionalTargetLangInfo As System.Windows.Forms.Label
    Friend WithEvents chkIrregular As System.Windows.Forms.CheckBox
    Friend WithEvents lblWord As System.Windows.Forms.Label
    Friend WithEvents lblPost As System.Windows.Forms.Label
    Friend WithEvents lblPre As System.Windows.Forms.Label
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader

End Class
