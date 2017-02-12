'Version 1.0.2 2013-03-29
Imports System.Text

Public Class frmSuggestionAndComplaint
    Public strLanguage_SuggestionAndComplaint() As String
    Public lstType As New List(Of String)
    Public lstFirstCategory As New List(Of String)
    Public lstSecondCategory As New List(Of String)

    Private Sub frmSuggestionAndComplaint_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Call SuggestionAndComplaint_Language(Me)
            frmSkin(Me, False)

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub cbType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbType.SelectedIndexChanged
        If cbType.SelectedIndex <> -1 Then
            Call DisableNeeded()

            Select Case cbType.SelectedIndex
                Case 0
                    cbCategory.DataSource = Nothing
                    cbCategory.Enabled = False

                Case 1
                    cbCategory.DataSource = lstFirstCategory

                Case 2
                    cbCategory.DataSource = lstSecondCategory

                Case Else
                    cbCategory.Items.Clear()

            End Select

        End If
    End Sub

    Private Sub DisableNeeded()
        If cbType.SelectedIndex < 1 Then
            cbCategory.Enabled = False
            txtName.Enabled = False
            txtEmail.Enabled = False
            txtMessage.Enabled = False
            btnNext.Enabled = False

        ElseIf cbCategory.SelectedIndex < 1 Then
            cbCategory.Enabled = True
            txtName.Enabled = False
            txtEmail.Enabled = False
            txtMessage.Enabled = False
            btnNext.Enabled = False

            'ElseIf cbCategory.SelectedIndex >= 1 Then
            '    cbCategory.Enabled = True
            '    txtName.Enabled = True
            '    txtEmail.Enabled = True
            '    txtMessage.Enabled = False
            '    btnNext.Enabled = False

        ElseIf Not (txtName.Text.Length > 1 AndAlso Not IsNumeric(txtName.Text) AndAlso txtEmail.Text.Length > 5 AndAlso txtEmail.Text.Contains("@") AndAlso txtEmail.Text.Contains(".")) Then
            cbCategory.Enabled = True
            txtName.Enabled = True
            txtEmail.Enabled = True
            txtMessage.Enabled = False
            btnNext.Enabled = False

        ElseIf txtMessage.Text.Length < 9 Then
            txtName.Enabled = True
            txtEmail.Enabled = True
            txtMessage.Enabled = True
            btnNext.Enabled = False

        Else
            txtName.Enabled = True
            txtEmail.Enabled = True
            txtMessage.Enabled = True
            btnNext.Enabled = True
        End If
    End Sub

    Private Sub cbCategory_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbCategory.SelectedIndexChanged
        Call DisableNeeded()
        cbCategory.Enabled = True

        If cbCategory.SelectedIndex > 1 Then
            txtName.Enabled = True
            txtEmail.Enabled = True
        End If
    End Sub

    Private Sub btnNext_Click(sender As System.Object, e As System.EventArgs) Handles btnNext.Click
        If MsgBox(strLanguage_SuggestionAndComplaint(26), MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then 'Are you sure you want to proceed?
            btnNext.Enabled = False
            btnCancel.Enabled = False

            Dim LastDateTimeSent As Date = DefaultDate
            Dim AppDataSettings() As String = {}

            Try
                ReadFile(strAppDataSettingsFile, AppDataSettings)
                LastDateTimeSent = Date.ParseExact(AppDataSettings(1), "yyyyMMdd", Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception
            End Try

            If LastDateTimeSent = DefaultDate OrElse LastDateTimeSent.Year < Today.Year OrElse (LastDateTimeSent.Month < Today.Month AndAlso LastDateTimeSent.Year >= Today.Year) Then
                Dim Type As String = "Type: "
                If cbType.SelectedIndex = 1 Then Type &= "Suggestion" Else Type &= "Complaint"

                Dim Category As String = "Category: "
                If cbType.SelectedIndex = 1 Then
                    If cbCategory.SelectedIndex = 1 Then
                        Category &= "New Feature"
                    ElseIf cbCategory.SelectedIndex = 2 Then
                        Category &= "Improvement of a Feature"
                    ElseIf cbCategory.SelectedIndex = 3 Then
                        Category &= "Other"
                    End If

                ElseIf cbType.SelectedIndex = 2 Then
                    If cbCategory.SelectedIndex = 1 Then
                        Category &= "Technical Issue"
                    ElseIf cbCategory.SelectedIndex = 2 Then
                        Category &= "Account/Registration/Key Issue"
                    ElseIf cbCategory.SelectedIndex = 3 Then
                        Category &= "Support Issue"
                    ElseIf cbCategory.SelectedIndex = 4 Then
                        Category &= "Crash Report"
                    ElseIf cbCategory.SelectedIndex = 5 Then
                        Category &= "Bug Report"
                    End If
                End If

                Dim MailBody As String = Type & vbCrLf & Category & vbCrLf & "Name: " & txtName.Text & vbCrLf & "Email: " & txtEmail.Text & vbCrLf & vbCrLf & txtMessage.Text
                If SendEmail(MailBody, True, Type) Then
                    If AppDataSettings.Length < 2 Then ReDim Preserve AppDataSettings(1)
                    AppDataSettings(1) = Zero_A_Num(Today.Year.ToString, 4) & Zero_A_Num(Today.Month.ToString, 2) & Zero_A_Num(Today.Day.ToString, 2)
                    WriteText(strAppDataSettingsFile, AppDataSettings, Encoding.Unicode)
                    MsgBox(strLanguage_SuggestionAndComplaint(23) & cbType.Items(cbType.SelectedIndex).ToString & strLanguage_SuggestionAndComplaint(24), MsgBoxStyle.Information) 'Your Suggestion/Complaint has been successfully send
                    Close()
                Else
                    MsgBox(strLanguage_SuggestionAndComplaint(25), MsgBoxStyle.Exclamation) 'The program encountered an error and your request could not be successfully completed.
                    Close()
                End If

            Else
                MsgBox(strLanguage_SuggestionAndComplaint(22), MsgBoxStyle.Exclamation) 'You have already sent a suggestion or complaint this month.
                Close()
            End If

        End If
    End Sub

    Private Sub txtName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtName.TextChanged
        DisableNeeded()
    End Sub

    Private Sub txtEmail_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtEmail.TextChanged
        DisableNeeded()
    End Sub

    Private Sub txtMessage_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtMessage.TextChanged
        DisableNeeded()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CloseForm(Me)
    End Sub
End Class