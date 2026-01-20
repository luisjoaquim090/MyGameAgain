
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
            Console.Write($"/{RepeatStr(Default_grid.MaxCoords.X, "-")}\\\n");

            Console.Write($"|{RepeatStr(Default_grid.MaxCoords.X, ".")}|\n");

            Console.Write($"|{RepeatStr(Default_grid.MaxCoords.X, "-")}|\n");


            //Body
            for (int y = 0; y < Default_grid.MaxCoords.Y; y++)
            {
                Console.Write("|");
                Default_grid.ToConsole(1, y);
                Console.Write("|\n");
            }

            Console.Write($"\\{RepeatStr(Default_grid.MaxCoords.X, "-")}/\n");
        }

        private string RepeatStr(int len, string s)
        {
            string line = "";
            for (int i = 0; i < len; i++)
            {
                line += s;
            }
            return line;
        }
    }
}
