Imports FluentAssertions
Imports Gravo
Imports NUnit.Framework
Imports System.Data.SQLite
Imports System.IO

Public Class PropertiesDaoTests
    Private ReadOnly ResourceFile = "test-data-meta.s3db"
    Private _propertiesDao As PropertiesDao
    Private _tempDb As String
    Private _db As IDataBaseOperation

    <SetUp>
    Public Sub Setup()
        _tempDb = Path.GetTempFileName
        File.Copy(DaoUtils.GetSqliteResource(ResourceFile), _tempDb, True)

        _db = New SQLiteDataBaseOperation()
        _db.Open(_tempDb)

        _propertiesDao = New PropertiesDao(_db)
    End Sub

    <TearDown>
    Public Sub CleanUp()
        _db.Close()
        SQLiteConnection.ClearAllPools()

        File.Delete(_tempDb)
    End Sub

    <Test>
    Public Sub Load_LoadsDefault()
        Dim p As Properties = _propertiesDao.LoadProperties()

        p.DBVersionMaxLengthDescription.Should.Be(80)
        p.DictionaryMainMaxLengthLanguage.Should.Be(16)
        p.DictionaryMainMaxLengthMainLanguage.Should.Be(16)
        p.DictionaryMainMaxLengthWordEntry.Should.Be(50)
        p.DictionaryWordsMaxLengthAdditionalTargetLangInfo.Should.Be(50)
        p.DictionaryWordsMaxLengthMeaning.Should.Be(80)
        p.DictionaryWordsMaxLengthPost.Should.Be(16)
        p.DictionaryWordsMaxLengthPre.Should.Be(16)
        p.DictionaryWordsMaxLengthWord.Should.Be(80)
        p.GroupMaxLengthExample.Should.Be(64)
        p.GroupsMaxLengthName.Should.Be(50)
        p.GroupsMaxLengthSubName.Should.Be(50)
        p.GroupsMaxLengthTable.Should.Be(50)
    End Sub

    <Test>
    Public Sub Load_Loads_Version()
        Dim p As Properties = _propertiesDao.LoadProperties()

        Dim expectedVersion As New Properties.DBVersion(1, 23, New Date(2017, 4, 5), "DB-Version Test")

        p.Verion.Should.BeEquivalentTo(expectedVersion)
    End Sub
End Class
