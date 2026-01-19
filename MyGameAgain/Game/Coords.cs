using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Coords : IComparable<Coords>
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public static readonly Coords _default = new(0,0,0);

        public Coords()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }
        public Coords(int x, int y, int z = 1)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int CompareTo(Coords? other)
        {
            return other == null ? 1 : (this.X == other.X && this.Y == other.Y && this.Z == other.Z) ? 0 : -1;
        }

        
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Coords c = (Coords)obj;
            return (this.X == c.X && this.Y == c.Y && this.Z == c.Z);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Coords left, Coords right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Coords left, Coords right)
        {
            return !(left == right);
        }

        public static bool operator <(Coords left, Coords right)
        {
            return Equals(left, null) ? !Equals(right, null) : left.CompareTo(right) < 0;
        }

        public static bool operator <=(Coords left, Coords right)
        {
            return Equals(left, null) || left.CompareTo(right) <= 0;
        }

        public static bool operator >(Coords left, Coords right)
        {
            return !Equals(left, null) && left.CompareTo(right) > 0;
        }

        public static bool operator >=(Coords left, Coords right)
        {
            return Equals(left, null) ? Equals(right, null) : left.CompareTo(right) >= 0;
        }
    }
}
