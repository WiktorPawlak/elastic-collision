using System;
/// pair of x, y coordinates, used as vectors, coordinates, deltas of the above

namespace ElasticCollision.Logic
{
    public record class Vector(double X, double Y)
    {
        public static Vector vec(double x, double y) => new Vector(x, y);

        public double Magnitude { get { return Math.Sqrt(X * X + Y * Y); } }

        public static double Distance(Vector l, Vector r) => (l - r).Magnitude;

        public static Vector operator +(Vector l, Vector r)
        {
            var x = l.X + r.X;
            var y = l.Y + r.Y;
            return vec(x, y);
        }

        public static Vector operator -(Vector v) => vec(-v.X, -v.Y);

        public static Vector operator -(Vector l, Vector r) => l + (-r);

        public static Vector operator *(Vector v, double f) => vec(v.X * f, v.Y * f);

        public static Vector operator *(double f, Vector v) => v * f;
    }
}
