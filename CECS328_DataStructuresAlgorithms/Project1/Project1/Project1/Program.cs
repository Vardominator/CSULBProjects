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
            Console.WriteLine($"Problem 1 Result: {Sn(sampleN)}\n\n");
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
            Console.WriteLine($"Sum: {sum}\n\n");
            #endregion

            #region Problem 4: Third possible way to sum Fibonnaci
            Console.WriteLine($"Problem 4: {SnAlt(20)}\n\n");
            #endregion

            #region Problem 5: Calculate S for n = 10, 20, 30; Calculate f for n = 12, 22, 32

            Console.WriteLine($"Problem 5 results: \n");
            Console.WriteLine($"S(n) for n = 10 => {SnAlt(10)}");
            Console.WriteLine($"S(n) for n = 20 => {SnAlt(20)}");
            Console.WriteLine($"S(n) for n = 30 => {SnAlt(30)}\n");
            Console.WriteLine($"f(n) for n = 12 => {Fibonacci(12)}");
            Console.WriteLine($"f(n) for n = 22 => {Fibonacci(22)}");
            Console.WriteLine($"f(n) for n = 32 => {Fibonacci(32)}\n");

            #endregion

            #region Problem 7: Fourth way to calcualte S(n)

            Console.WriteLine($"Problem 7: {SnAlt2(20)}\n\n");

            #endregion

            #region Problem 8: Largest n that can be computed successfully by each program
            // Considering that we are using 32-bit integers, there is a limitation on how large n can be
            //  I will throw an exception to indicate when the resulting integer overflows

            int n = 1;

            while (true)
            {
                int result = 0;
                try
                {

                }
                catch (OverflowException exc)
                {
                    Console.WriteLine($"Maximum n reached
                }
            }

            #endregion

        }

        public static int SnAlt2(int number)
        {
            return Fibonacci(number + 2) - 1;
        }

        public static int SnAlt(int number)
        {
            int result = 0;

            for (int i = 0; i <= number; i++)
            {
                result += Gn(i);
            }

            return result;

        }
        public static int Gn(int k)
        {
            return (int)((1 / Math.Sqrt(5)) * (Math.Pow(((1 + Math.Sqrt(5)) / 2), k) - Math.Pow(((1 - Math.Sqrt(5)) / 2), k)));
        }

        public static int Sn(int number)
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
