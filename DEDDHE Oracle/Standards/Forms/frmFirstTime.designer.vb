<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFirstTime
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
        Me.btnNext = New System.Windows.Forms.Button()
        Me.tmrNext = New System.Windows.Forms.Timer(Me.components)
        Me.wbWelcome = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Enabled = False
        Me.btnNext.Location = New System.Drawing.Point(12, 597)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(724, 23)
        Me.btnNext.TabIndex = 0
        Me.btnNext.Text = "&Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'tmrNext
        '
        Me.tmrNext.Interval = 1300
        '
        'wbWelcome
        '
        Me.wbWelcome.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.wbWelcome.Location = New System.Drawing.Point(12, 12)
        Me.wbWelcome.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbWelcome.Name = "wbWelcome"
        Me.wbWelcome.Size = New System.Drawing.Size(724, 579)
        Me.wbWelcome.TabIndex = 1
        '
        'frmFirstTime
        '
        Me.AcceptButton = Me.btnNext
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(748, 632)
        Me.Controls.Add(Me.wbWelcome)
        Me.Controls.Add(Me.btnNext)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmFirstTime"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Welcome"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents tmrNext As System.Windows.Forms.Timer
    Friend WithEvents wbWelcome As System.Windows.Forms.WebBrowser
End Class
