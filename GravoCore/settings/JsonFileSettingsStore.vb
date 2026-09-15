Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Json
Imports System.Xml

''' <summary>
''' Stores the application settings as simple key:value pairs as JSON object in a single file.
''' </summary>
Public Class JsonFileSettingsStore
    Implements ISettingsStore

    Private ReadOnly filePath As String
    Private ReadOnly serializer As New DataContractJsonSerializer(GetType(Dictionary(Of String, String)),
            New DataContractJsonSerializerSettings With {.UseSimpleDictionaryFormat = True})

    Public Sub New(path As String)
        filePath = path
    End Sub

    ''' <summary>
    ''' Returns the machine-dependent default path to the settings file.
    ''' <c>%AppData%\Gravo\settings.json</c> on Windows, <c>~/.config/Gravo/settings.json</c> elsewhere.
    ''' </summary>
    Public Shared Function DefaultPath() As String
        Return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Gravo", "settings.json")
    End Function

    ''' <summary>
    ''' Returns the persisted values. Empty when the file is missing or cannot be parsed.
    ''' </summary>
    Public Function Load() As IDictionary(Of String, String) Implements ISettingsStore.Load
        If Not File.Exists(filePath) Then Return New Dictionary(Of String, String)()
        Try
            Using stream As FileStream = File.OpenRead(filePath)
                Return If(TryCast(serializer.ReadObject(stream), Dictionary(Of String, String)),
                    New Dictionary(Of String, String)())
            End Using
        Catch ex As SerializationException
            Return New Dictionary(Of String, String)()
        Catch ex As XmlException
            Return New Dictionary(Of String, String)()
        End Try
    End Function

    ''' <summary>
    ''' Writes the values. If needed, the directory is created.
    ''' </summary>
    Public Sub Save(values As IDictionary(Of String, String)) Implements ISettingsStore.Save
        Dim folder As String = Path.GetDirectoryName(filePath)
        If Not String.IsNullOrEmpty(folder) Then Directory.CreateDirectory(folder)
        Using stream As FileStream = File.Create(filePath)
            serializer.WriteObject(stream, New Dictionary(Of String, String)(values))
        End Using
    End Sub
End Class
