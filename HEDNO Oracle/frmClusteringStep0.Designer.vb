<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmClusteringStep0
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
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.gbOptions = New System.Windows.Forms.GroupBox()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.chkUseExistingXDFFile = New System.Windows.Forms.CheckBox()
        Me.chkShowDataSummary = New System.Windows.Forms.CheckBox()
        Me.chkShowVariableInfo = New System.Windows.Forms.CheckBox()
        Me.chkShowGeoLocGraph = New System.Windows.Forms.CheckBox()
        Me.btnClustering0 = New System.Windows.Forms.Button()
        Me.chkCleanXDFFile = New System.Windows.Forms.CheckBox()
        Me.pnlMain.SuspendLayout()
        Me.gbOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.gbOptions)
        Me.pnlMain.Controls.Add(Me.btnClustering0)
        Me.pnlMain.Controls.Add(Me.chkCleanXDFFile)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(283, 246)
        Me.pnlMain.TabIndex = 5
        '
        'gbOptions
        '
        Me.gbOptions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbOptions.Controls.Add(Me.btnSelectAll)
        Me.gbOptions.Controls.Add(Me.chkUseExistingXDFFile)
        Me.gbOptions.Controls.Add(Me.chkShowDataSummary)
        Me.gbOptions.Controls.Add(Me.chkShowVariableInfo)
        Me.gbOptions.Controls.Add(Me.chkShowGeoLocGraph)
        Me.gbOptions.Location = New System.Drawing.Point(12, 12)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(259, 142)
        Me.gbOptions.TabIndex = 2
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Main Options:"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelectAll.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSelectAll.Location = New System.Drawing.Point(6, 113)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(170, 23)
        Me.btnSelectAll.TabIndex = 3
        Me.btnSelectAll.Text = "Select &All"
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
        Me.chkShowDataSummary.UseVisualStyleBackColor = True
        '
        'chkShowVariableInfo
        '
        Me.chkShowVariableInfo.AutoSize = True
        Me.chkShowVariableInfo.Location = New System.Drawing.Point(6, 65)
        Me.chkShowVariableInfo.Name = "chkShowVariableInfo"
        Me.chkShowVariableInfo.Size = New System.Drawing.Size(149, 17)
        Me.chkShowVariableInfo.TabIndex = 3
        Me.chkShowVariableInfo.Text = "Show Variable Information"
        Me.chkShowVariableInfo.UseVisualStyleBackColor = True
        '
        'chkShowGeoLocGraph
        '
        Me.chkShowGeoLocGraph.AutoSize = True
        Me.chkShowGeoLocGraph.Location = New System.Drawing.Point(6, 88)
        Me.chkShowGeoLocGraph.Name = "chkShowGeoLocGraph"
        Me.chkShowGeoLocGraph.Size = New System.Drawing.Size(170, 17)
        Me.chkShowGeoLocGraph.TabIndex = 2
        Me.chkShowGeoLocGraph.Text = "Show the Geo-Location Graph"
        Me.chkShowGeoLocGraph.UseVisualStyleBackColor = True
        '
        'btnClustering0
        '
        Me.btnClustering0.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClustering0.Location = New System.Drawing.Point(12, 211)
        Me.btnClustering0.Name = "btnClustering0"
        Me.btnClustering0.Size = New System.Drawing.Size(259, 23)
        Me.btnClustering0.TabIndex = 0
        Me.btnClustering0.Text = "Commence Clustering Step 0"
        Me.btnClustering0.UseVisualStyleBackColor = True
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
        Me.chkCleanXDFFile.UseVisualStyleBackColor = True
        '
        'frmClusteringStep0
        '
        Me.AcceptButton = Me.btnClustering0
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnSelectAll
        Me.ClientSize = New System.Drawing.Size(283, 246)
        Me.Controls.Add(Me.pnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmClusteringStep0"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Clustering Step 0 Options"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.gbOptions.ResumeLayout(False)
        Me.gbOptions.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents gbOptions As GroupBox
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents chkUseExistingXDFFile As CheckBox
    Friend WithEvents chkShowDataSummary As CheckBox
    Friend WithEvents chkShowVariableInfo As CheckBox
    Friend WithEvents chkShowGeoLocGraph As CheckBox
    Friend WithEvents chkCleanXDFFile As CheckBox
    Friend WithEvents btnClustering0 As Button
End Class
