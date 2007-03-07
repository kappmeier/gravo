Imports System.Collections.ObjectModel
Imports Gravo2k7.AccessDatabaseOperation

Public Class xlsGroup
  Inherits xlsBase

  Dim groupTableName As String

  Sub New(ByVal GroupTable As String)
    MyBase.new()
    groupTableName = GroupTable
  End Sub

  Public Function GetWords() As Collection(Of String)
    Dim words As New Collection(Of String)
    ' dieser befehl holt alle wörter aus DictionaryMain die in einer gruppe benutzt werden
    'Dim command As String = "SELECT DISTINCT M.WordEntry, D.Word, G.Index FROM DictionaryMain AS M, DictionaryWords AS D, " & AddHighColons(groupTableName) & " AS G WHERE D.Index=G.WordIndex AND M.Index=D.MainIndex ORDER BY G.Index;"
    ' dieser befehl holt alle wörter aus DictionaryWords die in einer gruppe benutzt werden
    ' das G.Index ist nötig damit nach g.Index sortiert werden kann
    Dim command As String = "SELECT D.Word, G.Index FROM DictionaryWords AS D, " & AddHighColons(groupTableName) & " AS G WHERE D.Index=G.WordIndex ORDER BY G.Index;"
    Try
      DBConnection.ExecuteReader(command)
    Catch
      Dim e As xlsExceptionEntryNotFound = New Exception("Es gibt keine Tabelle """ & groupTableName & """")
      Throw e
    End Try
    Do While DBConnection.DBCursor.Read()
      Dim add As String = DBConnection.SecureGetString(0)
      If words.Contains(add) Then
      Else
        words.Add(add)
      End If
    Loop
    DBConnection.DBCursor.Close()
    Return words
  End Function

  Sub Add(ByVal WordIndex As Integer)
    ' TODO Exception, falls GroupTable nicht existiert

    ' nur hinzufügen, wenn noch nicht vorhanden
    Dim command As String = "SELECT WordIndex FROM " & AddHighColons(groupTableName) & " WHERE WordIndex=" & WordIndex & ";"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows Then DBConnection.DBCursor.Close() : Exit Sub Else DBConnection.DBCursor.Close() ' schon ein eintrag vorhanden!

    ' einfügen
    command = "INSERT INTO " & groupTableName & "([WordIndex]) VALUES(" & WordIndex & ");"
    DBConnection.ExecuteReader(command)
  End Sub

  Public Property GroupTable() As String
    Get
      Return groupTableName
    End Get
    Set(ByVal value As String)
      groupTableName = value
    End Set
  End Property

  ' Hohlt alle wörter, bei denen word = word gilt, die auch in der gruppe sind, als komplette dictionaryentrys
  Public Function GetWords(ByVal word As String) As Collection(Of xlsDictionaryEntry)
    Dim dictionaryEntrys As New Collection(Of xlsDictionaryEntry)

    Dim command As String = "Select D.Index FROM DictionaryWords AS D, " & AddHighColons(groupTableName) & " AS G WHERE (((D.Index)=G.WordIndex) AND ((D.Word)='" & AddHighColons(word) & "'));"
    DBConnection.ExecuteReader(command)
    If DBConnection.DBCursor.HasRows = False Then Return dictionaryEntrys ' kein wort entspricht den geforderten angaben
    Dim indices As New Collection(Of Integer)
    Do While DBConnection.DBCursor.Read
      indices.Add(DBConnection.SecureGetInt32(0))
    Loop
    DBConnection.DBCursor.Close()
    Dim wCurrent As xlsDictionaryEntry
    For Each index As Integer In indices
      wCurrent = New xlsDictionaryEntry(DBConnection, index)
      dictionaryEntrys.Add(wCurrent)
    Next
    Return dictionaryEntrys
  End Function

  Public ReadOnly Property WordCount() As Integer
    Get
      Dim command As String = "SELECT COUNT([Index]) FROM " & AddHighColons(groupTableName) & ";"
      DBConnection.ExecuteReader(command)
      DBConnection.DBCursor.Read()
      Dim ret As Integer = DBConnection.SecureGetInt32(0)
      DBConnection.DBCursor.Close()
      Return ret
    End Get
  End Property

  Public ReadOnly Property LanguageCount() As Integer
    Get
      Dim command As String = "SELECT DISTINCT M.LanguageName FROM DictionaryMain AS M, DictionaryWords AS W, " & AddHighColons(groupTableName) & " AS G WHERE G.WordIndex=W.Index AND W.MainIndex=M.Index;"
      DBConnection.ExecuteReader(command)
      Dim count As Integer = 0
      Do While DBConnection.DBCursor.Read
        count += 1
      Loop
      DBConnection.DBCursor.Close()
      Return count
    End Get
  End Property

  Public Function GetUniqueLanguage() As String
    Dim ret As String = ""
    Dim once As Boolean = True
    Dim command As String = "SELECT DISTINCT M.LanguageName FROM DictionaryMain AS M, DictionaryWords AS W, " & AddHighColons(groupTableName) & " AS G WHERE G.WordIndex=W.Index AND W.MainIndex=M.Index;"
    DBConnection.ExecuteReader(command)
    Do While DBConnection.DBCursor.Read
      If ret <> "" Then once = False : Exit Do
      ret = DBConnection.SecureGetString(0)
      If ret = "" Then Throw New xlsException("Illegal language found.")
    Loop
    DBConnection.DBCursor.Close()
    If Not once Then Throw New xlsException("More than one language.")
    Return ret
  End Function

  Public Function GetIndex(ByVal word As String, ByVal meaning As String) As Integer
    Dim command As String = "SELECT G.WordIndex FROM DictionaryWords AS W, " & AddHighColons(groupTableName) & " AS G WHERE G.WordIndex=W.Index AND W.Word='" & AddHighColons(word) & "' AND W.Meaning='" & AddHighColons(meaning) & "';"
    DBConnection.ExecuteReader(command)
    If Not DBConnection.DBCursor.HasRows Then Throw New xlsExceptionEntryNotFound("No Entry for the given word and meaning in the current group.")
    DBConnection.DBCursor.Read()
    Dim index As Integer = DBConnection.SecureGetInt32(0)
    DBConnection.DBCursor.Close()
    Return index
  End Function

  Public Sub Delete(ByVal index As Integer)
    Dim command As String = "DELETE FROM " & AddHighColons(groupTableName) & " WHERE WordIndex=" & index & ";"
    DBConnection.ExecuteNonQuery(command)
  End Sub

  Public Function GetIndices() As Collection(Of Integer)
    If DBConnection Is Nothing Then Throw New xlsException("Datenbank ist nicht verbunden")
    Dim indices As New Collection(Of Integer)
    Dim command As String = "SELECT WordIndex FROM " & AddHighColons(groupTableName) & ";"
    DBConnection.ExecuteReader(command)
    While DBConnection.DBCursor.Read()
      indices.Add(DBConnection.SecureGetInt32(0))
    End While
    DBConnection.CloseReader()
    Return indices
  End Function
End Class
