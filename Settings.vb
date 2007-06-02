Imports Microsoft.VisualBasic.myservices

Public Structure WindowSettings
    Dim name As String
    Dim position As Point
    Dim height As Integer
    Dim width As Integer
End Structure

Public Class Settings
    Dim reg As Microsoft.VisualBasic.MyServices.RegistryProxy
    Dim key As Microsoft.Win32.RegistryKey

    ' Test-Einstellungen
    Dim m_testSetPhrases As Boolean
    Dim m_testFormerLanguage As Boolean
    Dim m_useCards As Boolean
    Dim m_CardsInitialInterval As Integer

    ' Fenster-Position
    Dim m_saveWindowPosition As Boolean
    Dim m_mainWindowState As FormWindowState
    Dim m_childWindowState As FormWindowState
    Dim m_mainWindow As WindowSettings
    Dim m_explorerWindow As WindowSettings
    Dim m_groupWindow As WindowSettings
    Dim m_statisticWindow As WindowSettings

    ' Gruppen
    Dim m_lastGroup As String = ""
    Dim m_lastSubGroup As String = ""

    ' Grundeinrichtungen und Konstruktor
    Public Sub New()
        ' Erstellen. Es wird nichts überschrieben
        Create()
    End Sub

    Public Sub Create()
        reg = My.Computer.Registry
        ' Versuche den Key zu laden, wenn das nicht geht setzte "insalled" auf false
        key = reg.CurrentUser.OpenSubKey("Software\Gravo")
        If key Is Nothing Then
            key = reg.CurrentUser.OpenSubKey("Software", True)
            key.CreateSubKey("Gravo")
        End If
        key = reg.CurrentUser.OpenSubKey("Software\Gravo", True)

        ' Erzeuge die benötigten Werte mit Standardeinstellungen
        SecureStoreBool("TestSetPhrases", False)
        SecureStoreBool("TestFormerLanguage", True)
        SecureStoreBool("SaveWindowPosition", False)
        SecureStoreString("LastGroup", "")
        SecureStoreString("LastSubGroup", "")
        SecureStoreWindowSettings("WindowSettingsMain", 600, 800, New Point(0, 0), FormWindowState.Normal)
        SecureStoreWindowSettings("WindowSettingsExplorer", 300, 400, New Point(0, 0), FormWindowState.Normal)
        SecureStoreWindowSettings("WindowSettingsGroups", 300, 400, New Point(22, 29), FormWindowState.Normal)
        SecureStoreWindowSettings("WindowSettingsStatistic", 300, 400, New Point(44, 58), FormWindowState.Normal)
        SecureStoreWindowState("MainWindowState", FormWindowState.Normal)
        SecureStoreWindowState("ChildWindowState", FormWindowState.Normal)
        SecureStoreBool("UseCards", True)
        SecureStoreInt("CardsInitialInterval", 1)
        LoadSettings()
    End Sub

    Public Sub LoadSettings()
        TestFormerLanguage = LoadBool("TestFormerLanguage")
        TestSetPhrases = LoadBool("TestSetPhrases")
        SaveWindowPosition = LoadBool("SaveWindowPosition")
        LastGroup = LoadString("LastGroup")
        LastSubGroup = LoadString("LastSubGroup")
        ExplorerWindowSettings = LoadWindowSettings("WindowSettingsExplorer")
        GroupWindowSettings = LoadWindowSettings("WindowSettingsGroups")
        MainWindowSettings = LoadWindowSettings("WindowSettingsMain")
        StatisticWindowSettings = LoadWindowSettings("WindowSettingsStatistic")
        MainWindowState = LoadWindowState("MainWindowState")
        ChildWindowState = LoadWindowState("ChildWindowState")
        UseCards = LoadBool("UseCards")
        CardsInitialInterval = LoadInt("CardsInitialInterval")
    End Sub

    Public Sub SaveSettings()
        StoreBool("TestFormerLanguage", TestFormerLanguage)
        StoreBool("TestSetPhrases", TestSetPhrases)
        StoreBool("SaveWindowPosition", SaveWindowPosition)
        StoreString("LastGroup", LastGroup)
        StoreString("LastSubGroup", LastSubGroup)
        StoreWindowSettings(ExplorerWindowSettings)
        StoreWindowSettings(GroupWindowSettings)
        StoreWindowSettings(MainWindowSettings)
        StoreWindowSettings(StatisticWindowSettings)
        StoreWindowState("MainWindowState", MainWindowState)
        StoreWindowState("ChildWindowState", ChildWindowState)
        StoreBool("UseCards", UseCards)
        StoreInt("CardsInitialInterval", CardsInitialInterval)
    End Sub

    ' Funktionen und Methoden zum Speichern und Laden
    Private Sub StoreBool(ByVal Name As String, ByVal Value As Boolean)
        If Value Then StoreInt(Name, 1) Else StoreInt(Name, 0)
    End Sub

    Private Function LoadBool(ByVal name As String) As Boolean
        Dim val As Integer = LoadInt(name)
        If val = 0 Then Return False Else Return True
    End Function

    Private Sub SecureStoreBool(ByVal name As String, ByVal value As Boolean)
        Try
            LoadBool(name)
        Catch ex As RegistryNotReadyException
            StoreBool(name, value)
        End Try
    End Sub

    Private Sub StoreInt(ByVal name As String, ByVal value As Integer)
        key.SetValue(name, value)
    End Sub

    Private Function LoadInt(ByVal name As String) As Integer
        Dim val As String = key.GetValue(name, "null")
        If val <> "null" Then
            Return val
        Else
            Throw New RegistryNotReadyException("Wert " & name & " nicht in Registry vorhanden")
        End If
    End Function

    Private Sub SecureStoreInt(ByVal name As String, ByVal value As Integer)
        Try
            LoadInt(name)
        Catch ex As RegistryNotReadyException
            StoreInt(name, value)
        End Try
    End Sub

    Private Sub StoreString(ByVal name As String, ByVal value As String)
        key.SetValue(name, value)
    End Sub

    Private Function LoadString(ByVal name As String) As String
        Dim val As String = key.GetValue(name, "null")
        If val <> "null" Then
            Return val
        Else
            StoreString(name, "")
            Throw New RegistryNotReadyException("Wert " & name & " nicht in Registry vorhanden")
        End If
    End Function

    Private Sub SecureStoreString(ByVal name As String, ByVal value As String)
        Try
            LoadString(name)
        Catch ex As RegistryNotReadyException
            StoreString(name, value)
        End Try
    End Sub

    Private Sub StoreWindowSettings(ByVal value As WindowSettings)
        key.SetValue(value.name & "Height", value.height)
        key.SetValue(value.name & "Width", value.width)
        key.SetValue(value.name & "PosX", value.position.X)
        key.SetValue(value.name & "PosY", value.position.Y)
    End Sub

    Private Function LoadWindowSettings(ByVal name As String) As WindowSettings
        Dim val As WindowSettings
        val.name = name
        val.height = LoadInt(name & "Height")
        val.width = LoadInt(name & "Width")
        val.position.X = LoadInt(name & "PosX")
        val.position.Y = LoadInt(name & "PosY")
        Return val
    End Function

    Private Sub SecureStoreWindowSettings(ByVal value As WindowSettings)
        Try
            LoadWindowSettings(value.name)
        Catch ex As RegistryNotReadyException
            key.SetValue(value.name & "Height", value.height)
            key.SetValue(value.name & "Width", value.width)
            key.SetValue(value.name & "PosX", value.position.X)
            key.SetValue(value.name & "PosY", value.position.Y)
        End Try
    End Sub

    Private Sub SecureStoreWindowSettings(ByVal name As String, ByVal height As Integer, ByVal width As Integer, ByVal position As Point, ByVal state As FormWindowState)
        Dim temp As WindowSettings
        temp.name = name
        temp.height = height
        temp.position = position
        temp.width = width
        SecureStoreWindowSettings(temp)
    End Sub

    Private Sub StoreWindowState(ByVal name As String, ByVal value As FormWindowState)
        Select Case value
            Case FormWindowState.Maximized
                key.SetValue(name, 0)
            Case FormWindowState.Minimized
                key.SetValue(name, 1)
            Case FormWindowState.Normal
                key.SetValue(name, 2)
        End Select
    End Sub

    Private Function LoadWindowState(ByVal name As String) As FormWindowState
        Dim ret As FormWindowState
        Select Case LoadInt(name)
            Case 0
                ret = FormWindowState.Maximized
            Case 1
                ret = FormWindowState.Minimized
            Case 2
                ret = FormWindowState.Normal
        End Select
        Return ret
    End Function

    Private Sub SecureStoreWindowState(ByVal name As String, ByVal value As FormWindowState)
        Try
            LoadWindowState(name)
        Catch ex As RegistryNotReadyException
            Select Case value
                Case FormWindowState.Maximized
                    key.SetValue(name, 0)
                Case FormWindowState.Minimized
                    key.SetValue(name, 1)
                Case FormWindowState.Normal
                    key.SetValue(name, 2)
            End Select
        End Try
    End Sub

    ' Eigenschaften zum Abrufen der Einstellungen
    Public Property TestFormerLanguage() As Boolean
        Get
            Return m_testFormerLanguage
        End Get
        Set(ByVal value As Boolean)
            m_testFormerLanguage = value
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

    Public Property MainWindowState() As FormWindowState
        Get
            Return m_mainWindowState
        End Get
        Set(ByVal value As FormWindowState)
            m_mainWindowState = value
        End Set
    End Property

    Public Property ChildWindowState() As FormWindowState
        Get
            Return m_childWindowState
        End Get
        Set(ByVal value As FormWindowState)
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