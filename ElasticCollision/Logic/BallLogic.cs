using System;
using System.Collections.Generic;
using System.Linq;
using static ElasticCollision.Logic.Vector;
namespace ElasticCollision.Logic
{
    public record BallLogic(
         double Radius,
         double Mass,
         Vector Location,
         Vector Velocity
    )
    {
        public bool Touching(BallLogic other)
        {
            double distance = Distance(Location, other.Location);
            double reach = Radius + other.Radius;
            return distance <= reach;
        }
        public BallLogic Budge(double Δt)
        {
            // (っ◔◡◔)っ ♥ thesaurus ♥
            return this with { Location = Location + (Velocity * Δt) };
        }

        public bool Within(Area area)
        {
            return area.Shrink(Radius).Contains(Location);
        }

        public BallLogic Collide(Area area)
        {
            var shrunk = area.Shrink(Radius);
            var X = Velocity.X;
            var Y = Velocity.Y;

            if (Location.X < shrunk.UpperLeftCorner.X) { X = Math.Abs(X); }
            if (Location.X > shrunk.LowerRightCorner.X) { X = -Math.Abs(X); }

            if (Location.Y < shrunk.UpperLeftCorner.Y) { Y = Math.Abs(Y); }
            if (Location.Y > shrunk.LowerRightCorner.Y) { Y = -Math.Abs(Y); }

            return this with { Velocity = vec(X, Y) };
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

        public BallLogic ApplyImpulse(Vector Momentum)
        {
            return this with { Velocity = Velocity + Momentum * (1 / Mass) };
        }

        public bool Approaching(BallLogic other)
        {
            var direction = other.Location - this.Location;
            var relative_velocity = (this.Velocity - other.Velocity).On(direction);
            return direction.SameDir(relative_velocity);
        }

        public Vector CollisionImpulse(BallLogic other) // for self
        {
            var direction = other.Location - this.Location;
            var relative_velocity = (this.Velocity - other.Velocity).On(direction);
            return -(relative_velocity * other.Mass);
        }

        public BallLogic Collide(IEnumerable<BallLogic> Neighbors)
        {
            var TotalImpulse = Neighbors
                .Where(other => this.Touching(other) && this.Approaching(other)) // filter
                .Select(other => this.CollisionImpulse(other)) // map
                .Aggregate(vec(0, 0), (a, b) => a + b); // reduce
            return this.ApplyImpulse(TotalImpulse); // tylko z pozamienianymi nazwami
        }
    };
}
