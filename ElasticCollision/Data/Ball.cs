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

        public Ball Budge(double Δt) => this with { Location = Location + (Velocity * Δt) };

        public Vector Momentum { get { return Velocity * Mass; } }

        public Ball ApplyImpulse(Vector Momentum) => this with { Velocity = Velocity + Momentum * (1 / Mass) };

        public double KineticEnergy
        {
            get
            {
                double speed = Velocity.Magnitude;
                return speed * speed * Mass * 0.5;
            }
        }

    }
}
