###########################
### Supervised Learning ###
###########################

if (!(file.exists("{1}{6}{5}.RDS")) && (UseExistingModel)) {
  RDSCreatedOutOfNecessity <- TRUE
} else {
  RDSCreatedOutOfNecessity <- FALSE
}

n_Train <- rxGetInfo(data = Training_DS)$numRows

#######################################################################
### 7) Creating a Classification Model using Boosted Decision Trees ###
#######################################################################

if ((!(UseExistingModel)) || (!file.exists("{1}{6}{5}.RDS"))) {
	## Creating the Model ##
	rxDTreeElapsedTime <- system.time(
	  #Tweakable:
	  #--Variables
	  #--numTrees
	  #--numLeaves
	  #--gainConfLevel
	  #//To be left as is on this experiment (though tweakable if needed)\\#
	  {6}{5} <- rxFastTrees(Label ~ {0}
						, data = paste(strXDF, "Training_DS.xdf", sep = "")
						
						,numTrees = {7} #Specifies the total number of decision trees to create in the ensemble.By creating more decision trees, you can potentially get better coverage, but the training time increases. The default value is 100
						,numLeaves = {8} #The maximum number of leaves (terminal nodes) that can be created in any tree. Higher values potentially increase the size of the tree and get better precision, but risk overfitting and requiring longer training times. The default value is 20.
						,numBins = round(min(1001, max(101, sqrt(n_Train)))) #this controls the maximum number of bins used for each variable. Managing the number of bins is important in controlling memory usage. The default is min(1001, max(101, sqrt(num of obs))). For small data sets with continuous predictors, you may find that you need to increase the maxNumBins to obtain models that resemble those from rpart.
						,gainConfLevel = {9} #Tree fitting gain confidence requirement (should be in the range [0,1)). The default value is 0.
						,unbalancedSets = unbalancedSets
						#,learningRate = #Determines the size of the step taken in the direction of the gradient in each step of the learning process. This determines how fast or slow the learner converges on the optimal solution. If the step size is too big, you might overshoot the optimal solution. If the step size is too samll, training takes longer to converge to the best solution.
						#,minSplit = 10 #Minimum number of training instances required to form a leaf. That is, the minimal number of documents allowed in a leaf of a regression tree, out of the sub-sampled data. A 'split' means that features in each level of the tree (node) are randomly divided. The default value is 10. Only the number of instances is counted even if instances are weighted.
						#,exampleFraction = 0.7 #The fraction of randomly chosen instances to use for each tree. The default value is 0.7
						#,featureFraction = 1 #The fraction of randomly chosen features to use for each tree. The default value is 1
						#,splitFraction = 1 #The fraction of randomly chosen features to use on each split. The default value is 1
						#,firstUsePenalty = 0 #The feature first use penalty coefficient. This is a form of regularization that incurs a penalty for using a new feature when creating the tree. Increase this value to create trees that don't use many features. The default value is 0.
						#,trainThreads = 8
					   
						,reportProgress = {reportProgress}
						,blocksPerRead = {blocksPerRead}
						,rowSelection = {rowSelection}
	  )
	)
# summary(BDTModel) #The Boosted Decision Tree Model

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
		PredictedLabel = list(newName = "BDT_Prediction{5}"),
		Probability.1 = list(newName = "BDT_PredictionReal{5}")
	)
	rxSetVarInfo(varInfo = tmpVarInfo,
				data = paste(strXDF, "tmp.xdf", sep = "")
	)
	rxDataStep(inData = paste(strXDF, "tmp.xdf", sep = ""),
				outFile = paste(strXDF, "tmp2.xdf", sep = ""),
				varsToKeep = c("BDT_PredictionReal{5}", "BDT_Prediction{5}"),
				overwrite = TRUE
	 )
	CurVarNamesTest <- rxSummary(~., data = paste(strXDF, "Test_DS.xdf", sep = ""))$sDataFrame[[1]]
	if ("BDT_PredictionReal{5}" %in% (CurVarNamesTest)) {
	  rxDataStep(inData = paste(strXDF, "Test_DS.xdf", sep = ""),
				 outFile = paste(strXDF, "tmp3.xdf", sep = ""),
				 varsToDrop = c("BDT_PredictionReal{5}", "BDT_Prediction{5}"),
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
		tmp <- rxDataStep(inData = Test_DS, varsToKeep = c("ID_Erga", "BDT_PredictionReal{5}", "BDT_Prediction{5}"))
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
		if (("BDT_Prediction{5}" %in% TestColumnNames) && ("Label" %in% TestColumnNames)) {
			LabelPredictionExist <- TRUE
			
			tmp <- rxCube(~ F(Label):F(BDT_Prediction{5}), data = paste(strXDF, "Test_DS.xdf", sep = ""))
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
		if (("BDT_Prediction{5}" %in% TestColumnNames) && ("Label" %in% TestColumnNames)) {
			PredictionRealExist <- TRUE
			
			if (ColumnsCombinations) { #REngine can't handle multiple Plot Windows, hence they should be saved as PNG and ran afterwards
				png(filename = paste(strGraphs, "{6}ROC{5}.png", sep = "") , width = 615, height = 520, units = "px")
			}
			
			rxRocCurve(actualVarName = "Label",
					   predVarName = "BDT_PredictionReal{5}",
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
