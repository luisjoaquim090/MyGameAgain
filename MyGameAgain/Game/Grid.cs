
using System.Drawing;

namespace Game
{
    public class Grid
    {
        public Coords MaxCoords { get; }

        private List<Cell> cells;

        private Cell defaultCell;

        public Grid(Coords maxCoords)
        {
            MaxCoords = maxCoords;
            cells = [];
            defaultCell = new Cell('.', Coords._default, ConsoleColor.DarkGreen);
            Generate();
        }
        public void Generate()
        {
            AddCircle('^', ConsoleColor.DarkGray);
            AddCircle('~', ConsoleColor.DarkBlue);
            AddRiver('~', ConsoleColor.Blue);
            AddSquare('#', 5, 5, ConsoleColor.DarkYellow);

            AddCell('ª', ConsoleColor.DarkRed);
            AddCell('ª', ConsoleColor.DarkRed);
            AddCell('ª', ConsoleColor.DarkRed);

            AddCell('T', ConsoleColor.Green);
            AddCell('T', ConsoleColor.Green);
            AddCell('T', ConsoleColor.Green);

            AddCell('+', ConsoleColor.DarkCyan);

            AddCell('*', ConsoleColor.Yellow);
            AddCell('*', ConsoleColor.Yellow);
            AddCell('*', ConsoleColor.Yellow);

            AddCell('H', ConsoleColor.DarkMagenta);

        }

        private void AddRiver(char tile, ConsoleColor color = ConsoleColor.Blue)
        {
            var _ = new Random();
            Coords start = new(_.Next(0, MaxCoords.X), _.Next(0, MaxCoords.Y), _.Next(0, MaxCoords.Z));
            Coords end = new(_.Next(0, MaxCoords.X), _.Next(0, MaxCoords.Y), _.Next(0, MaxCoords.Z));
            int movesX = end.X - start.X;
            int movesY = end.Y - start.Y;
            int steps = Math.Max(Math.Abs(movesX), Math.Abs(movesY));
            for (int i = 0; i <= steps; i++)
            {
                int x = start.X + i * movesX / steps;
                int y = start.Y + i * movesY / steps;
                AddCell(tile, new Coords(x, y, 1), color);
            }

        }

        private void AddCircle(char tile, ConsoleColor color = ConsoleColor.Gray)
        {
            var _ = new Random();
            Coords center = new(_.Next(0, MaxCoords.X), _.Next(0, MaxCoords.Y), _.Next(0, MaxCoords.Z));
            int minRad = Math.Min(MaxCoords.X, MaxCoords.Y);
            int radius = _.Next(1, minRad / _.Next(1, minRad));
            for (int x = center.X - radius; x <= center.X + radius; x++)
            {
                for (int y = center.Y - radius; y <= center.Y + radius; y++)
                {
                    if (x >= 0 && x < MaxCoords.X && y >= 0 && y < MaxCoords.Y)
                    {
                        double distance = Math.Sqrt(Math.Pow(x - center.X, 2 ) + Math.Pow(y - center.Y, 2));
                        if (distance <= radius)
                        {
                            AddCell(tile, new Coords(x, y, 1), color);
                        }
                    }
                }
            }
        }

        private void AddSquare(char tile, int sizeX, int sizeY, ConsoleColor color = ConsoleColor.Gray)
        {
            var _ = new Random();
            Coords topLeft = new(_.Next(0, MaxCoords.X - sizeX), _.Next(0, MaxCoords.Y - sizeY), _.Next(0, MaxCoords.Z));
            for (int x = topLeft.X; x < topLeft.X + sizeX; x++)
            {
                for (int y = topLeft.Y; y < topLeft.Y + sizeY; y++)
                {
                    AddCell(tile, new Coords(x, y, 1), color);
                }
            }
        }

        private Cell AddCell(char tile, Coords coords, ConsoleColor color)
        {

            Cell cell = new(tile, coords, color);
            cells.Add(cell);
            return cell;
        }

        private Cell AddCell(char tile, ConsoleColor color)
        {
            Random rand = new();
            int x = rand.Next(1, MaxCoords.X);
            int y = rand.Next(1, MaxCoords.Y);
            int z = rand.Next(1, MaxCoords.Z);
            if (GetCell(new(x, y, z)) == null)
            {
                return AddCell(tile, new(x, y, z), color);
            }
            else
            {
                return AddCell(tile, color);
            }
        }

        public Cell? GetCell(Coords coords)
        {
            Cell? r = cells.Find(cell => cell.Coords == coords);
            return r;
        }

        public void ToConsole()
        {
            for (int z = 0; z < MaxCoords.Z; z++)
            {
                for (int y = 0; y < MaxCoords.Y; y++)
                {
                    ToConsole(z, y);
                    Console.Write("\n");
                }
                Console.Write("\n");
            }
        }

        public void ToConsole(int z, int y)
        {
            for (int x = 0; x < MaxCoords.X; x++)
            {
                Cell? cell = GetCell(new(x, y, z));
                if (cell != null)
                {
                    Console.ForegroundColor = cell.Color;
                    Console.Write(cell.tile);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = defaultCell.Color;
                    Console.Write(defaultCell.tile);
                    Console.ResetColor();
                }
            }
        }
    }
}
