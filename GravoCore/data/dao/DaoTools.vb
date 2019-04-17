Public Module DaoTools

    Public Function GetDBEntry(ByVal text As String) As String
        Return "'" & EscapeSingleQuotes(text) & "'"
    End Function

    Public Function GetDBEntry(ByVal value As Integer) As String
        Return "'" & value.ToString & "'"
    End Function

    Public Function GetDBEntry(ByVal value As Boolean) As String
        Return "'" & BooleanToString(value) & "'"
    End Function

    Public Function BooleanToString(ByVal Value As Boolean) As String
        Return (If(Value, "-1", "0"))
    End Function

    ''' <summary>
    ''' Escapes all single quotes by replacing them with two single quotes.
    ''' </summary>
    ''' <param name="Text">The string to be escaped.</param>
    ''' <returns></returns>
    Public Function EscapeSingleQuotes(ByVal Text As String) As String
        Dim sTemp, sTemp2 As String
        Dim i As Integer = 0
        sTemp2 = Text
        sTemp = ""
        Do
            i = sTemp2.IndexOf("'")

            If i >= 0 Then
                sTemp = sTemp & sTemp2.Substring(0, i + 1) & "'"
                sTemp2 = sTemp2.Substring(i + 1)
            Else
                sTemp = sTemp & sTemp2
                sTemp2 = ""
            End If
        Loop Until sTemp2 = ""
        Return sTemp
    End Function

    ''' <summary>
    ''' Strips forbidden characters from a string that are not valid as SQL table names.
    ''' Removes space and question mark (" ", !).
    ''' </summary>
    ''' <param name="input">The input string.</param>
    ''' <returns>The original string without the special characters.</returns>
    Public Function StripSpecialCharacters(ByVal input As String) As String
        Dim withoutSpecialCharacters As String = input
        withoutSpecialCharacters = withoutSpecialCharacters.Replace(" ", "")
        withoutSpecialCharacters = withoutSpecialCharacters.Replace("!", "")
        Return withoutSpecialCharacters
    End Function

End Module
