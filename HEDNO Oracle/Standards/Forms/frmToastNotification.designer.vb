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
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.tmrLifeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.tltMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.pbIcon = New System.Windows.Forms.PictureBox()
        CType(Me.pbIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMessage
        '
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblMessage.Location = New System.Drawing.Point(0, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(326, 150)
        Me.lblMessage.TabIndex = 0
        Me.lblMessage.Text = "Message will appear here"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tmrLifeTimer
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
        'pbIcon
        '
        Me.pbIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pbIcon.Dock = System.Windows.Forms.DockStyle.Left
        Me.pbIcon.Location = New System.Drawing.Point(0, 0)
        Me.pbIcon.Name = "pbIcon"
        Me.pbIcon.Size = New System.Drawing.Size(76, 150)
        Me.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbIcon.TabIndex = 2
        Me.pbIcon.TabStop = False
        '
        'frmToastNotification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(326, 150)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.pbIcon)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmToastNotification"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Toast Form"
        Me.TopMost = True
        CType(Me.pbIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents lblMessage As System.Windows.Forms.Label
    Private WithEvents tmrLifeTimer As System.Windows.Forms.Timer
    Friend WithEvents tltMain As ToolTip
    Friend WithEvents pbIcon As PictureBox
End Class
