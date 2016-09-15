using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UninformedSearch
{

    public class PuzzleGraph
    {
        
        public int Dimension { get; set; }

        public int[,] Grid { get; set; }

        public Point Start { get; set; }

        Dictionary<Point, int> GridPoints { get; set; }
        HashSet<Point> VisitedPoints { get; set; }


        public PuzzleGraph(int[,] grid, Point start)
        {

            Grid = grid;
            Dimension = grid.GetLength(0);

            Start = start;

            CreateGridPoints();
            VisitedPoints = new HashSet<Point>();

        }



        public void BreadthFirstSearch(int[] goalState)
        {
            
            Queue<int[,]> gridQueue = new Queue<int[,]>();
            gridQueue.Enqueue(Grid);
    
            VisitedPoints.Add(Start);

            while (!CheckGoalReached(gridQueue.Peek(), goalState))
            {

                int[,] currentGrid = new int[Dimension, Dimension];
                Array.Copy(gridQueue.Dequeue(), currentGrid, Dimension * Dimension);

                Point currentStart = CurrentStart(currentGrid);
                

                foreach (Point neighbor in GetNeighbors(currentStart))
                {

                    if (!VisitedPoints.Contains(neighbor))
                    {
                        // New grid to add to queue
                        int[,] newGrid = new int[Dimension, Dimension];
                        Array.Copy(currentGrid, newGrid, Dimension * Dimension);
                        // x and y for 0 position
                        int zeroX = neighbor.X; int zeroY = neighbor.Y;

                        int temp = newGrid[currentStart.X, currentStart.Y];
                        newGrid[currentStart.X, currentStart.Y] = newGrid[zeroX, zeroY];
                        newGrid[zeroX, zeroY] = temp;
                        VisitedPoints.Add(neighbor);

                        gridQueue.Enqueue(newGrid);
                        
                        for (int i = 0; i < Dimension; i++)
                        {
                            for (int j = 0; j < Dimension; j++)
                            {
                                Console.Write(newGrid[i, j] + " ");
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine();

                    }

                }
                
            }

        }

        public Point CurrentStart(int[,] grid)
        {

            Point zeroPoint = new Point();

            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    if(grid[i,j] == 0)
                    {
                        zeroPoint.X = i;
                        zeroPoint.Y = j;
                    }

                }

            }

            return zeroPoint;
        }

        public List<Point> GetNeighbors(Point current)
        {

            List<Point> neighbors = new List<Point>();

            if(current.X - 1 > -1)
            {
                neighbors.Add(new Point(current.X - 1, current.Y));
            }
            if(current.X + 1 < Dimension)
            {
                neighbors.Add(new Point(current.X + 1, current.Y));
            }
            if(current.Y - 1 > -1)
            {
                neighbors.Add(new Point(current.X, current.Y - 1));
            }
            if(current.Y + 1 < Dimension)
            {
                neighbors.Add(new Point(current.X, current.Y + 1));
            }

            return neighbors;

        }

        public void CreateGridPoints()
        {
            GridPoints = new Dictionary<Point, int>();

            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    Point currentPoint = new Point(i, j);
                    GridPoints.Add(currentPoint, Grid[i, j]);
                }

            }

        }

        public void PrintGrid()
        {
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    Point currentPoint = new Point(i, j);
                    Console.Write(GridPoints[currentPoint] + " ");

                }
                Console.WriteLine();

            }
        }

    
        public bool CheckGoalReached(int[,] grid, int[] goalState)
        {

            int place = 0;

            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {

                    if (grid[i, j] != goalState[place])
                    {
                        return false;
                    }

                    place++;

                }

            }

            return true;

        }

    }


}
