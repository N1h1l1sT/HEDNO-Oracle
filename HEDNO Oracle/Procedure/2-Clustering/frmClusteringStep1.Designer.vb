<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmClusteringStep1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClusteringStep1))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pbLoading = New System.Windows.Forms.ProgressBar()
        Me.lblLoading = New System.Windows.Forms.Label()
        Me.gbOptions = New System.Windows.Forms.GroupBox()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.chkUseExistingXDFFile = New System.Windows.Forms.CheckBox()
        Me.chkShowGeoLocGraph = New System.Windows.Forms.CheckBox()
        Me.chkShowVariableInfo = New System.Windows.Forms.CheckBox()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.chkShowDataSummary = New System.Windows.Forms.CheckBox()
        Me.chkStatisticsMode = New System.Windows.Forms.CheckBox()
        Me.lblSavePath = New System.Windows.Forms.Label()
        Me.lblMaxClusterNum = New System.Windows.Forms.Label()
        Me.txtSavePath = New System.Windows.Forms.TextBox()
        Me.txtMaxClusterNum = New System.Windows.Forms.TextBox()
        Me.chkSaveKMeansModel = New System.Windows.Forms.CheckBox()
        Me.chkCleanXDFFile = New System.Windows.Forms.CheckBox()
        Me.btnClustering1 = New System.Windows.Forms.Button()
        Me.fbdKMeansModel = New System.Windows.Forms.FolderBrowserDialog()
        Me.tmrModelExists = New System.Windows.Forms.Timer(Me.components)
        Me.fswModelExists = New System.IO.FileSystemWatcher()
        Me.ttMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlMain.SuspendLayout()
        Me.gbOptions.SuspendLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        CType(Me.fswModelExists, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pbLoading)
        Me.pnlMain.Controls.Add(Me.lblLoading)
        Me.pnlMain.Controls.Add(Me.gbOptions)
        Me.pnlMain.Controls.Add(Me.btnClustering1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(496, 213)
        Me.pnlMain.TabIndex = 6
        '
        'pbLoading
        '
        Me.pbLoading.Location = New System.Drawing.Point(316, 0)
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
        'gbOptions
        '
        Me.gbOptions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbOptions.Controls.Add(Me.scMain)
        Me.gbOptions.Location = New System.Drawing.Point(12, 12)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(472, 160)
        Me.gbOptions.TabIndex = 2
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Main Options:"
        '
        'scMain
        '
        Me.scMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scMain.Location = New System.Drawing.Point(3, 16)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.chkUseExistingXDFFile)
        Me.scMain.Panel1.Controls.Add(Me.chkShowGeoLocGraph)
        Me.scMain.Panel1.Controls.Add(Me.chkShowVariableInfo)
        Me.scMain.Panel1.Controls.Add(Me.btnSelectAll)
        Me.scMain.Panel1.Controls.Add(Me.chkShowDataSummary)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.chkStatisticsMode)
        Me.scMain.Panel2.Controls.Add(Me.lblSavePath)
        Me.scMain.Panel2.Controls.Add(Me.lblMaxClusterNum)
        Me.scMain.Panel2.Controls.Add(Me.txtSavePath)
        Me.scMain.Panel2.Controls.Add(Me.txtMaxClusterNum)
        Me.scMain.Panel2.Controls.Add(Me.chkSaveKMeansModel)
        Me.scMain.Panel2.Controls.Add(Me.chkCleanXDFFile)
        Me.scMain.Size = New System.Drawing.Size(466, 141)
        Me.scMain.SplitterDistance = 224
        Me.scMain.TabIndex = 10
        '
        'chkUseExistingXDFFile
        '
        Me.chkUseExistingXDFFile.AutoSize = True
        Me.chkUseExistingXDFFile.Location = New System.Drawing.Point(6, 3)
        Me.chkUseExistingXDFFile.Name = "chkUseExistingXDFFile"
        Me.chkUseExistingXDFFile.Size = New System.Drawing.Size(127, 17)
        Me.chkUseExistingXDFFile.TabIndex = 5
        Me.chkUseExistingXDFFile.Text = "Use Existing XDF File"
        Me.ttMain.SetToolTip(Me.chkUseExistingXDFFile, resources.GetString("chkUseExistingXDFFile.ToolTip"))
        Me.chkUseExistingXDFFile.UseVisualStyleBackColor = True
        '
        'chkShowGeoLocGraph
        '
        Me.chkShowGeoLocGraph.AutoSize = True
        Me.chkShowGeoLocGraph.Location = New System.Drawing.Point(6, 72)
        Me.chkShowGeoLocGraph.Name = "chkShowGeoLocGraph"
        Me.chkShowGeoLocGraph.Size = New System.Drawing.Size(170, 17)
        Me.chkShowGeoLocGraph.TabIndex = 2
        Me.chkShowGeoLocGraph.Text = "Show the Geo-Location Graph"
        Me.ttMain.SetToolTip(Me.chkShowGeoLocGraph, "View the Geo-Location Graph as of Clustering Step 1's point of view (Clear Cluste" &
        "rs with colour-coding)")
        Me.chkShowGeoLocGraph.UseVisualStyleBackColor = True
        '
        'chkShowVariableInfo
        '
        Me.chkShowVariableInfo.AutoSize = True
        Me.chkShowVariableInfo.Location = New System.Drawing.Point(6, 49)
        Me.chkShowVariableInfo.Name = "chkShowVariableInfo"
        Me.chkShowVariableInfo.Size = New System.Drawing.Size(149, 17)
        Me.chkShowVariableInfo.TabIndex = 3
        Me.chkShowVariableInfo.Text = "Show Variable Information"
        Me.ttMain.SetToolTip(Me.chkShowVariableInfo, "View Variable Information such as their types and descriptions.")
        Me.chkShowVariableInfo.UseVisualStyleBackColor = True
        '
        'btnSelectAll
        '
        Me.btnSelectAll.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSelectAll.Location = New System.Drawing.Point(6, 95)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(170, 23)
        Me.btnSelectAll.TabIndex = 3
        Me.btnSelectAll.Text = "Select &All"
        Me.ttMain.SetToolTip(Me.btnSelectAll, "Selects all options in the GroupBox")
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'chkShowDataSummary
        '
        Me.chkShowDataSummary.AutoSize = True
        Me.chkShowDataSummary.Location = New System.Drawing.Point(6, 26)
        Me.chkShowDataSummary.Name = "chkShowDataSummary"
        Me.chkShowDataSummary.Size = New System.Drawing.Size(125, 17)
        Me.chkShowDataSummary.TabIndex = 4
        Me.chkShowDataSummary.Text = "Show Data Summary"
        Me.ttMain.SetToolTip(Me.chkShowDataSummary, "View a Data Summary of the clustering dataset, such as Min, Max, Mean, etc.")
        Me.chkShowDataSummary.UseVisualStyleBackColor = True
        '
        'chkStatisticsMode
        '
        Me.chkStatisticsMode.AutoSize = True
        Me.chkStatisticsMode.Checked = True
        Me.chkStatisticsMode.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkStatisticsMode.Location = New System.Drawing.Point(3, 3)
        Me.chkStatisticsMode.Name = "chkStatisticsMode"
        Me.chkStatisticsMode.Size = New System.Drawing.Size(98, 17)
        Me.chkStatisticsMode.TabIndex = 14
        Me.chkStatisticsMode.Text = "Statistics Mode"
        Me.ttMain.SetToolTip(Me.chkStatisticsMode, resources.GetString("chkStatisticsMode.ToolTip"))
        Me.chkStatisticsMode.UseVisualStyleBackColor = True
        '
        'lblSavePath
        '
        Me.lblSavePath.AutoSize = True
        Me.lblSavePath.Enabled = False
        Me.lblSavePath.Location = New System.Drawing.Point(0, 50)
        Me.lblSavePath.Name = "lblSavePath"
        Me.lblSavePath.Size = New System.Drawing.Size(92, 13)
        Me.lblSavePath.TabIndex = 10
        Me.lblSavePath.Text = "Save K-Means at:"
        '
        'lblMaxClusterNum
        '
        Me.lblMaxClusterNum.AutoSize = True
        Me.lblMaxClusterNum.Location = New System.Drawing.Point(0, 118)
        Me.lblMaxClusterNum.Name = "lblMaxClusterNum"
        Me.lblMaxClusterNum.Size = New System.Drawing.Size(116, 13)
        Me.lblMaxClusterNum.TabIndex = 7
        Me.lblMaxClusterNum.Text = "Maximum # of Clusters:"
        '
        'txtSavePath
        '
        Me.txtSavePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSavePath.Enabled = False
        Me.txtSavePath.Location = New System.Drawing.Point(3, 66)
        Me.txtSavePath.Name = "txtSavePath"
        Me.txtSavePath.ReadOnly = True
        Me.txtSavePath.Size = New System.Drawing.Size(229, 20)
        Me.txtSavePath.TabIndex = 9
        Me.ttMain.SetToolTip(Me.txtSavePath, "The path to save the K-Means Model")
        '
        'txtMaxClusterNum
        '
        Me.txtMaxClusterNum.Location = New System.Drawing.Point(122, 115)
        Me.txtMaxClusterNum.Name = "txtMaxClusterNum"
        Me.txtMaxClusterNum.ReadOnly = True
        Me.txtMaxClusterNum.Size = New System.Drawing.Size(51, 20)
        Me.txtMaxClusterNum.TabIndex = 8
        Me.ttMain.SetToolTip(Me.txtMaxClusterNum, resources.GetString("txtMaxClusterNum.ToolTip"))
        '
        'chkSaveKMeansModel
        '
        Me.chkSaveKMeansModel.AutoSize = True
        Me.chkSaveKMeansModel.Location = New System.Drawing.Point(3, 26)
        Me.chkSaveKMeansModel.Name = "chkSaveKMeansModel"
        Me.chkSaveKMeansModel.Size = New System.Drawing.Size(128, 17)
        Me.chkSaveKMeansModel.TabIndex = 9
        Me.chkSaveKMeansModel.Text = "Save K-Means Model"
        Me.ttMain.SetToolTip(Me.chkSaveKMeansModel, "Saves the K-Means Model to the path specified below")
        Me.chkSaveKMeansModel.UseVisualStyleBackColor = True
        '
        'chkCleanXDFFile
        '
        Me.chkCleanXDFFile.AutoSize = True
        Me.chkCleanXDFFile.Location = New System.Drawing.Point(3, 92)
        Me.chkCleanXDFFile.Name = "chkCleanXDFFile"
        Me.chkCleanXDFFile.Size = New System.Drawing.Size(189, 17)
        Me.chkCleanXDFFile.TabIndex = 1
        Me.chkCleanXDFFile.Text = "Clean the XDF file after completion"
        Me.ttMain.SetToolTip(Me.chkCleanXDFFile, "If selected, then the Clustering XDF file is going to be deleted when the form cl" &
        "oses, meaning it is going to be unavailable for further and future use")
        Me.chkCleanXDFFile.UseVisualStyleBackColor = True
        '
        'btnClustering1
        '
        Me.btnClustering1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClustering1.Location = New System.Drawing.Point(12, 178)
        Me.btnClustering1.Name = "btnClustering1"
        Me.btnClustering1.Size = New System.Drawing.Size(472, 23)
        Me.btnClustering1.TabIndex = 0
        Me.btnClustering1.Text = "Commence Clustering Step 1"
        Me.ttMain.SetToolTip(Me.btnClustering1, resources.GetString("btnClustering1.ToolTip"))
        Me.btnClustering1.UseVisualStyleBackColor = True
        '
        'tmrModelExists
        '
        Me.tmrModelExists.Interval = 10
        '
        'fswModelExists
        '
        Me.fswModelExists.EnableRaisingEvents = True
        Me.fswModelExists.SynchronizingObject = Me
        '
        'ttMain
        '
        Me.ttMain.AutoPopDelay = 10000
        Me.ttMain.InitialDelay = 500
        Me.ttMain.ReshowDelay = 100
        Me.ttMain.ShowAlways = True
        '
        'frmClusteringStep1
        '
        Me.AcceptButton = Me.btnClustering1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnSelectAll
        Me.ClientSize = New System.Drawing.Size(496, 213)
        Me.Controls.Add(Me.pnlMain)
        Me.Name = "frmClusteringStep1"
        Me.Text = "Clustering Step 1 Options"
        Me.pnlMain.ResumeLayout(False)
        Me.gbOptions.ResumeLayout(False)
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel1.PerformLayout()
        Me.scMain.Panel2.ResumeLayout(False)
        Me.scMain.Panel2.PerformLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        CType(Me.fswModelExists, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents gbOptions As GroupBox
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents chkUseExistingXDFFile As CheckBox
    Friend WithEvents chkShowDataSummary As CheckBox
    Friend WithEvents chkShowVariableInfo As CheckBox
    Friend WithEvents chkShowGeoLocGraph As CheckBox
    Friend WithEvents btnClustering1 As Button
    Friend WithEvents chkCleanXDFFile As CheckBox
    Friend WithEvents lblMaxClusterNum As Label
    Friend WithEvents txtMaxClusterNum As TextBox
    Friend WithEvents chkSaveKMeansModel As CheckBox
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents lblSavePath As Label
    Friend WithEvents txtSavePath As TextBox
    Friend WithEvents fbdKMeansModel As FolderBrowserDialog
    Friend WithEvents chkStatisticsMode As CheckBox
    Friend WithEvents lblLoading As Label
    Friend WithEvents tmrModelExists As Timer
    Friend WithEvents fswModelExists As IO.FileSystemWatcher
    Friend WithEvents pbLoading As ProgressBar
    Friend WithEvents ttMain As ToolTip
End Class
