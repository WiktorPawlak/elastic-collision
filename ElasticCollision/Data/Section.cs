using System;
using ExtensionMethods;
namespace ElasticCollision.Data
{
    public interface Section
    // abstraction over areas and intervals,
    // provides methods to check whether ball fits within and intersects with.
    {
        bool FullyContains(Ball b);

        bool Intersects(Ball b);

    }
}
