# IMPORT LIBRARY
#install.packages("e1071")
library(e1071)
#install.packages("ggplot2")
library(ggplot2)
#install.packages("data.tree")
library(data.tree)

abalone_df <- read.table("abalone.data", sep = ",", header = FALSE)
colnames(abalone_df) <- c("Sex", "Length", "Diam", "Height", "Whole", "Shucked", "Viscera", "Shell", "Rings")

degrees <- c(1:3)
costs <- c(10 ^ (-1:2))
cross_fold <- 5
best_accuracy <- 0
best_predictions <- data.frame()

# For each combination perform: 5-fold cross validation and training accuracy from training over the entire data set
accuracies_table <- data.frame("Degree" = integer(), "Cost" = numeric(), "Accuracy" = numeric(), stringsAsFactors = FALSE)

for (d in degrees) {
    for (c in costs) {

        current_model <- svm(Rings ~ ., data = abalone_df, kernel = "polynomial", degree = d, type = "C-classification", cost = c, cross = cross_fold)
        accuracies_table[nrow(accuracies_table) + 1, ] <- c(d, c, current_model$tot.accuracy)
        
        if (current_model$tot.accuracy > best_accuracy) {
            best_accuracy <- current_model$tot.accuracy
            best_predictions <- predict(current_model, abalone_df[,-length(abalone_df)])
        }
    
    }
}

# SORT BY INCREASING COMPLEXITY
accuracies_table <- accuracies_table[order(-accuracies_table$Cost),]
accuracies_table <- accuracies_table[order(accuracies_table$Degree),]

# combination with the highest accuracy
max_combination <- accuracies_table[which.max(accuracies_table$Accuracy),]


best_pred_int <- as.integer(best_predictions)

# for the best classifier in the table, provide the average distance of the predicted class from the true class
class_distances <- abs(best_pred_int - abalone_df$Rings)
average_distance <- mean(class_distance)

# provide a histogram that shows the frequency of how often a prediction is m rings away from the true number of rings
hist(class_distances)


binary_classifier_table <- data.frame("Description" = character(), "Dataset Size" = integer(), "Degree" = numeric(), "Cost" = numeric(), 
                                      "Average CV Accuracy" = numeric(), "Best Accuracy" = numeric(), stringsAsFactors = FALSE)
max_rings <- max(abalone_df$Rings)


f1 <- list(c(0:9), c(10:max_rings))
f2 <- list(c(0:7), c(8:9))
f3 <- list(c(0:5), c(6:7))
f4 <- list(c(8), c(9))
f5 <- list(c(6), c(7))
f6 <- list(c(10:11), c(12:max_rings))
f7 <- list(c(12:13), c(14:max_rings))
f8 <- list(c(10), c(11))
f9 <- list(c(12), c(13))




# exercise 2: best learning-parameter(BLP) with binary classifiers
binary_classifier <- function(df, degrees, costs, negative_class, positive_class) {
    # given the dataframe and bounds, degrees, and costs, will return
    #   the size of the subset, degree, cost, average CV accuracy of the BLP combinations,
    #   and the best accuracy
}
