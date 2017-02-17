Imports System.Data.SqlClient
Imports System.Drawing.Color
Imports System.IO
Imports System.Text

Public Class frmCreateSQLView
    Public strLanguage_CreateSQLView As String()

    Dim SQLFiles() As String
    Dim SQLFileNames() As String

    Private Sub frmCreateSQLView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'initialization
            Call CreateSQLView_Language(Me)
            frmSkin(Me, False)
            '/initialization

            fswSQLViewFiles.Path = strSQLViewsDir
            SQLFiles = Directory.GetFiles(strSQLViewsDir, "*.sql")
            SQLFileNames = (From sqlFile In SQLFiles Select GetFileNameAlone(sqlFile)).ToArray
            clbSQLViews.DataSource = Nothing
            clbSQLViews.DataSource = SQLFileNames

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub fswSQLViewFiles_Changed(sender As Object, e As FileSystemEventArgs) Handles fswSQLViewFiles.Created, fswSQLViewFiles.Deleted, fswSQLViewFiles.Renamed
        SQLFiles = Directory.GetFiles(strSQLViewsDir, "*.sql")
        SQLFileNames = (From sqlFile In SQLFiles Select GetFileNameAlone(sqlFile)).ToArray
        clbSQLViews.DataSource = Nothing
        clbSQLViews.DataSource = SQLFileNames
    End Sub

    Private Sub btnCreateSQLViews_Click(sender As Object, e As EventArgs) Handles btnCreateSQLViews.Click

        Try
            If clbSQLViews.CheckedIndices.Count > 0 Then
                If ConnectedToSQLServer = True Then
                    Dim sbResultsInfo As New StringBuilder
                    For i As Integer = 0 To clbSQLViews.CheckedIndices.Count - 1
                        Dim RowIndex As Integer = -1

                        Dim SQLTextSting As String = String.Format(File.ReadAllText(SQLFiles(clbSQLViews.CheckedIndices(i))), DatabaseName)
                        Dim SQLTextStingAr() As String = SQLTextSting.Split(CChar(vbNewLine))

                        Dim SQLViewName As String = GetSubstrAfterString(SQLTextStingAr, "CREATE VIEW",,,, RowIndex,, True).Trim
                        If SQLViewName <> "" Then SQLViewName = (From str As String In SQLViewName.Split("."c) Select str).Last.Replace("[", "").Replace("]", "").Trim

                        Dim ViewExists As Boolean = SQLViewExists(SQLConn, DatabaseName, SQLViewName)

                        'rdbDeleteItAndCreateIt to delete it before using the "CREATE"
                        If chkDeleteAll.Checked OrElse rdbDeleteItAndCreateIt.Checked Then
                            If ViewExists Then
                                Dim DeletionResult As Boolean = DeleteSQLView(SQLViewName)
                                sbResultsInfo.AppendLine()
                                If DeletionResult = True Then sbResultsInfo.Append(SQLViewName & ": Deleted") Else sbResultsInfo.Append(SQLViewName & ": Failed to Delete")
                            ElseIf chkDeleteAll.Checked Then
                                sbResultsInfo.AppendLine()
                                sbResultsInfo.Append(SQLViewName & ": Didn't exist in the first place")
                            End If
                        End If

                        If gbSQLViewOptions.Enabled = True Then
                            If rdbAlterIt.Checked = True AndAlso ViewExists = True Then
                                SQLTextStingAr(RowIndex) = <SQL>ALTER VIEW [dbo].[<%= SQLViewName %>]</SQL>.Value
                            End If
                            'If rdbDeleteItAndCreateIt was checked it has already been deleted before; so let's just create it now
                            If Not ViewExists OrElse Not rdbDoNothing.Checked Then
                                Dim Succeeded As Boolean = ExecuteSQLQuery(ArrayBox(SQLTextStingAr))
                                If rdbDeleteItAndCreateIt.Checked AndAlso ViewExists Then
                                    If Succeeded Then sbResultsInfo.Append(" and re-Created") Else sbResultsInfo.Append(" but Failed to Create it")
                                ElseIf ViewExists And rdbAlterIt.Checked Then
                                    sbResultsInfo.AppendLine()
                                    If Succeeded Then sbResultsInfo.Append(SQLViewName & ": Altered") Else sbResultsInfo.Append(SQLViewName & ": Failed to Alter it")
                                ElseIf Not ViewExists Then
                                    sbResultsInfo.AppendLine()
                                    If Succeeded Then sbResultsInfo.Append(SQLViewName & ": Created") Else sbResultsInfo.Append(SQLViewName & ": Failed to Create it")
                                End If
                            Else
                                sbResultsInfo.Append(SQLViewName & ": No Action")
                            End If
                        End If
                    Next
                    MsgBox(sbResultsInfo.ToString.Substring(2), MsgBoxStyle.Information)

                Else
                    MsgBox("There is no connection to the SQL Server!", MsgBoxStyle.Exclamation)
                End If

            Else
                MsgBox("You've selected no views from the Checked List Box and there's therefore nothing to do.", MsgBoxStyle.Information)
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub chkDeleteAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkDeleteAll.CheckedChanged
        If chkDeleteAll.Checked = True Then
            gbSQLViewOptions.Enabled = False
            btnCreateSQLViews.Text = "Delete SQL &Views"
        Else
            gbSQLViewOptions.Enabled = True
            btnCreateSQLViews.Text = "&Create SQL Views"
        End If
    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        If clbSQLViews.CheckedIndices.Count = clbSQLViews.Items.Count Then
            For i = 0 To clbSQLViews.Items.Count - 1
                clbSQLViews.SetItemChecked(i, False)
            Next
            btnSelectAll.Text = "&Select All"
        Else
            For i = 0 To clbSQLViews.Items.Count - 1
                clbSQLViews.SetItemChecked(i, True)
            Next
            btnSelectAll.Text = "&Unselect All"
        End If
    End Sub

    Private Sub clbSQLViews_SelectedIndexChanged(sender As Object, e As EventArgs) Handles clbSQLViews.SelectedIndexChanged, clbSQLViews.DoubleClick
        If clbSQLViews.CheckedIndices.Count = clbSQLViews.Items.Count Then
            btnSelectAll.Text = "&Unselect All"
        Else
            btnSelectAll.Text = "&Select All"
        End If
    End Sub
End Class