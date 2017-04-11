# IMPORT LIBRARY
library(e1071)

abalone_df <- read.table("abalone.data", sep = ",", header = FALSE)
colnames(abalone_df) <- c("Sex", "Length", "Diam", "Height", "Whole", "Shucked", "Viscera", "Shell", "Rings")
#print(head(abalone_df))

# 20 different combinations of degree and c:
degrees <- c(1:4)
costs <- c(10 ^ (-1:3))
cross_fold <- 10
best_accuracy <- 0

# For each combination perform: 10-fold cross validation and training accuracy from training over the entire data set
accuracies_table <- data.frame("Degree" = integer(), "Cost" = integer(), "Accuracy" = numeric(), stringsAsFactors = FALSE)

for (d in degrees) {
    for (c in costs) {
        current_model <- svm(Rings ~ ., data = abalone_df, kernel = "polynomial", degree = d, type = "C-classification", cost = c, cross = cross_fold)

        accuracies_table[nrow(accuracies_table) + 1, ] <- c(d, c, current_model$tot.accuracy)
        print(accuracies_table)
        if (current_model$tot.accuracy > best_accuracy) {
            best_accuracy <- current_model$tot.accuracy
            
        }


    }
}

print(accuracies_table)