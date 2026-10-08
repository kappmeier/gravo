Imports System.Collections.ObjectModel

''' <summary>
''' Access to the localized UI texts, implemented by <see cref="localization"/>. Data is loaded from
''' <c>languages.s3db</c>.
''' </summary>
Public Interface ILocalization

    ''' <summary>
    ''' The currently localized language, e.g. <c>german</c>. Can by changed using <see cref="SwitchToLanguage"/>.
    ''' See <see cref="GetLanguageNames"/> for the list of available languages.
    ''' </summary>
    ''' <remarks>
    ''' The language equals the table name the texts are read from in the database.
    ''' </remarks>
    Property Language As String

    ''' <summary>Retrieves the localized text specified by the given id.</summary>
    Function GetText(id As Integer) As String

    ''' <summary>
    ''' Retrieves the localized text specified by the given name.
    ''' </summary>
    ''' <remarks>
    ''' The name corresponds to the constant defined in the code for the localized text. This is a convenience method
    ''' for retrieving texts without needing to know their integer ids. It is not implemented for all of the localized
    ''' texts.
    ''' </remarks>
    ''' <param name="name">The text name.</param>
    ''' <returns>The localized text corresponding to the given name.</returns>
    Function GetText(name As String) As String

    ''' <summary>
    ''' Returns a list of unique names for available language sets.
    ''' </summary>
    ''' <returns>The list of unique names of languages.</returns>
    Function GetLanguageNames() As Collection(Of String)

    Sub SwitchToLanguage(name As String)
End Interface
