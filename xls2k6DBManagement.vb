Imports System.Data.OleDb

Public Class xlsDBManagement
	Inherits xlsDBBase

  Private sVersionHistory() As String = {"3.00", "2.10", "2.01", "2.00", "1.22", "1.21", "1.20", "1.11", "1.10", "1.00"}    ' Neuste vorne

	Sub New()
		MyBase.New()
	End Sub

  Sub New(ByVal db As AccessDatabaseOperation)
    MyBase.New(db)
  End Sub

	Public Sub AddGroup(ByVal sGroupName As String, ByVal sLanguage As String, ByVal sLDFType As String)
		' sLanguage ist ein beliebiger string, allerdings sinnvoll in zusammenhang mit dem LDF system

		' Feststellen, welche Table-Nummer die neue Gruppe hat
		Dim iCount As Integer
		Dim sCommand As String
		sCommand = "SELECT COUNT(Lang) FROM tables WHERE Lang='" & sLanguage & "';"
		ExecuteReader(sCommand)
		DBCursor.Read()
		If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then iCount = DBCursor.GetInt32(0) Else iCount = 0
		DBCursor.Close()

		Dim sNewTable As String
		If iCount + 1 < 10 Then
			sNewTable = sLanguage & "0" & Trim(Str(iCount + 1))
		Else
			sNewTable = sLanguage & Trim(Str(iCount + 1))
		End If

		sCommand = "INSERT INTO tables VALUES ("
		sCommand &= "'" & xlsVocInput.AddHighColons(sGroupName) & "',"
		sCommand &= "'" & sNewTable & "',"
		sCommand &= "'" & sLanguage & "',"
		sCommand &= "'" & sLDFType & "'"
		sCommand &= ");"
		ExecuteNonQuery(sCommand)

		CreateTables(sNewTable)

		LoadGroupInfos()	   ' Informationen neu laden
	End Sub

	Public Shadows Sub AddUnit(ByVal sName As String)
		Dim sCommand As String
		sCommand = "SELECT COUNT(Nummer) FROM " & CurrentGroupName & "Units"
		ExecuteReader(sCommand)
		DBCursor.Read()
		Dim iCount As Integer
		If TypeOf DBCursor.GetValue(0) Is DBNull Then iCount = 1 Else iCount = DBCursor.GetValue(0) + 1
		DBCursor.Close()

		sCommand = "INSERT INTO " & CurrentGroupName & "Units VALUES("
		sCommand &= iCount & ","
    sCommand &= "'" & AddHighColons(sName) & "')"
		ExecuteNonQuery(sCommand)

		Me.LoadUnitInfos()
	End Sub

  Private Sub CreateTables(ByVal TableName As String)
    CreateTableWords(TableName)
    CreateTableWordsStats(TableName)
    CreateTableWordsUnits(TableName)
  End Sub

	Private Sub CreateTableWords(ByVal Name As String)
		' Erstellt Tabellen nach Version 1.20
		Dim sCommandText As String
		sCommandText = "CREATE TABLE " & Name & " (UnitNumber INTEGER, ChapterNumber INTEGER, Word TEXT(50), "
		sCommandText += "WordNumber INTEGER, WordInUnit INTEGER, WordType INTEGER, MustKnow BIT, Pre TEXT(50), "
		sCommandText += "Post TEXT(50), Meaning1 TEXT(50), IrregularForm BIT, "
		sCommandText += "Irregular1 TEXT(50), Irregular2 TEXT(50), Irregular3 TEXT(50), Description TEXT(80), Deleted BIT, "
		sCommandText += "AdditionalTargetLangInfo TEXT(80))"
		ExecuteNonQuery(sCommandText)
	End Sub

	Private Sub CreateTableWordsStats(ByVal Name As String)
		Dim sCommandText As String
		sCommandText = "CREATE TABLE " & Name & "Stats (WordNumber INTEGER, Abfragen INTEGER, AbfragenGesamt INTEGER, "
		sCommandText += "Richtig INTEGER, Falsch INTEGER, FalschGesamt INTEGER, ErsteAbfrage DATETIME, LetzteAbfrage DATETIME, "
    sCommandText += "AbfrageGestartet BIT, Hilfe1Richtig INT, Hilfe2Richtig INT, Hilfe3Richtig INT, "
    sCommandText &= "NextTest INT, NextInterval INT)"
    ExecuteNonQuery(sCommandText)
  End Sub

	Private Sub CreateTableWordsUnits(ByVal Name As String)
		Dim sCommandText As String
		sCommandText = "CREATE TABLE " & Name & "Units (Nummer INTEGER, Name TEXT(50))"
		ExecuteNonQuery(sCommandText)
		sCommandText = "CREATE UNIQUE INDEX IndexDoppelt ON " & Name & "Units (Nummer)"
		ExecuteNonQuery(sCommandText)
	End Sub

	Function DatabaseVersion() As String
    If IsConnected() = False Then Return "" ' TODO exception werfen
		Return sVersionHistory(DatabaseVersionIndex)
	End Function

	Function DatabaseVersion(ByVal Index As Integer) As String
		Return sVersionHistory(Index)
	End Function

	Function DatabaseVersionIndex() As Integer
		If IsConnected() = False Then Exit Function ' TODO exception werfen
		Dim bFound As Boolean = False
		Dim i As Integer = 0
		Do Until bFound = True		  'Or i = sVersionHistory.Length spätestens bei 1.00 ist schluß
      Dim sCommand As String = "SELECT Version FROM Version WHERE Version='" & sVersionHistory(i) & "'"
			ExecuteReader(sCommand)
			DBCursor.Read()
			Try
				If DBCursor.GetValue(0) Then bFound = True Else i += 1
			Catch e As Exception
				i += 1
			End Try
		Loop
		DBCursor.Close()
		Return i
	End Function

	Public Sub InsertUnit(ByVal sName As String, ByVal iNumber As Integer)

	End Sub

	Public Sub MoveUnit(ByVal iNumber As Integer, ByVal iNewNumber As Integer)

	End Sub

	Public Sub MoveUnit(ByVal sName As String, ByVal iNewNumber As Integer)

	End Sub

	Public Function NextVersionIndex() As Integer
		If IsConnected() = False Then Exit Function ' TODO exception werfen
		If DatabaseVersionIndex() = 0 Then
			Return 0
		Else
			Return DatabaseVersionIndex() - 1
		End If
	End Function

  Sub Reorganize()
    ' Durchsuchen aller Felder NextTest und NextInterval ob da überhall Werte > 1 gespeichert sind
    Dim i As Integer
    Dim sCommand As String

    Dim dbc As CDBOperation = New CDBOperation()
    dbc.Open(DBConnection.Path)

    For i = 1 To Groups.Count
      sCommand = "SELECT NextTest, NextInterval, WordNumber FROM " & Groups(i).Table & "Stats"
      ExecuteReader(sCommand)
      'DBCursor.Read()
      'If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then iWordCount = DBCursor.GetValue(0) Else iWordCount = 0

      Do While DBCursor.Read
        Dim iNext, iInterval, iWordNumber As Integer
        If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then iNext = DBCursor.GetInt32(0) Else iNext = 1
        If Not TypeOf (DBCursor.GetValue(1)) Is DBNull Then iInterval = DBCursor.GetInt32(1) Else iInterval = 1
        If Not TypeOf (DBCursor.GetValue(2)) Is DBNull Then iWordNumber = DBCursor.GetInt32(2) Else iWordNumber = 0
        If iWordNumber > 0 Then
          ' Aktualisieren der anderen Einträge
          sCommand = "UPDATE " & Groups(i).Table & "Stats SET NextTest=" & iNext & ", NextInterval=" & iInterval & " WHERE WordNumber=" & iWordNumber & ";"
          dbc.ExecuteNonQuery(sCommand) ' hier wird eine zweite Datenbankverbindung benutzt
        End If
      Loop
    Next i
  End Sub

  Sub UpdateDatabaseVersion()
    Dim sCommand As String = ""
    ' Die Datenbank auf die neueste Version bringen.
    Dim i As Integer
    Dim iVersion As Integer = DatabaseVersionIndex()
    If iVersion = 0 Then Exit Sub
    Select Case sVersionHistory(iVersion)
      Case "1.00"      ' Startversion
        ' Einfügen der Versions-Zählung
        sCommand = "INSERT INTO Version VALUES('1.10', '24.10.2003', 'Versionsinfo')"
      Case "1.10"      ' Versionsinfo schon hinzugefügt
        ' Hinzufügen von Beschreibungen zu den Wörtern
        For i = 0 To Groups.Count - 1
          sCommand = "ALTER TABLE " & Groups(i).Table & " ADD COLUMN Description TEXT(80);"
          ExecuteNonQuery(sCommand)
        Next i
        sCommand = "INSERT INTO Version VALUES('1.11', '25.10.2003', 'Beschreibung')"
      Case "1.11"      ' Beschreibung schon hinzugefügt
        ' Hinzufügen von Lösch-Feldern
        For i = 0 To Groups.Count - 1
          sCommand = "ALTER TABLE " & Groups(i).Table & " ADD COLUMN Deleted BIT;"
          ExecuteNonQuery(sCommand)
        Next i
        sCommand = "INSERT INTO Version VALUES('1.20', '26.10.2003', 'Löschen')"
      Case "1.20"      ' Löschen schon hinzugefügt
        ' Hinzufügen von Stat-Informationen für Hilfe
        For i = 0 To Groups.Count - 1
          sCommand = "ALTER TABLE " & Groups(i).Table & "Stats ADD COLUMN Hilfe1Richtig INT;"
          ExecuteNonQuery(sCommand)
          sCommand = "ALTER TABLE " & Groups(i).Table & "Stats ADD COLUMN Hilfe2Richtig INT;"
          ExecuteNonQuery(sCommand)
          sCommand = "ALTER TABLE " & Groups(i).Table & "Stats ADD COLUMN Hilfe3Richtig INT;"
          ExecuteNonQuery(sCommand)
        Next i
        sCommand = "INSERT INTO Version VALUES('1.21', '29.02.2004', 'Hilfe')"
      Case "1.21"      'Hilfe schon hinzugefügt
        ' Hinzufügen von Stat-Informationen für Hilfe
        For i = 0 To Groups.Count - 1
          sCommand = "ALTER TABLE " & Groups(i).Table & " ADD COLUMN AdditionalTargetLangInfo TEXT(80);"
          ExecuteNonQuery(sCommand)
        Next i
        sCommand = "INSERT INTO Version VALUES('1.22', '15.09.2004', 'Zusatzinfo')"
      Case "1.22"      ' Zusatzinformation schon hinzugefügt
        ' Alle Vokabeln aus den drei Bedeutungsfeldern zusammenfassen.
        For i = 0 To GroupNames().Count - 1
          Dim j As Integer
          Dim iWordCount As Int32
          Dim sMeaning1, sMeaning2, sMeaning3 As String
          ' Die Gruppe laden 
          SelectGroup(GroupNames.Item(i))
          sCommand = "SELECT COUNT(WordNumber) FROM " & CurrentGroupName
          ExecuteReader(sCommand)
          DBCursor.Read()
          If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then iWordCount = DBCursor.GetValue(0) Else iWordCount = 0
          ' alle Vokabeln durchgehen
          For j = 1 To iWordCount
            sCommand = "SELECT Meaning1, Meaning2, Meaning3 FROM " & CurrentGroupName & " WHERE WordNumber= " & j
            ExecuteReader(sCommand)
            DBCursor.Read()            ' muesste eigentlich gehen, da nummern nicht gelöscht werden können
            If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then sMeaning1 = DBCursor.GetValue(0) Else sMeaning1 = ""
            If Not TypeOf (DBCursor.GetValue(1)) Is DBNull Then sMeaning2 = DBCursor.GetValue(1) Else sMeaning2 = ""
            If Not TypeOf (DBCursor.GetValue(2)) Is DBNull Then sMeaning3 = DBCursor.GetValue(2) Else sMeaning3 = ""
            If Trim(sMeaning2) <> "" Then sMeaning1 = Trim(sMeaning1) & ";" & Trim(sMeaning2)
            If Trim(sMeaning3) <> "" Then sMeaning1 = Trim(sMeaning1) & ";" & Trim(sMeaning3)
            DBCursor.Close()
            sCommand = "UPDATE " & CurrentGroupName & " SET Meaning1 = '" & AddHighColons(sMeaning1) & "' WHERE WordNumber = " & j & ";"
            Me.ExecuteNonQuery(sCommand)
          Next j
          ' Löschen der Spalten
          sCommand = "ALTER TABLE " & CurrentGroupName & " DROP COLUMN Meaning2, Meaning3;"
          ExecuteNonQuery(sCommand)
        Next i
        sCommand = "INSERT INTO Version VALUES('2.00', '10.12.2005', 'Wortbedeutungen')"
      Case "2.00"      ' Beliebige Anzahl an Wortbedeutungen schon hinzugefügt
        ' Zu Table eine neue spalte hinzufügen

        sCommand = "ALTER TABLE Tables ADD COLUMN Type TEXT(16)"
        ExecuteNonQuery(sCommand)
                sCommand = "UPDATE Tables SET Type = 'std'"
        ExecuteNonQuery(sCommand)
        sCommand = "INSERT INTO Version VALUES('2.01', '12.12.2005', 'LDF-Typ')"
      Case "2.01"
        ' Möglichkeiten für stärkes Abfrage-Management
        ' Abfrage wie mit Karteikarten möglich --> zwei neue Integer-Spalten in den Stats-Tables
        ' In der ersten wird gespeichert, wie viele Abfragen noch passieren müssen, bis das Wort erneut
        ' abgefragt wird. In der zweiten wird gespeichert, welches Intervall bei der nächsten Abfrage
        ' benutzt werden soll.
        ' Das Intervall wird bei richtiger beantwortung verdoppelt, bei falscher Beantwortung halbiert.
        ' Das Minimum wird 1 sein.
        For i = 1 To Groups.Count
          sCommand = "ALTER TABLE " & Groups(i).Table & "Stats ADD COLUMN NextTest INT;"
          ExecuteNonQuery(sCommand)
          sCommand = "ALTER TABLE " & Groups(i).Table & "Stats ADD COLUMN NextInterval INT;"
          ExecuteNonQuery(sCommand)
        Next i
        sCommand = "INSERT INTO Version VALUES('2.10', '13.07.2006', 'Karteikarten')"
      Case "2.10" ' Karteikarten-Abfrage schon hinzugefügt
        sCommand = ""
        ' Wurde manuell durchgeführt, evtl. später mal hier einfügen:
        ' hinzufügen von tabellen DictionaryMain, DictionaryWords als haupt-datenbanken aus denen die gruppen wörter
        ' auswählen.
        sCommand = "INSERT INTO Version VALUES('3.00', '01.09.2006', 'Update')"
      Case "3.00" ' Neue organisation schon eingefügt
        ' Aktuelle Version
        ' sCommand = "INSERT INTO Version VALUES('3.00', '01.09.2006', 'Update')"
        sCommand = ""
    End Select
    ExecuteNonQuery(sCommand)
  End Sub

  Public Sub UpdateGroup(ByVal sOldName As String, ByVal sNewName As String)
    ' Namen einer Gruppe ändern
    If Trim(sNewName) = "" Then Exit Sub ' TODO Exception, außerdem prüfen ob überhaupt existiert
    Dim sCommand As String
    sCommand = "UPDATE tables SET tables.Group='" & AddHighColons(sNewName) & "' WHERE tables.Group='" & sOldName & "'"
    ExecuteNonQuery(sCommand)

    ' In den Listen ändern, dazu einfach neu einlesen (langsam aber einfach ;) es gibt wahrscheinlich nicht viele lektionen)
    Dim sCurrent As String
    If CurrentGroupName = sOldName Then sCurrent = sNewName Else sCurrent = CurrentGroupName
    Me.LoadGroupInfos()     ' Protected-Methode aufrufen zum aktualisieren. evtl später mal mittels update-methode
    Me.SelectGroup(sCurrent)
  End Sub

  Public Sub UpdateUnit(ByVal UnitNumber As Integer, ByVal NewName As String)
    Dim sCommand As String
    sCommand = "UPDATE " & CurrentGroupName & "Units SET Name='" & xlsVocInput.AddHighColons(NewName) & "' WHERE Nummer=" & UnitNumber
    ExecuteNonQuery(sCommand)
    Me.LoadUnitInfos()
  End Sub
End Class
