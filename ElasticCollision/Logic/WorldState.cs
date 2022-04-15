using System;
using System.Collections.Immutable;
namespace ElasticCollision.Logic
{
    public record WorldState(
         ImmutableList<Ball> Balls,
         // ściany nie będą wpisane na sztywno, nie wypada tak
         ImmutableList<Wall> Walls
    );
}
