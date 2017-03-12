###########################
### Supervised Learning ###
###########################

if (!(file.exists("{1}{6}{5}.RDS")) && (UseExistingModel)) {
  RDSCreatedOutOfNecessity <- TRUE
} else {
  RDSCreatedOutOfNecessity <- FALSE
}

###############################################################
### 2) Creating a Classification Model using Decision Trees ###
###############################################################

n_Train <- rxGetInfo(data = Training_DS)$numRows

if ((!(UseExistingModel)) || (!file.exists("{1}{6}{5}.RDS"))) {
	## Creating the Model ##
	rxDTreeElapsedTime <- system.time(
	  #Tweakable:
	  #--Dependent Variables
	  #--maxDepth
	  #--method [anova/class]
	  #//To be left as is on this experiment (though tweakable if needed)\\#
	  #--xval: Cross Validation Folds for Pruning, 10 is great (&very time consuming)
	  #--cp: 0 is the best (&most time consuming)
	  {6}{5} <- rxDTree(Label ~ {0}
						, data = paste(strXDF, "Training_DS.xdf", sep = "")
						
                        ,xVal = 10
                        ,maxDepth = 15
                        ,method = ClassMethod
                        ,maxNumBins = round(min(1001, max(101, sqrt(n_Train))))
						,cp = {8}
						,pruneCp = "auto"
						#,maxCompete = 0
						#,useSurrogate = 
						#,maxSurrogate = 0
						#,surrogateStyle = #0 or 1, 0 penalises surrogates with many missing values
					    #,minSplit = 
					    #,minBucket =  
					    #,fweights = 
					    #,pweights = 
					    #,cost = c("")
					    #,parms = list(loss = c(0, 3, 1, 0))
					   
						,reportProgress = {reportProgress}
						,blocksPerRead = {blocksPerRead}
						,rowSelection = {rowSelection}
						
                        ,xdfCompressionLevel = rxGetOption("xdfCompressionLevel")
						# ,variableSelection = #rxStepControl(method="stepwise", scope = ~ Age + Start + Number)
	  )
	)
# {6}{5} #The Tree model
# {6}{5}$variable.importance   #[vector] A numerical value representing how important the variable has been to the model
# {6}{5}$frame                 #[vector] var      n     wt          dev       yval   complexity ncompete nsurrogate

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
			  ,predVarNames = c({7}) #Different from class to anova
			  # ,computeStdErr = TRUE
			  # ,stdErrorsVarNames = "Tree_StdError"
			  # ,interval = "confidence" 
			  # ,intervalVarNames = c("Tree_LowerConfInterv", "Tree_UpperConfInterv")
			  # ,computeResiduals = TRUE
			  # ,residVarNames = "Tree_Residual"

			  ,blocksPerRead = rxGetOption("blocksPerRead")
			  ,reportProgress = rxGetOption("reportProgress")
			  ,xdfCompressionLevel = rxGetOption("xdfCompressionLevel")
	)
	rxDataStep(inData = paste(strXDF, "Test_DS.xdf", sep = ""),
			   outFile = paste(strXDF, "Test_DS.xdf", sep = ""),
			   transforms = list(Tree_Prediction{5} = as.logical(round(Tree_PredictionReal{5}))),
			   overwrite = TRUE
	)
	
	if(ClassMethod == "class") {
		rxDataStep(inData = paste(strXDF, "tmp.xdf", sep = ""),
				   outFile = paste(strXDF, "Test_DS.xdf", sep = ""),
				   varsToDrop = "0_prob",
				   overwrite = TRUE
		)
	}
	
	if (!(StatisticsMode)) {
		tmp <- rxDataStep(inData = Test_DS, varsToKeep = c("ID_Erga", "Tree_PredictionReal{5}", "Tree_Prediction{5}"))
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
		if (("Tree_Prediction{5}" %in% TestColumnNames) && ("Label" %in% TestColumnNames)) {
			LabelPredictionExist <- TRUE
			
			tmp <- rxCube(~ F(Label):F(Tree_Prediction{5}), data = paste(strXDF, "Test_DS.xdf", sep = ""))
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
		if (("Tree_Prediction{5}" %in% TestColumnNames) && ("Label" %in% TestColumnNames)) {
			PredictionRealExist <- TRUE
			
			if (ColumnsCombinations) { #REngine can't handle multiple Plot Windows, hence they should be saved as PNG and ran afterwards
				png(filename = paste(strGraphs, "{6}ROC{5}.png", sep = "") , width = 615, height = 520, units = "px")
			}
			
			rxRocCurve(actualVarName = "Label",
					   predVarName = "Tree_PredictionReal{5}",
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

if (ShowComplexityPlot) {
	# write.csv({6}{5}$cptable, "{6}{5} cptable.csv")
	printcp(rxAddInheritance({6}{5})) #Table of optimal prunings based on complexity
	plotcp(rxAddInheritance({6}{5}))
}

if (PlotTreeModel) {
	# Visualise the decision tree
	plot(createTreeView({6}{5}))
}

remove({6}{5})
