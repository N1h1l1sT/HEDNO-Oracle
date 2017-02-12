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
        Me.tpRGeneral = New System.Windows.Forms.TabPage()
        Me.lblRoundAt = New System.Windows.Forms.Label()
        Me.txtRoundAt = New System.Windows.Forms.TextBox()
        Me.btnXDFPath = New System.Windows.Forms.Button()
        Me.txtRowsPerRead = New System.Windows.Forms.TextBox()
        Me.lblXDFPath = New System.Windows.Forms.Label()
        Me.txtXDFPath = New System.Windows.Forms.TextBox()
        Me.lblRowsPerRead = New System.Windows.Forms.Label()
        Me.tpGeolocation = New System.Windows.Forms.TabPage()
        Me.txtColID_Erga = New System.Windows.Forms.TextBox()
        Me.lblColID_Erga = New System.Windows.Forms.Label()
        Me.txtTablevErga = New System.Windows.Forms.TextBox()
        Me.lblTablevErga = New System.Windows.Forms.Label()
        Me.txtColGeoLocY = New System.Windows.Forms.TextBox()
        Me.lblColGeoLocY = New System.Windows.Forms.Label()
        Me.txtColGeoLocX = New System.Windows.Forms.TextBox()
        Me.lblColGeoLocX = New System.Windows.Forms.Label()
        Me.txtColCityName = New System.Windows.Forms.TextBox()
        Me.lblColCityName = New System.Windows.Forms.Label()
        Me.txtTableErga = New System.Windows.Forms.TextBox()
        Me.lblTableErga = New System.Windows.Forms.Label()
        Me.txtColvID_Erga = New System.Windows.Forms.TextBox()
        Me.lblColvID_Erga = New System.Windows.Forms.Label()
        Me.txtColvGeoLocY = New System.Windows.Forms.TextBox()
        Me.lblColvGeoLocY = New System.Windows.Forms.Label()
        Me.txtColvGeoLocX = New System.Windows.Forms.TextBox()
        Me.lblColvGeoLocX = New System.Windows.Forms.Label()
        Me.txtColvCityName = New System.Windows.Forms.TextBox()
        Me.lblColvCityName = New System.Windows.Forms.Label()
        Me.txtAPIKey = New System.Windows.Forms.TextBox()
        Me.lblAPIKey = New System.Windows.Forms.Label()
        Me.txtCityFieldSuffix = New System.Windows.Forms.TextBox()
        Me.txtAPIExceededQuotaError = New System.Windows.Forms.TextBox()
        Me.txtErrorMessageIdentifierInJSON = New System.Windows.Forms.TextBox()
        Me.txtGeoLocationAPILink = New System.Windows.Forms.TextBox()
        Me.lblCityFieldSuffix = New System.Windows.Forms.Label()
        Me.lblAPIExceededQuotaError = New System.Windows.Forms.Label()
        Me.lblErrorMessageIdentifierInJSON = New System.Windows.Forms.Label()
        Me.lblGeoLocationAPILink = New System.Windows.Forms.Label()
        Me.gbCommands = New System.Windows.Forms.GroupBox()
        Me.cmdCurrent = New System.Windows.Forms.Button()
        Me.cmdDefault = New System.Windows.Forms.Button()
        Me.ofdFileBrowser = New System.Windows.Forms.OpenFileDialog()
        Me.lblRSQLConnStr = New System.Windows.Forms.Label()
        Me.txtRSQLConnStr = New System.Windows.Forms.TextBox()
        Me.tcSettings.SuspendLayout()
        Me.tpGeneral.SuspendLayout()
        Me.tpDatabase.SuspendLayout()
        Me.tpRGeneral.SuspendLayout()
        Me.tpGeolocation.SuspendLayout()
        Me.gbCommands.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSecs
        '
        Me.lblSecs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSecs.AutoSize = True
        Me.lblSecs.Location = New System.Drawing.Point(511, 100)
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
        Me.txtDelayTime.Size = New System.Drawing.Size(258, 20)
        Me.txtDelayTime.TabIndex = 4
        Me.txtDelayTime.Tag = "0"
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
        Me.cbStartWithWin.Size = New System.Drawing.Size(327, 21)
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
        Me.cbLanguage.Size = New System.Drawing.Size(326, 21)
        Me.cbLanguage.TabIndex = 0
        Me.cbLanguage.Tag = "0"
        Me.cbLanguage.Text = "Unknown"
        '
        'lblLanguage
        '
        Me.lblLanguage.AutoSize = True
        Me.lblLanguage.BackColor = System.Drawing.Color.Black
        Me.lblLanguage.ForeColor = System.Drawing.Color.Gold
        Me.lblLanguage.Location = New System.Drawing.Point(6, 19)
        Me.lblLanguage.Name = "lblLanguage"
        Me.lblLanguage.Size = New System.Drawing.Size(58, 13)
        Me.lblLanguage.TabIndex = 15
        Me.lblLanguage.Text = "Language:"
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.Location = New System.Drawing.Point(27, 547)
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
        Me.cmdApply.Location = New System.Drawing.Point(488, 547)
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
        Me.cbShowStartupForm.Size = New System.Drawing.Size(326, 21)
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
        Me.tcSettings.Controls.Add(Me.tpRGeneral)
        Me.tcSettings.Controls.Add(Me.tpGeolocation)
        Me.tcSettings.Location = New System.Drawing.Point(27, 94)
        Me.tcSettings.Name = "tcSettings"
        Me.tcSettings.SelectedIndex = 0
        Me.tcSettings.Size = New System.Drawing.Size(594, 439)
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
        Me.tpGeneral.Size = New System.Drawing.Size(586, 413)
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
        Me.cbRemWindowState.Size = New System.Drawing.Size(326, 21)
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
        Me.txtWindowResolutionHeight.Tag = "0"
        Me.txtWindowResolutionHeight.Text = "Unknown"
        '
        'txtWindowResolutionWidth
        '
        Me.txtWindowResolutionWidth.Location = New System.Drawing.Point(252, 255)
        Me.txtWindowResolutionWidth.Name = "txtWindowResolutionWidth"
        Me.txtWindowResolutionWidth.ReadOnly = True
        Me.txtWindowResolutionWidth.Size = New System.Drawing.Size(104, 20)
        Me.txtWindowResolutionWidth.TabIndex = 61
        Me.txtWindowResolutionWidth.Tag = "0"
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
        Me.cbWindowState.Size = New System.Drawing.Size(326, 21)
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
        Me.cbCheckForNewVersion.Size = New System.Drawing.Size(326, 21)
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
        Me.btnSkinAdvanced.Location = New System.Drawing.Point(554, 42)
        Me.btnSkinAdvanced.Name = "btnSkinAdvanced"
        Me.btnSkinAdvanced.Size = New System.Drawing.Size(25, 23)
        Me.btnSkinAdvanced.TabIndex = 2
        Me.btnSkinAdvanced.Tag = "0"
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
        Me.cbSkin.Size = New System.Drawing.Size(297, 21)
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
        Me.tpDatabase.Size = New System.Drawing.Size(586, 413)
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
        Me.txtProtectedTables.Size = New System.Drawing.Size(296, 20)
        Me.txtProtectedTables.TabIndex = 71
        Me.txtProtectedTables.Tag = "0"
        Me.txtProtectedTables.Text = "Unknown"
        '
        'btnProtectedTables
        '
        Me.btnProtectedTables.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnProtectedTables.Location = New System.Drawing.Point(553, 149)
        Me.btnProtectedTables.Name = "btnProtectedTables"
        Me.btnProtectedTables.Size = New System.Drawing.Size(25, 23)
        Me.btnProtectedTables.TabIndex = 72
        Me.btnProtectedTables.Tag = "0"
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
        Me.txtDatabaseTables.Size = New System.Drawing.Size(296, 20)
        Me.txtDatabaseTables.TabIndex = 62
        Me.txtDatabaseTables.Tag = "0"
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
        Me.cbSplitDbEveryMonth.Size = New System.Drawing.Size(326, 21)
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
        Me.btnDatabaseTables.Location = New System.Drawing.Point(553, 123)
        Me.btnDatabaseTables.Name = "btnDatabaseTables"
        Me.btnDatabaseTables.Size = New System.Drawing.Size(25, 23)
        Me.btnDatabaseTables.TabIndex = 63
        Me.btnDatabaseTables.Tag = "0"
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
        Me.cbAccessType.Size = New System.Drawing.Size(326, 21)
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
        Me.txtDBpass.Size = New System.Drawing.Size(326, 20)
        Me.txtDBpass.TabIndex = 6
        Me.txtDBpass.Tag = "0"
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
        Me.txtDBFile.Size = New System.Drawing.Size(296, 20)
        Me.txtDBFile.TabIndex = 0
        Me.txtDBFile.Tag = "0"
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
        Me.btnBrowseDBPath.Location = New System.Drawing.Point(553, 94)
        Me.btnBrowseDBPath.Name = "btnBrowseDBPath"
        Me.btnBrowseDBPath.Size = New System.Drawing.Size(25, 23)
        Me.btnBrowseDBPath.TabIndex = 1
        Me.btnBrowseDBPath.Tag = "0"
        Me.btnBrowseDBPath.Text = "..."
        Me.btnBrowseDBPath.UseVisualStyleBackColor = True
        '
        'tpRGeneral
        '
        Me.tpRGeneral.Controls.Add(Me.lblRSQLConnStr)
        Me.tpRGeneral.Controls.Add(Me.txtRSQLConnStr)
        Me.tpRGeneral.Controls.Add(Me.lblRoundAt)
        Me.tpRGeneral.Controls.Add(Me.txtRoundAt)
        Me.tpRGeneral.Controls.Add(Me.btnXDFPath)
        Me.tpRGeneral.Controls.Add(Me.txtRowsPerRead)
        Me.tpRGeneral.Controls.Add(Me.lblXDFPath)
        Me.tpRGeneral.Controls.Add(Me.txtXDFPath)
        Me.tpRGeneral.Controls.Add(Me.lblRowsPerRead)
        Me.tpRGeneral.Location = New System.Drawing.Point(4, 22)
        Me.tpRGeneral.Name = "tpRGeneral"
        Me.tpRGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.tpRGeneral.Size = New System.Drawing.Size(586, 413)
        Me.tpRGeneral.TabIndex = 3
        Me.tpRGeneral.Text = "R (General)"
        Me.tpRGeneral.UseVisualStyleBackColor = True
        '
        'lblRoundAt
        '
        Me.lblRoundAt.AutoSize = True
        Me.lblRoundAt.Location = New System.Drawing.Point(6, 71)
        Me.lblRoundAt.Name = "lblRoundAt"
        Me.lblRoundAt.Size = New System.Drawing.Size(54, 13)
        Me.lblRoundAt.TabIndex = 7
        Me.lblRoundAt.Text = "Round at:"
        '
        'txtRoundAt
        '
        Me.txtRoundAt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRoundAt.Location = New System.Drawing.Point(252, 68)
        Me.txtRoundAt.Name = "txtRoundAt"
        Me.txtRoundAt.ReadOnly = True
        Me.txtRoundAt.Size = New System.Drawing.Size(328, 20)
        Me.txtRoundAt.TabIndex = 6
        Me.txtRoundAt.Tag = "0"
        Me.txtRoundAt.Text = "Unknown"
        '
        'btnXDFPath
        '
        Me.btnXDFPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnXDFPath.Location = New System.Drawing.Point(555, 42)
        Me.btnXDFPath.Name = "btnXDFPath"
        Me.btnXDFPath.Size = New System.Drawing.Size(25, 23)
        Me.btnXDFPath.TabIndex = 5
        Me.btnXDFPath.Tag = "0"
        Me.btnXDFPath.Text = "..."
        Me.btnXDFPath.UseVisualStyleBackColor = True
        '
        'txtRowsPerRead
        '
        Me.txtRowsPerRead.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRowsPerRead.Location = New System.Drawing.Point(252, 16)
        Me.txtRowsPerRead.Name = "txtRowsPerRead"
        Me.txtRowsPerRead.ReadOnly = True
        Me.txtRowsPerRead.Size = New System.Drawing.Size(328, 20)
        Me.txtRowsPerRead.TabIndex = 4
        Me.txtRowsPerRead.Tag = "0"
        Me.txtRowsPerRead.Text = "Unknown"
        '
        'lblXDFPath
        '
        Me.lblXDFPath.AutoSize = True
        Me.lblXDFPath.Location = New System.Drawing.Point(6, 45)
        Me.lblXDFPath.Name = "lblXDFPath"
        Me.lblXDFPath.Size = New System.Drawing.Size(146, 13)
        Me.lblXDFPath.TabIndex = 3
        Me.lblXDFPath.Text = "Path to Save/Load XDF files:"
        '
        'txtXDFPath
        '
        Me.txtXDFPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtXDFPath.Location = New System.Drawing.Point(252, 42)
        Me.txtXDFPath.Name = "txtXDFPath"
        Me.txtXDFPath.Size = New System.Drawing.Size(297, 20)
        Me.txtXDFPath.TabIndex = 2
        Me.txtXDFPath.Tag = "0"
        Me.txtXDFPath.Text = "Unknown"
        '
        'lblRowsPerRead
        '
        Me.lblRowsPerRead.AutoSize = True
        Me.lblRowsPerRead.Location = New System.Drawing.Point(6, 19)
        Me.lblRowsPerRead.Name = "lblRowsPerRead"
        Me.lblRowsPerRead.Size = New System.Drawing.Size(84, 13)
        Me.lblRowsPerRead.TabIndex = 0
        Me.lblRowsPerRead.Text = "Rows per Read:"
        '
        'tpGeolocation
        '
        Me.tpGeolocation.Controls.Add(Me.txtColID_Erga)
        Me.tpGeolocation.Controls.Add(Me.lblColID_Erga)
        Me.tpGeolocation.Controls.Add(Me.txtTablevErga)
        Me.tpGeolocation.Controls.Add(Me.lblTablevErga)
        Me.tpGeolocation.Controls.Add(Me.txtColGeoLocY)
        Me.tpGeolocation.Controls.Add(Me.lblColGeoLocY)
        Me.tpGeolocation.Controls.Add(Me.txtColGeoLocX)
        Me.tpGeolocation.Controls.Add(Me.lblColGeoLocX)
        Me.tpGeolocation.Controls.Add(Me.txtColCityName)
        Me.tpGeolocation.Controls.Add(Me.lblColCityName)
        Me.tpGeolocation.Controls.Add(Me.txtTableErga)
        Me.tpGeolocation.Controls.Add(Me.lblTableErga)
        Me.tpGeolocation.Controls.Add(Me.txtColvID_Erga)
        Me.tpGeolocation.Controls.Add(Me.lblColvID_Erga)
        Me.tpGeolocation.Controls.Add(Me.txtColvGeoLocY)
        Me.tpGeolocation.Controls.Add(Me.lblColvGeoLocY)
        Me.tpGeolocation.Controls.Add(Me.txtColvGeoLocX)
        Me.tpGeolocation.Controls.Add(Me.lblColvGeoLocX)
        Me.tpGeolocation.Controls.Add(Me.txtColvCityName)
        Me.tpGeolocation.Controls.Add(Me.lblColvCityName)
        Me.tpGeolocation.Controls.Add(Me.txtAPIKey)
        Me.tpGeolocation.Controls.Add(Me.lblAPIKey)
        Me.tpGeolocation.Controls.Add(Me.txtCityFieldSuffix)
        Me.tpGeolocation.Controls.Add(Me.txtAPIExceededQuotaError)
        Me.tpGeolocation.Controls.Add(Me.txtErrorMessageIdentifierInJSON)
        Me.tpGeolocation.Controls.Add(Me.txtGeoLocationAPILink)
        Me.tpGeolocation.Controls.Add(Me.lblCityFieldSuffix)
        Me.tpGeolocation.Controls.Add(Me.lblAPIExceededQuotaError)
        Me.tpGeolocation.Controls.Add(Me.lblErrorMessageIdentifierInJSON)
        Me.tpGeolocation.Controls.Add(Me.lblGeoLocationAPILink)
        Me.tpGeolocation.Location = New System.Drawing.Point(4, 22)
        Me.tpGeolocation.Name = "tpGeolocation"
        Me.tpGeolocation.Size = New System.Drawing.Size(586, 413)
        Me.tpGeolocation.TabIndex = 4
        Me.tpGeolocation.Text = "Geolocation"
        Me.tpGeolocation.UseVisualStyleBackColor = True
        '
        'txtColID_Erga
        '
        Me.txtColID_Erga.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtColID_Erga.Location = New System.Drawing.Point(252, 380)
        Me.txtColID_Erga.Name = "txtColID_Erga"
        Me.txtColID_Erga.Size = New System.Drawing.Size(326, 20)
        Me.txtColID_Erga.TabIndex = 32
        Me.txtColID_Erga.Tag = "0"
        Me.txtColID_Erga.Text = "Unknown"
        '
        'lblColID_Erga
        '
        Me.lblColID_Erga.AutoSize = True
        Me.lblColID_Erga.Location = New System.Drawing.Point(6, 383)
        Me.lblColID_Erga.Name = "lblColID_Erga"
        Me.lblColID_Erga.Size = New System.Drawing.Size(146, 13)
        Me.lblColID_Erga.TabIndex = 33
        Me.lblColID_Erga.Text = "Suffix for each Address Entry:"
        '
        'txtTablevErga
        '
        Me.txtTablevErga.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTablevErga.Location = New System.Drawing.Point(252, 172)
        Me.txtTablevErga.Name = "txtTablevErga"
        Me.txtTablevErga.Size = New System.Drawing.Size(326, 20)
        Me.txtTablevErga.TabIndex = 30
        Me.txtTablevErga.Tag = "0"
        Me.txtTablevErga.Text = "Unknown"
        '
        'lblTablevErga
        '
        Me.lblTablevErga.AutoSize = True
        Me.lblTablevErga.Location = New System.Drawing.Point(6, 175)
        Me.lblTablevErga.Name = "lblTablevErga"
        Me.lblTablevErga.Size = New System.Drawing.Size(121, 13)
        Me.lblTablevErga.TabIndex = 31
        Me.lblTablevErga.Text = "SQL View ERGA Name:"
        '
        'txtColGeoLocY
        '
        Me.txtColGeoLocY.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtColGeoLocY.Location = New System.Drawing.Point(252, 354)
        Me.txtColGeoLocY.Name = "txtColGeoLocY"
        Me.txtColGeoLocY.Size = New System.Drawing.Size(326, 20)
        Me.txtColGeoLocY.TabIndex = 28
        Me.txtColGeoLocY.Tag = "0"
        Me.txtColGeoLocY.Text = "Unknown"
        '
        'lblColGeoLocY
        '
        Me.lblColGeoLocY.AutoSize = True
        Me.lblColGeoLocY.Location = New System.Drawing.Point(6, 357)
        Me.lblColGeoLocY.Name = "lblColGeoLocY"
        Me.lblColGeoLocY.Size = New System.Drawing.Size(163, 13)
        Me.lblColGeoLocY.TabIndex = 29
        Me.lblColGeoLocY.Text = "SQL Table GeoLocationY Name:"
        '
        'txtColGeoLocX
        '
        Me.txtColGeoLocX.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtColGeoLocX.Location = New System.Drawing.Point(252, 328)
        Me.txtColGeoLocX.Name = "txtColGeoLocX"
        Me.txtColGeoLocX.Size = New System.Drawing.Size(326, 20)
        Me.txtColGeoLocX.TabIndex = 26
        Me.txtColGeoLocX.Tag = "0"
        Me.txtColGeoLocX.Text = "Unknown"
        '
        'lblColGeoLocX
        '
        Me.lblColGeoLocX.AutoSize = True
        Me.lblColGeoLocX.Location = New System.Drawing.Point(6, 331)
        Me.lblColGeoLocX.Name = "lblColGeoLocX"
        Me.lblColGeoLocX.Size = New System.Drawing.Size(163, 13)
        Me.lblColGeoLocX.TabIndex = 27
        Me.lblColGeoLocX.Text = "SQL Table GeoLocationX Name:"
        '
        'txtColCityName
        '
        Me.txtColCityName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtColCityName.Location = New System.Drawing.Point(252, 302)
        Me.txtColCityName.Name = "txtColCityName"
        Me.txtColCityName.Size = New System.Drawing.Size(326, 20)
        Me.txtColCityName.TabIndex = 24
        Me.txtColCityName.Tag = "0"
        Me.txtColCityName.Text = "Unknown"
        '
        'lblColCityName
        '
        Me.lblColCityName.AutoSize = True
        Me.lblColCityName.Location = New System.Drawing.Point(6, 305)
        Me.lblColCityName.Name = "lblColCityName"
        Me.lblColCityName.Size = New System.Drawing.Size(150, 13)
        Me.lblColCityName.TabIndex = 25
        Me.lblColCityName.Text = "SQL Table City Column Name:"
        '
        'txtTableErga
        '
        Me.txtTableErga.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTableErga.Location = New System.Drawing.Point(252, 276)
        Me.txtTableErga.Name = "txtTableErga"
        Me.txtTableErga.Size = New System.Drawing.Size(326, 20)
        Me.txtTableErga.TabIndex = 22
        Me.txtTableErga.Tag = "0"
        Me.txtTableErga.Text = "Unknown"
        '
        'lblTableErga
        '
        Me.lblTableErga.AutoSize = True
        Me.lblTableErga.Location = New System.Drawing.Point(6, 279)
        Me.lblTableErga.Name = "lblTableErga"
        Me.lblTableErga.Size = New System.Drawing.Size(121, 13)
        Me.lblTableErga.TabIndex = 23
        Me.lblTableErga.Text = "SQL Table ΕΡΓΑ Name:"
        '
        'txtColvID_Erga
        '
        Me.txtColvID_Erga.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtColvID_Erga.Location = New System.Drawing.Point(252, 250)
        Me.txtColvID_Erga.Name = "txtColvID_Erga"
        Me.txtColvID_Erga.Size = New System.Drawing.Size(326, 20)
        Me.txtColvID_Erga.TabIndex = 20
        Me.txtColvID_Erga.Tag = "0"
        Me.txtColvID_Erga.Text = "Unknown"
        '
        'lblColvID_Erga
        '
        Me.lblColvID_Erga.AutoSize = True
        Me.lblColvID_Erga.Location = New System.Drawing.Point(6, 253)
        Me.lblColvID_Erga.Name = "lblColvID_Erga"
        Me.lblColvID_Erga.Size = New System.Drawing.Size(140, 13)
        Me.lblColvID_Erga.TabIndex = 21
        Me.lblColvID_Erga.Text = "SQL View Column ID Name:"
        '
        'txtColvGeoLocY
        '
        Me.txtColvGeoLocY.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtColvGeoLocY.Location = New System.Drawing.Point(252, 224)
        Me.txtColvGeoLocY.Name = "txtColvGeoLocY"
        Me.txtColvGeoLocY.Size = New System.Drawing.Size(326, 20)
        Me.txtColvGeoLocY.TabIndex = 18
        Me.txtColvGeoLocY.Tag = "0"
        Me.txtColvGeoLocY.Text = "Unknown"
        '
        'lblColvGeoLocY
        '
        Me.lblColvGeoLocY.AutoSize = True
        Me.lblColvGeoLocY.Location = New System.Drawing.Point(6, 227)
        Me.lblColvGeoLocY.Name = "lblColvGeoLocY"
        Me.lblColvGeoLocY.Size = New System.Drawing.Size(197, 13)
        Me.lblColvGeoLocY.TabIndex = 19
        Me.lblColvGeoLocY.Text = "SQL View Column GeoLocationY Name:"
        '
        'txtColvGeoLocX
        '
        Me.txtColvGeoLocX.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtColvGeoLocX.Location = New System.Drawing.Point(252, 198)
        Me.txtColvGeoLocX.Name = "txtColvGeoLocX"
        Me.txtColvGeoLocX.Size = New System.Drawing.Size(326, 20)
        Me.txtColvGeoLocX.TabIndex = 16
        Me.txtColvGeoLocX.Tag = "0"
        Me.txtColvGeoLocX.Text = "Unknown"
        '
        'lblColvGeoLocX
        '
        Me.lblColvGeoLocX.AutoSize = True
        Me.lblColvGeoLocX.Location = New System.Drawing.Point(6, 201)
        Me.lblColvGeoLocX.Name = "lblColvGeoLocX"
        Me.lblColvGeoLocX.Size = New System.Drawing.Size(197, 13)
        Me.lblColvGeoLocX.TabIndex = 17
        Me.lblColvGeoLocX.Text = "SQL View Column GeoLocationX Name:"
        '
        'txtColvCityName
        '
        Me.txtColvCityName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtColvCityName.Location = New System.Drawing.Point(252, 146)
        Me.txtColvCityName.Name = "txtColvCityName"
        Me.txtColvCityName.Size = New System.Drawing.Size(326, 20)
        Me.txtColvCityName.TabIndex = 14
        Me.txtColvCityName.Tag = "0"
        Me.txtColvCityName.Text = "Unknown"
        '
        'lblColvCityName
        '
        Me.lblColvCityName.AutoSize = True
        Me.lblColvCityName.Location = New System.Drawing.Point(6, 149)
        Me.lblColvCityName.Name = "lblColvCityName"
        Me.lblColvCityName.Size = New System.Drawing.Size(146, 13)
        Me.lblColvCityName.TabIndex = 15
        Me.lblColvCityName.Text = "SQL View City Column Name:"
        '
        'txtAPIKey
        '
        Me.txtAPIKey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAPIKey.Location = New System.Drawing.Point(252, 42)
        Me.txtAPIKey.Name = "txtAPIKey"
        Me.txtAPIKey.Size = New System.Drawing.Size(326, 20)
        Me.txtAPIKey.TabIndex = 12
        Me.txtAPIKey.Tag = "0"
        Me.txtAPIKey.Text = "Unknown"
        '
        'lblAPIKey
        '
        Me.lblAPIKey.AutoSize = True
        Me.lblAPIKey.Location = New System.Drawing.Point(6, 45)
        Me.lblAPIKey.Name = "lblAPIKey"
        Me.lblAPIKey.Size = New System.Drawing.Size(108, 13)
        Me.lblAPIKey.TabIndex = 13
        Me.lblAPIKey.Text = "Geolocation API Key:"
        '
        'txtCityFieldSuffix
        '
        Me.txtCityFieldSuffix.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCityFieldSuffix.Location = New System.Drawing.Point(252, 120)
        Me.txtCityFieldSuffix.Name = "txtCityFieldSuffix"
        Me.txtCityFieldSuffix.Size = New System.Drawing.Size(326, 20)
        Me.txtCityFieldSuffix.TabIndex = 10
        Me.txtCityFieldSuffix.Tag = "0"
        Me.txtCityFieldSuffix.Text = "Unknown"
        '
        'txtAPIExceededQuotaError
        '
        Me.txtAPIExceededQuotaError.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAPIExceededQuotaError.Location = New System.Drawing.Point(252, 94)
        Me.txtAPIExceededQuotaError.Name = "txtAPIExceededQuotaError"
        Me.txtAPIExceededQuotaError.Size = New System.Drawing.Size(326, 20)
        Me.txtAPIExceededQuotaError.TabIndex = 8
        Me.txtAPIExceededQuotaError.Tag = "0"
        Me.txtAPIExceededQuotaError.Text = "Unknown"
        '
        'txtErrorMessageIdentifierInJSON
        '
        Me.txtErrorMessageIdentifierInJSON.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtErrorMessageIdentifierInJSON.Location = New System.Drawing.Point(252, 68)
        Me.txtErrorMessageIdentifierInJSON.Name = "txtErrorMessageIdentifierInJSON"
        Me.txtErrorMessageIdentifierInJSON.Size = New System.Drawing.Size(326, 20)
        Me.txtErrorMessageIdentifierInJSON.TabIndex = 6
        Me.txtErrorMessageIdentifierInJSON.Tag = "0"
        Me.txtErrorMessageIdentifierInJSON.Text = "Unknown"
        '
        'txtGeoLocationAPILink
        '
        Me.txtGeoLocationAPILink.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGeoLocationAPILink.Location = New System.Drawing.Point(252, 16)
        Me.txtGeoLocationAPILink.Name = "txtGeoLocationAPILink"
        Me.txtGeoLocationAPILink.Size = New System.Drawing.Size(326, 20)
        Me.txtGeoLocationAPILink.TabIndex = 4
        Me.txtGeoLocationAPILink.Tag = "0"
        Me.txtGeoLocationAPILink.Text = "Unknown"
        '
        'lblCityFieldSuffix
        '
        Me.lblCityFieldSuffix.AutoSize = True
        Me.lblCityFieldSuffix.Location = New System.Drawing.Point(6, 123)
        Me.lblCityFieldSuffix.Name = "lblCityFieldSuffix"
        Me.lblCityFieldSuffix.Size = New System.Drawing.Size(146, 13)
        Me.lblCityFieldSuffix.TabIndex = 11
        Me.lblCityFieldSuffix.Text = "Suffix for each Address Entry:"
        '
        'lblAPIExceededQuotaError
        '
        Me.lblAPIExceededQuotaError.AutoSize = True
        Me.lblAPIExceededQuotaError.Location = New System.Drawing.Point(6, 97)
        Me.lblAPIExceededQuotaError.Name = "lblAPIExceededQuotaError"
        Me.lblAPIExceededQuotaError.Size = New System.Drawing.Size(135, 13)
        Me.lblAPIExceededQuotaError.TabIndex = 9
        Me.lblAPIExceededQuotaError.Text = "API Exceeded Quota Error:"
        '
        'lblErrorMessageIdentifierInJSON
        '
        Me.lblErrorMessageIdentifierInJSON.AutoSize = True
        Me.lblErrorMessageIdentifierInJSON.Location = New System.Drawing.Point(6, 71)
        Me.lblErrorMessageIdentifierInJSON.Name = "lblErrorMessageIdentifierInJSON"
        Me.lblErrorMessageIdentifierInJSON.Size = New System.Drawing.Size(223, 13)
        Me.lblErrorMessageIdentifierInJSON.TabIndex = 7
        Me.lblErrorMessageIdentifierInJSON.Text = "Error Message Identifier in Geolocation JSON:"
        '
        'lblGeoLocationAPILink
        '
        Me.lblGeoLocationAPILink.AutoSize = True
        Me.lblGeoLocationAPILink.Location = New System.Drawing.Point(6, 19)
        Me.lblGeoLocationAPILink.Name = "lblGeoLocationAPILink"
        Me.lblGeoLocationAPILink.Size = New System.Drawing.Size(110, 13)
        Me.lblGeoLocationAPILink.TabIndex = 5
        Me.lblGeoLocationAPILink.Text = "Geolocation API Link:"
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
        Me.gbCommands.Size = New System.Drawing.Size(605, 48)
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
        Me.cmdDefault.Location = New System.Drawing.Point(466, 19)
        Me.cmdDefault.Name = "cmdDefault"
        Me.cmdDefault.Size = New System.Drawing.Size(133, 23)
        Me.cmdDefault.TabIndex = 1
        Me.cmdDefault.Text = "Reset Default Settings"
        Me.cmdDefault.UseVisualStyleBackColor = True
        '
        'lblRSQLConnStr
        '
        Me.lblRSQLConnStr.AutoSize = True
        Me.lblRSQLConnStr.Location = New System.Drawing.Point(6, 97)
        Me.lblRSQLConnStr.Name = "lblRSQLConnStr"
        Me.lblRSQLConnStr.Size = New System.Drawing.Size(144, 13)
        Me.lblRSQLConnStr.TabIndex = 9
        Me.lblRSQLConnStr.Text = "SQL Connection String for R:"
        '
        'txtRSQLConnStr
        '
        Me.txtRSQLConnStr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRSQLConnStr.Location = New System.Drawing.Point(252, 94)
        Me.txtRSQLConnStr.Name = "txtRSQLConnStr"
        Me.txtRSQLConnStr.Size = New System.Drawing.Size(328, 20)
        Me.txtRSQLConnStr.TabIndex = 8
        Me.txtRSQLConnStr.Tag = "0"
        Me.txtRSQLConnStr.Text = "Unknown"
        '
        'frmSettings
        '
        Me.AcceptButton = Me.cmdApply
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(644, 570)
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
        Me.tpRGeneral.ResumeLayout(False)
        Me.tpRGeneral.PerformLayout()
        Me.tpGeolocation.ResumeLayout(False)
        Me.tpGeolocation.PerformLayout()
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
    Friend WithEvents tpRGeneral As TabPage
    Friend WithEvents lblRowsPerRead As Label
    Friend WithEvents btnXDFPath As Button
    Friend WithEvents txtRowsPerRead As TextBox
    Friend WithEvents lblXDFPath As Label
    Friend WithEvents txtXDFPath As TextBox
    Friend WithEvents lblRoundAt As Label
    Friend WithEvents txtRoundAt As TextBox
    Friend WithEvents tpGeolocation As TabPage
    Friend WithEvents lblGeoLocationAPILink As Label
    Friend WithEvents txtGeoLocationAPILink As TextBox
    Friend WithEvents lblCityFieldSuffix As Label
    Friend WithEvents txtCityFieldSuffix As TextBox
    Friend WithEvents lblAPIExceededQuotaError As Label
    Friend WithEvents txtAPIExceededQuotaError As TextBox
    Friend WithEvents lblErrorMessageIdentifierInJSON As Label
    Friend WithEvents txtErrorMessageIdentifierInJSON As TextBox
    Friend WithEvents txtAPIKey As TextBox
    Friend WithEvents lblAPIKey As Label
    Friend WithEvents txtColID_Erga As TextBox
    Friend WithEvents lblColID_Erga As Label
    Friend WithEvents txtTablevErga As TextBox
    Friend WithEvents lblTablevErga As Label
    Friend WithEvents txtColGeoLocY As TextBox
    Friend WithEvents lblColGeoLocY As Label
    Friend WithEvents txtColGeoLocX As TextBox
    Friend WithEvents lblColGeoLocX As Label
    Friend WithEvents txtColCityName As TextBox
    Friend WithEvents lblColCityName As Label
    Friend WithEvents txtTableErga As TextBox
    Friend WithEvents lblTableErga As Label
    Friend WithEvents txtColvID_Erga As TextBox
    Friend WithEvents lblColvID_Erga As Label
    Friend WithEvents txtColvGeoLocY As TextBox
    Friend WithEvents lblColvGeoLocY As Label
    Friend WithEvents txtColvGeoLocX As TextBox
    Friend WithEvents lblColvGeoLocX As Label
    Friend WithEvents txtColvCityName As TextBox
    Friend WithEvents lblColvCityName As Label
    Friend WithEvents lblRSQLConnStr As Label
    Friend WithEvents txtRSQLConnStr As TextBox
End Class
