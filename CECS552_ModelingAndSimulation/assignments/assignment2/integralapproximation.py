from math import exp, cos, pi
import numpy as np
import scipy.stats as st


def g(x, y):
    return exp(cos(x + y))

def w(x, y):
    return (2/pi) * (1/x)

def h(x, y):
    return g(x, y) / w(x, y)
    

n = int(input('Enter sample size: '))
f = []

for _ in range(n):
    x_sample = np.random.uniform(0, pi/2, 1)
    y_sample = np.random.uniform(0, x_sample, 1)
    f.append(h(x_sample, y_sample))


lambda_hat = np.mean(f)
variance = np.var(f)
std_err = np.std(f)
conf_int = st.t.interval(0.95, len(f) - 1, loc=lambda_hat, scale=st.sem(f))

print('Integral approximation: {}'.format(lambda_hat))
print('Sample variance: {}'.format(variance))
print('Standard error: {}'.format(std_err))
print('95% Confidence interval: {}'.format([x[0] for x in conf_int]))