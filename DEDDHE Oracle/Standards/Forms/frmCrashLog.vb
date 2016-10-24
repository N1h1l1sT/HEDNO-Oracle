'Version 1.2 2016/07/23
Option Strict On

Public Class frmCrashLog
    Public strLanguage_Crush() As String
    Public CrashFilePath() As String = {}

    Private Sub frmCrush_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Call Crash_Language(Me)

            Call frmSkin(Me, False)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSendCrashLog_Click(sender As Object, e As EventArgs) Handles btnSendCrashLog.Click
        On Error Resume Next

        btnSendCrashLog.Enabled = False
        btnCancel.Enabled = False

        'Sending the CrashLog
        Dim isFirstTime As Boolean = strSettings(36).Substring("036LastCrashDate=".Length) = ""
        Dim LastCrashNum As Integer = CInt(strSettings(37).Substring("037LastCrashNum=".Length))
        Dim TwentyDaysHavePassed As Boolean

        If Not isFirstTime Then
            TwentyDaysHavePassed = (Today - Date.ParseExact(strSettings(36).Substring("036LastCrashDate=".Length), "d.M.yyyy", System.Globalization.CultureInfo.InvariantCulture)) > TimeSpan.FromDays(20)
            If TwentyDaysHavePassed Then LastCrashNum = 0
        End If

        If isFirstTime OrElse TwentyDaysHavePassed OrElse LastCrashNum <= 10 Then
            Dim isMailSent As Boolean = SendEmail("Crash: """ & My.Application.Info.Title & """ v" & My.Application.Info.Version.ToString & vbCrLf & vbCrLf & txtWhatCausedTheException.Text, True,
              DecypherToText(My.Settings.mFrom, MailSettingsCipherLevel), DecypherToText(My.Settings.mPW, MailSettingsCipherLevel), DecypherToText(My.Settings.mTo, MailSettingsCipherLevel),
              DecypherToText(My.Settings.mClientHost, MailSettingsCipherLevel), CrashFilePath, "Crash: ", My.Settings.mSSL, , My.Settings.mPort)
            If isMailSent Then
                strSettings(36) = "036LastCrashDate=" & Today.Day & "." & Today.Month & "." & Today.Year
                strSettings(37) = "037LastCrashNum=" & (LastCrashNum + 1)
                WriteSettings(strSettings, "Emailing a Crash")
            End If
        End If

        btnSendCrashLog.Enabled = True
        btnCancel.Enabled = True

        Close()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub
End Class