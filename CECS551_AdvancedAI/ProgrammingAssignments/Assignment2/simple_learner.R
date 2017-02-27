simple_learner <- function(df){
    positive_vectors <- df[df$X6 == 1,]
    positive_vectors <- subset(positive_vectors, select = -c(X6))
    negative_vectors <- df[df$X6 == -1,]
    negative_vectors <- subset(negative_vectors, select = -c(X6))
    c_plus <- colMeans(positive_vectors)
    c_minus <- colMeans(negative_vectors)
    w <- c_plus - c_minus
    c <- (c_plus + c_minus) / 2
    b <- sum(w * c)
    return (c(w, b))
}