<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPleaseWait
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
        Me.pbMarquee = New System.Windows.Forms.ProgressBar()
        Me.lblPleaseWait = New System.Windows.Forms.Label()
        Me.lblInfoText = New System.Windows.Forms.Label()
        Me.tmrPleaseWaitDots = New System.Windows.Forms.Timer(Me.components)
        Me.tmrFlashWindowTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'pbMarquee
        '
        Me.pbMarquee.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbMarquee.Location = New System.Drawing.Point(13, 70)
        Me.pbMarquee.MarqueeAnimationSpeed = 30
        Me.pbMarquee.Name = "pbMarquee"
        Me.pbMarquee.Size = New System.Drawing.Size(351, 23)
        Me.pbMarquee.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pbMarquee.TabIndex = 0
        '
        'lblPleaseWait
        '
        Me.lblPleaseWait.AutoSize = True
        Me.lblPleaseWait.Location = New System.Drawing.Point(10, 9)
        Me.lblPleaseWait.Name = "lblPleaseWait"
        Me.lblPleaseWait.Size = New System.Drawing.Size(64, 13)
        Me.lblPleaseWait.TabIndex = 1
        Me.lblPleaseWait.Text = "Please Wait"
        '
        'lblInfoText
        '
        Me.lblInfoText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInfoText.Location = New System.Drawing.Point(10, 25)
        Me.lblInfoText.Name = "lblInfoText"
        Me.lblInfoText.Size = New System.Drawing.Size(354, 40)
        Me.lblInfoText.TabIndex = 2
        '
        'tmrPleaseWaitDots
        '
        Me.tmrPleaseWaitDots.Interval = 400
        '
        'tmrFlashWindowTimer
        '
        Me.tmrFlashWindowTimer.Interval = 500
        '
        'frmPleaseWait
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(376, 105)
        Me.Controls.Add(Me.lblInfoText)
        Me.Controls.Add(Me.lblPleaseWait)
        Me.Controls.Add(Me.pbMarquee)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmPleaseWait"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmPleaseWait"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbMarquee As System.Windows.Forms.ProgressBar
    Friend WithEvents lblPleaseWait As System.Windows.Forms.Label
    Friend WithEvents lblInfoText As System.Windows.Forms.Label
    Friend WithEvents tmrPleaseWaitDots As System.Windows.Forms.Timer
    Friend WithEvents tmrFlashWindowTimer As System.Windows.Forms.Timer
End Class
