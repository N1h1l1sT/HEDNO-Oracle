'Version 1.3 2016-09-08
'Implemented code for SQL Database
'Needs Settings v1.6; ModuleStrings v1.4

'Updating a SQL Table
'SQLAdapter (<SQL>, SQLConn)
'SQLcmdBuilder(SQLAdapter)
'SQLAdapter.Fill(dt)
'dt.BlahBlah
'SQLAdapter.Update(dt)
'SQLAdapterDispose

Imports System.IO
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient


Public Enum DBType As Integer
    None = -1
    Access97to2003 = 0
    Access2007to2016 = 1
    SQLServer = 2
End Enum

Module modDatabase
#Region "Constants and Variables"

#Region "Global Vars"
    '=========================
    '==Changes per Programme==
    '=========================
    Public CurDBType As Integer = DBType.None
    '/=======================\
    '/=Changes per Programme=\
    '/=======================\
    Public DatabaseTables As List(Of String) ' {"Purchased", "Deleted", "Returned", "Products", "Customers", "Discounts"}
    Public ProtectedTables As List(Of String) '{"Products", "Customers"}
#End Region

#Region "SQL Server Vars"
    Public SQLServerConnStr As String = String.Empty
    Public strTablesSchemaFile As String = strDatabaseDir & "Tables.JSON"
    Public SQLConn As SqlConnection = Nothing
    Public ServerName As String = "'" & My.Computer.Name & "'"
    Public DatabaseName As String = My.Application.Info.AssemblyName.Replace(" ", "_")
    Public OriginalDatabaseName As String = My.Application.Info.AssemblyName.Replace(" ", "_")
    Public IntegratedSecurity As String = ""
    Public SQLServerUserID As String = ""
    Public SQLServerPass As String = ""
    Public ServerList() As String = {
                                        "'{ComputerName}\SQLEXPRESS'".Replace("{ComputerName}", My.Computer.Name),
                                        "'{ComputerName}'".Replace("{ComputerName}", My.Computer.Name),
                                        "'{ComputerName}\{ComputerUserName}'".Replace("{ComputerName}\{ComputerUserName}", My.User.Name),
                                        "'(localdb)\v11.0'",
                                        "'(localdb)\v12.0'",
                                        "'(localdb)\v13.0'",
                                        "'(localdb)\MSSQLLocalDB'",
                                        "'(localdb)\SQLEXPRESS'",
                                        "'.\MSSQLLocalDB'",
                                        "'.\SQLEXPRESS'"
                                    }
#End Region

#Region "Access Vars"
    Public isAccessDBBusy As Boolean

    Public AccessDBProvider As String = "PROVIDER=Microsoft.ACE.OLEDB.12.0;"
    Public OleDbConnection As New OleDb.OleDbConnection
    Public AccessDBSource As String

    Public AccessDataBaseFile As String
    Public AccessDataBaseFileRarred As String = strDatabaseDir & "Database.rar"
    Public AccessDatabasePass As String
    Public AccessAutoSplitDatabase As Boolean
    Public AccessLastDatabaseSplit As String
    Public AccessCompactDBOnStartup As Boolean
#End Region

#End Region

#Region "Global Functions"
    Public Function LoadDatabaseTableNames() As Boolean
        Dim Successful As Boolean = True

        If strSettings(19).Substring("019DataBaseTables=".Length) <> "" Then
            DatabaseTables = ReadList(strSettings(19).Substring("019DataBaseTables=".Length), False, True)
        Else
            Successful = False
            MsgBox(strModLanguage(80)) 'No tables found on the settings file
        End If

        Return Successful
    End Function

#End Region

#Region "SQL Server Only Functions"

#Region "Get/GetFrom/Set SQLConnectionString"
    Public Function GetSQLConnectionString(Optional ByVal Server As String = "", Optional ByVal Database As String = "", Optional ByVal IntegratedSecurity As String = "",
                                           Optional ByVal SQLUserID As String = "", Optional ByVal SQLPass As String = "", Optional ByVal LeaveDatabaseBlankIfBlank As Boolean = False,
                                           Optional ByVal ConnectionTimeout As Integer = 2) As String
        Dim Result As String = ""

        If Server = "" Then Server = ServerName
        If Not LeaveDatabaseBlankIfBlank AndAlso Database = "" Then Database = DatabaseName

        Result = "Server = " & Server & "; "
        If Database <> "" Then Result &= "Database = " & Database & "; "

        If IntegratedSecurity = "" Then
            Result &= " Integrated Security = True; "
        ElseIf IntegratedSecurity.ToLower().Trim() = "true" OrElse IntegratedSecurity.ToLower().Trim() = "yes" OrElse IntegratedSecurity.ToLower().Trim() = "sspi" Then
            Result &= " Integrated Security = " & IntegratedSecurity & "; "
        Else
            If SQLUserID <> "" Then Result &= "User Id = " & SQLUserID & "; "
            If SQLPass <> "" Then Result &= "Password = " & SQLPass & "; "
        End If

        Result &= "Connection Timeout = " & ConnectionTimeout.ToString & ";"

        Result = Result.Trim

        Return Result
    End Function

    Public Function GetFromSQLDBConnStr(ByVal SQLConnStr As String) As List(Of String)
        'Doesn't recognise Username/Password and other secondary parameters!
        Dim Result As New List(Of String)
        Dim IndexStart As Integer
        Dim Lngth As Integer

        IndexStart = SQLConnStr.ToLower.IndexOf("Server".ToLower)
        If IndexStart >= 0 Then
            IndexStart += SQLConnStr.Substring(IndexStart).IndexOf("=") + "=".Length
            Lngth = SQLConnStr.Substring(IndexStart + "=".Length).IndexOf(";") + ";".Length
            If Lngth >= 0 Then
                Result.Add(SQLConnStr.Substring(IndexStart, Lngth).Trim())
            Else
                Result.Add("")
            End If
        Else
            Result.Add("")
        End If

        IndexStart = SQLConnStr.ToLower.IndexOf("Database".ToLower)
        If IndexStart >= 0 Then
            IndexStart += SQLConnStr.Substring(IndexStart).IndexOf("=") + "=".Length
            Lngth = SQLConnStr.Substring(IndexStart + "=".Length).IndexOf(";") + ";".Length
            If Lngth >= 0 Then
                Result.Add(SQLConnStr.Substring(IndexStart, Lngth).Trim())
            Else
                Result.Add("")
            End If
        Else
            Result.Add("")
        End If

        IndexStart = SQLConnStr.ToLower.IndexOf("Integrated Security".ToLower)
        If IndexStart >= 0 Then
            IndexStart += SQLConnStr.Substring(IndexStart).IndexOf("=") + "=".Length
            Lngth = SQLConnStr.Substring(IndexStart + "=".Length).IndexOf(";") + ";".Length
            If Lngth >= 0 Then
                Result.Add(SQLConnStr.Substring(IndexStart, Lngth).Trim())
            Else
                Result.Add("")
            End If
        Else
            Result.Add("")
        End If

        IndexStart = SQLConnStr.ToLower.IndexOf("User Id".ToLower)
        If IndexStart >= 0 Then
            IndexStart += SQLConnStr.Substring(IndexStart).IndexOf("=") + "=".Length
            Lngth = SQLConnStr.Substring(IndexStart + "=".Length).IndexOf(";") + ";".Length
            If Lngth >= 0 Then
                Result.Add(SQLConnStr.Substring(IndexStart, Lngth).Trim())
            Else
                Result.Add("")
            End If
        Else
            Result.Add("")
        End If

        IndexStart = SQLConnStr.ToLower.IndexOf("Password".ToLower)
        If IndexStart >= 0 Then
            IndexStart += SQLConnStr.Substring(IndexStart).IndexOf("=") + "=".Length
            Lngth = SQLConnStr.Substring(IndexStart + "=".Length).IndexOf(";") + ";".Length
            If Lngth >= 0 Then
                Result.Add(SQLConnStr.Substring(IndexStart, Lngth).Trim())
            Else
                Result.Add("")
            End If
        Else
            Result.Add("")
        End If

        IndexStart = SQLConnStr.ToLower.IndexOf("Connection Timeout".ToLower)
        If IndexStart >= 0 Then
            IndexStart += SQLConnStr.Substring(IndexStart).IndexOf("=") + "=".Length
            Lngth = SQLConnStr.Substring(IndexStart + "=".Length).IndexOf(";") + ";".Length
            If Lngth >= 0 Then
                Result.Add(SQLConnStr.Substring(IndexStart, Lngth).Trim())
            Else
                Result.Add("")
            End If
        Else
            Result.Add("")
        End If

        Return Result
    End Function

    Public Sub SetSQLConnectionString(ByVal DontSetIfValueEmpty As Boolean, ByVal ConnString As String)
        UpdateSQLServerVars(DontSetIfValueEmpty, ConnString)
        SQLServerConnStr = ConnString
        If strSettings(40) <> "040SQLServerConnStr=" & ConnString Then
            strSettings(40) = "040SQLServerConnStr=" & ConnString
            WriteSettings(strSettings, "SetSQLConnectionString")
        End If
    End Sub
    Public Sub SetSQLConnectionString(ByVal DontSetIfValueEmpty As Boolean, ByVal Server As String, ByVal Database As String, Optional ByVal IntegrSecurity As String = "", Optional ByVal SQLUserID As String = "", Optional ByVal SQLPass As String = "")
        UpdateSQLServerVars(DontSetIfValueEmpty, Server, Database, IntegrSecurity, SQLUserID, SQLPass)
        SQLServerConnStr = GetSQLConnectionString(Server, Database, IntegrSecurity, SQLUserID, SQLPass)

        If strSettings(40) <> "040SQLServerConnStr=" & SQLServerConnStr Then
            strSettings(40) = "040SQLServerConnStr=" & SQLServerConnStr
            WriteSettings(strSettings, "SetSQLConnectionString")
        End If
    End Sub
#End Region

#Region "ConnectToSQLServer"

    Public Function ConnectToSQLServer(Optional ByVal CloseAtSuccessfulConn As Boolean = True) As Boolean
        Return ConnectToSQLServer(SQLConn, "", CloseAtSuccessfulConn)
    End Function
    Public Function ConnectToSQLServer(ByRef CurSQLConn As SqlConnection, Optional ByVal CloseAtSuccessfulConn As Boolean = True) As Boolean
        Return ConnectToSQLServer(CurSQLConn, "", CloseAtSuccessfulConn)
    End Function
    Public Function ConnectToSQLServer(ByVal SQLConnString As String, Optional ByVal CloseAtSuccessfulConn As Boolean = True) As Boolean
        Return ConnectToSQLServer(SQLConn, SQLConnString, CloseAtSuccessfulConn)
    End Function
    Public Function ConnectToSQLServer(ByRef CurSQLConn As SqlConnection, ByVal SQLConnString As String, Optional ByVal CloseAtSuccessfulConn As Boolean = True, Optional ByRef ErrorMessage As String = "") As Boolean
        Try
            If SQLConnString = "" Then
                If SQLServerConnStr <> "" Then
                    SQLConnString = SQLServerConnStr
                Else
                    SQLConnString = GetSQLConnectionString()
                End If
            End If

            If CurSQLConn Is Nothing Then CurSQLConn = New SqlConnection()

            Try
                'Trying the Default SQL Connection String (which includes the Programme's Database Name)
                If CurSQLConn.State <> ConnectionState.Closed Then CurSQLConn.Close()
                CurSQLConn = New SqlConnection(SQLConnString)
                CurSQLConn.Open()

                'If it succeeds to open a SQL Connection, then the SQL Server Vars are updated to reflect the accessible SQL Server instance
                Call UpdateSQLServerVars(False, SQLConnString) 'Don't use "SetSQLConnectionString" here because for the Conn String to be valid we need the table creation to also pass. Then the ConnString is saved on my.settings

                'Verifying Programme's SQL Tables exist, else create them
                If Not CreateDefaultTables(CurSQLConn) Then Throw New Exception("Failed to create the default Tables (1)")

                If CloseAtSuccessfulConn Then CurSQLConn.Close()
                SQLServerConnStr = SQLConnString
                If strSettings(40) <> "040SQLServerConnStr=" & SQLServerConnStr Then
                    strSettings(40) = "040SQLServerConnStr=" & SQLServerConnStr
                    WriteSettings(strSettings, "ConnectToSQLServer")
                End If
            Catch ex As Exception
                Try
                    'Trying the Different Server Names on the default SQL Connection String (which includes the Programme's Database Name)
                    Dim ConnSucceeded As Boolean = False
                    For Each PossibleServerName As String In ServerList
                        Try
                            SQLConnString = GetSQLConnectionString(PossibleServerName, DatabaseName,,,, True)
                            If CurSQLConn.State <> ConnectionState.Closed Then CurSQLConn.Close()
                            CurSQLConn = New SqlConnection(SQLConnString)
                            CurSQLConn.Open()
                            ConnSucceeded = True
                            Exit For
                        Catch ex3 As Exception
                        End Try
                    Next

                    If ConnSucceeded Then
                        'If it succeeds to open a SQL Connection, then the SQL Server Vars are updated to reflect the accessible SQL Server instance
                        Call UpdateSQLServerVars(False, SQLConnString)

                        'Verifying Programme's SQL Tables exist, else create them
                        If Not CreateDefaultTables(CurSQLConn) Then Throw New Exception("Failed to create the default Tables (2)") 'Since the connection succeeded with the Database name, we only check for the Table names

                        If CloseAtSuccessfulConn Then CurSQLConn.Close()
                        SQLServerConnStr = SQLConnString
                        If strSettings(40) <> "040SQLServerConnStr=" & SQLServerConnStr Then
                            strSettings(40) = "040SQLServerConnStr=" & SQLServerConnStr
                            WriteSettings(strSettings, "ChangeConnectionString")
                        End If
                    Else
                            Throw New Exception("SQL Server Connection Failed (2)")
                    End If

                Catch ex2 As Exception
                    Try
                        'Trying the Different Server Names on the default SQL Connection string with "Master" as the Server
                        Dim ConnSucceeded As Boolean = False
                        For Each PossibleServerName As String In ServerList
                            Try
                                SQLConnString = GetSQLConnectionString(PossibleServerName, "master")
                                If CurSQLConn.State <> ConnectionState.Closed Then CurSQLConn.Close()
                                CurSQLConn = New SqlConnection(SQLConnString)
                                CurSQLConn.Open()
                                ConnSucceeded = True
                                Exit For
                            Catch ex3 As Exception
                            End Try
                        Next

                        If ConnSucceeded = True Then
                            If Not CreateDefaultDatabase(CurSQLConn) Then Throw New Exception("Failed to create the default database (3)") 'The Connection didn't succeed with the Database name, so we create it
                            If Not CreateDefaultTables(CurSQLConn) Then Throw New Exception("Failed to create the default Tables (2)") 'and since the database wasn't created, neither are the tables
                            Dim WorkingServerName As String = GetFromSQLDBConnStr(SQLConnString)(0)
                            SQLConnString = GetSQLConnectionString(WorkingServerName, DatabaseName)
                            Call SetSQLConnectionString(False, SQLConnString) 'The one that really worked is the one with "master" as Database, but the default Database got created by the CreateDefaultDatabase, hence it is assumed that this connection string works now
                        Else
                            Throw New Exception("SQL Server Connection Failed (3)")
                        End If

                    Catch exc As Exception
                        'Trying to connect with User-Defined connection String
                        Dim Connected As Boolean = False
                        Do Until Connected = True
                            MsgBox("Unfortunately a connection to the SQL Server could not be established." & vbCrLf &
                               "This is probably because the connection string is neither the default one for Microsoft SQL Server, nor the one specified." & vbCrLf &
                               "Please try again with a different Connection String")
                            If TypeBox("Please type in the connection string to the '" & DatabaseName & "' SQL Database", ReturnedString:=SQLConnString, AllowNothingAsResult:=False) Then
                                Try
                                    'Attempting to create with everything BUT the database as user-defined
                                    Dim tmp As List(Of String) = GetFromSQLDBConnStr(SQLConnString)
                                    UpdateSQLServerVars(False, tmp(0), DatabaseName, tmp(2), tmp(3), tmp(4))
                                    If CurSQLConn.State <> ConnectionState.Closed Then CurSQLConn.Close()
                                    CurSQLConn = New SqlConnection(SQLConnString)
                                    CurSQLConn.Open()

                                    If Not CreateDefaultDatabase(CurSQLConn) Then Throw New Exception("Failed to create the default database")
                                    If Not CreateDefaultTables(CurSQLConn) Then Throw New Exception("Failed to create the default Tables (4)") 'and since the database wasn't created, neither are the tables
                                    If CloseAtSuccessfulConn Then CurSQLConn.Close()
                                    SetSQLConnectionString(False, SQLConnString)
                                    Connected = True
                                Catch exx As Exception
                                    Try
                                        'Attempting to create with EVERYTHING as user-defined
                                        Dim tmp As List(Of String) = GetFromSQLDBConnStr(SQLConnString)
                                        UpdateSQLServerVars(False, tmp(0), tmp(1), tmp(2), tmp(3), tmp(4))
                                        If CurSQLConn.State <> ConnectionState.Closed Then CurSQLConn.Close()
                                        CurSQLConn = New SqlConnection(SQLConnString)
                                        CurSQLConn.Open()

                                        If Not CreateDefaultDatabase(CurSQLConn) Then Throw New Exception("Failed to create the default database")
                                        If Not CreateDefaultTables(CurSQLConn) Then Throw New Exception("Failed to create the default Tables (5)") 'and since the database wasn't created, neither are the tables
                                        If CloseAtSuccessfulConn Then CurSQLConn.Close()
                                        SetSQLConnectionString(False, SQLConnString)
                                        Connected = True
                                    Catch ex4 As Exception
                                        CurSQLConn.Dispose()
                                        Connected = False
                                    End Try
                                End Try

                            Else
                                CurSQLConn.Dispose()
                                Return False
                            End If
                        Loop
                        Return Connected '"Return Connected" is needed because if the user cancels the TypeBox and this return isn't here, it's going to return True though it should have been false

                    End Try
                End Try
            End Try

            Return True
        Catch ex As Exception
            ErrorMessage = ex.ToString
            Return False
        End Try
    End Function

    Private Function CreateDefaultDatabase(ByVal SQLConn As SqlConnection) As Boolean
        Dim tmpTries As Integer = 0
        Dim Successful As Boolean = False
        Dim OriginalDatabaseName As String = DatabaseName

        Do Until (tmpTries >= 10 OrElse Successful = True)
            Try
                If tmpTries > 0 Then DatabaseName &= tmpTries
                Dim SQLCmd As New SqlCommand(
                    <SQL>
                        DECLARE @<%= DatabaseName %>DBName nvarchar(<%= DatabaseName.Length %>) = N'<%= DatabaseName %>'

                        IF (NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = @<%= DatabaseName %>DBName OR name = @<%= DatabaseName %>DBName)))
                        BEGIN
	                        CREATE DATABASE [<%= DatabaseName %>]
                        END
                    </SQL>.Value, SQLConn)
                SQLCmd.ExecuteNonQuery()
                SQLCmd = New SqlCommand(<SQL>
                                           USE [<%= DatabaseName %>]
                                        </SQL>.Value, SQLConn)
                SQLCmd.ExecuteNonQuery()
                Successful = True

            Catch ex As Exception
                DatabaseName = OriginalDatabaseName
                tmpTries += 1
                Successful = False
            End Try
        Loop

        Return Successful
    End Function

    Private Function CreateDefaultTables(Optional ByRef ErrorMessage As String = "") As Boolean
        Return CreateDefaultTables(SQLConn, DatabaseName, Nothing, ErrorMessage)
    End Function
    Private Function CreateDefaultTables(ByVal SQLConnect As SqlConnection, Optional ByRef ErrorMessage As String = "") As Boolean
        Return CreateDefaultTables(SQLConn, DatabaseName, Nothing, ErrorMessage)
    End Function
    Private Function CreateDefaultTables(ByVal SQLConn As SqlConnection, ByVal DatabaseName As String, ByVal TablesSchema As JObject, Optional ByRef ErrorMessage As String = "") As Boolean
        Dim Result As Boolean = False

        If TablesSchema Is Nothing Then
            If File.Exists(strTablesSchemaFile) Then
                Dim FileString As String = File.ReadAllText(strTablesSchemaFile)
                Dim tmpp As String = FileString.Replace(" ", "")
                If tmpp = "" OrElse tmpp = "{}" OrElse tmpp = "[]" OrElse tmpp = "{[]}" Then Return True

                Try
                    TablesSchema = JObject.Parse(FileString)
                Catch ex As Exception
                    Return False
                End Try

            Else
                Return True 'True because there's nothing to create.
            End If
        End If




        For i As Integer = 0 To TablesSchema("Tables").Count - 1
            Dim MyTableName As String = CStr(TablesSchema("Tables")(i)("TableName"))
            Dim MyTableSQLQuery As String = <SQL>
                                                USE <%= DatabaseName %>
                                                DECLARE @ThisTable nvarchar(<%= MyTableName.Length %>) = N'<%= MyTableName %>'
                                                IF (NOT EXISTS (SELECT TABLE_NAME FROM [<%= DatabaseName %>].INFORMATION_SCHEMA.TABLES WHERE ('[' + TABLE_NAME + ']' = @ThisTable OR TABLE_NAME = @ThisTable)))
                                                BEGIN
                                                    SET ANSI_NULLS ON
                                                    SET QUOTED_IDENTIFIER ON
                                                    CREATE TABLE [dbo].[<%= MyTableName %>](
                                            </SQL>.Value

            For j As Integer = 0 To TablesSchema("Tables")(i)("TableVars").Count - 1
                Dim MyColumnName As String = CStr(TablesSchema("Tables")(i)("TableVars")(j)("ColumnName"))
                Dim MyType As String = CStr(TablesSchema("Tables")(i)("TableVars")(j)("Type"))
                Dim MyDefault As String = CStr(TablesSchema("Tables")(i)("TableVars")(j)("Default"))
                Dim MyNullBehaviour As String = CStr(TablesSchema("Tables")(i)("TableVars")(j)("NullBehaviour"))
                Dim MyIdentity As String = CStr(TablesSchema("Tables")(i)("TableVars")(j)("Identity"))

                If MyColumnName = "" OrElse MyType = "" OrElse MyNullBehaviour = "" Then
                    ErrorMessage = "Error: Column Name, Variable Type and Null Behaviour must be set on the JSON!"
                    Return False

                Else
                    MyTableSQLQuery &= vbCrLf & "[" & MyColumnName & "] " & MyType & " "
                    If MyDefault <> "" Then MyTableSQLQuery &= "CONSTRAINT [DF_" & MyTableName & "] DEFAULT (" & MyDefault & ") "
                    If MyIdentity <> "" Then MyTableSQLQuery &= MyIdentity & " "
                    MyTableSQLQuery &= MyNullBehaviour
                    If j <> TablesSchema("Tables")(i)("TableVars").Count - 1 OrElse CStr(TablesSchema("Tables")(i)("PrimaryKey")("ColumnName")) <> "" Then MyTableSQLQuery &= ","
                End If
            Next


            Dim MyPKName As String = CStr(TablesSchema("Tables")(i)("PrimaryKey")("ColumnName"))
            Dim MyPKType As String = CStr(TablesSchema("Tables")(i)("PrimaryKey")("ClusterType"))
            Dim MyPKOrder As String = CStr(TablesSchema("Tables")(i)("PrimaryKey")("MyPKOrder"))

            If MyPKName <> "" Then
                MyTableSQLQuery &= vbCrLf & "CONSTRAINT [PK_" & MyTableName & "] PRIMARY KEY "
                If MyPKType <> "" Then MyTableSQLQuery &= MyPKType & " "
                MyTableSQLQuery &= "([" & MyPKName & "] "
                If MyPKOrder <> "" Then MyTableSQLQuery &= MyPKOrder
                MyTableSQLQuery &= ")"
            End If

            MyTableSQLQuery &= <SQL>
                                    ) ON [PRIMARY]
                                END
                            </SQL>.Value


            Result = ExecuteSQLQuery(SQLConn, MyTableSQLQuery)
            MyTableSQLQuery = String.Empty


            'Indexes
            If TablesSchema("Tables")(i)("Indexes").ToString() <> "" AndAlso TablesSchema("Tables")(i)("Indexes").Count > 0 Then
                For j As Integer = 0 To TablesSchema("Tables")(i)("Indexes").Count - 1
                    Dim MyIndexName As String = CStr(TablesSchema("Tables")(i)("Indexes")(j)("ColumnName"))
                    Dim MyIndexType As String = CStr(TablesSchema("Tables")(i)("Indexes")(j)("ClusterType"))
                    Dim MyIndexOrder As String = CStr(TablesSchema("Tables")(i)("Indexes")(j)("MyPKOrder"))
                    If MyIndexName <> "" Then
                        MyTableSQLQuery = <SQL>
                                        DECLARE @ThisTable nvarchar(<%= MyTableName.Length %>) = N'<%= MyTableName %>'
                                        IF (NOT EXISTS (SELECT TABLE_NAME FROM [<%= DatabaseName %>].INFORMATION_SCHEMA.TABLES WHERE ('[' + TABLE_NAME + ']' = @ThisTable OR TABLE_NAME = @ThisTable)))
                                        BEGIN
                                        </SQL>.Value

                        MyTableSQLQuery &= vbCrLf & "CREATE "
                        If MyIndexType <> "" Then MyTableSQLQuery &= MyIndexType & " "
                        MyTableSQLQuery &= "INDEX [IX_" & MyTableName & "]" &
                                            vbCrLf & "    ON [dbo].[" & MyTableName & "]([" & MyIndexName & "]"
                        If MyIndexOrder <> "" Then MyTableSQLQuery &= " " & MyIndexOrder
                        MyTableSQLQuery &= <SQL>
                                                );
                                                END
                                           </SQL>.Value

                        Result = Result AndAlso ExecuteSQLQuery(SQLConn, MyTableSQLQuery)
                    End If
                    MyTableSQLQuery = String.Empty

                Next
            End If

        Next

        Return Result
    End Function

    Private Sub UpdateSQLServerVars(ByVal DontSetIfValueEmpty As Boolean, ByVal Server As String, Database As String, ByVal IntegrSecurity As String, ByVal UserID As String, ByVal UserPass As String)
        If Server <> "" OrElse Not DontSetIfValueEmpty Then ServerName = Server
        If Database <> "" OrElse Not DontSetIfValueEmpty Then DatabaseName = Database
        If IntegrSecurity <> "" OrElse Not DontSetIfValueEmpty Then IntegratedSecurity = IntegrSecurity
        If UserID <> "" OrElse Not DontSetIfValueEmpty Then SQLServerUserID = UserID
        If UserPass <> "" OrElse Not DontSetIfValueEmpty Then SQLServerPass = UserPass
    End Sub

    Private Sub UpdateSQLServerVars(ByVal DontSetIfValueEmpty As Boolean, ByVal ConnString As String)
        'If it succeeds to open a SQL Connection, then the SQL Server Vars are updated to reflect the accessible SQL Server instance
        Dim SQLConnStringCredentials As List(Of String) = GetFromSQLDBConnStr(ConnString)
        If SQLConnStringCredentials(0) <> "" OrElse Not DontSetIfValueEmpty Then ServerName = SQLConnStringCredentials(0)
        If SQLConnStringCredentials(1) <> "" OrElse Not DontSetIfValueEmpty Then DatabaseName = SQLConnStringCredentials(1)
        If SQLConnStringCredentials(2) <> "" OrElse Not DontSetIfValueEmpty Then IntegratedSecurity = SQLConnStringCredentials(2)
        If SQLConnStringCredentials(3) <> "" OrElse Not DontSetIfValueEmpty Then SQLServerUserID = SQLConnStringCredentials(3)
        If SQLConnStringCredentials(4) <> "" OrElse Not DontSetIfValueEmpty Then SQLServerPass = SQLConnStringCredentials(4)
    End Sub
#End Region

#Region "DisconnectFromSQLServer"
    Public Function DisconnectFromSQLServer(Optional ByRef ErrorMessage As String = "") As Boolean
        Return DisconnectFromSQLServer(SQLConn, ErrorMessage)
    End Function
    Public Function DisconnectFromSQLServer(ByVal SQLConn As SqlConnection, Optional ByRef ErrorMessage As String = "") As Boolean
        Try
            SQLConn.Dispose()
            Return True

        Catch ex As Exception
            ErrorMessage = ex.ToString
            Return False
        End Try
    End Function
#End Region

#Region "ExecuteSQLQuery"
    Public Function ExecuteSQLQuery(ByVal QueryString As String) As Boolean
        Return ExecuteSQLQuery(SQLConn, QueryString)
    End Function
    Public Function ExecuteSQLQuery(ByVal SQLConn As SqlConnection, ByVal QueryString As String, Optional ByRef ErrorMessage As String = "") As Boolean
        Try
            'Making sure there's a SQLConn, else returning false as the Query cannot be executed
            If SQLConn IsNot Nothing Then
                'Provided there's a SQLConn, getting its state to restore it at the end.
                'If the connection is closed, we're temporarily opening it to execute the query
                Dim OriginalSQLConnState As ConnectionState = SQLConn.State
                If SQLConn.State <> ConnectionState.Open Then SQLConn.Open()

                Dim SQLCmd As New SqlCommand(QueryString, SQLConn)
                SQLCmd.ExecuteNonQuery()

                If OriginalSQLConnState = ConnectionState.Closed Then SQLConn.Close()
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            ErrorMessage = ex.ToString
            Return False
        End Try
    End Function
#End Region

#Region "ExecuteSQLQueryAsync"
    Public Async Function ExecuteSQLQueryAsync(ByVal QueryString As String) As Task(Of Boolean)
        Return Await ExecuteSQLQueryAsync(SQLConn, QueryString)
    End Function
    Public Async Function ExecuteSQLQueryAsync(ByVal SQLConn As SqlConnection, ByVal QueryString As String) As Task(Of Boolean)
        Try
            'Making sure there's a SQLConn, else returning false as the Query cannot be executed
            If SQLConn IsNot Nothing Then
                'Provided there's a SQLConn, getting its state to restore it at the end.
                'If the connection is closed, we're temporarily opening it to execute the query
                Dim OriginalSQLConnState As ConnectionState = SQLConn.State
                If SQLConn.State <> ConnectionState.Open Then SQLConn.Open()

                Dim SQLCmd As New SqlCommand(QueryString, SQLConn)
                Await SQLCmd.ExecuteNonQueryAsync()

                If OriginalSQLConnState = ConnectionState.Closed Then SQLConn.Close()
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "GetDataTableFromSQLTable"
    Public Function GetDataTableFromSQLTable(ByVal SQLTableName As String, Optional ByVal PrependDefaultServerToTable As Boolean = False, Optional ByVal TopCount As Integer = 0, Optional ByVal isPercent As Boolean = False, Optional ByRef ErrorMessage As String = "") As DataTable
        Return GetDataTableFromSQLTable(SQLConn, SQLTableName, PrependDefaultServerToTable, TopCount, isPercent, ErrorMessage)
    End Function
    Public Function GetDataTableFromSQLTable(ByVal SQLConn As SqlConnection, ByVal SQLTableName As String, Optional ByVal PrependDefaultServerToTable As Boolean = False, Optional ByVal TopCount As Integer = 0, Optional ByVal isPercent As Boolean = False, Optional ByRef ErrorMessage As String = "") As DataTable
        Dim Result As New DataTable

        Try
            'Making sure there's a SQLConn, else returning false as the Query cannot be executed
            If SQLConn IsNot Nothing Then
                'Provided there's a SQLConn, getting its state to restore it at the end.
                'If the connection is closed, we're temporarily opening it to execute the query
                Dim OriginalSQLConnState As ConnectionState = SQLConn.State
                If SQLConn.State <> ConnectionState.Open Then SQLConn.Open()

                Dim SQLAdapter As SqlDataAdapter
                Dim QueryString As String = "SELECT "

                If TopCount > 0 Then
                    QueryString &= "TOP " & TopCount.ToString & " "
                    If isPercent Then QueryString &= " PERCENT "
                End If

                If PrependDefaultServerToTable Then SQLTableName = "[" & DatabaseName & "].[dbo]." & SQLTableName
                QueryString &= "* FROM" & SQLTableName
                SQLAdapter = New SqlDataAdapter(QueryString, SQLConn)
                SQLAdapter.Fill(Result)

                If OriginalSQLConnState = ConnectionState.Closed Then SQLConn.Close()

            Else
                ErrorMessage = "There's no SQL Connection!"
            End If

        Catch ex As Exception
            ErrorMessage = ex.ToString
        End Try

        Return Result
    End Function
#End Region

#Region "GetDataTableFromSQLQuery"
    'ErrorMessage is not OPTIONAL because it's the only way to check if everything went alright, or if the resulting table is empty because of a Query error
    Public Function GetDataTableFromSQLQuery(ByVal QueryString As String, ByRef ErrorMessage As String) As DataTable
        Return GetDataTableFromSQLQuery(SQLConn, QueryString, ErrorMessage)
    End Function
    'ErrorMessage is not OPTIONAL because it's the only way to check if everything went alright, or if the resulting table is empty because of a Query error
    Public Function GetDataTableFromSQLQuery(ByVal SQLConn As SqlConnection, ByVal QueryString As String, ByRef ErrorMessage As String) As DataTable
        Dim Result As New DataTable

        Try
            'Making sure there's a SQLConn, else returning false as the Query cannot be executed
            If SQLConn IsNot Nothing Then
                'Provided there's a SQLConn, getting its state to restore it at the end.
                'If the connection is closed, we're temporarily opening it to execute the query
                Dim OriginalSQLConnState As ConnectionState = SQLConn.State
                If SQLConn.State <> ConnectionState.Open Then SQLConn.Open()

                Dim SQLAdapter As New SqlDataAdapter(QueryString, SQLConn)
                SQLAdapter.Fill(Result)

                If OriginalSQLConnState = ConnectionState.Closed Then SQLConn.Close()

            Else
                ErrorMessage = "There's no SQL Connection!"
            End If

        Catch ex As Exception
            ErrorMessage = ex.ToString
        End Try

        Return Result
    End Function
#End Region

#Region "ShrinkSQLDatabase"
    Public Function ShrinkSQLDatabase() As Boolean
        Return ShrinkSQLDatabase(SQLConn, DatabaseName)
    End Function
    Public Function ShrinkSQLDatabase(ByVal DBName As String) As Boolean
        Return ShrinkSQLDatabase(SQLConn, DBName)
    End Function
    Public Function ShrinkSQLDatabase(ByVal SQLConn As SqlConnection, ByVal DBName As String) As Boolean
        Try
            'Making sure there's a SQLConn, else returning false as the Query cannot be executed
            If SQLConn IsNot Nothing Then
                'Provided there's a SQLConn, getting its state to restore it at the end.
                'If the connection is closed, we're temporarily opening it to execute the query
                Dim OriginalSQLConnState As ConnectionState = SQLConn.State
                If SQLConn.State <> ConnectionState.Open Then SQLConn.Open()

                Dim SQLCmd As New SqlCommand(<SQL>DBCC SHRINKDATABASE ([<%= DBName %>], 10)</SQL>.Value, SQLConn)
                SQLCmd.ExecuteNonQuery()

                If OriginalSQLConnState = ConnectionState.Closed Then SQLConn.Close()
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#End Region

#Region "Access Only Functions"
    Public Function LoadAccessDBConnStr(ByVal DatabaseTypeID As Integer, ByVal AccessDataBaseFile As String, ByVal AccessDatabasePass As String, ByRef AccessDBSource As String, ByRef AccessDBProvider As String) As String  'Loads AccessDBSource and AccessDBProvider, and returns the Connection String is necessary
        Dim AccessExt As String = GetExt(AccessDataBaseFile)

        If DatabaseTypeID = DBType.Access2007to2016 AndAlso (AccessExt = ".mdb" OrElse AccessExt = ".accdb") Then
            AccessDBProvider = "PROVIDER=Microsoft.ACE.OLEDB.12.0;"
        ElseIf DatabaseTypeID = DBType.Access97to2003 AndAlso (AccessExt = ".mdb" OrElse AccessExt = ".accdb") Then
            AccessDBProvider = "PROVIDER=Microsoft.Jet.OLEDB.4.0;"
        Else
            AccessDBProvider = "Error: Unknown Database Provider due to Unknown Database Type"
        End If

        AccessDBSource = "Data Source = " & AccessDataBaseFile
        If AccessDatabasePass <> "" Then
            AccessDBSource &= "; Jet OLEDB:Database Password=" & AccessDatabasePass
        End If
        AccessDBSource &= ";"
        Dim ConnectionString As String = AccessDBProvider & AccessDBSource

        Return ConnectionString
    End Function

    Public Function GetFromAccessDBConnStr(ByVal AccessConnStr As String) As List(Of String)
        Dim Result As New List(Of String)
        Dim IndexStart As Integer
        Dim Lngth As Integer

        IndexStart = AccessConnStr.ToLower.IndexOf("PROVIDER".ToLower)
        If IndexStart >= 0 Then
            IndexStart += AccessConnStr.Substring(IndexStart).IndexOf("=") + "=".Length
            Lngth = AccessConnStr.Substring(IndexStart + "=".Length).IndexOf(";") + ";".Length
            If Lngth >= 0 Then
                Result.Add(AccessConnStr.Substring(IndexStart, Lngth).Trim())
            Else
                Result.Add("")
            End If
        Else
            Result.Add("")
        End If

        IndexStart = AccessConnStr.ToLower.IndexOf("Data Source".ToLower)
        If IndexStart >= 0 Then
            IndexStart += AccessConnStr.Substring(IndexStart).IndexOf("=") + "=".Length
            Lngth = AccessConnStr.Substring(IndexStart + "=".Length).IndexOf(";") + ";".Length
            If Lngth >= 0 Then
                Result.Add(AccessConnStr.Substring(IndexStart, Lngth).Trim())
            Else
                Result.Add("")
            End If
        Else
            Result.Add("")
        End If

        IndexStart = AccessConnStr.ToLower.IndexOf("Jet OLEDB:Database Password".ToLower)
        If IndexStart >= 0 Then
            IndexStart += AccessConnStr.Substring(IndexStart).IndexOf("=") + "=".Length
            Lngth = AccessConnStr.Substring(IndexStart + "=".Length).IndexOf(";") + ";".Length
            If Lngth >= 0 Then
                Result.Add(AccessConnStr.Substring(IndexStart, Lngth).Trim())
            Else
                Result.Add("")
            End If
        Else
            Result.Add("")
        End If

        Return Result
    End Function

    Public Function GetAccessDBType(ByVal Provider As String) As DBType
        If Provider.ToLower.Contains("Jet.OLEDB".ToLower) Then
            Return DBType.Access97to2003
        Else
            Return DBType.Access2007to2016
        End If
    End Function

    Public Function CompactAccessDB(ByVal DatabaseFile As String, ByVal AccessDBProvider As String, ByVal Type As Integer, Optional ByVal Password As String = "", Optional ByVal ShowErrorMessage As Boolean = False) As Boolean
        Dim AccessExt As String = GetExt(DatabaseFile)
        Dim strTempDbFullName As String = strExtras & "tmpDB" & AccessExt
        Try

            File.Delete(strTempDbFullName)
            File.Move(DatabaseFile, strTempDbFullName)

            Dim ConnectionPassword As String = ""

            If Password <> "" Then ConnectionPassword = ";pwd=" & Password
            Dim DBEngine As New Microsoft.Office.Interop.Access.Dao.DBEngine
            DBEngine.CompactDatabase(strTempDbFullName, DatabaseFile, , , ConnectionPassword)

            File.Delete(strTempDbFullName)
            Return True

        Catch ex As Exception
            Try
                File.Move(strTempDbFullName, DatabaseFile)
            Catch exc As Exception
            End Try
            If ShowErrorMessage Then
                CreateCrashFile(ex, ShowErrorMessage)
            End If
            Return False
        End Try
    End Function

    Public Function SplitAccessDatabase(ByVal DatabaseFile As String, ByVal AccessDBProvider As String, ByVal DatabaseTypeID As Integer,
                                  ByVal AccessDatabasePass As String, Optional ByVal Password As String = "") As Boolean

        Dim AccessExt As String = GetExt(DatabaseFile)
        Dim strSplitDBFile As String = strDatabaseDir & String.Format("Database Split {0:D2}-{1:D4}", Today.Month, Today.Year) & AccessExt

        Try
            DelFileFolder(strSplitDBFile)
            UnlockFileFolder(DatabaseFile)

            Dim ConnectionPassword As String = ""
            If Password <> "" Then ConnectionPassword = ";pwd=" & Password
            Dim DBEngine As New Microsoft.Office.Interop.Access.Dao.DBEngine
            DBEngine.CompactDatabase(DatabaseFile, strSplitDBFile, , , ConnectionPassword)
            'End If

            Call ClearAccessDatabase(DatabaseTables.ToArray, ProtectedTables.ToArray, DatabaseFile, AccessDatabasePass)
            Call CompactAccessDB(DatabaseFile, AccessDBProvider, CurDBType, Password)

            Return True
        Catch ex As Exception
            CreateCrashFile(ex)
            Return False
        End Try
    End Function

#Region "ClearSQLTable"
    Public Function ClearSQLTable(ByVal SQLConn As SqlConnection, ByVal TableToBeCleared As String, ByVal ProtectedTables() As String) As Boolean
        Return ClearSQLDatabase(SQLConn, {TableToBeCleared}, ProtectedTables)
    End Function
    Public Function ClearSQLTable(ByVal TableToBeCleared As String, ByVal ProtectedTables() As String) As Boolean
        Return ClearSQLDatabase(SQLConn, {TableToBeCleared}, ProtectedTables)
    End Function
#End Region

#Region "ClearSQLDatabase"
    Public Function ClearSQLDatabase() As Boolean
        Return ClearSQLDatabase(SQLConn, DatabaseTables.ToArray, ProtectedTables.ToArray)
    End Function
    Public Function ClearSQLDatabase(ByVal TablesToBeCleared() As String, ByVal ProtectedTables() As String, Optional ByRef NoTablesFound As Boolean = False) As Boolean
        Return ClearSQLDatabase(SQLConn, TablesToBeCleared, ProtectedTables, NoTablesFound)
    End Function
    Public Function ClearSQLDatabase(ByVal SQLConn As SqlConnection, ByVal TablesToBeCleared() As String, ByVal ProtectedTables() As String, Optional ByRef NoTablesFound As Boolean = False) As Boolean
        Try
            Dim ActualTableToBeCleared As IEnumerable(Of String) = From item In TablesToBeCleared Where Not ProtectedTables.Contains(item) Select item

            If ActualTableToBeCleared.Count > 0 Then
                Dim SQLCommandStr As String = ""

                For Each item In ActualTableToBeCleared
                    SQLCommandStr &= <SQL>
                                         USE <%= DatabaseName %>
                                         TRUNCATE TABLE [<%= item %>]</SQL>.Value & vbCrLf
                Next

                Dim SQLCmd As New SqlCommand(SQLCommandStr, SQLConn)
                SQLCmd.ExecuteNonQuery()
                Return True

            Else
                MsgBox(strModLanguage(83), MsgBoxStyle.Exclamation) 'No tables found, therefore nothing could be cleaned!
                NoTablesFound = True
                Return False
            End If


        Catch ex As Exception
            CreateCrashFile(ex, True)
            Return False
        End Try
    End Function
#End Region

    Public Function ClearAccessDatabase(ByVal TablesToBeCleared() As String, ByVal ProtectedTables() As String, ByVal AccessDataBaseFile As String, ByVal AccessDatabasePass As String,
                              Optional ByRef NoTablesFound As Boolean = False) As Boolean
        Try
            If TablesToBeCleared.Length > 0 Then
                Call LoadDatabaseTableNames()

                Dim ConnectionString As String = LoadAccessDBConnStr(CurDBType, AccessDataBaseFile, AccessDatabasePass, AccessDBSource, AccessDBProvider)
                OleDbConnection.ConnectionString = ConnectionString
                OleDbConnection.Open()

                For Each TableName In TablesToBeCleared
                    If Not isContained(TableName, ProtectedTables, False) Then
                        Dim EraseTable As New OleDb.OleDbCommand("Delete from " & TableName, OleDbConnection)
                        EraseTable.ExecuteNonQuery()
                    End If
                Next

                OleDbConnection.Close() '/Connection to DB Closed
                Return True

            Else
                MsgBox(strModLanguage(83), MsgBoxStyle.Exclamation) 'No tables found, therefore nothing could be cleaned!
                NoTablesFound = True
                Return False
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
            Return False
        End Try
    End Function

    Public Function ClearAccessTable(ByVal TableIndex As Integer) As Boolean 'Circumvents Protected tables!!!
        Try
            LoadDatabaseTableNames()
            Dim ConnectionString As String = LoadAccessDBConnStr(CurDBType, AccessDataBaseFile, AccessDatabasePass, AccessDBSource, AccessDBProvider)
            OleDbConnection.ConnectionString = ConnectionString
            OleDbConnection.Open()

            Dim EraseProductsTable As New OleDb.OleDbCommand("Delete from " & DatabaseTables.Item(TableIndex), OleDbConnection)
            EraseProductsTable.ExecuteNonQuery()

            OleDbConnection.Close() '/Connection to DB Closed
            Return True

        Catch ex As Exception
            CreateCrashFile(ex, True)
            Return False
        End Try
    End Function

#End Region

End Module
