Imports System.Collections.ObjectModel
Imports Gravo2k7.AccessDatabaseOperation

Public Class xlsManagement
  Inherits xlsBase

  Private versionHistory() As String = {"1.07", "1.06", "1.05", "1.04", "1.03", "1.02", "1.01", "1.00"}    ' Neuste vorne
  Private updateComplex() As Boolean = {False, True, False, True, False, False, False, False}
  Dim m_errorCount As Integer = 0

  Public Sub New()
    MyBase.New()
  End Sub

  Public Sub New(ByRef db As AccessDatabaseOperation)
    MyBase.New(db)
  End Sub

  Function DatabaseVersion() As String
    If IsConnected() = False Then Throw New Exception("Database not connected.")
    Return versionHistory(DatabaseVersionIndex)
  End Function

  Function DatabaseVersion(ByVal Index As Integer) As String
    Return versionHistory(Index)
  End Function

  Function DatabaseVersionNeeded() As String
    Return versionHistory(0)
  End Function

  Function DatabaseVersionIndex() As Integer
    If IsConnected() = False Then Exit Function ' TODO exception werfen
    Dim found As Boolean = False
    Dim i As Integer = 0
    Do Until found = True      'Or i = sVersionHistory.Length spätestens bei 1.00 ist schluß
      Dim command As String = "SELECT Version FROM [DBVersion] WHERE Version='" & versionHistory(i) & "'"
      DBConnection.ExecuteReader(command)
      DBConnection.DBCursor.Read()
      Try
        If DBConnection.DBCursor.GetValue(0) Then found = True Else i += 1
      Catch e As Exception
        i += 1
      End Try
    Loop
    DBConnection.DBCursor.Close()
    Return i
  End Function

  Public Function NextVersionIndex() As Integer
    If IsConnected() = False Then Exit Function ' TODO exception werfen
    If DatabaseVersionIndex() = 0 Then
      Return 0
    Else
      Return DatabaseVersionIndex() - 1
    End If
  End Function

  Public Function IsVersionUpToDate() As Boolean
    Return Not DatabaseVersionIndex() <> 0
  End Function

  Public Function IsUpdateComplex() As Boolean
    Return updateComplex(DatabaseVersionIndex)
  End Function

  Public Sub UpdateDatabaseVersion()
    ' Datenbankformat für GraVo 2k7.
    Dim command As String
    Select Case DatabaseVersion()
      Case "1.00"      ' Startversion
        ' Einfügen der zweiten Sprache in Main
        command = "ALTER TABLE [DictionaryMain] ADD COLUMN [MainLanguage] TEXT(50);"
        DBConnection.ExecuteNonQuery(command)
        ' Mit german vorbelegen
        command = "UPDATE [DictionaryMain] SET [MainLanguage]='german';"
        DBConnection.ExecuteNonQuery(command)
        ' Löschen des Index
        command = "DROP INDEX [Word] ON [DictionaryMain];"
        DBConnection.ExecuteNonQuery(command)
        ' Hinzufügen des Index
        command = "CREATE UNIQUE INDEX [Word] ON [DictionaryMain] ([WordEntry], [LanguageName], [MainLanguage]) WITH DISALLOW NULL;"
        DBConnection.ExecuteNonQuery(command)
        command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('1.01', '07.03.2007', 'MainLanguage')"
      Case "1.01"       ' Hauptsprache Hinzugefügt
        ' Erweitern der Tabelle
        command = "ALTER TABLE [DictionaryWords] ADD COLUMN [Irregular] BIT;"
        DBConnection.ExecuteNonQuery(command)
        command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('1.02', '08.03.2007', 'Irregular')"
      Case "1.02"
        ' Cards-Modus für abfrage Abfrage der Hauptsprache hinzugefügt
        command = "ALTER TABLE [Cards] ADD COLUMN [TestIntervalMain] INT, [CounterMain] INT;"
        DBConnection.ExecuteNonQuery(command)
        command = "UPDATE [Cards] SET [TestIntervalMain] = 1, [CounterMain] = 1;"
        DBConnection.ExecuteNonQuery(command)
        command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('1.03', '22.04.2007', 'Cards test Main-Language Update')"
      Case "1.03"
        ' Erweitern der Tabelle um ein 'Marked' eintrag
        command = "ALTER TABLE [DictionaryWords] ADD COLUMN [Marked] BIT;"
        DBConnection.ExecuteNonQuery(command)
        command = "UPDATE DictionaryWords SET Marked='-1'"
        DBConnection.ExecuteNonQuery(command)
        command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('1.04', '07.05.2007', 'Important words marker')"
      Case "1.04"
        ' Fehlerkorrektur wegen Markierung
        Dim grp As New xlsGroups(Me.DBConnection)
        Dim groups As Collection(Of xlsGroupEntry) = grp.GetAllGroups()

        For Each group As xlsGroupEntry In groups
          ' neue spalte hinzufügen
          command = "ALTER TABLE [" & group.Table & "] ADD COLUMN [Marked] BIT;"
          DBConnection.ExecuteNonQuery(command)

          ' weitermachen
          command = "SELECT [WordIndex] FROM [" & group.Table & "];"
          DBConnection.ExecuteReader(command)
          Dim indices As New Collection(Of Integer)
          While DBConnection.DBCursor.Read()
            Dim index = DBConnection.SecureGetInt32(0)
            ' speichere den index in ein array 
            indices.Add(index)
          End While

          Dim dict As New xlsDictionary(Me.DBConnection)
          For Each index As Integer In indices
            command = "SELECT Marked FROM [DictionaryWords] WHERE [Index]=" & index & ";"
            DBConnection.ExecuteReader(command)
            DBConnection.DBCursor.Read()
            Dim marked As Boolean = DBConnection.SecureGetBool(0)
            DBConnection.DBCursor.Close()

            'speichern
            command = "UPDATE [" & group.Table & "] SET [Marked] = " & GetDBEntry(marked) & " WHERE [WordIndex]=" & index & ";"
            DBConnection.ExecuteNonQuery(command)
          Next
        Next group

        command = "ALTER TABLE [DictionaryWords] DROP COLUMN [Marked] BIT;"
        DBConnection.ExecuteNonQuery(command)

        command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('1.05', '07.05.2007', 'Word-marker for groups')"
      Case "1.05"
        ' Feldlänge aktualisieren
        command = "ALTER TABLE [DictionaryMain] ALTER COLUMN [LanguageName] TEXT(16) NOT NULL;"
        DBConnection.ExecuteNonQuery(command)
        command = "ALTER TABLE [DictionaryMain] ALTER COLUMN [MainLanguage] TEXT(16) NOT NULL;"
        DBConnection.ExecuteNonQuery(command)

        command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('1.06', '28.05.2007', 'Correct max length for entrys')"
      Case "1.06"
        ' großes Update.
        ' aktualisiere Cards-Table zu not null
        ' füge Beispiel- und Cards-Felder ein für Gruppen
        ' füge unterstütze Word-Types ein

        command = "ALTER TABLE [Cards] ALTER COLUMN [TestIntervalMain] INT NOT NULL;"
        DBConnection.ExecuteNonQuery(command)
        command = "ALTER TABLE [Cards] ALTER COLUMN [CounterMain] INT NOT NULL;"
        DBConnection.ExecuteNonQuery(command)

        Dim grp As New xlsGroups(Me.DBConnection)
        Dim groups As Collection(Of xlsGroupEntry) = grp.GetAllGroups()
        For Each group As xlsGroupEntry In groups
          ' neue spalte hinzufügen
          command = "ALTER TABLE [" & group.Table & "] ADD COLUMN [Example] TEXT(64), [TestInterval] INT NOT NULL, [Counter] INT NOT NULL, [LastDate] DATETIME NOT NULL, [TestIntervalMain] INT NOT NULL, [CounterMain] INT NOT NULL);"
          DBConnection.ExecuteNonQuery(command)

          ' weitermachen
          command = "SELECT [WordIndex] FROM [" & group.Table & "];"
          DBConnection.ExecuteReader(command)
          Dim indices As New Collection(Of Integer)
          While DBConnection.DBCursor.Read()
            Dim index = DBConnection.SecureGetInt32(0)
            ' speichere den Index in ein array 
            indices.Add(index)
          End While

          Dim dict As New xlsDictionary(Me.DBConnection)
          For Each index As Integer In indices
            command = "SELECT [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain] FROM [Cards] WHERE [Index]=" & index & ";"
            DBConnection.ExecuteReader(command)
            DBConnection.DBCursor.Read()
            Dim testInterval As Integer = DBConnection.SecureGetInt32(0)
            Dim counter As Integer = DBConnection.SecureGetInt32(1)
            Dim lastDateTemp As System.DateTime = DBConnection.SecureGetDateTime(2)
            Dim lastDate As String = lastDateTemp.Day & "." & lastDateTemp.Month & "." & lastDateTemp.Year
            Dim testIntervalMain As Integer = DBConnection.SecureGetInt32(3)
            Dim counterMain As Integer = DBConnection.SecureGetInt32(4)
            DBConnection.DBCursor.Close()

            'speichern
            command = "UPDATE [" & group.Table & "] SET [TestInterval] = " & GetDBEntry(testInterval) & ", [Counter] = " & GetDBEntry(counter) & ",[LastDate] = " & GetDBEntry(lastDate) & ",[TestIntervalMain] = " & GetDBEntry(testIntervalMain) & ",[CounterMain] = " & GetDBEntry(counterMain) & " WHERE [WordIndex]=" & index & ";"
            DBConnection.ExecuteNonQuery(command)
          Next index
        Next group

        ' Erstelle die SupportedWordTypes-Tabelle
        command = "CREATE TABLE [SupportedWordTypes] ([Type] TEXT(32) NOT NULL, [Index] INT PRIMARY KEY);"
        DBConnection.ExecuteNonQuery(command)
        command = "INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES('WORD_TYPE_SUBSTANTIVE', '0');"
        DBConnection.ExecuteNonQuery(command)
        command = "INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES('WORD_TYPE_VERB', '1');"
        DBConnection.ExecuteNonQuery(command)
        command = "INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES('WORD_TYPE_ADJECTIVE', '2');"
        DBConnection.ExecuteNonQuery(command)
        command = "INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES('WORD_TYPE_SIMPLE', '3');"
        DBConnection.ExecuteNonQuery(command)
        command = "INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES('WORD_TYPE_ADVERB', '4');"
        DBConnection.ExecuteNonQuery(command)
        command = "INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES('WORD_TYPE_SET_PHRASE', '5');"
        DBConnection.ExecuteNonQuery(command)
        command = "INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES('WORD_TYPE_EXAMPLE', '6');"
        DBConnection.ExecuteNonQuery(command)

        ' Sorge dafür, daß alle Einträge die nicht 0 bis 6 sind, zu 3 werden
        command = "UPDATE [DictionaryWords] SET [WordType] = '3' WHERE [WordType] Not In (0,1,2,3,4,5,6);"
        DBConnection.ExecuteNonQuery(command)

        ' Erzeuge Referenz
        command = "ALTER TABLE DictionaryWords ADD CONSTRAINT TestConstraint FOREIGN KEY ([WordType]) REFERENCES SupportedWordTypes ON UPDATE CASCADE;"
        DBConnection.ExecuteNonQuery(command)

        command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('1.07', '29.05.2007', 'Group features update and WordTypes')"
      Case "1.07"
        ' aktuelle Version
        command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('', '', '')"
        ' Case "1.08"
        ' aktuelle Version
        ' command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('', '', '')"
      Case Else
        'command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('', '', '')"
        Throw New Exception("Database broken!")
        Exit Sub
    End Select
    DBConnection.ExecuteNonQuery(command)
  End Sub

  Sub Reorganize()
    ' Datenbank auf Fehler überprüfen, die automatisch behoben werden können, ohne Datenverlust zu verursachen
    ErrorCount = 0

    ' löscht Cards-Einträge zu Wörtern, die nicht mehr vorhanden sind
    Dim command As String = "SELECT Index FROM Cards ORDER BY Index"
    DBConnection.ExecuteReader(command)
    Dim indices As New Collection(Of Integer)
    Do While DBConnection.DBCursor.Read()
      indices.Add(DBConnection.SecureGetInt32(0))
    Loop
    DBConnection.DBCursor.Close()
    For Each index As Integer In indices
      command = "SELECT Word FROM DictionaryWords WHERE Index=" & index & ";"
      DBConnection.ExecuteReader(command)
      If Not DBConnection.DBCursor.HasRows Then
        command = "DELETE FROM Cards WHERE Index=" & index & ";"
        DBConnection.DBCursor.Close()
        DBConnection.ExecuteNonQuery(command)
        ErrorCount += 1
      Else
        DBConnection.DBCursor.Close()
      End If
    Next index

    ' fügt für alle Indizes aus dictionary words einen Card-Index hinzu
    command = "SELECT Index FROM DictionaryWords ORDER BY Index"
    DBConnection.ExecuteReader(command)
    indices = New Collection(Of Integer)
    Do While DBConnection.DBCursor.Read
      indices.Add(DBConnection.SecureGetInt32(0))
    Loop
    DBConnection.DBCursor.Close()
    For Each index As Integer In indices
      command = "SELECT TestInterval FROM Cards WHERE Index =" & index & ";"
      DBConnection.ExecuteReader(command)
      If Not DBConnection.DBCursor.HasRows Then
        DBConnection.DBCursor.Close()
        'command = "INSERT INTO Cards ([Index], [], [], [], []) VALUES(" & index & ");"
        'command = "INSERT INTO Cards ([Index], [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain])VALUES (" & index & ", 1, 1, '01.01.1900', 1, 1);"
        Dim cards As New xlsCards(DBConnection)
        cards.AddNewEntry(index)
        'DBConnection.ExecuteNonQuery(command)
        ErrorCount += 1
      Else
        DBConnection.DBCursor.Close()
      End If
    Next index

    ' Suche in Gruppen nach Einträgen zu Wörtern die nicht existieren
    Dim groups As New xlsGroups(DBConnection)
    For Each group As xlsGroupEntry In groups.GetAllGroups()
      Dim grp As New xlsGroup(group.Table)
      grp.DBConnection = DBConnection
      For Each index As Integer In grp.GetIndices()
        command = "SELECT MainIndex FROM DictionaryWords WHERE [Index]=" & index & ";"
        DBConnection.ExecuteReader(command)
        If DBConnection.DBCursor.HasRows = False Then
          ' löschen, da eintrag nicht existiert
          DBConnection.CloseReader()
          grp.Delete(index)
          ErrorCount += 1
        Else
          DBConnection.CloseReader()
        End If
      Next index
    Next group
  End Sub

  Public Property ErrorCount() As Integer
    Get
      Return m_errorCount
    End Get
    Set(ByVal value As Integer)
      m_errorCount = value
    End Set
  End Property

  Public Sub CreateNewVocabularyDatabase(ByVal filename As String)
    Dim db As New AccessDatabaseOperation(filename)
    Dim command As String

    ' Erstelle die [DBVersion] Tabelle
    command = "CREATE TABLE [DBVersion] ([Version] TEXT(5) NOT NULL, [Date] DATETIME NOT NULL, [Description] TEXT(80) NOT NULL);"
    db.ExecuteNonQuery(command)

    ' Füge Startversion ein
    command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES ('1.00', '27.02.2007', 'VokTrain 2k7 DB-Version');"
    db.ExecuteNonQuery(command)

    ' Erstelle die Cards Tabelle
    command = "CREATE TABLE [Cards] ([Index] INT NOT NULL, [TestInterval] INT NOT NULL, [Counter] INT NOT NULL, [LastDate] DATETIME NOT NULL)"
    db.ExecuteNonQuery(command)

    ' Erstelle die DictionaryMain Tabelle
    command = "CREATE TABLE [DictionaryMain] ([Index] AUTOINCREMENT, [WordEntry] TEXT(50) NOT NULL, [LanguageName] TEXT(50) NOT NULL);"
    db.ExecuteNonQuery(command)

    ' Hinzufügen des Index
    command = "CREATE UNIQUE INDEX [Word] ON DictionaryMain ([WordEntry], [LanguageName]) WITH DISALLOW NULL;"
    db.ExecuteNonQuery(command)

    ' Erstelle die DictionaryWords Tabelle
    command = "CREATE TABLE [DictionaryWords] ([Index] AUTOINCREMENT, [MainIndex] INT NOT NULL, [Word] TEXT(80) NOT NULL, [Pre] TEXT(16), [Post] TEXT(16), [WordType] INT, [Meaning] TEXT(80) NOT NULL, TargetLanguageInfo TEXT(50));"
    db.ExecuteNonQuery(command)

    ' Erstelle Primary Key
    command = "CREATE INDEX [PrimaryKey] ON DictionaryWords ([Index]) WITH PRIMARY;"
    db.ExecuteNonQuery(command)

    ' Hinzufügen des Index
    command = "CREATE UNIQUE INDEX [Word] ON DictionaryWords ([Word], [Meaning], [MainIndex]) WITH DISALLOW NULL;"
    db.ExecuteNonQuery(command)

    ' Erstelle die Groups Tabelle
    command = "CREATE TABLE Groups ([Index] AUTOINCREMENT, [GroupName] TEXT(50) NOT NULL, [GroupSubName] TEXT(50) NOT NULL, [GroupTable] TEXT(50) NOT NULL);"
    db.ExecuteNonQuery(command)

    ' Erstelle Primary Key
    command = "CREATE INDEX [PrimaryKey] ON Groups ([Index]) WITH PRIMARY;"
    db.ExecuteNonQuery(command)

    ' Hinzufügen des Index
    command = "CREATE UNIQUE INDEX [Group] ON Groups ([GroupName], [GroupSubName]) WITH DISALLOW NULL;"
    db.ExecuteNonQuery(command)

    ' Datenbank auf aktuelle Version bringen
    Dim man As New xlsManagement()
    man.DBConnection = db

    While man.DatabaseVersionIndex() <> 0
      man.UpdateDatabaseVersion()
    End While

    db.Close()
  End Sub

  Public Sub CopyGobalCardsToGroups()
    Dim command As String
    Dim grp As New xlsGroups(Me.DBConnection)
    Dim groups As Collection(Of xlsGroupEntry) = grp.GetAllGroups()
    For Each group As xlsGroupEntry In groups
      command = "SELECT [WordIndex] FROM [" & group.Table & "];"
      DBConnection.ExecuteReader(Command)
      Dim indices As New Collection(Of Integer)
      While DBConnection.DBCursor.Read()
        Dim index = DBConnection.SecureGetInt32(0)
        ' speichere den Index in ein Array
        indices.Add(index)
      End While

      Dim dict As New xlsDictionary(Me.DBConnection)
      For Each index As Integer In indices
        command = "SELECT [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain] FROM [Cards] WHERE [Index]=" & index & ";"
        DBConnection.ExecuteReader(Command)
        DBConnection.DBCursor.Read()
        Dim testInterval As Integer = DBConnection.SecureGetInt32(0)
        Dim counter As Integer = DBConnection.SecureGetInt32(1)
        Dim lastDateTemp As System.DateTime = DBConnection.SecureGetDateTime(2)
        Dim lastDate As String = lastDateTemp.Day & "." & lastDateTemp.Month & "." & lastDateTemp.Year
        Dim testIntervalMain As Integer = DBConnection.SecureGetInt32(3)
        Dim counterMain As Integer = DBConnection.SecureGetInt32(4)
        DBConnection.DBCursor.Close()

        'speichern
        command = "UPDATE [" & group.Table & "] SET [TestInterval] = " & GetDBEntry(testInterval) & ", [Counter] = " & GetDBEntry(counter) & ",[LastDate] = " & GetDBEntry(lastDate) & ",[TestIntervalMain] = " & GetDBEntry(testIntervalMain) & ",[CounterMain] = " & GetDBEntry(counterMain) & " WHERE [WordIndex]=" & index & ";"
        DBConnection.ExecuteNonQuery(Command)
      Next index
    Next group
  End Sub
End Class
