Option Strict On
Imports System.IO
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

            .mniPreProcessing.Text = strLanguage_Main(136)
            .mniCreateGeoColumns.Text = strLanguage_Main(137)
            .mniCreateNeededSQLViews.Text = strLanguage_Main(138)
            .mniGeoLocate.Text = strLanguage_Main(108)
            .mniGeoLocationStatus.Text = strLanguage_Main(139)
            .mniExportListofProblematicAddresses.Text = strLanguage_Main(140)
            .mniResetInvalidGeolocationEntries.Text = strLanguage_Main(141)
            .mniPreProcessTheData.Text = strLanguage_Main(142)
            .mniClustering.Text = strLanguage_Main(143)
            .mniClusteringStep0.Text = strLanguage_Main(144)
            .mniClusteringStep1.Text = strLanguage_Main(145)
            .mniClassification.Text = strLanguage_Main(146)
            .mniFormTrainAndTestSets.Text = strLanguage_Main(147)
            .mniLogisticRegression.Text = strLanguage_Main(148)
            .mniDecisionTrees.Text = strLanguage_Main(149)
            .mniNaiveBayes.Text = strLanguage_Main(150)
            .mniRandomForest.Text = strLanguage_Main(151)
            .mniStochasticGradientBoosting.Text = strLanguage_Main(152)
            .mniStochasticDualCoordinateAscent.Text = strLanguage_Main(153)
            .mniBoostedDecisionTrees.Text = strLanguage_Main(154)
            .mniEnsambleOfDecisionTrees.Text = strLanguage_Main(155)
            .mniNeuralNetworks.Text = strLanguage_Main(156)
            .mniFastLogisticRegression.Text = strLanguage_Main(157)


            'TIPS
            .mniCreateGeoColumns.ToolTipText = sa(strLanguage_Main_Tips(50), vbCrLf)
            .mniCreateNeededSQLViews.ToolTipText = sa(strLanguage_Main_Tips(51), vbCrLf)
            .mniGeoLocate.ToolTipText = sa(strLanguage_Main_Tips(52), vbCrLf)
            .mniGeoLocationStatus.ToolTipText = sa(strLanguage_Main_Tips(53), vbCrLf)
            .mniExportListofProblematicAddresses.ToolTipText = sa(strLanguage_Main_Tips(54), vbCrLf)
            .mniResetInvalidGeolocationEntries.ToolTipText = sa(strLanguage_Main_Tips(55), vbCrLf)
            .mniPreProcessTheData.ToolTipText = sa(strLanguage_Main_Tips(56), vbCrLf)
            .mniClusteringStep0.ToolTipText = sa(strLanguage_Main_Tips(57), vbCrLf)
            .mniClusteringStep1.ToolTipText = sa(strLanguage_Main_Tips(58), vbCrLf)
            .mniFormTrainAndTestSets.ToolTipText = sa(strLanguage_Main_Tips(59), vbCrLf)
            .mniLogisticRegression.ToolTipText = sa(strLanguage_Main_Tips(60), vbCrLf)
            .mniDecisionTrees.ToolTipText = sa(strLanguage_Main_Tips(61), vbCrLf)
            .mniNaiveBayes.ToolTipText = sa(strLanguage_Main_Tips(62), vbCrLf)
            .mniRandomForest.ToolTipText = sa(strLanguage_Main_Tips(63), vbCrLf)
            .mniStochasticGradientBoosting.ToolTipText = sa(strLanguage_Main_Tips(64), vbCrLf)
            .mniStochasticDualCoordinateAscent.ToolTipText = sa(strLanguage_Main_Tips(65), vbCrLf)
            .mniBoostedDecisionTrees.ToolTipText = sa(strLanguage_Main_Tips(66), vbCrLf)
            .mniEnsambleOfDecisionTrees.ToolTipText = sa(strLanguage_Main_Tips(67), vbCrLf)
            .mniNeuralNetworks.ToolTipText = sa(strLanguage_Main_Tips(68), vbCrLf)
            .mniFastLogisticRegression.ToolTipText = sa(strLanguage_Main_Tips(69), vbCrLf)
            '/TIP

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

            Dim WelcomeHTMLFile As String = strLanguageFolders & CurrentLanguage & "\" & .Name & "_txtInfo.html"
            If File.Exists(WelcomeHTMLFile) AndAlso File.ReadAllText(WelcomeHTMLFile) <> "" Then
                Uri.TryCreate(WelcomeHTMLFile, UriKind.RelativeOrAbsolute, .wbWelcome.Url)
            End If

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
            Dim strLanguage_PreProcessing_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_PreProcessing_Tips)
            On Error Resume Next

            .Text = strLanguage_PreProcessing(1) '
            .lblLoading.Text = strLanguage_PreProcessing(2) '
            .gbOptions.Text = strLanguage_PreProcessing(3) '
            .chkUseExistingXDFFile.Text = strLanguage_PreProcessing(4) '
            .chkShowDataSummary.Text = strLanguage_PreProcessing(5) '
            .chkShowVariableInfo.Text = strLanguage_PreProcessing(6) '
            .chkShowGeoLocGraph.Text = strLanguage_PreProcessing(7) '
            .btnSelectAll.Text = strLanguage_PreProcessing(8) '
            .chkStatisticsMode.Text = strLanguage_PreProcessing(9) '
            .chkCleanXDFFile.Text = strLanguage_PreProcessing(10) '
            .btnPreProcess.Text = strLanguage_PreProcessing(11) '


            'TIPS
            .ttMain.SetToolTip(.gbOptions, sa(strLanguage_PreProcessing_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.chkUseExistingXDFFile, sa(strLanguage_PreProcessing_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.chkShowDataSummary, sa(strLanguage_PreProcessing_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.chkShowVariableInfo, sa(strLanguage_PreProcessing_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.chkShowGeoLocGraph, sa(strLanguage_PreProcessing_Tips(5), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_PreProcessing_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.chkStatisticsMode, sa(strLanguage_PreProcessing_Tips(7), vbCrLf))
            .ttMain.SetToolTip(.chkCleanXDFFile, sa(strLanguage_PreProcessing_Tips(8), vbCrLf))
            .ttMain.SetToolTip(.btnPreProcess, sa(strLanguage_PreProcessing_Tips(9), vbCrLf))
            '/TIPS

        End With
    End Sub

    Public Sub CreateSQLView_Language(ByVal frm As frmCreateSQLView)
        With frm
            Dim strLanguage_CreateSQLView() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_CreateSQLView)
            Dim strLanguage_CreateSQLView_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_CreateSQLView_Tips)
            On Error Resume Next

            .Text = strLanguage_CreateSQLView(1)
            .clbSQLViews.Text = strLanguage_CreateSQLView(2)
            .btnSelectAll.Text = strLanguage_CreateSQLView(3)
            .gbSQLViewOptions.Text = strLanguage_CreateSQLView(4)
            .lblElaboration.Text = strLanguage_CreateSQLView(5)
            .rdbDoNothing.Text = strLanguage_CreateSQLView(6)
            .rdbAlterIt.Text = strLanguage_CreateSQLView(7)
            .rdbDeleteItAndCreateIt.Text = strLanguage_CreateSQLView(8)
            .chkDeleteAll.Text = strLanguage_CreateSQLView(9)
            .lblWarning.Text = sa(strLanguage_CreateSQLView(10), vbCrLf)
            .btnCreateSQLViews.Text = strLanguage_CreateSQLView(11)
            .gbSQLViews.Text = strLanguage_CreateSQLView(27)

            'TIPS
            .ttMain.SetToolTip(.gbSQLViews, sa(strLanguage_CreateSQLView_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.clbSQLViews, sa(strLanguage_CreateSQLView_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_CreateSQLView_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.rdbDoNothing, sa(strLanguage_CreateSQLView_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.rdbAlterIt, sa(strLanguage_CreateSQLView_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.rdbDeleteItAndCreateIt, sa(strLanguage_CreateSQLView_Tips(5), vbCrLf))
            .ttMain.SetToolTip(.chkDeleteAll, sa(strLanguage_CreateSQLView_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.btnCreateSQLViews, sa(strLanguage_CreateSQLView_Tips(7), vbCrLf))
            '/TIS
        End With
    End Sub

    Public Sub DataSummaryVisualiser_Language(ByVal frm As frmDataSummaryVisualiser)
        With frm
            Dim strLanguage_DataSummaryVisualiser() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_DataSummaryVisualiser)
            On Error Resume Next

            .lblSelectedCells.Text = strLanguage_DataSummaryVisualiser(6) 'Selected Cells:
            .lblSelectedRows.Text = strLanguage_DataSummaryVisualiser(7) 'Selected Rows:
            .lblSelectedColumns.Text = strLanguage_DataSummaryVisualiser(8) 'Selected Columns:
            .lblRowsCount.Text = strLanguage_DataSummaryVisualiser(12) 'Rows Count:
            .lblColumnsCount.Text = strLanguage_DataSummaryVisualiser(13) 'Columns Count:
            .lblCurRow.Text = strLanguage_DataSummaryVisualiser(14) 'Current Row:
            .lblCurColumn.Text = strLanguage_DataSummaryVisualiser(15) 'Current Column:

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
            Dim strLanguage_ClusteringStep0_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_ClusteringStep0_Tips)
            On Error Resume Next

            .Text = strLanguage_ClusteringStep0(1) '
            .lblLoading.Text = strLanguage_ClusteringStep0(2) '
            .gbOptions.Text = strLanguage_ClusteringStep0(3) '
            .chkUseExistingXDFFile.Text = strLanguage_ClusteringStep0(4) '
            .chkShowDataSummary.Text = strLanguage_ClusteringStep0(5) '
            .chkShowVariableInfo.Text = strLanguage_ClusteringStep0(6) '
            .chkShowGeoLocGraph.Text = strLanguage_ClusteringStep0(7) '
            .btnSelectAll.Text = strLanguage_ClusteringStep0(8) '
            .chkStatisticsMode.Text = strLanguage_ClusteringStep0(9) '
            .chkCleanXDFFile.Text = strLanguage_ClusteringStep0(10) '
            .btnClustering0.Text = strLanguage_ClusteringStep0(11) '


            'TIPS
            .ttMain.SetToolTip(.gbOptions, sa(strLanguage_ClusteringStep0_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.chkUseExistingXDFFile, sa(strLanguage_ClusteringStep0_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.chkShowDataSummary, sa(strLanguage_ClusteringStep0_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.chkShowVariableInfo, sa(strLanguage_ClusteringStep0_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.chkShowGeoLocGraph, sa(strLanguage_ClusteringStep0_Tips(5), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_ClusteringStep0_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.chkStatisticsMode, sa(strLanguage_ClusteringStep0_Tips(7), vbCrLf))
            .ttMain.SetToolTip(.chkCleanXDFFile, sa(strLanguage_ClusteringStep0_Tips(8), vbCrLf))
            .ttMain.SetToolTip(.btnClustering0, sa(strLanguage_ClusteringStep0_Tips(9), vbCrLf))
            '/TIPS
        End With
    End Sub

    Public Sub ClusteringStep1_Language(ByVal frm As frmClusteringStep1)
        With frm
            Dim strLanguage_ClusteringStep1() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_ClusteringStep1)
            Dim strLanguage_ClusteringStep1_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_ClusteringStep1_Tips)
            On Error Resume Next

            .Text = strLanguage_ClusteringStep1(1) '
            .lblLoading.Text = strLanguage_ClusteringStep1(2) '
            .gbOptions.Text = strLanguage_ClusteringStep1(3) '
            .chkUseExistingXDFFile.Text = strLanguage_ClusteringStep1(4) '
            .chkShowDataSummary.Text = strLanguage_ClusteringStep1(5) '
            .chkShowVariableInfo.Text = strLanguage_ClusteringStep1(6) '
            .chkShowGeoLocGraph.Text = strLanguage_ClusteringStep1(7) '
            .btnSelectAll.Text = strLanguage_ClusteringStep1(8) '
            .chkStatisticsMode.Text = strLanguage_ClusteringStep1(9) '
            .chkCleanXDFFile.Text = strLanguage_ClusteringStep1(10) '
            .btnClustering1.Text = strLanguage_ClusteringStep1(11) '
            .chkSaveKMeansModel.Text = strLanguage_ClusteringStep1(16)
            .lblSavePath.Text = strLanguage_ClusteringStep1(22)
            .lblMaxClusterNum.Text = strLanguage_ClusteringStep1(23)


            'TIPS
            .ttMain.SetToolTip(.gbOptions, sa(strLanguage_ClusteringStep1_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.chkUseExistingXDFFile, sa(strLanguage_ClusteringStep1_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.chkShowDataSummary, sa(strLanguage_ClusteringStep1_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.chkShowVariableInfo, sa(strLanguage_ClusteringStep1_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.chkShowGeoLocGraph, sa(strLanguage_ClusteringStep1_Tips(5), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_ClusteringStep1_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.chkStatisticsMode, sa(strLanguage_ClusteringStep1_Tips(7), vbCrLf))
            .ttMain.SetToolTip(.chkCleanXDFFile, sa(strLanguage_ClusteringStep1_Tips(8), vbCrLf))
            .ttMain.SetToolTip(.btnClustering1, sa(strLanguage_ClusteringStep1_Tips(9), vbCrLf))
            .ttMain.SetToolTip(.chkSaveKMeansModel, sa(strLanguage_ClusteringStep1_Tips(10), vbCrLf))
            .ttMain.SetToolTip(.txtSavePath, sa(strLanguage_ClusteringStep1_Tips(11), vbCrLf))
            .ttMain.SetToolTip(.chkCleanXDFFile, sa(strLanguage_ClusteringStep1_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.txtMaxClusterNum, sa(strLanguage_ClusteringStep1_Tips(13), vbCrLf))
            '/TIPS
        End With
    End Sub

    Public Sub Classification_Language(ByVal frm As frmClassification)
        With frm
            Dim strLanguage_Classification() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_Classification)
            Dim strLanguage_ClusteringStep1_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_Classification_Tips)
            On Error Resume Next

            .Text = strLanguage_Classification(1) '
            .lblLoading.Text = strLanguage_Classification(2) '
            .gbOptions.Text = strLanguage_Classification(3) '
            .chkUseExistingXDFFile.Text = strLanguage_Classification(4) '
            .chkShowDataSummary.Text = strLanguage_Classification(5) '
            .chkShowVariableInfo.Text = strLanguage_Classification(6) '
            .btnSelectAll.Text = strLanguage_Classification(8) '
            .chkStatisticsMode.Text = strLanguage_Classification(9) '
            .chkCleanXDFFile.Text = strLanguage_Classification(10) '
            .btnClassification.Text = strLanguage_Classification(11) '
            .chkFormTrainSet.Text = strLanguage_Classification(16)
            .chkFormTestSet.Text = strLanguage_Classification(17)
            .chkVisualiseClassImbal.Text = strLanguage_Classification(18)
            .lblTrainPercent.Text = strLanguage_Classification(19)
            .chkShowTrainDataSummary.Text = strLanguage_Classification(20)
            .chkShowTrainVarInfo.Text = strLanguage_Classification(21)
            .chkShowTestDataSummary.Text = strLanguage_Classification(22)
            .chkShowTestVarInfo.Text = strLanguage_Classification(23)


            'TIPS
            .ttMain.SetToolTip(.gbOptions, sa(strLanguage_ClusteringStep1_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.chkUseExistingXDFFile, sa(strLanguage_ClusteringStep1_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.chkShowDataSummary, sa(strLanguage_ClusteringStep1_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.chkShowVariableInfo, sa(strLanguage_ClusteringStep1_Tips(4), vbCrLf))

            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_ClusteringStep1_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.chkStatisticsMode, sa(strLanguage_ClusteringStep1_Tips(7), vbCrLf))
            .ttMain.SetToolTip(.chkCleanXDFFile, sa(strLanguage_ClusteringStep1_Tips(8), vbCrLf))
            .ttMain.SetToolTip(.btnClassification, sa(strLanguage_ClusteringStep1_Tips(9), vbCrLf))

            .ttMain.SetToolTip(.chkFormTrainSet, sa(strLanguage_ClusteringStep1_Tips(10), vbCrLf))
            .ttMain.SetToolTip(.chkFormTestSet, sa(strLanguage_ClusteringStep1_Tips(11), vbCrLf))
            .ttMain.SetToolTip(.chkVisualiseClassImbal, sa(strLanguage_ClusteringStep1_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.chkShowTrainDataSummary, sa(strLanguage_ClusteringStep1_Tips(13), vbCrLf))
            .ttMain.SetToolTip(.chkShowTrainVarInfo, sa(strLanguage_ClusteringStep1_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.chkShowTestDataSummary, sa(strLanguage_ClusteringStep1_Tips(15), vbCrLf))
            .ttMain.SetToolTip(.chkShowTestVarInfo, sa(strLanguage_ClusteringStep1_Tips(16), vbCrLf))
            '/TIPS

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
            Dim strLanguage_LogisticRegression_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_LogisticRegression_Tips)
            On Error Resume Next

            .Text = strLanguage_LogisticRegression(1) '
            .lblLoading.Text = strLanguage_LogisticRegression(2) '
            .lblColumnsLoading.Text = strLanguage_LogisticRegression(2) '
            .tpGeneralOptions.Text = strLanguage_LogisticRegression(3) '
            .tpAlgorithmOptions.Text = strLanguage_LogisticRegression(4) '
            .gbOptions.Text = strLanguage_LogisticRegression(5) '
            .chkUseExistingModel.Text = strLanguage_LogisticRegression(6) '
            .chkMakePredictions.Text = strLanguage_LogisticRegression(7) '
            .chkSavePredictionModel.Text = strLanguage_LogisticRegression(8) '
            .lblSavePath.Text = strLanguage_LogisticRegression(9) '
            .chkShowDataSummary.Text = strLanguage_LogisticRegression(10) '
            .chkShowVariableInfo.Text = strLanguage_LogisticRegression(11) '
            .btnSelectAll.Text = strLanguage_LogisticRegression(12) '
            .chkStatisticsMode.Text = strLanguage_LogisticRegression(13) '
            .chkShowStatistics.Text = strLanguage_LogisticRegression(14) '
            .chkShowROCCurve.Text = strLanguage_LogisticRegression(15) '
            .chkOpenGraphDirectory.Text = strLanguage_LogisticRegression(16) '
            .lblRoundAt.Text = strLanguage_LogisticRegression(17) '
            .lblNGrams.Text = strLanguage_LogisticRegression(18) '
            If .lblCombinationsCount.Text = "1 Combination" Then .lblCombinationsCount.Text = "1" & strLanguage_LogisticRegression(19) '
            .chkColumnsCombinations.Text = strLanguage_LogisticRegression(21) '
            .chkUpToNGramsN.Text = strLanguage_LogisticRegression(22) '
            .lblInProgress.Text = strLanguage_LogisticRegression(23) '
            .btnRunModel.Text = strLanguage_LogisticRegression(24) '
            .btnSelectAllColumns.Text = strLanguage_LogisticRegression(29) '
            .gbSettings.Text = strLanguage_LogisticRegression(48) 'Settings:


            'TIPS
            .ttMain.SetToolTip(.chkUseExistingModel, sa(strLanguage_LogisticRegression_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.chkMakePredictions, sa(strLanguage_LogisticRegression_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.chkSavePredictionModel, sa(strLanguage_LogisticRegression_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.lblSavePath, sa(strLanguage_LogisticRegression_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.txtSavePath, sa(strLanguage_LogisticRegression_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.chkShowDataSummary, sa(strLanguage_LogisticRegression_Tips(5), vbCrLf))
            .ttMain.SetToolTip(.chkShowVariableInfo, sa(strLanguage_LogisticRegression_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_LogisticRegression_Tips(7), vbCrLf))
            .ttMain.SetToolTip(.chkStatisticsMode, sa(strLanguage_LogisticRegression_Tips(8), vbCrLf))
            .ttMain.SetToolTip(.chkShowStatistics, sa(strLanguage_LogisticRegression_Tips(9), vbCrLf))
            .ttMain.SetToolTip(.chkShowROCCurve, sa(strLanguage_LogisticRegression_Tips(10), vbCrLf))
            .ttMain.SetToolTip(.chkOpenGraphDirectory, sa(strLanguage_LogisticRegression_Tips(11), vbCrLf))
            .ttMain.SetToolTip(.lblRoundAt, sa(strLanguage_LogisticRegression_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.txtRoundAt, sa(strLanguage_LogisticRegression_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAllColumns, sa(strLanguage_LogisticRegression_Tips(13), vbCrLf))
            .ttMain.SetToolTip(.lblNGrams, sa(strLanguage_LogisticRegression_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.txtNGrams, sa(strLanguage_LogisticRegression_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.chkColumnsCombinations, sa(strLanguage_LogisticRegression_Tips(15), vbCrLf))
            .ttMain.SetToolTip(.chkUpToNGramsN, sa(strLanguage_LogisticRegression_Tips(16), vbCrLf))
            .ttMain.SetToolTip(.btnRunModel, sa(strLanguage_LogisticRegression_Tips(17), vbCrLf))
            .ttMain.SetToolTip(.clbColumns, sa(strLanguage_LogisticRegression_Tips(18), vbCrLf))

            .ttMain.SetToolTip(.txtReportProgress, sa(strLanguage_LogisticRegression_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.chkreportProgress, sa(strLanguage_LogisticRegression_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.txtBlocksPerRead, sa(strLanguage_LogisticRegression_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.chkBlocksPerRead, sa(strLanguage_LogisticRegression_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.txtrowSelection, sa(strLanguage_LogisticRegression_Tips(21), vbCrLf))
            .ttMain.SetToolTip(.chkrowSelection, sa(strLanguage_LogisticRegression_Tips(21), vbCrLf))

            .ttMain.SetToolTip(.cbCovCoef, sa(strLanguage_LogisticRegression_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.chkcovCoef, sa(strLanguage_LogisticRegression_Tips(22), vbCrLf))
            '/TIPS

        End With
    End Sub

    Public Sub DecisionTrees_Language(ByVal frm As frmDecisionTrees)
        With frm
            Dim strLanguage_DecisionTrees() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_DecisionTrees)
            Dim strLanguage_DecisionTrees_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_DecisionTrees_Tips)
            On Error Resume Next

            .Text = strLanguage_DecisionTrees(1) '
            .lblLoading.Text = strLanguage_DecisionTrees(2) '
            .lblColumnsLoading.Text = strLanguage_DecisionTrees(2) '
            .tpGeneralOptions.Text = strLanguage_DecisionTrees(3) '
            .tpAlgorithmOptions.Text = strLanguage_DecisionTrees(4) '
            .gbOptions.Text = strLanguage_DecisionTrees(5) '
            .chkUseExistingModel.Text = strLanguage_DecisionTrees(6) '
            .chkMakePredictions.Text = strLanguage_DecisionTrees(7) '
            .chkSavePredictionModel.Text = strLanguage_DecisionTrees(8) '
            .lblSavePath.Text = strLanguage_DecisionTrees(9) '
            .chkShowDataSummary.Text = strLanguage_DecisionTrees(10) '
            .chkShowVariableInfo.Text = strLanguage_DecisionTrees(11) '
            .btnSelectAll.Text = strLanguage_DecisionTrees(12) '
            .chkStatisticsMode.Text = strLanguage_DecisionTrees(13) '
            .chkShowStatistics.Text = strLanguage_DecisionTrees(14) '
            .chkShowROCCurve.Text = strLanguage_DecisionTrees(15) '
            .chkOpenGraphDirectory.Text = strLanguage_DecisionTrees(16) '
            .lblRoundAt.Text = strLanguage_DecisionTrees(17) '
            .lblNGrams.Text = strLanguage_DecisionTrees(18) '
            If .lblCombinationsCount.Text = "1 Combination" Then .lblCombinationsCount.Text = "1" & strLanguage_DecisionTrees(19) '
            .chkColumnsCombinations.Text = strLanguage_DecisionTrees(21) '
            .chkUpToNGramsN.Text = strLanguage_DecisionTrees(22) '
            .lblInProgress.Text = strLanguage_DecisionTrees(23) '
            .btnRunModel.Text = strLanguage_DecisionTrees(24) '
            .btnSelectAllColumns.Text = strLanguage_DecisionTrees(29) '
            .gbSettings.Text = strLanguage_DecisionTrees(48) 'Settings:


            'TIPS
            .ttMain.SetToolTip(.chkUseExistingModel, sa(strLanguage_DecisionTrees_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.chkMakePredictions, sa(strLanguage_DecisionTrees_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.chkSavePredictionModel, sa(strLanguage_DecisionTrees_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.lblSavePath, sa(strLanguage_DecisionTrees_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.txtSavePath, sa(strLanguage_DecisionTrees_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.chkShowDataSummary, sa(strLanguage_DecisionTrees_Tips(5), vbCrLf))
            .ttMain.SetToolTip(.chkShowVariableInfo, sa(strLanguage_DecisionTrees_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_DecisionTrees_Tips(7), vbCrLf))
            .ttMain.SetToolTip(.chkStatisticsMode, sa(strLanguage_DecisionTrees_Tips(8), vbCrLf))
            .ttMain.SetToolTip(.chkShowStatistics, sa(strLanguage_DecisionTrees_Tips(9), vbCrLf))
            .ttMain.SetToolTip(.chkShowROCCurve, sa(strLanguage_DecisionTrees_Tips(10), vbCrLf))
            .ttMain.SetToolTip(.chkOpenGraphDirectory, sa(strLanguage_DecisionTrees_Tips(11), vbCrLf))
            .ttMain.SetToolTip(.lblRoundAt, sa(strLanguage_DecisionTrees_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.txtRoundAt, sa(strLanguage_DecisionTrees_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAllColumns, sa(strLanguage_DecisionTrees_Tips(13), vbCrLf))
            .ttMain.SetToolTip(.lblNGrams, sa(strLanguage_DecisionTrees_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.txtNGrams, sa(strLanguage_DecisionTrees_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.chkColumnsCombinations, sa(strLanguage_DecisionTrees_Tips(15), vbCrLf))
            .ttMain.SetToolTip(.chkUpToNGramsN, sa(strLanguage_DecisionTrees_Tips(16), vbCrLf))
            .ttMain.SetToolTip(.btnRunModel, sa(strLanguage_DecisionTrees_Tips(17), vbCrLf))
            .ttMain.SetToolTip(.clbColumns, sa(strLanguage_DecisionTrees_Tips(18), vbCrLf))

            .ttMain.SetToolTip(.txtReportProgress, sa(strLanguage_DecisionTrees_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.chkreportProgress, sa(strLanguage_DecisionTrees_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.txtBlocksPerRead, sa(strLanguage_DecisionTrees_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.chkBlocksPerRead, sa(strLanguage_DecisionTrees_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.txtrowSelection, sa(strLanguage_DecisionTrees_Tips(21), vbCrLf))
            .ttMain.SetToolTip(.chkrowSelection, sa(strLanguage_DecisionTrees_Tips(21), vbCrLf))

            .ttMain.SetToolTip(.cbPlotTreeModel, sa(strLanguage_DecisionTrees_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.chkPlotTreeModel, sa(strLanguage_DecisionTrees_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.cbClassMethod, sa(strLanguage_DecisionTrees_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.chkClassMethod, sa(strLanguage_DecisionTrees_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.cbShowComplexityPlot, sa(strLanguage_DecisionTrees_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.chkShowComplexityPlot, sa(strLanguage_DecisionTrees_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.txtCP, sa(strLanguage_DecisionTrees_Tips(25), vbCrLf))
            .ttMain.SetToolTip(.chkCP, sa(strLanguage_DecisionTrees_Tips(25), vbCrLf))

            '/TIPS

        End With
    End Sub

    Public Sub NaiveBayes_Language(ByVal frm As frmNaiveBayes)
        With frm
            Dim strLanguage_NaiveBayes() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_NaiveBayes)
            Dim strLanguage_NaiveBayes_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_NaiveBayes_Tips)
            On Error Resume Next

            .Text = strLanguage_NaiveBayes(1) '
            .lblLoading.Text = strLanguage_NaiveBayes(2) '
            .lblColumnsLoading.Text = strLanguage_NaiveBayes(2) '
            .tpGeneralOptions.Text = strLanguage_NaiveBayes(3) '
            .tpAlgorithmOptions.Text = strLanguage_NaiveBayes(4) '
            .gbOptions.Text = strLanguage_NaiveBayes(5) '
            .chkUseExistingModel.Text = strLanguage_NaiveBayes(6) '
            .chkMakePredictions.Text = strLanguage_NaiveBayes(7) '
            .chkSavePredictionModel.Text = strLanguage_NaiveBayes(8) '
            .lblSavePath.Text = strLanguage_NaiveBayes(9) '
            .chkShowDataSummary.Text = strLanguage_NaiveBayes(10) '
            .chkShowVariableInfo.Text = strLanguage_NaiveBayes(11) '
            .btnSelectAll.Text = strLanguage_NaiveBayes(12) '
            .chkStatisticsMode.Text = strLanguage_NaiveBayes(13) '
            .chkShowStatistics.Text = strLanguage_NaiveBayes(14) '
            .chkShowROCCurve.Text = strLanguage_NaiveBayes(15) '
            .chkOpenGraphDirectory.Text = strLanguage_NaiveBayes(16) '
            .lblRoundAt.Text = strLanguage_NaiveBayes(17) '
            .lblNGrams.Text = strLanguage_NaiveBayes(18) '
            If .lblCombinationsCount.Text = "1 Combination" Then .lblCombinationsCount.Text = "1" & strLanguage_NaiveBayes(19) '
            .chkColumnsCombinations.Text = strLanguage_NaiveBayes(21) '
            .chkUpToNGramsN.Text = strLanguage_NaiveBayes(22) '
            .lblInProgress.Text = strLanguage_NaiveBayes(23) '
            .btnRunModel.Text = strLanguage_NaiveBayes(24) '
            .btnSelectAllColumns.Text = strLanguage_NaiveBayes(29) '
            .gbSettings.Text = strLanguage_NaiveBayes(48) 'Settings:


            'TIPS
            .ttMain.SetToolTip(.chkUseExistingModel, sa(strLanguage_NaiveBayes_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.chkMakePredictions, sa(strLanguage_NaiveBayes_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.chkSavePredictionModel, sa(strLanguage_NaiveBayes_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.lblSavePath, sa(strLanguage_NaiveBayes_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.txtSavePath, sa(strLanguage_NaiveBayes_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.chkShowDataSummary, sa(strLanguage_NaiveBayes_Tips(5), vbCrLf))
            .ttMain.SetToolTip(.chkShowVariableInfo, sa(strLanguage_NaiveBayes_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_NaiveBayes_Tips(7), vbCrLf))
            .ttMain.SetToolTip(.chkStatisticsMode, sa(strLanguage_NaiveBayes_Tips(8), vbCrLf))
            .ttMain.SetToolTip(.chkShowStatistics, sa(strLanguage_NaiveBayes_Tips(9), vbCrLf))
            .ttMain.SetToolTip(.chkShowROCCurve, sa(strLanguage_NaiveBayes_Tips(10), vbCrLf))
            .ttMain.SetToolTip(.chkOpenGraphDirectory, sa(strLanguage_NaiveBayes_Tips(11), vbCrLf))
            .ttMain.SetToolTip(.lblRoundAt, sa(strLanguage_NaiveBayes_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.txtRoundAt, sa(strLanguage_NaiveBayes_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAllColumns, sa(strLanguage_NaiveBayes_Tips(13), vbCrLf))
            .ttMain.SetToolTip(.lblNGrams, sa(strLanguage_NaiveBayes_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.txtNGrams, sa(strLanguage_NaiveBayes_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.chkColumnsCombinations, sa(strLanguage_NaiveBayes_Tips(15), vbCrLf))
            .ttMain.SetToolTip(.chkUpToNGramsN, sa(strLanguage_NaiveBayes_Tips(16), vbCrLf))
            .ttMain.SetToolTip(.btnRunModel, sa(strLanguage_NaiveBayes_Tips(17), vbCrLf))
            .ttMain.SetToolTip(.clbColumns, sa(strLanguage_NaiveBayes_Tips(18), vbCrLf))

            .ttMain.SetToolTip(.txtReportProgress, sa(strLanguage_NaiveBayes_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.chkreportProgress, sa(strLanguage_NaiveBayes_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.txtBlocksPerRead, sa(strLanguage_NaiveBayes_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.chkBlocksPerRead, sa(strLanguage_NaiveBayes_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.txtrowSelection, sa(strLanguage_NaiveBayes_Tips(21), vbCrLf))
            .ttMain.SetToolTip(.chkrowSelection, sa(strLanguage_NaiveBayes_Tips(21), vbCrLf))
            '/TIPS

        End With
    End Sub

    Public Sub RandomForest_Language(ByVal frm As frmRandomForest)
        With frm
            Dim strLanguage_RandomForest() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_RandomForest)
            Dim strLanguage_RandomForest_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_RandomForest_Tips)
            On Error Resume Next

            .Text = strLanguage_RandomForest(1) '
            .lblLoading.Text = strLanguage_RandomForest(2) '
            .lblColumnsLoading.Text = strLanguage_RandomForest(2) '
            .tpGeneralOptions.Text = strLanguage_RandomForest(3) '
            .tpAlgorithmOptions.Text = strLanguage_RandomForest(4) '
            .gbOptions.Text = strLanguage_RandomForest(5) '
            .chkUseExistingModel.Text = strLanguage_RandomForest(6) '
            .chkMakePredictions.Text = strLanguage_RandomForest(7) '
            .chkSavePredictionModel.Text = strLanguage_RandomForest(8) '
            .lblSavePath.Text = strLanguage_RandomForest(9) '
            .chkShowDataSummary.Text = strLanguage_RandomForest(10) '
            .chkShowVariableInfo.Text = strLanguage_RandomForest(11) '
            .btnSelectAll.Text = strLanguage_RandomForest(12) '
            .chkStatisticsMode.Text = strLanguage_RandomForest(13) '
            .chkShowStatistics.Text = strLanguage_RandomForest(14) '
            .chkShowROCCurve.Text = strLanguage_RandomForest(15) '
            .chkOpenGraphDirectory.Text = strLanguage_RandomForest(16) '
            .lblRoundAt.Text = strLanguage_RandomForest(17) '
            .lblNGrams.Text = strLanguage_RandomForest(18) '
            If .lblCombinationsCount.Text = "1 Combination" Then .lblCombinationsCount.Text = "1" & strLanguage_RandomForest(19) '
            .chkColumnsCombinations.Text = strLanguage_RandomForest(21) '
            .chkUpToNGramsN.Text = strLanguage_RandomForest(22) '
            .lblInProgress.Text = strLanguage_RandomForest(23) '
            .btnRunModel.Text = strLanguage_RandomForest(24) '
            .btnSelectAllColumns.Text = strLanguage_RandomForest(29) '
            .gbSettings.Text = strLanguage_RandomForest(48) 'Settings:


            'TIPS
            .ttMain.SetToolTip(.chkUseExistingModel, sa(strLanguage_RandomForest_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.chkMakePredictions, sa(strLanguage_RandomForest_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.chkSavePredictionModel, sa(strLanguage_RandomForest_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.lblSavePath, sa(strLanguage_RandomForest_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.txtSavePath, sa(strLanguage_RandomForest_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.chkShowDataSummary, sa(strLanguage_RandomForest_Tips(5), vbCrLf))
            .ttMain.SetToolTip(.chkShowVariableInfo, sa(strLanguage_RandomForest_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_RandomForest_Tips(7), vbCrLf))
            .ttMain.SetToolTip(.chkStatisticsMode, sa(strLanguage_RandomForest_Tips(8), vbCrLf))
            .ttMain.SetToolTip(.chkShowStatistics, sa(strLanguage_RandomForest_Tips(9), vbCrLf))
            .ttMain.SetToolTip(.chkShowROCCurve, sa(strLanguage_RandomForest_Tips(10), vbCrLf))
            .ttMain.SetToolTip(.chkOpenGraphDirectory, sa(strLanguage_RandomForest_Tips(11), vbCrLf))
            .ttMain.SetToolTip(.lblRoundAt, sa(strLanguage_RandomForest_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.txtRoundAt, sa(strLanguage_RandomForest_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAllColumns, sa(strLanguage_RandomForest_Tips(13), vbCrLf))
            .ttMain.SetToolTip(.lblNGrams, sa(strLanguage_RandomForest_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.txtNGrams, sa(strLanguage_RandomForest_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.chkColumnsCombinations, sa(strLanguage_RandomForest_Tips(15), vbCrLf))
            .ttMain.SetToolTip(.chkUpToNGramsN, sa(strLanguage_RandomForest_Tips(16), vbCrLf))
            .ttMain.SetToolTip(.btnRunModel, sa(strLanguage_RandomForest_Tips(17), vbCrLf))
            .ttMain.SetToolTip(.clbColumns, sa(strLanguage_RandomForest_Tips(18), vbCrLf))

            .ttMain.SetToolTip(.txtReportProgress, sa(strLanguage_RandomForest_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.chkreportProgress, sa(strLanguage_RandomForest_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.txtBlocksPerRead, sa(strLanguage_RandomForest_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.chkBlocksPerRead, sa(strLanguage_RandomForest_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.txtrowSelection, sa(strLanguage_RandomForest_Tips(21), vbCrLf))
            .ttMain.SetToolTip(.chkrowSelection, sa(strLanguage_RandomForest_Tips(21), vbCrLf))

            .ttMain.SetToolTip(.cbPlotVarImportance, sa(strLanguage_RandomForest_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.chkPlotVarImportance, sa(strLanguage_RandomForest_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.cbClassMethod, sa(strLanguage_RandomForest_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.chkClassMethod, sa(strLanguage_RandomForest_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.cbShowOOBEPlot, sa(strLanguage_RandomForest_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.chkShowOOBEPlot, sa(strLanguage_RandomForest_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.txtCP, sa(strLanguage_RandomForest_Tips(25), vbCrLf))
            .ttMain.SetToolTip(.chkCP, sa(strLanguage_RandomForest_Tips(25), vbCrLf))
            .ttMain.SetToolTip(.txtnTree, sa(strLanguage_RandomForest_Tips(26), vbCrLf))
            .ttMain.SetToolTip(.chknTree, sa(strLanguage_RandomForest_Tips(26), vbCrLf))
            .ttMain.SetToolTip(.txtmTry, sa(strLanguage_RandomForest_Tips(27), vbCrLf))
            .ttMain.SetToolTip(.chkmTry, sa(strLanguage_RandomForest_Tips(27), vbCrLf))
            .ttMain.SetToolTip(.txtmaxDepth, sa(strLanguage_RandomForest_Tips(28), vbCrLf))
            .ttMain.SetToolTip(.chkmaxDepth, sa(strLanguage_RandomForest_Tips(28), vbCrLf))
            '/TIPS

        End With
    End Sub

    Public Sub StochasticGradientBoosting_Language(ByVal frm As frmStochasticGradientBoosting)
        With frm
            Dim strLanguage_StochasticGradientBoosting() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_StochasticGradientBoosting)
            Dim strLanguage_StochasticGradientBoosting_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_StochasticGradientBoosting_Tips)
            On Error Resume Next

            .Text = strLanguage_StochasticGradientBoosting(1) '
            .lblLoading.Text = strLanguage_StochasticGradientBoosting(2) '
            .lblColumnsLoading.Text = strLanguage_StochasticGradientBoosting(2) '
            .tpGeneralOptions.Text = strLanguage_StochasticGradientBoosting(3) '
            .tpAlgorithmOptions.Text = strLanguage_StochasticGradientBoosting(4) '
            .gbOptions.Text = strLanguage_StochasticGradientBoosting(5) '
            .chkUseExistingModel.Text = strLanguage_StochasticGradientBoosting(6) '
            .chkMakePredictions.Text = strLanguage_StochasticGradientBoosting(7) '
            .chkSavePredictionModel.Text = strLanguage_StochasticGradientBoosting(8) '
            .lblSavePath.Text = strLanguage_StochasticGradientBoosting(9) '
            .chkShowDataSummary.Text = strLanguage_StochasticGradientBoosting(10) '
            .chkShowVariableInfo.Text = strLanguage_StochasticGradientBoosting(11) '
            .btnSelectAll.Text = strLanguage_StochasticGradientBoosting(12) '
            .chkStatisticsMode.Text = strLanguage_StochasticGradientBoosting(13) '
            .chkShowStatistics.Text = strLanguage_StochasticGradientBoosting(14) '
            .chkShowROCCurve.Text = strLanguage_StochasticGradientBoosting(15) '
            .chkOpenGraphDirectory.Text = strLanguage_StochasticGradientBoosting(16) '
            .lblRoundAt.Text = strLanguage_StochasticGradientBoosting(17) '
            .lblNGrams.Text = strLanguage_StochasticGradientBoosting(18) '
            If .lblCombinationsCount.Text = "1 Combination" Then .lblCombinationsCount.Text = "1" & strLanguage_StochasticGradientBoosting(19) '
            .chkColumnsCombinations.Text = strLanguage_StochasticGradientBoosting(21) '
            .chkUpToNGramsN.Text = strLanguage_StochasticGradientBoosting(22) '
            .lblInProgress.Text = strLanguage_StochasticGradientBoosting(23) '
            .btnRunModel.Text = strLanguage_StochasticGradientBoosting(24) '
            .btnSelectAllColumns.Text = strLanguage_StochasticGradientBoosting(29) '
            .gbSettings.Text = strLanguage_StochasticGradientBoosting(48) 'Settings:


            'TIPS
            .ttMain.SetToolTip(.chkUseExistingModel, sa(strLanguage_StochasticGradientBoosting_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.chkMakePredictions, sa(strLanguage_StochasticGradientBoosting_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.chkSavePredictionModel, sa(strLanguage_StochasticGradientBoosting_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.lblSavePath, sa(strLanguage_StochasticGradientBoosting_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.txtSavePath, sa(strLanguage_StochasticGradientBoosting_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.chkShowDataSummary, sa(strLanguage_StochasticGradientBoosting_Tips(5), vbCrLf))
            .ttMain.SetToolTip(.chkShowVariableInfo, sa(strLanguage_StochasticGradientBoosting_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_StochasticGradientBoosting_Tips(7), vbCrLf))
            .ttMain.SetToolTip(.chkStatisticsMode, sa(strLanguage_StochasticGradientBoosting_Tips(8), vbCrLf))
            .ttMain.SetToolTip(.chkShowStatistics, sa(strLanguage_StochasticGradientBoosting_Tips(9), vbCrLf))
            .ttMain.SetToolTip(.chkShowROCCurve, sa(strLanguage_StochasticGradientBoosting_Tips(10), vbCrLf))
            .ttMain.SetToolTip(.chkOpenGraphDirectory, sa(strLanguage_StochasticGradientBoosting_Tips(11), vbCrLf))
            .ttMain.SetToolTip(.lblRoundAt, sa(strLanguage_StochasticGradientBoosting_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.txtRoundAt, sa(strLanguage_StochasticGradientBoosting_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAllColumns, sa(strLanguage_StochasticGradientBoosting_Tips(13), vbCrLf))
            .ttMain.SetToolTip(.lblNGrams, sa(strLanguage_StochasticGradientBoosting_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.txtNGrams, sa(strLanguage_StochasticGradientBoosting_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.chkColumnsCombinations, sa(strLanguage_StochasticGradientBoosting_Tips(15), vbCrLf))
            .ttMain.SetToolTip(.chkUpToNGramsN, sa(strLanguage_StochasticGradientBoosting_Tips(16), vbCrLf))
            .ttMain.SetToolTip(.btnRunModel, sa(strLanguage_StochasticGradientBoosting_Tips(17), vbCrLf))
            .ttMain.SetToolTip(.clbColumns, sa(strLanguage_StochasticGradientBoosting_Tips(18), vbCrLf))

            .ttMain.SetToolTip(.txtReportProgress, sa(strLanguage_StochasticGradientBoosting_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.chkreportProgress, sa(strLanguage_StochasticGradientBoosting_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.txtBlocksPerRead, sa(strLanguage_StochasticGradientBoosting_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.chkBlocksPerRead, sa(strLanguage_StochasticGradientBoosting_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.txtrowSelection, sa(strLanguage_StochasticGradientBoosting_Tips(21), vbCrLf))
            .ttMain.SetToolTip(.chkrowSelection, sa(strLanguage_StochasticGradientBoosting_Tips(21), vbCrLf))

            .ttMain.SetToolTip(.txtnTree, sa(strLanguage_StochasticGradientBoosting_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.chknTree, sa(strLanguage_StochasticGradientBoosting_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.txtmTry, sa(strLanguage_StochasticGradientBoosting_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.chkmTry, sa(strLanguage_StochasticGradientBoosting_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.txtmaxDepth, sa(strLanguage_StochasticGradientBoosting_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.chkmaxDepth, sa(strLanguage_StochasticGradientBoosting_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.txtCP, sa(strLanguage_StochasticGradientBoosting_Tips(25), vbCrLf))
            .ttMain.SetToolTip(.chkCP, sa(strLanguage_StochasticGradientBoosting_Tips(25), vbCrLf))
            .ttMain.SetToolTip(.cblossFunction, sa(strLanguage_StochasticGradientBoosting_Tips(26), vbCrLf))
            .ttMain.SetToolTip(.chklossFunction, sa(strLanguage_StochasticGradientBoosting_Tips(26), vbCrLf))
            .ttMain.SetToolTip(.cbPlotVarImportance, sa(strLanguage_StochasticGradientBoosting_Tips(27), vbCrLf))
            .ttMain.SetToolTip(.chkPlotVarImportance, sa(strLanguage_StochasticGradientBoosting_Tips(27), vbCrLf))
            .ttMain.SetToolTip(.cbShowOOBEPlot, sa(strLanguage_StochasticGradientBoosting_Tips(28), vbCrLf))
            .ttMain.SetToolTip(.chkShowOOBEPlot, sa(strLanguage_StochasticGradientBoosting_Tips(28), vbCrLf))
            '/TIPS

        End With
    End Sub

    Public Sub StochasticDualCoordinateAscent_Language(ByVal frm As frmStochasticDualCoordinateAscent)
        With frm
            Dim strLanguage_StochasticDualCoordinateAscent() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_StochasticDualCoordinateAscent)
            Dim strLanguage_StochasticDualCoordinateAscent_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_StochasticDualCoordinateAscent_Tips)
            On Error Resume Next

            .Text = strLanguage_StochasticDualCoordinateAscent(1) '
            .lblLoading.Text = strLanguage_StochasticDualCoordinateAscent(2) '
            .lblColumnsLoading.Text = strLanguage_StochasticDualCoordinateAscent(2) '
            .tpGeneralOptions.Text = strLanguage_StochasticDualCoordinateAscent(3) '
            .tpAlgorithmOptions.Text = strLanguage_StochasticDualCoordinateAscent(4) '
            .gbOptions.Text = strLanguage_StochasticDualCoordinateAscent(5) '
            .chkUseExistingModel.Text = strLanguage_StochasticDualCoordinateAscent(6) '
            .chkMakePredictions.Text = strLanguage_StochasticDualCoordinateAscent(7) '
            .chkSavePredictionModel.Text = strLanguage_StochasticDualCoordinateAscent(8) '
            .lblSavePath.Text = strLanguage_StochasticDualCoordinateAscent(9) '
            .chkShowDataSummary.Text = strLanguage_StochasticDualCoordinateAscent(10) '
            .chkShowVariableInfo.Text = strLanguage_StochasticDualCoordinateAscent(11) '
            .btnSelectAll.Text = strLanguage_StochasticDualCoordinateAscent(12) '
            .chkStatisticsMode.Text = strLanguage_StochasticDualCoordinateAscent(13) '
            .chkShowStatistics.Text = strLanguage_StochasticDualCoordinateAscent(14) '
            .chkShowROCCurve.Text = strLanguage_StochasticDualCoordinateAscent(15) '
            .chkOpenGraphDirectory.Text = strLanguage_StochasticDualCoordinateAscent(16) '
            .lblRoundAt.Text = strLanguage_StochasticDualCoordinateAscent(17) '
            .lblNGrams.Text = strLanguage_StochasticDualCoordinateAscent(18) '
            If .lblCombinationsCount.Text = "1 Combination" Then .lblCombinationsCount.Text = "1" & strLanguage_StochasticDualCoordinateAscent(19) '
            .chkColumnsCombinations.Text = strLanguage_StochasticDualCoordinateAscent(21) '
            .chkUpToNGramsN.Text = strLanguage_StochasticDualCoordinateAscent(22) '
            .lblInProgress.Text = strLanguage_StochasticDualCoordinateAscent(23) '
            .btnRunModel.Text = strLanguage_StochasticDualCoordinateAscent(24) '
            .btnSelectAllColumns.Text = strLanguage_StochasticDualCoordinateAscent(29) '
            .gbSettings.Text = strLanguage_StochasticDualCoordinateAscent(48) 'Settings:


            'TIPS
            .ttMain.SetToolTip(.chkUseExistingModel, sa(strLanguage_StochasticDualCoordinateAscent_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.chkMakePredictions, sa(strLanguage_StochasticDualCoordinateAscent_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.chkSavePredictionModel, sa(strLanguage_StochasticDualCoordinateAscent_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.lblSavePath, sa(strLanguage_StochasticDualCoordinateAscent_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.txtSavePath, sa(strLanguage_StochasticDualCoordinateAscent_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.chkShowDataSummary, sa(strLanguage_StochasticDualCoordinateAscent_Tips(5), vbCrLf))
            .ttMain.SetToolTip(.chkShowVariableInfo, sa(strLanguage_StochasticDualCoordinateAscent_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_StochasticDualCoordinateAscent_Tips(7), vbCrLf))
            .ttMain.SetToolTip(.chkStatisticsMode, sa(strLanguage_StochasticDualCoordinateAscent_Tips(8), vbCrLf))
            .ttMain.SetToolTip(.chkShowStatistics, sa(strLanguage_StochasticDualCoordinateAscent_Tips(9), vbCrLf))
            .ttMain.SetToolTip(.chkShowROCCurve, sa(strLanguage_StochasticDualCoordinateAscent_Tips(10), vbCrLf))
            .ttMain.SetToolTip(.chkOpenGraphDirectory, sa(strLanguage_StochasticDualCoordinateAscent_Tips(11), vbCrLf))
            .ttMain.SetToolTip(.lblRoundAt, sa(strLanguage_StochasticDualCoordinateAscent_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.txtRoundAt, sa(strLanguage_StochasticDualCoordinateAscent_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAllColumns, sa(strLanguage_StochasticDualCoordinateAscent_Tips(13), vbCrLf))
            .ttMain.SetToolTip(.lblNGrams, sa(strLanguage_StochasticDualCoordinateAscent_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.txtNGrams, sa(strLanguage_StochasticDualCoordinateAscent_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.chkColumnsCombinations, sa(strLanguage_StochasticDualCoordinateAscent_Tips(15), vbCrLf))
            .ttMain.SetToolTip(.chkUpToNGramsN, sa(strLanguage_StochasticDualCoordinateAscent_Tips(16), vbCrLf))
            .ttMain.SetToolTip(.btnRunModel, sa(strLanguage_StochasticDualCoordinateAscent_Tips(17), vbCrLf))
            .ttMain.SetToolTip(.clbColumns, sa(strLanguage_StochasticDualCoordinateAscent_Tips(18), vbCrLf))

            .ttMain.SetToolTip(.txtReportProgress, sa(strLanguage_StochasticDualCoordinateAscent_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.chkreportProgress, sa(strLanguage_StochasticDualCoordinateAscent_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.txtBlocksPerRead, sa(strLanguage_StochasticDualCoordinateAscent_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.chkBlocksPerRead, sa(strLanguage_StochasticDualCoordinateAscent_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.txtrowSelection, sa(strLanguage_StochasticDualCoordinateAscent_Tips(21), vbCrLf))
            .ttMain.SetToolTip(.chkrowSelection, sa(strLanguage_StochasticDualCoordinateAscent_Tips(21), vbCrLf))

            .ttMain.SetToolTip(.cbtype, sa(strLanguage_StochasticDualCoordinateAscent_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.chktype, sa(strLanguage_StochasticDualCoordinateAscent_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.cbnormalize, sa(strLanguage_StochasticDualCoordinateAscent_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.chknormalize, sa(strLanguage_StochasticDualCoordinateAscent_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.txtconvergenceTolerance, sa(strLanguage_StochasticDualCoordinateAscent_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.chkconvergenceTolerance, sa(strLanguage_StochasticDualCoordinateAscent_Tips(24), vbCrLf))
            '/TIPS

        End With
    End Sub

    Public Sub BoostedDecisionTrees_Language(ByVal frm As frmBoostedDecisionTrees)
        With frm
            Dim strLanguage_BoostedDecisionTrees() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_BoostedDecisionTrees)
            Dim strLanguage_BoostedDecisionTrees_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_BoostedDecisionTrees_Tips)
            On Error Resume Next

            .Text = strLanguage_BoostedDecisionTrees(1) '
            .lblLoading.Text = strLanguage_BoostedDecisionTrees(2) '
            .lblColumnsLoading.Text = strLanguage_BoostedDecisionTrees(2) '
            .tpGeneralOptions.Text = strLanguage_BoostedDecisionTrees(3) '
            .tpAlgorithmOptions.Text = strLanguage_BoostedDecisionTrees(4) '
            .gbOptions.Text = strLanguage_BoostedDecisionTrees(5) '
            .chkUseExistingModel.Text = strLanguage_BoostedDecisionTrees(6) '
            .chkMakePredictions.Text = strLanguage_BoostedDecisionTrees(7) '
            .chkSavePredictionModel.Text = strLanguage_BoostedDecisionTrees(8) '
            .lblSavePath.Text = strLanguage_BoostedDecisionTrees(9) '
            .chkShowDataSummary.Text = strLanguage_BoostedDecisionTrees(10) '
            .chkShowVariableInfo.Text = strLanguage_BoostedDecisionTrees(11) '
            .btnSelectAll.Text = strLanguage_BoostedDecisionTrees(12) '
            .chkStatisticsMode.Text = strLanguage_BoostedDecisionTrees(13) '
            .chkShowStatistics.Text = strLanguage_BoostedDecisionTrees(14) '
            .chkShowROCCurve.Text = strLanguage_BoostedDecisionTrees(15) '
            .chkOpenGraphDirectory.Text = strLanguage_BoostedDecisionTrees(16) '
            .lblRoundAt.Text = strLanguage_BoostedDecisionTrees(17) '
            .lblNGrams.Text = strLanguage_BoostedDecisionTrees(18) '
            If .lblCombinationsCount.Text = "1 Combination" Then .lblCombinationsCount.Text = "1" & strLanguage_BoostedDecisionTrees(19) '
            .chkColumnsCombinations.Text = strLanguage_BoostedDecisionTrees(21) '
            .chkUpToNGramsN.Text = strLanguage_BoostedDecisionTrees(22) '
            .lblInProgress.Text = strLanguage_BoostedDecisionTrees(23) '
            .btnRunModel.Text = strLanguage_BoostedDecisionTrees(24) '
            .btnSelectAllColumns.Text = strLanguage_BoostedDecisionTrees(29) '
            .gbSettings.Text = strLanguage_BoostedDecisionTrees(48) 'Settings:


            'TIPS
            .ttMain.SetToolTip(.chkUseExistingModel, sa(strLanguage_BoostedDecisionTrees_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.chkMakePredictions, sa(strLanguage_BoostedDecisionTrees_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.chkSavePredictionModel, sa(strLanguage_BoostedDecisionTrees_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.lblSavePath, sa(strLanguage_BoostedDecisionTrees_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.txtSavePath, sa(strLanguage_BoostedDecisionTrees_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.chkShowDataSummary, sa(strLanguage_BoostedDecisionTrees_Tips(5), vbCrLf))
            .ttMain.SetToolTip(.chkShowVariableInfo, sa(strLanguage_BoostedDecisionTrees_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_BoostedDecisionTrees_Tips(7), vbCrLf))
            .ttMain.SetToolTip(.chkStatisticsMode, sa(strLanguage_BoostedDecisionTrees_Tips(8), vbCrLf))
            .ttMain.SetToolTip(.chkShowStatistics, sa(strLanguage_BoostedDecisionTrees_Tips(9), vbCrLf))
            .ttMain.SetToolTip(.chkShowROCCurve, sa(strLanguage_BoostedDecisionTrees_Tips(10), vbCrLf))
            .ttMain.SetToolTip(.chkOpenGraphDirectory, sa(strLanguage_BoostedDecisionTrees_Tips(11), vbCrLf))
            .ttMain.SetToolTip(.lblRoundAt, sa(strLanguage_BoostedDecisionTrees_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.txtRoundAt, sa(strLanguage_BoostedDecisionTrees_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAllColumns, sa(strLanguage_BoostedDecisionTrees_Tips(13), vbCrLf))
            .ttMain.SetToolTip(.lblNGrams, sa(strLanguage_BoostedDecisionTrees_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.txtNGrams, sa(strLanguage_BoostedDecisionTrees_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.chkColumnsCombinations, sa(strLanguage_BoostedDecisionTrees_Tips(15), vbCrLf))
            .ttMain.SetToolTip(.chkUpToNGramsN, sa(strLanguage_BoostedDecisionTrees_Tips(16), vbCrLf))
            .ttMain.SetToolTip(.btnRunModel, sa(strLanguage_BoostedDecisionTrees_Tips(17), vbCrLf))
            .ttMain.SetToolTip(.clbColumns, sa(strLanguage_BoostedDecisionTrees_Tips(18), vbCrLf))

            .ttMain.SetToolTip(.txtReportProgress, sa(strLanguage_BoostedDecisionTrees_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.chkreportProgress, sa(strLanguage_BoostedDecisionTrees_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.txtBlocksPerRead, sa(strLanguage_BoostedDecisionTrees_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.chkBlocksPerRead, sa(strLanguage_BoostedDecisionTrees_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.txtrowSelection, sa(strLanguage_BoostedDecisionTrees_Tips(21), vbCrLf))
            .ttMain.SetToolTip(.chkrowSelection, sa(strLanguage_BoostedDecisionTrees_Tips(21), vbCrLf))

            .ttMain.SetToolTip(.cbunbalancedSets, sa(strLanguage_BoostedDecisionTrees_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.chkunbalancedSets, sa(strLanguage_BoostedDecisionTrees_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.txtnumTrees, sa(strLanguage_BoostedDecisionTrees_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.chknumTrees, sa(strLanguage_BoostedDecisionTrees_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.txtnumLeaves, sa(strLanguage_BoostedDecisionTrees_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.chknumLeaves, sa(strLanguage_BoostedDecisionTrees_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.txtgainConfLevel, sa(strLanguage_BoostedDecisionTrees_Tips(25), vbCrLf))
            .ttMain.SetToolTip(.chkgainConfLevel, sa(strLanguage_BoostedDecisionTrees_Tips(25), vbCrLf))
            '/TIPS

        End With
    End Sub

    Public Sub EnsembleofDecisionTrees_Language(ByVal frm As frmEnsembleofDecisionTrees)
        With frm
            Dim strLanguage_EnsembleofDecisionTrees() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_EnsembleofDecisionTrees)
            Dim strLanguage_EnsembleofDecisionTrees_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_EnsembleofDecisionTrees_Tips)
            On Error Resume Next

            .Text = strLanguage_EnsembleofDecisionTrees(1) '
            .lblLoading.Text = strLanguage_EnsembleofDecisionTrees(2) '
            .lblColumnsLoading.Text = strLanguage_EnsembleofDecisionTrees(2) '
            .tpGeneralOptions.Text = strLanguage_EnsembleofDecisionTrees(3) '
            .tpAlgorithmOptions.Text = strLanguage_EnsembleofDecisionTrees(4) '
            .gbOptions.Text = strLanguage_EnsembleofDecisionTrees(5) '
            .chkUseExistingModel.Text = strLanguage_EnsembleofDecisionTrees(6) '
            .chkMakePredictions.Text = strLanguage_EnsembleofDecisionTrees(7) '
            .chkSavePredictionModel.Text = strLanguage_EnsembleofDecisionTrees(8) '
            .lblSavePath.Text = strLanguage_EnsembleofDecisionTrees(9) '
            .chkShowDataSummary.Text = strLanguage_EnsembleofDecisionTrees(10) '
            .chkShowVariableInfo.Text = strLanguage_EnsembleofDecisionTrees(11) '
            .btnSelectAll.Text = strLanguage_EnsembleofDecisionTrees(12) '
            .chkStatisticsMode.Text = strLanguage_EnsembleofDecisionTrees(13) '
            .chkShowStatistics.Text = strLanguage_EnsembleofDecisionTrees(14) '
            .chkShowROCCurve.Text = strLanguage_EnsembleofDecisionTrees(15) '
            .chkOpenGraphDirectory.Text = strLanguage_EnsembleofDecisionTrees(16) '
            .lblRoundAt.Text = strLanguage_EnsembleofDecisionTrees(17) '
            .lblNGrams.Text = strLanguage_EnsembleofDecisionTrees(18) '
            If .lblCombinationsCount.Text = "1 Combination" Then .lblCombinationsCount.Text = "1" & strLanguage_EnsembleofDecisionTrees(19) '
            .chkColumnsCombinations.Text = strLanguage_EnsembleofDecisionTrees(21) '
            .chkUpToNGramsN.Text = strLanguage_EnsembleofDecisionTrees(22) '
            .lblInProgress.Text = strLanguage_EnsembleofDecisionTrees(23) '
            .btnRunModel.Text = strLanguage_EnsembleofDecisionTrees(24) '
            .btnSelectAllColumns.Text = strLanguage_EnsembleofDecisionTrees(29) '
            .gbSettings.Text = strLanguage_EnsembleofDecisionTrees(48) 'Settings:


            'TIPS
            .ttMain.SetToolTip(.chkUseExistingModel, sa(strLanguage_EnsembleofDecisionTrees_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.chkMakePredictions, sa(strLanguage_EnsembleofDecisionTrees_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.chkSavePredictionModel, sa(strLanguage_EnsembleofDecisionTrees_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.lblSavePath, sa(strLanguage_EnsembleofDecisionTrees_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.txtSavePath, sa(strLanguage_EnsembleofDecisionTrees_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.chkShowDataSummary, sa(strLanguage_EnsembleofDecisionTrees_Tips(5), vbCrLf))
            .ttMain.SetToolTip(.chkShowVariableInfo, sa(strLanguage_EnsembleofDecisionTrees_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_EnsembleofDecisionTrees_Tips(7), vbCrLf))
            .ttMain.SetToolTip(.chkStatisticsMode, sa(strLanguage_EnsembleofDecisionTrees_Tips(8), vbCrLf))
            .ttMain.SetToolTip(.chkShowStatistics, sa(strLanguage_EnsembleofDecisionTrees_Tips(9), vbCrLf))
            .ttMain.SetToolTip(.chkShowROCCurve, sa(strLanguage_EnsembleofDecisionTrees_Tips(10), vbCrLf))
            .ttMain.SetToolTip(.chkOpenGraphDirectory, sa(strLanguage_EnsembleofDecisionTrees_Tips(11), vbCrLf))
            .ttMain.SetToolTip(.lblRoundAt, sa(strLanguage_EnsembleofDecisionTrees_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.txtRoundAt, sa(strLanguage_EnsembleofDecisionTrees_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAllColumns, sa(strLanguage_EnsembleofDecisionTrees_Tips(13), vbCrLf))
            .ttMain.SetToolTip(.lblNGrams, sa(strLanguage_EnsembleofDecisionTrees_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.txtNGrams, sa(strLanguage_EnsembleofDecisionTrees_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.chkColumnsCombinations, sa(strLanguage_EnsembleofDecisionTrees_Tips(15), vbCrLf))
            .ttMain.SetToolTip(.chkUpToNGramsN, sa(strLanguage_EnsembleofDecisionTrees_Tips(16), vbCrLf))
            .ttMain.SetToolTip(.btnRunModel, sa(strLanguage_EnsembleofDecisionTrees_Tips(17), vbCrLf))
            .ttMain.SetToolTip(.clbColumns, sa(strLanguage_EnsembleofDecisionTrees_Tips(18), vbCrLf))

            .ttMain.SetToolTip(.txtReportProgress, sa(strLanguage_EnsembleofDecisionTrees_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.chkreportProgress, sa(strLanguage_EnsembleofDecisionTrees_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.txtBlocksPerRead, sa(strLanguage_EnsembleofDecisionTrees_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.chkBlocksPerRead, sa(strLanguage_EnsembleofDecisionTrees_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.txtrowSelection, sa(strLanguage_EnsembleofDecisionTrees_Tips(21), vbCrLf))
            .ttMain.SetToolTip(.chkrowSelection, sa(strLanguage_EnsembleofDecisionTrees_Tips(21), vbCrLf))

            .ttMain.SetToolTip(.cbtype, sa(strLanguage_EnsembleofDecisionTrees_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.chktype, sa(strLanguage_EnsembleofDecisionTrees_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.txtnumTrees, sa(strLanguage_EnsembleofDecisionTrees_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.chknumTrees, sa(strLanguage_EnsembleofDecisionTrees_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.txtnumLeaves, sa(strLanguage_EnsembleofDecisionTrees_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.chknumLeaves, sa(strLanguage_EnsembleofDecisionTrees_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.txtgainConfLevel, sa(strLanguage_EnsembleofDecisionTrees_Tips(25), vbCrLf))
            .ttMain.SetToolTip(.chkgainConfLevel, sa(strLanguage_EnsembleofDecisionTrees_Tips(25), vbCrLf))

            '/TIPS

        End With
    End Sub

    Public Sub NeuralNetworks_Language(ByVal frm As frmNeuralNetworks)
        With frm
            Dim strLanguage_NeuralNetworks() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_NeuralNetworks)
            Dim strLanguage_NeuralNetworks_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_NeuralNetworks_Tips)
            On Error Resume Next

            .Text = strLanguage_NeuralNetworks(1) '
            .lblLoading.Text = strLanguage_NeuralNetworks(2) '
            .lblColumnsLoading.Text = strLanguage_NeuralNetworks(2) '
            .tpGeneralOptions.Text = strLanguage_NeuralNetworks(3) '
            .tpAlgorithmOptions.Text = strLanguage_NeuralNetworks(4) '
            .gbOptions.Text = strLanguage_NeuralNetworks(5) '
            .chkUseExistingModel.Text = strLanguage_NeuralNetworks(6) '
            .chkMakePredictions.Text = strLanguage_NeuralNetworks(7) '
            .chkSavePredictionModel.Text = strLanguage_NeuralNetworks(8) '
            .lblSavePath.Text = strLanguage_NeuralNetworks(9) '
            .chkShowDataSummary.Text = strLanguage_NeuralNetworks(10) '
            .chkShowVariableInfo.Text = strLanguage_NeuralNetworks(11) '
            .btnSelectAll.Text = strLanguage_NeuralNetworks(12) '
            .chkStatisticsMode.Text = strLanguage_NeuralNetworks(13) '
            .chkShowStatistics.Text = strLanguage_NeuralNetworks(14) '
            .chkShowROCCurve.Text = strLanguage_NeuralNetworks(15) '
            .chkOpenGraphDirectory.Text = strLanguage_NeuralNetworks(16) '
            .lblRoundAt.Text = strLanguage_NeuralNetworks(17) '
            .lblNGrams.Text = strLanguage_NeuralNetworks(18) '
            If .lblCombinationsCount.Text = "1 Combination" Then .lblCombinationsCount.Text = "1" & strLanguage_NeuralNetworks(19) '
            .chkColumnsCombinations.Text = strLanguage_NeuralNetworks(21) '
            .chkUpToNGramsN.Text = strLanguage_NeuralNetworks(22) '
            .lblInProgress.Text = strLanguage_NeuralNetworks(23) '
            .btnRunModel.Text = strLanguage_NeuralNetworks(24) '
            .btnSelectAllColumns.Text = strLanguage_NeuralNetworks(29) '
            .gbSettings.Text = strLanguage_NeuralNetworks(48) 'Settings:


            'TIPS
            .ttMain.SetToolTip(.chkUseExistingModel, sa(strLanguage_NeuralNetworks_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.chkMakePredictions, sa(strLanguage_NeuralNetworks_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.chkSavePredictionModel, sa(strLanguage_NeuralNetworks_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.lblSavePath, sa(strLanguage_NeuralNetworks_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.txtSavePath, sa(strLanguage_NeuralNetworks_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.chkShowDataSummary, sa(strLanguage_NeuralNetworks_Tips(5), vbCrLf))
            .ttMain.SetToolTip(.chkShowVariableInfo, sa(strLanguage_NeuralNetworks_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_NeuralNetworks_Tips(7), vbCrLf))
            .ttMain.SetToolTip(.chkStatisticsMode, sa(strLanguage_NeuralNetworks_Tips(8), vbCrLf))
            .ttMain.SetToolTip(.chkShowStatistics, sa(strLanguage_NeuralNetworks_Tips(9), vbCrLf))
            .ttMain.SetToolTip(.chkShowROCCurve, sa(strLanguage_NeuralNetworks_Tips(10), vbCrLf))
            .ttMain.SetToolTip(.chkOpenGraphDirectory, sa(strLanguage_NeuralNetworks_Tips(11), vbCrLf))
            .ttMain.SetToolTip(.lblRoundAt, sa(strLanguage_NeuralNetworks_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.txtRoundAt, sa(strLanguage_NeuralNetworks_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAllColumns, sa(strLanguage_NeuralNetworks_Tips(13), vbCrLf))
            .ttMain.SetToolTip(.lblNGrams, sa(strLanguage_NeuralNetworks_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.txtNGrams, sa(strLanguage_NeuralNetworks_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.chkColumnsCombinations, sa(strLanguage_NeuralNetworks_Tips(15), vbCrLf))
            .ttMain.SetToolTip(.chkUpToNGramsN, sa(strLanguage_NeuralNetworks_Tips(16), vbCrLf))
            .ttMain.SetToolTip(.btnRunModel, sa(strLanguage_NeuralNetworks_Tips(17), vbCrLf))
            .ttMain.SetToolTip(.clbColumns, sa(strLanguage_NeuralNetworks_Tips(18), vbCrLf))

            .ttMain.SetToolTip(.txtReportProgress, sa(strLanguage_NeuralNetworks_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.chkreportProgress, sa(strLanguage_NeuralNetworks_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.txtBlocksPerRead, sa(strLanguage_NeuralNetworks_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.chkBlocksPerRead, sa(strLanguage_NeuralNetworks_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.txtrowSelection, sa(strLanguage_NeuralNetworks_Tips(21), vbCrLf))
            .ttMain.SetToolTip(.chkrowSelection, sa(strLanguage_NeuralNetworks_Tips(21), vbCrLf))

            .ttMain.SetToolTip(.txtnumHiddenNodes, sa(strLanguage_NeuralNetworks_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.chknumHiddenNodes, sa(strLanguage_NeuralNetworks_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.txtnumIterations, sa(strLanguage_NeuralNetworks_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.chknumIterations, sa(strLanguage_NeuralNetworks_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.cbacceleration, sa(strLanguage_NeuralNetworks_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.chkacceleration, sa(strLanguage_NeuralNetworks_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.cbnormalize, sa(strLanguage_NeuralNetworks_Tips(25), vbCrLf))
            .ttMain.SetToolTip(.chknormalize, sa(strLanguage_NeuralNetworks_Tips(25), vbCrLf))
            .ttMain.SetToolTip(.txtminiBatchSize, sa(strLanguage_NeuralNetworks_Tips(26), vbCrLf))
            .ttMain.SetToolTip(.chkminiBatchSize, sa(strLanguage_NeuralNetworks_Tips(26), vbCrLf))
            '/TIPS

        End With
    End Sub

    Public Sub FastLogisticRegression_Language(ByVal frm As frmFastLogisticRegression)
        With frm
            Dim strLanguage_FastLogisticRegression() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & ".lng", .strLanguage_FastLogisticRegression)
            Dim strLanguage_FastLogisticRegression_Tips() As String = ReadFile(strLanguageFolders & CurrentLanguage & "\" & .Name & "_Tips.lng", .strLanguage_FastLogisticRegression_Tips)
            On Error Resume Next

            .Text = strLanguage_FastLogisticRegression(1) '
            .lblLoading.Text = strLanguage_FastLogisticRegression(2) '
            .lblColumnsLoading.Text = strLanguage_FastLogisticRegression(2) '
            .tpGeneralOptions.Text = strLanguage_FastLogisticRegression(3) '
            .tpAlgorithmOptions.Text = strLanguage_FastLogisticRegression(4) '
            .gbOptions.Text = strLanguage_FastLogisticRegression(5) '
            .chkUseExistingModel.Text = strLanguage_FastLogisticRegression(6) '
            .chkMakePredictions.Text = strLanguage_FastLogisticRegression(7) '
            .chkSavePredictionModel.Text = strLanguage_FastLogisticRegression(8) '
            .lblSavePath.Text = strLanguage_FastLogisticRegression(9) '
            .chkShowDataSummary.Text = strLanguage_FastLogisticRegression(10) '
            .chkShowVariableInfo.Text = strLanguage_FastLogisticRegression(11) '
            .btnSelectAll.Text = strLanguage_FastLogisticRegression(12) '
            .chkStatisticsMode.Text = strLanguage_FastLogisticRegression(13) '
            .chkShowStatistics.Text = strLanguage_FastLogisticRegression(14) '
            .chkShowROCCurve.Text = strLanguage_FastLogisticRegression(15) '
            .chkOpenGraphDirectory.Text = strLanguage_FastLogisticRegression(16) '
            .lblRoundAt.Text = strLanguage_FastLogisticRegression(17) '
            .lblNGrams.Text = strLanguage_FastLogisticRegression(18) '
            If .lblCombinationsCount.Text = "1 Combination" Then .lblCombinationsCount.Text = "1" & strLanguage_FastLogisticRegression(19) '
            .chkColumnsCombinations.Text = strLanguage_FastLogisticRegression(21) '
            .chkUpToNGramsN.Text = strLanguage_FastLogisticRegression(22) '
            .lblInProgress.Text = strLanguage_FastLogisticRegression(23) '
            .btnRunModel.Text = strLanguage_FastLogisticRegression(24) '
            .btnSelectAllColumns.Text = strLanguage_FastLogisticRegression(29) '
            .gbSettings.Text = strLanguage_FastLogisticRegression(48) 'Settings:


            'TIPS
            .ttMain.SetToolTip(.chkUseExistingModel, sa(strLanguage_FastLogisticRegression_Tips(1), vbCrLf))
            .ttMain.SetToolTip(.chkMakePredictions, sa(strLanguage_FastLogisticRegression_Tips(2), vbCrLf))
            .ttMain.SetToolTip(.chkSavePredictionModel, sa(strLanguage_FastLogisticRegression_Tips(3), vbCrLf))
            .ttMain.SetToolTip(.lblSavePath, sa(strLanguage_FastLogisticRegression_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.txtSavePath, sa(strLanguage_FastLogisticRegression_Tips(4), vbCrLf))
            .ttMain.SetToolTip(.chkShowDataSummary, sa(strLanguage_FastLogisticRegression_Tips(5), vbCrLf))
            .ttMain.SetToolTip(.chkShowVariableInfo, sa(strLanguage_FastLogisticRegression_Tips(6), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAll, sa(strLanguage_FastLogisticRegression_Tips(7), vbCrLf))
            .ttMain.SetToolTip(.chkStatisticsMode, sa(strLanguage_FastLogisticRegression_Tips(8), vbCrLf))
            .ttMain.SetToolTip(.chkShowStatistics, sa(strLanguage_FastLogisticRegression_Tips(9), vbCrLf))
            .ttMain.SetToolTip(.chkShowROCCurve, sa(strLanguage_FastLogisticRegression_Tips(10), vbCrLf))
            .ttMain.SetToolTip(.chkOpenGraphDirectory, sa(strLanguage_FastLogisticRegression_Tips(11), vbCrLf))
            .ttMain.SetToolTip(.lblRoundAt, sa(strLanguage_FastLogisticRegression_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.txtRoundAt, sa(strLanguage_FastLogisticRegression_Tips(12), vbCrLf))
            .ttMain.SetToolTip(.btnSelectAllColumns, sa(strLanguage_FastLogisticRegression_Tips(13), vbCrLf))
            .ttMain.SetToolTip(.lblNGrams, sa(strLanguage_FastLogisticRegression_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.txtNGrams, sa(strLanguage_FastLogisticRegression_Tips(14), vbCrLf))
            .ttMain.SetToolTip(.chkColumnsCombinations, sa(strLanguage_FastLogisticRegression_Tips(15), vbCrLf))
            .ttMain.SetToolTip(.chkUpToNGramsN, sa(strLanguage_FastLogisticRegression_Tips(16), vbCrLf))
            .ttMain.SetToolTip(.btnRunModel, sa(strLanguage_FastLogisticRegression_Tips(17), vbCrLf))
            .ttMain.SetToolTip(.clbColumns, sa(strLanguage_FastLogisticRegression_Tips(18), vbCrLf))

            .ttMain.SetToolTip(.txtReportProgress, sa(strLanguage_FastLogisticRegression_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.chkreportProgress, sa(strLanguage_FastLogisticRegression_Tips(19), vbCrLf))
            .ttMain.SetToolTip(.txtBlocksPerRead, sa(strLanguage_FastLogisticRegression_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.chkBlocksPerRead, sa(strLanguage_FastLogisticRegression_Tips(20), vbCrLf))
            .ttMain.SetToolTip(.txtrowSelection, sa(strLanguage_FastLogisticRegression_Tips(21), vbCrLf))
            .ttMain.SetToolTip(.chkrowSelection, sa(strLanguage_FastLogisticRegression_Tips(21), vbCrLf))

            .ttMain.SetToolTip(.cbnormalize, sa(strLanguage_FastLogisticRegression_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.chknormalize, sa(strLanguage_FastLogisticRegression_Tips(22), vbCrLf))
            .ttMain.SetToolTip(.txtsgdInitTol, sa(strLanguage_FastLogisticRegression_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.chksgdInitTol, sa(strLanguage_FastLogisticRegression_Tips(23), vbCrLf))
            .ttMain.SetToolTip(.txtl2Weight, sa(strLanguage_FastLogisticRegression_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.chkl2Weight, sa(strLanguage_FastLogisticRegression_Tips(24), vbCrLf))
            .ttMain.SetToolTip(.txtl1Weight, sa(strLanguage_FastLogisticRegression_Tips(25), vbCrLf))
            .ttMain.SetToolTip(.chkl1Weight, sa(strLanguage_FastLogisticRegression_Tips(25), vbCrLf))
            .ttMain.SetToolTip(.txtoptTol, sa(strLanguage_FastLogisticRegression_Tips(26), vbCrLf))
            .ttMain.SetToolTip(.chkoptTol, sa(strLanguage_FastLogisticRegression_Tips(26), vbCrLf))
            .ttMain.SetToolTip(.txtmemorySize, sa(strLanguage_FastLogisticRegression_Tips(27), vbCrLf))
            .ttMain.SetToolTip(.chkmemorySize, sa(strLanguage_FastLogisticRegression_Tips(27), vbCrLf))
            .ttMain.SetToolTip(.txtinitWtsScale, sa(strLanguage_FastLogisticRegression_Tips(28), vbCrLf))
            .ttMain.SetToolTip(.chkinitWtsScale, sa(strLanguage_FastLogisticRegression_Tips(28), vbCrLf))
            .ttMain.SetToolTip(.txtmaxIterations, sa(strLanguage_FastLogisticRegression_Tips(29), vbCrLf))
            .ttMain.SetToolTip(.chkmaxIterations, sa(strLanguage_FastLogisticRegression_Tips(29), vbCrLf))
            '/TIPS

        End With
    End Sub


End Module
