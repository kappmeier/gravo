Public Class xlsLDFBase
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

	Protected Function GetCommandBlockStartPos(ByVal LeftPart As String, ByVal RightPart As String, ByVal ldfCommandlines As Collection) As Integer
		Dim i As Integer
		For i = 1 To ldfCommandlines.Count()
			If ldfCommandlines(i).Left = LeftPart And ldfCommandlines(i).right = RightPart Then Exit For
		Next i
		If i > ldfCommandlines.Count Then Return 0 Else Return i
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

End Class
