Imports System.Data.OleDb

Public Class xlsGroupEntry
  Inherits xlsBase

  Dim m_iIndex As Integer = -1
  Dim m_sName As String = ""
  Dim m_sSubName As String = ""
  Dim m_sTable As String = ""

  Sub New(ByVal db As AccessDatabaseOperation, ByVal Index As Integer)
    MyBase.new(db)
    LoadGroup(Index)
  End Sub

  Sub New(ByVal db As AccessDatabaseOperation)
    MyBase.New(db)
  End Sub

  Public Sub LoadGroup(ByVal Index As Integer)
    m_iIndex = Index
    LoadGroup(False)
  End Sub

  Private Sub LoadGroup(ByVal newWord As Boolean)
    If IsConnected() = False Then Throw New Exception("Datenbank nicht verbunden.")

    Dim DBCursor As OleDbDataReader
    Dim sCommand As String = "SELECT GroupName, GroupSubName, GroupTable FROM Groups WHERE Index = " & GroupIndex & ";"
    DBCursor = DBConnection.ExecuteReader(sCommand)
    If DBCursor.HasRows = False Then
      Throw New Exception("Eintrag mit Index " & GroupIndex & " nicht vorhanden.")
    End If
    DBCursor.Read()
    m_sName = SecureGetString(DBCursor, 0)
    m_sSubName = SecureGetString(DBCursor, 1)
    m_sTable = SecureGetString(DBCursor, 2)
  End Sub

  Public Property GroupIndex() As Integer
    Set(ByVal newIndex As Integer)
      m_iIndex = newIndex
    End Set
    Get
      Return m_iIndex
    End Get
  End Property

  Public Property Group() As String
    Set(ByVal newName As String)
      m_sName = newName
    End Set
    Get
      Return m_sName
    End Get
  End Property

  Public Property SubGroup() As String
    Set(ByVal newName As String)
      m_sSubName = newName
    End Set
    Get
      Return m_sSubName
    End Get
  End Property

  Public Property Table() As String
    Set(ByVal newTable As String)
      m_sTable = newTable
    End Set
    Get
      Return m_sTable
    End Get
  End Property
End Class