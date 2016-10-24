'Version 1.0 2012-09-22

' Selected Win API Function Calls
Public Class clsFullScreen

    Public Declare Function GetSystemMetrics Lib "user32.dll" Alias "GetSystemMetrics" (ByVal which As Integer) As Integer
    Public Declare Sub SetWindowPos Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal hwndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal flags As UInteger)

    Private Const SM_CXSCREEN As Integer = 0
    Private Const SM_CYSCREEN As Integer = 1
    Private Shared HWND_TOP As IntPtr = IntPtr.Zero
    Private Const SWP_SHOWWINDOW As Integer = 64 ' 0x0040

    Public Shared ReadOnly Property ScreenX As Integer
        Get
            Return GetSystemMetrics(SM_CXSCREEN)
        End Get
    End Property

    Public Shared ReadOnly Property ScreenY As Integer
        Get
            Return GetSystemMetrics(SM_CYSCREEN)
        End Get
    End Property

    Public Shared Sub SetWinFullScreen(ByVal hwnd As IntPtr)
        SetWindowPos(hwnd, HWND_TOP, 0, 0, ScreenX, ScreenY, SWP_SHOWWINDOW)
    End Sub
End Class

' Class used to preserve / restore / maximize state of the form
Public Class FormState
    Private winState As FormWindowState
    Private brdStyle As FormBorderStyle
    Private topMost As Boolean
    Private bounds As Rectangle
    Private wasMDI As Boolean

    Private IsMaximized As Boolean = False

    Public Sub Maximize(ByVal targetForm As Form)
        If Not IsMaximized Then
            Call Save(targetForm)
            targetForm.Visible = False
            IsMaximized = True

            If FullScreenWindowed Then
                targetForm.MaximizeBox = False
                targetForm.FormBorderStyle = FormBorderStyle.Fixed3D
                targetForm.WindowState = FormWindowState.Normal
            Else
                targetForm.FormBorderStyle = FormBorderStyle.None
                targetForm.WindowState = FormWindowState.Maximized
            End If

            clsFullScreen.SetWinFullScreen(targetForm.Handle)
            targetForm.IsMdiContainer = True
            If targetForm Is frmMain Then isMainFormMDI = True

            targetForm.TopMost = True
            If targetForm Is frmMain Then UpdateTexts(frmMain)
            Call SaveChangesToSettingsIni(targetForm)

            targetForm.Visible = True
        End If
    End Sub

    Public Sub Save(ByVal targetForm As Form)
        winState = targetForm.WindowState
        brdStyle = targetForm.FormBorderStyle
        topMost = targetForm.TopMost
        bounds = targetForm.Bounds
        wasMDI = targetForm.IsMdiContainer
    End Sub

    Public Sub Restore(ByVal targetForm As Form)
        targetForm.Visible = False

        targetForm.WindowState = winState
        targetForm.FormBorderStyle = brdStyle
        targetForm.TopMost = topMost
        targetForm.Bounds = bounds
        targetForm.IsMdiContainer = wasMDI
        IsMaximized = False
        If targetForm Is frmMain Then UpdateTexts(frmMain)

        targetForm.Visible = True
    End Sub

    Private Sub SaveChangesToSettingsIni(ByVal targetForm As Form)
        If RememberWindowState AndAlso Not FullScreen Then
            strSettings(27) = "027WindowState=" & targetForm.WindowState
            If targetForm.WindowState = FormWindowState.Normal Then
                If targetForm.Width = OriginalWindowWidth Then strSettings(28) = "028WindowWidth=Form" Else strSettings(28) = "028WindowWidth=" & targetForm.Width
                If targetForm.Height = OriginalWindowHeight Then strSettings(29) = "028WindowWidth=Form" Else strSettings(29) = "029WindowHeight=" & targetForm.Height
            End If
            Call WriteSettings(strSettings, "clsFullScreen SaveChangesToSettingsIni")
        End If
    End Sub
End Class