using System;
using System.Collections.Generic;
using System.Linq;
using ElasticCollision.Data;

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

        public static void CollideWalls(Area area, Ball ball)
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


            ((BallWithJunk)ball).cb(2 * arm * ball.Mass);
        }

        public static bool Approaching(Ball a, Ball b)
        {
            var direction = b.Location - a.Location;
            var relative_velocity = (a.Velocity - b.Velocity).On(direction);
            return direction.SameDir(relative_velocity);
        }

        public static Ball CollisionImpulse(Ball self, Ball other)
        {
            // zwraca drugą kulkę
            // cały ten kod to jest jeden wielki dramat
            // AlE KuLkI siĘ SAMe RuSzajOm❕❕❕❕❕❕❕❕❕
            var direction = other.Location - self.Location;
            var relative_velocity = (other.Velocity - self.Velocity).On(direction);
            var our_impulse = relative_velocity * other.Mass;
            var other_impulse = relative_velocity * -self.Mass;
            ((BallWithJunk)self).cb.Invoke(our_impulse);
            ((BallWithJunk)other).cb.Invoke(other_impulse);
            return other.ApplyImpulse(other_impulse);
        }

        public static IEnumerable<Ball> CollideBalls(Ball self, IEnumerable<Ball> Neighbors)
        {
            return Neighbors
                .Where(other => Touching(self, other) && Approaching(self, other))
                .Select(other => CollisionImpulse(self, other));
        }

        public static IEnumerable<Ball> Collide(Ball self, Area area, List<Ball> neighbours)
        {
            CollideWalls(area, self);
            return CollideBalls(self, neighbours);
        }
    };
}
