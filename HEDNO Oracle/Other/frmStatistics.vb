Imports System.Drawing.Color
Imports System.IO
Imports System.Text
Imports RDotNet

Public Class frmStatistics
    Public strLanguage_Statistics() As String

    Public rlstVariableInfo As GenericVector = Nothing
    Public ClassificationModelName As String = Nothing
    Public SinkFilePath As String = Nothing

    Private Sub frmStatistics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'initialization
            Call Statistics_Language(Me)
            frmSkin(Me, False)

            If ClassificationModelName <> "" Then Text = sa(strLanguage_Statistics(1), ClassificationModelName) '{0} Statistics
            '/initialization


            If rlstVariableInfo IsNot Nothing Then
                Dim sbVariableInfoText As New StringBuilder

                For i = 0 To rlstVariableInfo.Length - 1
                    Dim VarDescription As String = String.Empty

                    Dim VarType As String = (rlstVariableInfo(i).AsCharacter)("varType")

                    sbVariableInfoText.AppendLine()
                    sbVariableInfoText.AppendLine()



                Next

            ElseIf SinkFilePath <> "" Then
                Dim CurSinkFilePath As String = doProperFileName(SinkFilePath)
                If File.Exists(CurSinkFilePath) Then
                    txtStatistics.Text = File.ReadAllText(CurSinkFilePath)
                    File.Delete(CurSinkFilePath)
                End If
            End If

            txtStatistics.SelectionStart = 0


        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub
End Class