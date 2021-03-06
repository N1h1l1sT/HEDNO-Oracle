﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmClassification
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClassification))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pbLoading = New System.Windows.Forms.ProgressBar()
        Me.lblLoading = New System.Windows.Forms.Label()
        Me.chkStatisticsMode = New System.Windows.Forms.CheckBox()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.gbOptions = New System.Windows.Forms.GroupBox()
        Me.chkFormTestSet = New System.Windows.Forms.CheckBox()
        Me.chkFormTrainSet = New System.Windows.Forms.CheckBox()
        Me.chkUseExistingXDFFile = New System.Windows.Forms.CheckBox()
        Me.chkShowDataSummary = New System.Windows.Forms.CheckBox()
        Me.chkVisualiseClassImbal = New System.Windows.Forms.CheckBox()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.chkShowVariableInfo = New System.Windows.Forms.CheckBox()
        Me.gbStatistics = New System.Windows.Forms.GroupBox()
        Me.chkShowTestDataSummary = New System.Windows.Forms.CheckBox()
        Me.chkShowTestVarInfo = New System.Windows.Forms.CheckBox()
        Me.chkShowTrainDataSummary = New System.Windows.Forms.CheckBox()
        Me.chkShowTrainVarInfo = New System.Windows.Forms.CheckBox()
        Me.lblTrainPercent = New System.Windows.Forms.Label()
        Me.cbTrainPercent = New System.Windows.Forms.ComboBox()
        Me.chkCleanXDFFile = New System.Windows.Forms.CheckBox()
        Me.btnClassification = New System.Windows.Forms.Button()
        Me.fswModelExists = New System.IO.FileSystemWatcher()
        Me.tmrModelExists = New System.Windows.Forms.Timer(Me.components)
        Me.ttMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlMain.SuspendLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.gbOptions.SuspendLayout()
        Me.gbStatistics.SuspendLayout()
        CType(Me.fswModelExists, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pbLoading)
        Me.pnlMain.Controls.Add(Me.lblLoading)
        Me.pnlMain.Controls.Add(Me.chkStatisticsMode)
        Me.pnlMain.Controls.Add(Me.scMain)
        Me.pnlMain.Controls.Add(Me.btnClassification)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(615, 248)
        Me.pnlMain.TabIndex = 7
        '
        'pbLoading
        '
        Me.pbLoading.Location = New System.Drawing.Point(326, 0)
        Me.pbLoading.MarqueeAnimationSpeed = 10
        Me.pbLoading.Name = "pbLoading"
        Me.pbLoading.Size = New System.Drawing.Size(100, 23)
        Me.pbLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pbLoading.TabIndex = 15
        Me.pbLoading.Visible = False
        '
        'lblLoading
        '
        Me.lblLoading.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblLoading.Location = New System.Drawing.Point(393, 0)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(100, 23)
        Me.lblLoading.TabIndex = 14
        Me.lblLoading.Text = "Loading..."
        Me.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblLoading.Visible = False
        '
        'chkStatisticsMode
        '
        Me.chkStatisticsMode.AutoSize = True
        Me.chkStatisticsMode.Checked = True
        Me.chkStatisticsMode.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkStatisticsMode.Location = New System.Drawing.Point(304, 12)
        Me.chkStatisticsMode.Name = "chkStatisticsMode"
        Me.chkStatisticsMode.Size = New System.Drawing.Size(101, 17)
        Me.chkStatisticsMode.TabIndex = 11
        Me.chkStatisticsMode.Text = "Statistics Mode:"
        Me.ttMain.SetToolTip(Me.chkStatisticsMode, resources.GetString("chkStatisticsMode.ToolTip"))
        Me.chkStatisticsMode.UseVisualStyleBackColor = True
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
        Me.scMain.Panel1.Controls.Add(Me.gbOptions)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.gbStatistics)
        Me.scMain.Size = New System.Drawing.Size(591, 195)
        Me.scMain.SplitterDistance = 279
        Me.scMain.TabIndex = 10
        '
        'gbOptions
        '
        Me.gbOptions.Controls.Add(Me.chkFormTestSet)
        Me.gbOptions.Controls.Add(Me.chkFormTrainSet)
        Me.gbOptions.Controls.Add(Me.chkUseExistingXDFFile)
        Me.gbOptions.Controls.Add(Me.chkShowDataSummary)
        Me.gbOptions.Controls.Add(Me.chkVisualiseClassImbal)
        Me.gbOptions.Controls.Add(Me.btnSelectAll)
        Me.gbOptions.Controls.Add(Me.chkShowVariableInfo)
        Me.gbOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbOptions.Location = New System.Drawing.Point(0, 0)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(279, 195)
        Me.gbOptions.TabIndex = 2
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Main Options:"
        '
        'chkFormTestSet
        '
        Me.chkFormTestSet.AutoSize = True
        Me.chkFormTestSet.Location = New System.Drawing.Point(6, 68)
        Me.chkFormTestSet.Name = "chkFormTestSet"
        Me.chkFormTestSet.Size = New System.Drawing.Size(106, 17)
        Me.chkFormTestSet.TabIndex = 7
        Me.chkFormTestSet.Text = "Form Testing Set"
        Me.ttMain.SetToolTip(Me.chkFormTestSet, resources.GetString("chkFormTestSet.ToolTip"))
        Me.chkFormTestSet.UseVisualStyleBackColor = True
        '
        'chkFormTrainSet
        '
        Me.chkFormTrainSet.AutoSize = True
        Me.chkFormTrainSet.Location = New System.Drawing.Point(6, 45)
        Me.chkFormTrainSet.Name = "chkFormTrainSet"
        Me.chkFormTrainSet.Size = New System.Drawing.Size(109, 17)
        Me.chkFormTrainSet.TabIndex = 6
        Me.chkFormTrainSet.Text = "Form Training Set"
        Me.ttMain.SetToolTip(Me.chkFormTrainSet, resources.GetString("chkFormTrainSet.ToolTip"))
        Me.chkFormTrainSet.UseVisualStyleBackColor = True
        '
        'chkUseExistingXDFFile
        '
        Me.chkUseExistingXDFFile.AutoSize = True
        Me.chkUseExistingXDFFile.Location = New System.Drawing.Point(6, 22)
        Me.chkUseExistingXDFFile.Name = "chkUseExistingXDFFile"
        Me.chkUseExistingXDFFile.Size = New System.Drawing.Size(127, 17)
        Me.chkUseExistingXDFFile.TabIndex = 5
        Me.chkUseExistingXDFFile.Text = "Use Existing XDF File"
        Me.ttMain.SetToolTip(Me.chkUseExistingXDFFile, resources.GetString("chkUseExistingXDFFile.ToolTip"))
        Me.chkUseExistingXDFFile.UseVisualStyleBackColor = True
        '
        'chkShowDataSummary
        '
        Me.chkShowDataSummary.AutoSize = True
        Me.chkShowDataSummary.Location = New System.Drawing.Point(6, 114)
        Me.chkShowDataSummary.Name = "chkShowDataSummary"
        Me.chkShowDataSummary.Size = New System.Drawing.Size(125, 17)
        Me.chkShowDataSummary.TabIndex = 4
        Me.chkShowDataSummary.Text = "Show Data Summary"
        Me.ttMain.SetToolTip(Me.chkShowDataSummary, "View a Data Summary of the Classification dataset, such as Min, Max, Mean, etc.")
        Me.chkShowDataSummary.UseVisualStyleBackColor = True
        '
        'chkVisualiseClassImbal
        '
        Me.chkVisualiseClassImbal.AutoSize = True
        Me.chkVisualiseClassImbal.Location = New System.Drawing.Point(6, 91)
        Me.chkVisualiseClassImbal.Name = "chkVisualiseClassImbal"
        Me.chkVisualiseClassImbal.Size = New System.Drawing.Size(147, 17)
        Me.chkVisualiseClassImbal.TabIndex = 2
        Me.chkVisualiseClassImbal.Text = "Visualise Class Imbalance"
        Me.ttMain.SetToolTip(Me.chkVisualiseClassImbal, "If selected, a graph visualisation of the class imbalance is shown")
        Me.chkVisualiseClassImbal.UseVisualStyleBackColor = True
        '
        'btnSelectAll
        '
        Me.btnSelectAll.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSelectAll.Location = New System.Drawing.Point(6, 160)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(170, 23)
        Me.btnSelectAll.TabIndex = 3
        Me.btnSelectAll.Text = "Select &All"
        Me.ttMain.SetToolTip(Me.btnSelectAll, "Selects all options in the GroupBox")
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'chkShowVariableInfo
        '
        Me.chkShowVariableInfo.AutoSize = True
        Me.chkShowVariableInfo.Location = New System.Drawing.Point(6, 137)
        Me.chkShowVariableInfo.Name = "chkShowVariableInfo"
        Me.chkShowVariableInfo.Size = New System.Drawing.Size(149, 17)
        Me.chkShowVariableInfo.TabIndex = 3
        Me.chkShowVariableInfo.Text = "Show Variable Information"
        Me.ttMain.SetToolTip(Me.chkShowVariableInfo, "View Variable Information such as their types and descriptions.")
        Me.chkShowVariableInfo.UseVisualStyleBackColor = True
        '
        'gbStatistics
        '
        Me.gbStatistics.Controls.Add(Me.chkShowTestDataSummary)
        Me.gbStatistics.Controls.Add(Me.chkShowTestVarInfo)
        Me.gbStatistics.Controls.Add(Me.chkShowTrainDataSummary)
        Me.gbStatistics.Controls.Add(Me.chkShowTrainVarInfo)
        Me.gbStatistics.Controls.Add(Me.lblTrainPercent)
        Me.gbStatistics.Controls.Add(Me.cbTrainPercent)
        Me.gbStatistics.Controls.Add(Me.chkCleanXDFFile)
        Me.gbStatistics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbStatistics.Location = New System.Drawing.Point(0, 0)
        Me.gbStatistics.Name = "gbStatistics"
        Me.gbStatistics.Size = New System.Drawing.Size(308, 195)
        Me.gbStatistics.TabIndex = 11
        Me.gbStatistics.TabStop = False
        '
        'chkShowTestDataSummary
        '
        Me.chkShowTestDataSummary.AutoSize = True
        Me.chkShowTestDataSummary.Location = New System.Drawing.Point(9, 93)
        Me.chkShowTestDataSummary.Name = "chkShowTestDataSummary"
        Me.chkShowTestDataSummary.Size = New System.Drawing.Size(163, 17)
        Me.chkShowTestDataSummary.TabIndex = 16
        Me.chkShowTestDataSummary.Text = "Show Testing Data Summary"
        Me.ttMain.SetToolTip(Me.chkShowTestDataSummary, "View a Data Summary of the Testing dataset, such as Min, Max, Mean, etc." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.chkShowTestDataSummary.UseVisualStyleBackColor = True
        '
        'chkShowTestVarInfo
        '
        Me.chkShowTestVarInfo.AutoSize = True
        Me.chkShowTestVarInfo.Location = New System.Drawing.Point(9, 118)
        Me.chkShowTestVarInfo.Name = "chkShowTestVarInfo"
        Me.chkShowTestVarInfo.Size = New System.Drawing.Size(187, 17)
        Me.chkShowTestVarInfo.TabIndex = 15
        Me.chkShowTestVarInfo.Text = "Show Testing Variable Information"
        Me.ttMain.SetToolTip(Me.chkShowTestVarInfo, "View Variable Information such as their types and descriptions.")
        Me.chkShowTestVarInfo.UseVisualStyleBackColor = True
        '
        'chkShowTrainDataSummary
        '
        Me.chkShowTrainDataSummary.AutoSize = True
        Me.chkShowTrainDataSummary.Location = New System.Drawing.Point(9, 45)
        Me.chkShowTrainDataSummary.Name = "chkShowTrainDataSummary"
        Me.chkShowTrainDataSummary.Size = New System.Drawing.Size(166, 17)
        Me.chkShowTrainDataSummary.TabIndex = 14
        Me.chkShowTrainDataSummary.Text = "Show Training Data Summary"
        Me.ttMain.SetToolTip(Me.chkShowTrainDataSummary, "View a Data Summary of the Training dataset, such as Min, Max, Mean, etc.")
        Me.chkShowTrainDataSummary.UseVisualStyleBackColor = True
        '
        'chkShowTrainVarInfo
        '
        Me.chkShowTrainVarInfo.AutoSize = True
        Me.chkShowTrainVarInfo.Location = New System.Drawing.Point(9, 70)
        Me.chkShowTrainVarInfo.Name = "chkShowTrainVarInfo"
        Me.chkShowTrainVarInfo.Size = New System.Drawing.Size(190, 17)
        Me.chkShowTrainVarInfo.TabIndex = 13
        Me.chkShowTrainVarInfo.Text = "Show Training Variable Information"
        Me.ttMain.SetToolTip(Me.chkShowTrainVarInfo, "View Variable Information such as their types and descriptions.")
        Me.chkShowTrainVarInfo.UseVisualStyleBackColor = True
        '
        'lblTrainPercent
        '
        Me.lblTrainPercent.AutoSize = True
        Me.lblTrainPercent.Location = New System.Drawing.Point(6, 23)
        Me.lblTrainPercent.Name = "lblTrainPercent"
        Me.lblTrainPercent.Size = New System.Drawing.Size(125, 13)
        Me.lblTrainPercent.TabIndex = 12
        Me.lblTrainPercent.Text = "Training Set Percentage:"
        '
        'cbTrainPercent
        '
        Me.cbTrainPercent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbTrainPercent.FormattingEnabled = True
        Me.cbTrainPercent.Location = New System.Drawing.Point(137, 20)
        Me.cbTrainPercent.Name = "cbTrainPercent"
        Me.cbTrainPercent.Size = New System.Drawing.Size(165, 21)
        Me.cbTrainPercent.TabIndex = 8
        Me.ttMain.SetToolTip(Me.cbTrainPercent, "When creating a new XDF file in Statistics Mode, what is the percentage of projec" &
        "ts to be marked for the Training Set?")
        '
        'chkCleanXDFFile
        '
        Me.chkCleanXDFFile.AutoSize = True
        Me.chkCleanXDFFile.Location = New System.Drawing.Point(9, 141)
        Me.chkCleanXDFFile.Name = "chkCleanXDFFile"
        Me.chkCleanXDFFile.Size = New System.Drawing.Size(189, 17)
        Me.chkCleanXDFFile.TabIndex = 1
        Me.chkCleanXDFFile.Text = "Clean the XDF file after completion"
        Me.ttMain.SetToolTip(Me.chkCleanXDFFile, "If selected, then the Classification XDF file is going to be deleted when the for" &
        "m closes, meaning it is going to be unavailable for further and future use.")
        Me.chkCleanXDFFile.UseVisualStyleBackColor = True
        '
        'btnClassification
        '
        Me.btnClassification.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClassification.Enabled = False
        Me.btnClassification.Location = New System.Drawing.Point(12, 213)
        Me.btnClassification.Name = "btnClassification"
        Me.btnClassification.Size = New System.Drawing.Size(591, 23)
        Me.btnClassification.TabIndex = 0
        Me.btnClassification.Text = "Form Training and Test Sets"
        Me.ttMain.SetToolTip(Me.btnClassification, resources.GetString("btnClassification.ToolTip"))
        Me.btnClassification.UseVisualStyleBackColor = True
        '
        'fswModelExists
        '
        Me.fswModelExists.EnableRaisingEvents = True
        Me.fswModelExists.SynchronizingObject = Me
        '
        'tmrModelExists
        '
        Me.tmrModelExists.Interval = 10
        '
        'ttMain
        '
        Me.ttMain.AutoPopDelay = 10000
        Me.ttMain.InitialDelay = 500
        Me.ttMain.ReshowDelay = 100
        Me.ttMain.ShowAlways = True
        '
        'frmClassification
        '
        Me.AcceptButton = Me.btnClassification
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnSelectAll
        Me.ClientSize = New System.Drawing.Size(615, 248)
        Me.Controls.Add(Me.pnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmClassification"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Classification Options"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.gbOptions.ResumeLayout(False)
        Me.gbOptions.PerformLayout()
        Me.gbStatistics.ResumeLayout(False)
        Me.gbStatistics.PerformLayout()
        CType(Me.fswModelExists, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents gbOptions As GroupBox
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents chkUseExistingXDFFile As CheckBox
    Friend WithEvents chkVisualiseClassImbal As CheckBox
    Friend WithEvents chkShowVariableInfo As CheckBox
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents chkShowDataSummary As CheckBox
    Friend WithEvents chkCleanXDFFile As CheckBox
    Friend WithEvents btnClassification As Button
    Friend WithEvents gbStatistics As GroupBox
    Friend WithEvents chkStatisticsMode As CheckBox
    Friend WithEvents chkFormTestSet As CheckBox
    Friend WithEvents chkFormTrainSet As CheckBox
    Friend WithEvents cbTrainPercent As ComboBox
    Friend WithEvents lblTrainPercent As Label
    Friend WithEvents chkShowTestDataSummary As CheckBox
    Friend WithEvents chkShowTestVarInfo As CheckBox
    Friend WithEvents chkShowTrainDataSummary As CheckBox
    Friend WithEvents chkShowTrainVarInfo As CheckBox
    Friend WithEvents fswModelExists As IO.FileSystemWatcher
    Friend WithEvents tmrModelExists As Timer
    Friend WithEvents lblLoading As Label
    Friend WithEvents pbLoading As ProgressBar
    Friend WithEvents ttMain As ToolTip
End Class
