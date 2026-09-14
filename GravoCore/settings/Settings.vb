Imports System.Globalization

Public Structure WindowSettings
    Dim name As String
    Dim posX As Integer
    Dim posY As Integer
    Dim height As Integer
    Dim width As Integer
End Structure

''' <summary>
''' Window state as persisted in the settings. Numerically identical to c>System.Windows.Forms.FormWindowState</c>.
''' </summary>
Public Enum WindowStateSetting
    Normal = 0
    Minimized = 1
    Maximized = 2
End Enum

Public Class Settings
    Private ReadOnly store As ISettingsStore

    ' Test-Einstellungen
    Dim m_testSetPhrases As Boolean
    Dim m_queryLanguage As QueryLanguage
    Dim m_useCards As Boolean
    Dim m_CardsInitialInterval As Integer

    ' Fenster-Position
    Dim m_saveWindowPosition As Boolean
    Dim m_mainWindowState As WindowStateSetting
    Dim m_childWindowState As WindowStateSetting
    Dim m_mainWindow As WindowSettings
    Dim m_explorerWindow As WindowSettings
    Dim m_groupWindow As WindowSettings
    Dim m_statisticWindow As WindowSettings

    ' Gruppen
    Dim m_lastGroup As String = ""
    Dim m_lastSubGroup As String = ""

    ' Grundeinrichtungen und Konstruktor
    Public Sub New(store As ISettingsStore)
        Me.store = store
    End Sub

    Public Sub LoadSettings()
        Dim values As IDictionary(Of String, String) = store.Load()
        QueryLanguage = If(LoadBool(values, "TestTargetLanguage", True), QueryLanguage.TargetLanguage, QueryLanguage.OriginalLanguage)
        TestSetPhrases = LoadBool(values, "TestSetPhrases", False)
        SaveWindowPosition = LoadBool(values, "SaveWindowPosition", False)
        LastGroup = LoadString(values, "LastGroup", "")
        LastSubGroup = LoadString(values, "LastSubGroup", "")
        ExplorerWindowSettings = LoadWindowSettings(values, "WindowSettingsExplorer", 300, 400, 0, 0)
        GroupWindowSettings = LoadWindowSettings(values, "WindowSettingsGroups", 300, 400, 22, 29)
        MainWindowSettings = LoadWindowSettings(values, "WindowSettingsMain", 600, 800, 0, 0)
        StatisticWindowSettings = LoadWindowSettings(values, "WindowSettingsStatistic", 300, 400, 44, 58)
        MainWindowState = LoadWindowState(values, "MainWindowState")
        ChildWindowState = LoadWindowState(values, "ChildWindowState")
        UseCards = LoadBool(values, "UseCards", True)
        CardsInitialInterval = LoadInt(values, "CardsInitialInterval", 1)
    End Sub

    Public Sub SaveSettings()
        Dim targetLanguage As Boolean
        Select Case QueryLanguage
            Case QueryLanguage.OriginalLanguage
                targetLanguage = False
            Case QueryLanguage.TargetLanguage
                targetLanguage = True
            Case Else
                Throw New ArgumentException("Query type " & CInt(QueryLanguage) & " not supported.")
        End Select
        Dim values As New Dictionary(Of String, String)()
        StoreBool(values, "TestTargetLanguage", targetLanguage)
        StoreBool(values, "TestSetPhrases", TestSetPhrases)
        StoreBool(values, "SaveWindowPosition", SaveWindowPosition)
        StoreString(values, "LastGroup", LastGroup)
        StoreString(values, "LastSubGroup", LastSubGroup)
        StoreWindowSettings(values, ExplorerWindowSettings)
        StoreWindowSettings(values, GroupWindowSettings)
        StoreWindowSettings(values, MainWindowSettings)
        StoreWindowSettings(values, StatisticWindowSettings)
        StoreWindowState(values, "MainWindowState", MainWindowState)
        StoreWindowState(values, "ChildWindowState", ChildWindowState)
        StoreBool(values, "UseCards", UseCards)
        StoreInt(values, "CardsInitialInterval", CardsInitialInterval)
        store.Save(values)
    End Sub

    ' Funktionen und Methoden zum Speichern und Laden
    Private Shared Sub StoreBool(values As IDictionary(Of String, String), name As String, value As Boolean)
        If value Then StoreInt(values, name, 1) Else StoreInt(values, name, 0)
    End Sub

    Private Shared Function LoadBool(values As IDictionary(Of String, String), name As String, defaultValue As Boolean) As Boolean
        Dim val As Integer = LoadInt(values, name, If(defaultValue, 1, 0))
        If val = 0 Then Return False Else Return True
    End Function

    Private Shared Sub StoreInt(values As IDictionary(Of String, String), name As String, value As Integer)
        values(name) = value.ToString(CultureInfo.InvariantCulture)
    End Sub

    Private Shared Function LoadInt(values As IDictionary(Of String, String), name As String, defaultValue As Integer) As Integer
        Dim text As String = Nothing
        Dim val As Integer
        If values.TryGetValue(name, text) AndAlso Integer.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, val) Then Return val
        Return defaultValue
    End Function

    Private Shared Sub StoreString(values As IDictionary(Of String, String), name As String, value As String)
        values(name) = value
    End Sub

    Private Shared Function LoadString(values As IDictionary(Of String, String), name As String, defaultValue As String) As String
        Dim text As String = Nothing
        If values.TryGetValue(name, text) Then Return text
        Return defaultValue
    End Function

    Private Shared Sub StoreWindowSettings(values As IDictionary(Of String, String), value As WindowSettings)
        StoreInt(values, value.name & "Height", value.height)
        StoreInt(values, value.name & "Width", value.width)
        StoreInt(values, value.name & "PosX", value.posX)
        StoreInt(values, value.name & "PosY", value.posY)
    End Sub

    Private Shared Function LoadWindowSettings(values As IDictionary(Of String, String), name As String, height As Integer, width As Integer, posX As Integer, posY As Integer) As WindowSettings
        Dim val As WindowSettings
        val.name = name
        val.height = LoadInt(values, name & "Height", height)
        val.width = LoadInt(values, name & "Width", width)
        val.posX = LoadInt(values, name & "PosX", posX)
        val.posY = LoadInt(values, name & "PosY", posY)
        Return val
    End Function

    Private Shared Sub StoreWindowState(values As IDictionary(Of String, String), name As String, value As WindowStateSetting)
        StoreInt(values, name, CInt(value))
    End Sub

    Private Shared Function LoadWindowState(values As IDictionary(Of String, String), name As String) As WindowStateSetting
        Dim val As Integer = LoadInt(values, name, CInt(WindowStateSetting.Normal))
        If [Enum].IsDefined(GetType(WindowStateSetting), val) Then Return CType(val, WindowStateSetting)
        Return WindowStateSetting.Normal
    End Function

    ' Eigenschaften zum Abrufen der Einstellungen
    Public Property QueryLanguage() As QueryLanguage
        Get
            Return m_queryLanguage
        End Get
        Set(ByVal value As QueryLanguage)
            m_queryLanguage = value
        End Set
    End Property

    Public Property TestSetPhrases() As Boolean
        Get
            Return m_testSetPhrases
        End Get
        Set(ByVal value As Boolean)
            m_testSetPhrases = value
        End Set
    End Property

    Public Property SaveWindowPosition() As Boolean
        Get
            Return m_saveWindowPosition
        End Get
        Set(ByVal value As Boolean)
            m_saveWindowPosition = value
        End Set
    End Property

    Public Property LastGroup() As String
        Get
            Return m_lastGroup
        End Get
        Set(ByVal value As String)
            m_lastGroup = value
        End Set
    End Property

    Public Property LastSubGroup() As String
        Get
            Return m_lastSubGroup
        End Get
        Set(ByVal value As String)
            m_lastSubGroup = value
        End Set
    End Property

    Public Property ExplorerWindowSettings() As WindowSettings
        Get
            Return m_explorerWindow
        End Get
        Set(ByVal value As WindowSettings)
            m_explorerWindow = value
        End Set
    End Property

    Public Property GroupWindowSettings() As WindowSettings
        Get
            Return m_groupWindow
        End Get
        Set(ByVal value As WindowSettings)
            m_groupWindow = value
        End Set
    End Property

    Public Property MainWindowSettings() As WindowSettings
        Get
            Return m_mainWindow
        End Get
        Set(ByVal value As WindowSettings)
            m_mainWindow = value
        End Set
    End Property

    Public Property StatisticWindowSettings() As WindowSettings
        Get
            Return m_statisticWindow
        End Get
        Set(ByVal value As WindowSettings)
            m_statisticWindow = value
        End Set
    End Property

    Public Property MainWindowState() As WindowStateSetting
        Get
            Return m_mainWindowState
        End Get
        Set(ByVal value As WindowStateSetting)
            m_mainWindowState = value
        End Set
    End Property

    Public Property ChildWindowState() As WindowStateSetting
        Get
            Return m_childWindowState
        End Get
        Set(ByVal value As WindowStateSetting)
            m_childWindowState = value
        End Set
    End Property

    Public Property UseCards() As Boolean
        Get
            Return m_useCards
        End Get
        Set(ByVal value As Boolean)
            m_useCards = value
        End Set
    End Property

    Public Property CardsInitialInterval() As Integer
        Get
            Return m_CardsInitialInterval
        End Get
        Set(ByVal value As Integer)
            m_CardsInitialInterval = value
        End Set
    End Property
End Class
