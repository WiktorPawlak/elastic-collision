using System;
using ExtensionMethods;

using static ElasticCollision.Data.Vector;
namespace ElasticCollision.Data
{
    public record Area(
         Interval Horizontal,
         Interval Vertical
    ) : Section
    {

        public double Top { get { return UpperLeftCorner.Y; } }
        public double Bottom { get { return LowerRightCorner.Y; } }
        public double Left { get { return UpperLeftCorner.X; } }
        public double Right { get { return LowerRightCorner.X; } }
        public Vector UpperLeftCorner { get { return vec(Horizontal.low, Vertical.low); } }
        public Vector LowerRightCorner { get { return vec(Horizontal.high, Vertical.high); } }

        public static Area FromCorners(Vector tl, Vector br)
        {
            var vert = new Interval(tl.Y, br.Y);
            var hori = new Interval(tl.X, br.X);
            return new Area(hori, vert);
        }

        public Area Shrink(double r)
        {
            return new Area(Horizontal.Shrink(r), Vertical.Shrink(r));
        }

        public bool Contains(Vector loc)
        {
            return Vertical.Contains(loc.Y) && Horizontal.Contains(loc.X);
        }

        private static readonly Random rng = new Random();

        public Vector GetRandomLocation()
        {
            double x = rng.NextDoubleInRange(Left, Right);
            double y = rng.NextDoubleInRange(Bottom, Top);
            return vec(x, y);
        }

        ///  o
        /// ---
        ///  o
        public (Area, Area) SplitHorizontally()
        {
            var (top, bottom) = Vertical.Split();
            return (this with { Vertical = top },
               this with { Vertical = bottom });
        }
        ///  |
        /// o|o
        ///  |
        public (Area, Area) SplitVertically()
        {
            var (left, right) = Horizontal.Split();
            return (this with { Horizontal = left },
               this with { Horizontal = right });
        }

        public bool FullyContains(Ball b) => Shrink(b.Radius).Contains(b.Location);

        public bool Intersects(Ball b) => Shrink(-b.Radius).Contains(b.Location);

    }
}
