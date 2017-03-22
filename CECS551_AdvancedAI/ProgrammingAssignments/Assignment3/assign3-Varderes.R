# Varderes Barsegyan

setwd('C:\\Users\\barse\\Desktop\\Github\\CSULBProjects\\CECS551_AdvancedAI\\ProgrammingAssignments\\Assignment3')
library(e1071)



#EXERCISE 1
partition <- function(df, alpha){
"
  Description: Partitions dataframe based on size of a. A can be a number between 0 and 1.
               The first partitioned dataframe is of size a * n, where n is the number of 
               rows of the dataframe. The second partitioned dataframe is of size n - a * n.
               The dataframes do not have common points.

  Arguments:   df: dataframe to be partitioned
               a: splitting point of the partition
"
  
  # NUMBER OF ROWS IN GIVEN DATAFRAME
  n <- nrow(df)
  
  # n * a RANDOM INDICES
  rand_indices <- sample(nrow(df), n * alpha, replace = FALSE)
  
  df1 <- df[rand_indices, ]
  df2 <- df[-rand_indices, ]
  
  return(list(df1, df2))
  
}



#EXERCISE 2
best_svm <- function(df, alpha, degree, cost){
"
  Description: Finds the most accurate SVM model for a given set of costs and degrees.
               Returns a list of degree, cost, and accuracy for which the accuracy was
               the highest.

  Arguments: df: dataframe to used to build model
             alpha: splitting point to be used to partition dataframe
             degree: vector of degrees to be used to build the models
             cost: vector of costs to be used to build the models
"
  
  df_split = partition(df, alpha)
  df1 <- df_split[[1]]
  df2 <- df_split[[2]]
  
  best_d = 0
  best_c = 0
  best_acc = 0
  
  for(d in degree){
    
    for(c in cost){
      
      model <- svm(Class~., data=df1, kernel="polynomial", 
                         degree=d, type="C-classification", cost=c)
      
      predictions <- predict(model, df2[,-length(df2)])
      accuracy <- mean(predictions==df2[,length(df2)])
      
      if(accuracy > best_acc){
        
        best_acc <- accuracy
        best_d <- d
        best_c <- c
        
      }
    
    }
  
  }
  
  return(list(best_d, best_c, best_acc))

}

# READ IN CARS DATASET 
cars_df <- read.table("car.data", sep=",", header=FALSE)

# RENAME THE LAST COLUMN TO "CLASS"
colnames(cars_df)[ncol(cars_df)] <- "Class"

# INITIALIZE LEARNING PARAMETERS
degree = c(1:4)
cost = c(10^(-1:4))
alpha = 0.8



#EXERCISE 3
cat("degree", "\t", "cost", "\t", "accuracy", "\n")
for(i in 1:10){
  model <- best_svm(cars_df, alpha, degree=degree, cost=cost)
  cat(model[[1]], "\t", model[[2]], "\t", model[[3]], "\n")
}

#EXERCISE 3 RESULTS
"
degree 	 cost 	 accuracy 
3 	     1000 	 0.9971098 
3 	     1000 	 1 
2 	     10000 	 1 
2 	     1000 	 1 
2 	     1000 	 1 
2 	     10000 	 0.9971098 
3 	     1000 	 0.9884393 
3 	     1000 	 0.9971098 
3 	     1000 	 1 
3 	     10000 	 0.9913295 
"



#EXERCISE 4
high_cost <- c(10^5)
degree <- 1

# KEEP RUNNING BEST_SVM UNTIL ACCURACY IS 100%
repeat{
  
  model <- svm(Class~., data=cars_df, kernel="polynomial", 
                          degree=degree, type="C-classification", cost=high_cost)
  
  predictions <- predict(model, cars_df[,-length(cars_df)])
  accuracy <- mean(predictions==cars_df[,length(cars_df)])
  
  if(accuracy==1){
    break
  }
  
  degree <- degree + 1
  
}
cat("Least degree with 100% accuracy: ", degree, "\n")

#EXERCISE 4 RESULT
"
Least degree with 100% accuracy:  2
"


#EXERCISE 5
best.svm.cross <- function(df, degree, cost, n){
"
  Description: Returns the model with the best accuracy given a degree vector, a cost vector
               and a cross validation parameters n.
  
  Arguments: df: dataframe to be used to build model
             degree: vector of degrees to be used to build the models
             cost: vector of costs to be used to build the models
             n: cross-validation parameter
            
"
  best_d = 0
  best_c = 0
  best_acc = 0
  
  for(d in degree){
    
    for(c in cost){
      
      cat(d, " - ", c, " - ", "\n")
      model <- svm(Class~., data=df, kernel="polynomial", degree=d,
                   type = "C-classification", cost=c, cross=n)
      
      if(model$tot.accuracy > best_acc){
        
        best_acc <- model$tot.accuracy
        best_d <- d
        best_c <- c
        
      }
      
    }
  }
  
  return(list(best_d, best_c, best_acc))

}


# READ IN BREAST CANCER DATA
cancer_df <- read.table("breast-cancer-wisconsin.data", sep=",", header=FALSE)

# RENAME THE LAST COLUMN TO "CLASS"
colnames(cancer_df)[ncol(cancer_df)] <- "Class"

# THE FIRST COLUMN IS THE "Sample code number", AN ID, WHICH IS NOT NECESSARY FOR CLASSIFICATION
# THUS, I WILL REMOVE IT
cancer_df <- cancer_df[,-1]


# INITIALIZE PARAMETERS
degree = c(1:4)
cost = c(10^(-1:4))



# EXERCISE 6
model_cross <- best.svm.cross(cancer_df, cost, degree, 10)
print(model_cross)

# EXERCISE 6 RESULTS & DISCUSSION
"
model_cross gives the following results for d, c, and accuracy:

c = 1
d = 3
accuracy = 96.56652


It appears that the SVM algorithm is reaching a maximum number of iterations. The following
message was printed after c = 1000 and d = 1: WARNING: reaching max number of iterations.

After doing some investigation, I learned that the SVM algorithm has a maximum number of
iterations when trying to solve a quadratic programming optimization to find the optimal
separating plane and hyperplanes.  This means that after a certain cost, such as 1000,
the algorithm has a difficult time separating the data even with higher degrees. Based on
these results the Wisconsin data is much more complex than the cars data.
"



# EXERCISE 7
bootstrap <- function(df, model, p, n){
"
  Description: Takes bootstrap samples of df and ensures that sampling is done with
               replacements. Runs each bootstrap through the given model and finds 
               the accuracy of the model. Then it sorts the accuracies. Finally,
               based on the probability of interests, returns the lower and upper
               of the confidence interval.
  
  Arguments: df: dataframe to bootstrapped
             model: previously trained used to predict bootstrapped samples
             p: probability of the model accuracy within confidence interval.
             n: number of bootstrap samples to be made
"

  accuracies <- c()
  
  for(i in 1:n){
    rand_indices <- sample(nrow(df), nrow(df), replace = TRUE)
    boot <- df[rand_indices, ]
    predictions <- predict(model, boot[,-length(boot)])
    accuracy <- mean(predictions==boot[,length(boot)])
    accuracies <- c(accuracies, accuracy)
  }
  
  accuracies <- sort(accuracies)
  
  # REMOVE LOWEST AND HIGHEST (100 * P)/2 PERCENT OF ACCURACIES
  rem <- 1 - p
  outskirt <- rem/2

  # THE REMAINING LOWEST AND HIGHEST VALUES ARE THE BOUNDS OF THE CONFIDENCE INTERVAL
  return(list(accuracies[outskirt * n + 1], accuracies[length(accuracies) - outskirt * n]))
  
}




#EXERCISE 8
model <- svm(Class~., data=cancer_df, kernel="polynomial", degree=3,
             type = "C-classification", cost=1, cross=10)

CI <- bootstrap(cancer_df, model, 0.90, 100)
cat("Upper bound: ", CI[[1]], " - ", "Lower bound: ", CI[[2]])


#EXERCISE 8 RESULTS
"
Upper bound:  0.8869814  -  Lower bound:  0.9270386
"



