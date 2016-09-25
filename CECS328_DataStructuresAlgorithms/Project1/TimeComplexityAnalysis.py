import pandas as pd
import matplotlib.pyplot as plt


def PlotComplexities(title = "Fibonacci Complexities"):

    fpath = 'F:/GitHub/CSULBProjects/CECS328_DataStructuresAlgorithms/Project1/fibonacciRunningTimes.txt'
    df1 = pd.read_table(fpath, index_col=0)
    df1 = df1['Sum']
    print(df1)
    ax = df1.plot(title = title)
    ax.set_xlabel("Input Size")
    ax.set_ylabel("Running Time (s)")
    
    plt.show()
    

# Starting point
if __name__ == "__main__":
    PlotComplexities()