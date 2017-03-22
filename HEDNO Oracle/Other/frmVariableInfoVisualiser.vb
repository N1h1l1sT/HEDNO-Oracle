Imports System.Drawing.Color
Imports System.Text
Imports RDotNet

Public Class frmVariableInfoVisualiser
    Public strLanguage_VariableInfoVisualiser() As String

    Public rlstVariableInfo As GenericVector = Nothing
    Public DatasetName As String = Nothing
    Public ColumnNames() As String = Nothing

    Private Sub frmVariableInfoVisualiser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'initialization
            Call VariableInfoVisualiser_Language(Me)
            frmSkin(Me, False)
            '/initialization

            If DatasetName <> "" Then Text = sa(strLanguage_VariableInfoVisualiser(1), DatasetName) '{0} Variables Information

            Location = New Point(My.Computer.Screen.Bounds.Left + My.Computer.Screen.Bounds.Width - Width,
                                 My.Computer.Screen.Bounds.Top + My.Computer.Screen.Bounds.Height - Height)

            If rlstVariableInfo IsNot Nothing Then
                Dim sbVariableInfoText As New StringBuilder

                If ColumnNames IsNot Nothing Then
                    '                                                               Var :
                    Dim VarStringLength As Integer = strLanguage_VariableInfoVisualiser(2).Length + (ColumnNames.Length - 1).ToString.Length
                    Dim VarInitialSpace As String = SpaceAString("", VarStringLength)
                    For i = 0 To ColumnNames.Length - 1
                        Dim VarDescription As String = String.Empty
                        Try 'description might not exist
                            VarDescription = (rlstVariableInfo(ColumnNames(i)).AsCharacter)("description")
                        Catch ex As Exception
                        End Try
                        Dim VarType As String = (rlstVariableInfo(ColumnNames(i)).AsCharacter)("varType")
                        Dim VarLow As String = (rlstVariableInfo(ColumnNames(i)).AsCharacter)("low")
                        Dim Varhigh As String = (rlstVariableInfo(ColumnNames(i)).AsCharacter)("high")
                        Dim Varlevels As String = String.Empty

                        sbVariableInfoText.AppendLine()
                        sbVariableInfoText.AppendLine()
                        '                                         Var {0}:
                        sbVariableInfoText.Append(SpaceAString(sa(strLanguage_VariableInfoVisualiser(3), (i + 1).ToString), VarStringLength)) '           Var 1:
                        sbVariableInfoText.Append(sa("{0}", ColumnNames(i))) '                                                      Label
                        If VarDescription <> String.Empty Then sbVariableInfoText.Append(sa(", {0}", VarDescription)) '                 , The Label for the supervised......
                        sbVariableInfoText.AppendLine()

                        If VarType.ToLower = "integer".ToLower OrElse VarType.ToLower = "numeric".ToLower Then
                            '                            '{0}Type: {1}
                            sbVariableInfoText.Append(sa(strLanguage_VariableInfoVisualiser(4), VarInitialSpace, VarType))
                            '                             , Min: {0}, Max: {1}
                            sbVariableInfoText.Append(sa(strLanguage_VariableInfoVisualiser(5), VarLow, Varhigh))

                        ElseIf VarType.ToLower = "factor".ToLower Then
                            'Dim AllLevels As String = rlstVariableInfo(ColumnNames(i)).AsCharacter("levels")
                            'Dim tmpFirstValues() As String = (From Level As String In (rlstVariableInfo(ColumnNames(i)).AsCharacter)("levels") Select Level).Take(3).ToArray
                            'Dim tmpLastValue As String = (From Level As String In (rlstVariableInfo(ColumnNames(i)).AsCharacter)("levels") Select Level).Last
                            'Varlevels = ArrayBox(False, ",", 0, False, tmpFirstValues) & " ... " & tmpLastValue

                            'sbVariableInfoText.Append(sa("{2}{0} Factor Levels: [{1}],", Varhigh, Varlevels, VarInitialSpace))
                            '                            {2}{0} Factor Levels.
                            sbVariableInfoText.Append(sa(strLanguage_VariableInfoVisualiser(6), Varhigh, Varlevels, VarInitialSpace))
                        End If

                    Next
                Else
                    '                                                               Var :
                    Dim VarStringLength As Integer = strLanguage_VariableInfoVisualiser(2).Length + (rlstVariableInfo.Length - 1).ToString.Length
                    Dim VarInitialSpace As String = SpaceAString("", VarStringLength)
                    For i = 0 To rlstVariableInfo.Length - 1
                        Dim VarDescription As String = String.Empty
                        Try 'description might not exist
                            VarDescription = (rlstVariableInfo(i).AsCharacter)("description")
                        Catch ex As Exception
                        End Try
                        Dim VarType As String = (rlstVariableInfo(i).AsCharacter)("varType")
                        Dim VarLow As String = (rlstVariableInfo(i).AsCharacter)("low")
                        Dim Varhigh As String = (rlstVariableInfo(i).AsCharacter)("high")
                        Dim Varlevels As String = String.Empty

                        sbVariableInfoText.AppendLine()
                        sbVariableInfoText.AppendLine()
                        '                                         Var {0}:
                        sbVariableInfoText.Append(SpaceAString(sa(strLanguage_VariableInfoVisualiser(3), (i + 1).ToString), VarStringLength)) '           Var 1:
                        sbVariableInfoText.Append(sa("{0}", rlstVariableInfo.Names(i))) '                                           Label
                        If VarDescription <> String.Empty Then sbVariableInfoText.Append(sa(", {0}", VarDescription)) '                 , The Label for the supervised......
                        sbVariableInfoText.AppendLine()

                        If VarType.ToLower = "integer".ToLower OrElse VarType.ToLower = "numeric".ToLower Then
                            '                            '{0}Type: {1}
                            sbVariableInfoText.Append(sa(strLanguage_VariableInfoVisualiser(4), VarInitialSpace, VarType))
                            '                             , Min: {0}, Max: {1}
                            sbVariableInfoText.Append(sa(strLanguage_VariableInfoVisualiser(5), VarLow, Varhigh))

                        ElseIf VarType.ToLower = "factor".ToLower Then
                            'Dim tmpFirstValues() As String = (From Level As String In (rlstVariableInfo(i).AsCharacter)("levels") Select Level).Take(3).ToArray
                            'Dim tmpLastValue As String = (From Level As String In (rlstVariableInfo(i).AsCharacter)("levels") Select Level).Last
                            'Varlevels = ArrayBox(False, ",", 0, False, tmpFirstValues) & " ... " & tmpLastValue

                            'sbVariableInfoText.Append(sa("{2}{0} Factor Levels: [{1}],", Varhigh, Varlevels, VarInitialSpace))
                            '                            {2}{0} Factor Levels.
                            sbVariableInfoText.Append(sa(strLanguage_VariableInfoVisualiser(6), Varhigh, Varlevels, VarInitialSpace))
                        End If
                    Next
                End If

                txtVariableInfo.Text = sbVariableInfoText.ToString.Substring((vbCrLf.Length * 2))
                txtVariableInfo.SelectionStart = 0

                'Notify(ArrayBox(rlstVariableInfo.Names), cyan, black, 20)
            Else

            End If
        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub
End Class