Module OldCode

    'Overridable Function CreateStats(ByVal TableName As String)
    '    Dim oleCursor As OleDbDataReader

    '    Dim iCountWords As Integer

    '    oledbCmd.CommandText = "SELECT COUNT(*) FROM " & TableName & ";"
    '    oleCursor = oledbCmd.ExecuteReader
    '    oleCursor.Read()
    '    If TypeOf (oleCursor.GetValue(0)) Is DBNull Then iCountWords = 0 Else iCountWords = oleCursor.GetValue(0)
    '    oleCursor.Close()


    '    Dim i As Integer
    '    Dim sStatsTableName = TableName & "Stats"

    '    For i = 1 To iCountWords
    '        Dim sInput As String
    '        sInput = "INSERT INTO " & sStatsTableName & " VALUES ("
    '        sInput += AddHighColons(i) & ","
    '        sInput += AddHighColons(0) & ","
    '        sInput += AddHighColons(0) & ","
    '        sInput += AddHighColons(0) & ","
    '        sInput += AddHighColons(0) & ","
    '        sInput += AddHighColons(0) & ","
    '        sInput += "'" & AddHighColons("01.01.1900") & "',"
    '        sInput += "'" & AddHighColons("01.01.1900") & "',"
    '        sInput += AddHighColons(False) & ");"
    '        oledbCmd.CommandText = sInput
    '        oledbCmd.ExecuteNonQuery()
    '    Next
    'End Function

    'Protected Function EnglishTestControl(Optional ByVal Word As String = "", Optional ByVal Meaning1 As String = "", Optional ByVal Meaning2 As String = "", Optional ByVal Meaning3 As String = "", Optional ByVal Irregular1 As String = "", Optional ByVal Irregular2 As String = "", Optional ByVal Irregular3 As String = "") As Boolean
    '    If bConnected = False Then Exit Function
    '    If m_bTestMode = False Then Exit Function

    '    Dim bRight As Boolean = True

    '    If m_bWordToMeaning Then
    '        Select Case m_iWordType
    '            Case 0

    '        End Select
    '    Else
    '        If Word <> m_sMeaning1 Then bRight = False
    '        Select Case m_iWordType
    '            Case 0      ' Substantiv

    '            Case 1      ' Verb
    '                If (Irregular1 <> m_sIrregular1) Or (Irregular2 <> m_sIrregular2) Then bRight = False
    '            Case 2      ' Adjektiv

    '        End Select
    '    End If
    '    Return bRight
    'End Function

    'Protected Function LatinTestControl(Optional ByVal Word As String = "", Optional ByVal Meaning1 As String = "", Optional ByVal Meaning2 As String = "", Optional ByVal Meaning3 As String = "", Optional ByVal Irregular1 As String = "", Optional ByVal Irregular2 As String = "", Optional ByVal Irregular3 As String = "") As Boolean
    '    If bConnected = False Then Exit Function
    '    If m_bTestMode = False Then Exit Function

    '    Dim bRight As Boolean = False
    '    Dim sIrregular As String

    '    If m_bWordToMeaning Then
    '        If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning3) Then bRight = True
    '        If (Meaning1 = m_sMeaning1) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning2) Then bRight = True
    '        If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning3) Then bRight = True
    '        If (Meaning1 = m_sMeaning2) And (Meaning2 = m_sMeaning3) And (Meaning3 = m_sMeaning1) Then bRight = True
    '        If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning2) And (Meaning3 = m_sMeaning1) Then bRight = True
    '        If (Meaning1 = m_sMeaning3) And (Meaning2 = m_sMeaning1) And (Meaning3 = m_sMeaning2) Then bRight = True
    '        Select Case m_iWordType
    '            Case 0      ' Substantiv

    '            Case 1      ' Verb

    '            Case 2      ' Adjektiv

    '        End Select
    '    Else
    '        If Word <> m_sWord Then bRight = False
    '        Select Case m_iWordType
    '            Case 0      ' Substantiv

    '            Case 1      ' Verb

    '            Case 2      ' Adjektiv

    '        End Select
    '    End If
    '    Return bRight
    'End Function

    'Protected Function FranceTestControl(Optional ByVal Word As String = "", Optional ByVal Meaning1 As String = "", Optional ByVal Meaning2 As String = "", Optional ByVal Meaning3 As String = "", Optional ByVal Irregular1 As String = "", Optional ByVal Irregular2 As String = "", Optional ByVal Irregular3 As String = "") As Boolean
    '    If bConnected = False Then Exit Function
    '    If m_bTestMode = False Then Exit Function

    '    Dim bRight As Boolean = True
    '    Dim sIrregular As String

    '    If m_bWordToMeaning Then
    '        Select Case m_iWordType
    '            Case 0

    '        End Select
    '    Else
    '        If Meaning1 <> m_sWord Then bRight = False
    '        Select Case m_iWordType
    '            Case 0      ' Substantiv
    '                If (m_sPre = "la") Or (m_sPre = "une") Then
    '                    sIrregular = "f"
    '                ElseIf (m_sPre = "le") Or (m_sPre = "un") Then
    '                    sIrregular = "m"
    '                Else
    '                    sIrregular = m_sIrregular1
    '                End If
    '                If Irregular1 <> sIrregular Then bRight = False
    '            Case 1      ' Verb

    '            Case 2      ' Adjektiv
    '                If m_bIrregularForm Then
    '                    sIrregular = m_sIrregular1
    '                Else
    '                    sIrregular = m_sWord & "e"
    '                End If
    '                If Irregular1 <> sIrregular Then bRight = False
    '        End Select
    '    End If
    '    Return bRight
    'End Function

    'Protected Shared Function bDBCommandReader(ByVal DBPath As String, ByVal CommandText As String) As OleDbDataReader
    '    Dim oledbCmd As OleDbCommand = New OleDbCommand()
    '    Dim oledbConnect As OleDbConnection = New OleDbConnection()
    '    Dim oleCursor As OleDbDataReader

    '    oledbConnect.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=" & DBPath
    '    oledbConnect.Open()

    '    oledbCmd.Connection = oledbConnect
    '    oledbCmd.CommandText = CommandText
    '    oleCursor = oledbCmd.ExecuteReader()

    '    'oledbConnect.Close()

    '    Return oleCursor
    'End Function

    'Sub OpenSaveDatabase(ByVal Path As String)
    '    ' Wenn Datenbankverbindung offen, dann schließen, auf TRUE lassen, da sofort neue geöffnet wird
    '    DBSaveConnection.Open(Path)
    '    sSaveDBPath = Path
    '    bConnectedSaveDB = True
    'End Sub

    'Sub CloseSaveDatabase()
    '    DBSaveConnection.Close()
    '    sSaveDBPath = ""
    '    bConnectedSaveDB = False
    'End Sub
End Module
