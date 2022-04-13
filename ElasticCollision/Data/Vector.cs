using System;
/// pair of x, y coordinates, used as vectors, coordinates, deltas of the above

namespace ElasticCollision.Data
{
    public class Vector
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        private Vector(double X, double Y) // use `vec` instead
        {
            this.X = X;
            this.Y = Y;
        }

        public double Magnitude() => Math.Sqrt(X * X + Y * Y);

        public static double Distance(Vector l, Vector r) => (l - r).Magnitude();

        public static Vector CreateVector(double x, double y) => new Vector(x, y);

        public override bool Equals(object obj)
        {
            return obj is Vector vector &&
                   X == vector.X &&
                   Y == vector.Y;
        }

        public static Vector operator +(Vector l, Vector r)
        {
            var x = l.X + r.X;
            var y = l.Y + r.Y;
            return CreateVector(x, y);
        }
        public static Vector operator -(Vector v) => CreateVector(-v.X, -v.Y);
        public static Vector operator -(Vector l, Vector r) => l + (-r);
        public static Vector operator *(Vector v, double f) => CreateVector(v.X * f, v.Y * f);
        public static Vector operator *(double f, Vector v) => v * f;
    }
}
