VERSION 2.00
Begin Form frmDatei_Login 
   BorderStyle     =   3  'Fixed Double
   Caption         =   "Sprach-Trainer   -   Anmeldung"
   ClientHeight    =   1680
   ClientLeft      =   1035
   ClientTop       =   1815
   ClientWidth     =   5025
   ControlBox      =   0   'False
   Height          =   2085
   Icon            =   FRMLOGIN.FRX:0000
   Left            =   975
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1680
   ScaleWidth      =   5025
   Top             =   1470
   Width           =   5145
   Begin CommandButton cmdAbbrechen 
      Cancel          =   -1  'True
      Caption         =   "&Abbrechen"
      Height          =   375
      Left            =   3720
      TabIndex        =   3
      Top             =   1080
      Width           =   1095
   End
   Begin CommandButton cmdOK 
      Caption         =   "&OK"
      Default         =   -1  'True
      Height          =   375
      Left            =   2520
      TabIndex        =   2
      Top             =   1080
      Width           =   1095
   End
   Begin TextBox txtBenutzerName 
      Height          =   285
      Left            =   1680
      MaxLength       =   50
      TabIndex        =   0
      Top             =   240
      Width           =   3135
   End
   Begin TextBox txtPasswort 
      Height          =   285
      Left            =   1680
      MaxLength       =   50
      TabIndex        =   1
      Top             =   600
      Width           =   3135
   End
   Begin Label ZLabel02 
      BackStyle       =   0  'Transparent
      Caption         =   "Passwort:"
      Height          =   255
      Left            =   240
      TabIndex        =   5
      Top             =   600
      Width           =   855
   End
   Begin Label ZLabel01 
      BackStyle       =   0  'Transparent
      Caption         =   "Benutzername:"
      Height          =   255
      Left            =   240
      TabIndex        =   4
      Top             =   240
      Width           =   1335
   End
End
Option Explicit

Sub cmdAbbrechen_Click ()
    End
End Sub

Sub cmdOK_Click ()
On Error GoTo cmdOKError
'Test ob der Benutzer schon angelegt worden ist.
    If Trim(txtBenutzername.Text) = "" Then
            txtBenutzername.SetFocus
            Exit Sub            'Wenn kein Name eingegeben ist
        Else
            'Test ob Verzeichnis existiert
            ChDir app.Path + "\" + Trim(txtBenutzername.Text)
    End If
Unload Me
Exit Sub
cmdOKError:
    If Err = 76 Then
            'Verneichnis nicht vorhanden.
            'Benutzer anlegen
            M$ = "Der Benutzer existiert nicht." + Chr(13) + "Soll er angelegt werden?"
            T$ = "Sprachtrainer 99"
            a = MsgBox(M$, 4, T$)
            'BenutzerErstellen (txtBenutzername.Text)
            If a = 6 Then
                    'Anlegen
                    b = txtBenutzername.Text
                    a = BenutzerErstellen(b)
                    M$ = "Bitte richtigen Dateinamen angeben"
                    T$ = "SprachTrainer 99"
                    If a = False Then MsgBox M$, , T$: Exit Sub
                Else
                    'Nicht Anlegen
                    txtBenutzername.SetFocus
                    Exit Sub
            End If
        Else
            M$ = "FRMLOGIN - cmdOK" + const_Fehlermeldung1 + Chr(13) + const_fehlermeldung2
            MsgBox M$, , T$
    End If
    Resume
End Sub

