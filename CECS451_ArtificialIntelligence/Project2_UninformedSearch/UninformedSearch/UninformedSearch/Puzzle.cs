using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UninformedSearch
{
    public class Puzzle
    {

        public int Dimension { get; set; }

        public int[] Goal;
        public int[,] InitialGrid;
        public bool[,] InitialVisited;
        public Point InitialZeroPoint;

        

        public Puzzle(int dimension)
        {
            Dimension = dimension;
            Goal = new int[dimension * dimension];
            InitialGrid = new int[dimension, dimension];
            InitialVisited = new bool[dimension, dimension];
            GenerateGoal();
            
        }

        public void GenerateGoal()
        {
            for (int z = 0; z < Goal.Length; z++)
            {
                Goal[z] = z;
            }
        }

        public void GenerateRandomPuzzle()
        {

            int[] numbers = new int[Dimension * Dimension];
            for (int blah = 0; blah < numbers.Length; blah++)
            {
                numbers[blah] = Goal[blah];
            }

            //Shuffle
            Random rand = new Random();
            for (int z = 0; z < numbers.Length - 2; z++)
            {
                int x = rand.Next(z, numbers.Length);
                int temp = numbers[z];
                numbers[z] = numbers[x];
                numbers[x] = temp;
            }

            int place = 0;

            for (int row = 0; row < InitialGrid.GetLength(0); row++)
            {
                for (int col = 0; col < InitialGrid.GetLength(1); col++)
                {
                    InitialGrid[row, col] = numbers[place];
                    place++;

                    // Set initial zero point (the point that moves around the grid)
                    if (InitialGrid[row, col] == 0)
                    {
                        InitialZeroPoint = new Point(row, col);
                    }
                }
            }

        }
        

        public void BreadthFirstSearch()
        {

            PrintGrid(InitialGrid);

            if (!Solvable(InitialGrid))
            {
                Console.WriteLine("This puzzle is not solvable. Try again.");
                return;
            }

            HashSet<string> attemptedFrontiers = new HashSet<string>();

            Queue<GridVisitedPair> queue = new Queue<GridVisitedPair>();

            GridVisitedPair initialPair = new GridVisitedPair(InitialGrid, InitialZeroPoint);
            queue.Enqueue(initialPair);

            attemptedFrontiers.Add(StringRepresentation(InitialGrid));
            
            while(queue.Count > 0)
            {

                GridVisitedPair currentPair = queue.Dequeue();
                Point currentPoint = currentPair.ZeroPoint;
                
                if(CheckGoalReached(currentPair.Grid, Goal))
                {
                    PrintGrid(currentPair.Grid);
                    return;
                }

                foreach (Point neighborPoint in GetNeighbors(currentPair.ZeroPoint))
                {
                    
                    // Copy grid
                    int[,] newGrid = new int[Dimension, Dimension];
                    Array.Copy(currentPair.Grid, newGrid, Dimension * Dimension);

                    int temp = newGrid[currentPoint.X, currentPoint.Y];
                    newGrid[currentPoint.X, currentPoint.Y] = newGrid[neighborPoint.X, neighborPoint.Y];
                    newGrid[neighborPoint.X, neighborPoint.Y] = temp;
                    GridVisitedPair newPair = new GridVisitedPair(newGrid, neighborPoint);
                    newPair.PreviousPoint = currentPoint;
                    
                    if (!attemptedFrontiers.Contains(StringRepresentation(newGrid)))
                    {
                        queue.Enqueue(newPair);
                        attemptedFrontiers.Add(StringRepresentation(newGrid));
                    }
                    
                }

            }

           

        }


        public string StringRepresentation(int[,] grid)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {

                    sb.Append(grid[i, j]);

                }

            }

            return sb.ToString();
        }

        // Checking for solvability by counting the number of inversions
        // A pair of numbers in the puzzle are inverted if the their values are in
        //  reverse order of their goal state
        public bool Solvable(int[,] grid)
        {
            int inverseCount = 0;

            int[] buffer = new int[Dimension * Dimension];

            System.Buffer.BlockCopy(grid, 0, buffer, 0, sizeof(int)*grid.Length);

            for (int i = 0; i < Dimension * Dimension; i++)
            {
                for (int j = i + 1; j < Dimension * Dimension; j++)
                {
                    if(buffer[j] > buffer[i]) { inverseCount++; }
                }

            }

            return inverseCount % 2 == 0;

        }

        public bool CheckEquals(int[,] grid, int[,] initialGrid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if(grid[i,j] != initialGrid[i, j]) { return false; }

                }

            }
            return true;
        }

        public List<Point> GetNeighbors(Point current)
        {

            List<Point> neighbors = new List<Point>();

            if (current.X - 1 > -1)
            {
                neighbors.Add(new Point(current.X - 1, current.Y));
            }
            if (current.X + 1 < Dimension)
            {
                neighbors.Add(new Point(current.X + 1, current.Y));
            }
            if (current.Y - 1 > -1)
            {
                neighbors.Add(new Point(current.X, current.Y - 1));
            }
            if (current.Y + 1 < Dimension)
            {
                neighbors.Add(new Point(current.X, current.Y + 1));
            }

            return neighbors;

        }

        public void PrintGrid(int[,] grid)
        {
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    Console.Write(grid[i, j] + "\t");
                }
                Console.WriteLine();

            }

            Console.WriteLine();
        }


        public bool CheckGoalReached(int[,] grid, int[] goal)
        {

            int place = 0;

            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {

                    if (grid[i, j] != goal[place])
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
