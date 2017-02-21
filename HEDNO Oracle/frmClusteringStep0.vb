Imports RDotNet

Public Class frmClusteringStep0
    Public strLanguage_ClusteringStep0 As String()

    Private Sub frmClusteringStep0_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'initialization
            Call ClusteringStep0_Language(Me)
            frmSkin(Me, False)
            '/initialization

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnClustering_Click(sender As Object, e As EventArgs) Handles btnClustering0.Click
        Try
            If FuncInProgress.Count = 0 Then
                FuncInProgress.Add("Clustering Step 0")
                pnlMain.Enabled = False
                Try
                    Dim ClusteringColumnNames() As String = {}
                    If RDotNet_Initialization() Then
                        If chkUseExistingXDFFile.Checked Then Rdo.Evaluate("UseExistingXDFFile <- TRUE") Else Rdo.Evaluate("UseExistingXDFFile <- FALSE")
                        If chkCleanXDFFile.Checked Then Rdo.Evaluate("CleanupXFDFile <- TRUE") Else Rdo.Evaluate("CleanupXFDFile <- FALSE")
                        If chkShowGeoLocGraph.Checked Then Rdo.Evaluate("ShowGeoLocGraph <- TRUE") Else Rdo.Evaluate("ShowGeoLocGraph <- FALSE")
                        If chkShowDataSummary.Checked Then Rdo.Evaluate("ShowDataSummary <- TRUE") Else Rdo.Evaluate("ShowDataSummary <- FALSE")
                        If chkShowVariableInfo.Checked Then Rdo.Evaluate("ShowVariableInfo <- TRUE") Else Rdo.Evaluate("ShowVariableInfo <- FALSE")


                        If RSource({strFunctions & "[ColumnsInfo].R",
                                    strFunctions & "2.0 Clustering Step 0.R"}, , {"{0}", TablevErga,
                                                                                  "{1}", ColvGeoLocX,
                                                                                  "{2}", ColvGeoLocY}, True) Then

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
                    End If
                Catch ex As Exception
                    Notify(ex.ToString, Color.Red, Color.Black, 10)
                    CreateCrashFile(ex)
                    pnlMain.Enabled = True
                End Try

                FuncInProgress.Remove("Clustering Step 0")
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

End Class