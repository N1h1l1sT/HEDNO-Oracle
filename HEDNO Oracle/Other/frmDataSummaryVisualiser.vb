Option Strict On

Imports RDotNet
Imports System.IO
Imports System.Drawing.Printing

Public Class frmDataSummaryVisualiser
    Public dfDataSummary As DataFrame = Nothing
    Public DatasetName As String = Nothing

    Public strLanguage_DataSummaryVisualiser() As String
    Public DefaultSaveFileName As String = String.Empty
    Public LastSaveFile As String = String.Empty
    Public LastSeparationChar As Char = ControlChars.Tab

    Dim MyDataGridViewPrinter As clsDataGridViewPrinter
    Dim CurrentColumnIndex As Integer = -1

    Dim dtDataSummary As New DataTable

    Private Sub frmDataSummaryVisualiser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'initialization
            Call DataSummaryVisualiser_Language(Me)
            frmSkin(Me, False)
            '/initialization

            If DatasetName <> "" Then Text = DatasetName & " Data Summary"

            Location = New Point(My.Computer.Screen.Bounds.Left, My.Computer.Screen.Bounds.Top)

            For i = 0 To dfDataSummary.ColumnCount - 1
                dtDataSummary.Columns.Add(dfDataSummary.ColumnNames(i))
            Next


            For j = 0 To dfDataSummary.RowCount - 1
                Dim DataSummaryRow As DataRow = dtDataSummary.NewRow

                For i = 0 To dfDataSummary.ColumnCount - 1
                    Dim PotentialValue As String = CType(dfDataSummary(j, i), String)

                    If IsNumeric(PotentialValue) OrElse isNumericExtended(PotentialValue) Then
                        Dim BecameZeroByRounding As Boolean = False
                        DataSummaryRow(i) = StatRound(PotentialValue, CUInt(RoundAt), , BecameZeroByRounding)
                        If BecameZeroByRounding Then DataSummaryRow(i) = Zero_A_String("", RoundAt) & "*"

                    Else
                        DataSummaryRow(i) = PotentialValue
                    End If
                Next
                dtDataSummary.Rows.Add(DataSummaryRow)
            Next

            dgvDataSummary.DataSource = dtDataSummary

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub dgvDataSummary_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvDataSummary.DataBindingComplete
        If dgvDataSummary.DataSource IsNot Nothing Then
            For Each r As DataGridViewRow In dgvDataSummary.Rows
                dgvDataSummary.Rows(r.Index).HeaderCell.Value = (r.Index + 1).ToString()
            Next
            For i = 0 To dtDataSummary.Columns.Count - 1
                dgvDataSummary.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End If
    End Sub

    Private Sub txtColumnRen_KeyUp(sender As Object, e As KeyEventArgs) Handles tstxtColumnRen.KeyUp
        If e.KeyCode = Keys.Enter Then
            If tstxtColumnRen.Text <> "" AndAlso CurrentColumnIndex >= 0 Then
                dgvDataSummary.Columns(CurrentColumnIndex).HeaderText = tstxtColumnRen.Text
                dgvDataSummary.Columns(CurrentColumnIndex).Name = tstxtColumnRen.Text
                CurrentColumnIndex = -1
                tstxtColumnRen.Text = ""
                cmsColumnRen.Close()
                dgvDataSummary.ContextMenuStrip = Nothing
            End If
        End If
    End Sub

    Private Sub dgvDataSummary_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvDataSummary.ColumnHeaderMouseClick
        If e.Button = MouseButtons.Right Then
            CurrentColumnIndex = e.ColumnIndex
            cmsColumnRen.Show(Cursor.Position)
            tstxtColumnRen.Focus()

        ElseIf e.Button = MouseButtons.Left Then
            dgvDataSummary.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect
            If dgvDataSummary.SelectedColumns.Count = 0 Then dgvDataSummary.Columns(e.ColumnIndex).Selected = True
        End If
    End Sub

    Private Sub dgvDataSummary_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvDataSummary.RowHeaderMouseClick
        If e.Button = MouseButtons.Left Then
            dgvDataSummary.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
            If dgvDataSummary.SelectedRows.Count = 0 Then dgvDataSummary.Rows(e.RowIndex).Selected = True
        End If
    End Sub


    Private Sub dgvDataSummary_DataSourceChanged(sender As Object, e As EventArgs) Handles dgvDataSummary.DataSourceChanged
        Try
            Call DataSummaryVisualiser_Language(Me)
            If dgvDataSummary.ColumnCount > 0 Then lblRowsCount.Text = strLanguage_DataSummaryVisualiser(12) & dgvDataSummary.RowCount 'Rows Count:
            lblSelectedColumns.Text = strLanguage_DataSummaryVisualiser(13) & dgvDataSummary.ColumnCount 'Columns Count:
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgvDataSummary_SelectionChanged(sender As Object, e As EventArgs) Handles dgvDataSummary.SelectionChanged
        If Not IsNothing(strLanguage_DataSummaryVisualiser) Then
            If dgvDataSummary.SelectedCells.Count > 0 Then
                lblCurRow.Text = strLanguage_DataSummaryVisualiser(14) & dgvDataSummary.SelectedCells(dgvDataSummary.SelectedCells.Count - 1).RowIndex.ToString 'Current Row:
                lblCurColumn.Text = strLanguage_DataSummaryVisualiser(15) & dgvDataSummary.SelectedCells(dgvDataSummary.SelectedCells.Count - 1).ColumnIndex.ToString 'Current Column:
                lblSelectedCells.Text = strLanguage_DataSummaryVisualiser(6) & dgvDataSummary.SelectedCells.Count 'Selected Cells:
            End If

            lblSelectedRows.Text = strLanguage_DataSummaryVisualiser(7) & dgvDataSummary.SelectedRows.Count 'Selected Rows:
            lblSelectedColumns.Text = strLanguage_DataSummaryVisualiser(8) & dgvDataSummary.SelectedColumns.Count 'Selected Columns:
        End If

    End Sub

    Private Sub dgvDataSummary_CellContentClick(sender As System.Object, e As DataGridViewCellEventArgs) Handles dgvDataSummary.CellContentClick
        If dgvDataSummary.SelectedCells.Count > 0 Then
            lblCurRow.Text = strLanguage_DataSummaryVisualiser(14) & dgvDataSummary.SelectedCells(dgvDataSummary.SelectedCells.Count - 1).RowIndex.ToString
            lblCurColumn.Text = strLanguage_DataSummaryVisualiser(15) & dgvDataSummary.SelectedCells(dgvDataSummary.SelectedCells.Count - 1).ColumnIndex.ToString
        End If
    End Sub


#Region "Functions"
    Private Sub SaveAs()
        Try
            sfdExport.FileName = ""
            sfdExport.InitialDirectory = strDataDir
            sfdExport.DefaultExt = "txt"
            sfdExport.FileName = DefaultSaveFileName & " " & DateString & "." & sfdExport.DefaultExt  'Today's Statistics or something similar
            sfdExport.Filter = strLanguage_DataSummaryVisualiser(4) & ProgrammeExtentionDescriptions & "|*." & ProgrammeExtentionValue & "|Comma Separated Values|*.csv|Text File|*.txt|All Files|*.*"  'Comma-Separated Values Files
            Dim SaveDialogResult As DialogResult = sfdExport.ShowDialog()
            If SaveDialogResult = DialogResult.OK Then

                If sfdExport.FileName.Contains("\") Then
                    Dim LastSlashIndex As Integer = sfdExport.FileName.LastIndexOf("\"c)
                    Dim ParentDir As String = Mid(sfdExport.FileName, 1, LastSlashIndex)
                    If Not Directory.Exists(ParentDir) Then
                        sfdExport.FileName = strDocumentsProgDir & sfdExport.FileName.Replace("\", "")
                    End If
                Else
                    sfdExport.FileName = strDocumentsProgDir & sfdExport.FileName
                End If

                Dim SeparationCharacter As Char = ControlChars.Tab
                If sfdExport.FileName.EndsWith(".csv") Then SeparationCharacter = ","c

                Dim Success As Boolean = Save_dgv_To_csv(sfdExport.FileName, dgvDataSummary, True, , , SeparationCharacter, False)
                If Success Then
                    LastSaveFile = sfdExport.FileName
                    SeparationCharacter = SeparationCharacter
                    MsgBox(strLanguage_DataSummaryVisualiser(2) & sfdExport.FileName, MsgBoxStyle.Information) 'The file has been successfully saved.
                End If
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

#End Region

#Region "Other Code"
    Private Sub mniSaveAs_Click(sender As System.Object, e As EventArgs) Handles mniSaveAs.Click
        Call SaveAs()
    End Sub

    Private Sub mniSave_Click(sender As System.Object, e As EventArgs) Handles mniSave.Click
        If LastSaveFile = String.Empty Then
            Call SaveAs()
        Else
            Dim Success As Boolean = Save_dgv_To_csv(LastSaveFile, dgvDataSummary, True, , , LastSeparationChar, False)
            If Success Then
                MsgBox(strLanguage_DataSummaryVisualiser(2) & LastSaveFile, MsgBoxStyle.Information) 'The file has been successfully saved.
            End If
        End If
    End Sub

    Private Sub mniExit_Click(sender As System.Object, e As EventArgs) Handles mniExit.Click
        Close()
    End Sub

    Private Sub frmDataGridView_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        Try
            lblRowsCount.Text = strLanguage_DataSummaryVisualiser(12) & dgvDataSummary.RowCount 'Rows Count:
            lblColumnsCount.Text = strLanguage_DataSummaryVisualiser(13) & dgvDataSummary.ColumnCount 'Columns Count:
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MyPrintDocument_PrintPage(sender As System.Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles MyPrintDocument.PrintPage

    End Sub

    Private Sub mniPrintPreview_Click(sender As System.Object, e As EventArgs) Handles mniPrintPreview.Click
        If SetupThePrinting() Then
            Dim MyPrintPreviewDialog As New PrintPreviewDialog
            MyPrintPreviewDialog.Document = MyPrintDocument
            MyPrintPreviewDialog.ShowIcon = False
            Dim wasTopmost As Boolean = frmMain.TopMost
            frmMain.TopMost = False
            MyPrintPreviewDialog.BringToFront()
            MyPrintPreviewDialog.ShowDialog()
            frmMain.TopMost = wasTopmost
        End If
    End Sub


    Private Sub mniPrintTool_Click(sender As System.Object, e As EventArgs) Handles mniPrintTool.Click
        If SetupThePrinting() Then
            MyPrintDocument.Print()
        End If
    End Sub

    Private Function SetupThePrinting() As Boolean
        Dim MyPrintDialog As PrintDialog = New PrintDialog
        MyPrintDialog.AllowCurrentPage = False
        MyPrintDialog.AllowPrintToFile = False
        MyPrintDialog.AllowSelection = False
        MyPrintDialog.AllowSomePages = False
        MyPrintDialog.PrintToFile = False
        MyPrintDialog.ShowHelp = False
        MyPrintDialog.ShowNetwork = False
        If (MyPrintDialog.ShowDialog <> DialogResult.OK) Then
            Return False
        End If

        MyPrintDocument.DocumentName = strLanguage_DataSummaryVisualiser(9) 'Report
        MyPrintDocument.PrinterSettings = MyPrintDialog.PrinterSettings
        MyPrintDocument.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings
        MyPrintDocument.DefaultPageSettings.Margins = New Margins(40, 40, 40, 40)

        If (MessageBox.Show(strLanguage_DataSummaryVisualiser(10), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
            MyDataGridViewPrinter = New clsDataGridViewPrinter(dgvDataSummary, MyPrintDocument, True, False, "isFalse", New Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, True)
        Else
            MyDataGridViewPrinter = New clsDataGridViewPrinter(dgvDataSummary, MyPrintDocument, False, False, "isFalse", New Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, True)
        End If
        Return True
    End Function

#End Region

End Class