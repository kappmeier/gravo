Imports System.Collections.ObjectModel

' Klasse die Eigenschaften der Datenbank-Datei liefert.
' z.B.:
' Datenbank-Version
' Länge der einzelnen Felder
'
' Felder sind ReadOnly Propertys, evtl. mal später editierbar. Aber welchen Sinn hat das?
Public Class xlsDBPropertys
    Inherits xlsBase

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByRef db As DataBaseOperation)
        MyBase.New(db)
    End Sub

    Public ReadOnly Property DatabaseVersion() As String
        Get
            Dim man As New xlsManagement(DBConnection)
            Return man.DatabaseVersionNeeded
        End Get
    End Property

    Public ReadOnly Property DatabaseVersionMain() As Integer
        Get
            Dim db As New SQLiteDataBaseOperation()
            db.Open(DBPath)
            Dim man As New xlsManagement(db)
            Return CStr(Left(man.DatabaseVersion(), 1))
        End Get
    End Property

    Public ReadOnly Property DatabaseVersionSmall() As Integer
        Get
            Dim db As New SQLiteDataBaseOperation()
            db.Open(DBPath)
            Dim man As New xlsManagement(db)
            Return CStr(Right(man.DatabaseVersion(), 2))
        End Get
    End Property

    ' Längen der Felder
    ' DictionaryWords
    Public ReadOnly Property DictionaryWordsMaxLengthWord() As Byte
        Get
            Return 80
        End Get
    End Property

    Public ReadOnly Property DictionaryWordsMaxLengthPre() As Byte
        Get
            Return 16
        End Get
    End Property

    Public ReadOnly Property DictionaryWordsMaxLengthPost() As Byte
        Get
            Return 16
        End Get
    End Property

    Public ReadOnly Property DictionaryWordsMaxLengthMeaning() As Byte
        Get
            Return 80
        End Get
    End Property

    Public ReadOnly Property DictionaryWordsMaxLengthAdditionalTargetLangInfo() As Byte
        Get
            Return 50
        End Get
    End Property

    Public ReadOnly Property DictionaryMainMaxLengthWordEntry() As Byte
        Get
            Return 50
        End Get
    End Property

    Public ReadOnly Property DictionaryMainMaxLengthLanguage() As Byte
        Get
            Return 16
        End Get
    End Property

    Public ReadOnly Property DictionaryMainMaxLengthMainLanguage() As Byte
        Get
            Return 16
        End Get
    End Property

    Public ReadOnly Property DBVersionMaxLengthDescription() As Byte
        Get
            Return 80
        End Get
    End Property

    Public ReadOnly Property GroupsMaxLengthName() As Byte
        Get
            Return 50
        End Get
    End Property

    Public ReadOnly Property GroupsMaxLengthSubName() As Byte
        Get
            Return 50
        End Get
    End Property

    Public ReadOnly Property GroupsMaxLengthTable() As Byte
        Get
            Return 50
        End Get
    End Property

    Public ReadOnly Property GroupMaxLengthExample() As Byte
        Get
            Return 64
        End Get
    End Property

    Public Function GetSupportedWordTypes() As Collection(Of String)
        Dim wordTypes As New Collection(Of String)

        ' Erst möglich, ab version 1.07 der Datenbank
        If (DatabaseVersionMain = 1 And Me.DatabaseVersionSmall >= 7) Or DatabaseVersionMain > 1 Then
            Dim command As String = "SELECT [Type] FROM [SupportedWordTypes] ORDER BY [Index];"
            DBConnection.ExecuteReader(command)
            While DBConnection.DBCursor.Read
                wordTypes.Add(DBConnection.SecureGetString(0))
            End While
            DBConnection.DBCursor.Close()
        End If

        Return wordTypes
    End Function

    Public Function GetWordType(ByVal number As Integer) As String
        Dim command As String = "SELECT [Type] FROM [SupportedWordTypes] WHERE [Index] = " & number & ";"
        DBConnection.ExecuteReader(command)
        If Not DBConnection.DBCursor.HasRows Then Throw New EntryNotFoundException("Type " & number & " not supported")
        DBConnection.DBCursor.Read()
        Dim ret As String = DBConnection.SecureGetString(0)
        DBConnection.DBCursor.Close()
        Return ret
    End Function
End Class
