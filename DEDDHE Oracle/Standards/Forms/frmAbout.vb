'Version 1.3.1 2013-03-29
Option Strict On

Public Class frmAbout
    Public strLanguage_About() As String

    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            'initialization
            Call About_Language(Me)
            '/initialization

            frmSkin(Me, False)

            txtAssemblyName.Text = My.Application.Info.AssemblyName
            txtCompanyName.Text = My.Application.Info.CompanyName
            txtCopyright.Text = My.Application.Info.Copyright
            txtTrademark.Text = My.Application.Info.Trademark
            txtDirectoryPath.Text = My.Application.Info.DirectoryPath
            txtProductName.Text = My.Application.Info.ProductName
            txtTitle.Text = My.Application.Info.Title
            txtVersion.Text = System.Convert.ToString(My.Application.Info.Version)
            txtWorkingSet.Text = CStr(My.Application.Info.WorkingSet)
            txtHash.Text = CStr(My.User.GetHashCode())
            txtUser.Text = My.User.Name
            If UserName <> String.Empty AndAlso UserName.ToLower <> "trial" Then
                txtLicense.Visible = True
                lblLicense.Visible = True
                txtLicense.Text = UserName
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Close()
        Exit Sub
    End Sub

End Class