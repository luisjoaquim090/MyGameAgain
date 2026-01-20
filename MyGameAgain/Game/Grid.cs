
using Game.Interfaces;

namespace Game
{
    public class Grid:IGrid
    {
        public ICoord MaxCoords { get; }

        private List<ICell> cells;

        private ICell defaultCell;

        public Grid(ICoord maxCoords)
        {
            MaxCoords = maxCoords;
            cells = [];
            defaultCell = new Cell('.', Coord._default, ConsoleColor.DarkGreen);
        }

        public void Generate()
        {
            cells = [];
            AddCircle('^', ConsoleColor.DarkGray);
            AddCircle('^', ConsoleColor.DarkGray);
            AddCircle('^', ConsoleColor.DarkGray);
            AddCircle('~', ConsoleColor.DarkBlue);
            AddCircle('T', ConsoleColor.DarkGreen);
            AddCircle('T', ConsoleColor.DarkGreen);
            AddCircle('T', ConsoleColor.DarkGreen);
            AddSquare('#', 5, 5, ConsoleColor.DarkYellow);
            AddSquare('#', 5, 15, ConsoleColor.DarkYellow);

            AddCell('ª', ConsoleColor.DarkRed);
            AddCell('ª', ConsoleColor.DarkRed);
            AddCell('ª', ConsoleColor.DarkRed);

            AddCell('T', ConsoleColor.Green);
            AddCell('T', ConsoleColor.Green);
            AddCell('T', ConsoleColor.Green);

            AddCell('H', ConsoleColor.DarkMagenta);

            AddPlayer("Hero", '+', ConsoleColor.Yellow);

        }

        private void AddRiver(char tile, ConsoleColor color = ConsoleColor.Blue)
        {
            var _ = new Random();
            ICoord start = new Coord(_.Next(0, MaxCoords.X), _.Next(0, MaxCoords.Y), _.Next(0, MaxCoords.Z));
            ICoord end = new Coord(_.Next(0, MaxCoords.X), _.Next(0, MaxCoords.Y), _.Next(0, MaxCoords.Z));
            int movesX = end.X - start.X;
            int movesY = end.Y - start.Y;
            int steps = Math.Max(Math.Abs(movesX), Math.Abs(movesY));
            for (int i = 0; i <= steps; i++)
            {
                int x = start.X + i * movesX / steps;
                int y = start.Y + i * movesY / steps;
                AddCell(tile, new Coord(x, y, 1), color);
            }

        }

        private void AddCircle(char tile, ConsoleColor color = ConsoleColor.Gray)
        {
            var _ = new Random();
            ICoord center = new Coord(_.Next(0, MaxCoords.X), _.Next(0, MaxCoords.Y), _.Next(0, MaxCoords.Z));
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
                            AddCell(tile, new Coord(x, y, 1), color);
                        }
                    }
                }
            }
        }

        private void AddSquare(char tile, int sizeX, int sizeY, ConsoleColor color = ConsoleColor.Gray)
        {
            var _ = new Random();
            ICoord topLeft = new Coord(_.Next(0, MaxCoords.X - sizeX), _.Next(0, MaxCoords.Y - sizeY), _.Next(0, MaxCoords.Z));
            for (int x = topLeft.X; x < topLeft.X + sizeX; x++)
            {
                for (int y = topLeft.Y; y < topLeft.Y + sizeY; y++)
                {
                    AddCell(tile, new Coord(x, y, 1), color);
                }
            }
        }

        private ICell AddCell(ICell cell)
        {
            cells.Add(cell);
            return cell;
        }

        private ICell AddCell(char tile, ICoord coords, ConsoleColor color)
        {

            ICell cell = new Cell(tile, coords, color);
            cells.Add(cell);
            return cell;
        }

        private ICell AddCell(char tile, ConsoleColor color)
        {
            
            Coord coords = GenCoords();
            if (GetCell(coords) == null)
            {
                return AddCell(tile, coords, color);
            }
            else
            {
                return AddCell(tile, color);
            }
        }

        private Coord GenCoords()
        {
            Random rand = new();
            int x = rand.Next(1, MaxCoords.X);
            int y = rand.Next(1, MaxCoords.Y);
            int z = rand.Next(1, MaxCoords.Z);
            return new(x, y, z);
        }

        public ICell? GetCell(ICoord coords)
        {
            ICell? r = cells.Find(cell => cell.Coords.Equals(coords));
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
                ICell? cell = GetCell(new Coord(x, y, z));
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

        public void AddPlayer(string Name, char tile, ConsoleColor color)
        {
            Coord coord = GenCoords();
            if (GetCell(coord) == null)
            {
                Peg player = new(IPeg.PegType.Player, coord, Name, tile, color);
                AddCell(player);
            }
            else
            {
                AddPlayer(Name, tile, color);
            }
        }
    }
}
