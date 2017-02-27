perceptron_learner <- function(df){
    
    x <- subset(df, select = -c(length(df)))
    y <- subset(df, select = c(length(df)))

    w <- rep(0, length(df) - 1)
    b <- 0
    r <- max_magnitude(x)
    eta <- 0.5
    
    repeat{

        for (i in 1:nrow(df)){

            if (sign(sum(w * x[i, ]) - b) != y[i, ]){

                w <- w + eta * y[i, ] * x[i, ]
                b <- b - eta * y[i, ] * (r * r)
                print(b)

            }
        }

        classes <- apply(x, 1, function(x) sign(sum(w * x - b)))
        accuracy <- classes == y

        # STOP LEARNING ONLY WHEN 100% OF THE DATA IS ACCURATELY CLASSIFIED
        if(length(accuracy[accuracy == FALSE]) == 0){
            break
        }

    }

    return(c(w, b))

}

max_magnitude <- function(x){
    x_sqrd <- x * x
    x_sqrd_sum <- rowSums(x_sqrd)
    return (max(sqrt(x_sqrd_sum)))
}