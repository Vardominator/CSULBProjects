binary_classifier_model <- function(df, degrees, costs, negative_class, positive_class) {

    df <- df[df$Rings %in% negative_class | df$Rings %in% positive_class,]

    df$Rings[df$Rings %in% negative_class] <- -1
    df$Rings[df$Rings %in% positive_class] <- 1

    average_accuracy <- 0
    best_accuracy <- 0
    best_d <- 0
    best_c <- 0
    cross_fold <- 5
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

    return(best_model)

}