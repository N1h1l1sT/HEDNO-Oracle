Imports System.Drawing.Color
Imports System.IO
Imports RDotNet

Public Class frmPreProcessing
    Public strLanguage_PreProcessing As String()
    Private XDFFileExists As Boolean = False
    Private isStatisticsXDF As Boolean = True

    '!Put on frm_Load
    'pbLoading.Location = New Point(0, CInt(pbLoading.Parent.Height / 2) + 15)
    'pbLoading.Width = pbLoading.Parent.Width
    'fswXDFFileExists.Path = strXDF
    'fswXDFFileExists.Filter = "vErga_DS.xdf"
    'Call CheckXDFFileExists()
    '
    '!Put on chkOptions_CheckedChanged
    'Call ColourChkStatisticsMode()

#Region "XDFFileExists"

    Private Sub CheckXDFFileExists()
        If File.Exists(doProperFileName(strXDF & "vErga_DS.xdf")) Then
            XDFFileExists = True
            chkUseExistingXDFFile.BackColor = LightGreen

            pbLoading.MarqueeAnimationSpeed = 10
            pbLoading.Visible = True
            lblLoading.Visible = True
            lblLoading.Dock = DockStyle.Fill
            tmrXDFExists.Enabled = True

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

    Private Sub tmrXDFExists_Tick(sender As Object, e As EventArgs) Handles tmrXDFExists.Tick
        tmrXDFExists.Enabled = False

        Try 'Non-Essential Functions
            If XDFFileExists Then
                If RDotNet_Initialization() Then
                    RSource(strFunctions & "[ColumnNames_For_FormLoad].R",, {"{0}", "vErga_DS"})
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

    Private Sub fswXDFFileExists_Created_Created_Renamed(sender As Object, e As FileSystemEventArgs) Handles fswXDFFileExists.Created, fswXDFFileExists.Deleted, fswXDFFileExists.Renamed
        Call CheckXDFFileExists()
    End Sub


#End Region

    Private Sub frmPreProcessing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'initialization
            Call PreProcessing_Language(Me)
            frmSkin(Me, False)
            '/initialization

            pbLoading.Location = New Point(0, CInt(pbLoading.Parent.Height / 2) + 15)
            pbLoading.Width = pbLoading.Parent.Width
            fswXDFFileExists.Path = strXDF
            fswXDFFileExists.Filter = "vErga_DS.xdf"
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

    Private Sub btnPreProcess_Click(sender As Object, e As EventArgs) Handles btnPreProcess.Click
        Try
            If FuncInProgress.Count = 0 Then
                FuncInProgress.Add("Pre-Processing Data")
                pnlMain.Enabled = False
                Try
                    Dim vErgaColumnNames() As String = {}
                    If RDotNet_Initialization() Then
                        If chkUseExistingXDFFile.Checked Then Rdo.Evaluate("UseExistingXDFFile <- TRUE") Else Rdo.Evaluate("UseExistingXDFFile <- FALSE")
                        If chkCleanXDFFile.Checked Then Rdo.Evaluate("CleanupXFDFile <- TRUE") Else Rdo.Evaluate("CleanupXFDFile <- FALSE")
                        If chkShowGeoLocGraph.Checked Then Rdo.Evaluate("ShowGeoLocGraph <- TRUE") Else Rdo.Evaluate("ShowGeoLocGraph <- FALSE")
                        If chkShowDataSummary.Checked Then Rdo.Evaluate("ShowDataSummary <- TRUE") Else Rdo.Evaluate("ShowDataSummary <- FALSE")
                        If chkShowVariableInfo.Checked Then Rdo.Evaluate("ShowVariableInfo <- TRUE") Else Rdo.Evaluate("ShowVariableInfo <- FALSE")

                        If RSource({strFunctions & "[ColumnsInfo].R",
                                    strFunctions & "1.0 Data-PreProcessing.R"}, , {"{0}", If(chkStatisticsMode.Checked, TablevErga, TablevFinalDataset),
                                                                                  "{1}", ColvGeoLocX,
                                                                                  "{2}", ColvGeoLocY}, True) Then
                            If chkShowDataSummary.Checked OrElse chkShowVariableInfo.Checked Then
                                vErgaColumnNames = Rdo.GetSymbol("vErgaColumnNames").AsCharacter.ToArray

                                If chkShowDataSummary.Checked Then
                                    Dim DataSummary As DataFrame = Rdo.GetSymbol("vErgaDataSummary").AsDataFrame
                                    Dim DataSummaryVisualiserForm As New frmDataSummaryVisualiser With {.dfDataSummary = DataSummary, .DatasetName = "Clustering"}
                                    DataSummaryVisualiserForm.Show()
                                End If

                                If chkShowVariableInfo.Checked Then
                                    Dim VariableInfo = Rdo.GetSymbol("vErgaVarInfo").AsList
                                    Dim VariableInfoVisualiserForm As New frmVariableInfoVisualiser With {.rlstVariableInfo = VariableInfo, .ColumnNames = vErgaColumnNames, .DatasetName = "Clustering"}
                                    VariableInfoVisualiserForm.Show()
                                End If
                            End If

                            Dim XDFCreatedOutOfNecessity As Boolean = Rdo.GetSymbol("XDFCreatedOutOfNecessity").AsLogical.First
                            If XDFCreatedOutOfNecessity Then MsgBox(sa("The option '{0}' was checked but the file was unreachable and was created instead.", RemCtrHotLetter(chkUseExistingXDFFile)))
                        End If
                    End If
                Catch ex As Exception
                    Notify(ex.ToString, Color.Red, Color.Black, 10)
                    CreateCrashFile(ex)
                    pnlMain.Enabled = True
                End Try

                FuncInProgress.Remove("Pre-Processing Data")
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


End Class