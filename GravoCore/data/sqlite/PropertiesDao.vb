Imports Gravo

Public Class PropertiesDao
    Implements IPropertiesDao

    Private ReadOnly SHORT_VALUE As Byte = 16
    Private ReadOnly LONG_VALUE As Byte = 80
    Private ReadOnly MEDIUM_VALUE As Byte = 50

    Private ReadOnly DBConnection As IDataBaseOperation

    Sub New(ByRef db As IDataBaseOperation)
        DBConnection = db
    End Sub

    Public Function LoadProperties() As Properties Implements IPropertiesDao.LoadProperties
        Dim builder = New Properties.PropertiesBuilder()
        builder.WithDictionaryWordsMaxLengthWord(LONG_VALUE)
        builder.WithDictionaryWordsMaxLengthPre(SHORT_VALUE)
        builder.WithDictionaryWordsMaxLengthPost(SHORT_VALUE)
        builder.WithDictionaryWordsMaxLengthMeaning(LONG_VALUE)
        builder.WithDictionaryWordsMaxLengthAdditionalTargetLangInfo(MEDIUM_VALUE)
        builder.WithDictionaryMainMaxLengthWordEntry(MEDIUM_VALUE)
        builder.WithDictionaryMainMaxLengthLanguage(SHORT_VALUE)
        builder.WithDictionaryMainMaxLengthMainLanguage(SHORT_VALUE)
        builder.WithDBVersionMaxLengthDescription(LONG_VALUE)
        builder.WithGroupsMaxLengthName(MEDIUM_VALUE)
        builder.WithGroupsMaxLengthSubName(MEDIUM_VALUE)
        builder.WithGroupsMaxLengthTable(MEDIUM_VALUE)
        builder.WithGroupMaxLengthExample(64)
        Dim versions = LoadVersions()
        builder.WithVersion(versions(versions.Count - 1))
        Return New Properties(builder)
    End Function

    Function LoadVersions() As ICollection(Of Properties.DBVersion)
        Dim versions As New List(Of Properties.DBVersion)
        Dim command As String = "SELECT [Version], [Date], [Description] FROM DBVersion"
        DBConnection.ExecuteReader(command, Array.Empty(Of Object))
        Do While DBConnection.DBCursor.Read()
            Dim versionString As String = DBConnection.SecureGetString(0)
            Dim introduced As Date = DBConnection.SecureGetDateTime(1)
            Dim description As String = DBConnection.SecureGetString(2)

            Dim splits = versionString.Split(".".ToCharArray(), 2)
            Dim major As UInt16 = Convert.ToUInt16(splits(0))
            Dim minor As UInt16 = Convert.ToUInt16(splits(1))
            Dim version As New Properties.DBVersion(major, minor, introduced, description)
            versions.Add(version)
        Loop
        DBConnection.DBCursor.Close()
        versions.Sort()
        Return versions
    End Function

    ''' <summary>
    ''' Loading word types:
    ''' By the database there is a mapping to id, it may be custom, so we have to get the mapping
    ''' from the database.
    ''' Fixed word types: are implemented. Load list of word types from database
    ''' find the ones that are fixed and their number.
    ''' Find non-known and their number
    ''' Add known but non-existant with new numbers
    ''' </summary>
    ''' <returns></returns>
    Function LoadWordTypes() As WordTypes Implements IPropertiesDao.LoadWordTypes
        Dim foundWordTypes As New Dictionary(Of String, WordType)

        Dim wordTypes As New Dictionary(Of String, Integer)

        Dim dbVersion As Properties.DBVersion = LoadVersions().Last
        ' Erst möglich, ab version 1.07 der Datenbank
        If (dbVersion.Major = 1 And dbVersion.Minor >= 7) Or dbVersion.Major > 1 Then
            Dim command As String = "SELECT [Type], [Index] FROM [SupportedWordTypes]"
            DBConnection.ExecuteReader(command)
            While DBConnection.DBCursor.Read
                Dim wordType As String = DBConnection.SecureGetString(0)
                Dim index As Integer = DBConnection.SecureGetInt32(1)

                Dim parsedWordType As WordType
                If [Enum].TryParse(wordType, parsedWordType) Then
                    foundWordTypes.Add(wordType, parsedWordType)
                End If
                wordTypes.Add(wordType, index)
            End While
            DBConnection.DBCursor.Close()
        End If

        If foundWordTypes.Count <> [Enum].GetNames(GetType(WordType)).Length Then
            Throw New DataInvalidException("Not all word types stored in database")
        End If

        Return New WordTypes(wordTypes, foundWordTypes)
    End Function
End Class
