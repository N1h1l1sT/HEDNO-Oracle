'Version 1.3.5 2016/07/06
'Fixed it so that if the current version is newer than the online one, nothing happens
'Added a "Please Wait" form and made the downloading Async; Fixed a bug that the update seached for the Edition Num even though none existed.
'Added doUpdateDependingOnProgramEdition(); Added option to avoid asking on startup
Option Strict On

Imports System.IO
Imports System.Net

Module modUpdate
#Region "Constants and Variables"

    '=========================
    '==Changes Per Programme==
    '=========================
    Public Const MinEditionForUpgrade As Integer = -1
    '/=======================\
    '/=Changes Per Programme=\
    '/=======================\

    Public isProgramUpdated As Boolean
    Public isProgramUpgraded As Boolean
    Public CheckForNewVersionOnStartup As Boolean

    Dim DownloadClient As New WebClient

    Dim NewVersionOnlineRar, NewVersionLocalRar, NewVersionLocalExe As String
    Dim strNewVersion As String = My.Application.Info.Version.ToString 'Initial (its current or old)
    Dim strOnlineMainInfo() As String
    Dim lstAvailableVersionUpdates As New List(Of String)
#End Region

    'To be used on the ChangeLanguage_Main
    Public Sub UpdateTexts(ByVal frm As frmMain)
        With frm
            If isProgramUpdated Then
                .lblProgUpdated.Text = strModLanguage(3) 'Program Updated
                .lblProgUpdated.ForeColor = Color.LightGreen
                .lblProgUpdated.BackColor = Color.Black

            ElseIf BETA = True Then
                .lblProgUpdated.Text = "Alpha Updated status: N/A"
                .lblProgUpdated.ForeColor = Color.Gold
                .lblProgUpdated.BackColor = Color.Gray
            Else
                .lblProgUpdated.Text = strModLanguage(5) 'Program Outdated
                .lblProgUpdated.ForeColor = Color.MistyRose
                .lblProgUpdated.BackColor = Color.Black
            End If

            .lblProgUpdated.Location = New Point(.Width - 45 - .lblProgUpdated.Width, .lblProgUpdated.Location.Y)
            .lblProgUpgraded.Location = New Point(.Width - 45 - .lblProgUpgraded.Width, .lblProgUpgraded.Location.Y)

        End With
    End Sub

    Public Async Function doCheckUpdate(ByVal frm As frmMain) As Task(Of Boolean)
        With frm
            .lblProgUpdated.Visible = True
            If isInternetAvailable Then
                Try
                    strOnlineMainInfo = DownloadClient.DownloadString(MainFolderOnline & "MainInfo.txt").Split(New String() {System.Environment.NewLine}, StringSplitOptions.None)
                Catch ex As Exception
                    isProgramUpdated = False
                    .lblProgUpdated.Visible = False
                    Return False
                End Try

                If Not CBool(strOnlineMainInfo(2).Substring("UpdateInProgress=".Length)) Then
                    strNewVersion = GetSubstrAfterString(strOnlineMainInfo(1), "=", , False)

                    If strNewVersion = My.Application.Info.Version.ToString Then
                        isProgramUpdated = True
                        Call UpdateTexts(frm)
                        Return False

                    ElseIf strNewVersion <> "" Then
                        'To check the current version is indeed newer than the online one, hence this is an updated Alpha/Beta
                        Dim MajorVersionIndex As Integer = strNewVersion.IndexOf(".")
                        Dim MajorVersionOnline As Integer = CInt(strNewVersion.Substring(0, MajorVersionIndex))
                        Dim MinorVersionSubString As String = GetSubstrAfterString(strNewVersion, ".", "0")
                        Dim MinorVersionIndex As Integer = MinorVersionSubString.IndexOf(".")
                        Dim MinorVersionOnline As Integer = CInt(MinorVersionSubString.Substring(0, MinorVersionIndex))
                        Dim YearVersionSubString As String = GetSubstrAfterString(MinorVersionSubString, ".", "00")
                        Dim YearVersionIndex As Integer = YearVersionSubString.IndexOf(".")
                        Dim YearVersionOnline As Integer = CInt(YearVersionSubString.Substring(0, YearVersionIndex))
                        Dim MonthVersionOnline As Integer
                        Dim DayVersionOnline As Integer = 0

                        If (strNewVersion.Length - strNewVersion.LastIndexOf(".") - 1) = 4 Then 'if month starts with 0, the 0 is omitted in the version string, hence, MDD instead of MMDD
                            MonthVersionOnline = CInt(strNewVersion.Substring(strNewVersion.Length - 4, 2))
                            DayVersionOnline = CInt(strNewVersion.Substring(strNewVersion.Length - 2, 2))
                        ElseIf (strNewVersion.Length - strNewVersion.LastIndexOf(".") - 1) = 3 Then
                            MonthVersionOnline = CInt(strNewVersion.Substring(strNewVersion.Length - 3, 1))
                            DayVersionOnline = CInt(strNewVersion.Substring(strNewVersion.Length - 2, 2))
                        ElseIf (strNewVersion.Length - strNewVersion.LastIndexOf(".") - 1) = 2 Then
                            MonthVersionOnline = CInt(strNewVersion.Substring(strNewVersion.Length - 2, 1))
                            DayVersionOnline = CInt(strNewVersion.Substring(strNewVersion.Length - 1, 1))
                        ElseIf (strNewVersion.Length - strNewVersion.LastIndexOf(".") - 1) = 1 Then
                            MonthVersionOnline = CInt(strNewVersion.Substring(strNewVersion.Length - 1, 1))
                        End If


                        Dim MajorVersionLocal As Integer = My.Application.Info.Version.Major
                        Dim MinorVersionLocal As Integer = My.Application.Info.Version.Minor
                        Dim YearVersionLocal As Integer = My.Application.Info.Version.Build
                        Dim MonthVersionLocal As Integer = 0
                        Dim DayVersionLocal As Integer = 0

                        If My.Application.Info.Version.MinorRevision.ToString.Length = 4 Then
                            MonthVersionLocal = CInt(My.Application.Info.Version.MinorRevision.ToString.Substring(0, 2))
                            DayVersionLocal = CInt(My.Application.Info.Version.MinorRevision.ToString.Substring(2, 2))
                        ElseIf My.Application.Info.Version.MinorRevision.ToString.Length = 3 Then
                            MonthVersionLocal = CInt(My.Application.Info.Version.MinorRevision.ToString.Substring(0, 1))
                            DayVersionLocal = CInt(My.Application.Info.Version.MinorRevision.ToString.Substring(1, 2))
                        ElseIf My.Application.Info.Version.MinorRevision.ToString.Length = 2 Then
                            MonthVersionLocal = CInt(My.Application.Info.Version.MinorRevision.ToString.Substring(0, 1))
                            DayVersionLocal = CInt(My.Application.Info.Version.MinorRevision.ToString.Substring(1, 1))
                        ElseIf My.Application.Info.Version.MinorRevision.ToString.Length = 1 Then
                            MonthVersionLocal = CInt(My.Application.Info.Version.MinorRevision.ToString.Substring(0, 1))
                        End If

                        If (MajorVersionLocal > MajorVersionOnline) OrElse
                            (MajorVersionLocal = MajorVersionOnline And MinorVersionLocal > MinorVersionOnline) OrElse
                            (MajorVersionLocal = MajorVersionOnline And MinorVersionLocal = MinorVersionOnline And YearVersionLocal > YearVersionOnline) OrElse
                            (MajorVersionLocal = MajorVersionOnline And MinorVersionLocal = MinorVersionOnline And YearVersionLocal = YearVersionOnline And MonthVersionLocal > MonthVersionOnline) OrElse
                            (MajorVersionLocal = MajorVersionOnline And MinorVersionLocal = MinorVersionOnline And YearVersionLocal = YearVersionOnline And MonthVersionLocal = MonthVersionOnline And DayVersionLocal > DayVersionOnline) Then
                            isProgramUpdated = True
                            Call UpdateTexts(frm)
                            Return False

                        Else

                            Dim Question = MsgBox(strModLanguage(4), MsgBoxStyle.YesNo, strModLanguage(6)) 'An update is available, Would you like to download it?
                            If Question = vbYes Then
                                Try
                                    My.Settings.UpdateFile = DownloadClient.DownloadString(MainFolderOnline & "update.ini")   'in case there is no more space for the program i will upload it elsewhere and make the program download it from there
                                Catch ex As Exception
                                    MsgBox(strModLanguage(11) & vbCrLf & strModLanguage(12) & My.Application.Info.CompanyName & strModLanguage(13), MsgBoxStyle.Exclamation) 'There was an error during the auto-update process.
                                    isProgramUpdated = False
                                    .lblProgUpdated.Visible = False
                                    Return False
                                End Try

                                Dim iWebsite As Integer = My.Settings.UpdateFile.IndexOf("="c) + 1
                                NewVersionOnlineRar = (My.Settings.UpdateFile.Substring(iWebsite) & My.Application.Info.Title & " Version " & strNewVersion & " " & ProgramArchitecture & "bit.rar").Replace(" ", "%20")
                                NewVersionLocalRar = strExtras & My.Application.Info.Title & " Version " & strNewVersion & " " & ProgramArchitecture & "bit.rar"
                                NewVersionLocalExe = My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & My.Application.Info.Title & " Version " & strNewVersion & " " & ProgramArchitecture & "bit.exe"

                                If Not File.Exists(NewVersionLocalExe) Then
#If DEBUG Then
                                    Try
                                        My.Computer.Clipboard.SetText(NewVersionOnlineRar)
                                    Catch ex As Exception
                                    End Try
                                    MsgBox("NewVersionOnlineRar=""" & NewVersionOnlineRar & """" & vbCrLf &
                                           "NewVersionLocalRar=""" & NewVersionLocalRar & """" & vbCrLf &
                                           "NewVersionLocalExe=""" & NewVersionLocalExe & """")
#End If
                                    Call PleaseWait(strModLanguage(16)) 'The downloading procedure of the updated version might take several minutes.
                                    Try
                                        frm.Opacity = 0
                                        frm.ShowInTaskbar = False
                                        Await Task.Run(
                                                        Sub()
                                                            DownloadClient.DownloadFile(NewVersionOnlineRar, NewVersionLocalRar)
                                                        End Sub)
                                        Unrar(NewVersionLocalRar, My.Computer.FileSystem.SpecialDirectories.Desktop, , True, , , , True)

                                    Catch ex As Exception
                                        frm.Opacity = 1
                                        frm.ShowInTaskbar = True
                                        MsgBox(strModLanguage(11) & vbCrLf & strModLanguage(12) & My.Application.Info.CompanyName & strModLanguage(13) & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation) 'There was an error during the out-update process.
                                        isProgramUpdated = False
                                        .lblProgUpdated.Visible = False
                                        Call ClosePleaseWaitForm() 'Ending the Please Wait Form
                                        Return False
                                    End Try
                                    File.Delete(NewVersionLocalRar)
                                    Call ClosePleaseWaitForm() 'Ending the Please Wait Form
                                End If

                                RunOpenDir(strStartupExe, """" & NewVersionLocalExe & """ none")
                                Application.Exit()

                            Else
                                isProgramUpdated = False
                                Call UpdateTexts(frm)
                            End If
                            Return True
                        End If

                    Else
                        isProgramUpdated = False
                        .lblProgUpdated.Text = strModLanguage(7) 'Unavailable Update information
                        Return False
                    End If

                Else
                    isProgramUpdated = False
                    Call UpdateTexts(frm)
                    MsgBox(strModLanguage(14) & vbCrLf & strModLanguage(15), MsgBoxStyle.Information) 'An update to this version, that will be available soon, is being uploaded to the remote server.
                    Return True
                End If

            Else
                isProgramUpdated = False
                .lblProgUpdated.Visible = False
                Return False
            End If

        End With
    End Function

    Public Async Function doUpdateDependingOnProgramEdition(ByVal frm As frmMain) As Task(Of Boolean)
        Return Await doCheckUpdate(frm)
    End Function

End Module
