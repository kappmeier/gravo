Public Class xlsFormCreator
	Inherits xlsLDFBase

	Dim m_iExtended As Integer = 0
	Dim m_cDesc As Collection
	Dim m_cDescEx As Collection
	Dim m_xlsRuleList As Collection
	Dim m_sFormName As String


	Sub New(ByVal ldfFormBlock As Collection)
		' Collections erzeugen
		m_cDesc = New Collection
		m_cDescEx = New Collection

		' Extended Gruppen Herausfinden
		Dim i As Integer
		Dim ldfCommandBlock As Collection
		m_sFormName = Me.GetCommandRight("Form", ldfFormBlock)
		If GetCommandBlockStartPos("Extended", "", ldfFormBlock) = 0 Then Exit Sub
		ldfCommandBlock = GetCommandBlock(GetCommandBlockStartPos("Extended", "", ldfFormBlock), ldfFormBlock)
		ldfCommandBlock.Remove(1)
		m_iExtended = 0
		While ldfCommandBlock.Count > 1
			Select Case Me.GetCommandRight("ExtendedForm", ldfCommandBlock)
				Case 1
					m_iExtended = m_iExtended Or 1
				Case 2
					m_iExtended = m_iExtended Or 2
				Case 3
					m_iExtended = m_iExtended Or 4
			End Select
		End While
		ldfCommandBlock.Remove(1)
		m_xlsRuleList = New Collection
		If m_iExtended And 1 Then m_xlsRuleList.Add(GetNewExtended(1, ldfFormBlock))
		If m_iExtended And 2 Then m_xlsRuleList.Add(GetNewExtended(2, ldfFormBlock))
		If m_iExtended And 4 Then m_xlsRuleList.Add(GetNewExtended(3, ldfFormBlock))
	End Sub

	Protected Function GetNewExtended(ByVal Number As Integer, ByVal ldfFormBlock As Collection) As Collection
		Dim ldfExtendedBlock As Collection
		ldfExtendedBlock = GetCommandBlock(GetCommandBlockStartPos("Extended", Trim(Str(Number)), ldfFormBlock), ldfFormBlock)
		m_cDesc.Add(getcommandright("Desc", ldfExtendedBlock))
		m_cDescEx.Add(GetCommandRight("DescEx", ldfExtendedBlock))
		Dim ldfRuleBlock As Collection
		Dim ldfRule As xlsLDFRule

		Dim xlsRuleList = New Collection
		While Trim(ldfExtendedBlock.Item(2).left) = "Rule"
			ldfRuleBlock = Me.GetCommandBlock(2, ldfExtendedBlock)
			ldfRule = New xlsLDFRule(ldfRuleBlock)
			xlsRuleList.Add(ldfRule)
		End While
		Return xlsRuleList
	End Function

	ReadOnly Property FormName() As String
		Get
			Return Me.m_sFormName
		End Get
	End Property

	ReadOnly Property FormDesc() As Collection
		Get
			Return Me.m_cDesc
		End Get
	End Property

	ReadOnly Property FormDescEx() As Collection
		Get
			Return Me.m_cDescEx
		End Get
	End Property

	Public Function CreateForm(ByVal wtWord As xlsWord, ByVal iExtended As Integer) As String
		If m_xlsRuleList Is Nothing Then Return "" ' Es sind keine Regeln vorhanden
		If iExtended > m_xlsRuleList.Count Then Return "" ' Es gibt keine Regeln für diese Extended-Ausgabe
		Dim xlsRuleList As Collection = m_xlsRuleList(iExtended)		  ' Rules für diese Extended-Ausgabe
		Dim i As Integer = 0
		Do
			i += 1
			xlsRuleList(i).TryRule(wtWord)
		Loop Until xlsRuleList(i).ruleused = True Or i >= xlsRuleList.Count
		Return xlsRuleList(i).extendedform
	End Function
End Class
