﻿'Version 2.5 2016/08/30
'Fixed a bug that Expiration Date was needed on registration code even though the registration code doesn't expire.
'Possibly fixed the infinite recursion problem 'Made database Upgrade-Ready; Changed Original Databased to RARed (to avoid deletion on upgrade)
'Needs Settings.ini version 1.6; Language file Version 2.0

'API Key: AIzaSyB1TNGKJ4jTOKWR9lsaJy8R9o1CBgYWCMc

'https://maps.googleapis.com/maps/api/geocode/json?&address=ΠΕΡΙΞ%20ΣΕΡΡΩΝ
'https://maps.googleapis.com/maps/api/geocode/json?&address=ΑΝΩ%20ΜΗΛΙΑ&key=AIzaSyB4qeHfUHoimexLFOfS05EdVcVCMJLK2h4

Option Strict On
Imports RDotNet
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Threading
Imports Microsoft.Win32
Imports Newtonsoft.Json.Linq
Imports System.Text

Public Class frmMain

#Region "Constants And Variables"
    Public Args() As String = Environment.GetCommandLineArgs
    Public isProgramStartingUp As Boolean = True

    Public strLanguage_Main() As String
    Public strLanguage_Main_Tips() As String

    Public CommandsDefaultText As String

    Dim FormStatefrmMain As New FormState
    Dim LastWindowState As FormWindowState = WindowState 'Used on Resize Event
    Dim PreviousWindowState As FormWindowState = WindowState 'Used on "Show/Hide" (Tray Menu)

    'Command-Line Arguments
    Dim PreventBETAwarningMessage As Boolean

    'Auxiliary
    Dim intTimerUnknownCmd As Integer = 0
    Dim IsCurLoadingSettings As Boolean = False 'Used in the FileSystemWatcher Settings to check for external settings manipulation
    Dim AlreadyHidedSoon As Boolean = False
    '=============================
    '==END OF STANDARD VARIABLES==
    '=============================

    'Colouring
    Dim GeoLocateRect As New Rectangle(0, 0, 0, 0)

    Private blbtnGeoLocateContinue As Boolean = True
    Private ErrorsOnUpdateCount As Integer = 0
    Private pbGeneralValue As Integer = 0
    Private CityOrigName As New List(Of String)
    Private CityCorrespName As New List(Of String)

    '===============================
    '==BEGIN OF STANDARD PROCEDURE==
    '===============================

#End Region

    '=============================
    '==END OF STANDARD PROCEDURE==
    '=============================

#Region "Security Settings"
    'Security Settings of {Programme}:
    'ProgramGUID = "{GUID}"
    'LicenseDelimiter = ";"c
    'AllUpgradesEdition = -1
    'MainFolderOnline = ""
    'IniChars = ""
    'MidChars = ""
    'EndChars = ""
    'HasExpirationDate = Boolean
    'HasEditionNum = Boolean
    'MailSettingsCipherLevel = Integer
    'UserNameLengthToHex = Integer
    'EditionNumMultiplicator = Integer
    'EngineNumber = Integer
    'EditionsNames = {}
    'MinUserNameLength = Integer
    'MaxUserNameLength = -1
#End Region

    '===============================
    '==BEGIN OF STANDARD PROCEDURE==
    '===============================

    '=== Regarding Settings.ini ===
    'If there 's NO "=" in the old settings, assume the new line
    'Else
    '   If the New line starts With "{U}" Then assume the New line
    '   If the New line's schema is the same is the old, assume the old line
    '   Else, assume the New line
    Private Async Sub frmMain_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        isProgramStartingUp = True

        Try
            'initialization
            Text &= " - Version: " & My.Application.Info.Version.ToString

            OriginalWindowWidth = Width
            OriginalWindowHeight = Height

            Try
                If Not Directory.Exists(strDocumentsProgDir) Then
                    Directory.CreateDirectory(strDocumentsProgDir)
                End If
            Catch ex As Exception
            End Try

            If Args.Length > 1 Then
                For i = 1 To Args.Length - 1
                    Select Case Args(i).ToLower
                        Case "ni", "nointernet", "no internet"
                            isInternetAvailable = False
                            PreventInternetCheck = True

                        Case "nobetawarning", "nobeta", "nb"
                            PreventBETAwarningMessage = True

                        Case "-visible=false"
                            Visible = False
                            Opacity = 0
                            ShowInTaskbar = False
                            '=============================
                            '==END OF STANDARD PROCEDURE==
                            '=============================

                            '===============================
                            '==BEGIN OF STANDARD PROCEDURE==
                            '===============================

                    End Select
                Next
            End If

            If BETA AndAlso Not PreventBETAwarningMessage Then
                Dim fontName As FontFamily = lblHelp.Font.FontFamily
                lblHelp.Text = "WARNING: This version is still in BETA!!"
                lblHelp.BackColor = Color.Black
                lblHelp.ForeColor = Color.Red
                lblHelp.Font = New Font(fontName, 12, FontStyle.Bold)
                lblHelp.Visible = True
            End If

            Call ReadMainStrings()

            Call Main_Language(Me)

            Dim VariableEquations As List(Of String) = ReadList(strSettings(34).Substring("034Variables=".Length), True, True)
            Dim FunctionEquations As List(Of String) = ReadList(strSettings(35).Substring("035Functions=".Length), True, True)
            AddVariables(VariableEquations.ToArray, False)
            AddFunctions(FunctionEquations.ToArray, False)

            lblCompany.Text = My.Application.Info.Trademark

            If strSettings(23).Length > "023SettingsLevel=".Length Then SettingsLevel = CInt(strSettings(23).Substring("023SettingsLevel=".Length))

            If strSettings(14).Length > "014CheckForNewVersionOnStartup=".Length Then CheckForNewVersionOnStartup = CBool(strSettings(14).Substring("014CheckForNewVersionOnStartup=".Length))

            CurDBType = CInt(strSettings(15).Substring("015DBType=".Length))

            Dim tmpDBName As String = strSettings(16).Substring("016DatabaseName=".Length)
            If tmpDBName <> "" AndAlso tmpDBName.ToLower <> "default" Then
                DatabaseName = tmpDBName
            End If

            AccessDatabasePass = strSettings(17).Substring("017DataBasePass=".Length)

            If strSettings(18).Substring("018DataBaseDir=".Length).ToLower = "default" Then
                AccessDataBaseFile = strDefDBFile
            Else
                Dim ProperFileName As String = doProperPathName(doResolveWildNames(strSettings(18).Substring("018DataBaseDir=".Length)))
                ProperFileName = ProperFileName.Substring(0, ProperFileName.Length - 1)
                AccessDataBaseFile = ProperFileName
            End If

            DatabaseTables = ReadList(strSettings(19).Substring("019DataBaseTables=".Length), False, True)
            ProtectedTables = ReadList(strSettings(22).Substring("022DBProtectedTables=".Length), False, True)

            AccessAutoSplitDatabase = CBool(strSettings(20).Substring("020AutoSplitDatabase=".Length))

            If strSettings(38).Substring("038CompactDatabaseOnStartup=".Length).Length > 0 Then AccessCompactDBOnStartup = CBool(strSettings(38).Substring("038CompactDatabaseOnStartup=".Length))

            AccessLastDatabaseSplit = strSettings(21).Substring("021LastDatabaseSplit=".Length)

            If strSettings(30).Length > "030RememberWindowState=".Length Then RememberWindowState = CBool(strSettings(30).Substring("030RememberWindowState=".Length))

            If strSettings(24).Length > "024FullScreen=".Length Then FullScreen = CBool(strSettings(24).Substring("024FullScreen=".Length))

            If strSettings(31).Length > "031FullScreenWindowed=".Length Then FullScreenWindowed = CBool(strSettings(31).Substring("031FullScreenWindowed=".Length))

            If strSettings(27).Length > "027WindowState=".Length Then intWindowState = CInt(strSettings(27).Substring("027WindowState=".Length)) Else intWindowState = -1

            Dim tmpSQLConnStr As String = strSettings(40).Substring("040SQLServerConnStr=".Length)
            If tmpSQLConnStr = "" OrElse tmpSQLConnStr.ToLower = "default" Then SQLServerConnStr = "" Else SQLServerConnStr = tmpSQLConnStr

            Dim fWidth As String = strSettings(25).Substring("025FullScreenWidth=".Length)
            If fWidth = String.Empty OrElse fWidth.ToLower = "default" Then
                FullScreenWidth = My.Computer.Screen.Bounds.Width
            ElseIf IsNumeric(fWidth) Then
                FullScreenWidth = CInt(fWidth)
            Else
                FullScreenWidth = Width
            End If

            Dim fHeight As String = strSettings(26).Substring("026FullScreenHeight=".Length)
            If fHeight = String.Empty OrElse fHeight.ToLower = "default" Then
                FullScreenHeight = My.Computer.Screen.Bounds.Height
            ElseIf IsNumeric(fHeight) Then
                FullScreenHeight = CInt(fHeight)
            Else
                FullScreenHeight = Height
            End If

            Dim wWidth As String = strSettings(28).Substring("028WindowWidth=".Length)
            If IsNumeric(wWidth) AndAlso CInt(wWidth) <> 0 Then
                WindowWidth = CInt(wWidth)
            ElseIf wWidth = "form" Then 'AndAlso Not IsNumeric
                WindowWidth = OriginalWindowWidth
            Else
                WindowWidth = My.Computer.Screen.Bounds.Width
            End If

            Dim wHeight As String = strSettings(29).Substring("029WindowHeight=".Length)
            If IsNumeric(wHeight) AndAlso CInt(wHeight) <> 0 Then
                WindowHeight = CInt(wHeight)
            ElseIf wHeight = "default" Then 'AndAlso Not IsNumeric
                WindowHeight = OriginalWindowHeight
            Else
                WindowWidth = My.Computer.Screen.Bounds.Height
            End If


            Try 'If there's no such key (i.e. no Install-Shield installation but plain big instead), it's okay
                MainFolderOnline = Registry.GetValue(strProgramRegistryKeyName, strMainFolderOverrideValueName, MainFolderOnline).ToString
            Catch ex As Exception
            End Try

            Try
                strOnlineMainInfo = DlFile.DownloadString(MainFolderOnline & "MainInfo.txt").Split(New String() {Environment.NewLine}, StringSplitOptions.None)
                If Not CBool(strOnlineMainInfo(0).Substring("ProgramRun=".Length)) AndAlso BETA = False Then 'Check Run Parameter
                    MsgBox(strModLanguage(20)) 'Run=False|The Program's framework is currently down. Please try again in 5 minutes.
                    Application.Exit()
                    Exit Sub
                End If
            Catch exc As Exception
                'If there is a problem with that, then it's okay, lets just continue
            End Try

            CommandsDefaultText = strLanguage_Main(26)  'You may type commands here. For a list of available commands, please type "help()"
            txtCommands.Text = CommandsDefaultText

            '/initialization

            '==================
            '=Loading Settings=
            '==================
            Try
                '=============================
                '==END OF STANDARD PROCEDURE==
                '=============================

                GeoLocAPIKey = strSettings(46).Substring("046APIKey=".Length)
                GeoLocationAPILink = strSettings(47).Substring("047GeoLocationAPILink=".Length)
                ErrorMessageIdentifierInJSON = strSettings(48).Substring("048ErrorMessageIdentifierInJSON=".Length)
                APIExceededQuotaError = strSettings(49).Substring("049APIExceededQuotaError=".Length)
                CityFieldSuffix = strSettings(50).Substring("050CityFieldSuffix=".Length)

                If File.Exists(strExtras & "Cities.csv") Then
                    Dim tmpText() As String = File.ReadAllLines(strExtras & "Cities.csv")
                    For Each line As String In tmpText
                        Dim LineAr() As String = line.Split("	"c)
                        If LineAr.Length > 1 Then
                            CityOrigName.Add(LineAr(0).ToLower)
                            CityCorrespName.Add(LineAr(1).ToLower)
                        End If
                    Next
                End If

                RowsPerRead = CInt(strSettings(51).Substring("051RowsPerRead=".Length))
                Dim tmpStrXDF As String = strSettings(52).Substring("052PathtoSaveLoadXDFFiles=".Length)
                strXDF = If(tmpStrXDF <> "" AndAlso tmpStrXDF.ToLower <> "default" AndAlso Directory.Exists(doProperPathName(tmpStrXDF)), doProperPathNameLinux(tmpStrXDF), doProperPathNameLinux(strDesktop))
                RoundAt = CInt(strSettings(53).Substring("053RoundAt=".Length))
                RSQLConnStr = strSettings(64).Substring("064RSQLConnStr=".Length) 'If it's not filled in, then it's auto-configured in line 499

                TablevErga = strSettings(54).Substring("054TablevErga=".Length)
                ColvCityName = strSettings(55).Substring("055ColvCityName=".Length)
                ColvGeoLocX = strSettings(56).Substring("056ColvGeoLocX=".Length)
                ColvGeoLocY = strSettings(57).Substring("057ColvGeoLocY=".Length)
                ColvID_Erga = strSettings(58).Substring("058ColvID_Erga=".Length)
                TableErga = strSettings(59).Substring("059TableErga=".Length)
                ColCityName = strSettings(60).Substring("060ColCityName=".Length)
                ColGeoLocX = strSettings(61).Substring("061ColGeoLocX=".Length)
                ColGeoLocY = strSettings(62).Substring("062ColGeoLocY=".Length)
                ColID_Erga = strSettings(63).Substring("063ColID_Erga=".Length)
                TablevFinalDataset = strSettings(66).Substring("066TablevFinalDataset=".Length)

                '============================
                '==BEGIN STANDARD PROCEDURE==
                '============================

            Catch ex As Exception
                If SettingsLevel = 0 Then
                    Dim RestoreDefaultSettings As MsgBoxResult = MsgBox(strLanguage_Main(45), MsgBoxStyle.YesNoCancel) 'There is a problem with the settings file; would you like to restore the original settings?
                    If RestoreDefaultSettings = MsgBoxResult.Yes Then
                        Try
                            File.Delete(strSettingsIni)
                            File.Copy(strSettingsOrig, strSettingsIni)
                            LoadSettings()
                            strSettings(23) = "023SettingsLevel=" & SettingsLevel + 2
                            WriteSettings(strSettings, "SettingsLevel+2;0")
                            RestartApplication()

                        Catch exc As Exception
                            CreateCrashFile(exc, True)
                        End Try

                    Else
                        LoadSettings()
                        strSettings(23) = "023SettingsLevel=" & SettingsLevel + 1
                        WriteSettings(strSettings, "SettingsLevel+1;0")
                    End If

                ElseIf SettingsLevel = 1 Then
                    File.Delete(strSettingsIni)
                    File.Copy(strSettingsOrig, strSettingsIni)
                    LoadSettings()
                    strSettings(23) = "023SettingsLevel=" & SettingsLevel + 1
                    WriteSettings(strSettings, "SettingsLevel+1;0")
                    RestartApplication()

                Else
                    MsgBox(strLanguage_Main(46) & My.Settings.ContactEmail & vbCrLf & vbCrLf & strLanguage_Main(47), MsgBoxStyle.Information) 'It seems like the settings problem is persistent; Please contact for support via email at
                    If SettingsLevel = 2 Then CreateCrashFile(ex, False)
                End If

            End Try

            '==================
            '==FIRST TIME RUN==
            '==================
            If isFirstTimeRun() Then
                Dim FirstTimeForm As New frmFirstTime With {
                    .TopMost = True
                }
                FirstTimeForm.ShowDialog()
                If FirstTimeForm.LicenseAccepted = True Then
                    Dim AppDataSettings() As String = {""}
                    Try
                        ReadFile(strAppDataSettingsFile, AppDataSettings)
                    Catch ex As Exception
                    End Try
                    AppDataSettings(0) = "FirstTime=False"
                    WriteText(strAppDataSettingsFile, AppDataSettings, System.Text.Encoding.Unicode)
                Else
                    Exit Sub
                End If
            End If

            '===========================
            '==Database Initialisation==
            '===========================

            If CurDBType = DBType.Access2007to2016 OrElse CurDBType = DBType.Access97to2003 Then
                If File.Exists(AccessDataBaseFile) AndAlso File.Exists(AccessDataBaseFileRarred) Then
                    Dim DBFileName As String = GetFileName(AccessDataBaseFile)
                    Dim DBFileNameAlone As String = GetFileNameAlone(AccessDataBaseFile)
                    Dim DBFileNameExt As String = GetExt(AccessDataBaseFile)

                    Dim BackupDBFile As String = GetSubStr(AccessDataBaseFile, AccessDataBaseFile.Length - DBFileName.Length) & DBFileNameAlone & " Upgrade " & Today.Year & " " & Today.Month & " " & Today.Day & DBFileNameExt
                    RenFileFolder(AccessDataBaseFile, BackupDBFile)
                End If

                If File.Exists(AccessDataBaseFileRarred) Then
                    Unrar(AccessDataBaseFileRarred, strDatabaseDir, , True)
                    DelFileFolder(AccessDataBaseFileRarred, False)
                End If

                'Compacting // Splitting the Database
                Try
                    If File.Exists(AccessDataBaseFile) Then
                        Call LoadAccessDBConnStr(CurDBType, AccessDataBaseFile, AccessDatabasePass, AccessDBSource, AccessDBProvider)
                        If AccessCompactDBOnStartup AndAlso CompactAccessDB(AccessDataBaseFile, AccessDBProvider, CurDBType, AccessDatabasePass, True) = False Then
                            MsgBox(strLanguage_Main(41), MsgBoxStyle.Exclamation) 'The Database file cannot be accessed correctly.
                        End If

                        If AccessLastDatabaseSplit = "" Then
                            strSettings(21) = "021LastDatabaseSplit=" & String.Format("{0:D2}{1:D4}", Today.Month, Today.Year)
                            Call WriteSettings(strSettings, "Main Load: AccessLastDatabaseSplit was ''")

                        Else
                            Dim LastSplitMonth As Integer = CInt(GetSubStr(AccessLastDatabaseSplit, 2, "Left"))
                            Dim LastSplitYear As Integer = CInt(GetSubStr(AccessLastDatabaseSplit, 4, "Right"))
                            Dim NextMonthArrived As Boolean = Today.Year > LastSplitYear OrElse (Today.Month > LastSplitMonth AndAlso Today.Year = LastSplitYear)
                            If AccessAutoSplitDatabase AndAlso NextMonthArrived Then
                                If SplitAccessDatabase(AccessDataBaseFile, AccessDBProvider, CurDBType, AccessDatabasePass) Then
                                    strSettings(21) = "021LastDatabaseSplit=" & String.Format("{0:D2}{1:D4}", Today.Month, Today.Year)
                                    Call WriteSettings(strSettings, "Main Load: Database Split Successful")
                                Else
                                    MsgBox(strLanguage_Main(40), MsgBoxStyle.Information) 'An error occurred while trying to Split this month's database
                                End If
                            End If
                        End If

                    End If
                Catch ex As Exception
                    CreateCrashFile(ex, True)
                End Try

            ElseIf CurDBType = DBType.SQLServer Then
                ConnectedToSQLServer = ConnectToSQLServer(True)
            End If

            If CheckForNewVersionOnStartup Then Await doUpdateDependingOnProgramEdition(Me)

            If FullScreenWindowed Then
                ChangeResolution(FullScreenWidth, FullScreenHeight, True)
                FormStatefrmMain.Maximize(Me)

            ElseIf FullScreen Then
                ChangeResolution(FullScreenWidth, FullScreenHeight, True)
                FormStatefrmMain.Maximize(Me)

            Else
                If intWindowState <> -1 Then
                    MaximizeBox = True
                    FormBorderStyle = FormBorderStyle.Sizable

                    If intWindowState = 0 Then
                        WindowState = FormWindowState.Normal
                        Visible = False
                        Top = 0
                        Left = 0

                        If WindowWidth <> 0 AndAlso WindowHeight <> 0 Then
                            Width = WindowWidth
                            Height = WindowHeight
                        Else
                            Width = OriginalWindowWidth
                            Height = OriginalWindowHeight
                        End If

                    ElseIf intWindowState = 1 Then
                        PreviousWindowState = WindowState
                        tmrMinimizationDelay.Enabled = True

                    ElseIf intWindowState = 2 Then
                        WindowState = FormWindowState.Maximized
                    End If

                Else
                    intWindowState = 0
                End If
            End If

            Call frmSkin(Me, False, pnlMain)

            Call UpdateTexts(Me)

            'Tray Settings MUST always be below Skin Load
            TrayIcon.BalloonTipText = My.Application.Info.Title
            TrayIcon.BalloonTipTitle = My.Application.Info.Title
            TrayIcon.Icon = Icon
            TrayIcon.Text = My.Application.Info.Title
            TrayIcon.Visible = True

            '=============================
            '==END OF STANDARD PROCEDURE==
            '=============================

            Try
                SQLServerUserID = strSettings(65).Substring("065DBUsername=".Length)
                SQLServerPass = strSettings(17).Substring("017DataBasePass=".Length)

                If ConnectedToSQLServer Then
                    If RSQLConnStr = "" OrElse RSQLConnStr.ToLower = "default".ToLower Then
                        RSQLConnStr = GetRSQLConStr(ServerName, DatabaseName,, SQLServerUserID, SQLServerPass)
                    End If
                End If

                If RDotNet_Initialization() Then
                    Dim IsRServerOrEquivalent As Boolean = Rdo.Evaluate("('RevoScaleR' %in% loadedNamespaces())").AsLogical.First

                    If IsRServerOrEquivalent Then
                        RSource(strFunctions & "[Initialisation].R",, {"{0}", RowsPerRead.ToString,
                                                                       "{1}", """" & strXDF & """",
                                                                       "{2}", RSQLConnStr,
                                                                       "{3}", """" & doProperPathNameLinux(strGraphs) & """"}, False)
                    Else
                        MsgBox(sa(strLanguage_Main(100), vbCrLf)) 'Unfortunately, although some version of R has been found in your system, you need R_Server or equivalent for this programme to run{0}This requirement is imposed because the code hereinafter requires the RevoScaleR R Package to bypass R's limitations in computer Memory (RAM) and CPU.{0}{0}Please install R Server and update your Registry and Environment to it
                    End If
                End If

            Catch ex As Exception
                Notify(ex.ToString, Color.Red, Color.Black, 10)
                CreateCrashFile(ex)
            End Try

            Try
                If File.Exists(strGraphs & "Models.log") Then
                    Dim tmpStr() As String = File.ReadAllLines(strGraphs & "Models.log")
                    If tmpStr.Length > 10000 Then
                        tmpStr = (From line In tmpStr Select line Skip tmpStr.Length - 1000).ToArray
                        WriteText(strGraphs & "Models.log", tmpStr, Encoding.UTF8)
                    End If
                End If
            Catch ex As Exception
            End Try

            tmrFunctInProgress.Enabled = True

            '============================
            '==BEGIN STANDARD PROCEDURE==
            '============================

            pbGeneralProgress.Visible = False

            Enabled = True

        Catch ex As Exception
            File.Delete(strSettingsIni)
            File.Copy(strSettingsBakup, strSettingsIni)
            CreateCrashFile(ex, True, , , , ArrayBox(strSettings))
        End Try

        fswSettings.Path = strSettingsPath
        isProgramStartingUp = False
    End Sub

    <DebuggerStepThrough()>
    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If FullScreen OrElse FullScreenWindowed Then
            Visible = False
            Call FormStatefrmMain.Restore(Me)
            Call RestoreResolution()

        ElseIf Rdo IsNot Nothing Then
            Rdo.Evaluate("graphics.off() Then")
        End If

        '=============================
        '==END OF STANDARD PROCEDURE==
        '=============================

        '============================
        '==BEGIN STANDARD PROCEDURE==
        '============================

        TrayIcon.Visible = False
    End Sub

    'Note that Form.KeyPreview must be set to true for this event handler to be called.
    Sub frmMain_KeyPress(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        '=============================
        '==END OF STANDARD PROCEDURE==
        '=============================

        'If e.Control = True AndAlso e.KeyCode = Keys.U Then
        '    e.Handled = True
        '    ShowForm(frmUpdateGames)
        'End If

        '============================
        '==BEGIN STANDARD PROCEDURE==
        '============================
    End Sub

    '======================================================================================================================
    '== !CAUTION! Some of the Commands of txtCommands used in "btnEnter" are UNIQUE on every APP and MUSTN'T be deleted! ==
    '======================================================================================================================

#Region "Standard Stuff"

    Private Async Sub btnEnter_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdGo.Click
        Dim DoNotEraseTextInTheEnd As Boolean
        tmrUnknownCmd.Enabled = False
        intTimerUnknownCmd = 0
        lblUnknownCmd.Visible = False
        tmrUnknownCmd.Interval = 1
        If txtCommands.Text IsNot Nothing AndAlso txtCommands.Text <> "" Then My.Computer.Clipboard.SetText(txtCommands.Text)

        Select Case txtCommands.Text.ToLower

            Case CommandsDefaultText.ToLower, ""
                txtCommands.Text = CommandsDefaultText
                DoNotEraseTextInTheEnd = True

                '==============
                '==Menu Items==
                '==============

            Case "settings", "settings()", "open settings", "open settings()", "stg", "stgs", "stg()", "stgs()"
                ShowForm(frmSettings)

            Case "Exit", "quit", "close", "altqq", "Exit()", "quit()", "close()", "altqq()"
                CloseForm(Me, strLanguage_Main(11)) 'Are you sure you want to exit?

            Case "databasemaintenance", "dbmaint", "dbm", "db mainrenance", "database maintenance", "databasemaintenance()", "dbmaint()", "dbm()", "db mainrenance()", "database maintenance()"
                ShowForm(frmDatabaseMaintenance)

            Case "suggest", "complain", "suggestorcomplain", "suggest()", "complain()", "suggestorcomplain()"
                ShowForm(frmSuggestionAndComplaint)

            Case "settingsini", "settingsini()", "open settings ini", "stgini", "stgsini", "open settings ini()", "stgini()", "stgsini()"
                RunOpenDir(strSettingsIni)

            Case "program documents", "prog documents", "programme documents", "program documents()", "prog documents()", "programme documents()"
                RunOpenDir(strDocumentsProgDir)

            Case "path", "dir", "program", "open path", "open dir", "program path", "program dir", "programme path", "programme dir", "prog path", "prog dir", "path()", "dir()", "program()", "open path()", "open dir()", "program path()", "program dir()", "programme path()", "programme dir()", "prog path", "prog dir()"
                RunOpenDir("explorer.exe", strRoot)

            Case "dbpath", "db path", "database", "database path", "db dir", "database", "database dir", "dbpath()", "db path()", "database()", "database path()", "db dir()", "database()", "database dir()"
                If strSettings(18).Substring("018DataBaseDir=".Length).ToLower = "Default" Then
                    RunOpenDir("explorer.exe", strDatabaseDir)
                Else
                    RunOpenDir("explorer.exe", strSettings(18).Substring("018DataBaseDir=".Length))
                End If

            Case "extras", "extrasdir", "extraspath", "extras()", "extrasdir()", "extraspath()"
                RunOpenDir(strExtras)

            Case "language", "languagedir", "languagepath", "language()", "languagedir()", "languagepath()"
                RunOpenDir(strLanguageFolders)

            Case "settingsdir", "stgdir", "stgsdir", "open settings dir", "settingsdir()", "open settings dir()", "stgdir()", "stgsdir()"
                RunOpenDir(strSettingsPath)

            Case "skin", "change skin", "open skin", "open change skin", "skin()", "change skin()", "open skin()", "open change skin()"
                ShowForm(frmSkinCreator)

            Case "website", "site"
                With My.Settings
                    If .ProgrammeWebsite <> "" Then
                        RunOpenDir(strExplorerExe, .ProgrammeWebsite)
                    ElseIf .ProgrammersWebsite <> "" Then
                        RunOpenDir(strExplorerExe, .ProgrammersWebsite)
                    ElseIf .CompanyWebsite <> "" Then
                        RunOpenDir(strExplorerExe, .CompanyWebsite)
                    End If
                End With

            Case "progwebsite", "progsite", "programwebsite", "programsite", "progweb site", "prog site", "program website", "program site", "progwebsite()", "progsite()", "programwebsite()", "programsite()", "prog website()", "prog site()", "programweb site()", "program site()"
                RunOpenDir(strExplorerExe, My.Settings.ProgrammeWebsite)

            Case "programmerwebsite", "programmersite", "programmer website", "programmer site", "programmerwebsite()", "programmersite()", "programmer website()", "programmer site()"
                RunOpenDir(strExplorerExe, My.Settings.ProgrammersWebsite)

            Case "companywebsite", "companysite", "company website", "company site", "companywebsite()", "companysite()", "company website()", "company site()"
                RunOpenDir(strExplorerExe, My.Settings.ProgrammersWebsite)

            Case "pres", "presentation", "show presentation", "pres()", "presentation()", "show presentation()"
                ShowForm(frmPresentation)

            Case "wlcm", "welcome", "show welcome screen", "wlcme()", "welcome()", "show welcome screen()"
                ShowForm(frmFirstTime)

            Case "eula", "license agreement", "show eula", "show license agreement", "eula()", "license agreement()", "show eula()", "show license agreement()"
                RunOpenDir(strEULA)

            Case "changelog", "changelog()"
                RunOpenDir(strChangeLog)

            Case "help", "help()"
                ShowForm(frmHelp)

            Case "update", "check For updates", "update()", "check For updates()"
                Dim UpdatesAreAvailable As Boolean = Await doUpdateDependingOnProgramEdition(Me)
                If Not UpdatesAreAvailable Then MsgBox(strModLanguage(17), MsgBoxStyle.Information) 'There are no updates available for download.

            Case "credits"
                RunOpenDir(strCredits)

            Case "about", "info", "info()", "about()", "information", "information()"
                ShowForm(frmAbout)

                '===============
                '==Other Items==
                '===============

            Case "skindir", "skinpath", "open skin dir", "skindir()", "open skin path", "open skin dir()"
                RunOpenDir(strSettingsPath)

            Case "fuck you", "fuckyou", "fuck u", "fucku"
                Close()

                '===============
                '==Math Parser==
                '===============

            Case "variables", "list variables", "show variables"
                If lstVariablesNames.Count > 0 Then
                    MsgBox(ArrayBox("=", lstVariablesNames, lstVariablesValues))
                Else
                    MsgBox(strLanguage_Main(50), MsgBoxStyle.Exclamation) 'There are no available variables
                End If

            Case "functions", "list functions", "show functions"
                If lstVariablesNames.Count > 0 Then
                    MsgBox(ArrayBox("=", lstFunctionsNames, lstFunctionsValues))
                Else
                    MsgBox(strLanguage_Main(68), MsgBoxStyle.Exclamation) 'There are no available functions
                End If

            Case "equation mode On", "calculation mode On", "calc mode On"
                EquationModeOn = True
                AddVariableModeOn = False
                AddFunctionModeOn = False
                lblMathMode.Text = strLanguage_Main(63) & strLanguage_Main(64)
                lblMathMode.Visible = True
                MsgBox(strLanguage_Main(53) & vbCrLf & strLanguage_Main(62), MsgBoxStyle.Information) 'Equation Mode has been turned On, therefore you may use variables and no prefix is necessary.

            Case "equation mode off", "calculation mode off", "calc mode off"
                If EquationModeOn Then
                    EquationModeOn = False
                    lblMathMode.Visible = False
                    MsgBox(strLanguage_Main(56), MsgBoxStyle.Exclamation) 'Equation Mode has been turned Off, therefore a prefix is necessary for use of variables.
                Else
                    MsgBox(strLanguage_Main(67), MsgBoxStyle.Exclamation) 'The specified Mode was not on
                End If

            Case "add variable mode On", "variable mode On"
                AddVariableModeOn = True
                EquationModeOn = False
                AddFunctionModeOn = False
                lblMathMode.Visible = True
                lblMathMode.Text = strLanguage_Main(63) & strLanguage_Main(65)
                MsgBox(strLanguage_Main(54) & vbCrLf & strLanguage_Main(62), MsgBoxStyle.Information) 'Add Variable mode has been turned On, no prefix is necessary

            Case "add variable mode off", "variable mode off"
                If AddVariableModeOn = True Then
                    AddVariableModeOn = False
                    lblMathMode.Visible = False
                    MsgBox(strLanguage_Main(57), MsgBoxStyle.Exclamation) 'Add Variable mode has been turned Off, a prefix is necessary
                Else
                    MsgBox(strLanguage_Main(67), MsgBoxStyle.Exclamation) 'The specified Mode was not on
                End If

            Case "add Function mode On", "Function mode On"
                AddFunctionModeOn = True
                EquationModeOn = False
                AddVariableModeOn = False
                lblMathMode.Visible = True
                lblMathMode.Text = strLanguage_Main(63) & strLanguage_Main(66)
                MsgBox(strLanguage_Main(55) & vbCrLf & strLanguage_Main(62), MsgBoxStyle.Information) 'Add Function mode has been turned On, no prefix is necessary

            Case "add Function mode off", "Function mode off"
                If AddFunctionModeOn Then
                    AddFunctionModeOn = False
                    lblMathMode.Visible = False
                    MsgBox(strLanguage_Main(58), MsgBoxStyle.Exclamation) 'Add Function mode has been turned Off, a prefix is necessary
                Else
                    MsgBox(strLanguage_Main(67), MsgBoxStyle.Exclamation) 'The specified Mode was not on
                End If

                '=============================
                '===END OF STANDARD FUNCTION==
                '=============================

                '======================
                '===STANDARD FUNCTION==
                '======================

            Case Else
                Try
                    If IsNumeric(txtCommands.Text.ToLower.Replace("(", "").Replace(")", "").Replace("+", "").Replace("-", "").Replace("/", "").Replace("*", "").Replace("^", "").Replace("%", "").Replace("log", "").Replace("logn", "").Replace("abs", "").Replace("sin", "").Replace("cos", "").Replace("tan", "").Replace("pi", "").Replace("e", "")) Then
                        Dim Equation As String = txtCommands.Text.Replace(" ", "")
                        Call ShowEquationOnLabel(Equation)
                        txtCommands.Text = SimplifyValue(Equation).ToString
                        txtCommands.SelectionStart = txtCommands.Text.Length
                        DoNotEraseTextInTheEnd = True

                    ElseIf Not txtCommands.Text.Contains("=") AndAlso (EquationModeOn OrElse (txtCommands.Text.ToLower.StartsWith("e ") AndAlso Not txtCommands.Text.ToLower.Replace(" ", "").StartsWith("e=")) OrElse txtCommands.Text.ToLower.StartsWith("evaluate ") OrElse (txtCommands.Text.ToLower.StartsWith("c ") AndAlso Not txtCommands.Text.ToLower.Replace(" ", "").StartsWith("c=")) OrElse txtCommands.Text.ToLower.StartsWith("calculate ")) Then
                        Dim StarterWord As String = String.Empty
                        If txtCommands.Text.ToLower.StartsWith("e ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "e ".Length)
                        ElseIf txtCommands.Text.ToLower.StartsWith("evaluate") Then
                            StarterWord = GetSubStr(txtCommands.Text, "evaluate ".Length)
                        ElseIf txtCommands.Text.ToLower.StartsWith("calculate ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "calculate ".Length)
                        ElseIf txtCommands.Text.ToLower.StartsWith("c ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "c ".Length)
                        End If

                        Dim Equation As String = txtCommands.Text.Substring(StarterWord.Length).Replace(" ", "")

                        Call ShowEquationOnLabel(Equation)
                        txtCommands.Text = SimplifyValue(Equation).ToString
                        txtCommands.SelectionStart = txtCommands.Text.Length
                        DoNotEraseTextInTheEnd = True

                    ElseIf txtCommands.Text.ToLower = "remove variables" OrElse txtCommands.Text.ToLower = "remove all variables" Then
                        Call RemoveAllMathVariables(True)
                        MsgBox(strLanguage_Main(59), MsgBoxStyle.Information) 'All variables have been erased

                    ElseIf txtCommands.Text.ToLower = "remove functions" OrElse txtCommands.Text.ToLower = "remove all functions" Then
                        RemoveAllFunctions(True)
                        MsgBox(strLanguage_Main(73), MsgBoxStyle.Information) 'All functions have been erased

                    ElseIf (txtCommands.Text.ToLower.StartsWith("r v ") AndAlso Not txtCommands.Text.ToLower.Replace(" ", "").StartsWith("rv=")) OrElse txtCommands.Text.ToLower.StartsWith("remove variable ") Then
                        Dim StarterWord As String = String.Empty
                        If txtCommands.Text.ToLower.StartsWith("r v ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "r v ".Length)
                        ElseIf txtCommands.Text.ToLower.StartsWith("remove variable ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "remove variable  ".Length)
                        End If

                        Dim VariableName As String = txtCommands.Text.Substring(StarterWord.Length).Replace(" ", "")
                        Dim VariableIndexOnList As Integer = FindIndex(VariableName, lstVariablesNames, False)

                        If VariableIndexOnList <> -1 Then
                            Call RemoveVariable(lstVariablesNames.Item(VariableIndexOnList), VariableIndexOnList)
                            MsgBox(strLanguage_Main(60), MsgBoxStyle.Information) 'The specified variable has been deleted

                        Else
                            MsgBox(strLanguage_Main(61), MsgBoxStyle.Exclamation) 'The specified variable wasn't found!
                        End If

                    ElseIf (txtCommands.Text.ToLower.StartsWith("r f ") AndAlso Not txtCommands.Text.ToLower.Replace(" ", "").StartsWith("rf=")) OrElse txtCommands.Text.ToLower.StartsWith("remove Function ") Then
                        Dim StarterWord As String = String.Empty
                        If txtCommands.Text.ToLower.StartsWith("r f ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "r f ".Length)
                        ElseIf txtCommands.Text.ToLower.StartsWith("remove Function ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "remove Function ".Length)
                        End If

                        Dim FunctionName As String = txtCommands.Text.Substring(StarterWord.Length).Replace(" ", "").Replace("(", "{").Replace(")", "}")
                        If Not FunctionName.ToLower.StartsWith("func") Then FunctionName = "func" & FunctionName
                        Dim FunctionIndexOnList As Integer = FindIndex(FunctionName, lstFunctionsNames, False)

                        If FunctionIndexOnList <> -1 Then
                            Call RemoveFunction(GetSubStr(lstFunctionsNames.Item(FunctionIndexOnList), lstFunctionsNames.Item(FunctionIndexOnList).IndexOf("{")).Substring("func".Length), FunctionIndexOnList, True)
                            MsgBox(strLanguage_Main(71), MsgBoxStyle.Information) 'The specified function has been deleted

                        Else
                            MsgBox(strLanguage_Main(72), MsgBoxStyle.Exclamation) 'The specified function wasn't found!
                        End If

                    ElseIf AddVariableModeOn OrElse txtCommands.Text.ToLower.StartsWith("a v ") OrElse txtCommands.Text.ToLower.StartsWith("add v ") OrElse txtCommands.Text.ToLower.StartsWith("add variable ") Then
                        Dim StarterWord As String = String.Empty
                        If txtCommands.Text.ToLower.StartsWith("a v ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "a v ".Length)
                        ElseIf txtCommands.Text.ToLower.StartsWith("add v ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "add v ".Length)
                        ElseIf txtCommands.Text.ToLower.StartsWith("add variable ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "add variable ".Length)
                        End If

                        Dim Equation As String = txtCommands.Text.Substring(StarterWord.Length).Replace(" ", "")
                        Dim IndexOfEqualSign As Integer = Count("="c, Equation)

                        If IndexOfEqualSign = 1 Then
                            Dim VariableNameAndValue() As String = Equation.Split("="c)

                            If Not isContained(VariableNameAndValue(0), lstFunctionsNames, False, True) Then
                                If Not isContained(VariableNameAndValue(0), lstVariablesNames, False, True) Then
                                    AddVariable(VariableNameAndValue(0), VariableNameAndValue(1))
                                    Call ShowEquationOnLabel(Equation)
                                    MsgBox(strLanguage_Main(52), MsgBoxStyle.Information) 'The variable has been successfully added.

                                Else
                                    MsgBox(strLanguage_Main(51), MsgBoxStyle.Exclamation) 'A variable with this name already exists!
                                End If

                            Else
                                MsgBox(strLanguage_Main(69), MsgBoxStyle.Exclamation) 'A Function with this name already exists!
                            End If

                        Else
                            tmrUnknownCmd.Enabled = True
                            txtCommands.Focus()
                        End If

                    ElseIf AddFunctionModeOn OrElse txtCommands.Text.ToLower.StartsWith("a f ") OrElse txtCommands.Text.ToLower.StartsWith("add f ") OrElse txtCommands.Text.ToLower.StartsWith("add Function ") Then
                        Dim StarterWord As String = String.Empty
                        If txtCommands.Text.ToLower.StartsWith("a f ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "a f ".Length)
                        ElseIf txtCommands.Text.ToLower.StartsWith("add f ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "add f ".Length)
                        ElseIf txtCommands.Text.ToLower.StartsWith("add Function ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "add Function ".Length)
                        End If

                        Dim FunctionToAdd As String = txtCommands.Text.Substring(StarterWord.Length).Replace(" ", "")
                        Dim IndexOfEqualSign As Integer = Count("="c, FunctionToAdd)

                        If IndexOfEqualSign = 1 Then
                            Dim FunctionNameAndValue() As String = FunctionToAdd.Split("="c)
                            FunctionNameAndValue(0) = FunctionNameAndValue(0).Replace("(", "{").Replace(")", "}")
                            FunctionNameAndValue(1) = FunctionNameAndValue(1).Replace("{", "(").Replace("}", ")").Replace("[", "(").Replace("]", ")")

                            If Not isContained(FunctionNameAndValue(0), lstFunctionsNames, False, True) Then
                                If Not isContained(FunctionNameAndValue(0), lstVariablesNames, False, True) Then
                                    If Count("{", FunctionNameAndValue(0)) = 1 AndAlso Count("}", FunctionNameAndValue(0)) = 1 AndAlso
                                        Count("(", FunctionNameAndValue(1)) = Count(")", FunctionNameAndValue(1)) Then

                                        Dim IndexOfBracket As Integer = FunctionNameAndValue(0).IndexOf("{")
                                        If IndexOfBracket <> -1 Then
                                            Dim FunctionName As String = GetSubStr(FunctionNameAndValue(0), FunctionNameAndValue(0).IndexOf("{"))
                                            Dim Vars As List(Of String) = ReadList(FunctionNameAndValue(0).Substring(FunctionNameAndValue(0).IndexOf("{")), True, True)
                                            Dim FunctionValue As String = FunctionNameAndValue(1)

                                            Call AddFunction(FunctionName, Vars, FunctionValue)
                                            Call ShowEquationOnLabel(FunctionToAdd)
                                            MsgBox(strLanguage_Main(70), MsgBoxStyle.Information) 'The Function has been successfully added.

                                        Else
                                            tmrUnknownCmd.Enabled = True
                                            txtCommands.Focus()
                                        End If

                                    Else
                                        tmrUnknownCmd.Enabled = True
                                        txtCommands.Focus()
                                    End If

                                Else
                                    MsgBox(strLanguage_Main(51), MsgBoxStyle.Exclamation) 'A variable with this name already exists!
                                End If

                            Else
                                MsgBox(strLanguage_Main(69), MsgBoxStyle.Exclamation) 'A Function with this name already exists!
                            End If

                        Else
                            tmrUnknownCmd.Enabled = True
                            txtCommands.Focus()
                        End If

                    Else
                        tmrUnknownCmd.Enabled = True
                        txtCommands.Focus()
                    End If

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

        End Select

        If Not DoNotEraseTextInTheEnd Then txtCommands.Text = String.Empty
        txtCommands.SelectionStart = txtCommands.Text.Length
    End Sub

    Private Sub ShowEquationOnLabel(ByVal Equation As String)
        lblUnknownCmd.Text = Equation
        lblUnknownCmd.ForeColor = Color.Cyan
        lblUnknownCmd.BackColor = Color.Black
        lblUnknownCmd.Visible = True
    End Sub

    Private Async Sub frmMain_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus, Me.MdiChildActivate
        Await Task.Run(
            Sub()
                Thread.Sleep(200)
            End Sub)
        If IsMdiContainer AndAlso MdiChildren.Length = 0 Then pnlMain.Visible = True
    End Sub

    <DebuggerStepThrough()>
    Private Async Sub frmMain_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        Await Task.Run(
            Sub()
                Thread.Sleep(100)
            End Sub)
        If IsMdiContainer AndAlso Not isProgramStartingUp AndAlso ActiveForm IsNot Me Then
            pnlMain.Visible = False
        End If

    End Sub

    Private Sub mnuAbout_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mniAbout.Click
        ShowForm(frmAbout)
    End Sub

    Private Sub mniDirectoryPath_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mniProgramsDir.Click
        RunOpenDir(strExplorerExe, My.Application.Info.DirectoryPath)
    End Sub

    Private Sub mnuDBdir_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mniDatabaseDir.Click
        If strSettings(18).Substring("018DataBaseDir=".Length).ToLower = "Default" Then
            RunOpenDir(strExplorerExe, strDatabaseDir)
        Else
            RunOpenDir(strExplorerExe, strSettings(18).Substring("018DataBaseDir=".Length))
        End If
    End Sub

    Private Sub mniExit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mniExit.Click
        CloseForm(Me, strLanguage_Main(11)) 'Are you sure you want to exit?
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExit.Click
        CloseForm(Me, strLanguage_Main(11)) 'Are you sure you want to exit?
    End Sub

    Private Sub mnuSettings_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mniSettings.Click
        ShowForm(frmSettings)
    End Sub

    Private Sub mniCredits_Click(sender As System.Object, e As EventArgs) Handles mniCredits.Click
        RunOpenDir(strCredits)
    End Sub

    Private Sub mniProgramWebsite_Click(sender As System.Object, e As EventArgs) Handles mniProgramWebsite.Click
        RunOpenDir(strExplorerExe, My.Settings.ProgrammeWebsite)
    End Sub

    Private Sub mniProgrammerWebsite_Click(sender As System.Object, e As EventArgs) Handles mniProgrammerWebsite.Click
        RunOpenDir(strExplorerExe, My.Settings.ProgrammersWebsite)
    End Sub

    Private Sub mniEULA_Click(sender As System.Object, e As EventArgs) Handles mniEULA.Click
        RunOpenDir(strEULA)
    End Sub

    Private Sub mniChangeLog_Click(sender As System.Object, e As EventArgs) Handles mniChangeLog.Click
        RunOpenDir(strChangeLog)
    End Sub

    Private Sub mniShowWelcomeScreen_Click(sender As System.Object, e As EventArgs) Handles mniShowWelcomeScreen.Click
        ShowForm(frmFirstTime)
    End Sub

    Private Sub mniShowPresentation_Click(sender As System.Object, e As EventArgs) Handles mniShowPresentation.Click
        ShowForm(frmPresentation)
    End Sub

    Private Sub DatabaseMaintenanceToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles mniDatabaseMaintenance.Click
        ShowForm(frmDatabaseMaintenance)
    End Sub

    Private Sub mniHelp_Click(sender As System.Object, e As EventArgs) Handles mniHelp.Click
        ShowForm(frmHelp)
    End Sub

    Private Sub tmrUnknownCmd_Tick(ByVal sender As System.Object, ByVal e As EventArgs) Handles tmrUnknownCmd.Tick

        lblUnknownCmd.Text = strLanguage_Main(31) 'Unknown Command
        tmrUnknownCmd.Interval = 300
        intTimerUnknownCmd = intTimerUnknownCmd + 1

        If intTimerUnknownCmd >= 10 Then
            tmrUnknownCmd.Enabled = False
            intTimerUnknownCmd = 0
            lblUnknownCmd.Visible = False
            tmrUnknownCmd.Interval = 1
        Else

            lblUnknownCmd.Visible = True
            If lblUnknownCmd.BackColor = Color.Black Then
                lblUnknownCmd.BackColor = Color.Red
                lblUnknownCmd.ForeColor = Color.Black
            Else
                lblUnknownCmd.BackColor = Color.Black
                lblUnknownCmd.ForeColor = Color.Red
            End If

        End If
    End Sub

    'Not finished
    Private Async Sub UnknownCmdFlash(ByVal LabelText As String, ByVal IntervalMS As Integer, ByVal MaxFlashes As Integer, ByVal FirstColour As Color, ByVal SecondColour As Color)

        Do While (True)
            lblUnknownCmd.Text = LabelText
            intTimerUnknownCmd += 1

            If intTimerUnknownCmd >= MaxFlashes Then
                intTimerUnknownCmd = 0
                lblUnknownCmd.Visible = False
                pbGeneralProgress.Visible = False
                Exit Do
            Else

                lblUnknownCmd.Visible = True
                pbGeneralProgress.Visible = True
                If lblUnknownCmd.BackColor = FirstColour Then
                    lblUnknownCmd.BackColor = SecondColour
                    lblUnknownCmd.ForeColor = FirstColour
                Else
                    lblUnknownCmd.BackColor = FirstColour
                    lblUnknownCmd.ForeColor = SecondColour
                End If

            End If

            Await Task.Run(Sub()
                               Thread.Sleep(IntervalMS)
                           End Sub)

        Loop
    End Sub

    <DebuggerStepThrough>
    Private Sub txtCommands_LostFocus(sender As Object, e As EventArgs) Handles txtCommands.LostFocus
        If txtCommands.Text = String.Empty Then txtCommands.Text = CommandsDefaultText
    End Sub

    Private Sub txtCommands_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtCommands.Click
        txtCommands.Text = ""
    End Sub

    Private Sub txtCommands_GotFocus(sender As Object, e As EventArgs) Handles txtCommands.GotFocus
        If txtCommands.Text = CommandsDefaultText Then
            txtCommands.Text = String.Empty
            txtCommands.Focus()
        End If
    End Sub

    Private Sub mniSuggestOrComplain_Click(sender As System.Object, e As EventArgs) Handles mniSuggestOrComplain.Click
        ShowForm(frmSuggestionAndComplaint)
    End Sub

    Private Sub cmdSettings_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdSettings.Click
        ShowForm(frmSettings)
    End Sub

    Private Sub cmdHelp_Click(sender As System.Object, e As EventArgs) Handles cmdHelp.Click
        ShowForm(frmHelp)
    End Sub

    Private Sub mniOpenSettingsFile_Click(sender As System.Object, e As EventArgs) Handles mniOpenSettingsFile.Click
        If MsgBox(strLanguage_Main(34) & vbCrLf & vbCrLf & strLanguage_Main(35), MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then RunOpenDir(strSettingsIni)
    End Sub

    Private Sub mniExtrasDir_Click(sender As System.Object, e As EventArgs) Handles mniExtrasDir.Click
        RunOpenDir(strExtras)
    End Sub

    Private Sub mniLanguageDir_Click(sender As System.Object, e As EventArgs) Handles mniLanguageDir.Click
        RunOpenDir(strLanguageFolders)
    End Sub

    Private Sub mniSettingsDir_Click(sender As System.Object, e As EventArgs) Handles mniSettingsDir.Click
        RunOpenDir(strSettingsPath)
    End Sub

    Private Sub mniSkinDir_Click(sender As System.Object, e As EventArgs) Handles mniSkinDir.Click
        RunOpenDir(strSkin)
    End Sub

    Private Sub mniCompanySite_Click(sender As System.Object, e As EventArgs) Handles mniCompanySite.Click
        RunOpenDir(strExplorerExe, My.Settings.CompanyWebsite)
    End Sub

    Private Sub mniProgramDocuments_Click(sender As System.Object, e As EventArgs) Handles mniProgramDocuments.Click
        RunOpenDir(strDocumentsProgDir)
    End Sub

    Private Sub mniLicenses_Click(sender As Object, e As EventArgs) Handles mniLicenses.Click
        Dim frm As New frmLicenseViewer With {
            .AlreadyAcceptedAll = True
        }
        frm.Show()
    End Sub

    Public Sub ShowMe()
        Visible = True
        Opacity = 1
        If Top < 0 Then Top = 0
        If Left < 0 Then Left = 0
        WindowState = PreviousWindowState
        ShowInTaskbar = True
        Show()
        BringToFront()
    End Sub

    Public Sub HideMe(Optional ByVal HideNoMatterWhat As Boolean = False)
        If Not AlreadyHidedSoon OrElse HideNoMatterWhat Then
            AlreadyHidedSoon = True
            Visible = False
            ShowInTaskbar = False
            tmrHideReEnable.Enabled = True
        End If
    End Sub

    Private Sub trayShow_Click(sender As Object, e As EventArgs) Handles trayShow.Click
        Call ShowMe()
    End Sub

    Private Sub trayHide_Click(sender As Object, e As EventArgs) Handles trayHide.Click
        Call HideMe()
    End Sub

    Private Sub TrayIcon_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TrayIcon.MouseDoubleClick
        If Visible = False Then
            ShowMe()
        Else
            HideMe()
        End If
    End Sub

    Private Sub tmrHideReEnable_Tick(sender As Object, e As EventArgs) Handles tmrHideReEnable.Tick

    End Sub

    Private Sub traySettings_Click(sender As Object, e As EventArgs) Handles traySettings.Click
        ShowForm(frmSettings)
    End Sub

    Private Sub trayWebsite_Click(sender As Object, e As EventArgs) Handles trayWebsite.Click
        With My.Settings
            If .ProgrammeWebsite <> "" Then
                RunOpenDir(strExplorerExe, .ProgrammeWebsite)
            ElseIf .ProgrammersWebsite <> "" Then
                RunOpenDir(strExplorerExe, .ProgrammersWebsite)
            ElseIf .CompanyWebsite <> "" Then
                RunOpenDir(strExplorerExe, .CompanyWebsite)
            End If
        End With
    End Sub

    Private Sub trayAbout_Click(sender As Object, e As EventArgs) Handles trayAbout.Click
        ShowForm(frmAbout)
    End Sub

    Private Sub trayClose_Click(sender As Object, e As EventArgs) Handles trayClose.Click
        Application.Exit()
    End Sub

    Private Sub CreditsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles trayCredits.Click
        RunOpenDir(strCredits)
    End Sub

    Private Async Sub CheckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mniCheckForUpdates.Click
        Dim UpdatesAreAvailable As Boolean = Await doUpdateDependingOnProgramEdition(Me)
        If Not UpdatesAreAvailable Then MsgBox(strModLanguage(17), MsgBoxStyle.Information) 'There are no updates available for download.
    End Sub

    Private Sub frmMain_ResizeBegin(sender As Object, e As EventArgs) Handles Me.ResizeBegin
        If Me.FormBorderStyle = FormBorderStyle.Sizable Then frmUnSkin(Me)
    End Sub

    Private Sub frmMain_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        If Me.FormBorderStyle = FormBorderStyle.Sizable Then
            frmSkin(Me, False)
            UpdateTexts(Me)

            If RememberWindowState AndAlso Not FullScreen Then
                Dim intWindowState As Integer
                If Me.WindowState = FormWindowState.Normal Then
                    intWindowState = 0
                ElseIf Me.WindowState = FormWindowState.Minimized Then
                    intWindowState = 1
                ElseIf Me.WindowState = FormWindowState.Maximized Then
                    intWindowState = 2
                End If
                strSettings(27) = String.Format("{0}{1}", "027WindowState=", intWindowState)
                strSettings(28) = String.Format("{0}{1}", "028WindowWidth=", Width)
                strSettings(29) = String.Format("{0}{1}", "029WindowHeight=", Height)
                WriteSettings(strSettings, "frmMain ResizeEnd")
            End If
        End If
    End Sub

    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If WindowState <> LastWindowState Then
            LastWindowState = WindowState
            Call UpdateTexts(Me)

            If RememberWindowState AndAlso Not FullScreen AndAlso Not FullScreenWindowed Then
                Dim intWindowState As Integer
                If Me.WindowState = FormWindowState.Normal Then
                    intWindowState = 0
                ElseIf Me.WindowState = FormWindowState.Minimized Then
                    intWindowState = 1
                ElseIf Me.WindowState = FormWindowState.Maximized Then
                    intWindowState = 2
                End If
                strSettings(27) = String.Format("{0}{1}", "027WindowState=", intWindowState)
                strSettings(28) = String.Format("{0}{1}", "028WindowWidth=", Width)
                strSettings(29) = String.Format("{0}{1}", "029WindowHeight=", Height)
                WriteSettings(strSettings, "frmMain: ResizeEnd")
            End If
        End If
    End Sub

    Private Sub tmrMinimizationDelay_Tick(sender As Object, e As EventArgs) Handles tmrMinimizationDelay.Tick
        tmrMinimizationDelay.Enabled = False
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub lblUnknownCmd_SizeChanged(sender As Object, e As EventArgs) Handles lblUnknownCmd.SizeChanged, lblUnknownCmd.VisibleChanged
        If lblUnknownCmd.Visible Then
            lblMathMode.Location = New Point(lblUnknownCmd.Location.X + lblUnknownCmd.Width + 3, lblMathMode.Location.Y)
        Else
            lblMathMode.Location = lblUnknownCmd.Location
        End If
    End Sub

    Private Sub txtCommands_TextChanged(sender As Object, e As EventArgs) Handles txtCommands.TextChanged
        If Not isProgramStartingUp AndAlso txtCommands.Text <> String.Empty Then
            If GetSubStr(txtCommands.Text.ToLower, txtCommands.Text.Length - 1, "Left") = CommandsDefaultText.ToLower Then
                txtCommands.Text = txtCommands.Text.Substring(CommandsDefaultText.Length)
                txtCommands.SelectionStart = txtCommands.Text.Length

            ElseIf (txtCommands.Text.ToLower = GetSubStr(CommandsDefaultText.ToLower, CommandsDefaultText.Length - 1, "Right") OrElse
                    txtCommands.Text.ToLower = GetSubStr(CommandsDefaultText.ToLower, CommandsDefaultText.Length - 1, "Left")) Then
                txtCommands.Text = String.Empty
            End If

        End If
    End Sub

    Private Sub fswSettings_Changed(sender As Object, e As FileSystemEventArgs) Handles fswSettings.Changed
        If Not IsCurLoadingSettings Then
            IsCurLoadingSettings = True
            If Not CurrentlyWritingSettings Then LoadSettings()
            IsCurLoadingSettings = False
        End If
    End Sub
#End Region

    '=============================
    '==END OF STANDARD PROCEDURE==
    '=============================

    Private Sub tmrUpdatePB_Tick(sender As Object, e As EventArgs) Handles tmrUpdatePB.Tick
        Try
            pbGeneralProgress.Value = pbGeneralValue
        Catch ex As Exception
        End Try
    End Sub

    <DebuggerStepThrough()>
    Private Sub frmMain_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        If GeoLocateRect.Width > 0 Then
            Dim transColour As Color = Color.FromArgb(40, 255, 0, 0)
            e.Graphics.FillRectangle(New SolidBrush(transColour), GeoLocateRect)
            e.Graphics.DrawRectangle(Pens.Red, GeoLocateRect)
        End If
    End Sub

    Private Sub mniDatabaseMaintenance_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub mniCreateGeoColumns_Click(sender As Object, e As EventArgs) Handles mniCreateGeoColumns.Click
        If FuncInProgress.Count = 0 Then
            FuncInProgress.Add(strLanguage_Main(106)) 'Creating Geo-Location Columns
            If ConnectedToSQLServer = True Then
                If SQLConn.State <> ConnectionState.Open Then SQLConn.Open()
                Dim TableExists As Boolean = SQLTableExists(SQLConn, DatabaseName, TableErga)
                If TableExists Then
                    Dim GeoLocXExists As Boolean = SQLColumnExists(SQLConn, DatabaseName, TableErga, ColvGeoLocX)
                    If Not GeoLocXExists Then
                        Dim curSQLCmd As New SqlCommand(<SQL>
                                                        USE [<%= DatabaseName %>]
                                                        ALTER TABLE [dbo].<%= TableErga %>
                                                        ADD <%= ColvGeoLocX %> INT NULL
                                                    </SQL>.Value, SQLConn)
                        curSQLCmd.ExecuteNonQuery()
                    End If

                    Dim GeoLocYExists As Boolean = SQLColumnExists(SQLConn, DatabaseName, TableErga, ColvGeoLocY)
                    If Not GeoLocYExists Then
                        Dim curSQLCmd As New SqlCommand(<SQL>
                                                        USE [<%= DatabaseName %>]
                                                        ALTER TABLE [dbo].<%= TableErga %>
                                                        ADD <%= ColvGeoLocY %> INT NULL
                                                    </SQL>.Value, SQLConn)
                        curSQLCmd.ExecuteNonQuery()
                    End If

                    If GeoLocXExists AndAlso GeoLocYExists Then
                        'The Variables {0} and {1} already existed in {2}.{3}.{4}No action was taken.
                        MsgBox(sa(strLanguage_Main(101), ColvGeoLocX, ColvGeoLocY, DatabaseName, TableErga, vbNewLine), MsgBoxStyle.Information)
                    ElseIf GeoLocXExists AndAlso Not GeoLocYExists Then
                        '{0} was successfully created; {1} already existed in {2}.{3}.
                        MsgBox(sa(strLanguage_Main(102), ColvGeoLocX, ColvGeoLocY, DatabaseName, TableErga), MsgBoxStyle.Information)
                    ElseIf GeoLocXExists AndAlso Not GeoLocYExists Then
                        '{1} was successfully created; {0} already existed in {2}.{3}.
                        MsgBox(sa(strLanguage_Main(103), ColvGeoLocX, ColvGeoLocY, DatabaseName, TableErga), MsgBoxStyle.Information)
                    Else
                        '{0} and {1} have been successfully created in {2}.{3}.
                        MsgBox(sa(strLanguage_Main(104), ColvGeoLocX, ColvGeoLocY, DatabaseName, TableErga), MsgBoxStyle.Information)
                    End If

                Else
                    'Unfortunately, the table {0} cannot be found
                    MsgBox(sa(strLanguage_Main(105), TableErga), MsgBoxStyle.Exclamation)
                End If

            Else
                'You need to be connected to the SQL Server first
                MsgBox(sa(strLanguage_Main(106)), MsgBoxStyle.Exclamation)
            End If

            FuncInProgress.Remove(strLanguage_Main(106)) 'Creating Geo-Location Columns
        Else
            'Please wait for: {0} to finish
            MsgBox(sa(strLanguage_Main(107), ArrayBox(False, ";", 0, True, FuncInProgress)), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Async Sub mniGeoLocate_Click(sender As Object, e As EventArgs) Handles mniGeoLocate.Click
        Try
            'FuncInProgress.Add("Geo-Locating")
            '                       &Geo-Locate
            If mniGeoLocate.Text = strLanguage_Main(108) AndAlso blbtnGeoLocateContinue Then
                If MsgBox(strLanguage_Main(126), MsgBoxStyle.YesNo) = vbYes Then 'The Geo-Location procedure is about to begin, proceed?
                    If ConnectedToSQLServer = True Then
                        'Creating the Red padding so that it's easy for one to spot which button is currently doing (the) work (and to push it again if one wants to stop it)
                        mniGeoLocate.Text = strLanguage_Main(109) '&Stop GeoLocating
                        btnGeoLocate.Visible = True
                        GeoLocateRect = New Rectangle(pnlMain.Location.X + gbFunctions.Location.X + pnlFunctions.Location.X + btnGeoLocate.Location.X - 8,
                                                      pnlMain.Location.Y + gbFunctions.Location.Y + pnlFunctions.Location.Y + btnGeoLocate.Location.Y - 8,
                                                      btnGeoLocate.Width + 16,
                                                      btnGeoLocate.Height + 16)
                        Me.Refresh()
                        If SQLTableExists(TablevFinalDataset) Then
                            If SQLColumnExists(TablevFinalDataset, ColvGeoLocX) AndAlso SQLColumnExists(TablevFinalDataset, ColvGeoLocY) Then
                                If GeoLocationAPILink <> "" Then
                                    If GeoLocAPIKey <> "" Then
                                        pbGeneralProgress.Style = ProgressBarStyle.Marquee
                                        pbGeneralProgress.MarqueeAnimationSpeed = 10
                                        pbGeneralProgress.Minimum = 0
                                        pbGeneralProgress.Maximum = 1
                                        pbGeneralProgress.Value = 1
                                        pbGeneralProgress.Visible = True
                                        pbGeneralProgress.BringToFront()
                                        tmrUpdatePB.Enabled = True

                                        If ErrorsOnUpdateCount < 5 Then
                                            Dim RowsToGeoLCount As Integer = 0
                                            Dim DlClinet As New WebClient

                                            Dim dtVFinalDataset As New DataTable
                                            Dim SQLAdaptrVFinalDataset As SqlDataAdapter = Nothing

                                            Dim CityName As String = String.Empty
                                            Try
                                                'Getting the IDs and city name from SQL View
                                                SQLAdaptrVFinalDataset = New SqlDataAdapter(<SQL>
                                                                       USE [<%= DatabaseName %>]
                                                                       SELECT [<%= ColvID_Erga %>], [<%= ColvCityName %>], [<%= ColvGeoLocX %>]
                                                                       FROM [dbo].[<%= TablevFinalDataset %>]
                                                                       WHERE [<%= ColvGeoLocX %>] IS NULL AND [<%= ColvCityName %>] IS NOT NULL
                                                                    </SQL>.Value, SQLConn)
                                                SQLAdaptrVFinalDataset.Fill(dtVFinalDataset)
                                                RowsToGeoLCount = dtVFinalDataset.Rows.Count
                                                'Starting the Progress Bar
                                                pbGeneralProgress.Style = ProgressBarStyle.Blocks
                                                pbGeneralProgress.Minimum = 0
                                                pbGeneralProgress.Maximum = RowsToGeoLCount
                                                pbGeneralProgress.Value = 0
                                                pbGeneralValue = 0

                                                If RowsToGeoLCount > 0 Then
                                                    Dim strJSON As String
                                                    Dim joGeoLoc As JObject
                                                    Await Task.Run(
                                                Sub()
                                                    Dim ShowErrorDialogInError As Boolean = True
                                                    For Each row As DataRow In dtVFinalDataset.Rows
                                                        If blbtnGeoLocateContinue Then
                                                            pbGeneralValue += 1

                                                            Dim LongitudeX As String = String.Empty
                                                            Dim LatitudeY As String = String.Empty
                                                            CityName = CStr(row(columnName:=ColvCityName))

                                                            Dim dtAlreadyFoundCityGeoLoc As New DataTable
                                                            Dim SQLAdptAlreadyFoundCity As New SqlDataAdapter(<SQL>
                                                                                                        USE [<%= DatabaseName %>]
                                                                                                        SELECT TOP 1 [<%= ColvGeoLocX %>], [<%= ColvGeoLocY %>]
                                                                                                        FROM [<%= DatabaseName %>].[dbo].[<%= TablevFinalDataset %>]
                                                                                                        WHERE [<%= TablevFinalDataset %>].[<%= ColvCityName %>] = N'<%= CityName.Replace("'", "''") %>' --Because some entries contain the character "'", which messes with the SQL Query.
                                                                                                      </SQL>.Value, SQLConn)
                                                            SQLAdptAlreadyFoundCity.Fill(dtAlreadyFoundCityGeoLoc)

                    If dtAlreadyFoundCityGeoLoc.Rows.Count > 0 AndAlso Not IsDBNull(dtAlreadyFoundCityGeoLoc(0)(ColGeoLocX)) AndAlso Not IsDBNull(dtAlreadyFoundCityGeoLoc(0)(ColGeoLocY)) Then
                        LongitudeX = CDbl(dtAlreadyFoundCityGeoLoc.Rows(0)(ColGeoLocX)).ToString 'To ensure that it is a number. If it is not then an error will be thrown,
                        LatitudeY = CDbl(dtAlreadyFoundCityGeoLoc.Rows(0)(ColGeoLocY)).ToString 'and the procedure will cease without saving the non-number to the DB

                    Else
                        'Initialise
                        Dim Tries As Integer = 0
                        strJSON = String.Empty
                        joGeoLoc = Nothing

                        'Download the JSON GeoLocation from the API and convert it to a JSON Object.
                        'If something goes wrong try again pausing each time
                        Dim tmpJSONObjectNames As New List(Of String)
                        Do Until Tries >= 5
                            Try
                                Dim CityNameHasChanged As Boolean = False
                                Dim tmpCityName As String = CityName.ToLower
                                For i As Integer = 0 To CityOrigName.Count - 1 'The list has to already be in lowercase letters (programmatically)!
                                    If tmpCityName = CityOrigName(i) AndAlso CityCorrespName(i) <> "" Then
                                        tmpCityName = CityCorrespName(i)
                                        CityNameHasChanged = True
                                        Exit For
                                    End If
                                Next

                                If CityNameHasChanged Then
                                    'From "Agios Nikolaos" to "Agios%20Nikolaos
                                    strJSON = DlClinet.DownloadString(GeoLocationAPILink.Replace("{Address}", tmpCityName.Replace(" ", "%20")).Replace("{APIKey}", GeoLocAPIKey))
                                Else
                                    'From "Agios Nikolaos" to "Agios%20Nikolaos,%20Ελλάδα"
                                    strJSON = DlClinet.DownloadString(GeoLocationAPILink.Replace("{Address}", (CityName & CityFieldSuffix).Replace(" ", "%20")).Replace("{APIKey}", GeoLocAPIKey))
                                End If
                                joGeoLoc = JObject.Parse(strJSON)
                                Tries = 5

                                tmpJSONObjectNames = joGeoLoc.Properties.Select(Function(x) x.Name).ToList
                                If tmpJSONObjectNames.Contains(ErrorMessageIdentifierInJSON) Then
                                    WriteToLog("Try No.:" & Tries.ToString & ", error_message: " & CStr(joGeoLoc(ErrorMessageIdentifierInJSON)))
                                    If ShowErrorDialogInError Then
                                        'The API returned the Error: {1}{0}Do you want to stop this procedure?{0}'Yes' will stop the procedure, 'No' will let it continue ignoring future warning, 'Cancel' will ask for a new API key in case there is something wrong with the current one.
                                        Dim UserResult = MsgBox(sa(strLanguage_Main(110), vbCrLf, CStr(joGeoLoc(ErrorMessageIdentifierInJSON))), MsgBoxStyle.YesNoCancel)
                                        If UserResult = vbYes Then
                                            Exit For
                                        ElseIf UserResult = vbNo Then
                                            ShowErrorDialogInError = False
                                        Else
                                            'Type in a valid Google Geolocation API
                                            TypeBox(strLanguage_Main(111), GeoLocAPIKey, False,,,,,,,, {GeoLocAPIKey})
                                        End If
                                    End If
                                End If

                            Catch exc As Exception
                                If Tries >= 5 Then
                                    Throw New Exception(exc.Message, exc.InnerException)
                                Else
                                    Tries += 1
                                    WriteToLog("Try No.:" & Tries.ToString & ", Exception: " & exc.Message)
                                    Thread.Sleep(2000) 'APIs can reject it if it's tried to connect too many times per second. Waiting 2 seconds allows the API to 'calm itself down'
                                End If
                            End Try
                        Loop

                        Try
                            If joGeoLoc("results").HasValues Then
                                LongitudeX = CDbl(joGeoLoc("results")(0)("geometry")("location")("lng")).ToString 'To ensure that it is a number. If it is not then an error will be thrown,
                                LatitudeY = CDbl(joGeoLoc("results")(0)("geometry")("location")("lat")).ToString 'and the procedure will cease without saving the non-number to the DB
                            ElseIf Not tmpJSONObjectNames.Contains(ErrorMessageIdentifierInJSON) Or
                                                                            (tmpJSONObjectNames.Contains(ErrorMessageIdentifierInJSON) AndAlso Not joGeoLoc(ErrorMessageIdentifierInJSON).ToString.ToLower = APIExceededQuotaError.ToLower) Then
                                LongitudeX = "-1"
                                LatitudeY = "-1"
                            End If
                        Catch ex As Exception
                        End Try

                    End If

                    'No matter how the Geolocation was loaded, now push it into the Database
                    If LatitudeY <> String.Empty AndAlso LongitudeX <> String.Empty Then 'If they are numbers, then save them
                        'Dim SQLCmd As New SqlCommand(<SQL>  USE [<%= DatabaseName %>];
                        '                                    UPDATE [<%= DatabaseName %>].[dbo].[<%= TableErga %>]
                        '                                    SET [<%= ColGeoLocX %>] = <%= LongitudeX %>,
                        '                                        [<%= ColGeoLocY %>] = <%= LatitudeY %>
                        '                                    WHERE [<%= TableErga %>].[<%= ColID_Erga %>] = N'<%= row(columnName:=ColvID_Erga) %>';
                        '                             </SQL>.Value, SQLConn)
                        'SQLCmd.ExecuteNonQuery()
                        ExecuteSQLQuery(<SQL>  USE [<%= DatabaseName %>];
                                                                                            UPDATE [<%= DatabaseName %>].[dbo].[<%= TableErga %>]
                                                                                            SET [<%= ColGeoLocX %>] = <%= LongitudeX %>,
                                                                                                [<%= ColGeoLocY %>] = <%= LatitudeY %>
                                                                                            WHERE [<%= TableErga %>].[<%= ColID_Erga %>] = N'<%= Row(columnName:=ColvID_Erga) %>';
                                                                                     </SQL>.Value)
                    End If

                Else
                    Exit For
                                                        End If
                                                    Next
                                                End Sub)
                                                End If

                                                SQLAdaptrVFinalDataset.Dispose()

                                                If blbtnGeoLocateContinue Then
                                                    'Procedure Successful!
                                                    Notify(strLanguage_Main(112), Color.Cyan, Color.Black, 25)
                                                Else
                                                    'Procedure Cancelled!
                                                    Notify(strLanguage_Main(113), Color.Red, Color.Black, 25)
                                                End If

                                            Catch ex As WebException
                                                ErrorsOnUpdateCount += 1
                                                'Something went wrong whilst trying to download the Geo-Location of [{1}]{0}Either your internet is down, or the API is unresponsive{0}{2}
                                                Notify(sa(strLanguage_Main(114), vbCrLf, CityName, ex.ToString), Color.Red, Color.Black, 25)

                                            Catch ex As Exception
                                                ErrorsOnUpdateCount += 1
                                                'Something went wrong whilst trying to push the Geo-Location of [{1}] to the SQL Server!
                                                Notify(sa(strLanguage_Main(115), vbCrLf, CityName), Color.Red, Color.Black, 25)

                                            Finally
                                                pbGeneralValue = 0
                                                pbGeneralProgress.Style = ProgressBarStyle.Marquee
                                                pbGeneralProgress.MarqueeAnimationSpeed = 10
                                                pbGeneralProgress.Minimum = 0
                                                pbGeneralProgress.Maximum = 1
                                                pbGeneralProgress.Value = 1
                                                pbGeneralProgress.Visible = False
                                                tmrUpdatePB.Enabled = False
                                            End Try

                                        Else
                                            'The procedure Is cancelled because it's failed too many times in a row
                                            MsgBox(sa(strLanguage_Main(116), vbCrLf), MsgBoxStyle.Critical)
                                        End If

                                    Else
                                        'The GeoLocation API Key is empty!{0}Please change it from the Settings form and try again
                                        MsgBox(sa(strLanguage_Main(117), vbCrLf), MsgBoxStyle.Information)
                                        frmSettings.Show()
                                    End If

                                Else
                                    'The GeoLocation API Link is empty!{0}Please change it from the Settings form and try again
                                    MsgBox(sa(strLanguage_Main(118), vbCrLf), MsgBoxStyle.Information)
                                    frmSettings.Show()
                                End If

                            Else
                                'The GeoLocation procedure is trying to upload the longitude and latitude on the SQL Columns {3} on the {1} Table/View on the SQL Server, however one or more columns are unreachable.{0}'{2}' will open for you to create the GeoLocation Columns
                                MsgBox(sa(strLanguage_Main(119), vbCrLf, "[" & DatabaseName & "].[dbo].[" & TablevFinalDataset & "]", RemMniHotLetter(mniCreateNeededSQLViews), sa(strLanguage_Main(120), ColvGeoLocX, ColvGeoLocY))) ''{0}' and '{1}'
                                Call mniCreateGeoColumns_Click(Nothing, New EventArgs)
                            End If

                        Else
                            'The GeoLocation procedure is trying to upload the longitude and latitude on {1} on the SQL Server, however the Table/View is unreachable.{0}'{2}' will open for you to create the Table/View
                            MsgBox(sa(strLanguage_Main(121), vbCrLf, sa("[{0}].dbo.[{1}]", DatabaseName, TablevFinalDataset), RemMniHotLetter(mniCreateNeededSQLViews)))
                            'FuncInProgress.Remove("Geo-Locating")
                            Call CreateNeededSQLViews_Click(Nothing, New EventArgs)
                            Exit Sub
                        End If

                    Else
                        MsgBox(strLanguage_Main(122), MsgBoxStyle.Exclamation) 'There is no connection to the SQL Server!
                    End If

                    blbtnGeoLocateContinue = True
                    mniGeoLocate.Text = strLanguage_Main(108) '&Geo-Locate
                    GeoLocateRect = New Rectangle(0, 0, 0, 0)
                    Refresh()
                    btnGeoLocate.Visible = False
                End If

            ElseIf mniGeoLocate.Text = strLanguage_Main(108) AndAlso Not blbtnGeoLocateContinue Then '&Geo-Locate
                ShowNonInterruptingMsgbox(Me, strLanguage_Main(131), MsgBoxStyle.Information)

            ElseIf mniGeoLocate.Text = strLanguage_Main(109) AndAlso blbtnGeoLocateContinue Then '&Stop GeoLocating
                blbtnGeoLocateContinue = False
            End If

            'FuncInProgress.Remove("Geo-Locating")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub mniClustering_Click(sender As Object, e As EventArgs) Handles mniClusteringStep0.Click
        Dim ClusteringStep0Form As New frmClusteringStep0
        ClusteringStep0Form.Show()
    End Sub

    Private Sub mniPreProcessTheData_Click(sender As Object, e As EventArgs) Handles mniPreProcessTheData.Click
        Dim PreProcessingForm As New frmPreProcessing
        PreProcessingForm.Show()
    End Sub

    <DebuggerStepThrough>
    Private Sub tmrFunctInProgress_Tick(sender As Object, e As EventArgs) Handles tmrFunctInProgress.Tick
        If FuncInProgress.Count > 0 Then
            lblFuncInProgress.Text = ArrayBox(False, ";", 0, True, FuncInProgress)
            If lblFuncInProgress.Visible = False Then
                GeoLocateRect = New Rectangle(pnlMain.Location.X + gbFunctions.Location.X + pnlFunctions.Location.X + lblFuncInProgress.Location.X - 8,
                                              pnlMain.Location.Y + gbFunctions.Location.Y + pnlFunctions.Location.Y + lblFuncInProgress.Location.Y - 8,
                                              lblFuncInProgress.Width + 16,
                                              lblFuncInProgress.Height + 16)
                Refresh()
                lblFuncInProgress.Visible = True
            End If

        ElseIf lblFuncInProgress.Visible = True Then
            lblFuncInProgress.Visible = False
            GeoLocateRect = New Rectangle(0, 0, 0, 0)
            Refresh()
        End If
    End Sub

    Private Sub CreateNeededSQLViews_Click(sender As Object, e As EventArgs) Handles mniCreateNeededSQLViews.Click
        Dim CreateSQLViewForm As New frmCreateSQLView
        CreateSQLViewForm.Show()
    End Sub

    Private Sub Step1OptimalNumberOfClustersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mniClusteringStep1.Click
        Dim ClusteringStep1Form As New frmClusteringStep1
        ClusteringStep1Form.Show()
    End Sub

    Private Sub FormTrainAndTestSetsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mniFormTrainAndTestSets.Click
        Dim ClassificationForm As New frmClassification
        ClassificationForm.Show()
    End Sub

    Private Sub LogisticRegressionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mniLogisticRegression.Click
        Dim LogisticRegressionForm As New frmLogisticRegression
        LogisticRegressionForm.Show()
    End Sub

    Private Sub DecisionTreesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mniDecisionTrees.Click
        Dim DecisionTreesForm As New frmDecisionTrees
        DecisionTreesForm.Show()
    End Sub

    Private Sub mniRandomForest_Click(sender As Object, e As EventArgs) Handles mniRandomForest.Click
        Dim RandomForestForm As New frmRandomForest
        RandomForestForm.Show()
    End Sub

    Private Sub mniStochasticGradientBoosting_Click(sender As Object, e As EventArgs) Handles mniStochasticGradientBoosting.Click
        Dim StochasticGradientBoostingForm As New frmStochasticGradientBoosting
        StochasticGradientBoostingForm.Show()
    End Sub

    Private Sub mniStochasticDualCoordinateAscent_Click(sender As Object, e As EventArgs) Handles mniStochasticDualCoordinateAscent.Click
        Dim StochasticDualCoordinateAscentForm As New frmStochasticDualCoordinateAscent
        StochasticDualCoordinateAscentForm.Show()
    End Sub

    Private Sub mniBoostedDecisionTrees_Click(sender As Object, e As EventArgs) Handles mniBoostedDecisionTrees.Click
        Dim BoostedDecisionTreesForm As New frmBoostedDecisionTrees
        BoostedDecisionTreesForm.Show()
    End Sub

    Private Sub mniEnsambleOfDecisionTrees_Click(sender As Object, e As EventArgs) Handles mniEnsambleOfDecisionTrees.Click
        Dim EnsambleOfDecisionTreesForm As New frmEnsembleofDecisionTrees
        EnsambleOfDecisionTreesForm.Show()
    End Sub

    Private Sub mniNeuralNetworks_Click(sender As Object, e As EventArgs) Handles mniNeuralNetworks.Click
        Dim NeuralNetworksForm As New frmNeuralNetworks
        NeuralNetworksForm.Show()
    End Sub

    Private Sub mniNaiveBayes_Click(sender As Object, e As EventArgs) Handles mniNaiveBayes.Click
        Dim NaiveBayesForm As New frmNaiveBayes
        NaiveBayesForm.Show()
    End Sub

    Private Sub mniFastLogisticRegression_Click(sender As Object, e As EventArgs) Handles mniFastLogisticRegression.Click
        Dim FastLogisticRegressionForm As New frmFastLogisticRegression
        FastLogisticRegressionForm.Show()
    End Sub

    Private Sub mniGeoLocationStatus_Click(sender As Object, e As EventArgs) Handles mniGeoLocationStatus.Click
        Try
            If FuncInProgress.Count = 0 Then
                FuncInProgress.Add(strLanguage_Main(123)) 'Acquiring Geo-Location Status
                If ConnectedToSQLServer = True Then
                    If SQLConn.State <> ConnectionState.Open Then SQLConn.Open()
                    Dim TableExists As Boolean = SQLTableExists(SQLConn, DatabaseName, TablevFinalDataset)
                    If TableExists Then
                        If SQLColumnExists(SQLConn, DatabaseName, TablevFinalDataset, ColvGeoLocX) Then
                            If SQLColumnExists(SQLConn, DatabaseName, TablevFinalDataset, ColvGeoLocY) Then
                                Dim Total_Rows As Integer = CInt(ExecuteSQLScalar(SQLConn, <SQL>SELECT COUNT(<%= ColvID_Erga %>) AS [Total_Rows]
                                                                  FROM [<%= DatabaseName %>].[dbo].[<%= TablevFinalDataset %>]</SQL>.Value))

                                Dim GeoLoc_Rows As Integer = CInt(ExecuteSQLScalar(SQLConn, <SQL>SELECT COUNT(<%= ColvID_Erga %>) AS [GeoLoc_Rows]
                                                                    FROM [<%= DatabaseName %>].[dbo].[<%= TablevFinalDataset %>]
                                                                    WHERE <%= ColvGeoLocX %> IS NOT NULL and <%= ColvGeoLocX %> &lt;> '-1'</SQL>.Value))

                                Dim NonGeoLoc_Rows As Integer = CInt(ExecuteSQLScalar(SQLConn, <SQL>SELECT COUNT(<%= ColvID_Erga %>) AS [NonGeoLoc_Rows]
                                                                            FROM [<%= DatabaseName %>].[dbo].[<%= TablevFinalDataset %>]
                                                                            WHERE <%= ColvGeoLocX %> IS NULL</SQL>.Value))

                                Dim Problematic_Rows As Integer = CInt(ExecuteSQLScalar(SQLConn, <SQL>SELECT COUNT(<%= ColvID_Erga %>) AS [Problematic_Rows]
                                                                            FROM [<%= DatabaseName %>].[dbo].[<%= TablevFinalDataset %>]
                                                                            WHERE <%= ColvGeoLocX %> = '-1'</SQL>.Value))

                                Dim ProblematicOnomaPolisCount As Integer = CInt(ExecuteSQLScalar(SQLConn, <SQL>SELECT COUNT(DISTINCT [<%= ColvCityName %>]) AS [ProblematicOnomaPolisCount]
                                                                                    FROM [<%= DatabaseName %>].[dbo].[<%= TablevFinalDataset %>]
                                                                                    WHERE <%= ColvGeoLocX %> = '-1'</SQL>.Value))
                                'There is a total of {1} projects.{0}For {2}, the Geo-Location process has been successful, whilst for {4} it has not.{0}{3} projects have yet to undergo Geolocation.{0}There is a total of {5} cities/addresses throughout the {4} project for which the Geo-Location failed.
                                MsgBox(sa(strLanguage_Main(124), vbCrLf, Total_Rows, GeoLoc_Rows, NonGeoLoc_Rows, Problematic_Rows, ProblematicOnomaPolisCount), MsgBoxStyle.Information)

                            Else
                                'Unfortunately, the {0} column cannot be accessed; use '{1}' from the Menu to create it
                                MsgBox(sa(strLanguage_Main(125), ColvGeoLocY, RemMniHotLetter(mniCreateGeoColumns)), MsgBoxStyle.Information)
                            End If
                        Else
                            'Unfortunately, the {0} column cannot be accessed; use '{1}' from the Menu to create it
                            MsgBox(sa(strLanguage_Main(125), ColvGeoLocX, RemMniHotLetter(mniCreateGeoColumns)), MsgBoxStyle.Information)
                        End If

                    Else
                        MsgBox(sa(strLanguage_Main(127), TablevFinalDataset), MsgBoxStyle.Exclamation) 'Unfortunately, the Table/View {0} cannot be found
                    End If

                Else
                    MsgBox(sa(strLanguage_Main(128)), MsgBoxStyle.Exclamation) 'You need to be connected to the SQL Server first
                End If

                FuncInProgress.Remove(strLanguage_Main(123)) 'Acquiring Geo-Location Status
            Else
                MsgBox(sa(strLanguage_Main(107), ArrayBox(False, ";", 0, True, FuncInProgress)), MsgBoxStyle.Exclamation) 'Please wait for: {0} to finish
            End If


        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub mniExportListofProblematicAddresses_Click(sender As Object, e As EventArgs) Handles mniExportListofProblematicAddresses.Click
        Try
            If FuncInProgress.Count = 0 Then
                FuncInProgress.Add(strLanguage_Main(130)) 'Exporting List of Problematic Addresses
                If ConnectedToSQLServer = True Then
                    If SQLConn.State <> ConnectionState.Open Then SQLConn.Open()
                    Dim TableExists As Boolean = SQLTableExists(SQLConn, DatabaseName, TablevFinalDataset)
                    If TableExists Then
                        If SQLColumnExists(SQLConn, DatabaseName, TablevFinalDataset, ColvGeoLocX) Then
                            If SQLColumnExists(SQLConn, DatabaseName, TablevFinalDataset, ColvGeoLocY) Then
                                Dim dtProblematicAddresses As New DataTable
                                Dim SQLAdaptrVFinalDataset As SqlDataAdapter = Nothing

                                Dim CityName As String = String.Empty
                                SQLAdaptrVFinalDataset = New SqlDataAdapter(<SQL>
                                                                                SELECT DISTINCT [<%= ColvCityName %>]
                                                                                FROM [<%= DatabaseName %>].[dbo].[<%= TablevFinalDataset %>]
                                                                                WHERE <%= ColvGeoLocX %> = '-1'
                                                                                ORDER BY [<%= ColvCityName %>]
                                                                            </SQL>.Value, SQLConn)
                                SQLAdaptrVFinalDataset.Fill(dtProblematicAddresses)

                                Dim csvFileName As String = doProperFileName(strXDF & "Problematic_Cities.csv")
                                Save_Datatable_To_csv(csvFileName, dtProblematicAddresses)
                                RunOpenDir(csvFileName)

                            Else
                                MsgBox(sa(strLanguage_Main(125), ColvGeoLocY, RemMniHotLetter(mniCreateGeoColumns)), MsgBoxStyle.Information) 'Unfortunately, the {0} column cannot be accessed; use '{1}' from the Menu to create it
                            End If
                        Else
                            MsgBox(sa(strLanguage_Main(125), ColvGeoLocX, RemMniHotLetter(mniCreateGeoColumns)), MsgBoxStyle.Information) 'Unfortunately, the {0} column cannot be accessed; use '{1}' from the Menu to create it
                        End If

                    Else
                        MsgBox(sa(strLanguage_Main(105), TablevFinalDataset), MsgBoxStyle.Exclamation) 'Unfortunately, the table {0} cannot be found
                    End If

                Else
                    MsgBox(sa(strLanguage_Main(128)), MsgBoxStyle.Exclamation) 'You need to be connected to the SQL Server first
                End If

                FuncInProgress.Remove(strLanguage_Main(130)) 'Exporting List of Problematic Addresses
            Else
                MsgBox(sa(strLanguage_Main(107), ArrayBox(False, ";", 0, True, FuncInProgress)), MsgBoxStyle.Exclamation) 'Please wait for: {0} to finish
            End If


        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub mniResetInvalidGeolocationEntries_Click(sender As Object, e As EventArgs) Handles mniResetInvalidGeolocationEntries.Click
        Try
            If FuncInProgress.Count = 0 Then
                FuncInProgress.Add(strLanguage_Main(134)) 'Resetting Invalid Geolocation Entries
                If ConnectedToSQLServer = True Then
                    If SQLConn.State <> ConnectionState.Open Then SQLConn.Open()
                    Dim TableExists As Boolean = SQLTableExists(SQLConn, DatabaseName, TablevFinalDataset)
                    If TableExists Then
                        If SQLColumnExists(SQLConn, DatabaseName, TablevFinalDataset, ColvGeoLocX) Then
                            If SQLColumnExists(SQLConn, DatabaseName, TablevFinalDataset, ColvGeoLocY) Then
                                Dim ErrorMessage As String = String.Empty
                                ExecuteSQLQuery(SQLConn, <SQL>UPDATE [<%= DatabaseName %>].[dbo].[<%= TableErga %>]
                                                                SET [<%= ColGeoLocX %>] = NULL, [<%= ColGeoLocY %>] = NULL
                                                                WHERE <%= ColGeoLocX %> = -1 AND <%= ColGeoLocY %> = -1 </SQL>.Value, ErrorMessage)
                                If ErrorMessage = String.Empty Then
                                    MsgBox(strLanguage_Main(135), MsgBoxStyle.Information) 'All invalid entries (if any) have been successfully reset.
                                Else
                                    MsgBox(ErrorMessage, MsgBoxStyle.Exclamation)
                                End If
                            Else
                                MsgBox(sa(strLanguage_Main(125), ColvGeoLocY, RemMniHotLetter(mniCreateGeoColumns)), MsgBoxStyle.Information) 'Unfortunately, the {0} column cannot be accessed; use '{1}' from the Menu to create it
                            End If
                        Else
                            MsgBox(sa(strLanguage_Main(125), ColvGeoLocX, RemMniHotLetter(mniCreateGeoColumns)), MsgBoxStyle.Information) 'Unfortunately, the {0} column cannot be accessed; use '{1}' from the Menu to create it
                        End If

                    Else
                        MsgBox(sa(strLanguage_Main(105), TablevFinalDataset), MsgBoxStyle.Exclamation) 'Unfortunately, the table {0} cannot be found
                    End If

                Else
                    MsgBox(sa(strLanguage_Main(128)), MsgBoxStyle.Exclamation) 'You need to be connected to the SQL Server first
                End If

                FuncInProgress.Remove(strLanguage_Main(134)) 'Resetting Invalid Geolocation Entries
            Else
                MsgBox(sa(strLanguage_Main(107), ArrayBox(False, ";", 0, True, FuncInProgress)), MsgBoxStyle.Exclamation)
            End If


        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnGeoLocate_Click(sender As Object, e As EventArgs) Handles btnGeoLocate.Click
        Call mniGeoLocate_Click(Nothing, New EventArgs)
    End Sub
End Class