Imports System.Collections.ObjectModel

Public Class Management
  Inherits System.Windows.Forms.Form

  ' Datenbank-Zugriff
  Dim voc As xlsBase            ' Zugriff auf Vokabel-Datenbank
  Dim grp As xlsGroups
  Dim man As xlsManagement
  Dim dic As xlsDictionary
  Dim importFilename As String = ""

  Public Sub New()
    ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
    InitializeComponent()

    ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
    Dim db As New AccessDatabaseOperation               ' Datenbankoperationen
    db.Open(Application.StartupPath() & "\voc.mdb")     ' Datenbank öffnen
    voc = New xlsBase(db)                               ' Datenbank zur Verfügung stellen
    grp = New xlsGroups()
    grp.DBConnection = db
    man = New xlsManagement
    man.DBConnection = db
    dic = New xlsDictionary
    dic.DBConnection = db
  End Sub

  Private Sub LoadForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ' Position
    Me.Left = Me.Owner.Left + Me.Owner.Width / 2 - Me.Width / 2
    Me.Top = Me.Owner.Top + Me.Owner.Height / 2 - Me.Height / 2
    If Me.Top < 0 Then Me.Top = 0
    If Me.Left < 0 Then Me.Left = 0

    ' Form-Update durchführen (Lädt Gruppen und Bezeichnungen)
    UpdateForm()

    ' Datenbank-Version, reorganisieren, importieren, exportieren
    UpdateDatabaseVersion()
    lblErrorCount.Text = "Gefundene und behobene Fehler: keine Überprüfung durchgeführt"
    lblImportDB.Text = "Datenbank: noch keine gewählt"
    cmdImportDictionary.Enabled = False
    cmdImportGroup.Enabled = False
    lblImportDictCount.Text = "Importierte Haupteinträge: " & vbCrLf & "Importierte Untereinträge: "
    lblImportGroupCount.Text = "Importierte Gruppen: " & vbCrLf & "Importierte Untergruppen: " & vbCrLf & "Importierte Gruppeneinträge: "

    'Dialoge
    dlgExport.InitialDirectory = Application.StartupPath
    dlgImport.InitialDirectory = Application.StartupPath
    dlgSaveDb.InitialDirectory = Application.StartupPath
  End Sub

  Private Sub UpdateForm()
    ' Gruppen in die Listen einfügen
    lstGroupList.Items.Clear()          ' Liste der Gruppen in der aktuellen Sprache
    cmbUnitSelectGroup.Items.Clear()    ' 
    lstExportGroups.Items.Clear()    ' Liste der Gruppen zum exportieren

    Dim groupNames As Collection(Of String) = grp.GetGroups()
    For Each groupName As String In groupNames
      lstGroupList.Items.Add(groupName)
      cmbUnitSelectGroup.Items.Add(groupName)
      lstExportGroups.Items.Add(groupName)
    Next

    If groupNames.Count > 0 Then
      lstGroupList.SelectedIndex = 0
      cmbUnitSelectGroup.SelectedIndex = 0
      lstExportGroups.SelectedIndex = 0
    End If

    If lstGroupList.Items.Count = 0 Then
      cmdGroupDelete.Enabled = False
      cmdGroupEdit.Enabled = False
      txtGroupName.Text = ""
      lblGroupInfo.Text = "Keine Gruppe vorhanden"
      cmdUnitDelete.Enabled = False
      cmdUnitEdit.Enabled = False
      txtUnitName.Text = ""
      lblUnitInfo.Text = "Keine Gruppe vorhanden"
      cmbUnitSelectGroup.Text = ""
    Else
      cmdGroupDelete.Enabled = True
      cmdGroupEdit.Enabled = True
      cmdUnitDelete.Enabled = False ' nicht implementiert
      cmdUnitEdit.Enabled = True
    End If

    ' lade Sprachen in die Export-Sprachen-Liste, nur tatsächlich vorhandene! (LDF-unabhängig)
    lstExportLanguages.Items.Clear()
    For Each language As String In dic.DictionaryLanguages()
      lstExportLanguages.Items.Add(language)
    Next
    If lstExportLanguages.Items.Count > 0 Then lstExportLanguages.SelectedIndex = 0
  End Sub

  Private Sub CloseForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSchließen.Click
    Me.Close()
  End Sub

  Private Sub ClosingForm(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    voc.Close()
  End Sub

  Private Sub DataUpdateDBVersion(ByVal sender As System.Object, ByVal e As System.EventArgs)
    man.UpdateDatabaseVersion()
    UpdateForm()
    If man.DatabaseVersionIndex <> 0 Then
      cmdDBVersion.Text = "Update auf Version " & man.DatabaseVersion(man.NextVersionIndex)
      cmdDBVersion.Enabled = True
    Else
      cmdDBVersion.Text = "Update auf Version " & man.DatabaseVersion(0)
      cmdDBVersion.Enabled = False
    End If
  End Sub

  Private Sub GroupAdd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGroupAdd.Click
    ' da eine gruppe nicht direkt hinzugefügt werden kann, wird direkt ein untereintrag erzeugt
    If Trim(txtGroupName.Text = "") Then Exit Sub
    Try
      grp.AddGroup(txtGroupName.Text, "Untereintrag 1")
    Catch e2 As xlsExceptionEntryExists
      MsgBox("Gruppen können nur einmal unter einem Namen existieren.", MsgBoxStyle.Information, "Warning")
      Exit Sub
    Catch es As Exception
      MsgBox("Ein Fehler ist aufgetreten: " & es.Message, MsgBoxStyle.Critical, "Error")
      UpdateForm()
      Exit Sub
    End Try
    UpdateForm()
  End Sub

  Private Sub GroupEdit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGroupEdit.Click
    If Trim(Me.txtGroupName.Text) = "" Then Exit Sub

    ' Ändern der Gruppen-Informationen in der Datenbank
    grp.EditGroup(lstGroupList.SelectedItem, txtGroupName.Text)

    ' Anzeige Aktualisieren
    UpdateForm()
    lstGroupList.SelectedIndex = lstGroupList.FindStringExact(txtGroupName.Text)
  End Sub

  Private Sub GroupDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGroupDelete.Click
    If MsgBox("Wollen sie wirklich die komplette Gruppe löschen?", MsgBoxStyle.YesNo, "Warning") = MsgBoxResult.No Then Return

    grp.DeleteGroup(lstGroupList.SelectedItem)
    UpdateForm()
  End Sub

  Private Sub GroupSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstGroupList.SelectedIndexChanged
    If lstGroupList.SelectedIndex = -1 Then Exit Sub ' Irreguläre Werte abfangen
    txtGroupName.Text = lstGroupList.SelectedItem     ' Text aktualisieren
    Dim count As Integer = grp.WordCount(txtGroupName.Text)
    lblGroupInfo.Text = IIf(count = 1, count & " Eintrag", count & " Einträge")
  End Sub

  Private Sub UnitAdd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnitAdd.Click
    If Trim(txtUnitName.Text = "") Then Exit Sub
    Try
      grp.AddGroup(cmbUnitSelectGroup.SelectedItem, txtUnitName.Text)
    Catch e2 As xlsExceptionEntryExists
      MsgBox("Gruppen können nur einmal unter einem Namen existieren.", MsgBoxStyle.Information, "Warning")
      Exit Sub
    Catch es As Exception
      MsgBox("Ein Fehler ist aufgetreten: " & es.Message, MsgBoxStyle.Critical, "Error")
      UpdateForm()
      Exit Sub
    End Try
    Me.lstUnitList.Items.Add(txtUnitName.Text)
    Me.lstUnitList.SelectedItem = txtUnitName.Text
  End Sub

  Private Sub UnitEdit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnitEdit.Click
    If Trim(txtUnitName.Text) = "" Then Exit Sub

    ' Ändern der Gruppen-Informationen in der Datenbank
    grp.EditSubGroup(cmbUnitSelectGroup.SelectedItem, lstUnitList.SelectedItem, txtUnitName.Text)

    ' Anzeige Aktualisieren
    UnitSelectGroup(sender, e)
    lstUnitList.SelectedIndex = lstUnitList.FindStringExact(txtUnitName.Text)
  End Sub

  Private Sub UnitDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnitDelete.Click
    MsgBox("Leider ist es zur Zeit nicht möglich Units zu Löschen oder zu Verschieben.", vbInformation)
  End Sub

  Private Sub UnitSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstUnitList.SelectedIndexChanged
    txtUnitName.Text = lstUnitList.SelectedItem
    Dim group As xlsGroup = grp.GetGroup(cmbUnitSelectGroup.SelectedItem, txtUnitName.Text)
    Dim count As Integer = group.WordCount
    lblUnitInfo.Text = IIf(count = 1, count & " Eintrag", count & " Einträge")
    count = group.LanguageCount
    lblUnitInfo.Text &= vbCrLf & IIf(count = 1, count & " benutzte Sprache", count & " benutzte Sprachen")
  End Sub

  Private Sub UnitSelectGroup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnitSelectGroup.SelectedIndexChanged
    ' Lektionen anzeigen
    lstUnitList.Items.Clear()     ' Liste leeren

    Dim subGroups As Collection(Of xlsGroupEntry) = grp.GetSubGroups(cmbUnitSelectGroup.SelectedItem)
    For Each entry As xlsGroupEntry In subGroups
      lstUnitList.Items.Add(entry.SubGroup)
    Next

    If lstUnitList.Items.Count > 0 Then lstUnitList.SelectedIndex = 0
  End Sub

  Private Sub cmdDataSelectSaveDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImortSelectDB.Click
    Dim res As DialogResult = dlgImport.ShowDialog(Me)
    Dim db As AccessDatabaseOperation
    If res = Windows.Forms.DialogResult.OK Then
      db = New AccessDatabaseOperation()
      Try ' Testweise öffnen
        db.Open(dlgImport.FileName)
        db.Close()
        lblImportDB.Text = "Datenbank: " & dlgImport.FileName
        cmdImportDictionary.Enabled = True
        cmdImportGroup.Enabled = True
        dlgExport.FileName = dlgImport.FileName
        importFilename = dlgImport.FileName
      Catch ex As System.Data.OleDb.OleDbException
        If ex.ErrorCode = -2147467259 Then
          lblImportDB.Text = "Datenbank: noch keine gewählt"
          Me.cmdImportDictionary.Enabled = False
          Me.cmdImportGroup.Enabled = False
          MsgBox("Bitte geben sie eine existierende Datei an", MsgBoxStyle.Information, "Fehler")
        Else
          lblImportDB.Text = "Datenbank: noch keine gewählt"
          cmdImportDictionary.Enabled = False
          cmdImportGroup.Enabled = False
          MsgBox("Unbekannter Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
          Exit Sub
        End If
      End Try
    End If
  End Sub

  Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click
    ' Teste, ob die aktuelle Dateie die höchste Version hat
    If Not man.IsVersionUpToDate Then
      MsgBox("Ihre Datenbank ist nicht aktuell. Bitte aktualisieren Sie sie bevor Sie Daten exportieren.", MsgBoxStyle.Information, "Fehler")
      Exit Sub
    End If

    Dim res As DialogResult = dlgExport.ShowDialog(Me)
    Dim db As AccessDatabaseOperation
    If res = Windows.Forms.DialogResult.OK Then
      db = New AccessDatabaseOperation()
      Try
        db.Open(dlgExport.FileName)
        ' datei existierte schon.
        ' überschreiben
        db.Close()
        Try
          FileCopy(Application.StartupPath() & "\empty.mdb", dlgExport.FileName)
          man.CreateNewVocabularyDatabase(dlgExport.FileName)
        Catch ex As Exception
          MsgBox(ex.Message, MsgBoxStyle.Critical, "Fehler")
          Exit Sub
        End Try
        Try
          db.Open(dlgExport.FileName) ' müsste funktionieren
        Catch ex As Exception
          MsgBox(ex.Message, MsgBoxStyle.Critical, "Fehler")
          Exit Sub
        End Try
      Catch ex As System.Data.OleDb.OleDbException
        If ex.ErrorCode = -2147467259 Then
          ' db existiert nicht
          FileCopy(Application.StartupPath() & "\empty.mdb", dlgExport.FileName)
          man.CreateNewVocabularyDatabase(dlgExport.FileName)
          db.Open(dlgExport.FileName)
        Else
          MsgBox("Unbekannter Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
          Exit Sub
        End If
      End Try
    Else
      Exit Sub
    End If
    dlgImport.FileName = dlgExport.FileName

    ' sichern
    Dim export As New xlsImportExport
    export.DBConnection = voc.DBConnection
    export.ExportEmptyEntrys = Not chkExportEmptyEntrys.Checked
    export.ExportStats = chkExportStats.Checked
    For Each selectedLanguage As String In lstExportLanguages.CheckedItems
      export.ExportLanguage(selectedLanguage, "german", db)
    Next
    For Each selectedGroup As String In lstExportGroups.CheckedItems
      export.ExportGroup(selectedGroup, "german", db)
    Next

    ' Meldung
    MsgBox("Exportieren erfolgreich!", MsgBoxStyle.Information, Application.ProductName)
  End Sub

  Private Sub cmdReorganizeDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReorganizeDB.Click
    man.Reorganize()
    lblErrorCount.Text = "Gefundene und behobene Fehler: " & man.ErrorCount
    MsgBox("Testen der Datenbank auf Konsistenz abgeschlossen.", MsgBoxStyle.Information, "Hinweis")
  End Sub

  Private Sub cmdSaveDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveDB.Click
    Dim res As DialogResult = dlgSaveDb.ShowDialog(Me)
    If res = Windows.Forms.DialogResult.OK Then
      Try
        FileCopy(Application.StartupPath() & "\voc.mdb", dlgExport.FileName)
      Catch ex As Exception
        MsgBox("Beim Kopieren ist ein Fehler aufgetreten: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
      End Try
    Else
      Exit Sub
    End If
  End Sub

  Private Sub cmdImportGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImportGroup.Click
    ' Teste, ob beide Dateien die höchste Version haben
    If Not man.IsVersionUpToDate Then
      MsgBox("Ihre Datenbank ist nicht aktuell. Bitte aktualisieren Sie sie bevor Sie Daten exportieren.", MsgBoxStyle.Information, "Fehler")
      Exit Sub
    End If

    ' Sichern von Gruppen
    Dim db As New AccessDatabaseOperation
    Try
      db.Open(importFilename)
    Catch ex As Exception
      MsgBox("Fehler beim Datenbankzugriff: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
      Exit Sub
    End Try
    Dim versionTest As New xlsManagement
    versionTest.DBConnection = db
    If Not versionTest.IsVersionUpToDate Then
      Dim res As MsgBoxResult = MsgBox("Die Version der zu importierenden Datenbank ist nicht aktuell. Soll sie aktualisiert werden?", MsgBoxStyle.YesNo, "Warnung")
      If res = MsgBoxResult.No Then
        db.Close()
        Exit Sub
      End If
      ' aktualisieren
      While Not versionTest.IsVersionUpToDate
        versionTest.UpdateDatabaseVersion()
      End While
    End If

    Dim import As New xlsImportExport(voc.DBConnection)
    import.ImportGroups("german", db)
    db.Close()

    lblImportDictCount.Text = "Importierte Haupteinträge: " & import.ImportedMainEntrys & vbCrLf & "Importierte Untereinträge: " & import.ImportedSubEntrys
    lblImportGroupCount.Text = "Importierte Gruppen: " & import.ImportedGroups & vbCrLf & "Importierte Untergruppen: " & import.ImportedSubGroups & vbCrLf & "Importierte Gruppeneinträge: " & import.ImportedGroupEntrys
    UpdateForm()

    ' Meldung
    MsgBox("Importieren erfolgreich!", MsgBoxStyle.Information, Application.ProductName)
  End Sub

  Private Sub cmdImportDictionary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImportDictionary.Click
    ' sichern
    Dim db As New AccessDatabaseOperation()
    Try
      db.Open(importFilename)
    Catch ex As Exception
      MsgBox("Fehler beim Datenbankzugriff: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
      Exit Sub
    End Try
    Dim import As New xlsImportExport(voc.DBConnection)
    import.ImportDictionary("german", db)
    db.Close()

    lblImportDictCount.Text = "Importierte Haupteinträge: " & import.ImportedMainEntrys & vbCrLf & "Importierte Untereinträge: " & import.ImportedSubEntrys
    lblImportGroupCount.Text = "Importierte Gruppen: " & import.ImportedGroups & vbCrLf & "Importierte Untergruppen: " & import.ImportedSubGroups & vbCrLf & "Importierte Gruppeneinträge: " & import.ImportedGroupEntrys
    UpdateForm()

    ' Meldung
    MsgBox("Importieren erfolgreich!", MsgBoxStyle.Information, Application.ProductName)
  End Sub

  Private Sub cmdDBVersion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDBVersion.Click
    man.UpdateDatabaseVersion()
    UpdateDatabaseVersion()
  End Sub

  Private Sub UpdateDatabaseVersion()
    lblDBVersion.Text = "Aktuelle Datenbank-Version: " & man.DatabaseVersion
    If man.IsVersionUpToDate Then
      cmdDBVersion.Text = "Update auf Version " & man.DatabaseVersion(0)
      cmdDBVersion.Enabled = False
    Else
      cmdDBVersion.Text = "Update auf Version " & man.DatabaseVersion(man.NextVersionIndex)
      cmdDBVersion.Enabled = True
    End If
  End Sub

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    FileCopy(Application.StartupPath() & "\empty.mdb", Application.StartupPath() & "\erzeugt.mdb")
    man.CreateNewVocabularyDatabase(Application.StartupPath() & "\erzeugt.mdb")
  End Sub
End Class
