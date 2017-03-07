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
						
                        ,xVal = 10 #this controls the number of folds used to perform cross-validation. The default of 2 allows for some pruning; once you have closed in a model you may want to increase the value for final fitting and pruning.
                        ,maxDepth = 15 #this sets the maximum depth of any node of the tree. Computations grow rapidly more expensive as the depth increases, so we recommend a maxDepth of 10 to 15.
                        ,method = ClassMethod
                        ,maxNumBins = round(min(1001, max(101, sqrt(n_Train))))
						,cp = {8}
						,pruneCp = "auto"
						#,maxCompete = 0 #this specifies the number of “competitor splits” retained in the output. By default, rxDTree sets this to 0, but a setting of 3 or 4 can be useful for diagnostic purposes in determining why a particular split was chosen.
						#,useSurrogate = #0, 1 or 2
						#,maxSurrogate = 0 #this specifies the number of surrogate splits retained in the output. Again, by default rxDTree sets this to 0. Surrogate splits are used to assign an observation when the primary split variable is missing for that observation.
						#,surrogateStyle = #0 or 1, 0 penalises surrogates with many missing values
					    #,minSplit = #determines how many observations must be in a node before a split is attempted
					    #,minBucket =  #determines how many observations must remain in a terminal node.
					    #,fweights = #If duplicate rows have been eliminated, creating a new variable of how many duplicate rows were, then this Variable/Column can be used as Frequency Weight
					    #,pweights = #Probability weights for the observations
					    #,cost = c("") #a vector of non-negative costs, containing one element for each variable in the model. Defaults to one for all variables. When deciding which split	to choose, the improvement on splitting on a variable is divided by its cost
					    #,parms = list(loss = c(0, 3, 1, 0))
					   
						,reportProgress = {reportProgress}
						,blocksPerRead = {blocksPerRead}
						,rowSelection = {rowSelection}
						
                        ,xdfCompressionLevel = rxGetOption("xdfCompressionLevel")
						# ,variableSelection = #rxStepControl(method="stepwise", scope = ~ Age + Start + Number )); parameters that control aspects of stepwise regression; cube must be FALSE
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
