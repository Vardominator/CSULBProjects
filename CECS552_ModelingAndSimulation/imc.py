import math
import numpy as np


# def g(x):
#    return math.cos(x) * math.exp(math.sin(x))

def g(x):
    return x * (1 - x**2)**(3/2)

def g2(x, y):
    return 2 * x * y

X = np.random.uniform(0,1,100000)
Y = np.random.uniform(0,1,100000)

h_vals = [g(i) for i in X]

# lambda_hat = sum(h_vals)/len(h_vals)
lambda_hat = np.mean(h_vals)

# print(h_vals)
# print(lambda_hat)


h2_vals = [g2(X[i], Y[i]) for i in range(len(X))]
lambda_hat2 = np.mean(h2_vals)
print(lambda_hat2)
