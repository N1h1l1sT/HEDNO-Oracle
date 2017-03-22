Imports System.IO
Imports RDotNet
Imports System.Drawing.Color
Imports System.Math
Imports System.Text

Public Class frmRandomForest
    Public strLanguage_RandomForest As String()
    Public strLanguage_RandomForest_Tips As String()
    Private isInitialising As Boolean = True
    Private XDFFileExists As Boolean = False
    Private isStatisticsXDF As Boolean = True

    Private ModelExists As Boolean = False

    Private Shared ModelSavePath As String = strModelsPath
    Private Shared CurRoundAt As Integer = RoundAt
    Private TrainingColumnNames() As String = {}
    Private Test_DSColumnNames() As String = {}

    Private isWorking As Boolean = False
    Private StopWorking As Boolean = False

    '!Put on frm_Load
    'pbLoading.Location = New Point(0, CInt(pbLoading.Parent.Height / 2) + 15)
    'pbLoading.Width = pbLoading.Parent.Width
    'fswXDFFileExists.Path = strXDF
    'fswXDFFileExists.Filter = "Test_DS.xdf"
    'Call CheckXDFFileExists()
    '
    '!Put on chkOptions_CheckedChanged
    'Call ColourChkStatisticsMode()

#Region "XDFFileExists"

    Private Sub CheckXDFFileExists()
        If File.Exists(doProperFileName(strXDF & "Test_DS.xdf")) Then
            XDFFileExists = True

            pbLoading.MarqueeAnimationSpeed = 10
            pbLoading.Visible = True
            lblLoading.Visible = True
            lblLoading.Dock = DockStyle.Fill
            tmrXDFExists.Enabled = True

        Else
            XDFFileExists = False
            chkStatisticsMode.BackColor = SystemColors.Control
        End If
    End Sub

    Private Sub ColourChkStatisticsMode()
        If XDFFileExists Then
            If chkStatisticsMode.Checked Then
                If isStatisticsXDF Then chkStatisticsMode.BackColor = LightGreen Else chkStatisticsMode.BackColor = IndianRed
            Else
                If isStatisticsXDF Then chkStatisticsMode.BackColor = IndianRed Else chkStatisticsMode.BackColor = LightGreen
            End If

        Else
            chkStatisticsMode.BackColor = SystemColors.Control
        End If
    End Sub

    Private Sub tmrXDFExists_Tick(sender As Object, e As EventArgs) Handles tmrXDFExists.Tick
        tmrXDFExists.Enabled = False

        Try 'Non-Essential Functions
            If XDFFileExists Then
                If RDotNet_Initialization() Then
                    RSource(strFunctions & "[ColumnNames_For_FormLoad].R",, {"{0}", "Test_DS"})
                    isStatisticsXDF = Rdo.GetSymbol("isStatisticsXDF").AsLogical.First
                    Test_DSColumnNames = Rdo.GetSymbol("Test_DSColumnNames").AsCharacter.ToArray

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

        'If we're here then both Training.XDF AND Test.XDF do exist, ergo the R function has to be able to provide the column names
        tmrLoadColumns.Enabled = True

    End Sub

    'Private Sub chkStatisticsMode_CheckedChanged(sender As Object, e As EventArgs) Handles chkStatisticsMode.CheckedChanged
    '    Call ColourChkStatisticsMode()
    'End Sub

    Private Sub fswXDFFileExists_Created_Created_Renamed(sender As Object, e As FileSystemEventArgs) Handles fswXDFFileExists.Created, fswXDFFileExists.Deleted, fswXDFFileExists.Renamed
        Call CheckXDFFileExists()
    End Sub


#End Region

#Region "Functions"
    Private Sub UpdateCombinations()
        If Not isInitialising Then
            If CInt(txtNGrams.Text) > clbColumns.CheckedIndices.Count Then
                If clbColumns.CheckedIndices.Count >= 1 Then
                    txtNGrams.Text = clbColumns.CheckedIndices.Count.ToString
                Else
                    txtNGrams.Text = "1"
                End If
            End If

            If clbColumns.CheckedIndices.Count <= 1 Or Not chkColumnsCombinations.Checked Then
                lblCombinationsCount.Text = "1" & strLanguage_RandomForest(19) ' Combination
            Else
                If chkUpToNGramsN.Checked Then
                    Dim CombSum As Double = 0
                    For i = 1 To CInt(txtNGrams.Text)
                        CombSum += NChooseK(clbColumns.CheckedIndices.Count, i)
                    Next
                    lblCombinationsCount.Text = (CombSum).ToString("n0") & strLanguage_RandomForest(20) ' Combinations
                Else
                    lblCombinationsCount.Text = (NChooseK(clbColumns.CheckedIndices.Count, CInt(txtNGrams.Text))).ToString("n0") & strLanguage_RandomForest(20) ' Combinations
                End If
            End If
        End If
    End Sub

    Public Function getDependentVars(ByVal Variables As List(Of String)) As String
        Dim Result As String = String.Empty

        For Each Var In Variables
            Result &= Var & " + "
        Next
        If Result <> String.Empty Then Result = AntiSubString(Result, " + ".Length)

        Return Result
    End Function

    Public Function getCombOfDpdntVars(ByVal variables() As String, CombinationsUpToNumCount As Boolean) As List(Of String)
        Dim lstResult As New List(Of String)

        If CombinationsUpToNumCount Then

        Else

        End If


        Return lstResult
    End Function

    Private Sub CheckModelExists()
        If File.Exists(doProperFileName(strModelsPath & "RandomForestModel.RDS")) Then
            ModelExists = True
            chkUseExistingModel.BackColor = LightGreen
        Else
            ModelExists = False
            chkUseExistingModel.BackColor = IndianRed
        End If
    End Sub


#End Region

    Private Shadows Sub FormClosing(ByVal sender As Object, ByVal e As ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            If FuncInProgress.Count <> 0 Then
                'Procedure '{1}' is in process and closing the form now might have unexpected results as the rsession is independent and could continue working in the background even after the form and all its code is over.{0}Are you sure you wish to close this form?
                If MsgBox(sa(strLanguage_RandomForest(30), vbCrLf, ArrayBox(False, ";", 0, True, FuncInProgress)), MsgBoxStyle.YesNoCancel) <> MsgBoxResult.Yes Then
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmRandomForest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'initialization
            Call RandomForest_Language(Me)
            frmSkin(Me, False)

            cbPlotVarImportance.Items.Clear()
            cbPlotVarImportance.Items.Add("False")
            cbPlotVarImportance.Items.Add("True")
            cbPlotVarImportance.SelectedItem = cbPlotVarImportance.Items(1)

            cbClassMethod.Items.Clear()
            cbClassMethod.Items.Add("class")
            cbClassMethod.Items.Add("anova")
            cbClassMethod.SelectedItem = cbClassMethod.Items(1)

            cbShowOOBEPlot.Items.Clear()
            cbShowOOBEPlot.Items.Add("False")
            cbShowOOBEPlot.Items.Add("True")
            cbShowOOBEPlot.SelectedItem = cbShowOOBEPlot.Items(1)
            '/initialization

            lblColumnsLoading.Visible = True
            pbColumnsLoading.Visible = True
            pbColumnsLoading.MarqueeAnimationSpeed = 1

            txtSavePath.Text = ModelSavePath
            txtRoundAt.Text = CurRoundAt.ToString

            If Not File.Exists(strXDF & "Training_DS.xdf") OrElse Not File.Exists(strXDF & "Test_DS.xdf") Then
                If File.Exists(strXDF & "Classification_DS.xdf") OrElse File.Exists(strXDF & "Clustering_DS.xdf") Then
                    Dim ClassificationForm As New frmClassification
                    ClassificationForm.chkFormTrainSet.Checked = True
                    ClassificationForm.chkFormTrainSet.Enabled = False
                    ClassificationForm.chkFormTestSet.Checked = True
                    ClassificationForm.chkFormTestSet.Enabled = False

                    If File.Exists(strXDF & "Classification_DS.xdf") Then
                        If File.Exists(strXDF & "Training_DS.xdf") OrElse File.Exists(strXDF & "Test_DS.xdf") Then
                            'In order to apply the Machine Learning Algorithm, the Training & Testing sets need to be on '{1}', however, only one of the 2 is reachable.{0}As the Classification Dataset is found, the Classification Form will now open with needed options locked on for you, to create them anew
                            MsgBox(sa(strLanguage_RandomForest(33), vbCrLf, strXDF))
                        Else
                            'In order to apply the Machine Learning Algorithm, the Training & Testing sets need to be on '{1}'; none of which is reachable.{0}However, the Classification Dataset is found and hence the Classification Form will now open with needed options locked on for you to create them
                            MsgBox(sa(strLanguage_RandomForest(34), vbCrLf, strXDF))
                        End If

                    ElseIf File.Exists(strXDF & "Clustering_DS.xdf") Then
                        'In order to apply the Machine Learning Algorithm, the Training & Testing sets need to be on '{1}'{0}Because neither the Training & Test, nor the Classification Datasets appear to exist, the Classification Form will now open with needed options locked on for you to create them.
                        MsgBox(sa(strLanguage_RandomForest(35), vbCrLf, strXDF))
                    End If

                    ClassificationForm.Show()
                    Close()
                    Exit Sub

                Else
                    'In order to apply the Machine Learning Algorithm, the Training & Testing sets need to be on '{1}'{0}Because neither the Training & Test, nor the Clustering & Classification Datasets appear to exist, the Clustering Form will now open with needed options locked on for you to create the Clustering Dataset.{0}Please proceed to creating the rest using the Menu '{4}→{3}' afterwards
                    MsgBox(sa(strLanguage_RandomForest(36), vbCrLf, strXDF, RemMniHotLetter(frmMain.mniClustering), RemMniHotLetter(frmMain.mniFormTrainAndTestSets), RemMniHotLetter(frmMain.mniClassification)))
                    Dim ClusteringForm As New frmClusteringStep1
                    ClusteringForm.chkCleanXDFFile.Checked = False
                    ClusteringForm.chkCleanXDFFile.Enabled = False
                    ClusteringForm.Show()
                    Close()
                    Exit Sub
                End If
            End If

            pbLoading.Location = New Point(0, CInt(pbLoading.Parent.Height / 2) + 15)
            pbLoading.Width = pbLoading.Parent.Width
            fswXDFFileExists.Path = doProperPathName(strXDF)
            fswXDFFileExists.Filter = "Test_DS.xdf"
            Call CheckXDFFileExists()

            fswModelFileExists.Path = doProperPathName(strModelsPath)
            fswModelFileExists.Filter = "RandomForestModel.RDS"
            Call CheckModelExists()

            fswTrainAndTest.Path = doProperPathName(strXDF)
            fswModelFileExists.Filter = "*.xdf"

            isInitialising = False

        Catch ex As Exception
            CreateCrashFile(ex, True)
            Close()
            Exit Sub
        End Try
    End Sub

    Private Sub btnRunModel_Click(sender As Object, e As EventArgs) Handles btnRunModel.Click
        Try
            If Not isWorking Then

                If clbColumns.CheckedIndices.Count > 0 OrElse
                (chkUseExistingModel.Checked AndAlso ModelExists) OrElse
                (Not chkUseExistingModel.Checked AndAlso Not chkMakePredictions.Checked AndAlso Not chkSavePredictionModel.Checked) Then

                    If FuncInProgress.Count = 0 Then
                        pnlMain.Enabled = False
                        isWorking = True
                        pnlMain.Controls.Remove(btnRunModel)
                        Controls.Add(btnRunModel)
                        btnRunModel.BringToFront()
                        btnRunModel.Enabled = True
                        pbLoading.Location = New Point(lblInProgress.Location.X + lblInProgress.Width + 6, btnRunModel.Location.Y - pbLoading.Height - 6)
                        pbLoading.Width = pbLoading.Parent.Width - pbLoading.Location.X - 14
                        lblInProgress.Visible = True
                        pbLoading.Style = ProgressBarStyle.Marquee
                        pbLoading.MarqueeAnimationSpeed = 1
                        pbLoading.Visible = True
                        btnRunModel.Text = strLanguage_RandomForest(37) '&Cancel
                        fswModelFileExists.EnableRaisingEvents = False
                        fswTrainAndTest.EnableRaisingEvents = False
                        fswXDFFileExists.EnableRaisingEvents = False
                        FuncInProgress.Add(strLanguage_RandomForest(24)) 'Applying Random Forest
                        Try
                            Dim TestColumnNames() As String = {}

                            If RDotNet_Initialization() Then
                                If StopWorking Then
                                    FuncInProgress.Remove(strLanguage_RandomForest(24)) 'Applying Random Forest
                                    Close()
                                    Exit Sub
                                End If
                                If chkShowDataSummary.Checked Then Rdo.Evaluate("ShowDataSummary <- TRUE") Else Rdo.Evaluate("ShowDataSummary <- FALSE")
                                If chkShowVariableInfo.Checked Then Rdo.Evaluate("ShowVariableInfo <- TRUE") Else Rdo.Evaluate("ShowVariableInfo <- FALSE")
                                If chkStatisticsMode.Checked Then Rdo.Evaluate("StatisticsMode <- TRUE") Else Rdo.Evaluate("StatisticsMode <- FALSE")
                                If chkUseExistingModel.Checked Then Rdo.Evaluate("UseExistingModel <- TRUE") Else Rdo.Evaluate("UseExistingModel <- FALSE")
                                If chkMakePredictions.Checked Then Rdo.Evaluate("MakePredictions <- TRUE") Else Rdo.Evaluate("MakePredictions <- FALSE")
                                If chkSavePredictionModel.Checked Then Rdo.Evaluate("SavePredictionModel <- TRUE") Else Rdo.Evaluate("SavePredictionModel <- FALSE")
                                If chkShowStatistics.Checked Then Rdo.Evaluate("ShowStatistics <- TRUE") Else Rdo.Evaluate("ShowStatistics <- FALSE")
                                If chkShowROCCurve.Checked Then Rdo.Evaluate("ShowROCCurve <- TRUE") Else Rdo.Evaluate("ShowROCCurve <- FALSE")
                                If chkColumnsCombinations.Checked Then Rdo.Evaluate("ColumnsCombinations <- TRUE") Else Rdo.Evaluate("ColumnsCombinations <- FALSE")

                                If chkPlotVarImportance.Checked Then Rdo.Evaluate(sa("PlotVarImportance <- {0}", cbPlotVarImportance.SelectedItem.ToString)) Else Rdo.Evaluate("PlotVarImportance <- FALSE")
                                If chkClassMethod.Checked Then Rdo.Evaluate(sa("ClassMethod <- '{0}'", cbClassMethod.SelectedItem.ToString)) Else Rdo.Evaluate("ClassMethod <- 'anova'")
                                If chkShowOOBEPlot.Checked Then Rdo.Evaluate(sa("ShowOOBEPlot <- {0}", cbShowOOBEPlot.SelectedItem.ToString)) Else Rdo.Evaluate("ShowOOBEPlot <- FALSE")

                                WriteToLog({vbCrLf &
                                            sa("chkShowDataSummary.Checked = {0}", chkShowDataSummary.Checked),
                                            sa("chkShowVariableInfo.Checked = {0}", chkShowVariableInfo.Checked),
                                            sa("chkStatisticsMode.Checked = {0}", chkStatisticsMode.Checked),
                                            sa("chkUseExistingModel.Checked = {0}", chkUseExistingModel.Checked),
                                            sa("chkMakePredictions.Checked = {0}", chkMakePredictions.Checked),
                                            sa("chkSavePredictionModel.Checked = {0}", chkSavePredictionModel.Checked),
                                            sa("chkShowStatistics.Checked = {0}", chkShowStatistics.Checked),
                                            sa("chkShowROCCurve.Checked = {0}", chkShowROCCurve.Checked),
                                            sa("chkColumnsCombinations.Checked = {0}", chkColumnsCombinations.Checked)
                                           }, doProperPathName(strGraphs) & "Models.log", False)

                                Dim CurCheckedColumnNames As New List(Of String)
                                For i = 0 To clbColumns.CheckedIndices.Count - 1
                                    CurCheckedColumnNames.Add(TrainingColumnNames(clbColumns.CheckedIndices(i)))
                                Next

                                'Making sure every checked column exists in the test dataset as well (as they came from training)
                                Dim CheckedColumnsExistInTest As Boolean = True
                                For i As Integer = 0 To CurCheckedColumnNames.Count - 1
                                    CheckedColumnsExistInTest = CheckedColumnsExistInTest And isContained(CurCheckedColumnNames(i), Test_DSColumnNames, True)
                                Next

                                If CheckedColumnsExistInTest Then

                                    Dim ColumnNamesForFormula As New List(Of List(Of List(Of String)))
                                    Dim ColumnNamesFormula As New List(Of String)


                                    If Not chkColumnsCombinations.Checked Then 'If there's only 1 combination - that of all of the variables together
                                        ColumnNamesForFormula.Add(New List(Of List(Of String)))
                                        ColumnNamesForFormula(0).Add(CurCheckedColumnNames.ToList)
                                        ColumnNamesFormula.Add(getDependentVars(ColumnNamesForFormula(0)(0)))

                                    ElseIf Not chkUpToNGramsN.Checked Then 'Else if there is only nChoosek combinations of CheckedColumns-Choose-nGram
                                        Dim k As Integer = CInt(txtNGrams.Text)
                                        ColumnNamesForFormula.Add(nChooseK(CurCheckedColumnNames, k))
                                        ColumnNamesFormula.AddRange((From EachComb In ColumnNamesForFormula(0) Select getDependentVars(EachComb)).ToList)

                                    Else 'Else it's all nChoosek combinations for each 'k' from 1 to nGram
                                        Dim CurK As Integer = -1
                                        For i As Integer = 0 To CInt(txtNGrams.Text) - 1
                                            CurK = i + 1
                                            ColumnNamesForFormula.Add(nChooseK(CurCheckedColumnNames, CurK))
                                            ColumnNamesFormula.AddRange((From EachComb In ColumnNamesForFormula(i) Select getDependentVars(EachComb)).ToList)
                                        Next
                                    End If

                                    If ColumnNamesFormula(0) = "" AndAlso clbColumns.Items.Count > 0 Then ColumnNamesFormula(0) = clbColumns.Items(0).ToString

                                    Dim predVarName_ForMultiROC As New List(Of String)

                                    For i As Integer = 0 To ColumnNamesFormula.Count - 1
                                        If StopWorking Then
                                            FuncInProgress.Remove(strLanguage_RandomForest(24)) 'Applying Random Forest
                                            Close()
                                            Exit Sub
                                        End If
                                        WriteToLog({sa("RF_PredictionReal{0}:  {1}",
                                                        If(chkColumnsCombinations.Checked, (i + 1).ToString, ""),
                                                        ColumnNamesFormula(i))},
                                                   doProperPathName(strGraphs) & "Models.log")

                                        Dim ResultsFileName As String
                                        If chkColumnsCombinations.Checked OrElse chkUpToNGramsN.Checked Then
                                            ResultsFileName = sa("RandomForest_Results_{0}.csv", (i + 1))
                                            predVarName_ForMultiROC.Add(sa("RF_PredictionReal{0}", (i + 1)))
                                        Else
                                            ResultsFileName = "RandomForest_Results.csv"
                                        End If

                                        Dim SinkFilePathLinux As String = doProperFileNameLinux(strSinkFile)
                                        If RSource({strFunctions & "3.4 Random Forest.R"}, , {"{reportProgress}", If(chkreportProgress.Checked, txtReportProgress.Text, "rxGetOption('reportProgress')"),
                                                                                                "{blocksPerRead}", If(chkBlocksPerRead.Checked, txtBlocksPerRead.Text, "rxGetOption('blocksPerRead')"),
                                                                                                "{rowSelection}", If(chkrowSelection.Checked, txtrowSelection.Text, "NULL"),
                                                                                                "{0}", ColumnNamesFormula(i),
                                                                                                "{1}", ModelSavePath,
                                                                                                "{2}", txtRoundAt.Text,
                                                                                                "{3}", ResultsFileName,
                                                                                                "{4}", SinkFilePathLinux,
                                                                                                "{5}", If(chkColumnsCombinations.Checked, (i + 1).ToString, ""),
                                                                                                "{6}", "RandomForestModel",
                                                                                                "{7}", If(chkClassMethod.Checked AndAlso cbClassMethod.SelectedIndex = 0, sa("'0_prob', 'RF_PredictionReal{0}'", If(chkColumnsCombinations.Checked, (i + 1).ToString, "")),
                                                                                                                                                                          sa("'RF_PredictionReal{0}'", If(chkColumnsCombinations.Checked, (i + 1).ToString, ""))),
                                                                                                "{8}", If(chkCP.Checked, txtCP.Text, "0.001"),
                                                                                                "{9}", If(chknTree.Checked, txtnTree.Text, "500"),
                                                                                                "{10}", If(chkmTry.Checked, txtmTry.Text, "NULL"),
                                                                                                "{11}", If(chkmaxDepth.Checked, txtmaxDepth.Text, "15")
                                                                                                }, True) Then

                                            If StopWorking Then
                                                FuncInProgress.Remove(strLanguage_RandomForest(24)) 'Applying Random Forest
                                                Close()
                                                Exit Sub
                                            End If

                                            If chkStatisticsMode.Checked Then
                                                If chkShowStatistics.Checked Then
                                                    Dim LabelPredictionExist As Boolean = Rdo.GetSymbol("LabelPredictionExist").AsLogical.First

                                                    'StatisticsResults GenericVector was to be used to extract the info and mold it to a better shape, but this is too time consuming to do at the moment, so 'sink()' is used instead.
                                                    If Not LabelPredictionExist Then 'If those don't exist, then the Statistics will crash
                                                        '         '{1}' was checked, however the Testing Dataset did not have the needed variables.{0}Have you forgotten applying the Predictions from the Machine Learning Algorithm?
                                                        MsgBox(sa(strLanguage_RandomForest(38), vbCrLf, RemCtrHotLetter(chkShowStatistics)))
                                                    Else
                                                        Dim StatisticsResults As GenericVector = Rdo.GetSymbol("StatisticsResults").AsList
                                                    End If

                                                    If chkShowStatistics.Checked Then
                                                        Dim StatisticsForm As New frmStatistics With {.SinkFilePath = SinkFilePathLinux, .ClassificationModelName = "RandomForestModel"}
                                                        StatisticsForm.Show()
                                                    End If

                                                End If

                                                If chkShowROCCurve.Checked Then
                                                    Dim PredictionRealExist As Boolean = Rdo.GetSymbol("PredictionRealExist").AsLogical.First

                                                    If Not PredictionRealExist Then 'If this doesn't exist, then the ROC curve will crash
                                                        '         '{1}' was checked, however the Testing Dataset did not have the needed variables.{0}Have you forgotten applying the Predictions from the Machine Learning Algorithm?
                                                        MsgBox(sa(strLanguage_RandomForest(38), vbCrLf, RemCtrHotLetter(chkShowROCCurve)))
                                                    End If
                                                End If
                                            End If

                                            If chkShowDataSummary.Checked OrElse chkShowVariableInfo.Checked Then
                                                TestColumnNames = Rdo.GetSymbol("TestColumnNames").AsCharacter.ToArray

                                                If chkShowDataSummary.Checked Then
                                                    Dim DataSummary As DataFrame = Rdo.GetSymbol("TestDataSummary").AsDataFrame
                                                    Dim DataSummaryVisualiserForm As New frmDataSummaryVisualiser With {.dfDataSummary = DataSummary, .DatasetName = "Test"}
                                                    DataSummaryVisualiserForm.Show()
                                                End If

                                                If chkShowVariableInfo.Checked Then
                                                    Dim VariableInfo = Rdo.GetSymbol("TestVarInfo").AsList
                                                    Dim VariableInfoVisualiserForm As New frmVariableInfoVisualiser With {.rlstVariableInfo = VariableInfo, .ColumnNames = TestColumnNames, .DatasetName = "Test"}
                                                    VariableInfoVisualiserForm.Show()
                                                End If
                                            End If

                                            Dim RDSCreatedOutOfNecessity As Boolean = Rdo.GetSymbol("RDSCreatedOutOfNecessity").AsLogical.First
                                            'The option '{1}' was checked but the file was unreachable and was created instead.
                                            If RDSCreatedOutOfNecessity Then MsgBox(sa(strLanguage_RandomForest(28), vbCrLf, RemCtrHotLetter(chkUseExistingModel)))

                                            If chkMakePredictions.Checked AndAlso Not chkStatisticsMode.Checked Then
                                                Dim ResultsFilePath As String = doProperFileName(strXDF & ResultsFileName)
                                                If File.Exists(ResultsFilePath) Then
                                                    Try
                                                        RunOpenDir(ResultsFilePath)
                                                    Catch ex As Exception
                                                    End Try
                                                End If
                                            End If

                                            If chkPlotVarImportance.Checked Then
                                                Dim PlotFilePath As String = doProperFileName(strGraphs & "RandomForestModel_VariablesImportance" & If(chkColumnsCombinations.Checked, (i + 1).ToString, "") & ".png")
                                                If File.Exists(PlotFilePath) Then
                                                    RunOpenDir(PlotFilePath)
                                                End If
                                            End If

                                            If chkShowOOBEPlot.Checked Then
                                                Dim PlotFilePath As String = doProperFileName(strGraphs & "RandomForestModel_OutOfBagError" & If(chkColumnsCombinations.Checked, (i + 1).ToString, "") & ".png")
                                                If File.Exists(PlotFilePath) Then
                                                    RunOpenDir(PlotFilePath)
                                                End If
                                            End If

                                        End If
                                    Next
                                    WriteToLog("", doProperPathName(strGraphs) & "Models.log", False)

                                    If chkColumnsCombinations.Checked AndAlso chkShowROCCurve.Checked Then
                                        If chkOpenGraphDirectory.Checked Then
                                            RunOpenDir(doProperPathName(strGraphs))
                                        End If

                                        Dim predVarName As String = ArrayBox(False, ",", 0UI, False, predVarName_ForMultiROC, False, " ", True, """", """",,,, "''")
                                        If Not RSource({strFunctions & "[Multiple_ROC_Comparisons].R"}, , {"{0}", predVarName,
                                                                                                    "{1}", CurRoundAt.ToString
                                                                                                    }, True) Then
                                            '         Either '{1}' is not reachable, or it did not contain the expected Columns:{0}{2}
                                            MsgBox(sa(strLanguage_RandomForest(39), vbCrLf, strXDF & "Test_DS.xdf", predVarName), MsgBoxStyle.Critical)
                                        End If
                                    End If

                                Else
                                    '         Not all of the Columns you checked exist in the Testing file, which means that they represent different Datasets and hence this Training set Cannot be used to predict this Testing set.{0}{0}Please create them anew.
                                    MsgBox(sa(strLanguage_RandomForest(40), vbCrLf))
                                End If

                            End If
                        Catch ex As Exception
                            MsgBox(ex.ToString)
                            'Notify(ex.ToString, Red, Black, 10)
                            'CreateCrashFile(ex) ''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            fswModelFileExists.EnableRaisingEvents = False
                            fswTrainAndTest.EnableRaisingEvents = False
                            fswXDFFileExists.EnableRaisingEvents = False
                            pnlMain.Enabled = True
                        End Try

                        lblInProgress.Visible = False
                        pbLoading.MarqueeAnimationSpeed = 0
                        pbLoading.Visible = False
                        FuncInProgress.Remove(strLanguage_RandomForest(24)) 'Applying Random Forest
                        fswModelFileExists.EnableRaisingEvents = False
                        fswTrainAndTest.EnableRaisingEvents = False
                        fswXDFFileExists.EnableRaisingEvents = False
                        pnlMain.Enabled = True
                        Close()
                    Else
                        '         Please wait for: {0} to finish
                        MsgBox(sa(strLanguage_RandomForest(26), ArrayBox(False, ";", 0, True, FuncInProgress)), MsgBoxStyle.Exclamation) '
                    End If


                Else
                    '         You haven't selected any Dependent Variables, ergo the operation is cancelled
                    MsgBox(sa(strLanguage_RandomForest(41)), MsgBoxStyle.Exclamation)
                End If


            Else '#If isWorking
                StopWorking = True
                btnRunModel.Enabled = False
                btnRunModel.Text = strLanguage_RandomForest(42) 'Cancelling...

            End If
        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub


#Region "Misc"

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        If (chkShowDataSummary.Checked OrElse Not chkShowDataSummary.Enabled) AndAlso
           (chkShowVariableInfo.Checked OrElse Not chkShowVariableInfo.Enabled) AndAlso
           (chkUseExistingModel.Checked OrElse Not chkUseExistingModel.Enabled) AndAlso
           (chkMakePredictions.Checked OrElse Not chkMakePredictions.Enabled) AndAlso
           (chkSavePredictionModel.Checked OrElse Not chkSavePredictionModel.Enabled) Then

            If chkShowDataSummary.Enabled Then chkShowDataSummary.Checked = False
            If chkShowVariableInfo.Enabled Then chkShowVariableInfo.Checked = False
            If chkUseExistingModel.Enabled Then chkUseExistingModel.Checked = False
            If chkMakePredictions.Enabled Then chkMakePredictions.Checked = False
            If chkSavePredictionModel.Enabled Then chkSavePredictionModel.Checked = False
        Else
            If chkShowDataSummary.Enabled Then chkShowDataSummary.Checked = True
            If chkShowVariableInfo.Enabled Then chkShowVariableInfo.Checked = True
            If chkUseExistingModel.Enabled Then chkUseExistingModel.Checked = True
            If chkMakePredictions.Enabled Then chkMakePredictions.Checked = True
            If chkSavePredictionModel.Enabled Then chkSavePredictionModel.Checked = True
        End If
    End Sub

    Private Sub chkUseExistingModel_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseExistingModel.CheckedChanged
        If chkUseExistingModel.Checked AndAlso ModelExists Then
            'Something
            'Dim Count As Integer = clbColumns.CheckedIndices.Count
            'Dim iTo As Integer = Count - 1
            'For i As Integer = 0 To iTo
            '    Dim a As Integer = clbColumns.CheckedIndices(i)
            '    clbColumns.SetItemChecked(a, False)
            'Next
            gbSettings.Enabled = False

            btnSelectAllColumns.Enabled = False
            clbColumns.Enabled = False
            For i As Integer = 0 To clbColumns.Items.Count - 1
                clbColumns.SetItemChecked(i, False)
            Next

        Else
            gbSettings.Enabled = True

            btnSelectAllColumns.Enabled = True
            clbColumns.Enabled = True

        End If
    End Sub

    Private Sub chkOptions_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowDataSummary.CheckedChanged,
                                                                                    chkShowVariableInfo.CheckedChanged,
                                                                                    chkUseExistingModel.CheckedChanged,
                                                                                    chkMakePredictions.CheckedChanged,
                                                                                    chkSavePredictionModel.CheckedChanged
        Call UpdateSelectAllText()

        Call ColourChkStatisticsMode()
    End Sub

    Private Sub UpdateSelectAllText()
        If (chkShowDataSummary.Checked OrElse Not chkShowDataSummary.Enabled) AndAlso
           (chkShowVariableInfo.Checked OrElse Not chkShowVariableInfo.Enabled) AndAlso
           (chkUseExistingModel.Checked OrElse Not chkUseExistingModel.Enabled) AndAlso
           (chkMakePredictions.Checked OrElse Not chkMakePredictions.Enabled) AndAlso
           (chkSavePredictionModel.Checked OrElse Not chkSavePredictionModel.Enabled) Then

            btnSelectAll.Text = strLanguage_RandomForest(25) 'Unselect &All
        Else
            btnSelectAll.Text = strLanguage_RandomForest(12) 'Select &All
        End If
    End Sub
    Private Sub clbColumns_SelectedIndexChanged(sender As Object, e As EventArgs) Handles clbColumns.SelectedIndexChanged, clbColumns.DoubleClick
        If clbColumns.CheckedIndices.Count = clbColumns.Items.Count Then
            btnSelectAllColumns.Text = strLanguage_RandomForest(43) 'Unselect All &Columns
        Else
            btnSelectAllColumns.Text = strLanguage_RandomForest(29) 'Select &All Columns
        End If

        If clbColumns.CheckedIndices.Count > 0 Then
            If chkColumnsCombinations.Checked AndAlso chkUpToNGramsN.Checked = False Then
                txtNGrams.Text = If((clbColumns.CheckedIndices.Count > 1), ((clbColumns.CheckedIndices.Count - 1).ToString), "1")
            Else
                txtNGrams.Text = clbColumns.CheckedIndices.Count.ToString
            End If
        Else
            txtNGrams.Text = "1"
        End If
        'Call UpdateCombinations() Included in txtNGrams_TextChanged

    End Sub

    Private Sub btnSelectAllColumns_Click(sender As Object, e As EventArgs) Handles btnSelectAllColumns.Click
        If clbColumns.CheckedIndices.Count = clbColumns.Items.Count Then
            For i = 0 To clbColumns.Items.Count - 1
                clbColumns.SetItemChecked(i, False)
            Next
            btnSelectAllColumns.Text = strLanguage_RandomForest(29) 'Select &All Columns
        Else
            For i = 0 To clbColumns.Items.Count - 1
                clbColumns.SetItemChecked(i, True)
            Next
            btnSelectAllColumns.Text = strLanguage_RandomForest(43) 'Unselect All &Columns

            If chkColumnsCombinations.Checked AndAlso chkUpToNGramsN.Checked = False Then
                txtNGrams.Text = If((clbColumns.CheckedIndices.Count > 1), ((clbColumns.CheckedIndices.Count - 1).ToString), "1")
            Else
                txtNGrams.Text = clbColumns.CheckedIndices.Count.ToString
            End If

        End If

        Call UpdateCombinations()

    End Sub

    Private Sub chkUpToColumnsCount_CheckedChanged(sender As Object, e As EventArgs) Handles chkUpToNGramsN.CheckedChanged, chkColumnsCombinations.CheckedChanged
        If chkColumnsCombinations.Checked Then
            chkUpToNGramsN.Enabled = True

            chkSavePredictionModel.Checked = False
            chkSavePredictionModel.Enabled = False
            chkShowDataSummary.Checked = False
            chkShowDataSummary.Enabled = False
            chkShowVariableInfo.Checked = False
            chkShowVariableInfo.Enabled = False
            chkUseExistingModel.Checked = False
            chkUseExistingModel.Enabled = False
            chkmTry.Checked = False
            chkmTry.Enabled = False

            If chkShowROCCurve.Checked Then
                chkOpenGraphDirectory.Enabled = True
            Else
                chkOpenGraphDirectory.Enabled = False
                chkOpenGraphDirectory.Checked = False
            End If


        Else
            chkUpToNGramsN.Enabled = False
            chkUpToNGramsN.Checked = False

            chkSavePredictionModel.Enabled = True
            chkShowDataSummary.Enabled = True
            chkShowVariableInfo.Enabled = True
            chkUseExistingModel.Enabled = True
            chkmTry.Enabled = True

            chkOpenGraphDirectory.Enabled = False
            chkOpenGraphDirectory.Checked = False

            Call UpdateCombinations() 'txtNGrams_TextChanged isn't called from here, hence manually Updating
        End If

        If clbColumns.CheckedIndices.Count > 0 Then
            If chkColumnsCombinations.Checked AndAlso chkUpToNGramsN.Checked = False Then
                txtNGrams.Text = If((clbColumns.CheckedIndices.Count > 1), ((clbColumns.CheckedIndices.Count - 1).ToString), "1")
            Else
                txtNGrams.Text = clbColumns.CheckedIndices.Count.ToString
            End If
        Else
            txtNGrams.Text = "1"
        End If

        Call UpdateSelectAllText()
        'Call UpdateCombinations() 'Is Included in txtNGrams_TextChanged
    End Sub

    Private Sub chkStatisticsMode_CheckedChanged(sender As Object, e As EventArgs) Handles chkStatisticsMode.CheckedChanged
        If chkStatisticsMode.Checked Then
            chkShowStatistics.Enabled = True
            chkShowROCCurve.Enabled = True
            If chkColumnsCombinations.Checked AndAlso chkColumnsCombinations.Checked Then chkOpenGraphDirectory.Enabled = True
        Else
            chkShowStatistics.Enabled = False
            chkShowStatistics.Checked = False
            chkShowROCCurve.Enabled = False
            chkShowROCCurve.Checked = False
            chkOpenGraphDirectory.Enabled = False
            chkOpenGraphDirectory.Checked = False
        End If

        Call ColourChkStatisticsMode()
    End Sub
    Private Sub txtSavePath_Click(sender As Object, e As EventArgs) Handles txtSavePath.Click
        fbdModelPath.Description = strLanguage_RandomForest(44) 'Where would you like your Model to be saved?
        fbdModelPath.RootFolder = Environment.SpecialFolder.Desktop
        fbdModelPath.ShowNewFolderButton = True
        If fbdModelPath.ShowDialog() = DialogResult.OK Then
            ModelSavePath = doProperPathNameLinux(fbdModelPath.SelectedPath)
            txtSavePath.Text = ModelSavePath
        End If
    End Sub


#End Region
    <DebuggerStepThrough>
    Private Sub tmrLoadColumns_Tick(sender As Object, e As EventArgs) Handles tmrLoadColumns.Tick
        Try
            tmrLoadColumns.Enabled = False

            Try
                RSource(strFunctions & "[ColumnNames_For_Classification].R")
                TrainingColumnNames = Rdo.GetSymbol("TrainingColumnNames").AsCharacter.ToArray
                clbColumns.DataSource = Nothing
                clbColumns.DataSource = TrainingColumnNames
            Catch ex As Exception
                'Unfortunately R was unable to provide the variable '{1}' as requested by '{2}', which is needed for the classification to take place.{0}The form will now close
                MsgBox(sa(strLanguage_RandomForest(45), vbCrLf, "TrainingColumnNames", strFunctions & "[ColumnNames_For_Classification].R"))
                Close()
                Exit Sub
            End Try

            lblColumnsLoading.Visible = False
            pbColumnsLoading.Visible = False
            pbColumnsLoading.MarqueeAnimationSpeed = 0
            If chkUseExistingModel.Checked AndAlso ModelExists Then btnSelectAllColumns.Enabled = False Else btnSelectAllColumns.Enabled = True
            chkColumnsCombinations.Enabled = True
            btnRunModel.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtNGrams_Click(sender As Object, e As EventArgs) Handles txtNGrams.Click
        If clbColumns.CheckedIndices.Count > 0 Then
            Dim NGram As Integer = -1
            '          Type a number for the 'n' for n-grams:
            If TypeBox(strLanguage_RandomForest(46), NGram, False,, 1, clbColumns.CheckedIndices.Count,,,,
                       If((clbColumns.CheckedIndices.Count > 1), (clbColumns.CheckedIndices.Count - 1).ToString, "1")) Then
                txtNGrams.Text = NGram.ToString
            End If
        End If
    End Sub
    Private Sub txtNGrams_TextChanged(sender As Object, e As EventArgs) Handles txtNGrams.TextChanged
        Call UpdateCombinations()
    End Sub

    Private Sub txtRoundAt_Click(sender As Object, e As EventArgs) Handles txtRoundAt.Click
        '          Round decimal points at:
        If TypeBox(strLanguage_RandomForest(17), CurRoundAt, False,, 1, Integer.MaxValue,,,, RoundAt.ToString) Then
            txtRoundAt.Text = CurRoundAt.ToString
        End If
    End Sub

    Private Sub fswModelExists_Created_Created_Renamed(sender As Object, e As FileSystemEventArgs) Handles fswModelFileExists.Created, fswModelFileExists.Deleted, fswModelFileExists.Renamed
        Call CheckModelExists()
    End Sub

    Private Sub lblCombinationsCount_SizeChanged(sender As Object, e As EventArgs) Handles lblCombinationsCount.SizeChanged
        lblCombinationsCount.Location = New Point(lblCombinationsCount.Parent.Width - lblCombinationsCount.Width - 5, lblCombinationsCount.Location.Y)
    End Sub

    Private Sub fswTrainAndTest_Created_Created_Renamed(sender As Object, e As FileSystemEventArgs) Handles fswTrainAndTest.Renamed, fswTrainAndTest.Deleted, fswTrainAndTest.Created
        If e.Name.EndsWith("Training_DS.xdf") OrElse e.Name.EndsWith("Test_DS.xdf") Then
            If File.Exists(strXDF & "Training_DS.xdf") AndAlso File.Exists(strXDF & "Test_DS.xdf") Then
                pnlMain.Enabled = True
                lblColumnsLoading.Visible = True
                pbColumnsLoading.Visible = True
                pbColumnsLoading.MarqueeAnimationSpeed = 1

                CheckXDFFileExists()
            Else
                'The Training or Testing XDF file have been deleted; without them the Classification Procedure cannot commence and the form has therefore been disabled.{0}{0}Please replace them for the form to be re-enabled.
                ShowMTNonInterruptingMsgbox(Me, sa(strLanguage_RandomForest(47), vbCrLf), MsgBoxStyle.Exclamation)
                'MsgBox(sa("The Training or Testing XDF file have been deleted; without them the Classification Procedure cannot commence and the form has therefore been disabled.{0}{0}Please replace them for the form to be re-enabled.", vbCrLf), MsgBoxStyle.Exclamation)
                pnlMain.Enabled = False
            End If
        End If
    End Sub

    Private Sub chkShowROCCurve_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowROCCurve.CheckedChanged
        If chkColumnsCombinations.Checked Then
            chkOpenGraphDirectory.Enabled = True
        Else
            chkOpenGraphDirectory.Enabled = False
            chkOpenGraphDirectory.Checked = False
        End If
    End Sub

    Private Sub txtCP_Click(sender As Object, e As EventArgs) Handles txtCP.Click
        Dim Complexity As Decimal = 0.001D
        If TypeBox("Give a value for the minimum complexity:", Complexity, False,, 0D, 1D,,,,,,, Complexity.ToString) Then
            txtCP.Text = Complexity.ToString
        End If
    End Sub

    Private Sub txtnTree_Click(sender As Object, e As EventArgs) Handles txtnTree.Click
        Dim nTree As Integer = 500
        If TypeBox("Grow this number of trees:", nTree, False,, 10, Integer.MaxValue,,,, nTree.ToString) Then
            txtnTree.Text = nTree.ToString
        End If
    End Sub

    Private Sub txtmTry_Click(sender As Object, e As EventArgs) Handles txtmTry.Click
        Dim mTry As Integer = If(cbClassMethod.SelectedIndex = 0, CInt(Sqrt(clbColumns.CheckedIndices.Count)), CInt(clbColumns.CheckedIndices.Count / 3))
        If TypeBox("Variables to sample as split candidates at each node:", mTry, False,, 1, clbColumns.CheckedIndices.Count,,,, mTry.ToString) Then
            txtmTry.Text = mTry.ToString
        Else
            txtmTry.Text = "NULL"
        End If
    End Sub

    Private Sub txtmaxDepth_Click(sender As Object, e As EventArgs) Handles txtmaxDepth.Click
        Dim maxDepth As Integer = 15
        If TypeBox("Maximum depth of any tree node:", maxDepth, False,, 1, Integer.MaxValue,,,, maxDepth.ToString) Then
            txtmaxDepth.Text = maxDepth.ToString
        End If
    End Sub

    Private Sub lblRoundAt_SizeChanged(sender As Object, e As EventArgs) Handles lblRoundAt.SizeChanged
        txtRoundAt.Location = New Point(lblRoundAt.Location.X + lblRoundAt.Width + 6, txtRoundAt.Location.Y)
    End Sub

    Private Sub txtReportProgress_TextChanged(sender As Object, e As EventArgs) Handles txtReportProgress.TextChanged
        If Not isInitialising Then chkreportProgress.Checked = True
    End Sub

    Private Sub txtBlocksPerRead_TextChanged(sender As Object, e As EventArgs) Handles txtBlocksPerRead.TextChanged
        If Not isInitialising Then chkBlocksPerRead.Checked = True
    End Sub

    Private Sub txtrowSelection_TextChanged(sender As Object, e As EventArgs) Handles txtrowSelection.TextChanged
        If Not isInitialising Then chkrowSelection.Checked = True
    End Sub

    Private Sub cbPlotVarImportance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPlotVarImportance.SelectedIndexChanged
        If Not isInitialising Then chkPlotVarImportance.Checked = True
    End Sub

    Private Sub cbClassMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbClassMethod.SelectedIndexChanged
        If Not isInitialising Then chkClassMethod.Checked = True
    End Sub

    Private Sub cbShowOOBEPlot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbShowOOBEPlot.SelectedIndexChanged
        If Not isInitialising Then chkShowOOBEPlot.Checked = True
    End Sub

    Private Sub txtCP_TextChanged(sender As Object, e As EventArgs) Handles txtCP.TextChanged
        If Not isInitialising Then chkCP.Checked = True
    End Sub

    Private Sub txtnTree_TextChanged(sender As Object, e As EventArgs) Handles txtnTree.TextChanged
        If Not isInitialising Then chknTree.Checked = True
    End Sub

    Private Sub txtmTry_TextChanged(sender As Object, e As EventArgs) Handles txtmTry.TextChanged
        If Not isInitialising Then chkmTry.Checked = True
    End Sub

    Private Sub txtmaxDepth_TextChanged(sender As Object, e As EventArgs) Handles txtmaxDepth.TextChanged
        If Not isInitialising Then chkmaxDepth.Checked = True
    End Sub
End Class