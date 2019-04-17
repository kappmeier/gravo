Imports System.IO

Module DaoUtils

    Public Function GetSqliteResource(ByVal name As String)
        Dim assemblyLocation As String = System.Reflection.Assembly.GetExecutingAssembly().Location
        Dim executionPath As String = Path.GetDirectoryName(assemblyLocation)
        Return Path.Combine(executionPath, name)
    End Function
End Module
