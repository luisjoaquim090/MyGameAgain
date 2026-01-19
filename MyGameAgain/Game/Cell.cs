using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Cell
    {
        public char tile { get; }
        public Coords Coords { get; }
        public ConsoleColor Color { get; }
        public Cell()
        {
            this.tile = '.';
            this.Coords = new Coords();
            Color = ConsoleColor.White;
        }

        public Cell(char tile, Coords coords, ConsoleColor color)
        { 
            this.tile = tile;
            this.Coords = coords;
            this.Color = color;
        }
    }
}
