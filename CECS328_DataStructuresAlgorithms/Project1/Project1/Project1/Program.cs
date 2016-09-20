using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine($"Problem 1 Result: {FibSum(sampleN)}\n\n");
            #endregion

            #region Problem 2: Write a non-recursive program to calculate S(n)
            int first = 0;
            int second = 1;

            int sum = 1;

            Console.WriteLine("Problem 2 Result: \n");
            Console.WriteLine($"Fibonacci Table for n = {sampleN}:");
            Console.WriteLine("f(0) = 0");
            Console.WriteLine("f(1) = 1");
            
            for (int i = 2; i <= sampleN; i++)
            {
                int currentFib = first + second;
                Console.WriteLine($"f({i}) = {currentFib}");
                sum += currentFib;
                first = second;
                second = currentFib;
            }
            Console.WriteLine("------------");
            Console.WriteLine($"Sum: {sum}");

            #endregion

        }

        public static int FibSum(int number)
        {
            int result = 0;

            for (int i = 0; i <= number; i++)
            {
                result += Fibonacci(i);
            }

            return result;
        }
       
        public static int Fibonacci(int number)
        {

            if(number == 0)
            {
                return 0;
            }
            else if(number == 1)
            {
                return 1;
            }

            return Fibonacci(number - 1) + Fibonacci(number - 2);

        }
    }
}
