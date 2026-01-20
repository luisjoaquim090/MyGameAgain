using Game.Interfaces;

namespace Game
{
    public class Peg : ICell, IPeg
    {
        public IPeg.PegType Type { get; }

        public int Health { get; }

        public int Attack { get; }
        public int Defense { get; }
        public int AttackDamage { get; }
        public char tile { get; }

        public bool CanMove { get; }

        public ICoord Coords { get; private set; }
        public ConsoleColor Color { get; }

        public Peg()
        {
            Type = IPeg.PegType.None;
            Coords = Coord._default;
        }

        public Peg(IPeg.PegType type, ICoord coords)
        {
            Type = type;
            Coords = coords;
        }

        public Peg(IPeg.PegType type, ICoord coords, char tile, ConsoleColor color) : this(type, coords)
        {
            this.tile = tile;
            Color = color;
        }

        public void MoveTo(ICoord newCoords)
        {
            if(CanMove)
                Coords = newCoords;
        }
    }
}
