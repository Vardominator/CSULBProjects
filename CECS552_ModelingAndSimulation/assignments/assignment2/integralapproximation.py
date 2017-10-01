from math import exp, cos, pi
import numpy as np


def g(x, y):
    return exp(-(x + y))


n = int(input('Enter sample size: '))
s = []

for _ in range(n):
    x_sample = np.random.uniform(0, 4, 1)
    f = (1/4) * x_sample
    s.append(f)

lambda_hat = np.mean([])
print(lambda_hat)