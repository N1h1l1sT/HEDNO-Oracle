'Version 1.1 2013-03-29
Option Strict On

Imports System.IO
Imports System.Drawing.Printing

Public Class frmDataGridView

    Public strLanguage_DataGridView() As String
    Public DefaultSaveFileName As String = String.Empty
    Public LastSaveFile As String = String.Empty
    Public LastSeparationChar As Char = ControlChars.Tab

    Dim MyDataGridViewPrinter As clsDataGridViewPrinter
    Dim CurrentColumnIndex As Integer = -1

    Private Sub frmZToday_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            dgvDataGrid.AutoGenerateColumns = True
            Call DataGridView_Language(Me)

            frmSkin(Me, False)

            Dim DateString As String = Today.Day & "-" & Today.Month & "-" & Today.Year & " " & My.Computer.Clock.LocalTime.Hour & " " & My.Computer.Clock.LocalTime.Minute

            DefaultSaveFileName = strLanguage_DataGridView(9)
            LastSaveFile = strDocumentsProgDir & DefaultSaveFileName & " " & DateString & "." & sfdExport.DefaultExt

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub SaveAs()
        Try
            sfdExport.FileName = ""
            sfdExport.InitialDirectory = strDataDir
            sfdExport.DefaultExt = "txt"
            sfdExport.FileName = DefaultSaveFileName & " " & DateString & "." & sfdExport.DefaultExt  'Today's Statistics or something similar
            sfdExport.Filter = strLanguage_DataGridView(4) & ProgrammeExtentionDescriptions & "|*." & ProgrammeExtentionValue & "|Comma Separated Values|*.csv|Text File|*.txt|All Files|*.*"  'Comma-Separated Values Files
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

                Dim Success As Boolean = Save_dgv_To_csv(sfdExport.FileName, dgvDataGrid, True, , , SeparationCharacter, False)
                If Success Then
                    LastSaveFile = sfdExport.FileName
                    SeparationCharacter = SeparationCharacter
                    MsgBox(strLanguage_DataGridView(2) & sfdExport.FileName, MsgBoxStyle.Information) 'The file has been successfully saved.
                End If
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub txtColumnRen_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tstxtColumnRen.KeyUp
        If e.KeyCode = Keys.Enter Then
            If tstxtColumnRen.Text <> "" AndAlso CurrentColumnIndex >= 0 Then
                dgvDataGrid.Columns(CurrentColumnIndex).HeaderText = tstxtColumnRen.Text
                dgvDataGrid.Columns(CurrentColumnIndex).Name = tstxtColumnRen.Text
                CurrentColumnIndex = -1
                tstxtColumnRen.Text = ""
                cmsColumnRen.Close()
                dgvDataGrid.ContextMenuStrip = Nothing
            End If
        End If
    End Sub

    Private Sub dgvDataGrid_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvDataGrid.ColumnHeaderMouseClick
        If e.Button = MouseButtons.Right Then
            CurrentColumnIndex = e.ColumnIndex
            cmsColumnRen.Show(Cursor.Position)
            tstxtColumnRen.Focus()

        ElseIf e.Button = MouseButtons.Left Then
            dgvDataGrid.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect
            If dgvDataGrid.SelectedColumns.Count = 0 Then dgvDataGrid.Columns(e.ColumnIndex).Selected = True
        End If
    End Sub

    Private Sub dgvDataGrid_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvDataGrid.RowHeaderMouseClick
        If e.Button = MouseButtons.Left Then
            dgvDataGrid.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
            If dgvDataGrid.SelectedRows.Count = 0 Then dgvDataGrid.Rows(e.RowIndex).Selected = True
        End If
    End Sub

    Private Sub dgvDataGrid_DataSourceChanged(sender As Object, e As System.EventArgs) Handles dgvDataGrid.DataSourceChanged
        Try
            Call DataGridView_Language(Me)
            If dgvDataGrid.ColumnCount > 0 Then lblRowsCount.Text = strLanguage_DataGridView(12) & dgvDataGrid.RowCount 'Rows Count: 
            lblSelectedColumns.Text = strLanguage_DataGridView(13) & dgvDataGrid.ColumnCount 'Columns Count: 
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgvDataGrid_SelectionChanged(sender As Object, e As System.EventArgs) Handles dgvDataGrid.SelectionChanged
        If Not IsNothing(strLanguage_DataGridView) Then
            If dgvDataGrid.SelectedCells.Count > 0 Then

                lblCurRow.Text = strLanguage_DataGridView(14) & dgvDataGrid.SelectedCells(dgvDataGrid.SelectedCells.Count - 1).RowIndex.ToString 'Current Row: 
                lblCurColumn.Text = strLanguage_DataGridView(15) & dgvDataGrid.SelectedCells(dgvDataGrid.SelectedCells.Count - 1).ColumnIndex.ToString 'Current Column: 

                Dim MoneyOfssLbl As Double = 0
                Dim Swarm As Integer = 0

                For i = 0 To dgvDataGrid.SelectedCells.Count - 1
                    If Not IsNothing(dgvDataGrid.SelectedCells(i).Value) AndAlso Not IsDBNull(dgvDataGrid.SelectedCells(i).Value) AndAlso IsNumeric(CStr(dgvDataGrid.SelectedCells(i).Value)) Then
                        MoneyOfssLbl += CDbl(CStr(dgvDataGrid.SelectedCells(i).Value))
                        Swarm += 1
                    End If
                Next

                lblSum.Text = String.Format("{0}{1:n2}", strLanguage_DataGridView(5), MoneyOfssLbl) 'Sum: 
                lblAverage.Text = String.Format("{0}{1:n2}", strLanguage_DataGridView(3), MoneyOfssLbl / Swarm) ' Average:
                lblSelectedCells.Text = strLanguage_DataGridView(6) & dgvDataGrid.SelectedCells.Count 'Selected Cells: 
            End If

            lblSelectedRows.Text = strLanguage_DataGridView(7) & dgvDataGrid.SelectedRows.Count 'Selected Rows: 
            lblSelectedColumns.Text = strLanguage_DataGridView(8) & dgvDataGrid.SelectedColumns.Count 'Selected Columns: 
        End If

    End Sub

    Private Sub dgvDataGrid_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDataGrid.CellContentClick
        If dgvDataGrid.SelectedCells.Count > 0 Then
            lblCurRow.Text = strLanguage_DataGridView(14) & dgvDataGrid.SelectedCells(dgvDataGrid.SelectedCells.Count - 1).RowIndex.ToString
            lblCurColumn.Text = strLanguage_DataGridView(15) & dgvDataGrid.SelectedCells(dgvDataGrid.SelectedCells.Count - 1).ColumnIndex.ToString
        End If
    End Sub

#Region "Other Code"
    Private Sub mniSaveAs_Click(sender As System.Object, e As System.EventArgs) Handles mniSaveAs.Click
        Call SaveAs()
    End Sub

    Private Sub mniSave_Click(sender As System.Object, e As System.EventArgs) Handles mniSave.Click
        If LastSaveFile = String.Empty Then
            Call SaveAs()
        Else
            Dim Success As Boolean = Save_dgv_To_csv(LastSaveFile, dgvDataGrid, True, , , LastSeparationChar, False)
            If Success Then
                MsgBox(strLanguage_DataGridView(2) & LastSaveFile, MsgBoxStyle.Information) 'The file has been successfully saved.
            End If
        End If
    End Sub

    Private Sub mniExit_Click(sender As System.Object, e As System.EventArgs) Handles mniExit.Click
        Close()
    End Sub

    Private Sub frmZStatisticsSheet_VisibleChanged(sender As Object, e As System.EventArgs) Handles Me.VisibleChanged
        Try
            lblRowsCount.Text = strLanguage_DataGridView(12) & dgvDataGrid.RowCount 'Rows Count: 
            lblColumnsCount.Text = strLanguage_DataGridView(13) & dgvDataGrid.ColumnCount 'Columns Count: 
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MyPrintDocument_PrintPage(sender As System.Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles MyPrintDocument.PrintPage
        Dim more As Boolean = MyDataGridViewPrinter.DrawDataGridView(e.Graphics)
        If (more = True) Then
            e.HasMorePages = True
        End If
    End Sub

    Private Sub mniPrintPreview_Click(sender As System.Object, e As System.EventArgs) Handles mniPrintPreview.Click
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


    Private Sub mniPrintTool_Click(sender As System.Object, e As System.EventArgs) Handles mniPrintTool.Click
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

        MyPrintDocument.DocumentName = strLanguage_DataGridView(9) 'Report
        MyPrintDocument.PrinterSettings = MyPrintDialog.PrinterSettings
        MyPrintDocument.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings
        MyPrintDocument.DefaultPageSettings.Margins = New Margins(40, 40, 40, 40)

        If (MessageBox.Show(strLanguage_DataGridView(10), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
            MyDataGridViewPrinter = New clsDataGridViewPrinter(dgvDataGrid, MyPrintDocument, True, False, "isFalse", New Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, True)
        Else
            MyDataGridViewPrinter = New clsDataGridViewPrinter(dgvDataGrid, MyPrintDocument, False, False, "isFalse", New Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, True)
        End If
        Return True
    End Function

#End Region

End Class