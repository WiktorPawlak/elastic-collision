using System;
/// pair of x, y coordinates, used as vectors, coordinates, deltas of the above

namespace ElasticCollision.Data
{
    public record class Vector(double x, double y)
    {
        public static Vector vec(double x, double y) => new Vector(x, y);

        public double Magnitude() => Math.Sqrt(x * x + y * y);

        public static double Distance(Vector l, Vector r) => (l - r).Magnitude();

        public static Vector operator +(Vector l, Vector r)
        {
            var x = l.x + r.x;
            var y = l.y + r.y;
            return vec(x, y);
        }

        public static Vector operator -(Vector v) => vec(-v.x, -v.y);

        public static Vector operator -(Vector l, Vector r) => l + (-r);

        public static Vector operator *(Vector v, double f) => vec(v.x * f, v.y * f);

        public static Vector operator *(double f, Vector v) => v * f;
    }
}
