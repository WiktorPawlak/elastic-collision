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

        public Ball Collide(Area area)
        {
            var shrunk = area.Shrink(Radius);
            var X = shrunk.ContainsHorizontally(Location) ? Velocity.X : -Velocity.X;
            var Y = shrunk.ContainsVertically(Location) ? Velocity.Y : -Velocity.Y;
            return this with { Velocity = vec(X, Y) };
            // lokalizacji nie "naprawiamy", w nadzieji, że Δt będzie na tyle mały
            // że to nie będzie miało znaczenia
        }
        public double KineticEnergy
        {
            get
            {
                double speed = Velocity.Magnitude;
                return speed * speed * Mass * 0.5;
            }
        }
    };
}
