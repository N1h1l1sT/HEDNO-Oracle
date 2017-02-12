'Version 1.1
'Requires Language file version 1.1

Imports System.IO

Public Class frmLicenseViewer
    Public strLanguage_LicenseViewer() As String
    Public AcceptedLicenses As Boolean = False
    Public AlreadyAcceptedAll As Boolean

    Dim isClosing As Boolean = False
    Dim TrueLicenseFiles As New List(Of String)
    Dim CurrentLicense As Integer = 0
    Dim LicensesCount As Integer = 0

    Private Sub LoadNextLicense(ByRef rtb As RichTextBox, ByRef XoutOfXLabel As Label, ByRef CurLicenseNum As Integer, ByRef MaxLicenseNum As Integer, ByVal rtfFilePath As String)
        CurLicenseNum += 1
        XoutOfXLabel.Text = CurLicenseNum & strLanguage_LicenseViewer(2) & MaxLicenseNum ' out of
        'MsgBox(rtfFilePath & vbCrLf & vbCrLf & CurLicenseNum & vbCrLf & vbCrLf & MaxLicenseNum)
        rtb.LoadFile(rtfFilePath)
    End Sub

    Private Sub frmLicenseViewer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not AcceptedLicenses AndAlso Not AlreadyAcceptedAll AndAlso Not isClosing Then
            isClosing = True
            MsgBox(strLanguage_LicenseViewer(5), MsgBoxStyle.Critical) 'You do not agree to the license of one of the component that this programme uses. Please uninstall the programme and delete all the files and folders that came with it.
            Application.Exit()
        End If
    End Sub

    Private Sub frmLicenseViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Call LicenseViewer_Language(Me)

            Call frmSkin(Me, False)


            Dim LicenseFiles() As String = Directory.GetFiles(strLicensesDir)
            For Each License In LicenseFiles
                If Not GetFileNameAlone(License).StartsWith("~") AndAlso GetFileName(License).ToLower <> "thumbs.db" Then TrueLicenseFiles.Add(License)
            Next

            LicensesCount = TrueLicenseFiles.Count

            If LicensesCount > 0 Then
                LoadNextLicense(rtbLicenseViewer, lblXOutOfX, CurrentLicense, LicensesCount, TrueLicenseFiles.Item(CurrentLicense))

                If AlreadyAcceptedAll Then
                    lblAlreadyAgreedToAll.Visible = True
                    btnDisagree.Enabled = False
                End If

            Else
                AcceptedLicenses = True
                Close()
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnAgree_Click(sender As Object, e As EventArgs) Handles btnAgree.Click
        btnAgree.Enabled = False

        If CurrentLicense < LicensesCount Then
            LoadNextLicense(rtbLicenseViewer, lblXOutOfX, CurrentLicense, LicensesCount, TrueLicenseFiles.Item(CurrentLicense))
        Else
            AcceptedLicenses = True
            Close()
        End If

        btnAgree.Enabled = True
    End Sub

    Private Sub btnDisagree_Click(sender As Object, e As EventArgs) Handles btnDisagree.Click
        AcceptedLicenses = False
        Me.Close()
    End Sub

    Private Sub btnAgreeToAll_Click(sender As Object, e As EventArgs) Handles btnAgreeToAll.Click
        AcceptedLicenses = True
        Close()
    End Sub
End Class