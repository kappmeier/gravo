VERSION 2.00
Begin Form frmEingabe_Vokabeln 
   AutoRedraw      =   -1  'True
   BackColor       =   &H00808080&
   BorderStyle     =   3  'Fixed Double
   Caption         =   "Vokabeleingabe - StdVok1.VOK"
   ClientHeight    =   5280
   ClientLeft      =   885
   ClientTop       =   1305
   ClientWidth     =   6720
   Height          =   5685
   Left            =   825
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5280
   ScaleWidth      =   6720
   Top             =   960
   Width           =   6840
   Begin CommandButton cmdNeu 
      Caption         =   "&Neu"
      Default         =   -1  'True
      Height          =   375
      Left            =   120
      TabIndex        =   34
      Top             =   4800
      Width           =   1215
   End
   Begin TextBox txtVokabelInfo 
      Enabled         =   0   'False
      Height          =   285
      Left            =   3480
      TabIndex        =   9
      Top             =   2880
      Width           =   1455
   End
   Begin CommandButton cmdVorwärts 
      Caption         =   "1000 >"
      Height          =   735
      Index           =   1000
      Left            =   5880
      TabIndex        =   17
      Top             =   2640
      Width           =   735
   End
   Begin CommandButton cmdVorwärts 
      Caption         =   "100 >"
      Height          =   735
      Index           =   100
      Left            =   5880
      TabIndex        =   15
      Top             =   1800
      Width           =   735
   End
   Begin CommandButton cmdZurück 
      Caption         =   "< 100"
      Height          =   735
      Index           =   100
      Left            =   5040
      TabIndex        =   14
      Top             =   1800
      Width           =   735
   End
   Begin CommandButton cmdZurück 
      Caption         =   "< 1000"
      Height          =   735
      Index           =   1000
      Left            =   5040
      TabIndex        =   16
      Top             =   2640
      Width           =   735
   End
   Begin CommandButton cmdZurück 
      Caption         =   "< 10"
      Height          =   735
      Index           =   10
      Left            =   5040
      TabIndex        =   12
      Top             =   960
      Width           =   735
   End
   Begin CommandButton cmdVorwärts 
      Caption         =   "10 >"
      Height          =   735
      Index           =   10
      Left            =   5880
      TabIndex        =   13
      Top             =   960
      Width           =   735
   End
   Begin CommandButton cmdZurück 
      Caption         =   "<"
      Height          =   735
      Index           =   1
      Left            =   5040
      TabIndex        =   10
      Top             =   120
      Width           =   735
   End
   Begin CommandButton cmdVorwärts 
      Caption         =   ">"
      Height          =   735
      Index           =   1
      Left            =   5880
      TabIndex        =   11
      Top             =   120
      Width           =   735
   End
   Begin CommandButton cmdÖffnen 
      Caption         =   "&Öffnen"
      Height          =   375
      Left            =   1440
      TabIndex        =   18
      Top             =   4800
      Width           =   1215
   End
   Begin CommandButton cmdAbbrechen 
      Caption         =   "&Abbrechen"
      Height          =   375
      Left            =   5400
      TabIndex        =   19
      Top             =   4800
      Width           =   1215
   End
   Begin TextBox txtVokabelnummer 
      Enabled         =   0   'False
      Height          =   285
      Left            =   1800
      MaxLength       =   5
      TabIndex        =   8
      Top             =   2880
      Width           =   1455
   End
   Begin TextBox txtArt 
      Enabled         =   0   'False
      Height          =   285
      Left            =   120
      MaxLength       =   3
      TabIndex        =   7
      Top             =   2880
      Width           =   1455
   End
   Begin TextBox txtGrammatik3 
      Enabled         =   0   'False
      Height          =   285
      Left            =   2640
      MaxLength       =   45
      TabIndex        =   6
      Top             =   2160
      Width           =   2295
   End
   Begin TextBox txtGrammatik2 
      Enabled         =   0   'False
      Height          =   285
      Left            =   2640
      MaxLength       =   45
      TabIndex        =   5
      Top             =   1560
      Width           =   2295
   End
   Begin TextBox txtGrammatik1 
      Enabled         =   0   'False
      Height          =   285
      Left            =   2640
      MaxLength       =   45
      TabIndex        =   4
      Top             =   960
      Width           =   2295
   End
   Begin TextBox txtBedeutung3 
      Enabled         =   0   'False
      Height          =   285
      Left            =   120
      MaxLength       =   45
      TabIndex        =   3
      Top             =   2160
      Width           =   2295
   End
   Begin TextBox txtBedeutung2 
      Enabled         =   0   'False
      Height          =   285
      Left            =   120
      MaxLength       =   45
      TabIndex        =   2
      Top             =   1560
      Width           =   2295
   End
   Begin TextBox txtBedeutung1 
      Enabled         =   0   'False
      Height          =   285
      Left            =   120
      MaxLength       =   45
      TabIndex        =   1
      Top             =   960
      Width           =   2295
   End
   Begin TextBox txtVokabel 
      Enabled         =   0   'False
      Height          =   285
      Left            =   120
      MaxLength       =   45
      TabIndex        =   0
      Top             =   360
      Width           =   4815
   End
   Begin Label Bezeichnung2 
      Alignment       =   1  'Right Justify
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "Satznummer:"
      Height          =   195
      Left            =   360
      TabIndex        =   33
      Top             =   3600
      Width           =   1095
   End
   Begin Label lblSatzNr 
      Alignment       =   1  'Right Justify
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "1"
      Height          =   195
      Left            =   1920
      TabIndex        =   32
      Top             =   3600
      Width           =   120
   End
   Begin Label ZLabel11 
      BackStyle       =   0  'Transparent
      Caption         =   "Vokabelinfo:"
      Height          =   255
      Left            =   3480
      TabIndex        =   31
      Top             =   2640
      Width           =   1215
   End
   Begin Label lblVokabelAnzahl 
      Alignment       =   1  'Right Justify
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "1"
      Height          =   195
      Left            =   1920
      TabIndex        =   30
      Top             =   3360
      Width           =   120
   End
   Begin Label ZLabel10 
      Alignment       =   1  'Right Justify
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "Vokabelanzahl:"
      Height          =   195
      Left            =   120
      TabIndex        =   29
      Top             =   3360
      Width           =   1320
   End
   Begin Label ZLabel09 
      BackStyle       =   0  'Transparent
      Caption         =   "Vokabelnummer:"
      Height          =   255
      Left            =   1800
      TabIndex        =   28
      Top             =   2640
      Width           =   1695
   End
   Begin Label ZLabel08 
      BackStyle       =   0  'Transparent
      Caption         =   "Art:"
      Height          =   255
      Left            =   120
      TabIndex        =   27
      Top             =   2640
      Width           =   975
   End
   Begin Label ZLabel07 
      BackStyle       =   0  'Transparent
      Caption         =   "Grammatik 3:"
      Height          =   255
      Left            =   2640
      TabIndex        =   26
      Top             =   1920
      Width           =   2295
   End
   Begin Label ZLabel06 
      BackStyle       =   0  'Transparent
      Caption         =   "Grammatik 2:"
      Height          =   255
      Left            =   2640
      TabIndex        =   25
      Top             =   1320
      Width           =   2295
   End
   Begin Label ZLabel05 
      BackStyle       =   0  'Transparent
      Caption         =   "Grammatik 1:"
      Height          =   255
      Left            =   2640
      TabIndex        =   24
      Top             =   720
      Width           =   2295
   End
   Begin Label ZLabel04 
      BackStyle       =   0  'Transparent
      Caption         =   "Bedeutung 3:"
      Height          =   255
      Left            =   120
      TabIndex        =   23
      Top             =   1920
      Width           =   2295
   End
   Begin Label ZLabel03 
      BackStyle       =   0  'Transparent
      Caption         =   "Bedeutung 2:"
      Height          =   255
      Left            =   120
      TabIndex        =   22
      Top             =   1320
      Width           =   2295
   End
   Begin Label ZLabel02 
      BackStyle       =   0  'Transparent
      Caption         =   "Bedeutung 1:"
      Height          =   255
      Left            =   120
      TabIndex        =   21
      Top             =   720
      Width           =   2295
   End
   Begin Label ZLabel01 
      BackStyle       =   0  'Transparent
      Caption         =   "Fremdwort:"
      Height          =   255
      Left            =   120
      TabIndex        =   20
      Top             =   120
      Width           =   2295
   End
End
Option Explicit

Sub cmdAbbrechen_Click ()
    Unload Me
End Sub

Sub cmdNeu_Click ()
On Error GoTo cmdNeu_Click_Error
    SatzAnzahl = LOF(DNr) \ Len(VokabelDaten)
    SatzNr = SatzAnzahl
    If DatenLesen(SatzNr) = False Then
            MsgBox "Fehler beim Lesen.", , const_programmname
            Exit Sub
    End If
    cmdvorwärts_Click (1)
    txtvokabel.SetFocus
    NullControl
cmdNeu_Click_Error:
Exit Sub
End Sub

Sub cmdÖffnen_Click ()
On Error GoTo cmdÖffnen_Fehler
    'Öffnen der Standard-Vokabeln Latein1
    DNr = 1
    Open App.Path + "\" + "StdVok1.VOK" For Random As #DNr Len = Len(VokabelDaten)
    SatzAnzahl = LOF(DNr) \ Len(VokabelDaten)
    If DatenLesen(1) = False Then
            MsgBox "Ein Fehler ist aufgetreten.", , const_programmname
        Else
            SatzNr = 1
    End If
    lblVokabelAnzahl.Caption = SatzAnzahl
    lblSatzNr = 1
    txtvokabel.Enabled = True
    txtBedeutung1.Enabled = True
    txtBedeutung2.Enabled = True
    txtBedeutung3.Enabled = True
    txtGrammatik1.Enabled = True
    txtGrammatik2.Enabled = True
    txtGrammatik3.Enabled = True
    txtart.Enabled = True
    'txtVokabelInfo.Enabled = True
    txtVokabelnummer.Enabled = True
Exit Sub
cmdÖffnen_Fehler:
Resume Next
End Sub

Sub cmdvorwärts_Click (Index As Integer)
On Error GoTo cmdVorwärts_Fehler
    nullsetzen
    If DatenSpeichern(SatzNr) = False Then Exit Sub
    If SatzNr + Index > SatzAnzahl Then
            b = DatenLesen(SatzAnzahl + 1)
            SatzNr = SatzAnzahl + 1
        Else
            a = DatenLesen(SatzNr + Index)
            SatzNr = SatzNr + Index
    End If
    NullControl
Exit Sub
cmdVorwärts_Fehler:
Resume Next
End Sub

Sub cmdZurück_Click (Index As Integer)
On Error GoTo cmdZurück_Fehler
    nullsetzen
    If DatenSpeichern(SatzNr) = False Then Exit Sub
    If SatzNr - Index <= 0 Then
            a = DatenLesen(1)
            SatzNr = 1
        Else
            b = DatenLesen(SatzNr - Index)
            SatzNr = SatzNr - Index
    End If
    NullControl
Exit Sub
cmdZurück_Fehler:
Resume
End Sub

Function DatenLesen (DatenSatzNr)
On Error GoTo DatenLesen_Fehler
    DatenLesen = False
    'Anzeigen der Datensätze in den Feldern
    Get #DNr, DatenSatzNr, VokabelDaten
    lblVokabelAnzahl.Caption = SatzAnzahl
    lblSatzNr = Trim(DatenSatzNr)
    txtvokabel = Trim(VokabelDaten.VokabelName)
    txtBedeutung1.Text = Trim(VokabelDaten.Deutsch1)
    txtBedeutung2.Text = Trim(VokabelDaten.Deutsch2)
    txtBedeutung3.Text = Trim(VokabelDaten.Deutsch3)
    txtGrammatik1.Text = Trim(VokabelDaten.Grammatik1)
    txtGrammatik2.Text = Trim(VokabelDaten.Grammatik2)
    txtGrammatik3.Text = Trim(VokabelDaten.Grammatik3)
    txtart.Text = VokabelDaten.Art
    txtVokabelnummer.Text = VokabelDaten.VokabelNummer
    txtVokabelInfo.Text = VokabelDaten.VokabelInfo
    SatzAnzahl = LOF(DNr) \ Len(VokabelDaten)
    DatenLesen = True
    Exit Function
DatenLesen_Fehler:
    DatenLesen = False
End Function

Function DatenSpeichern (DatenSatzNr)
On Error GoTo DatenSpeichern_Fehler
    DatenSpeichern = False
    'Anzeigen der Datensätze in den Feldern
    VokabelDaten.VokabelName = txtvokabel.Text
    VokabelDaten.Deutsch1 = txtBedeutung1.Text
    VokabelDaten.Deutsch2 = txtBedeutung2.Text
    VokabelDaten.Deutsch3 = txtBedeutung3.Text
    VokabelDaten.Grammatik1 = txtGrammatik1.Text
    VokabelDaten.Grammatik2 = txtGrammatik2.Text
    VokabelDaten.Grammatik3 = txtGrammatik3.Text
    VokabelDaten.Art = txtart.Text
    VokabelDaten.VokabelNummer = txtVokabelnummer.Text
    VokabelDaten.VokabelInfo = txtVokabelInfo.Text
    Put #DNr, DatenSatzNr, VokabelDaten
    DatenSpeichern = True
    SatzAnzahl = LOF(DNr) \ Len(VokabelDaten)
    Exit Function
DatenSpeichern_Fehler:
    DatenSpeichern = False
End Function

Sub Form_Unload (Cancel As Integer)
    Close
End Sub

Sub NullControl ()
    If txtart.Text = "0" Then txtart.Text = ""
    If txtVokabelnummer.Text = "0" Then txtVokabelnummer.Text = ""
    If txtVokabelInfo.Text = "0" Then txtVokabelInfo.Text = ""
End Sub

Sub nullsetzen ()
    If txtart.Text = "" Then txtart.Text = "0"
    If txtVokabelnummer.Text = "" Then txtVokabelnummer.Text = "0"
    If txtVokabelInfo.Text = "" Then txtVokabelInfo.Text = "0"
End Sub

