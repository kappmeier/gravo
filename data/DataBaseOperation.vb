Imports System.Data.OleDb

''' <summary>
''' A wrapper that allows to open a database, perform SQL queries and retrieve values.
''' 
''' Implementations hide the connection details, such as connection string.
''' </summary>
Public Interface DataBaseOperation
    ''' <summary>
    ''' Conects to a database at a given location.
    ''' 
    ''' As long as the connection is open, it is possible to execute queries and get
    ''' results.
    ''' </summary>
    ''' <param name="DBPath">location of the database on hard drive</param>
    ''' <returns>Whether the opening was successful.</returns>
    Function Open(ByVal DBPath As String) As Boolean

    ''' <summary>
    ''' Closes the database connection.
    ''' </summary>
    ''' <returns>Whether closing the connection was successful.</returns>
    Function Close() As Boolean

    ''' <summary>
    ''' Executes a non-query command.
    ''' 
    ''' Non query commands are commands that do not return values, such as INSERT or UPDATE commands.
    ''' </summary>
    ''' <param name="CommandText">the command text</param>
    ''' <returns>whether the command was executed successful.</returns>
    Function ExecuteNonQuery(ByVal CommandText As String) As Boolean

    ''' <summary>
    ''' Executes an SQL command and returns the database cursor with the results.
    ''' </summary>
    ''' <param name="CommandText">the command text</param>
    ''' <returns>The cursor to the reader with the results of the query.</returns>
    Function ExecuteReader(ByVal CommandText As String) As OleDbDataReader

    ''' <summary>
    ''' Returns the cursor to the last query.
    ''' </summary>
    ''' <returns>The cursor.</returns>
    Function DBCursor() As OleDbDataReader

    ''' <summary>
    ''' Closes the current reader.
    ''' </summary>
    Sub CloseReader()

    ''' <summary>
    ''' Returns a boolean value from the reader at a given index.
    ''' </summary>
    ''' <param name="Index">The index of the parameter in the reader.</param>
    ''' <returns>The value at the given index.</returns>
    Function SecureGetBool(ByVal Index As Integer) As Boolean

    ''' <summary>
    ''' Returns an integer value from the reader at a given index.
    ''' </summary>
    ''' <param name="Index">The index of the parameter in the reader.</param>
    ''' <returns>The value at the given index.</returns>
    Function SecureGetInt32(ByVal Index As Integer) As Integer

    ''' <summary>
    ''' Returns a string from the reader at a given index.
    ''' </summary>
    ''' <param name="Index">The index of the parameter in the reader.</param>
    ''' <returns>The value at the given index.</returns>
    Function SecureGetString(ByVal Index As Integer) As String

    ''' <summary>
    ''' Returns a date and time value from the reader at a given index.
    ''' </summary>
    ''' <param name="Index">The index of the parameter in the reader.</param>
    ''' <returns>The value at the given index.</returns>
    Function SecureGetDateTime(ByVal Index As Integer) As DateTime
End Interface
