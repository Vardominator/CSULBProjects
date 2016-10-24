using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] preHeap = { 1,2,3,4,5,6};

            Heap heap1 = new Heap(preHeap);
            heap1.BuildMaxHeap();
            heap1.PrintHeap();

            Heap heap2 = new Heap(preHeap);
            heap2.BuildMaxHeapAlternate(preHeap);
            heap2.PrintHeap();

        }
    }
}
