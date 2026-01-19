using Game.Interfaces;

namespace Game
{
    public class Peg : ICell, IPeg
    {
        private char tag;

        public IPeg.PegType Type { get; }

        public int Health { get; }

        public int Attack { get; }
        public int Defense { get; }
        public int AttackDamage { get; }
        public char tile { get; }

        public ICoord Coords { get; private set; }
        public ConsoleColor Color { get; }

        public Peg()
        {
            Type = IPeg.PegType.None;
            Coords = Coord._default;
        }

        public Peg(IPeg.PegType type, ICoord coords, string name)
        {
            Type = type;
            Coords = coords;
        }

        public Peg(IPeg.PegType type, ICoord coords, string name, char tag, ConsoleColor color) : this(type, coords, name)
        {
            this.tag = tag;
            Color = color;
        }

        public void MoveTo(ICoord newCoords)
        {
            Coords = newCoords;
        }
    }
}
