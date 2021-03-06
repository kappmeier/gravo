﻿Imports System.Data.Common
Imports System.Runtime.CompilerServices

Public Module DataBaseExtension
    ''' <summary>
    ''' Executes an SQL command and returns the database cursor with the results.
    ''' </summary>
    ''' <param name="commandText">the command text</param>
    ''' <param name="value">value for a single parameter</param>
    ''' <returns>The cursor to the reader with the results of the query.</returns>
    <Extension()>
    Function ExecuteReader(ByRef aDataBaseOperation As IDataBaseOperation, ByVal commandText As String, ByRef value As String) As DbDataReader
        Return aDataBaseOperation.ExecuteReader(commandText, Enumerable.Repeat(value, 1))
    End Function

    ''' <summary>
    ''' Checks if the curser currently open in the database operation has any data. If
    ''' it does not have data, the connection is closed and an exception thrown.
    ''' </summary>
    ''' <typeparam name="T">Type of the exception</typeparam>
    ''' <param name="aDataBaseOperation">the database access</param>
    ''' <param name="createError">the exception factory</param>
    <Extension()>
    Sub FailIfEmpty(Of T As Exception)(ByRef aDataBaseOperation As IDataBaseOperation, ByRef createError As Func(Of T))
        If aDataBaseOperation.DBCursor.HasRows = False Then
            aDataBaseOperation.DBCursor.Close()
            Throw createError.Invoke
        End If
    End Sub

    <Extension()>
    Sub FailIfExists(Of T As Exception)(ByRef aDataBaseOperation As IDataBaseOperation, ByRef createError As Func(Of T))
        If aDataBaseOperation.DBCursor.HasRows = True Then
            aDataBaseOperation.DBCursor.Close()
            Throw createError.Invoke
        End If
    End Sub

    <Extension()>
    Function IsEmpty(ByRef dataBaseOperation As IDataBaseOperation)
        Dim command As String = "SELECT count(*) FROM sqlite_master"
        dataBaseOperation.ExecuteReader(command, Array.Empty(Of Object))
        dataBaseOperation.DBCursor.Read()
        Dim objects As Int32 = dataBaseOperation.SecureGetInt32(0)
        dataBaseOperation.DBCursor.Close()
        IsEmpty = (objects = 0)
    End Function

    <Extension()>
    Public Function ExistsTable(dataBaseOperation As IDataBaseOperation, tableName As String)
        Dim command As String = "SELECT name FROM sqlite_master WHERE type='table' AND name=?"
        dataBaseOperation.ExecuteReader(command, EscapeSingleQuotes(New List(Of Object) From {tableName}))
        ExistsTable = dataBaseOperation.DBCursor.HasRows
        dataBaseOperation.DBCursor.Close()
    End Function
End Module
