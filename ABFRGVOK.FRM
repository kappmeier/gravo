VERSION 2.00
Begin Form frmAbfrage_Vokabeln 
   BorderStyle     =   3  'Fixed Double
   Caption         =   "Vokabelabfrage [Unnamed.JSD] - Latein"
   ClientHeight    =   4365
   ClientLeft      =   75
   ClientTop       =   1305
   ClientWidth     =   6000
   Height          =   4770
   Left            =   15
   LinkTopic       =   "Form1"
   ScaleHeight     =   4365
   ScaleWidth      =   6000
   Top             =   960
   Width           =   6120
   Begin Frame ZFrame2 
      Caption         =   "Grammatik:"
      Height          =   2535
      Left            =   3120
      TabIndex        =   4
      Top             =   1080
      Width           =   2775
      Begin TextBox txtGrammatik3 
         Height          =   285
         Left            =   240
         MaxLength       =   45
         TabIndex        =   5
         Top             =   2040
         Width           =   2295
      End
      Begin TextBox txtGrammatik2 
         Height          =   285
         Left            =   240
         MaxLength       =   45
         TabIndex        =   6
         Top             =   1320
         Width           =   2295
      End
      Begin TextBox txtGrammatik1 
         Height          =   285
         Left            =   240
         MaxLength       =   45
         TabIndex        =   17
         Top             =   600
         Width           =   2295
      End
      Begin Label lblGrammatik1Hilfe 
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   1320
         TabIndex        =   18
         Top             =   360
         Width           =   1215
      End
      Begin Label lblGrammatik2Hilfe 
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   1320
         TabIndex        =   19
         Top             =   1080
         Width           =   1215
      End
      Begin Label lblGrammatik3Hilfe 
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   1320
         TabIndex        =   23
         Top             =   1800
         Width           =   1215
      End
      Begin Label lblGrammatik3 
         Caption         =   "Grammatik 3"
         Height          =   255
         Left            =   240
         TabIndex        =   7
         Top             =   1800
         Width           =   1215
      End
      Begin Label lblGrammatik2 
         Caption         =   "Grammatik 2"
         Height          =   255
         Left            =   240
         TabIndex        =   8
         Top             =   1080
         Width           =   1215
      End
      Begin Label lblGrammatik1 
         Caption         =   "Grammatik 1"
         Height          =   255
         Left            =   240
         TabIndex        =   9
         Top             =   360
         Width           =   1215
      End
   End
   Begin Frame ZFrame1 
      Caption         =   "Bedeutungen:"
      Height          =   2535
      Left            =   120
      TabIndex        =   3
      Top             =   1080
      Width           =   2775
      Begin TextBox txtBedeutung3 
         Height          =   285
         Left            =   240
         MaxLength       =   45
         TabIndex        =   13
         Top             =   2040
         Width           =   2295
      End
      Begin TextBox txtBedeutung2 
         Height          =   285
         Left            =   240
         MaxLength       =   45
         TabIndex        =   12
         Top             =   1320
         Width           =   2295
      End
      Begin TextBox txtBedeutung1 
         Height          =   285
         Left            =   240
         MaxLength       =   45
         TabIndex        =   11
         Top             =   600
         Width           =   2295
      End
      Begin Label lblBedeutung1Hilfe 
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   1320
         TabIndex        =   20
         Top             =   360
         Width           =   1215
      End
      Begin Label lblBedeutung3Hilfe 
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   1320
         TabIndex        =   21
         Top             =   1800
         Width           =   1215
      End
      Begin Label lblBedeutung2Hilfe 
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   1320
         TabIndex        =   22
         Top             =   1080
         Width           =   1215
      End
      Begin Label lblBedeutung3 
         Caption         =   "Bedeutung 3"
         Height          =   255
         Left            =   240
         TabIndex        =   16
         Top             =   1800
         Width           =   1215
      End
      Begin Label lblBedeutung2 
         Caption         =   "Bedeutung 2"
         Height          =   255
         Left            =   240
         TabIndex        =   15
         Top             =   1080
         Width           =   1215
      End
      Begin Label lblBedeutung1 
         Caption         =   "Bedeutung 1"
         Height          =   255
         Left            =   240
         TabIndex        =   14
         Top             =   360
         Width           =   1215
      End
   End
   Begin CommandButton cmdFertig 
      Caption         =   "&Fertig"
      Default         =   -1  'True
      Height          =   375
      Left            =   120
      TabIndex        =   2
      Top             =   3840
      Width           =   1215
   End
   Begin CommandButton cmdHilfe 
      Caption         =   "&Hilfe"
      Height          =   375
      Left            =   1560
      TabIndex        =   1
      Top             =   3840
      Width           =   1215
   End
   Begin CommandButton cmdAbbrechen 
      Cancel          =   -1  'True
      Caption         =   "&Abbrechen"
      Height          =   375
      Left            =   4680
      TabIndex        =   0
      Top             =   3840
      Width           =   1215
   End
   Begin Line ZLine1 
      X1              =   120
      X2              =   5880
      Y1              =   960
      Y2              =   960
   End
   Begin Label lblVokabel 
      Alignment       =   2  'Center
      BackStyle       =   0  'Transparent
      Caption         =   "Vokabel ???"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   13.5
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   735
      Left            =   0
      TabIndex        =   10
      Top             =   120
      Width           =   6015
   End
End
Option Explicit

Sub cmdAbbrechen_Click ()
    Unload Me
End Sub

Sub Form_Load ()
    DNr = 1
    SatzNr = 0
    Open App.Path + "\" + "StdVok2.VOK" For Random As #DNr Len = Len(VokabelDaten)
    SatzAnzahl = LOF(DNr) \ Len(VokabelDaten)
    pv_Vokabel = 1
    a = GetVokabelDaten(pv_Buchstufe, pv_Lektion, pv_Vokabel)
    lblVokabel.Caption = VokabelDaten.VokabelName




End Sub

Function GetVokabelDaten (Buch, Lektion, Vokabel)
Dim VokabelGefunden
    SatzNr = 0
    While VokabelGefunden = False
        SatzNr = SatzNr + 1
        Get #DNr, SatzNr, VokabelDaten
        a = Right(VokabelDaten.VokabelNummer, 2)
        b = Left(VokabelDaten.VokabelNummer, 3)
        c = Right(VokabelDaten.VokabelNummer, 2)
        If Val(a) = Vokabel And Val(c) = Lektion Then VokabelGefunden = True
    Wend
End Function

