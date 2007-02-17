Imports System.Data.OleDb

Public Structure xlsVocInputGroupListInfo
	Public Table As String
	Public Description As String
	Public Type As String
	Public Language As String
End Structure

Public Class xlsDBBase
	Inherits xlsBase

	' Klassenvariablen
	Private aGroups As ArrayList	' enthält nur die gruppennamen
	Private cGroups As Collection	' enthält die Gruppen mit allen Infos
	Private aUnits As ArrayList = New ArrayList
	Private cUnits As Collection = New Collection

	' Klassenzustände
	Private m_bTableSelected As Boolean = False	' ob ein Vokabelset gewählt wurde
	Private m_bUnitSelected As Boolean = False
	Private m_xTable As xlsVocInputGroupListInfo	' Information zur Gruppe an sich
	Private m_xUnit As xlsUnitListInfo
	Private m_iWordNumber As Integer

	Sub New()
	End Sub

  Sub New(ByVal db As AccessDatabaseOperation, ByVal sTable As String)  ' Bestimmte Tabelle zum Zugriff öffnen
    MyClass.new(db)
    m_bTableSelected = True
  End Sub

  Sub New(ByVal db As AccessDatabaseOperation)
    MyBase.New(db)
    LoadGroupInfos()
    m_bTableSelected = False
  End Sub

	Public ReadOnly Property CountGroups() As Integer
		Get
			Return cGroups.Count
		End Get
	End Property

	Public ReadOnly Property CountUnits() As Integer
		Get
			Return cUnits.Count
		End Get
	End Property

	Public ReadOnly Property CurrentUnit() As xlsUnitListInfo
		Get
			Return m_xUnit
		End Get
	End Property

	Public ReadOnly Property CurrentUnitName() As String
		Get
			Return m_xUnit.Name
		End Get
	End Property

	Public ReadOnly Property CurrentUnitNumber() As Integer
		Get
			Return m_xUnit.Number
		End Get
	End Property

	Public ReadOnly Property CurrentGroup() As xlsVocInputGroupListInfo
		Get
			Return m_xTable
		End Get
	End Property

	Public ReadOnly Property CurrentGroupName() As String
		Get
			Return m_xTable.Table
		End Get
	End Property

  Public Function GetUnitName(ByVal iUnitNumber As Int32) As String
    ' liest zu einer gegebnenen unit-nummer den namen ein
    If IsGroupSelected() = False Then Throw New Exception("Keine Gruppe ausgewählt.")
    Dim sCommand As String
    sCommand = "SELECT Name FROM " & CurrentGroupName & "Units WHERE Nummer = " & iUnitNumber & ";"
    ExecuteReader(sCommand)
    DBCursor.Read()
    If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then Return DBCursor.GetString(0)
    DBCursor.Close()
    Return ""
  End Function

	Public Function GroupDescriptionToName(ByVal sDescription As String) As String
		Dim i As Integer		  ' Index
		For i = 1 To Me.cGroups.Count
			If cGroups.Item(i).Description = sDescription Then Return cGroups.Item(i).Table
		Next i
		Return ""
	End Function

	Public ReadOnly Property GroupNames() As ArrayList
		Get
			Return aGroups
		End Get
	End Property

	Public ReadOnly Property Groups() As Collection
		Get
			Return cGroups
		End Get
	End Property

	Public Function IsGroupSelected() As Boolean
		Return m_bTableSelected
	End Function

	Public Function IsUnitSelected() As Boolean
		Return m_bUnitSelected
	End Function

	ReadOnly Property Language() As String
		Get
			Dim sCommand As String
      If IsConnected() = False Then Throw New Exception("Nicht mit der Datenbank verbunden")
      If Trim(CurrentGroupName) = "" Then Throw New Exception("Falsche Gruppenbezeichnung")

			Dim sLanguage As String

			sCommand = "SELECT Lang FROM tables WHERE tables.Table='" & CurrentGroupName & "';"
			ExecuteReader(sCommand)
			DBCursor.Read()
			If TypeOf (DBCursor.GetValue(0)) Is DBNull Then sLanguage = "" Else sLanguage = DBCursor.GetValue(0)

			Return sLanguage
		End Get
	End Property

	ReadOnly Property LDFType() As String
		Get
			Dim sCommand As String
      If IsConnected() = False Then Throw New Exception("Nicht verbunden")
      If Trim(CurrentGroupName) = "" Then Throw New Exception("Falsche Gruppenbezeichnung")

			Dim sLDFType As String

			sCommand = "SELECT Type FROM tables WHERE tables.Table='" & CurrentGroupName & "';"
			ExecuteReader(sCommand)
			DBCursor.Read()
			If TypeOf (DBCursor.GetValue(0)) Is DBNull Then sLDFType = "" Else sLDFType = DBCursor.GetValue(0)

			Return sLDFType
		End Get
	End Property

	Protected Sub LoadGroupInfos()
		Dim structGroup As xlsVocInputGroupListInfo
		Dim sCommand As String

		' Laden der Gruppeninformationen
		cGroups = New Collection		  ' Arraylist erstellen
		aGroups = New ArrayList
		sCommand = "SELECT * FROM tables ORDER BY Group;"
		ExecuteReader(sCommand)
		Do While DBCursor.Read
			structGroup = New xlsVocInputGroupListInfo
			If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then structGroup.Description = DBCursor.GetValue(0) Else structGroup.Description = ""
			If Not TypeOf (DBCursor.GetValue(1)) Is DBNull Then structGroup.Table = DBCursor.GetValue(1) Else structGroup.Table = ""
			If Not TypeOf (DBCursor.GetValue(2)) Is DBNull Then structGroup.Language = DBCursor.GetValue(2) Else structGroup.Type = ""
			If Not TypeOf (DBCursor.GetValue(3)) Is DBNull Then structGroup.Type = DBCursor.GetValue(3) Else structGroup.Language = ""
			cGroups.Add(structGroup, structGroup.Table)
			aGroups.Add(structGroup.Table)			 ' hinzufügen in die arraylist
		Loop
		DBCursor.Close()
        ' Gruppe auswählen
        ' TODO In LoadGroupInfos wird SelectGroup aufgerufen, aber es wird die in der mutterklasse aufgerufen,
        ' wo noch gar nichts initialisiert worden ist!
		If CountGroups > 0 Then SelectGroup(aGroups(0)) Else Exit Sub
	End Sub

	Protected Sub LoadUnitInfos()
		If Not Me.IsGroupSelected Then Exit Sub ' TODO exception
		Dim wtUnit As New xlsUnitListInfo
		Dim sCommand As String
		cUnits = New Collection
		aUnits.Clear()

		sCommand = "SELECT DISTINCT Nummer, Name FROM " & CurrentGroupName & "Units ORDER BY Nummer;"
		ExecuteReader(sCommand)
		Do While DBCursor.Read
			wtUnit = New xlsUnitListInfo
			If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then wtUnit.Number = DBCursor.GetInt32(0)
			If Not TypeOf (DBCursor.GetValue(0)) Is DBNull Then wtUnit.Name = DBCursor.GetString(1)
			cUnits.Add(wtUnit, wtUnit.Name)
			aUnits.Add(wtUnit.Name)
		Loop
		DBCursor.Close()
		m_bUnitSelected = False
	End Sub

	Public Overridable Sub SelectGroup(ByVal sGroupname As String)
		If CountGroups = 0 Then Exit Sub
		If Not aGroups.Contains(sGroupname) Then MsgBox("nicht enthalten") : Exit Sub ' TODO Exception
		m_bTableSelected = True
		m_xTable = cGroups.Item(sGroupname)
		LoadUnitInfos()		  ' Informationen über die Units laden
		If CountUnits > 0 Then SelectUnit(1) Else m_bUnitSelected = False
	End Sub

	Public Overridable Sub SelectUnit(ByVal iUnitNumber As Int32)
		SelectUnit(GetUnitName(iUnitNumber))
	End Sub

	Public Overridable Sub SelectUnit(ByVal sUnitName As String)
		If aUnits.Contains(sUnitName) = False Then Exit Sub
		m_xUnit = cUnits(sUnitName)
		m_bUnitSelected = True
	End Sub

	Public ReadOnly Property UnitNames() As ArrayList
		Get
			Return aUnits
		End Get
	End Property

	Public ReadOnly Property Units() As Collection
		Get
			Return cUnits
		End Get
	End Property
End Class
