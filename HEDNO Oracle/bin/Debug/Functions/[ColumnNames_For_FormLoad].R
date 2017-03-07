#Checking Column Names for Form_Load to see if current Dataset is in Statistics Mode, etc.
{0} <- RxXdfData(file = paste(strXDF, "{0}.xdf", sep = ""))

{0}Summary <- rxSummary(~., data = {0})$sDataFrame
{0}ColumnNames <- {0}Summary$Name

isStatisticsXDF <- !("MarkedForTest" %in% {0}ColumnNames)