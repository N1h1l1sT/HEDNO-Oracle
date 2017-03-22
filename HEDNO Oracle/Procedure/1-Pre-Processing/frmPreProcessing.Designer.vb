<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreProcessing
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPreProcessing))
        Me.btnPreProcess = New System.Windows.Forms.Button()
        Me.chkCleanXDFFile = New System.Windows.Forms.CheckBox()
        Me.gbOptions = New System.Windows.Forms.GroupBox()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.chkUseExistingXDFFile = New System.Windows.Forms.CheckBox()
        Me.chkShowDataSummary = New System.Windows.Forms.CheckBox()
        Me.chkShowGeoLocGraph = New System.Windows.Forms.CheckBox()
        Me.chkShowVariableInfo = New System.Windows.Forms.CheckBox()
        Me.lblLoading = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pbLoading = New System.Windows.Forms.ProgressBar()
        Me.chkStatisticsMode = New System.Windows.Forms.CheckBox()
        Me.fswXDFFileExists = New System.IO.FileSystemWatcher()
        Me.tmrXDFExists = New System.Windows.Forms.Timer(Me.components)
        Me.ttMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbOptions.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.fswXDFFileExists, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPreProcess
        '
        Me.btnPreProcess.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreProcess.Location = New System.Drawing.Point(12, 211)
        Me.btnPreProcess.Name = "btnPreProcess"
        Me.btnPreProcess.Size = New System.Drawing.Size(279, 23)
        Me.btnPreProcess.TabIndex = 0
        Me.btnPreProcess.Text = "Commence Pre-Processing"
        Me.ttMain.SetToolTip(Me.btnPreProcess, resources.GetString("btnPreProcess.ToolTip"))
        Me.btnPreProcess.UseVisualStyleBackColor = True
        '
        'chkCleanXDFFile
        '
        Me.chkCleanXDFFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkCleanXDFFile.AutoSize = True
        Me.chkCleanXDFFile.Location = New System.Drawing.Point(12, 188)
        Me.chkCleanXDFFile.Name = "chkCleanXDFFile"
        Me.chkCleanXDFFile.Size = New System.Drawing.Size(189, 17)
        Me.chkCleanXDFFile.TabIndex = 1
        Me.chkCleanXDFFile.Text = "Clean the XDF file after completion"
        Me.ttMain.SetToolTip(Me.chkCleanXDFFile, "If selected, then the XDF file is going to be deleted when the form closes, meani" &
        "ng it is going to be unavailable for further and future use")
        Me.chkCleanXDFFile.UseVisualStyleBackColor = True
        '
        'gbOptions
        '
        Me.gbOptions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbOptions.Controls.Add(Me.btnSelectAll)
        Me.gbOptions.Controls.Add(Me.chkUseExistingXDFFile)
        Me.gbOptions.Controls.Add(Me.chkShowDataSummary)
        Me.gbOptions.Controls.Add(Me.chkShowGeoLocGraph)
        Me.gbOptions.Controls.Add(Me.chkShowVariableInfo)
        Me.gbOptions.Location = New System.Drawing.Point(12, 12)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(279, 142)
        Me.gbOptions.TabIndex = 2
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Main Options:"
        Me.ttMain.SetToolTip(Me.gbOptions, "The main options to consider creating the Pre-Processing Dataset")
        '
        'btnSelectAll
        '
        Me.btnSelectAll.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSelectAll.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnSelectAll.Location = New System.Drawing.Point(3, 116)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(273, 23)
        Me.btnSelectAll.TabIndex = 3
        Me.btnSelectAll.Text = "Select &All"
        Me.ttMain.SetToolTip(Me.btnSelectAll, "Selects all options in the GroupBox")
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'chkUseExistingXDFFile
        '
        Me.chkUseExistingXDFFile.AutoSize = True
        Me.chkUseExistingXDFFile.Location = New System.Drawing.Point(6, 19)
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
        Me.chkShowDataSummary.Location = New System.Drawing.Point(6, 42)
        Me.chkShowDataSummary.Name = "chkShowDataSummary"
        Me.chkShowDataSummary.Size = New System.Drawing.Size(125, 17)
        Me.chkShowDataSummary.TabIndex = 4
        Me.chkShowDataSummary.Text = "Show Data Summary"
        Me.ttMain.SetToolTip(Me.chkShowDataSummary, "View a Data Summary of the dataset, such as Min, Max, Mean, etc.")
        Me.chkShowDataSummary.UseVisualStyleBackColor = True
        '
        'chkShowGeoLocGraph
        '
        Me.chkShowGeoLocGraph.AutoSize = True
        Me.chkShowGeoLocGraph.Location = New System.Drawing.Point(6, 88)
        Me.chkShowGeoLocGraph.Name = "chkShowGeoLocGraph"
        Me.chkShowGeoLocGraph.Size = New System.Drawing.Size(170, 17)
        Me.chkShowGeoLocGraph.TabIndex = 2
        Me.chkShowGeoLocGraph.Text = "Show the Geo-Location Graph"
        Me.ttMain.SetToolTip(Me.chkShowGeoLocGraph, "View the Geo-Location Graph as of Pre-Processing's point of view (may contain err" &
        "oneous latitudes/longitudes outside Greece's region)")
        Me.chkShowGeoLocGraph.UseVisualStyleBackColor = True
        '
        'chkShowVariableInfo
        '
        Me.chkShowVariableInfo.AutoSize = True
        Me.chkShowVariableInfo.Location = New System.Drawing.Point(6, 65)
        Me.chkShowVariableInfo.Name = "chkShowVariableInfo"
        Me.chkShowVariableInfo.Size = New System.Drawing.Size(149, 17)
        Me.chkShowVariableInfo.TabIndex = 3
        Me.chkShowVariableInfo.Text = "Show Variable Information"
        Me.ttMain.SetToolTip(Me.chkShowVariableInfo, "View Variable Information such as their types and descriptions.")
        Me.chkShowVariableInfo.UseVisualStyleBackColor = True
        '
        'lblLoading
        '
        Me.lblLoading.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblLoading.Location = New System.Drawing.Point(183, 0)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(100, 23)
        Me.lblLoading.TabIndex = 13
        Me.lblLoading.Text = "Loading..."
        Me.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblLoading.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pbLoading)
        Me.pnlMain.Controls.Add(Me.lblLoading)
        Me.pnlMain.Controls.Add(Me.chkStatisticsMode)
        Me.pnlMain.Controls.Add(Me.gbOptions)
        Me.pnlMain.Controls.Add(Me.btnPreProcess)
        Me.pnlMain.Controls.Add(Me.chkCleanXDFFile)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(303, 246)
        Me.pnlMain.TabIndex = 4
        '
        'pbLoading
        '
        Me.pbLoading.Location = New System.Drawing.Point(139, 0)
        Me.pbLoading.MarqueeAnimationSpeed = 10
        Me.pbLoading.Name = "pbLoading"
        Me.pbLoading.Size = New System.Drawing.Size(100, 23)
        Me.pbLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pbLoading.TabIndex = 14
        Me.pbLoading.Visible = False
        '
        'chkStatisticsMode
        '
        Me.chkStatisticsMode.AutoSize = True
        Me.chkStatisticsMode.Checked = True
        Me.chkStatisticsMode.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkStatisticsMode.Location = New System.Drawing.Point(12, 165)
        Me.chkStatisticsMode.Name = "chkStatisticsMode"
        Me.chkStatisticsMode.Size = New System.Drawing.Size(98, 17)
        Me.chkStatisticsMode.TabIndex = 12
        Me.chkStatisticsMode.Text = "Statistics Mode"
        Me.ttMain.SetToolTip(Me.chkStatisticsMode, resources.GetString("chkStatisticsMode.ToolTip"))
        Me.chkStatisticsMode.UseVisualStyleBackColor = True
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
        'ttMain
        '
        Me.ttMain.AutoPopDelay = 10000
        Me.ttMain.InitialDelay = 500
        Me.ttMain.ReshowDelay = 100
        Me.ttMain.ShowAlways = True
        '
        'frmPreProcessing
        '
        Me.AcceptButton = Me.btnPreProcess
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnSelectAll
        Me.ClientSize = New System.Drawing.Size(303, 246)
        Me.Controls.Add(Me.pnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmPreProcessing"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pre-Processing Options"
        Me.gbOptions.ResumeLayout(False)
        Me.gbOptions.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        CType(Me.fswXDFFileExists, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnPreProcess As Button
    Friend WithEvents chkCleanXDFFile As CheckBox
    Friend WithEvents gbOptions As GroupBox
    Friend WithEvents chkShowDataSummary As CheckBox
    Friend WithEvents chkShowVariableInfo As CheckBox
    Friend WithEvents chkShowGeoLocGraph As CheckBox
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents pnlMain As Panel
    Friend WithEvents chkUseExistingXDFFile As CheckBox
    Friend WithEvents chkStatisticsMode As CheckBox
    Friend WithEvents fswXDFFileExists As IO.FileSystemWatcher
    Friend WithEvents lblLoading As Label
    Friend WithEvents tmrXDFExists As Timer
    Friend WithEvents pbLoading As ProgressBar
    Friend WithEvents ttMain As ToolTip
End Class
