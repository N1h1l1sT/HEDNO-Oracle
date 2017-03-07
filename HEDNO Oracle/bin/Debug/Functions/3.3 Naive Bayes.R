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
								
							    ,smoothingFactor = 1 #If we try to use our classifier on the test data without specifying a smoothing factor in our call to rxNaiveBayes the function rxPredict produces no results since our test data only has data from 2009. In general, smoothing is used to avoid over-fitting your model. It follows that to achieve the optimal classifier you may want to smooth the conditional probabilities even if every level of each variable is observed. perform Laplace smoothing. A positive smoothing factor to account for cases not present in the training data. It avoids modelling issues by preventing zero conditional probability estimates. Since the conditional probabilities are being multiplied in the model, adding a small number to 0 probabilities, precludes missing categories from wiping out the calculation.
							    #,fweights = #If duplicate rows have been eliminated, creating a new variable of how many duplicate rows were, then this Variable/Column can be used as Frequency Weight
							    #,pweights = #Probability weights for the observations
							    
								,reportProgress = {reportProgress}
								,blocksPerRead = {blocksPerRead}
								,rowSelection = {rowSelection}
								
								,xdfCompressionLevel = rxGetOption("xdfCompressionLevel")
								# ,variableSelection = #rxStepControl(method="stepwise", scope = ~ Age + Start + Number )); parameters that control aspects of stepwise regression; cube must be FALSE
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
			  ,predVarNames = c("NBCancelledProbabil", "NB_PredictionReal")
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
			   transforms = list(NB_Prediction = as.logical(round(NB_PredictionReal))),
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
