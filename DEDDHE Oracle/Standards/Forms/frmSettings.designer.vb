<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
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
        Me.lblSecs = New System.Windows.Forms.Label()
        Me.txtDelayTime = New System.Windows.Forms.TextBox()
        Me.lblDelayTime = New System.Windows.Forms.Label()
        Me.cbStartWithWin = New System.Windows.Forms.ComboBox()
        Me.lblStartWithWin = New System.Windows.Forms.Label()
        Me.cbLanguage = New System.Windows.Forms.ComboBox()
        Me.lblLanguage = New System.Windows.Forms.Label()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdApply = New System.Windows.Forms.Button()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.fbdPathBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.cbShowStartupForm = New System.Windows.Forms.ComboBox()
        Me.lblShowStartupForm = New System.Windows.Forms.Label()
        Me.tcSettings = New System.Windows.Forms.TabControl()
        Me.tpGeneral = New System.Windows.Forms.TabPage()
        Me.cbRemWindowState = New System.Windows.Forms.ComboBox()
        Me.lblRemWindowState = New System.Windows.Forms.Label()
        Me.lblX = New System.Windows.Forms.Label()
        Me.txtWindowResolutionHeight = New System.Windows.Forms.TextBox()
        Me.txtWindowResolutionWidth = New System.Windows.Forms.TextBox()
        Me.lblWindowResolution = New System.Windows.Forms.Label()
        Me.cbFullScreenResolutions = New System.Windows.Forms.ComboBox()
        Me.lblFullScreenResolutions = New System.Windows.Forms.Label()
        Me.cbWindowState = New System.Windows.Forms.ComboBox()
        Me.lblWindowState = New System.Windows.Forms.Label()
        Me.cbCheckForNewVersion = New System.Windows.Forms.ComboBox()
        Me.lblCheckForNewVersion = New System.Windows.Forms.Label()
        Me.btnSkinAdvanced = New System.Windows.Forms.Button()
        Me.lblSkin = New System.Windows.Forms.Label()
        Me.cbSkin = New System.Windows.Forms.ComboBox()
        Me.tpDatabase = New System.Windows.Forms.TabPage()
        Me.txtProtectedTables = New System.Windows.Forms.TextBox()
        Me.btnProtectedTables = New System.Windows.Forms.Button()
        Me.lblProtectedTables = New System.Windows.Forms.Label()
        Me.txtDatabaseTables = New System.Windows.Forms.TextBox()
        Me.lblDatabaseTables = New System.Windows.Forms.Label()
        Me.cbSplitDbEveryMonth = New System.Windows.Forms.ComboBox()
        Me.lblSplitDbEveryMonth = New System.Windows.Forms.Label()
        Me.btnDatabaseTables = New System.Windows.Forms.Button()
        Me.cbAccessType = New System.Windows.Forms.ComboBox()
        Me.txtDBpass = New System.Windows.Forms.TextBox()
        Me.lblDBpass = New System.Windows.Forms.Label()
        Me.txtDBFile = New System.Windows.Forms.TextBox()
        Me.lblDBFile = New System.Windows.Forms.Label()
        Me.lblDBtype = New System.Windows.Forms.Label()
        Me.btnBrowseDBPath = New System.Windows.Forms.Button()
        Me.gbCommands = New System.Windows.Forms.GroupBox()
        Me.cmdCurrent = New System.Windows.Forms.Button()
        Me.cmdDefault = New System.Windows.Forms.Button()
        Me.ofdFileBrowser = New System.Windows.Forms.OpenFileDialog()
        Me.tcSettings.SuspendLayout()
        Me.tpGeneral.SuspendLayout()
        Me.tpDatabase.SuspendLayout()
        Me.gbCommands.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSecs
        '
        Me.lblSecs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSecs.AutoSize = True
        Me.lblSecs.Location = New System.Drawing.Point(418, 100)
        Me.lblSecs.Name = "lblSecs"
        Me.lblSecs.Size = New System.Drawing.Size(49, 13)
        Me.lblSecs.TabIndex = 41
        Me.lblSecs.Text = "Seconds"
        '
        'txtDelayTime
        '
        Me.txtDelayTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDelayTime.Location = New System.Drawing.Point(252, 97)
        Me.txtDelayTime.Name = "txtDelayTime"
        Me.txtDelayTime.Size = New System.Drawing.Size(165, 20)
        Me.txtDelayTime.TabIndex = 4
        Me.txtDelayTime.Text = "Unknown"
        '
        'lblDelayTime
        '
        Me.lblDelayTime.AutoSize = True
        Me.lblDelayTime.Location = New System.Drawing.Point(4, 94)
        Me.lblDelayTime.Name = "lblDelayTime"
        Me.lblDelayTime.Size = New System.Drawing.Size(63, 13)
        Me.lblDelayTime.TabIndex = 39
        Me.lblDelayTime.Text = "Delay Time:"
        '
        'cbStartWithWin
        '
        Me.cbStartWithWin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbStartWithWin.FormattingEnabled = True
        Me.cbStartWithWin.Location = New System.Drawing.Point(252, 70)
        Me.cbStartWithWin.Name = "cbStartWithWin"
        Me.cbStartWithWin.Size = New System.Drawing.Size(234, 21)
        Me.cbStartWithWin.TabIndex = 3
        Me.cbStartWithWin.Tag = "0"
        Me.cbStartWithWin.Text = "Unknown"
        '
        'lblStartWithWin
        '
        Me.lblStartWithWin.AutoSize = True
        Me.lblStartWithWin.Location = New System.Drawing.Point(5, 70)
        Me.lblStartWithWin.Name = "lblStartWithWin"
        Me.lblStartWithWin.Size = New System.Drawing.Size(101, 13)
        Me.lblStartWithWin.TabIndex = 33
        Me.lblStartWithWin.Text = "Start with Windows:"
        '
        'cbLanguage
        '
        Me.cbLanguage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbLanguage.FormattingEnabled = True
        Me.cbLanguage.Location = New System.Drawing.Point(252, 16)
        Me.cbLanguage.Name = "cbLanguage"
        Me.cbLanguage.Size = New System.Drawing.Size(233, 21)
        Me.cbLanguage.TabIndex = 0
        Me.cbLanguage.Tag = "0"
        Me.cbLanguage.Text = "Unknown"
        '
        'lblLanguage
        '
        Me.lblLanguage.AutoSize = True
        Me.lblLanguage.BackColor = System.Drawing.Color.Black
        Me.lblLanguage.ForeColor = System.Drawing.Color.Gold
        Me.lblLanguage.Location = New System.Drawing.Point(5, 17)
        Me.lblLanguage.Name = "lblLanguage"
        Me.lblLanguage.Size = New System.Drawing.Size(58, 13)
        Me.lblLanguage.TabIndex = 15
        Me.lblLanguage.Text = "Language:"
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.Location = New System.Drawing.Point(27, 420)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(133, 21)
        Me.cmdExit.TabIndex = 2
        Me.cmdExit.Text = "Cancel"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdApply
        '
        Me.cmdApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdApply.Enabled = False
        Me.cmdApply.Location = New System.Drawing.Point(395, 420)
        Me.cmdApply.Name = "cmdApply"
        Me.cmdApply.Size = New System.Drawing.Size(133, 21)
        Me.cmdApply.TabIndex = 1
        Me.cmdApply.Text = "Apply And Close"
        Me.cmdApply.UseVisualStyleBackColor = True
        '
        'lblInfo
        '
        Me.lblInfo.AutoSize = True
        Me.lblInfo.BackColor = System.Drawing.Color.Black
        Me.lblInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.Gold
        Me.lblInfo.Location = New System.Drawing.Point(27, 68)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(272, 15)
        Me.lblInfo.TabIndex = 5
        Me.lblInfo.Text = "You may Change the language from here."
        '
        'cbShowStartupForm
        '
        Me.cbShowStartupForm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbShowStartupForm.FormattingEnabled = True
        Me.cbShowStartupForm.Location = New System.Drawing.Point(252, 123)
        Me.cbShowStartupForm.Name = "cbShowStartupForm"
        Me.cbShowStartupForm.Size = New System.Drawing.Size(233, 21)
        Me.cbShowStartupForm.TabIndex = 8
        Me.cbShowStartupForm.Tag = "0"
        Me.cbShowStartupForm.Text = "Unknown"
        '
        'lblShowStartupForm
        '
        Me.lblShowStartupForm.AutoSize = True
        Me.lblShowStartupForm.Location = New System.Drawing.Point(4, 123)
        Me.lblShowStartupForm.Name = "lblShowStartupForm"
        Me.lblShowStartupForm.Size = New System.Drawing.Size(100, 13)
        Me.lblShowStartupForm.TabIndex = 16
        Me.lblShowStartupForm.Text = "Show Startup Form:"
        '
        'tcSettings
        '
        Me.tcSettings.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcSettings.Controls.Add(Me.tpGeneral)
        Me.tcSettings.Controls.Add(Me.tpDatabase)
        Me.tcSettings.Location = New System.Drawing.Point(27, 94)
        Me.tcSettings.Name = "tcSettings"
        Me.tcSettings.SelectedIndex = 0
        Me.tcSettings.Size = New System.Drawing.Size(501, 312)
        Me.tcSettings.TabIndex = 0
        '
        'tpGeneral
        '
        Me.tpGeneral.Controls.Add(Me.cbRemWindowState)
        Me.tpGeneral.Controls.Add(Me.lblRemWindowState)
        Me.tpGeneral.Controls.Add(Me.lblX)
        Me.tpGeneral.Controls.Add(Me.txtWindowResolutionHeight)
        Me.tpGeneral.Controls.Add(Me.txtWindowResolutionWidth)
        Me.tpGeneral.Controls.Add(Me.lblWindowResolution)
        Me.tpGeneral.Controls.Add(Me.cbFullScreenResolutions)
        Me.tpGeneral.Controls.Add(Me.lblFullScreenResolutions)
        Me.tpGeneral.Controls.Add(Me.cbWindowState)
        Me.tpGeneral.Controls.Add(Me.lblWindowState)
        Me.tpGeneral.Controls.Add(Me.cbCheckForNewVersion)
        Me.tpGeneral.Controls.Add(Me.lblCheckForNewVersion)
        Me.tpGeneral.Controls.Add(Me.cbShowStartupForm)
        Me.tpGeneral.Controls.Add(Me.lblShowStartupForm)
        Me.tpGeneral.Controls.Add(Me.btnSkinAdvanced)
        Me.tpGeneral.Controls.Add(Me.lblSkin)
        Me.tpGeneral.Controls.Add(Me.cbSkin)
        Me.tpGeneral.Controls.Add(Me.cbLanguage)
        Me.tpGeneral.Controls.Add(Me.lblLanguage)
        Me.tpGeneral.Controls.Add(Me.lblSecs)
        Me.tpGeneral.Controls.Add(Me.txtDelayTime)
        Me.tpGeneral.Controls.Add(Me.lblDelayTime)
        Me.tpGeneral.Controls.Add(Me.lblStartWithWin)
        Me.tpGeneral.Controls.Add(Me.cbStartWithWin)
        Me.tpGeneral.Location = New System.Drawing.Point(4, 22)
        Me.tpGeneral.Name = "tpGeneral"
        Me.tpGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.tpGeneral.Size = New System.Drawing.Size(493, 286)
        Me.tpGeneral.TabIndex = 0
        Me.tpGeneral.Text = "General Settings"
        Me.tpGeneral.UseVisualStyleBackColor = True
        '
        'cbRemWindowState
        '
        Me.cbRemWindowState.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbRemWindowState.FormattingEnabled = True
        Me.cbRemWindowState.Location = New System.Drawing.Point(252, 177)
        Me.cbRemWindowState.Name = "cbRemWindowState"
        Me.cbRemWindowState.Size = New System.Drawing.Size(233, 21)
        Me.cbRemWindowState.TabIndex = 65
        Me.cbRemWindowState.Tag = "0"
        Me.cbRemWindowState.Text = "Unknown"
        '
        'lblRemWindowState
        '
        Me.lblRemWindowState.AutoSize = True
        Me.lblRemWindowState.Location = New System.Drawing.Point(4, 177)
        Me.lblRemWindowState.Name = "lblRemWindowState"
        Me.lblRemWindowState.Size = New System.Drawing.Size(131, 13)
        Me.lblRemWindowState.TabIndex = 66
        Me.lblRemWindowState.Text = "Remember Window State:"
        '
        'lblX
        '
        Me.lblX.AutoSize = True
        Me.lblX.Location = New System.Drawing.Point(363, 258)
        Me.lblX.Name = "lblX"
        Me.lblX.Size = New System.Drawing.Size(12, 13)
        Me.lblX.TabIndex = 64
        Me.lblX.Text = "x"
        '
        'txtWindowResolutionHeight
        '
        Me.txtWindowResolutionHeight.Location = New System.Drawing.Point(381, 255)
        Me.txtWindowResolutionHeight.Name = "txtWindowResolutionHeight"
        Me.txtWindowResolutionHeight.ReadOnly = True
        Me.txtWindowResolutionHeight.Size = New System.Drawing.Size(104, 20)
        Me.txtWindowResolutionHeight.TabIndex = 63
        Me.txtWindowResolutionHeight.Text = "Unknown"
        '
        'txtWindowResolutionWidth
        '
        Me.txtWindowResolutionWidth.Location = New System.Drawing.Point(252, 255)
        Me.txtWindowResolutionWidth.Name = "txtWindowResolutionWidth"
        Me.txtWindowResolutionWidth.ReadOnly = True
        Me.txtWindowResolutionWidth.Size = New System.Drawing.Size(104, 20)
        Me.txtWindowResolutionWidth.TabIndex = 61
        Me.txtWindowResolutionWidth.Text = "Unknown"
        '
        'lblWindowResolution
        '
        Me.lblWindowResolution.AutoSize = True
        Me.lblWindowResolution.Location = New System.Drawing.Point(5, 258)
        Me.lblWindowResolution.Name = "lblWindowResolution"
        Me.lblWindowResolution.Size = New System.Drawing.Size(102, 13)
        Me.lblWindowResolution.TabIndex = 62
        Me.lblWindowResolution.Text = "Window Resolution:"
        '
        'cbFullScreenResolutions
        '
        Me.cbFullScreenResolutions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbFullScreenResolutions.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbFullScreenResolutions.FormattingEnabled = True
        Me.cbFullScreenResolutions.Location = New System.Drawing.Point(252, 231)
        Me.cbFullScreenResolutions.Name = "cbFullScreenResolutions"
        Me.cbFullScreenResolutions.Size = New System.Drawing.Size(233, 21)
        Me.cbFullScreenResolutions.TabIndex = 59
        Me.cbFullScreenResolutions.Tag = "0"
        Me.cbFullScreenResolutions.Text = "Unknown"
        '
        'lblFullScreenResolutions
        '
        Me.lblFullScreenResolutions.AutoSize = True
        Me.lblFullScreenResolutions.Location = New System.Drawing.Point(5, 230)
        Me.lblFullScreenResolutions.Name = "lblFullScreenResolutions"
        Me.lblFullScreenResolutions.Size = New System.Drawing.Size(113, 13)
        Me.lblFullScreenResolutions.TabIndex = 60
        Me.lblFullScreenResolutions.Text = "FullScreen Resolution:"
        '
        'cbWindowState
        '
        Me.cbWindowState.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbWindowState.FormattingEnabled = True
        Me.cbWindowState.Location = New System.Drawing.Point(252, 204)
        Me.cbWindowState.Name = "cbWindowState"
        Me.cbWindowState.Size = New System.Drawing.Size(233, 21)
        Me.cbWindowState.TabIndex = 57
        Me.cbWindowState.Tag = "0"
        Me.cbWindowState.Text = "Unknown"
        '
        'lblWindowState
        '
        Me.lblWindowState.AutoSize = True
        Me.lblWindowState.Location = New System.Drawing.Point(5, 203)
        Me.lblWindowState.Name = "lblWindowState"
        Me.lblWindowState.Size = New System.Drawing.Size(77, 13)
        Me.lblWindowState.TabIndex = 58
        Me.lblWindowState.Text = "Window State:"
        '
        'cbCheckForNewVersion
        '
        Me.cbCheckForNewVersion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbCheckForNewVersion.FormattingEnabled = True
        Me.cbCheckForNewVersion.Location = New System.Drawing.Point(252, 150)
        Me.cbCheckForNewVersion.Name = "cbCheckForNewVersion"
        Me.cbCheckForNewVersion.Size = New System.Drawing.Size(233, 21)
        Me.cbCheckForNewVersion.TabIndex = 45
        Me.cbCheckForNewVersion.Tag = "0"
        Me.cbCheckForNewVersion.Text = "Unknown"
        '
        'lblCheckForNewVersion
        '
        Me.lblCheckForNewVersion.AutoSize = True
        Me.lblCheckForNewVersion.Location = New System.Drawing.Point(5, 150)
        Me.lblCheckForNewVersion.Name = "lblCheckForNewVersion"
        Me.lblCheckForNewVersion.Size = New System.Drawing.Size(119, 13)
        Me.lblCheckForNewVersion.TabIndex = 46
        Me.lblCheckForNewVersion.Text = "Check for New Version:"
        '
        'btnSkinAdvanced
        '
        Me.btnSkinAdvanced.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSkinAdvanced.Location = New System.Drawing.Point(461, 42)
        Me.btnSkinAdvanced.Name = "btnSkinAdvanced"
        Me.btnSkinAdvanced.Size = New System.Drawing.Size(25, 23)
        Me.btnSkinAdvanced.TabIndex = 2
        Me.btnSkinAdvanced.Text = "..."
        Me.btnSkinAdvanced.UseVisualStyleBackColor = True
        '
        'lblSkin
        '
        Me.lblSkin.AutoSize = True
        Me.lblSkin.Location = New System.Drawing.Point(5, 43)
        Me.lblSkin.Name = "lblSkin"
        Me.lblSkin.Size = New System.Drawing.Size(31, 13)
        Me.lblSkin.TabIndex = 23
        Me.lblSkin.Text = "Skin:"
        '
        'cbSkin
        '
        Me.cbSkin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSkin.FormattingEnabled = True
        Me.cbSkin.Items.AddRange(New Object() {"True", "False"})
        Me.cbSkin.Location = New System.Drawing.Point(252, 43)
        Me.cbSkin.Name = "cbSkin"
        Me.cbSkin.Size = New System.Drawing.Size(204, 21)
        Me.cbSkin.TabIndex = 1
        Me.cbSkin.Tag = "0"
        Me.cbSkin.Text = "Unknown"
        '
        'tpDatabase
        '
        Me.tpDatabase.Controls.Add(Me.txtProtectedTables)
        Me.tpDatabase.Controls.Add(Me.btnProtectedTables)
        Me.tpDatabase.Controls.Add(Me.lblProtectedTables)
        Me.tpDatabase.Controls.Add(Me.txtDatabaseTables)
        Me.tpDatabase.Controls.Add(Me.lblDatabaseTables)
        Me.tpDatabase.Controls.Add(Me.cbSplitDbEveryMonth)
        Me.tpDatabase.Controls.Add(Me.lblSplitDbEveryMonth)
        Me.tpDatabase.Controls.Add(Me.btnDatabaseTables)
        Me.tpDatabase.Controls.Add(Me.cbAccessType)
        Me.tpDatabase.Controls.Add(Me.txtDBpass)
        Me.tpDatabase.Controls.Add(Me.lblDBpass)
        Me.tpDatabase.Controls.Add(Me.txtDBFile)
        Me.tpDatabase.Controls.Add(Me.lblDBFile)
        Me.tpDatabase.Controls.Add(Me.lblDBtype)
        Me.tpDatabase.Controls.Add(Me.btnBrowseDBPath)
        Me.tpDatabase.Location = New System.Drawing.Point(4, 22)
        Me.tpDatabase.Name = "tpDatabase"
        Me.tpDatabase.Size = New System.Drawing.Size(493, 286)
        Me.tpDatabase.TabIndex = 2
        Me.tpDatabase.Text = "Database"
        Me.tpDatabase.UseVisualStyleBackColor = True
        '
        'txtProtectedTables
        '
        Me.txtProtectedTables.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProtectedTables.Location = New System.Drawing.Point(251, 151)
        Me.txtProtectedTables.Name = "txtProtectedTables"
        Me.txtProtectedTables.ReadOnly = True
        Me.txtProtectedTables.Size = New System.Drawing.Size(203, 20)
        Me.txtProtectedTables.TabIndex = 71
        Me.txtProtectedTables.Text = "Unknown"
        '
        'btnProtectedTables
        '
        Me.btnProtectedTables.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnProtectedTables.Location = New System.Drawing.Point(460, 149)
        Me.btnProtectedTables.Name = "btnProtectedTables"
        Me.btnProtectedTables.Size = New System.Drawing.Size(25, 23)
        Me.btnProtectedTables.TabIndex = 72
        Me.btnProtectedTables.Text = "..."
        Me.btnProtectedTables.UseVisualStyleBackColor = True
        '
        'lblProtectedTables
        '
        Me.lblProtectedTables.AutoSize = True
        Me.lblProtectedTables.Location = New System.Drawing.Point(5, 154)
        Me.lblProtectedTables.Name = "lblProtectedTables"
        Me.lblProtectedTables.Size = New System.Drawing.Size(91, 13)
        Me.lblProtectedTables.TabIndex = 73
        Me.lblProtectedTables.Text = "Protected Tables:"
        '
        'txtDatabaseTables
        '
        Me.txtDatabaseTables.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDatabaseTables.Location = New System.Drawing.Point(251, 125)
        Me.txtDatabaseTables.Name = "txtDatabaseTables"
        Me.txtDatabaseTables.ReadOnly = True
        Me.txtDatabaseTables.Size = New System.Drawing.Size(203, 20)
        Me.txtDatabaseTables.TabIndex = 62
        Me.txtDatabaseTables.Text = "Unknown"
        '
        'lblDatabaseTables
        '
        Me.lblDatabaseTables.AutoSize = True
        Me.lblDatabaseTables.Location = New System.Drawing.Point(5, 128)
        Me.lblDatabaseTables.Name = "lblDatabaseTables"
        Me.lblDatabaseTables.Size = New System.Drawing.Size(92, 13)
        Me.lblDatabaseTables.TabIndex = 64
        Me.lblDatabaseTables.Text = "DataBase Tables:"
        '
        'cbSplitDbEveryMonth
        '
        Me.cbSplitDbEveryMonth.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSplitDbEveryMonth.FormattingEnabled = True
        Me.cbSplitDbEveryMonth.Location = New System.Drawing.Point(252, 16)
        Me.cbSplitDbEveryMonth.Name = "cbSplitDbEveryMonth"
        Me.cbSplitDbEveryMonth.Size = New System.Drawing.Size(233, 21)
        Me.cbSplitDbEveryMonth.TabIndex = 47
        Me.cbSplitDbEveryMonth.Tag = "0"
        Me.cbSplitDbEveryMonth.Text = "Unknown"
        '
        'lblSplitDbEveryMonth
        '
        Me.lblSplitDbEveryMonth.AutoSize = True
        Me.lblSplitDbEveryMonth.Location = New System.Drawing.Point(6, 19)
        Me.lblSplitDbEveryMonth.Name = "lblSplitDbEveryMonth"
        Me.lblSplitDbEveryMonth.Size = New System.Drawing.Size(142, 13)
        Me.lblSplitDbEveryMonth.TabIndex = 48
        Me.lblSplitDbEveryMonth.Text = "Split Database Every Month:"
        '
        'btnDatabaseTables
        '
        Me.btnDatabaseTables.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDatabaseTables.Location = New System.Drawing.Point(460, 123)
        Me.btnDatabaseTables.Name = "btnDatabaseTables"
        Me.btnDatabaseTables.Size = New System.Drawing.Size(25, 23)
        Me.btnDatabaseTables.TabIndex = 63
        Me.btnDatabaseTables.Text = "..."
        Me.btnDatabaseTables.UseVisualStyleBackColor = True
        '
        'cbAccessType
        '
        Me.cbAccessType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbAccessType.FormattingEnabled = True
        Me.cbAccessType.Location = New System.Drawing.Point(252, 69)
        Me.cbAccessType.Name = "cbAccessType"
        Me.cbAccessType.Size = New System.Drawing.Size(233, 21)
        Me.cbAccessType.TabIndex = 60
        Me.cbAccessType.Tag = "0"
        Me.cbAccessType.Text = "Unknown"
        '
        'txtDBpass
        '
        Me.txtDBpass.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDBpass.Location = New System.Drawing.Point(252, 43)
        Me.txtDBpass.Name = "txtDBpass"
        Me.txtDBpass.Size = New System.Drawing.Size(233, 20)
        Me.txtDBpass.TabIndex = 6
        Me.txtDBpass.Text = "Unknown"
        '
        'lblDBpass
        '
        Me.lblDBpass.AutoSize = True
        Me.lblDBpass.Location = New System.Drawing.Point(6, 46)
        Me.lblDBpass.Name = "lblDBpass"
        Me.lblDBpass.Size = New System.Drawing.Size(56, 13)
        Me.lblDBpass.TabIndex = 55
        Me.lblDBpass.Text = "Password:"
        '
        'txtDBFile
        '
        Me.txtDBFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDBFile.Location = New System.Drawing.Point(251, 96)
        Me.txtDBFile.Name = "txtDBFile"
        Me.txtDBFile.Size = New System.Drawing.Size(203, 20)
        Me.txtDBFile.TabIndex = 0
        Me.txtDBFile.Text = "Unknown"
        '
        'lblDBFile
        '
        Me.lblDBFile.AutoSize = True
        Me.lblDBFile.Location = New System.Drawing.Point(5, 99)
        Me.lblDBFile.Name = "lblDBFile"
        Me.lblDBFile.Size = New System.Drawing.Size(76, 13)
        Me.lblDBFile.TabIndex = 48
        Me.lblDBFile.Text = "DataBase File:"
        '
        'lblDBtype
        '
        Me.lblDBtype.AutoSize = True
        Me.lblDBtype.Location = New System.Drawing.Point(5, 72)
        Me.lblDBtype.Name = "lblDBtype"
        Me.lblDBtype.Size = New System.Drawing.Size(83, 13)
        Me.lblDBtype.TabIndex = 51
        Me.lblDBtype.Text = "Database Type:"
        '
        'btnBrowseDBPath
        '
        Me.btnBrowseDBPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseDBPath.Location = New System.Drawing.Point(460, 94)
        Me.btnBrowseDBPath.Name = "btnBrowseDBPath"
        Me.btnBrowseDBPath.Size = New System.Drawing.Size(25, 23)
        Me.btnBrowseDBPath.TabIndex = 1
        Me.btnBrowseDBPath.Text = "..."
        Me.btnBrowseDBPath.UseVisualStyleBackColor = True
        '
        'gbCommands
        '
        Me.gbCommands.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbCommands.BackColor = System.Drawing.Color.Transparent
        Me.gbCommands.Controls.Add(Me.cmdCurrent)
        Me.gbCommands.Controls.Add(Me.cmdDefault)
        Me.gbCommands.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gbCommands.Location = New System.Drawing.Point(22, 12)
        Me.gbCommands.Name = "gbCommands"
        Me.gbCommands.Size = New System.Drawing.Size(512, 48)
        Me.gbCommands.TabIndex = 31
        Me.gbCommands.TabStop = False
        Me.gbCommands.Text = "Commands:"
        '
        'cmdCurrent
        '
        Me.cmdCurrent.Location = New System.Drawing.Point(6, 19)
        Me.cmdCurrent.Name = "cmdCurrent"
        Me.cmdCurrent.Size = New System.Drawing.Size(133, 23)
        Me.cmdCurrent.TabIndex = 0
        Me.cmdCurrent.Text = "Read Current Settings"
        Me.cmdCurrent.UseVisualStyleBackColor = True
        '
        'cmdDefault
        '
        Me.cmdDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDefault.Location = New System.Drawing.Point(373, 19)
        Me.cmdDefault.Name = "cmdDefault"
        Me.cmdDefault.Size = New System.Drawing.Size(133, 23)
        Me.cmdDefault.TabIndex = 1
        Me.cmdDefault.Text = "Reset Default Settings"
        Me.cmdDefault.UseVisualStyleBackColor = True
        '
        'frmSettings
        '
        Me.AcceptButton = Me.cmdApply
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(551, 443)
        Me.Controls.Add(Me.gbCommands)
        Me.Controls.Add(Me.tcSettings)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.cmdApply)
        Me.Controls.Add(Me.cmdExit)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettings"
        Me.Text = "Settings"
        Me.tcSettings.ResumeLayout(False)
        Me.tpGeneral.ResumeLayout(False)
        Me.tpGeneral.PerformLayout()
        Me.tpDatabase.ResumeLayout(False)
        Me.tpDatabase.PerformLayout()
        Me.gbCommands.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblLanguage As System.Windows.Forms.Label
    Friend WithEvents cbLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents cmdApply As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents cbStartWithWin As System.Windows.Forms.ComboBox
    Friend WithEvents lblStartWithWin As System.Windows.Forms.Label
    Friend WithEvents txtDelayTime As System.Windows.Forms.TextBox
    Friend WithEvents lblDelayTime As System.Windows.Forms.Label
    Friend WithEvents lblSecs As System.Windows.Forms.Label
    Friend WithEvents cbShowStartupForm As System.Windows.Forms.ComboBox
    Friend WithEvents lblShowStartupForm As System.Windows.Forms.Label
    Friend WithEvents tcSettings As System.Windows.Forms.TabControl
    Friend WithEvents tpGeneral As System.Windows.Forms.TabPage
    Friend WithEvents btnSkinAdvanced As System.Windows.Forms.Button
    Friend WithEvents lblSkin As System.Windows.Forms.Label
    Friend WithEvents cbSkin As System.Windows.Forms.ComboBox
    Friend WithEvents gbCommands As System.Windows.Forms.GroupBox
    Friend WithEvents cmdCurrent As System.Windows.Forms.Button
    Friend WithEvents cmdDefault As System.Windows.Forms.Button
    Friend WithEvents tpDatabase As System.Windows.Forms.TabPage
    Friend WithEvents txtDBpass As System.Windows.Forms.TextBox
    Friend WithEvents lblDBpass As System.Windows.Forms.Label
    Friend WithEvents txtDBFile As System.Windows.Forms.TextBox
    Friend WithEvents lblDBFile As System.Windows.Forms.Label
    Friend WithEvents btnBrowseDBPath As System.Windows.Forms.Button
    Friend WithEvents ofdFileBrowser As System.Windows.Forms.OpenFileDialog
    Private WithEvents fbdPathBrowser As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents cbCheckForNewVersion As System.Windows.Forms.ComboBox
    Friend WithEvents lblCheckForNewVersion As System.Windows.Forms.Label
    Friend WithEvents cbAccessType As System.Windows.Forms.ComboBox
    Friend WithEvents cbSplitDbEveryMonth As System.Windows.Forms.ComboBox
    Friend WithEvents lblSplitDbEveryMonth As System.Windows.Forms.Label
    Friend WithEvents txtDatabaseTables As System.Windows.Forms.TextBox
    Friend WithEvents lblDatabaseTables As System.Windows.Forms.Label
    Friend WithEvents btnDatabaseTables As System.Windows.Forms.Button
    Friend WithEvents cbRemWindowState As System.Windows.Forms.ComboBox
    Friend WithEvents lblRemWindowState As System.Windows.Forms.Label
    Friend WithEvents lblX As System.Windows.Forms.Label
    Friend WithEvents txtWindowResolutionHeight As System.Windows.Forms.TextBox
    Friend WithEvents txtWindowResolutionWidth As System.Windows.Forms.TextBox
    Friend WithEvents lblWindowResolution As System.Windows.Forms.Label
    Friend WithEvents cbFullScreenResolutions As System.Windows.Forms.ComboBox
    Friend WithEvents lblFullScreenResolutions As System.Windows.Forms.Label
    Friend WithEvents cbWindowState As System.Windows.Forms.ComboBox
    Friend WithEvents lblWindowState As System.Windows.Forms.Label
    Friend WithEvents txtProtectedTables As System.Windows.Forms.TextBox
    Friend WithEvents btnProtectedTables As System.Windows.Forms.Button
    Friend WithEvents lblProtectedTables As System.Windows.Forms.Label
    Friend WithEvents lblDBtype As Label
End Class
