Imports AwesomeAssertions
Imports Gravo
Imports Microsoft.Data.Sqlite
Imports Moq
Imports NUnit.Framework

''' <summary>
''' Tests the transaction helper. This class tests the transaction helper on database failures against a mocked
''' instance. The real SQLite path is covered by the DAO tests.
''' </summary>
<TestFixture>
Public Class DataBaseExtensionTests
    Private _dbMock As Mock(Of IDataBaseOperation)

    <SetUp>
    Public Sub Setup()
        _dbMock = New Mock(Of IDataBaseOperation)(MockBehavior.Strict)
        _dbMock.Setup(Function(d) d.ExecuteNonQuery("BEGIN", It.IsAny(Of IEnumerable(Of Object)))).Returns(True)
    End Sub

    <Test>
    Public Sub InTransaction_WorkThrows_RollsBackAndRethrows()
        _dbMock.Setup(Function(d) d.ExecuteNonQuery("ROLLBACK", It.IsAny(Of IEnumerable(Of Object)))).Returns(True)
        Dim failure As New InvalidOperationException("disk full")

        Dim act As Action = Sub() _dbMock.Object.InTransaction(Sub() Throw failure)

        act.Should.Throw(Of InvalidOperationException)().Which.Should.BeSameAs(failure)
        _dbMock.Verify(Function(d) d.ExecuteNonQuery("ROLLBACK", It.IsAny(Of IEnumerable(Of Object))), Times.Once)
    End Sub

    <Test>
    Public Sub InTransaction_RollbackFailsAfterAutoRollback_ThrowsTheOriginalError()
        _dbMock.Setup(Function(d) d.ExecuteNonQuery("ROLLBACK", It.IsAny(Of IEnumerable(Of Object)))).
            Throws(New SqliteException("SQLite Error 1: 'cannot rollback - no transaction is active'.", 1))
        Dim failure As New InvalidOperationException("disk full")

        Dim act As Action = Sub() _dbMock.Object.InTransaction(Sub() Throw failure)

        act.Should.Throw(Of InvalidOperationException)().Which.Should.BeSameAs(failure)
    End Sub
End Class
