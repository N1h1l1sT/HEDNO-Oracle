<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVariableInfoVisualiser
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
        Me.txtVariableInfo = New System.Windows.Forms.TextBox()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.txtVariableInfo)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1078, 704)
        Me.pnlMain.TabIndex = 0
        '
        'txtVariableInfo
        '
        Me.txtVariableInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtVariableInfo.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.txtVariableInfo.Location = New System.Drawing.Point(0, 0)
        Me.txtVariableInfo.Multiline = True
        Me.txtVariableInfo.Name = "txtVariableInfo"
        Me.txtVariableInfo.ReadOnly = True
        Me.txtVariableInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtVariableInfo.Size = New System.Drawing.Size(1078, 704)
        Me.txtVariableInfo.TabIndex = 0
        '
        'frmVariableInfoVisualiser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1078, 704)
        Me.Controls.Add(Me.pnlMain)
        Me.Name = "frmVariableInfoVisualiser"
        Me.Text = "Variable Information Visualiser"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents txtVariableInfo As TextBox
End Class
