VERSION 5.00
Begin VB.Form Auswahl_HauptVokabel 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Form1"
   ClientHeight    =   4530
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   3450
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4530
   ScaleWidth      =   3450
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdAuswahl 
      Caption         =   "&Beenden"
      Height          =   375
      Index           =   3
      Left            =   1800
      TabIndex        =   4
      Top             =   4080
      Width           =   1575
   End
   Begin VB.CommandButton cmdAuswahl 
      Caption         =   "Vokabel &Abfrage"
      Default         =   -1  'True
      Height          =   375
      Index           =   2
      Left            =   1800
      TabIndex        =   3
      Top             =   1320
      Width           =   1575
   End
   Begin VB.CommandButton cmdAuswahl 
      Caption         =   "Vokabel &Eingabe"
      Height          =   375
      Index           =   1
      Left            =   1800
      TabIndex        =   2
      Top             =   720
      Width           =   1575
   End
   Begin VB.CommandButton cmdAuswahl 
      Caption         =   "&Datei öffnen"
      Height          =   375
      Index           =   0
      Left            =   1800
      TabIndex        =   1
      Top             =   120
      Width           =   1575
   End
   Begin VB.TextBox lstLektionen 
      Height          =   4335
      Left            =   120
      ScrollBars      =   1  'Horizontal
      TabIndex        =   0
      Text            =   "Text1"
      Top             =   120
      Width           =   1575
   End
End
Attribute VB_Name = "Auswahl_HauptVokabel"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'Author: Jan-Philipp Kappmeier
'Geändert: 2000-02-09
Public sUserName As String

Public Function Initialize(ByVal UserName As String)
'Author: Jan-Philipp Kappmeier
'Geändert: 2000-02-09   Caption einrichten
'          2000-02-10   Wird nicht mehr aufgerufen
    Me.Caption = "Sprachtrainer 2000 [Vokabeltraining] [" _
        & UserName & "]"
    Datei_Auswählen.Show 1
End Function

Private Sub cmdAuswahl_Click(Index As Integer)
'Author: Jan-Philipp Kappmeier
'Geändert: 2000-02-09   Formanzeige bei Schaltfläche
                        'und Beenden
On Error GoTo Auswahl_HauptVokabel_cmdAuswahl_Click_Error
    Select Case Index
        Case 0
            'Datei auswählen
            Datei_Auswählen.Caption = sUserName
            Datei_Auswählen.Show 1
        Case 1
            'Vokabel eingeben
            Vokabel_Eingeben.Show 1
        Case 2
            'Vokabel abfragen
            Vokabel_Abfragen.Show 1
        Case 3
            'Beenden
            i = MsgBox("Wollen sie wirklich beenden", _
                    vbYesNo, App.Title)
            If i = vbYes Then End
    End Select
Exit Sub
Auswahl_HauptVokabel_cmdAuswahl_Click_Error:
    MsgBox Err.Description & vbCrLf _
        & "Fehler-Nr.: " & vbCrLf & vbCrLf & Err
End Sub

Private Sub Form_Load()
'Jan-Philipp Kappmeier
'Geändert: 2000-02-10   Aufruf des Dateiauswahl-Dialogs
    Me.Caption = "Sprachtrainer 2000 " _
            & "[Vokabeltraining] - " & sUserName
    Datei_Auswählen.Show 1
End Sub
