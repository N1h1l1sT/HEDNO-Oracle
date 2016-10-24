'Version 1.2.1 2012/08/16
Option Strict On

Imports System.IO
Imports Microsoft.Win32

Public NotInheritable Class frmSplashScreen
    Dim MyPoint As New Point()
    Dim X, Y As Integer

    Dim strPictures() As String
    Dim strForeColours() As String
    Dim strBackColours() As String

    Dim RandomClass As New Random()
    Dim RandomNumber As Integer

    'Private Sub frmSplashScreen_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
    '    If e.Button = MouseButtons.Left Then
    '        MyPoint = MousePosition
    '        MyPoint.X -= (X)
    '        MyPoint.Y -= (Y)
    '        Location = MyPoint
    '    End If
    'End Sub

    'Private Sub frmSplashScreen_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
    '    X = MousePosition.X - Me.Location.X
    '    Y = MousePosition.Y - Me.Location.Y
    'End Sub

    Private Sub frmSplashScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        On Error Resume Next
        If My.Application.Info.Title <> "" Then
            lblApplicationTitle.Text = My.Application.Info.Title
        Else
            lblApplicationTitle.Text = Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If

        'Licensee = Registry.GetValue(strProgramRegistryKeyName, strLicenseeValueName, "").ToString
        If Licensee <> "" Then
            lblLicense.Text = "Licensed to: " & Licensee
            lblLicense.Visible = True  'When there r no licensees, this is useless.
        End If

        lblVersion.Text = My.Application.Info.Version.ToString
        lblCopyright.Text = My.Application.Info.Copyright

        Dim SkinPath As String = strSkin & doProperPathName(My.Settings.strSkinChoice) & My.Settings.strSplashScreenPic
        If Not Directory.Exists(SkinPath) Then SkinPath = strSkin & "None\" & My.Settings.strSplashScreenPic
        If Directory.Exists(SkinPath) Then
            strPictures = Directory.GetFiles(SkinPath)
            If strPictures.Length > 0 Then
                RandomNumber = RandomClass.Next(0, CInt(strPictures.Length / 3))
                MainLayoutPanel.BackgroundImage = Image.FromFile(strPictures(RandomNumber * 3))

                ReadFile(strPictures(RandomNumber + 1), strBackColours)
                ReadFile(strPictures(RandomNumber + 2), strForeColours)

                'Fore Colours
                lblApplicationTitle.ForeColor = Color.FromName(GetSubstrAfterString(strForeColours, lblApplicationTitle.Name & "="))
                lblCopyright.ForeColor = Color.FromName(GetSubstrAfterString(strForeColours, lblCopyright.Name & "="))
                lblVersion.ForeColor = Color.FromName(GetSubstrAfterString(strForeColours, lblVersion.Name & "="))
                lblLicense.ForeColor = Color.FromName(GetSubstrAfterString(strForeColours, lblLicense.Name & "="))

                'Back Colours
                lblApplicationTitle.BackColor = Color.FromName(GetSubstrAfterString(strBackColours, lblApplicationTitle.Name & "="))
                lblCopyright.BackColor = Color.FromName(GetSubstrAfterString(strBackColours, lblCopyright.Name & "="))
                lblVersion.BackColor = Color.FromName(GetSubstrAfterString(strBackColours, lblVersion.Name & "="))
                lblLicense.BackColor = Color.FromName(GetSubstrAfterString(strBackColours, lblLicense.Name & "="))

            End If
        End If

    End Sub

End Class
