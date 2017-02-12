<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDataGridView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDataGridView))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.mniFileMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mniPrintTool = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniPrintPreview = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mniExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.lblSum = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblAverage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblSelectedCells = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblSelectedColumns = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblSelectedRows = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblColumnsCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblRowsCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblCurColumn = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblCurRow = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sfdExport = New System.Windows.Forms.SaveFileDialog()
        Me.MyPrintDocument = New System.Drawing.Printing.PrintDocument()
        Me.dgvDataGrid = New System.Windows.Forms.DataGridView()
        Me.cmsColumnRen = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiColumnName = New System.Windows.Forms.ToolStripMenuItem()
        Me.tstxtColumnRen = New System.Windows.Forms.ToolStripTextBox()
        Me.mnuMain.SuspendLayout()
        Me.ssMain.SuspendLayout()
        CType(Me.dgvDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsColumnRen.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuMain
        '
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniFileMenu})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(874, 24)
        Me.mnuMain.TabIndex = 1
        Me.mnuMain.Text = "Menu"
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
        'ssMain
        '
        Me.ssMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblSum, Me.lblAverage, Me.lblSelectedCells, Me.lblSelectedColumns, Me.lblSelectedRows, Me.lblColumnsCount, Me.lblRowsCount, Me.lblCurColumn, Me.lblCurRow})
        Me.ssMain.Location = New System.Drawing.Point(0, 520)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(874, 22)
        Me.ssMain.TabIndex = 2
        Me.ssMain.Text = "StatusStrip1"
        '
        'lblSum
        '
        Me.lblSum.Name = "lblSum"
        Me.lblSum.Size = New System.Drawing.Size(37, 17)
        Me.lblSum.Text = "Sum: "
        '
        'lblAverage
        '
        Me.lblAverage.Name = "lblAverage"
        Me.lblAverage.Size = New System.Drawing.Size(56, 17)
        Me.lblAverage.Text = "Average: "
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
        'sfdExport
        '
        Me.sfdExport.DefaultExt = "csv"
        '
        'MyPrintDocument
        '
        '
        'dgvDataGrid
        '
        Me.dgvDataGrid.AllowUserToAddRows = False
        Me.dgvDataGrid.AllowUserToDeleteRows = False
        Me.dgvDataGrid.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.dgvDataGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDataGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDataGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDataGrid.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvDataGrid.Location = New System.Drawing.Point(0, 27)
        Me.dgvDataGrid.Name = "dgvDataGrid"
        Me.dgvDataGrid.ReadOnly = True
        Me.dgvDataGrid.RowHeadersWidth = 15
        Me.dgvDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvDataGrid.Size = New System.Drawing.Size(874, 490)
        Me.dgvDataGrid.TabIndex = 3
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
        'frmDataGridView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(874, 542)
        Me.Controls.Add(Me.dgvDataGrid)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.mnuMain)
        Me.MainMenuStrip = Me.mnuMain
        Me.Name = "frmDataGridView"
        Me.Text = "Report"
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        CType(Me.dgvDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsColumnRen.ResumeLayout(False)
        Me.cmsColumnRen.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents mniFileMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniSaveAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mniPrintTool As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniPrintPreview As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mniExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ssMain As System.Windows.Forms.StatusStrip
    Friend WithEvents sfdExport As System.Windows.Forms.SaveFileDialog
    Friend WithEvents lblSum As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblSelectedCells As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblSelectedRows As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MyPrintDocument As System.Drawing.Printing.PrintDocument
    Friend WithEvents lblRowsCount As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblSelectedColumns As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblCurRow As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblCurColumn As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents dgvDataGrid As System.Windows.Forms.DataGridView
    Friend WithEvents cmsColumnRen As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmiColumnName As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tstxtColumnRen As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents lblColumnsCount As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblAverage As System.Windows.Forms.ToolStripStatusLabel
End Class
