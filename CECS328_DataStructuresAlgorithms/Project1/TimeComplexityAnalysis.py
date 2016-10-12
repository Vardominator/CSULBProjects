import pandas as pd
import matplotlib.pyplot as plt


def PlotComplexities(title = "Fibonacci Complexities"):

    fpath = 'F:/GitHub/CSULBProjects/CECS328_DataStructuresAlgorithms/Project1/fibonacciRunningTimes.csv'
    df1 = pd.read_csv(fpath)
    df1.columns = ['Sum', 'SumNoRec', 'Grim', 'SumAlt']
    print(df1)
    ax = df1.plot(title = title)
    ax.set_xlabel("Input Size")
    ax.set_ylabel("Running Time (s)")

    plt.show()
    

# Starting point
if __name__ == "__main__":
    PlotComplexities()