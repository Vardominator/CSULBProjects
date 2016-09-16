using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UninformedSearch
{
    class Program
    {
        static void Main(string[] args)
        {

            var root = new Node<char>('A');
            var a = new Node<char>('A'); var b = new Node<char>('B'); var c = new Node<char>('C');
            var d = new Node<char>('D'); var e = new Node<char>('E'); var f = new Node<char>('F');
            var g = new Node<char>('G'); var h = new Node<char>('H'); var i = new Node<char>('I');
            var j = new Node<char>('J'); var k = new Node<char>('K'); var l = new Node<char>('L');
            var m = new Node<char>('M'); var n = new Node<char>('N'); var o = new Node<char>('O');
            var p = new Node<char>('P'); var q = new Node<char>('Q'); var r = new Node<char>('R');
            var s = new Node<char>('S'); var t = new Node<char>('T'); var u = new Node<char>('U');

            a.AddSuccessors(new List<Node<char>>() { b, c, d });
            b.AddSuccessors(new List<Node<char>>() { e, f });
            c.AddSuccessors(new List<Node<char>>() { g, h });
            d.AddSuccessors(new List<Node<char>>() { i, j });
            e.AddSuccessors(new List<Node<char>>() { k, l });
            f.AddSuccessors(new List<Node<char>>() { l, m });
            g.AddSuccessors(new List<Node<char>>() { n });
            h.AddSuccessors(new List<Node<char>>() { o, p });
            i.AddSuccessors(new List<Node<char>>() { p, q });
            j.AddSuccessors(new List<Node<char>>() { r });
            k.AddSuccessor(s);
            l.AddSuccessor(t);
            p.AddSuccessor(u);


            Node<char> start = a;
            Node<char> goal = u;

            Tree<char> tree = new Tree<char>(root);


            //tree.BreadthFirstSearch(start, goal);
            //tree.DepthFirstSearch(start, goal);

            Random rand = new Random();

            
            int[] goalState = new int[9];
            for (int z = 0; z < goalState.Length; z++)
            {
                goalState[z] = z;
            }

            
            int[] numbers = new int[9];
            for (int blah = 0; blah < numbers.Length; blah++) { numbers[blah] = goalState[blah]; }

            //Shuffle
            for (int z = 0; z < numbers.Length - 2; z++)
            {
                int x = rand.Next(z, numbers.Length);
                int temp = numbers[z];
                numbers[z] = numbers[x];
                numbers[x] = temp;
            }
            
            int[,] grid = new int[3, 3];
            int[,] visited = new int[3, 3];

            Point startPoint = new Point(0, 0);

            int place = 0;

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    grid[row, col] = numbers[place];

                    if(numbers[place] == 0)
                    {
                        startPoint = new Point(row, col);
                    }
                    place++;
                }
            }

            int[,] InitialGrid = new int[3, 3];
            InitialGrid[0, 0] = 8; InitialGrid[0, 1] = 3; InitialGrid[0, 2] = 2;
            InitialGrid[1, 0] = 1; InitialGrid[1, 1] = 6; InitialGrid[1, 2] = 4;
            InitialGrid[2, 0] = 7; InitialGrid[2, 1] = 0; InitialGrid[2, 2] = 5;

            int[,] eightPuzzleGoal = new int[3, 3];
            eightPuzzleGoal[0, 0] = 1; eightPuzzleGoal[0, 1] = 2; eightPuzzleGoal[0, 2] = 3;
            eightPuzzleGoal[1, 0] = 8; eightPuzzleGoal[1, 1] = 0; eightPuzzleGoal[1, 2] = 4;
            eightPuzzleGoal[2, 0] = 7; eightPuzzleGoal[2, 1] = 6; eightPuzzleGoal[2, 2] = 5;

            Puzzle puzzle = new Puzzle(InitialGrid, eightPuzzleGoal);

            // Check computation times
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            Console.WriteLine("Breadth first search: ");
            puzzle.BreadthFirstSearch();
            stopwatch.Stop();
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms\n\n");

            stopwatch.Start();
            Console.WriteLine("Depth first search: ");
            puzzle.DepthFirstSearch();
            stopwatch.Stop();
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms\n\n");

        }
    }
}
