###########################
### Supervised Learning ###
###########################

if (!(file.exists("{1}{6}{5}.RDS")) && (UseExistingModel)) {
  RDSCreatedOutOfNecessity <- TRUE
} else {
  RDSCreatedOutOfNecessity <- FALSE
}

############################################################
### 3) Creating a Classification Model using Naive Bayes ###
############################################################

if ((!(UseExistingModel)) || (!file.exists("{1}{6}{5}.RDS"))) {
	## Creating the Model ##
	rxDNBElapsedTime <- system.time(
	#Tweakable:
	#--Dependent Variables
    #--smoothingFactor
	#//To be left as is on this experiment (though tweakable if needed)\\#
	  {6}{5} <- rxNaiveBayes(LabelFactorial ~ {0}
								, data = paste(strXDF, "Training_DS.xdf", sep = "")
								
							    ,smoothingFactor = 1
							    #,fweights =
							    #,pweights =
							    
								,reportProgress = {reportProgress}
								,blocksPerRead = {blocksPerRead}
								,rowSelection = {rowSelection}
								
								,xdfCompressionLevel = rxGetOption("xdfCompressionLevel")
								# ,variableSelection = #rxStepControl(method="stepwise", scope = ~ Age + Start + Number )
	  )
	)
# NaiveBayesModel #The Naive Bayes model
# NaiveBayesModel$apriori           #[valueC] Proportion of the Label for each of its categories | Cancelled 0.2289043 Approved 0.7710957

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
			  ,predVarNames = c("NBCancelledProbabil", "NB_PredictionReal{5}")
			  ,type = "prob" #To get probabilities instead of 0/1
			  # ,computeStdErr = TRUE
			  # ,stdErrorsVarNames = "NB_StdError"
			  # ,interval = "confidence" 
			  # ,intervalVarNames = c("NB_LowerConfInterv", "NB_UpperConfInterv")
			  # ,computeResiduals = TRUE
			  # ,residVarNames = "NB_Residual"

			  ,blocksPerRead = rxGetOption("blocksPerRead")
			  ,reportProgress = rxGetOption("reportProgress")
			  ,xdfCompressionLevel = rxGetOption("xdfCompressionLevel")
	)
	rxDataStep(inData = paste(strXDF, "Test_DS.xdf", sep = ""),
			   outFile = paste(strXDF, "tmp.xdf", sep = ""),
			   transforms = list(NB_Prediction{5} = as.logical(round(NB_PredictionReal{5}))),
			   overwrite = TRUE
	)
	rxDataStep(inData = paste(strXDF, "tmp.xdf", sep = ""),
			   outFile = paste(strXDF, "Test_DS.xdf", sep = ""),
			   varsToDrop = c("NBCancelledProbabil"),
			   overwrite = TRUE
	)
	file.remove(paste(strXDF, "tmp.xdf", sep = ""))
	
	if (!(StatisticsMode)) {
		tmp <- rxDataStep(inData = Test_DS, varsToKeep = c("ID_Erga", "NB_PredictionReal{5}", "NB_Prediction{5}"))
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
		if (("NB_Prediction{5}" %in% TestColumnNames) && ("Label" %in% TestColumnNames)) {
			LabelPredictionExist <- TRUE
			
			tmp <- rxCube(~ F(Label):F(NB_Prediction{5}), data = paste(strXDF, "Test_DS.xdf", sep = ""))
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
		if (("NB_Prediction{5}" %in% TestColumnNames) && ("Label" %in% TestColumnNames)) {
			PredictionRealExist <- TRUE
			
			if (ColumnsCombinations) { #REngine can't handle multiple Plot Windows, hence they should be saved as PNG and ran afterwards
				png(filename = paste(strGraphs, "{6}ROC{5}.png", sep = "") , width = 615, height = 520, units = "px")
			}
			
			rxRocCurve(actualVarName = "Label",
					   predVarName = "NB_PredictionReal{5}",
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
