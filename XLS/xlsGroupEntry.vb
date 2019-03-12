Imports Gravo.AccessDatabaseOperation

Public Class xlsGroupEntry
  Inherits xlsBase

  Dim m_index As Integer = -1
  Dim m_sName As String = ""
  Dim m_sSubName As String = ""
  Dim m_sTable As String = ""

    Sub New(ByVal db As DataBaseOperation, ByVal Index As Integer)
        MyBase.New(db)
        LoadGroup(Index)
    End Sub

    Sub New(ByVal db As DataBaseOperation)
        MyBase.New(db)
    End Sub

    Public Sub LoadGroup(ByVal Index As Integer)
    m_index = Index
    LoadGroup(False)
  End Sub

  Private Sub LoadGroup(ByVal newWord As Boolean)
    ' wird immer mit false aufgerufen
    If IsConnected() = False Then Throw New Exception("Datenbank nicht verbunden.")

        Dim command As String = "SELECT GroupName, GroupSubName, GroupTable FROM Groups WHERE [Index] = " & GroupIndex & ";"
        DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then
      Throw New Exception("Eintrag mit Index " & GroupIndex & " nicht vorhanden.")
    End If
    DBConnection.DBCursor.Read()
    m_sName = DBConnection.SecureGetString(0)
    m_sSubName = DBConnection.SecureGetString(1)
    m_sTable = DBConnection.SecureGetString(2)
  End Sub

  Public Property GroupIndex() As Integer
    Set(ByVal newIndex As Integer)
      m_index = newIndex
    End Set
    Get
      Return m_index
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