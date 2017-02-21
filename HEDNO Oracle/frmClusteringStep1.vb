Imports RDotNet

Public Class frmClusteringStep1
    Public strLanguage_ClusteringStep1 As String()

    Private Const DefaultMaxKNum As Integer = 30
    Private MaxKNum As Integer = DefaultMaxKNum
    Private Shared KMeansModelSavePath As String = strXDF

    Private Sub frmClusteringStep1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'initialization
            Call ClusteringStep1_Language(Me)
            frmSkin(Me, False)
            '/initialization

            txtMaxClusterNum.Text = DefaultMaxKNum.ToString
            txtSavePath.Text = KMeansModelSavePath

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnClustering_Click(sender As Object, e As EventArgs) Handles btnClustering1.Click
        Try
            If FuncInProgress.Count = 0 Then
                FuncInProgress.Add("Clustering Step 1")
                pnlMain.Enabled = False
                Try
                    Dim ClusteringColumnNames() As String = {}
                    If RDotNet_Initialization() Then
                        If chkUseExistingXDFFile.Checked Then Rdo.Evaluate("UseExistingXDFFile <- TRUE") Else Rdo.Evaluate("UseExistingXDFFile <- FALSE")
                        If chkCleanXDFFile.Checked Then Rdo.Evaluate("CleanupXFDFile <- TRUE") Else Rdo.Evaluate("CleanupXFDFile <- FALSE")
                        If chkShowGeoLocGraph.Checked Then Rdo.Evaluate("ShowGeoLocGraph <- TRUE") Else Rdo.Evaluate("ShowGeoLocGraph <- FALSE")
                        If chkShowDataSummary.Checked Then Rdo.Evaluate("ShowDataSummary <- TRUE") Else Rdo.Evaluate("ShowDataSummary <- FALSE")
                        If chkShowVariableInfo.Checked Then Rdo.Evaluate("ShowVariableInfo <- TRUE") Else Rdo.Evaluate("ShowVariableInfo <- FALSE")
                        If chkSaveKMeansModel.Checked Then Rdo.Evaluate("SaveKMeansModel <- TRUE") Else Rdo.Evaluate("SaveKMeansModel <- FALSE")


                        If RSource({strFunctions & "[ColumnsInfo].R",
                                    strFunctions & "2.1 Clustering Step 1.R"}, , {"{0}", TablevErga,
                                                                                  "{1}", ColvGeoLocX,
                                                                                  "{2}", ColvGeoLocY,
                                                                                  "{3}", MaxKNum.ToString,
                                                                                  "{4}", KMeansModelSavePath}, True) Then

                            Dim k As Integer = Rdo.GetSymbol("k").AsInteger.First
                            If TypeBox(ss("How many clusters should the database be separated into?{0}After analysis, the Recommended Value for this dataset is: [{1}].", vbCrLf, k),
                                       k,
                                       False,
                                       "Choosing k for K-Means",
                                       2,,,,, k.ToString) Then
                                Rdo.SetSymbol("k", Rdo.CreateIntegerVector({k}))

                                If RSource({strFunctions & "[ColumnsInfo].R",
                                        strFunctions & "2.2 Clustering Step 1.R"}, , {"{0}", TablevErga,
                                                          "{1}", ColvGeoLocX,
                                                          "{2}", ColvGeoLocY,
                                                          "{3}", MaxKNum.ToString,
                                                          "{4}", KMeansModelSavePath}, True) Then



                                    If chkShowDataSummary.Checked OrElse chkShowVariableInfo.Checked Then
                                        ClusteringColumnNames = Rdo.GetSymbol("ClusteringColumnNames").AsCharacter.ToArray

                                        If chkShowDataSummary.Checked Then
                                            Dim DataSummary As DataFrame = Rdo.GetSymbol("ClusteringDataSummary").AsDataFrame
                                            Dim DataSummaryVisualiserForm As New frmDataSummaryVisualiser With {.dfDataSummary = DataSummary, .DatasetName = "Clustering"}
                                            DataSummaryVisualiserForm.Show()
                                        End If

                                        If chkShowVariableInfo.Checked Then
                                            Dim VariableInfo = Rdo.GetSymbol("ClusteringVarInfo").AsList
                                            Dim VariableInfoVisualiserForm As New frmVariableInfoVisualiser With {.rlstVariableInfo = VariableInfo, .ColumnNames = ClusteringColumnNames, .DatasetName = "Clustering"}
                                            VariableInfoVisualiserForm.Show()
                                        End If
                                    End If

                                    Dim XDFCreatedOutOfNecessity As Boolean = Rdo.GetSymbol("XDFCreatedOutOfNecessity").AsLogical.First
                                    If XDFCreatedOutOfNecessity Then MsgBox(ss("The option '{0}' was checked but the file was unreachable and was created instead.", RemCtrHotLetter(chkUseExistingXDFFile)))
                                End If

                            Else
                                MsgBox(ss("Operation Cancelled."), MsgBoxStyle.Exclamation)
                            End If
                        End If
                        End If
                Catch ex As Exception
                    Notify(ex.ToString, Color.Red, Color.Black, 10)
                    CreateCrashFile(ex)
                    pnlMain.Enabled = True
                End Try

                FuncInProgress.Remove("Clustering Step 1")
                pnlMain.Enabled = True
                Close()
            Else
                MsgBox(ss("Please wait for: {0} to finish", ArrayBox(False, ";", 0, True, FuncInProgress)), MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        If chkShowDataSummary.Checked And chkShowGeoLocGraph.Checked And chkShowVariableInfo.Checked And chkUseExistingXDFFile.Checked Then
            chkUseExistingXDFFile.Checked = False
            chkShowDataSummary.Checked = False
            chkShowGeoLocGraph.Checked = False
            chkShowVariableInfo.Checked = False
        Else
            chkUseExistingXDFFile.Checked = True
            chkShowDataSummary.Checked = True
            chkShowGeoLocGraph.Checked = True
            chkShowVariableInfo.Checked = True
        End If
    End Sub

    Private Sub chkOptions_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowDataSummary.CheckedChanged, chkShowGeoLocGraph.CheckedChanged,
                                                                                    chkShowVariableInfo.CheckedChanged, chkUseExistingXDFFile.CheckedChanged
        If chkShowDataSummary.Checked And chkShowGeoLocGraph.Checked And chkShowVariableInfo.Checked And chkUseExistingXDFFile.Checked Then
            btnSelectAll.Text = "Unselect &All"
        Else
            btnSelectAll.Text = "Select &All"
        End If
    End Sub

    Private Shadows Sub FormClosing(ByVal sender As Object, ByVal e As ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If FuncInProgress.Count <> 0 Then
            e.Cancel = True
            MsgBox(ss("Please wait for: {0} to finish", ArrayBox(False, ";", 0, True, FuncInProgress)), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub txtMaxClusterNum_Click(sender As Object, e As EventArgs) Handles txtMaxClusterNum.Click
        If TypeBox(ss("Which is the maximum number of clusters you wish to test for?{0}The Default Value is: {1}", vbCrLf, DefaultMaxKNum), MaxKNum, False,, 3, MaxInteger,,,, DefaultMaxKNum.ToString) Then
            txtMaxClusterNum.Text = MaxKNum.ToString
        End If
    End Sub

    Private Sub lblMaxClusterNum_TextChanged(sender As Object, e As EventArgs) Handles lblMaxClusterNum.TextChanged
        Dim XLoc As Integer = pnlMain.Location.X + lblMaxClusterNum.Location.X + lblMaxClusterNum.Width + 5
        Dim YLoc As Integer = lblMaxClusterNum.Location.Y
        txtMaxClusterNum.Location = New Point(XLoc, YLoc)
    End Sub

    Private Sub txtSavePath_Click(sender As Object, e As EventArgs) Handles txtSavePath.Click
        fbdKMeansModel.Description = "Where would you like your K-Means Model to be saved?"
        fbdKMeansModel.RootFolder = Environment.SpecialFolder.Desktop
        fbdKMeansModel.ShowNewFolderButton = True
        If fbdKMeansModel.ShowDialog() = DialogResult.OK Then
            KMeansModelSavePath = doProperPathNameLinux(fbdKMeansModel.SelectedPath)
            txtSavePath.Text = KMeansModelSavePath
        End If

    End Sub
End Class