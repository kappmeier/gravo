VERSION 5.00
Begin VB.Form Datei_Auswählen 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Form1"
   ClientHeight    =   1425
   ClientLeft      =   45
   ClientTop       =   285
   ClientWidth     =   2760
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1425
   ScaleWidth      =   2760
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.OptionButton optDatei 
      Caption         =   "Ianua Nova 2"
      Height          =   195
      Index           =   1
      Left            =   120
      TabIndex        =   1
      Top             =   480
      Width           =   1335
   End
   Begin VB.OptionButton optDatei 
      Caption         =   "Ianua Nova 1"
      Height          =   195
      Index           =   0
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   1455
   End
   Begin VB.CommandButton cmdOK 
      Caption         =   "OK"
      Default         =   -1  'True
      Height          =   375
      Left            =   1680
      TabIndex        =   2
      Top             =   960
      Width           =   975
   End
End
Attribute VB_Name = "Datei_Auswählen"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'Author: Jan-Philipp Kappmeier
'Geändert: 2000-02-09
'Geändert: 2000-02-10   bDatei
                        'sVokabelDatei

Dim bDatei As Byte  'Index des opt-Steuerelementes
Public sVokabelDatei As String

Private Sub cmdOK_Click()
'Author: Jan-Philipp Kappmeier
'Geändert: 2000-02-10   Schließen und öffnen der Dateien
    Close DNR_VOKABEL
    Close DNR_INFO
    Select Case bDatei
        Case 1
            'Ianua Nova 1 starten
            sVokabelDatei = "SVLat1.voc"
            Open App.Path & "\" & sVokabelDatei _
                    For Random As DNR_VOKABEL _
                    Len = Len(VokabelDatei)
            Open App.Path & "\SVLat1.vid" _
                    For Random As DNR_INFO _
                    Len = Len(InfoDatei)
        Case 2
            'Ianua Nova 2 starten
            sVokabelDatei = "SVLat2.voc"
            Open App.Path & "\" & sVokabelDatei _
                    For Random As DNR_VOKABEL _
                    Len = Len(VokabelDatei)
            Open App.Path & "\SVLat2.vid" _
                    For Random As DNR_INFO _
                    Len = Len(InfoDatei)
        Case Else
            Exit Sub
    End Select
    Unload Me
End Sub

Private Sub optDatei_Click(Index As Integer)
'Author: Jan-Philipp Kappmeier
'Geändert: 2000-02-10   bDatei mit Index besetzen
    bDatei = Index + 1
End Sub
