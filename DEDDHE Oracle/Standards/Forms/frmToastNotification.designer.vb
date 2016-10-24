<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmToastNotification
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
        Me.messageLabel = New System.Windows.Forms.Label()
        Me.lifeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.tltMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'messageLabel
        '
        Me.messageLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.messageLabel.Location = New System.Drawing.Point(0, 0)
        Me.messageLabel.Name = "messageLabel"
        Me.messageLabel.Size = New System.Drawing.Size(292, 118)
        Me.messageLabel.TabIndex = 0
        Me.messageLabel.Text = "Message will appear here"
        Me.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lifeTimer
        '
        '
        'tltMain
        '
        Me.tltMain.AutoPopDelay = 25000
        Me.tltMain.InitialDelay = 500
        Me.tltMain.IsBalloon = True
        Me.tltMain.ReshowDelay = 100
        Me.tltMain.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'frmToastNotification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 118)
        Me.Controls.Add(Me.messageLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmToastNotification"
        Me.Text = "Toast Form"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents messageLabel As System.Windows.Forms.Label
    Private WithEvents lifeTimer As System.Windows.Forms.Timer
    Friend WithEvents tltMain As ToolTip
End Class
