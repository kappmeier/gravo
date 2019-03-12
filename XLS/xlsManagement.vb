Imports System.Collections.ObjectModel
Imports Gravo.AccessDatabaseOperation

Public Class xlsManagement
    Inherits xlsBase

    Private versionHistory() As String = {"1.07", "1.06", "1.05", "1.04", "1.03", "1.02", "1.01", "1.00"}    ' Neuste vorne
    Private updateComplex() As Boolean = {False, True, False, True, False, False, False, False}
    Dim m_errorCount As Integer = 0

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByRef db As DataBaseOperation)
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
        Do Until found = True      'Or i = sVersionHistory.Length sp�testens bei 1.00 ist schlu�
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
        ' Datenbankformat f�r GraVo 2k7.
        Dim command As String
        Select Case DatabaseVersion()
            Case "1.00"      ' Startversion
                ' Einf�gen der zweiten Sprache in Main
                command = "ALTER TABLE [DictionaryMain] ADD COLUMN [MainLanguage] TEXT(50);"
                DBConnection.ExecuteNonQuery(command)
                ' Mit german vorbelegen
                command = "UPDATE [DictionaryMain] SET [MainLanguage]='german';"
                DBConnection.ExecuteNonQuery(command)
                ' L�schen des Index
                command = "DROP INDEX [DictionaryWord]"
                DBConnection.ExecuteNonQuery(command)
                ' Hinzuf�gen des Index
                command = "CREATE UNIQUE INDEX [DictionaryWord] ON [DictionaryMain] ([WordEntry], [LanguageName], [MainLanguage])"
                DBConnection.ExecuteNonQuery(command)
                command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('1.01', '2007-03-07', 'MainLanguage')"
            Case "1.01"       ' Hauptsprache Hinzugef�gt
                ' Erweitern der Tabelle
                command = "ALTER TABLE [DictionaryWords] ADD COLUMN [Irregular] BIT;"
                DBConnection.ExecuteNonQuery(command)
                command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('1.02', '2007-03-08', 'Irregular')"
            Case "1.02"
                ' Cards-Modus f�r abfrage Abfrage der Hauptsprache hinzugef�gt
                command = "ALTER TABLE [Cards] ADD COLUMN [TestIntervalMain] INT"
                DBConnection.ExecuteNonQuery(command)
                command = "ALTER TABLE [Cards] ADD COLUMN [CounterMain] INT"
                DBConnection.ExecuteNonQuery(command)
                command = "UPDATE [Cards] SET [TestIntervalMain] = 1, [CounterMain] = 1;"
                DBConnection.ExecuteNonQuery(command)
                command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('1.03', '2007-04-12', 'Cards test Main-Language Update')"
            Case "1.03"
                ' Erweitern der Tabelle um ein 'Marked' eintrag
                command = "ALTER TABLE [DictionaryWords] ADD COLUMN [Marked] BIT;"
                DBConnection.ExecuteNonQuery(command)
                command = "UPDATE DictionaryWords SET Marked='-1'"
                DBConnection.ExecuteNonQuery(command)
                command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('1.04', '2007-05-07', 'Important words marker')"
            Case "1.04"
                ' Fehlerkorrektur wegen Markierung
                Dim grp As New xlsGroups(Me.DBConnection)
                Dim groups As Collection(Of xlsGroupEntry) = grp.GetAllGroups()

                For Each group As xlsGroupEntry In groups
                    ' neue spalte hinzuf�gen
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

                'command = "ALTER TABLE [DictionaryWords] DROP COLUMN [Marked] BIT;"
                command = "CREATE TABLE [DictionaryWordsCopy] ([Index] INTEGER PRIMARY KEY AUTOINCREMENT, [MainIndex] INT NOT NULL, [Word] TEXT(80) NOT NULL, [Pre] TEXT(16), [Post] TEXT(16), [WordType] INT, [Meaning] TEXT(80) NOT NULL, TargetLanguageInfo TEXT(50), [Irregular] BIT)"
                DBConnection.ExecuteNonQuery(command)
                command = "INSERT INTO [DictionaryWordsCopy] ([MainIndex], [Word], [Pre], [Post], [WordType], [Meaning], [TargetLanguageInfo], [Irregular]) SELECT [MainIndex], [Word], [Pre], [Post], [WordType], [Meaning], [TargetLanguageInfo], '' FROM [DictionaryWords]"
                DBConnection.ExecuteNonQuery(command)
                command = "DROP TABLE [DictionaryWords]"
                DBConnection.ExecuteNonQuery(command)
                command = "ALTER TABLE [DictionaryWordsCopy] RENAME TO [DictionaryWords]"
                DBConnection.ExecuteNonQuery(command)

                ' Recreate the index
                command = "CREATE UNIQUE INDEX [Word] ON DictionaryWords ([Word], [Meaning], [MainIndex]);"
                DBConnection.ExecuteNonQuery(command)

                command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('1.05', '2007-05-07', 'Word-marker for groups')"
            Case "1.05"
                ' Feldl�nge aktualisieren
                'command = "ALTER TABLE [DictionaryMain] ALTER COLUMN [LanguageName] TEXT(16) NOT NULL;"
                'DBConnection.ExecuteNonQuery(command)
                'command = "ALTER TABLE [DictionaryMain] ALTER COLUMN [MainLanguage] TEXT(16) NOT NULL;"
                'DBConnection.ExecuteNonQuery(command)

                command = "CREATE TABLE [DictionaryMainCopy] ([Index] INTEGER PRIMARY KEY AUTOINCREMENT, [WordEntry] TEXT(50) NOT NULL, [LanguageName] TEXT(16) NOT NULL, [MainLanguage] TEXT(16))"
                DBConnection.ExecuteNonQuery(command)
                command = "INSERT INTO [DictionaryMainCopy] ([WordEntry], [LanguageName], [MainLanguage]) SELECT [WordEntry], [LanguageName], [MainLanguage] FROM [DictionaryMain]"
                DBConnection.ExecuteNonQuery(command)
                command = "DROP TABLE [DictionaryMain]"
                DBConnection.ExecuteNonQuery(command)
                command = "ALTER TABLE [DictionaryMainCopy] RENAME TO [DictionaryMain]"
                DBConnection.ExecuteNonQuery(command)
                ' Recreate the index
                command = "CREATE UNIQUE INDEX [DictionaryWord] ON [DictionaryMain] ([WordEntry], [LanguageName], [MainLanguage])"
                DBConnection.ExecuteNonQuery(command)


                command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('1.06', '2007-05-28', 'Correct max length for entrys')"
            Case "1.06"
                ' gro�es Update.
                ' aktualisiere Cards-Table zu not null
                ' f�ge Beispiel- und Cards-Felder ein f�r Gruppen
                ' f�ge unterst�tze Word-Types ein

                'command = "ALTER TABLE [Cards] ALTER COLUMN [TestIntervalMain] INT NOT NULL;"
                'DBConnection.ExecuteNonQuery(command)
                'command = "ALTER TABLE [Cards] ALTER COLUMN [CounterMain] INT NOT NULL;"
                'DBConnection.ExecuteNonQuery(command)


                command = "CREATE TABLE [CardsCopy] ([Index] INT NOT NULL, [TestInterval] INT NOT NULL, [Counter] INT NOT NULL, [LastDate] DATETIME NOT NULL, [TestIntervalMain] INT NOT NULL, [CounterMain] INT NOT NULL)"
                DBConnection.ExecuteNonQuery(command)
                command = "INSERT INTO [CardsCopy] ([Index], [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain]) SELECT [Index], [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain] FROM [Cards]"
                DBConnection.ExecuteNonQuery(command)
                command = "DROP TABLE [Cards]"
                DBConnection.ExecuteNonQuery(command)
                command = "ALTER TABLE [CardsCopy] RENAME TO [Cards]"
                DBConnection.ExecuteNonQuery(command)




                Dim grp As New xlsGroups(Me.DBConnection)
                Dim groups As Collection(Of xlsGroupEntry) = grp.GetAllGroups()
                For Each group As xlsGroupEntry In groups
                    ' neue spalte hinzuf�gen
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

                ' Sorge daf�r, da� alle Eintr�ge die nicht 0 bis 6 sind, zu 3 werden
                command = "UPDATE [DictionaryWords] SET [WordType] = '3' WHERE [WordType] Not In (0,1,2,3,4,5,6);"
                DBConnection.ExecuteNonQuery(command)

                ' Erzeuge Referenz
                ' instead of ON UPDATE CASCADE we probably need ON DELETE REJECT
                command = "PRAGMA foreign_keys = ON"
                DBConnection.ExecuteNonQuery(command)

                'command = "ALTER TABLE  ADD CONSTRAINT TestConstraint FOREIGN KEY ([WordType]) REFERENCES SupportedWordTypes ON UPDATE CASCADE;"
                'DBConnection.ExecuteNonQuery(command)

                ' What happens with the index?
                ' DROP index
                command = "CREATE TABLE [DictionaryWordsCopy] ([Index] INTEGER PRIMARY KEY AUTOINCREMENT, [MainIndex] INT NOT NULL, [Word] TEXT(80) NOT NULL, [Pre] TEXT(16), [Post] TEXT(16), [WordType] INT, [Meaning] TEXT(80) NOT NULL, TargetLanguageInfo TEXT(50), [Irregular] BIT, FOREIGN KEY ([WordType]) REFERENCES SupportedWordTypes ON DELETE RESTRICT)"
                DBConnection.ExecuteNonQuery(command)
                command = "INSERT INTO [DictionaryWordsCopy] ([MainIndex], [Word], [Pre], [Post], [WordType], [Meaning], [TargetLanguageInfo], [Irregular]) SELECT [MainIndex], [Word], [Pre], [Post], [WordType], [Meaning], [TargetLanguageInfo], 'False' FROM [DictionaryWords]"
                DBConnection.ExecuteNonQuery(command)
                command = "DROP TABLE [DictionaryWords]"
                DBConnection.ExecuteNonQuery(command)
                command = "ALTER TABLE [DictionaryWordsCopy] RENAME TO [DictionaryWords]"
                DBConnection.ExecuteNonQuery(command)

                ' Recreate the Index
                command = "CREATE UNIQUE INDEX [Word] ON DictionaryWords ([Word], [Meaning], [MainIndex]);"
                DBConnection.ExecuteNonQuery(command)

                command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES('1.07', '2007-05-29', 'Group features update and WordTypes')"
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
        ' Datenbank auf Fehler �berpr�fen, die automatisch behoben werden k�nnen, ohne Datenverlust zu verursachen
        ErrorCount = 0

        ' l�scht Cards-Eintr�ge zu W�rtern, die nicht mehr vorhanden sind
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

        ' f�gt f�r alle Indizes aus dictionary words einen Card-Index hinzu
        command = "SELECT Index FROM DictionaryWords ORDER BY Index"
        DBConnection.ExecuteReader(command)
        indices = New Collection(Of Integer)
        Do While DBConnection.DBCursor.Read
            indices.Add(DBConnection.SecureGetInt32(0))
        Loop
        DBConnection.DBCursor.Close()
        For Each index As Integer In indices
            command = "SELECT TestInterval FROM Cards WHERE Index =" & index
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

        ' Suche in Gruppen nach Eintr�gen zu W�rtern die nicht existieren
        Dim groups As New xlsGroups(DBConnection)
        For Each group As xlsGroupEntry In groups.GetAllGroups()
            Dim grp As New xlsGroup(group.Table)
            grp.DBConnection = DBConnection
            For Each index As Integer In grp.GetIndices()
                command = "SELECT MainIndex FROM DictionaryWords WHERE [Index]=" & index & ";"
                DBConnection.ExecuteReader(command)
                If DBConnection.DBCursor.HasRows = False Then
                    ' l�schen, da eintrag nicht existiert
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
        Dim db As New SQLiteDataBaseOperation()
        db.Open(filename)
        Dim command As String

        ' Erstelle die [DBVersion] Tabelle
        command = "CREATE TABLE [DBVersion] ([Version] TEXT(5) NOT NULL, [Date] DATETIME NOT NULL, [Description] TEXT(80) NOT NULL);"
        db.ExecuteNonQuery(command)

        ' F�ge Startversion ein
        Dim dte As Date = New Date(2007, 2, 27)
        'command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES ('1.00', '" & dte & "', 'VokTrain 2k7 DB-Version');"
        command = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES ('1.00', '2007-02-27', 'VokTrain 2k7 DB-Version');"
        db.ExecuteNonQuery(command)

        ' Erstelle die Cards Tabelle
        command = "CREATE TABLE [Cards] ([Index] INT NOT NULL, [TestInterval] INT NOT NULL, [Counter] INT NOT NULL, [LastDate] DATETIME NOT NULL)"
        db.ExecuteNonQuery(command)

        ' Erstelle die DictionaryMain Tabelle
        command = "CREATE TABLE [DictionaryMain] ([Index] INTEGER PRIMARY KEY AUTOINCREMENT, [WordEntry] TEXT(50) NOT NULL, [LanguageName] TEXT(50) NOT NULL)"
        db.ExecuteNonQuery(command)

        ' Hinzuf�gen des Index
        command = "CREATE UNIQUE INDEX [DictionaryWord] ON DictionaryMain ([WordEntry], [LanguageName])"
        db.ExecuteNonQuery(command)

        ' Erstelle die DictionaryWords Tabelle
        command = "CREATE TABLE [DictionaryWords] ([Index] INTEGER PRIMARY KEY AUTOINCREMENT, [MainIndex] INT NOT NULL, [Word] TEXT(80) NOT NULL, [Pre] TEXT(16), [Post] TEXT(16), [WordType] INT, [Meaning] TEXT(80) NOT NULL, TargetLanguageInfo TEXT(50))"
        db.ExecuteNonQuery(command)

        '' Erstelle Primary Key
        'command = "CREATE INDEX [PrimaryKey] ON DictionaryWords ([Index]);"
        'db.ExecuteNonQuery(command)

        ' Hinzuf�gen des Index
        command = "CREATE UNIQUE INDEX [Word] ON DictionaryWords ([Word], [Meaning], [MainIndex]);"
        db.ExecuteNonQuery(command)

        ' Erstelle die Groups Tabelle
        command = "CREATE TABLE Groups ([Index] INTEGER PRIMARY KEY AUTOINCREMENT, [GroupName] TEXT(50) NOT NULL, [GroupSubName] TEXT(50) NOT NULL, [GroupTable] TEXT(50) NOT NULL)"
        db.ExecuteNonQuery(command)

        '' Erstelle Primary Key
        'command = "CREATE INDEX [PrimaryKey] ON Groups ([Index]);"
        'db.ExecuteNonQuery(command)

        ' Hinzuf�gen des Index
        command = "CREATE UNIQUE INDEX [Group] ON Groups ([GroupName], [GroupSubName])"
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
            DBConnection.ExecuteReader(command)
            Dim indices As New Collection(Of Integer)
            While DBConnection.DBCursor.Read()
                Dim index = DBConnection.SecureGetInt32(0)
                ' speichere den Index in ein Array
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
    End Sub
End Class
