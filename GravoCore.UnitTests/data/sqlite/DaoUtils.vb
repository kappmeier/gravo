Imports System.IO

Module DaoUtils

    Public Function GetSqliteResource(ByVal name As String) As String
        Dim assemblyLocation As String = System.Reflection.Assembly.GetExecutingAssembly().Location
        Dim executionPath As String = Path.GetDirectoryName(assemblyLocation)
        Return Path.Combine(executionPath, name)
    End Function

    ''' <summary>
    ''' Returns a CREATE TRIGGER statement that aborts every insert on the given table.
    ''' </summary>
    Public Function FailWriteInsertTrigger(ByVal table As String) As String
        Return FailWriteTrigger($"BEFORE INSERT ON [{table}]")
    End Function

    ''' <summary>
    ''' Returns a CREATE TRIGGER statement that aborts every update on the given table,
    ''' or only updates of the given column when one is passed.
    ''' </summary>
    Public Function FailWriteUpdateTrigger(ByVal table As String, Optional ByVal column As String = Nothing) As String
        Dim updateOfColumn As String = If(column Is Nothing, "", $" OF [{column}]")
        Return FailWriteTrigger($"BEFORE UPDATE{updateOfColumn} ON [{table}]")
    End Function

    Private Function FailWriteTrigger(ByVal beforeClause As String) As String
        Return $"
            CREATE TRIGGER fail_write
            {beforeClause}
             BEGIN
                   SELECT RAISE(ABORT, 'forced');
               END
        "
    End Function
End Module
