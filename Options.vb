Imports System.Windows.Forms

Public Class Options
  Dim initialized = False

  Dim m_testFormerLanguage As Boolean = False
  Dim m_testSetPhrases As Boolean = False

  Dim m_saveWindowPosition As Boolean = False

  Dim m_useCards As Boolean
  Dim m_CardsInitialInterval As Integer

  Dim oldValue As Integer = 1

  ' Lokalisierung
  Public Overrides Sub LocalizationChanged()

  End Sub

  Private Sub OK(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    TestFormerLanguage = chkTestFormerLanguage.Checked
    TestSetPhrases = chkTestSetPhrases.Checked
    SaveWindowPosition = chkSaveWindowPosition.Checked
    UseCards = chkUseCards.Checked
    CardsInitialInterval = updownCardsInitialInterval.Value
    Me.Close()
  End Sub

  Private Sub Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub Options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    chkTestFormerLanguage.Checked = TestFormerLanguage
    chkTestSetPhrases.Checked = TestSetPhrases
    chkSaveWindowPosition.Checked = SaveWindowPosition
    chkUseCards.Checked = UseCards
    updownCardsInitialInterval.Value = CardsInitialInterval
    oldValue = CardsInitialInterval
    initialized = True
  End Sub

  ' Eigenschaften für die Objekte
  Public Property TestFormerLanguage() As Boolean
    Get
      Return m_testFormerLanguage
    End Get
    Set(ByVal value As Boolean)
      m_testFormerLanguage = value
    End Set
  End Property

  Public Property TestSetPhrases() As Boolean
    Get
      Return m_testSetPhrases
    End Get
    Set(ByVal value As Boolean)
      m_testSetPhrases = value
    End Set
  End Property

  Public Property SaveWindowPosition() As Boolean
    Get
      Return m_saveWindowPosition
    End Get
    Set(ByVal value As Boolean)
      m_saveWindowPosition = value
    End Set
  End Property

  Public Property UseCards() As Boolean
    Get
      Return m_useCards
    End Get
    Set(ByVal value As Boolean)
      m_useCards = value
    End Set
  End Property

  Public Property CardsInitialInterval() As Integer
    Get
      Return m_CardsInitialInterval
    End Get
    Set(ByVal value As Integer)
      m_CardsInitialInterval = value
    End Set
  End Property

  Private Sub cardsInitialInterval_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles updownCardsInitialInterval.ValueChanged
    If Not initialized Then Exit Sub
    Dim newValue As Integer
    If updownCardsInitialInterval.Value > oldValue Then newValue = oldValue * 2 Else newValue = oldValue / 2
    If newValue < updownCardsInitialInterval.Minimum Then newValue = updownCardsInitialInterval.Minimum
    If newValue > updownCardsInitialInterval.Maximum Then newValue = updownCardsInitialInterval.Maximum
    updownCardsInitialInterval.Value = newValue
    oldValue = newValue
  End Sub

  Private Sub cmdCopyCards_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyCards.Click
        Dim db As DataBaseOperation = New SQLiteDataBaseOperation()
        db.Open(DBPath)
        Dim man As New xlsManagement(db)
        man.CopyGobalCardsToGroups()
  End Sub
End Class