Option Explicit
'Deklarationen der API-Funktionen

'Deklarationen der Konstanten
Global Const const_Fehlermeldung1 = "meldet fehler."
Global Const const_Fehlermeldung2 = "Bitte wenden sie sich an ihren Fachhändler"
Global Const const_ProgrammName = "VokabelTrainer 2.0"

'Deklarationen der Standard-Variablen
Global a
Global b
Global c
Global i
Global x
Global y
Global z
Global Text$
Global Wert
Global Ergebnis
Global M$
Global T$
Global ProzedurName$
Global ProzedurTyp$
Global FehlerNummer
Global FehlerMeldungsText$
Global SatzNr
Global SatzAnzahl&
Global DNr%
Global Zeile

'Deklarationen der öffentlichen Variablen
Global pv_BuchStufe%
Global pv_Lektion%
Global pv_Vokabel%

'Deklarationen der privaten Variablen

'Deklarationen der Benutzerdefinierten Typen
Type VokabelVerarbeitungsDaten
    VokabelFalsch(1 To 1000) As Integer
    VokabelBuch(1 To 1000) As Integer
    VokabelLektion(1 To 1000) As Integer
    VokabelNummer(1 To 1000) As Integer
    Aktiv As Integer
End Type


Type VokabelDateiDaten
    VokabelName As String * 45
    Deutsch1 As String * 45
    Deutsch2 As String * 45
    Deutsch3 As String * 45
    Grammatik1 As String * 45
    Grammatik2 As String * 45
    Grammatik3 As String * 45
    Art As Integer
    VokabelNummer As Integer
    VokabelInfo As Integer
End Type

'Deklarationen der Benutzerdefinierten Typ-Variablen
Global VokabelDaten As VokabelDateiDaten
Global Vokabel As VokabelVerarbeitungsDaten

