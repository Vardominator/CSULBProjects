from math import exp, pow
import numpy as np


def w11(x):
    return x * (1 - x**2)**(3/2)/1

def w12(x):
    return (exp(x))/(1/4)

def w21(x, y):
    return 2*x*y

def w22(x, y):
    return (2*x + 6*pow(x,2)*y)/(1/9)

X11 = np.random.uniform(0,1,10000)
X12 = np.random.uniform(-2,2,10000)

X21 = np.random.uniform(0,1,10000)
Y21 = np.random.uniform(0,1,10000)
X22 = np.random.uniform(-1,2,10000)
Y22 = np.random.uniform(1,4,10000)

h11 = [w11(i) for i in X11]
h12 = [w12(i) for i in X12]

h21 = [w21(i,j) for i,j in zip(X21,Y21)]
h22 = [w22(i,j) for i,j in zip(X22,Y22)]

lambda11 = np.mean(h11)
lambda12 = np.mean(h12)

lambda21 = np.mean(h21)
lambda22 = np.mean(h22)

print(lambda11)
print(lambda12)

print(lambda21)
print(lambda22)

# h2_vals = [g2(X[i], Y[i]) for i in range(len(X))]
# lambda_hat2 = np.mean(h2_vals)
# print(lambda_hat2)