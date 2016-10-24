'Version 2.5 2016/08/30
'Fixed a bug that Expiration Date was needed on registration code even though the registration code doesn't expire.
'Possibly fixed the infinite recursion problem 'Made database Upgrade-Ready; Changed Original Databased to RARed (to avoid deletion on upgrade)
'Needs Settings.ini version 1.6; Language file Version 2.0

'API Key: AIzaSyB1TNGKJ4jTOKWR9lsaJy8R9o1CBgYWCMc
'API Key: AIzaSyB4qeHfUHoimexLFOfS05EdVcVCMJLK2h4
'https://maps.googleapis.com/maps/api/geocode/json?&address=ΠΕΡΙΞ%20ΣΕΡΡΩΝ
'https://maps.googleapis.com/maps/api/geocode/json?&address=ΑΝΩ%20ΜΗΛΙΑ&key=AIzaSyB4qeHfUHoimexLFOfS05EdVcVCMJLK2h4

Option Strict On
Imports System.IO
Imports Microsoft.Win32
Imports System.Threading
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports System.Net

Public Class frmMain

#Region "Constants And Variables"
    Public Args() As String = Environment.GetCommandLineArgs
    Public isProgramStartingUp As Boolean = True

    Public strLanguage_Main() As String
    Public strLanguage_Main_Tips() As String

    Public CommandsDefaultText As String

    Dim ConnectedToSQLServer As Boolean = False

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

    Private blbtnGeoLocateContinue As Boolean = True
    Private ErrorsOnUpdateCount As Integer = 0
    Private pbGeneralValue As Integer = 0

    Public Structure GeoLocation
        Public LatitudeX As Double
        Public longitude As Double
    End Structure


    '===============================
    '==BEGIN OF STANDARD PROCEDURE==
    '===============================

#End Region

    '=============================
    '==END OF STANDARD PROCEDURE==
    '=============================

    Public Function GetLatLon(ByVal APIKey As String, ByVal strAddress As String) As GeoLocation
        Dim url = Convert.ToString("http://maps.google.com/maps/geo?output=csv&key=" & APIKey & "&q=") & strAddress
        Dim request = WebRequest.Create(url)
        Dim response = DirectCast(request.GetResponse(), HttpWebResponse)

        If response.StatusCode = HttpStatusCode.OK Then
            Dim ms = New MemoryStream()
            Dim responseStream = response.GetResponseStream()
            Dim buffer = New Byte(2047) {}
            Dim count As Integer = responseStream.Read(buffer, 0, buffer.Length)
            While count > 0
                ms.Write(buffer, 0, count)
                count = responseStream.Read(buffer, 0, buffer.Length)
            End While
            responseStream.Close()
            ms.Close()
            Dim responseBytes = ms.ToArray()
            Dim encoding = New Text.ASCIIEncoding()
            Dim coords = encoding.GetString(responseBytes)
            Dim parts = coords.Split(","c)

            Return New GeoLocation With {.LatitudeX = Convert.ToDouble(parts(2)), .longitude = Convert.ToDouble(parts(3))}
        End If
        Return Nothing
    End Function

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
    Private Async Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isProgramStartingUp = True

        Try
            'initialization
            Text &= " - Version: " & My.Application.Info.Version.ToString

            OriginalWindowWidth = Width
            OriginalWindowHeight = Height

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

            Try
                MainFolderOnline = Registry.GetValue(strProgramRegistryKeyName, strMainFolderOverrideValueName, MainFolderOnline).ToString

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
            Catch ex As Exception
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
                Dim FirstTimeForm As New frmFirstTime
                FirstTimeForm.TopMost = True
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

            If IsMdiContainer Then
                mniMDIWindows.Visible = True
                mniToggleVisibility.Visible = True
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


            If ConnectedToSQLServer = True Then
                If SQLConn.State <> ConnectionState.Open Then SQLConn.Open()
                Dim curSQLCmd As New SqlCommand(<SQL>
                                                    USE [<%= DatabaseName %>]
                                                    IF NOT EXISTS(
                                                        SELECT *
                                                        FROM sys.columns
                                                        WHERE (Name = N'<%= ColvGeoLocX %>') AND (Object_ID = Object_ID(N'<%= TablevErga %>'))
                                                    )
                                                        BEGIN
                                                            ALTER TABLE [dbo].<%= TablevErga %>
                                                            ADD <%= ColvGeoLocX %> INT NULL, <%= ColvGeoLocY %> INT NULL
                                                        END
                                                </SQL>.Value, SQLConn)
                curSQLCmd.ExecuteNonQuery()

            End If

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

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If FullScreen OrElse FullScreenWindowed Then
            Visible = False
            Call FormStatefrmMain.Restore(Me)
            Call RestoreResolution()
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

    Private Async Sub btnEnter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGo.Click
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

            Case "exit", "quit", "close", "altqq", "exit()", "quit()", "close()", "altqq()"
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
                If strSettings(18).Substring("018DataBaseDir=".Length).ToLower = "default" Then
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

            Case "update", "check for updates", "update()", "check for updates()"
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

            Case "equation mode on", "calculation mode on", "calc mode on"
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

            Case "add variable mode on", "variable mode on"
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

            Case "add function mode on", "function mode on"
                AddFunctionModeOn = True
                EquationModeOn = False
                AddVariableModeOn = False
                lblMathMode.Visible = True
                lblMathMode.Text = strLanguage_Main(63) & strLanguage_Main(66)
                MsgBox(strLanguage_Main(55) & vbCrLf & strLanguage_Main(62), MsgBoxStyle.Information) 'Add Function mode has been turned On, no prefix is necessary

            Case "add function mode off", "function mode off"
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

                    ElseIf (txtCommands.Text.ToLower.StartsWith("r f ") AndAlso Not txtCommands.Text.ToLower.Replace(" ", "").StartsWith("rf=")) OrElse txtCommands.Text.ToLower.StartsWith("remove function ") Then
                        Dim StarterWord As String = String.Empty
                        If txtCommands.Text.ToLower.StartsWith("r f ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "r f ".Length)
                        ElseIf txtCommands.Text.ToLower.StartsWith("remove function ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "remove function ".Length)
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

                    ElseIf AddFunctionModeOn OrElse txtCommands.Text.ToLower.StartsWith("a f ") OrElse txtCommands.Text.ToLower.StartsWith("add f ") OrElse txtCommands.Text.ToLower.StartsWith("add function ") Then
                        Dim StarterWord As String = String.Empty
                        If txtCommands.Text.ToLower.StartsWith("a f ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "a f ".Length)
                        ElseIf txtCommands.Text.ToLower.StartsWith("add f ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "add f ".Length)
                        ElseIf txtCommands.Text.ToLower.StartsWith("add function ") Then
                            StarterWord = GetSubStr(txtCommands.Text, "add function ".Length)
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

    Private Async Sub frmMain_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        Await Task.Run(
            Sub()
                Thread.Sleep(100)
            End Sub)
        If IsMdiContainer AndAlso Not isProgramStartingUp AndAlso ActiveForm IsNot Me Then
            pnlMain.Visible = False
        End If

    End Sub

    Private Sub mniToggleVisibility_Click(sender As Object, e As EventArgs) Handles mniToggleVisibility.Click
        If pnlMain.Visible Then pnlMain.Visible = False Else pnlMain.Visible = True
    End Sub

    Private Sub mnuAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniAbout.Click
        ShowForm(frmAbout)
    End Sub

    Private Sub mniDirectoryPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniProgramsDir.Click
        RunOpenDir(strExplorerExe, My.Application.Info.DirectoryPath)
    End Sub

    Private Sub mnuDBdir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniDatabaseDir.Click
        If strSettings(18).Substring("018DataBaseDir=".Length).ToLower = "default" Then
            RunOpenDir(strExplorerExe, strDatabaseDir)
        Else
            RunOpenDir(strExplorerExe, strSettings(18).Substring("018DataBaseDir=".Length))
        End If
    End Sub

    Private Sub mniExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniExit.Click
        CloseForm(Me, strLanguage_Main(11)) 'Are you sure you want to exit?
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        CloseForm(Me, strLanguage_Main(11)) 'Are you sure you want to exit?
    End Sub

    Private Sub mnuSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniSettings.Click
        ShowForm(frmSettings)
    End Sub

    Private Sub mniCredits_Click(sender As System.Object, e As System.EventArgs) Handles mniCredits.Click
        RunOpenDir(strCredits)
    End Sub

    Private Sub mniProgramWebsite_Click(sender As System.Object, e As System.EventArgs) Handles mniProgramWebsite.Click
        RunOpenDir(strExplorerExe, My.Settings.ProgrammeWebsite)
    End Sub

    Private Sub mniProgrammerWebsite_Click(sender As System.Object, e As System.EventArgs) Handles mniProgrammerWebsite.Click
        RunOpenDir(strExplorerExe, My.Settings.ProgrammersWebsite)
    End Sub

    Private Sub mniEULA_Click(sender As System.Object, e As System.EventArgs) Handles mniEULA.Click
        RunOpenDir(strEULA)
    End Sub

    Private Sub mniChangeLog_Click(sender As System.Object, e As System.EventArgs) Handles mniChangeLog.Click
        RunOpenDir(strChangeLog)
    End Sub

    Private Sub mniShowWelcomeScreen_Click(sender As System.Object, e As System.EventArgs) Handles mniShowWelcomeScreen.Click
        ShowForm(frmFirstTime)
    End Sub

    Private Sub mniShowPresentation_Click(sender As System.Object, e As System.EventArgs) Handles mniShowPresentation.Click
        ShowForm(frmPresentation)
    End Sub

    Private Sub DatabaseMaintenanceToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles mniDatabaseMaintenance.Click
        ShowForm(frmDatabaseMaintenance)
    End Sub

    Private Sub mniHelp_Click(sender As System.Object, e As System.EventArgs) Handles mniHelp.Click
        ShowForm(frmHelp)
    End Sub

    Private Sub tmrUnknownCmd_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrUnknownCmd.Tick

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

    Private Sub txtCommands_LostFocus(sender As Object, e As System.EventArgs) Handles txtCommands.LostFocus
        If txtCommands.Text = String.Empty Then txtCommands.Text = CommandsDefaultText
    End Sub

    Private Sub txtCommands_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCommands.Click
        txtCommands.Text = ""
    End Sub

    Private Sub txtCommands_GotFocus(sender As Object, e As System.EventArgs) Handles txtCommands.GotFocus
        If txtCommands.Text = CommandsDefaultText Then
            txtCommands.Text = String.Empty
            txtCommands.Focus()
        End If
    End Sub

    Private Sub mniSuggestOrComplain_Click(sender As System.Object, e As System.EventArgs) Handles mniSuggestOrComplain.Click
        ShowForm(frmSuggestionAndComplaint)
    End Sub

    Private Sub cmdSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSettings.Click
        ShowForm(frmSettings)
    End Sub

    Private Sub cmdHelp_Click(sender As System.Object, e As System.EventArgs) Handles cmdHelp.Click
        ShowForm(frmHelp)
    End Sub

    Private Sub mniOpenSettingsFile_Click(sender As System.Object, e As System.EventArgs) Handles mniOpenSettingsFile.Click
        If MsgBox(strLanguage_Main(34) & vbCrLf & vbCrLf & strLanguage_Main(35), MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then RunOpenDir(strSettingsIni)
    End Sub

    Private Sub mniExtrasDir_Click(sender As System.Object, e As System.EventArgs) Handles mniExtrasDir.Click
        RunOpenDir(strExtras)
    End Sub

    Private Sub mniLanguageDir_Click(sender As System.Object, e As System.EventArgs) Handles mniLanguageDir.Click
        RunOpenDir(strLanguageFolders)
    End Sub

    Private Sub mniSettingsDir_Click(sender As System.Object, e As System.EventArgs) Handles mniSettingsDir.Click
        RunOpenDir(strSettingsPath)
    End Sub

    Private Sub mniSkinDir_Click(sender As System.Object, e As System.EventArgs) Handles mniSkinDir.Click
        RunOpenDir(strSkin)
    End Sub

    Private Sub mniCompanySite_Click(sender As System.Object, e As System.EventArgs) Handles mniCompanySite.Click
        RunOpenDir(strExplorerExe, My.Settings.CompanyWebsite)
    End Sub

    Private Sub mniProgramDocuments_Click(sender As System.Object, e As System.EventArgs) Handles mniProgramDocuments.Click
        RunOpenDir(strDocumentsProgDir)
    End Sub

    Private Sub mniLicenses_Click(sender As Object, e As EventArgs) Handles mniLicenses.Click
        Dim frm As New frmLicenseViewer
        frm.AlreadyAcceptedAll = True
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
                WriteSettings(strSettings, "frmMain: ResizeEnd")
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

    Private Async Sub btnGeoLocate_Click(sender As Object, e As EventArgs) Handles btnGeoLocate.Click
        If btnGeoLocate.Text = "&Geo-Locate" AndAlso blbtnGeoLocateContinue Then
            btnGeoLocate.Text = "&Stop GeoLocating"

            If ConnectedToSQLServer = True Then
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

                            Dim dtVErga As New DataTable
                            Dim SQLAdaptrVErga As SqlDataAdapter = Nothing

                            Dim CityName As String = String.Empty
                            Try
                                Dim strJSON As String
                                Dim joGeoLoc As JObject

                                'Getting the IDs from View
                                SQLAdaptrVErga = New SqlDataAdapter(<SQL>
                                                                   USE [<%= DatabaseName %>]
                                                                   SELECT [<%= ColvID_Erga %>], [<%= ColvCityName %>], [<%= ColvGeoLocX %>]
                                                                   FROM [dbo].[<%= TablevErga %>]
                                                                   WHERE [<%= ColvGeoLocX %>] IS NULL AND [<%= ColvCityName %>] IS NOT NULL
                                                                </SQL>.Value, SQLConn)
                                SQLAdaptrVErga.Fill(dtVErga)

                                RowsToGeoLCount = dtVErga.Rows.Count
                                pbGeneralProgress.Style = ProgressBarStyle.Blocks
                                pbGeneralProgress.Minimum = 0
                                pbGeneralProgress.Maximum = RowsToGeoLCount
                                pbGeneralProgress.Value = 0
                                pbGeneralValue = 0

                                If RowsToGeoLCount > 0 Then
                                    Await Task.Run(
                                        Sub()
                                            Dim ShowErrorDialogInError As Boolean = True
                                            For Each row As DataRow In dtVErga.Rows
                                                If blbtnGeoLocateContinue Then
                                                    pbGeneralValue += 1

                                                    Dim LatitudeX As String = String.Empty
                                                    Dim LongitudeY As String = String.Empty
                                                    CityName = CStr(row(columnName:=ColvCityName))

                                                    Dim dtAlreadyFoundCityGeoLoc As New DataTable
                                                    Dim SQLAdptAlreadyFoundCity As New SqlDataAdapter(<SQL>
                                                                                                            USE [<%= DatabaseName %>]
                                                                                                            SELECT TOP 1 [<%= ColvGeoLocX %>], [<%= ColvGeoLocY %>]
                                                                                                            FROM [<%= DatabaseName %>].[dbo].[<%= TablevErga %>]
                                                                                                            WHERE [<%= TablevErga %>].[<%= ColvCityName %>] = N'<%= CityName.Replace("'", "''") %>' --Because some entries contain the character "'", which messes with the SQL Query.
                                                                                                         </SQL>.Value, SQLConn)
                                                    SQLAdptAlreadyFoundCity.Fill(dtAlreadyFoundCityGeoLoc)

                                                    If dtAlreadyFoundCityGeoLoc.Rows.Count > 0 AndAlso Not IsDBNull(dtAlreadyFoundCityGeoLoc(0)(ColGeoLocX)) AndAlso Not IsDBNull(dtAlreadyFoundCityGeoLoc(0)(ColGeoLocY)) Then
                                                        LatitudeX = CDbl(dtAlreadyFoundCityGeoLoc.Rows(0)(ColGeoLocX)).ToString 'To ensure that it is a number. If it is not then an error will be thrown,
                                                        LongitudeY = CDbl(dtAlreadyFoundCityGeoLoc.Rows(0)(ColGeoLocY)).ToString 'and the procedure will cease without saving the non-number to the DB

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
                                                                strJSON = DlClinet.DownloadString(GeoLocationAPILink.Replace("{Address}", CityName.Replace(" ", "%20")).Replace("{APIKey}", GeoLocAPIKey))
                                                                joGeoLoc = JObject.Parse(strJSON)
                                                                Tries = 5

                                                                tmpJSONObjectNames = joGeoLoc.Properties.Select(Function(x) x.Name).ToList
                                                                If tmpJSONObjectNames.Contains(ErrorMessageIdentifierInJSON) Then
                                                                    WriteToLog("Try No.:" & Tries.ToString & ", error_message: " & CStr(joGeoLoc(ErrorMessageIdentifierInJSON)))
                                                                    If ShowErrorDialogInError Then
                                                                        Dim UserResult = MsgBox("The API returned the Error: " & CStr(joGeoLoc(ErrorMessageIdentifierInJSON)) & vbCrLf & "Do you want to stop this procedure?" & vbCrLf & "'Yes' will stop the procedure, 'No' will let it continue ignoring future warning, 'Cancel' will ask for a new API key in case there is something wrong with the current one.", MsgBoxStyle.YesNoCancel)
                                                                        If UserResult = vbYes Then
                                                                            Exit For
                                                                        ElseIf UserResult = vbNo Then
                                                                            ShowErrorDialogInError = False
                                                                        Else
                                                                            TypeBox("Type in a valid Google Geolocation API", GeoLocAPIKey, False,,,,,,,, {GeoLocAPIKey})
                                                                        End If
                                                                    End If
                                                                End If

                                                            Catch exc As Exception
                                                                If Tries >= 5 Then
                                                                    Throw New Exception(exc.Message, exc.InnerException)
                                                                Else
                                                                    Tries += 1
                                                                    WriteToLog("Try No.:" & Tries.ToString & ", Exception: " & exc.Message)
                                                                    Thread.Sleep(2000)
                                                                End If
                                                            End Try
                                                        Loop

                                                        Try
                                                            If joGeoLoc("results").HasValues Then
                                                                LatitudeX = CDbl(joGeoLoc("results")(0)("geometry")("location")("lat")).ToString 'To ensure that it is a number. If it is not then an error will be thrown,
                                                                LongitudeY = CDbl(joGeoLoc("results")(0)("geometry")("location")("lng")).ToString 'and the procedure will cease without saving the non-number to the DB
                                                            ElseIf Not tmpJSONObjectNames.Contains(ErrorMessageIdentifierInJSON) Or
                                                                    (tmpJSONObjectNames.Contains(ErrorMessageIdentifierInJSON) AndAlso Not joGeoLoc(ErrorMessageIdentifierInJSON).ToString.ToLower = APIExceededQuotaError.ToLower) Then
                                                                LatitudeX = "-1"
                                                                LongitudeY = "-1"
                                                            End If
                                                        Catch ex As Exception
                                                        End Try

                                                    End If

                                                    'No matter how the Geolocation was loaded, now push it into the Database
                                                    If LatitudeX <> String.Empty AndAlso LongitudeY <> String.Empty Then 'If they are numbers, then save them
                                                        Dim SQLCmd As New SqlCommand(<SQL>  USE [<%= DatabaseName %>];
                                                                                            UPDATE [<%= DatabaseName %>].[dbo].[<%= TableErga %>]
                                                                                            SET [<%= ColGeoLocX %>] = <%= LatitudeX %>,
                                                                                                [<%= ColGeoLocY %>] = <%= LongitudeY %>
                                                                                            WHERE [<%= TableErga %>].[<%= ColID_Erga %>] = N'<%= row(columnName:=ColvID_Erga) %>';
                                                                                     </SQL>.Value, SQLConn)
                                                        SQLCmd.ExecuteNonQuery()
                                                    End If

                                                Else
                                                    Exit For
                                                End If
                                            Next
                                        End Sub)
                                End If

                                SQLAdaptrVErga.Dispose()

                                If blbtnGeoLocateContinue Then
                                    Notify("Procedure Successful!", 25.0#)
                                Else
                                    Notify("Procedure Cancelled!", 25.0#)
                                End If

                            Catch ex As WebException
                                ErrorsOnUpdateCount += 1
                                Notify("Something went wrong whilst trying to download the Geo Location of [" & CityName & "]" & vbCrLf & "Either your internet is down, or the API is unresponsive" & vbCrLf & ex.ToString)

                            Catch ex As Exception
                                ErrorsOnUpdateCount += 1
                                Notify("Something went wrong whilst trying to push the Geo Location of [" & CityName & "]" & " to the SQL Server!")

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
                            MsgBox("The procedure is cancelled because it's failed too many times in a row", MsgBoxStyle.Critical)
                        End If

                    Else
                        MsgBox("The GeoLocation API Key is empty!{vbCrLf}Change it from the Settings form".Replace("vbCrLf", vbCrLf), MsgBoxStyle.Information)
                        frmSettings.Show()
                    End If

                Else
                    MsgBox("The GeoLocation API Link is empty!{vbCrLf}Change it from the Settings form".Replace("vbCrLf", vbCrLf), MsgBoxStyle.Information)
                    frmSettings.Show()
                End If
            Else
                MsgBox("There is no connection to the SQL Server!", MsgBoxStyle.Exclamation)
            End If

            blbtnGeoLocateContinue = True
            btnGeoLocate.Text = "&Geo-Locate"

        ElseIf btnGeoLocate.Text = "&Geo-Locate" AndAlso Not blbtnGeoLocateContinue Then
            ShowNonInterruptingMsgbox(Me, "Please Wait...", MsgBoxStyle.Information)

        ElseIf btnGeoLocate.Text = "&Stop GeoLocating" AndAlso blbtnGeoLocateContinue Then
            blbtnGeoLocateContinue = False
        End If
    End Sub

    Private Sub tmrUpdatePB_Tick(sender As Object, e As EventArgs) Handles tmrUpdatePB.Tick
        Try
            pbGeneralProgress.Value = pbGeneralValue
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim a = GetLatLon("AIzaSyB4qeHfUHoimexLFOfS05EdVcVCMJLK2h4", "ΒΟΛΟΣ")
        MsgBox(a.LatitudeX & vbCrLf & a.longitude)
    End Sub

End Class
