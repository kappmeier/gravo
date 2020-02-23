<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestSelect
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
    Me.cmbGroup = New System.Windows.Forms.ComboBox
    Me.cmbSubGroup = New System.Windows.Forms.ComboBox
    Me.lblGroup = New System.Windows.Forms.Label
    Me.lblWordCount = New System.Windows.Forms.Label
    Me.lblSubGroup = New System.Windows.Forms.Label
    Me.chkTestDirection = New System.Windows.Forms.CheckBox
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
    Me.cmdOK = New System.Windows.Forms.Button
    Me.cmdCancel = New System.Windows.Forms.Button
    Me.chkTestMarked = New System.Windows.Forms.CheckBox
    Me.chkTestPhrases = New System.Windows.Forms.CheckBox
    Me.chkRandomOrder = New System.Windows.Forms.CheckBox
    Me.TableLayoutPanel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'cmbGroup
    '
    Me.cmbGroup.FormattingEnabled = True
    Me.cmbGroup.Location = New System.Drawing.Point(12, 25)
    Me.cmbGroup.Name = "cmbGroup"
    Me.cmbGroup.Size = New System.Drawing.Size(230, 21)
    Me.cmbGroup.TabIndex = 0
    '
    'cmbSubGroup
    '
    Me.cmbSubGroup.FormattingEnabled = True
    Me.cmbSubGroup.Location = New System.Drawing.Point(12, 65)
    Me.cmbSubGroup.Name = "cmbSubGroup"
    Me.cmbSubGroup.Size = New System.Drawing.Size(230, 21)
    Me.cmbSubGroup.TabIndex = 1
    '
    'lblGroup
    '
    Me.lblGroup.AutoSize = True
    Me.lblGroup.Location = New System.Drawing.Point(12, 9)
    Me.lblGroup.Name = "lblGroup"
    Me.lblGroup.Size = New System.Drawing.Size(45, 13)
    Me.lblGroup.TabIndex = 4
    Me.lblGroup.Text = "Gruppe:"
    '
    'lblWordCount
    '
    Me.lblWordCount.AutoSize = True
    Me.lblWordCount.Location = New System.Drawing.Point(12, 89)
    Me.lblWordCount.Name = "lblWordCount"
    Me.lblWordCount.Size = New System.Drawing.Size(14, 13)
    Me.lblWordCount.TabIndex = 5
    Me.lblWordCount.Text = "#"
    '
    'lblSubGroup
    '
    Me.lblSubGroup.AutoSize = True
    Me.lblSubGroup.Location = New System.Drawing.Point(12, 49)
    Me.lblSubGroup.Name = "lblSubGroup"
    Me.lblSubGroup.Size = New System.Drawing.Size(69, 13)
    Me.lblSubGroup.TabIndex = 6
    Me.lblSubGroup.Text = "Untergruppe:"
    '
    'chkTestDirection
    '
    Me.chkTestDirection.AutoSize = True
    Me.chkTestDirection.Location = New System.Drawing.Point(12, 105)
    Me.chkTestDirection.Name = "chkTestDirection"
    Me.chkTestDirection.Size = New System.Drawing.Size(182, 17)
    Me.chkTestDirection.TabIndex = 2
    Me.chkTestDirection.Text = "Wörter in fremder Sprache testen"
    Me.chkTestDirection.UseVisualStyleBackColor = True
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.cmdOK, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.cmdCancel, 1, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(100, 199)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 8
    '
    'cmdOK
    '
    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.cmdOK.Location = New System.Drawing.Point(3, 3)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(67, 23)
    Me.cmdOK.TabIndex = 5
    Me.cmdOK.Text = "OK"
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.Location = New System.Drawing.Point(76, 3)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(67, 23)
    Me.cmdCancel.TabIndex = 6
    Me.cmdCancel.Text = "Abbrechen"
    '
    'chkTestMarked
    '
    Me.chkTestMarked.AutoSize = True
    Me.chkTestMarked.Location = New System.Drawing.Point(12, 128)
    Me.chkTestMarked.Name = "chkTestMarked"
    Me.chkTestMarked.Size = New System.Drawing.Size(156, 17)
    Me.chkTestMarked.TabIndex = 3
    Me.chkTestMarked.Text = "Nur markierte Wörter testen"
    Me.chkTestMarked.UseVisualStyleBackColor = True
    '
    'chkTestSetPhrases
    '
    Me.chkTestPhrases.AutoSize = True
    Me.chkTestPhrases.Location = New System.Drawing.Point(12, 151)
    Me.chkTestPhrases.Name = "chkTestSetPhrases"
    Me.chkTestPhrases.Size = New System.Drawing.Size(153, 17)
    Me.chkTestPhrases.TabIndex = 4
    Me.chkTestPhrases.Text = "Frage Redewendungen ab"
    Me.chkTestPhrases.UseVisualStyleBackColor = True
    '
    'chkRandomOrder
    '
    Me.chkRandomOrder.AutoSize = True
    Me.chkRandomOrder.Location = New System.Drawing.Point(12, 174)
    Me.chkRandomOrder.Name = "chkRandomOrder"
    Me.chkRandomOrder.Size = New System.Drawing.Size(33, 17)
    Me.chkRandomOrder.TabIndex = 9
    Me.chkRandomOrder.Text = "#"
    Me.chkRandomOrder.UseVisualStyleBackColor = True
    '
    'TestSelect
    '
    Me.AcceptButton = Me.cmdOK
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(258, 240)
    Me.Controls.Add(Me.chkRandomOrder)
    Me.Controls.Add(Me.chkTestPhrases)
    Me.Controls.Add(Me.chkTestMarked)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.Controls.Add(Me.chkTestDirection)
    Me.Controls.Add(Me.lblSubGroup)
    Me.Controls.Add(Me.lblWordCount)
    Me.Controls.Add(Me.lblGroup)
    Me.Controls.Add(Me.cmbSubGroup)
    Me.Controls.Add(Me.cmbGroup)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "TestSelect"
    Me.Text = "Testgruppe auswählen..."
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cmbGroup As System.Windows.Forms.ComboBox
  Friend WithEvents cmbSubGroup As System.Windows.Forms.ComboBox
  Friend WithEvents lblGroup As System.Windows.Forms.Label
  Friend WithEvents lblWordCount As System.Windows.Forms.Label
  Friend WithEvents lblSubGroup As System.Windows.Forms.Label
  Friend WithEvents chkTestDirection As System.Windows.Forms.CheckBox
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents cmdOK As System.Windows.Forms.Button
  Friend WithEvents cmdCancel As System.Windows.Forms.Button
  Friend WithEvents chkTestMarked As System.Windows.Forms.CheckBox
  Friend WithEvents chkTestPhrases As System.Windows.Forms.CheckBox
  Friend WithEvents chkRandomOrder As System.Windows.Forms.CheckBox
End Class
