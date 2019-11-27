Imports System.Collections.ObjectModel

Public Class GroupInput
    ''' <summary>
    ''' Data access for groups.
    ''' </summary>
    Dim groupsDao As IGroupsDao
    ''' <summary>
    ''' Access to all words in the dictionary.
    ''' </summary>
    Dim dictionaryDao As IDictionaryDao
    ''' <summary>
    ''' 
    ''' </summary>
    Dim groupDao As IGroupDao
    ''' <summary>
    ''' The currently loaded group.
    ''' </summary>
    Dim groupEntry As GroupEntry
    ''' <summary>
    ''' Data of the currently loaded group.
    ''' </summary>
    Dim groupData As GroupDto

    ''' <summary>
    ''' A collection of all words in the selection field
    ''' </summary>
    Dim meanings As Collection(Of WordEntry)

    Dim lastControl As Control

    Public Sub New()
        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        Dim db As IDataBaseOperation = New SQLiteDataBaseOperation()
        db.Open(DBPath)
        GroupsDao = New GroupsDao(db)
        DictionaryDao = New DictionaryDao(db)
        GroupDao = New GroupDao(db)
    End Sub

    Private Sub GroupInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Allgemeine Einstellungen
        Me.DoubleBuffered = True

        ' Gruppen in die Liste einfügen
        cmbSelectGroup.Items.Clear()
        Dim groupNames As Collection(Of String) = GroupsDao.GetGroups()
        For Each groupName As String In groupNames
            cmbSelectGroup.Items.Add(groupName)
        Next

        ' Sprachen in die Liste einfügen
        cmbSelectLanguage.Items.Clear()
        Dim languages As Collection(Of String) = DictionaryDao.DictionaryLanguages("german")
        For Each language As String In languages
            cmbSelectLanguage.Items.Add(language)
        Next
        If groupNames.Count > 0 Then cmbSelectGroup.SelectedIndex = 0
        If languages.Count > 0 Then cmbSelectLanguage.SelectedIndex = 0 Else UpdateDisplayedInfo()
        SelectUniqueLanguage()

        ' Die Suche initialisieren, Label Anzeige einrichten
        SearchWord("")

        ' zweites Auswahlfeld vorbereiten
        lstWords.Columns.Clear()

        lstWords.Columns.Clear()
        lstWords.Columns.Add("Pre")
        lstWords.Columns.Add("Word")
        lstWords.Columns.Add("Post")
        lstWords.Columns.Add("Bedeutung")
        lstMeanings.Columns.Clear()
        lstMeanings.Columns.Add("Pre")
        lstMeanings.Columns.Add("Word")
        lstMeanings.Columns.Add("Post")
        lstMeanings.Columns.Add("Bedeutung")
        lstMeanings.Columns.Item(1).Width *= 2
        lstMeanings.Columns.Item(3).Width *= 2
        lstWords.Columns.Item(1).Width *= 2
        lstWords.Columns.Item(3).Width *= 2
    End Sub

    ' Anpassen der Größe
    Private Sub GroupInput_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        ' ComboBox und Beschreibungen
        Dim windowWidth As Integer = Me.DisplayRectangle.Width
        Dim windowHeight As Integer = Me.DisplayRectangle.Height
        Dim comboWidth As Integer = (windowWidth - 9 - 9 - 12 - 12) / 3
        cmbSelectGroup.Width = comboWidth
        cmbSelectLanguage.Width = comboWidth
        cmbSelectSubGroup.Width = comboWidth
        cmbSelectGroup.Left = 12
        cmbSelectSubGroup.Left = cmbSelectGroup.Left + comboWidth + 9
        cmbSelectLanguage.Left = cmbSelectSubGroup.Left + comboWidth + 9
        lblWordsInGroup.Left = cmbSelectGroup.Left
        lblWordsInLanguage.Left = cmbSelectLanguage.Left
        lblWordsInSubGroup.Left = cmbSelectSubGroup.Left

        ' Vokabellisten
        lstMeanings.Width = 2 * comboWidth + 9
        lstWords.Width = 2 * comboWidth + 9
        txtSearchText.Width = 2 * comboWidth + 9
        lstMeanings.Left = cmbSelectSubGroup.Left
        lstWords.Left = cmbSelectSubGroup.Left
        txtSearchText.Left = cmbSelectSubGroup.Left
        lblMeaningsDescription.Left = cmbSelectSubGroup.Left
        lblWordsDescription.Left = cmbSelectSubGroup.Left
        lblSearchDescription.Left = cmbSelectSubGroup.Left
        lblSimilarWord.Left = cmbSelectSubGroup.Left
        Dim lstHeight As Integer = ((windowHeight - lstMeanings.Top - 12) - 9 - 9 - lblMeaningsDescription.Height - lblWordsDescription.Height - lblSearchDescription.Height - lblSimilarWord.Height - 3 - 3 - 3 - 3 - txtSearchText.Height) / 2
        lstMeanings.Height = lstHeight
        lstWords.Height = lstHeight
        lblMeaningsDescription.Top = lblWordsInSubGroup.Top + lblWordsInSubGroup.Height + 9 + 3
        lstMeanings.Top = lblMeaningsDescription.Top + lblMeaningsDescription.Height + 3
        lblWordsDescription.Top = lstMeanings.Top + lstMeanings.Height + 9
        lstWords.Top = lblWordsDescription.Top + lblWordsDescription.Height + 3
        lblSearchDescription.Top = lstWords.Top + lstWords.Height + 9
        txtSearchText.Top = lblSearchDescription.Top + lblSearchDescription.Height + 3
        lblSimilarWord.Top = txtSearchText.Top + txtSearchText.Height + 3

        ' Gruppenliste und Buttons und CheckButten für 'Marked'
        lstWordsInGroup.Width = cmbSelectGroup.Width - 9 - cmdSelect.Width
        cmdSelect.Left = lstWordsInGroup.Left + lstWordsInGroup.Width + 9
        cmdDeselect.Left = cmdSelect.Left 'lstWordsInGroup.Left + lstWordsInGroup.Width + 9
        lstWordsInGroup.Top = lstMeanings.Top
        lstWordsInGroup.Height = windowHeight - lstWordsInGroup.Top - 12
        cmdSelect.Top = lstWords.Top + lstWords.Height / 2 - cmdSelect.Height / 2
        cmdDeselect.Top = lstMeanings.Top + lstMeanings.Height / 2 - cmdDeselect.Height / 2
        lblCurrentWordIndex.Top = lblMeaningsDescription.Top
        chkMarked.Left = cmdSelect.Left
        chkMarked.Top = cmdSelect.Top + cmdSelect.Height + 6

        ' Exit button
        cmdExit.Top = txtSearchText.Top + txtSearchText.Height + 9
        cmdExit.Left = txtSearchText.Left + txtSearchText.Width - cmdExit.Width
    End Sub

    ' Lokalisierung
    Public Overrides Sub LocalizationChanged()

    End Sub

    Private Sub cmbSelectGroup_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSelectGroup.GotFocus
        lastControl = cmbSelectGroup
    End Sub

    Private Sub cmbSelectGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSelectGroup.SelectedIndexChanged
        ' Untergruppen in die andere Liste eintragen
        cmbSelectSubGroup.Items.Clear()     ' Liste leeren
        Dim subGroups As Collection(Of GroupEntry) = GroupsDao.GetSubGroups(cmbSelectGroup.SelectedItem)
        For Each entry As GroupEntry In subGroups
            cmbSelectSubGroup.Items.Add(entry.SubGroup)
        Next
        If cmbSelectSubGroup.Items.Count > 0 Then cmbSelectSubGroup.SelectedIndex = 0

        UpdateDisplayedInfo()
    End Sub

    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        If lstWords.Items.Count = 0 Then Exit Sub ' No items in list
        If cmbSelectGroup.Items.Count = 0 Then Exit Sub ' no group exists

        ' TODO example
        Dim word As WordEntry
        Try
            word = meanings.Item(lstWords.SelectedIndices.Item(0))
        Catch ex As Exception
            MsgBox("Bitte wählen sie das Wort erneut aus!", vbInformation, "Fehler aufgetreten!")
            Exit Sub
        End Try

        Try
            groupDao.Add(groupEntry, word, chkMarked.Checked, "")
        Catch ex As EntryExistsException
            MsgBox("Wort bereits in der Gruppe enthalten", vbInformation, "Hinzufügen nicht möglich")
            Exit Sub
        End Try

        ' update display
        UpdateWordsInGroup()
        lstWordsInGroup.SelectedIndex = lstWordsInGroup.Items.Count - 1

        If lastControl.Name = txtSearchText.Name Then
            txtSearchText.SelectAll()
            txtSearchText.Focus()
        End If
        If lastControl.Name = lstWords.Name Then
            txtSearchText.SelectAll()
            lstWords.Focus()
        End If
    End Sub

    Private Sub lstWordsInGroup_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstWordsInGroup.GotFocus
        lastControl = lstWordsInGroup
    End Sub

    Private Sub lstWordsInGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstWordsInGroup.SelectedIndexChanged
        UpdateWordsInGroupSelected()

        ' Markiere das erste Wort in der Bedeutungsliste
        lstMeanings.SelectedIndices.Clear()
        lstMeanings.SelectedIndices.Add(0)
    End Sub

    Private Sub UpdateWordsInGroupSelected()
        Dim entries = groupData.FilterWords(lstWordsInGroup.SelectedItem)

        lstMeanings.Items.Clear()
        For Each entry As TestWord In entries
            Dim lvItem As ListViewItem = lstMeanings.Items.Add(entry.Pre)
            lvItem.SubItems.AddRange(New String() {entry.Word, entry.Post, entry.Meaning})
        Next entry

        If lstMeanings.Items.Count >= 1 Then lstMeanings.SelectedIndices.Add(0)
    End Sub

    Private Sub txtSearchText_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchText.GotFocus
        lastControl = txtSearchText
    End Sub

    Private Sub txtSearchText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchText.TextChanged
        Dim count As Integer = 0
        Dim word As String
        Do
            word = txtSearchText.Text.Substring(0, txtSearchText.Text.Length - count)
            count += 1
        Loop While (Not SearchWord(word)) And word.Length > 0
    End Sub

    Private Sub cmbSelectSubGroup_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSelectSubGroup.GotFocus
        lastControl = cmbSelectSubGroup
    End Sub

    Private Sub cmbSelectSubGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSelectSubGroup.SelectedIndexChanged
        ' Zeige die Wörter der aktuell gewählten Gruppe in der Liste an
        UpdateWordsInGroup()

        ' Sprache der Gruppe auswählen
        SelectUniqueLanguage()
    End Sub

    Private Sub cmbSelectLanguage_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSelectLanguage.GotFocus
        lastControl = cmbSelectLanguage
    End Sub

    Private Sub cmbSelectLanguage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSelectLanguage.SelectedIndexChanged
        ' Wort suchen
        SearchWord(txtSearchText.Text)
        UpdateDisplayedInfo()
    End Sub

    Private Sub lstMeanings_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMeanings.GotFocus
        lastControl = lstMeanings
    End Sub

    Private Sub lstWords_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstWords.GotFocus
        lastControl = lstWords
    End Sub

    Private Sub cmdDeselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeselect.Click
        'Throw New NotImplementedException

        If lstMeanings.Items.Count = 0 Then Exit Sub
        Dim selectedIndex As Integer = lstWordsInGroup.SelectedIndex
        Dim testitem As ListViewItem
        testitem = lstMeanings.Items(lstMeanings.SelectedIndices.Item(0))
        Dim word As String = testitem.SubItems(1).Text
        Dim meaning As String = testitem.SubItems(3).Text
        Dim testWord As TestWord = GroupDao.GetTestWord(groupEntry, word, meaning)

        GroupDao.Delete(groupEntry, testWord)
        UpdateWordsInGroup()
        If selectedIndex > lstWordsInGroup.Items.Count - 1 Then
            If selectedIndex > 0 Then lstWordsInGroup.SelectedIndex = selectedIndex - 1 Else lstMeanings.Items.Clear()
        Else
            lstWordsInGroup.SelectedIndex = selectedIndex
        End If
    End Sub

    Private Function SearchWord(ByVal word As String) As Boolean
        Dim similar As String = DictionaryDao.FindSimilar(txtSearchText.Text, cmbSelectLanguage.SelectedItem, "german")
        lblSimilarWord.Text = similar
        If similar = "" Then Return False
        meanings = DictionaryDao.GetWordsAndSubWords(similar, cmbSelectLanguage.SelectedItem, "german")

        ' Anzeigen aller Einträge aus der Collection
        lstWords.Items.Clear()
        For Each entry As WordEntry In meanings
            Dim lvItem As ListViewItem = lstWords.Items.Add(entry.Pre)
            lvItem.SubItems.AddRange(New String() {entry.Word, entry.Post, entry.Meaning})
        Next

        ' Markieren
        lstWords.SelectedIndices.Clear()
        If lstWords.Items.Count >= 1 Then lstWords.SelectedIndices.Add(0)

        Return True
    End Function

    Private Sub SelectUniqueLanguage()
        ' Falls nur eine Sprache gewählt worden ist, diese markieren
        Dim language As String
        Try
            If groupData IsNot Nothing Then
                language = GroupDao.GetUniqueLanguage(groupEntry)
            Else
                If cmbSelectLanguage.Items.Count > 0 Then language = cmbSelectLanguage.Items.Item(0) Else Exit Sub
            End If
        Catch ex As xlsException
            ' Sprache nicht da, oder ein schwererer Fehler
            Exit Sub
        End Try
        cmbSelectLanguage.SelectedItem = language
    End Sub

    Private Sub UpdateDisplayedInfo()
        If cmbSelectLanguage.SelectedItem Is Nothing Then
            Return
        End If
        ' Anzeige aktualisieren
        Dim t As String = DictionaryDao.WordCount(cmbSelectLanguage.SelectedItem, "german") & IIf(DictionaryDao.WordCount(cmbSelectLanguage.SelectedItem, "german") = 1, " Eintrag", " Einträge")
        lblWordsInLanguage.Text = t & " in der Sprache."
        ' Anzeigen, wie viele Wörter in der Gruppe sind
        Dim t1 As String = lstWordsInGroup.Items.Count & IIf(lstWordsInGroup.Items.Count = 1, " verschiedener Eintrag", " verschiedene Einträge")

        If groupData Is Nothing Then
            t = "0 Einträge insgesamt"
        Else
            t = groupData.WordCount & IIf(groupData.WordCount = 1, " Eintrag insgesamt", " Einträge insgesamt")
        End If
        lblWordsInSubGroup.Text = t1 & " in der Gruppe," & vbCrLf & t & "."
        ' Anzeige der Vokabeln aktualisieren
        t = DataTools.WordCount(GroupsDao, GroupDao, cmbSelectGroup.SelectedItem) & IIf(DataTools.WordCount(GroupsDao, GroupDao, cmbSelectGroup.SelectedItem) = 1, " Eintrag", " Einträge")
        lblWordsInGroup.Text = t & " in der Gruppe insgesamt."
    End Sub

    Private Sub UpdateWordsInGroup()
        Dim selected As Integer = lstWordsInGroup.SelectedIndex

        ' Neue Gruppe ausgewählt. Zeige die enthaltenen Vokabeln in der liste an
        lstWordsInGroup.BeginUpdate()
        lstWordsInGroup.Items.Clear()
        groupEntry = GroupsDao.GetGroup(cmbSelectGroup.SelectedItem, cmbSelectSubGroup.SelectedItem)
        groupData = GroupDao.Load(GroupEntry)


        Dim wordstrings As IEnumerable(Of String) = groupData.GetWords
        For Each word As String In groupData.GetWords
            lstWordsInGroup.Items.Add(word)
        Next
        lstWordsInGroup.EndUpdate()
        If Not selected >= lstWordsInGroup.Items.Count Then lstWordsInGroup.SelectedIndex = selected

        UpdateDisplayedInfo()
    End Sub
End Class