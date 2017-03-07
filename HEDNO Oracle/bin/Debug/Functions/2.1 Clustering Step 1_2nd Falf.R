## Unsupervised Learning - Clustering ##

#############
## K-Means ##
#############
KMeansModel <- rxKmeans(formula = ~ {1} + {2},
					  data = paste(strXDF, "Clustering_DS.xdf", sep = ""),
					  numClusters = k,
					  outFile = paste(strXDF, "Clustering_DS.xdf", sep = ""),
					  algorithm = "lloyd",
					  blocksPerRead = 1,
					  overwrite = TRUE
)
#K-Means model information

if (SaveKMeansModel) {
	saveRDS(KMeansModel, "{4}KMeansModel.RDS")
}
remove(KMeansModel)

if (ShowGeoLocGraph) {
	rxLinePlot({2} ~ {1},
			   groups = .rxCluster,
			   data = paste(strXDF, "Clustering_DS.xdf", sep = ""),
			   type = "p"
	)
}

if ((ShowDataSummary) || (ShowVariableInfo)) {
	ClusteringDataSummary <- rxSummary(~., data = Clustering_DS)$sDataFrame
	ClusteringColumnNames <- ClusteringDataSummary$Name
}

if (ShowVariableInfo) {
	ClusteringVarInfo <- rxGetInfo(Clustering_DS, getVarInfo = TRUE, numRows = 0)$varInfo
}

if (CleanupXFDFile && file.exists(paste(strXDF, "Clustering_DS.xdf", sep = ""))) {
	file.remove(paste(strXDF, "Clustering_DS.xdf", sep = ""))
	remove(Clustering_DS)
}
