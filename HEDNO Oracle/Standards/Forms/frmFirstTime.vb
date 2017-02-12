'Version 1.4 2013-04-29
'Fixed a bug that the programme could be used even if the licenses were not agreed upon
'Removed frmSettings from appearing on first time
'Fixed a bug that temporary files were also opened, causing problems
'Changed TextBox to WebBrowser, Runs Licenses; Changed txtLanguage to strLanguage_FirstTime
Option Strict On

Public Class frmFirstTime
    Public LicenseAccepted As Boolean
    Public strLanguage_FirstTime() As String
    Dim ReadingTime As Integer = 3
    Dim isFirstTime As Boolean = isFirstTimeRun()

    Private Sub frmFirstTime_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call FirstTime_Language(Me)

        Call frmSkin(Me)

        If isFirstTime AndAlso Visible Then
            tmrNext.Enabled = True
        Else
            btnNext.Text = strLanguage_FirstTime(2).Substring(4) '&Next
            tmrNext.Enabled = False
            btnNext.Enabled = True
        End If
    End Sub

    Private Sub tmrNext_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrNext.Tick
        ReadingTime = ReadingTime - 1
        btnNext.Text = "(" & ReadingTime & ")" & strLanguage_FirstTime(2).Substring(3) ' &Next
        If ReadingTime = 0 Then
            btnNext.Text = strLanguage_FirstTime(2).Substring(4) '&Next
            tmrNext.Enabled = False
            btnNext.Enabled = True
        End If

    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If isFirstTime Then
            Visible = False

            Dim LicenseViewerForm As New frmLicenseViewer
            LicenseViewerForm.ShowDialog()
            LicenseAccepted = LicenseViewerForm.AcceptedLicenses

            'frmSettings.TopMost = True
            'frmSettings.cmdApply.Enabled = False
            'ShowForm(frmSettings, , True)
            'frmSettings.cmdApply.Enabled = False
            'frmSettings.TopMost = False

            ShowForm(frmPresentation, , True)
        End If

        Close()

    End Sub

End Class