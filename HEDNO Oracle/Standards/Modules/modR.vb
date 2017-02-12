'Version 1.2 2013/03/10

Imports RDotNet
Imports System.IO
Imports System.Text
Imports Microsoft.Win32

Module modR
    Public Rdo As REngine
    Public strFunctions As String = strRoot & "Functions\"

    Public Function RDotNet_Initialization() As Boolean
        Try
            If Rdo Is Nothing Then

                Dim ArchitectureString As String
                If ProgramArchitecture = 32 Then
                    ArchitectureString = "i386"
                Else
                    ArchitectureString = "x64"
                End If

                Dim envPath As String = Environment.GetEnvironmentVariable("PATH")
                Dim rBinPath As String = ""
                Try
                    rBinPath = Registry.GetValue("HKEY_LOCAL_MACHINE\Software\R-core\R", "InstallPath", "").ToString
                Catch ex As Exception
                End Try

                If My.Settings.RDirectory = String.Empty _
                    OrElse Not Directory.Exists(doProperPathName(doResolveWildNames(My.Settings.RDirectory))) _
                    OrElse (doProperPathName(doResolveWildNames(rBinPath)).ToLower <> doProperPathName(doResolveWildNames(My.Settings.RDirectory))) Then

                    'If Registry Exists and is Valid
                    If rBinPath <> "" AndAlso File.Exists(doProperFileName(doResolveWildNames(rBinPath & "\bin\" & ArchitectureString & "\R.dll"))) Then
                        rBinPath &= "\bin\" & ArchitectureString
                        My.Settings.RDirectory = rBinPath
                        My.Settings.Save()

                    Else
                        rBinPath = ""

                        Dim ProgrammesHeadParent As String = Application.StartupPath
                        Do Until ProgrammesHeadParent.Length = 3
                            ProgrammesHeadParent = Directory.GetParent(ProgrammesHeadParent).FullName
                        Loop

                        Dim PossibleRFolder() As String = {"%ProgramFiles%\R\", "%ProgramFiles(x86)%\R\", "C:\Programs\R", "C:\Programmes\R", "C:\Program Files\R\", "C:\Progs\R\",
                                                           ProgrammesHeadParent & "ProgramFiles\R\", ProgrammesHeadParent & "ProgramFiles(x86)\R\", ProgrammesHeadParent & "Programs\R", ProgrammesHeadParent & "Programmes\R", ProgrammesHeadParent & "Program Files\R\", ProgrammesHeadParent & "Progs\R\"}
                        Dim SubFolders() As String = {}

                        For Each Folder In PossibleRFolder
                            If Directory.Exists(doProperPathName(doResolveWildNames(Folder))) Then
                                SubFolders = Directory.GetDirectories(doProperPathName(doResolveWildNames(Folder)))
                                Exit For
                            End If
                        Next

                        Dim R_Path As String = ""
                        Dim R_File As String = ""
                        Dim InstallPath As String = ""
                        Dim R_VersionFolderNameAlone As String = ""

                        If SubFolders.Length > 0 Then
                            R_Path = SubFolders(SubFolders.Length - 1) & "\bin\" & ArchitectureString
                            If File.Exists(R_Path & "\R.dll") Then
                                R_File = R_Path & "\R.dll"
                                InstallPath = SubFolders(SubFolders.Length - 1)
                                R_VersionFolderNameAlone = GetFolderNameAlone(InstallPath).Replace("R", "").Replace("r", "").Replace("-", "")
                            End If
                        End If

                        If File.Exists(R_File) Then
                            rBinPath = R_Path

                        Else
                            Dim fbdRPath As New FolderBrowserDialog
                            Do While rBinPath = ""
                                fbdRPath.SelectedPath = ""
                                fbdRPath.Description = strModLanguage(162) 'Please locate the R folder on your computer, usually on C:\Program Files\R\

                                Dim FindRForm As New frmFindR
                                Dim FindRResult As DialogResult = FindRForm.ShowDialog

                                If FindRResult = DialogResult.Cancel Then
                                    Return False

                                ElseIf FindRResult = DialogResult.Retry Then
                                    Return RDotNet_Initialization()

                                Else
                                    If fbdRPath.ShowDialog() = DialogResult.Cancel Then
                                        Return False

                                    Else
                                        R_Path = fbdRPath.SelectedPath
                                        Dim SelectedDirectorySubdirectories As String() = Directory.GetDirectories(R_Path)

                                        If File.Exists(R_Path & "\bin\" & ArchitectureString & "\R.dll") Then
                                            InstallPath = R_Path
                                            R_VersionFolderNameAlone = GetFolderNameAlone(InstallPath).Replace("R", "").Replace("r", "").Replace("-", "")
                                            R_Path &= "\bin\" & ArchitectureString
                                            rBinPath = R_Path
                                            My.Settings.RDirectory = rBinPath
                                            My.Settings.Save()

                                        ElseIf File.Exists(R_Path & "\" & ArchitectureString & "\R.dll") Then
                                            InstallPath = Directory.GetParent(R_Path).Name
                                            R_VersionFolderNameAlone = GetFolderNameAlone(InstallPath).Replace("R", "").Replace("r", "").Replace("-", "")
                                            R_Path &= "\" & ArchitectureString
                                            rBinPath = R_Path
                                            My.Settings.RDirectory = rBinPath
                                            My.Settings.Save()

                                        ElseIf File.Exists(R_Path & "\R.dll") Then
                                            InstallPath = Directory.GetParent(R_Path).Parent.Name
                                            R_VersionFolderNameAlone = GetFolderNameAlone(InstallPath).Replace("R", "").Replace("r", "").Replace("-", "")
                                            rBinPath = R_Path
                                            My.Settings.RDirectory = rBinPath
                                            My.Settings.Save()

                                        ElseIf SelectedDirectorySubdirectories.Length > 0 AndAlso File.Exists(SelectedDirectorySubdirectories(SelectedDirectorySubdirectories.Length - 1) & "\bin\" & ArchitectureString & "\R.dll") Then
                                            InstallPath = SelectedDirectorySubdirectories(SelectedDirectorySubdirectories.Length - 1)
                                            R_VersionFolderNameAlone = GetFolderNameAlone(InstallPath).Replace("R", "").Replace("r", "").Replace("-", "")
                                            R_Path = InstallPath & "\bin\" & ArchitectureString
                                            rBinPath = R_Path
                                            My.Settings.RDirectory = rBinPath
                                            My.Settings.Save()

                                        ElseIf Directory.Exists(R_Path & "\R") Then
                                            Dim RSubDirectories As String() = Directory.GetDirectories(R_Path & "\R")

                                            If RSubDirectories.Length > 0 Then
                                                InstallPath = RSubDirectories(RSubDirectories.Length - 1)
                                                R_VersionFolderNameAlone = GetFolderNameAlone(InstallPath).Replace("R", "").Replace("r", "").Replace("-", "")
                                                R_Path = InstallPath & "\bin\" & ArchitectureString
                                                rBinPath = R_Path
                                                My.Settings.RDirectory = rBinPath
                                                My.Settings.Save()
                                            End If

                                        Else
                                            MsgBox(strModLanguage(162) & vbCrLf & strModLanguage(163), MsgBoxStyle.Exclamation) 'The folder you've chosen is not the R's Installation folder.
                                        End If


                                    End If
                                End If
                            Loop

                            R_File = R_Path & "\R.dll"

                        End If

                        '32bit
                        Try
                            Registry.LocalMachine.DeleteSubKeyTree("Software\R-core")
                        Catch ex As Exception
                        End Try

                        Registry.LocalMachine.CreateSubKey("Software\R-core")

                        Registry.LocalMachine.CreateSubKey("Software\R-core\R")
                        Registry.LocalMachine.OpenSubKey("Software\R-core\R", True).SetValue("Current Version", R_VersionFolderNameAlone, RegistryValueKind.String)
                        Registry.LocalMachine.OpenSubKey("Software\R-core\R", True).SetValue("InstallPath", InstallPath, RegistryValueKind.String)
                        Registry.LocalMachine.CreateSubKey("Software\R-core\R\" & R_VersionFolderNameAlone)
                        Registry.LocalMachine.OpenSubKey("Software\R-core\R\" & R_VersionFolderNameAlone, True).SetValue("InstallPath", InstallPath, RegistryValueKind.String)

                        Registry.LocalMachine.CreateSubKey("Software\R-core\R64")
                        Registry.LocalMachine.OpenSubKey("Software\R-core\R64", True).SetValue("Current Version", R_VersionFolderNameAlone, RegistryValueKind.String)
                        Registry.LocalMachine.OpenSubKey("Software\R-core\R64", True).SetValue("InstallPath", InstallPath, RegistryValueKind.String)
                        Registry.LocalMachine.CreateSubKey("Software\R-core\R64\" & R_VersionFolderNameAlone)
                        Registry.LocalMachine.OpenSubKey("Software\R-core\R64\" & R_VersionFolderNameAlone, True).SetValue("InstallPath", InstallPath, RegistryValueKind.String)

                        '64bit
                        Try
                            Registry.LocalMachine.DeleteSubKeyTree("Software\Wow6432Node\R-core")
                        Catch ex As Exception
                        End Try

                        Registry.LocalMachine.CreateSubKey("Software\Wow6432Node\R-core")

                        Registry.LocalMachine.CreateSubKey("Software\Wow6432Node\R-core\R")
                        Registry.LocalMachine.OpenSubKey("Software\Wow6432Node\R-core\R", True).SetValue("Current Version", R_VersionFolderNameAlone, RegistryValueKind.String)
                        Registry.LocalMachine.OpenSubKey("Software\Wow6432Node\R-core\R", True).SetValue("InstallPath", InstallPath, RegistryValueKind.String)
                        Registry.LocalMachine.CreateSubKey("Software\Wow6432Node\R-core\R\" & R_VersionFolderNameAlone)
                        Registry.LocalMachine.OpenSubKey("Software\Wow6432Node\R-core\R\" & R_VersionFolderNameAlone, True).SetValue("InstallPath", InstallPath, RegistryValueKind.String)

                        Registry.LocalMachine.CreateSubKey("Software\Wow6432Node\R-core\R32")
                        Registry.LocalMachine.OpenSubKey("Software\Wow6432Node\R-core\R32", True).SetValue("Current Version", R_VersionFolderNameAlone, RegistryValueKind.String)
                        Registry.LocalMachine.OpenSubKey("Software\Wow6432Node\R-core\R32", True).SetValue("InstallPath", InstallPath, RegistryValueKind.String)
                        Registry.LocalMachine.CreateSubKey("Software\Wow6432Node\R-core\R32\" & R_VersionFolderNameAlone)
                        Registry.LocalMachine.OpenSubKey("Software\Wow6432Node\R-core\R32\" & R_VersionFolderNameAlone, True).SetValue("InstallPath", InstallPath, RegistryValueKind.String)

                    End If

                Else
                    rBinPath = My.Settings.RDirectory
                End If

                Environment.SetEnvironmentVariable("PATH", envPath & Path.PathSeparator & rBinPath)
                Rdo = REngine.CreateInstance("RDotNet")
                Rdo.Initialize()

            End If

            My.Settings.Save()
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

#Region "RSource"
    Public Sub RSource(ByVal FilePaths() As String, Optional ByVal CypherLevel As Integer = -1, Optional ByVal lstReplace() As String = Nothing, Optional ByVal KillGraphsFirst As Boolean = False)
        Dim rnd As New Random()
        Dim strAuxiliaryFile As String = strFunctions & "AuxiliaryFile" & rnd.Next(0, MaxInteger)
        Dim sbTemp As New StringBuilder

        If KillGraphsFirst Then Rdo.Evaluate("graphics.off()")

        For Each FilePath In FilePaths
            sbTemp.AppendLine(ReadFile(FilePath, "", , CypherLevel))
            sbTemp.AppendLine()
        Next

        If lstReplace IsNot Nothing AndAlso lstReplace.Length > 1 Then
            For i = 0 To lstReplace.Count - 1 Step 2
                sbTemp.Replace(lstReplace(i), lstReplace(i + 1))
            Next
        End If

        WriteText(strAuxiliaryFile, sbTemp.ToString, Encoding.Default)
        Rdo.Evaluate("source(""" & strAuxiliaryFile.Replace("\", "\\") & """)")
        DelFileFolder(strAuxiliaryFile, False, 100)
    End Sub
    Public Sub RSource(ByVal FilePath As String, Optional ByVal CypherLevel As Integer = -1, Optional ByVal strArReplace() As String = Nothing, Optional ByVal KillGraphsFirst As Boolean = False)
        Dim rnd As New Random()
        Dim tmptext As String = ReadFile(FilePath, "", , CypherLevel)
        Dim strAuxiliaryFile As String = strFunctions & "AuxiliaryFile" & rnd.Next(0, MaxInteger) & ".R"

        If KillGraphsFirst Then Rdo.Evaluate("graphics.off()")

        If strArReplace IsNot Nothing AndAlso strArReplace.Length > 1 Then
            For i As Integer = 0 To strArReplace.Count - 1 Step 2
                tmptext = tmptext.Replace(strArReplace(i), strArReplace(i + 1))
            Next
        End If

        WriteText(strAuxiliaryFile, tmptext, Encoding.Default)
        Rdo.Evaluate("source(""" & strAuxiliaryFile.Replace("\", "\\") & """)")
        DelFileFolder(strAuxiliaryFile, False, 100)
    End Sub
#End Region

#Region "Aux Functions"
    Public Sub DownloadRLibraries(ByVal LibraryNames As String(), Optional ByVal ForceDownload As Boolean = False)
        Dim DownloadedLibraries As List(Of String) = ReadFile(strFunctions & "Downloaded Libraries.ini", {}).ToList

        For Each LibraryName In LibraryNames
            If ForceDownload OrElse FindIndex(LibraryName, DownloadedLibraries.ToList) = -1 Then
                Rdo.Evaluate("install.packages(""" & LibraryName & """,.libPaths(), ""http://cran.us.r-project.org"")")
                DownloadedLibraries.Add(LibraryName)
            End If
        Next

        WriteText(strFunctions & "Downloaded Libraries.ini", ArrayBox(DownloadedLibraries), Encoding.UTF8)
    End Sub

    Public Sub Clear_Aux_Files()
        Dim FilesFullNames As List(Of String) = Directory.GetFiles(strFunctions).ToList
        Dim FilesBareNames As New List(Of String)

        For Each File In FilesFullNames
            FilesBareNames.Add(GetFileNameAlone(File))
        Next

        For i = 0 To FilesBareNames.Count - 1
            If FilesBareNames.Item(i).ToLower.StartsWith("auxiliaryfile") AndAlso IsNumeric(FilesBareNames.Item(i).Substring("auxiliaryfile".Length)) Then DelFileFolder(FilesFullNames.Item(i))
        Next
    End Sub

    Public Function MatrixToArArDouble(ByVal Matrix As NumericMatrix) As Double(,)
        Dim Cols As Integer = Matrix.ColumnCount
        Dim Rows As Integer = Matrix.RowCount
        Dim dblArArMatrix(,) As Double = DirectCast(Array.CreateInstance(GetType([Double]), Rows, Cols), [Double](,))
        Matrix.CopyTo(dblArArMatrix, Rows, Cols)

        Return dblArArMatrix
    End Function

    Public Function GetColumnFromArrayOfArray(Of T)(ByVal ArArVariable As T(,), ByVal Index As Integer) As List(Of T)
        Dim Result As New List(Of T)

        For i = 0 To ArArVariable.GetLength(0) - 1
            Result.Add(ArArVariable(i, Index))
        Next

        Return Result
    End Function

    Public Function GetRowFromArrayOfArray(Of T)(ByVal ArArVariable As T(,), ByVal Index As Integer) As List(Of T)
        Dim Result As New List(Of T)

        For i = 0 To ArArVariable.GetLength(0) - 1
            Result.Add(ArArVariable(Index, i))
        Next

        Return Result
    End Function

    Public Function ArArDoubleToMatrix(ByVal ArArDouble As Double(,), Optional ByVal MatrixName As String = "tmpMatrix", Optional ByVal ColumnNames As List(Of String) = Nothing) As NumericMatrix
        Dim ReturnedMatrix As NumericMatrix = Rdo.CreateNumericMatrix(ArArDouble)

        Rdo.SetSymbol(MatrixName, ReturnedMatrix)

        If ColumnNames IsNot Nothing Then
            Rdo.SetSymbol("tmpColNames", Rdo.CreateCharacterVector(ColumnNames.ToArray))
            Rdo.Evaluate("colnames (" & MatrixName & ") = tmpColNames")
        End If

        Return ReturnedMatrix
    End Function

    Public Function datatableColumnsToArArDouble(ByVal dtVariables As DataTable, ByVal ColumnIndices As List(Of Integer), Optional ByRef ColumnNames As List(Of String) = Nothing) As Double(,)
        Dim Result(dtVariables.Rows.Count - 1, ColumnIndices.Count - 1) As Double

        For i As Integer = 0 To ColumnIndices.Count - 1
            ColumnNames.Add(dtVariables.Columns(ColumnIndices.Item(i)).ColumnName)
            For j = 0 To dtVariables.Rows.Count - 1
                If dtVariables.Rows(j).Item(ColumnIndices.Item(i)) IsNot Nothing AndAlso Not IsDBNull(dtVariables.Rows(j).Item(ColumnIndices.Item(i))) Then Result(j, i) = CDbl(dtVariables.Rows(j).Item(ColumnIndices.Item(i))) Else Exit For
            Next
        Next

        Return Result
    End Function
#End Region

End Module
