Imports Gravo

Public Class Properties

    Public Sub New(builder As PropertiesBuilder)
        Dim pbi As IPropertiesBuilderAccess = builder
        _dictionaryWordsMaxLengthWord = pbi.DictionaryWordsMaxLengthWord
        _dictionaryWordsMaxLengthPre = pbi.DictionaryWordsMaxLengthPre
        _dictionaryWordsMaxLengthPost = pbi.DictionaryWordsMaxLengthPost
        _dictionaryWordsMaxLengthMeaning = pbi.DictionaryWordsMaxLengthMeaning
        _dictionaryWordsMaxLengthAdditionalTargetLangInfo = pbi.DictionaryWordsMaxLengthAdditionalTargetLangInfo
        _dictionaryMainMaxLengthWordEntry = pbi.DictionaryMainMaxLengthWordEntry
        _dictionaryMainMaxLengthLanguage = pbi.DictionaryMainMaxLengthLanguage
        _dictionaryMainMaxLengthMainLanguage = pbi.DictionaryMainMaxLengthMainLanguage
        _dBVersionMaxLengthDescription = pbi.DBVersionMaxLengthDescription
        _groupsMaxLengthName = pbi.GroupsMaxLengthName
        _groupsMaxLengthTable = pbi.GroupsMaxLengthTable
        _groupsMaxLengthSubName = pbi.GroupsMaxLengthSubName
        _groupMaxLengthExample = pbi.GroupMaxLengthExample
        _verion = pbi.Version
    End Sub

    Private ReadOnly _dictionaryWordsMaxLengthWord As Byte
    Private ReadOnly _dictionaryWordsMaxLengthPre As Byte
    Private ReadOnly _dictionaryWordsMaxLengthPost As Byte
    Private ReadOnly _dictionaryWordsMaxLengthMeaning As Byte
    Private ReadOnly _dictionaryWordsMaxLengthAdditionalTargetLangInfo As Byte
    Private ReadOnly _dictionaryMainMaxLengthWordEntry As Byte
    Private ReadOnly _dictionaryMainMaxLengthLanguage As Byte
    Private ReadOnly _dictionaryMainMaxLengthMainLanguage As Byte
    Private ReadOnly _dBVersionMaxLengthDescription As Byte
    Private ReadOnly _groupsMaxLengthName As Byte
    Private ReadOnly _groupsMaxLengthTable As Byte
    Private ReadOnly _groupsMaxLengthSubName As Byte
    Private ReadOnly _groupMaxLengthExample As Byte
    Private ReadOnly _verion As DBVersion

    Public ReadOnly Property DictionaryWordsMaxLengthWord() As Byte
        Get
            Return _dictionaryWordsMaxLengthWord
        End Get
    End Property

    Public ReadOnly Property DictionaryWordsMaxLengthPre() As Byte
        Get
            Return _dictionaryWordsMaxLengthPre
        End Get
    End Property

    Public ReadOnly Property DictionaryWordsMaxLengthPost() As Byte
        Get
            Return _dictionaryWordsMaxLengthPost
        End Get
    End Property

    Public ReadOnly Property DictionaryWordsMaxLengthMeaning() As Byte
        Get
            Return _dictionaryWordsMaxLengthMeaning
        End Get
    End Property

    Public ReadOnly Property DictionaryWordsMaxLengthAdditionalTargetLangInfo() As Byte
        Get
            Return _dictionaryWordsMaxLengthAdditionalTargetLangInfo
        End Get
    End Property

    Public ReadOnly Property DictionaryMainMaxLengthWordEntry() As Byte
        Get
            Return _dictionaryMainMaxLengthWordEntry
        End Get
    End Property

    Public ReadOnly Property DictionaryMainMaxLengthLanguage() As Byte
        Get
            Return _dictionaryMainMaxLengthLanguage
        End Get
    End Property

    Public ReadOnly Property DictionaryMainMaxLengthMainLanguage() As Byte
        Get
            Return _dictionaryMainMaxLengthMainLanguage
        End Get
    End Property

    Public ReadOnly Property DBVersionMaxLengthDescription() As Byte
        Get
            Return _dBVersionMaxLengthDescription
        End Get
    End Property

    Public ReadOnly Property GroupsMaxLengthName() As Byte
        Get
            Return _groupsMaxLengthName
        End Get
    End Property

    Public ReadOnly Property GroupsMaxLengthSubName() As Byte
        Get
            Return _groupsMaxLengthSubName
        End Get
    End Property

    Public ReadOnly Property GroupsMaxLengthTable() As Byte
        Get
            Return _groupsMaxLengthTable
        End Get
    End Property

    Public ReadOnly Property GroupMaxLengthExample() As Byte
        Get
            Return _groupMaxLengthExample
        End Get
    End Property

    Public ReadOnly Property Verion As DBVersion
        Get
            Return _verion
        End Get
    End Property

    Public Class DBVersion
        Implements IComparable(Of DBVersion)

        Private ReadOnly _major As UInt16
        Private ReadOnly _minor As UInt16
        Private ReadOnly _date As Date
        Private ReadOnly _description As String

        Public Sub New(major As UShort, minor As UShort, introduced As Date, description As String)
            _major = major
            _minor = minor
            _date = introduced
            _description = description
        End Sub

        Public ReadOnly Property Major As UInt16
            Get
                Return _major
            End Get
        End Property

        Public ReadOnly Property Minor As UInt16
            Get
                Return _minor
            End Get
        End Property

        Public ReadOnly Property Introduction As Date
            Get
                Return _date
            End Get
        End Property

        Public ReadOnly Property Description As String
            Get
                Return _description
            End Get
        End Property

        Public Function CompareTo(other As DBVersion) As Integer Implements IComparable(Of DBVersion).CompareTo
            If (_major > other._major) Then
                Return 1
            ElseIf (_major < other._major) Then
                Return -1
            Else
                Return Math.Sign(CInt(_minor) - CInt(other._minor))
            End If
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            Dim version = TryCast(obj, DBVersion)
            Return version IsNot Nothing AndAlso
                   _major = version._major AndAlso
                   _minor = version._minor AndAlso
                   _date = version._date AndAlso
                   _description = version._description
        End Function
    End Class

    Private Interface IPropertiesBuilderAccess

        ReadOnly Property DictionaryWordsMaxLengthWord As Byte

        ReadOnly Property DictionaryWordsMaxLengthPre As Byte

        ReadOnly Property DictionaryWordsMaxLengthPost As Byte

        ReadOnly Property DictionaryWordsMaxLengthMeaning As Byte

        ReadOnly Property DictionaryWordsMaxLengthAdditionalTargetLangInfo As Byte

        ReadOnly Property DictionaryMainMaxLengthWordEntry As Byte

        ReadOnly Property DictionaryMainMaxLengthLanguage As Byte

        ReadOnly Property DictionaryMainMaxLengthMainLanguage As Byte

        ReadOnly Property DBVersionMaxLengthDescription As Byte

        ReadOnly Property GroupsMaxLengthName As Byte

        ReadOnly Property GroupsMaxLengthTable As Byte

        ReadOnly Property GroupsMaxLengthSubName As Byte

        ReadOnly Property GroupMaxLengthExample As Byte

        ReadOnly Property Version As DBVersion
    End Interface

    Public Class PropertiesBuilder
        Implements IPropertiesBuilderAccess

        Private _dictionaryWordsMaxLengthWord As Byte
        Private _dictionaryWordsMaxLengthPre As Byte
        Private _dictionaryWordsMaxLengthPost As Byte
        Private _dictionaryWordsMaxLengthMeaning As Byte
        Private _dictionaryWordsMaxLengthAdditionalTargetLangInfo As Byte
        Private _dictionaryMainMaxLengthWordEntry As Byte
        Private _dictionaryMainMaxLengthLanguage As Byte
        Private _dictionaryMainMaxLengthMainLanguage As Byte
        Private _dBVersionMaxLengthDescription As Byte
        Private _groupsMaxLengthName As Byte
        Private _groupsMaxLengthTable As Byte
        Private _groupsMaxLengthSubName As Byte
        Private _groupMaxLengthExample As Byte
        Private _version As DBVersion

        Private ReadOnly Property DictionaryWordsMaxLengthWord As Byte Implements IPropertiesBuilderAccess.DictionaryWordsMaxLengthWord
            Get
                Return _dictionaryWordsMaxLengthWord
            End Get
        End Property

        Private ReadOnly Property DictionaryWordsMaxLengthPre As Byte Implements IPropertiesBuilderAccess.DictionaryWordsMaxLengthPre
            Get
                Return _dictionaryWordsMaxLengthPre
            End Get
        End Property

        Private ReadOnly Property DictionaryWordsMaxLengthPost As Byte Implements IPropertiesBuilderAccess.DictionaryWordsMaxLengthPost
            Get
                Return _dictionaryWordsMaxLengthPost
            End Get
        End Property

        Private ReadOnly Property DictionaryWordsMaxLengthMeaning As Byte Implements IPropertiesBuilderAccess.DictionaryWordsMaxLengthMeaning
            Get
                Return _dictionaryWordsMaxLengthMeaning
            End Get
        End Property

        Private ReadOnly Property DictionaryWordsMaxLengthAdditionalTargetLangInfo As Byte Implements IPropertiesBuilderAccess.DictionaryWordsMaxLengthAdditionalTargetLangInfo
            Get
                Return _dictionaryWordsMaxLengthAdditionalTargetLangInfo
            End Get
        End Property

        Private ReadOnly Property DictionaryMainMaxLengthWordEntry As Byte Implements IPropertiesBuilderAccess.DictionaryMainMaxLengthWordEntry
            Get
                Return _dictionaryMainMaxLengthWordEntry
            End Get
        End Property

        Private ReadOnly Property DictionaryMainMaxLengthLanguage As Byte Implements IPropertiesBuilderAccess.DictionaryMainMaxLengthLanguage
            Get
                Return _dictionaryMainMaxLengthLanguage
            End Get
        End Property

        Private ReadOnly Property DBVersionMaxLengthDescription As Byte Implements IPropertiesBuilderAccess.DBVersionMaxLengthDescription
            Get
                Return _dBVersionMaxLengthDescription
            End Get
        End Property

        Private ReadOnly Property DictionaryMainMaxLengthMainLanguage As Byte Implements IPropertiesBuilderAccess.DictionaryMainMaxLengthMainLanguage
            Get
                Return _dictionaryMainMaxLengthMainLanguage
            End Get
        End Property

        Private ReadOnly Property GroupsMaxLengthName As Byte Implements IPropertiesBuilderAccess.GroupsMaxLengthName
            Get
                Return _groupsMaxLengthName
            End Get
        End Property

        Private ReadOnly Property GroupsMaxLengthTable As Byte Implements IPropertiesBuilderAccess.GroupsMaxLengthTable
            Get
                Return _groupsMaxLengthTable
            End Get
        End Property

        Private ReadOnly Property GroupsMaxLengthSubName As Byte Implements IPropertiesBuilderAccess.GroupsMaxLengthSubName
            Get
                Return _groupsMaxLengthSubName
            End Get
        End Property

        Private ReadOnly Property GroupMaxLengthExample As Byte Implements IPropertiesBuilderAccess.GroupMaxLengthExample
            Get
                Return _groupMaxLengthExample
            End Get
        End Property

        Private ReadOnly Property Version As DBVersion Implements IPropertiesBuilderAccess.Version
            Get
                Return _version
            End Get
        End Property

        Public Function WithDictionaryWordsMaxLengthWord(value As Byte) As PropertiesBuilder
            _dictionaryWordsMaxLengthWord = value
            Return Me
        End Function

        Public Function WithDictionaryWordsMaxLengthPre(value As Byte) As PropertiesBuilder
            _dictionaryWordsMaxLengthPre = value
            Return Me
        End Function

        Public Function WithDictionaryWordsMaxLengthPost(value As Byte) As PropertiesBuilder
            _dictionaryWordsMaxLengthPost = value
            Return Me
        End Function

        Public Function WithDictionaryWordsMaxLengthMeaning(value As Byte) As PropertiesBuilder
            _dictionaryWordsMaxLengthMeaning = value
            Return Me
        End Function

        Public Function WithDictionaryWordsMaxLengthAdditionalTargetLangInfo(value As Byte) As PropertiesBuilder
            _dictionaryWordsMaxLengthAdditionalTargetLangInfo = value
            Return Me
        End Function

        Public Function WithDictionaryMainMaxLengthWordEntry(value As Byte) As PropertiesBuilder
            _dictionaryMainMaxLengthWordEntry = value
            Return Me
        End Function

        Public Function WithDictionaryMainMaxLengthLanguage(value As Byte) As PropertiesBuilder
            _dictionaryMainMaxLengthLanguage = value
            Return Me
        End Function

        Public Function WithDictionaryMainMaxLengthMainLanguage(value As Byte) As PropertiesBuilder
            _dictionaryMainMaxLengthMainLanguage = value
            Return Me
        End Function

        Public Function WithDBVersionMaxLengthDescription(value As Byte) As PropertiesBuilder
            _dBVersionMaxLengthDescription = value
            Return Me
        End Function

        Public Function WithGroupsMaxLengthName(value As Byte) As PropertiesBuilder
            _groupsMaxLengthName = value
            Return Me
        End Function

        Public Function WithGroupsMaxLengthTable(value As Byte) As PropertiesBuilder
            _groupsMaxLengthTable = value
            Return Me
        End Function

        Public Function WithGroupsMaxLengthSubName(value As Byte) As PropertiesBuilder
            _groupsMaxLengthSubName = value
            Return Me
        End Function

        Public Function WithGroupMaxLengthExample(value As Byte) As PropertiesBuilder
            _groupMaxLengthExample = value
            Return Me
        End Function

        Public Function WithVersion(value As DBVersion) As PropertiesBuilder
            _version = value
            Return Me
        End Function
    End Class
End Class
