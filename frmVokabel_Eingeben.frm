VERSION 5.00
Begin VB.Form Vokabel_Eingeben 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Form1"
   ClientHeight    =   5370
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   5880
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5370
   ScaleWidth      =   5880
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox txtEingabe 
      Height          =   285
      Index           =   0
      Left            =   120
      MaxLength       =   25
      TabIndex        =   0
      ToolTipText     =   "Grammatik 1"
      Top             =   360
      Width           =   5655
   End
   Begin VB.CommandButton cmdBeenden 
      Cancel          =   -1  'True
      Caption         =   "&Beenden"
      Height          =   375
      Left            =   4080
      TabIndex        =   20
      Top             =   4920
      Width           =   1695
   End
   Begin VB.TextBox Text1 
      Height          =   285
      Left            =   2280
      MaxLength       =   2
      TabIndex        =   19
      Top             =   3960
      Width           =   1335
   End
   Begin VB.CommandButton cmdDatei÷ffnen 
      Caption         =   "&Datei ˆffnen"
      Height          =   375
      Left            =   120
      TabIndex        =   11
      Top             =   3840
      Width           =   1815
   End
   Begin VB.CommandButton cmdLektionsinfo 
      Caption         =   "&Lektionsinfo"
      Height          =   375
      Left            =   3960
      TabIndex        =   12
      Top             =   3840
      Width           =   1815
   End
   Begin VB.TextBox txtEingabe 
      Height          =   285
      Index           =   10
      Left            =   4440
      TabIndex        =   10
      Top             =   2760
      Width           =   1335
   End
   Begin VB.CommandButton cmdBewegenZur 
      Caption         =   "<<"
      Height          =   375
      Index           =   2
      Left            =   120
      TabIndex        =   13
      ToolTipText     =   "Zum ersten Datensatz"
      Top             =   4320
      Width           =   855
   End
   Begin VB.CommandButton cmdBewegenZur 
      Caption         =   "< Lektion"
      Height          =   375
      Index           =   1
      Left            =   1080
      TabIndex        =   14
      ToolTipText     =   "Zum letzten Datensatz, bei dem eine neue Lektion anf‰ngt"
      Top             =   4320
      Width           =   855
   End
   Begin VB.CommandButton cmdBewegenVor 
      Caption         =   ">>"
      Height          =   375
      Index           =   2
      Left            =   4920
      TabIndex        =   18
      ToolTipText     =   "Zum letzten Datensatz"
      Top             =   4320
      Width           =   855
   End
   Begin VB.CommandButton cmdBewegenVor 
      Caption         =   "Lektion >"
      Height          =   375
      Index           =   1
      Left            =   3960
      TabIndex        =   17
      ToolTipText     =   "Zum n‰chsten Datensatz bei dem eine neue Lektion anf‰ngt"
      Top             =   4320
      Width           =   855
   End
   Begin VB.CommandButton cmdBewegenZur 
      Caption         =   "<"
      Height          =   375
      Index           =   0
      Left            =   2040
      TabIndex        =   15
      ToolTipText     =   "Einen Datensatz zur¸ck"
      Top             =   4320
      Width           =   855
   End
   Begin VB.CommandButton cmdBewegenVor 
      Caption         =   ">"
      Default         =   -1  'True
      Height          =   375
      Index           =   0
      Left            =   3000
      TabIndex        =   16
      ToolTipText     =   "Einen Datensatz vor oder neuen anlegen"
      Top             =   4320
      Width           =   855
   End
   Begin VB.TextBox txtEingabe 
      Height          =   285
      Index           =   9
      Left            =   3000
      MaxLength       =   2
      TabIndex        =   9
      Top             =   2760
      Width           =   1335
   End
   Begin VB.TextBox txtEingabe 
      Height          =   285
      Index           =   8
      Left            =   1560
      MaxLength       =   2
      TabIndex        =   8
      Top             =   2760
      Width           =   1335
   End
   Begin VB.TextBox txtEingabe 
      Height          =   285
      Index           =   7
      Left            =   120
      MaxLength       =   3
      TabIndex        =   7
      Top             =   2760
      Width           =   1335
   End
   Begin VB.TextBox txtEingabe 
      Height          =   285
      Index           =   6
      Left            =   3000
      MaxLength       =   25
      TabIndex        =   6
      ToolTipText     =   "Grammatik 3"
      Top             =   1920
      Width           =   2775
   End
   Begin VB.TextBox txtEingabe 
      Height          =   285
      Index           =   5
      Left            =   3000
      MaxLength       =   25
      TabIndex        =   5
      ToolTipText     =   "Grammatik 2"
      Top             =   1560
      Width           =   2775
   End
   Begin VB.TextBox txtEingabe 
      Height          =   285
      Index           =   4
      Left            =   3000
      MaxLength       =   25
      TabIndex        =   4
      ToolTipText     =   "Grammatik 1"
      Top             =   1200
      Width           =   2775
   End
   Begin VB.TextBox txtEingabe 
      Height          =   285
      Index           =   3
      Left            =   120
      MaxLength       =   25
      TabIndex        =   3
      ToolTipText     =   "Bedeutung 3"
      Top             =   1920
      Width           =   2775
   End
   Begin VB.TextBox txtEingabe 
      Height          =   285
      Index           =   2
      Left            =   120
      MaxLength       =   25
      TabIndex        =   2
      ToolTipText     =   "Bedeutung 2"
      Top             =   1560
      Width           =   2775
   End
   Begin VB.TextBox txtEingabe 
      Height          =   285
      Index           =   1
      Left            =   120
      MaxLength       =   25
      TabIndex        =   1
      ToolTipText     =   "Bedeutung 1"
      Top             =   1200
      Width           =   2775
   End
   Begin VB.Label ZLabel7 
      Caption         =   "Info"
      Height          =   255
      Left            =   4440
      TabIndex        =   29
      Top             =   2520
      Width           =   1215
   End
   Begin VB.Label ZLabel8 
      Alignment       =   2  'Center
      Caption         =   "Gehe zu Lektion"
      Height          =   255
      Left            =   2280
      TabIndex        =   28
      Top             =   3720
      Width           =   1335
   End
   Begin VB.Label lblDatensatz 
      AutoSize        =   -1  'True
      Caption         =   "Datensatznummer ? / ??"
      Height          =   195
      Left            =   120
      TabIndex        =   27
      Top             =   3240
      Width           =   1755
   End
   Begin VB.Label ZLabel6 
      Caption         =   "Vokabelnummer"
      Height          =   255
      Left            =   3000
      TabIndex        =   26
      Top             =   2520
      Width           =   1215
   End
   Begin VB.Label ZLabel5 
      Caption         =   "Lektion"
      Height          =   255
      Left            =   1560
      TabIndex        =   25
      Top             =   2520
      Width           =   735
   End
   Begin VB.Label ZLabel4 
      Caption         =   "Art"
      Height          =   255
      Left            =   120
      TabIndex        =   24
      Top             =   2520
      Width           =   495
   End
   Begin VB.Label ZLabel3 
      Caption         =   "Grammatik"
      Height          =   255
      Left            =   3000
      TabIndex        =   23
      Top             =   960
      Width           =   1215
   End
   Begin VB.Label ZLabel2 
      Caption         =   "Bedeutungen"
      Height          =   255
      Left            =   120
      TabIndex        =   22
      Top             =   960
      Width           =   1215
   End
   Begin VB.Label ZLabel1 
      Caption         =   "Vokabel"
      Height          =   255
      Left            =   120
      TabIndex        =   21
      Top             =   120
      Width           =   1215
   End
End
Attribute VB_Name = "Vokabel_Eingeben"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'Author: Jan-Philipp Kappmeier
'Ge‰ndert: 2000-02-09
Dim iSatzL‰nge
Dim iSatzNummer

Private Sub cmdBeenden_Click()
'Author: Jan-Philipp Kappmeier
'Ge‰ndert: 2000-02-10
    Speichern (iSatzNummer)
    Unload Me
End Sub

Private Sub cmdBewegenVor_Click(Index As Integer)
'Author: Jan-Philipp Kappmeier
'Ge‰ndert: 2000-02-10 Einen Datensatz Vor, zum Ende
On Error GoTo Vokabel_Eingeben_cmdBewegenVor_Click_Error
    Select Case Index
        Case 0
            iSatzNummer = iSatzNummer + 1
        Case 1
        
        Case 2
            iSatzNummer = iSatzL‰nge
    End Select
    i = Speichern(iSatzNummer - 1)
    If i <> 0 Then i = Fehler(ErrorDescription, _
            "Vokabel_Eingeben_cmdBewegenVor_Click", Err): Exit Sub: Unload Me
    i = Anzeigen(iSatzNummer)
    If i <> 0 Then i = Fehler(ErrorDescription, _
            "Vokabel_Eingeben_cmdBewegenVor_Click", Err): Exit Sub: Unload Me
    txtEingabe(0).SetFocus
Exit Sub
Vokabel_Eingeben_cmdBewegenVor_Click_Error:
        i = Fehler(Err.Description, "Vokabel_Eingeben_cmdBewegenVor_Click", Err)
        iSatzNummer = iSatzNummer - 1
End Sub

Private Sub cmdBewegenZur_Click(Index As Integer)
'Author: Jan-Philipp Kappmeier
'Ge‰ndert: 2000-02-10   Einen Datensatz Zur¸ck, zum Anfang
On Error GoTo Vokabel_Eingeben_cmdBewegenZur_Click_Error
    Select Case Index
        Case 0
            If iSatzNummer = 1 Then Exit Sub
            iSatzNummer = iSatzNummer - 1
        Case 1
            
        Case 2
            iSatzNummer = 1
    End Select
    i = Speichern(iSatzNummer + 1)
    If i <> 0 Then i = Fehler(ErrorDescription, _
            "Vokabel_Eingeben__cmdBewegenZur_Click", Err): Exit Sub: Unload Me
    i = Anzeigen(iSatzNummer)
    If i <> 0 Then i = Fehler(ErrorDescription, _
            "Vokabel_Eingeben__cmdBewegenZur_Click", Err): Exit Sub: Unload Me
    txtEingabe(0).SetFocus
Exit Sub
Vokabel_Eingeben_cmdBewegenZur_Click_Error:
        i = Fehler(Err.Description, "Vokabel_Eingeben_cmdBewegenZur_Click", Err)
        iSatzNummer = iSatzNummer - 1
End Sub

Private Sub cmdDatei÷ffnen_Click()
'Author: Jan-Philipp Kappmeier
'Ge‰ndert: 2000-02-10   Nur zugelassene Zeichen verwenden
On Error GoTo Vokabel_Eingeben_cmdDatei÷ffnen_Click_Error
    Datei_Ausw‰hlen.Show 1
    i = Anzeigen(1)
    If i <> 0 Then i = Fehler(ErrorDescription, _
            "Vokabel_Eingeben_cmdDatei÷ffnen_Click", Err): Exit Sub: Unload Me
    Me.Caption = "Sprachtrainer 2000 [Vokabeleingabe] - " _
            & Datei_Ausw‰hlen.sVokabelDatei
    iSatzNummer = 1
Exit Sub
Vokabel_Eingeben_cmdDatei÷ffnen_Click_Error:
        i = Fehler(Err.Description, "Vokabel_Eingeben_Form_Load", Err)
End Sub

Private Sub cmdLektionsinfo_Click()
'Author: Jan-Philipp Kappmeier
'Ge‰ndert: 2000-02-10
    Vokabel_Eingeben_Info.Show 1
End Sub

Private Sub Form_Load()
'Author: Jan-Philipp Kappmeier
'Ge‰ndert: 2000-02-10
On Error GoTo Vokabel_Eingeben_Form_Load_Error
    i = Anzeigen(1)
    If i <> 0 Then i = Fehler(ErrorDescription, _
            "Vokabel_Eingeben_FormLoad", Err): Exit Sub: Unload Me
    Me.Caption = "Sprachtrainer 2000 [Vokabeleingabe] - " _
            & Datei_Ausw‰hlen.sVokabelDatei
    iSatzNummer = 1
Exit Sub
Vokabel_Eingeben_Form_Load_Error:
        i = Fehler(Err.Description, "Vokabel_Eingeben_Form_Load", Err)
End Sub

Private Sub txtEingabe_KeyPress(Index As Integer, KeyAscii As Integer)
'Author: Jan-Philipp Kappmeier
'Ge‰ndert: 2000-02-10   Nur zugelassene Zeichen verwenden
    Select Case Index
        Case 7 To 10
            Select Case KeyAscii
                Case 1 To 7, 9 To 47, 58 To 255
                    KeyAscii = 0
            End Select
    End Select
End Sub

Private Function Anzeigen(ByVal SatzNummer As Integer) As Integer
'Author: Jan-Philipp Kappmeier
'Ge‰ndert: 2000-02-10
Dim iSav As Integer
On Error GoTo Vokabel_Eingeben_Anzeigen_Error
    Get DNR_VOKABEL, SatzNummer, VokabelDatei
    With VokabelDatei
        txtEingabe(0) = .sVokabel
        txtEingabe(1) = .sBedeutung1
        txtEingabe(2) = .sBedeutung2
        txtEingabe(3) = .sBedeutung3
        txtEingabe(4) = .sGrammatik1
        txtEingabe(5) = .sGrammatik2
        txtEingabe(6) = .sGrammatik3
        txtEingabe(7) = .iArt
        txtEingabe(8) = .iLektion
        txtEingabe(9) = .iVokabel
        txtEingabe(10) = .iVokabelInfo
    End With
    If txtEingabe(0) = "" Then
        iSav = Speichern(SatzNummer)
        If iSav <> 0 Then MsgBox ErrorDescription & vbCrLf _
                & vbCrLf & iSav: Exit Function: Unload Me
    End If
    iSatzL‰nge = LOF(DNR_VOKABEL) \ Len(VokabelDatei)
    lblDatensatz = "Datensatz " & Str(SatzNummer) _
            & " / " & iSatzL‰nge
    Aktualisieren
    Anzeigen = 0
Exit Function
Vokabel_Eingeben_Anzeigen_Error:
        i = Fehler(Err.Description, "Vokabel_Eingeben_Anzeigen", Err)
End Function

Private Sub Aktualisieren()
'Author: Jan-Philipp Kappmeier
'Ge‰ndert: 2000-02-10   Felder zur Eingabe vorbereiten
    For i = 0 To 6
        txtEingabe(i) = Trim(txtEingabe(i))
    Next i '= 0 To 6
    For i = 7 To 10
        If txtEingabe(i) = 0 Then txtEingabe(i) = ""
    Next i '= 7 To 10
End Sub

Private Sub AktualisierenSav()
'Author: Jan-Philipp Kappmeier
'Ge‰ndert: 2000-02-10   Felder zum  Speichern vorbereiten
    For i = 7 To 10
        If txtEingabe(i) = "" Then txtEingabe(i) = 0
    Next i '= 7 To 10
End Sub

Private Function Speichern(SatzNummer) As Integer
'Author: Jan-Philipp Kappmeier
'Ge‰ndert: 2000-02-10
On Error GoTo Vokabel_Eingeben_Speichern_Error
    AktualisierenSav
    With VokabelDatei
        .iArt = txtEingabe(7)
        .iLektion = txtEingabe(8)
        .iVokabel = txtEingabe(9)
        .iVokabelInfo = txtEingabe(10)
        .sBedeutung1 = txtEingabe(1)
        .sBedeutung2 = txtEingabe(2)
        .sBedeutung3 = txtEingabe(3)
        .sGrammatik1 = txtEingabe(4)
        .sGrammatik2 = txtEingabe(5)
        .sGrammatik3 = txtEingabe(6)
        .sVokabel = txtEingabe(0)
    End With
    Put DNR_VOKABEL, SatzNummer, VokabelDatei
    Speichern = 0
Exit Function
Vokabel_Eingeben_Speichern_Error:
        i = Fehler(Err.Description, "Vokabel_Eingeben_Speichern", Err)
End Function
