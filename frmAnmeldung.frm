VERSION 5.00
Begin VB.Form Anmeldung 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Form1"
   ClientHeight    =   1770
   ClientLeft      =   45
   ClientTop       =   285
   ClientWidth     =   4080
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1770
   ScaleWidth      =   4080
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdOK 
      Caption         =   "OK"
      Default         =   -1  'True
      Height          =   375
      Left            =   3000
      TabIndex        =   2
      Top             =   1320
      Width           =   975
   End
   Begin VB.TextBox txtBenutzerPasswort 
      Height          =   285
      IMEMode         =   3  'DISABLE
      Left            =   120
      MaxLength       =   40
      PasswordChar    =   "*"
      TabIndex        =   1
      Top             =   960
      Width           =   3855
   End
   Begin VB.TextBox txtBenutzerName 
      Height          =   285
      Left            =   120
      MaxLength       =   40
      TabIndex        =   0
      Top             =   360
      Width           =   3855
   End
   Begin VB.CommandButton cmdAbbrechen 
      Cancel          =   -1  'True
      Caption         =   "Abbrechen"
      Height          =   255
      Left            =   3120
      TabIndex        =   3
      TabStop         =   0   'False
      Top             =   1440
      Width           =   615
   End
   Begin VB.Label ZLabel2 
      Caption         =   "Benutzerpasswort:"
      Height          =   255
      Left            =   120
      TabIndex        =   5
      Top             =   720
      Width           =   1455
   End
   Begin VB.Label ZLabel1 
      Caption         =   "Benutzername:"
      Height          =   255
      Left            =   120
      TabIndex        =   4
      Top             =   120
      Width           =   1215
   End
End
Attribute VB_Name = "Anmeldung"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'Author: Jan-Philipp Kappmeier
'Geändert: 2000-02-09

Private Sub cmdAbbrechen_Click()
'Author: Jan-Philipp Kappmeier
'Geändert: 2000-02-09
    End
End Sub

Private Sub cmdOK_Click()
'Author: Jan-Philipp Kappmeier
'Geändert: 2000-02-09   Benutzer anlegen und Hauptform
                        'aufrufen
'Geändert: 2000-02-10   Aufruf der Hauptform geändert

On Error GoTo Anmeldung_cmdOK_Click_Error
    'Es wird in das Verzeichnis des
    'angegebenen Benutzers gesprungen
    'Wenn es nicht existiert, wird
    'es angelegt.
    If Trim(txtBenutzerName) = "" Then
        MsgBox "Bitte geben sie einen Namen ein"
        txtBenutzerName.SetFocus
        Exit Sub
    End If
    ChDir App.Path & "\" & Trim(txtBenutzerName)
    Auswahl_HauptVokabel.sUserName = Trim(txtBenutzerName)
    Unload Me
    'Anzeigen des Hauptauswahlformulares
    'für die Vokabelabfrage
    Auswahl_HauptVokabel.Show
Exit Sub
Anmeldung_cmdOK_Click_Error:
    If Err = 76 Then
        i = CreateUserDir(App.Path & "\" & _
            txtBenutzerName)
        If i <> 0 Then _
            If Fehler(ErrorDescription, "Dateiverwaltung_CreateUserDir" _
                    , Err) = False Then MsgBox "Totalabsturz", _
                    vbCritical, "Letzte Warnung": End
    Else
        If Fehler(Err.Description, "Anmeldung_cmdOK_Click", Err) _
            = False Then MsgBox "Totalabsturz", vbCritical, _
            "Letzte Warnung": End
    End If
End Sub
