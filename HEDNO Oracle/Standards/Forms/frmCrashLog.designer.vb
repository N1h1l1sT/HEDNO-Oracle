<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCrashLog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCrashLog))
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.btnSendCrashLog = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtWhatCausedTheException = New System.Windows.Forms.TextBox()
        Me.lblWhatCausedTheException = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblInfo
        '
        Me.lblInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblInfo.Location = New System.Drawing.Point(12, 9)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(321, 94)
        Me.lblInfo.TabIndex = 0
        Me.lblInfo.Text = "It seems that the programme crashed!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please push ""Send CrashLog"" so that the dev" &
    "elopers of the programme can fix whatever caused this exception."
        '
        'btnSendCrashLog
        '
        Me.btnSendCrashLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSendCrashLog.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnSendCrashLog.Location = New System.Drawing.Point(174, 255)
        Me.btnSendCrashLog.Name = "btnSendCrashLog"
        Me.btnSendCrashLog.Size = New System.Drawing.Size(159, 36)
        Me.btnSendCrashLog.TabIndex = 1
        Me.btnSendCrashLog.Text = "Send CrashLog"
        Me.btnSendCrashLog.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(12, 268)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(83, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtWhatCausedTheException
        '
        Me.txtWhatCausedTheException.AcceptsReturn = True
        Me.txtWhatCausedTheException.AcceptsTab = True
        Me.txtWhatCausedTheException.Location = New System.Drawing.Point(15, 106)
        Me.txtWhatCausedTheException.Multiline = True
        Me.txtWhatCausedTheException.Name = "txtWhatCausedTheException"
        Me.txtWhatCausedTheException.Size = New System.Drawing.Size(318, 143)
        Me.txtWhatCausedTheException.TabIndex = 3
        '
        'lblWhatCausedTheException
        '
        Me.lblWhatCausedTheException.AutoSize = True
        Me.lblWhatCausedTheException.Location = New System.Drawing.Point(15, 87)
        Me.lblWhatCausedTheException.Name = "lblWhatCausedTheException"
        Me.lblWhatCausedTheException.Size = New System.Drawing.Size(196, 13)
        Me.lblWhatCausedTheException.TabIndex = 4
        Me.lblWhatCausedTheException.Text = "How can this exception be reproduced?"
        '
        'frmCrashLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(345, 303)
        Me.Controls.Add(Me.lblWhatCausedTheException)
        Me.Controls.Add(Me.txtWhatCausedTheException)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSendCrashLog)
        Me.Controls.Add(Me.lblInfo)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCrashLog"
        Me.Text = "Oops! The programme Crushed"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents btnSendCrashLog As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtWhatCausedTheException As TextBox
    Friend WithEvents lblWhatCausedTheException As Label
End Class
