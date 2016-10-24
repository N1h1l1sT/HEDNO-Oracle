<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLicenseViewer
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
        Me.btnAgree = New System.Windows.Forms.Button()
        Me.lblXOutOfX = New System.Windows.Forms.Label()
        Me.btnDisagree = New System.Windows.Forms.Button()
        Me.rtbLicenseViewer = New System.Windows.Forms.RichTextBox()
        Me.lblAlreadyAgreedToAll = New System.Windows.Forms.Label()
        Me.btnAgreeToAll = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnAgree
        '
        Me.btnAgree.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAgree.Location = New System.Drawing.Point(466, 629)
        Me.btnAgree.Name = "btnAgree"
        Me.btnAgree.Size = New System.Drawing.Size(147, 23)
        Me.btnAgree.TabIndex = 0
        Me.btnAgree.Text = "I &Agree"
        Me.btnAgree.UseVisualStyleBackColor = True
        '
        'lblXOutOfX
        '
        Me.lblXOutOfX.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblXOutOfX.AutoSize = True
        Me.lblXOutOfX.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblXOutOfX.Location = New System.Drawing.Point(12, 635)
        Me.lblXOutOfX.Name = "lblXOutOfX"
        Me.lblXOutOfX.Size = New System.Drawing.Size(102, 20)
        Me.lblXOutOfX.TabIndex = 1
        Me.lblXOutOfX.Text = "XX out of XX"
        '
        'btnDisagree
        '
        Me.btnDisagree.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnDisagree.Location = New System.Drawing.Point(301, 629)
        Me.btnDisagree.Name = "btnDisagree"
        Me.btnDisagree.Size = New System.Drawing.Size(147, 23)
        Me.btnDisagree.TabIndex = 2
        Me.btnDisagree.Text = "I &Disagree"
        Me.btnDisagree.UseVisualStyleBackColor = True
        '
        'rtbLicenseViewer
        '
        Me.rtbLicenseViewer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbLicenseViewer.Location = New System.Drawing.Point(12, 20)
        Me.rtbLicenseViewer.Name = "rtbLicenseViewer"
        Me.rtbLicenseViewer.Size = New System.Drawing.Size(754, 603)
        Me.rtbLicenseViewer.TabIndex = 3
        Me.rtbLicenseViewer.Text = ""
        '
        'lblAlreadyAgreedToAll
        '
        Me.lblAlreadyAgreedToAll.AutoSize = True
        Me.lblAlreadyAgreedToAll.BackColor = System.Drawing.Color.Chartreuse
        Me.lblAlreadyAgreedToAll.Location = New System.Drawing.Point(16, 4)
        Me.lblAlreadyAgreedToAll.Name = "lblAlreadyAgreedToAll"
        Me.lblAlreadyAgreedToAll.Size = New System.Drawing.Size(205, 13)
        Me.lblAlreadyAgreedToAll.TabIndex = 4
        Me.lblAlreadyAgreedToAll.Text = "*You've already agreed to all the licenses*"
        Me.lblAlreadyAgreedToAll.Visible = False
        '
        'btnAgreeToAll
        '
        Me.btnAgreeToAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAgreeToAll.Location = New System.Drawing.Point(619, 629)
        Me.btnAgreeToAll.Name = "btnAgreeToAll"
        Me.btnAgreeToAll.Size = New System.Drawing.Size(147, 23)
        Me.btnAgreeToAll.TabIndex = 5
        Me.btnAgreeToAll.Text = "&I Agree to all"
        Me.btnAgreeToAll.UseVisualStyleBackColor = True
        '
        'frmLicenseViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(778, 664)
        Me.Controls.Add(Me.btnAgreeToAll)
        Me.Controls.Add(Me.lblAlreadyAgreedToAll)
        Me.Controls.Add(Me.rtbLicenseViewer)
        Me.Controls.Add(Me.btnDisagree)
        Me.Controls.Add(Me.lblXOutOfX)
        Me.Controls.Add(Me.btnAgree)
        Me.Name = "frmLicenseViewer"
        Me.Text = "Licenses"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnAgree As System.Windows.Forms.Button
    Friend WithEvents lblXOutOfX As System.Windows.Forms.Label
    Friend WithEvents btnDisagree As System.Windows.Forms.Button
    Friend WithEvents rtbLicenseViewer As System.Windows.Forms.RichTextBox
    Friend WithEvents lblAlreadyAgreedToAll As Label
    Friend WithEvents btnAgreeToAll As Button
End Class
