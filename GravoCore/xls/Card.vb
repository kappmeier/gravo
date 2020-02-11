Public Class Card
    Private ReadOnly m_testInterval As Integer
    Private ReadOnly m_counter As Integer
    Private ReadOnly m_lastDate As Date
    Private ReadOnly m_testIntervalMain As Integer
    Private ReadOnly m_counterMain As Integer

    Public Sub New(testInterval As Integer, counter As Integer, lastDate As Date, testIntervalMain As Integer, counterMain As Integer)
        Me.m_testInterval = testInterval
        Me.m_counter = counter
        Me.m_lastDate = lastDate
        Me.m_testIntervalMain = testIntervalMain
        Me.m_counterMain = counterMain
    End Sub

    Public ReadOnly Property TestInterval As Integer
        Get
            Return m_testInterval
        End Get
    End Property

    Public ReadOnly Property Counter As Integer
        Get
            Return m_counter
        End Get
    End Property

    Public ReadOnly Property LastDate As Date
        Get
            Return m_lastDate
        End Get
    End Property

    Public ReadOnly Property TestIntervalMain As Integer
        Get
            Return m_testIntervalMain
        End Get
    End Property

    Public ReadOnly Property CounterMain As Integer
        Get
            Return m_counterMain
        End Get
    End Property
End Class
