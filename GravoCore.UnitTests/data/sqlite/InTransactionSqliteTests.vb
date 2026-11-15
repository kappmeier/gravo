Imports System.IO
Imports AwesomeAssertions
Imports Gravo
Imports NUnit.Framework

''' <summary>
''' Tests the transaction helper against a real SQLite database.
''' </summary>
<TestFixture>
Public Class InTransactionSqliteTests
    Private _tempDb As String
    Private _db As IDataBaseOperation

    <SetUp>
    Public Sub Setup()
        _tempDb = Path.GetTempFileName
        File.Copy(DaoUtils.GetSqliteResource("test-data-simple.s3db"), _tempDb, True)

        _db = New SQLiteDataBaseOperation()
        _db.Open(_tempDb)

        ExecuteNonQuery(_db, "
            CREATE TABLE [TestTable] (
                [TestColumn] INT
            )
        ")
    End Sub

    <TearDown>
    Public Sub CleanUp()
        _db.Close()

        File.Delete(_tempDb)
    End Sub

    <Test>
    Public Sub InTransaction_NestedBothReturn_CommitsBoth()
        _db.InTransaction(AddressOf InsertOneThenTwo)

        Values().Should.Equal(1, 2)
    End Sub

    <Test>
    Public Sub InTransaction_NestedInnerThrowsAndIsCaught_KeepsTheOuterWrites()
        _db.InTransaction(AddressOf InsertOneThenFailingInnerThenThree)

        Values().Should.Equal(1, 3)
    End Sub

    <Test>
    Public Sub InTransaction_NestedOuterThrowsAfterInner_RollsBackBoth()
        Dim act As Action = Sub() _db.InTransaction(AddressOf InsertOneTwoThenThrow)

        act.Should.Throw(Of InvalidOperationException)()
        Values().Should.BeEmpty()
    End Sub

    Private Sub InsertOneThenTwo()
        InsertValue(1)
        _db.InTransaction(Sub() InsertValue(2))
    End Sub

    Private Sub InsertOneThenFailingInnerThenThree()
        InsertValue(1)
        Try
            _db.InTransaction(AddressOf InsertTwoThenThrow)
        Catch
            ' the inner savepoint is rolled back, the outer writes stay
        End Try
        InsertValue(3)
    End Sub

    Private Sub InsertOneTwoThenThrow()
        InsertValue(1)
        _db.InTransaction(Sub() InsertValue(2))
        Throw New InvalidOperationException("outer")
    End Sub

    Private Sub InsertTwoThenThrow()
        InsertValue(2)
        Throw New InvalidOperationException("inner")
    End Sub

    Private Sub InsertValue(value As Integer)
        ExecuteNonQuery(_db, $"
            INSERT INTO [TestTable] ([TestColumn])
            VALUES ({value})
        ")
    End Sub

    Private Function Values() As List(Of Integer)
        Dim result As New List(Of Integer)
        ExecuteReader(_db, "
            SELECT [TestColumn]
              FROM [TestTable]
             ORDER BY [TestColumn]
        ")
        Do While _db.DBCursor.Read()
            result.Add(_db.SecureGetInt32(0))
        Loop
        _db.CloseReader()
        Return result
    End Function
End Class
