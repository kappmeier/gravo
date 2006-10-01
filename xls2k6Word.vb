Public Class xlsWord
  Inherits xlsWordBase

  Sub New(ByVal db As AccessDatabaseOperation)
    MyBase.new(db)
  End Sub

  Sub New(ByVal db As AccessDatabaseOperation, ByVal iWordNumber As Integer, ByVal sTable As String)
    MyBase.New(db)
    Table = sTable
    LoadWord(iWordNumber, sTable)
  End Sub

	Public Sub Update()
		' Speichern der geänderten informationen
    If IsConnected() = False Or Valid = False Then Exit Sub

		Dim sCommand As String
    sCommand = "UPDATE " & SelectedGroup() & " SET Word='" & AddHighColons(Word)
    sCommand &= "', Pre='" & AddHighColons(Pre)
    sCommand &= "', Post='" & AddHighColons(Post)
    sCommand &= "', AdditionalTargetLangInfo='" & AddHighColons(AdditionalTargetLangInfo)
    sCommand &= "', Description='" & AddHighColons(Description)
    sCommand &= "' WHERE WordNumber=" & WordNumber
		ExecuteNonQuery(sCommand)

		'Meaning
		Dim sMeaning As String = ""
		Dim i As Integer		  ' Index
    For i = 0 To MeaningList.Count - 1
      sMeaning = sMeaning & MeaningList.Item(i) & ";"
    Next i
		If Right(sMeaning, 1) = ";" Then sMeaning = Left(sMeaning, Len(sMeaning) - 1)

    sCommand = "UPDATE " & SelectedGroup() & " SET Meaning1='" & AddHighColons(sMeaning) & "' WHERE WordNumber=" & WordNumber
		ExecuteNonQuery(sCommand)

    sCommand = "UPDATE " & SelectedGroup() & " SET Irregular1='" & AddHighColons(Extended1)
    sCommand &= "', Irregular2='" & AddHighColons(Extended2)
    sCommand &= "', Irregular3='" & AddHighColons(Extended3)
    sCommand &= "', IrregularForm=" & ExtendedIsValid
    sCommand &= ", WordType=" & WordType
    sCommand &= " WHERE WordNumber=" & WordNumber
		ExecuteNonQuery(sCommand)

		Exit Sub

    If ExtendedIsValid = False Then
      Extended1 = ""
      sCommand = "UPDATE " & SelectedGroup() & " SET Irregular1='" & AddHighColons(Extended1) & "' WHERE WordNumber=" & WordNumber & ";"
      ExecuteReader(sCommand)
      Extended2 = ""
      sCommand = "UPDATE " & SelectedGroup() & " SET Irregular2='" & AddHighColons(Extended1) & "' WHERE WordNumber=" & WordNumber & ";"
      ExecuteReader(sCommand)
      Extended3 = ""
      sCommand = "UPDATE " & SelectedGroup() & " SET Irregular2='" & AddHighColons(Extended1) & "' WHERE WordNumber=" & WordNumber & ";"
      ExecuteReader(sCommand)
    End If

    sCommand = "UPDATE " & SelectedGroup() & " SET MustKnow=" & MustKnow & " WHERE WordNumber=" & WordNumber & ";"
		ExecuteReader(sCommand)
    sCommand = "UPDATE " & SelectedGroup() & " SET ChapterNumber=" & Chapter & " WHERE WordNumber=" & WordNumber & ";"
		ExecuteReader(sCommand)
	End Sub
End Class