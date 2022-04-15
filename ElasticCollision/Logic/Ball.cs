using System;
using static ElasticCollision.Logic.Vector;
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
        public Ball Budge(double Δt)
        {
            // (っ◔◡◔)っ ♥ thesaurus ♥
            return this with { Location = Location + (Velocity * Δt) };
        }

        public bool Within(Area area)
        {
            return area.Shrink(Radius).Contains(Location);
        }

    };
}
