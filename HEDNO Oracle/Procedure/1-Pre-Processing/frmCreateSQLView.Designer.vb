<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateSQLView
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
        Me.gbSQLViews = New System.Windows.Forms.GroupBox()
        Me.clbSQLViews = New System.Windows.Forms.CheckedListBox()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnCreateSQLViews = New System.Windows.Forms.Button()
        Me.fswSQLViewFiles = New System.IO.FileSystemWatcher()
        Me.gbSQLViewOptions = New System.Windows.Forms.GroupBox()
        Me.rdbDeleteItAndCreateIt = New System.Windows.Forms.RadioButton()
        Me.rdbAlterIt = New System.Windows.Forms.RadioButton()
        Me.rdbDoNothing = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkDeleteAll = New System.Windows.Forms.CheckBox()
        Me.lblWarning = New System.Windows.Forms.Label()
        Me.ttMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbSQLViews.SuspendLayout()
        CType(Me.fswSQLViewFiles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbSQLViewOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbSQLViews
        '
        Me.gbSQLViews.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbSQLViews.Controls.Add(Me.clbSQLViews)
        Me.gbSQLViews.Controls.Add(Me.btnSelectAll)
        Me.gbSQLViews.Location = New System.Drawing.Point(12, 12)
        Me.gbSQLViews.Name = "gbSQLViews"
        Me.gbSQLViews.Size = New System.Drawing.Size(185, 237)
        Me.gbSQLViews.TabIndex = 0
        Me.gbSQLViews.TabStop = False
        Me.gbSQLViews.Text = "Create SQL View for:"
        '
        'clbSQLViews
        '
        Me.clbSQLViews.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbSQLViews.FormattingEnabled = True
        Me.clbSQLViews.Location = New System.Drawing.Point(6, 19)
        Me.clbSQLViews.Name = "clbSQLViews"
        Me.clbSQLViews.Size = New System.Drawing.Size(173, 184)
        Me.clbSQLViews.TabIndex = 0
        Me.ttMain.SetToolTip(Me.clbSQLViews, "Select which SQL Views you wish to Create, Alter or Delete." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "The corresponding .S" &
        "QL Files are in the Programme's Install Directory under the folder ""SQL"" and sub" &
        "folder ""Views""")
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelectAll.Location = New System.Drawing.Point(6, 208)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(173, 23)
        Me.btnSelectAll.TabIndex = 3
        Me.btnSelectAll.Text = "Select &All"
        Me.ttMain.SetToolTip(Me.btnSelectAll, "Selects or Unselects all SQL Views")
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'btnCreateSQLViews
        '
        Me.btnCreateSQLViews.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreateSQLViews.Location = New System.Drawing.Point(12, 263)
        Me.btnCreateSQLViews.Name = "btnCreateSQLViews"
        Me.btnCreateSQLViews.Size = New System.Drawing.Size(375, 23)
        Me.btnCreateSQLViews.TabIndex = 1
        Me.btnCreateSQLViews.Text = "&Create SQL Views"
        Me.ttMain.SetToolTip(Me.btnCreateSQLViews, "Needs: An active connection to the SQL Server." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Does: Creates, Alters, or Deletes" &
        " the SQL Views you've selected from the ListBox according to the option you sele" &
        "cted from the RadioButtons")
        Me.btnCreateSQLViews.UseVisualStyleBackColor = True
        '
        'fswSQLViewFiles
        '
        Me.fswSQLViewFiles.EnableRaisingEvents = True
        Me.fswSQLViewFiles.Filter = "*.SQL"
        Me.fswSQLViewFiles.NotifyFilter = System.IO.NotifyFilters.FileName
        Me.fswSQLViewFiles.SynchronizingObject = Me
        '
        'gbSQLViewOptions
        '
        Me.gbSQLViewOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbSQLViewOptions.Controls.Add(Me.rdbDeleteItAndCreateIt)
        Me.gbSQLViewOptions.Controls.Add(Me.rdbAlterIt)
        Me.gbSQLViewOptions.Controls.Add(Me.rdbDoNothing)
        Me.gbSQLViewOptions.Controls.Add(Me.Label1)
        Me.gbSQLViewOptions.Location = New System.Drawing.Point(202, 12)
        Me.gbSQLViewOptions.Name = "gbSQLViewOptions"
        Me.gbSQLViewOptions.Size = New System.Drawing.Size(185, 110)
        Me.gbSQLViewOptions.TabIndex = 1
        Me.gbSQLViewOptions.TabStop = False
        Me.gbSQLViewOptions.Text = "Options for SQL View creation:"
        '
        'rdbDeleteItAndCreateIt
        '
        Me.rdbDeleteItAndCreateIt.AutoSize = True
        Me.rdbDeleteItAndCreateIt.Location = New System.Drawing.Point(10, 83)
        Me.rdbDeleteItAndCreateIt.Name = "rdbDeleteItAndCreateIt"
        Me.rdbDeleteItAndCreateIt.Size = New System.Drawing.Size(127, 17)
        Me.rdbDeleteItAndCreateIt.TabIndex = 3
        Me.rdbDeleteItAndCreateIt.Text = "D&elete it and Create it"
        Me.ttMain.SetToolTip(Me.rdbDeleteItAndCreateIt, "If the SQL View is already there, then it will delete the view first, and then cr" &
        "eate it anew")
        Me.rdbDeleteItAndCreateIt.UseVisualStyleBackColor = True
        '
        'rdbAlterIt
        '
        Me.rdbAlterIt.AutoSize = True
        Me.rdbAlterIt.Location = New System.Drawing.Point(10, 60)
        Me.rdbAlterIt.Name = "rdbAlterIt"
        Me.rdbAlterIt.Size = New System.Drawing.Size(54, 17)
        Me.rdbAlterIt.TabIndex = 2
        Me.rdbAlterIt.Text = "A&lter it"
        Me.ttMain.SetToolTip(Me.rdbAlterIt, "If the SQL View is already there, then it will alter it")
        Me.rdbAlterIt.UseVisualStyleBackColor = True
        '
        'rdbDoNothing
        '
        Me.rdbDoNothing.AutoSize = True
        Me.rdbDoNothing.Checked = True
        Me.rdbDoNothing.Location = New System.Drawing.Point(10, 37)
        Me.rdbDoNothing.Name = "rdbDoNothing"
        Me.rdbDoNothing.Size = New System.Drawing.Size(79, 17)
        Me.rdbDoNothing.TabIndex = 1
        Me.rdbDoNothing.TabStop = True
        Me.rdbDoNothing.Text = "Do &Nothing"
        Me.ttMain.SetToolTip(Me.rdbDoNothing, "If the SQL View is already there, then it will not create, alter or delete it")
        Me.rdbDoNothing.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(152, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "If SQL View is already created:"
        '
        'chkDeleteAll
        '
        Me.chkDeleteAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkDeleteAll.AutoSize = True
        Me.chkDeleteAll.Location = New System.Drawing.Point(203, 128)
        Me.chkDeleteAll.Name = "chkDeleteAll"
        Me.chkDeleteAll.Size = New System.Drawing.Size(171, 17)
        Me.chkDeleteAll.TabIndex = 2
        Me.chkDeleteAll.Text = "&Delete All Selected SQL Views"
        Me.ttMain.SetToolTip(Me.chkDeleteAll, "Instead of creating or altering the selected SQL Views, if this is enabled, selec" &
        "ted SQL Views will be deleted.")
        Me.chkDeleteAll.UseVisualStyleBackColor = True
        '
        'lblWarning
        '
        Me.lblWarning.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblWarning.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblWarning.Location = New System.Drawing.Point(199, 148)
        Me.lblWarning.Name = "lblWarning"
        Me.lblWarning.Size = New System.Drawing.Size(197, 101)
        Me.lblWarning.TabIndex = 4
        Me.lblWarning.Text = "Remember:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SQL Files must be encoded as UTF8, otherise the operations will fail."
        Me.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ttMain
        '
        Me.ttMain.AutoPopDelay = 10000
        Me.ttMain.InitialDelay = 500
        Me.ttMain.ReshowDelay = 100
        Me.ttMain.ShowAlways = True
        '
        'frmCreateSQLView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(399, 298)
        Me.Controls.Add(Me.lblWarning)
        Me.Controls.Add(Me.chkDeleteAll)
        Me.Controls.Add(Me.gbSQLViewOptions)
        Me.Controls.Add(Me.btnCreateSQLViews)
        Me.Controls.Add(Me.gbSQLViews)
        Me.Name = "frmCreateSQLView"
        Me.Text = "Create SQL Views"
        Me.gbSQLViews.ResumeLayout(False)
        CType(Me.fswSQLViewFiles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbSQLViewOptions.ResumeLayout(False)
        Me.gbSQLViewOptions.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents gbSQLViews As GroupBox
    Friend WithEvents clbSQLViews As CheckedListBox
    Friend WithEvents btnCreateSQLViews As Button
    Friend WithEvents fswSQLViewFiles As IO.FileSystemWatcher
    Friend WithEvents gbSQLViewOptions As GroupBox
    Friend WithEvents chkDeleteAll As CheckBox
    Friend WithEvents rdbDeleteItAndCreateIt As RadioButton
    Friend WithEvents rdbAlterIt As RadioButton
    Friend WithEvents rdbDoNothing As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents lblWarning As Label
    Friend WithEvents ttMain As ToolTip
End Class
