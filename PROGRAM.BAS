Option Explicit

Sub Main ()
    'Das Programm wird von hier aus gestartet.
    'Zuerst wird der Eröffnungsbildschirm gezeigt.

    'Nachdem der Eröffnungsbildschirm verschwunden ist,
    'wird der Login-Bildschirm angezeigt.
    frmDatei_Login.Show 1
    'Jetzt muß ausgewählt werden,
    'welches Programmodul gestartet werden soll.
    frmAuswahl_Modul.Show 1
End Sub

Function BenutzerErstellen (DirName)
On Error GoTo BenutzerErstellenError
    'Der Benutzer wird angelegt.
    '8-Zeichen-Format
    DirName = Left(DirName, 8)
    'Benutzerverzeichnis erstellen.
    MkDir app.Path + "\" + DirName
    'Benutzerdatei erstellen
    BenutzerErstellen = True
    Exit Function
BenutzerErstellenError:
    BenutzerErstellen = False
    Exit Function
End Function

