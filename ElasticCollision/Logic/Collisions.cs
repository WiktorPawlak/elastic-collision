using ElasticCollision.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElasticCollision.Logic
{
    public static class Collisions
    {
        public static bool Touching(Ball a, Ball b)
        {
            double distance = Vector.Distance(a.Location, b.Location);
            double reach = a.Radius + b.Radius;
            return distance <= reach;
        }

        public static Vector Collide(Area area, Ball ball)
        {
            var shrunk = area.Shrink(ball.Radius);
            var X = ball.Velocity.X;
            var Y = ball.Velocity.Y;
            var deltaX = 0.0;
            var deltaY = 0.0;

            if (ball.Location.X < shrunk.UpperLeftCorner.X) { deltaX = Math.Abs(X); }
            if (ball.Location.X > shrunk.LowerRightCorner.X) { deltaX = -Math.Abs(X); }

            if (ball.Location.Y < shrunk.UpperLeftCorner.Y) { deltaY = Math.Abs(Y); }
            if (ball.Location.Y > shrunk.LowerRightCorner.Y) { deltaY = -Math.Abs(Y); }
            Vector arm = Vector.vec(deltaX, deltaY);


            return 2 * arm * ball.Mass;
        }

        public static Vector Momentum(Ball ball)
        {
            return ball.Velocity * ball.Mass;
        }

        public static bool Approaching(Ball a, Ball b)
        {
            var direction = a.Location - b.Location;
            var relative_velocity = (a.Velocity - b.Velocity).On(direction);
            return direction.SameDir(relative_velocity);
        }

        public static Vector CollisionImpulse(Ball self, Ball other)
        {
            var direction = self.Location - other.Location;
            var relative_velocity = (self.Velocity - other.Velocity).On(direction);
            return -(relative_velocity * other.Mass);
        }

        public static Vector Collide(Ball self, IEnumerable<Ball> Neighbors)
        {
            return Neighbors
                .Where(other => Touching(self, other) && Approaching(self, other))
                .Select(other => CollisionImpulse(self, other))
                .Aggregate(Vector.vec(0, 0), (a, b) => a + b);
        }
    };
}
