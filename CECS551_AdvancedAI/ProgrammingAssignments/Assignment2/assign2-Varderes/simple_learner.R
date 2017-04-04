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
    positive_vectors <- df[df$X6 == 1,]
    positive_vectors <- subset(positive_vectors, select = -c(X6))
    
    # ISOLATE NEGATIVE VECTORS
    negative_vectors <- df[df$X6 == -1,]
    negative_vectors <- subset(negative_vectors, select = -c(X6))

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