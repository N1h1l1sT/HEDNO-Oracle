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

############################################
## Finding the optimal number of clusters ##
############################################
unsupervisedLocationData1 <- rxDataStep(inData = Clustering_DS,
									  varsToKeep = c("{1}", "{2}")
)
WithinGroupsSquaredError <- (nrow(unsupervisedLocationData1) - 1) * sum(apply(unsupervisedLocationData1, 2, var))
for (i in 2:{3}) {
WithinGroupsSquaredError[i] <- sum(rxKmeans(formula = formula(~ {1} + {2}),
											data = unsupervisedLocationData1,
											numClusters = i,
											algorithm = "lloyd"
											)$withinss
							   )
}

remove(unsupervisedLocationData1)

plot(1:{3},
   WithinGroupsSquaredError,
   type = "b",
   xlab = "# of Clusters",
   ylab = "Within Groups Sum of Squares"
)

k <- 1
for (i in 2:{3}) {
if (((WithinGroupsSquaredError[i-1] - WithinGroupsSquaredError[i]) / WithinGroupsSquaredError[i]) <= 0.1) {
  break
}
else {
  k <- i
}
}
k

remove(i)
remove(WithinGroupsSquaredError)

