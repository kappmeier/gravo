Public Class xlsWordStats
	Inherits xlsWord


	Sub New(ByVal db As CDBOperation)
		MyBase.New(db)
	End Sub

	Sub New(ByVal db As CDBOperation, ByVal WordNumber As Integer, ByVal Table As String)
		MyBase.New(db, WordNumber, Table)
	End Sub

	Protected Overloads Sub LoadWord()
		MyBase.LoadWord()
		If m_bvalid = False Then Exit Sub

		Exit Sub
		'DBCommand = "SELECT Abfragen, AbfragenGesamt, Richtig, Falsch, FalschGesamt, AbfrageGestartet, ErsteAbfrage, LetzteAbfrage FROM " & m_sTable & "Stats WHERE WordNumber=" & m_iWordNumber & ";"
		Try
			dbconnection.ReOpen()

			DBCommand = "SELECT LetzteAbfrage FROM " & m_sTable & "Stats WHERE WordNumber=" & m_iWordNumber & ";"
			Application.DoEvents()
			DBCursor = DBConnection.ExecuteReader(dbCommand)
			DBCursor.Read()
			'If TypeOf (dbCursor.GetValue(0)) Is DBNull Thenm_sLastTested = "01.01.1900" Else 
			m_sLastTested = dbCursor.GetValue(0)
		Catch ex As Exception
			MsgBox(ex.Message & vbCrLf & Err.Number)
		End Try
	End Sub

	Public Overloads Sub LoadWord(ByVal WordNumber As Integer)
		m_iWordNumber = WordNumber
		LoadWord()
	End Sub

	Public Overloads Sub LoadWord(ByVal WordNumber As Integer, ByVal Table As String)
		m_stable = Table
		m_btableselected = True
		LoadWord(WordNumber)
	End Sub

	Property LastTested() As String
		Get
			Return m_sLastTested
		End Get
		Set(ByVal LastTested As String)
			DBCommand = "UPDATE " & m_sTable & "Stats SET  LetzteAbfrage='" & LastTested & "' WHERE WordNumber=" & m_iWordNumber & ";"
			DBConnection.ExecuteNonQuery(DBCommand)
		End Set
	End Property
End Class
