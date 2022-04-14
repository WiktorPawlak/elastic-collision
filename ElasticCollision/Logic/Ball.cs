using System;

namespace ElasticCollision.Logic
{
    public record Ball(
         double Radius,
         double Mass,
         Vector Location,
         Vector Velocity
    );
}
