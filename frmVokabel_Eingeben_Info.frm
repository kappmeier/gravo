VERSION 5.00
Begin VB.Form Vokabel_Eingeben_Info 
   BorderStyle     =   3  'Fester Dialog
   Caption         =   "Form1"
   ClientHeight    =   5850
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   4050
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5850
   ScaleWidth      =   4050
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows-Standard
   Begin VB.CommandButton Command2 
      Caption         =   "Command2"
      Height          =   375
      Left            =   2760
      TabIndex        =   5
      Top             =   5400
      Width           =   1215
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Command1"
      Height          =   375
      Left            =   120
      TabIndex        =   4
      Top             =   5400
      Width           =   1215
   End
   Begin VB.TextBox Text2 
      Height          =   285
      Left            =   2880
      TabIndex        =   3
      Text            =   "Text2"
      Top             =   360
      Width           =   1095
   End
   Begin VB.ListBox List2 
      Height          =   4545
      Left            =   2880
      TabIndex        =   2
      Top             =   720
      Width           =   1095
   End
   Begin VB.TextBox Text1 
      Height          =   285
      Left            =   120
      TabIndex        =   1
      Text            =   "Text1"
      Top             =   360
      Width           =   2655
   End
   Begin VB.ListBox List1 
      Height          =   4545
      Left            =   120
      TabIndex        =   0
      Top             =   720
      Width           =   2655
   End
   Begin VB.Label Label2 
      Caption         =   "Label2"
      Height          =   255
      Left            =   3000
      TabIndex        =   7
      Top             =   120
      Width           =   1215
   End
   Begin VB.Label Label1 
      Caption         =   "Label1"
      Height          =   255
      Left            =   120
      TabIndex        =   6
      Top             =   120
      Width           =   1575
   End
End
Attribute VB_Name = "Vokabel_Eingeben_Info"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
