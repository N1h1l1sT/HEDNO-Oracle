Option Strict On
'Version 2.0 2013-04-22
'Updated frmLicenseViewer
'Added frmLicenseViewer
'Added TrayWebsite; frmDataGridView

Module modChangeLanguage
    Public Tip_LangString As String = "Tip:"

    '==============================
    '==STANDARD LANGUAGE FUNCTION==
    '==============================

    Public Sub Main_Language(ByVal frm As frmMain)
        With frm
            Dim strLanguage_Main() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_Main)
            Dim strLanguage_Main_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_Main_Tips)
            On Error Resume Next

            .tltMain.ToolTipTitle = Tip_LangString

            .mniFileMenu.Text = strLanguage_Main(1)
            .mniSettings.Text = strLanguage_Main(2)
            .traySettings.Text = strLanguage_Main(2)
            .cmdSettings.Text = strLanguage_Main(2)
            .mniExit.Text = strLanguage_Main(3)
            .trayClose.Text = strLanguage_Main(3)
            .mniHelpMenu.Text = strLanguage_Main(4)
            .mniCredits.Text = strLanguage_Main(5)
            .trayCredits.Text = strLanguage_Main(5)
            .mniAbout.Text = strLanguage_Main(6)
            .trayAbout.Text = strLanguage_Main(6)
            .mniProgramsDir.Text = strLanguage_Main(7)
            .btnExit.Text = strLanguage_Main(8)
            .mniEULA.Text = strLanguage_Main(10)
            .mniDirectoriesMenu.Text = strLanguage_Main(12)
            .mniDatabaseDir.Text = strLanguage_Main(13)
            .mniLinksMenu.Text = strLanguage_Main(14)
            .mniProgramWebsite.Text = strLanguage_Main(15)
            .mniProgrammerWebsite.Text = strLanguage_Main(21)
            .mniShowPresentation.Text = strLanguage_Main(22)
            .mniShowWelcomeScreen.Text = strLanguage_Main(23)
            .mniDatabaseMaintenance.Text = strLanguage_Main(24)
            .mniHelp.Text = strLanguage_Main(25)
            .cmdHelp.Text = strLanguage_Main(25)
            .CommandsDefaultText = strLanguage_Main(26)
            .txtCommands.Text = strLanguage_Main(26)
            .cmdGo.Text = strLanguage_Main(28)
            .mniChangeLog.Text = strLanguage_Main(29)
            .gbCommands.Text = strLanguage_Main(30)
            .lblUnknownCmd.Text = strLanguage_Main(31)
            .mniSuggestOrComplain.Text = strLanguage_Main(32)
            .mniOpenSettingsFile.Text = strLanguage_Main(33)
            .mniExtrasDir.Text = strLanguage_Main(36)
            .mniLanguageDir.Text = strLanguage_Main(37)
            .mniSettingsDir.Text = strLanguage_Main(38)
            .mniSkinDir.Text = strLanguage_Main(39)
            .mniCompanySite.Text = strLanguage_Main(43)
            .mniProgramDocuments.Text = strLanguage_Main(44)
            .mniCheckForUpdates.Text = strLanguage_Main(49)
            .trayShow.Text = strLanguage_Main(74)
            .trayHide.Text = strLanguage_Main(75)
            .gbFunctions.Text = strLanguage_Main(77)
            .trayWebsite.Text = strLanguage_Main(15)
            .mniLicenses.Text = strLanguage_Main(79)

            Call UpdateTexts(frm)
            With My.Settings
                If .ProgrammeWebsite <> "" OrElse .ProgrammersWebsite <> "" OrElse .CompanyWebsite <> "" Then
                    frm.mniLinksMenu.Visible = True
                    If .ProgrammeWebsite <> "" Then frm.mniProgramWebsite.Visible = True
                    If .ProgrammersWebsite <> "" Then frm.mniProgrammerWebsite.Visible = True
                    If .CompanyWebsite <> "" Then frm.mniCompanySite.Visible = True
                End If
            End With

            'TIPS
            .mniSettings.ToolTipText = strLanguage_Main_Tips(1)
            .tltMain.SetToolTip(.cmdSettings, strLanguage_Main_Tips(1))
            .mniExit.ToolTipText = strLanguage_Main_Tips(2)
            .tltMain.SetToolTip(.btnExit, strLanguage_Main_Tips(2))
            .mniDatabaseMaintenance.ToolTipText = strLanguage_Main_Tips(3)
            .mniSuggestOrComplain.ToolTipText = strLanguage_Main_Tips(4)
            .mniOpenSettingsFile.ToolTipText = strLanguage_Main_Tips(5)
            .mniProgramDocuments.ToolTipText = strLanguage_Main_Tips(6)
            .mniProgramsDir.ToolTipText = strLanguage_Main_Tips(7)
            .mniDatabaseDir.ToolTipText = strLanguage_Main_Tips(8)
            .mniExtrasDir.ToolTipText = strLanguage_Main_Tips(9)
            .mniLanguageDir.ToolTipText = strLanguage_Main_Tips(10)
            .mniSettingsDir.ToolTipText = strLanguage_Main_Tips(11)
            .mniSkinDir.ToolTipText = strLanguage_Main_Tips(12)
            .mniProgramWebsite.ToolTipText = strLanguage_Main_Tips(13)
            .mniProgrammerWebsite.ToolTipText = strLanguage_Main_Tips(14)
            .mniCompanySite.ToolTipText = strLanguage_Main_Tips(15)
            .mniShowPresentation.ToolTipText = strLanguage_Main_Tips(16)
            .mniShowWelcomeScreen.ToolTipText = strLanguage_Main_Tips(17)
            .mniEULA.ToolTipText = strLanguage_Main_Tips(18)
            .mniChangeLog.ToolTipText = strLanguage_Main_Tips(19)
            .mniHelp.ToolTipText = strLanguage_Main_Tips(20)
            .tltMain.SetToolTip(.cmdHelp, strLanguage_Main_Tips(20))
            .mniCheckForUpdates.ToolTipText = strLanguage_Main_Tips(21)
            .mniCredits.ToolTipText = strLanguage_Main_Tips(22)
            .mniAbout.ToolTipText = strLanguage_Main_Tips(23)
            .tltMain.SetToolTip(.cmdGo, strLanguage_Main_Tips(29))
            '/TIP

            '=====================================
            '==END OF STANDARD LANGUAGE FUNCTION==
            '=====================================

        End With
    End Sub

    '==============================
    '==STANDARD LANGUAGE FUNCTION==
    '==============================

    Public Sub Settings_Language(ByVal frm As frmSettings)
        With frm
            Dim strLanguage_Settings() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_Settings)
            On Error Resume Next

            'Standard
            .Text = strLanguage_Settings(10)
            .cmdExit.Text = strLanguage_Settings(12)
            .cmdApply.Text = strLanguage_Settings(13)
            .gbCommands.Text = strLanguage_Settings(16)
            .cmdCurrent.Text = strLanguage_Settings(17)
            .cmdDefault.Text = strLanguage_Settings(18)
            .tpGeneral.Text = strLanguage_Settings(19)
            .lblLanguage.Text = strLanguage_Settings(20)
            .lblSkin.Text = strLanguage_Settings(23)
            .lblCheckForNewVersion.Text = strLanguage_Settings(24)
            .lblDBpass.Text = strLanguage_Settings(25)
            .lblDBtype.Text = strLanguage_Settings(26)
            .lblStartWithWin.Text = strLanguage_Settings(27)
            .lblDelayTime.Text = strLanguage_Settings(29)
            .lblSecs.Text = strLanguage_Settings(30)
            .lblSplitDbEveryMonth.Text = strLanguage_Settings(31)
            .lblDBFile.Text = strLanguage_Settings(32)
            .lblShowStartupForm.Text = strLanguage_Settings(33)
            .tpDatabase.Text = strLanguage_Settings(34)
            .lblDatabaseTables.Text = strLanguage_Settings(35)
            .lblWindowState.Text = strLanguage_Settings(39)
            .lblFullScreenResolutions.Text = strLanguage_Settings(45)
            .lblRemWindowState.Text = strLanguage_Settings(46)
            .lblWindowResolution.Text = strLanguage_Settings(52)
            .lblProtectedTables.Text = strLanguage_Settings(53)

            For i = 0 To frm.tcSettings.TabPages.Count - 1
                For Each ctrl As Control In frm.tcSettings.TabPages(i).Controls
                    If TypeOf ctrl Is TextBox Then
                        DirectCast(ctrl, TextBox).Text = strLanguage_Settings(3) 'Unknown

                    ElseIf TypeOf ctrl Is ComboBox Then
                        DirectCast(ctrl, ComboBox).Text = strLanguage_Settings(3) 'Unknown

                    End If
                Next
            Next


            '=====================================
            '==END OF STANDARD LANGUAGE FUNCTION==
            '=====================================


            'Current Program's Language

            '/Current Program's Language

            '==============================
            '==STANDARD LANGUAGE FUNCTION==
            '==============================

        End With
    End Sub

#Region "Standard Language Function"
    Public Sub About_Language(ByVal frm As frmAbout)    'Version 1.3.1
        With frm
            Dim strLanguage_About() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name.ToString & ".lng", .strLanguage_About)
            ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name.ToString & "_Textbox.lng", .txtCredits)    'This is an actual visible textbox
            On Error Resume Next

            .lblAssemblyName.Text = strLanguage_About(1)
            .lblCompanyName.Text = strLanguage_About(2)
            .lblCopyright.Text = strLanguage_About(3)
            .lblDirectoryPath.Text = strLanguage_About(4)
            .lblProductName.Text = strLanguage_About(5)
            .lblTitle.Text = strLanguage_About(6)
            .lblVersion.Text = strLanguage_About(7)
            .lblWorkingSet.Text = strLanguage_About(8)
            .lblHash.Text = strLanguage_About(9)
            .lblUser.Text = strLanguage_About(10)
            .Text = strLanguage_About(12)
            .txtCredits.Text = strLanguage_About(15) & RemMniHotLetter(frmMain.mniHelpMenu) & strLanguage_About(17) & RemMniHotLetter(frmMain.mniCredits) & strLanguage_About(16) & vbCrLf & .txtCredits.Text
            .cmdExit.Text = strLanguage_About(20)
            .lblLicense.Text = strLanguage_About(21)
            .lblTrademark.Text = strLanguage_About(22)

        End With

    End Sub

    Public Sub Presentation_Language(ByVal frm As frmPresentation)
        With frm
            Dim strLanguage_Presentation() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_Presentation)
            On Error Resume Next

            .btnNext.Text = strLanguage_Presentation(1)
            .btnExit.Text = strLanguage_Presentation(2)
            .btnPrevious.Text = strLanguage_Presentation(6)
            .Text = .MeText
            strPresentation = strLanguageFolders & CurrentLanguage & "\Presentation\"

        End With
    End Sub

    Public Sub FirstTime_Language(ByVal frm As frmFirstTime)
        With frm
            Dim strLanguage_FirstTime() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_FirstTime)
            On Error Resume Next

            .Text = strLanguage_FirstTime(1)
            .btnNext.Text = strLanguage_FirstTime(2)

            Uri.TryCreate(strLanguageFolders & CurrentLanguage & "\" & .Name & "_txtInfo.html", UriKind.RelativeOrAbsolute, .wbWelcome.Url)

        End With
    End Sub

    Public Sub Typebox_Language(ByVal frm As dlgTypeBox)
        With frm
            Dim strLanguage_Typebox() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_Typebox)
            On Error Resume Next

            .OK_Button.Text = strLanguage_Typebox(4)
            .Cancel_Button.Text = strLanguage_Typebox(5)

        End With
    End Sub

    Public Sub SkinCreator_Language(ByVal frm As frmSkinCreator)
        With frm
            Dim strLanguage_SkinCreator() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_SkinCreator)
            On Error Resume Next

            .Text = strLanguage_SkinCreator(1)
            .gbSkins.Text = strLanguage_SkinCreator(2)
            .gbItems.Text = strLanguage_SkinCreator(3)
            .lblSelForm.Text = strLanguage_SkinCreator(4)
            .lblSelControl.Text = strLanguage_SkinCreator(5)
            .gbActions.Text = strLanguage_SkinCreator(6)
            .lblSplashScreenImage.Text = strLanguage_SkinCreator(7)
            .lblImage.Text = strLanguage_SkinCreator(8)
            .lblBigImage.Text = strLanguage_SkinCreator(9)
            .lblForeColour.Text = strLanguage_SkinCreator(10)
            .lblBackColour.Text = strLanguage_SkinCreator(11)
            .gbCommands.Text = strLanguage_SkinCreator(12)
            .btnNewSkin.Text = strLanguage_SkinCreator(13)
            .btnRename.Text = strLanguage_SkinCreator(14)
            .btnDelSkin.Text = strLanguage_SkinCreator(15)
            .btnApply.Text = strLanguage_SkinCreator(16)
            .btnClose.Text = strLanguage_SkinCreator(17)
            .lblCtrlImage.Text = strLanguage_SkinCreator(26)

        End With
    End Sub

    Public Sub Help_Language(ByVal frm As frmHelp)
        With frm
            Dim strLanguage_frmHelp() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_frmHelp)
            On Error Resume Next

            .Text = strLanguage_frmHelp(1)
            .btnClose.Text = strLanguage_frmHelp(2)
            .lblCommands.Text = strLanguage_frmHelp(3)
            .lblUsage.Text = strLanguage_frmHelp(4)


        End With
    End Sub

    Public Sub DatabaseMaintenance_Language(ByVal frm As frmDatabaseMaintenance)
        With frm
            Dim strLanguage_DatabaseMaintenance() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_DatabaseMaintenance)
            Dim strLanguage_DatabaseMaintenance_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_DatabaseMaintenance_Tips)
            On Error Resume Next

            .tltDatabaseMaintenance.ToolTipTitle = Tip_LangString

            .Text = strLanguage_DatabaseMaintenance(1)
            .btnExit.Text = strLanguage_DatabaseMaintenance(2)
            .gbMaintenanceOptions.Text = strLanguage_DatabaseMaintenance(3)
            .btnClearAll.Text = strLanguage_DatabaseMaintenance(4)
            .btnClearSingleTable.Text = strLanguage_DatabaseMaintenance(5)
            .btnCompactDB.Text = strLanguage_DatabaseMaintenance(6)


            'TIPS
            .tltDatabaseMaintenance.SetToolTip(.btnCompactDB, strLanguage_DatabaseMaintenance_Tips(1))
            .tltDatabaseMaintenance.SetToolTip(.btnClearAll, strLanguage_DatabaseMaintenance_Tips(2))
            .tltDatabaseMaintenance.SetToolTip(.btnClearSingleTable, strLanguage_DatabaseMaintenance_Tips(3))
            .tltDatabaseMaintenance.SetToolTip(.btnExit, strLanguage_DatabaseMaintenance_Tips(4))
            '/TIPS

        End With
    End Sub

    Public Sub SuggestionAndComplaint_Language(ByVal frm As frmSuggestionAndComplaint)
        With frm
            Dim strLanguage_SuggestionAndComplaint() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_SuggestionAndComplaint)
            On Error Resume Next

            .Text = strLanguage_SuggestionAndComplaint(1)
            .lblInfo.Text = strLanguage_SuggestionAndComplaint(2)
            .gbInformation.Text = strLanguage_SuggestionAndComplaint(3)
            .lblType.Text = strLanguage_SuggestionAndComplaint(4)

            .lstType.Clear()
            .lstType.Add(strLanguage_SuggestionAndComplaint(5)) 'Select One
            .lstType.Add(strLanguage_SuggestionAndComplaint(6)) 'Suggestion
            .lstType.Add(strLanguage_SuggestionAndComplaint(7)) 'Complaint
            .cbType.DataSource = .lstType

            .lblCategory.Text = strLanguage_SuggestionAndComplaint(8)

            .lstFirstCategory.Clear()
            .lstFirstCategory.Add(strLanguage_SuggestionAndComplaint(5))  'Select One
            .lstFirstCategory.Add(strLanguage_SuggestionAndComplaint(9))  'New Feature
            .lstFirstCategory.Add(strLanguage_SuggestionAndComplaint(10)) 'Impovement of a Feature
            .lstFirstCategory.Add(strLanguage_SuggestionAndComplaint(11)) 'Other

            .lstSecondCategory.Clear()
            .lstSecondCategory.Add(strLanguage_SuggestionAndComplaint(5))  'Select One
            .lstSecondCategory.Add(strLanguage_SuggestionAndComplaint(12)) 'Technical Issue
            .lstSecondCategory.Add(strLanguage_SuggestionAndComplaint(13)) 'Account/Registration/Key Issue
            .lstSecondCategory.Add(strLanguage_SuggestionAndComplaint(14)) 'Support Issue
            .lstSecondCategory.Add(strLanguage_SuggestionAndComplaint(15)) 'Crush Report
            .lstSecondCategory.Add(strLanguage_SuggestionAndComplaint(16)) 'Bug Report
            .lstSecondCategory.Add(strLanguage_SuggestionAndComplaint(11)) 'Other

            .lblName.Text = strLanguage_SuggestionAndComplaint(17)
            .lblEmail.Text = strLanguage_SuggestionAndComplaint(18)
            .gbMessage.Text = strLanguage_SuggestionAndComplaint(19)
            .btnCancel.Text = strLanguage_SuggestionAndComplaint(20)
            .btnNext.Text = strLanguage_SuggestionAndComplaint(21)

        End With
    End Sub

    Public Sub PleaseWait_Language(ByVal frm As frmPleaseWait)
        With frm
            Dim strLanguage_PleaseWait() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_PleaseWait)
            On Error Resume Next

            .lblPleaseWait.Text = strLanguage_PleaseWait(1)
        End With
    End Sub

    Public Sub DataGridView_Language(ByVal frm As frmDataGridView)
        With frm
            Dim strLanguage_DataGridView() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_DataGridView)
            On Error Resume Next

            .Text = strLanguage_DataGridView(1)
            .lblSum.Text = strLanguage_DataGridView(5) & "0"
            .lblSelectedCells.Text = strLanguage_DataGridView(6) & "0"
            .lblSelectedRows.Text = strLanguage_DataGridView(7) & "0"
            .lblSelectedColumns.Text = strLanguage_DataGridView(8) & "0"
            .lblRowsCount.Text = strLanguage_DataGridView(12) & "0"
            .lblColumnsCount.Text = strLanguage_DataGridView(13) & "0"
            .lblCurRow.Text = strLanguage_DataGridView(14) & "0"
            .lblCurColumn.Text = strLanguage_DataGridView(15) & "0"
        End With
    End Sub

    Public Sub Crash_Language(ByVal frm As frmCrashLog)
        With frm
            Dim strLanguage_Crash() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_Crush)
            On Error Resume Next

            .Text = strLanguage_Crash(1)
            .btnCancel.Text = strLanguage_Crash(2)
            .btnSendCrashLog.Text = strLanguage_Crash(3)
            .lblInfo.Text = strLanguage_Crash(4).Replace("{vbcrlf}", vbCrLf)
            .lblWhatCausedTheException.Text = strLanguage_Crash(5)
        End With
    End Sub

    Public Sub FindR_Language(ByVal frm As frmFindR)
        With frm
            Dim strLanguage_FindR() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_FindR)
            Dim strLanguage_FindR_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_FindR_Tips)
            On Error Resume Next

            .tltFindR.ToolTipTitle = Tip_LangString

            .Text = strLanguage_FindR(1)
            .lblInfo.Text = strLanguage_FindR(2).Replace("%ProgrammeName%", My.Application.Info.Title).Replace("%vbcrlf%", vbCrLf).Replace("%browse%", RemCtrHotLetter(frm.btnBrowse)).Replace("%RWebpage%", RemCtrHotLetter(frm.btnRWebsite))
            .btnRWebsite.Text = strLanguage_FindR(3)
            .btnBrowse.Text = strLanguage_FindR(4)
            .btnCancel.Text = strLanguage_FindR(5)

            'TIPS
            .tltFindR.SetToolTip(.btnRWebsite, strLanguage_FindR_Tips(1))
            .tltFindR.SetToolTip(.btnBrowse, strLanguage_FindR_Tips(2))
            .tltFindR.SetToolTip(.btnCancel, strLanguage_FindR_Tips(3))
            '/TIPS

        End With
    End Sub

    Public Sub LicenseViewer_Language(ByVal frm As frmLicenseViewer)
        With frm
            Dim strLanguage_LicenseViewer() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_LicenseViewer)
            On Error Resume Next

            .Text = strLanguage_LicenseViewer(1) 'Licenses
            .btnDisagree.Text = strLanguage_LicenseViewer(3) 'I &Disagree
            .btnAgree.Text = strLanguage_LicenseViewer(4) 'I &Agree
            .lblAlreadyAgreedToAll.Text = strLanguage_LicenseViewer(6) '*You've already agreed to all the licenses*
            .btnAgreeToAll.Text = strLanguage_LicenseViewer(7) '&I Agree to all

        End With
    End Sub

#End Region

    '=====================================
    '==END OF STANDARD LANGUAGE FUNCTION==
    '=====================================

    Public Sub PreProcessing_Language(ByVal frm As frmPreProcessing)
        With frm
            Dim strLanguage_PreProcessing() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_PreProcessing)
            On Error Resume Next

            '.Text = strLanguage_PreProcessing(1) '

        End With
    End Sub

    Public Sub CreateSQLView_Language(ByVal frm As frmCreateSQLView)
        With frm
            Dim strLanguage_CreateSQLView() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_CreateSQLView)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub DataSummaryVisualiser_Language(ByVal frm As frmDataSummaryVisualiser)
        With frm
            Dim strLanguage_DataSummaryVisualiser() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_DataSummaryVisualiser)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub VariableInfoVisualiser_Language(ByVal frm As frmVariableInfoVisualiser)
        With frm
            Dim strLanguage_VariableInfoVisualiser() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_VariableInfoVisualiser)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub ClusteringStep0_Language(ByVal frm As frmClusteringStep0)
        With frm
            Dim strLanguage_ClusteringStep0() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_ClusteringStep0)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub ClusteringStep1_Language(ByVal frm As frmClusteringStep1)
        With frm
            Dim strLanguage_ClusteringStep1() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_ClusteringStep1)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub Classification_Language(ByVal frm As frmClassification)
        With frm
            Dim strLanguage_Classification() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_Classification)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub Statistics_Language(ByVal frm As frmStatistics)
        With frm
            Dim strLanguage_Statistics() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_Statistics)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub LogisticRegression_Language(ByVal frm As frmLogisticRegression)
        With frm
            Dim strLanguage_LogisticRegression() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_LogisticRegression)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub DecisionTrees_Language(ByVal frm As frmDecisionTrees)
        With frm
            Dim strLanguage_DecisionTrees() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_DecisionTrees)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub NaiveBayes_Language(ByVal frm As frmNaiveBayes)
        With frm
            Dim strLanguage_NaiveBayes() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_NaiveBayes)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub RandomForest_Language(ByVal frm As frmRandomForest)
        With frm
            Dim strLanguage_RandomForest() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_RandomForest)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub StochasticGradientBoosting_Language(ByVal frm As frmStochasticGradientBoosting)
        With frm
            Dim strLanguage_StochasticGradientBoosting() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_StochasticGradientBoosting)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub StochasticDualCoordinateAscent_Language(ByVal frm As frmStochasticDualCoordinateAscent)
        With frm
            Dim strLanguage_StochasticDualCoordinateAscent() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_StochasticDualCoordinateAscent)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub BoostedDecisionTrees_Language(ByVal frm As frmBoostedDecisionTrees)
        With frm
            Dim strLanguage_BoostedDecisionTrees() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_BoostedDecisionTrees)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub EnsembleofDecisionTrees_Language(ByVal frm As frmEnsembleofDecisionTrees)
        With frm
            Dim strLanguage_EnsembleofDecisionTrees() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_EnsembleofDecisionTrees)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub NeuralNetworks_Language(ByVal frm As frmNeuralNetworks)
        With frm
            Dim strLanguage_NeuralNetworks() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_NeuralNetworks)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub

    Public Sub FastLogisticRegression_Language(ByVal frm As frmFastLogisticRegression)
        With frm
            Dim strLanguage_FastLogisticRegression() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_FastLogisticRegression)
            On Error Resume Next

            '.Text = strLanguage_strLanguage_CreateSQLView(1) '

        End With
    End Sub


End Module
