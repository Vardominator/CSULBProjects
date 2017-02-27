classify <- function(df, w, b){

    x <- subset(df, select = -c(length(df)))
    classes <- apply(x, 1, function(x) sign(sum(w * x - b)))

    return (classes)

}