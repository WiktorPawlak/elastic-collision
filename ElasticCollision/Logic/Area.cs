using System;
using ExtensionMethods;

using static ElasticCollision.Logic.Vector;
namespace ElasticCollision.Logic
{
    public record Area(
         Vector UpperLeftCorner,
         Vector LowerRightCorner
    )
    {
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
            return UpperLeftCorner.Y <= loc.Y &&
                loc.Y <= LowerRightCorner.Y;
        }
        public bool ContainsHorizontally(Vector loc)
        {
            return UpperLeftCorner.X <= loc.X &&
            loc.X <= LowerRightCorner.X;
        }
        private static readonly Random rng = new Random();

        public Vector GetRandomLocation()
        {
            double x = rng.NextDoubleInRange(UpperLeftCorner.X, LowerRightCorner.X);
            double y = rng.NextDoubleInRange(LowerRightCorner.Y, UpperLeftCorner.Y);
            return vec(x, y);
        }
    }
}
