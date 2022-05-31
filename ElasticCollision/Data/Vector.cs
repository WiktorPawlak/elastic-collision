using System;
/// pair of x, y coordinates, used as vectors, coordinates, deltas of the above

namespace ElasticCollision.Data
{
    public record class Vector(double X, double Y)
    {
        public override string ToString() => "[" + X + "," + Y + "]";

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

        public static Vector operator /(Vector v, double f) => vec(v.X / f, v.Y / f);

        public static Vector operator /(double f, Vector v) => v / f;

        /// iloczyn skalarny
        public static double operator *(Vector a, Vector b) => a.X * b.X + a.Y * b.Y;

        public Vector On(Vector basis)
        {
            // https://www.obliczeniowo.com.pl/64
            var P1 = basis;
            var P3 = this;
            return (P1 * P3 * P1) / (P1 * P1);
        }

        // other should be parallel
        public bool SameDir(Vector other)
        {
            return (this * other) > 0;
        }
    }
}
