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

  Private m_cRules As Collection ' enth�lt f�r jede sprache eine unter-collection in selber reihenfolge

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

  Protected Overridable Sub LoadFormInfos()
    Dim ldfCommandLines As Collection = Me.CommandLines
    Dim ldfCommandBlock As Collection
    Dim ldfInnerBlock As Collection
    Dim ldfFormBlock As Collection
    Dim ldfExtended As ArrayList
    Dim ldfExtendedEx As ArrayList

    Me.m_cFormDesc = New Collection
    Me.m_cFormDescEx = New Collection
    m_cRules = New Collection

    Dim i As Integer     ' Index
    Dim j As Integer     ' Index

    For i = 1 To Me.m_cFormList.Count
      ldfCommandBlock = Me.GetCommandBlock(GetCommandBlockStartPos("Form", m_cFormList.Item(i).left, ldfCommandLines), ldfCommandLines)
      ' ldfCommandBlock enth�lt nun den Block f�r diese Wortform
      ' Finde alle validen Formen von 1 bis 3 heraus
      ldfInnerBlock = Me.GetCommandBlockStripped(GetCommandBlockStartPos("Extended", "", ldfCommandBlock), ldfCommandBlock)
      ' F�r jeden Eintrag durchlaufen
      ldfExtended = New ArrayList
      ldfExtendedEx = New ArrayList

      Dim ldfFormRules As Collection = New Collection ' speichert f�r eine Wortart alle drei typen

      For j = 1 To ldfInnerBlock.Count      ' TODO Begrenzung auf drei einf�gen oder entfernen
        ldfFormBlock = Me.GetCommandBlockStripped(GetCommandBlockStartPos("Extended", j, ldfCommandBlock), ldfCommandBlock)
        ldfExtended.Add(GetCommandRight("Desc", ldfFormBlock))
        ldfExtendedEx.Add(GetCommandRight("DescEx", ldfFormBlock))

        ' rules-liste f�llen
        Dim ldfRule As xlsLDFRule
        Dim ldfThisForm As Collection = New Collection
        Do While ldfFormBlock.Count > 1
          ldfRule = New xlsLDFRule(GetCommandBlock(1, ldfFormBlock))
          ldfThisForm.Add(ldfRule)
        Loop
        ldfFormRules.Add(ldfThisForm)
      Next j
      m_cRules.Add(ldfFormRules)
      For j = 1 To 3 - ldfExtended.Count
        ldfExtended.Add("")
        ldfExtendedEx.Add("")
      Next j
      m_cFormDesc.Add(ldfExtended)
      m_cFormDescEx.Add(ldfExtendedEx)
    Next i
  End Sub

	Private Sub LoadFormList()
    Dim ldfCommandLines As Collection = Me.CommandLines

		m_cFormList = GetCommandBlockstripped(GetCommandBlockStartPos("Forms", "", ldfCommandLines), ldfCommandLines)
  End Sub

	Private Sub LoadLanguageInfo()
		' TODO LoadLanguageInfo in die Base-Klasse verschieben und in xlsLDFLanguageDefinition l�schen
		'Language:
		'Name:french
		'Desc:Franz�sisch
		'DescEx:Franz�sisch � Standard
		'TestDir: mtw
		'LanguageEnd:
		Dim xlsLanguage As xlsLanguageInfo
    'Dim sLastError As String
    Dim ldfCommandLines As Collection = Me.CommandLines()

		Dim ldfCommandBlock As Collection = GetCommandBlock(GetCommandBlockStartPos("Language", "", ldfCommandLines), ldfCommandLines)

		xlsLanguage.Name = GetCommandRight("Name", ldfCommandBlock)
		xlsLanguage.Type = GetCommandRight("Type", ldfCommandBlock)
		xlsLanguage.Description = GetCommandRight("Desc", ldfCommandBlock)
		xlsLanguage.DescriptionEx = GetCommandRight("DescEx", ldfCommandBlock)
		If GetCommandRight("TestDir", ldfCommandBlock) = "mtw" Then
      xlsLanguage.TestDirection = xlsLanguageTestDirection.TestMeaning
		ElseIf GetCommandRight("TestDir", ldfCommandBlock) = "wtm" Then
      xlsLanguage.TestDirection = xlsLanguageTestDirection.TestWord
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

  Public ReadOnly Property RuleList() As Collection
    Get
      Return Me.m_cRules
    End Get
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
