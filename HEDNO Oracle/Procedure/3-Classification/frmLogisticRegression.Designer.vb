﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLogisticRegression
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogisticRegression))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pbLoading = New System.Windows.Forms.ProgressBar()
        Me.lblLoading = New System.Windows.Forms.Label()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.gbStatistics = New System.Windows.Forms.GroupBox()
        Me.chkOpenGraphDirectory = New System.Windows.Forms.CheckBox()
        Me.lblRoundAt = New System.Windows.Forms.Label()
        Me.txtRoundAt = New System.Windows.Forms.TextBox()
        Me.chkShowROCCurve = New System.Windows.Forms.CheckBox()
        Me.chkShowStatistics = New System.Windows.Forms.CheckBox()
        Me.chkStatisticsMode = New System.Windows.Forms.CheckBox()
        Me.gbOptions = New System.Windows.Forms.GroupBox()
        Me.chkSavePredictionModel = New System.Windows.Forms.CheckBox()
        Me.lblSavePath = New System.Windows.Forms.Label()
        Me.txtSavePath = New System.Windows.Forms.TextBox()
        Me.chkMakePredictions = New System.Windows.Forms.CheckBox()
        Me.chkUseExistingModel = New System.Windows.Forms.CheckBox()
        Me.chkShowDataSummary = New System.Windows.Forms.CheckBox()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.chkShowVariableInfo = New System.Windows.Forms.CheckBox()
        Me.scColumns = New System.Windows.Forms.SplitContainer()
        Me.pbColumnsLoading = New System.Windows.Forms.ProgressBar()
        Me.lblColumnsLoading = New System.Windows.Forms.Label()
        Me.clbColumns = New System.Windows.Forms.CheckedListBox()
        Me.lblNGrams = New System.Windows.Forms.Label()
        Me.txtNGrams = New System.Windows.Forms.TextBox()
        Me.chkUpToNGramsN = New System.Windows.Forms.CheckBox()
        Me.btnSelectAllColumns = New System.Windows.Forms.Button()
        Me.lblCombinationsCount = New System.Windows.Forms.Label()
        Me.chkColumnsCombinations = New System.Windows.Forms.CheckBox()
        Me.btnRunModel = New System.Windows.Forms.Button()
        Me.fbdModelPath = New System.Windows.Forms.FolderBrowserDialog()
        Me.tmrLoadColumns = New System.Windows.Forms.Timer(Me.components)
        Me.fswModelFileExists = New System.IO.FileSystemWatcher()
        Me.ttMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.fswXDFFileExists = New System.IO.FileSystemWatcher()
        Me.tmrXDFExists = New System.Windows.Forms.Timer(Me.components)
        Me.fswTrainAndTest = New System.IO.FileSystemWatcher()
        Me.pnlMain.SuspendLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.gbStatistics.SuspendLayout()
        Me.gbOptions.SuspendLayout()
        CType(Me.scColumns, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scColumns.Panel1.SuspendLayout()
        Me.scColumns.Panel2.SuspendLayout()
        Me.scColumns.SuspendLayout()
        CType(Me.fswModelFileExists, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fswXDFFileExists, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fswTrainAndTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pbLoading)
        Me.pnlMain.Controls.Add(Me.lblLoading)
        Me.pnlMain.Controls.Add(Me.scMain)
        Me.pnlMain.Controls.Add(Me.btnRunModel)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(540, 389)
        Me.pnlMain.TabIndex = 8
        '
        'pbLoading
        '
        Me.pbLoading.Location = New System.Drawing.Point(395, 0)
        Me.pbLoading.MarqueeAnimationSpeed = 10
        Me.pbLoading.Name = "pbLoading"
        Me.pbLoading.Size = New System.Drawing.Size(100, 23)
        Me.pbLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pbLoading.TabIndex = 16
        Me.pbLoading.Visible = False
        '
        'lblLoading
        '
        Me.lblLoading.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblLoading.Location = New System.Drawing.Point(439, 0)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(100, 23)
        Me.lblLoading.TabIndex = 15
        Me.lblLoading.Text = "Loading..."
        Me.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.Location = New System.Drawing.Point(12, 12)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.gbStatistics)
        Me.scMain.Panel1.Controls.Add(Me.gbOptions)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.scColumns)
        Me.scMain.Size = New System.Drawing.Size(516, 336)
        Me.scMain.SplitterDistance = 252
        Me.scMain.TabIndex = 10
        '
        'gbStatistics
        '
        Me.gbStatistics.Controls.Add(Me.chkOpenGraphDirectory)
        Me.gbStatistics.Controls.Add(Me.lblRoundAt)
        Me.gbStatistics.Controls.Add(Me.txtRoundAt)
        Me.gbStatistics.Controls.Add(Me.chkShowROCCurve)
        Me.gbStatistics.Controls.Add(Me.chkShowStatistics)
        Me.gbStatistics.Controls.Add(Me.chkStatisticsMode)
        Me.gbStatistics.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbStatistics.Location = New System.Drawing.Point(0, 216)
        Me.gbStatistics.Name = "gbStatistics"
        Me.gbStatistics.Size = New System.Drawing.Size(252, 120)
        Me.gbStatistics.TabIndex = 11
        Me.gbStatistics.TabStop = False
        '
        'chkOpenGraphDirectory
        '
        Me.chkOpenGraphDirectory.AutoSize = True
        Me.chkOpenGraphDirectory.Enabled = False
        Me.chkOpenGraphDirectory.Location = New System.Drawing.Point(6, 65)
        Me.chkOpenGraphDirectory.Name = "chkOpenGraphDirectory"
        Me.chkOpenGraphDirectory.Size = New System.Drawing.Size(116, 17)
        Me.chkOpenGraphDirectory.TabIndex = 20
        Me.chkOpenGraphDirectory.Text = "Open Graph Folder"
        Me.chkOpenGraphDirectory.UseVisualStyleBackColor = True
        '
        'lblRoundAt
        '
        Me.lblRoundAt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRoundAt.AutoSize = True
        Me.lblRoundAt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblRoundAt.Location = New System.Drawing.Point(6, 91)
        Me.lblRoundAt.Name = "lblRoundAt"
        Me.lblRoundAt.Size = New System.Drawing.Size(55, 13)
        Me.lblRoundAt.TabIndex = 19
        Me.lblRoundAt.Text = "Round At:"
        '
        'txtRoundAt
        '
        Me.txtRoundAt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRoundAt.Location = New System.Drawing.Point(67, 88)
        Me.txtRoundAt.Name = "txtRoundAt"
        Me.txtRoundAt.ReadOnly = True
        Me.txtRoundAt.Size = New System.Drawing.Size(44, 20)
        Me.txtRoundAt.TabIndex = 19
        Me.txtRoundAt.Text = "1"
        '
        'chkShowROCCurve
        '
        Me.chkShowROCCurve.AutoSize = True
        Me.chkShowROCCurve.Location = New System.Drawing.Point(6, 42)
        Me.chkShowROCCurve.Name = "chkShowROCCurve"
        Me.chkShowROCCurve.Size = New System.Drawing.Size(110, 17)
        Me.chkShowROCCurve.TabIndex = 13
        Me.chkShowROCCurve.Text = "Show ROC Curve"
        Me.chkShowROCCurve.UseVisualStyleBackColor = True
        '
        'chkShowStatistics
        '
        Me.chkShowStatistics.AutoSize = True
        Me.chkShowStatistics.Location = New System.Drawing.Point(6, 19)
        Me.chkShowStatistics.Name = "chkShowStatistics"
        Me.chkShowStatistics.Size = New System.Drawing.Size(98, 17)
        Me.chkShowStatistics.TabIndex = 12
        Me.chkShowStatistics.Text = "Show Statistics"
        Me.chkShowStatistics.UseVisualStyleBackColor = True
        '
        'chkStatisticsMode
        '
        Me.chkStatisticsMode.AutoSize = True
        Me.chkStatisticsMode.Checked = True
        Me.chkStatisticsMode.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkStatisticsMode.Location = New System.Drawing.Point(1, -1)
        Me.chkStatisticsMode.Name = "chkStatisticsMode"
        Me.chkStatisticsMode.Size = New System.Drawing.Size(101, 17)
        Me.chkStatisticsMode.TabIndex = 11
        Me.chkStatisticsMode.Text = "Statistics Mode:"
        Me.chkStatisticsMode.UseVisualStyleBackColor = True
        '
        'gbOptions
        '
        Me.gbOptions.Controls.Add(Me.chkSavePredictionModel)
        Me.gbOptions.Controls.Add(Me.lblSavePath)
        Me.gbOptions.Controls.Add(Me.txtSavePath)
        Me.gbOptions.Controls.Add(Me.chkMakePredictions)
        Me.gbOptions.Controls.Add(Me.chkUseExistingModel)
        Me.gbOptions.Controls.Add(Me.chkShowDataSummary)
        Me.gbOptions.Controls.Add(Me.btnSelectAll)
        Me.gbOptions.Controls.Add(Me.chkShowVariableInfo)
        Me.gbOptions.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbOptions.Location = New System.Drawing.Point(0, 0)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(252, 210)
        Me.gbOptions.TabIndex = 2
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Main Options:"
        '
        'chkSavePredictionModel
        '
        Me.chkSavePredictionModel.AutoSize = True
        Me.chkSavePredictionModel.Location = New System.Drawing.Point(9, 65)
        Me.chkSavePredictionModel.Name = "chkSavePredictionModel"
        Me.chkSavePredictionModel.Size = New System.Drawing.Size(133, 17)
        Me.chkSavePredictionModel.TabIndex = 13
        Me.chkSavePredictionModel.Text = "Save Prediction Model"
        Me.chkSavePredictionModel.UseVisualStyleBackColor = True
        '
        'lblSavePath
        '
        Me.lblSavePath.AutoSize = True
        Me.lblSavePath.Location = New System.Drawing.Point(6, 88)
        Me.lblSavePath.Name = "lblSavePath"
        Me.lblSavePath.Size = New System.Drawing.Size(79, 13)
        Me.lblSavePath.TabIndex = 12
        Me.lblSavePath.Text = "Save Model at:"
        '
        'txtSavePath
        '
        Me.txtSavePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSavePath.Location = New System.Drawing.Point(6, 104)
        Me.txtSavePath.Name = "txtSavePath"
        Me.txtSavePath.ReadOnly = True
        Me.txtSavePath.Size = New System.Drawing.Size(240, 20)
        Me.txtSavePath.TabIndex = 11
        '
        'chkMakePredictions
        '
        Me.chkMakePredictions.AutoSize = True
        Me.chkMakePredictions.Location = New System.Drawing.Point(9, 42)
        Me.chkMakePredictions.Name = "chkMakePredictions"
        Me.chkMakePredictions.Size = New System.Drawing.Size(108, 17)
        Me.chkMakePredictions.TabIndex = 6
        Me.chkMakePredictions.Text = "Make Predictions"
        Me.chkMakePredictions.UseVisualStyleBackColor = True
        '
        'chkUseExistingModel
        '
        Me.chkUseExistingModel.AutoSize = True
        Me.chkUseExistingModel.Location = New System.Drawing.Point(9, 19)
        Me.chkUseExistingModel.Name = "chkUseExistingModel"
        Me.chkUseExistingModel.Size = New System.Drawing.Size(116, 17)
        Me.chkUseExistingModel.TabIndex = 5
        Me.chkUseExistingModel.Text = "Use Existing Model"
        Me.ttMain.SetToolTip(Me.chkUseExistingModel, resources.GetString("chkUseExistingModel.ToolTip"))
        Me.chkUseExistingModel.UseVisualStyleBackColor = True
        '
        'chkShowDataSummary
        '
        Me.chkShowDataSummary.AutoSize = True
        Me.chkShowDataSummary.Location = New System.Drawing.Point(6, 133)
        Me.chkShowDataSummary.Name = "chkShowDataSummary"
        Me.chkShowDataSummary.Size = New System.Drawing.Size(125, 17)
        Me.chkShowDataSummary.TabIndex = 4
        Me.chkShowDataSummary.Text = "Show Data Summary"
        Me.chkShowDataSummary.UseVisualStyleBackColor = True
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelectAll.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSelectAll.Location = New System.Drawing.Point(6, 179)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(240, 23)
        Me.btnSelectAll.TabIndex = 3
        Me.btnSelectAll.Text = "Select &All"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'chkShowVariableInfo
        '
        Me.chkShowVariableInfo.AutoSize = True
        Me.chkShowVariableInfo.Location = New System.Drawing.Point(6, 156)
        Me.chkShowVariableInfo.Name = "chkShowVariableInfo"
        Me.chkShowVariableInfo.Size = New System.Drawing.Size(149, 17)
        Me.chkShowVariableInfo.TabIndex = 3
        Me.chkShowVariableInfo.Text = "Show Variable Information"
        Me.chkShowVariableInfo.UseVisualStyleBackColor = True
        '
        'scColumns
        '
        Me.scColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scColumns.Location = New System.Drawing.Point(0, 0)
        Me.scColumns.Name = "scColumns"
        Me.scColumns.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scColumns.Panel1
        '
        Me.scColumns.Panel1.Controls.Add(Me.pbColumnsLoading)
        Me.scColumns.Panel1.Controls.Add(Me.lblColumnsLoading)
        Me.scColumns.Panel1.Controls.Add(Me.clbColumns)
        '
        'scColumns.Panel2
        '
        Me.scColumns.Panel2.Controls.Add(Me.lblNGrams)
        Me.scColumns.Panel2.Controls.Add(Me.txtNGrams)
        Me.scColumns.Panel2.Controls.Add(Me.chkUpToNGramsN)
        Me.scColumns.Panel2.Controls.Add(Me.btnSelectAllColumns)
        Me.scColumns.Panel2.Controls.Add(Me.lblCombinationsCount)
        Me.scColumns.Panel2.Controls.Add(Me.chkColumnsCombinations)
        Me.scColumns.Size = New System.Drawing.Size(260, 336)
        Me.scColumns.SplitterDistance = 232
        Me.scColumns.TabIndex = 1
        '
        'pbColumnsLoading
        '
        Me.pbColumnsLoading.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbColumnsLoading.Location = New System.Drawing.Point(3, 127)
        Me.pbColumnsLoading.MarqueeAnimationSpeed = 1
        Me.pbColumnsLoading.Name = "pbColumnsLoading"
        Me.pbColumnsLoading.Size = New System.Drawing.Size(254, 23)
        Me.pbColumnsLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pbColumnsLoading.TabIndex = 2
        '
        'lblColumnsLoading
        '
        Me.lblColumnsLoading.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblColumnsLoading.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblColumnsLoading.Location = New System.Drawing.Point(0, 0)
        Me.lblColumnsLoading.Name = "lblColumnsLoading"
        Me.lblColumnsLoading.Size = New System.Drawing.Size(260, 232)
        Me.lblColumnsLoading.TabIndex = 1
        Me.lblColumnsLoading.Text = "Loading..."
        Me.lblColumnsLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'clbColumns
        '
        Me.clbColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbColumns.FormattingEnabled = True
        Me.clbColumns.Location = New System.Drawing.Point(0, 0)
        Me.clbColumns.Name = "clbColumns"
        Me.clbColumns.Size = New System.Drawing.Size(260, 232)
        Me.clbColumns.TabIndex = 0
        '
        'lblNGrams
        '
        Me.lblNGrams.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblNGrams.AutoSize = True
        Me.lblNGrams.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblNGrams.Location = New System.Drawing.Point(53, 34)
        Me.lblNGrams.Name = "lblNGrams"
        Me.lblNGrams.Size = New System.Drawing.Size(38, 13)
        Me.lblNGrams.TabIndex = 18
        Me.lblNGrams.Text = "-grams"
        '
        'txtNGrams
        '
        Me.txtNGrams.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNGrams.Location = New System.Drawing.Point(3, 31)
        Me.txtNGrams.Name = "txtNGrams"
        Me.txtNGrams.ReadOnly = True
        Me.txtNGrams.Size = New System.Drawing.Size(44, 20)
        Me.txtNGrams.TabIndex = 17
        Me.txtNGrams.Text = "1"
        '
        'chkUpToNGramsN
        '
        Me.chkUpToNGramsN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkUpToNGramsN.AutoSize = True
        Me.chkUpToNGramsN.Enabled = False
        Me.chkUpToNGramsN.Location = New System.Drawing.Point(3, 80)
        Me.chkUpToNGramsN.Name = "chkUpToNGramsN"
        Me.chkUpToNGramsN.Size = New System.Drawing.Size(108, 17)
        Me.chkUpToNGramsN.TabIndex = 16
        Me.chkUpToNGramsN.Text = "Up to n-grams's n"
        Me.chkUpToNGramsN.UseVisualStyleBackColor = True
        '
        'btnSelectAllColumns
        '
        Me.btnSelectAllColumns.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelectAllColumns.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSelectAllColumns.Enabled = False
        Me.btnSelectAllColumns.Location = New System.Drawing.Point(3, 5)
        Me.btnSelectAllColumns.Name = "btnSelectAllColumns"
        Me.btnSelectAllColumns.Size = New System.Drawing.Size(254, 23)
        Me.btnSelectAllColumns.TabIndex = 14
        Me.btnSelectAllColumns.Text = "Select &All"
        Me.btnSelectAllColumns.UseVisualStyleBackColor = True
        '
        'lblCombinationsCount
        '
        Me.lblCombinationsCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCombinationsCount.AutoSize = True
        Me.lblCombinationsCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblCombinationsCount.Location = New System.Drawing.Point(183, 34)
        Me.lblCombinationsCount.Name = "lblCombinationsCount"
        Me.lblCombinationsCount.Size = New System.Drawing.Size(74, 13)
        Me.lblCombinationsCount.TabIndex = 15
        Me.lblCombinationsCount.Text = "1 Combination"
        '
        'chkColumnsCombinations
        '
        Me.chkColumnsCombinations.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkColumnsCombinations.AutoSize = True
        Me.chkColumnsCombinations.Enabled = False
        Me.chkColumnsCombinations.Location = New System.Drawing.Point(3, 57)
        Me.chkColumnsCombinations.Name = "chkColumnsCombinations"
        Me.chkColumnsCombinations.Size = New System.Drawing.Size(184, 17)
        Me.chkColumnsCombinations.TabIndex = 14
        Me.chkColumnsCombinations.Text = "Iterate over Column Combinations"
        Me.chkColumnsCombinations.UseVisualStyleBackColor = True
        '
        'btnRunModel
        '
        Me.btnRunModel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRunModel.Enabled = False
        Me.btnRunModel.Location = New System.Drawing.Point(12, 354)
        Me.btnRunModel.Name = "btnRunModel"
        Me.btnRunModel.Size = New System.Drawing.Size(516, 23)
        Me.btnRunModel.TabIndex = 0
        Me.btnRunModel.Text = "Apply Logistic Regression"
        Me.btnRunModel.UseVisualStyleBackColor = True
        '
        'tmrLoadColumns
        '
        '
        'fswModelFileExists
        '
        Me.fswModelFileExists.EnableRaisingEvents = True
        Me.fswModelFileExists.SynchronizingObject = Me
        '
        'fswXDFFileExists
        '
        Me.fswXDFFileExists.EnableRaisingEvents = True
        Me.fswXDFFileExists.SynchronizingObject = Me
        '
        'tmrXDFExists
        '
        Me.tmrXDFExists.Interval = 10
        '
        'fswTrainAndTest
        '
        Me.fswTrainAndTest.EnableRaisingEvents = True
        Me.fswTrainAndTest.SynchronizingObject = Me
        '
        'frmLogisticRegression
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(540, 389)
        Me.Controls.Add(Me.pnlMain)
        Me.Name = "frmLogisticRegression"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "1. Logistic Regression"
        Me.pnlMain.ResumeLayout(False)
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.gbStatistics.ResumeLayout(False)
        Me.gbStatistics.PerformLayout()
        Me.gbOptions.ResumeLayout(False)
        Me.gbOptions.PerformLayout()
        Me.scColumns.Panel1.ResumeLayout(False)
        Me.scColumns.Panel2.ResumeLayout(False)
        Me.scColumns.Panel2.PerformLayout()
        CType(Me.scColumns, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scColumns.ResumeLayout(False)
        CType(Me.fswModelFileExists, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fswXDFFileExists, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fswTrainAndTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents gbOptions As GroupBox
    Friend WithEvents chkShowDataSummary As CheckBox
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents chkShowVariableInfo As CheckBox
    Friend WithEvents gbStatistics As GroupBox
    Friend WithEvents chkStatisticsMode As CheckBox
    Friend WithEvents btnRunModel As Button
    Friend WithEvents chkMakePredictions As CheckBox
    Friend WithEvents chkUseExistingModel As CheckBox
    Friend WithEvents chkShowROCCurve As CheckBox
    Friend WithEvents chkShowStatistics As CheckBox
    Friend WithEvents lblSavePath As Label
    Friend WithEvents txtSavePath As TextBox
    Friend WithEvents fbdModelPath As FolderBrowserDialog
    Friend WithEvents chkSavePredictionModel As CheckBox
    Friend WithEvents clbColumns As CheckedListBox
    Friend WithEvents chkColumnsCombinations As CheckBox
    Friend WithEvents scColumns As SplitContainer
    Friend WithEvents lblCombinationsCount As Label
    Friend WithEvents btnSelectAllColumns As Button
    Friend WithEvents lblColumnsLoading As Label
    Friend WithEvents tmrLoadColumns As Timer
    Friend WithEvents pbColumnsLoading As ProgressBar
    Friend WithEvents chkUpToNGramsN As CheckBox
    Friend WithEvents lblNGrams As Label
    Friend WithEvents txtNGrams As TextBox
    Friend WithEvents lblRoundAt As Label
    Friend WithEvents txtRoundAt As TextBox
    Friend WithEvents fswModelFileExists As IO.FileSystemWatcher
    Friend WithEvents ttMain As ToolTip
    Friend WithEvents pbLoading As ProgressBar
    Friend WithEvents lblLoading As Label
    Friend WithEvents fswXDFFileExists As IO.FileSystemWatcher
    Friend WithEvents tmrXDFExists As Timer
    Friend WithEvents fswTrainAndTest As IO.FileSystemWatcher
    Friend WithEvents chkOpenGraphDirectory As CheckBox
End Class