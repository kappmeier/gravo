Imports Gravo2k9.AccessDatabaseOperation

Public Class xlsCard
  Inherits xlsBase

  Dim m_wordNumber As Integer = -1
  Dim m_testInterval As Integer
  Dim m_counter As Integer
  Dim m_lastDate As DateTime
  Dim m_testIntervalMain As Integer
  Dim m_counterMain As Integer

  Public Sub New(ByVal wordNumber As Integer)
    MyBase.New()
    Load(wordNumber)
  End Sub

  Public Sub New(ByVal db As AccessDatabaseOperation, ByVal wordNumber As Integer)
    MyBase.New(db)
    Load(wordNumber)
  End Sub

  Public Sub Load(ByVal wordNumber As Integer)
    Dim command As String = "SELECT [TestInterval], [Counter], [LastDate], [TestIntervalMain], [CounterMain] FROM [Cards] WHERE [Index]=" & wordNumber & ";"
    DBConnection.ExecuteReader(command)
    If Not DBConnection.DBCursor.HasRows Then Throw New xlsExceptionEntryNotFound("Entry " & wordNumber & " not found in global cards-system. If you Expect to have it, try to reorganize the database.")

    m_wordNumber = wordNumber
    DBConnection.DBCursor.Read()
    TestInterval = DBConnection.SecureGetInt32(0)
    Counter = DBConnection.SecureGetInt32(1)
    LastDate = DBConnection.SecureGetDateTime(2)
    TestIntervalMain = DBConnection.SecureGetInt32(3)
    CounterMain = DBConnection.SecureGetInt32(4)
    DBConnection.DBCursor.Close()
  End Sub

  Public Sub Save()
    If m_wordNumber = -1 Then Throw New xlsExceptionCards(2)
    Dim command As String = "UPDATE [Cards] SET [TestInterval] = " & GetDBEntry(TestInterval) & ", [Counter] = " & GetDBEntry(Counter) & ", [LastDate] = " & GetDBEntry(LastDate) & ", [TestIntervalMain] = " & GetDBEntry(TestIntervalMain) & ", [CounterMain] = " & GetDBEntry(CounterMain) & " WHERE [Index]=" & m_wordNumber & ";"
    DBConnection.ExecuteNonQuery(command)
  End Sub

  Public Property TestInterval() As Integer
    Get
      Return m_testInterval
    End Get
    Set(ByVal testInterval As Integer)
      m_testInterval = testInterval
    End Set
  End Property

  Public Property Counter() As Integer
    Get
      Return m_counter
    End Get
    Set(ByVal counter As Integer)
      m_counter = counter
    End Set
  End Property

  Public Property LastDate() As DateTime
    Get
      Return m_lastDate
    End Get
    Set(ByVal lastDate As DateTime)
      m_lastDate = lastDate
    End Set
  End Property

  Public Property TestIntervalMain() As Integer
    Get
      Return m_testIntervalMain
    End Get
    Set(ByVal testIntervalMain As Integer)
      m_testIntervalMain = testIntervalMain
    End Set
  End Property

  Public Property CounterMain() As Integer
    Get
      Return m_counterMain
    End Get
    Set(ByVal counterMain As Integer)
      m_counterMain = counterMain
    End Set
  End Property
End Class
