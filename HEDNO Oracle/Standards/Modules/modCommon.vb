'Version 7.14 2017-02-10
'Added "ss" as a quick way of writing string; previously added doMT
'CAUTION ArrayBox(...,List(of List(of T))): it showed columns as rows and vice verse... fixed it but it MIGHT cause a problem to anything that already used it
'Added the "ExtraTextOnTheBeginning on CrashCreate; Moved "TRIAL, BETA, VERBOSE" in here.
'Requires Settings v1.6; ModuleStrings.lng version 1.5.6;

'Admin Privileges: Project Properties -> View (Windows/UAC) Settings ->

'Version Goes: Major.Minor.Build.Revision

'Error Code:        0       x   (2:ErrorType)(2:FormNumber)(2:SubNumber)(2:InSubPlacement)(2:LocalPlacementNum)
'i.e.      :        0       x          0E          01             0B            1F                  A3
'i.e.      :        0xE01010B0A3

'Prevent Form from Closing (add code, don't go to "form_Closing" event cuz this is different)
'Private Shadows Sub FormClosing(ByVal sender As Object, ByVal e As ComponentModel.CancelEventArgs) Handles MyBase.Closing
'    e.Cancel = True
'End Sub

'String Format
'MsgBox(String.Format("{0:n7}", 0.0123456789D) & vbCrLf & Int(0.9123456789D))
'String.Format("{0:D2}", Today.Month)

'Clock / Date / Time
'My.Computer.Clock.LocalTime        -       Has Date and Time (Hours Minutes seconds)
'My.Computer.Clock.GmtTime          -       Same as above, but always in GMT+00 hours, not the computer's hour
'Today.ToLongDateString             -       Does NOT contain any Time (HH mm ss) only Date
'Date and Time variable manipulation
'DateTime.ParseExact(SomeDateString, "dd.MM.yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)

'File Type Association
'Public Shared Sub AssociateFileType(ByVal Extension As String, ByVal Description As String, ByVal Executable As String, Optional ByVal Icon As String = "")
'    My.Computer.Registry.ClassesRoot.CreateSubKey(Extension).SetValue("", Description, 1)
'    My.Computer.Registry.ClassesRoot.CreateSubKey(Extension & "\Shell\Open\Command").SetValue("", Executable & " %l", 1)
'    If Icon <> "" Then My.Computer.Registry.ClassesRoot.CreateSubKey(Extension & "\DefaultIcon").SetValue("", Icon, 1)
'End Sub

'File Type Disassociation
'Public Shared Sub UnassociateFileType(ByVal Extension As String)
'    My.Computer.Registry.ClassesRoot.DeleteSubKeyTree(Extension)
'End Sub

'Registry Get/Del value/Key
'Registry.GetValue("HKEY_LOCAL_MACHINE\Software\R-core\R", "InstallPath", "C:")
'Registry.ClassesRoot.DeleteSubKeyTree(<SubKey>) 'If key doesn't exist, there is a problem!
'Registry.CurrentUser.DeleteSubKey(<SubKey>, False) 'Now everything's okay, no exception will be thrown
'Registry.CurrentUser.OpenSubKey(strProgramRegistrySubKey, True).DeleteValue(strPreviousSerialNumberValueName, False)

'Query
'It doesn't matter what is in front of % or after it. You can also use only a one of the %, using two isn't mandatory.
'SELECT * FROM <table> WHERE <title> LIKE %Something%

'In Access DB: Query Date: "WHERE [buy-date] = #1/1/2001 00:00:00#" - 'MM.dd.yyyy HH:mm:ss
'In SQL      : Query Date: "WHERE "buy-date" = '1/1/2001'"

'Variable Type Symbols
' % / I	Integer  UI	UInteger
' & / L	Long     UL	ULong
' @ / D	Decimal  US	UShort
' ! / F	Single   S	Short
' # / R	Double   C	Char
' $	    String   ## Time

'File Manipulation
'File.SetCreationTime(File,Time)
'File.SetLastAccessTime(File,Time)
'File.SetLastWriteTime(File,Time)

'Splitting String by Next Line
'.Split(New String() {System.Environment.NewLine}, StringSplitOptions.None)

'Key Event
'If My.Computer.Keyboard.ShiftKeyDown AndAlso My.Computer.Keyboard.CtrlKeyDown

Option Strict On

Imports JCS
Imports System.IO
Imports System.Net
Imports System.Text
Imports Microsoft.Win32
Imports System.Net.Mail
Imports System.Threading

Module modCommon
#Region "Initialisation - Changes per Programme!"
    '=========================
    '==Changes per Programme==
    '=========================
    Public IniChars As String = "HEDO" 'In Case there is no modSecurity
    Public Const MailSettingsCipherLevel As Integer = 7    'THIS ONE IS SET EVEN WHEN THE PROGRAMME DOES NOT USE modSecurity !
    Public BETA As Boolean = False
    Public TRIAL As Boolean = True
    Public Verbose As Boolean = False
    Public CypherEncryptionNum As Integer = 1
    '/=======================\
    '/=Changes per Programme=\
    '/=======================\

#Region "Constants and Variables"
    'Constants
    Public Const DefaultDate As Date = #1/1/1753#
    Public Const DefaultDateFormat As String = "dd.MM.yyyy"
    Public Const DefaultDateStringFormat As String = "{0:D2}.{1:D2}.{2:D2}"
    Public Const DefaultDateTimeFormat As String = "dd.MM.yyyy HH:mm"
    Public Const DefaultDateTimeStringFormat As String = "{0:D2}.{1:D2}.{2:D2} {3:D2}:{4:D2}"
    Public Const DefaultDateDelimiter As Char = "."c
    Public Const strArrow As String = " -> "
    Public Const null As String = "null"
    Public Const FreeLicenseString As String = "Everyone (Free License)"

    Public CoresCount As Integer = Environment.ProcessorCount

    Public Const MaxDouble As Double = Double.MaxValue - 1
    Public Const MinDouble As Double = Double.MinValue + 1

    Public Const MaxDecimal As Decimal = Decimal.MaxValue - 1
    Public Const MinDecimal As Decimal = Decimal.MinValue + 1

    Public Const MaxSingle As Single = Single.MaxValue - 1
    Public Const MinSingle As Single = Single.MinValue + 1

    Public Const MaxInteger As Integer = Integer.MaxValue - 1
    Public Const MinInteger As Integer = Integer.MinValue + 1

    Public Const MaxUInteger As UInteger = UInteger.MaxValue - 1

    Public Const MaxShort As Short = Short.MaxValue - 1
    Public Const MinShort As Short = Short.MinValue + 1

    Public Const MaxUShort As UShort = UShort.MaxValue - 1

    Public Const MaxLong As Long = Long.MaxValue - 1
    Public Const MinLong As Long = Long.MinValue + 1

    Public Const MaxULong As ULong = ULong.MaxValue - 1UL
    'Variables

    'Super Global
    Public UserName As String = FreeLicenseString
    Public Licensee As String
    Public isMainFormMDI As Boolean
    Public SettingsLevel As Integer
    Public strSettings() As String
    Public strAppDataSettings() As String
    Public strModLanguage() As String
    Public strOnlineMainInfo() As String
    Public isInternetAvailable As Boolean
    Public PreventInternetCheck As Boolean
    Public CurrentLanguage As String = "en-GB"
    Public strDecimalSeparator As String = "."
    Public LangDelimiterForRangeOrSpan As String = "to"
    Public Const strMainFolderOverrideValueName As String = "Override Main Folder"
    Public RemoteServer As String = "https://www.nihilistslab.com/Programs/" & My.Application.Info.Title.Replace(" ", "")
    Public MainFolderOnline As String = RemoteServer & "/Main/"
    Public strProgramRegistrySubKey As String = "Software\Microsoft\" & IniChars
    Public strProgramRegistryKeyName As String = Registry.CurrentUser.Name & "\Software\Microsoft\" & IniChars
    Public TextLanguage As InputLanguage = Application.CurrentInputLanguage 'Changes on ReadMainStrings()

    'General
    Public CharLevel(94) As Char
    Public DlFile As New WebClient
    Public StreamWrite_Persistent As StreamWriter
    Public ProgramArchitecture As Integer = Runtime.InteropServices.Marshal.SizeOf(GetType(IntPtr)) * 8
    Public WindowsArchitecture As Integer = OSVersionInfo.OSBits * 32
    Public FullScreenWidth As Integer
    Public FullScreenHeight As Integer
    Public FullScreenDisplayFrequency As Integer
    Public FullScreenColour As Short
    Public FullScreen As Boolean
    Public FullScreenWindowed As Boolean
    Public WindowWidth As Integer
    Public WindowHeight As Integer
    Public RememberWindowState As Boolean
    Public intWindowState As Integer
    Public OriginalWindowWidth As Integer = 800
    Public OriginalWindowHeight As Integer = 600
    Public CurrentlyWritingSettings As Boolean

    '=========================================================================================
    '==Paths====ANYTHING NEW HERE MUST ALSO BE INSERTED INTO THE RESOLVE/UNRESOLVE WILDNAMES==
    '=========================================================================================
    Public strAutoRunRegistryKeyPath As String = Registry.LocalMachine.Name & "\Software\Microsoft\Windows\CurrentVersion\Run"
    Public strAutoRunRegistrySubKeyPath As String = "Software\Microsoft\Windows\CurrentVersion\Run"
    Public strDesktop As String = My.Computer.FileSystem.SpecialDirectories.Desktop & "\"
    Public strMyDocuments As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\"
    Public strDocumentsProgDir As String = strMyDocuments & My.Application.Info.AssemblyName & "\"
    Public strWindowsDir As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows).ToString & "\"
    Public strAppDataProgDir As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\" & My.Application.Info.Title & "\"
    Public strAppDataSettingsFile As String = strAppDataProgDir & "Settings.ini"
    Public strRoot As String = My.Application.Info.DirectoryPath & "\"
    Public strStartupExe As String = strRoot & "StartUp.exe"
    Public strSettingsPath As String = strRoot & "Settings\"
    Public strExtras As String = strRoot & "Extras\"
    Public strLanguageFolders As String = strRoot & "Language\"
    Public strSkin As String = strRoot & "Skins\"
    Public strDataDir As String = strRoot & "\Data\"
    Public strDatabaseDir As String = strRoot & "Database\"
    Public strBackupDir As String = strRoot & "\Backup\"
    Public strDefDBName As String = "Database.accdb"
    Public strDefDBFile As String = strDatabaseDir & strDefDBName
    Public strChangeLog As String = strRoot & "Changelog.html"
    Public strEULA As String = strRoot & "EULA.rtf"
    Public strLicensesDir As String = strRoot & "Licenses\"
    Public strPictures As String = strRoot & "Pictures\"
    Public strPresentation As String = strLanguageFolders & CurrentLanguage & "\Presentation\"
    Public strModuleLanguageFile As String = strLanguageFolders & CurrentLanguage & "\ModuleStrings.lng"
    Public strUniversal As String = strLanguageFolders & "UniversalStrings.lng"
    Public strCredits As String = strLanguageFolders & CurrentLanguage & "\HTML\" & My.Application.Info.Title.ToString.Replace(" ", "") & "Credits.html"
    Public strSettingsIni As String = strSettingsPath & "Settings.ini"
    Public strSettingsBakup As String = strSettingsPath & "Settings.bak"
    Public strSettingsOrig As String = strSettingsPath & "Settings.orig"
    Public strSettingsRar As String = strSettingsPath & "Settings.rar"
    Public strExplorerExe As String = strWindowsDir & "explorer.exe"
    Public strIExplore As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) & "\Internet Explorer\iexplore.exe"
    Public strUnrar As String = strExtras & "UnRARx64.exe"
    Public strLogFilePath As String = strExtras & My.Application.Info.AssemblyName.Replace(" ", "_") & ".log"
    'Public strUnlocker As String = strExtras & "X" & ProgramArchitecture & "\Unlocker.exe"
    '=========================================================================================
    '==Paths====ANYTHING NEW HERE MUST ALSO BE INSERTED INTO THE RESOLVE/UNRESOLVE WILDNAMES==
    '=========================================================================================

    'Auxiliary
    Dim exeProcesses As New Process
    Dim numOfPercents As Integer = 0
    Dim CurPleaseWaitForm As frmPleaseWait = Nothing
    Dim AlreadyRestoredSettings As Boolean = False

#End Region

    'Public Function CreateShortcut(ByVal FilePathToShortcutTo As String, ByVal SavePath As String, ByVal ShortCutNameAlone As String, Optional ByVal IconPath As String = "") As Boolean
    '    Try
    '        Dim wsh As Object = CreateObject("WScript.Shell")
    '        Dim MyShortcut As Object = wsh.CreateShortcut(SavePath & ShortCutNameAlone & ".lnk")

    '        MyShortcut.TargetPath = wsh.ExpandEnvironmentStrings(FilePathToShortcutTo)
    '        MyShortcut.WorkingDirectory = wsh.ExpandEnvironmentStrings(GetParentDir(FilePathToShortcutTo))
    '        MyShortcut.WindowStyle = 4
    '        If IconPath <> "" Then MyShortcut.IconLocation = IconPath
    '        MyShortcut.Save()

    '        Return True

    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    Public Sub WriteToLog(ByVal Text As String, Optional ByVal PrependTime As Boolean = True)
        WriteToLog(Text, strLogFilePath, PrependTime)
    End Sub
    Public Sub WriteToLog(ByVal Text As String, ByVal LogFilePath As String, Optional ByVal PrependTime As Boolean = True)
        If PrependTime Then Text = Now.ToString("dd/MM/yyyy hh:mm:ss.fff") & vbTab & Text
        File.AppendAllText(LogFilePath, Text & Environment.NewLine, Encoding.UTF8)
    End Sub
    Public Sub WriteToLog(ByVal Lines() As String, Optional ByVal PrependTime As Boolean = True)
        If PrependTime AndAlso Lines.Length > 0 Then Lines(0) = Now.ToString("dd/MM/yyyy hh:mm:ss.fff") & vbTab & Lines(0)
        WriteToLog(Lines, strLogFilePath, PrependTime)
    End Sub
    Public Sub WriteToLog(ByVal Lines() As String, ByVal LogFilePath As String, Optional ByVal PrependTime As Boolean = True)
        File.AppendAllLines(LogFilePath, Lines, Encoding.UTF8)
    End Sub


    Public Sub ShowNonInterruptingMsgbox(ByVal frm As Form, TextToShow As String, Optional Style As MsgBoxStyle = MsgBoxStyle.Information)
        frm.BeginInvoke(Sub() MsgBox(TextToShow, Style))
    End Sub

    Public Async Sub ShowMTNonInterruptingMsgbox(ByVal frm As Form, TextToShow As String, Optional Style As MsgBoxStyle = MsgBoxStyle.Information)
        Await Task.Run(
            Sub()
                ShowNonInterruptingMsgbox(frm, TextToShow, Style)
            End Sub)
    End Sub

    Public Sub Notify(ByVal NotificationText As String, ByVal FirstColour As Color, ByVal SecondColour As Color, ByVal SecondsToDisplay As Double, Optional ByVal frmWidth As Integer = 0, Optional ByVal frmHeigh As Integer = 0, Optional ByVal WarningIcon As Boolean = False)
        Dim NewToastNotifier As New frmToastNotification(CInt(SecondsToDisplay * 1000), NotificationText)
        If frmWidth <> 0 Then NewToastNotifier.Width = frmWidth
        If frmHeigh <> 0 Then NewToastNotifier.Height = frmHeigh
        NewToastNotifier.WarningIcon = WarningIcon
        NewToastNotifier.Show()

        '    rtbNotifications.Text = DateString & " " & TimeString & vbCrLf & NotificationText & vbCrLf & vbCrLf & rtbNotifications.Text

        '    Dim SelectionStart As Integer = rtbNotifications.Find(TimeString)
        '    rtbNotifications.Select(SelectionStart + TimeString.Length + 1, NotificationText.Length)
        '    rtbNotifications.SelectionColor = FirstColour
        '    rtbNotifications.SelectionBackColor = SecondColour

        '    Width = NotificationsAnalysisWidth

        '    'NotificationIntCount = 0
        '    CloseNotificationIntCount = pbCloseNotificationPanel.Maximum

        '    GlobalNotificationText = NotificationText
        '    GlobalFirstColour = FirstColour
        '    GlobalSecondColour = SecondColour
        '    tmrCloseNotificationPanel.Enabled = True

    End Sub

    Public Sub RunSecurely(ByVal ActionSub As Action, ByVal frm As Form)
        doMT(ActionSub, frm)
    End Sub
    Public Sub SecurelyExecute(ByVal ActionSub As Action, ByVal frm As Form)
        doMT(ActionSub, frm)
    End Sub
    Public Sub doMT(ByVal ActionSub As Action, ByVal frm As Form)
        If frm.InvokeRequired = True Then
            frm.BeginInvoke(ActionSub)
        Else
            ActionSub()
        End If
    End Sub

    Public Function AllocateArray2D(Of T)(ByVal pWidth As Integer, ByVal pHeight As Integer) As T()()
        Dim ret As T()() = New T(pWidth - 1)() {}
        For i As Integer = 0 To pHeight - 1
            ret(i) = New T(pHeight - 1) {}
        Next

        Return ret
    End Function

    Public Sub ChangeVariablesContainingCurrentLanguageVar(ByVal Language As String)
        strPresentation = strLanguageFolders & Language & "\Presentation\"
        strModuleLanguageFile = strLanguageFolders & Language & "\ModuleStrings.lng"
        strCredits = strLanguageFolders & Language & "\HTML\" & My.Application.Info.Title.ToString.Replace(" ", "") & "Credits.html"
    End Sub

    Public Sub ReadMainStrings()
        Try
            Call CypherVariablization()
            Call LoadSettings()

            If strSettings(32).Length > "032isMDI=".Length Then
                If strSettings(32).Length = "032isMDI=".Length OrElse strSettings(32).Substring("032isMDI=".Length).ToLower = "default" Then
                    isMainFormMDI = frmMain.IsMdiContainer
                Else
                    isMainFormMDI = CBool(strSettings(32).Substring("032isMDI=".Length))
                    frmMain.IsMdiContainer = isMainFormMDI
                End If
            End If

            'Loading Language
            If strSettings(6).Substring("006Language=".Length) <> "" AndAlso strSettings(6).Substring("006Language=".Length).ToLower <> "default" Then
                CurrentLanguage = strSettings(6).Substring("006Language=".Length)
            Else
                If Directory.Exists(strLanguageFolders & Thread.CurrentThread.CurrentCulture.Name) Then
                    CurrentLanguage = Thread.CurrentThread.CurrentCulture.Name
                ElseIf Directory.Exists(strLanguageFolders & "en-GB") Then
                    CurrentLanguage = "en-GB"
                ElseIf Directory.Exists(strLanguageFolders & "en-US") Then
                    CurrentLanguage = "en-US"
                End If
            End If

            'As the Current Language has changed, all variables that are dependent on CurrentLanguage should change too.
            Call ChangeVariablesContainingCurrentLanguageVar(CurrentLanguage)

            If TextLanguage.Culture.ToString.ToLower <> CurrentLanguage.ToLower Then
                For i As Integer = 0 To InputLanguage.InstalledInputLanguages.Count - 1
                    If InputLanguage.InstalledInputLanguages(i).Culture.ToString.ToLower = CurrentLanguage.ToLower OrElse ((CurrentLanguage.ToLower = "en-gb".ToLower OrElse CurrentLanguage.ToLower = "en-us".ToLower) AndAlso (InputLanguage.InstalledInputLanguages(i).Culture.ToString.ToLower = "en-gb".ToLower OrElse InputLanguage.InstalledInputLanguages(i).Culture.ToString.ToLower = "en-us".ToLower)) Then
                        Application.CurrentInputLanguage = InputLanguage.InstalledInputLanguages(i)
                        TextLanguage = Application.CurrentInputLanguage
                    End If
                Next
            End If

            'Loading Module Language
            ReadFile(strModuleLanguageFile, strModLanguage)

            'Loading the Language's Delimiter for Ranges/Spans
            LangDelimiterForRangeOrSpan = strModLanguage(44)    'to

            If ProgramArchitecture = 64 Then
                'strUnlocker = strExtras & "Unlockerx64.exe" ''''
                strUnrar = strExtras & "UnRARx64.exe"
            ElseIf ProgramArchitecture = 32 Then
                'strUnlocker = strExtras & "Unlockerx86.exe" ''''
                strUnrar = strExtras & "UnRARx86.exe"
            Else 'The architecture of the program or of the windows is unknown (not 32bit nor 64bit).
                MsgBox(strModLanguage(40) & vbCrLf & strModLanguage(41), MsgBoxStyle.Exclamation) 'It is possible that some operations malfunction.
            End If

            isInternetAvailable = CheckInternetAvailability("http://www.google.com")

            Tip_LangString = strModLanguage(121) 'Tip:

        Catch ex As Exception
            If AlreadyRestoredSettings Then
                CreateCrashFile(ex, True)
            Else
                AlreadyRestoredSettings = True
                File.Delete(strSettingsIni)
                File.Copy(strSettingsOrig, strSettingsIni)
                Call ReadMainStrings()
            End If
        End Try
    End Sub

    Public Function IsFormOpen(ByVal frm As Form) As Boolean
        Return Application.OpenForms.OfType(Of Form).Contains(frm)
    End Function

#End Region

#Region "isNumeric - All Variations"
    Public Function isNumericExtended(Of T)(ByVal Var As T) As Boolean
        Dim tmpString As String = String.Empty
        Try
            tmpString = MathEvaluator.SimplifyObject(Var.ToString).ToString
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

#Region "is Numeric Boolean"
    Public Function isNumericBoolean(Of T)(ByVal Variable As T) As Boolean
        If IsNumeric(Variable.ToString) AndAlso (CInt(Variable.ToString) = 0 OrElse CInt(Variable.ToString) = 1) Then Return True Else Return False
    End Function
    Public Function isNumericBoolean(Of T)(ByVal Variable() As T) As Boolean
        For Each Item In Variable
            If Not isNumericBoolean(Item) Then Return False
        Next
        Return True
    End Function
#End Region

#Region "is Numeric Array"
    Public Function isNumericArray(Of T)(ByVal Variable() As T, Optional ByVal ExcludeQuotesOnSearch As Boolean = False, Optional ByVal SimplifyByMathParserFirst As Boolean = False) As Boolean

        For Each Item In Variable

            '1 - 0
            If ExcludeQuotesOnSearch AndAlso Not SimplifyByMathParserFirst Then
                If Not IsNumeric(Item.ToString.Replace("""", "")) Then Return False

                '0 - 0
            ElseIf Not ExcludeQuotesOnSearch AndAlso Not SimplifyByMathParserFirst Then
                If Not IsNumeric(Item) Then Return False

                '1 - 1
            ElseIf ExcludeQuotesOnSearch AndAlso SimplifyByMathParserFirst Then
                Dim tmpResult As String = String.Empty
                Try
                    tmpResult = MathEvaluator.SimplifyObject(Item.ToString).ToString 'It automatically doesn't care for """"
                Catch ex As Exception
                    Return False
                End Try

                '0 - 1
            Else
                If Not Item.ToString.Contains("""") Then
                    Dim tmpResult As String = String.Empty
                    Try
                        tmpResult = MathEvaluator.SimplifyObject(Item.ToString).ToString 'It automatically doesn't care for """"
                    Catch ex As Exception
                        Return False
                    End Try
                End If
            End If

        Next

        Return True
    End Function
    Public Function isNumericArray(Of T)(ByVal Variable As List(Of T), Optional ByVal ExcludeQuotesOnSearch As Boolean = False, Optional ByVal SimplifyByMathParserFirst As Boolean = False) As Boolean

        For Each Item In Variable

            '1 - 0
            If ExcludeQuotesOnSearch AndAlso Not SimplifyByMathParserFirst Then
                If Not IsNumeric(Item.ToString.Replace("""", "")) Then Return False

                '0 - 0
            ElseIf Not ExcludeQuotesOnSearch AndAlso Not SimplifyByMathParserFirst Then
                If Not IsNumeric(Item) Then Return False

                '1 - 1
            ElseIf ExcludeQuotesOnSearch AndAlso SimplifyByMathParserFirst Then
                Dim tmpResult As String = String.Empty
                Try
                    tmpResult = MathEvaluator.SimplifyObject(Item.ToString).ToString 'It automatically doesn't care for """"
                Catch ex As Exception
                    Return False
                End Try

                '0 - 1
            Else
                If Not Item.ToString.Contains("""") Then
                    Dim tmpResult As String = String.Empty
                    Try
                        tmpResult = MathEvaluator.SimplifyObject(Item.ToString).ToString 'It automatically doesn't care for """"
                    Catch ex As Exception
                        Return False
                    End Try
                End If
            End If

        Next

        Return True
    End Function
#End Region

    Public Function isSpecificNumericArray(Of T)(ByVal Variable() As T, ListOfAcceptableValues() As Double) As Boolean
        For Each Item In Variable
            If Not IsNumeric(Item) OrElse Not IsOneOfTheFollowing(CDbl(Item.ToString), ListOfAcceptableValues) Then Return False
        Next

        Return True
    End Function
#End Region

#Region "GetSubstrAfterString String/List"

    Public Function GetSubStr(ByVal Str As String, ByVal Length As Integer, Optional ByVal LeftMidRight As String = "Left", Optional ByVal EndingIndex As Integer = -1, Optional ByVal AppendSpacesTillEndingIndex As Boolean = False) As String
        'This is used by non-module subs that do not have access to "right" and "left" functions
        Dim Result As String = Str

        If LeftMidRight.ToLower = "left" Then
            If Str.Length > Length Then
                Result = Left(Result, Length)
            End If

        ElseIf LeftMidRight.ToLower = "mid" Then
            Result = Mid(Result, Length, EndingIndex)

        ElseIf LeftMidRight.ToLower = "right" Then
            If Str.Length > Length Then
                Result = Right(Result, Length)
            End If
        End If

        If AppendSpacesTillEndingIndex AndAlso Result.Length < Length Then
            For i As Integer = Result.Length To Length
                Result &= " "
            Next
        End If

        Return Result
    End Function
    Public Function AntiSubString(ByVal str As String, ByVal Length As Integer) As String
        Try
            Return str.Substring(0, str.Length - Length)

        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' CASE SENSITIVE (unless otherwise specified): Gets a rest of the line from a string array extracted from the line that contains the specific string (StrToGetSubstrAfter).
    ''' </summary>
    ''' <param name="StrToBeSearched"></param>
    ''' <param name="StrToGetSubstrAfter"></param>
    ''' <param name="ResultIfStrNotFound"></param>
    ''' <param name="StartsWith"></param>
    ''' <param name="LastIndexOfInsteadOfFirst"></param>
    ''' <param name="RowIndex"></param>
    ''' <param name="StrIndexInRow"></param>
    ''' <returns></returns>
    Public Function GetSubstrAfterString(ByVal StrToBeSearched() As String, ByVal StrToGetSubstrAfter As String, Optional ByVal ResultIfStrNotFound As String = "", Optional ByVal StartsWith As Boolean = False, Optional ByVal LastIndexOfInsteadOfFirst As Boolean = False, Optional ByRef RowIndex As Integer = -1, Optional ByRef StrIndexInRow As Integer = -1, Optional ByVal IgnoreCase As Boolean = False) As String
        Dim strResult As String = ResultIfStrNotFound
        Dim MyStringComparison As StringComparison = StringComparison.InvariantCulture

        For i = 0 To StrToBeSearched.Length - 1
            If StrToBeSearched(i).Contains(StrToGetSubstrAfter) Then
                Dim Index As Integer = -1

                If Not StartsWith AndAlso Not LastIndexOfInsteadOfFirst Then '                                                                               Anywhere in the string, first occurrence
                    Index = StrToBeSearched(i).IndexOf(StrToGetSubstrAfter, MyStringComparison) + StrToGetSubstrAfter.Length

                ElseIf StartsWith AndAlso StrToBeSearched(i).StartsWith(StrToGetSubstrAfter, MyStringComparison) AndAlso Not LastIndexOfInsteadOfFirst Then 'Start of the string, first occurrence
                    Index = StrToGetSubstrAfter.Length

                ElseIf Not StartsWith AndAlso LastIndexOfInsteadOfFirst Then '                                                                               Anywhere in the string, Last occurrence
                    Index = StrToBeSearched(i).LastIndexOf(StrToGetSubstrAfter, MyStringComparison) + StrToGetSubstrAfter.Length

                ElseIf StartsWith AndAlso StrToBeSearched(i).StartsWith(StrToGetSubstrAfter) AndAlso LastIndexOfInsteadOfFirst Then '                        Start of the string, Last occurrence
                    Index = StrToBeSearched(i).LastIndexOf(StrToGetSubstrAfter, MyStringComparison) + StrToGetSubstrAfter.Length
                End If

                If Index <> -1 Then
                    RowIndex = i
                    StrIndexInRow = Index
                    strResult = StrToBeSearched(i).Substring(Index)
                    Exit For
                End If
            End If
        Next

        Return strResult
    End Function
    Public Function GetSubstrAfterString(ByVal StrToBeSearched As String, ByVal StrToGetSubstrAfter As String, Optional ByVal ResultIfStrNotFound As String = "", Optional ByVal StartsWith As Boolean = False, Optional ByVal LastIndexOfInsteadOfFirst As Boolean = False) As String
        Dim strResult As String = ResultIfStrNotFound

        If StrToBeSearched.Contains(StrToGetSubstrAfter) Then
            Dim Index As Integer = -1

            If Not StartsWith AndAlso Not LastIndexOfInsteadOfFirst Then '                                                              Anywhere in the string, first occurance
                Index = StrToBeSearched.IndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length

            ElseIf StartsWith AndAlso StrToBeSearched.StartsWith(StrToGetSubstrAfter) AndAlso Not LastIndexOfInsteadOfFirst Then '   Start of the string, first occurance
                Index = StrToGetSubstrAfter.Length

            ElseIf Not StartsWith AndAlso LastIndexOfInsteadOfFirst Then '                                                              Anywhere in the string, Last occurance
                Index = StrToBeSearched.LastIndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length

            ElseIf StartsWith AndAlso StrToBeSearched.StartsWith(StrToGetSubstrAfter) AndAlso LastIndexOfInsteadOfFirst Then '       Start of the string, Last occurance
                Index = StrToBeSearched.LastIndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length
            End If

            If Index <> -1 Then strResult = StrToBeSearched.Substring(Index)
        End If

        Return strResult
    End Function
    Public Function GetMultipleSubstrAfterString(ByVal StrToBeSearched() As String, ByVal StrToGetSubstrAfter As String, Optional ByVal ResultIfStrNotFound As String = "", Optional ByVal StartsWith As Boolean = False, Optional ByVal LastIndexOfInsteadOfFirst As Boolean = False) As String()
        Dim strResult(0) As String
        strResult(0) = ResultIfStrNotFound

        For i = 0 To StrToBeSearched.Length - 1
            If StrToBeSearched(i).Contains(StrToGetSubstrAfter) Then
                Dim Index As Integer = -1

                If Not StartsWith AndAlso Not LastIndexOfInsteadOfFirst Then '                                                              Anywhere in the string, first occurance
                    Index = StrToBeSearched(i).IndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length

                ElseIf StartsWith AndAlso StrToBeSearched(i).StartsWith(StrToGetSubstrAfter) AndAlso Not LastIndexOfInsteadOfFirst Then '   Start of the string, first occurance
                    Index = StrToGetSubstrAfter.Length

                ElseIf Not StartsWith AndAlso LastIndexOfInsteadOfFirst Then '                                                              Anywhere in the string, Last occurance
                    Index = StrToBeSearched(i).LastIndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length

                ElseIf StartsWith AndAlso StrToBeSearched(i).StartsWith(StrToGetSubstrAfter) AndAlso LastIndexOfInsteadOfFirst Then '       Start of the string, Last occurance
                    Index = StrToBeSearched(i).LastIndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length
                End If

                If Index <> -1 Then
                    If strResult.Length > 1 Then ReDim Preserve strResult(strResult.Length)
                    strResult(strResult.Length - 1) = StrToBeSearched(i).Substring(Index)
                End If
            End If
        Next

        Return strResult
    End Function
    Public Function GetSubstrAfterString(ByVal LstToBeSearched As List(Of String), ByVal StrToGetSubstrAfter As String, Optional ByVal ResultIfStrNotFound As String = "", Optional ByVal StartsWith As Boolean = True, Optional ByVal LastIndexOfInsteadOfFirst As Boolean = False) As String
        Dim strResult As String = ResultIfStrNotFound

        For i As Integer = 0 To LstToBeSearched.Count - 1
            If LstToBeSearched(i).Contains(StrToGetSubstrAfter) Then
                Dim Index As Integer = -1

                If Not StartsWith AndAlso Not LastIndexOfInsteadOfFirst Then '                                                                  Anywhere in the string, first occurance
                    Index = LstToBeSearched.Item(i).IndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length

                ElseIf StartsWith AndAlso LstToBeSearched.Item(i).StartsWith(StrToGetSubstrAfter) AndAlso Not LastIndexOfInsteadOfFirst Then '  Start of the string, first occurance
                    Index = StrToGetSubstrAfter.Length

                ElseIf Not StartsWith AndAlso LastIndexOfInsteadOfFirst Then '                                                                  Anywhere in the string, Last occurance
                    Index = LstToBeSearched.Item(i).LastIndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length

                ElseIf StartsWith AndAlso LstToBeSearched.Item(i).StartsWith(StrToGetSubstrAfter) AndAlso LastIndexOfInsteadOfFirst Then '      Start of the string, Last occurance
                    Index = LstToBeSearched.Item(i).LastIndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length
                End If

                If Index <> -1 Then
                    strResult = LstToBeSearched(i).Substring(Index)
                    Exit For
                End If
            End If
        Next

        Return strResult
    End Function
    Public Function GetMultipleSubstrAfterString(ByVal LstToBeSearched As List(Of String), ByVal StrToGetSubstrAfter As String, Optional ByVal ResultIfStrNotFound As String = "", Optional ByVal StartsWith As Boolean = False, Optional ByVal LastIndexOfInsteadOfFirst As Boolean = False) As String()
        Dim strResult(0) As String
        strResult(0) = ResultIfStrNotFound

        For i = 0 To LstToBeSearched.Count - 1
            If LstToBeSearched(i).Contains(StrToGetSubstrAfter) Then
                Dim Index As Integer = -1

                If Not StartsWith AndAlso Not LastIndexOfInsteadOfFirst Then '                                                                  Anywhere in the string, first occurance
                    Index = LstToBeSearched.Item(i).IndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length

                ElseIf StartsWith AndAlso LstToBeSearched.Item(i).StartsWith(StrToGetSubstrAfter) AndAlso Not LastIndexOfInsteadOfFirst Then '  Start of the string, first occurance
                    Index = StrToGetSubstrAfter.Length

                ElseIf Not StartsWith AndAlso LastIndexOfInsteadOfFirst Then '                                                                  Anywhere in the string, Last occurance
                    Index = LstToBeSearched.Item(i).LastIndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length

                ElseIf StartsWith AndAlso LstToBeSearched.Item(i).StartsWith(StrToGetSubstrAfter) AndAlso LastIndexOfInsteadOfFirst Then '      Start of the string, Last occurance
                    Index = LstToBeSearched.Item(i).LastIndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length
                End If

                If Index <> -1 Then
                    If strResult.Length > 1 Then ReDim Preserve strResult(strResult.Length)
                    strResult(strResult.Length - 1) = LstToBeSearched(i).Substring(Index)
                End If
            End If
        Next

        Return strResult
    End Function
    Public Function GetMultipleSubstrAfterString(ByVal LstToBeSearched As List(Of String), ByVal StrToGetSubstrAfter As String, ByVal ReturnNullListOnStrNotFound As Boolean, Optional ByVal StartsWith As Boolean = False, Optional ByVal LastIndexOfInsteadOfFirst As Boolean = False) As List(Of String)
        Dim strResult As New List(Of String)
        If Not ReturnNullListOnStrNotFound Then
            strResult.Add("")
        End If

        For i = 0 To LstToBeSearched.Count - 1
            If LstToBeSearched(i).Contains(StrToGetSubstrAfter) Then
                Dim Index As Integer = -1

                If Not StartsWith AndAlso Not LastIndexOfInsteadOfFirst Then '                                                                  Anywhere in the string, first occurance
                    Index = LstToBeSearched.Item(i).IndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length

                ElseIf StartsWith AndAlso LstToBeSearched.Item(i).StartsWith(StrToGetSubstrAfter) AndAlso Not LastIndexOfInsteadOfFirst Then '  Start of the string, first occurance
                    Index = StrToGetSubstrAfter.Length

                ElseIf Not StartsWith AndAlso LastIndexOfInsteadOfFirst Then '                                                                  Anywhere in the string, Last occurance
                    Index = LstToBeSearched.Item(i).LastIndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length

                ElseIf StartsWith AndAlso LstToBeSearched.Item(i).StartsWith(StrToGetSubstrAfter) AndAlso LastIndexOfInsteadOfFirst Then '      Start of the string, Last occurance
                    Index = LstToBeSearched.Item(i).LastIndexOf(StrToGetSubstrAfter) + StrToGetSubstrAfter.Length
                End If

                If Index <> -1 Then strResult.Add(LstToBeSearched(i).Substring(Index))
            End If
        Next

        Return strResult
    End Function
#End Region

#Region "ArrayBox"
#Region "T()"
    ' => T()
    Public Function ArrayBox(Of T)(ByVal TVar As T(), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, "", 1, True, TVar, , DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems => T()
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal TVar As T(), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(doNumeriseItems, "", 1, True, TVar, , DefaultSpace)
        Return Result
    End Function
    'DelimitationStr => T()
    Public Function ArrayBox(Of T)(ByVal DelimitationStr As String, ByVal TVar As T(), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, DelimitationStr, 0, True, TVar, , DefaultSpace)
        Return Result
    End Function
    'SplitOnNum => T()
    Public Function ArrayBox(Of T)(ByVal SplitOnNum As UInteger, ByVal TVar As T(), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, "", SplitOnNum, True, TVar, IgnoreDelimitSpace, DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems, DelimitationStr, SplitOnNum, IgnoreNullValues, Var => T()
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal DelimitationStr As String, ByVal SplitOnNum As UInteger, ByVal IgnoreNullValues As Boolean, ByVal TVar As T(), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ", Optional ByVal AlwaysDelimitBeforeNewLine As Boolean = False, Optional ByVal PrefixString As String = "", Optional ByVal SuffixString As String = "", Optional DoNotPrefixIfValueIsNumeric As Boolean = False, Optional DoNotSuffixIfValueIsNumeric As Boolean = False, Optional ByVal AddTwoDoubleQuotesBeforeADoubleQuoteCharacter As Boolean = False, Optional ByVal StrInCaseOfNullValue As String = " ") As String
        Dim sbRet As New StringBuilder

        If TVar IsNot Nothing Then
            For i = 0 To TVar.Length - 1
                If (TVar(i) IsNot Nothing AndAlso Not IsDBNull(TVar(i))) OrElse Not IgnoreNullValues Then   '        If the variable is something
                    Dim StringToBeAppended As String = StrInCaseOfNullValue
                    Try
                        StringToBeAppended = TVar(i).ToString
                    Catch ex As Exception
                    End Try

                    If doNumeriseItems Then sbRet.Append(i + 1).Append(") ") '      Numerise it if user asked it

                    If PrefixString <> "" AndAlso (Not IsNumeric(StringToBeAppended) OrElse Not DoNotPrefixIfValueIsNumeric) Then ' If there is a Prefix set, and value isnt numeric OR, is numeric but prefixing is allowed
                        sbRet.Append(PrefixString) '                                                                                then append it before the actual value
                    End If

                    If Not AddTwoDoubleQuotesBeforeADoubleQuoteCharacter Then '         If we are to proceed normally,
                        sbRet.Append(StringToBeAppended) '                              Print the Value of TVar(i)
                    Else
                        sbRet.Append(StringToBeAppended.Replace("""", """""""")) '      Else, add 2 double-quotes before any double-quote character
                    End If

                    If SuffixString <> "" AndAlso (Not IsNumeric(StringToBeAppended) OrElse Not DoNotSuffixIfValueIsNumeric) Then 'If there is a Suffix set, and value isnt numeric OR, is numeric but suffixing is allowed
                        sbRet.Append(SuffixString) '                                                                                    then append it after the actual value
                    End If

                    If AlwaysDelimitBeforeNewLine AndAlso DelimitationStr <> "" AndAlso i <> TVar.Length - 1 Then '      If Always Delimit And it isn't the last element of all
                        sbRet.Append(DelimitationStr) '                                                                 then Delimit it
                        If Not IgnoreDelimitSpace AndAlso (SplitOnNum = 0 OrElse (i + 1) Mod SplitOnNum <> 0) Then sbRet.Append(DefaultSpace) 'Put a space if a space should be put

                    ElseIf DelimitationStr <> "" AndAlso (SplitOnNum = 0 OrElse (i + 1) Mod SplitOnNum <> 0) AndAlso i <> TVar.Length - 1 Then ' if it should be delimited And it isn't the last element of the line
                        sbRet.Append(DelimitationStr) '                                                                                         then Delimit it
                        If Not IgnoreDelimitSpace Then sbRet.Append(DefaultSpace) '                                                             Put a space if a space should be put
                    End If

                    If SplitOnNum <> 0 AndAlso (i + 1) Mod SplitOnNum = 0 AndAlso i <> TVar.Length - 1 Then 'If you separate them with NewLines and it is the last element of the line
                        sbRet.AppendLine() '                                                                then append a NewLine
                    End If
                End If
            Next

        End If

        Return sbRet.ToString
    End Function
#End Region

#Region "List(Of T)"
    ' => List(Of T)
    Public Function ArrayBox(Of T)(ByVal listTVar As List(Of T), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, "", 1, True, listTVar, , DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems => List(Of T)
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal listTVar As List(Of T), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(doNumeriseItems, "", 1, True, listTVar, , DefaultSpace)
        Return Result
    End Function
    'DelimitationStr => List(Of T)
    Public Function ArrayBox(Of T)(ByVal DelimitationStr As String, ByVal listTVar As List(Of T), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, DelimitationStr, 0, True, listTVar, , DefaultSpace)
        Return Result
    End Function
    'SplitOnNum => List(Of T)
    Public Function ArrayBox(Of T)(ByVal SplitOnNum As UInteger, ByVal listTVar As List(Of T), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, "", SplitOnNum, True, listTVar, IgnoreDelimitSpace, DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems, DelimitationStr, SplitOnNum, IgnoreNullValues, listTVar => List(of T)
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal DelimitationStr As String, ByVal SplitOnNum As UInteger, ByVal IgnoreNullValues As Boolean, ByVal listTVar As List(Of T), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ", Optional ByVal AlwaysDelimitBeforeNewLine As Boolean = False, Optional ByVal PrefixString As String = "", Optional ByVal SuffixString As String = "", Optional DoNotPrefixIfValueIsNumeric As Boolean = False, Optional DoNotSuffixIfValueIsNumeric As Boolean = False, Optional ByVal AddTwoDoubleQuotesBeforeADoubleQuoteCharacter As Boolean = False, Optional ByVal StrInCaseOfNullValue As String = " ") As String
        Dim sbRet As New StringBuilder

        If listTVar IsNot Nothing Then
            For i = 0 To listTVar.Count - 1
                If (listTVar.Item(i) IsNot Nothing AndAlso Not IsDBNull(listTVar.Item(i))) OrElse Not IgnoreNullValues Then   '   If the variable is something
                    Dim StringToBeAppended As String = StrInCaseOfNullValue
                    Try
                        StringToBeAppended = listTVar.Item(i).ToString
                    Catch ex As Exception
                    End Try

                    If doNumeriseItems Then sbRet.Append(i + 1).Append(") ") '      Numerise it if user asked it

                    If PrefixString <> "" AndAlso (Not IsNumeric(StringToBeAppended) OrElse Not DoNotPrefixIfValueIsNumeric) Then ' If there is a Prefix set, and value isnt numeric OR, is numeric but prefixing is allowed
                        sbRet.Append(PrefixString) '                                                                                then append it before the actual value
                    End If

                    If Not AddTwoDoubleQuotesBeforeADoubleQuoteCharacter Then '         If we are to proceed normally,
                        sbRet.Append(StringToBeAppended) '                              Print the Value of listTVar(i)
                    Else
                        sbRet.Append(StringToBeAppended.Replace("""", """""""")) '      Else, add 2 double-quotes before any double-quote character
                    End If

                    If SuffixString <> "" AndAlso (Not IsNumeric(StringToBeAppended) OrElse Not DoNotSuffixIfValueIsNumeric) Then 'If there is a Suffix set, and value isnt numeric OR, is numeric but suffixing is allowed
                        sbRet.Append(SuffixString) '                                                                                    then append it after the actual value
                    End If

                    If AlwaysDelimitBeforeNewLine AndAlso DelimitationStr <> "" AndAlso i <> listTVar.Count - 1 Then '       If Always Delimit And it isn't the last element of all
                        sbRet.Append(DelimitationStr) '                                                                 then Delimit it
                        If Not IgnoreDelimitSpace AndAlso (SplitOnNum = 0 OrElse (i + 1) Mod SplitOnNum <> 0) Then sbRet.Append(DefaultSpace) ' Put a space if a space should be put

                    ElseIf DelimitationStr <> "" AndAlso (SplitOnNum = 0 OrElse (i + 1) Mod SplitOnNum <> 0) AndAlso i <> listTVar.Count - 1 Then '  if it should be delimited And it isn't the last element of the line
                        sbRet.Append(DelimitationStr) '                                                                                         then Delimit it
                        If Not IgnoreDelimitSpace Then sbRet.Append(DefaultSpace) '                                                             Put a space if a space should be put
                    End If

                    If SplitOnNum <> 0 AndAlso (i + 1) Mod SplitOnNum = 0 AndAlso i <> listTVar.Count - 1 Then '  If you separate them with NewLines and it is the last element of the line
                        sbRet.AppendLine() '                                                                then append a NewLine
                    End If
                End If
            Next

        End If

        Return sbRet.ToString
    End Function
#End Region

#Region "IEnumerable(Of T)"
    ' => IEnumerable(Of T)
    Public Function ArrayBox(Of T)(ByVal IETVar As IEnumerable(Of T), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, "", 1, True, IETVar, , DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems => IEnumerable(Of T)
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal IETVar As IEnumerable(Of T), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(doNumeriseItems, "", 1, True, IETVar, , DefaultSpace)
        Return Result
    End Function
    'DelimitationStr => IEnumerable(Of T)
    Public Function ArrayBox(Of T)(ByVal DelimitationStr As String, ByVal IETVar As IEnumerable(Of T), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, DelimitationStr, 0, True, IETVar, , DefaultSpace)
        Return Result
    End Function
    'SplitOnNum => IEnumerable(Of T)
    Public Function ArrayBox(Of T)(ByVal SplitOnNum As UInteger, ByVal IETVar As IEnumerable(Of T), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, "", SplitOnNum, True, IETVar, IgnoreDelimitSpace, DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems, DelimitationStr, SplitOnNum, IgnoreNullValues, Var => IEnumerable(Of T)
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal DelimitationStr As String, ByVal SplitOnNum As UInteger, ByVal IgnoreNullValues As Boolean, ByVal IETVar As IEnumerable(Of T), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ", Optional ByVal AlwaysDelimitBeforeNewLine As Boolean = False, Optional ByVal PrefixString As String = "", Optional ByVal SuffixString As String = "", Optional DoNotPrefixIfValueIsNumeric As Boolean = False, Optional DoNotSuffixIfValueIsNumeric As Boolean = False, Optional ByVal AddTwoDoubleQuotesBeforeADoubleQuoteCharacter As Boolean = False, Optional ByVal StrInCaseOfNullValue As String = " ") As String
        Dim sbRet As New StringBuilder

        If IETVar IsNot Nothing Then
            For i = 0 To IETVar.Count - 1
                If (IETVar(i) IsNot Nothing AndAlso Not IsDBNull(IETVar(i))) OrElse Not IgnoreNullValues Then   '   If the IEVariable is something
                    Dim StringToBeAppended As String = StrInCaseOfNullValue
                    Try
                        StringToBeAppended = IETVar(i).ToString
                    Catch ex As Exception
                    End Try

                    If doNumeriseItems Then sbRet.Append(i + 1).Append(") ") '      Numerise it if user asked it

                    If PrefixString <> "" AndAlso (Not IsNumeric(StringToBeAppended) OrElse Not DoNotPrefixIfValueIsNumeric) Then ' If there is a Prefix set, and value isnt numeric OR, is numeric but prefixing is allowed
                        sbRet.Append(PrefixString) '                                                                                then append it before the actual value
                    End If

                    If Not AddTwoDoubleQuotesBeforeADoubleQuoteCharacter Then '         If we are to proceed normally,
                        sbRet.Append(StringToBeAppended) '                              Print the Value of IETVar(i)
                    Else
                        sbRet.Append(StringToBeAppended.Replace("""", """""""")) '      Else, add 2 double-quotes before any double-quote character
                    End If

                    If SuffixString <> "" AndAlso (Not IsNumeric(StringToBeAppended) OrElse Not DoNotSuffixIfValueIsNumeric) Then 'If there is a Suffix set, and value isnt numeric OR, is numeric but suffixing is allowed
                        sbRet.Append(SuffixString) '                                                                                    then append it after the actual value
                    End If

                    If AlwaysDelimitBeforeNewLine AndAlso DelimitationStr <> "" AndAlso i <> IETVar.Count - 1 Then '       If Always Delimit And it isn't the last element of all
                        sbRet.Append(DelimitationStr) '                                                                 then Delimit it
                        If Not IgnoreDelimitSpace AndAlso (SplitOnNum = 0 OrElse (i + 1) Mod SplitOnNum <> 0) Then sbRet.Append(DefaultSpace) ' Put a space if a space should be put

                    ElseIf DelimitationStr <> "" AndAlso (SplitOnNum = 0 OrElse (i + 1) Mod SplitOnNum <> 0) AndAlso i <> IETVar.Count - 1 Then '  if it should be delimited And it isn't the last element of the line
                        sbRet.Append(DelimitationStr) '                                                                                         then Delimit it
                        If Not IgnoreDelimitSpace Then sbRet.Append(DefaultSpace) '                                                             Put a space if a space should be put
                    End If

                    If SplitOnNum <> 0 AndAlso (i + 1) Mod SplitOnNum = 0 AndAlso i <> IETVar.Count - 1 Then '  If you separate them with NewLines and it is the last element of the line
                        sbRet.AppendLine() '                                                                then append a NewLine
                    End If
                End If
            Next

        End If

        Return sbRet.ToString
    End Function
#End Region

#Region "T1(), T2()"
    ' => [ T1(), T2() ]
    Public Function ArrayBox(Of T)(ByVal TArVar1 As T(), ByVal TArVar2 As T(), Optional ByVal DelimitationStr As String = " ") As String
        Dim Result As String = ArrayBox(False, DelimitationStr, TArVar1, TArVar2)
        Return Result
    End Function
    'doNumeriseItems => [ T1(), T2() ]
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal TArVar1 As T(), ByVal TArVar2 As T(), Optional ByVal DelimitationStr As String = " ") As String
        Dim Result As String = ArrayBox(doNumeriseItems, DelimitationStr, TArVar1, TArVar2)
        Return Result
    End Function
    'DefaultSpace => [ T1(), T2() ]
    Public Function ArrayBox(Of T)(ByVal DelimitationStr As String, ByVal TArVar1 As T(), ByVal TArVar2 As T(), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, DelimitationStr, TArVar1, TArVar2, IgnoreDelimitSpace, DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems, DelimitationStr, SplitOnNum, IgnoreNullValues, var => [ T1(), T2() ]
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal DelimitationStr As String, ByVal TArVar1 As T(), ByVal TArVar2 As T(), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = "", Optional ByVal AlwaysDelimitBeforeNewLine As Boolean = False, Optional ByVal PrefixString As String = "", Optional ByVal SuffixString As String = "", Optional DoNotPrefixIfValueIsNumeric As Boolean = False, Optional DoNotSuffixIfValueIsNumeric As Boolean = False, Optional ByVal AddTwoDoubleQuotesBeforeADoubleQuoteCharacter As Boolean = False, Optional ByVal StrInCaseOfNullValue As String = " ") As String
        Dim sbRet As New StringBuilder
        Dim Var1Length As Integer = TArVar1.Length
        Dim Var2Length As Integer = TArVar2.Length
        Dim MaxLength As Integer
        If Var1Length >= Var2Length Then MaxLength = Var1Length Else MaxLength = Var2Length

        For i = 0 To MaxLength - 1
            If doNumeriseItems Then sbRet.Append(i + 1).Append(") ")

            If TArVar1.Length >= i AndAlso Not IsNothing(TArVar1) AndAlso Not IsDBNull(TArVar1) Then
                If PrefixString <> "" AndAlso (Not DoNotPrefixIfValueIsNumeric OrElse Not IsNumeric(TArVar1(i).ToString)) Then sbRet.Append(PrefixString)

                If Not AddTwoDoubleQuotesBeforeADoubleQuoteCharacter Then
                    sbRet.Append(TArVar1(i).ToString)
                Else
                    sbRet.Append(TArVar1(i).ToString.Replace("""", """"""""))
                End If

                If SuffixString <> "" AndAlso (Not DoNotSuffixIfValueIsNumeric OrElse Not IsNumeric(TArVar1(i).ToString)) Then sbRet.Append(SuffixString)

            Else
                If PrefixString <> "" Then sbRet.Append(PrefixString)
                sbRet.Append(StrInCaseOfNullValue)
                If SuffixString <> "" Then sbRet.Append(SuffixString)
            End If

            If DelimitationStr <> "" Then
                sbRet.Append(DelimitationStr)
                If Not IgnoreDelimitSpace Then sbRet.Append(DefaultSpace)
            End If

            If TArVar2.Length >= i AndAlso Not IsNothing(TArVar2) AndAlso Not IsDBNull(TArVar2) Then
                If PrefixString <> "" AndAlso (Not DoNotPrefixIfValueIsNumeric OrElse Not IsNumeric(TArVar2(i).ToString)) Then sbRet.Append(PrefixString)

                If Not AddTwoDoubleQuotesBeforeADoubleQuoteCharacter Then
                    sbRet.Append(TArVar2(i).ToString)
                Else
                    sbRet.Append(TArVar2(i).ToString.Replace("""", """"""""))
                End If

                If SuffixString <> "" AndAlso (Not DoNotSuffixIfValueIsNumeric OrElse Not IsNumeric(TArVar2(i).ToString)) Then sbRet.Append(SuffixString)

            Else
                If PrefixString <> "" Then sbRet.Append(PrefixString)
                sbRet.Append(StrInCaseOfNullValue)
                If SuffixString <> "" Then sbRet.Append(SuffixString)
            End If

            If AlwaysDelimitBeforeNewLine AndAlso i <> MaxLength - 1 Then sbRet.Append(DelimitationStr)
            If i <> MaxLength - 1 Then sbRet.AppendLine()
        Next

        Return sbRet.ToString
    End Function
#End Region

#Region "List(Of T1), List(Of T2)"
    ' => [ List(Of T1), List(Of T2) ]
    Public Function ArrayBox(Of T)(ByVal Var1 As List(Of T), ByVal ListTVar2 As List(Of T), Optional ByVal DelimitationStr As String = " ") As String
        Dim Result As String = ArrayBox(False, DelimitationStr, Var1, ListTVar2)
        Return Result
    End Function
    'doNumeriseItems => [ List(Of T1), List(Of T2) ]
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal Var1 As List(Of T), ByVal ListTVar2 As List(Of T), Optional ByVal DelimitationStr As String = " ") As String
        Dim Result As String = ArrayBox(doNumeriseItems, DelimitationStr, Var1, ListTVar2)
        Return Result
    End Function
    'DefaultSpace => [ List(Of T1), List(Of T2) ]
    Public Function ArrayBox(Of T)(ByVal DelimitationStr As String, ByVal Var1 As List(Of T), ByVal ListTVar2 As List(Of T), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, DelimitationStr, Var1, ListTVar2, IgnoreDelimitSpace, DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems, DelimitationStr, SplitOnNum, IgnoreNullValues, var => [ List(of T1), List(of T2) ]
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal DelimitationStr As String, ByVal ListTVar1 As List(Of T), ByVal ListTVar2 As List(Of T), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ", Optional ByVal AlwaysDelimitBeforeNewLine As Boolean = False, Optional ByVal PrefixString As String = "", Optional ByVal SuffixString As String = "", Optional DoNotPrefixIfValueIsNumeric As Boolean = False, Optional DoNotSuffixIfValueIsNumeric As Boolean = False, Optional ByVal AddTwoDoubleQuotesBeforeADoubleQuoteCharacter As Boolean = False, Optional ByVal StrInCaseOfNullValue As String = " ") As String
        Dim sbRet As New StringBuilder
        Dim Var1Length As Integer = ListTVar1.Count
        Dim Var2Length As Integer = ListTVar2.Count
        Dim MaxLength As Integer
        If Var1Length >= Var2Length Then MaxLength = Var1Length Else MaxLength = Var2Length

        For i = 0 To MaxLength - 1
            If doNumeriseItems Then sbRet.Append(i + 1).Append(") ")

            If ListTVar1.Count >= i AndAlso Not IsNothing(ListTVar1) AndAlso Not IsDBNull(ListTVar1) Then
                If PrefixString <> "" AndAlso (Not DoNotPrefixIfValueIsNumeric OrElse Not IsNumeric(ListTVar1.Item(i).ToString)) Then sbRet.Append(PrefixString)

                If Not AddTwoDoubleQuotesBeforeADoubleQuoteCharacter Then
                    sbRet.Append(ListTVar1.Item(i).ToString)
                Else
                    sbRet.Append(ListTVar1.Item(i).ToString.Replace("""", """"""""))
                End If

                If SuffixString <> "" AndAlso (Not DoNotSuffixIfValueIsNumeric OrElse Not IsNumeric(ListTVar1.Item(i).ToString)) Then sbRet.Append(SuffixString)

            Else
                If PrefixString <> "" Then sbRet.Append(PrefixString)
                sbRet.Append(StrInCaseOfNullValue)
                If SuffixString <> "" Then sbRet.Append(SuffixString)
            End If

            If DelimitationStr <> "" Then
                sbRet.Append(DelimitationStr)
                If Not IgnoreDelimitSpace Then sbRet.Append(DefaultSpace)
            End If

            If ListTVar2.Count >= i AndAlso Not IsNothing(ListTVar2) AndAlso Not IsDBNull(ListTVar2) Then
                If PrefixString <> "" AndAlso (Not DoNotPrefixIfValueIsNumeric OrElse Not IsNumeric(ListTVar2.Item(i).ToString)) Then sbRet.Append(PrefixString)

                If Not AddTwoDoubleQuotesBeforeADoubleQuoteCharacter Then
                    sbRet.Append(ListTVar2.Item(i).ToString)
                Else
                    sbRet.Append(ListTVar2.Item(i).ToString.Replace("""", """"""""))
                End If

                If SuffixString <> "" AndAlso (Not DoNotSuffixIfValueIsNumeric OrElse Not IsNumeric(ListTVar2.Item(i).ToString)) Then sbRet.Append(SuffixString)

            Else
                If PrefixString <> "" Then sbRet.Append(PrefixString)
                sbRet.Append(StrInCaseOfNullValue)
                If SuffixString <> "" Then sbRet.Append(SuffixString)
            End If

            If AlwaysDelimitBeforeNewLine AndAlso i <> MaxLength - 1 Then sbRet.Append(DelimitationStr)
            If i <> MaxLength - 1 Then sbRet.AppendLine()
        Next

        Return sbRet.ToString
    End Function
#End Region

#Region "IEnumerable(Of T1), IEnumerable(Of T2)"
    ' => [ IEnumerable(Of T1), IEnumerable(Of T2) ]
    Public Function ArrayBox(Of T)(ByVal IETVar1 As IEnumerable(Of T), ByVal IETVar2 As IEnumerable(Of T), Optional ByVal DelimitationStr As String = " ") As String
        Dim Result As String = ArrayBox(False, DelimitationStr, IETVar1, IETVar2)
        Return Result
    End Function
    'doNumeriseItems => [ IEnumerable(Of T1), IEnumerable(Of T2) ]
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal IETVar1 As IEnumerable(Of T), ByVal IETVar2 As IEnumerable(Of T), Optional ByVal DelimitationStr As String = " ") As String
        Dim Result As String = ArrayBox(doNumeriseItems, DelimitationStr, IETVar1, IETVar2)
        Return Result
    End Function
    'DefaultSpace => [ IEnumerable(Of T1), IEnumerable(Of T2) ]
    Public Function ArrayBox(Of T)(ByVal DelimitationStr As String, ByVal IETVar1 As IEnumerable(Of T), ByVal IETVar2 As IEnumerable(Of T), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, DelimitationStr, IETVar1, IETVar2, IgnoreDelimitSpace, DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems, DelimitationStr, SplitOnNum, IgnoreNullValues, var => [ IEnumerable(of T1), IEnumerable(of T2) ]
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal DelimitationStr As String, ByVal IETVar1 As IEnumerable(Of T), ByVal IETVar2 As IEnumerable(Of T), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ", Optional ByVal AlwaysDelimitBeforeNewLine As Boolean = False, Optional ByVal PrefixString As String = "", Optional ByVal SuffixString As String = "", Optional DoNotPrefixIfValueIsNumeric As Boolean = False, Optional DoNotSuffixIfValueIsNumeric As Boolean = False, Optional ByVal AddTwoDoubleQuotesBeforeADoubleQuoteCharacter As Boolean = False, Optional ByVal StrInCaseOfNullValue As String = " ") As String
        Dim sbRet As New StringBuilder
        Dim Var1Length As Integer = IETVar1.Count
        Dim Var2Length As Integer = IETVar2.Count
        Dim MaxLength As Integer
        If Var1Length >= Var2Length Then MaxLength = Var1Length Else MaxLength = Var2Length

        For i = 0 To MaxLength - 1
            If doNumeriseItems Then sbRet.Append(i + 1).Append(") ")

            If IETVar1.Count >= i AndAlso Not IsNothing(IETVar1) AndAlso Not IsDBNull(IETVar1) Then
                If PrefixString <> "" AndAlso (Not DoNotPrefixIfValueIsNumeric OrElse Not IsNumeric(IETVar1(i).ToString)) Then sbRet.Append(PrefixString)

                If Not AddTwoDoubleQuotesBeforeADoubleQuoteCharacter Then
                    sbRet.Append(IETVar1(i).ToString)
                Else
                    sbRet.Append(IETVar1(i).ToString.Replace("""", """"""""))
                End If

                If SuffixString <> "" AndAlso (Not DoNotSuffixIfValueIsNumeric OrElse Not IsNumeric(IETVar1(i).ToString)) Then sbRet.Append(SuffixString)

            Else
                If PrefixString <> "" Then sbRet.Append(PrefixString)
                sbRet.Append(StrInCaseOfNullValue)
                If SuffixString <> "" Then sbRet.Append(SuffixString)
            End If

            If DelimitationStr <> "" Then
                sbRet.Append(DelimitationStr)
                If Not IgnoreDelimitSpace Then sbRet.Append(DefaultSpace)
            End If

            If IETVar2.Count >= i AndAlso Not IsNothing(IETVar2) AndAlso Not IsDBNull(IETVar2) Then
                If PrefixString <> "" AndAlso (Not DoNotPrefixIfValueIsNumeric OrElse Not IsNumeric(IETVar2(i).ToString)) Then sbRet.Append(PrefixString)

                If Not AddTwoDoubleQuotesBeforeADoubleQuoteCharacter Then
                    sbRet.Append(IETVar2(i).ToString)
                Else
                    sbRet.Append(IETVar2(i).ToString.Replace("""", """"""""))
                End If

                If SuffixString <> "" AndAlso (Not DoNotSuffixIfValueIsNumeric OrElse Not IsNumeric(IETVar2(i).ToString)) Then sbRet.Append(SuffixString)

            Else
                If PrefixString <> "" Then sbRet.Append(PrefixString)
                sbRet.Append(StrInCaseOfNullValue)
                If SuffixString <> "" Then sbRet.Append(SuffixString)
            End If

            If AlwaysDelimitBeforeNewLine AndAlso i <> MaxLength - 1 Then sbRet.Append(DelimitationStr)
            If i <> MaxLength - 1 Then sbRet.AppendLine()
        Next

        Return sbRet.ToString
    End Function
#End Region

#Region "T()()"
    ' => [ T()() ]
    Public Function ArrayBox(Of T)(ByVal ArArTVar As T()(), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, "", ArArTVar, , DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems => [ T()() ]
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal ArArTVar As T()(), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(doNumeriseItems, "", ArArTVar, , DefaultSpace)
        Return Result
    End Function
    'DelimitationStr => [ T()() ]
    Public Function ArrayBox(Of T)(ByVal DelimitationStr As String, ByVal ArArTVar As T()(), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, DelimitationStr, ArArTVar, IgnoreDelimitSpace, DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems, DelimitationStr, SplitOnNum, IgnoreNullValues, ArArTVar => [ T()() ]
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal DelimitationStr As String, ByVal ArArTVar As T()(), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ", Optional ByVal AlwaysDelimitBeforeNewLine As Boolean = False, Optional ByVal PrefixString As String = "", Optional ByVal SuffixString As String = "", Optional DoNotPrefixIfValueIsNumeric As Boolean = False, Optional DoNotSuffixIfValueIsNumeric As Boolean = False, Optional ByVal AddTwoDoubleQuotesBeforeADoubleQuoteCharacter As Boolean = False, Optional ByVal StrInCaseOfNullValue As String = " ") As String
        Dim sbRet As New StringBuilder
        Dim LastRowIndex As Integer

        For i = 0 To ArArTVar.Length - 1
            If ArArTVar(i).Length > LastRowIndex Then LastRowIndex = ArArTVar(i).Length '                                     Finding which the index-number of the row with the most elements in it
        Next

        For i = 0 To LastRowIndex - 1
            If doNumeriseItems Then sbRet.Append(i + 1).Append(") ") '                                                                      Numerise the element if it should

            For j = 0 To ArArTVar.Length - 1
                If PrefixString <> "" AndAlso (Not DoNotPrefixIfValueIsNumeric OrElse IsNothing(ArArTVar(j)(i)) OrElse IsDBNull(ArArTVar(j)(i)) OrElse Not IsNumeric(ArArTVar(j)(i).ToString)) Then
                    sbRet.Append(PrefixString) 'If there is a PrefixString and either we don't care whether it is a number or not, or it isn't number anyway, then prefix it
                End If

                If ArArTVar(j).Length > i AndAlso Not IsNothing(ArArTVar(j)(i)) AndAlso Not IsDBNull(ArArTVar(j)(i)) Then 'If there is an element
                    If Not AddTwoDoubleQuotesBeforeADoubleQuoteCharacter Then '                                                             If everything should be printed normally
                        sbRet.Append(ArArTVar(j)(i).ToString) '                                                                             Print the actual element
                    Else
                        sbRet.Append(ArArTVar(j)(i).ToString.Replace("""", """""""")) '                                                     Else put 2 double-quotes b4 every double-quote
                    End If

                Else
                    sbRet.Append(StrInCaseOfNullValue) '         If there isn't then just put a space
                End If

                If SuffixString <> "" AndAlso (Not DoNotSuffixIfValueIsNumeric OrElse IsNothing(ArArTVar(j)(i)) OrElse IsDBNull(ArArTVar(j)(i)) OrElse Not IsNumeric(ArArTVar(j)(i).ToString)) Then
                    sbRet.Append(SuffixString) 'If there is a SuffixString and either we don't care whether it is a number or not, or it isn't number anyway, then Suffix it
                End If

                If DelimitationStr <> "" Then
                    If j <> ArArTVar.Length - 1 OrElse (AlwaysDelimitBeforeNewLine AndAlso i <> LastRowIndex) Then sbRet.Append(DelimitationStr) 'If it isn't the last element on the line, or it is but we should always delimit
                    If j <> ArArTVar.Length - 1 AndAlso Not IgnoreDelimitSpace Then sbRet.Append(DefaultSpace) '                                  Also put a space after delimitation if should be
                End If
            Next

            If i <> LastRowIndex - 1 Then sbRet.AppendLine() '                                                                              New line to separate previous row from new one
        Next

        Return sbRet.ToString
    End Function
#End Region

#Region "List(Of T())"
    ' => [ List(Of T()) ]
    Public Function ArrayBox(Of T)(ByVal listTArVar As List(Of T()), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, "", listTArVar, , DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems => [ List(Of T() ]
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal listTArVar As List(Of T()), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(doNumeriseItems, "", listTArVar, , DefaultSpace)
        Return Result
    End Function
    'DelimitationStr => [ List(Of T() ]
    Public Function ArrayBox(Of T)(ByVal DelimitationStr As String, ByVal listTArVar As List(Of T()), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, DelimitationStr, listTArVar, IgnoreDelimitSpace, DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems, DelimitationStr, SplitOnNum, IgnoreNullValues, listTArVar => [ List(Of T() ]
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal DelimitationStr As String, ByVal listTArVar As List(Of T()), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ", Optional ByVal AlwaysDelimitBeforeNewLine As Boolean = False, Optional ByVal PrefixString As String = "", Optional ByVal SuffixString As String = "", Optional DoNotPrefixIfValueIsNumeric As Boolean = False, Optional DoNotSuffixIfValueIsNumeric As Boolean = False, Optional ByVal AddTwoDoubleQuotesBeforeADoubleQuoteCharacter As Boolean = False, Optional ByVal StrInCaseOfNullValue As String = " ") As String
        Dim sbRet As New StringBuilder
        Dim LastRowIndex As Integer

        For i = 0 To listTArVar.Count - 1
            If listTArVar.Item(i).Length > LastRowIndex Then LastRowIndex = listTArVar.Item(i).Length '                                     Finding which the index-number of the row with the most elements in it
        Next

        For i = 0 To LastRowIndex - 1
            If doNumeriseItems Then sbRet.Append(i + 1).Append(") ") '                                                                      Numerise the element if it should

            For j = 0 To listTArVar.Count - 1
                If PrefixString <> "" AndAlso (Not DoNotPrefixIfValueIsNumeric OrElse IsNothing(listTArVar.Item(j)(i)) OrElse IsDBNull(listTArVar.Item(j)(i)) OrElse Not IsNumeric(listTArVar.Item(j)(i).ToString)) Then
                    sbRet.Append(PrefixString) 'If there is a PrefixString and either we don't care whether it is a number or not, or it isn't number anyway, then prefix it
                End If

                If listTArVar.Item(j).Length > i AndAlso Not IsNothing(listTArVar(j)(i)) AndAlso Not IsDBNull(listTArVar(j)(i)) Then 'If there is an element
                    If Not AddTwoDoubleQuotesBeforeADoubleQuoteCharacter Then '                                                             If everything should be printed normally
                        sbRet.Append(listTArVar.Item(j)(i).ToString) '                                                                             Print the actual element
                    Else
                        sbRet.Append(listTArVar.Item(j)(i).ToString.Replace("""", """""""")) '                                                     Else put 2 double-quotes b4 every double-quote
                    End If

                Else
                    sbRet.Append(StrInCaseOfNullValue) '         If ther isn't then just put a space
                End If

                If SuffixString <> "" AndAlso (Not DoNotSuffixIfValueIsNumeric OrElse IsNothing(listTArVar.Item(j)(i)) OrElse IsDBNull(listTArVar.Item(j)(i)) OrElse Not IsNumeric(listTArVar.Item(j)(i).ToString)) Then
                    sbRet.Append(SuffixString) 'If there is a SuffixString and either we don't care whether it is a number or not, or it isn't number anyway, then Suffix it
                End If

                If DelimitationStr <> "" Then
                    If j <> listTArVar.Count - 1 OrElse (AlwaysDelimitBeforeNewLine AndAlso i <> LastRowIndex) Then sbRet.Append(DelimitationStr) 'If it isnt the last element on the line, or it is but we should always delimit
                    If j <> listTArVar.Count - 1 AndAlso Not IgnoreDelimitSpace Then sbRet.Append(DefaultSpace) '                                  Also put a space after delimitation if should be
                End If
            Next

            If i <> LastRowIndex - 1 Then sbRet.AppendLine() '                                                                              New line to separate previous row from new one
        Next

        Return sbRet.ToString
    End Function
#End Region

#Region "List(Of List(of T))"
    ' => [ List(Of List(of T)) ]
    Public Function ArrayBox(Of T)(ByVal ListListTVar As List(Of List(Of T)), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, "", ListListTVar, , DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems => [ List(Of T() ]
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal ListListTVar As List(Of List(Of T)), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(doNumeriseItems, "", ListListTVar, , DefaultSpace)
        Return Result
    End Function
    'DelimitationStr => [ List(Of T() ]
    Public Function ArrayBox(Of T)(ByVal DelimitationStr As String, ByVal ListListTVar As List(Of List(Of T)), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, DelimitationStr, ListListTVar, IgnoreDelimitSpace, DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems, DelimitationStr, SplitOnNum, IgnoreNullValues, ListListTVar => [ List(Of T() ]
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal DelimitationStr As String, ByVal ListListTVar As List(Of List(Of T)), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ", Optional ByVal AlwaysDelimitBeforeNewLine As Boolean = False, Optional ByVal PrefixString As String = "", Optional ByVal SuffixString As String = "", Optional DoNotPrefixIfValueIsNumeric As Boolean = False, Optional DoNotSuffixIfValueIsNumeric As Boolean = False, Optional ByVal AddTwoDoubleQuotesBeforeADoubleQuoteCharacter As Boolean = False, Optional ByVal StrInCaseOfNullValue As String = " ") As String
        Dim sbRet As New StringBuilder
        Dim LastRowIndex As Integer

        For i = 0 To ListListTVar.Count - 1
            If ListListTVar.Item(i).Count > LastRowIndex Then LastRowIndex = ListListTVar.Item(i).Count '                                     Finding which the index-number of the row with the most elements in it
        Next

        For j = 0 To ListListTVar.Count - 1
            If doNumeriseItems Then sbRet.Append(j + 1).Append(") ") '                                                                      Numerise the element if it should

            For i = 0 To LastRowIndex - 1
                If PrefixString <> "" AndAlso (Not DoNotPrefixIfValueIsNumeric OrElse IsNothing(ListListTVar.Item(j)(i)) OrElse IsDBNull(ListListTVar.Item(j)(i)) OrElse Not IsNumeric(ListListTVar.Item(j)(i).ToString)) Then
                    sbRet.Append(PrefixString) 'If there is a PrefixString and either we don't care whether it is a number or not, or it isn't number anyway, then prefix it
                End If

                If ListListTVar.Item(j).Count > i AndAlso Not IsNothing(ListListTVar(j)(i)) AndAlso Not IsDBNull(ListListTVar(j)(i)) Then 'If there is an element
                    If Not AddTwoDoubleQuotesBeforeADoubleQuoteCharacter Then '                                                             If everything should be printed normally
                        sbRet.Append(ListListTVar.Item(j)(i).ToString) '                                                                             Print the actual element
                    Else
                        sbRet.Append(ListListTVar.Item(j)(i).ToString.Replace("""", """""""")) '                                                     Else put 2 double-quotes b4 every double-quote
                    End If

                Else
                    sbRet.Append(StrInCaseOfNullValue) '         If there isn't then just put a space
                End If

                If SuffixString <> "" AndAlso (Not DoNotSuffixIfValueIsNumeric OrElse IsNothing(ListListTVar.Item(j)(i)) OrElse IsDBNull(ListListTVar.Item(j)(i)) OrElse Not IsNumeric(ListListTVar.Item(j)(i).ToString)) Then
                    sbRet.Append(SuffixString) 'If there is a SuffixString and either we don't care whether it is a number or not, or it isn't number anyway, then Suffix it
                End If

                If DelimitationStr <> "" Then
                    If i <> LastRowIndex - 1 OrElse (AlwaysDelimitBeforeNewLine AndAlso i <> LastRowIndex) Then sbRet.Append(DelimitationStr) 'If it isn't the last element on the line, or it is but we should always delimit
                    If i <> LastRowIndex - 1 AndAlso Not IgnoreDelimitSpace Then sbRet.Append(DefaultSpace) '                                  Also put a space after delimitation if should be
                End If
            Next

            If j <> ListListTVar.Count - 1 Then sbRet.AppendLine() '                                                                              New line to separate previous row from new one
        Next

        Return sbRet.ToString
    End Function
#End Region

#Region "IEnumerable(Of T())"
    ' => [ IEnumerable(Of T()) ]
    Public Function ArrayBox(Of T)(ByVal IEArTVar As IEnumerable(Of T()), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, "", IEArTVar, , DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems => [ IEnumerable(Of T() ]
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal IEArTVar As IEnumerable(Of T()), Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(doNumeriseItems, "", IEArTVar, , DefaultSpace)
        Return Result
    End Function
    'DelimitationStr => [ IEnumerable(Of T() ]
    Public Function ArrayBox(Of T)(ByVal DelimitationStr As String, ByVal IEArTVar As IEnumerable(Of T()), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ") As String
        Dim Result As String = ArrayBox(False, DelimitationStr, IEArTVar, IgnoreDelimitSpace, DefaultSpace)
        Return Result
    End Function
    'doNumeriseItems, DelimitationStr, SplitOnNum, IgnoreNullValues, IEArTVar => [ IEnumerable(Of T() ]
    Public Function ArrayBox(Of T)(ByVal doNumeriseItems As Boolean, ByVal DelimitationStr As String, ByVal IEArTVar As IEnumerable(Of T()), Optional ByVal IgnoreDelimitSpace As Boolean = False, Optional ByVal DefaultSpace As String = " ", Optional ByVal AlwaysDelimitBeforeNewLine As Boolean = False, Optional ByVal PrefixString As String = "", Optional ByVal SuffixString As String = "", Optional DoNotPrefixIfValueIsNumeric As Boolean = False, Optional DoNotSuffixIfValueIsNumeric As Boolean = False, Optional ByVal AddTwoDoubleQuotesBeforeADoubleQuoteCharacter As Boolean = False, Optional ByVal StrInCaseOfNullValue As String = " ") As String
        Dim sbRet As New StringBuilder
        Dim LastRowIndex As Integer

        For i = 0 To IEArTVar.Count - 1
            If IEArTVar(i).Length > LastRowIndex Then LastRowIndex = IEArTVar(i).Length '                                     Finding which the index-number of the row with the most elements in it
        Next

        For i = 0 To LastRowIndex - 1
            If doNumeriseItems Then sbRet.Append(i + 1).Append(") ") '                                                                      Numerise the element if it should

            For j = 0 To IEArTVar.Count - 1
                If PrefixString <> "" AndAlso (Not DoNotPrefixIfValueIsNumeric OrElse IsNothing(IEArTVar(j)(i)) OrElse IsDBNull(IEArTVar(j)(i)) OrElse Not IsNumeric(IEArTVar(j)(i).ToString)) Then
                    sbRet.Append(PrefixString) 'If there is a PrefixString and either we don't care whether it is a number or not, or it isn't number anyway, then prefix it
                End If

                If IEArTVar(j).Length > i AndAlso Not IsNothing(IEArTVar(j)(i)) AndAlso Not IsDBNull(IEArTVar(j)(i)) Then 'If there is an element
                    If Not AddTwoDoubleQuotesBeforeADoubleQuoteCharacter Then '                                                             If everything should be printed normally
                        sbRet.Append(IEArTVar(j)(i).ToString) '                                                                             Print the actual element
                    Else
                        sbRet.Append(IEArTVar(j)(i).ToString.Replace("""", """""""")) '                                                     Else put 2 double-quotes b4 every double-quote
                    End If

                Else
                    sbRet.Append(StrInCaseOfNullValue) '         If ther isn't then just put a space
                End If

                If SuffixString <> "" AndAlso (Not DoNotSuffixIfValueIsNumeric OrElse IsNothing(IEArTVar(j)(i)) OrElse IsDBNull(IEArTVar(j)(i)) OrElse Not IsNumeric(IEArTVar(j)(i).ToString)) Then
                    sbRet.Append(SuffixString) 'If there is a SuffixString and either we don't care whether it is a number or not, or it isn't number anyway, then Suffix it
                End If

                If DelimitationStr <> "" Then
                    If j <> IEArTVar.Count - 1 OrElse (AlwaysDelimitBeforeNewLine AndAlso i <> LastRowIndex) Then sbRet.Append(DelimitationStr) 'If it isnt the last element on the line, or it is but we should always delimit
                    If j <> IEArTVar.Count - 1 AndAlso Not IgnoreDelimitSpace Then sbRet.Append(DefaultSpace) '                                  Also put a space after delimitation if should be
                End If
            Next

            If i <> LastRowIndex - 1 Then sbRet.AppendLine() '                                                                              New line to separate previous row from new one
        Next

        Return sbRet.ToString
    End Function
#End Region
#End Region

#Region "TypeBox"

#Region "String"
    ''' <summary>
    ''' Text (String Array)
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedStringAr"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="isNumericOnly"></param>
    ''' <param name="MustntStartWthNum"></param>
    ''' <param name="MinimumNumOrLength"></param>
    ''' <param name="MaximumNumOrLength"></param>
    ''' <param name="EraseNullLines"></param>
    ''' <param name="PreLoadString"></param>
    ''' <param name="IsRange"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="MinLengthPerLine"></param>
    ''' <param name="MaxLengthPerLine"></param>
    ''' <param name="ReturnedStrDoubleArrayOne"></param>
    ''' <param name="ReturnedStrDoubleArrayTwo"></param>
    ''' <param name="RoundNum"></param>
    ''' <param name="MinNumLength"></param>
    ''' <param name="BecameZeroByRounding"></param>
    ''' <returns>Boolean Succeeded, ByRef StringArray</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedStringAr() As String, ByVal AllowNothingAsResult As Boolean, Optional ByVal Title As String = "",
        Optional ByVal isNumericOnly As Boolean = False, Optional ByVal MustntStartWthNum As Boolean = False,
        Optional ByVal MinimumNumOrLength As Double = Double.MinValue, Optional ByVal MaximumNumOrLength As Double = Double.MinValue, Optional ByVal EraseNullLines As Boolean = False, Optional ByVal PreLoadString() As String = Nothing,
        Optional ByVal IsRange As Boolean = False, Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True,
        Optional ByVal MinLengthPerLine As Double = Double.MinValue, Optional ByVal MaxLengthPerLine As Double = Double.MinValue,
        Optional ByRef ReturnedStrDoubleArrayOne() As String = Nothing, Optional ByRef ReturnedStrDoubleArrayTwo() As String = Nothing,
        Optional ByVal RoundNum As Integer = -1, Optional MinNumLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Text
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.isNumericOnly = isNumericOnly
            dlg.MustntStartWthNum = MustntStartWthNum
            dlg.MinimumValidTextSize = MinimumNumOrLength
            dlg.MaximumValidTextSize = MaximumNumOrLength
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = IsRange
            dlg.doEraseNullLines = EraseNullLines
            dlg.PreLoadText = PreLoadString
            dlg.MinimumValidTextSizePerLine = MinLengthPerLine
            dlg.MaximumValidTextSizePerLine = MaxLengthPerLine
            dlg.RoundNum = RoundNum
            dlg.MinNumLength = MinNumLength

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso dlg.txtInput.Text = "" Then
                    ReturnedStringAr = Nothing
                    ReturnedStrDoubleArrayOne = Nothing
                    ReturnedStrDoubleArrayTwo = Nothing

                Else
                    ReturnedStringAr = dlg.txtInput.Lines 'Both when it is numeric, and when it is not, because not the actual double, but the text that represents a double could be needed (i.e. "6/7")

                    If isNumericOnly Then
                        ReturnedStrDoubleArrayOne = dlg.ReturnedStrDoubleArrayOne
                        If IsRange Then ReturnedStrDoubleArrayTwo = dlg.ReturnedStrDoubleArrayTwo
                        BecameZeroByRounding = dlg.AtLeastOneBecameZeroByRounding
                    End If
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Text (String)
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedString"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="isNumericOnly"></param>
    ''' <param name="MustntStartWthNum"></param>
    ''' <param name="MinimumNumOrLength"></param>
    ''' <param name="MaximumNumOrLength"></param>
    ''' <param name="Multiline"></param>
    ''' <param name="EraseNullLines"></param>
    ''' <param name="PreLoadString"></param>
    ''' <param name="IsRange"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="ReturnedStrDoubleOne"></param>
    ''' <param name="ReturnedStrDoubleTwo"></param>
    ''' <param name="RoundNum"></param>
    ''' <param name="MinNumLength"></param>
    ''' <param name="BecameZeroByRounding"></param>
    ''' <returns>Boolean Succeeded, ByRef String</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedString As String, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal isNumericOnly As Boolean = False, Optional ByVal MustntStartWthNum As Boolean = False,
        Optional ByVal MinimumNumOrLength As Double = Double.MinValue, Optional ByVal MaximumNumOrLength As Double = Double.MinValue, Optional ByVal Multiline As Boolean = False,
        Optional ByVal EraseNullLines As Boolean = False, Optional ByVal PreLoadString() As String = Nothing, Optional ByVal IsRange As Boolean = False,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True,
        Optional ByRef ReturnedStrDoubleOne As String = Nothing, Optional ByRef ReturnedStrDoubleTwo As String = Nothing,
        Optional ByVal RoundNum As Integer = -1, Optional MinNumLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Text
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.isNumericOnly = isNumericOnly
            dlg.MustntStartWthNum = MustntStartWthNum
            dlg.MinimumValidTextSize = MinimumNumOrLength
            dlg.MaximumValidTextSize = MaximumNumOrLength
            dlg.MultiLine = Multiline
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = IsRange
            dlg.doEraseNullLines = EraseNullLines
            dlg.PreLoadText = PreLoadString
            dlg.RoundNum = RoundNum
            dlg.MinNumLength = MinNumLength

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso dlg.txtInput.Text = "" Then 'The if isn't needed, but it is to demonstrate that "AllowNothingAsResult" is implemented
                    ReturnedString = String.Empty
                    ReturnedStrDoubleOne = String.Empty
                    ReturnedStrDoubleTwo = String.Empty

                Else
                    ReturnedString = dlg.txtInput.Text 'Both when it is numeric, and when it is not, because not the actual double, but the text that represents a double could be needed (i.e. "6/7")

                    If isNumericOnly Then
                        ReturnedStrDoubleOne = dlg.ReturnedStrDoubleOne
                        If IsRange Then ReturnedStrDoubleTwo = dlg.ReturnedStrDoubleTwo
                        BecameZeroByRounding = dlg.AtLeastOneBecameZeroByRounding
                    End If
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function
#End Region

#Region "Double"
    ''' <summary>
    ''' Double Value
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidDouble"></param>
    ''' <param name="MaximumValidDouble"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="RoundNum"></param>
    ''' <param name="MinNumLength"></param>
    ''' <param name="BecameZeroByRounding"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Double Value</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum As Double, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidDouble As Double = Double.MinValue, Optional ByVal MaximumValidDouble As Double = Double.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True,
        Optional ByVal RoundNum As Integer = -1, Optional MinNumLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Double
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidDouble = MinimumValidDouble
            dlg.MaximumValidDouble = MaximumValidDouble
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.RoundNum = RoundNum
            dlg.MinNumLength = MinNumLength
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Double.MinValue)) Then
                    ReturnedNum = Double.MinValue
                Else
                    ReturnedNum = dlg.ReturnedDoubleOne
                    BecameZeroByRounding = dlg.AtLeastOneBecameZeroByRounding
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Double Range
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidDouble"></param>
    ''' <param name="MaximumValidDouble"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="RoundNum"></param>
    ''' <param name="MinNumLength"></param>
    ''' <param name="BecameZeroByRounding"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Double Range</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin As Double, ByRef ReturnedNumEnd As Double, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidDouble As Double = Double.MinValue, Optional ByVal MaximumValidDouble As Double = Double.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True,
        Optional ByVal RoundNum As Integer = -1, Optional MinNumLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Double
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidDouble = MinimumValidDouble
            dlg.MaximumValidDouble = MaximumValidDouble
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.RoundNum = RoundNum
            dlg.MinNumLength = MinNumLength
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Double.MinValue)) Then
                    ReturnedNumBegin = Double.MinValue
                    ReturnedNumEnd = Double.MinValue
                Else
                    ReturnedNumBegin = dlg.ReturnedDoubleOne
                    ReturnedNumEnd = dlg.ReturnedDoubleTwo
                    BecameZeroByRounding = dlg.AtLeastOneBecameZeroByRounding
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Double Value Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidDouble"></param>
    ''' <param name="MaximumValidDouble"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="RoundNum"></param>
    ''' <param name="MinNumLength"></param>
    ''' <param name="BecameZeroByRounding"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Double Value Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum() As Double, ByVal AllowNothingAsResult As Boolean, Optional ByVal Title As String = "",
        Optional ByVal MinimumValidDouble As Double = Double.MinValue, Optional ByVal MaximumValidDouble As Double = Double.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True,
        Optional ByVal RoundNum As Integer = -1, Optional MinNumLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Double
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidDouble = MinimumValidDouble
            dlg.MaximumValidDouble = MaximumValidDouble
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.RoundNum = RoundNum
            dlg.MinNumLength = MinNumLength
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Double.MinValue)) Then
                    ReturnedNum = Nothing
                Else
                    ReturnedNum = dlg.ReturnedDoubleArrayOne
                    BecameZeroByRounding = dlg.AtLeastOneBecameZeroByRounding
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Double Range Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidDouble"></param>
    ''' <param name="MaximumValidDouble"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="RoundNum"></param>
    ''' <param name="MinNumLength"></param>
    ''' <param name="BecameZeroByRounding"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Double Range Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin() As Double, ByRef ReturnedNumEnd() As Double, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidDouble As Double = Double.MinValue, Optional ByVal MaximumValidDouble As Double = Double.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True,
        Optional ByVal RoundNum As Integer = -1, Optional MinNumLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Double
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidDouble = MinimumValidDouble
            dlg.MaximumValidDouble = MaximumValidDouble
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.RoundNum = RoundNum
            dlg.MinNumLength = MinNumLength
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Double.MinValue)) Then
                    ReturnedNumBegin = Nothing
                    ReturnedNumEnd = Nothing
                Else
                    ReturnedNumBegin = dlg.ReturnedDoubleArrayOne
                    ReturnedNumEnd = dlg.ReturnedDoubleArrayTwo
                    BecameZeroByRounding = dlg.AtLeastOneBecameZeroByRounding
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function
#End Region

#Region "Decimal"
    ''' <summary>
    ''' Decimal Value
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidDecimal"></param>
    ''' <param name="MaximumValidDecimal"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="RoundNum"></param>
    ''' <param name="MinNumLength"></param>
    ''' <param name="BecameZeroByRounding"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Decimal Value</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum As Decimal, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidDecimal As Decimal = Decimal.MinValue, Optional ByVal MaximumValidDecimal As Decimal = Decimal.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True,
        Optional ByVal RoundNum As Integer = -1, Optional MinNumLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Decimal
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidDecimal = MinimumValidDecimal
            dlg.MaximumValidDecimal = MaximumValidDecimal
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.RoundNum = RoundNum
            dlg.MinNumLength = MinNumLength
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Decimal.MinValue)) Then
                    ReturnedNum = Decimal.MinValue
                Else
                    ReturnedNum = dlg.ReturnedDecimalOne
                    BecameZeroByRounding = dlg.AtLeastOneBecameZeroByRounding
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Decimal Range
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidDecimal"></param>
    ''' <param name="MaximumValidDecimal"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="RoundNum"></param>
    ''' <param name="MinNumLength"></param>
    ''' <param name="BecameZeroByRounding"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Decimal Range</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin As Decimal, ByRef ReturnedNumEnd As Decimal, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidDecimal As Decimal = Decimal.MinValue, Optional ByVal MaximumValidDecimal As Decimal = Decimal.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True,
        Optional ByVal RoundNum As Integer = -1, Optional MinNumLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Decimal
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidDecimal = MinimumValidDecimal
            dlg.MaximumValidDecimal = MaximumValidDecimal
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.RoundNum = RoundNum
            dlg.MinNumLength = MinNumLength
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Decimal.MinValue)) Then
                    ReturnedNumBegin = Decimal.MinValue
                    ReturnedNumEnd = Decimal.MinValue
                Else
                    ReturnedNumBegin = dlg.ReturnedDecimalOne
                    ReturnedNumEnd = dlg.ReturnedDecimalTwo
                    BecameZeroByRounding = dlg.AtLeastOneBecameZeroByRounding
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Decimal Value Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidDecimal"></param>
    ''' <param name="MaximumValidDecimal"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="RoundNum"></param>
    ''' <param name="MinNumLength"></param>
    ''' <param name="BecameZeroByRounding"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Decimal Value Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum() As Decimal, ByVal AllowNothingAsResult As Boolean, Optional ByVal Title As String = "",
        Optional ByVal MinimumValidDecimal As Decimal = Decimal.MinValue, Optional ByVal MaximumValidDecimal As Decimal = Decimal.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True,
        Optional ByVal RoundNum As Integer = -1, Optional MinNumLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Decimal
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidDecimal = MinimumValidDecimal
            dlg.MaximumValidDecimal = MaximumValidDecimal
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.RoundNum = RoundNum
            dlg.MinNumLength = MinNumLength
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Decimal.MinValue)) Then
                    ReturnedNum = Nothing
                Else
                    ReturnedNum = dlg.ReturnedDecimalArrayOne
                    BecameZeroByRounding = dlg.AtLeastOneBecameZeroByRounding
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Decimal Range Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidDecimal"></param>
    ''' <param name="MaximumValidDecimal"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="RoundNum"></param>
    ''' <param name="MinNumLength"></param>
    ''' <param name="BecameZeroByRounding"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Decimal Range Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin() As Decimal, ByRef ReturnedNumEnd() As Decimal, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidDecimal As Decimal = Decimal.MinValue, Optional ByVal MaximumValidDecimal As Decimal = Decimal.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True,
        Optional ByVal RoundNum As Integer = -1, Optional MinNumLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Decimal
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidDecimal = MinimumValidDecimal
            dlg.MaximumValidDecimal = MaximumValidDecimal
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.RoundNum = RoundNum
            dlg.MinNumLength = MinNumLength
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Decimal.MinValue)) Then
                    ReturnedNumBegin = Nothing
                    ReturnedNumEnd = Nothing
                Else
                    ReturnedNumBegin = dlg.ReturnedDecimalArrayOne
                    ReturnedNumEnd = dlg.ReturnedDecimalArrayTwo
                    BecameZeroByRounding = dlg.AtLeastOneBecameZeroByRounding
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function
#End Region

#Region "Single"
    ''' <summary>
    ''' Single Value
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidSingle"></param>
    ''' <param name="MaximumValidSingle"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="RoundNum"></param>
    ''' <param name="MinNumLength"></param>
    ''' <param name="BecameZeroByRounding"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Single Value</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum As Single, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidSingle As Single = Single.MinValue, Optional ByVal MaximumValidSingle As Single = Single.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True,
        Optional ByVal RoundNum As Integer = -1, Optional MinNumLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Single
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidSingle = MinimumValidSingle
            dlg.MaximumValidSingle = MaximumValidSingle
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.RoundNum = RoundNum
            dlg.MinNumLength = MinNumLength
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Single.MinValue)) Then
                    ReturnedNum = Single.MinValue
                Else
                    ReturnedNum = dlg.ReturnedSingleOne
                    BecameZeroByRounding = dlg.AtLeastOneBecameZeroByRounding
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Single Range
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidSingle"></param>
    ''' <param name="MaximumValidSingle"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="RoundNum"></param>
    ''' <param name="MinNumLength"></param>
    ''' <param name="BecameZeroByRounding"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, Single Range</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin As Single, ByRef ReturnedNumEnd As Single, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidSingle As Single = Single.MinValue, Optional ByVal MaximumValidSingle As Single = Single.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True,
        Optional ByVal RoundNum As Integer = -1, Optional MinNumLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Single
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidSingle = MinimumValidSingle
            dlg.MaximumValidSingle = MaximumValidSingle
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.RoundNum = RoundNum
            dlg.MinNumLength = MinNumLength
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Single.MinValue)) Then
                    ReturnedNumBegin = Single.MinValue
                    ReturnedNumEnd = Single.MinValue
                Else
                    ReturnedNumBegin = dlg.ReturnedSingleOne
                    ReturnedNumEnd = dlg.ReturnedSingleTwo
                    BecameZeroByRounding = dlg.AtLeastOneBecameZeroByRounding
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Single Value Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidSingle"></param>
    ''' <param name="MaximumValidSingle"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="RoundNum"></param>
    ''' <param name="MinNumLength"></param>
    ''' <param name="BecameZeroByRounding"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Single Value Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum() As Single, ByVal AllowNothingAsResult As Boolean, Optional ByVal Title As String = "",
        Optional ByVal MinimumValidSingle As Single = Single.MinValue, Optional ByVal MaximumValidSingle As Single = Single.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True,
        Optional ByVal RoundNum As Integer = -1, Optional MinNumLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Single
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidSingle = MinimumValidSingle
            dlg.MaximumValidSingle = MaximumValidSingle
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.RoundNum = RoundNum
            dlg.MinNumLength = MinNumLength
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Single.MinValue)) Then
                    ReturnedNum = Nothing
                Else
                    ReturnedNum = dlg.ReturnedSingleArrayOne
                    BecameZeroByRounding = dlg.AtLeastOneBecameZeroByRounding
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Single Range Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidSingle"></param>
    ''' <param name="MaximumValidSingle"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="RoundNum"></param>
    ''' <param name="MinNumLength"></param>
    ''' <param name="BecameZeroByRounding"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Single Range Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin() As Single, ByRef ReturnedNumEnd() As Single, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidSingle As Single = Single.MinValue, Optional ByVal MaximumValidSingle As Single = Single.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True,
        Optional ByVal RoundNum As Integer = -1, Optional MinNumLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Single
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidSingle = MinimumValidSingle
            dlg.MaximumValidSingle = MaximumValidSingle
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.RoundNum = RoundNum
            dlg.MinNumLength = MinNumLength
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Single.MinValue)) Then
                    ReturnedNumBegin = Nothing
                    ReturnedNumEnd = Nothing
                Else
                    ReturnedNumBegin = dlg.ReturnedSingleArrayOne
                    ReturnedNumEnd = dlg.ReturnedSingleArrayTwo
                    BecameZeroByRounding = dlg.AtLeastOneBecameZeroByRounding
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function
#End Region

#Region "Integer"
    ''' <summary>
    ''' Integer Value
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumNumber"></param>
    ''' <param name="MaximumNumber"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Integer Value</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum As Integer, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumNumber As Integer = Integer.MinValue, Optional ByVal MaximumNumber As Integer = Integer.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Integer
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidInteger = MinimumNumber
            dlg.MaximumValidInteger = MaximumNumber
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Integer.MinValue)) Then
                    ReturnedNum = Integer.MinValue
                Else
                    ReturnedNum = dlg.ReturnedIntegerOne
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Integer Range
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidInteger"></param>
    ''' <param name="MaximumValidInteger"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Integer Range</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin As Integer, ByRef ReturnedNumEnd As Integer, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidInteger As Integer = Integer.MinValue, Optional ByVal MaximumValidInteger As Integer = Integer.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Integer
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidInteger = MinimumValidInteger
            dlg.MaximumValidInteger = MaximumValidInteger
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Integer.MinValue)) Then
                    ReturnedNumBegin = Integer.MinValue
                    ReturnedNumEnd = Integer.MinValue
                Else
                    ReturnedNumBegin = dlg.ReturnedIntegerOne
                    ReturnedNumEnd = dlg.ReturnedIntegerTwo
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Integer Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidInteger"></param>
    ''' <param name="MaximumValidInteger"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Integer Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum() As Integer, ByVal AllowNothingAsResult As Boolean, Optional ByVal Title As String = "",
        Optional ByVal MinimumValidInteger As Integer = Integer.MinValue, Optional ByVal MaximumValidInteger As Integer = Integer.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Integer
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidInteger = MinimumValidInteger
            dlg.MaximumValidInteger = MaximumValidInteger
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Integer.MinValue)) Then
                    ReturnedNum = Nothing
                Else
                    ReturnedNum = dlg.ReturnedIntegerArrayOne
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Integer Range Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidInteger"></param>
    ''' <param name="MaximumValidInteger"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Integer Range Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin() As Integer, ByRef ReturnedNumEnd() As Integer, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidInteger As Integer = Integer.MinValue, Optional ByVal MaximumValidInteger As Integer = Integer.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Integer
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidInteger = MinimumValidInteger
            dlg.MaximumValidInteger = MaximumValidInteger
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Integer.MinValue)) Then
                    ReturnedNumBegin = Nothing
                    ReturnedNumEnd = Nothing
                Else
                    ReturnedNumBegin = dlg.ReturnedIntegerArrayOne
                    ReturnedNumEnd = dlg.ReturnedIntegerArrayTwo
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function
#End Region

#Region "UInteger"
    ''' <summary>
    ''' UInteger Value
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumNumber"></param>
    ''' <param name="MaximumNumber"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef UInteger Value</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum As UInteger, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumNumber As UInteger = UInteger.MaxValue, Optional ByVal MaximumNumber As UInteger = UInteger.MaxValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._UInteger
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidUInteger = MinimumNumber
            dlg.MaximumValidUInteger = MaximumNumber
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(-1)) Then
                    ReturnedNum = UInteger.MaxValue
                Else
                    ReturnedNum = dlg.ReturnedUIntegerOne
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' UInteger Range
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidUInteger"></param>
    ''' <param name="MaximumValidUInteger"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef UInteger Range</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin As UInteger, ByRef ReturnedNumEnd As UInteger, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidUInteger As UInteger = UInteger.MaxValue, Optional ByVal MaximumValidUInteger As UInteger = UInteger.MaxValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._UInteger
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidUInteger = MinimumValidUInteger
            dlg.MaximumValidUInteger = MaximumValidUInteger
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(-1)) Then
                    ReturnedNumBegin = UInteger.MaxValue
                    ReturnedNumEnd = UInteger.MaxValue
                Else
                    ReturnedNumBegin = dlg.ReturnedUIntegerOne
                    ReturnedNumEnd = dlg.ReturnedUIntegerTwo
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' UInteger Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidUInteger"></param>
    ''' <param name="MaximumValidUInteger"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef UInteger Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum() As UInteger, ByVal AllowNothingAsResult As Boolean, Optional ByVal Title As String = "",
        Optional ByVal MinimumValidUInteger As UInteger = UInteger.MaxValue, Optional ByVal MaximumValidUInteger As UInteger = UInteger.MaxValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._UInteger
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidUInteger = MinimumValidUInteger
            dlg.MaximumValidUInteger = MaximumValidUInteger
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(-1)) Then
                    ReturnedNum = Nothing
                Else
                    ReturnedNum = dlg.ReturnedUIntegerArrayOne
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' UInteger Range Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidUInteger"></param>
    ''' <param name="MaximumValidUInteger"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef UInteger Range Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin() As UInteger, ByRef ReturnedNumEnd() As UInteger, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidUInteger As UInteger = UInteger.MaxValue, Optional ByVal MaximumValidUInteger As UInteger = UInteger.MaxValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._UInteger
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidUInteger = MinimumValidUInteger
            dlg.MaximumValidUInteger = MaximumValidUInteger
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(-1)) Then
                    ReturnedNumBegin = Nothing
                    ReturnedNumEnd = Nothing
                Else
                    ReturnedNumBegin = dlg.ReturnedUIntegerArrayOne
                    ReturnedNumEnd = dlg.ReturnedUIntegerArrayTwo
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function
#End Region

#Region "Short"
    ''' <summary>
    ''' Short Value
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumNumber"></param>
    ''' <param name="MaximumNumber"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Short Value</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum As Short, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumNumber As Short = Short.MinValue, Optional ByVal MaximumNumber As Short = Short.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Short
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidShort = MinimumNumber
            dlg.MaximumValidShort = MaximumNumber
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Short.MinValue)) Then
                    ReturnedNum = Short.MinValue
                Else
                    ReturnedNum = dlg.ReturnedShortOne
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Short Range
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidShort"></param>
    ''' <param name="MaximumValidShort"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Short Range</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin As Short, ByRef ReturnedNumEnd As Short, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidShort As Short = Short.MinValue, Optional ByVal MaximumValidShort As Short = Short.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Short
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidShort = MinimumValidShort
            dlg.MaximumValidShort = MaximumValidShort
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Short.MinValue)) Then
                    ReturnedNumBegin = Short.MinValue
                    ReturnedNumEnd = Short.MinValue
                Else
                    ReturnedNumBegin = dlg.ReturnedShortOne
                    ReturnedNumEnd = dlg.ReturnedShortTwo
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Short Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidShort"></param>
    ''' <param name="MaximumValidShort"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Short Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum() As Short, ByVal AllowNothingAsResult As Boolean, Optional ByVal Title As String = "",
        Optional ByVal MinimumValidShort As Short = Short.MinValue, Optional ByVal MaximumValidShort As Short = Short.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Short
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidShort = MinimumValidShort
            dlg.MaximumValidShort = MaximumValidShort
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Short.MinValue)) Then
                    ReturnedNum = Nothing
                Else
                    ReturnedNum = dlg.ReturnedShortArrayOne
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Short Range Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidShort"></param>
    ''' <param name="MaximumValidShort"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Short Range Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin() As Short, ByRef ReturnedNumEnd() As Short, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidShort As Short = Short.MinValue, Optional ByVal MaximumValidShort As Short = Short.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Short
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidShort = MinimumValidShort
            dlg.MaximumValidShort = MaximumValidShort
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Short.MinValue)) Then
                    ReturnedNumBegin = Nothing
                    ReturnedNumEnd = Nothing
                Else
                    ReturnedNumBegin = dlg.ReturnedShortArrayOne
                    ReturnedNumEnd = dlg.ReturnedShortArrayTwo
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function
#End Region

#Region "UShort"
    ''' <summary>
    ''' UShort Value
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumNumber"></param>
    ''' <param name="MaximumNumber"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef UShort Value</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum As UShort, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumNumber As UShort = UShort.MaxValue, Optional ByVal MaximumNumber As UShort = UShort.MaxValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._UShort
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidUShort = MinimumNumber
            dlg.MaximumValidUShort = MaximumNumber
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(-1)) Then
                    ReturnedNum = UShort.MaxValue
                Else
                    ReturnedNum = dlg.ReturnedUShortOne
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' UShort Range
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidUShort"></param>
    ''' <param name="MaximumValidUShort"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef UShort Range</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin As UShort, ByRef ReturnedNumEnd As UShort, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidUShort As UShort = UShort.MaxValue, Optional ByVal MaximumValidUShort As UShort = UShort.MaxValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._UShort
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidUShort = MinimumValidUShort
            dlg.MaximumValidUShort = MaximumValidUShort
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(-1)) Then
                    ReturnedNumBegin = UShort.MaxValue
                    ReturnedNumEnd = UShort.MaxValue
                Else
                    ReturnedNumBegin = dlg.ReturnedUShortOne
                    ReturnedNumEnd = dlg.ReturnedUShortTwo
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' UShort Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidUShort"></param>
    ''' <param name="MaximumValidUShort"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef UShort Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum() As UShort, ByVal AllowNothingAsResult As Boolean, Optional ByVal Title As String = "",
        Optional ByVal MinimumValidUShort As UShort = UShort.MaxValue, Optional ByVal MaximumValidUShort As UShort = UShort.MaxValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._UShort
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidUShort = MinimumValidUShort
            dlg.MaximumValidUShort = MaximumValidUShort
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(-1)) Then
                    ReturnedNum = Nothing
                Else
                    ReturnedNum = dlg.ReturnedUShortArrayOne
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' UShort Range Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidUShort"></param>
    ''' <param name="MaximumValidUShort"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef UShort Range Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin() As UShort, ByRef ReturnedNumEnd() As UShort, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidUShort As UShort = UShort.MaxValue, Optional ByVal MaximumValidUShort As UShort = UShort.MaxValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._UShort
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidUShort = MinimumValidUShort
            dlg.MaximumValidUShort = MaximumValidUShort
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(-1)) Then
                    ReturnedNumBegin = Nothing
                    ReturnedNumEnd = Nothing
                Else
                    ReturnedNumBegin = dlg.ReturnedUShortArrayOne
                    ReturnedNumEnd = dlg.ReturnedUShortArrayTwo
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function
#End Region

#Region "Long"

    ''' <summary>
    ''' Long Value
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumNumber"></param>
    ''' <param name="MaximumNumber"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Long Value</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum As Long, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumNumber As Long = Long.MinValue, Optional ByVal MaximumNumber As Long = Long.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Long
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidLong = MinimumNumber
            dlg.MaximumValidLong = MaximumNumber
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Long.MinValue)) Then
                    ReturnedNum = Long.MinValue
                Else
                    ReturnedNum = dlg.ReturnedLongOne
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Long Range
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidLong"></param>
    ''' <param name="MaximumValidLong"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Long Range</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin As Long, ByRef ReturnedNumEnd As Long, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidLong As Long = Long.MinValue, Optional ByVal MaximumValidLong As Long = Long.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Long
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidLong = MinimumValidLong
            dlg.MaximumValidLong = MaximumValidLong
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Long.MinValue)) Then
                    ReturnedNumBegin = Long.MinValue
                    ReturnedNumEnd = Long.MinValue
                Else
                    ReturnedNumBegin = dlg.ReturnedLongOne
                    ReturnedNumEnd = dlg.ReturnedLongTwo
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Long Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidLong"></param>
    ''' <param name="MaximumValidLong"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Long Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum() As Long, ByVal AllowNothingAsResult As Boolean, Optional ByVal Title As String = "",
        Optional ByVal MinimumValidLong As Long = Long.MinValue, Optional ByVal MaximumValidLong As Long = Long.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Long
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidLong = MinimumValidLong
            dlg.MaximumValidLong = MaximumValidLong
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Long.MinValue)) Then
                    ReturnedNum = Nothing
                Else
                    ReturnedNum = dlg.ReturnedLongArrayOne
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Long Range Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidLong"></param>
    ''' <param name="MaximumValidLong"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef Long Range Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin() As Long, ByRef ReturnedNumEnd() As Long, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidLong As Long = Long.MinValue, Optional ByVal MaximumValidLong As Long = Long.MinValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Long
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidLong = MinimumValidLong
            dlg.MaximumValidLong = MaximumValidLong
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(Long.MinValue)) Then
                    ReturnedNumBegin = Nothing
                    ReturnedNumEnd = Nothing
                Else
                    ReturnedNumBegin = dlg.ReturnedLongArrayOne
                    ReturnedNumEnd = dlg.ReturnedLongArrayTwo
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function
#End Region

#Region "ULong"
    ''' <summary>
    ''' ULong Value
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumNumber"></param>
    ''' <param name="MaximumNumber"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef ULong Value</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum As ULong, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumNumber As ULong = ULong.MaxValue, Optional ByVal MaximumNumber As ULong = ULong.MaxValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._ULong
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidULong = MinimumNumber
            dlg.MaximumValidULong = MaximumNumber
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(-1)) Then
                    ReturnedNum = ULong.MaxValue
                Else
                    ReturnedNum = dlg.ReturnedULongOne
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' ULong Range
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidULong"></param>
    ''' <param name="MaximumValidULong"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef ULong Range</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin As ULong, ByRef ReturnedNumEnd As ULong, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidULong As ULong = ULong.MaxValue, Optional ByVal MaximumValidULong As ULong = ULong.MaxValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String = "") As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._ULong
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidULong = MinimumValidULong
            dlg.MaximumValidULong = MaximumValidULong
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.PreLoadText = {PreLoadText}

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(-1)) Then
                    ReturnedNumBegin = ULong.MaxValue
                    ReturnedNumEnd = ULong.MaxValue
                Else
                    ReturnedNumBegin = dlg.ReturnedULongOne
                    ReturnedNumEnd = dlg.ReturnedULongTwo
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' ULong Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNum"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidULong"></param>
    ''' <param name="MaximumValidULong"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef ULong Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNum() As ULong, ByVal AllowNothingAsResult As Boolean, Optional ByVal Title As String = "",
        Optional ByVal MinimumValidULong As ULong = ULong.MaxValue, Optional ByVal MaximumValidULong As ULong = ULong.MaxValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._ULong
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidULong = MinimumValidULong
            dlg.MaximumValidULong = MaximumValidULong
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(-1)) Then
                    ReturnedNum = Nothing
                Else
                    ReturnedNum = dlg.ReturnedULongArrayOne
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' ULong Range Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedNumBegin"></param>
    ''' <param name="ReturnedNumEnd"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="MinimumValidULong"></param>
    ''' <param name="MaximumValidULong"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="PreLoadText"></param>
    ''' <returns>Boolean Succeeded, ByRef ULong Range Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedNumBegin() As ULong, ByRef ReturnedNumEnd() As ULong, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal Title As String = "", Optional ByVal MinimumValidULong As ULong = ULong.MaxValue, Optional ByVal MaximumValidULong As ULong = ULong.MaxValue,
        Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal PreLoadText As String() = Nothing) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._ULong
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.MinimumValidULong = MinimumValidULong
            dlg.MaximumValidULong = MaximumValidULong
            dlg.MultiLine = True
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.isRange = True
            dlg.PreLoadText = PreLoadText

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso (dlg.txtInput.Text = "" OrElse dlg.txtInput.Text = CStr(-1)) Then
                    ReturnedNumBegin = Nothing
                    ReturnedNumEnd = Nothing
                Else
                    ReturnedNumBegin = dlg.ReturnedULongArrayOne
                    ReturnedNumEnd = dlg.ReturnedULongArrayTwo
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function
#End Region

#Region "Date"
    ''' <summary>
    ''' Date and Date Range (Can only be single line)
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedDateFrom"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="DateFormat"></param>
    ''' <param name="DateRange"></param>
    ''' <param name="ReturnedDateTo"></param>
    ''' <param name="ValidDateFrom"></param>
    ''' <param name="ValidDateTo"></param>
    ''' <param name="Title"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="WholeRangeMustChange"></param>
    ''' <returns>Boolean Succeeded, ByRef ReturnedDateFrom (, ReturnedDateTo [IF DateRange = True])</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedDateFrom As Date, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal DateFormat As String = DefaultDateTimeFormat, Optional ByVal DateRange As Boolean = False,
        Optional ByRef ReturnedDateTo As Date = DefaultDate, Optional ByVal ValidDateFrom As Date = DefaultDate, Optional ByVal ValidDateTo As Date = DefaultDate,
        Optional ByVal Title As String = "", Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal WholeRangeMustChange As Boolean = False) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Date
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.DateFormat = DateFormat
            dlg.ValidDateFrom = ValidDateFrom
            dlg.ValidDateTo = ValidDateTo
            dlg.isRange = DateRange
            dlg.WholeRangeMustChange = WholeRangeMustChange

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso dlg.dtFrom.Value = DefaultDate Then 'The if isn't needed, but it is to demonstrate that "AllowNothingAsResult" is implemented
                    ReturnedDateFrom = DefaultDate
                Else
                    ReturnedDateFrom = dlg.dtFrom.Value
                End If

                If AllowNothingAsResult AndAlso dlg.dtTo.Value = DefaultDate Then 'The if isn't needed, but it is to demonstrate that "AllowNothingAsResult" is implemented
                    ReturnedDateTo = DefaultDate
                Else
                    ReturnedDateTo = dlg.dtTo.Value
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Date Array
    ''' </summary>
    ''' <param name="LabelText"></param>
    ''' <param name="ReturnedDates"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="DateFormat"></param>
    ''' <param name="ValidDateFrom"></param>
    ''' <param name="ValidDateTo"></param>
    ''' <param name="Title"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <param name="WholeRangeMustChange"></param>
    ''' <returns>Boolean Succeeded, ByRef Date Array</returns>
    Public Function TypeBox(ByVal LabelText As String, ByRef ReturnedDates() As Date, ByVal AllowNothingAsResult As Boolean,
        Optional ByVal DateFormat As String = DefaultDateTimeFormat, Optional ByVal ValidDateFrom As Date = DefaultDate, Optional ByVal ValidDateTo As Date = DefaultDate,
        Optional ByVal Title As String = "", Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True, Optional ByVal WholeRangeMustChange As Boolean = False) As Boolean

        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._Date
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.DateFormat = DateFormat
            dlg.ValidDateFrom = ValidDateFrom
            dlg.ValidDateTo = ValidDateTo
            dlg.isRange = False
            dlg.MultiLine = True
            dlg.WholeRangeMustChange = WholeRangeMustChange

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso dlg.dtFrom.Value = DefaultDate Then 'The if isn't needed, but it is to demonstrate that "AllowNothingAsResult" is implemented
                    ReturnedDates = {DefaultDate}
                Else
                    ReturnedDates = dlg.ReturnedDateArrayOne
                End If
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function
#End Region

#Region "Combo Box"
    ''' <summary>
    ''' Combo Box (List of Items to retrieve Text); No Integer Return
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="LabelText"></param>
    ''' <param name="cbDataSource"></param>
    ''' <param name="ReturnedText"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <returns>Boolean Succeeded, ByRef ReturnedText As String</returns>
    Public Function TypeBox(Of T)(ByVal LabelText As String, ByVal cbDataSource As T, ByRef ReturnedText As String, ByVal AllowNothingAsResult As Boolean,
                Optional ByVal Title As String = "", Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True) As Boolean
        Return TypeBox(LabelText, cbDataSource, 0, ReturnedText, AllowNothingAsResult, Title, Width, Height, ShowErrorMessage)
    End Function
    '
    ''' <summary>
    ''' Combo Box (List of Items to retrieve Index or Text or Both)
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="LabelText"></param>
    ''' <param name="cbDataSource"></param>
    ''' <param name="ReturnedIndex"></param>
    ''' <param name="ReturnedText"></param>
    ''' <param name="AllowNothingAsResult"></param>
    ''' <param name="Title"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="ShowErrorMessage"></param>
    ''' <returns>Boolean Succeeded, byRef Index, ByRef Text</returns>
    Public Function TypeBox(Of T)(ByVal LabelText As String, ByVal cbDataSource As T, ByRef ReturnedIndex As Integer, ByRef ReturnedText As String, ByVal AllowNothingAsResult As Boolean,
                                  Optional ByVal Title As String = "", Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, Optional ShowErrorMessage As Boolean = True) As Boolean
        Try
            Dim dlg As New dlgTypeBox
            dlg.TypeBoxMode = TypeMode._ComboBox
            dlg.LabelText = LabelText
            dlg.AllowNothingAsResult = AllowNothingAsResult
            dlg.Title = Title
            dlg.FormWidth = Width
            dlg.FormHeight = Height
            dlg.ComboBoxDataSource = cbDataSource

            If dlg.ShowDialog = DialogResult.OK Then
                If AllowNothingAsResult AndAlso ReturnedIndex = -1 Then
                    ReturnedIndex = -1
                    ReturnedText = ""
                Else
                    ReturnedIndex = dlg.cbIndexReturner.SelectedIndex
                    ReturnedText = dlg.cbIndexReturner.SelectedItem.ToString
                End If

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowErrorMessage)
            Return False
        End Try

    End Function
#End Region

#End Region

#Region "Read File"
    Public Function ReadFile(ByVal FileName As String, ByVal TextboxToFill As TextBox, Optional ByVal ReadMethod As String = "ReadToEnd", Optional isCyphered As Boolean = False, Optional ByVal CipherLlvl As Integer = 1, Optional ByVal ShowError As Boolean = False) As String
        'ReadMethods: ReadToEnd, ReadLine
        Try
            Dim fStream As FileStream = File.OpenRead(FileName)
            Dim sReader As New StreamReader(fStream, True)
            If ReadMethod.ToLower = "readline" Then
                TextboxToFill.Text = sReader.ReadLine
            Else
                TextboxToFill.Text = sReader.ReadToEnd
            End If

            sReader.Dispose()
            sReader.Close()
            fStream.Dispose()
            fStream.Close()

            If isCyphered Then
                TextboxToFill.Text = DecypherToText(TextboxToFill.Lines, CipherLlvl)
            End If

        Catch ex As Exception
            CreateCrashFile(ex, ShowError)
        End Try

        Return TextboxToFill.Text
    End Function

    Public Function ReadFile(ByVal FileName As String, ByRef StringToFill As String, Optional ByVal ReadMethod As String = "ReadToEnd", Optional ByVal CipherLlvl As Integer = -1) As String
        'ReadMethods: ReadToEnd, ReadLine
        Dim fStream As FileStream = File.OpenRead(FileName)
        Dim sReader As New StreamReader(fStream, True)
        If ReadMethod.ToLower = "readline" Then
            StringToFill = sReader.ReadLine
        ElseIf ReadMethod.ToLower = "readtoend" Then
            StringToFill = sReader.ReadToEnd
        Else
            StringToFill = "ReadMethod string(" & ReadMethod & ") is unknown."
            Return null
        End If

        sReader.Dispose()
        sReader.Close()
        fStream.Dispose()
        fStream.Close()

        If CipherLlvl <> -1 Then
            Dim StringArrayToFill(0) As String
            StringArrayToFill(0) = StringToFill
            StringToFill = DecypherToText(StringArrayToFill, CipherLlvl)
        End If

        Return StringToFill
    End Function

    Public Function ReadFile(ByVal FileName As String, ByRef ArrayToFill() As String, Optional ByVal ReadMethod As String = "ReadToEnd", Optional isCyphered As Boolean = False, Optional ByVal Cipherlvl As Integer = 1) As String()
        'ReadMethods: ReadToEnd, ReadLine
        Dim fStream As FileStream = File.OpenRead(FileName)
        Dim sReader As New StreamReader(fStream, True)

        If ReadMethod.ToLower = "readtoend" Then
            ArrayToFill = Split(sReader.ReadToEnd, vbCrLf)

            If isCyphered Then
                ArrayToFill = DecypherToArray(ArrayToFill, Cipherlvl)
            End If

        ElseIf ReadMethod.ToLower = "readline" Then
            ArrayToFill(0) = sReader.ReadLine
            If isCyphered Then
                ArrayToFill(0) = DecypherToText(ArrayToFill(0), Cipherlvl)
            End If

        Else
            ArrayToFill(0) = "ReadMethod string(" & ReadMethod & ") is unknown."
        End If

        sReader.Dispose()
        sReader.Close()
        fStream.Dispose()
        fStream.Close()

        Return ArrayToFill
    End Function

    Public Function ReadFile(ByVal FileName As String, ByRef ListToFill As List(Of String), Optional ByVal ReadMethod As String = "ReadToEnd", Optional isCyphered As Boolean = False, Optional ByVal CipherLlvl As Integer = 1) As List(Of String)
        'ReadMethods: ReadToEnd, ReadLine
        Dim fStream As FileStream = File.OpenRead(FileName)
        Dim sReader As New StreamReader(fStream, True)

        If ReadMethod.ToLower = "readtoend" Then
            While (Not sReader.EndOfStream)
                ListToFill.Add(sReader.ReadLine)
                If isCyphered Then
                    ListToFill.Item(ListToFill.Count - 1) = DecypherToText(ListToFill.Item(ListToFill.Count - 1), CipherLlvl)
                End If

            End While

        ElseIf ReadMethod.ToLower = "readline" Then
            Dim TextRead As TextReader
            TextRead = File.OpenText(FileName)
            ListToFill.Add(TextRead.ReadLine)

        Else
            ListToFill.Add("ReadMethod string(" & ReadMethod & ") is unknown.")
        End If

        Return ListToFill
    End Function
#End Region

#Region "Write Text"
    Public Sub WriteText(ByVal FileName As String, ByVal TextToWrite As String, ByVal Encoding As System.Text.Encoding, Optional ByVal WriteMethod As String = "Write", Optional isCyphered As Boolean = False, Optional ByVal CipherLlvl As Integer = 1, Optional ByVal ToBeCyphered As Boolean = False, Optional ByVal CipherWriteLvl As Integer = 1)
        'Methods: Write / WriteLine / Continue / ContEnd / End
        'On WriteLine TextWrite should also be closed on the calling-sub's Catch Exception by WriteText(FileName,"","End")

        Dim LastSlashIndex As Integer = FileName.LastIndexOf("\"c)
        Dim ParentDir As String
        Try
            ParentDir = Mid(FileName, 1, LastSlashIndex)
            If Not Directory.Exists(ParentDir) Then Directory.CreateDirectory(ParentDir)
        Catch ex As Exception
        End Try

        If isCyphered Then
            Dim tmpString(0) As String
            tmpString(0) = TextToWrite
            TextToWrite = DecypherToText(tmpString, CipherLlvl)
        End If

        If ToBeCyphered Then
            Dim tmpString(0) As String
            tmpString(0) = TextToWrite
            TextToWrite = Cypher(tmpString, CipherWriteLvl)
        End If

        If WriteMethod.ToLower = "write" Then
            Dim StreamWrite As New StreamWriter(FileName, False, Encoding)
            StreamWrite.Write(TextToWrite)
            StreamWrite.Flush()
            StreamWrite.Close()

        ElseIf WriteMethod.ToLower = "writeline" Then
            StreamWrite_Persistent = New StreamWriter(FileName, False, Encoding)
            StreamWrite_Persistent.WriteLine(TextToWrite)

        ElseIf WriteMethod.ToLower = "continue" Or WriteMethod.ToLower = "cont" Then
            StreamWrite_Persistent.WriteLine(TextToWrite)

        ElseIf WriteMethod.ToLower = "contend" Then
            StreamWrite_Persistent.WriteLine(TextToWrite)
            StreamWrite_Persistent.Flush()
            StreamWrite_Persistent.Close()

        ElseIf WriteMethod.ToLower = "end" Then
            StreamWrite_Persistent.Flush()
            StreamWrite_Persistent.Close()

        End If
    End Sub
    Public Sub WriteText(ByVal FileName As String, ByVal ArToWrite() As String, ByVal Encoding As System.Text.Encoding, Optional ByVal WriteMethod As String = "Write", Optional isCyphered As Boolean = False, Optional ByVal CipherLlvl As Integer = 1, Optional ByVal ToBeCyphered As Boolean = False, Optional ByVal CipherWriteLvl As Integer = 1)
        Dim strToWrite As String = ArrayBox(ArToWrite)
        WriteText(FileName, strToWrite, Encoding, WriteMethod, isCyphered, CipherLlvl, ToBeCyphered, CipherWriteLvl)
    End Sub
#End Region

#Region "Get Fuctions (FileName, Extention, InternetFolderName..."
    Public Function GetFileName(ByVal FilePath As String) As String
        Dim FileName As String = String.Empty

        If File.Exists(FilePath) Then FileName = Path.GetFileName(FilePath)

        Return FileName
    End Function

    Public Function GetFileNameAlone(ByVal FilePath As String) As String 'Returned String.Empty if file does not exist
        Dim PureFileName As String = String.Empty

        If File.Exists(FilePath) Then PureFileName = Path.GetFileNameWithoutExtension(FilePath)

        Return PureFileName
    End Function

    Public Function GetExt(ByVal FilePath As String, Optional ByVal WithoutTheDot As Boolean = False) As String
        Dim Extension As String = String.Empty

        If File.Exists(FilePath) Then
            If Not WithoutTheDot Then Extension = Path.GetExtension(FilePath) Else Extension = GetSubstrAfterString(Path.GetExtension(FilePath), ".", Path.GetExtension(FilePath), True)
        End If

        Return Extension
    End Function

    Public Function GetFolderNameAlone(ByVal FolderPath As String) As String
        Dim FolderNameAlone As String = String.Empty
        If FolderPath.EndsWith("\") Then FolderPath = Left(FolderPath, FolderPath.Length - 1)

        If FolderPath.Contains("\") Then
            Dim Index As Integer = FolderPath.LastIndexOf("\")
            FolderNameAlone = FolderPath.Substring(Index + 1)
        Else
            FolderNameAlone = FolderPath
        End If

        Return FolderNameAlone
    End Function

    Public Function GetInternetFolderNameAlone(ByVal InternetURL As String) As String
        Dim FolderNameAlone As String = String.Empty

        If InternetURL.EndsWith("/") Then InternetURL = Left(InternetURL, InternetURL.Length - 1)

        If InternetURL.Contains("/") Then
            Dim Index As Integer = InternetURL.LastIndexOf("/")
            FolderNameAlone = InternetURL.Substring(Index + 1)
        Else
            FolderNameAlone = InternetURL
        End If

        Return FolderNameAlone
    End Function

    Public Function GetParentDir(ByVal FileOrPathName As String) As String
        Return doProperPathName(Directory.GetParent(FileOrPathName).FullName)
    End Function
#End Region

#Region "Send Email"
    Public Function SendEmail(ByVal Text As String, ByVal SendComputerDetails As Boolean) As Boolean
        Return SendEmail(Text, SendComputerDetails, DecypherToText(My.Settings.mFrom, MailSettingsCipherLevel), DecypherToText(My.Settings.mPW, MailSettingsCipherLevel),
                         DecypherToText(My.Settings.mTo, MailSettingsCipherLevel), DecypherToText(My.Settings.mClientHost, MailSettingsCipherLevel), {}, , My.Settings.mSSL, , My.Settings.mPort)
    End Function
    Public Function SendEmail(ByVal Text As String) As Boolean
        Return SendEmail(Text, True, DecypherToText(My.Settings.mFrom, MailSettingsCipherLevel), DecypherToText(My.Settings.mPW, MailSettingsCipherLevel),
                         DecypherToText(My.Settings.mTo, MailSettingsCipherLevel), DecypherToText(My.Settings.mClientHost, MailSettingsCipherLevel), {}, , My.Settings.mSSL, , My.Settings.mPort)
    End Function
    Public Function SendEmail(ByVal Text As String, ByVal SendComputerDetails As Boolean, ByVal ExtraSubject As String) As Boolean
        Return SendEmail(Text, SendComputerDetails, DecypherToText(My.Settings.mFrom, MailSettingsCipherLevel), DecypherToText(My.Settings.mPW, MailSettingsCipherLevel),
                         DecypherToText(My.Settings.mTo, MailSettingsCipherLevel), DecypherToText(My.Settings.mClientHost, MailSettingsCipherLevel), {}, ExtraSubject, My.Settings.mSSL, , My.Settings.mPort)
    End Function
    Public Function SendEmail(ByVal Text As String, ByVal ExtraSubject As String) As Boolean
        Return SendEmail(Text, True, DecypherToText(My.Settings.mFrom, MailSettingsCipherLevel), DecypherToText(My.Settings.mPW, MailSettingsCipherLevel),
                         DecypherToText(My.Settings.mTo, MailSettingsCipherLevel), DecypherToText(My.Settings.mClientHost, MailSettingsCipherLevel), {}, ExtraSubject, My.Settings.mSSL, , My.Settings.mPort)
    End Function
    Public Function SendEmail(ByVal Text As String, ByVal SendComputerDetails As Boolean, ToMailAcc As String, ByVal ExtraSubject As String) As Boolean
        Return SendEmail(Text, SendComputerDetails, DecypherToText(My.Settings.mFrom, MailSettingsCipherLevel), DecypherToText(My.Settings.mPW, MailSettingsCipherLevel),
                         ToMailAcc, DecypherToText(My.Settings.mClientHost, MailSettingsCipherLevel), {}, ExtraSubject, My.Settings.mSSL, , My.Settings.mPort)
    End Function
    Public Function SendEmail(ByVal Text As String, ToMailAcc As String, ByVal ExtraSubject As String) As Boolean
        Return SendEmail(Text, False, DecypherToText(My.Settings.mFrom, MailSettingsCipherLevel), DecypherToText(My.Settings.mPW, MailSettingsCipherLevel),
                        ToMailAcc, DecypherToText(My.Settings.mClientHost, MailSettingsCipherLevel), {}, ExtraSubject, My.Settings.mSSL, , My.Settings.mPort)
    End Function
    Public Function SendEmail(ByVal Message As String, ByVal SendComputerDetail As Boolean, ByVal SenderMailAcc As String, ByVal SenderMailPass As String, ByVal MailTo As String,
                              ByVal MailClientHost As String, ByVal strAttachments() As String, Optional ByVal ExtraSubjectText As String = "",
                                  Optional ByVal UseSSL As Boolean = True, Optional ByVal ShowErrorMessage As Boolean = False, Optional mPort As Integer = 587) As Boolean
        Dim MailSent As Boolean = False
        'gmail has SSL, Otenet and HOL do not.

        'Mail Body Requirements
        Dim ProgramName As String = My.Application.Info.Title & " v" & My.Application.Info.Version.ToString
        Dim CurrentDateTime As String = Today.ToShortDateString & " " & My.Computer.Clock.LocalTime.ToShortTimeString

        Try
            If CheckInternetAvailability("http://www.google.com") Then
                'Actually creating the MailBody
                Dim MailBody As String = String.Empty

                If SendComputerDetail Then
                    With My.Computer.Info
                        'With modSecurity

                        'MailBody = "Program: " & ProgramName & vbCrLf & "Expiration Date: " & ExpirationDateOfRegistrySN.ToShortDateString &
                        '         vbCrLf & vbCrLf & "License Username: " & UserName & vbCrLf & "Password: " & SerialNumber & vbCrLf & "Email: " & UserEmail &
                        '         vbCrLf & vbCrLf & "Country: " & UserCountry & vbCrLf & "City: " & UserCity & vbCrLf & "Street Address: " & UserStreetAddress & vbCrLf &
                        '         "Zip Code: " & UserZipCode & vbCrLf & "Landline Number: " & UserLandlineNumber & vbCrLf & "Mobile Number: " & UserMobileNumber &
                        '         vbCrLf & vbCrLf & "Edition Number: " & EditionNum & vbCrLf

                        'If EditionNum >= 0 AndAlso EditionsNames.Length > EditionNum Then MailBody &= vbCrLf & "Edition Name: " & EditionsNames(EditionNum) & vbCrLf & vbCrLf

                        'MailBody &= "Computer Name: " & My.Computer.Name & vbCrLf & "Computer User: " & My.User.Name & vbCrLf & "Date and Time: " & CurrentDateTime & vbCrLf &
                        '         "OS Full Name: " & .OSFullName & vbCrLf & "OS Platform: " & .OSPlatform & vbCrLf & "OS Version: " & .OSVersion & vbCrLf &
                        '         "Installed UI Culture: " & .InstalledUICulture.ToString & vbCrLf & vbCrLf

                        'Without Mod Security
                        MailBody = "Program: " & ProgramName & vbCrLf &
                                    vbCrLf & "License Username: " & UserName &
                                    "Computer Name: " & My.Computer.Name & vbCrLf &
                                    "Computer User: " & My.User.Name & vbCrLf & "Date and Time: " & CurrentDateTime & vbCrLf & "OS Full Name: " & .OSFullName & vbCrLf &
                                    "OS Platform: " & .OSPlatform & vbCrLf & "OS Version: " & .OSVersion & vbCrLf & "Installed UI Culture: " & .InstalledUICulture.ToString & vbCrLf & vbCrLf

                    End With
                End If

                MailBody &= Message

                Dim Email As New MailMessage(SenderMailAcc, MailTo)
                Email.Subject = ExtraSubjectText & " " & ProgramName & " " & UserName
                Email.Body = MailBody

                For Each strAttachment As String In strAttachments
                    If strAttachment <> "" Then
                        Dim Attachment As Attachment = New Attachment(strAttachment)
                        Email.Attachments.Add(Attachment)
                    End If
                Next

                Dim mailClient As New System.Net.Mail.SmtpClient()
                mailClient.Port = mPort
                Dim basicAuthenticationInfo As New System.Net.NetworkCredential(SenderMailAcc, SenderMailPass)
                mailClient.Host = MailClientHost
                mailClient.UseDefaultCredentials = False
                If UseSSL Then mailClient.EnableSsl = True
                mailClient.Credentials = basicAuthenticationInfo
                mailClient.Send(Email)

                MailSent = True

            Else
                MailSent = False
            End If

        Catch ex As Exception
            MailSent = False

#If DEBUG Then
            MsgBox("Debug-Only-Visible Message:" & vbCrLf & ex.ToString)
#End If
        End Try

        Return MailSent
    End Function
#End Region

#Region "Load/Write Settings"
    Public Sub LoadSettings()
        Call UpdateSettings()
        ReadFile(strSettingsIni, strSettings)
    End Sub

    Public Sub WriteSettings(ByVal SettingsString As String, ByVal FormOrControlName As String)
begin:
        If Not CurrentlyWritingSettings Then
            CurrentlyWritingSettings = True
            WriteText(strSettingsIni, SettingsString, Encoding.Unicode)
            CurrentlyWritingSettings = False

        ElseIf MsgBox("Settings file is currently being used by: " & FormOrControlName & vbCrLf & "Do you want to try again?", MsgBoxStyle.YesNoCancel) = vbYes Then
            GoTo begin
        End If

    End Sub
    Public Sub WriteSettings(ByVal SettingsString() As String, ByVal FormOrControlName As String)
        Dim SettingsText As String = ArrayBox(False, "", 1, False, SettingsString)
        WriteSettings(SettingsText, FormOrControlName)
    End Sub

    Public Sub UpdateSettings()
        If File.Exists(strSettingsRar) Then
            Dim OldSettingsFile As String = strSettingsIni & ".bak"
            Dim OldSettings() As String = {}
            Dim NewSettings() As String = {}
            Dim FinalSettings() As String = {}

            If File.Exists(strSettingsIni) Then
                DelFileFolder(OldSettingsFile)
                RenFileFolder(strSettingsIni, OldSettingsFile, True)
            End If

            Unrar(strSettingsRar, strSettingsPath, , True)
            If File.Exists(OldSettingsFile) Then ReadFile(OldSettingsFile, OldSettings)
            ReadFile(strSettingsIni, NewSettings)

            If OldSettings.Length > 0 Then
                ReDim FinalSettings(NewSettings.Length - 1)
                For i = 0 To NewSettings.Length - 1
                    If i < OldSettings.Length Then
                        Dim OldEqualSignIndex As Integer = FindIndex("=", OldSettings(i))
                        Dim NewEqualSignIndex As Integer = FindIndex("=", NewSettings(i))

                        If OldEqualSignIndex <> -1 Then
                            If NewSettings(i).ToUpper.StartsWith("{U}") Then
                                FinalSettings(i) = NewSettings(i).Substring("{U}".Length)
                            ElseIf OldEqualSignIndex = NewEqualSignIndex Then
                                FinalSettings(i) = OldSettings(i)
                            Else
                                FinalSettings(i) = NewSettings(i)
                            End If

                        Else
                            FinalSettings(i) = NewSettings(i)
                        End If

                    Else
                            FinalSettings(i) = NewSettings(i)
                    End If
                Next

            Else
                For i = 0 To NewSettings.Length - 1
                    If NewSettings(i).ToUpper.StartsWith("{U}") Then
                        NewSettings(i) = NewSettings(i).Substring("{U}".Length)
                    End If
                Next
                FinalSettings = NewSettings
            End If

            Call WriteSettings(FinalSettings, "UpdateSettings()")
            DelFileFolder(strSettingsRar, False)
            DelFileFolder(OldSettingsFile, False)
        End If
    End Sub
#End Region

#Region "Numeric Range"
    Public Function isNumRangeReturned(ByVal Text As String, ByRef ReturnValue1 As Integer, ByRef ReturnValue2 As Integer) As Boolean
        'Delimiter Parser v1.1 2012/08/11
        Dim Delimiter As String = String.Empty
        Text = Text.Replace(" ", "")

        If Text.Contains("("c) Then Text = Text.Remove(Text.IndexOf("("c), 1)
        If Text.Contains(")"c) Then Text = Text.Remove(Text.IndexOf(")"c), 1)

        If Text.Contains(LangDelimiterForRangeOrSpan.Replace(" ", "")) Then
            Delimiter = LangDelimiterForRangeOrSpan.Replace(" ", "") 'to (for example 1to5)
        ElseIf Text.Contains(",") Then
            Delimiter = "," 'to (for example 1,5)
        End If

        If Delimiter <> String.Empty Then
            Dim DecimalRange() As String = Split(Text, Delimiter)

            If DecimalRange.Length = 2 Then
                ReturnValue1 = CInt(DecimalRange(0))
                ReturnValue2 = CInt(DecimalRange(1))
                Return True

            Else
                Return False
            End If

        Else
            Return False
        End If
    End Function
    Public Function DecimalRange(ByVal Text As String, ByRef ReturnValue1 As Integer, ByRef ReturnValue2 As Integer) As Boolean
        Return isNumRangeReturned(Text, ReturnValue1, ReturnValue2)
    End Function
    Public Function NumericRange(ByVal Text As String, ByRef ReturnValue1 As Integer, ByRef ReturnValue2 As Integer) As Boolean
        Return isNumRangeReturned(Text, ReturnValue1, ReturnValue2)
    End Function
#End Region

#Region "Date / Time Operations"
    Public Function LoadHexDate(ByVal strHexDateDDMMYYYY As String, Optional ByVal DateFormat As String = DefaultDateFormat, Optional DateStringFormat As String = DefaultDateStringFormat) As Date
        Dim ResultDate As Date = DefaultDate

        Try
            Dim HexDay As String = Left(strHexDateDDMMYYYY, 2)
            Dim HexMonth As String = Mid(strHexDateDDMMYYYY, 3, 2)
            Dim HexYear As String = Right(strHexDateDDMMYYYY, 4)

            Dim IntDay As Integer = Convert.ToInt32(HexDay, 16)
            Dim IntMonth As Integer = Convert.ToInt32(HexMonth, 16)
            Dim IntYear As Integer = Convert.ToInt32(HexYear, 16)

            Dim strDate As String = String.Format(DateStringFormat, IntDay, IntMonth, IntYear)
            ResultDate = Date.ParseExact(strDate, DateFormat, System.Globalization.CultureInfo.InvariantCulture)


        Catch ex As Exception
        End Try

        Return ResultDate
    End Function

    Public Function LoadOctalDate(ByVal strHexDateDDMMYYYYY As String, Optional ByVal DateFormat As String = DefaultDateFormat, Optional DateStringFormat As String = DefaultDateStringFormat) As Date
        Dim ResultDate As Date = DefaultDate

        Try
            Dim HexDay As String = Left(strHexDateDDMMYYYYY, 2)
            Dim HexMonth As String = Mid(strHexDateDDMMYYYYY, 3, 2)
            Dim HexYear As String = Right(strHexDateDDMMYYYYY, 5)

            Dim IntDay As Integer = Convert.ToInt32(HexDay, 8)
            Dim IntMonth As Integer = Convert.ToInt32(HexMonth, 8)
            Dim IntYear As Integer = Convert.ToInt32(HexYear, 8)

            Dim strDate As String = String.Format(DateStringFormat, IntDay, IntMonth, IntYear)
            ResultDate = Date.ParseExact(strDate, DateFormat, System.Globalization.CultureInfo.InvariantCulture)

        Catch ex As Exception
        End Try

        Return ResultDate
    End Function

#Region "Get Time Difference"
    Public Function GetTimeDifference(ByVal TimeDifference As TimeSpan, Optional ByVal ExtraTextOnTheBeginning_OrTypeDefault As String = "") As String
        Dim strTimeDifference As String = String.Empty

        If TimeDifference.Days > 0 AndAlso strTimeDifference = String.Empty Then
            strTimeDifference &= TimeDifference.Days & strModLanguage(46) ' Days

        ElseIf TimeDifference.Days > 0 Then
            strTimeDifference &= ", " & TimeDifference.Days & strModLanguage(46) ' Days
        End If

        If TimeDifference.Hours > 0 AndAlso strTimeDifference = String.Empty Then
            strTimeDifference &= TimeDifference.Hours & strModLanguage(47) ' Hours

        ElseIf TimeDifference.Hours > 0 Then
            strTimeDifference &= ", " & TimeDifference.Hours & strModLanguage(47) ' Hours
        End If

        If TimeDifference.Minutes > 0 AndAlso strTimeDifference = String.Empty Then
            strTimeDifference &= TimeDifference.Minutes & strModLanguage(48) ' Minutes

        ElseIf TimeDifference.Minutes > 0 Then
            strTimeDifference &= ", " & TimeDifference.Minutes & strModLanguage(48) ' Minutes
        End If

        If TimeDifference.Seconds > 0 AndAlso strTimeDifference = String.Empty Then
            strTimeDifference &= TimeDifference.Seconds & strModLanguage(49) ' Seconds

        ElseIf TimeDifference.Seconds > 0 Then
            strTimeDifference &= ", " & TimeDifference.Seconds & strModLanguage(49) ' Seconds
        End If

        If TimeDifference.Milliseconds > 0 AndAlso strTimeDifference = String.Empty Then
            strTimeDifference &= TimeDifference.Milliseconds & strModLanguage(50) ' Milliseconds

        ElseIf TimeDifference.Milliseconds > 0 Then
            strTimeDifference &= ", " & TimeDifference.Milliseconds & strModLanguage(50) ' Milliseconds
        End If

        If strTimeDifference = String.Empty Then strTimeDifference = strModLanguage(51) ' Less than a millisecond

        If ExtraTextOnTheBeginning_OrTypeDefault.ToLower = "default" Then
            strTimeDifference = strModLanguage(53) & strTimeDifference 'The procedure finished in:
        ElseIf ExtraTextOnTheBeginning_OrTypeDefault <> "" Then
            If Not ExtraTextOnTheBeginning_OrTypeDefault.EndsWith(" ") Then ExtraTextOnTheBeginning_OrTypeDefault &= " "
            strTimeDifference = ExtraTextOnTheBeginning_OrTypeDefault & strTimeDifference
        End If

        Return strTimeDifference
    End Function
    Public Function GetTimeDifference(ByVal TimeDifferenceBegin As Date, ByVal TimeDifferenceEnd As Date, Optional ByVal ExtraTextOnTheBeginning_OrTypeDefault As String = "") As String
        Dim strTimeDifference As String = String.Empty

        Dim TimeDifference As TimeSpan = (TimeDifferenceEnd - TimeDifferenceBegin)
        strTimeDifference = GetTimeDifference(TimeDifference, ExtraTextOnTheBeginning_OrTypeDefault)

        Return strTimeDifference
    End Function
#End Region

#End Region

    <DebuggerStepThrough()>
    Public Function ss(ByVal str As String, ParamArray Vars() As Object) As String
        Try
            Return String.Format(str, Vars)
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Public Function SpaceAString(ByVal StringToSpace As String, ByVal ItemMaxLength As Integer, Optional ByVal RemOnExcess As String = "None", Optional ByVal PrefixInsteadOfSuffix As Boolean = False) As String
        Dim sbResult As New StringBuilder
        sbResult.Append(StringToSpace)

        If StringToSpace.Length < ItemMaxLength Then
            If Not PrefixInsteadOfSuffix Then
                sbResult.Insert(sbResult.Length, " ", ItemMaxLength - StringToSpace.Length)
            Else
                sbResult.Insert(0, " ", ItemMaxLength - StringToSpace.Length)
            End If
        ElseIf StringToSpace.Length > ItemMaxLength Then
            If RemOnExcess.ToLower = "top" OrElse RemOnExcess.ToLower = "begin" OrElse RemOnExcess.ToLower = "beginning" OrElse RemOnExcess.ToLower = "left" Then
                sbResult.Remove(0, StringToSpace.Length - ItemMaxLength)
            ElseIf RemOnExcess.ToLower = "bottom" OrElse RemOnExcess.ToLower = "end" OrElse RemOnExcess.ToLower = "ending" OrElse RemOnExcess.ToLower = "right" Then
                sbResult.Remove(ItemMaxLength, StringToSpace.Length - ItemMaxLength)
            End If
        End If

        Return sbResult.ToString
    End Function

#Region "Zero"
    Public Function Zero_Auto(Of T)(ByVal Sth As T, ByVal MinimumLength As Integer, Optional ByVal MaxLength As Integer = -1, Optional ByVal SuffixZeroInsteadOfPrefix As Boolean = False, Optional ByVal DeleteByAnyMeansNecessary As Boolean = False, Optional ByVal DecimalSeparator As String = "") As String
        Dim Result As String

        If IsNumeric(Sth.ToString) Then Result = Zero_A_Num(Sth, MinimumLength, MaxLength, DeleteByAnyMeansNecessary, DecimalSeparator) Else Result = Zero_A_String(Sth.ToString, MinimumLength, MaxLength, SuffixZeroInsteadOfPrefix)

        Return Result
    End Function
    Public Function Zero_A_String(ByVal Str As String, MinimumLength As Integer, Optional ByVal MaxLength As Integer = -1, Optional ByVal SuffixZeroInsteadOfPrefix As Boolean = False) As String
        Dim result As String = Str

        If result.Length < MinimumLength Then
            If Not SuffixZeroInsteadOfPrefix Then
                For i = 0 To MinimumLength - result.Length - 1
                    result = "0" & result
                Next

            Else
                For i = 0 To MinimumLength - result.Length - 1
                    result &= "0"
                Next
            End If

        ElseIf result.Length > MaxLength AndAlso MaxLength <> -1 Then
            result = Left(result, MaxLength)
        End If

        Return result
    End Function

    Public Function Zero_A_Num(Of T)(ByVal Num As T, ByVal MinimumLength As Integer, Optional ByVal MaxLength As Integer = -1, Optional ByVal DeleteByAnyMeansNecessary As Boolean = False, Optional ByVal DecimalSeparator As String = "") As String
        Dim Result As String = Num.ToString
        If DecimalSeparator = "" Then DecimalSeparator = strDecimalSeparator

        If Result.Length < MinimumLength Then
            If Result.Contains(DecimalSeparator) Then
                For i = 0 To MinimumLength - Result.Length - 1
                    Result &= "0"
                Next

            Else
                If CDbl(Result) > 0 Then
                    For i = 0 To MinimumLength - Result.Length - 1
                        Result = "0" & Result
                    Next

                Else
                    For i = 0 To MinimumLength - Result.Length - 1
                        Result = Left(Result, 1) & "0" & Right(Result, Result.Length - 1)
                    Next
                End If

            End If

        ElseIf Result.Length > MaxLength AndAlso MaxLength <> -1 Then
            If Result.Contains(DecimalSeparator) Then
                Dim AllowableLossOfDigits As Integer = Result.Length - CInt(Result).ToString.Length - 2
                Dim tmpString As String = Left(Result, MaxLength)

                If MaxLength <= AllowableLossOfDigits OrElse tmpString(tmpString.Length - 1) <> "."c Then 'If the loss of digits is acceptable, or it isn't be we must do it andalso the string is a real number who doesn't end in a decimal separation point
                    Result = tmpString

                ElseIf DeleteByAnyMeansNecessary Then
                    If CDbl(Result) > 0 Then Result = "0" & Left(tmpString, tmpString.Length - 1) Else Result = Left(tmpString, 1) & "0" & Mid(tmpString, 1, tmpString.Length - 2) 'If it does have a "." in the end, then lets replace it with a "0" in the begining (or after the - in a negative number)
                End If

            ElseIf DeleteByAnyMeansNecessary Then
                Result = Left(Result, MaxLength)
            End If
        End If

        Return Result
    End Function
#End Region

#Region "IsContained"
    Public Function isContained(Of T)(ByVal Sth As T, ByVal IntoSth() As T, Optional ByVal CaseSensitive As Boolean = True) As Boolean
        Dim Result As Boolean

        For i = 0 To IntoSth.Length - 1

            If CaseSensitive Then
                If IntoSth(i).ToString.Contains(Sth.ToString) Then
                    Result = True
                    Exit For
                End If

            Else
                If IntoSth(i).ToString.ToLower.Contains(Sth.ToString.ToLower) Then
                    Result = True
                    Exit For
                End If
            End If
        Next

        Return Result
    End Function
    Public Function isContained(Of T)(ByVal Sth As T, ByVal IntoSthElse As List(Of T), Optional ByVal CaseSensitive As Boolean = True, Optional ByVal TheSthIsTheWholeWordIntoSthElse As Boolean = False) As Boolean
        Dim Result As Boolean

        For i = 0 To IntoSthElse.Count - 1
            If CaseSensitive Then
                If TheSthIsTheWholeWordIntoSthElse Then
                    If IntoSthElse(i).ToString = Sth.ToString Then
                        Result = True
                        Exit For
                    End If

                Else
                    If IntoSthElse(i).ToString.Contains(Sth.ToString) Then
                        Result = True
                        Exit For
                    End If
                End If

            Else
                If TheSthIsTheWholeWordIntoSthElse Then
                    If IntoSthElse(i).ToString.ToLower = Sth.ToString.ToLower Then
                        Result = True
                        Exit For
                    End If

                Else
                    If IntoSthElse(i).ToString.ToLower.Contains(Sth.ToString.ToLower) Then
                        Result = True
                        Exit For
                    End If
                End If
            End If
        Next

        Return Result
    End Function
    Public Function isContained(Of T)(ByVal Sth As T, ByVal IntoSth As T, Optional ByVal CaseSensitive As Boolean = True) As Boolean
        Dim Result As Boolean

        If CaseSensitive Then
            If IntoSth.ToString.Contains(Sth.ToString) Then Result = True

        Else
            If IntoSth.ToString.ToLower.Contains(Sth.ToString.ToLower) Then Result = True
        End If

        Return Result
    End Function
#End Region

#Region "Read List"
    Public Function ReadList(ByVal str As String, ByVal Trim As Boolean, ByRef Succeeded As Boolean) As List(Of String)
        Dim Result As New List(Of String)
        Try
            Dim IndexOfFirstBracket As Integer = str.Replace(" ", "").IndexOf("{") 'Not the real one, this has no spaces
            If IndexOfFirstBracket <> -1 Then
                Dim tmpStrAr() As String = {}

                'If the string is delimited by "," Then proceed normally
                If str.Replace(" ", "")(IndexOfFirstBracket + 1) <> """" Then
                    Dim OpenBrackets As Integer = Count("{"c, str)
                    Dim ClosedBrackets As Integer = Count("}"c, str)

                    If OpenBrackets = 1 AndAlso ClosedBrackets = 1 Then
                        tmpStrAr = str.Replace("{", "").Replace("}", "").Split(","c)
                    Else
                        Succeeded = False
                    End If

                Else 'If there is "," inside a string, then the string begins and ends with "", so things change
                    Dim QuotesIndexes As List(Of Integer) = FindIndexes("""", str)
                    If QuotesIndexes.Count > 0 AndAlso QuotesIndexes.Count Mod 2 = 0 Then
                        ReDim tmpStrAr(CInt(QuotesIndexes.Count / 2) - 1)
                        For i = 0 To CInt(QuotesIndexes.Count / 2) - 1
                            tmpStrAr(i) = str.Substring(QuotesIndexes.Item(i * 2) + 1, (QuotesIndexes.Item((i * 2) + 1) - QuotesIndexes.Item(i * 2) - 1))
                        Next

                    Else
                        Succeeded = False
                    End If
                End If

                For Each Strng In tmpStrAr
                    If Trim Then
                        Result.Add(Strng.Trim)
                    Else
                        Result.Add(Strng)
                    End If
                Next
                Succeeded = True

            Else
                Succeeded = False
            End If

            Return Result

        Catch ex As Exception
            Return Result
            Succeeded = False
        End Try
    End Function
    Public Function ReadList(ByVal str As String, ByVal Trim As Boolean, ByRef Succeeded As Boolean, ByVal OnlyNumeric As Boolean) As List(Of Integer)
        Dim Result As New List(Of Integer)
        Try
            Dim IndexOfFirstBracket As Integer = str.Replace(" ", "").IndexOf("{") 'Not the real one, this has no spaces
            If IndexOfFirstBracket <> -1 Then
                Dim tmpStrAr() As String = {}

                'If the string is delimited by "," Then proceed normally
                If str.Replace(" ", "")(IndexOfFirstBracket + 1) <> """" Then
                    Dim OpenBrackets As Integer = Count("{"c, str)
                    Dim ClosedBrackets As Integer = Count("}"c, str)

                    If OpenBrackets = ClosedBrackets Then
                        tmpStrAr = str.Replace("{", "").Replace("}", "").Split(","c)
                    Else
                        Succeeded = False
                    End If

                Else 'If there is "," inside a string, then the string begind and ends with "", so things change
                    Dim QuotesIndexes As List(Of Integer) = FindIndexes("""", str)
                    If QuotesIndexes.Count > 0 AndAlso QuotesIndexes.Count Mod 2 = 0 Then
                        ReDim tmpStrAr(CInt(QuotesIndexes.Count / 2) - 1)
                        For i = 0 To CInt(QuotesIndexes.Count / 2) - 1
                            tmpStrAr(i) = str.Substring(QuotesIndexes.Item(i * 2) + 1, (QuotesIndexes.Item((i * 2) + 1) - QuotesIndexes.Item(i * 2) - 1))
                        Next

                    Else
                        Succeeded = False
                    End If
                End If

                For Each Intg In tmpStrAr
                    If Trim Then
                        Result.Add(CInt(Intg.Trim))
                    Else
                        Result.Add(CInt(Intg))
                    End If
                Next
                Succeeded = True

            Else
                Succeeded = False
            End If

            Return Result

        Catch ex As Exception
            Succeeded = False
            Return Result
        End Try
    End Function
    Public Function ReadList(ByVal str As String, ByVal Trim As Boolean, ByRef Succeeded As Boolean, ByVal dec As Decimal) As List(Of Decimal)
        Dim Result As New List(Of Decimal)
        Try
            Dim IndexOfFirstBracket As Integer = str.Replace(" ", "").IndexOf("{") 'Not the real one, this has no spaces
            If IndexOfFirstBracket <> -1 Then
                Dim tmpStrAr() As String = {}

                'If the string is delimited by "," Then proceed normally
                If Not str.Contains(""""c) Then
                    Dim OpenBrackets As Integer = Count("{"c, str)
                    Dim ClosedBrackets As Integer = Count("}"c, str)

                    If OpenBrackets = ClosedBrackets Then
                        tmpStrAr = str.Replace("{", "").Replace("}", "").Split(","c)
                    Else
                        Succeeded = False
                    End If

                Else 'If there is "," inside a string, then the string begind and ends with "", so things change
                    Dim QuotesIndexes As List(Of Integer) = FindIndexes("""", str)
                    If QuotesIndexes.Count > 0 AndAlso QuotesIndexes.Count Mod 2 = 0 Then
                        ReDim tmpStrAr(CInt(QuotesIndexes.Count / 2) - 1)
                        For i = 0 To CInt(QuotesIndexes.Count / 2) - 1
                            tmpStrAr(i) = str.Substring(QuotesIndexes.Item(i * 2) + 1, (QuotesIndexes.Item((i * 2) + 1) - QuotesIndexes.Item(i * 2) - 1))
                        Next

                    Else
                        Succeeded = False
                    End If
                End If


                For Each Intg In tmpStrAr
                    If Trim Then
                        Result.Add(CDec(Intg.Trim))
                    Else
                        Result.Add(CDec(Intg))
                    End If
                Next
                Succeeded = True

            Else
                Succeeded = False
            End If

            Return Result

        Catch ex As Exception
            Succeeded = False
            Return Result
        End Try
    End Function

    Public Function ReadListAr(ByVal str As String, ByVal Trim As Boolean, ByRef Succeeded As Boolean) As List(Of List(Of String)) 'On error Returns empty list
        Dim Result As New List(Of List(Of String))

        Try
            Dim OpenBrackets As Integer = Count("{"c, str)
            Dim ClosedBrackets As Integer = Count("}"c, str)

            If OpenBrackets = ClosedBrackets Then
                Dim strAr() As String = str.Split("{"c)
                For j = 1 To strAr.Length - 1
                    Dim Length As Integer = strAr(j).IndexOf("}"c) + 1
                    strAr(j) = "{" & strAr(j).Substring(0, Length)

                    Dim tmpResult As List(Of String) = ReadList(strAr(j), Trim, Succeeded)
                    Result.Add(tmpResult)
                Next

                Succeeded = True
            Else
                Succeeded = False
            End If

            Return Result

        Catch ex As Exception
            Succeeded = False
            Return Result
        End Try
    End Function
    Public Function ReadListAr(ByVal str As String, ByVal Trim As Boolean, ByRef Succeeded As Boolean, ByVal OnlyNumeric As Boolean) As List(Of List(Of Integer)) 'On error Returns empty list
        Dim Result As New List(Of List(Of Integer))

        Try
            Dim OpenBrackets As Integer = Count("{"c, str)
            Dim ClosedBrackets As Integer = Count("}"c, str)

            If OpenBrackets = ClosedBrackets Then
                Dim intAr() As String = str.Split("{"c)
                For j = 1 To intAr.Length - 1
                    Dim Length As Integer = intAr(j).IndexOf("}"c) + 1
                    intAr(j) = "{" & intAr(j).Substring(0, Length)

                    Dim tmpResult As List(Of Integer) = ReadList(intAr(j), Trim, Succeeded, OnlyNumeric)
                    Result.Add(tmpResult)
                Next
                Succeeded = True

            Else
                Succeeded = False
            End If

            Return Result

        Catch ex As Exception
            Succeeded = False
            Return Result
        End Try
    End Function
    Public Function ReadListAr(ByVal str As String, ByVal Trim As Boolean, ByRef Succeeded As Boolean, ByVal dec As Decimal) As List(Of List(Of Decimal)) 'On error Returns empty list
        Dim Result As New List(Of List(Of Decimal))

        Try
            Dim OpenBrackets As Integer = Count("{"c, str)
            Dim ClosedBrackets As Integer = Count("}"c, str)

            If OpenBrackets = ClosedBrackets Then
                Dim intAr() As String = str.Split("{"c)
                For j = 1 To intAr.Length - 1
                    Dim Length As Integer = intAr(j).IndexOf("}"c) + 1
                    intAr(j) = "{" & intAr(j).Substring(0, Length)

                    Dim tmpResult As List(Of Decimal) = ReadList(intAr(j), Trim, Succeeded, dec)
                    Result.Add(tmpResult)
                Next
                Succeeded = True

            Else
                Succeeded = False
            End If

            Return Result

        Catch ex As Exception
            Succeeded = False
            Return Result
        End Try
    End Function
#End Region

#Region "Create List/Array-String"
    Public Function CreateListString(Of T)(ParamArray lst() As List(Of T)) As String
        Dim StrBuilder As New StringBuilder

        StrBuilder.Append("{"c)
        If lst IsNot Nothing AndAlso lst.Count > 0 AndAlso lst(0).Count > 0 Then
            Dim AllListsHaveMinimumIndexesGr8erEqualToFirstList As Boolean = True
            For Each subList In lst
                If subList.Count < lst(0).Count Then
                    AllListsHaveMinimumIndexesGr8erEqualToFirstList = False
                    Exit For
                End If
            Next

            If AllListsHaveMinimumIndexesGr8erEqualToFirstList Then
                For i = 0 To lst(0).Count - 1
                    StrBuilder.Append("""")
                    For Each subList In lst
                        StrBuilder.Append(subList.Item(i).ToString)
                    Next
                    StrBuilder.Append(""", ")
                Next
                StrBuilder.Remove(StrBuilder.ToString.Length - ", ".Length, ", ".Length)
            End If

        End If
        StrBuilder.Append("}"c)

        Return StrBuilder.ToString
    End Function
    Public Function CreateArrayString(Of T)(ByVal Array As T()) As String
        Dim StrBuilder As New StringBuilder

        StrBuilder.Append("{"c)
        If Array IsNot Nothing AndAlso Array.Length > 0 Then
            For i = 0 To Array.Length - 1
                StrBuilder.Append("""" & Array(i).ToString & """, ")
            Next
            StrBuilder.Remove(StrBuilder.ToString.Length - ", ".Length, ", ".Length)
        End If
        StrBuilder.Append("}"c)

        Return StrBuilder.ToString
    End Function

    Public Function CreateListStringAr(Of T)(ByVal lst As List(Of List(Of T))) As String
        Dim StrBuilder As New StringBuilder

        StrBuilder.Append("("c)
        If lst IsNot Nothing AndAlso lst.Count > 0 Then
            For i = 0 To lst.Count - 1
                StrBuilder.Append(CreateListString(lst.Item(i))).Append(", ")
            Next
            StrBuilder.Remove(StrBuilder.ToString.Length - ", ".Length, ", ".Length)
        End If
        StrBuilder.Append(")"c)

        Return StrBuilder.ToString
    End Function
    Public Function CreateArrayStringAr(Of T)(ByVal Array()() As T) As String
        Dim StrBuilder As New StringBuilder

        StrBuilder.Append("("c)
        If Array IsNot Nothing AndAlso Array.Length > 0 Then
            For i = 0 To Array.Length - 1
                StrBuilder.Append(CreateArrayString(Array(i))).Append(" ,")
            Next
            StrBuilder.Remove(StrBuilder.ToString.Length - ", ".Length, ", ".Length)
        End If
        StrBuilder.Append(")"c)

        Return StrBuilder.ToString
    End Function
#End Region

#Region "Count"
    Public Function Count(ByVal CharToCount As Char, ByVal StrToSearch As String) As Integer
        Dim Result As Integer = 0

        For i = 0 To StrToSearch.Length - 1
            If StrToSearch(i) = CharToCount Then Result += 1
        Next

        Return Result
    End Function
    Public Function Count(ByVal strToCount As String, ByVal StrToSearch As String) As Integer
        Dim Result As Integer = 0

        For i = 0 To StrToSearch.Length - 1 - (strToCount.Length - 1)
            If StrToSearch.Substring(i, strToCount.Length) = strToCount Then Result += 1
        Next

        Return Result
    End Function
    Public Function Count(ByVal CharToCount As Char, ByVal StrToSearch() As String) As Integer
        Dim Result As Integer = 0

        For j = 0 To StrToSearch.Length - 1
            For i = 0 To StrToSearch(j).Length - 1
                If StrToSearch(j)(i) = CharToCount Then Result += 1
            Next
        Next

        Return Result
    End Function
    Public Function Count(ByVal strToCount As String, ByVal StrToSearch() As String) As Integer
        Dim Result As Integer = 0

        For j = 0 To StrToSearch.Length - 1
            For i = 0 To StrToSearch(j).Length - 1 - (strToCount.Length - 1)
                If StrToSearch(j).Substring(i, strToCount.Length) = strToCount Then Result += 1
            Next
        Next

        Return Result
    End Function
#End Region

#Region "Find Index(es)"
    Public Function FindIndex(Of T)(ByVal StrToFindIndexOf As String, ByVal StrToSearch As T, Optional ByVal CaseSensitive As Boolean = False) As Integer
        If CaseSensitive Then Return StrToSearch.ToString.IndexOf(StrToFindIndexOf) Else Return StrToSearch.ToString.ToLower.IndexOf(StrToFindIndexOf.ToLower)
    End Function

    Public Function FindIndexes(Of T)(ByVal StrToFindIndexOf As String, ByVal StrToSearch As T, Optional ByVal CaseSensitive As Boolean = False) As List(Of Integer)
        Dim Result As New List(Of Integer)

        Dim LastFoundIndex As Integer = 0

        Do While True
            Dim CurrentIndex As Integer = -1
            If CaseSensitive Then
                CurrentIndex = StrToSearch.ToString.IndexOf(StrToFindIndexOf, LastFoundIndex)
            Else
                CurrentIndex = StrToSearch.ToString.ToLower.IndexOf(StrToFindIndexOf.ToLower, LastFoundIndex)
            End If

            If CurrentIndex <> -1 Then
                LastFoundIndex = CurrentIndex + 1
                Result.Add(CurrentIndex)
            Else
                Exit Do 'If there are no more indexes (FindNextIndex)
            End If
        Loop

        Return Result
    End Function

    Public Function FindIndex(Of T)(ByVal strtoFindIndexOf As String, ByVal lstToSearch As List(Of T), Optional ByVal CaseSensitive As Boolean = False) As Integer
        Dim Index As Integer = -1

        For i = 0 To lstToSearch.Count - 1
            If CaseSensitive Then
                If lstToSearch.Item(i).ToString = strtoFindIndexOf Then
                    Index = i
                    Exit For
                End If
            Else
                If lstToSearch.Item(i).ToString.ToLower = strtoFindIndexOf.ToLower Then
                    Index = i
                    Exit For
                End If
            End If
        Next

        Return Index
    End Function

    Public Function FindIndexes(Of T)(ByVal strtoFindIndexOf As String, ByVal lstToSearch As List(Of T), Optional ByVal CaseSensitive As Boolean = False) As List(Of Integer)
        Dim Index As New List(Of Integer)

        For i = 0 To lstToSearch.Count - 1
            If CaseSensitive Then
                If lstToSearch.Item(i).ToString = strtoFindIndexOf Then
                    Index.Add(i)
                End If
            Else
                If lstToSearch.Item(i).ToString.ToLower = strtoFindIndexOf.ToLower Then
                    Index.Add(i)
                End If
            End If
        Next

        Return Index
    End Function

#End Region

    Public Function ProperString(str As String) As String
        Return StrConv(str, VbStrConv.ProperCase)
    End Function

    Public Function RangeArrayToLINQstring(ByVal VariableName As String, ByVal RangeArrayString As String, Optional ByVal OperatorAndOr As String = "OR") As String
        Dim ResultSB As New StringBuilder
        Dim Succeeded As Boolean = False
        Dim FromInt As Integer
        Dim ToInt As Integer

        If VariableName IsNot Nothing AndAlso VariableName.Trim <> "" AndAlso RangeArrayString.Contains("{") AndAlso RangeArrayString.Contains("}") Then
            Dim Ranges As List(Of String) = ReadList(RangeArrayString, True, Succeeded)
            If Succeeded Then
                ResultSB.Append("( ")

                For Each Strg In Ranges
                    If Strg <> "" Then
                        If Strg(0) = "!" AndAlso isNumRangeReturned(Strg.Substring(1), FromInt, ToInt) Then
                            ResultSB.Append("( " & VariableName & " < " & FromInt & " AND " & VariableName & " > " & ToInt & " ) " & OperatorAndOr & "")

                        ElseIf Left(Strg, 2) = "<>" AndAlso isNumRangeReturned(Strg.Substring(2), FromInt, ToInt) Then
                            ResultSB.Append("( " & VariableName & " < " & FromInt & " AND " & VariableName & " > " & ToInt & " ) " & OperatorAndOr & "")

                        ElseIf isNumRangeReturned(Strg, FromInt, ToInt) Then
                            ResultSB.Append("( " & VariableName & " >= " & FromInt & " AND " & VariableName & " <= " & ToInt & " ) " & OperatorAndOr & "")

                        ElseIf IsNumeric(Strg) OrElse (Strg(0) = "=" AndAlso IsNumeric(Strg.Substring(1))) Then
                            ResultSB.Append(VariableName & " = " & Strg & " " & OperatorAndOr & " ")

                        ElseIf Strg(0) = "!" AndAlso IsNumeric(Strg.Substring(1)) Then
                            ResultSB.Append(VariableName & " <> " & Strg & " " & OperatorAndOr & " ")

                        ElseIf Left(Strg, 2) = "<>" AndAlso IsNumeric(Strg.Substring(2)) Then
                            ResultSB.Append(VariableName & " <> " & Strg & " " & OperatorAndOr & " ")
                        End If
                    End If
                Next

                If ResultSB.ToString.Length > (" " & OperatorAndOr & " ").Length Then ResultSB = ResultSB.Remove(ResultSB.Length - OperatorAndOr.Length - 1, OperatorAndOr.Length + 1)

                ResultSB.Append(" )")
            End If
        End If

        Return ResultSB.ToString
    End Function

    Public Function IsOneOfTheFollowing(Of T)(ByVal What As T, ListOfAcceptableValues() As T, Optional ByVal CaseSensitive As Boolean = False) As Boolean

        If Not CaseSensitive Then
            For i = 0 To ListOfAcceptableValues.Length - 1
                If What.ToString.ToLower = ListOfAcceptableValues(i).ToString.ToLower Then Return True
            Next
        Else
            For i = 0 To ListOfAcceptableValues.Length - 1
                If What.ToString = ListOfAcceptableValues(i).ToString Then Return True
            Next
        End If

        Return False
    End Function

    Public Function TriangleMatrix(Of T)(ByVal DataSetNames As String(), ByVal Variables As T(), ByVal ItemsLength As Integer, Optional ByVal RoundNumber As Integer = -1, Optional ByVal SignificanceLevel As Double = -1.0#, Optional ByVal ReturnedDataSetNames As List(Of String) = Nothing) As String()
        Dim lstNewDataSetNames As New List(Of String)
        Dim strTriangle(DataSetNames.Length) As String
        Dim RunCount As Integer = -1

        strTriangle(0) = SpaceAString(" ", ItemsLength) & " "

        'Filling In The Data Set Names(Horizontal & Vertical)
        For i As Integer = 0 To DataSetNames.Length - 1
            lstNewDataSetNames.Add(SpaceAString(DataSetNames(i), ItemsLength, "Bottom"))
            strTriangle(0) &= lstNewDataSetNames(lstNewDataSetNames.Count - 1) & ControlChars.Tab
            strTriangle(i + 1) = lstNewDataSetNames(lstNewDataSetNames.Count - 1) & ControlChars.Tab

            'Filling The Spaces Till The Values
            For j As Integer = 0 To i
                If j = i Then
                    For k As Integer = 1 To i
                        strTriangle(i + 1) &= SpaceAString(" ", ItemsLength) & ControlChars.Tab
                    Next
                    strTriangle(i + 1) &= SpaceAString("", CInt(ItemsLength / 2) - 1) & "-" & SpaceAString("", CInt(ItemsLength / 2)) & ControlChars.Tab
                End If
            Next
        Next

        'Filling In The Values
        For i As Integer = 0 To DataSetNames.Length - 2
            For j As Integer = 0 To DataSetNames.Length - 2 - i
                Dim CurString As String

                RunCount += 1
                If IsNumeric(Variables(RunCount).ToString) Then
                    Dim CurRoundNum As Integer = RoundNumber

                    If CDbl(Variables(RunCount).ToString) < SignificanceLevel AndAlso CurRoundNum - 1 > 2 Then CurRoundNum -= 1 'Which means the lower rounding should be 3, so 3-1(for the *) = >1

                    CurString = Math.Round(CDbl(Variables(RunCount).ToString), CurRoundNum).ToString
                    If CDbl(Variables(RunCount).ToString) < SignificanceLevel Then CurString &= "*"

                    CurString = SpaceAString(CurString, ItemsLength) & ControlChars.Tab

                Else
                    CurString = SpaceAString(Variables(RunCount).ToString, ItemsLength)
                End If

                strTriangle(i + 1) &= CurString

            Next
        Next

        ReturnedDataSetNames = lstNewDataSetNames
        Return strTriangle
    End Function

#Region "Find Delimiter"
    Public Function FindDelimiter(ByVal strNumericRange As String) As String
        Return DelimiterFinder(strNumericRange)
    End Function
    Public Function DelimiterFinder(ByVal strNumericRange As String) As String
        Dim Delimiter As String = String.Empty

        If strNumericRange.Contains(" " & strModLanguage(44) & " ") Then 'Range "to"
            Delimiter = " " & strModLanguage(44) & " " '" to " (for example 1 to 5)
        ElseIf strNumericRange.Contains(" " & strModLanguage(44) & " ") Then
            Delimiter = strModLanguage(44) & " " '" to " (for example 1to 5)
        ElseIf strNumericRange.Contains(" " & strModLanguage(44)) Then
            Delimiter = " " & strModLanguage(44) '" to " (for example 1 to5)
        ElseIf strNumericRange.Contains(strModLanguage(44)) Then
            Delimiter = strModLanguage(44) 'to (for example 1to5)
        ElseIf strNumericRange.Contains(" , ") Then
            Delimiter = " , " 'to (for example 1 , 5)
        ElseIf strNumericRange.Contains(" ,") Then
            Delimiter = " , " 'to (for example 1 ,5)
        ElseIf strNumericRange.Contains(", ") Then
            Delimiter = ", " 'to (for example 1, 5)
        ElseIf strNumericRange.Contains(",") Then
            Delimiter = "," 'to (for example 1,5)
        End If

        Return Delimiter
    End Function
#End Region

#Region "DataTables"
    Public Sub AddRowToDatatable(ByRef DataTable As DataTable, ParamArray Values() As String)
        AddColumnsToDatatable(DataTable, Values.Length - 1)

        Dim NewDataRow As DataRow = DataTable.NewRow()
        For i = 0 To Values.Length - 1
            NewDataRow(i) = Values(i)
        Next
        DataTable.Rows.Add(NewDataRow)
    End Sub

    Public Sub AddLinesToDatatable(ByRef DataTable As DataTable, Optional NumberOfLine As Integer = 1)
        For i = 0 To NumberOfLine - 1
            Dim NewDataRow As DataRow = DataTable.NewRow()
            DataTable.Rows.Add(NewDataRow)
        Next i
    End Sub

    Public Sub AddColumnsToDatatable(ByRef DataTable As DataTable, Optional ByVal MaxColumnsNeeded As Integer = 0) 'Does not need a "Handles"
        'Figuring out how many Columns we have to add
        If (DataTable.Columns.Count - 1) < MaxColumnsNeeded Then
            For i As Integer = DataTable.Columns.Count To MaxColumnsNeeded
                DataTable.Columns.Add("Column " & i + 1, GetType(String))    'Column is a NAME not a text, so stays the same
            Next
        End If
    End Sub

    Public Sub AppendWholeColumnsToDatatable(Of T)(ByRef Datatable As DataTable, ByVal ColumnNames As List(Of String), ByVal ItemstoFill As List(Of T()))
        AppendWholeColumnsToDatatable(Datatable, ColumnNames.ToArray, ItemstoFill)
    End Sub

    Public Sub AppendWholeColumnsToDatatable(Of T)(ByRef Datatable As DataTable, ByVal ColumnNames() As String, ByVal ItemstoFill As List(Of T()))
        For i = 0 To ColumnNames.Length - 1
            AppendWholeColumnToDatatable(Datatable, ColumnNames(i), ItemstoFill.Item(i))
        Next
    End Sub

    Public Sub AppendWholeColumnToDatatable(Of T)(ByRef DataTable As DataTable, ByVal ColumnName As String, ByVal ItemsToFill As T())
        If DataTable Is Nothing Then DataTable = New DataTable

        Dim NewColumnName As String = CheckForDuplicateColumnNameOnDatatable(DataTable, ColumnName)
        DataTable.Columns.Add(NewColumnName)

        Dim CurColumnsCount As Integer = DataTable.Columns.Count
        Dim CurrentRowCount As Integer = DataTable.Rows.Count

        If ItemsToFill.Length > CurrentRowCount Then AddLinesToDatatable(DataTable, ItemsToFill.Length - CurrentRowCount)

        For i = 0 To ItemsToFill.Length - 1
            DataTable.Rows(i).Item(CurColumnsCount - 1) = ItemsToFill(i)
        Next i

    End Sub

    Public Function CheckForDuplicateColumnNameOnDatatable(ByVal DataTable As DataTable, ByVal SuggestedName As String, Optional ByVal StartNumber As Integer = 1) As String
        Dim Result As String = SuggestedName

        If DataTable IsNot Nothing AndAlso DataTable.Columns.Count > 0 Then
            For i As Integer = 0 To DataTable.Columns.Count - 1
                If DataTable.Columns(i).ColumnName.ToLower = SuggestedName.ToLower Then
                    If StartNumber = 1 Then
                        Result = CheckForDuplicateColumnNameOnDatatable(DataTable, Result & "_" & (StartNumber + 1), StartNumber + 1)
                    Else
                        Result = CheckForDuplicateColumnNameOnDatatable(DataTable, Mid(Result, 1, Result.Length - StartNumber.ToString.Length) & (StartNumber + 1), StartNumber + 1)
                    End If
                    Exit For
                End If
            Next
        End If

        Return Result
    End Function

    Public Function GetColumnsAsDoubleArray(ByVal dtVariables As DataTable, ByVal ColumnIndices As Integer(), Optional ByRef ColumnNames() As String = Nothing, Optional ByVal OrderAscending As Boolean = False, Optional CheckForValidArrayRange As Boolean = False) As List(Of Double())
        Dim Result As New List(Of Double())
        Dim DoubleArray() As Double
        Dim CurValidArrayLength As Integer = dtVariables.Rows.Count - 1

        If OrderAscending Then Array.Sort(ColumnIndices)

        For Each Index In ColumnIndices
            If CheckForValidArrayRange Then 'Returning values ONLY until the last valid Double number
                CurValidArrayLength = 0
                Do Until CurValidArrayLength = dtVariables.Rows.Count OrElse IsDBNull(dtVariables.Rows(CurValidArrayLength).Item(Index)) OrElse Not IsNumeric(dtVariables.Rows(CurValidArrayLength).Item(Index))
                    CurValidArrayLength += 1
                Loop
            End If

            ReDim DoubleArray(CurValidArrayLength - 1)
            For i = 0 To CurValidArrayLength - 1
                DoubleArray(i) = CDbl(dtVariables.Rows(i).Item(Index))
            Next
            Result.Add(DoubleArray)
        Next

        ColumnNames = GetColumnNamesOfDataTable(dtVariables, ColumnIndices, OrderAscending).ToArray

        Return Result
    End Function
    Public Function GetColumnAsDoubleArray(ByVal dtVariables As DataTable, ByVal ColumnIndex As Integer, Optional ByRef ColumnName As String = Nothing, Optional ByVal OrderAscending As Boolean = False, Optional CheckForValidArrayRange As Boolean = False) As Double()
        Dim Result() As Double
        Dim ColumnIndicies() As Integer = {ColumnIndex}

        Result = GetColumnsAsDoubleArray(dtVariables, ColumnIndicies, , OrderAscending, CheckForValidArrayRange)(0)

        ColumnName = dtVariables.Columns(ColumnIndex).ColumnName

        Return Result
    End Function

    Public Function GetColumnsAsStringArray(ByVal dtVariables As DataTable, ByVal ColumnIndices As Integer(), Optional ByRef ColumnNames() As String = Nothing, Optional ByVal OrderAscending As Boolean = False, Optional CheckForValidArrayRange As Boolean = False) As List(Of String())
        Dim Result As New List(Of String())
        Dim StringArray() As String
        Dim CurValidArrayLength As Integer = dtVariables.Rows.Count - 1

        If OrderAscending Then Array.Sort(ColumnIndices)

        For Each Index In ColumnIndices
            If CheckForValidArrayRange Then 'Returning values ONLY until the last valid string (not dbNull)
                CurValidArrayLength = 0
                Do Until CurValidArrayLength = dtVariables.Rows.Count OrElse IsDBNull(dtVariables.Rows(CurValidArrayLength).Item(Index)) OrElse CStr(dtVariables.Rows(CurValidArrayLength).Item(Index)) = String.Empty
                    CurValidArrayLength += 1
                Loop
            End If

            ReDim StringArray(CurValidArrayLength - 1)
            For i = 0 To CurValidArrayLength - 1
                StringArray(i) = dtVariables.Rows(i).Item(Index).ToString
            Next
            Result.Add(StringArray)
        Next

        ColumnNames = GetColumnNamesOfDataTable(dtVariables, ColumnIndices, OrderAscending).ToArray

        Return Result
    End Function
    Public Function GetColumnAsStringArray(ByVal dtVariables As DataTable, ByVal ColumnIndex As Integer, Optional ByRef ColumnName As String = Nothing, Optional ByVal OrderAscending As Boolean = False, Optional CheckForValidArrayRange As Boolean = False) As String()
        Dim Result() As String
        Dim ColumnIndicies() As Integer = {ColumnIndex}

        Result = GetColumnsAsStringArray(dtVariables, ColumnIndicies, , OrderAscending, CheckForValidArrayRange)(0)

        ColumnName = dtVariables.Columns(ColumnIndex).ColumnName

        Return Result
    End Function

    Public Function GetColumnNamesOfDataTable(ByVal dtVariables As DataTable, ByVal ColumnIndices As Integer(), Optional ByVal OrderAscending As Boolean = False) As List(Of String)
        Dim Results As New List(Of String)

        If OrderAscending Then Array.Sort(ColumnIndices)

        For Each Index In ColumnIndices
            Results.Add(dtVariables.Columns(Index).ColumnName)
        Next

        Return Results
    End Function

#End Region

#Region "Erase Null Lines"
    Public Function EraseNullLines(ByVal str As String) As String
        Dim Result As New List(Of String)
        Dim Lines() As String = SplitByNewLine(str, True)

        Return ArrayBox(1, Lines)
    End Function
    Public Function EraseNullLines(ByVal Lines As String()) As String()
        Dim Result As New List(Of String)

        For Each Line In Lines
            If Line <> "" Then Result.Add(Line)
        Next

        Return Result.ToArray
    End Function
    Public Function EraseNullLines(ByVal Lines As List(Of String)) As List(Of String)
        Dim Result As New List(Of String)

        For Each Line In Lines
            If Line <> "" Then Result.Add(Line)
        Next

        Return Result
    End Function
#End Region

    Public Function SplitByNewLine(ByVal Str As String, Optional ByVal RemoveEmptyEntries As Boolean = False) As String()
        If RemoveEmptyEntries Then
            Return Str.Split(New String() {System.Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
        Else
            Return Str.Split(New String() {System.Environment.NewLine}, StringSplitOptions.None)
        End If
    End Function

#Region "Please Wait Form"
    Public Sub PleaseWait(Optional ByVal InfoText As String = "", Optional ShowDialog As Boolean = False)
        If CurPleaseWaitForm IsNot Nothing Then
            CurPleaseWaitForm.Close()
            CurPleaseWaitForm = Nothing

        Else
            CurPleaseWaitForm = New frmPleaseWait
            CurPleaseWaitForm.strInfoText = InfoText
            If ShowDialog Then ShowDialogForm(CurPleaseWaitForm) Else ShowForm(CurPleaseWaitForm)
        End If
    End Sub
    Public Sub ShowPleaseWait(Optional ByVal InfoText As String = "", Optional ShowDialog As Boolean = False)
        PleaseWait(InfoText, ShowDialog)
    End Sub
    Public Sub ShowPleaseWaitForm(Optional ByVal InfoText As String = "", Optional ShowDialog As Boolean = False)
        PleaseWait(InfoText, ShowDialog)
    End Sub

    Public Sub ClosePleaseWaitForm()
        If CurPleaseWaitForm IsNot Nothing Then
            CurPleaseWaitForm.Close()
            CurPleaseWaitForm = Nothing
        End If
    End Sub

#End Region

#Region "Fixed String Length"
    Public Function FixedStringLength(Of T)(ByVal Var() As T) As List(Of String)
        Dim Result As New List(Of String)
        Dim MaxLength As Integer = (From Str In Var Order By Str.ToString.Length Descending).FirstOrDefault.ToString.Length

        If MaxLength > 0 Then
            For i = 0 To Var.Length - 1
                Result.Add(SpaceAString(Var(i).ToString, MaxLength))
            Next
        End If

        Return Result
    End Function

    Public Function FixedStringLength(ByVal Str() As String, ByVal MaxLength As Integer, Optional ByVal RemoveRight As Boolean = False, Optional ByVal RemoveLeft As Boolean = False) As String()
        Dim Result() As String = Str

        Dim RemoveMethod As String = "None"
        If RemoveRight Then
            RemoveMethod = "Right"
        ElseIf RemoveLeft Then
            RemoveMethod = "Left"
        End If

        If MaxLength > 0 Then
            For i = 0 To Result.Length - 1
                Result(i) = SpaceAString(Result(i), MaxLength, RemoveMethod)
            Next
        End If

        Return Result
    End Function
#End Region

#Region "To String Array"
    Public Function ToStringArray(Of T)(ByVal Var As T()) As String()
        Return ToStringArray(Var.ToList).ToArray
    End Function
    Public Function ToStringArray(Of T)(ByVal Var As List(Of T)) As List(Of String)
        Dim Result As New List(Of String)

        For Each Item In Var
            Result.Add(Item.ToString)
        Next

        Return Result
    End Function
#End Region

    Public Sub Unrar(ByVal RarredFile As String, ByVal ToDirectory As String, Optional ByVal Password As String = "", Optional ByVal WaitForExit As Boolean = False, Optional ByVal ErrorMessage As String = "", Optional ByVal CannotFindFile As String = "Cannot find the specified file: ", Optional ByVal WithArguments As String = " with arguments: ", Optional ByVal WriteCrushFileAnyway As Boolean = False)
        Dim ExtractionStr As String = String.Empty

        If Password = String.Empty Then
            ExtractionStr = "x -e -o+ """ & RarredFile & """ """ & ToDirectory & """"
        Else
            ExtractionStr = "x -p""" & Password & """ -e -o+ """ & RarredFile & """ """ & ToDirectory & """"
        End If

        RunOpenDir(strUnrar, ExtractionStr, WaitForExit, ErrorMessage, CannotFindFile, WithArguments, WriteCrushFileAnyway)
    End Sub

    Public Sub RestartApplication(Optional ByVal Argument As String = "none") 'Should always have a "Exit Sub" on the sub that calls it!
        'Requires StartUp.exe Version 2.2.0.1208
        RunOpenDir(strStartupExe, """" & Application.ExecutablePath & """ """ & Argument & """")
        Application.Exit()
    End Sub

#Region "File/Folder Operations"
    Public Sub RenFileFolder(ByVal FileFolderCurrentPath As String, ByVal FileFolderNewPath As String, Optional ByVal WaitForRenaming As Boolean = True)
        FileFolderCurrentPath = FileFolderCurrentPath.Replace("""", "")
        FileFolderNewPath = FileFolderNewPath.Replace("""", "")
        'RunOpenDir(strUnlocker, """" & FileFolderCurrentPath & """ /S /R """ & FileFolderNewPath & """", WaitForRenaming) ''''

        If File.Exists(FileFolderCurrentPath) Then
            FileFolderNewPath = Mid(FileFolderNewPath, FileFolderNewPath.LastIndexOf("\") + 2, FileFolderNewPath.Length - (FileFolderNewPath.LastIndexOf("\") + 1))
            My.Computer.FileSystem.RenameFile(FileFolderCurrentPath, FileFolderNewPath)
        ElseIf Directory.Exists(FileFolderCurrentPath) Then
            FileFolderNewPath = Mid(FileFolderNewPath, FileFolderNewPath.LastIndexOf("\") + 2, FileFolderNewPath.Length - (FileFolderNewPath.LastIndexOf("\") + 1))
            My.Computer.FileSystem.RenameDirectory(FileFolderCurrentPath, FileFolderNewPath)
        End If
    End Sub

    Public Async Sub DelFileFolder(ByVal FileFolderPath As String, Optional ByVal WaitForDeletion As Boolean = True, Optional ByVal WaitSeconds As Integer = 0)
        FileFolderPath = FileFolderPath.Replace("""", "")
        If WaitSeconds <> 0 Then
            Await Task.Run(Sub()
                               Thread.Sleep(WaitSeconds)
                           End Sub)
        End If
        'RunOpenDir(strUnlocker, """" & FileFolderPath & """ /S /D", WaitForDeletion) ''''
        If File.Exists(FileFolderPath) Then
            File.Delete(FileFolderPath)
        ElseIf Directory.Exists(FileFolderPath) Then
            Directory.Delete(FileFolderPath, True)
        End If
    End Sub


    Public Sub MoveFileFolder(ByVal FileFolderCurrentPath As String, ByVal FileFolderNewPath As String, Optional ByVal WaitForRenaming As Boolean = True)
        FileFolderCurrentPath = FileFolderCurrentPath.Replace("""", "")
        FileFolderNewPath = FileFolderNewPath.Replace("""", "")

        'RunOpenDir(strUnlocker, """" & FileFolderCurrentPath & """ /S /M """ & FileFolderNewPath & """", WaitForRenaming) 'DOESNT WORK ON WIN XP ''''
        If File.Exists(FileFolderCurrentPath) Then
            File.Move(FileFolderCurrentPath, FileFolderNewPath)
        ElseIf Directory.Exists(FileFolderCurrentPath) Then
            Directory.Move(FileFolderCurrentPath, FileFolderNewPath)
        End If
    End Sub

    Public Sub UnlockFileFolder(ByVal FileFolderPath As String, Optional ByVal WaitForRenaming As Boolean = True)
        FileFolderPath = FileFolderPath.Replace("""", "")
        'RunOpenDir(strUnlocker, """" & FileFolderPath & """ /S""", WaitForRenaming) ''''
    End Sub
#End Region

#Region "(De)Cypherisation"
    Public Sub CypherVariablization()
        CharLevel(1) = ("1"c)
        CharLevel(2) = ("q"c)
        CharLevel(3) = ("a"c)
        CharLevel(4) = ("z"c)
        CharLevel(5) = ("2"c)
        CharLevel(6) = ("w"c)
        CharLevel(7) = ("s"c)
        CharLevel(8) = ("x"c)
        CharLevel(9) = ("3"c)
        CharLevel(10) = ("e"c)
        CharLevel(11) = ("d"c)
        CharLevel(12) = ("c"c)
        CharLevel(13) = ("4"c)
        CharLevel(14) = ("r"c)
        CharLevel(15) = ("f"c)
        CharLevel(16) = ("v"c)
        CharLevel(17) = ("5"c)
        CharLevel(18) = ("t"c)
        CharLevel(19) = ("g"c)
        CharLevel(20) = ("b"c)
        CharLevel(21) = ("6"c)
        CharLevel(22) = ("y"c)
        CharLevel(23) = ("h"c)
        CharLevel(24) = ("n"c)
        CharLevel(25) = ("7"c)
        CharLevel(26) = ("u"c)
        CharLevel(27) = ("j"c)
        CharLevel(28) = ("m"c)
        CharLevel(29) = ("8"c)
        CharLevel(30) = ("i"c)
        CharLevel(31) = ("k"c)
        CharLevel(32) = (","c)
        CharLevel(33) = ("9"c)
        CharLevel(34) = ("o"c)
        CharLevel(35) = ("l"c)
        CharLevel(36) = ("."c)
        CharLevel(37) = ("0"c)
        CharLevel(38) = ("p"c)
        CharLevel(39) = (";"c)
        CharLevel(40) = ("/"c)
        CharLevel(41) = ("-"c)
        CharLevel(42) = ("["c)
        CharLevel(43) = ("'"c)
        CharLevel(44) = ("`"c)
        CharLevel(45) = ("="c)
        CharLevel(46) = ("]"c)
        CharLevel(47) = ("\"c)
        CharLevel(48) = ("!"c)
        CharLevel(49) = ("Q"c)
        CharLevel(50) = ("A"c)
        CharLevel(51) = ("Z"c)
        CharLevel(52) = ("@"c)
        CharLevel(53) = ("W"c)
        CharLevel(54) = ("S"c)
        CharLevel(55) = ("X"c)
        CharLevel(56) = ("#"c)
        CharLevel(57) = ("E"c)
        CharLevel(58) = ("D"c)
        CharLevel(59) = ("C"c)
        CharLevel(60) = ("$"c)
        CharLevel(61) = ("R"c)
        CharLevel(62) = ("F"c)
        CharLevel(63) = ("V"c)
        CharLevel(64) = ("%"c)
        CharLevel(65) = ("T"c)
        CharLevel(66) = ("G"c)
        CharLevel(67) = ("B"c)
        CharLevel(68) = ("^"c)
        CharLevel(69) = ("Y"c)
        CharLevel(70) = ("H"c)
        CharLevel(71) = ("N"c)
        CharLevel(72) = ("&"c)
        CharLevel(73) = ("U"c)
        CharLevel(74) = ("J"c)
        CharLevel(75) = ("M"c)
        CharLevel(76) = ("*"c)
        CharLevel(77) = ("I"c)
        CharLevel(78) = ("K"c)
        CharLevel(79) = ("<"c)
        CharLevel(80) = ("("c)
        CharLevel(81) = ("O"c)
        CharLevel(82) = ("L"c)
        CharLevel(83) = (">"c)
        CharLevel(84) = (")"c)
        CharLevel(85) = ("P"c)
        CharLevel(86) = (":"c)
        CharLevel(87) = ("?"c)
        CharLevel(88) = ("_"c)
        CharLevel(89) = ("{"c)
        CharLevel(90) = (""""c)
        CharLevel(91) = ("~"c)
        CharLevel(92) = ("+"c)
        CharLevel(93) = ("}"c)
        CharLevel(94) = ("|"c)
    End Sub

    Public Function Cypher(ByVal TextToCypher As String(), ByVal CypherEncryptionNum As Integer) As String
        Dim CypheredText As String = ""
        Dim intCharLevel As Integer = -1

        Try
            For i As Integer = 0 To TextToCypher.Length - 1
                Dim chrLine() As Char = TextToCypher(i).ToCharArray

                For j As Integer = 0 To chrLine.Length - 1

                    For n As Integer = 1 To CharLevel.Length - 1
                        If chrLine(j) = CharLevel(n) Then
                            intCharLevel = n
                            Exit For
                        End If
                    Next

                    If intCharLevel <> -1 Then
                        If intCharLevel + CypherEncryptionNum < CharLevel.Length Then
                            CypheredText = CypheredText & CharLevel(intCharLevel + CypherEncryptionNum)

                        Else
                            CypheredText = CypheredText & CharLevel((intCharLevel + CypherEncryptionNum) - (CharLevel.Length - 1))

                        End If
                    Else
                        CypheredText = CypheredText & chrLine(j)
                    End If
                    intCharLevel = -1
                Next

                If i <> TextToCypher.Length - 1 Then
                    CypheredText = CypheredText & vbCrLf
                End If

            Next

        Catch ex As Exception
        End Try

        Return CypheredText
    End Function
    Public Function Cypher(ByVal TextToCypher As String, ByVal CypherEncryptionNum As Integer) As String
        Dim CypheredText As String = ""
        Dim intCharLevel As Integer = -1

        Try
            Dim chrLine() As Char = TextToCypher.ToCharArray

            For j As Integer = 0 To chrLine.Length - 1

                For n As Integer = 1 To CharLevel.Length - 1
                    If chrLine(j) = CharLevel(n) Then
                        intCharLevel = n
                        Exit For
                    End If
                Next

                If intCharLevel <> -1 Then
                    If intCharLevel + CypherEncryptionNum < CharLevel.Length Then
                        CypheredText = CypheredText & CharLevel(intCharLevel + CypherEncryptionNum)

                    Else
                        CypheredText = CypheredText & CharLevel((intCharLevel + CypherEncryptionNum) - (CharLevel.Length - 1))

                    End If
                Else
                    CypheredText = CypheredText & chrLine(j)
                End If
                intCharLevel = -1
            Next

        Catch ex As Exception
        End Try

        Return CypheredText
    End Function

    Public Function DecypherToText(ByVal TextToDecypher() As String, ByVal CypherEncryptionNum As Integer) As String
        Dim CypheredText As String = ""
        Dim intCharLevel As Integer = -1

        Try
            For i As Integer = 0 To TextToDecypher.Length - 1
                Dim chrLine() As Char = TextToDecypher(i).ToCharArray

                For j As Integer = 0 To chrLine.Length - 1

                    For n As Integer = 1 To CharLevel.Length - 1
                        If chrLine(j) = CharLevel(n) Then
                            intCharLevel = n
                            Exit For
                        End If
                    Next

                    If intCharLevel <> -1 Then
                        If intCharLevel - CypherEncryptionNum > 0 Then
                            CypheredText = CypheredText & CharLevel(intCharLevel - CypherEncryptionNum)

                        Else
                            CypheredText = CypheredText & CharLevel((intCharLevel - CypherEncryptionNum) + (CharLevel.Length - 1))

                        End If
                    Else
                        CypheredText = CypheredText & chrLine(j)
                    End If
                    intCharLevel = -1
                Next

                If i <> TextToDecypher.Length - 1 Then
                    CypheredText = CypheredText & vbCrLf
                End If

            Next

        Catch ex As Exception
        End Try

        Return CypheredText
    End Function
    Public Function DecypherToText(ByVal TextToDecypher As String, ByVal CypherEncryptionNum As Integer) As String
        Dim DeCypheredText As String = ""
        Dim intCharLevel As Integer = -1

        Try
            For j As Integer = 0 To TextToDecypher.Length - 1

                For n As Integer = 1 To CharLevel.Length - 1
                    If TextToDecypher(j) = CharLevel(n) Then
                        intCharLevel = n
                        Exit For
                    End If
                Next

                If intCharLevel <> -1 Then
                    If intCharLevel - CypherEncryptionNum > 0 Then
                        DeCypheredText &= CharLevel(intCharLevel - CypherEncryptionNum)

                    Else
                        DeCypheredText &= CharLevel((intCharLevel - CypherEncryptionNum) + (CharLevel.Length - 1))

                    End If
                Else
                    DeCypheredText &= TextToDecypher(j)
                End If
                intCharLevel = -1
            Next

        Catch ex As Exception
        End Try

        Return DeCypheredText
    End Function

    Public Function DecypherToArray(ByVal TextToDecypher() As String, ByVal CypherEncryptionNum As Integer) As String()
        Dim CypheredText(TextToDecypher.Length - 1) As String
        Dim intCharLevel As Integer = -1

        Try
            For i As Integer = 0 To TextToDecypher.Length - 1
                Dim chrLine() As Char = TextToDecypher(i).ToCharArray

                For j As Integer = 0 To chrLine.Length - 1

                    For n As Integer = 1 To CharLevel.Length - 1
                        If chrLine(j) = CharLevel(n) Then
                            intCharLevel = n
                            Exit For
                        End If
                    Next

                    If intCharLevel <> -1 Then
                        If intCharLevel - CypherEncryptionNum > 0 Then
                            CypheredText(i) = CypheredText(i) & CharLevel(intCharLevel - CypherEncryptionNum)

                        Else
                            CypheredText(i) = CypheredText(i) & CharLevel((intCharLevel - CypherEncryptionNum) + (CharLevel.Length - 1))
                        End If
                    Else
                        CypheredText(i) = CypheredText(i) & chrLine(j)
                    End If
                    intCharLevel = -1
                Next

            Next

        Catch ex As Exception
        End Try

        Return CypheredText
    End Function

    Public Function CeasarCipher(ByVal Str As String(), ByVal By As Integer) As String
        Dim Text As New StringBuilder

        Try
            For i = 0 To Str.Length - 1
                If i <> 0 Then Text.AppendLine()
                For j = 0 To Str(i).Length - 1
                    Dim Num As Integer = AscW(Str(i)(j)) + By
                    If Num > 65535 Then
                        Num = -32768 + (Num - 65535)
                    ElseIf Num < -32768 Then
                        Num = 65535 - (-32768 - Num)
                    End If

                    Text.Append(ChrW(Num))
                Next
            Next

        Catch ex As Exception
        End Try

        Return Text.ToString
    End Function

    Public Function CeasarDeCipher(ByVal Str As String(), ByVal by As Integer) As String
        Return CeasarCipher(Str, -by)
    End Function

#End Region

    Public Function CheckInternetAvailability(Optional ByVal TestUrl As String = "http://www.google.com") As Boolean
        If My.Computer.Network.IsAvailable AndAlso Not PreventInternetCheck Then
            ' Returns True if connection is available
            ' is guaranteed to be online - perhaps your
            ' corporate site, or microsoft.com
            Dim objUrl As New System.Uri(TestUrl)
            ' Setup WebRequest
            Dim objWebReq As System.Net.WebRequest
            objWebReq = System.Net.WebRequest.Create(objUrl)
            Dim objResp As System.Net.WebResponse
            Try
                ' Attempt to get response and return True
                objResp = objWebReq.GetResponse
                objResp.Close()
                objWebReq = Nothing
                Return True
            Catch ex As Exception
                ' Error, exit and return False
                objResp = Nothing
                'objResp.Close()
                objWebReq = Nothing
                Return False
            End Try

        Else
            Return False
        End If
    End Function

    Public Function doProperPathName(ByVal DirectoryPath As String) As String

        If DirectoryPath.Contains("\\") Then DirectoryPath = DirectoryPath.Replace("\\", "\")
        If DirectoryPath.Contains("/") Then DirectoryPath = DirectoryPath.Replace("/", "\")
        If Not DirectoryPath.EndsWith("\") Then DirectoryPath = DirectoryPath & "\"

        Return DirectoryPath
    End Function

    Public Function doProperFileName(ByVal FileName As String) As String
        If FileName.Contains("\\") Then FileName = FileName.Replace("\\", "\")
        If FileName.Contains("/") Then FileName = FileName.Replace("/", "\")
        If FileName.EndsWith("\") Then FileName = Mid(FileName, 1, FileName.Length - 1)

        Return FileName
    End Function

    Public Function doProperPathNameLinux(ByVal DirectoryPath As String) As String
        If DirectoryPath.Contains("\\") Then DirectoryPath = DirectoryPath.Replace("\\", "/")
        If DirectoryPath.Contains("\") Then DirectoryPath = DirectoryPath.Replace("\", "/")
        If DirectoryPath.Contains("//") Then DirectoryPath = DirectoryPath.Replace("//", "/")
        If Not DirectoryPath.EndsWith("/") Then DirectoryPath = DirectoryPath & "/"

        Return DirectoryPath
    End Function

    Public Function doProperFileNameLinux(ByVal FileName As String) As String
        If FileName.Contains("\\") Then FileName = FileName.Replace("\\", "/")
        If FileName.Contains("\") Then FileName = FileName.Replace("\", "/")
        If FileName.Contains("//") Then FileName = FileName.Replace("//", "/")
        If FileName.EndsWith("/") Then FileName = Mid(FileName, 1, FileName.Length - 1)

        Return FileName
    End Function

    Public Function doResolveWildNames(ByVal TextToAmend As String, Optional ByRef WarnOnError As Boolean = False) As String

        If TextToAmend.Contains("%") Then

            Dim chrNewPathName() As Char = TextToAmend.ToCharArray
            Dim intStart As Integer = 0
            Dim intEnd As Integer = 1

            numOfPercents = Count("%"c, TextToAmend)

            If numOfPercents > 0 Then
                If (numOfPercents Mod 2) = 0 Then
                    For i = 1 To (numOfPercents / 2)
                        intStart = intEnd - 1
                        intEnd = 0
                        Dim WildWord As String = String.Empty
                        If chrNewPathName(intStart) <> "%" Then
                            Do While chrNewPathName(intStart) <> "%" 'Finding the first index of "%"
                                intStart += 1
                            Loop
                        End If
                        intStart += 2

                        intEnd = 0
                        If chrNewPathName(intStart - 1) <> "%" Then
                            Do While chrNewPathName(intStart + intEnd - 1) <> "%"
                                intEnd += 1
                            Loop
                        End If

                        WildWord = Mid(TextToAmend, intStart, intEnd)
                        Select Case WildWord.ToUpper
                            'Paths
                            Case "DESKTOP"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", My.Computer.FileSystem.SpecialDirectories.Desktop)
                            Case "PROGDOCUMENTS", "DOCUMENTSPROG"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strDocumentsProgDir)
                            Case "APPDATAPROG", "PROGAPPDATA"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strAppDataProgDir)
                            Case "APPDATASETTINGS", "SETTINGSAPPDATA"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strAppDataSettingsFile)
                            Case "ROOT"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strRoot)
                            Case "STARTUP", "STARTUPEXE"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strStartupExe)
                            Case "SETTINGSPATH", "SETTINGSDIR"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strSettingsPath)
                            Case "EXTRAS", "EXTRASPATH", "EXTRASDIR"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strExtras)
                            Case "LANGUAGEPATH", "LANGUAGEDIR"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strLanguageFolders)
                            Case "SKINPATH", "SKINDIR"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strSkin)
                            Case "DATABASEPATH", "DATABASE", "DATABASEDIR", "DBPATH", "DBDIR"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strDatabaseDir)
                            Case "BACKUPPATH", "BACKUP", "BACKUPDIR"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strBackupDir)
                            Case "DEFDATABASENAME", "DEFDBNAME", "DEFAULTDATABASENAME", "DEFAULTDBNAME"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strDefDBName)
                            Case "DEFDATABASEFILE", "DEFDBFILE", "DEFAULTDATABASEFILE", "DEFAULTDBFILE"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strDefDBFile)
                            Case "CHANGELOG", "CHANGELOGFILE"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strChangeLog)
                            Case "EULA", "EULAFILE"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strEULA)
                            Case "PRESENTATIONPATH", "PRESENTATIONDIR"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strPresentation)
                            Case "MODLANGFILE", "MODLANGUAGEFILE", "MODULELANGFILE", "MODULELANGUAGEFILE", "MODLANG", "MODLANGUAGE", "MODULELANG", "MODULELANGUAGE"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strModuleLanguageFile)
                            Case "UNIVERSALFILE", "UNIVERSALLANGUAGEFILE", "UNILANGFILE"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strUniversal)
                            Case "CREDITS"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strCredits)
                            Case "SETTINGS", "SETTINGSFILE", "SETTINGSINI"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strSettingsIni)
                            Case "SETTINGSORIG", "SETTINGSORIGFILE", "SETTINGSORIGINI"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strSettingsOrig)
                            Case "EXPLORER", "WINEXPLORER", "EXPLORERFILE", "WINEXPLORERFILE"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strExplorerExe)
                            Case "IEXPLORER", "INTERNETEXPLORER", "IEXPLORERFILE", "INTERNETEXPLORERFILE"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strIExplore)
                            Case "UNRAR", "UNRARFILE"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strUnrar)
                                'Case "UNLOCKER", "UNLOCKERFILE"
                                'TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strUnlocker) ''''
                            Case "DOCUMENTS", "MYDOCUMENTS", "DOCUMENTSDIR", "DOCUMENTSPATH"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strMyDocuments)
                            Case "PICTURES", "PICDIC", "PICPATH", "PICTURESDIR", "PICTURESPATH"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strPictures)

                                'Registry
                            Case "AutoRunRegistryKeyPath".ToUpper
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strAutoRunRegistryKeyPath)
                            Case "AutoRunRegistrySubKeyPath".ToUpper
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strAutoRunRegistrySubKeyPath)
                            Case "ProgramRegistryKeyName".ToUpper
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strProgramRegistryKeyName)
                            Case "ProgramRegistrySubKey".ToUpper
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strProgramRegistrySubKey)


                                'Other
                            Case "WINDOWSARCHITECTURE", "WINARCHITECTURE", "WINARCH"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", ProgramArchitecture.ToString)
                            Case "USERNAME", "ACCOUNTNAME", "ACCNAME"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", UserName)
                            Case "LICENSEE", "LICENSENAME"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", Licensee)
                            Case "ISINTERNETAVAILABLE", "ISINTAVAILABLE", "INTERNETAVAILABLE", "INTAVAILABLE"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", isInternetAvailable.ToString)
                            Case "LANGUAGE", "CURRENTLANGUAGE"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", CurrentLanguage)
                            Case "DECSEPARATOR", "DECIMALSEPARATOR"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strDecimalSeparator)
                            Case "LANGUAGEDELIMITER", "LANGDELIMITER", "LANGDEL", "LANGUAGEDEL"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", LangDelimiterForRangeOrSpan)
                            Case "CYPHERNUM", "CYPHERENCRYPTIONNUM", "ENCRYPTIONNUM"
                                TextToAmend = TextToAmend.Replace("%" & WildWord & "%", CypherEncryptionNum.ToString)

                            Case Else
                                Dim strTryToGetVariable As String = Environment.GetEnvironmentVariable("%" & WildWord & "%")
                                If strTryToGetVariable <> String.Empty Then TextToAmend = TextToAmend.Replace("%" & WildWord & "%", strTryToGetVariable & "\")
                        End Select

                    Next i

                Else
                    WarnOnError = True
                End If

            End If
        End If

        numOfPercents = 0

        Return TextToAmend
    End Function

    Public Function doUnresolveWildNames(ByVal StringToCode As String) As String
        Dim StartI As Integer = 0
        Dim Endi As Integer = 0
        Dim ReturnString As String = StringToCode
        Dim hasChanged As Boolean = True
        Try
            'Paths
            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(My.Computer.FileSystem.SpecialDirectories.Desktop & "\") Then
                    StartI = ReturnString.ToLower.IndexOf(My.Computer.FileSystem.SpecialDirectories.Desktop.ToLower & "\")
                    Endi = StartI + (My.Computer.FileSystem.SpecialDirectories.Desktop & "\").Length
                    ReturnString = Left(ReturnString, StartI) & "%DESKTOP%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            'Paths
            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strMyDocuments.ToLower & "\") Then
                    StartI = ReturnString.ToLower.IndexOf(strMyDocuments.ToLower & "\")
                    Endi = StartI + (strMyDocuments & "\").Length
                    ReturnString = Left(ReturnString, StartI) & "%MYDOCUMENTS%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strDocumentsProgDir.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strDocumentsProgDir.ToLower)
                    Endi = StartI + strDocumentsProgDir.Length
                    ReturnString = Left(ReturnString, StartI) & "%ProgDocuments%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strWindowsDir.ToLower & "\") Then
                    StartI = ReturnString.ToLower.IndexOf(strWindowsDir.ToLower & "\")
                    Endi = StartI + (strWindowsDir & "\").Length
                    ReturnString = Left(ReturnString, StartI) & "%WINDIR%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strAppDataProgDir.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strAppDataProgDir.ToLower)
                    Endi = StartI + strAppDataProgDir.Length
                    ReturnString = Left(ReturnString, StartI) & "%AppDataProg%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strAppDataSettingsFile.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strAppDataSettingsFile.ToLower)
                    Endi = StartI + strAppDataSettingsFile.Length
                    ReturnString = Left(ReturnString, StartI) & "%AppDataSettings%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strStartupExe.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strStartupExe.ToLower)
                    Endi = StartI + strStartupExe.Length
                    ReturnString = Left(ReturnString, StartI) & "%AppDataSettings%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strLanguageFolders.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strLanguageFolders.ToLower)
                    Endi = StartI + strLanguageFolders.Length
                    ReturnString = Left(ReturnString, StartI) & "%LanguagePath%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strSkin.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strSkin.ToLower)
                    Endi = StartI + strSkin.Length
                    ReturnString = Left(ReturnString, StartI) & "%SkinPath%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strDatabaseDir.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strDatabaseDir.ToLower)
                    Endi = StartI + strDatabaseDir.Length
                    ReturnString = Left(ReturnString, StartI) & "%DatabasePath%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strBackupDir.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strBackupDir.ToLower)
                    Endi = StartI + strBackupDir.Length
                    ReturnString = Left(ReturnString, StartI) & "%BackupPath%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strChangeLog.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strChangeLog.ToLower)
                    Endi = StartI + strChangeLog.Length
                    ReturnString = Left(ReturnString, StartI) & "%Changelog%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strEULA.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strEULA.ToLower)
                    Endi = StartI + strEULA.Length
                    ReturnString = Left(ReturnString, StartI) & "%EULA%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strPresentation.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strPresentation.ToLower)
                    Endi = StartI + strPresentation.Length
                    ReturnString = Left(ReturnString, StartI) & "%PresentationPath%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strModuleLanguageFile.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strModuleLanguageFile.ToLower)
                    Endi = StartI + strModuleLanguageFile.Length
                    ReturnString = Left(ReturnString, StartI) & "%ModLangPath%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strUniversal.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strUniversal.ToLower)
                    Endi = StartI + strUniversal.Length
                    ReturnString = Left(ReturnString, StartI) & "%UniversalFile%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strCredits.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strCredits.ToLower)
                    Endi = StartI + strCredits.Length
                    ReturnString = Left(ReturnString, StartI) & "%Credits%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strSettingsIni.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strSettingsIni.ToLower)
                    Endi = StartI + strSettingsIni.Length
                    ReturnString = Left(ReturnString, StartI) & "%Settings%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strSettingsOrig.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strSettingsOrig.ToLower)
                    Endi = StartI + strSettingsOrig.Length
                    ReturnString = Left(ReturnString, StartI) & "%SettingsOrig%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strExplorerExe.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strExplorerExe.ToLower)
                    Endi = StartI + strExplorerExe.Length
                    ReturnString = Left(ReturnString, StartI) & "%ExplorerFile%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strIExplore.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strIExplore.ToLower)
                    Endi = StartI + strIExplore.Length
                    ReturnString = Left(ReturnString, StartI) & "%InternetExplorer%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strUnrar.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strUnrar.ToLower)
                    Endi = StartI + strUnrar.Length
                    ReturnString = Left(ReturnString, StartI) & "%Unrar%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            'Do While hasChanged = True ''''
            '    hasChanged = False
            '    If ReturnString.ToLower.Contains(strUnlocker.ToLower) Then
            '        StartI = ReturnString.ToLower.IndexOf(strUnlocker.ToLower)
            '        Endi = StartI + strUnlocker.Length
            '        ReturnString = Left(ReturnString, StartI) & "%Unlocker%" & Right(ReturnString, ReturnString.Length - Endi)
            '        hasChanged = True
            '    End If
            'Loop
            'hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strPictures.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strPictures.ToLower)
                    Endi = StartI + strPictures.Length
                    ReturnString = Left(ReturnString, StartI) & "%PicturesPath%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True



            'Windows Variables
            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(Environment.GetEnvironmentVariable("programdata").ToLower & "\") Then
                    StartI = ReturnString.ToLower.IndexOf(Environment.GetEnvironmentVariable("programdata").ToLower & "\")
                    Endi = StartI + (Environment.GetEnvironmentVariable("programdata") & "\").Length
                    ReturnString = Left(ReturnString, StartI) & "%ProgramData%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(Environment.GetEnvironmentVariable("appdata").ToLower & "\") Then
                    StartI = ReturnString.ToLower.IndexOf(Environment.GetEnvironmentVariable("appdata").ToLower & "\")
                    Endi = StartI + (Environment.GetEnvironmentVariable("appdata") & "\").Length
                    ReturnString = Left(ReturnString, StartI) & "%AppData%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(Environment.GetEnvironmentVariable("programfiles").ToLower & "\") Then
                    StartI = ReturnString.ToLower.IndexOf(Environment.GetEnvironmentVariable("programfiles").ToLower & "\")
                    Endi = StartI + (Environment.GetEnvironmentVariable("programfiles") & "\").Length
                    ReturnString = Left(ReturnString, StartI) & "%ProgramFiles%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True



            'These must always be the last
            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strExtras.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strExtras.ToLower)
                    Endi = StartI + strExtras.Length
                    ReturnString = Left(ReturnString, StartI) & "%Extras%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strSettingsPath.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strSettingsPath.ToLower)
                    Endi = StartI + strSettingsPath.Length
                    ReturnString = Left(ReturnString, StartI) & "%SettingsPath%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(My.Application.Info.DirectoryPath.ToLower & "\") Then
                    StartI = ReturnString.ToLower.IndexOf(My.Application.Info.DirectoryPath.ToLower & "\")
                    Endi = StartI + (My.Application.Info.DirectoryPath & "\").Length
                    ReturnString = Left(ReturnString, StartI) & "%ROOT%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True



            'Other
            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strProgramRegistryKeyName.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strProgramRegistryKeyName.ToLower)
                    Endi = StartI + (strProgramRegistryKeyName).Length
                    ReturnString = Left(ReturnString, StartI) & "%ProgramRegistryKeyName%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strProgramRegistrySubKey.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strProgramRegistrySubKey.ToLower)
                    Endi = StartI + (strProgramRegistrySubKey).Length
                    ReturnString = Left(ReturnString, StartI) & "%ProgramRegistrySubKey%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strAutoRunRegistryKeyPath.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strAutoRunRegistryKeyPath.ToLower)
                    Endi = StartI + (strAutoRunRegistryKeyPath).Length
                    ReturnString = Left(ReturnString, StartI) & "%AutoRunRegistryKeyPath%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True

            Do While hasChanged = True
                hasChanged = False
                If ReturnString.ToLower.Contains(strAutoRunRegistrySubKeyPath.ToLower) Then
                    StartI = ReturnString.ToLower.IndexOf(strAutoRunRegistrySubKeyPath.ToLower)
                    Endi = StartI + (strAutoRunRegistrySubKeyPath).Length
                    ReturnString = Left(ReturnString, StartI) & "%AutoRunRegistrySubKeyPath%" & Right(ReturnString, ReturnString.Length - Endi)
                    hasChanged = True
                End If
            Loop
            hasChanged = True


        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try

        Return ReturnString
    End Function

    Public Sub RunOpenDir(ByRef PathName As String, Optional ByVal Arguments As String = "", Optional ByVal WaitForExit As Boolean = False, Optional ByVal ErrorMessage As String = "", Optional ByVal CannotFindFile As String = "Cannot find the specified file: ", Optional ByVal WithArguments As String = " with arguments: ", Optional ByVal WriteCrushFileAnyway As Boolean = False)
        PathName = doResolveWildNames(PathName)
        Arguments = doResolveWildNames(Arguments)

        If PathName.ToLower = "explorer" Or PathName.ToLower = "explorer.exe" Then
            PathName = strExplorerExe
        End If

        If File.Exists(PathName) Then                   'If it is a FILE
            exeProcesses.StartInfo.FileName = PathName
            exeProcesses.StartInfo.Arguments = Arguments
            exeProcesses.Start()
            If WaitForExit = True Then exeProcesses.WaitForExit()

        ElseIf Directory.Exists(doProperPathName(PathName)) Then    'If it is a DIRECTORY / FOLDER
            exeProcesses.StartInfo.FileName = strExplorerExe
            exeProcesses.StartInfo.Arguments = PathName
            exeProcesses.Start()

        Else
            If ErrorMessage <> "" Or WriteCrushFileAnyway = True Then
                If ErrorMessage <> "" Then
                    MsgBox(ErrorMessage)
                End If

                WriteText(My.Computer.FileSystem.SpecialDirectories.Desktop & "\Crush " & Today.Day & "-" & Today.Month & "-" & My.Computer.Clock.LocalTime.Year & " " & My.Computer.Clock.LocalTime.Minute & " " & My.Computer.Clock.LocalTime.Second & ".txt", My.Application.Info.Title & vbCrLf & CannotFindFile & """" & PathName & """" & WithArguments & """" & Arguments & """", Encoding.Unicode)
            End If
        End If
    End Sub

    Public Sub CloseForm(ByVal frm As Form, Optional ByVal ClosingPromptText As String = "", Optional ByVal RestartApp As Boolean = False,
                         Optional ByVal frm2 As Form = Nothing, Optional ByVal frm3 As Form = Nothing, Optional ByVal frm4 As Form = Nothing, Optional ByVal frm5 As Form = Nothing)
        Try
            If ClosingPromptText = "" Then
                If RestartApp = True Then RestartApplication()
                frm.Close()

            ElseIf MsgBox(ClosingPromptText, MsgBoxStyle.YesNo) = vbYes Then

                If frm2 IsNot Nothing Then
                    frm2.Close()
                End If

                If frm3 IsNot Nothing Then
                    frm3.Close()
                End If

                If frm4 IsNot Nothing Then
                    frm4.Close()
                End If

                If frm5 IsNot Nothing Then
                    frm5.Close()
                End If

                If RestartApp = True Then
                    RestartApplication()
                End If

                frm.Close()
            End If

        Catch ex As Exception
            CreateCrashFile(ex)
        End Try
    End Sub

    Public Function isFirstTimeRun() As Boolean
        Dim Result As Boolean
        Try

            If Directory.Exists(strAppDataProgDir) Then
                If File.Exists(strAppDataSettingsFile) Then
                    Dim AppDataSettings() As String = {}
                    ReadFile(strAppDataSettingsFile, AppDataSettings)
                    If CBool(AppDataSettings(0).Substring("FirstTime=".Length)) = True Then
                        Result = True

                    Else
                        Result = False
                    End If

                Else
                    Result = True
                End If

            Else
                Directory.CreateDirectory(strAppDataProgDir)
                Result = True
            End If

        Catch ex As Exception
            Result = True
        End Try

        Return Result
    End Function

    Public Function RemCtrHotLetter(ByVal btn As Control) As String
        Dim ResultString As String

        ResultString = btn.Text.Replace("&", "")

        Return ResultString
    End Function

    Public Function RemMniHotLetter(ByVal mni As ToolStripMenuItem) As String
        Dim ResultString As String

        ResultString = mni.Text.Replace("&", "")

        Return ResultString
    End Function

    Public Sub CreateCrashFile(ByVal CrashException As Exception, Optional ByVal ShowCrashMessage As Boolean = False, Optional ByVal TitleText As String = "", Optional ByVal MsgStyle As MsgBoxStyle = MsgBoxStyle.Exclamation, Optional ByVal ExtraTextInTheBeginningWithoutVbCrLf As String = "", Optional ByVal ExtraTextInTheBottomForLogOnly As String = "")
        Dim strDateTime As String = String.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}", My.Computer.Clock.GmtTime.Year, My.Computer.Clock.GmtTime.Month,
            My.Computer.Clock.GmtTime.Day, My.Computer.Clock.GmtTime.Hour, My.Computer.Clock.GmtTime.Minute)
        Dim strWinInfo As String

        With My.Computer.Info
            strWinInfo =
                "Assembly Name: " & My.Application.Info.AssemblyName & vbCrLf &
                "User Name: " & My.User.Name & vbCrLf &
                "Version: " & My.Application.Info.Version.ToString & vbCrLf &
                "Directory Path: " & My.Application.Info.DirectoryPath & vbCrLf &
                "Available Physical Memory: " & .AvailablePhysicalMemory & vbCrLf &
                "Total Physical Memory: " & .TotalPhysicalMemory & vbCrLf &
                "Available Virtual Memory: " & .AvailableVirtualMemory & vbCrLf &
                "Total Virtual Memory: " & .TotalVirtualMemory & vbCrLf &
                "Installed UI Culture: " & .InstalledUICulture.ToString & vbCrLf &
                "OS Full Name: " & .OSFullName & vbCrLf &
                "OS Platform: " & .OSPlatform & vbCrLf &
                "OS Version: " & .OSVersion & vbCrLf &
                "Computer Name: " & My.Computer.Name & vbCrLf &
                "IsNetworkAvailable: " & My.Computer.Network.IsAvailable & vbCrLf & vbCrLf &
                "Username: " & UserName & vbCrLf

        End With

        Dim OpenForms As New List(Of String)
        For Each OpenForm In My.Application.OpenForms
            OpenForms.Add(DirectCast(OpenForm, Form).Name)
        Next

#If DEBUG Then
        MsgBox(CrashException.ToString)
#End If

        Dim CrashFilePath() As String = {strExtras & "Crash ex" & strDateTime & ".txt"}
        WriteText(CrashFilePath(0), strWinInfo & vbCrLf & "Open Forms:" & vbCrLf & ArrayBox(OpenForms) & vbCrLf & vbCrLf & ExtraTextInTheBeginningWithoutVbCrLf & vbCrLf & vbCrLf & CrashException.ToString & vbCrLf & vbCrLf & ExtraTextInTheBottomForLogOnly, System.Text.Encoding.Unicode)

        If ShowCrashMessage Then
            If TitleText = "" Then
                MsgBox(ExtraTextInTheBeginningWithoutVbCrLf & strModLanguage(42) & CrashException.Message & vbCrLf & vbCrLf & strModLanguage(43) & CrashFilePath(0), MsgStyle) 'An Error has occurred: <Error> | An error report file has been stored on:
            Else
                MsgBox(ExtraTextInTheBeginningWithoutVbCrLf & strModLanguage(42) & CrashException.Message & vbCrLf & vbCrLf & strModLanguage(43) & CrashFilePath(0), MsgStyle, TitleText)  'An Error has occurred: <Error> | An error report file has been stored on:
            End If
        End If

        Dim CrashReportForm As New frmCrashLog
        CrashReportForm.CrashFilePath = CrashFilePath
        ShowDialogForm(CrashReportForm)

    End Sub

#Region "Show Form"
    Public Sub ShowDialogForm(ByVal frm As Form, Optional ByVal Me_ParentForm As Form = Nothing, Optional ByVal X As Integer = -1, Optional ByVal Y As Integer = -1)
        Call ShowForm(frm, Me_ParentForm, True, X, Y)
    End Sub
    Public Sub ShowForm(ByVal frm As Form, Optional ByVal Me_ParentForm As Form = Nothing, Optional ByVal isDialog As Boolean = False, Optional ByVal X As Integer = -1, Optional ByVal Y As Integer = -1)
        If Me_ParentForm Is Nothing Then Me_ParentForm = frmMain

        If Me_ParentForm.IsMdiContainer Then
            frm.MdiParent = Me_ParentForm
            frmMain.pnlMain.Visible = False
            If X <> -1 Then frm.Left = X
            If Y <> -1 Then frm.Top = Y
        End If

        If isDialog AndAlso frm.TopLevel Then
            frm.TopMost = True
            If X <> -1 Then frm.Left = X
            If Y <> -1 Then frm.Top = Y
            frm.ShowDialog()

        Else
            frm.Show()
            frm.Focus()
            frm.BringToFront()
            If X <> -1 Then frm.Left = X
            If Y <> -1 Then frm.Top = Y
        End If

    End Sub
#End Region

    Public Function MStr(ByVal Message As String) As String
        Return Message.Replace("{VbCrLf}", vbCrLf).Replace("{VbTab}", vbTab).Replace("{vbcrlf}", vbCrLf).Replace("{vbtab}", vbTab)
    End Function

    Public Function TimeToTimeSpan(ByVal strTime As String, Optional ByVal ContainsHours As Boolean = False) As TimeSpan
        Dim tmp() As String = strTime.Split(":"c)
        Dim TimeSpanHelper As List(Of Double) = (From item In tmp Select CDbl(item)).ToList
        Dim TimeSpanResult As New TimeSpan

        If TimeSpanHelper.Count > 4 OrElse ContainsHours Then
            TimeSpanResult += TimeSpan.FromDays(TimeSpanHelper(0))
            TimeSpanHelper.RemoveAt(0)
        End If

        For i As Integer = 0 To TimeSpanHelper.Count - 1
            Select Case i
                Case 0
                    TimeSpanResult += TimeSpan.FromHours(TimeSpanHelper(i))
                Case 1
                    TimeSpanResult += TimeSpan.FromMinutes(TimeSpanHelper(i))
                Case 2
                    TimeSpanResult += TimeSpan.FromSeconds(TimeSpanHelper(i))
                Case 3
                    TimeSpanResult += TimeSpan.FromMilliseconds(CDbl(TimeSpanHelper(i).ToString.Substring(0, 3)))
            End Select
        Next
        Return TimeSpanResult
    End Function

    Public Function CopyTextToClipboard(ByVal text As String) As Boolean
        Try
            Dim a As New Thread(Sub()
                                    Try
                                        Clipboard.SetText(text)
                                    Catch ex As Exception
                                    End Try
                                End Sub)
            a.SetApartmentState(ApartmentState.STA)
            a.Start()
            a.Join() 'Wait for the thread to end
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


End Module
