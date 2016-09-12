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

            Node<char> root = new Node<char>('A');
            root.AddSuccessors(new List<char>() { 'B', 'C', 'D' });
            root.Successors[0].AddSuccessors(new List<char>() { 'E', 'F' });

            Node<char> start = root;
            Node<char> goal = root.Successors[0].Successors[1];

            Tree<char> tree = new Tree<char>(root);

            //tree.DepthFirstSearch(start, goal);
            tree.BreadthFirstSearch(start, goal);

        }
    }
}
