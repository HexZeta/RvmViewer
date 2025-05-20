using System;
using System.Collections.Generic;
using System.Text;

namespace HexZeta.PlantTools.RvmViewer.Prim
{
    class Position
    {
        public double X;
        public double Y;
        public double Z;

        public Position() { }
        public Position(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object? obj) // Updated to use nullable reference type
        {
            if (obj is Position p)
            {
                double d = Math.Abs(p.X - X) + Math.Abs(p.Y - Y) + Math.Abs(p.Z - Z);
                return d < 0.1;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (int)(X + Y * 100 + Z * 10000);
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", X, Y, Z);
        }
    }

    class Face
    {
        public readonly List<Position> PtList = new List<Position>();

        public Face(Position a, Position b, Position c)
        {
            PtList.Add(a);
            PtList.Add(b);
            PtList.Add(c);
        }

        public Face(Position a, Position b, Position c, Position d)
        {
            PtList.Add(a);
            PtList.Add(b);
            PtList.Add(c);
            PtList.Add(d);
        }

        public Face(List<Position> list)
        {
            PtList.AddRange(list);
        }

        public Face()
        {

        }

        public void Add(Position p)
        {
            PtList.Add(p);
        }
    }
}