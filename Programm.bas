Attribute VB_Name = "Programm"
'Author: Jan-Philipp Kappmeier
'Geändert: 2000-02-09

Public Function Fehler(ByVal sErrName As String, _
        ByVal sErrP As String, ByVal iVal As Integer) As Boolean
On Error GoTo Programm_Fehler_Error
    MsgBox "Fehler " & iVal & " trat in" & vbCrLf _
            & sErrP & " auf." & vbCrLf & vbCrLf _
            & sErrName, vbCritical, "Sprachtrainer 2000 - Fehler"
    Fehler = True
Exit Function
Programm_Fehler_Error:
    MsgBox "Totalabsturz", vbCritical, _
            "Letzte Warnung": End
End Function
