<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSplashScreen
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
    Friend WithEvents lblApplicationTitle As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents MainLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DetailsLayoutPanel As System.Windows.Forms.TableLayoutPanel

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MainLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.DetailsLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.lblApplicationTitle = New System.Windows.Forms.Label()
        Me.lblLicense = New System.Windows.Forms.Label()
        Me.MainLayoutPanel.SuspendLayout()
        Me.DetailsLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainLayoutPanel
        '
        Me.MainLayoutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MainLayoutPanel.ColumnCount = 2
        Me.MainLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 243.0!))
        Me.MainLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 253.0!))
        Me.MainLayoutPanel.Controls.Add(Me.DetailsLayoutPanel, 1, 1)
        Me.MainLayoutPanel.Controls.Add(Me.lblApplicationTitle, 1, 0)
        Me.MainLayoutPanel.Controls.Add(Me.lblLicense, 0, 1)
        Me.MainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.MainLayoutPanel.Name = "MainLayoutPanel"
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 218.0!))
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38.0!))
        Me.MainLayoutPanel.Size = New System.Drawing.Size(496, 303)
        Me.MainLayoutPanel.TabIndex = 0
        '
        'DetailsLayoutPanel
        '
        Me.DetailsLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.DetailsLayoutPanel.BackColor = System.Drawing.Color.Transparent
        Me.DetailsLayoutPanel.ColumnCount = 1
        Me.DetailsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 247.0!))
        Me.DetailsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142.0!))
        Me.DetailsLayoutPanel.Controls.Add(Me.lblVersion, 0, 0)
        Me.DetailsLayoutPanel.Controls.Add(Me.lblCopyright, 0, 1)
        Me.DetailsLayoutPanel.Location = New System.Drawing.Point(246, 221)
        Me.DetailsLayoutPanel.Name = "DetailsLayoutPanel"
        Me.DetailsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.DetailsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.DetailsLayoutPanel.Size = New System.Drawing.Size(247, 79)
        Me.DetailsLayoutPanel.TabIndex = 1
        '
        'lblVersion
        '
        Me.lblVersion.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblVersion.Location = New System.Drawing.Point(3, 9)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(241, 20)
        Me.lblVersion.TabIndex = 1
        Me.lblVersion.Text = "Version {0}.{1:00}"
        '
        'lblCopyright
        '
        Me.lblCopyright.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblCopyright.BackColor = System.Drawing.Color.Transparent
        Me.lblCopyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyright.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCopyright.Location = New System.Drawing.Point(3, 39)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(241, 40)
        Me.lblCopyright.TabIndex = 2
        Me.lblCopyright.Text = "Copyright"
        '
        'lblApplicationTitle
        '
        Me.lblApplicationTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblApplicationTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblApplicationTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApplicationTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblApplicationTitle.Location = New System.Drawing.Point(246, 3)
        Me.lblApplicationTitle.Name = "lblApplicationTitle"
        Me.lblApplicationTitle.Size = New System.Drawing.Size(247, 212)
        Me.lblApplicationTitle.TabIndex = 0
        Me.lblApplicationTitle.Text = "ApplicationTitle"
        Me.lblApplicationTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'lblLicense
        '
        Me.lblLicense.AutoSize = True
        Me.lblLicense.BackColor = System.Drawing.Color.Transparent
        Me.lblLicense.Location = New System.Drawing.Point(3, 218)
        Me.lblLicense.Name = "lblLicense"
        Me.lblLicense.Size = New System.Drawing.Size(68, 13)
        Me.lblLicense.TabIndex = 2
        Me.lblLicense.Text = "Licensed to: "
        Me.lblLicense.Visible = False
        '
        'frmSplashScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(496, 303)
        Me.ControlBox = False
        Me.Controls.Add(Me.MainLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSplashScreen"
        Me.Opacity = 0.83R
        Me.ShowInTaskbar = False
        Me.TopMost = True
        Me.MainLayoutPanel.ResumeLayout(False)
        Me.MainLayoutPanel.PerformLayout()
        Me.DetailsLayoutPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblLicense As System.Windows.Forms.Label

End Class
