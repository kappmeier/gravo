Imports Gravo
Imports Moq
Imports NUnit.Framework
Imports FluentAssertions

''' <summary>
''' Tests the current behavior of `DictionaryDao.GetWords(mainLanguage)`, which is a partial implementation.
''' It builds a SQL command string but never executes it and never returns a value, so it always yields Nothing.
''' Implementation is still WiP, the test can be moved to the correct folder when the function is implemented.
''' </summary>
<TestFixture>
Public Class DictionaryDaoDeadOverloadTests
    <Test>
    Public Sub GetWords_MainLanguageOverload_ReturnsNothingWithoutExecutingSql()
        Dim dbMock As New Mock(Of IDataBaseOperation)(MockBehavior.Strict)
        Dim dao As New DictionaryDao(dbMock.Object)

        Dim result As ICollection(Of WordEntry) = dao.GetWords("german")

        result.Should.BeNull()
    End Sub
End Class
