########################
#### Initialisation ####
########################

# Installing required Libraries
if (!("rpart.plot" %in% installed.packages()[,1])) {
	install.packages(c("rpart.plot"))
}
if (!("rattle" %in% installed.packages()[,1])) {
	install.packages(c("rattle"))
}

#Loading required Libraries
library(rpart) #No installation needed, is already installed
library(rpart.plot)
library(rattle)
library(RevoTreeView) #No installation needed, is already installed
library(MicrosoftML)  #No installation needed, is already installed

rxSetComputeContext(computeContext = RxLocalSeq())

True = TRUE
true = TRUE
False = FALSE
false = FALSE
RowsPerRead <- {0}
strXDF <- {1}
strDesktop <- gsub("\\", "/", file.path(Sys.getenv("USERPROFILE"), "Desktop", fsep = "\\"), fixed = TRUE)
sqlConnString <- {2}
strGraphs <- {3}

#################
### Functions ###
#################
Spaces <- function(NumOfSpaces) {
  str <- ""

  if (!is.null(NumOfSpaces)) {
    if (NumOfSpaces > 0) {
      for (i in 1:NumOfSpaces) {
        str = paste(str, " ", sep = "")
      }
    }
  }

  return(str)
}

insertRow <- function(existingDF, newrow, r) {
  existingDF[seq(r+1,nrow(existingDF)+1),] <- existingDF[seq(r,nrow(existingDF)),]
  existingDF[r,] <- newrow
  existingDF
}

#####################################
### Creating the confusion matrix ###
#####################################
ConfMatrix <- function(ActAndPredValues, RoundAt) {
  ActAndPredValues <- as.data.frame(ActAndPredValues)
  irow <- 1

  #Both 0 and 1 are needed as Factor Levels, but settings them manually with Levels(x) = y is changing the underlying values
  #(i.e. 0 to 1 or 1 to 0); hence this is needed for it to work properly when there are no 0s or 1s in prediction or real values
  if (nrow(ActAndPredValues) >= irow) {
    FirstNum <- levels(ActAndPredValues[[1]])[1]
    if (FirstNum == "0") {
      levels(ActAndPredValues[[1]]) <- cbind("0", "1")
    } else {
      levels(ActAndPredValues[[1]]) <- cbind("1", "0")
    }
    FirstNum <- levels(ActAndPredValues[[2]])[1]
    if (FirstNum == "0") {
      levels(ActAndPredValues[[2]]) <- cbind("0", "1")
    } else {
      levels(ActAndPredValues[[2]]) <- cbind("1", "0")
    }
    if (!((ActAndPredValues[irow, 1] == 0) && (ActAndPredValues[irow, 2] == 0))) {
      ActAndPredValues <- insertRow(ActAndPredValues, cbind(0, 0, 0), irow)
    }
  } else {
    ActAndPredValues <- rbind(ActAndPredValues, c(0, 0, 0))
  }
  irow = irow + 1

  if (nrow(ActAndPredValues) >= irow) {
    FirstNum <- levels(ActAndPredValues[[1]])[1]
    if (FirstNum == "0") {
      levels(ActAndPredValues[[1]]) <- cbind("0", "1")
    } else {
      levels(ActAndPredValues[[1]]) <- cbind("1", "0")
    }
    FirstNum <- levels(ActAndPredValues[[2]])[1]
    if (FirstNum == "0") {
      levels(ActAndPredValues[[2]]) <- cbind("0", "1")
    } else {
      levels(ActAndPredValues[[2]]) <- cbind("1", "0")
    }
    if (!((ActAndPredValues[irow, 1] == 1) && (ActAndPredValues[irow, 2] == 0))) {
      ActAndPredValues <- insertRow(ActAndPredValues, cbind(1, 0, 0), irow)
    }
  } else {
    ActAndPredValues <- rbind(ActAndPredValues, c(1, 0, 0))
  }
  irow = irow + 1

  if (nrow(ActAndPredValues) >= irow) {
    FirstNum <- levels(ActAndPredValues[[1]])[1]
    if (FirstNum == "0") {
      levels(ActAndPredValues[[1]]) <- cbind("0", "1")
    } else {
      levels(ActAndPredValues[[1]]) <- cbind("1", "0")
    }
    FirstNum <- levels(ActAndPredValues[[2]])[1]
    if (FirstNum == "0") {
      levels(ActAndPredValues[[2]]) <- cbind("0", "1")
    } else {
      levels(ActAndPredValues[[2]]) <- cbind("1", "0")
    }
    if (!((ActAndPredValues[irow, 1] == 0) && (ActAndPredValues[irow, 2] == 1))) {
      ActAndPredValues <- insertRow(ActAndPredValues, cbind(0, 1, 0), irow)
    }
  } else {
    ActAndPredValues <- rbind(ActAndPredValues, c(0, 1, 0))
  }
  irow = irow + 1

  if (nrow(ActAndPredValues) >= irow) {
    FirstNum <- levels(ActAndPredValues[[1]])[1]
    if (FirstNum == "0") {
      levels(ActAndPredValues[[1]]) <- cbind("0", "1")
    } else {
      levels(ActAndPredValues[[1]]) <- cbind("1", "0")
    }
    FirstNum <- levels(ActAndPredValues[[2]])[1]
    if (FirstNum == "0") {
      levels(ActAndPredValues[[2]]) <- cbind("0", "1")
    } else {
      levels(ActAndPredValues[[2]]) <- cbind("1", "0")
    }
    if (!((ActAndPredValues[irow, 1] == 1) && (ActAndPredValues[irow, 2] == 1))) {
      ActAndPredValues <- insertRow(ActAndPredValues, cbind(1, 1, 0), irow)
    }
  } else {
    ActAndPredValues <- rbind(ActAndPredValues, c(1, 1, 0))
  }

  TN <- ActAndPredValues$Counts[1]
  FN <- ActAndPredValues$Counts[2]
  FP <- ActAndPredValues$Counts[3]
  TP <- ActAndPredValues$Counts[4]

  MaxItemLength <- max(nchar(ActAndPredValues$Counts))

  strConfMatrix <- paste(Spaces(nchar("Pred_Value  ")), "Actual_Value", Spaces(((2 * MaxItemLength) + nchar(paste("  "))) - nchar("Actual_Value")), sep = "")
  strConfMatrix <- rbind(strConfMatrix, paste("Pred_Value  " , Spaces(MaxItemLength - 1) , "0  " , Spaces(MaxItemLength - 1) , "1", sep = ""))
  strConfMatrix <- rbind(strConfMatrix, paste(Spaces(nchar("Pred_Value  ") - 3), "0  " , Spaces(MaxItemLength - nchar(TN)), TN, "  ",
                                              Spaces(MaxItemLength - nchar(FN)), FN, sep = ""))
  strConfMatrix <- rbind(strConfMatrix, paste(Spaces(nchar("Pred_Value  ") - 3), "1  " , Spaces(MaxItemLength - nchar(FP)), FP, "  ",
                                              Spaces(MaxItemLength - nchar(TP)), TP, sep = ""))
  ActAndPredValues <- as.data.frame(ActAndPredValues)
  ActAndPredValues$Result <- c("True Negative", "False Negative", "False Positive", "True Positive")
  ActAndPredValues$PCT <- round(ActAndPredValues$Counts / sum(ActAndPredValues$Counts), RoundAt) * 100
  ActAndPredValues$Rates <- round(c(TN / (TN + FP),
                                    FN / (FN + TP),
                                    FP / (TN + FP),
                                    TP / (FN + TP)), RoundAt)
  names(ActAndPredValues) <- c("Actual Value", "Predicted Value", "Cases", "Results", "Percentage", "Rates")
  retList <- list(strConfMatrix, ActAndPredValues)
  return(retList)
}

###############################
### Printing all Statistics ###
###############################
Statistics <- function(ActAndPredValues, RoundAt, DataVarOrPath) {
  TN <- ActAndPredValues$Counts[1]
  FN <- ActAndPredValues$Counts[2]
  FP <- ActAndPredValues$Counts[3]
  TP <- ActAndPredValues$Counts[4]

  tmp <- rxSummary(~LabelFactorial, data = DataVarOrPath)$categorical
  MaxClass <- max(tmp[[1]][[2]])
  MinClass <- min(tmp[[1]][[2]])

  P0 <- (TP + TN) / (TN + FN + FP + TP)
  Ma <- ((TP + FP) * (TP + FN)) / (TN + FN + FP + TP)
  Mb <- ((FN + TN) * (FP + TN)) / (TN + FN + FP + TP)
  Pe <- (Ma + Mb) / (TN + FN + FP + TP)

  Stat <- list(
    ConfusionMatrix = ConfMatrix(ActAndPredValues, RoundAt),
    TotalPredictionPercentages = data.frame(Correctly = round(100 * (TN + TP) / sum(ActAndPredValues$Counts), RoundAt),
                                            Incorrectly = round(100 * (FN + FP) / sum(ActAndPredValues$Counts), RoundAt)),
    Measures = data.frame(
      F1 = round((2 * TP) / ((2 * TP) + FP + FN), RoundAt),
      G = round(sqrt((TP / (TP + FP)) * (TP / (TP + FN))), RoundAt),
      PhiMCC = round((((TP * TN) - (FP * FN)) / sqrt((TP + FP) * (TP + FN) * (TN + FP) * (TN + FN))), RoundAt),
      CohensK = round((P0 - Pe) / (1 - Pe), RoundAt),
      YoudensJ = round((TP/(TP+FN)) + (TN/(TN+FP)) - 1, RoundAt)
    ),
    Rates = data.frame(
      Accuracy = round((TN + TP) / (TN + FN + FP + TP), RoundAt),
      BalancedAccuracy = round(((TP / (TP + FN)) + (TN / (TN + FP))) / 2, RoundAt),
      DetectionRate = round(TP / (TN + FN + FP + TP), RoundAt),
      MisclassRate = round((FP + FN) / (TN + FN + FP + TP), RoundAt),
      SensitRecallTPR = round(TP / (TP + FN), RoundAt),
      FPR = round(FP / (FP + TN), RoundAt),
      SpecificityTNR = round(TN / (TN + FP), RoundAt),
      FNR = round(FN / (FN + TP), RoundAt),
      PrecisionPPV1 = round(TP / (TP + FP), RoundAt),
      PPV2 = round(((TP / (TP + FN) * (FP + TP) / (TN + FN + FP + TP)) / (TP / (TP + FN) * (FP + TP) / (TN + FN + FP + TP)) + (1 - (TN / (TN + FP))) * (1 - ((FP + TP) / (TN + FN + FP + TP)))), RoundAt),
      NPV1 = round(TN / (TN + FN), RoundAt),
      NPV2 = round((TN / (TN + FP) * (1 - (FP + TP) / (TN + FN + FP + TP))) / ((1 - TP / (TP + FN)) * (FP + TP) / (TN + FN + FP + TP) + TN / (TN + FP) * (1 - (FP + TP) / (TN + FN + FP + TP))), RoundAt),
      FDR = round(FP / (FP + TP), RoundAt),
      NullErrorRate = round(MinClass / MaxClass, RoundAt),
      Prevalence = round((FP + TP) / (TN + FN + FP + TP), RoundAt)
    )
  )
  return(Stat)
}