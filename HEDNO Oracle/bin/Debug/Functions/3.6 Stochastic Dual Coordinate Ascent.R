###########################
### Supervised Learning ###
###########################

if (!(file.exists("{1}{6}{5}.RDS")) && (UseExistingModel)) {
  RDSCreatedOutOfNecessity <- TRUE
} else {
  RDSCreatedOutOfNecessity <- FALSE
}

##################################################################################
### 6) Creating a Classification Model using Stochastic Dual Coordinate Ascent ###
##################################################################################

if ((!(UseExistingModel)) || (!file.exists("{1}{6}{5}.RDS"))) {
	## Creating the Model ##
	rxDTreeElapsedTime <- system.time(
	  #Tweakable:
	  #-- Variables
	  #--lossFunction [logLoss/hingeLoss/smoothHingeLoss]
	  #--l2Weight
	  #--l1Weight
	  #--maxIterations
	  #--shuffle
	  #--type [binary/regression]
	  #//To be left as is on this experiment (though tweakable if needed)\\#
	  {6}{5} <- rxFastLinear(Label ~ {0}
						, data = paste(strXDF, "Training_DS.xdf", sep = "")
						
						,type = type
						,convergenceTolerance = {7} #Specifies the tolerance threshold used as a convergence criterion. It must be between 0 and 1. The default value is 0.1. The algorithm is considered to have converged if the relative duality gap, which is the ratio between the duality gap and the primal loss, falls below the specified convergence tolerance.
						,normalize = "{8}" #Specifies the type of automatic normalization used: [auto/no/yes/warn] "warn": if normalization is needed, a warning message is displayed, but normalization is not performed. Normalization rescales disparate data ranges to a standard scale. Feature scaling insures the distances between data points are proportional and enables various optimization methods such as gradient descent to converge much faster. If normalization is performed, a MaxMin normalizer is used. It normalizes values in an interval [a, b] where -1 <= a <= 0 and 0 <= b <= 1 and b - a = 1. This normalizer preserves sparsity by mapping zero to zero
						# ,maxIterations = 25 #Specifies an upper bound on the number of training iterations. This parameter must be positive or NULL. If NULL is specified, the actual value is automatically computed based on data set. Each iteration requires a complete pass over the training data. Training terminates after the total number of iterations reaches the specified upper bound or when the loss function converges, whichever happens earlier.
						# ,lossFunction = #Specifies the empirical loss function to optimize. For binary classification, the following choices are available: logLoss: The log-loss. This is the default. hingeLoss: The SVM hinge loss. Its parameter represents the margin size. smoothHingeLoss: The smoothed hinge loss. Its parameter represents the smoothing constant. For linear regression, squared loss squaredLoss is currently supported.
						# ,l2Weight = #Specifies the L2 regularization weight. The value must be either non-negative or NULL. If NULL is specified, the actual value is automatically computed based on data set
						# ,l1Weight = #Specifies the L1 regularization weight. The value must be either non-negative or NULL. If NULL is specified, the actual value is automatically computed based on data set
						# ,shuffle = FALSE #Specifies whether to shuffle the training data. Set TRUE to shuffle the data; FALSE not to shuffle. The default value is FALSE. SDCA is a stochastic optimization algorithm. If shuffling is turned on, the training data is shuffled on each iteration.

					   
						,reportProgress = {reportProgress}
						,blocksPerRead = {blocksPerRead}
						,rowSelection = {rowSelection}
	  )
	)
# summary(SDCAModel) #The Stochastic Dual Coordinate Ascend Model
# SDCAModel$coefficients

} else {
	{6}{5} <- readRDS("{1}{6}{5}.RDS")
}

if (SavePredictionModel) {
	saveRDS({6}{5}, "{1}{6}{5}.RDS")
}

if (MakePredictions) {
	## Applying the Predictions ##
	rxPredict(modelObject = {6}{5},
			  data = paste(strXDF, "Test_DS.xdf", sep = ""),
			  outData = paste(strXDF, "tmp.xdf", sep = ""),
			  overwrite = TRUE
	)
	
	if (type == "binary") {
	  tmpVarInfo <- list(
		PredictedLabel = list(newName = "SDCA_Prediction{5}"),
		Probability.1 = list(newName = "SDCA_PredictionReal{5}")
	  )
	  rxSetVarInfo(varInfo = tmpVarInfo,
				   data = paste(strXDF, "tmp.xdf", sep = "")
	  )
	  rxDataStep(inData = paste(strXDF, "tmp.xdf", sep = ""),
				 outFile = paste(strXDF, "tmp2.xdf", sep = ""),
				 varsToKeep = c("SDCA_PredictionReal{5}", "SDCA_Prediction{5}"),
				 overwrite = TRUE
	  )
	} else {
	  tmpVarInfo <- list(
		Score = list(newName = "SDCA_PredictionReal{5}")
	  )
	  rxSetVarInfo(varInfo = tmpVarInfo,
				   data = paste(strXDF, "tmp.xdf", sep = "")
	  )
	  rxDataStep(inData = paste(strXDF, "tmp.xdf", sep = ""),
				 outFile = paste(strXDF, "tmp2.xdf", sep = ""),
				 transforms = list(SDCA_Prediction = ifelse(SDCA_PredictionReal >= 0.5, 1, 0)),
				 overwrite = TRUE
	  )
	}
	CurVarNamesTest <- rxSummary(~., data = paste(strXDF, "Test_DS.xdf", sep = ""))$sDataFrame[[1]]
	if ("SDCA_PredictionReal{5}" %in% (CurVarNamesTest)) {
	  rxDataStep(inData = paste(strXDF, "Test_DS.xdf", sep = ""),
				 outFile = paste(strXDF, "tmp3.xdf", sep = ""),
				 varsToDrop = c("SDCA_PredictionReal{5}", "SDCA_Prediction{5}"),
				 overwrite = TRUE
	  )
	  Test_DS <- rxImport(inData = paste(strXDF, "tmp3.xdf", sep = ""),
						  outFile = paste(strXDF, "Test_DS.xdf", sep = ""),
						  rowsPerRead = RowsPerRead,
						  overwrite = TRUE
	  )
	  file.remove(paste(strXDF, "tmp3.xdf", sep = ""))
	}
	remove(CurVarNamesTest)
	rxMerge(inData1 = paste(strXDF, "Test_DS.xdf", sep = ""),
			inData2 = paste(strXDF, "tmp2.xdf", sep = ""),
			outFile = paste(strXDF, "tmp.xdf", sep = ""),
			type = "oneToOne",
			overwrite = TRUE
	)
	Test_DS <- rxImport(inData = paste(strXDF, "tmp.xdf", sep = ""),
						outFile = paste(strXDF, "Test_DS.xdf", sep = ""),
						rowsPerRead = RowsPerRead,
						overwrite = TRUE
	)
	remove(tmpVarInfo)
	file.remove(paste(strXDF, "tmp.xdf", sep = ""))
	file.remove(paste(strXDF, "tmp2.xdf", sep = ""))
	
	if (!(StatisticsMode)) {
		tmp <- rxDataStep(inData = Test_DS, varsToKeep = c("ID_Erga", "SDCA_PredictionReal{5}", "SDCA_Prediction{5}"))
		write.csv(tmp, file = paste(strXDF, "{3}", sep = ""), row.names = FALSE)
		remove(tmp)
	}
}

#This is always needed because TestColumnNames is used no matter what
#if ((ShowDataSummary) || (ShowVariableInfo)) {
	#Updating the Column Names and Data Summary as new variables might have been introduced (e.g. predictions)
	TestDataSummary <- rxSummary(~., data = Test_DS)$sDataFrame
	TestColumnNames <- TestDataSummary$Name
#}
if (ShowVariableInfo) {
	TestVarInfo <- rxGetInfo(Test_DS, getVarInfo = TRUE, numRows = 0)$varInfo
}

if (StatisticsMode) {
	## Creating and Viewing Statistics & Graphs ##
	
	if (ShowStatistics) {
		## Statistics ##
		if (("SDCA_Prediction{5}" %in% TestColumnNames) && ("Label" %in% TestColumnNames)) {
			LabelPredictionExist <- TRUE
			
			tmp <- rxCube(~ F(Label):F(SDCA_Prediction{5}), data = paste(strXDF, "Test_DS.xdf", sep = ""))
			StatisticsResults <- Statistics(tmp, {2}, paste(strXDF, "Training_DS.xdf", sep = ""))
			remove(tmp)
			sink("{4}") 	#Starting sink-ing to Sink.R file
			print(StatisticsResults)
			sink()			#Stopping sink-ing
		} else {
			LabelPredictionExist <- FALSE
		}
	}

	if (ShowROCCurve) {
		## Graphs ##
		if (("SDCA_Prediction{5}" %in% TestColumnNames) && ("Label" %in% TestColumnNames)) {
			PredictionRealExist <- TRUE
			
			if (ColumnsCombinations) { #REngine can't handle multiple Plot Windows, hence they should be saved as PNG and ran afterwards
				png(filename = paste(strGraphs, "{6}ROC{5}.png", sep = "") , width = 615, height = 520, units = "px")
			}
			
			rxRocCurve(actualVarName = "Label",
					   predVarName = "SDCA_PredictionReal{5}",
					   data = paste(strXDF, "Test_DS.xdf", sep = "")
			)
			
			if (ColumnsCombinations) { #REngine can't handle multiple Plot Windows, hence they should be saved as PNG and ran afterwards
				dev.off()
			}

		} else {
			PredictionRealExist <- FALSE
		}
	}
	
}

remove({6}{5})
