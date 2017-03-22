<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDataSummaryVisualiser
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDataSummaryVisualiser))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.dgvDataSummary = New System.Windows.Forms.DataGridView()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.lblSelectedCells = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblSelectedColumns = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblSelectedRows = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblColumnsCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblRowsCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblCurColumn = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblCurRow = New System.Windows.Forms.ToolStripStatusLabel()
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.mniFileMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mniPrintTool = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniPrintPreview = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mniExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsColumnRen = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiColumnName = New System.Windows.Forms.ToolStripMenuItem()
        Me.tstxtColumnRen = New System.Windows.Forms.ToolStripTextBox()
        Me.MyPrintDocument = New System.Drawing.Printing.PrintDocument()
        Me.sfdExport = New System.Windows.Forms.SaveFileDialog()
        Me.pnlMain.SuspendLayout()
        CType(Me.dgvDataSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ssMain.SuspendLayout()
        Me.mnuMain.SuspendLayout()
        Me.cmsColumnRen.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.dgvDataSummary)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(649, 657)
        Me.pnlMain.TabIndex = 0
        '
        'dgvDataSummary
        '
        Me.dgvDataSummary.AllowUserToAddRows = False
        Me.dgvDataSummary.AllowUserToDeleteRows = False
        Me.dgvDataSummary.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDataSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvDataSummary.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDataSummary.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDataSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDataSummary.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvDataSummary.Location = New System.Drawing.Point(0, 0)
        Me.dgvDataSummary.Name = "dgvDataSummary"
        Me.dgvDataSummary.ReadOnly = True
        Me.dgvDataSummary.RowHeadersWidth = 50
        Me.dgvDataSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvDataSummary.Size = New System.Drawing.Size(649, 654)
        Me.dgvDataSummary.TabIndex = 0
        '
        'ssMain
        '
        Me.ssMain.BackColor = System.Drawing.Color.LightGray
        Me.ssMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblSelectedCells, Me.lblSelectedColumns, Me.lblSelectedRows, Me.lblColumnsCount, Me.lblRowsCount, Me.lblCurColumn, Me.lblCurRow})
        Me.ssMain.Location = New System.Drawing.Point(0, 657)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(649, 22)
        Me.ssMain.TabIndex = 3
        '
        'lblSelectedCells
        '
        Me.lblSelectedCells.Name = "lblSelectedCells"
        Me.lblSelectedCells.Size = New System.Drawing.Size(85, 17)
        Me.lblSelectedCells.Text = "Cells Selected: "
        '
        'lblSelectedColumns
        '
        Me.lblSelectedColumns.Name = "lblSelectedColumns"
        Me.lblSelectedColumns.Size = New System.Drawing.Size(108, 17)
        Me.lblSelectedColumns.Text = "Selected Columns: "
        '
        'lblSelectedRows
        '
        Me.lblSelectedRows.Name = "lblSelectedRows"
        Me.lblSelectedRows.Size = New System.Drawing.Size(88, 17)
        Me.lblSelectedRows.Text = "Selected Rows: "
        '
        'lblColumnsCount
        '
        Me.lblColumnsCount.Name = "lblColumnsCount"
        Me.lblColumnsCount.Size = New System.Drawing.Size(97, 17)
        Me.lblColumnsCount.Text = "Columns Count: "
        '
        'lblRowsCount
        '
        Me.lblRowsCount.Name = "lblRowsCount"
        Me.lblRowsCount.Size = New System.Drawing.Size(77, 17)
        Me.lblRowsCount.Text = "Rows Count: "
        '
        'lblCurColumn
        '
        Me.lblCurColumn.Name = "lblCurColumn"
        Me.lblCurColumn.Size = New System.Drawing.Size(99, 17)
        Me.lblCurColumn.Text = "Current Column: "
        '
        'lblCurRow
        '
        Me.lblCurRow.Name = "lblCurRow"
        Me.lblCurRow.Size = New System.Drawing.Size(79, 17)
        Me.lblCurRow.Text = "Current Row: "
        '
        'mnuMain
        '
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniFileMenu})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(764, 24)
        Me.mnuMain.TabIndex = 4
        Me.mnuMain.Text = "Menu"
        Me.mnuMain.Visible = False
        '
        'mniFileMenu
        '
        Me.mniFileMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniSave, Me.mniSaveAs, Me.toolStripSeparator1, Me.mniPrintTool, Me.mniPrintPreview, Me.toolStripSeparator2, Me.mniExit})
        Me.mniFileMenu.Name = "mniFileMenu"
        Me.mniFileMenu.Size = New System.Drawing.Size(37, 20)
        Me.mniFileMenu.Text = "&File"
        '
        'mniSave
        '
        Me.mniSave.Image = CType(resources.GetObject("mniSave.Image"), System.Drawing.Image)
        Me.mniSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mniSave.Name = "mniSave"
        Me.mniSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mniSave.Size = New System.Drawing.Size(216, 22)
        Me.mniSave.Text = "&Save"
        '
        'mniSaveAs
        '
        Me.mniSaveAs.Name = "mniSaveAs"
        Me.mniSaveAs.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mniSaveAs.Size = New System.Drawing.Size(216, 22)
        Me.mniSaveAs.Text = "Save &As"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(213, 6)
        '
        'mniPrintTool
        '
        Me.mniPrintTool.Image = CType(resources.GetObject("mniPrintTool.Image"), System.Drawing.Image)
        Me.mniPrintTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mniPrintTool.Name = "mniPrintTool"
        Me.mniPrintTool.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.mniPrintTool.Size = New System.Drawing.Size(216, 22)
        Me.mniPrintTool.Text = "&Print"
        '
        'mniPrintPreview
        '
        Me.mniPrintPreview.Image = CType(resources.GetObject("mniPrintPreview.Image"), System.Drawing.Image)
        Me.mniPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mniPrintPreview.Name = "mniPrintPreview"
        Me.mniPrintPreview.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.mniPrintPreview.Size = New System.Drawing.Size(216, 22)
        Me.mniPrintPreview.Text = "Print Pre&view"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(213, 6)
        '
        'mniExit
        '
        Me.mniExit.Name = "mniExit"
        Me.mniExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.mniExit.Size = New System.Drawing.Size(216, 22)
        Me.mniExit.Text = "E&xit"
        '
        'cmsColumnRen
        '
        Me.cmsColumnRen.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiColumnName, Me.tstxtColumnRen})
        Me.cmsColumnRen.Name = "cmsColumnRen"
        Me.cmsColumnRen.Size = New System.Drawing.Size(161, 49)
        Me.cmsColumnRen.Text = "&Rename"
        '
        'tsmiColumnName
        '
        Me.tsmiColumnName.Name = "tsmiColumnName"
        Me.tsmiColumnName.Size = New System.Drawing.Size(160, 22)
        Me.tsmiColumnName.Text = "Column Name:"
        '
        'tstxtColumnRen
        '
        Me.tstxtColumnRen.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.tstxtColumnRen.Name = "tstxtColumnRen"
        Me.tstxtColumnRen.Size = New System.Drawing.Size(100, 21)
        '
        'MyPrintDocument
        '
        '
        'sfdExport
        '
        Me.sfdExport.DefaultExt = "csv"
        '
        'frmDataSummaryVisualiser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(649, 679)
        Me.Controls.Add(Me.mnuMain)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.ssMain)
        Me.Name = "frmDataSummaryVisualiser"
        Me.Text = "Data Summary"
        Me.pnlMain.ResumeLayout(False)
        CType(Me.dgvDataSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.cmsColumnRen.ResumeLayout(False)
        Me.cmsColumnRen.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents dgvDataSummary As DataGridView
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents lblSelectedCells As ToolStripStatusLabel
    Friend WithEvents lblSelectedColumns As ToolStripStatusLabel
    Friend WithEvents lblSelectedRows As ToolStripStatusLabel
    Friend WithEvents lblColumnsCount As ToolStripStatusLabel
    Friend WithEvents lblRowsCount As ToolStripStatusLabel
    Friend WithEvents lblCurColumn As ToolStripStatusLabel
    Friend WithEvents lblCurRow As ToolStripStatusLabel
    Friend WithEvents mnuMain As MenuStrip
    Friend WithEvents mniFileMenu As ToolStripMenuItem
    Friend WithEvents mniSave As ToolStripMenuItem
    Friend WithEvents mniSaveAs As ToolStripMenuItem
    Friend WithEvents toolStripSeparator1 As ToolStripSeparator
    Friend WithEvents mniPrintTool As ToolStripMenuItem
    Friend WithEvents mniPrintPreview As ToolStripMenuItem
    Friend WithEvents toolStripSeparator2 As ToolStripSeparator
    Friend WithEvents mniExit As ToolStripMenuItem
    Friend WithEvents cmsColumnRen As ContextMenuStrip
    Friend WithEvents tsmiColumnName As ToolStripMenuItem
    Friend WithEvents tstxtColumnRen As ToolStripTextBox
    Friend WithEvents MyPrintDocument As Printing.PrintDocument
    Friend WithEvents sfdExport As SaveFileDialog
End Class
