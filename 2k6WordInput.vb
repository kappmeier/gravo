Public Class WordInput

  Dim voc As New xlsDictionary                          ' Zugriff auf die Wort-Datenbank allgemein
  Dim ldfManagement As xlsLDFManagement

  Private Sub WordInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    voc.DBConnection = New AccessDatabaseOperation(Application.StartupPath() & "\voc.mdb")
    ldfManagement = New xlsLDFManagement
    ldfManagement.LDFPath = Application.StartupPath()
    Me.cmbLanguages.SelectedIndex = 0
    Me.cmbXLSTypes.SelectedIndex = 0
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
    deWord.Pre = Me.txtPre.Text
    deWord.Word = Me.txtWord.Text
    deWord.Post = Me.txtPost.Text
    deWord.Meaning = Me.txtMeaning.Text
    deWord.AdditionalTargetLangInfo = Me.txtAdditionalTargetlanguageInfo.Text
    deWord.WordType = Me.lstWordTypes.SelectedIndex

    Try
      voc.AddSubEntry(deWord, Me.txtMainEntry.Text, Me.cmbLanguages.SelectedItem, Me.cmbXLSTypes.SelectedItem)
    Catch ex As xlsExceptionEntryNotFound
      Dim res As MsgBoxResult = MsgBox("Der Haupteintrag " & Me.txtMainEntry.Text & " ist nicht vorhanden. Soll er erstellt werden?", MsgBoxStyle.YesNo, "Haupteintrag nicht vorhanden")
      If res = MsgBoxResult.Yes Then
        ' Hinzufügen. Da die nicht-existiert-exception auftrag, kann nicht mehr die existiert-schon-exception auftreten
        Try
          voc.AddEntry(Trim(txtMainEntry.Text), cmbLanguages.SelectedItem, cmbXLSTypes.SelectedItem)
        Catch sex As xlsExceptionInput
          MsgBox(sex.Message, MsgBoxStyle.Information, "Unkorrekte Eingabe")
        End Try

        ' Erneut den subentry hinzufügen
        Try
          voc.AddSubEntry(deWord, Me.txtMainEntry.Text, Me.cmbLanguages.SelectedItem, Me.cmbXLSTypes.SelectedItem)
        Catch
          MsgBox("Eintrag nicht möglich. Wahrscheinlich schon in der Datenbank vorhanden.", MsgBoxStyle.Critical, "Fehler")
        End Try
      Else
        ' Eintrag soll nicht erstellt werden, ende.
      End If
    Catch ex As System.Data.OleDb.OleDbException
      'ErrorCode = -2147467259
      'Message = "Die von Ihnen vorgenommenen Änderungen an der Tabelle konnten nicht vorgenommen werden, da der Index, Primärschlüssel oder die Beziehung mehrfach vorkommende Werte enthalten würde. Ändern Sie die Daten in den Feldern, die gleiche Daten enthalten, entfernen Sie den Index, oder definieren Sie den Index neu, damit doppelte Einträge möglich sind, und versuchen Sie es erneut."
      'Source = "Microsoft JET Database Engine"
      MsgBox("Eintrag nicht möglich. Wahrscheinlich schon in der Datenbank vorhanden.", MsgBoxStyle.Critical, "Fehler")
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
End Class