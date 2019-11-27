Imports System.Collections.ObjectModel
Imports Gravo.AccessDatabaseOperation

Public Structure xlsWordAndMainIndex
    Dim Word As String
    Dim MainIndex As Integer
End Structure

Public Class xlsDictionary
    Inherits xlsBase
    ' Grundlegende Informationen über die Datenbank und die vorhandenen Vokabeln:
    ' Version
    ' Änderungen
    ' Vokabelanzahl
    ' Vokabelinfo
    ' Benutzer
    ' Gruppen

    ' Standardkonstruktor
    Sub New()
        MyBase.New()
    End Sub

    Sub New(ByVal db As IDataBaseOperation)    ' Keinen Speziellen Table auswählen
        MyBase.New(db)
    End Sub

    ' Alle Sprachen die es im Dictionary gibt, anzeigen
    Public Function DictionaryEntrys(ByVal Language As String, ByVal MainLanguage As String) As Collection(Of String)
        Dim words As New Collection(Of String)
        Dim command As String = "SELECT WordEntry FROM DictionaryMain WHERE LanguageName=" & GetDBEntry(Language) & " AND MainLanguage=" & GetDBEntry(MainLanguage) & " ORDER BY WordEntry;"
        DBConnection.ExecuteReader(command)
        Do While DBConnection.DBCursor.Read()
            words.Add(DBConnection.SecureGetString(0))
        Loop
        DBConnection.DBCursor.Close()
        Return words
    End Function

    ' Already in DictionaryDao
    Function GetEntryIndex(ByVal MainEntry As String, ByVal Language As String, ByVal MainLanguage As String) As Integer
        ' Check, ob die Sprache vorhanden ist
        Dim command As String = "SELECT DISTINCT LanguageName FROM DictionaryMain WHERE LanguageName=" & GetDBEntry(Language)
        DBConnection.ExecuteReader(command)
        If DBConnection.DBCursor.HasRows = False Then Throw New LanguageNotFoundException("Sprache " & Language & " nicht vorhanden.")

        ' Check, ob die Main-Sprache vorhanden ist
        command = "SELECT DISTINCT LanguageName FROM DictionaryMain WHERE MainLanguage=" & GetDBEntry(MainLanguage)
        DBConnection.ExecuteReader(command)
        If DBConnection.DBCursor.HasRows = False Then Throw New LanguageNotFoundException("Hauptsprache " & MainLanguage & " nicht vorhanden.")

        ' Index herausfinden
        command = "SELECT [Index] FROM DictionaryMain WHERE WordEntry=" & GetDBEntry(MainEntry) & " AND LanguageName = " & GetDBEntry(Language) & " AND MainLanguage=" & GetDBEntry(MainLanguage)
        DBConnection.ExecuteReader(command)
        If DBConnection.DBCursor.HasRows = False Then Throw New EntryNotFoundException("Kein Haupteintrag " & MainEntry & " zur gewählten Sprache vorhanden.")
        DBConnection.DBCursor.Read()
        Dim ret As Int32 = DBConnection.SecureGetInt32(0)
        DBConnection.DBCursor.Close()
        Return ret
    End Function

    ' Already in IDictionaryDao
    Public Sub AddEntry(ByVal Word As String, ByVal Language As String, ByVal MainLanguage As String)
        If Word = "" Then Throw New xlsExceptionInput(1)
        If Language = "" Then Throw New xlsExceptionInput(3)
        Try
            GetEntryIndex(Word, Language, MainLanguage)
        Catch ex As EntryNotFoundException
            ' Eintrag nicht gefunden, kann also hinzugefügt werden
            Dim command As String = "INSERT INTO DictionaryMain (WordEntry, LanguageName, MainLanguage) VALUES(" & GetDBEntry(Word) & ", " & GetDBEntry(Language) & ", " & GetDBEntry(MainLanguage) & ");"
            DBConnection.ExecuteNonQuery(command)
            Exit Sub
        Catch ex As LanguageNotFoundException
            ' Sprache nicht vorhanden! kann also auf jeden fall eingefügt werden
            Dim command As String = "INSERT INTO DictionaryMain (WordEntry, LanguageName, MainLanguage) VALUES(" & GetDBEntry(Word) & ", " & GetDBEntry(Language) & ", " & GetDBEntry(MainLanguage) & ");"
            DBConnection.ExecuteNonQuery(command)
            Exit Sub
        Catch ex As Exception
            ' Etwas anderes ist schiefgegangen. Weiterleiten
            Throw ex
        End Try
        ' Nichts schiefgegangen, das heißt es gibt ein Wort mit diesem Index
        Throw New EntryExistsException("Es existiert bereits ein Wort unter diesem Eintrag.")
    End Sub

    Public Function GetMaxEntryIndex() As Integer
        Return GetMaxIndex("DictionaryMain")
    End Function

    Public Function GetMaxSubEntryIndex() As Integer
        Return GetMaxIndex("DictionaryWords")
    End Function

    ' as GetMainEntry in dictionarydao
    Public Function GetEntryName(ByVal Index As Integer) As String
        Dim command As String = "SELECT WordEntry FROM DictionaryMain WHERE [Index] = " & Index & ";"
        DBConnection.ExecuteReader(command)
        If DBConnection.DBCursor.HasRows = False Then Throw New EntryNotFoundException("Der Eintrag existiert nicht.")
        DBConnection.DBCursor.Read()
        Dim ret As String = DBConnection.SecureGetString(0)
        DBConnection.DBCursor.Close()
        Return ret
    End Function

    Public Function GetSubEntry(ByVal index As Integer) As xlsDictionaryEntry
        Dim a As New xlsDictionaryEntry(DBConnection, index)
        Return a
    End Function

    Public Function GetEntryLanguage(ByVal MainIndex As Integer) As String
        Dim command As String = "SELECT LanguageName FROM DictionaryMain WHERE [Index] = " & MainIndex
        DBConnection.ExecuteReader(command)
        If DBConnection.DBCursor.HasRows = False Then Throw New EntryNotFoundException("The Entry with index " & MainIndex & " does not exist.")
        DBConnection.DBCursor.Read()
        Dim ret As String = DBConnection.SecureGetString(0)
        DBConnection.DBCursor.Close()
        Return ret
    End Function

    Public Function GetEntryMainLanguage(ByVal Index As Integer) As String
        Dim command As String = "SELECT MainLanguage FROM DictionaryMain WHERE [Index] = " & Index
        DBConnection.ExecuteReader(command)
        If DBConnection.DBCursor.HasRows = False Then Throw New EntryNotFoundException("Der Eintrag existiert nicht.")
        DBConnection.DBCursor.Read()
        Dim ret As String = DBConnection.SecureGetString(0)
        DBConnection.DBCursor.Close()
        Return ret
    End Function

    ' Already in IDictionaryDao
    Public Function GetSubEntryIndex(ByVal MainIndex As Integer, ByVal Word As String, ByVal Meaning As String) As Integer
        Dim command As String = "SELECT [Index] FROM DictionaryWords WHERE Word=" & GetDBEntry(Word) & " AND Meaning=" & GetDBEntry(Meaning) & " AND MainIndex=" & MainIndex
        DBConnection.ExecuteReader(command)
        If DBConnection.DBCursor.HasRows = False Then Throw New EntryNotFoundException("Der Eintrag existiert nicht.")
        DBConnection.DBCursor.Read()
        Dim ret As String = DBConnection.SecureGetInt32(0)
        DBConnection.DBCursor.Close()
        Return ret
    End Function

End Class
