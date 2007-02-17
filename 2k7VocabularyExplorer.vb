Imports System.Diagnostics
Imports System.Windows.Forms

'Temporär:
Imports System.Data.OleDb

Public Class VocabularyExplorer
  Enum ListViewStyle
    WordSubEntry
    WordEntry
  End Enum

  Private Const NODE_LEVEL_LANGUAGE As Integer = 0
  Private Const NODE_LEVEL_ENTRY As Integer = NODE_LEVEL_LANGUAGE + 1
  Private Const NODE_LEVEL_SUBENTRY As Integer = NODE_LEVEL_ENTRY + 1

  'Gibt an, ob der ausgewählte Knoten der Strukturansicht programmgesteuert geändert wird
  Private ChangingSelectedNode As Boolean

  Dim db As New AccessDatabaseOperation                 ' Datenbankoperationen für Microsoft Access Datenbanken
  Dim voc As New xlsDictionary                          ' Zugriff auf die Wort-Datenbank allgemein

  Private Sub VocabularyExplorer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    db.Open(Application.StartupPath() & "\voc.mdb")     ' Datenbank öffnen
    voc.DBConnection = db
    'Benutzeroberfläche einrichten
    SetUpListViewColumns(ListViewStyle.WordEntry)
    LoadTree()

    Exit Sub



    '    ' Hinzufügen der Vokabeln aus espresso 1 in DictionaryWords
    '    Dim i As Integer
    '    Dim sTable As String = "italian05"
    '    Dim cWords As New Collection
    '    Dim cAddWords As New Collection
    '    Dim DBCursor As OleDbDataReader
    '    Dim DBCursor2 As OleDbDataReader
    '    Dim sCommand As String = "SELECT Word FROM " & sTable & " WHERE bla=" & False & ";"
    '    Dim sTemp As String
    '    DBCursor = db.ExecuteReader(sCommand)
    '    Do While DBCursor.Read
    '      If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then sTemp = DBCursor.GetValue(0)
    '      cWords.Add(sTemp)
    '    Loop
    '    DBCursor.Close()

    '    ' Alle Wörter sind in der Words-Liste.  Testen, ob das wort in der Dictionary-Main-Liste enthalten ist
    '    For i = 1 To cWords.Count
    '      sCommand = "SELECT Index FROM DictionaryMain WHERE WordEntry='" & xlsBase.AddHighColons(cWords.Item(i)) & "';"
    '      DBCursor = db.ExecuteReader(sCommand)
    '      If DBCursor.HasRows = True Then
    '        ' Es gibt einen Eintrag mit diesem Namen
    '        DBCursor.Read()
    '        Dim index As Integer = DBCursor.GetInt32(0)
    '        DBCursor.Close()
    '        sCommand = "SELECT Word, Pre,Post, WordType, Meaning1, Description, AdditionalTargetLangInfo, WordNumber FROM " & sTable & " WHERE Word='" & xlsBase.AddHighColons(cWords.Item(i)) & "' AND bla=0;"

    '        DBCursor = db.ExecuteReader(sCommand)
    '        If DBCursor.HasRows = True Then
    '          DBCursor.Read()
    '          sTemp = DBCursor.GetValue(0)
    '          sCommand = "INSERT INTO DictionaryWords VALUES(" & index & ", '" & xlsBase.AddHighColons(DBCursor.GetString(0)) & "', '" & xlsBase.AddHighColons(DBCursor.GetString(1)) & "', '" & xlsBase.AddHighColons(DBCursor.GetString(2)) & "', " & xlsBase.AddHighColons(DBCursor.GetInt32(3)) & ", '" & xlsBase.AddHighColons(DBCursor.GetString(4)) & "', '" & xlsBase.AddHighColons(DBCursor.GetString(5)) & "', '" & xlsBase.AddHighColons(DBCursor.GetString(6)) & "', " & False & ");"
    '          Dim wordnumber As Integer = DBCursor.GetInt32(7)
    '          DBCursor.Close()
    '          db.ExecuteNonQuery(sCommand)

    '          sCommand = "UPDATE " & sTable & " SET bla=" & True & " WHERE Word='" & xlsBase.AddHighColons(cWords.Item(i)) & "' AND WordNumber=" & wordnumber & ";"
    '          db.ExecuteNonQuery(sCommand)
    '        End If
    '      Else
    'a:
    '        ' Es gibt keinen Eintrag mit diesem Namen
    '        ' Abfragen ob ein neuer angelegt werden soll
    '        Dim result As MsgBoxResult = MsgBox(" Für " & cWords.Item(i) & " existiert kein Eintrag. Soll das Wort unter einem anderen Eintrag hinzugefügt werden?", MsgBoxStyle.YesNo)
    '        If result = MsgBoxResult.Yes Then
    '          ' nach dem namen fragen
    '          Dim sresult As String = InputBox("Bitte legen Sie fest unter welchem Eintrag " & cWords.Item(i) & " gespeichert werden soll.")
    '          'Testen, ob der gwählte eintrag überhaupt vorhanden ist. Falls nicht, nachfragen, ob er erstellt werden soll.
    '          ' ansonsten nochmal testen, oder mit cancel abbrechen.
    '          sCommand = "SELECT Index FROM DictionaryMain WHERE WordEntry='" & xlsBase.AddHighColons(sresult) & "';"
    '          DBCursor = db.ExecuteReader(sCommand)
    '          Dim index As Integer
    '          If DBCursor.HasRows = True Then
    '            ' kann hinzugefügt werden
    '            DBCursor.Read()
    '            index = DBCursor.GetInt32(0)
    '            DBCursor.Close()
    '          Else
    '            result = MsgBox("Eintrag nicht vorhanden. Soll er erstellt werden?", MsgBoxStyle.YesNoCancel)
    '            If result = MsgBoxResult.Yes Then

    '              ' den höchsten Index-Eintrag herausbekommen, 
    '              sCommand = "SELECT MAX(Index) FROM DictionaryMain;"
    '              DBCursor = db.ExecuteReader(sCommand)
    '              DBCursor.Read()
    '              index = DBCursor.GetInt32(0) + 1
    '              DBCursor.Close()

    '              ' Eintrag in DictionaryMain erstellen
        '              sCommand = "INSERT INTO DictionaryMain VALUES(" & index & ", '" & xlsBase.AddHighColons(sresult) & "', 'italian', 'std');"
    '              db.ExecuteNonQuery(sCommand)
    '            ElseIf result = MsgBoxResult.No Then
    '              GoTo a
    '            Else
    '              ' abbrechen, mit nächstem wort fortfahren
    '              Continue For
    '            End If
    '          End If
    '          ' Daten holen, Eintrag ist jetzt auf jeden Fall vorhanden, in index ist der Index
    '          sCommand = "SELECT Word, Pre,Post, WordType, Meaning1, Description, AdditionalTargetLangInfo,WordNumber FROM " & sTable & " WHERE Word='" & xlsBase.AddHighColons(cWords.Item(i)) & "' AND bla=0;"

    '          DBCursor = db.ExecuteReader(sCommand)
    '          If DBCursor.HasRows = True Then
    '            DBCursor.Read()
    '            sTemp = DBCursor.GetValue(0)
    '            sCommand = "INSERT INTO DictionaryWords VALUES(" & index & ", '" & xlsBase.AddHighColons(DBCursor.GetString(0)) & "', '" & xlsBase.AddHighColons(DBCursor.GetString(1)) & "', '" & xlsBase.AddHighColons(DBCursor.GetString(2)) & "', " & xlsBase.AddHighColons(DBCursor.GetInt32(3)) & ", '" & xlsBase.AddHighColons(DBCursor.GetString(4)) & "', '" & xlsBase.AddHighColons(DBCursor.GetString(5)) & "', '" & xlsBase.AddHighColons(DBCursor.GetString(6)) & "', " & False & ");"
    '            Dim wordnumber As Integer = DBCursor.GetInt32(7)
    '            DBCursor.Close()
    '            db.ExecuteNonQuery(sCommand)

    '            sCommand = "UPDATE " & sTable & " SET bla=" & True & " WHERE Word='" & xlsBase.AddHighColons(cWords.Item(i)) & "' AND WordNumber=" & wordnumber & ";"
    '            db.ExecuteNonQuery(sCommand)
    '          End If
    '        Else
    '          ' nichts machen
    '        End If
    '      End If
    '    Next i






    '    Exit Sub
    '    ' Hinzufügen von allen Vokabeln aus expresso 1 in DictionaryMain




    '    DBCursor = db.ExecuteReader(sCommand)
    '    Do While DBCursor.Read
    '      If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then sTemp = DBCursor.GetValue(0)
    '      Dim iresult As MsgBoxResult = MsgBox(" " & sTemp & " ", MsgBoxStyle.YesNo)
    '      If iresult = MsgBoxResult.Yes Then
    '        cAddWords.Add(sTemp)
    '      End If
    '    Loop
    '    DBCursor.Close()

    '    For i = 1 To cAddWords.Count
        '      db.ExecuteNonQuery("insert into DictionaryMain VALUES ('" & cAddWords.Item(i) & "' , 'italian', 'std')")
    '    Next



  End Sub

  Private Sub LoadTree()
    Dim tvRoot As TreeNode
    Dim tvNode As TreeNode
    Dim tvSubNode As TreeNode

    ' für jede vorhandene Sprache einen Wurzelknoten hinzufügen
    Dim cLanguages As Collection = voc.DictionaryLanguages()
    Dim i, j, k As Integer
    For i = 1 To cLanguages.Count
      tvRoot = Me.TreeView.Nodes.Add(cLanguages.Item(i))   ' Wurzel für jede Sprache erzeugen
      ' Für diese Sprache sämtliche vorhandenen vokabeln einfügen
      Dim cWords As Collection = voc.DictionaryEntrys(cLanguages.Item(i))
      For j = 1 To cWords.Count
        tvNode = tvRoot.Nodes.Add(cWords.Item(j))
        ' Für dieses Wort sämtliche Untereinträge eintragen
        Dim cSubWords As Collection = voc.DictionarySubEntrys(cLanguages.Item(i), cWords.Item(j))
        If cSubWords.Count = 0 Then Continue For
        For k = 1 To cSubWords.Count
          tvSubNode = tvNode.Nodes.Add(cSubWords.Item(k))
        Next k
      Next j
    Next i
  End Sub

  Private Sub TreeView_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles TreeView.AfterLabelEdit
    Dim tvSelectedNode As TreeNode = TreeView.SelectedNode
    Dim sOldName As String = tvSelectedNode.Text
    Dim sNewName As String = e.Label

    Select Case tvSelectedNode.Level
      Case NODE_LEVEL_ENTRY
        Dim iIndex As Integer = voc.GetEntryIndex(GetLanguageFromNode(tvSelectedNode), sOldName)
        Try
          ' Den Eintrag im Hauptverzeichnis ändern
          voc.ChangeEntry(iIndex, sNewName)
          ' Alle Einträge für das Wort im Unterverzeichnis ändern, falls es welche gibt
          Dim cIndices As Collection = voc.GetSubEntryIndices(iIndex, sOldName)
          voc.ChangeSubEntries(cIndices, sNewName)
        Catch ex As Exception
          MsgBox(ex.Message, MsgBoxStyle.Exclamation, """" & sOldName & """ konnte nicht umbenannt werden.")
          e.CancelEdit = True
        End Try
      Case NODE_LEVEL_SUBENTRY
        Dim iIndex As Integer = voc.GetEntryIndex(GetLanguageFromNode(tvSelectedNode), GetEntryFromNode(tvSelectedNode))
        Try
          Dim cIndices As Collection = voc.GetSubEntryIndices(voc.GetEntryIndex(GetLanguageFromNode(tvSelectedNode), GetEntryFromNode(tvSelectedNode)), sOldName)
          voc.ChangeSubEntries(cIndices, sNewName)
        Catch ex As Exception
          MsgBox(ex.Message, MsgBoxStyle.Exclamation, """" & sOldName & """ konnte nicht umbenannt werden.")
          e.CancelEdit = True
        End Try
    End Select
  End Sub

  Private Sub TreeView_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView.AfterSelect
    LoadListView()
  End Sub

  Private Sub SetUpListViewColumns(ByVal Type As ListViewStyle)
    ListView.Columns.Clear()
    Select Case Type
      Case ListViewStyle.WordEntry
        ListView.Columns.Add("Pre")
        ListView.Columns.Add("Wort")
        ListView.Columns.Add("Post")
        ListView.Columns.Add("Bedeutung")
        ListView.Columns.Add("Typ")
        ListView.Columns.Add("Erweiterte Info")
      Case ListViewStyle.WordSubEntry
        ListView.Columns.Add("Bedeutung")
        ListView.Columns.Add("Pre")
        ListView.Columns.Add("Post")
        ListView.Columns.Add("Typ")
        ListView.Columns.Add("Erweiterte Info")
      Case Else
    End Select
    SetView(View.Details)
  End Sub

  Private Sub LoadListView()
    ' Abhängig vom aktuell ausgewählten Wort die Bedeutungen anzeigen

    Dim tvSelectedNode As TreeNode = TreeView.SelectedNode
    Dim lvItem As ListViewItem
    ListView.Items.Clear()

    ' Sprache herausfinden
    Dim sLanguage As String = GetLanguageFromNode(tvSelectedNode)
    Dim sMainEntry As String = GetEntryFromNode(tvSelectedNode)
    Dim sSubEntry As String = GetSubEntryFromNode(tvSelectedNode)

    Dim cWords As Collection ' Finde die Ebene Raus
    Select Case tvSelectedNode.Level
      Case NODE_LEVEL_LANGUAGE
        ' Nichts zu tun, für die gesamte Sprache wird zur Zeit nichts angezeigt
        Exit Sub
      Case NODE_LEVEL_ENTRY
        ' Anzeigen der Bedeutungen für dieses Wort 
        Me.SetUpListViewColumns(ListViewStyle.WordEntry)
        ' Main-Entry berechnen
        cWords = voc.GetWords(sLanguage, sMainEntry, sMainEntry)
        ' Anzeigen aller Einträge aus der Collection
        For Each wCurrent As xlsDictionaryEntry In cWords
          lvItem = ListView.Items.Add(wCurrent.Pre)
          lvItem.SubItems.AddRange(New String() {wCurrent.Word, wCurrent.Post, wCurrent.Meaning, wCurrent.WordType, wCurrent.AdditionalTargetLangInfo})
        Next
        ' Anzeigen der SubEntrys zum gewählten Eintrag
        cWords = voc.GetSubWords(sLanguage, sMainEntry)
        ' Anzeigen aller Einträge aus der Collection
        For Each wCurrent As xlsDictionaryEntry In cWords
          lvItem = ListView.Items.Add(wCurrent.Pre)
          lvItem.SubItems.AddRange(New String() {wCurrent.Word, wCurrent.Post, wCurrent.Meaning, wCurrent.WordType, wCurrent.AdditionalTargetLangInfo})
        Next
      Case NODE_LEVEL_SUBENTRY
        ' Anzeigen aller Einträge die angegebenen Wort aus der Sprache entsprechen
        'Main-Entry berechnen
        SetUpListViewColumns(ListViewStyle.WordSubEntry)
        cWords = voc.GetWords(sLanguage, sMainEntry, sSubEntry)
        ' Anzeigen aller Einträge aus der Collection
        For Each wCurrent As xlsDictionaryEntry In cWords
          lvItem = ListView.Items.Add(wCurrent.Meaning)
          lvItem.SubItems.AddRange(New String() {wCurrent.Pre, wCurrent.Post, wCurrent.WordType, wCurrent.AdditionalTargetLangInfo})
        Next
    End Select
  End Sub

  Private Function GetLanguageFromNode(ByRef tvNode As TreeNode) As String
    ' Sprache herausfinden
    Return Mid(tvNode.FullPath, 1, InStr(tvNode.FullPath & "\", "\") - 1)
  End Function

  Private Function GetEntryFromNode(ByRef tvNode As TreeNode) As String
    Select Case tvNode.Level
      Case NODE_LEVEL_ENTRY
        Return Mid(tvNode.FullPath, InStrRev(tvNode.FullPath, "\") + 1)
      Case NODE_LEVEL_SUBENTRY
        Return Mid(tvNode.FullPath, InStr(tvNode.FullPath, "\") + 1, InStrRev(tvNode.FullPath, "\") - InStr(tvNode.FullPath, "\") - 1)
      Case Else
        Return ""
    End Select
  End Function

  Private Function GetSubEntryFromNode(ByRef tvNode As TreeNode) As String
    Return Mid(tvNode.FullPath, InStrRev(tvNode.FullPath, "\") + 1)
  End Function








  ' Stuff für die unsichtaben Sachen, evtl. später mal verwenden TODO
  Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
    'Anwendung beenden
    Global.System.Windows.Forms.Application.Exit()
  End Sub

  Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolBarToolStripMenuItem.Click
    'Sichtbarkeit des Toolstrips und aktivierten Zustand des zugehörigen Menüelements umschalten
    ToolBarToolStripMenuItem.Checked = Not ToolBarToolStripMenuItem.Checked
    ToolStrip.Visible = ToolBarToolStripMenuItem.Checked
  End Sub

  Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusBarToolStripMenuItem.Click
    'Sichtbarkeit des Statusstrips und aktivierten Zustand des zugehörigen Menüelements umschalten
    StatusBarToolStripMenuItem.Checked = Not StatusBarToolStripMenuItem.Checked
    StatusStrip.Visible = StatusBarToolStripMenuItem.Checked
  End Sub

  'Sichtbarkeit des Ordnerbereichs ändern
  Private Sub ToggleFoldersVisible()
    'Zuerst den aktivierten Zustand des zugehörigen Menüelements umschalten
    FoldersToolStripMenuItem.Checked = Not FoldersToolStripMenuItem.Checked

    'Symbolleistenschaltfläche "Ordner" für die Synchronisierung ändern
    FoldersToolStripButton.Checked = FoldersToolStripMenuItem.Checked

    ' Bereich mit TreeView reduzieren.
    Me.SplitContainer.Panel1Collapsed = Not FoldersToolStripMenuItem.Checked
  End Sub

  Private Sub FoldersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FoldersToolStripMenuItem.Click
    ToggleFoldersVisible()
  End Sub

  Private Sub FoldersToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FoldersToolStripButton.Click
    ToggleFoldersVisible()
  End Sub

  Private Sub SetView(ByVal View As System.Windows.Forms.View)
    'Bestimmen, welche Menüelemente aktiviert werden sollen
    Dim MenuItemToCheck As ToolStripMenuItem = Nothing
    Select Case View
      Case View.Details
        MenuItemToCheck = DetailsToolStripMenuItem
      Case View.LargeIcon
        MenuItemToCheck = LargeIconsToolStripMenuItem
      Case View.List
        MenuItemToCheck = ListToolStripMenuItem
      Case View.SmallIcon
        MenuItemToCheck = SmallIconsToolStripMenuItem
      Case View.Tile
        MenuItemToCheck = TileToolStripMenuItem
      Case Else
        Debug.Fail("Unerwartete Ansicht")
        View = View.Details
        MenuItemToCheck = DetailsToolStripMenuItem
    End Select

    'Entsprechendes Menüelement aktivieren und Auswahl aller anderen Elemente im Menü "Ansichten" aufheben
    For Each MenuItem As ToolStripMenuItem In ListViewToolStripButton.DropDownItems
      If MenuItem Is MenuItemToCheck Then
        MenuItem.Checked = True
      Else
        MenuItem.Checked = False
      End If
    Next

    'Abschließend die angeforderte Ansicht festlegen
    ListView.View = View
  End Sub

  Private Sub ListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListToolStripMenuItem.Click
    SetView(View.List)
  End Sub

  Private Sub DetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DetailsToolStripMenuItem.Click
    SetView(View.Details)
  End Sub

  Private Sub LargeIconsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LargeIconsToolStripMenuItem.Click
    SetView(View.LargeIcon)
  End Sub

  Private Sub SmallIconsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SmallIconsToolStripMenuItem.Click
    SetView(View.SmallIcon)
  End Sub

  Private Sub TileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TileToolStripMenuItem.Click
    SetView(View.Tile)
  End Sub

  Private Sub OpenToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
    Dim OpenFileDialog As New OpenFileDialog
    OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    OpenFileDialog.Filter = "Textdateien (*.txt)|*.txt"
    OpenFileDialog.ShowDialog(Me)

    Dim FileName As String = OpenFileDialog.FileName
    ' TODO: Code zum Öffnen der Datei hinzufügen
  End Sub

  Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click
    Dim SaveFileDialog As New SaveFileDialog
    SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    SaveFileDialog.Filter = "Textdateien (*.txt)|*.txt"
    SaveFileDialog.ShowDialog(Me)

    Dim FileName As String = SaveFileDialog.FileName
    ' TODO: Hier Code einfügen, um den aktuellen Inhalt des Formulars in einer Datei zu speichern.
  End Sub

  Private Sub TreeView_DrawNode(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawTreeNodeEventArgs) Handles TreeView.DrawNode
  End Sub

  Private Structure tmp
    Dim pre As String
    Dim post As String
    Dim word As String
    Dim meaning As String
    Dim unit As Integer
    Dim wordtype As Integer
  End Structure

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    'TreeView.Sort()
    ' kopieren der vokabeln in die DictionaryWords liste
    Dim command As String = "SELECT DISTINCT Table, Group FROM Tables WHERE Lang='italian';"
    Dim DBCursor As OleDbDataReader
    DBCursor = db.ExecuteReader(command)
    Dim cTables As New Collection
    Dim cGroups As New Collection
    Dim temp As String
    Do While DBCursor.Read  ' davon ausgehen, daß ein Eintrag gefunden worden ist
      temp = DBCursor.GetString(0)
      cTables.Add(temp)
      temp = DBCursor.GetString(1)
      cGroups.Add(temp)
    Loop
    DBCursor.Close()
    Dim i As Integer
    For i = 1 To cTables.Count
      ' table ist der name einer tabelle die italienische vokabeln enthält
      command = "SELECT Word, Pre, Post, Meaning1, UnitNumber, WordType FROM " & xlsBase.AddHighColons(cTables(i)) & ";"
      DBCursor = db.ExecuteReader(command)
      Dim words As New Collection
      Do While DBCursor.Read
        Dim actword As tmp
        actword.word = DBCursor.GetString(0)
        actword.pre = DBCursor.GetString(1)
        actword.post = DBCursor.GetString(2)
        actword.meaning = DBCursor.GetString(3)
        actword.unit = DBCursor.GetInt32(4)
        actword.wordtype = DBCursor.GetInt32(5)
        words.Add(actword)
      Loop
      DBCursor.Close()

      ' jedes wort in die main liste hinzufügen
      For Each word As tmp In words
        ' finde den mainindex heraus
        Dim entry As Integer
        If word.word = "" Then
          Continue For
        Else
          entry = voc.GetEntryIndex("italian", word.word)
        End If
        If word.meaning = "" Then Continue For


        Dim meaningarray As String() = word.meaning.Split(";")

        Dim j As Integer
        For j = 0 To meaningarray.Length - 1
          command = "INSERT INTO DictionaryWords (MainIndex, Word, Pre, Post, WordType, Meaning, TargetLanguageInfo, Deleted, Cards) VALUES"
          command += " (" & xlsBase.AddHighColons(entry) & ","
          command += " '" & xlsBase.AddHighColons(word.word) & "',"
          command += " '" & xlsBase.AddHighColons(word.pre) & "',"
          command += " '" & xlsBase.AddHighColons(word.post) & "',"
          command += " '" & xlsBase.AddHighColons(word.wordtype) & "',"
          command += " '" & xlsBase.AddHighColons(meaningarray(j)) & "',"
          command += " '',"
          command += " " & False & ", 1);"
          Try
            db.ExecuteNonQuery(command)
          Catch esbla As Exception
            ' wohl doppelt vorgekommen
          End Try

          ' versuche nun die vokabel die eben hinzugefügt worden ist, zu laden und ihren index in die vokabelliste hinzuzufügen
          ' bestimme tabelle in die eingefügt werden soll. zunächst den namen
          command = "SELECT Group FROM Tables WHERE Table='" & xlsBase.AddHighColons(cTables(i)) & "';"
          DBCursor = db.ExecuteReader(command)
          DBCursor.Read()
          Dim groupname As String = DBCursor.GetString(0)
          DBCursor.Close()
          groupname = groupname.Replace(" ", "")
          groupname = groupname.Replace("!", "")

          Dim subentry As Integer = voc.GetSubEntryIndex(entry, word.word, meaningarray(j))

          If word.unit < 10 Then
            command = "INSERT INTO Group" & groupname & "0" & word.unit & "(WordIndex) VALUES (" & subentry & ");"
          Else
            command = "INSERT INTO Group" & groupname & word.unit & "(WordIndex) VALUES (" & subentry & ");"
          End If
          db.ExecuteNonQuery(command)
        Next
      Next
    Next



    ' kopieren der vokabeln aus der kategorie 'latein', 'english', 'french', 'italian'
    'Dim command As String = "SELECT DISTINCT Table, Group FROM Tables WHERE Lang='italian';"
    'Dim DBCursor As OleDbDataReader
    'DBCursor = db.ExecuteReader(command)
    'Dim cTables As New Collection
    'Dim cGroups As New Collection
    'Dim temp As String
    'Do While DBCursor.Read  ' davon ausgehen, daß ein Eintrag gefunden worden ist
    '  temp = DBCursor.GetString(0)
    '  cTables.Add(temp)
    '  temp = DBCursor.GetString(1)
    '  cGroups.Add(temp)
    'Loop
    'DBCursor.Close()

    ''Dim i As Integer
    'For Each table As String In cTables
    '  ' table ist der name einer tabelle die italienische vokabeln enthält
    '  command = "SELECT Word FROM " & xlsBase.AddHighColons(table) & ";"
    '  DBCursor = db.ExecuteReader(command)
    '  Dim words As New Collection
    '  Do While DBCursor.Read
    '    words.Add(DBCursor.GetString(0))
    '  Loop
    '  DBCursor.Close()

    '  ' jedes wort in die main liste hinzufügen
    '  For Each word As String In words
        '    command = "INSERT INTO DictionaryMain (WordEntry, LanguageName, XLSType) VALUES ('" & xlsBase.AddHighColons(word) & "', 'italian', 'std');"
    '    Try
    '      db.ExecuteNonQuery(command)
    '    Catch esbla As Exception
    '      ' wohl doppelt vorgekommen
    '    End Try
    '  Next
    'Next

    ' alte Gruppen übernehmen
    ' der folgende code kopiert sämtliche gruppen aus der Tables tabelle in eine große liste, dazu werden
    ' die untergruppen aus den entsprechenden ...Units tabellen ausgelesen. anschließend wird zu jeder
    ' dieser untergruppen eine tabelle erstellt.
    '' erzeuge liste der lehrbücher
    'Dim command As String = "SELECT Group, Table FROM Tables;"
    'Dim DBCursor As OleDbDataReader
    'DBCursor = db.ExecuteReader(command)
    'Dim cGroups As New Collection
    'Dim cGroupsTables As New Collection
    'Dim temp As String
    'Do While DBCursor.Read  ' davon ausgehen, daß ein Eintrag gefunden worden ist
    '  temp = DBCursor.GetString(0)
    '  cGroups.Add(temp)
    '  temp = DBCursor.GetString(1)
    '  cGroupsTables.Add(temp)
    'Loop
    'DBCursor.Close()

    '' für jede gruop alle lektionen lesen und in die group tabelle einfügen
    'Dim i As Integer
    'For i = 1 To cGroups.Count
    '  Dim cSubGroups As New Collection
    '  command = "SELECT Name FROM " & cGroupsTables(i) & "Units ORDER BY Nummer"
    '  DBCursor = db.ExecuteReader(command)
    '  Do While DBCursor.Read
    '    temp = DBCursor.GetString(0)
    '    cSubGroups.Add(temp)
    '  Loop
    '  DBCursor.Close()

    '  ' füge die herausgefundenen einträge in die group tabelle hinzu
    '  Dim counter As Integer = 1
    '  ' lösche leerzeichen und andere unerwünschte zeichen
    '  Dim groupWithoutSpace As String = cGroups(i)
    '  groupWithoutSpace = groupWithoutSpace.Replace(" ", "")
    '  groupWithoutSpace = groupWithoutSpace.Replace("!", "")
    '  For Each subgroup As String In cSubGroups
    '    ' erstelle eintrag in der tabelle der gruppen
    '    If counter < 10 Then
    '      command = "INSERT INTO Groups (GroupName, GroupSubName, GroupTable) VALUES ('" & xlsBase.AddHighColons(cGroups(i)) & "' ,'" & xlsBase.AddHighColons(subgroup) & "', 'Group" & groupWithoutSpace & "0" & counter & "');"
    '    Else
    '      command = "INSERT INTO Groups (GroupName, GroupSubName, GroupTable) VALUES ('" & xlsBase.AddHighColons(cGroups(i)) & "' ,'" & xlsBase.AddHighColons(subgroup) & "', 'Group" & groupWithoutSpace & counter & "');"
    '    End If
    '    db.ExecuteNonQuery(command)

    '    ' erstelle die leeren tabellen für die vokabeln
    '    If counter < 10 Then
    '      command = "CREATE TABLE Group" & groupWithoutSpace & "0" & counter & " ([Index] AUTOINCREMENT, [WordIndex] LONG NOT NULL, CONSTRAINT bla PRIMARY KEY ([Index]) )"
    '    Else
    '      command = "CREATE TABLE Group" & groupWithoutSpace & counter & " ([Index] AUTOINCREMENT, [WordIndex] LONG NOT NULL, CONSTRAINT bla PRIMARY KEY ([Index]) )"
    '    End If
    '    db.ExecuteNonQuery(command)

    '    ' erhöhe counter für die tabellen
    '    counter += 1
    '  Next
    'Next i

  End Sub
End Class
