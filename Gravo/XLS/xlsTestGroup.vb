Imports System.Collections.ObjectModel
Imports Gravo.AccessDatabaseOperation

Public Class xlsTestGroup
    Inherits xlsTestBase

    Dim group As String
    ' Abfragen von Vokabeln
    ' unterstützt (nur) Gruppen

    Private m_testMarked As Boolean = False       ' sollen nur markierte Wörter abgefragt werden?

    ' Finde alle Wörter, die zu dieser Sprache passen heraus
    Overrides Sub Start(ByVal Group As String)
        Me.group = Group
        If IsConnected() = False Then Throw New Exception("Database not connected.")
        Dim words As Collection(Of Integer) = New Collection(Of Integer)
        Dim command As String = ""
        If TestSetPhrases And TestMarked Then ' nur markierte und Phrasen einschließen
            command = "SELECT W.[Index] FROM DictionaryWords AS W, [" & EscapeSingleQuotes(Group) & "] AS G WHERE(((W.[Index]) = G.[WordIndex]) AND ((G.Marked) = True))"
        ElseIf Not TestSetPhrases And TestMarked Then ' nur markierte und Phrasen ausschließen
            command = "SELECT W.[Index] FROM DictionaryWords AS W, [" & EscapeSingleQuotes(Group) & "] AS G WHERE W.[Index] = G.WordIndex AND G.Marked = True AND (NOT W.WordType=5);"
        ElseIf TestSetPhrases And Not TestMarked Then ' alle auswählen und Phrasen einschließen
            command = "SELECT W.[Index] FROM DictionaryWords AS W, [" & EscapeSingleQuotes(Group) & "] AS G WHERE W.[Index] = G.WordIndex;"
        Else  ' alle auswählen und Phrasen ausschließen
            command = "SELECT W.[Index] FROM [DictionaryWords] AS W, [" & EscapeSingleQuotes(Group) & "] AS G WHERE W.[Index] = G.WordIndex AND  W.WordType <> 5"
        End If
        DBConnection.ExecuteReader(command)
        Do While DBConnection.DBCursor.Read
            words.Add(DBConnection.SecureGetInt32(0))
        Loop
        DBConnection.DBCursor.Close()
        Start(words)
    End Sub

    Public Property TestMarked() As Boolean
        Get
            Return m_testMarked
        End Get
        Set(ByVal value As Boolean)
            m_testMarked = value
        End Set
    End Property


    Public Overrides Function TestControl(ByVal input As String) As TestResult
        ' Ist abgeleitet, einige Sachen sind vermischt mit der Basismethode...
        ' kompliziert das Programmieren vielleichty
        Dim res As TestResult = MyBase.TestControl(input)

        ' TODO zweites Cards-System auch bei Next-Word einbinden

        ' First-Test muß neu belegt werden, genauso wie in Basismethode
        firstTest = IIf(firstRun, True, False)

        ' Update des Gruppen-Cards-Systems, falls nötig
        If UseCards And firstTest Then
            Dim cards As New xlsCards(TestFormerLanguage, DBConnection)
            If res = TestResult.NoError Then
                ' Update der Group-Cards-Einstellungen
                cards.Update(group, TestDictionaryEntry.WordIndex, True)
                firstTest = False
            ElseIf res = TestResult.Wrong Then
                ' Update der Group-Cards-Einstellungen
                cards.Update(group, TestDictionaryEntry.WordIndex, False)
                firstTest = False
            End If
        End If

        Return res
    End Function




End Class
