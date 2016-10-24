<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSuggestionAndComplaint
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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.cbType = New System.Windows.Forms.ComboBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.gbInformation = New System.Windows.Forms.GroupBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.cbCategory = New System.Windows.Forms.ComboBox()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.gbMessage = New System.Windows.Forms.GroupBox()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.gbInformation.SuspendLayout()
        Me.gbMessage.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(13, 528)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(197, 27)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Enabled = False
        Me.btnNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnNext.Location = New System.Drawing.Point(575, 528)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(197, 27)
        Me.btnNext.TabIndex = 2
        Me.btnNext.Text = "&Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'lblInfo
        '
        Me.lblInfo.BackColor = System.Drawing.Color.Black
        Me.lblInfo.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.Magenta
        Me.lblInfo.Location = New System.Drawing.Point(13, 1)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(759, 63)
        Me.lblInfo.TabIndex = 4
        Me.lblInfo.Text = "!WARNING! Abusing the Suggestion and Complaints form may result in temporary or p" & _
    "ermanent registration key ban (if applicable)!"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cbType
        '
        Me.cbType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cbType.FormattingEnabled = True
        Me.cbType.Location = New System.Drawing.Point(119, 21)
        Me.cbType.Name = "cbType"
        Me.cbType.Size = New System.Drawing.Size(225, 21)
        Me.cbType.TabIndex = 0
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.Location = New System.Drawing.Point(22, 24)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(39, 13)
        Me.lblType.TabIndex = 4
        Me.lblType.Text = "Type:"
        '
        'gbInformation
        '
        Me.gbInformation.BackColor = System.Drawing.Color.Transparent
        Me.gbInformation.Controls.Add(Me.txtName)
        Me.gbInformation.Controls.Add(Me.lblName)
        Me.gbInformation.Controls.Add(Me.txtEmail)
        Me.gbInformation.Controls.Add(Me.lblEmail)
        Me.gbInformation.Controls.Add(Me.cbCategory)
        Me.gbInformation.Controls.Add(Me.lblCategory)
        Me.gbInformation.Controls.Add(Me.cbType)
        Me.gbInformation.Controls.Add(Me.lblType)
        Me.gbInformation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.gbInformation.Location = New System.Drawing.Point(17, 84)
        Me.gbInformation.Name = "gbInformation"
        Me.gbInformation.Size = New System.Drawing.Size(755, 95)
        Me.gbInformation.TabIndex = 0
        Me.gbInformation.TabStop = False
        Me.gbInformation.Text = "Information:"
        '
        'txtName
        '
        Me.txtName.Enabled = False
        Me.txtName.Location = New System.Drawing.Point(119, 58)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(225, 20)
        Me.txtName.TabIndex = 2
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(22, 61)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(43, 13)
        Me.lblName.TabIndex = 5
        Me.lblName.Text = "Name:"
        '
        'txtEmail
        '
        Me.txtEmail.Enabled = False
        Me.txtEmail.Location = New System.Drawing.Point(498, 58)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(225, 20)
        Me.txtEmail.TabIndex = 3
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(401, 61)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(41, 13)
        Me.lblEmail.TabIndex = 7
        Me.lblEmail.Text = "Email:"
        '
        'cbCategory
        '
        Me.cbCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cbCategory.Enabled = False
        Me.cbCategory.FormattingEnabled = True
        Me.cbCategory.Location = New System.Drawing.Point(498, 21)
        Me.cbCategory.Name = "cbCategory"
        Me.cbCategory.Size = New System.Drawing.Size(225, 21)
        Me.cbCategory.TabIndex = 1
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Location = New System.Drawing.Point(401, 24)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(61, 13)
        Me.lblCategory.TabIndex = 6
        Me.lblCategory.Text = "Category:"
        '
        'gbMessage
        '
        Me.gbMessage.BackColor = System.Drawing.Color.Transparent
        Me.gbMessage.Controls.Add(Me.txtMessage)
        Me.gbMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.gbMessage.Location = New System.Drawing.Point(17, 189)
        Me.gbMessage.Name = "gbMessage"
        Me.gbMessage.Size = New System.Drawing.Size(755, 331)
        Me.gbMessage.TabIndex = 1
        Me.gbMessage.TabStop = False
        Me.gbMessage.Text = "Message (English or Greek only):"
        '
        'txtMessage
        '
        Me.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMessage.Enabled = False
        Me.txtMessage.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.txtMessage.Location = New System.Drawing.Point(3, 16)
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(749, 312)
        Me.txtMessage.TabIndex = 0
        '
        'frmSuggestionAndComplaint
        '
        Me.AcceptButton = Me.btnNext
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.gbMessage)
        Me.Controls.Add(Me.gbInformation)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmSuggestionAndComplaint"
        Me.Text = "Suggestion and Complaints"
        Me.gbInformation.ResumeLayout(False)
        Me.gbInformation.PerformLayout()
        Me.gbMessage.ResumeLayout(False)
        Me.gbMessage.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents cbType As System.Windows.Forms.ComboBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents gbInformation As System.Windows.Forms.GroupBox
    Friend WithEvents cbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents gbMessage As System.Windows.Forms.GroupBox
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
End Class
