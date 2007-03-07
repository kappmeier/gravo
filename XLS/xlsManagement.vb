Imports System.Collections.ObjectModel

Public Class xlsManagement
  Inherits xlsBase

  Private versionHistory() As String = {"1.01", "1.00"}    ' Neuste vorne
  Dim m_errorCount As Integer = 0

  Function DatabaseVersion() As String
    If IsConnected() = False Then Return "" ' TODO exception werfen
    Return versionHistory(DatabaseVersionIndex)
  End Function

  Function DatabaseVersion(ByVal Index As Integer) As String
    Return versionHistory(Index)
  End Function

  Function DatabaseVersionIndex() As Integer
    If IsConnected() = False Then Exit Function ' TODO exception werfen
    Dim found As Boolean = False
    Dim i As Integer = 0
    Do Until found = True      'Or i = sVersionHistory.Length spätestens bei 1.00 ist schluß
      Dim command As String = "SELECT Version FROM DBVersion WHERE Version='" & versionHistory(i) & "'"
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

  Public Sub UpdateDatabaseVersion()
    ' Neues Datenbank Format in Vokabeltrainer 2k7 (mit Übergangszeit in 2k6). Alte codes zum Angleichen gelöscht
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
        command = "INSERT INTO DBVersion ([Version], [Date], [Description]) VALUES('1.01', '07.03.2007', 'MainLanguage')"
      Case "1.10"      ' Versionsinfo schon hinzugefügt
        ' Aktuelle Version
        command = "INSERT INTO DBVersion ([Version], [Date], [Description]) VALUES('', '', '')"
      Case Else
        command = "INSERT INTO DBVersion ([Version], [Date], [Description]) VALUES('', '', '')"
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
        command = "INSERT INTO Cards ([Index]) VALUES(" & index & ");"
        DBConnection.ExecuteNonQuery(command)
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

    ' Erstelle die DBVersion Tabelle
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
End Class
