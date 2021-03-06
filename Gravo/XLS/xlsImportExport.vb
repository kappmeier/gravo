Imports System.Collections.ObjectModel
Imports Gravo.AccessDatabaseOperation

Public Class xlsImportExport
    Inherits xlsBase

    Dim m_exportEmptyEntrys As Boolean = False
    Dim m_exportStats As Boolean = False
    Dim m_importedMainEntrys As Integer
    Dim m_importedSubEntrys As Integer
    Dim m_importedGroups As Integer
    Dim m_importedSubGroups As Integer
    Dim m_importedGroupEntrys As Integer

    Sub New()
        MyBase.New()
    End Sub

    Sub New(ByVal db As IDataBaseOperation)    ' Keinen Speziellen Table ausw�hlen
        MyBase.New(db)
    End Sub

    Public Sub ExportGroup(ByVal Group As String, ByVal MainLanguage As String, ByVal dbSource As IDataBaseOperation)
        Dim xlsGrp As New xlsGroups()
        Dim newdic As New xlsDictionary()
        Dim newDictionaryDao As IDictionaryDao = New CardsDao(dbSource)
        newdic.DBConnection = dbSource
        Dim dic As New xlsDictionary()
        dic.DBConnection = DBConnection
        Dim newGroups As New xlsGroups()
        newGroups.DBConnection = dbSource
        xlsGrp.DBConnection = DBConnection
        Dim GroupsDao As IGroupsDao = New GroupsDao(DBConnection)
        Dim GroupDao As IGroupDao = New GroupDao(DBConnection)
        Dim Groups As Collection(Of GroupEntry)
        Groups = GroupsDao.GetSubGroups(Group)

        For Each groupEntry As GroupEntry In Groups
            GroupsDao.AddGroup(Group, groupEntry.SubGroup)
            Dim currentGroup As xlsGroup = newGroups.GetGroup(Group, groupEntry.SubGroup)
            Dim existingGroup As xlsGroup = xlsGrp.GetGroup(Group, groupEntry.SubGroup)
            For Each index As Integer In existingGroup.GetIndices()
                ' Lade das Wort aus der originalen Datenbank
                Dim existingEntry As New xlsDictionaryEntry(DBConnection, index)
                Dim existingWord As WordEntry
                Dim newEntry As String = dic.GetEntryName(existingEntry.MainIndex)
                Dim newEntryLanguage As String = dic.GetEntryLanguage(existingEntry.MainIndex)

                ' Lade das Wort aus der neuen Datenbank
                ' Main-Index
                Dim entryMainIndex As Integer
                Try
                    entryMainIndex = newdic.GetEntryIndex(newEntry, newEntryLanguage, MainLanguage)
                Catch ex As LanguageNotFoundException
                    newdic.AddEntry(newEntry, newEntryLanguage, MainLanguage)
                    entryMainIndex = newdic.GetEntryIndex(newEntry, newEntryLanguage, MainLanguage)
                Catch ex As EntryNotFoundException
                    newdic.AddEntry(newEntry, newEntryLanguage, MainLanguage)
                    entryMainIndex = newdic.GetEntryIndex(newEntry, newEntryLanguage, MainLanguage)
                End Try

                ' Index in der Word-Datenbank
                Dim entryIndex As Integer
                Try
                    entryIndex = newdic.GetSubEntryIndex(entryMainIndex, existingEntry.Word, existingEntry.Meaning)
                Catch ex As EntryNotFoundException
                    ' F�ge das Wort nun ein
                    existingEntry.MainIndex = entryMainIndex
                    newDictionaryDao.AddSubEntry(existingWord, newEntry, newEntryLanguage, MainLanguage)
                    entryIndex = newdic.GetSubEntryIndex(entryMainIndex, existingEntry.Word, existingEntry.Meaning)
                End Try
                Dim marked As Boolean = existingGroup.GetMarked(index)
                ' TODO example
                'currentGroup.Add(entryIndex, marked, "")
                GroupDao.Add(Nothing, Nothing, marked, "")
            Next index
        Next groupEntry
    End Sub

    Public Sub ExportLanguage(ByVal Language As String, ByVal MainLanguage As String, ByVal dbSource As IDataBaseOperation)
        ' sichern der Main-Eintr�ge
        Dim command As String
        If ExportEmptyEntrys Then
            command = "SELECT WordEntry FROM DictionaryMain WHERE LanguageName=" & GetDBEntry(Language) & " AND MainLanguage=" & GetDBEntry(MainLanguage) & " ORDER BY WordEntry;"
        Else
            command = "SELECT DISTINCT M.WordEntry FROM DictionaryMain AS M, DictionaryWords AS W WHERE LanguageName=" & GetDBEntry(Language) & " AND M.MainLanguage=" & GetDBEntry(MainLanguage) & " AND W.MainIndex=M.[Index] ORDER BY M.WordEntry;"
        End If
        DBConnection.ExecuteReader(command)
        ' Sichern in die neue Datenbank
        While DBConnection.DBCursor.Read()
            Dim entry As String = DBConnection.SecureGetString(0)
            command = "INSERT INTO DictionaryMain (WordEntry, LanguageName, MainLanguage) VALUES (" & GetDBEntry(entry) & ", " & GetDBEntry(Language) & ", " & GetDBEntry(MainLanguage) & ");"
            dbSource.ExecuteNonQuery(command)
        End While
        DBConnection.CloseReader()

        ' Sichern der Word-Eintr�ge
        command = "SELECT M.WordEntry, W.Word, W.Pre, W.Post, W.WordType, W.Meaning, W.TargetLanguageInfo FROM DictionaryMain AS M, DictionaryWords AS W WHERE W.MainIndex=M.[Index] AND M.LanguageName=" & GetDBEntry(Language) & " AND M.MainLanguage=" & GetDBEntry(MainLanguage) & " ORDER BY M.WordEntry, W.Word, W.Meaning"
        DBConnection.ExecuteReader(command)
        ' Speichern in neuer Datenbank
        Dim firstNewIndex As Integer = -1
        While DBConnection.DBCursor.Read()
            ' Holen des Haupteintrages der neuen Datenbank
            command = "SELECT [Index] FROM DictionaryMain WHERE WordEntry=" & GetDBEntry(DBConnection.SecureGetString(0)) & " AND LanguageName=" & GetDBEntry(Language) & " AND MainLanguage=" & GetDBEntry(MainLanguage) & ";"
            dbSource.ExecuteReader(command)
            dbSource.DBCursor.Read()
            Dim newMainIndex As Integer = dbSource.SecureGetInt32(0)
            dbSource.CloseReader()
            command = "INSERT INTO DictionaryWords (MainIndex, Word, Pre, Post, WordType, Meaning, TargetLanguageInfo) VALUES (" & newMainIndex & ", " & GetDBEntry(DBConnection.SecureGetString(1)) & ", " & GetDBEntry(DBConnection.SecureGetString(2)) & ", " & GetDBEntry(DBConnection.SecureGetString(3)) & ", " & DBConnection.SecureGetInt32(4) & ", " & GetDBEntry(DBConnection.SecureGetString(5)) & ", " & GetDBEntry(DBConnection.SecureGetString(6)) & ");"
            dbSource.ExecuteNonQuery(command)
            ' Erzeugen der Cards-Eintr�ge
            ' erzeuge f�r jeden Eintrag in der neuen Datenbank einen Stat-Eintrag
            command = "SELECT [Index] FROM DictionaryWords WHERE MainIndex=" & newMainIndex & " AND Word=" & GetDBEntry(DBConnection.SecureGetString(1)) & " AND Meaning=" & GetDBEntry(DBConnection.SecureGetString(5)) & ";"
            dbSource.ExecuteReader(command)
            dbSource.DBCursor.Read()
            Dim newWordIndex As Int32 = dbSource.SecureGetInt32(0)
            dbSource.CloseReader()
            If ExportStats Then
                command = "INSERT INTO Cards ([Index], [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain]) VALUES (" & newWordIndex & ", 1, 1, '1900-01-01', 1, 1);"
            Else
                command = "INSERT INTO Cards ([Index], [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain]) VALUES (" & newWordIndex & ", 1, 1, '1900-01-01', 1, 1);"
            End If
            dbSource.ExecuteNonQuery(command)
        End While
    End Sub

    Public Property ExportStats() As Boolean
        Get
            Return m_exportStats
        End Get
        Set(ByVal value As Boolean)
            m_exportStats = value
        End Set
    End Property

    Public Property ExportEmptyEntrys() As Boolean
        Get
            Return m_exportEmptyEntrys
        End Get
        Set(ByVal value As Boolean)
            m_exportEmptyEntrys = value
        End Set
    End Property

    Public Sub ImportDictionary(ByVal mainLanguage As String, ByVal dbSource As IDataBaseOperation)
        Dim dicImport As New xlsDictionary(dbSource)
        Dim importDictionaryDao As IDictionaryDao = New CardsDao(dbSource)
        Dim dic As New xlsDictionary(DBConnection)
        ImportedMainEntrys = 0
        ImportedSubEntrys = 0
        ImportedGroups = 0
        ImportedGroupEntrys = 0
        ImportedSubGroups = 0

        ' F�ge die Haupt-Eintr�ge ein
        For Each language As String In importDictionaryDao.DictionaryLanguages(mainLanguage)
            For Each word As String In dicImport.DictionaryEntrys(language, mainLanguage)
                Try
                    dic.AddEntry(word, language, mainLanguage)
                    ImportedMainEntrys += 1
                Catch ex As EntryExistsException
                    Exit Try
                Catch ex As Exception
                    Throw ex
                End Try

                ' F�ge die Sub-Eintr�ge ein
                For Each subEntry As WordEntry In importDictionaryDao.GetWordsAndSubWords(word, language, mainLanguage)
                    'subEntry.MainIndex = dic.GetEntryIndex(word, language, mainLanguage)
                    Try
                        '                        dic.AddSubEntry(subEntry, word, language, mainLanguage)
                    Catch ex As EntryExistsException
                        ' schon vorhanden gewesen
                        Continue For
                    End Try
                    ImportedSubEntrys += 1
                Next subEntry
            Next word
        Next language
    End Sub

    Public Sub ImportGroups(ByVal mainLanguage As String, ByVal dbSource As IDataBaseOperation)
        Dim dicImport As New xlsDictionary(dbSource)
        Dim dic As New xlsDictionary(DBConnection)
        Dim dictionaryDao As IDictionaryDao = New CardsDao(DBConnection)
        Dim xlsGroupsImport As New xlsGroups(dbSource)
        Dim GroupsImportDao As IGroupsDao = New GroupsDao(dbSource)

        Dim groups As New xlsGroups(DBConnection)
        Dim GrouspDao As IGroupsDao = New GroupsDao(DBConnection)
        Dim GroupDao As IGroupDao = New GroupDao(DBConnection)
        ImportedMainEntrys = 0
        ImportedSubEntrys = 0
        ImportedGroups = 0
        ImportedGroupEntrys = 0
        ImportedSubGroups = 0

        ' Alle Gruppen �berschreiben!
        ' das hei�t, wenn eine Gruppe unter demselben Namen vorhanden war, _alles_ l�schen
        For Each group As String In GroupsImportDao.GetGroups
            ' Testen, ob eine solche Gruppe in der DB vorhanden ist. Wenn ja --> l�schen
            If GrouspDao.GroupExists(group) Then GrouspDao.DeleteGroup(group)

            ' Einf�gen der Gruppe
            For Each subGroup As GroupEntry In GroupsImportDao.GetSubGroups(group)
                GroupsImportDao.AddGroup(group, subGroup.SubGroup)

                Dim grpImport As xlsGroup = xlsGroupsImport.GetGroup(group, subGroup.SubGroup)
                Dim grp As xlsGroup = groups.GetGroup(group, subGroup.SubGroup)

                For Each index As Integer In grpImport.GetIndices()
                    Dim word As xlsDictionaryEntry
                    Dim wordEntry As WordEntry
                    Try
                        word = New xlsDictionaryEntry(dbSource, index) ' eintag mu� da vorhanden sein, wenn db ok.
                    Catch ex As Exception
                        Continue For ' aus irgendeinem Grund ist der Index nicht vorhanden, �berspringen
                    End Try

                    Dim entry As String = dicImport.GetEntryName(word.MainIndex)
                    Dim language As String = dicImport.GetEntryLanguage(word.MainIndex)

                    Dim mainIndex As Integer
                    Try
                        mainIndex = dic.GetEntryIndex(entry, language, mainLanguage)
                    Catch ex As LanguageNotFoundException
                        dic.AddEntry(entry, language, mainLanguage)
                        mainIndex = dic.GetEntryIndex(entry, language, mainLanguage)
                        ImportedMainEntrys += 1
                    Catch ex As EntryNotFoundException
                        dic.AddEntry(entry, language, mainLanguage)
                        mainIndex = dic.GetEntryIndex(entry, language, mainLanguage)
                        ImportedMainEntrys += 1
                    End Try

                    Dim subIndex As Integer
                    Try
                        subIndex = dic.GetSubEntryIndex(mainIndex, word.Word, word.Meaning)
                    Catch ex As EntryNotFoundException
                        word.MainIndex = mainIndex
                        dictionaryDao.AddSubEntry(wordEntry, entry, language, mainLanguage)
                        subIndex = dic.GetSubEntryIndex(mainIndex, word.Word, word.Meaning)
                        ImportedSubEntrys += 1
                    End Try
                    Dim marked As Boolean = grpImport.GetMarked(index)
                    ' TODO example
                    'grp.Add(subIndex, marked, "")
                    GroupDao.Add(Nothing, Nothing, marked, "")
                    ImportedGroupEntrys += 1
                Next index
                ImportedSubGroups += 1
            Next subGroup
            ImportedGroups += 1
        Next group
    End Sub

    Public Property ImportedMainEntrys() As Integer
        Get
            Return m_importedMainEntrys
        End Get
        Set(ByVal value As Integer)
            m_importedMainEntrys = value
        End Set
    End Property

    Public Property ImportedSubEntrys() As Integer
        Get
            Return m_importedSubEntrys
        End Get
        Set(ByVal value As Integer)
            m_importedSubEntrys = value
        End Set
    End Property

    Public Property ImportedGroups() As Integer
        Get
            Return m_importedGroups
        End Get
        Set(ByVal value As Integer)
            m_importedGroups = value
        End Set
    End Property

    Public Property ImportedGroupEntrys() As Integer
        Get
            Return m_importedGroupEntrys
        End Get
        Set(ByVal value As Integer)
            m_importedGroupEntrys = value
        End Set
    End Property

    Public Property ImportedSubGroups() As Integer
        Get
            Return m_importedSubGroups
        End Get
        Set(ByVal value As Integer)
            m_importedSubGroups = value
        End Set
    End Property
End Class