using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticCollision.Data
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
            double distance = Vector.Distance(Location, other.Location);
            double reach = Radius + other.Radius;
            return distance <= reach;
        }

        public Ball Budge(double Δt)
        {
            return this with { Location = Location + (Velocity * Δt) };
        }

        public bool Within(Area area)
        {
            return area.Shrink(Radius).Contains(Location);
        }
        public bool Intersects(Area area)
        {
            return area.Shrink(-2 * Radius).Contains(Location);
        }

        public Ball Collide(Area area)
        {
            var shrunk = area.Shrink(Radius);
            var X = Velocity.X;
            var Y = Velocity.Y;

            if (Location.X < shrunk.Left) { X = Math.Abs(X); }
            if (Location.X > shrunk.Right) { X = -Math.Abs(X); }

            if (Location.Y < shrunk.Top) { Y = Math.Abs(Y); }
            if (Location.Y > shrunk.Bottom) { Y = -Math.Abs(Y); }

            return this with { Velocity = Vector.vec(X, Y) };
        }

        public double KineticEnergy
        {
            get
            {
                double speed = Velocity.Magnitude;
                return speed * speed * Mass * 0.5;
            }
        }

        public Vector Momentum { get { return Velocity * Mass; } }

        public Ball ApplyImpulse(Vector Momentum)
        {
            return this with { Velocity = Velocity + Momentum * (1 / Mass) };
        }

        public bool Approaching(Ball other)
        {
            var direction = other.Location - this.Location;
            var relative_velocity = (this.Velocity - other.Velocity).On(direction);
            return direction.SameDir(relative_velocity);
        }

        public Vector CollisionImpulse(Ball other) // for self
        {
            var direction = other.Location - this.Location;
            var relative_velocity = (this.Velocity - other.Velocity).On(direction);
            return -(relative_velocity * other.Mass);
        }

        public Ball Collide(IEnumerable<Ball> Neighbors)
        {
            var TotalImpulse = Neighbors
                .Where(other => this.Touching(other) && this.Approaching(other)) // filter
                .Select(other => this.CollisionImpulse(other)) // map
                .Aggregate(Vector.vec(0, 0), (a, b) => a + b); // reduce
            return this.ApplyImpulse(TotalImpulse); // tylko z pozamienianymi nazwami
        }
    }
}
