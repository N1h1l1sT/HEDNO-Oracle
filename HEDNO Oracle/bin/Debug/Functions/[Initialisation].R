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
#Applied Predictive Modelling By Max Kuhn and Kjell Johnson ISBN 978-1-4614-6849-3 http://www.springer.com/gp/book/9781461468486
#In relation to Bayesian statistics, the sensitivity and specificity are the conditional probabilities, the prevalence is the prior, and the positive/negative predicted values are the posterior probabilities.
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
      #This is a weighted average of the true positive rate (recall) and precision, their harmonic mean.
      #F-score, like recall and precision, only considers the so-called positive predictions, with recall being the probability of predicting just the positive class, precision being the probability of a positive prediction being correct, and F-score equating these probabilities under the effective assumption that the positive labels and the positive predictions should have the same distribution and prevalence
      F1 = round((2 * TP) / ((2 * TP) + FP + FN), RoundAt),
      #indicates the central tendency or typical value of a set of numbers
      G = round(sqrt((TP / (TP + FP)) * (TP / (TP + FN))), RoundAt),
      #Matthews correlation coefficient is a measure of the quality of binary classifications. The MCC is in essence a correlation coefficient between the observed and predicted binary classifications; it returns a value between −1 and +1. A coefficient of +1 represents a perfect prediction, 0 no better than random prediction and −1 indicates total disagreement between prediction and observation. is generally regarded as a balanced measure which can be used even if the classes are of very different sizes.
      PhiMCC = round((((TP * TN) - (FP * FN)) / sqrt((TP + FP) * (TP + FN) * (TN + FP) * (TN + FN))), RoundAt),
      #A measure of how well the classifier performed as compared to how well it would have performed simply by chance. In other words, a model will have a high Kappa score if there is a big difference between the accuracy and the null error rate.
      CohensK = round((P0 - Pe) / (1 - Pe), RoundAt),
      #Youden's J statistic (also called Youden's index). estimates the probability of an informed decision.
      #Its value ranges from -1 to 1, and has a zero value when a diagnostic test gives the same proportion of positive results for groups with and without the disease, i.e the test is useless.
      #Youden's index is often used in conjunction with Receiver Operating Characteristic (ROC) analysis.[2] The index is defined for all points of an ROC curve, and the maximum value of the index may be used as a criterion for selecting the optimum cut-off point when a diagnostic test gives a numeric rather than a dichotomous result.
      YoudensJ = round((TP/(TP+FN)) + (TN/(TN+FP)) - 1, RoundAt)
    ),
    Rates = data.frame(
      #The accuracy paradox for predictive analytics states that predictive models with a given level of accuracy may have greater predictive power than models with higher accuracy. It may be better to avoid the accuracy metric in favour of other metrics such as precision and recall
      #Overall, how often is the classifier correct?
      Accuracy = round((TN + TP) / (TN + FN + FP + TP), RoundAt),
      BalancedAccuracy = round(((TP / (TP + FN)) + (TN / (TN + FP))) / 2, RoundAt),
      DetectionRate = round(TP / (TN + FN + FP + TP), RoundAt),
      #Overall, how often is it wrong?
      MisclassRate = round((FP + FN) / (TN + FN + FP + TP), RoundAt),
      #When it's actually yes, how often does it predict yes?
      SensitRecallTPR = round(TP / (TP + FN), RoundAt),
      #When it's actually no, how often does it predict yes? The proportion of all negatives that still yield positive test outcomes, i.e., the conditional probability of a positive test result given an event that was not present.
      FPR = round(FP / (FP + TN), RoundAt),
      #When it's actually no, how often does it predict no?
      SpecificityTNR = round(TN / (TN + FP), RoundAt),
      #The proportion of positives which yield negative test outcomes with the test, i.e., the conditional probability of a negative test result given that the condition being looked for is present
      FNR = round(FN / (FN + TP), RoundAt),
      #A description of random errors, a measure of statistical variability; when it predicts yes, how often is it correct?
      PrecisionPPV1 = round(TP / (TP + FP), RoundAt),
      #Very similar to precision, except that it takes prevalence into account. In the case where the classes are perfectly balanced (meaning the prevalence is 50%), the positive predictive value (PPV) is equivalent to precision
      PPV2 = round(((TP / (TP + FN) * (FP + TP) / (TN + FN + FP + TP)) / (TP / (TP + FN) * (FP + TP) / (TN + FN + FP + TP)) + (1 - (TN / (TN + FP))) * (1 - ((FP + TP) / (TN + FN + FP + TP)))), RoundAt),
      #When it predicts no, how often is it correct
      NPV1 = round(TN / (TN + FN), RoundAt),
      #Very similar to precision, except that it takes prevalence into account. In the case where the classes are perfectly balanced (meaning the prevalence is 50%), the negative predictive value (NPV2) is equivalent to NPV1
      NPV2 = round((TN / (TN + FP) * (1 - (FP + TP) / (TN + FN + FP + TP))) / ((1 - TP / (TP + FN)) * (FP + TP) / (TN + FN + FP + TP) + TN / (TN + FP) * (1 - (FP + TP) / (TN + FN + FP + TP))), RoundAt),
      #A way of conceptualizing the rate of type I errors in null hypothesis testing when conducting multiple comparisons
      FDR = round(FP / (FP + TP), RoundAt),
      #The accuracy paradox for predictive analytics states that predictive models with a given level of accuracy may have greater predictive power than models with higher accuracy. It may be better to avoid the accuracy metric in favour of other metrics such as precision and recall
      #This is how often you would be wrong if you always predicted the majority class. This can be a useful baseline metric to compare your classifier against. However, the best classifier for a particular application will sometimes have a higher error rate than the null error rate, as demonstrated by the Accuracy Paradox.
      NullErrorRate = round(MinClass / MaxClass, RoundAt),
      #How often does the yes condition actually occur in our sample?
      Prevalence = round((FP + TP) / (TN + FN + FP + TP), RoundAt)
    )
  )
  return(Stat)
}