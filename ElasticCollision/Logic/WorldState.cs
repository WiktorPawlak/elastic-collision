using System.Collections.Generic;
using System.Linq; // my beloved
namespace ElasticCollision.Logic
{
    public record WorldState(
         IEnumerable<Ball> Balls,
         Area area
    )
    {
        public WorldState Proceed(double Δt)
        {
            var newBalls = Balls.Select(ball => ball.Budge(Δt).Collide(area));
            // tutaj będzie kod zderzający piłeczki
            // zasadniczo trzeba podzielić je na dwie kolekcje
            // > piłeczki które nie zderzyły się z innymi piłeczkami
            // > pary przecinających się piłeczek
            // te drugie zderzyć i dodać obie grupy
            return this with { Balls = newBalls };
        }

        public WorldState AddBall(Ball ball)
        {
            return this with { Balls = Balls.Append(ball) };
        }
        public WorldState AddBall(double radius, double mass)
        {
            var location = area.Shrink(radius).GetRandomLocation();
            var velocity = new Vector(100, 100);
            return AddBall(new Ball(radius, mass, location, velocity));
        }
    }
}
