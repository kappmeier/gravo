Imports System.Collections.ObjectModel

Public Class WordInput
  Dim db As New AccessDatabaseOperation                 ' Datenbankoperationen für Microsoft Access Datenbanken
  Dim grp As New xlsGroup("")                           ' Zugriff auf eine Gruppe
  Dim voc As New xlsDictionary                          ' Zugriff auf die Wort-Datenbank allgemein
  Dim groups As New xlsGroups

  Dim language As String
  Dim mainLanguage As String

  Public Sub New()
    ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
    InitializeComponent()

    ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
    db.Open(Application.StartupPath() & "\voc.mdb")     ' Datenbank öffnen
    voc.DBConnection = db
    grp.DBConnection = db
    groups.DBConnection = db
  End Sub

  Private Sub WordInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ' Position
    Me.Left = Me.Owner.Left + Me.Owner.Width / 2 - Me.Width / 2
    Me.Top = Me.Owner.Top + Me.Owner.Height / 2 - Me.Height / 2
    If Me.Top < 0 Then Me.Top = 0
    If Me.Left < 0 Then Me.Left = 0

    ' Sprachen in die Listen einfügen
    cmbLanguages.Items.Clear()
    Dim languages As Collection(Of String) = voc.DictionaryLanguages()
    For Each language As String In languages
      cmbLanguages.Items.Add(language)
    Next
    If languages.Count > 0 Then cmbLanguages.SelectedIndex = 0
    cmbMainLanguages.Items.Clear()
    languages = voc.DictionaryMainLanguages()
    For Each language As String In languages
      cmbMainLanguages.Items.Add(language)
    Next
    If languages.Count > 0 Then cmbMainLanguages.SelectedIndex = 0
    UpdateLanguageSelection()

    ' Laden der Gruppen in das Auswahlfeld
    cmbDirectAddGroup.Items.Clear()
    Dim groupNames As Collection(Of String) = groups.GetGroups()
    For Each groupName As String In groupNames
      cmbDirectAddGroup.Items.Add(groupName)
    Next
    If groupNames.Count > 0 Then cmbDirectAddGroup.SelectedIndex = 0
    If cmbDirectAddGroup.Items.Count > 0 Then cmbDirectAddGroup.SelectedIndex = 0 Else chkDirectAdd.Enabled = False
    chkDirectAdd.Checked = False
    cmbDirectAddGroup.Enabled = False
  End Sub

  Private Sub AddSubEntry(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddSubEntry.Click
    Dim deWord As New xlsDictionaryEntry(voc.DBConnection)
    deWord.LoadNewWord(voc.GetMaxSubEntryIndex + 1)
    deWord.Pre = txtPre.Text
    deWord.Word = txtWord.Text
    deWord.Post = txtPost.Text
    deWord.Meaning = txtMeaning.Text
    deWord.AdditionalTargetLangInfo = txtAdditionalTargetlanguageInfo.Text
    deWord.WordType = lstWordTypes.SelectedIndex

    Try
      voc.AddSubEntry(deWord, txtMainEntry.Text, language, mainLanguage)
      If chkDirectAdd.Checked Then AddToGroup()
    Catch ex As xlsExceptionEntryExists
      ' Eintrag existiert schon
      If chkDirectAdd.Checked Then AddToGroup()
    Catch ex As xlsExceptionEntryNotFound
      ' Da der Haupteintrag nicht vorhanden ist, muß hier auch nicht auf die xlsExists-Exception geachtet werden.
      Dim res As MsgBoxResult = MsgBox("Der Haupteintrag " & txtMainEntry.Text & " ist für die gewählten Sprachen nicht vorhanden. Soll er erstellt werden?", MsgBoxStyle.YesNo, "Haupteintrag nicht vorhanden")
      If res = MsgBoxResult.Yes Then
        ' Hinzufügen. Da die nicht-existiert-exception auftrat, kann nicht mehr die existiert-schon-exception auftreten
        Try
          voc.AddEntry(Trim(txtMainEntry.Text), language, mainLanguage)
        Catch sex As xlsExceptionInput
          MsgBox(sex.Message, MsgBoxStyle.Information, "Unkorrekte Eingabe")
        End Try
        ' Erneut den subentry hinzufügen
        Try
          voc.AddSubEntry(deWord, txtMainEntry.Text, language, mainLanguage)
          ' hinzufügen in die gruppe
          If chkDirectAdd.Checked Then AddToGroup()
        Catch sex As xlsExceptionEntryExists
          If chkDirectAdd.Checked Then AddToGroup() ' da es schon vorhanden ist, kann es in die aktuelle Gruppe hinzugefügt werden
        Catch sex As Exception
          MsgBox("Eintrag nicht möglich, konflikt mit Index wahrscheinlich. Überprüfen Sie Ihre Datenbankversion." & vbCrLf & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try
      Else
        ' Eintrag soll nicht erstellt werden, ende.
      End If
    Catch ex As Exception 'System.Data.OleDb.OleDbException
      'ErrorCode = -2147467259
      MsgBox("Eintrag nicht möglich, konflikt mit Index wahrscheinlich. Überprüfen Sie Ihre Datenbankversion." & vbCrLf & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
    End Try
    Me.txtMainEntry.SelectAll()
    Me.txtMainEntry.Focus()
    Me.txtAdditionalTargetlanguageInfo.SelectAll()
    Me.txtMeaning.SelectAll()
    Me.txtPost.SelectAll()
    Me.txtPre.SelectAll()
    Me.txtWord.SelectAll()
  End Sub

  Private Sub chkDirectAdd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDirectAdd.CheckedChanged
    cmbDirectAddGroup.Enabled = chkDirectAdd.Checked
  End Sub

  Private Sub AddToGroup()
    ' Davon ausgehen, daß das Einfügen in die Wortliste korrekt erfolgt ist
    Dim subIndex As Integer = voc.GetSubEntryIndex(voc.GetEntryIndex(txtMainEntry.Text, language, mainLanguage), txtWord.Text, txtMeaning.Text)
    grp.Add(subIndex)
  End Sub

  Private Sub cmbLanguages_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLanguages.SelectedIndexChanged
    ' Liste der Wortarten füllen
    lstWordTypes.Items.Clear()
    Dim i As Integer      ' Index
    lstWordTypes.Items.Add("Substantiv")
    lstWordTypes.Items.Add("Verb")
    lstWordTypes.Items.Add("Adjektiv")
    lstWordTypes.Items.Add("Einfach")
    lstWordTypes.Items.Add("Redewendung")

    ' Sprache bekannt machen
    UpdateLanguageSelection()
  End Sub

  Private Sub UpdateLanguageSelection()
    If chkNewLanguages.Checked Then
      txtLanguage.Enabled = True
      txtMainLanguage.Enabled = True
      If cmbLanguages.Items.Count > 0 Then txtLanguage.Text = cmbLanguages.SelectedItem
      If cmbMainLanguages.Items.Count > 0 Then txtMainLanguage.Text = cmbMainLanguages.SelectedItem
      cmbLanguages.Enabled = False
      cmbMainLanguages.Enabled = False
    Else
      txtLanguage.Enabled = False
      txtMainLanguage.Enabled = False
      cmbLanguages.Enabled = True
      cmbMainLanguages.Enabled = True
    End If

    ' Sprache bestimmen
    If chkNewLanguages.Checked Then
      If Trim(txtLanguage.Text) = "" Then Exit Sub
      If Trim(txtMainLanguage.Text) = "" Then Exit Sub
      language = txtLanguage.Text
      mainLanguage = txtMainLanguage.Text
    Else
      If cmbLanguages.Items.Count = 0 Then Exit Sub
      If cmbMainLanguages.Items.Count = 0 Then Exit Sub
      language = cmbLanguages.SelectedItem
      mainLanguage = cmbMainLanguages.SelectedItem
    End If

  End Sub

  Private Sub chkNewLanguages_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNewLanguages.CheckedChanged
    UpdateLanguageSelection()
  End Sub

  Private Sub cmbMainLanguages_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMainLanguages.SelectedIndexChanged
    UpdateLanguageSelection()
  End Sub

  Private Sub cmbDirectAddGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDirectAddGroup.SelectedIndexChanged
    ' Untergruppen in die andere Liste eintragen
    cmbDirectAddSubGroup.Items.Clear()     ' Liste leeren
    Dim subGroups As Collection(Of xlsGroupEntry) = groups.GetSubGroups(cmbDirectAddGroup.SelectedItem)
    For Each entry As xlsGroupEntry In subGroups
      cmbDirectAddSubGroup.Items.Add(entry.SubGroup)
    Next
    If cmbDirectAddSubGroup.Items.Count > 0 Then cmbDirectAddSubGroup.SelectedIndex = 0
  End Sub

  Private Sub cmbDirectAddSubGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDirectAddSubGroup.SelectedIndexChanged
    grp = groups.GetGroup(cmbDirectAddSubGroup.SelectedItem, cmbDirectAddSubGroup.SelectedItem)
  End Sub

  Private Sub txtLanguage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLanguage.TextChanged
    language = txtLanguage.Text
  End Sub

  Private Sub txtMainLanguage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMainLanguage.TextChanged
    mainLanguage = txtMainLanguage.Text
  End Sub
End Class