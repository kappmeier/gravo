Imports Gravo.Properties

Public Class ManagementDao
    Implements IManagementDao

    Private ReadOnly DBConnection As IDataBaseOperation

    Public Shared ReadOnly DB_VERSION_1_0 As DBVersion = New DBVersion(1, 0, New Date(2007, 2, 27), "VokTrain 2k7 DB-Version")
    Public Shared ReadOnly DB_VERSION_1_1 As DBVersion = New DBVersion(1, 1, New Date(2007, 3, 7), "MainLanguage")
    Public Shared ReadOnly DB_VERSION_1_2 As DBVersion = New DBVersion(1, 2, New Date(2007, 3, 8), "Irregular")
    Public Shared ReadOnly DB_VERSION_1_3 As DBVersion = New DBVersion(1, 3, New Date(2007, 4, 12), "Cards test Main-Language Update")
    Public Shared ReadOnly DB_VERSION_1_4 As DBVersion = New DBVersion(1, 4, New Date(2007, 5, 7), "Important words marker")
    Public Shared ReadOnly DB_VERSION_1_5 As DBVersion = New DBVersion(1, 5, New Date(2007, 5, 7), "Word-marker for groups")
    Public Shared ReadOnly DB_VERSION_1_6 As DBVersion = New DBVersion(1, 6, New Date(2007, 5, 28), "Correct max length for entries")
    Public Shared ReadOnly DB_VERSION_1_7 As DBVersion = New DBVersion(1, 7, New Date(2007, 5, 29), "Group features update and WordTypes")

    Public ReadOnly ComplexVersionUpdates As HashSet(Of DBVersion) = New HashSet(Of DBVersion) From {DB_VERSION_1_6, DB_VERSION_1_4}

    Public ReadOnly supportedVersions() As DBVersion = {
        DB_VERSION_1_0,
        DB_VERSION_1_1,
        DB_VERSION_1_2,
        DB_VERSION_1_3,
        DB_VERSION_1_4,
        DB_VERSION_1_5,
        DB_VERSION_1_6,
        DB_VERSION_1_7
     }

    Private updateFunctions As New Dictionary(Of DBVersion, Action) From {
        {DB_VERSION_1_0, Sub() UpdateTo1_1()},
        {DB_VERSION_1_1, Sub() UpdateTo1_2()},
        {DB_VERSION_1_2, Sub() UpdateTo1_3()},
        {DB_VERSION_1_3, Sub() UpdateTo1_4()},
        {DB_VERSION_1_4, Sub() UpdateTo1_5()},
        {DB_VERSION_1_5, Sub() UpdateTo1_6()},
        {DB_VERSION_1_6, Sub() UpdateTo1_7()}
    }

    Sub New(ByRef db As IDataBaseOperation)
        DBConnection = db
    End Sub

    Public Sub Initialize() Implements IManagementDao.Initialize
        Dim dbEmpty As Boolean = DBConnection.IsEmpty()
        If Not dbEmpty Then Throw New IllegalVersionException("Database not empty, cannot be initialized")

        InitializeVersionTable()
        InitializeCardsTable()
        InitializeMainEntryTable()
        InitializeWordTable()
        InitializeGroupsTable()
    End Sub

    ''' <summary>
    ''' Creates DBVersion table and insert start version information.
    ''' </summary>
    Private Sub InitializeVersionTable()
        SimpleUpdateCommand("CREATE TABLE [DBVersion] ([Version] TEXT(5) Not NULL, [Date] DATETIME Not NULL, [Description] TEXT(80) Not NULL)")

        UpdateToVersion(DB_VERSION_1_0)
    End Sub

    ''' <summary>
    ''' Initializes the cards table.
    ''' </summary>
    Private Sub InitializeCardsTable()
        SimpleUpdateCommand("CREATE TABLE [Cards] ([Index] INT NOT NULL, [TestInterval] INT NOT NULL, [Counter] INT NOT NULL, [LastDate] DATETIME NOT NULL)")
    End Sub

    ''' <summary>
    ''' Creates (indexed) table for main entries.
    ''' </summary>
    Private Sub InitializeMainEntryTable()
        SimpleUpdateCommand("CREATE TABLE [DictionaryMain] ([Index] INTEGER PRIMARY KEY AUTOINCREMENT, [WordEntry] TEXT(50) NOT NULL, [LanguageName] TEXT(50) NOT NULL)")

        SimpleUpdateCommand("CREATE UNIQUE INDEX [DictionaryWord] ON DictionaryMain ([WordEntry], [LanguageName])")
    End Sub

    ''' <summary>
    ''' Creates the (indexed) table for word entries.
    ''' </summary>
    Private Sub InitializeWordTable()
        SimpleUpdateCommand("CREATE TABLE [DictionaryWords] ([Index] INTEGER PRIMARY KEY AUTOINCREMENT, [MainIndex] INT NOT NULL, [Word] TEXT(80) NOT NULL, [Pre] TEXT(16), [Post] TEXT(16), [WordType] INT, [Meaning] TEXT(80) NOT NULL, TargetLanguageInfo TEXT(50))")

        ' Create Primary Key, not possible in sqlite
        'command = "CREATE INDEX [PrimaryKey] ON DictionaryWords ([Index])"

        ' Add a unique key on dictionary words
        SimpleUpdateCommand("CREATE UNIQUE INDEX [Word] ON DictionaryWords ([Word], [Meaning], [MainIndex])")
    End Sub

    ''' <summary>
    ''' Creates (indexed) groups table.
    ''' </summary>
    Private Sub InitializeGroupsTable()
        SimpleUpdateCommand("CREATE TABLE Groups ([Index] INTEGER PRIMARY KEY AUTOINCREMENT, [GroupName] TEXT(50) NOT NULL, [GroupSubName] TEXT(50) NOT NULL, [GroupTable] TEXT(50) NOT NULL)")

        ' Create Primary Key, not possible in sqlite
        'command = "CREATE INDEX [PrimaryKey] ON Groups ([Index]);"
        'db.ExecuteNonQuery(command)

        ' Add index
        SimpleUpdateCommand("CREATE UNIQUE INDEX [Group] ON Groups ([GroupName], [GroupSubName])")
    End Sub

    ''' <summary>
    ''' Adds main language column to dictionary and initializes it with the value 'german'. The column is added to the unique index.
    ''' </summary>
    Private Sub UpdateTo1_1()
        SimpleUpdateCommand("ALTER TABLE [DictionaryMain] ADD COLUMN [MainLanguage] TEXT(50)")
        SimpleUpdateCommand("UPDATE [DictionaryMain] SET [MainLanguage]='german'")
        SimpleUpdateCommand("DROP INDEX [DictionaryWord]")
        SimpleUpdateCommand("CREATE UNIQUE INDEX [DictionaryWord] ON [DictionaryMain] ([WordEntry], [LanguageName], [MainLanguage])")
        UpdateToVersion(DB_VERSION_1_1)
    End Sub

    ''' <summary>
    ''' Extends the dictionary with a column to store whether a word is irregular.
    ''' </summary>
    Private Sub UpdateTo1_2()
        SimpleUpdateCommand("ALTER TABLE [DictionaryWords] ADD COLUMN [Irregular] BIT;")
        UpdateToVersion(DB_VERSION_1_2)
    End Sub

    ''' <summary>
    ''' Extends cards table to store intervals.
    ''' </summary>
    Private Sub UpdateTo1_3()
        ' Cards-Modus für abfrage Abfrage der Hauptsprache hinzugefügt
        SimpleUpdateCommand("ALTER TABLE [Cards] ADD COLUMN [TestIntervalMain] INT")
        SimpleUpdateCommand("ALTER TABLE [Cards] ADD COLUMN [CounterMain] INT")
        SimpleUpdateCommand("UPDATE [Cards] SET [TestIntervalMain] = 1, [CounterMain] = 1;")
        UpdateToVersion(DB_VERSION_1_3)
    End Sub

    ''' <summary>
    ''' Add column 'Marked' to the dictionary.
    ''' </summary>
    Private Sub UpdateTo1_4()
        SimpleUpdateCommand("ALTER TABLE [DictionaryWords] ADD COLUMN [Marked] BIT;")
        SimpleUpdateCommand("UPDATE DictionaryWords SET Marked='-1'")
        UpdateToVersion(DB_VERSION_1_4)
    End Sub

    ''' <summary>
    ''' Replace the 'Marked' column in the dictionary with a 'Marked' column in the groups.
    ''' </summary>
    Private Sub UpdateTo1_5()
        Dim GroupsDao As IGroupsDao = New GroupsDao(Me.DBConnection)
        Dim Groups As ICollection(Of GroupEntry) = GroupsDao.GetAllGroups()

        ' Add the 'Marked' column to groups and transfers the values from dictionary
        Dim command As String
        For Each group As GroupEntry In Groups
            Dim table As String = StripSpecialCharacters(group.Table)
            SimpleUpdateCommand("ALTER TABLE [" & table & "] ADD COLUMN [Marked] BIT")

            command = "SELECT [WordIndex] FROM [" & table & "]"
            DBConnection.ExecuteReader(command, Array.Empty(Of Object))
            Dim indices As New List(Of Integer)
            While DBConnection.DBCursor.Read()
                Dim index = DBConnection.SecureGetInt32(0)
                indices.Add(index)
            End While

            Dim dict As New DictionaryDao(Me.DBConnection)
            For Each index As Integer In indices
                command = "SELECT Marked FROM [DictionaryWords] WHERE [Index] = ?"
                DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {index}))
                DBConnection.DBCursor.Read()
                Dim marked As Boolean = DBConnection.SecureGetBool(0)
                DBConnection.DBCursor.Close()

                command = "UPDATE [" & table & "] SET [Marked] = ? WHERE [WordIndex] = ?" & index
                DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(New List(Of Object) From {marked, index}))
            Next
        Next group

        'drop 'Marked' column, need to copy everything in MySQL
        SimpleUpdateCommand("CREATE TABLE [DictionaryWordsCopy] ([Index] INTEGER PRIMARY KEY AUTOINCREMENT, [MainIndex] INT NOT NULL, [Word] TEXT(80) NOT NULL, [Pre] TEXT(16), [Post] TEXT(16), [WordType] INT, [Meaning] TEXT(80) NOT NULL, TargetLanguageInfo TEXT(50), [Irregular] BIT)")
        SimpleUpdateCommand("INSERT INTO [DictionaryWordsCopy] ([MainIndex], [Word], [Pre], [Post], [WordType], [Meaning], [TargetLanguageInfo], [Irregular]) SELECT [MainIndex], [Word], [Pre], [Post], [WordType], [Meaning], [TargetLanguageInfo], '' FROM [DictionaryWords]")
        SimpleUpdateCommand("DROP TABLE [DictionaryWords]")
        SimpleUpdateCommand("ALTER TABLE [DictionaryWordsCopy] RENAME TO [DictionaryWords]")

        ' Recreate the index
        SimpleUpdateCommand("CREATE UNIQUE INDEX [Word] ON DictionaryWords ([Word], [Meaning], [MainIndex])")

        UpdateToVersion(DB_VERSION_1_5)
    End Sub

    ''' <summary>
    ''' Updates database entries from version 1.05 to 1.06. Reduces the text length from main language to 16 chars.
    ''' </summary>
    Private Sub UpdateTo1_6()
        ' Update field length by copying as alter table is not supported by sqlite
        SimpleUpdateCommand("CREATE TABLE [DictionaryMainCopy] ([Index] INTEGER PRIMARY KEY AUTOINCREMENT, [WordEntry] TEXT(50) NOT NULL, [LanguageName] TEXT(16) NOT NULL, [MainLanguage] TEXT(16))")
        SimpleUpdateCommand("INSERT INTO [DictionaryMainCopy] ([WordEntry], [LanguageName], [MainLanguage]) SELECT [WordEntry], [LanguageName], [MainLanguage] FROM [DictionaryMain]")
        SimpleUpdateCommand("DROP TABLE [DictionaryMain]")
        SimpleUpdateCommand("ALTER TABLE [DictionaryMainCopy] RENAME TO [DictionaryMain]")

        ' Recreate the index
        SimpleUpdateCommand("CREATE UNIQUE INDEX [DictionaryWord] ON [DictionaryMain] ([WordEntry], [LanguageName], [MainLanguage])")

        UpdateToVersion(DB_VERSION_1_6)
    End Sub

    ''' <summary>
    ''' Updates the database entries from version 1.06 to 1.07. Updates Cards table to disallow NULL entires. Add column example, interval, last test date and counters to each
    ''' group table. Add irregular flag to dictionary
    ''' 
    ''' This is a big update.
    ''' </summary>
    Private Sub UpdateTo1_7()
        'command = "ALTER TABLE [Cards] ALTER COLUMN [TestIntervalMain] INT NOT NULL;"
        'DBConnection.ExecuteNonQuery(command)
        'command = "ALTER TABLE [Cards] ALTER COLUMN [CounterMain] INT NOT NULL;"
        'DBConnection.ExecuteNonQuery(command)

        SimpleUpdateCommand("CREATE TABLE [CardsCopy] ([Index] INT NOT NULL, [TestInterval] INT NOT NULL, [Counter] INT NOT NULL, [LastDate] DATETIME NOT NULL, [TestIntervalMain] INT NOT NULL, [CounterMain] INT NOT NULL)")
        SimpleUpdateCommand("INSERT INTO [CardsCopy] ([Index], [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain]) SELECT [Index], [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain] FROM [Cards]")
        SimpleUpdateCommand("DROP TABLE [Cards]")
        SimpleUpdateCommand("ALTER TABLE [CardsCopy] RENAME TO [Cards]")

        Dim groupsDao As IGroupsDao = New GroupsDao(Me.DBConnection)
        Dim groups As ICollection(Of GroupEntry) = groupsDao.GetAllGroups()
        For Each group As GroupEntry In groups
            Dim table As String = StripSpecialCharacters(group.Table)
            ' neue spalte hinzufügen
            SimpleUpdateCommand("ALTER TABLE [" & table & "] ADD COLUMN [Example] TEXT(64), [TestInterval] INT NOT NULL, [Counter] INT NOT NULL, [LastDate] DATETIME NOT NULL, [TestIntervalMain] INT NOT NULL, [CounterMain] INT NOT NULL)")

            Dim command As String = "SELECT [WordIndex] FROM [" & table & "]"
            DBConnection.ExecuteReader(command, Array.Empty(Of Object))
            Dim indices As ICollection(Of Integer) = New List(Of Integer)
            While DBConnection.DBCursor.Read()
                Dim index = DBConnection.SecureGetInt32(0)
                ' store the index for later
                indices.Add(index)
            End While

            Dim dictionary As New DictionaryDao(DBConnection)
            For Each index As Integer In indices
                command = "SELECT [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain] FROM [Cards] WHERE [Index] = ?"
                Dim o As Object = index
                DBConnection.ExecuteReader(command, Enumerable.Repeat(o, 1))
                DBConnection.DBCursor.Read()
                Dim testInterval As Integer = DBConnection.SecureGetInt32(0)
                Dim counter As Integer = DBConnection.SecureGetInt32(1)
                Dim lastDateTemp As System.DateTime = DBConnection.SecureGetDateTime(2)
                Dim lastDate As String = lastDateTemp.Day & "." & lastDateTemp.Month & "." & lastDateTemp.Year
                Dim testIntervalMain As Integer = DBConnection.SecureGetInt32(3)
                Dim counterMain As Integer = DBConnection.SecureGetInt32(4)
                DBConnection.DBCursor.Close()

                ' save
                command = "UPDATE [" & table & "] SET [TestInterval] = ?, [Counter] = ?, [LastDate] = ?, [TestIntervalMain] = ?, [CounterMain] = ? WHERE [WordIndex] = ?"
                DBConnection.ExecuteNonQuery(command, New List(Of Object) From {testInterval, counter, lastDate, testIntervalMain, counterMain, index})
            Next index
        Next group

        ' Create supported word types
        SimpleUpdateCommand("CREATE TABLE [SupportedWordTypes] ([Type] TEXT(32) NOT NULL, [Index] INT PRIMARY KEY)")
        SimpleUpdateCommand("INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES('WORD_TYPE_SUBSTANTIVE', '0')")
        SimpleUpdateCommand("INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES('WORD_TYPE_VERB', '1')")
        SimpleUpdateCommand("INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES('WORD_TYPE_ADJECTIVE', '2')")
        SimpleUpdateCommand("INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES('WORD_TYPE_SIMPLE', '3')")
        SimpleUpdateCommand("INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES('WORD_TYPE_ADVERB', '4')")
        SimpleUpdateCommand("INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES('WORD_TYPE_SET_PHRASE', '5')")
        SimpleUpdateCommand("INSERT INTO [SupportedWordTypes] ([Type], [Index]) VALUES('WORD_TYPE_EXAMPLE', '6')")

        ' Set unsupported values to 3 (simple)
        SimpleUpdateCommand("UPDATE [DictionaryWords] SET [WordType] = '3' WHERE [WordType] Not In (0,1,2,3,4,5,6)")

        ' instead of ON UPDATE CASCADE we need ON DELETE REJECT
        SimpleUpdateCommand("PRAGMA foreign_keys = ON")
        '"ALTER TABLE  ADD CONSTRAINT TestConstraint FOREIGN KEY ([WordType]) REFERENCES SupportedWordTypes ON UPDATE CASCADE"

        ' Update DictionaryWords table
        SimpleUpdateCommand("CREATE TABLE [DictionaryWordsCopy] ([Index] INTEGER PRIMARY KEY AUTOINCREMENT, [MainIndex] INT NOT NULL, [Word] TEXT(80) NOT NULL, [Pre] TEXT(16), [Post] TEXT(16), [WordType] INT, [Meaning] TEXT(80) NOT NULL, TargetLanguageInfo TEXT(50), [Irregular] BIT, FOREIGN KEY ([WordType]) REFERENCES SupportedWordTypes ON DELETE RESTRICT)")
        SimpleUpdateCommand("INSERT INTO [DictionaryWordsCopy] ([MainIndex], [Word], [Pre], [Post], [WordType], [Meaning], [TargetLanguageInfo], [Irregular]) SELECT [MainIndex], [Word], [Pre], [Post], [WordType], [Meaning], [TargetLanguageInfo], 'False' FROM [DictionaryWords]")
        SimpleUpdateCommand("DROP TABLE [DictionaryWords]")
        SimpleUpdateCommand("ALTER TABLE [DictionaryWordsCopy] RENAME TO [DictionaryWords]")

        ' Recreate the Index
        SimpleUpdateCommand("CREATE UNIQUE INDEX [Word] ON DictionaryWords ([Word], [Meaning], [MainIndex])")

        UpdateToVersion(DB_VERSION_1_7)
    End Sub

    ''' <summary>
    ''' Adds a certain version information to the Version table.
    ''' </summary>
    ''' <param name="version">The DBVersion for the new version</param>
    Private Sub UpdateToVersion(version As DBVersion)
        Dim command As String = "INSERT INTO [DBVersion] ([Version], [Date], [Description]) VALUES(?, ?, ?)"
        Dim versionString As String = version.Major & "." & version.Minor.ToString("00")
        Dim versionParameters = New List(Of Object) From {versionString, version.Introduction.ToString("yyyy-MM-dd"), version.Description}
        DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(versionParameters))
    End Sub

    ''' <summary>
    ''' Executes a simple non query command without parameters.
    ''' </summary>
    ''' <param name="command"></param>
    Private Sub SimpleUpdateCommand(command As String)
        DBConnection.ExecuteNonQuery(command, Enumerable.Empty(Of Object))
    End Sub

    Private Function GetLatest() As DBVersion
        GetLatest = supportedVersions.Last
    End Function

    Public Function IsVersionUpToDate() As Boolean Implements IManagementDao.IsVersionUpToDate
        Dim version As DBVersion = GetCurrentVersion()
        If version Is Nothing Then
            Return False
        End If
        If Not supportedVersions.Contains(Version) Then Throw New IllegalVersionException("Version " & Version.ToString & " is not supported.")
        Return Version.Equals(GetLatest())
    End Function

    Public Function IsUpdateComplex(version As DBVersion) As Boolean Implements IManagementDao.IsUpdateComplex
        If Not supportedVersions.Contains(version) Then Throw New IllegalVersionException("Version " & version.ToString & " is not supported.")
        Return ComplexVersionUpdates.Contains(version)
    End Function

    Sub UpdateDatabaseVersion() Implements IManagementDao.UpdateDatabaseVersion
        Dim nextVersion = GetNextVersion()
        If nextVersion Is Nothing Then
            Return
        ElseIf nextVersion.Equals(DB_VERSION_1_0) Then
            Throw New IllegalVersionException("Database not initialized. Use Initialize")
        End If
        While NeedsUpdate()
            UpdateToNext()
        End While
    End Sub

    ''' <summary>
    ''' Updates the database to the next version.
    ''' </summary>
    Sub UpdateDatabaseSingle()
        If NeedsUpdate() Then
            UpdateToNext()
        End If
    End Sub

    Private Function NeedsUpdate() As Boolean
        Dim nextVersion As DBVersion = GetNextVersion()
        If nextVersion Is Nothing OrElse nextVersion.Equals(GetCurrentVersion()) Then
            NeedsUpdate = False
        Else
            NeedsUpdate = True
        End If
    End Function

    Private Sub UpdateToNext()
        Dim updateFunction As Action = updateFunctions(GetCurrentVersion())
        updateFunction()
    End Sub

    Function Reorganize() As Integer Implements IManagementDao.Reorganize
        Dim ErrorCount = 0

        ' delete cards entries for non-existing words
        Dim command As String = "SELECT [Index] FROM Cards ORDER BY [Index]"
        DBConnection.ExecuteReader(command, Enumerable.Empty(Of Object))
        Dim indices As New List(Of Integer)
        Do While DBConnection.DBCursor.Read()
            indices.Add(DBConnection.SecureGetInt32(0))
        Loop
        DBConnection.DBCursor.Close()
        For Each index As Integer In indices
            command = "SELECT Word FROM DictionaryWords WHERE [Index] = ?"
            DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {index}))
            If Not DBConnection.DBCursor.HasRows Then
                command = "DELETE FROM Cards WHERE [Index] = ?"
                DBConnection.DBCursor.Close()
                DBConnection.ExecuteNonQuery(command, EscapeSingleQuotes(New List(Of Object) From {index}))
                ErrorCount += 1
            Else
                DBConnection.DBCursor.Close()
            End If
        Next index

        ' fixes missing card entries
        command = "SELECT [Index] FROM DictionaryWords ORDER BY [Index]"
        DBConnection.ExecuteReader(command, Enumerable.Empty(Of Object))
        indices = New List(Of Integer)
        Do While DBConnection.DBCursor.Read
            indices.Add(DBConnection.SecureGetInt32(0))
        Loop
        DBConnection.DBCursor.Close()
        For Each index As Integer In indices
            command = "SELECT TestInterval FROM Cards WHERE [Index] = ?"
            DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {index}))
            If Not DBConnection.DBCursor.HasRows Then
                DBConnection.DBCursor.Close()
                'command = "INSERT INTO Cards ([Index], [], [], [], []) VALUES(" & index & ")"
                'command = "INSERT INTO Cards ([Index], [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain])VALUES (" & index & ", 1, 1, '01.01.1900', 1, 1)"
                'Dim cards As New CardsDao(DBConnection)
                'cards.AddNewEntry(index)
                'DBConnection.ExecuteNonQuery(command)
                ErrorCount += 1
            Else
                DBConnection.DBCursor.Close()
            End If
        Next index

        ' Look for entries in groups without existing words
        Dim GroupsDao As IGroupsDao = New GroupsDao(DBConnection)
        Dim GroupDao As IGroupDao = New GroupDao(DBConnection)

        For Each Group As GroupEntry In GroupsDao.GetAllGroups()
            Dim groupDto As GroupDto = GroupDao.Load(Group)
            For Each entry As TestWord In groupDto.Entries
                command = "SELECT MainIndex FROM DictionaryWords WHERE [Index] = ?"
                DBConnection.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {entry.Index}))
                If DBConnection.DBCursor.HasRows = False Then
                    ' delete because entry does not exist
                    DBConnection.CloseReader()
                    GroupDao.Delete(Group, entry)
                    ErrorCount += 1
                Else
                    DBConnection.CloseReader()
                End If
            Next entry
        Next Group

        Return ErrorCount
    End Function

    Public ReadOnly Property LatestVersion() As DBVersion Implements IManagementDao.LatestVersion
        Get
            Return supportedVersions.Last
        End Get
    End Property

    Function GetCurrentVersion() As DBVersion Implements IManagementDao.GetCurrentVersion
        Dim PropertiesDao As New PropertiesDao(DBConnection)
        Dim versions As ICollection(Of DBVersion) = PropertiesDao.LoadVersions()
        Return versions.LastOrDefault()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns>The next version, or Nothing.</returns>
    Function GetNextVersion() As DBVersion Implements IManagementDao.GetNextVersion
        Dim current As DBVersion = GetCurrentVersion()
        If current Is Nothing Then
            GetNextVersion = DB_VERSION_1_0
        ElseIf current.Equals(LatestVersion()) Then
            GetNextVersion = Nothing
        Else
            Dim nextVersionIndex = GetVersionIndex(current)
            If nextVersionIndex = -1 Then Throw New IllegalVersionException("Current version not supported")
            GetNextVersion = supportedVersions(nextVersionIndex + 1)
        End If
    End Function

    Private Function GetVersionIndex(version As DBVersion) As Int16
        GetVersionIndex = Array.IndexOf(supportedVersions, version)
    End Function

    ''' <summary>
    ''' Initializes a new database at a given path.
    ''' </summary>
    ''' <param name="fileName"></param>
    Public Shared Sub CreateNewVocabularyDatabase(ByVal fileName As String)
        Dim db As New SQLiteDataBaseOperation()
        db.Open(fileName)
        Dim man = New ManagementDao(db)
        man.Initialize()
        man.UpdateDatabaseVersion()
        db.Close()
    End Sub
End Class
