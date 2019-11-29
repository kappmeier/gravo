Imports System.Collections.ObjectModel

Public Class WordInput
    Dim db As New SQLiteDataBaseOperation                 ' Datenbankoperationen für Microsoft Access Datenbanken
    Dim grp As New xlsGroup("")                           ' Zugriff auf eine Gruppe
    Dim DictionaryDao As IDictionaryDao
    Dim xlsGroups As New xlsGroups
    ''' <summary>
    ''' Data access for groups.
    ''' </summary>
    Dim GroupsDao As IGroupsDao
    ''' <summary>
    ''' Currently loaded group
    ''' </summary>
    Dim GroupEntry As GroupEntry
    Dim GroupDao As IGroupDao

    Dim language As String
    Dim mainLanguage As String

    ' easy-word-input Variablen
    Dim wordEdited As Boolean = False

    Public Sub New()
        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        db.Open(DBPath)
        grp.DBConnection = db
        xlsGroups.DBConnection = db
        DictionaryDao = New DictionaryDao(db)
        GroupsDao = New GroupsDao(db)

        Dim propertiesDao As IPropertiesDao = New PropertiesDao(db)
        Dim properties As Properties = propertiesDao.LoadProperties
        txtWord.MaxLength = properties.DictionaryWordsMaxLengthWord
        txtPre.MaxLength = properties.DictionaryWordsMaxLengthPre
        txtPost.MaxLength = properties.DictionaryWordsMaxLengthPost
        txtMeaning.MaxLength = properties.DictionaryWordsMaxLengthMeaning
        txtAdditionalTargetlanguageInfo.MaxLength = properties.DictionaryWordsMaxLengthAdditionalTargetLangInfo
        txtMainEntry.MaxLength = properties.DictionaryMainMaxLengthWordEntry
        txtLanguage.MaxLength = properties.DictionaryMainMaxLengthLanguage
        txtMainLanguage.MaxLength = properties.DictionaryMainMaxLengthMainLanguage
    End Sub

    Private Sub WordInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Position
        Me.Left = Me.Owner.Left + Me.Owner.Width / 2 - Me.Width / 2
        Me.Top = Me.Owner.Top + Me.Owner.Height / 2 - Me.Height / 2
        If Me.Top < 0 Then Me.Top = 0
        If Me.Left < 0 Then Me.Left = 0

        ' Sprachen in die Listen einfügen
        cmbLanguages.Items.Clear()
        Dim languages As Collection(Of String) = DictionaryDao.DictionaryLanguages("german")
        For Each language As String In languages
            cmbLanguages.Items.Add(language)
        Next
        If languages.Count > 0 Then cmbLanguages.SelectedIndex = 0
        cmbMainLanguages.Items.Clear()
        languages = DictionaryDao.DictionaryMainLanguages()
        For Each language As String In languages
            cmbMainLanguages.Items.Add(language)
        Next
        If languages.Count > 0 Then cmbMainLanguages.SelectedIndex = 0
        UpdateLanguageSelection()

        ' Laden der Gruppen in das Auswahlfeld
        cmbDirectAddGroup.Items.Clear()
        Dim groupNames As Collection(Of String) = GroupsDao.GetGroups()
        For Each groupName As String In groupNames
            cmbDirectAddGroup.Items.Add(groupName)
        Next
        If groupNames.Count > 0 Then cmbDirectAddGroup.SelectedIndex = 0
        If cmbDirectAddGroup.Items.Count > 0 Then cmbDirectAddGroup.SelectedIndex = 0 Else chkDirectAdd.Enabled = False
        chkDirectAdd.Checked = False
        cmbDirectAddGroup.Enabled = False
        cmbDirectAddSubGroup.Enabled = False
    End Sub

    ' Lokalisierung
    Public Overrides Sub LocalizationChanged()

    End Sub

    Private Sub AddSubEntry(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddSubEntry.Click
        If chkDirectAdd.Checked And grp Is Nothing Then
            MsgBox("Bitte wählen sie eine existierende Gruppe aus. Eintrag wird nicht erstellt!", MsgBoxStyle.Information, "Warnung")
            Exit Sub
        End If

        ' Falls keine Sprache in der Gruppe vorhanden ist (0 Einträgee bisher), nachfragen ob wirklich erstellt werden soll
        If grp.LanguageCount() = 0 Or grp.MainLanguageCount() = 0 Then
            If MsgBox("Es ist bisher noch kein Eintrag in der gewählten Gruppe vorhanden. Soll ein neuer Eintrag mit den Sprachen '" & language & "' und '" & mainLanguage & "' erstellt werden?", MsgBoxStyle.YesNo, "Neue Sprache") = MsgBoxResult.No Then Exit Sub
        End If

        ' Falls bisher eine Sprache in der Gruppe vorhanden ist, nachfragen ob eine neue erstellt werden soll
        If grp.LanguageCount = 1 And grp.MainLanguageCount = 1 Then
            Dim usedLanguage As String
            Dim usedMainLanguage As String
            usedLanguage = GroupDao.GetUniqueLanguage(GroupEntry)
            usedMainLanguage = grp.GetUniqueMainLanguage()
            If usedLanguage <> language Or usedMainLanguage <> mainLanguage Then
                If MsgBox("Sie beabsichtigen einen eintrag mit den zweiten Sprachen '" & language & "' und '" & mainLanguage & "' zu erstellen. Soll damit fortgefahren werden?", MsgBoxStyle.YesNo, "Neue Sprache") = MsgBoxResult.No Then Exit Sub
            End If
        End If

        Dim deWord As New WordEntry(txtWord.Text, txtPre.Text, txtPost.Text, lstWordTypes.SelectedIndex, txtMeaning.Text, txtAdditionalTargetlanguageInfo.Text, chkIrregular.Checked)

        Try
            DictionaryDao.AddSubEntry(deWord, txtMainEntry.Text, language, mainLanguage)
            If chkDirectAdd.Checked Then AddToGroup()
        Catch ex As EntryExistsException
            ' Eintrag existiert schon
            If chkDirectAdd.Checked Then AddToGroup()
        Catch ex As EntryNotFoundException
            ' Da der Haupteintrag nicht vorhanden ist, muß hier auch nicht auf die xlsExists-Exception geachtet werden.
            Dim res As MsgBoxResult = MsgBox("Der Haupteintrag " & txtMainEntry.Text & " ist für die gewählten Sprachen nicht vorhanden. Soll er erstellt werden?", MsgBoxStyle.YesNo, "Haupteintrag nicht vorhanden")
            If res = MsgBoxResult.Yes Then
                ' Hinzufügen. Da die nicht-existiert-exception auftrat, kann nicht mehr die existiert-schon-exception auftreten
                Try
                    DictionaryDao.AddEntry(Trim(txtMainEntry.Text), language, mainLanguage)
                Catch sex As xlsExceptionInput
                    MsgBox(sex.Message, MsgBoxStyle.Information, "Unkorrekte Eingabe")
                End Try
                ' Erneut den subentry hinzufügen
                Try
                    DictionaryDao.AddSubEntry(deWord, txtMainEntry.Text, language, mainLanguage)
                    ' hinzufügen in die gruppe
                    If chkDirectAdd.Checked Then AddToGroup()
                Catch sex As EntryExistsException
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
        txtMainEntry.SelectAll()
        txtMainEntry.Focus()
        txtAdditionalTargetlanguageInfo.SelectAll()
        txtMeaning.SelectAll()
        txtPost.SelectAll()
        txtPre.SelectAll()
        txtWord.SelectAll()
        wordEdited = False
    End Sub

    Private Sub chkDirectAdd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDirectAdd.CheckedChanged
        cmbDirectAddGroup.Enabled = chkDirectAdd.Checked
        cmbDirectAddSubGroup.Enabled = chkDirectAdd.Checked
    End Sub

    Private Sub AddToGroup()
        ' Davon ausgehen, daß das Einfügen in die Wortliste korrekt erfolgt ist
        Dim mainEntry As MainEntry = DictionaryDao.GetMainEntry(txtMainEntry.Text, language, mainLanguage)
        Dim wordEntry As WordEntry = DictionaryDao.GetEntry(mainEntry, txtWord.Text, txtMeaning.Text)
        ' TODO example
        GroupDao.Add(GroupEntry, wordEntry, chkMarked.Checked, "")
    End Sub

    Private Sub cmbLanguages_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLanguages.SelectedIndexChanged
        ' Liste der Wortarten füllen (immer alle unterstützen zur zeit)
        lstWordTypes.Items.Clear()

        Dim propertiesDao As New PropertiesDao(db)
        For Each type As String In propertiesDao.LoadWordTypes.GetSupportedWordTypes()
            lstWordTypes.Items.Add(GetLoc.GetText(type))
        Next type
        lstWordTypes.SelectedIndex = 0

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
        Dim subGroups As Collection(Of GroupEntry) = GroupsDao.GetSubGroups(cmbDirectAddGroup.SelectedItem)
        For Each entry As GroupEntry In subGroups
            cmbDirectAddSubGroup.Items.Add(entry.SubGroup)
        Next
        If cmbDirectAddSubGroup.Items.Count > 0 Then cmbDirectAddSubGroup.SelectedIndex = 0
    End Sub

    Private Sub cmbDirectAddSubGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDirectAddSubGroup.SelectedIndexChanged
        If cmbDirectAddSubGroup.Items.Count = 0 Then grp = Nothing : Exit Sub
        GroupEntry = GroupsDao.GetGroup(cmbDirectAddGroup.SelectedItem, cmbDirectAddSubGroup.SelectedItem)

        ' Wenn die verwendeten Sprachen eindeutig sind, setzen
        Try
            Dim language As String = GroupDao.GetUniqueLanguage(GroupEntry)
            cmbLanguages.SelectedItem = language
        Catch ex As Exception
            ' keine eindeutige Language
            MsgBox("Sprache konnte nicht automatisch festgelegt werden. Bitte setzen sie manuell.")
        End Try

        Try
            Dim mainLanguage = grp.GetUniqueMainLanguage()
            cmbMainLanguages.SelectedItem = mainLanguage
        Catch ex As Exception
            ' keine eindeutige Mainlanguage
            MsgBox("Hauptsprache konnte nicht automatisch festgelegt werden. Bitte setzen sie manuell.")
        End Try

    End Sub

    Private Sub txtLanguage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLanguage.TextChanged
        language = txtLanguage.Text
    End Sub

    Private Sub txtMainLanguage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMainLanguage.TextChanged
        mainLanguage = txtMainLanguage.Text
    End Sub

    Private Sub txtMainEntry_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMainEntry.TextChanged
        If wordEdited = False Then
            txtWord.Text = txtMainEntry.Text
            txtWord.SelectAll()
            wordEdited = False
        End If
    End Sub

    Private Sub txtWord_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWord.TextChanged
        wordEdited = True
    End Sub
End Class