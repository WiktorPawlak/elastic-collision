using System;
using ExtensionMethods;

using static ElasticCollision.Data.Vector;
namespace ElasticCollision.Data
{
    public record Area(
         Vector UpperLeftCorner,
         Vector LowerRightCorner
    )
    {

        public double Top { get { return UpperLeftCorner.Y; } }
        public double Bottom { get { return LowerRightCorner.Y; } }
        public double Left { get { return UpperLeftCorner.X; } }
        public double Right { get { return LowerRightCorner.X; } }
        public Area Shrink(double r)
        {
            return new Area(UpperLeftCorner + vec(r, r),
                   LowerRightCorner - vec(r, r));
        }
        public bool Contains(Vector loc)
        {
            return ContainsVertically(loc) && ContainsHorizontally(loc);
        }
        public bool ContainsVertically(Vector loc)
        {
            return Top <= loc.Y && loc.Y <= Bottom;
        }
        public bool ContainsHorizontally(Vector loc)
        {
            return Left <= loc.X && loc.X <= Right;
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
            var midpoint = (Top + Bottom) / 2;
            var left = vec(Left, midpoint);
            var right = vec(Right, midpoint);
            return (this with { LowerRightCorner = right },
               this with { UpperLeftCorner = left });
        }
        ///  |
        /// o|o
        ///  |
        public (Area, Area) SplitVertically()
        {
            var midpoint = (Left + Right) / 2;
            var top = vec(midpoint, Top);
            var bottom = vec(midpoint, Bottom);
            return (this with { LowerRightCorner = bottom },
               this with { UpperLeftCorner = top });
        }

    }
}
