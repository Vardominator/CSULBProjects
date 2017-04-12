# exercise 2: best learning-parameter(BLP) with binary classifiers
binary_classifier_params <- function(df, degrees, costs, negative_class, positive_class) {
    # given the dataframe and bounds, degrees, and costs, will return
    #   the size of the subset, degree, cost, average CV accuracy of the BLP combinations,
    #   and the best accuracy

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

    return(list(description, nrow(df), best_d, best_c, average_accuracy / num_of_combos, best_accuracy, best_model))

}
