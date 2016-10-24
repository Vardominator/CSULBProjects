using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeNavigation
{
    public class Maze
    {
        public List<Node> Vertices { get; private set; }

        public Node Root { get; private set; }

        public Maze(List<Node> vertices, Node root)
        {
            Vertices = vertices;
            Root = root;
        }

        public void BreadthFirstSearch(Node start, Node goal)
        {
            Queue<Node> queue = new Queue<Node>();
            Node current = start;

            queue.Enqueue(current);

            Console.Write("Breadth-first search:  ");

            while(current != goal)
            {

                current = queue.Dequeue();

                Console.Write(current.Value + "  ");

                foreach (Node neighbor in current.Children)
                {
                    queue.Enqueue(neighbor);
                }
                    
            }

            Console.WriteLine();

        }


        public List<Node> AStarSearch(Node start, Node goal)
        {

            Dictionary<Node, Node> parentMap = new Dictionary<Node, Node>();
            PriorityQueue<Node> queue = new PriorityQueue<Node>();

            queue.Enqueue(start, 0);
            Node current;

            Console.Write("A* Search: ");

            while(queue.Count > 0)
            {

                current = queue.Dequeue();

                if (!current.IsVisited)
                {

                    current.IsVisited = true;

                    if (current == goal)
                    {
                        break;
                    }

                    foreach (Node neighbor in current.Children)
                    {

                        parentMap.Add(neighbor, current);
                        double priority = Heuristic(neighbor, goal);
                        queue.Enqueue(neighbor, priority);

                    }

                }

            }

            List<Node> path = ReconstructPath(parentMap, start, goal);

            return path;

        }

        public double Heuristic(Node nodeA, Node nodeB)
        {
            return nodeA.EuclidianDist(nodeB);
        }

        public List<Node> ReconstructPath(Dictionary<Node, Node> parentMap, Node start, Node goal)
        {
            List<Node> path = new List<Node>();
            Node current = goal;

            while(current != start)
            {
                path.Add(current);
                current = parentMap[current];
            }

            path.Add(start);

            path.Reverse();

            return path;

        }

    }
}
