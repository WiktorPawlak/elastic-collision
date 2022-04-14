using System;

namespace ElasticCollision.Data
{
    public record Ball(
         double Radius,
         double Mass,
         Vector Location,
         Vector Velocity
    );
}
