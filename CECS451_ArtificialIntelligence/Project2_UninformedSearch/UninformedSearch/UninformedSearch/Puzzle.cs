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

        public int[,] Goal;
        public int[,] InitialGrid;
        public Point InitialZeroPoint;

        public Puzzle(int[,] initialGrid, int[,] goal)
        {
            Dimension = initialGrid.GetLength(0);
            InitialGrid = initialGrid;
            Goal = goal;
        }

        public void BreadthFirstSearch()
        {

            PrintGrid(InitialGrid);

            HashSet<string> attemptedFrontiers = new HashSet<string>();
            Queue<PuzzleGrid> stack = new Queue<PuzzleGrid>();
            PuzzleGrid initialPair = new PuzzleGrid(InitialGrid, InitialZeroPoint);

            stack.Enqueue(initialPair);
            attemptedFrontiers.Add(StringRepresentation(InitialGrid));

            int numberOfAttempts = 0;
            string goalString = StringRepresentation(Goal);

            while (stack.Count > 0)
            {

                numberOfAttempts++;

                PuzzleGrid currentPair = stack.Dequeue();
                Point currentPoint = currentPair.ZeroPoint;

                string currentGridString = StringRepresentation(currentPair.Grid);

                if (currentGridString == goalString)
                {
                    PrintGrid(currentPair.Grid);
                    Console.WriteLine($"\nNumber of attemps: {numberOfAttempts}");
                    return;
                }

                foreach (Point neighborPoint in GetNeighbors(currentPair.ZeroPoint))
                {

                    // Copy grid
                    int[,] newGrid = new int[Dimension, Dimension];
                    Array.Copy(currentPair.Grid, newGrid, Dimension * Dimension);

                    // Perform "swapping"
                    int temp = newGrid[currentPoint.X, currentPoint.Y];
                    newGrid[currentPoint.X, currentPoint.Y] = newGrid[neighborPoint.X, neighborPoint.Y];
                    newGrid[neighborPoint.X, neighborPoint.Y] = temp;

                    // Create new grid 
                    PuzzleGrid newPair = new PuzzleGrid(newGrid, neighborPoint);

                    if (!attemptedFrontiers.Contains(StringRepresentation(newGrid)))
                    {
                        stack.Enqueue(newPair);
                        attemptedFrontiers.Add(StringRepresentation(newGrid));
                    }

                }

            }

            Console.WriteLine($"\nMaximum number of attempts reach: {numberOfAttempts}");

        }


        public void DepthFirstSearch()
        {

            PrintGrid(InitialGrid);

            HashSet<string> attemptedFrontiers = new HashSet<string>();
            Stack<PuzzleGrid> stack = new Stack<PuzzleGrid>();
            PuzzleGrid initialPair = new PuzzleGrid(InitialGrid, InitialZeroPoint);

            stack.Push(initialPair);
            attemptedFrontiers.Add(StringRepresentation(InitialGrid));

            int numberOfAttempts = 0;
            string goalString = StringRepresentation(Goal);

            while (stack.Count > 0)
            {

                numberOfAttempts++;

                PuzzleGrid currentPair = stack.Pop();
                Point currentPoint = currentPair.ZeroPoint;

                string currentGridString = StringRepresentation(currentPair.Grid);

                if (currentGridString == goalString)
                {
                    PrintGrid(currentPair.Grid);
                    Console.WriteLine($"\nNumber of attemps: {numberOfAttempts}");
                    return;
                }

                foreach (Point neighborPoint in GetNeighbors(currentPair.ZeroPoint))
                {

                    // Copy grid
                    int[,] newGrid = new int[Dimension, Dimension];
                    Array.Copy(currentPair.Grid, newGrid, Dimension * Dimension);

                    // Perform "swapping"
                    int temp = newGrid[currentPoint.X, currentPoint.Y];
                    newGrid[currentPoint.X, currentPoint.Y] = newGrid[neighborPoint.X, neighborPoint.Y];
                    newGrid[neighborPoint.X, neighborPoint.Y] = temp;

                    // Create new grid 
                    PuzzleGrid newPair = new PuzzleGrid(newGrid, neighborPoint);

                    if (!attemptedFrontiers.Contains(StringRepresentation(newGrid)))
                    {
                        stack.Push(newPair);
                        attemptedFrontiers.Add(StringRepresentation(newGrid));
                    }

                }

            }

            Console.WriteLine($"\nMaximum number of attempts reach: {numberOfAttempts}");

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
