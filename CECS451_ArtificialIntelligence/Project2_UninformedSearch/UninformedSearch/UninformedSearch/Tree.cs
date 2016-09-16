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


            OpenedNodes.Add(start);
            Console.Write($"open = {Print(OpenedNodes)}; ");
            Console.WriteLine($"closed = {Print(ClosedNodes)}");

            foreach (Node<T> successor in start.Successors)
            {

                DepthFirstSearch(successor, goal);
            }

            OpenedNodes.RemoveAt(OpenedNodes.Count - 1);

            if (!ClosedNodes.Contains(start))
            {
                ClosedNodes.Insert(0, start);
            }

            Console.Write($"open = {Print(OpenedNodes)}; ");
            Console.WriteLine($"closed = {Print(ClosedNodes)}");


           
                

        }

        public void BreadthFirstSearch(Node<T> start, Node<T> goal)
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();
            queue.Enqueue(start);

            OpenedNodes.Add(start);
            Console.Write($"open = {Print(OpenedNodes)}; ");
            Console.Write($"closed = {Print(ClosedNodes)}\n");

            while(queue.Peek() != goal)
            {

                Node<T> current = queue.Dequeue();
                OpenedNodes.Remove(current);

                if (!ClosedNodes.Contains(current))
                {
                    ClosedNodes.Insert(0, current);
                }

                foreach (var successor in current.Successors)
                {

                    queue.Enqueue(successor);

                    if (!OpenedNodes.Contains(successor))
                    {
                        OpenedNodes.Add(successor);
                    }

                }

                Console.Write($"open = {Print(OpenedNodes)}; ");
                Console.WriteLine($"closed = {Print(ClosedNodes)}");

            }

        }

        public string Print(List<Node<T>> openedNodes)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            foreach (var node in openedNodes)
            {
                sb.Append($"{node.Value},");
            }

            if (sb.Length > 1)
            {
                sb.Remove(sb.Length - 1, 1);
            }
 
            sb.Append("]");

            return sb.ToString();

        }

    }
}
