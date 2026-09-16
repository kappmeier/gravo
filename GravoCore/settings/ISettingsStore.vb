''' <summary>
''' Persistence of the program settings as flat string key:value pairs.
''' </summary>
Public Interface ISettingsStore
    ''' <summary>
    ''' Returns the persisted values. Empty when nothing has been saved yet.
    ''' </summary>
    Function Load() As IDictionary(Of String, String)

    ''' <summary>
    ''' Replaces the persisted values with <paramref name="values"/>.
    ''' </summary>
    Sub Save(values As IDictionary(Of String, String))
End Interface
