Public Class xlsLDFRule
	Inherits xlsLDFBase

	Private m_Rules As Collection
	Private m_bRuleUsed As Boolean
	Private m_sOutput As String

	Sub New(ByRef ldfRuleBlock As Collection)
		ldfRuleBlock.Remove(1)
		ldfRuleBlock.Remove(ldfRuleBlock.Count)
		m_Rules = ldfRuleBlock
	End Sub

	Public Sub TryRule(ByVal wtWord As xlsWord)
		' Die Regeln der Reihe nach abarbeiten
		Dim bStop As Boolean = False
		Dim i As Integer = 1
		m_bRuleUsed = False
		Do While i <= m_Rules.Count And Not bStop
			Select Case m_Rules.Item(i).left
				Case "Pre"			  ' Pre-Wert kontrollieren
					If Not wtWord.Pre = m_Rules.Item(i).right Then
						bStop = True
					End If
				Case "WordRight"
					If Not Right(wtWord.Word, Len(m_Rules.Item(i).right)) = m_Rules.Item(i).right Then
						bStop = True
					End If
				Case "Force"				' Irreguläres Wort forcieren
					If m_Rules.Item(i).right = "Irregular" Then
						m_sOutput = "LDF_FORCE_IRREGULAR"
						m_bRuleUsed = True
						bStop = True
					Else					  ' Reguläres Wort. Ausgabe ist schon belegt, schleife stoppen
						m_bRuleUsed = True
						bStop = True
					End If
				Case "SetExtended"				' Extended-Wert setzen
					m_sOutput = m_Rules.Item(i).right
				Case "AddExtended"				' Zum vorhandenen Extended-Wert etwas hinzufügen
					m_sOutput &= m_Rules.Item(i).right
				Case "CutExtendedRight"
					m_sOutput = Left(m_sOutput, Len(m_sOutput) - Val(m_Rules.Item(i).right))
				Case "CopyExtended"				' Ein schon vorhandenes Datenfeld in Extended kopieren
					m_sOutput = wtWord.Word
			End Select
			i += 1
		Loop
	End Sub

	ReadOnly Property RuleUsed() As Boolean
		Get
			Return m_bRuleUsed
		End Get
	End Property

	ReadOnly Property ExtendedForm() As String
		Get
			Return m_sOutput
		End Get
	End Property
End Class
