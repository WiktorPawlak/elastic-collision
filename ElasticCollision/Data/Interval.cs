using System;
using ExtensionMethods;

using static ElasticCollision.Data.Vector;
namespace ElasticCollision.Data
{

    public record Interval(double low, double high)
    {
        public double midpoint { get { return (low + high) / 2; } }
        public double length { get { return (high - low); } }

        public Interval Shrink(double r) => new Interval(low + r, high - r);

        public bool contains(double loc) => low <= loc && loc <= high;
        public (Interval, Interval) Split()
        {
            return (this with { high = midpoint },
               this with { low = midpoint });
        }
    }
}
