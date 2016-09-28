using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeNavigation
{
    class Program
    {
        static void Main(string[] args)
        {
            Point tempPoint = new Point(0, 0);

            List<Node> nodes = new List<Node>();

            Node A = new Node(new Point(0,0), 'A');
            Node B = new Node(new Point(1, 5), 'B');
            Node C = new Node(new Point(3, 5), 'C');
            Node D = new Node(new Point(2, 3), 'D');
            Node E = new Node(new Point(0, 1), 'E');
            Node F = new Node(new Point(0, 6), 'F');
            Node G = new Node(new Point(3, 3), 'G');
            Node H = new Node(new Point(5, 6), 'H');
            Node I = new Node(new Point(6, 3), 'I');
            Node J = new Node(new Point(7, 5), 'J');
            Node K = new Node(new Point(8, 3), 'K');
            Node L = new Node(new Point(10, 6), 'L');
            Node M = new Node(new Point(10, 0), 'M');
            Node N = new Node(tempPoint, 'N');

            AddNodesToList(nodes, A, B, C, D, E, F, G, H, I, J, K, L, M);

            A.AddChildren(B, D);
            B.AddChildren(E, F, C);
            D.AddChildren(G, H);
            H.AddChild(I);
            I.AddChildren(J, K);
            K.AddChildren(N, M);

            Maze maze = new Maze(nodes, A);
            
            //maze.BreadthFirstSearch(A, M);

            //Console.WriteLine();

            List<Node> aStarPath = maze.AStarSearch(A, M);
            PrintList(aStarPath);
            Console.WriteLine();

        }


        public static void AddNodesToList(List<Node> list, params Node[] nodes)
        {
            list.AddRange(nodes);
        }

        public static void PrintList(List<Node> list)
        {

            foreach (var item in list)
            {
                Console.Write(item.Value + "  ");

            }
            Console.WriteLine();
        }

    }
}
