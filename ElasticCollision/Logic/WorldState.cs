using System;
using System.Collections.Immutable;
namespace ElasticCollision.Logic
{
    public record WorldState(
         ImmutableList<Ball> Balls,
    );
}
