using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeNavigation
{

    class PriorityQueue<T>
    {

        List<Tuple<T, double>> placeHolder;
        public int Count { get { return placeHolder.Count; } }

        public PriorityQueue()
        {
            placeHolder = new List<Tuple<T, double>>();
        }

        public void Enqueue(T newItem, double newCost)
        {
            placeHolder.Add(Tuple.Create(newItem, newCost));
        }

        public T Dequeue()
        {
            int bestIndex = 0;

            for (int i = 0; i < placeHolder.Count; i++)
            {
                if (placeHolder[i].Item2 < placeHolder[bestIndex].Item2)
                {
                    bestIndex = i;
                }
            }

            T bestItem = placeHolder[bestIndex].Item1;
            placeHolder.RemoveAt(bestIndex);

            return bestItem;
        }
    }
    
}
