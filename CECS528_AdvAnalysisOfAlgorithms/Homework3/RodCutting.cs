// Author: Varderes Barsegyan (016163470)
// 
// CECS 551 Homework 3
//
// Description: Implementations of the Recursive Top-Down Rod-Cut and
//              the Bottom-Up Dynamic Rod-Cut algorithms.
//
// 
// Cuts that lead to the optimal revenue: 1 1 3
//
//
// Comparing running times:
//
//      Recursive: T(n) = 1 if n = 0
//                 T(n) = 1 + sum from j = 0 to j = n - 1 of T(j) if n >= 1
//                 T(n) = O(2^n)
//
//      Dynamic:   T(n) = O(n^2)

class Program
{
    static void Main(string[] args)
    {
        // Prices of cut size
        int[] prices = { 0, 3, 5, 10, 12, 14 };

        // Length of the rod
        int rodLength = prices.Length;

        // Recursive rod-cut results
        Console.WriteLine(RodCutRecursive(prices, rodLength));

        // Dynamic rod-cut results
        Console.WriteLine(RodCutDynamic(prices, rodLength));

        // Extended dynamic algorithm that records cut sequence
        PrintOptimalRodCut(prices, rodLength);

    }

    static int RodCutRecursive(int[] prices, int length)
    {
        // Base case is rod of length 0
        if (length == 0)
        {
            return 0;
        }

        int maxValue = int.MinValue;

        // Recursively call for each remaning rod
        for (int i = 0; i < length; i++)
        {
            maxValue = Math.Max(maxValue, prices[i] + RodCutRecursive(prices, length - i - 1));
        }

        return maxValue;

    }

    static int RodCutDynamic(int[] prices, int length)
    {
        // Optimal value for rods
        int[] results = new int[length + 1];
        results[0] = 0;
            
        for(int i = 1; i <= length; i++)
        {
            int maxValue = int.MinValue;

            // Maximum cut position up to i
            for(int j = 0; j < i; j++)
            {
                maxValue = Math.Max(maxValue, prices[j] + results[i - j - 1]);
            }
            results[i] = maxValue;
        }

        return results[length];

    }

    static Tuple<int[],int[]> ExtendedRodCutDynamic(int[] prices, int length)
    {
        // Optimal value for rods
        int[] results = new int[length];
        results[0] = 0;

        // Optimal cut for rods
        int[] optimalCuts = new int[length];

        for (int j = 1; j < length; j++)
        {
            int maxValue = int.MinValue;

            // Maximum cut position up to j
            for (int i = 1; i <= j; i++)
            {
                if(maxValue < (prices[i] + results[j - i]))
                {
                    maxValue = prices[i] + results[j - i];

                    // Cache the value of the best on the currect tree level
                    optimalCuts[j] = i;
                }
            }
            results[j] = maxValue;
        }

        return Tuple.Create(results, optimalCuts);

    }

    static void PrintOptimalRodCut(int[] prices, int length)
    {
        // Compute optimal rod-cut with optimal values
        var rodCutDynamic = ExtendedRodCutDynamic(prices, length);

        int[] results = rodCutDynamic.Item1;
        int[] optimalCuts = rodCutDynamic.Item2;

        // Cut rod according to cuts and print cut length
        while(length > 1)
        {
            Console.Write(optimalCuts[length - 1] + " ");
            length = length - optimalCuts[length - 1];
        }
        Console.WriteLine();
    }

}