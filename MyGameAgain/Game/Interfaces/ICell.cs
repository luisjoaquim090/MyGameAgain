
namespace Game.Interfaces
{
    public interface ICell
    {
        public char tile { get; }
        public ICoord Coords { get; }
        public ConsoleColor Color { get; }
    }
}
