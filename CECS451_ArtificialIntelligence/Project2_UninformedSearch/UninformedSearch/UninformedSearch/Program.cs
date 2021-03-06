﻿using System;
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



            int[,] InitialGrid = new int[3, 3];
            InitialGrid[0, 0] = 1; InitialGrid[0, 1] = 2; InitialGrid[0, 2] = 4;
            InitialGrid[1, 0] = 3; InitialGrid[1, 1] = 6; InitialGrid[1, 2] = 0;
            InitialGrid[2, 0] = 8; InitialGrid[2, 1] = 5; InitialGrid[2, 2] = 7;

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
