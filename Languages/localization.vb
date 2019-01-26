Imports System.Collections.ObjectModel

Public Class localization
    Inherits localizationBase

    Public Const DISCLAIMER_1 = 1
    Public Const COPYRIGHT_OLD = 2
    Public Const WORD_TYPE_SUBSTANTIVE = 3
    Public Const WORD_TYPE_VERB = 4
    Public Const WORD_TYPE_ADJECTIVE = 5
    Public Const WORD_TYPE_SIMPLE = 6
    Public Const WORD_TYPE_ADVERB = 7
    Public Const WORD_TYPE_SET_PHRASE = 8
    Public Const WORD_TYPE_EXAMPLE = 9
    Public Const DISCLAIMER_2 = 10
    Public Const HINT = 11
    Public Const DB_VERSION_OUTDATED = 12
    Public Const YES = 13
    Public Const NO = 14
    Public Const TREE_DICTIONARY = 15
    Public Const TREE_GROUPS = 16
    Public Const EXPLORER_HEADLINE_TOTAL_ENTRYS = 17
    Public Const EXPLORER_HEADLINE_MAIN_ENTRYS = 18
    Public Const EXPLORER_HEADLINE_LANGUAGE = 19
    Public Const EXPLORER_HEADLINE_MAIN_LANGUAGE = 20
    Public Const EXPLORER_HEADLINE_ADDITIONAL_INFO = 21
    Public Const EXPLORER_HEADLINE_IRREGULAR = 22
    Public Const EXPLORER_HEADLINE_MEANING = 23
    Public Const EXPLORER_HEADLINE_POST = 24
    Public Const EXPLORER_HEADLINE_PRE = 25
    Public Const EXPLORER_HEADLINE_WORD_TYPE = 26
    Public Const EXPLORER_HEADLINE_WORD = 27
    Public Const EXPLORER_HEADLINE_MARKED = 28
    Public Const EXPLORER_HEADLINE_SUBGROUP = 29
    Public Const EXPLORER_HEADLINE_ENTRYS = 30
    Public Const EXPLORER_HEADLINE_GROUPS = 31
    Public Const EXPLORER_HEADLINE_SUBGROUPS = 32
    Public Const EXCEPTION_UNKNOWN_HEADLINE = 33
    Public Const MAIN_MENU_FILE = 34
    Public Const MAIN_MENU_EDIT = 35
    Public Const MAIN_MENU_VIEW = 36
    Public Const MAIN_MENU_VOCABULARY = 37
    Public Const MAIN_MENU_EXTRAS = 38
    Public Const MAIN_MENU_WINDOWS = 39
    Public Const MAIN_MENU_HELP = 40
    Public Const MAIN_MENU_FILE_CHANGE_USER = 41
    Public Const MAIN_MENU_FILE_NEW = 42
    Public Const MAIN_MENU_FILE_OPEN = 43
    Public Const MAIN_MENU_FILE_SAVE = 44
    Public Const MAIN_MENU_FILE_SAVE_AS = 45
    Public Const MAIN_MENU_FILE_PRINT = 46
    Public Const MAIN_MENU_FILE_PRINT_PREVIEW = 47
    Public Const MAIN_MENU_FILE_PRINT_SETUP = 48
    Public Const MAIN_MENU_FILE_EXIT = 49
    Public Const MAIN_MENU_EDIT_UNDO = 50
    Public Const MAIN_MENU_EDIT_REDO = 51
    Public Const MAIN_MENU_EDIT_CUT = 52
    Public Const MAIN_MENU_EDIT_COPY = 53
    Public Const MAIN_MENU_EDIT_PASTE = 54
    Public Const MAIN_MENU_EDIT_SELECT_ALL = 55
    Public Const MAIN_MENU_VIEW_TOOL_BAR = 56
    Public Const MAIN_MENU_VIEW_STATUS_BAR = 57
    Public Const MAIN_MENU_VOCABULARY_EXPLORER = 58
    Public Const MAIN_MENU_VOCABULARY_ENLARGE_DICTIONARY = 59
    Public Const MAIN_MENU_VOCABULARY_INSERT_GROUPS = 60
    Public Const MAIN_MENU_VOCABULARY_TEST = 61
    Public Const MAIN_MENU_VOCABULARY_TEST_GENERAL = 62
    Public Const MAIN_MENU_VOCABULARY_TEST_GROUPS = 63
    Public Const MAIN_MENU_VOCABULARY_TEST_LANGUAGE = 64
    Public Const MAIN_MENU_VOCABULARY_STATISTIC = 65
    Public Const MAIN_MENU_EXTRAS_DATA_MANAGEMENT = 66
    Public Const MAIN_MENU_EXTRAS_CHECK_DATABASE = 67
    Public Const MAIN_MENU_EXTRAS_LDF_EDITOR = 68
    Public Const MAIN_MENU_EXTRAS_OPTIONS = 69
    Public Const MAIN_MENU_EXTRAS_LANGUAGE = 70
    Public Const MAIN_MENU_WINDOWS_NEW = 71
    Public Const MAIN_MENU_WINDOWS_CASCADE = 72
    Public Const MAIN_MENU_WINDOWS_TILE_VERTICAL = 73
    Public Const MAIN_MENU_WINDOWS_TILE_HORIZONTAL = 74
    Public Const MAIN_MENU_WINDOWS_CLOSE_ALL = 75
    Public Const MAIN_MENU_WINDOWS_ARRANGE_ICONS = 76
    Public Const MAIN_MENU_HELP_CONTENT = 77
    Public Const MAIN_MENU_HELP_INDEX = 78
    Public Const MAIN_MENU_HELP_SEARCH = 79
    Public Const MAIN_MENU_HELP_ABOUT = 80
    Public Const EXPLORER_TITLE = 81
    Public Const BUTTON_ADD = 82
    Public Const BUTTON_CHANGE = 83
    Public Const BUTTON_CLOSE = 84
    Public Const BUTTON_OK = 85
    Public Const WORDS_DIRECT_ADD = 86
    Public Const EXPLORER_MENU_PANELS = 87
    Public Const EXPLORER_MENU_PANELS_DEFAULT = 88
    Public Const EXPLORER_MENU_PANELS_SEARCH = 89
    Public Const EXPLORER_MENU_PANELS_WORD_INPUT = 90
    Public Const EXPLORER_MENU_PANELS_MULTI_EDIT = 91
    Public Const WORDS_PRE = EXPLORER_HEADLINE_PRE
    Public Const WORDS_POST = EXPLORER_HEADLINE_POST
    Public Const WORDS_WORD = EXPLORER_HEADLINE_WORD
    Public Const WORDS_MEANING = EXPLORER_HEADLINE_MEANING
    Public Const WORDS_IRREGULAR = EXPLORER_HEADLINE_IRREGULAR
    Public Const WORDS_MARKED = EXPLORER_HEADLINE_MARKED
    Public Const WORDS_WORD_TYPE = EXPLORER_HEADLINE_WORD_TYPE
    Public Const WORDS_MAIN_ENTRY = 92
    Public Const WORDS_ADDITIONAL_INFO = EXPLORER_HEADLINE_ADDITIONAL_INFO
    Public Const WORDS_EXAMPLE = 93
    Public Const WORDS_LANGUAGE = EXPLORER_HEADLINE_LANGUAGE
    Public Const WORDS_MAIN_LANGUAGE = EXPLORER_HEADLINE_MAIN_LANGUAGE
    Public Const BUTTON_CANCEL = 94
    Public Const TEST_INFO = 95
    Public Const TEST_TEST = 96
    Public Const TEST_MEANING = WORDS_MEANING
    Public Const TEST_CORRECT = 97
    Public Const TEST_ANOTHER_MEANING = 98
    Public Const TEST_WRONG_HINT = 99
    Public Const TEST_TYPE_ERROR = 100
    Public Const TEST_FINISHED = 101
    Public Const TEST_WELL_DONE = 102
    Public Const TEST_WRONG = 103
    Public Const TEST_ERROR = 104
    Public Const TEST_TITLE = 105
    Public Const GROUP = 106
    Public Const SUBGROUP = 107
    Public Const TEST_SELECT_TITLE = 108
    Public Const TEST_SELECT_GROUP = GROUP
    Public Const TEST_SELECT_SUBGROUP = SUBGROUP
    Public Const TEST_SELECT_ENTRYS = 109
    Public Const TEST_SELECT_ENTRY = 110
    Public Const TEST_SELECT_ONLY_MARKED = 111
    Public Const TEST_SELECT_TEST_DIRECTION = 112
    Public Const TEST_SELECT_PHRASES = 113
    Public Const TEST_SELECT_RANDOM_ORDER = 114
    Public Const EXPLORER_HEADLINE_GROUP_LANGUAGE_COUNT = 115
    ' Noch nicht in der Datenbank:
    Public Const UP = 116
    Public Const DOWN = 117

    Dim m_language As String = "german"

    Sub New()
        MyBase.New()
    End Sub

    Sub New(ByVal db as DatabaseOperation)
        MyBase.New(db)
    End Sub

    Public Property Language() As String
        Get
            Return m_language
        End Get
        Set(ByVal language As String)
            m_language = language
        End Set
    End Property

    Public Function GetText(ByVal name As String) As String
        ' existiert, weil es einfacher ist in manchen fällen
        Return GetText(NameToCode(name))
    End Function

    Public Function GetText(ByVal value As Integer) As String
        Dim command As String = "SELECT [Text] FROM [" & Language & "] WHERE [Field] = " & value & ";"
        DBConnection.ExecuteReader(command)
        DBConnection.DBCursor.Read()
        Dim ret As String = DBConnection.SecureGetString(0)
        DBConnection.DBCursor.Close()
        Return ret
    End Function

    Private Function NameToCode(ByVal name As String) As Integer
        Select Case name
            Case "DISCLAIMER_1"
                Return 1
            Case "COPYRIGHT_OLD"
                Return 2
            Case "WORD_TYPE_SUBSTANTIVE"
                Return 3
            Case "WORD_TYPE_VERB"
                Return 4
            Case "WORD_TYPE_ADJECTIVE"
                Return 5
            Case "WORD_TYPE_SIMPLE"
                Return 6
            Case "WORD_TYPE_ADVERB"
                Return 7
            Case "WORD_TYPE_SET_PHRASE"
                Return 8
            Case "WORD_TYPE_EXAMPLE"
                Return 9
            Case "DISCLAIMER_2"
                Return 10
            Case Else
                Return 0
        End Select
    End Function

    ''' <summary>
    ''' Returns a list of unique names for available language sets.
    ''' </summary>
    ''' <returns>The list of unique names of languages.</returns>
    Public Function GetLanguageNames() As Collection(Of String)
        Dim languages As New Collection(Of String)
        Dim command As String = "SELECT [Name] FROM [languages] ORDER BY [Name];"

        DBConnection.ExecuteReader(command)
        While DBConnection.DBCursor.Read
            languages.Add(DBConnection.SecureGetString(0))
        End While
        DBConnection.DBCursor.Close()

        Return languages
    End Function

    ''' <summary>
    ''' Retrieves the current version of a language set from the database.
    ''' </summary>
    ''' <param name="LanguageName">The language identifier</param>
    ''' <returns>The version as stored in the language database.</returns>
    Public Function GetVersionFor(LanguageName As String) As String
        Return GetFieldFor(LanguageName, "Version")
    End Function

    ''' <summary>
    ''' Retrieves the date of a language set from the database.
    ''' </summary>
    ''' <param name="LanguageName">The language identifier</param>
    ''' <returns>The author as stored in the language database.</returns>
    Public Function GetDateFor(LanguageName As String) As String
        Dim command As String = "SELECT [Date] FROM [languages] WHERE [Name] = " & AccessDatabaseOperation.GetDBEntry(LanguageName) & ";"
        DBConnection.ExecuteReader(command)
        If Not DBConnection.DBCursor.HasRows Then Throw New Exception("Wrong language")
        DBConnection.DBCursor.Read()
        Dim value = DBConnection.SecureGetDateTime(0)
        DBConnection.DBCursor.Close()
        Return value.ToString
    End Function

    ''' <summary>
    ''' Retrieves the author of a language set from the database.
    ''' </summary>
    ''' <param name="LanguageName">The language identifier</param>
    ''' <returns>The author as stored in the language database.</returns>
    Public Function GetAuthorFor(LanguageName As String) As String
        Return GetFieldFor(LanguageName, "Author")
    End Function

    ''' <summary>
    ''' Returns the language of the given language identifier.
    ''' </summary>
    ''' <param name="LanguageName">The language identifier</param>
    ''' <returns></returns>
    Public Function GetLanguageFor(LanguageName As String) As String
        Return GetFieldFor(LanguageName, "Language")
    End Function

    ''' <summary>
    ''' Returns the name of the database table containing the data for a given language.
    ''' </summary>
    ''' <param name="LanguageName">The language identifier</param>
    ''' <returns>The table name.</returns>
    Public Function GetTableFor(LanguageName As String) As String
        Return GetFieldFor(LanguageName, "Table")
    End Function

    Private Function GetFieldFor(LanguageName As String, Field As String) As String
        Dim command As String = "SELECT [" & Field & "] FROM [languages] WHERE [Name] = " & AccessDatabaseOperation.GetDBEntry(LanguageName) & ";"
        DBConnection.ExecuteReader(command)
        If Not DBConnection.DBCursor.HasRows Then Throw New Exception("Wrong language")
        DBConnection.DBCursor.Read()
        Dim value = DBConnection.SecureGetString(0)
        DBConnection.DBCursor.Close()
        Return value
    End Function

    Public Sub SwitchToLanguage(ByVal name As String)
        Language = GetTableFor(name)
    End Sub
End Class