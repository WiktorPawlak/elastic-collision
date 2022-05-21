using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ExtensionMethods;

namespace ElasticCollision.Data
{
    public record WorldState(
         IEnumerable<Ball> Balls,
         Area Area
    )
    {
        private static readonly Random rng = new Random();

        public WorldState Proceed(double Δt)
        {
            var newBalls = Balls
                .AsParallel()
                .AsOrdered()
                .Select(ball => ball
                .Budge(Δt))
                .ToList();
            return this with { Balls = newBalls };
        }

        public WorldState AddBall(Ball ball)
        {
            return this with { Balls = Balls.Append(ball) };
        }
        public WorldState AddBall(double radius, double mass)
        {
            var location = Area.Shrink(radius).GetRandomLocation();

            double x = rng.NextDoubleInRange(-100, 100);
            double y = rng.NextDoubleInRange(-100, 100);

            var velocity = new Vector(x, y);
            return AddBall(new Ball(radius, mass, location, velocity));
        }

        public WorldState AddBalls(int count, double radius, double mass)
        {
            return Enumerable.Range(0, count).Aggregate(this, (state, _) => state.AddBall(radius, mass));
        }

        public WorldState ApplyForces(IEnumerable<Vector> forces)
        {
            var newBalls = Balls.Zip(forces, (ball, force) => ball.ApplyImpulse(force)).ToList();
            var newState = this with { Balls = newBalls };

            return newState.ScaleVelocity(Math.Sqrt(KinecticEnergy / newState.KinecticEnergy));
        }

        private WorldState ScaleVelocity(double scale)
        {
            var newBalls = Balls.Select(ball => (ball with { Velocity = ball.Velocity * scale })).ToList();
            return this with { Balls = newBalls };
        }

        public double KinecticEnergy
        {
            get
            {
                return Balls.Select(ball => ball.KineticEnergy)
                  .Sum();
            }
        }
    }
}
