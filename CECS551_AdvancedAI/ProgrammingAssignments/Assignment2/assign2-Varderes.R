# SET DIRECTORY
setwd('/home/varderes/Desktop/GitHub/CSULBProjects/CECS551_AdvancedAI/ProgrammingAssignments/Assignment2')

# READ DATASET INTO DATAFRAME
df <- read.csv('Exercise-4.csv')

# IMPORT NECESSARY FUNCTIONS
source('simple_learner.R')
source('perceptron_learner.R')
source('classify.R')

# EXERCISE 1: PARAMETERS OF SIMPLE LEARNER DECISION SURFACE
print("Running simple learner...")
SL_params <- simple_learner(df)
cat("Simple learner complete!", '\n\n')

print("Running perceptron learner...")
# EXERCISE 2: PARAMETERS OF PERCEPTRON LEARNER DECISION SURFACE
PL_params <- perceptron_learner(df)
cat("Perceptron learner complete!", '\n\n')

w_sl <- SL_params[-length(SL_params)]
b_sl <- SL_params[length(SL_params)]

print("Simple learner decision surface parameters: ")
cat("    w = ", w_sl, '\n')
cat("    b = ", b_sl, '\n\n')

PL_params <- unlist(PL_params)
w_pl <- PL_params[-length(PL_params)]
b_pl <- PL_params[length(PL_params)]

print("Perceptron learner decision surface parameters: ")
cat("     w = ", w_pl, '\n')
cat("     b = ", b_pl, '\n\n')

# EXERCISE 3: CLASSIFY DATAPOINTS USING LEARNER PARAMETERS
classes_sl <- classify(df, w_sl, b_sl)
classes_pl <- classify(df, w_pl, b_pl)

# ISOLATE CLASSES TO TEST FOR ACCURACY
y <- subset(df, select = c(length(df)))

sl_truth <- classes_sl == y
pl_truth <- classes_pl == y

# EXERCISE 4: SIMPLE LEARNER ACCURACY
sl_accuracy <- length(sl_truth[sl_truth==TRUE]) / 500
cat("Accuracy of simple learner: ", sl_accuracy, '\n')

# EXERCISE 5: PERCEPTRON LEARNER ACCURACY
pl_accuracy <- length(pl_truth[pl_truth==TRUE]) / 500
cat("Accuracy of perceptron learner: ", pl_accuracy, '\n\n')