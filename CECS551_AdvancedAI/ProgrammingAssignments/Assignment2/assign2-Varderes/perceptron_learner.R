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

# HELPER FUNCTION USED TO CALCULATE THE r PARAM USED TO UPDATE b
max_magnitude <- function(x){
    x_sqrd <- x * x
    x_sqrd_sum <- rowSums(x_sqrd)
    return (max(sqrt(x_sqrd_sum)))
}