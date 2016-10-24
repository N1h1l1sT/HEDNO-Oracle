'Version 1.8.6  2013-08-03
'Needs Settings.lng V1.8.2
'Fixed a bug that made the Window State "Minimised" instead of "Maximised" when all options were available; Changed txtLanguage to strLanguage_Settings
'3  =   Unknown
'4  =   Auto
'5  =   None
'21 =   Default
'22 =   Error
Option Strict On

Imports System.IO
Imports System.Threading
Imports Microsoft.Win32

Public Class frmSettings
    Dim SavedText As String

    Public strLanguage_Settings() As String
    Dim strDirectories() As String

    Dim isLoading As Boolean = True
    Dim Answer As MsgBoxResult

    Dim LanguageName As String          'Used to extract the Lang File's name to add it to the cb
    Dim SkinName As String              'Same for skin

    Dim cbLanguageLastIndex As Integer

    Dim lstWidth As New List(Of Integer)
    Dim lstHeight As New List(Of Integer)
    Dim lstResolutions As List(Of String)

    Dim FullScreenEnabled As Boolean
    Dim FullScreenWindowedEnabled As Boolean
    Dim WindowStateEnabled As Boolean
    Dim RemWindowStateEnabled As Boolean

    Dim AllowResolutionAccessFromSettings As Boolean
    Dim lstRestartNeeded As New List(Of String)
    '=============================
    '==END OF STANDARD PROCEDURE==
    '=============================


    '============================
    '==BEGIN STANDARD PROCEDURE==
    '============================

    Private Sub RestoreTexts()
        cbLanguage.Text = ""
        cbSkin.Text = ""
        cbStartWithWin.Text = ""
        txtDelayTime.Text = ""
        cbShowStartupForm.Text = ""
        cbCheckForNewVersion.Text = ""
        cbRemWindowState.Text = ""
        cbWindowState.Text = ""
        cbFullScreenResolutions.Text = ""
        txtWindowResolutionWidth.Text = ""
        txtWindowResolutionHeight.Text = ""

        'Tab: Database
        cbSplitDbEveryMonth.Text = ""
        txtDBFile.Text = ""
        txtDBpass.Text = ""
        cbAccessType.Text = ""
        txtDatabaseTables.Text = ""
        txtProtectedTables.Text = ""


        '=============================
        '==END OF STANDARD PROCEDURE==
        '=============================

        '==========
        '==CUSTOM==
        '==========

        '/===============\
        '/=END OF CUSTOM=\
        '/===============\

    End Sub

    Private Sub RestoreTags()
        cbLanguage.Tag = 0
        cbSkin.Tag = 0
        cbStartWithWin.Tag = 0
        txtDelayTime.Tag = 0
        cbShowStartupForm.Tag = 0
        cbCheckForNewVersion.Tag = 0
        cbSplitDbEveryMonth.Tag = 0
        txtDBFile.Tag = 0
        txtDBpass.Tag = 0
        cbAccessType.Tag = 0
        txtDatabaseTables.Tag = 0
        cbRemWindowState.Tag = 0
        cbWindowState.Tag = 0
        cbFullScreenResolutions.Tag = 0
        txtWindowResolutionWidth.Tag = 0
        txtWindowResolutionHeight.Tag = 0
        txtProtectedTables.Tag = 0
        '=============================
        '==END OF STANDARD PROCEDURE==
        '=============================


        '==========
        '==CUSTOM==
        '==========

        '/===============\
        '/=END OF CUSTOM=\
        '/===============\

    End Sub

    Private Sub cmdDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDefault.Click
        Try
            If MsgBox(strLanguage_Settings(8) & vbCrLf & vbCrLf & strLanguage_Settings(9), MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then 'Are you sure you want to reset the settings?
                File.Delete(strSettingsIni)
                File.Copy(strSettingsOrig, strSettingsIni)
                Close()
                CloseForm(frmMain, strLanguage_Settings(14) & vbCrLf & vbCrLf & strLanguage_Settings(15), True) 'Would you like the program to restart now?
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub frmSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'initialization
        Try
            isLoading = True
            Call Settings_Language(Me)
            frmSkin(Me, False)

            SavedText = ""
            lstRestartNeeded.Clear()

            'Reading Settings
            Call LoadSettings()

            'Loading Languages
            cbLanguage.Items.Clear()
            cbLanguage.Items.Add(strLanguage_Settings(21))

            strDirectories = Directory.GetDirectories(strLanguageFolders)
            For i As Integer = 0 To strDirectories.Length - 1
                LanguageName = strDirectories(i).Substring(strLanguageFolders.Length)
                cbLanguage.Items.Add(LanguageName)
            Next

            'Loading Skins
            cbSkin.Items.Clear()
            cbSkin.Items.Add(strLanguage_Settings(5))  'None
            Dim tmpSkinNames As New TextBox With {.Lines = Directory.GetDirectories(strSkin)}
            For i = 0 To tmpSkinNames.Lines.Length - 1
                If tmpSkinNames.Lines(i).Substring(strSkin.Length).ToLower <> "none" Then
                    SkinName = tmpSkinNames.Lines(i).Substring(strSkin.Length)
                    cbSkin.Items.Add(SkinName)
                End If
            Next

            'Checking if we should disable the "Automatic Update" option.
            If Not strSettings(14).Length > "014CheckForNewVersionOnStartup=".Length Then cbCheckForNewVersion.Enabled = False

            'Screen and Window Resolutions
            cbWindowState.Items.Clear()

            AllowResolutionAccessFromSettings = strSettings(33).Length > "033AllowResolutionAccessFromSettings=".Length AndAlso CBool(strSettings(33).Substring("033AllowResolutionAccessFromSettings=".Length)) = True
            Dim AllDisbaled As Boolean

            If AllowResolutionAccessFromSettings Then
                FullScreenEnabled = (strSettings(24).Length > "024FullScreen=".Length)
                FullScreenWindowedEnabled = (strSettings(31).Length > "031FullScreenWindowed=".Length)
                WindowStateEnabled = (strSettings(27).Length > "027WindowState=".Length)
                AllDisbaled = Not FullScreenEnabled AndAlso Not FullScreenWindowedEnabled AndAlso Not WindowStateEnabled

                If Not FullScreenEnabled AndAlso Not FullScreenWindowedEnabled Then cbFullScreenResolutions.Enabled = False

                RemWindowStateEnabled = (strSettings(30).Length > "030RememberWindowState=".Length)
            End If

            If Not AllowResolutionAccessFromSettings OrElse AllDisbaled Then
                cbRemWindowState.Enabled = False
                cbWindowState.Enabled = False
                cbFullScreenResolutions.Enabled = False
                txtWindowResolutionHeight.Enabled = False
                txtWindowResolutionWidth.Enabled = False

            Else
                If Not RemWindowStateEnabled Then cbRemWindowState.Enabled = False

                If FullScreenEnabled Then cbWindowState.Items.Add(strLanguage_Settings(40)) 'FullScreen
                If FullScreenWindowedEnabled Then cbWindowState.Items.Add(strLanguage_Settings(41)) 'FullScreen Windowed
                If Not FullScreenEnabled AndAlso Not FullScreenWindowedEnabled Then cbFullScreenResolutions.Enabled = False

                If WindowStateEnabled Then
                    cbWindowState.Items.Add(strLanguage_Settings(42)) 'Windowed
                    cbWindowState.Items.Add(strLanguage_Settings(43)) 'Minimized
                    cbWindowState.Items.Add(strLanguage_Settings(44)) 'Maximized
                Else
                    txtWindowResolutionWidth.Enabled = False
                    txtWindowResolutionHeight.Enabled = False
                End If
            End If

            'Load True/False to ComboBoxes
            cbStartWithWin.Items.Clear()
            cbStartWithWin.Items.Add(strLanguage_Settings(1))  'False
            cbStartWithWin.Items.Add(strLanguage_Settings(2))  'True
            cbShowStartupForm.Items.Clear()
            cbShowStartupForm.Items.Add(strLanguage_Settings(1))  'False
            cbShowStartupForm.Items.Add(strLanguage_Settings(2))  'True
            cbCheckForNewVersion.Items.Clear()
            cbCheckForNewVersion.Items.Add(strLanguage_Settings(1))  'False
            cbCheckForNewVersion.Items.Add(strLanguage_Settings(2))  'True
            cbRemWindowState.Items.Clear()
            cbRemWindowState.Items.Add(strLanguage_Settings(1))  'False
            cbRemWindowState.Items.Add(strLanguage_Settings(2))  'True
            '/Load True/False to ComboBoxes

            cbAccessType.Items.AddRange([Enum].GetNames(GetType(DBType)))

            lstWidth.Clear()
            lstHeight.Clear()
            lstResolutions = ListResolutions.DisplayResolutions.GetAvailableResolutions(lstWidth, lstHeight)
            cbFullScreenResolutions.DataSource = lstResolutions
            cbFullScreenResolutions.Text = strLanguage_Settings(3) 'Unknown

            '=============================
            '==END OF STANDARD PROCEDURE==
            '=============================


            '==========
            '==CUSTOM==
            '==========
            'Load True/False to ComboBoxes

            '/===============\
            '/=END OF CUSTOM=\
            '/===============\

            '======================
            '==STANDARD PROCEDURE==
            '======================

            Call RestoreTags()

            cmdApply.Enabled = False

            isLoading = False
        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub cmdCurrent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCurrent.Click
        Try
            'Disabling the program until the search sequence is finished
            Enabled = False
            'Disabling the program until the search sequence is finished

            isLoading = True
            LoadSettings() 'Loading the settings anew to load possible changes

            'Language
            If strSettings(6).Length > "006Language=".Length Then
                If strSettings(6).Substring("006Language=".Length).ToLower <> "default" Then   'This compare WILL remain in english because the settings file will have it in english and then
                    cbLanguage.Text = strSettings(6).Substring("006Language=".Length)          'it will be translated to any other language. We need the string constant
                Else
                    cbLanguage.Text = strLanguage_Settings(21) 'Default
                End If
            End If

            '/Language

            'Skin
            If My.Settings.strSkinChoice.ToLower <> "none" Then         'This compare WILL remain in english because the settings file will have it in english and then
                cbSkin.Text = My.Settings.strSkinChoice.Substring(0, My.Settings.strSkinChoice.Length - 1)
            Else                                                        'it will be translated to any other language. We need the string constant
                cbSkin.Text = strLanguage_Settings(5)  'None
            End If
            '/Skin

            'Start With Windows
            If strSettings(7).Length > "007StartUp=".Length Then
                If CBool(strSettings(7).Substring("007StartUp=".Length)) Then
                    cbStartWithWin.Text = strLanguage_Settings(2) 'Yes
                Else
                    cbStartWithWin.Text = strLanguage_Settings(1) 'No
                End If
            Else
                cbStartWithWin.Text = strLanguage_Settings(22) '*Error
            End If
            '/Start with Windows

            'Delay Time
            If strSettings(9).Length > "009tmrStartIntrv=".Length AndAlso IsNumeric(strSettings(9).Substring("009tmrStartIntrv=".Length)) Then
                txtDelayTime.Text = CStr(CDbl(strSettings(9).Substring("009tmrStartIntrv=".Length)) / 1000)
            Else
                txtDelayTime.Text = strLanguage_Settings(22) '*Error
            End If

            '/Delay Time

            'Show Startup Form
            If strSettings(12).Length > "012Hidden=".Length Then
                If CBool(strSettings(12).Substring("012Hidden=".Length)) = False Then
                    cbShowStartupForm.Text = strLanguage_Settings(2) 'Yes
                Else
                    cbShowStartupForm.Text = strLanguage_Settings(1) 'No
                End If
            Else
                cbShowStartupForm.Text = strLanguage_Settings(22) '*Error
            End If
            '/Show startup Form

            'Check For New Version
            'This is with an "AndAlso" cuz the no text at all is normal, meaning a disabled ComboBox
            If strSettings(14).Length > "014CheckForNewVersionOnStartup=".Length AndAlso CBool(strSettings(14).Substring("014CheckForNewVersionOnStartup=".Length)) Then
                cbCheckForNewVersion.Text = strLanguage_Settings(2) 'Yes
            Else
                cbCheckForNewVersion.Text = strLanguage_Settings(1) 'No
            End If
            '/Check For New Version

            'Auto Split Database Per Month
            If CBool(strSettings(20).Length > "020AutoSplitDatabase=".Length) Then
                If CBool(strSettings(20).Substring("020AutoSplitDatabase=".Length)) = True Then
                    cbSplitDbEveryMonth.Text = strLanguage_Settings(2) 'Yes
                Else
                    cbSplitDbEveryMonth.Text = strLanguage_Settings(1) 'No
                End If
            Else
                cbSplitDbEveryMonth.Text = strLanguage_Settings(22) '*Error
            End If
            '/Auto Split Database Per Month

            'Remember Window State
            If strSettings(30).Substring("030RememberWindowState=".Length) = "" OrElse CBool(strSettings(30).Substring("030RememberWindowState=".Length)) = False Then
                cbRemWindowState.Text = strLanguage_Settings(1) 'No
            Else
                cbRemWindowState.Text = strLanguage_Settings(2) 'Yes
            End If
            '/Remember Window State

            'Window State
            If FullScreenEnabled OrElse FullScreenWindowedEnabled OrElse WindowStateEnabled Then
                Dim IndexOffset As Integer = 0
                Dim CurFullScreen As Boolean
                Dim CurFullScreenWindowed As Boolean

                If FullScreenEnabled Then
                    CurFullScreen = CBool(strSettings(24).Substring("024FullScreen=".Length))
                    IndexOffset += 1
                End If
                If FullScreenWindowedEnabled Then
                    CurFullScreenWindowed = CBool(strSettings(31).Substring("031FullScreenWindowed=".Length))
                End If

                If CurFullScreenWindowed Then
                    cbWindowState.Text = cbWindowState.Items(0 + IndexOffset).ToString
                ElseIf CurFullScreen Then
                    cbWindowState.Text = cbWindowState.Items(0).ToString
                End If

                If WindowStateEnabled AndAlso Not (CurFullScreen OrElse CurFullScreenWindowed) Then
                    IndexOffset = 0
                    If FullScreenEnabled Then IndexOffset += 1
                    If FullScreenWindowedEnabled Then IndexOffset += 1

                    If intWindowState = 0 Then
                        cbWindowState.Text = cbWindowState.Items(0 + IndexOffset).ToString
                    ElseIf intWindowState = 1 Then
                        cbWindowState.Text = cbWindowState.Items(1 + IndexOffset).ToString
                    ElseIf intWindowState = 2 Then
                        cbWindowState.Text = cbWindowState.Items(2 + IndexOffset).ToString
                    End If
                End If

            Else
                cbWindowState.Text = frmMain.WindowState.ToString
            End If
            '/Window State

            'FullScreen Resolution
            If FullScreenWidth <> 0 AndAlso FullScreenHeight <> 0 Then
                cbFullScreenResolutions.Text = FullScreenWidth & "x" & FullScreenHeight
            Else
                cbFullScreenResolutions.Text = My.Computer.Screen.Bounds.Width & "x" & My.Computer.Screen.Bounds.Height
            End If
            '/FullScreen Resolution

            'Window Resolution
            Dim WindowResolutionWidth As String = strSettings(28).Substring("028WindowWidth=".Length)
            If IsNumeric(WindowResolutionWidth) Then txtWindowResolutionWidth.Text = WindowResolutionWidth Else txtWindowResolutionWidth.Text = CStr(OriginalWindowWidth)

            Dim WindowResolutionHeight As String = strSettings(29).Substring("029WindowHeight=".Length)
            If IsNumeric(WindowResolutionHeight) Then txtWindowResolutionHeight.Text = WindowResolutionHeight Else txtWindowResolutionHeight.Text = CStr(OriginalWindowHeight)
            '/Window Resolution

            'DataBase File
            If strSettings(18).Length > "018DataBaseDir=".Length Then
                If strSettings(18).Substring("018DataBaseDir=".Length).ToLower = "default" Then
                    txtDBFile.Text = strDefDBFile
                Else
                    Dim ProperFileName As String = doProperPathName(doResolveWildNames(strSettings(18).Substring("018DataBaseDir=".Length)))
                    ProperFileName = ProperFileName.Substring(0, ProperFileName.Length - 1)
                    txtDBFile.Text = ProperFileName
                End If
            End If
            '/DataBase File

            'Database Pass
            If strSettings(17).Length > "017DataBasePass=".Length Then
                txtDBpass.Text = strSettings(17).Substring("017DataBasePass=".Length)
            Else
                txtDBpass.Text = ""
            End If
            '/Database Pass

            'Access Type
            If strSettings(15).Length > "015DBType=".Length Then
                cbAccessType.Text = [Enum].GetName(GetType(DBType), CInt(strSettings(15).Substring("015DBType=".Length)))
            Else
                cbAccessType.Text = strLanguage_Settings(22) '*Error
            End If
            '/Access Type

            'DataBase Tables
            If strSettings(19).Length > "019DataBaseTables=".Length Then
                txtDatabaseTables.Text = strSettings(19).Substring("019DataBaseTables=".Length)
            Else
                txtDatabaseTables.Text = String.Empty
            End If
            '/DataBase Tables

            If strSettings(22).Length > "022DBProtectedTables=".Length Then
                txtProtectedTables.Text = strSettings(22).Substring("022DBProtectedTables=".Length)
            Else
                txtDatabaseTables.Text = String.Empty
            End If

            '=============================
            '==END OF STANDARD PROCEDURE==
            '=============================

            '======================
            '==STANDARD PROCEDURE==
            '======================

            Call RestoreTags()

            isLoading = False

            'Re-Enabling the program when search sequence is finished
            Enabled = True
            cmdApply.Enabled = False
            'Re-Enabling the program when search sequence is finished

        Catch ex As Exception
            Enabled = True
            CreateCrashFile(ex, True, , MsgBoxStyle.Critical)
#If Not DEBUG Then
            Me.Close()
#End If
        End Try
    End Sub

    Private Sub cmdApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApply.Click
        Try
            'Disabling the program until the search sequence is finished
            Enabled = False
            'Disabling the program until the search sequence is finished

            If cbLanguage.Tag.ToString = "1" AndAlso cbLanguage.SelectedIndex <> -1 Then
                If cbLanguageLastIndex <> 0 Then
                    strSettings(6) = "006Language=" & cbLanguage.Items(cbLanguageLastIndex).ToString
                    CurrentLanguage = cbLanguage.Items(cbLanguageLastIndex).ToString
                Else
                    strSettings(6) = "006Language=Default"
                    If Directory.Exists(strLanguageFolders & Thread.CurrentThread.CurrentCulture.Name) Then
                        CurrentLanguage = Thread.CurrentThread.CurrentCulture.Name
                    ElseIf Directory.Exists(strLanguageFolders & "en-GB") Then
                        CurrentLanguage = "en-GB"
                    Else
                        Dim LanguageFolders() As String = Directory.GetDirectories(strLanguageFolders)
                        If LanguageFolders.Length > 0 Then CurrentLanguage = GetFolderNameAlone(LanguageFolders(0))
                    End If
                End If

                'the following need to be re-set for the language has changed and they are language-dependent
                Call ChangeVariablesContainingCurrentLanguageVar(CurrentLanguage)

                Call ReadFile(strModuleLanguageFile, strModLanguage)
                Tip_LangString = strModLanguage(121) 'Tip:
                LangDelimiterForRangeOrSpan = strModLanguage(44) 'to

                Call Main_Language(frmMain)
            End If
            '/Language

            'Skin
            If cbSkin.Tag.ToString = "1" AndAlso cbSkin.SelectedIndex <> -1 Then
                If cbSkin.Text.ToLower <> cbSkin.Items(0).ToString.ToLower Then
                    My.Settings.strSkinChoice = cbSkin.Items(cbSkin.SelectedIndex).ToString & "\"
                    For i As Integer = 0 To My.Application.OpenForms.Count - 1
                        Call frmSkin(My.Application.OpenForms(i))
                    Next
                    Call UpdateTexts(frmMain)
                Else
                    My.Settings.strSkinChoice = "None"
                    For i As Integer = 0 To My.Application.OpenForms.Count - 1
                        Call frmUnSkin(My.Application.OpenForms(i))
                    Next
                    Call UpdateTexts(frmMain)
                End If

                My.Settings.Save()
                UpdateTexts(frmMain)
            End If
            '/Skin

            'Start with Windows
            If cbStartWithWin.Tag.ToString = "1" AndAlso cbStartWithWin.SelectedIndex <> -1 Then
                If cbStartWithWin.Text = cbStartWithWin.Items(1).ToString Then
                    strSettings(7) = "007StartUp=True"
                    'Dim a = Shell("cmd.exe /c reg add HKLM\Software\Microsoft\Windows\CurrentVersion\Run /v KT-PoS-GM /t REG_SZ /d """ & My.Application.Info.DirectoryPath & "\" & "StartUp" & ".exe"" /f", 0)
                    Registry.SetValue(strAutoRunRegistryKeyPath, My.Application.Info.Title, strStartupExe)

                ElseIf cbStartWithWin.Text = cbStartWithWin.Items(0).ToString Then
                    strSettings(7) = "007StartUp=False"
                    Registry.LocalMachine.OpenSubKey(strAutoRunRegistrySubKeyPath, True).DeleteValue(My.Application.Info.Title, False)
                End If
            End If
            '/Start with Windows

            'Delay on Auto-Running With Windows
            If txtDelayTime.Tag.ToString = "1" Then
                If IsNumeric(txtDelayTime.Text) = True Then
                    strSettings(9) = "009tmrStartIntrv=" & CInt(CDbl(txtDelayTime.Text) * 1000)
                End If
            End If
            '/Delday on Auto-Running With Windows

            'Show Startup Form
            If cbShowStartupForm.Tag.ToString = "1" AndAlso cbShowStartupForm.SelectedIndex <> -1 Then
                If cbShowStartupForm.SelectedIndex = 1 Then
                    strSettings(12) = "012Hidden=False"
                ElseIf cbShowStartupForm.SelectedIndex = 0 Then
                    strSettings(12) = "012Hidden=True"
                Else
                    MsgBox(strLanguage_Settings(23), MsgBoxStyle.Information, "Error Code: SETFRM00002") 'An error occured during cmdApply_Click
                End If
            End If
            '/Show startup Form

            'Check For New Version on Startup
            If cbCheckForNewVersion.Tag.ToString = "1" AndAlso cbCheckForNewVersion.SelectedIndex <> -1 Then
                If cbCheckForNewVersion.SelectedIndex = 1 Then
                    strSettings(14) = "014CheckForNewVersionOnStartup=True"
                    CheckForNewVersionOnStartup = True
                ElseIf cbCheckForNewVersion.SelectedIndex = 0 Then
                    strSettings(14) = "014CheckForNewVersionOnStartup=False"
                    CheckForNewVersionOnStartup = False
                Else
                    MsgBox(strLanguage_Settings(23), MsgBoxStyle.Information, "Error Code: SETFRM00003") 'An error occured during cmdApply_Click
                End If
            End If
            '/Check For New Version on Startup

            'Remember Window State
            If cbRemWindowState.Tag.ToString = "1" AndAlso cbRemWindowState.SelectedIndex <> -1 Then
                If cbRemWindowState.SelectedIndex = 1 Then
                    strSettings(30) = "030RememberWindowState=True"
                    RememberWindowState = True
                ElseIf cbRemWindowState.SelectedIndex = 0 Then
                    strSettings(30) = "030RememberWindowState=False"
                Else
                    MsgBox(strLanguage_Settings(23), MsgBoxStyle.Information, "Error Code: SETFRM00004") 'An error occured during cmdApply_Click
                End If
            End If
            '/Remember Window State

            'Window State
            If cbWindowState.Tag.ToString = "1" AndAlso cbWindowState.SelectedIndex <> -1 Then
                If cbWindowState.SelectedIndex = 0 Then
                    If FullScreenEnabled Then
                        strSettings(24) = "024FullScreen=True"
                        If FullScreenWindowedEnabled Then strSettings(31) = "031FullScreenWindowed=False"

                    ElseIf FullScreenWindowedEnabled Then
                        strSettings(31) = "031FullScreenWindowed=True"

                    Else
                        strSettings(27) = "027WindowState=" & cbWindowState.SelectedIndex
                    End If

                ElseIf cbWindowState.SelectedIndex = 1 Then
                    If FullScreenEnabled AndAlso FullScreenWindowedEnabled Then
                        strSettings(24) = "024FullScreen=True"
                        strSettings(31) = "031FullScreenWindowed=True"

                    ElseIf FullScreenEnabled AndAlso Not FullScreenWindowedEnabled Then
                        strSettings(24) = "024FullScreen=False"
                        strSettings(27) = "027WindowState=" & (cbWindowState.SelectedIndex - 1)

                    ElseIf Not FullScreenEnabled AndAlso FullScreenWindowedEnabled Then
                        strSettings(31) = "031FullScreenWindowed=False"
                        strSettings(27) = "027WindowState=" & (cbWindowState.SelectedIndex - 1)

                    ElseIf Not FullScreenEnabled AndAlso Not FullScreenWindowedEnabled Then
                        strSettings(27) = "027WindowState=" & cbWindowState.SelectedIndex
                    End If

                ElseIf cbWindowState.SelectedIndex = 2 Then
                    If FullScreenEnabled AndAlso FullScreenWindowedEnabled Then
                        strSettings(24) = "024FullScreen=False"
                        strSettings(31) = "031FullScreenWindowed=False"
                        strSettings(27) = "027WindowState=" & (cbWindowState.SelectedIndex - 2)

                    ElseIf FullScreenEnabled AndAlso Not FullScreenWindowedEnabled Then
                        strSettings(24) = "024FullScreen=False"
                        strSettings(27) = "027WindowState=" & (cbWindowState.SelectedIndex - 1)

                    ElseIf Not FullScreenEnabled AndAlso FullScreenWindowedEnabled Then
                        strSettings(31) = "031FullScreenWindowed=False"
                        strSettings(27) = "027WindowState=" & (cbWindowState.SelectedIndex - 1)

                    ElseIf Not FullScreenEnabled AndAlso Not FullScreenWindowedEnabled Then
                        strSettings(27) = "027WindowState=" & cbWindowState.SelectedIndex
                    End If

                ElseIf cbWindowState.SelectedIndex = 3 Then
                    If FullScreenEnabled AndAlso FullScreenWindowedEnabled Then
                        strSettings(24) = "024FullScreen=False"
                        strSettings(31) = "031FullScreenWindowed=False"
                        strSettings(27) = "027WindowState=" & (cbWindowState.SelectedIndex - 2)

                    ElseIf FullScreenEnabled AndAlso Not FullScreenWindowedEnabled Then
                        strSettings(24) = "024FullScreen=False"
                        strSettings(27) = "027WindowState=" & (cbWindowState.SelectedIndex - 1)

                    ElseIf Not FullScreenEnabled AndAlso FullScreenWindowedEnabled Then
                        strSettings(31) = "031FullScreenWindowed=False"
                        strSettings(27) = "027WindowState=" & (cbWindowState.SelectedIndex - 1)
                    End If

                Else
                    strSettings(24) = "024FullScreen=False"
                    strSettings(31) = "031FullScreenWindowed=False"
                    strSettings(27) = "027WindowState=" & (cbWindowState.SelectedIndex - 2)
                End If

                lstRestartNeeded.Add(lblWindowState.Text.Replace(":", ""))
            End If
            '/Window State

            'FullScreen Resolution
            If cbFullScreenResolutions.Tag.ToString = "1" AndAlso cbFullScreenResolutions.SelectedIndex <> -1 Then
                Dim Resolution() As String = cbFullScreenResolutions.Items(cbFullScreenResolutions.SelectedIndex).ToString.ToLower.Split("x"c)
                strSettings(25) = "025FullScreenWidth=" & Resolution(0)
                strSettings(26) = "026FullScreenHeight=" & Resolution(1)
                lstRestartNeeded.Add(lblFullScreenResolutions.Text.Replace(":", ""))
            End If
            '/FullScreen Resolution

            'Window Resolution
            If txtWindowResolutionWidth.Tag.ToString = "1" OrElse txtWindowResolutionHeight.Tag.ToString = "1" Then
                strSettings(28) = "028WindowWidth=" & txtWindowResolutionWidth.Text
                strSettings(29) = "029WindowHeight=" & txtWindowResolutionHeight.Text
                lstRestartNeeded.Add(lblWindowResolution.Text.Replace(":", ""))
            End If
            '/Window Resolution

            '=============
            'tab: Database
            '=============

            'Split Database Per Month
            If cbSplitDbEveryMonth.Tag.ToString = "1" AndAlso cbSplitDbEveryMonth.SelectedIndex <> -1 AndAlso cbSplitDbEveryMonth.Enabled = True Then
                If cbSplitDbEveryMonth.SelectedIndex = 1 Then
                    strSettings(20) = "020AutoSplitDatabase=True"
                    AccessAutoSplitDatabase = True
                ElseIf cbSplitDbEveryMonth.SelectedIndex = 0 Then
                    strSettings(20) = "020AutoSplitDatabase=False"
                    AccessAutoSplitDatabase = False
                Else
                    MsgBox(strLanguage_Settings(23), MsgBoxStyle.Information, "Error Code: SETFRM00005") 'An error occured during cmdApply_Click
                End If
            End If
            '/Split Database Per Month

            'DataBase File
            If txtDBFile.Tag.ToString = "1" AndAlso txtDBFile.Text <> String.Empty AndAlso txtDBFile.Enabled = True Then
                Dim ProperFileName As String = doProperPathName(doResolveWildNames(txtDBFile.Text))
                ProperFileName = ProperFileName.Substring(0, ProperFileName.Length - 1)
                If File.Exists(ProperFileName) Then
                    If ProperFileName.ToLower = strDefDBFile.ToLower Then
                        strSettings(18) = "018DataBaseDir=Default"
                        AccessDataBaseFile = strDefDBFile
                    Else
                        strSettings(18) = "018DataBaseDir=" & doUnresolveWildNames(ProperFileName)
                        AccessDataBaseFile = ProperFileName
                    End If
                Else
                    MsgBox(strLanguage_Settings(37) & ProperFileName & strLanguage_Settings(38), MsgBoxStyle.Exclamation) 'Database Path Doesn't Exist
                End If
            End If
            '/DataBase Path

            'Database Pass
            If txtDBpass.Tag.ToString = "1" Then    'Password CAN be "" so not "AndAlso <> String.Empty"
                strSettings(17) = "017DataBasePass=" & txtDBpass.Text
                AccessDatabasePass = txtDBpass.Text
            End If
            '/Database Pass

            'DataBase Type
            If cbAccessType.Tag.ToString = "1" AndAlso cbAccessType.SelectedIndex <> -1 Then
                strSettings(15) = "015DBType=" & CInt(CType([Enum].Parse(GetType(DBType), cbAccessType.Items(cbAccessType.SelectedIndex).ToString), DBType))
                CurDBType = cbAccessType.SelectedIndex
            End If
            '/DataBase Type

            'Database Tables
            If txtDatabaseTables.Tag.ToString = "1" Then
                strSettings(19) = "019DataBaseTables=" & txtDatabaseTables.Text
            End If
            '/Database Tables

            'Protected Tables
            If txtProtectedTables.Tag.ToString = "1" Then
                strSettings(22) = "022DBProtectedTables=" & txtProtectedTables.Text
            End If
            '/Protected Tables


            '=============================
            '==END OF STANDARD PROCEDURE==
            '=============================

            '======================
            '==STANDARD PROCEDURE==
            '======================

            '<Ending>
            Call WriteSettings(strSettings, "Form: frmSettings -> ")

            Call RestoreTexts()
            Call RestoreTags()

            'Re-Enabling the program when search sequence is finished
            Enabled = True
            cmdApply.Enabled = False
            Close()
            'Re-Enabling the program when search sequence is finished

            If lstRestartNeeded.Count > 0 AndAlso (MsgBox(strLanguage_Settings(49) & vbCrLf & vbCrLf & strLanguage_Settings(50) & vbCrLf & ArrayBox(True, lstRestartNeeded) &
                vbCrLf & vbCrLf & strLanguage_Settings(51), MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes) Then 'Some of the settings you changed needs the program to restart.
                RestartApplication()
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
            Answer = MsgBox(strLanguage_Settings(11), MsgBoxStyle.YesNo)
            If Answer = vbYes Then
                DelFileFolder(strSettingsIni)
                File.Copy(strSettingsOrig, strSettingsOrig)
            End If
            Close()
        End Try
    End Sub

#Region "Standard: SelectedIndex/Text Changed"
    Private Sub cbLanguage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbLanguage.SelectedIndexChanged
        If Not isLoading Then
            Try
                cmdApply.Enabled = True
                cbLanguage.Tag = "1"
                cbLanguageLastIndex = cbLanguage.SelectedIndex

            Catch ex As Exception
                CreateCrashFile(ex, True)
            End Try
        End If
    End Sub

    Private Sub cbSkin_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbSkin.SelectedIndexChanged
        If Not isLoading Then
            cmdApply.Enabled = True
            cbSkin.Tag = "1"
        End If
    End Sub

    Private Sub btnSkinAdvanced_Click(sender As System.Object, e As System.EventArgs) Handles btnSkinAdvanced.Click
        ShowForm(frmSkinCreator)
    End Sub

    Private Sub cbStartWithWin_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbStartWithWin.SelectedIndexChanged
        If Not isLoading Then
            cmdApply.Enabled = True
            cbStartWithWin.Tag = "1"
        End If
    End Sub

    Private Sub txtDelayTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDelayTime.Click
        txtDelayTime.Text = ""
    End Sub

    Private Sub txtDelayTime_LostFocus(sender As Object, e As System.EventArgs) Handles txtDelayTime.LostFocus
        If txtDelayTime.Text = "" Then
            txtDelayTime.Text = strLanguage_Settings(3) 'Unknown
        End If
    End Sub

    Private Sub txtDelayTime_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDelayTime.TextChanged
        If Not isLoading Then

            If txtDelayTime.Text <> "" Then
                If IsNumeric(txtDelayTime.Text) = True Then
                    cmdApply.Enabled = True
                    txtDelayTime.Tag = "1"
                    If txtDelayTime.Text <> "" Then
                        SavedText = txtDelayTime.Text
                    End If

                ElseIf txtDelayTime.Text <> strLanguage_Settings(3) Then 'Unknown
                    txtDelayTime.Text = SavedText
                    txtDelayTime.SelectionStart = txtDelayTime.Text.Length
                End If


            Else
                txtDelayTime.Tag = 0
            End If

        End If
    End Sub

    Private Sub cbShowStartupForm_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbShowStartupForm.SelectedIndexChanged
        If Not isLoading Then
            cmdApply.Enabled = True
            cbShowStartupForm.Tag = "1"
        End If
    End Sub

    Private Sub cbCheckForNewVersion_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbCheckForNewVersion.SelectedIndexChanged
        If Not isLoading Then
            cmdApply.Enabled = True
            cbCheckForNewVersion.Tag = "1"
        End If
    End Sub

    Private Sub btnBrowseDBPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseDBPath.Click
        ofdFileBrowser.FileName = Nothing
        ofdFileBrowser.InitialDirectory = strDatabaseDir
        ofdFileBrowser.DefaultExt = ".accdb"
        ofdFileBrowser.Filter = "Access 2007-2010|*.accdb|Access 2000-2003|*.mdb"
        ofdFileBrowser.ShowDialog()
        If ofdFileBrowser.FileName IsNot Nothing Then
            txtDBFile.Text = ofdFileBrowser.FileName
        End If

    End Sub

    Private Sub txtDBFile_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDBFile.TextChanged
        If Not isLoading Then
            cmdApply.Enabled = True
            txtDBFile.Tag = "1"
        End If
    End Sub

    Private Sub txtDBpass_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDBpass.TextChanged
        If Not isLoading AndAlso txtDBpass.Enabled = True Then
            cmdApply.Enabled = True
            txtDBpass.Tag = "1"
        End If
    End Sub

    Private Sub cbAccessType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbAccessType.SelectedIndexChanged
        If cbAccessType.SelectedIndex <> -1 Then

            If Not isLoading Then
                cmdApply.Enabled = True
                cbAccessType.Tag = "1"
            End If

            Dim SelectedDBType As DBType = CType([Enum].Parse(GetType(DBType), cbAccessType.Items(cbAccessType.SelectedIndex).ToString), DBType)
            If SelectedDBType = DBType.Access97to2003 OrElse SelectedDBType = DBType.Access2007to2016 Then
                txtDBFile.Enabled = True
                cbSplitDbEveryMonth.Enabled = True
                btnBrowseDBPath.Enabled = True

                txtDBpass.Enabled = True
                txtDatabaseTables.Enabled = True
                txtProtectedTables.Enabled = True
                btnDatabaseTables.Enabled = True
                btnProtectedTables.Enabled = True

            ElseIf SelectedDBType = DBType.SQLServer Then
                txtDBFile.Enabled = False
                cbSplitDbEveryMonth.Enabled = False
                btnBrowseDBPath.Enabled = False

                txtDBpass.Enabled = True
                txtDatabaseTables.Enabled = True
                txtProtectedTables.Enabled = True
                btnDatabaseTables.Enabled = True
                btnProtectedTables.Enabled = True

            ElseIf SelectedDBType = DBType.None Then
                txtDBFile.Enabled = False
                cbSplitDbEveryMonth.Enabled = False

                txtDBpass.Enabled = False
                txtDatabaseTables.Enabled = False
                txtProtectedTables.Enabled = False
                btnBrowseDBPath.Enabled = False
                btnDatabaseTables.Enabled = False
                btnProtectedTables.Enabled = False
            End If

        End If
    End Sub

    Private Sub cbSplitDbEveryMonth_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbSplitDbEveryMonth.SelectedIndexChanged
        If Not isLoading Then
            cmdApply.Enabled = True
            cbSplitDbEveryMonth.Tag = "1"
        End If
    End Sub

    Private Sub btnDatabaseTables_Click(sender As System.Object, e As System.EventArgs) Handles btnDatabaseTables.Click
        Dim Tables() As String = {}
        Dim CurrentTables() As String = Nothing

        If txtDatabaseTables.Text <> String.Empty Then CurrentTables = ReadList(txtDatabaseTables.Text, True, True).ToArray

        If TypeBox(strLanguage_Settings(36), Tables, True, , False, True, , , True, CurrentTables) = True Then
            txtDatabaseTables.Text = CreateArrayString(Tables)
        End If
    End Sub

    Private Sub txtDatabaseTables_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDatabaseTables.TextChanged
        If Not isLoading AndAlso txtDatabaseTables.Enabled = True Then
            cmdApply.Enabled = True
            txtDatabaseTables.Tag = "1"
        End If
    End Sub

    Private Sub cbWindowState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbWindowState.SelectedIndexChanged
        If Not isLoading Then
            cmdApply.Enabled = True
            cbWindowState.Tag = "1"
        End If
    End Sub

    Private Sub cbFullScreenResolutions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFullScreenResolutions.SelectedIndexChanged
        If Not isLoading Then
            cmdApply.Enabled = True
            cbFullScreenResolutions.Tag = "1"
        End If
    End Sub

    Private Sub txtWindowResolutionWidth_Enter_Click(sender As Object, e As EventArgs) Handles txtWindowResolutionWidth.Enter, txtWindowResolutionWidth.Click
        If Not isLoading Then
            Dim CurWidth As Integer
            Dim Succeeded As Boolean = TypeBox(strLanguage_Settings(47), CurWidth, False, , 640, My.Computer.Screen.Bounds.Width)
            If Succeeded Then txtWindowResolutionWidth.Text = CStr(CurWidth)
        End If
    End Sub

    Private Sub txtWindowResolutionWidth_TextChanged(sender As Object, e As EventArgs) Handles txtWindowResolutionWidth.TextChanged
        If Not isLoading Then
            cmdApply.Enabled = True
            txtWindowResolutionWidth.Tag = "1"
        End If
    End Sub

    Private Sub txtWindowResolutionHeight_Enter_Click(sender As Object, e As EventArgs) Handles txtWindowResolutionHeight.Enter, txtWindowResolutionHeight.Click
        If Not isLoading Then
            Dim Curheight As Integer
            Dim Succeeded As Boolean = TypeBox(strLanguage_Settings(48), Curheight, False, , 480, My.Computer.Screen.Bounds.Height)
            If Succeeded Then txtWindowResolutionHeight.Text = CStr(Curheight)
        End If
    End Sub

    Private Sub txtWindowResolutionHeight_TextChanged(sender As Object, e As EventArgs) Handles txtWindowResolutionHeight.TextChanged
        If Not isLoading Then
            cmdApply.Enabled = True
            txtWindowResolutionWidth.Tag = "1"
        End If
    End Sub

    Private Sub cbRemWindowState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbRemWindowState.SelectedIndexChanged
        If Not isLoading Then
            cmdApply.Enabled = True
            cbRemWindowState.Tag = "1"
        End If
    End Sub

    Private Sub btnProtectedTables_Click(sender As Object, e As EventArgs) Handles btnProtectedTables.Click
        Dim ProtectedTables() As String = {}
        Dim CurrentProtectedTables() As String = Nothing

        If txtDatabaseTables.Text <> String.Empty Then CurrentProtectedTables = ReadList(txtProtectedTables.Text, True, True).ToArray

        If TypeBox(strLanguage_Settings(54), ProtectedTables, True, , False, True, , , True, CurrentProtectedTables) = True Then 'Type in the Databases Names you wish to protect one on each line without delimitation.
            txtProtectedTables.Text = CreateArrayString(ProtectedTables)
        End If
    End Sub

    Private Sub txtProtectedTables_TextChanged(sender As Object, e As EventArgs) Handles txtProtectedTables.TextChanged
        If Not isLoading AndAlso txtProtectedTables.Enabled = True Then
            cmdApply.Enabled = True
            txtProtectedTables.Tag = "1"
        End If
    End Sub

#End Region

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Close()
        Exit Sub
    End Sub

    '=============================
    '==END OF STANDARD PROCEDURE==
    '=============================



End Class