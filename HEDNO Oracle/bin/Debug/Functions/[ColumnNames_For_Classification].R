#Loading the Training & Test sets that we know must be there for this function to have been called.
Training_DS <- RxXdfData(file = paste(strXDF, "Training_DS.xdf", sep = ""))
Test_DS <- RxXdfData(file = paste(strXDF, "Test_DS.xdf", sep = ""))

#Getting the Column Names so that the user can select which columns to use for the Classification Process
TrainingSummary <- rxSummary(~., data = Training_DS)$sDataFrame
TrainingColumnNames <- TrainingSummary$Name

#Removing Columns which as not to be used for the Classification Process
ColumnsToBeRemoved <- c("Label", "ID_Erga", "LabelFactorial", "SelectionRatio", "MarkedForTest",
						"SAP_Typos_Pelati", "SAP_Eidos_Aitimatos")
TrainingColumnNames <- TrainingColumnNames [! TrainingColumnNames %in% ColumnsToBeRemoved]