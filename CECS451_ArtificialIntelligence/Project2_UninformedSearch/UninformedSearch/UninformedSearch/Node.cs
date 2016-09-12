using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UninformedSearch
{
    class Node<T>
    {

        public T Value { get; private set; }

        public List<Node<T>> Successors { get; set; }

        public Node(T value)
        {
            Value = value;
            Successors = new List<Node<T>>();
        }

        public void AddSuccessor(T newSuccessor)
        {
            Node<T> newNode = new Node<T>(newSuccessor);
            Successors.Add(newNode);
        }
        
        public void AddSuccessor(Node<T> newSuccessor)
        {
            Successors.Add(newSuccessor);
        }

        public void AddSuccessors(List<T> newSuccessors)
        {
            foreach (var successor in newSuccessors)
            {
                Successors.Add(new Node<T>(successor));
            }
        }

        public void AddSuccessors(List<Node<T>> newSuccessors)
        {
            Successors.AddRange(newSuccessors);
        }

    }
}
