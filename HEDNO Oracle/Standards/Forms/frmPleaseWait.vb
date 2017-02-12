'Version 1.0.3 2013-04-26
Option Strict On

Imports Microsoft.WindowsAPICodePack.Taskbar
Imports System.Runtime.InteropServices

Public Class frmPleaseWait
    Public strLanguage_PleaseWait() As String

    Public strInfoText As String = String.Empty
    Dim TimerCount As Integer = 0
    Dim HasAlreadyFinishedOnce As Boolean
    Dim HasAlreadyChangedToMarquee As Boolean

    Dim FlashWindowCurrentcount As Integer = 0
    Dim MaxFlashWindowCount As Integer = 1

    ' P/Invoke the Windows API FlashWindow to flash Taskbar button
    <DllImport("user32.dll")> _
    Private Shared Function FlashWindow(ByVal hwnd As IntPtr, ByVal bInvert As Boolean) As Boolean
    End Function

    Private Sub frmPleaseWait_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Call PleaseWait_Language(Me)

            Call frmSkin(Me, False)
            tmrPleaseWaitDots.Enabled = True

            If strInfoText <> String.Empty Then lblInfoText.Text = strInfoText Else lblInfoText.Text = strLanguage_PleaseWait(2) 'This procedure might take sever minutes

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub frmPleaseWait_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Try
            If TaskbarManager.IsPlatformSupported Then TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmPleaseWait_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        tmrFlashWindowTimer.Enabled = True
    End Sub

    Private Sub tmrPleaseWaitDots_Tick(sender As System.Object, e As System.EventArgs) Handles tmrPleaseWaitDots.Tick

        If TimerCount = 96 Then
            HasAlreadyFinishedOnce = True
            TimerCount = 0
        End If

        TimerCount += 1

        Dim Dots As String = String.Empty
        For i = 0 To TimerCount
            Dots &= "."
        Next

        lblPleaseWait.Text = strLanguage_PleaseWait(1) & Dots

        Try
            If TaskbarManager.IsPlatformSupported Then
                If Not HasAlreadyFinishedOnce Then
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal)
                    TaskbarManager.Instance.SetProgressValue(CInt(TimerCount / 2), 48)

                ElseIf Not HasAlreadyChangedToMarquee Then
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate)
                    HasAlreadyChangedToMarquee = True
                End If
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub tmrFlashWindowTimer_Tick(sender As Object, e As EventArgs) Handles tmrFlashWindowTimer.Tick
        Try
            If TaskbarManager.IsPlatformSupported Then
                If FlashWindowCurrentcount < MaxFlashWindowCount Then
                    FlashWindowCurrentcount += 1
                    ' Make the window flash or return to the original state
                    FlashWindow(Handle, True)
                Else
                    ' Restore the count variable
                    FlashWindowCurrentcount = 0
                    tmrFlashWindowTimer.Enabled = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class