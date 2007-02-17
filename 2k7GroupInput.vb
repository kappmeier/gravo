Public Class GroupInput
  Dim db As New AccessDatabaseOperation                 ' Datenbankoperationen f�r Microsoft Access Datenbanken
  Dim voc As New xlsGroups                              ' Zugriff auf die Gruppen der Datenbank
  Dim dic As New xlsDictionary                          ' Zugriff auf die Wort-Datenbank allgemein
  Dim grp As New xlsGroup("")                           ' Zugriff auf eine Gruppe
  Dim cGroups As Collection
  Dim cWords As Collection ' Die zur auswahl stehenden W�rter
  Dim cMeanings As Collection       ' Eine Sammlung der W�rter in der Bedeutungsauswahl

  Private Sub GroupInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    db.Open(Application.StartupPath() & "\voc.mdb")     ' Datenbank �ffnen
    voc.DBConnection = db
    dic.DBConnection = db
    grp.DBConnection = db

    ' Laden der Gruppen in das Auswahlfeld
    cGroups = voc.GetGroups()
    For Each group As xlsGroupEntry In cGroups
      Me.cmbSelectGroup.Items.Add(group.Group & " - " & group.SubGroup)
    Next
    cmbSelectGroup.SelectedIndex = 0

    ' zweites Auswahlfeld vorbereiten
    lstWords.columns.clear()

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
  End Sub

  Private Sub cmbSelectGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSelectGroup.SelectedIndexChanged
    UpdateWordsInGroup()

    ' update f�r die liste aller w�rter. nur haupteintr�ge werden eingef�gt
    lstAllWords.BeginUpdate()
    lstAllWords.Items.Clear()
        'cWords = dic.GetWords("italian", "std")
    cWords = dic.DictionaryEntrys("italian")
    For Each word As String In cWords
      Me.lstAllWords.Items.Add(word)
    Next

    lstAllWords.EndUpdate()
  End Sub

  Private Sub lstAllWords_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstAllWords.SelectedIndexChanged
    Dim sMainEntry As String = dic.GetEntry(dic.GetEntryIndex("italian", cWords.Item(lstAllWords.SelectedIndex + 1)))
    cMeanings = dic.GetWordsAndSubWords("italian", sMainEntry)

    ' Anzeigen aller Eintr�ge aus der Collection
    lstWords.Items.Clear()
    For Each wCurrent As xlsDictionaryEntry In cMeanings
      Dim lvItem As ListViewItem = lstWords.Items.Add(wCurrent.Pre)
      lvItem.SubItems.AddRange(New String() {wCurrent.Word, wCurrent.Post, wCurrent.Meaning})
    Next

    If lstWords.Items.Count >= 1 Then lstWords.SelectedIndices.Add(0)
  End Sub

  Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
    If lstWords.Items.Count = 0 Then Exit Sub ' Es gibt keine Items in der Liste
    ' Ausgeben der Nummer zu der ausgew�hlten Vokabel
    Dim iIndex As Integer = lstWords.SelectedIndices.Item(0)
    Dim wtWord As xlsDictionaryEntry = cMeanings.Item(iIndex + 1)
    ' Wort ist bekannt, in die Gruppe hinzuf�gen

    grp.Add(wtWord.WordIndex)

    UpdateWordsInGroup()
  End Sub

  Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
    Dim index As Integer = lstAllWords.Items.IndexOf(txtSearchText.Text)
    If index <> -1 Then ' Wort vorhanden
      Me.lstAllWords.SelectedIndex = index
    End If
  End Sub

  Private Sub lstWordsInGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstWordsInGroup.SelectedIndexChanged
    UpdateWordsInGroupSelected()
  End Sub

  Private Sub UpdateWordsInGroupSelected()
    Dim cWords As Collection = grp.GetWords(lstWordsInGroup.SelectedItem)

    lstMeanings.Items.Clear()
    For Each wCurrent As xlsDictionaryEntry In cWords
      Dim lvItem As ListViewItem = lstMeanings.Items.Add(wCurrent.Pre)
      lvItem.SubItems.AddRange(New String() {wCurrent.Word, wCurrent.Post, wCurrent.Meaning})
    Next

    If lstMeanings.Items.Count >= 1 Then lstMeanings.SelectedIndices.Add(0)
  End Sub

  Private Sub UpdateWordsInGroup()
    Dim selected As Integer = lstWordsInGroup.SelectedIndex

    ' Neue Gruppe ausgew�hlt. Zeige die enthaltenen Vokabeln in der liste an
    lstWordsInGroup.BeginUpdate()
    lstWordsInGroup.Items.Clear()
    Dim group As xlsGroupEntry = cGroups(cmbSelectGroup.SelectedIndex + 1)
    grp.GroupTable = group.Table
    Dim cWordStrings As Collection = grp.GetWords()
    For Each sWord As String In cWordStrings
      lstWordsInGroup.Items.Add(sWord)
    Next
    lstWordsInGroup.EndUpdate()
    If Not selected >= lstWordsInGroup.Items.Count Then lstWordsInGroup.SelectedIndex = selected
  End Sub
End Class

