<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Options
  Inherits MyForm

  'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Wird vom Windows Form-Designer benötigt.
  Private components As System.ComponentModel.IContainer

  'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
  'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
  'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
    Me.cmdOK = New System.Windows.Forms.Button
    Me.cmdCancel = New System.Windows.Forms.Button
    Me.chkTestTargetLanguage = New System.Windows.Forms.CheckBox
    Me.chkTestSetPhrases = New System.Windows.Forms.CheckBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.chkSaveWindowPosition = New System.Windows.Forms.CheckBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.updownCardsInitialInterval = New System.Windows.Forms.NumericUpDown
    Me.Label3 = New System.Windows.Forms.Label
    Me.chkUseCards = New System.Windows.Forms.CheckBox
    Me.Label4 = New System.Windows.Forms.Label
    Me.Label5 = New System.Windows.Forms.Label
    Me.cmdCopyCards = New System.Windows.Forms.Button
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.updownCardsInitialInterval, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.cmdOK, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.cmdCancel, 1, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(231, 255)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.cmdOK.Location = New System.Drawing.Point(3, 3)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(67, 23)
    Me.cmdOK.TabIndex = 6
    Me.cmdOK.Text = "OK"
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Location = New System.Drawing.Point(76, 3)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(67, 23)
    Me.cmdCancel.TabIndex = 7
    Me.cmdCancel.Text = "Abbrechen"
    '
    'chkTestFormerLanguage
    '
    Me.chkTestTargetLanguage.AutoSize = True
    Me.chkTestTargetLanguage.Location = New System.Drawing.Point(12, 25)
    Me.chkTestTargetLanguage.Name = "chkTestFormerLanguage"
    Me.chkTestTargetLanguage.Size = New System.Drawing.Size(195, 17)
    Me.chkTestTargetLanguage.TabIndex = 1
    Me.chkTestTargetLanguage.Text = "Frage Wörter in fremder Sprache ab"
    Me.chkTestTargetLanguage.UseVisualStyleBackColor = True
    '
    'chkTestSetPhrases
    '
    Me.chkTestSetPhrases.AutoSize = True
    Me.chkTestSetPhrases.Location = New System.Drawing.Point(12, 48)
    Me.chkTestSetPhrases.Name = "chkTestSetPhrases"
    Me.chkTestSetPhrases.Size = New System.Drawing.Size(153, 17)
    Me.chkTestSetPhrases.TabIndex = 2
    Me.chkTestSetPhrases.Text = "Frage Redewendungen ab"
    Me.chkTestSetPhrases.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 9)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(140, 13)
    Me.Label1.TabIndex = 3
    Me.Label1.Text = "Standard-Test-Einstellungen"
    '
    'chkSaveWindowPosition
    '
    Me.chkSaveWindowPosition.AutoSize = True
    Me.chkSaveWindowPosition.Location = New System.Drawing.Point(12, 175)
    Me.chkSaveWindowPosition.Name = "chkSaveWindowPosition"
    Me.chkSaveWindowPosition.Size = New System.Drawing.Size(146, 17)
    Me.chkSaveWindowPosition.TabIndex = 5
    Me.chkSaveWindowPosition.Text = "Fensterposition speichern"
    Me.chkSaveWindowPosition.UseVisualStyleBackColor = True
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 159)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(111, 13)
    Me.Label2.TabIndex = 5
    Me.Label2.Text = "Anzeige Einstellungen"
    '
    'updownCardsInitialInterval
    '
    Me.updownCardsInitialInterval.Location = New System.Drawing.Point(70, 116)
    Me.updownCardsInitialInterval.Maximum = New Decimal(New Integer() {1024, 0, 0, 0})
    Me.updownCardsInitialInterval.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.updownCardsInitialInterval.Name = "updownCardsInitialInterval"
    Me.updownCardsInitialInterval.Size = New System.Drawing.Size(82, 20)
    Me.updownCardsInitialInterval.TabIndex = 4
    Me.updownCardsInitialInterval.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(12, 77)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(77, 13)
    Me.Label3.TabIndex = 7
    Me.Label3.Text = "Lernstrategien:"
    '
    'chkUseCards
    '
    Me.chkUseCards.AutoSize = True
    Me.chkUseCards.Location = New System.Drawing.Point(12, 93)
    Me.chkUseCards.Name = "chkUseCards"
    Me.chkUseCards.Size = New System.Drawing.Size(115, 17)
    Me.chkUseCards.TabIndex = 3
    Me.chkUseCards.Text = "Karteikartensystem"
    Me.chkUseCards.UseVisualStyleBackColor = True
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(12, 118)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(52, 13)
    Me.Label4.TabIndex = 9
    Me.Label4.Text = "Startwert:"
    '
    'Label5
    '
    Me.Label5.Location = New System.Drawing.Point(231, 159)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(142, 73)
    Me.Label5.TabIndex = 10
    Me.Label5.Text = "Hinweis: Das Karteikartensystem ist immer aktiviert, der Startwert ist 1. Diese O" & _
        "ption ist erst in späteren Versionen aktiviert."
    '
    'cmdCopyCards
    '
    Me.cmdCopyCards.Location = New System.Drawing.Point(234, 101)
    Me.cmdCopyCards.Name = "cmdCopyCards"
    Me.cmdCopyCards.Size = New System.Drawing.Size(140, 46)
    Me.cmdCopyCards.TabIndex = 11
    Me.cmdCopyCards.Text = "Globale Karteikarten in Gruppen kopieren"
    Me.cmdCopyCards.UseVisualStyleBackColor = True
    '
    'Options
    '
    Me.AcceptButton = Me.cmdOK
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(389, 296)
    Me.Controls.Add(Me.cmdCopyCards)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.chkUseCards)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.updownCardsInitialInterval)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.chkSaveWindowPosition)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.chkTestSetPhrases)
    Me.Controls.Add(Me.chkTestTargetLanguage)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "Options"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Optionen"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.updownCardsInitialInterval, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents cmdOK As System.Windows.Forms.Button
  Friend WithEvents cmdCancel As System.Windows.Forms.Button
  Friend WithEvents chkTestTargetLanguage As System.Windows.Forms.CheckBox
  Friend WithEvents chkTestSetPhrases As System.Windows.Forms.CheckBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents chkSaveWindowPosition As System.Windows.Forms.CheckBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents updownCardsInitialInterval As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents chkUseCards As System.Windows.Forms.CheckBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents cmdCopyCards As System.Windows.Forms.Button

End Class
