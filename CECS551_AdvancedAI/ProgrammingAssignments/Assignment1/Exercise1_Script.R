"
  Title:      Using the Support Vector Machine functionality in the e1071 library to learn from
              the Mammographic Mass Data Set and make predictions using the learned model.

  Purpose:    Programming Assignment 1 for CECS 551 taught by Dr. Todd Ebert

  Author:     Varderes Barsegyan (CSULB ID: 016163470)

  Background: Although we have not tackeld SVM's in great detail, this exercise gave us the
              opportunity to become familiarized with the SVM functionality in the e1071 library.
              The library includes various machine learning algorithms, but for this exericise
              I used the svm and tune.svm function.  The tune.svm function was used to fine tune
              the 'cost' and 'gamma' parameters by seeing how the svm performed with a set of 
              these parameters.
              I used the Mammographic Masses Data Set provided by UC Irvine by loading it as a
              dataframe in my R script.  I gave the user the option to rid the dataset of rows
              containing '?' or replacing '?' with -1. The results were not identical mainly
              because more data is available when using the -1 replacement.
              Finally, I ran svm using three different kernels: linear, 2nd order polynomial,
              and (for fun) a 3rd order polynomial. This was all done using two-thirds of the
              dataset to train the svm model and the remaining one-third as a test set. The 
              accuracy was determined by inputting the test set into the learned model and examining
              how well it performed.

  Results:    A random run when removing rows with NA gave the following results:
                  Accuracy using linear classifier:  0.8188406 
                  Accuracy using polynomial classifier with degree 2:  0.7355072 
                  Accuracy using polynomial classifier with degree 3:  0.8007246

              A random run when replacing NA with -1:
                  Accuracy using linear classifier:  0.815625 
                  Accuracy using polynomial classifier with degree 2:  0.721875 
                  Accuracy using polynomial classifier with degree 3:  0.825
          

  Conclusion: It's interesting that the linear kernel performs better than the polynomial
              classifier. One would expect the contrary.  Yet, after doing some reading, I learned
              that a polynomial/nonlinear classifer is more likely to overfit, thus performing
              poorly on the test data.  This is termed 'generalization error'. A way this problem
              can be solved is by using more data. This leads me to the next point: it is possible
              that the data set is too small. There are 5 features for about 800 data points resulting
              in poor performance by a high-order kernel.

"



# SET WORKING DIRECTORY AND IMPORT NECESSARY LIBRARIES
setwd('C:/Users/barse/Google Drive/CSULB Spring 2017/CECS551/ProgrammingAssignments/Assignment1')
library(e1071)

# READ MAMMOGRAPHIC MASSES DATASET INTO A DATAFRAME, REPLACE '?' WITH NA
mammo_df <- read.csv(file='mammographic_masses.csv', header=FALSE, sep=',', na.strings="?")

# SET COLUMNS NAME OF DATAFRAME
column_names <- c('Birads','Age','Shape','Margin','Density','Severity')
colnames(mammo_df) <- column_names

# CHECK WHETHER TO REPLACE '?' WITH 'NA' OR '-1' (EXERCISE 1 AND 2 DISTINCTIONS)
exercise <- readline("Which exercise do you want to run? \n\n1: Rows with NA removed\n2: '?' replaced with -1\n")

if (exercise == "1") {
  # REMOVE ROWS WITH 'NA'
  mammo_df <- mammo_df[complete.cases(mammo_df),]
} else if (exercise == "2") {
  # REPLACE 'NA' WITH -1
  mammo_df[is.na(mammo_df)] <- -1
}

print(head(mammo_df))

# SHOW A SUMMARY OF THE DATAFRAME
print(summary(mammo_df))

# EXTRACT INDICES FROM DATAFRAME TO USE TO ISOLATE TRAINING AND TEST SETS
mammo_indices <- 1:nrow(mammo_df)

# A THIRD OF THE DATASET IS USED AS A TEST SET
testset_indices <- sample(mammo_indices, trunc(length(mammo_indices)/3))
testset <- mammo_df[testset_indices, ]

# THE REST OF THE DATASET WILL BE USED AS THE TRAINING SET
trainingset <- mammo_df[-testset_indices, ]

# THE FOLLOWING WAS USED TO TUNE THE SVM PARAMATERS: 'cost' and 'gamma'. IT WAS ONLY USED ONCE
# mammo_svm_tuner <- tune.svm(Severity~., data=trainingset, gamma=10^(-3:3), cost=10^(-3:3))


# RUN SVM WITH NEW PARAMETERS
mammo_svm <- svm(Severity~., data=trainingset, kernel='linear', type='C-classification', cost=1, gamma=0.1)
print(summary(mammo_svm))

# TEST MODEL AND SHOW SUMMARY
predictions <- predict(mammo_svm, testset[,-6])
predictions_table <- table(predictions=predictions, truth=testset[,6])
predictions_table

# PRINT ACCURACY WITH RESPECT TO THE TEST SET
mean = mean(predictions==testset[,6])[1]




# RUNNING WITH DEGREE 2 POLYNOMIAL KERNEL
mammo_svm_poly <- svm(Severity~., data=trainingset, kernel='polynomial', cost = 1000, gamma=0.01, degree=2, type='C-classification')
print(summary(mammo_svm_poly))

predictions_poly <- predict(mammo_svm_poly, testset[,-6])
predictions_table_poly <- table(predictions=predictions_poly, truth=testset[,6])
predictions_table_poly

mean_poly <- mean(predictions_poly==testset[,6])


# JUST FOR FUN, TRYING WITH A DEGREE 3 POLYNOMIAL KERNEL
mammo_svm_poly3 <- svm(Severity~., data=trainingset, kernel='polynomial', cost = 100, gamma=0.1, degree=3, type='C-classification')
print(summary(mammo_svm_poly3))

predictions_poly3 <- predict(mammo_svm_poly3, testset[,-6])
predictions_table_poly3 <- table(predictions=predictions_poly3, truth=testset[,6])
predictions_table_poly3

mean_poly3 <- mean(predictions_poly3==testset[,6])


# PRINT FINAL RESULTS
cat("Accuracy using linear classifier: ", mean, '\n')
cat("Accuracy using polynomial classifier with degree 2: ", mean_poly, '\n')
cat("Accuracy using polynomial classifier with degree 3: ", mean_poly3)
