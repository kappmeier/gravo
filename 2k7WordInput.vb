Public Class WordInput

  Dim db As New AccessDatabaseOperation                 ' Datenbankoperationen für Microsoft Access Datenbanken
  Dim grp As New xlsGroup("")                           ' Zugriff auf eine Gruppe
  Dim voc As New xlsDictionary                          ' Zugriff auf die Wort-Datenbank allgemein
  Dim ldfManagement As xlsLDFManagement
  Dim cGroups As Collection

  Private Sub WordInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    db.Open(Application.StartupPath() & "\voc.mdb")     ' Datenbank öffnen
    voc.DBConnection = db
    grp.DBConnection = db

    ldfManagement = New xlsLDFManagement
    ldfManagement.LDFPath = Application.StartupPath()
    Me.cmbLanguages.SelectedIndex = 0
    Me.cmbXLSTypes.SelectedIndex = 0

    ' laden der Gruppen in das Auswahlfeld
    Dim groups As New xlsGroups
    groups.DBConnection = voc.DBConnection
    cGroups = groups.GetGroups()
    For Each group As xlsGroupEntry In cGroups
      cmbDirectAddGroup.Items.Add(group.Group & " - " & group.SubGroup)
    Next
    cmbDirectAddGroup.SelectedIndex = 0
    chkDirectAdd.Checked = False
    cmbDirectAddGroup.Enabled = False
  End Sub

  Private Sub AddEntry(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddEntry.Click
    Try
      voc.AddEntry(Trim(txtAddEntry.Text), cmbLanguages.SelectedItem, cmbXLSTypes.SelectedItem)
    Catch ex As xlsExceptionEntryExists
      MsgBox("Das Wort existiert schon in der Liste und kann nicht hinzugefügt werden. Versuchen sie es mit einem anderen XLS-Typ.", MsgBoxStyle.Information, "Doppelter Eintrag")
    Catch ex As Exception
      ' Irgendwas anderes als doppelter eintrag 
      Throw ex
    End Try
  End Sub

  Private Sub cmbXLSTypes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbXLSTypes.SelectedIndexChanged
    ' LDF für diese Sprache wählen
    ldfManagement.SelectLD(cmbLanguages.SelectedItem, cmbXLSTypes.SelectedItem)
    Dim ldf As xlsLDF = New xlsLDF(ldfManagement.LDFFullPath)

    ' Liste der Wortarten füllen
    lstWordTypes.Items.Clear()
    Dim i As Integer      ' Index
    For i = 1 To ldfManagement.FormList.Count
      Me.lstWordTypes.Items.Add(ldfManagement.FormList.Item(i).Right)
    Next i
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
      voc.AddSubEntry(deWord, txtMainEntry.Text, cmbLanguages.SelectedItem, cmbXLSTypes.SelectedItem)
      If chkDirectAdd.Checked Then AddToGroup()
    Catch ex As xlsExceptionEntryNotFound
      Dim res As MsgBoxResult = MsgBox("Der Haupteintrag " & txtMainEntry.Text & " ist nicht vorhanden. Soll er erstellt werden?", MsgBoxStyle.YesNo, "Haupteintrag nicht vorhanden")
      If res = MsgBoxResult.Yes Then
        ' Hinzufügen. Da die nicht-existiert-exception auftrat, kann nicht mehr die existiert-schon-exception auftreten
        Try
          voc.AddEntry(Trim(txtMainEntry.Text), cmbLanguages.SelectedItem, cmbXLSTypes.SelectedItem)
        Catch sex As xlsExceptionInput
          MsgBox(sex.Message, MsgBoxStyle.Information, "Unkorrekte Eingabe")
        End Try

        ' Erneut den subentry hinzufügen
        Try
          voc.AddSubEntry(deWord, txtMainEntry.Text, cmbLanguages.SelectedItem, cmbXLSTypes.SelectedItem)
          ' hinzufügen in die gruppe
          If chkDirectAdd.Checked Then AddToGroup()
        Catch
          MsgBox("Eintrag nicht möglich. Wahrscheinlich schon in der Datenbank vorhanden.", MsgBoxStyle.Critical, "Fehler")
          If chkDirectAdd.Checked Then AddToGroup() ' da es schon vorhanden ist, kann es in die aktuelle Gruppe hinzugefügt werden
        End Try
      Else
        ' Eintrag soll nicht erstellt werden, ende.
      End If
    Catch ex As System.Data.OleDb.OleDbException
      'ErrorCode = -2147467259
      'Message = "Die von Ihnen vorgenommenen Änderungen an der Tabelle konnten nicht vorgenommen werden, da der Index, Primärschlüssel oder die Beziehung mehrfach vorkommende Werte enthalten würde. Ändern Sie die Daten in den Feldern, die gleiche Daten enthalten, entfernen Sie den Index, oder definieren Sie den Index neu, damit doppelte Einträge möglich sind, und versuchen Sie es erneut."
      'Source = "Microsoft JET Database Engine"
      MsgBox("Eintrag nicht möglich. Wahrscheinlich schon in der Datenbank vorhanden.", MsgBoxStyle.Critical, "Fehler")
      If chkDirectAdd.Checked Then AddToGroup() ' da es schon vorhanden ist, kann es in die aktuelle Gruppe hinzugefügt werden
    Catch ex As Exception
      Throw ex
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
    ' davon ausgehen, daß das Einfügen in die Gruppe korrekt erfolgt ist

    MsgBox("Wort wird nicht hinzugefügt, da die GetSubEntryFunktion geändert worden ist!", MsgBoxStyle.Critical, "Warning!")
    Exit Sub
    'Dim main As Integer = 0
    'Dim index As Integer = voc.GetSubEntryIndex(main, txtWord.Text, txtMeaning.Text)
    'grp.Add(index)
  End Sub

  Private Sub cmbDirectAddGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDirectAddGroup.SelectedIndexChanged
    Dim group As xlsGroupEntry = cGroups(cmbDirectAddGroup.SelectedIndex + 1)
    grp.GroupTable = group.Table
  End Sub
End Class