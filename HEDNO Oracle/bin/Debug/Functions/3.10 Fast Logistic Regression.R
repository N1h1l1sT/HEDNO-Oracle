###########################
### Supervised Learning ###
###########################

if (!(file.exists("{1}{6}{5}.RDS")) && (UseExistingModel)) {
  RDSCreatedOutOfNecessity <- TRUE
} else {
  RDSCreatedOutOfNecessity <- FALSE
}

##########################################################################
### 10) Creating a Classification Model using Fast Logistic Regression ###
##########################################################################

if ((!(UseExistingModel)) || (!file.exists("{1}{6}{5}.RDS"))) {
	## Creating the Model ##
	rxDTreeElapsedTime <- system.time(
	  #Tweakable:
	  #--Variables
	  #//To be left as is on this experiment (though tweakable if needed)\\#
	  #--normalize [auto/no/yes/warn]
	  {6}{5} <- rxLogisticRegression(Label ~ {0}
						, data = paste(strXDF, "Training_DS.xdf", sep = "")
						
						,type = "binary"
						,sgdInitTol = {7} #Set to a number greater than 0 to use Stochastic Gradient Descent (SGD) to find the initial parameters. A non-zero value set specifies the tolerance SGD uses to determine convergence. The default value is 0 specifying that SGD is not used.
						,l2Weight = {8} #The L2 regularization weight. Its value must be greater than or equal to 0 and the default value is set to 1; is preferable for data that is not sparse. It pulls large weights towards zero
						,l1Weight = {9} #The L1 regularization weight. Its value must be greater than or equal to 0 and the default value is set to 1; can be applied to sparse models, when working with high-dimensional data. It pulls small weights associated features that are relatively unimportant towards 0
						,optTol = {10} #Threshold value for optimizer convergence. If the improvement between iterations is less than the threshold, the algorithm stops and returns the current model. Smaller values are slower, but more accurate. The default value is 1e-07
						,memorySize = {11} #Memory size for L-BFGS, specifying the number of past positions and gradients to store for the computation of the next step. This optimization parameter limits the amount of memory that is used to compute the magnitude and direction of the next step. When you specify less memory, training is faster but less accurate. Must be greater than or equal to 1 and the default value is 20.
						,initWtsScale = {12} #Sets the initial weights diameter that specifies the range from which values are drawn for the initial weights. These weights are initialized randomly from within this range. For example, if the diameter is specified to be d, then the weights are uniformly distributed between -d/2 and d/2. The default value is 0, which specifies that all the weights are initialized to 0.
						,maxIterations = {13} #Sets the maximum number of iterations. After this number of steps, the algorithm stops even if it has not satisfied convergence criteria
						,normalize = "{14}" #[auto/no/yes/warn] "warn": if normalization is needed, a warning message is displayed, but normalization is not performed. Normalization rescales disparate data ranges to a standard scale. Feature scaling insures the distances between data points are proportional and enables various optimization methods such as gradient descent to converge much faster. If normalization is performed, a MaxMin normalizer is used. It normalizes values in an interval [a, b] where -1 <= a <= 0 and 0 <= b <= 1 and b - a = 1. This normalizer preserves sparsity by mapping zero to zero
						#trainThreads = NULL #The number of threads to use in training the model. This should be set to the number of cores on the machine. Note that L-BFGS multi-threading attempts to load dataset into memory. In case of out-of-memory issues, set trainThreads to 1 to turn off multi-threading. If NULL the number of threads to use is determined internally. The default value is NULL
						#denseOptimizer = #If TRUE, forces densification of the internal optimization vectors. If FALSE, enables the logistic regression optimizer use sparse or dense internal states as it finds appropriate. Setting denseOptimizer to TRUE requires the internal optimizer to use a dense internal state, which may help alleviate load on the garbage collector for some varieties of larger problems

						,reportProgress = {reportProgress}
						,blocksPerRead = {blocksPerRead}
						,rowSelection = {rowSelection}
	  )
	)
# summary(MLLRModel) #The Fast Logistic Regression Model

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
	
	  tmpVarInfo <- list(
		PredictedLabel = list(newName = "FLR_Prediction{5}"),
		Probability.1 = list(newName = "FLR_PredictionReal{5}")
	  )
	  rxSetVarInfo(varInfo = tmpVarInfo,
				   data = paste(strXDF, "tmp.xdf", sep = "")
	  )
	  rxDataStep(inData = paste(strXDF, "tmp.xdf", sep = ""),
				 outFile = paste(strXDF, "tmp2.xdf", sep = ""),
				 varsToKeep = c("FLR_PredictionReal{5}", "FLR_Prediction{5}"),
				 overwrite = TRUE
	  )
	CurVarNamesTest <- rxSummary(~., data = paste(strXDF, "Test_DS.xdf", sep = ""))$sDataFrame[[1]]
	if ("FLR_PredictionReal{5}" %in% (CurVarNamesTest)) {
	  rxDataStep(inData = paste(strXDF, "Test_DS.xdf", sep = ""),
				 outFile = paste(strXDF, "tmp3.xdf", sep = ""),
				 varsToDrop = c("FLR_PredictionReal{5}", "FLR_Prediction{5}"),
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
		tmp <- rxDataStep(inData = Test_DS, varsToKeep = c("ID_Erga", "FLR_PredictionReal{5}", "FLR_Prediction{5}"))
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
		if (("FLR_Prediction{5}" %in% TestColumnNames) && ("Label" %in% TestColumnNames)) {
			LabelPredictionExist <- TRUE
			
			tmp <- rxCube(~ F(Label):F(FLR_Prediction{5}), data = paste(strXDF, "Test_DS.xdf", sep = ""))
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
		if (("FLR_Prediction{5}" %in% TestColumnNames) && ("Label" %in% TestColumnNames)) {
			PredictionRealExist <- TRUE
			
			if (ColumnsCombinations) { #REngine can't handle multiple Plot Windows, hence they should be saved as PNG and ran afterwards
				png(filename = paste(strGraphs, "{6}ROC{5}.png", sep = "") , width = 615, height = 520, units = "px")
			}
			
			rxRocCurve(actualVarName = "Label",
					   predVarName = "FLR_PredictionReal{5}",
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
