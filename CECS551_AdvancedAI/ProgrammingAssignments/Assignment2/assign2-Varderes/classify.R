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