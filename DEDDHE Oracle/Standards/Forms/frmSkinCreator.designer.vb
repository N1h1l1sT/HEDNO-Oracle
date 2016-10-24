<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSkinCreator
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
        Me.cbSelForms = New System.Windows.Forms.ComboBox()
        Me.lblSelForm = New System.Windows.Forms.Label()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblSelControl = New System.Windows.Forms.Label()
        Me.cbSelControls = New System.Windows.Forms.ComboBox()
        Me.gbItems = New System.Windows.Forms.GroupBox()
        Me.gbActions = New System.Windows.Forms.GroupBox()
        Me.txtCtrlImage = New System.Windows.Forms.TextBox()
        Me.btnCtrlImage = New System.Windows.Forms.Button()
        Me.lblCtrlImage = New System.Windows.Forms.Label()
        Me.btnDelBackColour = New System.Windows.Forms.Button()
        Me.btnDelForeColour = New System.Windows.Forms.Button()
        Me.txtSplashScreenImage = New System.Windows.Forms.TextBox()
        Me.btnSplashScreenImage = New System.Windows.Forms.Button()
        Me.lblSplashScreenImage = New System.Windows.Forms.Label()
        Me.txtBigImage = New System.Windows.Forms.TextBox()
        Me.btnBigImage = New System.Windows.Forms.Button()
        Me.lblBigImage = New System.Windows.Forms.Label()
        Me.txtImage = New System.Windows.Forms.TextBox()
        Me.btnImage = New System.Windows.Forms.Button()
        Me.lblImage = New System.Windows.Forms.Label()
        Me.cbBackColour = New System.Windows.Forms.ComboBox()
        Me.lblBackColour = New System.Windows.Forms.Label()
        Me.cbForeColour = New System.Windows.Forms.ComboBox()
        Me.lblForeColour = New System.Windows.Forms.Label()
        Me.ShapeContainer2 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.lnsBackColour = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.lnsForeColour = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.lstSkins = New System.Windows.Forms.ListBox()
        Me.gbSkins = New System.Windows.Forms.GroupBox()
        Me.gbCommands = New System.Windows.Forms.GroupBox()
        Me.btnRename = New System.Windows.Forms.Button()
        Me.btnDelSkin = New System.Windows.Forms.Button()
        Me.btnNewSkin = New System.Windows.Forms.Button()
        Me.fswSkins = New System.IO.FileSystemWatcher()
        Me.ofdImage = New System.Windows.Forms.OpenFileDialog()
        Me.gbItems.SuspendLayout()
        Me.gbActions.SuspendLayout()
        Me.gbSkins.SuspendLayout()
        Me.gbCommands.SuspendLayout()
        CType(Me.fswSkins, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbSelForms
        '
        Me.cbSelForms.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSelForms.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbSelForms.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbSelForms.FormattingEnabled = True
        Me.cbSelForms.Location = New System.Drawing.Point(173, 28)
        Me.cbSelForms.Name = "cbSelForms"
        Me.cbSelForms.Size = New System.Drawing.Size(170, 21)
        Me.cbSelForms.TabIndex = 0
        '
        'lblSelForm
        '
        Me.lblSelForm.AutoSize = True
        Me.lblSelForm.Location = New System.Drawing.Point(6, 31)
        Me.lblSelForm.Name = "lblSelForm"
        Me.lblSelForm.Size = New System.Drawing.Size(102, 13)
        Me.lblSelForm.TabIndex = 2
        Me.lblSelForm.Text = "Select Form to Skin:"
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnApply.Enabled = False
        Me.btnApply.Location = New System.Drawing.Point(69, 393)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(119, 23)
        Me.btnApply.TabIndex = 2
        Me.btnApply.Text = "&Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(452, 393)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(110, 23)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lblSelControl
        '
        Me.lblSelControl.AutoSize = True
        Me.lblSelControl.Location = New System.Drawing.Point(6, 58)
        Me.lblSelControl.Name = "lblSelControl"
        Me.lblSelControl.Size = New System.Drawing.Size(112, 13)
        Me.lblSelControl.TabIndex = 3
        Me.lblSelControl.Text = "Select Control to Skin:"
        '
        'cbSelControls
        '
        Me.cbSelControls.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSelControls.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbSelControls.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbSelControls.FormattingEnabled = True
        Me.cbSelControls.Location = New System.Drawing.Point(173, 55)
        Me.cbSelControls.Name = "cbSelControls"
        Me.cbSelControls.Size = New System.Drawing.Size(170, 21)
        Me.cbSelControls.TabIndex = 1
        '
        'gbItems
        '
        Me.gbItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbItems.Controls.Add(Me.cbSelForms)
        Me.gbItems.Controls.Add(Me.cbSelControls)
        Me.gbItems.Controls.Add(Me.lblSelControl)
        Me.gbItems.Controls.Add(Me.lblSelForm)
        Me.gbItems.Enabled = False
        Me.gbItems.Location = New System.Drawing.Point(267, 12)
        Me.gbItems.Name = "gbItems"
        Me.gbItems.Size = New System.Drawing.Size(349, 93)
        Me.gbItems.TabIndex = 0
        Me.gbItems.TabStop = False
        Me.gbItems.Text = "Items to skin:"
        '
        'gbActions
        '
        Me.gbActions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbActions.Controls.Add(Me.txtCtrlImage)
        Me.gbActions.Controls.Add(Me.btnCtrlImage)
        Me.gbActions.Controls.Add(Me.lblCtrlImage)
        Me.gbActions.Controls.Add(Me.btnDelBackColour)
        Me.gbActions.Controls.Add(Me.btnDelForeColour)
        Me.gbActions.Controls.Add(Me.txtSplashScreenImage)
        Me.gbActions.Controls.Add(Me.btnSplashScreenImage)
        Me.gbActions.Controls.Add(Me.lblSplashScreenImage)
        Me.gbActions.Controls.Add(Me.txtBigImage)
        Me.gbActions.Controls.Add(Me.btnBigImage)
        Me.gbActions.Controls.Add(Me.lblBigImage)
        Me.gbActions.Controls.Add(Me.txtImage)
        Me.gbActions.Controls.Add(Me.btnImage)
        Me.gbActions.Controls.Add(Me.lblImage)
        Me.gbActions.Controls.Add(Me.cbBackColour)
        Me.gbActions.Controls.Add(Me.lblBackColour)
        Me.gbActions.Controls.Add(Me.cbForeColour)
        Me.gbActions.Controls.Add(Me.lblForeColour)
        Me.gbActions.Controls.Add(Me.ShapeContainer2)
        Me.gbActions.Location = New System.Drawing.Point(267, 111)
        Me.gbActions.Name = "gbActions"
        Me.gbActions.Size = New System.Drawing.Size(349, 197)
        Me.gbActions.TabIndex = 1
        Me.gbActions.TabStop = False
        Me.gbActions.Text = "Actions:"
        '
        'txtCtrlImage
        '
        Me.txtCtrlImage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCtrlImage.Location = New System.Drawing.Point(173, 163)
        Me.txtCtrlImage.Name = "txtCtrlImage"
        Me.txtCtrlImage.ReadOnly = True
        Me.txtCtrlImage.Size = New System.Drawing.Size(141, 20)
        Me.txtCtrlImage.TabIndex = 11
        '
        'btnCtrlImage
        '
        Me.btnCtrlImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrlImage.Enabled = False
        Me.btnCtrlImage.Location = New System.Drawing.Point(320, 161)
        Me.btnCtrlImage.Name = "btnCtrlImage"
        Me.btnCtrlImage.Size = New System.Drawing.Size(24, 23)
        Me.btnCtrlImage.TabIndex = 7
        Me.btnCtrlImage.Text = "..."
        Me.btnCtrlImage.UseVisualStyleBackColor = True
        '
        'lblCtrlImage
        '
        Me.lblCtrlImage.AutoSize = True
        Me.lblCtrlImage.Location = New System.Drawing.Point(7, 165)
        Me.lblCtrlImage.Name = "lblCtrlImage"
        Me.lblCtrlImage.Size = New System.Drawing.Size(82, 13)
        Me.lblCtrlImage.TabIndex = 17
        Me.lblCtrlImage.Text = "Control's Image:"
        '
        'btnDelBackColour
        '
        Me.btnDelBackColour.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelBackColour.Enabled = False
        Me.btnDelBackColour.Location = New System.Drawing.Point(320, 131)
        Me.btnDelBackColour.Name = "btnDelBackColour"
        Me.btnDelBackColour.Size = New System.Drawing.Size(24, 23)
        Me.btnDelBackColour.TabIndex = 3
        Me.btnDelBackColour.Text = "-"
        Me.btnDelBackColour.UseVisualStyleBackColor = True
        '
        'btnDelForeColour
        '
        Me.btnDelForeColour.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelForeColour.Enabled = False
        Me.btnDelForeColour.Location = New System.Drawing.Point(320, 103)
        Me.btnDelForeColour.Name = "btnDelForeColour"
        Me.btnDelForeColour.Size = New System.Drawing.Size(24, 23)
        Me.btnDelForeColour.TabIndex = 1
        Me.btnDelForeColour.Text = "-"
        Me.btnDelForeColour.UseVisualStyleBackColor = True
        '
        'txtSplashScreenImage
        '
        Me.txtSplashScreenImage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSplashScreenImage.Location = New System.Drawing.Point(173, 21)
        Me.txtSplashScreenImage.Name = "txtSplashScreenImage"
        Me.txtSplashScreenImage.ReadOnly = True
        Me.txtSplashScreenImage.Size = New System.Drawing.Size(141, 20)
        Me.txtSplashScreenImage.TabIndex = 8
        '
        'btnSplashScreenImage
        '
        Me.btnSplashScreenImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSplashScreenImage.Enabled = False
        Me.btnSplashScreenImage.Location = New System.Drawing.Point(320, 19)
        Me.btnSplashScreenImage.Name = "btnSplashScreenImage"
        Me.btnSplashScreenImage.Size = New System.Drawing.Size(24, 23)
        Me.btnSplashScreenImage.TabIndex = 4
        Me.btnSplashScreenImage.Text = "..."
        Me.btnSplashScreenImage.UseVisualStyleBackColor = True
        '
        'lblSplashScreenImage
        '
        Me.lblSplashScreenImage.AutoSize = True
        Me.lblSplashScreenImage.Location = New System.Drawing.Point(6, 23)
        Me.lblSplashScreenImage.Name = "lblSplashScreenImage"
        Me.lblSplashScreenImage.Size = New System.Drawing.Size(105, 13)
        Me.lblSplashScreenImage.TabIndex = 12
        Me.lblSplashScreenImage.Text = "SplashScreen Image"
        '
        'txtBigImage
        '
        Me.txtBigImage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBigImage.Location = New System.Drawing.Point(173, 79)
        Me.txtBigImage.Name = "txtBigImage"
        Me.txtBigImage.ReadOnly = True
        Me.txtBigImage.Size = New System.Drawing.Size(141, 20)
        Me.txtBigImage.TabIndex = 10
        '
        'btnBigImage
        '
        Me.btnBigImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBigImage.Enabled = False
        Me.btnBigImage.Location = New System.Drawing.Point(320, 77)
        Me.btnBigImage.Name = "btnBigImage"
        Me.btnBigImage.Size = New System.Drawing.Size(24, 23)
        Me.btnBigImage.TabIndex = 6
        Me.btnBigImage.Text = "..."
        Me.btnBigImage.UseVisualStyleBackColor = True
        '
        'lblBigImage
        '
        Me.lblBigImage.AutoSize = True
        Me.lblBigImage.Location = New System.Drawing.Point(7, 81)
        Me.lblBigImage.Name = "lblBigImage"
        Me.lblBigImage.Size = New System.Drawing.Size(90, 13)
        Me.lblBigImage.TabIndex = 14
        Me.lblBigImage.Text = "Form's Big Image:"
        '
        'txtImage
        '
        Me.txtImage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtImage.Location = New System.Drawing.Point(173, 50)
        Me.txtImage.Name = "txtImage"
        Me.txtImage.ReadOnly = True
        Me.txtImage.Size = New System.Drawing.Size(141, 20)
        Me.txtImage.TabIndex = 9
        '
        'btnImage
        '
        Me.btnImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImage.Enabled = False
        Me.btnImage.Location = New System.Drawing.Point(320, 48)
        Me.btnImage.Name = "btnImage"
        Me.btnImage.Size = New System.Drawing.Size(24, 23)
        Me.btnImage.TabIndex = 5
        Me.btnImage.Text = "..."
        Me.btnImage.UseVisualStyleBackColor = True
        '
        'lblImage
        '
        Me.lblImage.AutoSize = True
        Me.lblImage.Location = New System.Drawing.Point(7, 52)
        Me.lblImage.Name = "lblImage"
        Me.lblImage.Size = New System.Drawing.Size(72, 13)
        Me.lblImage.TabIndex = 13
        Me.lblImage.Text = "Form's Image:"
        '
        'cbBackColour
        '
        Me.cbBackColour.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbBackColour.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbBackColour.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbBackColour.Enabled = False
        Me.cbBackColour.FormattingEnabled = True
        Me.cbBackColour.Location = New System.Drawing.Point(173, 132)
        Me.cbBackColour.Name = "cbBackColour"
        Me.cbBackColour.Size = New System.Drawing.Size(142, 21)
        Me.cbBackColour.TabIndex = 2
        '
        'lblBackColour
        '
        Me.lblBackColour.AutoSize = True
        Me.lblBackColour.Location = New System.Drawing.Point(7, 135)
        Me.lblBackColour.Name = "lblBackColour"
        Me.lblBackColour.Size = New System.Drawing.Size(68, 13)
        Me.lblBackColour.TabIndex = 16
        Me.lblBackColour.Text = "Back Colour:"
        '
        'cbForeColour
        '
        Me.cbForeColour.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbForeColour.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbForeColour.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbForeColour.Enabled = False
        Me.cbForeColour.FormattingEnabled = True
        Me.cbForeColour.Location = New System.Drawing.Point(173, 105)
        Me.cbForeColour.Name = "cbForeColour"
        Me.cbForeColour.Size = New System.Drawing.Size(142, 21)
        Me.cbForeColour.TabIndex = 0
        '
        'lblForeColour
        '
        Me.lblForeColour.AutoSize = True
        Me.lblForeColour.Location = New System.Drawing.Point(7, 108)
        Me.lblForeColour.Name = "lblForeColour"
        Me.lblForeColour.Size = New System.Drawing.Size(64, 13)
        Me.lblForeColour.TabIndex = 15
        Me.lblForeColour.Text = "Fore Colour:"
        '
        'ShapeContainer2
        '
        Me.ShapeContainer2.Location = New System.Drawing.Point(3, 16)
        Me.ShapeContainer2.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer2.Name = "ShapeContainer2"
        Me.ShapeContainer2.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.lnsBackColour, Me.lnsForeColour})
        Me.ShapeContainer2.Size = New System.Drawing.Size(343, 178)
        Me.ShapeContainer2.TabIndex = 18
        Me.ShapeContainer2.TabStop = False
        '
        'lnsBackColour
        '
        Me.lnsBackColour.BorderWidth = 4
        Me.lnsBackColour.Name = "lnsBackColour"
        Me.lnsBackColour.X1 = 170
        Me.lnsBackColour.X2 = 337
        Me.lnsBackColour.Y1 = 140
        Me.lnsBackColour.Y2 = 140
        '
        'lnsForeColour
        '
        Me.lnsForeColour.BorderWidth = 4
        Me.lnsForeColour.Name = "lnsForeColour"
        Me.lnsForeColour.X1 = 170
        Me.lnsForeColour.X2 = 337
        Me.lnsForeColour.Y1 = 112
        Me.lnsForeColour.Y2 = 112
        '
        'lstSkins
        '
        Me.lstSkins.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstSkins.FormattingEnabled = True
        Me.lstSkins.Location = New System.Drawing.Point(3, 16)
        Me.lstSkins.Name = "lstSkins"
        Me.lstSkins.Size = New System.Drawing.Size(239, 277)
        Me.lstSkins.TabIndex = 0
        '
        'gbSkins
        '
        Me.gbSkins.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbSkins.Controls.Add(Me.lstSkins)
        Me.gbSkins.Location = New System.Drawing.Point(16, 12)
        Me.gbSkins.Name = "gbSkins"
        Me.gbSkins.Size = New System.Drawing.Size(245, 296)
        Me.gbSkins.TabIndex = 5
        Me.gbSkins.TabStop = False
        Me.gbSkins.Text = "Skins:"
        '
        'gbCommands
        '
        Me.gbCommands.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbCommands.Controls.Add(Me.btnRename)
        Me.gbCommands.Controls.Add(Me.btnDelSkin)
        Me.gbCommands.Controls.Add(Me.btnNewSkin)
        Me.gbCommands.Location = New System.Drawing.Point(16, 314)
        Me.gbCommands.Name = "gbCommands"
        Me.gbCommands.Size = New System.Drawing.Size(600, 61)
        Me.gbCommands.TabIndex = 3
        Me.gbCommands.TabStop = False
        Me.gbCommands.Text = "Commands:"
        '
        'btnRename
        '
        Me.btnRename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRename.Enabled = False
        Me.btnRename.Location = New System.Drawing.Point(215, 22)
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Size = New System.Drawing.Size(172, 23)
        Me.btnRename.TabIndex = 1
        Me.btnRename.Text = "&Rename Skin"
        Me.btnRename.UseVisualStyleBackColor = True
        '
        'btnDelSkin
        '
        Me.btnDelSkin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelSkin.Enabled = False
        Me.btnDelSkin.Location = New System.Drawing.Point(463, 22)
        Me.btnDelSkin.Name = "btnDelSkin"
        Me.btnDelSkin.Size = New System.Drawing.Size(119, 23)
        Me.btnDelSkin.TabIndex = 2
        Me.btnDelSkin.Text = "&Delete Skin"
        Me.btnDelSkin.UseVisualStyleBackColor = True
        '
        'btnNewSkin
        '
        Me.btnNewSkin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNewSkin.Location = New System.Drawing.Point(19, 22)
        Me.btnNewSkin.Name = "btnNewSkin"
        Me.btnNewSkin.Size = New System.Drawing.Size(119, 23)
        Me.btnNewSkin.TabIndex = 0
        Me.btnNewSkin.Text = "&New Skin"
        Me.btnNewSkin.UseVisualStyleBackColor = True
        '
        'fswSkins
        '
        Me.fswSkins.EnableRaisingEvents = True
        Me.fswSkins.SynchronizingObject = Me
        '
        'ofdImage
        '
        Me.ofdImage.FileName = "Image"
        Me.ofdImage.Filter = "JPG Pictures|*.jpg"
        Me.ofdImage.ReadOnlyChecked = True
        '
        'frmSkinCreator
        '
        Me.AcceptButton = Me.btnApply
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(628, 423)
        Me.Controls.Add(Me.gbCommands)
        Me.Controls.Add(Me.gbSkins)
        Me.Controls.Add(Me.gbActions)
        Me.Controls.Add(Me.gbItems)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnApply)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmSkinCreator"
        Me.Text = "Skin Creator"
        Me.gbItems.ResumeLayout(False)
        Me.gbItems.PerformLayout()
        Me.gbActions.ResumeLayout(False)
        Me.gbActions.PerformLayout()
        Me.gbSkins.ResumeLayout(False)
        Me.gbCommands.ResumeLayout(False)
        CType(Me.fswSkins, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cbSelForms As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelForm As System.Windows.Forms.Label
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblSelControl As System.Windows.Forms.Label
    Friend WithEvents cbSelControls As System.Windows.Forms.ComboBox
    Friend WithEvents gbItems As System.Windows.Forms.GroupBox
    Friend WithEvents gbActions As System.Windows.Forms.GroupBox
    Friend WithEvents cbBackColour As System.Windows.Forms.ComboBox
    Friend WithEvents lblBackColour As System.Windows.Forms.Label
    Friend WithEvents cbForeColour As System.Windows.Forms.ComboBox
    Friend WithEvents lblForeColour As System.Windows.Forms.Label
    Friend WithEvents lstSkins As System.Windows.Forms.ListBox
    Friend WithEvents gbSkins As System.Windows.Forms.GroupBox
    Friend WithEvents gbCommands As System.Windows.Forms.GroupBox
    Friend WithEvents btnRename As System.Windows.Forms.Button
    Friend WithEvents btnDelSkin As System.Windows.Forms.Button
    Friend WithEvents btnNewSkin As System.Windows.Forms.Button
    Friend WithEvents lblImage As System.Windows.Forms.Label
    Friend WithEvents fswSkins As System.IO.FileSystemWatcher
    Friend WithEvents txtImage As System.Windows.Forms.TextBox
    Friend WithEvents btnImage As System.Windows.Forms.Button
    Friend WithEvents txtBigImage As System.Windows.Forms.TextBox
    Friend WithEvents btnBigImage As System.Windows.Forms.Button
    Friend WithEvents lblBigImage As System.Windows.Forms.Label
    Friend WithEvents ofdImage As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtSplashScreenImage As System.Windows.Forms.TextBox
    Friend WithEvents btnSplashScreenImage As System.Windows.Forms.Button
    Friend WithEvents lblSplashScreenImage As System.Windows.Forms.Label
    Friend WithEvents btnDelBackColour As System.Windows.Forms.Button
    Friend WithEvents btnDelForeColour As System.Windows.Forms.Button
    Friend WithEvents txtCtrlImage As System.Windows.Forms.TextBox
    Friend WithEvents btnCtrlImage As System.Windows.Forms.Button
    Friend WithEvents lblCtrlImage As System.Windows.Forms.Label
    Friend WithEvents ShapeContainer2 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents lnsForeColour As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents lnsBackColour As Microsoft.VisualBasic.PowerPacks.LineShape
End Class
