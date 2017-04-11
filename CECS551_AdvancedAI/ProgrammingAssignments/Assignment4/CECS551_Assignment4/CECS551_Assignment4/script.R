# IMPORT LIBRARY
library(e1071)
library(ggplot2)

abalone_df <- read.table("abalone.data", sep = ",", header = FALSE)
colnames(abalone_df) <- c("Sex", "Length", "Diam", "Height", "Whole", "Shucked", "Viscera", "Shell", "Rings")

degrees <- c(1:3)
costs <- c(10 ^ (-1:2))
cross_fold <- 5
best_accuracy <- 0
best_predictions <- data.frame()

# For each combination perform: 5-fold cross validation and training accuracy from training over the entire data set
accuracies_table <- data.frame("Degree" = integer(), "Cost" = integer(), "Accuracy" = numeric(), stringsAsFactors = FALSE)

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