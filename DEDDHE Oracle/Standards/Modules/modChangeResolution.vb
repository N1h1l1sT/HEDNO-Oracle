'Version 1.0.1 2012-09-29
'Added and if to not change resolution when the required resolution is the current one
Imports System.Runtime.InteropServices

Module modChangeResolution

    Public OriginalScreenWidth As Integer = My.Computer.Screen.Bounds.Width
    Public OriginalScreenHeight As Integer = My.Computer.Screen.Bounds.Height

    Const ENUM_CURRENT_SETTINGS As Integer = -1
    Const CDS_UPDATEREGISTRY As Integer = &H1
    Const CDS_TEST As Long = &H2

    Const CCDEVICENAME As Integer = 32
    Const CCFORMNAME As Integer = 32

    Const DISP_CHANGE_SUCCESSFUL As Integer = 0
    Const DISP_CHANGE_RESTART As Integer = 1
    Const DISP_CHANGE_FAILED As Integer = -1

    Private Declare Function EnumDisplaySettings Lib "user32" Alias "EnumDisplaySettingsA" (ByVal lpszDeviceName As Integer, ByVal iModeNum As Integer, ByRef lpDevMode As DEVMODE) As Integer
    Private Declare Function ChangeDisplaySettings Lib "user32" Alias "ChangeDisplaySettingsA" (ByRef DEVMODE As DEVMODE, ByVal flags As Integer) As Integer

    <StructLayout(LayoutKind.Sequential)> Public Structure DEVMODE
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=CCDEVICENAME)> Public dmDeviceName As String
        Public dmSpecVersion As Short
        Public dmDriverVersion As Short
        Public dmSize As Short
        Public dmDriverExtra As Short
        Public dmFields As Integer
        Public dmOrientation As Short
        Public dmPaperSize As Short
        Public dmPaperLength As Short
        Public dmPaperWidth As Short
        Public dmScale As Short
        Public dmCopies As Short
        Public dmDefaultSource As Short
        Public dmPrintQuality As Short
        Public dmColor As Short
        Public dmDuplex As Short
        Public dmYResolution As Short
        Public dmTTOption As Short
        Public dmCollate As Short
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=CCFORMNAME)> Public dmFormName As String
        Public dmUnusedPadding As Short
        Public dmBitsPerPel As Integer
        Public dmPelsWidth As Integer
        Public dmPelsHeight As Integer
        Public dmDisplayFlags As Integer
        Public dmDisplayFrequency As Integer
    End Structure

    Public Sub ChangeResolution(ByVal Width As Integer, ByVal Height As Integer, ByVal DoNotUpdateSettings As Boolean, Optional ByVal ShowSuccessMessage As Boolean = False, Optional ByVal ShowErrorMessage As Boolean = True, Optional ShowRestartInfo As Boolean = True)
        If Width <> My.Computer.Screen.Bounds.Width OrElse Height <> My.Computer.Screen.Bounds.Height Then
            Try
                Dim DevM As DEVMODE

                DevM.dmDeviceName = New [String](New Char(32) {})
                DevM.dmFormName = New [String](New Char(32) {})
                DevM.dmSize = CShort(Marshal.SizeOf(GetType(DEVMODE)))



                If 0 <> EnumDisplaySettings(Nothing, ENUM_CURRENT_SETTINGS, DevM) Then
                    Dim lResult As Integer

                    DevM.dmPelsWidth = Width
                    DevM.dmPelsHeight = Height

                    lResult = ChangeDisplaySettings(DevM, CDS_TEST)

                    If lResult = DISP_CHANGE_FAILED Then
                        If ShowErrorMessage Then MsgBox(strModLanguage(104), MsgBoxStyle.Critical) 'Screen Resolution Change Failed.
                    Else

                        lResult = ChangeDisplaySettings(DevM, CDS_UPDATEREGISTRY)

                        Select Case lResult
                            Case DISP_CHANGE_RESTART
                                If ShowRestartInfo Then MsgBox(strModLanguage(101), MsgBoxStyle.Critical, strModLanguage(102)) 'You must restart your computer to apply these changes. Screen Resolution has Changed
                                If RememberWindowState Then
                                    If Width = OriginalWindowWidth Then strSettings(25) = "Form" Else strSettings(25) = "025FullScreenWidth=" & Width
                                    If Height = OriginalWindowHeight Then strSettings(26) = "Form" Else strSettings(26) = "026FullScreenHeight=" & Height
                                    WriteSettings(strSettings, "ChangeResolution: Restart Required")
                                End If
                            Case DISP_CHANGE_SUCCESSFUL
                                If ShowSuccessMessage Then MsgBox(strModLanguage(103), MsgBoxStyle.Information) 'Screen Resolution change was Successful.
                                If RememberWindowState AndAlso Not DoNotUpdateSettings Then
                                    strSettings(25) = "025FullScreenWidth=" & Width
                                    strSettings(26) = "026FullScreenHeight=" & Height
                                    WriteSettings(strSettings, "ChangeResolution: Change Successful")
                                End If
                            Case Else
                                If ShowErrorMessage Then MsgBox(strModLanguage(104), MsgBoxStyle.Critical) 'Screen Resolution Change Failed.
                        End Select
                    End If

                End If

            Catch ex As Exception
                CreateCrashFile(ex, True)
            End Try
        End If
    End Sub

    Public Sub RestoreResolution()
        If My.Computer.Screen.Bounds.Width <> OriginalScreenWidth OrElse My.Computer.Screen.Bounds.Height <> OriginalScreenHeight Then
            ChangeResolution(OriginalScreenWidth, OriginalScreenHeight, True, False, False, False)
        End If
    End Sub

End Module
