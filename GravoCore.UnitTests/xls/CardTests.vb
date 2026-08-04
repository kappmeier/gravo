Imports Gravo
Imports NUnit.Framework
Imports FluentAssertions

''' <summary>
''' Tests the GravoCore/xls/Card.vb class. Simple constructor/property test
''' for a plain immutable data holder.
''' </summary>
<TestFixture>
Public Class CardTests

    <Test>
    Public Sub Constructor_RoundTripsAllFields()
        Dim lastDate As Date = New Date(2020, 1, 15)
        Dim card As New Card(3, 7, lastDate, 5, 2)

        card.TestInterval.Should.Be(3)
        card.Counter.Should.Be(7)
        card.LastDate.Should.Be(lastDate)
        card.TestIntervalMain.Should.Be(5)
        card.CounterMain.Should.Be(2)
    End Sub
End Class
