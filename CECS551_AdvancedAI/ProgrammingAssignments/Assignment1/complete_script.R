setwd('C:/Users/barse/Google Drive/CSULB Spring 2017/CECS551/ProgrammingAssignments/Assignment1')

head
column_names <- c('Birads','Age','Shape','Margin','Density','Severity')

colnames(mammographic_data) <- column_names
summary(mammographic_data)

mammographic_data[mammographic_data=='?'] <- NA

mammographic_data_complete <- mammographic_data[complete.cases(mammographic_data),]

library(e1071)

plot(mammographic_data_complete)


mammo_row_count <- dim(mammographic_data_complete)[1]
s <- sample(mammo_row_count, 600)

mammo_training_set <- mammographic_data_complete[s, column_names]
mammo_test_set <- mammographic_data_complete[-s, column_names]

svm_mammo <- svm(Severity ~., mammo_training_set, kernel = "linear", cost = .1, scale = FALSE)
summary(svm_mammo)


svm_mammo_tuned <- tune.svm(Severity~., data=mammo_training_set, kernel="linear", cost=10^(-3:3))
summary(svm_mammo_tuned)

p <- predict(svm_mammo, mammo_test_set[,column_names], tpye="class")
plot(p)

