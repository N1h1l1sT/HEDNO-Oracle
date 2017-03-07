#Plotting the ROC Curve of multiple Prediction Models in a juxtaposing manner
rocOut <- rxRoc(actualVarName = "Label",
                predVarName = c({0}),
                data = paste(strXDF, "Test_DS.xdf", sep = "")
)

#Show ROC Information
rocOut
round(rxAuc(rocOut), {1})

plot(rocOut,
     title = "ROC Curve for Label",
     lineStyle = c("solid", "twodash", "dashed")
)

remove(rocOut)