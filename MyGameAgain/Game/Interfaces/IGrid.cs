
namespace Game.Interfaces
{
    public interface IGrid
    {
        ICoord MaxCoords { get; }

        void AddPlayer(string Name, char tag, ConsoleColor color);
        void Generate();

        ICell? GetCell(ICoord coords);

        void ToConsole();
        void ToConsole(int z, int y);

    }
}
