using System;
using System.Collections.Generic;
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


            int[] eightPuzzleGoal = new int[9];
            eightPuzzleGoal[0] = 1; eightPuzzleGoal[1] = 2; eightPuzzleGoal[2] = 3;
            eightPuzzleGoal[3] = 8; eightPuzzleGoal[4] = 0; eightPuzzleGoal[5] = 4;
            eightPuzzleGoal[6] = 7; eightPuzzleGoal[7] = 6; eightPuzzleGoal[8] = 5;

            Puzzle puzzle = new Puzzle(3);
            puzzle.GenerateRandomPuzzle();
            
            puzzle.Goal = eightPuzzleGoal;
            puzzle.BreadthFirstSearch();

        }
    }
}
