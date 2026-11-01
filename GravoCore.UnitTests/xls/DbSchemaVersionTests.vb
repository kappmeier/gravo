Imports AwesomeAssertions
Imports Gravo
Imports NUnit.Framework

<TestFixture>
Public Class DbSchemaVersionTests

    <Test>
    Public Sub ToString_ReturnsMajorDotMinor()
        Dim fixture As New Properties.DbSchemaVersion(1, 7, New Date(2007, 5, 29), "Group features")

        fixture.ToString().Should.Be("1.7")
    End Sub
End Class
