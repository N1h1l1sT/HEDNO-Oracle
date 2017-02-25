Module modGlobal
    Public strGraphs As String = strRoot & "Graphs\"
    Public strSQLDir As String = strRoot & "SQL\"
    Public strSQLViewsDir As String = strSQLDir & "Views"
    Public ConnectedToSQLServer As Boolean = False

    Public strModelsPath As String = doProperPathNameLinux(strRoot & "Models/")

    Public TablevFinalDataset As String = "v9FinalDataset" 'This is v4Erga without the Label Restriction Clause, hence all rows are fetched, not only whose projects actually got or didn't get the Approval

    Public TablevErga As String = "v4Erga"
    Public ColvCityName As String = "Onoma_Polis"
    Public ColvGeoLocX As String = "GeoLocX"
    Public ColvGeoLocY As String = "GeoLocY"
    Public ColvID_Erga As String = "ID_Erga"

    Public TableErga As String = "ΕΡΓΑ"
    Public ColCityName As String = "ΠΟΛΗ"
    Public ColGeoLocX As String = "GeoLocX"
    Public ColGeoLocY As String = "GeoLocY"
    Public ColID_Erga As String = "ID"

    Public GeoLocAPIKey As String = ""
    Public GeoLocationAPILink As String = ""
    Public ErrorMessageIdentifierInJSON As String = ""
    Public CityFieldSuffix As String = ""

    Public APIExceededQuotaError As String = ""

    Public RowsPerRead As Integer = 25000
    Public strXDF As String = ""
    Public RoundAt As Integer = 3
    Public RSQLConnStr As String = ""

    'Informing the User
    Public FuncInProgress As New List(Of String)

End Module
