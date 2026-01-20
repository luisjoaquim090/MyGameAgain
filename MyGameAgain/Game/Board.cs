
using Game.Interfaces;

namespace Game
{
    public class Board(string? name, int? x, int? y, int? z)
    {
        public string Name { get; } = name ?? "Board";
        public IGrid Default_grid { get; } = new Grid(new Coord(x ?? 1, y ?? 1, z ?? 1));

        public void Generate()
        {
            Default_grid.Generate();
        }

        public void ToConsole()
        {
            //Header
            Console.Write($"{(char)0x250C}{new((char)0x2500,Default_grid.MaxCoords.X)}{(char)0x2510}\n");

            Console.Write($"{(char)0x2502}{new((char)0x2508,Default_grid.MaxCoords.X)}{(char)0x2502}\n");

            Console.Write($"{(char)0x251C}{new((char)0x2500, Default_grid.MaxCoords.X)}{(char)0x2524}\n");


            //Body
            for (int y = 0; y < Default_grid.MaxCoords.Y; y++)
            {
                Console.Write((char)0x2502);
                Default_grid.ToConsole(1, y);
                Console.Write($"{(char)0x2502}\n");
            }

            Console.Write($"{(char)0x2514}{new((char)0x2500, Default_grid.MaxCoords.X)}{(char)0x2518}\n");
        }

    }
}
