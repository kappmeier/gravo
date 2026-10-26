Imports Gravo

''' <summary>
''' Column limits and the schema version of a vocabulary database.
''' </summary>
Public Class Properties

    Public Sub New(builder As PropertiesBuilder)
        DictionaryWordsMaxLengthWord = builder.DictionaryWordsMaxLengthWord
        DictionaryWordsMaxLengthPre = builder.DictionaryWordsMaxLengthPre
        DictionaryWordsMaxLengthPost = builder.DictionaryWordsMaxLengthPost
        DictionaryWordsMaxLengthMeaning = builder.DictionaryWordsMaxLengthMeaning
        DictionaryWordsMaxLengthAdditionalTargetLangInfo = builder.DictionaryWordsMaxLengthAdditionalTargetLangInfo
        DictionaryMainMaxLengthWordEntry = builder.DictionaryMainMaxLengthWordEntry
        DictionaryMainMaxLengthLanguage = builder.DictionaryMainMaxLengthLanguage
        DictionaryMainMaxLengthMainLanguage = builder.DictionaryMainMaxLengthMainLanguage
        DbSchemaVersionMaxLengthDescription = builder.DbSchemaVersionMaxLengthDescription
        GroupsMaxLengthName = builder.GroupsMaxLengthName
        GroupsMaxLengthTable = builder.GroupsMaxLengthTable
        GroupsMaxLengthSubName = builder.GroupsMaxLengthSubName
        GroupMaxLengthExample = builder.GroupMaxLengthExample
        Version = builder.Version
    End Sub

    Public ReadOnly Property DictionaryWordsMaxLengthWord As Byte
    Public ReadOnly Property DictionaryWordsMaxLengthPre As Byte
    Public ReadOnly Property DictionaryWordsMaxLengthPost As Byte
    Public ReadOnly Property DictionaryWordsMaxLengthMeaning As Byte
    Public ReadOnly Property DictionaryWordsMaxLengthAdditionalTargetLangInfo As Byte
    Public ReadOnly Property DictionaryMainMaxLengthWordEntry As Byte
    Public ReadOnly Property DictionaryMainMaxLengthLanguage As Byte
    Public ReadOnly Property DictionaryMainMaxLengthMainLanguage As Byte
    Public ReadOnly Property DbSchemaVersionMaxLengthDescription As Byte
    Public ReadOnly Property GroupsMaxLengthName As Byte
    Public ReadOnly Property GroupsMaxLengthSubName As Byte
    Public ReadOnly Property GroupsMaxLengthTable As Byte
    Public ReadOnly Property GroupMaxLengthExample As Byte
    Public ReadOnly Property Version As DbSchemaVersion

    Public Class DbSchemaVersion
        Implements IComparable(Of DbSchemaVersion)

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

        Public Function CompareTo(other As DbSchemaVersion) As _
            Integer Implements IComparable(Of DbSchemaVersion).CompareTo
            If (_major > other._major) Then
                Return 1
            ElseIf (_major < other._major) Then
                Return -1
            Else
                Return Math.Sign(CInt(_minor) - CInt(other._minor))
            End If
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            Dim version = TryCast(obj, DbSchemaVersion)
            Return version IsNot Nothing AndAlso
                   _major = version._major AndAlso
                   _minor = version._minor AndAlso
                   _date = version._date AndAlso
                   _description = version._description
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return (_major, _minor, _date, _description).GetHashCode()
        End Function
    End Class

    ''' <summary>
    ''' Creates a <see cref="Properties"/> instance with values.
    ''' </summary>
    Public Class PropertiesBuilder
        Friend Property DictionaryWordsMaxLengthWord As Byte
        Friend Property DictionaryWordsMaxLengthPre As Byte
        Friend Property DictionaryWordsMaxLengthPost As Byte
        Friend Property DictionaryWordsMaxLengthMeaning As Byte
        Friend Property DictionaryWordsMaxLengthAdditionalTargetLangInfo As Byte
        Friend Property DictionaryMainMaxLengthWordEntry As Byte
        Friend Property DictionaryMainMaxLengthLanguage As Byte
        Friend Property DictionaryMainMaxLengthMainLanguage As Byte
        Friend Property DbSchemaVersionMaxLengthDescription As Byte
        Friend Property GroupsMaxLengthName As Byte
        Friend Property GroupsMaxLengthTable As Byte
        Friend Property GroupsMaxLengthSubName As Byte
        Friend Property GroupMaxLengthExample As Byte
        Friend Property Version As DbSchemaVersion

        Public Function WithDictionaryWordsMaxLengthWord(value As Byte) As PropertiesBuilder
            DictionaryWordsMaxLengthWord = value
            Return Me
        End Function

        Public Function WithDictionaryWordsMaxLengthPre(value As Byte) As PropertiesBuilder
            DictionaryWordsMaxLengthPre = value
            Return Me
        End Function

        Public Function WithDictionaryWordsMaxLengthPost(value As Byte) As PropertiesBuilder
            DictionaryWordsMaxLengthPost = value
            Return Me
        End Function

        Public Function WithDictionaryWordsMaxLengthMeaning(value As Byte) As PropertiesBuilder
            DictionaryWordsMaxLengthMeaning = value
            Return Me
        End Function

        Public Function WithDictionaryWordsMaxLengthAdditionalTargetLangInfo(value As Byte) As PropertiesBuilder
            DictionaryWordsMaxLengthAdditionalTargetLangInfo = value
            Return Me
        End Function

        Public Function WithDictionaryMainMaxLengthWordEntry(value As Byte) As PropertiesBuilder
            DictionaryMainMaxLengthWordEntry = value
            Return Me
        End Function

        Public Function WithDictionaryMainMaxLengthLanguage(value As Byte) As PropertiesBuilder
            DictionaryMainMaxLengthLanguage = value
            Return Me
        End Function

        Public Function WithDictionaryMainMaxLengthMainLanguage(value As Byte) As PropertiesBuilder
            DictionaryMainMaxLengthMainLanguage = value
            Return Me
        End Function

        Public Function WithDbSchemaVersionMaxLengthDescription(value As Byte) As PropertiesBuilder
            DbSchemaVersionMaxLengthDescription = value
            Return Me
        End Function

        Public Function WithGroupsMaxLengthName(value As Byte) As PropertiesBuilder
            GroupsMaxLengthName = value
            Return Me
        End Function

        Public Function WithGroupsMaxLengthTable(value As Byte) As PropertiesBuilder
            GroupsMaxLengthTable = value
            Return Me
        End Function

        Public Function WithGroupsMaxLengthSubName(value As Byte) As PropertiesBuilder
            GroupsMaxLengthSubName = value
            Return Me
        End Function

        Public Function WithGroupMaxLengthExample(value As Byte) As PropertiesBuilder
            GroupMaxLengthExample = value
            Return Me
        End Function

        Public Function WithVersion(value As DbSchemaVersion) As PropertiesBuilder
            Version = value
            Return Me
        End Function
    End Class
End Class
