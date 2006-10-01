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

Public Structure xlsLDFExtededDesc
	Public Extended1 As String
	Public Extended2 As String
	Public Extended3 As String
End Structure

Public Enum xlsLanguageTestDirection
	TestWord
	TestMeaning
End Enum

Public Structure xlsLanguageInfo
	Public Name As String
	Public Type As String
	Public Description As String
	Public DescriptionEx As String
	Public TestDirection As xlsLanguageTestDirection
End Structure

Public Structure xlsLDFLine
	Public Left As String
	Public Right As String
End Structure

Public Class xlsLanguageDefinitionOld
	Inherits xlsLDFBase
	Dim m_bLoaded As Boolean = False
	Dim m_xlsLanguageInfo As xlsLanguageInfo

	Dim m_sLastError As xlsLanguageDefinitionErrors

	Protected m_ldfFormCreator As Collection

	Sub New()

	End Sub

	Sub New(ByVal Language As String)
		'LoadLDF(Language)
	End Sub

	Public Overrides Sub LoadLDF(ByVal Language As String)
		MyBase.LoadLDF(Language)

		Dim ldfcommandlines As Collection = Me.CommandLines

		'If CheckLDF(ldfcommandlines) = Not xlsLanguageDefinitionErrors.NoErrors Then m_sLastError = xlsLanguageDefinitionErrors.NoLDF : Return m_sLastError ' Fehlerhafte Datei
		'LoadMainInfo(ldfcommandlines)
		'LoadLanguageInfo(ldfCommandLines)
		'LoadFormList(ldfcommandlines)

		' Die Liste der Formen-Klassen vorbereiten
		m_ldfFormCreator = New Collection
		'FormCreator erzeugen
		'For i = 1 To m_ldfFormList.Count
		'	' Alle Zeilen zu einer Form heraussuchen
		'	ldfFormCommandBlock = GetCommandBlock(GetCommandBlockStartPos("Form", m_ldfFormList(i).left, ldfcommandlines), ldfcommandlines)
		'	' Aus diesen Zeilen eine Klasse zum erzeugen der Formen erstellen
		'	ldfFormCreator = New xlsFormCreator(ldfFormCommandBlock)
		'	m_ldfFormCreator.Add(ldfFormCreator)
		'Next i
	End Sub

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
      'Dim i As Integer
			Dim sList As New Collection
			'For i = 1 To m_ldfFormList.Count
			'sList.Add(m_ldfFormList(i).right)
			'Next i
			Return sList
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
    Dim iType As Integer = wtWord.WordType()
		Dim i As Integer = 0
		Do
			i += 1
		Loop Until m_ldfFormCreator(i).formname = FormName Or i >= m_ldfFormCreator.Count
		If i >= m_ldfFormCreator.Count Then Return "" Else Return m_ldfFormCreator(i).createform(wtWord)
	End Function
End Class