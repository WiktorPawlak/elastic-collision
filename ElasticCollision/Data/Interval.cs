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

        public bool Contains(double loc) => low <= loc && loc <= high;

        public (Interval, Interval) Split()
        {
            return (this with { high = midpoint },
               this with { low = midpoint });
        }
    }
    public record HorizontalInterval(double low, double high) : Interval(low, high), Section
    {
        public bool FullyContains(Ball b) => Shrink(b.Radius).Contains(b.Location.X);

        public bool Intersects(Ball b) => Shrink(-b.Radius).Contains(b.Location.X);

        // static typing and its consequences
        public (Section, Section) SplitSection() => (this with { high = midpoint }, this with { low = midpoint });

    }
    public record VerticalInterval(double low, double high) : Interval(low, high), Section
    {
        public bool FullyContains(Ball b) => Shrink(b.Radius).Contains(b.Location.Y);

        public bool Intersects(Ball b) => Shrink(-b.Radius).Contains(b.Location.Y);

        public (Section, Section) SplitSection() => (this with { high = midpoint }, this with { low = midpoint });
    }
}
