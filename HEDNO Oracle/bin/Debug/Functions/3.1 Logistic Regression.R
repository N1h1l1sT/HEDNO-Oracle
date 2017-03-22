###########################
### Supervised Learning ###
###########################

if (!(file.exists("{1}{6}{5}.RDS")) && (UseExistingModel)) {
  RDSCreatedOutOfNecessity <- TRUE
} else {
  RDSCreatedOutOfNecessity <- FALSE
}

####################################################################
### 1) Creating a Classification Model using Logistic Regression ###
####################################################################

if ((!(UseExistingModel)) || (!file.exists("{1}{6}{5}.RDS"))) {
	## Creating the Model ##
	rxLogitElapsedTime <- system.time(
	  #Tweakable:
	  #--Dependent Variables
	  {6}{5} <- rxLogit(Label ~ {0}
						, data = paste(strXDF, "Training_DS.xdf", sep = "")
						, covCoef = {7}
						# ,maxIterations =
						# ,variableSelection = 
						# ,fweights = 
						# ,pweights = 
						,reportProgress = {reportProgress}
						,blocksPerRead = {blocksPerRead}
						,rowSelection = {rowSelection}
	  )
	)
	# summary({6}{5})
	#{6}{5}$coefficients     #[vector] Model's Coefficients
	#{6}{5}$covCoef          #[vector] variance-covariance matrix for the regression coefficient estimates
	#{6}{5}$condition.number #[value ] estimated reciprocal condition number of final weighted cross-product (X'WX) matrix
	#{6}{5}$aliased          #[vector] TRUE/FALSE of whether columns were dropped or not due to collinearity
	#{6}{5}$coef.std.error   #[vector] standard errors of the coefficients
	#{6}{5}$coef.t.value     #[vector] coefficients divided by their standard errors
	#{6}{5}$coef.p.value     #[vector] p-values for coef.t.values, using the normal distribution (Pr(>|z|))
	#{6}{5}$total.squares    #[value ] Y'Y of raw Y's
	#{6}{5}$df               #[value3] degrees of freedom, a 3-vector (p, n-p, p*), the last being the number of non-aliased coefficients
	#{6}{5}$deviance         #[value ] deviance minus twice the maximized log-likelihood (up to a constant)
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
			  ,predVarNames = "LogRe_PredictionReal{5}"
			  # ,computeStdErr = TRUE
			  # ,stdErrorsVarNames = "LogRe_StdError"
			  # ,interval = "confidence" 
			  # ,intervalVarNames = c("LogRe_LowerConfInterv", "LogRe_UpperConfInterv")
			  # ,computeResiduals = TRUE
			  # ,residVarNames = "LogRe_Residual"

			  ,blocksPerRead = rxGetOption("blocksPerRead")
			  ,reportProgress = rxGetOption("reportProgress")
			  ,xdfCompressionLevel = rxGetOption("xdfCompressionLevel")
	)

	rxDataStep(inData = paste(strXDF, "Test_DS.xdf", sep = ""),
			   outFile = paste(strXDF, "Test_DS.xdf", sep = ""),
			   transforms = list(LogRe_Prediction{5} = as.logical(round(LogRe_PredictionReal{5}))),
			   overwrite = TRUE
	)
	
	if (!(StatisticsMode)) {
		tmp <- rxDataStep(inData = Test_DS, varsToKeep = c("ID_Erga", "LogRe_PredictionReal{5}", "LogRe_Prediction{5}"))
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
		if (("LogRe_Prediction{5}" %in% TestColumnNames) && ("Label" %in% TestColumnNames)) {
			LabelPredictionExist <- TRUE
			
			tmp <- rxCube(~ F(Label):F(LogRe_Prediction{5}), data = paste(strXDF, "Test_DS.xdf", sep = ""))
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
		if (("LogRe_Prediction{5}" %in% TestColumnNames) && ("Label" %in% TestColumnNames)) {
			PredictionRealExist <- TRUE
			
			if (ColumnsCombinations) { #REngine can't handle multiple Plot Windows, hence they should be saved as PNG and ran afterwards
				png(filename = paste(strGraphs, "{6}ROC{5}.png", sep = "") , width = 615, height = 520, units = "px")
			}
			
			rxRocCurve(actualVarName = "Label",
					   predVarName = "LogRe_PredictionReal{5}",
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
