Module Functions
    Function ValueToString(ByVal Value As Byte, ByVal FillUp As String, ByVal Length As Byte, Optional ByVal AdditionalText As String = "") As String
        Dim i As Byte
        Dim iTextLength As Byte
        Dim iFillUpLength As Byte
        Dim sResult As String

        iTextLength = Len(Trim(Str(Value)))
        iFillUpLength = Length - iTextLength

        If iFillUpLength >= 0 Then
            For i = 0 To iFillUpLength
                sResult = sResult & FillUp
            Next
            sResult = Right(sResult, Len(sResult) - 1)
        End If
        sResult = sResult & Value & AdditionalText

        Return sResult
    End Function
End Module
