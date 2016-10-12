using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// CECS 328 Programming Assignment 1
    /// 
    /// Author: Varderes Barsegyan
    /// Due date: 9/29/2016
    /// </summary>

    class Program
    {
        static void Main(string[] args)
        {

            #region Problem 1: Calculate S(n) by calculating the values of the Fibonacci sequence recursively
            // S(n) definition: S(n) = f(0) + f(1) +...+ f(n)

            int sampleN = 20;
            Console.WriteLine($"Problem 1 Result: {Sn(sampleN)}\n\n");
            #endregion

            #region Problem 2: Write a non-recursive program to calculate S(n)
            Console.WriteLine("Problem 2 Result: \n");
            Console.WriteLine($"Fibonacci Table for n = {sampleN}:");
            Console.WriteLine("f(0) = 0");
            Console.WriteLine("f(1) = 1");
            
            for (int i = 2; i <= sampleN; i++)
            {
                Console.WriteLine($"f({i}) = {Fibonacci(i)}");
            }
            Console.WriteLine("------------");
            Console.WriteLine($"Sum: {NonRecursiveSn(sampleN)}\n\n");
            #endregion

            #region Problem 4: Third possible way to sum Fibonnaci
            Console.WriteLine($"Problem 4: {GrimaldiSum(20)}\n\n");
            #endregion

            #region Problem 5: Calculate S for 10, 20, 30; Calculate f for 12, 22, 32
            Console.WriteLine($"Problem 5 results: \n");
            Console.WriteLine($"S(n) for n = 10 => {GrimaldiSum(10)}");
            Console.WriteLine($"S(n) for n = 20 => {GrimaldiSum(20)}");
            Console.WriteLine($"S(n) for n = 30 => {GrimaldiSum(30)}\n");
            Console.WriteLine($"f(n) for n = 12 => {Fibonacci(12)}");
            Console.WriteLine($"f(n) for n = 22 => {Fibonacci(22)}");
            Console.WriteLine($"f(n) for n = 32 => {Fibonacci(32)}\n");
            #endregion

            #region Problem 7: Fourth way to calcualte S(n)
            Console.WriteLine($"Problem 7: {SnAlt2(20)}\n\n");
            #endregion

            #region Problem 8: Largest n that can be computed successfully by each program

            StreamWriter streamWriter = new StreamWriter(@"F:\GitHub\CSULBProjects\CECS328_DataStructuresAlgorithms\Project1\fibonacciRunningTimes.csv");
            Stopwatch stopWatch = new Stopwatch();
            StringBuilder currentLine = new StringBuilder();

            for (int n = 1; n <= 40; n++)
            {

                stopWatch.Start();
                long value1 = Sn(n);
                currentLine.Append($"{stopWatch.ElapsedTicks},");
                stopWatch.Reset();

                stopWatch.Start();
                long value2 = NonRecursiveSn(n);
                currentLine.Append($"{stopWatch.ElapsedTicks},");
                stopWatch.Reset();

                stopWatch.Start();
                long value3 = GrimaldiSum(n);
                currentLine.Append($"{stopWatch.ElapsedTicks},");
                stopWatch.Reset();

                stopWatch.Start();
                long value4 = SnAlt2(n);
                currentLine.Append($"{stopWatch.ElapsedTicks}");
                stopWatch.Reset();

                streamWriter.WriteLine(currentLine.ToString());
                Console.WriteLine($"Running...{n}");
                currentLine.Clear();

            }

            streamWriter.Close();

            #endregion


        }
         
        public static long SnAlt2(long number)
        {
            return Fibonacci(number + 2) - 1;
        }

        public static long GrimaldiSum(long number)
        {
            long result = 0;

            for (int i = 1; i <= number; i++)
            {
                result += Grimaldi(i);
            }

            return result;

        }
        public static long Grimaldi(long k)
        {
            return (long)((1 / Math.Sqrt(5)) * (Math.Pow(((1 + Math.Sqrt(5)) / 2), k) - Math.Pow(((1 - Math.Sqrt(5)) / 2), k)));
        }

        public static long NonRecursiveSn(long number)
        {

            long previous = 1;
            long previous2 = 1;
            long current = 0;
            long sum = 2;

            for (int i = 3; i <= number; i++)
            {
                current = previous + previous2;
                sum += current;
                previous = previous2;
                previous2 = current;
            }

            return sum;

        }

        public static long Sn(long number)
        {
            long result = 0;

            for (int i = 1; i <= number; i++)
            {
                result += Fibonacci(i);
            }

            return result;
        }
       
        public static long Fibonacci(long number)
        {

            if(number <= 2)
            {
                if(number == 0)
                {
                    return 0;
                }
                return 1;
            }

            return Fibonacci(number - 1) + Fibonacci(number - 2);

        }
    }
}
