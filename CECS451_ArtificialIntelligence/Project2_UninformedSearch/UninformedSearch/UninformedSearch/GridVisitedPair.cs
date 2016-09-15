using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UninformedSearch
{
    public class GridVisitedPair
    {

        public int[,] Grid { get; set; }

        public Point ZeroPoint { get; set; }

        public Point PreviousPoint { get; set; }

        public GridVisitedPair(int[,] grid, Point zeroPoint)
        {
            Grid = grid;
            ZeroPoint = zeroPoint;
            PreviousPoint = new Point(-1, -1);
        }
        
    }
}
