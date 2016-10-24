'Version 1.2.3 2013-08-18
'Added "RichTextBox", "Panel", "SplitContainer"
'Made changes to be applied instantly to any form
'Changed txtLanguage to strLanguage_SkinCreator

Imports System.Reflection
Imports System.IO

Public Class frmSkinCreator
    Public strLanguage_SkinCreator() As String

    Dim formAndControls As New Dictionary(Of String, List(Of String))()
    Dim forms() As Form
    Dim strSkinsFolders() As String
    Dim CtrlForeColours() As String
    Dim CtrlBackColours() As String

    Dim ForeColoursText As String
    Dim BackColoursText As String

    Dim JumpToForeColour As Boolean = False

    Private Function ColourFill() As String()
        Dim ColorNames() As String
        ColorNames = System.Enum.GetNames(GetType(KnownColor))
        Return ColorNames
    End Function

    Private Sub ReadSkinNames()
        lstSkins.SelectedIndex = -1
        strSkinsFolders = Directory.GetDirectories(strSkin)

        lstSkins.Items.Clear()
        For Each Skin As String In strSkinsFolders
            If Skin.Substring(strSkin.Length).ToLower <> "none" Then
                lstSkins.Items.Add(Skin.Substring(strSkin.Length))
                If GetSubStr(My.Settings.strSkinChoice, My.Settings.strSkinChoice.Length - 1) = lstSkins.Items(lstSkins.Items.Count - 1).ToString Then
                    lstSkins.SelectedIndex = lstSkins.Items.Count - 1
                End If
            End If
        Next
    End Sub

    Private Sub frmSkinCreator_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Call SkinCreator_Language(Me)

            frmSkin(Me, False)

            fswSkins.Path = strSkin

            For Each frmtype As Type In Assembly.GetExecutingAssembly().GetTypes()
                If frmtype.IsSubclassOf(GetType(Form)) Then
                    cbSelForms.Items.Add(frmtype.Name)
                    formAndControls(frmtype.Name) = New List(Of String)()
                    Dim members As FieldInfo() = frmtype.GetFields(BindingFlags.NonPublic Or BindingFlags.Instance)
                    For Each item As FieldInfo In members
                        If item.FieldType.IsSubclassOf(GetType(Control)) OrElse item.FieldType Is GetType(ToolStripMenuItem) Then
                            If item.FieldType Is GetType(ToolStripMenuItem) OrElse item.FieldType Is GetType(Button) OrElse item.FieldType Is GetType(Label) OrElse item.FieldType Is GetType(RadioButton) OrElse item.FieldType Is GetType(CheckBox) OrElse item.FieldType Is GetType(GroupBox) OrElse item.FieldType = GetType(SplitContainer) OrElse item.FieldType = GetType(Panel) Then
                                formAndControls(frmtype.Name).Add(item.Name.Substring(1, item.Name.Length - 1))
                            End If
                        End If
                    Next

                End If
            Next

            cbForeColour.Items.AddRange(ColourFill)
            cbBackColour.Items.AddRange(ColourFill)

            If Directory.Exists(strSkin) Then
                Call ReadSkinNames()

                If strSkinsFolders.Length > 0 Then
                    btnDelSkin.Enabled = True
                    btnRename.Enabled = True
                End If

            Else
                Directory.CreateDirectory(strSkin)
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub CheckImagesNcolours()
        Dim strFormImage As String = strSkin & lstSkins.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & ".jpg"
        If File.Exists(strFormImage) Then
            txtImage.Text = strLanguage_SkinCreator(18)
        Else
            txtImage.Text = String.Empty
        End If

        Dim strFormBigImage As String = strSkin & lstSkins.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & "_Pattern.jpg"
        If File.Exists(strFormBigImage) Then
            txtBigImage.Text = strLanguage_SkinCreator(18)
        Else
            txtBigImage.Text = String.Empty
        End If

        If File.Exists(strSkin & lstSkins.Items(lstSkins.SelectedIndex).ToString & "\" & My.Settings.strSplashScreenPic & "SplashScreen 1.jpg") Then
            txtSplashScreenImage.Text = strLanguage_SkinCreator(18)
        Else
            txtSplashScreenImage.Text = String.Empty
        End If

        Dim strForeColours As String = strSkin & lstSkins.SelectedItem.ToString & "\" & cbSelForms.Text & "\" & cbSelForms.Text & "_ForeColours.txt"
        If Not File.Exists(strForeColours) Then
            WriteText(strForeColours, "", System.Text.Encoding.Unicode)
        End If
        Dim strBackColours As String = strSkin & lstSkins.SelectedItem.ToString & "\" & cbSelForms.Text & "\" & cbSelForms.Text & "_BackColours.txt"
        If Not File.Exists(strBackColours) Then
            WriteText(strBackColours, "", System.Text.Encoding.Unicode)
        End If

        If Not cbSelForms.SelectedItem.ToString.ToLower = "frmsplashscreen" Then
            ForeColoursText = strSkin & lstSkins.Items(lstSkins.SelectedIndex).ToString & "\" & cbSelForms.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & "_ForeColours.txt"
            BackColoursText = strSkin & lstSkins.Items(lstSkins.SelectedIndex).ToString & "\" & cbSelForms.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & "_BackColours.txt"
        Else
            ForeColoursText = strSkin & lstSkins.Items(lstSkins.SelectedIndex).ToString & "\" & My.Settings.strSplashScreenPic & "SplashScreen 1_ForeColours.txt"
            BackColoursText = strSkin & lstSkins.Items(lstSkins.SelectedIndex).ToString & "\" & My.Settings.strSplashScreenPic & "SplashScreen 1_BackColours.txt"
        End If

    End Sub

    Private Sub lstSkins_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstSkins.SelectedIndexChanged
        Try
            If lstSkins.SelectedIndex <> -1 Then
                gbItems.Enabled = True
                btnApply.Enabled = False

                If cbSelForms.SelectedIndex <> -1 Then
                    Dim FormSkinDir As String = strSkin & lstSkins.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString
                    If Not Directory.Exists(FormSkinDir) Then
                        Directory.CreateDirectory(FormSkinDir)
                    End If

                    Call CheckImagesNcolours()

                    Call LoadControlColours()

                    If cbSelControls.SelectedIndex <> -1 Then

                        If File.Exists(strSkin & lstSkins.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & "\" & cbSelControls.SelectedItem.ToString & ".jpg") Then
                            txtCtrlImage.Text = strLanguage_SkinCreator(18)   'Image Found
                        Else
                            txtCtrlImage.Text = String.Empty
                        End If
                        btnCtrlImage.Enabled = True

                    End If

                Else
                    cbSelControls.Text = String.Empty
                    cbSelControls.SelectedIndex = -1

                    txtImage.Text = String.Empty
                    txtBigImage.Text = String.Empty
                    txtSplashScreenImage.Text = String.Empty

                    cbForeColour.SelectedIndex = -1
                    cbForeColour.Enabled = False
                    btnDelForeColour.Enabled = False
                    cbForeColour.Text = String.Empty

                    cbBackColour.SelectedIndex = -1
                    cbBackColour.Enabled = False
                    btnDelBackColour.Enabled = False
                    cbBackColour.Text = String.Empty

                    txtCtrlImage.Text = String.Empty
                    btnCtrlImage.Enabled = False
                End If


            Else
                gbItems.Enabled = False

                cbSelForms.Text = String.Empty
                cbSelControls.Text = String.Empty
                cbSelControls.Items.Clear()

                txtImage.Text = String.Empty
                txtBigImage.Text = String.Empty
                txtSplashScreenImage.Text = String.Empty

                cbForeColour.Enabled = False
                btnDelForeColour.Enabled = False
                cbForeColour.Text = String.Empty

                cbBackColour.Enabled = False
                btnDelBackColour.Enabled = False
                cbBackColour.Text = String.Empty

                txtCtrlImage.Text = String.Empty
                btnCtrlImage.Enabled = False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub cbSelForms_LostFocus(sender As Object, e As EventArgs) Handles cbSelForms.LostFocus
        If cbSelControls.Enabled AndAlso cbSelForms.SelectedIndex = -1 Then 'If we leave this field, and the next is enabled, and nothing is selected on this one, lets select something
            cbSelForms.Text = cbSelForms.Items(0).ToString
            cbSelForms.SelectedItem = cbSelForms.Items(0)
            cbSelForms.SelectedIndex = 0

        ElseIf cbSelControls.Enabled AndAlso cbSelForms.SelectedIndex <> -1 AndAlso cbSelForms.Text <> cbSelForms.SelectedItem.ToString Then 'Else if, the next field is enabled, and something is selected here, but the text isn't what it should have been, lets make it
            cbSelForms.Text = cbSelForms.Items(0).ToString
        End If
    End Sub

    Private Sub cbSelForms_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbSelForms.SelectedIndexChanged
        Try
            If cbSelForms.SelectedIndex <> -1 Then
                btnApply.Enabled = False
                cbSelControls.SelectedIndex = -1
                cbSelControls.Text = String.Empty
                cbSelControls.Items.Clear()
                cbSelControls.Items.AddRange(formAndControls(cbSelForms.Text).ToArray())

                If lstSkins.SelectedIndex <> -1 Then
                    btnImage.Enabled = True
                    btnBigImage.Enabled = True
                    btnSplashScreenImage.Enabled = True

                    Dim FormSkinDir As String = strSkin & lstSkins.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString
                    If Not Directory.Exists(FormSkinDir) AndAlso FormSkinDir <> strSkin & lstSkins.SelectedItem.ToString & "\" & "frmSplashScreen" Then
                        Directory.CreateDirectory(FormSkinDir)
                    End If

                    Call CheckImagesNcolours()

                    Call LoadControlColours()

                Else
                    btnImage.Enabled = False
                    txtImage.Text = String.Empty
                    btnBigImage.Enabled = False
                    txtBigImage.Text = String.Empty
                    btnSplashScreenImage.Enabled = False
                    txtSplashScreenImage.Text = String.Empty
                End If

            Else
                cbSelControls.SelectedIndex = -1
                cbSelControls.Enabled = False
                cbSelControls.Items.Clear()
                cbSelControls.Text = String.Empty
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub LoadControlColours()
        Dim txtForeColours As New TextBox
        Dim isForeColoursChanged As Boolean = False
        Dim txtBackColours As New TextBox
        Dim isBackColoursChanged As Boolean = False

        ReadFile(ForeColoursText, txtForeColours, , , , True)
        ReadFile(BackColoursText, txtBackColours, , , , True)

        ReDim CtrlForeColours(cbSelControls.Items.Count - 1)
        ReDim CtrlBackColours(cbSelControls.Items.Count - 1)

        If txtForeColours.Text <> String.Empty OrElse txtBackColours.Text <> String.Empty Then
            For i = 0 To cbSelControls.Items.Count - 1
                For Each txtForeColLine As String In txtForeColours.Lines
                    If txtForeColLine.ToLower.StartsWith(cbSelControls.Items(i).ToString.ToLower & "=") Then
                        CtrlForeColours(i) = txtForeColLine.Substring((cbSelControls.Items(i).ToString.ToLower & "=").Length)
                        Exit For
                    End If
                Next

                For Each txtBackColLine As String In txtBackColours.Lines
                    If txtBackColLine.ToLower.StartsWith(cbSelControls.Items(i).ToString.ToLower & "=") Then
                        CtrlBackColours(i) = txtBackColLine.Substring((cbSelControls.Items(i).ToString.ToLower & "=").Length)
                        Exit For
                    End If
                Next
            Next
        End If

        If cbSelControls.SelectedIndex <> -1 AndAlso txtForeColours.Text <> String.Empty Then
            cbForeColour.Text = CtrlForeColours(cbSelControls.SelectedIndex)
        Else
            cbForeColour.Text = String.Empty
        End If

        If cbSelControls.SelectedIndex <> -1 AndAlso txtBackColours.Text <> String.Empty Then
            cbBackColour.Text = CtrlBackColours(cbSelControls.SelectedIndex)
        Else
            cbBackColour.Text = String.Empty
        End If

        If cbSelControls.SelectedIndex <> -1 AndAlso File.Exists(strSkin & lstSkins.SelectedItem.ToString & "\" & cbSelControls.SelectedItem.ToString & ".jpg") Then
            txtCtrlImage.Text = strLanguage_SkinCreator(18)   'Image Found
        Else
            txtCtrlImage.Text = String.Empty
        End If

    End Sub

    Private Sub cbSelControls_LostFocus(sender As Object, e As EventArgs) Handles cbSelControls.LostFocus
        If cbForeColour.Enabled AndAlso cbSelControls.SelectedIndex = -1 Then 'If we leave this field, and the next is enabled, and nothing is selected on this one, lets select something
            cbSelControls.Text = cbSelControls.Items(0).ToString
            cbSelControls.SelectedItem = cbSelControls.Items(0)
            cbSelControls.SelectedIndex = 0

        ElseIf cbForeColour.Enabled AndAlso cbSelControls.SelectedIndex <> -1 AndAlso cbSelControls.Text <> cbSelControls.SelectedItem.ToString Then 'Else if, the next field is enabled, and something is selected here, but the text isn't what it should have been, lets make it
            cbSelControls.Text = cbSelControls.Items(0).ToString
        End If
    End Sub

    Private Sub cbSelControls_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbSelControls.SelectedIndexChanged
        Try
            If cbSelControls.SelectedIndex <> -1 Then

                cbForeColour.SelectedIndex = -1
                cbForeColour.Enabled = True
                btnDelForeColour.Enabled = True
                cbForeColour.Text = CtrlForeColours(cbSelControls.SelectedIndex)

                cbBackColour.SelectedIndex = -1
                cbBackColour.Enabled = True
                btnDelBackColour.Enabled = True
                cbBackColour.Text = CtrlBackColours(cbSelControls.SelectedIndex)

                If File.Exists(strSkin & lstSkins.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & "\" & cbSelControls.SelectedItem.ToString & ".jpg") Then
                    txtCtrlImage.Text = strLanguage_SkinCreator(18)   'Image Found
                Else
                    txtCtrlImage.Text = String.Empty
                End If
                btnCtrlImage.Enabled = True
                JumpToForeColour = True

            Else
                cbForeColour.Enabled = False
                btnDelForeColour.Enabled = False
                cbForeColour.Text = String.Empty

                cbBackColour.Enabled = False
                btnDelBackColour.Enabled = False
                cbBackColour.Text = String.Empty

                txtCtrlImage.Text = String.Empty
                btnCtrlImage.Enabled = False
                JumpToForeColour = False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnApply_Click(sender As System.Object, e As System.EventArgs) Handles btnApply.Click
        Call Apply()
    End Sub

    Private Sub Apply()
        Try
            Dim sbRet As New System.Text.StringBuilder
            Dim ForeColoursText As String
            Dim BackColoursText As String

            If Not cbSelForms.SelectedItem.ToString.ToLower = "frmsplashscreen" Then
                ForeColoursText = strSkin & lstSkins.Items(lstSkins.SelectedIndex).ToString & "\" & cbSelForms.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & "_ForeColours.txt"
                BackColoursText = strSkin & lstSkins.Items(lstSkins.SelectedIndex).ToString & "\" & cbSelForms.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & "_BackColours.txt"
            Else
                ForeColoursText = strSkin & lstSkins.Items(lstSkins.SelectedIndex).ToString & "\" & My.Settings.strSplashScreenPic & "SplashScreen 1_ForeColours.txt"
                BackColoursText = strSkin & lstSkins.Items(lstSkins.SelectedIndex).ToString & "\" & My.Settings.strSplashScreenPic & "SplashScreen 1_BackColours.txt"
            End If

            For i = 0 To CtrlForeColours.Length - 1
                If CtrlForeColours(i) IsNot Nothing AndAlso CtrlForeColours(i) <> "" Then
                    sbRet.Append(cbSelControls.Items(i).ToString & "=" & CtrlForeColours(i).ToString)
                    sbRet.AppendLine()
                End If
            Next
            WriteText(ForeColoursText, sbRet.ToString, System.Text.Encoding.Unicode)

            sbRet.Clear() 'Before we start the new string, lets delete the old one
            For i = 0 To CtrlBackColours.Length - 1
                If CtrlBackColours(i) IsNot Nothing AndAlso CtrlBackColours(i) <> "" Then
                    sbRet.Append(cbSelControls.Items(i).ToString & "=" & CtrlBackColours(i).ToString)
                    sbRet.AppendLine()
                End If
            Next
            WriteText(BackColoursText, sbRet.ToString, System.Text.Encoding.Unicode)

            For i As Integer = 0 To My.Application.OpenForms.Count - 1
                If My.Application.OpenForms(i).Name = cbSelForms.Items(cbSelForms.SelectedIndex).ToString Then
                    frmSkin(My.Application.OpenForms(i), True)
                    Exit For
                End If
            Next
            Call UpdateTexts(frmMain)

            btnApply.Enabled = False
            MsgBox(strLanguage_SkinCreator(27)) 'Done!


        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnImage_Click(sender As System.Object, e As System.EventArgs) Handles btnImage.Click
        Try
            Dim UserResult As MsgBoxResult = MsgBox(strLanguage_SkinCreator(19) & vbCrLf & strLanguage_SkinCreator(20), MsgBoxStyle.YesNoCancel)
            Dim NewImagePathName As String = strSkin & lstSkins.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & ".jpg"
            If UserResult = MsgBoxResult.Yes Then
                ofdImage.ShowDialog()
                If ofdImage.FileName <> "" Then
                    Try
                        DelFileFolder(NewImagePathName)
                        File.Copy(ofdImage.FileName, NewImagePathName)
                        txtImage.Text = strLanguage_SkinCreator(18)
                        If cbSelForms.SelectedItem.ToString.ToLower = "frmmain" Then
                            frmSkin(frmMain)
                        End If

                    Catch ex As Exception
                        MsgBox(ex.Message & vbCrLf & vbCrLf & strLanguage_SkinCreator(28) & vbCrLf & strLanguage_SkinCreator(30)) 'It is possible that this error is due to your trying to change elements of the currently in use skin.
                    End Try
                End If

            ElseIf UserResult = MsgBoxResult.No Then
                DelFileFolder(NewImagePathName)
                txtImage.Text = ""
                If cbSelForms.SelectedItem.ToString.ToLower = "frmmain" Then
                    frmSkin(frmMain)
                End If
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnBigImage_Click(sender As System.Object, e As System.EventArgs) Handles btnBigImage.Click
        Try
            Dim UserResult As MsgBoxResult = MsgBox(strLanguage_SkinCreator(19) & vbCrLf & strLanguage_SkinCreator(20), MsgBoxStyle.YesNoCancel)    'Do you want to search for an image, OrElse clear the image?

            Dim NewImagePathName As String = strSkin & lstSkins.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & "_Pattern.jpg"
            If UserResult = MsgBoxResult.Yes Then
                ofdImage.ShowDialog()
                If ofdImage.FileName <> "" Then
                    Try
                        DelFileFolder(NewImagePathName)
                        File.Copy(ofdImage.FileName, NewImagePathName)
                        txtBigImage.Text = strLanguage_SkinCreator(18)
                        If cbSelForms.SelectedItem.ToString.ToLower = "frmmain" Then
                            frmSkin(frmMain)
                        End If

                    Catch ex As Exception
                        MsgBox(ex.Message & vbCrLf & vbCrLf & strLanguage_SkinCreator(28) & vbCrLf & strLanguage_SkinCreator(30)) 'It is possible that this error is due to your trying to change elements of the currently in use skin.
                    End Try
                End If

            ElseIf UserResult = MsgBoxResult.No Then
                DelFileFolder(NewImagePathName)
                txtBigImage.Text = ""
                If cbSelForms.SelectedItem.ToString.ToLower = "frmmain" Then
                    frmSkin(frmMain)
                End If
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnCtrlImage_Click(sender As System.Object, e As System.EventArgs) Handles btnCtrlImage.Click
        Try
            Dim UserResult As MsgBoxResult = MsgBox(strLanguage_SkinCreator(19) & vbCrLf & strLanguage_SkinCreator(20), MsgBoxStyle.YesNoCancel)    'Do you want to search for an image, OrElse clear the image?

            Dim NewImagePathName As String = strSkin & lstSkins.SelectedItem.ToString & "\" & cbSelForms.SelectedItem.ToString & "\" & cbSelControls.SelectedItem.ToString & ".jpg"
            If UserResult = MsgBoxResult.Yes Then
                Dim DialogResult As DialogResult = ofdImage.ShowDialog()
                If DialogResult = DialogResult.OK Then
                    Try
                        DelFileFolder(NewImagePathName)
                        File.Copy(ofdImage.FileName, NewImagePathName)
                        txtCtrlImage.Text = strLanguage_SkinCreator(18)

                        For i As Integer = 0 To My.Application.OpenForms.Count - 1
                            If My.Application.OpenForms(i).Name = cbSelForms.Items(cbSelForms.SelectedIndex).ToString Then
                                frmSkin(My.Application.OpenForms(i), True)
                                Exit For
                            End If
                        Next
                        Call UpdateTexts(frmMain)

                    Catch ex As Exception
                        MsgBox(ex.Message & vbCrLf & vbCrLf & strLanguage_SkinCreator(28) & vbCrLf & strLanguage_SkinCreator(30)) 'It is possible that this error is due to your trying to change elements of the currently in use skin.
                    End Try
                End If

            ElseIf UserResult = MsgBoxResult.No Then
                DelFileFolder(NewImagePathName)
                txtCtrlImage.Text = ""

                For i As Integer = 0 To My.Application.OpenForms.Count - 1
                    If My.Application.OpenForms(i).Name = cbSelForms.Items(cbSelForms.SelectedIndex).ToString Then
                        frmSkin(My.Application.OpenForms(i), True)
                        Exit For
                    End If
                Next
                Call UpdateTexts(frmMain)

            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnSplashScreenImage_Click(sender As System.Object, e As System.EventArgs) Handles btnSplashScreenImage.Click
        Try
            Dim UserResult As MsgBoxResult = MsgBox(strLanguage_SkinCreator(19) & vbCrLf & strLanguage_SkinCreator(20), MsgBoxStyle.YesNoCancel)    'Do you want to search for an image, OrElse clear the image?

            Dim NewImagePathName As String = strSkin & lstSkins.SelectedItem.ToString & "\" & My.Settings.strSplashScreenPic & "SplashScreen 1.jpg"
            If UserResult = MsgBoxResult.Yes Then
                ofdImage.ShowDialog()
                If ofdImage.FileName <> "" Then
                    Try
                        DelFileFolder(NewImagePathName)
                        File.Copy(ofdImage.FileName, NewImagePathName)
                        txtSplashScreenImage.Text = strLanguage_SkinCreator(18)
                        If cbSelForms.SelectedItem.ToString.ToLower = "frmmain" Then
                            frmSkin(frmMain)
                        End If

                    Catch ex As Exception
                        MsgBox(ex.Message & vbCrLf & vbCrLf & strLanguage_SkinCreator(28) & vbCrLf & strLanguage_SkinCreator(30)) 'It is possible that this error is due to your trying to change elements of the currently in use skin.
                    End Try
                End If

            ElseIf UserResult = MsgBoxResult.No Then
                DelFileFolder(NewImagePathName)
                txtSplashScreenImage.Text = ""
                If cbSelForms.SelectedItem.ToString.ToLower = "frmmain" Then
                    frmSkin(frmMain)
                End If
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnNewSkin_Click(sender As System.Object, e As System.EventArgs) Handles btnNewSkin.Click
        Try
            gbCommands.Enabled = False

            Dim NameAlreadyExists As Boolean = False
            Dim SkinName As String = String.Empty

            If TypeBox(strLanguage_SkinCreator(21), SkinName, False, , , , 3, 10) = True Then    'Input the name of the skin
                For Each Skin As String In lstSkins.Items
                    If Skin.ToLower = SkinName.ToLower Then
                        NameAlreadyExists = True
                        Exit For
                    End If
                Next

                If Not NameAlreadyExists Then
                    Directory.CreateDirectory(strSkin & SkinName)
                    'Now that at least one skin exists, enable the buttons
                    btnDelSkin.Enabled = True
                    btnRename.Enabled = True
                Else
                    MsgBox(strLanguage_SkinCreator(22), MsgBoxStyle.Information) 'This name already exists! No change was made
                End If

            End If

            gbCommands.Enabled = True

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnRename_Click(sender As System.Object, e As System.EventArgs) Handles btnRename.Click
        Try
            gbCommands.Enabled = False

            If lstSkins.SelectedIndex <> -1 Then
                If lstSkins.SelectedItem.ToString <> GetSubStr(My.Settings.strSkinChoice, My.Settings.strSkinChoice.Length - 1) Then
                    Dim SkinNewName As String = String.Empty
                    Dim SkinOldName As String = lstSkins.Items(lstSkins.SelectedIndex).ToString

                    If TypeBox(strLanguage_SkinCreator(21), SkinNewName, False) = True Then    'Input the name of the skin
                        Dim NameAlreadyExists As Boolean = False

                        For Each SkinName As String In lstSkins.Items
                            If SkinName.ToLower = SkinNewName.ToLower Then
                                NameAlreadyExists = True
                                Exit For
                            End If
                        Next

                        If Not NameAlreadyExists Then
                            RenFileFolder(strSkin & SkinOldName, strSkin & SkinNewName)
                            If My.Settings.strSkinChoice.Replace("\", "").Replace("/", "") = SkinOldName Then
                                My.Settings.strSkinChoice = SkinNewName & "\"
                                My.Settings.Save()
                            End If

                        Else
                            MsgBox(strLanguage_SkinCreator(22), MsgBoxStyle.Information) 'This name already exists! No change was made
                        End If

                    End If

                Else
                    MsgBox(strLanguage_SkinCreator(29) & vbCrLf & strLanguage_SkinCreator(23), MsgBoxStyle.Exclamation) 'This skin cannot be renamed because it is currently in use.
                End If
            Else
                MsgBox(strLanguage_SkinCreator(23), MsgBoxStyle.Information)  'No skin is selected!
            End If

            gbCommands.Enabled = True

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnDelSkin_Click(sender As System.Object, e As System.EventArgs) Handles btnDelSkin.Click
        Try
            gbCommands.Enabled = False

            If lstSkins.SelectedIndex <> -1 Then

                Dim UserResponse As MsgBoxResult = MsgBox(strLanguage_SkinCreator(24) & lstSkins.Items(lstSkins.SelectedIndex).ToString & strLanguage_SkinCreator(25), MsgBoxStyle.YesNoCancel) 'Are you sure you want to delete "
                If UserResponse = MsgBoxResult.Yes Then

                    'Delete the Skin's Folder
                    DelFileFolder(strSkin & lstSkins.Items(lstSkins.SelectedIndex).ToString)

                    lstSkins.SelectedIndex = -1

                    'If there r no items left, then you cant delete any more, nor can you rename.
                    If lstSkins.Items.Count = 0 Then
                        btnDelSkin.Enabled = False
                        btnRename.Enabled = False
                    End If

                End If

            Else
                MsgBox(strLanguage_SkinCreator(23), MsgBoxStyle.Information)  'No skin is selected!
            End If

            gbCommands.Enabled = True

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        On Error Resume Next

        Close()
    End Sub

    Private Sub fswSkins_Created(sender As Object, e As System.IO.FileSystemEventArgs) Handles fswSkins.Created, fswSkins.Deleted, fswSkins.Renamed
        ReadSkinNames()
    End Sub

    Private Sub cbForeColour_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbForeColour.SelectedIndexChanged
        If cbForeColour.SelectedIndex <> -1 Then
            CtrlForeColours(cbSelControls.SelectedIndex) = cbForeColour.SelectedItem.ToString
            lnsForeColour.BorderColor = Color.FromName(cbForeColour.SelectedItem.ToString)
        Else
            lnsForeColour.BorderColor = Color.FromKnownColor(KnownColor.ControlText)
        End If
        btnApply.Enabled = True
    End Sub

    Private Sub cbBackColour_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBackColour.SelectedIndexChanged
        If cbBackColour.SelectedIndex <> -1 Then
            CtrlBackColours(cbSelControls.SelectedIndex) = cbBackColour.SelectedItem.ToString
            lnsBackColour.BorderColor = Color.FromName(cbBackColour.SelectedItem.ToString)
        Else
            lnsBackColour.BorderColor = Color.FromKnownColor(KnownColor.ControlText)
        End If
        btnApply.Enabled = True
    End Sub

    Private Sub btnDelForeColour_Click(sender As System.Object, e As System.EventArgs) Handles btnDelForeColour.MouseClick
        CtrlForeColours(cbSelControls.SelectedIndex) = String.Empty
        cbForeColour.SelectedIndex = -1
        cbForeColour.Text = String.Empty
        btnApply.Enabled = True
    End Sub

    Private Sub btnDelBackColour_Click(sender As System.Object, e As System.EventArgs) Handles btnDelBackColour.MouseClick
        CtrlBackColours(cbSelControls.SelectedIndex) = String.Empty
        cbBackColour.SelectedIndex = -1
        cbBackColour.Text = String.Empty
        btnApply.Enabled = True
    End Sub

    Private Sub btnDelForeColour_Click_1(sender As System.Object, e As System.EventArgs) Handles btnDelForeColour.Click
        Call Apply()
    End Sub

    Private Sub btnDelBackColour_Click_1(sender As System.Object, e As System.EventArgs) Handles btnDelBackColour.Click
        Call Apply()
    End Sub

    Private Sub btnSplashScreenImage_KeyUp(sender As Object, e As KeyEventArgs) Handles btnSplashScreenImage.KeyUp
        If JumpToForeColour AndAlso e.KeyValue = AscW(ControlChars.Tab) Then
            JumpToForeColour = False

            If cbSelControls.SelectedIndex <> -1 Then
                cbForeColour.Enabled = True
                cbForeColour.Focus()
                cbForeColour.SelectionStart = cbForeColour.Text.Length
            End If
        End If
    End Sub
End Class