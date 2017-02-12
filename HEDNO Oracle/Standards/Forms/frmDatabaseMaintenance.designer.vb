<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDatabaseMaintenance
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
        Me.gbMaintenanceOptions = New System.Windows.Forms.GroupBox()
        Me.lblConnectionString = New System.Windows.Forms.Label()
        Me.txtConnectionString = New System.Windows.Forms.TextBox()
        Me.btnCompactDB = New System.Windows.Forms.Button()
        Me.btnClearAll = New System.Windows.Forms.Button()
        Me.btnClearSingleTable = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.tltDatabaseMaintenance = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbMaintenanceOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbMaintenanceOptions
        '
        Me.gbMaintenanceOptions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbMaintenanceOptions.BackColor = System.Drawing.Color.Transparent
        Me.gbMaintenanceOptions.Controls.Add(Me.lblConnectionString)
        Me.gbMaintenanceOptions.Controls.Add(Me.txtConnectionString)
        Me.gbMaintenanceOptions.Controls.Add(Me.btnCompactDB)
        Me.gbMaintenanceOptions.Controls.Add(Me.btnClearAll)
        Me.gbMaintenanceOptions.Controls.Add(Me.btnClearSingleTable)
        Me.gbMaintenanceOptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.gbMaintenanceOptions.Location = New System.Drawing.Point(12, 25)
        Me.gbMaintenanceOptions.Name = "gbMaintenanceOptions"
        Me.gbMaintenanceOptions.Size = New System.Drawing.Size(365, 205)
        Me.gbMaintenanceOptions.TabIndex = 0
        Me.gbMaintenanceOptions.TabStop = False
        Me.gbMaintenanceOptions.Text = "Maintenance Options"
        '
        'lblConnectionString
        '
        Me.lblConnectionString.AutoSize = True
        Me.lblConnectionString.Location = New System.Drawing.Point(6, 26)
        Me.lblConnectionString.Name = "lblConnectionString"
        Me.lblConnectionString.Size = New System.Drawing.Size(112, 13)
        Me.lblConnectionString.TabIndex = 6
        Me.lblConnectionString.Text = "Connection String:"
        '
        'txtConnectionString
        '
        Me.txtConnectionString.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtConnectionString.Location = New System.Drawing.Point(6, 49)
        Me.txtConnectionString.Name = "txtConnectionString"
        Me.txtConnectionString.ReadOnly = True
        Me.txtConnectionString.Size = New System.Drawing.Size(353, 20)
        Me.txtConnectionString.TabIndex = 5
        Me.txtConnectionString.Text = "{ConnectionString}"
        '
        'btnCompactDB
        '
        Me.btnCompactDB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCompactDB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCompactDB.Location = New System.Drawing.Point(6, 89)
        Me.btnCompactDB.Name = "btnCompactDB"
        Me.btnCompactDB.Size = New System.Drawing.Size(353, 30)
        Me.btnCompactDB.TabIndex = 4
        Me.btnCompactDB.Text = "Compact Database"
        Me.btnCompactDB.UseVisualStyleBackColor = True
        '
        'btnClearAll
        '
        Me.btnClearAll.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearAll.Location = New System.Drawing.Point(6, 125)
        Me.btnClearAll.Name = "btnClearAll"
        Me.btnClearAll.Size = New System.Drawing.Size(353, 30)
        Me.btnClearAll.TabIndex = 3
        Me.btnClearAll.Text = "Clear &All Tables"
        Me.btnClearAll.UseVisualStyleBackColor = True
        '
        'btnClearSingleTable
        '
        Me.btnClearSingleTable.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearSingleTable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSingleTable.Location = New System.Drawing.Point(6, 161)
        Me.btnClearSingleTable.Name = "btnClearSingleTable"
        Me.btnClearSingleTable.Size = New System.Drawing.Size(353, 30)
        Me.btnClearSingleTable.TabIndex = 0
        Me.btnClearSingleTable.Text = "Clear a &Single Table"
        Me.btnClearSingleTable.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnExit.Location = New System.Drawing.Point(12, 249)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(365, 23)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "E&xit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'tltDatabaseMaintenance
        '
        Me.tltDatabaseMaintenance.IsBalloon = True
        Me.tltDatabaseMaintenance.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.tltDatabaseMaintenance.ToolTipTitle = "Tip:"
        '
        'frmDatabaseMaintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(389, 293)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.gbMaintenanceOptions)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "frmDatabaseMaintenance"
        Me.Text = "Database Maintenance"
        Me.gbMaintenanceOptions.ResumeLayout(False)
        Me.gbMaintenanceOptions.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbMaintenanceOptions As System.Windows.Forms.GroupBox
    Friend WithEvents btnCompactDB As System.Windows.Forms.Button
    Friend WithEvents btnClearAll As System.Windows.Forms.Button
    Friend WithEvents btnClearSingleTable As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents tltDatabaseMaintenance As System.Windows.Forms.ToolTip
    Friend WithEvents lblConnectionString As Label
    Friend WithEvents txtConnectionString As TextBox
End Class
