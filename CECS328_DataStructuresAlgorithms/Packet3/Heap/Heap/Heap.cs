using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
    public class Heap
    {
        
        public int[] array { get; private set; }
        public int Heapsize { get; private set; }

        public Heap(int[] array)
        {
            this.array = array;
            Heapsize = 0;
        }


        #region Priority Queue

        public int HeapMaximum()
        {
            return array[0];
        }

        public int HeapExtractMax()
        {
            if(Heapsize < 1)
            {
                throw new ArgumentOutOfRangeException("Heap underflow");
            }
            int max = array[0];
            array[0] = array[Heapsize - 1];

            Heapsize--;

            MaxHeapify(0);

            return max;
        }

        public void HeapIncreaseKey(int i, int key)
        {
            if(key < array[i])
            {
                throw new ArgumentOutOfRangeException("New key is smaller than the current key");
            }

            array[i] = key;

            while(i > 0 && array[Parent(i)] < array[i])
            {
                swap(i, Parent(i));
                i = Parent(i);
            }
        }

        public void MaxHeapInsert(int key)
        {
            Heapsize++;
            int[] newArray = new int[Heapsize];
            Array.Copy(array, newArray, Heapsize - 1);
            array = newArray;
            array[Heapsize - 1] = int.MinValue;
            HeapIncreaseKey(Heapsize - 1, key);
        }

        // Problem 6 - 1: Alternate heap building
        public void BuildMaxHeapAlternate(int[] newArray)
        {
            Heapsize = 1;
            for(int i = 1; i < newArray.Length; i++)
            {
                MaxHeapInsert(newArray[i]);
            }
        }

        #endregion


        public void MaxHeapify(int i)
        {

            int leftIndex = Left(i);
            int rightIndex = Right(i);
            int largest = i;

            if(leftIndex < Heapsize && array[leftIndex] > array[i])
            {
                largest = leftIndex;
            }
            else
            {
                largest = i;
            }

            if(rightIndex < Heapsize && array[rightIndex] > array[largest])
            {
                largest = rightIndex;
            }

            if(largest != i)
            {
                swap(i, largest);
                MaxHeapify(largest);
            }

        }

        public void MinHeapify(int i)
        {
            int leftIndex = Left(i);
            int rightIndex = Right(i);
            int smallest = i;

            if (leftIndex < Heapsize && array[leftIndex] < array[i])
            {
                smallest = leftIndex;
            }
            else
            {
                smallest = i;
            }

            if (rightIndex < Heapsize && array[rightIndex] < array[smallest])
            {
                smallest = rightIndex;
            }

            if (smallest != i)
            {
                swap(i, smallest);
                MaxHeapify(smallest);
            }
        }

        public void BuildMaxHeap()
        {
            Heapsize = array.Length;

            for (int i = array.Length/2; i >= 0; i--)
            {
                MaxHeapify(i);
            }
        }

        public void Heapsort()
        {
            BuildMaxHeap();
            for (int i = array.Length - 1; i >= 1; i--)
            {
                swap(0, i);
                Heapsize -= 1;
                MaxHeapify(0);
            }
        }

        private int Parent(int i)
        {
            return i / 2;
        }
        private int Left(int i)
        {
            return 2 * i;
        }
        private int Right(int i)
        {
            return 2 * i + 1;
        }

        private void swap(int a, int b)
        {
            int temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }

        public void PrintHeap()
        {
            foreach (int num in array)
            {
                Console.Write(num + "  ");
            }
            Console.WriteLine();
        }

    }
}
