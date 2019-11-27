Public Interface IGroupDao
    Sub Add(ByRef group As GroupEntry, ByRef word As WordEntry, ByRef marked As Boolean, ByRef example As String)

    Function Load(ByRef group As GroupEntry) As GroupDto

    Function Load(ByRef group As GroupEntry, ByRef word As WordEntry) As TestWord

    Sub Delete(ByRef group As GroupEntry, ByRef entry As TestWord)

    ' Not clear, if we need this.
    Sub UpdateMarked(ByRef group As GroupEntry, ByRef word As TestWord, ByVal marked As Boolean)

    '<Obsolete("This method is deprecated, work on data objects and update.")>
    'Function GetIndex(ByRef group As GroupEntry, ByVal word As String, ByVal meaning As String) As Integer

    <Obsolete("This method is deprecated, work on data objects and update.")>
    Function GetTestWord(ByRef group As GroupEntry, ByVal word As String, ByVal meaning As String) As TestWord

    ''' <summary>
    ''' Returns the single language that is to be learned in a given group. Throws exception, if contains
    ''' multiple languages.
    ''' </summary>
    ''' <returns>The single language of a group</returns>
    Function GetUniqueLanguage(ByRef group As GroupEntry) As String

    Function GetLanguages(ByRef group As GroupEntry) As ICollection(Of String)

    Function GetUniqueMainLanguage(ByRef group As GroupEntry) As String

    Function GetMainLanguages(ByRef group As GroupEntry) As ICollection(Of String)
End Interface
