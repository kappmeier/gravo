Imports Gravo
Imports Gravo.localization


''' <summary>
''' A window for testing vocabulary.
''' 
''' How it should work:
''' - Input: List of words. Computed outside. Also the order of words is computed outside
''' - Vocabulary checker class checks
''' - The list is updated. E.g. the word is queued in again for later re-testing
''' - The database is updated with test result, etc.
''' 
''' </summary>
Public Class TestSimple
    Dim voc As xlsTestBase
    Dim db As New SQLiteDataBaseOperation()

    Dim startVal As String
    Dim controller As TestController
    Dim checker As Checker

    Private ReadOnly dictionaryDao As IDictionaryDao = New DictionaryDao(db)

    ''' <summary>
    ''' Initializes the word with given set of test data
    ''' </summary>
    ''' <param name="testController"></param>
    Public Sub New(ByVal testController As TestController)

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Me.controller = testController
    End Sub

    Public Sub New(ByVal OneLanguage As Boolean, ByVal Language As String, ByRef Owner As Main)
        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        'voc = New xlsTestBase
        db.Open(DBPath)     ' Datenbank öffnen
        'voc.DBConnection = db
        If OneLanguage Then
            startVal = Language
        Else
            startVal = ""
        End If
    End Sub

    Public Sub New(ByVal GroupName As String, ByRef Owner As Main)
        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        'voc = New xlsTestGroup
        db.Open(DBPath)     ' Datenbank öffnen
        'voc.DBConnection = db
        startVal = GroupName
    End Sub

    Private Sub TestSimple_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Take a word from testdata

        checker = controller.GetTestChecker
        CheckForQuit()
        DisplayCurrentWord()

        txtInput.Focus()
        LocalizationChanged()
    End Sub

    Public Overrides Sub LocalizationChanged()
        lblAdditionalInfoDescription.Text = GetLoc.GetText(TEST_INFO)
        lblMeaningDescription.Text = GetLoc.GetText(TEST_MEANING)
        lblTestInformationDescription.Text = GetLoc.GetText(TEST_TEST)
        cmdOK.Text = GetLoc.GetText(BUTTON_OK)
        cmdExit.Text = GetLoc.GetText(BUTTON_CLOSE)
        Me.Text = GetLoc.GetText(TEST_TITLE)
    End Sub

    Public Sub Start()
        If startVal = "" Then
            voc.Start()
        Else
            voc.Start(startVal)
        End If
        voc.NextWord()
    End Sub

    Public ReadOnly Property RestCount()
        Get
            Return voc.WordCount
        End Get
    End Property

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Dim result As TestResult = checker.Evaluate(txtInput.Text.Trim)
        Dim oldChecker As Checker = checker

        controller.Update(result)
        checker = controller.GetTestChecker
        If Not checker Is Nothing AndAlso checker.Retest Then
            Select Case result
                Case TestResult.OtherMeaning
                    ' Richtig, aber nicht die gewünschte Bedeutung
                    lblTestInformation.Text = GetLoc.GetText(TEST_ANOTHER_MEANING)
                    txtInput.SelectAll()
                Case TestResult.Wrong
                    lblTestInformation.Text = GetLoc.GetText(TEST_WRONG_HINT) & vbCrLf & checker.Question & " = " & checker.Answer
                    MsgBox(GetLoc.GetText(TEST_WRONG), MsgBoxStyle.Information, GetLoc.GetText(TEST_ERROR))
                    txtInput.Text = ""
                Case TestResult.Misspelled
                    lblTestInformation.Text = GetLoc.GetText(TEST_TYPE_ERROR)
            End Select
        Else
            If result = TestResult.Wrong Then
                lblTestInformation.Text = GetLoc.GetText(TEST_WRONG_HINT) & vbCrLf & oldChecker.Question & " = " & oldChecker.Answer
                MsgBox(GetLoc.GetText(TEST_WRONG), MsgBoxStyle.Information, GetLoc.GetText(TEST_ERROR))
            End If
            CheckForQuit()
            DisplayCurrentWord()
        End If
        txtInput.Focus()
    End Sub

    Private Sub DisplayCurrentWord()
        lblWord.Text = checker.Question()
        lblAdditionalInfo.Text = checker.Info()
        lblTestInformation.Text = ""
        txtInput.Text = ""
        lblCount.Text = controller.count()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
        Close()
    End Sub

    Private Sub CheckForQuit()
        If Not controller.HasWords Then
            lblWord.Text = ""
            lblTestInformation.Text = " "
            lblAdditionalInfo.Text = ""
            MsgBox(GetLoc.GetText(TEST_FINISHED), MsgBoxStyle.Information, TEST_WELL_DONE)
            Dim frmMain As Main = Me.Owner
            Close()
            frmMain.TestFinished()
        End If
    End Sub

    Public WriteOnly Property TestFormerLanguage() As Boolean
        Set(ByVal value As Boolean)
            voc.TestFormerLanguage = value
        End Set
    End Property

    Public WriteOnly Property UseCards() As Boolean
        Set(ByVal value As Boolean)
            voc.UseCards = value
        End Set
    End Property

    Public WriteOnly Property TestSetPhrases() As Boolean
        Set(ByVal value As Boolean)
            voc.TestSetPhrases = value
        End Set
    End Property

    Public WriteOnly Property TestMarked() As Boolean
        Set(ByVal value As Boolean)
            If TypeOf voc Is xlsTestGroup Then
                Dim voc2 As xlsTestGroup = voc
                voc2.TestMarked = value
            End If
        End Set
    End Property

    Public WriteOnly Property RandomOrder() As Boolean
        Set(ByVal value As Boolean)
            If value Then
                voc.TestStyle = xlsTestStyle.RandomTestAgain
            Else
                voc.TestStyle = xlsTestStyle.TestAgain
            End If
        End Set
    End Property
End Class