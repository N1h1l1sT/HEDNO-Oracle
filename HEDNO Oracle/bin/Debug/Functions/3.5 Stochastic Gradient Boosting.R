###########################
### Supervised Learning ###
###########################

if (!(file.exists("{1}{6}{5}.RDS")) && (UseExistingModel)) {
  RDSCreatedOutOfNecessity <- TRUE
} else {
  RDSCreatedOutOfNecessity <- FALSE
}

#############################################################################
### 5) Creating a Classification Model using Stochastic Gradient Boosting ###
#############################################################################

n_Train <- rxGetInfo(data = Training_DS)$numRows

if ((!(UseExistingModel)) || (!file.exists("{1}{6}{5}.RDS"))) {
	  ## Creating the Model ##
	  rxDTreeElapsedTime <- system.time(
	  #Tweakable:
	  #--Dependent Variables
	  #--cp
	  #--nTree
	  #--mTry
	  #--learningRate
	  #--lossFunction [gaussian|Regr/bernoulli|Class/multinomial|MultiClass]
	  #--replace
	  #//To be left as is on this experiment (though tweakable if needed)\\#
	  {6}{5} <- rxBTrees(Label ~ {0}
						, data = paste(strXDF, "Training_DS.xdf", sep = "")
						
					    ,cp = {8}
					    ,nTree = {9}
					    ,mTry = {10}
					    ,maxDepth = {11}
					    ,lossFunction = lossFunction
					    ,importance = TRUE
					    ,maxNumBins = round(min(1001, max(101, sqrt(n_Train))))
                         # ,learningRate = 0.1
                         # ,findSplitsInParallel = TRUE
                         # ,replace =
                         # ,strata =
                         # ,cost = c("")
                         # ,minSplit =
                         # ,minBucket =
                         # ,maxCompete = 0
                         # ,useSurrogate =
                         # ,maxSurrogate = 0
                         # ,surrogateStyle =
                         # ,fweights =
                         # ,pweights =
					   
						,reportProgress = {reportProgress}
						,blocksPerRead = {blocksPerRead}
						,rowSelection = {rowSelection}
                        ,xdfCompressionLevel = rxGetOption("xdfCompressionLevel")
	  )
	)
# RandForestModel #The Stochastic Gradient Boosting model
# RandForestModel$oob.err        #[vector] a data frame containing the out-of-bag error estimate. For classification forests, this includes the OOB error estimate, which represents the proportion of times the predicted class is not equal to the true class, and the cumulative number of out-of-bag observations for the forest. For regression forests, this includes the OOB error estimate, which here represents the sum of squared residuals of the out-of-bag observations divided by the number of out-of-bag observations, the number of out-of-bag observations, the out-of-bag variance, and the “pseudo-R-Squared”, which is 1 minus the quotient of the oob.err and oob.var.

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
			  outData = paste(strXDF, "Test_DS.xdf", sep = ""),
			  overwrite = TRUE
			  ,predVarNames = c("SGB_PredictionReal{5}") #Different from class to anova
			  # ,computeStdErr = TRUE
			  # ,stdErrorsVarNames = "SGB_StdError"
			  # ,interval = "confidence" 
			  # ,intervalVarNames = c("SGB_LowerConfInterv", "SGB_UpperConfInterv")
			  # ,computeResiduals = TRUE
			  # ,residVarNames = "SGB_Residual"

			  ,blocksPerRead = rxGetOption("blocksPerRead")
			  ,reportProgress = rxGetOption("reportProgress")
			  ,xdfCompressionLevel = rxGetOption("xdfCompressionLevel")
	)
	rxDataStep(inData = paste(strXDF, "Test_DS.xdf", sep = ""),
			   outFile = paste(strXDF, "Test_DS.xdf", sep = ""),
			   transforms = list(SGB_Prediction{5} = as.logical(round(SGB_PredictionReal{5}))),
			   overwrite = TRUE
	)

	if (!(StatisticsMode)) {
		tmp <- rxDataStep(inData = Test_DS, varsToKeep = c("ID_Erga", "SGB_PredictionReal{5}", "SGB_Prediction{5}"))
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

if (PlotVarImportance) {
	png(filename = paste(strGraphs, "{6}_VariablesImportance{5}.png", sep = "") , width = 615, height = 520, units = "px")
	rxVarImpPlot({6}{5})
	dev.off()
}

if (ShowOOBEPlot) {
	png(filename = paste(strGraphs, "{6}_OutOfBagError{5}.png", sep = "") , width = 615, height = 520, units = "px")
	plot({6}{5})
	dev.off()
}

if (StatisticsMode) {
	## Creating and Viewing Statistics & Graphs ##
	
	if (ShowStatistics) {
		## Statistics ##
		if (("SGB_Prediction{5}" %in% TestColumnNames) && ("Label" %in% TestColumnNames)) {
			LabelPredictionExist <- TRUE
			
			tmp <- rxCube(~ F(Label):F(SGB_Prediction{5}), data = paste(strXDF, "Test_DS.xdf", sep = ""))
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
		if (("SGB_Prediction{5}" %in% TestColumnNames) && ("Label" %in% TestColumnNames)) {
			PredictionRealExist <- TRUE
			
			if (ColumnsCombinations) { #REngine can't handle multiple Plot Windows, hence they should be saved as PNG and ran afterwards
				png(filename = paste(strGraphs, "{6}ROC{5}.png", sep = "") , width = 615, height = 520, units = "px")
			}
			
			rxRocCurve(actualVarName = "Label",
					   predVarName = "SGB_PredictionReal{5}",
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
