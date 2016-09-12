using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UninformedSearch
{
    class Tree<T>
    {

        public Node<T> Root { get; private set; }

        public List<Node<T>> OpenedNodes { get; set; }

        public List<Node<T>> ClosedNodes { get; set; }

        public Tree(T rootValue)
        {
            Root = new Node<T>(rootValue);
            OpenedNodes = new List<Node<T>>();
            ClosedNodes = new List<Node<T>>();
        }

        public Tree(Node<T> initialRoot)
        {
            Root = initialRoot;
            OpenedNodes = new List<Node<T>>();
            ClosedNodes = new List<Node<T>>();
        }

        public void DepthFirstSearch(Node<T> start, Node<T> goal)
        {
            
            if (start != goal)
            {
                OpenedNodes.Add(start);
                Print(OpenedNodes);
                Console.Write("; ");
                Print(ClosedNodes);
                ClosedNodes.Add(start);
                Console.WriteLine();

                foreach (Node<T> successor in start.Successors)
                {
                    DepthFirstSearch(successor, goal);
                    OpenedNodes.Add(start);
                }

                OpenedNodes.Remove(start);

            }

        }

        public void BreadthFirstSearch(Node<T> start, Node<T> goal)
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();
            Console.WriteLine(start.Value);

            queue.Enqueue(start);

            while(queue.Peek() != goal)
            {
                Node<T> current = queue.Dequeue();

                foreach (var successor in current.Successors)
                {

                    Console.WriteLine(successor.Value);
                    queue.Enqueue(successor);

                }

            }

        }

        public void Print(List<Node<T>> openedNodes)
        {

            Console.Write($"[");

            foreach (var node in openedNodes)
            {
                Console.Write($"{node.Value} ");
            }

            Console.Write($"]");

        }

    }
}
