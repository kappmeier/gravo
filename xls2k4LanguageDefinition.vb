Public Enum xlsLanguageDefinitionErrors
	UnexpectedCommand
	NoLDF
	NoCommandLine
	NoLanguageInfo
	NoLanguageInfoName
	NoLanguageInfoDesc
	NoLanguageInfoDescEx
	NoLanguageInfoTestDir
	UnexpectedLanguageInfoTestDir
	NoErrors
End Enum

Public Enum xlsLanguageTestDirection
	TestWord
	TestMeaning
End Enum

Public Structure xlsLanguageInfo
	Public Name As String
	Public Description As String
	Public DescriptionEx As String
	Public TestDirection As xlsLanguageTestDirection
End Structure

Public Structure xlsLDFLine
	Public Left As String
	Public Right As String
End Structure

Public Class xlsLanguageDefinition
	Inherits xlsLDFBase
	Dim m_bLoaded As Boolean = False
	Dim m_iVersionMajor As Integer
	Dim m_iVersionMinor As Integer
	Dim m_sLanguageDesc As String
	Dim m_sLanguageTarget As String
	Dim m_xlsLanguageInfo As xlsLanguageInfo
	Dim m_sLDFPath As String

	Dim m_sLastError As xlsLanguageDefinitionErrors

	Protected m_ldfFormList As Collection
	Protected m_ldfFormCreator As Collection

	Sub New()

	End Sub

	Sub New(ByVal Language As String)
		m_sLastError = LoadLDF(Language)
	End Sub

	Public Function LoadLDF(ByVal Language As String) As xlsLanguageDefinitionErrors
		' Ganze Funktion einlesen und in eine Liste von LDFLines packen
		Dim ldfCommandLines As New Collection

		Dim ldfLine As xlsLDFLine
		m_sLDFPath = Application.StartupPath() & "\" & Language & ".ldf"
		FileOpen(1, m_sLDFPath, OpenMode.Input)
		While Not EOF(1)
			Dim sLine As String
			ldfLine = New xlsLDFLine
			sLine = LineInput(1)
			ldfLine.Left = GetLeftCommandPart(sLine)
			ldfLine.Right = GetRightCommandPart(sLine)
			ldfCommandLines.Add(ldfLine)
		End While
		FileClose(1)

		If CheckLDF(ldfCommandLines) = Not xlsLanguageDefinitionErrors.NoErrors Then m_sLastError = xlsLanguageDefinitionErrors.NoLDF : Return m_sLastError ' Fehlerhafte Datei
		LoadMainInfo(ldfCommandLines)
		LoadLanguageInfo(ldfCommandLines)
		LoadFormList(ldfCommandLines)
	End Function

	Protected Function CheckLDF(ByVal ldfCommandlines As Collection) As xlsLanguageDefinitionErrors
		If ldfCommandlines(1).left <> "LDF" Then Return xlsLanguageDefinitionErrors.NoLDF Else Return xlsLanguageDefinitionErrors.NoErrors
	End Function

	Protected Function LoadMainInfo(ByVal ldfCommandlines As Collection) As xlsLanguageDefinitionErrors
		'Version:1.0
		'LangDesc:Deutsch
		'LangTarg:german
		Dim ldfLine As xlsLDFLine
		ldfLine = GetCommand("Version", ldfCommandlines)
		m_iVersionMajor = Val(Left(ldfLine.Right, 1))
		m_iVersionMinor = Val(Right(ldfLine.Right, 2))
		m_sLanguageDesc = GetCommandRight("LangDesc", ldfCommandlines)
		m_sLanguageTarget = GetCommandRight("LangTarg", ldfCommandlines)
	End Function

	Protected Function LoadLanguageInfo(ByVal ldfCommandlines As Collection) As xlsLanguageDefinitionErrors
		'Language:
		'Name:french
		'Desc:Französisch
		'DescEx:Französisch – Standard
		'TestDir: mtw
		'LanguageEnd:
		Dim ldfCommandBlock As Collection = GetCommandBlock(GetCommandBlockStartPos("Language", "", ldfCommandlines), ldfCommandlines)
		m_xlsLanguageInfo.Name = GetCommandRight("Name", ldfCommandBlock)
		m_xlsLanguageInfo.Description = GetCommandRight("Desc", ldfCommandBlock)
		m_xlsLanguageInfo.DescriptionEx = GetCommandRight("DescEx", ldfCommandBlock)
		If GetCommandRight("TestDir", ldfCommandBlock) = "mtw" Then
			m_xlsLanguageInfo.TestDirection = xlsLanguageTestDirection.TestWord
		ElseIf GetCommandRight("TestDir", ldfCommandBlock) = "wtm" Then
			m_xlsLanguageInfo.TestDirection = xlsLanguageTestDirection.TestMeaning
		Else
			m_sLastError = xlsLanguageDefinitionErrors.UnexpectedLanguageInfoTestDir
			Return m_sLastError
		End If
		'Return xlsLanguageDefinitionErrors.NoLanguageInfoName
		'Return xlsLanguageDefinitionErrors.NoLanguageInfoDesc
		'Return xlsLanguageDefinitionErrors.NoLanguageInfoDescEx
		'Return xlsLanguageDefinitionErrors.NoLanguageInfoDesc
	End Function

	Protected Function LoadFormList(ByVal ldfCommandLines As Collection) As xlsLanguageDefinitionErrors
		'Forms:
		'noun:Substantiv
		'verb:Verb
		'adjective:Adjektiv
		'simple:Einfache
		'FormsEnd:
		m_ldfFormList = GetCommandBlock(GetCommandBlockStartPos("Forms", "", ldfCommandLines), ldfCommandLines)
		m_ldfFormList.Remove(1)
		m_ldfFormList.Remove(m_ldfFormList.Count())

		Dim i As Integer
		Dim ldfFormCreator As xlsFormCreator
		Dim ldfFormCommandBlock As Collection

		' Die Liste der Formen-Klassen vorbereiten
		m_ldfFormCreator = New Collection
		For i = 1 To m_ldfFormList.Count
			' Alle Zeilen zu einer Form heraussuchen
			ldfFormCommandBlock = GetCommandBlock(GetCommandBlockStartPos("Form", m_ldfFormList(i).left, ldfCommandLines), ldfCommandLines)
			' Aus diesen Zeilen eine Klasse zum erzeugen der Formen erstellen
			ldfFormCreator = New xlsFormCreator(ldfFormCommandBlock)
			m_ldfFormCreator.Add(ldfFormCreator)
		Next i
	End Function

	ReadOnly Property TestDirection() As xlsLanguageTestDirection
		Get
			Return m_xlsLanguageInfo.TestDirection
		End Get
	End Property

	Public Function FormNames(ByVal iWordType As Integer) As Collection
		Dim sList As Collection
		sList = m_ldfFormCreator(iWordType + 1).FormDesc
		Return sList
	End Function

	Public Function FormNamesEx(ByVal iWordType As Integer) As Collection
		Dim sList As Collection
		sList = m_ldfFormCreator(iWordType + 1).FormDescEx
		Return sList
	End Function

	Public ReadOnly Property FormList() As Collection
		Get
			Dim i As Integer
			Dim sList As New Collection
			For i = 1 To m_ldfFormList.Count
				sList.Add(m_ldfFormList(i).right)
			Next i
			Return sList
		End Get
	End Property

	Public ReadOnly Property LDFPath() As String
		Get
			Return m_sLDFPath
		End Get
	End Property

	Public ReadOnly Property LDFVersion() As String
		Get
			Return m_iVersionMajor & "." & m_iVersionMinor
		End Get
	End Property

	Public ReadOnly Property Language() As String
		Get
			Return Me.m_xlsLanguageInfo.Description
		End Get
	End Property

	Public ReadOnly Property LanguageEx() As String
		Get
			Return Me.m_xlsLanguageInfo.DescriptionEx
		End Get
	End Property

	Public ReadOnly Property InternalLanguage() As String
		Get
			Return Me.m_xlsLanguageInfo.Name
		End Get
	End Property

	Public ReadOnly Property LastError() As xlsLanguageDefinitionErrors
		Get
			Return m_sLastError
		End Get
	End Property

	Public Function CreateExtended1(ByVal wtWord As xlsWord) As String
		If wtWord.ExtendedIsValid Then Return wtWord.Extended1
		Return m_ldfFormCreator(wtWord.WordType + 1).createform(wtWord, 1)
	End Function

	Public Function CreateExtended2(ByVal wtWord As xlsWord) As String
		If wtWord.ExtendedIsValid Then Return wtWord.Extended2
		Return m_ldfFormCreator(wtWord.WordType + 1).createform(wtWord, 2)
	End Function

	Public Function CreateExtended3(ByVal wtWord As xlsWord) As String
		If wtWord.ExtendedIsValid Then Return wtWord.Extended3
		Return m_ldfFormCreator(wtWord.WordType + 1).createform(wtWord, 3)
	End Function

	Public Function CreateExtended1(ByVal FormName As String, ByVal wtWord As xlsWord) As String
		Dim iType = wtWord.WordType()
		Dim i As Integer = 0
		Do
			i += 1
		Loop Until m_ldfFormCreator(i).formname = FormName Or i >= m_ldfFormCreator.Count
		If i >= m_ldfFormCreator.Count Then Return "" Else Return m_ldfFormCreator(i).createform(wtWord)
	End Function
End Class

