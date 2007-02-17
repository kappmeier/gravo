Public Class xlsLDF
  Inherits xlsLDFBaseEx

  Private m_sWord As String

  Public Sub New(ByVal langDefFile As String)
    MyBase.New()

    LoadLDF(langDefFile) ' laden der datei
  End Sub

  Public Sub UseRule(ByVal wtWord As xlsWord, ByVal i As Integer) 'i gibt an, welche position benutzt wird. beginn bei 1
    Dim cRuleSet As Collection = Me.RuleList(wtWord.WordType + 1)
    If cRuleSet Is Nothing Then Return
    If i = 0 Then Return
    If i > cRuleSet.Count Then
      Me.m_sWord = ""
      Return
    End If
    Dim cRules As Collection = cRuleSet(i)
    ' versuche, alle regeln zu benutzen, bis die erste benutzt worden ist oder alle getestet wurden
    Dim c As Integer = 1
    Dim bUsed As Boolean = False
    Dim ldfrule As xlsLDFRule
    Do While (c <= cRules.Count And bUsed = False)
      ldfrule = cRules(c)
      ldfrule.TryRule(wtWord)
      bUsed = ldfrule.RuleUsed
      c += 1
    Loop
    If ldfrule Is Nothing Then m_sWord = "" Else m_sWord = ldfrule.ExtendedForm
  End Sub

  Public ReadOnly Property Output() As String
    Get
      Return m_sWord
    End Get
  End Property
End Class
