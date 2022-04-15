using System.Collections.Generic;
using System.Linq;
namespace ElasticCollision.Logic
{
    public record WorldState(
         IEnumerable<Ball> Balls,
         Area area
    )
    {
        public WorldState Move(double Δt)
        {
            var newBalls = Balls.Select(ball => ball.Budge(Δt).Collide(area));
            // tutaj będzie kod zderzający piłeczki
            // zasadniczo trzeba podzielić je na dwie kolekcje
            // > piłeczki które nie zderzyły się z innymi piłeczkami
            // > pary przecinających się piłeczek
            // te drugie zderzyć i dodać obie grupy
            return this with { Balls = newBalls };
        }
    }
}
