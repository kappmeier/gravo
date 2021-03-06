Imports System.Collections.ObjectModel
Imports Gravo.Properties

Public Class Management
    ' Datenbank-Zugriff
    ''' <summary>
    ''' Data access for groups.
    ''' </summary>
    Dim GroupsDao As IGroupsDao
    ''' <summary>
    ''' Data access for a single group.
    ''' </summary>
    Dim GroupDao As IGroupDao
    'Dim man As xlsManagement
    ''' <summary>
    ''' Data access to the dictionary.
    ''' </summary>
    Dim DictionaryDao As IDictionaryDao
    ''' <summary>
    ''' Data access to manage database versions.
    ''' </summary>
    Dim ManagementDao As IManagementDao

    Dim importFilename As String = ""

    Public Sub New()
        ' Dieser Aufruf ist f�r den Windows Form-Designer erforderlich.
        InitializeComponent()

        Dim db As IDataBaseOperation = New SQLiteDataBaseOperation()
        db.Open(DBPath)
        GroupsDao = New GroupsDao(db)
        GroupDao = New GroupDao(db)
        DictionaryDao = New CardsDao(db)
        ManagementDao = New ManagementDao(db)

        ' Anzahl der Zeichen f�r Textfelder
        Dim properties As Properties = New PropertiesDao(db).LoadProperties
        txtGroupName.MaxLength = properties.GroupsMaxLengthName
        txtUnitName.MaxLength = properties.GroupsMaxLengthSubName
    End Sub

    Private Sub LoadForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Position
        Me.Left = Me.Owner.Left + Me.Owner.Width / 2 - Me.Width / 2
        Me.Top = Me.Owner.Top + Me.Owner.Height / 2 - Me.Height / 2
        If Me.Top < 0 Then Me.Top = 0
        If Me.Left < 0 Then Me.Left = 0

        ' Form-Update durchf�hren (L�dt Gruppen und Bezeichnungen)
        UpdateForm()

        ' Datenbank-Version, reorganisieren, importieren, exportieren
        UpdateDatabaseVersionText()
        lblErrorCount.Text = "Gefundene und behobene Fehler: keine �berpr�fung durchgef�hrt"
        lblImportDB.Text = "Datenbank: noch keine gew�hlt"
        cmdImportDictionary.Enabled = False
        cmdImportGroup.Enabled = False
        lblImportDictCount.Text = "Importierte Haupteintr�ge: " & vbCrLf & "Importierte Untereintr�ge: "
        lblImportGroupCount.Text = "Importierte Gruppen: " & vbCrLf & "Importierte Untergruppen: " & vbCrLf & "Importierte Gruppeneintr�ge: "

        'Dialoge
        dlgExport.InitialDirectory = Application.StartupPath
        dlgImport.InitialDirectory = Application.StartupPath
        dlgSaveDb.InitialDirectory = Application.StartupPath
    End Sub

    ' Lokalisierung
    Public Overrides Sub LocalizationChanged()
        Me.cmdUnitUp.Text = GetLoc().GetText(localization.UP)
    End Sub

    Private Sub UpdateForm()
        ' Gruppen in die Listen einf�gen
        lstGroupList.Items.Clear()          ' Liste der Gruppen in der aktuellen Sprache
        cmbUnitSelectGroup.Items.Clear()    ' 
        lstExportGroups.Items.Clear()    ' Liste der Gruppen zum exportieren

        Dim groupNames As Collection(Of String) = GroupsDao.GetGroups()
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

        ' lade Sprachen in die Export-Sprachen-Liste, nur tats�chlich vorhandene! (LDF-unabh�ngig)
        lstExportLanguages.Items.Clear()
        For Each language As String In DictionaryDao.DictionaryLanguages("german")
            lstExportLanguages.Items.Add(language)
        Next
        If lstExportLanguages.Items.Count > 0 Then lstExportLanguages.SelectedIndex = 0
    End Sub

    Private Sub CloseForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSchlie�en.Click
        Me.Close()
    End Sub

    Private Sub ClosingForm(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

    End Sub

    Private Sub DataUpdateDBVersion(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ManagementDao.UpdateDatabaseVersion()
        UpdateForm()
        UpdateDatabaseVersionText()
    End Sub

    Private Sub GroupAdd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGroupAdd.Click
        ' da eine gruppe nicht direkt hinzugef�gt werden kann, wird direkt ein untereintrag erzeugt
        If Trim(txtGroupName.Text = "") Then Exit Sub
        Try
            GroupsDao.AddGroup(txtGroupName.Text, "Untereintrag 1")
        Catch e2 As EntryExistsException
            MsgBox("Gruppen k�nnen nur einmal unter einem Namen existieren.", MsgBoxStyle.Information, "Warning")
            Exit Sub
        Catch es As Exception
            MsgBox("Ein Fehler ist aufgetreten: " & es.Message, MsgBoxStyle.Critical, "Error")
            UpdateForm()
            Exit Sub
        End Try
        UpdateForm()
    End Sub

    Private Sub GroupEdit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGroupEdit.Click
        If Trim(txtGroupName.Text) = "" Then Exit Sub

        ' �ndern der Gruppen-Informationen in der Datenbank
        GroupsDao.EditGroup(lstGroupList.SelectedItem, txtGroupName.Text)

        ' Anzeige Aktualisieren
        UpdateForm()
        lstGroupList.SelectedIndex = lstGroupList.FindStringExact(txtGroupName.Text)
    End Sub

    Private Sub GroupDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGroupDelete.Click
        If MsgBox("Wollen sie wirklich die komplette Gruppe l�schen?", MsgBoxStyle.YesNo, "Warning") = MsgBoxResult.No Then Return

        GroupsDao.DeleteGroup(lstGroupList.SelectedItem)
        UpdateForm()
    End Sub

    Private Sub GroupSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstGroupList.SelectedIndexChanged
        If lstGroupList.SelectedIndex = -1 Then Exit Sub ' Irregul�re Werte abfangen
        txtGroupName.Text = lstGroupList.SelectedItem     ' Text aktualisieren
        Dim count As Integer = DataTools.WordCount(GroupsDao, GroupDao, txtGroupName.Text)
        lblGroupInfo.Text = IIf(count = 1, count & " Eintrag", count & " Eintr�ge")
    End Sub

    Private Sub UnitAdd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnitAdd.Click
        If Trim(txtUnitName.Text = "") Then Exit Sub
        Try
            GroupsDao.AddGroup(cmbUnitSelectGroup.SelectedItem, txtUnitName.Text)
        Catch e2 As EntryExistsException
            MsgBox("Gruppen k�nnen nur einmal unter einem Namen existieren.", MsgBoxStyle.Information, "Warning")
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

        ' �ndern der Gruppen-Informationen in der Datenbank
        GroupsDao.EditSubGroup(cmbUnitSelectGroup.SelectedItem, lstUnitList.SelectedItem, txtUnitName.Text)

        ' Anzeige Aktualisieren
        UnitSelectGroup(sender, e)
        lstUnitList.SelectedIndex = lstUnitList.FindStringExact(txtUnitName.Text)
    End Sub

    Private Sub UnitDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnitDelete.Click
        MsgBox("Leider ist es zur Zeit nicht m�glich Units zu L�schen oder zu Verschieben.", vbInformation)
    End Sub

    Private Sub UnitSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstUnitList.SelectedIndexChanged
        txtUnitName.Text = lstUnitList.SelectedItem
        Dim groupEntry As GroupEntry = GroupsDao.GetGroup(cmbUnitSelectGroup.SelectedItem, txtUnitName.Text)
        Dim groupData As GroupDto = GroupDao.Load(groupEntry)
        Dim count As Integer = groupData.WordCount
        lblUnitInfo.Text = IIf(count = 1, count & " Eintrag", count & " Eintr�ge")
        count = GroupDao.GetLanguages(groupEntry).Count
        lblUnitInfo.Text &= vbCrLf & IIf(count = 1, count & " benutzte Sprache", count & " benutzte Sprachen")
    End Sub

    Private Sub UnitSelectGroup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnitSelectGroup.SelectedIndexChanged
        ' Lektionen anzeigen
        lstUnitList.Items.Clear()     ' Liste leeren

        Dim subGroups As Collection(Of GroupEntry) = GroupsDao.GetSubGroups(cmbUnitSelectGroup.SelectedItem)
        For Each entry As GroupEntry In subGroups
            lstUnitList.Items.Add(entry.SubGroup)
        Next

        If lstUnitList.Items.Count > 0 Then lstUnitList.SelectedIndex = 0
    End Sub

    Private Sub cmdDataSelectSaveDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImortSelectDB.Click
        Dim res As DialogResult = dlgImport.ShowDialog(Me)
        Dim db As IDataBaseOperation
        If res = Windows.Forms.DialogResult.OK Then
            db = New SQLiteDataBaseOperation()
            Try ' Testweise �ffnen
                db.Open(dlgImport.FileName)
                db.Close()
                lblImportDB.Text = "Datenbank: " & dlgImport.FileName
                cmdImportDictionary.Enabled = True
                cmdImportGroup.Enabled = True
                dlgExport.FileName = dlgImport.FileName
                importFilename = dlgImport.FileName
            Catch ex As System.Data.OleDb.OleDbException
                If ex.ErrorCode = -2147467259 Then
                    lblImportDB.Text = "Datenbank: noch keine gew�hlt"
                    Me.cmdImportDictionary.Enabled = False
                    Me.cmdImportGroup.Enabled = False
                    MsgBox("Bitte geben sie eine existierende Datei an", MsgBoxStyle.Information, "Fehler")
                Else
                    lblImportDB.Text = "Datenbank: noch keine gew�hlt"
                    cmdImportDictionary.Enabled = False
                    cmdImportGroup.Enabled = False
                    MsgBox("Unbekannter Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
                    Exit Sub
                End If
            End Try
        End If
    End Sub

    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        ' Teste, ob die aktuelle Dateie die h�chste Version hat
        If Not ManagementDao.IsVersionUpToDate() Then
            MsgBox("Ihre Datenbank ist nicht aktuell. Bitte aktualisieren Sie sie bevor Sie Daten exportieren.", MsgBoxStyle.Information, "Fehler")
            Exit Sub
        End If

        Dim db As IDataBaseOperation = New SQLiteDataBaseOperation()
        Dim res As DialogResult = dlgExport.ShowDialog(Me)
        If res = Windows.Forms.DialogResult.OK Then
            If FileIO.FileSystem.FileExists(dlgExport.FileName) Then
                Try
                    FileIO.FileSystem.DeleteFile(dlgExport.FileName)
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "Fehler")
                    Exit Sub
                End Try
            End If

            'Try
            Gravo.ManagementDao.CreateNewVocabularyDatabase(dlgExport.FileName)
            db.Open(dlgExport.FileName)
            'Catch ex As Exception
            '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Fehler")
            '    Exit Sub
            'End Try

            'db = New AccessDatabaseOperation()
            'Try
            '    db.Open(dlgExport.FileName)
            '    ' datei existierte schon.
            '    ' �berschreiben
            '    db.Close()
            '    Try
            '        FileCopy(Application.StartupPath() & "\empty.mdb", dlgExport.FileName)
            '        man.CreateNewVocabularyDatabase(dlgExport.FileName)
            '    Catch ex As Exception
            '        MsgBox(ex.Message, MsgBoxStyle.Critical, "Fehler")
            '        Exit Sub
            '    End Try
            '    Try
            '        db.Open(dlgExport.FileName) ' m�sste funktionieren
            '    Catch ex As Exception
            '        MsgBox(ex.Message, MsgBoxStyle.Critical, "Fehler")
            '        Exit Sub
            '    End Try
            'Catch ex As System.Data.OleDb.OleDbException
            '    If ex.ErrorCode = -2147467259 Then
            '        ' db existiert nicht
            '        FileCopy(Application.StartupPath() & "\empty.mdb", dlgExport.FileName)
            '        man.CreateNewVocabularyDatabase(dlgExport.FileName)
            '        db.Open(dlgExport.FileName)
            '    Else
            '        MsgBox("Unbekannter Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
            '        Exit Sub
            '    End If
            'End Try
        Else
            Exit Sub
        End If
        dlgImport.FileName = dlgExport.FileName

        ' sichern
        Dim export As New xlsImportExport
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
        Dim errorCount = ManagementDao.Reorganize
        lblErrorCount.Text = "Gefundene und behobene Fehler: " & errorCount
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
        ' Teste, ob beide Dateien die h�chste Version haben
        If Not ManagementDao.IsVersionUpToDate() Then
            MsgBox("Ihre Datenbank ist nicht aktuell. Bitte aktualisieren Sie sie bevor Sie Daten exportieren.", MsgBoxStyle.Information, "Fehler")
            Exit Sub
        End If

        ' Sichern von Gruppen
        Dim db As IDataBaseOperation = New SQLiteDataBaseOperation()
        Try
            db.Open(importFilename)
        Catch ex As Exception
            MsgBox("Fehler beim Datenbankzugriff: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
            Exit Sub
        End Try
        Dim versionTest As IManagementDao = New ManagementDao(db)
        If Not versionTest.IsVersionUpToDate() Then
            Dim res As MsgBoxResult = MsgBox("Die Version der zu importierenden Datenbank ist nicht aktuell. Soll sie aktualisiert werden?", MsgBoxStyle.YesNo, "Warnung")
            If res = MsgBoxResult.No Then
                db.Close()
                Exit Sub
            End If
            ' update
            versionTest.UpdateDatabaseVersion()
        End If

        Dim import As New xlsImportExport(Nothing)
        import.ImportGroups("german", db)
        db.Close()

        lblImportDictCount.Text = "Importierte Haupteintr�ge: " & import.ImportedMainEntrys & vbCrLf & "Importierte Untereintr�ge: " & import.ImportedSubEntrys
        lblImportGroupCount.Text = "Importierte Gruppen: " & import.ImportedGroups & vbCrLf & "Importierte Untergruppen: " & import.ImportedSubGroups & vbCrLf & "Importierte Gruppeneintr�ge: " & import.ImportedGroupEntrys
        UpdateForm()

        ' Meldung
        MsgBox("Importieren erfolgreich!", MsgBoxStyle.Information, Application.ProductName)
    End Sub

    Private Sub cmdImportDictionary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImportDictionary.Click
        ' sichern
        Dim db As IDataBaseOperation = New SQLiteDataBaseOperation()
        Try
            db.Open(importFilename)
        Catch ex As Exception
            MsgBox("Fehler beim Datenbankzugriff: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
            Exit Sub
        End Try
        Dim import As New xlsImportExport(Nothing)
        import.ImportDictionary("german", db)
        db.Close()

        lblImportDictCount.Text = "Importierte Haupteintr�ge: " & import.ImportedMainEntrys & vbCrLf & "Importierte Untereintr�ge: " & import.ImportedSubEntrys
        lblImportGroupCount.Text = "Importierte Gruppen: " & import.ImportedGroups & vbCrLf & "Importierte Untergruppen: " & import.ImportedSubGroups & vbCrLf & "Importierte Gruppeneintr�ge: " & import.ImportedGroupEntrys
        UpdateForm()

        ' Meldung
        MsgBox("Importieren erfolgreich!", MsgBoxStyle.Information, Application.ProductName)
    End Sub

    Private Sub cmdDBVersion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDBVersion.Click
        Dim currentVersion As DBVersion = ManagementDao.GetCurrentVersion
        If ManagementDao.IsUpdateComplex(currentVersion) Then MsgBox("Der Updatevorgang kann einige Zeit dauern!", MsgBoxStyle.Information, "Hinweis")
        ManagementDao.UpdateDatabaseVersion()
        UpdateDatabaseVersionText()
    End Sub

    Private Sub UpdateDatabaseVersionText()
        Dim nextVersion = ManagementDao.GetNextVersion
        Dim currentVersion = ManagementDao.GetCurrentVersion
        If nextVersion Is Nothing Then
            cmdDBVersion.Text = "Auf aktueller Version " & currentVersion.ToString
            cmdDBVersion.Enabled = False
        Else
            cmdDBVersion.Text = "Update auf Version " & nextVersion.ToString
            cmdDBVersion.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Moves the selected unit one position up. If the unit is the last one, nothing happens.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmdUnitUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnitUp.Click
        If (lstUnitList.SelectedIndex = 0) Then Exit Sub
        GroupsDao.SwapGroups(cmbUnitSelectGroup.SelectedItem, lstUnitList.SelectedItem, lstUnitList.Items(lstUnitList.SelectedIndex - 1))
    End Sub

    ''' <summary>
    ''' Moves the selected unit one position down. If the unit is the last one, nothing happens.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmdUnitDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnitDown.Click
        If (lstUnitList.SelectedIndex = lstUnitList.Items.Count - 1) Then Exit Sub
        GroupsDao.SwapGroups(cmbUnitSelectGroup.SelectedItem, lstUnitList.SelectedItem, lstUnitList.Items(lstUnitList.SelectedIndex + 1))
    End Sub

    Private Sub tabGroup_Click(sender As Object, e As EventArgs) Handles tabGroup.Click

    End Sub
End Class
