Public Class xlsLDFBase
	Private m_sCurrentLDF As String
	Private m_ldfCommandLines As Collection
	Private m_ldfLastError As xlsLanguageDefinitionErrors


	Public Sub New()
		m_ldfLastError = xlsLanguageDefinitionErrors.NoErrors
	End Sub

	Protected Function CheckLDF(ByVal ldfCommandlines As Collection) As xlsLanguageDefinitionErrors
		If ldfCommandlines(1).left <> "LDF" Then Return xlsLanguageDefinitionErrors.NoLDF Else Return xlsLanguageDefinitionErrors.NoErrors
	End Function

	Protected ReadOnly Property CommandLines() As Collection
		Get
			Return m_ldfCommandLines
		End Get
	End Property

	Public ReadOnly Property CurrentFile() As String
		Get
			Return m_sCurrentLDF
		End Get
	End Property

	Protected Function GetCommand(ByVal LeftPart As String, ByVal ldfCommandlines As Collection) As xlsLDFLine
		Dim i As Integer
		For i = 1 To ldfCommandlines.Count()
			If ldfCommandlines(i).Left = LeftPart Then Exit For
		Next i

		If i > ldfCommandlines.Count Then
			Return Nothing
		Else
			Dim ldfRet As xlsLDFLine
			ldfRet = ldfCommandlines(i)
			ldfCommandlines.Remove(i)
			Return ldfRet
		End If
	End Function

	Protected Function GetCommandBlock(ByVal StartPos As Integer, ByVal ldfCommandlines As Collection) As Collection
		Dim sEndLeft As String = ldfCommandlines(StartPos).left & "End"
		Dim sEndRight As String = ldfCommandlines(StartPos).right
		Dim ldfCommandBlock As New Collection

		Do
			ldfCommandBlock.Add(ldfCommandlines(StartPos))
			ldfCommandlines.Remove(StartPos)
		Loop Until (ldfCommandlines(StartPos).left = sEndLeft) And (ldfCommandlines(StartPos).right = sEndRight)
		ldfCommandBlock.Add(ldfCommandlines(StartPos))
		ldfCommandlines.Remove(StartPos)
		Return ldfCommandBlock
	End Function

	Protected Function GetCommandBlockStripped(ByVal StartPos As Integer, ByVal ldfCommandlines As Collection) As Collection
		Dim sEndLeft As String = ldfCommandlines(StartPos).left & "End"
		Dim sEndRight As String = ldfCommandlines(StartPos).right
		Dim ldfCommandBlock As New Collection

		Do
			ldfCommandBlock.Add(ldfCommandlines(StartPos))
			ldfCommandlines.Remove(StartPos)
		Loop Until (ldfCommandlines(StartPos).left = sEndLeft) And (ldfCommandlines(StartPos).right = sEndRight)
		ldfCommandBlock.Add(ldfCommandlines(StartPos))
		ldfCommandlines.Remove(StartPos)
		ldfCommandBlock.Remove(1)
		ldfCommandBlock.Remove(ldfCommandBlock.Count)
		Return ldfCommandBlock
	End Function

	Protected Function GetCommandBlockStartPos(ByVal LeftPart As String, ByVal RightPart As String, ByVal ldfCommandlines As Collection) As Integer
		Dim i As Integer
		For i = 1 To ldfCommandlines.Count()
			If ldfCommandlines(i).Left = LeftPart And ldfCommandlines(i).right = RightPart Then Exit For
		Next i
		If i > ldfCommandlines.Count Then Return 0 Else Return i
	End Function

	Protected Function GetCommandRight(ByVal LeftPart As String, ByVal ldfCommandlines As Collection) As String
		Dim i As Integer
		For i = 1 To ldfCommandlines.Count()
			If ldfCommandlines(i).Left = LeftPart Then Exit For
		Next i

		If i > ldfCommandlines.Count Then
			Return ""
		Else
			Dim sRet As String
			sRet = ldfCommandlines(i).right
			ldfCommandlines.Remove(i)
			Return sRet
		End If
	End Function

	Protected Function GetLeftCommandPart(ByVal Command As String) As String
		Dim iColonPos As Integer
		iColonPos = InStr(Command, ":")
		Return Trim(Left(Command, iColonPos - 1))
	End Function

	Protected Function GetRightCommandPart(ByVal Command As String) As String
		Dim iColonPos As Integer
		iColonPos = InStr(Command, ":")
		Return Trim(Mid(Command, iColonPos + 1))
	End Function

	Public Overridable Sub LoadLDF(ByVal sFile As String)
		' Ganze Funktion einlesen und in eine Liste von LDFLines packen
		' erstes einlesen, keine informationsverarbeitung
		' TODO path-unabhängig machen
		m_ldfCommandLines = New Collection

		Dim ldfLine As xlsLDFLine
		m_sCurrentLDF = sFile		 'Application.StartupPath() & "\" & sFile & ".ldf"
		FileOpen(1, m_sCurrentLDF, OpenMode.Input)
		While Not EOF(1)
			Dim sLine As String
			ldfLine = New xlsLDFLine
			sLine = LineInput(1)
			ldfLine.Left = GetLeftCommandPart(sLine)
			ldfLine.Right = GetRightCommandPart(sLine)
			m_ldfCommandLines.Add(ldfLine)
		End While
		FileClose(1)
	End Sub

	Public ReadOnly Property LastError() As xlsLanguageDefinitionErrors
		Get
			Return Me.m_ldfLastError
		End Get
	End Property

	Protected Sub SetError(ByVal ldfError As xlsLanguageDefinitionErrors)
		Me.m_ldfLastError = ldfError
	End Sub
End Class
