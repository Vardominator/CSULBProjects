# IMPORTLIBRARY
#install.packages("e1071")
library(e1071)
#install.packages("ggplot2")
library(ggplot2)





abalone_df <- read.table("abalone.data", sep = ",", header = FALSE)
colnames(abalone_df) <- c("Sex", "Length", "Diam", "Height", "Whole", "Shucked", "Viscera", "Shell", "Rings")

degrees <- c(1:3)
costs <- c(10 ^ (-1:2))
cross_fold <- 5
best_cv_accuracy <- 0
best_predictions <- data.frame()

# For each combination perform: 5-fold cross validation and training accuracy from training over the entire data set
accuracies_table <- data.frame("Degree" = integer(), "Cost" = numeric(), "CV Accuracy" = numeric(), "Entire DF Accuracy" = numeric(), stringsAsFactors = FALSE)

for (d in degrees) {
    for (c in costs) {

        current_model_entire_df <- svm(Rings ~ ., data = abalone_df, kernel = "polynomial", degree = d, type = "C-classification", cost = c)
        predictions <- predict(current_model_entire_df, abalone_df[, - length(abalone_df)])
        accuracy_total <- 100 * mean(predictions == abalone_df[, length(abalone_df)])

        current_model <- svm(Rings ~ ., data = abalone_df, kernel = "polynomial", degree = d, type = "C-classification", cost = c, cross = cross_fold)

        accuracies_table[nrow(accuracies_table) + 1,] <- c(d, c, current_model$tot.accuracy, accuracy_total)
        
        if (current_model$tot.accuracy > best_cv_accuracy) {
            best_cv_accuracy <- current_model$tot.accuracy
            best_predictions <- predict(current_model, abalone_df[,-length(abalone_df)])
        }
    
    }
}

# SORT BY INCREASING COMPLEXITY
accuracies_table <- accuracies_table[order(-accuracies_table$Cost),]
accuracies_table <- accuracies_table[order(accuracies_table$Degree),]

# combination with the CV highest accuracy
max_combination <- accuracies_table[which.max(accuracies_table$CV.Accuracy),]


best_pred_int <- as.integer(best_predictions)

# for the best classifier in the table, provide the average distance of the predicted class from the true class
class_distances <- abs(best_pred_int - abalone_df$Rings)
average_distance <- mean(class_distances)

# provide a histogram that shows the frequency of how often a prediction is m rings away from the true number of rings
hist(class_distances, main="Histogram of Class Distances")


# make all less than 5 = 5
abalone_df$Rings[abalone_df$Rings < 5] <- 5
# make all greater than 14 = 14
abalone_df$Rings[abalone_df$Rings > 14] <- 14

f1 <- list(c(0:9), c(10:30))
f2 <- list(c(0:7), c(8:9))
f3 <- list(c(0:5), c(6:7))
f4 <- list(c(8), c(9))
f5 <- list(c(6), c(7))
f6 <- list(c(10:11), c(12:30))
f7 <- list(c(12:13), c(14:30))
f8 <- list(c(10), c(11))
f9 <- list(c(12), c(13))


classifier_bounds <- list(f1, f2, f3, f4, f5, f6, f7, f8, f9)

binary_classifier_table <- data.frame("Description" = character(), "Dataset Size" = integer(), "Degree" = numeric(), "Cost" = numeric(),
                                      "Average CV Accuracy" = numeric(), "Best Accuracy" = numeric(), stringsAsFactors = FALSE)

binary_classifier_models <- list()

for (bound in classifier_bounds) {

    # CALCULATE BEST PARAMATERS GIVEN THE BINARY PARTITION BOUNDS AND APPEND TO CLASSIFIER PARAMS DATAFRAME
    negative_class <- bound[[1]]
    positive_class <- bound[[2]]

    description <- ""

    if (length(negative_class) > 2) {
        description <- paste(description, "<= ", negative_class[length(negative_class)])
    } else if (length(negative_class) == 2) {
        description <- paste(description, negative_class[1], "-", negative_class[2])
    } else {
        description <- paste(description, negative_class[1])
    }
    description <- paste(description, " vs ")
    if (length(positive_class) > 2) {
        description <- paste(description, ">= ", positive_class[length(positive_class)])
    } else if (length(positive_class) == 2) {
        description <- paste(description, positive_class[1], "-", positive_class[2])
    } else {
        description <- paste(description, positive_class[1])
    }

    df <- abalone_df

    df <- df[df$Rings %in% negative_class | df$Rings %in% positive_class,]

    df$Rings[df$Rings %in% negative_class] <- -1
    df$Rings[df$Rings %in% positive_class] <- 1

    average_accuracy <- 0
    best_accuracy <- 0
    best_d <- 0
    best_c <- 0
    cross_fold <- 5
    num_of_combos <- length(costs) * length(degrees)
    best_model <- 0

    for (d in degrees) {
        for (c in costs) {

            current_model <- svm(Rings ~ ., data = df, kernel = "polynomial", degree = d, type = "C-classification", cost = c, cross = cross_fold)
            average_accuracy = average_accuracy + current_model$tot.accuracy

            if (current_model$tot.accuracy > best_accuracy) {
                best_accuracy <- current_model$tot.accuracy
                best_d <- d
                best_c <- c
                best_model <- current_model
            }

        }
    }

    binary_classifier_table[nrow(binary_classifier_table) + 1,] <- c(description, nrow(df), best_d, best_c, average_accuracy / num_of_combos, best_accuracy)
    binary_classifier_models[[length(binary_classifier_models) + 1]] <- best_model
    
}

print(binary_classifier_table)
print(binary_classifier_models)



final_predictions <- c()

for (i in 1:nrow(abalone_df)) {
    # <= 9 or >= 10
    model <- binary_classifier_models[[1]]
    prediction <- predict(model, abalone_df[i, - length(abalone_df)])

    if (prediction == 1) {
        # prediction >= 10

        # 10-11 or >= 12
        model <- binary_classifier_models[[6]]
        prediction <- predict(model, abalone_df[i, - length(abalone_df)])

        if (prediction == 1) {
            # prediction >= 12

            # 12-13 or >= 14
            model <- binary_classifier_models[[7]]
            prediction <- predict(model, abalone_df[i, - length(abalone_df)])

            if (prediction == 1) {
                # final prediction is 14
                final_predictions <- c(final_predictions, 14)

            } else {
                # 12 or 13
                model <- binary_classifier_models[[9]]
                prediction <- predict(model, abalone_df[i, - length(abalone_df)])

                if (prediction == 1) {
                    # final prediction is 13
                    final_predictions <- c(final_predictions, 13)

                } else {
                    # final prediction is 12
                    final_predictions <- c(final_predictions, 12)
                }

            }

        } else {
            # prediction 10-11

            model <- binary_classifier_models[[8]]
            prediction <- predict(model, abalone_df[i, - length(abalone_df)])

            if (prediction == 1) {
                # final prediction is 11
                final_predictions <- c(final_predictions, 11)
            } else {
                # final prediction is 10
                final_predictions <- c(final_predictions, 10)
            }
        }

    } else if (prediction == -1){
        # prediction <= 9

        # <= 7 or 8-9
        model <- binary_classifier_models[[2]]
        prediction <- predict(model, abalone_df[i, - length(abalone_df)])

        if (prediction == 1) {
            # prediction 8-9

            model <- binary_classifier_models[[4]]
            prediction <- predict(model, abalone_df[i, - length(abalone_df)])

            if (prediction == 1) {
                # final prediction is 9
                final_predictions <- c(final_predictions, 9)
            } else {
                # final prediction is 8
                final_predictions <- c(final_predictions, 8)
            }

        } else {
            # prediction <= 7

            # <= 5 or 6-7
            model <- binary_classifier_models[[3]]
            prediction <- predict(model, abalone_df[i, - length(abalone_df)])

            if (prediction == 1) {
                # prediction 6-7

                model <- binary_classifier_models[[5]]
                prediction <- predict(model, abalone_df[i, - length(abalone_df)])
            
                if (prediction == 1) {
                    # final prediction is 7
                    final_predictions <- c(final_predictions, 7)
                } else {
                    # final prediction is 6
                    final_predictions <- c(final_predictions, 6)
                }

            } else {
                # final prediction is 5
                final_predictions <- c(final_predictions, 5)
            }
        }

    }

}



binary_class_distances <- abs(final_predictions - abalone_df$Rings)
binary_average_distance <- mean(binary_class_distances)

hist(binary_class_distances, main = "Histogram of Binary Class Distances")





# EXERCISE 4
reg_df <- read.csv("Exercise-4.csv")

reg_cross_fold <- 5
reg_costs <- c(10 ^ (-1:3))
epsilons <- c(1, 1.25, 1.5, 1.75)
lowest_mse <- 1000
best_reg_c <- 0
best_reg_e <- 0
best_reg_model <- 0

reg_accuracies_table <- data.frame("Epsilon" = integer(), "Cost" = numeric(), "CV MSE" = numeric(), "Entire DF MSE" = numeric(), stringsAsFactors = FALSE)

# epsilon values 1.5 and 1.75
for (c in reg_costs) {
    for (e in epsilons) {

        reg_model_entire <- svm(Y ~ X, reg_df, kernel = "polynomial", degree = 2, type = "eps-regression", epsilons = e, costs = c)
        mse_entire <- mean(reg_model_entire$residuals^2)

        reg_model <- svm(Y ~ X, reg_df, kernel = "polynomial", degree = 2, type = "eps-regression", epsilons = e, costs = c, cross = reg_cross_fold)

        reg_accuracies_table[nrow(reg_accuracies_table) + 1,] <- c(e, c, reg_model$tot.MSE, mse_entire)

        if (reg_model$tot.MSE < lowest_mse) {
            lowest_mse = reg_model$tot.MSE
            best_reg_c <- c
            best_reg_e <- e
            best_reg_model <- reg_model
        }
    }
}


reg_accuracies_table <- reg_accuracies_table[order(reg_accuracies_table$Epsilon),]
reg_accuracies_table <- reg_accuracies_table[order(reg_accuracies_table$Cost),]

# combination with the CV highest accuracy
reg_max_combination <- reg_accuracies_table[which.min(reg_accuracies_table$CV.MSE),]

plot(reg_df, pch = 16)

test_data <- data.frame(seq(0, 10, length.out = 1000))
colnames(test_data) <- c("X")

predicted <- predict(best_reg_model, test_data)
points(test_data$X, predicted, col = "red", pch = 4)
title("Data Points with Fitted Line")


#EXERCISE 6
best_reg_c_abalone <- 0
best_reg_e_abalone <- 0
best_reg_d_abalone <- 0
best_reg_model_abalone <- 0
lowest_mse_abalone <- 1000
best_reg_model_abalone <- 0

for (c in reg_costs) {
    for (e in epsilons) {
        for (d in degrees) {

            reg_model <- svm(Rings ~ ., abalone_df, kernel = "polynomial", degree = d, type = "eps-regression", epsilons = e, costs = c, cross = reg_cross_fold)

            if (reg_model$tot.MSE < lowest_mse_abalone) {
                lowest_mse_abalone = reg_model$tot.MSE
                best_reg_c_abalone <- c
                best_reg_e_abalone <- e
                best_reg_d_abalone <- d
                best_reg_model_abalone <- reg_model
            }
        }
    }
}

predicted_reg_abalone <- predict(best_reg_model_abalone, abalone_df[, - length(df)])
reg_abalone_distances <- abs(predicted_reg_abalone - abalone_df[, length(df)])
average_distance_reg_abalone <- mean(reg_abalone_distances)

hist(binary_class_distances, main = "Histogram of Binary Class Distances")