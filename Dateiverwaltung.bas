Attribute VB_Name = "Dateiverwaltung"
'Author: Jan-Philipp Kappmeier
'Geändert: 2000-02-09   i
                        'ErrorDescription
                        'ERROR_MOD_DATEIVERW
                        '
                        'Nicht benutzte
                        '
                        'ERROR_UNKNOWN
'Geändert: 2000-02-09   DNR_INFO
                        'DNR_VOKABEL
                        'TDateiInfo
                        'TDateiVokabel
                        'DatenVokabel
                        'DatenInfo

Public Const DNR_INFO = 2
Public Const DNR_VOKABEL = 1

Type TDateiInfo
    iAnzahl(0 To 99) As Integer 'Anzahl an Vokabel in der Lektion
    sName(0 To 99) As String * 25   'Name der Lektion
End Type

Type TDateiVokabel
    sVokabel As String * 45
    sBedeutung1 As String * 45
    sBedeutung2 As String * 45
    sBedeutung3 As String * 45
    sGrammatik1 As String * 45
    sGrammatik2 As String * 45
    sGrammatik3 As String * 45
    iArt As Integer
    iVokabel As Integer
    iVokabelInfo As Long
    iLektion As Integer
End Type

Public VokabelDatei As TDateiVokabel
Public InfoDatei As TDateiInfo

Public i As Integer
Public ErrorDescription As String

Public Const ERROR_MOD_DATEIVW = "Fehler in Modul" _
                & " Dateiverwaltung bei CreateUserDir" _
                & vbCrLf & "Es trat folgender Fehler" _
                & "auf: " & vbCrLf & vbCrLf
Public Const ERROR_UNKNOWN = "Unbekannter Fehler"


Public Function CreateUserDir(ByVal UserPath As String) As Integer
'Author: Jan-Philipp Kappmeier
'Geändert: 2000-02-09   Verzeichnis anlegen
On Error GoTo Dateiverwaltung_CreateUserDir_Error
    'Benutzerpfad anlegen
    MkDir UserPath
    CreateUserDir = 0
    Exit Function
Dateiverwaltung_CreateUserDir_Error:
    ErrorDescription = ERROR_MOD_DATEIVW & Err.Description & _
        vbCrLf
    CreateUserDir = Err
End Function

Public Function Default() As Integer
'Author: Jan-Philipp Kappmeier
'Geändert: 2000-02-10   'Defaultprozedur
On Error GoTo Modul_Default_Error

Exit Function
Modul_Default_Error:
    ErrorDescription = ERROR_MOD_DATEIVW & Err.Description & _
        vbCrLf
    Default = Err
End Function


