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
    public class Vertex
    {
        int size;
        public int xPos;
        public int yPos;

        public List<Vertex> neighbors;

        public Vertex(int size, int xPos, int yPos)
        {
            
            this.size = size;
            this.xPos = xPos;
            this.yPos = yPos;

            neighbors = new List<Vertex>();

        }

        public void AddNeighbor(Vertex vertex)
        {
            neighbors.Add(vertex);
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillEllipse(Brushes.Black, xPos, yPos, size, size);
        }

        public string ToString()
        {
            return $"{xPos},{yPos}";
        }

    }
}
