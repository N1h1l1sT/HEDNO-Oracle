<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHelp
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.scHelp = New System.Windows.Forms.SplitContainer()
        Me.lsbCommands = New System.Windows.Forms.ListBox()
        Me.wbUseage = New System.Windows.Forms.WebBrowser()
        Me.lblCommands = New System.Windows.Forms.Label()
        Me.lblUsage = New System.Windows.Forms.Label()
        CType(Me.scHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scHelp.Panel1.SuspendLayout()
        Me.scHelp.Panel2.SuspendLayout()
        Me.scHelp.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(12, 413)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(570, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'scHelp
        '
        Me.scHelp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scHelp.Location = New System.Drawing.Point(12, 29)
        Me.scHelp.Name = "scHelp"
        '
        'scHelp.Panel1
        '
        Me.scHelp.Panel1.Controls.Add(Me.lsbCommands)
        '
        'scHelp.Panel2
        '
        Me.scHelp.Panel2.Controls.Add(Me.wbUseage)
        Me.scHelp.Size = New System.Drawing.Size(570, 378)
        Me.scHelp.SplitterDistance = 190
        Me.scHelp.TabIndex = 3
        '
        'lsbCommands
        '
        Me.lsbCommands.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsbCommands.FormattingEnabled = True
        Me.lsbCommands.Location = New System.Drawing.Point(0, 0)
        Me.lsbCommands.Name = "lsbCommands"
        Me.lsbCommands.Size = New System.Drawing.Size(190, 378)
        Me.lsbCommands.TabIndex = 0
        '
        'wbUseage
        '
        Me.wbUseage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbUseage.Location = New System.Drawing.Point(0, 0)
        Me.wbUseage.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbUseage.Name = "wbUseage"
        Me.wbUseage.Size = New System.Drawing.Size(376, 378)
        Me.wbUseage.TabIndex = 0
        '
        'lblCommands
        '
        Me.lblCommands.AutoSize = True
        Me.lblCommands.BackColor = System.Drawing.Color.Transparent
        Me.lblCommands.Location = New System.Drawing.Point(13, 13)
        Me.lblCommands.Name = "lblCommands"
        Me.lblCommands.Size = New System.Drawing.Size(62, 13)
        Me.lblCommands.TabIndex = 4
        Me.lblCommands.Text = "Commands:"
        '
        'lblUsage
        '
        Me.lblUsage.AutoSize = True
        Me.lblUsage.BackColor = System.Drawing.Color.Transparent
        Me.lblUsage.Location = New System.Drawing.Point(206, 9)
        Me.lblUsage.Name = "lblUsage"
        Me.lblUsage.Size = New System.Drawing.Size(41, 13)
        Me.lblUsage.TabIndex = 5
        Me.lblUsage.Text = "Usage:"
        '
        'frmHelp
        '
        Me.AcceptButton = Me.btnClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 448)
        Me.Controls.Add(Me.lblUsage)
        Me.Controls.Add(Me.lblCommands)
        Me.Controls.Add(Me.scHelp)
        Me.Controls.Add(Me.btnClose)
        Me.Name = "frmHelp"
        Me.Text = "frmHelp"
        Me.scHelp.Panel1.ResumeLayout(False)
        Me.scHelp.Panel2.ResumeLayout(False)
        CType(Me.scHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scHelp.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents scHelp As System.Windows.Forms.SplitContainer
    Friend WithEvents lsbCommands As System.Windows.Forms.ListBox
    Friend WithEvents lblCommands As System.Windows.Forms.Label
    Friend WithEvents lblUsage As System.Windows.Forms.Label
    Friend WithEvents wbUseage As System.Windows.Forms.WebBrowser
End Class
