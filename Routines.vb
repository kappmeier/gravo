Imports System.Runtime.InteropServices

Public Module Routines


    Private Declare Function SHBrowseForFolder Lib "shell32.dll" Alias "SHBrowseForFolderA" (ByRef lpBrowseInfo As BROWSEINFO) As Long
    Private Declare Function SHGetPathFromIDList Lib "shell32.dll" Alias "SHGetPathFromIDListA" (ByRef pidl As Long, ByRef pszPath As String) As Long

    Public Structure BROWSEINFO
        Public hOwner As Integer
        Public pidlRoot As Integer
        Public pszDisplayName As String
        Public lpszTitle As String
        Public ulFlags As Integer
        Public lpfn As Integer
        Public lParam As Integer
        Public iImage As Integer
    End Structure

    Private Declare Sub GlobalMemoryStatus Lib "kernel32.dll" (ByRef lpBuffer As MEMORYSTATUS)

    <StructLayout(LayoutKind.Sequential)> Public Structure MEMORYSTATUS
        Public dwLength As Int32
        ' Muss nicht manuell gesetzt werden.
        Public dwMemoryLoad As Int32
        Public dwTotalPhys As Int32
        Public dwAvailPhys As Int32
        Public dwTotalPageFile As Int32
        Public dwAvailPageFile As Int32
        Public dwTotalVirtual As Int32
        Public dwAvailVirtual As Int32
    End Structure


    Public Function GetDir(ByVal hWnd As Long, ByVal sTitle As String)
        Dim m_ms As MEMORYSTATUS
        GlobalMemoryStatus(m_ms)

        Dim fRet As Boolean
        Dim lPid As Long
        Dim sFolder As String
        Dim BrInfo As BROWSEINFO
        Dim IDRoot As Long

        'BrInfo = New BROWSEINFO()

        BrInfo.hOwner = hWnd            ' hwnd der parent form
        BrInfo.pidlRoot = IDRoot
        BrInfo.lpszTitle = sTitle       ' titeltext
        BrInfo.ulFlags = 1              ' nur datenträger auswählbar,keine systemordner

        lPid = SHBrowseForFolder(BrInfo)
        sFolder = New String("a", 512)
        'sFolder = Chr(0) & Chr(0) & Chr(0) & Chr(0)
        Call SHGetPathFromIDList(lPid, sFolder)

        If fRet Then
            Return Left(sFolder, InStr(sFolder, Chr(0)) - 1)
        Else
            Return ""
        End If
    End Function

End Module
