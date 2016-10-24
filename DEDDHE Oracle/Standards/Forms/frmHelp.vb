'Version 1.0.2 2013-04-21

Imports System.IO

Public Class frmHelp
    Public strLanguage_frmHelp() As String
    Dim CommandsFiles() As String = {}

    Private Sub frmHelp_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call Help_Language(Me)

        frmSkin(Me)

        Try
            CommandsFiles = Directory.GetFiles(strLanguageFolders & CurrentLanguage & "\Help")
        Catch ex As Exception
        End Try

        If CommandsFiles.Length > 0 Then
            Dim CommandNames(CommandsFiles.Length - 1) As String
            For i = 0 To CommandsFiles.Length - 1
                CommandNames(i) = GetFileNameAlone(CommandsFiles(i))
            Next

            lsbCommands.DataSource = CommandNames

        Else
            MsgBox(strLanguage_frmHelp(5), MsgBoxStyle.Critical) 'There are no registered commands in the archive!
            Close()
        End If

    End Sub

    Private Sub lsbCommands_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lsbCommands.SelectedIndexChanged
        If lsbCommands.SelectedIndex <> -1 Then wbUseage.Navigate(strLanguageFolders & CurrentLanguage & "\Help\" & lsbCommands.SelectedItem.ToString & ".html")
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub scHelp_SplitterMoved(sender As System.Object, e As System.Windows.Forms.SplitterEventArgs) Handles scHelp.SplitterMoved
        lblUsage.Location = New Point(scHelp.SplitterDistance, lblUsage.Location.Y)
    End Sub

End Class