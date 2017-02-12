'Version 1.2 2012/12/10
Imports System.IO
Imports System.Data.OleDb
Imports System.Threading

Module modIO
    '===============================
    '==CHANGES FOR EVERY PROGRAMME==
    '===============================
    Public Const ProgrammeExtentionValue As String = "lsm"
    Public Const ProgrammeExtentionDescriptions As String = "Saved Data of LSM Exercise"
    '/=============================/
    '/=CHANGES FOR EVERY PROGRAMME=/
    '/=============================/

    'Private Sub dsImportExcel(ByVal isExcel2007 As Boolean, ByRef strFileName As String, ByRef txtTemp As TextBox)
    '    Dim ColumnsCount As Integer = -1
    '    Dim RowsCount As Integer = 0

    '    FileStream = File.Open(strFileName, FileMode.Open, FileAccess.Read)
    '    If isExcel2007 Then
    '        ExcelFile = ExcelReaderFactory.CreateOpenXmlReader(FileStream)
    '    Else
    '        ExcelFile = ExcelReaderFactory.CreateBinaryReader(FileStream)
    '    End If
    '    ExcelFile.IsFirstRowAsColumnNames = True
    '    Dim Sheets As DataSet = ExcelFile.AsDataSet()

    '    If Not IsNothing(Sheets) OrElse Sheets.Tables(0).Rows(0).ToString <> String.Empty Then
    '        If Sheets.Tables(0).Rows.Count > 2000 Then
    '            Dim EscapeForLargeDS As MsgBoxResult = MsgBox(strLanguage_Permutation(123) & strLanguage_Permutation(124), MsgBoxStyle.YesNoCancel) 'This file appears to be far too big for a dataset.
    '            If EscapeForLargeDS = MsgBoxResult.No Or EscapeForLargeDS = MsgBoxResult.Cancel Then
    '                Return
    '            End If
    '        End If

    '        rtbOutput.Text = ""
    '        dgvData.Columns.Clear()
    '        dgvData.SelectionMode = DataGridViewSelectionMode.CellSelect

    '        For i = 0 To Sheets.Tables(0).Columns.Count - 1
    '            If Not IsNothing(Sheets.Tables(0).Columns(i)) AndAlso Sheets.Tables(0).Columns(i).ToString <> String.Empty Then
    '                dgvData.Columns.Add(Sheets.Tables(0).Columns(i).ColumnName, Sheets.Tables(0).Columns(i).ColumnName)
    '                dgvData.Columns(dgvData.Columns.Count - 1).SortMode = DataGridViewColumnSortMode.NotSortable
    '                ColumnsCount += 1
    '            Else
    '                Exit For
    '            End If
    '        Next
    '        dgvData.SelectionMode = DataGridViewSelectionMode.FullColumnSelect

    '        For j = 0 To Sheets.Tables(0).Rows.Count - 1
    '            If Not Sheets.Tables(0).Rows(0).Item(0).ToString = String.Empty Then
    '                Dim SplitMedium(ColumnsCount) As String
    '                For i = 0 To ColumnsCount
    '                    If isExcel2007 Then
    '                        SplitMedium(i) = Sheets.Tables(0).Rows(RowsCount).Item(i).ToString.Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)
    '                        If IsNumeric(SplitMedium(i)) Then
    '                            SplitMedium(i) = CStr(CDbl(SplitMedium(i)))
    '                        End If
    '                    Else
    '                        SplitMedium(i) = Sheets.Tables(0).Rows(RowsCount).Item(i).ToString.Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)
    '                    End If
    '                Next
    '                RowsCount += 1
    '                dgvData.Rows.Add(SplitMedium)
    '            Else
    '                Exit For
    '            End If
    '        Next

    '    Else
    '        MsgBox(strLanguage_Permutation(121) & strFileName & strLanguage_Permutation(122), MsgBoxStyle.Critical) 'The file is either empty or corrupted
    '    End If
    'End Sub

#Region "Misc"
    Public Sub ResizeFormWidth(ByVal frm As frmDataGridView) 'Does not need a "Handles"
        With frm
            Dim ProposedWidth As Integer
            'This is to determine if a Vertical Scroll is shown, thus taking up space (hence the +17)
            If .dgvDataGrid.Rows.GetRowsHeight(DataGridViewElementStates.None) > .Height Then
                ProposedWidth = .dgvDataGrid.Columns.GetColumnsWidth(DataGridViewElementStates.None) + 60
            Else
                ProposedWidth = .dgvDataGrid.Columns.GetColumnsWidth(DataGridViewElementStates.None) + 43
            End If

            If ProposedWidth < My.Computer.Screen.Bounds.Width Then
                .Width = ProposedWidth
            Else
                .Width = My.Computer.Screen.Bounds.Width
            End If
        End With
    End Sub

    Public Sub ResizeFormHeight(ByVal frm As frmDataGridView) 'Does not need a "Handles"
        With frm
            Try
                If Not IsDBNull(DirectCast(.dgvDataGrid.DataSource, DataTable).Rows(.dgvDataGrid.RowCount - 1).Item(0)) Then DirectCast(.dgvDataGrid.DataSource, DataTable).Rows.Add(DirectCast(.dgvDataGrid.DataSource, DataTable).NewRow)
            Catch ex As Exception
            End Try

            Dim ProposedHeight As Integer = .dgvDataGrid.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + 93

            If (ProposedHeight) <= My.Computer.Screen.Bounds.Height - 30 Then
                .Size = New Size(.Size.Width, ProposedHeight)
            Else
                .Size = New Size(.Size.Width, My.Computer.Screen.Bounds.Height - 30)
            End If

        End With
    End Sub
#End Region

    Public Function Save_dgv_To_csv(ByVal FileNamePath As String, DataGrid As DataGridView, Optional ByVal IncludeColumnNames As Boolean = False, Optional LastSaveFile As String = "", Optional ByVal ShowExceptionMessage As Boolean = True, Optional ByVal SeparationCharacter As Char = ","c, Optional ShowMessageWhenSaved As Boolean = True) As Boolean
        Try
            Dim ExportationText As String = String.Empty

            If IncludeColumnNames Then
                For i As Integer = 0 To DataGrid.ColumnCount - 1
                    ExportationText &= """" & DataGrid.Columns(i).HeaderText & """"
                    If i <> DataGrid.ColumnCount - 1 Then ExportationText &= SeparationCharacter
                Next
            End If

            For j = 0 To DataGrid.RowCount - 2
                ExportationText &= vbCrLf
                Dim CurrentLine As String = ""
                For i = 0 To DataGrid.ColumnCount - 1
                    If IsNumeric(DataGrid.Rows(j).Cells(i).Value) Then
                        CurrentLine &= """" & DataGrid.Rows(j).Cells(i).Value.ToString.Replace(",", ".") & """" & SeparationCharacter
                    Else
                        If DataGrid.Rows(j).Cells(i).Value IsNot Nothing Then
                            CurrentLine &= """" & DataGrid.Rows(j).Cells(i).Value.ToString & """" & SeparationCharacter
                        Else
                            CurrentLine &= """" & """" & SeparationCharacter
                        End If
                    End If
                Next
                ExportationText &= Mid(CurrentLine, 1, CurrentLine.Length - SeparationCharacter.ToString.Length) 'so as not to save the last comma

            Next

            WriteText(FileNamePath, ExportationText, System.Text.Encoding.UTF8)
            LastSaveFile = FileNamePath
            If ShowMessageWhenSaved Then MsgBox(strModLanguage(65) & FileNamePath, MsgBoxStyle.Information) 'The file has been successfully saved on:
            Return True

        Catch ex As Exception
            CreateCrashFile(ex, ShowExceptionMessage)
            Return False
        End Try
    End Function

    Public Function Save_Datatable_To_csv(ByVal FileNamePath As String, TheDatatable As DataTable, Optional ByVal IncludeColumnNames As Boolean = False, Optional LastSaveFile As String = "", Optional ByVal ShowExceptionMessage As Boolean = True, Optional ByVal SeparationCharacter As Char = ","c, Optional ShowMessageWhenSaved As Boolean = True) As Boolean
        Try
            Dim ExportationText As String = String.Empty

            If IncludeColumnNames Then
                For i As Integer = 0 To TheDatatable.Columns.Count - 1
                    ExportationText &= """" & TheDatatable.Columns(i).ColumnName & """"
                    If i <> TheDatatable.Columns.Count - 1 Then ExportationText &= SeparationCharacter
                Next
            End If

            For j = 0 To TheDatatable.Rows.Count - 1
                ExportationText &= vbCrLf
                Dim CurrentLine As String = ""
                For i = 0 To TheDatatable.Columns.Count - 1
                    If IsNumeric(TheDatatable.Rows(j).Item(i)) Then
                        CurrentLine &= """" & TheDatatable.Rows(j).Item(i).ToString.Replace(",", ".") & """" & SeparationCharacter
                    Else
                        If TheDatatable.Rows(j).Item(i) IsNot Nothing Then
                            CurrentLine &= """" & TheDatatable.Rows(j).Item(i).ToString & """" & SeparationCharacter
                        Else
                            CurrentLine &= """" & """" & SeparationCharacter
                        End If
                    End If
                Next
                ExportationText &= Mid(CurrentLine, 1, CurrentLine.Length - SeparationCharacter.ToString.Length) 'so as not to save the last comma

            Next

            WriteText(FileNamePath, ExportationText, System.Text.Encoding.UTF8)
            If ShowMessageWhenSaved Then MsgBox(strModLanguage(65) & FileNamePath, MsgBoxStyle.Information) 'The file has been successfully saved on:
            LastSaveFile = FileNamePath

            Return True

        Catch ex As Exception
            CreateCrashFile(ex, ShowExceptionMessage)
            Return False
        End Try
    End Function

    '===============================================================
    'This needs improvement; should not depend on "CurDBType"
    '===============================================================
    Public Function Import_To_Datatable_From_xls(ByRef TheDataTable As DataTable, ByVal ExcelPathWithName As String, ByVal UseFirstRowAsHeaders As Boolean, Optional ByVal EraseOldDataset As Boolean = True) As Boolean
        Try
            If TheDataTable Is Nothing OrElse EraseOldDataset Then TheDataTable = New DataTable
            Dim ExcelExtention = GetExt(ExcelPathWithName)
            Dim strUseFirstRowAsHeaders = "No"
            If UseFirstRowAsHeaders Then strUseFirstRowAsHeaders = "yes"

            If CurDBType = DBType.Access2007to2016 AndAlso (ExcelExtention = ".xlsx" Or ExcelExtention = ".xls") Then
                Using cn As New OleDbConnection
                    Dim Builder As New OleDbConnectionStringBuilder With {.DataSource = ExcelPathWithName, .Provider = "Microsoft.ACE.OLEDB.12.0"}

                    Builder.Add("Extended Properties", "Excel 12.0; IMEX=1;HDR=" & strUseFirstRowAsHeaders & ";")
                    cn.ConnectionString = Builder.ConnectionString

                    cn.Open()

                    Using cmd As New OleDbCommand With {.Connection = cn}
                        cmd.CommandText = "SELECT * FROM [Sheet1$]"
                        Dim dr As IDataReader = cmd.ExecuteReader

                        If EraseOldDataset OrElse TheDataTable.Columns.Count = 0 OrElse TheDataTable.Rows.Count = 0 Then
                            TheDataTable.Load(dr)

                        Else
                            Dim DatatableMedium As New DataTable
                            DatatableMedium.Load(dr)
                            Dim NewDtColumnsCount As Integer = DatatableMedium.Columns.Count
                            Dim NewDtRowsCount As Integer = DatatableMedium.Rows.Count
                            If TheDataTable.Rows.Count < NewDtRowsCount Then AddLinesToDatatable(TheDataTable, (NewDtRowsCount - TheDataTable.Rows.Count))

                            For i = 0 To NewDtColumnsCount - 1
                                TheDataTable.Columns.Add(CheckForDuplicateColumnNameOnDatatable(TheDataTable, DatatableMedium.Columns(i).ColumnName))
                                For j = 0 To NewDtRowsCount - 1
                                    TheDataTable.Rows(j).Item(TheDataTable.Columns.Count - 1) = DatatableMedium.Rows(j).Item(i)
                                Next
                            Next
                        End If
                    End Using

                    cn.Close()
                End Using
                Return True

            ElseIf CurDBType = DBType.Access97to2003 AndAlso (ExcelExtention = ".xlsx" Or ExcelExtention = ".xls") Then 'Seems like the ACE.OLEDB will work for .xls too
                Dim MyConnection As OleDbConnection
                Dim MyCommand As OleDbDataAdapter

                MyConnection = New OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & ExcelPathWithName & "';Extended Properties=Excel 8.0;HDR=" & strUseFirstRowAsHeaders) 'had a & Yes; before ")"
                MyCommand = New OleDbDataAdapter("select * from [Sheet1$]", MyConnection)
                MyCommand.TableMappings.Add("Table", My.Application.Info.Title)

                If EraseOldDataset OrElse TheDataTable.Columns.Count = 0 OrElse TheDataTable.Rows.Count = 0 Then
                    MyCommand.Fill(TheDataTable)
                Else
                    Dim DatatableMedium As New DataTable
                    MyCommand.Fill(DatatableMedium)
                    Dim NewDtColumnsCount As Integer = DatatableMedium.Columns.Count
                    Dim NewDtRowsCount As Integer = DatatableMedium.Rows.Count
                    If TheDataTable.Rows.Count < NewDtRowsCount Then AddLinesToDatatable(TheDataTable, (NewDtRowsCount - TheDataTable.Rows.Count))

                    For i = 0 To NewDtColumnsCount - 1
                        TheDataTable.Columns.Add(CheckForDuplicateColumnNameOnDatatable(TheDataTable, DatatableMedium.Columns(i).ColumnName))
                        For j = 0 To NewDtRowsCount - 1
                            TheDataTable.Rows(j).Item(TheDataTable.Columns.Count - 1) = DatatableMedium.Rows(j).Item(i)
                        Next
                    Next
                End If

                MyConnection.Close()
                Return True

            Else
                MsgBox("""" & ExcelExtention & """ " & strModLanguage(60), MsgBoxStyle.Exclamation)
                Return False

            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Import_To_dgv_From_xls(ByRef TheDataGridView As DataGridView, ByVal ExcelPathWithName As String, ByVal UseFirstRowAsHeaders As Boolean, Optional ByVal EraseOldDataset As Boolean = True) As Boolean
        Try
            TheDataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect

            Dim TheDatatable As DataTable
            If TheDataGridView.DataSource IsNot Nothing AndAlso TypeOf (TheDataGridView.DataSource) Is DataTable Then
                TheDatatable = DirectCast(TheDataGridView.DataSource, DataTable)
            Else
                TheDatatable = New DataTable
            End If


            Dim Succeeded As Boolean = Import_To_Datatable_From_xls(TheDatatable, ExcelPathWithName, UseFirstRowAsHeaders, EraseOldDataset)

            If Succeeded Then
                TheDataGridView.DataSource = TheDatatable
                For i = 0 To TheDataGridView.ColumnCount - 1
                    TheDataGridView.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next
                TheDataGridView.SelectionMode = DataGridViewSelectionMode.FullColumnSelect
                Return True
            Else
                For i = 0 To TheDataGridView.ColumnCount - 1
                    TheDataGridView.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next
                TheDataGridView.SelectionMode = DataGridViewSelectionMode.FullColumnSelect
                Return False
            End If

        Catch ex As Exception
            For i = 0 To TheDataGridView.ColumnCount - 1
                TheDataGridView.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next
            TheDataGridView.SelectionMode = DataGridViewSelectionMode.FullColumnSelect
            Return False
        End Try
    End Function

    Public Function Import_To_DataTable_From_CSV(ByVal strFileName As String, ByRef TheDatatable As DataTable, Optional ByVal SeparationCharacter As Char = ","c) As Boolean
        Try
            Dim txtTemp As New TextBox
            ReadFile(strFileName, txtTemp)                 'Save the file into the txt so we can import the data to the grid later
            txtTemp.Text = txtTemp.Text.Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)

            If txtTemp.Lines.Length > 1 Then                               'Check to see if there r actually data inside
                Dim DatasetString As List(Of String) = txtTemp.Lines.ToList
                Dim ColumnNames() As String = DatasetString.Item(0).Split(SeparationCharacter)          'Read the Headers
                For i = 0 To ColumnNames.Length - 1
                    If ColumnNames(i).Replace(" ", "").StartsWith("""") AndAlso Count("""", ColumnNames(i)) = 2 Then
                        Dim IndexBegin As Integer = ColumnNames(i).IndexOf("""")
                        Dim IndexEnd As Integer = ColumnNames(i).LastIndexOf("""")
                        ColumnNames(i) = Mid(ColumnNames(i), IndexBegin + 2, IndexEnd - 1)
                    End If
                Next
                DatasetString.RemoveAt(0)

                If DatasetString.Count > 2000 Then
                    Dim EscapeForLargeDS As MsgBoxResult = MsgBox(strModLanguage(62) & strModLanguage(63), MsgBoxStyle.YesNoCancel) 'This file appears to be far too big for a dataset.
                    If EscapeForLargeDS = MsgBoxResult.No Or EscapeForLargeDS = MsgBoxResult.Cancel Then
                        Return False
                    End If
                End If

                Dim IndexOfFirstNewColumn As Integer = TheDatatable.Columns.Count
                For i = 0 To ColumnNames.Length - 1
                    TheDatatable.Columns.Add(CheckForDuplicateColumnNameOnDatatable(TheDatatable, ColumnNames(i)))
                Next

                For j As Integer = 0 To DatasetString.Count - 1             'And now all the data inside
                    Dim tmpStr As String() = DatasetString(j).Split(SeparationCharacter)
                    Dim isAllNull As Boolean = True
                    For i = 0 To tmpStr.Length - 1
                        If tmpStr(i) <> "" Then
                            isAllNull = False
                            Exit For
                        End If
                    Next

                    If Not isAllNull Then
                        If TheDatatable.Rows.Count <= j Then TheDatatable.Rows.Add(TheDatatable.NewRow)

                        For i = 0 To ColumnNames.Length - 1

                            If tmpStr(i).Replace(" ", "").StartsWith("""") AndAlso Count("""", tmpStr(i)) = 2 Then
                                Dim IndexBegin As Integer = tmpStr(i).IndexOf("""")
                                Dim IndexEnd As Integer = tmpStr(i).LastIndexOf("""")
                                TheDatatable.Rows(j).Item(i + IndexOfFirstNewColumn) = Mid(tmpStr(i), IndexBegin + 2, IndexEnd - 1)

                            Else
                                TheDatatable.Rows(j).Item(i + IndexOfFirstNewColumn) = tmpStr(i)
                            End If
                        Next
                    End If
                Next
                TheDatatable.AcceptChanges()

            Else
                MsgBox(strModLanguage(65) & strFileName, MsgBoxStyle.Critical)   'There are no data inside the file!
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Import_To_dgv_From_CSV(ByVal strFileName As String, ByRef TheDataGridView As DataGridView, Optional ByVal SeparationCharacter As Char = ","c) As Boolean
        Try
            Dim TheDatatable As DataTable
            If TheDataGridView.DataSource IsNot Nothing AndAlso TypeOf (TheDataGridView.DataSource) Is DataTable Then
                TheDatatable = DirectCast(TheDataGridView.DataSource, DataTable)
            Else
                TheDatatable = New DataTable
            End If

            TheDataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect
            TheDataGridView.DataSource = Nothing

            If Import_To_DataTable_From_CSV(strFileName, TheDatatable, SeparationCharacter) Then
                TheDataGridView.DataSource = TheDatatable

                For i = 0 To TheDataGridView.ColumnCount - 1
                    TheDataGridView.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next
                TheDataGridView.SelectionMode = DataGridViewSelectionMode.FullColumnSelect
                Return True

            Else
                For i = 0 To TheDataGridView.ColumnCount - 1
                    TheDataGridView.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next
                TheDataGridView.SelectionMode = DataGridViewSelectionMode.FullColumnSelect
                Return False
            End If

        Catch ex As Exception
            For i = 0 To TheDataGridView.ColumnCount - 1
                TheDataGridView.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next
            TheDataGridView.SelectionMode = DataGridViewSelectionMode.FullColumnSelect
            Return False
        End Try
    End Function

    Public Function Importation_Wizard_To_DGV(ByVal FilePath As String, ByRef dgvData As DataGridView, Optional ByVal Extentions As String = "All Files|*.*|" & ProgrammeExtentionDescriptions & "|*." & ProgrammeExtentionValue & ";*.txt|Comma Separated Values|*.csv|Excel < 2007|*.xls|Excel >= 2007|*.xlsx") As Boolean
        Return Importation_Wizard_To_DGV({FilePath}, dgvData, Extentions)
    End Function
    Public Function Importation_Wizard_To_DGV(ByVal FilePaths() As String, ByRef dgvData As DataGridView, Optional ByVal Extentions As String = "All Files|*.*|" & ProgrammeExtentionDescriptions & "|*." & ProgrammeExtentionValue & ";*.txt|Comma Separated Values|*.csv|Excel < 2007|*.xls|Excel >= 2007|*.xlsx") As Boolean
        Dim ofdImport As New OpenFileDialog With {.Filter = Extentions, .DefaultExt = "csv", .Multiselect = True, .ReadOnlyChecked = True, .ShowReadOnly = True, .InitialDirectory = strDataDir}
        Dim Result As Boolean

        If FilePaths.Length = 0 OrElse FilePaths(0) = String.Empty Then
            ofdImport.FileName = Nothing
            If ofdImport.ShowDialog() = DialogResult.OK Then 'Making the user chose a file or files to import
                FilePaths = ofdImport.FileNames
            End If
        End If

        If FilePaths.Length > 0 AndAlso FilePaths(0) <> String.Empty Then       'If he pushed cancel then no filename(s) exist(s)..

            If dgvData.DataSource IsNot Nothing AndAlso DirectCast(dgvData.DataSource, DataTable).Columns.Count > 0 Then             'Checking if data already exist
                If MsgBox(strModLanguage(70) & vbCrLf & vbCrLf & strModLanguage(71), MsgBoxStyle.YesNo) = vbNo Then  'There already are data inside the dataset. Would you like to append the new colums, or erase the current data and add the new colums?
                    DirectCast(dgvData.DataSource, DataTable).Rows.Clear()
                    DirectCast(dgvData.DataSource, DataTable).Columns.Clear()
                End If
            End If

            For i As Integer = 0 To FilePaths.Length - 1                        'For each file lets do something
                Dim ext As String = GetExt(FilePaths(i)).ToLower

                If File.Exists(FilePaths(i)) Then                    'If the file hasnt been deleted or renamed in the meantime
                    If ext = ".txt" Or ext = "." & ProgrammeExtentionValue.ToLower Then
                        If Import_To_dgv_From_CSV(FilePaths(i), dgvData, ControlChars.Tab) Then Result = True

                    ElseIf ext = ".csv" Then
                        If Import_To_dgv_From_CSV(FilePaths(i), dgvData, ","c) Then Result = True

                    ElseIf ext = ".xls" OrElse ext = ".xlsx" Then
                        If Import_To_dgv_From_xls(dgvData, FilePaths(i), True, False) Then Result = True
                        'Dim isExcel2007 As Boolean = False
                        'Call dsImportExcel(False, FilePaths(i), txtTemp)

                        'ElseIf ext = ".xlsx" Then
                        'Dim isExcel2007 As Boolean = True
                        'Call dsImportExcel(True, FilePaths(i), txtTemp)

                    Else
                        MsgBox(strModLanguage(66), MsgBoxStyle.Exclamation)  'Unknown File Format
                    End If

                Else
                    MsgBox(strModLanguage(67) & FilePaths(i) & strModLanguage(68))     'The file " " does not exist
                End If
            Next

            'Else
            '    MsgBox(strModLanguage(69), MsgBoxStyle.Exclamation)                              'No file was selected!
        End If

        Return Result
    End Function

    Public Function Importation_Wizard_To_DataTable(ByVal FilePath As String, ByRef TheDataTable As DataTable, Optional ByVal Extentions As String = "All Files|*.*|" & ProgrammeExtentionDescriptions & "|*." & ProgrammeExtentionValue & ";*.txt|Comma Separated Values|*.csv|Excel < 2007|*.xls|Excel >= 2007|*.xlsx") As Boolean
        Return Importation_Wizard_To_DataTable({FilePath}, TheDataTable, Extentions)
    End Function
    Public Function Importation_Wizard_To_DataTable(ByVal FilePaths() As String, ByRef TheDataTable As DataTable, Optional ByVal Extentions As String = "All Files|*.*|" & ProgrammeExtentionDescriptions & "|*." & ProgrammeExtentionValue & ";*.txt|Comma Separated Values|*.csv|Excel < 2007|*.xls|Excel >= 2007|*.xlsx") As Boolean
        Dim ofdImport As New OpenFileDialog With {.Filter = Extentions, .DefaultExt = "csv", .Multiselect = True, .ReadOnlyChecked = True, .ShowReadOnly = True, .InitialDirectory = strDataDir}
        Dim Result As Boolean

        If FilePaths.Length = 0 OrElse FilePaths(0) = String.Empty Then
            ofdImport.FileName = Nothing
            If ofdImport.ShowDialog() = DialogResult.OK Then 'Making the user chose a file or files to import
                FilePaths = ofdImport.FileNames
            End If
        End If

        If FilePaths.Length > 0 AndAlso FilePaths(0) <> String.Empty Then       'If he pushed cancel then no filename(s) exist(s)..

            If TheDataTable IsNot Nothing AndAlso TheDataTable.Columns.Count > 0 Then             'Checking if data already exist
                If MsgBox(strModLanguage(70) & vbCrLf & vbCrLf & strModLanguage(71), MsgBoxStyle.YesNo) = vbNo Then  'There already are data inside the dataset. Would you like to append the new colums, or erase the current data and add the new colums?
                    TheDataTable.Rows.Clear()
                    TheDataTable.Columns.Clear()
                End If
            End If

            For i As Integer = 0 To FilePaths.Length - 1                        'For each file lets do something
                Dim ext As String = GetExt(FilePaths(i)).ToLower

                If File.Exists(FilePaths(i)) Then                    'If the file hasnt been deleted or renamed in the meantime
                    If ext = ".txt" Or ext = "." & ProgrammeExtentionValue.ToLower Then
                        If Import_To_DataTable_From_CSV(FilePaths(i), TheDataTable, ControlChars.Tab) Then Result = True

                    ElseIf ext = ".csv" Then
                        If Import_To_DataTable_From_CSV(FilePaths(i), TheDataTable, ","c) Then Result = True

                    ElseIf ext = ".xls" OrElse ext = ".xlsx" Then
                        If Import_To_Datatable_From_xls(TheDataTable, FilePaths(i), True, False) Then Result = True
                        'Dim isExcel2007 As Boolean = False
                        'Call dsImportExcel(False, FilePaths(i), txtTemp)

                        'ElseIf ext = ".xlsx" Then
                        'Dim isExcel2007 As Boolean = True
                        'Call dsImportExcel(True, FilePaths(i), txtTemp)

                    Else
                        MsgBox(strModLanguage(66), MsgBoxStyle.Exclamation)  'Unknown File Format
                    End If

                Else
                    MsgBox(strModLanguage(67) & FilePaths(i) & strModLanguage(68))     'The file " " does not exist
                End If
            Next

            'Else
            '    MsgBox(strModLanguage(69), MsgBoxStyle.Exclamation)                              'No file was selected!
        End If

        Return Result
    End Function

End Module
