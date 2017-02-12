<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPresentation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPresentation))
        Me.txtInfo = New System.Windows.Forms.TextBox()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.picInfo = New System.Windows.Forms.PictureBox()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.scTextMedia = New System.Windows.Forms.SplitContainer()
        Me.wbInfo = New System.Windows.Forms.WebBrowser()
        Me.wmpMedia = New AxWMPLib.AxWindowsMediaPlayer()
        CType(Me.picInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.scTextMedia, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scTextMedia.Panel1.SuspendLayout()
        Me.scTextMedia.Panel2.SuspendLayout()
        Me.scTextMedia.SuspendLayout()
        CType(Me.wmpMedia, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtInfo
        '
        Me.txtInfo.BackColor = System.Drawing.Color.Black
        Me.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.txtInfo.ForeColor = System.Drawing.Color.Red
        Me.txtInfo.Location = New System.Drawing.Point(0, 0)
        Me.txtInfo.Multiline = True
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.ReadOnly = True
        Me.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInfo.Size = New System.Drawing.Size(560, 78)
        Me.txtInfo.TabIndex = 0
        Me.txtInfo.Visible = False
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Location = New System.Drawing.Point(449, 527)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(123, 23)
        Me.btnNext.TabIndex = 1
        Me.btnNext.Text = "&Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Location = New System.Drawing.Point(12, 527)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 23)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "E&xit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'picInfo
        '
        Me.picInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picInfo.Location = New System.Drawing.Point(0, 0)
        Me.picInfo.Name = "picInfo"
        Me.picInfo.Size = New System.Drawing.Size(560, 427)
        Me.picInfo.TabIndex = 2
        Me.picInfo.TabStop = False
        Me.picInfo.Visible = False
        '
        'btnPrevious
        '
        Me.btnPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrevious.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnPrevious.Location = New System.Drawing.Point(325, 527)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(118, 23)
        Me.btnPrevious.TabIndex = 0
        Me.btnPrevious.Text = "&Previous"
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'scTextMedia
        '
        Me.scTextMedia.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scTextMedia.Location = New System.Drawing.Point(12, 12)
        Me.scTextMedia.Name = "scTextMedia"
        Me.scTextMedia.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scTextMedia.Panel1
        '
        Me.scTextMedia.Panel1.Controls.Add(Me.wbInfo)
        Me.scTextMedia.Panel1.Controls.Add(Me.txtInfo)
        '
        'scTextMedia.Panel2
        '
        Me.scTextMedia.Panel2.Controls.Add(Me.wmpMedia)
        Me.scTextMedia.Panel2.Controls.Add(Me.picInfo)
        Me.scTextMedia.Size = New System.Drawing.Size(560, 509)
        Me.scTextMedia.SplitterDistance = 78
        Me.scTextMedia.TabIndex = 2
        '
        'wbInfo
        '
        Me.wbInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbInfo.Location = New System.Drawing.Point(0, 0)
        Me.wbInfo.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbInfo.Name = "wbInfo"
        Me.wbInfo.Size = New System.Drawing.Size(560, 78)
        Me.wbInfo.TabIndex = 0
        Me.wbInfo.Visible = False
        '
        'wmpMedia
        '
        Me.wmpMedia.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wmpMedia.Enabled = True
        Me.wmpMedia.Location = New System.Drawing.Point(0, 0)
        Me.wmpMedia.Name = "wmpMedia"
        Me.wmpMedia.OcxState = CType(resources.GetObject("wmpMedia.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wmpMedia.Size = New System.Drawing.Size(560, 427)
        Me.wmpMedia.TabIndex = 0
        Me.wmpMedia.Visible = False
        '
        'frmPresentation
        '
        Me.AcceptButton = Me.btnNext
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.btnPrevious
        Me.ClientSize = New System.Drawing.Size(584, 562)
        Me.Controls.Add(Me.scTextMedia)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnNext)
        Me.Name = "frmPresentation"
        Me.Text = "<waiting for info>"
        CType(Me.picInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scTextMedia.Panel1.ResumeLayout(False)
        Me.scTextMedia.Panel1.PerformLayout()
        Me.scTextMedia.Panel2.ResumeLayout(False)
        CType(Me.scTextMedia, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scTextMedia.ResumeLayout(False)
        CType(Me.wmpMedia, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtInfo As System.Windows.Forms.TextBox
    Friend WithEvents picInfo As System.Windows.Forms.PictureBox
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents scTextMedia As System.Windows.Forms.SplitContainer
    Friend WithEvents wmpMedia As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents wbInfo As System.Windows.Forms.WebBrowser
End Class
