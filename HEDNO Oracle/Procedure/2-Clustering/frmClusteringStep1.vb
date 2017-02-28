Imports System.Drawing.Color
Imports System.IO
Imports RDotNet

Public Class frmClusteringStep1
    Public strLanguage_ClusteringStep1 As String()

    Private Const DefaultMaxKNum As Integer = 30
    Private MaxKNum As Integer = DefaultMaxKNum
    Private Shared KMeansModelSavePath As String = strXDF
    Private XDFFileExists As Boolean = False
    Private isStatisticsXDF As Boolean = True

    '!Put on frm_Load
    'pbLoading.Location = New Point(0, CInt(pbLoading.Parent.Height / 2) + 15)
    'pbLoading.Width = pbLoading.Parent.Width
    'fswModelExists.Path = strXDF
    'fswModelExists.Filter = "vErga_DS.xdf"
    'Call CheckXDFFileExists()
    '
    '!Put on chkOptions_CheckedChanged
    'Call ColourChkStatisticsMode()
#Region "XDFFileExists"

    Private Sub CheckXDFFileExists()
        If File.Exists(doProperFileName(strXDF & "Clustering_DS.xdf")) Then
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
                    RSource(strFunctions & "[ColumnNames_For_FormLoad].R",, {"{0}", "Clustering_DS"})
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

    Private Sub chkStatisticsMode_CheckedChanged(sender As Object, e As EventArgs) Handles chkStatisticsMode.CheckedChanged
        Call ColourChkStatisticsMode()
    End Sub

    Private Sub fswModelExists_Created_Created_Renamed(sender As Object, e As FileSystemEventArgs) Handles fswModelExists.Created, fswModelExists.Deleted, fswModelExists.Renamed
        Call CheckXDFFileExists()
    End Sub


#End Region

    Private Sub frmClusteringStep1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'initialization
            Call ClusteringStep1_Language(Me)
            frmSkin(Me, False)
            '/initialization

            txtMaxClusterNum.Text = DefaultMaxKNum.ToString
            txtSavePath.Text = KMeansModelSavePath

            pbLoading.Location = New Point(0, CInt(pbLoading.Parent.Height / 2) + 15)
            pbLoading.Width = pbLoading.Parent.Width
            fswModelExists.Path = strXDF
            fswModelExists.Filter = "Clustering_DS.xdf"
            Call CheckXDFFileExists()

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Shadows Sub FormClosing(ByVal sender As Object, ByVal e As ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If FuncInProgress.Count <> 0 Then
            e.Cancel = True
            MsgBox(sa("Please wait for: {0} to finish", ArrayBox(False, ";", 0, True, FuncInProgress)), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub btnClustering_Click(sender As Object, e As EventArgs) Handles btnClustering1.Click
        Try
            If FuncInProgress.Count = 0 Then
                FuncInProgress.Add("Clustering Step 1")
                fswModelExists.EnableRaisingEvents = False
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
                        If chkStatisticsMode.Checked Then Rdo.Evaluate("StatisticsMode <- TRUE") Else Rdo.Evaluate("StatisticsMode <- FALSE")


                        If RSource({strFunctions & "[ColumnsInfo].R",
                                    strFunctions & "2.1 Clustering Step 1.R"}, , {"{0}", If(chkStatisticsMode.Checked, TablevErga, TablevFinalDataset),
                                                                                  "{1}", ColvGeoLocX,
                                                                                  "{2}", ColvGeoLocY,
                                                                                  "{3}", MaxKNum.ToString,
                                                                                  "{4}", KMeansModelSavePath}, True) Then

                            Dim k As Integer = Rdo.GetSymbol("k").AsInteger.First
                            If TypeBox(sa("How many clusters should the database be separated into?{0}After analysis, the Recommended Value for this dataset is: [{1}].", vbCrLf, k),
                                       k,
                                       False,
                                       "Choosing k for K-Means",
                                       2,,,,, k.ToString) Then
                                Rdo.SetSymbol("k", Rdo.CreateIntegerVector({k}))

                                If RSource({strFunctions & "[ColumnsInfo].R",
                                        strFunctions & "2.1 Clustering Step 1_2nd Falf.R"}, , {"{0}", If(chkStatisticsMode.Checked, TablevErga, TablevFinalDataset),
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
                                    If XDFCreatedOutOfNecessity Then MsgBox(sa("The option '{0}' was checked but the file was unreachable and was created instead.", RemCtrHotLetter(chkUseExistingXDFFile)))
                                End If

                            Else
                                MsgBox(sa("Operation Cancelled."), MsgBoxStyle.Exclamation)
                            End If
                        End If
                    End If
                Catch ex As Exception
                    Notify(ex.ToString, Color.Red, Color.Black, 10)
                    CreateCrashFile(ex)
                    pnlMain.Enabled = True
                End Try

                fswModelExists.EnableRaisingEvents = True
                FuncInProgress.Remove("Clustering Step 1")
                pnlMain.Enabled = True
                Close()
            Else
                MsgBox(sa("Please wait for: {0} to finish", ArrayBox(False, ";", 0, True, FuncInProgress)), MsgBoxStyle.Exclamation)
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

        Call ColourChkStatisticsMode()
    End Sub

    Private Sub txtMaxClusterNum_Click(sender As Object, e As EventArgs) Handles txtMaxClusterNum.Click
        If TypeBox(sa("Which is the maximum number of clusters you wish to test for?{0}The Default Value is: {1}", vbCrLf, DefaultMaxKNum), MaxKNum, False,, 3, MaxInteger,,,, DefaultMaxKNum.ToString) Then
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

    Private Sub chkSaveKMeansModel_CheckedChanged(sender As Object, e As EventArgs) Handles chkSaveKMeansModel.CheckedChanged
        If chkSaveKMeansModel.Checked Then
            lblSavePath.Enabled = True
            txtSavePath.Enabled = True
        Else
            lblSavePath.Enabled = False
            txtSavePath.Enabled = False
        End If
    End Sub
End Class