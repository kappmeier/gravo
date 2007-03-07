Imports System.Collections.ObjectModel

Public Class GroupInput
  Dim db As New AccessDatabaseOperation                 ' Datenbankoperationen für Microsoft Access Datenbanken
  Dim voc As New xlsGroups                              ' Zugriff auf die Gruppen der Datenbank
  Dim dic As New xlsDictionary                          ' Zugriff auf die Wort-Datenbank allgemein
  Dim grp As xlsGroup                                   ' Zugriff auf eine Gruppe
  Dim groups As New xlsGroups

  Dim meanings As Collection(Of xlsDictionaryEntry)     ' Eine Sammlung der Wörter in der Bedeutungsauswahl

  Dim lastControl As Control

  Public Sub New()
    ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
    InitializeComponent()

    ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
    db.Open(Application.StartupPath() & "\voc.mdb")     ' Datenbank öffnen
    voc.DBConnection = db
    dic.DBConnection = db
    groups.DBConnection = db
  End Sub

  Private Sub GroupInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ' Allgemeine Einstellungen
    Me.DoubleBuffered = True

    ' Gruppen in die Liste einfügen
    cmbSelectGroup.Items.Clear()
    Dim groupNames As Collection(Of String) = groups.GetGroups()
    For Each groupName As String In groupNames
      cmbSelectGroup.Items.Add(groupName)
    Next
    If groupNames.Count > 0 Then cmbSelectGroup.SelectedIndex = 0

    ' Sprachen in die Liste einfügen
    cmbSelectLanguage.Items.Clear()
    Dim languages As Collection(Of String) = dic.DictionaryLanguages()
    For Each language As String In languages
      cmbSelectLanguage.Items.Add(language)
    Next
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

    ' Gruppenliste und Buttons
    lstWordsInGroup.Width = cmbSelectGroup.Width - 9 - cmdSelect.Width
    cmdSelect.Left = lstWordsInGroup.Left + lstWordsInGroup.Width + 9
    cmdDeselect.Left = lstWordsInGroup.Left + lstWordsInGroup.Width + 9
    lstWordsInGroup.Top = lstMeanings.Top
    lstWordsInGroup.Height = windowHeight - lstWordsInGroup.Top - 12
    cmdSelect.Top = lstWords.Top + lstWords.Height / 2 - cmdSelect.Height / 2
    cmdDeselect.Top = lstMeanings.Top + lstMeanings.Height / 2 - cmdDeselect.Height / 2
    lblCurrentWordIndex.Top = lblMeaningsDescription.Top

    ' Exit button
    cmdExit.Top = txtSearchText.Top + txtSearchText.Height + 9
    cmdExit.Left = txtSearchText.Left + txtSearchText.Width - cmdExit.Width
  End Sub

  Private Sub cmbSelectGroup_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSelectGroup.GotFocus
    lastControl = cmbSelectGroup
  End Sub

  Private Sub cmbSelectGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSelectGroup.SelectedIndexChanged
    ' Untergruppen in die andere Liste eintragen
    cmbSelectSubGroup.Items.Clear()     ' Liste leeren
    Dim subGroups As Collection(Of xlsGroupEntry) = groups.GetSubGroups(cmbSelectGroup.SelectedItem)
    For Each entry As xlsGroupEntry In subGroups
      cmbSelectSubGroup.Items.Add(entry.SubGroup)
    Next
    If cmbSelectSubGroup.Items.Count > 0 Then cmbSelectSubGroup.SelectedIndex = 0

    UpdateDisplayedInfo()
  End Sub

  Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
    If lstWords.Items.Count = 0 Then Exit Sub ' Es gibt keine Items in der Liste
    If cmbSelectGroup.Items.Count = 0 Then Exit Sub ' Es gibt keine Gruppe in die etwas eingefügt werden könnte

    ' Wort in Gruppe einfügen
    Dim wtWord As xlsDictionaryEntry
    Try
      wtWord = meanings.Item(lstWords.SelectedIndices.Item(0))
    Catch ex As Exception
      MsgBox("Bitte wählen sie das Wort erneut aus!", vbInformation, "Fehler aufgetreten!")
      Exit Sub
    End Try
    grp.Add(wtWord.WordIndex)

    ' Anzeige aktualisieren
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
    Dim words As Collection(Of xlsDictionaryEntry) = grp.GetWords(lstWordsInGroup.SelectedItem)

    lstMeanings.Items.Clear()
    For Each wCurrent As xlsDictionaryEntry In words
      Dim lvItem As ListViewItem = lstMeanings.Items.Add(wCurrent.Pre)
      lvItem.SubItems.AddRange(New String() {wCurrent.Word, wCurrent.Post, wCurrent.Meaning})
    Next

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
    If lstMeanings.Items.Count = 0 Then Exit Sub
    Dim selectedIndex As Integer = lstWordsInGroup.SelectedIndex
    Dim testitem As ListViewItem
    testitem = lstMeanings.Items(lstMeanings.SelectedIndices.Item(0))
    Dim word As String = testitem.SubItems(1).Text
    Dim meaning As String = testitem.SubItems(3).Text
    Dim index As Integer = grp.GetIndex(word, meaning)
    grp.Delete(index)
    UpdateWordsInGroup()
    If selectedIndex > lstWordsInGroup.Items.Count - 1 Then
      If selectedIndex > 0 Then lstWordsInGroup.SelectedIndex = selectedIndex - 1 Else lstMeanings.Items.Clear()
    Else
      lstWordsInGroup.SelectedIndex = selectedIndex
    End If
  End Sub

  Private Function SearchWord(ByVal word As String) As Boolean
    Dim similar As String = dic.FindSimilar(txtSearchText.Text, cmbSelectLanguage.SelectedItem, "german")
    lblSimilarWord.Text = similar
    If similar = "" Then Return False
    meanings = dic.GetWordsAndSubWords(similar, cmbSelectLanguage.SelectedItem, "german")

    ' Anzeigen aller Einträge aus der Collection
    lstWords.Items.Clear()
    For Each entry As xlsDictionaryEntry In meanings
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
      If grp IsNot Nothing Then
        language = grp.GetUniqueLanguage()
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
    ' Anzeige aktualisieren
    Dim t As String = dic.WordCount(cmbSelectLanguage.SelectedItem, "german") & IIf(dic.WordCount(cmbSelectLanguage.SelectedItem, "german") = 1, " Eintrag", " Einträge")
    lblWordsInLanguage.Text = t & " in der Sprache."
    ' Anzeigen, wie viele Wörter in der Gruppe sind
    Dim t1 As String = lstWordsInGroup.Items.Count & IIf(lstWordsInGroup.Items.Count = 1, " verschiedener Eintrag", " verschiedene Einträge")
    If grp Is Nothing Then
      t = "0 Einträge insgesamt"
    Else
      t = grp.WordCount & IIf(grp.WordCount = 1, " Eintrag insgesamt", " Einträge insgesamt")
    End If
    lblWordsInSubGroup.Text = t1 & " in der Gruppe," & vbCrLf & t & "."
    ' Anzeige der Vokabeln aktualisieren
    t = groups.WordCount(cmbSelectGroup.SelectedItem) & IIf(groups.WordCount(cmbSelectGroup.SelectedItem) = 1, " Eintrag", " Einträge")
    lblWordsInGroup.Text = t & " in der Gruppe insgesamt."
  End Sub

  Private Sub UpdateWordsInGroup()
    Dim selected As Integer = lstWordsInGroup.SelectedIndex

    ' Neue Gruppe ausgewählt. Zeige die enthaltenen Vokabeln in der liste an
    lstWordsInGroup.BeginUpdate()
    lstWordsInGroup.Items.Clear()
    grp = groups.GetGroup(cmbSelectGroup.SelectedItem, cmbSelectSubGroup.SelectedItem)

    Dim wordstrings As Collection(Of String) = grp.GetWords()
    For Each sWord As String In wordstrings
      lstWordsInGroup.Items.Add(sWord)
    Next
    lstWordsInGroup.EndUpdate()
    If Not selected >= lstWordsInGroup.Items.Count Then lstWordsInGroup.SelectedIndex = selected

    UpdateDisplayedInfo()
  End Sub
End Class