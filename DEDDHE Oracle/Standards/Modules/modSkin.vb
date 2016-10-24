'Version 1.6.2  2013-04-06
'Added "Panel", "SplitContainer"
'Fixed a bug that when two controls start with the same name but one has bigger name, only one control would change colour
'Added Comments to ease understanding
Option Strict On

Imports System.IO

Module modSkin
    Public GlobalIcon As New Icon(strRoot & "icon.ico")
    Dim txtForeColours As New TextBox
    Dim txtBackColours As New TextBox

    Dim Allcontrols As New List(Of String)

    Public Sub frmSkin(ByVal frm As Form, Optional ByVal UnskinFirst As Boolean = True, Optional MDIPanel As Panel = Nothing)
        If UnskinFirst Then frmUnSkin(frm)
        frm.Icon = GlobalIcon

        With frm
            If My.Settings.strSkinChoice <> "" AndAlso My.Settings.strSkinChoice.ToLower <> "none" Then

                Dim strFormImage As String = strSkin & My.Settings.strSkinChoice & .Name & "\" & .Name & ".jpg" 'BackGroundImage File Path
                If File.Exists(strFormImage) Then
                    Dim img As Image = Image.FromFile(strFormImage) 'Loading BackGroundImage

                    If .Size.Width > img.Size.Width - 50 AndAlso .Size.Height > img.Size.Height - 50 _
                    AndAlso .Size.Width < img.Size.Width + 50 AndAlso .Size.Height < img.Size.Height + 50 Then 'Checking if the pic is within acceptable distortion limits
                        If frm.IsMdiContainer AndAlso MDIPanel IsNot Nothing Then MDIPanel.BackgroundImage = img Else .BackgroundImage = img

                    Else 'If not, then let's see if a Pattern picture exists
                        Dim strFormBigImage As String = strSkin & My.Settings.strSkinChoice & .Name & "\" & .Name & "_Pattern.jpg"
                        If File.Exists(strFormBigImage) Then 'If one exists, then lets load the Pattern instead
                            If frm.IsMdiContainer AndAlso MDIPanel IsNot Nothing Then MDIPanel.BackgroundImage = img Else .BackgroundImage = Image.FromFile(strFormBigImage)

                        Else 'If not, then we go back to the original picture even if it gets distorted
                            If frm.IsMdiContainer AndAlso MDIPanel IsNot Nothing Then MDIPanel.BackgroundImage = img Else .BackgroundImage = img
                        End If
                    End If

                End If

                Dim strFormForeColours As String = strSkin & My.Settings.strSkinChoice & .Name & "\" & .Name & "_ForeColours.txt"
                Dim strFormBackColours As String = strSkin & My.Settings.strSkinChoice & .Name & "\" & .Name & "_BackColours.txt"
                If File.Exists(strFormForeColours) Or File.Exists(strFormBackColours) Then
                    Dim ExistForecolours, ExistBackColours As Boolean

                    If File.Exists(strFormForeColours) Then
                        ReadFile(strFormForeColours, txtForeColours)
                        ExistForecolours = True
                    End If

                    If File.Exists(strFormBackColours) Then
                        ReadFile(strFormBackColours, txtBackColours)
                        ExistBackColours = True
                    End If

                    For Each MainCtrl As Control In .Controls
                        Call SkinControl(MainCtrl, ExistForecolours, ExistBackColours, .Name)
                    Next

                    If Not IsNothing(.MainMenuStrip) AndAlso .MainMenuStrip.Items.Count > 0 Then
                        For Each mni As Object In .MainMenuStrip.Items
                            If TypeOf mni Is ToolStripMenuItem Then
                                Call SkinMenu(DirectCast(mni, ToolStripMenuItem), ExistForecolours, ExistBackColours)
                            End If
                        Next
                    End If

                End If


            End If

        End With
    End Sub

    Private Sub SkinControl(ByVal ctrl As Control, ByVal ExistForecolours As Boolean, ByVal ExistBackColours As Boolean, ByVal Name As String)

        If TypeOf ctrl Is Button Then
            If ExistForecolours Then
                For i = 0 To txtForeColours.Lines.Length - 1
                    If txtForeColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                        DirectCast(ctrl, Button).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                        Exit For
                    End If
                Next
            End If
            If ExistBackColours Then
                For i = 0 To txtBackColours.Lines.Length - 1
                    If txtBackColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                        DirectCast(ctrl, Button).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                        Exit For
                    End If
                Next
            End If

            If File.Exists(strSkin & My.Settings.strSkinChoice & Name & "\" & ctrl.Name & ".jpg") Then
                DirectCast(ctrl, Button).BackgroundImageLayout = ImageLayout.Stretch
                DirectCast(ctrl, Button).BackgroundImage = Image.FromFile(strSkin & My.Settings.strSkinChoice & Name & "\" & ctrl.Name & ".jpg")
            End If

        ElseIf TypeOf ctrl Is Label Then
            If ExistForecolours Then
                For i = 0 To txtForeColours.Lines.Length - 1
                    If txtForeColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                        DirectCast(ctrl, Label).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                        Exit For
                    End If
                Next
            End If
            If ExistBackColours Then
                For i = 0 To txtBackColours.Lines.Length - 1
                    If txtBackColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                        DirectCast(ctrl, Label).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                        Exit For
                    End If
                Next
            End If

        ElseIf TypeOf ctrl Is RadioButton Then
            If ExistForecolours Then
                For i = 0 To txtForeColours.Lines.Length - 1
                    If txtForeColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                        DirectCast(ctrl, RadioButton).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                        Exit For
                    End If
                Next
            End If
            If ExistBackColours Then
                For i = 0 To txtBackColours.Lines.Length - 1
                    If txtBackColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                        DirectCast(ctrl, RadioButton).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                        Exit For
                    End If
                Next
            End If

        ElseIf TypeOf ctrl Is CheckBox Then
            If ExistForecolours Then
                For i = 0 To txtForeColours.Lines.Length - 1
                    If txtForeColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                        DirectCast(ctrl, CheckBox).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                        Exit For
                    End If
                Next
            End If
            If ExistBackColours Then
                For i = 0 To txtBackColours.Lines.Length - 1
                    If txtBackColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                        DirectCast(ctrl, CheckBox).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                        Exit For
                    End If
                Next
            End If

        ElseIf TypeOf ctrl Is GroupBox Then
            If ExistForecolours Then
                For i = 0 To txtForeColours.Lines.Length - 1
                    If txtForeColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                        DirectCast(ctrl, GroupBox).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                        Exit For
                    End If
                Next
            End If
            If ExistBackColours Then
                For i = 0 To txtBackColours.Lines.Length - 1
                    If txtBackColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                        DirectCast(ctrl, GroupBox).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                        Exit For
                    End If
                Next
            End If

        ElseIf TypeOf ctrl Is SplitContainer Then
            If ExistForecolours Then
                For i = 0 To txtForeColours.Lines.Length - 1
                    If txtForeColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                        DirectCast(ctrl, SplitContainer).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                        Exit For
                    End If
                Next
            End If
            If ExistBackColours Then
                For i = 0 To txtBackColours.Lines.Length - 1
                    If txtBackColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                        DirectCast(ctrl, SplitContainer).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                        Exit For
                    End If
                Next
            End If

        ElseIf TypeOf ctrl Is Panel Then
            If ExistForecolours Then
                For i = 0 To txtForeColours.Lines.Length - 1
                    If txtForeColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                        DirectCast(ctrl, Panel).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                        Exit For
                    End If
                Next
            End If
            If ExistBackColours Then
                For i = 0 To txtBackColours.Lines.Length - 1
                    If txtBackColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                        DirectCast(ctrl, Panel).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                        Exit For
                    End If
                Next
            End If
        End If

        If ctrl.HasChildren Then
            For Each subCtrl As Control In ctrl.Controls
                SkinControl(subCtrl, ExistForecolours, ExistBackColours, Name)
            Next
        End If
    End Sub

    Private Sub SkinMenu(ByVal mni As ToolStripMenuItem, ByVal ExistForeColours As Boolean, ByVal ExistBackColours As Boolean)
        Dim mniName As String = DirectCast(mni, ToolStripMenuItem).Name

        If ExistForeColours Then
            For i = 0 To txtForeColours.Lines.Length - 1
                If txtForeColours.Lines(i).StartsWith(mniName & "=") Then
                    DirectCast(mni, ToolStripMenuItem).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((mniName & "=").Length))
                    Exit For
                End If
            Next
        End If
        If ExistBackColours Then
            For i = 0 To txtBackColours.Lines.Length - 1
                If txtBackColours.Lines(i).StartsWith(mniName & "=") Then
                    DirectCast(mni, ToolStripMenuItem).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((mniName & "=").Length))
                    Exit For
                End If
            Next
        End If

        If mni.HasDropDownItems Then
            For Each subMni As Object In mni.DropDownItems
                If TypeOf (subMni) Is ToolStripMenuItem Then SkinMenu(DirectCast(subMni, ToolStripMenuItem), ExistForeColours, ExistBackColours)
            Next
        End If

    End Sub

    Public Sub frmUnSkin(ByVal frm As Form)
        With frm
            .BackgroundImage = Nothing

            Dim strDeForeColours As String = strSkin & My.Settings.strSkinChoice & "\" & .Name & "\" & .Name & "_DeForeColours.txt"
            Dim strDeBackColours As String = strSkin & My.Settings.strSkinChoice & "\" & .Name & "\" & .Name & "_DeBackColours.txt"

            Dim ExistDeForeColours, ExistDeBackColours As Boolean

            If File.Exists(strDeForeColours) Then
                ReadFile(strDeForeColours, txtForeColours)
                ExistDeForeColours = True
            End If

            If File.Exists(strDeBackColours) Then
                ReadFile(strDeBackColours, txtBackColours)
                ExistDeBackColours = True
            End If

            If Not IsNothing(.MainMenuStrip) AndAlso .MainMenuStrip.Items.Count > 0 Then
                For Each mni As Object In .MainMenuStrip.Items
                    If TypeOf mni Is ToolStripMenuItem Then
                        Call UnSkinMenu(DirectCast(mni, ToolStripMenuItem), ExistDeForeColours, ExistDeBackColours)
                    End If
                Next
            End If

            For Each MainCtrl As Control In .Controls
                Call UnSkinControl(MainCtrl, ExistDeForeColours, ExistDeBackColours, .Name)
            Next

        End With
    End Sub

    Private Sub UnSkinControl(ByVal ctrl As Control, ByVal ExistDeForeColours As Boolean, ByVal ExistDeBackColours As Boolean, ByVal Name As String)
        Try
            If TypeOf ctrl Is Button Then
                DirectCast(ctrl, Button).BackgroundImage = Nothing
                DirectCast(ctrl, Button).ForeColor = Button.DefaultForeColor
                If (Not DirectCast(ctrl, Button).BackColor = SystemColors.Control) AndAlso (Not DirectCast(ctrl, Button).BackColor = Color.Transparent) Then DirectCast(ctrl, Button).BackColor = Button.DefaultBackColor

                If ExistDeForeColours Then
                    For i = 0 To txtForeColours.Lines.Length - 1
                        If txtForeColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                            DirectCast(ctrl, Button).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                            Exit For
                        End If
                    Next
                End If
                If ExistDeBackColours Then
                    For i = 0 To txtBackColours.Lines.Length - 1
                        If txtBackColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                            DirectCast(ctrl, Button).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                            Exit For
                        End If
                    Next
                End If

            ElseIf TypeOf ctrl Is Label Then
                DirectCast(ctrl, Label).ForeColor = Label.DefaultForeColor
                DirectCast(ctrl, Label).BackColor = Color.Transparent

                If ExistDeForeColours Then
                    For i = 0 To txtForeColours.Lines.Length - 1
                        If txtForeColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                            DirectCast(ctrl, Label).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                            Exit For
                        End If
                    Next
                End If
                If ExistDeBackColours Then
                    For i = 0 To txtBackColours.Lines.Length - 1
                        If txtBackColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                            DirectCast(ctrl, Label).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                            Exit For
                        End If
                    Next
                End If

            ElseIf TypeOf ctrl Is RadioButton Then
                DirectCast(ctrl, RadioButton).ForeColor = RadioButton.DefaultForeColor
                DirectCast(ctrl, RadioButton).BackColor = Color.Transparent

                If ExistDeForeColours Then
                    For i = 0 To txtForeColours.Lines.Length - 1
                        If txtForeColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                            DirectCast(ctrl, RadioButton).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                            Exit For
                        End If
                    Next
                End If
                If ExistDeBackColours Then
                    For i = 0 To txtBackColours.Lines.Length - 1
                        If txtBackColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                            DirectCast(ctrl, RadioButton).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                            Exit For
                        End If
                    Next
                End If

            ElseIf TypeOf ctrl Is CheckBox Then
                DirectCast(ctrl, CheckBox).ForeColor = CheckBox.DefaultForeColor
                DirectCast(ctrl, CheckBox).BackColor = Color.Transparent

                If ExistDeForeColours Then
                    For i = 0 To txtForeColours.Lines.Length - 1
                        If txtForeColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                            DirectCast(ctrl, CheckBox).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                            Exit For
                        End If
                    Next
                End If
                If ExistDeBackColours Then
                    For i = 0 To txtBackColours.Lines.Length - 1
                        If txtBackColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                            DirectCast(ctrl, CheckBox).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                            Exit For
                        End If
                    Next
                End If

            ElseIf TypeOf ctrl Is GroupBox Then
                DirectCast(ctrl, GroupBox).ForeColor = GroupBox.DefaultForeColor
                DirectCast(ctrl, GroupBox).BackColor = Color.Transparent

                If ExistDeForeColours Then
                    For i = 0 To txtForeColours.Lines.Length - 1
                        If txtForeColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                            DirectCast(ctrl, GroupBox).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                            Exit For
                        End If
                    Next
                End If
                If ExistDeBackColours Then
                    For i = 0 To txtBackColours.Lines.Length - 1
                        If txtBackColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                            DirectCast(ctrl, GroupBox).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                            Exit For
                        End If
                    Next
                End If

            ElseIf TypeOf ctrl Is SplitContainer Then
                DirectCast(ctrl, SplitContainer).ForeColor = SplitContainer.DefaultForeColor
                DirectCast(ctrl, SplitContainer).BackColor = Color.Transparent

                If ExistDeForeColours Then
                    For i = 0 To txtForeColours.Lines.Length - 1
                        If txtForeColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                            DirectCast(ctrl, SplitContainer).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                            Exit For
                        End If
                    Next
                End If
                If ExistDeBackColours Then
                    For i = 0 To txtBackColours.Lines.Length - 1
                        If txtBackColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                            DirectCast(ctrl, SplitContainer).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                            Exit For
                        End If
                    Next
                End If

            ElseIf TypeOf ctrl Is Panel Then
                DirectCast(ctrl, Panel).ForeColor = Panel.DefaultForeColor
                DirectCast(ctrl, Panel).BackColor = Color.Transparent

                If ExistDeForeColours Then
                    For i = 0 To txtForeColours.Lines.Length - 1
                        If txtForeColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                            DirectCast(ctrl, Panel).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                            Exit For
                        End If
                    Next
                End If
                If ExistDeBackColours Then
                    For i = 0 To txtBackColours.Lines.Length - 1
                        If txtBackColours.Lines(i).StartsWith(ctrl.Name & "=") Then
                            DirectCast(ctrl, Panel).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((ctrl.Name & "=").Length))
                            Exit For
                        End If
                    Next
                End If
            End If

            If ctrl.HasChildren Then
                For Each subCtrl As Control In ctrl.Controls
                    UnSkinControl(subCtrl, ExistDeForeColours, ExistDeBackColours, Name)
                Next
            End If

        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub

    Private Sub UnSkinMenu(ByVal mni As ToolStripMenuItem, ByVal ExistDeForeColours As Boolean, ByVal ExistDeBackColours As Boolean)
        Dim mniName As String = DirectCast(mni, ToolStripMenuItem).Name

        DirectCast(mni, ToolStripMenuItem).ForeColor = SystemColors.ControlText
        DirectCast(mni, ToolStripMenuItem).BackColor = Color.Transparent

        If ExistDeForeColours Then
            For i = 0 To txtForeColours.Lines.Length - 1
                If txtForeColours.Lines(i).StartsWith(mniName & "=") Then
                    DirectCast(mni, ToolStripMenuItem).ForeColor = Color.FromName(txtForeColours.Lines(i).Replace(" ", "").Substring((mniName & "=").Length))
                    Exit For
                End If
            Next
        End If
        If ExistDeBackColours Then
            For i = 0 To txtBackColours.Lines.Length - 1
                If txtBackColours.Lines(i).StartsWith(mniName & "=") Then
                    DirectCast(mni, ToolStripMenuItem).BackColor = Color.FromName(txtBackColours.Lines(i).Replace(" ", "").Substring((mniName & "=").Length))
                    Exit For
                End If
            Next
        End If

        If mni.HasDropDownItems Then
            For Each subMni As Object In mni.DropDownItems
                If TypeOf subMni Is ToolStripMenuItem Then UnSkinMenu(DirectCast(subMni, ToolStripMenuItem), ExistDeForeColours, ExistDeBackColours)
            Next
        End If

    End Sub

End Module
