Imports System.Data.OleDb

Public Interface DataBaseOperation
    Function Open(ByVal DBPath As String) As Boolean

    Function Close() As Boolean

    Function ExecuteNonQuery(ByVal CommandText As String) As Boolean

    Function ExecuteReader(ByVal DBPath As String, ByVal CommandText As String) As OleDbDataReader

    Function ExecuteReader(ByVal CommandText As String) As OleDbDataReader

    Function DBCursor() As OleDbDataReader

    Sub CloseReader()

    Function SecureGetBool(ByVal Index As Integer) As Boolean

    Function SecureGetInt32(ByVal Index As Integer) As Integer

    Function SecureGetString(ByVal Index As Integer) As String

    Function SecureGetDateTime(ByVal Index As Integer) As DateTime
End Interface
