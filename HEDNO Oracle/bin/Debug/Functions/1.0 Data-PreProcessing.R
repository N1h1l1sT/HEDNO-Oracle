#########################
#### SQL Table: vErga ###
#########################
if (!(file.exists(paste(strXDF, "vErga_DS.xdf", sep = ""))) && (UseExistingXDFFile)) {
  XDFCreatedOutOfNecessity <- TRUE
} else {
  XDFCreatedOutOfNecessity <- FALSE
}

if (!(UseExistingXDFFile) || !file.exists(paste(strXDF, "vErga_DS.xdf", sep = ""))) {
	rxDataStep(inData = RxOdbcData(sqlQuery = "SELECT * FROM {0}", connectionString = sqlConnString, rowsPerRead = RowsPerRead),
			   outFile = paste(strXDF, "tmp.xdf", sep = ""),
			   colClasses = vErgaColClasses,
			   colInfo = vErgaColInfo,
			   stringsAsFactors = TRUE,
			   rowsPerRead = RowsPerRead,
			   overwrite = TRUE
	)

	rxSetVarInfo(varInfo = NewvErgaVarInfo,
				 data = paste(strXDF, "tmp.xdf", sep = "")
	)
	rxFactors(inData = paste(strXDF, "tmp.xdf", sep = ""),
			  outFile = paste(strXDF, "vErga_DS.xdf", sep = ""),
			  factorInfo = c("TimeSeriesDate"),
			  sortLevels = TRUE,
			  overwrite = TRUE
	)
	file.remove(paste(strXDF, "tmp.xdf", sep = ""))
}

vErga_DS <- RxXdfData(file = paste(strXDF, "vErga_DS.xdf", sep = ""))

if ((ShowDataSummary) || (ShowVariableInfo)) {
	vErgaDataSummary <- rxSummary(~., data = vErga_DS)$sDataFrame
	vErgaColumnNames <- vErgaDataSummary$Name
}
if (ShowVariableInfo) {
	vErgaVarInfo <- rxGetInfo(vErga_DS, getVarInfo = TRUE, numRows = 0)$varInfo
}

#################
## Iteration 0 ##
#################
if (ShowGeoLocGraph) {
	#Peaking at the database as it is in the SQL View
	rxLinePlot(formula = {2} ~ {1},
			   data = vErga_DS,
			   type = "p"
	)
	#Many entries are outside Greece's rectangle
}

if (CleanupXFDFile && file.exists(paste(strXDF, "vErga_DS.xdf", sep = ""))) {
	file.remove(paste(strXDF, "vErga_DS.xdf", sep = ""))
	remove(vErga_DS)
}

remove(NewvErgaVarInfo)
remove(vErgaColClasses)
remove(vErgaColInfo)