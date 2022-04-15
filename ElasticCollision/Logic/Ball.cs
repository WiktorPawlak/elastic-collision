using System;
using static ElasticCollision.Logic.Vector
namespace ElasticCollision.Logic
{
    public record Ball(
         double Radius,
         double Mass,
         Vector Location,
         Vector Velocity
    )
    {
        public bool Touching(Ball other)
        {
            double distance = Distance(Location, other.Location);
            double reach = Radius + other.Radius;
            return distance <= reach;
        }
    };
}
