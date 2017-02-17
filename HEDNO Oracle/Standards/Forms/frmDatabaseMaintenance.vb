'Version 1.0.2 2013-03-29

Public Class frmDatabaseMaintenance
    Public strLanguage_DatabaseMaintenance() As String
    Public strLanguage_DatabaseMaintenance_Tips() As String


    Private Sub frmDatabaseMaintenance_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
        Try
            Call DatabaseMaintenance_Language(Me)
            frmSkin(Me, False)

            If CurDBType = DBType.SQLServer Then
                txtConnectionString.Text = SQLServerConnStr

            ElseIf CurDBType = DBType.Access97to2003 OrElse CurDBType = DBType.Access2007to2016 Then
                txtConnectionString.Text = LoadAccessDBConnStr(CurDBType, AccessDataBaseFile, AccessDatabasePass, AccessDBSource, AccessDBProvider)

            ElseIf CurDBType = DBType.None Then
                MsgBox("This programme does not have a Database!")
                gbMaintenanceOptions.Enabled = False
            End If

            '=============================
            '==End of Standard Procedure==
            '=============================


        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

#Region "Standard Procedure"
    Private Sub btnCompactDB_Click(sender As Object, e As EventArgs) Handles btnCompactDB.Click
        Select Case CurDBType
            Case DBType.Access97to2003, DBType.Access2007to2016
                If Not isAccessDBBusy Then
                    If MsgBox(strLanguage_DatabaseMaintenance(7), MsgBoxStyle.YesNoCancel) = vbYes Then 'Are you sure you want to compact the database?
                        btnCompactDB.Enabled = False
                        LoadAccessDBConnStr(CurDBType, AccessDataBaseFile, AccessDatabasePass, AccessDBSource, AccessDBProvider)
                        If CompactAccessDB(AccessDataBaseFile, AccessDBProvider, CurDBType, AccessDatabasePass, True) = True Then
                            MsgBox(strLanguage_DatabaseMaintenance(8), MsgBoxStyle.Information)  'The database has been compacted successfully
                        Else
                            MsgBox(strLanguage_DatabaseMaintenance(9), MsgBoxStyle.Exclamation)  'The database has not been compacted successfully
                        End If
                        btnCompactDB.Enabled = True
                    End If
                Else
                    MsgBox(strLanguage_DatabaseMaintenance(10), MsgBoxStyle.Exclamation) 'The Database file is currently busy, please finish any pending process and try again.
                End If


            Case DBType.SQLServer
                If MsgBox(strLanguage_DatabaseMaintenance(7), MsgBoxStyle.YesNoCancel) = vbYes Then 'Are you sure you want to compact the database?
                    btnCompactDB.Enabled = False

                    If ShrinkSQLDatabase() Then
                        MsgBox(strLanguage_DatabaseMaintenance(8), MsgBoxStyle.Information)  'The database has been compacted successfully
                    Else
                        MsgBox(strLanguage_DatabaseMaintenance(9), MsgBoxStyle.Exclamation)  'The database has not been compacted successfully
                    End If

                    btnCompactDB.Enabled = True
                End If


        End Select

    End Sub

    Private Sub btnClearAll_Click(sender As System.Object, e As EventArgs) Handles btnClearAll.Click
        Select Case CurDBType
            Case DBType.Access97to2003, DBType.Access2007to2016
                If Not isAccessDBBusy Then
                    If MsgBox(strLanguage_DatabaseMaintenance(11) & vbCrLf & strLanguage_DatabaseMaintenance(12), MsgBoxStyle.YesNoCancel) = vbYes Then 'Are you sure you want to clear the whole database?
                        btnClearAll.Enabled = False
                        If ClearAccessDatabase(DatabaseTables.ToArray, ProtectedTables.ToArray, AccessDataBaseFile, AccessDatabasePass) = True Then
                            MsgBox(strLanguage_DatabaseMaintenance(13), MsgBoxStyle.Information)  'Database is now Cleaned
                        Else
                            MsgBox(strLanguage_DatabaseMaintenance(14), MsgBoxStyle.Exclamation)  'The database could not be cleared
                        End If
                        btnClearAll.Enabled = True
                    End If
                Else
                    MsgBox(strLanguage_DatabaseMaintenance(10), MsgBoxStyle.Exclamation) 'The Database file is currently busy, please finish any pending process and try again.
                End If


            Case DBType.SQLServer
                If MsgBox(strLanguage_DatabaseMaintenance(11) & vbCrLf & strLanguage_DatabaseMaintenance(12), MsgBoxStyle.YesNoCancel) = vbYes Then 'Are you sure you want to clear the whole database?
                    btnClearAll.Enabled = False
                    If ClearSQLDatabase() = True Then
                        MsgBox(strLanguage_DatabaseMaintenance(13), MsgBoxStyle.Information)  'Database is now Cleaned
                    Else
                        MsgBox(strLanguage_DatabaseMaintenance(14), MsgBoxStyle.Exclamation)  'The database could not be cleared
                    End If
                    btnClearAll.Enabled = True
                End If

        End Select
    End Sub

    Private Sub btnClearProds_Click(sender As System.Object, e As EventArgs) Handles btnClearSingleTable.Click
        Select Case CurDBType
            Case DBType.Access97to2003, DBType.Access2007to2016
                If Not isAccessDBBusy Then
                    If DatabaseTables.Count > 0 Then
                        Dim TableIndex As Integer = -1
                        Dim TableText As String = String.Empty

                        If TypeBox(strLanguage_DatabaseMaintenance(21), DatabaseTables, TableIndex, TableText, False) Then 'Please Select which table to erase:
                            If MsgBox(strLanguage_DatabaseMaintenance(15) & DatabaseTables(TableIndex) & strLanguage_DatabaseMaintenance(16), MsgBoxStyle.YesNoCancel) = vbYes Then 'Are you sure you want to clear the "Products" table?
                                btnClearSingleTable.Enabled = False
                                If ClearAccessTable(TableIndex) = True Then
                                    MsgBox(strLanguage_DatabaseMaintenance(17) & DatabaseTables(TableIndex) & strLanguage_DatabaseMaintenance(18), MsgBoxStyle.Information)  'The "x" table has been cleared
                                Else
                                    MsgBox(strLanguage_DatabaseMaintenance(17) & DatabaseTables(TableIndex) & strLanguage_DatabaseMaintenance(19), MsgBoxStyle.Exclamation)  'The "x" table has not been cleared
                                End If
                                btnClearSingleTable.Enabled = True
                            End If
                        End If

                    Else
                        MsgBox(strLanguage_DatabaseMaintenance(20), MsgBoxStyle.Critical) 'There are no tables in the settings file, hence no table can be cleared.
                    End If

                Else
                    MsgBox(strLanguage_DatabaseMaintenance(10), MsgBoxStyle.Exclamation) 'The Database file is currently busy, please finish any pending process and try again.
                End If

            Case DBType.SQLServer
                If DatabaseTables.Count > 0 Then
                    Dim TableIndex As Integer = -1
                    Dim TableText As String = String.Empty

                    If TypeBox(strLanguage_DatabaseMaintenance(21), DatabaseTables, TableIndex, TableText, False) Then 'Please Select which table to erase:
                        If MsgBox(strLanguage_DatabaseMaintenance(15) & DatabaseTables(TableIndex) & strLanguage_DatabaseMaintenance(16), MsgBoxStyle.YesNoCancel) = vbYes Then 'Are you sure you want to clear the "Products" table?
                            btnClearSingleTable.Enabled = False
                            If ClearSQLTable(DatabaseTables(TableIndex), ProtectedTables.ToArray) = True Then
                                MsgBox(strLanguage_DatabaseMaintenance(17) & DatabaseTables(TableIndex) & strLanguage_DatabaseMaintenance(18), MsgBoxStyle.Information)  'The "x" table has been cleared
                            Else
                                MsgBox(strLanguage_DatabaseMaintenance(17) & DatabaseTables(TableIndex) & strLanguage_DatabaseMaintenance(19), MsgBoxStyle.Exclamation)  'The "x" table has not been cleared
                            End If
                            btnClearSingleTable.Enabled = True
                        End If
                    End If

                Else
                    MsgBox(strLanguage_DatabaseMaintenance(20), MsgBoxStyle.Critical) 'There are no tables in the settings file, hence no table can be cleared.
                End If
        End Select
    End Sub

    Private Sub btnExit_Click(sender As System.Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub

    Public Sub ChangeConnectionString()
        Dim ConnectionString As String = ""
        If TypeBox("Please type in the connection string to the " & DatabaseName & "SQL Database", ReturnedString:=ConnectionString, AllowNothingAsResult:=False) Then

            If CurDBType = DBType.SQLServer Then
                SQLServerConnStr = ConnectionString
                If strSettings(40) <> "040SQLServerConnStr=" & SQLServerConnStr Then
                    strSettings(40) = "040SQLServerConnStr=" & SQLServerConnStr
                    WriteSettings(strSettings, "ConnectToSQLServer")
                End If

            ElseIf CurDBType = DBType.Access97to2003 OrElse CurDBType = DBType.Access2007to2016 Then
                Dim tmp As List(Of String) = GetFromAccessDBConnStr(ConnectionString)

                strSettings(17) = "017DataBasePass=" & tmp(2)
                AccessDatabasePass = tmp(2)

                CurDBType = GetAccessDBType(tmp(0))
                strSettings(15) = "015DBType=" & CInt(CurDBType)

                If tmp(1).ToLower = strDefDBFile.ToLower Then
                    strSettings(18) = "018DataBaseDir=Default"
                    AccessDataBaseFile = strDefDBFile
                Else
                    strSettings(18) = "018DataBaseDir=" & doUnresolveWildNames(tmp(1))
                    AccessDataBaseFile = tmp(1)
                End If

                WriteSettings(strSettings, "ChangeConnectionString()")

            End If

            If MsgBox("You need to restart the application for the changes to take full effect" & vbCrLf & "Would you like to restart now?", MsgBoxStyle.YesNo) = vbYes Then
                RestartApplication()
                Exit Sub
            End If

        End If

    End Sub

    Private Sub txtConnectionString_Click(sender As Object, e As EventArgs) Handles txtConnectionString.MouseClick
        Call ChangeConnectionString()
    End Sub

    Private Sub txtConnectionString_KeyUp(sender As Object, e As KeyEventArgs) Handles txtConnectionString.KeyUp
        If e.KeyCode = Keys.Enter Then
            Call ChangeConnectionString()
        End If
    End Sub



#End Region

    '=============================
    '==End of Standard Procedure==
    '=============================


End Class