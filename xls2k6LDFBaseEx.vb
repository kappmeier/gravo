Public Class xlsLDFBaseEx
	Inherits xlsLDFBase

	Private m_iVersionMajor As Integer
	Private m_iVersionMinor As Integer
	Private m_sLanguageDesc As String
	Private m_sLanguageTarget As String
	Private m_ldfLanguageInfo As xlsLanguageInfo
	Private m_cFormList As Collection
	Private m_cFormDesc As Collection
	Private m_cFormDescEx As Collection

	Private m_bQuickLoad As Boolean

	Sub New()
		MyBase.New()
		m_cFormList = New Collection
		m_cFormDesc = New Collection
		m_cFormDescEx = New Collection
		m_bQuickLoad = False
	End Sub

	Public ReadOnly Property FormList() As Collection
		Get
			Return Me.m_cFormList
		End Get
	End Property

	Public ReadOnly Property FormDesc() As Collection
		Get
			Return Me.m_cFormDesc
		End Get
	End Property

	Public ReadOnly Property FormDescEx() As Collection
		Get
			Return Me.m_cFormDescEx
		End Get
	End Property

	Private Sub LoadFormInfos()
		Dim ldfCommandLines As Collection = Me.CommandLines
		Dim ldfCommandBlock As Collection
		Dim ldfInnerBlock As Collection
		Dim ldfFormBlock As Collection
		Dim ldfExtended As ArrayList
		Dim ldfExtendedEx As ArrayList

		Me.m_cFormDesc = New Collection
		Me.m_cFormDescEx = New Collection

		Dim i As Integer		 ' Index
		Dim j As Integer		 ' Index
		For i = 1 To Me.m_cFormList.Count
			ldfCommandBlock = Me.GetCommandBlock(getcommandblockstartpos("Form", m_cFormList.Item(i).left, ldfCommandLines), ldfCommandLines)
			' ldfCommandBlock enthält nun den Block für diese Wortform
			' Finde alle validen Formen von 1 bis 3 heraus
			ldfInnerBlock = Me.GetCommandBlockStripped(getcommandblockstartpos("Extended", "", ldfCommandBlock), ldfCommandBlock)
			' Für jeden Eintrag durchlaufen
			ldfExtended = New ArrayList
			ldfExtendedEx = New ArrayList
			For j = 1 To ldfInnerBlock.Count		  ' TODO Begrenzung auf drei einfügen oder entfernen
				ldfFormBlock = Me.GetCommandBlockStripped(getcommandblockstartpos("Extended", j, ldfCommandBlock), ldfCommandBlock)
				ldfExtended.Add(GetCommandRight("Desc", ldfFormBlock))
				ldfExtendedEx.Add(GetCommandRight("DescEx", ldfFormBlock))
			Next j
			For j = 1 To 3 - ldfExtended.Count
				ldfExtended.Add("")
				ldfExtendedEx.Add("")
			Next j
			m_cFormDesc.Add(ldfExtended)
			m_cFormDescEx.Add(ldfExtendedEx)
		Next i

	End Sub

	Private Sub LoadFormList()
		'Forms:
		'noun:Substantiv
		'verb:Verb
		'adjective:Adjektiv
		'simple:Einfache
		'adverb:Adverb
		'FormsEnd:
		Dim ldfCommandLines As Collection = Me.CommandLines

		m_cFormList = GetCommandBlockstripped(GetCommandBlockStartPos("Forms", "", ldfCommandLines), ldfCommandLines)
		'm_cFormList.Remove(1)
		'm_cFormList.Remove(m_cFormList.Count())

		Dim i As Integer
		Dim ldfFormCreator As xlsFormCreator
		Dim ldfFormCommandBlock As Collection
	End Sub

	Private Sub LoadLanguageInfo()
		' TODO LoadLanguageInfo in die Base-Klasse verschieben und in xlsLDFLanguageDefinition löschen
		'Language:
		'Name:french
		'Desc:Französisch
		'DescEx:Französisch – Standard
		'TestDir: mtw
		'LanguageEnd:
		Dim xlsLanguage As xlsLanguageInfo
		Dim sLastError As String
		Dim ldfCommandLines = Me.CommandLines

		Dim ldfCommandBlock As Collection = GetCommandBlock(GetCommandBlockStartPos("Language", "", ldfCommandLines), ldfCommandLines)

		xlsLanguage.Name = GetCommandRight("Name", ldfCommandBlock)
		xlsLanguage.Type = GetCommandRight("Type", ldfCommandBlock)
		xlsLanguage.Description = GetCommandRight("Desc", ldfCommandBlock)
		xlsLanguage.DescriptionEx = GetCommandRight("DescEx", ldfCommandBlock)
		If GetCommandRight("TestDir", ldfCommandBlock) = "mtw" Then
			xlsLanguage.TestDirection = xlsLanguageTestDirection.TestWord
		ElseIf GetCommandRight("TestDir", ldfCommandBlock) = "wtm" Then
			xlsLanguage.TestDirection = xlsLanguageTestDirection.TestMeaning
		Else
			Me.SetError(xlsLanguageDefinitionErrors.UnexpectedLanguageInfoTestDir)
			'Return sLastError TODO exception werfen
		End If
		m_ldfLanguageInfo = xlsLanguage
	End Sub

	Public Overrides Sub LoadLDF(ByVal sFile As String)
		MyBase.LoadLDF(sFile)		  ' Laden der Zeilen
		LoadLanguageInfo()
		If m_bQuickLoad = False Then
			LoadMainInfo()
			LoadFormList()
			LoadFormInfos()
		End If

	End Sub

	Private Sub LoadMainInfo()
		'Version:1.0
		'LangDesc:Deutsch
		'LangTarg:german
		Dim ldfLine As xlsLDFLine
		Dim ldfCommandLines As Collection = Me.CommandLines

		ldfLine = GetCommand("Version", ldfCommandLines)
		m_iVersionMajor = Val(Left(ldfLine.Right, 1))
		m_iVersionMinor = Val(Right(ldfLine.Right, 2))
		m_sLanguageDesc = GetCommandRight("LangDesc", ldfCommandLines)
		m_sLanguageTarget = GetCommandRight("LangTarg", ldfCommandLines)
	End Sub

	Public ReadOnly Property LanguageInfo() As xlsLanguageInfo
		Get
			Return Me.m_ldfLanguageInfo
		End Get
	End Property

	Protected Property QuickLoad() As Boolean
		Get
			Return m_bQuickLoad
		End Get
		Set(ByVal Value As Boolean)
			m_bQuickLoad = Value
		End Set
	End Property

	Public ReadOnly Property Version() As String
		Get
			Return m_iVersionMajor & "." & m_iVersionMinor
		End Get
	End Property

	Public ReadOnly Property VersionMajor() As Integer
		Get
			Return m_iVersionMajor
		End Get
	End Property

	Public ReadOnly Property VersionMinor() As Integer
		Get
			Return m_iVersionMinor
		End Get
	End Property
End Class
