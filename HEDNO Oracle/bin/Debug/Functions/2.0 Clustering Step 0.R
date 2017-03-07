## Unsupervised Learning - Clustering ##

if (!(file.exists(paste(strXDF, "Clustering_DS.xdf", sep = ""))) && (UseExistingXDFFile)) {
  XDFCreatedOutOfNecessity <- TRUE
} else {
  XDFCreatedOutOfNecessity <- FALSE
}

##################################
### Forming the Clustering Set ###
##################################

#################
## Iteration 1 ##
#################
if (!(UseExistingXDFFile) || !file.exists(paste(strXDF, "Clustering_DS.xdf", sep = ""))) {
	#Eliminating invalid entries
	ClusteringSQLQuery <- "SELECT * FROM {0} WHERE ({1} >= 18 AND {1} <= 29 AND {2} >= 34 AND {2} <= 42) AND ({1} <> -1 and {2} <> -1)"
	Clustering_DS <- rxImport(inData = RxOdbcData(sqlQuery = ClusteringSQLQuery, connectionString = sqlConnString, rowsPerRead = RowsPerRead),
						   outFile = paste(strXDF, "tmp.xdf", sep = ""),
						   colClasses = vErgaColClasses,
						   colInfo = vErgaColInfo,
						   stringsAsFactors = TRUE,
						   overwrite = TRUE
	)
	rxSetVarInfo(varInfo = NewvErgaVarInfo,
			   data = paste(strXDF, "tmp.xdf", sep = "")
	)
	rxDataStep(inData = paste(strXDF, "tmp.xdf", sep = ""),
			 outFile = paste(strXDF, "tmp2.xdf", sep = ""),
			 varsToDrop = c("Onoma_Polis"),
			 overwrite = TRUE
	)
	rxFactors(inData = paste(strXDF, "tmp2.xdf", sep = ""),
			outFile = paste(strXDF, "Clustering_DS.xdf", sep = ""),
			factorInfo = c("TimeSeriesDate"),
			sortLevels = TRUE,
			overwrite = TRUE
	)

	file.remove(paste(strXDF, "tmp.xdf", sep = ""))
	file.remove(paste(strXDF, "tmp2.xdf", sep = ""))
	remove(NewvErgaVarInfo)
	remove(vErgaColClasses)
	remove(vErgaColInfo)
	remove(ClusteringSQLQuery)
}

Clustering_DS <- RxXdfData(paste(strXDF, "Clustering_DS.xdf", sep = ""))

if ((ShowDataSummary) || (ShowVariableInfo)) {
	ClusteringDataSummary <- rxSummary(~., data = Clustering_DS)$sDataFrame
	ClusteringColumnNames <- ClusteringDataSummary$Name
}
if (ShowVariableInfo) {
	ClusteringVarInfo <- rxGetInfo(Clustering_DS, getVarInfo = TRUE, numRows = 0)$varInfo
}
if (ShowGeoLocGraph) {
	#Visualising the invalid-entries-free Locations of the Clustering Dataset
	rxLinePlot({2} ~ {1}, Clustering_DS, type = "p")
}
if (CleanupXFDFile && file.exists(paste(strXDF, "Clustering_DS.xdf", sep = ""))) {
	file.remove(paste(strXDF, "Clustering_DS.xdf", sep = ""))
	remove(Clustering_DS)
}