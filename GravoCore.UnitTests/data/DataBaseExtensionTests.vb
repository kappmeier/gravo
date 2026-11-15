Imports AwesomeAssertions
Imports Gravo
Imports Microsoft.Data.Sqlite
Imports Moq
Imports NUnit.Framework

''' <summary>
''' Tests the transaction helper. This class tests the transaction helper on database failures against a mocked
''' instance. The real SQLite path is covered by the DAO tests and the transaction tests.
''' </summary>
<TestFixture>
Public Class DataBaseExtensionTests
    Private _dbMock As Mock(Of IDataBaseOperation)

    <SetUp>
    Public Sub Setup()
        _dbMock = New Mock(Of IDataBaseOperation)(MockBehavior.Strict)
        _dbMock.Setup(Function(d) d.ExecuteNonQuery("SAVEPOINT gravo", It.IsAny(Of IEnumerable(Of Object)))).
            Returns(True)
    End Sub

    <Test>
    Public Sub InTransaction_WorkReturns_ReleasesTheSavepoint()
        _dbMock.Setup(Function(d) d.ExecuteNonQuery("RELEASE gravo", It.IsAny(Of IEnumerable(Of Object)))).
            Returns(True)

        _dbMock.Object.InTransaction(Sub() Return)

        _dbMock.Verify(Function(d) d.ExecuteNonQuery("RELEASE gravo", It.IsAny(Of IEnumerable(Of Object))), Times.Once)
    End Sub

    <Test>
    Public Sub InTransaction_WorkThrows_RollsBackToSavepointAndRethrows()
        _dbMock.Setup(Function(d) d.ExecuteNonQuery("ROLLBACK TO gravo", It.IsAny(Of IEnumerable(Of Object)))).
            Returns(True)
        _dbMock.Setup(Function(d) d.ExecuteNonQuery("RELEASE gravo", It.IsAny(Of IEnumerable(Of Object)))).
            Returns(True)
        Dim failure As New InvalidOperationException("disk full")

        Dim act As Action = Sub() _dbMock.Object.InTransaction(Sub() Throw failure)

        act.Should.Throw(Of InvalidOperationException)().Which.Should.BeSameAs(failure)
        _dbMock.Verify(Function(d) d.ExecuteNonQuery("ROLLBACK TO gravo", It.IsAny(Of IEnumerable(Of Object))),
            Times.Once)
        _dbMock.Verify(Function(d) d.ExecuteNonQuery("RELEASE gravo", It.IsAny(Of IEnumerable(Of Object))), Times.Once)
    End Sub

    <Test>
    Public Sub InTransaction_ReleaseFails_RollsBackAndRethrows()
        Dim releaseFailure As New SqliteException("SQLite Error 1: 'cannot release savepoint'.", 1)
        _dbMock.SetupSequence(Function(d) d.ExecuteNonQuery("RELEASE gravo", It.IsAny(Of IEnumerable(Of Object)))).
            Throws(releaseFailure).
            Returns(True)
        _dbMock.Setup(Function(d) d.ExecuteNonQuery("ROLLBACK TO gravo", It.IsAny(Of IEnumerable(Of Object)))).
            Returns(True)

        Dim act As Action = Sub() _dbMock.Object.InTransaction(Sub() Return)

        act.Should.Throw(Of SqliteException)().Which.Should.BeSameAs(releaseFailure)
        _dbMock.Verify(Function(d) d.ExecuteNonQuery("ROLLBACK TO gravo", It.IsAny(Of IEnumerable(Of Object))),
            Times.Once)
        _dbMock.Verify(Function(d) d.ExecuteNonQuery("RELEASE gravo", It.IsAny(Of IEnumerable(Of Object))),
            Times.Exactly(2))
    End Sub

    <Test>
    Public Sub InTransaction_RollbackToFailsAfterAutoRollback_ThrowsTheOriginalError()
        _dbMock.Setup(Function(d) d.ExecuteNonQuery("ROLLBACK TO gravo", It.IsAny(Of IEnumerable(Of Object)))).
            Throws(New SqliteException("SQLite Error 1: 'cannot rollback - no transaction is active'.", 1))
        _dbMock.Setup(Function(d) d.ExecuteNonQuery("RELEASE gravo", It.IsAny(Of IEnumerable(Of Object)))).
            Returns(True)
        Dim failure As New InvalidOperationException("disk full")

        Dim act As Action = Sub() _dbMock.Object.InTransaction(Sub() Throw failure)

        act.Should.Throw(Of InvalidOperationException)().Which.Should.BeSameAs(failure)
        _dbMock.Verify(Function(d) d.ExecuteNonQuery("ROLLBACK TO gravo", It.IsAny(Of IEnumerable(Of Object))),
            Times.Once)
        _dbMock.Verify(Function(d) d.ExecuteNonQuery("RELEASE gravo", It.IsAny(Of IEnumerable(Of Object))), Times.Once)
    End Sub
End Class
