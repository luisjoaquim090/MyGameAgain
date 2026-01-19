using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Coord : ICoord,IComparable<Coord>
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public static readonly Coord _default = new(0,0,0);

        public Coord()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }
        public Coord(int x, int y, int z = 1)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int CompareTo(Coord? other)
        {
            return other == null ? 1 : (this.X == other.X && this.Y == other.Y && this.Z == other.Z) ? 0 : -1;
        }

        
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Coord c = (Coord)obj;
            return (this.X == c.X && this.Y == c.Y && this.Z == c.Z);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Coord left, Coord right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Coord left, Coord right)
        {
            return !(left == right);
        }

        public static bool operator <(Coord left, Coord right)
        {
            return Equals(left, null) ? !Equals(right, null) : left.CompareTo(right) < 0;
        }

        public static bool operator <=(Coord left, Coord right)
        {
            return Equals(left, null) || left.CompareTo(right) <= 0;
        }

        public static bool operator >(Coord left, Coord right)
        {
            return !Equals(left, null) && left.CompareTo(right) > 0;
        }

        public static bool operator >=(Coord left, Coord right)
        {
            return Equals(left, null) ? Equals(right, null) : left.CompareTo(right) >= 0;
        }
    }
}
