Imports System.Drawing.Color
Imports System.IO
Imports RDotNet

Public Class frmClassification
    Public strLanguage_Classification As String()
    Public strLanguage_Classification_Tips As String()
    Private XDFFileExists As Boolean = False
    Private isStatisticsXDF As Boolean = True

    Dim TrainPercentages As New List(Of String)

    '!Put on frm_Load
    'pbLoading.Location = New Point(0, CInt(pbLoading.Parent.Height / 2) + 15)
    'pbLoading.Width = pbLoading.Parent.Width
    'fswModelExists.Path = doProperPathName(strXDF)
    'fswModelExists.Filter = "Classification_DS.xdf"
    'Call CheckXDFFileExists()
    '
    '!Put on chkOptions_CheckedChanged
    'Call ColourChkStatisticsMode()
#Region "XDFFileExists"

    Private Sub CheckXDFFileExists()
        If File.Exists(doProperFileName(strXDF & "Classification_DS.xdf")) Then
            XDFFileExists = True
            chkUseExistingXDFFile.BackColor = LightGreen

            pbLoading.MarqueeAnimationSpeed = 10
            pbLoading.Visible = True
            lblLoading.Visible = True
            lblLoading.Dock = DockStyle.Fill
            tmrModelExists.Enabled = True

        Else
            XDFFileExists = False
            chkUseExistingXDFFile.BackColor = IndianRed
            chkStatisticsMode.BackColor = SystemColors.Control
        End If
    End Sub

    Private Sub ColourChkStatisticsMode()
        If XDFFileExists AndAlso chkUseExistingXDFFile.Checked Then
            If chkStatisticsMode.Checked Then
                If isStatisticsXDF Then chkStatisticsMode.BackColor = LightGreen Else chkStatisticsMode.BackColor = IndianRed
            Else
                If isStatisticsXDF Then chkStatisticsMode.BackColor = IndianRed Else chkStatisticsMode.BackColor = LightGreen
            End If

        Else
            chkStatisticsMode.BackColor = SystemColors.Control
        End If
    End Sub

    Private Sub tmrModelExists_Tick(sender As Object, e As EventArgs) Handles tmrModelExists.Tick
        tmrModelExists.Enabled = False

        Try 'Non-Essential Functions
            If XDFFileExists Then
                If RDotNet_Initialization() Then
                    RSource(strFunctions & "[ColumnNames_For_FormLoad].R",, {"{0}", "Classification_DS"})
                    isStatisticsXDF = Rdo.GetSymbol("isStatisticsXDF").AsLogical.First

                    Call ColourChkStatisticsMode()

                End If
            End If
        Catch ex As Exception
            Notify(ex.ToString, Red, Black, 10,,, True)
        End Try

        pbLoading.MarqueeAnimationSpeed = 0
        pbLoading.Visible = False
        lblLoading.Dock = DockStyle.None
        lblLoading.Visible = False

    End Sub

    'Private Sub chkStatisticsMode_CheckedChanged(sender As Object, e As EventArgs) Handles chkStatisticsMode.CheckedChanged
    '    Call ColourChkStatisticsMode()
    'End Sub

    Private Sub fswModelExists_Created_Created_Renamed(sender As Object, e As FileSystemEventArgs) Handles fswModelExists.Created, fswModelExists.Deleted, fswModelExists.Renamed
        Call CheckXDFFileExists()
    End Sub


#End Region

    Private Sub frmClassification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'initialization
            Call Classification_Language(Me)
            frmSkin(Me, False)

            cbTrainPercent.DataSource = Nothing
            For i As Integer = 1 To 10
                TrainPercentages.Add((i * 10).ToString & "%")
            Next
            cbTrainPercent.DataSource = TrainPercentages
            cbTrainPercent.SelectedIndex = 7 '80%
            '/initialization

            pbLoading.Location = New Point(0, CInt(pbLoading.Parent.Height / 2) + 15)
            pbLoading.Width = pbLoading.Parent.Width
            fswModelExists.Path = doProperPathName(strXDF)
            fswModelExists.Filter = "Classification_DS.xdf"
            Call CheckXDFFileExists()

            If File.Exists(doProperPathName(strXDF) & "Clustering_DS.xdf") Then
                btnClassification.Enabled = True
            Else
                If File.Exists(doProperPathName(strXDF) & "Classification_DS.xdf") Then
                    'The Clustering Dataset file '{1}' does not exist or is not reachable, however the Classification one '{2}' is found, hence the button '{3}' will remain locked on checked.{0}{0}If you wish to uncheck it so that the Classification file is created anew, please do the Clustering first.
                    Notify(sa(strLanguage_Classification(7), vbCrLf, doProperPathName(strXDF) & "Clustering_DS.xdf", doProperPathName(strXDF) & "Classification_DS.xdf", RemCtrHotLetter(chkUseExistingXDFFile)),
                           Red, Black, 30)
                    chkUseExistingXDFFile.Checked = True
                    chkUseExistingXDFFile.Enabled = False
                    btnClassification.Enabled = True

                Else
                    'For the classification process to commence, clustering must have already occurred.{0}Unfortunately the file '{1}' cannot be reached.{0}The Clustering form will now open for you to perform clustering with the CleanXDF option disabled so that the file needed remains.
                    MsgBox(sa(strLanguage_Classification(24), vbCrLf, doProperPathName(strXDF) & "Clustering_DS.xdf"))
                    Dim ClusteringForm As New frmClusteringStep1
                    ClusteringForm.chkCleanXDFFile.Checked = False
                    ClusteringForm.chkCleanXDFFile.Enabled = False
                    ClusteringForm.Show()
                    Close()
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Shadows Sub FormClosing(ByVal sender As Object, ByVal e As ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If FuncInProgress.Count <> 0 Then
            e.Cancel = True
            MsgBox(sa(strLanguage_Classification(13), ArrayBox(False, ";", 0, True, FuncInProgress)), MsgBoxStyle.Exclamation) 'Please wait for: {0} to finish
        End If
    End Sub

    Private Sub btnClassification_Click(sender As Object, e As EventArgs) Handles btnClassification.Click
        Try
            If FuncInProgress.Count = 0 Then
                FuncInProgress.Add(strLanguage_Classification(11)) 'Forming Training and Test Sets
                fswModelExists.EnableRaisingEvents = False
                pnlMain.Enabled = False
                Try

                    Dim ClassificationColumnNames() As String = {}
                    Dim TrainColumnNames() As String = {}
                    Dim TestColumnNames() As String = {}

                    If RDotNet_Initialization() Then
                        If chkUseExistingXDFFile.Checked Then Rdo.Evaluate("UseExistingXDFFile <- TRUE") Else Rdo.Evaluate("UseExistingXDFFile <- FALSE")
                        If chkCleanXDFFile.Checked Then Rdo.Evaluate("CleanupXFDFile <- TRUE") Else Rdo.Evaluate("CleanupXFDFile <- FALSE")
                        If chkVisualiseClassImbal.Checked Then Rdo.Evaluate("VisualiseClassImbalance <- TRUE") Else Rdo.Evaluate("VisualiseClassImbalance <- FALSE")
                        If chkShowDataSummary.Checked Then Rdo.Evaluate("ShowDataSummary <- TRUE") Else Rdo.Evaluate("ShowDataSummary <- FALSE")
                        If chkShowVariableInfo.Checked Then Rdo.Evaluate("ShowVariableInfo <- TRUE") Else Rdo.Evaluate("ShowVariableInfo <- FALSE")
                        If chkFormTrainSet.Checked Then Rdo.Evaluate("FormTrainingSet <- TRUE") Else Rdo.Evaluate("FormTrainingSet <- FALSE")
                        If chkFormTestSet.Checked Then Rdo.Evaluate("FormTestingSet <- TRUE") Else Rdo.Evaluate("FormTestingSet <- FALSE")
                        If chkStatisticsMode.Checked Then Rdo.Evaluate("StatisticsMode <- TRUE") Else Rdo.Evaluate("StatisticsMode <- FALSE")

                        If chkShowTrainDataSummary.Checked Then Rdo.Evaluate("ShowTrainDataSummary <- TRUE") Else Rdo.Evaluate("ShowTrainDataSummary <- FALSE")
                        If chkShowTrainVarInfo.Checked Then Rdo.Evaluate("ShowTrainVarInfo <- TRUE") Else Rdo.Evaluate("ShowTrainVarInfo <- FALSE")

                        If chkShowTestDataSummary.Checked Then Rdo.Evaluate("ShowTestDataSummary <- TRUE") Else Rdo.Evaluate("ShowTestDataSummary <- FALSE")
                        If chkShowTestVarInfo.Checked Then Rdo.Evaluate("ShowTestVarInfo <- TRUE") Else Rdo.Evaluate("ShowTestVarInfo <- FALSE")


                        If RSource({strFunctions & "[ColumnsInfo].R",
                                    strFunctions & "3.0 Classification - Form Train and Test Sets.R"}, , {"{0}", If(chkStatisticsMode.Checked, TablevErga, TablevFinalDataset),
                                                                                                          "{1}", ColvGeoLocX,
                                                                                                          "{2}", ColvGeoLocY,
                                                                                                          "{3}", If(chkStatisticsMode.Checked, "SelectionRatio = as.integer(runif(.rxNumRows,1,11)),", ""),
                                                                                                          "{4}", (cbTrainPercent.SelectedIndex + 1).ToString}, True) Then

                            If chkShowDataSummary.Checked OrElse chkShowVariableInfo.Checked Then
                                ClassificationColumnNames = Rdo.GetSymbol("ClassificationColumnNames").AsCharacter.ToArray

                                If chkShowDataSummary.Checked Then
                                    Dim DataSummary As DataFrame = Rdo.GetSymbol("ClassificationDataSummary").AsDataFrame
                                    Dim DataSummaryVisualiserForm As New frmDataSummaryVisualiser With {.dfDataSummary = DataSummary, .DatasetName = "Classification"}
                                    DataSummaryVisualiserForm.Show()
                                End If

                                If chkShowVariableInfo.Checked Then
                                    Dim VariableInfo = Rdo.GetSymbol("ClassificationVarInfo").AsList
                                    Dim VariableInfoVisualiserForm As New frmVariableInfoVisualiser With {.rlstVariableInfo = VariableInfo, .ColumnNames = ClassificationColumnNames, .DatasetName = "Classification"}
                                    VariableInfoVisualiserForm.Show()
                                End If
                            End If

                            If chkFormTrainSet.Checked OrElse File.Exists(doProperPathName(strXDF) & "Training_DS.xdf") Then
                                If chkShowTrainDataSummary.Checked OrElse chkShowTrainVarInfo.Checked Then
                                    TrainColumnNames = Rdo.GetSymbol("TrainColumnNames").AsCharacter.ToArray

                                    If chkShowTrainDataSummary.Checked Then
                                        Dim DataSummary As DataFrame = Rdo.GetSymbol("TrainDataSummary").AsDataFrame
                                        Dim DataSummaryVisualiserForm As New frmDataSummaryVisualiser With {.dfDataSummary = DataSummary, .DatasetName = "Train"}
                                        DataSummaryVisualiserForm.Show()
                                    End If

                                    If chkShowTrainVarInfo.Checked Then
                                        Dim VariableInfo = Rdo.GetSymbol("TrainVarInfo").AsList
                                        Dim VariableInfoVisualiserForm As New frmVariableInfoVisualiser With {.rlstVariableInfo = VariableInfo, .ColumnNames = TrainColumnNames, .DatasetName = "Train"}
                                        VariableInfoVisualiserForm.Show()
                                    End If
                                End If
                            End If

                            If chkFormTestSet.Checked OrElse File.Exists(doProperPathName(strXDF) & "Test_DS.xdf") Then
                                If chkShowTestDataSummary.Checked OrElse chkShowTestVarInfo.Checked Then
                                    TestColumnNames = Rdo.GetSymbol("TestColumnNames").AsCharacter.ToArray

                                    If chkShowTestDataSummary.Checked Then
                                        Dim DataSummary As DataFrame = Rdo.GetSymbol("TestDataSummary").AsDataFrame
                                        Dim DataSummaryVisualiserForm As New frmDataSummaryVisualiser With {.dfDataSummary = DataSummary, .DatasetName = "Test"}
                                        DataSummaryVisualiserForm.Show()
                                    End If

                                    If chkShowTestVarInfo.Checked Then
                                        Dim VariableInfo = Rdo.GetSymbol("TestVarInfo").AsList
                                        Dim VariableInfoVisualiserForm As New frmVariableInfoVisualiser With {.rlstVariableInfo = VariableInfo, .ColumnNames = TestColumnNames, .DatasetName = "Test"}
                                        VariableInfoVisualiserForm.Show()
                                    End If
                                End If
                            End If

                            Dim XDFCreatedOutOfNecessity As Boolean = Rdo.GetSymbol("XDFCreatedOutOfNecessity").AsLogical.First
                            '                                          The option '{0}' was checked but the file was unreachable and was created instead.
                            If XDFCreatedOutOfNecessity Then MsgBox(sa(strLanguage_Classification(15), RemCtrHotLetter(chkUseExistingXDFFile)))

                        End If
                    End If
                Catch ex As Exception
                    Notify(ex.ToString, Red, Black, 10)
                    'CreateCrashFile(ex) ''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    pnlMain.Enabled = True
                End Try

                fswModelExists.EnableRaisingEvents = True
                FuncInProgress.Remove(strLanguage_Classification(11)) 'Forming Training and Test Sets
                pnlMain.Enabled = True
                Close()
            Else
                MsgBox(sa(strLanguage_Classification(13), ArrayBox(False, ";", 0, True, FuncInProgress)), MsgBoxStyle.Exclamation) 'Please wait for: {0} to finish
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub chkOptions_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowDataSummary.CheckedChanged, chkVisualiseClassImbal.CheckedChanged,
                                                                                    chkShowVariableInfo.CheckedChanged, chkUseExistingXDFFile.CheckedChanged,
                                                                                    chkFormTrainSet.CheckedChanged, chkFormTestSet.CheckedChanged
        If chkShowDataSummary.Checked And chkVisualiseClassImbal.Checked And chkShowVariableInfo.Checked And chkUseExistingXDFFile.Checked And
            chkFormTrainSet.Checked And chkFormTestSet.Checked Then
            btnSelectAll.Text = strLanguage_Classification(12) 'Unselect &All
        Else
            btnSelectAll.Text = strLanguage_Classification(8) 'Select &All
        End If

        Call ColourChkStatisticsMode()
    End Sub

    Private Sub lblTrainPercent_TextChanged(sender As Object, e As EventArgs) Handles lblTrainPercent.TextChanged
        cbTrainPercent.Location = New Point(lblTrainPercent.Location.X + lblTrainPercent.Width + 5, cbTrainPercent.Location.Y)
        cbTrainPercent.Width = cbTrainPercent.Parent.Width - cbTrainPercent.Location.X
    End Sub

    Private Sub chkStatisticsMode_CheckedChanged(sender As Object, e As EventArgs) Handles chkStatisticsMode.CheckedChanged
        If chkStatisticsMode.Checked Then
            gbStatistics.Enabled = True
        Else
            gbStatistics.Enabled = False
        End If

        Call ColourChkStatisticsMode()
    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        If chkShowDataSummary.Checked And chkVisualiseClassImbal.Checked And chkShowVariableInfo.Checked And chkUseExistingXDFFile.Checked Then
            If chkUseExistingXDFFile.Enabled Then chkUseExistingXDFFile.Checked = False
            If chkShowDataSummary.Enabled Then chkShowDataSummary.Checked = False
            If chkVisualiseClassImbal.Enabled Then chkVisualiseClassImbal.Checked = False
            If chkShowVariableInfo.Enabled Then chkShowVariableInfo.Checked = False
            If chkFormTrainSet.Enabled Then chkFormTrainSet.Checked = False
            If chkFormTestSet.Enabled Then chkFormTestSet.Checked = False
        Else
            If chkUseExistingXDFFile.Enabled Then chkUseExistingXDFFile.Checked = True
            If chkShowDataSummary.Enabled Then chkShowDataSummary.Checked = True
            If chkVisualiseClassImbal.Enabled Then chkVisualiseClassImbal.Checked = True
            If chkShowVariableInfo.Enabled Then chkShowVariableInfo.Checked = True
            If chkFormTrainSet.Enabled Then chkFormTrainSet.Checked = True
            If chkFormTestSet.Enabled Then chkFormTestSet.Checked = True
        End If
    End Sub

    Private Sub scMain_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles scMain.SplitterMoved
        chkStatisticsMode.Location = New Point(scMain.SplitterDistance + 16, chkStatisticsMode.Location.Y)
        cbTrainPercent.Location = New Point(lblTrainPercent.Location.X + lblTrainPercent.Width + 6, cbTrainPercent.Location.Y)
        Dim cbWidth As Integer = 50
        Try
            cbWidth = cbTrainPercent.Parent.Width - cbTrainPercent.Location.X - 3
            cbTrainPercent.Width = cbWidth
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lblTrainPercent_SizeChanged(sender As Object, e As EventArgs) Handles lblTrainPercent.SizeChanged
        chkStatisticsMode.Location = New Point(scMain.SplitterDistance + 16, chkStatisticsMode.Location.Y)
        cbTrainPercent.Location = New Point(lblTrainPercent.Location.X + lblTrainPercent.Width + 6, cbTrainPercent.Location.Y)
        Dim cbWidth As Integer = 50
        Try
            cbWidth = cbTrainPercent.Parent.Width - cbTrainPercent.Location.X - 3
            cbTrainPercent.Width = cbWidth
        Catch ex As Exception
        End Try
    End Sub
End Class