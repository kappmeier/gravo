Imports System.Data.SQLite
Imports System.IO
Imports Gravo
Imports NUnit.Framework
Imports FluentAssertions
Imports Gravo.Properties
Imports Moq
Imports System.Data.Common
Imports NUnit.Compatibility

<TestFixture>
Public Class ManagementDaoTests
    Private ReadOnly ResourceFile = "test-data-management.s3db"
    Private ReadOnly IllegalResourceFile = "test-data-meta.s3db"
    Private _managementDao As ManagementDao
    Private _tempDb As String
    Private _db As IDataBaseOperation

    <SetUp>
    Public Sub Setup()
        _tempDb = Path.GetTempFileName
        File.Copy(DaoUtils.GetSqliteResource(ResourceFile), _tempDb, True)

        _db = New SQLiteDataBaseOperation()
        _db.Open(_tempDb)

        _managementDao = New ManagementDao(_db)
    End Sub

    <TearDown>
    Public Sub CleanUp()
        _db.Close()
        SQLiteConnection.ClearAllPools()

        File.Delete(_tempDb)
    End Sub

    <Test>
    Public Sub LatestVersion_NonInitialized_Works()
        Dim emptyDb As IDataBaseOperation = PropertiesDaoTests.CreateEmptyTestDb

        Dim fixture = New ManagementDao(emptyDb)

        fixture.IsVersionUpToDate().Should.BeFalse()
    End Sub

    <Test>
    Public Sub LatestVersion_Returns_Correct()
        Dim emptyDb As IDataBaseOperation = PropertiesDaoTests.CreateEmptyTestDb()

        Dim fixture = New ManagementDao(emptyDb)
        fixture.Initialize()

        Dim lastIndex = fixture.supportedVersions.Count() - 1
        For Each version As Properties.DBVersion In fixture.supportedVersions.Take(lastIndex)
            fixture.IsVersionUpToDate().Should.BeFalse()
            fixture.UpdateDatabaseSingle()
        Next version
        fixture.IsVersionUpToDate().Should.BeTrue()
    End Sub

    <Test>
    Public Sub ComplexUpdate_Returns_Correct()
        _managementDao.IsUpdateComplex(ManagementDao.DB_VERSION_1_0).Should.BeFalse()
        _managementDao.IsUpdateComplex(ManagementDao.DB_VERSION_1_1).Should.BeFalse()
        _managementDao.IsUpdateComplex(ManagementDao.DB_VERSION_1_2).Should.BeFalse()
        _managementDao.IsUpdateComplex(ManagementDao.DB_VERSION_1_3).Should.BeFalse()
        _managementDao.IsUpdateComplex(ManagementDao.DB_VERSION_1_4).Should.BeTrue()
        _managementDao.IsUpdateComplex(ManagementDao.DB_VERSION_1_5).Should.BeFalse()
        _managementDao.IsUpdateComplex(ManagementDao.DB_VERSION_1_6).Should.BeTrue()
        _managementDao.IsUpdateComplex(ManagementDao.DB_VERSION_1_7).Should.BeFalse()
    End Sub

    <Test>
    Public Sub ComplexUpdate_Throws_ForUnknownVersions()
        Assert.Throws(Of IllegalVersionException)(Sub() _managementDao.IsUpdateComplex(New Properties.DBVersion(1, 0, New Date(), "")))
    End Sub

    <Test>
    Public Sub UpdateVersion_WithIllegalData_Throws()
        Dim illegalDb As SQLiteDataBaseOperation = CreateIllegalDb()
        Dim fixture As ManagementDao = New ManagementDao(illegalDb)
        Assert.Throws(Of IllegalVersionException)(Sub() fixture.UpdateDatabaseVersion())

        CloseIllegalDb(illegalDb)
    End Sub

    Private Class FixedVersionManagementDao
        Inherits ManagementDao

        Private ReadOnly version As DBVersion

        Public Sub New(version As DBVersion)
            MyBase.New(New Mock(Of IDataBaseOperation)(MockBehavior.Strict).Object)
            Me.version = version
        End Sub

        Shadows Function GetNextVersion() As DBVersion
            Return version
        End Function

        Shadows Function GetCurrentVersion() As DBVersion
            Return version
        End Function
    End Class

    <Test>
    Public Sub UpdateVersion_WithLatestVersion_DoesNotUpdate()
        Dim mockedDb = New Mock(Of IDataBaseOperation)(MockBehavior.Strict)
        Dim mockDbDataReader = New Mock(Of DbDataReader)(MockBehavior.Strict)

        mockedDb.Setup(Function(x) x.ExecuteReader("SELECT [Version], [Date], [Description] FROM DBVersion", Array.Empty(Of Object))).Returns(mockDbDataReader.Object)
        mockedDb.Setup(Function(x) x.DBCursor()).Returns(mockDbDataReader.Object)
        mockDbDataReader.SetupSequence(Function(x) x.Read()).Returns(True).Returns(False)

        mockedDb.Setup(Function(x) x.SecureGetString(0)).Returns(ManagementDao.DB_VERSION_1_7.Major & "." & ManagementDao.DB_VERSION_1_7.Minor)
        mockedDb.Setup(Function(x) x.SecureGetDateTime(1)).Returns(ManagementDao.DB_VERSION_1_7.Introduction)
        mockedDb.Setup(Function(x) x.SecureGetString(2)).Returns(ManagementDao.DB_VERSION_1_7.Description)

        mockDbDataReader.Setup(Sub(x) x.Close())

        Dim fixture As ManagementDao = New ManagementDao(mockedDb.Object)
        fixture.UpdateDatabaseVersion()
    End Sub

    <Test>
    Public Sub UpdateVersion_WithEmtpyVersion_Throws()
        Dim emptyDb As IDataBaseOperation = PropertiesDaoTests.CreateEmptyTestDb()

        Dim fixture = New ManagementDao(emptyDb)

        Assert.Throws(Of IllegalVersionException)(Sub() fixture.UpdateDatabaseVersion())
    End Sub

    <Test>
    Public Sub Initialize_WithEmpty_Initializes()
        Dim emptyDb As IDataBaseOperation = PropertiesDaoTests.CreateEmptyTestDb()

        Dim fixture = New ManagementDao(emptyDb)

        fixture.Initialize()

        Dim result As Boolean = emptyDb.ExistsTable("DBVersion")
        result.Should().BeTrue()

        fixture.GetCurrentVersion.Should.Be(ManagementDao.DB_VERSION_1_0)
    End Sub

    <Test>
    Public Sub Initialize_WithNonEmpty_Fails()
        Assert.Throws(Of IllegalVersionException)(Sub() _managementDao.Initialize())
    End Sub

    <Test>  ', Timeout(2000) ' Not yet available in .NET standard
    Public Sub UpdateVersion_Updates_ToLatest()
        Dim testDb As IDataBaseOperation = PropertiesDaoTests.CreateEmptyTestDb()
        Dim fixture = New ManagementDao(testDb)
        fixture.Initialize()

        fixture.UpdateDatabaseVersion()

        AssertTableExists(testDb, "DBVersion")

        fixture.GetCurrentVersion.Should.Be(ManagementDao.DB_VERSION_1_7)
    End Sub

    Private Sub AssertTableExists(db As IDataBaseOperation, table As String)
        Dim result As Boolean = db.ExistsTable(table)
        result.Should().BeTrue()
    End Sub

    <Test>
    Public Sub Latest_Returns_LatestVersion()
        _managementDao.LatestVersion.Should.Be(ManagementDao.DB_VERSION_1_7)
    End Sub

    <Test>
    Public Sub CurrentVersion_Returns_CurrentVersion()
        _managementDao.GetCurrentVersion.Should.Be(ManagementDao.DB_VERSION_1_0)
    End Sub

    <Test>
    Public Sub Next_Returns_Next()
        _managementDao.GetNextVersion.Should.Be(ManagementDao.DB_VERSION_1_1)
    End Sub

    <Test>
    Public Sub Next_WithIllegalData_Throws()
        Dim illegalDb As SQLiteDataBaseOperation = CreateIllegalDb()
        Dim fixture As ManagementDao = New ManagementDao(illegalDb)
        Assert.Throws(Of IllegalVersionException)(Sub() fixture.GetNextVersion())

        CloseIllegalDb(illegalDb)
    End Sub

    <Test>
    Public Sub CreateNew_Creates_AllDataUpdated()
        Dim newDataBase = Path.GetTempFileName
        ManagementDao.CreateNewVocabularyDatabase(newDataBase)

        Dim NewDbConnection = New SQLiteDataBaseOperation()
        NewDbConnection.Open(newDataBase)
    End Sub

    <Test>
    Public Sub Reorganize_Runs_WithoutErrors()
        Dim realDataBaseResource = "test-data.s3db"

        Dim tempRealDataBase = Path.GetTempFileName
        File.Copy(DaoUtils.GetSqliteResource(realDataBaseResource), tempRealDataBase, True)

        Dim realDataBase = New SQLiteDataBaseOperation()
        realDataBase.Open(tempRealDataBase)

        Dim fixture = New ManagementDao(realDataBase)

        fixture.Reorganize()
    End Sub

    Private Function CreateIllegalDb() As SQLiteDataBaseOperation
        Dim tempIllegalDb = Path.GetTempFileName
        File.Copy(DaoUtils.GetSqliteResource(IllegalResourceFile), tempIllegalDb, True)

        CreateIllegalDb = New SQLiteDataBaseOperation()
        CreateIllegalDb.Open(tempIllegalDb)
    End Function

    Private Sub CloseIllegalDb(illegalDb As SQLiteDataBaseOperation)
        illegalDb.Close()
        SQLiteConnection.ClearAllPools()
    End Sub
End Class
