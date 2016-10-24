using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeNavigation
{
    public class Node
    {

        public Point Location { get; private set; }
        public char Value { get; private set; }

        public List<Node> Children { get; private set; }

        public bool IsVisited { get; set; }

        public Node(Point location, char value)
        {
            Children = new List<Node>();
            Location = location;
            Value = value;
            IsVisited = false;
        }

        public double EuclidianDist(Node node)
        {
            double xComponent = Math.Pow((Location.X - node.Location.X), 2);
            double yComponent = Math.Pow((Location.Y - node.Location.Y), 2);

            return Math.Sqrt(xComponent + yComponent);
        }

        public void AddChild(Node newChild)
        {
            Children.Add(newChild);
        }

        public void AddChildren(params Node[] newChildren)
        {
            Children.AddRange(newChildren);
        }

    }
}
