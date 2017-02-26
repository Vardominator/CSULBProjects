neuralnewtork <- function(xor, rate=0.01){
  
  # weights mapping input neurons to hidden layer (w/ bias neuron)
  wInputToHidden = 2 * matrix(runif(2*3),nrow=2,ncol=3) - 1
  wHiddenToOut = 2 * matrix(runif(3),nrow=1,ncol=3) - 1
  
  # weights adjustment based on back propagation (w/ bias neuron)
  delInputToHidden = 2 * matrix(dim(wInputToHidden))
  delHiddenToOut = 2 * matrix(dim(wHiddenToOut))
  
  
  m = 0
  
  # cost function
  J = 0.0
  
  for x in xor:
    X1 = 
  
}