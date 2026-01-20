
using Game.Interfaces;
using System.Collections.Generic;
using System.Drawing;

namespace Game
{
    public class Grid(ICoord maxCoords) : IGrid
    {
        private static readonly char maxTile = (char)65535;
        private static readonly char minTile = (char)0;

        public ICoord MaxCoords { get; } = maxCoords;

        private List<ICell> cells = [];

        private readonly Cell _defaultCell = new('.', Coord._default, ConsoleColor.DarkGreen);

        public void Generate()
        {
            cells = [];
            Random _ = new();
            int Hills = _.Next(1, 5);
            for (int i = 0; i < Hills; i++)
            {
                AddRandomCircle('^', ConsoleColor.DarkGray);
            }
            int Forests = _.Next(1, 3);
            for (int i = 0; i < Forests; i++)
            {
                AddRandomCircle('Y', ConsoleColor.DarkGreen);
            }
            int Lakes = _.Next(3, 5);
            for (int i = 0; i < Lakes; i++)
            {
                AddRandomCircle('~', ConsoleColor.DarkBlue);
            }
            int Enemies = _.Next(3, 10);
            for (int i = 0; i < Enemies; i++)
            {
                AddRandomCell('ª', color: ConsoleColor.DarkRed);
            }

            int rivers = _.Next(0, 2);
            for (int i = 0; i < rivers; i++)
            {
                AddRandomMultiPointLine('~', ConsoleColor.Blue);
            }

            int towns = _.Next(1, 4);
            for (int i = 0; i < towns; i++)
            {
                AddRandomSquare('A', ConsoleColor.White);
            }

            int fields = _.Next(2, 5);
            for (int i = 0; i < fields; i++)
            {
                AddRandomSquare('#', ConsoleColor.DarkYellow);
            }

            AddRandomCell('H', color: ConsoleColor.DarkMagenta);

            AddPeg("Hero", (char)0x2573, color: ConsoleColor.Yellow);

        }

        private void AddRandomMultiPointLine(char tile, ConsoleColor color = ConsoleColor.White)
        {
            var _ = new Random();
            var midpoints = _.Next(2, 5);
            List<ICoord> points = [];
            points.Add(new Coord(0, _.Next(0, MaxCoords.Y), 1));

            for (int i = 1; i < midpoints; i++)
            {
                ICoord mid = new Coord(_.Next(MaxCoords.X / midpoints * i, MaxCoords.X / midpoints * (i + 1)), _.Next(0, MaxCoords.Y), 1);
                points.Add(mid);
            }

            points.Add(new Coord(MaxCoords.X - 1, _.Next(0, MaxCoords.Y), 1));

            AddMultiPointLine(tile, points, color);

        }

        private void AddMultiPointLine(char tile, List<ICoord> points, ConsoleColor color = ConsoleColor.White)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                AddLine(tile, points[i], points[i + 1], color);
            }
        }

        private void AddLine(char tile, ICoord start, ICoord end, ConsoleColor color = ConsoleColor.White)
        {
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

        private void AddRandomCircle(char tile, ConsoleColor color = ConsoleColor.White)
        {
            var _ = new Random();
            ICoord center = new Coord(_.Next(0, MaxCoords.X), _.Next(0, MaxCoords.Y), _.Next(0, MaxCoords.Z));
            int minRad = Math.Min(MaxCoords.X, MaxCoords.Y);
            int radius = _.Next(1, minRad / _.Next(1, minRad));
            AddCircle(tile, center, radius, color);
        }

        private void AddCircle(char tile, ICoord center, int radius, ConsoleColor color = ConsoleColor.White)
        {
            for (int x = center.X - radius; x <= center.X + radius; x++)
            {
                for (int y = center.Y - radius; y <= center.Y + radius; y++)
                {
                    if (x >= 0 && x < MaxCoords.X && y >= 0 && y < MaxCoords.Y)
                    {
                        double distance = Math.Sqrt(Math.Pow(x - center.X, 2) + Math.Pow(y - center.Y, 2));
                        if (distance <= radius)
                        {
                            AddCell(tile, new Coord(x, y, 1), color);
                        }
                    }
                }
            }
        }

        private void AddRandomSquare(char tile, ConsoleColor color = ConsoleColor.White)
        {
            var _ = new Random();
            int sizeX = _.Next(2, MaxCoords.X / 4);
            int sizeY = _.Next(2, MaxCoords.Y / 4);
            ICoord topLeft = new Coord(_.Next(0, MaxCoords.X - sizeX), _.Next(0, MaxCoords.Y - sizeY), _.Next(0, MaxCoords.Z));
            AddSquare(tile, topLeft, sizeX, sizeY, color);
        }

        private void AddSquare(char tile, ICoord topLeft, int sizeX, int sizeY, ConsoleColor color = ConsoleColor.White)
        {
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

        private void RemoveCell(ICoord coords)
        {
            ICell? cell = GetCell(coords);
            if (cell != null)
            {
                cells.Remove(cell);
            }
        }

        private ICell AddCell(char tile, ICoord coords, ConsoleColor color = ConsoleColor.White)
        {

            ICell cell = new Cell(tile, coords, color);
            cells.Add(cell);
            return cell;
        }

        private ICell AddRandomCell(char tile, int depth = 0, int maxdepth = 10, ConsoleColor color = ConsoleColor.White)
        {

            Coord coords = GenCoords();
            if (GetCell(coords) == null)
            {
                return AddCell(tile, coords, color);
            }
            else
            {
                if (depth <= maxdepth)
                    return AddRandomCell(tile, depth++, maxdepth, color);
                else
                    return _defaultCell;
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
                    Console.ForegroundColor = _defaultCell.Color;
                    Console.Write(_defaultCell.tile);
                    Console.ResetColor();
                }
            }
        }

        public void AddPeg(string Name, char tile, int depth = 0, int maxdepth = 10, ConsoleColor color = ConsoleColor.White)
        {
            Coord coord = GenCoords();
            if (GetCell(coord) == null)
            {
                AddCell(new Peg(IPeg.PegType.Player, coord, tile, color));
            }
            else
            {
                if (depth <= maxdepth)
                    AddPeg(Name, tile, depth++, maxdepth, color);
                else
                {
                    RemoveCell(coord);
                    AddCell(new Peg(IPeg.PegType.Player, coord, tile, color));
                }
            }
        }
    }
}
