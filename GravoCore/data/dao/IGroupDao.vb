Public Interface IGroupDao
    Sub Add(ByVal group As GroupEntry, ByVal word As WordEntry, ByVal marked As Boolean, ByVal example As String)

    Function Load(ByVal group As GroupEntry) As GroupDto

    Function Load(ByVal group As GroupEntry, ByVal word As WordEntry) As TestWord

    Sub Delete(ByVal group As GroupEntry, ByVal entry As TestWord)

    ' Not clear, if we need this.
    Sub UpdateMarked(ByVal group As GroupEntry, ByVal word As TestWord, ByVal marked As Boolean)

    <Obsolete("This method is deprecated, work on data objects and update.")>
    Function GetTestWord(ByVal group As GroupEntry, ByVal word As String, ByVal meaning As String) As TestWord

    ''' <summary>
    ''' Returns the single language that is to be learned in a given group. Throws exception, if contains
    ''' multiple languages.
    ''' </summary>
    ''' <returns>The single language of a group</returns>
    Function GetUniqueLanguage(ByVal group As GroupEntry) As String

    Function GetLanguages(ByVal group As GroupEntry) As ICollection(Of String)

    Function GetUniqueMainLanguage(ByVal group As GroupEntry) As String

    Function GetMainLanguages(ByVal group As GroupEntry) As ICollection(Of String)
End Interface
