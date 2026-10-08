Imports Microsoft.Data.Sqlite
Imports System.Collections.Immutable
Imports System.ComponentModel
Imports System.Data.Common
Imports System.Globalization
Imports System.Reflection
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

    ''' <summary>
    ''' Returns nothing if no version information was found.    
    ''' </summary>
    ''' <returns></returns>
    Function LoadVersions() As ICollection(Of Properties.DBVersion)
        Try
            Dim command As String = "SELECT [Version], [Date], [Description] FROM DBVersion"
            DBConnection.ExecuteReader(command, Array.Empty(Of Object))
        Catch ex As SqliteException
            If ex.SqliteErrorCode = 1 AndAlso ex.Message.Contains("no such table: DBVersion") AndAlso DBConnection.IsEmpty Then
                Return ImmutableList(Of Properties.DBVersion).Empty
            End If
            Throw
        End Try
        LoadVersions = ExtractVersions(DBConnection.DBCursor)
        DBConnection.DBCursor.Close()
        If LoadVersions.Count = 0 Then Throw New IllegalVersionException("Version table exists, but no version found. Database invalid.")
    End Function

    Private Function ExtractVersions(cursor As DbDataReader) As ICollection(Of Properties.DBVersion)
        Dim versions As New List(Of Properties.DBVersion)
        Do While DBConnection.DBCursor.Read()
            Dim versionString As String = DBConnection.SecureGetString(0)
            Dim introduced As Date = DBConnection.SecureGetDateTime(1)
            Dim description As String = DBConnection.SecureGetString(2)

            Dim splits = versionString.Split(".".ToCharArray(), 2)
            Dim major As UInt16 = Convert.ToUInt16(splits(0), CultureInfo.InvariantCulture)
            Dim minor As UInt16 = Convert.ToUInt16(splits(1), CultureInfo.InvariantCulture)
            Dim version As New Properties.DBVersion(major, minor, introduced, description)
            versions.Add(version)
        Loop
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
        Dim foundWordTypes As IDictionary(Of String, WordType) = New Dictionary(Of String, WordType)

        Dim wordTypes As IDictionary(Of String, Integer) = New Dictionary(Of String, Integer)

        Dim dbVersion As Properties.DBVersion = LoadVersions().Last
        ' Erst möglich, ab version 1.07 der Datenbank
        If (dbVersion.Major = 1 And dbVersion.Minor >= 7) Or dbVersion.Major > 1 Then
            Dim command As String = "SELECT [Type], [Index] FROM [SupportedWordTypes]"
            DBConnection.ExecuteReader(command)
            While DBConnection.DBCursor.Read
                Dim wordTypeInDb As String = DBConnection.SecureGetString(0)
                Dim index As Integer = DBConnection.SecureGetInt32(1)

                Dim wordTypeCandidate As WordType? = FromTechnicalName(Of WordType)(wordTypeInDb)
                If wordTypeCandidate.HasValue Then
                    foundWordTypes.Add(wordTypeInDb, wordTypeCandidate.Value)
                End If
                wordTypes.Add(wordTypeInDb, index)
            End While
            DBConnection.DBCursor.Close()
        End If

        Dim foundWordCount = foundWordTypes.Count
        Dim expectedWordCount = [Enum].GetNames(GetType(WordType)).Length
        If foundWordCount <> expectedWordCount Then
            Throw New DataInvalidException(String.Format(CultureInfo.InvariantCulture,
                    "Not all word types stored in database. Should be {0}, but only found {1}.", expectedWordCount, foundWordCount
                    ))
        End If

        Return New WordTypes(wordTypes, foundWordTypes)
    End Function

    Private Function FromTechnicalName(Of T As Structure)(name As String) As T?
        For Each f In GetType(T).GetFields(BindingFlags.Public Or BindingFlags.Static)
            Dim attribute = f.GetCustomAttribute(Of DescriptionAttribute)()
            If attribute IsNot Nothing AndAlso
                    String.Equals(attribute.Description, name, StringComparison.OrdinalIgnoreCase) Then
                Return DirectCast(f.GetValue(Nothing), T)
            End If
        Next
        Return Nothing
    End Function
End Class
