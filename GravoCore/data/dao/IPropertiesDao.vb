Public Interface IPropertiesDao

    ''' <summary>
    ''' Loads properties for a given data connection. The properties contain the version
    ''' of the data model, limits, and more.
    ''' </summary>
    ''' <returns>The properties of the data connection.</returns>
    Function LoadProperties() As Properties

    Function LoadWordTypes() As WordTypes

End Interface
