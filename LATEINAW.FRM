VERSION 2.00
Begin Form frmAuswahl_Latein 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Double
   Caption         =   "Latein [Auswahl] "
   ClientHeight    =   5175
   ClientLeft      =   885
   ClientTop       =   1575
   ClientWidth     =   3855
   Height          =   5865
   Left            =   825
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5175
   ScaleWidth      =   3855
   Top             =   945
   Width           =   3975
   Begin CommandButton cmdAbfrageoptionen 
      Caption         =   "&Abfrageoptionen"
      Height          =   375
      Left            =   2160
      TabIndex        =   6
      Top             =   3960
      Width           =   1455
   End
   Begin CommandButton cmdBeenden 
      Cancel          =   -1  'True
      Caption         =   "&Beenden"
      Height          =   375
      Left            =   2160
      TabIndex        =   5
      Top             =   4560
      Width           =   1455
   End
   Begin CommandButton cmdVokabelAbfrage 
      Caption         =   "&Vokabelabfrage"
      Default         =   -1  'True
      Height          =   375
      Left            =   2160
      TabIndex        =   4
      Top             =   2760
      Width           =   1455
   End
   Begin CommandButton cmdVokabelEingabe 
      Caption         =   "Vokabel&eingabe"
      Height          =   375
      Left            =   2160
      TabIndex        =   3
      Top             =   3360
      Width           =   1455
   End
   Begin OptionButton optLektion 
      Caption         =   "Ianua Nova 1"
      Height          =   195
      Index           =   1
      Left            =   2160
      TabIndex        =   2
      Top             =   240
      Value           =   -1  'True
      Width           =   1455
   End
   Begin OptionButton optLektion 
      Caption         =   "Ianua Nova 2"
      Height          =   195
      Index           =   2
      Left            =   2160
      TabIndex        =   1
      Top             =   600
      Width           =   1455
   End
   Begin ListBox lstLektionAuswahl 
      Height          =   4710
      Left            =   240
      TabIndex        =   0
      Top             =   240
      Width           =   1695
   End
   Begin Menu mnuDatei 
      Caption         =   "&Datei"
      Begin Menu mnuNeuesErgebnis 
         Caption         =   "&Neues Ergebnis"
         Shortcut        =   ^N
      End
      Begin Menu ZMenu03 
         Caption         =   "-"
      End
      Begin Menu mnuErgebnisÖffnen 
         Caption         =   "Ergebnis &öffnen..."
      End
      Begin Menu mnuErgebnisSpeichern 
         Caption         =   "Ergebnis &speichern"
         Shortcut        =   ^S
      End
      Begin Menu mnuErgebnisSpeichernUnter 
         Caption         =   "Ergebnis speichern &unter..."
         Shortcut        =   ^U
      End
      Begin Menu ZMenu01 
         Caption         =   "-"
      End
      Begin Menu mnuDruckerEinrichten 
         Caption         =   "Drucker &einrichten..."
      End
      Begin Menu mnuDrucken 
         Caption         =   "&Drucken"
         Shortcut        =   ^P
      End
      Begin Menu ZMenu02 
         Caption         =   "-"
      End
      Begin Menu mnuBenutzerAbmelden 
         Caption         =   "Benutzer &abmelden"
         Shortcut        =   ^A
      End
      Begin Menu mnuAbfrageBeenden 
         Caption         =   "&Beenden"
         Shortcut        =   ^X
      End
   End
   Begin Menu mnuBearbeiten 
      Caption         =   "&Bearbeiten"
      Begin Menu mnuAbfragen 
         Caption         =   "&Abfragen"
         Begin Menu mnuVokabelabfrage 
            Caption         =   "&Vokabelabfrage..."
         End
         Begin Menu mnuPronomenabfrage 
            Caption         =   "&Pronomenabfrage..."
         End
         Begin Menu mnuGrammatikabfrage 
            Caption         =   "&Grammatikabfrage..."
         End
      End
      Begin Menu ZMenu07 
         Caption         =   "-"
      End
      Begin Menu mnuEingeben 
         Caption         =   "&Eingeben"
         Begin Menu mnuVokabelnEingeben 
            Caption         =   "&Vokabeln eingeben..."
         End
         Begin Menu mnuGrammatikEingeben 
            Caption         =   "&Grammatik eingeben..."
         End
      End
      Begin Menu ZMenu04 
         Caption         =   "-"
      End
      Begin Menu mnuWitz 
         Caption         =   "&Witz"
      End
      Begin Menu ZMenu05 
         Caption         =   "-"
      End
      Begin Menu mnuOptionen 
         Caption         =   "Abfrage&optionen..."
      End
      Begin Menu mnuEigenschaften 
         Caption         =   "&Eigenschaften..."
      End
   End
   Begin Menu mnuHilfe 
      Caption         =   "&?"
      Begin Menu mnuIndex 
         Caption         =   "&Index"
      End
      Begin Menu ZMenu06 
         Caption         =   "-"
      End
      Begin Menu mnuInfoÜber 
         Caption         =   "Info &über..."
      End
   End
End
Option Explicit

Dim var_LektionAusgewählt

Sub AuswahlListeFüllen ()
Dim Eintrag
    If pv_BuchStufe = 1 Then
            X = 35
        Else
            X = 27
    End If
    lstlektionauswahl.Clear
    For i = 1 To X
        Eintrag = "Lektion " + Str(i)
        lstlektionauswahl.AddItem (Eintrag)
    Next i
End Sub

Sub cmdBeenden_Click ()
    End
End Sub

Sub cmdVokabelAbfrage_Click ()
    If var_LektionAusgewählt = False Then MsgBox "Bitte wählen sie eine Lektion aus.", , const_ProgrammName: Exit Sub
    frmAbfrage_Vokabeln.Show 1
End Sub

Sub cmdVokabelEingabe_Click ()
    frmEingabe_Vokabeln.Show 1
End Sub

Sub Form_Load ()
    pv_BuchStufe = 1
    AuswahlListeFüllen
    var_LektionAusgewählt = False
End Sub

Sub lstLektionAuswahl_Click ()
    pv_Lektion = lstlektionauswahl.ListIndex + 1
    var_LektionAusgewählt = True
End Sub

Sub mnuAbfrageBeenden_Click ()
    cmdBeenden_Click
End Sub

Sub mnuVokabelabfrage_Click ()
    cmdVokabelAbfrage_Click
End Sub

Sub mnuVokabelnEingeben_Click ()
    cmdVokabelEingabe_Click
End Sub

Sub optLektion_Click (Index As Integer)
    pv_BuchStufe = Index
    AuswahlListeFüllen
    var_LektionAusgewählt = False
End Sub

