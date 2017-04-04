"
    Title:      Building a simple learner and perceptron learner from scratch to demonstrate
                simple learning on a multidimensional, linearly separable dataset.
            
    Purpose:    Programming assignment 2 for CECS 551 taught by Dr. Todd Ebert

    Date due:   3/1/2017

    Author:     Varderes Barsegyan

"

simple_learner <- function(df){
    # Exercise 1:
    #   Computes necessary parameters for simple learning algorithm
    # 
    # Args:
    #   df: Dataframe which will be used for the learning model
    # 
    # Returns:
    #   The w, b parameters which define the decision surface
    
    # ISOLATE POSITIVE VECTORS
    positive_vectors <- df[length(df) == 1,]
    positive_vectors <- subset(positive_vectors, select = -c(length(df)))
    
    # ISOLATE NEGATIVE VECTORS
    negative_vectors <- df[length(df) == -1,]
    negative_vectors <- subset(negative_vectors, select = -c(length(df)))

    # CALCULATE NECESSARY VECTORS FOR DECISION SURFACE
    c_plus <- colMeans(positive_vectors)
    c_minus <- colMeans(negative_vectors)

    # CALCULATE W VECTOR
    w <- c_plus - c_minus

    # CALCULATE OFFSET VALUE
    c <- (c_plus + c_minus) / 2
    b <- sum(w * c)

    return (c(w, b))
}


perceptron_learner <- function(df){
    # Exercise 2:
    #   Computes necessary parameters for perceptron learning algorithm
    # 
    # Args:
    #   df: Dataframe which will be used for the learning model
    # 
    # Returns:
    #   The w, b parameters which define the decision surface

    # ISOLATE FEATURES AND CLASSES
    x <- subset(df, select = -c(length(df)))
    y <- subset(df, select = c(length(df)))

    # DECLARE PREPARATORY VARIABLES
    w <- rep(0, length(df) - 1)
    b <- 0
    r <- max_magnitude(x)
    eta <- 0.5
    
    repeat{

        # FOR EVERY DATA POINT CHECK FOR AN INCORRECT CLASSIFICATION
        for (i in 1:nrow(df)){

            # IF SO, UPDATE THE DECISION SURFACE PARAMETERS
            if (sign(sum(w * x[i, ]) - b) != y[i, ]){

                w <- w + eta * y[i, ] * x[i, ]
                b <- b - eta * y[i, ] * (r * r)

            }
        }

        # CHECK IF ANY OF THE DATA POINTS ARE INCORRECTLY CLASSIFIED
        classes <- apply(x, 1, function(x) sign(sum(w * x - b)))
        accuracy <- classes == y

        # STOP LEARNING ONLY WHEN 100% OF THE DATA IS ACCURATELY CLASSIFIED
        if(length(accuracy[accuracy == FALSE]) == 0){
            break
        }

    }

    return(c(w, b))

}


classify <- function(df, w, b){
    # Exercise 3:
    #   Classifies data points of dataframe given decision surface parameters
    # 
    # Args:
    #   df: Dataframe which will be used for the learning model
    #   w: orthogonal surface to decision surface
    #   b: bias term
    # 
    # Returns:
    #   classifications of every point in dataframe

    x <- subset(df, select = -c(length(df)))
    classes <- apply(x, 1, function(x) sign(sum(w * x - b)))

    return (classes)

}


max_magnitude <- function(x){
    # Helper function used to calculate the r param for perceptron learner
    # 
    # Args:
    #   x: Dataframe
    #
    # Returns:
    #   data point with the largest magnitude
    x_sqrd <- x * x
    x_sqrd_sum <- rowSums(x_sqrd)
    return (max(sqrt(x_sqrd_sum)))
}


# SET WORKING DIRECTORY HERE
# setwd('/home/varderes/Desktop/GitHub/CSULBProjects/CECS551_AdvancedAI/ProgrammingAssignments/Assignment2/assign2-Varderes')

# READ DATASET INTO DATAFRAME
df <- read.csv('Exercise-4.csv')

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
sl_accuracy <- length(sl_truth[sl_truth==TRUE]) / nrow(df)
cat("Accuracy of simple learner: ", sl_accuracy, '\n')

# EXERCISE 5: PERCEPTRON LEARNER ACCURACY
pl_accuracy <- length(pl_truth[pl_truth==TRUE]) / nrow(df)
cat("Accuracy of perceptron learner: ", pl_accuracy, '\n\n')


