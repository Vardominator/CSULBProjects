using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeGenerator
{
    public partial class Form1 : Form
    {
        Graphics graphics;

        Vertex[,] grid;

        Dictionary<string, bool> drawLines;
        Color[,] lineColors;

        int dimension = 10;
        int size = 5;

        Bitmap bitmap;


        // Maze generation
        Random rand = new Random();
        HashSet<string> visited = new HashSet<string>();


        public Form1()
        {
            

            InitializeComponent();

            generateMazeButton.BringToFront();

            bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);


            graphics = Graphics.FromImage(bitmap);

            grid = new Vertex[dimension, dimension];
            drawLines = new Dictionary<string, bool>();
            lineColors = new Color[dimension, dimension];

            // generate dots
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    int currentX = i * (ClientSize.Width) / dimension + 30;
                    int currentY = j * (ClientSize.Height) / dimension + 30;

                    grid[i, j] = new Vertex(size, currentX, currentY);
                    drawLines.Add(grid[i, j].ToString(), true);
                    lineColors[i, j] = Color.Black;
                }
            }


            // Set up neighbors
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    if (i < dimension - 1)
                    {
                        AddPair(grid[i, j], grid[i + 1, j]);
                    }
                    if (j < dimension - 1)
                    {
                        AddPair(grid[i, j], grid[i, j + 1]);
                    }
                }

            }
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {



            pictureBox1.Image = bitmap;

            // Draw vertices
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    grid[i, j].Draw(graphics);
                }
            }

            int shift = -30;

            // Draw edges
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    foreach (Vertex neighbor in grid[i, j].neighbors)
                    {
                        if (drawLines[grid[i, j].ToString()] == true)
                        {
                            Point point1 = new Point(grid[i,j].xPos + 2 + shift, grid[i,j].yPos + 2 + shift);
                            Point point2 = new Point(neighbor.xPos + 2 + shift, neighbor.yPos + 2 + shift);
                            graphics.DrawLine(new Pen(lineColors[i,j]), point1, point2);
                        }
                    }
                }

            }

            
            
        }




        private void generateMazeButton_Click(object sender, EventArgs e)
        {

            int randRow = rand.Next(dimension);
            int randCol = rand.Next(dimension);

            Vertex current = grid[randRow, randCol];

            visited.Add(current.ToString());

            while (visited.Count < dimension * dimension)
            {

                drawLines[current.ToString()] = false;

                List<Vertex> neighbors = current.neighbors;
                int randIndex = rand.Next(neighbors.Count);

                Vertex randNeighbor = neighbors[randIndex];

                while (visited.Contains(randNeighbor.ToString()))
                {
                    randIndex = rand.Next(neighbors.Count);
                    randNeighbor = neighbors[randIndex];
                }

                visited.Add(randNeighbor.ToString());

                current = randNeighbor;

            }

            graphics.Clear(Color.White);

        }

        public void AddPair(Vertex vertexA, Vertex vertexB)
        {
            vertexA.AddNeighbor(vertexB);
            vertexB.AddNeighbor(vertexA);
        }



    }
}
