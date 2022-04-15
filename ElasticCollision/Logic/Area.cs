using System;
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
            return UpperLeftCorner.X <= loc.X &&
                loc.X <= LowerRightCorner.X &&
                UpperLeftCorner.Y <= loc.Y &&
                loc.Y <= LowerRightCorner.Y;
        }
    }

}
