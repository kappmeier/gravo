Imports System.Windows.Forms
Imports Gravo.localization

Public Class Main
    Dim programSettings As Settings

    Dim m_windowSettings As WindowSettings
    Dim mainWindowLoaded As Boolean = False
    Dim childWindowLoaded As Boolean = True

    Dim frmGroupInput As GroupInput
    Dim frmVocabularyExplorer As VocabularyExplorer
    Dim frmStatistic As Statistic

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs) Handles NewMenuItem.Click, NewToolStripButton.Click, NewWindowMenuItem.Click
        ' Neue Instanz des untergeordneten Formulars erstellen.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Vor der Anzeige dem MDI-Formular unterordnen.
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "Fenster " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenMenuItem.Click, OpenToolStripButton.Click
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Textdateien (*.txt)|*.txt|Alle Dateien (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Hier Code zum Öffnen der Datei hinzufügen.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsMenuItem.Click
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Datenbanken (*.mdb)|*.mdb|Alle Dateien (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            Try
                FileCopy(Application.StartupPath() & "\voc.mdb", SaveFileDialog.FileName)
            Catch ex As Exception
                MsgBox("Beim Kopieren ist ein Fehler aufgetreten: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
            End Try
        End If
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitMenuItem.Click
        Global.System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CutMenuItem.Click
        ' Mithilfe von My.Computer.Clipboard den ausgewählten Text bzw. die ausgewählten Bilder in die Zwischenablage kopieren
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CopyMenuItem.Click
        ' Mithilfe von My.Computer.Clipboard den ausgewählten Text bzw. die ausgewählten Bilder in die Zwischenablage kopieren
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PasteMenuItem.Click
        'Mithilfe von My.Computer.Clipboard.GetText() oder My.Computer.Clipboard.GetData Informationen aus der Zwischenablage abrufen
    End Sub

    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolBarMenuItem.Click
        Me.ToolStrip.Visible = Me.ToolBarMenuItem.Checked
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarMenuItem.Click
        Me.StatusStrip.Visible = Me.StatusBarMenuItem.Checked
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllMenuItem.Click
        ' Alle untergeordneten Formulare des übergeordneten Formulars schließen.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer = 0

    ' Formular
    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Text = AppTitleShort

        ' Lade verfügbare Sprachen
        Dim languages As ToolStripMenuItem = Me.LanguageMenuItem
        Dim firstItem As myMenu = Nothing
        Dim languageItem As myMenu
        For Each language As String In GetLoc.GetLanguageNames()
            languageItem = New myMenu
            languageItem.Text = language
            languageItem.Tag = language
            languageItem.Checked = False
            languageItem.CheckOnClick = True
            languageItem.CheckState = System.Windows.Forms.CheckState.Unchecked
            languageItem.MainForm = Me
            languages.DropDownItems.Add(languageItem)
            If language = "Deutsch" Then
                GetLoc.SwitchToLanguage(language)
                languageItem.Checked = True
                languageItem.CheckState = CheckState.Checked
            End If
        Next language

        ' Lokalisierung
        LocalizationChanged()

        ' Settings laden
        programSettings = New Settings()
        programSettings.LoadSettings()
        m_windowSettings = programSettings.MainWindowSettings
        If programSettings.SaveWindowPosition Then
            Me.Location = m_windowSettings.position
            Me.Width = m_windowSettings.width
            Me.Height = m_windowSettings.height
            If programSettings.MainWindowState = FormWindowState.Maximized Then Me.WindowState = FormWindowState.Maximized Else Me.WindowState = programSettings.MainWindowState
        End If
        mainWindowLoaded = True

        ' Initialisieren
        Dim db As New SQLiteDataBaseOperation()
        db.Open(DBPath)
        Dim man As New xlsManagement(db)
        If Not man.IsVersionUpToDate Then
            MsgBox(GetLoc.GetText(DB_VERSION_OUTDATED), MsgBoxStyle.Information, GetLoc.GetText(HINT))
        End If
        Me.ExplorerMenuItem.PerformClick()
    End Sub

    Private Sub Main_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        If Not mainWindowLoaded Then Exit Sub
        If WindowState = FormWindowState.Normal Then
            m_windowSettings.position = Me.Location
        End If
        programSettings.MainWindowState = Me.WindowState
        programSettings.MainWindowSettings = m_windowSettings
        programSettings.SaveSettings()
    End Sub

    Private Sub Main_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Not mainWindowLoaded Then Exit Sub
        If WindowState = FormWindowState.Normal Then
            m_windowSettings.width = Me.Width
            m_windowSettings.height = Me.Height
        End If
        programSettings.MainWindowState = Me.WindowState
        programSettings.MainWindowSettings = m_windowSettings
        programSettings.SaveSettings()
    End Sub

    ' Lokalisierung
    Public Sub LocalizationChangeLanguage(ByVal newLanguage As String)
        For Each language As myMenu In LanguageMenuItem.DropDownItems
            If language.Tag = newLanguage Then
                GetLoc.SwitchToLanguage(newLanguage)
            Else
                language.Checked = False
                language.CheckState = CheckState.Unchecked
            End If
        Next language

        LocalizationChanged()
    End Sub

    Public Overrides Sub LocalizationChanged()
        ' Lade die Menü-Einträge in der Sprache
        Me.FileMenu.Text = GetLoc.GetText(MAIN_MENU_FILE)
        Me.EditMenu.Text = GetLoc.GetText(MAIN_MENU_EDIT)
        Me.ViewMenu.Text = GetLoc.GetText(MAIN_MENU_VIEW)
        Me.VocabularyMenu.Text = GetLoc.GetText(MAIN_MENU_VOCABULARY)
        Me.ExtrasMenu.Text = GetLoc.GetText(MAIN_MENU_EXTRAS)
        Me.WindowsMenu.Text = GetLoc.GetText(MAIN_MENU_WINDOWS)
        Me.HelpMenu.Text = GetLoc.GetText(MAIN_MENU_HELP)

        Me.ChangeUserMenuItem.Text = GetLoc.GetText(MAIN_MENU_FILE_CHANGE_USER)
        Me.NewMenuItem.Text = GetLoc.GetText(MAIN_MENU_FILE_NEW)
        Me.OpenMenuItem.Text = GetLoc.GetText(MAIN_MENU_FILE_OPEN)
        Me.SaveMenuItem.Text = GetLoc.GetText(MAIN_MENU_FILE_SAVE)
        Me.SaveAsMenuItem.Text = GetLoc.GetText(MAIN_MENU_FILE_SAVE_AS)
        Me.PrintMenuItem.Text = GetLoc.GetText(MAIN_MENU_FILE_PRINT)
        Me.PrintPreviewMenuItem.Text = GetLoc.GetText(MAIN_MENU_FILE_PRINT_PREVIEW)
        Me.PrintSetupMenuItem.Text = GetLoc.GetText(MAIN_MENU_FILE_PRINT_SETUP)
        Me.ExitMenuItem.Text = GetLoc.GetText(MAIN_MENU_FILE_EXIT)

        Me.UndoMenuItem.Text = GetLoc.GetText(MAIN_MENU_EDIT_UNDO)
        Me.RedoMenuItem.Text = GetLoc.GetText(MAIN_MENU_EDIT_REDO)
        Me.CutMenuItem.Text = GetLoc.GetText(MAIN_MENU_EDIT_CUT)
        Me.CopyMenuItem.Text = GetLoc.GetText(MAIN_MENU_EDIT_COPY)
        Me.PasteMenuItem.Text = GetLoc.GetText(MAIN_MENU_EDIT_PASTE)
        Me.SelectAllMenuItem.Text = GetLoc.GetText(MAIN_MENU_EDIT_SELECT_ALL)

        Me.ToolBarMenuItem.Text = GetLoc.GetText(MAIN_MENU_VIEW_TOOL_BAR)
        Me.StatusBarMenuItem.Text = GetLoc.GetText(MAIN_MENU_VIEW_STATUS_BAR)

        Me.ExplorerMenuItem.Text = GetLoc.GetText(MAIN_MENU_VOCABULARY_EXPLORER)
        Me.EnlargeDictionaryMenuItem.Text = GetLoc.GetText(MAIN_MENU_VOCABULARY_ENLARGE_DICTIONARY)
        Me.InsertGroupsMenuItem.Text = GetLoc.GetText(MAIN_MENU_VOCABULARY_INSERT_GROUPS)
        Me.TestMenuItem.Text = GetLoc.GetText(MAIN_MENU_VOCABULARY_TEST)
        Me.TestGeneralMenuItem.Text = GetLoc.GetText(MAIN_MENU_VOCABULARY_TEST_GENERAL)
        Me.TestGroupsMenuItem.Text = GetLoc.GetText(MAIN_MENU_VOCABULARY_TEST_GROUPS)
        Me.TestLanguageMenuItem.Text = GetLoc.GetText(MAIN_MENU_VOCABULARY_TEST_LANGUAGE)
        Me.StatisticMenuItem.Text = GetLoc.GetText(MAIN_MENU_VOCABULARY_STATISTIC)

        Me.DataManagementMenuItem.Text = GetLoc.GetText(MAIN_MENU_EXTRAS_DATA_MANAGEMENT)
        Me.CheckDatabaseMenuItem.Text = GetLoc.GetText(MAIN_MENU_EXTRAS_CHECK_DATABASE)
        Me.LDFEditorMenuItem.Text = GetLoc.GetText(MAIN_MENU_EXTRAS_LDF_EDITOR)
        Me.OptionsMenuItem.Text = GetLoc.GetText(MAIN_MENU_EXTRAS_OPTIONS)
        Me.LanguageMenuItem.Text = GetLoc.GetText(MAIN_MENU_EXTRAS_LANGUAGE)

        Me.NewWindowMenuItem.Text = GetLoc.GetText(MAIN_MENU_WINDOWS_NEW)
        Me.CascadeMenuItem.Text = GetLoc.GetText(MAIN_MENU_WINDOWS_CASCADE)
        Me.TileVerticalMenuItem.Text = GetLoc.GetText(MAIN_MENU_WINDOWS_TILE_VERTICAL)
        Me.TileHorizontalMenuItem.Text = GetLoc.GetText(MAIN_MENU_WINDOWS_TILE_HORIZONTAL)
        Me.CloseAllMenuItem.Text = GetLoc.GetText(MAIN_MENU_WINDOWS_CLOSE_ALL)
        Me.ArrangeIconsMenuItem.Text = GetLoc.GetText(MAIN_MENU_WINDOWS_ARRANGE_ICONS)

        Me.ContentsMenuItem.Text = GetLoc.GetText(MAIN_MENU_HELP_CONTENT)
        Me.IndexMenuItem.Text = GetLoc.GetText(MAIN_MENU_HELP_INDEX)
        Me.SearchMenuItem.Text = GetLoc.GetText(MAIN_MENU_HELP_SEARCH)
        Me.AboutMenuItem.Text = GetLoc.GetText(MAIN_MENU_HELP_ABOUT)

        For Each ChildForm As MyForm In Me.MdiChildren
            ChildForm.LocalizationChanged()
        Next ChildForm
    End Sub

    ' Menüeinträge
    Private Sub ExplorerMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExplorerMenuItem.Click
        childWindowLoaded = False
        If frmVocabularyExplorer Is Nothing Then
            frmVocabularyExplorer = New VocabularyExplorer()
            frmVocabularyExplorer.MdiParent = Me
        Else
            If frmVocabularyExplorer.IsDisposed Then
                frmVocabularyExplorer = New VocabularyExplorer()
                frmVocabularyExplorer.MdiParent = Me
            End If
        End If
        frmVocabularyExplorer.Show()
        If programSettings.SaveWindowPosition Then
            frmVocabularyExplorer.SetBounds(programSettings.ExplorerWindowSettings.position.X, programSettings.ExplorerWindowSettings.position.Y, programSettings.ExplorerWindowSettings.width, programSettings.ExplorerWindowSettings.height)
            frmVocabularyExplorer.WindowState = programSettings.ChildWindowState
        End If
        childWindowLoaded = True
    End Sub

    Private Sub WörterbuchErweiternToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnlargeDictionaryMenuItem.Click
        Dim frmDictionaryInput As New WordInput
        frmDictionaryInput.ShowDialog(Me)
    End Sub

    Private Sub GruppenEingebenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InsertGroupsMenuItem.Click
        childWindowLoaded = False
        If frmGroupInput Is Nothing Then
            frmGroupInput = New GroupInput()
            frmGroupInput.MdiParent = Me
        End If
        frmGroupInput.Show()
        If programSettings.SaveWindowPosition Then
            frmGroupInput.SetBounds(programSettings.GroupWindowSettings.position.X, programSettings.GroupWindowSettings.position.Y, programSettings.GroupWindowSettings.width, programSettings.GroupWindowSettings.height)
            frmGroupInput.WindowState = programSettings.ChildWindowState
        End If
        childWindowLoaded = True
    End Sub

    Private Sub AbfragestatistikToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatisticMenuItem.Click
        childWindowLoaded = False
        If frmStatistic Is Nothing Then
            frmStatistic = New Statistic()
            frmStatistic.MdiParent = Me
        End If
        frmStatistic.Show()
        If programSettings.SaveWindowPosition Then
            frmStatistic.SetBounds(programSettings.StatisticWindowSettings.position.X, programSettings.StatisticWindowSettings.position.Y, programSettings.StatisticWindowSettings.width, programSettings.StatisticWindowSettings.height)
            frmStatistic.WindowState = programSettings.ChildWindowState
        End If
        childWindowLoaded = True
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutMenuItem.Click
        Dim frmInfo As New Info
        frmInfo.ShowDialog(Me)
    End Sub

    Private Sub DatenManagementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataManagementMenuItem.Click
        Dim frmManagement As New Management
        frmManagement.ShowDialog(Me)
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsMenuItem.Click
        Dim dlgOptions As New Options()
        dlgOptions.TestFormerLanguage = programSettings.TestFormerLanguage
        dlgOptions.TestSetPhrases = programSettings.TestSetPhrases
        dlgOptions.SaveWindowPosition = programSettings.SaveWindowPosition
        dlgOptions.UseCards = programSettings.UseCards
        dlgOptions.CardsInitialInterval = programSettings.CardsInitialInterval
        Dim res As DialogResult = dlgOptions.ShowDialog(Me)
        If res = Windows.Forms.DialogResult.OK Then
            ' Einstellungen speichern
            programSettings.TestFormerLanguage = dlgOptions.TestFormerLanguage
            programSettings.TestSetPhrases = dlgOptions.TestSetPhrases
            programSettings.SaveWindowPosition = dlgOptions.SaveWindowPosition
            programSettings.UseCards = dlgOptions.UseCards
            programSettings.CardsInitialInterval = dlgOptions.CardsInitialInterval
            programSettings.SaveSettings()
        Else
            ' verwerfen
            Exit Sub
        End If
    End Sub

    Private Sub AllgemeinToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestGeneralMenuItem.Click
        ' Abfragen von Vokabeln aus dem Programm, ohne einschränkung, evtl. Sprache
        Dim frmTest As New TestSimple(True, "italian", Me)
        frmTest.Show(Me)
    End Sub

    Private Sub GruppenAbfragenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestGroupsMenuItem.Click
        TestFinished()
    End Sub

    Private Sub SpracheAbfragenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestLanguageMenuItem.Click
        ' Abfragen von Vokabeln aus dem Programm, ohne einschränkung
        Dim frmTest As New TestSimple(False, "", Me)
        frmTest.Show(Me)
    End Sub

    Private Sub DatenbankÜberprüfenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckDatabaseMenuItem.Click
        Dim ret As DialogResult = MsgBox("Es wird versucht, Fehler automatisch zu Beheben. Für weitere Möglichkeiten benutzen Sie bitte das Datenbank-Management. Wollen Sie den Test jetzt durchführen? Der Vorgang kann einige Minuten dauern.", MsgBoxStyle.YesNo, "Hinweis")
        If ret = Windows.Forms.DialogResult.No Then Exit Sub

        Dim db As DataBaseOperation = New SQLiteDataBaseOperation()
        db.Open(DBPath)

        Dim man As New xlsManagement(db)

        man.Reorganize()
        If man.ErrorCount > 0 Then
            MsgBox("Testen der Datenbank auf Konsistenz abgeschlossen. Es wurden " & man.ErrorCount & " Fehler behoben.", MsgBoxStyle.Information, "Hinweis")
        Else
            MsgBox("Testen der Datenbank auf Konsistenz abgeschlossen. Es wurden keine Fehler gefunden.", MsgBoxStyle.Information, "Hinweis")
        End If
    End Sub

    ' Symbolleiste
    Private Sub HelpToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripButton.Click
        Dim frmInfo As New Info
        frmInfo.ShowDialog(Me)
    End Sub

    ' Hilfsfunktionen
    Public Sub UpdateWindowSettings(ByVal State As FormWindowState)
        ' für alle geladenen Kindfenster die Positionen auslesen und speichern
        If Not childWindowLoaded Then Exit Sub
        Dim tmp As WindowSettings
        programSettings.ChildWindowState = State

        ' Vokabel-Explorer
        If frmVocabularyExplorer IsNot Nothing Then
            tmp = programSettings.ExplorerWindowSettings
            If frmVocabularyExplorer.WindowState = FormWindowState.Normal Then
                tmp.position = frmVocabularyExplorer.Location
                tmp.width = frmVocabularyExplorer.Width
                tmp.height = frmVocabularyExplorer.Height
            End If
            programSettings.ExplorerWindowSettings = tmp
        End If

        ' Groups
        If frmGroupInput IsNot Nothing Then
            tmp = programSettings.GroupWindowSettings
            If frmGroupInput.WindowState = FormWindowState.Normal Then
                tmp.position = frmGroupInput.Location
                tmp.width = frmGroupInput.Width
                tmp.height = frmGroupInput.Height
            End If
            programSettings.GroupWindowSettings = tmp
        End If

        ' Statistik
        If frmStatistic IsNot Nothing Then
            tmp = programSettings.StatisticWindowSettings
            If frmStatistic.WindowState = FormWindowState.Normal Then
                tmp.position = frmStatistic.Location
                tmp.width = frmStatistic.Width
                tmp.height = frmStatistic.Height
            End If
            programSettings.StatisticWindowSettings = tmp
        End If

        programSettings.SaveSettings()
    End Sub

    Public Sub TestFinished()
        Dim frmSelect As New TestSelect
        If Trim(programSettings.LastGroup) <> "" Then frmSelect.LastGroup = programSettings.LastGroup
        If Trim(programSettings.LastSubGroup) <> "" Then frmSelect.LastSubGroup = programSettings.LastSubGroup
        frmSelect.TestSetPhrases = programSettings.TestSetPhrases
        frmSelect.TestFormerLanguage = programSettings.TestFormerLanguage
        Dim group As xlsGroup
        Dim frmTest As TestSimple = Nothing
        Do
            Dim res As DialogResult = frmSelect.ShowDialog(Me)
            If res = Windows.Forms.DialogResult.Cancel Then Exit Sub
            programSettings.LastGroup = frmSelect.LastGroup
            programSettings.LastSubGroup = frmSelect.LastSubGroup
            programSettings.SaveSettings()
            group = frmSelect.SelectedGroup
            If group Is Nothing Then Continue Do
            frmSelect.Hide()
            frmTest = New TestSimple(group.GroupTable, Me)
            frmTest.TestFormerLanguage = frmSelect.TestFormerLanguage
            frmTest.UseCards = programSettings.UseCards
            frmTest.TestSetPhrases = frmSelect.TestSetPhrases
            frmTest.TestMarked = frmSelect.TestMarked
            frmTest.RandomOrder = frmSelect.RandomOrder
            frmTest.Start()

            If frmTest.RestCount <> 0 Then Exit Do
            frmTest.Close()
        Loop
        frmTest.Show(Me)
    End Sub
End Class