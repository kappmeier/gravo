Imports FluentAssertions
Imports Gravo
Imports NUnit.Framework
Imports Microsoft.Data.Sqlite
Imports System.IO

Public Class SQLiteDataBaseOperationTests
    Private _tempDb As String
    Private _db As IDataBaseOperation

    <SetUp>
    Public Sub Setup()
        ' The empty temp file is opened as an empty database; no fixture needed.
        _tempDb = Path.GetTempFileName

        _db = New SQLiteDataBaseOperation()
        _db.Open(_tempDb)
    End Sub

    <TearDown>
    Public Sub CleanUp()
        _db.Close()
        SqliteConnection.ClearAllPools()

        File.Delete(_tempDb)
    End Sub

    <Test>
    Public Sub ExecuteReader_QuestionMarkInBoundValue_RoundTrips()
        _db.ExecuteNonQuery("CREATE TABLE [Words] ([Name] TEXT NOT NULL);", Array.Empty(Of Object))
        _db.ExecuteNonQuery("INSERT INTO [Words] ([Name]) VALUES (?);", New List(Of Object) From {"x?y"})

        Dim reader = _db.ExecuteReader("SELECT [Name] FROM [Words] WHERE [Name] = ?;", New List(Of Object) From {"x?y"})

        reader.Read().Should.BeTrue()
        reader.GetString(0).Should.Be("x?y")
    End Sub

    <Test>
    Public Sub ExecuteReader_PlaceholderInsideQuotedRegions_KeptVerbatim()
        _db.ExecuteNonQuery("CREATE TABLE [Groups] (""we?rd"" TEXT NOT NULL);", Array.Empty(Of Object))
        _db.ExecuteNonQuery("INSERT INTO [Groups] (""we?rd"") VALUES (?);", New List(Of Object) From {"v"})

        Dim reader = _db.ExecuteReader("SELECT 'Lektion ''5''?', ""we?rd"" FROM [Groups] WHERE ""we?rd"" = ?;", New List(Of Object) From {"v"})

        reader.Read().Should.BeTrue()
        reader.GetString(0).Should.Be("Lektion '5'?")
        reader.GetString(1).Should.Be("v")
    End Sub

    <Test>
    Public Sub ExecuteReader_PlaceholderInsideBracketedIdentifier_KeptVerbatim()
        _db.ExecuteNonQuery("CREATE TABLE [Grup?pe] ([Name] TEXT NOT NULL);", Array.Empty(Of Object))
        _db.ExecuteNonQuery("INSERT INTO [Grup?pe] ([Name]) VALUES (?);", New List(Of Object) From {"v"})

        Dim reader = _db.ExecuteReader("SELECT [Name] FROM [Grup?pe] WHERE [Name] = ?;", New List(Of Object) From {"v"})

        reader.Read().Should.BeTrue()
        reader.GetString(0).Should.Be("v")
    End Sub

    <Test>
    Public Sub ExecuteNonQuery_PlaceholderCountMismatch_Throws()
        Assert.Throws(Of ArgumentException)(Sub() _db.ExecuteNonQuery("SELECT '?';", New List(Of Object) From {"x"}))
    End Sub
End Class
