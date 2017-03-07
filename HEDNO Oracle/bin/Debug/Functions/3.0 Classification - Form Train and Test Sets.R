###########################
### Supervised Learning ###
###########################

if (!(file.exists(paste(strXDF, "Classification_DS.xdf", sep = ""))) && (UseExistingXDFFile)) {
  XDFCreatedOutOfNecessity <- TRUE
} else {
  XDFCreatedOutOfNecessity <- FALSE
}

################################################
### 0) Forming the Training and Testing Sets ###
################################################

if (!(UseExistingXDFFile) || !file.exists(paste(strXDF, "Classification_DS.xdf", sep = ""))) {
	#Creating a selection ratio, dropping no longer needed variables and finalising the classification dataset
	Clustering_DS <- RxXdfData(paste(strXDF, "Clustering_DS.xdf", sep = ""))
	rxDataStep(inData = paste(strXDF, "Clustering_DS.xdf", sep = ""),
			   outFile = paste(strXDF, "preClassification1_DS.xdf", sep = ""),
			   transforms = list({3}
								 LabelFactorial = factor(Label, c(0,1))),
			   overwrite=TRUE
	)
	rxDataStep(inData = paste(strXDF, "preClassification1_DS.xdf", sep = ""),
			   outFile = paste(strXDF, "preClassification2_DS.xdf", sep = ""),
			   varsToDrop = c("{1}", "{2}"),
			   overwrite = TRUE
	)
	file.remove(paste(strXDF, "preClassification1_DS.xdf", sep = ""))
	preClassification2_DS <- RxXdfData(file = paste(strXDF, "preClassification2_DS.xdf", sep = ""))
	
	ClassificationColInfo <- list("LabelFactorial" = list(type = "factor", levels = c("0", "1"), newLevels = c("Cancelled", "Approved")))
	Classification_DS <- rxImport(inData = preClassification2_DS,
								  outFile = paste(strXDF, "Classification_DS.xdf", sep = ""),
								  colInfo = ClassificationColInfo,
								  overwrite = TRUE
	)
	remove(ClassificationColInfo)
	file.remove(paste(strXDF, "preClassification2_DS.xdf", sep = ""))
	remove(preClassification2_DS)
}

Classification_DS <- RxXdfData(paste(strXDF, "Classification_DS.xdf", sep = ""))
n_Classification <- rxGetInfo(data = Classification_DS)$numRows

if (VisualiseClassImbalance) {
	#Visualising the Class Imbalance
	rxHistogram(~LabelFactorial, data = Classification_DS)
}

##########################
#Forming the Training set#
##########################
if (FormTrainingSet) {
	if (StatisticsMode) {
		rxDataStep(inData = paste(strXDF, "Classification_DS.xdf", sep = ""),
				   outFile = paste(strXDF, "Training_DS.xdf", sep = ""),
				   rowSelection = SelectionRatio <= {4}, #About 80% of the data
				   varsToDrop = "SelectionRatio",
				   blocksPerRead = 20,
				   rowsPerRead = RowsPerRead,
				   overwrite = TRUE
		)
		Training_DS <- RxXdfData(file = paste(strXDF, "Training_DS.xdf", sep = ""))
		n_Train <- rxGetInfo(data = Training_DS)$numRows
		ActualTrainingPercentage <- n_Train / n_Classification * 100
		ActualTrainingPercentage
		remove(ActualTrainingPercentage)
	
	} else {
		rxDataStep(inData = paste(strXDF, "Classification_DS.xdf", sep = ""),
				   outFile = paste(strXDF, "Training_DS.xdf", sep = ""),
				   rowSelection = MarkedForTest == 0, #Get only those project that aren't marked for the Testing Dataset
				   blocksPerRead = 20,
				   rowsPerRead = RowsPerRead,
				   overwrite = TRUE
		)
		Training_DS <- RxXdfData(file = paste(strXDF, "Training_DS.xdf", sep = ""))
	}
} else {
	if(file.exists(paste(strXDF, "Training_DS.xdf", sep = ""))) {
		Training_DS <- RxXdfData(file = paste(strXDF, "Training_DS.xdf", sep = ""))
	}
}

######################
#Forming the test set#
######################
if (FormTestingSet) {
	if (StatisticsMode) {
		rxDataStep(inData = paste(strXDF, "Classification_DS.xdf", sep = ""),
				   outFile = paste(strXDF, "Test_DS.xdf", sep = ""),
				   rowSelection = SelectionRatio > {4}, #About 20% of the data
				   varsToDrop = "SelectionRatio",
				   blocksPerRead = 20,
				   rowsPerRead = RowsPerRead,
				   overwrite = TRUE
		)
		Test_DS <- RxXdfData(file = paste(strXDF, "Test_DS.xdf", sep = ""))
		n_Test <- rxGetInfo(data = Test_DS)$numRows
		ActualTestPercentage <- n_Test / n_Classification * 100
		ActualTestPercentage
		remove(ActualTestPercentage)
	
	} else {
		rxDataStep(inData = paste(strXDF, "Classification_DS.xdf", sep = ""),
				   outFile = paste(strXDF, "Test_DS.xdf", sep = ""),
				   rowSelection = MarkedForTest == 1, #Get only those project that are marked for the Testing Dataset
				   blocksPerRead = 20,
				   rowsPerRead = RowsPerRead,
				   overwrite = TRUE
		)
		Test_DS <- RxXdfData(file = paste(strXDF, "Test_DS.xdf", sep = ""))
	}
} else {
	if(file.exists(paste(strXDF, "Test_DS.xdf", sep = ""))) {
		Test_DS <- RxXdfData(file = paste(strXDF, "Test_DS.xdf", sep = ""))
	}
}

if ((ShowDataSummary) || (ShowVariableInfo)) {
	ClassificationDataSummary <- rxSummary(~., data = Classification_DS)$sDataFrame
	ClassificationColumnNames <- ClassificationDataSummary$Name
}
if (ShowVariableInfo) {
	ClassificationVarInfo <- rxGetInfo(Classification_DS, getVarInfo = TRUE, numRows = 0)$varInfo
}


if(FormTrainingSet || file.exists(paste(strXDF, "Training_DS.xdf", sep = ""))) {
	if ((ShowTrainDataSummary) || (ShowTrainVarInfo)) {
		TrainDataSummary <- rxSummary(~., data = Training_DS)$sDataFrame
		TrainColumnNames <- TrainDataSummary$Name
	}
	if (ShowTrainVarInfo) {
		TrainVarInfo <- rxGetInfo(Training_DS, getVarInfo = TRUE, numRows = 0)$varInfo
	}
}

if(FormTestingSet || file.exists(paste(strXDF, "Test_DS.xdf", sep = ""))) {
	if ((ShowTestDataSummary) || (ShowTestVarInfo)) {
		TestDataSummary <- rxSummary(~., data = Test_DS)$sDataFrame
		TestColumnNames <- TestDataSummary$Name
	}
	if (ShowTestVarInfo) {
		TestVarInfo <- rxGetInfo(Test_DS, getVarInfo = TRUE, numRows = 0)$varInfo
	}
}

if (CleanupXFDFile && file.exists(paste(strXDF, "Classification_DS.xdf", sep = ""))) {
	file.remove(paste(strXDF, "Classification_DS.xdf", sep = ""))
	remove(Classification_DS)
}
