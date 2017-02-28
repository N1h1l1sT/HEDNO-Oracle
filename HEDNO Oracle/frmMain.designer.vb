<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.mniFileMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mniDatabaseMaintenance = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniSuggestOrComplain = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniOpenSettingsFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mniExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniPreProcessing = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniCreateGeoColumns = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniCreateNeededSQLViews = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniGeoLocate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniGeoLocationStatus = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniExportListofProblematicAddresses = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniResetInvalidGeolocationEntries = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniPreProcessTheData = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniClustering = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniClusteringStep0 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniClusteringStep1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniClassification = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniFormTrainAndTestSets = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniLogisticRegression = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniDecisionTrees = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniNaiveBayes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniRandomForest = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniStochasticGradientBoosting = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniStochasticDualCoordinateAscent = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniBoostedDecisionTrees = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniEnsambleOfDecisionTrees = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniNeuralNetworks = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniFastLogisticRegression = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniDirectoriesMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniProgramDocuments = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mniProgramsDir = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniDatabaseDir = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniExtrasDir = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniLanguageDir = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniSettingsDir = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniSkinDir = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniLinksMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniProgramWebsite = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniProgrammerWebsite = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniCompanySite = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniHelpMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniShowPresentation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniShowWelcomeScreen = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mniLicenses = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniEULA = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniChangeLog = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mniHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniCheckForUpdates = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mniCredits = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblHelp = New System.Windows.Forms.Label()
        Me.lblState = New System.Windows.Forms.Label()
        Me.lblCompany = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lblProgUpdated = New System.Windows.Forms.Label()
        Me.lblProgUpgraded = New System.Windows.Forms.Label()
        Me.gbCommands = New System.Windows.Forms.GroupBox()
        Me.pnlCommands = New System.Windows.Forms.Panel()
        Me.lblMathMode = New System.Windows.Forms.Label()
        Me.lblUnknownCmd = New System.Windows.Forms.Label()
        Me.cmdSettings = New System.Windows.Forms.Button()
        Me.cmdGo = New System.Windows.Forms.Button()
        Me.cmdHelp = New System.Windows.Forms.Button()
        Me.txtCommands = New System.Windows.Forms.TextBox()
        Me.tmrUnknownCmd = New System.Windows.Forms.Timer(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.gbFunctions = New System.Windows.Forms.GroupBox()
        Me.pnlFunctions = New System.Windows.Forms.Panel()
        Me.lblFuncInProgress = New System.Windows.Forms.Label()
        Me.pbGeneralProgress = New System.Windows.Forms.ProgressBar()
        Me.tmrMinimizationDelay = New System.Windows.Forms.Timer(Me.components)
        Me.TrayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.TraySetting = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.trayShow = New System.Windows.Forms.ToolStripMenuItem()
        Me.trayHide = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripSeparator()
        Me.traySettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripSeparator()
        Me.trayWebsite = New System.Windows.Forms.ToolStripMenuItem()
        Me.trayCredits = New System.Windows.Forms.ToolStripMenuItem()
        Me.trayAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripSeparator()
        Me.trayClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.tltMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.fswSettings = New System.IO.FileSystemWatcher()
        Me.tmrHideReEnable = New System.Windows.Forms.Timer(Me.components)
        Me.tmrUpdatePB = New System.Windows.Forms.Timer(Me.components)
        Me.tmrFunctInProgress = New System.Windows.Forms.Timer(Me.components)
        Me.btnGeoLocate = New System.Windows.Forms.Button()
        Me.mnuMain.SuspendLayout()
        Me.gbCommands.SuspendLayout()
        Me.pnlCommands.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.gbFunctions.SuspendLayout()
        Me.pnlFunctions.SuspendLayout()
        Me.TraySetting.SuspendLayout()
        CType(Me.fswSettings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'mnuMain
        '
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniFileMenu, Me.mniPreProcessing, Me.mniClustering, Me.mniClassification, Me.mniDirectoriesMenu, Me.mniLinksMenu, Me.mniHelpMenu})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(778, 24)
        Me.mnuMain.TabIndex = 4
        Me.mnuMain.Text = "Main Menu"
        '
        'mniFileMenu
        '
        Me.mniFileMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniSettings, Me.mnuSeparator2, Me.mniDatabaseMaintenance, Me.mniSuggestOrComplain, Me.mniOpenSettingsFile, Me.ToolStripSeparator1, Me.mniExit})
        Me.mniFileMenu.Name = "mniFileMenu"
        Me.mniFileMenu.Size = New System.Drawing.Size(36, 20)
        Me.mniFileMenu.Text = "&File"
        '
        'mniSettings
        '
        Me.mniSettings.Name = "mniSettings"
        Me.mniSettings.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mniSettings.Size = New System.Drawing.Size(244, 22)
        Me.mniSettings.Text = "&Settings"
        '
        'mnuSeparator2
        '
        Me.mnuSeparator2.Name = "mnuSeparator2"
        Me.mnuSeparator2.Size = New System.Drawing.Size(241, 6)
        '
        'mniDatabaseMaintenance
        '
        Me.mniDatabaseMaintenance.Name = "mniDatabaseMaintenance"
        Me.mniDatabaseMaintenance.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.mniDatabaseMaintenance.Size = New System.Drawing.Size(244, 22)
        Me.mniDatabaseMaintenance.Text = "&Database Maintenance"
        '
        'mniSuggestOrComplain
        '
        Me.mniSuggestOrComplain.Name = "mniSuggestOrComplain"
        Me.mniSuggestOrComplain.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G), System.Windows.Forms.Keys)
        Me.mniSuggestOrComplain.Size = New System.Drawing.Size(244, 22)
        Me.mniSuggestOrComplain.Text = "&Suggest or Complain"
        '
        'mniOpenSettingsFile
        '
        Me.mniOpenSettingsFile.Name = "mniOpenSettingsFile"
        Me.mniOpenSettingsFile.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mniOpenSettingsFile.Size = New System.Drawing.Size(244, 22)
        Me.mniOpenSettingsFile.Text = "&Open Settings File"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(241, 6)
        '
        'mniExit
        '
        Me.mniExit.Name = "mniExit"
        Me.mniExit.Size = New System.Drawing.Size(244, 22)
        Me.mniExit.Text = "E&xit"
        '
        'mniPreProcessing
        '
        Me.mniPreProcessing.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniCreateGeoColumns, Me.mniCreateNeededSQLViews, Me.mniGeoLocate, Me.mniGeoLocationStatus, Me.mniExportListofProblematicAddresses, Me.mniResetInvalidGeolocationEntries, Me.mniPreProcessTheData})
        Me.mniPreProcessing.Name = "mniPreProcessing"
        Me.mniPreProcessing.Size = New System.Drawing.Size(103, 20)
        Me.mniPreProcessing.Text = "Pre-Processing"
        '
        'mniCreateGeoColumns
        '
        Me.mniCreateGeoColumns.Name = "mniCreateGeoColumns"
        Me.mniCreateGeoColumns.Size = New System.Drawing.Size(273, 22)
        Me.mniCreateGeoColumns.Text = "Create Geolocation SQL Columns"
        Me.mniCreateGeoColumns.ToolTipText = "Needs: An active connection to the SQL Server and Main SQL Table" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Does: If they d" &
    "on't already exist, it creates the 2 SQL Columns needed for the process on the M" &
    "ain SQL Table (not the view!)"
        '
        'mniCreateNeededSQLViews
        '
        Me.mniCreateNeededSQLViews.Name = "mniCreateNeededSQLViews"
        Me.mniCreateNeededSQLViews.Size = New System.Drawing.Size(273, 22)
        Me.mniCreateNeededSQLViews.Text = "Create Needed SQL Views"
        Me.mniCreateNeededSQLViews.ToolTipText = "Opens the 'Create SQL View' form so that you may create, modify or delete needed " &
    "SQL Views"
        '
        'mniGeoLocate
        '
        Me.mniGeoLocate.Name = "mniGeoLocate"
        Me.mniGeoLocate.Size = New System.Drawing.Size(273, 22)
        Me.mniGeoLocate.Text = "&Geo-Locate"
        Me.mniGeoLocate.ToolTipText = resources.GetString("mniGeoLocate.ToolTipText")
        '
        'mniGeoLocationStatus
        '
        Me.mniGeoLocationStatus.Name = "mniGeoLocationStatus"
        Me.mniGeoLocationStatus.Size = New System.Drawing.Size(273, 22)
        Me.mniGeoLocationStatus.Text = "Geo-Location Status"
        Me.mniGeoLocationStatus.ToolTipText = resources.GetString("mniGeoLocationStatus.ToolTipText")
        '
        'mniExportListofProblematicAddresses
        '
        Me.mniExportListofProblematicAddresses.Name = "mniExportListofProblematicAddresses"
        Me.mniExportListofProblematicAddresses.Size = New System.Drawing.Size(273, 22)
        Me.mniExportListofProblematicAddresses.Text = "&Export List of Problematic Addresses"
        Me.mniExportListofProblematicAddresses.ToolTipText = resources.GetString("mniExportListofProblematicAddresses.ToolTipText")
        '
        'mniResetInvalidGeolocationEntries
        '
        Me.mniResetInvalidGeolocationEntries.Name = "mniResetInvalidGeolocationEntries"
        Me.mniResetInvalidGeolocationEntries.Size = New System.Drawing.Size(273, 22)
        Me.mniResetInvalidGeolocationEntries.Text = "Reset Invalid Geolocation Entries"
        Me.mniResetInvalidGeolocationEntries.ToolTipText = resources.GetString("mniResetInvalidGeolocationEntries.ToolTipText")
        '
        'mniPreProcessTheData
        '
        Me.mniPreProcessTheData.Name = "mniPreProcessTheData"
        Me.mniPreProcessTheData.Size = New System.Drawing.Size(273, 22)
        Me.mniPreProcessTheData.Text = "&Pre-Process The Data"
        Me.mniPreProcessTheData.ToolTipText = resources.GetString("mniPreProcessTheData.ToolTipText")
        '
        'mniClustering
        '
        Me.mniClustering.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniClusteringStep0, Me.mniClusteringStep1})
        Me.mniClustering.Name = "mniClustering"
        Me.mniClustering.Size = New System.Drawing.Size(74, 20)
        Me.mniClustering.Text = "Clustering"
        '
        'mniClusteringStep0
        '
        Me.mniClusteringStep0.Name = "mniClusteringStep0"
        Me.mniClusteringStep0.Size = New System.Drawing.Size(274, 22)
        Me.mniClusteringStep0.Text = "Step 0: Process Data"
        Me.mniClusteringStep0.ToolTipText = resources.GetString("mniClusteringStep0.ToolTipText")
        '
        'mniClusteringStep1
        '
        Me.mniClusteringStep1.Name = "mniClusteringStep1"
        Me.mniClusteringStep1.Size = New System.Drawing.Size(274, 22)
        Me.mniClusteringStep1.Text = "Step 1: Apply Unsupervised Learning"
        Me.mniClusteringStep1.ToolTipText = resources.GetString("mniClusteringStep1.ToolTipText")
        '
        'mniClassification
        '
        Me.mniClassification.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniFormTrainAndTestSets, Me.mniLogisticRegression, Me.mniDecisionTrees, Me.mniNaiveBayes, Me.mniRandomForest, Me.mniStochasticGradientBoosting, Me.mniStochasticDualCoordinateAscent, Me.mniBoostedDecisionTrees, Me.mniEnsambleOfDecisionTrees, Me.mniNeuralNetworks, Me.mniFastLogisticRegression})
        Me.mniClassification.Name = "mniClassification"
        Me.mniClassification.Size = New System.Drawing.Size(88, 20)
        Me.mniClassification.Text = "Classification"
        '
        'mniFormTrainAndTestSets
        '
        Me.mniFormTrainAndTestSets.Name = "mniFormTrainAndTestSets"
        Me.mniFormTrainAndTestSets.Size = New System.Drawing.Size(277, 22)
        Me.mniFormTrainAndTestSets.Text = "&Form Train and Test sets"
        Me.mniFormTrainAndTestSets.ToolTipText = resources.GetString("mniFormTrainAndTestSets.ToolTipText")
        '
        'mniLogisticRegression
        '
        Me.mniLogisticRegression.Name = "mniLogisticRegression"
        Me.mniLogisticRegression.Size = New System.Drawing.Size(277, 22)
        Me.mniLogisticRegression.Text = "&1) Logistic Regression"
        Me.mniLogisticRegression.ToolTipText = resources.GetString("mniLogisticRegression.ToolTipText")
        '
        'mniDecisionTrees
        '
        Me.mniDecisionTrees.Name = "mniDecisionTrees"
        Me.mniDecisionTrees.Size = New System.Drawing.Size(277, 22)
        Me.mniDecisionTrees.Text = "&2) Decision Trees"
        Me.mniDecisionTrees.ToolTipText = resources.GetString("mniDecisionTrees.ToolTipText")
        '
        'mniNaiveBayes
        '
        Me.mniNaiveBayes.Name = "mniNaiveBayes"
        Me.mniNaiveBayes.Size = New System.Drawing.Size(277, 22)
        Me.mniNaiveBayes.Text = "&3) Naive Bayes"
        Me.mniNaiveBayes.ToolTipText = resources.GetString("mniNaiveBayes.ToolTipText")
        '
        'mniRandomForest
        '
        Me.mniRandomForest.Name = "mniRandomForest"
        Me.mniRandomForest.Size = New System.Drawing.Size(277, 22)
        Me.mniRandomForest.Text = "&4) Random Forest"
        Me.mniRandomForest.ToolTipText = resources.GetString("mniRandomForest.ToolTipText")
        '
        'mniStochasticGradientBoosting
        '
        Me.mniStochasticGradientBoosting.Name = "mniStochasticGradientBoosting"
        Me.mniStochasticGradientBoosting.Size = New System.Drawing.Size(277, 22)
        Me.mniStochasticGradientBoosting.Text = "&5) Stochastic Gradient Boosting"
        Me.mniStochasticGradientBoosting.ToolTipText = resources.GetString("mniStochasticGradientBoosting.ToolTipText")
        '
        'mniStochasticDualCoordinateAscent
        '
        Me.mniStochasticDualCoordinateAscent.Name = "mniStochasticDualCoordinateAscent"
        Me.mniStochasticDualCoordinateAscent.Size = New System.Drawing.Size(277, 22)
        Me.mniStochasticDualCoordinateAscent.Text = "&6) Stochastic Dual Coordinate Ascent"
        Me.mniStochasticDualCoordinateAscent.ToolTipText = resources.GetString("mniStochasticDualCoordinateAscent.ToolTipText")
        '
        'mniBoostedDecisionTrees
        '
        Me.mniBoostedDecisionTrees.Name = "mniBoostedDecisionTrees"
        Me.mniBoostedDecisionTrees.Size = New System.Drawing.Size(277, 22)
        Me.mniBoostedDecisionTrees.Text = "&7) Boosted Decision Trees"
        Me.mniBoostedDecisionTrees.ToolTipText = resources.GetString("mniBoostedDecisionTrees.ToolTipText")
        '
        'mniEnsambleOfDecisionTrees
        '
        Me.mniEnsambleOfDecisionTrees.Name = "mniEnsambleOfDecisionTrees"
        Me.mniEnsambleOfDecisionTrees.Size = New System.Drawing.Size(277, 22)
        Me.mniEnsambleOfDecisionTrees.Text = "&8) Ensamble of Decision Trees"
        Me.mniEnsambleOfDecisionTrees.ToolTipText = resources.GetString("mniEnsambleOfDecisionTrees.ToolTipText")
        '
        'mniNeuralNetworks
        '
        Me.mniNeuralNetworks.Name = "mniNeuralNetworks"
        Me.mniNeuralNetworks.Size = New System.Drawing.Size(277, 22)
        Me.mniNeuralNetworks.Text = "&9) Neural Networks"
        Me.mniNeuralNetworks.ToolTipText = resources.GetString("mniNeuralNetworks.ToolTipText")
        '
        'mniFastLogisticRegression
        '
        Me.mniFastLogisticRegression.Name = "mniFastLogisticRegression"
        Me.mniFastLogisticRegression.Size = New System.Drawing.Size(277, 22)
        Me.mniFastLogisticRegression.Text = "1&0) Fast Logistic Regression"
        Me.mniFastLogisticRegression.ToolTipText = resources.GetString("mniFastLogisticRegression.ToolTipText")
        '
        'mniDirectoriesMenu
        '
        Me.mniDirectoriesMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniProgramDocuments, Me.ToolStripMenuItem3, Me.mniProgramsDir, Me.mniDatabaseDir, Me.mniExtrasDir, Me.mniLanguageDir, Me.mniSettingsDir, Me.mniSkinDir})
        Me.mniDirectoriesMenu.Name = "mniDirectoriesMenu"
        Me.mniDirectoriesMenu.Size = New System.Drawing.Size(78, 20)
        Me.mniDirectoriesMenu.Text = "&Directories"
        '
        'mniProgramDocuments
        '
        Me.mniProgramDocuments.Name = "mniProgramDocuments"
        Me.mniProgramDocuments.Size = New System.Drawing.Size(198, 22)
        Me.mniProgramDocuments.Text = "Program's D&ocuments"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(195, 6)
        '
        'mniProgramsDir
        '
        Me.mniProgramsDir.Name = "mniProgramsDir"
        Me.mniProgramsDir.Size = New System.Drawing.Size(198, 22)
        Me.mniProgramsDir.Text = "Program's Directory"
        '
        'mniDatabaseDir
        '
        Me.mniDatabaseDir.Name = "mniDatabaseDir"
        Me.mniDatabaseDir.Size = New System.Drawing.Size(198, 22)
        Me.mniDatabaseDir.Text = "&Database"
        '
        'mniExtrasDir
        '
        Me.mniExtrasDir.Name = "mniExtrasDir"
        Me.mniExtrasDir.Size = New System.Drawing.Size(198, 22)
        Me.mniExtrasDir.Text = "&Extras"
        '
        'mniLanguageDir
        '
        Me.mniLanguageDir.Name = "mniLanguageDir"
        Me.mniLanguageDir.Size = New System.Drawing.Size(198, 22)
        Me.mniLanguageDir.Text = "&Language"
        '
        'mniSettingsDir
        '
        Me.mniSettingsDir.Name = "mniSettingsDir"
        Me.mniSettingsDir.Size = New System.Drawing.Size(198, 22)
        Me.mniSettingsDir.Text = "&Settings"
        '
        'mniSkinDir
        '
        Me.mniSkinDir.Name = "mniSkinDir"
        Me.mniSkinDir.Size = New System.Drawing.Size(198, 22)
        Me.mniSkinDir.Text = "S&kin"
        '
        'mniLinksMenu
        '
        Me.mniLinksMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniProgramWebsite, Me.mniProgrammerWebsite, Me.mniCompanySite})
        Me.mniLinksMenu.Name = "mniLinksMenu"
        Me.mniLinksMenu.Size = New System.Drawing.Size(46, 20)
        Me.mniLinksMenu.Text = "&Links"
        Me.mniLinksMenu.Visible = False
        '
        'mniProgramWebsite
        '
        Me.mniProgramWebsite.Name = "mniProgramWebsite"
        Me.mniProgramWebsite.Size = New System.Drawing.Size(204, 22)
        Me.mniProgramWebsite.Text = "&Programme's Website"
        Me.mniProgramWebsite.Visible = False
        '
        'mniProgrammerWebsite
        '
        Me.mniProgrammerWebsite.Name = "mniProgrammerWebsite"
        Me.mniProgrammerWebsite.Size = New System.Drawing.Size(204, 22)
        Me.mniProgrammerWebsite.Text = "Programmer's Website"
        Me.mniProgrammerWebsite.Visible = False
        '
        'mniCompanySite
        '
        Me.mniCompanySite.Name = "mniCompanySite"
        Me.mniCompanySite.Size = New System.Drawing.Size(204, 22)
        Me.mniCompanySite.Text = "Company's Site"
        Me.mniCompanySite.Visible = False
        '
        'mniHelpMenu
        '
        Me.mniHelpMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniShowPresentation, Me.mniShowWelcomeScreen, Me.ToolStripMenuItem2, Me.mniLicenses, Me.mniEULA, Me.mniChangeLog, Me.mnuSeparator1, Me.mniHelp, Me.mniCheckForUpdates, Me.ToolStripMenuItem1, Me.mniCredits, Me.mniAbout})
        Me.mniHelpMenu.Name = "mniHelpMenu"
        Me.mniHelpMenu.Size = New System.Drawing.Size(44, 20)
        Me.mniHelpMenu.Text = "&Help"
        '
        'mniShowPresentation
        '
        Me.mniShowPresentation.Name = "mniShowPresentation"
        Me.mniShowPresentation.Size = New System.Drawing.Size(236, 22)
        Me.mniShowPresentation.Text = "&Show Presentation Again"
        '
        'mniShowWelcomeScreen
        '
        Me.mniShowWelcomeScreen.Name = "mniShowWelcomeScreen"
        Me.mniShowWelcomeScreen.Size = New System.Drawing.Size(236, 22)
        Me.mniShowWelcomeScreen.Text = "Show &Welcome Screen Again"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(233, 6)
        '
        'mniLicenses
        '
        Me.mniLicenses.Name = "mniLicenses"
        Me.mniLicenses.Size = New System.Drawing.Size(236, 22)
        Me.mniLicenses.Text = "L&icenses"
        '
        'mniEULA
        '
        Me.mniEULA.Name = "mniEULA"
        Me.mniEULA.Size = New System.Drawing.Size(236, 22)
        Me.mniEULA.Text = "&EULA"
        '
        'mniChangeLog
        '
        Me.mniChangeLog.Name = "mniChangeLog"
        Me.mniChangeLog.Size = New System.Drawing.Size(236, 22)
        Me.mniChangeLog.Text = "&Changelog"
        '
        'mnuSeparator1
        '
        Me.mnuSeparator1.Name = "mnuSeparator1"
        Me.mnuSeparator1.Size = New System.Drawing.Size(233, 6)
        '
        'mniHelp
        '
        Me.mniHelp.Name = "mniHelp"
        Me.mniHelp.Size = New System.Drawing.Size(236, 22)
        Me.mniHelp.Text = "&Help"
        '
        'mniCheckForUpdates
        '
        Me.mniCheckForUpdates.Name = "mniCheckForUpdates"
        Me.mniCheckForUpdates.Size = New System.Drawing.Size(236, 22)
        Me.mniCheckForUpdates.Text = "Check for &Updates"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(233, 6)
        '
        'mniCredits
        '
        Me.mniCredits.Name = "mniCredits"
        Me.mniCredits.Size = New System.Drawing.Size(236, 22)
        Me.mniCredits.Text = "&Credits"
        Me.mniCredits.Visible = False
        '
        'mniAbout
        '
        Me.mniAbout.Name = "mniAbout"
        Me.mniAbout.Size = New System.Drawing.Size(236, 22)
        Me.mniAbout.Text = "&About"
        '
        'lblHelp
        '
        Me.lblHelp.AutoSize = True
        Me.lblHelp.BackColor = System.Drawing.Color.Black
        Me.lblHelp.ForeColor = System.Drawing.Color.Cyan
        Me.lblHelp.Location = New System.Drawing.Point(12, 21)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(23, 13)
        Me.lblHelp.TabIndex = 5
        Me.lblHelp.Text = "null"
        Me.lblHelp.Visible = False
        '
        'lblState
        '
        Me.lblState.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblState.AutoSize = True
        Me.lblState.Location = New System.Drawing.Point(20, 411)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(23, 13)
        Me.lblState.TabIndex = 2
        Me.lblState.Text = "null"
        Me.lblState.Visible = False
        '
        'lblCompany
        '
        Me.lblCompany.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCompany.AutoSize = True
        Me.lblCompany.BackColor = System.Drawing.Color.Transparent
        Me.lblCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblCompany.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompany.Location = New System.Drawing.Point(17, 424)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(78, 13)
        Me.lblCompany.TabIndex = 3
        Me.lblCompany.Text = "{TradeMark}"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnExit.Location = New System.Drawing.Point(606, 416)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(150, 23)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "E&xit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lblProgUpdated
        '
        Me.lblProgUpdated.AutoSize = True
        Me.lblProgUpdated.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblProgUpdated.Location = New System.Drawing.Point(644, 7)
        Me.lblProgUpdated.Name = "lblProgUpdated"
        Me.lblProgUpdated.Size = New System.Drawing.Size(95, 13)
        Me.lblProgUpdated.TabIndex = 6
        Me.lblProgUpdated.Text = "<Awaiting Info>"
        Me.lblProgUpdated.Visible = False
        '
        'lblProgUpgraded
        '
        Me.lblProgUpgraded.AutoSize = True
        Me.lblProgUpgraded.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblProgUpgraded.Location = New System.Drawing.Point(644, 27)
        Me.lblProgUpgraded.Name = "lblProgUpgraded"
        Me.lblProgUpgraded.Size = New System.Drawing.Size(95, 13)
        Me.lblProgUpgraded.TabIndex = 7
        Me.lblProgUpgraded.Text = "<Awaiting Info>"
        Me.lblProgUpgraded.Visible = False
        '
        'gbCommands
        '
        Me.gbCommands.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbCommands.BackColor = System.Drawing.Color.Transparent
        Me.gbCommands.Controls.Add(Me.pnlCommands)
        Me.gbCommands.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.gbCommands.Location = New System.Drawing.Point(20, 315)
        Me.gbCommands.Name = "gbCommands"
        Me.gbCommands.Size = New System.Drawing.Size(736, 85)
        Me.gbCommands.TabIndex = 0
        Me.gbCommands.TabStop = False
        Me.gbCommands.Text = "Commands:"
        '
        'pnlCommands
        '
        Me.pnlCommands.AutoScroll = True
        Me.pnlCommands.AutoSize = True
        Me.pnlCommands.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlCommands.Controls.Add(Me.lblMathMode)
        Me.pnlCommands.Controls.Add(Me.lblUnknownCmd)
        Me.pnlCommands.Controls.Add(Me.cmdSettings)
        Me.pnlCommands.Controls.Add(Me.cmdGo)
        Me.pnlCommands.Controls.Add(Me.cmdHelp)
        Me.pnlCommands.Controls.Add(Me.txtCommands)
        Me.pnlCommands.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCommands.Location = New System.Drawing.Point(3, 16)
        Me.pnlCommands.Name = "pnlCommands"
        Me.pnlCommands.Size = New System.Drawing.Size(730, 66)
        Me.pnlCommands.TabIndex = 24
        '
        'lblMathMode
        '
        Me.lblMathMode.AutoSize = True
        Me.lblMathMode.BackColor = System.Drawing.Color.Black
        Me.lblMathMode.ForeColor = System.Drawing.Color.Yellow
        Me.lblMathMode.Location = New System.Drawing.Point(124, 53)
        Me.lblMathMode.Name = "lblMathMode"
        Me.lblMathMode.Size = New System.Drawing.Size(21, 13)
        Me.lblMathMode.TabIndex = 21
        Me.lblMathMode.Text = "??"
        Me.lblMathMode.Visible = False
        '
        'lblUnknownCmd
        '
        Me.lblUnknownCmd.AutoSize = True
        Me.lblUnknownCmd.BackColor = System.Drawing.Color.Black
        Me.lblUnknownCmd.ForeColor = System.Drawing.Color.Red
        Me.lblUnknownCmd.Location = New System.Drawing.Point(3, 53)
        Me.lblUnknownCmd.Name = "lblUnknownCmd"
        Me.lblUnknownCmd.Size = New System.Drawing.Size(118, 13)
        Me.lblUnknownCmd.TabIndex = 5
        Me.lblUnknownCmd.Text = "Unknown Command"
        Me.lblUnknownCmd.Visible = False
        '
        'cmdSettings
        '
        Me.cmdSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.cmdSettings.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSettings.Location = New System.Drawing.Point(6, 2)
        Me.cmdSettings.Name = "cmdSettings"
        Me.cmdSettings.Size = New System.Drawing.Size(115, 23)
        Me.cmdSettings.TabIndex = 2
        Me.cmdSettings.Text = "&Settings"
        Me.cmdSettings.UseVisualStyleBackColor = True
        '
        'cmdGo
        '
        Me.cmdGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdGo.BackColor = System.Drawing.Color.Transparent
        Me.cmdGo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGo.Location = New System.Drawing.Point(632, 29)
        Me.cmdGo.Name = "cmdGo"
        Me.cmdGo.Size = New System.Drawing.Size(75, 20)
        Me.cmdGo.TabIndex = 1
        Me.cmdGo.Text = "&Go"
        Me.cmdGo.UseVisualStyleBackColor = False
        '
        'cmdHelp
        '
        Me.cmdHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelp.Location = New System.Drawing.Point(127, 2)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.Size = New System.Drawing.Size(94, 23)
        Me.cmdHelp.TabIndex = 4
        Me.cmdHelp.Text = "&Help"
        Me.cmdHelp.UseVisualStyleBackColor = True
        '
        'txtCommands
        '
        Me.txtCommands.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCommands.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.txtCommands.ForeColor = System.Drawing.Color.Lime
        Me.txtCommands.Location = New System.Drawing.Point(6, 30)
        Me.txtCommands.Name = "txtCommands"
        Me.txtCommands.Size = New System.Drawing.Size(603, 20)
        Me.txtCommands.TabIndex = 0
        Me.txtCommands.Text = "You may type commands here. For a list of available commands, please type ""help()" &
    """"
        '
        'tmrUnknownCmd
        '
        Me.tmrUnknownCmd.Interval = 1
        '
        'pnlMain
        '
        Me.pnlMain.AutoScroll = True
        Me.pnlMain.AutoSize = True
        Me.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.btnExit)
        Me.pnlMain.Controls.Add(Me.lblCompany)
        Me.pnlMain.Controls.Add(Me.lblState)
        Me.pnlMain.Controls.Add(Me.gbCommands)
        Me.pnlMain.Controls.Add(Me.lblProgUpgraded)
        Me.pnlMain.Controls.Add(Me.lblProgUpdated)
        Me.pnlMain.Controls.Add(Me.gbFunctions)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 24)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(778, 444)
        Me.pnlMain.TabIndex = 21
        '
        'gbFunctions
        '
        Me.gbFunctions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbFunctions.BackColor = System.Drawing.Color.Transparent
        Me.gbFunctions.Controls.Add(Me.pnlFunctions)
        Me.gbFunctions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.gbFunctions.Location = New System.Drawing.Point(20, 15)
        Me.gbFunctions.Name = "gbFunctions"
        Me.gbFunctions.Size = New System.Drawing.Size(736, 294)
        Me.gbFunctions.TabIndex = 1
        Me.gbFunctions.TabStop = False
        Me.gbFunctions.Text = "Functions:"
        '
        'pnlFunctions
        '
        Me.pnlFunctions.AutoScroll = True
        Me.pnlFunctions.AutoSize = True
        Me.pnlFunctions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlFunctions.Controls.Add(Me.btnGeoLocate)
        Me.pnlFunctions.Controls.Add(Me.lblFuncInProgress)
        Me.pnlFunctions.Controls.Add(Me.pbGeneralProgress)
        Me.pnlFunctions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFunctions.Location = New System.Drawing.Point(3, 16)
        Me.pnlFunctions.Name = "pnlFunctions"
        Me.pnlFunctions.Size = New System.Drawing.Size(730, 275)
        Me.pnlFunctions.TabIndex = 20
        '
        'lblFuncInProgress
        '
        Me.lblFuncInProgress.AutoSize = True
        Me.lblFuncInProgress.BackColor = System.Drawing.Color.Black
        Me.lblFuncInProgress.ForeColor = System.Drawing.Color.Cyan
        Me.lblFuncInProgress.Location = New System.Drawing.Point(15, 9)
        Me.lblFuncInProgress.Name = "lblFuncInProgress"
        Me.lblFuncInProgress.Size = New System.Drawing.Size(118, 13)
        Me.lblFuncInProgress.TabIndex = 7
        Me.lblFuncInProgress.Text = "Unknown Command"
        Me.lblFuncInProgress.Visible = False
        '
        'pbGeneralProgress
        '
        Me.pbGeneralProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbGeneralProgress.Location = New System.Drawing.Point(17, 239)
        Me.pbGeneralProgress.MarqueeAnimationSpeed = 400
        Me.pbGeneralProgress.Name = "pbGeneralProgress"
        Me.pbGeneralProgress.Size = New System.Drawing.Size(697, 23)
        Me.pbGeneralProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pbGeneralProgress.TabIndex = 6
        Me.pbGeneralProgress.Value = 100
        Me.pbGeneralProgress.Visible = False
        '
        'tmrMinimizationDelay
        '
        '
        'TrayIcon
        '
        Me.TrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.TrayIcon.BalloonTipText = "2"
        Me.TrayIcon.BalloonTipTitle = "3"
        Me.TrayIcon.ContextMenuStrip = Me.TraySetting
        Me.TrayIcon.Text = "1"
        '
        'TraySetting
        '
        Me.TraySetting.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.trayShow, Me.trayHide, Me.ToolStripMenuItem7, Me.traySettings, Me.ToolStripMenuItem8, Me.trayWebsite, Me.trayCredits, Me.trayAbout, Me.ToolStripMenuItem9, Me.trayClose})
        Me.TraySetting.Name = "ContextMenuStrip1"
        Me.TraySetting.Size = New System.Drawing.Size(121, 176)
        Me.TraySetting.Text = "TraySettings"
        '
        'trayShow
        '
        Me.trayShow.Name = "trayShow"
        Me.trayShow.Size = New System.Drawing.Size(120, 22)
        Me.trayShow.Text = "Show"
        '
        'trayHide
        '
        Me.trayHide.Name = "trayHide"
        Me.trayHide.Size = New System.Drawing.Size(120, 22)
        Me.trayHide.Text = "Hide"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(117, 6)
        '
        'traySettings
        '
        Me.traySettings.Name = "traySettings"
        Me.traySettings.Size = New System.Drawing.Size(120, 22)
        Me.traySettings.Text = "Settings"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(117, 6)
        '
        'trayWebsite
        '
        Me.trayWebsite.Name = "trayWebsite"
        Me.trayWebsite.Size = New System.Drawing.Size(120, 22)
        Me.trayWebsite.Text = "Website"
        '
        'trayCredits
        '
        Me.trayCredits.Name = "trayCredits"
        Me.trayCredits.Size = New System.Drawing.Size(120, 22)
        Me.trayCredits.Text = "Credits"
        '
        'trayAbout
        '
        Me.trayAbout.Name = "trayAbout"
        Me.trayAbout.Size = New System.Drawing.Size(120, 22)
        Me.trayAbout.Text = "About"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(117, 6)
        '
        'trayClose
        '
        Me.trayClose.Name = "trayClose"
        Me.trayClose.Size = New System.Drawing.Size(120, 22)
        Me.trayClose.Text = "Close"
        '
        'tltMain
        '
        Me.tltMain.AutoPopDelay = 10000
        Me.tltMain.InitialDelay = 500
        Me.tltMain.IsBalloon = True
        Me.tltMain.ReshowDelay = 100
        Me.tltMain.ShowAlways = True
        Me.tltMain.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.tltMain.ToolTipTitle = "Tip:"
        '
        'fswSettings
        '
        Me.fswSettings.EnableRaisingEvents = True
        Me.fswSettings.Filter = "Settings.ini"
        Me.fswSettings.NotifyFilter = System.IO.NotifyFilters.LastWrite
        Me.fswSettings.SynchronizingObject = Me
        '
        'tmrHideReEnable
        '
        Me.tmrHideReEnable.Interval = 6000
        '
        'tmrUpdatePB
        '
        '
        'tmrFunctInProgress
        '
        Me.tmrFunctInProgress.Interval = 1000
        '
        'btnGeoLocate
        '
        Me.btnGeoLocate.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnGeoLocate.Location = New System.Drawing.Point(18, 25)
        Me.btnGeoLocate.Name = "btnGeoLocate"
        Me.btnGeoLocate.Size = New System.Drawing.Size(150, 120)
        Me.btnGeoLocate.TabIndex = 13
        Me.btnGeoLocate.Text = "&Stop Geo-Location"
        Me.btnGeoLocate.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGeoLocate.UseVisualStyleBackColor = True
        Me.btnGeoLocate.Visible = False
        '
        'frmMain
        '
        Me.AcceptButton = Me.cmdGo
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(778, 468)
        Me.Controls.Add(Me.lblHelp)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.mnuMain)
        Me.Enabled = False
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.mnuMain
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.Text = "HEDNO Oracle"
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.gbCommands.ResumeLayout(False)
        Me.gbCommands.PerformLayout()
        Me.pnlCommands.ResumeLayout(False)
        Me.pnlCommands.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.gbFunctions.ResumeLayout(False)
        Me.gbFunctions.PerformLayout()
        Me.pnlFunctions.ResumeLayout(False)
        Me.pnlFunctions.PerformLayout()
        Me.TraySetting.ResumeLayout(False)
        CType(Me.fswSettings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents mniFileMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniHelpMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblHelp As System.Windows.Forms.Label
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents mniDirectoriesMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniDatabaseDir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents mnuSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mniExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniCredits As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniLinksMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniProgramWebsite As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniProgrammerWebsite As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniProgramsDir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblProgUpgraded As System.Windows.Forms.Label
    Friend WithEvents lblProgUpdated As System.Windows.Forms.Label
    Friend WithEvents mniEULA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniShowPresentation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniShowWelcomeScreen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mniChangeLog As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gbCommands As System.Windows.Forms.GroupBox
    Friend WithEvents cmdHelp As System.Windows.Forms.Button
    Friend WithEvents lblUnknownCmd As System.Windows.Forms.Label
    Friend WithEvents cmdGo As System.Windows.Forms.Button
    Friend WithEvents txtCommands As System.Windows.Forms.TextBox
    Friend WithEvents cmdSettings As System.Windows.Forms.Button
    Friend WithEvents tmrUnknownCmd As System.Windows.Forms.Timer
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mniExtrasDir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniLanguageDir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniSettingsDir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniSkinDir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniProgramDocuments As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mniCheckForUpdates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblMathMode As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents gbFunctions As System.Windows.Forms.GroupBox
    Friend WithEvents pnlFunctions As System.Windows.Forms.Panel
    Friend WithEvents tmrMinimizationDelay As System.Windows.Forms.Timer
    Friend WithEvents TrayIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents TraySetting As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents trayShow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents trayHide As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents traySettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents trayWebsite As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents trayCredits As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents trayAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents trayClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tltMain As System.Windows.Forms.ToolTip
    Friend WithEvents mniCompanySite As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlCommands As System.Windows.Forms.Panel
    Friend WithEvents pbGeneralProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents fswSettings As IO.FileSystemWatcher
    Friend WithEvents mniLicenses As ToolStripMenuItem
    Friend WithEvents tmrHideReEnable As Timer
    Friend WithEvents tmrUpdatePB As Timer
    Friend WithEvents mniDatabaseMaintenance As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents mniSuggestOrComplain As ToolStripMenuItem
    Friend WithEvents mniOpenSettingsFile As ToolStripMenuItem
    Friend WithEvents mniPreProcessing As ToolStripMenuItem
    Friend WithEvents mniCreateGeoColumns As ToolStripMenuItem
    Friend WithEvents mniGeoLocate As ToolStripMenuItem
    Friend WithEvents mniClustering As ToolStripMenuItem
    Friend WithEvents mniClusteringStep0 As ToolStripMenuItem
    Friend WithEvents mniPreProcessTheData As ToolStripMenuItem
    Friend WithEvents mniCreateNeededSQLViews As ToolStripMenuItem
    Friend WithEvents lblFuncInProgress As Label
    Friend WithEvents tmrFunctInProgress As Timer
    Friend WithEvents mniClusteringStep1 As ToolStripMenuItem
    Friend WithEvents mniClassification As ToolStripMenuItem
    Friend WithEvents mniLogisticRegression As ToolStripMenuItem
    Friend WithEvents mniFormTrainAndTestSets As ToolStripMenuItem
    Friend WithEvents mniDecisionTrees As ToolStripMenuItem
    Friend WithEvents mniNaiveBayes As ToolStripMenuItem
    Friend WithEvents mniRandomForest As ToolStripMenuItem
    Friend WithEvents mniStochasticGradientBoosting As ToolStripMenuItem
    Friend WithEvents mniStochasticDualCoordinateAscent As ToolStripMenuItem
    Friend WithEvents mniBoostedDecisionTrees As ToolStripMenuItem
    Friend WithEvents mniEnsambleOfDecisionTrees As ToolStripMenuItem
    Friend WithEvents mniNeuralNetworks As ToolStripMenuItem
    Friend WithEvents mniGeoLocationStatus As ToolStripMenuItem
    Friend WithEvents mniExportListofProblematicAddresses As ToolStripMenuItem
    Friend WithEvents mniFastLogisticRegression As ToolStripMenuItem
    Friend WithEvents mniResetInvalidGeolocationEntries As ToolStripMenuItem
    Friend WithEvents btnGeoLocate As Button
End Class
