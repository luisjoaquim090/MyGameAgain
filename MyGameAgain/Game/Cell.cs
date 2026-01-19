using Game.Interfaces;

namespace Game
{
    public class Cell:ICell
    {
        public char tile { get; }
        public ICoord Coords { get; }
        public ConsoleColor Color { get; }
        public Cell()
        {
            this.tile = '.';
            this.Coords = Coord._default;
            Color = ConsoleColor.White;
        }

        public Cell(char tile, ICoord coords, ConsoleColor color)
        { 
            this.tile = tile;
            this.Coords = coords;
            this.Color = color;
        }
    }
}
