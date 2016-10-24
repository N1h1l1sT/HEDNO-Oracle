'Version 1.1 2013-03-29

Option Strict On

Public Class frmFindR
    Public strLanguage_FindR() As String
    Public strLanguage_FindR_Tips() As String

    Dim tmrRWebsiteButtonEnableCounter As Integer = -1

    Private Sub frmFindR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Call FindR_Language(Me)
            Call frmSkin(Me, False)

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub lblInfo_SizeChanged(sender As Object, e As EventArgs) Handles lblInfo.SizeChanged
        Dim ProposedWidth As Integer = lblInfo.Location.X + lblInfo.Size.Width + lblInfo.Location.X + 3
        If ProposedWidth < Width Then ProposedWidth = Width

        Size = New Size(ProposedWidth, lblInfo.Location.Y + (2 * lblInfo.Size.Height) + 10 + btnBrowse.Size.Height + 10)
    End Sub

    Private Sub btnRWebsite_Click(sender As Object, e As EventArgs) Handles btnRWebsite.Click
        If btnRWebsite.Text = strLanguage_FindR(3) Then '&Visit R's Website
            RunOpenDir(strExplorerExe, "http://cran.rstudio.com/")
            btnRWebsite.Text = strLanguage_FindR(6) '&I've just installed it
            tltFindR.SetToolTip(btnRWebsite, strLanguage_FindR_Tips(4)) 'Tries to automatically locate the R folder now that you've installed R, and if it fails, you will be prompted to locate the R Folder yourself.
            btnRWebsite.Enabled = False
            TopMost = False
            tmrRWebsiteButtonEnable.Enabled = True

        Else
            DialogResult = DialogResult.Retry
        End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        DialogResult = DialogResult.OK
    End Sub

    Private Sub tmrRWebsiteButtonEnable_Tick(sender As Object, e As EventArgs) Handles tmrRWebsiteButtonEnable.Tick
        If tmrRWebsiteButtonEnableCounter = -1 Then
            tmrRWebsiteButtonEnableCounter = 10

        ElseIf tmrRWebsiteButtonEnableCounter = 0 Then
            tmrRWebsiteButtonEnable.Enabled = False
            btnRWebsite.Enabled = True

        Else
            tmrRWebsiteButtonEnableCounter -= 1
        End If

        If tmrRWebsiteButtonEnableCounter > 0 Then btnRWebsite.Text = strLanguage_FindR(6) & " [" & tmrRWebsiteButtonEnableCounter & "]" Else btnRWebsite.Text = strLanguage_FindR(6) '&I've just installed it
    End Sub
End Class