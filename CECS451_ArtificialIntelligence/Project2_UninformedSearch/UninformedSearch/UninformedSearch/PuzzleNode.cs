using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UninformedSearch
{
    public class PuzzleNode
    {

        public int Value { get; set; }

        public List<PuzzleNode> neighbors = new List<PuzzleNode>();
        
    }
}
