using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UninformedSearch
{
    public class PuzzleGrid
    {

        public int[,] Grid { get; set; }

        public Point ZeroPoint { get; set; }

        public PuzzleGrid(int[,] grid, Point zeroPoint)
        {
            Grid = grid;
            ZeroPoint = zeroPoint;
        }
        
    }
}
