Imports System.Data.OleDb

Public Class xlsCollection
	Inherits xlsBase

	Protected m_cList As Collection
	Protected m_iCount As Integer

	Public Sub New(ByVal db As CDBOperation)
		MyBase.New(db)
		m_cList = New Collection
	End Sub

	Public Sub Add(ByVal Value As Object)
		m_cList.Add(Value)
	End Sub

	Public Sub Clear()
		m_cList = Nothing
		m_cList = New Collection
	End Sub

	Public ReadOnly Property Count() As Integer
		Get
			Return m_cList.Count
		End Get
	End Property

	Default Public ReadOnly Property Item(ByVal Index As Integer) As Object
		Get
			If Index > m_cList.Count Then Return Nothing
			Return m_cList(Index)
		End Get
	End Property
End Class
